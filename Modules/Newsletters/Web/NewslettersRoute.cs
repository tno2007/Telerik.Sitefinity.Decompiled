// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.NewslettersRoute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Modules.Newsletters.Web
{
  /// <summary>Represents a route for newsletter campaigns.</summary>
  public class NewslettersRoute : RouteBase
  {
    private string path = "Sitefinity/SFNwslttrs/";
    private NewslettersRouteHandler editorHandler;
    private NewslettersRouteHandler previewHandler;

    /// <summary>
    /// When overridden in a derived class, returns route information about the request.
    /// </summary>
    /// <param name="httpContext">An object that encapsulates information about the HTTP request.</param>
    /// <returns>
    /// An object that contains the values from the route definition if the route matches the current request, or null if the route does not match the request.
    /// </returns>
    public override RouteData GetRouteData(HttpContextBase httpContext)
    {
      string virtualPath = httpContext.Request.AppRelativeCurrentExecutionFilePath.Substring(2) + httpContext.Request.PathInfo;
      if (!virtualPath.StartsWith(this.path, StringComparison.OrdinalIgnoreCase))
        return (RouteData) null;
      SiteMapProvider currentProvider = SitefinitySiteMap.GetCurrentProvider();
      httpContext.Items[(object) "SF_SiteMap"] = (object) currentProvider;
      IList<string> pathSegmentStrings = RouteHelper.SplitUrlToPathSegmentStrings(virtualPath.Substring(this.path.Length), true);
      bool isPreview;
      string newslettersProviderName;
      string campaignId;
      switch (pathSegmentStrings.Count)
      {
        case 1:
          isPreview = false;
          newslettersProviderName = string.Empty;
          campaignId = pathSegmentStrings[0];
          break;
        case 2:
          if ("Preview".Equals(pathSegmentStrings[1], StringComparison.OrdinalIgnoreCase))
          {
            isPreview = true;
            newslettersProviderName = ManagerBase<NewslettersDataProvider>.GetDefaultProviderName();
            campaignId = pathSegmentStrings[0];
            break;
          }
          if ("View".Equals(pathSegmentStrings[0], StringComparison.OrdinalIgnoreCase))
          {
            isPreview = true;
            newslettersProviderName = ManagerBase<NewslettersDataProvider>.GetDefaultProviderName();
            campaignId = pathSegmentStrings[1];
            break;
          }
          isPreview = false;
          newslettersProviderName = pathSegmentStrings[0];
          campaignId = pathSegmentStrings[1];
          break;
        case 3:
          if (!"Preview".Equals(pathSegmentStrings[2], StringComparison.OrdinalIgnoreCase))
            return (RouteData) null;
          isPreview = true;
          newslettersProviderName = ManagerBase<NewslettersDataProvider>.GetDefaultProviderName();
          campaignId = pathSegmentStrings[0];
          break;
        default:
          return (RouteData) null;
      }
      this.ProcessLanguage(virtualPath);
      NewslettersSiteNode node = new NewslettersSiteNode((SiteMapBase) currentProvider, newslettersProviderName, campaignId);
      return this.GetNewslettersEditorHandler(pathSegmentStrings.ToArray<string>(), (SiteMapNode) node, isPreview);
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

    private RouteData GetNewslettersEditorHandler(
      string[] parameters,
      SiteMapNode node,
      bool isPreview)
    {
      NewslettersRouteHandler newslettersRouteHandler;
      if (isPreview)
      {
        if (this.previewHandler == null)
        {
          this.previewHandler = ObjectFactory.Resolve<NewslettersRouteHandler>();
          this.previewHandler.IsPreview = true;
        }
        newslettersRouteHandler = this.previewHandler;
      }
      else
      {
        if (this.editorHandler == null)
        {
          this.editorHandler = ObjectFactory.Resolve<NewslettersRouteHandler>();
          this.editorHandler.IsPreview = false;
        }
        newslettersRouteHandler = this.editorHandler;
      }
      return new RouteData((RouteBase) this, (IRouteHandler) newslettersRouteHandler)
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

    protected virtual void ProcessLanguage(string virtualPath) => RouteHelper.ApplyThreadCulturesForCurrentUser();
  }
}
