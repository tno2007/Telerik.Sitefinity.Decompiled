// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Data.RoleDataProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Security.Events;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Events;
using Telerik.Sitefinity.Utilities;

namespace Telerik.Sitefinity.Security.Data
{
  /// <summary>Represents data provider for role-management.</summary>
  public abstract class RoleDataProvider : DataProviderBase
  {
    private string[] supportedPermissionSets = new string[1]
    {
      "Backend"
    };

    /// <summary>
    /// Gets a list of all the roles for the configured applicationName.
    /// </summary>
    /// <returns>
    /// A string array containing the names of all the roles stored in the data
    /// source for the configured applicationName.
    /// </returns>
    public virtual string[] GetRoleNames() => this.GetRoles().Select<Role, string>((Expression<Func<Role, string>>) (r => r.Name)).ToArray<string>();

    /// <summary>
    /// Gets a value indicating whether the specified role name already
    /// exists in the role data source for the configured applicationName.
    /// </summary>
    /// <returns>
    /// true if the role name already exists in the data source for the
    /// configured applicationName; otherwise, false.
    /// </returns>
    /// <param name="roleName">
    /// The name of the role to search for in the data source.
    /// </param>
    public virtual bool RoleExists(string roleName) => this.GetRoles().Where<Role>((Expression<Func<Role, bool>>) (r => r.Name == roleName)).Any<Role>();

    /// <summary>Gets the role with the specified name.</summary>
    /// <param name="roleName">Name of the role.</param>
    /// <returns>The role with the passed role name</returns>
    public virtual Role GetRole(string roleName) => this.GetRoles().Where<Role>((Expression<Func<Role, bool>>) (r => r.Name == roleName)).SingleOrDefault<Role>();

    /// <summary>
    /// Gets all users from all providers which are members of the specified role.
    /// </summary>
    /// <param name="roleId">The role ID.</param>
    /// <returns>A list of users with the passed role id.</returns>
    public virtual IList<User> GetUsersInRole(Guid roleId) => this.GetUsersInRole(this.GetRole(roleId), string.Empty, string.Empty, 0, 0);

    /// <summary>
    /// Gets all users from all providers which are members of the specified role.
    /// </summary>
    /// <param name="roleId">The role ID.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="sortExpression">The sort expression.</param>
    /// <param name="skip">The number of roles to skip.</param>
    /// <param name="take">The number or roles to take.</param>
    /// <returns>A list of users.</returns>
    public virtual IList<User> GetUsersInRole(
      Guid roleId,
      string filterExpression,
      string sortExpression,
      int skip,
      int take)
    {
      return this.GetUsersInRole(this.GetRole(roleId), filterExpression, sortExpression, skip, take);
    }

    /// <summary>
    /// Gets all users from all providers which are members of the specified role.
    /// </summary>
    /// <param name="roleId">The role ID.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="sortExpression">The sort expression.</param>
    /// <param name="skip">The number of roles to skip.</param>
    /// <param name="take">The number or roles to take.</param>
    /// <param name="totalCount">The total count.</param>
    /// <returns>A list of users.</returns>
    public virtual IList<User> GetUsersInRole(
      Guid roleId,
      string filterExpression,
      string sortExpression,
      int skip,
      int take,
      out int totalCount)
    {
      return this.GetUsersInRole(this.GetRole(roleId), filterExpression, sortExpression, skip, take, out totalCount);
    }

    /// <summary>
    /// Gets all users from all providers which are members of the specified role.
    /// </summary>
    /// <param name="roleName">Name of the role.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="sortExpression">The sort expression.</param>
    /// <param name="skip">The number of roles to skip.</param>
    /// <param name="take">The number or roles to take.</param>
    /// <returns>A list of users.</returns>
    public virtual IList<User> GetUsersInRole(
      string roleName,
      string filterExpression,
      string sortExpression,
      int skip,
      int take)
    {
      return this.GetUsersInRole(this.GetRole(roleName), filterExpression, sortExpression, skip, take);
    }

