// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.LogHttpHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.IO;
using System.Web;
using System.Web.Routing;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Security.Claims;

namespace Telerik.Sitefinity.Web
{
  /// <summary>
  /// Http handler that is used to read logs from ~/App_Data/Sitefinity/Log
  /// </summary>
  public class LogHttpHandler : IHttpHandler
  {
    private readonly RequestContext requestContext;

    public LogHttpHandler(RequestContext requestContext) => this.requestContext = requestContext;

    /// <summary>Gets the is reusable.</summary>
    /// <value>The is reusable.</value>
    public bool IsReusable => false;

    /// <summary>Processes the request.</summary>
    /// <param name="context">The context.</param>
    public void ProcessRequest(HttpContext context)
    {
      if (!ClaimsManager.IsUnrestricted())
        throw new HttpException(401, "Access Denied");
      string str1 = Log.MapLogFilePath(this.requestContext.RouteData.Values["Params"].ToString());
      string str2 = File.Exists(str1) ? VirtualPathUtility.GetExtension(str1) : throw new HttpException(404, "File not found");
      if (!(str2 == ".log"))
      {
        if (!(str2 == ".htm") && !(str2 == ".html"))
          throw new HttpException(400, "Bad request");
        context.Response.ContentType = "text/html";
      }
      else
        context.Response.ContentType = "text/plain";
      context.Response.WriteFile(str1);
      context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
    }
  }
}
