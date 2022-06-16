// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Data.MembershipDataProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Web.Security;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Events;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Events;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Security.Data
{
  /// <summary>
  /// Represents data provider base class for Sitefinity membership services.
  /// </summary>
  public abstract class MembershipDataProvider : DataProviderBase, IDataEventProvider
  {
    private int minRequiredPasswordLength;
    private string passwordStrengthRegularExpression;
    private bool requiresQuestionAndAnswer;
    private bool enablePasswordReset;
    private string recoveryMailBody;
    private string confirmRegistrationMailBody;
    private string confirmRegistrationMailSubject;
    private string successfulRegistrationEmailBody;
    private string successfulRegistrationMailSubject;
    private int newPasswordLength;
    private string[] supportedPermissionSets = new string[1]
    {
      "Backend"
    };

    /// <summary>
    /// Gets the number of invalid password or password-answer attempts allowed before the membership user is locked out.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// The number of invalid password or password-answer attempts allowed before the membership user is locked out.
    /// </returns>
    public virtual int MaxInvalidPasswordAttempts { get; private set; }

    /// <summary>
    /// Gets the minimum number of special characters that must be present in a valid password.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// The minimum number of special characters that must be present in a valid password.
    /// </returns>
    public virtual int MinRequiredNonAlphanumericCharacters { get; private set; }

    /// <summary>Gets the minimum length required for a password.</summary>
    /// <value></value>
    /// <returns>The minimum length required for a password.</returns>
    public virtual int MinRequiredPasswordLength
    {
      get => this.minRequiredPasswordLength;
      private set => this.minRequiredPasswordLength = value;
    }

    /// <summary>
    /// Gets the number of minutes in which a maximum number of invalid password or password-answer attempts are allowed before the membership user is locked out.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// The number of minutes in which a maximum number of invalid password or password-answer attempts are allowed before the membership user is locked out.
    /// </returns>
    public virtual int PasswordAttemptWindow { get; private set; }

    /// <summary>
    /// Gets a value indicating the format for storing passwords in the membership data store.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.Security.MembershipPasswordFormat" /> values indicating the format for storing passwords in the data store.
    /// </returns>
    public virtual MembershipPasswordFormat PasswordFormat { get; private set; }

    /// <summary>
    /// Gets the regular expression used to evaluate a password.
    /// </summary>
    /// <value></value>
    /// <returns>A regular expression used to evaluate a password.</returns>
    public virtual string PasswordStrengthRegularExpression
    {
      get => this.passwordStrengthRegularExpression;
      private set => this.passwordStrengthRegularExpression = value;
    }

    /// <summary>
    /// Gets a value indicating whether the membership provider is configured to require the user to answer a password question for password reset and retrieval.
    /// </summary>
    /// <value></value>
    /// <returns>true if a password answer is required for password reset and retrieval; otherwise, false. The default is true.
    /// </returns>
    public virtual bool RequiresQuestionAndAnswer
    {
      get => this.requiresQuestionAndAnswer;
      private set => this.requiresQuestionAndAnswer = value;
    }

    /// <summary>
    /// Gets a value indicating whether the membership provider is configured to require a unique e-mail address for each user name.
    /// </summary>
    /// <value></value>
    /// <returns>true if the membership provider requires a unique e-mail address; otherwise, false. The default is true.
    /// </returns>
    [Obsolete("Parameter is no longer used, the email must be always unique")]
    public virtual bool RequiresUniqueEmail => true;

    /// <summary>
    /// Indicates whether the membership provider is configured to allow users to reset their passwords.
    /// </summary>
    /// <value></value>
    /// <returns>true if the membership provider supports password reset; otherwise, false. The default is true.
    /// </returns>
    public virtual bool EnablePasswordReset
    {
      get => this.enablePasswordReset;
      private set => this.enablePasswordReset = value;
    }

    /// <summary>
    /// Gets or sets the recovery mail address. User will receive email with recovered password from this email address.
    /// </summary>
    /// <value>The recovery mail address.</value>
    public virtual string RecoveryMailAddress { get; private set; }

    /// <summary>
    /// Gets or sets the recovery mail body. User will receive password recovery mail with this body.
    /// </summary>
    /// <value>The recovery mail body.</value>
    public virtual string RecoveryMailBody => string.IsNullOrEmpty(this.recoveryMailBody) ? Res.Get<ErrorMessages>().CreateUserWizardDefaultBody : this.recoveryMailBody;

    /// <summary>
    /// Gets or sets the recovery mail subject. User will receive password recovery mail with this subject.
    /// </summary>
    /// <value>The recovery mail subject.</value>
    public virtual string RecoveryMailSubject { get; private set; }

    /// <summary>
    /// Gets or sets the confirmation mail address. User will receive confirmation email from this email address.
    /// </summary>
    public virtual string ConfirmationEmailAddress { get; private set; }

    /// <summary>
    /// Gets the body of the email sent to the client for registration confirmation.
    /// </summary>
    public virtual string ConfirmRegistrationMailBody => string.IsNullOrEmpty(this.confirmRegistrationMailBody) ? Res.Get<ErrorMessages>().ConfirmRegistrationMailBody : this.confirmRegistrationMailBody;

    /// <summary>
    /// Gets the subject of the email sent to the client for registration confirmation.
    /// </summary>
    public virtual string ConfirmRegistrationMailSubject => string.IsNullOrEmpty(this.confirmRegistrationMailSubject) ? Res.Get<ErrorMessages>().ConfirmRegistrationMailSubject : this.confirmRegistrationMailSubject;

    /// <summary>
    /// Gets the successful registration email address. User will receive confirmation email from this email address.
    /// </summary>
    public virtual string SuccessfulRegistrationEmailAddress { get; private set; }

    /// <summary>
    /// Gets the body of the email sent to the client after a successful registration.
    /// </summary>
    public virtual string SuccessfulRegistrationEmailBody => string.IsNullOrEmpty(this.successfulRegistrationEmailBody) ? Res.Get<ErrorMessages>().SuccessfulRegistrationEmailBody : this.successfulRegistrationEmailBody;

    /// <summary>
    /// Gets the subject of the email sent to the client after a successful registration.
    /// </summary>
    public virtual string SuccessfulRegistrationMailSubject => string.IsNullOrEmpty(this.successfulRegistrationMailSubject) ? Res.Get<ErrorMessages>().SuccessfulRegistrationMailSubject : this.successfulRegistrationMailSubject;

    /// <summary>
    /// Gets or sets the key that is used to validate encrypted/hashed data, or the process by which the key is generated.
    /// </summary>
    public virtual string ValidationKey { get; private set; }

    /// <summary>
    /// Indicates whether the membership provider is configured to allow users to retrieve their passwords.
    /// </summary>
    /// <value></value>
    /// <returns>true if the membership provider is configured to support password retrieval; otherwise, false. The default is false.
    /// </returns>
    public virtual bool EnablePasswordRetrieval { get; private set; }

    /// <summary>The length used when generating new passwords</summary>
    public virtual int NewPasswordLength
    {
      get => this.newPasswordLength;
      private set => this.newPasswordLength = value;
    }

    /// <summary>
    /// Gets the length of time, in minutes, before a user is no longer considered to be online.
    /// </summary>
    public virtual TimeSpan UserIsOnlineTimeWindow => Config.Get<SecurityConfig>().UserIsOnlineTimeWindow;

    /// <summary>
    /// Gets a value indicating whether to check for updates for the provider during the installation.
    /// </summary>
    /// <value><c>true</c> if [check for updates]; otherwise, <c>false</c>.</value>
    public override bool CheckForUpdates => false;

    /// <summary>
    /// Gets a value indicating if the specified user already exists.
    /// </summary>
    /// <param name="userName">The username.</param>
    /// <returns>true if exists else false.</returns>
    public virtual bool UserExists(string userName) => this.GetUsers().Where<User>((Expression<Func<User, bool>>) (u => u.UserName == userName)).Any<User>();

    /// <summary>
    /// Gets a value indicating if the specified email already exists.
    /// </summary>
    /// <param name="email">The email address that ought to be checked.</param>
    /// <returns>If email already exists returns true; otherwise false.</returns>
    public virtual bool EmailExists(string email) => this.GetUsers().Where<User>((Expression<Func<User, bool>>) (u => u.Email == email)).Any<User>();

    /// <summary>Gets the user with the specified username.</summary>
    /// <param name="userName">Name of the user.</param>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Security.Model.User" /> object with specified userName.</returns>
    public virtual User GetUser(string userName) => this.GetUsers().Where<User>((Expression<Func<User, bool>>) (u => u.UserName == userName)).SingleOrDefault<User>();

    /// <summary>
    /// Gets the user associated with the specified e-mail address.
    /// </summary>
    /// <param name="email">The e-mail address to search for.</param>
    /// <returns>
    /// The user associated with the specified e-mail address. If no match is found, return null.
    /// </returns>
    public virtual User GetUserByEmail(string email) => this.GetUsers().Where<User>((Expression<Func<User, bool>>) (u => u.Email == email && u.Password != default (string) && u.Salt != default (string))).FirstOrDefault<User>();

    /// <summary>Changes the password.</summary>
    /// <param name="userId">The user identity.</param>
    /// <param name="oldPassword">The old password.</param>
    /// <param name="newPassword">The new password.</param>
    /// <returns></returns>
    public virtual bool ChangePassword(Guid userId, string oldPassword, string newPassword) => this.ChangePassword(this.GetUser(userId), oldPassword, newPassword);

    /// <summary>Changes the password.</summary>
    /// <param name="userName">The username of the user.</param>
    /// <param name="oldPassword">The old password.</param>
    /// <param name="newPassword">The new password.</param>
    /// <returns></returns>
    public virtual bool ChangePassword(string userName, string oldPassword, string newPassword) => this.ChangePassword(this.GetUser(userName), oldPassword, newPassword);

    /// <summary>Changes the password.</summary>
    /// <param name="user">The user object.</param>
    /// <param name="oldPassword">The old password.</param>
    /// <param name="newPassword">The new password.</param>
    /// <returns></returns>
    public virtual bool ChangePassword(User user, string oldPassword, string newPassword)
    {
      if (user == null)
        throw new ProviderException(Res.Get<ErrorMessages>().MembershipUserNotFound);
      if (!this.CheckValidPassword(user, oldPassword))
        throw new ProviderException(Res.Get<ErrorMessages>().InvalidPassword);
      if (newPassword == oldPassword)
        throw new ProviderException(Res.Get<ErrorMessages>().NewAndOldPasswordsAreEqual);
      this.ValidatePassword(newPassword, true);
      MembershipPasswordFormat passwordFormat = (MembershipPasswordFormat) user.PasswordFormat;
      user.Password = this.EncodePassword(newPassword, user.Salt, passwordFormat);
      user.SetLastPasswordChangedDate(DateTime.UtcNow);
      if (user.IsLockedOut)
        user.SetIsLockedOut(false);
      return true;
    }

    /// <summary>
    /// Verifies that the specified user and password exist in the data source.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <param name="password">The password for the specified user.</param>
    /// <returns>
    /// true if the specified username and password are valid; otherwise, false.
    /// </returns>
    public virtual bool ValidateUser(Guid userId, string password) => this.ValidateUser(this.GetUser(userId), password);

    /// <summary>
    /// Verifies that the specified user and password exist in the data source.
    /// </summary>
    /// <param name="userName">The username of the user to validate.</param>
    /// <param name="password">The password for the specified user.</param>
    /// <returns>
    /// true if the specified username and password are valid; otherwise, false.
    /// </returns>
    public virtual bool ValidateUser(string userName, string password) => this.ValidateUser(this.GetUser(userName), password);

    /// <summary>
    /// Verifies that the specified user and password exist in the data source.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <param name="password">The password for the specified user.</param>
    /// <returns>
    /// true if the specified username and password are valid; otherwise, false.
    /// </returns>
    public virtual bool ValidateUser(User user, string password)
    {
      if (user == null)
        return false;
      int num = this.CheckValidPassword(user, password) ? 1 : 0;
      if (num == 0)
      {
        this.UpdateFailureCount(user, nameof (password));
        return num != 0;
      }
      user.LastLoginDate = DateTime.UtcNow;
      user.FailedPasswordAttemptWindowStart = DateTime.UtcNow;
      user.FailedPasswordAttemptCount = 0;
      return num != 0;
    }

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
      return this.ChangePasswordQuestionAndAnswer(this.GetUser(id), password, newPasswordQuestion, newPasswordAnswer);
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
      return this.ChangePasswordQuestionAndAnswer(this.GetUser(userName), password, newPasswordQuestion, newPasswordAnswer);
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
      if (!this.ValidateUser(user, password))
        return false;
      int passwordFormat = user.PasswordFormat;
      user.SetPasswordQuestion(newPasswordQuestion);
      user.PasswordAnswer = this.EncodePassword(newPasswordAnswer.ToUpperInvariant(), (string) null, this.PasswordFormat);
      return true;
    }

    /// <summary>Adds a new membership user to the data source.</summary>
    /// <param name="email">The e-mail address for the new user.</param>
    /// <param name="password">The password for the new user.</param>
    /// <param name="passwordQuestion">The password question for the new user.</param>
    /// <param name="passwordAnswer">The password answer for the new user</param>
    /// <param name="isApproved">Whether or not the new user is approved to be validated.</param>
    /// <param name="providerUserKey">The unique identifier from the membership data source for the user.</param>
    /// <param name="status">A <see cref="T:System.Web.Security.MembershipCreateStatus" /> enumeration value indicating whether the user was created successfully.</param>
    /// <returns>
    /// A <see cref="T:Telerik.Sitefinity.Security.Model.User" /> object populated with the information for the newly created user.
    /// </returns>
    public virtual User CreateUser(
      string email,
      string password,
      string passwordQuestion,
      string passwordAnswer,
      bool isApproved,
      object providerUserKey,
      out MembershipCreateStatus status)
    {
      return this.CreateUser(email, password, email, passwordQuestion, passwordAnswer, isApproved, providerUserKey, out status);
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
      if (!this.ValidateParameters(ref username, ref password, ref email, ref passwordQuestion, ref passwordAnswer, ref providerUserKey, out status))
        return (User) null;
      string salt = this.GenerateSalt();
      DateTime utcNow = DateTime.UtcNow;
      User user = this.CreateUser((Guid) providerUserKey, email);
      if (this is MembershipProviderWrapper)
      {
        user.Password = password;
        user.PasswordAnswer = passwordAnswer;
      }
      else
      {
        user.Password = this.EncodePassword(password, salt, this.PasswordFormat);
        user.PasswordAnswer = this.EncodePassword(passwordAnswer.ToUpperInvariant(), (string) null, this.PasswordFormat);
      }
      user.SetUserName(username);
      user.Salt = salt;
      user.Comment = string.Empty;
      user.IsApproved = isApproved;
      user.FailedPasswordAttemptCount = 0;
      user.FailedPasswordAttemptWindowStart = utcNow;
      user.FailedPasswordAnswerAttemptCount = 0;
      user.FailedPasswordAnswerAttemptWindowStart = utcNow;
      user.PasswordFormat = (int) this.PasswordFormat;
      user.SetPasswordQuestion(passwordQuestion);
      user.SetCreationDate(DateTime.Now);
      return user;
    }

    /// <summary>
    /// Gets the number of users currently accessing the application.
    /// </summary>
    /// <returns>
    /// The number of users currently accessing the application.
    /// </returns>
    public virtual int GetNumberOfUsersOnline()
    {
      DateTime compareTime = DateTime.UtcNow.Subtract(this.UserIsOnlineTimeWindow);
      compareTime = compareTime.ToUniversalTime();
      return this.GetUsers().Where<User>((Expression<Func<User, bool>>) (e => e.LastActivityDate > compareTime)).Count<User>();
    }

    /// <summary>
    /// Gets the password for the specified user from the data source.
    /// </summary>
    /// <param name="userId">The pageId of the user for which the password should be retrieved.</param>
    /// <param name="answer">The password answer for the user.</param>
    /// <returns>The password for the specified user name.</returns>
    public virtual string GetPassword(Guid userId, string answer) => this.GetPassword(this.GetUser(userId), answer);

    /// <summary>
    /// Gets the password for the specified user name from the data source.
    /// </summary>
    /// <param name="userName">The user to retrieve the password for.</param>
    /// <param name="answer">The password answer for the user.</param>
    /// <returns>The password for the specified user name.</returns>
    public virtual string GetPassword(string userName, string answer) => this.GetPassword(this.GetUser(userName), answer);

    /// <summary>
    /// Gets the password for the specified user name from the data source.
    /// </summary>
    /// <param name="user">
    /// An instance of <see cref="T:Telerik.Sitefinity.Security.Model.User" /> object for which the passoword should be retrieved.
    /// </param>
    /// <param name="answer">The password answer for the user.</param>
    /// <returns>The password for the specified user name.</returns>
    public virtual string GetPassword(User user, string answer)
    {
      if (!this.EnablePasswordRetrieval)
        throw new ProviderException(Res.Get<ErrorMessages>().PasswordRetrievalNotEnabled);
      MembershipPasswordFormat passwordFormat = user != null ? (MembershipPasswordFormat) user.PasswordFormat : throw new ProviderException(Res.Get<ErrorMessages>().MembershipUserNotFound);
      if (passwordFormat == MembershipPasswordFormat.Hashed)
        throw new ProviderException(Res.Get<ErrorMessages>().CannotRetrieveHashedPasswords);
      if (user.IsLockedOut)
        throw new ProviderException(Res.Get<ErrorMessages>().MembershipUserLocked);
      if (this.RequiresQuestionAndAnswer)
      {
        if (string.IsNullOrEmpty(answer))
        {
          this.UpdateFailureCount(user, nameof (answer));
          throw new ProviderException(Res.Get<ErrorMessages>().PasswordAnswerRequired);
        }
        string passwordAnswer = user.PasswordAnswer;
        if (!this.CheckValidPassword(answer.Trim().ToUpperInvariant(), passwordAnswer, passwordFormat))
        {
          this.UpdateFailureCount(user, nameof (answer));
          throw new ProviderException(Res.Get<ErrorMessages>().WrongPasswordAnswer);
        }
      }
      string salt = user.Salt;
      string str = this.DecodePassword(user.Password, passwordFormat);
      return str.Left(str.Length - salt.Length);
    }

    /// <summary>
    /// Resets a user's password to a new, automatically generated password.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <param name="answer">The password answer for the specified user.</param>
    /// <returns>The new password for the specified user.</returns>
    public virtual string ResetPassword(Guid userId, string answer) => this.ResetPassword(this.GetUser(userId), answer);

    /// <summary>
    /// Resets a user's password to a new, automatically generated password.
    /// </summary>
    /// <param name="userName">The user to reset the password for.</param>
    /// <param name="answer">The password answer for the specified user.</param>
    /// <returns>The new password for the specified user.</returns>
    public virtual string ResetPassword(string userName, string answer) => this.ResetPassword(this.GetUser(userName), answer);

    /// <summary>
    /// Resets a user's password to a new, automatically generated password.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <param name="answer">The password answer for the specified user.</param>
    /// <returns>The new password for the specified user.</returns>
    public virtual string ResetPassword(User user, string answer)
    {
      if (!this.EnablePasswordReset)
        throw new NotSupportedException(Res.Get<ErrorMessages>().PasswordResetIsNotEnabled);
      MembershipPasswordFormat passwordFormat = user != null ? (MembershipPasswordFormat) user.PasswordFormat : throw new ProviderException(Res.Get<ErrorMessages>().MembershipUserNotFound);
      if (this.RequiresQuestionAndAnswer && !ClaimsManager.IsUnrestricted())
      {
        if (string.IsNullOrEmpty(answer))
        {
          this.UpdateFailureCount(user, nameof (answer));
          throw new ProviderException(Res.Get<ErrorMessages>().PasswordAnswerRequired);
        }
        string passwordAnswer = user.PasswordAnswer;
        if (!this.CheckValidPassword(answer.Trim().ToUpperInvariant(), passwordAnswer, passwordFormat))
        {
          this.UpdateFailureCount(user, nameof (answer));
          throw new ProviderException(Res.Get<ErrorMessages>().WrongPasswordAnswer);
        }
      }
      string password = Membership.GeneratePassword(this.NewPasswordLength, this.MinRequiredNonAlphanumericCharacters);
      string salt = user.Salt;
      string str = this.EncodePassword(password, salt, passwordFormat);
      user.Password = str;
      user.SetLastPasswordChangedDate(DateTime.UtcNow);
      if (user.IsLockedOut)
        user.SetIsLockedOut(false);
      return password;
    }

    /// <summary>
    /// Clears a lock so that the membership user can be validated.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <returns>
    /// true if the membership user was successfully unlocked; otherwise, false.
    /// </returns>
    public virtual bool UnlockUser(Guid userId) => this.UnlockUser(this.GetUser(userId));

    /// <summary>
    /// Clears a lock so that the membership user can be validated.
    /// </summary>
    /// <param name="userName">The membership user whose lock status you want to clear.</param>
    /// <returns>
    /// true if the membership user was successfully unlocked; otherwise, false.
    /// </returns>
    public virtual bool UnlockUser(string userName) => this.UnlockUser(this.GetUser(userName));

    /// <summary>
    /// Clears a lock so that the membership user can be validated.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <returns>
    /// true if the membership user was successfully unlocked; otherwise, false.
    /// </returns>
    public virtual bool UnlockUser(User user)
    {
      if (user == null)
        return false;
      user.SetIsLockedOut(false);
      user.FailedPasswordAttemptCount = 0;
      user.FailedPasswordAnswerAttemptCount = 0;
      DateTime dateTime = new DateTime(1754, 1, 1);
      user.FailedPasswordAnswerAttemptWindowStart = dateTime;
      user.FailedPasswordAttemptWindowStart = dateTime;
      return true;
    }

    /// <summary>Creates new user with the specified username.</summary>
    /// <param name="email">Email of the user.</param>
    /// <returns>The new user.</returns>
    [MethodPermission("Backend", new string[] {"ManageUsers"})]
    public abstract User CreateUser(string email);

    /// <summary>
    /// Creates new user with the specified identity and username.
    /// </summary>
    /// <param name="id">The identity of the new user.</param>
    /// <param name="email">Email of the user.</param>
    /// <returns>The new user.</returns>
    [MethodPermission("Backend", new string[] {"ManageUsers"})]
    public abstract User CreateUser(Guid id, string email);

    /// <summary>Gets the user with the specified identity.</summary>
    /// <param name="pageId">The identity to search for.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Security.Model.User" />.</returns>
    public abstract User GetUser(Guid id);

    /// <summary>Gets a query for users.</summary>
    /// <returns>The query for users.</returns>
    public abstract IQueryable<User> GetUsers();

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The item to delete.</param>
    [MethodPermission("Backend", new string[] {"ManageUsers"})]
    public abstract void Delete(User item);

    /// <summary>Validates user parameters.</summary>
    /// <param name="userName">The user name for the new user.</param>
    /// <param name="password">The password for the new user.</param>
    /// <param name="email">The e-mail address for the new user.</param>
    /// <param name="passwordQuestion">The password question for the new user.</param>
    /// <param name="passwordAnswer">The password answer for the new user</param>
    /// <param name="providerUserKey">The unique identifier from the membership data source for the user.</param>
    /// <param name="status">A <see cref="T:System.Web.Security.MembershipCreateStatus" /> enumeration value indicating whether the user was created successfully.</param>
    /// <returns>true if all parameters are valid.</returns>
    protected virtual bool ValidateParameters(
      ref string userName,
      ref string password,
      ref string email,
      ref string passwordQuestion,
      ref string passwordAnswer,
      ref object providerUserKey,
      out MembershipCreateStatus status)
    {
      if (this.UserExists(userName))
      {
        status = MembershipCreateStatus.DuplicateUserName;
        return false;
      }
      if (this.EmailExists(email))
      {
        status = MembershipCreateStatus.DuplicateEmail;
        return false;
      }
      if (providerUserKey != null && !(providerUserKey is Guid))
      {
        status = MembershipCreateStatus.InvalidProviderUserKey;
        return false;
      }
      if (providerUserKey == null)
        providerUserKey = (object) this.GetNewGuid();
      if (passwordAnswer != null)
        passwordAnswer = passwordAnswer.Trim().ToUpperInvariant();
      if (!string.IsNullOrEmpty(passwordAnswer) && passwordAnswer.Length > 128)
      {
        status = MembershipCreateStatus.InvalidAnswer;
        return false;
      }
      if (this.RequiresQuestionAndAnswer && string.IsNullOrEmpty(passwordAnswer))
      {
        status = MembershipCreateStatus.InvalidAnswer;
        return false;
      }
      if (passwordAnswer == null)
        passwordAnswer = string.Empty;
      if (string.IsNullOrEmpty(userName))
      {
        status = MembershipCreateStatus.InvalidUserName;
        return false;
      }
      if (this.RequiresQuestionAndAnswer && string.IsNullOrEmpty(passwordQuestion))
      {
        status = MembershipCreateStatus.InvalidQuestion;
        return false;
      }
      if (passwordQuestion == null)
        passwordQuestion = string.Empty;
      if (!this.ValidatePassword(password))
      {
        status = MembershipCreateStatus.InvalidPassword;
        return false;
      }
      status = MembershipCreateStatus.Success;
      return true;
    }

    /// <summary>Validates the provided password.</summary>
    /// <param name="password">The password to validate.</param>
    /// <returns>true if valid else false.</returns>
    protected virtual bool ValidatePassword(string password, bool throwException = false)
    {
      if (password.Length < this.MinRequiredPasswordLength)
      {
        if (throwException)
          throw new ArgumentException(string.Format(Res.Get<SecurityResources>().PasswordTooShort, (object) this.MinRequiredPasswordLength));
        return false;
      }
      int num = 0;
      for (int index = 0; index < password.Length; ++index)
      {
        if (!char.IsLetterOrDigit(password, index))
          ++num;
      }
      if (num < this.MinRequiredNonAlphanumericCharacters)
      {
        if (throwException)
          throw new ArgumentException(string.Format(Res.Get<SecurityResources>().PasswordNeedsMoreAlphaNumericCharacters, (object) this.MinRequiredNonAlphanumericCharacters));
        return false;
      }
      if (this.PasswordStrengthRegularExpression.Length <= 0 || Regex.IsMatch(password, this.PasswordStrengthRegularExpression))
        return true;
      if (throwException)
        throw new ArgumentException(Res.Get<SecurityResources>().PasswordNeedsMoreAlphaNumericCharacters);
      return false;
    }

    /// <summary>Encodes the provided password.</summary>
    /// <param name="password">The password to encode.</param>
    /// <param name="salt">An optional suffix that can be added to encoded password.</param>
    /// <param name="passwordFormat">The password format.</param>
    /// <returns>The encoded password.</returns>
    protected virtual string EncodePassword(
      string password,
      string salt,
      MembershipPasswordFormat passwordFormat)
    {
      string data = password;
      if (!string.IsNullOrEmpty(salt))
        data += salt;
      switch (passwordFormat)
      {
        case MembershipPasswordFormat.Clear:
          return data;
        case MembershipPasswordFormat.Hashed:
          return SecurityManager.ComputeHash(data, this.ValidationKey);
        case MembershipPasswordFormat.Encrypted:
          return SecurityManager.EncryptData(data);
        default:
          throw new ProviderException("Unsupported password format.");
      }
    }

    /// <summary>Decodes the provided password.</summary>
    /// <param name="encodedPassword">The password to decode.</param>
    /// <param name="passwordFormat">The password format.</param>
    /// <returns>The decoded password.</returns>
    protected virtual string DecodePassword(
      string encodedPassword,
      MembershipPasswordFormat passwordFormat)
    {
      string data = encodedPassword;
      switch (passwordFormat)
      {
        case MembershipPasswordFormat.Clear:
          return data;
        case MembershipPasswordFormat.Hashed:
          throw new ProviderException("Cannot decode a hashed password.");
        case MembershipPasswordFormat.Encrypted:
          data = SecurityManager.DecryptData(data);
          goto case MembershipPasswordFormat.Clear;
        default:
          throw new ProviderException("Unsupported password format.");
      }
    }

    private bool CheckValidPassword(User user, string password)
    {
      if (user == null || !user.IsApproved || string.IsNullOrWhiteSpace(user.Password))
        return false;
      if (user.IsLockedOut)
      {
        if (!(DateTime.UtcNow > user.FailedPasswordAttemptWindowStart.AddMinutes((double) this.PasswordAttemptWindow)))
          return false;
        this.UnlockUser(user);
      }
      MembershipPasswordFormat passwordFormat = (MembershipPasswordFormat) user.PasswordFormat;
      return this.CheckValidPassword(password + user.Salt, user.Password, passwordFormat);
    }

    private bool CheckValidPassword(
      string enteredByUser,
      string original,
      MembershipPasswordFormat passwordFormat)
    {
      return passwordFormat != MembershipPasswordFormat.Encrypted ? original == this.EncodePassword(enteredByUser, (string) null, passwordFormat) : this.DecodePassword(original, passwordFormat) == enteredByUser;
    }

    protected virtual void UpdateFailureCount(User user, string failureType)
    {
      if (failureType == "password")
      {
        int passwordAttemptCount = user.FailedPasswordAttemptCount;
        DateTime dateTime1 = user.FailedPasswordAttemptWindowStart;
        if (dateTime1 == DateTime.MinValue)
          dateTime1 = DateTime.UtcNow;
        DateTime dateTime2 = dateTime1.AddMinutes((double) this.PasswordAttemptWindow);
        if (passwordAttemptCount == 0 || DateTime.UtcNow > dateTime2)
        {
          user.FailedPasswordAttemptCount = 1;
          user.FailedPasswordAttemptWindowStart = DateTime.UtcNow;
        }
        else
        {
          int num1 = passwordAttemptCount;
          int num2 = num1 + 1;
          if (num1 >= this.MaxInvalidPasswordAttempts)
          {
            user.SetIsLockedOut(true);
            user.SetLastLockoutDate(DateTime.UtcNow);
          }
          else
            user.FailedPasswordAttemptCount = num2;
        }
      }
      else
      {
        int answerAttemptCount = user.FailedPasswordAnswerAttemptCount;
        DateTime dateTime3 = user.FailedPasswordAnswerAttemptWindowStart;
        if (dateTime3 == DateTime.MinValue)
          dateTime3 = DateTime.UtcNow;
        DateTime dateTime4 = dateTime3.AddMinutes((double) this.PasswordAttemptWindow);
        if (answerAttemptCount == 0 || DateTime.UtcNow > dateTime4)
        {
          user.FailedPasswordAnswerAttemptCount = 1;
          user.FailedPasswordAnswerAttemptWindowStart = DateTime.UtcNow;
        }
        else
        {
          int num3 = answerAttemptCount;
          int num4 = num3 + 1;
          if (num3 >= this.MaxInvalidPasswordAttempts)
          {
            user.SetIsLockedOut(true);
            user.SetLastLockoutDate(DateTime.UtcNow);
          }
          else
            user.FailedPasswordAnswerAttemptCount = num4;
        }
      }
    }

    protected virtual string GenerateSalt() => this.PasswordFormat != MembershipPasswordFormat.Clear ? SecurityManager.GetRandomKey(16) : "";

    /// <summary>Creates new data item.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="pageId">The pageId.</param>
    /// <returns></returns>
    public override object CreateItem(Type itemType, Guid id)
    {
      if (itemType == typeof (User))
        return (object) this.CreateUser(id, string.Empty);
      throw DataProviderBase.GetInvalidItemTypeException(itemType, this.GetKnownTypes());
    }

    /// <summary>
    /// Gets the data item with the specified ID.
    /// An exception should be thrown if an item with the specified ID does not exist.
    /// </summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="pageId">The ID of the item to return.</param>
    /// <returns></returns>
    public override object GetItem(Type itemType, Guid id) => itemType == typeof (User) ? (object) this.GetUser(id) : base.GetItem(itemType, id);

    /// <summary>
    /// Get item by primary key without throwing exceptions if it doesn't exist
    /// </summary>
    /// <param name="itemType">Type of the item to get</param>
    /// <param name="id">Primary key</param>
    /// <returns>Item or default value</returns>
    public override object GetItemOrDefault(Type itemType, Guid id)
    {
      if (!(itemType == typeof (User)))
        return base.GetItemOrDefault(itemType, id);
      return (object) this.GetUsers().Where<User>((Expression<Func<User, bool>>) (u => u.Id == id)).FirstOrDefault<User>();
    }

    /// <summary>Gets the items.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="orderExpression">The order expression.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <param name="totalCount">Total count of the items that are filtered by <paramref name="filterExpression" /></param>
    /// <returns></returns>
    public override IEnumerable GetItems(
      Type itemType,
      string filterExpression,
      string orderExpression,
      int skip,
      int take,
      ref int? totalCount)
    {
      if (itemType == typeof (MembershipUser) || itemType == typeof (User))
        return (IEnumerable) DataProviderBase.SetExpressions<User>(this.GetUsers(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      throw DataProviderBase.GetInvalidItemTypeException(itemType, this.GetKnownTypes());
    }

    /// <summary>
    /// Marks the provided persistent item for deletion.
    /// The item is deleted form the storage when the transaction is committed.
    /// </summary>
    /// <param name="item">The item to be deleted.</param>
    public override void DeleteItem(object item)
    {
      if (item is User)
        this.Delete((User) item);
      throw DataProviderBase.GetInvalidItemTypeException(item.GetType(), this.GetKnownTypes());
    }

    /// <summary>Gets a unique key for each data provider base.</summary>
    /// <value></value>
    public override string RootKey => nameof (MembershipDataProvider);

    /// <summary>Get a list of types served by this manager</summary>
    /// <returns></returns>
    public override Type[] GetKnownTypes() => new Type[1]
    {
      typeof (User)
    };

    /// <summary>Gets the security root.</summary>
    /// <returns></returns>
    public override ISecuredObject GetSecurityRoot()
    {
      if (AppPermission.Root != null)
        return (ISecuredObject) SecurityManager.GetManager().GetSecurityRoot("ApplicationSecurityRoot");
      throw new InvalidOperationException("Missing application global permission.");
    }

    /// <summary>Gets the security root.</summary>
    /// <param name="create">if set to <c>true</c> a security root will be created for the provider if there is no root.</param>
    /// <returns></returns>
    public override ISecuredObject GetSecurityRoot(bool create)
    {
      if (AppPermission.Root != null)
        return (ISecuredObject) SecurityManager.GetManager().GetSecurityRoot("ApplicationSecurityRoot");
      throw new InvalidOperationException("Missing application global permission.");
    }

    /// <summary>Commits the provided transaction.</summary>
    [GlobalPermission(new string[] {"ManageUsers"})]
    public override void CommitTransaction() => base.CommitTransaction();

    /// <summary>
    /// Gets the permission sets relevant to this specific secured object.
    /// To be overridden by relevant providers (which involve security roots)
    /// </summary>
    /// <value>The supported permission sets.</value>
    public override string[] SupportedPermissionSets
    {
      get => this.supportedPermissionSets;
      set => this.supportedPermissionSets = value;
    }

    protected override void AddEvents(
      ICollection<IEvent> events,
      IDataItem dataItem,
      SecurityConstants.TransactionActionType actionType)
    {
      User user = dataItem as User;
      UserEventBase userEvent1 = (UserEventBase) null;
      if (user == null)
        return;
      object origin;
      this.TryGetExecutionStateData("EventOriginKey", out origin);
      if (actionType != SecurityConstants.TransactionActionType.New)
      {
        SitefinityOAContext transaction = (SitefinityOAContext) this.GetTransaction();
        if (transaction.IsFieldDirty((object) user, "LastLoginDate") | transaction.IsFieldDirty((object) user, "IsLoggedIn"))
          return;
      }
      switch (actionType)
      {
        case SecurityConstants.TransactionActionType.New:
          userEvent1 = (UserEventBase) new UserCreating()
          {
            User = user
          };
          this.PopulateUserEventBase(ref userEvent1, user);
          this.RaiseEvent((IEvent) userEvent1, origin, true);
          userEvent1 = (UserEventBase) new UserCreated();
          break;
        case SecurityConstants.TransactionActionType.Updated:
          UserEventBase userEvent2 = (UserEventBase) new UserUpdating()
          {
            ApprovalStatusChanged = user.IsAprovalStatusChanged,
            PasswordChanged = user.IsPasswordChanged,
            User = user
          };
          this.PopulateUserEventBase(ref userEvent2, user);
          this.RaiseEvent((IEvent) userEvent2, origin, true);
          userEvent1 = (UserEventBase) new UserUpdated()
          {
            ApprovalStatusChanged = user.IsAprovalStatusChanged,
            PasswordChanged = user.IsPasswordChanged
          };
          break;
        case SecurityConstants.TransactionActionType.Deleted:
          userEvent1 = (UserEventBase) new UserDeleting()
          {
            User = user
          };
          this.PopulateUserEventBase(ref userEvent1, user);
          this.RaiseEvent((IEvent) userEvent1, origin, true);
          userEvent1 = (UserEventBase) new UserDeleted();
          break;
      }
      this.PopulateUserEventBase(ref userEvent1, user);
      events.Add((IEvent) userEvent1);
    }

    private void PopulateUserEventBase(ref UserEventBase userEvent, User user)
    {
      userEvent.UserId = user.Id;
      userEvent.UserName = user.UserName;
      userEvent.Email = user.Email;
      userEvent.MembershipProviderName = user.ProviderName;
      userEvent.IsApproved = user.IsApproved;
      userEvent.Password = user.Password;
      userEvent.PasswordFormat = user.PasswordFormat;
      userEvent.ExternalProviderName = user.ExternalProviderName;
      userEvent.ExternalId = user.ExternalId;
      DataEventFactory.SetChangedProperties((IPropertyChangeDataEvent) userEvent, (IDataItem) user, (CultureInfo) null);
    }

    private void RaiseEvent(IEvent eventData, object origin, bool throwExceptions)
    {
      if (origin != null)
        eventData.Origin = (string) origin;
      EventHub.Raise(eventData, throwExceptions);
    }

    /// <summary>Initializes the provider.</summary>
    /// <param name="providerName">The friendly name of the provider.</param>
    /// <param name="config">
    /// A collection of the name/value pairs representing the provider-specific attributes specified in the configuration for this provider.
    /// </param>
    /// <param name="managerType">
    /// The type of the manger initialized this provider.
    /// </param>
    protected internal override void Initialize(
      string providerName,
      NameValueCollection config,
      Type managerType)
    {
      string s1 = config["maxInvalidPasswordAttempts"];
      this.MaxInvalidPasswordAttempts = !string.IsNullOrEmpty(s1) ? int.Parse(s1, (IFormatProvider) CultureInfo.InvariantCulture) : 5;
      config.Remove("maxInvalidPasswordAttempts");
      string s2 = config["minRequiredNonalphanumericCharacters"];
      this.MinRequiredNonAlphanumericCharacters = !string.IsNullOrEmpty(s2) ? int.Parse(s2, (IFormatProvider) CultureInfo.InvariantCulture) : 0;
      config.Remove("minRequiredNonalphanumericCharacters");
      string s3 = config["minRequiredPasswordLength"];
      this.MinRequiredPasswordLength = !string.IsNullOrEmpty(s3) ? int.Parse(s3, (IFormatProvider) CultureInfo.InvariantCulture) : 7;
      config.Remove("minRequiredPasswordLength");
      string s4 = config["newPasswordLength"];
      this.NewPasswordLength = !string.IsNullOrEmpty(s4) ? int.Parse(s4, (IFormatProvider) CultureInfo.InvariantCulture) : 8;
      config.Remove("newPasswordLength");
      string s5 = config["passwordAttemptWindow"];
      this.PasswordAttemptWindow = !string.IsNullOrEmpty(s5) ? int.Parse(s5, (IFormatProvider) CultureInfo.InvariantCulture) : 10;
      config.Remove("passwordAttemptWindow");
      string str1 = config["passwordFormat"];
      this.PasswordFormat = !string.IsNullOrEmpty(str1) ? (MembershipPasswordFormat) Enum.Parse(typeof (MembershipPasswordFormat), str1) : MembershipPasswordFormat.Hashed;
      config.Remove("passwordFormat");
      string str2 = config["passwordStrengthRegularExpression"];
      this.PasswordStrengthRegularExpression = !string.IsNullOrEmpty(str2) ? str2 : string.Empty;
      config.Remove("passwordStrengthRegularExpression");
      string str3 = config["requiresQuestionAndAnswer"];
      this.RequiresQuestionAndAnswer = !string.IsNullOrEmpty(str3) && bool.Parse(str3);
      config.Remove("requiresQuestionAndAnswer");
      string str4 = config["enablePasswordRetrieval"];
      this.EnablePasswordRetrieval = !string.IsNullOrEmpty(str4) && bool.Parse(str4);
      config.Remove("enablePasswordRetrieval");
      string str5 = config["enablePasswordReset"];
      this.EnablePasswordReset = string.IsNullOrEmpty(str5) || bool.Parse(str5);
      config.Remove("enablePasswordReset");
      string str6 = config["recoveryMailAddress"];
      this.RecoveryMailAddress = !string.IsNullOrEmpty(str6) ? str6 : string.Empty;
      string str7 = config["recoveryMailBody"];
      if (!string.IsNullOrEmpty(str7))
        this.recoveryMailBody = str7;
      string str8 = config["recoveryMailSubject"];
      this.RecoveryMailSubject = !string.IsNullOrEmpty(str8) ? str8 : string.Empty;
      string str9 = config["confirmationEmailAddress"];
      this.ConfirmationEmailAddress = !string.IsNullOrEmpty(str9) ? str9 : "noreply@noreply.com";
      string str10 = config["confirmRegistrationMailBody"];
      if (!string.IsNullOrEmpty(str10))
        this.confirmRegistrationMailBody = str10;
      string str11 = config["confirmRegistrationMailSubject"];
      if (!string.IsNullOrEmpty(str11))
        this.confirmRegistrationMailSubject = str11;
      string str12 = config["successfulRegistrationEmailAddress"];
      this.SuccessfulRegistrationEmailAddress = !string.IsNullOrEmpty(str12) ? str12 : "noreply@noreply.com";
      string str13 = config["successfulRegistrationEmailBody"];
      if (!string.IsNullOrEmpty(str13))
        this.successfulRegistrationEmailBody = str13;
      string str14 = config["successfulRegistrationMailSubject"];
      if (!string.IsNullOrEmpty(str14))
        this.successfulRegistrationMailSubject = str14;
      string str15 = config["validationKey"];
      if (!string.IsNullOrEmpty(str15))
        this.ValidationKey = str15;
      base.Initialize(providerName, config, managerType);
    }

    public bool DataEventsEnabled => true;

    public bool ApplyDataEventItemFilter(IDataItem item) => item is User;
  }
}
