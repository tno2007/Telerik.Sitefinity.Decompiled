// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.CreateUserWizardForm
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Data;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Mail;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Templates;

namespace Telerik.Sitefinity.Security.Web.UI
{
  /// <summary>
  /// 
  /// </summary>
  public class CreateUserWizardForm : CompositeControl, INamingContainer
  {
    private string _answer;
    private MultiView multiview;
    private bool activeStepIndexSet;
    private Stack historyStack;
    private WizardStepCollection wizardStepCollection;
    private Control providersHolder;
    private ListControl providersList;
    private const string _multiViewID = "WizardMultiView";
    private const string _answerID = "Answer";
    private const string _answerRequiredID = "AnswerRequired";
    private const string _completeStepContainerID = "CompleteStepContainer";
    private const string _confirmPasswordID = "ConfirmPassword";
    private const string _confirmPasswordRequiredID = "ConfirmPasswordRequired";
    private const string _continueButtonID = "ContinueButton";
    private const string _createUserNavigationTemplateName = "CreateUserNavigationTemplate";
    private const string _createUserStepContainerID = "CreateUserStepContainer";
    private const string _editProfileLinkID = "EditProfileLink";
    private const string _emailID = "Email";
    private const string _emailRegExpID = "EmailRegExp";
    private const string _emailRequiredID = "EmailRequired";
    private const string _errorMessageID = "ErrorMessage";
    private ITemplate createUserTemplate;
    private ITemplate completeTemplate;
    private CreateUserWizardStep createUserStep;
    private CreateUserWizardForm.CreateUserStepContainer createUserStepContainer;
    private const ValidatorDisplay compareFieldValidatorDisplay = ValidatorDisplay.Dynamic;
    private CompleteWizardStep completeStep;
    private CreateUserWizardForm.CompleteStepContainer completeStepContainer;
    private string _confirmPassword;
    private bool failure;
    private const string _helpLinkID = "HelpLink";
    private MailDefinition _mailDefinition;
    private string _password;
    private const string _passwordCompareID = "PasswordCompare";
    private const string _passwordID = "Password";
    private const string _passwordRegExpID = "PasswordRegExp";
    private const string _passwordReplacementKey = "<%\\s*Password\\s*%>";
    private const string _passwordRequiredID = "PasswordRequired";
    private const string _questionID = "Question";
    private const string _questionRequiredID = "QuestionRequired";
    private const ValidatorDisplay _regexpFieldValidatorDisplay = ValidatorDisplay.Dynamic;
    private const ValidatorDisplay _requiredFieldValidatorDisplay = ValidatorDisplay.Static;
    private const string _sideBarLabelID = "SideBarLabel";
    private string unknownErrorMessage;
    private const string _userNameID = "UserName";
    private const string _userNameReplacementKey = "<%\\s*UserName\\s*%>";
    private const string _userNameRequiredID = "UserNameRequired";
    private string validationGroup;
    private const int _viewStateArrayLength = 13;
    /// <summary>The command name for the Continue button.</summary>
    public static readonly string ContinueButtonCommandName = "Continue";
    private static readonly object EventButtonContinueClick = new object();
    private static readonly object EventCreatedUser = new object();
    private static readonly object EventCreateUserError = new object();
    private static readonly object EventCreatingUser = new object();
    private static readonly object EventSendingMail = new object();
    private static readonly object EventSendMailError = new object();
    private static readonly object EventActiveStepChanged = new object();
    private string membershipProvider;
    /// <summary>
    /// Specifies the name of the embeded CreateUser template.
    /// </summary>
    public static readonly string CreateUserPath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Security.CreateUserWizardCreateUserStepTemplate.ascx");
    /// <summary>Specifies the name of the embeded Complete template.</summary>
    public static readonly string CompletePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Security.CreateUserWizardCompleteStepTemplate.ascx");

    /// <summary>Occurs when continue button is clicked.</summary>
    public event EventHandler ContinueButtonClick
    {
      add => this.Events.AddHandler(CreateUserWizardForm.EventButtonContinueClick, (Delegate) value);
      remove => this.Events.RemoveHandler(CreateUserWizardForm.EventButtonContinueClick, (Delegate) value);
    }

    /// <summary>Occurs after a membership user is created.</summary>
    public event EventHandler CreatedUser
    {
      add => this.Events.AddHandler(CreateUserWizardForm.EventCreatedUser, (Delegate) value);
      remove => this.Events.RemoveHandler(CreateUserWizardForm.EventCreatedUser, (Delegate) value);
    }

    /// <summary>
    /// Occurs when error is occured during the creation of new user user.
    /// </summary>
    public event CreateUserErrorEventHandler CreateUserError
    {
      add => this.Events.AddHandler(CreateUserWizardForm.EventCreateUserError, (Delegate) value);
      remove => this.Events.RemoveHandler(CreateUserWizardForm.EventCreateUserError, (Delegate) value);
    }

    /// <summary>Occurs before the user is created.</summary>
    public event LoginCancelEventHandler CreatingUser
    {
      add => this.Events.AddHandler(CreateUserWizardForm.EventCreatingUser, (Delegate) value);
      remove => this.Events.RemoveHandler(CreateUserWizardForm.EventCreatingUser, (Delegate) value);
    }

    /// <summary>Occurs before sending the email to the user.</summary>
    public event MailMessageEventHandler SendingMail
    {
      add => this.Events.AddHandler(CreateUserWizardForm.EventSendingMail, (Delegate) value);
      remove => this.Events.RemoveHandler(CreateUserWizardForm.EventSendingMail, (Delegate) value);
    }

    /// <summary>Occurs when an error occurs during sending email.</summary>
    public event SendMailErrorEventHandler SendMailError
    {
      add => this.Events.AddHandler(CreateUserWizardForm.EventSendMailError, (Delegate) value);
      remove => this.Events.RemoveHandler(CreateUserWizardForm.EventSendMailError, (Delegate) value);
    }

