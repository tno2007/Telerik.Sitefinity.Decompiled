// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.OutputCache.CacheInvalidationStrategy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.HealthMonitoring;
using Telerik.Sitefinity.Localization.UrlLocalizationStrategies;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.OutputCache;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Events;
using Telerik.Sitefinity.SyncLock;
using Telerik.Sitefinity.Web.OutputCache.Data;

namespace Telerik.Sitefinity.Web.OutputCache
{
  internal class CacheInvalidationStrategy
  {
    private UrlLocalizationService urlLocalizationService;
    private static readonly string LockId = nameof (CacheInvalidationStrategy);

    internal int CacheExpirationMaxDelay { get; set; }

    internal int BatchSize { get; set; }

    internal string CacheItemHostHeaderName { get; set; }

    internal WarmupLevel WarmupLevel { get; set; }

    /// <summary>
    /// Gets or sets the number of cache dependency keys per type that will be persisted prior to aggregate them in one cache dependency of that type without key.
    /// </summary>
    internal int CacheDependencyAggregationTreshold { get; set; }

    /// <summary>
    /// Gets or sets the number of maximum output cache items of illegal cache variations, such as query string variations when varyByParams is *.
    /// </summary>
    internal int IllegalCacheByParamVariationsLimit { get; set; }

    internal int DependenciesUpdateBatchSize { get; set; }

    internal int OutputCacheItemsGetBatchSize { get; set; }

    internal bool UseHostFromOriginalRequest { get; set; }

    internal int MaxParallelTasks { get; set; }

    internal bool TryRun()
    {
      using (LockRegion<OutputCacheRelationsManager> syncLock = new LockRegion<OutputCacheRelationsManager>(CacheInvalidationStrategy.LockId, TimeSpan.FromMinutes(2.0)))
      {
        if (syncLock.TryAcquire())
        {
          this.Execute(syncLock);
          return true;
        }
      }
      return false;
    }

