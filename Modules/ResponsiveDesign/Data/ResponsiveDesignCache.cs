// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ResponsiveDesign.Data.ResponsiveDesignCache
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Telerik.OpenAccess;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.ResponsiveDesign.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Modules.ResponsiveDesign.Data
{
  internal class ResponsiveDesignCache
  {
    private readonly Dictionary<string, IMediaQueryLink> mediaQueryLinks = new Dictionary<string, IMediaQueryLink>();
    private readonly Dictionary<ResponsiveDesignBehavior, IDictionary<Guid, IMediaQuery>> mediaQueries = new Dictionary<ResponsiveDesignBehavior, IDictionary<Guid, IMediaQuery>>();
    private readonly Dictionary<string, object> subCaches = new Dictionary<string, object>();
    private readonly object subCachesSync = new object();
    private static readonly object CacheManagerSync = new object();
    private const string CacheKey = "sf_responsive_design_cache";

    private ResponsiveDesignCache()
    {
      ResponsiveDesignManager manager = ResponsiveDesignManager.GetManager();
      IQueryable<MediaQuery> query = manager.GetMediaQueries().Where<MediaQuery>((Expression<Func<MediaQuery, bool>>) (mq => mq.IsActive)).Include<MediaQuery>((Expression<Func<MediaQuery, object>>) (mq => mq.MediaQueryRules));
      Expression<Func<MediaQuery, object>> path1 = (Expression<Func<MediaQuery, object>>) (mq => mq.NavigationTransformations);
      foreach (IMediaQuery from in (IEnumerable<MediaQuery>) query.Include<MediaQuery>(path1))
        this.AddMediaQuery((IMediaQuery) new MediaQueryProxy(from));
      IQueryable<MediaQueryLink> mediaQueryLinks = manager.GetMediaQueryLinks();
      Expression<Func<MediaQueryLink, object>> path2 = (Expression<Func<MediaQueryLink, object>>) (mql => mql.MediaQueries);
      foreach (MediaQueryLink from1 in (IEnumerable<MediaQueryLink>) mediaQueryLinks.Include<MediaQueryLink>(path2))
      {
        MediaQueryLinkProxy mediaQueryLinkProxy = new MediaQueryLinkProxy((IMediaQueryLink) from1, true);
        List<IMediaQuery> mediaQueryList = new List<IMediaQuery>();
        foreach (MediaQuery from2 in from1.MediaQueries.Where<MediaQuery>((Func<MediaQuery, bool>) (mq => mq.IsActive)))
        {
          IMediaQuery mediaQuery = this.FindMediaQuery(from2.Id);
          if (mediaQuery == null)
          {
            mediaQuery = (IMediaQuery) new MediaQueryProxy((IMediaQuery) from2);
            this.AddMediaQuery(mediaQuery);
          }
          mediaQueryList.Add(mediaQuery);
        }
        mediaQueryLinkProxy.MediaQueries = (IEnumerable<IMediaQuery>) mediaQueryList;
        this.mediaQueryLinks.Add(this.GetMediaQueryLinksKey(from1.ItemType, from1.ItemId), (IMediaQueryLink) mediaQueryLinkProxy);
      }
    }

    internal static ResponsiveDesignCache GetInstance()
    {
      ICacheManager cacheManager = SystemManager.GetCacheManager(CacheManagerInstance.Global);
      if (!(cacheManager["sf_responsive_design_cache"] is ResponsiveDesignCache instance1))
      {
        lock (ResponsiveDesignCache.CacheManagerSync)
        {
          if (!(cacheManager["sf_responsive_design_cache"] is ResponsiveDesignCache instance1))
          {
            instance1 = new ResponsiveDesignCache();
            cacheManager.Add("sf_responsive_design_cache", (object) instance1, CacheItemPriority.Normal, (ICacheItemRefreshAction) null, (ICacheItemExpiration) new DataItemCacheDependency(typeof (MediaQueryLink), (string) null), (ICacheItemExpiration) new DataItemCacheDependency(typeof (MediaQuery), (string) null), (ICacheItemExpiration) new DataItemCacheDependency(typeof (MediaQueryRule), (string) null), (ICacheItemExpiration) new DataItemCacheDependency(typeof (NavigationTransformation), (string) null), (ICacheItemExpiration) new SlidingTime(TimeSpan.FromMinutes(60.0)));
          }
        }
      }
      return instance1;
    }

    internal IEnumerable<IMediaQuery> GetMediaQueries(
      Guid pageDataId,
      Func<IEnumerable<Guid>> templateIdsByPageDataId,
      ResponsiveDesignBehavior behavior)
    {
      if (pageDataId == Guid.Empty)
        return this.GetMediaQueries(behavior);
      IMediaQueryLink mediaQueryLink = (IMediaQueryLink) null;
      if (!this.mediaQueryLinks.TryGetValue(this.GetMediaQueryLinksKey(DesignMediaType.Page, pageDataId), out mediaQueryLink) || mediaQueryLink.LinkType == MediaQueryLinkType.Inherit)
      {
        foreach (Guid itemId in templateIdsByPageDataId())
        {
          if (this.mediaQueryLinks.TryGetValue(this.GetMediaQueryLinksKey(DesignMediaType.Template, itemId), out mediaQueryLink))
          {
            if (mediaQueryLink.LinkType != MediaQueryLinkType.Inherit)
              break;
          }
        }
      }
      if (mediaQueryLink == null || mediaQueryLink.LinkType == MediaQueryLinkType.All)
        return this.GetMediaQueries(behavior);
      return mediaQueryLink.LinkType == MediaQueryLinkType.None ? (IEnumerable<IMediaQuery>) new IMediaQuery[0] : mediaQueryLink.MediaQueries.Where<IMediaQuery>((Func<IMediaQuery, bool>) (mq => mq.Behavior == behavior));
    }

    internal IEnumerable<IMediaQuery> GetMediaQueries(
      ResponsiveDesignBehavior behavior)
    {
      IDictionary<Guid, IMediaQuery> dictionary;
      return !this.mediaQueries.TryGetValue(behavior, out dictionary) ? (IEnumerable<IMediaQuery>) new IMediaQuery[0] : (IEnumerable<IMediaQuery>) dictionary.Values;
    }

    internal IEnumerable<IMediaQuery> GetMediaQueries(
      PageDataProxy pageData,
      ResponsiveDesignBehavior behavior)
    {
      Guid pageDataId = pageData == null ? Guid.Empty : pageData.Id;
      IEnumerable<Guid> templateIds = pageData == null ? (IEnumerable<Guid>) new Guid[0] : (IEnumerable<Guid>) pageData.TemplatesIds;
      return this.GetMediaQueries(pageDataId, (Func<IEnumerable<Guid>>) (() => templateIds), behavior);
    }

    internal object GetOrAddSubCache(string key, Func<object> factory)
    {
      object orAddSubCache;
      if (!this.subCaches.TryGetValue(key, out orAddSubCache))
      {
        lock (this.subCachesSync)
        {
          if (!this.subCaches.TryGetValue(key, out orAddSubCache))
          {
            orAddSubCache = factory();
            this.subCaches.Add(key, orAddSubCache);
          }
        }
      }
      return orAddSubCache;
    }

    private string GetMediaQueryLinksKey(DesignMediaType itemType, Guid itemId) => this.GetMediaQueryLinksKey(itemType.ToString(), itemId);

    private string GetMediaQueryLinksKey(string itemType, Guid itemId) => itemType + (object) itemId;

    private IMediaQuery FindMediaQuery(Guid id)
    {
      foreach (IDictionary<Guid, IMediaQuery> dictionary in this.mediaQueries.Values)
      {
        IMediaQuery mediaQuery;
        if (dictionary.TryGetValue(id, out mediaQuery))
          return mediaQuery;
      }
      return (IMediaQuery) null;
    }

    private void AddMediaQuery(IMediaQuery mediaQuery)
    {
      IDictionary<Guid, IMediaQuery> dictionary;
      if (!this.mediaQueries.TryGetValue(mediaQuery.Behavior, out dictionary))
      {
        dictionary = (IDictionary<Guid, IMediaQuery>) new Dictionary<Guid, IMediaQuery>();
        this.mediaQueries.Add(mediaQuery.Behavior, dictionary);
      }
      dictionary.Add(mediaQuery.Id, mediaQuery);
    }
  }
}
