// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.UserProfiles.Web.Services.IUserProfileTypesService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ServiceModel;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Modules.UserProfiles.Web.Services.Model;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Modules.UserProfiles.Web.Services
{
  /// <summary>Web services for user profiles management.</summary>
  [ServiceContract]
  public interface IUserProfileTypesService
  {
    /// <summary>
    /// Gets the collection of user profile types and returns the result in JSON format.
    /// </summary>
    /// <param name="userProfilesFilter">Filter expression to be applied.</param>
    /// <returns>A collection context that contains the selected user profile types.</returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "?userProfilesFilter={userProfilesFilter}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&root={root}")]
    CollectionContext<UserProfileTypeViewModel> GetUserProfileTypes(
      string userProfilesFilter,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string root);

    /// <summary>
    /// Gets the collection of user profile types and returns the result in XML format.
    /// </summary>
    /// <param name="pageFilter">Filter expression to be applied.</param>
    /// <returns>A collection context that contains the selected user profile types.</returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/?userProfilesFilter={userProfilesFilter}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&root={root}")]
    CollectionContext<UserProfileTypeViewModel> GetUserProfileTypesInXml(
      string userProfilesFilter,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string root);

    /// <summary>
    /// Deletes an array of pages.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="Ids">An array containing the Ids of the pages to be deleted.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the page.</param>
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "batch/?providerName={providerName}")]
    bool BatchDeleteUserProfileTypes(string[] Ids, string providerName);

    /// <summary>
    /// Deletes an array of pages.
    /// Result is returned in Xml format.
    /// </summary>
    /// <param name="Ids">An array containing the Ids of the pages to be deleted.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the page.</param>
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/batch/?providerName={providerName}")]
    bool BatchDeleteUserProfileTypesInXml(string[] Ids, string providerName);

    /// <summary>
    /// Deletes the user profile type and returns true if it has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="profileTypeId">Id of the user profile type to be deleted.</param>
    /// <param name="providerName">Name of the provider to be used when deleting the user profile type.</param>
    /// <param name="duplicate">if set to <c>true</c> the page will be duplicated.</param>
    /// <returns></returns>
    [OperationContract]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, UriTemplate = "{profileTypeId}/?providerName={providerName}")]
    bool DeleteUserProfileType(string profileTypeId, string providerName);

    /// <summary>
    /// Deletes the user profile type and returns true if it has been deleted; otherwise false.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="profileTypeId">The user profile type id.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the user profile type.</param>
    /// <param name="duplicate">if set to <c>true</c> the page will be duplicated.</param>
    /// <returns></returns>
    [OperationContract]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/{profileTypeId}/?providerName={providerName}&duplicate={duplicate}")]
    bool DeleteUserProfileTypeInXml(string profileTypeId, string providerName, bool duplicate);

    /// <summary>
    /// Gets the single user profile type and returs it in JSON format.
    /// </summary>
    /// <param name="profileTypeId">Id of the user profile type that ought to be retrieved.</param>
    /// <param name="providerName">Name of the provider that is to be used to retrieve the item.</param>
    /// <param name="duplicate">if set to <c>true</c> the page will be duplicated.</param>
    /// <returns>
    /// An instance of CollectionContext that contains the content item to be retrieved.
    /// </returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "{profileTypeId}/?providerName={providerName}&duplicate={duplicate}")]
    UserProfileTypeContext GetUserProfileType(
      string profileTypeId,
      string providerName,
      bool duplicate);

    /// <summary>Gets the single page and returs it in XML format.</summary>
    /// <param name="profileTypeId">Id of the content item that ought to be retrieved.</param>
    /// <param name="providerName">Name of the content provider that is to be used to retrieve the content item.</param>
    /// <param name="duplicate">if set to <c>true</c> the page will be duplicated.</param>
    /// <returns>
    /// An instance of ContentItemContext that contains the content item to be retrieved.
    /// </returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/{profileTypeId}/?providerName={providerName}&duplicate={duplicate}")]
    UserProfileTypeContext GetUserProfileTypeInXml(
      string profileTypeId,
      string providerName,
      bool duplicate);

    /// <summary>Saves the page.</summary>
    /// <param name="profileTypeContext">The page context.</param>
    /// <param name="profileTypeId">The user profile type id.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="duplicate">if set to <c>true</c> the page will be duplicated.</param>
    /// <returns></returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "{profileTypeId}/?providerName={providerName}&duplicate={duplicate}")]
    UserProfileTypeContext SaveUserProfileType(
      UserProfileTypeContext profileTypeDataContext,
      string profileTypeId,
      string providerName,
      bool duplicate);

    /// <summary>Saves the page in XML.</summary>
    /// <param name="content">The content.</param>
    /// <param name="profileTypeId">The user profile id.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="parentTaxonId">The parent taxon id.</param>
    /// <param name="templateId">The template id.</param>
    /// 
    ///             /// <param name="duplicate">if set to <c>true</c> the page will be duplicated.</param>
    /// <returns></returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/{profileTypeId}/?providerName={providerName}&duplicate={duplicate}")]
    UserProfileTypeContext SaveUserProfileTypeInXml(
      UserProfileTypeContext profileTypeDataContext,
      string profileTypeId,
      string providerName,
      bool duplicate);

    /// <summary>Gets all profiles for user.</summary>
    /// <param name="userId">The user id.</param>
    /// <returns></returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{userId}")]
    string GetAllProfilesForUser(string userId);

    /// <summary>Gets all profiles for user in XML.</summary>
    /// <param name="userId">The user id.</param>
    /// <returns></returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/{userId}")]
    string GetAllProfilesForUserInXml(string userId);

    /// <summary>Saves all profiles for user.</summary>
    /// <param name="profileData">The profile data.</param>
    /// <param name="userId">The user id.</param>
    /// <returns></returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "{userId}")]
    string SaveAllProfilesForUser(string profileData, string userId);

    /// <summary>Saves all profiles for user in XML.</summary>
    /// <param name="profileData">The profile data.</param>
    /// <param name="userId">The user id.</param>
    /// <returns></returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/{userId}")]
    string SaveAllProfilesForUserInXml(string profileData, string userId);
  }
}
