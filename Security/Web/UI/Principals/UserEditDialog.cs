// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.Principals.UserEditDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Data;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Security.Sanitizers;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail;

namespace Telerik.Sitefinity.Security.Web.UI.Principals
{
  /// <summary>The dialog for editing user.</summary>
  public class UserEditDialog : AjaxDialogBase
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Security.Principals.UserEdit.ascx");
    internal const string scriptName = "Telerik.Sitefinity.Security.Web.UI.Scripts.UserEditDialog.js";
    internal const string wcfHelperScriptName = "Telerik.Sitefinity.Web.Scripts.WcfHelpers.js";
    private const string usersServiceUrl = "~/Sitefinity/Services/Security/Users.svc";
    private const string forceLogoutUserServiceUrl = "~/Sitefinity/Services/Security/Users.svc/ForceLogout/";
    private List<CheckBoxInfo> checkboxes = new List<CheckBoxInfo>();

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? UserEditDialog.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    public override string ClientComponentType => typeof (UserEditDialog).FullName;

    /// <summary>
    /// Gets the reference to the hidden field that holds the url of the web service
    /// used to manage users.
    /// </summary>
    protected virtual HiddenField UsersServiceUrl => this.Container.GetControl<HiddenField>("usersServiceUrl", true);

    /// <summary>Gets the force logout user service URL control.</summary>
    /// <value>The force logout user service URL control .</value>
    protected virtual HiddenField ForceLogoutUserServiceUrl => this.Container.GetControl<HiddenField>("hfForceLogoutServiceUrl", true);

    /// <summary>Repeater of checkboxes for each user's role</summary>
    protected virtual Repeater RolesList => this.Container.GetControl<Repeater>("rptRolesList", true);

    /// <summary>
    /// A hidden field to hold the checkbox client-side information, in JSON format
    /// </summary>
    protected virtual HiddenField CheckBoxesDetails => this.Container.GetControl<HiddenField>("hCheckBoxesDetails", true);

    /// <summary>Gets the backend users role id hidden field.</summary>
    /// <value>The backend users role id hidden field.</value>
    protected virtual HiddenField BackendUsersRoleId => this.Container.GetControl<HiddenField>("hBackendUsersRoleId", true);

    /// <summary>Gets the administrators role id hidden field.</summary>
    /// <value>The administrators role id hidden field.</value>
    protected virtual HiddenField AdministratorsRoleId => this.Container.GetControl<HiddenField>("hAdministratorsRoleId", true);

    /// <summary>
    /// Gets the user checkbox determining whether this user is a backend user.
    /// </summary>
    /// <value>The user checkbox determining whether this user is a backend user.</value>
    protected virtual CheckBox UserIsBackendUserCheckBox => this.Container.GetControl<CheckBox>("chkUserIsBackendUser", true);

    /// <summary>
    /// Gets the hidden field containing the name of the backend role provider.
    /// </summary>
    /// <value>The the hidden field containing the name of the backend role provider.</value>
    protected virtual HiddenField BackendRoleProviderName => this.Container.GetControl<HiddenField>("hBackendRoleProviderName", true);

    /// <summary>
    /// Gets the hidden field containing the IDs of roles related to backend access.
    /// </summary>
    /// <value>The the hidden field containing the IDs of roles related to backend access.</value>
    protected virtual HiddenField BackendRelatedRoleIds => this.Container.GetControl<HiddenField>("hBackendRelatedRoleIds", true);

    /// <summary>Gets the profile details view</summary>
    /// <value>The profile details.</value>
    protected virtual DetailFormView ProfileDetails => this.Container.GetControl<DetailFormView>("profileDetails", true);

    /// <summary>
    /// Gets the panel where the UserIsBackendUserCheckBox and InOrderToManageContentOrSettingsLiteral are located.
    /// </summary>
    protected virtual HtmlGenericControl UserIsBackendUserPanel => this.Container.GetControl<HtmlGenericControl>("userIsBackendUserPanel", true);

