// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.UserManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Configuration.Provider;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Web;
using System.Web.Security;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Abstractions.TemporaryStorage;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.DataSource;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Data;
using Telerik.Sitefinity.Security.Events;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Security.Web.UI;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Configuration;
using Telerik.Sitefinity.Services.Events;
using Telerik.Sitefinity.Services.Notifications;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.DataResolving;
using Telerik.Sitefinity.Web.Mail;

namespace Telerik.Sitefinity.Security
{
  /// <summary>
  /// Represents an intermediary between users and membership data providers.
  /// </summary>
  public class UserManager : ManagerBase<MembershipDataProvider>, IDataSource
  {
    private ConfigElementDictionary<string, DataProviderSettings> providerSettings;
    internal const string PasswordRecoveryQueryStringKey = "cp";
    internal const string PasswordRecoveryQueryStringValue = "pr";
    internal const string PasswordRecoveryUserValidationQueryStringKey = "vk";
    internal const char PasswordRecoveryUserValidationCharSeparator = ',';
    private static readonly object usersCacheSync = new object();
    private static CacheProfileElement userCacheSettings = (CacheProfileElement) null;
    private static readonly Dictionary<Guid, bool> existingUsers = new Dictionary<Guid, bool>();

    /// <summary>
    /// Initializes a new instance of <see cref="T:Telerik.Sitefinity.Security.UserManager" /> class with the default provider.
    /// </summary>
    public UserManager()
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="T:Telerik.Sitefinity.Security.UserManager" /> class and sets the specified provider.
    /// </summary>
    /// <param name="providerName">
    /// The name of the provider. If empty string or null the default provider is set.
    /// </param>
    public UserManager(string providerName)
      : base(providerName)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.UserManager" /> class.
    /// </summary>
    /// <param name="providerName">
    /// The name of the provider. If empty string or null the default provider is set
    /// </param>
    /// <param name="transactionName">
    /// The name of a distributed transaction. If empty string or null this manager will use separate transaction.
    /// </param>
    public UserManager(string providerName, string transactionName)
      : base(providerName, transactionName)
    {
    }

    /// <summary>
    /// Indicates whether the membership provider is configured to allow users to reset their passwords.
    /// </summary>
    /// <value></value>
    /// <returns>true if the membership provider supports password reset; otherwise, false. The default is true.
    /// </returns>
    public virtual bool EnablePasswordReset => this.Provider.EnablePasswordReset;

    /// <summary>
    /// Indicates whether the membership provider is configured to allow users to retrieve their passwords.
    /// </summary>
    /// <value></value>
    /// <returns>true if the membership provider is configured to support password retrieval; otherwise, false. The default is false.
    /// </returns>
    public virtual bool EnablePasswordRetrieval => this.Provider.EnablePasswordRetrieval;

    /// <summary>
    /// Gets the number of invalid password or password-answer attempts allowed before the membership user is locked out.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// The number of invalid password or password-answer attempts allowed before the membership user is locked out.
    /// </returns>
    public virtual int MaxInvalidPasswordAttempts => this.Provider.MaxInvalidPasswordAttempts;

    /// <summary>
    /// Gets the minimum number of special characters that must be present in a valid password.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// The minimum number of special characters that must be present in a valid password.
    /// </returns>
    public virtual int MinRequiredNonAlphanumericCharacters => this.Provider.MinRequiredNonAlphanumericCharacters;

    /// <summary>Gets the minimum length required for a password.</summary>
    /// <value></value>
    /// <returns>The minimum length required for a password.</returns>
    public virtual int MinRequiredPasswordLength => this.Provider.MinRequiredPasswordLength;

    /// <summary>
    /// Gets the number of minutes in which a maximum number of invalid password or password-answer attempts are allowed before the membership user is locked out.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// The number of minutes in which a maximum number of invalid password or password-answer attempts are allowed before the membership user is locked out.
    /// </returns>
    public virtual int PasswordAttemptWindow => this.Provider.PasswordAttemptWindow;

    /// <summary>
    /// Gets a value indicating the format for storing passwords in the membership data store.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.Security.MembershipPasswordFormat" /> values indicating the format for storing passwords in the data store.
    /// </returns>
    public virtual MembershipPasswordFormat PasswordFormat => this.Provider.PasswordFormat;

    /// <summary>
    /// Gets a value indicating whether the membership provider is configured to require the user to answer a password question for password reset and retrieval.
    /// </summary>
    /// <value></value>
    /// <returns>true if a password answer is required for password reset and retrieval; otherwise, false. The default is true.
    /// </returns>
    public virtual bool RequiresQuestionAndAnswer => this.Provider.RequiresQuestionAndAnswer;

    /// <summary>
    /// Gets a value indicating whether the membership provider is configured to require a unique e-mail address for each user name.
    /// </summary>
    /// <value></value>
    /// <returns>true if the membership provider requires a unique e-mail address; otherwise, false. The default is true.
    /// </returns>
    public virtual bool RequiresUniqueEmail => this.Provider.RequiresUniqueEmail;

    /// <summary>
    /// Gets the recovery mail address. User will receive email with recovered password from this email address.
    /// </summary>
    /// <value>The recovery mail address.</value>
    public virtual string RecoveryMailAddress => this.Provider.RecoveryMailAddress;

    /// <summary>
    /// Gets the recovery mail body. User will receive password recovery mail with this body.
    /// </summary>
    /// <value>The recovery mail body.</value>
    public virtual string RecoveryMailBody => this.Provider.RecoveryMailBody;

    /// <summary>
    /// Gets the recovery mail subject. User will receive password recovery mail with this subject.
    /// </summary>
    /// <value>The recovery mail subject.</value>
    public virtual string RecoveryMailSubject => this.Provider.RecoveryMailSubject;

