// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.Principals.RolesPanel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.Routing;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI.Backend;

namespace Telerik.Sitefinity.Security.Web.UI.Principals
{
  /// <summary>The control panel for the Administration Roles UI.</summary>
  public class RolesPanel : ProviderControlPanel<RolesPanel>, IPartialRouteHandler
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.Web.UI.Principals.RolesPanel" /> class.
    /// </summary>
    public RolesPanel() => this.Title = Res.Get<PageResources>().Roles;

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

    /// <summary>Loads configured views.</summary>
    protected override void CreateViews() => this.AddView<RolesView<RolesPanel>>();

    /// <summary>
    /// When overridden this method returns a list of standard Command Panels.
    /// </summary>
    /// <param name="viewMode">The view mode.</param>
    /// <param name="commandsInfo">A list of <see cref="T:Telerik.Sitefinity.Web.UI.Backend.CommandItem">ControlPanel.CommandItem</see> objects.</param>
    /// <param name="commandPanels">A list of Command Panels for this Control Panel.
    /// This list can be used to add and remove Command Panels.</param>
    /// <remarks>
    /// This method will automatically create Command Panels
    /// based on the information passed with CommandItem classes.
    /// One command panel will be created per each
    /// unique panel name specified in the collection of command items.
    /// Command panels will appear in the order they were created.
    /// </remarks>
    protected override void CreateStandardCommandPanels(
      string viewMode,
      IList<CommandItem> commandsInfo,
      IList<ICommandPanel> commandPanels)
    {
      if (commandsInfo == null)
        commandsInfo = (IList<CommandItem>) new List<CommandItem>();
      base.CreateStandardCommandPanels(viewMode, commandsInfo, commandPanels);
    }
  }
}
