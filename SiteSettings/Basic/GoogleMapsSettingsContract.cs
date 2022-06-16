// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSettings.Basic.GoogleMapsSettingsContract
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.SiteSettings.Basic
{
  /// <summary>Represents the contract for google maps settings.</summary>
  [DataContract]
  public class GoogleMapsSettingsContract : ISettingsDataContract
  {
    /// <summary>Gets or sets the google maps api v3 key.</summary>
    [DataMember]
    public string GoogleMapApiV3Key { get; set; }

    /// <inheritdoc />
    public void LoadDefaults(bool forEdit = false) => this.GoogleMapApiV3Key = (!forEdit ? Config.Get<SystemConfig>() : ConfigManager.GetManager().GetSection<SystemConfig>()).GeoLocationSettings.GoogleMapApiV3Key;

    /// <inheritdoc />
    public void SaveDefaults()
    {
      ConfigManager manager = ConfigManager.GetManager();
      SystemConfig systemConfig = Config.Get<SystemConfig>();
      systemConfig.GeoLocationSettings.GoogleMapApiV3Key = this.GoogleMapApiV3Key;
      SystemConfig section = systemConfig;
      manager.SaveSection((ConfigSection) section);
    }
  }
}
