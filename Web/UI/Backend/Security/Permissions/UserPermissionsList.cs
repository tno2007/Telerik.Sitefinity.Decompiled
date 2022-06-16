// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Security.Permissions.UserPermissionsList
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Data.WcfHelpers;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Web.UI.ClientBinders;

namespace Telerik.Sitefinity.Web.UI.Backend.Security.Permissions
{
  /// <summary>
  /// Represents the UserPermissionsList control, for listing permissions for various actions, for a specific user
  /// </summary>
  public class UserPermissionsList : SimpleView, IScriptControl, IClientBinderContainer
  {
    /// <summary>
    /// A list of permissions to be loaded when the control loads, for binding with the main repeater
    /// </summary>
    protected List<Telerik.Sitefinity.Security.Model.Permission> userPermissions = new List<Telerik.Sitefinity.Security.Model.Permission>();
    /// <summary>
    /// URL of the Permission service to use for getting/saving data
    /// </summary>
    protected string PrincipalServiceURL = string.Empty;
    private bool bindOnLoad = true;
    private bool showPermissionSetNameTitle = true;
    private bool showGeneralPermissionSetTitles = true;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Security.Permissions.UserPermissionsList.ascx");
    private const string permissionEditorWindow = "~/Sitefinity/Dialog/UserPermissionsEditor";
    private const string sitesUsageWindowUrl = "~/Sitefinity/Dialog/PermissionsSitesUsageDialog";

    /// <summary>Created the control's child-controls</summary>
    protected override void CreateChildControls()
    {
      this.Controls.Add((Control) this.Container);
      this.InitializeControls(this.Container);
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="dialogContainer">The container of the control</param>
    protected override void InitializeControls(GenericContainer dialogContainer)
    {
      this.PermissionsSelectionRadWindow.NavigateUrl = "~/Sitefinity/Dialog/UserPermissionsEditor";
      this.SitesUsageRadWindow.NavigateUrl = "~/Sitefinity/Dialog/PermissionsSitesUsageDialog";
      this.PrincipalServiceURL = this.ResolveClientUrl("~/Sitefinity/Services/Security/Permissions.svc");
      this.UserOrRoleNameLabel.Attributes.Add("style", "display:" + (this.ShowPrincipalName ? "block" : "none"));
    }

    /// <summary>Event handler for the PreRender lifecycle event</summary>
    /// <param name="e">Not used.</param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      ScriptManager current = ScriptManager.GetCurrent(this.Page);
      if (current == null)
        throw new HttpException(Res.Get<ErrorMessages>().ScriptManagerIsNull);
      PageManager.ConfigureScriptManager(this.Page, ScriptRef.MicrosoftAjax | ScriptRef.MicrosoftAjaxTemplates | ScriptRef.JQuery | ScriptRef.TelerikSitefinity);
      current.RegisterScriptControl<UserPermissionsList>(this);
    }

    /// <summary>Event handler for the Render lifecycle event</summary>
    /// <param name="writer">Not used.</param>
    protected override void Render(HtmlTextWriter writer)
    {
      if (!this.DesignMode && this.Page != null)
        ScriptManager.GetCurrent(this.Page)?.RegisterScriptDescriptors((IScriptControl) this);
      base.Render(writer);
    }

    /// <summary>
    /// Retrieves the embedded path of the template used for this control
    /// </summary>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? UserPermissionsList.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>ID of the principal (user/role) to bind to</summary>
    public Guid PrincipalID { get; set; }

    /// <summary>
    /// Full name (incl. namespace) of the class to be used as manager for the edited permissions
    /// </summary>
    public string ManagerClassName { get; set; }

    /// <summary>Data provider for which permissions are edited</summary>
    public string DataProviderName { get; set; }

    /// <summary>PermissionSet of the edited permissions</summary>
    public string PermissionsSetName { get; set; }

    /// <summary>Whether to show the current user name label</summary>
    public bool ShowPrincipalName { get; set; }

    /// <summary>
    /// ID of the secured object (optional. If not specified, the SecurityRoot of the manager+provider is used).
    /// </summary>
    public Guid SecuredObjectID { get; set; }

    /// <summary>
    /// The assembly-qualified name of the type of the secured object, including the assembly name.
    /// E.g.: "Telerik.Sitefinity.Security.Model.SecurityRoot, Telerik.Sitefinity.Model"
    /// </summary>
    public string SecuredObjectType { get; set; }

    /// <summary>Gets or sets the name of the active principal.</summary>
    public string PrincipalName { get; set; }

