// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.SitefinitySettingsElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Abstractions;

namespace Telerik.Sitefinity.Configuration
{
  /// <summary>
  /// Represents the <c>settings</c> configuration element within the <c>configuration/telerik/sitefinity</c>
  /// configuration section, used to configure Sitefinity's configuration.
  /// </summary>
  public class SitefinitySettingsElement : ConfigurationElement
  {
    /// <summary>
    /// Gets or sets the name of the default provider that is used to manage users and roles.
    /// </summary>
    [ConfigurationProperty("defaultProvider")]
    [StringValidator]
    public string DefaultProvider
    {
      get => (string) this["defaultProvider"];
      set => this["defaultProvider"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the name of the default provider that is used to manage users and roles.
    /// </summary>
    [ConfigurationProperty("defaultSecretResolver")]
    [StringValidator]
    public string DefaultSecretResolver
    {
      get => (string) this["defaultSecretResolver"];
      set => this["defaultSecretResolver"] = (object) value;
    }

    /// <summary>
    /// Gets a ProviderSettingsCollection object of ProviderSettings objects.
    /// </summary>
    [ConfigurationProperty("providers")]
    public ProviderSettingsCollection Providers => (ProviderSettingsCollection) this["providers"];

    /// <summary>
    /// Gets a ProviderSettingsCollection object of SecretResolvers objects.
    /// </summary>
    [ConfigurationProperty("secretResolvers")]
    public ProviderSettingsCollection SecretResolvers => (ProviderSettingsCollection) this["secretResolvers"];

    /// <summary>
    /// Gets or sets the name of the default provider that is used to manage users and roles.
    /// </summary>
    [ConfigurationProperty("storageFolder", DefaultValue = "~/App_Data/Sitefinity/Configuration/")]
    [StringValidator]
    public string StorageFolder
    {
      get => (string) this["storageFolder"];
      set => this["storageFolder"] = (object) value;
    }

    /// <summary>Gets or sets the storage mode.</summary>
    /// <value>The storage mode.</value>
    [ConfigurationProperty("storageMode", DefaultValue = ConfigStorageMode.FileSystem)]
    public ConfigStorageMode StorageMode
    {
      get => (ConfigStorageMode) this["storageMode"];
      set => this["storageMode"] = (object) value;
    }

    /// <summary>Gets or sets the restriction level.</summary>
    /// <value>The restriction level.</value>
    [ConfigurationProperty("restrictionLevel", DefaultValue = RestrictionLevel.Default)]
    public RestrictionLevel RestrictionLevel
    {
      get => (RestrictionLevel) this["restrictionLevel"];
      set => this["restrictionLevel"] = (object) value;
    }

    /// <summary>
    /// Determines whether to upgrade files when storage mode is database.
    /// </summary>
    /// <value>True if files should be upgraded, otherwise false.</value>
    [ConfigurationProperty("upgradeFilesWhenDatabaseMode", DefaultValue = true)]
    public bool UpgradeFilesWhenDatabaseMode
    {
      get => (bool) this["upgradeFilesWhenDatabaseMode"];
      set => this["upgradeFilesWhenDatabaseMode"] = (object) value;
    }
  }
}
