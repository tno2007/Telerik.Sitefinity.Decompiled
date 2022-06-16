// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.SpellCheckRouteHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web;
using System.Web.Routing;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web
{
  /// <summary>
  ///  Forward processing of all requests that matching route pattern to <see cref="T:Telerik.Sitefinity.Web.SpellCheckRouteHandler" />.
  /// </summary>
  public class SpellCheckRouteHandler : IRouteHandler
  {
    /// <summary>Gets the HTTP handler.</summary>
    /// <param name="requestContext">The request context.</param>
    /// <returns></returns>
    public IHttpHandler GetHttpHandler(RequestContext requestContext) => (IHttpHandler) new SpellCheckHandler();
  }
}