    /// <summary>Called when the active step is changed.</summary>
    /// <param name="source">The source.</param>
    /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
    protected virtual void OnActiveStepChanged(object source, EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[CreateUserWizardForm.EventActiveStepChanged];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    /// <summary>The Text in Answer textbox has changed.</summary>
    /// <param name="source">The source.</param>
    /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
    private void AnswerTextChanged(object source, EventArgs e) => this.Answer = ((ITextControl) source).Text;

    private void MultiViewActiveViewChanged(object source, EventArgs e) => this.OnActiveStepChanged((object) this, EventArgs.Empty);

    /// <summary>
    /// Applies the common properties for CreateUserStep template controls.
    /// </summary>
    private void ApplyCommonCreateUserValues()
    {
      if (!string.IsNullOrEmpty(this.UserNameInternal))
      {
        ITextControl userNameTextBox = (ITextControl) this.createUserStepContainer.UserNameTextBox;
        if (userNameTextBox != null)
          userNameTextBox.Text = this.UserNameInternal;
      }
      if (!string.IsNullOrEmpty(this.EmailInternal))
      {
        ITextControl emailTextBox = (ITextControl) this.createUserStepContainer.EmailTextBox;
        if (emailTextBox != null)
          emailTextBox.Text = this.EmailInternal;
      }
      if (!string.IsNullOrEmpty(this.QuestionInternal))
      {
        ITextControl questionTextBox = (ITextControl) this.createUserStepContainer.QuestionTextBox;
        if (questionTextBox != null)
          questionTextBox.Text = this.QuestionInternal;
      }
      if (string.IsNullOrEmpty(this.AnswerInternal))
        return;
      ITextControl answerTextBox = (ITextControl) this.createUserStepContainer.AnswerTextBox;
      if (answerTextBox == null)
        return;
      answerTextBox.Text = this.AnswerInternal;
    }

    /// <summary>Applies the properties Complete template controls .</summary>
    private void ApplyCompleteValues() => this.completeStepContainer.ContinueButton.ValidationGroup = this.ValidationGroup;

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

    private void ProvidersList_SelectedIndexChanged(object sender, EventArgs e) => this.MembershipProvider = ((ListControl) sender).SelectedValue;

    /// <summary>Initizlizes the Membership Providers list control.</summary>
    /// <param name="providersListLocal">The provider list control.</param>
    /// <param name="providersHolderLocal">The placeholder containing the list control.</param>
    protected void InitializeProvidersList(
      ListControl providersListLocal,
      Control providersHolderLocal)
    {
      bool flag;
      if (this.AllowSelectMembershipProvider)
      {
        IEnumerable<DataProviderBase> contextProviders = UserManager.GetManager().GetContextProviders();
        if (contextProviders.Any<DataProviderBase>())
        {
          flag = true;
          if (providersListLocal == null)
          {
            flag = false;
          }
          else
          {
            string defaultProviderName = ManagerBase<MembershipDataProvider>.GetDefaultProviderName();
            providersListLocal.Items.Clear();
            foreach (DataProviderBase dataProviderBase in contextProviders)
            {
              ListItem listItem = new ListItem(dataProviderBase.Title, dataProviderBase.Name);
              listItem.Selected = false;
              if (string.IsNullOrEmpty(this.MembershipProvider) && defaultProviderName == dataProviderBase.Name || dataProviderBase.Name == this.MembershipProvider)
                listItem.Selected = true;
              providersListLocal.Items.Add(listItem);
            }
            providersListLocal.SelectedIndexChanged += new EventHandler(this.ProvidersList_SelectedIndexChanged);
          }
        }
        else
          flag = false;
      }
      else
        flag = false;
      if (providersHolderLocal != null)
        providersHolderLocal.Visible = flag;
      if (providersListLocal == null)
        return;
      providersListLocal.Visible = flag;
    }

    /// <summary>Sets the control properties.</summary>
    protected void ApplyControlProperties() => this.SetChildProperties();

    /// <summary>
    /// Applies the default properties to controls in Create User layout.
    /// </summary>
    private void ApplyDefaultCreateUserValues()
    {
      WebControl userNameTextBox = (WebControl) this.createUserStepContainer.UserNameTextBox;
      userNameTextBox.TabIndex = this.TabIndex;
      userNameTextBox.AccessKey = this.AccessKey;
      WebControl passwordTextBox = (WebControl) this.createUserStepContainer.PasswordTextBox;
      passwordTextBox.TabIndex = this.TabIndex;
      WebControl confirmPasswordTextBox = (WebControl) this.createUserStepContainer.ConfirmPasswordTextBox;
      confirmPasswordTextBox.TabIndex = this.TabIndex;
      bool flag = true;
      RequiredFieldValidator emailRequired = this.createUserStepContainer.EmailRequired;
      if (this.RequireEmail)
      {
        WebControl emailTextBox;
        ((ITextControl) (emailTextBox = (WebControl) this.createUserStepContainer.EmailTextBox)).Text = this.Email;
        int tabIndex = (int) this.TabIndex;
        emailTextBox.TabIndex = (short) tabIndex;
      }
      else
      {
        this.createUserStepContainer.EmailTextBox.Visible = false;
        if (this.createUserStepContainer.EmailPlaceHolder != null)
          this.createUserStepContainer.EmailPlaceHolder.Visible = false;
      }
      int num1 = !flag ? 0 : (this.QuestionAndAnswerRequired ? 1 : 0);
      WebControl questionTextBox = (WebControl) this.createUserStepContainer.QuestionTextBox;
      WebControl answerTextBox = (WebControl) this.createUserStepContainer.AnswerTextBox;
      if (this.QuestionAndAnswerRequired)
      {
        ((ITextControl) questionTextBox).Text = this.Question;
        questionTextBox.TabIndex = this.TabIndex;
        ((ITextControl) answerTextBox).Text = this.Answer;
        answerTextBox.TabIndex = this.TabIndex;
      }
      else
      {
        if (questionTextBox != null)
          questionTextBox.Visible = false;
        if (answerTextBox != null)
          answerTextBox.Visible = false;
        if (this.createUserStepContainer.SecurityQuestionPlaceHolder != null)
          this.createUserStepContainer.SecurityQuestionPlaceHolder.Visible = false;
      }
      int num2 = !flag ? 0 : (!this.AutoGeneratePassword ? 1 : 0);
      if (this.AutoGeneratePassword)
      {
        passwordTextBox.Visible = false;
        confirmPasswordTextBox.Visible = false;
        if (this.createUserStepContainer.PasswordsPlaceHolder != null)
          this.createUserStepContainer.PasswordsPlaceHolder.Visible = false;
      }
      RequiredFieldValidator userNameRequired = this.createUserStepContainer.UserNameRequired;
      userNameRequired.Enabled = flag;
      userNameRequired.Visible = flag;
      Control errorMessageLabel = this.createUserStepContainer.ErrorMessageLabel;
      if (errorMessageLabel == null)
        return;
      if (this.failure && !string.IsNullOrEmpty(this.unknownErrorMessage))
      {
        ((ITextControl) errorMessageLabel).Text = this.unknownErrorMessage;
        errorMessageLabel.Visible = true;
      }
      else
        errorMessageLabel.Visible = false;
    }

    private bool AttemptCreateUser()
    {
      if (this.Page == null || this.Page.IsValid)
      {
        LoginCancelEventArgs e = new LoginCancelEventArgs();
        this.OnCreatingUser(e);
        if (e.Cancel)
          return false;
        UserManager userManager = new UserManager(this.MembershipProvider);
        if (this.AutoGeneratePassword)
          this._password = Membership.GeneratePassword(Math.Max(10, userManager.MinRequiredPasswordLength), Membership.MinRequiredNonAlphanumericCharacters);
        MembershipCreateStatus status;
        userManager.CreateUser(this.EmailInternal, this.PasswordInternal, this.QuestionInternal, this.AnswerInternal, !this.DisableCreatedUser, (object) null, out status);
        if (status == MembershipCreateStatus.Success)
        {
          this.OnCreatedUser(EventArgs.Empty);
          userManager.SaveChanges();
          if (this._mailDefinition != null && !string.IsNullOrEmpty(this.EmailInternal))
          {
            MailMessage passwordMail = EmailSender.CreatePasswordMail(userManager.RecoveryMailAddress, this.EmailInternal, this.UserNameInternal, this.PasswordInternal, Res.Get<ErrorMessages>().CreateUserWizardDefaultSubject, Res.Get<ErrorMessages>().CreateUserWizardDefaultBody);
            EmailSender emailSender = EmailSender.Get();
            emailSender.SenderProfileName = Config.Get<SecurityConfig>().Notifications.SenderProfile;
            emailSender.Sending += (EventHandler<MailMessageEventArgs>) ((s, args) => this.OnSendingMail(args));
            emailSender.Error += (EventHandler<SendMailErrorEventArgs>) ((s, args) => this.OnSendMailError(args));
            emailSender.TrySend(passwordMail);
          }
          if (this.LoginCreatedUser)
            this.AttemptLogin();
          return true;
        }
        this.OnCreateUserError(new CreateUserErrorEventArgs(status));
        switch (status - 2)
        {
          case MembershipCreateStatus.Success:
            string format = this.InvalidPasswordErrorMessage;
            if (!string.IsNullOrEmpty(format))
              format = string.Format((IFormatProvider) CultureInfo.InvariantCulture, format, new object[2]
              {
                (object) userManager.MinRequiredPasswordLength,
                (object) userManager.MinRequiredNonAlphanumericCharacters
              });
            this.unknownErrorMessage = format;
            break;
          case MembershipCreateStatus.InvalidUserName:
            this.unknownErrorMessage = this.InvalidQuestionErrorMessage;
            break;
          case MembershipCreateStatus.InvalidPassword:
            this.unknownErrorMessage = this.InvalidAnswerErrorMessage;
            break;
          case MembershipCreateStatus.InvalidQuestion:
            this.unknownErrorMessage = this.InvalidEmailErrorMessage;
            break;
          case MembershipCreateStatus.InvalidAnswer:
            this.unknownErrorMessage = this.DuplicateUserNameErrorMessage;
            break;
          case MembershipCreateStatus.InvalidEmail:
            this.unknownErrorMessage = this.DuplicateEmailErrorMessage;
            break;
          default:
            this.unknownErrorMessage = this.UnknownErrorMessage;
            break;
        }
      }
      return false;
    }

    private void AttemptLogin()
    {
      if (!new UserManager(this.MembershipProvider).ValidateUser(this.UserName, this.Password) || this.Page == null)
        return;
      string membershipProvider = this.MembershipProvider;
      if (string.IsNullOrEmpty(membershipProvider))
        membershipProvider = ManagerBase<MembershipDataProvider>.GetDefaultProviderName();
      SecurityManager.SetAuthenticationCookie(SystemManager.CurrentHttpContext.Response, membershipProvider, this.UserName, false, DateTime.Now);
      string str = this.Page.Request.QueryStringGet(SecurityManager.AuthenticationReturnUrl);
      if (string.IsNullOrEmpty(str))
        return;
      this.Page.Response.Redirect(HttpUtility.UrlDecode(str), true);
    }

    private void ConfirmPasswordTextChanged(object source, EventArgs e)
    {
      if (this.AutoGeneratePassword)
        return;
      this._confirmPassword = ((ITextControl) source).Text;
    }

    /// <summary>Creates the child controls.</summary>
    protected override void CreateChildControls()
    {
      this.Controls.Clear();
      this.createUserStep = (CreateUserWizardStep) null;
      this.completeStep = (CompleteWizardStep) null;
      this.CreateControlHierarchy();
      this.UpdateValidators();
    }

    /// <summary>
    /// Creates the hierarchy of child controls that make up the control.
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">
    /// The sidebar template does not contain a <see cref="T:System.Web.UI.WebControls.DataList" /> control.
    /// </exception>
    protected virtual void CreateControlHierarchy()
    {
      this.EnsureCreateUserSteps();
      this.InstantiateStepContentTemplates();
      if (this.createUserStepContainer.UserNameTextBox is IEditableTextControl userNameTextBox)
        userNameTextBox.TextChanged += new EventHandler(this.UserNameTextChanged);
      if (this.createUserStepContainer.EmailTextBox is IEditableTextControl emailTextBox)
        emailTextBox.TextChanged += new EventHandler(this.EmailTextChanged);
      if (this.createUserStepContainer.QuestionTextBox is IEditableTextControl questionTextBox)
        questionTextBox.TextChanged += new EventHandler(this.QuestionTextChanged);
      if (this.createUserStepContainer.AnswerTextBox is IEditableTextControl answerTextBox)
        answerTextBox.TextChanged += new EventHandler(this.AnswerTextChanged);
      if (this.createUserStepContainer.PasswordTextBox is IEditableTextControl passwordTextBox)
        passwordTextBox.TextChanged += new EventHandler(this.PasswordTextChanged);
      if (this.createUserStepContainer.ConfirmPasswordTextBox is IEditableTextControl confirmPasswordTextBox)
        confirmPasswordTextBox.TextChanged += new EventHandler(this.ConfirmPasswordTextChanged);
      this.Controls.Add((Control) this.MultiView);
      this.ApplyCommonCreateUserValues();
    }

    private void EmailTextChanged(object source, EventArgs e) => this.Email = ((ITextControl) source).Text;

    private void EnsureCreateUserSteps()
    {
      bool flag1 = false;
      bool flag2 = false;
      foreach (WizardStepBase wizardStep in this.WizardSteps)
      {
        if (wizardStep is CreateUserWizardStep)
        {
          flag1 = !flag1 ? true : throw new HttpException(Res.Get<ErrorMessages>().CreateUserWizardDuplicateCreateUserWizardStep);
          this.createUserStep = (CreateUserWizardStep) wizardStep;
        }
        else if (wizardStep is CompleteWizardStep)
        {
          flag2 = !flag2 ? true : throw new HttpException(Res.Get<ErrorMessages>().CreateUserWizardDuplicateCompleteWizardStep);
          this.completeStep = (CompleteWizardStep) wizardStep;
        }
      }
      if (!flag1)
      {
        this.createUserStep = new CreateUserWizardStep();
        this.WizardSteps.AddAt(0, (WizardStepBase) this.createUserStep);
      }
      if (!flag2)
      {
        this.completeStep = new CompleteWizardStep();
        this.WizardSteps.Add((WizardStepBase) this.completeStep);
      }
      if (this.ActiveStepIndex != -1)
        return;
      this.ActiveStepIndex = 0;
    }

    /// <summary>Gets or sets the index of the active step.</summary>
    /// <value>The index of the active step.</value>
    public virtual int ActiveStepIndex
    {
      get => this.MultiView.ActiveViewIndex;
      set
      {
        if (value < -1 || value >= this.WizardSteps.Count)
          throw new ArgumentOutOfRangeException(nameof (value), Res.Get<ErrorMessages>().WizardActiveStepIndexOutOfRange);
        if (this.MultiView.ActiveViewIndex == value)
          return;
        this.MultiView.ActiveViewIndex = value;
        this.activeStepIndexSet = true;
      }
    }

    /// <summary>
    /// Creates a layout template from a specified
    /// user control (external template) or embedded resource.
    /// </summary>
    /// <returns>
    /// An instance of <see cref="T:System.Web.UI.ITemplate" /> object.
    /// </returns>
    protected virtual ITemplate CreateLayoutTemplate(
      string layoutTemplatePath,
      string layoutTemplateName)
    {
      return ControlUtilities.GetTemplate(new TemplateInfo()
      {
        TemplatePath = layoutTemplatePath,
        TemplateName = layoutTemplateName,
        ControlType = this.GetType(),
        ConfigAdditionalKey = this.MembershipProvider
      });
    }

    /// <summary>
    /// Gets or sets the custom CreateUser template for the control.
    /// </summary>
    /// <value>The create user template.</value>
    [DescriptionResource(typeof (PageResources), "CreateUserTemplateDescription")]
    [Browsable(false)]
    public virtual ITemplate CreateUserTemplate
    {
      get
      {
        if (this.createUserTemplate == null)
          this.createUserTemplate = this.createUserStep == null || this.createUserStep.ContentTemplate == null ? this.CreateLayoutTemplate(this.CreateUserTemplatePath, (string) null) : this.createUserStep.ContentTemplate;
        return this.createUserTemplate;
      }
      set
      {
        this.createUserTemplate = value;
        this.ChildControlsCreated = false;
      }
    }

    /// <summary>
    /// Gets or sets the path to a custom CreateUser template template for the control.
    /// </summary>
    public virtual string CreateUserTemplatePath
    {
      get => (string) this.ViewState[nameof (CreateUserTemplatePath)] ?? CreateUserWizardForm.CreateUserPath;
      set => this.ViewState[nameof (CreateUserTemplatePath)] = (object) value;
    }

    /// <summary>
    /// Gets or sets the custom Complete template for the control.
    /// </summary>
    /// <value>The create user template.</value>
    [DescriptionResource(typeof (PageResources), "CompleteTemplateDescription")]
    [Browsable(false)]
    public virtual ITemplate CompleteTemplate
    {
      get
      {
        if (this.completeTemplate == null)
          this.completeTemplate = this.completeStep == null || this.completeStep.ContentTemplate == null ? this.CreateLayoutTemplate(this.CompleteTemplatePath, (string) null) : this.completeStep.ContentTemplate;
        return this.completeTemplate;
      }
      set
      {
        this.completeTemplate = value;
        this.ChildControlsCreated = false;
      }
    }

    /// <summary>
    /// Gets or sets the path to a custom Complete template template for the control.
    /// </summary>
    public virtual string CompleteTemplatePath
    {
      get => (string) this.ViewState[nameof (CompleteTemplatePath)] ?? CreateUserWizardForm.CompletePath;
      set => this.ViewState[nameof (CompleteTemplatePath)] = (object) value;
    }

    /// <summary>
    /// Gets or sets the cancel destination page URL. User is redirected to this url when click on Cancel button.
    /// </summary>
    /// <value>The cancel destination page URL.</value>
    public virtual string CancelDestinationPageUrl
    {
      get => this.ViewState[nameof (CancelDestinationPageUrl)] is string str ? str : string.Empty;
      set => this.ViewState[nameof (CancelDestinationPageUrl)] = (object) value;
    }

    /// <summary>
    /// Gets or sets the finish destination page URL. User is redirected to the url when click on Finish button.
    /// </summary>
    /// <value>The finish destination page URL.</value>
    public virtual string FinishDestinationPageUrl
    {
      get => (string) this.ViewState[nameof (FinishDestinationPageUrl)] ?? string.Empty;
      set => this.ViewState[nameof (FinishDestinationPageUrl)] = (object) value;
    }

    /// <summary>
    /// Gets the step in the WizardSteps collection that is currently displayed to the user.
    /// </summary>
    /// <value>Gets the step in the WizardSteps collection that is currently displayed to the user.</value>
    public WizardStepBase ActiveStep
    {
      get
      {
        if (this.ActiveStepIndex < -1 || this.ActiveStepIndex >= this.WizardSteps.Count)
          throw new InvalidOperationException(Res.Get<ErrorMessages>().WizardActiveStepIndexOutOfRange);
        return this.MultiView.GetActiveView() as WizardStepBase;
      }
    }

    /// <summary>Instantiates the step content templates.</summary>
    protected virtual void InstantiateStepContentTemplates()
    {
      WizardStepCollection wizardSteps = this.WizardSteps;
      for (int index = 0; index < wizardSteps.Count; ++index)
      {
        WizardStepBase wizardStepBase = wizardSteps[index];
        if (wizardStepBase == this.CreateUserStep)
        {
          wizardStepBase.Controls.Clear();
          this.createUserStepContainer = new CreateUserWizardForm.CreateUserStepContainer(this);
          this.createUserStepContainer.ID = "CreateUserStepContainer";
          this.CreateUserTemplate.InstantiateIn((Control) this.createUserStepContainer);
          this.providersHolder = this.createUserStepContainer.ProvidersHolder;
          this.providersList = this.createUserStepContainer.ProvidersList;
          if (this.providersHolder != null && this.providersList != null)
            this.InitializeProvidersList(this.providersList, this.providersHolder);
          wizardStepBase.Controls.Add((Control) this.createUserStepContainer);
        }
        else if (wizardStepBase == this.CompleteStep)
        {
          wizardStepBase.Controls.Clear();
          this.completeStepContainer = new CreateUserWizardForm.CompleteStepContainer();
          this.completeStepContainer.ID = "CompleteStepContainer";
          this.CompleteTemplate.InstantiateIn((Control) this.completeStepContainer);
          wizardStepBase.Controls.Add((Control) this.completeStepContainer);
        }
        else if (wizardStepBase is TemplatedWizardStep templatedWizardStep)
        {
          templatedWizardStep.Controls.Clear();
          GenericContainer genericContainer = new GenericContainer();
          templatedWizardStep.ContentTemplate?.InstantiateIn((Control) genericContainer);
          templatedWizardStep.Controls.Add((Control) genericContainer);
        }
      }
    }

    /// <summary>
    /// Restores view-state information from a previous request that was saved with the <see cref="M:System.Web.UI.WebControls.WebControl.SaveViewState" /> method.
    /// </summary>
    /// <param name="savedState">An object that represents the control state to restore.</param>
    protected override void LoadViewState(object savedState)
    {
      if (savedState == null)
      {
        base.LoadViewState((object) null);
      }
      else
      {
        object[] objArray = (object[]) savedState;
        if (objArray.Length != 2)
          throw new ArgumentException(Res.Get<ErrorMessages>().ViewStateInvalidViewState);
        base.LoadViewState(objArray[0]);
        if (objArray[1] == null)
          return;
        ((IStateManager) this.MailDefinition).LoadViewState(objArray[9]);
      }
    }

    /// <summary>Gets the type of the wizard step.</summary>
    /// <param name="index">The index.</param>
    /// <returns></returns>
    protected WizardStepType GetStepType(int index) => this.GetStepType(this.WizardSteps[index], index);

    /// <summary>Gets the type of the wizard step.</summary>
    /// <param name="step">The step.</param>
    /// <returns></returns>
    protected WizardStepType GetStepType(WizardStepBase step)
    {
      int index = this.WizardSteps.IndexOf(step);
      return this.GetStepType(step, index);
    }

    /// <summary>Gets the type of the wizard step.</summary>
    /// <param name="wizardStep">The wizard step.</param>
    /// <param name="index">The index.</param>
    /// <returns></returns>
    public WizardStepType GetStepType(WizardStepBase wizardStep, int index)
    {
      if (wizardStep.StepType != WizardStepType.Auto)
        return wizardStep.StepType;
      if (this.WizardSteps.Count == 1 || index < this.WizardSteps.Count - 1 && this.WizardSteps[index + 1].StepType == WizardStepType.Complete)
        return WizardStepType.Finish;
      if (index == 0)
        return WizardStepType.Start;
      return index == this.WizardSteps.Count - 1 ? WizardStepType.Finish : WizardStepType.Step;
    }

    /// <summary>
    /// Raises the <see cref="E:CancelButtonClick" /> event.
    /// </summary>
    /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
    protected virtual void OnCancelButtonClick(EventArgs e)
    {
      if (this.Page == null)
        return;
      string destinationPageUrl = this.CancelDestinationPageUrl;
      if (!string.IsNullOrEmpty(destinationPageUrl))
      {
        this.Page.Response.Redirect(this.ResolveClientUrl(destinationPageUrl), false);
      }
      else
      {
        string s = this.Page.Request.QueryStringGet(SecurityManager.AuthenticationReturnUrl);
        if (!string.IsNullOrEmpty(s))
          this.Page.Response.Redirect(SystemManager.CurrentHttpContext.Server.UrlDecode(s), true);
        else
          this.Page.Response.Redirect("~/Sitefinity/Login", true);
      }
    }

    /// <summary>Called when bubble event.</summary>
    /// <param name="source">The source.</param>
    /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
    /// <returns></returns>
    protected override bool OnBubbleEvent(object source, EventArgs e)
    {
      bool flag1 = false;
      if (e is CommandEventArgs)
      {
        CommandEventArgs commandEventArgs = (CommandEventArgs) e;
        if (commandEventArgs.CommandName.Equals(CreateUserWizardForm.ContinueButtonCommandName, StringComparison.CurrentCultureIgnoreCase))
        {
          this.OnContinueButtonClick(EventArgs.Empty);
          return true;
        }
        if (string.Equals(Wizard.CancelCommandName, commandEventArgs.CommandName, StringComparison.OrdinalIgnoreCase))
        {
          this.OnCancelButtonClick(EventArgs.Empty);
          return true;
        }
        int activeStepIndex = this.ActiveStepIndex;
        int nextStepIndex = activeStepIndex;
        bool flag2 = true;
        WizardStepType wizardStepType = WizardStepType.Auto;
        WizardStepBase wizardStep = this.WizardSteps[activeStepIndex];
        if (wizardStep is TemplatedWizardStep)
          flag2 = false;
        else
          wizardStepType = this.GetStepType(wizardStep);
        WizardNavigationFormsEventArgs e1 = new WizardNavigationFormsEventArgs(activeStepIndex, nextStepIndex);
        if (this.Page != null && !this.Page.IsValid)
          e1.Cancel = true;
        bool flag3 = false;
        this.activeStepIndexSet = false;
        if (string.Equals(Wizard.MoveNextCommandName, commandEventArgs.CommandName, StringComparison.OrdinalIgnoreCase))
        {
          if (flag2 && wizardStepType != WizardStepType.Start && wizardStepType != WizardStepType.Step)
            throw new InvalidOperationException(string.Format(Res.Get<ErrorMessages>().WizardInvalidBubbleEvent, (object) Wizard.MoveNextCommandName));
          if (activeStepIndex < this.WizardSteps.Count - 1)
            e1.NextStepIndex = activeStepIndex + 1;
          this.OnNextButtonClick(e1);
          flag1 = true;
        }
        else if (string.Equals(Wizard.MovePreviousCommandName, commandEventArgs.CommandName, StringComparison.OrdinalIgnoreCase))
        {
          if (flag2 && wizardStepType != WizardStepType.Step && wizardStepType != WizardStepType.Finish)
            throw new InvalidOperationException(string.Format(Res.Get<ErrorMessages>().WizardInvalidBubbleEvent, (object) Wizard.MovePreviousCommandName));
          flag3 = true;
          int previousStepIndex = this.GetPreviousStepIndex(false);
          if (previousStepIndex != -1)
            e1.NextStepIndex = previousStepIndex;
          flag1 = true;
        }
        else if (string.Equals(Wizard.MoveCompleteCommandName, commandEventArgs.CommandName, StringComparison.OrdinalIgnoreCase))
        {
          if (flag2 && wizardStepType != WizardStepType.Finish)
            throw new InvalidOperationException(string.Format(Res.Get<ErrorMessages>().WizardInvalidBubbleEvent, (object) Wizard.MoveCompleteCommandName));
          if (activeStepIndex < this.WizardSteps.Count - 1)
            e1.NextStepIndex = activeStepIndex + 1;
          flag1 = true;
        }
        else if (string.Equals(Wizard.MoveToCommandName, commandEventArgs.CommandName, StringComparison.OrdinalIgnoreCase))
        {
          int num = int.Parse((string) commandEventArgs.CommandArgument, (IFormatProvider) CultureInfo.InvariantCulture);
          e1.NextStepIndex = num;
          flag1 = true;
        }
        if (flag1)
        {
          if (!e1.Cancel)
          {
            if (!this.activeStepIndexSet && this.AllowNavigationToStep(e1.NextStepIndex))
            {
              if (flag3)
                this.GetPreviousStepIndex(true);
              this.ActiveStepIndex = e1.NextStepIndex;
            }
            return flag1;
          }
          this.ActiveStepIndex = activeStepIndex;
        }
      }
      return flag1;
    }

    /// <summary>
    /// Checks whether user can go to the wizard step on the specified index.
    /// </summary>
    /// <param name="index">The index.</param>
    /// <returns></returns>
    protected virtual bool AllowNavigationToStep(int index) => this.historyStack == null || !this.historyStack.Contains((object) index) || this.WizardSteps[index].AllowReturn;

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      if (this.ActiveStepIndex == -1 && this.WizardSteps.Count > 0 && !this.DesignMode)
        this.ActiveStepIndex = 0;
      this.EnsureChildControls();
      if (this.Page != null)
        this.Page.RegisterRequiresControlState((Control) this);
      if (this.Page == null || this.Page.IsPostBack || string.IsNullOrEmpty(this.Page.Request.QueryStringGet("provider")))
        return;
      this.MembershipProvider = this.Page.Request.QueryStringGet("provider");
    }

