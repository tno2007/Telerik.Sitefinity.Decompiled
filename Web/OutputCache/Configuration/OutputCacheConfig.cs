// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.OutputCache.Configuration.OutputCacheConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.GenericContent.Configuration;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.OutputCache.Data;

namespace Telerik.Sitefinity.Web.OutputCache.Configuration
{
  /// <summary>Configuration for Output Cache.</summary>
  /// <seealso cref="T:Telerik.Sitefinity.Modules.GenericContent.Configuration.ModuleConfigBase" />
  internal class OutputCacheConfig : ModuleConfigBase
  {
    private const string DefaultProviderName = "OpenAccessOutputCacheRelationsProvider";

    /// <summary>Gets or sets the name of the default data provider.</summary>
    [DescriptionResource(typeof (ConfigDescriptions), "DefaultProvider")]
    [ConfigurationProperty("defaultProvider", DefaultValue = "OpenAccessOutputCacheRelationsProvider")]
    public override string DefaultProvider
    {
      get => (string) this["defaultProvider"];
      set => this["defaultProvider"] = (object) value;
    }

    /// <summary>Initializes the default providers.</summary>
    /// <param name="providers">The storage providers for output cache metadata.</param>
    protected override void InitializeDefaultProviders(
      ConfigElementDictionary<string, DataProviderSettings> providers)
    {
      providers.Add(new DataProviderSettings((ConfigElement) providers)
      {
        Name = "OpenAccessOutputCacheRelationsProvider",
        Description = "A provider that stores output cache relations in database using OpenAccess ORM.",
        ProviderType = typeof (OpenAccessOutputCacheRelationsProvider)
      });
    }
  }
}
