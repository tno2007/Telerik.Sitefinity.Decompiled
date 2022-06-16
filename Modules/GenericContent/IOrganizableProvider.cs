// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.IOrganizableProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;

namespace Telerik.Sitefinity.Modules.GenericContent
{
  /// <summary>
  /// This interface must be implemented by all providers that support types that use classifications.
  /// </summary>
  public interface IOrganizableProvider
  {
    /// <summary>Gets the items by taxon.</summary>
    /// <param name="taxonId">The taxon id.</param>
    /// <param name="isSingleTaxon">A value indicating if it is a single taxon.</param>
    /// <param name="propertyName">Name of the property.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="orderExpression">The order expression.</param>
    /// <param name="skip">Items to skip.</param>
    /// <param name="take">Items to take.</param>
    /// <param name="totalCount">The total count.</param>
    /// <returns>IEnumerable of items that are marked with a specified taxon.</returns>
    IEnumerable GetItemsByTaxon(
      Guid taxonId,
      bool isSingleTaxon,
      string propertyName,
      Type itemType,
      string filterExpression,
      string orderExpression,
      int skip,
      int take,
      ref int? totalCount);
  }
}
