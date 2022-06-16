// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Security.Permissions.UserPermissionsEditor
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.WcfHelpers;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Security.Web.Services;

namespace Telerik.Sitefinity.Web.UI.Backend.Security.Permissions
{
  /// <summary>
  /// Represents the UserPermissionsEditor dialog, for settings permissions for various actions, for a specific user
  /// </summary>
  public class UserPermissionsEditor : AjaxDialogBase
  {
    /// <summary>
    /// A list of client IDs of the checkboxes which are to be checked when the control loads
    /// </summary>
    protected List<string> CheckedOnLoadClientIDs = new List<string>();
    /// <summary>
    /// A list of client IDs of the Deny column controls (in order to toggle them via the client)
    /// </summary>
    protected List<string> DenyButtonsColumn = new List<string>();
    /// <summary>
    /// Whether or not the Deny column should be hidden when the control loads
    /// </summary>
    protected bool showDenyColumnOnStartup;
    /// <summary>
    /// A list of WcfPermission objects, representing the premissions to load when the control loads
    /// </summary>
    protected List<WcfPermission> WcfPermissionArray = new List<WcfPermission>();
    /// <summary>The root ID of the edited secured object</summary>
    protected Guid PermissionsObjectRootID;
    /// <summary>
    /// URL of the Permission service to use for getting/saving data
    /// </summary>
    protected string PrincipalServiceURL = string.Empty;
    private List<Telerik.Sitefinity.Security.Model.Permission> userPermissions = new List<Telerik.Sitefinity.Security.Model.Permission>();
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Security.Permissions.UserPermissionsEditor.ascx");
    private ISecuredObject securedObject;

    /// <summary>Initializes the controls.</summary>
    /// <param name="dialogContainer">The container of the control</param>
    protected override void InitializeControls(GenericContainer dialogContainer)
    {
      this.UserOrRoleNameLabel.Text = SecurityManager.GetPrincipalName(this.PrincipalID);
      this.PermissionNameLabel.Text = Config.Get<SecurityConfig>().Permissions[this.PermissionsSetName].Name;
      this.AllowActionLabel.Text = Res.Get<SecurityResources>().AllowUserPermissionsList;
      this.DenyActionLabel.Text = Res.Get<SecurityResources>().DenyUserPermissionsList;
      this.CloseLinkButton.Text = Res.Get<SecurityResources>().UserPermissionsEditorCancelBtn;
      IManager manager = ManagerBase.GetManager(this.ManagerClassName, this.DataProviderName);
      Guid myPrincipalId = this.PrincipalID;
      string permSet = this.PermissionsSetName;
      this.securedObject = SecurityUtility.GetSecuredObject(manager, this.SecuredObjectType, this.SecuredObjectID, this.DynamicDataProviderName);
      this.userPermissions = this.securedObject.GetActivePermissions().Where<Telerik.Sitefinity.Security.Model.Permission>((Expression<Func<Telerik.Sitefinity.Security.Model.Permission, bool>>) (p => p.PrincipalId == myPrincipalId && p.SetName == permSet && p.ObjectId == this.securedObject.Id)).ToList<Telerik.Sitefinity.Security.Model.Permission>();
      this.PrincipalServiceURL = this.ResolveClientUrl("~/Sitefinity/Services/Security/Permissions.svc");
      this.UserOrRoleNameLabel.Attributes.Add("style", "display:" + (this.ShowPrincipalName ? "block" : "none"));
    }

