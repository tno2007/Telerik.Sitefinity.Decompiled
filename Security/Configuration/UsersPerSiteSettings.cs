// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Configuration.UsersPerSiteSettings
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Security.Configuration
{
  /// <summary>
  /// Settings for configuring the users per site settings for multisite.
  /// </summary>
  public class UsersPerSiteSettings : ConfigElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.Configuration.UsersPerSiteSettings" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public UsersPerSiteSettings(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Gets or sets a value indicating whether the system allows separate users per site
    /// </summary>
    /// <value>Value indicating whether the system allows separate users per site</value>
    [ConfigurationProperty("allowSeparateUsersPerSite", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "SeparateUsersPerSiteSettingsDescription", Title = "SeparateUsersPerSiteTitle")]
    public bool AllowSeparateUsersPerSite
    {
      get => (bool) this["allowSeparateUsersPerSite"];
      set => this["allowSeparateUsersPerSite"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the a comma-separated list of names of the provider used for managing the global system items, not only site specific.
    /// If empty, all membership providers are used.
    /// </summary>
    /// <value>The users sharing mode.</value>
    [ConfigurationProperty("globalUsersProviderNames", DefaultValue = "")]
    internal string GlobalUsersProviderNames
    {
      get => (string) this["globalUsersProviderNames"];
      set => this["globalUsersProviderNames"] = (object) value;
    }

    /// <summary>
    /// Gets a collection of defined effective permissions per permission set assigned for local (site) administrators.
    /// </summary>
    [ConfigurationProperty("siteAdminPermissions")]
    [ConfigurationCollection(typeof (Permission), AddItemName = "permission")]
    internal ConfigElementDictionary<string, EffectivePermission> SiteAdminPermissions => (ConfigElementDictionary<string, EffectivePermission>) this["siteAdminPermissions"];

    /// <summary>
    /// Gets or sets a value indicating how the data sources (providers) will be accessed across the sites.
    /// </summary>
    [ConfigurationProperty("accessLevel", DefaultValue = DataSourceAccessLevel.NoRestriction)]
    internal DataSourceAccessLevel AccessLevel
    {
      get => (DataSourceAccessLevel) this["accessLevel"];
      set => this["accessLevel"] = (object) value;
    }

    private class PropNames
    {
      internal const string AllowSeparateUsersPerSite = "allowSeparateUsersPerSite";
      internal const string GlobalUsersProviderNames = "globalUsersProviderNames";
      internal const string SiteAdminPermissions = "siteAdminPermissions";
      internal const string AccessLevel = "accessLevel";
    }
  }
}