    /// <summary>
    /// Gets or sets the name of the on client side function to be called when client initialization is done.
    /// </summary>
    /// <value>Name of the javascript function be called.</value>
    public string OnClientInitialized { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to bind the permission list on load].
    /// </summary>
    public bool BindOnLoad
    {
      get => this.bindOnLoad;
      set => this.bindOnLoad = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show permission set name titles.
    /// </summary>
    public bool ShowPermissionSetNameTitle
    {
      get => this.showPermissionSetNameTitle;
      set => this.showPermissionSetNameTitle = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show permission set name titles of the "General" permission set.
    /// </summary>
    public bool ShowGeneralPermissionSetTitles
    {
      get => this.showGeneralPermissionSetTitles;
      set => this.showGeneralPermissionSetTitles = value;
    }

    /// <summary>Label displaying the user/role name</summary>
    protected virtual HtmlGenericControl UserOrRoleNameLabel => this.Container.GetControl<HtmlGenericControl>("lblUserOrRoleName", true);

    /// <summary>Label displaying the permission set name</summary>
    protected virtual Literal PermissionNameLabel => this.Container.GetControl<Literal>("lblPermissionName", true);

    /// <summary>RadWindow of the editor dialog box</summary>
    protected virtual Telerik.Web.UI.RadWindow PermissionsSelectionRadWindow => this.Container.GetControl<Telerik.Web.UI.RadWindow>("permissionsSelection", true);

    /// <summary>Gets the main permissions table panel.</summary>
    /// <value>The main permissions table panel.</value>
    private Panel MainPermissionsTablePanel => this.Container.GetControl<Panel>("pnlMainPermissions", true);

    /// <summary>Gets the loading progress panel.</summary>
    /// <value>The loading progress panel.</value>
    protected virtual HtmlControl LoadingProgressPanel => this.Container.GetControl<HtmlControl>("loadingProgressPanel", true);

    /// <summary>
    /// Dialog for displaying sites usage of provider selected
    /// </summary>
    /// <value>The sites usage RAD window.</value>
    protected virtual Telerik.Web.UI.RadWindow SitesUsageRadWindow => this.Container.GetControl<Telerik.Web.UI.RadWindow>("sitesUsage", true);

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.</returns>
    IEnumerable<ScriptDescriptor> IScriptControl.GetScriptDescriptors()
    {
      ScriptBehaviorDescriptor behaviorDescriptor = new ScriptBehaviorDescriptor(this.GetType().ToString(), this.ClientID);
      behaviorDescriptor.AddProperty("_permissionsSelectionDialogID", (object) this.PermissionsSelectionRadWindow.ClientID);
      behaviorDescriptor.AddProperty("_principalServiceURL", (object) this.PrincipalServiceURL);
      behaviorDescriptor.AddProperty("_userOrRoleNameLabelID", (object) this.UserOrRoleNameLabel.ClientID);
      behaviorDescriptor.AddProperty("_mainPermissionsTablePanelID", (object) this.MainPermissionsTablePanel.ClientID);
      behaviorDescriptor.AddProperty("_loadingProgressPanelID", (object) this.LoadingProgressPanel.ClientID);
      behaviorDescriptor.AddProperty("_generalPermissionSetName", (object) "General");
      behaviorDescriptor.AddProperty("_multiSiteMode", (object) true);
      behaviorDescriptor.AddProperty("_sitesUsageRadWindowID", (object) this.SitesUsageRadWindow.ClientID);
      behaviorDescriptor.AddProperty("_sitesUsageSingleLabelText", (object) Res.Get<SecurityResources>().Site);
      behaviorDescriptor.AddProperty("_sitesUsageMultipleLabelText", (object) Res.Get<SecurityResources>().Sites);
      behaviorDescriptor.AddProperty("principalID", (object) this.PrincipalID.ToString());
      behaviorDescriptor.AddProperty("managerClassName", string.IsNullOrEmpty(this.ManagerClassName) ? (object) "" : (object) WcfHelper.EncodeWcfString(this.ManagerClassName));
      behaviorDescriptor.AddProperty("dataProviderName", string.IsNullOrEmpty(this.DataProviderName) ? (object) "" : (object) this.DataProviderName);
      behaviorDescriptor.AddProperty("permissionsSetName", string.IsNullOrEmpty(this.PermissionsSetName) ? (object) "" : (object) this.PermissionsSetName);
      behaviorDescriptor.AddProperty("showPrincipalName", (object) this.ShowPrincipalName.ToString().ToLower());
      behaviorDescriptor.AddProperty("securedObjectID", (object) this.SecuredObjectID);
      behaviorDescriptor.AddProperty("securedObjectType", (object) this.SecuredObjectType);
      behaviorDescriptor.AddProperty("principalName", (object) this.PrincipalName);
      behaviorDescriptor.AddProperty("bindOnLoad", (object) this.BindOnLoad);
      behaviorDescriptor.AddProperty("showPermissionSetNameTitle", (object) this.ShowPermissionSetNameTitle);
      behaviorDescriptor.AddProperty("showGeneralPermissionSetTitles", (object) this.ShowGeneralPermissionSetTitles);
      if (!string.IsNullOrEmpty(this.OnClientInitialized))
        behaviorDescriptor.AddEvent("onClientInitialized", this.OnClientInitialized);
      return (IEnumerable<ScriptDescriptor>) new ScriptBehaviorDescriptor[1]
      {
        behaviorDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.</returns>
    IEnumerable<ScriptReference> IScriptControl.GetScriptReferences()
    {
      string str = this.GetType().Assembly.GetName().ToString();
      return (IEnumerable<ScriptReference>) new ScriptReference[2]
      {
        new ScriptReference()
        {
          Assembly = str,
          Name = "Telerik.Sitefinity.Web.UI.Backend.Security.Permissions.Scripts.UserPermissionsList.js"
        },
        new ScriptReference()
        {
          Assembly = str,
          Name = "Telerik.Sitefinity.Web.Scripts.ClientManager.js"
        }
      };
    }
  }
}
