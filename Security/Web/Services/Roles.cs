// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.Services.Roles
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Data;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Security.Web.Services
{
  /// <summary>Implementation of WCF service for roles management.</summary>
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  public class Roles : IRoles
  {
    /// <summary>
    /// Recieve a list of user ids and send back the ids of those belonging to the role
    /// </summary>
    /// <param name="roleIdString">ID of the of the role to check against</param>
    /// <param name="provider">Roles provider to use</param>
    /// <param name="userIdsToPickFrom">List of user IDs to pick from</param>
    /// <returns>
    /// Total number of users in the role and the IDs of the users that are actually in the role
    /// </returns>
    public CollectionContext<Guid> PickUsersInRole(
      string roleIdString,
      string provider,
      Guid[] userIdsToPickFrom)
    {
      IEnumerable<Guid> guids = RoleManager.GetManager(provider).GetUsersInRole(Telerik.Sitefinity.Utilities.Utility.StringToGuid(roleIdString)).Select<User, Guid>((Func<User, Guid>) (u => u.Id));
      int num = guids.Count<Guid>();
      return new CollectionContext<Guid>(guids.Intersect<Guid>((IEnumerable<Guid>) userIdsToPickFrom))
      {
        TotalCount = num
      };
    }

    /// <summary>
    /// Recieve a list of user ids and send back the ids of those belonging to the role
    /// </summary>
    /// <param name="roleIdString">ID of the of the role to check against</param>
    /// <param name="provider">Roles provider to use</param>
    /// <param name="userIdsToPickFrom">List of user IDs to pick from</param>
    /// <returns>
    /// Total number of users in the role and the IDs of the users that are actually in the role
    /// </returns>
    public CollectionContext<Guid> PickUsersInRoleInXml(
      string roleIdString,
      string provider,
      Guid[] userIdsToPickFrom)
    {
      return this.PickUsersInRole(roleIdString, provider, userIdsToPickFrom);
    }

    /// <summary>Gets the collection of roles in JSON format.</summary>
    /// <param name="provider">The name of the role provider from which the roles should be retrived.</param>
    /// <param name="sortExpression">The sort expression used to order the retrieved roles.</param>
    /// <param name="skip">The number of roles to skip before populating the collection (used primarily for paging).</param>
    /// <param name="take">The maximum number of roles to take in the collection (used primarily for paging).</param>
    /// <param name="filter">The filter expression in dynamic LINQ format used to filter the retrieved roles.</param>
    /// <returns>
    /// Returns CollectionContext object with role entry items and other information about the retrieved collection.
    /// </returns>
    public CollectionContext<WcfRole> GetRoles(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      this.DisableServiceCache();
      return this.GetRolesInternal(provider, sortExpression, skip, take, filter, (string) null);
    }

    /// <summary>Gets the collection of roles in XML format.</summary>
    /// <param name="provider">The name of the role provider from which the roles should be retrived.</param>
    /// <param name="sortExpression">The sort expression used to order the retrieved roles.</param>
    /// <param name="skip">The number of roles to skip before populating the collection (used primarily for paging).</param>
    /// <param name="take">The maximum number of roles to take in the collection (used primarily for paging).</param>
    /// <param name="filter">The filter expression in dynamic LINQ format used to filter the retrieved roles.</param>
    /// <returns>
    /// 	Returns CollectionContext object with role entry items and other information about the retrieved collection.
    /// </returns>
    public CollectionContext<WcfRole> GetRolesInXml(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      return this.GetRolesInternal(provider, sortExpression, skip, take, filter, (string) null);
    }

    /// <summary>
    /// Gets the collection of roles in JSON format, while users number per role is filtered by a specific membership provider
    /// </summary>
    /// <param name="provider">The name of the role provider from which the roles should be retrived.</param>
    /// <param name="sortExpression">The sortExpression expression used to order the retrieved roles.</param>
    /// <param name="skip">The number of roles to skip before populating the collection (used primarily for paging).</param>
    /// <param name="take">The maximum number of roles to take in the collection (used primarily for paging).</param>
    /// <param name="filter">The filter expression in dynamic LINQ format used to filter the retrieved roles.</param>
    /// <param name="membershipProvider">The name of the membership provider from which the users should be counted.</param>
    /// <returns>Returns CollectionContext object with role entry items and other information about the retrieved collection.</returns>
    public CollectionContext<WcfRole> GetRolesWithUserCountPerMembershipProvider(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string membershipProvider)
    {
      return this.GetRolesInternal(provider, sortExpression, skip, take, filter, membershipProvider);
    }

    /// <summary>
    /// Gets the role (can be used to check existence of role).
    /// </summary>
    /// <param name="roleId">Id of the role to be retrieved.</param>
    /// <param name="provider">The name of the role provider from which the roles should be retrived.</param>
    /// <returns>
    /// If the role exists the role name will be returned, otherwise null string.
    /// </returns>
    public WcfRole GetRole(string roleId, string provider) => this.GetRoleInternal(roleId, provider);

    /// <summary>
    /// Gets the role in XML (can be used to check existence of role).
    /// </summary>
    /// <param name="roleId">Id of the role to be retrieved.</param>
    /// <param name="provider">The name of the role provider from which the roles should be retrived.</param>
    /// <returns>
    /// If the role exists the role name will be returned, otherwise null string.
    /// </returns>
    public WcfRole GetRoleInXml(string roleId, string provider) => this.GetRoleInternal(roleId, provider);

    /// <summary>Saves the role.</summary>
    /// <param name="role">The role object to be saved.</param>
    /// <param name="roleId">Id of the role to be saved.</param>
    /// <param name="provider">The name of the role provider from which the roles should be retrived.</param>
    /// <returns>Newly created WcfRole object.</returns>
    public WcfRole SaveRole(WcfRole role, string roleId, string provider) => this.SaveRoleInternal(role, roleId, provider);

    /// <summary>Saves the role in XML.</summary>
    /// <param name="role">The role object to be saved.</param>
    /// <param name="roleId">Id of the role to be saved</param>
    /// <param name="provider">The name of the role provider from which the roles should be retrived.</param>
    /// <returns>Newly created WcfRole object.</returns>
    public WcfRole SaveRoleInXml(WcfRole role, string roleId, string provider) => this.SaveRoleInternal(role, roleId, provider);

    /// <summary>
    /// Deletes the role and returns true if the role has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="roleId">Id of the role to be deleted.</param>
    /// <param name="provider">The name of the role provider from which the roles should be retrived.</param>
    /// <returns></returns>
    public bool DeleteRole(string roleId, string provider)
    {
      this.DisableServiceCache();
      return this.DeleteRoleInternal(roleId, provider);
    }

    /// <summary>
    /// Deletes the role and returns true if the role has been deleted; otherwise false.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="roleId"></param>
    /// <param name="provider">The name of the role provider from which the roles should be retrived.</param>
    /// <returns></returns>
    public bool DeleteRoleInXml(string roleId, string provider) => this.DeleteRoleInternal(roleId, provider);

    /// <summary>Gets the role providers.</summary>
    /// <param name="commaSeperatedAbilities">Optional list of required provider abilities to filter, comma seperated</param>
    /// <param name="addAppRoles">Indicates whether to include Sitefinity's application specific roles</param>
    /// <returns>A collection of UserProviderItem items</returns>
    public CollectionContext<RoleProviderItem> GetRoleProviders(
      string commaSeperatedAbilities,
      bool addAppRoles = false)
    {
      List<string> stringList;
      if (string.IsNullOrEmpty(commaSeperatedAbilities))
        stringList = new List<string>();
      else
        stringList = ((IEnumerable<string>) commaSeperatedAbilities.Split(',')).ToList<string>();
      List<string> source = stringList;
      List<RoleProviderItem> items = new List<RoleProviderItem>();
      items.Add(new RoleProviderItem()
      {
        RoleProviderTitle = this.GetAllRolesLabel(),
        RoleProviderName = string.Empty,
        NumOfRoles = -1L
      });
      if (ManagerBase<RoleDataProvider>.StaticProvidersCollection == null)
      {
        ProvidersCollection<RoleDataProvider> staticProviders1 = RoleManager.GetManager().StaticProviders;
      }
      foreach (RoleDataProvider staticProviders2 in (Collection<RoleDataProvider>) ManagerBase<RoleDataProvider>.StaticProvidersCollection)
      {
        bool flag = true;
        if (staticProviders2.Name != "AppRoles" | addAppRoles)
        {
          if (source.Count<string>() > 0)
          {
            foreach (string key in source)
            {
              if (staticProviders2.Abilities.Keys.Contains<string>(key) && !staticProviders2.Abilities[key].Supported)
                flag = false;
            }
          }
          if (flag)
            items.Add(new RoleProviderItem()
            {
              NumOfRoles = (long) staticProviders2.GetRoles().Count<Role>(),
              RoleProviderName = staticProviders2.Name,
              RoleProviderTitle = staticProviders2.Title
            });
        }
      }
      this.DisableServiceCache();
      return new CollectionContext<RoleProviderItem>((IEnumerable<RoleProviderItem>) items);
    }

    /// <summary>Gets the role/users relations in JSON format.</summary>
    /// <param name="roleId">Id of the role for which the role/users relations should be retrieved.</param>
    /// <param name="provider">The name of the role provider from which the role/users relation should be retrieved.</param>
    /// <param name="sortExpression">The sortExpression expression used to order the retrieved role/users relations.</param>
    /// <param name="skip">The number of role/users relations to skip before populating the collection (used primarily for paging).</param>
    /// <param name="take">The maximum number of role/users relations to take in the collection (used primarily for paging).</param>
    /// <param name="filter">The filter expression in dynamic LINQ format used to filter the retrieved role/users relations.</param>
    /// <returns>
    /// 	Returns CollectionContext object with role/users relations  and other information about the retrieved collection.
    /// </returns>
    public CollectionContext<UserProviderPair> GetRoleUsers(
      string roleId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      return this.GetRoleUsersInternal(Telerik.Sitefinity.Utilities.Utility.StringToGuid(roleId), provider, sortExpression, skip, take, filter);
    }

    /// <summary>
    /// Get all users in a role and return collection context of wcf membership users
    /// </summary>
    /// <param name="roleId">ID of the role whose users to get</param>
    /// <param name="roleProvider">Role provider to use</param>
    /// <param name="sortExpression">Sort expression</param>
    /// <param name="skip">Used for paging. How many items to skip.</param>
    /// <param name="take">Used for paging. How many items to take.</param>
    /// <param name="filter">Used for searching. Basically, something like a 'WHERE' clause of a 'SELECT' statement in T-SQL</param>
    /// <param name="forAllUserProviders">Whether or not to use all user providers</param>
    /// <param name="userProvider">User provider name</param>
    /// <param name="localChange">Local (not persisted) change that was made in JavaScript</param>
    /// <returns>
    /// Collection context of wcf membership users belonging to a particualr role, with paging.
    /// </returns>
    public CollectionContext<WcfMembershipUser> GetWcfUsersInRole(
      UserProviderPair[] localChange,
      string roleId,
      string roleProvider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string userProvider,
      bool forAllUserProviders)
    {
      RoleManager manager = RoleManager.GetManager(roleProvider);
      Guid guid1 = Telerik.Sitefinity.Utilities.Utility.StringToGuid(roleId);
      Guid[] usersToAdd = (Guid[]) null;
      List<User> users2;
      if (forAllUserProviders)
      {
        users2 = manager.GetUsersInRole(guid1).ToList<User>();
      }
      else
      {
        string userApplicationName = UserManager.GetManager(userProvider).Provider.ApplicationName;
        users2 = manager.GetUsersInRole(guid1).Where<User>((Func<User, bool>) (u => u.ApplicationName == userApplicationName)).ToList<User>();
      }
      if (localChange.Length != 0)
      {
        foreach (Guid guid2 in ((IEnumerable<UserProviderPair>) localChange).Where<UserProviderPair>((Func<UserProviderPair, bool>) (c =>
        {
          if (!c.Remove || !users2.Any<User>((Func<User, bool>) (u => u.Id == c.UserId)))
            return false;
          return forAllUserProviders || c.ProviderName == userProvider;
        })).Select<UserProviderPair, Guid>((Func<UserProviderPair, Guid>) (change => change.UserId)))
        {
          Guid item = guid2;
          User user = users2.Find((Predicate<User>) (u => u.Id == item));
          users2.Remove(user);
        }
        usersToAdd = ((IEnumerable<UserProviderPair>) localChange).Where<UserProviderPair>((Func<UserProviderPair, bool>) (c =>
        {
          if (c.Remove || users2.Any<User>((Func<User, bool>) (u => u.Id == c.UserId)))
            return false;
          return forAllUserProviders || c.ProviderName == userProvider;
        })).Select<UserProviderPair, Guid>((Func<UserProviderPair, Guid>) (change => change.UserId)).ToArray<Guid>();
      }
      if (usersToAdd == null && users2.Count == 0)
      {
        this.DisableServiceCache();
        return CollectionContext<WcfMembershipUser>.Empty;
      }
      int totalCount;
      if (usersToAdd != null)
      {
        IList<User> collection;
        if (forAllUserProviders)
        {
          // ISSUE: reference to a compiler-generated field
          collection = ManagerBase<MembershipDataProvider>.JoinResult<User>((GetQuery<MembershipDataProvider, User>) (p => p.GetUsers().Where<User>((Expression<Func<User, bool>>) (u => this.usersToAdd.Contains<Guid>(u.Id)))), filter, sortExpression, 0, 0, out totalCount, (ForEachItem<MembershipDataProvider, User>) null);
        }
        else
        {
          IQueryable<User> query = UserManager.GetManager(userProvider).GetUsers().Where<User>((Expression<Func<User, bool>>) (u => usersToAdd.Contains<Guid>(u.Id)));
          int? nullable = new int?(0);
          string filterExpression = filter;
          string orderExpression = sortExpression;
          int? skip1 = new int?(0);
          int? take1 = new int?(0);
          ref int? local = ref nullable;
          collection = (IList<User>) DataProviderBase.SetExpressions<User>(query, filterExpression, orderExpression, skip1, take1, ref local).ToList<User>();
          int num = nullable.Value;
        }
        users2.AddRange((IEnumerable<User>) collection);
      }
      if (filter != null && filter != "")
        users2 = users2.AsQueryable<User>().Where<User>(filter).ToList<User>();
      totalCount = users2.Count;
      if (skip != 0)
        users2 = users2.Skip<User>(skip).ToList<User>();
      if (take != 0)
        users2 = users2.Take<User>(take).ToList<User>();
      IEnumerable<WcfMembershipUser> wcfFormat = this.ConvertUsersToWcfFormat((IList<User>) users2);
      this.DisableServiceCache();
      return new CollectionContext<WcfMembershipUser>(wcfFormat)
      {
        TotalCount = totalCount
      };
    }

    public int GetNumOfUsersInRoleWithLocalChanges(
      UserProviderPair[] localChange,
      string roleId,
      string roleProvider,
      string filter,
      string userProvider,
      bool forAllUserProviders)
    {
      RoleManager manager = RoleManager.GetManager(roleProvider);
      Guid id = Telerik.Sitefinity.Utilities.Utility.StringToGuid(roleId);
      List<Guid> ids;
      if (forAllUserProviders)
        ids = manager.Provider.GetUserLinks().Where<UserLink>((Expression<Func<UserLink, bool>>) (l => l.Role.Id == id)).Select<UserLink, Guid>((Expression<Func<UserLink, Guid>>) (l => l.UserId)).ToList<Guid>();
      else
        ids = manager.Provider.GetUserLinks().Where<UserLink>((Expression<Func<UserLink, bool>>) (l => l.Role.Id == id && l.MembershipManagerInfo.ProviderName == userProvider)).Select<UserLink, Guid>((Expression<Func<UserLink, Guid>>) (l => l.UserId)).ToList<Guid>();
      if (localChange.Length != 0)
      {
        foreach (Guid guid in ((IEnumerable<UserProviderPair>) localChange).Where<UserProviderPair>((Func<UserProviderPair, bool>) (c =>
        {
          if (!c.Remove || !ids.Contains(c.UserId))
            return false;
          return forAllUserProviders || c.ProviderName == userProvider;
        })).Select<UserProviderPair, Guid>((Func<UserProviderPair, Guid>) (change => change.UserId)))
          ids.Remove(guid);
        IEnumerable<Guid> collection = ((IEnumerable<UserProviderPair>) localChange).Where<UserProviderPair>((Func<UserProviderPair, bool>) (c =>
        {
          if (c.Remove || ids.Contains(c.UserId))
            return false;
          return forAllUserProviders || c.ProviderName == userProvider;
        })).Select<UserProviderPair, Guid>((Func<UserProviderPair, Guid>) (change => change.UserId));
        ids.AddRange(collection);
      }
      this.DisableServiceCache();
      return ids.Count;
    }

    public int GetNumOfUsersInRoleWithLocalChangesInXml(
      UserProviderPair[] localChange,
      string roleId,
      string roleProvider,
      string filter,
      string userProvider,
      bool forAllUserProviders)
    {
      return this.GetNumOfUsersInRoleWithLocalChanges(localChange, roleId, roleProvider, filter, userProvider, forAllUserProviders);
    }

    /// <summary>Gets the role/users relations in JSON format.</summary>
    /// <param name="roleId">Id of the role for which the role/users relations should be retrieved.</param>
    /// <param name="provider">The name of the role provider from which the role/users relation should be retrieved.</param>
    /// <returns>Count of users in the specified role</returns>
    public int GetCountOfUsersInRole(string roleId, string provider)
    {
      RoleManager manager = RoleManager.GetManager(provider);
      int countOfUsersInRole = 0;
      Guid guid = Telerik.Sitefinity.Utilities.Utility.StringToGuid(roleId);
      ref int local = ref countOfUsersInRole;
      manager.GetUsersInRole(guid, (string) null, (string) null, 0, 0, out local);
      this.DisableServiceCache();
      return countOfUsersInRole;
    }

    /// <summary>Gets the role/users relations in JSON format.</summary>
    /// <param name="roleId">Id of the role for which the role/users relations should be retrieved.</param>
    /// <param name="provider">The name of the role provider from which the role/users relation should be retrieved.</param>
    /// <returns>Count of users in the specified role</returns>
    public int GetCountOfUsersInRoleInXml(string roleId, string provider) => this.GetCountOfUsersInRole(roleId, provider);

    /// <summary>
    /// Get all users in a role and return collection context of wcf membership users
    /// </summary>
    /// <param name="roleId">ID of the role whose users to get</param>
    /// <param name="provider">Role provider to use</param>
    /// <param name="sortExpression">Sort expression</param>
    /// <param name="skip">Used for paging. How many items to skip.</param>
    /// <param name="take">Used for paging. How many items to take.</param>
    /// <param name="filter">Used for searching. Basically, something like a 'WHERE' clause of a 'SELECT' statement in T-SQL</param>
    /// <param name="forAllUserProviders">Whether or not to use all user providers</param>
    /// <param name="userProvider">User provider name</param>
    /// <param name="localChange">Local (not persisted) change that was made in JavaScript</param>
    /// <returns>
    /// Collection context of wcf membership users belonging to a particualr role, with paging.
    /// </returns>
    public CollectionContext<WcfMembershipUser> GetWcfUsersInRoleInXml(
      UserProviderPair[] localChange,
      string roleId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string userProvider,
      bool forAllUserProviders)
    {
      return this.GetWcfUsersInRole(localChange, roleId, provider, sortExpression, skip, take, filter, userProvider, forAllUserProviders);
    }

    /// <summary>Gets the role/users relations in XML format.</summary>
    /// <param name="roleId">Id of the role for which the role/users relations should be retrieved.</param>
    /// <param name="provider">The name of the role provider from which the role/users relation should be retrieved.</param>
    /// <param name="sortExpression">The sort expression used to order the retrieved role/users relations.</param>
    /// <param name="skip">The number of role/users relations to skip before populating the collection (used primarily for paging).</param>
    /// <param name="take">The maximum number of role/users relations to take in the collection (used primarily for paging).</param>
    /// <param name="filter">The filter expression in dynamic LINQ format used to filter the retrieved role/users relations.</param>
    /// <returns>
    /// 	Returns CollectionContext object with role/users relations  and other information about the retrieved collection.
    /// </returns>
    public CollectionContext<UserProviderPair> GetRoleUsersInXml(
      string roleId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      return this.GetRoleUsersInternal(Telerik.Sitefinity.Utilities.Utility.StringToGuid(roleId), provider, sortExpression, skip, take, filter);
    }

    /// <summary>Adds relation between role and user.</summary>
    /// <param name="users">Array of <see cref="T:Telerik.Sitefinity.Security.Web.Services.UserProviderPair" /> objects.</param>
    /// <param name="roleId">Id of the role.</param>
    /// <param name="provider">The name of role provider.</param>
    /// <param name="sortExpression"></param>
    /// <param name="skip"></param>
    /// <param name="take"></param>
    /// <param name="filter"></param>
    /// <returns></returns>
    public string SaveRoleUser(
      UserProviderPair[] users,
      string roleId,
      string provider,
      string sortExpression,
      string skip,
      string take,
      string filter)
    {
      return this.SaveAndReturnRoleUsersResource(users, Telerik.Sitefinity.Utilities.Utility.StringToGuid(roleId), provider);
    }

    /// <summary>Adds relation between role and user.</summary>
    /// <param name="users">Array of <see cref="T:Telerik.Sitefinity.Security.Web.Services.UserProviderPair" /> objects.</param>
    /// <param name="roleId">Id of the role.</param>
    /// <param name="provider">The name of role provider.</param>
    /// <returns></returns>
    public string SaveRoleUserInXml(
      UserProviderPair[] users,
      string roleId,
      string provider,
      string sortExpression,
      string skip,
      string take,
      string filter)
    {
      return this.SaveAndReturnRoleUsersResource(users, Telerik.Sitefinity.Utilities.Utility.StringToGuid(roleId), provider);
    }

    /// <summary>
    /// Deletes a role user relation and returns true if the role user has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="roleId">Id of the role.</param>
    /// <param name="userId">Id of the user.</param>
    /// <param name="provider">The name of role provider.</param>
    /// <returns></returns>
    public bool DeleteRoleUser(string roleId, string userId, string provider) => this.DeleteRoleUserRelation(Telerik.Sitefinity.Utilities.Utility.StringToGuid(roleId), Telerik.Sitefinity.Utilities.Utility.StringToGuid(userId), provider);

    /// <summary>
    /// Deletes a role user relation and returns true if the role user has been deleted; otherwise false.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="roleId">Id of the role.</param>
    /// <param name="userId">Id of the user.</param>
    /// <param name="provider">The name of role provider.</param>
    /// <returns></returns>
    public bool DeleteRoleUserInXml(string roleId, string userId, string provider) => this.DeleteRoleUserRelation(Telerik.Sitefinity.Utilities.Utility.StringToGuid(roleId), Telerik.Sitefinity.Utilities.Utility.StringToGuid(userId), provider);

    internal virtual CollectionContext<WcfRole> GetRolesInternal(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string membershipProviderName)
    {
      if (string.IsNullOrEmpty(sortExpression))
        sortExpression = "Name ASC";
      int totalCount;
      CollectionContext<WcfRole> rolesInternal = new CollectionContext<WcfRole>(!string.IsNullOrEmpty(provider) ? (IEnumerable<WcfRole>) this.GetWcfRolesFromSpecificProvider(provider, filter, sortExpression, skip, take, out totalCount, membershipProviderName) : (IEnumerable<WcfRole>) this.GetWcfRolesFromAllProviders(filter, sortExpression, skip, take, out totalCount, membershipProviderName));
      rolesInternal.TotalCount = totalCount;
      this.DisableServiceCache();
      return rolesInternal;
    }

    internal virtual WcfRole SaveRoleInternal(WcfRole role, string roleId, string provider)
    {
      RoleManager manager = RoleManager.GetManager(provider);
      if (manager.RoleExists(role.Name))
        throw new WebProtocolException(HttpStatusCode.InternalServerError, string.Format(Res.Get<SecurityResources>().RoleExistsOnProvider, (object) role.Name, (object) provider), (Exception) null);
      if (string.Equals("Administrators", role.Name, StringComparison.OrdinalIgnoreCase))
        throw new WebProtocolException(HttpStatusCode.InternalServerError, Res.Get<SecurityResources>().ThereCanBeOnlyOneAdministratorsRole, (Exception) null);
      if (string.Equals("Administrators", role.Name, StringComparison.OrdinalIgnoreCase))
        throw new WebProtocolException(HttpStatusCode.InternalServerError, Res.Get<SecurityResources>().ThereCanBeOnlyOneAdministratorsRole, (Exception) null);
      if (string.Equals("BackendUsers", role.Name, StringComparison.OrdinalIgnoreCase))
        throw new WebProtocolException(HttpStatusCode.InternalServerError, Res.Get<SecurityResources>().RoleNameIsReservedByTheSystem.Arrange((object) "BackendUsers"), (Exception) null);
      try
      {
        if (Telerik.Sitefinity.Utilities.Utility.StringToGuid(roleId) == Guid.Empty)
        {
          manager.CreateRole(role.Name);
        }
        else
        {
          Guid guid = Telerik.Sitefinity.Utilities.Utility.StringToGuid(roleId);
          manager.CreateRole(guid, role.Name);
        }
        manager.SaveChanges();
      }
      catch (ArgumentException ex)
      {
        throw new WebProtocolException(HttpStatusCode.InternalServerError, ex.Message, ex.InnerException);
      }
      catch (Exception ex)
      {
        throw new WebProtocolException(HttpStatusCode.InternalServerError, Res.Get<ErrorMessages>().WCFErrorOnSave, ex.InnerException);
      }
      return role;
    }

    internal virtual WcfRole GetRoleInternal(string roleId, string provider)
    {
      Guid guid = Telerik.Sitefinity.Utilities.Utility.StringToGuid(roleId);
      Role role = RoleManager.GetManager(provider).GetRole(guid);
      return role == null ? (WcfRole) null : new WcfRole(role.Name, role.Id, role.Users.Count, provider);
    }

    internal virtual bool DeleteRoleInternal(string roleId, string provider)
    {
      RoleManager manager = RoleManager.GetManager(provider);
      Guid guid = Telerik.Sitefinity.Utilities.Utility.StringToGuid(roleId);
      Role role = manager.GetRole(guid);
      if (!(guid == this.GetAdminRoleId()))
      {
        if (!(guid == this.GetBackendUsersRoleId()))
        {
          try
          {
            manager.Delete(role);
            manager.SaveChanges();
            return true;
          }
          catch (Exception ex)
          {
            throw new WebProtocolException(HttpStatusCode.InternalServerError, Res.Get<ErrorMessages>().RoleIsNotEmpty, ex.InnerException);
          }
        }
      }
      throw new WebProtocolException(HttpStatusCode.InternalServerError, string.Format(Res.Get<SecurityResources>().AdministratorsRoleCannotBeDeleted, (object) role.Name), (Exception) null);
    }

    internal virtual string SaveAndReturnRoleUsersResource(
      UserProviderPair[] users,
      Guid roleId,
      string provider)
    {
      UserManager currentInstance = (UserManager) null;
      RoleManager manager = RoleManager.GetManager(provider);
      SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
      if (!currentIdentity.Roles.Any<RoleInfo>((Func<RoleInfo, bool>) (r => SecurityManager.IsAdministrativeRole(r.Id))) && !currentIdentity.Roles.Any<RoleInfo>((Func<RoleInfo, bool>) (p => p.Id == roleId)))
        throw new WebProtocolException(HttpStatusCode.InternalServerError, Res.Get<SecurityResources>().NoEditPermissionsPreviewOnlyConfirmationNoViewOption, (Exception) null);
      Role role = manager.GetRole(roleId);
      foreach (UserProviderPair user1 in users)
      {
        try
        {
          bool flag = manager.IsUserInRole(user1.UserId, roleId);
          if (user1.Remove)
          {
            if (flag)
            {
              currentInstance = this.GetUserManager(currentInstance, user1.ProviderName);
              User user2 = currentInstance.GetUser(user1.UserId);
              manager.RemoveUserFromRole(user2, role);
            }
          }
          else if (!flag)
          {
            currentInstance = this.GetUserManager(currentInstance, user1.ProviderName);
            User user3 = currentInstance.GetUser(user1.UserId);
            manager.AddUserToRole(user3, role);
          }
        }
        catch (UnauthorizedAccessException ex)
        {
          throw new WebProtocolException(HttpStatusCode.InternalServerError, Res.Get<ErrorMessages>().YouAreNotAuthorizedTo.Arrange((object) Res.Get<SecurityResources>().CreateOrModifyRoles), ex.InnerException);
        }
        catch (NotImplementedException ex)
        {
          throw new WebProtocolException(HttpStatusCode.InternalServerError, ex.Message, ex.InnerException);
        }
        catch (Exception ex)
        {
          throw new WebProtocolException(HttpStatusCode.InternalServerError, Res.Get<ErrorMessages>().WCFErrorOnSave, ex.InnerException);
        }
      }
      manager.SaveChanges();
      return role.Name;
    }

    internal virtual bool DeleteRoleUserRelation(Guid roleId, Guid userId, string provider)
    {
      try
      {
        RoleManager manager = RoleManager.GetManager(provider);
        Role role = manager.GetRole(roleId);
        manager.RemoveUserFromRole(userId, role);
        manager.SaveChanges();
        return true;
      }
      catch (Exception ex)
      {
        throw new WebProtocolException(HttpStatusCode.InternalServerError, Res.Get<ErrorMessages>().RoleIsNotEmpty, ex.InnerException);
      }
    }

    internal virtual CollectionContext<UserProviderPair> GetRoleUsersInternal(
      Guid roleId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      RoleManager manager = RoleManager.GetManager(provider);
      int num = 0;
      Guid roleId1 = roleId;
      string filterExpression = filter;
      string sortExpression1 = sortExpression;
      int skip1 = skip;
      int take1 = take;
      ref int local = ref num;
      IList<User> usersInRole = manager.GetUsersInRole(roleId1, filterExpression, sortExpression1, skip1, take1, out local);
      List<UserProviderPair> items = new List<UserProviderPair>(usersInRole.Count);
      foreach (User user in (IEnumerable<User>) usersInRole)
        items.Add(new UserProviderPair()
        {
          UserId = user.Id,
          ProviderName = provider
        });
      CollectionContext<UserProviderPair> roleUsersInternal = new CollectionContext<UserProviderPair>((IEnumerable<UserProviderPair>) items);
      roleUsersInternal.TotalCount = num;
      this.DisableServiceCache();
      return roleUsersInternal;
    }

    internal virtual void DisableServiceCache() => ServiceUtility.DisableCache();

    internal virtual string GetAllRolesLabel() => Res.Get<Labels>().AllRoles;

    internal virtual List<WcfRole> GetWcfRolesFromAllProviders(
      string filter,
      string sortExpression,
      int skip,
      int take,
      out int totalCount,
      string membershipProviderName)
    {
      bool hasAdditionalRoleProviders = ManagerBase<RoleDataProvider>.StaticProvidersCollection.Count > 2;
      List<WcfRole> wcfRoles = new List<WcfRole>();
      ManagerBase<RoleDataProvider>.JoinResult<Role>((GetQuery<RoleDataProvider, Role>) (p => p.GetRoles()), filter, sortExpression, skip, take, out totalCount, (ForEachItem<RoleDataProvider, Role>) ((p, i) =>
      {
        Guid id = i.Id;
        wcfRoles.Add(new WcfRole()
        {
          Id = id,
          Name = i.Name,
          UsersInRole = 0,
          ProviderName = p.Name,
          ProviderTitle = hasAdditionalRoleProviders ? string.Format(" ({0})", (object) p.Title) : string.Empty
        });
      }));
      return wcfRoles;
    }

    internal virtual List<WcfRole> GetWcfRolesFromSpecificProvider(
      string roleProviderName,
      string filter,
      string sortExpression,
      int skip,
      int take,
      out int totalCount,
      string membershipProviderName)
    {
      List<WcfRole> wcfRoles = new List<WcfRole>();
      RoleManager roleManager = new RoleManager(roleProviderName);
      roleManager.GetList<Role>(roleManager.GetRoles(), filter, sortExpression, skip, take, out totalCount, (ForEachItem<RoleDataProvider, Role>) ((p, i) =>
      {
        Guid id = i.Id;
        wcfRoles.Add(new WcfRole()
        {
          Id = id,
          Name = i.Name,
          UsersInRole = 0,
          ProviderName = roleProviderName,
          ProviderTitle = p.Title
        });
      }));
      return wcfRoles;
    }

    internal virtual UserManager GetUserManager(
      UserManager currentInstance,
      string membershipProviderName)
    {
      if (currentInstance == null || currentInstance.Provider.Name != membershipProviderName)
        currentInstance = UserManager.GetManager(membershipProviderName);
      return currentInstance;
    }

    protected internal virtual Guid GetAdminRoleId() => SecurityManager.AdminRole.Id;

    protected internal virtual Guid GetBackendUsersRoleId() => SecurityManager.BackEndUsersRole.Id;

    internal virtual IEnumerable<WcfMembershipUser> ConvertUsersToWcfFormat(
      IList<User> users)
    {
      List<WcfMembershipUser> wcfFormat = new List<WcfMembershipUser>();
      foreach (User user in (IEnumerable<User>) users)
        wcfFormat.Add(this.ConvertToWcfMembershipUser(user.ProviderName, user));
      return (IEnumerable<WcfMembershipUser>) wcfFormat;
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

    /// <summary>Gets the total users count.</summary>
    /// <param name="membershipProviderName">The membership provider name</param>
    /// <param name="role">Role object</param>
    /// <param name="roleDataProvider">RoleDataProvider object</param>
    /// <returns>Users count in role </returns>
    internal virtual int GetUsersCountInRole(
      string membershipProviderName,
      Role role,
      RoleDataProvider roleDataProvider)
    {
      int count = 200;
      int usersCountInRole;
      if (!string.IsNullOrEmpty(membershipProviderName))
        usersCountInRole = roleDataProvider.GetUserLinks().Where<UserLink>((Expression<Func<UserLink, bool>>) (l => l.Role.Id == role.Id && l.MembershipManagerInfo.ProviderName == membershipProviderName)).Take<UserLink>(count).Count<UserLink>();
      else
        usersCountInRole = roleDataProvider.GetUserLinks().Where<UserLink>((Expression<Func<UserLink, bool>>) (l => l.Role.Id == role.Id)).Take<UserLink>(count).Count<UserLink>();
      return usersCountInRole;
    }
  }
}
