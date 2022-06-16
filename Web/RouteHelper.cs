// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.RouteHelper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Hosting;
using System.Web.Routing;
using System.Web.UI;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Fluent;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Model.Localization;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Modules.ResponsiveDesign.Web;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.Events;

namespace Telerik.Sitefinity.Web
{
  /// <summary>Helper class for working with routes.</summary>
  public static class RouteHelper
  {
    private const string NodeReferenceQualifier = "node";
    private static readonly Regex ReferenceRegex = RouteHelper.CompileReferenceRegex(new string[1]
    {
      "node"
    });
    internal static object UrlParamtersResolvedKey = (object) "UrlParametersResolution";
    /// <summary>
    /// Url segment name indicating that an action name follows.
    /// </summary>
    public const string ActionKey = "Action";

    /// <summary>Splits the URL to path segment strings.</summary>
    /// <param name="url">The URL.</param>
    /// <param name="skipPathSeparators">if set to <c>true</c> path separators are not added to the list.</param>
    /// <returns>The path segment strings.</returns>
    public static IList<string> SplitUrlToPathSegmentStrings(
      string url,
      bool skipPathSeparators)
    {
      List<string> pathSegmentStrings = new List<string>();
      int num;
      if (!string.IsNullOrEmpty(url))
      {
        for (int startIndex = 0; startIndex < url.Length; startIndex = num + 1)
        {
          num = url.IndexOf('/', startIndex);
          if (num == -1)
          {
            string str = url.Substring(startIndex);
            if (str.Length > 0)
              pathSegmentStrings.Add(str);
            return (IList<string>) pathSegmentStrings;
          }
          string str1 = url.Substring(startIndex, num - startIndex);
          if (str1.Length > 0)
            pathSegmentStrings.Add(str1);
          if (!skipPathSeparators)
            pathSegmentStrings.Add("/");
        }
      }
      return (IList<string>) pathSegmentStrings;
    }

    /// <summary>
    /// Returns information about the URL that is associated with the route.
    /// </summary>
    /// <returns>
    /// An object that contains information about the URL that is associated with the route.
    /// </returns>
    /// <param name="handler">The handler
    /// <see cref="T:Telerik.Sitefinity.Web.IPartialRouteHandler" />
    /// </param>
    public static string GetVirtualPath(IPartialRouteHandler handler) => RouteHelper.GetVirtualPath(handler, (RouteValueDictionary) null, UrlResolveOptions.None);

    /// <summary>
    /// Returns information about the URL that is associated with the route.
    /// </summary>
    /// <returns>
    /// An object that contains information about the URL that is associated with the route.
    /// </returns>
    /// <param name="handler"> The handler
    /// <see cref="T:Telerik.Sitefinity.Web.IPartialRouteHandler" />
    /// </param>
    /// <param name="values">
    /// An object that contains the parameters for a route.
    /// </param>
    public static string GetVirtualPath(IPartialRouteHandler handler, RouteValueDictionary values) => RouteHelper.GetVirtualPath(handler, values, UrlResolveOptions.None);

    /// <summary>
    /// Returns information about the URL that is associated with the route.
    /// </summary>
    /// <returns>
    /// An object that contains information about the URL that is associated with the route.
    /// </returns>
    /// <param name="handler"> The handler
    /// <see cref="T:Telerik.Sitefinity.Web.IPartialRouteHandler" />
    /// </param>
    /// <param name="values">
    /// An object that contains the parameters for a route.
    /// </param>
    /// <param name="urlOptions">
    /// Specifies how an URL should be resolved.
    /// </param>
    public static string GetVirtualPath(
      IPartialRouteHandler handler,
      RouteValueDictionary values,
      UrlResolveOptions urlOptions)
    {
      if (handler == null)
        throw new ArgumentNullException(nameof (handler));
      StringBuilder stringBuilder = new StringBuilder();
      Stack<IPartialRouteHandler> partialRouteHandlerStack = new Stack<IPartialRouteHandler>();
      for (IPartialRouteHandler partialRouteHandler = handler; partialRouteHandler != null; partialRouteHandler = partialRouteHandler.ParentRouteHandler)
        partialRouteHandlerStack.Push(partialRouteHandler);
      while (partialRouteHandlerStack.Count > 0)
      {
        IPartialRouteHandler handler1 = partialRouteHandlerStack.Pop();
        RouteValueDictionary values1;
        if (handler1.Equals((object) handler))
        {
          values1 = values;
        }
        else
        {
          values1 = new RouteValueDictionary((IDictionary<string, object>) handler1.PartialRequestContext.RouteData.Values);
          values1[partialRouteHandlerStack.Peek().PartialRequestContext.PathKey] = (object) null;
        }
        if (stringBuilder.Length > 0 && stringBuilder[stringBuilder.Length - 1] != '/')
          stringBuilder.Append('/');
        stringBuilder.Append(RouteHelper.GetPath(handler1, values1));
      }
      return RouteHelper.ResolveUrl(stringBuilder.ToString(), urlOptions);
    }

    /// <summary>
    /// Returns information about the URL that is associated with the route.
    /// </summary>
    /// <returns>
    /// An object that contains information about the URL that is associated with the route.
    /// </returns>
    /// <param name="handlerType"> The handler type
    /// <see cref="T:Telerik.Sitefinity.Web.IPartialRouteHandler" />
    /// </param>
    /// <param name="values">
    /// An object that contains the parameters for a route.
    /// </param>
    /// <param name="urlOptions">
    /// Specifies how an URL should be resolved.
    /// </param>
    public static string GetVirtualPath(
      Type handlerType,
      RouteValueDictionary values,
      UrlResolveOptions urlOptions)
    {
      return RouteHelper.GetVirtualPath(handlerType, (string) null, values, urlOptions);
    }

