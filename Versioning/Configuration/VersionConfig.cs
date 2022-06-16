// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Versioning.Configuration.VersionConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.GenericContent.Configuration;
using Telerik.Sitefinity.Versioning.Cleaner.Configuration;
using Telerik.Sitefinity.Versioning.Data;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Versioning.Configuration
{
  /// <summary>
  /// Represents configuration settings for Version Control module.
  /// </summary>
  [ObjectInfo(typeof (VersionResources), Description = "VersionConfigDescription", Title = "VersionConfigTitle")]
  [DescriptionResource(typeof (ConfigDescriptions), "VersioningConfigurationDescriptions")]
  public class VersionConfig : ModuleConfigBase
  {
    [ConfigurationProperty("cleaner")]
    [ObjectInfo(typeof (Labels), Title = "CleanerTitle")]
    public virtual CleanerConfig Cleaner => (CleanerConfig) this["cleaner"];

    protected override void InitializeDefaultProviders(
      ConfigElementDictionary<string, DataProviderSettings> providers)
    {
      providers.Add(new DataProviderSettings((ConfigElement) providers)
      {
        Name = "OpenAccessDataProvider",
        Description = "A data provider that stores version data in database using OpenAccess ORM.",
        ProviderType = typeof (OpenAccessVersionDataProvider)
      });
    }
  }
}
