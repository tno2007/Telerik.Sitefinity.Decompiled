// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.Principals.UsersList
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Data;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend;

namespace Telerik.Sitefinity.Security.Web.UI.Principals
{
  /// <summary>The view for showing list of users.</summary>
  /// <typeparam name="THost">The type of the host.</typeparam>
  public class UsersList : ViewModeControl<UsersPanel>, IScriptControl
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Security.Principals.UsersList.ascx");
    private const string usersServiceUrl = "~/Sitefinity/Services/Security/Users.svc";
    private static string IsProviderCaseSensitiveKey = "isProviderCaseSensitive";

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? UsersList.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Returns the UsersListBinder</summary>
    protected virtual RadGridBinder UsersListBinder => this.Container.GetControl<RadGridBinder>("usersListBinder", true);

    protected virtual ListControl RolesList => this.Container.GetControl<ListControl>("rolesList", true);

    protected virtual HiddenField hUsersServiceUrl => this.Container.GetControl<HiddenField>(nameof (hUsersServiceUrl), true);

    protected virtual HiddenField hfLoginPageUrl => this.Container.GetControl<HiddenField>(nameof (hfLoginPageUrl), true);

    /// <summary>Stores the ID of the current user</summary>
    protected virtual HiddenField hCurrentUserID => this.Container.GetControl<HiddenField>(nameof (hCurrentUserID), true);

    /// <summary>
    /// Stores the message show to the end-user when they try to delete
    /// their own account.
    /// </summary>
    protected virtual HiddenField hDeleteCurrentUserWarning => this.Container.GetControl<HiddenField>(nameof (hDeleteCurrentUserWarning), true);

    /// <summary>
    /// Stores information about the providers' abilities, in order to make the UI behave accordingly.
    /// </summary>
    protected virtual HiddenField hProvidersAbilities => this.Container.GetControl<HiddenField>(nameof (hProvidersAbilities), true);

    protected virtual HiddenField hProvidersSettings => this.Container.GetControl<HiddenField>(nameof (hProvidersSettings), true);

    protected virtual HiddenField hDefaultMembershipProviderName => this.Container.GetControl<HiddenField>(nameof (hDefaultMembershipProviderName), true);

    protected virtual HiddenField hShowSeparateUsersPerSiteMessage => this.Container.GetControl<HiddenField>(nameof (hShowSeparateUsersPerSiteMessage), true);

    /// <summary>Gets the provider selector panel.</summary>
    /// <value>The provider selector panel.</value>
    protected virtual FlatProviderSelector ProviderSelectorPanel => this.Container.GetControl<FlatProviderSelector>("providerSelectorPanel", true);

    /// <summary>
    /// Stores information about the case sensitivity for the membership providers.
    /// </summary>
    protected virtual HiddenField hProviderCaseSensitivity => this.Container.GetControl<HiddenField>(nameof (hProviderCaseSensitivity), true);

    /// <summary>
    /// Initializes all controls instantiated in the layout container. This method is called at appropriate time for setting initial values and subscribing for events of layout controls.
    /// </summary>
    /// <param name="viewContainer">The control that will host the current view.</param>
    protected override void InitializeControls(Control viewContainer)
    {
      base.InitializeControls(viewContainer);
      this.hUsersServiceUrl.Value = this.ResolveClientUrl("~/Sitefinity/Services/Security/Users.svc");
      this.hfLoginPageUrl.Value = this.ResolveClientUrl(Config.Get<SecurityConfig>().Permissions["Backend"].LoginUrl);
      this.hCurrentUserID.Value = SecurityManager.CurrentUserId.ToString();
      this.hDeleteCurrentUserWarning.Value = Res.Get<SecurityResources>().YouCantDeleteTheCurrentUser;
      if (SecurityManager.AllowSeparateUsersPerSite)
      {
        UserManager manager = UserManager.GetManager();
        this.hShowSeparateUsersPerSiteMessage.Value = (manager.GetProviderNames(ProviderBindingOptions.NoFilter).Count<string>() > manager.StaticProviders.Count).ToString();
      }
      this.ProviderSelectorPanel.ShowAllProvidersLink = false;
      this.BindProviders();
      this.BindRoles();
    }

