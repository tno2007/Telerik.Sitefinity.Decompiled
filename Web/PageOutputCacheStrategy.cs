// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.PageOutputCacheStrategy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Localization.UrlLocalizationStrategies;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Web.OutputCache;

namespace Telerik.Sitefinity.Web
{
  /// <summary>Handles default page output cache behavior.</summary>
  public class PageOutputCacheStrategy
  {
    /// <summary>
    /// Applies output cache control on the provided context response and returns true on success.
    /// </summary>
    /// <param name="context">The context the cache should be applied on.</param>
    /// <param name="profile">The profile containing the output cache settings. </param>
    /// <param name="page">The page site node.</param>
    /// <returns>Returns true if successful.</returns>
    public virtual bool ApplyServerCache(
      HttpContextBase context,
      IOutputCacheProfile profile,
      PageSiteNode page)
    {
      HttpCachePolicyBase cache = context.Response.Cache;
      HttpCacheability cacheability = this.ResolvePageCacheability(profile, page);
      cache.SetCacheability(cacheability);
      if (cacheability != HttpCacheability.NoCache)
      {
        int duration = profile.Duration;
        bool slidingExpiration = profile.SlidingExpiration;
        cache.SetExpires(DateTime.Now.AddSeconds((double) duration));
        cache.SetMaxAge(new TimeSpan(0, 0, duration));
        cache.SetValidUntilExpires(true);
        cache.SetSlidingExpiration(slidingExpiration);
        bool flag1;
        if (profile.TryGetVaryByUserAgent(out flag1))
          cache.VaryByHeaders.UserAgent = flag1;
        bool flag2;
        cache.VaryByHeaders["Host"] = !profile.TryGetVaryByHost(out flag2) ? this.ShouldVaryByHost() : flag2;
        string[] parameters;
        if (profile.VaryByParams(out parameters))
        {
          cache.VaryByParams.SetParams(parameters);
        }
        else
        {
          bool flag3 = false;
          cache.VaryByParams.IgnoreParams = !flag3;
          cache.VaryByParams["*"] = flag3;
        }
        if (!string.IsNullOrEmpty(profile.VaryByCustom))
          cache.SetVaryByCustom(profile.VaryByCustom);
      }
      if (profile.TryGetNoStore())
        cache.SetNoStore();
      HttpCacheRevalidation revalidation;
      if (profile.SetRevalidation(out revalidation))
        cache.SetRevalidation(revalidation);
      bool omit;
      if (profile.TryGetOmitVaryStar(out omit))
        cache.SetOmitVaryStar(omit);
      return true;
    }

    /// <summary>Apply response no cache.</summary>
    /// <param name="context">The context the cache should be applied on</param>
    /// <param name="siteNode">The page site node.</param>
    /// <returns>Returns true if successful.</returns>
    public virtual bool ApplyNoCache(HttpContextBase context, PageSiteNode siteNode)
    {
      context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
      return true;
    }

    private HttpCacheability ResolvePageCacheability(
      IOutputCacheProfile profile,
      PageSiteNode page)
    {
      HttpCacheability httpCacheability = profile.GetCacheability();
      switch (httpCacheability)
      {
        case HttpCacheability.NoCache:
        case HttpCacheability.Server:
          return httpCacheability;
        default:
          if (PageOutputCacheStrategy.IsClientCacheDenied(page))
          {
            httpCacheability = HttpCacheability.Server;
            goto case HttpCacheability.NoCache;
          }
          else
            goto case HttpCacheability.NoCache;
      }
    }

    private bool ShouldVaryByHost() => !SitefinityOutputCacheModule.Initialized && ObjectFactory.Resolve<UrlLocalizationService>().CurrentUrlLocalizationStrategy is DomainUrlLocalizationStrategy;

    internal static bool IsClientCacheDenied(PageSiteNode page) => page.IsPersonalized() || !page.IsAccessibleForEveryone || page.HasSecuredWidgets;
  }
}
