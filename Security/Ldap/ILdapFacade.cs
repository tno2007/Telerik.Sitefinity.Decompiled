// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Ldap.ILdapFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using Telerik.Sitefinity.Data.Linq.Ldap;

namespace Telerik.Sitefinity.Security.Ldap
{
  /// <summary>
  ///  This is a simplified interfface used by LDAP providers for calling the LDAP API, that can be abstract the implementation or be used by mocks for unit testing purposes
  /// </summary>
  public interface ILdapFacade : IDisposable
  {
    /// <summary>Get users from specific role distinguished name</summary>
    /// <param name="roleDN">role distinguished name</param>
    /// <returns>collection of search result entries</returns>
    IEnumerable<SearchResultEntry> GetUsersByRoleDN(string roleDN);

    /// <summary>Gets all roles.</summary>
    /// <returns></returns>
    IEnumerable<SearchResultEntry> GetAllRoles();

    /// <summary>
    /// Execute search against ldap server and concatinates user defined role filter
    /// </summary>
    /// <param name="query">ldap query</param>
    /// <returns>collection of search result entries</returns>
    IEnumerable<SearchResultEntry> RoleSearch(LdapQuery query);

    /// <summary>
    /// Execute search against ldap server and concatinates user defined user filter
    /// </summary>
    /// <param name="query">ldap query</param>
    /// <returns>collection of search result entries</returns>
    IEnumerable<SearchResultEntry> UserSearch(LdapQuery query);

    /// <summary>Gets the users from role entry.</summary>
    /// <param name="roleEntry">The role entry.</param>
    /// <returns></returns>
    IEnumerable<SearchResultEntry> GetUsersFromRoleEntry(
      SearchResultEntry roleEntry);

    /// <summary>returns role by pageId</summary>
    /// <param name="roleId"></param>
    /// <returns></returns>
    /// <exception cref="T:Telerik.Sitefinity.Security.Ldap.LdapProviderException"></exception>
    SearchResultEntry GetRole(Guid roleId);

    /// <summary>Get roles by roles distinguished names</summary>
    /// <param name="roleDNs">collection of distinguished names</param>
    /// <returns>collection of roles search result entries</returns>
    IEnumerable<SearchResultEntry> GetRolesFromRoleDns(
      IEnumerable<string> roleDNs);

    /// <summary>Get the user by ID(objectGuid in LDAP)</summary>
    /// <param name="userID">User ID</param>
    /// <param name="attributes">the directory entry attributes to be returned (like LastName etc.)</param>
    /// <returns>a result entry</returns>
    SearchResultEntry GetUserById(Guid userID, params string[] attributes);

    /// <summary>Gets the user of the user userName.</summary>
    /// <param name="userName">Name of the user.</param>
    /// <param name="attributes">The attributes  to be returned</param>
    /// <returns></returns>
    SearchResultEntry GetUserByUsername(
      string userName,
      params string[] attributes);

    /// <summary>Gets the user by email</summary>
    /// <param name="email">email of the user</param>
    /// <param name="attributes">The attributes  to be returned</param>
    /// <returns></returns>
    SearchResultEntry GetUserByEmail(string email, params string[] attributes);

    /// <summary>
    /// Gets all the users from the LDAP , with specified attributes
    /// </summary>
    /// <param name="attributes">the directory entry attributes to be returned (like LastName etc.)</param>
    /// <returns></returns>
    IEnumerable<SearchResultEntry> GetUsers(params string[] attributes);

    /// <summary>Authenticate user agaist Ldap</summary>
    /// <param name="userName">The username.</param>
    /// <param name="password">The password.</param>
    /// <param name="userId">The returned user pageId (objectGUID)</param>
    /// <returns></returns>
    bool AuthenticateUser(string userName, string password, out Guid userId);

    /// <summary>Gets the role  by name</summary>
    /// <param name="roleName">Name of the role.</param>
    /// <returns></returns>
    SearchResultEntry GetRole(string roleName);

    /// <summary>
    /// Determines whether [is user in role] [the specified role entry].
    /// </summary>
    /// <param name="roleEntry">The role entry.</param>
    /// <param name="userId">The user pageId.</param>
    /// <returns>
    /// 	<c>true</c> if [is user in role] [the specified role entry]; otherwise, <c>false</c>.
    /// </returns>
    bool IsUserInRole(SearchResultEntry roleEntry, Guid userId);
  }
}
