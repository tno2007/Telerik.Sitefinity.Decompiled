// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContextualHelp.Http.ContextualHelpRouteHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web;
using System.Web.Routing;

namespace Telerik.Sitefinity.ContextualHelp.Http
{
  /// <summary>Handler for the contextual help route.</summary>
  /// <seealso cref="T:System.Web.Routing.IRouteHandler" />
  internal class ContextualHelpRouteHandler : IRouteHandler
  {
    private readonly ContextualHelpHttpHandler httpHandler;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.ContextualHelp.Http.ContextualHelpRouteHandler" /> class.
    /// </summary>
    public ContextualHelpRouteHandler() => this.httpHandler = new ContextualHelpHttpHandler();

    /// <summary>Provides the object that processes the request.</summary>
    /// <param name="requestContext">An object that encapsulates information about the request.</param>
    /// <returns>An object that processes the request.</returns>
    public IHttpHandler GetHttpHandler(RequestContext requestContext) => (IHttpHandler) this.httpHandler;
  }
}
