// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.Principals.RolesView`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Security.Data;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Security.Web.UI.Principals
{
  /// <summary>The view for managing the roles.</summary>
  /// <typeparam name="THost">The type of the host.</typeparam>
  public class RolesView<THost> : ViewModeControl<THost>, IScriptControl
    where THost : Control
  {
    private const string webServiceUrl = "~/Sitefinity/Services/Security/Roles.svc";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Security.Principals.RolesView.ascx");

    /// <summary>
    /// Gets the name of the embedded layout template. If the control uses layout template this
    /// property must be overridden to provide the path (key) to an embedded resource file.
    /// </summary>
    /// <value></value>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? RolesView<THost>.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Returns a GridView object</summary>
    protected virtual RadGrid Grid => this.Container.GetControl<RadGrid>();

    /// <summary>Returns the RolesGridBinder</summary>
    protected virtual RadGridBinder RolesGridBinder => this.Container.GetControl<RadGridBinder>("rolesBinder", true);

    /// <summary>
    /// Gets the control displaying list of providers for creating a new role.
    /// </summary>
    /// <value>The instance of the control.</value>
    protected virtual ListControl CreateProvidersList => this.Container.GetControl<ListControl>("createProvidersList", true);

    /// <summary>
    /// Gets the reference to the hidden field which holds the url of the roles web service
    /// </summary>
    protected virtual HiddenField WebServiceUrl => this.Container.GetControl<HiddenField>("webServiceUrl", true);

    /// <summary>
    /// Gets A hidden field containing the service URL for getting updated provider information
    /// </summary>
    /// <value>A hidden field containing the service URL for getting updated provider information.</value>
    protected virtual HiddenField hProvidersServiceUrl => this.Container.GetControl<HiddenField>(nameof (hProvidersServiceUrl), true);

    /// <summary>A hidden field containing the admin role ID.</summary>
    /// <value>The admin role ID.</value>
    protected virtual HiddenField AdminRoleID => this.Container.GetControl<HiddenField>("adminRoleID", true);

    /// <summary>Gets A hidden field containing the backend role ID.</summary>
    /// <value>The backend role ID.</value>
    protected virtual HiddenField BackendRoleID => this.Container.GetControl<HiddenField>("backendRoleID", true);

    /// <summary>Gets A hidden field containing the roles filter.</summary>
    /// <value>The h roles filter.</value>
    protected virtual HiddenField hRolesFilter => this.Container.GetControl<HiddenField>("rolesFilter", true);

    /// <summary>Gets the flat provider selector panel.</summary>
    /// <value>The provider selector panel.</value>
    protected virtual FlatProviderSelector ProviderSelectorPanel => this.Container.GetControl<FlatProviderSelector>("providerSelectorPanel", true);

    /// <summary>
    /// Initializes all controls instantiated in the layout container. This method is called at appropriate time for setting initial values and subscribing for events of layout controls.
    /// </summary>
    /// <param name="viewContainer">The control that will host the current view.</param>
    protected override void InitializeControls(Control viewContainer)
    {
      this.WebServiceUrl.Value = this.Page.ResolveUrl("~/Sitefinity/Services/Security/Roles.svc");
      this.BindProvidersList(this.CreateProvidersList, false, false, "AddRole");
      this.hProvidersServiceUrl.Value = this.ResolveClientUrl("~/Sitefinity/Services/Security/Roles.svc");
      this.AdminRoleID.Value = SecurityManager.AdminRole.Id.ToString();
      this.BackendRoleID.Value = SecurityManager.BackEndUsersRole.Id.ToString();
      string str = string.Empty;
      foreach (Guid unassignableRole in SecurityManager.UnassignableRoles)
      {
        if (str != string.Empty)
          str += " AND ";
        str = str + " ( Id != \"" + unassignableRole.ToString() + "\" ) ";
      }
      if (str.Length > 0)
        str = "( " + str + " ) ";
      this.hRolesFilter.Value = str;
      RadGrid control = this.Container.GetControl<RadGrid>("RolesGrid", false);
      if (control == null)
        return;
      SiteMapNode siteMapNode = BackendSiteMap.FindSiteMapNode(SiteInitializer.PermissionsPageId, true);
      GridColumn byUniqueName = control.Columns.FindByUniqueName("BinderContainer3");
      BinderContainer binderContainer = this.RolesGridBinder.Containers.Where<BinderContainer>((Func<BinderContainer, bool>) (c => c.ID == "BinderContainer4")).FirstOrDefault<BinderContainer>();
      if (byUniqueName == null || binderContainer == null)
        return;
      if (siteMapNode != null)
      {
        binderContainer.Markup = string.Format("<a href=\"javascript:void(0);\" class=\"sf_binderCommand_managePermissions sfGoto\">{0}</a>", (object) Res.Get<SecurityResources>().Permissions);
        byUniqueName.Visible = true;
      }
      else
        byUniqueName.Visible = false;
    }

    private void BindProvidersList(
      ListControl list,
      bool includeAllProvidersOption,
      bool hideIfOnlyOneProvider,
      string commaSeperatedAbilities)
    {
      this.ProviderSelectorPanel.AllProvidersTitle = Res.Get<Labels>().AllRoles;
      this.ProviderSelectorPanel.Manager = (IManager) RoleManager.GetManager();
      this.ProviderSelectorPanel.WebServiceUrlFormat = this.ProviderSelectorPanel.WebServiceUrlFormat = "~/" + "Sitefinity/Services/MembershipProviderService" + "/roleProviders";
      List<string> stringList;
      if (string.IsNullOrEmpty(commaSeperatedAbilities))
        stringList = new List<string>();
      else
        stringList = ((IEnumerable<string>) commaSeperatedAbilities.Split(',')).ToList<string>();
      List<string> source = stringList;
      list.Items.Clear();
      foreach (RoleDataProvider roleDataProvider in (Collection<RoleDataProvider>) (ManagerBase<RoleDataProvider>.StaticProvidersCollection ?? RoleManager.GetManager().StaticProviders))
      {
        if (roleDataProvider.Name != "AppRoles")
        {
          bool flag = true;
          if (roleDataProvider.Name != "AppRoles")
          {
            if (source.Count<string>() > 0)
            {
              foreach (string key in source)
              {
                if (roleDataProvider.Abilities.Keys.Contains<string>(key) && !roleDataProvider.Abilities[key].Supported)
                  flag = false;
              }
            }
            if (flag)
            {
              int num = RoleManager.GetManager(roleDataProvider.Name).GetRoles().Count<Role>();
              string text = this.FormatWithCount(roleDataProvider.Title, num.ToString());
              list.Items.Add(new ListItem(text, roleDataProvider.Name));
            }
          }
        }
      }
      if (hideIfOnlyOneProvider && list.Items.Count < 2)
        list.CssClass += string.Empty;
      if (!includeAllProvidersOption)
        return;
      list.Items.Insert(0, new ListItem(Res.Get<Labels>().AllRoles, string.Empty));
    }

    private string FormatWithCount(string itemName, string count) => string.Format("{0} ({1})", (object) itemName, (object) count);

    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      PageManager.ConfigureScriptManager(this.Page, ScriptRef.DialogManager).RegisterScriptControl<RolesView<THost>>(this);
    }

    public IEnumerable<ScriptDescriptor> GetScriptDescriptors() => (IEnumerable<ScriptDescriptor>) null;

    public IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) null;
  }
}
