// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSettings.Configuration.SiteSettingsConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.SiteSettings.Data;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.SiteSettings.Configuration
{
  /// <summary>
  /// Represents the configuration section for Site Settings management.
  /// </summary>
  internal class SiteSettingsConfig : ConfigSection
  {
    /// <summary>Gets or sets the name of the default data provider.</summary>
    [DescriptionResource(typeof (ConfigDescriptions), "DefaultProvider")]
    [ConfigurationProperty("defaultProvider", DefaultValue = "OpenAccessDataProvider")]
    public virtual string DefaultProvider
    {
      get => (string) this["defaultProvider"];
      set => this["defaultProvider"] = (object) value;
    }

    /// <summary>Gets a collection of data provider settings.</summary>
    [DescriptionResource(typeof (ConfigDescriptions), "Providers")]
    [ConfigurationProperty("providers")]
    public virtual ConfigElementDictionary<string, DataProviderSettings> Providers => (ConfigElementDictionary<string, DataProviderSettings>) this["providers"];

    /// <summary>
    /// Gets a collection of property paths for properties that could have different values for different sites.
    /// </summary>
    [ConfigurationProperty("siteSpecificProperties")]
    public virtual ConfigElementDictionary<string, PropertyPath> SiteSpecificProperties => (ConfigElementDictionary<string, PropertyPath>) this["siteSpecificProperties"];

    protected override void OnPropertiesInitialized()
    {
      base.OnPropertiesInitialized();
      ConfigElementDictionary<string, DataProviderSettings> providers = this.Providers;
      providers.Add(new DataProviderSettings((ConfigElement) providers)
      {
        Name = "OpenAccessDataProvider",
        Description = "A provider that stores content data in database using OpenAccess ORM.",
        ProviderType = typeof (OpenAccessSiteSettingsProvider)
      });
    }
  }
}