    /// <summary>
    /// Returns information about the URL that is associated with the route.
    /// </summary>
    /// <returns>
    /// An object that contains information about the URL that is associated with the route.
    /// </returns>
    /// <param name="handlerType"> The handler type
    /// <see cref="T:Telerik.Sitefinity.Web.IPartialRouteHandler" />
    /// </param>
    /// <param name="handlerName">The name of the partial route handler.</param>
    /// <param name="values">
    /// An object that contains the parameters for a route.
    /// </param>
    /// <param name="urlOptions">
    /// Specifies how an URL should be resolved.
    /// </param>
    public static string GetVirtualPath(
      Type handlerType,
      string handlerName,
      RouteValueDictionary values,
      UrlResolveOptions urlOptions)
    {
      if (handlerType == (Type) null)
        throw new ArgumentNullException(nameof (handlerType));
      return RouteHelper.ResolveUrl(RouteHandlerBase.GetVirtualPath(handlerType, handlerName, values), urlOptions);
    }

    /// <summary>
    /// Creates a specially formatted placeholder, which on resolution time is replaced by the base URL of the specified node.
    /// </summary>
    /// <param name="nodeId">The ID of the node.</param>
    /// <param name="backend">A value indicating whether to use <see cref="T:Telerik.Sitefinity.Web.BackendSiteMap" />.</param>
    /// <returns>The node reference.</returns>
    public static string CreateNodeReference(Guid nodeId, bool backend = true) => string.Format("[{0}:{1}{2}]", (object) "node", (object) nodeId, backend ? (object) string.Empty : (object) "/sitefinity");

    /// <summary>
    /// Resolves the specified URL according to the specified options.
    /// </summary>
    /// <param name="url">The URL to resolve.</param>
    /// <param name="urlOptions">
    /// Specifies how an URL should be resolved.
    /// </param>
    /// <returns>Resolved URL.</returns>
    public static string ResolveUrl(string url, UrlResolveOptions urlOptions)
    {
      if (!string.IsNullOrEmpty(url) && !url.StartsWith("javascript:", StringComparison.InvariantCultureIgnoreCase))
      {
        if (url.Contains("["))
          url = RouteHelper.ReferenceRegex.Replace(url, RouteHelper.ReferenceMatchEvaluator((RouteHelper.ReferenceEvaluator) ((type, value, options) =>
          {
            if (!(type.ToLower() == "node"))
              throw new ArgumentException("Unsupported URL reference type:" + type);
            SiteMapNode siteMapNode = BackendSiteMap.FindSiteMapNode(Guid.Parse(value), false);
            return siteMapNode != null ? siteMapNode.Url : string.Empty;
          })));
        if ((urlOptions & UrlResolveOptions.AppendTrailingSlash) != UrlResolveOptions.None)
          url = VirtualPathUtility.AppendTrailingSlash(url);
        else if ((urlOptions & UrlResolveOptions.RemoveTrailingSlash) != UrlResolveOptions.None)
          url = VirtualPathUtility.RemoveTrailingSlash(url);
        string str = !url.StartsWith("/", StringComparison.OrdinalIgnoreCase) ? (!url.StartsWith("~/", StringComparison.OrdinalIgnoreCase) ? ((urlOptions & UrlResolveOptions.Absolute) == UrlResolveOptions.None ? "~/" + url : url) : url) : "~" + url;
        if ((urlOptions & UrlResolveOptions.Absolute) != UrlResolveOptions.None)
          url = UrlPath.ResolveAbsoluteUrl(str);
        else if ((urlOptions & UrlResolveOptions.Rooted) != UrlResolveOptions.None)
          url = UrlPath.ResolveUrl(str);
        else if ((urlOptions & UrlResolveOptions.ApplicationRelative) != UrlResolveOptions.None)
          url = str;
        else if ((urlOptions & UrlResolveOptions.CurrentRequestRelative) != UrlResolveOptions.None)
          url = UrlPath.ResolveUrl(url);
      }
      return url;
    }

    /// <summary>Gets the absolute URL.</summary>
    /// <param name="url">The URL.</param>
    /// <returns>The absolute URL.</returns>
    public static string GetAbsoluteUrl(string url)
    {
      string str = url;
      string absoluteUrl;
      if (str.StartsWith("~"))
      {
        absoluteUrl = RouteHelper.ResolveUrl(str, UrlResolveOptions.Absolute);
      }
      else
      {
        Uri uri = new Uri(str);
        absoluteUrl = uri.IsAbsoluteUri ? uri.ToString() : VirtualPathUtility.ToAbsolute(uri.ToString());
      }
      return absoluteUrl;
    }

    /// <summary>
    /// Checks if the url is complete (does not need to be resolved or modified)
    /// </summary>
    /// <param name="url">The url to be checked.</param>
    /// <returns>Whether the url is complete.</returns>
    public static bool IsCompleteUrl(string url) => url.StartsWith("http://") || url.StartsWith("https://");

    /// <summary>Sets the partial route handler.</summary>
    /// <param name="partHandler">The partial route handler.</param>
    /// <param name="parentHandler">The parent handler.</param>
    /// <param name="routes">The routes.</param>
    /// <param name="path">The path.</param>
    /// <param name="routeKey">The route key.</param>
    public static void SetPartialRouteHandler(
      IPartialRouteHandler partHandler,
      IPartialRouteHandler parentHandler,
      RouteCollection routes,
      string path,
      string routeKey)
    {
      if (string.IsNullOrEmpty(routeKey))
        routeKey = "Params";
      partHandler.ParentRouteHandler = parentHandler;
      PartialHttpContext httpContext;
      RouteData routeData;
      if (routes != null)
      {
        httpContext = new PartialHttpContext(path);
        routeData = routes.GetRouteData((HttpContextBase) httpContext) ?? new RouteData();
      }
      else
      {
        httpContext = new PartialHttpContext(string.Empty);
        routeData = new RouteData();
      }
      partHandler.PartialRequestContext = new PartialRequestContext(httpContext, routeData, routeKey);
    }

