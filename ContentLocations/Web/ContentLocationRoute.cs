// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContentLocations.Web.ContentLocationRoute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web;
using System.Web.Routing;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Security;

namespace Telerik.Sitefinity.ContentLocations.Web
{
  internal class ContentLocationRoute : RouteBase
  {
    internal static string path = "Sitefinity/ContentLocation/Preview";

    public override RouteData GetRouteData(HttpContextBase httpContext)
    {
      if (!(httpContext.Request.AppRelativeCurrentExecutionFilePath.Substring(2) + httpContext.Request.PathInfo).StartsWith(ContentLocationRoute.path, StringComparison.OrdinalIgnoreCase))
        return (RouteData) null;
      if (SecurityManager.IsBackendUser())
        return new RouteData((RouteBase) this, (IRouteHandler) ObjectFactory.Resolve<ContentLocationRouteHandler>());
      SecurityManager.RedirectToLogin(httpContext);
      return (RouteData) null;
    }

    public override VirtualPathData GetVirtualPath(
      RequestContext requestContext,
      RouteValueDictionary values)
    {
      throw new NotImplementedException();
    }
  }
}
