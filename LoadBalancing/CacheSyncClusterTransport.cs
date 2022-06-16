// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.LoadBalancing.CacheSyncClusterTransport
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Telerik.OpenAccess.Cluster;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.LoadBalancing
{
  /// <summary>
  /// Synchronizes the Open Access level 2 caches between load balanced nodes using <see cref="T:Telerik.Sitefinity.LoadBalancing.SystemMessageDispatcher" />
  /// for sending invalidation messages. Open Access will instantiate only one instance of this class.
  /// </summary>
  public class CacheSyncClusterTransport : OpenAccessClusterTransport
  {
    /// <summary>A list of active caches</summary>
    internal static readonly IList<CacheSyncClusterTransport> CurrentlyActiveCaches = (IList<CacheSyncClusterTransport>) new List<CacheSyncClusterTransport>();
    private Lazy<CacheSyncClusterTransport.IMessageSender> sender;
    private OpenAccessClusterMsgHandler messageHandler;
    internal const string LocalPathKey = "LocalPath";
    private static readonly ReaderWriterLockSlim ReadWriteLock = new ReaderWriterLockSlim();

    /// <summary>
    /// Handles the message with the corresponding cache handler
    /// </summary>
    /// <param name="message">The message.</param>
    public static void HandleMessage(SystemMessageBase message)
    {
      string str = (string) null;
      if (message.MessageData.IsNullOrEmpty() || !message.AdditionalInfo.TryGetValue("LocalPath", out str))
        return;
      CacheSyncClusterTransport.ReadWriteLock.EnterReadLock();
      try
      {
        foreach (CacheSyncClusterTransport currentlyActiveCach in (IEnumerable<CacheSyncClusterTransport>) CacheSyncClusterTransport.CurrentlyActiveCaches)
        {
          if (currentlyActiveCach.Localpath.Equals(str))
            currentlyActiveCach.ReceiveMessage(Convert.FromBase64String(message.MessageData));
        }
      }
      finally
      {
        CacheSyncClusterTransport.ReadWriteLock.ExitReadLock();
      }
    }

    /// <summary>
    /// Gets the maximum message size that is allowed for this transport implementation.
    /// </summary>
    /// <value>Maximum message size in bytes</value>
    /// <remarks>
    /// When a logical eviction message is bigger in size than the allowed message size, an EvictAll will be sent instead.
    /// The current value is set as 60 000 bytes so that when base 64 encoded the message will still be below the 85kb
    /// Large Object Heap limit.
    /// </remarks>
    public virtual int MaxMessageSize => 60000;

    /// <summary>
    /// Gets or sets the name of the connection this transport implementation is created for
    /// </summary>
    public string Localpath { get; set; }

    /// <summary>
    /// Called by Open Access to dispose any unmanaged resources.
    /// This object doesn't have unmanaged resources to release.
    /// </summary>
    /// <remarks>
    /// This is called when the Telerik Data Access process is closed (i.e. the
    /// <see cref="T:Telerik.OpenAccess.Database" /> is closed).
    /// </remarks>
    public void Close()
    {
      CacheSyncClusterTransport.ReadWriteLock.EnterWriteLock();
      try
      {
        CacheSyncClusterTransport.CurrentlyActiveCaches.Remove(this);
      }
      finally
      {
        CacheSyncClusterTransport.ReadWriteLock.ExitWriteLock();
      }
    }

    /// <summary>
    /// This method is called by OpenAccess to initialize a single instance of this type to synchronize the Level 2 cache between nodes.
    /// Registers this instance of <see cref="T:Telerik.Sitefinity.LoadBalancing.CacheSyncClusterTransport" /> in the ObjectFactory as singleton for later resolutions.
    /// This is done so that external components can acquire this instance and call its ReceiveMessage method.
    /// </summary>
    /// <param name="msgHandler">The message handler instance user do receive messages.</param>
    /// <param name="serverName">The name of this OpenAccess instance.</param>
    /// <param name="identifier">The name to use for reporting identification.</param>
    /// <param name="log">Logging Instance</param>
    public void Init(
      OpenAccessClusterMsgHandler msgHandler,
      string serverName,
      string identifier,
      IOpenAccessClusterTransportLog log)
    {
      this.messageHandler = msgHandler;
      if (this.Localpath.IsNullOrEmpty())
        this.Localpath = this.CreateCacheLocalpathAlternative(msgHandler, serverName, identifier, log);
      this.sender = new Lazy<CacheSyncClusterTransport.IMessageSender>(new Func<CacheSyncClusterTransport.IMessageSender>(this.GetSender));
      CacheSyncClusterTransport.ReadWriteLock.EnterWriteLock();
      try
      {
        CacheSyncClusterTransport.CurrentlyActiveCaches.Add(this);
      }
      finally
      {
        CacheSyncClusterTransport.ReadWriteLock.ExitWriteLock();
      }
    }

    /// <summary>Called by OpenAccess when cache eviction occurs.</summary>
    /// <param name="buffer">The message that must be send to the other nodes.</param>
    /// <seealso cref="M:Telerik.OpenAccess.Cluster.OpenAccessClusterMsgHandler.HandleMessage(System.IO.Stream)" />
    public void SendMessage(byte[] buffer) => this.Sender.SendMessage(buffer);

    /// <summary>Receive cache invalidation message.</summary>
    /// <param name="data">The data sent by other nodes from the setup.</param>
    /// <exception cref="T:System.Exception">messageHandler was not initialized. Thrown when the messageHandler instance is not initialized.</exception>
    public virtual void ReceiveMessage(byte[] data)
    {
      if (this.messageHandler == null)
        throw new Exception("messageHandler was not initialized. This could mean that Open Access did not initialize this instance of CacheSyncClusterTransport object prior to the ReceiveMessage call.");
      if (data == null)
        throw new ArgumentNullException(nameof (data), "Receive message called with byte array data being null.");
      Exception exception;
      using (MemoryStream memoryStream = new MemoryStream(data))
        exception = this.messageHandler.HandleMessage((Stream) memoryStream);
      if (exception == null)
        return;
      this.ReportException(data, exception);
    }

    /// <summary>Reports the exception in the error log.</summary>
    /// <param name="data">The data.</param>
    /// <param name="exception">The exception.</param>
    protected void ReportException(byte[] data, Exception exception)
    {
      Exception exceptionToHandle = this.WrapException(data, exception);
      if (Exceptions.HandleException(exceptionToHandle, ExceptionPolicyName.IgnoreExceptions))
        throw exceptionToHandle;
    }

    private CacheSyncClusterTransport.IMessageSender Sender => this.sender.Value;

    /// <summary>
    /// Called to create an alternative Local path value, as is not provided from the OpenAccess back end configuration.
    /// </summary>
    /// <param name="msgHandler">The message handler instance user do receive messages.</param>
    /// <param name="serverName">The name of this OpenAccess instance.</param>
    /// <param name="identifier">The name to use for reporting identification.</param>
    /// <param name="log">Logging Instance</param>
    /// <returns>database cache identifier used to recognize the cache instance on another process</returns>
    private string CreateCacheLocalpathAlternative(
      OpenAccessClusterMsgHandler msgHandler,
      string serverName,
      string identifier,
      IOpenAccessClusterTransportLog log)
    {
      if (log == null)
        return string.Empty;
      object obj = log.GetType().GetProperty("Config").GetValue((object) log, (object[]) null);
      return SecurityManager.ComputeHash(obj.GetType().GetField("url").GetValue(obj).ToString());
    }

    private Exception WrapException(byte[] data, Exception exception)
    {
      exception = new Exception("Exception occurred while handling an OpenAccess Level 2 Cache synchronization message. See inner exception for more details. The message data was: {0}".Arrange((object) Convert.ToBase64String(data)), exception);
      return exception;
    }

    private CacheSyncClusterTransport.IMessageSender GetSender() => SystemManager.IsInLoadBalancingMode ? (CacheSyncClusterTransport.IMessageSender) new CacheSyncClusterTransport.NlbMessageSender(this) : (CacheSyncClusterTransport.IMessageSender) new CacheSyncClusterTransport.DummyMessageSender();

    private interface IMessageSender
    {
      void SendMessage(byte[] buffer);
    }

    private class DummyMessageSender : CacheSyncClusterTransport.IMessageSender
    {
      public void SendMessage(byte[] buffer)
      {
      }
    }

    private class NlbMessageSender : CacheSyncClusterTransport.IMessageSender
    {
      private CacheSyncClusterTransport cacheSyncClusterTransport;

      public NlbMessageSender(CacheSyncClusterTransport cscTransport) => this.cacheSyncClusterTransport = cscTransport;

      public void SendMessage(byte[] buffer)
      {
        Level2CacheSyncMessage msg = new Level2CacheSyncMessage();
        msg.MessageData = Convert.ToBase64String(buffer);
        msg.AdditionalInfo.Add("LocalPath", this.cacheSyncClusterTransport.Localpath);
        msg.Connection = this.cacheSyncClusterTransport.Localpath;
        SystemMessageDispatcher.SendSystemMessage((SystemMessageBase) msg);
      }
    }
  }
}
