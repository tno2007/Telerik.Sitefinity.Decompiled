// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.UsageTracking.Configuration.UsageTrackingElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using NCrontab;
using System;
using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.UsageTracking.Configuration
{
  /// <summary>Represents usage tracking configuration section.</summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "UsageTrackingConfigDescription", Title = "UsageTrackingConfigCaption")]
  public class UsageTrackingElement : ConfigElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.UsageTracking.Configuration.UsageTrackingElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public UsageTrackingElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    ///  Gets or sets a value indicating whether to enable usage tracking.
    /// </summary>
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EnableUsageTrackingDescription", Title = "EnableUsageTrackingTitle")]
    [ConfigurationProperty("enableUsageTracking", DefaultValue = true)]
    public bool EnableUsageTracking
    {
      get => (bool) this["enableUsageTracking"];
      set => this["enableUsageTracking"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the cron specification for auto-sync task.
    /// </summary>
    [ConfigurationProperty("autoSyncCronSpec", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "AutoSyncCronSpecDescription", Title = "AutoSyncCronSpecTitle")]
    public string AutoSyncCronSpec
    {
      get => (string) this["autoSyncCronSpec"];
      set
      {
        value = value != null ? value.Trim() : throw new ArgumentNullException("autoSyncCronSpec");
        bool flag = true;
        if (!string.IsNullOrEmpty(value))
        {
          if (value.Split(new char[1]{ ' ' }, StringSplitOptions.RemoveEmptyEntries).Length != 5 || CrontabSchedule.TryParse(value).IsError)
            flag = false;
        }
        if (!flag)
          throw new FormatException("Invalid cron date");
        this["autoSyncCronSpec"] = (object) value;
      }
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct ConfigProps
    {
      public const string EnableUsageTracking = "enableUsageTracking";
      public const string AutoSyncCronSpec = "autoSyncCronSpec";
    }
  }
}
