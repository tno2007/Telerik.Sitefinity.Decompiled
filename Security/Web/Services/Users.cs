// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.Services.Users
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Web;
using System.Web.Security;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.Licensing.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Data;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Utilities;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Security.Web.Services
{
  /// <summary>
  /// The WCF web service implementation for membership users management.
  /// </summary>
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  public class Users : IUsers
  {
    internal static TimeSpan AuthCookieTimeout = Config.Get<SecurityConfig>().AuthCookieTimeout;
    private const string PredefinedFilterLoggedInUsers = "predefinedFilterLoggedInUsers";
    private const int MaximumUserSearchResults = 200;

    /// <summary>Get all users, not filtering them by role</summary>
    /// <param name="usersProvider">Name of the users provider to use</param>
    /// <param name="forAllUserProviders">True to combine the result of all user providers, false to use the one specified by <paramref name="usersProvider" /></param>
    /// <param name="sort">Sort expression</param>
    /// <param name="filter">Filter expression</param>
    /// <param name="skip">Used for paging. Start taking items from that number of items.</param>
    /// <param name="take">Used for paging. Take the first x items, starting from <paramref name="skip" /></param>
    /// <returns>Returns all users in a provider or all providers.</returns>
    public WcfMembershipUserCollectionContext GetAllUsers(
      string usersProvider,
      bool forAllUserProviders,
      string sort,
      string filter,
      int skip,
      int take,
      string roleProviderName,
      string roleIdString)
    {
      UserManager manager = UserManager.GetManager(usersProvider);
      WcfMembershipUserCollectionContext allUsers;
      if (!forAllUserProviders)
      {
        IQueryable<User> users = manager.GetUsers();
        int? nullable = new int?(0);
        string filterExpression = filter;
        string orderExpression = sort;
        int? skip1 = new int?(skip);
        int? take1 = new int?(take);
        ref int? local = ref nullable;
        WcfMembershipUserCollectionContext collectionContext = new WcfMembershipUserCollectionContext(this.ConvertUsersToWcfFormat((IList<User>) DataProviderBase.SetExpressions<User>(users, filterExpression, orderExpression, skip1, take1, ref local).ToList<User>()));
        collectionContext.TotalCount = nullable.Value;
        allUsers = collectionContext;
      }
      else
      {
        int totalCount = 0;
        WcfMembershipUserCollectionContext collectionContext = new WcfMembershipUserCollectionContext(this.ConvertUsersToWcfFormat(ManagerBase<MembershipDataProvider>.JoinResult<User>((GetQuery<MembershipDataProvider, User>) (p => p.GetUsers()), filter, sort, skip, take, out totalCount, (ForEachItem<MembershipDataProvider, User>) null)));
        collectionContext.TotalCount = totalCount;
        allUsers = collectionContext;
      }
      this.DisableServiceCache();
      return allUsers;
    }

    /// <summary>Get all users, not filtering them by role</summary>
    /// <param name="usersProvider">Name of the users provider to use</param>
    /// <param name="forAllUserProviders">True to combine the result of all user providers, false to use the one specified by <paramref name="usersProvider" /></param>
    /// <param name="sort">Sort expression</param>
    /// <param name="filter">Filter expression</param>
    /// <param name="skip">Used for paging. Start taking items from that number of items.</param>
    /// <param name="take">Used for paging. Take the first x items, starting from <paramref name="skip" /></param>
    /// <returns>Returns all users in a provider or all providers.</returns>
    public WcfMembershipUserCollectionContext GetAllUsersInXml(
      string usersProvider,
      bool forAllUserProviders,
      string sort,
      string filter,
      int skip,
      int take,
      string roleProviderName,
      string roleIdString)
    {
      return this.GetAllUsers(usersProvider, forAllUserProviders, sort, filter, skip, take, roleProviderName, roleIdString);
    }

    /// <inheritdoc />
    public CollectionContext<WcfMembershipUser> SearchUsers(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string searchText,
      string roleId,
      string roleProvider,
      bool forAllProviders)
    {
      return string.IsNullOrEmpty(searchText) || searchText == "predefinedFilterLoggedInUsers" ? this.GetUsersInternal(provider, sortExpression, skip, take, searchText, Telerik.Sitefinity.Utilities.Utility.StringToGuid(roleId), roleProvider, forAllProviders) : this.SearchUsersInternal(provider, sortExpression, skip, take, searchText, Telerik.Sitefinity.Utilities.Utility.StringToGuid(roleId), roleProvider, forAllProviders);
    }

    /// <summary>
    /// Gets the collection of users in JSON format. Returns the users sorted in ascedning order by their username.
    /// </summary>
    /// <param name="provider">The name of the membership provider from which the users should be retrieved.</param>
    /// <param name="sortExpression">The sort expression used to order the retrieved users.</param>
    /// <param name="skip">The number of users to skip before populating the collection (used primarily for paging).</param>
    /// <param name="take">The maximum number of users to take in the collection (used primarily for paging).</param>
    /// <param name="filter">The filter expression in dynamic LINQ format used to filter the retrieved users.</param>
    /// <param name="roleProvider">The role provider.</param>
    /// <param name="role">The role.</param>
    /// <returns>
    /// 	<see cref="T:Telerik.Sitefinity.Web.Services.CollectionContext`1" /> object with user items and other information about the retrieved collection.
    /// </returns>
    public CollectionContext<WcfMembershipUser> GetUsers(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string roleId,
      string roleProvider,
      bool forAllProviders)
    {
      return this.GetUsersInternal(provider, sortExpression, skip, take, filter, Telerik.Sitefinity.Utilities.Utility.StringToGuid(roleId), roleProvider, forAllProviders);
    }

    /// <inheritdoc />
    public CollectionContext<WcfMembershipUserDisplayInfo> GetUsersDisplayInfo(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string roleId,
      string roleProvider,
      bool forAllProviders)
    {
      int totalCount;
      return new CollectionContext<WcfMembershipUserDisplayInfo>(this.GetUsers(provider, sortExpression, skip, take, filter, Telerik.Sitefinity.Utilities.Utility.StringToGuid(roleId), roleProvider, forAllProviders, out totalCount).Select<User, WcfMembershipUserDisplayInfo>((Func<User, WcfMembershipUserDisplayInfo>) (u => new WcfMembershipUserDisplayInfo(u))))
      {
        TotalCount = totalCount
      };
    }

    /// <inheritdoc />
    private CollectionContext<WcfMembershipUser> SearchUsersInternal(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string searchString,
      Guid roleId,
      string roleProvider,
      bool forAllProviders)
    {
      string str = UserProfilesHelper.BuildFirstNameLastNameFilter(searchString);
      ParameterExpression parameterExpression;
      // ISSUE: method reference
      List<string> list1 = UserProfilesHelper.GetUserProfileManager(typeof (SitefinityProfile)).GetUserProfiles<SitefinityProfile>().Where<SitefinityProfile>(str).Take<SitefinityProfile>(200).Select<SitefinityProfile, string>(Expression.Lambda<Func<SitefinityProfile, string>>((Expression) Expression.Call(x.Owner, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.ToString)), Array.Empty<Expression>()), parameterExpression)).ToList<string>();
      string filter = UserProfilesHelper.BuildUsersSearchFilterInlcuingResultsFromSitefinityProfile(searchString, str, (IEnumerable<string>) list1);
      int totalCount = 0;
      int take1 = skip >= 200 ? 0 : Math.Min(take, 200 - skip);
      List<User> list2 = this.GetUsers(provider, sortExpression, skip, take1, filter, roleId, roleProvider, forAllProviders, out totalCount).ToList<User>();
      int num = Math.Min(200, totalCount);
      return new CollectionContext<WcfMembershipUser>(this.ConvertUsersToWcfFormat((IList<User>) list2))
      {
        TotalCount = num
      };
    }

    /// <summary>
    /// Gets the collection of users in XML format. Returns the users sorted in ascedning order by their username.
    /// </summary>
    /// <param name="provider">The name of the membership provider from which the users should be retrieved.</param>
    /// <param name="sortExpression">The sort expression used to order the retrieved users.</param>
    /// <param name="skip">The number of users to skip before populating the collection (used primarily for paging).</param>
    /// <param name="take">The maximum number of users to take in the collection (used primarily for paging).</param>
    /// <param name="filter">The filter expression in dynamic LINQ format used to filter the retrieved users.</param>
    /// <param name="roleId">The role pageId.</param>
    /// <param name="roleProvider">The role provider.</param>
    /// <param name="forAllProviders">if set to <c>true</c> [for all providers].</param>
    /// <returns>
    /// 	<see cref="T:Telerik.Sitefinity.Web.Services.CollectionContext`1" /> object with user items and other information about the retrieved collection.
    /// </returns>
    public CollectionContext<WcfMembershipUser> GetUsersInXml(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string roleId,
      string roleProvider,
      bool forAllProviders)
    {
      return this.GetUsersInternal(provider, sortExpression, skip, take, filter, Telerik.Sitefinity.Utilities.Utility.StringToGuid(roleId), roleProvider, forAllProviders);
    }

    /// <summary>Gets the user.</summary>
    /// <param name="userId">Id of the user to be retrieved.</param>
    /// <param name="provider">The name of membership provider.</param>
    /// <returns></returns>
    public WcfMembershipUser GetUser(string userId, string provider)
    {
      Guid guid = Telerik.Sitefinity.Utilities.Utility.StringToGuid(userId);
      return guid == Guid.Empty ? (WcfMembershipUser) null : this.GetUserInternal(guid, provider);
    }

    /// <summary>Gets the user in XML.</summary>
    /// <param name="userId">Id of the user to be retrieved.</param>
    /// <param name="provider">The name of membership provider.</param>
    /// <returns></returns>
    public WcfMembershipUser GetUserInXml(string userId, string provider)
    {
      Guid guid = Telerik.Sitefinity.Utilities.Utility.StringToGuid(userId);
      return guid == Guid.Empty ? (WcfMembershipUser) null : this.GetUserInternal(guid, provider);
    }

    /// <summary>
    /// Gets all the active providers with the number of registered users, optionally filtered by role. The results are returned in JSON format
    /// </summary>
    /// <param name="roleId">Optional Id of the role to filter</param>
    /// <param name="roleProvider">Provider of the role, related to the roleId</param>
    /// <returns>A collection of UserProviderItem items</returns>
    public CollectionContext<UserProviderItem> GetUserProviders(
      string roleId,
      string roleProvider)
    {
      return this.GetUserProviders(roleId, roleProvider, "");
    }

    /// <summary>
    /// Gets all the active providers with the number of registered users, optionally filtered by role. The results are returned in JSON format
    /// </summary>
    /// <param name="roleId">Optional Id of the role to filter</param>
    /// <param name="roleProvider">Provider of the role, related to the roleId</param>
    /// <param name="userFilter">userFilter</param>
    /// <returns>A collection of UserProviderItem items</returns>
    public CollectionContext<UserProviderItem> GetUserProviders(
      string roleId,
      string roleProvider,
      string userFilter)
    {
      string predicate = this.ProcessPredefinedFilters(userFilter);
      Guid guid = Telerik.Sitefinity.Utilities.Utility.StringToGuid(roleId);
      RoleManager roleMan = !(guid == Guid.Empty) || !string.IsNullOrEmpty(roleProvider) ? RoleManager.GetManager(roleProvider) : (RoleManager) null;
      List<UserProviderItem> items = new List<UserProviderItem>();
      UserProviderItem userProviderItem1 = new UserProviderItem();
      userProviderItem1.UserProviderTitle = this.GetAllUsersLabel();
      userProviderItem1.UserProviderName = string.Empty;
      items.Add(userProviderItem1);
      foreach (MembershipDataProvider siteProvider in UserManager.GetManager().GetSiteProviders())
      {
        UserProviderItem userProviderItem2 = new UserProviderItem();
        userProviderItem2.UserProviderTitle = siteProvider.Title;
        userProviderItem2.UserProviderName = siteProvider.Name;
        if (roleMan != null)
        {
          string name = siteProvider.Name;
          userProviderItem2.NumOfUsers = (long) this.GetNumOfUsersForRole(roleMan, guid, name, predicate);
        }
        else if (userFilter == "predefinedFilterLoggedInUsers")
        {
          IQueryable<UserActivity> queryable = UserActivityManager.GetManager().UserActivities().Where<UserActivity>(predicate);
          int num = 0;
          foreach (UserActivity userActivity in (IEnumerable<UserActivity>) queryable)
          {
            if (userActivity.ProviderName == siteProvider.Name)
              ++num;
          }
          userProviderItem2.NumOfUsers = (long) num;
        }
        else
        {
          IQueryable<User> source = siteProvider.GetUsers();
          if (!string.IsNullOrEmpty(predicate))
            source = source.Where<User>(predicate);
          userProviderItem2.NumOfUsers = (long) source.Count<User>();
        }
        items.Add(userProviderItem2);
        userProviderItem1.NumOfUsers += userProviderItem2.NumOfUsers;
      }
      this.DisableServiceCache();
      return new CollectionContext<UserProviderItem>((IEnumerable<UserProviderItem>) items);
    }

    /// <summary>
    /// Inserts/Updates the user information. The update user information is returned in JSON.
    /// </summary>
    /// <param name="user">User object to be created.</param>
    /// <param name="userId">Id of the user to be created.</param>
    /// <param name="provider">The name of membership provider.</param>
    /// <returns></returns>
    public WcfMembershipUser CreateUser(
      WcfMembershipUser user,
      string userId,
      string provider)
    {
      Guid guid = Telerik.Sitefinity.Utilities.Utility.StringToGuid(userId);
      this.DisableServiceCache();
      return this.SaveUserInternal(user, guid, provider, false, true);
    }

    /// <summary>
    /// Inserts/Updates the user information. The update user information is returned in XML.
    /// </summary>
    /// <param name="user">User object to be created.</param>
    /// <param name="userId">Id of the user to be created.</param>
    /// <param name="provider">The name of membership provider.</param>
    /// <returns></returns>
    public WcfMembershipUser CreateUserInXml(
      WcfMembershipUser user,
      string userId,
      string provider)
    {
      Guid guid = Telerik.Sitefinity.Utilities.Utility.StringToGuid(userId);
      this.DisableServiceCache();
      return this.SaveUserInternal(user, guid, provider, false, true);
    }

    /// <summary>
    /// Inserts/Updates the user information. The update user information is returned in JSON.
    /// </summary>
    /// <param name="user">User object to be updated.</param>
    /// <param name="userId">Id of the user to be updated.</param>
    /// <param name="provider">The name of membership provider.</param>
    /// <returns></returns>
    public WcfMembershipUser UpdateUser(
      WcfMembershipUser user,
      string userId,
      string provider)
    {
      Guid guid = Telerik.Sitefinity.Utilities.Utility.StringToGuid(userId);
      this.DisableServiceCache();
      return this.SaveUserInternal(user, guid, provider, true, true);
    }

    /// <summary>
    /// Updates the user's basic information, without affecting the user's roles. The update user information is returned in JSON.
    /// </summary>
    /// <param name="user">User object to be updated.</param>
    /// <param name="userId">Id of the user to be updated.</param>
    /// <param name="provider">The name of membership provider.</param>
    /// <returns></returns>
    public WcfMembershipUser UpdateUserBasic(
      WcfMembershipUser user,
      string userId,
      string provider)
    {
      Guid guid = Telerik.Sitefinity.Utilities.Utility.StringToGuid(userId);
      this.DisableServiceCache();
      return this.SaveUserInternal(user, guid, provider, true, false);
    }

    /// <summary>
    /// Inserts/Updates the user information. The update user information is returned in XML.
    /// </summary>
    /// <param name="user">User object to be updated.</param>
    /// <param name="userId">Id of the user to be update.</param>
    /// <param name="provider">The name of membership provider.</param>
    /// <returns></returns>
    public WcfMembershipUser UpdateUserInXml(
      WcfMembershipUser user,
      string userId,
      string provider)
    {
      Guid guid = Telerik.Sitefinity.Utilities.Utility.StringToGuid(userId);
      this.DisableServiceCache();
      return this.SaveUserInternal(user, guid, provider, true, true);
    }

    /// <summary>
    /// Deletes the user by username and returns true if the user has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="userId">Id of the user to be deleted.</param>
    /// <param name="provider">The name of membership provider.</param>
    /// <returns></returns>
    public bool DeleteUser(string userId, string provider)
    {
      Guid guid = Telerik.Sitefinity.Utilities.Utility.StringToGuid(userId);
      this.DisableServiceCache();
      return this.DeleteUserInternal(guid, provider);
    }

    /// <summary>
    /// Deletes the user by username and returns true if the user has been deleted; otherwise false.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="userId">Id of the user to be deleted.</param>
    /// <param name="provider">The name of membership provider.</param>
    /// <returns></returns>
    public bool DeleteUserInXml(string userId, string provider)
    {
      Guid guid = Telerik.Sitefinity.Utilities.Utility.StringToGuid(userId);
      this.DisableServiceCache();
      return this.DeleteUserInternal(guid, provider);
    }

    /// <summary>Gets the user/roles relations in JSON format.</summary>
    /// <param name="userProviderName">The membership provider name of the user.</param>
    /// <param name="userName">The name of the user from which the user/roles relation should be retrieved.</param>
    /// <param name="provider">The name of the role provider from which the user/roles relation should be retrieved.</param>
    /// <param name="sortExpression">The sort expression used to order the retrieved user/roles relations.</param>
    /// <param name="skip">The number of user/roles relations to skip before populating the collection (used primarily for paging).</param>
    /// <param name="take">The maximum number of user/roles relations to take in the collection (used primarily for paging).</param>
    /// <param name="filter">The filter expression in dynamic LINQ format used to filter the retrieved user/roles relations.</param>
    /// <returns>
    /// Returns CollectionContext object with user/roles relations and other information about the retrieved collection.
    /// </returns>
    public CollectionContext<RoleProviderPair> GetUserRoles(
      string userProviderName,
      string userName,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      return this.GetUserRolesInternal(userProviderName, userName, provider, sortExpression, skip, take, filter);
    }

    /// <summary>Gets the user/roles relations in XML format.</summary>
    /// <param name="userProviderName">The membership provider name of the user.</param>
    /// <param name="userName">The name of the user from which the user/roles relation should be retrieved.</param>
    /// <param name="provider">The name of the role provider from which the user/roles relation should be retrieved.</param>
    /// <param name="sortExpression">The sort expression used to order the retrieved user/roles relations.</param>
    /// <param name="skip">The number of user/roles relations to skip before populating the collection (used primarily for paging).</param>
    /// <param name="take">The maximum number of user/roles relations to take in the collection (used primarily for paging).</param>
    /// <param name="filter">The filter expression in dynamic LINQ format used to filter the retrieved user/roles relations.</param>
    /// <returns>
    /// Returns CollectionContext object with user/roles relations and other information about the retrieved collection.
    /// </returns>
    public CollectionContext<RoleProviderPair> GetUserRolesInXml(
      string userProviderName,
      string userName,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      return this.GetUserRolesInternal(userProviderName, userName, provider, sortExpression, skip, take, filter);
    }

    /// <summary>Adds user/role. Result is returned in JSON.</summary>
    /// <param name="usersRoles">The users roles.</param>
    public void SaveUserRole(UserRolesItem[] usersRoles) => this.SaveAndReturnUserRolesResource(usersRoles);

    /// <summary>Adds user/role. Result is returned in XML.</summary>
    /// <param name="propertyBag">The property bag.</param>
    /// <param name="userProviderName">The membership provider name of the user.</param>
    /// <param name="userName">The username.</param>
    /// <param name="role">The role.</param>
    /// <param name="provider">The name of role provider.</param>
    /// <returns>Nothing</returns>
    public void SaveUserRoleInXml(UserRolesItem[] usersRoles) => this.SaveAndReturnUserRolesResource(usersRoles);

    /// <summary>
    /// Deletes a user/role relation and returns true if the user/role relation has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="userProviderName">The membership provider name of the user.</param>
    /// <param name="userName">The username.</param>
    /// <param name="roleId"></param>
    /// <param name="roleProviderName"></param>
    /// <returns></returns>
    public bool DeleteUserRole(
      string userProviderName,
      string userName,
      string roleId,
      string roleProviderName)
    {
      return this.DeleteUserRoleRelation(userProviderName, userName, Telerik.Sitefinity.Utilities.Utility.StringToGuid(roleId), roleProviderName);
    }

    /// <summary>
    /// Deletes a user/role relation and returns true if the user/role relation has been deleted; otherwise false.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="userProviderName">The membership provider name of the user.</param>
    /// <param name="userName">The username.</param>
    /// <param name="roleId"></param>
    /// <param name="roleProviderName"></param>
    /// <returns></returns>
    public bool DeleteUserRoleInXml(
      string userProviderName,
      string userName,
      string roleId,
      string roleProviderName)
    {
      return this.DeleteUserRoleRelation(userProviderName, userName, Telerik.Sitefinity.Utilities.Utility.StringToGuid(roleId), roleProviderName);
    }

    /// <summary>Processes the predefined filters.</summary>
    /// <param name="filter">The filter.</param>
    /// <returns></returns>
    internal virtual string ProcessPredefinedFilters(string filter)
    {
      if (!(filter == "predefinedFilterLoggedInUsers"))
        return filter;
      DateTime universalTime = DateTime.UtcNow.AddTicks(-Users.AuthCookieTimeout.Ticks).ToUniversalTime();
      return "IsLoggedIn == true And IsBackendUser == true And LastActivityDate >= (" + universalTime.ToString("d", (IFormatProvider) CultureInfo.InvariantCulture) + " " + universalTime.ToString("HH:mm:ss") + ") ";
    }

    internal virtual CollectionContext<WcfMembershipUser> GetUsersInternal(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      Guid roleId,
      string roleProvider,
      bool forAllProviders)
    {
      int totalCount;
      CollectionContext<WcfMembershipUser> usersInternal = new CollectionContext<WcfMembershipUser>(this.ConvertUsersToWcfFormat(this.GetUsers(provider, sortExpression, skip, take, filter, roleId, roleProvider, forAllProviders, out totalCount)));
      usersInternal.TotalCount = totalCount;
      this.DisableServiceCache();
      return usersInternal;
    }

    /// <summary>
    /// Converts membership user to WCF membership user (for serialization reasons).
    /// </summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="user">The user.</param>
    /// <returns></returns>
    public virtual WcfMembershipUser ConvertToWcfMembershipUser(
      string providerName,
      User user)
    {
      if (user == null)
        return (WcfMembershipUser) null;
      string userDisplayName = UserProfilesHelper.GetUserDisplayName(user.Id);
      WcfMembershipUser wcfMembershipUser = new WcfMembershipUser(providerName, user.ProviderUserKey, user.Email, user.PasswordQuestion, user.Comment, user.IsApproved, user.IsLockedOut, user.CreationDate, user.LastLoginDate, user.LastActivityDate, user.LastPasswordChangedDate, user.LastLockoutDate, user.ProviderUserKey.ToString(), userDisplayName, user.IsLoggedIn, user.IsBackendUser, user.ExternalProviderName);
      Telerik.Sitefinity.Libraries.Model.Image image;
      wcfMembershipUser.AvatarImageUrl = UserProfilesHelper.GetAvatarImageUrl(user.Id, out image);
      wcfMembershipUser.AvatarThumbnailUrl = image == null || string.IsNullOrEmpty(image.MediaUrl) ? RouteHelper.ResolveUrl("~/SFRes/images/Telerik.Sitefinity.Resources/Images.DefaultPhoto.png", UrlResolveOptions.Rooted) : image.ThumbnailUrl;
      wcfMembershipUser.AvatarImageSmallerWidth = image != null && image.Height > image.Width;
      UserActivity userActivity = UserActivityManager.GetManager().GetUserActivity(user.Id, providerName);
      if (userActivity != null)
      {
        wcfMembershipUser.LastLoginDate = new DateTime?(user.LastLoginDate > userActivity.LastLoginDate ? user.LastLoginDate : userActivity.LastLoginDate);
        wcfMembershipUser.LastActivityDate = new DateTime?(userActivity.LastActivityDate);
        wcfMembershipUser.IsBackendUser = userActivity.IsBackendUser;
      }
      else
      {
        wcfMembershipUser.LastLoginDate = new DateTime?();
        wcfMembershipUser.LastActivityDate = new DateTime?();
      }
      wcfMembershipUser.IsLoggedIn = SecurityManager.IsUserOnline(providerName, user, userActivity);
      if (ManagerBase<RoleDataProvider>.StaticProvidersCollection == null)
      {
        RoleManager roleManager = new RoleManager();
      }
      List<RoleProviderPair> roleProviderPairList = new List<RoleProviderPair>();
      foreach (RoleDataProvider staticProviders in (Collection<RoleDataProvider>) ManagerBase<RoleDataProvider>.StaticProvidersCollection)
      {
        foreach (Role role in (IEnumerable<Role>) staticProviders.GetRolesForUser(user.Id))
          roleProviderPairList.Add(new RoleProviderPair()
          {
            RoleName = role.Name,
            RoleId = role.Id.ToString(),
            ProviderName = staticProviders.Name
          });
      }
      wcfMembershipUser.RolesOfUser = roleProviderPairList.ToArray();
      return wcfMembershipUser;
    }

    internal virtual WcfMembershipUser SaveUserInternal(
      WcfMembershipUser user,
      Guid userId,
      string providerName,
      bool update,
      bool updateRoles)
    {
      string transaction = "SaveUserTransaction" + Guid.NewGuid().ToString();
      bool flag1 = false;
      UserManager manager1 = UserManager.GetManager(providerName, transaction);
      bool flag2 = false;
      User createdUser = (User) null;
      try
      {
        if (userId == Guid.Empty)
        {
          MembershipCreateStatus status;
          createdUser = manager1.CreateUser(user.Email, user.Password, user.PasswordQuestion, user.PasswordAnswer, user.IsApproved, user.ProviderUserKey, out status);
          if (status != MembershipCreateStatus.Success)
            throw new MembershipCreateUserException(status);
        }
        else
        {
          createdUser = manager1.GetUser(userId);
          if (update)
          {
            if (string.IsNullOrEmpty(createdUser.ExternalProviderName) && !this.ValidateEmail(manager1.Provider, user))
              throw new DuplicateUserEmailException();
            this.SyncUserObjects(createdUser, user);
          }
        }
        if (updateRoles)
          flag2 = this.IsUserTheLastAdmin(userId, transaction);
        TransactionManager.FlushTransaction(transaction);
        UserProfilesSerializer profilesSerializer = new UserProfilesSerializer();
        Dictionary<UserProfile, UserProfileManager> changedProfiles = new Dictionary<UserProfile, UserProfileManager>();
        profilesSerializer.MetaTypeConverter.CreateInstanceDelegate = (CreateObjectInstanceDelegate) ((type, dictionary, serializer) =>
        {
          string fullName = type.FullName;
          UserProfileManager manager2 = UserProfileManager.GetManager(UserProfilesHelper.GetProfileTypeSettings(fullName).ProfileProvider, transaction);
          UserProfile key = manager2.GetUserProfile(createdUser, fullName) ?? manager2.CreateProfile(createdUser, fullName);
          changedProfiles.Add(key, manager2);
          return (IDynamicFieldsContainer) key;
        });
        profilesSerializer.Deserialize<Dictionary<string, UserProfile>>(user.ProfileData);
        foreach (KeyValuePair<UserProfile, UserProfileManager> keyValuePair in changedProfiles)
        {
          ((IDataItem) keyValuePair.Key).Transaction = (object) transaction;
          keyValuePair.Value.RecompileItemUrls<UserProfile>(keyValuePair.Key);
        }
        TransactionManager.FlushTransaction(transaction);
        if (updateRoles)
        {
          this.SynchronizeUserRoles(createdUser, user.RolesOfUser, transaction, new bool?(flag2));
          bool flag3 = ((IEnumerable<RoleProviderPair>) user.RolesOfUser).Any<RoleProviderPair>((Func<RoleProviderPair, bool>) (r => r.RoleId == SecurityManager.BackEndUsersRole.Id.ToString()));
          UserActivity userActivity = UserActivityManager.GetManager((string) null, transaction).GetUserActivity(createdUser.Id, providerName);
          if (userActivity != null && userActivity.IsBackendUser != flag3)
            userActivity.IsBackendUser = flag3;
        }
        TransactionManager.CommitTransaction(transaction);
        flag1 = true;
      }
      catch (UnauthorizedAccessException ex)
      {
        throw new WebProtocolException(HttpStatusCode.InternalServerError, Res.Get<ErrorMessages>().YouAreNotAuthorizedTo.Arrange((object) Res.Get<SecurityResources>().CreateOrModifyUsers), ex.InnerException);
      }
      catch (MembershipCreateUserException ex)
      {
        throw new WebProtocolException(HttpStatusCode.InternalServerError, this.GetErrorMessageFromStatusCode(manager1, ex.StatusCode), ex.InnerException);
      }
      catch (NotSupportedException ex)
      {
        throw ex;
      }
      catch (WebProtocolException ex)
      {
        throw ex;
      }
      catch (ModelValidationException ex)
      {
        throw new WebProtocolException(HttpStatusCode.InternalServerError, ex.GetMessage(), (Exception) ex);
      }
      catch (DuplicateUserEmailException ex)
      {
        throw new WebProtocolException(HttpStatusCode.InternalServerError, Res.Get<ErrorMessages>().CreateUserWizardDefaultDuplicateEmailErrorMessage, (Exception) ex);
      }
      catch (Exception ex)
      {
        throw new WebProtocolException(HttpStatusCode.InternalServerError, Res.Get<ErrorMessages>().WCFErrorOnSave, ex.InnerException);
      }
      finally
      {
        if (!flag1)
          TransactionManager.RollbackTransaction(transaction);
      }
      this.DisableServiceCache();
      return this.ConvertToWcfMembershipUser(manager1.Provider.Name, createdUser);
    }

    internal virtual WcfMembershipUser GetUserInternal(
      Guid userId,
      string providerName)
    {
      if (userId != SecurityManager.CurrentUserId)
        AppPermission.Root.Demand("Backend", "ManageUsers");
      UserManager manager = UserManager.GetManager(providerName);
      User user = manager.GetUser(userId);
      this.DisableServiceCache();
      WcfMembershipUser wcfMembershipUser = this.ConvertToWcfMembershipUser(manager.Provider.Name, user);
      wcfMembershipUser.ProfileData = new UserProfilesSerializer().Serialize((object) UserProfilesHelper.GetUserProfiles(user, out List<string> _, true));
      return wcfMembershipUser;
    }

    internal virtual bool DeleteUserInternal(Guid userId, string providerName)
    {
      bool flag = false;
      if (this.IsUserTheLastAdmin(userId))
        throw new WebProtocolException(HttpStatusCode.InternalServerError, Res.Get<SecurityResources>().UserIsLastAdministrator, (Exception) null);
      if (userId == SecurityManager.GetCurrentUserId())
        throw new WebProtocolException(HttpStatusCode.InternalServerError, Res.Get<SecurityResources>().YouCantDeleteTheCurrentUser, (Exception) null);
      User user1 = !(SecurityManager.CurrentUserId == userId) ? this.FindUserById(userId) : throw new WebProtocolException(HttpStatusCode.InternalServerError, Res.Get<SecurityResources>().UserIsCurrentlyLogged, (Exception) null);
      if (user1 != null)
      {
        UserProfilesHelper.DeleteUserProfiles(user1);
        if (string.IsNullOrEmpty(providerName))
          providerName = user1.ProviderName;
        UserManager manager1 = UserManager.GetManager(providerName);
        User user2 = manager1.GetUser(user1.Id);
        List<Role> roleList = new List<Role>();
        foreach (RoleDataProvider staticProviders in (Collection<RoleDataProvider>) ManagerBase<RoleDataProvider>.StaticProvidersCollection)
        {
          roleList.Clear();
          foreach (Role role in (IEnumerable<Role>) staticProviders.GetRolesForUser(user2.Id))
            roleList.Add(role);
          RoleManager manager2 = RoleManager.GetManager(staticProviders.Name);
          foreach (Role role in roleList)
            manager2.RemoveUserFromRole(user2, role);
          manager2.SaveChanges();
        }
        manager1.Delete(user2);
        manager1.SaveChanges();
        if (SecurityManager.CurrentUserId == userId)
        {
          SecurityManager.DeleteAuthCookies();
          string str = Config.Get<SecurityConfig>().Permissions["Backend"].LoginUrl;
          str = VirtualPathUtility.IsAppRelative(str) ? VirtualPathUtility.ToAbsolute(str) : throw new WebProtocolException(HttpStatusCode.PreconditionFailed, str, (Exception) null);
        }
        else
          flag = true;
      }
      return flag;
    }

    internal virtual bool IsUserTheLastAdmin(Guid userId, string transactionName = null)
    {
      RoleManager manager = RoleManager.GetManager("AppRoles", transactionName);
      return manager.IsUserInRole(userId, this.GetAdminRoleId()) && manager.GetUsersInRole(this.GetAdminRoleId()).Count <= 1;
    }

    private IList<User> GetUsers(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      Guid roleId,
      string roleProvider,
      bool forAllProviders,
      out int totalCount)
    {
      string str = this.ProcessPredefinedFilters(filter);
      if (string.IsNullOrEmpty(sortExpression))
        sortExpression = "UserName ASC";
      IList<User> users;
      if (!(roleId == Guid.Empty))
      {
        if (forAllProviders)
        {
          users = this.GetUsersForSpecificRoleFromAllMembershipProviders(roleProvider, roleId, str, sortExpression, skip, take, out totalCount);
        }
        else
        {
          RoleManager manager = RoleManager.GetManager(roleProvider);
          users = manager.GetUsersInRole(manager.GetRole(roleId), provider, str, sortExpression, skip, take, out totalCount);
        }
      }
      else if (forAllProviders)
      {
        if (filter == "predefinedFilterLoggedInUsers")
        {
          IQueryable<UserActivity> queryable = UserActivityManager.GetManager().UserActivities().Where<UserActivity>(str);
          IList<User> source = (IList<User>) new List<User>();
          totalCount = 0;
          foreach (UserActivity userActivity in (IEnumerable<UserActivity>) queryable)
          {
            User user = UserManager.GetManager(userActivity.ProviderName).GetUser(userActivity.UserId);
            source.Add(user);
            ++totalCount;
          }
          users = (IList<User>) source.AsQueryable<User>().OrderBy<User>(sortExpression).ToList<User>();
        }
        else
          users = this.GetUsersFromAllMembershipProviders(str, sortExpression, skip, take, out totalCount);
      }
      else if (filter == "predefinedFilterLoggedInUsers")
      {
        UserManager manager = UserManager.GetManager(provider);
        IQueryable<UserActivity> queryable = UserActivityManager.GetManager().UserActivities().Where<UserActivity>(str);
        IList<User> source = (IList<User>) new List<User>();
        totalCount = 0;
        foreach (UserActivity userActivity in (IEnumerable<UserActivity>) queryable)
        {
          if (userActivity.ProviderName == provider)
          {
            User user = manager.GetUser(userActivity.UserId);
            source.Add(user);
            ++totalCount;
          }
        }
        users = (IList<User>) source.AsQueryable<User>().OrderBy<User>(sortExpression).ToList<User>();
      }
      else
        users = this.GetUsersFromSpecificMembershipProvider(provider, str, sortExpression, skip, take, out totalCount);
      return users;
    }

    private bool ValidateEmail(MembershipDataProvider provider, WcfMembershipUser newData)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      Users.\u003C\u003Ec__DisplayClass32_0 cDisplayClass320 = new Users.\u003C\u003Ec__DisplayClass32_0()
      {
        newData = newData
      };
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      cDisplayClass320.email = cDisplayClass320.newData.Email.ToLower();
      ParameterExpression parameterExpression;
      // ISSUE: method reference
      // ISSUE: field reference
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: field reference
      // ISSUE: method reference
      // ISSUE: method reference
      bool flag = provider.GetUsers().Any<User>(Expression.Lambda<Func<User, bool>>((Expression) Expression.AndAlso((Expression) Expression.Equal((Expression) Expression.Call(u.Email, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (string.ToLower)), Array.Empty<Expression>()), (Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass320, typeof (Users.\u003C\u003Ec__DisplayClass32_0)), FieldInfo.GetFieldFromHandle(__fieldref (Users.\u003C\u003Ec__DisplayClass32_0.email)))), (Expression) Expression.NotEqual((Expression) Expression.Call((Expression) Expression.Property((Expression) parameterExpression, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (User.get_Id))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.ToString)), Array.Empty<Expression>()), (Expression) Expression.Call((Expression) Expression.Property((Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass320, typeof (Users.\u003C\u003Ec__DisplayClass32_0)), FieldInfo.GetFieldFromHandle(__fieldref (Users.\u003C\u003Ec__DisplayClass32_0.newData))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (WcfMembershipUser.get_ProviderUserKey))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.ToString)), Array.Empty<Expression>()))), parameterExpression));
      return !(provider.RequiresUniqueEmail & flag);
    }

    private MembershipUser SyncUserObjects(
      User originalData,
      WcfMembershipUser newData)
    {
      originalData.Comment = newData.Comment;
      originalData.Email = newData.Email;
      originalData.IsApproved = newData.IsApproved;
      return (MembershipUser) originalData;
    }

    internal virtual IEnumerable<WcfMembershipUser> ConvertUsersToWcfFormat(
      IList<User> users)
    {
      List<WcfMembershipUser> wcfFormat = new List<WcfMembershipUser>();
      foreach (User user in (IEnumerable<User>) users)
        wcfFormat.Add(this.ConvertToWcfMembershipUser(user.ProviderName, user));
      return (IEnumerable<WcfMembershipUser>) wcfFormat;
    }

    internal virtual string GetErrorMessageFromStatusCode(
      UserManager manager,
      MembershipCreateStatus status)
    {
      SecurityResources securityResources = Res.Get<SecurityResources>();
      switch (status)
      {
        case MembershipCreateStatus.InvalidUserName:
          return securityResources.InvalidUserName;
        case MembershipCreateStatus.InvalidPassword:
          return securityResources.InvalidPassword + SecurityUtility.GetPasswordRequirementsText(manager.Provider);
        case MembershipCreateStatus.InvalidQuestion:
          return securityResources.InvalidQuestion;
        case MembershipCreateStatus.InvalidAnswer:
          return securityResources.InvalidAnswer;
        case MembershipCreateStatus.InvalidEmail:
          return securityResources.InvalidEmail;
        case MembershipCreateStatus.DuplicateUserName:
          return securityResources.UserWithThisUsernameExists;
        case MembershipCreateStatus.DuplicateEmail:
          return securityResources.DuplicateEmail;
        case MembershipCreateStatus.UserRejected:
          return securityResources.UserRejected;
        case MembershipCreateStatus.InvalidProviderUserKey:
          return securityResources.InvalidProviderUserKey;
        case MembershipCreateStatus.DuplicateProviderUserKey:
          return securityResources.DuplicateProviderUserKey;
        case MembershipCreateStatus.ProviderError:
          return securityResources.ProviderError;
        default:
          return Res.Get<ErrorMessages>().WCFErrorOnSave;
      }
    }

    internal virtual string getProviderNameForRole(Role role)
    {
      Guid roleId = role.Id;
      foreach (RoleDataProvider staticProviders in (Collection<RoleDataProvider>) ManagerBase<RoleDataProvider>.StaticProvidersCollection)
      {
        if (staticProviders.GetRoles().Where<Role>((Expression<Func<Role, bool>>) (r => r.Id == roleId)).FirstOrDefault<Role>() != null)
          return staticProviders.Name;
      }
      return string.Empty;
    }

    internal virtual bool AllAdministratorsAreUnassignedFromAdminRole(UserRolesItem[] userRoles)
    {
      Dictionary<string, UserRolesItem> userRolesAsDictionary;
      if (this.ContainsAdministartor(userRoles, out userRolesAsDictionary))
        return false;
      foreach (User user in (IEnumerable<User>) RoleManager.GetManager("AppRoles").GetUsersInRole(this.GetAdminRoleId()))
      {
        if (!userRolesAsDictionary.ContainsKey(user.Id.ToString()))
          return false;
      }
      return true;
    }

    internal virtual bool ContainsAdministartor(
      UserRolesItem[] userRoles,
      out Dictionary<string, UserRolesItem> userRolesAsDictionary)
    {
      userRolesAsDictionary = new Dictionary<string, UserRolesItem>();
      foreach (UserRolesItem userRole in userRoles)
      {
        foreach (RoleProviderPair role in userRole.Roles)
        {
          Guid guid = Telerik.Sitefinity.Utilities.Utility.StringToGuid(role.RoleId);
          if (guid != Guid.Empty && SecurityManager.IsAdministrativeRole(guid))
            return true;
        }
        if (!userRolesAsDictionary.ContainsKey(userRole.UserId))
          userRolesAsDictionary.Add(userRole.UserId, userRole);
      }
      return false;
    }

    internal virtual bool DeleteUserRoleRelation(
      string userProvider,
      string username,
      Guid roleId,
      string roleProvider)
    {
      try
      {
        RoleManager manager1 = RoleManager.GetManager(roleProvider);
        UserManager manager2 = UserManager.GetManager(userProvider);
        Role role = manager1.GetRole(roleId);
        manager1.RemoveUserFromRole(manager2.GetUser(username), role);
        manager1.SaveChanges();
        return true;
      }
      catch (Exception ex)
      {
        throw new WebProtocolException(HttpStatusCode.InternalServerError, Res.Get<ErrorMessages>().RoleIsNotEmpty, ex.InnerException);
      }
    }

    internal virtual CollectionContext<RoleProviderPair> GetUserRolesInternal(
      string userProvider,
      string username,
      string roleProvider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      IQueryable<Role> source = RoleManager.GetManager(roleProvider).GetRolesForUser(UserManager.GetManager(userProvider).GetUser(username).Id);
      if (!string.IsNullOrEmpty(filter))
        source = source.Where<Role>(filter);
      if (!string.IsNullOrEmpty(sortExpression))
        source = source.OrderBy<Role>(sortExpression);
      IList<RoleProviderPair> list;
      int num;
      if (take == 0 && skip == 0)
      {
        ParameterExpression parameterExpression;
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        list = (IList<RoleProviderPair>) source.Select<Role, RoleProviderPair>(Expression.Lambda<Func<Role, RoleProviderPair>>((Expression) Expression.MemberInit(Expression.New(typeof (RoleProviderPair)), (MemberBinding) Expression.Bind((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (RoleProviderPair.set_ProviderName)), )))); //unable to render the statement
        num = list.Count;
      }
      else
      {
        num = source.Count<Role>();
        if (skip != 0)
          source = source.Skip<Role>(skip);
        if (take != 0)
          source = source.Take<Role>(take);
        ParameterExpression parameterExpression;
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        list = (IList<RoleProviderPair>) source.Select<Role, RoleProviderPair>(Expression.Lambda<Func<Role, RoleProviderPair>>((Expression) Expression.MemberInit(Expression.New(typeof (RoleProviderPair)), (MemberBinding) Expression.Bind((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (RoleProviderPair.set_ProviderName)), )))); //unable to render the statement
      }
      return new CollectionContext<RoleProviderPair>((IEnumerable<RoleProviderPair>) list)
      {
        TotalCount = num
      };
    }

    internal virtual void SaveAndReturnUserRolesResource(UserRolesItem[] userRoles)
    {
      if (this.AllAdministratorsAreUnassignedFromAdminRole(userRoles))
        throw new WebProtocolException(HttpStatusCode.InternalServerError, Res.Get<SecurityResources>().CantUnassignAllAdministrators, (Exception) null);
      foreach (UserRolesItem userRole in userRoles)
        this.SynchronizeUserRoles(Telerik.Sitefinity.Utilities.Utility.StringToGuid(userRole.UserId), userRole.Roles);
    }

    /// <summary>
    /// New method to handle assignment/unassignment of u roles to users
    /// </summary>
    /// <param name="userId">Id of the user</param>
    /// <param name="newUserRoles">Updated set of roles to synchronize</param>
    /// <param name="transactionName">The name of the transaction to use. If left empty no transaction will be used.</param>
    /// <param name="isLastAdmin">A value, indicating if the user is the last admin</param>
    internal virtual void SynchronizeUserRoles(
      User user,
      RoleProviderPair[] newUserRoles,
      string transactionName = null,
      bool? isLastAdmin = null)
    {
      Guid id = user.Id;
      int num = isLastAdmin.HasValue ? (isLastAdmin.Value ? 1 : 0) : (this.IsUserTheLastAdmin(id, transactionName) ? 1 : 0);
      string adminRole = this.GetAdminRoleId().ToString();
      if (num != 0 && !((IEnumerable<RoleProviderPair>) newUserRoles).Any<RoleProviderPair>((Func<RoleProviderPair, bool>) (newRole => newRole.RoleId == adminRole)))
        throw new WebProtocolException(HttpStatusCode.InternalServerError, Res.Get<SecurityResources>().CantUnassignAllAdministrators, (Exception) null);
      IList<Role> rolesForUser = this.FindRolesForUser(id, transactionName);
      SitefinityIdentity currentUser = ClaimsManager.GetCurrentIdentity();
      if (!currentUser.Roles.Any<RoleInfo>((Func<RoleInfo, bool>) (r => SecurityManager.IsAdministrativeRole(r.Id))) && (((IEnumerable<RoleProviderPair>) newUserRoles).Any<RoleProviderPair>((Func<RoleProviderPair, bool>) (r => !currentUser.Roles.Any<RoleInfo>((Func<RoleInfo, bool>) (p => p.Id == Telerik.Sitefinity.Utilities.Utility.StringToGuid(r.RoleId))))) || rolesForUser.Where<Role>((Func<Role, bool>) (r => !((IEnumerable<RoleProviderPair>) newUserRoles).Any<RoleProviderPair>((Func<RoleProviderPair, bool>) (p => Telerik.Sitefinity.Utilities.Utility.StringToGuid(p.RoleId) == r.Id)))).Any<Role>((Func<Role, bool>) (r => !currentUser.Roles.Any<RoleInfo>((Func<RoleInfo, bool>) (p => p.Id == r.Id))))))
        throw new WebProtocolException(HttpStatusCode.InternalServerError, Res.Get<SecurityResources>().NoEditPermissionsPreviewOnlyConfirmationNoViewOption, (Exception) null);
      foreach (RoleProviderPair newUserRole in newUserRoles)
      {
        Guid guid = Telerik.Sitefinity.Utilities.Utility.StringToGuid(newUserRole.RoleId);
        RoleManager manager = RoleManager.GetManager(newUserRole.ProviderName, transactionName);
        if (!manager.IsUserInRole(id, guid))
        {
          Role role = manager.GetRole(guid);
          if (!manager.Provider.Abilities.Keys.Contains<string>("AssingUserToRole") || manager.Provider.Abilities["AssingUserToRole"].Supported)
          {
            manager.AddUserToRole(user, role);
            if (string.IsNullOrEmpty(transactionName))
              manager.SaveChanges();
          }
        }
      }
      foreach (Role role1 in (IEnumerable<Role>) rolesForUser)
      {
        Role role = role1;
        if (((IEnumerable<RoleProviderPair>) newUserRoles).Where<RoleProviderPair>((Func<RoleProviderPair, bool>) (r => r.RoleId == role.Id.ToString())).FirstOrDefault<RoleProviderPair>() == null)
        {
          RoleManager manager = RoleManager.GetManager(this.getProviderNameForRole(role), transactionName);
          if (!manager.Provider.Abilities.Keys.Contains<string>("UnAssingUserFromRole") || manager.Provider.Abilities["UnAssingUserFromRole"].Supported)
          {
            manager.RemoveUserFromRole(user, role);
            if (string.IsNullOrEmpty(transactionName))
              manager.SaveChanges();
          }
        }
      }
    }

    internal virtual void SynchronizeUserRoles(Guid userId, RoleProviderPair[] newUserRoles) => this.SynchronizeUserRoles(this.FindUserById(userId), newUserRoles);

    internal virtual string GetAllUsersLabel() => Res.Get<Labels>().AllUsers;

    internal virtual void DisableServiceCache() => ServiceUtility.DisableCache();

    internal virtual IList<User> GetUsersFromAllMembershipProviders(
      string filter,
      string sortExpression,
      int skip,
      int take,
      out int totalCount)
    {
      return ManagerBase<MembershipDataProvider>.JoinResult<User>((GetQuery<MembershipDataProvider, User>) (p => p.GetUsers()), filter, sortExpression, skip, take, out totalCount, (ForEachItem<MembershipDataProvider, User>) null);
    }

    internal virtual User FindUserById(Guid userId) => UserManager.FindUser(userId);

    internal virtual IList<User> GetUsersFromSpecificMembershipProvider(
      string membershipProvider,
      string filter,
      string sortExpression,
      int skip,
      int take,
      out int totalCount)
    {
      UserManager manager = UserManager.GetManager(membershipProvider);
      return manager.GetList<User>(manager.GetUsers(), filter, sortExpression, skip, take, out totalCount, (ForEachItem<MembershipDataProvider, User>) null);
    }

    internal virtual IList<User> GetUsersForSpecificRoleFromAllMembershipProviders(
      string roleProvider,
      Guid roleId,
      string filter,
      string sortExpression,
      int skip,
      int take,
      out int totalCount)
    {
      return RoleManager.GetManager(roleProvider).GetUsersInRole(roleId, filter, sortExpression, skip, take, out totalCount);
    }

    internal virtual int GetNumOfUsersForRole(
      RoleManager roleMan,
      Guid guidRoleId,
      string membreshpProvider,
      params string[] userFitlers)
    {
      IQueryable<User> source = roleMan.GetUsersInRole(guidRoleId).Where<User>((Func<User, bool>) (u => u.ProviderName == membreshpProvider)).AsQueryable<User>();
      foreach (string predicate in ((IEnumerable<string>) userFitlers).Where<string>((Func<string, bool>) (u => !string.IsNullOrEmpty(u))))
        source = source.Where<User>(predicate);
      return source.Count<User>();
    }

    internal virtual IList<Role> FindRolesForUser(Guid userId, string transactionName = null) => RoleManager.FindRolesForUser(userId, transactionName);

    internal virtual IEnumerable<User> GetUserLinksForSpecificRoleFromSpecificMembershipProvider(
      string roleProvider,
      string membershipProvider,
      Guid roleId)
    {
      return RoleManager.GetManager(roleProvider).GetUsersInRole(roleId).Where<User>((Func<User, bool>) (u => u.ProviderName == membershipProvider));
    }

    internal virtual IList<User> GetUsersBySpecificUserIdsFromSpecificMembershipProvider(
      Guid[] userIds,
      string membershipProvider,
      string filter,
      string sortExpression,
      int skip,
      int take,
      out int totalCount)
    {
      UserManager manager = UserManager.GetManager(membershipProvider);
      IQueryable<User> query = manager.GetUsers().Where<User>((Expression<Func<User, bool>>) (u => userIds.Contains<Guid>(u.Id)));
      return manager.GetList<User>(query, filter, sortExpression, skip, take, out totalCount, (ForEachItem<MembershipDataProvider, User>) null);
    }

    internal virtual Guid GetAdminRoleId() => SecurityManager.AdminRole.Id;

    /// <summary>Gets the logged in users count.</summary>
    /// <param name="roleProvider">The role provider.</param>
    /// <returns></returns>
    public int GetLoggedInUsersCount(string provider)
    {
      int backendUsersCount = SecurityManager.GetLoggedInBackendUsersCount(provider);
      ServiceUtility.DisableCache();
      return backendUsersCount;
    }

    /// <summary>
    /// Sets authentication cookies to the current request if the provided credentials are valid.
    /// </summary>
    /// <param name="credentials">The credentials.</param>
    /// <returns></returns>
    public UserLoggingReason AuthenticateUser(Credentials credentials)
    {
      if (credentials != null)
        return SecurityManager.AuthenticateUser(credentials);
      UserLoggingReason loginResult = UserLoggingReason.Unknown;
      SecurityManager.RaiseLoginEvent(loginResult: loginResult);
      return loginResult;
    }

    /// <summary>Logs out the user making the current request.</summary>
    /// <returns></returns>
    public bool Logout()
    {
      SecurityManager.Logout();
      return true;
    }

    /// <summary>
    /// Logs out the user with the specified credentials.
    /// This request doesn't have to be authenticated but the credentials must be valid.
    /// </summary>
    /// <param name="credentials"></param>
    /// <returns></returns>
    public bool LogoutWithCredentials(Credentials credentials)
    {
      SecurityManager.Logout(credentials);
      return true;
    }

    /// <summary>Logs out the specified user.</summary>
    /// <param name="providerName">The name of the membership provider.</param>
    /// <param name="userName">The username of the user to logout.</param>
    /// <returns></returns>
    public bool LogoutUser(string providerName, string userName)
    {
      SecurityManager.Logout(providerName, userName);
      return true;
    }

    /// <summary>Forces the logout.</summary>
    /// <param name="userId">The user pageId.</param>
    /// <param name="provider">The provider.</param>
    /// <returns>Need a redirect to loginPage</returns>
    public bool ForceLogout(string userId, string provider)
    {
      Guid userId1 = new Guid(userId);
      SecurityManager.Logout(provider, userId1);
      return userId1 == ClaimsManager.GetCurrentUserId();
    }
  }
}