    /// <summary>
    /// Creates <see cref="T:System.Web.HttpContext" /> object for the provided URL.
    /// </summary>
    /// <param name="rawUrl">The URL to create context for.</param>
    /// <returns>The HttpContext.</returns>
    public static HttpContext CreateHttpContext(string rawUrl) => RouteHelper.CreateHttpContext(rawUrl, (TextWriter) null);

    /// <summary>
    /// Creates <see cref="T:System.Web.HttpContext" /> object for the provided URL.
    /// </summary>
    /// <param name="rawUrl">The URL to create context for.</param>
    /// <param name="writer">The writer.</param>
    /// <returns>A <see cref="T:System.Web.HttpContext" /> object.</returns>
    public static HttpContext CreateHttpContext(string rawUrl, TextWriter writer)
    {
      HttpContext context;
      if (writer == null && HttpContext.Current != null && rawUrl.Equals(HttpContext.Current.Request.RawUrl, StringComparison.OrdinalIgnoreCase))
      {
        context = HttpContext.Current;
      }
      else
      {
        if (writer == null)
          writer = (TextWriter) new StringWriter((IFormatProvider) CultureInfo.InvariantCulture);
        int length = rawUrl.IndexOf("?", StringComparison.OrdinalIgnoreCase);
        string queryString;
        string str;
        if (length != -1)
        {
          queryString = rawUrl.Substring(length + 1);
          str = rawUrl.Substring(0, length);
        }
        else
        {
          queryString = string.Empty;
          str = rawUrl;
        }
        context = new HttpContext(new System.Web.HttpRequest(RouteHelper.IsAbsoluteUrl(str) ? str : HostingEnvironment.MapPath(str), UrlPath.ResolveAbsoluteUrl(str), queryString), new System.Web.HttpResponse(writer));
        context.ApplicationInstance = HttpContext.Current.ApplicationInstance;
        RouteHelper.SetSslOffloadingFlag(context);
      }
      return context;
    }

    /// <summary>
    /// Creates new request context for the provided <see cref="T:System.Web.HttpContext" />
    /// </summary>
    /// <param name="httpContext">The <see cref="T:System.Web.HttpContext" />.</param>
    /// <returns>A <see cref="T:System.Web.Routing.RequestContext" /> object.</returns>
    public static RequestContext CreateRequestContext(HttpContext httpContext)
    {
      HttpContextWrapper httpContext1 = new HttpContextWrapper(httpContext);
      RouteData routeData = RouteTable.Routes.GetRouteData((HttpContextBase) httpContext1);
      return routeData != null ? new RequestContext((HttpContextBase) httpContext1, routeData) : (RequestContext) null;
    }

    /// <summary>Creates new request context for the provided URL.</summary>
    /// <param name="rawUrl">The URL to create context for.</param>
    /// <returns>A <see cref="T:System.Web.Routing.RequestContext" /> object.</returns>
    public static RequestContext CreateRequestContext(string rawUrl) => RouteHelper.CreateRequestContext(RouteHelper.CreateHttpContext(rawUrl));

    /// <summary>Gets the page editor handler.</summary>
    /// <param name="httpContext">The HTTP context.</param>
    /// <param name="route">The route.</param>
    /// <param name="node">The node.</param>
    /// <param name="parameters">The parameters.</param>
    /// <returns>The page editor handler.</returns>
    public static RouteData GetPageEditorHandler(
      HttpContextBase httpContext,
      RouteBase route,
      SiteMapNode node,
      string[] parameters)
    {
      if (parameters != null && parameters.Length > 1)
      {
        List<string> list = ((IEnumerable<string>) parameters).ToList<string>();
        int index = -1;
        if ("Action".Equals(parameters[parameters.Length - 2], StringComparison.OrdinalIgnoreCase))
          index = parameters.Length - 2;
        else if (parameters.Length > 2 && "Action".Equals(parameters[parameters.Length - 3], StringComparison.OrdinalIgnoreCase))
          index = parameters.Length - 3;
        if (index > -1)
        {
          list.RemoveAt(index);
          string parameter = parameters[index + 1];
          bool isPreview = parameter.Equals("Preview", StringComparison.OrdinalIgnoreCase);
          bool isMobilePreview = parameter.Equals("MobilePreview", StringComparison.OrdinalIgnoreCase);
          bool isInlineEditing = parameter.Equals("InEdit", StringComparison.OrdinalIgnoreCase) && ControlExtensions.BrowseAndEditIsEnabled();
          if (parameter.Equals("Edit", StringComparison.OrdinalIgnoreCase) | isPreview | isMobilePreview | isInlineEditing)
          {
            list.RemoveAt(index);
            BackendRestriction.Current.EndRequestIfForbidden(httpContext);
            IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
            if (!SecurityManager.IsBackendUser())
            {
              SFClaimsAuthenticationManager.ProcessRejectedUser(httpContext.ApplicationInstance != null ? httpContext : SystemManager.CurrentHttpContext);
              SecurityManager.RedirectToLogin(httpContext);
              return (RouteData) null;
            }
            if (multisiteContext != null)
            {
              SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
              if (!multisiteContext.GetAllowedSites(currentIdentity.UserId, currentIdentity.MembershipProvider).Contains<Guid>(multisiteContext.CurrentSite.Id))
              {
                ClaimsManager.CurrentAuthenticationModule.RedirectToSiteNotAccessiblePage(httpContext);
                return new RouteData(route, (IRouteHandler) new StopRoutingHandler());
              }
            }
            CultureInfo culture = (CultureInfo) null;
            if (isInlineEditing)
            {
              culture = SystemManager.CurrentContext.Culture;
            }
            else
            {
              if (index > 0)
                return (RouteData) null;
              if (index == parameters.Length - 3)
              {
                culture = CultureInfo.GetCultureInfo(parameters[index + 2]);
                list.RemoveAt(index);
              }
            }
            Guid rootKey = (node as PageSiteNode).RootKey;
            httpContext.Items[(object) "IsFrontendPageEdit"] = (object) (rootKey != SiteInitializer.BackendRootNodeId);
            return RouteHelper.GetPageEditorHandler(route, node, list.ToArray(), isPreview, isMobilePreview, isInlineEditing, culture);
          }
        }
      }
      return (RouteData) null;
    }

