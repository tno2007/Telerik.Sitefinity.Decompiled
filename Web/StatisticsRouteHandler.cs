// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.StatisticsRouteHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web;
using System.Web.Routing;
using Telerik.Sitefinity.Abstractions;

namespace Telerik.Sitefinity.Web
{
  /// <summary>Used when processing web visits counters</summary>
  internal class StatisticsRouteHandler : IRouteHandler
  {
    private IHttpHandler handler;

    public virtual IHttpHandler GetHttpHandler(RequestContext requestContext)
    {
      if (this.handler != null)
        return this.handler;
      StatisticsHttpHandler httpHandler = ObjectFactory.Resolve<StatisticsHttpHandler>();
      if (httpHandler.IsReusable)
        this.handler = (IHttpHandler) httpHandler;
      return (IHttpHandler) httpHandler;
    }
  }
}
