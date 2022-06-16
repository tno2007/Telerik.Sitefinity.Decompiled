// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Security.Permissions.ItemActionPermissionsList
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.WcfHelpers;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Licensing;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Web.Services;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.Json;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.Backend.Security.Permissions
{
  /// <summary>
  /// Control for associating users and roles with actions of a specific permission set's action, for a secured object
  /// </summary>
  public class ItemActionPermissionsList : SimpleView, IScriptControl
  {
    private bool requireSystemProviders;
    private bool showPermissionSetNameTitle;
    private bool bindOnLoad = true;
    private string managerClassName = string.Empty;
    /// <summary>
    /// URL of the service to use for setting/getting permissions
    /// </summary>
    protected string permissionsServiceUrl = string.Empty;
    /// <summary>URL of the users/roles selection window</summary>
    private const string usersSelectionWindowUrl = "~/Sitefinity/Dialog/ActionUsersSelection";
    /// <summary>URL of the permissions sites usage window</summary>
    private const string sitesUsageWindowUrl = "~/Sitefinity/Dialog/PermissionsSitesUsageDialog";
    /// <summary>Name of the template to use</summary>
    public static readonly string layoutTemplateName = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Security.Permissions.ItemActionPermissionsList.ascx");

    /// <summary>Gets or sets the selected provider.</summary>
    /// <value>The selected provider.</value>
    internal string SelectedProviderName { get; set; }

    /// <summary>The name of the permission set</summary>
    public string PermissionsSetName { get; set; }

    /// <summary>
    /// Full name of the manager class for managing the permissions
    /// </summary>
    public string ManagerClassName
    {
      get
      {
        if (string.IsNullOrEmpty(this.managerClassName) && !string.IsNullOrEmpty(this.SecuredObjectTypeName))
          this.managerClassName = ManagerBase.GetMappedManagerType(this.SecuredObjectTypeName).AssemblyQualifiedName;
        return this.managerClassName;
      }
      set => this.managerClassName = value;
    }

    /// <summary>
    /// Name of the provider to use for managing the permissions
    /// </summary>
    public string DataProviderName { get; set; }

    /// <summary>
    /// ID of the secured object (optional. If not specified, the SecurityRoot of the manager+provider is used).
    /// </summary>
    public Guid SecuredObjectID { get; set; }

    /// <summary>
    /// Gets or sets the string representing the secured object's type.
    /// </summary>
    public string SecuredObjectTypeName { get; set; }

    /// <summary>Gets or sets the title.</summary>
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to bind the permission list on load].
    /// </summary>
    public bool BindOnLoad
    {
      get => this.bindOnLoad;
      set => this.bindOnLoad = value;
    }

    /// <summary>
    /// Gets or sets the name of the on client side function to be called when client initialization is done.
    /// </summary>
    /// <value>Name of the javascript function be called.</value>
    public string OnClientInitialized { get; set; }

    /// <summary>
    /// Gets or sets the display status of the permission-set name in the list of actions
    /// </summary>
    public bool ShowPermissionSetNameTitle
    {
      get => this.showPermissionSetNameTitle;
      set => this.showPermissionSetNameTitle = value;
    }

    /// <summary>
    /// Gets or sets the display if the permission list should include secure providers
    /// </summary>
    public bool RequireSystemProviders
    {
      get => this.requireSystemProviders;
      set => this.requireSystemProviders = value;
    }

    /// <summary>
    /// Gets or sets the optional name of the module to bind to.
    /// </summary>
    public string ModuleName { get; set; }

    /// <summary>A combo box with providers</summary>
    protected virtual RadComboBox ProvidersCombo => this.Container.GetControl<RadComboBox>("lstProviders", true);

    /// <summary>
    /// Panel containing the providers combo box and a matching label
    /// </summary>
    protected virtual Panel ProviderSelectionPanel => this.Container.GetControl<Panel>("pnlProviderSelection", true);

    /// <summary>
    /// Client binder for listing permission sets, actions and users
    /// </summary>
    protected virtual GenericCollectionBinder PermissionSetsBinder => this.Container.GetControl<GenericCollectionBinder>("permissionSetsBinder", true);

    /// <summary>
    /// Dialog for selecting allowed and denied users per action
    /// </summary>
    protected virtual Telerik.Web.UI.RadWindow UsersSelectionRadWindow => this.Container.GetControl<Telerik.Web.UI.RadWindow>("usersSelection", true);

    /// <summary>
    /// Dialog for displaying sites usage of provider selected
    /// </summary>
    /// <value>The sites usage RAD window.</value>
    protected virtual Telerik.Web.UI.RadWindow SitesUsageRadWindow => this.Container.GetControl<Telerik.Web.UI.RadWindow>("sitesUsage", true);

    /// <summary>
    /// Label for displaynig title of the control (automatically: the selected module)
    /// </summary>
    protected virtual HtmlGenericControl PermissionTitleLabel => this.Container.GetControl<HtmlGenericControl>("lblPermissionTitle", true);

    /// <summary>Gets the sites usage label.</summary>
    /// <value>The sites usage label.</value>
    protected virtual Label SitesUsageLabel => this.Container.GetControl<Label>("usedIn", true);

    /// <summary>Gets the sites usage link.</summary>
    /// <value>The sites usage link.</value>
    protected virtual HyperLink SitesUsageLink => this.Container.GetControl<HyperLink>("sitesUsageLink", true);

    /// <summary>Tooltip for displaying an action's description text</summary>
    protected virtual RadToolTip ActionDescriptionToolTip => this.Container.GetControl<RadToolTip>("actionDescriptionToolTip", true);

    /// <summary>
    /// Gets the panel which is displayed when permissions are inherited.
    /// </summary>
    protected virtual Panel InheritsPermissionsPanel => this.Container.GetControl<Panel>("pnlInheritsPermissions", true);

    /// <summary>
    /// Gets the panel which is displayed when the permissions inheritance is broken.
    /// </summary>
    protected virtual Panel InheritanceBrokenPanel => this.Container.GetControl<Panel>("pnlInheritanceBroken", true);

    /// <summary>Gets the "override inherited permissions" button.</summary>
    protected virtual LinkButton OverrideInheritedPermissionsButton => this.Container.GetControl<LinkButton>("btnOverrideInheritedPermissions", true);

    /// <summary>Gets the "inherit permissions" button.</summary>
    protected virtual LinkButton InheritPermissionsButton => this.Container.GetControl<LinkButton>("btnInheritPermissions", true);

    /// <summary>Gets the loading progress panel Inheritance.</summary>
    /// <value>The loading progress panel Inheritance.</value>
    protected virtual HtmlControl LoadingProgressPanelInheritance => this.Container.GetControl<HtmlControl>("loadingProgressPanelInheritance", true);

    /// <summary>Gets the main permission inheritance panel.</summary>
    /// <value>Main permission inheritance panel.</value>
    protected virtual HtmlControl MainPermissionInheritancePanel => this.Container.GetControl<HtmlControl>("mainPermissionInheritancePanel", true);

    /// <summary>Gets the loading progress panel.</summary>
    /// <value>The loading progress panel.</value>
    protected virtual HtmlControl LoadingProgressPanel => this.Container.GetControl<HtmlControl>("loadingProgressPanel", true);

    /// <summary>Gets the main permissions panel.</summary>
    /// <value>The main permissions panel.</value>
    protected virtual HtmlControl MainPermissionsPanel => this.Container.GetControl<HtmlControl>("mainPermissionsPanel", true);

    /// <summary>This control will be rendered as a DIV</summary>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Initializes the controls.</summary>
    /// <param name="container">The container of the control</param>
    protected override void InitializeControls(GenericContainer container)
    {
      this.permissionsServiceUrl = this.ResolveClientUrl("~/Sitefinity/Services/Security/Permissions.svc");
      this.UsersSelectionRadWindow.NavigateUrl = "~/Sitefinity/Dialog/ActionUsersSelection";
      this.SitesUsageRadWindow.NavigateUrl = "~/Sitefinity/Dialog/PermissionsSitesUsageDialog";
      this.ProviderSelectionPanel.Attributes.Add("style", "display:none");
      this.PermissionTitleLabel.InnerText = this.Title;
    }

    /// <summary>Event handler for the PreRender lifecycle event</summary>
    /// <param name="e">Not used.</param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      (ScriptManager.GetCurrent(this.Page) ?? throw new HttpException(Res.Get<ErrorMessages>().ScriptManagerIsNull)).RegisterScriptControl<ItemActionPermissionsList>(this);
    }

    /// <summary>Event handler for the Render lifecycle event</summary>
    /// <param name="writer">Not used.</param>
    protected override void Render(HtmlTextWriter writer)
    {
      if (!this.DesignMode && this.Page != null)
        ScriptManager.GetCurrent(this.Page)?.RegisterScriptDescriptors((IScriptControl) this);
      base.Render(writer);
    }

    /// <summary>The name of the ASCX template to use</summary>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ItemActionPermissionsList.layoutTemplateName : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.</returns>
    public IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptBehaviorDescriptor behaviorDescriptor = new ScriptBehaviorDescriptor(this.GetType().ToString(), this.ClientID);
      behaviorDescriptor.AddProperty("_providersComboID", (object) this.ProvidersCombo.ClientID);
      behaviorDescriptor.AddProperty("_providerSelectionPanelID", (object) this.ProviderSelectionPanel.ClientID);
      behaviorDescriptor.AddProperty("_permissionsUrl", (object) this.permissionsServiceUrl);
      behaviorDescriptor.AddProperty("_permissionSetsBinderID", (object) this.PermissionSetsBinder.ClientID);
      behaviorDescriptor.AddProperty("_wcfPrincipalType", (object) JsonUtility.EnumToJson(typeof (WcfPrincipalType)));
      behaviorDescriptor.AddProperty("_administratorsOnlyLabelText", (object) Res.Get<SecurityResources>().AdministratorsOnly);
      behaviorDescriptor.AddProperty("_usersSelectionRadWindowID", (object) this.UsersSelectionRadWindow.ClientID);
      behaviorDescriptor.AddProperty("_allowedUsersLabelText", (object) Res.Get<SecurityResources>().AllowedUsersPermissionsListTitle);
      behaviorDescriptor.AddProperty("_deniedUsersLabelText", (object) Res.Get<SecurityResources>().DeniedUsersPermissionsListTitle);
      behaviorDescriptor.AddProperty("_changeButtonLabelText", (object) Res.Get<Labels>().Change);
      behaviorDescriptor.AddProperty("_permissionTitleLabelID", (object) this.PermissionTitleLabel.ClientID);
      behaviorDescriptor.AddProperty("_actionDescriptionToolTip", (object) this.ActionDescriptionToolTip.ClientID);
      behaviorDescriptor.AddProperty("_inheritsPermissionsPanelID", (object) this.InheritsPermissionsPanel.ClientID);
      behaviorDescriptor.AddProperty("_inheritanceBrokenPanelID", (object) this.InheritanceBrokenPanel.ClientID);
      behaviorDescriptor.AddProperty("_overrideInheritedPermissionsButtonID", (object) this.OverrideInheritedPermissionsButton.ClientID);
      behaviorDescriptor.AddProperty("_inheritPermissionsButtonID", (object) this.InheritPermissionsButton.ClientID);
      behaviorDescriptor.AddProperty("_confirmInheritPermissionsMessage", (object) Res.Get<SecurityResources>().ConfirmInheritPermissions);
      behaviorDescriptor.AddProperty("_loadingProgressPanelID", (object) this.LoadingProgressPanel.ClientID);
      behaviorDescriptor.AddProperty("_loadingProgressPanelInheritanceID", (object) this.LoadingProgressPanelInheritance.ClientID);
      behaviorDescriptor.AddProperty("_mainPermissionsPanelID", (object) this.MainPermissionsPanel.ClientID);
      behaviorDescriptor.AddProperty("_mainPermissionInheritancePanelID", (object) this.MainPermissionInheritancePanel.ClientID);
      behaviorDescriptor.AddProperty("_requireSystemProviders", (object) this.RequireSystemProviders);
      behaviorDescriptor.AddProperty("_multiSiteMode", (object) true);
      behaviorDescriptor.AddProperty("_sitesUsageRadWindowID", (object) this.SitesUsageRadWindow.ClientID);
      behaviorDescriptor.AddProperty("_sitesUsageLabelID", (object) this.SitesUsageLabel.ClientID);
      behaviorDescriptor.AddProperty("_sitesUsageLinkID", (object) this.SitesUsageLink.ClientID);
      behaviorDescriptor.AddProperty("_sitesUsageSingleLabelText", (object) Res.Get<SecurityResources>().Site);
      behaviorDescriptor.AddProperty("_sitesUsageMultipleLabelText", (object) Res.Get<SecurityResources>().Sites);
      behaviorDescriptor.AddProperty("permissionSetName", (object) this.PermissionsSetName);
      behaviorDescriptor.AddProperty("managerClassName", (object) this.ManagerClassName);
      behaviorDescriptor.AddProperty("dataProviderName", (object) this.DataProviderName);
      behaviorDescriptor.AddProperty("securedObjectID", (object) this.SecuredObjectID);
      behaviorDescriptor.AddProperty("showPermissionSetName", (object) this.ShowPermissionSetNameTitle.ToString().ToLower());
      behaviorDescriptor.AddProperty("securedObjectTypeName", (object) this.SecuredObjectTypeName);
      behaviorDescriptor.AddProperty("title", (object) this.Title);
      behaviorDescriptor.AddProperty("bindOnLoad", (object) this.BindOnLoad);
      behaviorDescriptor.AddProperty("moduleName", (object) this.ModuleName);
      behaviorDescriptor.AddProperty("selectedProviderName", (object) this.SelectedProviderName);
      if (!string.IsNullOrEmpty(this.OnClientInitialized))
        behaviorDescriptor.AddEvent("onClientInitialized", this.OnClientInitialized);
      Type type = (Type) null;
      if (string.IsNullOrWhiteSpace(this.SecuredObjectTypeName) && !string.IsNullOrWhiteSpace(this.Page.Request.QueryString["typeName"]))
        this.SecuredObjectTypeName = this.Page.Request.QueryString["typeName"];
      if (!string.IsNullOrWhiteSpace(this.SecuredObjectTypeName))
        type = TypeResolutionService.ResolveType(WcfHelper.DecodeWcfString(this.SecuredObjectTypeName), false, true);
      bool flag = !(type != (Type) null) || !type.IsAssignableFrom(typeof (PageNode)) ? LicenseState.Current.LicenseInfo.IsGranularPermissionsEnabled : LicenseState.Current.LicenseInfo.IsPagesGranularPermissionsEnabled;
      behaviorDescriptor.AddProperty("_isGranularPermissionsEnabled", (object) flag);
      if (this.ModuleName != null && SystemManager.GetDynamicModule(this.ModuleName) != null && (this.SecuredObjectTypeName == null || this.SecuredObjectTypeName.Equals(typeof (DynamicModuleType).FullName)))
        behaviorDescriptor.AddProperty("_applyDynamicModulePermissions", (object) true);
      behaviorDescriptor.AddProperty("_moduleBuilderClassName", (object) typeof (ModuleBuilderManager).AssemblyQualifiedName);
      behaviorDescriptor.AddProperty("_moduleBuilderDefaultProvider", (object) ManagerBase<ModuleBuilderDataProvider>.GetDefaultProviderName());
      return (IEnumerable<ScriptDescriptor>) new ScriptBehaviorDescriptor[1]
      {
        behaviorDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.</returns>
    public IEnumerable<ScriptReference> GetScriptReferences()
    {
      string str = this.GetType().Assembly.GetName().ToString();
      return (IEnumerable<ScriptReference>) new ScriptReference[3]
      {
        new ScriptReference()
        {
          Assembly = str,
          Name = "Telerik.Sitefinity.Web.UI.Backend.Security.Permissions.Scripts.ItemActionPermissionsList.js"
        },
        new ScriptReference()
        {
          Assembly = str,
          Name = "Telerik.Sitefinity.Web.Scripts.ClientManager.js"
        },
        new ScriptReference()
        {
          Assembly = Assembly.GetExecutingAssembly().FullName,
          Name = "Telerik.Sitefinity.Web.SitefinityJS.Telerik.Sitefinity.js"
        }
      };
    }
  }
}
