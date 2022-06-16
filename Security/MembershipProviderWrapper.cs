// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.MembershipProviderWrapper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Security;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Decorators;
using Telerik.Sitefinity.Data.Linq.Basic;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Security.Data;
using Telerik.Sitefinity.Security.Events;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Security
{
  public class MembershipProviderWrapper : MembershipDataProvider
  {
    private ManagerInfo managerInfo;
    private readonly MembershipProvider membershipProvider;
    private readonly Dictionary<Guid, UserWrapper> userList;

    public MembershipProviderWrapper(MembershipProvider membershipProvider)
    {
      this.membershipProvider = membershipProvider != null ? membershipProvider : throw new ArgumentNullException(nameof (membershipProvider));
      this.userList = new Dictionary<Guid, UserWrapper>();
      this.Initialize(this.Name, new NameValueCollection(), typeof (UserManager), true);
    }

    /// <summary>
    /// Gets the friendly name used to refer to the provider during configuration.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// The friendly name used to refer to the provider during configuration.
    /// </returns>
    public override string Name => this.membershipProvider.Name;

    /// <summary>
    /// Gets the friendly name used to refer to the provider during configuration.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// The friendly name used to refer to the provider during configuration.
    /// </returns>
    public override string Title => this.membershipProvider.Name;

    /// <summary>
    /// Gets a brief, friendly description suitable for display in administrative tools or other user interfaces (UIs).
    /// </summary>
    /// <value></value>
    /// <returns>
    /// A brief, friendly description suitable for display in administrative tools or other UIs.
    /// </returns>
    public override string Description => this.membershipProvider.Description;

    /// <summary>
    /// The name of the application using the custom membership provider.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// The name of the application using the custom membership provider.
    /// </returns>
    public override string ApplicationName => this.membershipProvider.ApplicationName;

    /// <summary>
    /// Indicates whether the membership provider is configured to allow users to reset their passwords.
    /// </summary>
    /// <value></value>
    /// <returns>true if the membership provider supports password reset; otherwise, false. The default is true.
    /// </returns>
    public override bool EnablePasswordReset => this.membershipProvider.EnablePasswordReset;

    /// <summary>
    /// Indicates whether the membership provider is configured to allow users to retrieve their passwords.
    /// </summary>
    /// <value></value>
    /// <returns>true if the membership provider is configured to support password retrieval; otherwise, false. The default is false.
    /// </returns>
    public override bool EnablePasswordRetrieval => this.membershipProvider.EnablePasswordRetrieval;

    /// <summary>
    /// Gets the number of invalid password or password-answer attempts allowed before the membership user is locked out.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// The number of invalid password or password-answer attempts allowed before the membership user is locked out.
    /// </returns>
    public override int MaxInvalidPasswordAttempts => this.membershipProvider.MaxInvalidPasswordAttempts;

    /// <summary>
    /// Gets the minimum number of special characters that must be present in a valid password.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// The minimum number of special characters that must be present in a valid password.
    /// </returns>
    public override int MinRequiredNonAlphanumericCharacters => this.membershipProvider.MinRequiredNonAlphanumericCharacters;

    /// <summary>Gets the minimum length required for a password.</summary>
    /// <value></value>
    /// <returns>The minimum length required for a password.</returns>
    public override int MinRequiredPasswordLength => this.membershipProvider.MinRequiredPasswordLength;

    /// <summary>
    /// Gets the number of minutes in which a maximum number of invalid password or password-answer attempts are allowed before the membership user is locked out.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// The number of minutes in which a maximum number of invalid password or password-answer attempts are allowed before the membership user is locked out.
    /// </returns>
    public override int PasswordAttemptWindow => this.membershipProvider.PasswordAttemptWindow;

    /// <summary>
    /// Gets a value indicating the format for storing passwords in the membership data store.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.Security.MembershipPasswordFormat" /> values indicating the format for storing passwords in the data store.
    /// </returns>
    public override MembershipPasswordFormat PasswordFormat => this.membershipProvider.PasswordFormat;

    /// <summary>
    /// Gets the regular expression used to evaluate a password.
    /// </summary>
    /// <value></value>
    /// <returns>A regular expression used to evaluate a password.</returns>
    public override string PasswordStrengthRegularExpression => this.membershipProvider.PasswordStrengthRegularExpression;

    /// <summary>
    /// Gets a value indicating whether the membership provider is configured to require the user to answer a password question for password reset and retrieval.
    /// </summary>
    /// <value></value>
    /// <returns>true if a password answer is required for password reset and retrieval; otherwise, false. The default is true.
    /// </returns>
    public override bool RequiresQuestionAndAnswer => this.membershipProvider.RequiresQuestionAndAnswer;

    /// <summary>
    /// Gets a value indicating whether the membership provider is configured to require a unique e-mail address for each user name.
    /// </summary>
    /// <value></value>
    /// <returns>true if the membership provider requires a unique e-mail address; otherwise, false. The default is true.
    /// </returns>
    public override bool RequiresUniqueEmail => this.membershipProvider.RequiresUniqueEmail;

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

    public override ManagerInfo GetManagerInfo(string managerType, string providerName) => new ManagerInfo()
    {
      ApplicationName = this.ApplicationName,
      ManagerType = managerType,
      ProviderName = providerName,
      Id = Guid.NewGuid()
    };

    /// <summary>Changes the password.</summary>
    /// <param name="userId">The user identity.</param>
    /// <param name="oldPassword">The old password.</param>
    /// <param name="newPassword">The new password.</param>
    /// <returns></returns>
    public override bool ChangePassword(Guid userId, string oldPassword, string newPassword) => this.ChangePassword(this.GetUser(userId), oldPassword, newPassword);

    /// <summary>Changes the password.</summary>
    /// <param name="userName">The username of the user.</param>
    /// <param name="oldPassword">The old password.</param>
    /// <param name="newPassword">The new password.</param>
    /// <returns></returns>
    public override bool ChangePassword(string userName, string oldPassword, string newPassword)
    {
      if (newPassword == oldPassword)
        throw new ProviderException(Res.Get<ErrorMessages>().NewAndOldPasswordsAreEqual);
      return this.membershipProvider.ChangePassword(userName, oldPassword, newPassword);
    }

    /// <summary>Changes the password.</summary>
    /// <param name="user">The user object.</param>
    /// <param name="oldPassword">The old password.</param>
    /// <param name="newPassword">The new password.</param>
    /// <returns></returns>
    public override bool ChangePassword(User user, string oldPassword, string newPassword)
    {
      if (user == null)
        throw new ProviderException(Res.Get<ErrorMessages>().MembershipUserNotFound);
      return this.ChangePassword(user.UserName, oldPassword, newPassword);
    }

    /// <summary>Creates new user with the specified username.</summary>
    /// <param name="email">Email of the user</param>
    /// <returns>The new user.</returns>
    public override User CreateUser(string email) => this.CreateUser(Guid.NewGuid(), email);

    /// <summary>
    /// Creates new user with the specified identity and username.
    /// </summary>
    /// <param name="pageId">The identity of the new user.</param>
    /// <param name="email">The email of the new user.</param>
    /// <returns>The new user.</returns>
    public override User CreateUser(Guid id, string email)
    {
      this.GetTransaction();
      if (id == Guid.Empty)
        throw new ArgumentNullException(nameof (id));
      if (!string.IsNullOrEmpty(email))
      {
        LoginUtils.CheckParameter(email, true, true, true, 256, nameof (email));
        LoginUtils.CheckValidEmail(email);
        if (this.UserExists(email))
          throw new ProviderException("Username already exists.");
      }
      UserWrapper user = new UserWrapper(id, email);
      user.Email = email;
      ManagerInfo managerInfo = this.GetManagerInfo(typeof (UserManager).FullName, this.Name);
      user.ManagerInfo = managerInfo;
      this.userList[id] = user;
      return (User) user;
    }

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The item to delete.</param>
    public override void Delete(User item)
    {
      this.GetTransaction();
      if (item.Id == SecurityManager.GetCurrentUserId())
        throw new Exception(Res.Get<SecurityResources>().YouCantDeleteTheCurrentUser);
      if (this.userList.ContainsKey(item.Id))
      {
        this.userList[item.Id].IsDeleted = true;
      }
      else
      {
        Guid userId = item.Id;
        User user = this.GetUsers().Where<User>((Expression<Func<User, bool>>) (u => u.Id == userId)).SingleOrDefault<User>();
        if (user == null)
          return;
        this.userList[userId] = UserWrapper.CopyFrom(user);
        this.userList[userId].IsDeleted = true;
      }
    }

    /// <summary>Check if there is a user with this email</summary>
    /// <param name="email">Email to check</param>
    /// <returns>true if there is a user with specified email.</returns>
    public override bool EmailExists(string email) => this.membershipProvider.GetUserNameByEmail(email) != null;

    /// <summary>
    /// Gets the password for the specified user name from the data source.
    /// </summary>
    /// <param name="user">
    /// An instance of <see cref="T:Telerik.Sitefinity.Security.Model.User" /> object for which the password should be retrieved.
    /// </param>
    /// <param name="answer">The password answer for the user.</param>
    /// <returns>The password for the specified user name.</returns>
    public override string GetPassword(User user, string answer)
    {
      if (user == null)
        throw new ProviderException(Res.Get<ErrorMessages>().MembershipUserNotFound);
      return this.GetPassword(user.UserName, answer);
    }

    /// <summary>
    /// Gets the password for the specified user name from the data source.
    /// </summary>
    /// <param name="userName">The user to retrieve the password for.</param>
    /// <param name="answer">The password answer for the user.</param>
    /// <returns>The password for the specified user name.</returns>
    public override string GetPassword(string userName, string answer) => this.membershipProvider.GetPassword(userName, answer);

    /// <summary>
    /// Gets the password for the specified user from the data source.
    /// </summary>
    /// <param name="userId">The pageId of the user for which the password should be retrieved.</param>
    /// <param name="answer">The password answer for the user.</param>
    /// <returns>The password for the specified user name.</returns>
    public override string GetPassword(Guid userId, string answer) => this.GetPassword(this.GetUser(userId), answer);

    /// <summary>Gets the user with the specified user name.</summary>
    /// <param name="pageId">The identity to search for.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Security.Model.User" />.</returns>
    public override User GetUser(string userName)
    {
      this.GetTransaction();
      MembershipUser user1 = this.membershipProvider.GetUser(userName, false);
      if (user1 == null)
        return (User) null;
      UserWrapper user2 = UserWrapper.CopyFrom(user1, this.ApplicationName);
      this.userList[user2.Id] = user2;
      return (User) user2;
    }

    /// <summary>Gets the user with the specified identity.</summary>
    /// <param name="pageId">The identity to search for.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Security.Model.User" />.</returns>
    public override User GetUser(Guid id)
    {
      if (id == Guid.Empty)
        throw new ArgumentException("ID cannot be empty GUID.");
      this.GetTransaction();
      MembershipUser user = this.membershipProvider.GetUser((object) id, false);
      if (user == null)
        return (User) null;
      this.userList[id] = UserWrapper.CopyFrom(user, this.ApplicationName);
      return (User) this.userList[id];
    }

    /// <summary>Get user by specific email</summary>
    /// <param name="email">User's email</param>
    /// <returns> returns user if there is a user with specified email or null if there isn't user with specified email</returns>
    public override User GetUserByEmail(string email)
    {
      this.GetTransaction();
      string userNameByEmail = this.membershipProvider.GetUserNameByEmail(email);
      return userNameByEmail != null ? this.GetUser(userNameByEmail) : (User) null;
    }

    /// <summary>Gets a query for users.</summary>
    /// <returns>The query for users.</returns>
    public override IQueryable<User> GetUsers()
    {
      if (this.membershipProvider is IBasicQueryExecutor membershipProvider)
        return (IQueryable<User>) new BasicQuery<User>(membershipProvider);
      this.GetTransaction();
      int totalRecords;
      this.membershipProvider.GetAllUsers(0, 1, out totalRecords);
      if (totalRecords == 0)
        totalRecords = 1;
      MembershipUserCollection allUsers = this.membershipProvider.GetAllUsers(0, totalRecords, out totalRecords);
      List<UserWrapper> source = new List<UserWrapper>();
      foreach (object user in allUsers)
      {
        UserWrapper userWrapper = UserWrapper.CopyFrom(user as MembershipUser, this.ApplicationName);
        source.Add(userWrapper);
      }
      return (IQueryable<User>) source.AsQueryable<UserWrapper>();
    }

    /// <summary>
    /// Resets a user's password to a new, automatically generated password.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <param name="answer">The password answer for the specified user.</param>
    /// <returns>throws NotSupportedException exception</returns>
    public override string ResetPassword(User user, string answer)
    {
      if (user == null)
        throw new ProviderException(Res.Get<ErrorMessages>().MembershipUserNotFound);
      return this.ResetPassword(user.UserName, answer);
    }

    /// <summary>
    /// Resets a user's password to a new, automatically generated password.
    /// </summary>
    /// <param name="userName">The user to reset the password for.</param>
    /// <param name="answer">The password answer for the specified user.</param>
    /// <returns>The new password for the specified user.</returns>
    public override string ResetPassword(string userName, string answer) => this.membershipProvider.ResetPassword(userName, answer);

    /// <summary>
    /// Resets a user's password to a new, automatically generated password.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <param name="answer">The password answer for the specified user.</param>
    /// <returns>The new password for the specified user.</returns>
    public override string ResetPassword(Guid userId, string answer) => this.ResetPassword(this.GetUser(userId), answer);

    /// <summary>Check if there is a user with this username</summary>
    /// <param name="userName">Username to check</param>
    /// <returns>true if there is a user with specified username</returns>
    public override bool UserExists(string userName) => this.membershipProvider.GetUser(userName, false) != null;

    /// <summary>
    /// Verifies that the specified user and password exist in the data source.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <param name="password">The password for the specified user.</param>
    /// <returns>
    /// true if the specified username and password are valid; otherwise, false.
    /// </returns>
    public override bool ValidateUser(User user, string password)
    {
      if (user == null)
        return false;
      int num = this.membershipProvider.ValidateUser(user.UserName, password) ? 1 : 0;
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
    /// Clears a lock so that the membership user can be validated.
    /// </summary>
    /// <param name="userId">The user ID.</param>
    /// <returns>
    /// true if the membership user was successfully unlocked; otherwise, false.
    /// </returns>
    public override bool UnlockUser(Guid userId) => this.UnlockUser(this.GetUser(userId));

    /// <summary>
    /// Clears a lock so that the membership user can be validated.
    /// </summary>
    /// <param name="userName">The membership user whose lock status you want to clear.</param>
    /// <returns>
    /// true if the membership user was successfully unlocked; otherwise, false.
    /// </returns>
    public override bool UnlockUser(string userName) => this.membershipProvider.UnlockUser(userName);

    /// <summary>
    /// Clears a lock so that the membership user can be validated.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <returns>
    /// true if the membership user was successfully unlocked; otherwise, false.
    /// </returns>
    public override bool UnlockUser(User user) => user != null ? this.UnlockUser(user.UserName) : throw new ProviderException(Res.Get<ErrorMessages>().MembershipUserNotFound);

    protected internal override Guid GetNewGuid() => Guid.NewGuid();

    /// <summary>Commits the provided transaction.</summary>
    [GlobalPermission(new string[] {"ManageUsers"})]
    public override void CommitTransaction()
    {
      this.CollectEventsData();
      this.CommitTransactionInternal();
    }

    /// <summary>
    /// Flush all dirty and new instances to the database and evict all instances from the local cache.
    /// </summary>
    public override void FlushTransaction()
    {
      this.CollectEventsData();
      this.CommitTransactionInternal();
    }

    private void CommitTransactionInternal()
    {
      List<UserEventBase> userEventBaseList = new List<UserEventBase>();
      object origin;
      this.TryGetExecutionStateData("EventOriginKey", out origin);
      foreach (UserWrapper user1 in this.userList.Values.ToList<UserWrapper>())
      {
        UserEventBase userEvent = (UserEventBase) null;
        if (user1.IsNew)
        {
          string passwordQuestion = user1.PasswordQuestion;
          string passwordAnswer = user1.PasswordAnswer;
          if (passwordQuestion == string.Empty)
            passwordQuestion = (string) null;
          if (passwordAnswer == string.Empty)
            passwordAnswer = (string) null;
          this.membershipProvider.CreateUser(user1.UserName, user1.Password, user1.Email, passwordQuestion, passwordAnswer, user1.IsApproved, (object) user1.Id, out MembershipCreateStatus _);
          user1.IsNew = false;
          user1.IsDirty = false;
          userEvent = (UserEventBase) new UserCreated();
        }
        if (user1.IsDirty)
        {
          MembershipUser user2 = this.membershipProvider.GetUser(user1.UserName, false);
          if (user2 != null)
          {
            user1.CopyDetailsToMembershipUser(ref user2);
            this.membershipProvider.UpdateUser(user2);
            user1.IsDirty = false;
            userEvent = (UserEventBase) new UserUpdated()
            {
              ApprovalStatusChanged = user1.IsAprovalStatusChanged,
              PasswordChanged = user1.IsPasswordChanged
            };
          }
          else
            continue;
        }
        if (user1.IsDeleted)
        {
          this.membershipProvider.DeleteUser(user1.UserName, true);
          userEvent = (UserEventBase) new UserDeleted();
        }
        if (userEvent != null)
        {
          this.PopulateUserEventBase(userEvent, user1);
          userEventBaseList.Add(userEvent);
        }
      }
      this.userList.Clear();
      foreach (IEvent eventData in userEventBaseList)
        this.RaiseEvent(eventData, origin, false);
    }

    private void CollectEventsData()
    {
      object origin;
      this.TryGetExecutionStateData("EventOriginKey", out origin);
      try
      {
        foreach (UserWrapper user in this.userList.Values.ToList<UserWrapper>())
        {
          if (user.IsNew)
          {
            UserEventBase userEventBase = (UserEventBase) new UserCreating()
            {
              User = (User) user
            };
            this.PopulateUserEventBase(userEventBase, user);
            this.RaiseEvent((IEvent) userEventBase, origin, true);
          }
          if (user.IsDirty)
          {
            UserEventBase userEventBase = (UserEventBase) new UserUpdating()
            {
              ApprovalStatusChanged = user.IsAprovalStatusChanged,
              PasswordChanged = user.IsPasswordChanged,
              User = (User) user
            };
            this.PopulateUserEventBase(userEventBase, user);
            this.RaiseEvent((IEvent) userEventBase, origin, true);
          }
          if (user.IsDeleted)
          {
            UserEventBase userEventBase = (UserEventBase) new UserDeleting()
            {
              User = (User) user
            };
            this.PopulateUserEventBase(userEventBase, user);
            this.RaiseEvent((IEvent) userEventBase, origin, true);
          }
        }
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    private void RaiseEvent(IEvent eventData, object origin, bool throwExceptions)
    {
      if (origin != null)
        eventData.Origin = (string) origin;
      EventHub.Raise(eventData, throwExceptions);
    }

    private void PopulateUserEventBase(UserEventBase userEvent, UserWrapper user)
    {
      userEvent.UserId = user.Id;
      userEvent.UserName = user.UserName;
      userEvent.Email = user.Email;
      userEvent.MembershipProviderName = user.ProviderName;
      userEvent.IsApproved = user.IsApproved;
      userEvent.Password = user.Password;
      userEvent.PasswordFormat = user.PasswordFormat;
    }

    /// <summary>This method returns new transaction object.</summary>
    /// <param name="transactionName">Name of the transaction.</param>
    /// <returns>The transaction object.</returns>
    protected internal override object CreateNewTransaction(string transactionName)
    {
      if (transactionName.IsNullOrEmpty())
        return new object();
      string name = this.membershipProvider.Name;
      object newTransaction = TransactionManager.GetTransaction<object>(transactionName, name);
      if (newTransaction == null)
      {
        newTransaction = new object();
        string transactionName1 = transactionName;
        string connectionString = name;
        EmptyDecorator decorator = new EmptyDecorator();
        decorator.DataProvider = (DataProviderBase) this;
        object transaction = newTransaction;
        TransactionManager.AddTransaction(transactionName1, connectionString, (IDataProviderDecorator) decorator, transaction);
      }
      return newTransaction;
    }

    /// <summary>Gets the dirty items from the current transaction.</summary>
    /// <returns>An array of dirty items.</returns>
    public override IList GetDirtyItems() => (IList) new ArrayList();
  }
}
