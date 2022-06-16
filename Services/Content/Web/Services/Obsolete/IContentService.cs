// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Content.Web.Services.Obsolete.IContentService
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

namespace Telerik.Sitefinity.Services.Content.Web.Services.Obsolete
{
  /// <summary>
  /// The WCF web service that is used to work with all types that inherit from base <see cref="N:Telerik.Sitefinity.Services.Content" />
  /// class.
  /// </summary>
  [ServiceContract(Namespace = "ContentService")]
  [AllowDynamicFields]
  [ServiceKnownType(typeof (ContentListsViewModel))]
  [ServiceKnownType(typeof (LibraryViewModel_old))]
  public interface IContentService
  {
    /// <summary>
    /// Gets the single content item and returs it in JSON format.
    /// </summary>
    /// <param name="contentId">Id of the content item that ought to be retrieved.</param>
    /// <param name="itemType">The actual type of the content that ought to be retrieved.</param>
    /// <param name="providerName">Name of the content provider that is to be used to retrieve the content item.</param>
    /// 
    ///             /// <param name="version">The version id. If not specified returns the current draft version, otherwise the specified version from the history</param>
    /// <returns>An instance of ContentItemContext that contains the content item to be retrieved.</returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{contentId}/?providerName={providerName}&itemType={itemType}&managerType={managerType}&version={version}&published={published}")]
    ContentItemContext<Telerik.Sitefinity.GenericContent.Model.Content> GetContent(
      string contentId,
      string itemType,
      string providerName,
      string managerType,
      string version,
      bool published);

    /// <summary>
    /// Gets the single content item and returs it in XML format.
    /// </summary>
    /// <param name="contentId">Id of the content item that ought to be retrieved.</param>
    /// <param name="itemType">The actual type of the content that ought to be retrieved.</param>
    /// <param name="providerName">Name of the content provider that is to be used to retrieve the content item.</param>
    /// <param name="managerType">Type of the manager.</param>
    /// <param name="version">The version id. If not specified returns the current draft version, otherwise the specified version from the history</param>
    /// <returns>
    /// An instance of ContentItemContext that contains the content item to be retrieved.
    /// </returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/{contentId}/?providerName={providerName}&itemType={itemType}&managerType={managerType}")]
    ContentItemContext<Telerik.Sitefinity.GenericContent.Model.Content> GetContentInXml(
      string contentId,
      string itemType,
      string providerName,
      string managerType);

    /// <summary>
    /// Gets a single child content item and returns it in JSON format.
    /// </summary>
    /// <param name="parentId">Id of the parent content item.</param>
    /// <param name="contentId">Id of the content item to be retrieved.</param>
    /// <param name="itemType">Actual type of the content item to be retrieved.</param>
    /// <param name="parentItemType">Actual type of the parent content item.</param>
    /// <param name="providerName">Name of the content provider that is to be used to retrieved the child content item.</param>
    /// <returns>An instance of ContentItemContext that contains the content item to be retrieved.</returns>
    [OperationContract]
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/parent/{parentId}/{contentId}/?providerName={providerName}&itemType={itemType}&parentItemType={parentItemType}&newParentId={newParentId}&version={version}")]
    ContentItemContext<Telerik.Sitefinity.GenericContent.Model.Content> GetChildContent(
      string parentId,
      string contentId,
      string itemType,
      string parentItemType,
      string providerName,
      string newParentId,
      string version);

    /// <summary>
    /// Gets a single child content item and returns it in XML format.
    /// </summary>
    /// <param name="parentId">Id of the parent content item.</param>
    /// <param name="contentId">Id of the content item to be retrieved.</param>
    /// <param name="itemType">Actual type of the content item to be retrieved.</param>
    /// <param name="parentItemType">Actual type of the parent content item.</param>
    /// <param name="providerName">Name of the content provider that is to be used to retrieve the child content item.</param>
    /// <returns>An instance of ContentItemContext that contains the content item to be retrieved.</returns>
    [OperationContract]
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/parent/{parentId}/{contentId}/?providerName={providerName}&itemType={itemType}&parentItemType={parentItemType}&newParentId={newParentId}")]
    ContentItemContext<Telerik.Sitefinity.GenericContent.Model.Content> GetChildContentInXml(
      string parentId,
      string contentId,
      string itemType,
      string parentItemType,
      string providerName,
      string newParentId);

