// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Configuration.TaxonomyConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.GenericContent.Configuration;
using Telerik.Sitefinity.Taxonomies.Data;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Taxonomies.Configuration
{
  /// <summary>Defines taxonomy configuration settings.</summary>
  [DescriptionResource(typeof (ConfigDescriptions), "TaxonomyConfig")]
  public class TaxonomyConfig : ModuleConfigBase
  {
    /// <inheritdoc />
    protected override void InitializeDefaultProviders(
      ConfigElementDictionary<string, DataProviderSettings> providers)
    {
      providers.Add(new DataProviderSettings((ConfigElement) providers)
      {
        Name = "OpenAccessDataProvider",
        Description = "A provider that stores taxonomy data in database using OpenAccess ORM.",
        ProviderType = typeof (OpenAccessTaxonomyProvider)
      });
    }

    /// <summary>
    /// Gets or sets the default page size of Hierarchical taxonomies in the Backend.
    /// </summary>
    [DescriptionResource(typeof (ConfigDescriptions), "DefaultPageSize")]
    [ConfigurationProperty("defaultPageSize", DefaultValue = 50)]
    public virtual int DefaultPageSize
    {
      get => (int) this["defaultPageSize"];
      set => this["defaultPageSize"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the default synonyms separator of taxon.
    /// </summary>
    [DescriptionResource(typeof (ConfigDescriptions), "TaxonSynonymsSeparator")]
    [ConfigurationProperty("taxonSynonymsSeparator", DefaultValue = ",")]
    public virtual string TaxonSynonymsSeparator
    {
      get => (string) this["taxonSynonymsSeparator"];
      set => this["taxonSynonymsSeparator"] = (object) value;
    }
  }
}
