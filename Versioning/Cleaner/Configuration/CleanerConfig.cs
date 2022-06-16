// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Versioning.Cleaner.Configuration.CleanerConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Versioning.Cleaner.Configuration
{
  public class CleanerConfig : ConfigElement
  {
    private readonly TimeSpan DefaultTimeToStartCleanupTask = TimeSpan.FromHours(1.0);

    public CleanerConfig(ConfigElement parent)
      : base(parent)
    {
    }

    [ConfigurationProperty("historyLimitEnabled", DefaultValue = false)]
    [Browsable(false)]
    public virtual bool HistoryLimitEnabled
    {
      get => (bool) this["historyLimitEnabled"];
      set => this["historyLimitEnabled"] = (object) value;
    }

    [ConfigurationProperty("majorVersionsToKeep", DefaultValue = 10)]
    [Browsable(false)]
    public virtual int MajorVersionsToKeep
    {
      get => (int) this["majorVersionsToKeep"];
      set => this["majorVersionsToKeep"] = (object) value;
    }

    [ConfigurationProperty("timeToKeep", DefaultValue = TimeToKeepEnum.LastThreeMonths)]
    [Browsable(false)]
    public virtual TimeToKeepEnum TimeToKeep
    {
      get => (TimeToKeepEnum) this["timeToKeep"];
      set => this["timeToKeep"] = (object) value;
    }

    [ConfigurationProperty("timeToRunCleanupTask")]
    [ObjectInfo(typeof (Labels), Description = "TimeToRunCleanupTaskDescription", Title = "TimeToRunCleanupTask")]
    [TimeSpanValidator(MaxValueString = "23:59:59", MinValueString = "00:00:00")]
    public TimeSpan TimeToRunCleanupTask
    {
      get => (TimeSpan) this["timeToRunCleanupTask"];
      set => this["timeToRunCleanupTask"] = (object) value;
    }

    internal TimeSpan GetPeriodToKeep()
    {
      switch (this.TimeToKeep)
      {
        case TimeToKeepEnum.LastWeek:
          return TimeSpan.FromDays(7.0);
        case TimeToKeepEnum.LastMonth:
          return TimeSpan.FromDays(30.0);
        case TimeToKeepEnum.LastThreeMonths:
          return TimeSpan.FromDays(90.0);
        case TimeToKeepEnum.LastSixMonths:
          return TimeSpan.FromDays(180.0);
        case TimeToKeepEnum.LastYear:
          return TimeSpan.FromDays(365.0);
        default:
          throw new InvalidOperationException("Invalid enumeration value for versioning TimeToKeep.");
      }
    }

    protected override void OnPropertiesInitialized()
    {
      this.TimeToRunCleanupTask = this.DefaultTimeToStartCleanupTask;
      base.OnPropertiesInitialized();
    }
  }
}