    /// <summary>
    /// Saves the content item and returns the saved content item in JSON format. If the content item
    /// with the specified pageId exists the content item will be updates; otherwise new content item will
    /// be created.
    /// </summary>
    /// <param name="content">The content object to be saved.</param>
    /// <param name="contentId">The pageId of the content item to be saved.</param>
    /// <param name="itemType">The actual type of the content to be saved.</param>
    /// <param name="providerName">Name of the content provider that is to be used to save the content item.</param>
    /// <returns>An instance of ContentItemContext that contains the content item that was saved.</returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{contentId}/?providerName={providerName}&itemType={itemType}&managerType={managerType}&version={version}&published={published}")]
    ContentItemContext<Telerik.Sitefinity.GenericContent.Model.Content> SaveContent(
      ContentItemContext<Telerik.Sitefinity.GenericContent.Model.Content> content,
      string contentId,
      string itemType,
      string providerName,
      string managerType,
      string version,
      bool published);

    /// <summary>
    /// Saves the content item and returns the saved content item in XML format. If the content item
    /// with the specified pageId exists the content item will be updated; otherwise a new content item will
    /// be created.
    /// </summary>
    /// <param name="content">The content object to be saved.</param>
    /// <param name="contentId">The pageId of the content item to be saved.</param>
    /// <param name="itemType">The actual type of the content to be saved.</param>
    /// <param name="providerName">Name of the content provider that is to be used to save the content item.</param>
    /// <returns>An instance of ContentItemContext that contains the content item that was saved.</returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/{contentId}/?providerName={providerName}&itemType={itemType}&managerType={managerType}")]
    ContentItemContext<Telerik.Sitefinity.GenericContent.Model.Content> SaveContentInXml(
      ContentItemContext<Telerik.Sitefinity.GenericContent.Model.Content> content,
      string contentId,
      string itemType,
      string providerName,
      string managerType);

    /// <summary>
    /// Saves the child content item and returns the saved child content item in JSON format. If the child
    /// content item with the specified pageId exists the content item will be updated; otherwise a new child
    /// content item will be created.
    /// </summary>
    /// <param name="content">The content object to be saved.</param>
    /// <param name="parentId">Id of the parent content item.</param>
    /// <param name="contentId">Id of the content item to be saved.</param>
    /// <param name="itemType">Actual type of the content item to be saved.</param>
    /// <param name="parentItemType">Actual type of the parent content item.</param>
    /// <param name="providerName">Name of the content provider that is to be used to save the child content item.</param>
    /// <returns>An instance of ContentItemContext that contains the content item that was saved.</returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/parent/{parentId}/{contentId}/?providerName={providerName}&itemType={itemType}&parentItemType={parentItemType}&newParentId={newParentId}&version={version}")]
    ContentItemContext<Telerik.Sitefinity.GenericContent.Model.Content> SaveChildContent(
      ContentItemContext<Telerik.Sitefinity.GenericContent.Model.Content> content,
      string parentId,
      string contentId,
      string itemType,
      string parentItemType,
      string providerName,
      string newParentId,
      string version);

    /// <summary>
    /// Saves the child content item and returns the saved child content item in XML format. If the child
    /// content item with the specified pageId exists the content item will be updated; otherwise a new child
    /// content item will be created.
    /// </summary>
    /// <param name="content">The content object to be saved.</param>
    /// <param name="parentId">Id of the parent content item.</param>
    /// <param name="contentId">Id of the content item to be saved.</param>
    /// <param name="itemType">Actual type of the content item to be saved.</param>
    /// <param name="parentItemType">Actual type of the parent content item.</param>
    /// <param name="providerName">Name of the content provider that is to be used to save the child content item.</param>
    /// <returns>An instance of ContentItemContext that contains the content item that was saved.</returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/parent/{parentId}/{contentId}/?providerName={providerName}&itemType={itemType}&parentItemType={parentItemType}&newParentId={newParentId}")]
    ContentItemContext<Telerik.Sitefinity.GenericContent.Model.Content> SaveChildContentInXml(
      ContentItemContext<Telerik.Sitefinity.GenericContent.Model.Content> content,
      string parentId,
      string contentId,
      string itemType,
      string parentItemType,
      string providerName,
      string newParentId);

    /// <summary>
    /// Gets the collection of content item summary objects and returns them in JSON format.
    /// </summary>
    /// <param name="itemType">The actual type of the content to be retrieved.</param>
    /// <param name="sortExpression">Sort expression used to order the content items in the result set.</param>
    /// <param name="skip">The number of items to skip before starting to take the items in the result set.</param>
    /// <param name="take">The number of items to take into the result set.</param>
    /// <param name="filter">Filter expression to be used to filter the items that will be taken into the result set.</param>
    /// <param name="providerName">Name of the content provider to be used to get the content items.</param>
    /// <returns>An instance of CollectionContext object that contains the collection of ContentSummary items.</returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/?sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&providerName={providerName}&itemType={itemType}&managerType={managerType}")]
    CollectionContext<ContentViewModelBase> GetContentItems(
      string itemType,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string providerName,
      string managerType);

