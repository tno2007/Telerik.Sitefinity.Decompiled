// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.Web.Services.ContentItemFilterStrategy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Filters;
using Telerik.Sitefinity.GenericContent.Model;

namespace Telerik.Sitefinity.Modules.GenericContent.Web.Services
{
  internal class ContentItemFilterStrategy : IFilterStrategy
  {
    private const string NotUsedInPagesOrTemplatesKey = "Not_Used_In_Pages_Or_Templates";
    private const string NotUsedInPagesOrTemplatesTitle = "Not used in pages or templates";

    public IEnumerable<FilterItem> GetFilters(
      Type itemType,
      string providerName,
      CultureInfo culture)
    {
      List<FilterItem> filters = new List<FilterItem>();
      if (typeof (ContentItem).IsAssignableFrom(itemType))
        filters.Add(new FilterItem()
        {
          Name = "Not_Used_In_Pages_Or_Templates",
          Title = "Not used in pages or templates"
        });
      return (IEnumerable<FilterItem>) filters;
    }

    public IEnumerable<Result> GetValues(
      string filter,
      Type itemType,
      string providerName,
      CultureInfo culture,
      int skip,
      int take,
      string search,
      ISet<string> parameters,
      ref int? totalCount)
    {
      return Enumerable.Empty<Result>();
    }

    public bool TryToFilterBy(
      string filter,
      Type itemType,
      string providerName,
      CultureInfo culture,
      IEnumerable<string> parameters,
      out IEnumerable<Guid> filteredItemsIDs)
    {
      filteredItemsIDs = (IEnumerable<Guid>) null;
      return false;
    }

    public bool TryToFilterByQuery(
      string filter,
      Type itemType,
      string providerName,
      CultureInfo culture,
      IEnumerable<string> parameters,
      IQueryable query,
      out IQueryable resultQuery)
    {
      resultQuery = (IQueryable) null;
      if (typeof (ContentItem).IsAssignableFrom(itemType) && filter == "Not_Used_In_Pages_Or_Templates")
      {
        ContentManager manager = ContentManager.GetManager(providerName);
        List<Guid> originalContentIdsOfLiveItems = manager.GetContentNotUsedOnAnyPage(providerName).Select<ContentItem, Guid>((Expression<Func<ContentItem, Guid>>) (x => x.OriginalContentId)).ToList<Guid>();
        resultQuery = (IQueryable) manager.GetContent().Where<ContentItem>((Expression<Func<ContentItem, bool>>) (x => (int) x.Status == 0 && originalContentIdsOfLiveItems.Contains(x.Id)));
      }
      return resultQuery != null;
    }
  }
}