    /// <summary>
    /// Initializes all controls instantiated in the layout container. This method is called at appropriate time for setting initial values and subscribing for events of layout controls.
    /// </summary>
    /// <param name="dialogContainer">The dialog container.</param>
    protected override void InitializeControls(GenericContainer dialogContainer)
    {
      this.UsersServiceUrl.Value = this.ResolveClientUrl("~/Sitefinity/Services/Security/Users.svc");
      this.ForceLogoutUserServiceUrl.Value = this.ResolveClientUrl("~/Sitefinity/Services/Security/Users.svc/ForceLogout/");
      SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
      IDictionary<string, Dictionary<string, List<string>>> providersMapping = UserManager.GetExternalProvidersMapping();
      JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
      this.AssignHfValue(dialogContainer, "hfExternalProvidersMappings", scriptSerializer.Serialize((object) providersMapping));
      this.AssignHfValue(dialogContainer, "hfExternalUserMessage", ObjectFactory.Resolve<IHtmlSanitizer>().Sanitize(Res.Get<Labels>().UserRegisteredWith));
      this.AssignHfValue(dialogContainer, "hfIsUnrestricted", currentIdentity.IsUnrestricted.ToString());
      this.AssignHfValue(dialogContainer, "hfIsBackend", currentIdentity.IsBackendUser.ToString());
      this.AssignHfValue(dialogContainer, "hfIsGlobalUser", currentIdentity.IsGlobalUser.ToString());
      this.AssignHfValue(dialogContainer, "hfCurrendUserId", currentIdentity.UserId.ToString());
      this.AssignHfValue(dialogContainer, "hfAllowSeparateUsersPerSite", SecurityManager.AllowSeparateUsersPerSite.ToString());
      this.RolesList.ItemDataBound += new RepeaterItemEventHandler(this.RolesList_ItemDataBound);
      this.RolesList.DataSource = (object) this.GetAllRoleProviders();
      this.RolesList.DataBind();
      this.BackendUsersRoleId.Value = SecurityManager.BackEndUsersRole.Id.ToString();
      this.AdministratorsRoleId.Value = SecurityManager.AdminRole.Id.ToString();
      string provider = string.Empty;
      SecurityManager.GetRole(SecurityManager.BackEndUsersRole.Id, out provider);
      this.BackendRoleProviderName.Value = provider;
    }

    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
      this.CheckBoxesDetails.Value = scriptSerializer.Serialize((object) this.checkboxes);
      this.BackendRelatedRoleIds.Value = scriptSerializer.Serialize((object) new string[5]
      {
        SecurityManager.AdminRole.Id.ToString(),
        SecurityManager.AuthorsRole.Id.ToString(),
        SecurityManager.BackEndUsersRole.Id.ToString(),
        SecurityManager.DesignersRole.Id.ToString(),
        SecurityManager.EditorsRole.Id.ToString()
      });
    }

    /// <summary>
    /// Event handler for the ItemDataBound event, of the role's list repeater
    /// </summary>
    /// <param name="sender">The sender object</param>
    /// <param name="e">Event arguments</param>
    private void RolesList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      RoleProviderPair dataItem = (RoleProviderPair) e.Item.DataItem;
      if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
        return;
      CheckBox control1 = e.Item.FindControl("role") as CheckBox;
      Label control2 = e.Item.FindControl("lblRole") as Label;
      control2.AssociatedControlID = control1.ID;
      string text = ManagerBase<RoleDataProvider>.StaticProvidersCollection.Count < 3 ? string.Format("{0}", (object) dataItem.RoleItem.Name) : string.Format("{0} ({1})", (object) dataItem.RoleItem.Name, (object) dataItem.ProviderItem.Title);
      this.checkboxes.Add(new CheckBoxInfo((WebControl) control1, (WebControl) control2, text, string.Format("{0}~{1}", (object) dataItem.RoleItem.Id.ToString(), (object) dataItem.ProviderItem.Name)));
    }

    private void AssignHfValue(GenericContainer container, string hfName, string value)
    {
      HiddenField control = container.GetControl<HiddenField>(hfName, false);
      if (control == null)
        return;
      control.Value = value;
    }

    /// <summary>
    /// Gets a list of all roles, each associated with its provider
    /// </summary>
    /// <returns>A list of RoleProviderPair items</returns>
    private List<RoleProviderPair> GetAllRoleProviders()
    {
      List<RoleProviderPair> allRoleProviders = new List<RoleProviderPair>();
      foreach (RoleDataProvider providerItem in (IEnumerable<RoleDataProvider>) ManagerBase<RoleDataProvider>.StaticProvidersCollection.OrderBy<RoleDataProvider, string>((Func<RoleDataProvider, string>) (prov => prov.Name)))
      {
        if (!providerItem.Abilities.Keys.Contains<string>("AssingUserToRole") && !providerItem.Abilities.Keys.Contains<string>("UnAssingUserFromRole") || providerItem.Abilities["AssingUserToRole"].Supported && providerItem.Abilities["UnAssingUserFromRole"].Supported)
        {
          IQueryable<Role> roles = providerItem.GetRoles();
          Expression<Func<Role, string>> keySelector = (Expression<Func<Role, string>>) (r => r.Name);
          foreach (Role roleItem in (IEnumerable<Role>) roles.OrderBy<Role, string>(keySelector))
          {
            if (!SecurityManager.UnassignableRoles.Contains(roleItem.Id) && roleItem.Id != SecurityManager.BackEndUsersRole.Id)
              allRoleProviders.Add(new RoleProviderPair(roleItem, providerItem));
          }
        }
      }
      return allRoleProviders;
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      IEnumerable<ScriptDescriptor> scriptDescriptors = base.GetScriptDescriptors();
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) scriptDescriptors.Last<ScriptDescriptor>();
      JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
      return scriptDescriptors;
    }

    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string fullName = typeof (UserEditDialog).Assembly.FullName;
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Security.Web.UI.Scripts.UserEditDialog.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Web.Scripts.WcfHelpers.js", fullName)
      };
    }
  }
}
