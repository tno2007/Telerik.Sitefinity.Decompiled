// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.ThemeHttpHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.IO;
using System.Web;
using System.Web.Routing;
using Telerik.Sitefinity.Abstractions;

namespace Telerik.Sitefinity.Web
{
  public class ThemeHttpHandler : IHttpHandler
  {
    private RequestContext requestContext;

    public ThemeHttpHandler(RequestContext requestContext) => this.requestContext = requestContext;

    public bool IsReusable => false;

    public void ProcessRequest(HttpContext context)
    {
      string str1 = context.Request.Url.ToString();
      if (str1.Contains("App_Theme") && str1.Contains(".less") && ObjectFactory.IsTypeRegistered<ILessCompiler>())
      {
        string str2 = context.Server.MapPath("~/App_Themes/" + this.requestContext.RouteData.Values["ThemeName"] + "/" + this.requestContext.RouteData.Values["resourceName"] + ".less");
        string s = File.Exists(str2) ? ThemeHttpHandler.CompileLessFile(str2) : throw new HttpException(404, "File not found");
        context.Response.ContentType = "text/css";
        if (!RouteHelper.SetFileCacheability(context, str2))
          return;
        context.Response.Write(s);
      }
      else
      {
        string str3 = context.Server.MapPath("~/App_Data/Sitefinity/Themes/" + this.requestContext.RouteData.Values["Params"]);
        if (!File.Exists(str3))
          throw new HttpException(404, "File not found");
        switch (str3.Substring(str3.LastIndexOf(".")).ToLower())
        {
          case ".css":
          case ".less":
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
          case ".png":
            context.Response.ContentType = "image/png";
            break;
        }
        if (!RouteHelper.SetFileCacheability(context, str3))
          return;
        context.Response.WriteFile(str3);
      }
    }

    internal static string CompileLessFile(string filePath) => ObjectFactory.IsTypeRegistered<ILessCompiler>() ? ObjectFactory.Resolve<ILessCompiler>().CompileFile(filePath, (LessCompilerSettings) null) : (string) null;
  }
}
