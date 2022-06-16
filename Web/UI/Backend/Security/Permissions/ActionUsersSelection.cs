// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Security.Permissions.ActionUsersSelection
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.WcfHelpers;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Security.Web.Services;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.Json;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets;

namespace Telerik.Sitefinity.Web.UI.Backend.Security.Permissions
{
  /// <summary>
  /// Represents the ActionUsersSelection dialog, for sepecting users/roles allowed/denied to perform a specific action
  /// </summary>
  public class ActionUsersSelection : AjaxDialogBase
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Security.Permissions.ActionUsersSelection.ascx");
    private List<Telerik.Sitefinity.Security.Model.Permission> predefinedPermissions = new List<Telerik.Sitefinity.Security.Model.Permission>();
    private bool administratorRoleSelected = true;
    private bool everyoneRoleSelected;
    private bool backendRoleSelected;
    private bool specificPrincipalsSelected;
    private string everyoneRoleID;
    private string backendRoleID;
    /// <summary>
    /// A list of permissions to preloaded when the dialog loads
    /// </summary>
    protected List<WcfPermission> OnLoadPermissions = new List<WcfPermission>();
    /// <summary>
    /// URL of the service to use for setting/getting permissions
    /// </summary>
    protected string permissionsServiceUrl = string.Empty;
    /// <summary>
    /// Preset text to be rendered to the client for every selected user/role.
    /// By CSS, this text is changed to a "X" (remove) icon
    /// </summary>
    protected const string PrincipalRemoveText = "delete";
    /// <summary>
    /// Preset text corresponding with the "Everyone" role name, which should be explicitly checked.
    /// </summary>
    protected const string EveryoneRoleName = "EVERYONE";
    protected const string BackendRoleName = "BACKENDUSERS";

    /// <summary>Gets or sets the Label for permissions.</summary>
    /// <value></value>
    protected virtual Label RolesPermissionsMessage => this.Container.GetControl<Label>("rolesPermissionsMessage", false);

