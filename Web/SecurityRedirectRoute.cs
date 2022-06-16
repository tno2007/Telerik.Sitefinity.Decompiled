// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.SecurityRedirectRoute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web;
using System.Web.Routing;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;

namespace Telerik.Sitefinity.Web
{
  /// <summary>
  /// This route disables by default access to sitefinity backend route handlers for non-authenticated and non-backend users.
  /// </summary>
  /// <remarks>
  /// The web services in Sitefinity/Services are excluded from this check,.
  /// </remarks>
  public class SecurityRedirectRoute : RouteBase
  {
    private readonly string backendRootDirectory = "Sitefinity" + "/";
    private readonly string backendServicesRootDirectory = "Sitefinity" + "/Services/";

    /// <summary>
    /// When overridden in a derived class, returns route information about the request.
    /// </summary>
    /// <param name="httpContext">An object that encapsulates information about the HTTP request.</param>
    /// <returns>
    /// An object that contains the values from the route definition if the route matches the current request,
    /// or null if the route does not match the request.
    /// </returns>
    public override RouteData GetRouteData(HttpContextBase httpContext)
    {
      string virtualPath = VirtualPathUtility.AppendTrailingSlash(httpContext.Request.AppRelativeCurrentExecutionFilePath.Substring(2) + httpContext.Request.PathInfo);
      if (virtualPath.StartsWith(this.backendRootDirectory, StringComparison.OrdinalIgnoreCase))
      {
        if (ProtectedRoute.IsBackendUnprotected(virtualPath))
        {
          httpContext.Items[(object) "SuppressAuthenticationRequirement"] = (object) true;
          return (RouteData) null;
        }
        BackendRestriction.Current.EndRequestIfForbidden(httpContext);
        if (virtualPath.StartsWith(this.backendServicesRootDirectory, StringComparison.OrdinalIgnoreCase))
          return (RouteData) null;
        if (!SecurityManager.IsBackendUser())
          SecurityManager.RedirectToLogin(httpContext);
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
      return new VirtualPathData((RouteBase) this, string.Empty);
    }
  }
}