    /// <summary>
    /// Gets the recovery mail address. User will receive email with recovered password from this email address.
    /// </summary>
    public virtual string ConfirmationEmailAddress => this.Provider.ConfirmationEmailAddress;

    /// <summary>
    /// Gets the body of the email sent to the client for registration confirmation.
    /// </summary>
    public virtual string ConfirmRegistrationMailBody => this.Provider.ConfirmRegistrationMailBody;

    /// <summary>
    /// Gets the subject of the email sent to the client for registration confirmation.
    /// </summary>
    public virtual string ConfirmRegistrationMailSubject => this.Provider.ConfirmRegistrationMailSubject;

    /// <summary>
    /// Gets or sets the successful registration email address. User will receive confirmation email from this email address.
    /// </summary>
    public virtual string SuccessfulRegistrationEmailAddress => this.Provider.SuccessfulRegistrationEmailAddress;

    /// <summary>
    /// Gets or sets the body of the email sent to the client after a successful registration.
    /// </summary>
    public virtual string SuccessfulRegistrationEmailBody => this.Provider.SuccessfulRegistrationEmailBody;

    /// <summary>
    /// Gets or sets the subject of the email sent to the client after a successful registration.
    /// </summary>
    public virtual string SuccessfulRegistrationMailSubject => this.Provider.SuccessfulRegistrationMailSubject;

    private static CacheProfileElement UsersCacheSettings
    {
      get
      {
        if (UserManager.userCacheSettings == null)
          UserManager.userCacheSettings = Config.Get<SecurityConfig>().UsersCache;
        return UserManager.userCacheSettings;
      }
    }

    /// <summary>
    /// Gets a value indicating if the specified user already exists.
    /// </summary>
    /// <param name="userName">The username.</param>
    /// <returns>true if exists else false.</returns>
    public virtual bool UserExists(string userName) => this.Provider.UserExists(userName);

    /// <summary>
    /// Gets a value indicating if the specified email already exists.
    /// </summary>
    /// <param name="userName">Name of the user.</param>
    /// <returns></returns>
    public virtual bool EmailExists(string email) => this.Provider.EmailExists(email);

    /// <summary>Gets the user with the specified username.</summary>
    /// <param name="roleName">The username of the user.</param>
    /// <returns></returns>
    public virtual User GetUser(string userName) => this.Provider.GetUser(userName);

    /// <summary>
    /// Gets the user associated with the specified e-mail address.
    /// </summary>
    /// <param name="email">The e-mail address to search for.</param>
    /// <returns>
    /// The user associated with the specified e-mail address. If no match is found, return null.
    /// </returns>
    public virtual User GetUserByEmail(string email) => this.Provider.GetUserByEmail(email);

    /// <summary>Changes the password.</summary>
    /// <param name="userId">The user identity.</param>
    /// <param name="oldPassword">The old password.</param>
    /// <param name="newPassword">The new password.</param>
    /// <returns></returns>
    public virtual bool ChangePassword(Guid userId, string oldPassword, string newPassword) => this.Provider.ChangePassword(userId, oldPassword, newPassword);

    /// <summary>Changes the password.</summary>
    /// <param name="userName">The username of the user.</param>
    /// <param name="oldPassword">The old password.</param>
    /// <param name="newPassword">The new password.</param>
    /// <returns></returns>
    public virtual bool ChangePassword(string userName, string oldPassword, string newPassword) => this.Provider.ChangePassword(userName, oldPassword, newPassword);

    /// <summary>Changes the password.</summary>
    /// <param name="user">The user object.</param>
    /// <param name="oldPassword">The old password.</param>
    /// <param name="newPassword">The new password.</param>
    /// <returns></returns>
    public virtual bool ChangePassword(User user, string oldPassword, string newPassword) => this.Provider.ChangePassword(user, oldPassword, newPassword);

    /// <summary>
    /// Changes the password for a user and also sends a "password changed" email. This method also supresses security checks so that it will succeed
    /// even if the current user does not have Manage Users permissions. This is necessary for non-admin users to be able to change their own password.
    /// </summary>
    /// <param name="userId">The user id.</param>
    /// <param name="oldPassword">The old password.</param>
    /// <param name="newPassword">The new password.</param>
    /// <returns></returns>
    public static void ChangePasswordForUser(
      UserManager manager,
      Guid userId,
      string oldPassword,
      string newPassword,
      bool sendEmail = false)
    {
      User user = manager.GetUser(userId);
      if (!manager.ChangePassword(user, oldPassword, newPassword))
        throw new ProviderException(Res.Get<ErrorMessages>().ChangePasswordGeneralFailureText);
      bool flag = userId == SecurityManager.GetCurrentUserId();
      try
      {
        if (flag)
          manager.Provider.SuppressSecurityChecks = true;
        manager.SaveChanges();
      }
      finally
      {
        if (flag)
          manager.Provider.SuppressSecurityChecks = false;
      }
      if (!sendEmail)
        return;
      UserManager.SendPasswordMail(manager, user, newPassword);
    }

    /// <summary>
    /// Method for change security question and answer for not authorized users also.
    /// </summary>
    /// <param name="manager">Provider manager</param>
    /// <param name="userId">The user Id</param>
    /// <param name="password">The password</param>
    /// <param name="newQuestion">New question</param>
    /// <param name="newAnswer">New answer</param>
    internal static void ChangePasswordQuestionAndAnswerForNotAuthUsers(
      UserManager manager,
      Guid userId,
      string password,
      string newQuestion,
      string newAnswer)
    {
      if (!manager.ChangePasswordQuestionAndAnswer(userId, password, newQuestion, newAnswer))
        throw new ProviderException(Res.Get<ErrorMessages>().ChangePasswordQuestionAndAnswerFailureText);
      bool flag = userId == SecurityManager.GetCurrentUserId();
      try
      {
        if (flag)
          manager.Provider.SuppressSecurityChecks = true;
        manager.SaveChanges();
      }
      finally
      {
        if (flag)
          manager.Provider.SuppressSecurityChecks = false;
      }
    }