    /// <summary>Gets the page editor handler.</summary>
    /// <param name="route">The route.</param>
    /// <param name="node">The node.</param>
    /// <param name="parameters">The parameters.</param>
    /// <param name="isPreview">The is preview.</param>
    /// <param name="isMobilePreview">The is mobile preview.</param>
    /// <param name="isInlineEditing">The is inline editing.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>The page editor handler.</returns>
    private static RouteData GetPageEditorHandler(
      RouteBase route,
      SiteMapNode node,
      string[] parameters,
      bool isPreview,
      bool isMobilePreview,
      bool isInlineEditing,
      CultureInfo culture)
    {
      IRouteHandler routeHandler;
      if (!isMobilePreview)
      {
        routeHandler = (IRouteHandler) ObjectFactory.Resolve<PageEditorRouteHandler>();
        PageEditorRouteHandler editorRouteHandler = (PageEditorRouteHandler) routeHandler;
        editorRouteHandler.IsPreview = isPreview;
        editorRouteHandler.IsInlineEditing = isInlineEditing;
        editorRouteHandler.ObjectEditCulture = culture;
      }
      else
        routeHandler = (IRouteHandler) ObjectFactory.Resolve<MobilePreviewRouteHandler>();
      return new RouteData(route, routeHandler)
      {
        DataTokens = {
          {
            "SiteMapNode",
            (object) node
          }
        },
        Values = {
          {
            "Params",
            (object) parameters
          }
        }
      };
    }

    /// <summary>SSLs the redirect if needed.</summary>
    /// <param name="httpContext">The HTTP context.</param>
    /// <param name="node">The node.</param>
    /// <returns>Whether the redirect is needed.</returns>
    public static bool SslRedirectIfNeeded(HttpContextBase httpContext, PageSiteNode node)
    {
      if (httpContext == null)
        throw new ArgumentNullException(nameof (httpContext));
      bool requireSsl = node != null ? node.RequireSsl : throw new ArgumentNullException(nameof (node));
      return RouteHelper.SslRedirectIfNeeded(httpContext, requireSsl);
    }

    internal static bool SslRedirectIfNeeded(HttpContextBase httpContext, bool requireSsl)
    {
      string url = string.Empty;
      bool flag = UrlPath.IsSecuredConnection(httpContext);
      Telerik.Sitefinity.Services.SiteUrlSettings siteUrlSettings = Config.Get<SystemConfig>().SiteUrlSettings;
      if (!flag & requireSsl)
      {
        if (siteUrlSettings.EnableNonDefaultSiteUrlSettings)
          url = new UriBuilder(SystemManager.CurrentContext.CurrentSite.GetUri())
          {
            Path = httpContext.Request.Url.AbsolutePath,
            Query = RouteHelper.TrimQuestionMark(httpContext.Request.Url.Query),
            Scheme = "https",
            Port = (siteUrlSettings.NonDefaultHttpsPort.IsNullOrEmpty() ? 443 : int.Parse(siteUrlSettings.NonDefaultHttpsPort))
          }.Uri.AbsoluteUri;
        else
          url = httpContext.Request.Url.AbsoluteUri.Replace("http://", "https://");
      }
      else if (flag && !requireSsl && siteUrlSettings.RemoveNonRequiredSsl)
      {
        if (siteUrlSettings.EnableNonDefaultSiteUrlSettings)
          url = new UriBuilder(SystemManager.CurrentContext.CurrentSite.GetUri())
          {
            Path = httpContext.Request.Url.AbsolutePath,
            Query = RouteHelper.TrimQuestionMark(httpContext.Request.Url.Query),
            Scheme = "http",
            Port = (siteUrlSettings.NonDefaultHttpPort.IsNullOrEmpty() ? 80 : int.Parse(siteUrlSettings.NonDefaultHttpPort))
          }.Uri.AbsoluteUri;
        else
          url = httpContext.Request.Url.AbsoluteUri.Replace("https://", "http://");
      }
      if (string.IsNullOrEmpty(url))
        return false;
      RouteHelper.RedirectPermanent(httpContext, url, true);
      return true;
    }

    /// <summary>Removes the ? from the query param</summary>
    /// <param name="queryToTrim">The query param</param>
    /// <returns>The query param without leading ?</returns>
    private static string TrimQuestionMark(string queryToTrim)
    {
      if (queryToTrim.Length <= 1)
        return queryToTrim;
      return queryToTrim.TrimStart('?');
    }

    /// <summary>
    /// Gets the preferred language from the backend user profile and sets it as Culture and UI Culture
    /// </summary>
    /// <remarks>
    /// If the user is not a backend one no cultures changes are made
    /// </remarks>
    public static void ApplyThreadCulturesForCurrentUser() => RouteHelper.ApplyThreadCulturesForUser(SecurityManager.GetCurrentUserId(), ControlExtensions.IsBackend());

    internal static void ApplyCurrentSiteFromUrlReferrer(HttpRequestBase request)
    {
      IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
      if (multisiteContext == null || ControlExtensions.IsBackend() || !(request.UrlReferrer != (Uri) null))
        return;
      string absolutePath = request.UrlReferrer.AbsolutePath;
      multisiteContext.UnresolveUrlAndApplyDetectedSite(absolutePath);
    }

    /// <summary>
    /// Gets the preferred language from the user profile and sets it as Culture and UI Culture
    /// </summary>
    /// <param name="userId">User Id</param>
    /// <param name="isBackendRequest">Whether the request is backend.</param>
    public static void ApplyThreadCulturesForUser(Guid userId, bool isBackendRequest = true)
    {
      if (!isBackendRequest)
        return;
      ResourcesConfig resourcesConfig = Config.Get<ResourcesConfig>();
      CultureElement cultureElement = (CultureElement) null;
      if (userId != Guid.Empty)
      {
        UserManager.UserProfileProxy cachedUserProfile = UserManager.GetCachedUserProfile(userId);
        if (cachedUserProfile != null && !string.IsNullOrEmpty(cachedUserProfile.PreferredLanguage))
          resourcesConfig.BackendCultures.TryGetValue(cachedUserProfile.PreferredLanguage, out cultureElement);
      }
      if (cultureElement == null)
        cultureElement = resourcesConfig.DefaultBackendCulture;
      SystemManager.CurrentContext.Culture = CultureInfo.GetCultureInfo(cultureElement.UICulture);
    }