    /// <summary>Initializes the controls.</summary>
    /// <param name="dialogContainer">The container of the control</param>
    protected override void InitializeControls(GenericContainer dialogContainer)
    {
      Guid securedObjectId1 = this.SecuredObjectID;
      Guid securedObjectId2 = !(this.SecuredObjectID != Guid.Empty) ? new Guid(this.PermissionObjectRootID) : this.SecuredObjectID;
      ISecuredObject securedObject = SecurityUtility.GetSecuredObject(ManagerBase.GetManager(WcfHelper.DecodeWcfString(this.ManagerClassName), this.DataProviderName), this.SecuredObjectType, securedObjectId2, this.DynamicDataProviderName);
      string permSetName = this.PermissionSetName;
      this.predefinedPermissions = securedObject.GetActivePermissions().Where<Telerik.Sitefinity.Security.Model.Permission>((Expression<Func<Telerik.Sitefinity.Security.Model.Permission, bool>>) (p => p.SetName == permSetName)).ToList<Telerik.Sitefinity.Security.Model.Permission>();
      List<WcfPermission> collection = new List<WcfPermission>();
      this.administratorRoleSelected = true;
      this.everyoneRoleSelected = false;
      this.backendRoleSelected = false;
      this.specificPrincipalsSelected = true;
      string tempEveryoneRoleName = "EVERYONE";
      ApplicationRole applicationRole1 = SecurityManager.ApplicationRoles.Values.Where<ApplicationRole>((Func<ApplicationRole, bool>) (apr => apr.Name.ToUpperInvariant() == tempEveryoneRoleName)).FirstOrDefault<ApplicationRole>();
      Guid guid1;
      if (applicationRole1 != null)
      {
        guid1 = applicationRole1.Id;
        this.everyoneRoleID = guid1.ToString();
      }
      string tempBackendRoleName = "BACKENDUSERS";
      ApplicationRole applicationRole2 = SecurityManager.ApplicationRoles.Values.Where<ApplicationRole>((Func<ApplicationRole, bool>) (apr => apr.Name.ToUpperInvariant() == tempBackendRoleName)).FirstOrDefault<ApplicationRole>();
      if (applicationRole2 != null)
      {
        guid1 = applicationRole2.Id;
        this.backendRoleID = guid1.ToString();
      }
      foreach (Telerik.Sitefinity.Security.Model.Permission predefinedPermission1 in this.predefinedPermissions)
      {
        Telerik.Sitefinity.Security.Model.Permission predefinedPermission = predefinedPermission1;
        ApplicationRole applicationRole3 = SecurityManager.ApplicationRoles.Values.Where<ApplicationRole>((Func<ApplicationRole, bool>) (apr => apr.Id == predefinedPermission.PrincipalId)).FirstOrDefault<ApplicationRole>();
        if (applicationRole3 != null)
        {
          string str = applicationRole3.Name.ToUpperInvariant().Trim();
          if (str == "EVERYONE" || str == "BACKENDUSERS")
          {
            if (predefinedPermission.IsGranted(this.ActionName))
            {
              this.everyoneRoleSelected = str == "EVERYONE";
              this.backendRoleSelected = str == "BACKENDUSERS";
              this.administratorRoleSelected = false;
              this.specificPrincipalsSelected = false;
              List<WcfPermission> wcfPermissionList = collection;
              string principalType = WcfPrincipalType.Role.ToString();
              guid1 = applicationRole3.Id;
              string principalID = guid1.ToString();
              string name1 = applicationRole3.Name;
              string name2 = applicationRole3.Name;
              string actionName = this.ActionName;
              string clientId1 = this.AllowedUsersOrRolesListPanel.ClientID;
              string clientId2 = this.DeniedUsersOrRolesListPanel.ClientID;
              string permissionSetName = this.PermissionSetName;
              guid1 = predefinedPermission.ObjectId;
              string securedObjectId3 = guid1.ToString();
              WcfPermission wcfPermission = new WcfPermission(principalType, principalID, name1, name2, "", true, false, actionName, clientId1, clientId2, permissionSetName, securedObjectId3);
              wcfPermissionList.Add(wcfPermission);
              break;
            }
          }
        }
      }
      this.OnLoadPermissions.AddRange((IEnumerable<WcfPermission>) collection);
      bool flag1 = false;
      foreach (Telerik.Sitefinity.Security.Model.Permission predefinedPermission in this.predefinedPermissions)
      {
        bool flag2 = predefinedPermission.IsGranted(this.ActionName);
        bool flag3 = predefinedPermission.IsDenied(this.ActionName);
        if (flag2 | flag3)
        {
          User user = (User) null;
          IRoleInfo roleInfo = (IRoleInfo) null;
          if (SecurityManager.IsPrincipalUser(predefinedPermission.PrincipalId))
            user = UserManager.FindUser(predefinedPermission.PrincipalId);
          if (SecurityManager.IsPrincipalRole(predefinedPermission.PrincipalId))
            roleInfo = SecurityManager.GetRoleOrAppRole(predefinedPermission.PrincipalId);
          WcfPrincipalType wcfPrincipalType;
          Guid guid2;
          if (user != null)
          {
            List<WcfPermission> onLoadPermissions = this.OnLoadPermissions;
            wcfPrincipalType = WcfPrincipalType.User;
            string principalType = wcfPrincipalType.ToString();
            guid2 = predefinedPermission.PrincipalId;
            string principalID = guid2.ToString();
            string userName = user.UserName;
            string userDisplayName = UserProfilesHelper.GetUserDisplayName(user.Id);
            string providerName = user.ProviderName;
            int num1 = flag2 ? 1 : 0;
            int num2 = flag3 ? 1 : 0;
            string actionName = this.ActionName;
            string clientId3 = this.AllowedUsersOrRolesListPanel.ClientID;
            string clientId4 = this.DeniedUsersOrRolesListPanel.ClientID;
            string permissionSetName = this.PermissionSetName;
            guid2 = predefinedPermission.ObjectId;
            string securedObjectId4 = guid2.ToString();
            WcfPermission wcfPermission = new WcfPermission(principalType, principalID, userName, userDisplayName, providerName, num1 != 0, num2 != 0, actionName, clientId3, clientId4, permissionSetName, securedObjectId4);
            onLoadPermissions.Add(wcfPermission);
            if (flag2)
              flag1 = true;
            if (flag2 && !this.everyoneRoleSelected)
            {
              this.administratorRoleSelected = false;
              this.everyoneRoleSelected = false;
              this.backendRoleSelected = false;
              this.specificPrincipalsSelected = true;
            }
          }
          else if (roleInfo != null && ((!(roleInfo.Id != applicationRole1.Id) ? 0 : (roleInfo.Id != applicationRole2.Id ? 1 : 0)) | (flag3 ? 1 : 0)) != 0)
          {
            List<WcfPermission> onLoadPermissions = this.OnLoadPermissions;
            wcfPrincipalType = WcfPrincipalType.Role;
            string principalType = wcfPrincipalType.ToString();
            guid2 = predefinedPermission.PrincipalId;
            string principalID = guid2.ToString();
            string name3 = roleInfo.Name;
            string name4 = roleInfo.Name;
            int num3 = flag2 ? 1 : 0;
            int num4 = flag3 ? 1 : 0;
            string actionName = this.ActionName;
            string clientId5 = this.AllowedUsersOrRolesListPanel.ClientID;
            string clientId6 = this.DeniedUsersOrRolesListPanel.ClientID;
            string permissionSetName = this.PermissionSetName;
            guid2 = predefinedPermission.ObjectId;
            string securedObjectId5 = guid2.ToString();
            WcfPermission wcfPermission = new WcfPermission(principalType, principalID, name3, name4, "", num3 != 0, num4 != 0, actionName, clientId5, clientId6, permissionSetName, securedObjectId5);
            onLoadPermissions.Add(wcfPermission);
            if (flag2)
              flag1 = true;
            if (flag2 && !this.everyoneRoleSelected)
            {
              this.administratorRoleSelected = false;
              this.everyoneRoleSelected = false;
              this.backendRoleSelected = false;
              this.specificPrincipalsSelected = true;
            }
          }
        }
      }
      if (flag1)
      {
        this.specificPrincipalsSelected = true;
        this.everyoneRoleSelected = false;
        this.backendRoleSelected = false;
        this.administratorRoleSelected = false;
      }
      if (this.InheritsPermissions)
      {
        this.InheritsPermissionsPanel.Attributes.Add("style", "display:block");
        this.AllowedEveryoneRadioButton.Enabled = false;
        this.AllowedBackendUsersRadioButton.Enabled = false;
        this.AllowedAdminsOnlyRadioButton.Enabled = false;
        this.AllowedSelectedUsersRadioButton.Enabled = false;
      }
      else
        this.InheritsPermissionsPanel.Attributes.Add("style", "display:none");
      SecurityAction action = Config.Get<SecurityConfig>().Permissions[this.PermissionSetName].Actions[this.ActionName];
      this.AllowedSelectedUsersRadioButton.Text = Res.Get<SecurityResources>().SpecificSelectedUsersInactive;
      this.CloseLinkButton.Text = Res.Get<SecurityResources>().ActionUserSelectionCancelBtn;
      this.ActionNameLabel.Text = action.GetTitle(securedObject);
      this.AllowedEveryoneRadioButton.Text = Res.Get<SecurityResources>().Everyone;
      this.AllowedBackendUsersRadioButton.Text = Res.Get<SecurityResources>().BackendUsers;
      this.AllowedAdminsOnlyRadioButton.Text = Res.Get<SecurityResources>().AdministratorsOnly;
      this.permissionsServiceUrl = this.ResolveClientUrl("~/Sitefinity/Services/Security/Permissions.svc");
      this.PrincipalSelectionPanel.Attributes.Add("style", "display:none");
      this.DisplayMessageInfoBox();
      this.PrincipalSelector.SelectorsReady += new MultiSelector.SelectorsReadyHandler(this.PrincipalSelector_SelectorsReady);
      if (SystemManager.IsDBPMode)
        this.RolesPermissionsMessage.Visible = true;
      else
        this.RolesPermissionsMessage.Visible = false;
    }

