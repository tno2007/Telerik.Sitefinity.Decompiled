// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.OutputCache.CacheClientFactory
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.OutputCache.Client;

namespace Telerik.Sitefinity.Web.OutputCache
{
  internal static class CacheClientFactory
  {
    private static CacheProvider currentCacheProvider;
    private static ICacheClient client;
    private static CacheInvalidationStrategy strategy;

    static CacheClientFactory() => ManagerBase<ConfigProvider>.Executed += new EventHandler<ExecutedEventArgs>(CacheClientFactory.ConfigManager_Executed);

    public static CacheProvider CurrentCacheProvider => CacheClientFactory.currentCacheProvider;

    /// <summary>Gets the current cache client</summary>
    /// <returns>The current cache client</returns>
    public static ICacheClient GetClient()
    {
      if (CacheClientFactory.client == null)
        CacheClientFactory.client = CacheClientFactory.GetClient(Config.Get<SystemConfig>().CacheSettings.DefaultCacheProvider);
      return CacheClientFactory.client;
    }

    /// <summary>Gets the current cache invalidation strategy</summary>
    /// <returns>The current cache client</returns>
    public static CacheInvalidationStrategy GetCacheInvalidationStrategy()
    {
      if (CacheClientFactory.strategy == null)
        CacheClientFactory.strategy = CacheClientFactory.GetStrategy(Config.Get<SystemConfig>().CacheSettings.PageCacheInvalidation);
      return CacheClientFactory.strategy;
    }

    private static ICacheClient GetClient(CacheProvider providerName)
    {
      CacheClientFactory.currentCacheProvider = providerName;
      switch (providerName)
      {
        case CacheProvider.InMemory:
          return (ICacheClient) new InMemoryCacheClient();
        case CacheProvider.SQLServer:
          return (ICacheClient) new SQLServerCacheClient();
        case CacheProvider.Redis:
          return (ICacheClient) new RedisCacheClient();
        case CacheProvider.Memcached:
          return (ICacheClient) new MemcachedClient();
        case CacheProvider.AwsDynamoDB:
          return (ICacheClient) new AwsDynamoDBCacheClient();
        case CacheProvider.Custom:
          return ObjectFactory.Resolve<ICacheClient>();
        default:
          string str = "Unknown cache provider was specified!";
          Log.Error(str);
          throw new ArgumentException(str, nameof (providerName));
      }
    }

    private static CacheInvalidationStrategy GetStrategy(
      CacheInvalidationConfigElement setting)
    {
      CacheInvalidationStrategy strategy = new CacheInvalidationStrategy();
      strategy.CacheExpirationMaxDelay = setting.CacheExpirationMaxDelay;
      strategy.BatchSize = setting.BatchSize;
      strategy.CacheItemHostHeaderName = setting.CacheItemHostHeaderName;
      strategy.WarmupLevel = setting.Warmup;
      string parameter1 = setting.Parameters["cacheDependencyAggregationTreshold"];
      int result1;
      if (parameter1.IsNullOrEmpty() || !int.TryParse(parameter1, out result1))
        result1 = 20;
      strategy.CacheDependencyAggregationTreshold = result1;
      string parameter2 = setting.Parameters["illegalCacheByParamVariationsLimit"];
      if (parameter2.IsNullOrEmpty() || !int.TryParse(parameter2, out result1))
        result1 = 100;
      strategy.IllegalCacheByParamVariationsLimit = result1;
      string parameter3 = setting.Parameters["dependenciesUpdateBatchSize"];
      if (parameter3.IsNullOrEmpty() || !int.TryParse(parameter3, out result1))
        result1 = 20;
      strategy.DependenciesUpdateBatchSize = result1;
      string parameter4 = setting.Parameters["outputCacheItemsGetBatchSize"];
      if (parameter4.IsNullOrEmpty() || !int.TryParse(parameter4, out result1))
        result1 = 100000;
      strategy.OutputCacheItemsGetBatchSize = result1;
      string parameter5 = setting.Parameters["useHostFromOriginalRequest"];
      bool result2;
      if (parameter5.IsNullOrEmpty() || !bool.TryParse(parameter5, out result2))
        result2 = false;
      strategy.UseHostFromOriginalRequest = result2;
      string parameter6 = setting.Parameters["maxParallelTasks"];
      if (parameter6.IsNullOrEmpty() || !int.TryParse(parameter6, out result1))
        result1 = 0;
      strategy.MaxParallelTasks = result1;
      return strategy;
    }

    private static void ConfigManager_Executed(object sender, ExecutedEventArgs args)
    {
      if (args == null || !(args.CommandName == "SaveSection") || !(args.CommandArguments.GetType() == typeof (SystemConfig)))
        return;
      CacheClientFactory.client = (ICacheClient) null;
      CacheClientFactory.strategy = (CacheInvalidationStrategy) null;
    }
  }
}
