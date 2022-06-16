// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Owin.HttpsRedirectMiddleware
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Microsoft.Owin;
using System.Threading.Tasks;
using System.Web;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Owin
{
  /// <summary>
  /// Middleware that redirects from http to https if required.
  /// </summary>
  internal class HttpsRedirectMiddleware : OwinMiddleware
  {
    public HttpsRedirectMiddleware(OwinMiddleware next)
      : base(next)
    {
    }

    public override async Task Invoke(IOwinContext context)
    {
      HttpsRedirectMiddleware redirectMiddleware = this;
      if (Bootstrapper.IsReady && !context.Request.IsSecure && Config.Get<SecurityConfig>().RequireHttpsForAllRequests)
        context.Response.Redirect("https://" + UrlPath.ResolveAbsoluteUrl(redirectMiddleware.GetHttpContext(context.Request).Request.RawUrl).Substring("http://".Length));
      else
        await redirectMiddleware.Next.Invoke(context);
    }

    private HttpContextBase GetHttpContext(IOwinRequest request) => request.Get<HttpContextBase>(typeof (HttpContextBase).FullName);
  }
}
