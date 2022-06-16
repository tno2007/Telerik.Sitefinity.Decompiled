// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.Configuration.ModuleConfigBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Configuration;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Modules.GenericContent.Configuration
{
  /// <summary>
  /// Defines the common configuration settings for content modules
  /// </summary>
  public abstract class ModuleConfigBase : ConfigSection, IModuleConfig
  {
    internal const string providerGroupParam = "providerGroup";
    internal const string systemProvidersGroup = "System";

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
    /// Called after the properties of this instance have been initialized.
    /// Load default values here.
    /// </summary>
    protected override void OnPropertiesInitialized()
    {
      base.OnPropertiesInitialized();
      this.InitializeDefaultProviders(this.Providers);
    }

    public override void Upgrade(Version oldVersion, Version newVersion)
    {
      base.Upgrade(oldVersion, newVersion);
      if (oldVersion.Build >= 1600)
        return;
      foreach (DataProviderSettings providerSettings in (IEnumerable<DataProviderSettings>) this.Providers.Values)
      {
        if (providerSettings.ProviderType.FullName.StartsWith("Telerik.Sitefinity."))
          providerSettings.Parameters.Remove("version");
      }
    }

    /// <summary>Initializes the default providers.</summary>
    protected abstract void InitializeDefaultProviders(
      ConfigElementDictionary<string, DataProviderSettings> providers);
  }
}
