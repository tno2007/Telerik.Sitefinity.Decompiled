// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Renderer.Web.Services.LayoutOperationProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Renderer.Editor.Specifics;
using Telerik.Sitefinity.Renderer.Generators;
using Telerik.Sitefinity.Renderer.Web.Services.Dto;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Services.Contracts.Operations;

namespace Telerik.Sitefinity.Renderer.Web.Services
{
  internal class LayoutOperationProvider : IOperationProvider
  {
    internal const string SkipFlag = "SkipRendererRewriteAndValidation";
    private CompositeGenerator generator;

    public LayoutOperationProvider(CompositeGenerator generator) => this.generator = generator;

    public IEnumerable<OperationData> GetOperations(Type clrType)
    {
      if (object.Equals((object) clrType, (object) typeof (PageNode)))
      {
        List<OperationData> operations = new List<OperationData>()
        {
          OperationData.Create<string, PageDtoWithContext>(new Func<OperationContext, string, PageDtoWithContext>(this.Model)),
          OperationData.Create<string, ComponentsResponse>(new Func<OperationContext, string, ComponentsResponse>(this.LazyComponents))
        };
        foreach (OperationData operationData in operations)
          operationData.OperationType = OperationType.Collection;
        return (IEnumerable<OperationData>) operations;
      }
      if (!object.Equals((object) clrType, (object) typeof (PageTemplate)))
        return (IEnumerable<OperationData>) Array.Empty<OperationData>();
      OperationData operationData1 = OperationData.Create<PageDtoWithContext>(new Func<OperationContext, PageDtoWithContext>(this.Model));
      operationData1.OperationType = OperationType.PerItem;
      return (IEnumerable<OperationData>) new OperationData[1]
      {
        operationData1
      };
    }

    private PageDtoWithContext Model(OperationContext context)
    {
      string key = context.GetKey();
      HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
      bool flag = currentHttpContext.Request.QueryString["sfaction"] != null;
      string str = currentHttpContext.Request.QueryString["sfversion"];
      PageManager manager = PageManager.GetManager();
      PageTemplate template = manager.GetTemplate(Guid.Parse(key));
      IControlsContainer startContainer = template != null ? (IControlsContainer) template : throw new ItemNotFoundException();
      if (flag)
        startContainer = (IControlsContainer) manager.TemplatesLifecycle.GetMaster(template) ?? startContainer;
      IRendererCommonData lastContainer = startContainer.GetLastContainer<IRendererCommonData>();
      if ((template.Framework == PageTemplateFramework.Mvc ? 1 : (lastContainer.IsExternallyRendered() ? 1 : 0)) == 0)
        throw new InvalidOperationException("Only Pure Mvc pages and external Renderer pages are supported");
      if (!template.Visible && !flag)
        throw new ItemNotFoundException();
      PageDto source = this.generator.Generate((IGeneratorArgs) new GeneratorArgs()
      {
        ContainerResolver = (IContainerResolver) new TemplateContainerResolver(template, manager),
        IsEdit = flag,
        Version = str
      });
      source.Id = Guid.Parse(key);
      return this.GetDtoWithContext(source);
    }

    private PageDtoWithContext Model(OperationContext context, string url)
    {
      HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
      bool isEditRequested = currentHttpContext.Request.QueryString["sfaction"] != null;
      string str = currentHttpContext.Request.QueryString["sfversion"];
      PageSiteNode siteNode = this.GetSiteNode(url, isEditRequested);
      if (siteNode == null)
        throw new ItemNotFoundException();
      GeneratorArgs generatorArgs = new GeneratorArgs()
      {
        ContainerResolver = (IContainerResolver) new PageDataContainerResolver(siteNode),
        IsEdit = isEditRequested,
        OriginalPathAndQuery = url,
        Version = str
      };
      SystemManager.CurrentHttpContext.RewritePath("~/" + generatorArgs.OriginalPathAndQuery.TrimStart('/'));
      PageDto addDependentObject;
      if (!isEditRequested)
      {
        string key = "pagemodel";
        if (siteNode.LocalizationStrategy == LocalizationStrategy.Synced)
          key += SystemManager.CurrentContext.Culture.Name;
        addDependentObject = (PageDto) siteNode.FindPersonalizedPageDataProxyOrFallbackToDefault().GetOrAddDependentObject(key, (Func<object>) (() => (object) this.generator.Generate((IGeneratorArgs) generatorArgs)));
      }
      else
        addDependentObject = this.generator.Generate((IGeneratorArgs) generatorArgs);
      addDependentObject.Id = siteNode.Id;
      return this.GetDtoWithContext(addDependentObject);
    }

