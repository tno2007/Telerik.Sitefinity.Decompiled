// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Ldap.LdapMembershipProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Specialized;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Security;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Security.Data;
using Telerik.Sitefinity.Security.Ldap.Helpers;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Security.Ldap
{
  /// <summary>
  ///  Represents LDAP directory implementation of data provider for Sitefinity membership services.
  /// </summary>
  public class LdapMembershipProvider : MembershipDataProvider, ILdapProviderMarker
  {
    protected string ldapConnectionName;
    private ManagerInfo managerInfo;
    protected ILdapFacade ldapFacade;
    private LdapBuilder ldapBuilder;

    /// <summary>
    ///  the Ldap facade to be used, if external is set the default is not used
    /// </summary>
    public virtual ILdapFacade LdapFacade
    {
      get => this.ldapFacade != null ? this.ldapFacade : (ILdapFacade) new Telerik.Sitefinity.Security.Ldap.LdapFacade(this.ldapConnectionName);
      set => this.ldapFacade = value;
    }

    protected virtual LdapBuilder LdapBuilder
    {
      get
      {
        if (this.ldapBuilder == null)
          this.ldapBuilder = LdapBuilder.GetBuilder();
        return this.ldapBuilder;
      }
    }

    /// <summary>Changes the password.</summary>
    /// <param name="userId">The user identity.</param>
    /// <param name="oldPassword">The old password.</param>
    /// <param name="newPassword">The new password.</param>
    /// <returns></returns>
    public override bool ChangePassword(Guid userId, string oldPassword, string newPassword) => throw new NotSupportedException();

    /// <summary>Changes the password.</summary>
    /// <param name="userName">The username of the user.</param>
    /// <param name="oldPassword">The old password.</param>
    /// <param name="newPassword">The new password.</param>
    /// <returns></returns>
    public override bool ChangePassword(string userName, string oldPassword, string newPassword) => throw new NotSupportedException();

    /// <summary>Changes the password.</summary>
    /// <param name="user">The user object.</param>
    /// <param name="oldPassword">The old password.</param>
    /// <param name="newPassword">The new password.</param>
    /// <returns></returns>
    public override bool ChangePassword(User user, string oldPassword, string newPassword) => throw new NotSupportedException();

    /// <summary>
    ///  Verifies that the specified user and password exist in the data source.
    /// </summary>
    /// <param name="userName">The username of the user to validate.</param>
    /// <param name="password">The password for the specified user.</param>
    /// <returns>true if the specified username and password are valid; otherwise, false.</returns>
    public override bool ValidateUser(string userName, string password)
    {
      Guid userId;
      if (!this.LdapFacade.AuthenticateUser(userName, password, out userId))
        return false;
      LdapCredentialsCache.AddCredential(userId, userName, password);
      return true;
    }

    /// <summary>
    /// Verifies that the specified user and password exist in the data source.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <param name="password">The password for the specified user.</param>
    /// <returns>
    /// true if the specified username and password are valid; otherwise, false.
    /// </returns>
    public override bool ValidateUser(User user, string password) => this.ValidateUser(user.UserName, password);

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
    public override bool ChangePasswordQuestionAndAnswer(
      User user,
      string password,
      string newPasswordQuestion,
      string newPasswordAnswer)
    {
      throw new NotSupportedException();
    }

    /// <summary>
    /// Processes a request to update the password question and answer for a membership user.
    /// </summary>
    /// <param name="pageId">The pageId.</param>
    /// <param name="password">The password for the specified user.</param>
    /// <param name="newPasswordQuestion">The new password question for the specified user.</param>
    /// <param name="newPasswodAnswer">The new password answer for the specified user.</param>
    /// <returns>
    /// true if the password question and answer are updated successfully; otherwise, false.
    /// </returns>
    public override bool ChangePasswordQuestionAndAnswer(
      Guid id,
      string password,
      string newPasswordQuestion,
      string newPasswordAnswer)
    {
      throw new NotSupportedException();
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
    public override bool ChangePasswordQuestionAndAnswer(
      string userName,
      string password,
      string newPasswordQuestion,
      string newPasswordAnswer)
    {
      throw new NotSupportedException();
    }

    /// <summary>Adds a new membership user to the data source.</summary>
    /// <param name="email">The email for the new user.</param>
    /// <param name="password">The password for the new user.</param>
    /// <param name="passwordQuestion">The password question for the new user.</param>
    /// <param name="passwordAnswer">The password answer for the new user</param>
    /// <param name="isApproved">Whether or not the new user is approved to be validated.</param>
    /// <param name="providerUserKey">The unique identifier from the membership data source for the user.</param>
    /// <param name="status">A <see cref="T:System.Web.Security.MembershipCreateStatus" /> enumeration value indicating whether the user was created successfully.</param>
    /// <returns>
    /// A <see cref="T:Telerik.Sitefinity.Security.Model.User" /> object populated with the information for the newly created user.
    /// </returns>
    public override User CreateUser(
      string email,
      string password,
      string passwordQuestion,
      string passwordAnswer,
      bool isApproved,
      object providerUserKey,
      out MembershipCreateStatus status)
    {
      throw new NotSupportedException();
    }

    /// <summary>
    /// Gets the password for the specified user name from the data source.
    /// </summary>
    /// <param name="user">
    /// An instance of <see cref="T:Telerik.Sitefinity.Security.Model.User" /> object for which the password should be retrieved.
    /// </param>
    /// <param name="answer">The password answer for the user.</param>
    /// <returns>The password for the specified user name.</returns>
    public override string GetPassword(User user, string answer) => throw new NotSupportedException();

    /// <summary>
    /// Gets the password for the specified user name from the data source.
    /// </summary>
    /// <param name="userName">The user to retrieve the password for.</param>
    /// <param name="answer">The password answer for the user.</param>
    /// <returns>The password for the specified user name.</returns>
    public override string GetPassword(string userName, string answer) => throw new NotSupportedException();

    /// <summary>
    /// Gets the password for the specified user from the data source.
    /// </summary>
    /// <param name="userId">The pageId of the user for which the password should be retrieved.</param>
    /// <param name="answer">The password answer for the user.</param>
    /// <returns>The password for the specified user name.</returns>
    public override string GetPassword(Guid userId, string answer) => throw new NotSupportedException();

    /// <summary>Check if there is a user with this email</summary>
    /// <param name="email">Email to check</param>
    /// <returns>true if there is a user with specified email.</returns>
    public override bool EmailExists(string email) => this.LdapFacade.GetUserByEmail(email, (string[]) null) != null;

    /// <summary>Check if there is a user with this username</summary>
    /// <param name="userName">Username to check</param>
    /// <returns>true if there is a user with specified username</returns>
    public override bool UserExists(string userName) => this.LdapFacade.GetUserByUsername(userName, (string[]) null) != null;

    /// <summary>Get user by specific email</summary>
    /// <param name="email">User's email</param>
    /// <returns> returns user if there is a user with specified email or null if there isn't user with specified email</returns>
    public override User GetUserByEmail(string email)
    {
      SearchResultEntry userByEmail = this.LdapFacade.GetUserByEmail(email);
      return userByEmail == null ? (User) null : this.LdapBuilder.Build<User>(userByEmail, this.ManagerInfo, this.ApplicationName);
    }

    /// <summary>NotSupported</summary>
    /// <param name="user">The user.</param>
    /// <param name="answer">The password answer for the specified user.</param>
    /// <returns>throws NotSupportedException exception</returns>
    public override string ResetPassword(User user, string answer) => throw new NotSupportedException();

    /// <summary>
    /// Resets a user's password to a new, automatically generated password.
    /// </summary>
    /// <param name="userName">The user to reset the password for.</param>
    /// <param name="answer">The password answer for the specified user.</param>
    /// <returns>The new password for the specified user.</returns>
    public override string ResetPassword(string userName, string answer) => throw new NotSupportedException();

    /// <summary>
    /// Resets a user's password to a new, automatically generated password.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <param name="answer">The password answer for the specified user.</param>
    /// <returns>The new password for the specified user.</returns>
    public override string ResetPassword(Guid userId, string answer) => throw new NotSupportedException();

    /// <summary>Creates new user with the specified username.</summary>
    /// <param name="userName"></param>
    /// <returns>The new user.</returns>
    public override User CreateUser(string userName) => throw new NotSupportedException();

    /// <summary>
    /// Creates new user with the specified identity and username.
    /// </summary>
    /// <param name="pageId">The identity of the new user.</param>
    /// <param name="userName"></param>
    /// <returns>The new user.</returns>
    public override User CreateUser(Guid id, string userName) => throw new NotSupportedException();

    /// <summary>Gets the user with the specified identity.</summary>
    /// <param name="pageId">The identity to search for.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Security.Model.Role" />.</returns>
    public override User GetUser(Guid id)
    {
      SearchResultEntry userById = this.LdapFacade.GetUserById(id);
      return userById == null ? (User) null : this.LdapBuilder.Build<User>(userById, this.ManagerInfo, this.ApplicationName);
    }

    /// <summary>Gets a query for users.</summary>
    /// <returns>The query for users.</returns>
    public override IQueryable<User> GetUsers() => SitefinityQuery.Get<User>((DataProviderBase) this).Where<User>((Expression<Func<User, bool>>) (users => users.ApplicationName == this.ApplicationName));

    /// <summary>Gets the user with the specified username.</summary>
    /// <param name="userName">username</param>
    /// <returns>user with specified username or null</returns>
    public override User GetUser(string userName)
    {
      SearchResultEntry userByUsername = this.LdapFacade.GetUserByUsername(userName);
      return userByUsername != null ? this.LdapBuilder.Build<User>(userByUsername, this.ManagerInfo, this.ApplicationName) : (User) null;
    }

    /// <summary>Execute query against Ldap</summary>
    /// <param name="itemType">return item type</param>
    /// <param name="filterExpression">filter expression</param>
    /// <param name="orderExpression">order expression</param>
    /// <param name="skip">skip count</param>
    /// <param name="take">take coun</param>
    /// <param name="totalCount">total items count </param>
    /// <returns>returns collection of items </returns>
    public override IEnumerable GetItems(
      Type itemType,
      string filterExpression,
      string orderExpression,
      int skip,
      int take,
      ref int? totalCount)
    {
      return base.GetItems(itemType, filterExpression, orderExpression, skip, take, ref totalCount);
    }

    /// <summary>Not Supported</summary>
    /// <param name="item">User</param>
    public override void Delete(User item) => throw new NotSupportedException();

    /// <summary>Initialize Provider</summary>
    /// <param name="providerName">Provider Name</param>
    /// <param name="config">Provider configuration</param>
    /// <param name="managerType">Provider manager type</param>
    protected internal override void Initialize(
      string providerName,
      NameValueCollection config,
      Type managerType)
    {
      base.Initialize(providerName, config, managerType);
      if (config["connection"] == null)
        return;
      this.ldapConnectionName = config["connection"];
      config.Remove("connection");
    }

    /// <summary>
    /// Override it just because this provider doesn't have a decorator. The LDAP provider doesn't commit anything anyway.
    /// </summary>
    /// <param name="transactionName">The name of the transaction.</param>
    /// <returns>An object used for the transaction.</returns>
    protected internal override object CreateNewTransaction(string transactionName) => new object();

    /// <summary>
    /// Override it just because this provider doesn't have a decorator.
    /// </summary>
    public override void CommitTransaction()
    {
    }

    /// <summary>
    /// Gets or sets the name of the configuration LDAP connection to be used for connecting the LDAP directory.
    /// </summary>
    /// <value>The name of the LDAP connection.</value>
    public string LdapConnectionName
    {
      get => this.ldapConnectionName;
      set => this.ldapConnectionName = value;
    }

    /// <summary>Gets the manager info.</summary>
    /// <value>The manager info.</value>
    public ManagerInfo ManagerInfo
    {
      get
      {
        if (this.managerInfo == null)
          this.managerInfo = new ManagerInfo()
          {
            ApplicationName = this.ApplicationName,
            ManagerType = typeof (UserManager).FullName,
            ProviderName = this.Name,
            Id = Guid.NewGuid()
          };
        return this.managerInfo;
      }
    }

    /// <summary>
    /// Gets the provider abilities for the current principal. E.g. which operations are supported and allowed
    /// </summary>
    /// <value>The provider abilities.</value>
    public override ProviderAbilities Abilities
    {
      get
      {
        ProviderAbilities abilities = new ProviderAbilities();
        abilities.ProviderName = this.Name;
        abilities.ProviderType = this.GetType().FullName;
        abilities.AddAbility("GetUser", true, true);
        abilities.AddAbility("AddUser", false, false);
        abilities.AddAbility("DeleteUser", false, false);
        abilities.AddAbility("UpdateUser", false, false);
        abilities.AddAbility("ValidateUser", true, true);
        return abilities;
      }
    }

    /// <summary>
    /// Clears a lock so that the membership user can be validated.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <returns>
    /// true if the membership user was successfully unlocked; otherwise, false.
    /// </returns>
    public override bool UnlockUser(Guid userId) => throw new NotSupportedException();

    /// <summary>
    /// Clears a lock so that the membership user can be validated.
    /// </summary>
    /// <param name="userName">The membership user whose lock status you want to clear.</param>
    /// <returns>
    /// true if the membership user was successfully unlocked; otherwise, false.
    /// </returns>
    public override bool UnlockUser(string userName) => throw new NotSupportedException();

    /// <summary>
    /// Clears a lock so that the membership user can be validated.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <returns>
    /// true if the membership user was successfully unlocked; otherwise, false.
    /// </returns>
    public override bool UnlockUser(User user) => throw new NotSupportedException();
  }
}
