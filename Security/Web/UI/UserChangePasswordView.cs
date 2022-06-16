// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.UserChangePasswordView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Security.Web.UI
{
  /// <summary>Control used to change a user password.</summary>
  [ControlTemplateInfo("UserProfilesResources", "ProfilePasswordEditor", "Users")]
  public class UserChangePasswordView : UserProfileDetailView
  {
    internal const string layoutTemplateName = "Telerik.Sitefinity.Resources.Templates.Frontend.Security.UserChangePasswordView.ascx";
    internal static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Frontend.Security.UserChangePasswordView.ascx");
    internal const string layoutTemplatePathBackend = "~/SFRes/Telerik.Sitefinity.Resources.Templates.Backend.Security.UserChangePasswordViewBackend.ascx";

    /// <inheritdoc />
    protected override string DefaultLayoutTemplatePath => UserChangePasswordView.layoutTemplatePath;

    /// <summary>
    /// Gets the reference to the text field used for setting the current password.
    /// </summary>
    protected TextField CurrentPasswordTextField => this.Container.GetControl<TextField>("currentPassword", false);

    /// <summary>
    /// Gets the reference to the text field used for setting the new password.
    /// </summary>
    protected TextField NewPasswordTextField => this.Container.GetControl<TextField>("newPassword", true);

    /// <summary>
    /// Gets the reference to the text field used for setting the password used for confirmation.
    /// </summary>
    protected TextField ConfirmNewPasswordTextField => this.Container.GetControl<TextField>("confirmNewPassword", true);

    /// <summary>
    /// Gets the reference to the buttons that saves the changes made to the current profile.
    /// </summary>
    protected LinkButton SaveChangesButton => this.Container.GetControl<LinkButton>("saveChanges", true);

    /// <summary>
    /// Gets the reference to the buttons that cancels the password changing.
    /// </summary>
    protected HyperLink CancelLink => this.Container.GetControl<HyperLink>("cancel", false);

    /// <summary>
    /// Gets the reference to the control displaying the error message.
    /// </summary>
    protected ITextControl ErrorLabel => this.Container.GetControl<ITextControl>("errorLabel", true);

    /// <summary>
    /// Gets the reference to the control displaying the success message.
    /// </summary>
    protected ITextControl SuccessLabel => this.Container.GetControl<ITextControl>("successLabel", true);

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnInit(EventArgs e)
    {
      if (FormManager.GetCurrent(this.Page) == null)
        this.Controls.Add((Control) new FormManager());
      base.OnInit(e);
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container">The controls container.</param>
    /// <param name="definition">The content view definition.</param>
    protected override void InitializeControls(
      GenericContainer container,
      IContentViewDefinition definition)
    {
      this.SaveChangesButton.Click += new EventHandler(this.SaveChanges_Click);
      this.ConfigureCancelButton();
      SystemManager.SetNoCache(this.Page.Response.Cache);
      this.BindUserProfile();
    }

    private void ConfigureCancelButton()
    {
      if (this.CancelLink == null)
        return;
      this.CancelLink.NavigateUrl = this.Host.GetViewUrl((string) null);
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets the required by the control, core library scripts predefined in the <see cref="T:Telerik.Sitefinity.Modules.Pages.ScriptRef" /> enum.
    /// </summary>
    /// <example>
    /// // The defaults are:
    /// ScriptRef.MicrosoftAjax |
    /// ScriptRef.MicrosoftAjaxWebForms |
    /// ScriptRef.JQuery |
    /// ScriptRef.JQueryValidate |
    /// ScriptRef.JQueryCookie |
    /// ScriptRef.TelerikSitefinity |
    /// ScriptRef.QueryString;
    /// </example>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Modules.Pages.ScriptRef" /> enum value indicating the mix of library scripts that the control requires.</returns>
    protected override ScriptRef GetRequiredCoreScripts() => ScriptRef.JQuery;

    private void SaveChanges_Click(object sender, EventArgs e)
    {
      if (!this.ValidateInput(out string _))
        return;
      this.ChangePassword();
    }

    private bool ValidateInput(out string errorMessage)
    {
      List<FieldControl> fieldControlList = new List<FieldControl>();
      if (this.CurrentPasswordTextField != null)
        fieldControlList.Add((FieldControl) this.CurrentPasswordTextField);
      fieldControlList.Add((FieldControl) this.NewPasswordTextField);
      fieldControlList.Add((FieldControl) this.ConfirmNewPasswordTextField);
      foreach (FieldControl fieldControl in fieldControlList)
      {
        if (!fieldControl.IsValid())
        {
          errorMessage = fieldControl.Validator.ErrorMessage;
          return false;
        }
      }
      errorMessage = (string) null;
      return true;
    }

    private void ChangePassword()
    {
      SitefinityIdentity user = this.GetUser();
      if (user == null)
        return;
      Guid userId = user.UserId;
      UserManager userManager = new UserManager(user.MembershipProvider);
      string newPassword = (string) this.NewPasswordTextField.Value;
      string oldPassword = (string) null;
      if (this.CurrentPasswordTextField != null)
        oldPassword = (string) this.CurrentPasswordTextField.Value;
      bool flag;
      try
      {
        this.ChangePassword(userManager, userId, oldPassword, newPassword);
        flag = true;
      }
      catch (ArgumentException ex)
      {
        flag = false;
      }
      catch (ProviderException ex)
      {
        flag = false;
      }
      if (flag)
      {
        if (this.CurrentPasswordTextField != null)
          this.CurrentPasswordTextField.Value = (object) "";
        this.NewPasswordTextField.Value = (object) "";
        this.ConfirmNewPasswordTextField.Value = (object) "";
        this.ErrorLabel.Text = "";
        ((Control) this.ErrorLabel).Visible = false;
        this.SuccessLabel.Text = Res.Get<ErrorMessages>().ChangePasswordDefaultSuccessText;
        ((Control) this.SuccessLabel).Visible = true;
      }
      else
      {
        this.ErrorLabel.Text = Res.Get<ErrorMessages>().ChangePasswordDefaultChangePasswordFailureText.Arrange((object) userManager.MinRequiredPasswordLength, (object) userManager.MinRequiredNonAlphanumericCharacters);
        ((Control) this.ErrorLabel).Visible = true;
        this.SuccessLabel.Text = "";
        ((Control) this.SuccessLabel).Visible = false;
      }
    }

    protected internal virtual SitefinityIdentity GetUser() => ClaimsManager.GetCurrentIdentity();

    internal virtual void ChangePassword(
      UserManager userManager,
      Guid userId,
      string oldPassword,
      string newPassword)
    {
      UserManager.ChangePasswordForUser(userManager, userId, oldPassword, newPassword);
    }
  }
}
