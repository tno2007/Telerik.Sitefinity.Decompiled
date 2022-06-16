// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Multisite.Configuration.MultisiteConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.GenericContent.Configuration;
using Telerik.Sitefinity.Multisite.Data;

namespace Telerik.Sitefinity.Multisite.Configuration
{
  /// <summary>
  /// Represents the configuration section for Multisite management.
  /// </summary>
  public class MultisiteConfig : ModuleConfigBase
  {
    private const string SubFolderSiteFallback = "subFolderSiteFallback";

    /// <summary>Initializes the default providers.</summary>
    /// <param name="providers"></param>
    protected override void InitializeDefaultProviders(
      ConfigElementDictionary<string, DataProviderSettings> providers)
    {
      providers.Add(new DataProviderSettings((ConfigElement) providers)
      {
        Name = "OpenAccessDataProvider",
        Description = "A provider that stores content data in database using OpenAccess ORM.",
        ProviderType = typeof (OpenAccessMultisiteProvider)
      });
    }

    /// <summary>
    /// Enables Domain Site Fallback.
    /// If true and no page is found in the resolved sub-folder site, the system will check for a page with the same URL in the domain site, and will throw 404 Page Not Found HTTP exception otherwise.
    /// </summary>
    [ConfigurationProperty("subFolderSiteFallback", DefaultValue = false)]
    [ObjectInfo(typeof (MultisiteResources), Description = "EnableSubFolderSiteFallbackDescription", Title = "EnableSubFolderSiteFallbackTitle")]
    public bool EnableSubFolderSiteFallback
    {
      get => (bool) this["subFolderSiteFallback"];
      set => this["subFolderSiteFallback"] = (object) value;
    }
  }
}
