// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Packaging.Configuration.PackagingConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.GenericContent.Configuration;
using Telerik.Sitefinity.Packaging.Data;
using Telerik.Sitefinity.Packaging.Restriction;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Packaging.Configuration
{
  /// <summary>The configuration for Dashboard module.</summary>
  [ObjectInfo(typeof (PackagingResources), Description = "PackagingConfigDescription", Title = "PackagingConfigCaption")]
  internal class PackagingConfig : ModuleConfigBase
  {
    /// <inheritdoc />
    protected override void InitializeDefaultProviders(
      ConfigElementDictionary<string, DataProviderSettings> providers)
    {
      if (this.Providers.Count != 0)
        return;
      this.Providers.Add(new DataProviderSettings()
      {
        Name = "OpenAccessDataProvider",
        Description = "The data provider for packaging module based on the Telerik OpenAccess ORM.",
        ProviderType = typeof (OpenAccessPackagingProvider)
      });
    }

    /// <summary>
    /// Gets or sets a value indicating whether Packaging module should delete modules not present in the import packages.
    /// </summary>
    [DescriptionResource(typeof (ConfigDescriptions), "DeleteModulesIfNotInPackage")]
    [ConfigurationProperty("deleteModulesIfNotInPackage", DefaultValue = false)]
    public virtual bool DeleteModulesIfNotInPackage
    {
      get => (bool) this["deleteModulesIfNotInPackage"];
      set => this["deleteModulesIfNotInPackage"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether Packaging module should import packages or Addons.
    /// </summary>
    [DescriptionResource(typeof (ConfigDescriptions), "DisableImport")]
    [ConfigurationProperty("disableImport", DefaultValue = false)]
    public virtual bool DisableImport
    {
      get => (bool) this["disableImport"];
      set => this["disableImport"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether [packaging mode].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [packaging mode]; otherwise, <c>PackagingMode.Source</c>.
    /// </value>
    [DescriptionResource(typeof (ConfigDescriptions), "PackagingMode")]
    [ConfigurationProperty("packagingMode", DefaultValue = PackagingMode.Source)]
    public virtual PackagingMode PackagingMode
    {
      get => (PackagingMode) this["packagingMode"];
      set => this["packagingMode"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether multisite import/export is disabled.
    /// </summary>
    /// <value>
    /// <c>true</c> if multisite import/export is disabled; otherwise, <c>false</c>.
    /// </value>
    [DescriptionResource(typeof (ConfigDescriptions), "DisableMultisiteImportExport")]
    [ConfigurationProperty("disableMultisiteImportExport", DefaultValue = false)]
    public virtual bool DisableMultisiteImportExport
    {
      get => (bool) this["disableMultisiteImportExport"];
      set => this["disableMultisiteImportExport"] = (object) value;
    }
  }
}