    /// <summary>Sets the file cache ability.</summary>
    /// <param name="context">The context.</param>
    /// <param name="file">The file.</param>
    /// <returns></returns>
    internal static bool SetFileCacheability(HttpContext context, string file)
    {
      DateTime lastModifiedTime = RouteHelper.GetFileLastModifiedTime(file);
      string s = context.Request.Headers["If-Modified-Since"];
      if (!string.IsNullOrEmpty(s))
      {
        int length = s.IndexOf(";");
        if (length > 0)
          s = s.Substring(0, length);
        DateTime result = DateTime.MaxValue;
        if (DateTime.TryParse(s, out result))
          result = result.ToUniversalTime();
        if (lastModifiedTime.AddSeconds(-1.0) <= result)
        {
          context.Response.StatusCode = 304;
          context.Response.StatusDescription = "Not Modified";
          return false;
        }
      }
      TimeSpan resourcesCacheDuration = Config.Get<AppearanceConfig>().FrontendResourcesCacheDuration;
      if (resourcesCacheDuration == TimeSpan.Zero)
        return true;
      HttpCachePolicy cache = context.Response.Cache;
      cache.SetCacheability(HttpCacheability.Public);
      cache.SetLastModified(lastModifiedTime);
      cache.SetExpires(DateTime.UtcNow.Add(resourcesCacheDuration));
      context.Response.StatusCode = 200;
      context.Response.StatusDescription = "OK";
      return true;
    }

    /// <summary>Sets the cache policy for an external web resource.</summary>
    /// <param name="context">The context.</param>
    /// <param name="file">The external resource file.</param>
    public static void SetWebResourceCache(HttpContext context, string file) => RouteHelper.SetFileCacheability(context, file);

    /// <summary>Checks whether given url is absolute</summary>
    /// <param name="url">Url to validate</param>
    /// <returns>True if the url is absolute, otherwise returns false,</returns>
    public static bool IsAbsoluteUrl(string url)
    {
      if (string.IsNullOrEmpty(url))
        return false;
      url = url.Trim();
      return Uri.TryCreate(url, UriKind.Absolute, out Uri _) || url.StartsWith("//") || url.StartsWith("http:") || url.StartsWith("https:");
    }

    /// <summary>
    /// Checks the passed site map node whether it should be shown in navigation - published, visible etc.
    /// </summary>
    /// <param name="pageSiteMapNode">The sitemap node.</param>
    /// <returns>Whether the node should be shown in navigation.</returns>
    public static bool CheckSiteMapNode(SiteMapNode pageSiteMapNode) => RouteHelper.CheckSiteMapNode(pageSiteMapNode, false);

    /// <summary>
    /// Checks the passed site map node whether it should be shown in navigation - published, visible etc.
    /// </summary>
    /// <param name="pageSiteMapNode">The sitemap node.</param>
    /// <param name="ignoreShowInNavigation">If True, the node will be shown in navigation regardless of its' ShowInNavigation property.</param>
    /// <returns>Whether the node should be shown in navigation.</returns>
    internal static bool CheckSiteMapNode(SiteMapNode pageSiteMapNode, bool ignoreShowInNavigation)
    {
      if (pageSiteMapNode == null)
        throw new ArgumentNullException(nameof (pageSiteMapNode));
      if (!(pageSiteMapNode is PageSiteNode pageSiteNode))
        return true;
      bool flag = ignoreShowInNavigation || pageSiteNode.ShowInNavigation;
      if (string.IsNullOrEmpty(pageSiteNode.Title) && pageSiteNode.Id != SiteInitializer.CurrentFrontendRootNodeId)
        return false;
      if (pageSiteNode.NodeType == NodeType.Group)
        return RouteHelper.CheckGroupSiteMapNode(pageSiteNode);
      if (pageSiteNode.NodeType == NodeType.Standard || pageSiteNode.NodeType == NodeType.External)
        return flag && pageSiteNode.Version > 0 && RouteHelper.CheckSiteNodePublished(pageSiteNode);
      if (pageSiteNode.NodeType == NodeType.InnerRedirect)
      {
        if (SystemManager.CurrentContext.AppSettings.Multilingual && !((IEnumerable<CultureInfo>) pageSiteNode.AvailableLanguages).Contains<CultureInfo>(SystemManager.CurrentContext.Culture))
          return false;
        PageSiteNode terminalNode = pageSiteNode.GetTerminalNode(true);
        return terminalNode != null & flag && RouteHelper.CheckSiteMapNode((SiteMapNode) terminalNode, true);
      }
      if (pageSiteNode.NodeType != NodeType.OuterRedirect || !SystemManager.CurrentContext.AppSettings.Multilingual)
        return flag;
      return flag && ((IEnumerable<CultureInfo>) pageSiteNode.AvailableLanguages).Contains<CultureInfo>(SystemManager.CurrentContext.Culture);
    }

    /// <summary>Checks the site map node culture.</summary>
    /// <param name="siteNode">The site node.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>Whether the node is published.</returns>
    internal static bool CheckSiteNodePublished(PageSiteNode siteNode, CultureInfo culture = null)
    {
      if (siteNode.NodeType != NodeType.Standard && siteNode.NodeType != NodeType.External || siteNode.IsBackend)
        return siteNode.Visible;
      bool retVal = false;
      CultureInfo currentLanguage = culture.GetSitefinityCulture();
      CommonMethods.ExecuteMlLogic<PageSiteNode>((Action<PageSiteNode>) (x => retVal = x.Visible), (Action<PageSiteNode>) (x => retVal = x.PublishedTranslations.Count == 0 && x.Visible || x.PublishedTranslations.Count != 0 && x.PublishedTranslations.Contains(currentLanguage.GetLanguageKeyRaw())), (Action<PageSiteNode>) (x => retVal = x.PublishedTranslations.Contains(currentLanguage.GetLanguageKeyRaw())), siteNode);
      return retVal;
    }

