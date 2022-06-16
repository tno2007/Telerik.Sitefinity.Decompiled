// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Mail.StandardDotNetEmailSender
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Events;
using Telerik.Sitefinity.Services.Notifications;
using Telerik.Sitefinity.Services.Notifications.Configuration;

namespace Telerik.Sitefinity.Web.Mail
{
  /// <summary>
  /// Implements IEmailSender via the standard .net class SmtpClient
  /// </summary>
  public class StandardDotNetEmailSender : 
    IEmailSender,
    IDisposable,
    ISupportNotificationService,
    ISenderProfileVerifiable
  {
    /// <summary>
    /// The sandard .NET implementation for sending and receiving emails
    /// </summary>
    [Obsolete("Use the NotificationService property instead.")]
    protected SmtpClient client;
    /// <summary>SMTP settings for Sitefinity.</summary>
    [Obsolete("Use the SenderProfile property instead.")]
    protected SmtpElement smtpSettings;
    /// <summary>
    /// Last error message that was set in CheckIfHasSmtpPermissions
    /// </summary>
    protected string lastErrorMessage;
    /// <summary>The web name of UTF-8</summary>
    protected const string Utf8 = "utf-8";
    private INotificationService notificationService;
    private ISenderProfile senderProfile;
    private string senderProfileName;
    private ServiceContext context;
    private const string OperationEndKeyPrefix = "NotificationServiceMessageJob|";
    private const string MessageJobDescription = "EmailSender message job";

    /// <summary>
    /// Returns a value indicating whether the provider supports async email sending.
    /// </summary>
    public virtual bool SupportsAsyncSending => false;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.Mail.StandardDotNetEmailSender" /> class.
    /// </summary>
    public StandardDotNetEmailSender() => EventHub.Subscribe<IResultOperationEndEvent<IMessageJobResponse>>(new SitefinityEventHandler<IResultOperationEndEvent<IMessageJobResponse>>(this.OperationEndEventHandler));

    /// <summary>Called only once to initialize the email sender.</summary>
    public void Initialize()
    {
      if (this.senderProfile != null)
        return;
      this.senderProfile = this.SenderProfileName.IsNullOrEmpty() ? this.GetDefaultSenderProfile() : this.GetSenderProfile(this.SenderProfileName);
      if (this.CheckIfHasSmtpPermissions())
        this.lastErrorMessage = string.Empty;
      else
        this.senderProfile = (ISenderProfile) null;
    }

    /// <summary>
    /// The standard implementation does not free any resources
    /// </summary>
    public void Dispose()
    {
      this.Sending = (EventHandler<MailMessageEventArgs>) null;
      this.Sent = (EventHandler<MailMessageEventArgs>) null;
      this.Error = (EventHandler<SendMailErrorEventArgs>) null;
    }

    /// <summary>Send an email</summary>
    /// <param name="message">Message to send</param>
    public virtual void Send(MailMessage message)
    {
      if (this.SenderProfile == null)
        throw new InvalidOperationException(this.LastErrorMessage);
      MailMessageEventArgs args = message != null ? new MailMessageEventArgs(message) : throw new ArgumentNullException(nameof (message));
      if (!this.RaiseSending(args))
        return;
      this.SendInternal(args.Message);
    }

    /// <summary>Senda an email</summary>
    /// <param name="from">Address to designate as sender</param>
    /// <param name="to">Address to designate as a receiver. Semi-column delimited</param>
    /// <param name="subject">Subjec of the email</param>
    /// <param name="message">Message body, in HTML</param>
    public virtual void Send(string from, string to, string subject, string message)
    {
      if (from.IsNullOrWhitespace() || to.IsNullOrWhitespace() || subject.IsNullOrWhitespace() || message.IsNullOrWhitespace())
        throw new ArgumentException();
      MailMessage mailMessage = this.GetMailMessage();
      mailMessage.From = this.GetMailAddress(from);
      string str1 = to;
      char[] separator = new char[1]{ ';' };
      foreach (string str2 in str1.Split(separator, StringSplitOptions.RemoveEmptyEntries))
        mailMessage.To.Add(this.GetMailAddress(str2.Trim()));
      mailMessage.Subject = subject;
      mailMessage.Body = message;
      mailMessage.IsBodyHtml = true;
      this.Send(mailMessage);
    }

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
      if (this.SenderProfile == null)
        throw new InvalidOperationException(this.LastErrorMessage);
      if (from.IsNullOrWhitespace() || recipients.IsNullOrWhitespace() || subject.IsNullOrWhitespace() || body.IsNullOrWhitespace())
        throw new ArgumentException();
      this.Send(from, recipients, subject, body);
    }

