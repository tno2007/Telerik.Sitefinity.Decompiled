// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.GlobalPermissionsPanel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web.Routing;
using System.Web.UI;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.GenericContent.Web.UI;
using Telerik.Sitefinity.Modules.Security.Permissions.Web.UI.Views;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI.Backend;

namespace Telerik.Sitefinity.Security.Web.UI
{
  /// <summary>
  /// A panel for displaying Global Permissions, based on the GlobalPermissionsView control
  /// </summary>
  public class GlobalPermissionsPanel : 
    ProviderControlPanel<Page>,
    IPartialRouteHandler,
    IGenericContentViewHost
  {
    private const string PermissionsName = "Permissions";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.Web.UI.GlobalPermissionsPanel" /> class.
    /// </summary>
    public GlobalPermissionsPanel()
      : base(false)
    {
      this.ResourceClassId = typeof (SecurityResources).Name;
      this.Title = Res.Get<SecurityResources>().GlobalPermissionsTitle;
    }

    /// <summary>Loads configured views.</summary>
    protected override void CreateViews() => this.AddView(ObjectFactory.Resolve<GlobalPermissionsView>().GetType(), "GlobalPermissions", Res.Get<SecurityResources>().GlobalPermissionsTitle, string.Empty, string.Empty);

    /// <summary>Gets or sets the Partial Request Context</summary>
    public PartialRequestContext PartialRequestContext { get; set; }

    /// <summary>Gets or sets the Parent Route Handler</summary>
    public IPartialRouteHandler ParentRouteHandler { get; set; }

    /// <summary>
    /// Gets a collection of objects that derive from the
    /// <see cref="T:System.Web.Routing.RouteBase" /> class.
    /// </summary>
    /// <returns>
    /// An object that contains all the routes in the collection.
    /// </returns>
    public RouteCollection CreateRoutes() => new RouteCollection()
    {
      {
        "Permissions",
        (RouteBase) new Route("Permissions/{*Params}", (IRouteHandler) new RouteHandlerBase("Permissions"))
      }
    };

    /// <summary>Registers child partial route handlers.</summary>
    /// <param name="list">A list of <see cref="T:Telerik.Sitefinity.Web.RouteInfo" /> objects.</param>
    public void RegisterChildRouteHandlers(IList<RouteInfo> list)
    {
    }

    /// <summary>
    /// Retrieves the type of content that should be edited by generic content-item views
    /// </summary>
    Type IGenericContentViewHost.ContentItemType => typeof (Permission);

    /// <summary>
    /// Retrieves the name of the content provider to use for data operations
    /// </summary>
    string IGenericContentViewHost.ProviderName => string.Empty;
  }
}
