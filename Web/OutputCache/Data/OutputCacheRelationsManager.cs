// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.OutputCache.Data.OutputCacheRelationsManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.OutputCache;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.SyncLock;
using Telerik.Sitefinity.Web.OutputCache.Configuration;

namespace Telerik.Sitefinity.Web.OutputCache.Data
{
  /// <summary>
  /// Responsible for manipulations with output cache relation items.
  /// </summary>
  /// <seealso cref="!:Telerik.Sitefinity.Data.ManagerBase&lt;Telerik.Sitefinity.Web.OutputCache.Data.OutputCacheRelationsProvider&gt;" />
  internal class OutputCacheRelationsManager : 
    ManagerBase<OutputCacheRelationsProvider>,
    ISyncLockSupportManager,
    IManager,
    IDisposable,
    IProviderResolver,
    ISyncLockSupport
  {
    private const string ContextDependencyTypesKey = "ContextDependencyTypes";
    private const string ContextUpdatedSitesKey = "ContextUpdatedSites";
    private static object typesCacheSync = new object();

    static OutputCacheRelationsManager()
    {
      ManagerBase<OutputCacheRelationsProvider>.Executing += new EventHandler<ExecutingEventArgs>(OutputCacheRelationsManager.Provider_Executing);
      ManagerBase<OutputCacheRelationsProvider>.Executed += new EventHandler<ExecutedEventArgs>(OutputCacheRelationsManager.Provider_Executed);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.OutputCache.Data.OutputCacheRelationsManager" /> class.
    /// </summary>
    public OutputCacheRelationsManager()
      : this((string) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.OutputCache.Data.OutputCacheRelationsManager" /> class.
    /// </summary>
    /// <param name="providerName">The name of the provider. If empty string or null the default provider is set</param>
    public OutputCacheRelationsManager(string providerName)
      : base(providerName)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.OutputCache.Data.OutputCacheRelationsManager" /> class.
    /// </summary>
    /// <param name="providerName">The name of the provider. If empty string or null the default provider is set</param>
    /// <param name="transactionName">The name of the transaction.</param>
    public OutputCacheRelationsManager(string providerName, string transactionName)
      : base(providerName, transactionName)
    {
    }

    /// <summary>The name of the module that this manager belongs to.</summary>
    public override string ModuleName => "OutputCache";

    /// <summary>Gets the default provider for this manager.</summary>
    protected internal override GetDefaultProvider DefaultProviderDelegate => (GetDefaultProvider) (() => Config.Get<OutputCacheConfig>().DefaultProvider);

    /// <summary>Collection of data provider settings.</summary>
    protected internal override ConfigElementDictionary<string, DataProviderSettings> ProvidersSettings => Config.Get<OutputCacheConfig>().Providers;

    /// <summary>Gets a manger instance for the default data provider.</summary>
    /// <returns>The manager instance.</returns>
    public static OutputCacheRelationsManager GetManager() => ManagerBase<OutputCacheRelationsProvider>.GetManager<OutputCacheRelationsManager>();

    /// <summary>
    /// Gets a manger instance for the specified data provider.
    /// </summary>
    /// <param name="providerName">The name of the data provider.</param>
    /// <returns>The manager instance.</returns>
    public static OutputCacheRelationsManager GetManager(
      string providerName)
    {
      return ManagerBase<OutputCacheRelationsProvider>.GetManager<OutputCacheRelationsManager>(providerName);
    }

    /// <summary>
    /// Gets a manger instance for the specified data provider.
    /// </summary>
    /// <param name="providerName">The name of the data provider.</param>
    /// <param name="transactionName">Name of a named global transaction.</param>
    /// <returns>The manager instance.</returns>
    public static OutputCacheRelationsManager GetManager(
      string providerName,
      string transactionName)
    {
      return ManagerBase<OutputCacheRelationsProvider>.GetManager<OutputCacheRelationsManager>(providerName, transactionName);
    }

    /// <summary>Creates the output cache item.</summary>
    /// <returns>The OutputCacheItem.</returns>
    public OutputCacheItem CreateOutputCacheItem() => this.Provider.CreateOutputCacheItem();

    /// <summary>Gets the output cache items.</summary>
    /// <param name="key">The cache item key.</param>
    /// <returns>OutputCacheItem queryable.</returns>
    public OutputCacheItem GetOutputCacheItem(string key) => this.Provider.GetOutputCacheItems().Where<OutputCacheItem>((Expression<Func<OutputCacheItem, bool>>) (oci => oci.Key == key)).SingleOrDefault<OutputCacheItem>();

    /// <summary>Deletes the output cache item.</summary>
    /// <param name="cacheItem">The output cache item to remove.</param>
    public void DeleteOutputCacheItem(OutputCacheItem cacheItem) => this.Provider.DeleteOutputCacheItem(cacheItem);

    /// <summary>Creates the output cache relation.</summary>
    /// <returns>The OutputCacheDependency.</returns>
    public OutputCacheDependency CreateOutputCacheDependency() => this.Provider.CreateOutputCacheDependency();

    /// <summary>Gets the output cache dependencies.</summary>
    /// <returns>OutputCacheDependency queryable.</returns>
    public IQueryable<OutputCacheDependency> GetOutputCacheDependencies() => this.Provider.GetOutputCacheDependencies();

    /// <summary>Deletes the output cache item key.</summary>
    /// <param name="cacheDependency">The output cache relation to remove.</param>
    public void DeleteOutputCacheDependency(OutputCacheDependency cacheDependency) => this.Provider.DeleteOutputCacheDependency(cacheDependency);

    /// <summary>Deletes all output cache Dependencies.</summary>
    /// <param name="cacheDependency">The cache dependencies to remove.</param>
    public void DeleteAllOutputCacheDependencies(
      Expression<Func<OutputCacheDependency, bool>> cacheDependency)
    {
      this.Provider.DeleteAllOutputCacheDependencies(cacheDependency);
    }

    internal void UpdateOutputCacheItemsStatusByKeys(
      IEnumerable<string> keys,
      OutputCacheItemStatus oldStatus,
      OutputCacheItemStatus newStatus)
    {
      this.Provider.UpdateOutputCacheItemsStatusByKeys(keys, oldStatus, newStatus);
    }

    internal void UpdateOutputCacheItemsStatusByKeys(
      IEnumerable<string> keys,
      OutputCacheItemStatus newStatus)
    {
      this.Provider.UpdateOutputCacheItemsStatusByKeys(keys, newStatus);
    }

    internal void UpdatePageVariationsStatus(
      IEnumerable<string> pageKeys,
      OutputCacheItemStatus oldStatus,
      OutputCacheItemStatus newStatus)
    {
      this.Provider.UpdatePageVariationsStatus(pageKeys, oldStatus, newStatus);
    }

    internal void DeleteItemsByPageNodeKeys(IEnumerable<string> pageNodeKeys) => this.Provider.DeleteItemsByPageNodeKeys(pageNodeKeys);

    internal void DeleteExpiredItemsOlderThan(DateTime olderThanDate) => this.Provider.DeleteExpiredItemsOlderThan(olderThanDate);

    private static void Provider_Executing(object sender, ExecutingEventArgs args)
    {
      if (!(args.CommandName == "CommitTransaction") && !(args.CommandName == "FlushTransaction"))
        return;
      OutputCacheRelationsProvider relationsProvider = sender as OutputCacheRelationsProvider;
      if (!(relationsProvider.GetExecutionStateData("ContextDependencyTypes") is HashSet<string> stringSet))
        stringSet = new HashSet<string>();
      if (!(relationsProvider.GetExecutionStateData("ContextUpdatedSites") is HashSet<Guid> guidSet))
        guidSet = new HashSet<Guid>();
      foreach (OutputCacheDependencyType itemInTransaction in relationsProvider.GetDirtyItems().OfType<OutputCacheDependencyType>())
      {
        if (relationsProvider.GetDirtyItemStatus((object) itemInTransaction) == SecurityConstants.TransactionActionType.New)
          stringSet.Add(itemInTransaction.TypeName);
      }
      foreach (OutputCacheItem outputCacheItem in relationsProvider.GetDirtyItems().OfType<OutputCacheItem>())
      {
        switch (relationsProvider.GetDirtyItemStatus((object) outputCacheItem))
        {
          case SecurityConstants.TransactionActionType.New:
            guidSet.Add(outputCacheItem.SiteId);
            continue;
          case SecurityConstants.TransactionActionType.Updated:
            if (!relationsProvider.IsFieldDirty((object) outputCacheItem, "Status"))
              continue;
            goto case SecurityConstants.TransactionActionType.New;
          default:
            continue;
        }
      }
      if (stringSet.Any<string>())
        relationsProvider.SetExecutionStateData("ContextDependencyTypes", (object) stringSet);
      if (!guidSet.Any<Guid>())
        return;
      relationsProvider.SetExecutionStateData("ContextUpdatedSites", (object) guidSet);
    }

    private static void Provider_Executed(object sender, ExecutedEventArgs args)
    {
      if (args.CommandName != "CommitTransaction")
        return;
      OutputCacheRelationsProvider relationsProvider = sender as OutputCacheRelationsProvider;
      if (relationsProvider.GetExecutionStateData("ContextDependencyTypes") is HashSet<string> executionStateData1 && executionStateData1.Any<string>())
        OutputCacheWorker.SupportedDependencyTypes.Notify(executionStateData1);
      if (!(relationsProvider.GetExecutionStateData("ContextUpdatedSites") is HashSet<Guid> executionStateData2) || !executionStateData2.Any<Guid>())
        return;
      CacheDependency.Notify((IList<CacheDependencyKey>) executionStateData2.Select<Guid, CacheDependencyKey>((Func<Guid, CacheDependencyKey>) (i => new CacheDependencyKey()
      {
        Type = typeof (OutputCacheItem),
        Key = i.ToString()
      })).ToList<CacheDependencyKey>());
      OutputCacheWorker.DeleteOldCacheItemsAsync();
    }

    Lock ISyncLockSupport.CreateLock(string lockId) => this.Provider.CreateLock(lockId);

    IQueryable<Lock> ISyncLockSupport.GetLocks() => this.Provider.GetLocks();

    void ISyncLockSupport.DeleteLock(Lock obj) => this.Provider.DeleteLock(obj);
  }
}
