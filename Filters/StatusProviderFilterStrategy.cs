// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Filters.StatusProviderFilterStrategy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Filters
{
  internal class StatusProviderFilterStrategy : IFilterStrategy
  {
    /// <inheritdoc />
    public bool TryToFilterBy(
      string filter,
      Type itemType,
      string providerName,
      CultureInfo culture,
      IEnumerable<string> parameters,
      out IEnumerable<Guid> filteredItemsIDs)
    {
      string[] strArray = filter.Split('_');
      if (strArray.Length == 2)
      {
        IStatusProvider provider = SystemManager.StatusProviderRegistry.GetProvider(strArray[0]);
        if (provider != null)
        {
          filteredItemsIDs = (IEnumerable<Guid>) provider.GetItemsByFilter(itemType, providerName, culture, (string) null, strArray[1]).Select<IItemStatusData, Guid>((Func<IItemStatusData, Guid>) (s => s.ItemId)).ToArray<Guid>();
          return true;
        }
      }
      filteredItemsIDs = (IEnumerable<Guid>) new List<Guid>();
      return false;
    }

    /// <inheritdoc />
    public IEnumerable<FilterItem> GetFilters(
      Type itemType,
      string providerName,
      CultureInfo culture)
    {
      List<FilterItem> filters1 = new List<FilterItem>();
      foreach (IStatusProvider provider in SystemManager.StatusProviderRegistry.GetProviders())
      {
        if (!(itemType != (Type) null) || provider.IsTypeSupported(itemType))
        {
          IEnumerable<IStatusFilter> filters2 = provider.GetFilters();
          if (filters2 != null)
          {
            foreach (IStatusFilter statusFilter in filters2)
              filters1.Add(new FilterItem()
              {
                Name = provider.Name + "_" + statusFilter.Key,
                Title = statusFilter.Title,
                IsStatus = true
              });
          }
        }
      }
      return (IEnumerable<FilterItem>) filters1;
    }

    /// <inheritdoc />
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
  }
}
