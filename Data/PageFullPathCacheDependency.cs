// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.PageFullPathCacheDependency
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Data
{
  [ComVisible(false)]
  [Serializable]
  public class PageFullPathCacheDependency : ICacheItemExpiration
  {
    private bool initialized;
    private bool expired;
    private CultureInfo culture;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.PageFullPathCacheDependency" /> class.
    /// </summary>
    public PageFullPathCacheDependency()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.PageFullPathCacheDependency" /> class.
    /// </summary>
    /// <param name="trackedItem">The tracked item.</param>
    public PageFullPathCacheDependency(PageNode trackedItem)
    {
      if (trackedItem == null)
        throw new ArgumentNullException(nameof (trackedItem));
      this.initialized = true;
      this.InternalSubscribe(trackedItem);
    }

    /// <summary>Specifies if item has expired or not.</summary>
    /// <returns>
    /// Returns true if the item has expired, otherwise false.
    /// </returns>
    public bool HasExpired() => this.expired;

    /// <summary>
    /// Called to tell the expiration that the CacheItem to which this expiration belongs has been touched by the user.
    /// </summary>
    public void Notify()
    {
    }

    /// <summary>
    /// Called to give the instance the opportunity to initialize itself from information contained in the CacheItem.
    /// </summary>
    /// <param name="owningCacheItem">CacheItem that owns this expiration object</param>
    public void Initialize(CacheItem owningCacheItem)
    {
      if (this.initialized)
        return;
      this.initialized = true;
      if (!(owningCacheItem.Value is PageNode))
        throw new InvalidOperationException("The cached item is not of type PageNode");
      this.InternalSubscribe((PageNode) owningCacheItem.Value);
    }

    private void InternalSubscribe(PageNode trackedItem) => CacheDependency.Subscribe((object) trackedItem, new ChangedCallback(this.Expire));

    private void Expire(ICacheDependencyHandler caller, Type itemType, string itemKey)
    {
      if (caller == null)
        throw new ArgumentNullException(nameof (caller));
      this.expired = true;
      this.UpdateCache(itemKey);
      CacheDependency.Unsubscribe(caller.GetType(), itemType, itemKey, new ChangedCallback(this.Expire));
    }

    private void UpdateCache(string itemKey)
    {
      ICacheManager cacheManager = SystemManager.GetCacheManager(CacheManagerInstance.PageFullPath);
      PageNodeCacheItem data = (PageNodeCacheItem) cacheManager.GetData(itemKey);
      if (data != null)
      {
        PageNode pageNode = PageManager.GetManager().GetPageNode(new Guid(itemKey));
        if (!this.HasItemChanged(data, pageNode))
          return;
        cacheManager.Flush();
      }
      else
        cacheManager.Flush();
    }

    private void RemoveChildNodesFromCache(ICacheManager cache, PageNode trackedItem)
    {
      List<PageNode> list = new List<PageNode>();
      foreach (PageNode childNode in PageManager.GetChildNodes(trackedItem, list))
        cache.Remove(childNode.Id.ToString());
    }

    private bool HasItemChanged(PageNodeCacheItem cachedItem, PageNode page) => (Lstring) cachedItem.UrlName != page.UrlName || cachedItem.ShowInNavigation != page.ShowInNavigation || cachedItem.NodeType != page.NodeType;
  }
}