    /// <summary>Sends the specified e-mail message to an SMTP server for delivery. This
    /// method does not block the calling thread and allows the caller to pass an object
    /// to the method that is invoked when the operation completes. </summary>
    public void SendAsync(MailMessage message, object userToken)
    {
      if (this.SenderProfile == null)
        throw new InvalidOperationException(this.LastErrorMessage);
      if (message == null)
        throw new ArgumentNullException(nameof (message));
      this.Send(message);
    }

    /// <summary>
    /// Does not throw exceptions, but raises <see cref="E:Telerik.Sitefinity.Web.Mail.StandardDotNetEmailSender.Error" />, instead
    /// </summary>
    /// <param name="message">message to send</param>
    /// <returns>True if succesfully sent, false otherwize</returns>
    public bool TrySend(MailMessage message)
    {
      try
      {
        this.Send(message);
        return true;
      }
      catch (Exception ex)
      {
        this.RaiseError(new SendMailErrorEventArgs(ex));
        return false;
      }
    }

    /// <summary>
    /// Does not throw exceptions, but raises <see cref="E:Telerik.Sitefinity.Web.Mail.StandardDotNetEmailSender.Error" />, instead
    /// </summary>
    /// <param name="from">Address to designate as sender</param>
    /// <param name="to">Address to designate as a receiver. Semi-column delimited</param>
    /// <param name="subject">Subjec of the email</param>
    /// <param name="message">Message body, in HTML</param>
    public bool TrySend(string from, string to, string subject, string message)
    {
      try
      {
        this.Send(from, to, subject, message);
        return true;
      }
      catch (Exception ex)
      {
        this.RaiseError(new SendMailErrorEventArgs(ex));
        return false;
      }
    }

    /// <summary>
    /// Returns whether the rights to send an email are granted.
    /// </summary>
    /// <value>True if user can send an email, false otherwize.</value>
    public bool CheckIfHasSmtpPermissions()
    {
      if (this.NotificationService is ISenderProfileVerifiable notificationService)
      {
        string profileName = this.SenderProfile?.ProfileName;
        string str;
        ref string local = ref str;
        if (notificationService.VerifySenderProfile(profileName, out local))
          return true;
        this.LastErrorMessage = str;
      }
      return false;
    }

    /// <summary>
    /// Get the error message that was set after the last call to <see cref="M:Telerik.Sitefinity.Web.Mail.StandardDotNetEmailSender.CheckIfHasSmtpPermissions" />
    /// </summary>
    /// <value></value>
    public virtual string LastErrorMessage
    {
      get => this.lastErrorMessage;
      set => this.lastErrorMessage = value;
    }

    /// <summary>
    /// Raises the event before sending an email, giving the option to cancel the sending
    /// </summary>
    public event EventHandler<MailMessageEventArgs> Sending;

    /// <summary>Raises the event in the case of an error</summary>
    public event EventHandler<SendMailErrorEventArgs> Error;

    /// <summary>
    /// Raises the event after the email has been succesfully sent
    /// </summary>
    public event EventHandler<MailMessageEventArgs> Sent;

    /// <summary>
    /// Raises the <see cref="E:Telerik.Sitefinity.Web.Mail.StandardDotNetEmailSender.Sending" /> event
    /// </summary>
    /// <param name="args">Event arguemtns</param>
    /// <returns>true if event was not cancelled, false if the event was cancelled</returns>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="args" /> is <c>null</c></exception>
    protected internal virtual bool RaiseSending(MailMessageEventArgs args)
    {
      if (args == null)
        throw new ArgumentNullException(nameof (args));
      if (this.Sending == null)
        return true;
      args.Cancel = false;
      this.Sending((object) this, args);
      return !args.Cancel;
    }

    /// <summary>
    /// Raises the <see cref="E:Telerik.Sitefinity.Web.Mail.StandardDotNetEmailSender.Error" /> event
    /// </summary>
    /// <param name="args">Event arguments</param>
    /// 
    ///             /// <exception cref="T:System.ArgumentNullException">When <paramref name="args" /> is <c>null</c></exception>
    protected internal virtual void RaiseError(SendMailErrorEventArgs args)
    {
      if (args == null)
        throw new ArgumentNullException(nameof (args));
      if (this.Error == null)
        return;
      this.Error((object) this, args);
    }

