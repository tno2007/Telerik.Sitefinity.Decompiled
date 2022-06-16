// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.LoginForm
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security.AntiXss;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Licensing.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Data;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.PublicControls;
using Telerik.Sitefinity.Web.UI.Templates;

namespace Telerik.Sitefinity.Security.Web.UI
{
  /// <summary>Represents a login form for Sitefinity applications.</summary>
  public class LoginForm : Login
  {
    private bool listSet;
    private bool hoderSet;
    private Control providersHolder;
    private ListControl providersList;
    private PlaceHolder loginLinksHolder;
    private HtmlAnchor forgotPasswordLink;
    private HtmlAnchor changePasswordLink;
    private HtmlAnchor createUserLink;
    private HtmlAnchor helpLink;
    private Label failureTextControl;
    private User currentUser;
    private ChoiceField userListChoice;
    private Panel userListPanel;
    private Panel selfLogoffPanel;
    private Panel denyLogonPanel;
    private Panel loginPanel;
    private LinkButton logoutButton;
    private UserLoggingReason logoutReason;
    private LinkButton selftLogoutButton;
    private LinkButton selfLogoutCancelButton;
    private Literal loginTitle;
    private Label userNameLabel;
    private Label providersLabel;
    private Label passwordLabel;
    private RequiredFieldValidator userNameRequiredValidator;
    private RequiredFieldValidator passwordRequiredValidator;
    private Literal userNameRequiredLiteral;
    private Literal passwordRequiredLiteral;
    private Label rememberMeTextLabel;
    private Literal loginButtonLiteral;
    private Literal usersListPanelLoginTitle;
    private Literal selfLogoffPanelLoginTitle;
    private Literal denyLogonPanelLoginTitle;
    private Literal userLimitLabel;
    private Literal logoutButtonLiteral;
    private Literal logoutOtherUserAndEnterLiteral;
    private Literal denyLogonMesage;
    private Literal loginRetryMessage;
    private Literal helpLinkLiteral;
    private Literal helpTitleLiteral;
    private Literal passwordRecoveryTextLiteral;
    private Literal changePasswordTextLiteral;
    private Literal registerUserTextLiteral;
    private Literal errorMessageNoSmtpConfigLiteral;
    private Literal errorMessageContactAdminToResetYourPasswordLiteral;
    private Literal errorOrAskAnAdministratorToConfigureTheSystemLiteral;
    private Literal errorMessageSmtpDetailsTitle;
    private Literal errorMessageSmtpPermissionDeniedDetailsTitle;
    private Literal errorMessageSmtpSettingsNotSetLiteral;
    private Literal errorMessageHowToSetSmtpLiteral;
    private Literal errorMessageContactAdminToResetYourPasswordSmtpLiteral;
    private Literal errorOrAskAnAdministratorToConfigureTheSystemSmtpLiteral;
    private Literal errorMessageTheSysIsNotPermittedToSendEmailsLiteral;
    protected bool invalidCastExceptionThrown;
    protected bool updateVisibilityOfLinksDisabled;
    /// <summary>Specifies the name of the embeded template.</summary>
    public static readonly string BackendLayoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Security.LoginForm.ascx");
    protected const string DenyLogonUser = "DenyLogonUser";
    protected const string AdminLogsOutUser = "AdminLogsOutUser";
    protected const string UserAlreadyLoggedIn = "UserAlreadyLoggedIn";

