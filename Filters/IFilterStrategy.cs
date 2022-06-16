// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Filters.IFilterStrategy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Telerik.Sitefinity.Filters
{
  /// <summary>Represents a strategy for filtering items</summary>
  internal interface IFilterStrategy
  {
    /// <summary>
    /// Gets all filters registered for specific item type from this strategy
    /// </summary>
    /// <param name="itemType">The item type</param>
    /// <param name="providerName">The provider name</param>
    /// <param name="culture">The culture</param>
    /// <returns>Available filters</returns>
    IEnumerable<FilterItem> GetFilters(
      Type itemType,
      string providerName,
      CultureInfo culture);

    /// <summary>
    /// Applies specific filter if this filter is registered from this strategy and returns the filtered items IDs
    /// </summary>
    /// <param name="filter">The filter</param>
    /// <param name="itemType">The item type</param>
    /// <param name="providerName">The provider name</param>
    /// <param name="culture">The culture</param>
    /// <param name="parameters">The parameters</param>
    /// <param name="filteredItemsIDs">Collection of filtered items IDs</param>
    /// <returns>True if this strategy can filter by the filter provided.</returns>
    bool TryToFilterBy(
      string filter,
      Type itemType,
      string providerName,
      CultureInfo culture,
      IEnumerable<string> parameters,
      out IEnumerable<Guid> filteredItemsIDs);

    bool TryToFilterByQuery(
      string filter,
      Type itemType,
      string providerName,
      CultureInfo culture,
      IEnumerable<string> parameters,
      IQueryable query,
      out IQueryable resultQuery);

    IEnumerable<Result> GetValues(
      string filter,
      Type itemType,
      string providerName,
      CultureInfo culture,
      int skip,
      int take,
      string search,
      ISet<string> parameters,
      ref int? totalCount);
  }
}
