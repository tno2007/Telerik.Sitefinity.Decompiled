// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.OutputCache.SitefinityOutputCacheProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Web.Routing;
using Telerik.OpenAccess.Exceptions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.OutputCache;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities;
using Telerik.Sitefinity.Web.OutputCache.Client;
using Telerik.Sitefinity.Web.OutputCache.Data;

namespace Telerik.Sitefinity.Web.OutputCache
{
  /// <summary>Handles output cache invalidation</summary>
  internal class SitefinityOutputCacheProvider : OutputCacheProviderAsync
  {
    /// <summary>The cache dependency items key</summary>
    private const string CacheDependencyItemsKey = "sf:CacheDependencyItems";
    private const string CacheVaryKeyKey = "sf:CacheVaryKey";
    private const string SubstitutionParametersKey = "sf:CacheSubParams";
    private const string SubstitutionParametersSetKey = "sf:CacheSubParamsSet";
    private const string CacheVariationsKey = "sf:CacheVariations";
    private const string CachedVaryKey = "sf:CachedVary";
    private const string IgnoreStatusCodeKey = "sf:IgnoreStatusCode";
    private static Dictionary<string, System.Web.Caching.CacheDependency> cacheDependencyMap = new Dictionary<string, System.Web.Caching.CacheDependency>();
    private static SitefinityOutputCacheProvider instance = (SitefinityOutputCacheProvider) null;
    private static object instanceSync = new object();
    private static volatile bool hasPendingExpiredItems = false;

    private SitefinityOutputCacheProvider()
    {
    }

    /// <summary>Gets the instance.</summary>
    /// <value>The instance.</value>
    public static SitefinityOutputCacheProvider Instance
    {
      get
      {
        if (SitefinityOutputCacheProvider.instance == null)
        {
          lock (SitefinityOutputCacheProvider.instanceSync)
          {
            if (SitefinityOutputCacheProvider.instance == null)
              SitefinityOutputCacheProvider.instance = new SitefinityOutputCacheProvider();
          }
        }
        return SitefinityOutputCacheProvider.instance;
      }
    }

    /// <summary>
    /// Registers the cache substitution call back parameters.
    /// </summary>
    /// <param name="cacheSubstitutionParameters">The cache substitution parameters.</param>
    public static void RegisterCacheSubstitutionCallbackParameters(
      Dictionary<string, string> cacheSubstitutionParameters)
    {
      if (cacheSubstitutionParameters == null)
        return;
      if (!(HttpContext.Current.Items[(object) "sf:CacheSubParams"] is Dictionary<string, string> dictionary))
      {
        HttpContext.Current.Items[(object) "sf:CacheSubParams"] = (object) cacheSubstitutionParameters;
      }
      else
      {
        foreach (KeyValuePair<string, string> substitutionParameter in cacheSubstitutionParameters)
        {
          if (!dictionary.ContainsKey(substitutionParameter.Key))
            dictionary.Add(substitutionParameter.Key, substitutionParameter.Value);
        }
      }
    }

    private ICacheClient CacheClient => CacheClientFactory.GetClient();

    /// <summary>Gets the substitution callback parameters.</summary>
    /// <param name="httpContext">The HttpContext</param>
    /// <returns>The substitution callback parameters.</returns>
    public static Dictionary<string, string> GetSubstitutionCallbackParameters(
      HttpContext httpContext)
    {
      return httpContext != null ? httpContext.Items[(object) "sf:CacheSubParams"] as Dictionary<string, string> : throw new ArgumentNullException(nameof (httpContext));
    }