    /// <summary>
    /// Indicates what the dialog should do when a user logs in
    /// </summary>
    public SuccessfulLoginAction LoginAction
    {
      get => this.ViewState[nameof (LoginAction)] != null ? (SuccessfulLoginAction) this.ViewState[nameof (LoginAction)] : SuccessfulLoginAction.Redirect;
      set => this.ViewState[nameof (LoginAction)] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the control allows users to select
    /// membership provider to authenticate with.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if provider selecting is allowed; otherwise, <c>false</c>.
    /// </value>
    public virtual bool AllowSelectMembershipProvider
    {
      get
      {
        object obj = this.ViewState[nameof (AllowSelectMembershipProvider)];
        return obj != null && (bool) obj;
      }
      set => this.ViewState[nameof (AllowSelectMembershipProvider)] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the forgot password link should be shown.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if the forgot password link should be shown; otherwise, <c>false</c>.
    /// </value>
    public bool ShowForgotPasswordLink
    {
      get
      {
        object obj = this.ViewState[nameof (ShowForgotPasswordLink)];
        return obj == null || (bool) obj;
      }
      set
      {
        this.ViewState[nameof (ShowForgotPasswordLink)] = (object) value;
        this.UpdateVisibilityOfLinks();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the change password link should be shown.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if the change password link should be shown; otherwise, <c>false</c>.
    /// </value>
    public bool ShowChangePasswordLink
    {
      get
      {
        object obj = this.ViewState[nameof (ShowChangePasswordLink)];
        return obj == null || (bool) obj;
      }
      set
      {
        this.ViewState[nameof (ShowChangePasswordLink)] = (object) value;
        this.UpdateVisibilityOfLinks();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the create user link should be shown.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if the create user link should be shown; otherwise, <c>false</c>.
    /// </value>
    public bool ShowRegisterUserLink
    {
      get
      {
        object obj = this.ViewState[nameof (ShowRegisterUserLink)];
        return obj == null || (bool) obj;
      }
      set
      {
        this.ViewState[nameof (ShowRegisterUserLink)] = (object) value;
        this.UpdateVisibilityOfLinks();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the help link should be shown.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if the help link should be shown; otherwise, <c>false</c>.
    /// </value>
    public bool ShowHelpLink
    {
      get
      {
        object obj = this.ViewState[nameof (ShowHelpLink)];
        return obj == null || (bool) obj;
      }
      set
      {
        this.ViewState[nameof (ShowHelpLink)] = (object) value;
        this.UpdateVisibilityOfLinks();
      }
    }

    /// <summary>Gets the providers list.</summary>
    /// <value>The providers list.</value>
    [Browsable(false)]
    public virtual ListControl ProvidersList
    {
      get
      {
        if (!this.listSet)
        {
          this.providersList = (ListControl) this.FindControl(nameof (ProvidersList));
          this.listSet = true;
        }
        return this.providersList;
      }
    }

    /// <summary>Gets the login links place holder.</summary>
    /// <value>The login links holder.</value>
    protected PlaceHolder LoginLinksHolder
    {
      get
      {
        if (this.loginLinksHolder == null)
          this.loginLinksHolder = this.FindControl("loginLinksHolder") as PlaceHolder;
        return this.loginLinksHolder;
      }
    }

    /// <summary>Gets the forgot password link.</summary>
    /// <value>The forgot password link.</value>
    [Browsable(false)]
    public HtmlAnchor ForgotPasswordLink
    {
      get
      {
        if (this.forgotPasswordLink == null)
          this.forgotPasswordLink = this.FindControl("PasswordRecoveryLink") as HtmlAnchor;
        return this.forgotPasswordLink;
      }
    }

    /// <summary>Gets the change password link.</summary>
    /// <value>The change password link.</value>
    [Browsable(false)]
    public HtmlAnchor ChangePasswordLink
    {
      get
      {
        if (this.changePasswordLink == null)
          this.changePasswordLink = this.FindControl(nameof (ChangePasswordLink)) as HtmlAnchor;
        return this.changePasswordLink;
      }
    }

    /// <summary>Gets the create user link.</summary>
    /// <value>The create user link.</value>
    [Browsable(false)]
    public HtmlAnchor CreateUserLink
    {
      get
      {
        if (this.createUserLink == null)
          this.createUserLink = this.FindControl(nameof (CreateUserLink)) as HtmlAnchor;
        return this.createUserLink;
      }
    }

    /// <summary>Gets the help link.</summary>
    /// <value>The help link.</value>
    [Browsable(false)]
    public HtmlAnchor HelpLink
    {
      get
      {
        if (this.helpLink == null)
          this.helpLink = this.FindControl(nameof (HelpLink)) as HtmlAnchor;
        return this.helpLink;
      }
    }

    /// <summary>Gets the place holder for providers list.</summary>
    /// <value>The place holder for providers list.</value>
    [Browsable(false)]
    public virtual Control ProvidersHolder
    {
      get
      {
        if (!this.hoderSet)
        {
          this.providersHolder = this.FindControl(nameof (ProvidersHolder));
          this.hoderSet = true;
        }
        return this.providersHolder;
      }
    }

    /// <summary>Gets the failure text control.</summary>
    /// <value>The failure text control.</value>
    [Browsable(false)]
    public virtual Label FailureTextControl
    {
      get
      {
        if (this.failureTextControl == null)
          this.failureTextControl = this.FindControl("FailureText") as Label;
        return this.failureTextControl;
      }
    }

    /// <summary>Gets the user list choice.</summary>
    /// <value>The user list choice.</value>
    [Browsable(false)]
    public virtual ChoiceField UserListChoice
    {
      get
      {
        if (this.userListChoice == null)
          this.userListChoice = this.FindControl("userListChoice") as ChoiceField;
        return this.userListChoice;
      }
    }

    /// <summary>Gets the user list panel.</summary>
    /// <value>The user list panel.</value>
    [Browsable(false)]
    public virtual Panel UserListPanel
    {
      get
      {
        if (this.userListPanel == null)
          this.userListPanel = this.FindControl("userListPanel") as Panel;
        return this.userListPanel;
      }
    }

    /// <summary>Gets the deny logon panel.</summary>
    /// <value>The deny logon panel.</value>
    [Browsable(false)]
    public virtual Panel DenyLogonPanel
    {
      get
      {
        if (this.denyLogonPanel == null)
          this.denyLogonPanel = this.FindControl("denyLogonPanel") as Panel;
        return this.denyLogonPanel;
      }
    }

    /// <summary>Gets the self logoff panel.</summary>
    /// <value>The self logoff panel.</value>
    [Browsable(false)]
    public virtual Panel SelfLogoffPanel
    {
      get
      {
        if (this.selfLogoffPanel == null)
          this.selfLogoffPanel = this.FindControl("selfLogoffPanel") as Panel;
        return this.selfLogoffPanel;
      }
    }

    /// <summary>Gets the login panel.</summary>
    /// <value>The login panel.</value>
    [Browsable(false)]
    public virtual Panel LoginPanel
    {
      get
      {
        if (this.loginPanel == null)
          this.loginPanel = this.FindControl("loginPanel") as Panel;
        return this.loginPanel;
      }
    }

    /// <summary>Gets the logout button.</summary>
    /// <value>The logout button.</value>
    [Browsable(false)]
    public virtual LinkButton LogoutButton
    {
      get
      {
        if (this.logoutButton == null)
          this.logoutButton = this.FindControl("logoutButton") as LinkButton;
        return this.logoutButton;
      }
    }

    /// <summary>Gets the self logout button.</summary>
    /// <value>The self logout button.</value>
    [Browsable(false)]
    public virtual LinkButton SelfLogoutButton
    {
      get
      {
        if (this.selftLogoutButton == null)
          this.selftLogoutButton = this.FindControl("selfLogoutButton") as LinkButton;
        return this.selftLogoutButton;
      }
    }

    [Browsable(false)]
    public virtual LinkButton SelfLogoutCancelButton
    {
      get
      {
        if (this.selfLogoutCancelButton == null)
          this.selfLogoutCancelButton = this.FindControl("selfLogoutCancelButton") as LinkButton;
        return this.selfLogoutCancelButton;
      }
    }

    [Browsable(false)]
    public virtual string LogoutUser
    {
      get => this.FindControl("logoutUser") is HiddenField control ? control.Value : (string) null;
      set
      {
        if (!(this.FindControl("logoutUser") is HiddenField control))
          return;
        control.Value = value;
      }
    }

    [Browsable(false)]
    public virtual bool SmtpSettingsAreSet
    {
      get => this.GetHiddenBoolean("smtpSettingsAreSet");
      set
      {
        if (!(this.FindControl("smtpSettingsAreSet") is HiddenField control))
          return;
        control.Value = value.ToString();
      }
    }

    [Browsable(false)]
    public virtual bool SmtpPermissionDenied
    {
      get => this.GetHiddenBoolean("smtpPermissionDenied");
      set
      {
        if (!(this.FindControl("smtpPermissionDenied") is HiddenField control))
          return;
        control.Value = value.ToString();
      }
    }

    [Browsable(false)]
    public Literal SmtpPermissionErrorMessage => this.FindControl(nameof (SmtpPermissionErrorMessage)) as Literal;

    /// <summary>
    /// Gets or sets the path to a custom layout template for the control.
    /// </summary>
    public virtual string LayoutTemplatePath
    {
      get
      {
        string layoutTemplatePath = this.ViewState[nameof (LayoutTemplatePath)] as string;
        if (string.IsNullOrEmpty(layoutTemplatePath))
          layoutTemplatePath = LoginForm.BackendLayoutTemplatePath;
        return layoutTemplatePath;
      }
      set => this.ViewState[nameof (LayoutTemplatePath)] = (object) value;
    }

    /// <summary>
    /// Gets or sets the path to a custom layout template for the control.
    /// </summary>
    [Browsable(false)]
    [DescriptionResource(typeof (PageResources), "LayoutTemplateDescription")]
    public override ITemplate LayoutTemplate
    {
      get
      {
        if (base.LayoutTemplate == null)
          base.LayoutTemplate = this.CreateLayoutTemplate();
        return base.LayoutTemplate;
      }
      set => base.LayoutTemplate = value;
    }

    /// <summary>
    /// Gets or sets the authentication ticket.
    /// The ticket is used when the user cannot be logged in due to the licensing limits/restrictions
    /// </summary>
    /// <value>The authentication ticket.</value>
    [Browsable(false)]
    public virtual string AuthTicket
    {
      get => this.FindControl("loginTicket") is HiddenField control ? control.Value : (string) null;
      set
      {
        if (!(this.FindControl("loginTicket") is HiddenField control))
          return;
        control.Value = value;
      }
    }

    /// <summary>
    /// Gets or sets the mode.
    /// This represents the workflow scenario.
    /// </summary>
    /// <value>The mode.</value>
    public virtual string Mode
    {
      get => this.FindControl("mode") is HiddenField control ? control.Value : string.Empty;
      set
      {
        if (!(this.FindControl("mode") is HiddenField control))
          return;
        control.Value = value;
      }
    }

    public override string TitleText
    {
      get => this.LoginTitle != null ? this.LoginTitle.Text : string.Empty;
      set
      {
        if (this.LoginTitle != null)
          this.LoginTitle.Text = value;
        if (this.UsersListPanelLoginTitle != null)
          this.UsersListPanelLoginTitle.Text = value;
        if (this.SelfLogoffPanelLoginTitle != null)
          this.SelfLogoffPanelLoginTitle.Text = value;
        if (this.DenyLogonPanelLoginTitle == null)
          return;
        this.DenyLogonPanelLoginTitle.Text = value;
      }
    }

    /// <summary>Gets the login title.</summary>
    /// <value>The login title.</value>
    protected virtual Literal LoginTitle
    {
      get
      {
        if (this.loginTitle == null)
          this.loginTitle = this.FindControl(nameof (LoginTitle)) as Literal;
        return this.loginTitle;
      }
    }

    protected virtual Literal UsersListPanelLoginTitle
    {
      get
      {
        if (this.usersListPanelLoginTitle == null)
          this.usersListPanelLoginTitle = this.FindControl(nameof (UsersListPanelLoginTitle)) as Literal;
        return this.usersListPanelLoginTitle;
      }
    }

    protected virtual Literal SelfLogoffPanelLoginTitle
    {
      get
      {
        if (this.selfLogoffPanelLoginTitle == null)
          this.selfLogoffPanelLoginTitle = this.FindControl(nameof (SelfLogoffPanelLoginTitle)) as Literal;
        return this.selfLogoffPanelLoginTitle;
      }
    }

    protected virtual Literal DenyLogonPanelLoginTitle
    {
      get
      {
        if (this.denyLogonPanelLoginTitle == null)
          this.denyLogonPanelLoginTitle = this.FindControl(nameof (DenyLogonPanelLoginTitle)) as Literal;
        return this.denyLogonPanelLoginTitle;
      }
    }

    /// <summary>
    /// Gets or sets the text of the label for the <see cref="P:System.Web.UI.WebControls.Login.UserName" /> text box.
    /// </summary>
    /// <value></value>
    /// <returns>The text of the label for the <see cref="P:System.Web.UI.WebControls.Login.UserName" /> text box. The default is "User Name:".</returns>
    public override string UserNameLabelText
    {
      get => this.UserNameLabel.Text;
      set => this.UserNameLabel.Text = value;
    }

    /// <summary>Gets the user name label.</summary>
    /// <value>The user name label.</value>
    protected virtual Label UserNameLabel
    {
      get
      {
        if (this.userNameLabel == null)
          this.userNameLabel = this.FindControl(nameof (UserNameLabel)) as Label;
        return this.userNameLabel;
      }
    }

    public string ProvidersLabelText
    {
      get => this.ProvidersLabel.Text;
      set => this.ProvidersLabel.Text = value;
    }

    /// <summary>Gets the user name label.</summary>
    /// <value>The user name label.</value>
    protected virtual Label ProvidersLabel
    {
      get
      {
        if (this.providersLabel == null)
          this.providersLabel = this.FindControl(nameof (ProvidersLabel)) as Label;
        return this.providersLabel;
      }
    }

    /// <summary>
    /// Gets or sets the error message to display in a <see cref="T:System.Web.UI.WebControls.ValidationSummary" /> control when the user name field is left blank.
    /// </summary>
    /// <value></value>
    /// <returns>The error message to display in a <see cref="T:System.Web.UI.WebControls.ValidationSummary" /> control when the user name field is left blank. The default is "User Name." </returns>
    public override string UserNameRequiredErrorMessage
    {
      get => this.UserNameRequiredValidator.ErrorMessage;
      set
      {
        this.UserNameRequiredValidator.ErrorMessage = value;
        this.UserNameRequiredLiteral.Text = value;
      }
    }

    /// <summary>Gets the user name required validator.</summary>
    /// <value>The user name required validator.</value>
    protected virtual RequiredFieldValidator UserNameRequiredValidator
    {
      get
      {
        if (this.userNameRequiredValidator == null)
          this.userNameRequiredValidator = this.FindControl("UserNameRequired") as RequiredFieldValidator;
        return this.userNameRequiredValidator;
      }
    }

    /// <summary>Gets the user name required literal message.</summary>
    /// <value>The user name required literal message.</value>
    protected virtual Literal UserNameRequiredLiteral
    {
      get
      {
        if (this.userNameRequiredLiteral == null)
          this.userNameRequiredLiteral = this.FindControl(nameof (UserNameRequiredLiteral)) as Literal;
        return this.userNameRequiredLiteral;
      }
    }

    /// <summary>
    /// Gets or sets the text of the label for the <see cref="P:System.Web.UI.WebControls.Login.Password" /> text box.
    /// </summary>
    /// <value></value>
    /// <returns>The text of the label for the <see cref="P:System.Web.UI.WebControls.Login.Password" /> text box. The default is "Password:".</returns>
    public override string PasswordLabelText
    {
      get => this.PasswordLabel.Text;
      set => this.PasswordLabel.Text = value;
    }

    /// <summary>
    /// Gets or sets the label for the <see cref="P:System.Web.UI.WebControls.Login.Password" /> text box.
    /// </summary>
    /// <value>The label for the <see cref="P:System.Web.UI.WebControls.Login.Password" /> text box.</value>
    protected virtual Label PasswordLabel
    {
      get
      {
        if (this.passwordLabel == null)
          this.passwordLabel = this.FindControl(nameof (PasswordLabel)) as Label;
        return this.passwordLabel;
      }
    }

    /// <summary>Gets or sets the password required error message.</summary>
    /// <value>The password required error message.</value>
    public override string PasswordRequiredErrorMessage
    {
      get => this.PasswordRequiredValidator.ErrorMessage;
      set
      {
        this.PasswordRequiredValidator.ErrorMessage = value;
        this.PasswordRequiredLiteral.Text = value;
      }
    }

    /// <summary>Gets the password required validator.</summary>
    /// <value>The password required validator.</value>
    protected virtual RequiredFieldValidator PasswordRequiredValidator
    {
      get
      {
        if (this.passwordRequiredValidator == null)
          this.passwordRequiredValidator = this.FindControl("PasswordRequired") as RequiredFieldValidator;
        return this.passwordRequiredValidator;
      }
    }

    /// <summary>Gets the password required literal.</summary>
    /// <value>The password required literal.</value>
    protected virtual Literal PasswordRequiredLiteral
    {
      get
      {
        if (this.passwordRequiredLiteral == null)
          this.passwordRequiredLiteral = this.FindControl(nameof (PasswordRequiredLiteral)) as Literal;
        return this.passwordRequiredLiteral;
      }
    }

    /// <summary>Gets or sets the remember me text.</summary>
    /// <value>The remember me text.</value>
    public override string RememberMeText
    {
      get => this.RememberMeTextLabel.Text;
      set => this.RememberMeTextLabel.Text = value;
    }

    /// <summary>Gets the remember me text label.</summary>
    /// <value>The remember me text label.</value>
    protected virtual Label RememberMeTextLabel
    {
      get
      {
        if (this.rememberMeTextLabel == null)
          this.rememberMeTextLabel = this.FindControl(nameof (RememberMeTextLabel)) as Label;
        return this.rememberMeTextLabel;
      }
    }

    /// <summary>Gets or sets the login button text.</summary>
    /// <value>The login button text.</value>
    public override string LoginButtonText
    {
      get => this.LoginButtonLiteral.Text;
      set => this.LoginButtonLiteral.Text = value;
    }

    /// <summary>Gets the login button literal.</summary>
    /// <value>The login button literal.</value>
    protected virtual Literal LoginButtonLiteral
    {
      get
      {
        if (this.loginButtonLiteral == null)
          this.loginButtonLiteral = this.FindControl(nameof (LoginButtonLiteral)) as Literal;
        return this.loginButtonLiteral;
      }
    }

    /// <summary>Gets or sets the user limit label text.</summary>
    /// <value>The user limit label text.</value>
    public string UserLimitLabelText
    {
      get => this.UserLimitLabel.Text;
      set => this.UserLimitLabel.Text = value;
    }

    /// <summary>Gets the user limit label.</summary>
    /// <value>The user limit label.</value>
    protected virtual Literal UserLimitLabel
    {
      get
      {
        if (this.userLimitLabel == null)
          this.userLimitLabel = this.FindControl("userLimitLabel") as Literal;
        return this.userLimitLabel;
      }
    }

    /// <summary>Gets or sets the logout button text.</summary>
    /// <value>The logout button text.</value>
    public string LogoutButtonText
    {
      get => this.LogoutButtonLiteral.Text;
      set => this.LogoutButtonLiteral.Text = value;
    }

    /// <summary>Gets the logout button literal.</summary>
    /// <value>The logout button literal.</value>
    protected virtual Literal LogoutButtonLiteral
    {
      get
      {
        if (this.logoutButtonLiteral == null)
          this.logoutButtonLiteral = this.FindControl(nameof (LogoutButtonLiteral)) as Literal;
        return this.logoutButtonLiteral;
      }
    }

    /// <summary>Gets or sets the logout other user and enter text.</summary>
    /// <value>The logout other user and enter text.</value>
    public string LogoutOtherUserAndEnterText
    {
      get => this.LogoutOtherUserAndEnterLiteral.Text;
      set => this.LogoutOtherUserAndEnterLiteral.Text = value;
    }

    /// <summary>Gets the logout other user and enter literal.</summary>
    /// <value>The logout other user and enter literal.</value>
    protected virtual Literal LogoutOtherUserAndEnterLiteral
    {
      get
      {
        if (this.logoutOtherUserAndEnterLiteral == null)
          this.logoutOtherUserAndEnterLiteral = this.FindControl(nameof (LogoutOtherUserAndEnterLiteral)) as Literal;
        return this.logoutOtherUserAndEnterLiteral;
      }
    }

    /// <summary>Gets or sets the deny logon message text.</summary>
    /// <value>The deny logon message text.</value>
    public string DenyLogonMessageText
    {
      get => this.DenyLogonMesage.Text;
      set => this.DenyLogonMesage.Text = value;
    }

    /// <summary>Gets the deny logon mesage.</summary>
    /// <value>The deny logon mesage.</value>
    protected virtual Literal DenyLogonMesage
    {
      get
      {
        if (this.denyLogonMesage == null)
          this.denyLogonMesage = this.FindControl("denyLogonMesage") as Literal;
        return this.denyLogonMesage;
      }
    }

    /// <summary>Gets or sets the login retry message text.</summary>
    /// <value>The login retry message text.</value>
    public string LoginRetryMessageText
    {
      get => this.LoginRetryMessage.Text;
      set => this.LoginRetryMessage.Text = value;
    }

    /// <summary>Gets the login retry message.</summary>
    /// <value>The login retry message.</value>
    protected virtual Literal LoginRetryMessage
    {
      get
      {
        if (this.loginRetryMessage == null)
          this.loginRetryMessage = this.FindControl("loginRetryMessage") as Literal;
        return this.loginRetryMessage;
      }
    }

    /// <summary>Gets or sets the help page text.</summary>
    /// <value>The help page text.</value>
    public override string HelpPageText
    {
      get => this.HelpLinkLiteral.Text;
      set
      {
        this.HelpLinkLiteral.Text = value;
        this.HelpTitleLiteral.Text = value;
      }
    }

    /// <summary>Gets the help title literal.</summary>
    /// <value>The help title literal.</value>
    protected virtual Literal HelpTitleLiteral
    {
      get
      {
        if (this.helpTitleLiteral == null)
          this.helpTitleLiteral = this.FindControl(nameof (HelpTitleLiteral)) as Literal;
        return this.helpTitleLiteral;
      }
    }

    /// <summary>Gets the help link literal.</summary>
    /// <value>The help link literal.</value>
    protected virtual Literal HelpLinkLiteral
    {
      get
      {
        if (this.helpLinkLiteral == null)
          this.helpLinkLiteral = this.FindControl(nameof (HelpLinkLiteral)) as Literal;
        return this.helpLinkLiteral;
      }
    }

    public override string PasswordRecoveryText
    {
      get => this.PasswordRecoveryTextLiteral.Text;
      set => this.PasswordRecoveryTextLiteral.Text = value;
    }

    /// <summary>Gets the password recovery text literal.</summary>
    /// <value>The password recovery text literal.</value>
    protected virtual Literal PasswordRecoveryTextLiteral
    {
      get
      {
        if (this.passwordRecoveryTextLiteral == null)
          this.passwordRecoveryTextLiteral = this.FindControl(nameof (PasswordRecoveryTextLiteral)) as Literal;
        return this.passwordRecoveryTextLiteral;
      }
    }

    /// <summary>Gets or sets the password recovery URL.</summary>
    /// <value>The password recovery URL.</value>
    public override string PasswordRecoveryUrl
    {
      get => this.ForgotPasswordLink.HRef;
      set => this.ForgotPasswordLink.HRef = value;
    }

    /// <summary>Gets or sets the password change text.</summary>
    /// <value>The password change text.</value>
    public string PasswordChangeText
    {
      get => this.ChangePasswordTextLiteral.Text;
      set => this.ChangePasswordTextLiteral.Text = value;
    }

    /// <summary>Gets the change password text literal.</summary>
    /// <value>The change password text literal.</value>
    protected virtual Literal ChangePasswordTextLiteral
    {
      get
      {
        if (this.changePasswordTextLiteral == null)
          this.changePasswordTextLiteral = this.FindControl(nameof (ChangePasswordTextLiteral)) as Literal;
        return this.changePasswordTextLiteral;
      }
    }

    /// <summary>Gets or sets the password change URL.</summary>
    /// <value>The password change URL.</value>
    public string PasswordChangeUrl
    {
      get => this.ChangePasswordLink.HRef;
      set => this.ChangePasswordLink.HRef = value;
    }

    /// <summary>Gets or sets the create user text.</summary>
    /// <value>The create user text.</value>
    public override string CreateUserText
    {
      get => this.RegisterUserTextLiteral.Text;
      set => this.RegisterUserTextLiteral.Text = value;
    }

    /// <summary>Gets the register user text literal.</summary>
    /// <value>The register user text literal.</value>
    protected virtual Literal RegisterUserTextLiteral
    {
      get
      {
        if (this.registerUserTextLiteral == null)
          this.registerUserTextLiteral = this.FindControl(nameof (RegisterUserTextLiteral)) as Literal;
        return this.registerUserTextLiteral;
      }
    }

    /// <summary>Gets or sets the create user URL.</summary>
    /// <value>The create user URL.</value>
    public override string CreateUserUrl
    {
      get => this.CreateUserLink.HRef;
      set => this.CreateUserLink.HRef = value;
    }

    /// <summary>
    /// Gets or sets the error message no SMTP configured text.
    /// </summary>
    /// <value>The error message no SMTP configured text.</value>
    public string ErrorMessageNoSmtpConfiguredText
    {
      get => this.ErrorMessageNoSmtpConfigLiteral.Text;
      set => this.ErrorMessageNoSmtpConfigLiteral.Text = value;
    }

    /// <summary>Gets the error message no SMTP config literal.</summary>
    /// <value>The error message no SMTP config literal.</value>
    protected virtual Literal ErrorMessageNoSmtpConfigLiteral
    {
      get
      {
        if (this.errorMessageNoSmtpConfigLiteral == null)
          this.errorMessageNoSmtpConfigLiteral = this.FindControl(nameof (ErrorMessageNoSmtpConfigLiteral)) as Literal;
        return this.errorMessageNoSmtpConfigLiteral;
      }
    }

    /// <summary>
    /// Gets or sets the error message contact admin to reset your password text.
    /// </summary>
    /// <value>The error message contact admin to reset your password text.</value>
    public string ErrorMessageContactAdminToResetYourPasswordText
    {
      get => this.ErrorMessageContactAdminToResetYourPasswordLiteral.Text;
      set
      {
        this.ErrorMessageContactAdminToResetYourPasswordLiteral.Text = value;
        this.ErrorMessageContactAdminToResetYourPasswordSmtpLiteral.Text = value;
      }
    }

    /// <summary>
    /// Gets the error message contact admin to reset your password literal.
    /// </summary>
    /// <value>The error message contact admin to reset your password literal.</value>
    protected virtual Literal ErrorMessageContactAdminToResetYourPasswordLiteral
    {
      get
      {
        if (this.errorMessageContactAdminToResetYourPasswordLiteral == null)
          this.errorMessageContactAdminToResetYourPasswordLiteral = this.FindControl(nameof (ErrorMessageContactAdminToResetYourPasswordLiteral)) as Literal;
        return this.errorMessageContactAdminToResetYourPasswordLiteral;
      }
    }

    /// <summary>
    /// Gets the error message contact admin to reset your password SMTP literal.
    /// </summary>
    /// <value>
    /// The error message contact admin to reset your password SMTP literal.
    /// </value>
    protected virtual Literal ErrorMessageContactAdminToResetYourPasswordSmtpLiteral
    {
      get
      {
        if (this.errorMessageContactAdminToResetYourPasswordSmtpLiteral == null)
          this.errorMessageContactAdminToResetYourPasswordSmtpLiteral = this.FindControl(nameof (ErrorMessageContactAdminToResetYourPasswordSmtpLiteral)) as Literal;
        return this.errorMessageContactAdminToResetYourPasswordSmtpLiteral;
      }
    }

    /// <summary>
    /// Gets the error or ask an administrator to configure the system SMTP literal.
    /// </summary>
    /// <value>
    /// The error or ask an administrator to configure the system SMTP literal.
    /// </value>
    protected virtual Literal ErrorOrAskAnAdministratorToConfigureTheSystemSmtpLiteral
    {
      get
      {
        if (this.errorOrAskAnAdministratorToConfigureTheSystemSmtpLiteral == null)
          this.errorOrAskAnAdministratorToConfigureTheSystemSmtpLiteral = this.FindControl(nameof (ErrorOrAskAnAdministratorToConfigureTheSystemSmtpLiteral)) as Literal;
        return this.errorOrAskAnAdministratorToConfigureTheSystemSmtpLiteral;
      }
    }

    /// <summary>
    /// Gets or sets the error or ask an administrator to configure the system text.
    /// </summary>
    /// <value>The error or ask an administrator to configure the system text.</value>
    public string ErrorOrAskAnAdministratorToConfigureTheSystemText
    {
      get => this.ErrorOrAskAnAdministratorToConfigureTheSystemLiteral.Text;
      set
      {
        this.ErrorOrAskAnAdministratorToConfigureTheSystemLiteral.Text = value;
        this.ErrorOrAskAnAdministratorToConfigureTheSystemSmtpLiteral.Text = value;
      }
    }

    /// <summary>
    /// Gets the error or ask an administrator to configure the system literal.
    /// </summary>
    /// <value>
    /// The error or ask an administrator to configure the system literal.
    /// </value>
    protected virtual Literal ErrorOrAskAnAdministratorToConfigureTheSystemLiteral
    {
      get
      {
        if (this.errorOrAskAnAdministratorToConfigureTheSystemLiteral == null)
          this.errorOrAskAnAdministratorToConfigureTheSystemLiteral = this.FindControl(nameof (ErrorOrAskAnAdministratorToConfigureTheSystemLiteral)) as Literal;
        return this.errorOrAskAnAdministratorToConfigureTheSystemLiteral;
      }
    }

    /// <summary>Gets or sets the error details title text.</summary>
    /// <value>The error details title text.</value>
    public string ErrorDetailsTitleText
    {
      get => this.ErrorMessageSmtpDetailsTitle.Text;
      set
      {
        this.ErrorMessageSmtpDetailsTitle.Text = value;
        this.ErrorMessageSmtpPermissionDeniedDetailsTitle.Text = value;
      }
    }

    /// <summary>Gets the error message SMTP details title.</summary>
    /// <value>The error message SMTP details title.</value>
    protected virtual Literal ErrorMessageSmtpDetailsTitle
    {
      get
      {
        if (this.errorMessageSmtpDetailsTitle == null)
          this.errorMessageSmtpDetailsTitle = this.FindControl(nameof (ErrorMessageSmtpDetailsTitle)) as Literal;
        return this.errorMessageSmtpDetailsTitle;
      }
    }

    /// <summary>
    /// Gets the error message SMTP permission denied details title.
    /// </summary>
    /// <value>The error message SMTP permission denied details title.</value>
    protected virtual Literal ErrorMessageSmtpPermissionDeniedDetailsTitle
    {
      get
      {
        if (this.errorMessageSmtpPermissionDeniedDetailsTitle == null)
          this.errorMessageSmtpPermissionDeniedDetailsTitle = this.FindControl(nameof (ErrorMessageSmtpPermissionDeniedDetailsTitle)) as Literal;
        return this.errorMessageSmtpPermissionDeniedDetailsTitle;
      }
    }

    /// <summary>Gets or sets the error SMTP settings not set text.</summary>
    /// <value>The error SMTP settings not set text.</value>
    public string ErrorSmtpSettingsNotSetText
    {
      get => this.ErrorMessageSmtpSettingsNotSetLiteral.Text;
      set => this.ErrorMessageSmtpSettingsNotSetLiteral.Text = value;
    }

    /// <summary>Gets the error message SMTP settings not set literal.</summary>
    /// <value>The error message SMTP settings not set literal.</value>
    protected virtual Literal ErrorMessageSmtpSettingsNotSetLiteral
    {
      get
      {
        if (this.errorMessageSmtpSettingsNotSetLiteral == null)
          this.errorMessageSmtpSettingsNotSetLiteral = this.FindControl(nameof (ErrorMessageSmtpSettingsNotSetLiteral)) as Literal;
        return this.errorMessageSmtpSettingsNotSetLiteral;
      }
    }

    /// <summary>Gets or sets the error message how to set SMTP text.</summary>
    /// <value>The error message how to set SMTP text.</value>
    public string ErrorMessageHowToSetSmtpText
    {
      get => this.ErrorMessageHowToSetSmtpLiteral.Text;
      set => this.ErrorMessageHowToSetSmtpLiteral.Text = value;
    }

    /// <summary>Gets the error message how to set SMTP literal.</summary>
    /// <value>The error message how to set SMTP literal.</value>
    protected virtual Literal ErrorMessageHowToSetSmtpLiteral
    {
      get
      {
        if (this.errorMessageHowToSetSmtpLiteral == null)
          this.errorMessageHowToSetSmtpLiteral = this.FindControl(nameof (ErrorMessageHowToSetSmtpLiteral)) as Literal;
        return this.errorMessageHowToSetSmtpLiteral;
      }
    }

    /// <summary>
    /// Gets or sets the error message the sys is not permitted to send emails text.
    /// </summary>
    /// <value>The error message the sys is not permitted to send emails text.</value>
    public string ErrorMessageTheSysIsNotPermittedToSendEmailsText
    {
      get => this.ErrorMessageTheSysIsNotPermittedToSendEmailsLiteral.Text;
      set => this.ErrorMessageTheSysIsNotPermittedToSendEmailsLiteral.Text = value;
    }

    /// <summary>
    /// Gets the error message the sys is not permitted to send emails literal.
    /// </summary>
    /// <value>
    /// The error message the sys is not permitted to send emails literal.
    /// </value>
    protected virtual Literal ErrorMessageTheSysIsNotPermittedToSendEmailsLiteral
    {
      get
      {
        if (this.errorMessageTheSysIsNotPermittedToSendEmailsLiteral == null)
          this.errorMessageTheSysIsNotPermittedToSendEmailsLiteral = this.FindControl(nameof (ErrorMessageTheSysIsNotPermittedToSendEmailsLiteral)) as Literal;
        return this.errorMessageTheSysIsNotPermittedToSendEmailsLiteral;
      }
    }

    private void AddProviderQueryKey(HtmlAnchor link)
    {
      if (string.IsNullOrEmpty(this.MembershipProvider))
        return;
      if (link.HRef.IndexOf("?") >= 0)
        link.HRef = link.HRef + "&" + "provider" + "=" + this.MembershipProvider;
      else
        link.HRef = link.HRef + "?" + "provider" + "=" + this.MembershipProvider;
    }

    private void UpdateVisibilityOfLinks()
    {
      if (this.updateVisibilityOfLinksDisabled)
        return;
      if (!this.ShowChangePasswordLink && !this.ShowForgotPasswordLink && !this.ShowHelpLink && !this.ShowRegisterUserLink && this.LoginLinksHolder != null)
      {
        this.loginLinksHolder.Visible = false;
      }
      else
      {
        if (this.ChangePasswordLink != null)
        {
          this.changePasswordLink.Visible = this.ShowChangePasswordLink;
          this.AddProviderQueryKey(this.changePasswordLink);
        }
        if (this.ForgotPasswordLink != null)
        {
          this.forgotPasswordLink.Visible = this.ShowForgotPasswordLink;
          this.AddProviderQueryKey(this.forgotPasswordLink);
        }
        if (this.CreateUserLink != null)
        {
          this.createUserLink.Visible = this.ShowRegisterUserLink;
          this.AddProviderQueryKey(this.createUserLink);
        }
        if (this.HelpLink != null)
          this.helpLink.Visible = this.ShowHelpLink;
      }
      bool areSmtpSettingsSet = LoginUtils.AreSmtpSettingsSet;
      bool flag = areSmtpSettingsSet && LoginUtils.AreProvidersSetUpForPasswordRecovery;
      if (this.ChangePasswordLink != null && !flag)
        this.ChangePasswordLink.HRef = "javascript:void(0)";
      if (this.ForgotPasswordLink != null && !flag)
        this.ForgotPasswordLink.HRef = "javascript:void(0)";
      if (this.CreateUserLink != null && !flag)
        this.CreateUserLink.HRef = "javascript:void(0)";
      if (this.HelpLink != null && !flag)
        this.HelpLink.HRef = "javascript:void(0)";
      this.SmtpSettingsAreSet = areSmtpSettingsSet;
      this.SmtpPermissionDenied = !areSmtpSettingsSet;
      if (areSmtpSettingsSet)
        return;
      this.SmtpPermissionErrorMessage.Text = LoginUtils.SmtpPermissionErrorMessage;
    }

    private bool GetHiddenBoolean(string id)
    {
      if (!(this.FindControl(id) is HiddenField control) || string.IsNullOrEmpty(control.Value))
        return false;
      bool result = false;
      bool.TryParse(control.Value, out result);
      return result;
    }

    private void InitializeProvidersList()
    {
      bool flag;
      if (this.AllowSelectMembershipProvider)
      {
        IEnumerable<DataProviderBase> contextProviders = UserManager.GetManager().GetContextProviders();
        if (contextProviders.Any<DataProviderBase>())
        {
          if (this.ProvidersList == null)
            throw new TemplateException(string.IsNullOrEmpty(this.LayoutTemplatePath) ? this.LayoutTemplateName : this.LayoutTemplatePath, "System.Web.UI.WebControls.ListControl", "ProvidersList");
          this.ProvidersList.SelectedIndexChanged += new EventHandler(this.ProvidersList_SelectedIndexChanged);
          string defaultProviderName = ManagerBase<MembershipDataProvider>.GetDefaultProviderName();
          this.ProvidersList.Items.Clear();
          foreach (DataProviderBase dataProviderBase in contextProviders)
          {
            dataProviderBase.SuppressSecurityChecks = true;
            ListItem listItem = new ListItem(dataProviderBase.Title, dataProviderBase.Name);
            if (string.IsNullOrEmpty(this.MembershipProvider) && defaultProviderName == dataProviderBase.Name || dataProviderBase.Name == this.MembershipProvider)
              listItem.Selected = true;
            this.ProvidersList.Items.Add(listItem);
            dataProviderBase.SuppressSecurityChecks = false;
          }
          flag = this.ProvidersList.Items.Count > 1;
        }
        else
          flag = false;
      }
      else
        flag = false;
      if (this.ProvidersHolder != null)
        this.ProvidersHolder.Visible = flag;
      if (this.ProvidersList == null)
        return;
      this.ProvidersList.Visible = flag;
    }

    /// <summary>
    /// Creates the individual controls that make up the
    /// <see cref="T:System.Web.UI.WebControls.Login" /> control and associates event handlers with their events.
    /// </summary>
    protected override void CreateChildControls()
    {
      this.Authenticate += new AuthenticateEventHandler(this.LoginForm_Authenticate);
      base.CreateChildControls();
      this.InitializeProvidersList();
    }

    private void ProvidersList_SelectedIndexChanged(object sender, EventArgs e) => this.MembershipProvider = this.ProvidersList.SelectedValue;

    /// <summary>Builds the authentication ticket.</summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="userName">Name of the user.</param>
    /// <param name="isAdmin">if set to <c>true</c> [is admin].</param>
    /// <returns></returns>
    private string BuildAuthTicket(bool isAdmin) => SecurityManager.EncryptData(string.Format("{0}|{1}|{2}|{3}|{4}|{5}", (object) this.MembershipProvider, (object) this.UserName, (object) isAdmin, (object) DateTime.UtcNow.ToString((IFormatProvider) CultureInfo.InvariantCulture), (object) this.Password, (object) this.RememberMeSet));

    /// <summary>Parses the authentication ticket.</summary>
    /// <param name="value">The value.</param>
    /// <returns>Array of strings contained the values</returns>
    protected virtual string[] ParseAuthTicket(string value)
    {
      if (string.IsNullOrEmpty(value))
        return (string[]) null;
      return SecurityManager.DecryptData(value).Split('|');
    }

    /// <summary>Occurs when a user is authenticated.</summary>
    /// <param name="sender"></param>
    /// <param name="e"><see cref="T:System.Web.UI.WebControls.AuthenticateEventArgs" /></param>
    protected virtual void LoginForm_Authenticate(object sender, AuthenticateEventArgs e)
    {
      if (string.IsNullOrEmpty(this.MembershipProvider))
        this.MembershipProvider = ManagerBase<MembershipDataProvider>.GetDefaultProviderName();
      UserLoggingReason reason = SecurityManager.AuthenticateUser(this.MembershipProvider, this.UserName, this.Password, this.RememberMeSet, out this.currentUser);
      e.Authenticated = reason == UserLoggingReason.Success;
      if (e.Authenticated)
        return;
      this.PrepareMessage(reason);
      HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
      this.invalidCastExceptionThrown = false;
      try
      {
        SitefinityIdentity identity = (SitefinityIdentity) currentHttpContext.User.Identity;
      }
      catch (InvalidCastException ex)
      {
        this.invalidCastExceptionThrown = true;
        this.FailureTextControl.Text = ex.Message + Res.Get<PublicControlsResources>().LoginControlAvailabilityCastException;
      }
      if (reason != UserLoggingReason.UserLimitReached && reason != UserLoggingReason.UserLoggedFromDifferentIp && reason != UserLoggingReason.UserLoggedFromDifferentComputer && reason != UserLoggingReason.UserAlreadyLoggedIn)
        return;
      this.PrepareWorkflowPanels(ClaimsManager.GetIdentity(currentHttpContext).IsUnrestricted, reason);
    }

    /// <summary>Binds the logged in users list.</summary>
    protected virtual void BindLoggedInUsersList()
    {
      if (!(this.Mode == "AdminLogsOutUser"))
        return;
      foreach (User loggedInBackendUser in (IEnumerable<User>) SecurityManager.GetLoggedInBackendUsers())
      {
        string userDisplayName = UserProfilesHelper.GetUserDisplayName(loggedInBackendUser.Id);
        this.UserListChoice.Choices.Add(new ChoiceItem()
        {
          Text = userDisplayName,
          Value = loggedInBackendUser.Id.ToString() + ";" + loggedInBackendUser.ManagerInfo.ProviderName
        });
      }
      this.UserListChoice.DataBind();
    }

    /// <summary>
    /// Handles the Command event of the LogoutButton control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:System.Web.UI.WebControls.CommandEventArgs" /> instance containing the event data.</param>
    private void LogoutButton_Command(object sender, CommandEventArgs e) => this.LogOutUserLogInMe();

    /// <summary>
    /// Prepares the Workflow panels.
    /// Display different panels regarding on of the currents scenario (property Mode)
    /// </summary>
    /// <param name="isAdmin">if set to <c>true</c> [is admin].</param>
    /// <param name="reason"></param>
    protected void PrepareWorkflowPanels(bool isAdmin, UserLoggingReason reason)
    {
      this.LoginPanel.Visible = false;
      this.AuthTicket = this.BuildAuthTicket(isAdmin);
      UserActivity userActivity = UserActivityManager.GetManager().GetUserActivity(this.currentUser.Id, this.currentUser.ProviderName);
      if (reason == UserLoggingReason.UserLimitReached)
      {
        if (isAdmin)
        {
          this.Mode = "AdminLogsOutUser";
          this.BindLoggedInUsersList();
          this.UserListPanel.Visible = true;
        }
        else
          this.DisplayDenyLogin();
      }
      else if (userActivity.LastActivityDate >= SecurityManager.ExpiredSessionsLastLoginDate)
      {
        this.SetSelfLogoutMode(this.currentUser, "UserAlreadyLoggedIn");
      }
      else
      {
        this.Mode = string.Empty;
        this.LoginPanel.Visible = true;
        this.SelfLogoffPanel.Visible = false;
      }
    }

    internal void SetSelfLogoutMode(User user, string mode)
    {
      this.Mode = mode;
      this.LoginPanel.Visible = false;
      this.SelfLogoffPanel.Visible = true;
      this.LogoutUser = user.Id.ToString() + ";" + user.ManagerInfo.ProviderName;
    }

    /// <summary>Displays the deny login panel.</summary>
    protected virtual void DisplayDenyLogin()
    {
      this.Mode = "DenyLogonUser";
      this.LoginPanel.Visible = false;
      this.DenyLogonPanel.Visible = true;
      this.PrepareMessage(UserLoggingReason.NeedAdminRights);
    }

    protected virtual void DisplayLoginPanel()
    {
      this.Mode = string.Empty;
      this.DenyLogonPanel.Visible = false;
      this.UserListPanel.Visible = false;
      this.SelfLogoffPanel.Visible = false;
      this.LoginPanel.Visible = true;
    }

    /// <summary>
    /// Logs out the selected user and log in the user requesting to log in.
    /// </summary>
    protected virtual void LogOutUserLogInMe()
    {
      string[] authTicket = this.ParseAuthTicket(this.AuthTicket);
      string membershipProviderName = authTicket[0];
      string userName = authTicket[1];
      bool flag = bool.Parse(authTicket[2]);
      DateTime dateTime = DateTime.SpecifyKind(DateTime.Parse(authTicket[3], (IFormatProvider) CultureInfo.InvariantCulture), DateTimeKind.Utc);
      string password = authTicket[4];
      bool persistent = bool.Parse(authTicket[5]);
      if (flag || this.Mode == "UserAlreadyLoggedIn")
      {
        if (!string.IsNullOrEmpty(this.LogoutUser))
        {
          if (ClaimsManager.GetCurrentIdentity().Name == userName)
          {
            SecurityManager.BuildLogoutCookie(UserLoggingReason.UserLoggedOff, SystemManager.CurrentHttpContext);
            SecurityManager.Logout();
          }
          else
          {
            string[] strArray = this.LogoutUser.Split(';');
            SecurityManager.BuildLogoutCookie(UserLoggingReason.UserLoggedOff, SystemManager.CurrentHttpContext);
            SecurityManager.Logout(strArray[1], new Guid(strArray[0]), new Credentials()
            {
              MembershipProvider = membershipProviderName,
              UserName = userName,
              Password = password
            });
          }
        }
        if (dateTime + this.AuthTicketExpirationTime < DateTime.UtcNow)
        {
          this.AuthTicket = (string) null;
        }
        else
        {
          this.UserName = userName;
          this.MembershipProvider = membershipProviderName;
          this.RememberMeSet = persistent;
          int num = (int) SecurityManager.AuthenticateUser(membershipProviderName, userName, password, persistent);
          this.OnLoggedIn((EventArgs) null);
        }
      }
      else
      {
        this.DisplayDenyLogin();
        SecurityManager.Logout(UserLoggingReason.NeedAdminRights, SystemManager.CurrentHttpContext);
      }
    }

    protected TimeSpan AuthTicketExpirationTime => new TimeSpan(1, 0, 0);

    /// <summary>Prepares the login failed message.</summary>
    /// <param name="reason">The reason for not logged in.</param>
    protected void PrepareMessage(UserLoggingReason reason)
    {
      this.FailureText = SecurityManager.GetLoginResultText(reason);
      if (reason == UserLoggingReason.Success)
        return;
      this.FailureTextControl.Text = this.FailureText;
      this.FailureTextControl.Visible = true;
    }

    /// <summary>
    /// Checks for logout reason in the cookie and immediately delete the cookie.
    /// When you are logged out during authenticate request for some reason.
    /// This cookie holds the reason for this logout.
    /// </summary>
    private void CheckForLogoutReason()
    {
      string loggingCookie = SecurityManager.GetLoggingCookie(SystemManager.CurrentHttpContext);
      SecurityConfig securityConfig = Config.Get<SecurityConfig>();
      SecurityManager.DeleteCookie(securityConfig.LoggingCookieName, securityConfig.AuthCookiePath, securityConfig.AuthCookieDomain, securityConfig.AuthCookieRequireSsl);
      if (loggingCookie == null)
        return;
      this.logoutReason = (UserLoggingReason) Enum.Parse(typeof (UserLoggingReason), loggingCookie);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.WebControls.Login.LoggedIn" /> event after the user logs in to the Web site and has been authenticated.
    /// </summary>
    /// <param name="e">A <see cref="T:System.EventArgs" /> that contains the event data.</param>
    protected override void OnLoggedIn(EventArgs e)
    {
      base.OnLoggedIn(e);
      if (this.Page == null)
        return;
      if (this.LoginAction == SuccessfulLoginAction.Redirect)
      {
        string str1 = this.Page.Request.QueryStringGet(SecurityManager.AuthenticationReturnUrl);
        string str2 = !string.IsNullOrEmpty(str1) ? HttpUtility.UrlDecode(str1) : this.DestinationPageUrl;
        if (!ObjectFactory.Resolve<IRedirectUriValidator>().IsValid(str2))
          str2 = "~";
        this.Page.Response.Redirect(str2, true);
      }
      else
      {
        string ajaxLoginUrl = Config.Get<SecurityConfig>().Permissions["Backend"].AjaxLoginUrl;
        this.DestinationPageUrl = !ajaxLoginUrl.Contains<char>('?') ? ajaxLoginUrl + "?closeWindow=true" : ajaxLoginUrl + "&closeWindow=true";
      }
    }

    /// <summary>
    /// Creates a layout template from a specified
    /// user control (external template) or embedded resource.
    /// </summary>
    /// <returns>
    /// An instance of <see cref="T:System.Web.UI.ITemplate" /> object.
    /// </returns>
    protected virtual ITemplate CreateLayoutTemplate() => ControlUtilities.GetTemplate(new TemplateInfo()
    {
      TemplatePath = this.LayoutTemplatePath,
      TemplateName = this.LayoutTemplateName,
      ControlType = this.GetType(),
      ConfigAdditionalKey = this.MembershipProvider,
      AddChildrenAsDirectDescendants = true
    });

    protected virtual string LayoutTemplateName => (string) null;

    /// <summary>Gets the user name text box.</summary>
    /// <value>The user name text box.</value>
    public IEditableTextControl UserNameTextBox => this.FindControl("UserName") as IEditableTextControl;

    /// <summary>
    /// Gets the hidden submit button. Used to allow Enter press working for all types of buttons (even for LinkButton).
    /// </summary>
    /// <value>The hidden submit button.</value>
    public virtual IButtonControl HiddenSubmitButton => this.FindControl("hiddenSubmitButton") as IButtonControl;

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnInit(EventArgs e)
    {
      this.CheckForLogoutReason();
      IAppSettings appSettings = SystemManager.CurrentContext.AppSettings;
      if (appSettings.Multilingual && this.IsBackend() && appSettings.DefaultBackendLanguage != null)
        this.Page.UICulture = appSettings.DefaultBackendLanguage.Name;
      base.OnInit(e);
      this.Page.RegisterRequiresControlState((Control) this);
      this.Page.LoadComplete += new EventHandler(this.Page_LoadComplete);
      this.LogoutButton.Command += new CommandEventHandler(this.LogoutButton_Command);
      this.SelfLogoutButton.Command += new CommandEventHandler(this.SelfLogoutButton_Command);
      this.SelfLogoutCancelButton.Command += new CommandEventHandler(this.SelfLogoutCancelButton_Command);
    }

    /// <summary>
    /// Handles the Command event of the SelfLogoutCancelButton control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:System.Web.UI.WebControls.CommandEventArgs" /> instance containing the event data.</param>
    private void SelfLogoutCancelButton_Command(object sender, CommandEventArgs e) => this.DisplayLoginPanel();

    /// <summary>
    /// Handles the Command event of the SelfLogoutButton control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:System.Web.UI.WebControls.CommandEventArgs" /> instance containing the event data.</param>
    private void SelfLogoutButton_Command(object sender, CommandEventArgs e) => this.LogOutUserLogInMe();

    /// <summary>Handles the LoadComplete event of the Page control.</summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
    private void Page_LoadComplete(object sender, EventArgs e)
    {
      string input = this.Page.Request.QueryStringGet("provider");
      if (this.Page == null || this.Page.IsPostBack || string.IsNullOrEmpty(input))
        return;
      this.MembershipProvider = AntiXssEncoder.UrlEncode(Regex.Replace(input, "[^\\w\\d_-]", ""));
    }

    /// <summary>
    /// Saves any server control state changes that have occurred since the time the page was posted back to the server.
    /// </summary>
    /// <returns>
    /// Returns the server control's current state. If there is no state associated with the control, this method returns null.
    /// </returns>
    protected override object SaveControlState() => (object) this.MembershipProvider;

    /// <summary>
    /// Restores control-state information from a previous page request that was saved by the <see cref="M:System.Web.UI.Control.SaveControlState" /> method.
    /// </summary>
    /// <param name="savedState">An <see cref="T:System.Object" /> that represents the control state to be restored.</param>
    protected override void LoadControlState(object savedState)
    {
      this.MembershipProvider = (string) savedState;
      this.InitializeProvidersList();
    }

    /// <summary>
    /// Implements the base <see cref="M:System.Web.UI.Control.OnPreRender(System.EventArgs)" /> method.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> that contains event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      if (this.Page != null)
      {
        IEditableTextControl userNameTextBox = this.UserNameTextBox;
        if (userNameTextBox != null && userNameTextBox is TextBox && !SystemManager.IsDesignMode)
          this.Page.SetFocus((Control) userNameTextBox);
        Control hiddenSubmitButton = this.HiddenSubmitButton as Control;
        if (this.Page.Form != null && hiddenSubmitButton != null)
          this.Page.Form.DefaultButton = hiddenSubmitButton.UniqueID;
        this.UpdateVisibilityOfLinks();
      }
      base.OnPreRender(e);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.WebControls.Login.LoginError" /> event when a login attempt fails.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
    protected override void OnLoginError(EventArgs e)
    {
      this.FailureText = this.invalidCastExceptionThrown ? this.FailureTextControl.Text : Res.Get<Labels>().IncorrectUsernamePassword;
      base.OnLoginError(e);
      this.FailureTextControl.Visible = true;
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.Load" /> event.
    /// </summary>
    /// <param name="e">The <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if (this.logoutReason != UserLoggingReason.Success)
        this.PrepareMessage(this.logoutReason);
      this.BindLoggedInUsersList();
      if (this.Page.IsPostBack)
        return;
      this.RememberMeSet = Config.Get<LoginConfig>().DefaultRememberMeLoginCheckBoxValue;
    }
  }
}
