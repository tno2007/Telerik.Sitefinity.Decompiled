// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.QueryBuilder.QueryBuilderConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Data.QueryBuilder
{
  /// <summary>Defines QueryBuilder configuration settings</summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "QueryBuilderConfigDescription", Title = "QueryBuilderConfigCaption")]
  public class QueryBuilderConfig : ConfigSection
  {
    /// <summary>
    /// Gets or sets the name of the default data provider that is used to manage queries.
    /// </summary>
    [DescriptionResource(typeof (ConfigDescriptions), "DefaultProvider")]
    [ConfigurationProperty("defaultProvider", DefaultValue = "OpenAccessQueryDataProvider")]
    public string DefaultProvider
    {
      get => (string) this["defaultProvider"];
      set => this["defaultProvider"] = (object) value;
    }

    /// <summary>Gets a collection of data provider settings.</summary>
    [DescriptionResource(typeof (ConfigDescriptions), "Providers")]
    [ConfigurationProperty("providers")]
    public ConfigElementDictionary<string, DataProviderSettings> Providers => (ConfigElementDictionary<string, DataProviderSettings>) this["providers"];

    /// <summary>
    /// Called when the corresponding XML element is read and properties loaded.
    /// </summary>
    protected override void OnPropertiesInitialized()
    {
      if (this.Providers.Count != 0)
        return;
      this.Providers.Add(new DataProviderSettings((ConfigElement) this.Providers)
      {
        Name = "OpenAccessQueryDataProvider",
        Description = "A provider that stores persistent objects queries in database using OpenAccess ORM.",
        ProviderType = typeof (OpenAccessQueryDataProvider)
      });
    }
  }
}
