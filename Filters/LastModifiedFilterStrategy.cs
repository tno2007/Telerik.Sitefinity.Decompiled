// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Filters.LastModifiedFilterStrategy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent;

namespace Telerik.Sitefinity.Filters
{
  internal class LastModifiedFilterStrategy : IFilterStrategy
  {
    public IEnumerable<FilterItem> GetFilters(
      Type itemType,
      string providerName,
      CultureInfo culture)
    {
      List<FilterItem> filters = new List<FilterItem>();
      if (typeof (IDataItem).IsAssignableFrom(itemType))
        filters.Add(new FilterItem()
        {
          Name = "last-modified",
          Title = string.Format(Res.Get<Labels>().FilterTitle, (object) Res.Get<ContentResources>().DateModified),
          Parameters = new FilterParameters((IDictionary<string, object>) new Dictionary<string, object>()
          {
            {
              "Type",
              (object) "date-range"
            },
            {
              "ContentSingularName",
              (object) Res.Get<ContentResources>().DateRange
            },
            {
              "ContentPluralName",
              (object) Res.Get<ContentResources>().DateRange
            }
          })
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
      IQueryable<IDataItem> source = Queryable.Cast<IDataItem>(query);
      List<string> list = parameters.ToList<string>();
      DateTime startDate = DateTime.Parse(list[0]).ToUniversalTime();
      DateTime endDate = DateTime.Parse(list[1]).ToUniversalTime();
      IQueryable<IDataItem> queryable = source.Where<IDataItem>((Expression<Func<IDataItem, bool>>) (x => startDate <= x.LastModified && x.LastModified <= endDate));
      resultQuery = (IQueryable) queryable;
      return true;
    }
  }
}
