// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Configuration.CacheInvalidationConfigElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Specialized;
using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.OutputCache;

namespace Telerik.Sitefinity.Modules.Pages.Configuration
{
  /// <summary>Represents Cache Invalidation settings.</summary>
  public class CacheInvalidationConfigElement : ConfigElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Configuration.CacheInvalidationConfigElement" /> class.
    /// </summary>
    /// <param name="parent">Parent configuration element.</param>
    public CacheInvalidationConfigElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Configuration.CacheInvalidationConfigElement" /> class.
    /// </summary>
    internal CacheInvalidationConfigElement()
      : base(false)
    {
    }

    /// <summary>
    /// Gets or sets the output cache invalidation expiration maximum delay time in seconds. The default is 120.
    /// </summary>
    [ConfigurationProperty("cacheExpirationMaxDelay", DefaultValue = 120)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CacheExpirationMaxDelayDescription", Title = "CacheExpirationMaxDelayTitle")]
    public int CacheExpirationMaxDelay
    {
      get => (int) this["cacheExpirationMaxDelay"];
      set => this["cacheExpirationMaxDelay"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the number of cache items invalidated in a single batch. The default is 100.
    /// </summary>
    [ConfigurationProperty("batchSize", DefaultValue = 100)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "BatchSizeDescription", Title = "BatchSizeTitle")]
    public int BatchSize
    {
      get => (int) this["batchSize"];
      set => this["batchSize"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the name of the header that will contain the host name for the output cache item. The default is "host".
    /// </summary>
    [ConfigurationProperty("cacheItemHostHeaderName", DefaultValue = "Host")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CacheItemHostHeaderNameDescription", Title = "CacheItemHostHeaderNameTitle")]
    public string CacheItemHostHeaderName
    {
      get => (string) this["cacheItemHostHeaderName"];
      set => this["cacheItemHostHeaderName"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether page should be warmup after it has been invalidated.
    /// </summary>
    [ConfigurationProperty("warmup", DefaultValue = WarmupLevel.None)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CacheInvalidationWarmupDescription", Title = "CacheInvalidationWarmupTitle")]
    public WarmupLevel Warmup
    {
      get => (WarmupLevel) this["warmup"];
      set => this["warmup"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a collection of user-defined parameters the cache invalidation strategy.
    /// </summary>
    [ConfigurationProperty("parameters")]
    public NameValueCollection Parameters
    {
      get => (NameValueCollection) this["parameters"];
      set => this["parameters"] = (object) value;
    }

    internal class PropNames
    {
      internal const string CacheExpirationMaxDelay = "cacheExpirationMaxDelay";
      internal const string BatchSize = "batchSize";
      internal const string CacheItemHostHeaderName = "cacheItemHostHeaderName";
      internal const string Warmup = "warmup";
      internal const string CacheDependencyAggregationTreshold = "cacheDependencyAggregationTreshold";
      internal const string IllegalCacheByParamVariationsLimit = "illegalCacheByParamVariationsLimit";
      internal const string DependenciesUpdateBatchSize = "dependenciesUpdateBatchSize";
      internal const string OutputCacheItemsGetBatchSize = "outputCacheItemsGetBatchSize";
      internal const string UseHostFromOriginalRequest = "useHostFromOriginalRequest";
      internal const string MaxParallelTasks = "maxParallelTasks";
    }
  }
}