    /// <summary>Loads the state of the control.</summary>
    /// <param name="savedState">The state.</param>
    protected override void LoadControlState(object savedState)
    {
      object[] objArray = (object[]) savedState;
      if (objArray[0] is Triplet triplet)
      {
        base.LoadControlState(triplet.First);
        if (triplet.Second is Array second)
        {
          Array.Reverse(second);
          this.historyStack = new Stack((ICollection) second);
        }
        this.ActiveStepIndex = (int) triplet.Third;
      }
      this.membershipProvider = (string) objArray[1];
      this.UpdateValidators();
      if (this.createUserStepContainer == null)
        return;
      this.providersHolder = this.createUserStepContainer.ProvidersHolder;
      this.providersList = this.createUserStepContainer.ProvidersList;
      if (this.providersHolder == null || this.providersList == null)
        return;
      this.InitializeProvidersList(this.providersList, this.providersHolder);
    }

    /// <summary>
    /// Saves any server control state changes that have occurred since the time the page was posted back to the server.
    /// </summary>
    /// <returns>
    /// Returns the server control's current state. If there is no state associated with the control, this method returns null.
    /// </returns>
    protected override object SaveControlState()
    {
      int activeStepIndex = this.ActiveStepIndex;
      if (this.historyStack == null || this.historyStack.Count == 0 || (int) this.historyStack.Peek() != activeStepIndex)
        this.History.Push((object) this.ActiveStepIndex);
      object x = base.SaveControlState();
      bool flag = this.historyStack != null && this.historyStack.Count > 0;
      Triplet triplet = (Triplet) null;
      if (x != null | flag || activeStepIndex != -1)
        triplet = new Triplet(x, flag ? (object) this.historyStack.ToArray() : (object) (object[]) null, (object) activeStepIndex);
      return (object) new object[2]
      {
        (object) triplet,
        (object) this.MembershipProvider
      };
    }

