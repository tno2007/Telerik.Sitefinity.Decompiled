// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.UserLimitReachedControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Licensing;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Security.Web.UI
{
  /// <summary>Control for the User Limit Reached backend page</summary>
  public class UserLimitReachedControl : SimpleScriptView
  {
    private bool isUnrestricted;
    internal const string JsComponentPath = "Telerik.Sitefinity.Security.Web.UI.Scripts.UserLimitReachedControl.js";
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Security.UserLimitReachedControl.ascx");

    /// <summary>
    /// Gets a reference to the user limit reached message control.
    /// </summary>
    protected Literal UserLimitReachedMessage => this.Container.GetControl<Literal>("userLimitReachedMessage", false);

    /// <summary>Gets a reference to the user list container control.</summary>
    protected HtmlGenericControl UserListContainer => this.Container.GetControl<HtmlGenericControl>("userListContainer", true);

    /// <summary>
    /// Gets a reference to the force logoff container control.
    /// </summary>
    protected HtmlGenericControl ForceLogoffContainer => this.Container.GetControl<HtmlGenericControl>("forceLogoffContainer", true);

    /// <summary>Gets a reference to the user list control.</summary>
    protected Repeater UserList => this.Container.GetControl<Repeater>("userList", true);

    /// <summary>Gets a reference to the user list choice control.</summary>
    protected virtual ChoiceField UserListChoice => this.Container.GetControl<ChoiceField>("userListChoice", true);

    /// <summary>Gets a reference to the or word literal.</summary>
    protected Literal OrWordLiteral => this.Container.GetControl<Literal>("orLiteral", false);

    /// <summary>
    /// Gets a reference to the force someone to logout button.
    /// </summary>
    protected LinkButton ForceSomeoneToLogoutButton => this.Container.GetControl<LinkButton>("forceSomeoneToLogoutButton", true);

    /// <summary>Gets a reference to the browse public website button.</summary>
    protected LinkButton BrowsePublicWebsiteButton => this.Container.GetControl<LinkButton>("browsePublicWebsiteButton", true);

    /// <summary>
    /// Gets a reference the wait for user to log off literal.
    /// </summary>
    protected Literal WaitForUserToLogOffLiteral => this.Container.GetControl<Literal>("waitForUserToLogOffLiteral", false);

    /// <summary>Gets a reference to the cancel logoff button.</summary>
    protected LinkButton CancelLogoffButton => this.Container.GetControl<LinkButton>("cancelLogoffButton", true);

    /// <summary>Gets a reference to the force logout button.</summary>
    protected LinkButton ForceLogoutButton => this.Container.GetControl<LinkButton>("logoutButton", true);

    protected void ForceLogoutButton_Click(object sender, EventArgs e)
    {
      string str = (string) this.UserListChoice.Value;
      if (str.IndexOf(';') > -1)
      {
        string[] strArray = str.Split(';');
        this.LogoutUser(strArray[0], strArray[1]);
      }
      SystemManager.CurrentHttpContext.GetOwinContext().Authentication.SignIn((ClaimsIdentity) ClaimsManager.GetCurrentIdentity());
    }

    protected void BrowsePublicWebsite_Click(object sender, EventArgs e) => SystemManager.CurrentHttpContext.GetOwinContext().Authentication.SignOut();

    /// <summary>Gets the name of the embedded layout template.</summary>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? UserLimitReachedControl.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container">The controls container.</param>
    protected override void InitializeControls(GenericContainer container)
    {
      this.isUnrestricted = this.GetIsUnrestricted();
      if (this.UserLimitReachedMessage != null)
        this.UserLimitReachedMessage.Text = string.Format(Res.Get<Labels>("UserLimitReachedMessage"), (object) LicenseState.Current.LicenseInfo.Users);
      if (this.isUnrestricted)
      {
        this.ForceLogoffContainer.Visible = true;
        this.BindLoggedUsers();
      }
      else
        this.UserListContainer.Visible = false;
      if (this.OrWordLiteral != null)
        this.OrWordLiteral.Visible = this.isUnrestricted;
      this.ForceSomeoneToLogoutButton.Visible = this.isUnrestricted;
      if (this.WaitForUserToLogOffLiteral != null)
        this.WaitForUserToLogOffLiteral.Visible = !this.isUnrestricted;
      this.ForceLogoutButton.Click += new EventHandler(this.ForceLogoutButton_Click);
      this.BrowsePublicWebsiteButton.Click += new EventHandler(this.BrowsePublicWebsite_Click);
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptBehaviorDescriptor behaviorDescriptor = new ScriptBehaviorDescriptor(this.GetType().FullName, this.ClientID);
      if (this.isUnrestricted)
      {
        behaviorDescriptor.AddElementProperty("forceSomeoneToLogoutButton", this.ForceSomeoneToLogoutButton.ClientID);
        behaviorDescriptor.AddElementProperty("forceLogoffContainer", this.ForceLogoffContainer.ClientID);
        behaviorDescriptor.AddElementProperty("cancelLogoffButton", this.CancelLogoffButton.ClientID);
      }
      behaviorDescriptor.AddElementProperty("userListContainer", this.UserListContainer.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptBehaviorDescriptor[1]
      {
        behaviorDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      List<ScriptReference> scriptReferences = new List<ScriptReference>();
      string fullName = this.GetType().Assembly.FullName;
      scriptReferences.Add(new ScriptReference()
      {
        Assembly = fullName,
        Name = "Telerik.Sitefinity.Security.Web.UI.Scripts.UserLimitReachedControl.js"
      });
      return (IEnumerable<ScriptReference>) scriptReferences;
    }

    private void LogoutUser(string logoutUserId, string logoutUserProvider) => SecurityManager.Logout(logoutUserProvider, new Guid(logoutUserId));

    private void BindLoggedUsers()
    {
      IEnumerable<\u003C\u003Ef__AnonymousType57<string, string>> datas = SecurityManager.GetLoggedInBackendUsers().Select(u => new
      {
        Name = UserProfilesHelper.GetUserDisplayName(u.Id),
        Value = u.Id.ToString() + ";" + u.ProviderName
      });
      this.UserList.DataSource = (object) datas;
      this.UserList.DataBind();
      if (!this.isUnrestricted)
        return;
      foreach (var data in datas)
        this.UserListChoice.Choices.Add(new ChoiceItem()
        {
          Text = data.Name,
          Value = data.Value
        });
      this.UserListChoice.DataBind();
    }

    private bool GetIsUnrestricted()
    {
      SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
      return currentIdentity != null && currentIdentity.IsUnrestricted;
    }
  }
}