    /// <summary>
    /// Event handler for the "SelectorsReady" event of the multi selector:
    /// Don't allow to select the "Backend users" role, if it's already selected by radio button, and locked (due to inheritance).
    /// This means, it cannot be "allowed" and "denied" at the same time.
    /// </summary>
    private void PrincipalSelector_SelectorsReady()
    {
      ItemSelector itemSelector1 = this.PrincipalSelector.ItemSelectors["rolesSelector"];
      string str1 = "(Id != (" + (object) SecurityManager.EveryoneRole.Id + "))";
      if (this.InheritsPermissions && this.backendRoleSelected)
        str1 = str1 + " and (Id != (" + (object) SecurityManager.BackEndUsersRole.Id + "))";
      ItemSelector itemSelector2 = itemSelector1;
      string str2;
      if (!string.IsNullOrEmpty(itemSelector1.ConstantFilter))
        str2 = "(" + itemSelector1.ConstantFilter + ") and (" + str1 + ")";
      else
        str2 = str1;
      itemSelector2.ConstantFilter = str2;
    }

    /// <summary>
    /// Panel containing the list of selected allowed users or roles
    /// </summary>
    protected virtual Panel AllowedUsersOrRolesListPanel => this.Container.GetControl<Panel>("pnlAllowedUsersOrRolesList", true);