    /// <summary>Gets the index of the previous step.</summary>
    /// <param name="popStack">if set to <c>true</c> [pop stack].</param>
    /// <returns></returns>
    protected int GetPreviousStepIndex(bool popStack)
    {
      int previousStepIndex = -1;
      int activeStepIndex = this.ActiveStepIndex;
      if (this.historyStack != null && this.historyStack.Count != 0)
      {
        if (popStack)
        {
          previousStepIndex = (int) this.historyStack.Pop();
          if (previousStepIndex == activeStepIndex && this.historyStack.Count > 0)
            previousStepIndex = (int) this.historyStack.Pop();
        }
        else
        {
          previousStepIndex = (int) this.historyStack.Peek();
          if (previousStepIndex == activeStepIndex && this.historyStack.Count > 1)
          {
            int num = (int) this.historyStack.Pop();
            previousStepIndex = (int) this.historyStack.Peek();
            this.historyStack.Push((object) num);
          }
        }
        if (previousStepIndex == activeStepIndex)
          return -1;
      }
      return previousStepIndex;
    }

    /// <summary>
    /// Raises the <see cref="E:ContinueButtonClick" /> event.
    /// </summary>
    /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
    protected virtual void OnContinueButtonClick(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[CreateUserWizardForm.EventButtonContinueClick];
      if (eventHandler != null)
        eventHandler((object) this, e);
      string destinationPageUrl = this.ContinueDestinationPageUrl;
      if (RouteHelper.IsAbsoluteUrl(destinationPageUrl) || string.IsNullOrEmpty(destinationPageUrl))
        return;
      this.Page.Response.Redirect(this.ResolveClientUrl(destinationPageUrl), false);
    }