    /// <summary>
    /// Gets the collection of content item summary objects and returns them in XML format.
    /// </summary>
    /// <param name="itemType">The actual type of the content to be retrieved.</param>
    /// <param name="sortExpression">Sort expression used to order the content items in the result set.</param>
    /// <param name="skip">The number of items to skip before starting to take the items in the result set.</param>
    /// <param name="take">The number of items to take into the result set.</param>
    /// <param name="filter">Filter expression to be used to filter the items that will be taken into the result set.</param>
    /// <param name="providerName">Name of the content provider to be used to get the content items.</param>
    /// <returns>An instance of CollectionContext object that contains the collection of ContentSummary items.</returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/?sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&providerName={providerName}&itemType={itemType}&managerType={managerType}")]
    CollectionContext<ContentViewModelBase> GetContentItemsInXml(
      string itemType,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string providerName,
      string managerType);

    /// <summary>
    /// Gets the collection of child content item summary objects and returns them in JSON format.
    /// </summary>
    /// <param name="parentId">The if of the parent content item for which the children ought to be retrieved.</param>
    /// <param name="providerName">Name of the content provider to be used to get the child content items.</param>
    /// <param name="itemType">The actual type of the child content that ought to be retrieved.</param>
    /// <param name="parentItemType">The actual type of the parent content for which the children ought to be retrieved.</param>
    /// <param name="sortExpression">Sort expression used to order the child content items in the result set.</param>
    /// <param name="filter">Filter expression to be used to filter the items that will be taken into the results set.</param>
    /// <param name="skip">The number of items to skip before starting to take the items in the result set.</param>
    /// <param name="take">The number of items to take into the result set.</param>
    /// <returns>An instance of CollectionContext object that contains the collection of ContentSummary items.</returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/parent/{parentId}/?providerName={providerName}&itemType={itemType}&parentItemType={parentItemType}&sortExpression={sortExpression}&filter={filter}&skip={skip}&take={take}")]
    CollectionContext<ContentViewModelBase> GetChildrenContentItems(
      string parentId,
      string providerName,
      string itemType,
      string parentItemType,
      string sortExpression,
      string filter,
      int skip,
      int take);

    /// <summary>
    /// Gets the collection of child content item summary objects and returns them in XML format.
    /// </summary>
    /// <param name="parentId">The if of the parent content item for which the children ought to be retrieved.</param>
    /// <param name="providerName">Name of the content provider to be used to get the child content items.</param>
    /// <param name="itemType">The actual type of the child content that ought to be retrieved.</param>
    /// <param name="parentItemType">The actual type of the parent content for which the children ought to be retrieved.</param>
    /// <param name="sortExpression">Sort expression used to order the child content items in the result set.</param>
    /// <param name="filter">Filter expression to be used to filter the items that will be taken into the results set.</param>
    /// <param name="skip">The number of items to skip before starting to take the items in the result set.</param>
    /// <param name="take">The number of items to take into the result set.</param>
    /// <returns>An instance of CollectionContext object that contains the collection of ContentSummary items.</returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/parent/{parentId}/?providerName={providerName}&itemType={itemType}&parentItemType={parentItemType}&sortExpression={sortExpression}&filter={filter}&skip={skip}&take={take}")]
    CollectionContext<ContentViewModelBase> GetChildrenContentItemsInXml(
      string parentId,
      string providerName,
      string itemType,
      string parentItemType,
      string sortExpression,
      string filter,
      int skip,
      int take);

    /// <summary>
    /// Deletes the content item and returns true if the content item has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="Id">Id of the content item to be deleted.</param>
    /// <param name="itemType">The actual type of the content item to be deleted.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the item.</param>
    [OperationContract]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{contentId}/?providerName={providerName}&itemType={itemType}&managerType={managerType}&version={version}&published={published}")]
    bool DeleteContent(
      string contentId,
      string itemType,
      string providerName,
      string managerType,
      string version,
      bool published);

    /// <summary>
    /// Deletes the content item and returns true if the content item has been deleted; otherwise false.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="Id">Id of the content item to be deleted.</param>
    /// <param name="itemType">The actual type of the content item to be deleted.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the item.</param>
    [OperationContract]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/{Id}/?providerName={providerName}&itemType={itemType}&managerType={managerType}")]
    bool DeleteContentInXml(string Id, string itemType, string providerName, string managerType);

