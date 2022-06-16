// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.IPartialRouteHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.Routing;

namespace Telerik.Sitefinity.Web
{
  /// <summary>
  /// Defines the contract that a class must implement to process a
  /// request for a matching partial route pattern.
  /// </summary>
  public interface IPartialRouteHandler
  {
    /// <summary>Gets the name of this handler.</summary>
    string Name { get; }

    /// <summary>
    /// An object that encapsulates information about the requested route.
    /// </summary>
    PartialRequestContext PartialRequestContext { get; set; }

    /// <summary>Gets or sets the parent route handler.</summary>
    IPartialRouteHandler ParentRouteHandler { get; set; }

    /// <summary>
    /// Creates a collection of objects that derive from the
    /// <see cref="T:System.Web.Routing.RouteBase" /> class.
    /// </summary>
    /// <returns>
    /// An object that contains all the routes in the collection.
    /// </returns>
    RouteCollection CreateRoutes();

    /// <summary>Registers child partial route handlers.</summary>
    /// <param name="list">A list of <see cref="T:Telerik.Sitefinity.Web.RouteInfo" /> objects.</param>
    void RegisterChildRouteHandlers(IList<RouteInfo> list);
  }
}
