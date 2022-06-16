// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Licensing.LicensingRoute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web;
using System.Web.Hosting;
using System.Web.Routing;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI.Backend;

namespace Telerik.Sitefinity.Licensing
{
  internal class LicensingRoute : RouteBase
  {
    public override RouteData GetRouteData(HttpContextBase httpContext)
    {
      string redirectKey = "Licensing?ReturnUrl=" + HttpUtility.UrlEncode(SystemManager.CurrentHttpContext.Request.RawUrl);
      if (LicensingRoute.IsAppStatusRequest(httpContext))
        return (RouteData) null;
      return LicenseState.Current.InvalidLicense ? new RouteData((RouteBase) this, (IRouteHandler) new BackendRedirectRouteHandler(redirectKey, string.Empty, false)) : (RouteData) null;
    }

    public override VirtualPathData GetVirtualPath(
      RequestContext requestContext,
      RouteValueDictionary values)
    {
      throw new NotImplementedException();
    }

    private static bool IsAppStatusRequest(HttpContextBase httpContext) => string.Equals((HostingEnvironment.ApplicationVirtualPath.TrimEnd('/') ?? string.Empty) + "/appstatus", httpContext.Request.Url.AbsolutePath, StringComparison.InvariantCultureIgnoreCase);
  }
}
