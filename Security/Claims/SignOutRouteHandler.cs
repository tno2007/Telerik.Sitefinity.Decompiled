// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Claims.SignOutRouteHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web;
using System.Web.Routing;

namespace Telerik.Sitefinity.Security.Claims
{
  /// <summary>
  /// Handles routing for sing out and passing parameters to the http handler
  /// </summary>
  internal class SignOutRouteHandler : IRouteHandler
  {
    internal const string SignOutMethod = "method";

    /// <summary>Provides the object that processes the request.</summary>
    /// <param name="requestContext">An object that encapsulates information about the
    /// request.</param>
    /// <returns>An object that processes the request.</returns>
    public IHttpHandler GetHttpHandler(RequestContext requestContext)
    {
      string str = requestContext.RouteData.Values["method"] as string;
      if (!string.IsNullOrWhiteSpace(str))
        requestContext.HttpContext.Items.Add((object) "method", (object) str);
      return (IHttpHandler) new SignOutHttpHandler();
    }
  }
}
