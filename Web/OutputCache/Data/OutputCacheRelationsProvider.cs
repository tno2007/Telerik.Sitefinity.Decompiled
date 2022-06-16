// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.OutputCache.Data.OutputCacheRelationsProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.OutputCache;
using Telerik.Sitefinity.SyncLock;

namespace Telerik.Sitefinity.Web.OutputCache.Data
{
  /// <summary>
  /// Provider for output cache relations between output cache items and cache dependencies.
  /// </summary>
  /// <seealso cref="T:Telerik.Sitefinity.Data.DataProviderBase" />
  public abstract class OutputCacheRelationsProvider : DataProviderBase, ISyncLockSupport
  {
    /// <summary>Gets a unique key for each data provider base.</summary>
    public override string RootKey => this.GetType().Name;

    /// <summary>Get a list of types served by this manager</summary>
    /// <returns>Returns the types it operates with.</returns>
    public override Type[] GetKnownTypes() => new Type[1]
    {
      typeof (OutputCacheDependency)
    };

    /// <summary>Creates the output cache item.</summary>
    /// <returns>The OutputCacheItem.</returns>
    public abstract OutputCacheItem CreateOutputCacheItem();

    /// <summary>Gets the output cache items.</summary>
    /// <returns>OutputCacheItem queryable.</returns>
    public abstract IQueryable<OutputCacheItem> GetOutputCacheItems();

    /// <summary>Deletes the output cache item.</summary>
    /// <param name="cacheItem">The output cache item to remove.</param>
    public abstract void DeleteOutputCacheItem(OutputCacheItem cacheItem);

    /// <summary>Deletes the output cache item.</summary>
    /// <param name="cacheItem">The output cache items to remove.</param>
    public abstract void DeleteAllOutputCacheItems(Expression<Func<OutputCacheItem, bool>> cacheItem);

    /// <summary>Creates the output cache dependency.</summary>
    /// <returns>The OutputCacheDependency.</returns>
    public abstract OutputCacheDependency CreateOutputCacheDependency();

    /// <summary>Gets the output cache dependencies.</summary>
    /// <returns>OutputCacheDependency queryable.</returns>
    public abstract IQueryable<OutputCacheDependency> GetOutputCacheDependencies();

    /// <summary>Deletes the output cache dependency.</summary>
    /// <param name="cacheDependency">The output cache dependency to remove.</param>
    public abstract void DeleteOutputCacheDependency(OutputCacheDependency cacheDependency);

    /// <summary>Deletes the output cache dependency.</summary>
    /// <param name="cacheDependency">The output cache dependencies to remove.</param>
    public abstract void DeleteAllOutputCacheDependencies(
      Expression<Func<OutputCacheDependency, bool>> cacheDependency);

    internal abstract void UpdateOutputCacheItemsStatusByKeys(
      IEnumerable<string> keys,
      OutputCacheItemStatus oldStatus,
      OutputCacheItemStatus newStatus);

    internal abstract void UpdateOutputCacheItemsStatusByKeys(
      IEnumerable<string> keys,
      OutputCacheItemStatus newStatus);

    internal abstract void UpdatePageVariationsStatus(
      IEnumerable<string> pageKeys,
      OutputCacheItemStatus oldStatus,
      OutputCacheItemStatus newStatus);

    internal abstract void DeleteItemsByPageNodeKeys(IEnumerable<string> pageNodeKeys);

    internal abstract void DeleteExpiredItemsOlderThan(DateTime olderThanDate);

    internal abstract IQueryable<OutputCacheDependencyType> GetOutputCacheDependencyTypes();

    internal abstract OutputCacheDependencyType CreateOutputCacheDependencyType(
      string typeName);

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
