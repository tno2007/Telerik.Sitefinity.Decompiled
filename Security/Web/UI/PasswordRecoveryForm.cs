// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.PasswordRecoveryForm
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Configuration.Provider;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Security;
using System.Web.UI;
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
  /// Represents the PasswordRecovery form for Sitefinity applications
  /// </summary>
  public class PasswordRecoveryForm : CompositeControl
  {
    private Control providersHolder;
    private ListControl providersList;
    private string _validationGroup;
    private GenericContainer container;
    private RequiredFieldValidator answerRequired;
    private Control answerTextBox;
    private Control question;
    private Control failureTextLabel;
    private System.Web.UI.WebControls.Image helpPageIcon;
    private HyperLink helpPageLink;
    private IButtonControl pushButton;
    private Control userNameTextBox;
    private RequiredFieldValidator userNameRequired;
    private IButtonControl hiddenSubmitButton;
    private string membershipProvider;
    private string _answer;
    private const string _answerID = "Answer";
    private const string _answerRequiredID = "AnswerRequired";
    /// <summary>The current shown view.</summary>
    protected PasswordRecoveryForm.View currentViewInternal;
    private const string _failureTextID = "FailureText";
    private const string _helpLinkID = "HelpLink";
    private MailDefinition _mailDefinition;
    private const string _pushButtonID = "SubmitButton";
    private string _question;
    /// <summary>Question view container.</summary>
    private const string _questionID = "Question";
    private ITemplate _questionTemplate;
    /// <summary>Success view container.</summary>
    private ITemplate _successTemplate;
    private string _userName;
    /// <summary>UserName view container.</summary>
    private const string _userNameID = "UserName";
    private const string _userNameRequiredID = "UserNameRequired";
    private ITemplate _userNameTemplate;
    private static readonly object EventAnswerLookupError = new object();
    private static readonly object EventSendingMail = new object();
    private static readonly object EventSendMailError = new object();
    private static readonly object EventUserLookupError = new object();
    private static readonly object EventVerifyingAnswer = new object();
    private static readonly object EventVerifyingUser = new object();
    /// <summary>The CommandName for the submit button.</summary>
    public static readonly string SubmitButtonCommandName = "Submit";
    /// <summary>The CommandName for the cancel button.</summary>
    public static readonly string CancelButtonCommandName = "Cancel";
    /// <summary>The CommandName for the continue button.</summary>
    public static readonly string ContinueButtonCommandName = "Continue";
    /// <summary>Specifies the name of the embeded Question template.</summary>
    public static readonly string QuestionTemplateName = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Security.PasswordRecoveryQuestionTemplate.ascx");
    /// <summary>Specifies the name of the embeded UserName template.</summary>
    public static readonly string UserNameTemplateName = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Security.PasswordRecoveryUserNameTemplate.ascx");
    /// <summary>Specifies the name of the embeded Success template.</summary>
    public static readonly string SuccessTemplateName = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Security.PasswordRecoverySuccessTemplate.ascx");

    /// <summary>Gets or sets the failure label.</summary>
    /// <value>The failure text label.</value>
    protected virtual Control FailureTextLabel
    {
      get
      {
        if (this.failureTextLabel == null)
          this.failureTextLabel = this.Container.GetControl<Control>("FailureText", false);
        return this.failureTextLabel;
      }
    }

    /// <summary>Gets or sets the help page icon.</summary>
    /// <value>The help page icon.</value>
    protected virtual System.Web.UI.WebControls.Image HelpPageIcon
    {
      get
      {
        if (this.helpPageIcon == null)
          this.helpPageIcon = this.Container.GetControl<System.Web.UI.WebControls.Image>();
        return this.helpPageIcon;
      }
    }

    /// <summary>Gets or sets the help page link.</summary>
    /// <value>The help page link.</value>
    protected virtual HyperLink HelpPageLink
    {
      get
      {
        if (this.helpPageLink == null)
          this.helpPageLink = this.Container.GetControl<HyperLink>("HelpLink", false);
        return this.helpPageLink;
      }
      set => this.helpPageLink = value;
    }

    /// <summary>Gets or sets the submit button.</summary>
    /// <value>The submit button.</value>
    protected virtual IButtonControl SubmitButton
    {
      get
      {
        if (this.pushButton == null)
          this.pushButton = this.Container.GetControl<IButtonControl>(nameof (SubmitButton), true);
        return this.pushButton;
      }
    }

    /// <summary>Gets the providers list.</summary>
    /// <value>The providers list.</value>
    protected virtual ListControl ProvidersList
    {
      get
      {
        if (this.providersList == null)
          this.providersList = this.Container.GetControl<ListControl>(nameof (ProvidersList), false);
        return this.providersList;
      }
    }

    /// <summary>Gets the place holder for providers list.</summary>
    /// <value>The place holder for providers list.</value>
    protected virtual Control ProvidersHolder
    {
      get
      {
        if (this.providersHolder == null)
          this.providersHolder = this.Container.GetControl<Control>(nameof (ProvidersHolder), false);
        return this.providersHolder;
      }
    }

    /// <summary>Gets or sets the user name text box.</summary>
    /// <value>The user name text box.</value>
    protected virtual Control UserNameTextBox
    {
      get
      {
        if (this.userNameTextBox == null)
          this.userNameTextBox = this.Container.GetControl<Control>("UserName", true);
        return this.userNameTextBox;
      }
    }

    /// <summary>Gets or sets the validator for the password answer.</summary>
    /// <value>The password answer validator.</value>
    protected virtual RequiredFieldValidator AnswerRequired
    {
      get
      {
        if (this.answerRequired == null)
          this.answerRequired = this.Container.GetControl<RequiredFieldValidator>(nameof (AnswerRequired), true);
        return this.answerRequired;
      }
    }

    /// <summary>Gets or sets the answer text box.</summary>
    /// <value>The answer text box.</value>
    protected virtual Control AnswerTextBox
    {
      get
      {
        if (this.answerTextBox == null)
          this.answerTextBox = this.Container.GetControl<Control>("Answer", true);
        return this.answerTextBox;
      }
    }

    /// <summary>Gets or sets the question.</summary>
    /// <value>The question.</value>
    protected virtual Control QuestionControl
    {
      get
      {
        if (this.question == null)
          this.question = this.Container.GetControl<Control>("Question", true);
        return this.question;
      }
    }

    /// <summary>
    /// Gets the hidden submit button. Used to allow Enter press working for all types of buttons (even for LinkButton).
    /// </summary>
    /// <value>The hidden submit button.</value>
    protected virtual IButtonControl HiddenSubmitButton
    {
      get
      {
        this.hiddenSubmitButton = this.hiddenSubmitButton ?? this.Container.GetControl<IButtonControl>("hiddenSubmitButton", true);
        return this.hiddenSubmitButton;
      }
    }

    /// <summary>Gets or sets the validator for the user name textbox.</summary>
    /// <value>The user name required.</value>
    public RequiredFieldValidator UserNameRequired
    {
      get
      {
        if (this.userNameRequired == null)
          this.userNameRequired = this.Container.GetControl<RequiredFieldValidator>(nameof (UserNameRequired), true);
        return this.userNameRequired;
      }
    }

    /// <summary>
    /// Gets or sets the continue destination page URL.Redirects to the specified url when user click on Continue button.
    /// </summary>
    /// <value>The continue destination page URL.</value>
    public virtual string ContinueDestinationPageUrl
    {
      get => (string) this.ViewState[nameof (ContinueDestinationPageUrl)] ?? string.Empty;
      set => this.ViewState[nameof (ContinueDestinationPageUrl)] = (object) value;
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
    /// Gets or sets the cancel destination page URL. User will be redirected to this page when press the Cancel button.
    /// </summary>
    /// <value>The cancel destination page URL.</value>
    public virtual string CancelDestinationPageUrl
    {
      get => (string) this.ViewState[nameof (CancelDestinationPageUrl)] ?? string.Empty;
      set => this.ViewState[nameof (CancelDestinationPageUrl)] = (object) value;
    }

    /// <summary>Gets or sets the general failure text.</summary>
    /// <value>The general failure text.</value>
    public virtual string GeneralFailureText
    {
      get => (string) this.ViewState[nameof (GeneralFailureText)] ?? Res.Get<ErrorMessages>().PasswordRecoveryDefaultGeneralFailureText;
      set => this.ViewState[nameof (GeneralFailureText)] = (object) value;
    }

    /// <summary>Gets or sets the help page icon URL.</summary>
    /// <value>The help page icon URL.</value>
    public virtual string HelpPageIconUrl
    {
      get => (string) this.ViewState[nameof (HelpPageIconUrl)] ?? string.Empty;
      set => this.ViewState[nameof (HelpPageIconUrl)] = (object) value;
    }

    /// <summary>Gets or sets the help page text.</summary>
    /// <value>The help page text.</value>
    public virtual string HelpPageText
    {
      get => (string) this.ViewState[nameof (HelpPageText)] ?? string.Empty;
      set => this.ViewState[nameof (HelpPageText)] = (object) value;
    }

    /// <summary>Gets or sets the help page URL.</summary>
    /// <value>The help page URL.</value>
    public virtual string HelpPageUrl
    {
      get => (string) this.ViewState[nameof (HelpPageUrl)] ?? string.Empty;
      set => this.ViewState[nameof (HelpPageUrl)] = (object) value;
    }

    /// <summary>
    /// Gets or sets the success page URL. Redirects to this url after successful password recovery.
    /// </summary>
    /// <value>The success page URL.</value>
    public virtual string SuccessPageUrl
    {
      get => (string) this.ViewState[nameof (SuccessPageUrl)] ?? string.Empty;
      set => this.ViewState[nameof (SuccessPageUrl)] = (object) value;
    }

    /// <summary>Gets or sets the user name failure text.</summary>
    /// <value>The user name failure text.</value>
    public virtual string UserNameFailureText => (string) this.ViewState[nameof (UserNameFailureText)] ?? Res.Get<ErrorMessages>().PasswordRecoveryDefaultUserNameFailureText;

    /// <summary>Gets or sets the password question.</summary>
    /// <value>The question.</value>
    [Filterable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [Themeable(false)]
    public string Question
    {
      get => this._question == null ? string.Empty : this._question;
      private set => this._question = value;
    }

    /// <summary>
    /// Gets the answer user entered in the password answer textbox.
    /// </summary>
    /// <value>The answer.</value>
    [Themeable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [Filterable(false)]
    public virtual string Answer => this._answer != null ? this._answer : string.Empty;

    /// <summary>Gets the mail details for the sending mail.</summary>
    /// <value>The mail definition.</value>
    [Themeable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [NotifyParentProperty(true)]
    [PersistenceMode(PersistenceMode.InnerProperty)]
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
      }
    }

    /// <summary>The username of currently authenticated user.</summary>
    public virtual string UserName
    {
      get => this._userName != null ? this._userName : string.Empty;
      set => this._userName = value;
    }

    /// <summary>Gets or sets the validation group of the control.</summary>
    /// <value>The validation group.</value>
    public string ValidationGroup
    {
      get => string.IsNullOrEmpty(this._validationGroup) ? this.UniqueID : this._validationGroup;
      set => this._validationGroup = value;
    }

    /// <summary>Used internally for answer value</summary>
    private string AnswerInternal
    {
      get
      {
        string answer = this.Answer;
        if (string.IsNullOrEmpty(answer))
        {
          ITextControl answerTextBox = (ITextControl) this.AnswerTextBox;
          if (answerTextBox != null && answerTextBox.Text != null)
            return answerTextBox.Text;
        }
        return answer;
      }
    }

    /// <summary>
    /// Current view. Possible views are: UserName, Question and Success
    /// </summary>
    private PasswordRecoveryForm.View CurrentView
    {
      get => this.currentViewInternal;
      set
      {
        if (value < PasswordRecoveryForm.View.UserName || value > PasswordRecoveryForm.View.Success)
          throw new ArgumentOutOfRangeException(nameof (value));
        if (value != this.CurrentView)
          this.currentViewInternal = value;
        this.RecreateChildControls();
      }
    }

    /// <summary>Used internally for username value</summary>
    private string UserNameInternal
    {
      get
      {
        string userName = this.UserName;
        return string.IsNullOrEmpty(userName) && this.UserNameTextBox is ITextControl userNameTextBox ? userNameTextBox.Text : userName;
      }
    }

    /// <summary>
    /// Gets or sets the path to a custom Question template for the control.
    /// </summary>
    [DescriptionResource(typeof (PageResources), "QuestionTemplateDescription")]
    [Browsable(false)]
    [PersistenceMode(PersistenceMode.InnerProperty)]
    [TemplateContainer(typeof (PasswordRecoveryForm))]
    public virtual ITemplate QuestionTemplate
    {
      get
      {
        if (this._questionTemplate == null)
          this._questionTemplate = this.CreateLayoutTemplate(this.QuestionTemplatePath, (string) null);
        return this._questionTemplate;
      }
      set
      {
        this._questionTemplate = value;
        this.ChildControlsCreated = false;
      }
    }

    /// <summary>
    /// Gets or sets the path to a custom UserName template for the control.
    /// </summary>
    [DescriptionResource(typeof (PageResources), "UserNameTemplateDescription")]
    [Browsable(false)]
    [PersistenceMode(PersistenceMode.InnerProperty)]
    [TemplateContainer(typeof (PasswordRecoveryForm))]
    public virtual ITemplate UserNameTemplate
    {
      get
      {
        if (this._userNameTemplate == null)
          this._userNameTemplate = this.CreateLayoutTemplate(this.UserNameTemplatePath, (string) null);
        return this._userNameTemplate;
      }
      set
      {
        this._userNameTemplate = value;
        this.ChildControlsCreated = false;
      }
    }

    /// <summary>
    /// Gets or sets the path to a custom Success template for the control.
    /// </summary>
    [DescriptionResource(typeof (PageResources), "SuccessTemplateDescription")]
    [Browsable(false)]
    [PersistenceMode(PersistenceMode.InnerProperty)]
    [TemplateContainer(typeof (PasswordRecoveryForm))]
    public virtual ITemplate SuccessTemplate
    {
      get
      {
        if (this._successTemplate == null)
          this._successTemplate = this.CreateLayoutTemplate(this.SuccessTemplatePath, (string) null);
        return this._successTemplate;
      }
      set
      {
        this._successTemplate = value;
        this.ChildControlsCreated = false;
      }
    }

    /// <summary>
    /// Gets or sets the path to a custom Question template template for the control.
    /// </summary>
    public virtual string QuestionTemplatePath
    {
      get => (string) this.ViewState[nameof (QuestionTemplatePath)] ?? PasswordRecoveryForm.QuestionTemplateName;
      set => this.ViewState[nameof (QuestionTemplatePath)] = (object) value;
    }

    /// <summary>
    /// Gets or sets the path to a custom UserName template template for the control.
    /// </summary>
    public virtual string UserNameTemplatePath
    {
      get => (string) this.ViewState[nameof (UserNameTemplatePath)] ?? PasswordRecoveryForm.UserNameTemplateName;
      set => this.ViewState[nameof (UserNameTemplatePath)] = (object) value;
    }

    /// <summary>
    /// Gets or sets the path to a custom Success template template for the control.
    /// </summary>
    public virtual string SuccessTemplatePath
    {
      get => (string) this.ViewState[nameof (SuccessTemplatePath)] ?? PasswordRecoveryForm.SuccessTemplateName;
      set => this.ViewState[nameof (SuccessTemplatePath)] = (object) value;
    }

    /// <summary>Gets the child controls container.</summary>
    /// <value>The container.</value>
    protected GenericContainer Container
    {
      get
      {
        if (this.container == null)
          this.container = new GenericContainer();
        return this.container;
      }
    }

    /// <summary>
    /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
    /// </summary>
    protected override void CreateChildControls()
    {
      if (this.Page != null && this.Page.Form != null)
        this.Page.Form.DefaultButton = "";
      this.container = (GenericContainer) null;
      this.providersHolder = (Control) null;
      this.providersList = (ListControl) null;
      this.answerRequired = (RequiredFieldValidator) null;
      this.answerTextBox = (Control) null;
      this.question = (Control) null;
      this.failureTextLabel = (Control) null;
      this.helpPageIcon = (System.Web.UI.WebControls.Image) null;
      this.helpPageLink = (HyperLink) null;
      this.pushButton = (IButtonControl) null;
      this.userNameTextBox = (Control) null;
      this.userNameRequired = (RequiredFieldValidator) null;
      this.hiddenSubmitButton = (IButtonControl) null;
      this.Controls.Clear();
      switch (this.CurrentView)
      {
        case PasswordRecoveryForm.View.UserName:
          this.CreateUserView();
          break;
        case PasswordRecoveryForm.View.Question:
          this.CreateQuestionView();
          break;
        case PasswordRecoveryForm.View.Success:
          this.CreateSuccessView();
          break;
      }
    }

    /// <summary>
    /// Restores control-state information from a previous page request that was saved by the <see cref="M:System.Web.UI.Control.SaveControlState" /> method.
    /// </summary>
    /// <param name="savedState">An <see cref="T:System.Object" /> that represents the control state to be restored.</param>
    protected override void LoadControlState(object savedState)
    {
      object[] objArray = (object[]) savedState;
      Triplet triplet = (Triplet) objArray[0];
      if (triplet != null)
      {
        if (triplet.First != null)
          base.LoadControlState(triplet.First);
        if (triplet.Second != null)
          this.CurrentView = (PasswordRecoveryForm.View) triplet.Second;
        if (triplet.Third != null)
          this._userName = (string) triplet.Third;
      }
      this.membershipProvider = (string) objArray[1];
      this.providersHolder = this.ProvidersHolder;
      this.providersList = this.ProvidersList;
      if (this.providersHolder == null || this.providersList == null)
        return;
      this.InitializeProvidersList(this.providersList, this.providersHolder);
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
          throw new ArgumentException("ViewState_InvalidViewState");
        base.LoadViewState(objArray[0]);
        if (objArray[1] == null)
          return;
        ((IStateManager) this.MailDefinition).LoadViewState(objArray[9]);
      }
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
      switch (this.CurrentView)
      {
        case PasswordRecoveryForm.View.UserName:
          this.SetUserNameChildProperties();
          break;
        case PasswordRecoveryForm.View.Question:
          this.SetQuestionChildProperties();
          break;
        case PasswordRecoveryForm.View.Success:
          if (this.Page != null && this.Page.Form != null)
          {
            this.Page.Form.DefaultButton = "";
            break;
          }
          break;
      }
      this.RenderContents(writer);
    }

    /// <summary>
    /// Saves any server control state changes that have occurred since the time the page was posted back to the server.
    /// </summary>
    /// <returns>
    /// Returns the server control's current state. If there is no state associated with the control, this method returns null.
    /// </returns>
    protected override object SaveControlState()
    {
      object x = base.SaveControlState();
      Triplet triplet;
      if (x == null && this.currentViewInternal == PasswordRecoveryForm.View.UserName && this._userName == null)
      {
        triplet = (Triplet) null;
      }
      else
      {
        object y = (object) null;
        object z = (object) null;
        if (this.currentViewInternal != PasswordRecoveryForm.View.UserName)
          y = (object) (int) this.currentViewInternal;
        if (this._userName != null && this.currentViewInternal != PasswordRecoveryForm.View.Success)
          z = (object) this._userName;
        triplet = new Triplet(x, y, z);
      }
      return (object) new object[2]
      {
        (object) triplet,
        (object) this.MembershipProvider
      };
    }

    /// <summary>
    /// Saves any state that was modified after the <see cref="M:System.Web.UI.WebControls.Style.TrackViewState" /> method was invoked.
    /// </summary>
    /// <returns>
    /// An object that contains the current view state of the control; otherwise, if there is no view state associated with the control, null.
    /// </returns>
    protected override object SaveViewState()
    {
      object[] objArray = new object[2]
      {
        base.SaveViewState(),
        this._mailDefinition != null ? ((IStateManager) this._mailDefinition).SaveViewState() : (object) null
      };
      for (int index = 0; index < objArray.Length; ++index)
      {
        if (objArray[index] != null)
          return (object) objArray;
      }
      return (object) null;
    }

    /// <summary>
    /// Causes the control to track changes to its view state so they can be stored in the object's <see cref="P:System.Web.UI.Control.ViewState" /> property.
    /// </summary>
    protected override void TrackViewState()
    {
      base.TrackViewState();
      if (this._mailDefinition == null)
        return;
      ((IStateManager) this._mailDefinition).TrackViewState();
    }

    /// <summary>
    /// Attempts to recover the password of user using the provided password answer.
    /// </summary>
    protected virtual void AttemptSendPasswordQuestionView()
    {
      UserManager manager = UserManager.GetManager(this.MembershipProvider);
      MembershipUser user = (MembershipUser) manager.GetUser(this.UserNameInternal);
      if (user != null)
      {
        if (user.IsLockedOut)
        {
          this.SetFailureTextLabel(this.GeneralFailureText);
        }
        else
        {
          this.Question = user.PasswordQuestion;
          if (string.IsNullOrEmpty(this.Question))
          {
            this.SetFailureTextLabel(this.GeneralFailureText);
          }
          else
          {
            LoginCancelEventArgs e = new LoginCancelEventArgs();
            this.OnVerifyingAnswer(e);
            if (e.Cancel)
              return;
            string failureText = (string) null;
            string answerInternal = this.AnswerInternal;
            string password = (string) null;
            if (string.IsNullOrEmpty(user.Email))
            {
              this.SetFailureTextLabel(this.GeneralFailureText);
            }
            else
            {
              if (manager.EnablePasswordRetrieval)
              {
                try
                {
                  password = user.GetPassword(answerInternal);
                }
                catch (ProviderException ex)
                {
                  password = (string) null;
                  failureText = ex.Message;
                }
              }
              else if (!manager.EnablePasswordReset)
              {
                failureText = Res.Get<ErrorMessages>().PasswordRecoveryRecoveryNotSupported;
              }
              else
              {
                try
                {
                  password = manager.ResetPassword(user.UserName, answerInternal);
                }
                catch (ProviderException ex)
                {
                  password = (string) null;
                  failureText = ex.Message;
                }
              }
              if (password != null)
              {
                MailMessage passwordMail = EmailSender.CreatePasswordMail(manager.RecoveryMailAddress, user.Email, user.UserName, password, manager.RecoveryMailSubject, manager.RecoveryMailBody);
                EmailSender emailSender = EmailSender.Get();
                emailSender.SenderProfileName = Config.Get<SecurityConfig>().Notifications.SenderProfile;
                emailSender.Sending += (EventHandler<MailMessageEventArgs>) ((s, args) => this.OnSendingMail(args));
                emailSender.Error += (EventHandler<SendMailErrorEventArgs>) ((s, args) => this.OnSendMailError(args));
                emailSender.TrySend(passwordMail);
                this.PerformSuccessAction();
              }
              else
              {
                this.OnAnswerLookupError(EventArgs.Empty);
                if (string.IsNullOrEmpty(failureText))
                  failureText = Res.Get<ErrorMessages>().PasswordRecoveryDefaultQuestionFailureText;
                this.SetFailureTextLabel(failureText);
              }
            }
          }
        }
      }
      else
        this.SetFailureTextLabel(this.GeneralFailureText);
    }

    /// <summary>Creates the user view.</summary>
    protected virtual void CreateUserView()
    {
      GenericContainer container = this.Container;
      container.ID = "UserNameContainerID";
      this.UserNameTemplate.InstantiateIn((Control) this.container);
      this.Controls.Add((Control) container);
      this.SetUserNameEditableChildProperties();
      if (this.UserNameTextBox is IEditableTextControl userNameTextBox)
        userNameTextBox.TextChanged += new EventHandler(this.UserNameTextChanged);
      this.UserNameRequired.ValidationGroup = this.ValidationGroup;
      this.SubmitButton.ValidationGroup = this.ValidationGroup;
      this.SubmitButton.CommandName = PasswordRecoveryForm.SubmitButtonCommandName;
      this.providersHolder = this.ProvidersHolder;
      this.providersList = this.ProvidersList;
      if (this.providersHolder != null && this.providersList != null)
        this.InitializeProvidersList(this.providersList, this.providersHolder);
      if (this.Page == null || this.Page.Form == null || this.HiddenSubmitButton == null)
        return;
      this.Page.Form.DefaultButton = ((Control) this.HiddenSubmitButton).UniqueID;
      this.HiddenSubmitButton.CommandName = PasswordRecoveryForm.SubmitButtonCommandName;
    }

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
            foreach (MembershipDataProvider provider in contextProviders)
            {
              if (LoginUtils.IsProviderSetUpForPasswordRecovery(provider))
              {
                ListItem listItem = new ListItem(provider.Title, provider.Name);
                listItem.Selected = false;
                if (string.IsNullOrEmpty(this.MembershipProvider) && defaultProviderName == provider.Name || provider.Name == this.MembershipProvider)
                  listItem.Selected = true;
                providersListLocal.Items.Add(listItem);
              }
            }
            this.MembershipProvider = providersListLocal.SelectedValue;
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
    /// Depending on current view, redirects to method that attempts to send the password
    /// </summary>
    private void AttemptSendPassword()
    {
      if (this.Page != null && !this.Page.IsValid)
        return;
      if (this.CurrentView == PasswordRecoveryForm.View.UserName)
      {
        this.AttemptSendPasswordUserNameView();
      }
      else
      {
        if (this.CurrentView != PasswordRecoveryForm.View.Question)
          return;
        this.AttemptSendPasswordQuestionView();
      }
    }

    /// <summary>
    /// Attempt to send a password to user using only username when question and answer are not required
    /// Checks if question and answer are required:
    /// -&gt; if yes, change view to Question (other template)
    /// -&gt; if no, check for EnablePasswordRetrieval property; then for EnablePasswordReset
    /// </summary>
    private void AttemptSendPasswordUserNameView()
    {
      LoginCancelEventArgs e = new LoginCancelEventArgs();
      this.OnVerifyingUser(e);
      if (e.Cancel)
        return;
      string failureText = (string) null;
      UserManager manager = UserManager.GetManager(this.MembershipProvider);
      MembershipUser user = (MembershipUser) manager.GetUser(this.UserNameInternal);
      if (user != null)
      {
        if (user.IsLockedOut)
          this.SetFailureTextLabel(this.UserNameFailureText);
        else if (manager.RequiresQuestionAndAnswer)
        {
          this.Question = user.PasswordQuestion;
          if (string.IsNullOrEmpty(this.Question))
            this.SetFailureTextLabel(this.GeneralFailureText);
          else
            this.CurrentView = PasswordRecoveryForm.View.Question;
        }
        else
        {
          string password = (string) null;
          string email = user.Email;
          if (string.IsNullOrEmpty(email))
          {
            this.SetFailureTextLabel(this.GeneralFailureText);
          }
          else
          {
            if (manager.EnablePasswordRetrieval)
            {
              try
              {
                password = user.GetPassword();
              }
              catch (ProviderException ex)
              {
                password = (string) null;
                failureText = ex.Message;
              }
            }
            else if (!manager.EnablePasswordReset)
            {
              failureText = Res.Get<ErrorMessages>().PasswordRecoveryRecoveryNotSupported;
            }
            else
            {
              try
              {
                password = manager.ResetPassword(user.UserName, string.Empty);
                manager.Provider.SuppressSecurityChecks = true;
                manager.SaveChanges();
                manager.Provider.SuppressSecurityChecks = false;
              }
              catch (ProviderException ex)
              {
                password = (string) null;
                failureText = ex.Message;
              }
            }
            if (password != null)
            {
              string recoveryMail = manager.RecoveryMailAddress;
              if (string.IsNullOrEmpty(recoveryMail))
              {
                recoveryMail = ConfigurationManager.AppSettings[this.MembershipProvider + "_RecoveryMailAddress"];
                if (string.IsNullOrEmpty(recoveryMail))
                  recoveryMail = string.Empty;
              }
              string subject = manager.RecoveryMailSubject;
              if (string.IsNullOrEmpty(subject))
              {
                subject = ConfigurationManager.AppSettings[this.MembershipProvider + "_RecoveryMailSubject"];
                if (string.IsNullOrEmpty(subject))
                  subject = Res.Get<Labels>().PasswordRecovery;
              }
              string body = manager.RecoveryMailBody;
              if (string.IsNullOrEmpty(body))
              {
                body = ConfigurationManager.AppSettings[this.MembershipProvider + "_RecoveryMailBody"];
                if (string.IsNullOrEmpty(body))
                  body = Res.Get<ErrorMessages>().CreateUserWizardDefaultBody;
              }
              MailMessage passwordMail = EmailSender.CreatePasswordMail(recoveryMail, email, user.UserName, password, subject, body);
              EmailSender emailSender = EmailSender.Get();
              emailSender.SenderProfileName = Config.Get<SecurityConfig>().Notifications.SenderProfile;
              emailSender.Sending += (EventHandler<MailMessageEventArgs>) ((s, args) => this.OnSendingMail(args));
              emailSender.Error += (EventHandler<SendMailErrorEventArgs>) ((s, args) => this.OnSendMailError(args));
              emailSender.TrySend(passwordMail);
              this.PerformSuccessAction();
            }
            else
            {
              if (string.IsNullOrEmpty(failureText))
                failureText = this.GeneralFailureText;
              this.SetFailureTextLabel(failureText);
            }
          }
        }
      }
      else
      {
        this.OnUserLookupError(EventArgs.Empty);
        this.SetFailureTextLabel(this.UserNameFailureText);
      }
    }

    /// <summary>
    /// Create the view for question and answer when in Question view
    /// </summary>
    private void CreateQuestionView()
    {
      this.container = this.Container;
      this.container.ID = "QuestionContainerID";
      this.QuestionTemplate.InstantiateIn((Control) this.container);
      this.Controls.Add((Control) this.container);
      if (this.AnswerTextBox is IEditableTextControl answerTextBox)
        answerTextBox.TextChanged += new EventHandler(this.AnswerTextChanged);
      this.SubmitButton.ValidationGroup = this.ValidationGroup;
      this.SubmitButton.CommandName = PasswordRecoveryForm.SubmitButtonCommandName;
      this.AnswerRequired.ValidationGroup = this.ValidationGroup;
      if (this.Page == null || this.Page.Form == null || this.HiddenSubmitButton == null)
        return;
      this.Page.Form.DefaultButton = ((Control) this.HiddenSubmitButton).UniqueID;
      this.HiddenSubmitButton.CommandName = PasswordRecoveryForm.SubmitButtonCommandName;
    }

    /// <summary>Create the final view for successful operation</summary>
    private void CreateSuccessView()
    {
      this.container = this.Container;
      this.SuccessTemplate.InstantiateIn((Control) this.container);
      this.Controls.Add((Control) this.container);
    }

    private void SetFailureTextLabel(string failureText)
    {
      ITextControl failureTextLabel = (ITextControl) this.FailureTextLabel;
      if (failureTextLabel == null)
        return;
      failureTextLabel.Text = failureText;
    }

    private void SetUserNameChildProperties() => this.SetUserNameDefaultChildProperties();

    private void SetUserNameDefaultChildProperties()
    {
      WebControl userNameTextBox = (WebControl) this.UserNameTextBox;
      userNameTextBox.TabIndex = this.TabIndex;
      userNameTextBox.AccessKey = this.AccessKey;
      bool flag1 = this.CurrentView == PasswordRecoveryForm.View.UserName;
      RequiredFieldValidator userNameRequired = this.UserNameRequired;
      userNameRequired.Enabled = flag1;
      userNameRequired.Visible = flag1;
      WebControl submitButton = this.SubmitButton as WebControl;
      submitButton.Visible = true;
      submitButton.TabIndex = this.TabIndex;
      HyperLink helpPageLink = this.HelpPageLink;
      string helpPageText = this.HelpPageText;
      System.Web.UI.WebControls.Image helpPageIcon = this.HelpPageIcon;
      if (helpPageLink != null)
      {
        if (helpPageText.Length > 0)
        {
          helpPageLink.Text = helpPageText;
          helpPageLink.NavigateUrl = this.HelpPageUrl;
          helpPageLink.Visible = true;
          helpPageLink.TabIndex = this.TabIndex;
        }
        else
          helpPageLink.Visible = false;
      }
      string helpPageIconUrl = this.HelpPageIconUrl;
      bool flag2 = helpPageIconUrl.Length > 0;
      if (helpPageIcon != null)
      {
        helpPageIcon.Visible = flag2;
        if (flag2)
        {
          helpPageIcon.ImageUrl = helpPageIconUrl;
          helpPageIcon.AlternateText = helpPageText;
        }
      }
      Control failureTextLabel = this.FailureTextLabel;
      if (((ITextControl) failureTextLabel).Text.Length > 0)
        failureTextLabel.Visible = true;
      else
        failureTextLabel.Visible = false;
    }

    private void SetQuestionChildProperties() => this.SetQuestionCommonChildProperties();

    private void SetQuestionCommonChildProperties()
    {
      ITextControl userNameTextBox = (ITextControl) this.UserNameTextBox;
      if (userNameTextBox != null)
        userNameTextBox.Text = HttpUtility.HtmlEncode(this.UserNameInternal);
      ITextControl questionControl = (ITextControl) this.QuestionControl;
      if (questionControl != null)
        questionControl.Text = HttpUtility.HtmlEncode(this.Question);
      ITextControl answerTextBox = (ITextControl) this.AnswerTextBox;
      if (answerTextBox == null)
        return;
      answerTextBox.Text = string.Empty;
    }

    /// <summary>
    /// Set the username textbox to appear as editable in UserName view
    /// </summary>
    private void SetUserNameEditableChildProperties()
    {
      string userNameInternal = this.UserNameInternal;
      if (userNameInternal.Length <= 0)
        return;
      ITextControl userNameTextBox = (ITextControl) this.UserNameTextBox;
      if (userNameTextBox == null)
        return;
      userNameTextBox.Text = userNameInternal;
    }

    /// <summary>Occurs when answer lookup error is occurred.</summary>
    public event EventHandler AnswerLookupError
    {
      add => this.Events.AddHandler(PasswordRecoveryForm.EventAnswerLookupError, (Delegate) value);
      remove => this.Events.RemoveHandler(PasswordRecoveryForm.EventAnswerLookupError, (Delegate) value);
    }

    /// <summary>Occurs before sending the email.</summary>
    public event MailMessageEventHandler SendingMail
    {
      add => this.Events.AddHandler(PasswordRecoveryForm.EventSendingMail, (Delegate) value);
      remove => this.Events.RemoveHandler(PasswordRecoveryForm.EventSendingMail, (Delegate) value);
    }

    /// <summary>Occurs when send mail error is occured.</summary>
    public event SendMailErrorEventHandler SendMailError
    {
      add => this.Events.AddHandler(PasswordRecoveryForm.EventSendMailError, (Delegate) value);
      remove => this.Events.RemoveHandler(PasswordRecoveryForm.EventSendMailError, (Delegate) value);
    }

    /// <summary>
    /// Occurs when user lookup error is occurred. The membership user cannot be found.
    /// </summary>
    public event EventHandler UserLookupError
    {
      add => this.Events.AddHandler(PasswordRecoveryForm.EventUserLookupError, (Delegate) value);
      remove => this.Events.RemoveHandler(PasswordRecoveryForm.EventUserLookupError, (Delegate) value);
    }

    /// <summary>Occurs before verifying the password answer.</summary>
    public event LoginCancelEventHandler VerifyingAnswer
    {
      add => this.Events.AddHandler(PasswordRecoveryForm.EventVerifyingAnswer, (Delegate) value);
      remove => this.Events.RemoveHandler(PasswordRecoveryForm.EventVerifyingAnswer, (Delegate) value);
    }

    /// <summary>Occurs before verifying user.</summary>
    public event LoginCancelEventHandler VerifyingUser
    {
      add => this.Events.AddHandler(PasswordRecoveryForm.EventVerifyingUser, (Delegate) value);
      remove => this.Events.RemoveHandler(PasswordRecoveryForm.EventVerifyingUser, (Delegate) value);
    }

    /// <summary>
    /// Raises the <see cref="E:AnswerLookupError" /> event.
    /// </summary>
    /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
    protected virtual void OnAnswerLookupError(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[PasswordRecoveryForm.EventAnswerLookupError];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      if (this.Page != null)
      {
        if (this.CurrentView == PasswordRecoveryForm.View.UserName)
          this.Page.SetFocus(this.UserNameTextBox);
        else if (this.CurrentView == PasswordRecoveryForm.View.Question)
          this.Page.SetFocus(this.AnswerTextBox);
      }
      base.OnPreRender(e);
      switch (this.CurrentView)
      {
        case PasswordRecoveryForm.View.UserName:
          this.SetUserNameEditableChildProperties();
          break;
      }
    }

    /// <summary>Called when bubble event is fired.</summary>
    /// <param name="source">The source.</param>
    /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
    /// <returns></returns>
    protected override bool OnBubbleEvent(object source, EventArgs e)
    {
      bool flag = false;
      if (e is CommandEventArgs)
      {
        CommandEventArgs commandEventArgs = (CommandEventArgs) e;
        if (commandEventArgs.CommandName.Equals(PasswordRecoveryForm.SubmitButtonCommandName, StringComparison.CurrentCultureIgnoreCase))
        {
          this.AttemptSendPassword();
          flag = true;
        }
        else if (commandEventArgs.CommandName.Equals(PasswordRecoveryForm.CancelButtonCommandName, StringComparison.CurrentCultureIgnoreCase))
          this.CancelButtonPressed();
        else if (commandEventArgs.CommandName.Equals(PasswordRecoveryForm.ContinueButtonCommandName, StringComparison.CurrentCultureIgnoreCase))
          this.ContinueButtonPressed();
      }
      return flag;
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      this.Page.RegisterRequiresControlState((Control) this);
      this.Page.LoadComplete += new EventHandler(this.OnPageLoadComplete);
    }

    /// <summary>
    /// Raises the <see cref="E:SendingMail" /> event.
    /// </summary>
    /// <param name="e">The <see cref="T:System.Web.UI.WebControls.MailMessageEventArgs" /> instance containing the event data.</param>
    protected virtual void OnSendingMail(MailMessageEventArgs e)
    {
      MailMessageEventHandler messageEventHandler = (MailMessageEventHandler) this.Events[PasswordRecoveryForm.EventSendingMail];
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
      SendMailErrorEventHandler errorEventHandler = (SendMailErrorEventHandler) this.Events[PasswordRecoveryForm.EventSendMailError];
      if (errorEventHandler == null)
        return;
      errorEventHandler((object) this, e);
    }

    /// <summary>
    /// Raises the <see cref="E:UserLookupError" /> event.
    /// </summary>
    /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
    protected virtual void OnUserLookupError(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[PasswordRecoveryForm.EventUserLookupError];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    /// <summary>
    /// Raises the <see cref="E:VerifyingAnswer" /> event.
    /// </summary>
    /// <param name="e">The <see cref="T:System.Web.UI.WebControls.LoginCancelEventArgs" /> instance containing the event data.</param>
    protected virtual void OnVerifyingAnswer(LoginCancelEventArgs e)
    {
      LoginCancelEventHandler cancelEventHandler = (LoginCancelEventHandler) this.Events[PasswordRecoveryForm.EventVerifyingAnswer];
      if (cancelEventHandler == null)
        return;
      cancelEventHandler((object) this, e);
    }

    /// <summary>
    /// Raises the <see cref="E:VerifyingUser" /> event.
    /// </summary>
    /// <param name="e">The <see cref="T:System.Web.UI.WebControls.LoginCancelEventArgs" /> instance containing the event data.</param>
    protected virtual void OnVerifyingUser(LoginCancelEventArgs e)
    {
      LoginCancelEventHandler cancelEventHandler = (LoginCancelEventHandler) this.Events[PasswordRecoveryForm.EventVerifyingUser];
      if (cancelEventHandler == null)
        return;
      cancelEventHandler((object) this, e);
    }

    /// <summary>On entered password answer</summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    private void AnswerTextChanged(object source, EventArgs e) => this._answer = ((ITextControl) source).Text;

    /// <summary>On entered username value</summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    private void UserNameTextChanged(object source, EventArgs e) => this.UserName = ((ITextControl) source).Text;

    /// <summary>On changed value in Providers list</summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ProvidersList_SelectedIndexChanged(object sender, EventArgs e) => this.MembershipProvider = ((ListControl) sender).SelectedValue;

    private void ContinueButtonPressed()
    {
      string destinationPageUrl = this.ContinueDestinationPageUrl;
      if (this.Page == null)
        return;
      if (!RouteHelper.IsAbsoluteUrl(destinationPageUrl) && !string.IsNullOrEmpty(destinationPageUrl))
      {
        this.Page.Response.Redirect(this.ResolveClientUrl(destinationPageUrl), false);
      }
      else
      {
        string str = this.Page.Request.QueryStringGet(SecurityManager.AuthenticationReturnUrl);
        if (!RouteHelper.IsAbsoluteUrl(str) && !string.IsNullOrEmpty(str))
          this.Page.Response.Redirect(SystemManager.CurrentHttpContext.Server.UrlDecode(str), true);
        else
          this.Page.Response.Redirect("~/Sitefinity/Login", true);
      }
    }

    private void CancelButtonPressed()
    {
      if (this.Page == null)
        return;
      string destinationPageUrl = this.CancelDestinationPageUrl;
      if (!RouteHelper.IsAbsoluteUrl(destinationPageUrl) && !string.IsNullOrEmpty(destinationPageUrl))
      {
        this.Page.Response.Redirect(this.ResolveClientUrl(destinationPageUrl), false);
      }
      else
      {
        string str = this.Page.Request.QueryStringGet(SecurityManager.AuthenticationReturnUrl);
        if (!RouteHelper.IsAbsoluteUrl(str) && !string.IsNullOrEmpty(str))
          this.Page.Response.Redirect(HttpUtility.UrlDecode(str), true);
        else
          this.Page.Response.Redirect("~/Sitefinity/Login", true);
      }
    }

    /// <summary>Called when page load complete.</summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
    private void OnPageLoadComplete(object sender, EventArgs e)
    {
      if (this.Page != null && !this.Page.IsPostBack && !string.IsNullOrEmpty(this.Page.Request.QueryStringGet("provider")))
        this.MembershipProvider = this.Page.Request.QueryStringGet("provider");
      if (this.CurrentView != PasswordRecoveryForm.View.Question || !string.IsNullOrEmpty(this.Question))
        return;
      MembershipUser user = (MembershipUser) new UserManager(this.MembershipProvider).GetUser(this.UserNameInternal);
      if (user != null)
      {
        this.Question = user.PasswordQuestion;
        if (!string.IsNullOrEmpty(this.Question))
          return;
        this.SetFailureTextLabel(this.GeneralFailureText);
      }
      else
        this.SetFailureTextLabel(this.GeneralFailureText);
    }

    private void PerformSuccessAction()
    {
      string successPageUrl = this.SuccessPageUrl;
      if (!RouteHelper.IsAbsoluteUrl(successPageUrl) && !string.IsNullOrEmpty(successPageUrl))
        this.Page.Response.Redirect(this.ResolveClientUrl(successPageUrl), false);
      else
        this.CurrentView = PasswordRecoveryForm.View.Success;
    }

    /// <summary>Defines different views of this control.</summary>
    protected enum View
    {
      /// <summary>UserName view - user must enter its username.</summary>
      UserName,
      /// <summary>
      /// Question view - if needed user must answer to password question.
      /// </summary>
      Question,
      /// <summary>
      /// Success view - user received email and success screen is shown.
      /// </summary>
      Success,
    }
  }
}
