// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.UserActivityManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Licensing;
using Telerik.Sitefinity.Licensing.Model;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Data;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Security
{
  /// <summary>Provides information about user activity</summary>
  internal class UserActivityManager : ManagerBase<UserActivityProvider>, ICacheItemRefreshAction
  {
    private ConfigElementDictionary<string, DataProviderSettings> providerSettings;

    /// <summary>
    /// Initializes a new instance of <see cref="T:Telerik.Sitefinity.Security.UserActivityManager" /> calss with the default provider.
    /// </summary>
    public UserActivityManager()
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="T:Telerik.Sitefinity.Security.UserActivityManager" /> calss and sets the specified provider.
    /// </summary>
    /// <param name="providerName">
    /// The name of the provider. If empty string or null the default provider is set.
    /// </param>
    public UserActivityManager(string providerName)
      : base(providerName)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.UserActivityManager" /> class.
    /// </summary>
    /// <param name="providerName">
    /// The name of the provider. If empty string or null the default provider is set
    /// </param>
    /// <param name="transactionName">
    /// The name of a distributed transaction. If empty string or null this manager will use separate transaction.
    /// </param>
    public UserActivityManager(string providerName, string transactionName)
      : base(providerName, transactionName)
    {
    }

    /// <summary>Gets the current UserActivityManager.</summary>
    /// <value>The current UserActivityManager.</value>
    internal static UserActivityManager Current => UserActivityManager.GetManager();

    /// <summary>
    /// Gets the name of the default provider for this manager.
    /// </summary>
    protected internal override GetDefaultProvider DefaultProviderDelegate => (GetDefaultProvider) (() => "OpenAccessUserActivityProvider");

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
          this.providerSettings = Config.Get<UserActivityConfig>().Providers;
        return this.providerSettings;
      }
    }

    /// <summary>
    /// Saves any changes made to objects retrieved with this manager.
    /// </summary>
    public override void SaveChanges()
    {
      if (!typeof (OpenAccessUserActivityProvider).IsAssignableFrom(this.Provider.GetType()))
        throw new SecurityException("Unsupported provider");
      base.SaveChanges();
    }

    /// <summary>Gets an instance for UserActivityManager.</summary>
    /// <returns>An instance of UserActivityManager.</returns>
    public static UserActivityManager GetManager() => ManagerBase<UserActivityProvider>.GetManager<UserActivityManager>();

    /// <summary>
    /// Gets an instance for UserActivityManager for the specified data provider.
    /// </summary>
    /// <param name="providerName">The name of the data provider.</param>
    /// <returns>An instance of UserActivityManager.</returns>
    public static UserActivityManager GetManager(string providerName) => ManagerBase<UserActivityProvider>.GetManager<UserActivityManager>(providerName);

    /// <summary>
    /// Gets an instance for UserActivityManager for the specified data provider.
    /// </summary>
    /// <param name="providerName">The name of the data provider.</param>
    /// <returns>An instance of UserActivityManager.</returns>
    /// <param name="transactionName">Name of the transaction.</param>
    /// <returns></returns>
    public static UserActivityManager GetManager(
      string providerName,
      string transactionName)
    {
      return ManagerBase<UserActivityProvider>.GetManager<UserActivityManager>(providerName, transactionName);
    }

    /// <summary>
    /// Returns the user activity record for the specified userId and providerName.
    /// </summary>
    /// <param name="userId">The userId of the user.</param>
    /// <param name="providerName">The provider name.</param>
    /// <returns>UserActivity record or null if none was found.</returns>
    public UserActivity GetUserActivity(Guid userId, string providerName) => this.Provider.GetUserActivity(userId, providerName);

    /// <summary>Creates a new user activity record.</summary>
    /// <param name="userId">The id of the user.</param>
    /// <param name="loginIp">The IP address he logged in from.</param>
    /// <param name="providerName">The name of the membership user provider.</param>
    /// <param name="isBackendUser">Whether the user is a backend user.</param>
    /// <param name="lastLoginDate">The date of the last login of the user.</param>
    /// <param name="lastActivityDate">The date of the last activity of the user.</param>
    /// <returns>The new user activity record.</returns>
    public UserActivity CreateUserActivity(
      Guid userId,
      string loginIp,
      string providerName,
      bool isBackendUser,
      DateTime lastLoginDate,
      DateTime lastActivityDate,
      string tokenId = null)
    {
      return this.Provider.CreateUserActivity(userId, loginIp, providerName, isBackendUser, lastLoginDate, lastActivityDate, tokenId);
    }

    /// <summary>
    /// Deletes the activity for the given userId and providerName.
    /// </summary>
    /// <param name="userId">The id of the user.</param>
    /// <param name="providerName">The name of the provider for the user.</param>
    public void DeleteUserActivity(Guid userId, string providerName) => this.Provider.DeleteUserActivity(userId, providerName);

    /// <summary>Returns a query for all the user activity records.</summary>
    /// <returns>A query for all the user activity records.</returns>
    public IQueryable<UserActivity> UserActivities() => this.Provider.GetUserActivities();

    internal virtual int Count => UserActivityManager.Cache.Count;

    internal static UserActivityRecord GetCurrentUserActivity() => (UserActivityRecord) UserActivityManager.Cache[SecurityManager.CurrentUserId.ToString()];

    /// <summary>Cache of the user activity records</summary>
    internal static ICacheManager Cache => SystemManager.GetCacheManager(CacheManagerInstance.UserActivities);

    internal virtual TimeSpan UserActivityCacheItemExpiration => TimeSpan.FromMinutes(1.0);

    internal virtual void RemoveFromCache(Guid userId)
    {
      string key = userId.ToString();
      if (!UserActivityManager.Cache.Contains(key))
        return;
      UserActivityManager.Cache.Remove(key);
    }

    internal virtual void UpdateRecord(Guid userId, DateTime lastActivityDate)
    {
      UserActivityRecord fromCache = this.GetFromCache(userId);
      if (fromCache == null)
        return;
      fromCache.LastActivityDate = lastActivityDate;
    }

    internal virtual void AddToCache(
      Guid userId,
      string loginIp,
      string providerName,
      bool isBackendUser,
      DateTime lastLoginDate,
      DateTime lastActivityDate,
      List<Guid> allowedSites,
      string tokenId = null)
    {
      string key = userId.ToString();
      UserActivityRecord userActivityRecord = new UserActivityRecord();
      this.RemoveFromCache(userId);
      userActivityRecord.UserId = userId;
      userActivityRecord.ProviderName = providerName;
      userActivityRecord.LoginIP = loginIp;
      userActivityRecord.IsBackendUser = isBackendUser;
      userActivityRecord.LastLoginDate = lastLoginDate;
      userActivityRecord.LastActivityDate = lastActivityDate;
      userActivityRecord.TokenId = tokenId;
      if (allowedSites != null)
        userActivityRecord.AllowedSites.AddRange((IEnumerable<Guid>) allowedSites);
      ICacheItemExpiration cacheItemExpiration = (ICacheItemExpiration) new AbsoluteTime(this.UserActivityCacheItemExpiration);
      UserActivityManager.Cache.Add(key, (object) userActivityRecord, CacheItemPriority.Normal, (ICacheItemRefreshAction) this, cacheItemExpiration);
    }

    internal virtual UserActivityRecord GetFromCache(Guid userId) => (UserActivityRecord) UserActivityManager.Cache[userId.ToString()];

    public virtual void Refresh(
      string removedKey,
      object expiredValue,
      CacheItemRemovedReason removalReason)
    {
      try
      {
        if (removalReason != CacheItemRemovedReason.Expired && removalReason != CacheItemRemovedReason.Scavenged || expiredValue == null)
          return;
        UserActivityRecord record = (UserActivityRecord) expiredValue;
        if (UserActivityManager.Cache.Contains(DeletedUserRecord.GetKey(record.UserId, record.ProviderName)))
          this.RemoveFromCache(record.UserId);
        else
          SecurityManager.UpdateUserActivity(record);
      }
      catch (Exception ex)
      {
        if (!Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
          return;
        throw;
      }
    }

    /// <summary>
    /// Registers the deleted user.
    /// User is logged in so we register this user is deleted and on the next request
    /// the user will be redirected to the login page
    /// </summary>
    /// <param name="user">The user.</param>
    internal static void RegisterDeletedUser(User user)
    {
      UserActivityManager.Current.RemoveFromCache(user.Id);
      if (!user.IsLoggedIn || !(user.LastActivityDate > SecurityManager.ExpiredSessionsLastLoginDate))
        return;
      string key = DeletedUserRecord.GetKey(user.Id, user.ProviderName);
      if (UserActivityManager.Cache.Contains(key))
        UserActivityManager.Cache.Remove(key);
      ICacheItemExpiration cacheItemExpiration = (ICacheItemExpiration) new AbsoluteTime(SecurityManager.AuthCookieTimeout.Add(TimeSpan.FromMinutes(5.0)));
      DeletedUserRecord deletedUserRecord = new DeletedUserRecord()
      {
        UserId = user.Id,
        Provider = user.ProviderName,
        LogDate = DateTime.UtcNow
      };
      UserActivityManager.Cache.Add(key, (object) deletedUserRecord, CacheItemPriority.Normal, (ICacheItemRefreshAction) null, cacheItemExpiration);
    }

    /// <summary>
    /// Registers the deleted user.
    /// User is logged in so we register this user is deleted and on the next request
    /// the user will be redirected to the login page
    /// </summary>
    /// <param name="userId">The user pageId.</param>
    /// <param name="providerName">Name of the provider.</param>
    internal static void RegisterDeletedUser(Guid userId, string providerName)
    {
      User user = UserManager.GetManager(providerName).GetUser(userId);
      if (user == null)
        return;
      UserActivityManager.RegisterDeletedUser(user);
    }

    /// <summary>
    /// Finds the and unregister the user from the deleted users list.
    /// </summary>
    /// <param name="userId">Id of the user.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns></returns>
    internal static DeletedUserRecord FindDeletedUser(
      Guid userId,
      string providerName)
    {
      return (DeletedUserRecord) UserActivityManager.Cache.GetData(DeletedUserRecord.GetKey(userId, providerName));
    }
  }
}
