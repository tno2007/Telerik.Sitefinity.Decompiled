// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Basic.GeneralSettingsModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Project.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Configuration.Basic
{
  /// <summary>Represents the model for general settings.</summary>
  [DataContract]
  [Obsolete("Use TimeZoneSettingsContract instead.")]
  public class GeneralSettingsModel
  {
    private ProjectConfig projectConfigSection;
    private SystemConfig systemConfigSection;
    private ConfigManager manager;
    private TypeConverter timeZoneInfoConverter;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Configuration.Basic.GeneralSettingsModel" /> class.
    /// </summary>
    public GeneralSettingsModel()
    {
      TimeZoneInfo timeZoneInfo = this.SystemConfigSection.UITimeZoneSettings.CurrentTimeZoneInfo ?? TimeZoneInfo.Local;
      this.CurrentTimeZoneId = timeZoneInfo.Id;
      this.SupportsDaylightSavingTime = timeZoneInfo.SupportsDaylightSavingTime;
    }

    /// <summary>Defines the name of the project.</summary>
    [DataMember]
    public string ProjectName
    {
      get => this.ProjectConfigSection.ProjectName;
      set => this.ProjectConfigSection.ProjectName = value;
    }

    [DataMember]
    public string SiteId { get; set; }

    /// <summary>Gets or sets the name of the time zone.</summary>
    /// <value>The name of the time zone.</value>
    [DataMember]
    public string CurrentTimeZoneId { get; set; }

    /// <summary>Gets a value indicating whether the time zone has any daylight saving
    /// time rules.</summary>
    /// <returns>true if the time zone supports daylight saving time; otherwise, false.
    /// </returns>
    [DataMember]
    public bool SupportsDaylightSavingTime { get; set; }

    /// <summary>Gets the project config section.</summary>
    /// <value>The project config section.</value>
    public ProjectConfig ProjectConfigSection
    {
      get
      {
        if (this.projectConfigSection == null)
          this.projectConfigSection = this.Manager.GetSection<ProjectConfig>();
        return this.projectConfigSection;
      }
    }

    /// <summary>Gets the system config section.</summary>
    /// <value>The system config section.</value>
    public SystemConfig SystemConfigSection
    {
      get
      {
        if (this.systemConfigSection == null)
          this.systemConfigSection = this.Manager.GetSection<SystemConfig>();
        return this.systemConfigSection;
      }
    }

    /// <summary>Gets the manager.</summary>
    /// <value>The manager.</value>
    private ConfigManager Manager
    {
      get
      {
        if (this.manager == null)
          this.manager = ConfigManager.GetManager();
        return this.manager;
      }
    }

    private TypeConverter TimeZoneInfoConverter
    {
      get
      {
        if (this.timeZoneInfoConverter == null)
          this.timeZoneInfoConverter = (TypeConverter) new TimeZoneInfoTypeConverter();
        return this.timeZoneInfoConverter;
      }
    }

    internal void SaveChanges()
    {
      this.SystemConfigSection.UITimeZoneSettings.CurrentTimeZoneInfo = (TimeZoneInfo) this.TimeZoneInfoConverter.ConvertFromString(this.CurrentTimeZoneId);
      this.Manager.SaveSection((ConfigSection) this.SystemConfigSection);
    }
  }
}
