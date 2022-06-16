// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSettings.Basic.TimeZoneSettingsContract
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Cloud.WindowsAzure;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.SiteSettings.Basic
{
  [DataContract]
  public class TimeZoneSettingsContract : ISettingsDataContract
  {
    private TypeConverter timeZoneInfoConverter;

    [DataMember]
    public string TimeZoneId { get; set; }

    public TimeZoneInfo TimeZone => (TimeZoneInfo) this.TimeZoneInfoConverter.ConvertFromString(this.TimeZoneId);

    private TypeConverter TimeZoneInfoConverter
    {
      get
      {
        if (this.timeZoneInfoConverter == null)
          this.timeZoneInfoConverter = (TypeConverter) new TimeZoneInfoTypeConverter();
        return this.timeZoneInfoConverter;
      }
    }

    public void LoadDefaults(bool forEdit = false) => this.TimeZoneId = ((!forEdit ? Config.Get<SystemConfig>() : ConfigManager.GetManager().GetSection<SystemConfig>()).UITimeZoneSettings.CurrentTimeZoneInfo ?? this.GetDefaultTimeZone()).Id;

    public void SaveDefaults()
    {
      ConfigManager manager = ConfigManager.GetManager();
      SystemConfig section = manager.GetSection<SystemConfig>();
      section.UITimeZoneSettings.CurrentTimeZoneInfo = this.TimeZone;
      manager.SaveSection((ConfigSection) section);
    }

    private TimeZoneInfo GetDefaultTimeZone() => AzureRuntime.IsRunning ? TimeZoneInfo.Utc : TimeZoneInfo.Local;
  }
}
