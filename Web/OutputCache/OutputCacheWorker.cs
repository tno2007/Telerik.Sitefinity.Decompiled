// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.OutputCache.OutputCacheWorker
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Telerik.OpenAccess.Exceptions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.BackgroundTasks;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.LoadBalancing;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.OutputCache;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities;
using Telerik.Sitefinity.Web.OutputCache.Data;

namespace Telerik.Sitefinity.Web.OutputCache
{
  internal class OutputCacheWorker
  {
    private const int CleanUpIntervalInMinutes = 10;
    private const int UpdateIntervalInMiliseconds = 2000;
    private const string ProviderExecutingDeletedPagesContextStateKey = "SitefinityOutputCacheProvider_DeletedPages";
    private static volatile bool deletingOldCacheItemsInProgress = false;
    private static DateTime lastDeleteOldCacheItemsDate = DateTime.UtcNow;
    private static ConcurrentDictionary<string, WeakReference<OutputCacheWorker.OutputCacheItemsCache>> pendingProxyCacheUpdates = new ConcurrentDictionary<string, WeakReference<OutputCacheWorker.OutputCacheItemsCache>>();
    private static OutputCacheWorker.DependenciesQueue pendingCacheDependencyUpdates = new OutputCacheWorker.DependenciesQueue();
    private static ConcurrentProperty<OutputCacheWorker.SupportedTypes> supportedDependencyTypes = new ConcurrentProperty<OutputCacheWorker.SupportedTypes>((Func<OutputCacheWorker.SupportedTypes>) (() => new OutputCacheWorker.SupportedTypes()));
    private static Dictionary<Type, string> typeHashMapping = new Dictionary<Type, string>();
    private static volatile bool processing = false;
    private static volatile int dependenciesProcessingTasks = 0;
    private static DateTime dependenciesProcessingLastActivity = DateTime.UtcNow;
    private static System.Timers.Timer timer = new System.Timers.Timer(2000.0);
    private static volatile int runningAsycOperationsCount = 0;
    private static object expSyncLock = new object();

    static OutputCacheWorker()
    {
      OutputCacheWorker.timer.Elapsed += new ElapsedEventHandler(OutputCacheWorker.Timer_Elapsed);
      OutputCacheWorker.timer.AutoReset = false;
      OutputCacheWorker.timer.Enabled = true;
      CacheDependency.CacheDependencyNotify += new CacheDependency.NotifyCallback(OutputCacheWorker.ExpireCacheItem);
      ManagerBase<PageDataProvider>.Executing += new EventHandler<ExecutingEventArgs>(OutputCacheWorker.PagesProvider_Executing);
      ManagerBase<PageDataProvider>.Executed += new EventHandler<ExecutedEventArgs>(OutputCacheWorker.PagesProvider_Executed);
    }

    internal static void Initialize()
    {
    }

    internal static OutputCacheWorker.SupportedTypes SupportedDependencyTypes => OutputCacheWorker.supportedDependencyTypes.Value;

    internal static OutputCacheItemProxy GetOutputCacheItemProxy(string key) => OutputCacheWorker.GetOutputCacheItems().GetItem(key);

    internal static IEnumerable<OutputCacheItemProxy> GetItemsPerPage(
      string pageKey)
    {
      return OutputCacheWorker.GetOutputCacheItems().GetItemsPerPage(pageKey);
    }

    internal static void UpdateCacheDependenciesAsync(
      string outputCacheKey,
      IEnumerable<CacheDependencyKey> dependencies,
      string siteId)
    {
      int aggregationTreshold = CacheClientFactory.GetCacheInvalidationStrategy().CacheDependencyAggregationTreshold;
      if (aggregationTreshold > 0)
      {
        List<CacheDependencyKey> cacheDependencyKeyList1 = new List<CacheDependencyKey>();
        CacheDependencyKey cacheDependencyKey1;
        foreach (IGrouping<Type, CacheDependencyKey> grouping in dependencies.GroupBy<CacheDependencyKey, Type>((Func<CacheDependencyKey, Type>) (d => d.Type)))
        {
          if (grouping.Count<CacheDependencyKey>() > aggregationTreshold)
          {
            if (typeof (ISiteMapRootCacheDependency).IsAssignableFrom(grouping.Key))
            {
              List<CacheDependencyKey> cacheDependencyKeyList2 = cacheDependencyKeyList1;
              cacheDependencyKey1 = new CacheDependencyKey();
              cacheDependencyKey1.Type = grouping.Key;
              cacheDependencyKey1.Key = siteId;
              CacheDependencyKey cacheDependencyKey2 = cacheDependencyKey1;
              cacheDependencyKeyList2.Add(cacheDependencyKey2);
            }
            else
            {
              List<CacheDependencyKey> cacheDependencyKeyList3 = cacheDependencyKeyList1;
              cacheDependencyKey1 = new CacheDependencyKey();
              cacheDependencyKey1.Type = grouping.Key;
              CacheDependencyKey cacheDependencyKey3 = cacheDependencyKey1;
              cacheDependencyKeyList3.Add(cacheDependencyKey3);
            }
          }
          else
            cacheDependencyKeyList1.AddRange((IEnumerable<CacheDependencyKey>) grouping);
        }
        dependencies = (IEnumerable<CacheDependencyKey>) cacheDependencyKeyList1;
      }
      OutputCacheWorker.pendingCacheDependencyUpdates.Enqueue(outputCacheKey, dependencies, siteId);
      if (OutputCacheWorker.dependenciesProcessingTasks != 0 && !(OutputCacheWorker.dependenciesProcessingLastActivity < DateTime.UtcNow.AddMinutes(-2.0)))
        return;
      OutputCacheWorker.StartUpdateDependenciesTask();
    }

