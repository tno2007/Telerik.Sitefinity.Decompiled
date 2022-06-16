// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContentUsages.ContentUsageService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Telerik.Microsoft.Practices.Unity.Utility;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.ContentUsages.Comparers;
using Telerik.Sitefinity.ContentUsages.Filters;
using Telerik.Sitefinity.ContentUsages.Model;
using Telerik.Sitefinity.ContentUsages.Sources;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.ContentUsages
{
  /// <summary>Content usage service</summary>
  internal class ContentUsageService : IContentUsageService
  {
    private static readonly object ContentUsagesCacheSync = new object();

    /// <inheritdoc />
    public IEnumerable<IContentItemUsage> GetContentUsages(
      ContentUsageFilter filter)
    {
      Guard.ArgumentNotNull((object) filter, nameof (filter));
      Guard.ArgumentNotNull((object) filter.ItemId, "itemId");
      Guard.ArgumentNotNullOrEmpty(filter.ItemProvider, "itemProvider");
      Guard.ArgumentNotNullOrEmpty(filter.ItemType, "itemType");
      string key = filter.ItemId.ToString() + filter.ItemProvider + filter.ItemType + (object) filter.ItemTypeFilter;
      if (filter.Culture != null)
        key += filter.Culture.Name;
      ICacheManager cacheManager = SystemManager.GetCacheManager(CacheManagerInstance.Internal);
      if (!(cacheManager[key] is IEnumerable<IContentItemUsage> contentItemUsages1) || contentItemUsages1.Count<IContentItemUsage>() == 0)
      {
        lock (ContentUsageService.ContentUsagesCacheSync)
        {
          if (cacheManager[key] is IEnumerable<IContentItemUsage> contentItemUsages1)
          {
            if (contentItemUsages1.Count<IContentItemUsage>() != 0)
              goto label_17;
          }
          contentItemUsages1 = Enumerable.Empty<IContentItemUsage>();
          foreach (IContentUsageSource contentUsageSource in ObjectFactory.Container.ResolveAll(typeof (IContentUsageSource)))
            contentItemUsages1 = contentItemUsages1.Concat<IContentItemUsage>((IEnumerable<IContentItemUsage>) contentUsageSource.GetContentUsages(filter).ToList<IContentItemUsage>());
          contentItemUsages1 = (IEnumerable<IContentItemUsage>) contentItemUsages1.OrderByDescending<IContentItemUsage, string>((Func<IContentItemUsage, string>) (p => p.LiveUrl));
          contentItemUsages1 = filter.Culture == null ? contentItemUsages1.Distinct<IContentItemUsage>((IEqualityComparer<IContentItemUsage>) new ContentItemSourceEqualityComparer()) : contentItemUsages1.Where<IContentItemUsage>((Func<IContentItemUsage, bool>) (u => u.Culture != null && u.Culture.Name == filter.Culture.Name)).Distinct<IContentItemUsage>((IEqualityComparer<IContentItemUsage>) new ContentItemSourceEqualityComparer()).Union<IContentItemUsage>(contentItemUsages1, (IEqualityComparer<IContentItemUsage>) new ContentItemSourceEqualityComparer());
          cacheManager.Add(key, (object) contentItemUsages1, CacheItemPriority.Normal, (ICacheItemRefreshAction) null, (ICacheItemExpiration) new SlidingTime(TimeSpan.FromMinutes(1.0)));
        }
      }
label_17:
      return filter.Take > 0 ? contentItemUsages1.Skip<IContentItemUsage>(filter.Skip).Take<IContentItemUsage>(filter.Take) : contentItemUsages1;
    }
  }
}
