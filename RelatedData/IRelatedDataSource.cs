// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.RelatedData.IRelatedDataSource
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.RelatedData
{
  /// <summary>Defines base methods for loading related data items.</summary>
  public interface IRelatedDataSource
  {
    /// <summary>Gets the items related to specified item.</summary>
    /// <typeparam name="T">Type of the related items</typeparam>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="itemProviderName">Name of the item provider.</param>
    /// <param name="itemId">The item identifier.</param>
    /// <param name="fieldName">The name of the field.</param>
    /// <param name="status">The status of the item.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="orderExpression">The order expression.</param>
    /// <param name="skip">The number of items to skip.</param>
    /// <param name="take">The number of items to take.</param>
    /// <param name="totalCount">Total count of the items that are filtered by <paramref name="filterExpression" /></param>
    /// <param name="relationDirection">The relation direction - child or parent.</param>
    /// <returns>Returns a collection of related items.</returns>
    IQueryable<T> GetRelatedItems<T>(
      string itemType,
      string itemProviderName,
      Guid itemId,
      string fieldName,
      ContentLifecycleStatus? status,
      string filterExpression,
      string orderExpression,
      int? skip,
      int? take,
      ref int? totalCount,
      RelationDirection relationDirection = RelationDirection.Child)
      where T : IDataItem;

    /// <summary>Gets the items related to specified item.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="itemProviderName">Name of the item provider.</param>
    /// <param name="itemId">The item identifier.</param>
    /// <param name="fieldName">The name of the field.</param>
    /// <param name="relatedItemsType">Type of the related items.</param>
    /// <param name="status">The status of the item.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="orderExpression">The order expression.</param>
    /// <param name="skip">The number of items to skip.</param>
    /// <param name="take">The number of items to take.</param>
    /// <param name="totalCount">Total count of the items that are filtered by <paramref name="filterExpression" /></param>
    /// <param name="relationDirection">The relation direction - child or parent.</param>
    /// <returns>Returns a collection of related items.</returns>
    IQueryable GetRelatedItems(
      string itemType,
      string itemProviderName,
      Guid itemId,
      string fieldName,
      Type relatedItemsType,
      ContentLifecycleStatus? status,
      string filterExpression,
      string orderExpression,
      int? skip,
      int? take,
      ref int? totalCount,
      RelationDirection relationDirection = RelationDirection.Child);

    /// <summary>Gets the items related to collection of parent items</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="itemProviderName">Name of the item provider.</param>
    /// <param name="parentItemIds">The ids of the parent items</param>
    /// <param name="fieldName">The name of the field.</param>
    /// <param name="relatedItemsType">Type of the related items.</param>
    /// <param name="status">The status of the item.</param>
    /// <returns>Returns a collection with ordered related items by parent item id.</returns>
    Dictionary<Guid, List<IDataItem>> GetRelatedItems(
      string itemType,
      string itemProviderName,
      List<Guid> parentItemIds,
      string fieldName,
      Type relatedItemsType,
      ContentLifecycleStatus? status);

    /// <summary>
    /// Gets the items related to collection of parent items as a list
    /// Should be used in case the related items are from multiple providers
    /// </summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="itemProviderName">Name of the item provider.</param>
    /// <param name="parentItemIds">The ids of the parent items</param>
    /// <param name="fieldName">The name of the field.</param>
    /// <param name="relatedItemsType">Type of the related items.</param>
    /// <param name="status">The status of the item.</param>
    /// <returns>Returns a collection of related items by parent item ids.</returns>
    IEnumerable<IDataItem> GetRelatedItemsList(
      string itemType,
      string itemProviderName,
      Collection<Guid> parentItemIds,
      string fieldName,
      Type relatedItemsType,
      ContentLifecycleStatus? status);
  }
}
