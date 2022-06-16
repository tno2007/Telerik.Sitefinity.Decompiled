// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.PublicControls.LoginWidget
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Data;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Security.Sanitizers;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Web.UI.PublicControls
{
  /// <summary>This class represents an authentication widget.</summary>
  [PropertyEditorTitle(typeof (PublicControlsResources), "LoginWidgetTitle")]
  public class LoginWidget : SimpleScriptView
  {
    private string usernameLabel;
    private string passwordLabel;
    private string loginButtonLabel;
    private string incorrectLoginMessage;
    private string registerUserLabel;
    private bool showLostPasswordLink;
    private bool showRegisterUserLink;
    internal const string JsComponentPath = "Telerik.Sitefinity.Web.UI.PublicControls.Scripts.LoginWidget.js";
    internal const string QueryStringManagerScript = "Telerik.Sitefinity.Resources.Scripts.QueryStringManager.js";
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.PublicControls.LoginWidget.ascx");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.PublicControls.LoginWidget" /> class.
    /// </summary>
    public LoginWidget()
    {
      this.LayoutTemplatePath = LoginWidget.layoutTemplatePath;
      this.MembershipProvider = ManagerBase<MembershipDataProvider>.GetDefaultProviderName();
    }

    /// <summary>Gets or sets the user name label.</summary>
    /// <value>The user name label.</value>
    public string UsernameLabel
    {
      get
      {
        this.usernameLabel = this.usernameLabel ?? Res.Get<Labels>("Username");
        return this.usernameLabel;
      }
      set => this.usernameLabel = value;
    }

    /// <summary>Gets or sets the password label.</summary>
    /// <value>The password label.</value>
    public string PasswordLabel
    {
      get
      {
        this.passwordLabel = this.passwordLabel ?? Res.Get<Labels>("Password");
        return this.passwordLabel;
      }
      set => this.passwordLabel = value;
    }

    /// <summary>Gets or sets the log in button label.</summary>
    /// <value>The log in button label.</value>
    public string LoginButtonLabel
    {
      get
      {
        this.loginButtonLabel = this.loginButtonLabel ?? Res.Get<Labels>("LoginCaps");
        return this.loginButtonLabel;
      }
      set => this.loginButtonLabel = value;
    }

    /// <summary>Gets or sets the incorrect log in message.</summary>
    /// <value>The incorrect log in message.</value>
    public string IncorrectLoginMessage
    {
      get
      {
        this.incorrectLoginMessage = this.incorrectLoginMessage ?? Res.Get<Labels>().IncorrectUsernamePassword;
        return this.incorrectLoginMessage;
      }
      set => this.incorrectLoginMessage = value;
    }

    /// <summary>Gets or sets the register user link label</summary>
    /// <value>The user registration link label.</value>
    public string RegisterUserLabel
    {
      get
      {
        this.registerUserLabel = this.registerUserLabel ?? Res.Get<Labels>("Register");
        return this.registerUserLabel;
      }
      set => this.registerUserLabel = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the lost password link will be displayed.
    /// </summary>
    /// <value>
    /// The value, indicating if the lost password link will be displayed.
    /// </value>
    public bool ShowLostPasswordLink
    {
      get => this.showLostPasswordLink;
      set => this.showLostPasswordLink = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the user registration link will be displayed.
    /// </summary>
    /// <value>
    /// The value, indicating if the user registration link will be displayed.
    /// </value>
    public bool ShowRegisterUserLink
    {
      get => this.showRegisterUserLink;
      set => this.showRegisterUserLink = value;
    }

    /// <summary>
    /// Gets or sets the url of the page with change password widget
    /// </summary>
    /// <value>The url of the page with change password widget</value>
    public string ChangePasswordPageUrl { get; set; }

    /// <summary>Gets or sets the url of the user registration page.</summary>
    /// <value>The url of the user registration page.</value>
    public string RegisterUserPageUrl { get; set; }

    /// <summary>Gets or sets the membership provider.</summary>
    public string MembershipProvider { get; set; }

    /// <summary>
    /// Gets or sets the page where the client will be redirected after a successful log in.
    /// </summary>
    public string DestinationPageUrl { get; set; }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <inheritdoc />
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Gets the submit button.</summary>
    private LinkButton SubmitButton => this.Container.GetControl<LinkButton>("LoginButton", true);

    /// <summary>Gets the user name text field.</summary>
    private TextField UserNameTextField => this.Container.GetControl<TextField>("UserName", true);

    /// <summary>Gets the password text field.</summary>
    private TextField PasswordTextField => this.Container.GetControl<TextField>("Password", true);

    /// <summary>Gets the reference to the remember me check box.</summary>
    private CheckBox RememberMeCheckbox => this.Container.GetControl<CheckBox>("rememberMeCheckbox", true);

    /// <summary>Gets the error message label.</summary>
    private SitefinityLabel ErrorMessageLabel => this.Container.GetControl<SitefinityLabel>(nameof (ErrorMessageLabel), true);

    /// <summary>Gets the log in button literal.</summary>
    private Literal LoginButtonLiteral => this.Container.GetControl<Literal>(nameof (LoginButtonLiteral), true);

    /// <summary>Gets the button for lost password.</summary>
    private LinkButton LostPasswordButton => this.Container.GetControl<LinkButton>("lostPasswordBtn", true);

    /// <summary>Gets the button, that sends password recovery mail</summary>
    private Button SendRecoveryMailBtn => this.Container.GetControl<Button>("sendRecoveryMailBtn", true);

    private Button EnterSecurityAnswerBtn => this.Container.GetControl<Button>("enterSecurityAnswerBtn", true);

    /// <summary>Gets the log in panel</summary>
    private Panel LoginWidgetPanel => this.Container.GetControl<Panel>("loginWidgetPanel", true);

    /// <summary>Gets the password reset instructions sent panel</summary>
    private Panel PasswordResetSentPanel => this.Container.GetControl<Panel>("passwordResetSentPanel", true);

    /// <summary>Gets the external login panel</summary>
    private Panel ExternalLoginPanel => this.Container.GetControl<Panel>("externalLoginPanel", true);

    private ITextControl PasswordResetSentLiteral => this.Container.GetControl<ITextControl>("passwordResetSentLiteral", true);

    /// <summary>Gets the lost password panel</summary>
    private Panel LostPasswordPanel => this.Container.GetControl<Panel>("lostPasswordPanel", true);

    /// <summary>Gets the security answer panel</summary>
    private Panel EnterSecurityAnswerPanel => this.Container.GetControl<Panel>("enterSecurityAnswerPanel", true);

    /// <summary>
    /// Gets the text control that contains the security answer
    /// </summary>
    private TextBox AnswerTextField => this.Container.GetControl<TextBox>("answerTextBox", true);

    private HiddenField HEmail => this.Container.GetControl<HiddenField>("hEmail", true);

    /// <summary>Gets the security question.</summary>
    private Label SecurityQuestion => this.Container.GetControl<Label>("securityQuestion", true);

    /// <summary>Gets the text control that contains the user email.</summary>
    /// <value>The mail text.</value>
    private IEditableTextControl MailText => this.Container.GetControl<IEditableTextControl>("mailTextBox", true);

    private ITextControl LostPasswordError => this.Container.GetControl<ITextControl>("lostPasswordError", true);

    private ITextControl LostPasswordEnterAnswerError => this.Container.GetControl<ITextControl>("lostPasswordEnterAnswerError", true);

    /// <summary>Gets the register user text literal.</summary>
    /// <value>The register user text literal.</value>
    private Literal RegisterUserTextField => this.Container.GetControl<Literal>("RegisterUserText", false);

    /// <summary>Gets the register user LinkButton.</summary>
    private LinkButton RegisterUserButton => this.Container.GetControl<LinkButton>("RegisterUserBtn", false);

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (LoginWidget).FullName, this.ClientID);
      controlDescriptor.AddElementProperty("submitButton", this.SubmitButton.ClientID);
      controlDescriptor.AddComponentProperty("userNameTextField", this.UserNameTextField.ClientID);
      controlDescriptor.AddComponentProperty("passwordTextField", this.PasswordTextField.ClientID);
      controlDescriptor.AddElementProperty("errorMessageLabel", this.ErrorMessageLabel.ClientID);
      controlDescriptor.AddProperty("incorrectLoginMessage", (object) this.IncorrectLoginMessage);
      controlDescriptor.AddProperty("membershipProvider", (object) this.MembershipProvider);
      controlDescriptor.AddElementProperty("rememberMeCheckbox", this.RememberMeCheckbox.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>()
    {
      new ScriptReference("Telerik.Sitefinity.Web.UI.PublicControls.Scripts.LoginWidget.js", typeof (LoginWidget).Assembly.FullName),
      new ScriptReference("Telerik.Sitefinity.Resources.Scripts.QueryStringManager.js", "Telerik.Sitefinity.Resources")
    };

    /// <summary>
    /// Gets the required by the control, core library scripts predefined in the <see cref="T:Telerik.Sitefinity.Modules.Pages.ScriptRef" /> enum.
    /// </summary>
    /// <returns>
    /// A <see cref="T:Telerik.Sitefinity.Modules.Pages.ScriptRef" /> enum value indicating the mix of library scripts that the control requires.
    /// </returns>
    /// <example>
    /// <para>The defaults are:</para>
    /// <para>ScriptRef.MicrosoftAjax | ScriptRef.MicrosoftAjaxWebForms | ScriptRef.JQuery | ScriptRef.JQueryValidate | ScriptRef.JQueryCookie | ScriptRef.TelerikSitefinity | ScriptRef.QueryString</para>;
    /// </example>
    protected override ScriptRef GetRequiredCoreScripts() => ScriptRef.JQuery;

    /// <summary>
    /// Writes the <see cref="T:System.Web.UI.WebControls.CompositeControl" /> content to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object, for display on the client.
    /// </summary>
    /// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    protected override void Render(HtmlTextWriter writer)
    {
      if (this.ShowLostPasswordLink && string.IsNullOrEmpty(this.ChangePasswordPageUrl) && SystemManager.IsDesignMode)
        writer.WriteEncodedText(Res.Get<PublicControlsResources>().ThePasswordRecoveryFunctionalityWillNotWork);
      else
        base.Render(writer);
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container">The container of the instantiated template.</param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
      string returnUrl = currentHttpContext.Request.QueryStringGet(SecurityManager.AuthenticationReturnUrl);
      if (SystemManager.IsDesignMode && SecurityManager.AuthenticationMode == AuthenticationMode.Forms)
      {
        this.Controls.Add((Control) new LiteralControl(Res.Get<PublicControlsResources>().LoginWidgetAvailability.Arrange((object) Res.Get<PublicControlsResources>().ExternalLinkLoginWidgetAvailability)));
        this.LoginWidgetPanel.Style.Add(HtmlTextWriterStyle.Display, "none");
      }
      else
      {
        SFClaimsAuthenticationManager.ProcessRejectedUserClearQueryString(currentHttpContext, returnUrl);
        this.ErrorMessageLabel.Style["display"] = "none";
        this.UserNameTextField.Title = this.UsernameLabel;
        this.PasswordTextField.Title = this.PasswordLabel;
        this.LoginButtonLiteral.Text = this.LoginButtonLabel;
        bool browserAutocomplete = Config.Get<LoginConfig>().DisableBrowserAutocomplete;
        this.UserNameTextField.DisableBrowserAutocomplete = browserAutocomplete;
        this.PasswordTextField.DisableBrowserAutocomplete = browserAutocomplete;
        if (this.RegisterUserTextField != null)
          this.RegisterUserTextField.Text = this.RegisterUserLabel;
        if (this.RegisterUserButton != null)
        {
          if (!this.ShowRegisterUserLink || string.IsNullOrEmpty(this.RegisterUserPageUrl))
            this.RegisterUserButton.Visible = false;
          else
            this.RegisterUserButton.Click += new EventHandler(this.RegisterUserButton_Click);
        }
        if (!this.ShowLostPasswordLink || string.IsNullOrEmpty(this.ChangePasswordPageUrl))
        {
          this.LostPasswordButton.Visible = false;
        }
        else
        {
          this.LostPasswordButton.Click += new EventHandler(this.LostPasswordButton_Click);
          this.EnterSecurityAnswerBtn.Click += new EventHandler(this.EnterSecurityAnswerButton_Click);
          this.SendRecoveryMailBtn.Click += new EventHandler(this.SendRecoveryMailBtn_Click);
          this.LostPasswordError.Text = string.Empty;
        }
        this.SubmitButton.Click += new EventHandler(this.LoginButton_Click);
        List<IExternalAuthenticationProvider> list = ClaimsManager.CurrentAuthenticationModule.ExternalAuthenticationProviders.Where<IExternalAuthenticationProvider>((Func<IExternalAuthenticationProvider, bool>) (x => x.Enabled && !string.IsNullOrEmpty(x.Name))).ToList<IExternalAuthenticationProvider>();
        if (list.Count<IExternalAuthenticationProvider>() == 0)
        {
          this.ExternalLoginPanel.Visible = false;
        }
        else
        {
          this.ExternalLoginPanel.Visible = true;
          foreach (IExternalAuthenticationProvider authenticationProvider in list)
          {
            Button button = new Button();
            button.ID = "btnExtProvider" + authenticationProvider.Name;
            button.Text = authenticationProvider.Title;
            button.CssClass = authenticationProvider.LinkCssClass;
            button.UseSubmitBehavior = false;
            Button child = button;
            child.Click += new EventHandler(this.ExtProviderButton_Click);
            child.CommandArgument = authenticationProvider.Name;
            this.ExternalLoginPanel.Controls.Add((Control) child);
          }
        }
      }
    }

    internal string GetWidgetUrl(HttpContext context) => RouteHelper.ResolveUrl(context.Request.AppRelativeCurrentExecutionFilePath, UrlResolveOptions.Absolute);

    private void ExtProviderButton_Click(object sender, EventArgs e)
    {
      string commandArgument = (sender as Button).CommandArgument;
      string widgetUrl = this.GetWidgetUrl(HttpContext.Current);
      string str = RouteHelper.ResolveUrl(this.DestinationPageUrl, UrlResolveOptions.Absolute);
      string errorRedirectUrlParameter = widgetUrl;
      AuthenticationProperties properties = ChallengeProperties.ForExternalUser(commandArgument, errorRedirectUrlParameter);
      properties.RedirectUri = str;
      this.Page.Request.GetOwinContext().Authentication.Challenge(properties, ClaimsManager.CurrentAuthenticationModule.STSAuthenticationType);
    }

    private void SendRecoveryMailBtn_Click(object sender, EventArgs e)
    {
      UserManager manager = UserManager.GetManager(this.MembershipProvider);
      User userByEmail = manager.GetUserByEmail(this.MailText.Text);
      if (!new Regex("@.*?\\.").Match(this.MailText.Text).Success)
      {
        this.LostPasswordError.Text = Res.Get<ErrorMessages>().EmailAddressViolationMessage;
        this.EnterSecurityAnswerPanel.Visible = false;
        this.LoginWidgetPanel.Visible = false;
        this.LostPasswordPanel.Visible = true;
      }
      else if (userByEmail == null)
      {
        this.PasswordResetSentLiteral.Text = Res.Get<UserProfilesResources>("SuccessResetPasswordEmailSend", (object) this.MailText.Text);
        this.EnterSecurityAnswerPanel.Visible = false;
        this.PasswordResetSentPanel.Visible = true;
        this.LoginWidgetPanel.Visible = false;
        this.LostPasswordPanel.Visible = false;
      }
      else if (!UserManager.ShouldSendPasswordEmail(userByEmail, manager.Provider.GetType()))
      {
        this.EnterSecurityAnswerPanel.Visible = false;
        this.LoginWidgetPanel.Visible = false;
        this.LostPasswordPanel.Visible = true;
      }
      else
      {
        if (UserManager.GetManager(userByEmail.ProviderName).RequiresQuestionAndAnswer)
        {
          this.HEmail.Value = this.MailText.Text;
          this.SecurityQuestion.Text = ObjectFactory.Resolve<IHtmlSanitizer>().Sanitize(userByEmail.PasswordQuestion);
          this.EnterSecurityAnswerPanel.Visible = true;
          this.PasswordResetSentPanel.Visible = false;
          this.LoginWidgetPanel.Visible = false;
          this.LostPasswordPanel.Visible = false;
        }
        else
        {
          this.PasswordResetSentLiteral.Text = Res.Get<UserProfilesResources>("SuccessResetPasswordEmailSend", (object) this.MailText.Text);
          this.EnterSecurityAnswerPanel.Visible = false;
          this.PasswordResetSentPanel.Visible = true;
          this.LoginWidgetPanel.Visible = false;
          this.LostPasswordPanel.Visible = false;
        }
        this.SendPasswordRecoveryEmail(userByEmail);
      }
    }

    private void EnterSecurityAnswerButton_Click(object sender, EventArgs e)
    {
      User userByEmail = UserManager.GetManager(this.MembershipProvider).GetUserByEmail(this.HEmail.Value);
      if (userByEmail == null)
        this.LostPasswordEnterAnswerError.Text = Res.Get<ErrorMessages>().EmailNotFound;
      this.CheckAnswerAndSendRecoveryEmail(userByEmail);
      this.PasswordResetSentLiteral.Text = Res.Get<UserProfilesResources>("SuccessResetPasswordEmailSend", (object) this.HEmail.Value);
      this.SecurityQuestion.Text = ObjectFactory.Resolve<IHtmlSanitizer>().Sanitize(userByEmail.PasswordQuestion);
      this.EnterSecurityAnswerPanel.Visible = false;
      this.PasswordResetSentPanel.Visible = true;
      this.LoginWidgetPanel.Visible = false;
      this.LostPasswordPanel.Visible = false;
    }

    private void LostPasswordButton_Click(object sender, EventArgs e)
    {
      this.LoginWidgetPanel.Visible = false;
      this.EnterSecurityAnswerPanel.Visible = false;
      this.LostPasswordPanel.Visible = true;
    }

    private void RegisterUserButton_Click(object sender, EventArgs e)
    {
      string str = this.Page.Request.QueryStringGet(SecurityManager.AuthenticationReturnUrl);
      if (!str.IsNullOrEmpty())
        this.Page.Response.Redirect(string.Format("{0}?{1}={2}", (object) this.RegisterUserPageUrl, (object) SecurityManager.AuthenticationReturnUrl, (object) HttpUtility.UrlEncode(str)));
      else
        this.Page.Response.Redirect(this.RegisterUserPageUrl);
    }

    private void LoginButton_Click(object sender, EventArgs e)
    {
      string username = (string) this.UserNameTextField.Value;
      string str1 = (string) this.PasswordTextField.Value;
      bool flag = this.RememberMeCheckbox.Checked;
      string widgetUrl = this.GetWidgetUrl(HttpContext.Current);
      string membershipProvider1 = this.MembershipProvider;
      string str2 = this.Page.Request.QueryStringGet(SecurityManager.AuthenticationReturnUrl);
      string str3 = (!str2.IsNullOrEmpty() ? str2 : RouteHelper.ResolveUrl(this.DestinationPageUrl, UrlResolveOptions.Absolute)) ?? new Uri(widgetUrl).GetLeftPart(UriPartial.Path);
      string password = str1;
      string membershipProvider2 = membershipProvider1;
      int num = flag ? 1 : 0;
      string errorRedirectUrlParameter = widgetUrl;
      AuthenticationProperties properties = ChallengeProperties.ForLocalUser(username, password, membershipProvider2, num != 0, errorRedirectUrlParameter);
      properties.RedirectUri = str3;
      this.Page.Request.GetOwinContext().Authentication.Challenge(properties, ClaimsManager.CurrentAuthenticationModule.STSAuthenticationType);
    }

    private void SendPasswordRecoveryEmail(User user)
    {
      UserManager manager = UserManager.GetManager(user.ProviderName);
      if (!manager.RequiresQuestionAndAnswer)
      {
        UserManager.SendRecoveryPasswordMail(manager, this.MailText.Text, this.ChangePasswordPageUrl);
      }
      else
      {
        this.EnterSecurityAnswerPanel.Visible = true;
        this.LostPasswordPanel.Visible = false;
      }
    }

    private void CheckAnswerAndSendRecoveryEmail(User user)
    {
      UserManager manager = UserManager.GetManager(user.ProviderName);
      if (!manager.RequiresQuestionAndAnswer)
        return;
      try
      {
        manager.ResetPassword(user.Id, this.AnswerTextField.Text);
        UserManager.SendRecoveryPasswordMail(manager, this.HEmail.Value, this.ChangePasswordPageUrl);
      }
      catch (Exception ex)
      {
        this.LostPasswordEnterAnswerError.Text = string.Format("{0} or {1}", (object) Res.Get<ErrorMessages>().WrongPasswordAnswer, (object) Res.Get<ErrorMessages>().CannotSendEmails);
      }
    }
  }
}
