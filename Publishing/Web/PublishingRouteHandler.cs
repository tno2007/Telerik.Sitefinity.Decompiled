// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Web.PublishingRouteHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web;
using System.Web.Routing;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Publishing.Configuration;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Publishing.Web
{
  /// <summary>Handles routing for feeds</summary>
  public class PublishingRouteHandler : IRouteHandler
  {
    private object cacheInfoSync = new object();
    private int cacheDuration;
    private bool cacheSlide;
    private DateTime lastCacheConfigCheck;
    public static string FeedUrlName = nameof (FeedUrlName);

    /// <summary>Provides the object that processes the request.</summary>
    /// <param name="requestContext">An object that encapsulates information about the request.</param>
    /// <returns>An object that processes the request.</returns>
    public IHttpHandler GetHttpHandler(RequestContext requestContext)
    {
      this.InitOutputCache(requestContext.HttpContext.Response.Cache);
      if (requestContext.RouteData.Values[PublishingRouteHandler.FeedUrlName] != null)
      {
        string str = requestContext.RouteData.Values[PublishingRouteHandler.FeedUrlName].ToString();
        requestContext.HttpContext.Items.Add((object) PublishingRouteHandler.FeedUrlName, (object) str);
      }
      return (IHttpHandler) new PublishingHttpHandler();
    }

    private void InitOutputCache(HttpCachePolicyBase cache)
    {
      if (this.lastCacheConfigCheck.AddMinutes(1.0) < DateTime.Now)
      {
        lock (this.cacheInfoSync)
        {
          if (this.lastCacheConfigCheck.AddMinutes(1.0) < DateTime.Now)
          {
            DateTime now = DateTime.Now;
            string cacheProfileName = Config.Get<PublishingConfig>().FeedsOutputCacheProfileName;
            if (string.IsNullOrEmpty(cacheProfileName))
            {
              this.lastCacheConfigCheck = now;
              this.cacheDuration = 0;
              return;
            }
            OutputCacheProfileElement cacheProfileElement;
            if (!Config.Get<SystemConfig>().CacheSettings.Profiles.TryGetValue(cacheProfileName, out cacheProfileElement))
            {
              this.lastCacheConfigCheck = now;
              this.cacheDuration = 0;
              return;
            }
            this.cacheDuration = cacheProfileElement.Duration;
            this.cacheSlide = cacheProfileElement.SlidingExpiration;
            this.lastCacheConfigCheck = now;
          }
        }
      }
      if (this.cacheDuration == 0)
        return;
      cache.SetCacheability(HttpCacheability.Server);
      cache.SetExpires(DateTime.Now.AddSeconds((double) this.cacheDuration));
      cache.SetMaxAge(new TimeSpan(0, 0, this.cacheDuration));
      cache.SetValidUntilExpires(true);
      if (this.cacheSlide)
      {
        cache.SetSlidingExpiration(true);
        cache.SetETagFromFileDependencies();
      }
      cache.VaryByParams.IgnoreParams = false;
      cache.VaryByParams["*"] = true;
      cache.VaryByHeaders["host"] = true;
      cache.VaryByHeaders["Accept"] = true;
    }
  }
}
