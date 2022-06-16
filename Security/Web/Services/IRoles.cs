// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.Services.IRoles
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Security.Web.Services
{
  /// <summary>The WCF web service interface for roles management.</summary>
  [ServiceContract]
  public interface IRoles
  {
    [OperationContract]
    [WebInvoke(Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "countWithLocalChanges/?roleProvider={roleProvider}&roleId={roleId}&filter={filter}&userProvider={userProvider}&forAllProviders={forAllUserProviders}")]
    [ServiceMethodRequirePermission("Backend", "ManageRoles", true)]
    int GetNumOfUsersInRoleWithLocalChanges(
      UserProviderPair[] localChange,
      string roleId,
      string roleProvider,
      string filter,
      string userProvider,
      bool forAllUserProviders);

    [WebInvoke(Method = "PUT", RequestFormat = WebMessageFormat.Xml, ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/countWithLocalChanges/?roleProvider={roleProvider}&roleId={roleId}&filter={filter}&userProvider={userProvider}&forAllProviders={forAllUserProviders}")]
    [OperationContract]
    [ServiceMethodRequirePermission("Backend", "ManageRoles", true)]
    int GetNumOfUsersInRoleWithLocalChangesInXml(
      UserProviderPair[] localChange,
      string roleId,
      string roleProvider,
      string filter,
      string userProvider,
      bool forAllUserProviders);

    /// <summary>Gets the collection of roles in JSON format.</summary>
    /// <param name="provider">The name of the role provider from which the roles should be retrived.</param>
    /// <param name="sortExpression">The sortExpression expression used to order the retrieved roles.</param>
    /// <param name="skip">The number of roles to skip before populating the collection (used primarily for paging).</param>
    /// <param name="take">The maximum number of roles to take in the collection (used primarily for paging).</param>
    /// <param name="filter">The filter expression in dynamic LINQ format used to filter the retrieved roles.</param>
    /// <returns>
    /// Returns CollectionContext object with role entry items and other information about the retrieved collection.
    /// </returns>
    [WebHelp(Comment = "Gets all roles for given roles provider. The results are returned in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    [OperationContract]
    CollectionContext<WcfRole> GetRoles(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter);

    /// <summary>Gets the collection of roles in XML format.</summary>
    /// <param name="provider">The name of the role provider from which the roles should be retrived.</param>
    /// <param name="sortExpression">The sort expression used to order the retrieved roles.</param>
    /// <param name="skip">The number of roles to skip before populating the collection (used primarily for paging).</param>
    /// <param name="take">The maximum number of roles to take in the collection (used primarily for paging).</param>
    /// <param name="filter">The filter expression in dynamic LINQ format used to filter the retrieved roles.</param>
    /// <returns>
    /// Returns CollectionContext object with role entry items and other information about the retrieved collection.
    /// </returns>
    [WebHelp(Comment = "Gets all roles for given roles provider. The results are returned in XML format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    [OperationContract]
    CollectionContext<WcfRole> GetRolesInXml(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter);

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
    [WebHelp(Comment = "Gets all roles for given roles provider. The results are returned in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "membershipProviderCount/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&membershipProvider={membershipProvider}")]
    [OperationContract]
    [ServiceMethodRequirePermission("Backend", "ManageRoles", true)]
    CollectionContext<WcfRole> GetRolesWithUserCountPerMembershipProvider(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string membershipProvider);

    /// <summary>
    /// Gets the role (can be used to check existence of role).
    /// </summary>
    /// <param name="role">The role that should be retrieved.</param>
    /// <param name="provider">The name of the role provider from which the roles should be retrived.</param>
    /// <returns>If the role exists the role name will be returned, otherwise null string.</returns>
    [WebHelp(Comment = "Gets users for given role. The results are returned in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "{roleId}/?provider={provider}")]
    [OperationContract]
    WcfRole GetRole(string roleId, string provider);

    /// <summary>
    /// Gets the role in XML (can be used to check existence of role).
    /// </summary>
    /// <param name="role">The role that should be retrieved.</param>
    /// <param name="provider">The name of the role provider from which the roles should be retrived.</param>
    /// <returns>If the role exists the role name will be returned, otherwise null string.</returns>
    [WebHelp(Comment = "Gets the role. The results are returned in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/{roleId}/?provider={provider}")]
    [OperationContract]
    WcfRole GetRoleInXml(string roleId, string provider);

    /// <summary>Saves the role.</summary>
    /// <param name="propertyBag">The property bag.</param>
    /// <param name="role">The role that should be added.</param>
    /// <param name="provider">The name of the role provider from which the roles should be retrived.</param>
    /// <returns></returns>
    [WebHelp(Comment = "Inserts or updates a user for given membership provider. The results are returned in JSON format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "{roleId}/?provider={provider}")]
    [OperationContract]
    [ServiceMethodRequirePermission("Backend", "ManageRoles", true)]
    WcfRole SaveRole(WcfRole role, string roleId, string provider);

    /// <summary>Saves the role in XML.</summary>
    /// <param name="propertyBag">The property bag.</param>
    /// <param name="role">The role that should be added.</param>
    /// <param name="provider">The name of the role provider from which the roles should be retrived.</param>
    /// <returns></returns>
    [WebHelp(Comment = "Inserts or updates a user for given membership provider. The results are returned in JSON format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/{roleId}?provider={provider}")]
    [OperationContract]
    [ServiceMethodRequirePermission("Backend", "ManageRoles", true)]
    WcfRole SaveRoleInXml(WcfRole role, string roleId, string provider);

    /// <summary>
    /// Deletes the role and returns true if the role has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="role">The role that should be retrieved.</param>
    /// <param name="provider">The name of the role provider from which the roles should be retrived.</param>
    [WebHelp(Comment = "Deletes role for given roles provider. The results are returned in JSON format.")]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, UriTemplate = "{roleId}/?provider={provider}")]
    [OperationContract]
    [ServiceMethodRequirePermission("Backend", "ManageRoles", true)]
    bool DeleteRole(string roleId, string provider);

    /// <summary>
    /// Deletes the role and returns true if the role has been deleted; otherwise false.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="role">The role that should be retrieved.</param>
    /// <param name="provider">The name of the role provider from which the roles should be retrived.</param>
    [WebHelp(Comment = "Deletes role for given roles provider. The results are returned in XML format.")]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/{roleId}/?provider={provider}")]
    [OperationContract]
    [ServiceMethodRequirePermission("Backend", "ManageRoles", true)]
    bool DeleteRoleInXml(string roleId, string provider);

    /// <summary>Gets the role providers.</summary>
    /// <param name="commaSeperatedAbilities">Optional list of required provider abilities to filter, comma seperated</param>
    /// <param name="addAppRoles">Indicates whether to include Sitefinity's application specific roles</param>
    /// <returns>A collection of UserProviderItem items</returns>
    [WebHelp(Comment = "All the active providers with the number of registered roles. The results are returned in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/GetRoleProviders/?abilities={commaSeperatedAbilities}&addAppRoles={addAppRoles}")]
    [OperationContract]
    CollectionContext<RoleProviderItem> GetRoleProviders(
      string commaSeperatedAbilities,
      bool addAppRoles = false);

    /// <summary>Gets the role/users relations in JSON format.</summary>
    /// <param name="roleId">Id of the role for which the role/users relations should be retrieved.</param>
    /// <param name="provider">The name of the role provider from which the role/users relation should be retrieved.</param>
    /// <param name="sortExpression">The sort expression used to order the retrieved role/users relations.</param>
    /// <param name="skip">The number of role/users relations to skip before populating the collection (used primarily for paging).</param>
    /// <param name="take">The maximum number of role/users relations to take in the collection (used primarily for paging).</param>
    /// <param name="filter">The filter expression in dynamic LINQ format used to filter the retrieved role/users relations.</param>
    /// <returns>
    /// Returns CollectionContext object with role/users relations  and other information about the retrieved collection.
    /// </returns>
    [WebHelp(Comment = "Gets all username in given user. The results are returned in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetRoleUsers/{roleId}/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    [OperationContract]
    [ServiceMethodRequirePermission("Backend", "ManageRoles", true)]
    CollectionContext<UserProviderPair> GetRoleUsers(
      string roleId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter);

    /// <summary>Gets the role/users relations in JSON format.</summary>
    /// <param name="roleId">Id of the role for which the role/users relations should be retrieved.</param>
    /// <param name="provider">The name of the role provider from which the role/users relation should be retrieved.</param>
    ///         //
    ///             <returns>Count of users in the specified role</returns>
    [WebHelp(Comment = "Total count of users in role. The results are returned in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "CountRoleUsers/{roleId}/{provider}")]
    [OperationContract]
    [ServiceMethodRequirePermission("Backend", "ManageRoles", true)]
    int GetCountOfUsersInRole(string roleId, string provider);

    /// <summary>Gets the role/users relations in JSON format.</summary>
    /// <param name="roleId">Id of the role for which the role/users relations should be retrieved.</param>
    /// <param name="provider">The name of the role provider from which the role/users relation should be retrieved.</param>
    ///         //
    ///             <returns>Count of users in the specified role</returns>
    [WebHelp(Comment = "Total count of users in role. The results are returned in XML format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "xml/CountRoleUsers/{roleId}/{provider}")]
    [OperationContract]
    [ServiceMethodRequirePermission("Backend", "ManageRoles", true)]
    int GetCountOfUsersInRoleInXml(string roleId, string provider);

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
    /// <returns>Collection context of wcf membership users belonging to a particualr role, with paging.</returns>
    [WebHelp(Comment = "Get all users in a role. The results are returned in JSON format.")]
    [WebInvoke(Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetWcfUsersInRole/{roleId}/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&userProvider={userProvider}&forAllUserProviders={forAllUserProviders}")]
    [OperationContract]
    [ServiceMethodRequirePermission("Backend", "ManageRoles", true)]
    CollectionContext<WcfMembershipUser> GetWcfUsersInRole(
      UserProviderPair[] localChange,
      string roleId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string userProvider,
      bool forAllUserProviders);

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
    /// <param name="localChange">Local (not persisted) change that was made in JavaScript</param>
    /// <param name="userProvider">User provider name</param>
    /// <returns>Collection context of wcf membership users belonging to a particualr role, with paging.</returns>
    [WebHelp(Comment = "Get all users in a role. The results are returned in XML format.")]
    [WebInvoke(Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "xml/GetWcfUsersInRole/{roleId}/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&userProvider={userProvider}&forAllUserProviders={forAllUserProviders}")]
    [OperationContract]
    [ServiceMethodRequirePermission("Backend", "ManageRoles", true)]
    CollectionContext<WcfMembershipUser> GetWcfUsersInRoleInXml(
      UserProviderPair[] localChange,
      string roleId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string userProvider,
      bool forAllUserProviders);

    /// <summary>Gets the role/users relations in XML format.</summary>
    /// <param name="roleId">Id of the role for which the role/users relations should be retrieved.</param>
    /// <param name="provider">The name of the role provider from which the role/users relation should be retrieved.</param>
    /// <param name="sortExpression">The sort expression used to order the retrieved role/users relations.</param>
    /// <param name="skip">The number of role/users relations to skip before populating the collection (used primarily for paging).</param>
    /// <param name="take">The maximum number of role/users relations to take in the collection (used primarily for paging).</param>
    /// <param name="filter">The filter expression in dynamic LINQ format used to filter the retrieved role/users relations.</param>
    /// <returns>
    /// Returns CollectionContext object with role/users relations  and other information about the retrieved collection.
    /// </returns>
    [WebHelp(Comment = "Gets all roles for given user. The results are returned in XML format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "GetRoleUsers/xml/{roleId}/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    [OperationContract]
    [ServiceMethodRequirePermission("Backend", "ManageRoles", true)]
    CollectionContext<UserProviderPair> GetRoleUsersInXml(
      string roleId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter);

    /// <summary>Adds relation between role and user.</summary>
    /// <param name="users">Array of <see cref="T:Telerik.Sitefinity.Security.Web.Services.UserProviderPair" /> objects to be saved.</param>
    /// <param name="roleId">Id of the role.</param>
    /// <param name="provider">The name of role provider.</param>
    /// <returns></returns>
    [WebHelp(Comment = "Adds a relation between a given user and role. The results are returned in JSON format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "SaveRoleUser/{roleId}/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    [OperationContract]
    [ServiceMethodRequirePermission("Backend", "ManageRoles", true)]
    string SaveRoleUser(
      UserProviderPair[] users,
      string roleId,
      string provider,
      string sortExpression,
      string skip,
      string take,
      string filter);

    /// <summary>Adds relation between role and user.</summary>
    /// <param name="users">Array of <see cref="T:Telerik.Sitefinity.Security.Web.Services.UserProviderPair" /> objects to be saved.</param>
    /// <param name="roleId">Id of the role.</param>
    /// <param name="provider">The name of role provider.</param>
    /// <returns></returns>
    [WebHelp(Comment = "Adds a relation between a given user and role. The results are returned in JSON format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "SaveRoleUser/xml/{roleId}/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    [OperationContract]
    [ServiceMethodRequirePermission("Backend", "ManageRoles", true)]
    string SaveRoleUserInXml(
      UserProviderPair[] users,
      string roleId,
      string provider,
      string sortExpression,
      string skip,
      string take,
      string filter);

    /// <summary>
    /// Deletes a role user relation and returns true if the role user has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="roleId">Id of the role.</param>
    /// <param name="userId">Id of the user.</param>
    /// <param name="provider">The name of role provider.</param>
    [WebHelp(Comment = "Deletes relation between given user and role. The results are returned in JSON format.")]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, UriTemplate = "{roleId}/{userId}/?provider={provider}")]
    [OperationContract]
    [ServiceMethodRequirePermission("Backend", "ManageRoles", true)]
    bool DeleteRoleUser(string roleId, string userId, string provider);

    /// <summary>
    /// Deletes a role user relation and returns true if the role user has been deleted; otherwise false.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="roleId">Id of the role.</param>
    /// <param name="userId">Id of the user.</param>
    /// <param name="provider">The name of role provider.</param>
    [WebHelp(Comment = "Deletes relation between given user and role. The results are returned in XML format.")]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/{roleId}/{userId}/?provider={provider}")]
    [OperationContract]
    [ServiceMethodRequirePermission("Backend", "ManageRoles", true)]
    bool DeleteRoleUserInXml(string roleId, string userId, string provider);

    /// <summary>
    /// Recieve a list of user ids and send back the ids of those belonging to the role
    /// </summary>
    /// <param name="roleIdString">ID of the of the role to check against</param>
    /// <param name="provider">Roles provider to use</param>
    /// <param name="userIdsToPickFrom">List of user IDs to pick from</param>
    /// <returns>Total number of users in the role and the IDs of the users that are actually in the role</returns>
    [WebHelp(Comment = "Recieve a list of user ids and send back the ids of those belonging to the role. Returns result in JSON.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "pick/{roleIdString}/?provider={provider}")]
    [OperationContract]
    [ServiceMethodRequirePermission("Backend", "ManageRoles", true)]
    CollectionContext<Guid> PickUsersInRole(
      string roleIdString,
      string provider,
      Guid[] userIdsToPickFrom);

    /// <summary>
    /// Recieve a list of user ids and send back the ids of those belonging to the role
    /// </summary>
    /// <param name="roleIdString">ID of the of the role to check against</param>
    /// <param name="provider">Roles provider to use</param>
    /// <param name="userIdsToPickFrom">List of user IDs to pick from</param>
    /// <returns>Total number of users in the role and the IDs of the users that are actually in the role</returns>
    [WebHelp(Comment = "Recieve a list of user ids and send back the ids of those belonging to the role. Returns result in JSON.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "xml/pick/{roleIdString}/?provider={provider}")]
    [OperationContract]
    [ServiceMethodRequirePermission("Backend", "ManageRoles", true)]
    CollectionContext<Guid> PickUsersInRoleInXml(
      string roleIdString,
      string provider,
      Guid[] userIdsToPickFrom);
  }
}