    internal static string GetCacheDependencyKey(CacheDependencyKey dependency) => OutputCacheWorker.SupportedDependencyTypes.GetTypeId(dependency.Type.FullName).ToString() + ":" + dependency.Key;

    internal static void ExpireAllOutputCacheItems()
    {
      HashSet<Guid> siteIds = new HashSet<Guid>();
      DateTime currentDateTime = DateTime.UtcNow;
      using (OutputCacheRelationsManager manager = OutputCacheRelationsManager.GetManager())
      {
        while (true)
        {
          \u003C\u003Ef__AnonymousType26<string, Guid>[] array1 = manager.Provider.GetOutputCacheItems().Where<OutputCacheItem>((Expression<Func<OutputCacheItem, bool>>) (i => (int) i.Status == 2 && i.DateModified < currentDateTime)).Take<OutputCacheItem>(100).Select(i => new
          {
            Key = i.Key,
            SiteId = i.SiteId
          }).Distinct().ToArray();
          string[] array2 = array1.Select(i => i.Key).ToArray<string>();
          if (((IEnumerable<string>) array2).Any<string>())
          {
            manager.UpdateOutputCacheItemsStatusByKeys((IEnumerable<string>) array2, OutputCacheItemStatus.Live, OutputCacheItemStatus.Expired);
            array1.Select(i => i.SiteId).Distinct<Guid>().ToList<Guid>().ForEach((Action<Guid>) (s => siteIds.Add(s)));
          }
          else
            break;
        }
      }
      CacheDependency.Notify((IList<CacheDependencyKey>) siteIds.Select<Guid, CacheDependencyKey>((Func<Guid, CacheDependencyKey>) (i => new CacheDependencyKey()
      {
        Type = typeof (OutputCacheItem),
        Key = i.ToString()
      })).ToList<CacheDependencyKey>());
      OutputCacheWorker.NotifyExpiredItems();
    }

    internal static void WaitCleanupOperationsToComplete()
    {
      int num = 100;
      int millisecondsTimeout = 100;
      while (num > 0)
      {
        --num;
        if (OutputCacheWorker.runningAsycOperationsCount != 0)
          Thread.Sleep(millisecondsTimeout);
        else
          break;
      }
      if (OutputCacheWorker.runningAsycOperationsCount > 0)
        throw new Exception("Async clean up operations does not complete in the given time");
      OutputCacheWorker.UpdateProxyCache();
    }

    internal static void WaitOutputCachePersistenceToComplete()
    {
      int num = 100;
      int millisecondsTimeout = 100;
      while (num > 0)
      {
        --num;
        if (OutputCacheWorker.dependenciesProcessingTasks != 0)
          Thread.Sleep(millisecondsTimeout);
        else
          break;
      }
      if (OutputCacheWorker.dependenciesProcessingTasks > 0)
        throw new Exception("Async cache dependency persistence does not complete in the given time");
      OutputCacheWorker.UpdateProxyCache();
    }

    internal static void UpdateProxyCache()
    {
      foreach (string key in OutputCacheWorker.pendingProxyCacheUpdates.Keys.ToArray<string>())
      {
        WeakReference<OutputCacheWorker.OutputCacheItemsCache> weakReference;
        if (OutputCacheWorker.pendingProxyCacheUpdates.TryGetValue(key, out weakReference))
        {
          OutputCacheWorker.OutputCacheItemsCache target;
          if (!weakReference.TryGetTarget(out target))
          {
            if (!OutputCacheWorker.pendingProxyCacheUpdates.TryRemove(key, out weakReference) || !weakReference.TryGetTarget(out target))
              break;
            OutputCacheWorker.pendingProxyCacheUpdates[key] = new WeakReference<OutputCacheWorker.OutputCacheItemsCache>(target);
          }
          target.Update();
        }
      }
    }

    internal static void NotifyCacheDependencyQueue(
      string[] types,
      string[] keys,
      bool sendSystemMessage = true)
    {
      OutputCacheWorker.pendingCacheDependencyUpdates.NotifyExpired(types, keys, sendSystemMessage);
    }

    private static OutputCacheWorker.OutputCacheItemsCache GetOutputCacheItems()
    {
      ICacheManager cacheManager = SystemManager.GetCacheManager(CacheManagerInstance.OutputCacheInfo);
      ISite currentSite = SystemManager.CurrentContext.CurrentSite;
      Guid id = currentSite.Id;
      string key = id.ToString();
      if (!(cacheManager[key] is OutputCacheWorker.OutputCacheItemsCache outputCacheItems1))
      {
        lock (currentSite)
        {
          if (!(cacheManager[key] is OutputCacheWorker.OutputCacheItemsCache outputCacheItems1))
          {
            outputCacheItems1 = new OutputCacheWorker.OutputCacheItemsCache(id);
            cacheManager.Add(key, (object) outputCacheItems1, CacheItemPriority.Normal, (ICacheItemRefreshAction) null, (ICacheItemExpiration) new SlidingTime(TimeSpan.FromMinutes(10.0)));
          }
        }
      }
      return outputCacheItems1;
    }

