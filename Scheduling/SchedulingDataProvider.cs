// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Scheduling.SchedulingDataProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Scheduling.Model;
using Telerik.Sitefinity.SyncLock;

namespace Telerik.Sitefinity.Scheduling
{
  /// <summary>Base provider for the scheduling module</summary>
  public abstract class SchedulingDataProvider : DataProviderBase, ISyncLockSupport
  {
    private static readonly Type[] knownDataTypes = new Type[1]
    {
      typeof (ScheduledTaskData)
    };

    /// <summary>Creates new data item.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="id">The id.</param>
    /// <returns></returns>
    public override object CreateItem(Type itemType, Guid id) => itemType == typeof (ScheduledTaskData) ? (object) this.CreateTaskData(id) : base.CreateItem(itemType, id);

    /// <summary>
    /// Gets the data item with the specified ID.
    /// An exception should be thrown if an item with the specified ID does not exist.
    /// </summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="id">The ID of the item to return.</param>
    /// <returns></returns>
    public override object GetItem(Type itemType, Guid id) => itemType == typeof (ScheduledTaskData) ? (object) this.GetTaskData(id) : base.GetItem(itemType, id);

    /// <summary>
    /// Get item by primary key without throwing exceptions if it doesn't exist
    /// </summary>
    /// <param name="itemType">Type of the item to get</param>
    /// <param name="id">Primary key</param>
    /// <returns>Item or default value</returns>
    public override object GetItemOrDefault(Type itemType, Guid id)
    {
      if (!(itemType == typeof (ScheduledTaskData)))
        return base.GetItem(itemType, id);
      return (object) this.GetTaskData().Where<ScheduledTaskData>((Expression<Func<ScheduledTaskData, bool>>) (t => t.Id == id)).FirstOrDefault<ScheduledTaskData>();
    }

    /// <summary>Gets the items.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="orderExpression">The order expression.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <param name="totalCount">Total count of the items that are filtered by <paramref name="filterExpression" /></param>
    /// <returns></returns>
    public override IEnumerable GetItems(
      Type itemType,
      string filterExpression,
      string orderExpression,
      int skip,
      int take,
      ref int? totalCount)
    {
      return itemType == typeof (ScheduledTaskData) ? (IEnumerable) DataProviderBase.SetExpressions<ScheduledTaskData>(this.GetTaskData(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount) : base.GetItems(itemType, filterExpression, orderExpression, skip, take, ref totalCount);
    }

    /// <summary>
    /// Marks the provided persistent item for deletion.
    /// The item is deleted form the storage when the transaction is committed.
    /// </summary>
    /// <param name="item">The item to be deleted.</param>
    public override void DeleteItem(object item)
    {
      if (item != null && item.GetType() == typeof (ScheduledTaskData))
        this.DeleteTaskData((ScheduledTaskData) item);
      else
        base.DeleteItem(item);
    }

    /// <summary>Get a list of types served by this manager</summary>
    /// <returns></returns>
    public override Type[] GetKnownTypes() => SchedulingDataProvider.knownDataTypes;

    /// <summary>Gets a unique key for each data provider base.</summary>
    public override string RootKey => "PublishingDataProviderBase";

    /// <summary>Create a new task data with random ID</summary>
    /// <returns>Newly created task data</returns>
    public abstract ScheduledTaskData CreateTaskData();

    /// <summary>Create a new task data with specific ID</summary>
    /// <param name="id">Primary key</param>
    /// <returns>Newly created task data</returns>
    public abstract ScheduledTaskData CreateTaskData(Guid id);

    /// <summary>Search task data by primary key</summary>
    /// <param name="id">Primary key</param>
    /// <returns>Task data with primary key equal to <paramref name="id" /></returns>
    /// <exception cref="T:Telerik.Sitefinity.SitefinityExceptions.ItemNotFoundException">
    /// When no task data is found by <paramref name="id" />
    /// </exception>
    public abstract ScheduledTaskData GetTaskData(Guid id);

    /// <summary>
    /// Get a query of all task data items stored by the current provider
    /// </summary>
    /// <returns>All task data items stored by the current provider</returns>
    public abstract IQueryable<ScheduledTaskData> GetTaskData();

    /// <summary>
    /// Remove a task data item from the current transaction that will be deleted when the transaction is commited
    /// </summary>
    /// <param name="task">Task data to delete</param>
    public abstract void DeleteTaskData(ScheduledTaskData task);

    /// <summary>
    /// Creates new instance of <see cref="T:Telerik.Sitefinity.SyncLock.Lock" />.
    /// </summary>
    /// <param name="lockId">The lock id.</param>
    /// <returns>New instance of <see cref="T:Telerik.Sitefinity.SyncLock.Lock" /></returns>
    internal abstract Lock CreateLock(string lockId);

    /// <summary>
    /// Get a query for all <see cref="T:Telerik.Sitefinity.SyncLock.Lock" /> items.
    /// </summary>
    /// <returns>Queryable object for all <see cref="T:Telerik.Sitefinity.SyncLock.Lock" /> items</returns>
    internal abstract IQueryable<Lock> GetLocks();

    /// <summary>
    /// Deletes the provided <see cref="T:Telerik.Sitefinity.SyncLock.Lock" />.
    /// </summary>
    /// <param name="obj">The <see cref="T:Telerik.Sitefinity.SyncLock.Lock" /> to delete.</param>
    internal abstract void DeleteLock(Lock obj);

    Lock ISyncLockSupport.CreateLock(string lockId) => this.CreateLock(lockId);

    IQueryable<Lock> ISyncLockSupport.GetLocks() => this.GetLocks();

    void ISyncLockSupport.DeleteLock(Lock obj) => this.DeleteLock(obj);
  }
}
