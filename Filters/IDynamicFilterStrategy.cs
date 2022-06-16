// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Filters.IDynamicFilterStrategy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;

namespace Telerik.Sitefinity.Filters
{
  /// <summary>Represents a strategy for getting dynamic filters</summary>
  internal interface IDynamicFilterStrategy : IFilterStrategy
  {
    /// <summary>
    /// Gets all dynamic filters registered for specific item type from this strategy
    /// </summary>
    /// <param name="itemType">The item type</param>
    /// <param name="providerName">The provider name</param>
    /// <param name="culture">The culture</param>
    /// <param name="parentId">The parent id</param>
    /// <returns>Available filters</returns>
    IEnumerable<FilterItem> GetDynamicFilters(
      Type itemType,
      string providerName,
      CultureInfo culture,
      Guid? parentId);
  }
}
