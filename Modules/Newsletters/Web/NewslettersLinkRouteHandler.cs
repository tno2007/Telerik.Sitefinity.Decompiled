// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.NewslettersLinkRouteHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web;
using System.Web.Routing;

namespace Telerik.Sitefinity.Modules.Newsletters.Web
{
  /// <summary>
  /// Represents a route handler for all the links that are inserted in newsletters.
  /// </summary>
  public class NewslettersLinkRouteHandler : IRouteHandler
  {
    internal static Guid linkPrefix = new Guid("AA88EE3C-D13D-4751-BA3F-7538ECC6B2CA");

    /// <summary>Provides the object that processes the request.</summary>
    /// <param name="requestContext">An object that encapsulates information about the request.</param>
    /// <returns>An object that processes the request.</returns>
    public IHttpHandler GetHttpHandler(RequestContext requestContext) => (IHttpHandler) new NewslettersLinkHttpHandler();
  }
}
