// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.PublishingPointDynamicTypeProviderBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Linq;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.DynamicTypes.Model;

namespace Telerik.Sitefinity.Publishing
{
  public abstract class PublishingPointDynamicTypeProviderBase : DataProviderBase
  {
    private static Type[] knownTypes;

    /// <summary>Creates the data item.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <returns></returns>
    public abstract object CreateDataItem(string itemType);

    /// <summary>Creates the data item.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="id">The id.</param>
    /// <returns></returns>
    public abstract object CreateDataItem(string itemType, Guid id);

    /// <summary>Deletes the data item.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="dataItemID">The data item ID.</param>
    public abstract void DeleteDataItem(string itemType, Guid dataItemID);

    /// <summary>Deletes the data item.</summary>
    /// <param name="item">The item.</param>
    public abstract void DeleteDataItem(object item);

    /// <summary>Gets the data item.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="dataItemID">The data item ID.</param>
    /// <returns></returns>
    public abstract object GetDataItem(string itemType, Guid dataItemID);

    /// <summary>Gets the data items.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <returns></returns>
    public abstract IQueryable GetDataItems(string itemType);

    /// <summary>Gets the data items.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="originalItemId">The original item id.</param>
    /// <returns></returns>
    public abstract IQueryable GetDataItems(string itemType, Guid originalItemId);

    /// <summary>Gets the children data items.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="parentId">The parent id.</param>
    /// <returns></returns>
    public abstract IQueryable GetChildrenDataItems(string itemType, Guid parentId);

    /// <summary>Ensures the dynamic types resolution.</summary>
    protected internal virtual void EnsureDynamicTypesResolution()
    {
    }

    /// <summary>Create an item of a specific type, if it is supported</summary>
    /// <param name="itemType">Type of the item</param>
    /// <param name="id">ID of the item to create</param>
    /// <returns>Created item</returns>
    /// <exception cref="T:System.ArgumentException">If <paramref name="itemType" /> is not supported by this manager.</exception>
    /// <exception cref="T:System.NotSupportedException">If you try to create a dynamic type without specifying a publishing point.</exception>
    public override object CreateItem(Type itemType, Guid id)
    {
      if (typeof (DynamicTypeBase).IsAssignableFrom(itemType))
        return this.CreateDataItem(itemType.FullName, id);
      throw DataProviderBase.GetInvalidItemTypeException(itemType, this.GetKnownTypes());
    }

    /// <summary>Get a queryable of items of the specified type</summary>
    /// <param name="itemType">Type to get queryables for.</param>
    /// <param name="filterExpression">Filter expression in Dynamic Linq.</param>
    /// <param name="orderExpression">Sorting expression in Dynamic Linq.</param>
    /// <param name="skip">Used for paging: how many items to skip from the start.</param>
    /// <param name="take">Used for paging: the maximum number of items to take.</param>
    /// <param name="totalCount">Total count of the items that are filtered by <paramref name="filterExpression" /></param>
    /// <returns>Queryable of items</returns>
    /// <remarks>
    /// Whether the result is queryable or not depends on the provider. However,
    /// Sitefinity's providers return IQueryable-s.
    /// </remarks>
    /// 
    ///             /// <exception cref="T:System.ArgumentException">If <paramref name="itemType" /> is not supported by this manager.</exception>
    /// <exception cref="T:System.NotSupportedException">If you try to create a dynamic type without specifying a publishing point.</exception>
    public override IEnumerable GetItems(
      Type itemType,
      string filterExpression,
      string orderExpression,
      int skip,
      int take,
      ref int? totalCount)
    {
      if (!(itemType == typeof (DynamicTypeBase)))
        throw DataProviderBase.GetInvalidItemTypeException(itemType, this.GetKnownTypes());
      IQueryable source = this.GetDataItems(itemType.FullName).Where(filterExpression).Skip(skip).Take(take).OrderBy(orderExpression);
      totalCount = new int?(source.Count());
      return (IEnumerable) source;
    }

    /// <summary>Delete an item</summary>
    /// <param name="item">Item to delete</param>
    /// <exception cref="T:System.ArgumentException">If <paramref name="item" />'s type is not supported by this manager.</exception>
    /// <exception cref="T:System.NotSupportedException">If you try to create a dynamic type without specifying a publishing point.</exception>
    public override void DeleteItem(object item) => this.DeleteDataItem(item);

    /// <summary>Get a list of types served by this manager</summary>
    /// <returns>Array of Type-s that can be used in the generic IManager methods as arguments.</returns>
    public override Type[] GetKnownTypes()
    {
      if (PublishingPointDynamicTypeProviderBase.knownTypes == null)
        PublishingPointDynamicTypeProviderBase.knownTypes = new Type[2]
        {
          typeof (DynamicTypeBase),
          typeof (object)
        };
      return PublishingPointDynamicTypeProviderBase.knownTypes;
    }

    /// <summary>Gets a unique key for each data provider base.</summary>
    /// <value></value>
    public override string RootKey => "PublishingDataProviderBase";
  }
}
