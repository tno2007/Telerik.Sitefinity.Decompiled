// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.ICollectionFacade`2
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;

namespace Telerik.Sitefinity.Fluent
{
  /// <summary>
  /// Base contract for all facades which works with collections.
  /// </summary>
  /// <typeparam name="TFacade">The type of the facade.</typeparam>
  /// <typeparam name="TDataItem">The type of the data item.</typeparam>
  public interface ICollectionFacade<TFacade, TDataItem> : IFacade<TFacade>
  {
    /// <summary>
    /// Gets the count of items in collection at current facade.
    /// </summary>
    /// <param name="result">The count of items.</param>
    /// <returns>An instance of the current <typeparamref name="TFacade" />.</returns>
    TFacade Count(out int result);

    /// <summary>
    /// Performs an arbitrary action for each item of collection at facade.
    /// </summary>
    /// <param name="action">An action to be performed for each item of collection.</param>
    /// <returns>An instance of the current <typeparamref name="TFacade" />.</returns>
    TFacade ForEach(Action<TDataItem> action);

    /// <summary>
    /// Gets query with instances of type &lt;typeparam name="TDataItem"&gt;.
    /// </summary>
    /// <returns>An instance of IQueryable[TDataItem] object. </returns>
    IQueryable<TDataItem> Get();

    /// <summary>
    /// Orders the items of collection in ascending order with keys specified with keySelector parameter.
    /// </summary>
    /// <param name="keySelector">The key selector.</param>
    /// <returns>An instance of the current <typeparamref name="TFacade" />.</returns>
    TFacade OrderBy<TKey>(Expression<Func<TDataItem, TKey>> keySelector);

    /// <summary>
    /// Orders the items of collection in descending order with keys specified with keySelector parameter.
    /// </summary>
    /// <param name="keySelector">The key selector.</param>
    /// <returns>An instance of the current <typeparamref name="TFacade" />.</returns>
    TFacade OrderByDescending<TKey>(Expression<Func<TDataItem, TKey>> keySelector);

    /// <summary>
    /// Sets the collection with items filtered with query parameter.
    /// </summary>
    /// <param name="query">The query to filter the items.</param>
    /// <returns>An instance of the current <typeparamref name="TFacade" />.</returns>
    TFacade Set(IQueryable<TDataItem> query);

    /// <summary>Skips the items of collection with specified count.</summary>
    /// <param name="count">The count.</param>
    /// <returns>An instance of the current <typeparamref name="TFacade" />.</returns>
    TFacade Skip(int count);

    /// <summary>
    /// Takes items from collection number of which is specified with the count parameter.
    /// </summary>
    /// <param name="count">The count.</param>
    /// <returns>An instance of the current <typeparamref name="TFacade" />.</returns>
    TFacade Take(int count);

    /// <summary>
    /// Filters items of the collection by specified where clause at predicate parameter.
    /// </summary>
    /// <param name="predicate">The predicate to filter by.</param>
    /// <returns>An instance of the current <typeparamref name="TFacade" />.</returns>
    TFacade Where(Func<TDataItem, bool> predicate);
  }
}
