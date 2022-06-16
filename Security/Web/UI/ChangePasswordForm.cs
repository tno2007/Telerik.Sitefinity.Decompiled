// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.ChangePasswordForm
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
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
  /// Login control that allows changing of password for a membership user.
  /// </summary>
  public class ChangePasswordForm : CompositeControl, INamingContainer
  {
    private ChangePasswordForm.ChangePasswordContainer _changePasswordContainer;
    private ITemplate _changePasswordTemplate;
    private string _confirmNewPassword;
    private ChangePasswordForm.View _currentView;
    private MailDefinition _mailDefinition;
    private string _newPassword;
    private string _password;
    private ChangePasswordForm.SuccessContainer _successContainer;
    private ITemplate _successTemplate;
    private string _userName;
    /// <summary>The command name of the Cancel button.</summary>
    public static readonly string CancelButtonCommandName = "Cancel";
    /// <summary>The command name of the Change Password button.</summary>
    public static readonly string ChangePasswordButtonCommandName = "ChangePassword";
    /// <summary>The command name of the Continue button.</summary>
    public static readonly string ContinueButtonCommandName = "Continue";
    private static readonly object EventCancelButtonClick = new object();
    private static readonly object EventChangedPassword = new object();
    private static readonly object EventChangePasswordError = new object();
    private static readonly object EventChangingPassword = new object();
    private static readonly object EventContinueButtonClick = new object();
    private static readonly object EventSendingMail = new object();
    private static readonly object EventSendMailError = new object();
    private Control providersHolder;
    private ListControl providersList;
    private string membershipProvider;
    /// <summary>
    /// Specifies the name of the embeded ChangePassword template.
    /// </summary>
    public static readonly string ChangePasswordPath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Security.ChangePassword_ChangePasswordTemplate.ascx");
    /// <summary>Specifies the name of the embeded Success template.</summary>
    public static readonly string SuccessPath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Security.ChangePassword_SuccessTemplate.ascx");

    /// <summary>Occurs when cancel button is clicked.</summary>
    public event EventHandler CancelButtonClick
    {
      add => this.Events.AddHandler(ChangePasswordForm.EventCancelButtonClick, (Delegate) value);
      remove => this.Events.RemoveHandler(ChangePasswordForm.EventCancelButtonClick, (Delegate) value);
    }

    /// <summary>Occurs when changed password button is clicked.</summary>
    public event EventHandler ChangedPassword
    {
      add => this.Events.AddHandler(ChangePasswordForm.EventChangedPassword, (Delegate) value);
      remove => this.Events.RemoveHandler(ChangePasswordForm.EventChangedPassword, (Delegate) value);
    }

    /// <summary>Occurs when change password error occurs.</summary>
    public event EventHandler ChangePasswordError
    {
      add => this.Events.AddHandler(ChangePasswordForm.EventChangePasswordError, (Delegate) value);
      remove => this.Events.RemoveHandler(ChangePasswordForm.EventChangePasswordError, (Delegate) value);
    }

    /// <summary>Occurs before changing of the password.</summary>
    public event LoginCancelEventHandler ChangingPassword
    {
      add => this.Events.AddHandler(ChangePasswordForm.EventChangingPassword, (Delegate) value);
      remove => this.Events.RemoveHandler(ChangePasswordForm.EventChangingPassword, (Delegate) value);
    }

    /// <summary>Occurs when continue button is clicked.</summary>
    public event EventHandler ContinueButtonClick
    {
      add => this.Events.AddHandler(ChangePasswordForm.EventContinueButtonClick, (Delegate) value);
      remove => this.Events.RemoveHandler(ChangePasswordForm.EventContinueButtonClick, (Delegate) value);
    }

    /// <summary>Occurs before sending of mail.</summary>
    public event MailMessageEventHandler SendingMail
    {
      add => this.Events.AddHandler(ChangePasswordForm.EventSendingMail, (Delegate) value);
      remove => this.Events.RemoveHandler(ChangePasswordForm.EventSendingMail, (Delegate) value);
    }

    /// <summary>Occurs when error is occured while sending the email.</summary>
    public event SendMailErrorEventHandler SendMailError
    {
      add => this.Events.AddHandler(ChangePasswordForm.EventSendMailError, (Delegate) value);
      remove => this.Events.RemoveHandler(ChangePasswordForm.EventSendMailError, (Delegate) value);
    }

    /// <summary>Attempts to change password.</summary>
    private void AttemptChangePassword()
    {
      if (this.Page != null && !this.Page.IsValid)
        return;
      LoginCancelEventArgs e = new LoginCancelEventArgs();
      this.OnChangingPassword(e);
      if (e.Cancel)
        return;
      UserManager userManager = new UserManager(this.MembershipProvider);
      MembershipUser user = (MembershipUser) userManager.GetUser(this.UserNameInternal);
      string passwordInternal = this.NewPasswordInternal;
      if (user != null)
      {
        bool flag;
        try
        {
          flag = userManager.ChangePassword(user.UserName, this.CurrentPasswordInternal, passwordInternal);
          userManager.Provider.SuppressSecurityChecks = true;
          userManager.SaveChanges();
          userManager.Provider.SuppressSecurityChecks = false;
        }
        catch (ArgumentException ex)
        {
          flag = false;
          this.SetFailureTextLabel(this._changePasswordContainer, ex.Message);
        }
        if (flag)
        {
          if (flag && user.IsApproved && !user.IsLockedOut)
            FormsAuthentication.SetAuthCookie(this.UserNameInternal, false);
          this.OnChangedPassword(EventArgs.Empty);
          this.PerformSuccessAction(user.UserName, passwordInternal);
        }
        else
          this.SetFailureTextLabel(this._changePasswordContainer, string.Format((IFormatProvider) CultureInfo.CurrentCulture, this.ChangePasswordFailureText, new object[2]
          {
            (object) userManager.MinRequiredPasswordLength,
            (object) userManager.MinRequiredNonAlphanumericCharacters
          }));
      }
      else
      {
        this.OnChangePasswordError(EventArgs.Empty);
        string str = this.ChangePasswordFailureText;
        if (!string.IsNullOrEmpty(str))
          str = string.Format((IFormatProvider) CultureInfo.CurrentCulture, str, new object[2]
          {
            (object) userManager.MinRequiredPasswordLength,
            (object) userManager.MinRequiredNonAlphanumericCharacters
          });
        this.SetFailureTextLabel(this._changePasswordContainer, str);
      }
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

    private void ConfirmNewPasswordTextChanged(object source, EventArgs e) => this._confirmNewPassword = ((ITextControl) source).Text;

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
            providersListLocal.SelectedIndexChanged += new EventHandler(this.ProvidersList_SelectedIndexChanged);
            foreach (DataProviderBase dataProviderBase in contextProviders)
            {
              ListItem listItem = new ListItem(dataProviderBase.Title, dataProviderBase.Name);
              if (string.IsNullOrEmpty(this.MembershipProvider) && ManagerBase<MembershipDataProvider>.GetDefaultProviderName() == dataProviderBase.Name || dataProviderBase.Name == this.MembershipProvider)
                listItem.Selected = true;
              providersListLocal.Items.Add(listItem);
            }
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

    private void CreateChangePasswordViewControls()
    {
      this._changePasswordContainer = new ChangePasswordForm.ChangePasswordContainer();
      this._changePasswordContainer.ID = "ChangePasswordContainerID";
      this.ChangePasswordTemplate.InstantiateIn((Control) this._changePasswordContainer);
      this.Controls.Add((Control) this._changePasswordContainer);
      if (this._changePasswordContainer.UserNameTextBox is IEditableTextControl userNameTextBox)
        userNameTextBox.TextChanged += new EventHandler(this.UserNameTextChanged);
      if (this._changePasswordContainer.UserNameHolder != null)
        this._changePasswordContainer.UserNameHolder.Visible = this.DisplayUserName;
      if (this._changePasswordContainer.CurrentPasswordTextBox is IEditableTextControl currentPasswordTextBox)
        currentPasswordTextBox.TextChanged += new EventHandler(this.PasswordTextChanged);
      if (this._changePasswordContainer.NewPasswordTextBox is IEditableTextControl newPasswordTextBox1)
        newPasswordTextBox1.TextChanged += new EventHandler(this.NewPasswordTextChanged);
      if (this._changePasswordContainer.ConfirmNewPasswordTextBox is IEditableTextControl newPasswordTextBox2)
        newPasswordTextBox2.TextChanged += new EventHandler(this.ConfirmNewPasswordTextChanged);
      this.SetEditableChildProperties();
      this.providersHolder = this._changePasswordContainer.ProvidersHolder;
      this.providersList = this._changePasswordContainer.ProvidersList;
      if (this.providersHolder != null && this.providersList != null)
        this.InitializeProvidersList(this.providersList, this.providersHolder);
      if (this._changePasswordContainer.CancelButton != null)
        this._changePasswordContainer.CancelButton.CommandName = ChangePasswordForm.CancelButtonCommandName;
      this._changePasswordContainer.ChangePasswordButton.CommandName = ChangePasswordForm.ChangePasswordButtonCommandName;
      if (this.Page == null || this.Page.Form == null || this._changePasswordContainer.HiddenSubmitButton == null)
        return;
      this.Page.Form.DefaultButton = ((Control) this._changePasswordContainer.HiddenSubmitButton).UniqueID;
      this._changePasswordContainer.HiddenSubmitButton.CommandName = ChangePasswordForm.ChangePasswordButtonCommandName;
    }

    /// <summary>
    /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
    /// </summary>
    protected override void CreateChildControls()
    {
      this.Controls.Clear();
      this.CreateChangePasswordViewControls();
      this.CreateSuccessViewControls();
      this.UpdateValidators();
    }

    private void CreateSuccessViewControls()
    {
      this._successContainer = new ChangePasswordForm.SuccessContainer();
      this._successContainer.ID = "SuccessContainerID";
      this.SuccessTemplate.InstantiateIn((Control) this._successContainer);
      this.Controls.Add((Control) this._successContainer);
      this._successContainer.ContinueButton.CommandName = ChangePasswordForm.ContinueButtonCommandName;
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
          this._currentView = (ChangePasswordForm.View) triplet.Second;
        if (triplet.Third != null)
          this._userName = (string) triplet.Third;
      }
      this.membershipProvider = (string) objArray[1];
      if (this._changePasswordContainer == null)
        return;
      this.providersHolder = this._changePasswordContainer.ProvidersHolder;
      this.providersList = this._changePasswordContainer.ProvidersList;
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
        if (objArray[1] != null)
          ((IStateManager) this.MailDefinition).LoadViewState(objArray[1]);
      }
      this.UpdateValidators();
    }

    private void NewPasswordTextChanged(object source, EventArgs e) => this._newPassword = ((ITextControl) source).Text;

    /// <summary>Called when the bubble event is fired.</summary>
    /// <param name="source">The source.</param>
    /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
    /// <returns></returns>
    protected override bool OnBubbleEvent(object source, EventArgs e)
    {
      bool flag = false;
      if (e is CommandEventArgs)
      {
        CommandEventArgs e1 = (CommandEventArgs) e;
        if (e1.CommandName.Equals(ChangePasswordForm.ChangePasswordButtonCommandName, StringComparison.CurrentCultureIgnoreCase))
        {
          this.AttemptChangePassword();
          return true;
        }
        if (e1.CommandName.Equals(ChangePasswordForm.CancelButtonCommandName, StringComparison.CurrentCultureIgnoreCase))
        {
          this.OnCancelButtonClick((EventArgs) e1);
          return true;
        }
        if (e1.CommandName.Equals(ChangePasswordForm.ContinueButtonCommandName, StringComparison.CurrentCultureIgnoreCase))
        {
          this.OnContinueButtonClick((EventArgs) e1);
          flag = true;
        }
      }
      return flag;
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

    /// <summary>
    /// Raises the <see cref="E:CancelButtonClick" /> event.
    /// </summary>
    /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
    protected virtual void OnCancelButtonClick(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[ChangePasswordForm.EventCancelButtonClick];
      if (eventHandler != null)
        eventHandler((object) this, e);
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
          this.Page.Response.Redirect(SystemManager.CurrentHttpContext.Server.UrlDecode(str), true);
        else
          this.Page.Response.Redirect("~/Sitefinity/Login", true);
      }
    }

    /// <summary>
    /// Raises the <see cref="E:ChangedPassword" /> event.
    /// </summary>
    /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
    protected virtual void OnChangedPassword(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[ChangePasswordForm.EventChangedPassword];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    /// <summary>
    /// Raises the <see cref="E:ChangePasswordError" /> event.
    /// </summary>
    /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
    protected virtual void OnChangePasswordError(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[ChangePasswordForm.EventChangePasswordError];
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    /// <summary>
    /// Raises the <see cref="E:ChangingPassword" /> event.
    /// </summary>
    /// <param name="e">The <see cref="T:System.Web.UI.WebControls.LoginCancelEventArgs" /> instance containing the event data.</param>
    protected virtual void OnChangingPassword(LoginCancelEventArgs e)
    {
      LoginCancelEventHandler cancelEventHandler = (LoginCancelEventHandler) this.Events[ChangePasswordForm.EventChangingPassword];
      if (cancelEventHandler == null)
        return;
      cancelEventHandler((object) this, e);
    }

    /// <summary>
    /// Raises the <see cref="E:ContinueButtonClick" /> event.
    /// </summary>
    /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
    protected virtual void OnContinueButtonClick(EventArgs e)
    {
      EventHandler eventHandler = (EventHandler) this.Events[ChangePasswordForm.EventContinueButtonClick];
      if (eventHandler != null)
        eventHandler((object) this, e);
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

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnInit(EventArgs e)
    {
      if (!this.DesignMode)
      {
        string userName = LoginUtils.GetUserName((Control) this);
        if (!string.IsNullOrEmpty(userName))
          this.UserName = userName;
      }
      base.OnInit(e);
      this.Page.RegisterRequiresControlState((Control) this);
      if (this.Page == null || this.Page.IsPostBack || string.IsNullOrEmpty(this.Page.Request.QueryStringGet("provider")))
        return;
      this.MembershipProvider = this.Page.Request.QueryStringGet("provider");
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      if (!this.IsDesignMode() && this.Page != null)
        this.Page.SetFocus(this._changePasswordContainer.UserNameTextBox);
      base.OnPreRender(e);
      if (this.CurrentView != ChangePasswordForm.View.ChangePasswordForm)
        return;
      this.SetEditableChildProperties();
    }

    /// <summary>
    /// Raises the <see cref="E:SendingMail" /> event.
    /// </summary>
    /// <param name="e">The <see cref="T:System.Web.UI.WebControls.MailMessageEventArgs" /> instance containing the event data.</param>
    protected virtual void OnSendingMail(MailMessageEventArgs e)
    {
      MailMessageEventHandler messageEventHandler = (MailMessageEventHandler) this.Events[ChangePasswordForm.EventSendingMail];
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
      SendMailErrorEventHandler errorEventHandler = (SendMailErrorEventHandler) this.Events[ChangePasswordForm.EventSendMailError];
      if (errorEventHandler == null)
        return;
      errorEventHandler((object) this, e);
    }

    /// <summary>Fired when password text has changed.</summary>
    /// <param name="source">The source.</param>
    /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
    private void PasswordTextChanged(object source, EventArgs e) => this._password = ((ITextControl) source).Text;

    private void PerformSuccessAction(string userName, string newPassword)
    {
      MailMessage passwordMail = EmailSender.CreatePasswordMail(this.MembershipProvider, userName, newPassword);
      EmailSender emailSender = EmailSender.Get();
      emailSender.SenderProfileName = Config.Get<SecurityConfig>().Notifications.SenderProfile;
      emailSender.Sending += (EventHandler<MailMessageEventArgs>) ((sender, args) => this.OnSendingMail(args));
      emailSender.Error += (EventHandler<SendMailErrorEventArgs>) ((sender, args) => this.OnSendMailError(args));
      emailSender.TrySend(passwordMail);
      string successPageUrl = this.SuccessPageUrl;
      if (!RouteHelper.IsAbsoluteUrl(successPageUrl) && !string.IsNullOrEmpty(successPageUrl))
        this.Page.Response.Redirect(this.ResolveClientUrl(successPageUrl), false);
      else
        this.CurrentView = ChangePasswordForm.View.Success;
    }

    /// <summary>
    /// Writes the <see cref="T:System.Web.UI.WebControls.CompositeControl" /> content to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object, for display on the client.
    /// </summary>
    /// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    protected override void Render(HtmlTextWriter writer)
    {
      if (this.Page != null)
        this.Page.VerifyRenderingInServerForm((Control) this);
      if (this.DesignMode)
        this.ChildControlsCreated = false;
      this.EnsureChildControls();
      this.SetChildProperties();
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
      object z = (object) null;
      object currentView = (object) (int) this._currentView;
      if (this._userName != null && this._currentView != ChangePasswordForm.View.Success)
        z = (object) this._userName;
      return (object) new object[2]
      {
        (object) new Triplet(x, currentView, z),
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
      for (int index = 0; index < objArray.Length - 1; ++index)
      {
        if (objArray[index] != null)
          return (object) objArray;
      }
      return (object) null;
    }

    /// <summary>Sets the child properties.</summary>
    public void SetChildProperties()
    {
      switch (this.CurrentView)
      {
        case ChangePasswordForm.View.ChangePasswordForm:
          this.SetCommonChangePasswordViewProperties();
          break;
        case ChangePasswordForm.View.Success:
          this.SetCommonSuccessViewProperties();
          break;
      }
    }

    private void SetCommonChangePasswordViewProperties() => this._successContainer.Visible = false;

    private void SetCommonSuccessViewProperties() => this._changePasswordContainer.Visible = false;

    private void SetEditableChildProperties()
    {
      if (this.UserNameInternal.Length <= 0 || !this.DisplayUserName)
        return;
      ITextControl userNameTextBox = (ITextControl) this._changePasswordContainer.UserNameTextBox;
      if (userNameTextBox == null)
        return;
      userNameTextBox.Text = this.UserNameInternal;
    }

    private void SetFailureTextLabel(
      ChangePasswordForm.ChangePasswordContainer container,
      string failureText)
    {
      ITextControl failureTextLabel = (ITextControl) container.FailureTextLabel;
      if (failureTextLabel == null)
        return;
      container.FailureTextLabel.Visible = true;
      failureTextLabel.Text = failureText;
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

    private void UpdateValidators()
    {
      if (this.DesignMode)
        return;
      ChangePasswordForm.ChangePasswordContainer passwordContainer = this._changePasswordContainer;
      if (passwordContainer == null)
        return;
      string validationGroup = this.ValidationGroup;
      bool displayUserName = this.DisplayUserName;
      RequiredFieldValidator userNameRequired = passwordContainer.UserNameRequired;
      CompareValidator oldPasswordCompare = passwordContainer.NewAndOldPasswordCompare;
      if (oldPasswordCompare != null)
      {
        oldPasswordCompare.ValidationGroup = validationGroup;
        oldPasswordCompare.Enabled = true;
        oldPasswordCompare.Visible = true;
      }
      if (userNameRequired != null)
      {
        userNameRequired.ValidationGroup = validationGroup;
        userNameRequired.Enabled = displayUserName;
        userNameRequired.Visible = displayUserName;
      }
      bool regExpEnabled = this.RegExpEnabled;
      RegularExpressionValidator regExpValidator = passwordContainer.RegExpValidator;
      if (regExpValidator != null)
      {
        regExpValidator.ValidationGroup = validationGroup;
        regExpValidator.Enabled = regExpEnabled;
        regExpValidator.Visible = regExpEnabled;
      }
      passwordContainer.ConfirmNewPasswordRequired.ValidationGroup = validationGroup;
      passwordContainer.NewPasswordCompareValidator.ValidationGroup = validationGroup;
      passwordContainer.NewPasswordRequired.ValidationGroup = validationGroup;
      passwordContainer.PasswordRequired.ValidationGroup = validationGroup;
      passwordContainer.ChangePasswordButton.ValidationGroup = validationGroup;
      if (passwordContainer.HiddenSubmitButton == null)
        return;
      passwordContainer.HiddenSubmitButton.ValidationGroup = validationGroup;
    }

    private void UserNameTextChanged(object source, EventArgs e)
    {
      string text = ((ITextControl) source).Text;
      if (string.IsNullOrEmpty(text))
        return;
      this.UserName = text;
    }

    /// <summary>Gets or sets the change password failure text.</summary>
    /// <value>The change password failure text.</value>
    [Localizable(true)]
    public virtual string ChangePasswordFailureText
    {
      get => (string) this.ViewState[nameof (ChangePasswordFailureText)] ?? Res.Get<ErrorMessages>().ChangePasswordDefaultChangePasswordFailureText;
      set => this.ViewState[nameof (ChangePasswordFailureText)] = (object) value;
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
    /// Gets or sets the path to a custom ChangePassword template for the control.
    /// </summary>
    [DescriptionResource(typeof (PageResources), "ChangePasswordTemplateDescription")]
    [Browsable(false)]
    [PersistenceMode(PersistenceMode.InnerProperty)]
    [TemplateContainer(typeof (ChangePasswordForm))]
    public virtual ITemplate ChangePasswordTemplate
    {
      get
      {
        if (this._changePasswordTemplate == null)
          this._changePasswordTemplate = this.CreateLayoutTemplate(this.ChangePasswordTemplatePath, (string) null);
        return this._changePasswordTemplate;
      }
      set
      {
        this._changePasswordTemplate = value;
        this.ChildControlsCreated = false;
      }
    }

    /// <summary>
    /// Gets or sets the path to a custom ChangePassword template template for the control.
    /// </summary>
    public virtual string ChangePasswordTemplatePath
    {
      get => (string) this.ViewState[nameof (ChangePasswordTemplatePath)] ?? ChangePasswordForm.ChangePasswordPath;
      set => this.ViewState[nameof (ChangePasswordTemplatePath)] = (object) value;
    }

    /// <summary>
    /// Gets or sets the path to a custom Success template template for the control.
    /// </summary>
    public virtual string SuccessTemplatePath
    {
      get => (string) this.ViewState[nameof (SuccessTemplatePath)] ?? ChangePasswordForm.SuccessPath;
      set => this.ViewState[nameof (SuccessTemplatePath)] = (object) value;
    }

    /// <summary>Gets the confirm new password.</summary>
    /// <value>The confirm new password.</value>
    [Themeable(false)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Filterable(false)]
    public virtual string ConfirmNewPassword => this._confirmNewPassword != null ? this._confirmNewPassword : string.Empty;

    /// <summary>Gets or sets the default continue button text.</summary>
    /// <value>The continue button text.</value>
    [Localizable(true)]
    public virtual string ContinueButtonText
    {
      get => (string) this.ViewState[nameof (ContinueButtonText)] ?? Res.Get<ErrorMessages>().ChangePasswordDefaultContinueButtonText;
      set => this.ViewState[nameof (ContinueButtonText)] = (object) value;
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

    /// <summary>Gets the current password of user.</summary>
    /// <value>The current password.</value>
    public virtual string CurrentPassword => this._password != null ? this._password : string.Empty;

    private string CurrentPasswordInternal
    {
      get
      {
        string currentPassword = this.CurrentPassword;
        if (string.IsNullOrEmpty(currentPassword) && this._changePasswordContainer != null)
        {
          ITextControl currentPasswordTextBox = (ITextControl) this._changePasswordContainer.CurrentPasswordTextBox;
          if (currentPasswordTextBox != null)
            return currentPasswordTextBox.Text;
        }
        return currentPassword;
      }
    }

    private ChangePasswordForm.View CurrentView
    {
      get => this._currentView;
      set
      {
        if (value < ChangePasswordForm.View.ChangePasswordForm || value > ChangePasswordForm.View.Success)
          throw new ArgumentOutOfRangeException(nameof (value));
        if (value == this.CurrentView)
          return;
        this._currentView = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to display user name textbox.
    /// </summary>
    /// <value><c>true</c> if display user name textbox; otherwise, <c>false</c>.</value>
    [DefaultValue(false)]
    public virtual bool DisplayUserName
    {
      get
      {
        object obj = this.ViewState[nameof (DisplayUserName)];
        return obj != null && (bool) obj;
      }
      set
      {
        if (this.DisplayUserName == value)
          return;
        this.ViewState[nameof (DisplayUserName)] = (object) value;
        this.UpdateValidators();
      }
    }

    /// <summary>Gets the mail details for sending email.</summary>
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
        if (!this.ChildControlsCreated)
          return;
        this.ChildControlsCreated = false;
      }
    }

    /// <summary>Gets the new password entered by the user.</summary>
    /// <value>The new password.</value>
    [Filterable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Themeable(false)]
    [Browsable(false)]
    public virtual string NewPassword => this._newPassword != null ? this._newPassword : string.Empty;

    private string NewPasswordInternal
    {
      get
      {
        string newPassword = this.NewPassword;
        if (string.IsNullOrEmpty(newPassword) && this._changePasswordContainer != null)
        {
          ITextControl newPasswordTextBox = (ITextControl) this._changePasswordContainer.NewPasswordTextBox;
          if (newPasswordTextBox != null)
            return newPasswordTextBox.Text;
        }
        return newPassword;
      }
    }

    /// <summary>
    /// Gets or sets the new password regular expression used to validate new password.
    /// </summary>
    /// <value>The new password regular expression.</value>
    public virtual string NewPasswordRegularExpression
    {
      get => (string) this.ViewState[nameof (NewPasswordRegularExpression)] ?? string.Empty;
      set
      {
        if (!(this.NewPasswordRegularExpression != value))
          return;
        this.ViewState[nameof (NewPasswordRegularExpression)] = (object) value;
        this.UpdateValidators();
      }
    }

    /// <summary>
    /// Gets or sets the error message for regular expression password validator.
    /// </summary>
    /// <value>The new password regular expression error message.</value>
    public virtual string NewPasswordRegularExpressionErrorMessage
    {
      get => (string) this.ViewState[nameof (NewPasswordRegularExpressionErrorMessage)] ?? Res.Get<ErrorMessages>().PasswordInvalidPasswordErrorMessage;
      set => this.ViewState[nameof (NewPasswordRegularExpressionErrorMessage)] = (object) value;
    }

    /// <summary>
    /// Gets or sets the error message if user did not set its current password.
    /// </summary>
    /// <value>The password required error message.</value>
    public virtual string PasswordRequiredErrorMessage
    {
      get => (string) this.ViewState[nameof (PasswordRequiredErrorMessage)] ?? Res.Get<ErrorMessages>().ChangePasswordDefaultPasswordRequiredErrorMessage;
      set => this.ViewState[nameof (PasswordRequiredErrorMessage)] = (object) value;
    }

    private bool RegExpEnabled => this.NewPasswordRegularExpression.Length > 0;

    /// <summary>
    /// Gets or sets the success page URL. Redirects to this url after successful change of password.
    /// </summary>
    /// <value>The success page URL.</value>
    public virtual string SuccessPageUrl
    {
      get => (string) this.ViewState[nameof (SuccessPageUrl)] ?? string.Empty;
      set => this.ViewState[nameof (SuccessPageUrl)] = (object) value;
    }

    /// <summary>
    /// Gets or sets the validation group. This validation group is used for all child controls.
    /// </summary>
    /// <value>The validation group.</value>
    public string ValidationGroup
    {
      get => (string) this.ViewState[nameof (ValidationGroup)] ?? this.UniqueID;
      set => this.ViewState[nameof (ValidationGroup)] = (object) value;
    }

    /// <summary>
    /// Gets or sets the path to a success template of the control.
    /// </summary>
    [DescriptionResource(typeof (PageResources), "SuccessTemplateChangePasswordDescription")]
    [Browsable(false)]
    [PersistenceMode(PersistenceMode.InnerProperty)]
    [TemplateContainer(typeof (ChangePasswordForm))]
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
    /// Gets or sets the name of the currently authenticated user.
    /// </summary>
    /// <value>The name of the user.</value>
    public virtual string UserName
    {
      get => this._userName != null ? this._userName : string.Empty;
      set => this._userName = value;
    }

    private string UserNameInternal
    {
      get
      {
        string userName = this.UserName;
        if (string.IsNullOrEmpty(userName) && this._changePasswordContainer != null && this.DisplayUserName)
        {
          ITextControl userNameTextBox = (ITextControl) this._changePasswordContainer.UserNameTextBox;
          if (userNameTextBox != null)
            return userNameTextBox.Text;
        }
        return userName;
      }
    }

    /// <summary>
    /// Gets or sets the error message if user do not fill the UserName textbox.
    /// </summary>
    /// <value>The user name required error message.</value>
    public virtual string UserNameRequiredErrorMessage
    {
      get => (string) this.ViewState[nameof (UserNameRequiredErrorMessage)] ?? Res.Get<ErrorMessages>().ChangePasswordDefaultUserNameRequiredErrorMessage;
      set => this.ViewState[nameof (UserNameRequiredErrorMessage)] = (object) value;
    }

    /// <summary>The container for ChangePassword View.</summary>
    protected class ChangePasswordContainer : GenericContainer
    {
      private IButtonControl cancelButton;
      private IButtonControl changePasswordButton;
      private RequiredFieldValidator confirmNewPasswordRequired;
      private Control confirmNewPasswordTextBox;
      private Control currentPasswordTextBox;
      private Control failureTextLabel;
      private CompareValidator newPasswordCompareValidator;
      private RequiredFieldValidator newPasswordRequired;
      private Control newPasswordTextBox;
      private RequiredFieldValidator passwordRequired;
      private RegularExpressionValidator regExpValidator;
      private RequiredFieldValidator userNameRequired;
      private Control userNameHolder;
      private Control userNameTextBox;
      private ListControl providersList;
      private Control providersHolder;
      private IButtonControl hiddenSubmitButton;
      private CompareValidator newAndOldPasswordCompare;

      /// <summary>
      /// Gets the hidden submit button. Used to allow Enter press working for all types of buttons (even for LinkButton).
      /// </summary>
      /// <value>The hidden submit button.</value>
      public virtual IButtonControl HiddenSubmitButton
      {
        get
        {
          this.hiddenSubmitButton = this.hiddenSubmitButton ?? this.GetControl<IButtonControl>("hiddenSubmitButton", true);
          return this.hiddenSubmitButton;
        }
      }

      /// <summary>Gets a validator.</summary>
      /// <value>The new and old password compare.</value>
      public CompareValidator NewAndOldPasswordCompare
      {
        get
        {
          if (this.newAndOldPasswordCompare == null)
            this.newAndOldPasswordCompare = this.GetControl<CompareValidator>(nameof (NewAndOldPasswordCompare), false);
          return this.newAndOldPasswordCompare;
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

      /// <summary>Gets or sets the cancel button.</summary>
      /// <value>The cancel button.</value>
      public IButtonControl CancelButton
      {
        get
        {
          if (this.cancelButton == null)
            this.cancelButton = this.GetControl<IButtonControl>("CancelPushButton", false);
          return this.cancelButton;
        }
        set => this.cancelButton = value;
      }

      /// <summary>Gets or sets the change password button.</summary>
      /// <value>The change password button.</value>
      public IButtonControl ChangePasswordButton
      {
        get
        {
          if (this.changePasswordButton == null)
            this.changePasswordButton = this.GetControl<IButtonControl>("ChangePasswordPushButton", true);
          return this.changePasswordButton;
        }
        set => this.changePasswordButton = value;
      }

      /// <summary>Gets or sets the confirm new password validator.</summary>
      /// <value>The confirm new password required.</value>
      public RequiredFieldValidator ConfirmNewPasswordRequired
      {
        get
        {
          if (this.confirmNewPasswordRequired == null)
            this.confirmNewPasswordRequired = this.GetControl<RequiredFieldValidator>(nameof (ConfirmNewPasswordRequired), true);
          return this.confirmNewPasswordRequired;
        }
        set => this.confirmNewPasswordRequired = value;
      }

      /// <summary>Gets or sets the confirm new password text box.</summary>
      /// <value>The confirm new password text box.</value>
      public Control ConfirmNewPasswordTextBox
      {
        get
        {
          if (this.confirmNewPasswordTextBox == null)
            this.confirmNewPasswordTextBox = this.GetControl<Control>("ConfirmNewPassword", true);
          return this.confirmNewPasswordTextBox;
        }
        set => this.confirmNewPasswordTextBox = value;
      }

      /// <summary>Gets or sets the current password text box.</summary>
      /// <value>The current password text box.</value>
      public Control CurrentPasswordTextBox
      {
        get
        {
          if (this.currentPasswordTextBox == null)
            this.currentPasswordTextBox = this.GetControl<Control>("CurrentPassword", true);
          return this.currentPasswordTextBox;
        }
        set => this.currentPasswordTextBox = value;
      }

      /// <summary>Gets or sets the failure label.</summary>
      /// <value>The failure text label.</value>
      public Control FailureTextLabel
      {
        get
        {
          if (this.failureTextLabel == null)
            this.failureTextLabel = this.GetControl<Control>("FailureText", true);
          return this.failureTextLabel;
        }
        set => this.failureTextLabel = value;
      }

      /// <summary>Gets or sets the new password compare validator.</summary>
      /// <value>The new password compare validator.</value>
      public CompareValidator NewPasswordCompareValidator
      {
        get
        {
          if (this.newPasswordCompareValidator == null)
            this.newPasswordCompareValidator = this.GetControl<CompareValidator>("NewPasswordCompare", true);
          return this.newPasswordCompareValidator;
        }
        set => this.newPasswordCompareValidator = value;
      }

      /// <summary>Gets or sets the new password validator.</summary>
      /// <value>The new password required.</value>
      public RequiredFieldValidator NewPasswordRequired
      {
        get
        {
          if (this.newPasswordRequired == null)
            this.newPasswordRequired = this.GetControl<RequiredFieldValidator>(nameof (NewPasswordRequired), true);
          return this.newPasswordRequired;
        }
        set => this.newPasswordRequired = value;
      }

      /// <summary>Gets or sets the new password text box.</summary>
      /// <value>The new password text box.</value>
      public Control NewPasswordTextBox
      {
        get
        {
          if (this.newPasswordTextBox == null)
            this.newPasswordTextBox = this.GetControl<Control>("NewPassword", true);
          return this.newPasswordTextBox;
        }
        set => this.newPasswordTextBox = value;
      }

      /// <summary>Gets or sets the validator for the current password.</summary>
      /// <value>Gets or sets the validator for the current password.</value>
      public RequiredFieldValidator PasswordRequired
      {
        get
        {
          if (this.passwordRequired == null)
            this.passwordRequired = this.GetControl<RequiredFieldValidator>("CurrentPasswordRequired", true);
          return this.passwordRequired;
        }
        set => this.passwordRequired = value;
      }

      /// <summary>
      /// Gets or sets the regular expression validator for the new password.
      /// </summary>
      /// <value>The reg exp validator.</value>
      public RegularExpressionValidator RegExpValidator
      {
        get
        {
          if (this.regExpValidator == null)
            this.regExpValidator = this.GetControl<RegularExpressionValidator>();
          return this.regExpValidator;
        }
        set => this.regExpValidator = value;
      }

      /// <summary>Gets or sets the validator for the user name.</summary>
      /// <value>The user name required.</value>
      public RequiredFieldValidator UserNameRequired
      {
        get
        {
          if (this.userNameRequired == null)
            this.userNameRequired = this.GetControl<RequiredFieldValidator>(nameof (UserNameRequired), false);
          return this.userNameRequired;
        }
        set => this.userNameRequired = value;
      }

      /// <summary>Gets or sets the user name holder.</summary>
      /// <value>The user name holder.</value>
      public Control UserNameHolder
      {
        get
        {
          if (this.userNameHolder == null)
            this.userNameHolder = this.GetControl<Control>(nameof (UserNameHolder), false);
          return this.userNameHolder;
        }
        set => this.userNameHolder = value;
      }

      /// <summary>Gets or sets the user name text box.</summary>
      /// <value>The user name text box.</value>
      public Control UserNameTextBox
      {
        get
        {
          if (this.userNameTextBox == null)
            this.userNameTextBox = this.GetControl<Control>("UserName", false);
          return this.userNameTextBox;
        }
        set => this.userNameTextBox = value;
      }
    }

    /// <summary>The container for the success view.</summary>
    protected class SuccessContainer : GenericContainer
    {
      private IButtonControl continueButton;

      /// <summary>Gets or sets the continue button.</summary>
      /// <value>The continue button.</value>
      public IButtonControl ContinueButton
      {
        get
        {
          if (this.continueButton == null)
            this.continueButton = this.GetControl<IButtonControl>("ContinuePushButton", true);
          return this.continueButton;
        }
      }
    }

    /// <summary>Enum defining the different views of the control.</summary>
    protected enum View
    {
      /// <summary>Coressponds to the ChangePassword View.</summary>
      ChangePasswordForm,
      /// <summary>Coressponds to the Success View.</summary>
      Success,
    }
  }
}