    /// <summary>
    /// Panel containing the list of selected denied users or roles
    /// </summary>
    protected virtual Panel DeniedUsersOrRolesListPanel => this.Container.GetControl<Panel>("pnlDeniedUsersOrRolesList", true);

    /// <summary>A LinkButton to be used to close the dialog</summary>
    protected virtual LinkButton CloseLinkButton => this.Container.GetControl<LinkButton>("btnClose", true);

    /// <summary>
    /// A LinkButton to be used to save data (and close the dialog)
    /// </summary>
    protected virtual LinkButton SaveLinkButton => this.Container.GetControl<LinkButton>("btnSave", true);

    /// <summary>
    /// A LinkButton to be used to show the user/role selection panel
    /// </summary>
    protected virtual LinkButton OpenUsersSelectionBoxLinkButton => this.Container.GetControl<LinkButton>("lnkOpeneUsersSelectionBox", true);

    /// <summary>
    /// A label containing the name of the action for which permissions are being edited
    /// </summary>
    protected virtual Literal ActionNameLabel => this.Container.GetControl<Literal>("lblActionName", true);

    /// <summary>
    /// A radio button for selecting "Everyone" role as allowed
    /// </summary>
    protected virtual RadioButton AllowedEveryoneRadioButton => this.Container.GetControl<RadioButton>("rbAllowedEveryone", true);

    /// <summary>
    /// A radio button for selecting "Everyone" role as allowed
    /// </summary>
    protected virtual RadioButton AllowedBackendUsersRadioButton => this.Container.GetControl<RadioButton>("rbAllowedBackendUsers", true);

    /// <summary>
    ///  A radio button for selecting only administrators as allowed
    /// </summary>
    protected virtual RadioButton AllowedAdminsOnlyRadioButton => this.Container.GetControl<RadioButton>("rbAllowedAdminsOnly", true);

    /// <summary>
    /// A radio button for selecting specific allowed users/roles (and not one of the listed application roles)
    /// </summary>
    protected virtual RadioButton AllowedSelectedUsersRadioButton => this.Container.GetControl<RadioButton>("rbAllowedSelectedUsers", true);

    /// <summary>Panel containing the selected denied users</summary>
    protected virtual Panel DeniedUsersPanel => this.Container.GetControl<Panel>("pnlDeniedUsers", true);

    protected virtual CheckBox ExplicitlyDeniedCheckbox => this.Container.GetControl<CheckBox>("chkExplicitlyDenied", true);

