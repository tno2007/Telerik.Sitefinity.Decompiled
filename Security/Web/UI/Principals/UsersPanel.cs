// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.Principals.UsersPanel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.Routing;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI.Backend;

namespace Telerik.Sitefinity.Security.Web.UI.Principals
{
  /// <summary>
  /// The control panel for administration users user interface.
  /// </summary>
  public class UsersPanel : ProviderControlPanel<UsersPanel>, IPrincipalHost, IPartialRouteHandler
  {
    private UserManager userManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.Web.UI.Principals.UsersPanel" /> class.
    /// </summary>
    public UsersPanel()
      : base(false)
    {
      this.Title = Res.Get<PageResources>().Users;
    }

    /// <summary>Gets the user manager.</summary>
    /// <value>The manager.</value>
    public UserManager Manager
    {
      get
      {
        if (this.userManager == null)
          this.userManager = UserManager.GetManager(this.ProviderName);
        return this.userManager;
      }
    }

    /// <summary>Loads configured views.</summary>
    protected override void CreateViews() => this.AddView<UsersList>();

    /// <summary>
    /// When overridden this method returns a list of custom Command Panels.
    /// </summary>
    /// <param name="viewMode">Specifies the current View Mode</param>
    /// <param name="list">A list of custom command panels.</param>
    protected override void CreateCustomCommandPanels(string viewMode, IList<ICommandPanel> list)
    {
      base.CreateCustomCommandPanels(viewMode, list);
      if (list == null)
        list = (IList<ICommandPanel>) new List<ICommandPanel>();
      UsersCommandPanel usersCommandPanel = ObjectFactory.Resolve<UsersCommandPanel>();
      usersCommandPanel.Host = this;
      usersCommandPanel.ControlPanel = (IControlPanel) this;
      list.Add((ICommandPanel) usersCommandPanel);
    }

    /// <summary>
    /// An object that encapsulates information about the requested route.
    /// </summary>
    /// <value></value>
    public PartialRequestContext PartialRequestContext { get; set; }

    /// <summary>Gets or sets the parent route handler.</summary>
    /// <value></value>
    public IPartialRouteHandler ParentRouteHandler { get; set; }

    /// <summary>
    /// Creates a collection of objects that derive from the
    /// <see cref="T:System.Web.Routing.RouteBase" /> class.
    /// </summary>
    /// <returns>
    /// An object that contains all the routes in the collection.
    /// </returns>
    public RouteCollection CreateRoutes() => new RouteCollection();

    /// <summary>Registers child partial route handlers.</summary>
    /// <param name="list">A list of <see cref="T:Telerik.Sitefinity.Web.RouteInfo" /> objects.</param>
    public void RegisterChildRouteHandlers(IList<RouteInfo> list)
    {
    }
  }
}