    /// <summary>
    /// Deletes an array of content items.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="Ids">An array containing the Ids of the content items to be deleted.</param>
    /// <param name="itemsType">The actual type of the content items to be deleted.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the item.</param>
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "batch/?providerName={providerName}&itemType={itemType}&managerType={managerType}")]
    bool BatchDeleteContent(
      string[] Ids,
      string itemType,
      string providerName,
      string managerType);

    /// <summary>
    /// Deletes an array of content items.
    /// Result is returned in Xml format.
    /// </summary>
    /// <param name="Ids">An array containing the Ids of the content items to be deleted.</param>
    /// <param name="itemsType">The actual type of the content items to be deleted.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the item.</param>
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/batch/?providerName={providerName}&itemType={itemType}&managerType={managerType}")]
    bool BatchDeleteContentInXml(
      string[] Ids,
      string itemType,
      string providerName,
      string managerType);

    /// <summary>
    /// Deletes the child content item and returns true if the child content item has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="pageId">Id of the child content item to be deleted.</param>
    /// <param name="parentId">Id of the parent content item who's child ought to be deleted.</param>
    /// <param name="itemType">The actual type of the content item to be deleted.</param>
    /// <param name="parentItemType">The actula type of the parent content item.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the item.</param>
    [OperationContract]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/parent/{parentId}/{id}/?providerName={providerName}&itemType={itemType}&parentItemType={parentItemType}&newParentId={newParentId}&version={version}")]
    bool DeleteChildContent(
      string id,
      string parentId,
      string itemType,
      string parentItemType,
      string providerName,
      string newParentId,
      string version);

    /// <summary>
    /// Deletes the child content item and returns true if the child content item has been deleted; otherwise false.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="pageId">Id of the child content item to be deleted.</param>
    /// <param name="parentId">Id of the parent content item who's child ought to be deleted.</param>
    /// <param name="itemType">The actual type of the content item to be deleted.</param>
    /// <param name="parentItemType">The actula type of the parent content item.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the item.</param>
    [OperationContract]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/parent/{parentId}/{id}/?providerName={providerName}&itemType={itemType}&parentItemType={parentItemType}&newParentId={newParentId}")]
    bool DeleteChildContentInXml(
      string id,
      string parentId,
      string itemType,
      string parentItemType,
      string providerName,
      string newParentId);

    /// <summary>
    /// Deletes an array of content items.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="Ids">An array containing the Ids of the content items to be deleted.</param>
    /// <param name="parentId">Id of the parent content item who's child ought to be deleted.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the item.</param>
    /// <param name="managerType">Type of the manager.</param>
    /// <returns></returns>
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/parent/{parentId}/batch/?providerName={providerName}&itemType={itemType}&managerType={managerType}")]
    bool BatchDeleteChildContent(
      string[] Ids,
      string parentId,
      string itemType,
      string providerName,
      string managerType);

    /// <summary>
    /// Deletes an array of content items.
    /// Result is returned in Xml format.
    /// </summary>
    /// <param name="Ids">An array containing the Ids of the content items to be deleted.</param>
    /// <param name="parentId">Id of the parent content item who's child ought to be deleted.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the item.</param>
    /// <param name="managerType">Type of the manager.</param>
    /// <returns></returns>
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/parent/{parentId}/batch/?providerName={providerName}&itemType={itemType}&managerType={managerType}")]
    bool BatchDeleteChildContentInXml(
      string[] Ids,
      string parentId,
      string itemType,
      string providerName,
      string managerType);

    /// <summary>Reorders the content.</summary>
    /// <param name="contentId">The content pageId.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="managerType">Type of the manager.</param>
    /// <param name="oldPosition">The old position.</param>
    /// <param name="newPosition">The new position.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/reorder/{contentId}/?providerName={providerName}&itemType={itemType}&managerType={managerType}&oldPosition={oldPosition}&newPosition={newPosition}")]
    [WebHelp(Comment = "Changes the order postition of a content item")]
    void ReorderContent(
      string contentId,
      string itemType,
      string providerName,
      string managerType,
      float oldPosition,
      float newPosition);

    /// <summary>Reorders multpile content items</summary>
    /// <param name="contentIdnewOrdinal">Dictionary where the key is the content item and the value is the offset</param>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="managerType">Type of the manager.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/batchReorder/?providerName={providerName}&itemType={itemType}&managerType={managerType}")]
    [WebHelp(Comment = "Changes the order postition of a content item")]
    void BatchReorderContent(
      Dictionary<string, float> contentIdnewOrdinal,
      string itemType,
      string providerName,
      string managerType);

    /// <summary>
    /// Changes the parent for an array of content items.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="ids">An array of content items ids.</param>
    /// <param name="newParentId">The new parent pageId.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="parentItemType">Type of the parent item.</param>
    /// <returns></returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/batch/{newParentId}/?provider={providerName}&itemType={itemType}&parentItemType={parentItemType}")]
    bool BatchChangeParent(
      string[] ids,
      string newParentId,
      string providerName,
      string itemType,
      string parentItemType);

    /// <summary>
    /// Changes the parent for an array of content items.
    /// Result is returned in Xml format.
    /// </summary>
    /// <param name="ids">An array of content items ids.</param>
    /// <param name="newParentId">The new parent pageId.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="parentItemType">Type of the parent item.</param>
    /// <returns></returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/batch/{newParentId}/?provider={providerName}&itemType={itemType}&parentItemType={parentItemType}")]
    bool BatchChangeParentInXml(
      string[] ids,
      string newParentId,
      string providerName,
      string itemType,
      string parentItemType);

    /// <summary>Get rating value for a content item</summary>
    /// <param name="itemType">type of the content item</param>
    /// <param name="contentId">pageId of the content item</param>
    /// <param name="providerName">name of the content provider to use</param>
    /// <returns>rating for the content item</returns>
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "{contentId}/rating/?itemType={itemType}&provider={providerName}")]
    [OperationContract]
    Decimal GetRating(string itemType, string contentId, string providerName);

    /// <summary>Get rating value for a content item, in xml</summary>
    /// <param name="itemType">type of the content item</param>
    /// <param name="contentId">pageId of the content item</param>
    /// <param name="providerName">name of the content provider to use</param>
    /// <returns>rating for the content item, in xml</returns>
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/{contentId}/rating/?itemType={itemType}&provider={providerName}")]
    [OperationContract]
    Decimal XmlGetRating(string itemType, string contentId, string providerName);

    /// <summary>Sets rating for a content item</summary>
    /// <param name="value">rating to set</param>
    /// <param name="itemType">type of the content item</param>
    /// <param name="contentId">pageId of the content item</param>
    /// <param name="providerName">name of the content provider to use</param>
    /// <returns>Whether user can vote again</returns>
    [WebInvoke(Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "{contentId}/rating/?itemType={itemType}&provider={providerName}")]
    [OperationContract]
    RatingResult SetRating(
      Decimal value,
      string itemType,
      string contentId,
      string providerName);

    /// <summary>Sets rating for a content item, in xml</summary>
    /// <param name="value">rating to set</param>
    /// <param name="itemType">type of the content item</param>
    /// <param name="contentId">pageId of the content item</param>
    /// <param name="providerName">name of the content provider to use</param>
    /// <returns>Whether user can vote again</returns>
    [WebInvoke(Method = "PUT", RequestFormat = WebMessageFormat.Xml, ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/{contentId}/rating/?itemType={itemType}&provider={providerName}")]
    [OperationContract]
    RatingResult XmlSetRating(
      Decimal value,
      string itemType,
      string contentId,
      string providerName);

    /// <summary>Resets the rating for a particular item.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="contentId">The content pageId.</param>
    /// <param name="providerName">Name of the provider.</param>
    [WebInvoke(Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "{contentId}/rating/?itemType={itemType}&provider={providerName}")]
    [OperationContract]
    RatingResult ResetRating(string itemType, string contentId, string providerName);

    /// <summary>Resets the rating for a particular item.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="contentId">The content pageId.</param>
    /// <param name="providerName">Name of the provider.</param>
    [WebInvoke(Method = "DELETE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "xml/{contentId}/rating/?itemType={itemType}&provider={providerName}")]
    [OperationContract]
    RatingResult XmlResetRating(string itemType, string contentId, string providerName);

    /// <summary>
    /// Determines whether the current user can rate by pageId or ip
    /// </summary>
    /// <param name="contentId">Id of the item type</param>
    /// <param name="providerName">name of the content provider to use</param>
    /// <param name="itemType">CLR type of the item to rate</param>
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "{contentId}/canRate/?itemType={itemType}&provider={providerName}")]
    bool CanRate(string contentId, string itemType, string providerName);

    /// <summary>
    /// Determines whether the current user can rate by pageId or ip [xml responce]
    /// </summary>
    /// <param name="contentId">Id of the item type</param>
    /// <param name="providerName">name of the content provider to use</param>
    /// <param name="itemType">CLR type of the item to rate</param>
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/{contentId}/canRate/?itemType={itemType}&provider={providerName}")]
    bool XmlCanRate(string contentId, string itemType, string providerName);
  }
}
