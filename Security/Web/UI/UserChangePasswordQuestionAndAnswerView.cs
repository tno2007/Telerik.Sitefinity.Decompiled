// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.UserChangePasswordQuestionAndAnswerView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Security.Web.UI
{
  /// <summary>Control used to change a user password.</summary>
  [ControlTemplateInfo("UserProfilesResources", "ProfilePasswordEditor", "Users")]
  public class UserChangePasswordQuestionAndAnswerView : ViewBase
  {
    internal const string layoutTemplateName = "Telerik.Sitefinity.Resources.Templates.Frontend.Security.UserChangePasswordQuestionAndAnswerView.ascx";
    internal static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Frontend.Security.UserChangePasswordQuestionAndAnswerView.ascx");
    internal const string layoutTemplatePathBackend = "~/SFRes/Telerik.Sitefinity.Resources.Templates.Backend.Security.UserChangePasswordQuestionAndAnswerViewBackend.ascx";

    /// <inheritdoc />
    protected override string DefaultLayoutTemplatePath => UserChangePasswordQuestionAndAnswerView.layoutTemplatePath;

    /// <summary>
    /// Gets the reference to the text field used for setting the current password.
    /// </summary>
    protected TextField PasswordTextField => this.Container.GetControl<TextField>("password", false);

    /// <summary>
    /// Gets the reference to the text field used for setting the new password.
    /// </summary>
    protected TextField NewQuestionTextField => this.Container.GetControl<TextField>("newQuestion", true);

    /// <summary>
    /// Gets the reference to the text field used for setting the password used for confirmation.
    /// </summary>
    protected TextField NewAnswerTextField => this.Container.GetControl<TextField>("newAnswer", true);

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
      this.ChangePasswordQuestionAndAnswer();
    }

    private bool ValidateInput(out string errorMessage)
    {
      List<FieldControl> fieldControlList = new List<FieldControl>();
      if (this.PasswordTextField != null)
        fieldControlList.Add((FieldControl) this.PasswordTextField);
      fieldControlList.Add((FieldControl) this.NewQuestionTextField);
      fieldControlList.Add((FieldControl) this.NewAnswerTextField);
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

    private void ChangePasswordQuestionAndAnswer()
    {
      SitefinityIdentity user = this.GetUser();
      if (user == null)
        return;
      Guid userId = user.UserId;
      UserManager manager = new UserManager(user.MembershipProvider);
      string newAnswer = (string) this.NewAnswerTextField.Value;
      string newQuestion = (string) this.NewQuestionTextField.Value;
      string password = (string) null;
      if (this.PasswordTextField != null)
        password = (string) this.PasswordTextField.Value;
      bool flag;
      try
      {
        this.ChangePasswordQuestionAndAnswer(manager, userId, password, newQuestion, newAnswer);
        flag = true;
      }
      catch (Exception ex)
      {
        flag = false;
      }
      if (!flag)
      {
        this.ErrorLabel.Text = Res.Get<ErrorMessages>().ChangePasswordQuestionAndAnswerDefaultChangePasswordFailureText;
        ((Control) this.ErrorLabel).Visible = true;
        this.SuccessLabel.Text = "";
        ((Control) this.SuccessLabel).Visible = false;
      }
      else
      {
        if (this.PasswordTextField != null)
          this.PasswordTextField.Value = (object) "";
        this.NewAnswerTextField.Value = (object) "";
        this.NewQuestionTextField.Value = (object) "";
        this.ErrorLabel.Text = "";
        ((Control) this.ErrorLabel).Visible = false;
        this.SuccessLabel.Text = Res.Get<UserProfilesResources>().ChangesAreSuccessfullySaved;
        ((Control) this.SuccessLabel).Visible = true;
      }
    }

    protected internal virtual SitefinityIdentity GetUser() => ClaimsManager.GetCurrentIdentity();

    internal virtual void ChangePasswordQuestionAndAnswer(
      UserManager manager,
      Guid userId,
      string password,
      string newQuestion,
      string newAnswer)
    {
      UserManager.ChangePasswordQuestionAndAnswerForNotAuthUsers(manager, userId, password, newQuestion, newAnswer);
    }
  }
}