    /// <summary>
    /// Raises the <see cref="E:Telerik.Sitefinity.Web.Mail.StandardDotNetEmailSender.Sent" /> event
    /// </summary>
    /// <param name="args">Event arguments</param>
    /// 
    ///             /// <exception cref="T:System.ArgumentNullException">When <paramref name="args" /> is <c>null</c></exception>
    protected internal virtual void RaiseSent(MailMessageEventArgs args)
    {
      if (args == null)
        throw new ArgumentNullException(nameof (args));
      if (this.Sent == null)
        return;
      this.Sent((object) this, args);
    }

    /// <inheritdoc />
    public string SenderProfileName
    {
      get => this.senderProfileName;
      set
      {
        this.senderProfileName = value;
        this.senderProfile = (ISenderProfile) null;
      }
    }

    /// <inheritdoc />
    public ISenderProfile SenderProfile
    {
      get => this.senderProfile;
      set => this.senderProfile = value;
    }

    /// <inheritdoc />
    public ServiceContext Context
    {
      get
      {
        if (this.context == null)
          this.context = new ServiceContext((string) null, (string) null);
        return this.context;
      }
      set
      {
        this.context = value;
        this.senderProfile = (ISenderProfile) null;
      }
    }

    /// <inheritdoc />
    public bool VerifySenderProfile(string profileName, out string errorMessage)
    {
      errorMessage = string.Empty;
      return this.NotificationService is ISenderProfileVerifiable notificationService && notificationService.VerifySenderProfile(profileName, out errorMessage);
    }

    protected internal virtual string ClientHost => this.GetSenderProfileProperty("host");

    protected internal virtual int ClientPort
    {
      get
      {
        int result;
        int.TryParse(this.GetSenderProfileProperty("port"), out result);
        return result;
      }
    }

    protected internal virtual bool IsSystemPermissionGranted(IPermission permission) => SecurityManager.IsGranted(permission);

    protected internal virtual string GetResource(string classId, string msgId) => Res.Get(classId, msgId);

    /// <summary>
    /// Builds a NetworkCredential instance with the supplied username, password, and domain parameters
    /// </summary>
    /// <param name="user">the user name</param>
    /// <param name="password">the password value</param>
    /// <param name="domain">the domain value</param>
    /// <returns>a NetworkCredential instance</returns>
    protected virtual NetworkCredential GetCredential(
      string user,
      string password,
      string domain)
    {
      return domain.IsNullOrEmpty() ? new NetworkCredential(user, password) : new NetworkCredential(user, password, domain);
    }

    protected internal virtual void SendInternal(MailMessage message) => this.NotificationService.SendMessage(this.Context, this.GetMessageJob(message), (IDictionary<string, string>) null);

    protected internal virtual MailMessage GetMailMessage() => new MailMessage();

    protected internal virtual MailAddress GetMailAddress(string address) => new MailAddress(address);

    [Obsolete("Use the SenderProfile property instead.")]
    protected internal virtual SmtpElement GetSmtpSettings() => Config.Get<SystemConfig>().SmtpSettings;

    [Obsolete("Use the NotificationService property instead.")]
    protected internal virtual SmtpClient GetSmtpClient(string host, int port) => new SmtpClient(host, port);

    private void OperationEndEventHandler(
      IResultOperationEndEvent<IMessageJobResponse> @event)
    {
      Guid messageJobId;
      if (!this.TryGetMessageJobId((IContextOperationEvent) @event, "NotificationServiceMessageJob|", out messageJobId))
        return;
      if (@event.Status == "Succeeded" && @event.Result != null)
      {
        IMessageJobRequest result = (IMessageJobRequest) @event.Result;
        if (result == null)
          return;
        this.RaiseSent(new MailMessageEventArgs(this.GetMailMessage(result)));
      }
      else
      {
        if (!(@event.Status == MessageJobStatus.Failed.ToString()))
          return;
        this.RaiseError(new SendMailErrorEventArgs((Exception) new InvalidOperationException(string.Format("Could not send message with job Id {0}", (object) messageJobId))));
      }
    }

    private ISenderProfile GetSenderProfile(string profileName) => this.NotificationService.GetSenderProfile(this.Context, profileName);

    private ISenderProfile GetDefaultSenderProfile()
    {
      ISenderProfile defaultSenderProfile = (ISenderProfile) null;
      try
      {
        defaultSenderProfile = this.NotificationService.GetDefaultSenderProfile(this.Context, "smtp");
      }
      catch
      {
      }
      return defaultSenderProfile;
    }