    private void BindProviders()
    {
      UserManager manager = this.Host.Manager;
      this.ProviderSelectorPanel.Manager = (IManager) manager;
      this.ProviderSelectorPanel.AllProvidersTitle = Res.Get<Labels>().AllUsers;
      this.ProviderSelectorPanel.WebServiceUrlFormat = "~/" + "Sitefinity/Services/MembershipProviderService" + "/userProviders";
      SecurityConfig securityConfig = Config.Get<SecurityConfig>();
      ConfigElementDictionary<string, DataProviderSettings> membershipProviders = securityConfig.MembershipProviders;
      string str1 = "{";
      string str2 = "{";
      string str3 = "{";
      IEnumerable<DataProviderBase> contextProviders = manager.GetContextProviders();
      for (int index = 0; index < contextProviders.Count<DataProviderBase>(); ++index)
      {
        MembershipDataProvider provider = manager.Providers[index];
        string str4 = str1 + (str1 == "{" ? "" : ", ") + "\"Provider" + index.ToString() + "\" : { " + "\"ProviderId\" : \"" + provider.Id.ToString() + "\", " + "\"ProviderName\" : \"" + provider.Name + "\"";
        string str5 = str2 + (str2 == "{" ? "" : ", ") + "\"Provider" + index.ToString() + "\" : { " + "\"ProviderName\" : \"" + provider.Name + "\", ";
        bool flag = provider.RequiresQuestionAndAnswer;
        string str6 = flag.ToString();
        str2 = str5 + "\"RequiresQuestionAndAnswer\" : \"" + str6 + "\"}";
        string str7 = "";
        foreach (string key in provider.Abilities.Keys)
        {
          ProviderAbility ability = provider.Abilities[key];
          string[] strArray = new string[7]
          {
            str7,
            str7 == "" ? "" : ", ",
            "\"",
            ability.OperationName,
            "\" : \"",
            null,
            null
          };
          flag = ability.Allowed && ability.Supported;
          strArray[5] = flag.ToString().ToLower();
          strArray[6] = "\" ";
          str7 = string.Concat(strArray);
        }
        if (str7 != string.Empty)
          str4 += ", ";
        str1 = str4 + str7 + "}";
        if (membershipProviders.ContainsKey(provider.Name))
        {
          string parameter = membershipProviders[provider.Name].Parameters[UsersList.IsProviderCaseSensitiveKey];
          if (!string.IsNullOrEmpty(parameter) && bool.TryParse(parameter, out bool _))
          {
            if (str3 != "{")
              str3 += ", ";
            str3 = str3 + provider.Name + ":" + parameter;
          }
        }
      }
      string str8 = str1 + "}";
      string str9 = str2 + "}";
      string str10 = str3 + "}";
      this.hProvidersAbilities.Value = str8;
      this.hProvidersSettings.Value = str9;
      this.hProviderCaseSensitivity.Value = str10;
      this.hDefaultMembershipProviderName.Value = securityConfig.DefaultBackendMembershipProvider;
    }

    private void BindRoles()
    {
      this.RolesList.Items.Clear();
      foreach (RoleDataProvider staticProvider in (Collection<RoleDataProvider>) RoleManager.GetManager().StaticProviders)
      {
        if (!staticProvider.Abilities.Keys.Contains<string>("AssingUserToRole") && !staticProvider.Abilities.Keys.Contains<string>("UnAssingUserFromRole") || staticProvider.Abilities["AssingUserToRole"].Supported && staticProvider.Abilities["UnAssingUserFromRole"].Supported)
        {
          foreach (Role role in (IEnumerable<Role>) staticProvider.GetRoles())
          {
            if (!SecurityManager.UnassignableRoles.Contains(role.Id))
              this.RolesList.Items.Add(new ListItem(this.FormatWithCount(role.Name, staticProvider.Title), role.Id.ToString() + "~" + staticProvider.Name));
          }
        }
      }
    }

    private string FormatWithCount(string itemName, string count) => string.Format("{0} ({1})", (object) itemName, (object) count);

    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      PageManager.ConfigureScriptManager(this.Page, ScriptRef.DialogManager).RegisterScriptControl<UsersList>(this);
    }

    public IEnumerable<ScriptDescriptor> GetScriptDescriptors() => (IEnumerable<ScriptDescriptor>) null;

    public IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) null;
  }
}
