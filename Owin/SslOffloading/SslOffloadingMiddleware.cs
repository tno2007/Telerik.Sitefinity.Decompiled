// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Owin.Extensions.SslOffloaderMiddleware
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Microsoft.Owin;
using System.Threading.Tasks;
using System.Web;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Owin.Extensions
{
  internal class SslOffloaderMiddleware : OwinMiddleware
  {
    public SslOffloaderMiddleware(OwinMiddleware next)
      : base(next)
    {
    }

    public override async Task Invoke(IOwinContext context)
    {
      SslOffloaderMiddleware offloaderMiddleware = this;
      if (!context.Request.IsSecure && UrlPath.IsSecuredConnection(context.Get<HttpContextBase>(typeof (HttpContextBase).FullName)))
        context.Request.Scheme = "https";
      await offloaderMiddleware.Next.Invoke(context);
    }
  }
}