    private void Execute(LockRegion<OutputCacheRelationsManager> syncLock)
    {
      OutputCacheRelationsManager manager = OutputCacheRelationsManager.GetManager();
      Dictionary<string, string> hostCache = new Dictionary<string, string>();
      OutputCacheWorker.UpdateProxyCache();
      using (new MethodPerformanceRegion("Process (Invalidate/Warmup) expired pages."))
      {
        while (true)
        {
          bool flag = true;
          IList<OutputCacheItem> list1;
          using (new MethodPerformanceRegion("Query {0} expired pages".Arrange((object) this.BatchSize)))
          {
            DateTime maxDelayDate = DateTime.UtcNow.AddSeconds((double) -this.CacheExpirationMaxDelay);
            list1 = (IList<OutputCacheItem>) manager.Provider.GetOutputCacheItems().Where<OutputCacheItem>((Expression<Func<OutputCacheItem, bool>>) (i => (int) i.Status == 3 && i.DateModified < maxDelayDate)).OrderBy<OutputCacheItem, DateTime>((Expression<Func<OutputCacheItem, DateTime>>) (i => i.DateModified)).Take<OutputCacheItem>(this.BatchSize).ToList<OutputCacheItem>();
            if (!list1.Any<OutputCacheItem>())
            {
              list1 = (IList<OutputCacheItem>) manager.Provider.GetOutputCacheItems().Where<OutputCacheItem>((Expression<Func<OutputCacheItem, bool>>) (i => (int) i.Status == 3 || (int) i.Status == 6 || (int) i.Status == 5)).OrderBy<OutputCacheItem, DateTime>((Expression<Func<OutputCacheItem, DateTime>>) (i => i.DateModified)).Take<OutputCacheItem>(this.BatchSize).ToList<OutputCacheItem>();
              flag = false;
            }
          }
          if (list1.Any<OutputCacheItem>())
          {
            using (new MethodPerformanceRegion("Process {0} pages with max {1} warmup thread(s) in parallel".Arrange((object) list1.Count, (object) this.MaxParallelTasks)))
            {
              syncLock.Update();
              List<IOutputCacheItem> outputCacheItemList = new List<IOutputCacheItem>();
              HashSet<string> stringSet1 = new HashSet<string>();
              HashSet<string> stringSet2 = new HashSet<string>();
              HashSet<string> source1 = new HashSet<string>();
              List<Task> source2 = new List<Task>();
              SemaphoreSlim semaphore = new SemaphoreSlim(this.MaxParallelTasks > 0 ? this.MaxParallelTasks : 1);
              try
              {
                foreach (OutputCacheItem cacheItem in (IEnumerable<OutputCacheItem>) list1)
                {
                  string str = this.ResolveHost(cacheItem, hostCache);
                  if (!flag && cacheItem.Status != OutputCacheItemStatus.Deleted && cacheItem.Priority <= 50 && (this.WarmupLevel == WarmupLevel.Full || this.WarmupLevel == WarmupLevel.Light && cacheItem.Priority == 1))
                  {
                    CacheInvalidationStrategy.WarmupRequestInfo warmupRequestInfo = this.GetWarmupRequestInfo(cacheItem, str);
                    semaphore.Wait();
                    source2.Add((Task) this.StartWarmupTask(warmupRequestInfo).ContinueWith<int>((Func<Task, int>) (t => semaphore.Release())));
                  }
                  if (cacheItem.Status == OutputCacheItemStatus.Deleted)
                    stringSet1.Add(cacheItem.SiteMapNodeKey);
                  else
                    stringSet2.Add(cacheItem.SiteMapNodeKey);
                  if (cacheItem.Status == OutputCacheItemStatus.NotifiedExpired)
                    source1.Add(cacheItem.Key);
                  else
                    outputCacheItemList.Add((IOutputCacheItem) new CacheInvalidationStrategy.OutputCacheItemInternal(cacheItem, str));
                }
              }
              catch (Exception ex)
              {
                Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions);
              }
              if (source2.Any<Task>())
              {
                using (new MethodPerformanceRegion("Wait warmup to complete"))
                  Task.WaitAll(source2.ToArray());
              }
              if (flag)
              {
                manager.UpdateOutputCacheItemsStatusByKeys(list1.Select<OutputCacheItem, string>((Func<OutputCacheItem, string>) (i => i.Key)), OutputCacheItemStatus.Expired, OutputCacheItemStatus.NotifiedExpired);
              }
              else
              {
                Type dependencyType;
                if (stringSet1.Any<string>())
                {
                  manager.DeleteItemsByPageNodeKeys((IEnumerable<string>) stringSet1);
                  dependencyType = typeof (OutputCacheWorker.OutputCacheItemsCache);
                }
                else
                  dependencyType = typeof (OutputCacheItem);
                if (stringSet2.Any<string>())
                  manager.UpdatePageVariationsStatus((IEnumerable<string>) stringSet2, OutputCacheItemStatus.Live, OutputCacheItemStatus.Expired);
                if (source1.Any<string>())
                  manager.UpdateOutputCacheItemsStatusByKeys(list1.Select<OutputCacheItem, string>((Func<OutputCacheItem, string>) (i => i.Key)), OutputCacheItemStatus.NotifiedExpired, OutputCacheItemStatus.Removed);
                manager.UpdateOutputCacheItemsStatusByKeys(outputCacheItemList.Select<IOutputCacheItem, string>((Func<IOutputCacheItem, string>) (i => i.Key)), OutputCacheItemStatus.Expired, OutputCacheItemStatus.Removed);
                string[] allKeys = list1.Select<OutputCacheItem, string>((Func<OutputCacheItem, string>) (i => i.Key)).ToArray<string>();
                \u003C\u003Ef__AnonymousType27<string, string, Guid>[] array = manager.Provider.GetOutputCacheItems().Where<OutputCacheItem>((Expression<Func<OutputCacheItem, bool>>) (i => allKeys.Contains<string>(i.Key) && (int) i.Status == 4)).Select(i => new
                {
                  Key = i.Key,
                  CacheVaryKey = i.CacheVaryKey,
                  SiteId = i.SiteId
                }).ToArray();
                CacheDependency.Notify((IList<CacheDependencyKey>) array.Select(i => i.SiteId).Distinct<Guid>().ToList<Guid>().Select<Guid, CacheDependencyKey>((Func<Guid, CacheDependencyKey>) (i => new CacheDependencyKey()
                {
                  Type = dependencyType,
                  Key = i.ToString()
                })).ToList<CacheDependencyKey>());
                List<string> list2 = array.Select(i => i.Key).ToList<string>();
                list2.AddRange(array.Where(i => !i.CacheVaryKey.IsNullOrEmpty()).Select(i => i.CacheVaryKey).Distinct<string>());
                if (list2.Any<string>())
                {
                  if (CacheClientFactory.CurrentCacheProvider == CacheProvider.InMemory)
                    CacheDependency.Notify((IList<CacheDependencyKey>) list2.Select<string, CacheDependencyKey>((Func<string, CacheDependencyKey>) (k => new CacheDependencyKey()
                    {
                      Type = typeof (InMemoryOutputCacheDependency),
                      Key = k
                    })).ToList<CacheDependencyKey>());
                  list2.ForEach((Action<string>) (k => SitefinityOutputCacheProvider.Instance.RemoveAsync(k)));
                }
              }
              if (outputCacheItemList.Any<IOutputCacheItem>())
                EventHub.Raise((IEvent) new CacheInvalidationStrategy.CacheInvalidationEvent((IEnumerable<IOutputCacheItem>) outputCacheItemList));
            }
          }
          else
            break;
        }
      }
    }

