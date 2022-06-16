// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.RouteHandlerBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.Compilation;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Templates;

namespace Telerik.Sitefinity.Web
{
  /// <summary>Route handler for Sitefinity dialogs.</summary>
  public class RouteHandlerBase : IRouteHandler
  {
    private static RouteInfoCollection childRoutes = new RouteInfoCollection();
    private const int SitefinityInitValue = 0;
    private const string SitefinityInitKey = "RadControlRandomNumber";

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Web.RouteHandlerBase" />.
    /// </summary>
    public RouteHandlerBase()
      : this((string) null)
    {
    }

    /// <summary>
    /// Initializes new named instance of <see cref="T:Telerik.Sitefinity.Web.RouteHandlerBase" />.
    /// </summary>
    /// <param name="name">The name of this handler.</param>
    public RouteHandlerBase(string name)
    {
      this.Name = name;
      List<RouteInfo> list = new List<RouteInfo>();
      this.RegisterChildRouteHandlers((IList<RouteInfo>) list);
      RouteHandlerBase.ChildRoutes.Unlock();
      foreach (RouteInfo routeInfo in list)
      {
        if (!RouteHandlerBase.ChildRoutes.Contains(routeInfo.Key))
          RouteHandlerBase.ChildRoutes.Add(routeInfo);
      }
      RouteHandlerBase.ChildRoutes.Lock();
    }

    /// <summary>
    /// Defines the page template and resources loaded for the current request.
    /// </summary>
    public virtual TemplateInfo GetTemplateInfo(RequestContext requestContext)
    {
      if (requestContext == null)
        throw new ArgumentNullException(nameof (requestContext));
      return (TemplateInfo) null;
    }

    /// <summary>Gets the root path for this handler.</summary>
    public virtual string Root => string.Empty;

    /// <summary>Gets the name of this handler.</summary>
    public string Name { get; private set; }

    /// <summary>
    /// Gets or sets the global resource class ID to use for localized strings.
    /// </summary>
    public string ResourceClassId { get; set; }

    /// <summary>Gets a dictionary of child routes.</summary>
    public static RouteInfoCollection ChildRoutes => RouteHandlerBase.childRoutes;

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
    }

    /// <summary>Registers child partial route handlers.</summary>
    /// <param name="list">A list of <see cref="T:Telerik.Sitefinity.Web.RouteInfo" /> objects.</param>
    protected virtual void RegisterChildRouteHandlers(IList<RouteInfo> list)
    {
    }

    /// <summary>
    /// Gets an instance of a control for the specified type and if it is a partial route
    /// handler, initializes it with the provided request context.
    /// </summary>
    /// <typeparam name="T">The type to resolve.</typeparam>
    /// <param name="requestContext"><see cref="T:System.Web.Routing.RequestContext" /></param>
    /// <returns></returns>
    protected static T ResolveControl<T>(RequestContext requestContext) => RouteHandlerBase.ResolveControl<T>((string) null, requestContext, (string) null);

    /// <summary>
    /// Gets an instance of a control for the specified type and if it is a partial route
    /// handler, initializes it with the provided request context.
    /// </summary>
    /// <typeparam name="T">The type to resolve.</typeparam>
    /// <param name="requestContext"><see cref="T:System.Web.Routing.RequestContext" /></param>
    /// <param name="routeKey"></param>
    /// <returns></returns>
    protected static T ResolveControl<T>(RequestContext requestContext, string routeKey) => RouteHandlerBase.ResolveControl<T>((string) null, requestContext, routeKey);

    /// <summary>
    /// Gets an instance of a control for the specified type and if it is a partial route
    /// handler, initializes it with the provided request context.
    /// </summary>
    /// <typeparam name="T">The type to resolve.</typeparam>
    /// <param name="name">The name of the control if registered as named instance.</param>
    /// <param name="requestContext"><see cref="T:System.Web.Routing.RequestContext" /></param>
    /// <param name="routeKey"></param>
    /// <returns></returns>
    protected static T ResolveControl<T>(
      string name,
      RequestContext requestContext,
      string routeKey)
    {
      if (requestContext == null)
        throw new ArgumentNullException(nameof (requestContext));
      T obj = !string.IsNullOrEmpty(name) ? ObjectFactory.Resolve<T>(name) : ObjectFactory.Resolve<T>();
      RouteHandlerBase.SetPartialRouteHandler((object) obj, name, requestContext, routeKey);
      return obj;
    }

