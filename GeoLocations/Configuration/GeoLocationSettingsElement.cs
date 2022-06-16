// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.GeoLocations.Configuration.GeoLocationSettingsElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.GeoLocations.Configuration
{
  /// <summary>Configures the geo location settings.</summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "GeoLocationSettingsElementDescription", Title = "GeoLocationSettingsElementTitle")]
  public class GeoLocationSettingsElement : ConfigElement
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public GeoLocationSettingsElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets or sets the google map API v3 key.</summary>
    [ConfigurationProperty("googleMapApiV3Key", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "GoogleMapApiV3KeyDescription", Title = "GoogleMapApiV3KeyTitle")]
    public virtual string GoogleMapApiV3Key
    {
      get => (string) this["googleMapApiV3Key"];
      set => this["googleMapApiV3Key"] = (object) value;
    }

    /// <summary>
    /// Gets or sets whether the search by geo location is enabled.
    /// </summary>
    [ConfigurationProperty("enableGeoLocationSearch", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EnableGeoLocationSearchDescription", Title = "EnableGeoLocationSearchTitle")]
    public virtual bool EnableGeoLocationSearch
    {
      get => (bool) this["enableGeoLocationSearch"];
      set => this["enableGeoLocationSearch"] = (object) value;
    }
  }
}