    private CacheInvalidationStrategy.WarmupRequestInfo GetWarmupRequestInfo(
      OutputCacheItem cacheItem,
      string originalHost)
    {
      try
      {
        using (SiteRegion.FromSiteId(cacheItem.SiteId))
        {
          Uri uri;
          UriBuilder uriBuilder = !SystemManager.TryResolveWarmupUrl(out uri) ? new UriBuilder(SystemManager.RootUrl) : new UriBuilder(uri);
          string[] strArray = cacheItem.Url.Split(new char[1]
          {
            '?'
          }, 2);
          uriBuilder.Path = strArray[0];
          if (strArray.Length > 1)
            uriBuilder.Query = strArray[1];
          string authority = new Uri(originalHost).Authority;
          return new CacheInvalidationStrategy.WarmupRequestInfo()
          {
            Url = uriBuilder.Uri.AbsoluteUri,
            Domain = authority
          };
        }
      }
      catch (Exception ex)
      {
        if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
          throw;
      }
      return (CacheInvalidationStrategy.WarmupRequestInfo) null;
    }

    private Task StartWarmupTask(
      CacheInvalidationStrategy.WarmupRequestInfo requestInfo)
    {
      return Task.Run((Action) (() =>
      {
        HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(requestInfo.Url);
        httpWebRequest.Method = "GET";
        httpWebRequest.Headers.Add(HttpRequestHeader.CacheControl, "no-cache");
        httpWebRequest.Headers.Add("Sf-Cache-Ignore-Code", SitefinityOutputCacheModule.GetWarmupIgnoreCode());
        if (!string.IsNullOrWhiteSpace(this.CacheItemHostHeaderName))
        {
          if (this.CacheItemHostHeaderName.Equals(System.Enum.GetName(typeof (HttpRequestHeader), (object) HttpRequestHeader.Host), StringComparison.InvariantCultureIgnoreCase))
            httpWebRequest.Host = requestInfo.Domain;
          else
            httpWebRequest.Headers[this.CacheItemHostHeaderName] = requestInfo.Domain;
        }
        try
        {
          using (httpWebRequest.GetResponse())
            ;
        }
        catch (WebException ex)
        {
          Telerik.Sitefinity.Abstractions.Log.Write((object) string.Format("The page '{0}' failed to warmup with error: {1}. Requested URL: {2}", (object) requestInfo.Url, (object) ex.Message, (object) requestInfo.Url), TraceEventType.Information);
        }
        catch (Exception ex)
        {
          if (!Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
            return;
          throw;
        }
      }));
    }

    private string ResolveHost(OutputCacheItem cacheItem, Dictionary<string, string> hostCache)
    {
      if (this.UseHostFromOriginalRequest && !cacheItem.Host.IsNullOrEmpty())
        return cacheItem.Host;
      string key = cacheItem.SiteId.ToString() + (cacheItem.Language ?? string.Empty);
      string leftPart;
      if (!hostCache.TryGetValue(key, out leftPart))
      {
        lock (hostCache)
        {
          if (!hostCache.TryGetValue(key, out leftPart))
          {
            using (SiteRegion.FromSiteId(cacheItem.SiteId))
            {
              Uri result = (Uri) null;
              if (!string.IsNullOrEmpty(cacheItem.Language))
              {
                IUrlLocalizationStrategy localizationStrategy = this.UrlLocalizationService.CurrentUrlLocalizationStrategy;
                Uri.TryCreate(!(localizationStrategy is DomainUrlLocalizationStrategy) ? this.UrlLocalizationService.ResolveUrl("~/", CultureInfo.GetCultureInfo(cacheItem.Language)) : (localizationStrategy as DomainUrlLocalizationStrategy).ResolveUrl("~/", CultureInfo.GetCultureInfo(cacheItem.Language), true), UriKind.Absolute, out result);
              }
              if (result == (Uri) null)
                result = SystemManager.CurrentContext.CurrentSite.GetUri();
              leftPart = result.GetLeftPart(UriPartial.Authority);
              hostCache.Add(key, leftPart);
            }
          }
        }
      }
      return leftPart;
    }

    private string ResolveLocalUrl(Uri siteUri, string path, string publicDoamin = null)
    {
      UriBuilder uriBuilder = new UriBuilder(path);
      publicDoamin = uriBuilder.Uri.Authority;
      uriBuilder.Scheme = siteUri.Scheme;
      uriBuilder.Port = siteUri.Port;
      uriBuilder.Host = siteUri.Host;
      return uriBuilder.Uri.AbsoluteUri;
    }

    private string GetOrAddSiteHost(Guid siteId, Dictionary<Guid, string> sitesHosts)
    {
      string orAddSiteHost = (string) null;
      if (sitesHosts.ContainsKey(siteId))
      {
        orAddSiteHost = sitesHosts[siteId];
      }
      else
      {
        IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
        ISite site = multisiteContext == null ? SystemManager.CurrentContext.CurrentSite : multisiteContext.GetSiteById(siteId);
        if (site != null)
        {
          Uri uri = site.GetUri();
          if (uri != (Uri) null)
          {
            orAddSiteHost = uri.GetComponents(UriComponents.SchemeAndServer, UriFormat.SafeUnescaped);
            sitesHosts.Add(siteId, orAddSiteHost);
          }
        }
      }
      return orAddSiteHost;
    }

    private UrlLocalizationService UrlLocalizationService
    {
      get
      {
        if (this.urlLocalizationService == null)
          this.urlLocalizationService = ObjectFactory.Resolve<UrlLocalizationService>();
        return this.urlLocalizationService;
      }
    }

    private class WarmupRequestInfo
    {
      public string Url { get; set; }

      public string Domain { get; set; }
    }

    private class OutputCacheItemInternal : IOutputCacheItem
    {
      public OutputCacheItemInternal(OutputCacheItem item, string host)
      {
        this.Key = item.Key;
        this.ETag = item.ETag;
        this.SiteId = item.SiteId;
        this.Url = item.Url;
        this.Host = host;
      }

      public string Key { get; private set; }

      public Guid SiteId { get; private set; }

      public string ETag { get; private set; }

      public string Url { get; private set; }

      public string Host { get; private set; }
    }

    private class CacheInvalidationEvent : IOutputCacheInvalidationEvent, IEvent
    {
      public CacheInvalidationEvent(IEnumerable<IOutputCacheItem> items) => this.ExpiredItems = items;

      public IEnumerable<IOutputCacheItem> ExpiredItems { get; private set; }

      public string Origin { get; set; }
    }
  }
}
