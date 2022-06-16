// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.RelatedData.IRelatedItemsApplicationState
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.RelatedData
{
  internal interface IRelatedItemsApplicationState
  {
    /// <summary>Generates a cache key</summary>
    /// <param name="itemTypeName">The item`s type full name</param>
    /// <param name="itemProviderName">item`s provider name</param>
    /// <returns>The generated key</returns>
    string GenerateCacheKey(string itemTypeName, string itemProviderName);

    /// <summary>
    /// Sets specified collection of items as a data source into the application state
    /// </summary>
    /// <param name="items">A collection of items</param>
    void SetRelatedItemsDataSource(IEnumerable<IDataItem> items);

    /// <summary>
    /// Sets specified collection of items as a data source into the application state
    /// </summary>
    /// <param name="items">A collection of items</param>
    /// <param name="itemTypeName">Item`s type</param>
    /// <param name="itemProviderName">Item`s provider name</param>
    void SetRelatedItemsDataSource(
      IEnumerable<IDataItem> items,
      string itemTypeName,
      string itemProviderName);

    /// <summary>
    /// Gets the related items. If they exist in the items collection of the application state they are returned from there not from the provider.
    /// </summary>
    /// <param name="item">Item for which to return the related data items</param>
    /// <param name="fieldName">The related data property name</param>
    /// <param name="childItemType">Child item type</param>
    /// <returns>The related data items</returns>
    IQueryable<IDataItem> GetRelatedItems(
      IDataItem item,
      string fieldName,
      Type childItemType);

    /// <summary>
    /// Sets collection of items to the current request context
    /// </summary>
    /// <param name="items">The items</param>
    void SetDataSourceToContext(IEnumerable<IDataItem> items);

    /// <summary>Gets the items</summary>
    Dictionary<string, List<RelatedDataWrapper>> Items { get; }
  }
}
