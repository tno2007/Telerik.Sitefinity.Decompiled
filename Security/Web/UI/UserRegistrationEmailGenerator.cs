// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.UserRegistrationEmailGenerator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Web;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Mail;
using Telerik.Sitefinity.Web.UI.Templates;

namespace Telerik.Sitefinity.Security.Web.UI
{
  /// <summary>
  /// Creates successful registration and registration confirmation emails
  /// </summary>
  public class UserRegistrationEmailGenerator
  {
    /// <summary>Generates a successful registration email.</summary>
    /// <param name="userManager">The user manager.</param>
    /// <param name="user">The user.</param>
    /// <param name="successEmailTemplateId">The success email template id.</param>
    /// <param name="successEmailSubject">The success email subject.</param>
    /// <returns>The generated successful registration email.</returns>
    public static MailMessage GenerateSuccessfulRegistrationEmail(
      UserManager userManager,
      User user,
      Guid? successEmailTemplateId,
      string successEmailSubject)
    {
      string body = UserRegistrationEmailGenerator.GetEmailMessageBody(successEmailTemplateId) ?? userManager.SuccessfulRegistrationEmailBody;
      return EmailSender.CreateRegistrationSuccessEmail(userManager.SuccessfulRegistrationEmailAddress ?? user.Email, user.Email, user.UserName, successEmailSubject, body);
    }

    /// <summary>Generates a successful registration email.</summary>
    /// <param name="userManager">The user manager.</param>
    /// <param name="user">The user.</param>
    /// <param name="successEmailTemplateId">The success email template id.</param>
    /// <param name="successEmailSubject">The success email subject.</param>
    /// <param name="successfulRegistrationSenderEmail">The success sender email.</param>
    /// <param name="successfulRegistrationSenderName">The success sender name.</param>
    /// <returns>The generated successful registration email.</returns>
    public static MailMessage GenerateSuccessfulRegistrationEmail(
      UserManager userManager,
      User user,
      Guid? successEmailTemplateId,
      string successEmailSubject,
      string successfulRegistrationSenderEmail = null,
      string successfulRegistrationSenderName = null)
    {
      MailMessage registrationEmail;
      if (string.IsNullOrEmpty(successfulRegistrationSenderEmail))
      {
        registrationEmail = UserRegistrationEmailGenerator.GenerateSuccessfulRegistrationEmail(userManager, user, successEmailTemplateId, successEmailSubject);
      }
      else
      {
        string body = UserRegistrationEmailGenerator.GetEmailMessageBody(successEmailTemplateId) ?? userManager.SuccessfulRegistrationEmailBody;
        registrationEmail = EmailSender.CreateRegistrationSuccessEmail(successfulRegistrationSenderEmail, user.Email, user.UserName, successEmailSubject, body);
      }
      if (!string.IsNullOrEmpty(successfulRegistrationSenderName))
        registrationEmail.From = new MailAddress(registrationEmail.From.Address, successfulRegistrationSenderName);
      return registrationEmail;
    }

    /// <summary>Generates a registration confirmation email.</summary>
    /// <param name="userManager">The user manager.</param>
    /// <param name="user">The user.</param>
    /// <param name="membershipProvider">The membership provider.</param>
    /// <param name="confirmationEmailTemplateId">The confirmation email template id.</param>
    /// <param name="confirmationPageUrl">The confirmation page URL.</param>
    /// <param name="confirmationEmailSubject">The confirmation email subject.</param>
    /// <param name="confirmRegistrationSenderEmail">The confirmation sender email.</param>
    /// <param name="confirmRegistrationSenderName">The confirmation sender name.</param>
    /// <returns>The generated registration confirmation email.</returns>
    public static MailMessage GenerateRegistrationConfirmationEmail(
      UserManager userManager,
      User user,
      string membershipProvider,
      Guid? confirmationEmailTemplateId,
      string confirmationPageUrl,
      string confirmationEmailSubject,
      string confirmRegistrationSenderEmail = null,
      string confirmRegistrationSenderName = null)
    {
      MailMessage confirmationEmail;
      if (string.IsNullOrEmpty(confirmRegistrationSenderEmail))
      {
        confirmationEmail = UserRegistrationEmailGenerator.GenerateRegistrationConfirmationEmail(userManager, user, membershipProvider, confirmationEmailTemplateId, confirmationPageUrl, confirmationEmailSubject);
      }
      else
      {
        string body = UserRegistrationEmailGenerator.GetEmailMessageBody(confirmationEmailTemplateId) ?? userManager.ConfirmRegistrationMailBody;
        confirmationEmail = EmailSender.CreateRegistrationConfirmationEmail(confirmRegistrationSenderEmail, user.Email, user.UserName, confirmationPageUrl, confirmationEmailSubject, body);
      }
      if (!string.IsNullOrEmpty(confirmRegistrationSenderName))
        confirmationEmail.From = new MailAddress(confirmationEmail.From.Address, confirmRegistrationSenderName);
      return confirmationEmail;
    }

