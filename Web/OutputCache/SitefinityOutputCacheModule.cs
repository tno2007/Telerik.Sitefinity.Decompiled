// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.OutputCache.SitefinityOutputCacheModule
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Configuration;
using System.Globalization;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Web.Configuration;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.OutputCache;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Web.OutputCache.Data;

namespace Telerik.Sitefinity.Web.OutputCache
{
  /// <summary>Output cache module implementation</summary>
  public sealed class SitefinityOutputCacheModule : IHttpModule
  {
    private static volatile bool initialized;
    internal const string WarmupCodeHeaderName = "Sf-Cache-Ignore-Code";

    /// <summary>Initializes the output cache module</summary>
    /// <param name="app">The Http Application parameter</param>
    public void Init(HttpApplication app)
    {
      if (app == null)
        throw new ArgumentNullException(nameof (app));
      if (!SitefinityOutputCacheModule.initialized)
        SitefinityOutputCacheModule.initialized = true;
      if (!(ConfigurationManager.GetSection("system.web/caching/outputCache") as OutputCacheSection).EnableOutputCache)
        return;
      app.AddOnResolveRequestCacheAsync(new BeginEventHandler(this.BeginOnResolveRequestCache), new EndEventHandler(SitefinityOutputCacheModule.EndOnResolveRequestCache));
      app.AddOnUpdateRequestCacheAsync(new BeginEventHandler(this.BeginOnUpdateRequestCache), new EndEventHandler(SitefinityOutputCacheModule.EndOnUpdateRequestCache));
    }

    /// <summary>
    /// Gets a value indicating whether the module is registered in the web.config
    /// </summary>
    internal static bool Initialized => SitefinityOutputCacheModule.initialized;

    private static void EndOnResolveRequestCache(IAsyncResult result) => TaskAsyncHelper.EndTask(result);

    private static void EndOnUpdateRequestCache(IAsyncResult result) => TaskAsyncHelper.EndTask(result);

    internal static string GetWarmupIgnoreCode() => SecurityManager.EncryptData(DateTime.UtcNow.ToString((IFormatProvider) CultureInfo.InvariantCulture));

    private bool IgnoreWarmupRequest(
      HttpContext context,
      OutputCacheHelper outputCacheHelper,
      CachedVary cachedVary)
    {
      string header = context.Request.Headers["Sf-Cache-Ignore-Code"];
      if (header != null)
      {
        try
        {
          DateTime utcNow = DateTime.UtcNow;
          if (DateTime.Parse(SecurityManager.DecryptData(header), (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal) > utcNow.AddMinutes(-1.0))
          {
            OutputCacheItemProxy outputCacheItemProxy = OutputCacheWorker.GetOutputCacheItemProxy(outputCacheHelper.GenerateCachedItemKey(cachedVary));
            return outputCacheItemProxy == null || outputCacheItemProxy.Status != OutputCacheItemStatus.Live;
          }
        }
        catch
        {
        }
      }
      return false;
    }

    private IAsyncResult BeginOnResolveRequestCache(
      object source,
      EventArgs e,
      AsyncCallback cb,
      object extraData)
    {
      return TaskAsyncHelper.BeginTask((Func<Task>) (() => this.OnEnterAsync(source, e)), cb, extraData);
    }

    private IAsyncResult BeginOnUpdateRequestCache(
      object source,
      EventArgs e,
      AsyncCallback cb,
      object extraData)
    {
      return TaskAsyncHelper.BeginTask((Func<Task>) (() => this.OnLeaveAsync(source, e)), cb, extraData);
    }

    /// <summary>Implement the IHTTPModule interface</summary>
    public void Dispose()
    {
    }

    private async Task OnEnterAsync(object source, EventArgs eventArgs)
    {
      HttpApplication httpApplication = (HttpApplication) source;
      OutputCacheHelper outputCacheHelper = new OutputCacheHelper(httpApplication.Context);
      if (!Bootstrapper.IsReady || !outputCacheHelper.IsHttpMethodSupported())
        return;
      string cachedVaryItemKey = outputCacheHelper.GenerateCachedVaryItemKey();
      CachedVary cachedVary = await outputCacheHelper.GetCachedVaryAsync(cachedVaryItemKey);
      if (cachedVary != null)
      {
        if (cachedVary.CustomVariationParams != null)
          PageRouteHandler.InitializeCacheHeaders(cachedVary.CustomVariationParams);
        CachedRawResponse cachedRawResponse = await outputCacheHelper.GetCachedResponse(cachedVary);
        if (cachedRawResponse != null && !this.IgnoreWarmupRequest(httpApplication.Context, outputCacheHelper, cachedVary))
        {
          HttpCachePolicySettings cachePolicy = cachedRawResponse.CachePolicy;
          if (!outputCacheHelper.CheckCachedVary(cachedVary, cachePolicy) && (!cachePolicy.IgnoreRangeRequests || !outputCacheHelper.IsRangeRequest()) && !outputCacheHelper.CheckHeaders(cachePolicy))
          {
            if (!await outputCacheHelper.CheckValidityAsync(cachedVaryItemKey, cachePolicy) && outputCacheHelper.IsContentEncodingAcceptable(cachedVary, cachedRawResponse.RawResponse))
            {
              outputCacheHelper.UpdateCachedResponse(cachePolicy, cachedRawResponse.RawResponse);
              if (outputCacheHelper.IsKernelCacheAPISupported() && cachedRawResponse.KernelCacheUrl != null)
                OutputCacheUtility.SetupKernelCaching(cachedRawResponse.KernelCacheUrl, httpApplication.Context.Response);
              httpApplication.CompleteRequest();
            }
          }
          cachePolicy = (HttpCachePolicySettings) null;
        }
        cachedRawResponse = (CachedRawResponse) null;
      }
      cachedVaryItemKey = (string) null;
      cachedVary = (CachedVary) null;
    }

    private async Task OnLeaveAsync(object source, EventArgs eventArgs)
    {
      OutputCacheHelper outputCacheHelper = new OutputCacheHelper(((HttpApplication) source).Context);
      if (!outputCacheHelper.IsResponseCacheable())
        return;
      await outputCacheHelper.CacheResponseAsync();
    }
  }
}
