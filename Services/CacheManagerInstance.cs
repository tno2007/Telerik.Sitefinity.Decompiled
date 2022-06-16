// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.CacheManagerInstance
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Services
{
  /// <summary>Defines cache manager instances.</summary>
  public enum CacheManagerInstance
  {
    /// <summary>Internal use cache manger instance.</summary>
    Internal,
    /// <summary>All-purpose common use cache manger instance.</summary>
    Global,
    /// <summary>Cache manager for caching configuration information.</summary>
    Configuration,
    /// <summary>Cache manager for caching site map nodes.</summary>
    SiteMap,
    /// <summary>
    /// Cache manager for caching the pageData of the site map nodes.
    /// </summary>
    SiteMapPageData,
    /// <summary>
    /// Cache manager for caching the site map nodes by URL requested from the route handler.
    /// </summary>
    SiteMapNodeUrl,
    /// <summary>Cache manager for caching user activities.</summary>
    UserActivities,
    /// <summary>Cache manager for caching content output.</summary>
    ContentOutput,
    /// <summary>Cache manager for caching pages full paths.</summary>
    PageFullPath,
    /// <summary>Cache manager for caching user information.</summary>
    Users,
    /// <summary>Cache manager for caching content links.</summary>
    ContentLinks,
    /// <summary>Cache manager for content locations service</summary>
    ContentLocations,
    /// <summary>Cache manager for labels</summary>
    LocalizationResources,
    /// <summary>Cache manager for statistics</summary>
    Statistics,
    /// <summary>Cache manager for output cache items' metadata</summary>
    OutputCacheInfo,
  }
}