    /// <summary>
    /// A link button to open the user/role selection panel, for selecting denied users
    /// </summary>
    protected virtual LinkButton AddRolesOrUsersDeniedLinkButton => this.Container.GetControl<LinkButton>("btnAddRolesOrUsersDenied", true);

    /// <summary>Gets the principal selection panel.</summary>
    protected virtual Panel PrincipalSelectionPanel => this.Container.GetControl<Panel>("pnlPrincipalSelection", true);

    /// <summary>
    /// Gets the button for saving the selected principals, and closing the dialog.
    /// </summary>
    protected virtual LinkButton DoneSelectingLink => this.Container.GetControl<LinkButton>("lnkDoneSelecting", true);

    /// <summary>
    /// Gets the button for cancelling and closing the dialog.
    /// </summary>
    protected virtual LinkButton CancelSelectingLink => this.Container.GetControl<LinkButton>("lnkCancelSelecting", true);

    /// <summary>Gets the principal multi selector control.</summary>
    protected virtual MultiSelector PrincipalSelector => this.Container.GetControl<MultiSelector>("principalSelector", true);

    /// <summary>
    /// Gets the panel displaying a message when the listed permissions are inherited.
    /// </summary>
    protected virtual Panel InheritsPermissionsPanel => this.Container.GetControl<Panel>("pnlInheritsPermissions", true);

    /// <summary>Gets the panel for message information box.</summary>
    protected virtual Panel MessageInfoBoxPanel => this.Container.GetControl<Panel>("pnlMessageInfoBox", true);

    /// <summary>Gets the literal control for message information box.</summary>
    protected virtual Literal MessageInfoBoxLiteral => this.Container.GetControl<Literal>("ltrMessageInfoBox", true);

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ActionUsersSelection.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>PermissionSet of the edited permissions</summary>
    protected virtual string PermissionSetName => this.Page.Request.QueryString["permissionSetName"];

    /// <summary>Name of the action for which permissions are edited</summary>
    protected virtual string ActionName => this.Page.Request.QueryString["actionName"];

    /// <summary>Data provider for which permissions are edited</summary>
    protected virtual string DataProviderName => this.Page.Request.QueryString["dataProviderName"];

    /// <summary>
    /// Root ID of the object for which permissions are edited
    /// </summary>
    protected virtual string PermissionObjectRootID => this.Page.Request.QueryString["permissionObjectRootID"];

    /// <summary>
    /// Full name (incl. namespace) of the class to be used as manager for the edited permissions
    /// </summary>
    protected virtual string ManagerClassName => this.Page.Request.QueryString["managerClassName"];

    /// <summary>
    /// ID of the secured object (optional. If not specified, the SecurityRoot of the manager+provider is used).
    /// </summary>
    protected virtual Guid SecuredObjectID
    {
      get
      {
        Guid securedObjectId = new Guid(this.Page.Request.QueryString["securedObjectID"]);
        if (securedObjectId == Guid.Empty)
          securedObjectId = new Guid(this.Page.Request.QueryString["permissionObjectRootID"]);
        return securedObjectId;
      }
    }

    /// <summary>
    /// ID of the actual secured object (optional. If not specified, the SecuredObjectID is used).
    /// </summary>
    protected virtual Guid ActualSecuredObjectId
    {
      get
      {
        string input = this.Page.Request.QueryString["actualSecuredObjectId"];
        Guid result;
        return input != null && Guid.TryParse(input, out result) ? result : Guid.Empty;
      }
    }

    /// <summary>
    /// The assembly-qualified name of the type of the secured object, including the assembly name.
    /// E.g.: "Telerik.Sitefinity.Security.Model.SecurityRoot, Telerik.Sitefinity.Model"
    /// </summary>
    public string SecuredObjectType => this.Page.Request.QueryString["securedObjectType"];

    /// <summary>Gets the name of the dynamic data provider.</summary>
    /// <value>The name of the dynamic data provider.</value>
    public string DynamicDataProviderName => this.Page.Request.QueryString["dynamicDataProviderName"];

