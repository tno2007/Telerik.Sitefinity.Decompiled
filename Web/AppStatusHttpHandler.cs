// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.AppStatusHttpHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Security;

namespace Telerik.Sitefinity.Web
{
  /// <summary>AppStatus http handler.</summary>
  public class AppStatusHttpHandler : IHttpHandler
  {
    internal const string ReturnUrlQueryStringKey = "ReturnUrl";

    /// <summary>
    /// Gets a value indicating whether the http handler is reusable.
    /// </summary>
    public bool IsReusable => false;

    /// <summary>
    /// Process the http request - redirect to return URL, if any, otherwise to homepage.
    /// </summary>
    /// <param name="context">The http context.</param>
    public void ProcessRequest(HttpContext context)
    {
      string str = context.Request.QueryStringGet("ReturnUrl");
      context.Response.CacheControl = "no-cache";
      if (!string.IsNullOrEmpty(str))
      {
        if (ObjectFactory.Resolve<IRedirectUriValidator>().IsValid(str))
          context.Response.Redirect(str);
        else
          context.Response.StatusCode = 400;
      }
      else
        context.Response.Redirect("/");
      context.ApplicationInstance.CompleteRequest();
    }
  }
}