    /// <summary>
    /// Gets all users from all providers which are members of the specified role.
    /// </summary>
    /// <param name="roleName">Name of the role.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="sortExpression">The sort expression.</param>
    /// <param name="skip">The number of roles to skip.</param>
    /// <param name="take">The number or roles to take.</param>
    /// <param name="totalCount">The total count.</param>
    /// <returns>A list of users</returns>
    public virtual IList<User> GetUsersInRole(
      string roleName,
      string filterExpression,
      string sortExpression,
      int skip,
      int take,
      out int totalCount)
    {
      return this.GetUsersInRole(this.GetRole(roleName), filterExpression, sortExpression, skip, take, out totalCount);
    }

    /// <summary>
    /// Gets all users from all providers which are members of the specified role.
    /// </summary>
    /// <param name="role">An instance of the <see cref="T:Telerik.Sitefinity.Security.Model.Role" /> object.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="sortExpression">The sort expression.</param>
    /// <param name="skip">The number of roles to skip.</param>
    /// <param name="take">The number or roles to take.</param>
    /// <returns>A list of users.</returns>
    public virtual IList<User> GetUsersInRole(
      Role role,
      string filterExpression,
      string sortExpression,
      int skip,
      int take)
    {
      if (role == null)
        throw new ArgumentNullException(nameof (role));
      return this.GetUsersInRole(role, filterExpression, sortExpression, skip, take, out int _);
    }

    /// <summary>Gets the users in role.</summary>
    /// <param name="role">The role.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="sortExpression">The sort expression.</param>
    /// <param name="skip">The number of roles to skip.</param>
    /// <param name="take">The number or roles to take.</param>
    /// <param name="totalCount">The total count.</param>
    /// <returns>A list of users.</returns>
    public virtual IList<User> GetUsersInRole(
      Role role,
      string filterExpression,
      string sortExpression,
      int skip,
      int take,
      out int totalCount)
    {
      return this.GetUsersInRole(role, (string) null, filterExpression, sortExpression, skip, take, out totalCount);
    }

