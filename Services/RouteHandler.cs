// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.RouteHandler`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web;
using System.Web.Routing;
using Telerik.Sitefinity.Abstractions;

namespace Telerik.Sitefinity.Services
{
  /// <summary>
  /// 
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class RouteHandler<T> : IRouteHandler where T : IHttpHandler
  {
    private IHttpHandler handler;

    /// <summary>Provides the object that processes the request.</summary>
    /// <param name="requestContext">An object that encapsulates information about the request.</param>
    /// <returns>An object that processes the request.</returns>
    public virtual IHttpHandler GetHttpHandler(RequestContext requestContext)
    {
      if (this.handler != null)
        return this.handler;
      T httpHandler = ObjectFactory.Resolve<T>();
      if (httpHandler.IsReusable)
        this.handler = (IHttpHandler) httpHandler;
      return (IHttpHandler) httpHandler;
    }
  }
}
