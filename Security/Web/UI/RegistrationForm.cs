// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.RegistrationForm
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Modules.UserProfiles.Configuration;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Events;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Security.Web.UI.Designers;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Events;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Mail;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.Contracts;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Security.Web.UI
{
  /// <summary>A control used for user registrations.</summary>
  [ControlDesigner(typeof (RegistrationFormDesigner))]
  [PropertyEditorTitle(typeof (UserProfilesResources), "RegistrationWidgetTitle")]
  [ControlTemplateInfo("UserProfilesResources", "UserList", "Users")]
  public class RegistrationForm : SimpleView
  {
    internal const string layoutTemplateName = "Telerik.Sitefinity.Resources.Templates.Frontend.Security.RegistrationForm.ascx";
    internal const string layoutTemplateNameBasic = "Telerik.Sitefinity.Resources.Templates.Frontend.Security.RegistrationFormBasic.ascx";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Frontend.Security.RegistrationForm.ascx");
    public static readonly string layoutTemplatePathBasic = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Frontend.Security.RegistrationFormBasic.ascx");
    internal const string successEmailTemplateName = "Telerik.Sitefinity.Resources.Templates.Frontend.Security.RegistrationSuccessEmail.ascx";
    internal const string confirmationEmailTemplateName = "Telerik.Sitefinity.Resources.Templates.Frontend.Security.RegistrationConfirmationEmail.ascx";
    public static readonly string successEmailTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Frontend.Security.RegistrationSuccessEmail.ascx");
    public static readonly string confirmationEmailTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Frontend.Security.RegistrationConfirmationEmail.ascx");
    protected string smptSettingsUrl = "~/Sitefinity/Administration/Settings/Advanced/System/";
    private string invalidPasswordErrorMessage;
    private string invalidAnswerErrorMessage;
    private string invalidEmailErrorMessage;
    private string invalidQuestionErrorMessage;
    private string duplicateEmailErrorMessage;
    private string duplicateUserNameErrorMessage;
    private string unknownErrorMessage;
    private string confirmationPageUrl;
    private string confirmationText;
    private Collection<ItemInfoDefinition> roles;
    private Dictionary<string, RoleManager> roleManagersToSubmit;
    private Dictionary<string, UserProfile> userProfilesToUpdate;
    private UserProfileManager userProfileManager;
    private string confirmationEmailSubject = Res.Get<UserProfilesResources>().ConfirmationEmailDefaultSubject;
    private string successEmailSubject = Res.Get<UserProfilesResources>().SuccessEmailDefaultSubject;
    private const string registrationEventOrigin = "Registration form";

    public RegistrationForm() => this.LayoutTemplatePath = RegistrationForm.layoutTemplatePath;

    /// <summary>Gets the name of the layout template.</summary>
    /// <value>The name of the layout template.</value>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the user provider name that will be used for the user creation.
    /// </summary>
    /// <value>The provider name.</value>
    public virtual string MembershipProvider { get; set; }

    /// <summary>
    /// Gets the provider sitting for required question and answer
    /// </summary>
    public bool QuestionAndAnswerRequired => !this.DesignMode && UserManager.GetManager(this.MembershipProvider).RequiresQuestionAndAnswer;

    /// <summary>
    /// Gets or sets an array of the roles as ItemInfos that will be assigned to the newly created user.
    /// </summary>
    [TypeConverter(typeof (CollectionJsonTypeConverter<ItemInfoDefinition>))]
    public virtual Collection<ItemInfoDefinition> Roles
    {
      get
      {
        if (this.roles == null)
          this.roles = new Collection<ItemInfoDefinition>();
        return this.roles;
      }
      set => this.roles = value;
    }

    /// <summary>
    /// Gets or sets the action that will be executed on successful user submission.
    /// </summary>
    /// <value>The success action.</value>
    public virtual SubmittingSuccessAction RegistratingUserSuccessAction { get; set; }

    /// <summary>
    /// Gets or sets whether to send email for user activation or activate it immediately.
    /// </summary>
    /// <value>The confirm registration.</value>
    public virtual bool SendRegistrationEmail { get; set; }

    /// <summary>Gets or sets whether to redirect to a predefined Url</summary>
    /// <value>The default redirect Url</value>
    public virtual string DefaultReturnUrl { get; set; }

    /// <summary>
    /// Gets or sets the confirmation text that would be displayed on successful registration.
    /// </summary>
    /// <value>The confirmation text.</value>
    public virtual string ConfirmationText
    {
      get => this.confirmationText.IsNullOrEmpty() ? Res.Get<UserProfilesResources>().SuccessThanksForFillingOutOurForm : this.confirmationText;
      set => this.confirmationText = value;
    }

    /// <summary>Gets or sets the subject of the confirmation email.</summary>
    /// <value>The subject of the email.</value>
    public virtual string ConfirmationEmailSubject
    {
      get => this.confirmationEmailSubject;
      set => this.confirmationEmailSubject = value;
    }

    /// <summary>
    /// Gets or sets the template id of the email template used for the confirmation email.
    /// </summary>
    public virtual Guid? ConfirmationEmailTemplateId { get; set; }

    /// <summary>
    /// Gets or sets the id of the page that will be opened on successful profile creation.
    /// </summary>
    public virtual Guid? RedirectOnSubmitPageId { get; set; }

    /// <summary>
    /// Gets the title of the page that will be used to confirm the registration.
    /// </summary>
    /// <value>The title of the page.</value>
    public virtual string RedirectOnSubmitPageTitle
    {
      get
      {
        string onSubmitPageTitle = string.Empty;
        if (this.RedirectOnSubmitPageId.HasValue && this.RedirectOnSubmitPageId.Value != Guid.Empty)
        {
          SiteMapNode siteMapNodeFromKey = SitefinitySiteMap.GetCurrentProvider().FindSiteMapNodeFromKey(this.RedirectOnSubmitPageId.Value.ToString());
          if (siteMapNodeFromKey != null)
            onSubmitPageTitle = siteMapNodeFromKey.Title;
        }
        return onSubmitPageTitle;
      }
    }

    /// <summary>
    /// Gets or sets the id of the page that will be used to confirm the registration.
    /// </summary>
    /// <value>The confirmation page id.</value>
    public virtual Guid? ConfirmationPageId { get; set; }

    /// <summary>
    /// Gets the title of the page that will be used to confirm the registration.
    /// </summary>
    /// <value>The title of the page.</value>
    public virtual string ConfirmationPageTitle
    {
      get
      {
        string confirmationPageTitle = string.Empty;
        if (this.ConfirmationPageExists())
        {
          SiteMapNode siteMapNodeFromKey = SitefinitySiteMap.GetCurrentProvider().FindSiteMapNodeFromKey(this.ConfirmationPageId.Value.ToString());
          if (siteMapNodeFromKey != null)
            confirmationPageTitle = siteMapNodeFromKey.Title;
        }
        return confirmationPageTitle;
      }
    }

    /// <summary>
    /// Gets or sets the whether to send email message on successful registration confirmation.
    /// </summary>
    public virtual bool SendEmailOnSuccess { get; set; }

    /// <summary>Gets or sets the subject of the success email.</summary>
    /// <value>The subject of the email.</value>
    public virtual string SuccessEmailSubject
    {
      get => this.successEmailSubject;
      set => this.successEmailSubject = value;
    }

    /// <summary>
    /// Gets or sets the template id of the email template used for the success email.
    /// </summary>
    public virtual Guid? SuccessEmailTemplateId { get; set; }

    /// <summary>
    /// Gets or sets the name of the email sender that will be used to send confirmation and successful registration emails.
    /// </summary>
    /// <value>The email sender.</value>
    public virtual string EmailSenderName { get; set; }

    /// <summary>
    /// Gets or sets the sender email that will be used to send successful registration emails.
    /// </summary>
    /// <value>The sender email.</value>
    public virtual string SuccessfulRegistrationSenderEmail { get; set; }

    /// <summary>
    /// Gets or sets the sender email that will be used to send confirmation registration emails.
    /// </summary>
    /// <value>The sender email.</value>
    public virtual string ConfirmRegistrationSenderEmail { get; set; }

    /// <summary>
    /// Gets or sets the sender namr that will be used to send successful registration emails.
    /// </summary>
    /// <value>The sender name.</value>
    public virtual string SuccessfulRegistrationSenderName { get; set; }

    /// <summary>
    /// Gets or sets the sender name that will be used to send confirmation registration emails.
    /// </summary>
    /// <value>The sender name.</value>
    public virtual string ConfirmRegistrationSenderName { get; set; }

    /// <summary>The text to be shown when the password is invalid.</summary>
    /// <value>The text to be shown when the password is invalid.</value>
    public virtual string InvalidPasswordErrorMessage
    {
      get => this.invalidPasswordErrorMessage == null ? Res.Get<ErrorMessages>().CreateUserWizardDefaultInvalidPasswordErrorMessage : this.invalidPasswordErrorMessage;
      set => this.invalidPasswordErrorMessage = value;
    }

    /// <summary>Text to be shown when the security answer is invalid.</summary>
    /// <value>Text to be shown when the security answer is invalid.</value>
    public virtual string InvalidAnswerErrorMessage
    {
      get => this.invalidAnswerErrorMessage == null ? Res.Get<ErrorMessages>().CreateUserWizardDefaultAnswerRequiredErrorMessage : this.invalidAnswerErrorMessage;
      set => this.invalidAnswerErrorMessage = value;
    }

    /// <summary>The text to be shown when the e-mail is invalid.</summary>
    /// <value>The text to be shown when the e-mail is invalid.</value>
    public virtual string InvalidEmailErrorMessage
    {
      get => this.invalidEmailErrorMessage == null ? Res.Get<ErrorMessages>().CreateUserWizardDefaultInvalidEmailErrorMessage : this.invalidEmailErrorMessage;
      set => this.invalidEmailErrorMessage = value;
    }

    /// <summary>
    /// Text to be shown when the security question is invalid.
    /// </summary>
    /// <value>Text to be shown when the security question is invalid.</value>
    public virtual string InvalidQuestionErrorMessage
    {
      get => this.invalidQuestionErrorMessage == null ? Res.Get<ErrorMessages>().CreateUserWizardDefaultInvalidQuestionErrorMessage : this.invalidQuestionErrorMessage;
      set => this.invalidQuestionErrorMessage = value;
    }

    /// <summary>
    /// Text to be shown when a duplicate e-mail error is returned from create user.
    /// </summary>
    /// <value>Text to be shown when a duplicate e-mail error is returned from create user.</value>
    public virtual string DuplicateEmailErrorMessage
    {
      get => this.duplicateEmailErrorMessage == null ? Res.Get<ErrorMessages>().CreateUserWizardDefaultDuplicateEmailErrorMessage : this.duplicateEmailErrorMessage;
      set => this.duplicateEmailErrorMessage = value;
    }

    /// <summary>
    /// Text to be shown when a duplicate username error is returned from create user.
    /// </summary>
    /// <value>Text to be shown when a duplicate username error is returned from create user.</value>
    public virtual string DuplicateUserNameErrorMessage
    {
      get => this.duplicateUserNameErrorMessage == null ? Res.Get<ErrorMessages>().CreateUserWizardDefaultDuplicateUserNameErrorMessage : this.duplicateUserNameErrorMessage;
      set => this.duplicateUserNameErrorMessage = value;
    }

    /// <summary>The text that is displayed for unknown errors.</summary>
    /// <value>The text that is displayed for unknown errors.</value>
    public virtual string UnknownErrorMessage
    {
      get => this.unknownErrorMessage == null ? Res.Get<ErrorMessages>().CreateUserWizardDefaultUnknownErrorMessage : this.unknownErrorMessage;
      set => this.unknownErrorMessage = value;
    }

    /// <summary>
    /// Returns the full name of the ItemInfoDefinition type used to transfer the Roles property.
    /// </summary>
    public virtual string RolesItemInfoName => typeof (ItemInfoDefinition).FullName;

    /// <summary>
    /// Returns the full name of the RegistrationFormTypeName type used when editing its templates.
    /// </summary>
    public virtual string RegistrationFormTypeName => typeof (RegistrationForm).FullName;

    /// <summary>Gets the role managers to submit.</summary>
    /// <value>The role managers to submit.</value>
    protected virtual Dictionary<string, RoleManager> RoleManagersToSubmit
    {
      get
      {
        if (this.roleManagersToSubmit == null)
          this.roleManagersToSubmit = new Dictionary<string, RoleManager>();
        return this.roleManagersToSubmit;
      }
    }

    /// <summary>Gets the user profiles to update.</summary>
    /// <value>The user profiles to update.</value>
    protected virtual Dictionary<string, UserProfile> UserProfilesToUpdate
    {
      get
      {
        if (this.userProfilesToUpdate == null)
          this.userProfilesToUpdate = new Dictionary<string, UserProfile>();
        return this.userProfilesToUpdate;
      }
    }

    /// <summary>Gets or sets the user profile provider.</summary>
    /// <value>The user profile provider.</value>
    public virtual string UserProfileProvider { get; set; }

    /// <summary>Gets the user profile manager.</summary>
    /// <value>The user profile manager.</value>
    protected virtual UserProfileManager UserProfileManager
    {
      get
      {
        if (this.userProfileManager == null)
          this.userProfileManager = UserProfileManager.GetManager();
        return this.userProfileManager;
      }
    }

    /// <summary>Gets the register button.</summary>
    /// <value>The register button.</value>
    protected virtual IButtonControl RegisterButton => this.Container.GetControl<IButtonControl>("registerButton", true);

    /// <summary>Gets the field controls.</summary>
    /// <value>The field controls.</value>
    protected virtual Dictionary<string, Control> FieldControls => this.Container.GetControls<Telerik.Sitefinity.Web.UI.Fields.Contracts.IFieldControl>();

    /// <summary>Gets the password field.</summary>
    /// <value>The password field.</value>
    protected virtual TextField PasswordField => this.Container.GetControl<TextField>("password", true);

    /// <summary>Gets the re-type password field.</summary>
    /// <value>The re-type password field.</value>
    protected virtual TextField ReTypePasswordField => this.Container.GetControl<TextField>("reTypePassword", true);

    /// <summary>Gets the email field.</summary>
    /// <value>The email field.</value>
    protected virtual TextField EmailField => this.Container.GetControl<TextField>("email", true);

    /// <summary>Gets or sets the security question field.</summary>
    /// <value>The security question field.</value>
    protected virtual TextField SecurityQuestionField => this.Container.GetControl<TextField>("securityQuestion", false);

    /// <summary>Gets or sets the security question field.</summary>
    /// <value>The security question field.</value>
    protected virtual TextField SecurityAnswerField => this.Container.GetControl<TextField>("securityAnswer", false);

    /// <summary>Gets the errors panel.</summary>
    /// <value>The errors panel.</value>
    protected virtual Panel ErrorsPanel => this.Container.GetControl<Panel>("errorsPanel", false);

    /// <summary>Gets the form container.</summary>
    /// <value>The form container.</value>
    protected virtual Control FormContainer => this.Container.GetControl<Control>("formContainer", false);

    /// <summary>Gets the SMTP settings error wrapper.</summary>
    /// <value>The SMTP settings error wrapper.</value>
    protected virtual HtmlGenericControl SmtpSettingsErrorWrapper => this.Container.GetControl<HtmlGenericControl>("smtpSettingsErrorWrapper", false);

    /// <summary>
    /// Gets the error panel showing any configuration errors.
    /// </summary>
    /// <value>The configuration errors panel.</value>
    protected virtual Panel ConfigurationErrorsPanel => this.Container.GetControl<Panel>("configurationErrorsPanel", false);

    /// <summary>Gets the SMTP configuration error label.</summary>
    /// <value>The SMTP configuration error.</value>
    protected virtual Label SmtpConfigurationError => this.Container.GetControl<Label>("smtpConfigurationError", false);

    /// <summary>Gets the success message label.</summary>
    /// <value>The success message label.</value>
    protected Label SuccessMessageLabel => this.Container.GetControl<Label>("successMessageLabel", false);

    /// <summary>Occurs when the 'register' button is clicked.</summary>
    public event EventHandler RegisterButtonClick;

    /// <summary>Occurs when creating a user.</summary>
    public event LoginCancelEventHandler CreatingUser;

    /// <summary>Occurs when a user was created.</summary>
    public event EventHandler UserCreated;

    /// <summary>Raised when user creation error occurred.</summary>
    public event CreateUserErrorEventHandler UserCreationError;

    /// <summary>Occurs when creating user profile(s).</summary>
    public event RegistrationForm.UserOperationInvokingEventHandler CreatingUserProfile;

    /// <summary>Occurs when user profile(s) were created.</summary>
    public event RegistrationForm.UserOperationInvokedEventHandler UserProfileCreated;

    /// <summary>Occurs when assigning roles to a user.</summary>
    public event RegistrationForm.UserOperationInvokingEventHandler AssigningRolesToUser;

    /// <summary>Occurs when roles were assigned to a user.</summary>
    public event RegistrationForm.UserOperationInvokedEventHandler RolesAssignedToUser;

    /// <summary>Occurs when sending a confirmation email.</summary>
    public event MailMessageEventHandler SendingConfirmationMail;

    /// <summary>Occurs when sending a registration email.</summary>
    public event MailMessageEventHandler SendingSuccessfulRegistrationMail;

    /// <summary>Initializes the controls.</summary>
    /// <param name="container">The container.</param>
    protected override void InitializeControls(GenericContainer container)
    {
      this.ValidateSettings();
      this.RenderRequiredQuestionAndAnswer();
      this.ConfigureSaveButton();
      Type baseType = typeof (UserProfile);
      IEnumerable<IFieldDefinition> source = Telerik.Sitefinity.Configuration.Config.Get<ContentViewConfig>().ContentViewControls.Values.Where<ContentViewControlElement>((Func<ContentViewControlElement, bool>) (c => baseType.IsAssignableFrom(c.ContentType) && c.ContentType != baseType)).SelectMany<ContentViewControlElement, IContentViewDefinition>((Func<ContentViewControlElement, IEnumerable<IContentViewDefinition>>) (cvc => (IEnumerable<IContentViewDefinition>) cvc.Views)).Where<IContentViewDefinition>((Func<IContentViewDefinition, bool>) (v => v.ViewName == UserProfilesHelper.GetContentViewName(ProfileTypeViewKind.FrontendCreate))).OfType<IDetailFormViewDefinition>().SelectMany<IDetailFormViewDefinition, IContentViewSectionDefinition>((Func<IDetailFormViewDefinition, IEnumerable<IContentViewSectionDefinition>>) (v => v.Sections)).SelectMany<IContentViewSectionDefinition, IFieldDefinition>((Func<IContentViewSectionDefinition, IEnumerable<IFieldDefinition>>) (s => s.Fields));
      foreach (Control control in this.FieldControls.Values)
      {
        FieldControl fieldControl = control as FieldControl;
        if (fieldControl != null && source.Where<IFieldDefinition>((Func<IFieldDefinition, bool>) (fd => fd.FieldName == fieldControl.DataFieldName)).FirstOrDefault<IFieldDefinition>() is IFieldControlDefinition controlDefinition)
          fieldControl.Value = controlDefinition.Value;
      }
    }

    protected void RenderRequiredQuestionAndAnswer()
    {
      if (this.SecurityQuestionField == null || this.SecurityAnswerField == null)
        return;
      if (this.QuestionAndAnswerRequired)
      {
        this.SecurityQuestionField.Visible = true;
        this.SecurityAnswerField.Visible = true;
      }
      else
      {
        this.SecurityQuestionField.Visible = false;
        this.SecurityAnswerField.Visible = false;
      }
    }

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

    /// <summary>Gets the tag key.</summary>
    /// <value>The tag key.</value>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Handles the Click event of the RegisterButton control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
    protected virtual void RegisterButton_Click(object sender, EventArgs e)
    {
      if (!this.ValidateInput())
        return;
      UserManager manager = UserManager.GetManager(this.MembershipProvider);
      bool suppressSecurityChecks = manager.Provider.SuppressSecurityChecks;
      try
      {
        if (this.OnCreatingUser().Cancel)
          return;
        manager.Provider.SuppressSecurityChecks = true;
        User user;
        MembershipCreateStatus status;
        if (this.TryCreateUser(manager, out user, out status))
        {
          this.UpdateUserInformation(user);
          manager.SaveChanges();
          this.OnUserCreated();
          this.CreateUserProfiles(user);
          this.AssignRolesToUser(user);
          this.ConfirmRegistration(manager, user);
          this.ExecuteUserProfileSuccessfullUpdateActions();
        }
        else
        {
          this.OnUserCreationError(status);
          this.ShowErrorMessage(status, manager);
        }
      }
      catch (Exception ex)
      {
        throw;
      }
      finally
      {
        manager.Provider.SuppressSecurityChecks = suppressSecurityChecks;
      }
    }

    /// <summary>Validates the user input.</summary>
    /// <returns></returns>
    protected virtual bool ValidateInput() => this.EmailField.IsValid() && this.PasswordField.IsValid() && this.ReTypePasswordField.IsValid();

    /// <summary>Attaches the click handlers to the RegisterButton.</summary>
    protected virtual void ConfigureSaveButton() => this.RegisterButton.Click += new EventHandler(this.RegisterButton_Click);

    /// <summary>Validates control settings and returns the result.</summary>
    /// <returns></returns>
    protected virtual bool ValidateSettings()
    {
      bool flag = true;
      if (this.SendRegistrationEmail || this.SendEmailOnSuccess)
      {
        int num1 = this.ValidateSmtpSettings() ? 1 : 0;
        flag = (num1 & (flag ? 1 : 0)) != 0;
        if (num1 == 0 && this.IsDesignMode())
          this.ShowSmtpConfigurationError();
        if (this.SendRegistrationEmail)
        {
          int num2 = this.ConfirmationPageExists() ? 1 : 0;
          flag = (num2 & (flag ? 1 : 0)) != 0;
          if (num2 == 0 && this.IsDesignMode())
          {
            this.HideForm();
            this.AddErrorMessage(Res.Get<ErrorMessages>().NoConfirmationPageIsSet, this.ConfigurationErrorsPanel);
          }
        }
      }
      return flag;
    }

    /// <summary>
    /// Shows an error indicating that the SMPT settings have not been set.
    /// </summary>
    protected virtual void ShowSmtpConfigurationError()
    {
      this.HideForm();
      if (this.ConfigurationErrorsPanel == null || this.SmtpSettingsErrorWrapper == null)
        return;
      this.ConfigurationErrorsPanel.Visible = true;
      this.SmtpSettingsErrorWrapper.Visible = true;
      string str = RouteHelper.ResolveUrl(this.smptSettingsUrl, UrlResolveOptions.Rooted | UrlResolveOptions.RemoveTrailingSlash);
      this.SmtpConfigurationError.Text = Res.Get<ErrorMessages>().SmtpSettingsAreNotSet.Arrange((object) str);
    }

    /// <summary>Validates the SMTP settings and returns the result.</summary>
    /// <returns></returns>
    protected virtual bool ValidateSmtpSettings()
    {
      string senderProfile = Telerik.Sitefinity.Configuration.Config.Get<SecurityConfig>().Notifications.SenderProfile;
      return EmailSender.Get().VerifySenderProfile(senderProfile);
    }

    /// <summary>
    /// Returns true if the configured confirmation page exists, otherwise false.
    /// </summary>
    /// <returns></returns>
    protected virtual bool ConfirmationPageExists()
    {
      if (!this.ConfirmationPageId.HasValue || !(this.ConfirmationPageId.Value != Guid.Empty))
        return false;
      this.confirmationPageUrl = this.GetPageUrl(this.ConfirmationPageId.Value, UrlResolveOptions.Absolute);
      return this.confirmationPageUrl != null;
    }

    /// <summary>
    /// Attempt to create user. Returns true if the creation was successful, otherwise false.
    /// </summary>
    /// <param name="manager">The manager.</param>
    /// <param name="user">The user.</param>
    /// <param name="status">The status that will be set depending on the creation outcome.</param>
    protected virtual bool TryCreateUser(
      UserManager manager,
      out User user,
      out MembershipCreateStatus status)
    {
      string passwordQuestion = this.SecurityQuestionField != null ? (string) this.SecurityQuestionField.Value : (string) null;
      string passwordAnswer = this.SecurityAnswerField != null ? (string) this.SecurityAnswerField.Value : (string) null;
      user = manager.CreateUser((string) this.EmailField.Value, (string) this.PasswordField.Value, passwordQuestion, passwordAnswer, !this.SendRegistrationEmail, (object) null, out status);
      return status == MembershipCreateStatus.Success;
    }

    /// <summary>
    /// Updates the supplied user instance with the values of the field controls.
    /// </summary>
    /// <param name="user">The user.</param>
    protected virtual void UpdateUserInformation(User user)
    {
      foreach (KeyValuePair<string, Control> fieldControl1 in this.FieldControls)
      {
        Telerik.Sitefinity.Web.UI.Fields.Contracts.IFieldControl fieldControl2 = (Telerik.Sitefinity.Web.UI.Fields.Contracts.IFieldControl) fieldControl1.Value;
        if (!fieldControl2.DataFieldName.IsNullOrEmpty() && (fieldControl2.DataItemType.IsNullOrEmpty() || fieldControl2.DataItemType == typeof (User).FullName))
        {
          PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties((object) user).Find(fieldControl2.DataFieldName, true);
          if (propertyDescriptor != null)
            propertyDescriptor.SetValue((object) user, fieldControl2.Value);
          else
            Telerik.Sitefinity.Model.Localization.ThrowHelper.ThrowNotSupported("Type does not contain property with that name", (object) "SetValue", (object) user, (object) fieldControl2.DataFieldName);
        }
      }
    }

    /// <summary>
    /// Assigns the specified roles to the newly created user.
    /// </summary>
    /// <param name="user">The user.</param>
    protected virtual void AssignRolesToUser(User user)
    {
      if (this.OnAssigningUserRoles(user).Cancel || this.Roles == null)
        return;
      foreach (ItemInfoDefinition role1 in this.Roles)
      {
        RoleManager roleManager = this.GetRoleManager(role1.ProviderName);
        Role role2 = roleManager.GetRole(role1.ItemId);
        SecurityManager.AssignRoleToUser(user, roleManager, role2);
      }
      foreach (KeyValuePair<string, RoleManager> keyValuePair in this.RoleManagersToSubmit)
        keyValuePair.Value.SaveChanges();
      this.OnUserRolesAssigned(user);
    }

    /// <summary>Creates the user profiles.</summary>
    /// <param name="user">The user.</param>
    protected virtual void CreateUserProfiles(User user)
    {
      if (this.OnCreatingUserProfile(user).Cancel)
        return;
      foreach (KeyValuePair<string, Control> fieldControl1 in this.FieldControls)
      {
        Telerik.Sitefinity.Web.UI.Fields.Contracts.IFieldControl fieldControl2 = (Telerik.Sitefinity.Web.UI.Fields.Contracts.IFieldControl) fieldControl1.Value;
        if (!fieldControl2.DataFieldName.IsNullOrEmpty() && !fieldControl2.DataItemType.IsNullOrEmpty() && fieldControl2.DataItemType != typeof (User).FullName)
          this.GetUserProfile(user, fieldControl2.DataItemType).SetValue(fieldControl2.DataFieldName, fieldControl2.Value);
      }
      foreach (KeyValuePair<string, UserProfile> keyValuePair in this.UserProfilesToUpdate)
        this.UserProfileManager.RecompileItemUrls<UserProfile>(keyValuePair.Value);
      this.UserProfileManager.SaveChanges();
      this.OnUserProfileCreated(user);
    }

    /// <summary>Gets the user profile.</summary>
    /// <param name="user">The user.</param>
    /// <param name="profileTypeFullName">Full name of the profile type.</param>
    /// <returns></returns>
    protected virtual UserProfile GetUserProfile(User user, string profileTypeFullName)
    {
      if (this.UserProfilesToUpdate.ContainsKey(profileTypeFullName))
        return this.UserProfilesToUpdate[profileTypeFullName];
      UserProfile profile = this.UserProfileManager.CreateProfile(user, profileTypeFullName);
      this.UserProfilesToUpdate[profileTypeFullName] = profile;
      return profile;
    }

    /// <summary>Gets the manager.</summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns></returns>
    protected virtual RoleManager GetRoleManager(string providerName)
    {
      if (this.RoleManagersToSubmit.ContainsKey(providerName))
        return this.RoleManagersToSubmit[providerName];
      RoleManager manager = RoleManager.GetManager(providerName);
      this.RoleManagersToSubmit.Add(providerName, manager);
      return manager;
    }

    /// <summary>Executes any user confirmations steps.</summary>
    /// <param name="user">The user.</param>
    protected virtual void ConfirmRegistration(UserManager userManager, User user)
    {
      if (this.SendRegistrationEmail)
        this.SendRegistrationConfirmationEmail(user, userManager);
      else if (this.SendEmailOnSuccess)
        this.SendSuccessfulRegistrationEmail(userManager, user);
      this.RaiseRegistrationEvent(user);
    }

    /// <summary>Sends the registration confirmation email.</summary>
    /// <param name="user">The user.</param>
    /// <param name="userManager">The user manager.</param>
    protected virtual void SendRegistrationConfirmationEmail(User user, UserManager userManager)
    {
      string confirmationPageUrl = this.GetConfirmationPageUrl(user);
      MailMessage confirmationEmail = UserRegistrationEmailGenerator.GenerateRegistrationConfirmationEmail(userManager, user, this.MembershipProvider, this.ConfirmationEmailTemplateId, confirmationPageUrl, this.ConfirmationEmailSubject, this.ConfirmRegistrationSenderEmail, this.ConfirmRegistrationSenderName);
      if (this.OnSendingConfirmationMail(confirmationEmail).Cancel)
        return;
      EmailSender emailSender = EmailSender.Get(this.EmailSenderName);
      emailSender.SenderProfileName = Telerik.Sitefinity.Configuration.Config.Get<SecurityConfig>().Notifications.SenderProfile;
      emailSender.SendAsync(confirmationEmail, (object) null);
    }

    /// <summary>Sends the successful registration email.</summary>
    /// <param name="userManager">The user manager.</param>
    /// <param name="user">The user.</param>
    protected virtual void SendSuccessfulRegistrationEmail(UserManager userManager, User user)
    {
      MailMessage registrationEmail = UserRegistrationEmailGenerator.GenerateSuccessfulRegistrationEmail(userManager, user, this.SuccessEmailTemplateId, this.SuccessEmailSubject, this.SuccessfulRegistrationSenderEmail, this.SuccessfulRegistrationSenderName);
      if (this.OnSendingSuccessfulRegistrationMail(registrationEmail).Cancel)
        return;
      EmailSender emailSender = EmailSender.Get(this.EmailSenderName);
      emailSender.SenderProfileName = Telerik.Sitefinity.Configuration.Config.Get<SecurityConfig>().Notifications.SenderProfile;
      emailSender.SendAsync(registrationEmail, (object) null);
    }

    /// <summary>
    /// Shows an error message depending on the MembershipCreateStatus.
    /// </summary>
    /// <param name="status">The status.</param>
    protected virtual void ShowErrorMessage(MembershipCreateStatus status, UserManager manager)
    {
      switch (status)
      {
        case MembershipCreateStatus.InvalidPassword:
          string str = this.InvalidPasswordErrorMessage;
          if (!string.IsNullOrEmpty(str))
            str = string.Format((IFormatProvider) CultureInfo.InvariantCulture, str, new object[2]
            {
              (object) manager.MinRequiredPasswordLength,
              (object) manager.MinRequiredNonAlphanumericCharacters
            });
          this.AddErrorMessage(str, this.ErrorsPanel);
          break;
        case MembershipCreateStatus.InvalidQuestion:
          this.AddErrorMessage(this.InvalidQuestionErrorMessage, this.ErrorsPanel);
          break;
        case MembershipCreateStatus.InvalidAnswer:
          this.AddErrorMessage(this.InvalidAnswerErrorMessage, this.ErrorsPanel);
          break;
        case MembershipCreateStatus.InvalidEmail:
          this.AddErrorMessage(this.InvalidEmailErrorMessage, this.ErrorsPanel);
          break;
        case MembershipCreateStatus.DuplicateUserName:
          this.AddErrorMessage(this.DuplicateUserNameErrorMessage, this.ErrorsPanel);
          break;
        case MembershipCreateStatus.DuplicateEmail:
          this.AddErrorMessage(this.DuplicateEmailErrorMessage, this.ErrorsPanel);
          break;
        default:
          this.AddErrorMessage(this.UnknownErrorMessage, this.ErrorsPanel);
          break;
      }
    }

    /// <summary>Adds an error message to the Errors panel.</summary>
    /// <param name="message">The message.</param>
    protected virtual void AddErrorMessage(string message, Panel errorPanel)
    {
      errorPanel.Controls.Add((Control) new Label()
      {
        Text = message
      });
      errorPanel.Visible = true;
    }

    /// <summary>Gets the confirmation page URL.</summary>
    /// <param name="user">The user.</param>
    /// <returns></returns>
    protected virtual string GetConfirmationPageUrl(User user) => UserRegistrationEmailGenerator.GetConfirmationPageUrl(this.confirmationPageUrl, user.Id, this.MembershipProvider, SecurityManager.AuthenticationReturnUrl, this.DefaultReturnUrl);

    /// <summary>Hides the container of the form controls.</summary>
    protected virtual void HideForm() => this.FormContainer.Visible = false;

    /// <summary>Executes the user profile successfull update actions.</summary>
    protected virtual void ExecuteUserProfileSuccessfullUpdateActions()
    {
      string empty = string.Empty;
      if (SystemManager.CurrentHttpContext.Request.QueryString.Keys.Contains(SecurityManager.AuthenticationReturnUrl) && !this.SendRegistrationEmail)
      {
        string defaultReturnUrl = SystemManager.CurrentHttpContext.Request.QueryStringGet(SecurityManager.AuthenticationReturnUrl);
        if (string.IsNullOrEmpty(defaultReturnUrl) && !string.IsNullOrEmpty(this.DefaultReturnUrl))
          defaultReturnUrl = this.DefaultReturnUrl;
        SystemManager.CurrentHttpContext.Response.Redirect(string.Format("{0}?{1}={2}", (object) this.GetFrontEndLoginPageUrl(), (object) SecurityManager.AuthenticationReturnUrl, (object) HttpUtility.UrlEncode(defaultReturnUrl)), true);
      }
      else if (!string.IsNullOrEmpty(this.DefaultReturnUrl) && !this.SendRegistrationEmail)
      {
        SystemManager.CurrentHttpContext.Response.Redirect(string.Format("{0}?{1}={2}", (object) this.GetFrontEndLoginPageUrl(), (object) SecurityManager.AuthenticationReturnUrl, (object) HttpUtility.UrlEncode(this.DefaultReturnUrl)), true);
      }
      else
      {
        switch (this.RegistratingUserSuccessAction)
        {
          case SubmittingSuccessAction.ShowMessage:
            this.ShowSubmissionSuccessMessage();
            break;
          case SubmittingSuccessAction.RedirectToPage:
            this.RedirectToRegistationSuccessPage();
            break;
        }
      }
    }

    /// <summary>Shows the submission success message.</summary>
    protected virtual void ShowSubmissionSuccessMessage()
    {
      if (this.SuccessMessageLabel == null || this.ConfirmationText.IsNullOrEmpty())
        return;
      this.SuccessMessageLabel.Text = this.ConfirmationText;
      this.SuccessMessageLabel.Visible = true;
      if (this.FormContainer == null)
        return;
      this.FormContainer.Visible = false;
    }

    /// <summary>
    /// Redirects to the page configured to be shown when a registration was successful.
    /// </summary>
    protected virtual void RedirectToRegistationSuccessPage()
    {
      if (!this.RedirectOnSubmitPageId.HasValue || !(this.RedirectOnSubmitPageId.Value != Guid.Empty))
        return;
      string pageUrl = this.GetPageUrl(this.RedirectOnSubmitPageId.Value, UrlResolveOptions.Absolute);
      if (pageUrl.IsNullOrEmpty())
        return;
      SystemManager.CurrentHttpContext.Response.Redirect(pageUrl, true);
    }

    /// <summary>Gets a url of a page by the id of its node.</summary>
    /// <param name="nodeId">The node id.</param>
    /// <returns>The url of the node or <code>null</code> if none is found</returns>
    private string GetPageUrl(Guid nodeId, UrlResolveOptions resolveOptions)
    {
      SiteMapNode siteMapNodeFromKey = SitefinitySiteMap.GetCurrentProvider().FindSiteMapNodeFromKey(nodeId.ToString());
      return siteMapNodeFromKey != null ? RouteHelper.ResolveUrl(siteMapNodeFromKey.Url, resolveOptions | UrlResolveOptions.RemoveTrailingSlash) : (string) null;
    }

    private MailMessageEventArgs OnSendingConfirmationMail(
      MailMessage confirmationEmail)
    {
      MailMessageEventArgs e = new MailMessageEventArgs(confirmationEmail);
      if (this.SendingConfirmationMail != null)
        this.SendingConfirmationMail((object) this, e);
      return e;
    }

    private MailMessageEventArgs OnSendingSuccessfulRegistrationMail(
      MailMessage registrationSuccessEmail)
    {
      MailMessageEventArgs e = new MailMessageEventArgs(registrationSuccessEmail);
      if (this.SendingSuccessfulRegistrationMail != null)
        this.SendingSuccessfulRegistrationMail((object) this, e);
      return e;
    }

    private LoginCancelEventArgs OnCreatingUser()
    {
      LoginCancelEventArgs e = new LoginCancelEventArgs();
      if (this.CreatingUser != null)
        this.CreatingUser((object) this, e);
      return e;
    }

    private EventArgs OnUserCreated()
    {
      EventArgs e = new EventArgs();
      if (this.UserCreated != null)
        this.UserCreated((object) this, e);
      return e;
    }

    private CreateUserErrorEventArgs OnUserCreationError(
      MembershipCreateStatus status)
    {
      CreateUserErrorEventArgs e = new CreateUserErrorEventArgs(status);
      if (this.UserCreationError != null)
        this.UserCreationError((object) this, e);
      return e;
    }

    private UserOperationInvokingEventArgs OnCreatingUserProfile(
      User user)
    {
      UserOperationInvokingEventArgs e = new UserOperationInvokingEventArgs(user);
      if (this.CreatingUserProfile != null)
        this.CreatingUserProfile((object) this, e);
      return e;
    }

    private UserOperationInvokedEventArgs OnUserProfileCreated(
      User user)
    {
      UserOperationInvokedEventArgs e = new UserOperationInvokedEventArgs(user);
      if (this.UserProfileCreated != null)
        this.UserProfileCreated((object) this, e);
      return e;
    }

    private UserOperationInvokingEventArgs OnAssigningUserRoles(
      User user)
    {
      UserOperationInvokingEventArgs e = new UserOperationInvokingEventArgs(user);
      if (this.AssigningRolesToUser != null)
        this.AssigningRolesToUser((object) this, e);
      return e;
    }

    private UserOperationInvokedEventArgs OnUserRolesAssigned(User user)
    {
      UserOperationInvokedEventArgs e = new UserOperationInvokedEventArgs(user);
      if (this.RolesAssignedToUser != null)
        this.RolesAssignedToUser((object) this, e);
      return e;
    }

    private string GetFrontEndLoginPageUrl()
    {
      string relativePath = string.Empty;
      Telerik.Sitefinity.Multisite.ISite currentSite = SystemManager.CurrentContext.CurrentSite;
      if (currentSite.FrontEndLoginPageId != Guid.Empty)
      {
        PageNode pageNode = PageManager.GetManager().GetPageNode(currentSite.FrontEndLoginPageId);
        if (pageNode != null)
          relativePath = pageNode.GetUrl();
      }
      else if (!string.IsNullOrWhiteSpace(currentSite.FrontEndLoginPageUrl))
        relativePath = currentSite.FrontEndLoginPageUrl;
      return UrlPath.ResolveAbsoluteUrl(relativePath);
    }

    private void RaiseRegistrationEvent(User user)
    {
      UserRegistered userRegistered = new UserRegistered();
      userRegistered.UserId = user.Id;
      userRegistered.Email = user.Email;
      userRegistered.UserName = user.UserName;
      userRegistered.Origin = "Registration form";
      EventHub.Raise((IEvent) userRegistered);
    }

    /// <summary>
    /// Represents a method that operates on a <see cref="T:Telerik.Sitefinity.Security.Model.User" /> instance.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">An argument passed to event handlers operating on a user instance that contains a flag specifying whether the operation should be canceled or not.</param>
    public delegate void UserOperationInvokingEventHandler(
      object sender,
      UserOperationInvokingEventArgs e);

    /// <summary>
    /// Represents a method that operates on a <see cref="T:Telerik.Sitefinity.Security.Model.User" /> instance.
    /// </summary>
    /// <param name="sender">The sender of the event.</param>
    /// <param name="e">An argument passed to event handlers operating on a user instance.</param>
    public delegate void UserOperationInvokedEventHandler(
      object sender,
      UserOperationInvokedEventArgs e);
  }
}