    /// <summary>
    /// Gets all users from all providers which are members of the specified role.
    /// </summary>
    /// <param name="role">The role.</param>
    /// <param name="usersProviderName">Name of the users provider to return users from.If set to null returns from all users providers</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="sortExpression">The sort expression.</param>
    /// <param name="skip">The number of users to skip.</param>
    /// <param name="take">The number or users to take.</param>
    /// <param name="totalCount">The total count.</param>
    /// <returns>An IList of found User objects.</returns>
    /// <remarks>
    /// Note that this method collects users from all the registered membership providers and does in memory sorting and paging
    /// </remarks>
    public virtual IList<User> GetUsersInRole(
      Role role,
      string usersProviderName,
      string filterExpression,
      string sortExpression,
      int skip,
      int take,
      out int totalCount)
    {
      Guid id = role != null ? role.Id : throw new ArgumentNullException(nameof (role));
      IQueryable<UserLink> source1;
      if (usersProviderName == null)
        source1 = this.GetUserLinks().Where<UserLink>((Expression<Func<UserLink, bool>>) (l => l.Role.Id == id));
      else
        source1 = this.GetUserLinks().Where<UserLink>((Expression<Func<UserLink, bool>>) (l => l.Role.Id == id && l.MembershipManagerInfo.ProviderName == usersProviderName));
      int num1 = source1.Count<UserLink>();
      if (num1 == 0)
      {
        totalCount = 0;
        return (IList<User>) new User[0];
      }
      bool flag = false;
      if (string.IsNullOrEmpty(filterExpression) && string.IsNullOrEmpty(sortExpression))
      {
        if (skip != 0)
          source1 = source1.Skip<UserLink>(skip);
        if (take != 0)
          source1 = source1.Take<UserLink>(take);
        flag = true;
        skip = 0;
        take = 0;
      }
      IEnumerable<IGrouping<string, \u003C\u003Ef__AnonymousType67<Guid, string>>> groupings = source1.ToList<UserLink>().Select(usr => new
      {
        Id = usr.UserId,
        ProviderName = usr.MembershipManagerInfo.ProviderName
      }).GroupBy(user => user.ProviderName);
      List<User> source2 = new List<User>();
      int num2 = 0;
      totalCount = 0;
      int userReadBatchSize = this.GetUserReadBatchSize();
      foreach (IGrouping<string, \u003C\u003Ef__AnonymousType67<Guid, string>> source3 in groupings)
      {
        UserManager usersManager = this.GetUsersManager(source3.Key, this.TransactionName);
        foreach (Guid[] guidArray in ((IEnumerable<Guid>) source3.Select(u => u.Id).ToArray<Guid>()).OnBatchesOf<Guid>(userReadBatchSize))
        {
          Guid[] chunkArray = guidArray;
          IQueryable<User> source4 = usersManager.GetUsers().Where<User>((Expression<Func<User, bool>>) (u => chunkArray.Contains<Guid>(u.Id)));
          if (!filterExpression.IsNullOrEmpty())
            source4 = source4.Where<User>(filterExpression);
          try
          {
            IList<User> list = (IList<User>) source4.ToList<User>();
            num2 += list.Count;
            source2.AddRange((IEnumerable<User>) list);
          }
          catch (NullReferenceException ex)
          {
          }
        }
      }
      if (flag)
      {
        totalCount = num1;
        return (IList<User>) source2;
      }
      totalCount = num2;
      IQueryable<User> source5 = source2.AsQueryable<User>();
      if (!sortExpression.IsNullOrEmpty())
        source5 = source5.OrderBy<User>(sortExpression);
      if (skip > 0)
        source5 = source5.Skip<User>(skip);
      if (take > 0)
        source5 = source5.Take<User>(take);
      return (IList<User>) source5.ToList<User>();
    }

    /// <summary>
    /// Gets the size of the users per role read batch size.
    /// This size is used when retrieving users in the GetUsersInRole method
    /// </summary>
    /// <returns>The user read batch size</returns>
    protected virtual int GetUserReadBatchSize() => 50;

    /// <summary>
    /// Gets the users manager by providerName. Extracted here for test mocking purposes
    /// </summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="transaction">The name of the transaction.</param>
    /// <returns>Returns a user manager</returns>
    protected virtual UserManager GetUsersManager(
      string providerName,
      string transaction = null)
    {
      return UserManager.GetManager(providerName, transaction);
    }

    /// <summary>Gets a list of the roles that a specified user is in.</summary>
    /// <param name="userId">The user id to return a list of roles for.</param>
    /// <returns>
    /// A string array containing the names of all the roles that the specified
    /// user is in for the configured applicationName.
    /// </returns>
    public virtual IQueryable<Role> GetRolesForUser(Guid userId) => this.GetUserLinks().Where<UserLink>((Expression<Func<UserLink, bool>>) (l => l.UserId == userId)).Select<UserLink, Role>((Expression<Func<UserLink, Role>>) (l => l.Role));

    /// <summary>
    /// Gets a value indicating whether the specified user is in the specified role.
    /// </summary>
    /// <returns>
    /// true if the specified user is in the specified role; otherwise, false.
    /// </returns>
    /// <param name="userId">The user ID to search for.</param>
    /// <param name="roleName">The role to search in.</param>
    public virtual bool IsUserInRole(Guid userId, string roleName) => this.GetUserLinks().Where<UserLink>((Expression<Func<UserLink, bool>>) (l => l.Role.Name == roleName && l.UserId == userId)).Any<UserLink>();