    /// <summary>Sends the password mail.</summary>
    /// <param name="manager">The manager.</param>
    /// <param name="user">The user.</param>
    /// <param name="newPassword">The new password.</param>
    public static void SendPasswordMail(UserManager manager, User user, string newPassword = null)
    {
      string password = newPassword ?? user.Password;
      MailMessage passwordMail = EmailSender.CreatePasswordMail(manager.RecoveryMailAddress, user.Email, user.UserName, password, manager.RecoveryMailSubject, manager.RecoveryMailBody);
      EmailSender emailSender = EmailSender.Get();
      emailSender.SenderProfileName = Config.Get<SecurityConfig>().Notifications.SenderProfile;
      emailSender.TrySend(passwordMail);
    }

    /// <summary>Finds the user by id.</summary>
    /// <param name="email">The id.</param>
    /// <returns></returns>
    internal static User FindUserById(Guid userId) => UserManager.FindUserById(userId, out string _);

    /// <summary>Finds the user by id.</summary>
    /// <param name="userId">The id.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns></returns>
    internal static User FindUserById(Guid userId, out string providerName)
    {
      foreach (DataProviderBase dataProviderBase in UserManager.GetManager().GetContextProviders().Union<DataProviderBase>((IEnumerable<DataProviderBase>) ManagerBase<MembershipDataProvider>.StaticProvidersCollection).Distinct<DataProviderBase>())
      {
        User user = UserManager.GetManager(dataProviderBase.Name).GetUser(userId);
        if (user != null)
        {
          providerName = dataProviderBase.Name;
          return user;
        }
      }
      providerName = (string) null;
      return (User) null;
    }

    /// <summary>Sends the recovery password mail.</summary>
    /// <param name="manager">The manager.</param>
    /// <param name="user">The user.</param>
    /// <param name="newPassword">The new password.</param>
    public static void SendRecoveryPasswordMail(
      UserManager manager,
      string userEmail,
      string confirmationPageUrl)
    {
      if (manager == null)
        throw new ArgumentNullException(nameof (manager));
      if (string.IsNullOrEmpty(userEmail))
        throw new ArgumentException("User email cannot be null or an empty string.");
      if (string.IsNullOrEmpty(confirmationPageUrl))
        throw new ArgumentException("Confirmation page URL cannot be null or an empty string.");
      User userByEmail = manager.GetUserByEmail(userEmail);
      if (userByEmail == null)
        throw new ArgumentException("Invalid user email.");
      string passwordEmailBody = Res.Get<Labels>().LostPasswordEmailBody;
      string str1 = manager.RecoveryMailSubject;
      if (string.IsNullOrEmpty(str1))
        str1 = Res.Get<Labels>().PasswordRecovery;
      confirmationPageUrl = RouteHelper.ResolveUrl(confirmationPageUrl, UrlResolveOptions.Absolute);
      string validationKeyEncoded = UserManager.GetUserValidationKeyEncoded(userByEmail);
      string newValue = confirmationPageUrl + "?" + "vk" + "=" + validationKeyEncoded + "&" + "cp" + "=" + "pr";
      string host = SystemManager.GetSiteUri().Host;
      string userDisplayName = UserProfilesHelper.GetUserDisplayName(userByEmail.Id);
      string str2 = passwordEmailBody.Replace("<%\\s*UserName\\s*%>", HttpUtility.HtmlEncode(userByEmail.UserName)).Replace("<%\\s*ConfirmationUrl\\s*%>", newValue).Replace("<%\\s*SiteName\\s*%>", HttpUtility.HtmlEncode(host)).Replace("<%\\s*UserDisplayName\\s*%>", HttpUtility.HtmlEncode(userDisplayName));
      ServiceContext context = new ServiceContext((string) null, (string) null);
      MessageTemplateRequestProxy templateRequestProxy = new MessageTemplateRequestProxy()
      {
        Subject = str1,
        BodyHtml = str2
      };
      ISubscriberRequest subscriberObject = SecurityManager.GetSubscriberObject(userByEmail);
      MessageJobRequestProxy messageJob = new MessageJobRequestProxy()
      {
        Description = string.Format("Password recovery for user '{0}'", (object) userByEmail.UserName),
        MessageTemplate = (IMessageTemplateRequest) templateRequestProxy,
        Subscribers = (IEnumerable<ISubscriberRequest>) new ISubscriberRequest[1]
        {
          subscriberObject
        }
      };
      if (!manager.RecoveryMailAddress.IsNullOrEmpty())
      {
        messageJob.SenderEmailAddress = manager.RecoveryMailAddress;
        messageJob.SenderName = manager.RecoveryMailAddress;
      }
      SystemManager.GetNotificationService().SendMessage(context, (IMessageJobRequest) messageJob, (IDictionary<string, string>) null);
      PasswordRecoveryRequested recoveryRequested = new PasswordRecoveryRequested();
      recoveryRequested.UserId = userByEmail.Id;
      recoveryRequested.UserName = userByEmail.UserName;
      recoveryRequested.Email = userByEmail.Email;
      recoveryRequested.IsApproved = userByEmail.IsApproved;
      recoveryRequested.MembershipProviderName = userByEmail.ProviderName;
      EventHub.Raise((IEvent) recoveryRequested, false);
    }

    private static string GetUserValidationKeyEncoded(User user)
    {
      string str = SecurityManager.EncryptData(string.Format("{0}{1}{2}{3}{4}", (object) user.ProviderName, (object) ',', (object) user.Id, (object) ',', (object) DateTime.UtcNow));
      ObjectFactory.Resolve<ITemporaryStorage>().AddOrUpdate(str, string.Empty, DateTime.UtcNow.AddHours(1.0));
      return str.UrlEncode();
    }

