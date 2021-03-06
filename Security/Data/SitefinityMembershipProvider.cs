// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Data.SitefinityMembershipProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Security;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Security.Data
{
  /// <summary>
  /// Represents data provider for ASP.NET membership services.
  /// </summary>
  public class SitefinityMembershipProvider : MembershipProvider
  {
    private MembershipDataProvider dataProvider;

    /// <summary>
    /// The name of the application using the custom membership provider.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// The name of the application using the custom membership provider.
    /// </returns>
    public override string ApplicationName
    {
      get => this.DataProvider.ApplicationName;
      set => throw new NotSupportedException();
    }

    /// <summary>
    /// Indicates whether the membership provider is configured to allow users to reset their passwords.
    /// </summary>
    /// <value></value>
    /// <returns>true if the membership provider supports password reset; otherwise, false. The default is true.
    /// </returns>
    public override bool EnablePasswordReset => this.DataProvider.EnablePasswordReset;

    /// <summary>
    /// Indicates whether the membership provider is configured to allow users to retrieve their passwords.
    /// </summary>
    /// <value></value>
    /// <returns>true if the membership provider is configured to support password retrieval; otherwise, false. The default is false.
    /// </returns>
    public override bool EnablePasswordRetrieval => this.DataProvider.EnablePasswordRetrieval;

    /// <summary>
    /// Gets the number of invalid password or password-answer attempts allowed before the membership user is locked out.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// The number of invalid password or password-answer attempts allowed before the membership user is locked out.
    /// </returns>
    public override int MaxInvalidPasswordAttempts => this.DataProvider.MaxInvalidPasswordAttempts;

    /// <summary>
    /// Gets the minimum number of special characters that must be present in a valid password.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// The minimum number of special characters that must be present in a valid password.
    /// </returns>
    public override int MinRequiredNonAlphanumericCharacters => this.DataProvider.MinRequiredNonAlphanumericCharacters;

    /// <summary>Gets the minimum length required for a password.</summary>
    /// <value></value>
    /// <returns>The minimum length required for a password.</returns>
    public override int MinRequiredPasswordLength => this.DataProvider.MinRequiredPasswordLength;

    /// <summary>
    /// Gets the number of minutes in which a maximum number of invalid password or password-answer attempts are allowed before the membership user is locked out.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// The number of minutes in which a maximum number of invalid password or password-answer attempts are allowed before the membership user is locked out.
    /// </returns>
    public override int PasswordAttemptWindow => this.DataProvider.PasswordAttemptWindow;

    /// <summary>
    /// Gets a value indicating the format for storing passwords in the membership data store.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.Security.MembershipPasswordFormat" /> values indicating the format for storing passwords in the data store.
    /// </returns>
    public override MembershipPasswordFormat PasswordFormat => this.DataProvider.PasswordFormat;

    /// <summary>
    /// Gets the regular expression used to evaluate a password.
    /// </summary>
    /// <value></value>
    /// <returns>A regular expression used to evaluate a password.</returns>
    public override string PasswordStrengthRegularExpression => this.DataProvider.PasswordStrengthRegularExpression;

    /// <summary>
    /// Gets a value indicating whether the membership provider is configured to require the user to answer a password question for password reset and retrieval.
    /// </summary>
    /// <value></value>
    /// <returns>true if a password answer is required for password reset and retrieval; otherwise, false. The default is true.
    /// </returns>
    public override bool RequiresQuestionAndAnswer => this.DataProvider.RequiresQuestionAndAnswer;

    /// <summary>
    /// Gets a value indicating whether the membership provider is configured to require a unique e-mail address for each user name.
    /// </summary>
    /// <value></value>
    /// <returns>true if the membership provider requires a unique e-mail address; otherwise, false. The default is true.
    /// </returns>
    public override bool RequiresUniqueEmail => this.DataProvider.RequiresUniqueEmail;

    internal MembershipDataProvider DataProvider
    {
      get
      {
        if (this.dataProvider == null)
          this.dataProvider = UserManager.GetManager().Provider;
        return this.dataProvider;
      }
      set => this.dataProvider = value;
    }

    /// <summary>
    /// Processes a request to update the password for a membership user.
    /// </summary>
    /// <param name="username">The user to update the password for.</param>
    /// <param name="oldPassword">The current password for the specified user.</param>
    /// <param name="newPassword">The new password for the specified user.</param>
    /// <returns>
    /// true if the password was updated successfully; otherwise, false.
    /// </returns>
    public override bool ChangePassword(string username, string oldPassword, string newPassword)
    {
      this.DataProvider.SuppressSecurityChecks = true;
      int num = this.DataProvider.ChangePassword(username, oldPassword, newPassword) ? 1 : 0;
      if (num == 0)
        return num != 0;
      this.DataProvider.CommitTransaction();
      return num != 0;
    }

    /// <summary>
    /// Processes a request to update the password question and answer for a membership user.
    /// </summary>
    /// <param name="username">The user to change the password question and answer for.</param>
    /// <param name="password">The password for the specified user.</param>
    /// <param name="newPasswordQuestion">The new password question for the specified user.</param>
    /// <param name="newPasswordAnswer">The new password answer for the specified user.</param>
    /// <returns>
    /// true if the password question and answer are updated successfully; otherwise, false.
    /// </returns>
    public override bool ChangePasswordQuestionAndAnswer(
      string username,
      string password,
      string newPasswordQuestion,
      string newPasswordAnswer)
    {
      this.DataProvider.SuppressSecurityChecks = true;
      int num = this.DataProvider.ChangePasswordQuestionAndAnswer(username, password, newPasswordQuestion, newPasswordAnswer) ? 1 : 0;
      if (num == 0)
        return num != 0;
      this.DataProvider.CommitTransaction();
      return num != 0;
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
    /// A <see cref="T:System.Web.Security.MembershipUser" /> object populated with the information for the newly created user.
    /// </returns>
    public override MembershipUser CreateUser(
      string username,
      string password,
      string email,
      string passwordQuestion,
      string passwordAnswer,
      bool isApproved,
      object providerUserKey,
      out MembershipCreateStatus status)
    {
      if (string.CompareOrdinal(username, email) != 0)
        throw new ArgumentException("Username and email are not equal");
      this.DataProvider.SuppressSecurityChecks = true;
      User user = this.DataProvider.CreateUser(email, password, passwordQuestion, passwordAnswer, isApproved, providerUserKey, out status);
      if (status != MembershipCreateStatus.Success)
        return (MembershipUser) user;
      this.DataProvider.CommitTransaction();
      return (MembershipUser) user;
    }

    /// <summary>Removes a user from the membership data source.</summary>
    /// <param name="username">The name of the user to delete.</param>
    /// <param name="deleteAllRelatedData">true to delete data related to the user from the database; false to leave data related to the user in the database.</param>
    /// <returns>
    /// true if the user was successfully deleted; otherwise, false.
    /// </returns>
    public override bool DeleteUser(string username, bool deleteAllRelatedData)
    {
      this.DataProvider.SuppressSecurityChecks = true;
      User user = this.DataProvider.GetUser(username);
      if (user.Id == SecurityManager.GetCurrentUserId())
        throw new Exception(Res.Get<SecurityResources>().YouCantDeleteTheCurrentUser);
      this.DataProvider.Delete(user);
      return true;
    }

    /// <summary>
    /// Gets a collection of membership users where the e-mail address contains the specified e-mail address to match.
    /// </summary>
    /// <param name="emailToMatch">The e-mail address to search for.</param>
    /// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex" /> is zero-based.</param>
    /// <param name="pageSize">The size of the page of results to return.</param>
    /// <param name="totalRecords">The total number of matched users.</param>
    /// <returns>
    /// A <see cref="T:System.Web.Security.MembershipUserCollection" /> collection that contains a page of <paramref name="pageSize" /><see cref="T:System.Web.Security.MembershipUser" /> objects beginning at the page specified by <paramref name="pageIndex" />.
    /// </returns>
    public override MembershipUserCollection FindUsersByEmail(
      string emailToMatch,
      int pageIndex,
      int pageSize,
      out int totalRecords)
    {
      this.DataProvider.SuppressSecurityChecks = true;
      IQueryable<User> source = this.DataProvider.GetUsers().Where<User>((Expression<Func<User, bool>>) (u => u.Email.Contains(emailToMatch)));
      totalRecords = source.Count<User>();
      MembershipUserCollection usersByEmail = new MembershipUserCollection();
      if (pageIndex != 0 && pageSize != 0)
      {
        source.Skip<User>(pageIndex * pageSize);
        source.Take<User>(pageSize);
      }
      foreach (User user in (IEnumerable<User>) source)
        usersByEmail.Add((MembershipUser) user);
      return usersByEmail;
    }

    /// <summary>
    /// Gets a collection of membership users where the user name contains the specified user name to match.
    /// </summary>
    /// <param name="usernameToMatch">The user name to search for.</param>
    /// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex" /> is zero-based.</param>
    /// <param name="pageSize">The size of the page of results to return.</param>
    /// <param name="totalRecords">The total number of matched users.</param>
    /// <returns>
    /// A <see cref="T:System.Web.Security.MembershipUserCollection" /> collection that contains a page of <paramref name="pageSize" /><see cref="T:System.Web.Security.MembershipUser" /> objects beginning at the page specified by <paramref name="pageIndex" />.
    /// </returns>
    public override MembershipUserCollection FindUsersByName(
      string usernameToMatch,
      int pageIndex,
      int pageSize,
      out int totalRecords)
    {
      this.DataProvider.SuppressSecurityChecks = true;
      IQueryable<User> source = this.DataProvider.GetUsers().Where<User>((Expression<Func<User, bool>>) (u => u.UserName.Contains(usernameToMatch)));
      totalRecords = source.Count<User>();
      MembershipUserCollection usersByName = new MembershipUserCollection();
      if (pageIndex != 0 && pageSize != 0)
      {
        source.Skip<User>(pageIndex * pageSize);
        source.Take<User>(pageSize);
      }
      foreach (User user in (IEnumerable<User>) source)
        usersByName.Add((MembershipUser) user);
      return usersByName;
    }

    /// <summary>
    /// Gets a collection of all the users in the data source in pages of data.
    /// </summary>
    /// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex" /> is zero-based.</param>
    /// <param name="pageSize">The size of the page of results to return.</param>
    /// <param name="totalRecords">The total number of matched users.</param>
    /// <returns>
    /// A <see cref="T:System.Web.Security.MembershipUserCollection" /> collection that contains a page of <paramref name="pageSize" /><see cref="T:System.Web.Security.MembershipUser" /> objects beginning at the page specified by <paramref name="pageIndex" />.
    /// </returns>
    public override MembershipUserCollection GetAllUsers(
      int pageIndex,
      int pageSize,
      out int totalRecords)
    {
      this.DataProvider.SuppressSecurityChecks = true;
      IQueryable<User> users = this.DataProvider.GetUsers();
      totalRecords = users.Count<User>();
      MembershipUserCollection allUsers = new MembershipUserCollection();
      if (pageIndex != 0 && pageSize != 0)
      {
        users.Skip<User>(pageIndex * pageSize);
        users.Take<User>(pageSize);
      }
      foreach (User user in (IEnumerable<User>) users)
        allUsers.Add((MembershipUser) user);
      return allUsers;
    }

    /// <summary>
    /// Gets the number of users currently accessing the application.
    /// </summary>
    /// <returns>
    /// The number of users currently accessing the application.
    /// </returns>
    public override int GetNumberOfUsersOnline()
    {
      this.DataProvider.SuppressSecurityChecks = true;
      return this.DataProvider.GetNumberOfUsersOnline();
    }

    /// <summary>
    /// Gets the password for the specified user name from the data source.
    /// </summary>
    /// <param name="username">The user to retrieve the password for.</param>
    /// <param name="answer">The password answer for the user.</param>
    /// <returns>The password for the specified user name.</returns>
    public override string GetPassword(string username, string answer)
    {
      this.DataProvider.SuppressSecurityChecks = true;
      return this.DataProvider.GetPassword(username, answer);
    }

    /// <summary>
    /// Gets information from the data source for a user. Provides an option to update the last-activity date/time stamp for the user.
    /// </summary>
    /// <param name="username">The name of the user to get information for.</param>
    /// <param name="userIsOnline">true to update the last-activity date/time stamp for the user; false to return user information without updating the last-activity date/time stamp for the user.</param>
    /// <returns>
    /// A <see cref="T:System.Web.Security.MembershipUser" /> object populated with the specified user's information from the data source.
    /// </returns>
    public override MembershipUser GetUser(string username, bool userIsOnline)
    {
      this.DataProvider.SuppressSecurityChecks = true;
      User user = this.DataProvider.GetUser(username);
      if (user != null & userIsOnline)
      {
        user.LastActivityDate = DateTime.UtcNow.ToUniversalTime();
        this.DataProvider.CommitTransaction();
      }
      return (MembershipUser) user;
    }

    /// <summary>
    /// Gets user information from the data source based on the unique identifier for the membership user. Provides an option to update the last-activity date/time stamp for the user.
    /// </summary>
    /// <param name="providerUserKey">The unique identifier for the membership user to get information for.</param>
    /// <param name="userIsOnline">true to update the last-activity date/time stamp for the user; false to return user information without updating the last-activity date/time stamp for the user.</param>
    /// <returns>
    /// A <see cref="T:System.Web.Security.MembershipUser" /> object populated with the specified user's information from the data source.
    /// </returns>
    public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
    {
      this.DataProvider.SuppressSecurityChecks = true;
      User user = this.DataProvider.GetUser((Guid) providerUserKey);
      if (user != null & userIsOnline)
      {
        user.LastActivityDate = DateTime.UtcNow.ToUniversalTime();
        this.DataProvider.CommitTransaction();
      }
      return (MembershipUser) user;
    }

    /// <summary>
    /// Gets the user name associated with the specified e-mail address.
    /// </summary>
    /// <param name="email">The e-mail address to search for.</param>
    /// <returns>
    /// The user name associated with the specified e-mail address. If no match is found, return null.
    /// </returns>
    public override string GetUserNameByEmail(string email)
    {
      this.DataProvider.SuppressSecurityChecks = true;
      User userByEmail = this.DataProvider.GetUserByEmail(email);
      return userByEmail != null ? userByEmail.UserName : string.Empty;
    }

    /// <summary>
    /// Resets a user's password to a new, automatically generated password.
    /// </summary>
    /// <param name="username">The user to reset the password for.</param>
    /// <param name="answer">The password answer for the specified user.</param>
    /// <returns>The new password for the specified user.</returns>
    public override string ResetPassword(string username, string answer)
    {
      this.DataProvider.SuppressSecurityChecks = true;
      return this.DataProvider.ResetPassword(username, answer);
    }

    /// <summary>
    /// Clears a lock so that the membership user can be validated.
    /// </summary>
    /// <param name="userName">The membership user whose lock status you want to clear.</param>
    /// <returns>
    /// true if the membership user was successfully unlocked; otherwise, false.
    /// </returns>
    public override bool UnlockUser(string userName)
    {
      this.DataProvider.SuppressSecurityChecks = true;
      return this.DataProvider.UnlockUser(userName);
    }

    /// <summary>Updates information about a user in the data source.</summary>
    /// <param name="user">A <see cref="T:System.Web.Security.MembershipUser" /> object that represents the user to update and the updated information for the user.</param>
    public override void UpdateUser(MembershipUser user)
    {
      this.DataProvider.SuppressSecurityChecks = true;
      if (!(user is User))
        throw new ArgumentException("The provided user object was not created with this provider.");
      this.DataProvider.CommitTransaction();
    }

    /// <summary>
    /// Verifies that the specified user name and password exist in the data source.
    /// </summary>
    /// <param name="username">The name of the user to validate.</param>
    /// <param name="password">The password for the specified user.</param>
    /// <returns>
    /// true if the specified username and password are valid; otherwise, false.
    /// </returns>
    public override bool ValidateUser(string username, string password)
    {
      this.DataProvider.SuppressSecurityChecks = true;
      return this.DataProvider.ValidateUser(username, password);
    }
  }
}
