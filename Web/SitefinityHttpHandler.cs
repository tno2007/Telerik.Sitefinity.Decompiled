// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.SitefinityHttpHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web;
using System.Web.Routing;
using Telerik.Sitefinity.Abstractions;

namespace Telerik.Sitefinity.Web
{
  /// <summary>Verifies and processes an HTTP request.</summary>
  public class SitefinityHttpHandler : UrlRoutingHandler
  {
    /// <summary>
    /// Initializes the <see cref="T:Telerik.Sitefinity.Web.SitefinityHttpHandler" /> class.
    /// </summary>
    static SitefinityHttpHandler() => Bootstrapper.Bootstrap();

    /// <summary>Verifies and processes an HTTP request.</summary>
    /// <param name="httpHandler">The HTTP handler.</param>
    /// <param name="httpContext">The HTTP context.</param>
    protected override void VerifyAndProcessRequest(
      IHttpHandler httpHandler,
      HttpContextBase httpContext)
    {
      if (httpHandler == null)
        throw new ArgumentNullException(nameof (httpHandler));
      httpHandler.ProcessRequest(HttpContext.Current);
    }
  }
}
