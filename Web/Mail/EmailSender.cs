// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Mail.EmailSender
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Project.Configuration;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Notifications;
using Telerik.Sitefinity.Services.Notifications.Configuration;

namespace Telerik.Sitefinity.Web.Mail
{
  /// <summary>
  /// Serves like a manager for <see cref="T:Telerik.Sitefinity.Web.Mail.IEmailSender" />
  /// </summary>
  public class EmailSender : 
    IEmailSender,
    IDisposable,
    ISupportNotificationService,
    ISenderProfileVerifiable
  {
    private string senderProfileName;
    private ISenderProfile senderProfile;
    private ServiceContext context;
    private static EmailSender instance = (EmailSender) null;
    private static readonly object syncLock = new object();
    private static readonly ConcurrentDictionary<string, IEmailSender> emailSenderProviders = new ConcurrentDictionary<string, IEmailSender>();
    public const string PasswordRecoveryReplacementKey = "<%\\s*Password\\s*%>";
    public const string UserNameReplacementKey = "<%\\s*UserName\\s*%>";
    public const string ConfirmationPageUrlReplacementKey = "<%\\s*ConfirmationUrl\\s*%>";
    public const string SiteNameReplacementKey = "<%\\s*SiteName\\s*%>";
    public const string UserDisplayNameReplacementKey = "<%\\s*UserDisplayName\\s*%>";

    /// <summary>
    /// Static constructor for the <see cref="T:Telerik.Sitefinity.Web.Mail.EmailSender" /> class.
    /// </summary>
    static EmailSender() => Bootstrapper.Bootstrapping += new EventHandler<EventArgs>(EmailSender.Bootstrapper_Bootstrapping);

    /// <summary>
    /// Get an instance of <see cref="T:Telerik.Sitefinity.Web.Mail.EmailSender" /> with the default name
    /// </summary>
    /// <returns>An instance of the email provider</returns>
    public static EmailSender Get() => EmailSender.Get((string) null);

    /// <summary>
    /// Get an instance of <see cref="T:Telerik.Sitefinity.Web.Mail.EmailSender" /> with the specified <paramref name="name" />
    /// </summary>
    /// <param name="name">Name that the provider was registerd with.</param>
    /// <returns>An instance of the email provider</returns>
    public static EmailSender Get(string name)
    {
      if (name.IsNullOrEmpty())
        name = "Standard";
      EmailSender instance = EmailSender.GetInstance();
      instance.Provider = EmailSender.GetProvider(name);
      return instance;
    }

    private static EmailSender GetInstance()
    {
      if (EmailSender.instance == null)
      {
        lock (EmailSender.syncLock)
        {
          if (EmailSender.instance == null)
            EmailSender.instance = new EmailSender();
        }
      }
      return EmailSender.instance;
    }

    private static IEmailSender GetProvider(string name)
    {
      if (EmailSender.emailSenderProviders.ContainsKey(name))
        return EmailSender.emailSenderProviders[name];
      IEmailSender provider = ObjectFactory.Resolve<IEmailSender>(name);
      provider.Initialize();
      EmailSender.emailSenderProviders.TryAdd(name, provider);
      return provider;
    }

    /// <summary>Gets/sets</summary>
    protected internal virtual IEmailSender Provider { get; set; }

    /// <inheritdoc />
    public string SenderProfileName
    {
      get => this.senderProfileName;
      set
      {
        this.senderProfileName = value;
        if (!(this.Provider is ISupportNotificationService provider) || !(provider.SenderProfileName != this.senderProfileName))
          return;
        provider.SenderProfileName = this.senderProfileName;
        this.Provider.Initialize();
      }
    }

    /// <inheritdoc />
    public ISenderProfile SenderProfile
    {
      get => this.senderProfile;
      set
      {
        this.senderProfile = value;
        if (!(this.Provider is ISupportNotificationService provider) || provider.SenderProfile == this.senderProfile)
          return;
        provider.SenderProfile = this.senderProfile;
        this.Provider.Initialize();
      }
    }

    /// <inheritdoc />
    public ServiceContext Context
    {
      get => this.context;
      set
      {
        this.context = value;
        if (!(this.Provider is ISupportNotificationService provider) || provider.Context == this.context)
          return;
        provider.Context = this.context;
        this.Provider.Initialize();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the the sender profile is configured correctly.
    /// </summary>
    /// <param name="profileName">The name of the sender profile to be verified.</param>
    /// <returns>Returns True if the profile is configured correctly.</returns>
    public bool VerifySenderProfile(string profileName) => this.VerifySenderProfile(profileName, out string _);

    /// <inheritdoc />
    public bool VerifySenderProfile(string profileName, out string errorMessage)
    {
      errorMessage = string.Empty;
      return this.Provider is ISenderProfileVerifiable provider && provider.VerifySenderProfile(profileName, out errorMessage);
    }

    /// <summary>
    /// Returns a value indicating whether the provider supports async email sending.
    /// </summary>
    /// <remarks>This property is required by the IEmailSender interface that is implemented by both the manager and the provider.</remarks>
    public virtual bool SupportsAsyncSending => false;

    /// <summary>Called only once to initialize the email sender.</summary>
    public virtual void Initialize() => this.Provider.Initialize();

    /// <summary>Send an email</summary>
    /// <param name="message">Message to send</param>
    public virtual void Send(MailMessage message) => this.Provider.Send(message);

    /// <summary>Senda an email</summary>
    /// <param name="from">Address to designate as sender</param>
    /// <param name="to">Address to designate as a receiver. Semi-column delimited</param>
    /// <param name="subject">Subjec of the email</param>
    /// <param name="message">Message body, in HTML</param>
    public virtual void Send(string from, string to, string subject, string message) => this.Provider.Send(from, to, subject, message);

    /// <summary>Sends an e-mail message to an SMTP server for delivery. The message
    /// sender, recipients, subject, and message body are specified using <see cref="T:System.String" />
    /// objects. This method does not block the calling thread and allows the caller
    /// to pass an object to the method that is invoked when the operation completes.
    /// </summary>
    public void SendAsync(
      string from,
      string recipients,
      string subject,
      string body,
      object userToken)
    {
      if (this.Provider.SupportsAsyncSending)
        this.Provider.SendAsync(from, recipients, subject, body, userToken);
      else
        new EmailSender.SendEmailAsyncLongDelegate(EmailSender.SendEmailLongAsync).BeginInvoke(this.Provider, from, recipients, subject, body, userToken, (AsyncCallback) null, (object) null);
    }

    /// <summary>Sends the specified e-mail message to an SMTP server for delivery. This
    /// method does not block the calling thread and allows the caller to pass an object
    /// to the method that is invoked when the operation completes. </summary>
    public void SendAsync(MailMessage message, object userToken)
    {
      if (this.Provider.SupportsAsyncSending)
        this.Provider.SendAsync(message, userToken);
      else
        new EmailSender.SendEmailAsyncShortDelegate(EmailSender.SendEmailShortAsync).BeginInvoke(this.Provider, message, userToken, (AsyncCallback) null, (object) null);
    }

    /// <summary>
    /// Does not throw exceptions, but raises <see cref="E:Telerik.Sitefinity.Web.Mail.EmailSender.Error" />, instead
    /// </summary>
    /// <param name="message">message to send</param>
    /// <returns>True if succesfully sent, false otherwize</returns>
    public virtual bool TrySend(MailMessage message) => this.Provider.TrySend(message);

    /// <summary>
    /// Does not throw exceptions, but raises <see cref="E:Telerik.Sitefinity.Web.Mail.EmailSender.Error" />, instead
    /// </summary>
    /// <param name="from">Address to designate as sender</param>
    /// <param name="to">Address to designate as a receiver. Semi-column delimited</param>
    /// <param name="subject">Subjec of the email</param>
    /// <param name="message">Message body, in HTML</param>
    /// <returns></returns>
    public virtual bool TrySend(string from, string to, string subject, string message) => this.Provider.TrySend(from, to, subject, message);

    /// <summary>
    /// Returns whether the rights to send an email are granted.
    /// </summary>
    /// <returns></returns>
    public virtual bool CheckIfHasSmtpPermissions() => this.Provider.CheckIfHasSmtpPermissions();

    /// <summary>
    /// Get the error message that was set after the last call to <see cref="M:Telerik.Sitefinity.Web.Mail.EmailSender.CheckIfHasSmtpPermissions" />
    /// </summary>
    /// <value></value>
    public virtual string LastErrorMessage => this.Provider.LastErrorMessage;

    /// <summary>
    /// Raises the event before sending an email, giving the option to cancel the sending
    /// </summary>
    public virtual event EventHandler<MailMessageEventArgs> Sending
    {
      add => this.Provider.Sending += value;
      remove => this.Provider.Sending -= value;
    }

    /// <summary>
    /// Raises the event after the email has been succesfully sent
    /// </summary>
    public event EventHandler<MailMessageEventArgs> Sent
    {
      add => this.Provider.Sent += value;
      remove => this.Provider.Sent -= value;
    }

    /// <summary>Raises the event in the case of an error</summary>
    public event EventHandler<SendMailErrorEventArgs> Error
    {
      add => this.Provider.Error += value;
      remove => this.Provider.Error -= value;
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose() => this.Provider.Dispose();

    /// <summary>
    /// Create a mail message for password recovery that is ready to be sent
    /// </summary>
    /// <param name="membershipProviderName">Membership provider name. Will use settings from here.</param>
    /// <param name="userName">User name</param>
    /// <param name="password">New password</param>
    /// <returns>Mail message that is ready to be sent.</returns>
    public static MailMessage CreatePasswordMail(
      string membershipProviderName,
      string userName,
      string password)
    {
      UserManager manager = UserManager.GetManager(membershipProviderName);
      User user = manager.GetUser(userName);
      return EmailSender.CreatePasswordMail(manager.RecoveryMailAddress, user.Email, user.UserName, password, manager.RecoveryMailSubject, manager.RecoveryMailBody);
    }

    /// <summary>
    /// Create a mail message for password recovery that is ready to be sent
    /// </summary>
    /// <param name="recoveryMail">Messages to users will be sent from this email address</param>
    /// <param name="userMmail">Recipient</param>
    /// <param name="userName">User name</param>
    /// <param name="password">New password for the user</param>
    /// <param name="subject">Recovery mail subject</param>
    /// <param name="body">Recovery mail message template</param>
    /// <returns>Mail message that is ready to be sent</returns>
    public static MailMessage CreatePasswordMail(
      string recoveryMail,
      string userMmail,
      string userName,
      string password,
      string subject,
      string body)
    {
      if (string.IsNullOrEmpty(recoveryMail))
        recoveryMail = string.Empty;
      if (string.IsNullOrEmpty(body))
        body = Res.Get<ErrorMessages>().CreateUserWizardDefaultBody;
      if (string.IsNullOrEmpty(subject))
        subject = Res.Get<Labels>().PasswordRecovery;
      MailMessage passwordMail = new MailMessage(recoveryMail, userMmail)
      {
        IsBodyHtml = true,
        Subject = subject
      };
      passwordMail.Sender = passwordMail.From;
      passwordMail.Body = body.Replace("<%\\s*UserName\\s*%>", HttpUtility.HtmlEncode(userName)).Replace("<%\\s*Password\\s*%>", HttpUtility.HtmlEncode(password));
      return passwordMail;
    }

    /// <summary>
    /// Creates an email message for user registration confirmation.
    /// </summary>
    /// <param name="membershipProviderName">Name of the membership provider.</param>
    /// <param name="userName">Name of the user.</param>
    /// <param name="confirmationPageUrl">The confirmation page URL.</param>
    /// <returns></returns>
    public static MailMessage CreateRegistrationConfirmationEmail(
      string membershipProviderName,
      string userName,
      string confirmationPageUrl)
    {
      UserManager manager = UserManager.GetManager(membershipProviderName);
      return EmailSender.CreateRegistrationConfirmationEmail(manager, manager.GetUser(userName), confirmationPageUrl);
    }

    /// <summary>
    /// Creates an email message for user registration confirmation.
    /// </summary>
    /// <param name="manager">The user manager instance that will be used to get the confirmation email content.</param>
    /// <param name="user">The user.</param>
    /// <param name="confirmationPageUrl">The confirmation page URL.</param>
    /// <returns></returns>
    public static MailMessage CreateRegistrationConfirmationEmail(
      UserManager manager,
      User user,
      string confirmationPageUrl)
    {
      return EmailSender.CreateRegistrationConfirmationEmail(manager.ConfirmationEmailAddress, user.Email, user.UserName, confirmationPageUrl, manager.ConfirmRegistrationMailSubject, manager.ConfirmRegistrationMailBody);
    }

    /// <summary>
    /// Creates an email message for user registration confirmation.
    /// </summary>
    /// <param name="confirmationEmailAddress">The confirmation email address.</param>
    /// <param name="userEmailAddress">The user email address.</param>
    /// <param name="userName">Name of the user.</param>
    /// <param name="confirmationPageUrl">The confirmation page URL.</param>
    /// <param name="subject">The subject of the confirmation message.</param>
    /// <param name="body">The body of the confirmation message.</param>
    /// <returns></returns>
    public static MailMessage CreateRegistrationConfirmationEmail(
      string confirmationEmailAddress,
      string userEmailAddress,
      string userName,
      string confirmationPageUrl,
      string subject,
      string body)
    {
      return new MailMessage(confirmationEmailAddress, userEmailAddress)
      {
        IsBodyHtml = true,
        SubjectEncoding = Encoding.Unicode,
        BodyEncoding = Encoding.Unicode,
        Subject = subject,
        Body = EmailSender.ParseMessageBody(body, userName, confirmationPageUrl)
      };
    }

    /// <summary>Creates a registration success email.</summary>
    /// <param name="membershipProviderName">Name of the membership provider.</param>
    /// <param name="userName">Name of the user.</param>
    /// <returns></returns>
    public static MailMessage CreateRegistrationSuccessEmail(
      string membershipProviderName,
      string userName)
    {
      UserManager manager = UserManager.GetManager(membershipProviderName);
      return EmailSender.CreateRegistrationSuccessEmail(manager, manager.GetUser(userName));
    }

    /// <summary>Creates a registration success email.</summary>
    /// <param name="manager">The manager.</param>
    /// <param name="user">The user.</param>
    /// <returns></returns>
    public static MailMessage CreateRegistrationSuccessEmail(
      UserManager manager,
      User user)
    {
      return EmailSender.CreateRegistrationSuccessEmail(manager.SuccessfulRegistrationEmailAddress, user.Email, user.UserName, manager.SuccessfulRegistrationMailSubject, manager.SuccessfulRegistrationEmailBody);
    }

    /// <summary>Creates a registration success email.</summary>
    /// <param name="registrationSuccessEmailAddress">The registration success email address.</param>
    /// <param name="registrationSuccessAddress">The registration success address.</param>
    /// <param name="userName">Name of the user.</param>
    /// <param name="subject">The subject.</param>
    /// <param name="body">The body.</param>
    /// <returns></returns>
    public static MailMessage CreateRegistrationSuccessEmail(
      string registrationSuccessEmailAddress,
      string userAddress,
      string userName,
      string subject,
      string body)
    {
      return new MailMessage(registrationSuccessEmailAddress, userAddress)
      {
        IsBodyHtml = true,
        SubjectEncoding = Encoding.Unicode,
        BodyEncoding = Encoding.Unicode,
        Subject = subject,
        Body = EmailSender.ParseMessageBody(body, userName, string.Empty)
      };
    }

    private static void Bootstrapper_Bootstrapping(object sender, EventArgs e)
    {
      if (EmailSender.instance != null)
      {
        EmailSender.instance.Dispose();
        EmailSender.instance = (EmailSender) null;
      }
      if (EmailSender.emailSenderProviders.Count <= 0)
        return;
      foreach (IDisposable disposable in (IEnumerable<IEmailSender>) EmailSender.emailSenderProviders.Values)
        disposable.Dispose();
      EmailSender.emailSenderProviders.Clear();
    }

    private static string ParseMessageBody(
      string original,
      string userName,
      string confirmationPageUrl)
    {
      string newValue = Config.Get<ProjectConfig>().ProjectName;
      if (newValue == "/")
        newValue = SystemManager.CurrentHttpContext.Request.Url.Host;
      return original.Replace("<%\\s*UserName\\s*%>", HttpUtility.HtmlEncode(userName)).Replace("<%\\s*ConfirmationUrl\\s*%>", confirmationPageUrl).Replace("<%\\s*SiteName\\s*%>", newValue);
    }

    private static void SendEmailLongAsync(
      IEmailSender provider,
      string from,
      string recipients,
      string subject,
      string body,
      object userToken)
    {
      try
      {
        provider.Send(from, recipients, subject, body);
      }
      catch (Exception ex)
      {
        Exceptions.HandleException(ex, ExceptionPolicyName.UnhandledExceptions);
      }
    }

    private static void SendEmailShortAsync(
      IEmailSender provider,
      MailMessage message,
      object userToken)
    {
      try
      {
        provider.Send(message);
      }
      catch (Exception ex)
      {
        Exceptions.HandleException(ex, ExceptionPolicyName.UnhandledExceptions);
      }
    }

    private delegate void SendEmailAsyncLongDelegate(
      IEmailSender provider,
      string from,
      string recipients,
      string subject,
      string body,
      object userToken);

    private delegate void SendEmailAsyncShortDelegate(
      IEmailSender provider,
      MailMessage message,
      object userToken);
  }
}
