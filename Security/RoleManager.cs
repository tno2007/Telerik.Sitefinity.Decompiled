// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.RoleManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration.Provider;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Security;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Data;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Security
{
  /// <summary>
  /// Represents an intermediary between user roles and role data providers.
  /// </summary>
  public class RoleManager : ManagerBase<RoleDataProvider>
  {
    private ConfigElementDictionary<string, DataProviderSettings> providerSettings;

    /// <summary>
    /// Initializes a new instance of <see cref="T:Telerik.Sitefinity.Security.RoleManager" /> class with the default provider.
    /// </summary>
    public RoleManager()
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="T:Telerik.Sitefinity.Security.RoleManager" /> class and sets the specified provider.
    /// </summary>
    /// <param name="providerName">
    /// The name of the provider. If empty string or null the default provider is set.
    /// </param>
    public RoleManager(string providerName)
      : base(providerName)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.RoleManager" /> class.
    /// </summary>
    /// <param name="providerName">
    /// The name of the provider. If empty string or null the default provider is set
    /// </param>
    /// <param name="transactionName">
    /// The name of a distributed transaction. If empty string or null this manager will use separate transaction.
    /// </param>
    public RoleManager(string providerName, string transactionName)
      : base(providerName, transactionName)
    {
    }

    /// <summary>
    /// Gets a list of all the roles for the configured applicationName.
    /// </summary>
    /// <returns>
    /// A string array containing the names of all the roles stored in the data
    /// source for the configured applicationName.
    /// </returns>
    public virtual string[] GetRoleNames() => this.Provider.GetRoleNames();

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
    public virtual bool RoleExists(string roleName) => this.Provider.RoleExists(roleName);

    /// <summary>Gets the role with the specified name.</summary>
    /// <param name="roleName">Name of the role.</param>
    /// <returns></returns>
    public virtual Role GetRole(string roleName) => this.Provider.GetRole(roleName);

    /// <summary>
    /// Gets all users from all providers which are members of the specified role.
    /// </summary>
    /// <param name="roleId">The role ID.</param>
    public virtual IList<User> GetUsersInRole(Guid roleId) => this.GetUsersInRole(roleId, (string) null, (string) null, 0, 0);

    /// <summary>
    /// Gets all users from all providers which are members of the specified role.
    /// </summary>
    /// <param name="roleName">Name of the role.</param>
    /// <returns>A list of <see cref="T:Telerik.Sitefinity.Security.Model.User" /> objects.</returns>
    public virtual IList<User> GetUsersInRole(string roleName) => this.GetUsersInRole(roleName, (string) null, (string) null, 0, 0);

    /// <summary>
    /// Gets all users from all providers which are members of the specified role.
    /// </summary>
    /// <param name="roleId">The role.</param>
    public virtual IList<User> GetUsersInRole(Role role) => this.GetUsersInRole(role, (string) null, (string) null, 0, 0);

    /// <summary>
    /// Gets all users from all providers which are members of the specified role.
    /// </summary>
    /// <param name="roleId">The role ID.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="sortExpression">The sort expression.</param>
    /// <param name="skip">The number of roles to skip.</param>
    /// <param name="take">The number or roles to take.</param>
    /// <returns></returns>
    public virtual IList<User> GetUsersInRole(
      Guid roleId,
      string filterExpression,
      string sortExpression,
      int skip,
      int take)
    {
      return this.Provider.GetUsersInRole(roleId, filterExpression, sortExpression, skip, take);
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
    /// <returns></returns>
    public virtual IList<User> GetUsersInRole(
      Guid roleId,
      string filterExpression,
      string sortExpression,
      int skip,
      int take,
      out int totalCount)
    {
      return this.Provider.GetUsersInRole(roleId, filterExpression, sortExpression, skip, take, out totalCount);
    }

    /// <summary>Gets the users in role.</summary>
    /// <param name="role">The role.</param>
    /// <param name="usersProviderName">Name of the users membership provider to return users from (act as a filtering parameter). If set to null returns users from all providers</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="sortExpression">The sort expression.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <param name="totalCount">The total count.</param>
    /// <returns></returns>
    public virtual IList<User> GetUsersInRole(
      Role role,
      string usersProviderName,
      string filterExpression,
      string sortExpression,
      int skip,
      int take,
      out int totalCount)
    {
      return this.Provider.GetUsersInRole(role, usersProviderName, filterExpression, sortExpression, skip, take, out totalCount);
    }

    /// <summary>
    /// Gets all users from all providers which are members of the specified role.
    /// </summary>
    /// <param name="roleId">The role name.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="sortExpression">The sort expression.</param>
    /// <param name="skip">The number of roles to skip.</param>
    /// <param name="take">The number or roles to take.</param>
    /// <returns></returns>
    public virtual IList<User> GetUsersInRole(
      string roleName,
      string filterExpression,
      string sortExpression,
      int skip,
      int take)
    {
      return this.Provider.GetUsersInRole(roleName, filterExpression, sortExpression, skip, take);
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
    /// <returns></returns>
    public virtual IList<User> GetUsersInRole(
      string roleName,
      string filterExpression,
      string sortExpression,
      int skip,
      int take,
      out int totalCount)
    {
      return this.Provider.GetUsersInRole(roleName, filterExpression, sortExpression, skip, take, out totalCount);
    }

    /// <summary>
    /// Gets all users from all providers which are members of the specified role.
    /// </summary>
    /// <param name="roleId">The role.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="sortExpression">The sort expression.</param>
    /// <param name="skip">The number of roles to skip.</param>
    /// <param name="take">The number or roles to take.</param>
    /// <returns></returns>
    public virtual IList<User> GetUsersInRole(
      Role role,
      string filterExpression,
      string sortExpression,
      int skip,
      int take)
    {
      return this.Provider.GetUsersInRole(role, filterExpression, sortExpression, skip, take);
    }

    /// <summary>
    /// Gets all users from all providers which are members of the specified role.
    /// </summary>
    /// <param name="role">The role.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="sortExpression">The sort expression.</param>
    /// <param name="skip">The number of roles to skip.</param>
    /// <param name="take">The number or roles to take.</param>
    /// <param name="totalCount">The total count.</param>
    /// <returns></returns>
    public virtual IList<User> GetUsersInRole(
      Role role,
      string filterExpression,
      string sortExpression,
      int skip,
      int take,
      out int totalCount)
    {
      return this.Provider.GetUsersInRole(role, filterExpression, sortExpression, skip, take, out totalCount);
    }

    /// <summary>Gets a list of the roles that a specified user is in.</summary>
    /// <param name="userName">The user to return a list of roles for.</param>
    /// <returns>
    /// A string array containing the names of all the roles that the specified
    /// user is in for the configured applicationName.
    /// </returns>
    public virtual IQueryable<Role> GetRolesForUser(Guid userId) => this.Provider.GetRolesForUser(userId);

    /// <summary>
    /// Gets a value indicating whether the specified user is in the specified role.
    /// </summary>
    /// <returns>
    /// true if the specified user is in the specified role; otherwise, false.
    /// </returns>
    /// <param name="username">The user ID to search for.</param>
    /// <param name="roleName">The role to search in.</param>
    public virtual bool IsUserInRole(Guid usernId, string roleName) => this.Provider.IsUserInRole(usernId, roleName);

    /// <summary>
    /// Gets a value indicating whether the specified user is in the specified role.
    /// </summary>
    /// <returns>
    /// true if the specified user is in the specified role; otherwise, false.
    /// </returns>
    /// <param name="username">The user ID to search for.</param>
    /// <param name="roleName">The role to search in.</param>
    public virtual bool IsUserInRole(Guid usernId, Guid roleId) => this.Provider.IsUserInRole(usernId, roleId);

    /// <summary>Adds the provided user to the provided role.</summary>
    /// <param name="user">The user.</param>
    /// <param name="role">The role.</param>
    public virtual void AddUserToRole(User user, Role role) => this.Provider.AddUserToRole(user, role);

    /// <summary>Removes the provided user from the provided role.</summary>
    /// <param name="user">The user.</param>
    /// <param name="role">The role.</param>
    public virtual void RemoveUserFromRole(User user, Role role)
    {
      UserActivityManager.RegisterDeletedUser(user);
      this.Provider.RemoveUserFromRole(user, role);
    }

    /// <summary>Removes the provided user from the provided role.</summary>
    /// <param name="userId">The user pageId.</param>
    /// <param name="role">The role.</param>
    public virtual void RemoveUserFromRole(Guid userId, Role role)
    {
      User user = UserManager.FindUser(userId);
      if (user != null)
        UserActivityManager.RegisterDeletedUser(user);
      this.Provider.RemoveUserFromRole(userId, role);
    }

    /// <summary>Creates new security role with the specified name.</summary>
    /// <param name="roleName">Name of the role.</param>
    /// <returns>The new role.</returns>
    public virtual Role CreateRole(string roleName) => this.VerifyRoleName(roleName) ? this.Provider.CreateRole(roleName) : throw new ArgumentException(string.Format(Res.Get<SecurityResources>().ApplicationRoleAlreadyExistsWithThisName, (object) roleName));

    /// <summary>
    /// Creates new security role with the specified identity and name.
    /// </summary>
    /// <param name="pageId">The identity of the new role.</param>
    /// <param name="roleName">Name of the role.</param>
    /// <returns>The new role.</returns>
    public virtual Role CreateRole(Guid id, string roleName)
    {
      if (!this.VerifyRoleName(roleName))
        throw new ArgumentException(string.Format(Res.Get<SecurityResources>().ApplicationRoleAlreadyExistsWithThisName, (object) roleName));
      return this.Provider.CreateRole(id, roleName);
    }

    /// <summary>
    /// Validates the role's name is ok, by checking it does not collide with existing application roles.
    /// (Additional validations may be added here).
    /// </summary>
    /// <param name="roleName">Name of the role.</param>
    /// <returns>True if the role name is ok, False otherwise.</returns>
    private bool VerifyRoleName(string roleName) => string.IsNullOrWhiteSpace(roleName) || !Config.Get<SecurityConfig>().ApplicationRoles.ContainsKey(roleName) || RoleManager.GetManager("AppRoles").GetRole(roleName) == null;

    /// <summary>Gets the role with the specified identity.</summary>
    /// <param name="pageId">The identity to search for.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Security.Model.Role" />.</returns>
    public virtual Role GetRole(Guid id) => this.Provider.GetRole(id);

    /// <summary>Gets a query for roles.</summary>
    /// <returns>The query for roles.</returns>
    public virtual IQueryable<Role> GetRoles() => this.Provider.GetRoles();

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The item to delete.</param>
    public virtual void Delete(Role item) => this.Provider.Delete(item);

    /// <summary>
    /// Gets the name of the default provider for this manager.
    /// </summary>
    protected internal override GetDefaultProvider DefaultProviderDelegate => (GetDefaultProvider) (() => Config.Get<SecurityConfig>().DefaultBackendRoleProvider);

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
          this.providerSettings = Config.Get<SecurityConfig>().RoleProviders;
        return this.providerSettings;
      }
    }

    /// <summary>
    /// This method is called after data provider initialization,
    /// when the manager is instantiated for the first time in the application lifecycle.
    /// </summary>
    protected override void OnInitialized()
    {
      if (!Roles.Enabled || !Config.Get<SecurityConfig>().AllowExternalRoleProviders)
        return;
      this.StaticProviders.Unlock();
      foreach (RoleProvider provider in (ProviderCollection) Roles.Providers)
      {
        if (provider is SitefinityRoleProvider sitefinityRoleProvider)
        {
          sitefinityRoleProvider.DataProvider = this.StaticProviders[sitefinityRoleProvider.Name];
        }
        else
        {
          if (this.Providers.Contains(provider.Name))
          {
            RoleDataProvider staticProvider = this.StaticProviders[provider.Name];
            this.StaticProviders.Remove(provider.Name);
            ObjectFactory.Container.Teardown((object) staticProvider);
          }
          this.StaticProviders.Add((RoleDataProvider) new RoleProviderWrapper(provider));
          Type type = this.GetType();
          ObjectFactory.Container.RegisterType(type, type, provider.Name.ToUpperInvariant(), (LifetimeManager) new HttpRequestLifetimeManager(), (InjectionMember) new InjectionConstructor(new object[1]
          {
            (object) provider.Name
          }));
        }
      }
      this.StaticProviders.Lock();
    }

    /// <summary>Gets an instance for RoleManager.</summary>
    /// <returns>An instance of RoleManager.</returns>
    public static RoleManager GetManager() => ManagerBase<RoleDataProvider>.GetManager<RoleManager>();

    /// <summary>
    /// Gets an instance for RoleManager for the specified data provider.
    /// </summary>
    /// <param name="providerName">The name of the data provider.</param>
    /// <returns>An instance of RoleManager.</returns>
    public static RoleManager GetManager(string providerName) => ManagerBase<RoleDataProvider>.GetManager<RoleManager>(providerName);

    public static RoleManager GetManager(string providerName, string transactionName) => ManagerBase<RoleDataProvider>.GetManager<RoleManager>(providerName, transactionName);

    public static IEnumerable<RoleInfo> GetAllRolesOfUser(Guid userId)
    {
      ConfigElementDictionary<string, ApplicationRole> applicationRoles = Config.Get<SecurityConfig>().ApplicationRoles;
      ApplicationRole applicationRole1 = applicationRoles["Everyone"];
      RoleInfo roleInfo = new RoleInfo()
      {
        Id = applicationRole1.Id,
        Name = applicationRole1.Name,
        Provider = applicationRole1.Provider.Name
      };
      if (userId == Guid.Empty)
      {
        ApplicationRole applicationRole2 = applicationRoles["Anonymous"];
        return (IEnumerable<RoleInfo>) new RoleInfo[2]
        {
          roleInfo,
          new RoleInfo()
          {
            Id = applicationRole2.Id,
            Name = applicationRole2.Name,
            Provider = applicationRole2.Provider.Name
          }
        };
      }
      List<RoleInfo> allRolesOfUser = new List<RoleInfo>();
      allRolesOfUser.Add(roleInfo);
      ApplicationRole applicationRole3 = applicationRoles["Authenticated"];
      allRolesOfUser.Add(new RoleInfo()
      {
        Id = applicationRole3.Id,
        Name = applicationRole3.Name,
        Provider = applicationRole3.Provider.Name
      });
      foreach (RoleDataProvider roleDataProvider in (Collection<RoleDataProvider>) (ManagerBase<RoleDataProvider>.StaticProvidersCollection ?? RoleManager.GetManager().StaticProviders))
      {
        IEnumerable<Role> rolesForUser;
        try
        {
          rolesForUser = (IEnumerable<Role>) roleDataProvider.GetRolesForUser(userId);
        }
        catch
        {
          continue;
        }
        foreach (Role role in rolesForUser)
          allRolesOfUser.Add(new RoleInfo()
          {
            Id = role.Id,
            Name = role.Name,
            Provider = roleDataProvider.Name
          });
      }
      return (IEnumerable<RoleInfo>) allRolesOfUser;
    }

    public static IEnumerable<Guid> GetAllRoleIdsOfUser(Guid userId)
    {
      ConfigElementDictionary<string, ApplicationRole> applicationRoles = Config.Get<SecurityConfig>().ApplicationRoles;
      Guid id = applicationRoles["Everyone"].Id;
      if (userId == Guid.Empty)
      {
        ApplicationRole applicationRole = applicationRoles["Anonymous"];
        return (IEnumerable<Guid>) new Guid[2]
        {
          id,
          applicationRole.Id
        };
      }
      List<Guid> allRoleIdsOfUser = new List<Guid>();
      ApplicationRole applicationRole1 = applicationRoles["Administrators"];
      allRoleIdsOfUser.Add(id);
      ApplicationRole applicationRole2 = applicationRoles["Authenticated"];
      allRoleIdsOfUser.Add(applicationRole2.Id);
      bool flag1 = false;
      bool flag2 = false;
      foreach (RoleDataProvider roleDataProvider in (Collection<RoleDataProvider>) (ManagerBase<RoleDataProvider>.StaticProvidersCollection ?? RoleManager.GetManager().StaticProviders))
      {
        foreach (Role role in (IEnumerable<Role>) roleDataProvider.GetRolesForUser(userId))
        {
          if (role.Id == applicationRole1.Id)
          {
            flag1 = true;
            flag2 = true;
          }
          else
            flag1 = SecurityManager.IsAdministrativeRole(role.Id);
          allRoleIdsOfUser.Add(role.Id);
        }
      }
      if (flag1 && !flag2)
        allRoleIdsOfUser.Add(applicationRole1.Id);
      return (IEnumerable<Guid>) allRoleIdsOfUser;
    }

    public static bool IsUserUnrestricted(Guid userId)
    {
      if (userId != Guid.Empty)
      {
        User user = UserManager.FindUser(userId);
        if (user != null && new SitefinityIdentity(user, true).IsUnrestricted)
          return true;
      }
      return false;
    }

    /// <summary>
    /// Finds the roles for the specified user from all role providers.
    /// </summary>
    /// <param name="id">The user id.</param>
    /// <param name="transactionName">The name of the transaction.</param>
    /// <returns></returns>
    public static IList<Role> FindRolesForUser(Guid id, string transactionName = null)
    {
      List<Role> rolesForUser1 = new List<Role>();
      foreach (DataProviderBase dataProviderBase in (Collection<RoleDataProvider>) (ManagerBase<RoleDataProvider>.StaticProvidersCollection ?? RoleManager.GetManager().StaticProviders))
      {
        IQueryable<Role> rolesForUser2 = RoleManager.GetManager(dataProviderBase.Name, transactionName).GetRolesForUser(id);
        rolesForUser1.AddRange((IEnumerable<Role>) rolesForUser2);
      }
      return (IList<Role>) rolesForUser1;
    }

    /// <summary>
    /// Checks in all data providers if a role with a specified name exists.
    /// </summary>
    /// <param name="role">the name of the role to search</param>
    /// <returns>true if the role exists, false otherwise</returns>
    public static bool RoleExistsInAnyProvider(string role)
    {
      if (ManagerBase<RoleDataProvider>.StaticProvidersCollection == null)
      {
        ProvidersCollection<RoleDataProvider> staticProviders1 = RoleManager.GetManager().StaticProviders;
      }
      foreach (RoleDataProvider staticProviders2 in (Collection<RoleDataProvider>) ManagerBase<RoleDataProvider>.StaticProvidersCollection)
      {
        if (staticProviders2.RoleExists(role))
          return true;
      }
      return false;
    }

    /// <summary>
    /// Checks in all data providers if a role with a specified pageId exists.
    /// </summary>
    /// <param name="pageId">the role pageId to search</param>
    /// <returns>true if the role exists, false otherwise</returns>
    public static bool RoleExistsInAnyProvider(Guid id)
    {
      if (ManagerBase<RoleDataProvider>.StaticProvidersCollection == null)
      {
        ProvidersCollection<RoleDataProvider> staticProviders1 = RoleManager.GetManager().StaticProviders;
      }
      foreach (RoleDataProvider staticProviders2 in (Collection<RoleDataProvider>) ManagerBase<RoleDataProvider>.StaticProvidersCollection)
      {
        if (staticProviders2.GetRoles().Where<Role>((Expression<Func<Role, bool>>) (r => r.Id == id)).Any<Role>())
          return true;
      }
      SecurityManager.ApplicationRoles.Values.Where<ApplicationRole>((Func<ApplicationRole, bool>) (approle => approle.Id == id)).Any<ApplicationRole>();
      return true;
    }
  }
}
