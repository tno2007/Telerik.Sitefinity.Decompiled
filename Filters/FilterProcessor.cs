// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Filters.FilterProcessor
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Filters
{
  internal class FilterProcessor
  {
    private IList<SimpleFilter> filters;
    private IList<SimpleFilter> notProcessedFilters;
    private IDictionary<string, IFilterStrategy> filterMap;
    private Type itemType;
    private string providerName;
    private CultureInfo culture;

    public FilterProcessor(string name, Type itemType, string providerName, CultureInfo culture)
    {
      this.itemType = itemType;
      this.providerName = providerName;
      this.culture = culture;
      this.filters = this.GetFilters(name);
      this.filterMap = this.GetFilterMap(this.itemType, this.providerName, this.culture);
    }

    public bool HasFiltersById => this.notProcessedFilters != null && this.notProcessedFilters.Count > 0;

    public IQueryable ApplyQuery(IQueryable source)
    {
      List<SimpleFilter> simpleFilterList = new List<SimpleFilter>();
      IDictionary<string, IFilterStrategy> filterMap = this.GetFilterMap(this.itemType, this.providerName, this.culture);
      foreach (SimpleFilter filter1 in (IEnumerable<SimpleFilter>) this.filters)
      {
        if (filterMap.ContainsKey(filter1.Name))
        {
          IFilterStrategy filter2 = this.filterMap[filter1.Name];
          IQueryable queryable = (IQueryable) null;
          string name = filter1.Name;
          Type itemType = this.itemType;
          string providerName = this.providerName;
          CultureInfo culture = this.culture;
          IEnumerable<string> parameters = filter1.Parameters;
          IQueryable query = source;
          ref IQueryable local = ref queryable;
          if (filter2.TryToFilterByQuery(name, itemType, providerName, culture, parameters, query, out local))
            source = queryable.Cast(this.itemType);
          else
            simpleFilterList.Add(filter1);
        }
      }
      this.notProcessedFilters = (IList<SimpleFilter>) simpleFilterList;
      return source;
    }

    public IQueryable ApplyContains(IQueryable query)
    {
      IList<Guid> guidList = (IList<Guid>) null;
      foreach (SimpleFilter notProcessedFilter in (IEnumerable<SimpleFilter>) this.notProcessedFilters)
      {
        IFilterStrategy filter = this.filterMap[notProcessedFilter.Name];
        IEnumerable<Guid> guids = (IEnumerable<Guid>) null;
        string name = notProcessedFilter.Name;
        Type itemType = this.itemType;
        string providerName = this.providerName;
        CultureInfo culture = this.culture;
        IEnumerable<string> parameters = notProcessedFilter.Parameters;
        ref IEnumerable<Guid> local = ref guids;
        if (filter.TryToFilterBy(name, itemType, providerName, culture, parameters, out local))
        {
          guidList = guidList != null ? (IList<Guid>) guidList.Intersect<Guid>(guids).ToList<Guid>() : (IList<Guid>) guids.ToList<Guid>();
          if (guidList.Count == 0)
            break;
        }
      }
      if (guidList != null)
      {
        if (guidList.Count > 0)
        {
          List<Guid> idList = guidList.ToList<Guid>();
          query = (IQueryable) Queryable.Cast<IDataItem>(query).Where<IDataItem>((Expression<Func<IDataItem, bool>>) (i => idList.Contains(i.Id)));
        }
        else
          query = (IQueryable) Enumerable.Empty<object>().AsQueryable<object>();
        query = query.Cast(this.itemType);
      }
      return query;
    }

    public IEnumerable<Result> GetFilterValues(
      string skip,
      string take,
      string search,
      ref int? totalCount)
    {
      int result = 0;
      int skip1 = 0;
      if (int.TryParse(skip, out result))
        skip1 = result;
      int take1 = 20;
      if (int.TryParse(take, out result))
        take1 = result;
      SimpleFilter simpleFilter = this.filters.First<SimpleFilter>();
      IFilterStrategy filterStrategy;
      return this.filterMap.TryGetValue(simpleFilter.Name, out filterStrategy) ? filterStrategy.GetValues(simpleFilter.Name, this.itemType, this.providerName, this.culture, skip1, take1, search, (ISet<string>) new HashSet<string>(simpleFilter.Parameters), ref totalCount) : (IEnumerable<Result>) null;
    }

    private IDictionary<string, IFilterStrategy> GetFilterMap(
      Type itemType,
      string providerName,
      CultureInfo culture)
    {
      IEnumerable<IFilterStrategy> filterStrategies = ObjectFactory.Container.ResolveAll(typeof (IFilterStrategy)).Cast<IFilterStrategy>();
      Dictionary<string, IFilterStrategy> filterMap = new Dictionary<string, IFilterStrategy>();
      foreach (IFilterStrategy filterStrategy in filterStrategies)
      {
        foreach (string key in filterStrategy.GetFilters(itemType, providerName, culture).Select<FilterItem, string>((Func<FilterItem, string>) (x => x.Name)).ToList<string>())
          filterMap.Add(key, filterStrategy);
      }
      return (IDictionary<string, IFilterStrategy>) filterMap;
    }

    public IList<SimpleFilter> GetFilters(string name)
    {
      List<SimpleFilter> filters = new List<SimpleFilter>();
      string str1 = name;
      char[] separator = new char[1]{ ';' };
      foreach (string str2 in str1.Split(separator, StringSplitOptions.RemoveEmptyEntries))
      {
        char[] chArray = new char[1]{ ',' };
        string[] source = str2.Split(chArray);
        string name1 = source[0];
        IEnumerable<string> parameters = ((IEnumerable<string>) source).Skip<string>(1);
        filters.Add(new SimpleFilter(name1, parameters));
      }
      return (IList<SimpleFilter>) filters;
    }
  }
}
