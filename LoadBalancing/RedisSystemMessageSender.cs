// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.LoadBalancing.RedisSystemMessageSender
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using ServiceStack.Redis;
using ServiceStack.Text;
using System;
using System.IO;
using System.Web;
using Telerik.Sitefinity.LoadBalancing.Web.Services;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.LoadBalancing
{
  /// <summary>A class for sending system messages.</summary>
  public class RedisSystemMessageSender : ISystemMessageSender
  {
    private static string channel = Telerik.Sitefinity.Configuration.Config.Get<SystemConfig>().LoadBalancingConfig.RedisSettings.KeyPrefix + "system-messages";

    /// <summary>Starts listening on pub/sub channel.</summary>
    public static void StartRedisReceiver()
    {
      if (!RedisSystemMessageSender.IsActive)
        return;
      if (RedisSystemMessageSender.RedisClientsManager != null)
      {
        RedisSystemMessageSender.RedisClientsManager.Stop();
        RedisSystemMessageSender.RedisClientsManager.Dispose();
      }
      if (RedisSystemMessageSender.ManagerPool != null)
        RedisSystemMessageSender.ManagerPool.Dispose();
      RedisSystemMessageSender.ManagerPool = (IRedisClientsManager) new RedisManagerPool(RedisSystemMessageSender.ConnectionString);
      RedisSystemMessageSender.RedisClientsManager = new RedisPubSubServer(RedisSystemMessageSender.ManagerPool, new string[1]
      {
        RedisSystemMessageSender.channel
      })
      {
        OnMessage = new Action<string, string>(RedisSystemMessageSender.OnRedisMessage),
        OnError = new Action<Exception>(RedisSystemMessageSender.OnError)
      };
      RedisSystemMessageSender.RedisClientsManager.Start();
    }

    /// <summary>
    /// Gets a value indicating whether <see cref="T:Telerik.Sitefinity.LoadBalancing.RedisSystemMessageSender" /> is active.
    /// </summary>
    bool ISystemMessageSender.IsActive => RedisSystemMessageSender.IsActive;

    /// <summary>Sends system message.</summary>
    /// <param name="msg">The system message object.</param>
    public void SendSystemMessage(SystemMessageBase msg) => this.SendSystemMessages(new SystemMessageBase[1]
    {
      msg
    });

    /// <summary>Sends an array of system messages.</summary>
    /// <param name="msgs">The messages to send.</param>
    public void SendSystemMessages(SystemMessageBase[] msgs)
    {
      if (!RedisSystemMessageSender.IsActive || RedisSystemMessageSender.ManagerPool == null)
        return;
      RedisSystemMessageSender.TryReinitializeRedisManager();
      using (IRedisClient client = RedisSystemMessageSender.ManagerPool.GetClient())
      {
        int length = msgs.Length;
        SystemMessageBase[] systemMessageBaseArray = new SystemMessageBase[length];
        for (int index = 0; index < length; ++index)
        {
          SystemMessageBase msg = msgs[index];
          SystemMessageBase systemMessageBase = new SystemMessageBase()
          {
            Key = msg.Key,
            MessageData = msg.MessageData,
            SenderId = SystemWebService.LocalId,
            AdditionalInfo = msg.AdditionalInfo,
            Timestamp = msg.Timestamp,
            Connection = msg.Connection
          };
          systemMessageBaseArray[index] = systemMessageBase;
        }
        client.PublishMessage(RedisSystemMessageSender.channel, TypeSerializer.SerializeToString<SystemMessageBase[]>(systemMessageBaseArray));
      }
    }

    /// <summary>
    /// Decides whether to handle or ignore a system messages.
    /// </summary>
    /// <param name="message">The system messages.</param>
    /// <returns>True if the message should be handled by the current node otherwise false.</returns>
    private static bool ShouldHandleMessage(SystemMessageBase message) => message.SenderId != SystemWebService.LocalId || message.Key == "sf_RedisCheckMessage";

    private static void TryReinitializeRedisManager()
    {
      if (!(RedisSystemMessageSender.RedisClientsManager.GetStatus() == "Stopping"))
        return;
      RedisSystemMessageSender.StartRedisReceiver();
    }

    private static void OnError(Exception ex) => RedisSystemMessageSender.TryReinitializeRedisManager();

    private static void OnRedisMessage(string channel, string msg)
    {
      using (StreamWriter writer = new StreamWriter(Stream.Null))
      {
        HttpContext.Current = new HttpContext(new HttpRequest(string.Empty, SystemManager.AbsolutePathRootUrlOfFirstRequest, string.Empty), new HttpResponse((TextWriter) writer));
        if (msg[0] == '[')
        {
          SystemMessageBase[] systemMessageBaseArray = TypeSerializer.DeserializeFromString<SystemMessageBase[]>(msg);
          if (systemMessageBaseArray.Length == 0)
            return;
          foreach (SystemMessageBase systemMessageBase in systemMessageBaseArray)
          {
            if (RedisSystemMessageSender.ShouldHandleMessage(systemMessageBase))
              SystemMessageDispatcher.HandleSystemMessage(systemMessageBase);
          }
        }
        else
        {
          SystemMessageBase systemMessageBase = TypeSerializer.DeserializeFromString<SystemMessageBase>(msg);
          if (!RedisSystemMessageSender.ShouldHandleMessage(systemMessageBase))
            return;
          SystemMessageDispatcher.HandleSystemMessage(systemMessageBase);
        }
      }
    }

    internal static bool IsActive => !RedisSystemMessageSender.ConnectionString.IsNullOrEmpty();

    internal static string ConnectionString => Telerik.Sitefinity.Configuration.Config.Get<SystemConfig>().LoadBalancingConfig.RedisSettings.ConnectionString;

    internal static RedisPubSubServer RedisClientsManager { get; set; }

    private static IRedisClientsManager ManagerPool { get; set; }
  }
}