    /// <summary>
    /// Raises the <see cref="E:CreatedUser" /> event.
    /// </summary>
    /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
    protected virtual void OnCreatedUser(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[CreateUserWizardForm.EventCreatedUser];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    /// <summary>
    /// Raises the <see cref="E:CreateUserError" /> event.
    /// </summary>
    /// <param name="e">The <see cref="T:System.Web.UI.WebControls.CreateUserErrorEventArgs" /> instance containing the event data.</param>
    protected virtual void OnCreateUserError(CreateUserErrorEventArgs e)
    {
      CreateUserErrorEventHandler errorEventHandler = (CreateUserErrorEventHandler) this.Events[CreateUserWizardForm.EventCreateUserError];
      if (errorEventHandler == null)
        return;
      errorEventHandler((object) this, e);
    }

    /// <summary>
    /// Raises the <see cref="E:CreatingUser" /> event.
    /// </summary>
    /// <param name="e">The <see cref="T:System.Web.UI.WebControls.LoginCancelEventArgs" /> instance containing the event data.</param>
    protected virtual void OnCreatingUser(LoginCancelEventArgs e)
    {
      LoginCancelEventHandler cancelEventHandler = (LoginCancelEventHandler) this.Events[CreateUserWizardForm.EventCreatingUser];
      if (cancelEventHandler == null)
        return;
      cancelEventHandler((object) this, e);
    }

    /// <summary>
    /// Raises the <see cref="E:NextButtonClick" /> event.
    /// </summary>
    /// <param name="e">The <see cref="T:Telerik.Sitefinity.Security.Web.UI.WizardNavigationFormsEventArgs" /> instance containing the event data.</param>
    protected virtual void OnNextButtonClick(WizardNavigationFormsEventArgs e)
    {
      if (this.WizardSteps[e.CurrentStepIndex] != this.createUserStep)
        return;
      e.Cancel = this.Page != null && !this.Page.IsValid;
      if (e.Cancel)
        return;
      UserManager manager = UserManager.GetManager();
      bool suppressSecurityChecks = manager.Provider.SuppressSecurityChecks;
      manager.Provider.SuppressSecurityChecks = true;
      this.failure = !this.AttemptCreateUser();
      manager.Provider.SuppressSecurityChecks = suppressSecurityChecks;
      if (!this.failure)
        return;
      e.Cancel = true;
      ITextControl errorMessageLabel = (ITextControl) this.createUserStepContainer.ErrorMessageLabel;
      if (errorMessageLabel == null || string.IsNullOrEmpty(this.unknownErrorMessage))
        return;
      errorMessageLabel.Text = this.unknownErrorMessage;
      if (!(errorMessageLabel is Control))
        return;
      ((Control) errorMessageLabel).Visible = true;
    }

    /// <summary>
    /// Writes the <see cref="T:System.Web.UI.WebControls.CompositeControl" /> content to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object, for display on the client.
    /// </summary>
    /// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    protected override void Render(HtmlTextWriter writer)
    {
      if (this.Page != null)
        this.Page.VerifyRenderingInServerForm((Control) this);
      this.EnsureChildControls();
      this.ApplyControlProperties();
      if (this.ActiveStepIndex == -1 || this.WizardSteps.Count == 0)
        return;
      this.RenderContents(writer);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      if (this.Page != null)
      {
        HtmlForm form = this.Page.Form;
        if (this.ActiveStep == this.createUserStep)
        {
          this.Page.SetFocus(this.createUserStepContainer.UserNameTextBox);
          Control hiddenSubmitButton = this.createUserStepContainer.HiddenSubmitButton as Control;
          if (this.Page.Form != null && hiddenSubmitButton != null)
            this.Page.Form.DefaultButton = hiddenSubmitButton.UniqueID;
        }
      }
      this.EnsureCreateUserSteps();
      base.OnPreRender(e);
      string membershipProvider = this.MembershipProvider;
      if (!string.IsNullOrEmpty(membershipProvider) && !this.NonDefaultProviderExists(membershipProvider))
        throw new HttpException(Res.Get<ErrorMessages>().WebControlCannotFindProvider);
    }

    private bool NonDefaultProviderExists(string membershipProviderLocal) => UserManager.GetManager().GetContextProviders().Any<DataProviderBase>((Func<DataProviderBase, bool>) (p => p.Name == membershipProviderLocal));

    /// <summary>
    /// Raises the <see cref="E:SendingMail" /> event.
    /// </summary>
    /// <param name="e">The <see cref="T:System.Web.UI.WebControls.MailMessageEventArgs" /> instance containing the event data.</param>
    protected virtual void OnSendingMail(MailMessageEventArgs e)
    {
      MailMessageEventHandler messageEventHandler = (MailMessageEventHandler) this.Events[CreateUserWizardForm.EventSendingMail];
      if (messageEventHandler == null)
        return;
      messageEventHandler((object) this, e);
    }

    /// <summary>
    /// Raises the <see cref="E:SendMailError" /> event.
    /// </summary>
    /// <param name="e">The <see cref="T:System.Web.UI.WebControls.SendMailErrorEventArgs" /> instance containing the event data.</param>
    protected virtual void OnSendMailError(SendMailErrorEventArgs e)
    {
      SendMailErrorEventHandler errorEventHandler = (SendMailErrorEventHandler) this.Events[CreateUserWizardForm.EventSendMailError];
      if (errorEventHandler == null)
        return;
      errorEventHandler((object) this, e);
    }

    /// <summary>Passwords text has changed.</summary>
    /// <param name="source">The source.</param>
    /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
    private void PasswordTextChanged(object source, EventArgs e)
    {
      if (this.AutoGeneratePassword)
        return;
      this._password = ((ITextControl) source).Text;
    }

    /// <summary>Questions text has changed.</summary>
    /// <param name="source">The source.</param>
    /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
    private void QuestionTextChanged(object source, EventArgs e) => this.Question = ((ITextControl) source).Text;

    /// <summary>Saves the viewstate of the control.</summary>
    /// <returns></returns>
    protected override object SaveViewState()
    {
      object[] objArray = new object[2]
      {
        base.SaveViewState(),
        this._mailDefinition != null ? ((IStateManager) this._mailDefinition).SaveViewState() : (object) null
      };
      for (int index = 0; index < 13; ++index)
      {
        if (objArray[index] != null)
          return (object) objArray;
      }
      return (object) null;
    }

    /// <summary>Gets the multi view.</summary>
    /// <value>The multi view.</value>
    public MultiView MultiView
    {
      get
      {
        if (this.multiview == null)
        {
          this.multiview = new MultiView();
          this.multiview.ID = "WizardMultiView";
          this.multiview.ActiveViewChanged += new EventHandler(this.MultiViewActiveViewChanged);
        }
        return this.multiview;
      }
    }

    /// <summary>Sets the child properties.</summary>
    protected void SetChildProperties()
    {
      this.ApplyCommonCreateUserValues();
      if (this.ActiveStep == this.CreateUserStep)
        this.ApplyDefaultCreateUserValues();
      if (this.ActiveStep == this.CompleteStep)
        this.ApplyCompleteValues();
      Control errorMessageLabel = this.createUserStepContainer.ErrorMessageLabel;
      if (errorMessageLabel == null)
        return;
      if (this.failure && !string.IsNullOrEmpty(this.unknownErrorMessage))
      {
        ((ITextControl) errorMessageLabel).Text = this.unknownErrorMessage;
        errorMessageLabel.Visible = true;
      }
      else
        errorMessageLabel.Visible = false;
    }

    /// <summary>Tracks the viewstate of the control.</summary>
    protected override void TrackViewState()
    {
      base.TrackViewState();
      if (this._mailDefinition == null)
        return;
      ((IStateManager) this._mailDefinition).TrackViewState();
    }

    private void SetValidationGroupOfValidator(BaseValidator validator)
    {
      if (validator == null)
        return;
      validator.ValidationGroup = this.ValidationGroup;
    }

    /// <summary>Updates the validators of the control.</summary>
    private void UpdateValidators()
    {
      if (this.DesignMode || this.createUserStepContainer == null)
        return;
      BaseValidator passwordRequired1 = (BaseValidator) this.createUserStepContainer.ConfirmPasswordRequired;
      BaseValidator passwordRequired2 = (BaseValidator) this.createUserStepContainer.PasswordRequired;
      BaseValidator passwordRegExpValidator = (BaseValidator) this.createUserStepContainer.PasswordRegExpValidator;
      BaseValidator emailRequired = (BaseValidator) this.createUserStepContainer.EmailRequired;
      BaseValidator emailRegExpValidator = (BaseValidator) this.createUserStepContainer.EmailRegExpValidator;
      BaseValidator questionRequired = (BaseValidator) this.createUserStepContainer.QuestionRequired;
      BaseValidator answerRequired = (BaseValidator) this.createUserStepContainer.AnswerRequired;
      BaseValidator compareValidator = (BaseValidator) this.createUserStepContainer.PasswordCompareValidator;
      this.SetValidationGroupOfValidator((BaseValidator) this.createUserStepContainer.UserNameRequired);
      this.SetValidationGroupOfValidator(passwordRequired1);
      this.SetValidationGroupOfValidator(passwordRequired2);
      this.SetValidationGroupOfValidator(passwordRegExpValidator);
      this.SetValidationGroupOfValidator(emailRequired);
      this.SetValidationGroupOfValidator(emailRegExpValidator);
      this.SetValidationGroupOfValidator(questionRequired);
      this.SetValidationGroupOfValidator(answerRequired);
      this.SetValidationGroupOfValidator(compareValidator);
      if (this.ActiveStep == this.CreateUserStep)
      {
        this.createUserStepContainer.CreateUserButton.ValidationGroup = this.ValidationGroup;
        if (this.createUserStepContainer.HiddenSubmitButton != null)
          this.createUserStepContainer.HiddenSubmitButton.ValidationGroup = this.ValidationGroup;
      }
      if (this.AutoGeneratePassword)
      {
        if (passwordRequired1 != null)
        {
          this.Page.Validators.Remove((IValidator) passwordRequired1);
          passwordRequired1.Enabled = false;
        }
        if (passwordRequired2 != null)
        {
          this.Page.Validators.Remove((IValidator) passwordRequired2);
          passwordRequired2.Enabled = false;
        }
        if (passwordRegExpValidator != null)
        {
          this.Page.Validators.Remove((IValidator) passwordRegExpValidator);
          passwordRegExpValidator.Enabled = false;
        }
        if (compareValidator != null)
        {
          this.Page.Validators.Remove((IValidator) compareValidator);
          compareValidator.Enabled = false;
        }
      }
      else if (this.PasswordRegularExpression.Length <= 0 && passwordRegExpValidator != null)
      {
        if (this.Page != null)
          this.Page.Validators.Remove((IValidator) passwordRegExpValidator);
        passwordRegExpValidator.Enabled = false;
      }
      if (!this.RequireEmail)
      {
        if (emailRequired != null)
        {
          if (this.Page != null)
            this.Page.Validators.Remove((IValidator) emailRequired);
          emailRequired.Enabled = false;
        }
        if (emailRegExpValidator != null)
        {
          if (this.Page != null)
            this.Page.Validators.Remove((IValidator) emailRegExpValidator);
          emailRegExpValidator.Enabled = false;
        }
      }
      else if (emailRegExpValidator != null && string.IsNullOrEmpty(((RegularExpressionValidator) emailRegExpValidator).ValidationExpression) && emailRegExpValidator != null)
      {
        if (this.Page != null)
          this.Page.Validators.Remove((IValidator) emailRegExpValidator);
        emailRegExpValidator.Enabled = false;
      }
      if (this.QuestionAndAnswerRequired)
        return;
      if (questionRequired != null)
      {
        if (this.Page != null)
          this.Page.Validators.Remove((IValidator) questionRequired);
        questionRequired.Enabled = false;
      }
      if (answerRequired == null)
        return;
      if (this.Page != null)
        this.Page.Validators.Remove((IValidator) answerRequired);
      answerRequired.Enabled = false;
    }

    /// <summary>Usersname text has changed.</summary>
    /// <param name="source">The source.</param>
    /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
    private void UserNameTextChanged(object source, EventArgs e) => this.UserName = ((ITextControl) source).Text;

    /// <summary>The initial value in the answer textbox.</summary>
    /// <value>The initial value in the answer textbox.</value>
    [Themeable(false)]
    [DefaultValue("")]
    [Localizable(true)]
    public virtual string Answer
    {
      get => this._answer != null ? this._answer : string.Empty;
      set => this._answer = value;
    }

    /// <summary>Gets the answer entered by the user.</summary>
    /// <value>Gets the answer entered by the user.</value>
    private string AnswerInternal
    {
      get
      {
        string answerInternal = this.Answer;
        if (string.IsNullOrEmpty(this.Answer) && this.createUserStepContainer != null)
        {
          ITextControl answerTextBox = (ITextControl) this.createUserStepContainer.AnswerTextBox;
          if (answerTextBox != null)
            answerInternal = answerTextBox.Text;
        }
        if (string.IsNullOrEmpty(answerInternal))
          answerInternal = (string) null;
        return answerInternal;
      }
    }

    /// <summary>
    /// Determines if the control autogenerates a password for the user.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if the control autogenerates a password for the user; otherwise, <c>false</c>.
    /// </value>
    public virtual bool AutoGeneratePassword
    {
      get
      {
        object obj = this.ViewState[nameof (AutoGeneratePassword)];
        return obj != null && (bool) obj;
      }
      set
      {
        if (this.AutoGeneratePassword == value)
          return;
        this.ViewState[nameof (AutoGeneratePassword)] = (object) value;
        this.RequiresControlsRecreation();
      }
    }

    private void RequiresControlsRecreation()
    {
      if (!this.ChildControlsCreated)
        return;
      this.ChildControlsCreated = false;
    }

    /// <summary>The complete WizardStep.</summary>
    /// <value>The complete WizardStep.</value>
    public CompleteWizardStep CompleteStep
    {
      get
      {
        this.EnsureChildControls();
        return this.completeStep;
      }
    }

    /// <summary>Gets the confirm password entered by the user.</summary>
    /// <value>Gets the confirm password entered by the user.</value>
    public virtual string ConfirmPassword => this._confirmPassword != null ? this._confirmPassword : string.Empty;

    /// <summary>
    /// The URL to redirect to when the continue button is clicked.
    /// </summary>
    /// <value>The URL to redirect to when the continue button is clicked.</value>
    public virtual string ContinueDestinationPageUrl
    {
      get => (string) this.ViewState[nameof (ContinueDestinationPageUrl)] ?? string.Empty;
      set => this.ViewState[nameof (ContinueDestinationPageUrl)] = (object) value;
    }

    /// <summary>The create user WizardStep.</summary>
    /// <value>The create user WizardStep.</value>
    public CreateUserWizardStep CreateUserStep
    {
      get
      {
        this.EnsureChildControls();
        return this.createUserStep;
      }
    }

    /// <summary>
    /// Determines if the newly created user will be disabled.
    /// </summary>
    /// <value><c>true</c> if the newly created user will be disabled; otherwise, <c>false</c>.</value>
    public virtual bool DisableCreatedUser
    {
      get
      {
        object obj = this.ViewState[nameof (DisableCreatedUser)];
        return obj != null && (bool) obj;
      }
      set => this.ViewState[nameof (DisableCreatedUser)] = (object) value;
    }

    /// <summary>
    /// Text to be shown when a duplicate e-mail error is returned from create user.
    /// </summary>
    /// <value>Text to be shown when a duplicate e-mail error is returned from create user.</value>
    public virtual string DuplicateEmailErrorMessage
    {
      get => (string) this.ViewState[nameof (DuplicateEmailErrorMessage)] ?? Res.Get<ErrorMessages>().CreateUserWizardDefaultDuplicateEmailErrorMessage;
      set => this.ViewState[nameof (DuplicateEmailErrorMessage)] = (object) value;
    }

    /// <summary>
    /// Text to be shown when a duplicate username error is returned from create user.
    /// </summary>
    /// <value>Text to be shown when a duplicate username error is returned from create user.</value>
    public virtual string DuplicateUserNameErrorMessage
    {
      get => (string) this.ViewState[nameof (DuplicateUserNameErrorMessage)] ?? Res.Get<ErrorMessages>().CreateUserWizardDefaultDuplicateUserNameErrorMessage;
      set => this.ViewState[nameof (DuplicateUserNameErrorMessage)] = (object) value;
    }

    /// <summary>
    /// The text to be shown in the initial textbox for e-mail.
    /// </summary>
    /// <value>The text to be shown in the initial textbox for e-mail.</value>
    public virtual string Email
    {
      get => (string) this.ViewState[nameof (Email)] ?? string.Empty;
      set => this.ViewState[nameof (Email)] = (object) value;
    }

    private string EmailInternal
    {
      get
      {
        string email = this.Email;
        if (string.IsNullOrEmpty(email) && this.createUserStepContainer != null)
        {
          ITextControl emailTextBox = (ITextControl) this.createUserStepContainer.EmailTextBox;
          if (emailTextBox != null)
            return emailTextBox.Text;
        }
        return email;
      }
    }

    /// <summary>Text to be shown when the security answer is invalid.</summary>
    /// <value>Text to be shown when the security answer is invalid.</value>
    public virtual string InvalidAnswerErrorMessage
    {
      get => (string) this.ViewState[nameof (InvalidAnswerErrorMessage)] ?? Res.Get<ErrorMessages>().CreateUserWizardDefaultInvalidAnswerErrorMessage;
      set => this.ViewState[nameof (InvalidAnswerErrorMessage)] = (object) value;
    }

    /// <summary>The text to be shown when the e-mail is invalid.</summary>
    /// <value>The text to be shown when the e-mail is invalid.</value>
    public virtual string InvalidEmailErrorMessage
    {
      get => (string) this.ViewState[nameof (InvalidEmailErrorMessage)] ?? Res.Get<ErrorMessages>().CreateUserWizardDefaultInvalidEmailErrorMessage;
      set => this.ViewState[nameof (InvalidEmailErrorMessage)] = (object) value;
    }

    /// <summary>The text to be shown when the password is invalid.</summary>
    /// <value>The text to be shown when the password is invalid.</value>
    public virtual string InvalidPasswordErrorMessage
    {
      get => (string) this.ViewState[nameof (InvalidPasswordErrorMessage)] ?? Res.Get<ErrorMessages>().CreateUserWizardDefaultInvalidPasswordErrorMessage;
      set => this.ViewState[nameof (InvalidPasswordErrorMessage)] = (object) value;
    }

    /// <summary>
    /// Text to be shown when the security question is invalid.
    /// </summary>
    /// <value>Text to be shown when the security question is invalid.</value>
    public virtual string InvalidQuestionErrorMessage
    {
      get => (string) this.ViewState[nameof (InvalidQuestionErrorMessage)] ?? Res.Get<ErrorMessages>().CreateUserWizardDefaultInvalidQuestionErrorMessage;
      set => this.ViewState[nameof (InvalidQuestionErrorMessage)] = (object) value;
    }

    /// <summary>
    /// Determines if the newly created user will be logged into the site.
    /// </summary>
    /// <value><c>true</c> if the newly created user will be logged into the site; otherwise, <c>false</c>.</value>
    public virtual bool LoginCreatedUser
    {
      get
      {
        object obj = this.ViewState[nameof (LoginCreatedUser)];
        return obj == null || (bool) obj;
      }
      set => this.ViewState[nameof (LoginCreatedUser)] = (object) value;
    }

    /// <summary>
    /// The content and format of the e-mail message that contains the create user notification.
    /// </summary>
    /// <value>The content and format of the e-mail message that contains the create user notification.</value>
    [Themeable(false)]
    [NotifyParentProperty(true)]
    [PersistenceMode(PersistenceMode.InnerProperty)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public MailDefinition MailDefinition
    {
      get
      {
        if (this._mailDefinition == null)
        {
          this._mailDefinition = new MailDefinition();
          if (this.IsTrackingViewState)
            ((IStateManager) this._mailDefinition).TrackViewState();
        }
        return this._mailDefinition;
      }
    }

    /// <summary>The name of the membership provider.</summary>
    /// <value>The name of the membership provider.</value>
    public virtual string MembershipProvider
    {
      get => this.membershipProvider != null ? this.membershipProvider : string.Empty;
      set
      {
        if (!(this.MembershipProvider != value))
          return;
        this.membershipProvider = value;
        this.RequiresControlsRecreation();
      }
    }

    /// <summary>Gets the password entered by the user.</summary>
    /// <value>The password.</value>
    public virtual string Password => this._password != null ? this._password : string.Empty;

    private string PasswordInternal
    {
      get
      {
        string password = this.Password;
        if (string.IsNullOrEmpty(password) && !this.AutoGeneratePassword && this.createUserStepContainer != null)
        {
          ITextControl passwordTextBox = (ITextControl) this.createUserStepContainer.PasswordTextBox;
          if (passwordTextBox != null)
            return passwordTextBox.Text;
        }
        return password;
      }
    }

    /// <summary>
    /// Regular expression specification for valid new passwords.
    /// </summary>
    /// <value>Regular expression specification for valid new passwords.</value>
    public virtual string PasswordRegularExpression
    {
      get => (string) this.ViewState[nameof (PasswordRegularExpression)] ?? string.Empty;
      set => this.ViewState[nameof (PasswordRegularExpression)] = (object) value;
    }

    /// <summary>
    /// The text to be shown in the validation summary when the password does not match the regular expression.
    /// </summary>
    /// <value>The text to be shown in the validation summary when the password does not match the regular expression.</value>
    public virtual string PasswordRegularExpressionErrorMessage
    {
      get => (string) this.ViewState[nameof (PasswordRegularExpressionErrorMessage)] ?? Res.Get<ErrorMessages>().PasswordInvalidPasswordErrorMessage;
      set => this.ViewState[nameof (PasswordRegularExpressionErrorMessage)] = (object) value;
    }

    /// <summary>The initial value in the question textbox.</summary>
    /// <value>The initial value in the question textbox.</value>
    public virtual string Question
    {
      get => (string) this.ViewState[nameof (Question)] ?? string.Empty;
      set => this.ViewState[nameof (Question)] = (object) value;
    }

    /// <summary>
    /// Gets whether a password question is required by the used membership provider.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if the password question is required; otherwise, <c>false</c>.
    /// </value>
    protected bool QuestionAndAnswerRequired
    {
      get
      {
        if (!this.DesignMode)
          return new UserManager(this.MembershipProvider).RequiresQuestionAndAnswer;
        return this.CreateUserStep == null || this.CreateUserStep.ContentTemplate == null;
      }
    }

    private string QuestionInternal
    {
      get
      {
        string questionInternal = this.Question;
        if (string.IsNullOrEmpty(questionInternal) && this.createUserStepContainer != null)
        {
          ITextControl questionTextBox = (ITextControl) this.createUserStepContainer.QuestionTextBox;
          if (questionTextBox != null)
            questionInternal = questionTextBox.Text;
        }
        if (string.IsNullOrEmpty(questionInternal))
          questionInternal = (string) null;
        return questionInternal;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether an e-mail address is required for the Web site user.
    /// </summary>
    /// <value><c>true</c> if e-mail is required; otherwise, <c>false</c>.</value>
    public virtual bool RequireEmail
    {
      get
      {
        object obj = this.ViewState[nameof (RequireEmail)];
        return obj == null || (bool) obj;
      }
      set
      {
        if (this.RequireEmail == value)
          return;
        this.ViewState[nameof (RequireEmail)] = (object) value;
      }
    }

    /// <summary>The text that is displayed for unknown errors.</summary>
    /// <value>The text that is displayed for unknown errors.</value>
    public virtual string UnknownErrorMessage
    {
      get => (string) this.ViewState[nameof (UnknownErrorMessage)] ?? Res.Get<ErrorMessages>().CreateUserWizardDefaultUnknownErrorMessage;
      set => this.ViewState[nameof (UnknownErrorMessage)] = (object) value;
    }

    /// <summary>The initial value in the user name textbox.</summary>
    /// <value>The initial value in the user name textbox.</value>
    public virtual string UserName
    {
      get => (string) this.ViewState[nameof (UserName)] ?? string.Empty;
      set => this.ViewState[nameof (UserName)] = (object) value;
    }

    /// <summary>Gets the user name of the membership user.</summary>
    /// <value>The user name protected.</value>
    private string UserNameInternal
    {
      get
      {
        string userName = this.UserName;
        if (string.IsNullOrEmpty(userName) && this.createUserStepContainer != null)
        {
          ITextControl userNameTextBox = (ITextControl) this.createUserStepContainer.UserNameTextBox;
          if (userNameTextBox != null)
            return userNameTextBox.Text;
        }
        return userName;
      }
    }

    /// <summary>Gets or sets the validation group.</summary>
    /// <value>The validation group.</value>
    public string ValidationGroup
    {
      get
      {
        if (this.validationGroup == null)
        {
          this.EnsureID();
          this.validationGroup = this.ID;
        }
        return this.validationGroup;
      }
      set => this.validationGroup = value;
    }

    /// <summary>
    /// Gets a collection containing all the <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> objects that are defined for the control.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// A <see cref="T:System.Web.UI.WebControls.WizardStepCollection" /> representing all the <see cref="T:System.Web.UI.WebControls.WizardStepBase" /> objects defined for the <see cref="T:System.Web.UI.WebControls.Wizard" />.
    /// </returns>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public WizardStepCollection WizardSteps
    {
      get
      {
        if (this.wizardStepCollection == null)
          this.wizardStepCollection = new WizardStepCollection(this);
        return this.wizardStepCollection;
      }
    }

    private Stack History
    {
      get
      {
        if (this.historyStack == null)
          this.historyStack = new Stack();
        return this.historyStack;
      }
    }

    /// <summary>The container for Create User layout</summary>
    protected class CompleteStepContainer : GenericContainer
    {
      private IButtonControl continueButton;

      /// <summary>Gets or sets the continue button.</summary>
      /// <value>The continue button.</value>
      public IButtonControl ContinueButton
      {
        get
        {
          if (this.continueButton == null)
            this.continueButton = this.GetControl<IButtonControl>(nameof (ContinueButton), true);
          return this.continueButton;
        }
        set => this.continueButton = value;
      }
    }

    /// <summary>The container for the Complete layout</summary>
    protected class CreateUserStepContainer : GenericContainer
    {
      private RequiredFieldValidator answerRequired;
      private Control answerTextBox;
      private RequiredFieldValidator confirmPasswordRequired;
      private Control confirmPasswordTextBox;
      private CreateUserWizardForm createUserWizard;
      private RegularExpressionValidator emailRegExpValidator;
      private RequiredFieldValidator emailRequired;
      private Control emailTextBox;
      private CompareValidator passwordCompareValidator;
      private RegularExpressionValidator passwordRegExpValidator;
      private RequiredFieldValidator passwordRequired;
      private Control passwordTextBox;
      private RequiredFieldValidator questionRequired;
      private Control questionTextBox;
      private Control unknownErrorMessageLabel;
      private RequiredFieldValidator userNameRequired;
      private Control userNameTextBox;
      private ListControl providersList;
      private Control providersHolder;
      private PlaceHolder passwordsPlaceHolder;
      private PlaceHolder emailPlaceHolder;
      private PlaceHolder securityQuestionPlaceHolder;
      private IButtonControl hiddenSubmitButton;
      private IButtonControl createUserButton;

      /// <summary>
      /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.Web.UI.CreateUserWizardForm.CreateUserStepContainer" /> class.
      /// </summary>
      /// <param name="wizard">The wizard.</param>
      public CreateUserStepContainer(CreateUserWizardForm wizard) => this.createUserWizard = wizard;

      /// <summary>Gets the create user button.</summary>
      /// <value>The create user button.</value>
      public IButtonControl CreateUserButton
      {
        get
        {
          if (this.createUserButton == null)
            this.createUserButton = this.GetControl<IButtonControl>("StepNext", true);
          return this.createUserButton;
        }
      }

      /// <summary>Gets the hidden submit button.</summary>
      /// <value>The hidden submit button.</value>
      public IButtonControl HiddenSubmitButton
      {
        get
        {
          if (this.hiddenSubmitButton == null)
            this.hiddenSubmitButton = this.GetControl<IButtonControl>("hiddenSubmitButton", false);
          return this.hiddenSubmitButton;
        }
      }

      /// <summary>Gets the place holder for passwords text boxes.</summary>
      /// <value>Gets the place holder for passwords text boxes.</value>
      public PlaceHolder PasswordsPlaceHolder
      {
        get
        {
          if (this.passwordsPlaceHolder == null)
            this.passwordsPlaceHolder = this.GetControl<PlaceHolder>(nameof (PasswordsPlaceHolder), false);
          return this.passwordsPlaceHolder;
        }
      }

      /// <summary>Gets the place holder for email.</summary>
      /// <value>Gets the place holder for email.</value>
      public PlaceHolder EmailPlaceHolder
      {
        get
        {
          if (this.emailPlaceHolder == null)
            this.emailPlaceHolder = this.GetControl<PlaceHolder>(nameof (EmailPlaceHolder), false);
          return this.emailPlaceHolder;
        }
      }

      /// <summary>
      /// Gets the place holder for the security question/answer.
      /// </summary>
      /// <value>Gets the place holder for the security question/answer.</value>
      public PlaceHolder SecurityQuestionPlaceHolder
      {
        get
        {
          if (this.securityQuestionPlaceHolder == null)
            this.securityQuestionPlaceHolder = this.GetControl<PlaceHolder>(nameof (SecurityQuestionPlaceHolder), false);
          return this.securityQuestionPlaceHolder;
        }
      }

      /// <summary>Gets the providers list.</summary>
      /// <value>The providers list.</value>
      public ListControl ProvidersList
      {
        get
        {
          if (this.providersList == null)
            this.providersList = this.GetControl<ListControl>(nameof (ProvidersList), false);
          return this.providersList;
        }
      }

      /// <summary>Gets the place holder for providers list.</summary>
      /// <value>The place holder for providers list.</value>
      public Control ProvidersHolder
      {
        get
        {
          if (this.providersHolder == null)
            this.providersHolder = this.GetControl<Control>(nameof (ProvidersHolder), false);
          return this.providersHolder;
        }
      }

      /// <summary>Gets or sets the validator for security answer.</summary>
      /// <value>The answer required.</value>
      public RequiredFieldValidator AnswerRequired
      {
        get
        {
          if (this.answerRequired == null)
            this.answerRequired = this.GetControl<RequiredFieldValidator>(nameof (AnswerRequired), false);
          return this.answerRequired;
        }
        set => this.answerRequired = value;
      }

      /// <summary>Gets or sets the answer text box.</summary>
      /// <value>The answer text box.</value>
      public Control AnswerTextBox
      {
        get
        {
          if (this.answerTextBox != null)
            return this.answerTextBox;
          this.answerTextBox = this.GetControl<Control>("Answer", !this.createUserWizard.DesignMode && this.createUserWizard.QuestionAndAnswerRequired);
          return this.answerTextBox is IEditableTextControl ? this.answerTextBox : (Control) null;
        }
        set => this.answerTextBox = value;
      }

      /// <summary>
      /// Gets or sets the validator for confirm password text box.
      /// </summary>
      /// <value>The confirm password required.</value>
      public RequiredFieldValidator ConfirmPasswordRequired
      {
        get
        {
          if (this.confirmPasswordRequired == null)
            this.confirmPasswordRequired = this.GetControl<RequiredFieldValidator>(nameof (ConfirmPasswordRequired), false);
          return this.confirmPasswordRequired;
        }
        set => this.confirmPasswordRequired = value;
      }

      /// <summary>Gets or sets the confirm password text box.</summary>
      /// <value>The confirm password text box.</value>
      public Control ConfirmPasswordTextBox
      {
        get
        {
          if (this.confirmPasswordTextBox != null)
            return this.confirmPasswordTextBox;
          this.confirmPasswordTextBox = this.GetControl<Control>("ConfirmPassword", false);
          return this.confirmPasswordTextBox is IEditableTextControl ? this.confirmPasswordTextBox : (Control) null;
        }
        set => this.confirmPasswordTextBox = value;
      }

      /// <summary>Gets or sets the email regular expression validator.</summary>
      /// <value>The email reg exp validator.</value>
      public RegularExpressionValidator EmailRegExpValidator
      {
        get
        {
          if (this.emailRegExpValidator == null)
            this.emailRegExpValidator = this.GetControl<RegularExpressionValidator>("EmailRegExp", false);
          return this.emailRegExpValidator;
        }
        set => this.emailRegExpValidator = value;
      }

      /// <summary>Gets or sets the validator for email textbox.</summary>
      /// <value>The email required.</value>
      public RequiredFieldValidator EmailRequired
      {
        get
        {
          if (this.emailRequired == null)
            this.emailRequired = this.GetControl<RequiredFieldValidator>(nameof (EmailRequired), true);
          return this.emailRequired;
        }
        set => this.emailRequired = value;
      }

      /// <summary>Gets or sets the email text box.</summary>
      /// <value>The email text box.</value>
      public Control EmailTextBox
      {
        get
        {
          if (this.emailTextBox != null)
            return this.emailTextBox;
          this.emailTextBox = this.GetControl<Control>("Email", !this.createUserWizard.DesignMode && this.createUserWizard.RequireEmail);
          return this.emailTextBox is IEditableTextControl ? this.emailTextBox : (Control) null;
        }
        set => this.emailTextBox = value;
      }

      /// <summary>Gets or sets the error message label.</summary>
      /// <value>The error message label.</value>
      public Control ErrorMessageLabel
      {
        get
        {
          if (this.unknownErrorMessageLabel != null)
            return this.unknownErrorMessageLabel;
          this.unknownErrorMessageLabel = this.GetControl<Control>("ErrorMessage", true);
          return this.unknownErrorMessageLabel is ITextControl ? this.unknownErrorMessageLabel : (Control) null;
        }
        set => this.unknownErrorMessageLabel = value;
      }

      /// <summary>Gets or sets the password compare validator.</summary>
      /// <value>The password compare validator.</value>
      public CompareValidator PasswordCompareValidator
      {
        get
        {
          if (this.passwordCompareValidator == null)
            this.passwordCompareValidator = this.GetControl<CompareValidator>("PasswordCompare", true);
          return this.passwordCompareValidator;
        }
        set => this.passwordCompareValidator = value;
      }

      /// <summary>
      /// Gets or sets the password regular expression validator.
      /// </summary>
      /// <value>The password reg exp validator.</value>
      public RegularExpressionValidator PasswordRegExpValidator
      {
        get
        {
          if (this.passwordRegExpValidator == null)
            this.passwordRegExpValidator = this.GetControl<RegularExpressionValidator>("PasswordRegExp", false);
          return this.passwordRegExpValidator;
        }
        set => this.passwordRegExpValidator = value;
      }

      /// <summary>Gets or sets the validator for password textbox.</summary>
      /// <value>The password required.</value>
      public RequiredFieldValidator PasswordRequired
      {
        get
        {
          if (this.passwordRequired == null)
            this.passwordRequired = this.GetControl<RequiredFieldValidator>(nameof (PasswordRequired), false);
          return this.passwordRequired;
        }
        set => this.passwordRequired = value;
      }

      /// <summary>Gets or sets the password text box.</summary>
      /// <value>The password text box.</value>
      public Control PasswordTextBox
      {
        get
        {
          if (this.passwordTextBox != null)
            return this.passwordTextBox;
          this.passwordTextBox = this.GetControl<Control>("Password", !this.createUserWizard.DesignMode && !this.createUserWizard.AutoGeneratePassword);
          return this.passwordTextBox is IEditableTextControl ? this.passwordTextBox : (Control) null;
        }
        set => this.passwordTextBox = value;
      }

      /// <summary>Gets or sets the validator for question textbox.</summary>
      /// <value>The question required.</value>
      public RequiredFieldValidator QuestionRequired
      {
        get
        {
          if (this.questionRequired == null)
            this.questionRequired = this.GetControl<RequiredFieldValidator>(nameof (QuestionRequired), true);
          return this.questionRequired;
        }
        set => this.questionRequired = value;
      }

      /// <summary>Gets or sets the security question text box.</summary>
      /// <value>The question text box.</value>
      public Control QuestionTextBox
      {
        get
        {
          if (this.questionTextBox != null)
            return this.questionTextBox;
          this.questionTextBox = this.GetControl<Control>("Question", !this.createUserWizard.DesignMode && this.createUserWizard.QuestionAndAnswerRequired);
          return this.questionTextBox is IEditableTextControl ? this.questionTextBox : (Control) null;
        }
        set => this.questionTextBox = value;
      }

      /// <summary>Gets or sets the validator for user name.</summary>
      /// <value>The user name required.</value>
      public RequiredFieldValidator UserNameRequired
      {
        get
        {
          if (this.userNameRequired == null)
            this.userNameRequired = this.GetControl<RequiredFieldValidator>(nameof (UserNameRequired), true);
          return this.userNameRequired;
        }
        set => this.userNameRequired = value;
      }

      /// <summary>Gets or sets the user name text box.</summary>
      /// <value>The user name text box.</value>
      public Control UserNameTextBox
      {
        get
        {
          if (this.userNameTextBox != null)
            return this.userNameTextBox;
          this.userNameTextBox = this.GetControl<Control>("UserName", true);
          return this.userNameTextBox is IEditableTextControl ? this.userNameTextBox : (Control) null;
        }
        set => this.userNameTextBox = value;
      }
    }
  }
}