    /// <summary>Generates a registration confirmation email.</summary>
    /// <param name="userManager">The user manager.</param>
    /// <param name="user">The user.</param>
    /// <param name="membershipProvider">The membership provider.</param>
    /// <param name="confirmationEmailTemplateId">The confirmation email template id.</param>
    /// <param name="confirmationPageUrl">The confirmation page URL.</param>
    /// <param name="confirmationEmailSubject">The confirmation email subject.</param>
    /// <returns>The generated registration confirmation email.</returns>
    public static MailMessage GenerateRegistrationConfirmationEmail(
      UserManager userManager,
      User user,
      string membershipProvider,
      Guid? confirmationEmailTemplateId,
      string confirmationPageUrl,
      string confirmationEmailSubject)
    {
      string body = UserRegistrationEmailGenerator.GetEmailMessageBody(confirmationEmailTemplateId) ?? userManager.ConfirmRegistrationMailBody;
      return EmailSender.CreateRegistrationConfirmationEmail(userManager.SuccessfulRegistrationEmailAddress ?? user.Email, user.Email, user.UserName, confirmationPageUrl, confirmationEmailSubject, body);
    }

    /// <summary>Gets the confirmation page URL.</summary>
    /// <param name="confirmationPageUrl">The confirmation page URL.</param>
    /// <param name="userId">The user id.</param>
    /// <param name="membershipProvider">The membership provider.</param>
    /// <param name="returnUrlName">Name of the return URL.</param>
    /// <param name="defaultReturnUrl">The default return URL.</param>
    /// <returns>Returns the generated confirmation page url.</returns>
    public static string GetConfirmationPageUrl(
      string confirmationPageUrl,
      Guid userId,
      string membershipProvider,
      string returnUrlName,
      string defaultReturnUrl)
    {
      Url url = new Url(confirmationPageUrl);
      url.Query["user"] = HttpUtility.UrlEncode(userId.ToString());
      url.Query["provider"] = HttpUtility.UrlEncode(membershipProvider);
      NameValueCollection queryString = SystemManager.CurrentHttpContext.Request.QueryString;
      if (queryString.Keys.Contains(returnUrlName))
        url.Query[returnUrlName] = HttpUtility.UrlEncode(queryString[returnUrlName]);
      else if (!string.IsNullOrEmpty(defaultReturnUrl))
        url.Query[returnUrlName] = HttpUtility.UrlEncode(defaultReturnUrl);
      return url.ToString();
    }

    /// <summary>Gets the email message body.</summary>
    /// <param name="templateId">The template identifier.</param>
    /// <returns>The email body.</returns>
    private static string GetEmailMessageBody(Guid? templateId)
    {
      if (templateId.HasValue && templateId.Value != Guid.Empty)
      {
        ControlPresentation controlPresentation = PageManager.GetManager().GetPresentationItems<ControlPresentation>().Where<ControlPresentation>((Expression<Func<ControlPresentation, bool>>) (tmpl => tmpl.DataType == "EMAIL_TEMPLATE" && tmpl.Id == templateId.Value)).SingleOrDefault<ControlPresentation>();
        if (controlPresentation != null)
          return ResourceParser.ParseResources(controlPresentation.Data);
      }
      return (string) null;
    }
  }
}
