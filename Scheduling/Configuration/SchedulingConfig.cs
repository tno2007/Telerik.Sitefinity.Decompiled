// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Scheduling.Configuration.SchedulingConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Specialized;
using System.Configuration;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Scheduling.Data;

namespace Telerik.Sitefinity.Scheduling.Configuration
{
  /// <summary>Main configuration for the scheduling sub-system</summary>
  [DescriptionResource(typeof (SchedulingResources), "SchedulingConfig")]
  public class SchedulingConfig : ConfigSection
  {
    /// <summary>Vanilla default provider name</summary>
    public const string DefaultProviderName = "OASchedulingProvider";

    /// <summary>
    /// Gets or sets the name of the default data provider that is used to manage pages.
    /// </summary>
    [DescriptionResource(typeof (SchedulingResources), "DefaultProvider")]
    [ConfigurationProperty("defaultProvider", DefaultValue = "OASchedulingProvider")]
    public virtual string DefaultProvider
    {
      get => (string) this["defaultProvider"];
      set => this["defaultProvider"] = (object) value;
    }

    /// <summary>Gets or sets the default provider settings</summary>
    [DescriptionResource(typeof (SchedulingResources), "ProviderSettings")]
    [ConfigurationProperty("providerSettings")]
    public virtual ConfigElementDictionary<string, DataProviderSettings> ProviderSettings
    {
      get => (ConfigElementDictionary<string, DataProviderSettings>) this["providerSettings"];
      set => this["providerSettings"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the maximum period of time after the last update of a running background task
    /// which would be displayed on the frontend (e.g. tasks that run on content entities)
    /// that it would be returned from the frontend-facing serivce.
    /// </summary>
    [DescriptionResource(typeof (SchedulingResources), "RunningTasksVisibilityMaxPeriod")]
    [ConfigurationProperty("runningTasksVisibilityMaxPeriod", DefaultValue = 0)]
    public virtual int RunningTasksVisibilityMaxPeriod
    {
      get => (int) this["runningTasksVisibilityMaxPeriod"];
      set => this["runningTasksVisibilityMaxPeriod"] = (object) value;
    }

    [ConfigurationProperty("disableScheduledTasksExecution")]
    [ObjectInfo(typeof (SchedulingResources), Description = "DisableScheduledTasksExecutionConfigDescription", Title = "DisableScheduledTasksExecutionConfigCaption")]
    public virtual bool DisableScheduledTasksExecution
    {
      get => (bool) this["disableScheduledTasksExecution"];
      set => this["disableScheduledTasksExecution"] = (object) value;
    }

    /// <summary>
    /// Called when the corresponding XML element is read and properties loaded.
    /// </summary>
    protected override void OnPropertiesInitialized()
    {
      base.OnPropertiesInitialized();
      this.ProviderSettings.Add(new DataProviderSettings((ConfigElement) this.ProviderSettings)
      {
        Name = "OASchedulingProvider",
        Description = "A provider that stores publishing data in database using OpenAccess ORM.",
        ProviderType = typeof (OpenAccessSchedulingProvider),
        Parameters = new NameValueCollection()
        {
          {
            "applicationName",
            "/Scheduling"
          }
        }
      });
    }

    private static class PropertyNames
    {
      public const string DefaultProvider = "defaultProvider";
      public const string ProviderSettings = "providerSettings";
      public const string ScheduledTypeStrategy = "scheduledTypeStrategy";
      public const string RunningTasksVisibilityMaxPeriod = "runningTasksVisibilityMaxPeriod";
    }
  }
}
