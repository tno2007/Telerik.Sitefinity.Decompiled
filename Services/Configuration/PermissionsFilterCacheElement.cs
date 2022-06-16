// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Configuration.PermissionsFilterCacheElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Services.Configuration
{
  /// <summary>
  /// Configures the permission cache when it comes to filtering the search results and also in the dashboard
  /// </summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "PermissionsFilterCacheElementDescription", Title = "PermissionsFilterCacheElementTitle")]
  public class PermissionsFilterCacheElement : ConfigElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.Configuration.PermissionsFilterCacheElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public PermissionsFilterCacheElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Gets or sets the time duration, in seconds, during which the type permissions will be cached.
    /// </summary>
    [ConfigurationProperty("typePermissionsCacheMaxTotalCount", DefaultValue = 1000)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "TypePermissionsCacheMaxTotalCountDescription", Title = "TypePermissionsCacheMaxTotalCountTitle")]
    public int TypePermissionsCacheMaxTotalCount
    {
      get => (int) this["typePermissionsCacheMaxTotalCount"];
      set => this["typePermissionsCacheMaxTotalCount"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the number of item permissions that will be stored
    /// </summary>
    [ConfigurationProperty("itemPermissionsCacheMaxTotalCount", DefaultValue = 10000)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ItemPermissionsCacheMaxTotalCountCountDescription", Title = "ItemPermissionsCacheMaxTotalCountTitle")]
    public int ItemPermissionsCacheMaxTotalCount
    {
      get => (int) this["itemPermissionsCacheMaxTotalCount"];
      set => this["itemPermissionsCacheMaxTotalCount"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the expiration time for permissions cache (in minutes)
    /// </summary>
    [ConfigurationProperty("permissionsCacheSlidingExpirationTime", DefaultValue = 15)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "PermissionsCacheSlidingExpirationTimeDescription", Title = "PermissionsCacheSlidingExpirationTimeTitle")]
    public int PermissionsCacheSlidingExpirationTime
    {
      get => (int) this["permissionsCacheSlidingExpirationTime"];
      set => this["permissionsCacheSlidingExpirationTime"] = (object) value;
    }
  }
}
