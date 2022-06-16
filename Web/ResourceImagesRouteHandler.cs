// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.ResourceImagesRouteHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web;
using System.Web.Routing;

namespace Telerik.Sitefinity.Web
{
  public class ResourceImagesRouteHandler : IRouteHandler
  {
    public const string AssemblyNameKey = "assemblyName";
    public const string ResourceNameKey = "resourceName";

    public IHttpHandler GetHttpHandler(RequestContext requestContext)
    {
      if (requestContext.RouteData.Values["assemblyName"] != null && requestContext.RouteData.Values["resourceName"] != null)
      {
        string str1 = requestContext.RouteData.Values["assemblyName"].ToString();
        requestContext.HttpContext.Items.Add((object) "assemblyName", (object) str1);
        string str2 = requestContext.RouteData.Values["resourceName"].ToString();
        requestContext.HttpContext.Items.Add((object) "resourceName", (object) str2);
      }
      return (IHttpHandler) new ResourceImagesHttpHandler();
    }
  }
}