    /// <summary>
    /// Gets a value indicating whether the specified user is in the specified role.
    /// </summary>
    /// <returns>
    /// true if the specified user is in the specified role; otherwise, false.
    /// </returns>
    /// <param name="userId">The user ID to search for.</param>
    /// <param name="roleId">The role to search in.</param>
    public virtual bool IsUserInRole(Guid userId, Guid roleId) => this.GetUserLinks().Where<UserLink>((Expression<Func<UserLink, bool>>) (l => l.Role.Id == roleId && l.UserId == userId)).Any<UserLink>();

    /// <summary>Adds the provided user to the provided role.</summary>
    /// <param name="user">The user.</param>
    /// <param name="role">The role.</param>
    [GlobalPermission(new string[] {"ManageUsers"})]
    public virtual void AddUserToRole(User user, Role role)
    {
      if (user == null)
        throw new ArgumentNullException(nameof (user));
      if (role == null)
        throw new ArgumentNullException(nameof (role));
      if (SecurityManager.UnassignableRoles.Contains(role.Id))
        throw new ArgumentException(string.Format("Users cannot be assigned to role {0} (Role ID: {1})", (object) role.Name, (object) role.Id.ToString()), nameof (role));
      Guid roleId = role.Id;
      Guid userId = user.Id;
      if (this.GetUserLinks().Where<UserLink>((Expression<Func<UserLink, bool>>) (l => l.Role.Id == roleId && l.UserId == userId)).Any<UserLink>())
        return;
      UserLink userLink = this.CreateUserLink();
      userLink.UserId = user.Id;
      userLink.Role = role;
      ManagerInfo managerInfo = this.GetManagerInfo(user.ManagerInfo.ManagerType, user.ManagerInfo.ProviderName);
      userLink.MembershipManagerInfo = managerInfo;
      role.Users.Add(userLink);
    }

    /// <summary>Removes the provided user from the provided role.</summary>
    /// <param name="user">The user.</param>
    /// <param name="role">The role.</param>
    [GlobalPermission(new string[] {"ManageUsers"})]
    public virtual void RemoveUserFromRole(User user, Role role)
    {
      if (user == null)
        throw new ArgumentNullException(nameof (user));
      if (role == null)
        throw new ArgumentNullException(nameof (role));
      this.RemoveUserFromRole(user.Id, role);
    }

    /// <summary>Removes the provided user from the provided role.</summary>
    /// <param name="userId">The user pageId.</param>
    /// <param name="role">The role.</param>
    [GlobalPermission(new string[] {"ManageUsers"})]
    public virtual void RemoveUserFromRole(Guid userId, Role role)
    {
      if (userId == Guid.Empty)
        throw new ArgumentNullException(nameof (userId));
      Guid roleId = role != null ? role.Id : throw new ArgumentNullException(nameof (role));
      UserLink userLink = this.GetUserLinks().Where<UserLink>((Expression<Func<UserLink, bool>>) (l => l.Role.Id == roleId && l.UserId == userId)).SingleOrDefault<UserLink>();
      if (userLink == null)
        return;
      this.Delete(userLink);
    }

    /// <summary>Creates new security role with the specified name.</summary>
    /// <param name="roleName">The role name.</param>
    /// <returns>The new role.</returns>
    [MethodPermission("Backend", new string[] {"ManageRoles"})]
    public abstract Role CreateRole(string roleName);

    /// <summary>
    /// Creates new security role with the specified identity and name.
    /// </summary>
    /// <param name="id">The id of the new role.</param>
    /// <param name="roleName">Name of the role.</param>
    /// <returns>The new role.</returns>
    [MethodPermission("Backend", new string[] {"ManageRoles"})]
    public abstract Role CreateRole(Guid id, string roleName);

    /// <summary>Gets the role with the specified identity.</summary>
    /// <param name="id">The identity to search for.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Security.Model.Role" />.</returns>
    public abstract Role GetRole(Guid id);

