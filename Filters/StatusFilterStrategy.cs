// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Filters.StatusFilterStrategy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Workflow;

namespace Telerik.Sitefinity.Filters
{
  internal class StatusFilterStrategy : IFilterStrategy
  {
    public IEnumerable<FilterItem> GetFilters(
      Type itemType,
      string providerName,
      CultureInfo culture)
    {
      foreach (IFilterStrategy filterStrategy in ObjectFactory.Container.ResolveAll(typeof (IFilterStrategy)).Cast<IFilterStrategy>())
      {
        if (!(filterStrategy.GetType() == typeof (StatusFilterStrategy)) && filterStrategy.GetFilters(itemType, providerName, culture).Where<FilterItem>((Func<FilterItem, bool>) (x => x.Parameters == null && x.IsStatus)).Count<FilterItem>() > 0)
          return (IEnumerable<FilterItem>) new FilterItem[1]
          {
            new FilterItem()
            {
              Name = "predefined",
              Title = string.Format(Res.Get<Labels>().FilterTitle, (object) Res.Get<WorkflowResources>().Status),
              Parameters = new FilterParameters((IDictionary<string, object>) new Dictionary<string, object>()
              {
                {
                  "Type",
                  (object) "call"
                },
                {
                  "ContentSingularName",
                  (object) Res.Get<WorkflowResources>().Status
                },
                {
                  "ContentPluralName",
                  (object) Res.Get<WorkflowResources>().Status
                }
              })
            }
          };
      }
      return Enumerable.Empty<FilterItem>();
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
      IEnumerable<FilterItem> source = ObjectFactory.Container.ResolveAll(typeof (IFilterStrategy)).Cast<IFilterStrategy>().SelectMany<IFilterStrategy, FilterItem>((Func<IFilterStrategy, IEnumerable<FilterItem>>) (x => x.GetFilters(itemType, providerName, culture))).Where<FilterItem>((Func<FilterItem, bool>) (x => x.Parameters == null && x.IsStatus));
      if (!string.IsNullOrEmpty(search))
        source = (IEnumerable<FilterItem>) source.Where<FilterItem>((Func<FilterItem, bool>) (x => x.Title.IndexOf(search, StringComparison.OrdinalIgnoreCase) == 0 || x.Title.IndexOf(string.Format(" {0}", (object) search), StringComparison.OrdinalIgnoreCase) >= 0)).OrderBy<FilterItem, string>((Func<FilterItem, string>) (x => x.Title));
      if (parameters.Count > 0)
        source = source.Where<FilterItem>((Func<FilterItem, bool>) (x => parameters.Contains(x.Name)));
      if (totalCount.HasValue)
        totalCount = new int?(source.Count<FilterItem>());
      return source.Skip<FilterItem>(skip).Take<FilterItem>(take).Select<FilterItem, Result>((Func<FilterItem, Result>) (x => new Result(x.Name, x.Title)));
    }

    public bool TryToFilterBy(
      string filter,
      Type itemType,
      string providerName,
      CultureInfo culture,
      IEnumerable<string> parameters,
      out IEnumerable<Guid> filteredItemsIDs)
    {
      IEnumerable<IFilterStrategy> source = ObjectFactory.Container.ResolveAll(typeof (IFilterStrategy)).Cast<IFilterStrategy>();
      HashSet<Guid> guidSet = new HashSet<Guid>();
      foreach (string parameter in parameters)
      {
        string subFilter = parameter;
        IFilterStrategy filterStrategy = source.FirstOrDefault<IFilterStrategy>((Func<IFilterStrategy, bool>) (x => x.GetFilters(itemType, providerName, culture).Any<FilterItem>((Func<FilterItem, bool>) (y => y.Name == subFilter))));
        if (filterStrategy != null)
        {
          IEnumerable<Guid> filteredItemsIDs1 = (IEnumerable<Guid>) null;
          if (filterStrategy.TryToFilterBy(subFilter, itemType, providerName, culture, Enumerable.Empty<string>(), out filteredItemsIDs1))
          {
            foreach (Guid guid in filteredItemsIDs1)
              guidSet.Add(guid);
          }
        }
      }
      filteredItemsIDs = (IEnumerable<Guid>) guidSet;
      return true;
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
      return false;
    }
  }
}
