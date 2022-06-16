// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.LoadBalancingConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.LoadBalancing;
using Telerik.Sitefinity.LoadBalancing.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Services
{
  /// <summary>Load balancing configuration element.</summary>
  public class LoadBalancingConfig : ConfigElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.LoadBalancingConfig" /> class.
    /// </summary>
    public LoadBalancingConfig()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.LoadBalancingConfig" /> class.
    /// </summary>
    public LoadBalancingConfig(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.LoadBalancingConfig" /> class.
    /// </summary>
    /// <param name="check">if set to <c>true</c> [check].</param>
    internal LoadBalancingConfig(bool check)
      : base(check)
    {
    }

    /// <summary>
    /// Gets or sets the list of base URLS of webservers that are part from load balanced environment.
    /// </summary>
    /// <value>The URLS.</value>
    [ConfigurationProperty("parameters")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "URLsDescription", Title = "URLsCaption")]
    public ConfigElementList<InstanceUrlConfigElement> URLS
    {
      get => (ConfigElementList<InstanceUrlConfigElement>) this["parameters"];
      set => this["parameters"] = (object) value;
    }

    /// <summary>
    /// 
    /// </summary>
    [ConfigurationProperty("disableHostHeaders", DefaultValue = "true")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "LoadBalancingDisableHostHeadersDescription", Title = "LoadBalancingDisableHostHeadersTitle")]
    public bool DisableHostHeaders
    {
      get => (bool) this["disableHostHeaders"];
      set => this["disableHostHeaders"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the request timeout of the system messages (in milliseconds).
    /// </summary>
    /// <value>The value.</value>
    [ObjectInfo(typeof (ConfigDescriptions), Description = "WebServiceSenderRequestTimeoutDescription", Title = "WebServiceSenderRequestTimeoutTitle")]
    [ConfigurationProperty("requestTimeout")]
    public int RequestTimeout
    {
      get => (int) this["requestTimeout"];
      set => this["requestTimeout"] = (object) value;
    }

    /// <summary>Gets or sets the settings for MSMQ</summary>
    /// <value>The MSMQ settings.</value>
    [ConfigurationProperty("msmqSettings")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "MsmqSettingsDescription", Title = "MsmqSettingsCaption")]
    public MsmqConfigElement MsmqSettings
    {
      get => (MsmqConfigElement) this["msmqSettings"];
      set => this["msmqSettings"] = (object) value;
    }

    /// <summary>Gets or sets the settings for Redis</summary>
    /// <value>The Redis settings.</value>
    [ConfigurationProperty("redisSettings")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "RedisSettingsDescription", Title = "RedisSettingsCaption")]
    public RedisConfigElement RedisSettings
    {
      get => (RedisConfigElement) this["redisSettings"];
      set => this["redisSettings"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the settings for system message synchronization.
    /// </summary>
    /// <value>The system message sync settings.</value>
    [ConfigurationProperty("replicationSyncSettings")]
    [ObjectInfo(typeof (ReplicationSyncConfigDescriptions), Description = "ReplicationSyncSettingsDescription", Title = "ReplicationSyncSettingsTitle")]
    public ReplicationSyncConfigElement ReplicationSyncSettings
    {
      get => (ReplicationSyncConfigElement) this["replicationSyncSettings"];
      set => this["replicationSyncSettings"] = (object) value;
    }

    /// <summary>Gets or sets the senders.</summary>
    /// <value>The senders.</value>
    [ConfigurationProperty("senders")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "SendersDescription", Title = "SendersCaption")]
    public ConfigElementList<TypeNameConfigElement> Senders
    {
      get => (ConfigElementList<TypeNameConfigElement>) this["senders"];
      set => this["senders"] = (object) value;
    }

    /// <summary>Gets or sets the handlers.</summary>
    /// <value>The handlers.</value>
    [ConfigurationProperty("handlers")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "HandlersDescription", Title = "HandlersCaption")]
    public ConfigElementList<TypeNameConfigElement> Handlers
    {
      get => (ConfigElementList<TypeNameConfigElement>) this["handlers"];
      set => this["handlers"] = (object) value;
    }

    /// <summary>
    /// Adds a new <see cref="T:Telerik.Sitefinity.LoadBalancing.ISystemMessageHandler" /> implementation type to the <see cref="P:Telerik.Sitefinity.Services.LoadBalancingConfig.Handlers" /> collection.
    /// </summary>
    /// <typeparam name="TSystemMessageHanlder">An <see cref="T:Telerik.Sitefinity.LoadBalancing.ISystemMessageHandler" /> implementation.</typeparam>
    public void AddHandler<TSystemMessageHanlder>() where TSystemMessageHanlder : ISystemMessageHandler
    {
      string fullName = typeof (TSystemMessageHanlder).FullName;
      if (this.Handlers.Contains(fullName))
        return;
      this.Handlers.Add(new TypeNameConfigElement()
      {
        Value = fullName
      });
    }

    /// <summary>
    /// Adds a new <see cref="T:Telerik.Sitefinity.LoadBalancing.ISystemMessageSender" /> implementation type to the <see cref="P:Telerik.Sitefinity.Services.LoadBalancingConfig.Senders" /> collection.
    /// </summary>
    /// <typeparam name="TSystemMessageSender">An <see cref="T:Telerik.Sitefinity.LoadBalancing.ISystemMessageSender" /> implementation.</typeparam>
    public void AddSender<TSystemMessageSender>() where TSystemMessageSender : ISystemMessageSender
    {
      string fullName = typeof (TSystemMessageSender).FullName;
      if (this.Senders.Contains(fullName))
        return;
      this.Senders.Add(new TypeNameConfigElement()
      {
        Value = fullName
      });
    }

    /// <summary>
    /// Removes an existing <see cref="T:Telerik.Sitefinity.LoadBalancing.ISystemMessageHandler" /> implementation type to the <see cref="P:Telerik.Sitefinity.Services.LoadBalancingConfig.Handlers" /> collection.
    /// </summary>
    /// <typeparam name="TSystemMessageHanlder">An <see cref="T:Telerik.Sitefinity.LoadBalancing.ISystemMessageHandler" /> implementation.</typeparam>
    public void RemoveHandler<TSystemMessageHanlder>() where TSystemMessageHanlder : ISystemMessageHandler
    {
      ConfigElement elementByKey = this.Handlers.GetElementByKey(typeof (TSystemMessageHanlder).FullName);
      if (elementByKey == null)
        return;
      this.Handlers.Remove(elementByKey);
    }

    /// <summary>
    /// Removes an existing <see cref="T:Telerik.Sitefinity.LoadBalancing.ISystemMessageSender" /> implementation type to the <see cref="P:Telerik.Sitefinity.Services.LoadBalancingConfig.Senders" /> collection.
    /// </summary>
    /// <typeparam name="TSystemMessageSender">An <see cref="T:Telerik.Sitefinity.LoadBalancing.ISystemMessageSender" /> implementation.</typeparam>
    public void RemoveSender<TSystemMessageSender>() where TSystemMessageSender : ISystemMessageSender
    {
      ConfigElement elementByKey = this.Senders.GetElementByKey(typeof (TSystemMessageSender).FullName);
      if (elementByKey == null)
        return;
      this.Senders.Remove(elementByKey);
    }

    /// <summary>
    /// Constants for the names of the configuration properties
    /// </summary>
    internal static class Names
    {
      internal const string Urls = "parameters";
      internal const string DisableHostHeaders = "disableHostHeaders";
      internal const string WSSenderRequestTimeout = "requestTimeout";
      internal const string MsmqSettings = "msmqSettings";
      internal const string RedisSettings = "redisSettings";
      internal const string ReplicationSyncSettings = "replicationSyncSettings";
      internal const string Senders = "senders";
      internal const string Handlers = "handlers";
    }
  }
}
