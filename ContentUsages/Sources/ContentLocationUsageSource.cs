// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContentUsages.Sources.ContentLocationUsageSource
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.ContentLocations.Web.Services;
using Telerik.Sitefinity.ContentUsages.Filters;
using Telerik.Sitefinity.ContentUsages.Model;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.ContentUsages.Sources
{
  internal class ContentLocationUsageSource : IContentUsageSource
  {
    public IEnumerable<IContentItemUsage> GetContentUsages(
      ContentUsageFilter filter)
    {
      IEnumerable<IContentItemUsage> contentUsages = Enumerable.Empty<IContentItemUsage>();
      switch (filter.ItemTypeFilter)
      {
        case ItemTypeFilter.All:
        case ItemTypeFilter.Pages:
          using (new CultureRegion(filter.Culture))
          {
            if (!string.IsNullOrEmpty(filter.ItemType))
            {
              Guid itemId = filter.ItemId;
              object item;
              contentUsages = (IEnumerable<IContentItemUsage>) ContentItemLocationService.GetFilteredContentItemLocations(TypeResolutionService.ResolveType(filter.ItemType), filter.ItemProvider, itemId, out item, ifAccessible: true).Select<IContentLocation, ContentItemUsage>((Func<IContentLocation, ContentItemUsage>) (location => new ContentItemUsage()
              {
                Culture = location.Culture,
                ItemId = location.PageId,
                ItemProvider = ManagerBase<PageDataProvider>.GetDefaultProviderName(),
                ItemType = typeof (PageNode).FullName,
                LiveUrl = location.GetUrl(item)
              }));
              break;
            }
            break;
          }
      }
      return contentUsages;
    }
  }
}
