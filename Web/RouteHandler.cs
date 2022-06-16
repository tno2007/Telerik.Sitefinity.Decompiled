// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.RouteHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization.UrlLocalizationStrategies;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.Compilation;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Templates;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web
{
  /// <summary>The base class for Sitefinity route handlers.</summary>
  public abstract class RouteHandler : IRouteHandler
  {
    private static TemplateInfo emptyTemplateInfo;
    private static TemplateInfo backendTemplateInfo;
    /// <summary>
    /// A key used to retrieve <see cref="T:System.Web.Routing.RequestContext" /> object for the current HTTP request.
    /// </summary>
    public const string RequestContextKey = "sf_RequestContext";
    /// <summary>
    /// A key used to retrieve a collection of place holders for the current page.
    /// </summary>
    public const string PlaceHoldersKey = "sf_PlaceHolders";
    internal const int SitefinityInitValue = 0;
    internal const string SitefinityInitKey = "RadControlRandomNumber";
    internal const string frontendTemplateName = "Telerik.Sitefinity.Resources.Pages.Frontend.aspx";
    private const string backendTempalteName = "Telerik.Sitefinity.Resources.Pages.Backend.master";
    public const string UrlEvaluationModeKey = "SF_PageUrlEvaluationMode";

    /// <summary>Gets the empty template info.</summary>
    /// <value>The empty template info.</value>
    internal static TemplateInfo EmptyTemplateInfo
    {
      get
      {
        if (RouteHandler.emptyTemplateInfo == null)
          RouteHandler.emptyTemplateInfo = new TemplateInfo()
          {
            TemplateName = "Telerik.Sitefinity.Resources.Pages.Frontend.aspx",
            ControlType = typeof (PageRouteHandler)
          };
        return RouteHandler.emptyTemplateInfo;
      }
    }

    /// <summary>Gets the backend template info.</summary>
    /// <value>The backend template info.</value>
    internal static TemplateInfo BackendTemplateInfo
    {
      get
      {
        if (RouteHandler.backendTemplateInfo == null)
          RouteHandler.backendTemplateInfo = new TemplateInfo()
          {
            TemplateName = "Telerik.Sitefinity.Resources.Pages.Backend.master",
            ControlType = typeof (PageRouteHandler)
          };
        return RouteHandler.backendTemplateInfo;
      }
    }

    protected virtual TemplateInfo DefaultTemplate => RouteHandler.EmptyTemplateInfo;

    /// <summary>Provides the object that processes the request.</summary>
    /// <param name="requestContext">An object that encapsulates information about the request.</param>
    /// <returns>An object that processes the request.</returns>
    public virtual IHttpHandler GetHttpHandler(RequestContext requestContext)
    {
      if (requestContext == null)
        throw new ArgumentNullException(nameof (requestContext));
      SiteMapNode dataToken = (SiteMapNode) requestContext.RouteData.DataTokens["SiteMapNode"];
      if (dataToken == null)
        throw new ArgumentException("Invalid request for this route handler. Route data must contain SiteMapNode.");
      this.ProcessSiteNode(dataToken);
      IPageData pageData = this.GetPageData(dataToken);
      if (pageData != null && pageData is DraftProxyBase draftProxyBase && requestContext.HttpContext != null && requestContext.HttpContext.Items != null)
        requestContext.HttpContext.Items[(object) "PageTemplateFramework"] = (object) draftProxyBase.Framework;
      return this.BuildHandler(requestContext, pageData);
    }

    /// <summary>Builds the handler.</summary>
    /// <param name="requestContext">The request context.</param>
    /// <param name="pageData">The page data.</param>
    /// <returns></returns>
    protected virtual IHttpHandler BuildHandler(
      RequestContext requestContext,
      IPageData pageData)
    {
      return (IHttpHandler) this.GetHandler(requestContext, pageData);
    }

    /// <summary>
    /// Gets the http handler for the specified page data in the specified context.
    /// </summary>
    /// <param name="requestContext">The request context.</param>
    /// <param name="pageData">The page data.</param>
    internal virtual Page GetHandler(RequestContext requestContext, IPageData pageData)
    {
      if (this.SecurityCheck(pageData, requestContext))
        return (Page) null;
      Page handler;
      if (!string.IsNullOrEmpty(pageData.ExternalPage))
        handler = RouteHandler.CreateHandler(requestContext, pageData.ExternalPage);
      else if (!string.IsNullOrEmpty(pageData.MasterPage))
      {
        handler = RouteHandler.CreateHandler(requestContext, (ITemplate) null);
      }
      else
      {
        ITemplate pageTemplate = this.GetPageTemplate(pageData);
        if (pageTemplate != null)
        {
          handler = RouteHandler.CreateHandler(requestContext, pageTemplate);
        }
        else
        {
          TemplateInfo defaultTemplate = this.DefaultTemplate;
          handler = RouteHandler.CreateHandler(requestContext, ControlUtilities.GetTemplate(defaultTemplate));
        }
      }
      bool flag = AppSettings.CurrentSettings.CombineScripts();
      if (pageData.IncludeScriptManager || flag)
        RouteHandler.EnsureScriptManager(handler);
      if (Config.Get<PagesConfig>().CombineStyleSheets.Value)
        RouteHandler.EnsureManagers(handler);
      RouteHandler.EnsureSitefinityResourceManager(handler);
      this.SetPageDirectives(handler, pageData);
      this.InitOutputCache(pageData, requestContext);
      handler.PreInit += new EventHandler(this.Handler_PreInit);
      handler.Load += new EventHandler(this.Handler_Load);
      handler.PreRenderComplete += (EventHandler) ((sender, args) => ResourceLinks.RegisterDynamicWebResourceContainer(sender as Control));
      return handler;
    }

    /// <summary>
    /// Performs security checks and takes appropriate actions. Return true if the request is redirected.
    /// </summary>
    /// <param name="pageData">The page data.</param>
    /// <param name="requestContext">The request context.</param>
    /// <returns></returns>
    protected virtual bool SecurityCheck(IPageData pageData, RequestContext requestContext) => false;

    /// <summary>Sets the page directives.</summary>
    /// <param name="handler">The handler.</param>
    /// <param name="pageData">The page data.</param>
    protected abstract void SetPageDirectives(Page handler, IPageData pageData);

    /// <summary>Gets the page data.</summary>
    /// <param name="node">The node.</param>
    /// <returns></returns>
    protected abstract IPageData GetPageData(SiteMapNode node);

    /// <summary>Gets the page template.</summary>
    /// <param name="pageData">The page data.</param>
    /// <returns></returns>
    protected abstract ITemplate GetPageTemplate(IPageData pageData);

    /// <summary>
    /// Applies the layouts and controls to the provided page.
    /// </summary>
    /// <param name="page">The page.</param>
    /// <param name="requestContext">The request context.</param>
    protected abstract void ApplyLayoutsAndControls(Page page, RequestContext requestContext);

    /// <summary>
    /// Initializes the output cache for the current page request.
    /// </summary>
    /// <param name="handler">The resolved HTTP handler.</param>
    /// <param name="pageData">The stored page data.</param>
    protected virtual void InitOutputCache(IPageData pageData, RequestContext requestContext)
    {
    }

    /// <summary>Processes the site node.</summary>
    /// <param name="node">The site node.</param>
    protected virtual void ProcessSiteNode(SiteMapNode node)
    {
    }

    private void Handler_Load(object sender, EventArgs e)
    {
      Page page = (Page) sender;
      RequestContext requestContext = page is ISitefinityPage sitefinityPage ? sitefinityPage.RequestContext : (RequestContext) SystemManager.CurrentHttpContext.Items[(object) "sf_RequestContext"];
      ResourceLinks globalStyles = ThemeController.GetGlobalStyles(page);
      if (globalStyles != null)
        page.Controls.Add((Control) globalStyles);
      this.InitializeContent((Page) sender, requestContext);
    }

    private void Handler_PreInit(object sender, EventArgs e)
    {
      Page handler = (Page) sender;
      RequestContext requestContext = handler is ISitefinityPage sitefinityPage ? sitefinityPage.RequestContext : (RequestContext) SystemManager.CurrentHttpContext.Items[(object) "sf_RequestContext"];
      requestContext.HttpContext.Items[(object) "RadControlRandomNumber"] = (object) 0;
      this.InitializeHttpHandler(handler, requestContext);
    }

    /// <summary>
    /// Initializes internal controls. In this method any additional controls can be
    /// instantiated and added to the page controls collection.
    /// </summary>
    /// <param name="handler">
    /// A <see cref="T:System.Web.UI.Page" /> that will handle the current HTTP request.
    /// </param>
    /// <param name="requestContext">
    /// An object that encapsulates information about the request.
    /// </param>
    protected virtual void InitializeHttpHandler(Page handler, RequestContext requestContext)
    {
    }

    /// <summary>
    /// Initializes the content of a backend page.
    /// The main navigation is added prior to this method.
    /// </summary>
    /// <param name="handler">
    /// A <see cref="T:System.Web.UI.Page" /> that will handle the current HTTP request.
    /// </param>
    /// <param name="requestContext">
    /// An object that encapsulates information about the request.
    /// </param>
    protected virtual void InitializeContent(Page handler, RequestContext requestContext)
    {
      if (handler.GetPlaceHolders() == null)
      {
        PlaceHoldersCollection handlerPlaceHolders = PageHelper.CreateHandlerPlaceHolders(handler);
        handler.SetPlaceHolders(handlerPlaceHolders);
      }
      this.ApplyLayoutsAndControls(handler, requestContext);
    }

    /// <summary>Creates the handler.</summary>
    /// <param name="requestContext">The request context.</param>
    /// <param name="virtualPath">The virtual path.</param>
    /// <returns></returns>
    protected static Page CreateHandler(RequestContext requestContext, string virtualPath)
    {
      Page handler = CompilationHelpers.LoadControl<Page>(virtualPath);
      if (handler is ISitefinityPage sitefinityPage)
      {
        sitefinityPage.RequestContext = requestContext;
        return handler;
      }
      requestContext.HttpContext.Items[(object) "sf_RequestContext"] = (object) requestContext;
      return handler;
    }

    /// <summary>Creates the handler.</summary>
    /// <param name="requestContext">The request context.</param>
    /// <param name="pageTemplate">The page template.</param>
    /// <returns></returns>
    protected static Page CreateHandler(RequestContext requestContext, ITemplate pageTemplate)
    {
      Page handler;
      if (pageTemplate is VirtualPathTemplate virtualPathTemplate && virtualPathTemplate.IsPage)
      {
        handler = (Page) virtualPathTemplate.GetInstance();
        PlaceHoldersCollection handlerPlaceHolders = PageHelper.CreateHandlerPlaceHolders(handler);
        if (handler is ISitefinityPage sitefinityPage)
        {
          sitefinityPage.RequestContext = requestContext;
          sitefinityPage.PlaceHolders = handlerPlaceHolders;
        }
        else
        {
          IDictionary items = requestContext.HttpContext.Items;
          items[(object) "sf_RequestContext"] = (object) requestContext;
          items[(object) "sf_PlaceHolders"] = (object) handlerPlaceHolders;
        }
      }
      else
      {
        ISitefinityPage sitefinityPage = CompilationHelpers.LoadControl<ISitefinityPage>("~/SFRes/Telerik.Sitefinity.Resources.SitefinityDefault.aspx");
        handler = (Page) sitefinityPage;
        sitefinityPage.RequestContext = requestContext;
        string str = ObjectFactory.Resolve<UrlLocalizationService>().UnResolveUrl(requestContext.HttpContext.Request.AppRelativeCurrentExecutionFilePath);
        handler.AppRelativeVirtualPath = str;
        if (pageTemplate != null)
        {
          if (pageTemplate is IContentPlaceHolderContainer placeHolderContainer)
            sitefinityPage.PlaceHolders = placeHolderContainer.InstantiateIn((Control) handler);
          else
            pageTemplate.InstantiateIn((Control) handler);
        }
      }
      return handler;
    }

    internal static void EnsureSitefinityResourceManager(Page page)
    {
      if (page.Form == null)
        page.InitComplete += new EventHandler(RouteHandler.page_InitCompleteSitefinityResourceManager);
      else
        RouteHandler.EnsureSitefinityResourceManagerInternal(page);
    }

    private static void page_InitCompleteSitefinityResourceManager(object sender, EventArgs e) => RouteHandler.EnsureSitefinityResourceManagerInternal(sender as Page);

    private static void EnsureSitefinityResourceManagerInternal(Page page)
    {
      if (SitefinityStyleSheetManager.GetCurrent(page) == null)
      {
        SitefinityStyleSheetManager child = new SitefinityStyleSheetManager();
        if (page.Form != null)
          page.Form.Controls.Add((Control) child);
      }
      if (SitefinityScriptManager.GetCurrent(page) != null)
        return;
      SitefinityScriptManager child1 = new SitefinityScriptManager();
      if (page.Form == null)
        return;
      page.Form.Controls.Add((Control) child1);
    }

    internal static void EnsureStyleSheetManager(Page page)
    {
      if (page.Form == null)
        page.InitComplete += new EventHandler(RouteHandler.page_InitCompleteStyleSheet);
      else
        RouteHandler.EnsureStyleSheetManagerInternal(page);
    }

    private static void page_InitCompleteStyleSheet(object sender, EventArgs e) => RouteHandler.EnsureStyleSheetManagerInternal(sender as Page);

    private static void EnsureStyleSheetManagerInternal(Page page)
    {
      if (RadStyleSheetManager.GetCurrent(page) != null)
        return;
      bool flag = Config.Get<PagesConfig>().CombineStyleSheets.Value;
      OutputCompression outputCompression = OutputCompression.Disabled;
      SitefinityRadStyleSheetManager styleSheetManager = new SitefinityRadStyleSheetManager();
      styleSheetManager.OutputCompression = outputCompression;
      styleSheetManager.EnableStyleSheetCombine = flag;
      SitefinityRadStyleSheetManager child = styleSheetManager;
      if (page.Form == null)
        return;
      page.Form.Controls.Add((Control) child);
    }

    internal static void EnsureScriptManager(Page page)
    {
      if (page.Form == null)
        page.InitComplete += new EventHandler(RouteHandler.page_InitCompleteScript);
      else
        RouteHandler.EnsureScriptManagerInternal(page);
    }

    private static void page_InitCompleteScript(object sender, EventArgs e) => RouteHandler.EnsureScriptManagerInternal(sender as Page);

    private static void EnsureScriptManagerInternal(Page page)
    {
      ScriptManager child = ScriptManager.GetCurrent(page);
      if (child == null && page.Form != null)
      {
        child = (ScriptManager) new RadScriptManager();
        page.Form.Controls.AddAt(0, (Control) child);
      }
      if (!(child is RadScriptManager radScriptManager))
        return;
      if (!radScriptManager.EnableHistory)
        radScriptManager.EnableHistory = true;
      if (radScriptManager.OutputCompression != AppSettings.CurrentSettings.CompressScripts())
        radScriptManager.OutputCompression = AppSettings.CurrentSettings.CompressScripts();
      if (radScriptManager.EnableScriptCombine != AppSettings.CurrentSettings.CombineScripts())
        radScriptManager.EnableScriptCombine = AppSettings.CurrentSettings.CombineScripts();
      if (radScriptManager.EnableScriptGlobalization)
        return;
      radScriptManager.EnableScriptGlobalization = true;
    }

    internal static void EnsureManagers(Page page)
    {
      RouteHandler.EnsureScriptManager(page);
      RouteHandler.EnsureStyleSheetManager(page);
    }
  }
}
