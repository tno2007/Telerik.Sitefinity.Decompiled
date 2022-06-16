// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.UserChangePasswordQuestionAndAnswerWidget
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Modules.Pages;
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
  [PropertyEditorTitle(typeof (PublicControlsResources), "UserChangePasswordQuestionAndAnswerWidgetTitle")]
  [ControlTemplateInfo("UserProfilesResources", "ProfilePasswordEditor", "Login")]
  public class UserChangePasswordQuestionAndAnswerWidget : UserChangePasswordQuestionAndAnswerView
  {
    internal new const string layoutTemplateName = "Telerik.Sitefinity.Resources.Templates.Frontend.Security.UserChangePasswordQuestionAndAnswerWidget.ascx";
    internal new static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Frontend.Security.UserChangePasswordQuestionAndAnswerWidget.ascx");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.Web.UI.UserChangePasswordWidget" /> class.
    /// </summary>
    public UserChangePasswordQuestionAndAnswerWidget() => this.LayoutTemplatePath = UserChangePasswordQuestionAndAnswerWidget.layoutTemplatePath;

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

    private Control ChangePasswordQuestionAndAnswerRegion => this.Container.GetControl<Control>("changePasswordQuestionAndAnswerRegion", true);

    private Control PasswordQuestionAndAnswerChangedRegion => this.Container.GetControl<Control>("passwordQuestionAndAnswerChangedRegion", true);

    /// <inheritdoc />
    protected override void InitializeControls(
      GenericContainer container,
      IContentViewDefinition definition)
    {
      base.InitializeControls(container, definition);
    }

    /// <summary>Gets the user.</summary>
    /// <returns></returns>
    protected internal override SitefinityIdentity GetUser() => SecurityManager.GetManager().GetPasswordRecoveryUser();

    internal override void ChangePasswordQuestionAndAnswer(
      UserManager userManager,
      Guid userId,
      string password,
      string newQuestion,
      string newAnswer)
    {
      using (new ElevatedModeRegion((IManager) userManager))
      {
        userManager.ChangePasswordQuestionAndAnswer(userId, password, newQuestion, newAnswer);
        userManager.SaveChanges();
        this.SwitchRegions(false);
      }
    }

    private bool ValidateIssueDate(DateTime issueDate) => issueDate.AddHours(1.0) > DateTime.UtcNow;

    private void SwitchRegions(bool showChangePassword = true)
    {
      this.ChangePasswordQuestionAndAnswerRegion.Visible = showChangePassword;
      this.PasswordQuestionAndAnswerChangedRegion.Visible = !showChangePassword;
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