    /// <summary>Shows if a email should be send on password change.</summary>
    /// <param name="user">The user.</param>
    /// <param name="providerType">Type of the provider.</param>
    /// <returns>
    /// <c>true</c> if password mail should be sent; otherwise, <c>false</c>.
    /// </returns>
    public static bool ShouldSendPasswordEmail(User user, Type providerType) => user != null && (!string.IsNullOrEmpty(user.Password) || typeof (MembershipProviderWrapper).IsAssignableFrom(providerType));

    /// <summary>
    /// Verifies that the specified user and password exist in the data source.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <param name="password">The password for the specified user.</param>
    /// <returns>
    /// true if the specified username and password are valid; otherwise, false.
    /// </returns>
    public virtual bool ValidateUser(Guid userId, string password) => this.Provider.ValidateUser(userId, password);

    /// <summary>
    /// Verifies that the specified user and password exist in the data source.
    /// </summary>
    /// <param name="userName">The username of the user to validate.</param>
    /// <param name="password">The password for the specified user.</param>
    /// <returns>
    /// true if the specified username and password are valid; otherwise, false.
    /// </returns>
    public virtual bool ValidateUser(string userName, string password) => this.Provider.ValidateUser(userName, password);

    /// <summary>
    /// Verifies that the specified user and password exist in the data source.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <param name="password">The password for the specified user.</param>
    /// <returns>
    /// true if the specified username and password are valid; otherwise, false.
    /// </returns>
    public virtual bool ValidateUser(User user, string password) => this.Provider.ValidateUser(user, password);

    /// <summary>
    /// Processes a request to update the password question and answer for a membership user.
    /// </summary>
    /// <param name="pageId">The pageId.</param>
    /// <param name="password">The password for the specified user.</param>
    /// <param name="newPasswordQuestion">The new password question for the specified user.</param>
    /// <param name="newPasswordAnswer">The new password answer for the specified user.</param>
    /// <returns>
    /// true if the password question and answer are updated successfully; otherwise, false.
    /// </returns>
    public virtual bool ChangePasswordQuestionAndAnswer(
      Guid id,
      string password,
      string newPasswordQuestion,
      string newPasswordAnswer)
    {
      return this.Provider.ChangePasswordQuestionAndAnswer(id, password, newPasswordQuestion, newPasswordAnswer);
    }

    /// <summary>
    /// Processes a request to update the password question and answer for a membership user.
    /// </summary>
    /// <param name="userName">The user to change the password question and answer for.</param>
    /// <param name="password">The password for the specified user.</param>
    /// <param name="newPasswordQuestion">The new password question for the specified user.</param>
    /// <param name="newPasswordAnswer">The new password answer for the specified user.</param>
    /// <returns>
    /// true if the password question and answer are updated successfully; otherwise, false.
    /// </returns>
    public virtual bool ChangePasswordQuestionAndAnswer(
      string userName,
      string password,
      string newPasswordQuestion,
      string newPasswordAnswer)
    {
      return this.Provider.ChangePasswordQuestionAndAnswer(userName, password, newPasswordQuestion, newPasswordAnswer);
    }

    /// <summary>
    /// Processes a request to update the password question and answer for a membership user.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <param name="password">The password for the specified user.</param>
    /// <param name="newPasswordQuestion">The new password question for the specified user.</param>
    /// <param name="newPasswordAnswer">The new password answer for the specified user.</param>
    /// <returns>
    /// true if the password question and answer are updated successfully; otherwise, false.
    /// </returns>
    public virtual bool ChangePasswordQuestionAndAnswer(
      User user,
      string password,
      string newPasswordQuestion,
      string newPasswordAnswer)
    {
      return this.Provider.ChangePasswordQuestionAndAnswer(user, password, newPasswordQuestion, newPasswordAnswer);
    }

    public virtual User CreateUser(
      string email,
      string password,
      string passwordQuestion,
      string passwordAnswer,
      bool isApproved,
      object providerUserKey,
      out MembershipCreateStatus status)
    {
      return this.Provider.CreateUser(email, password, passwordQuestion, passwordAnswer, isApproved, providerUserKey, out status);
    }

    /// <summary>Adds a new membership user to the data source.</summary>
    /// <param name="username">The user name for the new user.</param>
    /// <param name="password">The password for the new user.</param>
    /// <param name="email">The e-mail address for the new user.</param>
    /// <param name="passwordQuestion">The password question for the new user.</param>
    /// <param name="passwordAnswer">The password answer for the new user</param>
    /// <param name="isApproved">Whether or not the new user is approved to be validated.</param>
    /// <param name="providerUserKey">The unique identifier from the membership data source for the user.</param>
    /// <param name="status">A <see cref="T:System.Web.Security.MembershipCreateStatus" /> enumeration value indicating whether the user was created successfully.</param>
    /// <returns>
    /// A <see cref="T:Telerik.Sitefinity.Security.Model.User" /> object populated with the information for the newly created user.
    /// </returns>
    public virtual User CreateUser(
      string username,
      string password,
      string email,
      string passwordQuestion,
      string passwordAnswer,
      bool isApproved,
      object providerUserKey,
      out MembershipCreateStatus status)
    {
      return this.Provider.CreateUser(username, password, email, passwordQuestion, passwordAnswer, isApproved, providerUserKey, out status);
    }

    /// <summary>
    /// Gets the number of users currently accessing the application.
    /// </summary>
    /// <returns>
    /// The number of users currently accessing the application.
    /// </returns>
    public virtual int GetNumberOfUsersOnline() => this.Provider.GetNumberOfUsersOnline();

    /// <summary>
    /// Gets the password for the specified user from the data source.
    /// </summary>
    /// <param name="userId">The pageId of the user for which the password ought to be retrieved.</param>
    /// <param name="answer">The password answer for the user.</param>
    /// <returns>The password for the specified user name.</returns>
    public virtual string GetPassword(Guid userId, string answer) => this.Provider.GetPassword(userId, answer);

