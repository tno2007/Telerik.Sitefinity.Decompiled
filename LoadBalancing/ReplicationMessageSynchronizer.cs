// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.LoadBalancing.ReplicationMessageSynchronizer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Timers;
using System.Web;
using Telerik.OpenAccess.Exceptions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Configuration.Data;
using Telerik.Sitefinity.Configuration.Model;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Licensing;
using Telerik.Sitefinity.LoadBalancing.Configuration;
using Telerik.Sitefinity.LoadBalancing.Replication;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.LoadBalancing
{
  /// <summary>
  /// Manages synchronization of replication related system messages.
  /// </summary>
  internal class ReplicationMessageSynchronizer
  {
    internal const int DefaultTimestampReadInterval = 3;
    internal const int DefaultTimestampWriteInterval = 2;
    internal const int DefaultWriteInactivityThreshold = 300;
    internal const int DefaultMessageRetryInterval = 2;
    internal const string MultiRegionalDeploymentModuleId = "81346463-63E8-41E0-894F-A34955B377CC";
    private const string ConfigKey = "timestamp";
    private const string ConfigAppName = "ReplicationMessageSynchronizer";
    private static HashSet<string> masterConnections = new HashSet<string>();
    private static HashSet<string> slaveConnections = new HashSet<string>();
    private static ConcurrentDictionary<string, long> connectionTimestamps = new ConcurrentDictionary<string, long>();
    private static ConcurrentDictionary<string, DateTime> connectionUpdates = new ConcurrentDictionary<string, DateTime>();
    private static int unprocessedLimit;
    private static int messageAgeLimit;
    private static ConcurrentStack<SystemMessageBase> unprocessedMessages = new ConcurrentStack<SystemMessageBase>();
    private static TimeSpan inactivityThreshold;
    private static Timer readTimer;
    private static Timer writeTimer;
    private static Timer processTimer;
    private static List<IReplicationMessageTransporter> transporters = new List<IReplicationMessageTransporter>();

    private static bool IsConnectionActive(string connectionName)
    {
      DateTime dateTime;
      return ReplicationMessageSynchronizer.connectionUpdates.TryGetValue(connectionName, out dateTime) && DateTime.UtcNow - dateTime < ReplicationMessageSynchronizer.inactivityThreshold;
    }

    private static void OnProcessTimerElapsed(object sender, ElapsedEventArgs e)
    {
      if (ReplicationMessageSynchronizer.unprocessedMessages.Count == 0)
        return;
      List<SystemMessageBase> systemMessageBaseList1 = new List<SystemMessageBase>();
      SystemMessageBase[] systemMessageBaseArray = new SystemMessageBase[ReplicationMessageSynchronizer.unprocessedMessages.Count];
      ReplicationMessageSynchronizer.unprocessedMessages.TryPopRange(systemMessageBaseArray);
      long timestampLimit = DateTime.UtcNow.AddSeconds((double) -ReplicationMessageSynchronizer.messageAgeLimit).Ticks;
      SystemMessageBase[] array = ((IEnumerable<SystemMessageBase>) systemMessageBaseArray).Where<SystemMessageBase>((Func<SystemMessageBase, bool>) (m => m != null & m.Timestamp >= timestampLimit)).OrderBy<SystemMessageBase, long>((Func<SystemMessageBase, long>) (m => m.Timestamp)).Take<SystemMessageBase>(ReplicationMessageSynchronizer.unprocessedLimit).ToArray<SystemMessageBase>();
      List<SystemMessageBase> systemMessageBaseList2 = new List<SystemMessageBase>();
      foreach (SystemMessageBase systemMessageBase in array)
      {
        long connectionTimestamp = ReplicationMessageSynchronizer.GetConnectionTimestamp(systemMessageBase.Connection);
        if (systemMessageBase.Timestamp <= connectionTimestamp)
          systemMessageBaseList2.Add(systemMessageBase);
        else
          systemMessageBaseList1.Add(systemMessageBase);
      }
      if (systemMessageBaseList2.Count > 0)
        ReplicationMessageSynchronizer.HandleMessages(systemMessageBaseList2.ToArray());
      if (systemMessageBaseList1.Count <= 0)
        return;
      ReplicationMessageSynchronizer.unprocessedMessages.PushRange(systemMessageBaseList1.ToArray());
      ReplicationMessageSynchronizer.processTimer.Start();
    }

    private static void ReadDatabaseTimestamp()
    {
      if (SystemManager.Initializing)
        return;
      foreach (OpenAccessConnection connection in OpenAccessConnection.GetConnections())
      {
        if (connection.Replication == OpenAccessConnection.ReplicationMode.Slave)
        {
          using (SitefinityOAContext context = ReplicationMessageSynchronizer.GetContext(connection))
          {
            ConfigVarialble configVarialble = context.GetAll<ConfigVarialble>().Where<ConfigVarialble>((Expression<Func<ConfigVarialble, bool>>) (c => c.Key == "timestamp" && c.ApplicationName == nameof (ReplicationMessageSynchronizer))).FirstOrDefault<ConfigVarialble>();
            if (configVarialble != null)
            {
              long result;
              if (long.TryParse(configVarialble.Value, out result))
                ReplicationMessageSynchronizer.connectionTimestamps[connection.Name] = result;
            }
          }
        }
      }
    }

    /// <summary>Update master database timestamp.</summary>
    /// <returns>True if at least one db was active and updated properly. False if all connections are inactive.</returns>
    private static bool WriteDatabaseTimestamp()
    {
      bool flag = false;
      long ticks = DateTime.UtcNow.Ticks;
      foreach (OpenAccessConnection connection in OpenAccessConnection.GetConnections())
      {
        if (connection.Replication == OpenAccessConnection.ReplicationMode.Master && ReplicationMessageSynchronizer.IsConnectionActive(connection.Name))
        {
          using (SitefinityOAContext context = ReplicationMessageSynchronizer.GetContext(connection))
          {
            context.AttachCopy<ConfigVarialble>(new ConfigVarialble()
            {
              Key = "timestamp",
              ApplicationName = nameof (ReplicationMessageSynchronizer),
              Value = ticks.ToString()
            });
            context.SaveChanges();
            flag = true;
          }
        }
      }
      return flag;
    }

    private static void ReadTimerElapsed(object sender, ElapsedEventArgs e)
    {
      try
      {
        ReplicationMessageSynchronizer.ReadDatabaseTimestamp();
      }
      catch (Exception ex)
      {
        Log.Write((object) string.Format("ReplicationMessageSynchronizer: Reading database timestamp failed with error: '{0}'.", (object) ex.Message), ConfigurationPolicy.ErrorLog);
      }
      ReplicationMessageSynchronizer.readTimer.Start();
    }

    private static void WriteTimerElapsed(object sender, ElapsedEventArgs e)
    {
      bool flag = false;
      try
      {
        flag = ReplicationMessageSynchronizer.WriteDatabaseTimestamp();
      }
      catch (LockNotGrantedException ex)
      {
        flag = true;
        Log.Write((object) "ReplicationMessageSynchronizer: Could not update database due to lock timeout.", ConfigurationPolicy.Trace);
      }
      catch (Exception ex)
      {
        Log.Write((object) string.Format("ReplicationMessageSynchronizer: Writing database timestamp failed with error: '{0}'.", (object) ex.Message), ConfigurationPolicy.ErrorLog);
      }
      if (!flag)
        return;
      ReplicationMessageSynchronizer.writeTimer.Start();
    }

    private static void InitializeTimestamp(OpenAccessConnection connection)
    {
      using (SitefinityOAContext context = ReplicationMessageSynchronizer.GetContext(connection))
      {
        if (context.GetAll<ConfigVarialble>().Where<ConfigVarialble>((Expression<Func<ConfigVarialble, bool>>) (c => c.Key == "timestamp" && c.ApplicationName == nameof (ReplicationMessageSynchronizer))).FirstOrDefault<ConfigVarialble>() != null)
          return;
        context.Add((object) new ConfigVarialble()
        {
          Key = "timestamp",
          ApplicationName = nameof (ReplicationMessageSynchronizer),
          Value = DateTime.UtcNow.Ticks.ToString()
        });
        context.SaveChanges();
      }
    }

    private static void StartReadLoop(ReplicationSyncConfigElement settings)
    {
      ReplicationMessageSynchronizer.readTimer = new Timer();
      ReplicationMessageSynchronizer.readTimer.Interval = (double) ((settings.TimestampReadIntervalInSeconds > 0 ? settings.TimestampReadIntervalInSeconds : 3) * 1000);
      ReplicationMessageSynchronizer.readTimer.Elapsed += new ElapsedEventHandler(ReplicationMessageSynchronizer.ReadTimerElapsed);
      ReplicationMessageSynchronizer.readTimer.AutoReset = false;
      ReplicationMessageSynchronizer.readTimer.Start();
      Log.Write((object) "ReplicationMessageSynchronizer: Started read loop.", ConfigurationPolicy.Trace);
    }

    private static void StartWriteLoop(ReplicationSyncConfigElement settings)
    {
      ReplicationMessageSynchronizer.inactivityThreshold = TimeSpan.FromSeconds(settings.WriteInactivityThreshold > 0 ? (double) settings.WriteInactivityThreshold : 300.0);
      ReplicationMessageSynchronizer.writeTimer = new Timer();
      ReplicationMessageSynchronizer.writeTimer.Interval = (double) ((settings.TimestampWriteIntervalInSeconds > 0 ? settings.TimestampWriteIntervalInSeconds : 2) * 1000);
      ReplicationMessageSynchronizer.writeTimer.Elapsed += new ElapsedEventHandler(ReplicationMessageSynchronizer.WriteTimerElapsed);
      ReplicationMessageSynchronizer.writeTimer.AutoReset = false;
      ReplicationMessageSynchronizer.writeTimer.Start();
      Log.Write((object) "ReplicationMessageSynchronizer: Started write loop.", ConfigurationPolicy.Trace);
    }

    private static void OnMessageSending(SystemMessageEventArgs args)
    {
      if (!ReplicationMessageSynchronizer.masterConnections.Contains(args.Message.Connection))
        return;
      foreach (IReplicationMessageTransporter transporter in ReplicationMessageSynchronizer.transporters)
        transporter.Send(args.Message);
    }

    private static void StartSystemMessageListening()
    {
      SystemMessageDispatcher.MessageSending -= new SystemMessageEventHandler(ReplicationMessageSynchronizer.OnMessageSending);
      SystemMessageDispatcher.MessageSending += new SystemMessageEventHandler(ReplicationMessageSynchronizer.OnMessageSending);
    }

    private static void HandleMessages(params SystemMessageBase[] messages)
    {
      using (StreamWriter writer = new StreamWriter(Stream.Null))
      {
        HttpContext.Current = new HttpContext(new HttpRequest(string.Empty, SystemManager.AbsolutePathRootUrlOfFirstRequest, string.Empty), new HttpResponse((TextWriter) writer));
        SystemMessageDispatcher.HandleSystemMessages(messages);
      }
    }

    private static void OnMessageReceived(
      IReplicationMessageTransporter transporter,
      SystemMessageBase msg)
    {
      if (!ReplicationMessageSynchronizer.slaveConnections.Contains(msg.Connection))
        return;
      if (ReplicationMessageSynchronizer.GetConnectionTimestamp(msg.Connection) < msg.Timestamp)
      {
        ReplicationMessageSynchronizer.unprocessedMessages.Push(msg);
        ReplicationMessageSynchronizer.processTimer.Start();
      }
      else
        ReplicationMessageSynchronizer.HandleMessages(msg);
    }

    private static void StartMessageReceiving()
    {
      foreach (IReplicationMessageTransporter transporter in ReplicationMessageSynchronizer.transporters)
        transporter.Subscribe(new Action<IReplicationMessageTransporter, SystemMessageBase>(ReplicationMessageSynchronizer.OnMessageReceived));
    }

    private static void StartMessageProcessingLoop(ReplicationSyncConfigElement settings)
    {
      ReplicationMessageSynchronizer.unprocessedLimit = settings.MaxUnprocessedMessages;
      ReplicationMessageSynchronizer.messageAgeLimit = settings.MaxMessageAgeInSeconds;
      ReplicationMessageSynchronizer.processTimer = new Timer();
      ReplicationMessageSynchronizer.processTimer.AutoReset = false;
      ReplicationMessageSynchronizer.processTimer.Interval = (double) ((settings.MessageRetryIntervalInSeconds > 0 ? settings.MessageRetryIntervalInSeconds : 2) * 1000);
      ReplicationMessageSynchronizer.processTimer.Elapsed += new ElapsedEventHandler(ReplicationMessageSynchronizer.OnProcessTimerElapsed);
    }

    private static void StopTimer(Timer timer) => timer?.Stop();

    private static void Cleanup()
    {
      ReplicationMessageSynchronizer.StopTimer(ReplicationMessageSynchronizer.readTimer);
      ReplicationMessageSynchronizer.StopTimer(ReplicationMessageSynchronizer.writeTimer);
      ReplicationMessageSynchronizer.StopTimer(ReplicationMessageSynchronizer.processTimer);
      ReplicationMessageSynchronizer.masterConnections.Clear();
      ReplicationMessageSynchronizer.slaveConnections.Clear();
      ReplicationMessageSynchronizer.unprocessedMessages.Clear();
      foreach (IDisposable transporter in ReplicationMessageSynchronizer.transporters)
        transporter.Dispose();
      ReplicationMessageSynchronizer.transporters.Clear();
    }

    private static void InitializeTransporters(ReplicationSyncConfigElement settings)
    {
      foreach (ReplicationTransporterConfigElement transporter in settings.Transporters)
      {
        Type type = TypeResolutionService.ResolveType(transporter.TypeName, false);
        if (!(type == (Type) null))
        {
          IReplicationMessageTransporter instance = (IReplicationMessageTransporter) Activator.CreateInstance(type);
          if (instance.Activate(transporter.Parameters))
            ReplicationMessageSynchronizer.transporters.Add(instance);
        }
      }
    }

    private static void InitializeLoops(ReplicationSyncConfigElement settings)
    {
      foreach (OpenAccessConnection connection in OpenAccessConnection.GetConnections())
      {
        if (connection.Replication == OpenAccessConnection.ReplicationMode.Slave)
        {
          ReplicationMessageSynchronizer.connectionTimestamps[connection.Name] = 0L;
          ReplicationMessageSynchronizer.slaveConnections.Add(connection.Name);
          Log.Write((object) string.Format("ReplicationMessageSynchronizer: Registered slave connection with name: '{0}'.", (object) connection.Name), ConfigurationPolicy.Trace);
        }
        else if (connection.Replication == OpenAccessConnection.ReplicationMode.Master)
        {
          ReplicationMessageSynchronizer.InitializeTimestamp(connection);
          ReplicationMessageSynchronizer.connectionUpdates[connection.Name] = DateTime.UtcNow;
          ReplicationMessageSynchronizer.masterConnections.Add(connection.Name);
          Log.Write((object) string.Format("ReplicationMessageSynchronizer: Registered master connection with name: '{0}'.", (object) connection.Name), ConfigurationPolicy.Trace);
        }
      }
      if (ReplicationMessageSynchronizer.slaveConnections.Count > 0)
      {
        ReplicationMessageSynchronizer.StartReadLoop(settings);
        ReplicationMessageSynchronizer.StartMessageProcessingLoop(settings);
        ReplicationMessageSynchronizer.StartMessageReceiving();
      }
      if (ReplicationMessageSynchronizer.masterConnections.Count <= 0)
        return;
      ReplicationMessageSynchronizer.StartWriteLoop(settings);
      ReplicationMessageSynchronizer.StartSystemMessageListening();
    }

    private static SitefinityOAContext GetContext(OpenAccessConnection connection)
    {
      OpenAccessXmlConfigStorageProvider databaseStorageProvider = ConfigManager.GetManager().Provider.GetDatabaseStorageProvider() as OpenAccessXmlConfigStorageProvider;
      return connection.GetContext((IOpenAccessMetadataProvider) databaseStorageProvider);
    }

    internal static void Initialize()
    {
      ReplicationMessageSynchronizer.Cleanup();
      ReplicationSyncConfigElement replicationSyncSettings = Config.Get<SystemConfig>().LoadBalancingConfig.ReplicationSyncSettings;
      if (!replicationSyncSettings.Enabled)
        return;
      if (!LicenseState.Current.LicenseInfo.CheckIsModuleLicensed("81346463-63E8-41E0-894F-A34955B377CC"))
      {
        Log.Write((object) "No Multi-regional deployment license. Replication messages will not be synchronized.", ConfigurationPolicy.Trace);
      }
      else
      {
        ReplicationMessageSynchronizer.InitializeTransporters(replicationSyncSettings);
        ReplicationMessageSynchronizer.InitializeLoops(replicationSyncSettings);
      }
    }

    /// <summary>Gets a cached connection timestamp.</summary>
    /// <param name="connectionName">The name of the connection.</param>
    /// <returns>The connection timestamp.</returns>
    public static long GetConnectionTimestamp(string connectionName)
    {
      long num;
      return connectionName != null && ReplicationMessageSynchronizer.connectionTimestamps.TryGetValue(connectionName, out num) ? num : long.MaxValue;
    }

    /// <summary>
    /// Update cached connection timestamp. The value in the database is not changed.
    /// </summary>
    /// <param name="connectionName">The name of the connection.</param>
    /// <param name="timestamp">The new timestamp value.</param>
    public static void UpdateConnectionTimestamp(string connectionName, long timestamp) => ReplicationMessageSynchronizer.connectionTimestamps[connectionName] = timestamp;

    /// <summary>
    /// Schedule timestamp update in the database. The update will happen after preconfigured time interval.
    /// </summary>
    /// <param name="connectionName">Name of the connection.</param>
    public static void RegisterDatabaseUpdate(string connectionName)
    {
      ReplicationMessageSynchronizer.connectionUpdates[connectionName] = DateTime.UtcNow;
      if (SystemManager.Initializing || !ReplicationMessageSynchronizer.masterConnections.Contains(connectionName))
        return;
      ReplicationMessageSynchronizer.writeTimer.Start();
    }
  }
}
