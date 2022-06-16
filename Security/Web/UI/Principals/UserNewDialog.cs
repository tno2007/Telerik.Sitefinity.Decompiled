// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.Principals.UserNewDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Security.Data;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Security.Web.UI.Principals
{
  /// <summary>The dialog window for adding new user.</summary>
  public class UserNewDialog : UserEditDialog
  {
    public new static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Security.Principals.UserNew.ascx");
    private List<CheckBoxInfo> checkboxes = new List<CheckBoxInfo>();

    /// <summary>A panel containing the providers seletion</summary>
    protected virtual Control ProviderPanel => this.Container.GetControl<Control>("providerPanel", true);

    /// <summary>List of membership providers</summary>
    protected virtual ListControl MembershipProviderList => this.Container.GetControl<ListControl>("membershipProviderList", true);

    /// <summary>Repeater of checkboxes for each user's role</summary>
    protected new virtual Repeater RolesList => this.Container.GetControl<Repeater>("rptRolesList", true);

    /// <summary>
    /// A hidden field to hold the checkbox client-side information, in JSON format
    /// </summary>
    protected new virtual HiddenField CheckBoxesDetails => this.Container.GetControl<HiddenField>("hCheckBoxesDetails", true);

    /// <summary>Gets the backend users role id hidden field.</summary>
    /// <value>The backend users role id hidden field.</value>
    protected new virtual HiddenField BackendUsersRoleId => this.Container.GetControl<HiddenField>("hBackendUsersRoleId", true);

    /// <summary>Gets the administrators role id hidden field.</summary>
    /// <value>The administrators role id hidden field.</value>
    protected new virtual HiddenField AdministratorsRoleId => this.Container.GetControl<HiddenField>("hAdministratorsRoleId", true);

    /// <summary>
    /// Gets the user checkbox determining whether this user is a backend user.
    /// </summary>
    /// <value>The user checkbox determining whether this user is a backend user.</value>
    protected new virtual CheckBox UserIsBackendUserCheckBox => this.Container.GetControl<CheckBox>("chkUserIsBackendUser", true);

    /// <summary>
    /// Gets the hidden field containing the name of the backend role provider.
    /// </summary>
    /// <value>The the hidden field containing the name of the backend role provider.</value>
    protected new virtual HiddenField BackendRoleProviderName => this.Container.GetControl<HiddenField>("hBackendRoleProviderName", true);

    /// <summary>
    /// Gets the hidden field containing the IDs of roles related to backend access.
    /// </summary>
    /// <value>The the hidden field containing the IDs of roles related to backend access.</value>
    protected new virtual HiddenField BackendRelatedRoleIds => this.Container.GetControl<HiddenField>("hBackendRelatedRoleIds", true);

    /// <summary>ClientLableManager.</summary>
    /// <value>Client Lable Manager</value>
    protected virtual ClientLabelManager ClientLableManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>
    /// Gets the panel where the UserIsBackendUserCheckBox and InOrderToManageContentOrSettingsLiteral are located.
    /// </summary>
    protected new virtual HtmlGenericControl UserIsBackendUserPanel => this.Container.GetControl<HtmlGenericControl>("userIsBackendUserPanel", true);

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
      get => !string.IsNullOrEmpty(UserNewDialog.layoutTemplatePath) ? UserNewDialog.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    public string NewUserProfileJson { get; set; }

    /// <summary>Initializes the controls.</summary>
    /// <param name="dialogContainer">The container</param>
    protected override void InitializeControls(GenericContainer dialogContainer)
    {
      base.InitializeControls(dialogContainer);
      this.BindMembershipProviders();
      this.RolesList.ItemDataBound += new RepeaterItemEventHandler(this.RolesList_ItemDataBound);
      this.RolesList.DataSource = (object) this.GetAllRoleProviders();
      this.RolesList.DataBind();
      this.BackendUsersRoleId.Value = SecurityManager.BackEndUsersRole.Id.ToString();
      this.AdministratorsRoleId.Value = SecurityManager.AdminRole.Id.ToString();
      string provider = string.Empty;
      SecurityManager.GetRole(SecurityManager.BackEndUsersRole.Id, out provider);
      this.BackendRoleProviderName.Value = provider;
      this.NewUserProfileJson = new UserProfilesSerializer().Serialize((object) UserProfilesHelper.GetEmptyUserProfiles((string) null));
    }

    /// <summary>Event handler for the PreRender lifecycle event</summary>
    /// <param name="e">Not used.</param>
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

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

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

    /// <summary>Initializes the list of providers displayed</summary>
    private void BindMembershipProviders()
    {
      IEnumerable<MembershipDataProvider> membershipDataProviders = UserManager.GetManager().GetContextProviders().Cast<MembershipDataProvider>();
      this.ProviderPanel.Visible = true;
      foreach (MembershipDataProvider provider in membershipDataProviders)
      {
        if (!provider.Abilities.Keys.Contains<string>("AddUser") || provider.Abilities["AddUser"].Supported)
        {
          this.MembershipProviderList.Items.Add(new ListItem(provider.Name, provider.Name));
          string requirementsText = SecurityUtility.GetPasswordRequirementsText(provider);
          Collection<ClientLableBase> labels = this.ClientLableManager.Labels;
          ClientMesasge clientMesasge = new ClientMesasge();
          clientMesasge.Key = provider.Name + "PasswordExample";
          clientMesasge.Mesasge = requirementsText;
          labels.Add((ClientLableBase) clientMesasge);
        }
      }
      if (this.MembershipProviderList.Items.Count >= 2)
        return;
      this.ProviderPanel.Visible = false;
    }

    public override string ClientComponentType => typeof (UserEditDialog).FullName;
  }
}