    private static void PagesProvider_Executing(object sender, ExecutingEventArgs args)
    {
      if (!(args.CommandName == "CommitTransaction") && !(args.CommandName == "FlushTransaction"))
        return;
      PageDataProvider provider = sender as PageDataProvider;
      List<string> list = provider.GetDirtyItems().OfType<PageNode>().Where<PageNode>((Func<PageNode, bool>) (p => provider.GetDirtyItemStatus((object) p) == SecurityConstants.TransactionActionType.Deleted)).Select<PageNode, string>((Func<PageNode, string>) (p => PageSiteNode.GetKey(p.Id))).ToList<string>();
      if (list.Count <= 0)
        return;
      if (!(provider.GetExecutionStateData("SitefinityOutputCacheProvider_DeletedPages") is IList<IList<string>> data))
      {
        data = (IList<IList<string>>) new List<IList<string>>();
        provider.SetExecutionStateData("SitefinityOutputCacheProvider_DeletedPages", (object) data);
      }
      data.Add((IList<string>) list);
    }

    private static void PagesProvider_Executed(object sender, ExecutedEventArgs args)
    {
      if (args.CommandName != "CommitTransaction")
        return;
      PageDataProvider pageDataProvider = sender as PageDataProvider;
      if (!(pageDataProvider.GetExecutionStateData("SitefinityOutputCacheProvider_DeletedPages") is IList<IList<string>> executionStateData))
        return;
      foreach (IEnumerable<string> pageKeys in (IEnumerable<IList<string>>) executionStateData)
        OutputCacheWorker.DeletePageCacheItems(pageKeys);
      pageDataProvider.SetExecutionStateData("SitefinityOutputCacheProvider_DeletedPages", (object) null);
    }

