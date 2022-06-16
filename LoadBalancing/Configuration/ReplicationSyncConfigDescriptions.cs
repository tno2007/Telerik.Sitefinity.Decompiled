// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.LoadBalancing.Configuration.ReplicationSyncConfigDescriptions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.LoadBalancing.Configuration
{
  /// <summary>Resources for message sync settings.</summary>
  public class ReplicationSyncConfigDescriptions : Resource
  {
    /// <summary>Gets embedded resource.</summary>
    [ResourceEntry("ReplicationSyncSettingsTitle", Description = "Describes configuration element.", LastModified = "2018/06/20", Value = "Replication sync settings")]
    public string ReplicationSyncSettingsTitle => this[nameof (ReplicationSyncSettingsTitle)];

    /// <summary>Gets embedded resource.</summary>
    [ResourceEntry("ReplicationSyncSettingsDescription", Description = "Describes configuration element.", LastModified = "2018/06/20", Value = "Settings for synchronization of system messages during database replication.")]
    public string ReplicationSyncSettingsDescription => this[nameof (ReplicationSyncSettingsDescription)];

    /// <summary>Gets embedded resource.</summary>
    [ResourceEntry("TimestampWriteIntervalInSecondsTitle", LastModified = "2018/06/20", Value = "Timestamp write interval")]
    public string TimestampWriteIntervalInSecondsTitle => this[nameof (TimestampWriteIntervalInSecondsTitle)];

    /// <summary>Gets embedded resource.</summary>
    [ResourceEntry("TimestampWriteIntervalInSecondsDescription", LastModified = "2018/06/20", Value = "Interval (in seconds) after which the timestamp is updated.")]
    public string TimestampWriteIntervalInSecondsDescription => this[nameof (TimestampWriteIntervalInSecondsDescription)];

    /// <summary>Gets embedded resource.</summary>
    [ResourceEntry("WriteInactivityThresholdTitle", LastModified = "2018/07/02", Value = "Write inactivity threshold")]
    public string WriteInactivityThresholdTitle => this[nameof (WriteInactivityThresholdTitle)];

    /// <summary>Gets embedded resource.</summary>
    [ResourceEntry("WriteInactivityThresholdDescription", LastModified = "2018/07/02", Value = "In case a write operation is not performed in this interval (in seconds), the timestamp update process is stopped. Once a write operation is performed, timestamp update process is reset as well.")]
    public string WriteInactivityThresholdDescription => this[nameof (WriteInactivityThresholdDescription)];

    /// <summary>Gets embedded resource.</summary>
    [ResourceEntry("TimestampReadIntervalInSecondsTitle", LastModified = "2018/06/20", Value = "Timestamp read interval")]
    public string TimestampReadIntervalInSecondsTitle => this[nameof (TimestampReadIntervalInSecondsTitle)];

    /// <summary>Gets embedded resource.</summary>
    [ResourceEntry("TimestampReadIntervalInSecondsDescription", LastModified = "2018/06/20", Value = "Interval (in seconds) after which timestamp is read from the secondary database.")]
    public string TimestampReadIntervalInSecondsDescription => this[nameof (TimestampReadIntervalInSecondsDescription)];

    /// <summary>Gets embedded resource.</summary>
    [ResourceEntry("MessageRetryIntervalInSecondsTitle", LastModified = "2018/06/20", Value = "Message retry interval")]
    public string MessageRetryIntervalInSecondsTitle => this[nameof (MessageRetryIntervalInSecondsTitle)];

    /// <summary>Gets embedded resource.</summary>
    [ResourceEntry("MessageRetryIntervalInSecondsDescription", LastModified = "2018/06/20", Value = "Interval (in seconds) after which the system retries handling all unprocessed messages.")]
    public string MessageRetryIntervalInSecondsDescription => this[nameof (MessageRetryIntervalInSecondsDescription)];

    /// <summary>Gets embedded resource.</summary>
    [ResourceEntry("MaxUnprocessedMessagesTitle", LastModified = "2018/06/20", Value = "Max unprocessed messages")]
    public string MaxUnprocessedMessagesTitle => this[nameof (MaxUnprocessedMessagesTitle)];

    /// <summary>Gets embedded resource.</summary>
    [ResourceEntry("MaxUnprocessedMessagesDescription", LastModified = "2018/06/20", Value = "Maximum number of unprocessed messages the system will hold before discarding them.")]
    public string MaxUnprocessedMessagesDescription => this[nameof (MaxUnprocessedMessagesDescription)];

    /// <summary>Gets embedded resource.</summary>
    [ResourceEntry("MaxMessageAgeInSecondsTitle", LastModified = "2018/06/20", Value = "Max message age")]
    public string MaxMessageAgeInSecondsTitle => this[nameof (MaxMessageAgeInSecondsTitle)];

    /// <summary>Gets embedded resource.</summary>
    [ResourceEntry("MaxMessageAgeInSecondsDescription", LastModified = "2018/06/20", Value = "Maximum age (in seconds) of unprocessed messages. Messages older than this value are discarded.")]
    public string MaxMessageAgeInSecondsDescription => this[nameof (MaxMessageAgeInSecondsDescription)];

    /// <summary>Gets embedded resource.</summary>
    [ResourceEntry("RedisSettingsCaption", LastModified = "2018/07/17", Value = "Redis transporter settings")]
    public string RedisSettingsCaption => this[nameof (RedisSettingsCaption)];

    /// <summary>Gets embedded resource.</summary>
    [ResourceEntry("RedisSettingsDescription", LastModified = "2018/07/17", Value = "Settings for Redis transporter that will handle replication related NLB messages.")]
    public string RedisSettingsDescription => this[nameof (RedisSettingsDescription)];

    /// <summary>Gets embedded resource.</summary>
    [ResourceEntry("TransportersCaption", LastModified = "2018/07/23", Value = "Transporters")]
    public string TransportersCaption => this[nameof (TransportersCaption)];

    /// <summary>Gets embedded resource.</summary>
    [ResourceEntry("TransportersDescription", LastModified = "2018/07/23", Value = "Defines replication message transporters and their initialization values.")]
    public string TransportersDescription => this[nameof (TransportersDescription)];

    /// <summary>Gets embedded resource.</summary>
    [ResourceEntry("EnabledCaption", LastModified = "2018/07/25", Value = "Enabled")]
    public string EnabledCaption => this[nameof (EnabledCaption)];

    /// <summary>Gets embedded resource.</summary>
    [ResourceEntry("EnabledDescription", LastModified = "2018/07/25", Value = "Turns on/off message synchronization")]
    public string EnabledDescription => this[nameof (EnabledDescription)];

    /// <summary>Gets embedded resource.</summary>
    [ResourceEntry("TransporterTypeCaption", LastModified = "2018/07/25", Value = "Type Name")]
    public string TransporterTypeCaption => this[nameof (TransporterTypeCaption)];

    /// <summary>Gets embedded resource.</summary>
    [ResourceEntry("TransporterTypeDescription", LastModified = "2018/07/25", Value = "An instance of this type will be created and if successfully activated used for message transportation.")]
    public string TransporterTypeDescription => this[nameof (TransporterTypeDescription)];
  }
}