    /// <summary>Gets a query for roles.</summary>
    /// <returns>The query for roles.</returns>
    public abstract IQueryable<Role> GetRoles();

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The item to delete.</param>
    [MethodPermission("Backend", new string[] {"ManageRoles"})]
    public abstract void Delete(Role item);

    /// <summary>Creates new user link.</summary>
    /// <returns>The created user link</returns>
    public abstract UserLink CreateUserLink();

    /// <summary>
    /// Creates new user link for the specified user identity.
    /// </summary>
    /// <param name="id">The identity.</param>
    /// <returns>The created user link</returns>
    public abstract UserLink CreateUserLink(Guid id);

    /// <summary>Gets the role with the specified identity.</summary>
    /// <param name="id">The identity to search for.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Security.Model.Role" />.</returns>
    public abstract UserLink GetUserLink(Guid id);

    /// <summary>Gets a query for roles.</summary>
    /// <returns>The query for roles.</returns>
    public abstract IQueryable<UserLink> GetUserLinks();

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The item to delete.</param>
    public abstract void Delete(UserLink item);

    /// <summary>Creates new data item.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="id">The item id.</param>
    /// <returns>The created item.</returns>
    public override object CreateItem(Type itemType, Guid id)
    {
      if (itemType == typeof (Role))
        return (object) this.CreateRole(id, string.Empty);
      throw DataProviderBase.GetInvalidItemTypeException(itemType, this.GetKnownTypes());
    }

    /// <summary>
    /// Gets the data item with the specified ID.
    /// An exception should be thrown if an item with the specified ID does not exist.
    /// </summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="id">The ID of the item to return.</param>
    /// <returns>The item.</returns>
    public override object GetItem(Type itemType, Guid id) => itemType == typeof (Role) ? (object) this.GetRole(id) : base.GetItem(itemType, id);

    /// <summary>
    /// Get item by primary key without throwing exceptions if it doesn't exist
    /// </summary>
    /// <param name="itemType">Type of the item to get</param>
    /// <param name="id">Primary key</param>
    /// <returns>Item or default value</returns>
    public override object GetItemOrDefault(Type itemType, Guid id)
    {
      if (!(itemType == typeof (Role)))
        return base.GetItem(itemType, id);
      return (object) this.GetRoles().Where<Role>((Expression<Func<Role, bool>>) (r => r.Id == id)).FirstOrDefault<Role>();
    }

