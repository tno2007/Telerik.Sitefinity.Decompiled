// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.ResourceRouteHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web;
using System.Web.Routing;

namespace Telerik.Sitefinity.Web
{
  /// <summary>Route handler to return ResourceHttpHandler</summary>
  public class ResourceRouteHandler : IRouteHandler
  {
    internal const string ResourceNameKey = "resourceName";

    public IHttpHandler GetHttpHandler(RequestContext requestContext)
    {
      if (requestContext.RouteData.Values["resourceName"] != null)
      {
        string str = requestContext.RouteData.Values["resourceName"].ToString();
        requestContext.HttpContext.Items.Add((object) "resourceName", (object) str);
      }
      return (IHttpHandler) new ResourceHttpHandler();
    }
  }
}
