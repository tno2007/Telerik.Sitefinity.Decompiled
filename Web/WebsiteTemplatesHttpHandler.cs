// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.WebsiteTemplatesHttpHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.IO;
using System.Web;
using System.Web.Routing;

namespace Telerik.Sitefinity.Web
{
  public class WebsiteTemplatesHttpHandler : IHttpHandler
  {
    private RequestContext requestContext;

    public WebsiteTemplatesHttpHandler(RequestContext requestContext) => this.requestContext = requestContext;

    public bool IsReusable => false;

    public void ProcessRequest(HttpContext context)
    {
      string str = context.Server.MapPath("~/App_Data/Sitefinity/WebsiteTemplates/" + this.requestContext.RouteData.Values["Params"]);
      string s = (string) null;
      if (!File.Exists(str))
        throw new HttpException(404, "File not found");
      switch (str.Substring(str.LastIndexOf(".")).ToLower())
      {
        case ".css":
          context.Response.ContentType = "text/css";
          break;
        case ".gif":
          context.Response.ContentType = "image/gif";
          break;
        case ".jpeg":
        case ".jpg":
          context.Response.ContentType = "image/jpeg";
          break;
        case ".js":
          context.Response.ContentType = "application/x-javascript";
          break;
        case ".less":
          context.Response.ContentType = "text/css";
          s = ThemeHttpHandler.CompileLessFile(str);
          break;
        case ".png":
          context.Response.ContentType = "image/png";
          break;
        default:
          context.Response.ContentType = MimeMapping.GetMimeMapping(str);
          break;
      }
      if (!RouteHelper.SetFileCacheability(context, str))
        return;
      if (s != null)
        context.Response.Write(s);
      else
        context.Response.WriteFile(str);
    }
  }
}
