// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.PageRouteHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Abstractions.VirtualPath;
using Telerik.Sitefinity.Cloud.WindowsAzure;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Licensing;
using Telerik.Sitefinity.Localization.UrlLocalizationStrategies;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.OutputCache;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Events;
using Telerik.Sitefinity.Web.Compilation;
using Telerik.Sitefinity.Web.Events;
using Telerik.Sitefinity.Web.OutputCache;
using Telerik.Sitefinity.Web.OutputCache.Data;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Web
{
  /// <summary>Route handler for Sitefinity pages.</summary>
  public class PageRouteHandler : IRouteHandler
  {
    internal const string OutputCacheStatusKey = "OutputCacheStatus";
    internal const string CacheVariationKeyContextItem = "sf_output_cache_variation_key";
    internal const int maxTrialRequests = 20;
    private static Dictionary<string, int> requests;
    private static string[] cacheDependencyContextKeys = new string[3]
    {
      "PageDataCacheDependencyName",
      "PageNodesCacheDependencyName",
      "DataItemTypeCacheDependencyName"
    };
    /// <summary>The HttpContext data page id.</summary>
    public static readonly string HttpContextDataPageId = "sf_dataPageId";
    /// <summary>The HttpContext secured controls.</summary>
    public static readonly string HttpContextSecuredControls = "sf_securedConrols";
    /// <summary>Add cache dependencies string.</summary>
    public static readonly string AddCacheDependencies = nameof (AddCacheDependencies);
    /// <summary>The registered cache variations.</summary>
    public static readonly string RegisteredCacheVariations = "sf_regsitered_cache_var";
    /// <summary>The output cache page site node.</summary>
    public static readonly string OutputCachedPageSiteNode = "sf_outputCachedPageSiteNode";
    private const string NoCacheVaryHeaderValue = "sf_no_cache";
    private const string SiteAndLanguageResolvedContextKey = "sf_site_language_resolved";
    private static readonly IDictionary<string, IList<ICustomOutputCacheVariation>> CacheVariationsCache = (IDictionary<string, IList<ICustomOutputCacheVariation>>) new Dictionary<string, IList<ICustomOutputCacheVariation>>();
    private static readonly FieldInfo CacheUtcTimestampRequestField = typeof (HttpCachePolicy).GetField("_utcTimestampRequest", BindingFlags.Instance | BindingFlags.NonPublic);
    private static readonly PageRouteHandler.CacheDependencyPersistanceMode cacheDependencyPersistanceMode = SitefinityOutputCacheModule.Initialized ? PageRouteHandler.CacheDependencyPersistanceMode.Database : PageRouteHandler.CacheDependencyPersistanceMode.InMemory;
    private const string SitefinityTrialModeMessage = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"><html xmlns=\"http://www.w3.org/1999/xhtml\"><head><meta http-equiv=\"content-type\" content=\"text/html; charset=utf-8\" /><title>Sitefinity trial version</title></head><body><div style='font-family: Arial; font-size: 14px; color: #666; position: absolute; top: 50%; left: 50%; width: 680px; height: 50px; margin-left: -340px; margin-top: -25px; text-align: center;'><h1 style='font-size: 30px; color: #999; font-weight: normal; margin: 0; padding: 0;'>You are running a trial version of <a title='Sitefinity CMS' href='https://www.progress.com/sitefinity-cms' style='border-bottom: 1px dotted #ccc; color: #999; text-decoration: none;'>Sitefinity {1}</a>.</h1><p style='margin:0; padding: 0;'>To proceed with your evaluation, press <a href='{0}'>Continue</a> or refresh the page.</p></div></body></html>";
    private static bool loadedCacheSettings = false;
    private static bool waitForPageOutputCacheToFill;
    private static bool? resourceHttpModuleLoaded;

    static PageRouteHandler()
    {
      string appSetting = ConfigurationManager.AppSettings["sf:PageOutputCacheDependencyPersistance"];
      PageRouteHandler.CacheDependencyPersistanceMode result;
      if (appSetting.IsNullOrEmpty() || !Enum.TryParse<PageRouteHandler.CacheDependencyPersistanceMode>(appSetting, out result))
        return;
      PageRouteHandler.cacheDependencyPersistanceMode = result;
    }

    /// <summary>Provides the object that processes the request.</summary>
    /// <param name="requestContext">An object that encapsulates information about the request.</param>
    /// <returns>An object that processes the request.</returns>
    public IHttpHandler GetHttpHandler(RequestContext requestContext)
    {
      PageRouteHandler.ProcessRequestLicensing(requestContext.HttpContext);
      return this.BuildHttpHandler(requestContext);
    }

    /// <summary>
    /// Builds the HTTP handler. This method can be overridden to provide additional logic over the base Sitefinity route handler logic
    /// </summary>
    /// <param name="requestContext">The request context.</param>
    /// <returns>The HTTP handler.</returns>
    protected virtual IHttpHandler BuildHttpHandler(RequestContext requestContext)
    {
      RouteData routeData = requestContext.RouteData;
      if (!(routeData.DataTokens["SiteMapNode"] is PageSiteNode dataToken))
        throw new ArgumentException("Invalid request for this route handler. Route data must contain SiteMapNode.");
      if (routeData.Values.ContainsKey("Params"))
        RouteHelper.SetUrlParametersResolved(dataToken.IsBackend || Config.Get<PagesConfig>().EnableBackwardCompatabilityForPagesUrls);
      this.ProcessSiteNode(dataToken);
      PageSiteNode pageDataNode = RouteHelper.GetFirstPageDataNode(dataToken, true);
      SystemManager.CurrentHttpContext.Items[(object) PageRouteHandler.HttpContextDataPageId] = pageDataNode != null ? (object) pageDataNode.PageId : throw new InvalidOperationException("Invalid SiteMap node specified. Either the current group node doesn't have child nodes or the current user does not have rights to view any of the child nodes.");
      foreach (ICustomOutputCacheVariationInitializer variationInitializer in ObjectFactory.Container.ResolveAll(typeof (ICustomOutputCacheVariationInitializer)).Cast<ICustomOutputCacheVariationInitializer>())
        variationInitializer.RegisterCustomOutputCacheVariations(pageDataNode);
      if (pageDataNode.HasSecuredWidgets)
        requestContext.HttpContext.Items.Add((object) PageRouteHandler.HttpContextSecuredControls, (object) true);
      string theme = (string) null;
      Page page1 = PageRouteHandler.InstantiateHandler(requestContext, pageDataNode, ref theme);
      page1.Items[(object) "SF_PageUrlEvaluationMode"] = (object) pageDataNode.UrlEvaluationMode;
      page1.Init += (EventHandler) ((sender, args) =>
      {
        Page page2 = (Page) sender;
        this.ProcessRequiredControls(page2, pageDataNode);
        if (pageDataNode.IsBackend)
          return;
        this.ProcessSeoAndOpenGraphControls(page2, pageDataNode);
      });
      page1.Load += new EventHandler(this.handler_Load);
      page1.PreRenderComplete += new EventHandler(PageRouteHandler.handler_PreRenderComplete);
      page1.Error += new EventHandler(this.handler_Error);
      if (ThemeController.IsAspNetTheme(theme, page1))
      {
        page1.Theme = theme;
        page1.Items[(object) "theme"] = (object) theme;
      }
      else
        page1.Items[(object) "theme"] = (object) theme;
      if (AppSettings.CurrentSettings.CombineScripts() || bool.TrueString.Equals(pageDataNode.Attributes["IncludeScriptManager"], StringComparison.OrdinalIgnoreCase))
        RouteHandler.EnsureScriptManager(page1);
      RouteHandler.EnsureStyleSheetManager(page1);
      RouteHandler.EnsureSitefinityResourceManager(page1);
      this.InitOutputCache(requestContext, pageDataNode);
      page1.Unload += new EventHandler(this.handler_Unload);
      return SitefinityHttpModule.WaitForPageOutputCacheToFill && !pageDataNode.IsBackend ? (IHttpHandler) new PageHandlerWrapper((IHttpHandler) page1) : (IHttpHandler) page1;
    }

    internal static Page InstantiateHandler(
      RequestContext requestContext,
      PageSiteNode pageDataNode,
      ref string theme)
    {
      if (theme.IsNullOrEmpty())
        theme = ThemeController.GetCurrentTheme(requestContext.HttpContext, pageDataNode.Theme, pageDataNode.IsBackend);
      string virtualPath = PageRouteHandler.BuildVirtualPath(pageDataNode, theme, pageDataNode.IsMultilingual, requestContext.RouteData);
      Page instance = (Page) null;
      if (!PageAssemblyLoader.TryLoadFromAssembly(virtualPath, pageDataNode, out instance))
        instance = CompilationHelpers.LoadControl<Page>(virtualPath, true, pageDataNode);
      return instance;
    }

    internal static Page InstantiateHandler(
      RequestContext requestContext,
      PageSiteNode pageDataNode)
    {
      string theme = (string) null;
      return PageRouteHandler.InstantiateHandler(requestContext, pageDataNode, ref theme);
    }

    /// <summary>
    /// Builds the HTTP handler virtual path. This method can be overridden to provide different logic of the Sitefinity virtual path build process.
    /// </summary>
    /// <param name="requestContext">The request context.</param>
    /// <param name="pageDataNode">The page data node.</param>
    /// <param name="theme">The theme.</param>
    /// <returns>A string representing the HTTP handler virtual path.</returns>
    protected virtual string BuildHttpHandlerVirtualPath(
      RequestContext requestContext,
      PageSiteNode pageDataNode,
      string theme)
    {
      return PageRouteHandler.BuildVirtualPath(pageDataNode, theme, pageDataNode.IsMultilingual, requestContext.RouteData);
    }

    /// <summary>Builds the HTTP handler virtual path.</summary>
    /// <param name="pageDataNode">The page data node.</param>
    /// <param name="theme">The theme.</param>
    /// <param name="isMultilingual">A value indicating whether is multilingual.</param>
    /// <param name="routeData">The route data</param>
    /// <returns>A string representing the virtual path.</returns>
    public static string BuildVirtualPath(
      PageSiteNode pageDataNode,
      string theme,
      bool isMultilingual,
      RouteData routeData)
    {
      if (pageDataNode == null)
        throw new ArgumentNullException(nameof (pageDataNode));
      StringBuilder stringBuilder = new StringBuilder();
      if (pageDataNode.Framework == PageTemplateFramework.Mvc)
        stringBuilder.Append("~/SFMVCPageService/");
      else
        stringBuilder.Append("~/SFPageService/");
      string str = theme != null ? theme.Replace(" ", "_") : string.Empty;
      stringBuilder.Append(pageDataNode.Key).Append("_").Append(str);
      if (!pageDataNode.IsBackend)
      {
        string variationType;
        string variationKey = pageDataNode.GetVariationKey(out variationType);
        if (!string.IsNullOrEmpty(variationKey))
        {
          stringBuilder.Append("_");
          stringBuilder.Append(variationType);
          stringBuilder.Append(variationKey);
        }
      }
      if (isMultilingual)
        stringBuilder.Append("_").Append(SystemManager.CurrentContext.Culture.Name);
      stringBuilder.Append(".aspx");
      return stringBuilder.ToString();
    }

    private static void RaisePagePreRenderCompleteEvent(
      Page page,
      PageSiteNode pageSiteNode,
      string origin)
    {
      EventHub.Raise((IEvent) new PagePreRenderCompleteEvent(page, pageSiteNode, origin));
    }

    public static void AddRequiredControls(Page page, PageSiteNode data)
    {
      page.Controls.AddAt(0, (Control) new ScriptManagerWrapper()
      {
        PageId = data.PageId,
        PageVersion = data.Version,
        PageStatus = data.Status,
        Published = data.Visible
      });
      string attribute = data.Attributes["HeadTagContent"];
      if (!string.IsNullOrEmpty(attribute))
        page.Header.Controls.Add((Control) new LiteralControl(attribute));
      if (data.Crawlable)
        return;
      page.Header.Controls.Add((Control) new HtmlMeta()
      {
        Name = "robots",
        Content = "noindex"
      });
    }

    /// <summary>Add the additional controls</summary>
    /// <param name="page">The page.</param>
    /// <param name="data">The data.</param>
    public virtual void ProcessRequiredControls(Page page, PageSiteNode data) => PageRouteHandler.AddRequiredControls(page, data);

    /// <summary>Add SEO and OpenGraph controls</summary>
    /// <param name="page">The page.</param>
    /// <param name="data">The data.</param>
    private void ProcessSeoAndOpenGraphControls(Page page, PageSiteNode data) => PageHelper.AddSeoAndOpenGraphControls(page, data);

    public virtual void RaisePagePreRenderCompleteEvent(Page page, PageSiteNode pageSiteNode)
    {
      string name = this.GetType().Name;
      PageRouteHandler.RaisePagePreRenderCompleteEvent(page, pageSiteNode, name);
    }

    private void handler_Load(object sender, EventArgs e)
    {
      Page page = (Page) sender;
      if (!page.Items.Contains((object) "theme"))
        return;
      ResourceLinks globalStyles = ThemeController.GetGlobalStyles(page);
      if (globalStyles == null)
        return;
      page.Controls.Add((Control) globalStyles);
    }

    protected internal virtual void SetPageCacheDependencies()
    {
      HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
      if (currentHttpContext == null)
        return;
      object obj1 = currentHttpContext.Items[(object) PageRouteHandler.HttpContextDataPageId];
      object obj2 = currentHttpContext.Items[(object) PageRouteHandler.AddCacheDependencies];
      if (obj1 == null || obj2 == null || !(obj2.ToString().ToLower() == "true"))
        return;
      bool ignoreCache = false;
      object obj3;
      if (SystemManager.IsDetailsView(out obj3) && obj3 is ISecuredObject securedObject && !securedObject.IsAccessibleToEveryone())
        ignoreCache = true;
      if (!ignoreCache)
        this.HandleCustomOutputCacheVariations(out ignoreCache, currentHttpContext);
      if (ignoreCache)
      {
        currentHttpContext.Response.DisableKernelCache();
        currentHttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
      }
      else
      {
        HttpContext current = HttpContext.Current;
        if (current != null)
        {
          HttpCachePolicy cache = current.Response.Cache;
          switch (cache.GetCacheability())
          {
            case HttpCacheability.Public:
            case HttpCacheability.ServerAndPrivate:
              bool flag = false;
              string cachedItemKey = new OutputCacheHelper(current).GenerateCachedItemKey();
              if (!string.IsNullOrEmpty(cachedItemKey))
              {
                OutputCacheItemProxy outputCacheItemProxy = OutputCacheWorker.GetOutputCacheItemProxy(cachedItemKey);
                if (outputCacheItemProxy != null && !string.IsNullOrEmpty(outputCacheItemProxy.ETag) && outputCacheItemProxy.Status == OutputCacheItemStatus.Live)
                {
                  cache.SetETag(outputCacheItemProxy.ETag);
                  cache.SetLastModified(outputCacheItemProxy.DateModified);
                  flag = true;
                }
              }
              if (!flag)
              {
                cache.SetETag("\"" + Guid.NewGuid().ToString() + "\"");
                cache.SetLastModified(DateTime.UtcNow);
                break;
              }
              break;
          }
        }
        PageSiteNode currentNode = (PageSiteNode) ((SiteMapProvider) currentHttpContext.Items[(object) "SF_SiteMap"]).CurrentNode;
        List<CacheDependencyKey> cacheDependencyKeyList1 = new List<CacheDependencyKey>();
        foreach (string dependencyContextKey in PageRouteHandler.cacheDependencyContextKeys)
        {
          IEnumerable<CacheDependencyKey> dependencyObjects = this.GetCacheDependencyObjects(dependencyContextKey);
          if (dependencyObjects != null)
            cacheDependencyKeyList1.AddRange(dependencyObjects);
        }
        CacheDependencyKey cacheDependencyKey1;
        if (!Config.Get<PagesConfig>().DisableInvalidateOutputCacheOnPermissionsChange)
        {
          List<CacheDependencyKey> cacheDependencyKeyList2 = cacheDependencyKeyList1;
          cacheDependencyKey1 = new CacheDependencyKey();
          cacheDependencyKey1.Type = typeof (Telerik.Sitefinity.Security.Model.Permission);
          CacheDependencyKey cacheDependencyKey2 = cacheDependencyKey1;
          cacheDependencyKeyList2.Add(cacheDependencyKey2);
        }
        if (currentNode != null)
        {
          cacheDependencyKeyList1.AddRange((IEnumerable<CacheDependencyKey>) currentNode.GetCacheDependencyObjects());
          List<CacheDependencyKey> cacheDependencyKeyList3 = cacheDependencyKeyList1;
          cacheDependencyKey1 = new CacheDependencyKey();
          cacheDependencyKey1.Type = typeof (CacheDependencyPageNodeUrl);
          cacheDependencyKey1.Key = currentNode.Key;
          CacheDependencyKey cacheDependencyKey3 = cacheDependencyKey1;
          cacheDependencyKeyList3.Add(cacheDependencyKey3);
          currentHttpContext.Items[(object) PageRouteHandler.OutputCachedPageSiteNode] = (object) currentNode;
        }
        if (PageRouteHandler.cacheDependencyPersistanceMode == PageRouteHandler.CacheDependencyPersistanceMode.InMemory)
        {
          CompositeCacheDependency compositeCacheDependency = new CompositeCacheDependency(obj1.ToString());
          compositeCacheDependency.AddCacheDependencyKeys((IList<CacheDependencyKey>) cacheDependencyKeyList1);
          currentHttpContext.Response.AddCacheDependency((System.Web.Caching.CacheDependency) compositeCacheDependency);
        }
        else
        {
          List<CacheDependencyKey> cacheDependencyKeyList4 = cacheDependencyKeyList1;
          cacheDependencyKey1 = new CacheDependencyKey();
          cacheDependencyKey1.Type = CompositeCacheDependency.CacheDependencyType;
          cacheDependencyKey1.Key = obj1.ToString();
          CacheDependencyKey cacheDependencyKey4 = cacheDependencyKey1;
          cacheDependencyKeyList4.Insert(0, cacheDependencyKey4);
          SitefinityOutputCacheProvider.StoreCacheDependenciesInContext((IList<CacheDependencyKey>) cacheDependencyKeyList1.Distinct<CacheDependencyKey>().ToList<CacheDependencyKey>());
        }
      }
    }

    [Obsolete("Use GetCacheDependencyObjects method instead!")]
    protected virtual void SubscribeDependencyObjects(
      CompositeCacheDependency cacheDependency,
      string key)
    {
      IEnumerable<CacheDependencyKey> dependencyObjects = this.GetCacheDependencyObjects(key);
      if (dependencyObjects == null)
        return;
      foreach (CacheDependencyKey cacheDependencyKey in dependencyObjects)
        cacheDependency.AddCacheDependencyKey(cacheDependencyKey.Type, cacheDependencyKey.Key);
    }

    protected virtual IEnumerable<CacheDependencyKey> GetCacheDependencyObjects(
      string key)
    {
      return SystemManager.CurrentHttpContext.Items.Contains((object) key) ? (IEnumerable<CacheDependencyKey>) SystemManager.CurrentHttpContext.Items[(object) key] : (IEnumerable<CacheDependencyKey>) null;
    }

    private void handler_Unload(object sender, EventArgs e) => this.SetPageCacheDependencies();

    private void handler_Error(object sender, EventArgs e)
    {
    }

    private static void handler_PreRenderComplete(object sender, EventArgs e)
    {
      Page page = (Page) sender;
      PageSiteNode dataToken = (PageSiteNode) page.Request.RequestContext.RouteData.DataTokens["SiteMapNode"];
      if (!RouteHelper.GetUrlParametersResolved() && !dataToken.IsBackend)
      {
        page.Response.StatusCode = 404;
        page.Response.End();
      }
      Type type = typeof (PageRouteHandler);
      page.Header.Controls.Add((Control) new HtmlMeta()
      {
        Name = "Generator",
        Content = ("Sitefinity " + (object) type.Assembly.GetName().Version + " " + LicenseState.Current.LicenseInfo.LicenseType + (LicenseState.Current.LicenseInfo.IsTrial ? (object) " Trial version" : (object) string.Empty))
      });
      if (dataToken.IsMultilingual)
      {
        HtmlControl htmlTag = PageRouteHandler.FindHtmlTag(page);
        if (htmlTag != null)
        {
          string name = SystemManager.CurrentContext.Culture.Name;
          htmlTag.Attributes.Add("lang", name);
          if (htmlTag.Attributes["xmlns"] != null)
            htmlTag.Attributes.Add("xml:lang", name);
        }
      }
      page.SetCanonicalLinks(dataToken);
      if (Config.SectionHandler.Testing.Enabled)
      {
        string empty = string.Empty;
        string str = !AzureRuntime.IsRunning ? Environment.MachineName + "-" + HostingEnvironment.SiteName : AzureRuntime.CurrentRoleInstance.Id;
        if (!string.IsNullOrWhiteSpace(str))
        {
          if (str.Length > 20)
            str = ".." + str.Substring(str.Length - 20);
          page.Title = page.Title + " - " + str;
        }
      }
      PageRouteHandler.RaisePagePreRenderCompleteEvent(page, dataToken, typeof (PageRouteHandler).Name);
    }

    protected internal virtual void InitOutputCache(
      RequestContext requestContext,
      PageSiteNode siteNode)
    {
      if (Config.Get<SystemConfig>().CacheSettings.EnableOutputCache && !PageRouteHandler.IgnoreOutputCache(SystemManager.CurrentHttpContext, siteNode))
      {
        OutputCacheProfileElement outputCacheProfile = siteNode.GetOutputCacheProfile();
        if (outputCacheProfile != null && outputCacheProfile.Enabled && this.ApplyServerCache(requestContext.HttpContext, outputCacheProfile, siteNode))
        {
          requestContext.HttpContext.Response.Cache.AddValidationCallback(new HttpCacheValidateHandler(PageRouteHandler.ValidateCacheOutput), (object) new PageSiteNodeResolver(siteNode));
          requestContext.HttpContext.Items[(object) PageRouteHandler.AddCacheDependencies] = (object) true;
          Log.Write((object) string.Format("Page with id {0} was added to cache.", (object) siteNode.PageId.ToString().ToUpperInvariant()), ConfigurationPolicy.TestTracing);
          return;
        }
      }
      this.OutputCacheStrategy.ApplyNoCache(requestContext.HttpContext, siteNode);
    }

    /// <summary>
    /// Applies the server cache for the response of the specified context.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="profile">The profile.</param>
    /// <param name="siteNode">The site node.</param>
    /// <returns>Returns true if successful</returns>
    protected internal virtual bool ApplyServerCache(
      HttpContextBase context,
      OutputCacheProfileElement profile,
      PageSiteNode siteNode)
    {
      return this.OutputCacheStrategy.ApplyServerCache(context, (IOutputCacheProfile) profile, siteNode);
    }

    internal PageOutputCacheStrategy OutputCacheStrategy => ObjectFactory.Resolve<PageOutputCacheStrategy>();

    /// <summary>
    /// Registers output cache variation by user authenticated
    /// </summary>
    public static void RegisterUserAuthenticatedOutputCacheVariation() => PageRouteHandler.RegisterCustomOutputCacheVariation((ICustomOutputCacheVariation) new UserAuthenticatedOutputCacheVariation(), SystemManager.CurrentHttpContext);

    /// <summary>Registers output cache variation</summary>
    /// <param name="contentType">The output cache variation</param>
    public static void RegisterContentListCacheVariation(Type contentType, string providerName)
    {
      IManager manager;
      if (!contentType.ImplementsInterface(typeof (ISecuredObject)) || !ManagerBase.TryGetMappedManager(contentType, providerName, out manager) || !manager.Provider.FilterQueriesByViewPermissions)
        return;
      PageRouteHandler.RegisterCustomOutputCacheVariation((ICustomOutputCacheVariation) new AnonymousUsersOutputCacheVariation(), SystemManager.CurrentHttpContext);
    }

    /// <summary>Registers custom output cache variation</summary>
    /// <param name="cacheVariation">The output cache variation</param>
    public static void RegisterCustomOutputCacheVariation(ICustomOutputCacheVariation cacheVariation) => PageRouteHandler.RegisterCustomOutputCacheVariation(cacheVariation, SystemManager.CurrentHttpContext);

    internal static void RegisterCustomOutputCacheVariation(
      ICustomOutputCacheVariation cacheVariation,
      HttpContextBase context)
    {
      if (cacheVariation == null)
        throw new ArgumentNullException(nameof (cacheVariation));
      if (!cacheVariation.GetType().IsSerializable)
        throw new ArgumentException("The cache variation object should be serializable.", nameof (cacheVariation));
      if (string.IsNullOrEmpty(cacheVariation.Key))
        throw new InvalidOperationException("The Key property of the cache variation cannot be empty.");
      PageRouteHandler.CustomOutputCacheVariationsRegistry variationsRegistry = PageRouteHandler.GetCustomOutputCacheVariations(context.Items);
      if (variationsRegistry == null)
      {
        variationsRegistry = new PageRouteHandler.CustomOutputCacheVariationsRegistry();
        context.Items[(object) PageRouteHandler.RegisteredCacheVariations] = (object) variationsRegistry;
      }
      variationsRegistry.AddVariation(cacheVariation);
      variationsRegistry.Validated = true;
    }

    internal static PageRouteHandler.CustomOutputCacheVariationsRegistry GetCustomOutputCacheVariations(
      IDictionary items)
    {
      return items[(object) PageRouteHandler.RegisteredCacheVariations] as PageRouteHandler.CustomOutputCacheVariationsRegistry;
    }

    private void HandleCustomOutputCacheVariations(out bool ignoreCache, HttpContextBase context = null)
    {
      if (context == null)
        context = SystemManager.CurrentHttpContext;
      ignoreCache = false;
      if (!(context.Items[(object) PageRouteHandler.RegisteredCacheVariations] is PageRouteHandler.CustomOutputCacheVariationsRegistry cacheVariationsRegistry))
        return;
      if (cacheVariationsRegistry.Validated)
      {
        ignoreCache = cacheVariationsRegistry.IgnoreCache();
        if (!ignoreCache)
          PageRouteHandler.InitializeCacheHeaders(cacheVariationsRegistry.Variations, context, context.Response.Cache);
        PageRouteHandler.UpdateInmemoryCacheVariations(cacheVariationsRegistry);
      }
      else
        PageRouteHandler.RemoveCurrentCacheVariation();
    }

    internal static void InitializeCacheHeaders(IList<ICustomOutputCacheVariation> cacheVariations)
    {
      HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
      if (currentHttpContext == null || currentHttpContext.Items == null)
        return;
      PageRouteHandler.CustomOutputCacheVariationsRegistry variationsRegistry = new PageRouteHandler.CustomOutputCacheVariationsRegistry(cacheVariations);
      currentHttpContext.Items[(object) PageRouteHandler.RegisteredCacheVariations] = (object) variationsRegistry;
      PageRouteHandler.EnsureCurrentSiteContext(currentHttpContext);
      if (variationsRegistry.IgnoreCache())
        return;
      PageRouteHandler.InitializeCacheHeaders(cacheVariations, currentHttpContext, (HttpCachePolicyBase) null);
    }

    internal static void EnsureCurrentSiteContext(HttpContextBase context)
    {
      if (context == null)
        context = SystemManager.CurrentHttpContext;
      if (context == null || context.Items[(object) "sf_site_language_resolved"] != null)
        return;
      context.Items[(object) "sf_site_language_resolved"] = new object();
      string url = ObjectFactory.Resolve<SitefinityRoute>().GetVirtualPathInternal(context);
      IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
      if (multisiteContext != null)
        url = multisiteContext.UnresolveUrlAndApplyDetectedSite(url);
      ObjectFactory.Resolve<UrlLocalizationService>().UnResolveUrlAndApplyDetectedCultures(url);
    }

    private static void UpdateInmemoryCacheVariations(
      PageRouteHandler.CustomOutputCacheVariationsRegistry cacheVariationsRegistry)
    {
      if (SitefinityOutputCacheModule.Initialized)
        return;
      string requestKey = OutputCacheHelper.GenerateRequestKey(SystemManager.CurrentHttpContext);
      if (!cacheVariationsRegistry.Changed && PageRouteHandler.CacheVariationsCache.ContainsKey(requestKey))
        return;
      lock (PageRouteHandler.CacheVariationsCache)
      {
        if (!cacheVariationsRegistry.Changed && PageRouteHandler.CacheVariationsCache.ContainsKey(requestKey))
          return;
        PageRouteHandler.CacheVariationsCache[requestKey] = cacheVariationsRegistry.Variations;
      }
    }

    private static void RemoveCurrentCacheVariation()
    {
      if (SitefinityOutputCacheModule.Initialized)
        return;
      string requestKey = OutputCacheHelper.GenerateRequestKey(SystemManager.CurrentHttpContext);
      if (!PageRouteHandler.CacheVariationsCache.ContainsKey(requestKey))
        return;
      lock (PageRouteHandler.CacheVariationsCache)
      {
        if (!PageRouteHandler.CacheVariationsCache.ContainsKey(requestKey))
          return;
        PageRouteHandler.CacheVariationsCache.Remove(requestKey);
      }
    }

    private static void InitializeCacheHeaders(
      IList<ICustomOutputCacheVariation> cacheVariations,
      HttpContextBase context,
      HttpCachePolicyBase cache)
    {
      if (context == null)
        context = SystemManager.CurrentHttpContext;
      if (SitefinityOutputCacheModule.Initialized)
      {
        if (!cacheVariations.Any<ICustomOutputCacheVariation>())
          return;
        context.Items[(object) "sf_output_cache_variation_key"] = (object) OutputCacheHelper.GenerateVariationsKey(cacheVariations);
      }
      else
      {
        foreach (ICustomOutputCacheVariation cacheVariation in (IEnumerable<ICustomOutputCacheVariation>) cacheVariations)
        {
          if (cache != null)
            cache.VaryByHeaders[cacheVariation.Key] = true;
          if (context.Request.Headers[cacheVariation.Key] == null)
          {
            string str = cacheVariation.GetValue() ?? string.Empty;
            if (HttpRuntime.UsingIntegratedPipeline)
            {
              try
              {
                context.Request.Headers.Add(cacheVariation.Key, str);
              }
              catch (NotSupportedException ex)
              {
                PageRouteHandler.AddHeaderToRequest(context.Request.Headers, cacheVariation.Key, str);
              }
            }
            else
              PageRouteHandler.AddHeaderToRequest(context.Request.Headers, cacheVariation.Key, str);
          }
        }
      }
    }

    private static void AddHeaderToRequest(NameValueCollection headers, string key, string value)
    {
      Type type = headers.GetType();
      ArrayList arrayList = new ArrayList();
      type.InvokeMember("MakeReadWrite", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, (Binder) null, (object) headers, (object[]) null);
      type.InvokeMember("InvalidateCachedArrays", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, (Binder) null, (object) headers, (object[]) null);
      arrayList.Add((object) value);
      type.InvokeMember("BaseAdd", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, (Binder) null, (object) headers, new object[2]
      {
        (object) key,
        (object) arrayList
      });
      type.InvokeMember("MakeReadOnly", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, (Binder) null, (object) headers, (object[]) null);
    }

    private static bool ResourceHttpModuleLoaded
    {
      get
      {
        if (!PageRouteHandler.resourceHttpModuleLoaded.HasValue)
          PageRouteHandler.resourceHttpModuleLoaded = new bool?(((IEnumerable<string>) HttpContext.Current.ApplicationInstance.Modules.AllKeys).Any<string>((Func<string, bool>) (x => x == "ResourceModule")));
        return PageRouteHandler.resourceHttpModuleLoaded.Value;
      }
    }

    internal static void TryEnchanceResponseCache(HttpContext context)
    {
      if (!(context.Items[(object) PageRouteHandler.OutputCachedPageSiteNode] is PageSiteNode pageSiteNode))
        return;
      PageRouteHandler.FixCachePolicyTimestamp(context);
      OutputCacheProfileElement outputCacheProfile = pageSiteNode.GetOutputCacheProfile();
      if (outputCacheProfile == null || !outputCacheProfile.IsClient() || PageOutputCacheStrategy.IsClientCacheDenied(pageSiteNode))
        return;
      HttpCachePolicy cache = context.Response.Cache;
      int? nullable = outputCacheProfile.ClientMaxAge;
      if (nullable.HasValue)
      {
        HttpCachePolicy httpCachePolicy = cache;
        nullable = outputCacheProfile.ClientMaxAge;
        TimeSpan delta = new TimeSpan(0, 0, nullable.Value);
        httpCachePolicy.SetMaxAge(delta);
      }
      if (!outputCacheProfile.IsProxy())
        return;
      nullable = outputCacheProfile.ProxyMaxAge;
      if (!nullable.HasValue)
        return;
      HttpCachePolicy httpCachePolicy1 = cache;
      nullable = outputCacheProfile.ProxyMaxAge;
      TimeSpan delta1 = new TimeSpan(0, 0, nullable.Value);
      httpCachePolicy1.SetProxyMaxAge(delta1);
    }

    internal static bool TryGetCurrentCustomCacheVariations(
      out IList<ICustomOutputCacheVariation> cacheVariations)
    {
      string requestKey = OutputCacheHelper.GenerateRequestKey(SystemManager.CurrentHttpContext);
      return PageRouteHandler.CacheVariationsCache.TryGetValue(requestKey, out cacheVariations);
    }

    private static void ValidateCacheOutput(
      HttpContext context,
      object data,
      ref HttpValidationStatus status)
    {
      HttpContextWrapper httpContextWrapper = new HttpContextWrapper(context);
      PageSiteNodeResolver siteNodeResolver = (PageSiteNodeResolver) data;
      PageSiteNode pageSiteNode = siteNodeResolver.Resolve();
      if (pageSiteNode != null)
      {
        RouteHelper.SslRedirectIfNeeded((HttpContextBase) httpContextWrapper, pageSiteNode);
        if (PageRouteHandler.IgnoreOutputCache((HttpContextBase) httpContextWrapper, pageSiteNode, siteNodeResolver.SiteId) || PageRouteHandler.ProcessRequestLicensing((HttpContextBase) httpContextWrapper))
        {
          status = HttpValidationStatus.IgnoreThisRequest;
        }
        else
        {
          status = HttpValidationStatus.Valid;
          context.Items[(object) PageRouteHandler.OutputCachedPageSiteNode] = (object) pageSiteNode;
        }
      }
      else
        status = HttpValidationStatus.Invalid;
      SystemManager.HttpContextItems.Add((object) "OutputCacheStatus", (object) Enum.GetName(typeof (HttpValidationStatus), (object) status));
    }

    private static bool IgnoreOutputCache(
      HttpContextBase context,
      PageSiteNode siteNode,
      Guid siteId = default (Guid))
    {
      if (context.Request.HttpMethod != null && context.Request.HttpMethod.Equals("POST", StringComparison.OrdinalIgnoreCase) || context.Items[(object) PageRouteHandler.RegisteredCacheVariations] is PageRouteHandler.CustomOutputCacheVariationsRegistry variationsRegistry && variationsRegistry.IgnoreCache() || !PageRouteHandler.IsPageViewPermitted(context, siteNode))
        return true;
      if (siteId != Guid.Empty)
      {
        if (!siteNode.IsBackend && SystemManager.IsSiteOffline(siteId) || !siteNode.IsBackend && SystemManager.IsShieldEnabledForSite(siteId))
          return true;
      }
      else if (!siteNode.IsBackend && SystemManager.CurrentContext.CurrentSite.IsOffline || !siteNode.IsBackend && SystemManager.IsShieldEnabled)
        return true;
      if (siteNode.IsBackend || PageRouteHandler.DisableOutptuCacheForCurrentUser() || ControlExtensions.BrowseAndEditIsEnabled())
        return true;
      return context.Request.IsAuthenticated && siteNode.HasSecuredWidgets;
    }

    private static bool DisableOutptuCacheForCurrentUser()
    {
      KnownUserGroups outputCacheUserGroup = Config.Get<PagesConfig>().IgnoreOutputCacheUserGroup;
      if (outputCacheUserGroup != KnownUserGroups.None)
      {
        SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
        if (currentIdentity != null)
          return outputCacheUserGroup == KnownUserGroups.BackendUsers ? currentIdentity.IsBackendUser : currentIdentity.IsUnrestricted;
      }
      return false;
    }

    /// <summary>
    /// Determines whether the current user can view this page.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="siteNode">The site node.</param>
    /// <returns>
    ///     <c>true</c> if [is page view permitted] [the specified context]; otherwise, <c>false</c>.
    /// </returns>
    private static bool IsPageViewPermitted(HttpContextBase context, PageSiteNode siteNode) => !(siteNode.Provider is SiteMapBase provider) || provider.IsAccessibleToUser(context.ApplicationInstance.Context, (SiteMapNode) siteNode);

    /// <summary>
    /// Gets a value indicating whether the sitemap is in multilingual mode
    /// </summary>
    public bool IsMultilingual => SystemManager.CurrentContext.AppSettings.Multilingual;

    /// <summary>
    /// Request from a single IP Counter used for trial messages.
    /// </summary>
    /// <value>The previous requests.</value>
    private static Dictionary<string, int> PreviousRequests
    {
      get
      {
        if (PageRouteHandler.requests == null)
          PageRouteHandler.requests = new Dictionary<string, int>();
        return PageRouteHandler.requests;
      }
    }

    /// <summary>Gets the get trial mode response message.</summary>
    /// <value>The get trial response message.</value>
    internal static string GetTrialResponseMessage => "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"><html xmlns=\"http://www.w3.org/1999/xhtml\"><head><meta http-equiv=\"content-type\" content=\"text/html; charset=utf-8\" /><title>Sitefinity trial version</title></head><body><div style='font-family: Arial; font-size: 14px; color: #666; position: absolute; top: 50%; left: 50%; width: 680px; height: 50px; margin-left: -340px; margin-top: -25px; text-align: center;'><h1 style='font-size: 30px; color: #999; font-weight: normal; margin: 0; padding: 0;'>You are running a trial version of <a title='Sitefinity CMS' href='https://www.progress.com/sitefinity-cms' style='border-bottom: 1px dotted #ccc; color: #999; text-decoration: none;'>Sitefinity {1}</a>.</h1><p style='margin:0; padding: 0;'>To proceed with your evaluation, press <a href='{0}'>Continue</a> or refresh the page.</p></div></body></html>";

    /// <summary>Processes the site node.</summary>
    /// <param name="node">The site node.</param>
    protected virtual void ProcessSiteNode(PageSiteNode node)
    {
    }

    /// <summary>
    /// Verifies if the request matches the Sitefinity licensing restrictions (domain name, etc.). Shows a trial message on every 20 request that come from unlicensed domain or a trial license.
    /// </summary>
    /// <param name="httpContext">The HTTP context.</param>
    /// <returns>true if a trial message is to be shown</returns>
    internal static bool ProcessRequestLicensing(HttpContextBase httpContext)
    {
      HttpRequestBase request = httpContext.Request;
      if (httpContext.Request.Url.AbsolutePath.IndexOf("/" + "RestApi" + "/", StringComparison.OrdinalIgnoreCase) > -1 || !LicenseState.Current.IsInTrialMode)
        return false;
      string key = request.UserHostAddress ?? "default";
      int num;
      if (!PageRouteHandler.PreviousRequests.TryGetValue(key, out num))
      {
        PageRouteHandler.PreviousRequests[key] = 0;
      }
      else
      {
        if (num >= 20)
        {
          Version getProductVersion = LicenseState.GetProductVersion;
          string str = getProductVersion.Major.ToString() + "." + (object) getProductVersion.Minor;
          string s = PageRouteHandler.GetTrialResponseMessage.Arrange((object) HttpUtility.HtmlEncode(request.Url.ToString()), (object) str);
          PageRouteHandler.PreviousRequests[key] = 0;
          httpContext.Response.Clear();
          httpContext.Response.AddHeader("Content-Type", "text/html; charset=" + httpContext.Response.Charset);
          httpContext.Response.Write(s);
          httpContext.Response.End();
          return true;
        }
        PageRouteHandler.PreviousRequests[key] = num + 1;
      }
      return false;
    }

    internal static bool IsCacheDependenciesPersistedInMemory => PageRouteHandler.cacheDependencyPersistanceMode == PageRouteHandler.CacheDependencyPersistanceMode.InMemory;

    private static HtmlControl FindHtmlTag(Page page)
    {
      Control control1 = (Control) page;
      if (page.Master != null)
      {
        MasterPage master = page.Master;
        while (master.Master != null)
          master = master.Master;
        control1 = (Control) master;
      }
      foreach (object control2 in control1.Controls)
      {
        if (control2 is HtmlControl htmlTag && htmlTag.TagName.Equals("html", StringComparison.OrdinalIgnoreCase))
          return htmlTag;
      }
      return (HtmlControl) null;
    }

    private static void FixCachePolicyTimestamp(HttpContext context)
    {
      if (!(context.Response.Cache.GetType() == typeof (HttpCachePolicy)) || !(PageRouteHandler.CacheUtcTimestampRequestField != (FieldInfo) null) || !((DateTime) PageRouteHandler.CacheUtcTimestampRequestField.GetValue((object) context.Response.Cache) == DateTime.MinValue))
        return;
      PageRouteHandler.CacheUtcTimestampRequestField.SetValue((object) context.Response.Cache, (object) context.Timestamp.ToUniversalTime());
    }

    internal class CustomOutputCacheVariationsRegistry
    {
      private readonly IList<ICustomOutputCacheVariation> variations;
      private bool? ignoreCache;
      private bool changed;

      public CustomOutputCacheVariationsRegistry()
      {
        this.variations = (IList<ICustomOutputCacheVariation>) new List<ICustomOutputCacheVariation>();
        this.changed = true;
      }

      public CustomOutputCacheVariationsRegistry(IList<ICustomOutputCacheVariation> variations)
      {
        this.variations = variations;
        this.Validated = false;
      }

      public bool Validated { get; set; }

      public bool Changed => this.changed;

      public IList<ICustomOutputCacheVariation> Variations => this.variations;

      public void AddVariation(ICustomOutputCacheVariation variation)
      {
        ICustomOutputCacheVariation outputCacheVariation = this.variations.FirstOrDefault<ICustomOutputCacheVariation>((Func<ICustomOutputCacheVariation, bool>) (v => v.Key == variation.Key));
        if (outputCacheVariation != null)
        {
          if (outputCacheVariation.Equals((object) variation))
            return;
          this.variations.Remove(outputCacheVariation);
        }
        this.variations.Add(variation);
        this.changed = true;
        this.ignoreCache = new bool?();
      }

      public bool IgnoreCache()
      {
        if (!this.ignoreCache.HasValue)
          this.ignoreCache = new bool?(this.variations.Any<ICustomOutputCacheVariation>((Func<ICustomOutputCacheVariation, bool>) (cv => cv.NoCache)));
        return this.ignoreCache.Value;
      }
    }

    private enum CacheDependencyPersistanceMode
    {
      Database = 0,
      Default = 0,
      InMemory = 1,
    }
  }
}