    /// <summary>Gets the items.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="orderExpression">The order expression.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <param name="totalCount">Total count of the items that are filtered by <paramref name="filterExpression" /></param>
    /// <returns>The items filtered and sorted by the given parameters.</returns>
    public override IEnumerable GetItems(
      Type itemType,
      string filterExpression,
      string orderExpression,
      int skip,
      int take,
      ref int? totalCount)
    {
      if (itemType == typeof (Role))
        return (IEnumerable) DataProviderBase.SetExpressions<Role>(this.GetRoles(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      throw DataProviderBase.GetInvalidItemTypeException(itemType, this.GetKnownTypes());
    }

    /// <summary>
    /// Marks the provided persistent item for deletion.
    /// The item is deleted form the storage when the transaction is committed.
    /// </summary>
    /// <param name="item">The item to be deleted.</param>
    public override void DeleteItem(object item)
    {
      if (item is Role)
        this.Delete((Role) item);
      throw DataProviderBase.GetInvalidItemTypeException(item.GetType(), this.GetKnownTypes());
    }

    /// <summary>Gets a unique key for each data provider base.</summary>
    /// <value></value>
    public override string RootKey => nameof (RoleDataProvider);

    /// <summary>
    /// Gets a value indicating whether to check for updates for the provider during the installation.
    /// </summary>
    /// <value><c>true</c> if [check for updates]; otherwise, <c>false</c>.</value>
    public override bool CheckForUpdates => false;

    /// <summary>Get a list of types served by this manager</summary>
    /// <returns>Array of the know types</returns>
    public override Type[] GetKnownTypes() => new Type[1]
    {
      typeof (Role)
    };

    /// <summary>Gets the security root.</summary>
    /// <returns>The security root.</returns>
    public override ISecuredObject GetSecurityRoot()
    {
      if (AppPermission.Root != null)
        return (ISecuredObject) SecurityManager.GetManager().GetSecurityRoot("ApplicationSecurityRoot");
      throw new InvalidOperationException("Missing application global permission.");
    }

    /// <summary>Gets the security root.</summary>
    /// <param name="create">if set to <c>true</c> a security root will be created for the provider if there is no root.</param>
    /// <returns>The security root.</returns>
    public override ISecuredObject GetSecurityRoot(bool create)
    {
      if (AppPermission.Root != null)
        return (ISecuredObject) SecurityManager.GetManager().GetSecurityRoot("ApplicationSecurityRoot");
      throw new InvalidOperationException("Missing application global permission.");
    }

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

    /// <summary>Commits the transaction.</summary>
    public override void CommitTransaction()
    {
      this.CollectEventsData();
      base.CommitTransaction();
      this.RaiseEvents(false);
    }

    /// <summary>
    /// Flush all dirty and new instances to the database and evict all instances from the local cache.
    /// </summary>
    public override void FlushTransaction()
    {
      this.CollectEventsData();
      base.FlushTransaction();
    }

    private void CollectEventsData()
    {
      List<RoleAssignEventBase> data = new List<RoleAssignEventBase>();
      IList dirtyItems = this.GetDirtyItems();
      if (dirtyItems.Count == 0)
        return;
      try
      {
        object origin;
        this.TryGetExecutionStateData("EventOriginKey", out origin);
        foreach (object obj in (IEnumerable) dirtyItems)
        {
          UserLink userLink = obj as UserLink;
          if (userLink != null)
          {
            RoleAssignEventBase roleAssignEventBase;
            switch (this.GetDirtyItemStatus((object) userLink))
            {
              case SecurityConstants.TransactionActionType.New:
                UserLinkInfo userLinkInfo1 = new UserLinkInfo(userLink, this);
                this.RaiseEvent((IEvent) new RoleAssigning(userLinkInfo1), origin, true);
                roleAssignEventBase = (RoleAssignEventBase) new RoleAssigned(userLinkInfo1);
                break;
              case SecurityConstants.TransactionActionType.Deleted:
                Role originalValue = this.GetOriginalValue<Role>((object) userLink, "Role");
                UserLinkInfo userLinkInfo2 = new UserLinkInfo(userLink, originalValue, this);
                this.RaiseEvent((IEvent) new RoleUnassigning(userLinkInfo2), origin, true);
                roleAssignEventBase = (RoleAssignEventBase) new RoleUnassigned(userLinkInfo2);
                break;
              default:
                continue;
            }
            if (roleAssignEventBase != null)
              data.Add(roleAssignEventBase);
          }
        }
      }
      catch (Exception ex)
      {
        this.RollbackTransaction();
        throw ex;
      }
      if (data.Count <= 0)
        return;
      this.SetExecutionStateData("events", (object) data);
    }

    private void RaiseEvent(IEvent eventData, object origin, bool throwExceptions)
    {
      if (origin != null)
        eventData.Origin = (string) origin;
      EventHub.Raise(eventData, throwExceptions);
    }

    private void RaiseEvents(bool throwExceptions)
    {
      if (!(this.GetExecutionStateData("events") is List<RoleAssignEventBase> executionStateData))
        return;
      object origin;
      this.TryGetExecutionStateData("EventOriginKey", out origin);
      foreach (IEvent eventData in executionStateData)
        this.RaiseEvent(eventData, origin, throwExceptions);
      this.SetExecutionStateData("events", (object) null);
    }
  }
}
