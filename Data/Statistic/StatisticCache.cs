// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Statistic.StatisticCache
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Data.Statistic
{
  internal class StatisticCache
  {
    private IDictionary<string, StatisticCache.StatisticSupportProxy> pendingManagers = (IDictionary<string, StatisticCache.StatisticSupportProxy>) new Dictionary<string, StatisticCache.StatisticSupportProxy>();
    private object cachSyncLock = new object();
    private object registrySyncLock = new object();
    private IDictionary<string, StatisticCache.StatisticSupportProxy> registry = (IDictionary<string, StatisticCache.StatisticSupportProxy>) new Dictionary<string, StatisticCache.StatisticSupportProxy>();
    private IDictionary<Guid, StatisticCache.StatisticExecutionInfo> siteNodeMapping = (IDictionary<Guid, StatisticCache.StatisticExecutionInfo>) new Dictionary<Guid, StatisticCache.StatisticExecutionInfo>();
    private static StatisticCache currentInstance;
    private static object syncLock = new object();

    internal static StatisticCache Current
    {
      get
      {
        StatisticCache current = StatisticCache.currentInstance;
        if (current == null)
        {
          lock (StatisticCache.syncLock)
          {
            if (StatisticCache.currentInstance == null)
            {
              current = new StatisticCache();
              StatisticCache.currentInstance = current;
            }
          }
        }
        return current;
      }
      set
      {
        lock (StatisticCache.syncLock)
          StatisticCache.currentInstance = (StatisticCache) null;
      }
    }

    public void RegisterStatisticSupport<TStatisticSupport>(string moduleName = null, object[] arguments = null) where TStatisticSupport : IContentStatisticSupport => this.RegisterStatisticSupport(typeof (TStatisticSupport), moduleName, arguments);

    public void RegisterStatisticSupport(Type managerType, string moduleName = null, object[] arguments = null)
    {
      this.ValidateManagerType(managerType);
      StatisticCache.StatisticSupportProxy statisticSupportProxy = new StatisticCache.StatisticSupportProxy(managerType, moduleName, arguments);
      lock (this.registrySyncLock)
        this.pendingManagers[statisticSupportProxy.Key] = statisticSupportProxy;
    }

    public bool TryGetItemsCountPerPage(PageSiteNode siteNode, out int value)
    {
      object result;
      if (this.TryGetPageStatistic(siteNode, "Count", out result))
      {
        value = (int) result;
        return true;
      }
      value = 0;
      return false;
    }

    public bool TryGetTotalItemsCountPerPage(PageSiteNode siteNode, out int value)
    {
      object result;
      if (this.TryGetPageStatistic(siteNode, "Count", out result, true))
      {
        value = (int) result;
        return true;
      }
      value = 0;
      return false;
    }

    public bool TryGetPageStatistic(
      PageSiteNode siteNode,
      string statisticKind,
      out object result,
      bool forAllProviders = false)
    {
      this.EnsureRegistryIsUpToDate();
      siteNode = this.ResolveActualPageSiteNode(siteNode);
      StatisticCache.StatisticExecutionInfo statisticExecutionInfo;
      if (this.siteNodeMapping.TryGetValue(siteNode.Id, out statisticExecutionInfo) && statisticExecutionInfo.SupportsStatistic(statisticKind))
      {
        result = forAllProviders ? this.GetStatisticForAllProviders(statisticExecutionInfo.Type, statisticKind, statisticExecutionInfo.Filter) : this.GetStatistic(statisticExecutionInfo.Type, statisticKind, statisticExecutionInfo.Filter);
        return true;
      }
      result = (object) null;
      return false;
    }

    public int? GetItemsCount(Type contentType, string filter, string provider = null)
    {
      object statistic = this.GetStatistic(contentType, "Count", filter, provider);
      return statistic != null && statistic is int num ? new int?(num) : new int?();
    }

    public object GetStatistic(
      Type contentType,
      string statisticKind,
      string filter,
      string provider = null)
    {
      return this.GetStatistic(contentType, statisticKind, filter, false, provider);
    }

    public object GetStatisticForAllProviders(
      Type contentType,
      string statisticKind,
      string filter)
    {
      return this.GetStatistic(contentType, statisticKind, filter, true);
    }

    private object GetStatistic(
      Type contentType,
      string statisticKind,
      string filter,
      bool forAllProviders,
      string provider = null)
    {
      if (!string.Equals(statisticKind, "Count"))
        throw new NotSupportedException(statisticKind);
      this.EnsureRegistryIsUpToDate();
      string statisticKey = this.GetStatisticKey(contentType, statisticKind);
      StatisticCache.StatisticSupportProxy statisticSupportProxy;
      if (!this.registry.TryGetValue(statisticKey, out statisticSupportProxy))
        return (object) null;
      IList<string> stringList;
      if (forAllProviders)
      {
        stringList = (IList<string>) new List<string>(statisticSupportProxy.ResolveAllProviders());
      }
      else
      {
        if (provider == null)
          provider = statisticSupportProxy.ResolveDefaultProvider();
        stringList = (IList<string>) new List<string>()
        {
          provider
        };
      }
      int statistic1 = 0;
      foreach (string provider1 in (IEnumerable<string>) stringList)
      {
        string cacheKey = statisticSupportProxy.GetCacheKey(statisticKey, provider1, filter);
        object data = this.Cache.GetData(cacheKey);
        if (data == null)
        {
          lock (this.cachSyncLock)
          {
            if (data == null)
            {
              StatisticResult statistic2 = statisticSupportProxy.GetStatistic(contentType, statisticKind, provider1, filter);
              if (statistic2 == null)
              {
                this.UnregisterStatisticSupport(statisticKey);
                return (object) null;
              }
              data = statistic2.Value;
              ICacheItemExpiration cacheItemExpiration = statistic2.CacheDependency ?? (ICacheItemExpiration) new AbsoluteTime(TimeSpan.FromMinutes(10.0));
              this.Cache.Add(cacheKey, data, CacheItemPriority.Normal, (ICacheItemRefreshAction) null, cacheItemExpiration);
            }
          }
        }
        statistic1 += (int) data;
      }
      return (object) statistic1;
    }

    private PageSiteNode ResolveActualPageSiteNode(PageSiteNode pageSiteNode) => !string.IsNullOrEmpty(pageSiteNode.ModuleName) ? RouteHelper.GetFirstPageDataNode(pageSiteNode, false) : pageSiteNode;

    private void RegisterPageTypeStatisticMapping(
      Guid pageId,
      Type type,
      string[] supportedStatistics,
      string filter = null)
    {
      lock (this.siteNodeMapping)
        this.siteNodeMapping[pageId] = new StatisticCache.StatisticExecutionInfo(type, supportedStatistics, filter);
    }

    private void UnregisterStatisticSupport(string statKey)
    {
      lock (this)
        this.registry.Remove(statKey);
    }

    private void ValidateManagerType(Type managerType)
    {
      if (!typeof (IContentStatisticSupport).IsAssignableFrom(managerType))
        throw new ArgumentException("Parameter 'managerType' should be of type 'IContentStatisticSupport'", nameof (managerType));
    }

    private bool TryGetPageModuleName(PageSiteNode siteNode, out string moduleName)
    {
      moduleName = (string) null;
      do
      {
        moduleName = siteNode.ModuleName;
        if (!moduleName.IsNullOrEmpty())
          return true;
        siteNode = siteNode.ParentNode as PageSiteNode;
      }
      while (siteNode != null);
      return false;
    }

    private ICacheManager Cache => SystemManager.GetCacheManager(CacheManagerInstance.Statistics);

    private void EnsureRegistryIsUpToDate()
    {
      if (!this.pendingManagers.Any<KeyValuePair<string, StatisticCache.StatisticSupportProxy>>())
        return;
      lock (this.registrySyncLock)
      {
        if (!this.pendingManagers.Any<KeyValuePair<string, StatisticCache.StatisticSupportProxy>>())
          return;
        foreach (StatisticCache.StatisticSupportProxy statisticSupportProxy in (IEnumerable<StatisticCache.StatisticSupportProxy>) this.pendingManagers.Values)
        {
          try
          {
            this.RegisterManager(this.registry, statisticSupportProxy);
          }
          catch (Exception ex)
          {
            if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
              throw;
          }
        }
        this.pendingManagers.Clear();
      }
    }

    private void RegisterManager(
      IDictionary<string, StatisticCache.StatisticSupportProxy> registry,
      StatisticCache.StatisticSupportProxy statisticSupportProxy)
    {
      foreach (IStatisticSupportTypeInfo typeInfo in statisticSupportProxy.GetTypeInfos())
      {
        IEnumerable<StatisticLandingPageInfo> landingPages = typeInfo.LandingPages;
        if (landingPages != null)
        {
          foreach (StatisticLandingPageInfo statisticLandingPageInfo in landingPages)
            this.RegisterPageTypeStatisticMapping(statisticLandingPageInfo.PageId, typeInfo.Type, typeInfo.SupportedStatistics.ToArray<string>(), statisticLandingPageInfo.Filter);
        }
        foreach (string supportedStatistic in typeInfo.SupportedStatistics)
        {
          string statisticKey = this.GetStatisticKey(typeInfo.Type, supportedStatistic);
          registry[statisticKey] = statisticSupportProxy;
        }
      }
    }

    private string GetStatisticKey(Type type, string statisticKind) => type.FullName + "_" + statisticKind;

    private class StatisticSupportProxy
    {
      private string key;
      private IContentStatisticSupport manager;
      private Type managerType;
      private object[] arguments;
      private Dictionary<Guid, string> defaultProviderMap = new Dictionary<Guid, string>();
      private Dictionary<Guid, IEnumerable<string>> allProvidersMap = new Dictionary<Guid, IEnumerable<string>>();

      public StatisticSupportProxy(Type managerType, string moduleName, object[] arguments)
      {
        this.managerType = managerType;
        this.ModuleName = moduleName;
        this.arguments = arguments;
      }

      public string Key
      {
        get
        {
          if (this.key == null)
            this.key = this.managerType.FullName + "_" + (this.ModuleName ?? string.Empty) + "_" + (this.arguments != null ? string.Join("_", this.arguments) : string.Empty);
          return this.key;
        }
      }

      public StatisticResult GetStatistic(
        Type type,
        string statisticKind,
        string provider,
        string filter = null)
      {
        return this.GetManager().GetStatistic(type, statisticKind, provider, filter);
      }

      public IEnumerable<IStatisticSupportTypeInfo> GetTypeInfos() => this.GetManager().GetTypeInfos(this.ModuleName);

      public string ModuleName { get; private set; }

      public string GetCacheKey(string statKey, string provider, string filter) => statKey + "_" + provider + "_" + (filter ?? string.Empty);

      public string ResolveDefaultProvider()
      {
        Guid id = SystemManager.CurrentContext.CurrentSite.Id;
        string defaultProviderName;
        if (!this.defaultProviderMap.TryGetValue(id, out defaultProviderName))
        {
          lock (this.defaultProviderMap)
          {
            if (!this.defaultProviderMap.TryGetValue(id, out defaultProviderName))
            {
              defaultProviderName = this.GetManager().GetDefaultProviderName(this.ModuleName);
              this.defaultProviderMap.Add(id, defaultProviderName);
            }
          }
        }
        return defaultProviderName;
      }

      public IEnumerable<string> ResolveAllProviders()
      {
        Guid id = SystemManager.CurrentContext.CurrentSite.Id;
        IEnumerable<string> providerNames;
        if (!this.allProvidersMap.TryGetValue(id, out providerNames))
        {
          lock (this.allProvidersMap)
          {
            if (!this.allProvidersMap.TryGetValue(id, out providerNames))
            {
              providerNames = this.GetManager().GetProviderNames();
              this.allProvidersMap.Add(id, providerNames);
            }
          }
        }
        IEnumerable<string> strings = providerNames;
        if (strings != null)
          return strings;
        return (IEnumerable<string>) new List<string>()
        {
          this.ResolveDefaultProvider()
        };
      }

      private IContentStatisticSupport GetManager()
      {
        if (this.manager == null)
        {
          IContentStatisticSupport instance = (IContentStatisticSupport) Activator.CreateInstance(this.managerType, this.arguments);
          if (!instance.IsReusable)
            return instance;
          this.managerType = (Type) null;
          this.arguments = (object[]) null;
          this.manager = instance;
        }
        return this.manager;
      }
    }

    private class StatisticExecutionInfo
    {
      private string[] supportedStatistics;

      public StatisticExecutionInfo(IStatisticSupportTypeInfo typeInfo, string filter = null)
        : this(typeInfo.Type, typeInfo.SupportedStatistics.ToArray<string>(), filter)
      {
      }

      public StatisticExecutionInfo(Type type, string[] supportedStatistics, string filter = null)
      {
        this.Type = type;
        this.supportedStatistics = supportedStatistics;
        this.Filter = filter;
      }

      public string Filter { get; private set; }

      public Type Type { get; private set; }

      public bool SupportsStatistic(string statisticKind) => ((IEnumerable<string>) this.supportedStatistics).Contains<string>(statisticKind);
    }
  }
}