    /// <summary>
    /// Checks the passed site map node whether it meets given criteria for a group node.
    /// </summary>
    /// <param name="node">The sitemap node.</param>
    /// <returns>Whether the node meets given criteria for a group node.</returns>
    public static bool CheckGroupSiteMapNode(PageSiteNode node)
    {
      if (node.NodeType != NodeType.Group || !node.ShowInNavigation)
        return false;
      SiteMapNodeCollection childNodes = node.ChildNodes;
      bool flag = false;
      for (int index = 0; index < childNodes.Count; ++index)
      {
        if (RouteHelper.CheckSiteMapNode(childNodes[index]))
        {
          flag = true;
          break;
        }
      }
      if (!flag)
        return false;
      for (int index = 0; index < childNodes.Count; ++index)
      {
        if (childNodes[index] is PageSiteNode siteNode)
        {
          if (siteNode.NodeType == NodeType.InnerRedirect)
          {
            PageSiteNode terminalNode = siteNode.GetTerminalNode(true);
            if (terminalNode != null)
              siteNode = terminalNode;
            else
              continue;
          }
          if ((siteNode.NodeType == NodeType.Standard || siteNode.NodeType == NodeType.External) && siteNode.ShowInNavigation && siteNode.PageId != Guid.Empty && siteNode.Version > 0 && RouteHelper.CheckSiteNodePublished(siteNode) || siteNode.NodeType == NodeType.OuterRedirect && siteNode.ShowInNavigation)
            return true;
        }
      }
      for (int index = 0; index < childNodes.Count; ++index)
      {
        if (childNodes[index] is PageSiteNode node1 && node1.NodeType == NodeType.Group && RouteHelper.CheckGroupSiteMapNode(node1))
          return true;
      }
      return false;
    }

    public static PageSiteNode GetFirstPageDataNode(
      PageSiteNode node,
      bool ifAccessible)
    {
      return RouteHelper.GetFirstPageDataNode(node, ifAccessible, true);
    }

    internal static PageSiteNode GetFirstPageDataNode(
      PageSiteNode node,
      bool ifAccessible,
      bool drilldown)
    {
      if (node == null)
        throw new ArgumentNullException(nameof (node));
      if (node.NodeType != NodeType.Group)
        return node;
      SiteMapNodeCollection childNodes = ((SiteMapBase) node.Provider).GetChildNodes((SiteMapNode) node, ifAccessible);
      for (int index = 0; index < childNodes.Count; ++index)
      {
        if (childNodes[index] is PageSiteNode siteNode)
        {
          if (siteNode.NodeType == NodeType.InnerRedirect)
          {
            PageSiteNode terminalNode = siteNode.GetTerminalNode(true);
            if (terminalNode != null)
              siteNode = terminalNode;
            else
              continue;
          }
          if ((siteNode.NodeType == NodeType.Standard || siteNode.NodeType == NodeType.External) && siteNode.PageId != Guid.Empty && siteNode.Version > 0 && RouteHelper.CheckSiteNodePublished(siteNode) || siteNode.NodeType == NodeType.OuterRedirect)
            return siteNode;
        }
      }
      foreach (PageSiteNode node1 in childNodes)
      {
        if (node1.NodeType == NodeType.Group & drilldown)
          return RouteHelper.GetFirstPageDataNode(node1, ifAccessible);
      }
      return (PageSiteNode) null;
    }

    /// <summary>
    /// Gets the first page node from the hierarchy below the provided node.
    /// </summary>
    /// <param name="node">The pageNode node.</param>
    /// <param name="ifAccessible">If set to <c>true</c> checks if the child nodes are accessible to the user
    /// making the current request and returns only the accessible ones.</param>
    /// <param name="lang">The language that is requested.</param>
    /// <returns>The page node.</returns>
    public static PageSiteNode GetFirstPageDataNode(
      PageSiteNode node,
      bool ifAccessible,
      string lang)
    {
      if (node == null)
        throw new ArgumentNullException(nameof (node));
      if (node.NodeType != NodeType.Group)
        return node;
      SiteMapNodeCollection childNodes = ((SiteMapBase) node.Provider).GetChildNodes((SiteMapNode) node, ifAccessible);
      PageSiteNode firstPageDataNode = (PageSiteNode) null;
      for (int index = 0; index < childNodes.Count; ++index)
      {
        if (childNodes[index] is PageSiteNode siteNode)
        {
          if (siteNode.NodeType == NodeType.InnerRedirect)
          {
            PageSiteNode terminalNode = siteNode.GetTerminalNode(true);
            if (terminalNode != null)
              siteNode = terminalNode;
            else
              continue;
          }
          if ((siteNode.NodeType == NodeType.Standard || siteNode.NodeType == NodeType.External) && siteNode.PageId != Guid.Empty && siteNode.Version > 0 && RouteHelper.CheckSiteNodePublished(siteNode) || siteNode.NodeType == NodeType.OuterRedirect)
          {
            if (((IEnumerable<CultureInfo>) siteNode.AvailableLanguages).Where<CultureInfo>((Func<CultureInfo, bool>) (l => l.Name == lang)).Count<CultureInfo>() > 0 || siteNode.UiCulture == null && siteNode.LocalizationStrategy == LocalizationStrategy.Synced)
              return siteNode;
            if (firstPageDataNode == null)
              firstPageDataNode = siteNode;
          }
        }
      }
      if (firstPageDataNode != null)
        return firstPageDataNode;
      foreach (PageSiteNode node1 in childNodes)
      {
        if (node1.NodeType == NodeType.Group)
          return RouteHelper.GetFirstPageDataNode(node1, ifAccessible, lang);
      }
      return (PageSiteNode) null;
    }