    /// <summary>
    /// Gets an instance of a control for the specified type and if it is a partial route
    /// handler, initializes it with the provided request context.
    /// </summary>
    /// <param name="type">The type to resolve.</param>
    /// <param name="name">The name of the control if registered as named instance.</param>
    /// <param name="requestContext"><see cref="T:System.Web.Routing.RequestContext" /></param>
    /// <param name="routeKey"></param>
    /// <returns></returns>
    protected static object ResolveControl(
      Type type,
      string name,
      RequestContext requestContext,
      string routeKey)
    {
      if (requestContext == null)
        throw new ArgumentNullException(nameof (requestContext));
      object obj = !string.IsNullOrEmpty(name) ? ObjectFactory.Resolve(type, name) : ObjectFactory.Resolve(type);
      RouteHandlerBase.SetPartialRouteHandler(obj, name, requestContext, routeKey);
      return obj;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="handlerType"></param>
    /// <param name="name"></param>
    /// <param name="requestContext"></param>
    /// <param name="routeKey"></param>
    /// <returns></returns>
    protected static RequestContext GetPartialRequestContext(
      Type handlerType,
      string name,
      RequestContext requestContext,
      string routeKey)
    {
      RouteInfo routeInfo;
      PartialHttpContext httpContext;
      RouteData routeData;
      if (RouteHandlerBase.ChildRoutes.TryGetValue(handlerType.FullName + name, out routeInfo))
      {
        httpContext = new PartialHttpContext((string) requestContext.RouteData.Values[routeKey]);
        routeData = routeInfo.Routes.GetRouteData((HttpContextBase) httpContext) ?? new RouteData();
      }
      else
      {
        httpContext = new PartialHttpContext(string.Empty);
        routeData = new RouteData();
      }
      return (RequestContext) new PartialRequestContext(httpContext, routeData, routeKey);
    }

    /// <summary>Initializes a partial route handler.</summary>
    /// <param name="obj">The instance of the partial route handler to initialize.</param>
    /// <param name="handlerName">The given name of handler.</param>
    /// <param name="requestContext">The request context.</param>
    /// <param name="routeKey">The route key.</param>
    protected static void SetPartialRouteHandler(
      object obj,
      string handlerName,
      RequestContext requestContext,
      string routeKey)
    {
      if (!(obj is IPartialRouteHandler partHandler))
        return;
      RouteCollection routes = RouteHandlerBase.ChildRoutes[partHandler.GetType().FullName + handlerName].Routes;
      RouteHandlerBase.SetPartialRouteHandler(partHandler, routes, requestContext, routeKey);
    }

    /// <summary>Sets the partial route handler.</summary>
    /// <param name="partHandler">The partial route handler.</param>
    /// <param name="routes">The routes.</param>
    /// <param name="requestContext">The request context.</param>
    /// <param name="routeKey">The route key.</param>
    protected static void SetPartialRouteHandler(
      IPartialRouteHandler partHandler,
      RouteCollection routes,
      RequestContext requestContext,
      string routeKey)
    {
      if (string.IsNullOrEmpty(routeKey))
        routeKey = "Params";
      partHandler.ParentRouteHandler = (IPartialRouteHandler) requestContext.HttpContext.Handler;
      PartialHttpContext httpContext;
      RouteData routeData;
      if (routes != null)
      {
        httpContext = new PartialHttpContext((string) requestContext.RouteData.Values[routeKey]);
        routeData = routes.GetRouteData((HttpContextBase) httpContext) ?? new RouteData();
      }
      else
      {
        httpContext = new PartialHttpContext(string.Empty);
        routeData = new RouteData();
      }
      partHandler.PartialRequestContext = new PartialRequestContext(httpContext, routeData, routeKey);
    }

    internal static string GetVirtualPath(
      Type handlerType,
      string handlerName,
      RouteValueDictionary values)
    {
      RouteInfo routeInfo1;
      if (!RouteHandlerBase.ChildRoutes.TryGetValue(handlerType.FullName + handlerName, out routeInfo1))
        return string.Empty;
      string virtualPath = string.Empty;
      Stack<RouteInfo> routeInfoStack = new Stack<RouteInfo>();
      for (RouteInfo routeInfo2 = routeInfo1; routeInfo2 != null; routeInfo2 = routeInfo2.Parent)
        routeInfoStack.Push(routeInfo2);
      while (routeInfoStack.Count > 0)
      {
        RouteInfo routeInfo3 = routeInfoStack.Pop();
        virtualPath = routeInfo3.RootHandler == null ? virtualPath + "/" + routeInfo3.HandlerName : routeInfo3.RootHandler.Root + "/" + routeInfo3.RootHandler.Name;
      }
      return virtualPath;
    }

    private static Page CreateHandler(RequestContext requestContext, string virtualPath)
    {
      requestContext.HttpContext.Items[(object) "sf_RequestContext"] = (object) requestContext;
      Page handler = CompilationHelpers.LoadControl<Page>(virtualPath);
      if (!(handler is ISitefinityPage sitefinityPage))
        return handler;
      sitefinityPage.RequestContext = requestContext;
      return handler;
    }

    internal static Page CreateHandler(RequestContext requestContext, ITemplate pageTemplate)
    {
      requestContext.HttpContext.Items[(object) "sf_RequestContext"] = (object) requestContext;
      Page container;
      if (pageTemplate is VirtualPathTemplate virtualPathTemplate && virtualPathTemplate.IsPage)
      {
        container = (Page) virtualPathTemplate.GetInstance();
        if (container is ISitefinityPage sitefinityPage)
          sitefinityPage.RequestContext = requestContext;
      }
      else
      {
        ((ISitefinityPage) (container = (Page) CompilationHelpers.LoadControl<ISitefinityPage>("~/SFRes/Telerik.Sitefinity.Resources.SitefinityDefault.aspx"))).RequestContext = requestContext;
        container.AppRelativeVirtualPath = requestContext.HttpContext.Request.AppRelativeCurrentExecutionFilePath;
        pageTemplate?.InstantiateIn((Control) container);
      }
      container.EnableViewState = false;
      return container;
    }

    /// <summary>Provides the object that processes the request.</summary>
    /// <returns>An object that processes the request.</returns>
    /// <param name="requestContext">
    /// An object that encapsulates information about the request.
    /// </param>
    public virtual IHttpHandler GetHttpHandler(RequestContext requestContext)
    {
      int num = ConfigProvider.DisableSecurityChecks ? 1 : 0;
      ConfigProvider.DisableSecurityChecks = true;
      TemplateInfo templateInfo = this.GetTemplateInfo(requestContext);
      ConfigProvider.DisableSecurityChecks = num != 0;
      if (templateInfo == null)
        throw new InvalidOperationException(Res.Get<ErrorMessages>().NullTemplateInfo);
      Page page = !string.IsNullOrEmpty(templateInfo.TemplatePath) ? RouteHandlerBase.CreateHandler(requestContext, templateInfo.TemplatePath) : RouteHandlerBase.CreateHandler(requestContext, ControlUtilities.GetTemplate(templateInfo));
      RouteHandler.EnsureManagers(page);
      RouteHandler.EnsureSitefinityResourceManager(page);
      page.PreInit += new EventHandler(this.Handler_PreInit);
      page.Load += new EventHandler(this.Handler_Load);
      return (IHttpHandler) page;
    }

    private void Handler_Load(object sender, EventArgs e)
    {
      Page page = (Page) sender;
      RequestContext requestContext = page is ISitefinityPage sitefinityPage ? sitefinityPage.RequestContext : (RequestContext) SystemManager.CurrentHttpContext.Items[(object) "sf_RequestContext"];
      RouteHandler.EnsureScriptManager(page);
      this.InitializeContent((Page) sender, requestContext);
    }

    private void Handler_PreInit(object sender, EventArgs e)
    {
      Page handler = (Page) sender;
      RequestContext requestContext = handler is ISitefinityPage sitefinityPage ? sitefinityPage.RequestContext : (RequestContext) SystemManager.CurrentHttpContext.Items[(object) "sf_RequestContext"];
      requestContext.HttpContext.Items[(object) "RadControlRandomNumber"] = (object) 0;
      this.InitializeHttpHandler(handler, requestContext);
    }
  }
}