    private static void ExpireCacheItem(IEnumerable<CacheDependencyKey> items)
    {
      if (!Bootstrapper.IsReady || !ManagerBase<OutputCacheRelationsProvider>.initialized)
        return;
      IList<CacheDependencyKey> dependencies = OutputCacheWorker.SupportedDependencyTypes.Filter(items);
      if (dependencies.Count == 0)
        return;
      Guid currentSiteId = SystemManager.CurrentContext.CurrentSite.Id;
      ++OutputCacheWorker.runningAsycOperationsCount;
      try
      {
        OutputCacheWorker.BackgroundTasksService.EnqueueTask((Action) (() =>
        {
          try
          {
            if (OutputCacheWorker.GetExpiredOutputCacheItems(dependencies, currentSiteId).Length == 0)
              return;
            OutputCacheWorker.NotifyExpiredItems();
          }
          catch (Exception ex)
          {
            Telerik.Sitefinity.Abstractions.Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions);
          }
          finally
          {
            --OutputCacheWorker.runningAsycOperationsCount;
          }
        }));
      }
      catch (Exception ex)
      {
        Telerik.Sitefinity.Abstractions.Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions);
      }
    }

    private static string[] GetExpiredOutputCacheItems(
      IList<CacheDependencyKey> dependencies,
      Guid currentSiteId)
    {
      lock (OutputCacheWorker.expSyncLock)
      {
        HashSet<string> source = new HashSet<string>();
        IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
        if (multisiteContext != null)
        {
          foreach (string input in dependencies.Where<CacheDependencyKey>((Func<CacheDependencyKey, bool>) (d => d.Type == typeof (ISiteMapRootCacheDependency))).Select<CacheDependencyKey, string>((Func<CacheDependencyKey, string>) (d => d.Key)))
          {
            Guid result;
            if (Guid.TryParse(input, out result))
            {
              ISite siteBySiteMapRoot = multisiteContext.GetSiteBySiteMapRoot(result);
              if (siteBySiteMapRoot != null)
                source.Add(siteBySiteMapRoot.Id.ToString());
            }
          }
        }
        if (!source.Any<string>())
          source.Add(currentSiteId.ToString());
        dependencies = (IList<CacheDependencyKey>) dependencies.Where<CacheDependencyKey>((Func<CacheDependencyKey, bool>) (d => d.Type != typeof (ISiteMapRootCacheDependency))).ToList<CacheDependencyKey>();
        string[] types = dependencies.Where<CacheDependencyKey>((Func<CacheDependencyKey, bool>) (i => i.Key.IsNullOrEmpty())).Select<CacheDependencyKey, string>((Func<CacheDependencyKey, string>) (i => i.Type.FullName)).Distinct<string>().ToArray<string>();
        List<string> keys = dependencies.Where<CacheDependencyKey>((Func<CacheDependencyKey, bool>) (i => !((IEnumerable<string>) types).Contains<string>(i.Type.FullName))).Select<CacheDependencyKey, string>((Func<CacheDependencyKey, string>) (i => OutputCacheWorker.GetCacheDependencyKey(i))).ToList<string>();
        keys.AddRange((IEnumerable<string>) dependencies.Where<CacheDependencyKey>((Func<CacheDependencyKey, bool>) (i => !((IEnumerable<string>) types).Contains<string>(i.Type.FullName))).Select<CacheDependencyKey, string>((Func<CacheDependencyKey, string>) (i => OutputCacheWorker.GetCacheDependencyKey(new CacheDependencyKey()
        {
          Type = i.Type
        }))).Distinct<string>().ToList<string>());
        foreach (string str in source)
        {
          string site = str;
          keys.AddRange((IEnumerable<string>) dependencies.Where<CacheDependencyKey>((Func<CacheDependencyKey, bool>) (i => !((IEnumerable<string>) types).Contains<string>(i.Type.FullName) && typeof (ISiteMapRootCacheDependency).IsAssignableFrom(i.Type))).Select<CacheDependencyKey, string>((Func<CacheDependencyKey, string>) (i => OutputCacheWorker.GetCacheDependencyKey(new CacheDependencyKey()
          {
            Type = i.Type,
            Key = site
          }))).ToList<string>());
        }
        OutputCacheWorker.NotifyCacheDependencyQueue(types, keys.ToArray());
        IEnumerable<int> typeIds = ((IEnumerable<string>) types).Select<string, int>((Func<string, int>) (t => OutputCacheWorker.SupportedDependencyTypes.GetTypeId(t)));
        string[] array1;
        using (OutputCacheRelationsManager manager = OutputCacheRelationsManager.GetManager())
        {
          \u003C\u003Ef__AnonymousType26<string, Guid>[] array2 = manager.GetOutputCacheDependencies().Where<OutputCacheDependency>((Expression<Func<OutputCacheDependency, bool>>) (d => typeIds.Contains<int>(d.TypeId) || keys.Contains(d.ItemKey))).Where<OutputCacheDependency>((Expression<Func<OutputCacheDependency, bool>>) (d => (int) d.CacheItem.Status == 2)).Select(o => new
          {
            Key = o.CacheItem.Key,
            SiteId = o.CacheItem.SiteId
          }).Distinct().ToArray();
          array1 = array2.Select(i => i.Key).ToArray<string>();
          if (((IEnumerable<string>) array1).Any<string>())
          {
            manager.UpdateOutputCacheItemsStatusByKeys((IEnumerable<string>) array1, OutputCacheItemStatus.Live, OutputCacheItemStatus.Expired);
            CacheDependency.Notify((IList<CacheDependencyKey>) array2.Select(i => i.SiteId).Distinct<Guid>().Select<Guid, CacheDependencyKey>((Func<Guid, CacheDependencyKey>) (i => new CacheDependencyKey()
            {
              Type = typeof (OutputCacheItem),
              Key = i.ToString()
            })).ToList<CacheDependencyKey>());
          }
        }
        return array1;
      }
    }

    private static void NotifyExpiredItems()
    {
      ++OutputCacheWorker.runningAsycOperationsCount;
      Task.Run((Action) (() =>
      {
        try
        {
          SystemManager.RunWithElevatedPrivilege((SystemManager.RunWithElevatedPrivilegeDelegate) (parameters =>
          {
            CacheInvalidationStrategy invalidationStrategy = CacheClientFactory.GetCacheInvalidationStrategy();
            if (invalidationStrategy.TryRun())
              return;
            Thread.Sleep(50);
            invalidationStrategy.TryRun();
          }));
        }
        catch (Exception ex)
        {
          Telerik.Sitefinity.Abstractions.Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions);
        }
        finally
        {
          --OutputCacheWorker.runningAsycOperationsCount;
        }
      }));
    }

    private static void DeletePageCacheItems(IEnumerable<string> pageKeys)
    {
      ++OutputCacheWorker.runningAsycOperationsCount;
      Task.Run((Action) (() =>
      {
        try
        {
          SystemManager.RunWithElevatedPrivilege((SystemManager.RunWithElevatedPrivilegeDelegate) (parameters =>
          {
            using (OutputCacheRelationsManager manager = OutputCacheRelationsManager.GetManager())
            {
              \u003C\u003Ef__AnonymousType26<string, Guid>[] array3 = manager.Provider.GetOutputCacheItems().Where<OutputCacheItem>((Expression<Func<OutputCacheItem, bool>>) (i => pageKeys.Contains<string>(i.SiteMapNodeKey))).Select(i => new
              {
                Key = i.Key,
                SiteId = i.SiteId
              }).Distinct().ToArray();
              string[] array4 = array3.Select(i => i.Key).ToArray<string>();
              if (((IEnumerable<string>) array4).Any<string>())
              {
                manager.UpdateOutputCacheItemsStatusByKeys((IEnumerable<string>) array4, OutputCacheItemStatus.Deleted);
                CacheDependency.Notify((IList<CacheDependencyKey>) array3.Select(i => i.SiteId).Distinct<Guid>().Select<Guid, CacheDependencyKey>((Func<Guid, CacheDependencyKey>) (i => new CacheDependencyKey()
                {
                  Type = typeof (OutputCacheItem),
                  Key = i.ToString()
                })).ToList<CacheDependencyKey>());
              }
            }
            OutputCacheWorker.NotifyExpiredItems();
          }));
        }
        catch (Exception ex)
        {
          Telerik.Sitefinity.Abstractions.Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions);
        }
        finally
        {
          --OutputCacheWorker.runningAsycOperationsCount;
        }
      }));
    }

    private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
    {
      OutputCacheWorker.processing = true;
      try
      {
        OutputCacheWorker.UpdateProxyCache();
      }
      finally
      {
        OutputCacheWorker.timer.Enabled = true;
        OutputCacheWorker.processing = false;
      }
    }

    private static IBackgroundTasksService BackgroundTasksService => (IBackgroundTasksService) ObjectFactory.Resolve<OutputCacheWorker.OutputCacheWorkerBackgroundService>();

    private static void StartUpdateDependenciesTask()
    {
      ++OutputCacheWorker.dependenciesProcessingTasks;
      ++OutputCacheWorker.runningAsycOperationsCount;
      Task.Run((Action) (() =>
      {
        try
        {
          if (!OutputCacheWorker.pendingCacheDependencyUpdates.Any())
            return;
          SystemManager.RunWithElevatedPrivilege((SystemManager.RunWithElevatedPrivilegeDelegate) (parameters =>
          {
            using (OutputCacheRelationsManager manager = OutputCacheRelationsManager.GetManager())
            {
              if (manager.Provider is IOpenAccessDataProvider provider2)
              {
                SitefinityOAContext context = provider2.GetContext();
                if (context != null)
                  context.ContextOptions.IsolationLevel = new IsolationLevel?(IsolationLevel.ReadUncommitted);
              }
              int dependenciesUpdateBatchSize = CacheClientFactory.GetCacheInvalidationStrategy().DependenciesUpdateBatchSize;
              while (true)
              {
                IDictionary<string, IDictionary<string, string>> batch = OutputCacheWorker.pendingCacheDependencyUpdates.Dequeue(dependenciesUpdateBatchSize);
                if (batch.Count != 0)
                {
                  OutputCacheWorker.dependenciesProcessingLastActivity = DateTime.UtcNow;
                  int num = 2;
                  while (num-- > 0)
                  {
                    IQueryable<OutputCacheItem> outputCacheItems = manager.Provider.GetOutputCacheItems();
                    Expression<Func<OutputCacheItem, bool>> predicate = (Expression<Func<OutputCacheItem, bool>>) (i => batch.Keys.Contains(i.Key) && i.Status == OutputCacheItemStatus.New);
                    foreach (OutputCacheItem outputCacheItem in outputCacheItems.Where<OutputCacheItem>(predicate).ToList<OutputCacheItem>())
                    {
                      outputCacheItem.Status = OutputCacheItemStatus.Live;
                      outputCacheItem.DateModified = DateTime.UtcNow;
                      IDictionary<string, string> dictionary = batch[outputCacheItem.Key];
                      foreach (OutputCacheDependency cacheDependency in outputCacheItem.Dependencies.ToList<OutputCacheDependency>())
                      {
                        if (!dictionary.Keys.Contains(cacheDependency.ItemKey))
                          manager.Provider.DeleteOutputCacheDependency(cacheDependency);
                      }
                      foreach (KeyValuePair<string, string> keyValuePair in (IEnumerable<KeyValuePair<string, string>>) dictionary)
                      {
                        KeyValuePair<string, string> key = keyValuePair;
                        if (!outputCacheItem.Dependencies.Any<OutputCacheDependency>((Func<OutputCacheDependency, bool>) (d => d.ItemKey.Equals(key.Key))))
                        {
                          OutputCacheDependency outputCacheDependency = manager.CreateOutputCacheDependency();
                          outputCacheDependency.CacheItem = outputCacheItem;
                          outputCacheDependency.ItemKey = key.Key;
                          outputCacheDependency.TypeId = OutputCacheWorker.SupportedDependencyTypes.GetTypeId(key.Value, true);
                        }
                      }
                    }
                    try
                    {
                      manager.SaveChanges();
                      break;
                    }
                    catch (OptimisticVerificationException ex)
                    {
                      manager.CancelChanges();
                      Thread.Sleep(new Random().Next(0, 500));
                    }
                    catch (DuplicateKeyException ex)
                    {
                      manager.CancelChanges();
                    }
                    catch (Exception ex)
                    {
                      Telerik.Sitefinity.Abstractions.Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions);
                      break;
                    }
                  }
                  Thread.Sleep(10);
                }
                else
                  break;
              }
            }
          }));
        }
        catch (Exception ex)
        {
          Telerik.Sitefinity.Abstractions.Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions);
        }
      })).ContinueWith((Action<Task>) (t =>
      {
        --OutputCacheWorker.dependenciesProcessingTasks;
        --OutputCacheWorker.runningAsycOperationsCount;
        if (!OutputCacheWorker.pendingCacheDependencyUpdates.Any() || OutputCacheWorker.dependenciesProcessingTasks != 0)
          return;
        OutputCacheWorker.StartUpdateDependenciesTask();
      }));
    }

    internal static void DeleteOldCacheItemsAsync(bool force = false)
    {
      if (OutputCacheWorker.deletingOldCacheItemsInProgress)
        return;
      OutputCacheWorker.deletingOldCacheItemsInProgress = true;
      if (force || DateTime.UtcNow > OutputCacheWorker.lastDeleteOldCacheItemsDate.AddMinutes(10.0))
      {
        OutputCacheWorker.lastDeleteOldCacheItemsDate = DateTime.UtcNow;
        ++OutputCacheWorker.runningAsycOperationsCount;
        Task.Run((Action) (() =>
        {
          try
          {
            SystemManager.RunWithElevatedPrivilege((SystemManager.RunWithElevatedPrivilegeDelegate) (parameters =>
            {
              using (OutputCacheRelationsManager manager = OutputCacheRelationsManager.GetManager())
                manager.DeleteExpiredItemsOlderThan(DateTime.UtcNow.AddHours(-24.0));
            }));
          }
          catch (Exception ex)
          {
            Telerik.Sitefinity.Abstractions.Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions);
          }
          finally
          {
            --OutputCacheWorker.runningAsycOperationsCount;
          }
        }));
      }
      OutputCacheWorker.deletingOldCacheItemsInProgress = false;
    }

    internal class OutputCacheItemsCache
    {
      private const int ResetIntervalInMinutes = 5;
      private Exception lastException;
      private volatile int updates;
      private volatile bool fullUpdate;
      private DateTime lastReset = DateTime.UtcNow;
      private DateTime lastModified = DateTime.UtcNow;
      private Guid siteId;
      private IDictionary<string, OutputCacheItemProxy> items;
      private ReaderWriterLockWrapper readWriteLock = new ReaderWriterLockWrapper();

      public OutputCacheItemsCache(Guid siteId)
      {
        this.siteId = siteId;
        using (OutputCacheRelationsManager manager = OutputCacheRelationsManager.GetManager())
          this.items = this.GetAll(manager, true);
        this.Init();
        string key = this.siteId.ToString();
        OutputCacheWorker.pendingProxyCacheUpdates[key] = new WeakReference<OutputCacheWorker.OutputCacheItemsCache>(this);
        CacheDependency.Subscribe(typeof (OutputCacheItem), key, new ChangedCallback(this.OnUpdate));
        CacheDependency.Subscribe(typeof (OutputCacheWorker.OutputCacheItemsCache), key, new ChangedCallback(this.OnFullUpdate));
      }

      public OutputCacheItemProxy GetItem(string key)
      {
        OutputCacheItemProxy item = (OutputCacheItemProxy) null;
        this.readWriteLock.EnterForRead((Action) (() => this.items.TryGetValue(key, out item)));
        return item;
      }

      public IEnumerable<OutputCacheItemProxy> GetItemsPerPage(
        string pageKey)
      {
        List<OutputCacheItemProxy> result = (List<OutputCacheItemProxy>) null;
        this.readWriteLock.EnterForRead((Action) (() => result = new List<OutputCacheItemProxy>(this.items.Values.Where<OutputCacheItemProxy>((Func<OutputCacheItemProxy, bool>) (i => i.SiteMapNodeKey == pageKey)))));
        return (IEnumerable<OutputCacheItemProxy>) result;
      }

      public bool Update()
      {
        if (this.updates > 0)
        {
          lock (this)
          {
            bool fullUpdate = this.fullUpdate;
            this.fullUpdate = false;
            OutputCacheRelationsManager manager = OutputCacheRelationsManager.GetManager();
            try
            {
              if (manager.Provider is IOpenAccessDataProvider provider)
              {
                SitefinityOAContext context = provider.GetContext();
                if (context != null)
                  context.ContextOptions.IsolationLevel = new IsolationLevel?(IsolationLevel.ReadUncommitted);
              }
              List<OutputCacheItem> changes = (List<OutputCacheItem>) null;
              IDictionary<string, OutputCacheItemProxy> all = (IDictionary<string, OutputCacheItemProxy>) null;
              this.updates = 0;
              if (fullUpdate || DateTime.UtcNow > this.lastReset.AddMinutes(5.0))
                all = this.GetAll(manager);
              else
                changes = this.GetChanges(manager);
              if (changes != null)
              {
                if (changes.Any<OutputCacheItem>())
                {
                  this.lastModified = changes.Max<OutputCacheItem, DateTime>((Func<OutputCacheItem, DateTime>) (i => i.DateModified)).AddSeconds(-1.0);
                  this.readWriteLock.EnterForWrite((Action) (() =>
                  {
                    foreach (OutputCacheItem outputCacheItem in changes)
                    {
                      if (outputCacheItem.Status == OutputCacheItemStatus.Removed || outputCacheItem.Status == OutputCacheItemStatus.Deleted)
                        this.items.Remove(outputCacheItem.Key);
                      else
                        this.items[outputCacheItem.Key] = new OutputCacheItemProxy(outputCacheItem);
                    }
                  }));
                }
              }
              else
              {
                this.readWriteLock.EnterForWrite((Action) (() => this.items = all));
                this.Init();
              }
              this.lastException = (Exception) null;
              return true;
            }
            catch (Exception ex)
            {
              this.fullUpdate = true;
              if (this.lastException != null && this.lastException.Message.Equals(ex.Message))
              {
                ApplicationException exceptionToHandle = new ApplicationException("Failed to update OutputCache proxy items cache: {0}".Arrange((object) ex.Message), ex);
                if (Telerik.Sitefinity.Abstractions.Exceptions.HandleException((Exception) exceptionToHandle, ExceptionPolicyName.IgnoreExceptions))
                  throw exceptionToHandle;
              }
              this.lastException = ex;
            }
            finally
            {
              manager.Dispose();
            }
          }
        }
        return false;
      }

      private IDictionary<string, OutputCacheItemProxy> GetAll(
        OutputCacheRelationsManager manager,
        bool firstCall = false)
      {
        IOrderedQueryable<OutputCacheItem> source = manager.Provider.GetOutputCacheItems().Where<OutputCacheItem>((Expression<Func<OutputCacheItem, bool>>) (oci => oci.SiteId == this.siteId && (int) oci.Status != 4 && (int) oci.Status != 5)).OrderByDescending<OutputCacheItem, DateTime>((Expression<Func<OutputCacheItem, DateTime>>) (i => i.DateModified));
        int count1 = Math.Max(CacheClientFactory.GetCacheInvalidationStrategy().OutputCacheItemsGetBatchSize, 1000);
        Dictionary<string, OutputCacheItemProxy> dictionary = source.Take<OutputCacheItem>(count1).ToDictionary<OutputCacheItem, string, OutputCacheItemProxy>((Func<OutputCacheItem, string>) (i => i.Key), (Func<OutputCacheItem, OutputCacheItemProxy>) (i => new OutputCacheItemProxy(i)));
        if (dictionary.Count == count1)
        {
          if (firstCall)
          {
            this.fullUpdate = true;
          }
          else
          {
            int num1 = 1;
            DateTime minDate = dictionary.Values.Min<OutputCacheItemProxy, DateTime>((Func<OutputCacheItemProxy, DateTime>) (i => i.DateModified));
            IList<OutputCacheItemProxy> outputCacheItemProxyList = (IList<OutputCacheItemProxy>) null;
            int num2 = 2;
            do
            {
              try
              {
                outputCacheItemProxyList = (IList<OutputCacheItemProxy>) source.Where<OutputCacheItem>((Expression<Func<OutputCacheItem, bool>>) (i => i.DateModified <= minDate)).Take<OutputCacheItem>(count1).Select<OutputCacheItem, OutputCacheItemProxy>((Expression<Func<OutputCacheItem, OutputCacheItemProxy>>) (i => new OutputCacheItemProxy(i))).ToList<OutputCacheItemProxy>();
                num2 = 2;
              }
              catch
              {
                --num2;
                if (num2 <= 0)
                  throw;
                else
                  goto label_18;
              }
              int count2 = dictionary.Count;
              foreach (OutputCacheItemProxy outputCacheItemProxy in (IEnumerable<OutputCacheItemProxy>) outputCacheItemProxyList)
                dictionary[outputCacheItemProxy.Key] = outputCacheItemProxy;
              minDate = dictionary.Values.Min<OutputCacheItemProxy, DateTime>((Func<OutputCacheItemProxy, DateTime>) (i => i.DateModified));
              if (count2 == dictionary.Count)
                minDate.AddSeconds(-1.0);
              ++num1;
label_18:;
            }
            while (outputCacheItemProxyList != null && outputCacheItemProxyList.Count == count1);
          }
          ++this.updates;
        }
        return (IDictionary<string, OutputCacheItemProxy>) dictionary;
      }

      private List<OutputCacheItem> GetChanges(
        OutputCacheRelationsManager manager)
      {
        return manager.Provider.GetOutputCacheItems().Where<OutputCacheItem>((Expression<Func<OutputCacheItem, bool>>) (oci => oci.SiteId == this.siteId && oci.DateModified > this.lastModified)).ToList<OutputCacheItem>();
      }

      private void Init()
      {
        this.lastModified = !this.items.Any<KeyValuePair<string, OutputCacheItemProxy>>() ? DateTime.UtcNow.AddSeconds(-1.0) : this.items.Values.Max<OutputCacheItemProxy, DateTime>((Func<OutputCacheItemProxy, DateTime>) (i => i.DateModified));
        this.lastReset = DateTime.UtcNow;
      }

      private void OnUpdate(ICacheDependencyHandler caller, Type itemType, string itemKey) => ++this.updates;

      private void OnFullUpdate(ICacheDependencyHandler caller, Type itemType, string itemKey)
      {
        this.fullUpdate = true;
        this.OnUpdate(caller, itemType, itemKey);
      }
    }

    internal interface ITypesCacheDependency
    {
    }

    internal class SupportedTypes
    {
      private string[] knownTypes = new string[1]
      {
        typeof (ISiteMapRootCacheDependency).FullName
      };
      private string[] blackListedTypes = new string[1]
      {
        typeof (OutputCacheItem).FullName
      };
      private const string TransactionName = "AddDependencyType";

      public IDictionary<string, int> PersistedTypes
      {
        get
        {
          ICacheManager cacheManager = SystemManager.GetCacheManager(CacheManagerInstance.OutputCacheInfo);
          string key = "oc_types_cache";
          if (!(cacheManager[key] is IDictionary<string, int> dictionary1))
          {
            lock (this)
            {
              if (!(cacheManager[key] is IDictionary<string, int> dictionary1))
              {
                OutputCacheRelationsManager manager = OutputCacheRelationsManager.GetManager((string) null, "AddDependencyType");
                try
                {
                  dictionary1 = (IDictionary<string, int>) manager.Provider.GetOutputCacheDependencyTypes().ToDictionary<OutputCacheDependencyType, string, int>((Func<OutputCacheDependencyType, string>) (d => d.TypeName), (Func<OutputCacheDependencyType, int>) (d => d.Id));
                }
                finally
                {
                  TransactionManager.DisposeTransaction("AddDependencyType");
                }
                cacheManager.Add(key, (object) dictionary1, CacheItemPriority.Normal, (ICacheItemRefreshAction) null, (ICacheItemExpiration) new DataItemCacheDependency(typeof (OutputCacheWorker.ITypesCacheDependency), string.Empty), (ICacheItemExpiration) new SlidingTime(TimeSpan.FromMinutes(10.0)));
              }
            }
          }
          return dictionary1;
        }
      }

      public IList<CacheDependencyKey> Filter(
        IEnumerable<CacheDependencyKey> items)
      {
        IDictionary<string, int> supportedTypes = this.PersistedTypes;
        List<CacheDependencyKey> list = items.Where<CacheDependencyKey>((Func<CacheDependencyKey, bool>) (i =>
        {
          if (!(i.Type != (Type) null))
            return false;
          return supportedTypes.Keys.Contains(i.Type.FullName) || ((IEnumerable<string>) this.knownTypes).Contains<string>(i.Type.FullName);
        })).ToList<CacheDependencyKey>();
        for (int index = list.Count - 1; index >= 0; --index)
        {
          if (!OutputCacheDependencyHelper.IsLiveContentKey(list[index]))
            list.RemoveAt(index);
        }
        return list.Count == 1 && list[0].Type.Equals(typeof (ISiteMapRootCacheDependency)) ? (IList<CacheDependencyKey>) new List<CacheDependencyKey>() : (IList<CacheDependencyKey>) list;
      }

      public int GetTypeId(string typeName, bool createIfNotExist = false)
      {
        int typeId;
        if (this.PersistedTypes.TryGetValue(typeName, out typeId))
          return typeId;
        OutputCacheRelationsManager manager = OutputCacheRelationsManager.GetManager((string) null, "AddDependencyType");
        OutputCacheDependencyType cacheDependencyType;
        try
        {
          cacheDependencyType = manager.Provider.CreateOutputCacheDependencyType(typeName);
          TransactionManager.CommitTransaction("AddDependencyType");
        }
        catch (Exception ex)
        {
          TransactionManager.RollbackTransaction("AddDependencyType");
          cacheDependencyType = manager.Provider.GetOutputCacheDependencyTypes().FirstOrDefault<OutputCacheDependencyType>((Expression<Func<OutputCacheDependencyType, bool>>) (i => i.TypeName == typeName));
          if (cacheDependencyType == null)
            throw;
        }
        finally
        {
          TransactionManager.DisposeTransaction("AddDependencyType");
        }
        return cacheDependencyType.Id;
      }

      public void Notify(HashSet<string> added)
      {
        IDictionary<string, int> persistedTypes = this.PersistedTypes;
        if (added.IsSubsetOf((IEnumerable<string>) persistedTypes.Keys))
          return;
        CacheDependency.Notify(new object[1]
        {
          (object) typeof (OutputCacheWorker.ITypesCacheDependency)
        });
      }
    }

    internal class OutputCacheWorkerBackgroundService : Telerik.Sitefinity.BackgroundTasks.BackgroundTasksService
    {
      /// <summary>
      /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.OutputCache.OutputCacheWorker.OutputCacheWorkerBackgroundService" /> class.
      /// </summary>
      public OutputCacheWorkerBackgroundService()
        : base(2)
      {
      }
    }

    private class DependenciesQueue
    {
      private ConcurrentQueue<OutputCacheWorker.OutputCacheDependencyItem> queue = new ConcurrentQueue<OutputCacheWorker.OutputCacheDependencyItem>();
      private List<Tuple<string, string, WeakReference<OutputCacheWorker.OutputCacheDependencyItem>>> list = new List<Tuple<string, string, WeakReference<OutputCacheWorker.OutputCacheDependencyItem>>>();

      public void Enqueue(
        string itemKey,
        IEnumerable<CacheDependencyKey> cacheDependencies,
        string siteId)
      {
        Dictionary<string, string> dependencies = new Dictionary<string, string>();
        foreach (CacheDependencyKey dependency in cacheDependencies.Where<CacheDependencyKey>((Func<CacheDependencyKey, bool>) (i => i.Type != (Type) null)))
        {
          string cacheDependencyKey = OutputCacheWorker.GetCacheDependencyKey(dependency);
          if (!dependencies.ContainsKey(cacheDependencyKey))
            dependencies.Add(cacheDependencyKey, dependency.Type.FullName);
        }
        this.queue.Enqueue(new OutputCacheWorker.OutputCacheDependencyItem(itemKey, (IDictionary<string, string>) dependencies, siteId));
      }

      public IDictionary<string, IDictionary<string, string>> Dequeue(
        int batchSize)
      {
        Dictionary<string, IDictionary<string, string>> dictionary = new Dictionary<string, IDictionary<string, string>>();
        OutputCacheWorker.OutputCacheDependencyItem result;
        while (this.queue.TryDequeue(out result))
        {
          if (result.State != OutputCacheWorker.OutputCacheDependencyItemState.Expired)
          {
            result.State = OutputCacheWorker.OutputCacheDependencyItemState.Processing;
            dictionary[result.Key] = result.Dependencies;
            if (dictionary.Count >= batchSize)
              break;
          }
        }
        return (IDictionary<string, IDictionary<string, string>>) dictionary;
      }

      public void NotifyExpired(string[] types, string[] keys, bool sendSystemMessage = true)
      {
        if (types.Length == 0 && keys.Length == 0)
          return;
        List<OutputCacheWorker.OutputCacheDependencyItem> list = this.queue.Where<OutputCacheWorker.OutputCacheDependencyItem>((Func<OutputCacheWorker.OutputCacheDependencyItem, bool>) (i => i.State == OutputCacheWorker.OutputCacheDependencyItemState.Pending && i.Dependencies.Any<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (d => ((IEnumerable<string>) types).Contains<string>(d.Value) || ((IEnumerable<string>) keys).Contains<string>(d.Key))))).ToList<OutputCacheWorker.OutputCacheDependencyItem>();
        List<string> stringList = new List<string>();
        foreach (OutputCacheWorker.OutputCacheDependencyItem cacheDependencyItem in list)
        {
          if (cacheDependencyItem.State == OutputCacheWorker.OutputCacheDependencyItemState.Pending)
          {
            cacheDependencyItem.State = OutputCacheWorker.OutputCacheDependencyItemState.Expired;
            stringList.Add(cacheDependencyItem.Key);
          }
        }
        if (stringList.Any<string>())
        {
          using (OutputCacheRelationsManager manager = OutputCacheRelationsManager.GetManager())
            manager.UpdateOutputCacheItemsStatusByKeys((IEnumerable<string>) stringList, OutputCacheItemStatus.New, OutputCacheItemStatus.Expired);
          OutputCacheWorker.NotifyExpiredItems();
        }
        if (!sendSystemMessage)
          return;
        SystemMessageDispatcher.QueueSystemMessage((SystemMessageBase) new OutputCacheExpirationMessage(types, keys));
      }

      public bool Any() => this.queue.Any<OutputCacheWorker.OutputCacheDependencyItem>();
    }

    private class OutputCacheDependencyItem
    {
      private volatile OutputCacheWorker.OutputCacheDependencyItemState state;

      public OutputCacheDependencyItem(
        string key,
        IDictionary<string, string> dependencies,
        string siteId)
      {
        this.Key = key;
        this.Dependencies = dependencies;
        this.SiteId = siteId;
      }

      public string Key { get; private set; }

      public IDictionary<string, string> Dependencies { get; private set; }

      public string SiteId { get; private set; }

      public OutputCacheWorker.OutputCacheDependencyItemState State
      {
        get => this.state;
        set => this.state = value;
      }
    }

    private enum OutputCacheDependencyItemState
    {
      Pending,
      Processing,
      Expired,
    }
  }
}