    /// <summary>Gets the string content with UTF-8 preamble.</summary>
    /// <param name="content">The string content.</param>
    /// <returns>The content.</returns>
    public static byte[] GetContentWithPreamble(string content) => RouteHelper.GetContentWithPreamble(content, Encoding.UTF8);

    /// <summary>
    /// Gets the string content with the preamble of the specified encoding.
    /// </summary>
    /// <param name="content">The string content.</param>
    /// <param name="encoding">The encoding with the preamble.</param>
    /// <returns>The content.</returns>
    public static byte[] GetContentWithPreamble(string content, Encoding encoding)
    {
      byte[] bytes = encoding.GetBytes(content);
      byte[] preamble = encoding.GetPreamble();
      int length1 = bytes.Length;
      int length2 = preamble.Length;
      byte[] destinationArray = new byte[length1 + length2];
      Array.Copy((Array) preamble, (Array) destinationArray, length2);
      Array.Copy((Array) bytes, 0, (Array) destinationArray, length2, length1);
      return destinationArray;
    }

    /// <summary>
    /// Gets the content which resides on the given web address.
    /// </summary>
    /// <param name="webAddress">The web address.</param>
    /// <returns>The content.</returns>
    public static string GetWebContent(string webAddress)
    {
      string webContent = string.Empty;
      try
      {
        HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(webAddress);
        httpWebRequest.Timeout = 120000;
        using (HttpWebResponse response = (HttpWebResponse) httpWebRequest.GetResponse())
        {
          using (StreamReader streamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            webContent = streamReader.ReadToEnd();
        }
        return webContent;
      }
      catch
      {
        return string.Empty;
      }
    }

    /// <summary>Sets whether the URL parameters are resolved.</summary>
    /// <param name="resolved">if set to <c>true</c> marks that the URL parameters are resolved.</param>
    public static void SetUrlParametersResolved(bool resolved = true)
    {
      if (SystemManager.CurrentHttpContext == null)
        return;
      SystemManager.CurrentHttpContext.Items[RouteHelper.UrlParamtersResolvedKey] = (object) resolved;
    }

    /// <summary>Gets whether the URL parameters are resolved.</summary>
    /// <returns>Whether the URL parameters are resolved.</returns>
    public static bool GetUrlParametersResolved()
    {
      if (SystemManager.CurrentHttpContext == null)
        return true;
      IDictionary items = SystemManager.CurrentHttpContext.Items;
      bool parametersResolved = true;
      if (items.Contains(RouteHelper.UrlParamtersResolvedKey))
        parametersResolved = (bool) items[RouteHelper.UrlParamtersResolvedKey];
      return parametersResolved;
    }

    /// <summary>Gets the URL parameter string.</summary>
    /// <param name="urlParameters">The URL parameters.</param>
    /// <param name="excludePrefixedParams">if set to <c>true</c> excludes parameters prefixed with '!'.</param>
    /// <returns>URL parameters combined with '/' separator. The string starts with '/'.</returns>
    public static string GetUrlParameterString(string[] urlParameters, bool excludePrefixedParams = true) => urlParameters == null ? (string) null : "/" + string.Join("/", excludePrefixedParams ? ((IEnumerable<string>) urlParameters).Where<string>((Func<string, bool>) (p => !p.StartsWith("!"))) : (IEnumerable<string>) urlParameters);

    internal static void RedirectPermanent(
      HttpContextBase httpContext,
      string url,
      bool endResponse = false)
    {
      httpContext.Response.Cache.SetMaxAge(TimeSpan.FromSeconds((double) Config.Get<PagesConfig>().RedirectCacheExpired));
      httpContext.Response.RedirectPermanent(url, endResponse);
    }

    /// <summary>Resolves the node id from specified url</summary>
    /// <param name="url">The url</param>
    /// <returns>Node id as Guid</returns>
    internal static Guid ResolveNodeId(string url)
    {
      Guid nodeId = Guid.Empty;
      if (!string.IsNullOrEmpty(url) && !url.StartsWith("javascript:", StringComparison.InvariantCultureIgnoreCase) && url.Contains("["))
        RouteHelper.ReferenceRegex.Replace(url, RouteHelper.ReferenceMatchEvaluator((RouteHelper.ReferenceEvaluator) ((type, value, options) =>
        {
          string empty = string.Empty;
          if (!(type.ToLower() == "node"))
            throw new ArgumentException("Unsupported URL reference type:" + type);
          string input = value;
          nodeId = Guid.Parse(input);
          return input;
        })), 1);
      return nodeId;
    }

    /// <summary>Sets the SSL offloading.</summary>
    /// <param name="context">The context.</param>
    internal static void SetSslOffloadingFlag(HttpContext context)
    {
      HttpContext current = HttpContext.Current;
      if (current == null || current.Request == null)
        return;
      SslOffloadingElement offloadingSettings = Config.Get<SystemConfig>().SslOffloadingSettings;
      if (!offloadingSettings.EnableSslOffloading)
        return;
      string httpHeaderFieldName = offloadingSettings.HttpHeaderFieldName;
      if (!current.Request.Headers.Keys.Contains(httpHeaderFieldName))
        return;
      context.Items.Add((object) httpHeaderFieldName, (object) current.Request.Headers[httpHeaderFieldName]);
    }

    /// <summary>Gets the file last modified time in UTC.</summary>
    /// <param name="file">The file.</param>
    /// <returns>The last modified time.</returns>
    private static DateTime GetFileLastModifiedTime(string file) => System.IO.File.GetLastWriteTime(new Uri(file).LocalPath).ToUniversalTime();

    /// <summary>Gets the path.</summary>
    /// <param name="handler">The handler.</param>
    /// <param name="values">The values.</param>
    /// <returns>The path.</returns>
    private static string GetPath(IPartialRouteHandler handler, RouteValueDictionary values)
    {
      if (handler.PartialRequestContext != null && handler.PartialRequestContext.RouteData != null && handler.PartialRequestContext.RouteData.Route != null)
      {
        VirtualPathData virtualPath = handler.PartialRequestContext.RouteData.Route.GetVirtualPath((RequestContext) handler.PartialRequestContext, values);
        if (virtualPath != null)
          return virtualPath.VirtualPath;
      }
      return string.Empty;
    }

    /// <summary>References the match evaluator.</summary>
    /// <param name="evaluator">The evaluator.</param>
    /// <returns>The match evaluator.</returns>
    private static MatchEvaluator ReferenceMatchEvaluator(
      RouteHelper.ReferenceEvaluator evaluator)
    {
      return (MatchEvaluator) (m => evaluator(m.Groups["type"].Value, m.Groups["value"].Value, m.Groups["options"].Value));
    }

    /// <summary>Compiles the reference regex.</summary>
    /// <param name="nodeTypes">The node types.</param>
    /// <returns>The regex.</returns>
    private static Regex CompileReferenceRegex(string[] nodeTypes) => new Regex("\\[(?<type>" + string.Join("|", nodeTypes) + "):(?<value>[^/\\]]+)(?:/(?<options>[^\\]]+))?\\]", RegexOptions.IgnoreCase | RegexOptions.Compiled);

    /// <summary>
    /// Gets the location of the frontend login page depending on the backend settings.
    /// </summary>
    /// <param name="httpContext">The current HTTP context.</param>
    /// <param name="redirectStrategy">The redirect strategy.</param>
    /// <param name="siteMap">The site map.</param>
    /// <returns>The location of the frontend login page.</returns>
    public static string GetFrontEndLogin(
      HttpContextBase httpContext,
      out RedirectStrategyType redirectStrategy,
      SiteMapProvider siteMap = null)
    {
      redirectStrategy = RedirectStrategyType.None;
      siteMap = siteMap ?? SiteMapBase.GetCurrentProvider();
      string redirectPageUrl = string.Empty;
      ISite currentSite = SystemManager.CurrentContext.CurrentSite;
      if (currentSite.FrontEndLoginPageId != Guid.Empty)
        redirectPageUrl = RouteHelper.GetLoginPageUrl(siteMap);
      else if (!string.IsNullOrWhiteSpace(currentSite.FrontEndLoginPageUrl))
        redirectPageUrl = currentSite.FrontEndLoginPageUrl.StartsWith("~/") || currentSite.FrontEndLoginPageUrl.Contains("://") || currentSite.FrontEndLoginPageUrl.StartsWith("/") ? currentSite.FrontEndLoginPageUrl : string.Format("{0}{1}", (object) "~/", (object) currentSite.FrontEndLoginPageUrl);
      string redirectUrl = string.Empty;
      if (!string.IsNullOrWhiteSpace(redirectPageUrl))
      {
        redirectUrl = RouteHelper.AppendReturnUrl(httpContext, redirectPageUrl, redirectUrl);
        redirectStrategy = RedirectStrategyType.Site;
      }
      return redirectUrl;
    }

    private static string GetLoginPageUrl(SiteMapProvider siteMap)
    {
      ISite currentSite = SystemManager.CurrentContext.CurrentSite;
      if (!(siteMap.FindSiteMapNodeFromKey(currentSite.FrontEndLoginPageId.ToString()) is PageSiteNode siteMapNodeFromKey))
        return string.Empty;
      if (!(SystemManager.CurrentHttpContext.Items[(object) "Culture"] is CultureInfo culture))
        culture = SystemManager.CurrentContext.Culture;
      string urlForCulture1 = RouteHelper.GetUrlForCulture(culture, siteMapNodeFromKey);
      if (!string.IsNullOrEmpty(urlForCulture1))
        return urlForCulture1;
      string urlForCulture2 = RouteHelper.GetUrlForCulture(currentSite.DefaultCulture, siteMapNodeFromKey);
      if (!string.IsNullOrEmpty(urlForCulture2))
        return urlForCulture2;
      foreach (CultureInfo availableLanguage in siteMapNodeFromKey.AvailableLanguages)
      {
        string urlForCulture3 = RouteHelper.GetUrlForCulture(availableLanguage, siteMapNodeFromKey);
        if (!string.IsNullOrEmpty(urlForCulture3))
          return urlForCulture3;
      }
      return string.Empty;
    }

    private static string GetUrlForCulture(CultureInfo culture, PageSiteNode page)
    {
      if (!page.IsPublished(culture))
        return string.Empty;
      using (new CultureRegion(culture))
        return page.Url;
    }

    private static string AppendReturnUrl(
      HttpContextBase httpContext,
      string redirectPageUrl,
      string redirectUrl)
    {
      if (!string.IsNullOrWhiteSpace(redirectPageUrl))
      {
        string str1 = "?";
        if (redirectPageUrl.Contains(str1))
          str1 = "&";
        string str2 = httpContext.Request.Url.AbsoluteUri;
        if (UrlPath.IsSslOffloaded(httpContext))
          str2 = str2.Replace("http", "https");
        redirectUrl = string.Format("{0}{1}{2}={3}", (object) redirectPageUrl, (object) str1, (object) SecurityManager.AuthenticationReturnUrl, (object) HttpUtility.UrlEncode(str2));
      }
      return redirectUrl;
    }

    private delegate string ReferenceEvaluator(string type, string value, string options);

    /// <summary>
    /// This class contains constants with the page action names.
    /// </summary>
    public static class ActionKeys
    {
      /// <summary>"Edit" action.</summary>
      public const string Edit = "Edit";
      /// <summary>"Preview" action.</summary>
      public const string Preview = "Preview";
      /// <summary>"MobilePreview" action.</summary>
      public const string MobilePreview = "MobilePreview";
      /// <summary>"InEdit" action.</summary>
      public const string InEdit = "InEdit";
    }

    internal static class QueryConstants
    {
      internal const string ActionKey = "sfaction";
      internal const string VersionKey = "sfversion";
    }
  }
}
