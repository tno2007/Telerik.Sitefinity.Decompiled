// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.LoadBalancing.Configuration.ReplicationSyncConfigElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.LoadBalancing.Replication;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.LoadBalancing.Configuration
{
  /// <summary>Represents Redis configuration element.</summary>
  [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
  public class ReplicationSyncConfigElement : ConfigElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.LoadBalancing.Configuration.ReplicationSyncConfigElement" /> class.
    /// </summary>
    /// <param name="parent">Parent configuration element.</param>
    public ReplicationSyncConfigElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.LoadBalancing.Configuration.ReplicationSyncConfigElement" /> class.
    /// </summary>
    internal ReplicationSyncConfigElement()
      : base(false)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.LoadBalancing.Configuration.ReplicationSyncConfigElement" /> class.
    /// </summary>
    /// <param name="check">if set to <c>true</c> [check].</param>
    internal ReplicationSyncConfigElement(bool check)
      : base(check)
    {
    }

    /// <summary>
    /// Gets or sets a value indicating whether replication sync is on or off.
    /// </summary>
    /// <value>True of false.</value>
    [ConfigurationProperty("Enabled", DefaultValue = false)]
    [ObjectInfo(typeof (ReplicationSyncConfigDescriptions), Description = "EnabledDescription", Title = "EnabledCaption")]
    public bool Enabled
    {
      get => (bool) this[nameof (Enabled)];
      set => this[nameof (Enabled)] = (object) value;
    }

    /// <summary>Gets or sets timestamp write interval.</summary>
    /// <value>The value.</value>
    [ConfigurationProperty("TimestampWriteIntervalInSeconds", DefaultValue = 2)]
    [ObjectInfo(typeof (ReplicationSyncConfigDescriptions), Description = "TimestampWriteIntervalInSecondsDescription", Title = "TimestampWriteIntervalInSecondsTitle")]
    public int TimestampWriteIntervalInSeconds
    {
      get => (int) this[nameof (TimestampWriteIntervalInSeconds)];
      set => this[nameof (TimestampWriteIntervalInSeconds)] = (object) value;
    }

    /// <summary>Gets or sets write inactivity threshold.</summary>
    /// <value>The value.</value>
    [ConfigurationProperty("WriteInactivityThreshold", DefaultValue = 300)]
    [ObjectInfo(typeof (ReplicationSyncConfigDescriptions), Description = "WriteInactivityThresholdDescription", Title = "WriteInactivityThresholdTitle")]
    public int WriteInactivityThreshold
    {
      get => (int) this[nameof (WriteInactivityThreshold)];
      set => this[nameof (WriteInactivityThreshold)] = (object) value;
    }

    /// <summary>Gets or sets timestamp read interval.</summary>
    /// <value>The value.</value>
    [ConfigurationProperty("TimestampReadIntervalInSeconds", DefaultValue = 3)]
    [ObjectInfo(typeof (ReplicationSyncConfigDescriptions), Description = "TimestampReadIntervalInSecondsDescription", Title = "TimestampReadIntervalInSecondsTitle")]
    public int TimestampReadIntervalInSeconds
    {
      get => (int) this[nameof (TimestampReadIntervalInSeconds)];
      set => this[nameof (TimestampReadIntervalInSeconds)] = (object) value;
    }

    /// <summary>Gets or sets retry interval for unprocessed messages.</summary>
    /// <value>The value.</value>
    [ConfigurationProperty("MessageRetryIntervalInSeconds", DefaultValue = 2)]
    [ObjectInfo(typeof (ReplicationSyncConfigDescriptions), Description = "MessageRetryIntervalInSecondsDescription", Title = "MessageRetryIntervalInSecondsTitle")]
    public int MessageRetryIntervalInSeconds
    {
      get => (int) this[nameof (MessageRetryIntervalInSeconds)];
      set => this[nameof (MessageRetryIntervalInSeconds)] = (object) value;
    }

    /// <summary>
    /// Gets or sets the number of maximum unprocessed messages that the system will hold in its internal queue.
    /// </summary>
    /// <value>The value.</value>
    [ConfigurationProperty("MaxUnprocessedMessages", DefaultValue = 20000)]
    [ObjectInfo(typeof (ReplicationSyncConfigDescriptions), Description = "MaxUnprocessedMessagesDescription", Title = "MaxUnprocessedMessagesTitle")]
    public int MaxUnprocessedMessages
    {
      get => (int) this[nameof (MaxUnprocessedMessages)];
      set => this[nameof (MaxUnprocessedMessages)] = (object) value;
    }

    /// <summary>Gets or sets maximum message age.</summary>
    /// <value>The value.</value>
    [ConfigurationProperty("MaxMessageAgeInSeconds", DefaultValue = 3600)]
    [ObjectInfo(typeof (ReplicationSyncConfigDescriptions), Description = "MaxMessageAgeInSecondsDescription", Title = "MaxMessageAgeInSecondsTitle")]
    public int MaxMessageAgeInSeconds
    {
      get => (int) this[nameof (MaxMessageAgeInSeconds)];
      set => this[nameof (MaxMessageAgeInSeconds)] = (object) value;
    }

    /// <summary>Gets or sets message transporter types.</summary>
    /// <value>The transporters.</value>
    [ConfigurationProperty("Transporter")]
    [ObjectInfo(typeof (ReplicationSyncConfigDescriptions), Description = "TransportersDescription", Title = "TransportersCaption")]
    public ConfigElementList<ReplicationTransporterConfigElement> Transporters
    {
      get => (ConfigElementList<ReplicationTransporterConfigElement>) this["Transporter"];
      set => this["Transporter"] = (object) value;
    }

    /// <summary>
    /// Adds a new <see cref="T:Telerik.Sitefinity.LoadBalancing.Replication.IReplicationMessageTransporter" /> implementation type to the <see cref="P:Telerik.Sitefinity.LoadBalancing.Configuration.ReplicationSyncConfigElement.Transporters" /> collection.
    /// </summary>
    /// <typeparam name="TTransporter">An <see cref="T:Telerik.Sitefinity.LoadBalancing.Replication.IReplicationMessageTransporter" /> implementation.</typeparam>
    /// <param name="parameters">Activateion parameters.</param>
    public void AddTransporter<TTransporter>(NameValueCollection parameters) where TTransporter : IReplicationMessageTransporter
    {
      string fullName = typeof (TTransporter).FullName;
      if (this.Transporters.Contains(fullName))
        return;
      this.Transporters.Add(new ReplicationTransporterConfigElement()
      {
        TypeName = fullName,
        Parameters = parameters
      });
    }

    /// <summary>
    /// Removes an existing <see cref="T:Telerik.Sitefinity.LoadBalancing.Replication.IReplicationMessageTransporter" /> implementation type to the <see cref="P:Telerik.Sitefinity.LoadBalancing.Configuration.ReplicationSyncConfigElement.Transporters" /> collection.
    /// </summary>
    /// <typeparam name="TTransporter">An <see cref="T:Telerik.Sitefinity.LoadBalancing.Replication.IReplicationMessageTransporter" /> implementation.</typeparam>
    public void RemoveTransporter<TTransporter>() where TTransporter : IReplicationMessageTransporter
    {
      ConfigElement elementByKey = this.Transporters.GetElementByKey(typeof (TTransporter).FullName);
      if (elementByKey == null)
        return;
      this.Transporters.Remove(elementByKey);
    }
  }
}
