// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Multisite.MultisiteContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Hosting;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.DataSource;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.Licensing;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Security.Model.Proxy;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Multisite
{
  public sealed class MultisiteContext : SitefinityContextBase, IMultisiteContext
  {
    private const string CurrentSiteItemKey = "sf_current_site";
    private const string SitesCacheKey = "sf_sites_cache";
    private const string DataSourcesCacheKey = "sf_datasources_cache";
    private const string UnresolvedSubfolderUrlKey = "sf_sub_folder_path";
    [ThreadStatic]
    private static ISiteContext currentSiteSlot;
    private static readonly object sitesDataSourcesCacheSync = new object();
    private readonly object sitesCacheSync = new object();
    private readonly bool isLicensed;
    private readonly DataSourceAccessLevel dataSourceAccessLevel;

    public MultisiteContext()
    {
      this.isLicensed = LicenseState.CheckIsModuleLicensedInAnyDomain("FBD4773B-8688-4C75-8563-28BFDA27A185");
      this.dataSourceAccessLevel = Config.Get<SecurityConfig>().UsersPerSiteSettings.AccessLevel;
    }

    /// <inheritdoc />
    public ISiteContext CurrentSiteContext
    {
      get
      {
        if (HostingEnvironment.IsHosted && SystemManager.CurrentHttpContext != null)
        {
          HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
          ISiteContext currentSite = (ISiteContext) currentHttpContext.Items[(object) "sf_current_site"];
          if (currentSite == null)
          {
            currentSite = this.DetermineCurrentSite(currentHttpContext);
            currentHttpContext.Items[(object) "sf_current_site"] = (object) currentSite;
          }
          return currentSite;
        }
        return MultisiteContext.currentSiteSlot != null ? MultisiteContext.currentSiteSlot : (ISiteContext) new SiteContext(this.GetSitesCache().DefaultSite, SiteContextResolutionTypes.ByDomain);
      }
      set
      {
        if (HostingEnvironment.IsHosted && SystemManager.CurrentHttpContext != null)
          SystemManager.CurrentHttpContext.Items[(object) "sf_current_site"] = (object) value;
        else
          MultisiteContext.currentSiteSlot = value;
      }
    }

    /// <inheritdoc />
    public override ISite CurrentSite => this.CurrentSiteContext.Site;

    /// <inheritdoc />
    public override void InvalidateCache() => this.ReloadCurrentContext();

    /// <inheritdoc />
    public override IAppSettings AppSettings => (IAppSettings) new SiteAppSettings(this.CurrentSite);

    public override bool IsMultisiteMode => this.isLicensed;

    internal override void ReloadCurrentContext() => this.CurrentSiteContext = (ISiteContext) new SiteContext(this.GetSitesCache().FindSiteById(this.CurrentSite.Id), this.CurrentSiteContext.ResolutionType);

    /// <inheritdoc />
    internal override bool IsGlobalContext
    {
      get
      {
        HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
        if (currentHttpContext == null)
          return false;
        object isGlobalContext1 = currentHttpContext.Items[(object) "sf_global"];
        if (isGlobalContext1 != null)
          return (bool) isGlobalContext1;
        bool isGlobalContext2 = currentHttpContext.Request["sf_global"] != null && currentHttpContext.Request["sf_global"] == "true";
        currentHttpContext.Items[(object) "sf_global"] = (object) isGlobalContext2;
        return isGlobalContext2;
      }
      set
      {
        HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
        if (currentHttpContext == null)
          return;
        currentHttpContext.Items[(object) "sf_global"] = (object) value;
      }
    }

    internal override ISite DefaultSite => this.GetSitesCache().DefaultSite;

    internal override bool IsDataProviderRestricted(Type managerType, string providerName)
    {
      if (base.IsDataProviderRestricted(managerType, providerName))
        return true;
      if (!Bootstrapper.IsReady || this.dataSourceAccessLevel != DataSourceAccessLevel.SharedWithSite || !typeof (IDataSource).IsAssignableFrom(managerType) || !SystemManager.DataSourceRegistry.IsDataSourceRegistered(managerType.FullName))
        return false;
      ISiteDataSource[] array = MultisiteContext.GetDataSourcesCache().GetDataSourcesByManager(managerType.FullName).Where<ISiteDataSource>((Func<ISiteDataSource, bool>) (s => s.Sites.Contains<Guid>(this.CurrentSite.Id))).ToArray<ISiteDataSource>();
      return (array.Length != 0 || !managerType.Equals(typeof (UserManager))) && !((IEnumerable<ISiteDataSource>) array).Any<ISiteDataSource>((Func<ISiteDataSource, bool>) (s => s.Provider == providerName));
    }

    /// <inheritdoc />
    public void ChangeCurrentSite(ISite site, SiteContextResolutionTypes siteResolutionType = SiteContextResolutionTypes.ExplicitlySet) => this.CurrentSiteContext = (ISiteContext) new SiteContext(site, siteResolutionType);

    /// <inheritdoc />
    public ISite GetSiteById(Guid siteId) => this.GetSitesCache().FindSiteById(siteId);

    /// <inheritdoc />
    public ISite GetSiteBySiteMapRoot(Guid siteMapRootId) => this.GetSitesCache().FindSiteBySiteMapRoot(siteMapRootId);

    /// <inheritdoc />
    public ISite GetSiteByName(string name) => this.GetSitesCache().FindSiteByName(name);

    /// <inheritdoc />
    public ISite GetSiteByDomain(string domain) => this.GetSitesCache().FindSiteByDomain(domain);

    /// <inheritdoc />
    public IEnumerable<ISite> GetSites() => this.GetSitesCache().GetSites();

    /// <inheritdoc />
    public new string ResolveUrl(string url)
    {
      ISiteContext currentSiteContext = this.CurrentSiteContext;
      if ((currentSiteContext.ResolutionType == SiteContextResolutionTypes.ByFolder || currentSiteContext.ResolutionType == SiteContextResolutionTypes.ByParam) && currentSiteContext.Site is MultisiteContext.SiteProxy site)
      {
        if (currentSiteContext.ResolutionType == SiteContextResolutionTypes.ByParam && HostingEnvironment.IsHosted)
        {
          HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
          if (currentHttpContext != null)
          {
            if (!(this.GetSitesCache().FindSiteByDomain(currentHttpContext.Request.Url.Authority) is MultisiteContext.SiteProxy siteByDomain) || siteByDomain.Id == site.Id)
              return url;
            if (!siteByDomain.Contains(site))
              return UrlPath.ResolveAbsoluteUrl(site.GetUri(), url);
          }
        }
        url = this.ResolveSiteRelativeUrl(site, url);
      }
      return url;
    }

    internal string ResolveSiteRelativeUrl(MultisiteContext.SiteProxy site, string url)
    {
      if (!site.RedirectFolder.IsNullOrEmpty())
      {
        if (!string.IsNullOrEmpty(url))
        {
          if (url.IndexOf(':') != -1)
            return url;
          url = !url.StartsWith("~/") ? (!url.StartsWith("/") ? site.RedirectFolder + "/" + url : "/" + site.RedirectFolder + url) : url.Insert(1, "/" + site.RedirectFolder);
        }
        else
          url = site.RedirectFolder;
      }
      return url;
    }

    /// <inheritdoc />
    public string UnresolveUrl(string url)
    {
      ISiteContext currentSiteContext = this.CurrentSiteContext;
      string outUrl;
      return currentSiteContext.ResolutionType == SiteContextResolutionTypes.ByFolder && currentSiteContext.Site is MultisiteContext.SiteProxy site && site.Container != null && this.TryUnresolveSite(url, site.Container, out outUrl, out ISite _) ? outUrl : url;
    }

    /// <inheritdoc />
    public string UnresolveUrlAndApplyDetectedSite(string url)
    {
      ISiteContext currentSiteContext = this.CurrentSiteContext;
      if (currentSiteContext.ResolutionType != SiteContextResolutionTypes.ByFolder)
      {
        string outUrl;
        ISite outSite;
        if (currentSiteContext.Site is MultisiteContext.SiteProxy site && site.Container != null && this.TryUnresolveSite(url, site.Container, out outUrl, out outSite))
        {
          this.CurrentSiteContext = (ISiteContext) new SiteContext(outSite, SiteContextResolutionTypes.ByFolder);
          HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
          if (currentHttpContext != null)
            currentHttpContext.Items[(object) "sf_sub_folder_path"] = (object) outUrl;
          return outUrl;
        }
      }
      else
      {
        HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
        if (currentHttpContext != null && currentHttpContext.Items[(object) "sf_sub_folder_path"] is string str)
          return str;
      }
      return url;
    }

    internal IEnumerable<IMultisiteDataProviderInfo> GetAllDataProviders(
      string managerType,
      Func<IEnumerable<DataProviderBase>> getStaticProviders)
    {
      return MultisiteContext.GetDataSourcesCache().GetAllDataProviders(managerType, (Func<IEnumerable<IDataProviderInfo>>) getStaticProviders);
    }

    internal IEnumerable<ISiteDataSource> GetDataSourcesByManager(
      string managerType)
    {
      return MultisiteContext.GetDataSourcesCache().GetDataSourcesByManager(managerType);
    }

    internal IEnumerable<ISiteDataSource> GetDataSourcesByName(string name) => MultisiteContext.GetDataSourcesCache().GetDataSourcesByName(name);

    internal IEnumerable<ISiteDataSource> GetDataSourcesByManagerAndKey(
      string managerType,
      string keyName,
      string keyValue,
      Func<ISiteDataSource, string> generateKey)
    {
      return MultisiteContext.GetDataSourcesCache().GetDataSourcesByManagerAndKey(managerType, keyName, keyValue, generateKey);
    }

    private bool TryUnresolveSite(
      string inUrl,
      MultisiteContext.ISiteContainer inSite,
      out string outUrl,
      out ISite outSite)
    {
      outSite = (ISite) null;
      if (inSite.Folders != null)
      {
        IList<string> pathSegmentStrings1 = RouteHelper.SplitUrlToPathSegmentStrings(inUrl, true);
        if (pathSegmentStrings1.Count > 0)
        {
          int index = 0;
          if (inUrl.StartsWith("~/"))
            ++index;
          else if (inUrl.StartsWith(HttpRuntime.AppDomainAppVirtualPath))
          {
            IList<string> pathSegmentStrings2 = RouteHelper.SplitUrlToPathSegmentStrings(HttpRuntime.AppDomainAppVirtualPath, true);
            index = pathSegmentStrings2.Count >= pathSegmentStrings1.Count ? pathSegmentStrings2.Count - 1 : pathSegmentStrings2.Count;
          }
          int num = 0;
          MultisiteContext.ISiteContainer siteContainer;
          for (; index < pathSegmentStrings1.Count && inSite.Folders != null; inSite = siteContainer)
          {
            string lowerInvariant = pathSegmentStrings1[index].ToLowerInvariant();
            if (inSite.Folders.TryGetValue(lowerInvariant, out siteContainer))
            {
              if (siteContainer.Site != null)
              {
                outSite = siteContainer.Site;
                if (num > 0)
                {
                  index -= num;
                  do
                  {
                    pathSegmentStrings1.RemoveAt(index);
                    --num;
                  }
                  while (num > 0);
                }
                pathSegmentStrings1.RemoveAt(index);
              }
              else
              {
                ++num;
                ++index;
              }
            }
            else
              break;
          }
          if (outSite != null)
          {
            outUrl = string.Join("/", (IEnumerable<string>) pathSegmentStrings1);
            if (inUrl.StartsWith("/"))
              outUrl = "/" + outUrl;
            return true;
          }
        }
      }
      outUrl = (string) null;
      return false;
    }

    /// <inheritdoc />
    public IEnumerable<Guid> GetAllowedSites(Guid userId, string userProvider)
    {
      List<Guid> allowedSites = new List<Guid>();
      Func<Guid[]> func = (Func<Guid[]>) null;
      SitefinityIdentity currentUser = ClaimsManager.GetCurrentIdentity();
      bool flag1;
      bool flag2;
      if (currentUser != null && currentUser.UserId == userId)
      {
        flag1 = currentUser.IsGlobalUser;
        int num = currentUser.IsAdmin ? 1 : 0;
        flag2 = currentUser.IsUnrestricted;
        if (num == 0)
          func = (Func<Guid[]>) (() =>
          {
            List<Guid> guidList = new List<Guid>();
            guidList.Add(userId);
            guidList.AddRange(currentUser.Roles.Select<RoleInfo, Guid>((Func<RoleInfo, Guid>) (r => r.Id)));
            return guidList.ToArray();
          });
      }
      else
      {
        flag1 = SecurityManager.IsGlobalUserProvider(userProvider);
        int num = RoleManager.IsUserUnrestricted(userId) ? 1 : 0;
        flag2 = (num & (flag1 ? 1 : 0)) != 0;
        if (num == 0)
          func = (Func<Guid[]>) (() =>
          {
            List<Guid> guidList = new List<Guid>();
            guidList.Add(userId);
            guidList.AddRange(RoleManager.GetAllRoleIdsOfUser(userId));
            return guidList.ToArray();
          });
      }
      foreach (ISite site in this.GetSites())
      {
        if (!flag2)
        {
          if (flag1 || site.SiteDataSourceLinks.Any<MultisiteContext.SiteDataSourceLinkProxy>((Func<MultisiteContext.SiteDataSourceLinkProxy, bool>) (p => p.DataSourceName == typeof (UserManager).FullName && p.ProviderName == userProvider)))
          {
            if (site is ISecuredObject && func != null)
            {
              if (!((ISecuredObject) site).IsGranted("Site", func(), "AccessSite"))
                continue;
            }
          }
          else
            continue;
        }
        allowedSites.Add(site.Id);
      }
      return (IEnumerable<Guid>) allowedSites;
    }

    bool IMultisiteContext.DataSourceExists(string name, string provider) => this.CurrentSite.SiteDataSourceLinks.Any<MultisiteContext.SiteDataSourceLinkProxy>((Func<MultisiteContext.SiteDataSourceLinkProxy, bool>) (ds => ds.DataSourceName == name && ds.ProviderName == provider));

    private ISiteContext DetermineCurrentSite(HttpContextBase httpContext)
    {
      ISite site = this.DetermineCurrentSiteFromSelection(httpContext) ?? this.DefaultDetermineCurrentSite(httpContext);
      SiteContextResolutionTypes resolutionType = this.GetSitesCache().FindSiteByDomain(httpContext.Request.Url.Authority).Id == site.Id ? SiteContextResolutionTypes.ByDomain : SiteContextResolutionTypes.ByParam;
      return (ISiteContext) new SiteContext(site, resolutionType);
    }

    /// <summary>
    /// Tries to determine the current context site from the HttpContext request - query string, header, cookies, etc.
    /// With first priority is a query string parameter 'sf_site'. In this case the current site is stored in cookie with 'sf_site' name,
    /// except if there is another query string parameter 'sf_site_temp' saying that the site should be resolved only for this request.
    /// The second priority is a header with name 'sf_site', which determines the current site only for the current request.
    /// The last priority is a cookie with name 'sf_site'.
    /// </summary>
    /// <param name="httpContext">The HTTP context.</param>
    /// <returns>The current site resolved from the HttpRequest. If no site was resolved returns <c>Null</c>.</returns>
    /// <remarks>
    /// Returns a site only if the current user has access to it.
    /// As a side effect the method stores the resolved site in the cookies or deletes the cookie if not up to date.
    /// </remarks>
    private ISite DetermineCurrentSiteFromSelection(HttpContextBase httpContext)
    {
      string header = httpContext.Request.QueryString["sf_site"];
      bool flag = true;
      if (header.IsNullOrEmpty())
      {
        if (Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.IsBackend)
        {
          header = httpContext.Request.Headers["sf_site"];
          if (!header.IsNullOrEmpty())
          {
            flag = false;
          }
          else
          {
            HttpCookie cookie = httpContext.Request.Cookies["sf_site"];
            if (cookie != null)
            {
              header = cookie.Value;
              flag = false;
            }
          }
        }
      }
      else if (!string.IsNullOrEmpty(httpContext.Request.QueryString["sf_site_temp"]))
        flag = false;
      if (!header.IsNullOrEmpty())
      {
        Guid result;
        if (Guid.TryParse(header, out result) && result != Guid.Empty)
        {
          if (flag)
            httpContext.Response.Cookies.Add(new HttpCookie("sf_site", header)
            {
              Expires = DateTime.UtcNow.AddYears(2)
            });
          return this.GetSiteById(result);
        }
        if (!flag)
          httpContext.Response.Cookies.Remove("sf_site");
      }
      return (ISite) null;
    }

    /// <summary>
    /// Determines the default site to be used as current site.
    /// </summary>
    /// <param name="httpContext">The HTTP context.</param>
    /// <exception cref="T:System.Web.HttpException">Throws an exception when the current request is a backend request and the user has no access to any site.</exception>
    /// <returns>Returns the default current site the user has access to.</returns>
    private ISite DefaultDetermineCurrentSite(HttpContextBase httpContext)
    {
      ISite siteByDomain = this.GetSitesCache().FindSiteByDomain(httpContext.Request.Url.Authority);
      if (SystemManager.IsBackendRequest() && !SystemManager.IsAuthenticationRequirementSuppressed(httpContext) && SystemManager.CurrentHttpContext.User != null && SystemManager.CurrentHttpContext.User.Identity.IsAuthenticated && !this.IsSiteAccessible(httpContext, siteByDomain.Id))
      {
        ISite currentSite = this.GetSites().FirstOrDefault<ISite>((Func<ISite, bool>) (s => this.IsSiteAccessible(httpContext, s.Id)));
        if (currentSite != null)
          return currentSite;
        if (!this.AreSiteAccessExceptionsSuppressed(httpContext))
          ClaimsManager.CurrentAuthenticationModule.RedirectToNeedAdminRightsPage(httpContext);
      }
      return siteByDomain;
    }

    private bool AreSiteAccessExceptionsSuppressed(HttpContextBase httpContext)
    {
      object obj = httpContext.Items[(object) "sfSuppressSiteExceptions"];
      return obj != null && (bool) obj;
    }

    private bool IsSiteAccessible(HttpContextBase httpContext, Guid siteId)
    {
      UserActivityRecord currentUserActivity = UserActivityManager.GetCurrentUserActivity();
      if (currentUserActivity != null)
        return currentUserActivity.AllowedSites.Contains(siteId);
      if (SecurityManager.CurrentUserId.ToString().ToUpperInvariant() == SecurityManager.SystemAccountIDs[0])
        return true;
      if (!(httpContext.Items[(object) "sfMultisiteContextUserId"] is Tuple<Guid, string> tuple))
      {
        SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
        if (currentIdentity != null)
          tuple = new Tuple<Guid, string>(currentIdentity.UserId, currentIdentity.MembershipProvider);
      }
      return tuple != null && this.GetAllowedSites(tuple.Item1, tuple.Item2).Contains<Guid>(siteId);
    }

    private MultisiteContext.SitesCache GetSitesCache()
    {
      ICacheManager cacheManager = SystemManager.GetCacheManager(CacheManagerInstance.Internal);
      if (!(cacheManager["sf_sites_cache"] is MultisiteContext.SitesCache sitesCache1))
      {
        lock (this.sitesCacheSync)
        {
          if (!(cacheManager["sf_sites_cache"] is MultisiteContext.SitesCache sitesCache1))
          {
            MultisiteManager manager = MultisiteManager.GetManager((string) null, Guid.NewGuid().ToString());
            using (new ElevatedModeRegion((IManager) manager))
            {
              IQueryable<Site> queryable = manager.GetSites().Include<Site>((Expression<Func<Site, object>>) (x => x.CultureKeys)).Include<Site>((Expression<Func<Site, object>>) (x => x.SiteDataSourceLinks)).Include<Site>((Expression<Func<Site, object>>) (x => x.Permissions)).Include<Site>((Expression<Func<Site, object>>) (x => x.DomainAliases));
              if (!this.isLicensed)
                queryable = queryable.Where<Site>((Expression<Func<Site, bool>>) (s => s.IsDefault)).Take<Site>(1);
              sitesCache1 = new MultisiteContext.SitesCache(queryable);
            }
            cacheManager.Add("sf_sites_cache", (object) sitesCache1, CacheItemPriority.Normal, (ICacheItemRefreshAction) null, (ICacheItemExpiration) new DataItemCacheDependency(typeof (Site), (string) null), (ICacheItemExpiration) new DataItemCacheDependency(typeof (SiteDataSourceLink), (string) null), (ICacheItemExpiration) new SlidingTime(TimeSpan.FromMinutes(60.0)));
          }
        }
      }
      return sitesCache1;
    }

    private static MultisiteContext.DataSourcesCache GetDataSourcesCache()
    {
      ICacheManager cacheManager = SystemManager.GetCacheManager(CacheManagerInstance.Internal);
      if (!(cacheManager["sf_datasources_cache"] is MultisiteContext.DataSourcesCache dataSourcesCache1))
      {
        lock (MultisiteContext.sitesDataSourcesCacheSync)
        {
          if (!(cacheManager["sf_datasources_cache"] is MultisiteContext.DataSourcesCache dataSourcesCache1))
          {
            MultisiteManager manager = MultisiteManager.GetManager((string) null, Guid.NewGuid().ToString());
            using (new ElevatedModeRegion((IManager) manager))
              dataSourcesCache1 = new MultisiteContext.DataSourcesCache(manager.GetDataSources().Include<SiteDataSource>((Expression<Func<SiteDataSource, object>>) (x => x.Sites)));
            cacheManager.Add("sf_datasources_cache", (object) dataSourcesCache1, CacheItemPriority.Normal, (ICacheItemRefreshAction) null, (ICacheItemExpiration) new DataItemCacheDependency(typeof (SiteDataSource), (string) null), (ICacheItemExpiration) new DataItemCacheDependency(typeof (SiteDataSourceLink), (string) null), (ICacheItemExpiration) new SlidingTime(TimeSpan.FromMinutes(60.0)));
          }
        }
      }
      return dataSourcesCache1;
    }

    internal static Uri GetSiteUri(Site site) => MultisiteContext.GetUri(string.IsNullOrEmpty(site.StagingUrl) ? site.LiveUrl : site.StagingUrl, site.RequiresSsl);

    internal static Uri GetSiteUri(ISite site) => MultisiteContext.GetUri(string.IsNullOrEmpty(site.StagingUrl) ? site.LiveUrl : site.StagingUrl, site.RequiresSsl);

    private static Uri GetUri(string domainUrl, bool requireSsl)
    {
      if (string.IsNullOrEmpty(domainUrl))
        domainUrl = SystemManager.CurrentHttpContext == null ? "localhost" : SystemManager.CurrentHttpContext.Request.Url.Authority;
      UriBuilder uriBuilder = new UriBuilder();
      int length1 = domainUrl.IndexOf('/');
      string str;
      if (length1 > 0)
      {
        str = domainUrl.Substring(0, length1);
        uriBuilder.Path = domainUrl.Substring(length1 + 1);
      }
      else
        str = domainUrl;
      int length2 = str.IndexOf(':');
      if (length2 != -1)
      {
        uriBuilder.Port = int.Parse(str.Substring(length2 + 1));
        uriBuilder.Host = str.Substring(0, length2);
      }
      else
        uriBuilder.Host = str;
      uriBuilder.Scheme = !requireSsl ? "http" : "https";
      return uriBuilder.Uri;
    }

    private class DataSourcesCache
    {
      private readonly IDictionary<string, IList<ISiteDataSource>> itemsByName = (IDictionary<string, IList<ISiteDataSource>>) new Dictionary<string, IList<ISiteDataSource>>();
      private readonly IDictionary<string, MultisiteContext.DataSourcesCache.ItemsByManager> itemsByManagerType = (IDictionary<string, MultisiteContext.DataSourcesCache.ItemsByManager>) new Dictionary<string, MultisiteContext.DataSourcesCache.ItemsByManager>();
      private readonly IDictionary<Guid, IList<MultisiteContext.SiteDataSourceLinkProxy>> itemsBySite = (IDictionary<Guid, IList<MultisiteContext.SiteDataSourceLinkProxy>>) new Dictionary<Guid, IList<MultisiteContext.SiteDataSourceLinkProxy>>();
      private readonly IDictionary<string, IDictionary<string, IList<ISiteDataSource>>> customIndex = (IDictionary<string, IDictionary<string, IList<ISiteDataSource>>>) new Dictionary<string, IDictionary<string, IList<ISiteDataSource>>>();

      public DataSourcesCache(IQueryable<SiteDataSource> dataSources)
      {
        IQueryable<IGrouping<string, SiteDataSource>> queryable = dataSources.GroupBy<SiteDataSource, string>((Expression<Func<SiteDataSource, string>>) (ds => ds.Name));
        List<ISiteDataSource> siteDataSourceList1 = new List<ISiteDataSource>();
        foreach (IGrouping<string, SiteDataSource> grouping in (IEnumerable<IGrouping<string, SiteDataSource>>) queryable)
        {
          string key = grouping.Key;
          List<ISiteDataSource> siteDataSourceList2 = new List<ISiteDataSource>();
          bool isDynamicModule = !key.Contains(".");
          foreach (SiteDataSource ds in (IEnumerable<SiteDataSource>) grouping)
          {
            MultisiteContext.SiteDataSourceProxy source = new MultisiteContext.SiteDataSourceProxy(ds, isDynamicModule);
            foreach (SiteDataSourceLink site in (IEnumerable<SiteDataSourceLink>) ds.Sites)
            {
              source.AddSite(site.SiteId);
              IList<MultisiteContext.SiteDataSourceLinkProxy> dataSourceLinkProxyList;
              if (!this.itemsBySite.TryGetValue(site.SiteId, out dataSourceLinkProxyList))
              {
                dataSourceLinkProxyList = (IList<MultisiteContext.SiteDataSourceLinkProxy>) new List<MultisiteContext.SiteDataSourceLinkProxy>();
                this.itemsBySite.Add(site.SiteId, dataSourceLinkProxyList);
              }
              dataSourceLinkProxyList.Add(new MultisiteContext.SiteDataSourceLinkProxy((ISiteDataSource) source, site.IsDefault));
            }
            siteDataSourceList2.Add((ISiteDataSource) source);
          }
          this.itemsByName.Add(key, (IList<ISiteDataSource>) siteDataSourceList2);
          if (isDynamicModule)
            siteDataSourceList1.AddRange((IEnumerable<ISiteDataSource>) siteDataSourceList2);
          else
            this.itemsByManagerType.Add(key, new MultisiteContext.DataSourcesCache.ItemsByManager((IList<ISiteDataSource>) siteDataSourceList2));
        }
        if (siteDataSourceList1.Any<ISiteDataSource>())
          this.itemsByManagerType.Add(typeof (DynamicModuleManager).FullName, new MultisiteContext.DataSourcesCache.ItemsByManager((IList<ISiteDataSource>) siteDataSourceList1));
        foreach (IList<MultisiteContext.SiteDataSourceLinkProxy> source in (IEnumerable<IList<MultisiteContext.SiteDataSourceLinkProxy>>) this.itemsBySite.Values)
        {
          if (!source.Any<MultisiteContext.SiteDataSourceLinkProxy>((Func<MultisiteContext.SiteDataSourceLinkProxy, bool>) (sdl => sdl.IsDefault)))
            source.First<MultisiteContext.SiteDataSourceLinkProxy>().IsDefault = true;
        }
      }

      public IList<MultisiteContext.SiteDataSourceLinkProxy> GetSiteDataSourceLinks(
        Guid siteId)
      {
        IList<MultisiteContext.SiteDataSourceLinkProxy> dataSourceLinkProxyList;
        return this.itemsBySite.TryGetValue(siteId, out dataSourceLinkProxyList) ? dataSourceLinkProxyList : (IList<MultisiteContext.SiteDataSourceLinkProxy>) new List<MultisiteContext.SiteDataSourceLinkProxy>();
      }

      public IEnumerable<ISiteDataSource> GetDataSourcesByName(string name)
      {
        IList<ISiteDataSource> siteDataSourceList;
        return this.itemsByName.TryGetValue(name, out siteDataSourceList) ? (IEnumerable<ISiteDataSource>) siteDataSourceList : (IEnumerable<ISiteDataSource>) new ISiteDataSource[0];
      }

      public IEnumerable<ISiteDataSource> GetDataSourcesByManager(
        string managerType)
      {
        MultisiteContext.DataSourcesCache.ItemsByManager itemsByManager;
        return this.itemsByManagerType.TryGetValue(managerType, out itemsByManager) ? (IEnumerable<ISiteDataSource>) itemsByManager.Sources : (IEnumerable<ISiteDataSource>) new ISiteDataSource[0];
      }

      public IEnumerable<IMultisiteDataProviderInfo> GetAllDataProviders(
        string managerType,
        Func<IEnumerable<IDataProviderInfo>> getStaticProviders)
      {
        MultisiteContext.DataSourcesCache.ItemsByManager itemsByManager;
        if (!this.itemsByManagerType.TryGetValue(managerType, out itemsByManager))
          return getStaticProviders().Select<IDataProviderInfo, IMultisiteDataProviderInfo>((Func<IDataProviderInfo, IMultisiteDataProviderInfo>) (p => (IMultisiteDataProviderInfo) new MultisiteContext.DataProviderProxy(p)));
        if (itemsByManager.Providers == null)
        {
          lock (itemsByManager)
          {
            if (itemsByManager.Providers == null)
            {
              Dictionary<string, IMultisiteDataProviderInfo> dictionary = getStaticProviders().ToDictionary<IDataProviderInfo, string, IMultisiteDataProviderInfo>((Func<IDataProviderInfo, string>) (p => p.Name), (Func<IDataProviderInfo, IMultisiteDataProviderInfo>) (p => (IMultisiteDataProviderInfo) new MultisiteContext.DataProviderProxy(p)));
              List<IDataProviderInfo> dataProviderInfoList = new List<IDataProviderInfo>();
              foreach (IGrouping<string, ISiteDataSource> grouping in itemsByManager.Sources.GroupBy<ISiteDataSource, string>((Func<ISiteDataSource, string>) (s => s.Provider)))
              {
                IMultisiteDataProviderInfo provider = grouping.Count<ISiteDataSource>() <= 1 ? (IMultisiteDataProviderInfo) grouping.First<ISiteDataSource>() : (IMultisiteDataProviderInfo) new MultisiteContext.MultisourceProvider(grouping.Key, (IEnumerable<ISiteDataSource>) grouping);
                IMultisiteDataProviderInfo dataProviderInfo;
                if (dictionary.TryGetValue(grouping.Key, out dataProviderInfo))
                  (dataProviderInfo as MultisiteContext.DataProviderProxy).Merge(provider);
                else
                  dictionary.Add(grouping.Key, provider);
              }
              itemsByManager.Providers = (IEnumerable<IMultisiteDataProviderInfo>) dictionary.Values;
            }
          }
        }
        return itemsByManager.Providers;
      }

      public IEnumerable<ISiteDataSource> GetDataSourcesByManagerAndKey(
        string managerType,
        string keyName,
        string keyValue,
        Func<ISiteDataSource, string> generateKey)
      {
        string key1 = managerType + ":" + keyName;
        IDictionary<string, IList<ISiteDataSource>> dictionary;
        if (!this.customIndex.TryGetValue(key1, out dictionary))
        {
          lock (this.customIndex)
          {
            if (!this.customIndex.TryGetValue(key1, out dictionary))
            {
              dictionary = (IDictionary<string, IList<ISiteDataSource>>) new Dictionary<string, IList<ISiteDataSource>>();
              foreach (ISiteDataSource siteDataSource in this.GetDataSourcesByManager(managerType))
              {
                string key2 = generateKey(siteDataSource);
                IList<ISiteDataSource> siteDataSourceList;
                if (!dictionary.TryGetValue(key2, out siteDataSourceList))
                {
                  siteDataSourceList = (IList<ISiteDataSource>) new List<ISiteDataSource>();
                  dictionary.Add(key2, siteDataSourceList);
                }
                siteDataSourceList.Add(siteDataSource);
              }
              this.customIndex.Add(key1, dictionary);
            }
          }
        }
        IList<ISiteDataSource> siteDataSourceList1;
        return dictionary.TryGetValue(keyValue, out siteDataSourceList1) ? (IEnumerable<ISiteDataSource>) siteDataSourceList1 : (IEnumerable<ISiteDataSource>) new ISiteDataSource[0];
      }

      private class ItemsByManager
      {
        public ItemsByManager(IList<ISiteDataSource> originalItems) => this.Sources = originalItems;

        public IList<ISiteDataSource> Sources { get; private set; }

        public IEnumerable<IMultisiteDataProviderInfo> Providers { get; set; }
      }
    }

    private class SitesCache
    {
      private readonly ISite defaultSite;
      private readonly List<ISite> list;
      private readonly Dictionary<string, ISite> domainCache = new Dictionary<string, ISite>();

      public SitesCache(IQueryable<Site> sites)
      {
        List<Site> list = sites.ToList<Site>();
        if (list.Count > 1)
        {
          this.list = new List<ISite>();
          foreach (Site site in list)
          {
            MultisiteContext.SiteProxy siteProxy = new MultisiteContext.SiteProxy(site);
            this.list.Add((ISite) siteProxy);
            if (site.IsDefault)
              this.defaultSite = (ISite) siteProxy;
          }
          if (this.defaultSite == null)
            this.defaultSite = this.list.First<ISite>();
          foreach (MultisiteContext.SiteProxy siteProxy in this.list)
          {
            Uri uri = siteProxy.GetUri();
            string str1 = uri.AbsolutePath.Trim('/');
            if (!string.IsNullOrEmpty(str1))
            {
              MultisiteContext.SiteProxy siteByDomain = this.FindSiteByDomain(uri.Authority) as MultisiteContext.SiteProxy;
              if (!(siteByDomain.Id == siteProxy.Id))
              {
                if (siteByDomain.Container == null)
                  siteByDomain.Container = (MultisiteContext.ISiteContainer) new MultisiteContext.SiteContainer()
                  {
                    Site = (ISite) siteByDomain
                  };
                MultisiteContext.ISiteContainer siteContainer1 = siteByDomain.Container;
                string str2 = str1;
                char[] chArray = new char[1]{ '/' };
                foreach (string str3 in str2.Split(chArray))
                {
                  bool flag = false;
                  string key = str3;
                  if (siteContainer1.Folders == null)
                  {
                    siteContainer1.Folders = (IDictionary<string, MultisiteContext.ISiteContainer>) new Dictionary<string, MultisiteContext.ISiteContainer>();
                    flag = true;
                  }
                  MultisiteContext.ISiteContainer siteContainer2 = (MultisiteContext.ISiteContainer) null;
                  if (flag || !siteContainer1.Folders.TryGetValue(key, out siteContainer2))
                  {
                    siteContainer2 = (MultisiteContext.ISiteContainer) new MultisiteContext.SiteContainer();
                    siteContainer1.Folders.Add(key, siteContainer2);
                  }
                  siteContainer1 = siteContainer2;
                }
                if (siteContainer1.Site == null)
                {
                  siteContainer1.Site = (ISite) siteProxy;
                  siteProxy.RedirectFolder = str1;
                }
              }
            }
          }
        }
        else if (list.Count > 0)
          this.defaultSite = (ISite) new MultisiteContext.SiteProxy(sites.First<Site>());
        else
          this.defaultSite = (ISite) new SingleSiteContext.SingleSiteProxy();
      }

      public ISite FindSiteById(Guid siteId) => this.list != null ? this.list.FirstOrDefault<ISite>((Func<ISite, bool>) (s => s.Id == siteId)) : this.defaultSite;

      public ISite FindSiteBySiteMapRoot(Guid siteMapRootId)
      {
        if (this.list != null)
          return this.list.FirstOrDefault<ISite>((Func<ISite, bool>) (s => s.SiteMapRootNodeId == siteMapRootId));
        return !(this.defaultSite.SiteMapRootNodeId == siteMapRootId) ? (ISite) null : this.defaultSite;
      }

      public ISite FindSiteByName(string name)
      {
        if (this.list != null)
          return this.list.FirstOrDefault<ISite>((Func<ISite, bool>) (s => s.Name == name));
        return !(this.defaultSite.Name == name) ? (ISite) null : this.defaultSite;
      }

      public ISite FindSiteByDomain(string domain)
      {
        if (this.list == null)
          return this.defaultSite;
        ISite siteByDomain;
        if (this.domainCache.TryGetValue(domain, out siteByDomain))
          return siteByDomain;
        lock (this.domainCache)
        {
          if (this.domainCache.TryGetValue(domain, out siteByDomain))
            return siteByDomain;
          siteByDomain = ((this.list.FirstOrDefault<ISite>((Func<ISite, bool>) (s => s.LiveUrl == domain)) ?? this.list.FirstOrDefault<ISite>((Func<ISite, bool>) (s => s.DomainAliases.Contains(domain)))) ?? this.list.FirstOrDefault<ISite>((Func<ISite, bool>) (s => s.StagingUrl == domain))) ?? this.defaultSite;
          this.domainCache[domain] = siteByDomain;
        }
        return siteByDomain;
      }

      public IEnumerable<ISite> GetSites()
      {
        if (this.list != null)
          return (IEnumerable<ISite>) this.list;
        return (IEnumerable<ISite>) new ISite[1]
        {
          this.defaultSite
        };
      }

      public ISite DefaultSite => this.defaultSite;
    }

    /// <inheritdoc />
    internal class SiteProxy : ISite, ISecuredObject, IOwnership
    {
      private readonly Dictionary<string, bool> moduleAccessCache = new Dictionary<string, bool>();
      private IList<string> aliases;
      private IList<MultisiteContext.SiteDataSourceLinkProxy> siteDataSourceLinks;
      private string siteMapName;
      private string siteMapKey;
      private Uri uri;
      private readonly SecuredProxy securedSiteProxy;
      private readonly object fetchingCulturesSyncLock = new object();
      private IEnumerable<CultureInfo> fetchingCultures;
      private CultureInfo[] cultures;

      internal SiteProxy()
      {
      }

      public SiteProxy(Site site)
      {
        this.Id = site.Id;
        this.Name = site.Name;
        this.StagingUrl = site.StagingUrl;
        this.LiveUrl = site.LiveUrl;
        this.HomePageId = site.HomePageId;
        this.FrontEndLoginPageId = site.FrontEndLoginPageId;
        this.FrontEndLoginPageUrl = site.FrontEndLoginPageUrl;
        this.DefaultFrontendTemplateId = !site.DefaultFrontendTemplateId.IsEmpty() ? site.DefaultFrontendTemplateId : Config.Get<PagesConfig>().DefaultFrontendTemplateId;
        this.SiteMapRootNodeId = site.SiteMapRootNodeId;
        this.RequiresSsl = site.RequiresSsl;
        this.aliases = (IList<string>) site.DomainAliases.ToList<string>();
        this.IsLocatedInMainMenu = site.IsLocatedInMainMenu;
        this.Owner = site.Owner;
        this.IsDefault = site.IsDefault;
        this.SiteConfigurationMode = site.SiteConfigurationMode;
        ResourcesConfig resourcesConfig = Config.Get<ResourcesConfig>();
        CultureElement cultureElement;
        if (string.IsNullOrEmpty(site.DefaultCultureKey) || !resourcesConfig.Cultures.TryGetValue(site.DefaultCultureKey, out cultureElement))
          cultureElement = resourcesConfig.Cultures.Values.First<CultureElement>();
        this.DefaultCulture = CultureInfo.GetCultureInfo(cultureElement.UICulture);
        this.DefaultCultureKey = cultureElement.Key;
        this.SetPublicContentCultures(site.CultureKeys, resourcesConfig);
        this.securedSiteProxy = new SecuredProxy((ISecuredObject) site);
        if (!site.IsOffline)
          return;
        this.OfflineInfo = (ISiteOfflineInfo) new MultisiteContext.SiteOfflineInfo()
        {
          OfflineSiteMessage = site.OfflineSiteMessage,
          OfflinePageToRedirect = site.OfflinePageToRedirect,
          RedirectIfOffline = (site.OfflinePageToRedirect != Guid.Empty)
        };
      }

      /// <inheritdoc />
      public Guid Id { get; private set; }

      /// <inheritdoc />
      public string Name { get; internal set; }

      /// <inheritdoc />
      public Guid HomePageId { get; internal set; }

      /// <inheritdoc />
      public Guid FrontEndLoginPageId { get; internal set; }

      /// <inheritdoc />
      public string FrontEndLoginPageUrl { get; internal set; }

      /// <inheritdoc />
      public Guid DefaultFrontendTemplateId { get; internal set; }

      /// <inheritdoc />
      public Guid SiteMapRootNodeId { get; internal set; }

      /// <inheritdoc />
      public string StagingUrl { get; internal set; }

      /// <inheritdoc />
      public string LiveUrl { get; internal set; }

      /// <inheritdoc />
      public bool RequiresSsl { get; internal set; }

      /// <inheritdoc />
      [Obsolete("Use cultures.")]
      public CultureInfo[] PublicContentCultures
      {
        get => this.cultures;
        internal set => this.cultures = value;
      }

      /// <inheritdoc />
      public CultureInfo[] Cultures
      {
        get => this.cultures;
        internal set => this.cultures = value;
      }

      /// <inheritdoc />
      [Obsolete("Use cultures.")]
      public Dictionary<string, string> PublicCultures { get; internal set; }

      /// <inheritdoc />
      public CultureInfo DefaultCulture { get; internal set; }

      [Obsolete("For internal use only. Use DefaultCutlure.")]
      public string DefaultCultureKey { get; internal set; }

      public MultisiteContext.ISiteContainer Container { get; set; }

      public bool Contains(MultisiteContext.SiteProxy subSite)
      {
        if (this.Container == null || subSite.RedirectFolder.IsNullOrEmpty())
          return false;
        MultisiteContext.ISiteContainer container = this.Container;
        string redirectFolder = subSite.RedirectFolder;
        char[] chArray = new char[1]{ '/' };
        foreach (string key in redirectFolder.Split(chArray))
        {
          if (container.Folders == null || !container.Folders.TryGetValue(key, out container))
            return false;
        }
        return container.Site.Id == subSite.Id;
      }

      public string RedirectFolder { get; internal set; }

      /// <inheritdoc />
      public bool IsDefault { get; internal set; }

      /// <inheritdoc />
      public IList<string> DomainAliases
      {
        get
        {
          if (this.aliases == null)
            this.aliases = (IList<string>) new List<string>();
          return this.aliases;
        }
      }

      /// <inheritdoc />
      public IList<MultisiteContext.SiteDataSourceLinkProxy> SiteDataSourceLinks => MultisiteContext.GetDataSourcesCache().GetSiteDataSourceLinks(this.Id);

      /// <inheritdoc />
      public string SiteMapName
      {
        get
        {
          if (this.siteMapName == null)
          {
            PageManager manager = PageManager.GetManager((string) null, "sitemapname_get");
            using (new ElevatedModeRegion((IManager) manager))
            {
              using (new ReadUncommitedRegion((IManager) manager))
                this.siteMapName = manager.GetPageNode(this.SiteMapRootNodeId).Name;
            }
          }
          return this.siteMapName;
        }
      }

      /// <inheritdoc />
      public string SiteMapKey
      {
        get
        {
          if (this.siteMapKey == null)
            this.siteMapKey = SiteMapBase.GetNodeKey(this.SiteMapName) + this.SiteMapRootNodeId.ToString().ToUpperInvariant();
          return this.siteMapKey;
        }
      }

      /// <inheritdoc />
      public bool IsLocatedInMainMenu { get; private set; }

      /// <inheritdoc />
      public bool IsOffline => this.OfflineInfo != null;

      /// <inheritdoc />
      public ISiteOfflineInfo OfflineInfo { get; internal set; }

      public Guid Owner { get; set; }

      public SiteConfigurationMode SiteConfigurationMode { get; private set; }

      /// <inheritdoc />
      public Uri GetUri()
      {
        if (this.uri == (Uri) null)
          this.uri = MultisiteContext.GetSiteUri((ISite) this);
        return this.uri;
      }

      public IEnumerable<CultureInfo> FetchingCultures
      {
        get
        {
          if (this.fetchingCultures == null)
          {
            lock (this.fetchingCulturesSyncLock)
            {
              if (this.fetchingCultures == null)
              {
                List<CultureInfo> list = ((IEnumerable<CultureInfo>) this.PublicContentCultures).ToList<CultureInfo>();
                using (IEnumerator<CultureInfo> enumerator = ((IEnumerable<CultureInfo>) this.PublicContentCultures).Where<CultureInfo>((Func<CultureInfo, bool>) (c => !c.IsNeutralCulture)).GetEnumerator())
                {
label_10:
                  while (enumerator.MoveNext())
                  {
                    CultureInfo cultureInfo = enumerator.Current;
                    while (true)
                    {
                      do
                      {
                        if (cultureInfo != null && !cultureInfo.IsNeutralCulture)
                        {
                          cultureInfo = cultureInfo.Parent;
                          if (!((IEnumerable<CultureInfo>) Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.DefinedFrontendLanguages).Contains<CultureInfo>(cultureInfo))
                            goto label_10;
                        }
                        else
                          goto label_10;
                      }
                      while (list.Contains(cultureInfo));
                      list.Add(cultureInfo);
                    }
                  }
                }
                this.fetchingCultures = (IEnumerable<CultureInfo>) new ReadOnlyCollection<CultureInfo>((IList<CultureInfo>) list);
              }
            }
          }
          return this.fetchingCultures;
        }
      }

      private void SetPublicContentCultures(
        IList<string> cultureKeys,
        ResourcesConfig resourcesConfig)
      {
        List<CultureInfo> cultureInfoList = new List<CultureInfo>();
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        foreach (string cultureKey in (IEnumerable<string>) cultureKeys)
        {
          CultureElement cultureElement;
          if (resourcesConfig.Cultures.TryGetValue(cultureKey, out cultureElement))
          {
            CultureInfo cultureInfo = CultureInfo.GetCultureInfo(cultureElement.UICulture);
            cultureInfoList.Add(cultureInfo);
            dictionary.Add(cultureElement.Key, cultureElement.UICulture);
          }
        }
        if (cultureInfoList.Count == 0)
        {
          foreach (CultureElement cultureElement in (IEnumerable<CultureElement>) resourcesConfig.Cultures.Values)
          {
            cultureInfoList.Add(CultureInfo.GetCultureInfo(cultureElement.UICulture));
            dictionary.Add(cultureElement.Key, cultureElement.UICulture);
          }
        }
        this.PublicContentCultures = cultureInfoList.ToArray();
        this.PublicCultures = dictionary;
        this.Cultures = this.PublicContentCultures;
      }

      /// <inheritdoc />
      public MultisiteContext.SiteDataSourceLinkProxy GetDefaultProvider(
        string dataSourceName)
      {
        return this.SiteDataSourceLinks.SingleOrDefault<MultisiteContext.SiteDataSourceLinkProxy>((Func<MultisiteContext.SiteDataSourceLinkProxy, bool>) (pa => pa.DataSourceName == dataSourceName && pa.IsDefault));
      }

      /// <inheritdoc />
      public IEnumerable<MultisiteContext.SiteDataSourceLinkProxy> GetProviders(
        string dataSourceName)
      {
        return this.SiteDataSourceLinks.Where<MultisiteContext.SiteDataSourceLinkProxy>((Func<MultisiteContext.SiteDataSourceLinkProxy, bool>) (pa => pa.DataSourceName == dataSourceName));
      }

      /// <inheritdoc />
      public bool IsModuleAccessible(IModule module)
      {
        bool flag;
        if (!this.moduleAccessCache.TryGetValue(module.Name, out flag))
        {
          lock (this.moduleAccessCache)
          {
            if (!this.moduleAccessCache.TryGetValue(module.Name, out flag))
            {
              flag = true;
              if (module.ModuleId != Guid.Empty && !(module is DynamicAppModule))
                flag = LicenseState.CheckIsModuleLicensed(module.ModuleId, this.LiveUrl);
              if (flag)
              {
                string[] sources = SystemManager.DataSourceRegistry.GetDataSources().Where<IDataSource>((Func<IDataSource, bool>) (s => s.ModuleName == module.Name)).Select<IDataSource, string>((Func<IDataSource, string>) (s => s.Name)).ToArray<string>();
                if (sources.Length != 0 && !this.SiteDataSourceLinks.Any<MultisiteContext.SiteDataSourceLinkProxy>((Func<MultisiteContext.SiteDataSourceLinkProxy, bool>) (s => ((IEnumerable<string>) sources).Contains<string>(s.DataSourceName))))
                  flag = false;
              }
              this.moduleAccessCache[module.Name] = flag;
            }
          }
        }
        return flag;
      }

      /// <inheritdoc />
      public void SetHomePage(Guid pageId, PageManager pageManager = null)
      {
        List<CacheDependencyKey> cacheDependencyKeyList = new List<CacheDependencyKey>();
        MultisiteManager manager = MultisiteManager.GetManager();
        using (new ElevatedModeRegion((IManager) manager))
        {
          using (new DataSyncModeRegion((IManager) manager))
          {
            Site site = manager.GetSite(this.Id);
            if (site.HomePageId == pageId)
              return;
            List<Guid> pageIds = new List<Guid>()
            {
              pageId,
              site.HomePageId
            };
            MultisiteContext.SiteProxy.GetPagesCacheDependencies(cacheDependencyKeyList, (IEnumerable<Guid>) pageIds, pageManager);
            site.HomePageId = pageId;
            manager.SaveChanges();
          }
        }
        this.HomePageId = pageId;
        CacheDependency.Notify((IList<CacheDependencyKey>) cacheDependencyKeyList);
      }

      /// <summary>
      /// Adds the cache dependency keys of the given page ids to the provided List with key dependencies.
      /// </summary>
      /// <param name="cacheDependencyKeys">The cache dependency keys.</param>
      /// <param name="pageIds">The page ids.</param>
      internal static void GetPagesCacheDependencies(
        List<CacheDependencyKey> cacheDependencyKeys,
        IEnumerable<Guid> pageIds,
        PageManager pageManager = null)
      {
        if (pageManager == null)
          pageManager = PageManager.GetManager();
        using (new ElevatedModeRegion((IManager) pageManager))
        {
          foreach (Guid pageId1 in pageIds)
          {
            Guid pageId = pageId1;
            if (pageId != Guid.Empty)
            {
              PageNode pageNode = pageManager.GetPageNodes().Where<PageNode>((Expression<Func<PageNode, bool>>) (pn => pn.Id == pageId)).FirstOrDefault<PageNode>();
              if (pageNode != null)
                cacheDependencyKeys.AddRange((IEnumerable<CacheDependencyKey>) pageNode.GetKeysOfDependentObjects());
            }
          }
        }
      }

      public bool InheritsPermissions
      {
        get => this.securedSiteProxy.InheritsPermissions;
        set => this.securedSiteProxy.InheritsPermissions = value;
      }

      public bool CanInheritPermissions
      {
        get => this.securedSiteProxy.CanInheritPermissions;
        set => this.securedSiteProxy.CanInheritPermissions = value;
      }

      public IList<Telerik.Sitefinity.Security.Model.Permission> Permissions => this.securedSiteProxy.Permissions;

      public string[] SupportedPermissionSets
      {
        get => this.securedSiteProxy.SupportedPermissionSets;
        set => this.securedSiteProxy.SupportedPermissionSets = value;
      }

      public IDictionary<string, string> PermissionsetObjectTitleResKeys
      {
        get => this.securedSiteProxy.PermissionsetObjectTitleResKeys;
        set => this.securedSiteProxy.PermissionsetObjectTitleResKeys = value;
      }
    }

    internal interface ISiteContainer
    {
      ISite Site { get; set; }

      IDictionary<string, MultisiteContext.ISiteContainer> Folders { get; set; }
    }

    internal class SiteContainer : MultisiteContext.ISiteContainer
    {
      public ISite Site { get; set; }

      public IDictionary<string, MultisiteContext.ISiteContainer> Folders { get; set; }
    }

    private class SiteOfflineInfo : ISiteOfflineInfo
    {
      public string OfflineSiteMessage { get; internal set; }

      public Guid OfflinePageToRedirect { get; internal set; }

      public bool RedirectIfOffline { get; internal set; }
    }

    internal class SiteDataSourceProxy : 
      ISiteDataSource,
      IMultisiteDataProviderInfo,
      IDataProviderInfo
    {
      private HashSet<Guid> sites = new HashSet<Guid>();
      private string title;

      public SiteDataSourceProxy(SiteDataSource ds, bool isDynamicModule = false)
      {
        this.Id = ds.Id;
        this.Name = ds.Name;
        this.Provider = ds.Provider;
        this.Title = ds.Title;
        this.OwnerSiteId = ds.OwnerSiteId;
        this.IsDynamic = isDynamicModule;
      }

      public SiteDataSourceProxy(string name, string provider, Guid siteId)
      {
        this.Name = name;
        this.Provider = provider;
        this.OwnerSiteId = siteId;
      }

      public SiteDataSourceProxy(
        IDataSource dataSource,
        DataProviderInfo providerInfo,
        bool isDynamicModule = false,
        IEnumerable<Guid> sites = null)
      {
        this.Name = dataSource.Name;
        this.Provider = providerInfo.ProviderName;
        this.Title = providerInfo.ProviderTitle;
        this.IsDynamic = isDynamicModule;
        if (sites == null)
          return;
        this.sites = new HashSet<Guid>(sites);
      }

      public Guid Id { get; private set; }

      public string Name { get; private set; }

      public string Provider { get; private set; }

      public string Title
      {
        get
        {
          if (string.IsNullOrWhiteSpace(this.title))
            this.title = this.Provider;
          return this.title;
        }
        private set => this.title = value;
      }

      public Guid OwnerSiteId { get; private set; }

      public bool IsDynamic { get; private set; }

      public IEnumerable<Guid> Sites => (IEnumerable<Guid>) this.sites;

      string IDataProviderInfo.Name => this.Provider;

      bool IDataProviderInfo.IsVirtual => true;

      bool IDataProviderInfo.IsSystem => false;

      IEnumerable<string> IMultisiteDataProviderInfo.DataSources => (IEnumerable<string>) new string[1]
      {
        this.Name
      };

      internal bool AddSite(Guid siteId) => this.sites.Add(siteId);
    }

    internal class DataProviderProxy : IMultisiteDataProviderInfo, IDataProviderInfo
    {
      private HashSet<Guid> sites = new HashSet<Guid>();
      private HashSet<string> sources = new HashSet<string>();

      public DataProviderProxy(IDataProviderInfo sourceProvider)
      {
        this.Name = sourceProvider.Name;
        this.Title = sourceProvider.Title;
        this.IsSystem = sourceProvider.IsSystem;
        this.IsVirtual = sourceProvider.IsVirtual;
      }

      public string Name { get; private set; }

      public string Title { get; private set; }

      public bool IsSystem { get; private set; }

      public bool IsVirtual { get; private set; }

      public IEnumerable<Guid> Sites => (IEnumerable<Guid>) this.sites;

      IEnumerable<string> IMultisiteDataProviderInfo.DataSources => (IEnumerable<string>) new string[1]
      {
        this.Name
      };

      internal void Merge(IMultisiteDataProviderInfo provider)
      {
        foreach (Guid site in provider.Sites)
          this.sites.Add(site);
        foreach (string dataSource in provider.DataSources)
          this.sources.Add(dataSource);
      }
    }

    internal class MultisourceProvider : IMultisiteDataProviderInfo, IDataProviderInfo
    {
      private HashSet<Guid> sites = new HashSet<Guid>();
      private HashSet<string> sources = new HashSet<string>();

      public MultisourceProvider(string name, IEnumerable<ISiteDataSource> sources)
      {
        this.Name = name;
        this.Title = sources.First<ISiteDataSource>().Title;
        foreach (ISiteDataSource source in sources)
        {
          foreach (Guid site in source.Sites)
            this.sites.Add(site);
          this.sources.Add(source.Name);
        }
      }

      public string Name { get; private set; }

      public string Title { get; private set; }

      public IEnumerable<Guid> Sites => (IEnumerable<Guid>) this.sites;

      bool IDataProviderInfo.IsVirtual => false;

      bool IDataProviderInfo.IsSystem => false;

      IEnumerable<string> IMultisiteDataProviderInfo.DataSources => (IEnumerable<string>) this.sources;

      internal bool AddSite(Guid siteId) => this.sites.Add(siteId);
    }

    /// <summary>Represents a link between site and provider</summary>
    public class SiteDataSourceLinkProxy
    {
      private ISiteDataSource dataSource;

      internal SiteDataSourceLinkProxy(ISiteDataSource source, bool isDefault)
      {
        this.dataSource = source != null ? source : throw new ArgumentNullException(nameof (source));
        this.IsDefault = isDefault;
      }

      public SiteDataSourceLinkProxy(SiteDataSourceLink source)
        : this((ISiteDataSource) new MultisiteContext.SiteDataSourceProxy(source.DataSource), source.IsDefault)
      {
      }

      public SiteDataSourceLinkProxy(
        string dataSourceName,
        Guid siteId,
        string providerName,
        bool isDefault)
        : this((ISiteDataSource) new MultisiteContext.SiteDataSourceProxy(dataSourceName, providerName, siteId), isDefault)
      {
      }

      /// <summary>Gets the id of the data source link</summary>
      public Guid Id => this.dataSource.Id;

      /// <summary>Gets the name used to refer to a data souce</summary>
      public string DataSourceName => this.dataSource.Name;

      /// <summary>Gets id of the site to which this link is related</summary>
      public Guid SiteId => this.dataSource.OwnerSiteId;

      /// <summary>Gets the name used to refer to a provider</summary>
      public string ProviderName => this.dataSource.Provider;

      /// <summary>Gets the name used to get the manager type</summary>
      public string ManagerTypeName => this.dataSource.IsDynamic ? typeof (DynamicModuleManager).FullName : this.dataSource.Name;

      /// <summary>Gets the title of the provider</summary>
      public string Title => this.dataSource.Title;

      internal ISiteDataSource DataSource => this.dataSource;

      /// <summary>
      /// Gets whether the specified provider is the default one for the specified site
      /// </summary>
      public bool IsDefault { get; internal set; }
    }
  }
}
