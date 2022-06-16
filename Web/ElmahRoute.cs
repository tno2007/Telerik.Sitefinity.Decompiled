// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.ElmahRoute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web;
using System.Web.Routing;
using Telerik.Sitefinity.Security.Claims;

namespace Telerik.Sitefinity.Web
{
  public class ElmahRoute : RouteBase
  {
    /// <summary>
    /// When overridden in a derived class, returns route information about the request.
    /// </summary>
    /// <param name="httpContext">An object that encapsulates information about the HTTP request.</param>
    /// <returns>
    /// An object that contains the values from the route definition if the route matches the current request, or null if the route does not match the request.
    /// </returns>
    public override RouteData GetRouteData(HttpContextBase httpContext)
    {
      if (!httpContext.Request.FilePath.ToLower().EndsWith("/elmah.axd"))
        return (RouteData) null;
      if (ClaimsManager.GetCurrentIdentity().IsUnrestricted)
        return new RouteData((RouteBase) this, (IRouteHandler) new StopRoutingHandler());
      throw new HttpException(403, "Access denied!");
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
      return (VirtualPathData) null;
    }
  }
}
