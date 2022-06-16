// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.PartialRequestContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web;
using System.Web.Routing;

namespace Telerik.Sitefinity.Web
{
  /// <summary>
  /// Encapsulates information about an HTTP request that matches a defined route.
  /// </summary>
  public sealed class PartialRequestContext : RequestContext
  {
    internal PartialRequestContext(
      PartialHttpContext httpContext,
      RouteData routeData,
      string pathKey)
      : base((HttpContextBase) httpContext, routeData)
    {
      this.PathKey = pathKey;
    }

    internal string PathKey { get; private set; }

    /// <summary>
    /// Gets the name of the route handler if the hadler inherits from <see cref="!:SitefinityRouteHandler" />.
    /// </summary>
    public string RouteHandlerName => this.RouteData.RouteHandler is RouteHandlerBase routeHandler ? routeHandler.Name : (string) null;
  }
}
