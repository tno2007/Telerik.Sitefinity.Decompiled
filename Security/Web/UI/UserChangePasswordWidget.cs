// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.UserChangePasswordWidget
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.PublicControls;

namespace Telerik.Sitefinity.Security.Web.UI
{
  /// <summary>
  /// Control used to change a user password in the password recovery process.
  /// </summary>
  [PropertyEditorTitle(typeof (PublicControlsResources), "UserChangePasswordWidgetTitle")]
  [ControlTemplateInfo("UserProfilesResources", "ProfilePasswordEditor", "Login")]
  public class UserChangePasswordWidget : UserChangePasswordView
  {
    internal new const string layoutTemplateName = "Telerik.Sitefinity.Resources.Templates.Frontend.Security.UserChangePasswordWidget.ascx";
    internal new static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Frontend.Security.UserChangePasswordWidget.ascx");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.Web.UI.UserChangePasswordWidget" /> class.
    /// </summary>
    public UserChangePasswordWidget() => this.LayoutTemplatePath = UserChangePasswordWidget.layoutTemplatePath;

    /// <inheritdoc />
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the container for the specific views that display all types inheriting from
    /// the abstract type <see cref="T:Telerik.Sitefinity.GenericContent.Model.Content" />.
    /// </summary>
    /// <value>The host.</value>
    [Browsable(false)]
    public new ContentView Host
    {
      get => base.Host;
      set => base.Host = value;
    }

    /// <summary>Gets or sets the maximum length.</summary>
    public int MaxLength { get; set; }

    /// <summary>Gets or sets the minimum lenght.</summary>
    public int MinLength { get; set; }

    /// <summary>
    /// Gets or sets the message shown when max length validation has failed.
    /// </summary>
    public string MaxLengthViolationMessage { get; set; }

    /// <summary>
    /// Gets or sets the message shown when min length validation has failed.
    /// </summary>
    public string MinLengthViolationMessage { get; set; }

    /// <summary>
    /// Gets a reference to the change password region control.
    /// </summary>
    private Control ChangePasswordRegion => this.Container.GetControl<Control>("changePasswordRegion", true);

    /// <summary>
    /// Gets a reference to the password changed region control.
    /// </summary>
    private Control PasswordChangedRegion => this.Container.GetControl<Control>("passwordChangedRegion", true);

    /// <inheritdoc />
    protected override void InitializeControls(
      GenericContainer container,
      IContentViewDefinition definition)
    {
      if (this.Context.Request.QueryStringGet("cp") != "pr")
      {
        this.Visible = false;
      }
      else
      {
        this.NewPasswordTextField.ValidatorDefinition.MaxLength = this.MaxLength;
        this.NewPasswordTextField.ValidatorDefinition.MaxLengthViolationMessage = this.MaxLengthViolationMessage;
        this.NewPasswordTextField.ValidatorDefinition.MinLength = this.MinLength;
        this.NewPasswordTextField.ValidatorDefinition.MinLengthViolationMessage = this.MinLengthViolationMessage;
        this.ConfirmNewPasswordTextField.ValidatorDefinition.MaxLength = this.MaxLength;
        this.ConfirmNewPasswordTextField.ValidatorDefinition.MaxLengthViolationMessage = this.MaxLengthViolationMessage;
        this.ConfirmNewPasswordTextField.ValidatorDefinition.MinLength = this.MinLength;
        this.ConfirmNewPasswordTextField.ValidatorDefinition.MinLengthViolationMessage = this.MinLengthViolationMessage;
        base.InitializeControls(container, definition);
      }
    }

    /// <summary>Gets the user.</summary>
    /// <returns></returns>
    protected internal override SitefinityIdentity GetUser() => SecurityManager.GetManager().GetPasswordRecoveryUser();

    /// <summary>Changes the password.</summary>
    /// <param name="userManager">The user manager.</param>
    /// <param name="userId">The user id.</param>
    /// <param name="oldPassword">The old password.</param>
    /// <param name="newPassword">The new password.</param>
    internal override void ChangePassword(
      UserManager userManager,
      Guid userId,
      string oldPassword,
      string newPassword)
    {
      using (new ElevatedModeRegion((IManager) userManager))
      {
        string oldPassword1 = userManager.ResetPassword(userId, (string) null);
        userManager.ChangePassword(userId, oldPassword1, newPassword);
        userManager.SaveChanges();
        this.SwitchRegions(false);
      }
    }

    private bool ValidateIssueDate(DateTime issueDate) => issueDate.AddHours(1.0) > DateTime.UtcNow;

    private void SwitchRegions(bool showChangePassword = true)
    {
      this.ChangePasswordRegion.Visible = showChangePassword;
      this.PasswordChangedRegion.Visible = !showChangePassword;
    }

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
    protected override ScriptRef GetRequiredCoreScripts() => ScriptRef.Empty;
  }
}
