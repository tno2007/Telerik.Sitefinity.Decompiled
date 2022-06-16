// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SyncMap.Configuration.SyncMapConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.SyncMap.Data;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.SyncMap.Configuration
{
  /// <summary>
  /// Represents the configuration section for Site Settings management.
  /// </summary>
  internal class SyncMapConfig : ConfigSection
  {
    public override bool VisibleInUI => false;

    /// <summary>Gets or sets the name of the default data provider.</summary>
    [DescriptionResource(typeof (ConfigDescriptions), "DefaultProvider")]
    [ConfigurationProperty("defaultProvider", DefaultValue = "Default")]
    public virtual string DefaultProvider
    {
      get => (string) this["defaultProvider"];
      set => this["defaultProvider"] = (object) value;
    }

    /// <summary>Gets a collection of data provider settings.</summary>
    [DescriptionResource(typeof (ConfigDescriptions), "Providers")]
    [ConfigurationProperty("providers")]
    public virtual ConfigElementDictionary<string, DataProviderSettings> Providers => (ConfigElementDictionary<string, DataProviderSettings>) this["providers"];

    protected override void OnPropertiesInitialized()
    {
      base.OnPropertiesInitialized();
      ConfigElementDictionary<string, DataProviderSettings> providers = this.Providers;
      providers.Add(new DataProviderSettings((ConfigElement) providers)
      {
        Name = "Default",
        Description = "A provider that stores content data in database using OpenAccess ORM.",
        ProviderType = typeof (OpenAccessSyncMapProvider)
      });
    }
  }
}
