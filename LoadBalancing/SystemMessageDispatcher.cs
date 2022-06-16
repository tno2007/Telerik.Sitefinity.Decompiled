// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.LoadBalancing.SystemMessageDispatcher
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Cloud.WindowsAzure;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Licensing;
using Telerik.Sitefinity.LoadBalancing.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.LoadBalancing
{
  /// <summary>Dispatch system messages related to load balancing.</summary>
  public class SystemMessageDispatcher
  {
    private static object LockType = new object();
    internal static int FlushMessagesTreshold = 0;
    internal const string SystemMessagesQueueKey = "SystemMessagesQueue";
    internal const string SystemMessagesSent = "Multiple system messages sent.";
    internal const string SystemMessagesReceived = "Multiple system messages received.";

    internal static IEnumerable<ISystemMessageHandler> Handlers
    {
      get
      {
        IEnumerable<ISystemMessageHandler> data = (IEnumerable<ISystemMessageHandler>) SystemManager.Cache.GetData(LoadBalancingConstants.HandlersCacheKey);
        if (data == null)
        {
          lock (SystemMessageDispatcher.LockType)
          {
            data = (IEnumerable<ISystemMessageHandler>) SystemManager.Cache.GetData(LoadBalancingConstants.HandlersCacheKey);
            if (data == null)
            {
              data = SystemMessageDispatcher.Create<ISystemMessageHandler>(Config.Get<SystemConfig>().LoadBalancingConfig.Handlers);
              SystemManager.Cache.Add(LoadBalancingConstants.HandlersCacheKey, (object) data);
            }
          }
        }
        return data;
      }
    }

    internal static IEnumerable<ISystemMessageSender> Senders
    {
      get
      {
        IEnumerable<ISystemMessageSender> data = (IEnumerable<ISystemMessageSender>) SystemManager.Cache.GetData(LoadBalancingConstants.SendersCacheKey);
        if (data == null)
        {
          lock (SystemMessageDispatcher.LockType)
          {
            data = (IEnumerable<ISystemMessageSender>) SystemManager.Cache.GetData(LoadBalancingConstants.SendersCacheKey);
            if (data == null)
            {
              data = SystemMessageDispatcher.Create<ISystemMessageSender>(Config.Get<SystemConfig>().LoadBalancingConfig.Senders);
              SystemManager.Cache.Add(LoadBalancingConstants.SendersCacheKey, (object) data);
            }
          }
        }
        return data;
      }
    }

    internal static void InvalidateHandlers() => SystemManager.Cache.Add(LoadBalancingConstants.HandlersCacheKey, (object) null);

    internal static void InvalidateSenders() => SystemManager.Cache.Add(LoadBalancingConstants.SendersCacheKey, (object) null);

    private static IEnumerable<T> Create<T>(
      ConfigElementList<TypeNameConfigElement> types)
    {
      List<T> objList = new List<T>();
      foreach (TypeNameConfigElement type1 in types)
      {
        Type type2 = TypeResolutionService.ResolveType(type1.Value, false);
        if (type2 != (Type) null)
        {
          T instance = (T) Activator.CreateInstance(type2);
          objList.Add(instance);
        }
      }
      return (IEnumerable<T>) objList;
    }

    internal static bool SystemMessagingRequiresAuthentication => (uint) (Config.SectionHandler.Environment.Flags & SitefinityEnvironmentFlags.AuthenticatedLoadBalancingNotifications) > 0U;

    /// <summary>
    /// Fires before a system synchronization message is sent.
    /// </summary>
    public static event SystemMessageEventHandler MessageSending;

    /// <summary>Fires after a system synchronization message is sent.</summary>
    public static event SystemMessageEventHandler MessageSent;

    /// <summary>
    /// Fires when a system synchronization message is received and before it is handled.
    /// </summary>
    public static event SystemMessageEventHandler MessageReceived;

    /// <summary>
    /// Fires when a system synchronization message is received and is already handled.
    /// </summary>
    public static event SystemMessageEventHandler MessageHandled;

    private static void FireEvent(SystemMessageEventHandler @event, SystemMessageBase message)
    {
      if (@event == null)
        return;
      try
      {
        @event(new SystemMessageEventArgs(message));
      }
      catch (Exception ex)
      {
        if (!Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
          return;
        throw;
      }
    }

    /// <summary>Handles the system message.</summary>
    /// <param name="msg">The MSG.</param>
    public static void HandleSystemMessage(SystemMessageBase msg)
    {
      if (!Bootstrapper.IsNLBComunicationSetup || SystemMessageDispatcher.Handlers.Count<ISystemMessageHandler>() == 0 || !LicenseLimitations.CanUseLoadBalancing(false))
        return;
      SystemMessageDispatcher.LogTestingMessage(msg, "NLB/HANDLING");
      SystemMessageDispatcher.HandleSystemMessageInternal(msg);
    }

    /// <summary>Handles the system messages.</summary>
    /// <param name="msg">The system messages to be handled.</param>
    public static void HandleSystemMessages(SystemMessageBase[] msgs)
    {
      if (!Bootstrapper.IsNLBComunicationSetup || SystemMessageDispatcher.Handlers.Count<ISystemMessageHandler>() == 0 || !LicenseLimitations.CanUseLoadBalancing(false))
        return;
      foreach (SystemMessageBase msg in msgs)
        SystemMessageDispatcher.HandleSystemMessageInternal(msg);
      SystemMessageDispatcher.LogSystemMessages((IEnumerable<SystemMessageBase>) msgs, "Multiple system messages received.");
    }

    internal static void HandleSystemMessageInternal(SystemMessageBase msg)
    {
      SystemMessageDispatcher.FireEvent(SystemMessageDispatcher.MessageReceived, msg);
      foreach (ISystemMessageHandler handler in SystemMessageDispatcher.Handlers)
      {
        if (handler.CanProcessSystemMessage(msg))
        {
          try
          {
            handler.ProcessSystemMessage(msg);
          }
          catch (Exception ex)
          {
            if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
              throw ex;
          }
        }
      }
      SystemMessageDispatcher.FireEvent(SystemMessageDispatcher.MessageHandled, msg);
    }

    /// <summary>Sends the system message.</summary>
    /// <param name="msg">The system message to be sent.</param>
    public static void SendSystemMessage(SystemMessageBase msg)
    {
      if (!Bootstrapper.IsNLBComunicationSetup || !SystemManager.IsInLoadBalancingMode || !LicenseLimitations.CanUseLoadBalancing(false))
        return;
      SystemMessageDispatcher.FireEvent(SystemMessageDispatcher.MessageSending, msg);
      foreach (ISystemMessageSender sender in SystemMessageDispatcher.Senders)
      {
        if (sender.IsActive)
        {
          try
          {
            sender.SendSystemMessage(msg);
          }
          catch (Exception ex)
          {
            if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
              throw ex;
          }
        }
      }
      SystemMessageDispatcher.LogTestingMessage(msg, "NLB/SENT");
      SystemMessageDispatcher.FireEvent(SystemMessageDispatcher.MessageSent, msg);
    }

    /// <summary>Queues the system message.</summary>
    /// <param name="msg">The system message to be queued.</param>
    public static void QueueSystemMessage(SystemMessageBase msg) => SystemMessageDispatcher.SendSystemMessage(msg);

    /// <summary>Flushes the system messages.</summary>
    public static void FlushSystemMessages()
    {
      if (!(SystemManager.CurrentHttpContext.Items[(object) "SystemMessagesQueue"] is Queue<SystemMessageBase> systemMessageBaseQueue) || systemMessageBaseQueue.Count == 0 || !Bootstrapper.IsNLBComunicationSetup || !SystemManager.IsInLoadBalancingMode || !LicenseState.Current.LicenseInfo.SupportLoadBalancing)
        return;
      SystemMessageBase[] array = systemMessageBaseQueue.ToArray();
      foreach (ISystemMessageSender sender in SystemMessageDispatcher.Senders)
      {
        if (sender.IsActive)
        {
          try
          {
            sender.SendSystemMessages(array);
          }
          catch (Exception ex)
          {
            if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
              throw ex;
          }
        }
      }
      SystemMessageDispatcher.LogSystemMessages((IEnumerable<SystemMessageBase>) array, "Multiple system messages sent.");
    }

    private static void CheckFlushTreshold(Queue<SystemMessageBase> queue)
    {
      int messagesTreshold = SystemMessageDispatcher.FlushMessagesTreshold;
      if (messagesTreshold <= 0 || messagesTreshold >= queue.Count)
        return;
      SystemMessageDispatcher.FlushSystemMessages();
      queue.Clear();
    }

    internal static bool TestLoggingEnabled
    {
      get
      {
        SitefinityTestingElement testing = Config.SectionHandler.Testing;
        return testing.Enabled && testing.LoadBalancingSyncLoggingEnabled;
      }
    }

    /// <summary>
    /// Logs a load balancing related message,
    /// if <c>configuration/telerik/sitefinity/testing/@loadBalancingSyncLoggingEnabled</c> is <c>true</c>;
    /// does nothing otherwise.
    /// </summary>
    /// <param name="message">The message to be logged.</param>
    public static void LogTestingMessage(string message, string action = null)
    {
      if (!SystemMessageDispatcher.TestLoggingEnabled)
        return;
      if (!AzureRuntime.IsRunning)
      {
        if (action != null)
          message = action + ": " + message;
        Telerik.Sitefinity.Abstractions.Log.Write((object) message);
      }
      else if (action != null)
        Trace.WriteLine(message, action);
      else
        Trace.WriteLine(message);
    }

    internal static void LogTestingMessage(SystemMessageBase message, string action = null) => SystemMessageDispatcher.LogTestingMessage(message.Key + ":\n" + message.MessageData, action);

    private static void LogSystemMessages(IEnumerable<SystemMessageBase> msgs, string message)
    {
      if (!SystemMessageDispatcher.TestLoggingEnabled)
        return;
      foreach (SystemMessageBase msg in msgs)
        SystemMessageDispatcher.LogTestingMessage(msg);
      Telerik.Sitefinity.Abstractions.Log.Write((object) message);
    }
  }
}
