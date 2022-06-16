// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.Principals.UsersCommandPanel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend;

namespace Telerik.Sitefinity.Security.Web.UI.Principals
{
  /// <summary>
  /// The command panel for the Users administration section.
  /// </summary>
  public class UsersCommandPanel : ViewModeControl<UsersPanel>, ICommandPanel
  {
    private string loggedInUrl = "~/Sitefinity/Services/Security/Users.svc/GetLoggedInUsersCount/";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Security.Principals.UsersCommandPanel.ascx");

    /// <summary>
    /// Gets the name of the embedded layout template. If the control uses layout template this
    /// property must be overridden to provide the path (key) to an embedded resource file.
    /// </summary>
    /// <value></value>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets the roles binder.</summary>
    /// <value>The roles binder.</value>
    protected virtual GenericCollectionBinder RolesBinder => this.Container.GetControl<GenericCollectionBinder>("rolesBinder", true);

    /// <summary>Gets the manage profile types link.</summary>
    /// <value>The manage profile types link.</value>
    protected virtual SitefinityHyperLink ManageProfileTypesLink => this.Container.GetControl<SitefinityHyperLink>("cmdManageProfileTypes", true);

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? UsersCommandPanel.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Initializes all controls instantiated in the layout container. This method is called at appropriate time for setting initial values and subscribing for events of layout controls.
    /// </summary>
    /// <param name="viewContainer">The control that will host the current view.</param>
    protected override void InitializeControls(Control viewContainer)
    {
      this.Container.GetControl<HiddenField>("hfLoggedInUsersCountServiceUrl", true).Value = this.ResolveClientUrl(this.loggedInUrl);
      string str = string.Empty;
      foreach (Guid unassignableRole in SecurityManager.UnassignableRoles)
      {
        if (str != string.Empty)
          str += " AND ";
        str = str + " ( Id != \"" + unassignableRole.ToString() + "\" ) ";
      }
      if (str.Length > 0)
        str = "( " + str + " ) ";
      this.RolesBinder.FilterExpression = str;
      this.ManageProfileTypesLink.Visible = SystemManager.IsOperationEnabled(RestrictionLevel.ReadOnlyConfigFile);
      base.InitializeControls(viewContainer);
      this.SetHyperlink("cmdManagePermissions", SiteInitializer.PermissionsPageId);
      HtmlGenericControl control = this.Container.GetControl<HtmlGenericControl>("usersSettingsWrapper", false);
      if (control != null)
        control.Visible = false;
      this.SetHyperlink("cmdManageRoles", SiteInitializer.RolesPageId);
      this.SetHyperlink("cmdManageProfileTypes", SiteInitializer.ProfileTypesPageId);
    }

    private void SetHyperlink(string elementId, Guid pageId)
    {
      HyperLink control = this.Container.GetControl<HyperLink>(elementId, false);
      if (control == null)
        return;
      SiteMapNode siteMapNode = BackendSiteMap.FindSiteMapNode(pageId, true);
      if (siteMapNode != null)
      {
        control.NavigateUrl = UrlPath.ResolveUrl(siteMapNode.Url);
        control.Visible = true;
        if (control.Parent == null || control.Parent.Visible)
          return;
        control.Parent.Visible = true;
      }
      else
        control.Visible = false;
    }

    /// <summary>The name of the view.</summary>
    /// <value></value>
    public new string Name => throw new NotImplementedException();

    /// <summary>The title of the view.</summary>
    /// <value></value>
    public new string Title => throw new NotImplementedException();

    /// <summary>
    /// Reference to the control panel tied to the command panel instance.
    /// </summary>
    /// <value></value>
    /// <remarks>
    /// This property is used for communication between the command panel and its control
    /// panel.
    /// </remarks>
    /// <example>
    /// You can refer to <see cref="T:Telerik.Sitefinity.Web.UI.Backend.ICommandPanel">ICommandPanel</see> interface for more
    /// complicated example implementing the whole
    /// <see cref="T:Telerik.Sitefinity.Web.UI.Backend.ICommandPanel">ICommandPanel</see> interface.
    /// </example>
    public IControlPanel ControlPanel { get; set; }
  }
}
