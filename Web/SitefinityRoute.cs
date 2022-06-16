// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.SitefinityRoute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.UrlLocalizationStrategies;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Multisite.Configuration;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.ProtectionShield;
using Telerik.Sitefinity.ProtectionShield.Model;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web
{
  /// <summary>Represents a Sitefinity route.</summary>
  public class SitefinityRoute : RouteBase
  {
    private PageRouteHandler forndendHandler;
    internal const string DialogHandlerFullName = "Telerik.Web.UI.DialogHandler.aspx";
    internal const string ChartImageHandlerFullName = "ChartImage.axd";
    internal const string SiteMapNodeTokenKey = "SiteMapNode";
    internal const string TargetNode = "targetNode";
    private static bool? openHomePageAsSiteRoot;
    private const string DefaultAspxString = "default.aspx";

    /// <summary>
    /// When overridden in a derived class, returns route information about the request.
    /// </summary>
    /// <param name="httpContext">An object that encapsulates information about the HTTP request.</param>
    /// <returns>
    /// An object that contains the value sfrom the route definition if the route matches the current request, or null if the route does not match the request.
    /// </returns>
    public override RouteData GetRouteData(HttpContextBase httpContext)
    {
      if (!this.VerifyRequest(httpContext))
        return (RouteData) null;
      httpContext.Items[(object) "Culture"] = (object) SystemManager.CurrentContext.Culture;
      string virtualPathInternal = this.GetVirtualPathInternal(httpContext);
      if (virtualPathInternal != null)
      {
        SiteMapBase siteMapProvider = this.GetSiteMapProvider();
        if (siteMapProvider == null)
          return (RouteData) null;
        bool isAdditional;
        string[] pars;
        bool isHomePageAutoResolved;
        SiteMapNode siteMapNode = this.FindSiteMapNode(ref virtualPathInternal, siteMapProvider, out isAdditional, out pars, out isHomePageAutoResolved);
        if (siteMapNode != null)
        {
          PageSiteNode node1 = (PageSiteNode) siteMapNode;
          httpContext.Items[(object) "targetNode"] = (object) node1;
          if (node1.NodeType == NodeType.Standard && !node1.IsBackend && node1.IsHomePage() && SitefinityRoute.OpenHomePageAsSiteRoot && (pars == null || pars.Length == 0) && !isHomePageAutoResolved && httpContext.Request.Url.Query.IsNullOrEmpty())
            httpContext.Response.RedirectPermanent(SystemManager.CurrentContext.ResolveUrl("~/"), true);
          if (this.ProcessRedirects(httpContext, ref node1, isAdditional, pars) || !this.CheckSecurity(httpContext, node1))
            return new RouteData((RouteBase) this, (IRouteHandler) new StopRoutingHandler());
          this.SetRequestVariables(httpContext, (SiteMapProvider) siteMapProvider, node1);
          if (node1 != null && node1.IsExternallyRendered() && !httpContext.Items.Contains((object) "SkipRendererRewriteAndValidation"))
          {
            bool flag = httpContext.Request.QueryString["sfaction"] != null;
            if (!httpContext.Items.Contains((object) "X-SFRENDERER-PROXY") && !UrlPath.IsKnownProxy(httpContext))
            {
              if (httpContext.IsDebuggingEnabled || httpContext.User.Identity.IsAuthenticated & flag)
                return new RouteData((RouteBase) this, (IRouteHandler) new SiteOfflineRouteHandler(Res.Get<Labels>().CannotProcessExternalRendererPage.Arrange((object) node1.CurrentPageDataItem.Renderer))
                {
                  Title = "Cannot process page"
                });
              httpContext.Response.StatusCode = 404;
              return new RouteData((RouteBase) this, (IRouteHandler) new StopRoutingHandler());
            }
            if (!node1.IsPublished() && !flag)
              return (RouteData) null;
            string header = httpContext.Request.Headers["X-SF-WEBSERVICEPATH"];
            if (!string.IsNullOrEmpty(header))
            {
              string str = header.Trim('/');
              string components1 = httpContext.Request.Url.GetComponents(UriComponents.Path, UriFormat.Unescaped);
              string components2 = httpContext.Request.Url.GetComponents(UriComponents.Query, UriFormat.Unescaped);
              string path = "~/" + str + "/pages/Default.Model(url=@param)?@param='" + components1 + "'";
              if (!string.IsNullOrEmpty(components2))
                path = path + "&" + components2;
              httpContext.Server.TransferRequest(path, true, (string) null, httpContext.Request.Headers, false);
              return new RouteData((RouteBase) this, (IRouteHandler) new StopRoutingHandler())
              {
                DataTokens = {
                  {
                    "SiteMapNode",
                    (object) node1
                  }
                }
              };
            }
          }
          RouteData specialRouteData = this.TryGetSpecialRouteData(httpContext, node1, pars);
          if (specialRouteData != null)
            return specialRouteData;
          if (!node1.IsPublished())
            return (RouteData) null;
          if (!Config.Get<PagesConfig>().EnableBackwardCompatabilityForPagesUrls && !node1.IsBackend && !node1.AllowParametersValidation && pars != null && pars.Length != 0)
            return (RouteData) null;
          if (SystemManager.IsShieldEnabled)
          {
            if (!SystemManager.IsBackendRequest())
            {
              if (!AccessTokenHandler.GetHandler().IsAccessGranted(SystemManager.CurrentHttpContext))
              {
                SystemManager.CurrentHttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                return new RouteData((RouteBase) this, (IRouteHandler) new ProtectionShieldRouteHandler());
              }
            }
            else
            {
              AccessTokenHandler handler = AccessTokenHandler.GetHandler();
              if (!handler.IsAccessGranted(SystemManager.CurrentHttpContext))
                handler.GrantCurrentUserAccess(AccessTokenReason.BackEndUser);
            }
          }
          if (siteMapProvider is SitefinitySiteMap)
          {
            ISite currentSite = SystemManager.CurrentContext.CurrentSite;
            if (currentSite.IsOffline)
            {
              SiteMapNode node2 = (SiteMapNode) null;
              if (currentSite.OfflineInfo.RedirectIfOffline && currentSite.OfflineInfo.OfflinePageToRedirect != Guid.Empty)
                node2 = siteMapProvider.FindSiteMapNodeFromKey(currentSite.OfflineInfo.OfflinePageToRedirect.ToString(), true);
              return node2 != null ? this.GetRouteDataInternal(pars, httpContext.Request.QueryString, node2) : new RouteData((RouteBase) this, (IRouteHandler) new SiteOfflineRouteHandler(currentSite.OfflineInfo.OfflineSiteMessage));
            }
          }
          this.OnNodePreProcessing(httpContext, node1);
          return this.GetRouteDataInternal(pars, httpContext.Request.QueryString, (SiteMapNode) node1);
        }
        if (string.IsNullOrEmpty(virtualPathInternal) || virtualPathInternal.Equals("/"))
          return new RouteData((RouteBase) this, ObjectFactory.IsTypeRegistered<IRouteHandler>("UnderConstructionRouteHandler") ? ObjectFactory.Resolve<IRouteHandler>("UnderConstructionRouteHandler") : (IRouteHandler) new UnderConstructionRouteHandler());
      }
      return (RouteData) null;
    }

    internal SiteMapNode FindSiteMapNode(
      ref string virtualPath,
      SiteMapBase siteMapProvider,
      out bool isAdditional,
      out string[] pars,
      out bool isHomePageAutoResolved,
      bool skipSubSiteCheck = false)
    {
      ISiteContext siteContext = (ISiteContext) null;
      string str1 = (string) null;
      IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
      if (!skipSubSiteCheck && multisiteContext != null && !ControlExtensions.IsBackend())
      {
        siteContext = multisiteContext.CurrentSiteContext;
        string str2 = multisiteContext.UnresolveUrlAndApplyDetectedSite(virtualPath);
        if (str2 != virtualPath)
        {
          str1 = virtualPath;
          virtualPath = str2;
        }
      }
      virtualPath = this.ProcessLanguage(virtualPath);
      SiteMapNode siteMapNode = siteMapProvider.FindSiteMapNode(virtualPath, false, false, out isAdditional, out pars, out isHomePageAutoResolved);
      if (siteMapNode != null || str1 == null || !Config.Get<MultisiteConfig>().EnableSubFolderSiteFallback)
        return siteMapNode;
      virtualPath = str1;
      SystemManager.CurrentContext.MultisiteContext.ChangeCurrentSite(siteContext.Site, siteContext.ResolutionType);
      return this.FindSiteMapNode(ref virtualPath, siteMapProvider, out isAdditional, out pars, out isHomePageAutoResolved, true);
    }

    protected virtual void OnNodePreProcessing(HttpContextBase httpContext, PageSiteNode siteNode)
    {
    }

    /// <summary>
    /// When overridden in a derived class, checks whether the route matches the specified values, and if so, generates a URL and retrieves information about the route.
    /// </summary>
    /// <param name="requestContext">An object that encapsulates information about the requested route.</param>
    /// <param name="values">An object that contains the parameters for a route.</param>
    /// <returns>
    /// An object that contains the generated URL and information about the route, or null if the route does not match <paramref name="values" />.
    /// </returns>
    public override VirtualPathData GetVirtualPath(
      RequestContext requestContext,
      RouteValueDictionary values)
    {
      throw new NotImplementedException();
    }

    internal virtual bool CheckSecurity(HttpContextBase httpContext, PageSiteNode node)
    {
      bool flag = true;
      if (!node.IsAccessibleToUser(HttpContext.Current))
      {
        SFClaimsAuthenticationManager.ProcessRejectedUser(httpContext.ApplicationInstance != null ? httpContext : SystemManager.CurrentHttpContext);
        flag = false;
        if (SystemManager.CurrentHttpContext.User.Identity.IsAuthenticated)
          throw new HttpException(403, Res.Get<PageResources>().YouAreNotAuthorizedToAccessThisPage);
        ClaimsManager.CurrentAuthenticationModule.SendUnauthorizedResponse(httpContext);
      }
      return flag;
    }

    protected virtual string ProcessLanguage(string virtualPath)
    {
      RouteHelper.ApplyThreadCulturesForCurrentUser();
      CultureInfo culture;
      if (!SystemManager.IsBackendRequest(out culture))
        return ObjectFactory.Resolve<UrlLocalizationService>().UnResolveUrlAndApplyDetectedCultures(virtualPath);
      if (culture == null && !(this is BackendRoute))
        culture = SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage;
      if (culture != null)
        SystemManager.CurrentContext.Culture = culture;
      return virtualPath;
    }

    protected virtual RouteData TryGetSpecialRouteData(
      HttpContextBase httpContext,
      PageSiteNode node,
      string[] urlPrams)
    {
      RouteData pageEditorHandler = RouteHelper.GetPageEditorHandler(httpContext, (RouteBase) this, (SiteMapNode) node, urlPrams);
      if (pageEditorHandler != null)
      {
        foreach (IRedirectStrategy redirectStrategy in ObjectFactory.Container.ResolveAll(typeof (IRedirectStrategy)))
        {
          string url = redirectStrategy.Redirect(httpContext, (SiteMapNode) node);
          if (!string.IsNullOrEmpty(url))
          {
            httpContext.Response.Redirect(url, false);
            if (httpContext.ApplicationInstance != null)
              httpContext.ApplicationInstance.CompleteRequest();
            return new RouteData((RouteBase) this, (IRouteHandler) new StopRoutingHandler());
          }
        }
      }
      return pageEditorHandler;
    }

    /// <summary>Gets the virtual path.</summary>
    /// <param name="httpContext">The http context.</param>
    /// <returns>The virtual path as a string.</returns>
    protected internal virtual string GetVirtualPathInternal(HttpContextBase httpContext)
    {
      string str = (httpContext.Request.AppRelativeCurrentExecutionFilePath + httpContext.Request.PathInfo).Substring(2);
      if (!HttpRuntime.UsingIntegratedPipeline && str.EndsWith("default.aspx", StringComparison.OrdinalIgnoreCase))
        str = str.Substring(0, str.Length - 12);
      return HttpUtility.UrlDecode(str);
    }

    /// <summary>Verifies the request</summary>
    /// <param name="httpContext">The HTTP context.</param>
    /// <returns>A boolean value indicating if the request is ok</returns>
    protected virtual bool VerifyRequest(HttpContextBase httpContext)
    {
      string fileName = VirtualPathUtility.GetFileName(httpContext.Request.FilePath);
      return fileName.IsNullOrEmpty() || fileName.Equals("default.aspx", StringComparison.OrdinalIgnoreCase) || !SitefinityRoute.IsNotAllowedPageExtension(VirtualPathUtility.GetExtension(fileName));
    }

    /// <summary>Processes redirects.</summary>
    /// <param name="httpContext">The HTTP context.</param>
    /// <param name="node">The <see cref="T:Telerik.Sitefinity.Web.PageSiteNode" />.</param>
    /// <param name="isAdditionalUrl">The is additional url.</param>
    /// <param name="urlParams">The URL parameters.</param>
    /// <returns>Returns whether a redirect is needed.</returns>
    protected virtual bool ProcessRedirects(
      HttpContextBase httpContext,
      ref PageSiteNode node,
      bool isAdditionalUrl,
      string[] urlParams)
    {
      switch (node.NodeType)
      {
        case NodeType.Group:
          PageSiteNode firstPageDataNode = RouteHelper.GetFirstPageDataNode(node, !ControlExtensions.IsBackend(), SystemManager.CurrentContext.Culture.Name);
          if (firstPageDataNode == null)
            throw new HttpException(404, "The requested page doesn't exist or you do not have sufficient privileges to view it.");
          if (Config.Get<PagesConfig>().IsToRedirectFromGroupPage && firstPageDataNode.NodeType != NodeType.OuterRedirect && !firstPageDataNode.Url.Equals(node.Url, StringComparison.OrdinalIgnoreCase))
          {
            this.RedirectPermanent(httpContext, firstPageDataNode.Url, true);
            return true;
          }
          node = firstPageDataNode;
          return this.ProcessRedirects(httpContext, ref node, isAdditionalUrl, urlParams);
        case NodeType.InnerRedirect:
          this.RedirectPermanent(httpContext, (node.GetTerminalNode(false) ?? throw new HttpException(404, "The requested page doesn't exist or you do not have sufficient privileges to view it.")).Url, true);
          return true;
        case NodeType.OuterRedirect:
          this.RedirectPermanent(httpContext, node.RedirectUrl, true);
          return true;
        default:
          if (!isAdditionalUrl || !node.AdditionalUrlsRedirectToDefault)
            return RouteHelper.SslRedirectIfNeeded(httpContext, node);
          NameValueCollection queryString = httpContext.Request.QueryString;
          string str = queryString.Count == 0 ? string.Empty : "?" + string.Join("&", Array.ConvertAll<string, string>(queryString.AllKeys, (Converter<string, string>) (key => string.Format("{0}={1}", (object) key, (object) queryString[key]))));
          string url;
          if (SystemManager.CurrentHttpContext.Items[(object) "AdditionalUrlCulture"] is CultureInfo cultureInfo)
          {
            url = (urlParams != null ? node.GetUrl(cultureInfo, false, false) + "/" + string.Join("/", urlParams) : node.GetUrl(cultureInfo, false, true)) + str;
            if (!SystemManager.IsBackendRequest(out CultureInfo _))
              url = ObjectFactory.Resolve<UrlLocalizationService>().ResolveUrl(url, cultureInfo);
            IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
            if (!node.IsBackend && multisiteContext != null)
              url = multisiteContext.ResolveUrl(url);
          }
          else
            url = (urlParams != null ? node.UrlWithoutExtension + "/" + string.Join("/", urlParams) : node.Url) + str;
          this.RedirectPermanent(httpContext, url, true);
          return true;
      }
    }

    /// <summary>Redirects permanent</summary>
    /// <param name="httpContext">The HTTP context.</param>
    /// <param name="url">The url.</param>
    /// <param name="endResponse">The end response.</param>
    protected virtual void RedirectPermanent(
      HttpContextBase httpContext,
      string url,
      bool endResponse = false)
    {
      RouteHelper.RedirectPermanent(httpContext, url, endResponse);
    }

    /// <summary>Sets the requests variables.</summary>
    /// <param name="httpContext">The HTTP context.</param>
    /// <param name="siteMapProvider">The site map provider.</param>
    /// <param name="node">The page site node.</param>
    protected virtual void SetRequestVariables(
      HttpContextBase httpContext,
      SiteMapProvider siteMapProvider,
      PageSiteNode node)
    {
      httpContext.Items[(object) "ServedPageNode"] = (object) node;
      httpContext.Items[(object) "SF_SiteMap"] = (object) siteMapProvider;
      if (httpContext.Items.Contains((object) "sfContentFilters"))
        return;
      httpContext.Items.Add((object) "sfContentFilters", (object) new string[1]
      {
        "LinksParser"
      });
    }

    /// <summary>Gets the site map provider</summary>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Web.SiteMapBase" /></returns>
    protected virtual SiteMapBase GetSiteMapProvider() => SiteMapBase.GetSiteMapProviderForRequest() as SiteMapBase;

    /// <summary>
    /// Gets the route data by parameters, query and sitemap node.
    /// </summary>
    /// <param name="parameters">String parameters.</param>
    /// <param name="query">The query.</param>
    /// <param name="node">The site map node.</param>
    /// <returns>The route data.</returns>
    protected virtual RouteData GetRouteDataInternal(
      string[] parameters,
      NameValueCollection query,
      SiteMapNode node)
    {
      if (this.forndendHandler == null)
        this.forndendHandler = ObjectFactory.Resolve<PageRouteHandler>();
      RouteData routeDataInternal = new RouteData((RouteBase) this, (IRouteHandler) this.forndendHandler);
      routeDataInternal.DataTokens.Add("SiteMapNode", (object) node);
      if (parameters != null && parameters.Length != 0)
        routeDataInternal.Values.Add("Params", (object) parameters);
      if (query != null && query.Count > 0)
        routeDataInternal.Values.Add("Query", (object) query);
      return routeDataInternal;
    }

    /// <summary>Gets whether a language fallback is needed.</summary>
    protected virtual bool LanguageFallback => false;

    /// <summary>
    /// Gets a value indicating whether the open home page as site root configuration.
    /// </summary>
    /// <value>The open home page as site root.</value>
    internal static bool OpenHomePageAsSiteRoot
    {
      get
      {
        if (!SitefinityRoute.openHomePageAsSiteRoot.HasValue)
          SitefinityRoute.openHomePageAsSiteRoot = new bool?(Config.Get<PagesConfig>().OpenHomePageAsSiteRoot);
        return SitefinityRoute.openHomePageAsSiteRoot.Value;
      }
    }

    /// <summary>
    /// Gets a boolean value indicating whether the page extension is not allowed
    /// </summary>
    /// <param name="ext">The extension.</param>
    /// <returns>A boolean value indicating whether the page extension is not allowed</returns>
    internal static bool IsNotAllowedPageExtension(string ext)
    {
      if (!string.IsNullOrEmpty(ext))
      {
        ISet<string> allowedExtensions = Config.Get<PagesConfig>().GetNotAllowedExtensions();
        if (allowedExtensions.Count > 0 && allowedExtensions.Contains(ext))
          return true;
      }
      return false;
    }

    internal static bool IsKnownPageExtension(string ext)
    {
      if (!string.IsNullOrEmpty(ext))
      {
        ISet<string> knownExtensions = Config.Get<PagesConfig>().KnownExtensions;
        if (knownExtensions.Count == 0 || !knownExtensions.Contains(ext))
          return false;
      }
      return true;
    }
  }
}