    /// <summary>
    /// Handle the ItemDataBound event on the repeater listing actions and allow/deny checkboxes
    /// </summary>
    /// <param name="sender">Not used.</param>
    /// <param name="e">A <see cref="T:System.Web.UI.WebControls.RepeaterItemEventArgs" /> that contains the event data.</param>
    private void rptActions_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      Label control1 = e.Item.FindControl("lblActionName") as Label;
      CheckBox control2 = e.Item.FindControl("chkAllowed") as CheckBox;
      CheckBox control3 = e.Item.FindControl("chkDenied") as CheckBox;
      WcfPermission wcfPermission = new WcfPermission();
      wcfPermission.PrincipalID = this.PrincipalID.ToString();
      wcfPermission.PrincipalName = SecurityManager.GetPrincipalName(this.PrincipalID);
      string title = ((SecurityAction) e.Item.DataItem).GetTitle(this.securedObject);
      wcfPermission.ActionName = ((SecurityAction) e.Item.DataItem).Name;
      wcfPermission.ActionTitle = title;
      wcfPermission.AllowControlClientID = control2.ClientID;
      wcfPermission.DenyControlClientID = control3.ClientID;
      this.DenyButtonsColumn.Add(control3.ClientID);
      string str = title;
      control1.Text = str;
      wcfPermission.IsAllowed = false;
      wcfPermission.IsDenied = false;
      foreach (Telerik.Sitefinity.Security.Model.Permission userPermission in this.userPermissions)
      {
        if (userPermission.IsGranted(((SecurityAction) e.Item.DataItem).Name))
        {
          wcfPermission.IsAllowed = true;
          this.CheckedOnLoadClientIDs.Add(control2.ClientID);
        }
        if (userPermission.IsDenied(((SecurityAction) e.Item.DataItem).Name))
        {
          this.CheckedOnLoadClientIDs.Add(control3.ClientID);
          wcfPermission.IsDenied = true;
          this.showDenyColumnOnStartup = true;
        }
      }
      this.WcfPermissionArray.Add(wcfPermission);
    }