    private string GetSenderProfileProperty(string key)
    {
      string senderProfileProperty = (string) null;
      if (this.senderProfile != null && this.senderProfile.CustomProperties != null)
        this.senderProfile.CustomProperties.TryGetValue(key, out senderProfileProperty);
      return senderProfileProperty;
    }

    private bool TryGetMessageJobId(
      IContextOperationEvent @event,
      string keyPrefix,
      out Guid messageJobId)
    {
      messageJobId = Guid.Empty;
      return @event.OperationKey.StartsWith(keyPrefix) && Guid.TryParse(@event.OperationKey.Replace(keyPrefix, string.Empty), out messageJobId);
    }

    private MailMessage GetMailMessage(IMessageJobRequest messageJob)
    {
      string from = messageJob != null ? messageJob.SenderEmailAddress : throw new ArgumentNullException(nameof (messageJob));
      IEnumerable<ISubscriberRequest> subscribers = messageJob.Subscribers;
      IEnumerable<string> recipients = subscribers != null ? subscribers.Select<ISubscriberRequest, string>((Func<ISubscriberRequest, string>) (x => x.Email)) : (IEnumerable<string>) null;
      string subject = messageJob.MessageTemplate?.Subject;
      string bodyHtml = messageJob.MessageTemplate?.BodyHtml;
      return this.GetMailMessage(from, recipients, subject, bodyHtml);
    }

    private MailMessage GetMailMessage(
      string from,
      string to,
      string subject,
      string message)
    {
      string[] strArray;
      if (to == null)
        strArray = (string[]) null;
      else
        strArray = to.Split(new char[1]{ ';' }, StringSplitOptions.RemoveEmptyEntries);
      string[] recipients = strArray;
      return this.GetMailMessage(from, (IEnumerable<string>) recipients, subject, message);
    }

    private MailMessage GetMailMessage(
      string from,
      IEnumerable<string> recipients,
      string subject,
      string message)
    {
      MailMessage mailMessage = new MailMessage();
      mailMessage.From = new MailAddress(from);
      foreach (string recipient in recipients)
        mailMessage.To.Add(recipient.Trim());
      mailMessage.Subject = subject;
      mailMessage.Body = message;
      mailMessage.IsBodyHtml = true;
      return mailMessage;
    }

    private IMessageJobRequest GetMessageJob(MailMessage msg)
    {
      if (msg == null)
        throw new ArgumentNullException(nameof (msg));
      string address = msg.From?.Address;
      MailAddressCollection to = msg.To;
      IEnumerable<string> recipients = to != null ? to.Select<MailAddress, string>((Func<MailAddress, string>) (x => x.Address)) : (IEnumerable<string>) null;
      string subject = msg.Subject;
      string body = msg.Body;
      string senderName = msg.Sender != null ? msg.Sender.Address : (string) null;
      return this.GetMessageJob(address, recipients, subject, body, senderName);
    }

    private IMessageJobRequest GetMessageJob(
      string from,
      IEnumerable<string> recipients,
      string subject,
      string message,
      string senderName = null)
    {
      IEnumerable<SubscriberRequestProxy> subscriberRequestProxies = recipients.Select<string, SubscriberRequestProxy>((Func<string, SubscriberRequestProxy>) (x => new SubscriberRequestProxy()
      {
        Email = x.Trim()
      }));
      MessageTemplateRequestProxy templateRequestProxy = new MessageTemplateRequestProxy()
      {
        BodyHtml = message,
        Subject = subject
      };
      return (IMessageJobRequest) new MessageJobRequestProxy()
      {
        MessageTemplate = (IMessageTemplateRequest) templateRequestProxy,
        Description = "EmailSender message job",
        Subscribers = (IEnumerable<ISubscriberRequest>) subscriberRequestProxies,
        SenderEmailAddress = from,
        SenderProfileName = this.SenderProfileName,
        SenderName = senderName,
        ClearSubscriptionData = true
      };
    }

    /// <summary>
    /// The <see cref="T:Telerik.Sitefinity.Services.Notifications.INotificationService" /> implementation for sending and receiving emails
    /// </summary>
    protected INotificationService NotificationService
    {
      get
      {
        if (this.notificationService == null)
          this.notificationService = SystemManager.GetNotificationService();
        return this.notificationService;
      }
    }
  }
}