    /// <summary>
    /// Asynchronously inserts the specified entry into the output cache.
    /// </summary>
    /// <param name="key">The unique key of the item to be cached.</param>
    /// <param name="entry">The item to add.</param>
    /// <param name="utcExpiry">When does it expire?</param>
    /// <returns>A task.</returns>
    public override Task<object> AddAsync(string key, object entry, DateTime utcExpiry)
    {
      bool shouldAddCacheEntry = this.TryHandleCacheDependencies(key, entry, ref utcExpiry);
      return Task.Run<object>((Func<object>) (() =>
      {
        if (shouldAddCacheEntry)
        {
          try
          {
            this.CacheClient.Set<byte[]>(key, BinarySerializer.Serialize(entry), utcExpiry);
          }
          catch (Exception ex)
          {
            if (Telerik.Sitefinity.Abstractions.Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
              throw;
          }
        }
        return entry;
      }));
    }

    /// <summary>
    /// Asynchronously returns a reference to the specified entry in the output cache.
    /// </summary>
    /// <param name="key">The unique key of the item to be cached.</param>
    /// <returns>The cache item</returns>
    public override Task<object> GetAsync(string key) => Task.FromResult<object>(this.Get(key));

    /// <summary>
    /// Asynchronously Inserts the specified entry into the output cache, overwriting the entry if it is already cached.
    /// </summary>
    /// <param name="key">The unique key of the item to be cached.</param>
    /// <param name="entry">The item to update.</param>
    /// <param name="utcExpiry">When does it expire?</param>
    /// <returns>A task.</returns>
    public override Task SetAsync(string key, object entry, DateTime utcExpiry) => Task.Run((Action) (() =>
    {
      try
      {
        this.CacheClient.Set<byte[]>(key, BinarySerializer.Serialize(entry), utcExpiry);
      }
      catch (Exception ex)
      {
        if (!Telerik.Sitefinity.Abstractions.Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
          return;
        throw;
      }
    }));

    /// <summary>
    /// Asynchronously removes the specified entry from the output cache.
    /// </summary>
    /// <param name="key">The unique key of the item to be removed.</param>
    /// <returns>A task.</returns>
    public override Task RemoveAsync(string key) => Task.Run((Action) (() => this.Remove(key)));

    /// <summary>
    /// Returns a reference to the specified entry in the output cache.
    /// </summary>
    /// <param name="key">The unique key of the item to be get.</param>
    /// <returns>A task.</returns>
    public override object Get(string key)
    {
      object obj = (object) null;
      try
      {
        byte[] data = this.CacheClient.Get<byte[]>(key);
        if (data != null)
        {
          obj = BinarySerializer.Deserialize(data);
          if (obj is OutputCacheEntry outputCacheEntry)
          {
            if (!Telerik.Sitefinity.Web.PageRouteHandler.IsCacheDependenciesPersistedInMemory)
            {
              CachedVary cachedVary;
              if (SitefinityOutputCacheProvider.TryGetCachedVaryFromContext(out cachedVary))
              {
                if (!string.IsNullOrEmpty(cachedVary.SiteId))
                {
                  OutputCacheItemProxy outputCacheItemProxy = OutputCacheWorker.GetOutputCacheItemProxy(key);
                  if (outputCacheItemProxy == null || !outputCacheItemProxy.Expired)
                  {
                    if (outputCacheItemProxy == null)
                    {
                      if (!(outputCacheEntry.Settings.UtcTimestampCreated < DateTime.UtcNow.AddSeconds(-10.0)))
                        goto label_13;
                    }
                    else
                      goto label_13;
                  }
                  this.CacheClient.Remove(key);
                  return (object) null;
                }
              }
            }
          }
        }
      }
      catch (Exception ex)
      {
        if (Telerik.Sitefinity.Abstractions.Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
          throw;
        else
          obj = (object) null;
      }
label_13:
      return obj;
    }

    /// <summary>Inserts the specified entry into the output cache.</summary>
    /// <param name="key">The unique key of the item to be cached.</param>
    /// <param name="entry">The item to add.</param>
    /// <param name="utcExpiry">When does it expire?</param>
    /// <returns>The cache item.</returns>
    public override object Add(string key, object entry, DateTime utcExpiry)
    {
      if (this.TryHandleCacheDependencies(key, entry, ref utcExpiry))
      {
        try
        {
          byte[] numArray = BinarySerializer.Serialize(entry);
          this.CacheClient.Set<byte[]>(key, numArray, utcExpiry);
        }
        catch (Exception ex)
        {
          if (Telerik.Sitefinity.Abstractions.Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
            throw;
        }
      }
      return entry;
    }

    /// <summary>
    /// Inserts the specified entry into the output cache, overwriting the entry if it is already cached
    /// </summary>
    /// <param name="key">The unique key of the item to be cached.</param>
    /// <param name="entry">The item to set.</param>
    /// <param name="utcExpiry">When does it expire?</param>
    public override void Set(string key, object entry, DateTime utcExpiry)
    {
      try
      {
        byte[] numArray = BinarySerializer.Serialize(entry);
        this.CacheClient.Set<byte[]>(key, numArray, utcExpiry);
      }
      catch (Exception ex)
      {
        if (!Telerik.Sitefinity.Abstractions.Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
          return;
        throw;
      }
    }

    /// <summary>Removes the specified entry from the output cache.</summary>
    /// <param name="key">The unique key of the item to be removed.</param>
    public override void Remove(string key)
    {
      try
      {
        this.CacheClient.Remove(key);
      }
      catch (Exception ex)
      {
        if (!Telerik.Sitefinity.Abstractions.Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
          return;
        throw;
      }
    }

    internal static void SetIgnoreStatusCodeValidation() => HttpContext.Current.Items[(object) "sf:IgnoreStatusCode"] = (object) true;

    internal static bool IgnoreStatusCodeValidation()
    {
      object obj = HttpContext.Current.Items[(object) "sf:IgnoreStatusCode"];
      return obj != null && (bool) obj;
    }

    internal static void StoreCacheDependenciesInContext(IList<CacheDependencyKey> cacheDependencies) => HttpContext.Current.Items.Add((object) "sf:CacheDependencyItems", (object) cacheDependencies);

    internal static void StoreCacheVaryKeyInContext(string cacheVaryKey, HttpContext httpContext = null) => (httpContext ?? HttpContext.Current).Items.Add((object) "sf:CacheVaryKey", (object) cacheVaryKey);

    internal static void SetSubstitutionInfoInContext(string substitutionInfo)
    {
      if (string.IsNullOrWhiteSpace(substitutionInfo))
        return;
      HttpContext.Current.Items[(object) "sf:CacheSubParams"] = (object) JsonConvert.DeserializeObject<Dictionary<string, string>>(substitutionInfo);
    }

    internal static void StoreCacheVariationsInContext(IList<ICustomOutputCacheVariation> variations)
    {
      if (!variations.Any<ICustomOutputCacheVariation>())
        return;
      HttpContext.Current.Items[(object) "sf:CacheVariations"] = (object) variations;
    }

    internal static void StoreCachedVaryInContext(CachedVary cachedVary, HttpContext httpContext = null) => (httpContext ?? HttpContext.Current).Items[(object) "sf:CachedVary"] = (object) cachedVary;

    internal static bool TryGetCachedVaryFromContext(out CachedVary cachedVary)
    {
      HttpContext current = HttpContext.Current;
      if (current != null)
      {
        cachedVary = current.Items[(object) "sf:CachedVary"] as CachedVary;
        return cachedVary != null;
      }
      cachedVary = (CachedVary) null;
      return false;
    }

    internal static string GetSubstitutionInfo()
    {
      Dictionary<string, string> dictionary = HttpContext.Current.Items[(object) "sf:CacheSubParams"] as Dictionary<string, string>;
      string substitutionInfo = (string) null;
      if (dictionary != null && dictionary.Count > 0)
        substitutionInfo = JsonConvert.SerializeObject((object) dictionary);
      return substitutionInfo;
    }

    /// <summary>
    /// Stores information for cache dependencies that have been persisted in the current context in the current cache relations provider.
    /// </summary>
    /// <param name="outputCacheKey">The output cache key.</param>
    /// <param name="entry">The item to add.</param>
    /// <param name="utcExpiry">Reference to the currently applied cache expiration date.</param>
    /// <returns>Returns true on success</returns>
    private bool TryHandleCacheDependencies(
      string outputCacheKey,
      object entry,
      ref DateTime utcExpiry)
    {
      if (!Bootstrapper.IsReady)
        return false;
      HttpContext current = HttpContext.Current;
      if (current == null)
        return false;
      if (!(entry is OutputCacheEntry outputCacheEntry))
        return true;
      if (CacheClientFactory.CurrentCacheProvider == CacheProvider.InMemory && current.Items[(object) Telerik.Sitefinity.Web.PageRouteHandler.OutputCachedPageSiteNode] != null)
        current.Response.AddCacheDependency((System.Web.Caching.CacheDependency) new InMemoryOutputCacheDependency(outputCacheKey));
      System.Web.Caching.CacheDependency cacheDependency = OutputCacheUtility.CreateCacheDependency(current.Response);
      if (cacheDependency != null)
      {
        cacheDependency.TakeOwnership();
        cacheDependency.SetCacheDependencyChanged(new Action<object, EventArgs>(this.CacheDependencyChangedCallback));
        lock (SitefinityOutputCacheProvider.cacheDependencyMap)
        {
          if (cacheDependency.HasChanged)
            return false;
          SitefinityOutputCacheProvider.cacheDependencyMap[outputCacheKey] = cacheDependency;
        }
      }
      if (HttpContext.Current.Items[(object) "sf:CacheDependencyItems"] is IList<CacheDependencyKey> cacheDependencies && cacheDependencies.Count > 0)
      {
        string cacheVaryKey = HttpContext.Current.Items[(object) "sf:CacheVaryKey"] as string;
        this.PersistOutputCacheItemDependencies(current, outputCacheKey, outputCacheEntry, cacheDependencies, SystemManager.CurrentContext.CurrentSite.Id, cacheVaryKey, ref utcExpiry);
      }
      return true;
    }

    /// <summary>
    /// Stores information for cache dependencies that have been persisted in the current context in the current cache relations provider.
    /// </summary>
    /// <param name="context">HTTP context</param>
    /// <param name="outputCacheKey">The output cache key.</param>
    /// <param name="outputCacheEntry">The item to add.</param>
    /// <param name="cacheDependencies">List of cache dependencies.</param>
    /// <param name="siteId">Current site ID</param>
    /// <param name="cacheVaryKey">Cache vary key</param>
    /// <param name="utcExpiry">Reference to the currently applied cache expiration date.</param>
    /// <param name="retry">Should retry.</param>
    [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Method's logic requires more than 65 lines.")]
    private void PersistOutputCacheItemDependencies(
      HttpContext context,
      string outputCacheKey,
      OutputCacheEntry outputCacheEntry,
      IList<CacheDependencyKey> cacheDependencies,
      Guid siteId,
      string cacheVaryKey,
      ref DateTime utcExpiry,
      bool retry = false)
    {
      OutputCacheItemProxy outputCacheItemProxy = OutputCacheWorker.GetOutputCacheItemProxy(outputCacheKey);
      DateTime utcNow;
      if (outputCacheItemProxy != null && (outputCacheItemProxy.Status == OutputCacheItemStatus.Live || outputCacheItemProxy.Status == OutputCacheItemStatus.New))
      {
        DateTime dateModified = outputCacheItemProxy.DateModified;
        utcNow = DateTime.UtcNow;
        DateTime dateTime = utcNow.AddSeconds(-10.0);
        if (dateModified > dateTime)
          return;
      }
      using (OutputCacheRelationsManager manager = OutputCacheRelationsManager.GetManager())
      {
        try
        {
          OutputCacheItem outputCacheItem = manager.GetOutputCacheItem(outputCacheKey);
          if (outputCacheItem == null)
          {
            outputCacheItem = manager.CreateOutputCacheItem();
            outputCacheItem.Key = outputCacheKey;
          }
          else if (retry)
          {
            if (outputCacheItem.Status == OutputCacheItemStatus.Live)
              return;
            if (outputCacheItem.Status == OutputCacheItemStatus.New)
            {
              DateTime dateModified = outputCacheItem.DateModified;
              utcNow = DateTime.UtcNow;
              DateTime dateTime = utcNow.AddSeconds(-10.0);
              if (dateModified > dateTime)
                return;
            }
          }
          if (!(context.Items[(object) Telerik.Sitefinity.Web.PageRouteHandler.OutputCachedPageSiteNode] is PageSiteNode pageSiteNode))
            return;
          int priority = 100;
          RequestContext requestContext = context.Request.RequestContext;
          if (requestContext != null)
            priority = context.Request.QueryString.Count <= 0 ? (!(requestContext.RouteData.Values["Params"] is string[] strArray) || strArray.Length == 0 ? 1 : 10) : (!((IEnumerable<string>) outputCacheEntry.Settings.VaryByParams).Contains<string>("*") ? 20 : 51);
          if (this.IsPageAbusedByQueryStringParams(outputCacheKey, pageSiteNode.Key, ref priority))
          {
            utcNow = DateTime.UtcNow;
            DateTime dateTime = utcNow.AddMinutes(1.0);
            if (!(utcExpiry > dateTime))
              return;
            utcExpiry = dateTime;
          }
          else
          {
            outputCacheItem.SiteId = siteId;
            outputCacheItem.Url = context.Request.Url.PathAndQuery;
            outputCacheItem.ETag = outputCacheEntry.Settings.ETag;
            outputCacheItem.CacheVaryKey = cacheVaryKey;
            outputCacheItem.Status = priority == 51 ? OutputCacheItemStatus.Live : OutputCacheItemStatus.New;
            outputCacheItem.SiteMapNodeKey = pageSiteNode.Key;
            DateTime dateTime1 = outputCacheEntry.Settings.UtcLastModified;
            DateTime dateTime2 = dateTime1;
            utcNow = DateTime.UtcNow;
            DateTime dateTime3 = utcNow.AddMinutes(-5.0);
            if (dateTime2 < dateTime3)
              dateTime1 = DateTime.UtcNow;
            outputCacheItem.DateModified = dateTime1;
            outputCacheItem.Priority = priority;
            outputCacheItem.Language = SystemManager.CurrentContext.Culture.Name;
            outputCacheItem.Host = context.Request.Url.GetLeftPart(UriPartial.Authority);
            manager.SaveChanges();
            if (priority == 51)
              return;
            OutputCacheWorker.UpdateCacheDependenciesAsync(outputCacheKey, (IEnumerable<CacheDependencyKey>) cacheDependencies, siteId.ToString());
          }
        }
        catch (OptimisticVerificationException ex)
        {
          manager.CancelChanges();
        }
        catch (DuplicateKeyException ex)
        {
          manager.CancelChanges();
        }
        catch (Exception ex)
        {
          if (!retry)
          {
            OutputCacheItem outputCacheItem = manager.GetOutputCacheItem(outputCacheKey);
            if (outputCacheItem != null)
            {
              try
              {
                manager.DeleteOutputCacheItem(outputCacheItem);
                manager.SaveChanges();
              }
              catch
              {
                manager.CancelChanges();
              }
            }
            this.PersistOutputCacheItemDependencies(context, outputCacheKey, outputCacheEntry, cacheDependencies, siteId, cacheVaryKey, ref utcExpiry, true);
          }
          else
            Telerik.Sitefinity.Abstractions.Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions);
        }
      }
    }

    private bool IsPageAbusedByQueryStringParams(string cacheKey, string pageKey, ref int priority)
    {
      if (priority > 50)
      {
        IEnumerable<OutputCacheItemProxy> itemsPerPage = OutputCacheWorker.GetItemsPerPage(pageKey);
        if (!itemsPerPage.Any<OutputCacheItemProxy>((Func<OutputCacheItemProxy, bool>) (p => p.Key != cacheKey && p.Priority < 50)))
          priority = 49;
        else if (itemsPerPage.Count<OutputCacheItemProxy>((Func<OutputCacheItemProxy, bool>) (p => p.Priority > 50)) > CacheClientFactory.GetCacheInvalidationStrategy().IllegalCacheByParamVariationsLimit)
          return true;
      }
      return false;
    }

    private void CacheDependencyChangedCallback(object obj, EventArgs e)
    {
      lock (SitefinityOutputCacheProvider.cacheDependencyMap)
      {
        foreach (string key in SitefinityOutputCacheProvider.cacheDependencyMap.Where<KeyValuePair<string, System.Web.Caching.CacheDependency>>((Func<KeyValuePair<string, System.Web.Caching.CacheDependency>, bool>) (i => i.Value.HasChanged)).Select<KeyValuePair<string, System.Web.Caching.CacheDependency>, string>((Func<KeyValuePair<string, System.Web.Caching.CacheDependency>, string>) (i => i.Key)).ToList<string>())
        {
          SitefinityOutputCacheProvider.cacheDependencyMap.Remove(key);
          this.RemoveAsync(key);
        }
      }
    }
  }
}