    /// <summary>
    /// Gets the password for the specified user name from the data source.
    /// </summary>
    /// <param name="userName">The user to retrieve the password for.</param>
    /// <param name="answer">The password answer for the user.</param>
    /// <returns>The password for the specified user name.</returns>
    public virtual string GetPassword(string userName, string answer) => this.Provider.GetPassword(userName, answer);

    /// <summary>
    /// Gets the password for the specified user name from the data source.
    /// </summary>
    /// <param name="user">
    /// An instance of the <see cref="T:Telerik.Sitefinity.Security.Model.User" /> object for which the password ought to be retrieved.
    /// </param>
    /// <param name="answer">The password answer for the user.</param>
    /// <returns>The password for the specified user name.</returns>
    public virtual string GetPassword(User user, string answer) => this.Provider.GetPassword(user, answer);

    /// <summary>
    /// Resets a user's password to a new, automatically generated password.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <param name="answer">The password answer for the specified user.</param>
    /// <returns>The new password for the specified user.</returns>
    public virtual string ResetPassword(Guid userId, string answer) => this.Provider.ResetPassword(userId, answer);

    /// <summary>
    /// Resets a user's password to a new, automatically generated password.
    /// </summary>
    /// <param name="userName">The user to reset the password for.</param>
    /// <param name="answer">The password answer for the specified user.</param>
    /// <returns>The new password for the specified user.</returns>
    public virtual string ResetPassword(string userName, string answer) => this.Provider.ResetPassword(userName, answer);

    /// <summary>
    /// Resets a user's password to a new, automatically generated password.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <param name="answer">The password answer for the specified user.</param>
    /// <returns>The new password for the specified user.</returns>
    public virtual string ResetPassword(User user, string answer) => this.Provider.ResetPassword(user, answer);

    /// <summary>
    /// Clears a lock so that the membership user can be validated.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <returns>
    /// true if the membership user was successfully unlocked; otherwise, false.
    /// </returns>
    public virtual bool UnlockUser(Guid userId) => this.Provider.UnlockUser(userId);

    /// <summary>
    /// Clears a lock so that the membership user can be validated.
    /// </summary>
    /// <param name="userName">The membership user whose lock status you want to clear.</param>
    /// <returns>
    /// true if the membership user was successfully unlocked; otherwise, false.
    /// </returns>
    public virtual bool UnlockUser(string userName) => this.Provider.UnlockUser(userName);

    /// <summary>
    /// Clears a lock so that the membership user can be validated.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <returns>
    /// true if the membership user was successfully unlocked; otherwise, false.
    /// </returns>
    public virtual bool UnlockUser(User user) => this.Provider.UnlockUser(user);

    /// <summary>Creates new user with the specified email.</summary>
    /// <param name="email">The email of the user.</param>
    /// <returns>The new user.</returns>
    public virtual User CreateUser(string email) => this.Provider.CreateUser(email);

    /// <summary>
    /// Creates new user with the specified identity and email.
    /// </summary>
    /// <param name="id">The identity of the new user.</param>
    /// <param name="email">the email of the user.</param>
    /// <returns>The new user.</returns>
    public virtual User CreateUser(Guid id, string email) => this.Provider.CreateUser(id, email);

    /// <summary>Gets the user with the specified identity.</summary>
    /// <param name="pageId">The identity to search for.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Security.Model.User" />.</returns>
    public virtual User GetUser(Guid id) => this.Provider.GetUser(id);

    /// <summary>Tries to gets the user with the specified identity.</summary>
    /// <param name="pageId">The identity to search for.</param>
    /// <returns>True if user exists and parameter user is set to the user, False is user does not exist and parameter user is set to null</returns>
    public virtual bool TryGetUser(Guid id, out User user)
    {
      try
      {
        user = this.Provider.GetUser(id);
      }
      catch (ItemNotFoundException ex)
      {
        user = (User) null;
        return false;
      }
      return user != null;
    }

    /// <summary>Gets a query for users.</summary>
    /// <returns>The query for users.</returns>
    public virtual IQueryable<User> GetUsers() => this.Provider.GetUsers();

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The item to delete.</param>
    public virtual void Delete(User item)
    {
      UserActivityManager.RegisterDeletedUser(item);
      this.Provider.Delete(item);
    }

    /// <summary>
    /// Gets the name of the default provider for this manager.
    /// </summary>
    protected internal override GetDefaultProvider DefaultProviderDelegate => (GetDefaultProvider) (() => Config.Get<SecurityConfig>().DefaultBackendMembershipProvider);

    /// <summary>
    /// Gets the name of the module to which this manager belongs.
    /// </summary>
    public override string ModuleName => "Security";

    /// <summary>Gets all provider settings.</summary>
    protected internal override ConfigElementDictionary<string, DataProviderSettings> ProvidersSettings
    {
      get
      {
        if (this.providerSettings == null)
          this.providerSettings = Config.Get<SecurityConfig>().MembershipProviders;
        return this.providerSettings;
      }
    }

