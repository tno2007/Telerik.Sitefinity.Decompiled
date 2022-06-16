// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Content.Web.Services.IContentService`2
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Data.WcfHelpers;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Services.Content.Data;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Services.Content.Web.Services
{
  /// <summary>
  /// The WCF web service that is used to work with all types that inherit from base <see cref="N:Telerik.Sitefinity.Services.Content" />
  /// class.
  /// </summary>
  [ServiceContract(Namespace = "ContentService")]
  [AllowDynamicFields]
  public interface IContentService<TContent, TContentViewModel>
    where TContent : Telerik.Sitefinity.GenericContent.Model.Content
    where TContentViewModel : ContentViewModelBase
  {
    /// <summary>
    /// Gets the single content item and returns it in JSON format.
    /// </summary>
    /// <param name="contentId">Id of the content item that ought to be retrieved.</param>
    /// <param name="providerName">Name of the content provider that is to be used to retrieve the content item.</param>
    /// <param name="version">The version id. If not specified returns the current draft version, otherwise the specified version from the history</param>
    /// <param name="published">True if only published content is desired</param>
    /// <param name="checkOut">True to check out for the currently logged in user</param>
    /// <param name="workflowOperation">The workflow operation.</param>
    /// <param name="duplicate">Indicates whether the service should return a duplicate item.</param>
    /// <returns>
    /// An instance of ContentItemContext that contains the content item to be retrieved.
    /// If duplicate is set to true, a new blank item with properties duplicated from the original item is returned.
    /// </returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{contentId}/?provider={providerName}&version={version}&published={published}&checkOut={checkOut}&workflowOperation={workflowOperation}&duplicate={duplicate}")]
    ContentItemContext<TContent> GetContent(
      string contentId,
      string providerName,
      string version,
      bool published,
      bool checkOut,
      string workflowOperation,
      bool duplicate);

    /// <summary>
    /// Gets the single content item and returns it in XML format.
    /// </summary>
    /// <param name="contentId">Id of the content item that ought to be retrieved.</param>
    /// <param name="providerName">Name of the content provider that is to be used to retrieve the content item.</param>
    /// <param name="version">The version id. If not specified returns the current draft version, otherwise the specified version from the history</param>
    /// <param name="published">True if only published content is desired</param>
    /// <param name="checkOut">True to check out for the currently logged in user</param>
    /// <param name="workflowOperation">The workflow operation.</param>
    /// <param name="duplicate">Indicates whether the service should return a duplicate item.</param>
    /// <returns>
    /// An instance of ContentItemContext that contains the content item to be retrieved.
    /// If duplicate is set to true, a new blank item with properties duplicated from the original item is returned.
    /// </returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/{contentId}/?provider={providerName}&version={version}&published={published}&checkOut={checkOut}&workflowOperation={workflowOperation}&duplicate={duplicate}")]
    ContentItemContext<TContent> GetContentInXml(
      string contentId,
      string providerName,
      string version,
      bool published,
      bool checkOut,
      string workflowOperation,
      bool duplicate);

    /// <summary>Gets the single content item in live state.</summary>
    /// <param name="contentId">Id of the content item that ought to be retrieved.</param>
    /// <param name="providerName">Name of the content provider that is to be used to retrieve the content item.</param>
    /// <returns>An instance of ContentItemContext that contains the content item to be retrieved.</returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/live/{contentId}/?provider={providerName}")]
    ContentItemContext<TContent> GetLiveContent(
      string contentId,
      string providerName);

    /// <summary>Gets the single content item in live state.</summary>
    /// <param name="contentId">Id of the content item that ought to be retrieved.</param>
    /// <param name="providerName">Name of the content provider that is to be used to retrieve the content item.</param>
    /// <returns>An instance of ContentItemContext that contains the content item to be retrieved.</returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/live/{contentId}/?provider={providerName}")]
    ContentItemContext<TContent> GetLiveContentInXml(
      string contentId,
      string providerName);

    /// <summary>
    /// Gets a single child content item and returns it in JSON format.
    /// </summary>
    /// <param name="parentId">Id of the parent content item.</param>
    /// <param name="contentId">Id of the content item to be retrieved.</param>
    /// <param name="providerName">Name of the content provider that is to be used to retrieved the child content item.</param>
    /// <param name="newParentId">The new parent id.</param>
    /// <param name="version">The version id. If not specified returns the current draft version, otherwise the specified version from the history</param>
    /// <param name="published">True if only published content is desired</param>
    /// <param name="checkOut">True to check out for the currently logged in user</param>
    /// <param name="workflowOperation">The workflow operation.</param>
    /// <param name="duplicate">Indicates whether the service should return a duplicate item.</param>
    /// <returns>An instance of ContentItemContext that contains the content item to be retrieved.</returns>
    [OperationContract]
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/parent/{parentId}/{contentId}/?provider={providerName}&newParentId={newParentId}&version={version}&published={published}&checkOut={checkOut}&workflowOperation={workflowOperation}&duplicate={duplicate}")]
    ContentItemContext<TContent> GetChildContent(
      string parentId,
      string contentId,
      string providerName,
      string newParentId,
      string version,
      bool published,
      bool checkOut,
      string workflowOperation,
      bool duplicate);

    /// <summary>
    /// Gets a single child content item and returns it in XML format.
    /// </summary>
    /// <param name="parentId">Id of the parent content item.</param>
    /// <param name="contentId">Id of the content item to be retrieved.</param>
    /// <param name="providerName">Name of the content provider that is to be used to retrieve the child content item.</param>
    /// <param name="newParentId">The new parent id.</param>
    /// <param name="version">The version id. If not specified returns the current draft version, otherwise the specified version from the history</param>
    /// <param name="published">True if only published content is desired</param>
    /// <param name="checkOut">True to check out for the currently logged in user</param>
    /// <param name="workflowOperation">The workflow operation.</param>
    /// <param name="duplicate">Indicates whether the service should return a duplicate item.</param>
    /// <returns>An instance of ContentItemContext that contains the content item to be retrieved.</returns>
    [OperationContract]
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/parent/{parentId}/{contentId}/?provider={providerName}&newParentId={newParentId}&version={version}&published={published}&checkOut={checkOut}&workflowOperation={workflowOperation}&duplicate={duplicate}")]
    ContentItemContext<TContent> GetChildContentInXml(
      string parentId,
      string contentId,
      string providerName,
      string newParentId,
      string version,
      bool published,
      bool checkOut,
      string workflowOperation,
      bool duplicate);

    /// <summary>
    /// Saves the content item and returns the saved content item in JSON format. If the content item
    /// with the specified Id exists the content item will be updates; otherwise new content item will
    /// be created.
    /// </summary>
    /// <param name="content">The content object to be saved.</param>
    /// <param name="contentId">The Id of the content item to be saved.</param>
    /// <param name="providerName">Name of the content provider that is to be used to save the content item.</param>
    /// <param name="version">Ignored.</param>
    /// <param name="published">Ignored.</param>
    /// <param name="checkOut">Ignored.</param>
    /// <param name="workflowOperation">The workflow operation used for the content item to be saved.</param>
    /// <returns>An instance of ContentItemContext that contains the content item that was saved.</returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{contentId}/?provider={providerName}&version={version}&published={published}&checkOut={checkOut}&workflowOperation={workflowOperation}")]
    ContentItemContext<TContent> SaveContent(
      ContentItemContext<TContent> content,
      string contentId,
      string providerName,
      string version,
      bool published,
      bool checkOut,
      string workflowOperation);

    /// <summary>
    /// Saves the content item and returns the saved content item in XML format. If the content item
    /// with the specified Id exists the content item will be updated; otherwise a new content item will
    /// be created.
    /// </summary>
    /// <param name="content">The content object to be saved.</param>
    /// <param name="contentId">The Id of the content item to be saved.</param>
    /// <param name="providerName">Name of the content provider that is to be used to save the content item.</param>
    /// <param name="version">Ignored.</param>
    /// <param name="published">Ignored.</param>
    /// <param name="checkOut">Ignored.</param>
    /// <param name="workflowOperation">The workflow operation used for the content item to be saved.</param>
    /// <returns>An instance of ContentItemContext that contains the content item that was saved.</returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/{contentId}/?provider={providerName}&version={version}&published={published}&checkOut={checkOut}&workflowOperation={workflowOperation}")]
    ContentItemContext<TContent> SaveContentInXml(
      ContentItemContext<TContent> content,
      string contentId,
      string providerName,
      string version,
      bool published,
      bool checkOut,
      string workflowOperation);

    /// <summary>
    /// Saves the child content item and returns the saved child content item in JSON format. If the child
    /// content item with the specified pageId exists the content item will be updated; otherwise a new child
    /// content item will be created.
    /// </summary>
    /// <param name="content">The content object to be saved.</param>
    /// <param name="parentId">Id of the parent content item.</param>
    /// <param name="contentId">Id of the content item to be saved.</param>
    /// <param name="checkOut">Ignored.</param>
    /// <param name="published">Ignored.</param>
    /// <param name="version">Ignored.</param>
    /// <param name="providerName">Name of the content provider that is to be used to save the child content item.</param>
    /// <returns>An instance of ContentItemContext that contains the content item that was saved.</returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/parent/{parentId}/{contentId}/?provider={providerName}&newParentId={newParentId}&version={version}&published={published}&checkOut={checkOut}&workflowOperation={workflowOperation}")]
    ContentItemContext<TContent> SaveChildContent(
      ContentItemContext<TContent> content,
      string parentId,
      string contentId,
      string providerName,
      string newParentId,
      string version,
      bool published,
      bool checkOut,
      string workflowOperation);

    /// <summary>
    /// Saves the child content item and returns the saved child content item in XML format. If the child
    /// content item with the specified pageId exists the content item will be updated; otherwise a new child
    /// content item will be created.
    /// </summary>
    /// <param name="content">The content object to be saved.</param>
    /// <param name="parentId">Id of the parent content item.</param>
    /// <param name="contentId">Id of the content item to be saved.</param>
    /// <param name="checkOut">Ignored.</param>
    /// <param name="published">Ignored.</param>
    /// <param name="version">Ignored.</param>
    /// <param name="providerName">Name of the content provider that is to be used to save the child content item.</param>
    /// <returns>An instance of ContentItemContext that contains the content item that was saved.</returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/parent/{parentId}/{contentId}/?provider={providerName}&newParentId={newParentId}&version={version}&published={published}&checkOut={checkOut}&workflowOperation={workflowOperation}")]
    ContentItemContext<TContent> SaveChildContentInXml(
      ContentItemContext<TContent> content,
      string parentId,
      string contentId,
      string providerName,
      string newParentId,
      string version,
      bool published,
      bool checkOut,
      string workflowOperation);

    /// <summary>
    /// Gets the collection of content item summary objects and returns them in JSON format.
    /// </summary>
    /// <param name="sortExpression">Sort expression used to order the content items in the result set.</param>
    /// <param name="skip">The number of items to skip before starting to take the items in the result set.</param>
    /// <param name="take">The number of items to take into the result set.</param>
    /// <param name="filter">Filter expression to be used to filter the items that will be taken into the result set.</param>
    /// <param name="providerName">Name of the content provider to be used to get the content items.</param>
    /// <returns>An instance of CollectionContext object that contains the collection of ContentSummary items.</returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/?sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&provider={providerName}&workflowOperation={workflowOperation}")]
    CollectionContext<TContentViewModel> GetContentItems(
      string sortExpression,
      int skip,
      int take,
      string filter,
      string providerName,
      string workflowOperation);

    /// <summary>
    /// Gets the collection of content item summary objects and returns them in XML format.
    /// </summary>
    /// <param name="sortExpression">Sort expression used to order the content items in the result set.</param>
    /// <param name="skip">The number of items to skip before starting to take the items in the result set.</param>
    /// <param name="take">The number of items to take into the result set.</param>
    /// <param name="filter">Filter expression to be used to filter the items that will be taken into the result set.</param>
    /// <param name="providerName">Name of the content provider to be used to get the content items.</param>
    /// <returns>An instance of CollectionContext object that contains the collection of ContentSummary items.</returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/?sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&provider={providerName}&workflowOperation={workflowOperation}")]
    CollectionContext<TContentViewModel> GetContentItemsInXml(
      string sortExpression,
      int skip,
      int take,
      string filter,
      string providerName,
      string workflowOperation);

    /// <summary>
    /// Gets the collection of child content item summary objects and returns them in JSON format.
    /// </summary>
    /// <param name="parentId">The if of the parent content item for which the children ought to be retrieved.</param>
    /// <param name="providerName">Name of the content provider to be used to get the child content items.</param>
    /// <param name="sortExpression">Sort expression used to order the child content items in the result set.</param>
    /// <param name="filter">Filter expression to be used to filter the items that will be taken into the results set.</param>
    /// <param name="skip">The number of items to skip before starting to take the items in the result set.</param>
    /// <param name="take">The number of items to take into the result set.</param>
    /// <param name="excludeFolders">Default value is true. If set to false gets only the specific content items, else gets the media content items and the folders of the same level.</param>
    /// <param name="includeSubFoldersItems">Default value is false. If set to true then gets all content items including those in the library subfolders.</param>
    /// <returns>An instance of CollectionContext object that contains the collection of ContentSummary items.</returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/parent/{parentId}/?provider={providerName}&sortExpression={sortExpression}&filter={filter}&skip={skip}&take={take}&workflowOperation={workflowOperation}&excludeFolders={excludeFolders}&includeSubFoldersItems={includeSubFoldersItems}")]
    CollectionContext<TContentViewModel> GetChildrenContentItems(
      string parentId,
      string providerName,
      string sortExpression,
      string filter,
      int skip,
      int take,
      string workflowOperation,
      bool excludeFolders,
      bool includeSubFoldersItems);

    /// <summary>
    /// Gets the collection of child content item summary objects and returns them in XML format.
    /// </summary>
    /// <param name="parentId">The if of the parent content item for which the children ought to be retrieved.</param>
    /// <param name="providerName">Name of the content provider to be used to get the child content items.</param>
    /// <param name="sortExpression">Sort expression used to order the child content items in the result set.</param>
    /// <param name="filter">Filter expression to be used to filter the items that will be taken into the results set.</param>
    /// <param name="skip">The number of items to skip before starting to take the items in the result set.</param>
    /// <param name="take">The number of items to take into the result set.</param>
    /// <param name="excludeFolders">Default value is true. If set to false gets only the specific content items, else gets the media content items and the folders of the same level.</param>
    /// <param name="includeSubFoldersItems">Default value is false. If set to true then gets all content items including those in the library subfolders.</param>
    /// <returns>An instance of CollectionContext object that contains the collection of ContentSummary items.</returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/parent/{parentId}/?provider={providerName}&sortExpression={sortExpression}&filter={filter}&skip={skip}&take={take}&workflowOperation={workflowOperation}&excludeFolders={excludeFolders}&includeSubFoldersItems={includeSubFoldersItems}")]
    CollectionContext<TContentViewModel> GetChildrenContentItemsInXml(
      string parentId,
      string providerName,
      string sortExpression,
      string filter,
      int skip,
      int take,
      string workflowOperation,
      bool excludeFolders,
      bool includeSubFoldersItems);

    /// <summary>
    /// Deletes the content item and returns true if the content item has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="Id">Id of the content item to be deleted.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the item.</param>
    /// <param name="checkOut">Ignored.</param>
    /// <param name="published">Ignored.</param>
    /// <param name="deletedLanguage">The language version to delete. If null, all versions are deleted(e.g. the item itself is deleted).</param>
    [OperationContract]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{contentId}/?provider={providerName}&version={version}&published={published}&checkOut={checkOut}&workflowOperation={workflowOperation}&language={deletedLanguage}&checkRelatingData={checkRelatingData}")]
    bool DeleteContent(
      string contentId,
      string providerName,
      string version,
      bool published,
      bool checkOut,
      string workflowOperation,
      string deletedLanguage,
      bool checkRelatingData);

    /// <summary>
    /// Deletes the temp of a content item with <paramref name="contentId" />
    /// </summary>
    /// <param name="contentId">String that is parsalbe by the Guid constructor. Used for designating the content ID whose temp to delete (or the temp's ID itself)</param>
    /// <param name="providerName">Name of the provider to use</param>
    /// <param name="force">Force deletion of temp (e.g. when not owner of temp, but admin)</param>
    /// <returns>True if it were deleted, false otherwize</returns>
    [OperationContract]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/temp/{contentId}/?provider={providerName}&force={force}&workflowOperation={workflowOperation}")]
    bool DeleteTemp(string contentId, string providerName, bool force, string workflowOperation);

    /// <summary>
    /// Deletes the temp of a content item with <paramref name="contentId" />
    /// </summary>
    /// <param name="contentId">String that is parsalbe by the Guid constructor. Used for designating the content ID whose temp to delete (or the temp's ID itself)</param>
    /// <param name="providerName">Name of the provider to use</param>
    /// <param name="force">Force deletion of temp (e.g. when not owner of temp, but admin)</param>
    /// <returns>True if it were deleted, false otherwize</returns>
    [OperationContract]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/temp/{contentId}/?provider={providerName}&force={force}&workflowOperation={workflowOperation}")]
    bool DeleteTempInXml(
      string contentId,
      string providerName,
      bool force,
      string workflowOperation);

    /// <summary>
    /// Deletes the content item and returns true if the content item has been deleted; otherwise false.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="Id">Id of the content item to be deleted.</param>
    /// <param name="checkOut">Ignored.</param>
    /// <param name="published">Ignored.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the item.</param>
    /// <param name="deletedLanguage">The language version to delete. If null, all versions are deleted(e.g. the item itself is deleted).</param>
    [OperationContract]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/{Id}/?provider={providerName}&version={version}&published={published}&checkOut={checkOut}&workflowOperation={workflowOperation}&language={deletedLanguage}&checkRelatingData={checkRelatingData}")]
    bool DeleteContentInXml(
      string Id,
      string providerName,
      string version,
      bool published,
      bool checkOut,
      string workflowOperation,
      string deletedLanguage,
      bool checkRelatingData);

    /// <summary>
    /// Deletes an array of content items.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="Ids">An array containing the Ids of the content items to be deleted.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the item.</param>
    /// <param name="workflowOperationm">The workflow operation.</param>
    /// <param name="deletedLanguage">The language version to delete. If null, all versions are deleted(e.g. the item itself is deleted).</param>
    /// <returns></returns>
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "batch/?provider={providerName}&workflowOperation={workflowOperation}&language={deletedLanguage}&checkRelatingData={checkRelatingData}")]
    bool BatchDeleteContent(
      string[] Ids,
      string providerName,
      string workflowOperation,
      string deletedLanguage,
      bool checkRelatingData);

    /// <summary>
    /// Deletes an array of content items.
    /// Result is returned in Xml format.
    /// </summary>
    /// <param name="Ids">An array containing the Ids of the content items to be deleted.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the item.</param>
    /// <param name="deletedLanguage">The language version to delete. If null, all versions are deleted(e.g. the item itself is deleted).</param>
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/batch/?provider={providerName}&workflowOperation={workflowOperation}&language={deletedLanguage}&checkRelatingData={checkRelatingData}")]
    bool BatchDeleteContentInXml(
      string[] Ids,
      string providerName,
      string workflowOperation,
      string deletedLanguage,
      bool checkRelatingData);

    /// <summary>
    /// Deletes the child content item and returns true if the child content item has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="pageId">Id of the child content item to be deleted.</param>
    /// <param name="parentId">Id of the parent content item who's child ought to be deleted.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the item.</param>
    /// <param name="checkOut">Ignored.</param>
    /// <param name="published">Ignored.</param>
    [OperationContract]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/parent/{parentId}/{id}/?provider={providerName}&newParentId={newParentId}&version={version}&published={published}&checkOut={checkOut}&workflowOperation={workflowOperation}&checkRelatingData={checkRelatingData}")]
    bool DeleteChildContent(
      string id,
      string parentId,
      string providerName,
      string newParentId,
      string version,
      bool published,
      bool checkOut,
      string workflowOperation,
      bool checkRelatingData);

    /// <summary>
    /// Deletes the child content item and returns true if the child content item has been deleted; otherwise false.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="pageId">Id of the child content item to be deleted.</param>
    /// <param name="parentId">Id of the parent content item who's child ought to be deleted.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the item.</param>
    /// <param name="published">Ignored.</param>
    /// <param name="checkOut">Ignored.</param>
    [OperationContract]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/parent/{parentId}/{id}/?provider={providerName}&newParentId={newParentId}&version={version}&published={published}&checkOut={checkOut}&workflowOperation={workflowOperation}&checkRelatingData={checkRelatingData}")]
    bool DeleteChildContentInXml(
      string id,
      string parentId,
      string providerName,
      string newParentId,
      string version,
      bool published,
      bool checkOut,
      string workflowOperation,
      bool checkRelatingData);

    /// <summary>
    /// Deletes an array of content items.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="Ids">An array containing the Ids of the content items to be deleted.</param>
    /// <param name="parentId">Id of the parent content item who's child ought to be deleted.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the item.</param>
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/parent/{parentId}/batch/?provider={providerName}&workflowOperation={workflowOperation}&language={deletedLanguage}&checkRelatingData={checkRelatingData}")]
    bool BatchDeleteChildContent(
      string[] Ids,
      string parentId,
      string providerName,
      string workflowOperation,
      string deletedLanguage,
      bool checkRelatingData);

    /// <summary>
    /// Deletes an array of content items.
    /// Result is returned in Xml format.
    /// </summary>
    /// <param name="Ids">An array containing the Ids of the content items to be deleted.</param>
    /// <param name="parentId">Id of the parent content item who's child ought to be deleted.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the item.</param>
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/parent/{parentId}/batch/?provider={providerName}&workflowOperation={workflowOperation}&language={deletedLanguage}&checkRelatingData={checkRelatingData}")]
    bool BatchDeleteChildContentInXml(
      string[] Ids,
      string parentId,
      string providerName,
      string workflowOperation,
      string deletedLanguage,
      bool checkRelatingData);

    /// <summary>Reorders the content.</summary>
    /// <param name="contentId">The content pageId.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="oldPosition">The old position.</param>
    /// <param name="newPosition">The new position.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/reorder/{contentId}/?provider={providerName}&oldPosition={oldPosition}&newPosition={newPosition}&workflowOperation={workflowOperation}")]
    [WebHelp(Comment = "Changes the order postition of a content item")]
    void ReorderContent(
      string contentId,
      string providerName,
      float oldPosition,
      float newPosition,
      string workflowOperation);

    /// <summary>Reorders multpile content items</summary>
    /// <param name="contentIdnewOrdinal">Dictionary where the key is the content item and the value is the offset</param>
    /// <param name="providerName">Name of the provider.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/batchReorder/?provider={providerName}&workflowOperation={workflowOperation}")]
    [WebHelp(Comment = "Changes the order postition of a content item")]
    void BatchReorderContent(
      Dictionary<string, float> contentIdnewOrdinal,
      string providerName,
      string workflowOperation);

    /// <summary>
    /// Changes the parent for an array of content items.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="ids">An array of content items ids.</param>
    /// <param name="newParentId">The new parent pageId.</param>
    /// <param name="providerName">Name of the provider.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "batch/{newParentId}/?provider={providerName}&workflowOperation={workflowOperation}")]
    bool BatchChangeParent(
      string[] ids,
      string newParentId,
      string providerName,
      string workflowOperation);

    /// <summary>
    /// Changes the parent for an array of content items.
    /// Result is returned in Xml format.
    /// </summary>
    /// <param name="ids">An array of content items ids.</param>
    /// <param name="newParentId">The new parent pageId.</param>
    /// <param name="providerName">Name of the provider.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/batch/{newParentId}/?provider={providerName}&workflowOperation={workflowOperation}")]
    bool BatchChangeParentInXml(
      string[] ids,
      string newParentId,
      string providerName,
      string workflowOperation);

    /// <summary>Publishes a list of items, identitified by their IDs</summary>
    /// <param name="ids">Strigified IDs of the content items to publish</param>
    /// <param name="providerName">Name of the data provider that should be used</param>
    /// <returns>True on success, false on failure</returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "batch/publish/?provider={providerName}&workflowOperation={workflowOperation}")]
    bool BatchPublish(string[] ids, string providerName, string workflowOperation);

    /// <summary>Publishes a list of items, identitified by their IDs</summary>
    /// <param name="ids">Strigified IDs of the content items to publish</param>
    /// <param name="providerName">Name of the data provider that should be used</param>
    /// <returns>True on success, false on failure</returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/batch/publish/?provider={providerName}&workflowOperation={workflowOperation}")]
    bool BatchPublishInXml(string[] ids, string providerName, string workflowOperation);

    /// <summary>
    /// Publish a list of child items, identified by their IDs
    /// </summary>
    /// <param name="ids">Strigified IDs of the child content items to publish</param>
    /// <param name="providerName">Name of the data provider to use</param>
    /// <param name="parentId">ID of the parent</param>
    /// <returns>True if all are published</returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "parent/{parentId}/batch/publish/?provider={providerName}&workflowOperation={workflowOperation}")]
    bool BatchPublishChildItem(
      string[] ids,
      string providerName,
      string parentId,
      string workflowOperation);

    /// <summary>
    /// Publish a list of child items, identified by their IDs
    /// </summary>
    /// <param name="ids">Strigified IDs of the child content items to publish</param>
    /// <param name="providerName">Name of the data provider to use</param>
    /// <param name="parentId">ID of the parent</param>
    /// <returns>True if all are published</returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "xml/parent/{parentId}/batch/publish/?provider={providerName}&workflowOperation={workflowOperation}")]
    bool BatchPublishChildItemInXml(
      string[] ids,
      string providerName,
      string parentID,
      string workflowOperation);

    /// <summary>
    /// Unpublishes a list of items, identitified by their IDs
    /// </summary>
    /// <param name="ids">Strigified IDs of the content items to unpublish</param>
    /// <param name="providerName">Name of the data provider that should be used</param>
    /// <returns>True on success, false on failure</returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "batch/unpublish/?provider={providerName}&workflowOperation={workflowOperation}")]
    bool BatchUnpublish(string[] ids, string providerName, string workflowOperation);

    /// <summary>
    /// Unpublishes a list of items, identitified by their IDs
    /// </summary>
    /// <param name="ids">Strigified IDs of the content items to unpublish</param>
    /// <param name="providerName">Name of the data provider that should be used</param>
    /// <returns>True on success, false on failure</returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/batch/unpublish/?provider={providerName}&workflowOperation={workflowOperation}")]
    bool BatchUnpublishInXml(string[] ids, string providerName, string workflowOperation);

    /// <summary>
    /// Unpublish a list of child items, identified by their IDs
    /// </summary>
    /// <param name="ids">Strigified IDs of the child content items to unpublish</param>
    /// <param name="providerName">Name of the data provider to use</param>
    /// <param name="parentId">ID of the parent</param>
    /// <returns>True if all are unpublished</returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "parent/{parentId}/batch/unpublish/?provider={providerName}&workflowOperation={workflowOperation}")]
    bool BatchUnpublishChildItem(
      string[] ids,
      string providerName,
      string parentId,
      string workflowOperation);

    /// <summary>
    /// Unpublish a list of child items, identified by their IDs
    /// </summary>
    /// <param name="ids">Strigified IDs of the child content items to unpublish</param>
    /// <param name="providerName">Name of the data provider to use</param>
    /// <param name="parentId">ID of the parent</param>
    /// <returns>True if all are unpublished</returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "xml/parent/{parentId}/batch/unpublish/?provider={providerName}&workflowOperation={workflowOperation}")]
    bool BatchUnpublishChildItemInXml(
      string[] ids,
      string providerName,
      string parentID,
      string workflowOperation);

    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/predecessor/{contentId}/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&onlyPath={onlyPath}&itemType={itemType}")]
    [OperationContract]
    CollectionContext<TContentViewModel> GetPredecessorItems(
      string contentId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      bool onlyPath,
      string itemType);

    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/predecessor/{contentId}/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&onlyPath={onlyPath}&itemType={itemType}")]
    [OperationContract]
    CollectionContext<TContentViewModel> GetPredecessorItemsInXml(
      string contentId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      bool onlyPath,
      string itemType);

    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/parent/{parentId}/batch/place/?providerName={providerName}&placePosition={placePosition}&destination={destination}")]
    CollectionContext<TContentViewModel> BatchPlaceContent(
      string[] sourcePageIds,
      string parentId,
      string providerName,
      string placePosition,
      string destination);

    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/parent/{parentId}/batch/place/xml/?providerName={providerName}&placePosition={placePosition}&destination={destination}")]
    CollectionContext<TContentViewModel> BatchPlaceContentInXml(
      string[] sourcePageIds,
      string parentId,
      string providerName,
      string placePosition,
      string destination);

    /// <summary>Batch moving content.</summary>
    /// <param name="direction">The moving direction.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns></returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "parent/{parentId}/batch/move/?providerName={providerName}&direction={direction}")]
    void BatchMoveContent(
      string[] sourceContentIds,
      string parentId,
      string providerName,
      string direction);

    /// <summary>Batch moving content in XML.</summary>
    /// <param name="direction">The moving direction.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns></returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "parent/{parentId}/batch/move/xml/?providerName={providerName}&direction={direction}")]
    void BatchMoveContentInXml(
      string[] sourceContentIds,
      string parentId,
      string providerName,
      string direction);

    /// <summary>Get rating value for a content item</summary>
    /// <param name="contentId">pageId of the content item</param>
    /// <param name="providerName">name of the content provider to use</param>
    /// <returns>rating for the content item</returns>
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "{contentId}/rating/?provider={providerName}")]
    [OperationContract]
    Decimal GetRating(string contentId, string providerName);

    /// <summary>Get rating value for a content item, in xml</summary>
    /// <param name="contentId">pageId of the content item</param>
    /// <param name="providerName">name of the content provider to use</param>
    /// <returns>rating for the content item, in xml</returns>
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/{contentId}/rating/?provider={providerName}")]
    [OperationContract]
    Decimal XmlGetRating(string contentId, string providerName);

    /// <summary>Sets rating for a content item</summary>
    /// <param name="value">rating to set</param>
    /// <param name="contentId">pageId of the content item</param>
    /// <param name="providerName">name of the content provider to use</param>
    /// <returns>Whether user can vote again</returns>
    [WebInvoke(Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "{contentId}/rating/?provider={providerName}")]
    [OperationContract]
    RatingResult SetRating(Decimal value, string contentId, string providerName);

    /// <summary>Sets rating for a content item, in xml</summary>
    /// <param name="value">rating to set</param>
    /// <param name="contentId">pageId of the content item</param>
    /// <param name="providerName">name of the content provider to use</param>
    /// <returns>Whether user can vote again</returns>
    [WebInvoke(Method = "PUT", RequestFormat = WebMessageFormat.Xml, ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/{contentId}/rating/?provider={providerName}")]
    [OperationContract]
    RatingResult XmlSetRating(Decimal value, string contentId, string providerName);

    /// <summary>Resets the rating for a particular item.</summary>
    /// <param name="contentId">The content pageId.</param>
    /// <param name="providerName">Name of the provider.</param>
    [WebInvoke(Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "{contentId}/rating/?provider={providerName}")]
    [OperationContract]
    RatingResult ResetRating(string contentId, string providerName);

    /// <summary>Resets the rating for a particular item.</summary>
    /// <param name="contentId">The content pageId.</param>
    /// <param name="providerName">Name of the provider.</param>
    [WebInvoke(Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "xml/{contentId}/rating/?provider={providerName}")]
    [OperationContract]
    RatingResult XmlResetRating(string contentId, string providerName);

    /// <summary>
    /// Determines whether the current user can rate by pageId or ip
    /// </summary>
    /// <param name="contentId">Id of the item type</param>
    /// <param name="providerName">name of the content provider to use</param>
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "{contentId}/canRate/?provider={providerName}")]
    bool CanRate(string contentId, string providerName);

    /// <summary>
    /// Determines whether the current user can rate by pageId or ip [xml responce]
    /// </summary>
    /// <param name="contentId">Id of the item type</param>
    /// <param name="providerName">name of the content provider to use</param>
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/{contentId}/canRate/?provider={providerName}")]
    bool XmlCanRate(string contentId, string providerName);

    /// <summary>
    /// Saves the child folder and returns the saved folder item in XML format. If the child
    /// folder with the specified contentId exists the folder item will be updated; otherwise a new child
    /// folder item will be created.
    /// </summary>
    /// <param name="content">The content object to be saved.</param>
    /// <param name="parentId">Id of the parent content item.</param>
    /// <param name="contentId">Id of the folder to be saved.</param>
    /// <param name="providerName">Name of the content provider that is to be used to save the child folder.</param>
    /// <returns>An instance of ContentItemContext that contains the content item that was saved.</returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/folder/parent/{parentId}/{contentId}/?provider={providerName}")]
    ItemContext<FolderDetailViewModel> SaveChildFolder(
      ItemContext<FolderDetailViewModel> content,
      string parentId,
      string contentId,
      string providerName);

    /// <summary>
    /// Saves the child folder and returns the saved folder item in XML format. If the child
    /// folder with the specified contentId exists the folder item will be updated; otherwise a new child
    /// folder item will be created.
    /// </summary>
    /// <param name="content">The content object to be saved.</param>
    /// <param name="parentId">Id of the parent content item.</param>
    /// <param name="contentId">Id of the folder to be saved.</param>
    /// <param name="providerName">Name of the content provider that is to be used to save the child folder.</param>
    /// <returns>An instance of ContentItemContext that contains the content item that was saved.</returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/folder/parent/{parentId}/{contentId}/?provider={providerName}")]
    ItemContext<FolderDetailViewModel> SaveChildFolderInXml(
      ItemContext<FolderDetailViewModel> content,
      string parentId,
      string contentId,
      string providerName);

    /// <summary>Gets the single folder and returns it in JSON format.</summary>
    /// <param name="contentId">Id of the folder that ought to be retrieved.</param>
    /// <param name="providerName">Name of the content provider that is to be used to retrieve the folder.</param>
    /// <returns>An instance of ItemContext that contains the content item to be retrieved.</returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/folder/parent/{parentId}/{contentId}/?provider={providerName}")]
    ItemContext<FolderDetailViewModel> GetChildFolder(
      string parentId,
      string contentId,
      string providerName);

    /// <summary>Gets the single folder and returns it in XML format.</summary>
    /// <param name="contentId">Id of the folder that ought to be retrieved.</param>
    /// <param name="providerName">Name of the content provider that is to be used to retrieve the folder.</param>
    /// <returns>
    /// An instance of ItemContext that contains the content item to be retrieved.
    /// </returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/folder/parent/{parentId}/{contentId}/?provider={providerName}")]
    ItemContext<FolderDetailViewModel> GetChildFolderInXml(
      string parentId,
      string contentId,
      string providerName);

    /// <summary>
    /// Gets the collection of content item summary objects and returns them in JSON format.
    /// </summary>
    /// <param name="sortExpression">Sort expression used to order the content items in the result set.</param>
    /// <param name="skip">The number of items to skip before starting to take the items in the result set.</param>
    /// <param name="take">The number of items to take into the result set.</param>
    /// <param name="filter">Filter expression to be used to filter the items that will be taken into the result set.</param>
    /// <param name="providerName">Name of the content provider to be used to get the content items.</param>
    /// <param name="hierarchyMode">Checks for the hierarchical content</param>
    /// <returns>An instance of CollectionContext object that contains the collection of FolderViewModel items.</returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/folders/?sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&provider={providerName}&hierarchyMode={hierarchyMode}")]
    CollectionContext<FolderViewModel> GetFolders(
      string sortExpression,
      int skip,
      int take,
      string filter,
      string providerName,
      bool hierarchyMode);

    /// <summary>
    /// Gets the collection of content item summary objects and returns them in XML format.
    /// </summary>
    /// <param name="sortExpression">Sort expression used to order the content items in the result set.</param>
    /// <param name="skip">The number of items to skip before starting to take the items in the result set.</param>
    /// <param name="take">The number of items to take into the result set.</param>
    /// <param name="filter">Filter expression to be used to filter the items that will be taken into the result set.</param>
    /// <param name="providerName">Name of the content provider to be used to get the content items.</param>
    /// <param name="hierarchyMode">Checks for the hierarchical content</param>
    /// <returns>An instance of CollectionContext object that contains the collection of FolderViewModel items.</returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/folders/?sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&provider={providerName}&hierarchyMode={hierarchyMode}")]
    CollectionContext<FolderViewModel> GetFoldersInXml(
      string sortExpression,
      int skip,
      int take,
      string filter,
      string providerName,
      bool hierarchyMode);

    /// <summary>
    /// Gets the collection of sub folder objects and returns them in JSON format.
    /// </summary>
    /// <param name="folderId">The parent folder ID</param>
    /// <param name="sortExpression">Sort expression used to order the content items in the result set.</param>
    /// <param name="skip">The number of items to skip before starting to take the items in the result set.</param>
    /// <param name="take">The number of items to take into the result set.</param>
    /// <param name="filter">Filter expression to be used to filter the items that will be taken into the result set.</param>
    /// <param name="providerName">Name of the content provider to be used to get the content items.</param>
    /// <param name="hierarchyMode">Checks for the hierarchical content</param>
    /// <returns>An instance of CollectionContext object that contains the collection of FolderViewModel items.</returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/folders/{folderId}/?sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&provider={providerName}&hierarchyMode={hierarchyMode}")]
    CollectionContext<FolderViewModel> GetSubFolders(
      string folderId,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string providerName,
      bool hierarchyMode);

    /// <summary>
    /// Gets the collection of sub folder objects and returns them in XML format.
    /// </summary>
    /// <param name="folderId">The parent folder ID</param>
    /// <param name="sortExpression">Sort expression used to order the content items in the result set.</param>
    /// <param name="skip">The number of items to skip before starting to take the items in the result set.</param>
    /// <param name="take">The number of items to take into the result set.</param>
    /// <param name="filter">Filter expression to be used to filter the items that will be taken into the result set.</param>
    /// <param name="providerName">Name of the content provider to be used to get the content items.</param>
    /// <param name="hierarchyMode">Checks for the hierarchical content</param>
    /// <returns>An instance of CollectionContext object that contains the collection of FolderViewModel items.</returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/folders/{folderId}/?sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&provider={providerName}&hierarchyMode={hierarchyMode}")]
    CollectionContext<FolderViewModel> GetSubFoldersInXml(
      string folderId,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string providerName,
      bool hierarchyMode);

    /// <summary>
    /// Gets the collection of predecessor folders objects and returns them in JSON format.
    /// </summary>
    /// <param name="folderId">The parent folder ID</param>
    /// <param name="providerName">Name of the content provider to be used to get the content items.</param>
    /// <returns>An instance of CollectionContext object that contains the collection of FolderViewModel items.</returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/folders/predecessors/{folderId}/?provider={providerName}&sortExpression={sortExpression}&excludeNeighbours={excludeNeighbours}")]
    CollectionContext<FolderViewModel> GetPredecessorFolders(
      string folderId,
      string providerName,
      string sortExpression,
      bool excludeNeighbours);

    /// <summary>
    /// Gets the collection of predecessor folders objects and returns them in XML format.
    /// </summary>
    /// <param name="folderId">The parent folder ID</param>
    /// <param name="providerName">Name of the content provider to be used to get the content items.</param>
    /// <returns>An instance of CollectionContext object that contains the collection of FolderViewModel items.</returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/folders/predecessors/{folderId}/?provider={providerName}&sortExpression={sortExpression}&excludeNeighbours={excludeNeighbours}")]
    CollectionContext<FolderViewModel> GetPredecessorFoldersInXml(
      string folderId,
      string providerName,
      string sortExpression,
      bool excludeNeighbours);

    /// <summary>Gets folders as tree.</summary>
    /// <param name="provider">name of the provider</param>
    /// <param name="sortExpression">sort expresion</param>
    /// <param name="skip">number of items to skip</param>
    /// <param name="take">number of items to take</param>
    /// <param name="filter">filter to apply</param>
    /// <param name="hierarchyMode">Checks for the hierarchical content</param>
    /// <returns>An instance of CollectionContext object that contains the collection of FolderViewModel items</returns>
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/folders/tree/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&hierarchyMode={hierarchyMode}")]
    CollectionContext<FolderViewModel> GetFoldersAsTree(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      bool hierarchyMode);

    /// <summary>Gets folders as tree in XML.</summary>
    /// <param name="provider">name of the provider</param>
    /// <param name="sortExpression">sort expresion</param>
    /// <param name="skip">number of items to skip</param>
    /// <param name="take">number of items to take</param>
    /// <param name="filter">filter to apply</param>
    /// <param name="hierarchyMode">Checks for the hierarchical content</param>
    /// <returns>An instance of CollectionContext object that contains the collection of FolderViewModel items</returns>
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/folders/tree/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&hierarchyMode={hierarchyMode}")]
    CollectionContext<FolderViewModel> GetFoldersAsTreeInXml(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      bool hierarchyMode);

    /// <summary>Sets the image as cover of its parent.</summary>
    /// <param name="contentId">The Id of the image.</param>
    /// <param name="providerName">The name of the libraries provider.</param>
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/cover/{contentId}/?provider={providerName}")]
    void SetAsCover(string contentId, string providerName);

    /// <summary>Gets the file links collection for this item</summary>
    /// <param name="contentId">The Id of the content.</param>
    /// <param name="providerName">The name of the libraries provider.</param>
    /// <param name="culture">The name of the culture.</param>
    [OperationContract]
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/mediaFileLinks/?contentId={contentId}&provider={providerName}&culture={culture}")]
    CollectionContext<MediaFileLinkViewModel> GetMediaFileLinks(
      string contentId,
      string providerName,
      string culture);

    /// <summary>
    /// Copies a file link with a given file id to the current language of the media content
    /// </summary>
    /// <param name="contentId">The Id of the content.</param>
    /// <param name="providerName">The name of the libraries provider.</param>
    /// <param name="fileId">The if of the file.</param>
    [OperationContract]
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/copyFileLink/?contentId={contentId}&provider={providerName}&fileId={fileId}")]
    void CopyFileLink(string contentId, string providerName, string fileId);
  }
}
