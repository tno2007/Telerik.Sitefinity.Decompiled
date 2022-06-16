// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Web.FormsRoute
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
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Modules.Forms.Web
{
  /// <summary>Represents a template route.</summary>
  public class FormsRoute : RouteBase
  {
    private string path = "Sitefinity" + "/Forms/";
    private FormsRouteHandler formsRouteHandler;

    /// <summary>
    /// When overridden in a derived class, returns route information about the request.
    /// </summary>
    /// <param name="httpContext">An object that encapsulates information about the HTTP request.</param>
    /// <returns>
    /// An object that contains the values from the route definition if the route matches the current request, or null if the route does not match the request.
    /// </returns>
    public override RouteData GetRouteData(HttpContextBase httpContext)
    {
      string str = httpContext.Request.AppRelativeCurrentExecutionFilePath.Substring(2) + httpContext.Request.PathInfo;
      if (!str.StartsWith(this.path, StringComparison.OrdinalIgnoreCase))
        return (RouteData) null;
      if (!SecurityManager.IsBackendUser())
      {
        SecurityManager.RedirectToLogin(httpContext);
        return (RouteData) null;
      }
      RouteHelper.ApplyThreadCulturesForCurrentUser();
      SiteMapProvider currentProvider = SitefinitySiteMap.GetCurrentProvider();
      httpContext.Items[(object) "SF_SiteMap"] = (object) currentProvider;
      IList<string> pathSegmentStrings = RouteHelper.SplitUrlToPathSegmentStrings(str.Substring(this.path.Length), true);
      CultureInfo culture = (CultureInfo) null;
      if (SystemManager.CurrentContext.AppSettings.Multilingual && pathSegmentStrings.Count > 1)
      {
        string name = pathSegmentStrings[pathSegmentStrings.Count - 1];
        try
        {
          culture = new CultureInfo(name);
          pathSegmentStrings.RemoveAt(pathSegmentStrings.Count - 1);
        }
        catch (CultureNotFoundException ex)
        {
        }
      }
      bool isPreview;
      string defaultProviderName;
      string formName;
      switch (pathSegmentStrings.Count)
      {
        case 1:
          isPreview = false;
          defaultProviderName = ManagerBase<FormsDataProvider>.GetDefaultProviderName();
          formName = pathSegmentStrings[0];
          break;
        case 2:
          if ("Preview".Equals(pathSegmentStrings[1], StringComparison.OrdinalIgnoreCase))
          {
            isPreview = true;
            defaultProviderName = ManagerBase<FormsDataProvider>.GetDefaultProviderName();
            formName = pathSegmentStrings[0];
            break;
          }
          isPreview = false;
          defaultProviderName = pathSegmentStrings[0];
          formName = pathSegmentStrings[1];
          break;
        case 3:
          if (!"Preview".Equals(pathSegmentStrings[2], StringComparison.OrdinalIgnoreCase))
            return (RouteData) null;
          isPreview = true;
          defaultProviderName = pathSegmentStrings[0];
          formName = pathSegmentStrings[1];
          break;
        default:
          return (RouteData) null;
      }
      FormsSiteNode node = new FormsSiteNode((SiteMapBase) currentProvider, defaultProviderName, formName);
      return this.GetFormsEditorHandler(pathSegmentStrings.ToArray<string>(), (SiteMapNode) node, isPreview, culture);
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

    private RouteData GetFormsEditorHandler(
      string[] parameters,
      SiteMapNode node,
      bool isPreview,
      CultureInfo culture)
    {
      if (this.formsRouteHandler == null)
        this.formsRouteHandler = ObjectFactory.Resolve<FormsRouteHandler>();
      this.formsRouteHandler.ObjectEditCulture = culture;
      this.formsRouteHandler.IsPreview = isPreview;
      return new RouteData((RouteBase) this, (IRouteHandler) this.formsRouteHandler)
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
