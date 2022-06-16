// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.LoadBalancing.Replication.RedisMessageTransporter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using ServiceStack.Redis;
using ServiceStack.Text;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Telerik.Sitefinity.LoadBalancing.Replication
{
  /// <summary>
  /// Redis transporter for replication related system messages.
  /// </summary>
  public class RedisMessageTransporter : IReplicationMessageTransporter, IDisposable
  {
    /// <summary>Name of the connection string key.</summary>
    public const string ConnectionStringKeyName = "ConnectionString";
    /// <summary>Name of the prefix key.</summary>
    public const string PrefixKeyName = "Prefix";
    /// <summary>Default value of prefix property.</summary>
    public const string DefaultPrefixValue = "sf-";
    private const string KeyName = "replication-messages";
    private RedisManagerPool managerPool;
    private RedisPubSubServer pubSub;
    private string channel;
    private List<Action<IReplicationMessageTransporter, SystemMessageBase>> handlers;

    private void OnRedisMessage(string channel, string msg)
    {
      SystemMessageBase systemMessageBase = TypeSerializer.DeserializeFromString<SystemMessageBase>(msg);
      foreach (Action<IReplicationMessageTransporter, SystemMessageBase> handler in this.handlers)
        handler((IReplicationMessageTransporter) this, systemMessageBase);
    }

    /// <inheritdoc />
    public bool Activate(NameValueCollection parameters)
    {
      if (parameters == null || !parameters.Keys.Contains("ConnectionString") || !parameters.Keys.Contains("Prefix"))
        return false;
      string parameter1 = parameters["ConnectionString"];
      if (parameter1.IsNullOrWhitespace())
        return false;
      string parameter2 = parameters["Prefix"];
      this.managerPool = new RedisManagerPool(parameter1);
      this.channel = parameter2 + "replication-messages";
      return true;
    }

    /// <inheritdoc />
    public void Send(SystemMessageBase message)
    {
      using (IRedisClient client = this.managerPool.GetClient())
      {
        SystemMessageBase systemMessageBase = new SystemMessageBase()
        {
          Key = message.Key,
          MessageData = message.MessageData,
          SenderId = message.SenderId,
          Timestamp = message.Timestamp,
          Connection = message.Connection
        };
        Dictionary<string, string> dictionary = message.AdditionalInfo != null ? new Dictionary<string, string>((IDictionary<string, string>) message.AdditionalInfo) : new Dictionary<string, string>();
        dictionary.Add("SentFromDifferentRegion", "SentFromDifferentRegion");
        systemMessageBase.AdditionalInfo = dictionary;
        client.PublishMessage(this.channel, TypeSerializer.SerializeToString<SystemMessageBase>(systemMessageBase));
      }
    }

    /// <inheritdoc />
    public void Subscribe(
      Action<IReplicationMessageTransporter, SystemMessageBase> hander)
    {
      if (hander == null)
        return;
      if (this.pubSub == null)
      {
        this.handlers = new List<Action<IReplicationMessageTransporter, SystemMessageBase>>();
        this.pubSub = new RedisPubSubServer((IRedisClientsManager) this.managerPool, new string[1]
        {
          this.channel
        })
        {
          OnMessage = new Action<string, string>(this.OnRedisMessage)
        };
        this.pubSub.Start();
      }
      this.handlers.Add(hander);
    }

    /// <inheritdoc />
    public void Dispose()
    {
      if (this.pubSub != null)
      {
        this.pubSub.Stop();
        this.pubSub.Dispose();
      }
      if (this.managerPool != null)
        this.managerPool.Dispose();
      this.handlers.Clear();
    }
  }
}