    /// <summary>
    /// This method is called after data provider initialization,
    /// when the manager is instantiated for the first time in the application lifecycle.
    /// </summary>
    protected override void OnInitialized()
    {
      this.StaticProviders.Unlock();
      foreach (MembershipProvider provider in (ProviderCollection) Membership.Providers)
      {
        if (provider is SitefinityMembershipProvider membershipProvider)
        {
          MembershipDataProvider membershipDataProvider;
          if (!this.StaticProviders.TryGetValue(membershipProvider.Name, out membershipDataProvider))
            throw new ConfigurationErrorsException("Invalid provider name \"{0}\" for SitefinityMembershipProvider specified in web.config file. The name should match one of the providers configured in Sitefinity's Security.config configuration.".Arrange((object) membershipProvider.Name));
          membershipProvider.DataProvider = membershipDataProvider;
        }
        else
        {
          if (this.Providers.Contains(provider.Name))
          {
            using (MembershipDataProvider staticProvider = this.StaticProviders[provider.Name])
            {
              this.Providers.Remove(provider.Name);
              ObjectFactory.Container.Teardown((object) staticProvider);
            }
          }
          this.StaticProviders.Add((MembershipDataProvider) new MembershipProviderWrapper(provider));
          Type type = this.GetType();
          ObjectFactory.Container.RegisterType(type, type, provider.Name.ToUpperInvariant(), (LifetimeManager) new HttpRequestLifetimeManager(), (InjectionMember) new InjectionConstructor(new object[1]
          {
            (object) provider.Name
          }));
        }
      }
      this.StaticProviders.Lock();
    }

    public override IEnumerable<DataProviderBase> GetContextProviders()
    {
      IEnumerable<DataProviderBase> contextProviders = base.GetContextProviders();
      return contextProviders.Any<DataProviderBase>() ? contextProviders : (IEnumerable<DataProviderBase>) this.StaticProviders;
    }

    /// <summary>Gets an instance for RoleManager.</summary>
    /// <returns>An instance of UserManager.</returns>
    public static UserManager GetManager() => ManagerBase<MembershipDataProvider>.GetManager<UserManager>();

    /// <summary>
    /// Gets an instance for UserManager for the specified data provider.
    /// </summary>
    /// <param name="providerName">The name of the data provider.</param>
    /// <returns>An instance of UserManager.</returns>
    public static UserManager GetManager(string providerName) => ManagerBase<MembershipDataProvider>.GetManager<UserManager>(providerName);

    /// <summary>
    /// Gets an instance for RoleManager for the specified data provider.
    /// </summary>
    /// <param name="providerName">The name of the data provider.</param>
    /// <returns>An instance of UserManager.</returns>
    /// <param name="transactionName">Name of the transaction.</param>
    /// <returns></returns>
    public static UserManager GetManager(string providerName, string transactionName) => ManagerBase<MembershipDataProvider>.GetManager<UserManager>(providerName, transactionName);

    /// <summary>
    /// Searches all data providers for the specified username.
    /// </summary>
    /// <param name="username">The username.</param>
    /// <returns></returns>
    public static User FindUser(string username)
    {
      foreach (MembershipDataProvider membershipDataProvider in UserManager.GetManager().GetContextProviders().Union<DataProviderBase>((IEnumerable<DataProviderBase>) ManagerBase<MembershipDataProvider>.StaticProvidersCollection))
      {
        User user = membershipDataProvider.GetUser(username);
        if (user != null)
          return user;
      }
      return (User) null;
    }

    /// <summary>
    /// Searches all data providers for the specified user ID.
    /// </summary>
    /// <param name="username">The username.</param>
    /// <returns></returns>
    public static User FindUser(Guid id)
    {
      if (id == Guid.Empty)
        return (User) null;
      foreach (DataProviderBase dataProviderBase in UserManager.GetManager().GetContextProviders().Union<DataProviderBase>((IEnumerable<DataProviderBase>) ManagerBase<MembershipDataProvider>.StaticProvidersCollection).Distinct<DataProviderBase>())
      {
        try
        {
          User user = UserManager.GetManager(dataProviderBase.Name).GetUser(id);
          if (user != null)
          {
            if (user.ApplicationName.Equals(dataProviderBase.ApplicationName))
              return user;
          }
        }
        catch (Exception ex)
        {
          if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
            throw ex;
        }
      }
      return (User) null;
    }

    /// <summary>
    /// Checks in all data providers if a user with a specified username exists.
    /// </summary>
    /// <param name="username">the username to search</param>
    /// <returns>true if the user exists, false otherwise</returns>
    public static bool UserExistsInAnyProvider(string username)
    {
      foreach (MembershipDataProvider membershipDataProvider in UserManager.GetManager().GetContextProviders().Cast<MembershipDataProvider>().Union<MembershipDataProvider>((IEnumerable<MembershipDataProvider>) ManagerBase<MembershipDataProvider>.StaticProvidersCollection).Distinct<MembershipDataProvider>())
      {
        if (membershipDataProvider.UserExists(username))
          return true;
      }
      return false;
    }

    /// <summary>
    /// Checks in all data providers if a user with a specified id exists.
    /// </summary>
    /// <param name="id">the user id to search</param>
    /// <returns>true if the user exists, false otherwise</returns>
    public static bool UserExistsInAnyProvider(Guid id)
    {
      if (UserManager.existingUsers.ContainsKey(id))
        return UserManager.existingUsers[id];
      bool flag = UserManager.FindUser(id) != null;
      lock (UserManager.existingUsers)
      {
        if (!UserManager.existingUsers.ContainsKey(id))
          UserManager.existingUsers.Add(id, flag);
      }
      return flag;
    }

    /// <summary>
    /// Searches all data providers for the specified usernames.
    /// </summary>
    /// <param name="usernames">The usernames.</param>
    /// <returns></returns>
    public static IList<User> FindUsers(string[] usernames)
    {
      List<User> users = new List<User>(usernames.Length);
      foreach (MembershipDataProvider membershipDataProvider in UserManager.GetManager().GetContextProviders().Cast<MembershipDataProvider>())
      {
        IQueryable<User> collection = membershipDataProvider.GetUsers().Where<User>((Expression<Func<User, bool>>) (u => usernames.Contains<string>(u.UserName)));
        users.AddRange((IEnumerable<User>) collection);
      }
      return (IList<User>) users;
    }

    /// <summary>
    /// Gets user's display name from his/her (possibly cached) profile.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    [Obsolete("Use UserProfilesHelper.GetUserDisplayName instead.")]
    public static string GetUserDisplayName(User user) => UserProfilesHelper.GetUserDisplayName(user.Id);