    /// <summary>
    /// Gets a value indicating whether this instance inherits permissions.
    /// </summary>
    public bool InheritsPermissions
    {
      get
      {
        bool result = false;
        bool.TryParse(this.Page.Request.QueryString["inheritsPermissions"], out result);
        return result;
      }
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.</returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptBehaviorDescriptor behaviorDescriptor = new ScriptBehaviorDescriptor(typeof (ActionUsersSelection).FullName, this.ClientID);
      behaviorDescriptor.AddProperty("_onLoadPermissions", (object) new JavaScriptSerializer().Serialize((object) this.OnLoadPermissions.ToArray()));
      behaviorDescriptor.AddProperty("_actionName", (object) this.ActionName);
      behaviorDescriptor.AddProperty("_permissionObjectRootID", (object) this.PermissionObjectRootID);
      behaviorDescriptor.AddProperty("_permissionSetName", (object) this.PermissionSetName);
      behaviorDescriptor.AddProperty("_dataProviderName", (object) this.DataProviderName);
      behaviorDescriptor.AddProperty("_managerClassName", (object) this.ManagerClassName);
      behaviorDescriptor.AddProperty("_permissionsUrl", (object) this.permissionsServiceUrl);
      behaviorDescriptor.AddProperty("_allowedSelectedUsersRadioButtonID", (object) this.AllowedSelectedUsersRadioButton.ClientID);
      behaviorDescriptor.AddProperty("_allowedUsersOrRolesListPanelID", (object) this.AllowedUsersOrRolesListPanel.ClientID);
      behaviorDescriptor.AddProperty("_saveLinkButtonID", (object) this.SaveLinkButton.ClientID);
      behaviorDescriptor.AddProperty("_closeLinkButtonID", (object) this.CloseLinkButton.ClientID);
      behaviorDescriptor.AddProperty("_principalRemoveText", (object) "delete");
      behaviorDescriptor.AddProperty("_addRolesOrUsersDeniedLinkButtonID", (object) this.AddRolesOrUsersDeniedLinkButton.ClientID);
      behaviorDescriptor.AddProperty("_ctrlID", (object) this.ClientID);
      behaviorDescriptor.AddProperty("_securedObjectID", (object) this.SecuredObjectID);
      behaviorDescriptor.AddProperty("_actualSecuredObjectId", (object) this.ActualSecuredObjectId);
      behaviorDescriptor.AddProperty("_dynamicDataProviderName", (object) this.DynamicDataProviderName);
      behaviorDescriptor.AddProperty("_securedObjectType", (object) this.SecuredObjectType);
      behaviorDescriptor.AddProperty("_allowedEveryoneRadioButtonID", (object) this.AllowedEveryoneRadioButton.ClientID);
      behaviorDescriptor.AddProperty("_allowedBackendUsersRadioButtonID", (object) this.AllowedBackendUsersRadioButton.ClientID);
      behaviorDescriptor.AddProperty("_allowedAdminsOnlyRadioButtonID", (object) this.AllowedAdminsOnlyRadioButton.ClientID);
      behaviorDescriptor.AddProperty("_administratorRoleSelected", (object) this.administratorRoleSelected.ToString().ToLower());
      behaviorDescriptor.AddProperty("_everyoneRoleSelected", (object) this.everyoneRoleSelected.ToString().ToLower());
      behaviorDescriptor.AddProperty("_backendRoleSelected", (object) this.backendRoleSelected.ToString().ToLower());
      behaviorDescriptor.AddProperty("_specificPrincipalsSelected", (object) this.specificPrincipalsSelected.ToString().ToLower());
      behaviorDescriptor.AddProperty("_everyoneRoleID", (object) this.everyoneRoleID);
      behaviorDescriptor.AddProperty("_backendRoleID", (object) this.backendRoleID);
      behaviorDescriptor.AddProperty("_principalSelectionPanelID", (object) this.PrincipalSelectionPanel.ClientID);
      behaviorDescriptor.AddProperty("_specificUsersRadioInactiveHtml", (object) Res.Get<SecurityResources>().SpecificSelectedUsersInactive);
      behaviorDescriptor.AddProperty("_specificUsersRadioActiveHtml", (object) Res.Get<SecurityResources>().SpecificSelectedUsersActive);
      behaviorDescriptor.AddProperty("_SpecificDeniedUsersActiveHtml", (object) Res.Get<SecurityResources>().SpecificDeniedUsersActive);
      behaviorDescriptor.AddProperty("_SpecificDeniedUsersInactiveHtml", (object) Res.Get<SecurityResources>().SpecificDeniedUsersInactive);
      behaviorDescriptor.AddProperty("_explicitlyDeniedCheckboxID", (object) this.ExplicitlyDeniedCheckbox.ClientID);
      behaviorDescriptor.AddProperty("_deniedUsersOrRolesListPanelID", (object) this.DeniedUsersOrRolesListPanel.ClientID);
      behaviorDescriptor.AddProperty("_openUsersSelectionBoxLinkButtonID", (object) this.OpenUsersSelectionBoxLinkButton.ClientID);
      behaviorDescriptor.AddProperty("_doneSelectingLinkID", (object) this.DoneSelectingLink.ClientID);
      behaviorDescriptor.AddProperty("_cancelSelectingLinkID", (object) this.CancelSelectingLink.ClientID);
      behaviorDescriptor.AddComponentProperty("principalSelector", this.PrincipalSelector.ClientID);
      behaviorDescriptor.AddProperty("_usersSelectionMode", (object) JsonUtility.EnumToJson(typeof (UsersSelectionMode)));
      behaviorDescriptor.AddProperty("_wcfPrincipalType", (object) JsonUtility.EnumToJson(typeof (WcfPrincipalType)));
      behaviorDescriptor.AddProperty("_everyoneRoleName", (object) Res.Get<SecurityResources>().Everyone);
      behaviorDescriptor.AddProperty("_backendRoleName", (object) Res.Get<SecurityResources>().BackendUsersRole);
      behaviorDescriptor.AddProperty("_inheritsPermissions", (object) this.InheritsPermissions.ToString().ToLower());
      return (IEnumerable<ScriptDescriptor>) new List<ScriptDescriptor>(base.GetScriptDescriptors())
      {
        (ScriptDescriptor) behaviorDescriptor
      }.ToArray();
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.</returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string str = typeof (ActionUsersSelection).Assembly.GetName().ToString();
      ScriptReference scriptReference1 = new ScriptReference()
      {
        Assembly = str,
        Name = "Telerik.Sitefinity.Web.UI.Backend.Security.Permissions.Scripts.ActionUsersSelection.js"
      };
      ScriptReference scriptReference2 = new ScriptReference()
      {
        Assembly = str,
        Name = "Telerik.Sitefinity.Web.Scripts.ClientManager.js"
      };
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        scriptReference1,
        scriptReference2
      }.ToArray();
    }

    private void DisplayMessageInfoBox()
    {
      if (ManagerBase.GetManager(WcfHelper.DecodeWcfString(this.ManagerClassName), this.DataProviderName) is PageManager)
        return;
      string str = string.Empty;
      string actionName = this.ActionName;
      if (!(actionName == "ViewBackendLink"))
      {
        if (actionName == "View" && !this.IsFilterQueriesByViewPermissionsEnabled())
          str = Res.Get<SecurityResources>().ViewPermissionTurnedOffMessage.Arrange((object) Res.Get<SecurityResources>().ExternalLinkViewPermissionTurnedOffMessage);
      }
      else
        str = Res.Get<SecurityResources>().ViewBackendPermissionsDefaultMessage;
      if (string.IsNullOrWhiteSpace(str))
        return;
      this.MessageInfoBoxPanel.Visible = true;
      this.MessageInfoBoxLiteral.Text = str;
    }

    private bool IsFilterQueriesByViewPermissionsEnabled()
    {
      IManager manager = ManagerBase.GetManager(WcfHelper.DecodeWcfString(this.ManagerClassName), this.DataProviderName);
      return manager.Provider != null ? manager.Provider.FilterQueriesByViewPermissions : Config.Get<SecurityConfig>().FilterQueriesByViewPermissions;
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;
  }
}