    private ComponentsResponse LazyComponents(
      OperationContext context,
      string url)
    {
      bool isEditRequested = SystemManager.CurrentHttpContext.Request.QueryString["sfaction"] != null;
      return this.generator.GenerateLazyComponents((IGeneratorArgs) new GeneratorArgs()
      {
        ContainerResolver = (IContainerResolver) new PageDataContainerResolver(this.GetSiteNode(url, isEditRequested) ?? throw new ItemNotFoundException()),
        IsEdit = isEditRequested,
        OriginalPathAndQuery = url
      });
    }

    private PageDtoWithContext GetDtoWithContext(PageDto source)
    {
      PageDtoWithContext dtoWithContext = new PageDtoWithContext(source);
      dtoWithContext.Culture = SystemManager.CurrentContext.Culture.Name;
      dtoWithContext.SiteId = SystemManager.CurrentContext.CurrentSite.Id;
      HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
      dtoWithContext.IsUserAuthenticated = currentHttpContext.User.Identity.IsAuthenticated;
      if (currentHttpContext == null)
        return dtoWithContext;
      string header = currentHttpContext.Request.Headers["X-SFRENDERER-PROXY"];
      if (string.IsNullOrEmpty(header))
        return dtoWithContext;
      currentHttpContext.Response.Headers.Add("X-SFRENDERER-PROXY", header);
      return dtoWithContext;
    }

    private PageSiteNode GetSiteNode(string url, bool isEditRequested)
    {
      Uri result = (Uri) null;
      if (Uri.TryCreate(url, UriKind.Absolute, out result))
        throw new InvalidOperationException("Only relative urls are supported");
      if (url.StartsWith("~"))
        throw new ItemNotFoundException();
      HttpRequestBase request = SystemManager.CurrentHttpContext.Request;
      string components = request.Url.GetComponents(UriComponents.SchemeAndServer, UriFormat.SafeUnescaped);
      if (!url.StartsWith("/"))
        url = "/" + url;
      string str = url;
      url = components + str;
      HttpContext httpContext1 = SystemManager.GetHttpContext(url);
      httpContext1.Request.ContentEncoding = request.ContentEncoding;
      httpContext1.ApplicationInstance = SystemManager.CurrentHttpContext.ApplicationInstance;
      SitefinityRoute sitefinityRoute = ObjectFactory.Resolve<SitefinityRoute>();
      SystemManager.CurrentHttpContext.Items[(object) "IsBackendRequest"] = (object) false;
      HttpContextWrapper httpContext2 = new HttpContextWrapper(httpContext1);
      if (((IEnumerable<string>) request.Headers.AllKeys).Contains<string>("X-SFRENDERER-PROXY"))
        httpContext2.Items.Add((object) "X-SFRENDERER-PROXY", (object) request.Headers["X-SFRENDERER-PROXY"]);
      if (request.RawUrl.Contains("Default.Model"))
        httpContext2.Items.Add((object) "SkipRendererRewriteAndValidation", (object) true);
      RouteData routeData = sitefinityRoute.GetRouteData((HttpContextBase) httpContext2);
      if (routeData == null || routeData.RouteHandler is SiteOfflineRouteHandler)
      {
        PageSiteNode pageSiteNode = httpContext2.Items[(object) "targetNode"] as PageSiteNode;
        return pageSiteNode != null & isEditRequested ? pageSiteNode : throw new ItemNotFoundException();
      }
      if (routeData.Values["Params"] is string[] strArray && strArray.Length != 0)
        throw new ItemNotFoundException();
      if (routeData.RouteHandler is StopRoutingHandler)
      {
        switch (httpContext2.Response.StatusCode)
        {
          case 301:
          case 302:
            return this.GetSiteNode(httpContext2.Response.RedirectLocation, isEditRequested);
          case 401:
            throw new UnauthorizedAccessException();
        }
      }
      if (!(routeData.DataTokens["SiteMapNode"] is PageSiteNode dataToken))
        throw new ItemNotFoundException();
      SystemManager.CurrentHttpContext.Request.RequestContext.RouteData.DataTokens["SiteMapNode"] = (dataToken.Framework == PageTemplateFramework.Mvc ? 1 : (dataToken.IsExternallyRendered() ? 1 : 0)) != 0 ? (object) dataToken : throw new InvalidOperationException("Only Pure Mvc pages and external Renderer pages are supported");
      return dataToken;
    }
  }
}
