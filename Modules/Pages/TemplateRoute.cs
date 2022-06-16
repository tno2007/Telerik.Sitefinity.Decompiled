// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.TemplateRoute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Routing;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Renderer.Editor.Specifics;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UrlEvaluation;

namespace Telerik.Sitefinity.Modules.Pages
{
  /// <summary>Represents a template route.</summary>
  public class TemplateRoute : RouteBase
  {
    private string path = "Sitefinity" + "/Template/";
    private TemplateEditorRouteHandler templateEditorHandler;
    private TemplateEditorRouteHandler templatePreviewHandler;

    /// <summary>
    /// When overridden in a derived class, returns route information about the request.
    /// </summary>
    /// <param name="httpContext">An object that encapsulates information about the HTTP request.</param>
    /// <returns>
    /// An object that contains the values from the route definition if the route matches the current request, or null if the route does not match the request.
    /// </returns>
    public override RouteData GetRouteData(HttpContextBase httpContext)
    {
      string str1 = httpContext.Request.AppRelativeCurrentExecutionFilePath.Substring(2) + httpContext.Request.PathInfo;
      if (str1.StartsWith(this.path, StringComparison.OrdinalIgnoreCase))
      {
        if (!SecurityManager.IsBackendUser())
        {
          SecurityManager.RedirectToLogin(httpContext);
          return (RouteData) null;
        }
        SiteMapProvider currentProvider = SitefinitySiteMap.GetCurrentProvider();
        httpContext.Items[(object) "SF_SiteMap"] = (object) currentProvider;
        httpContext.Items[(object) "IsTemplate"] = (object) true;
        IList<string> pathSegmentStrings = RouteHelper.SplitUrlToPathSegmentStrings(str1.Substring(this.path.Length), true);
        if (pathSegmentStrings.Count > 0 && ControlUtilities.IsGuid(pathSegmentStrings[0]))
        {
          Guid id = new Guid(pathSegmentStrings[0]);
          TemplateSiteNode node = new TemplateSiteNode((SiteMapBase) currentProvider, new Guid(pathSegmentStrings[0]));
          CultureInfo culture = (CultureInfo) null;
          bool isPreview;
          if (pathSegmentStrings.Count > 1 && "Preview".Equals(pathSegmentStrings[1], StringComparison.OrdinalIgnoreCase))
          {
            isPreview = true;
            if (pathSegmentStrings.Count == 3)
              culture = CultureInfo.GetCultureInfo(pathSegmentStrings[2]);
          }
          else
          {
            isPreview = false;
            if (pathSegmentStrings.Count == 2)
              culture = CultureInfo.GetCultureInfo(pathSegmentStrings[1]);
          }
          PageManager manager = PageManager.GetManager();
          PageTemplate template = manager.GetTemplate(id);
          IRendererCommonData lastContainer = ((IControlsContainer) manager.TemplatesLifecycle.GetMaster(template) ?? (IControlsContainer) template).GetLastContainer<IRendererCommonData>();
          if (lastContainer.IsExternallyRendered())
          {
            if (!UrlPath.IsKnownProxy(httpContext))
            {
              if (httpContext.IsDebuggingEnabled || httpContext.User.Identity.IsAuthenticated & isPreview)
                return new RouteData((RouteBase) this, (IRouteHandler) new SiteOfflineRouteHandler(Res.Get<Labels>().CannotProcessExternalRendererPage.Arrange((object) lastContainer.Renderer))
                {
                  Title = "Cannot process page"
                });
              httpContext.Response.StatusCode = 404;
              return new RouteData((RouteBase) this, (IRouteHandler) new StopRoutingHandler());
            }
            string header = httpContext.Request.Headers["X-SF-WEBSERVICEPATH"];
            if (!string.IsNullOrEmpty(header))
            {
              string str2 = header.Trim('/');
              httpContext.Request.Url.GetComponents(UriComponents.Path, UriFormat.Unescaped);
              string components = httpContext.Request.Url.GetComponents(UriComponents.Query, UriFormat.Unescaped);
              string str3 = string.Format("~/{0}/templates({1})/Default.Model()", (object) str2, (object) id);
              QueryStringBuilder collection = new QueryStringBuilder(components);
              if (culture != null)
                collection.Add("sf_culture", culture.Name);
              if (!collection.Contains("sfaction"))
              {
                string str4 = !isPreview ? "Edit" : "Preview";
                collection.Add("sfaction", str4.ToLowerInvariant());
              }
              if (!collection.Contains("sf_site"))
                collection.Add("sf_site", SystemManager.CurrentContext.CurrentSite.Id.ToString());
              string path = str3 + collection.ToQueryString();
              httpContext.Server.TransferRequest(path, true, (string) null, httpContext.Request.Headers, false);
              return new RouteData((RouteBase) this, (IRouteHandler) new StopRoutingHandler());
            }
          }
          return this.GetTemplateEditorHandler(pathSegmentStrings.ToArray<string>(), (SiteMapNode) node, isPreview, culture);
        }
      }
      return (RouteData) null;
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

    private RouteData GetTemplateEditorHandler(
      string[] parameters,
      SiteMapNode node,
      bool isPreview,
      CultureInfo culture)
    {
      TemplateEditorRouteHandler editorRouteHandler;
      if (isPreview)
      {
        if (this.templatePreviewHandler == null)
        {
          this.templatePreviewHandler = ObjectFactory.Resolve<TemplateEditorRouteHandler>();
          this.templatePreviewHandler.IsPreview = true;
        }
        this.templatePreviewHandler.ObjectEditCulture = culture;
        editorRouteHandler = this.templatePreviewHandler;
      }
      else
      {
        if (this.templateEditorHandler == null)
        {
          this.templateEditorHandler = ObjectFactory.Resolve<TemplateEditorRouteHandler>();
          this.templateEditorHandler.IsPreview = false;
        }
        this.templateEditorHandler.ObjectEditCulture = culture;
        editorRouteHandler = this.templateEditorHandler;
      }
      return new RouteData((RouteBase) this, (IRouteHandler) editorRouteHandler)
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
  }
}
