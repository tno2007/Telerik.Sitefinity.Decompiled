// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Ldap.LdapRoleProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Security.Data;
using Telerik.Sitefinity.Security.Ldap.Helpers;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.SitefinityExceptions;

namespace Telerik.Sitefinity.Security.Ldap
{
  /// <summary>
  /// This class is implementation of Sitefinity 4.1 role provider for an LDAP database.
  /// </summary>
  public class LdapRoleProvider : 
    RoleDataProvider,
    ICommonDataProvider,
    IDataProviderBase,
    IDisposable,
    ICloneable,
    IDataProviderEventsCall,
    ILdapProviderMarker
  {
    private string ldapConnectionName;
    private ILdapFacade ldapFacade;
    private ManagerInfo managerInfo;
    private ManagerInfo userManagerInfo;
    private string membershipProviderName;
    private LdapBuilder ldapBuilder;

    /// <summary>Emplty overrided method- does not throws exception.</summary>
    public override void CommitTransaction()
    {
    }

    /// <summary>
    ///  the Ldap facade to be used, if external is set the default is not used
    /// </summary>
    public ILdapFacade LdapFacade
    {
      get => this.ldapFacade != null ? this.ldapFacade : (ILdapFacade) new Telerik.Sitefinity.Security.Ldap.LdapFacade(this.ldapConnectionName);
      set => this.ldapFacade = value;
    }

    /// <summary>Gets the LDAP builder.</summary>
    /// <value>The LDAP builder.</value>
    protected virtual LdapBuilder LdapBuilder
    {
      get
      {
        if (this.ldapBuilder == null)
          this.ldapBuilder = LdapBuilder.GetBuilder();
        return this.ldapBuilder;
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
        abilities.AddAbility("GetRole", true, true);
        abilities.AddAbility("AddRole", false, false);
        abilities.AddAbility("AssingUserToRole", false, false);
        abilities.AddAbility("UnAssingUserFromRole", false, false);
        abilities.AddAbility("DeleteRole", false, false);
        return abilities;
      }
    }

    /// <summary>Gets the role with the specified identity.</summary>
    /// <param name="pageId">The identity to search for.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Security.Model.Role" />.</returns>
    public override Role GetRole(Guid id)
    {
      using (ILdapFacade ldapFacade = this.LdapFacade)
      {
        Role role = this.BuildRoleWithUserLinks(ldapFacade.GetRole(id) ?? throw new ItemNotFoundException("LdapRoleProvider role id = {0}".Arrange((object) id)), ldapFacade);
        ((IDataItem) role).Provider = (object) this;
        return role;
      }
    }

    /// <summary>Gets the role with the specified identity.</summary>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Security.Model.Role" />.</returns>
    public override IQueryable<Role> GetRoles() => SitefinityQuery.Get<Role>((DataProviderBase) this).Where<Role>((Expression<Func<Role, bool>>) (c => c.ApplicationName == this.ApplicationName));

    /// <summary>Gets a list of the roles that a specified user is in.</summary>
    /// <param name="userId">The user to return a list of roles for.</param>
    /// <returns>
    /// A string array containing the names of all the roles that the specified
    /// user is in for the configured applicationName.
    /// </returns>
    public override IQueryable<Role> GetRolesForUser(Guid userId)
    {
      List<Role> source = new List<Role>();
      using (ILdapFacade ldapFacade = this.LdapFacade)
      {
        SearchResultEntry userById = ldapFacade.GetUserById(userId, LdapAttributeNames.memberOf);
        if (userById == null)
          return source.AsQueryable<Role>();
        if (userById.Attributes[LdapAttributeNames.memberOf] == null)
          return source.AsQueryable<Role>();
        IEnumerable<string> roleDNs = userById.Attributes[LdapAttributeNames.memberOf].GetValues(typeof (string)).Cast<string>();
        foreach (SearchResultEntry rolesFromRoleDn in ldapFacade.GetRolesFromRoleDns(roleDNs))
        {
          Role role = this.LdapBuilder.Build<Role>(rolesFromRoleDn, this.ManagerInfo, this.ApplicationName);
          ((IDataItem) role).Provider = (object) this;
          source.Add(role);
        }
      }
      return source.AsQueryable<Role>();
    }

    /// <summary>Gets the role with the specified identity.</summary>
    /// <param name="pageId">The identity to search for.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Security.Model.Role" />.</returns>
    public override UserLink GetUserLink(Guid id) => throw new NotSupportedException();

    /// <summary>Gets a query for roles.</summary>
    /// <returns>The query for roles.</returns>
    public override IQueryable<UserLink> GetUserLinks() => SitefinityQuery.Get<UserLink>((DataProviderBase) this).Where<UserLink>((Expression<Func<UserLink, bool>>) (ul => ul.ApplicationName == this.ApplicationName));

    /// <summary>
    /// Gets a list of all the roles for the configured applicationName.
    /// </summary>
    /// <returns>
    /// A string array containing the names of all the roles stored in the data
    /// source for the configured applicationName.
    /// </returns>
    public override string[] GetRoleNames()
    {
      using (ILdapFacade facade = this.LdapFacade)
        return facade.GetAllRoles().Select<SearchResultEntry, Role>((Func<SearchResultEntry, Role>) (role => this.BuildRoleWithUserLinks(role, facade))).ToList<Role>().Select<Role, string>((Func<Role, string>) (r => r.Name)).ToArray<string>();
    }

    /// <summary>Gets the role with the specified name.</summary>
    /// <param name="roleName">Name of the role.</param>
    /// <returns></returns>
    public override Role GetRole(string roleName)
    {
      using (ILdapFacade ldapFacade = this.LdapFacade)
      {
        SearchResultEntry role1 = ldapFacade.GetRole(roleName);
        if (role1 == null)
          return (Role) null;
        Role role2 = this.BuildRoleWithUserLinks(role1, ldapFacade);
        ((IDataItem) role2).Provider = (object) this;
        return role2;
      }
    }

    /// <summary>
    /// Gets a value indicating whether the specified role name already
    /// exists in the role data source for the configured applicationName.
    /// </summary>
    /// <param name="roleName">The name of the role to search for in the data source.</param>
    /// <returns>
    /// true if the role name already exists in the data source for the
    /// configured applicationName; otherwise, false.
    /// </returns>
    public override bool RoleExists(string roleName) => this.GetRole(roleName) != null;

    /// <summary>
    /// Gets all users from all providers which are members of the specified role.
    /// </summary>
    /// <param name="roleId">The role ID.</param>
    /// <returns></returns>
    public override IList<User> GetUsersInRole(Guid roleId)
    {
      using (ILdapFacade ldapFacade = this.LdapFacade)
      {
        SearchResultEntry role = ldapFacade.GetRole(roleId);
        return (IList<User>) new List<User>((IEnumerable<User>) new ProviderEnumerable<User>((object) this, ldapFacade.GetUsersFromRoleEntry(role).Select<SearchResultEntry, User>((Func<SearchResultEntry, User>) (u => this.LdapBuilder.Build<User>(u, this.ManagerInfo, this.ApplicationName)))));
      }
    }

    /// <summary>
    /// Gets a value indicating whether the specified user is in the specified role.
    /// </summary>
    /// <param name="userId">The user ID to search for.</param>
    /// <param name="roleName">The role to search in.</param>
    /// <returns>
    /// true if the specified user is in the specified role; otherwise, false.
    /// </returns>
    public override bool IsUserInRole(Guid userId, string roleName)
    {
      using (ILdapFacade ldapFacade = this.LdapFacade)
      {
        SearchResultEntry role = ldapFacade.GetRole(roleName);
        return ldapFacade.IsUserInRole(role, userId);
      }
    }

    /// <summary>
    /// Gets a value indicating whether the specified user is in the specified role.
    /// </summary>
    /// <param name="userId">The user ID to search for.</param>
    /// <param name="roleId">The role to search in.</param>
    /// <returns>
    /// true if the specified user is in the specified role; otherwise, false.
    /// </returns>
    public override bool IsUserInRole(Guid userId, Guid roleId)
    {
      using (ILdapFacade ldapFacade = this.LdapFacade)
      {
        SearchResultEntry role = ldapFacade.GetRole(roleId);
        return ldapFacade.IsUserInRole(role, userId);
      }
    }

    /// <summary>Creates new security role with the specified name.</summary>
    /// <param name="roleName">The role name.</param>
    /// <returns>The new role.</returns>
    public override Role CreateRole(string roleName) => throw new NotSupportedException();

    /// <summary>
    /// Creates new security role with the specified identity and name.
    /// </summary>
    /// <param name="pageId">The identity of the new role.</param>
    /// <param name="roleName">Name of the role.</param>
    /// <returns>The new role.</returns>
    public override Role CreateRole(Guid id, string roleName) => throw new NotSupportedException();

    /// <summary>
    /// Creates new user link for the specified user identity.
    /// </summary>
    /// <param name="pageId">The identity.</param>
    /// <returns></returns>
    public override UserLink CreateUserLink(Guid id) => throw new NotSupportedException();

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The item to delete.</param>
    public override void Delete(Role item) => throw new NotSupportedException();

    /// <summary>Creates new user link.</summary>
    /// <returns></returns>
    public override UserLink CreateUserLink() => throw new NotSupportedException();

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The item to delete.</param>
    public override void Delete(UserLink item) => throw new NotSupportedException();

    /// <summary>Adds the provided user to the provided role.</summary>
    /// <param name="user">The user.</param>
    /// <param name="role">The role.</param>
    public override void AddUserToRole(User user, Role role) => throw new NotSupportedException();

    /// <summary>Removes the provided user from the provided role.</summary>
    /// <param name="userId">The user pageId.</param>
    /// <param name="role">The role.</param>
    public override void RemoveUserFromRole(Guid userId, Role role) => throw new NotSupportedException();

    /// <summary>Removes the provided user from the provided role.</summary>
    /// <param name="user">The user.</param>
    /// <param name="role">The role.</param>
    public override void RemoveUserFromRole(User user, Role role) => throw new NotSupportedException();

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
            ManagerType = typeof (RoleManager).FullName,
            ProviderName = this.Name,
            Id = Guid.NewGuid()
          };
        return this.managerInfo;
      }
    }

    /// <summary>Gets the user manager info.</summary>
    /// <value>The user manager info.</value>
    public ManagerInfo UserManagerInfo
    {
      get
      {
        if (this.userManagerInfo == null)
          this.userManagerInfo = new ManagerInfo()
          {
            ApplicationName = this.ApplicationName,
            ManagerType = typeof (UserManager).FullName,
            ProviderName = this.membershipProviderName,
            Id = Guid.NewGuid()
          };
        return this.userManagerInfo;
      }
    }

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
      this.membershipProviderName = config["membershipProviderName"];
      if (string.IsNullOrEmpty(this.membershipProviderName))
        this.membershipProviderName = "LdapUsers";
      config.Remove("membershipProviderName");
      if (config["connection"] == null)
        return;
      this.ldapConnectionName = config["connection"];
      config.Remove("connection");
    }

    /// <summary>Build Role with userlinks from Search Result Entry</summary>
    /// <param name="roleEntry">Role Search Result Entry</param>
    /// <param name="facade">Ldap Facade</param>
    /// <returns>Role with user links</returns>
    public Role BuildRoleWithUserLinks(SearchResultEntry roleEntry, ILdapFacade facade)
    {
      Role role = this.LdapBuilder.Build<Role>(roleEntry, this.ManagerInfo, this.ApplicationName);
      foreach (SearchResultEntry entry in facade.GetUsersFromRoleEntry(roleEntry))
      {
        UserLink userLink = this.LdapBuilder.Build<UserLink>(entry, this.UserManagerInfo, this.ApplicationName);
        userLink.Role = role;
        role.Users.Add(userLink);
      }
      return role;
    }

    /// <summary>returns all the roles per user</summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public IEnumerable<UserLink> GetUserLinks(Guid userId)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      LdapRoleProvider.\u003C\u003Ec__DisplayClass37_0 cDisplayClass370 = new LdapRoleProvider.\u003C\u003Ec__DisplayClass37_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass370.\u003C\u003E4__this = this;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass370.userId = userId;
      ParameterExpression parameterExpression;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: field reference
      // ISSUE: method reference
      // ISSUE: method reference
      return (IEnumerable<UserLink>) ; //unable to render the statement
    }
  }
}