    public static IEnumerable<string> GetReadOnlyFields(
      string profileType,
      string externalProviderName)
    {
      return ObjectFactory.IsTypeRegistered<IUserPropertiesStrategy>() ? ObjectFactory.Resolve<IUserPropertiesStrategy>().GetReadOnlyFields(profileType, externalProviderName) : (IEnumerable<string>) new string[0];
    }

    public static bool IsEmailMapped(string externalProviderName) => UserManager.GetExternalProvidersMapping()[externalProviderName].ContainsKey("Email");

    internal static IDictionary<string, Dictionary<string, List<string>>> GetExternalProvidersMapping() => ObjectFactory.IsTypeRegistered<IUserPropertiesStrategy>() ? ObjectFactory.Resolve<IUserPropertiesStrategy>().GetExternalProvidersMapping() : (IDictionary<string, Dictionary<string, List<string>>>) new Dictionary<string, Dictionary<string, List<string>>>();

    /// <summary>
    /// Gets the common user information from a cached item.
    /// If no such item exists tries to create one.
    /// <para>If there is no available information - returns null.</para>
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    internal static UserManager.UserProfileProxy GetCachedUserProfile(Guid userId)
    {
      string key = userId.ToString();
      object cachedUserProfile = UserManager.UserProfilesCache[key];
      if (cachedUserProfile == null)
      {
        lock (UserManager.usersCacheSync)
        {
          cachedUserProfile = UserManager.UserProfilesCache[key];
          if (cachedUserProfile == null)
          {
            User user = UserManager.FindUser(userId);
            if (user != null)
            {
              UserManager.UserProfileProxy userProfileProxy = new UserManager.UserProfileProxy()
              {
                UserId = userId,
                UserName = user.UserName,
                Email = user.Email,
                RegistrationDate = user.CreationDate,
                IsExternalUser = !string.IsNullOrEmpty(user.ExternalProviderName),
                ExternalProviderName = user.ExternalProviderName
              };
              UserProfileManager userProfileManager = UserProfilesHelper.GetUserProfileManager(typeof (SitefinityProfile), "SF_UserCache");
              using (new ReadUncommitedRegion((IManager) userProfileManager))
              {
                if (userProfileManager.GetUserProfile(user.Id, typeof (SitefinityProfile).FullName) is SitefinityProfile userProfile)
                {
                  string profileUrl = UserManager.GetProfileUrl(userProfile);
                  userProfileProxy.FirstName = userProfile.FirstName;
                  userProfileProxy.LastName = userProfile.LastName;
                  userProfileProxy.Nickname = userProfile.Nickname;
                  userProfileProxy.About = userProfile.About;
                  userProfileProxy.ItemUrl = profileUrl;
                  userProfileProxy.PreferredLanguage = userProfile.PreferredLanguage;
                  userProfileProxy.AvatarUrl = UserProfilesHelper.GetAvatarThumbnailUrl(userProfile);
                  userProfileProxy.Preferences = userProfile.Preferences;
                  foreach (PropertyDescriptor propertyDescriptor in FieldHelper.GetFields(typeof (SitefinityProfile), true).Where<PropertyDescriptor>((Func<PropertyDescriptor, bool>) (propDesc => propDesc is MetafieldPropertyDescriptor || propDesc is ContentLinksPropertyDescriptor)))
                  {
                    if (propertyDescriptor.GetType().IsAssignableFrom(typeof (ContentLinksPropertyDescriptor)))
                    {
                      if (propertyDescriptor.GetValue((object) userProfile) is ContentLink source)
                      {
                        ContentLink contentLink = new ContentLink(source);
                        userProfileProxy.SetValue(propertyDescriptor.Name, (object) contentLink);
                      }
                    }
                    else
                      userProfileProxy.SetValue(propertyDescriptor.Name, propertyDescriptor.GetValue((object) userProfile));
                  }
                }
                else
                {
                  userProfileProxy.FirstName = user.FirstName;
                  userProfileProxy.LastName = user.LastName;
                }
                TimeSpan timeSpan = TimeSpan.FromSeconds((double) UserManager.UsersCacheSettings.Duration);
                ICacheItemExpiration[] cacheItemExpirationArray = new ICacheItemExpiration[3]
                {
                  !UserManager.UsersCacheSettings.SlidingExpiration ? (ICacheItemExpiration) new AbsoluteTime(timeSpan) : (ICacheItemExpiration) new SlidingTime(timeSpan),
                  (ICacheItemExpiration) new DataItemCacheDependency(typeof (User), user.Id),
                  (ICacheItemExpiration) (userProfile == null ? new DataItemCacheDependency(typeof (SitefinityProfile), string.Empty) : new DataItemCacheDependency(typeof (SitefinityProfile), userProfile.Id))
                };
                UserManager.UserProfilesCache.Add(key, (object) userProfileProxy, CacheItemPriority.Normal, (ICacheItemRefreshAction) null, cacheItemExpirationArray);
                cachedUserProfile = (object) userProfileProxy;
              }
            }
            else
            {
              cachedUserProfile = new object();
              TimeSpan timeSpan = TimeSpan.FromSeconds((double) UserManager.UsersCacheSettings.Duration);
              ICacheItemExpiration[] cacheItemExpirationArray = new ICacheItemExpiration[2]
              {
                !UserManager.UsersCacheSettings.SlidingExpiration ? (ICacheItemExpiration) new AbsoluteTime(timeSpan) : (ICacheItemExpiration) new SlidingTime(timeSpan),
                (ICacheItemExpiration) new DataItemCacheDependency(typeof (SitefinityProfile), (string) null)
              };
              UserManager.UserProfilesCache.Add(key, cachedUserProfile, CacheItemPriority.Normal, (ICacheItemRefreshAction) null, cacheItemExpirationArray);
            }
          }
        }
      }
      return cachedUserProfile as UserManager.UserProfileProxy;
    }