    /// <summary>Event handler for the PreRender lifecycle event</summary>
    /// <param name="e">Not used.</param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      this.ActionsRepeater.ItemDataBound += new RepeaterItemEventHandler(this.rptActions_ItemDataBound);
      this.ActionsRepeater.DataSource = (object) Config.Get<SecurityConfig>().Permissions[this.PermissionsSetName].Actions;
      this.DenyButtonsColumn.Add(this.DenyActionLabel.ClientID);
      this.ActionsRepeater.DataBind();
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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? UserPermissionsEditor.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.</returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptBehaviorDescriptor behaviorDescriptor = new ScriptBehaviorDescriptor(this.GetType().ToString(), this.ClientID);
      behaviorDescriptor.AddProperty("arrCheckedOnLoadClientIDs", (object) this.CheckedOnLoadClientIDs.ToArray());
      behaviorDescriptor.AddProperty("arrDenyButtonsColumn", (object) this.DenyButtonsColumn.ToArray());
      behaviorDescriptor.AddProperty("bShowDenyColumnOnStartup", (object) this.showDenyColumnOnStartup);
      behaviorDescriptor.AddProperty("showAdvancedOptionsLinkButtonID", (object) this.ShowAdvancedOptionsLinkButton.ClientID);
      behaviorDescriptor.AddProperty("hideAdvancedOptionsLinkButtonID", (object) this.HideAdvancedOptionsLinkButton.ClientID);
      behaviorDescriptor.AddProperty("saveLinkButtonClientID", (object) this.SaveLinkButton.ClientID);
      behaviorDescriptor.AddProperty("closeLinkButtonClientID", (object) this.CloseLinkButton.ClientID);
      behaviorDescriptor.AddProperty("wcfPermissionArray", (object) this.WcfPermissionArray.ToArray());
      behaviorDescriptor.AddProperty("dataProviderName", (object) this.DataProviderName);
      behaviorDescriptor.AddProperty("dynamicDataProviderName", (object) this.DynamicDataProviderName);
      behaviorDescriptor.AddProperty("managerClassName", (object) WcfHelper.EncodeWcfString(this.ManagerClassName));
      behaviorDescriptor.AddProperty("principalType", SecurityManager.IsPrincipalUser(this.PrincipalID) ? (object) "User" : (object) "Role");
      behaviorDescriptor.AddProperty("permissionsSetName", (object) this.PermissionsSetName);
      behaviorDescriptor.AddProperty("principalID", (object) this.PrincipalID.ToString());
      behaviorDescriptor.AddProperty("permissionsObjectRootID", (object) this.PermissionsObjectRootID.ToString());
      behaviorDescriptor.AddProperty("principalServiceURL", (object) this.PrincipalServiceURL);
      behaviorDescriptor.AddProperty("securedObjectID", (object) this.SecuredObjectID);
      behaviorDescriptor.AddProperty("securedObjectType", (object) this.SecuredObjectType);
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
      string str = this.GetType().Assembly.GetName().ToString();
      ScriptReference scriptReference1 = new ScriptReference()
      {
        Assembly = str,
        Name = "Telerik.Sitefinity.Web.UI.Backend.Security.Permissions.Scripts.UserPermissionsEditor.js"
      };
      ScriptReference scriptReference2 = new ScriptReference()
      {
        Assembly = str,
        Name = "Telerik.Sitefinity.Web.Scripts.ClientManager.js"
      };
      ScriptReference scriptReference3 = new ScriptReference()
      {
        Assembly = "Telerik.Web.UI",
        Name = "Telerik.Web.UI.Common.Core.js"
      };
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        scriptReference1,
        scriptReference2,
        scriptReference3
      }.ToArray();
    }

    /// <summary>ID of the principal (user/role) to bind to</summary>
    protected virtual Guid PrincipalID => new Guid(this.Page.Request.QueryString["principalID"].ToString());

    /// <summary>
    /// ID of the secured object (optional. If not specified, the SecurityRoot of the manager+provider is used).
    /// </summary>
    protected virtual Guid SecuredObjectID => new Guid(this.Page.Request.QueryString["securedObjectID"]);

    /// <summary>
    /// The assembly-qualified name of the type of the secured object, including the assembly name.
    /// E.g.: "Telerik.Sitefinity.Security.Model.SecurityRoot, Telerik.Sitefinity.Model"
    /// </summary>
    public string SecuredObjectType => this.Page.Request.QueryString["securedObjectType"];

    /// <summary>
    /// Full name (incl. namespace) of the class to be used as manager for the edited permissions
    /// </summary>
    protected virtual string ManagerClassName => WcfHelper.DecodeWcfString(this.Page.Request.QueryString["managerClassName"].ToString());

    /// <summary>Data provider for which permissions are edited</summary>
    protected virtual string DataProviderName => this.Page.Request.QueryString["dataProviderName"].ToString();

    /// <summary>
    /// Dynamic module data provider for which permissions are edited
    /// </summary>
    protected virtual string DynamicDataProviderName => this.Page.Request.QueryString["dynamicDataProviderName"].ToString();

    /// <summary>PermissionSet of the edited permissions</summary>
    protected virtual string PermissionsSetName => this.Page.Request.QueryString["permissionsSetName"].ToString();

    /// <summary>Whether or not the user/role name is displayed</summary>
    protected virtual bool ShowPrincipalName => this.Page.Request.QueryString["showPrincipalName"].ToString().ToLower().Trim() == "true";

    /// <summary>
    /// Repater for listing the actions related to the permission set
    /// </summary>
    protected virtual Repeater ActionsRepeater => this.Container.GetControl<Repeater>("rptActions", true);

    /// <summary>Label displaying the user/role name</summary>
    protected virtual Label UserOrRoleNameLabel => this.Container.GetControl<Label>("lblUserOrRoleName", true);

    /// <summary>Label displaying the permission set name</summary>
    protected virtual Label PermissionNameLabel => this.Container.GetControl<Label>("lblPermissionName", true);

    /// <summary>Table caption for the "Allow" title</summary>
    protected virtual Label AllowActionLabel => this.Container.GetControl<Label>("lblAllowAction", true);

    /// <summary>Table caption for the "Deny" title</summary>
    protected virtual Label DenyActionLabel => this.Container.GetControl<Label>("lblDenyAction", true);

    /// <summary>
    /// LinkButton to show advanced options for editing (namely the Deny column)
    /// </summary>
    protected virtual LinkButton ShowAdvancedOptionsLinkButton => this.Container.GetControl<LinkButton>("lnkShowAdvancedOptions", true);

    /// <summary>
    /// LinkButton to hide advanced options for editing (namely the Deny column)
    /// </summary>
    protected virtual LinkButton HideAdvancedOptionsLinkButton => this.Container.GetControl<LinkButton>("lnkHideAdvancedOptions", true);

    /// <summary>
    /// LinkButton for saving the data (and closing the dialog)
    /// </summary>
    protected virtual LinkButton SaveLinkButton => this.Container.GetControl<LinkButton>("btnSave", true);

    /// <summary>LinkButton for closing the dialog</summary>
    protected virtual LinkButton CloseLinkButton => this.Container.GetControl<LinkButton>("btnClose", true);
  }
}
