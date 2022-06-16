// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.Services.IUsers
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ServiceModel;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Security.Web.Services
{
  /// <summary>
  /// The WCF web service interface for membership users management.
  /// </summary>
  [ServiceContract]
  public interface IUsers
  {
    /// <summary>
    /// Gets the collection of users in JSON format. Returns the users sorted in ascedning order by their username.
    /// </summary>
    /// <param name="provider">The name of the membership provider from which the users should be retrived.</param>
    /// <param name="sortExpression">The sort expression used to order the retrieved users.</param>
    /// <param name="skip">The number of users to skip before populating the collection (used primarily for paging).</param>
    /// <param name="take">The maximum number of users to take in the collection (used primarily for paging).</param>
    /// <param name="filter">The filter expression in dynamic LINQ format used to filter the retrieved users.</param>
    /// <param name="roleProvider">The role provider.</param>
    /// <param name="role">The role.</param>
    /// <returns>
    /// 	<see cref="T:Telerik.Sitefinity.Web.Services.CollectionContext`1" /> object with user items and other information about the retrieved collection.
    /// </returns>
    [WebHelp(Comment = "Gets all users for given membership provider. The results are returned in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&roleId={roleId}&roleProvider={roleProvider}&forAllProviders={forAllProviders}")]
    [OperationContract]
    [ServiceMethodRequirePermission("Backend", "ManageUsers", true)]
    CollectionContext<WcfMembershipUser> GetUsers(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string roleId,
      string roleProvider,
      bool forAllProviders);

    /// <summary>
    /// Gets the collection of users in JSON format. Returns the users sorted in ascedning order by their username.
    /// </summary>
    /// <param name="provider">The name of the membership provider from which the users should be retrieved.</param>
    /// <param name="sortExpression">The sort expression used to order the retrieved users.</param>
    /// <param name="skip">The number of users to skip before populating the collection (used primarily for paging).</param>
    /// <param name="take">The maximum number of users to take in the collection (used primarily for paging).</param>
    /// <param name="searchText">The text that is used for searching in users.</param>
    /// <param name="roleProvider">The role provider.</param>
    /// <param name="role">The role.</param>
    /// <returns>
    /// 	<see cref="T:Telerik.Sitefinity.Web.Services.CollectionContext`1" /> object with user items and other information about the retrieved collection.
    /// </returns>
    [WebHelp(Comment = "Search in users. The results are returned in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "search/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&searchText={searchText}&roleId={roleId}&roleProvider={roleProvider}&forAllProviders={forAllProviders}")]
    [OperationContract]
    [ServiceMethodRequirePermission("Backend", "ManageUsers", true)]
    CollectionContext<WcfMembershipUser> SearchUsers(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string searchText,
      string roleId,
      string roleProvider,
      bool forAllProviders);

    /// <summary>
    /// Gets the collection of users display info in JSON format. Returns the users sorted in ascedning order by their username.
    /// </summary>
    /// <param name="provider">The name of the membership provider from which the users should be retrived.</param>
    /// <param name="sortExpression">The sort expression used to order the retrieved users.</param>
    /// <param name="skip">The number of users to skip before populating the collection (used primarily for paging).</param>
    /// <param name="take">The maximum number of users to take in the collection (used primarily for paging).</param>
    /// <param name="filter">The filter expression in dynamic LINQ format used to filter the retrieved users.</param>
    /// <param name="roleProvider">The role provider.</param>
    /// <param name="role">The role.</param>
    /// <returns>
    /// 	<see cref="T:Telerik.Sitefinity.Web.Services.CollectionContext`1" /> object with user items collection.
    /// </returns>
    [WebHelp(Comment = "Gets all users for given membership provider. The results are returned in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/display/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&roleId={roleId}&roleProvider={roleProvider}&forAllProviders={forAllProviders}")]
    [OperationContract]
    CollectionContext<WcfMembershipUserDisplayInfo> GetUsersDisplayInfo(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string roleId,
      string roleProvider,
      bool forAllProviders);

    /// <summary>Get all users, not filtering them by role</summary>
    /// <param name="usersProvider">Name of the users provider to use</param>
    /// <param name="forAllUserProviders">True to combine the result of all user providers, false to use the one specified by <paramref name="usersProvider" /></param>
    /// <param name="sort">Sort expression</param>
    /// <param name="filter">Filter expression</param>
    /// <param name="skip">Used for paging. Start taking items from that number of items.</param>
    /// <param name="take">Used for paging. Take the first x items, starting from <paramref name="skip" /></param>
    /// <returns>Returns all users in a provider or all providers.</returns>
    [WebHelp(Comment = "Get all users, not filtering them by role. Returns in JSON")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "all/?provider={usersProvider}&forAllProviders={forAllUserProviders}&sort={sort}&take={take}&skip={skip}&filter={filter}&roleProvider={roleProviderName}&roleId={roleIdString}")]
    [OperationContract]
    [ServiceMethodRequirePermission("Backend", "ManageUsers", true)]
    WcfMembershipUserCollectionContext GetAllUsers(
      string usersProvider,
      bool forAllUserProviders,
      string sort,
      string filter,
      int skip,
      int take,
      string roleProviderName,
      string roleIdString);

    /// <summary>Get all users, not filtering them by role</summary>
    /// <param name="usersProvider">Name of the users provider to use</param>
    /// <param name="forAllUserProviders">True to combine the result of all user providers, false to use the one specified by <paramref name="usersProvider" /></param>
    /// <param name="sort">Sort expression</param>
    /// <param name="filter">Filter expression</param>
    /// <param name="skip">Used for paging. Start taking items from that number of items.</param>
    /// <param name="take">Used for paging. Take the first x items, starting from <paramref name="skip" /></param>
    /// <returns>Returns all users in a provider or all providers.</returns>
    [WebHelp(Comment = "Get all users, not filtering them by role. Returns in XML.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/all/?provider={usersProvider}&forAllProviders={forAllUserProviders}&sort={sort}&take={take}&skip={skip}&filter={filter}&roleProvider={roleProviderName}&roleId={roleIdString}")]
    [OperationContract]
    [ServiceMethodRequirePermission("Backend", "ManageUsers", true)]
    WcfMembershipUserCollectionContext GetAllUsersInXml(
      string usersProvider,
      bool forAllUserProviders,
      string sort,
      string filter,
      int skip,
      int take,
      string roleProviderName,
      string roleIdString);

    /// <summary>
    /// Gets the collection of users in XML format. Returns the users sorted in ascedning order by their username.
    /// </summary>
    /// <param name="provider">The name of the membership provider from which the users should be retrived.</param>
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
    [WebHelp(Comment = "Gets a collection of all resources, with an option to retrieve all items for given culture or for given culture and class id. The results are returned in XML format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&roleId={roleId}&roleProvider={roleProvider}&fromAllProviders={forAllProviders}")]
    [OperationContract]
    [ServiceMethodRequirePermission("Backend", "ManageUsers", true)]
    CollectionContext<WcfMembershipUser> GetUsersInXml(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string roleId,
      string roleProvider,
      bool forAllProviders);

    /// <summary>Gets the user.</summary>
    /// <param name="userId">Id of the user to be retrieved.</param>
    /// <param name="provider">The name of membership provider.</param>
    /// <returns></returns>
    [WebHelp(Comment = "Gets a user for given membership provider by username. The results are returned in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "{userId}/?provider={provider}")]
    [OperationContract]
    WcfMembershipUser GetUser(string userId, string provider);

    /// <summary>Gets the user in XML.</summary>
    /// <param name="userId">Id of the user to be retrieved.</param>
    /// <param name="provider">The name of membership provider.</param>
    /// <returns></returns>
    [WebHelp(Comment = "Gets a user for given membership provider by username. The results are returned in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/{userId}/?provider={provider}")]
    [OperationContract]
    [ServiceMethodRequirePermission("Backend", "ManageUsers", true)]
    WcfMembershipUser GetUserInXml(string userId, string provider);

    /// <summary>
    /// Gets all the active providers with the number of registered users, optionally filtered by role. The results are returned in JSON format
    /// </summary>
    /// <param name="roleId">Optional Id of the role to filter</param>
    /// <param name="roleProvider">Provider of the role, related to the roleId</param>
    /// <returns>A collection of UserProviderItem items</returns>
    [WebHelp(Comment = "Gets all the active providers with the number of registered users, optionally filtered by role. The results are returned in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/GetUserProviders/?roleId={roleId}&roleProvider={roleProvider}&userFilter={userFilter}")]
    [OperationContract]
    CollectionContext<UserProviderItem> GetUserProviders(
      string roleId,
      string roleProvider,
      string userFilter);

    /// <summary>Gets the logged in users count.</summary>
    /// <param name="provider">The provider name.</param>
    /// <returns></returns>
    [WebHelp(Comment = "Gets logged users count. The result are returned in JSON format..")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/GetLoggedInUsersCount/?provider={provider}")]
    [OperationContract]
    int GetLoggedInUsersCount(string provider);

    /// <summary>
    /// Inserts/Updates the user information. The update user information is returned in JSON.
    /// </summary>
    /// <param name="user">User object to be created.</param>
    /// <param name="userId">Id of the user to be created.</param>
    /// <param name="provider">The name of membership provider.</param>
    /// <returns></returns>
    [WebHelp(Comment = "Inserts or updates a user for given membership provider. The results are returned in JSON format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/create/{userId}/?provider={provider}")]
    [OperationContract]
    [ServiceMethodRequirePermission("Backend", "ManageUsers", true)]
    WcfMembershipUser CreateUser(
      WcfMembershipUser user,
      string userId,
      string provider);

    /// <summary>
    /// Inserts/Updates the user information. The update user information is returned in XML.
    /// </summary>
    /// <param name="user">User object to be created.</param>
    /// <param name="userId">Id of the user to be created.</param>
    /// <param name="provider">The name of membership provider.</param>
    /// <returns></returns>
    [WebHelp(Comment = "Inserts or updates a user for given membership provider. The results are returned in JSON format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/create/{userId}/?provider={provider}")]
    [OperationContract]
    [ServiceMethodRequirePermission("Backend", "ManageUsers", true)]
    WcfMembershipUser CreateUserInXml(
      WcfMembershipUser user,
      string userId,
      string provider);

    /// <summary>
    /// Inserts/Updates the user information. The update user information is returned in JSON.
    /// </summary>
    /// <param name="user">User object to be updated.</param>
    /// <param name="userId">Id of the user to be updated.</param>
    /// <param name="provider">The name of membership provider.</param>
    /// <returns></returns>
    [WebHelp(Comment = "Inserts or updates a user for given membership provider. The results are returned in JSON format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/update/{userId}/?provider={provider}")]
    [OperationContract]
    [ServiceMethodRequirePermission("Backend", "ManageUsers", true)]
    WcfMembershipUser UpdateUser(
      WcfMembershipUser user,
      string userId,
      string provider);

    [WebHelp(Comment = "Force loggof user. Returns true if there is a need to redirect to login page. The results are returned in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/ForceLogout/?userId={userId}&provider={provider}")]
    [OperationContract]
    bool ForceLogout(string userId, string provider);

    /// <summary>
    /// Updates the user's basic information, without affecting the user's roles. The update user information is returned in JSON.
    /// </summary>
    /// <param name="user">User object to be updated.</param>
    /// <param name="userId">Id of the user to be updated.</param>
    /// <param name="provider">The name of membership provider.</param>
    /// <returns></returns>
    [WebHelp(Comment = "Inserts or updates a user for given membership provider. The results are returned in JSON format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/updateBasicInfo/{userId}/?provider={provider}")]
    [OperationContract]
    [ServiceMethodRequirePermission("Backend", "ManageUsers", true)]
    WcfMembershipUser UpdateUserBasic(
      WcfMembershipUser user,
      string userId,
      string provider);

    /// <summary>
    /// Inserts/Updates the user information. The update user information is returned in XML.
    /// </summary>
    /// <param name="user">User object to be updated.</param>
    /// <param name="userId">Id of the user to be updated.</param>
    /// <param name="provider">The name of membership provider.</param>
    /// <returns></returns>
    [WebHelp(Comment = "Inserts or updates a user for given membership provider. The results are returned in JSON format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/update/{userId}/?provider={provider}")]
    [OperationContract]
    [ServiceMethodRequirePermission("Backend", "ManageUsers", true)]
    WcfMembershipUser UpdateUserInXml(
      WcfMembershipUser user,
      string userId,
      string provider);

    /// <summary>
    /// Deletes the user by username and returns true if the user has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="userId">Id of the user to be deleted.</param>
    /// <param name="provider">The name of membership provider.</param>
    [WebHelp(Comment = "Deletes user for given membership provider and supplied username. The results are returned in JSON format.")]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, UriTemplate = "{userId}/?provider={provider}")]
    [OperationContract]
    [ServiceMethodRequirePermission("Backend", "ManageUsers", true)]
    bool DeleteUser(string userId, string provider);

    /// <summary>
    /// Deletes the user by username and returns true if the user has been deleted; otherwise false.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="userId">Id of the user to be deleted.</param>
    /// <param name="provider">The name of membership provider.</param>
    [WebHelp(Comment = "Deletes user for given membership provider and supplied username. The results are returned in XML format.")]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/{userId}/?provider={provider}")]
    [OperationContract]
    [ServiceMethodRequirePermission("Backend", "ManageUsers", true)]
    bool DeleteUserInXml(string userId, string provider);

    /// <summary>Gets the user/roles relations in JSON format.</summary>
    /// <param name="userProviderName">The membership provider name of the user.</param>
    /// <param name="userName">The name of the user from which the user/roles relation should be retrived.</param>
    /// <param name="provider">The name of the role provider from which the user/roles relation should be retrived.</param>
    /// <param name="sortExpression">The sort expression used to order the retrieved user/roles relations.</param>
    /// <param name="skip">The number of user/roles relations to skip before populating the collection (used primarily for paging).</param>
    /// <param name="take">The maximum number of user/roles relations to take in the collection (used primarily for paging).</param>
    /// <param name="filter">The filter expression in dynamic LINQ format used to filter the retrieved user/roles relations.</param>
    /// <returns>
    /// Returns CollectionContext object with user/roles relations and other information about the retrieved collection.
    /// </returns>
    [WebHelp(Comment = "Gets all roles for given user. The results are returned in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "{userProviderName}/{userName}/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    [OperationContract]
    [ServiceMethodRequirePermission("Backend", "ManageUsers", true)]
    CollectionContext<RoleProviderPair> GetUserRoles(
      string userProviderName,
      string userName,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter);

    /// <summary>Gets the user/roles relations in XML format.</summary>
    /// <param name="userProviderName">The membership provider name of the user.</param>
    /// <param name="userName">The name of the user from which the user/roles relation should be retrived.</param>
    /// <param name="provider">The name of the role provider from which the user/roles relation should be retrived.</param>
    /// <param name="sortExpression">The sort expression used to order the retrieved user/roles relations.</param>
    /// <param name="skip">The number of user/roles relations to skip before populating the collection (used primarily for paging).</param>
    /// <param name="take">The maximum number of user/roles relations to take in the collection (used primarily for paging).</param>
    /// <param name="filter">The filter expression in dynamic LINQ format used to filter the retrieved user/roles relations.</param>
    /// <returns>
    /// Returns CollectionContext object with user/roles relations and other information about the retrieved collection.
    /// </returns>
    [WebHelp(Comment = "Gets all roles for given user. The results are returned in XML format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/{userProviderName}/{userName}/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    [OperationContract]
    [ServiceMethodRequirePermission("Backend", "ManageUsers", true)]
    CollectionContext<RoleProviderPair> GetUserRolesInXml(
      string userProviderName,
      string userName,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter);

    /// <summary>Adds user/role. Result is returned in JSON.</summary>
    /// <param name="usersRoles">The users roles.</param>
    [WebHelp(Comment = "Adds a relation between a given user and role. The results are returned in JSON format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/SaveUserRole/")]
    [OperationContract]
    [ServiceMethodRequirePermission("Backend", "ManageUsers", true)]
    void SaveUserRole(UserRolesItem[] usersRoles);

    /// <summary>Adds user/role. Result is returned in XML.</summary>
    /// <param name="usersRoles">The users roles.</param>
    [WebHelp(Comment = "Adds a relation between a given user and role. The results are returned in JSON format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/SaveUserRole/xml/")]
    [OperationContract]
    [ServiceMethodRequirePermission("Backend", "ManageUsers", true)]
    void SaveUserRoleInXml(UserRolesItem[] usersRoles);

    /// <summary>
    /// Deletes a user/role relation and returns true if the user/role relation has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="userProviderName">The membership provider name of the user.</param>
    /// <param name="userName">The username.</param>
    /// <param name="roleId">The role pageId.</param>
    /// <param name="roleProviderName">Name of the role provider.</param>
    /// <returns></returns>
    [WebHelp(Comment = "Deletes relation between given user and role. The results are returned in JSON format.")]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, UriTemplate = "{userProviderName}/{userName}/{roleId}/?roleProviderName={roleProviderName}")]
    [OperationContract]
    [ServiceMethodRequirePermission("Backend", "ManageUsers", true)]
    bool DeleteUserRole(
      string userProviderName,
      string userName,
      string roleId,
      string roleProviderName);

    /// <summary>
    /// Deletes a user/role relation and returns true if the user/role relation has been deleted; otherwise false.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="userProviderName">The membership provider name of the user.</param>
    /// <param name="userName">The username.</param>
    /// <param name="roleId">The role pageId.</param>
    /// <param name="roleProviderName">Name of the role provider.</param>
    /// <returns></returns>
    [WebHelp(Comment = "Deletes relation between given user and role. The results are returned in XML format.")]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/{userProviderName}/{userName}/{roleId}/?roleProviderName={roleProviderName}")]
    [OperationContract]
    [ServiceMethodRequirePermission("Backend", "ManageUsers", true)]
    bool DeleteUserRoleInXml(
      string userProviderName,
      string userName,
      string roleId,
      string roleProviderName);

    /// <summary>
    /// Sets authentication cookies to the current request if the provided credentials are valid.
    /// </summary>
    /// <param name="credentials">The credentials.</param>
    /// <returns></returns>
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/Authenticate")]
    [WebHelp(Comment = "Sets authentication cookies to the current request if the provided credentials are valid.")]
    UserLoggingReason AuthenticateUser(Credentials credentials);

    /// <summary>Logs out the user making the current request.</summary>
    [OperationContract]
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/Logout")]
    [WebHelp(Comment = "Logs out the user making the current request.")]
    bool Logout();

    /// <summary>
    /// Logs out the user with the specified credentials.
    /// This request doesn't have to be authenticated but the credentials must be valid.
    /// </summary>
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/LogoutCredentials")]
    [WebHelp(Comment = "Logs out the user with the specified credentials.")]
    bool LogoutWithCredentials(Credentials credentials);

    /// <summary>Logs out the specified user.</summary>
    /// <param name="providerName">The name of the membership provider.</param>
    /// <param name="userName">The username of the user to logout.</param>
    /// <returns></returns>
    [OperationContract]
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/Logout/{providerName}/{userName}")]
    [WebHelp(Comment = "Logs out the specified user.")]
    bool LogoutUser(string providerName, string userName);
  }
}