    private static string GetProfileUrl(SitefinityProfile profile)
    {
      IDataItem dataItem = (IDataItem) profile;
      return (dataItem == null || dataItem.Provider == null ? (UrlDataProviderBase) ManagerBase.GetMappedManager(profile.GetType()).Provider : (UrlDataProviderBase) dataItem.Provider).GetItemUrl((ILocatable) profile);
    }

    private static ICacheManager UserProfilesCache => SystemManager.GetCacheManager(CacheManagerInstance.Users);

    /// <inheritdoc />
    internal string CreateProvider(
      string providerTitle,
      string validationKey,
      NameValueCollection parameters)
    {
      ConfigManager manager = ConfigManager.GetManager();
      SecurityConfig section = manager.GetSection<SecurityConfig>();
      DataProviderSettings membershipProvider = section.MembershipProviders[section.DefaultBackendMembershipProvider];
      DataProviderSettings providerSettings = new DataProviderSettings()
      {
        Name = providerTitle,
        Title = providerTitle,
        ProviderType = membershipProvider.ProviderType,
        Parameters = new NameValueCollection(membershipProvider.Parameters)
      };
      providerSettings.Parameters["applicationName"] = string.Format("{0}{1}", (object) providerTitle, (object) "Backend/");
      if (parameters != null)
      {
        foreach (object key in parameters.Keys)
          providerSettings.Parameters[key.ToString()] = parameters[key.ToString()];
      }
      providerSettings.Parameters[nameof (validationKey)] = validationKey;
      section.MembershipProviders.Add(providerSettings);
      manager.SaveSection((ConfigSection) section);
      this.InstatiateProvider((IDataProviderSettings) providerSettings);
      return providerTitle;
    }

    /// <summary>Gets the current user time zone.</summary>
    /// <returns></returns>
    public TimeZoneInfo GetUserTimeZone() => SystemManager.CurrentContext.GetCurrentTimeZone();

    /// <summary>
    /// Indicate if to use user browser settings for calculating dates.
    /// </summary>
    /// <returns></returns>
    public bool GetUserBrowserSettingsForCalculatingDates()
    {
      TimeZoneUISettings timeZoneSettings = Config.Get<SystemConfig>().UITimeZoneSettings;
      return timeZoneSettings == null || timeZoneSettings.UserBrowserSettingsForCalculatingDates;
    }

    string IDataSource.Name => this.GetType().FullName;

    string IDataSource.ModuleName => "Users";

    string IDataSource.Title => "Users";

    IEnumerable<DataProviderInfo> IDataSource.ProviderInfos => this.GetProviderInfos<MembershipDataProvider>(this.StaticProviders.Where<MembershipDataProvider>((Func<MembershipDataProvider, bool>) (p => !p.IsSystemProvider())));

    /// <inheritdoc />
    IEnumerable<DataProviderInfo> IDataSource.Providers => this.GetProviderInfos<MembershipDataProvider>(this.StaticProviders.Where<MembershipDataProvider>((Func<MembershipDataProvider, bool>) (p => !p.IsSystemProvider())));

    bool IDataSource.CanCreateProvider => true;

    string[] IDataSource.DependantDataSources { get; }

    string IDataSource.ProviderNameDefaultPrefix => "membership";

    string IDataSource.CreateProvider(
      string providerName,
      string providerTitle,
      NameValueCollection parameters)
    {
      return this.CreateProvider(providerTitle, providerName, parameters);
    }

    void IDataSource.DeleteProvider(string providerName) => throw new NotImplementedException();

    void IDataSource.EnableProvider(string providerName) => throw new NotImplementedException();

    void IDataSource.DisableProvider(string providerName) => throw new NotImplementedException();

    string IDataSource.GetDefaultProvider() => ManagerBase<MembershipDataProvider>.GetDefaultProviderName();

    /// <summary>
    /// A lightweight object that contains the minimum information about user profile that is most frequently used
    /// </summary>
    internal class UserProfileProxy : CacheableItem, ICacheUserProfile, ISimpleLocatable
    {
      private Lazy<JObject> cachedPreferences;

      public UserProfileProxy() => this.cachedPreferences = new Lazy<JObject>(new Func<JObject>(this.ResolvePreferences));

      /// <inheritdoc />
      public Guid UserId { get; set; }

      /// <inheritdoc />
      public string UserName { get; set; }

      /// <inheritdoc />
      public string Email { get; set; }

      /// <inheritdoc />
      public string Nickname { get; set; }

      /// <inheritdoc />
      public string FirstName { get; set; }

      /// <inheritdoc />
      public string LastName { get; set; }

      /// <inheritdoc />
      public bool IsExternalUser { get; set; }

      /// <inheritdoc />
      public string About { get; set; }

      /// <inheritdoc />
      public DateTime RegistrationDate { get; set; }

      /// <inheritdoc />
      public string ItemUrl { get; set; }

      /// <inheritdoc />
      public string PreferredLanguage { get; set; }

      /// <inheritdoc />
      public string AvatarUrl { get; set; }

      /// <inheritdoc />
      public string Preferences { get; set; }

      /// <inheritdoc />
      public string ExternalProviderName { get; set; }

      public TType GetPreference<TType>(string key, TType defaultValueIfEmpty)
      {
        JToken jtoken;
        return this.cachedPreferences.Value != null && this.cachedPreferences.Value.TryGetValue(key, out jtoken) ? (TType) jtoken.ToObject(typeof (TType)) : defaultValueIfEmpty;
      }

      private JObject ResolvePreferences() => !string.IsNullOrEmpty(this.Preferences) ? JObject.Parse(this.Preferences) : (JObject) null;
    }
  }
}
