// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Mail.IEmailSender
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Net.Mail;
using System.Web.UI.WebControls;

namespace Telerik.Sitefinity.Web.Mail
{
  /// <summary>
  /// Abstraction over the minimum email functionality used by Sitefinity
  /// </summary>
  public interface IEmailSender : IDisposable
  {
    /// <summary>Called only once to initialize the email sender.</summary>
    void Initialize();

    /// <summary>Send an email</summary>
    /// <param name="message">Message to send</param>
    void Send(MailMessage message);

    /// <summary>Senda an email</summary>
    /// <param name="from">Address to designate as sender</param>
    /// <param name="to">Address to designate as a receiver. Semi-column delimited</param>
    /// <param name="subject">Subjec of the email</param>
    /// <param name="message">Message body, in HTML</param>
    void Send(string from, string to, string subject, string message);

    /// <summary>Sends an e-mail message to an SMTP server for delivery. The message
    /// sender, recipients, subject, and message body are specified using <see cref="T:System.String" />
    /// objects. This method does not block the calling thread and allows the caller
    /// to pass an object to the method that is invoked when the operation completes.
    /// </summary>
    void SendAsync(string from, string recipients, string subject, string body, object userToken);

    /// <summary>Sends the specified e-mail message to an SMTP server for delivery. This
    /// method does not block the calling thread and allows the caller to pass an object
    /// to the method that is invoked when the operation completes. </summary>
    void SendAsync(MailMessage message, object userToken);

    /// <summary>
    /// Does not throw exceptions, but raises <see cref="E:Telerik.Sitefinity.Web.Mail.IEmailSender.Error" />, instead
    /// </summary>
    /// <param name="message">message to send</param>
    /// <returns>True if succesfully sent, false otherwize</returns>
    bool TrySend(MailMessage message);

    /// <summary>
    /// Does not throw exceptions, but raises <see cref="E:Telerik.Sitefinity.Web.Mail.IEmailSender.Error" />, instead
    /// </summary>
    /// <param name="from">Address to designate as sender</param>
    /// <param name="to">Address to designate as a receiver. Semi-column delimited</param>
    /// <param name="subject">Subjec of the email</param>
    /// <param name="message">Message body, in HTML</param>
    bool TrySend(string from, string to, string subject, string message);

    /// <summary>
    /// Returns whether the rights to send an email are granted.
    /// </summary>
    bool CheckIfHasSmtpPermissions();

    /// <summary>
    /// Get the error message that was set after the last call to <see cref="M:Telerik.Sitefinity.Web.Mail.IEmailSender.CheckIfHasSmtpPermissions" />
    /// </summary>
    string LastErrorMessage { get; }

    /// <summary>
    /// Returns a value indicating whether the provider supports async email sending.
    /// </summary>
    bool SupportsAsyncSending { get; }

    /// <summary>
    /// Raises the event before sending an email, giving the option to cancel the sending
    /// </summary>
    event EventHandler<MailMessageEventArgs> Sending;

    /// <summary>
    /// Raises the event after the email has been succesfully sent
    /// </summary>
    event EventHandler<MailMessageEventArgs> Sent;

    /// <summary>Raises the event in the case of an error</summary>
    event EventHandler<SendMailErrorEventArgs> Error;
  }
}
