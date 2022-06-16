// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Data.SitefinityRoleProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Security;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Security.Data
{
  /// <summary>
  /// Represents data provider for role-management services using XML storage.
  /// </summary>
  public class SitefinityRoleProvider : RoleProvider
  {
    private RoleDataProvider dataProvider;

    /// <summary>
    /// Gets or sets the name of the application to store and retrieve role information for.
    /// </summary>
    /// <returns>
    /// The name of the application to store and retrieve role information for.
    /// </returns>
    public override string ApplicationName
    {
      get => this.DataProvider.ApplicationName;
      set => throw new NotSupportedException();
    }

    /// <summary>Adds the specified user names to the specified roles.</summary>
    /// <param name="roleNames">
    /// A string array of the role names to add the specified user names to.
    /// </param>
    /// <param name="usernames">
    /// A string array of user names to be added to the specified roles.
    /// </param>
    public override void AddUsersToRoles(string[] usernames, string[] roleNames)
    {
      this.DataProvider.SuppressSecurityChecks = true;
      IQueryable<Role> queryable = this.DataProvider.GetRoles().Where<Role>((Expression<Func<Role, bool>>) (r => roleNames.Contains<string>(r.Name)));
      IList<User> users = UserManager.FindUsers(usernames);
      foreach (Role role in (IEnumerable<Role>) queryable)
      {
        foreach (User user in (IEnumerable<User>) users)
          this.DataProvider.AddUserToRole(user, role);
      }
      this.DataProvider.CommitTransaction();
    }

    /// <summary>
    /// Adds a new role to the data source for the configured applicationName.
    /// </summary>
    /// <param name="roleName">The name of the role to create.</param>
    public override void CreateRole(string roleName)
    {
      this.DataProvider.SuppressSecurityChecks = true;
      this.DataProvider.CreateRole(roleName);
    }

    /// <summary>
    /// Removes a role from the data source for the configured applicationName.
    /// </summary>
    /// <returns>
    /// true if the role was successfully deleted; otherwise, false.
    /// </returns>
    /// <param name="throwOnPopulatedRole">
    /// If true, throw an exception if roleName has one or more members and do not delete roleName.
    /// </param>
    /// <param name="roleName">The name of the role to delete.</param>
    public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
    {
      this.DataProvider.SuppressSecurityChecks = true;
      Role role = this.DataProvider.GetRole(roleName);
      if (throwOnPopulatedRole && role.Users.Count > 0)
        throw new ProviderException("Role is not empty.");
      this.DataProvider.Delete(role);
      return true;
    }

    /// <summary>
    /// Gets an array of user names in a role where the user name contains the
    /// specified user name to match.
    /// </summary>
    /// <returns>
    /// A string array containing the names of all the users where the user name
    /// matches usernameToMatch and the user is a member of the specified role.
    /// </returns>
    /// <param name="usernameToMatch">The user name to search for.</param>
    /// <param name="roleName">The role to search in.</param>
    public override string[] FindUsersInRole(string roleName, string usernameToMatch)
    {
      this.DataProvider.SuppressSecurityChecks = true;
      Guid[] userIds = this.DataProvider.GetRole(roleName).Users.Select<UserLink, Guid>((Func<UserLink, Guid>) (l => l.UserId)).ToArray<Guid>();
      List<string> stringList = new List<string>(userIds.Length);
      foreach (MembershipDataProvider contextProvider in UserManager.GetManager().GetContextProviders())
      {
        string[] array = contextProvider.GetUsers().Where<User>((Expression<Func<User, bool>>) (u => u.UserName.Contains(usernameToMatch) && userIds.Contains<Guid>(u.Id))).Select<User, string>((Expression<Func<User, string>>) (u => u.UserName)).ToArray<string>();
        stringList.AddRange((IEnumerable<string>) array);
        if (stringList.Count == userIds.Length)
          break;
      }
      return stringList.ToArray();
    }

    /// <summary>
    /// Gets a list of all the roles for the configured applicationName.
    /// </summary>
    /// <returns>
    /// A string array containing the names of all the roles stored in the data
    /// source for the configured applicationName.
    /// </returns>
    public override string[] GetAllRoles()
    {
      this.DataProvider.SuppressSecurityChecks = true;
      return this.DataProvider.GetRoles().Select<Role, string>((Expression<Func<Role, string>>) (r => r.Name)).ToArray<string>();
    }

    /// <summary>
    /// Gets a list of the roles that a specified user is in for the configured applicationName.
    /// </summary>
    /// <returns>
    /// A string array containing the names of all the roles that the specified
    /// user is in for the configured applicationName.
    /// </returns>
    /// <param name="username">The user to return a list of roles for.</param>
    public override string[] GetRolesForUser(string username)
    {
      this.DataProvider.SuppressSecurityChecks = true;
      Guid id = UserManager.FindUser(username).Id;
      return this.DataProvider.GetUserLinks().Where<UserLink>((Expression<Func<UserLink, bool>>) (l => l.UserId == id)).ToList<UserLink>().Select<UserLink, string>((Func<UserLink, string>) (i => i.Role.Name)).ToArray<string>();
    }

    /// <summary>
    /// Gets a list of users in the specified role for the configured applicationName.
    /// </summary>
    /// <returns>
    /// A string array containing the names of all the users who are members of the
    /// specified role for the configured applicationName.
    /// </returns>
    /// <param name="roleName">
    /// The name of the role to get the list of users for.
    /// </param>
    public override string[] GetUsersInRole(string roleName)
    {
      this.DataProvider.SuppressSecurityChecks = true;
      Guid[] userIds = this.DataProvider.GetRole(roleName).Users.Select<UserLink, Guid>((Func<UserLink, Guid>) (l => l.UserId)).ToArray<Guid>();
      List<string> stringList = new List<string>(userIds.Length);
      foreach (MembershipDataProvider contextProvider in UserManager.GetManager().GetContextProviders())
      {
        string[] array = contextProvider.GetUsers().Where<User>((Expression<Func<User, bool>>) (u => userIds.Contains<Guid>(u.Id))).Select<User, string>((Expression<Func<User, string>>) (u => u.UserName)).ToArray<string>();
        stringList.AddRange((IEnumerable<string>) array);
        if (stringList.Count == userIds.Length)
          break;
      }
      return stringList.ToArray();
    }

    /// <summary>
    /// Gets a value indicating whether the specified user is in the specified
    /// role for the configured applicationName.
    /// </summary>
    /// <returns>
    /// true if the specified user is in the specified role for the
    /// configured applicationName; otherwise, false.
    /// </returns>
    /// <param name="username">The user name to search for.</param>
    /// <param name="roleName">The role to search in.</param>
    public override bool IsUserInRole(string username, string roleName)
    {
      this.DataProvider.SuppressSecurityChecks = true;
      Guid id = UserManager.FindUser(username).Id;
      return this.DataProvider.GetUserLinks().Where<UserLink>((Expression<Func<UserLink, bool>>) (l => l.UserId == id && l.Role.Name == roleName)).Any<UserLink>();
    }

    /// <summary>
    /// Removes the specified user names from the specified roles for the configured applicationName.
    /// </summary>
    /// <param name="roleNames">
    /// A string array of role names to remove the specified user names from.
    /// </param>
    /// <param name="usernames">
    /// A string array of user names to be removed from the specified roles.
    /// </param>
    public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
    {
      this.DataProvider.SuppressSecurityChecks = true;
      IQueryable<Role> queryable = this.DataProvider.GetRoles().Where<Role>((Expression<Func<Role, bool>>) (r => roleNames.Contains<string>(r.Name)));
      IList<User> users = UserManager.FindUsers(usernames);
      foreach (Role role in (IEnumerable<Role>) queryable)
      {
        foreach (User user in (IEnumerable<User>) users)
          this.DataProvider.RemoveUserFromRole(user, role);
      }
      this.DataProvider.CommitTransaction();
    }

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
    public override bool RoleExists(string roleName)
    {
      this.DataProvider.SuppressSecurityChecks = true;
      return this.DataProvider.RoleExists(roleName);
    }

    internal RoleDataProvider DataProvider
    {
      get
      {
        if (this.dataProvider == null)
          this.dataProvider = RoleManager.GetManager().Provider;
        return this.dataProvider;
      }
      set => this.dataProvider = value;
    }
  }
}
