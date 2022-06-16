// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.QueryBuilder.QueryDataProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Web.Model;

namespace Telerik.Sitefinity.Data.QueryBuilder
{
  /// <summary>
  /// Represents a data provider for persisting queries built using the QueryBuilder control
  /// </summary>
  public abstract class QueryDataProvider : DataProviderBase
  {
    /// <summary>Creates a new query</summary>
    /// <returns></returns>
    public virtual Query CreateQuery() => this.CreateQuery(this.GetNewGuid());

    /// <summary>Creates a new query with the specified ID</summary>
    /// <param name="pageId"></param>
    /// <returns></returns>
    public abstract Query CreateQuery(Guid id);

    /// <summary>Get a query by ID</summary>
    /// <param name="pageId">The ID of the query to get</param>
    /// <returns></returns>
    public abstract Query GetQuery(Guid id);

    /// <summary>Get all queries</summary>
    /// <returns></returns>
    public abstract IQueryable<Query> GetQueries();

    /// <summary>Gets queries for a specified persistent type</summary>
    /// <param name="persistentTypeName"></param>
    /// <returns></returns>
    public abstract IQueryable<Query> GetQueries(string persistentTypeName);

    /// <summary>Delete a query</summary>
    /// <param name="query">The query to delete</param>
    public abstract void DeleteQuery(Query query);

    /// <summary>Get a list of types served by this manager</summary>
    /// <returns></returns>
    public override Type[] GetKnownTypes() => new Type[1]
    {
      typeof (Query)
    };

    /// <summary>Gets a unique key for each data provider base.</summary>
    /// <value></value>
    public override string RootKey => nameof (QueryDataProvider);

    /// <summary>
    /// Marks the provided persistent item for deletion.
    /// The item is deleted form the storage when the transaction is committed.
    /// </summary>
    /// <param name="item">The item to be deleted.</param>
    public override void DeleteItem(object item)
    {
      if (item == null)
        throw new ArgumentNullException(nameof (item));
      if (item.GetType() == typeof (Query))
        this.DeleteQuery((Query) item);
      else
        throw DataProviderBase.GetInvalidItemTypeException(item.GetType(), typeof (Query));
    }

    public override IEnumerable GetItems(
      Type itemType,
      string filterExpression,
      string orderExpression,
      int skip,
      int take,
      ref int? totalCount)
    {
      if (itemType == (Type) null)
        throw new ArgumentNullException(nameof (itemType));
      if (itemType == typeof (Query))
        return (IEnumerable) DataProviderBase.SetExpressions<Query>(this.GetQueries(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      throw DataProviderBase.GetInvalidItemTypeException(itemType, typeof (Query));
    }

    /// <summary>
    /// Gets the data item with the specified ID.
    /// An exception should be thrown if an item with the specified ID does not exist.
    /// </summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="pageId">The ID of the item to return.</param>
    /// <returns></returns>
    public override object GetItem(Type itemType, Guid id)
    {
      if (itemType == (Type) null)
        throw new ArgumentNullException(nameof (itemType));
      if (itemType == typeof (Query))
        return (object) this.GetQuery(id);
      throw DataProviderBase.GetInvalidItemTypeException(itemType, typeof (Query));
    }

    /// <summary>
    /// Get item by primary key without throwing exceptions if it doesn't exist
    /// </summary>
    /// <param name="itemType">Type of the item to get</param>
    /// <param name="id">Primary key</param>
    /// <returns>Item or default value</returns>
    public override object GetItemOrDefault(Type itemType, Guid id)
    {
      if (itemType == (Type) null)
        throw new ArgumentNullException(nameof (itemType));
      if (!(itemType == typeof (Query)))
        return base.GetItem(itemType, id);
      return (object) this.GetQueries().Where<Query>((Expression<Func<Query, bool>>) (q => q.Id == id)).FirstOrDefault<Query>();
    }

    /// <summary>Creates new data item.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="pageId">The pageId.</param>
    /// <returns></returns>
    public override object CreateItem(Type itemType, Guid id)
    {
      if (itemType == (Type) null)
        throw new ArgumentNullException(nameof (itemType));
      if (itemType == typeof (Query))
        return (object) this.CreateQuery(id);
      throw DataProviderBase.GetInvalidItemTypeException(itemType, typeof (Query));
    }
  }
}
