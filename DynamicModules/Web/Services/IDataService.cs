// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Web.Services.IDataService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ServiceModel;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Data.WcfHelpers;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.DynamicModules.Web.Services
{
  /// <summary>
  /// Web service for working with dynamic data of the dynamic modules.
  /// </summary>
  [ServiceContract]
  [AllowDynamicFields]
  internal interface IDataService
  {
    /// <summary>
    /// Gets the single dynamic data item and returs it in JSON format.
    /// </summary>
    /// <param name="itemId">Id of the dynamic data item that ought to be retrieved.</param>
    /// <param name="providerName">Name of the provider that is to be used to retrieve the dynamic data item.</param>
    /// <param name="version">The version.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="duplicate">The duplicate.</param>
    /// <param name="checkOut">The check out.</param>
    /// <returns>
    /// An instance of ItemContext that contains the dynamic data item to be retrieved.
    /// </returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{itemId}/?provider={providerName}&itemType={itemType}&version={version}&duplicate={duplicate}&checkOut={checkOut}")]
    DynamicItemContext GetDataItem(
      string itemId,
      string providerName,
      string version,
      string itemType,
      bool duplicate,
      bool checkOut = false);

    /// <summary>
    /// Gets the single dynamic data item and returs it in XML format.
    /// </summary>
    /// <param name="itemId">Id of the dynamic data item that ought to be retrieved.</param>
    /// <param name="providerName">Name of the provider that is to be used to retrieve the dynamic data item.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="duplicate">The duplicate.</param>
    /// <returns>
    /// An instance of ItemContext that contains the dynamic data item to be retrieved.
    /// </returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/{itemId}/?provider={providerName}&itemType={itemType}&duplicate={duplicate}")]
    DynamicItemContext GetDataItemInXml(
      string itemId,
      string providerName,
      string itemType,
      bool duplicate);

    /// <summary>
    /// Gets the collection of dynamic data items and returns the result in JSON format.
    /// </summary>
    /// <param name="provider">Name of the dynamic module provider to be used when retriving dynamic data items.</param>
    /// <param name="sortExpression">Sort expression used to order the collection of dynamic data items.</param>
    /// <param name="skip">Number of items to skip before starting to take the result set.</param>
    /// <param name="take">Number of items to take in the result set.</param>
    /// <param name="filter">Filter expression used to filter which dynamic data items ought to be retrieved.</param>
    /// <param name="itemType">Type of the dynamic data items that ought to be retrieved in the result set.</param>
    /// <param name="hierarchyMode">The hierarchy mode.</param>
    /// <returns>
    /// An instance of CollectionContext object that contains the collection of dynamic data items.
    /// </returns>
    [WebHelp(Comment = "Gets the collection of dynamic data items and returns the result in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&itemType={itemType}&hierarchyMode={hierarchyMode}")]
    [OperationContract]
    CollectionContext<DynamicContent> GetDataItems(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string itemType,
      bool hierarchyMode = false);

    /// <summary>
    /// Gets the collection of dynamic data items and returns the result in XML format.
    /// </summary>
    /// <param name="provider">Name of the dynamic module provider to be used when retriving dynamic data items.</param>
    /// <param name="sortExpression">Sort expression used to order the collection of dynamic data items.</param>
    /// <param name="skip">Number of items to skip before starting to take the result set.</param>
    /// <param name="take">Number of items to take in the result set.</param>
    /// <param name="filter">Filter expression used to filter which dynamic data items ought to be retrieved.</param>
    /// <param name="itemType">Type of the dynamic data items that ought to be retrieved in the result set.</param>
    /// <param name="hierarchyMode">The hierarchy mode.</param>
    /// <returns>
    /// An instance of CollectionContext object that contains the collection of dynamic data items.
    /// </returns>
    [WebHelp(Comment = "Gets the collection of dynamic data items and returns the result in XML format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&itemType={itemType}&hierarchyMode={hierarchyMode}")]
    [OperationContract]
    CollectionContext<DynamicContent> GetDataItemsInXml(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string itemType,
      bool hierarchyMode = false);

    /// <summary>
    /// Gets the collection of live dynamic data items and returns the result in JSON format.
    /// </summary>
    /// <param name="provider">Name of the dynamic module provider to be used when retriving dynamic data items.</param>
    /// <param name="sortExpression">Sort expression used to order the collection of dynamic data items.</param>
    /// <param name="skip">Number of items to skip before starting to take the result set.</param>
    /// <param name="take">Number of items to take in the result set.</param>
    /// <param name="filter">Filter expression used to filter which dynamic data items ought to be retrieved.</param>
    /// <param name="itemType">Type of the dynamic data items that ought to be retrieved in the result set.</param>
    /// <param name="published">Specifies if only published items are requested.</param>
    /// <returns>An instance of CollectionContext object that contains the collection of live dynamic data items.</returns>
    [WebHelp(Comment = "Gets the collection of dynamic data items and returns the result in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/live/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&itemType={itemType}&radius={radius}&latitude={latitude}&longitude={longitude}&geoLocationProperty={geoLocationProperty}&published={published}")]
    [OperationContract]
    CollectionContext<DynamicContent> GetLiveDataItems(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string itemType,
      double latitude = 0.0,
      double longitude = 0.0,
      double radius = 0.0,
      string geoLocationProperty = null,
      bool published = false);

    /// <summary>
    /// Gets the collection of live dynamic data items and returns the result in XML format.
    /// </summary>
    /// <param name="provider">Name of the dynamic module provider to be used when retriving dynamic data items.</param>
    /// <param name="sortExpression">Sort expression used to order the collection of dynamic data items.</param>
    /// <param name="skip">Number of items to skip before starting to take the result set.</param>
    /// <param name="take">Number of items to take in the result set.</param>
    /// <param name="filter">Filter expression used to filter which dynamic data items ought to be retrieved.</param>
    /// <param name="itemType">Type of the dynamic data items that ought to be retrieved in the result set.</param>
    /// <returns>An instance of CollectionContext object that contains the collection of live dynamic data items.</returns>
    [WebHelp(Comment = "Gets the collection of dynamic data items and returns the result in XML format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/live/xml/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&itemType={itemType}")]
    [OperationContract]
    CollectionContext<DynamicContent> GetLiveDataItemsInXml(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string itemType);

    /// <summary>
    /// Saves a dynamic data item. If the dynamic data item exists it updates it; otherwise creates a new dynamic data item.
    /// Returns the saved dynamic data item object in JSON format.
    /// </summary>
    /// <param name="dataItem">Dynamic data item object to be saved.</param>
    /// <param name="dataItemId">Id of the dynamic data item under which dynamic data item ought to be saved.</param>
    /// <param name="provider">Name of the dynamic module provider to be used when saving the dynamic data item.</param>
    /// <param name="itemType">Type of the item being saved.</param>
    /// <param name="workflowOperation">Workflow operation to be performed.</param>
    /// <returns>
    /// An instance of <see cref="T:Telerik.Sitefinity.DynamicModules.Model.DynamicContent" /> that represents the saved dynamic data item object.
    /// </returns>
    [WebHelp(Comment = "Saves a dynamic data item. If the dynamic data item exists it updates it; otherwise creates a new dynamic data item. Returns the saved dynamic data item object in JSON format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{dataItemId}/?provider={provider}&itemType={itemType}&workflowOperation={workflowOperation}")]
    [OperationContract]
    DynamicItemContext SaveDataItem(
      DynamicItemContext dataItem,
      string dataItemId,
      string provider,
      string itemType,
      string workflowOperation);

    /// <summary>
    /// Saves a dynamic data item. If the dynamic data item exists it updates it; otherwise creates a new dynamic data item.
    /// Returns the saved dynamic data item object in XML format.
    /// </summary>
    /// <param name="dataItem">Dynamic data item object to be saved.</param>
    /// <param name="dataItemId">Id of the dynamic data item under which dynamic data item ought to be saved.</param>
    /// <param name="provider">Name of the dynamic module provider to be used when saving the dynamic data item.</param>
    /// <param name="itemType">Type of the item being saved.</param>
    /// <param name="workflowOperation">Workflow operation to be performed.</param>
    /// <returns>
    /// An instance of <see cref="T:Telerik.Sitefinity.DynamicModules.Model.DynamicContent" /> that represents the saved dynamic data item object.
    /// </returns>
    [WebHelp(Comment = "Saves a dynamic data item. If the dynamic data item exists it updates it; otherwise creates a new dynamic data item. Returns the saved dynamic data item object in XMl format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/{dataItemId}/?provider={provider}&itemType={itemType}&workflowOperation={workflowOperation}")]
    [OperationContract]
    DynamicItemContext SaveDataItemInXml(
      DynamicItemContext dataItem,
      string dataItemId,
      string provider,
      string itemType,
      string workflowOperation);

    /// <summary>
    /// Gets the collection of dynamic data items that are children of the specified parent and returns the result in JSON format.
    /// </summary>
    /// <param name="parentId">The id of the parent of the items.</param>
    /// <param name="itemType">Type of the items to be obtained.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="sortExpression">Sort expression used to order the collection of dynamic data items.</param>
    /// <param name="skip">Number of items to skip before starting to take the result set.</param>
    /// <param name="take">Number of items to take in the result set.</param>
    /// <param name="filter">Filter expression used to filter which dynamic data items ought to be retrieved.</param>
    /// <returns>An instance of CollectionContext object that contains the collection of dynamic data items.</returns>
    [WebHelp(Comment = "Gets the collection of dynamic data items that are children of the specified parent and returns the result in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/parent/{parentId}/?provider={provider}&itemType={itemType}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    [OperationContract]
    CollectionContext<DynamicContent> GetChildDataItems(
      string parentId,
      string itemType,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter);

    /// <summary>
    /// Gets the collection of dynamic data items that are children of the specified parent and returns the result in XML format.
    /// </summary>
    /// <param name="parentId">The id of the parent of the items.</param>
    /// <param name="itemType">Type of the items to be obtained.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="sortExpression">Sort expression used to order the collection of dynamic data items.</param>
    /// <param name="skip">Number of items to skip before starting to take the result set.</param>
    /// <param name="take">Number of items to take in the result set.</param>
    /// <param name="filter">Filter expression used to filter which dynamic data items ought to be retrieved.</param>
    /// <returns>
    /// An instance of CollectionContext object that contains the collection of dynamic data items.
    /// </returns>
    [WebHelp(Comment = "Gets the collection of dynamic data items that are children of the specified parent and returns the result in XML format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/parent/{parentId}/?provider={provider}&itemType={itemType}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    [OperationContract]
    CollectionContext<DynamicContent> GetChildDataItemsInXml(
      string parentId,
      string itemType,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter);

    /// <summary>
    /// Gets the single dynamic data item which is a child of the specified parent and returns it in JSON format.
    /// </summary>
    /// <param name="parentId">The id parent of the parent item.</param>
    /// <param name="itemId">Id of the dynamic data item that ought to be retrieved.</param>
    /// <param name="providerName">Name of the provider that is to be used to retrieve the dynamic data item.</param>
    /// <param name="version">History version id of the item.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="duplicate">Determines whether the returned item will be duplicated later.</param>
    /// <param name="checkOut">The check out.</param>
    /// <returns>
    /// An instance of ItemContext that contains the dynamic data item to be retrieved.
    /// </returns>
    [OperationContract]
    [WebHelp(Comment = "Gets the single dynamic data item which is a child of the specified parent and returns it in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/parent/{parentId}/{itemId}/?provider={providerName}&version={version}&itemType={itemType}&duplicate={duplicate}&checkOut={checkOut}")]
    DynamicItemContext GetChildDataItem(
      string parentId,
      string itemId,
      string providerName,
      string version,
      string itemType,
      bool duplicate,
      bool checkOut = false);

    /// <summary>
    /// Gets the single dynamic data item which is a child of the specified parent and returns it in XML format.
    /// </summary>
    /// <param name="parentId">The id parent of the parent item.</param>
    /// <param name="itemId">Id of the dynamic data item that ought to be retrieved.</param>
    /// <param name="providerName">Name of the provider that is to be used to retrieve the dynamic data item.</param>
    /// <param name="version">History version id of the item.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="duplicate">Determines whether the returned item will be duplicated later.</param>
    /// <returns>
    /// An instance of ItemContext that contains the dynamic data item to be retrieved.
    /// </returns>
    [OperationContract]
    [WebHelp(Comment = "Gets the single dynamic data item which is a child of the specified parent and returns it in XML format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/parent/{parentId}/{itemId}/?provider={providerName}&version={version}&itemType={itemType}&duplicate={duplicate}")]
    DynamicItemContext GetChildDataItemInXml(
      string parentId,
      string itemId,
      string providerName,
      string version,
      string itemType,
      bool duplicate);

    /// <summary>
    /// Saves a dynamic data item. If the dynamic data item exists it updates it; otherwise creates a new dynamic data item, child of the specified parent.
    /// Returns the saved dynamic data item object in XML format.
    /// </summary>
    /// <param name="dataItem">Dynamic data item object to be saved.</param>
    /// <param name="parentId">Id of the item which is the parent of the item to be saved.</param>
    /// <param name="dataItemId">The data item id.</param>
    /// <param name="provider">Name of the dynamic module provider to be used when saving the dynamic data item.</param>
    /// <param name="itemType">Type of the item being saved.</param>
    /// <param name="workflowOperation">Workflow operation to be performed.</param>
    /// <param name="parentType">Type of the parent.</param>
    /// <param name="newParentId">Id of the new parent item.</param>
    /// <returns>
    /// An instance of <see cref="T:Telerik.Sitefinity.DynamicModules.Model.DynamicContent" /> that represents the saved dynamic data item object.
    /// </returns>
    [WebHelp(Comment = "Saves a dynamic data item. If the dynamic data item exists it updates it; otherwise creates a new dynamic data item. Returns the saved dynamic data item object in JSON format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/parent/{parentId}/{dataItemId}/?provider={provider}&itemType={itemType}&workflowOperation={workflowOperation}&parentType={parentType}&newParentId={newParentId}")]
    [OperationContract]
    DynamicItemContext SaveChildDataItem(
      DynamicItemContext dataItem,
      string parentId,
      string dataItemId,
      string provider,
      string itemType,
      string workflowOperation,
      string parentType,
      string newParentId);

    /// <summary>
    /// Saves a dynamic data item. If the dynamic data item exists it updates it; otherwise creates a new dynamic data item, child of the specified parent.
    /// Returns the saved dynamic data item object in XML format.
    /// </summary>
    /// <param name="dataItem">Dynamic data item object to be saved.</param>
    /// <param name="parentId">Id of the item which is the parent of the item to be saved.</param>
    /// <param name="dataItemId">The data item id.</param>
    /// <param name="provider">Name of the dynamic module provider to be used when saving the dynamic data item.</param>
    /// <param name="itemType">Type of the item being saved.</param>
    /// <param name="workflowOperation">Workflow operation to be performed.</param>
    /// <param name="parentType">Type of the parent.</param>
    /// <param name="newParentId">Id of the new parent item.</param>
    /// <returns>
    /// An instance of <see cref="T:Telerik.Sitefinity.DynamicModules.Model.DynamicContent" /> that represents the saved dynamic data item object.
    /// </returns>
    [WebHelp(Comment = "Saves a dynamic data item. If the dynamic data item exists it updates it; otherwise creates a new dynamic data item. Returns the saved dynamic data item object in XMl format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/parent/{parentId}/{dataItemId}/?provider={provider}&itemType={itemType}&workflowOperation={workflowOperation}&parentType={parentType}&newParentId={newParentId}")]
    [OperationContract]
    DynamicItemContext SaveChildDataItemInXml(
      DynamicItemContext dataItem,
      string parentId,
      string dataItemId,
      string provider,
      string itemType,
      string workflowOperation,
      string parentType,
      string newParentId);

    /// <summary>
    /// Deletes a child dynamic data item and returns true if the dynamic data item has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="parentId">Id of the parent data item that ought to be deleted.</param>
    /// <param name="dataItemId">Id of the data item that ought to be deleted.</param>
    /// <param name="provider">Name of the dynamic module provider to be used when deleting the dynamic data item.</param>
    /// <param name="itemType">The type of the data item to be deleted.</param>
    /// <param name="parentItemType">The parent item type of the data item to be deleted.</param>
    /// <param name="parentType">The parent type of the data item to be deleted.</param>
    /// <param name="deletedLanguage">The language version to delete. If null, all versions are deleted(e.g. the item itself is deleted).</param>
    /// <param name="version">The data item history version.</param>
    [WebHelp(Comment = "Deletes a child dynamic data item.")]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/parent/{parentId}/{dataItemId}/?provider={providerName}&itemType={itemType}&parentItemType={parentItemType}&parentType={parentType}&checkRelatingData={checkRelatingData}&language={deletedLanguage}&version={version}")]
    [OperationContract]
    bool DeleteChildDataItem(
      string parentId,
      string dataItemId,
      string providerName,
      string itemType,
      string parentItemType,
      string parentType,
      string deletedLanguage,
      bool checkRelatingData,
      string version);

    /// <summary>
    /// Deletes a child dynamic data item and returns true if the dynamic data item has been deleted; otherwise false.
    /// Result is returned in Xml format.
    /// </summary>
    /// <param name="parentId">Id of the parent data item that ought to be deleted.</param>
    /// <param name="dataItemId">Id of the data item that ought to be deleted.</param>
    /// <param name="provider">Name of the dynamic module provider to be used when deleting the dynamic data item.</param>
    /// <param name="itemType">The type of the data item to be deleted.</param>
    /// <param name="parentItemType">The parent item type of the data item to be deleted.</param>
    /// <param name="parentType">The parent type of the data item to be deleted.</param>
    /// <param name="deletedLanguage">The language version to delete. If null, all versions are deleted(e.g. the item itself is deleted).</param>
    /// <param name="version">The data item history version.</param>
    [WebHelp(Comment = "Deletes a child dynamic data item.")]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/parent/{parentId}/{dataItemId}/?provider={providerName}&itemType={itemType}&parentItemType={parentItemType}&parentType={parentType}&checkRelatingData={checkRelatingData}&language={deletedLanguage}&version={version}")]
    [OperationContract]
    bool DeleteChildDataItemXml(
      string parentId,
      string dataItemId,
      string providerName,
      string itemType,
      string parentItemType,
      string parentType,
      string deletedLanguage,
      bool checkRelatingData,
      string version);

    /// <summary>
    /// Deletes a dynamic data item and returns true if the dynamic data item has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="dataItemId">Id of the data item that ought to be deleted.</param>
    /// <param name="provider">Name of the dynamic module provider to be used when deleting the dynamic data item.</param>
    /// <param name="itemType">The type of the data item to be deleted.</param>
    /// <param name="deletedLanguage">The language version to delete. If null, all versions are deleted(e.g. the item itself is deleted).</param>
    /// <param name="version">The data item history version.</param>
    [WebHelp(Comment = "Deletes a dynamic data item.")]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{dataItemId}/?provider={provider}&itemType={itemType}&language={deletedLanguage}&checkRelatingData={checkRelatingData}&version={version}")]
    [OperationContract]
    bool DeleteDataItem(
      string dataItemId,
      string provider,
      string itemType,
      string deletedLanguage,
      bool checkRelatingData,
      string version);

    /// <summary>
    /// Deletes a dynamic data item and returns true if the dynamic data item has been deleted; otherwise false.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="dataItemId">Id of the data item that ought to be deleted.</param>
    /// <param name="provider">Name of the dynamic module provider to be used when deleting the dynamic data item.</param>
    /// <param name="itemType">The type of the data item to be deleted.</param>
    /// <param name="deletedLanguage">The language version to delete. If null, all versions are deleted(e.g. the item itself is deleted).</param>
    /// <param name="version">The data item history version.</param>
    [WebHelp(Comment = "Deletes a dynamic data item.")]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/{dataItemId}/?provider={provider}&itemType={itemType}&language={deletedLanguage}&checkRelatingData={checkRelatingData}&version={version}")]
    [OperationContract]
    bool DeleteDataItemInXml(
      string dataItemId,
      string provider,
      string itemType,
      string deletedLanguage,
      bool checkRelatingData,
      string version);

    /// <summary>
    /// Deletes a temp dynamic data item and returns true if the item has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="dataItemId">Id of the data item that ought to be deleted.</param>
    /// <param name="provider">Name of the dynamic module provider to be used when deleting the dynamic data item.</param>
    /// <param name="itemType">The type of the data item to be deleted.</param>
    /// <param name="deletedLanguage">The language version to delete. If null, all versions are deleted(e.g. the item itself is deleted).</param>
    [WebHelp(Comment = "Deletes a dynamic data item.")]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/temp/{dataItemId}/?provider={provider}&force={force}&item_type={itemType}")]
    [OperationContract]
    bool DeleteTempItem(string dataItemId, string provider, bool force, string itemType);

    /// <summary>
    /// Deletes a temp dynamic data item and returns true if the item has been deleted; otherwise false.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="dataItemId">Id of the data item that ought to be deleted.</param>
    /// <param name="provider">Name of the dynamic module provider to be used when deleting the dynamic data item.</param>
    /// <param name="itemType">The type of the data item to be deleted.</param>
    /// <param name="deletedLanguage">The language version to delete. If null, all versions are deleted(e.g. the item itself is deleted).</param>
    [WebHelp(Comment = "Deletes a dynamic data item.")]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/temp/{dataItemId}/?provider={provider}&force={force}&item_type={itemType}")]
    [OperationContract]
    bool DeleteTempItemInXml(string dataItemId, string provider, bool force, string itemType);

    /// <summary>
    /// Deletes an array of dynamic items. Result is returned in JSON format.
    /// </summary>
    /// <param name="Ids">An array containing the Ids of the dynamic items to be deleted.</param>
    /// <param name="providerName">Name of the dynamic module provider to be used when deleting the item.</param>
    /// <param name="workflowOperationm">The workflow operation.</param>
    /// <param name="deletedLanguage">The language version to delete. If null, all versions are deleted(e.g. the item itself is deleted).</param>
    /// 
    ///             /// <param name="itemType">Full name of the type to be deleted.</param>
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "batch/?provider={providerName}&workflowOperation={workflowOperation}&language={deletedLanguage}&itemType={itemType}&checkRelatingData={checkRelatingData}")]
    bool BatchDeleteItems(
      string[] Ids,
      string providerName,
      string workflowOperation,
      string deletedLanguage,
      string itemType,
      bool checkRelatingData);

    /// <summary>
    /// Deletes an array of dynamic items. Result is returned in XML format.
    /// </summary>
    /// <param name="Ids">An array containing the Ids of the dynamic items to be deleted.</param>
    /// <param name="providerName">Name of the dynamic module provider to be used when deleting the item.</param>
    /// <param name="workflowOperationm">The workflow operation.</param>
    /// <param name="deletedLanguage">The language version to delete. If null, all versions are deleted(e.g. the item itself is deleted).</param>
    /// <param name="itemType">Full name of the type to be deleted.</param>
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/batch/?provider={providerName}&workflowOperation={workflowOperation}&language={deletedLanguage}&itemType={itemType}&checkRelatingData={checkRelatingData}")]
    bool BatchDeleteItemsInXml(
      string[] Ids,
      string providerName,
      string workflowOperation,
      string deletedLanguage,
      string itemType,
      bool checkRelatingData);

    /// <summary>
    /// Deletes an array of dynamic items. Result is returned in JSON format.
    /// </summary>
    /// <param name="Ids">An array containing the Ids of the dynamic items to be deleted.</param>
    /// <param name="parentId">The id of the parent item.</param>
    /// <param name="providerName">Name of the dynamic module provider to be used when deleting the item.</param>
    /// <param name="workflowOperationm">The workflow operation.</param>
    /// <param name="deletedLanguage">The language version to delete. If null, all versions are deleted(e.g. the item itself is deleted).</param>
    /// 
    ///             /// <param name="itemType">Full name of the type to be deleted.</param>
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "parent/{parentId}/batch/?provider={providerName}&workflowOperation={workflowOperation}&language={deletedLanguage}&itemType={itemType}&checkRelatingData={checkRelatingData}")]
    bool BatchDeleteChildItems(
      string[] ids,
      string parentId,
      string providerName,
      string workflowOperation,
      string deletedLanguage,
      string itemType,
      bool checkRelatingData);

    /// <summary>
    /// Deletes an array of dynamic items. Result is returned in XML format.
    /// </summary>
    /// <param name="Ids">An array containing the Ids of the dynamic items to be deleted.</param>
    /// <param name="parentId">The id of the parent item.</param>
    /// <param name="providerName">Name of the dynamic module provider to be used when deleting the item.</param>
    /// <param name="workflowOperationm">The workflow operation.</param>
    /// <param name="deletedLanguage">The language version to delete. If null, all versions are deleted(e.g. the item itself is deleted).</param>
    /// <param name="itemType">Full name of the type to be deleted.</param>
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/parent/{parentId}/batch/?provider={providerName}&workflowOperation={workflowOperation}&language={deletedLanguage}&itemType={itemType}&checkRelatingData={checkRelatingData}")]
    bool BatchDeleteChildItemsInXml(
      string[] ids,
      string parentId,
      string providerName,
      string workflowOperation,
      string deletedLanguage,
      string itemType,
      bool checkRelatingData);

    /// <summary>Publishes a list of items, identitified by their IDs</summary>
    /// <param name="ids">Strigified IDs of the content items to publish</param>
    /// <param name="providerName">Name of the data provider that should be used</param>
    /// <returns>True on success, false on failure</returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "batch/publish/?provider={providerName}&workflowOperation={workflowOperation}&itemType={itemType}")]
    bool BatchPublish(
      string[] ids,
      string providerName,
      string workflowOperation,
      string itemType);

    /// <summary>Publishes a list of items, identitified by their IDs</summary>
    /// <param name="ids">Strigified IDs of the content items to publish</param>
    /// <param name="providerName">Name of the data provider that should be used</param>
    /// <returns>True on success, false on failure</returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/batch/publish/?provider={providerName}&workflowOperation={workflowOperation}&itemType={itemType}")]
    bool BatchPublishInXml(
      string[] ids,
      string providerName,
      string workflowOperation,
      string itemType);

    /// <summary>Publishes a list of items, identitified by their IDs</summary>
    /// <param name="ids">Strigified IDs of the content items to publish</param>
    /// <param name="parentId">The id of the parent item.</param>
    /// <param name="providerName">Name of the data provider that should be used</param>
    /// <returns>True on success, false on failure</returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "parent/{parentId}/batch/publish/?provider={providerName}&workflowOperation={workflowOperation}&itemType={itemType}")]
    bool BatchPublishChildItems(
      string[] ids,
      string parentId,
      string providerName,
      string workflowOperation,
      string itemType);

    /// <summary>Publishes a list of items, identitified by their IDs</summary>
    /// <param name="ids">Strigified IDs of the content items to publish</param>
    /// <param name="parentId">The id of the parent item.</param>
    /// <param name="providerName">Name of the data provider that should be used</param>
    /// <returns>True on success, false on failure</returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/parent/{parentId}/batch/publish/?provider={providerName}&workflowOperation={workflowOperation}&itemType={itemType}")]
    bool BatchPublishChildItemsInXml(
      string[] ids,
      string parentId,
      string providerName,
      string workflowOperation,
      string itemType);

    /// <summary>
    /// Unpublishes a list of items, identitified by their IDs
    /// </summary>
    /// <param name="ids">Strigified IDs of the content items to unpublish</param>
    /// <param name="providerName">Name of the data provider that should be used</param>
    /// <returns>True on success, false on failure</returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "batch/unpublish/?provider={providerName}&workflowOperation={workflowOperation}&itemType={itemType}")]
    bool BatchUnpublish(
      string[] ids,
      string providerName,
      string workflowOperation,
      string itemType);

    /// <summary>
    /// Unpublishes a list of items, identitified by their IDs
    /// </summary>
    /// <param name="ids">Strigified IDs of the content items to unpublish</param>
    /// <param name="providerName">Name of the data provider that should be used</param>
    /// <returns>True on success, false on failure</returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/batch/unpublish/?provider={providerName}&workflowOperation={workflowOperation}&itemType={itemType}")]
    bool BatchUnpublishInXml(
      string[] ids,
      string providerName,
      string workflowOperation,
      string itemType);

    /// <summary>
    /// Unpublishes a list of items, identitified by their IDs
    /// </summary>
    /// <param name="ids">Strigified IDs of the content items to unpublish</param>
    /// <param name="parentId">The id of the parent item.</param>
    /// <param name="providerName">Name of the data provider that should be used</param>
    /// <returns>True on success, false on failure</returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/parent/{parentId}/batch/unpublish/?provider={providerName}&workflowOperation={workflowOperation}&itemType={itemType}")]
    bool BatchUnpublishChildItems(
      string[] ids,
      string parentId,
      string providerName,
      string workflowOperation,
      string itemType);

    /// <summary>
    /// Unpublishes a list of items, identitified by their IDs
    /// </summary>
    /// <param name="ids">Strigified IDs of the content items to unpublish</param>
    /// <param name="parentId">The id of the parent item.</param>
    /// <param name="providerName">Name of the data provider that should be used</param>
    /// <returns>True on success, false on failure</returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/parent/{parentId}/batch/unpublish/?provider={providerName}&workflowOperation={workflowOperation}&itemType={itemType}")]
    bool BatchUnpublishChildItemsInXml(
      string[] ids,
      string parentId,
      string providerName,
      string workflowOperation,
      string itemType);

    /// <summary>
    /// Gets the path of items up to the root item, starting with the designated item and returns a collection
    /// of <see cref="T:Telerik.Sitefinity.DynamicModules.Model.DynamicContent" /> in JSON format.
    /// </summary>
    /// <param name="itemId">The items id.</param>
    /// <param name="pageFilter">The page filter.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="sortExpression">The sort expression.</param>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "predecessor/{itemId}/?provider={providerName}&itemType={itemType}&filter={filter}")]
    CollectionContext<DynamicContent> GetPredecessorItems(
      string itemId,
      string itemType,
      string providerName,
      string filter);

    /// <summary>
    /// Gets the path of items up to the root item, starting with the designated item and returns a collection
    /// of <see cref="T:Telerik.Sitefinity.DynamicModules.Model.DynamicContent" /> in XML format.
    /// </summary>
    /// <param name="itemId">The items id.</param>
    /// <param name="pageFilter">The page filter.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="sortExpression">The sort expression.</param>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/predecessor/{itemId}/?provider={providerName}&itemType={itemType}&filter={filter}")]
    CollectionContext<DynamicContent> GetPredecessorItemsInXml(
      string itemId,
      string itemType,
      string providerName,
      string filter);

    /// <summary>Gets the items as tree in JSON format.</summary>
    /// <param name="leafIds">The leaf ids.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="childLimit">The child limit.</param>
    /// <param name="perLevelLimit">The per level limit.</param>
    /// <param name="perSubtreeLimit">The per subtree limit.</param>
    /// <param name="subtreesLimit">The subtrees limit.</param>
    /// <param name="root">The root.</param>
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "tree/?provider={provider}&childLimit={childLimit}&perLevelLimit={perLevelLimit}&perSubtreeLimit={perSubtreeLimit}&subtreesLimit={subtreesLimit}&root={root}&itemType={itemType}&filter={filter}")]
    CollectionContext<DynamicContent> GetItemsAsTree(
      string[] leafIds,
      string provider,
      int childLimit,
      int perLevelLimit,
      int perSubtreeLimit,
      int subtreesLimit,
      string root,
      string itemType,
      string filter);

    /// <summary>Gets the items as tree in XML format.</summary>
    /// <param name="leafIds">The leaf ids.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="childLimit">The child limit.</param>
    /// <param name="perLevelLimit">The per level limit.</param>
    /// <param name="perSubtreeLimit">The per subtree limit.</param>
    /// <param name="subtreesLimit">The subtrees limit.</param>
    /// <param name="root">The root.</param>
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/tree/?provider={provider}&childLimit={childLimit}&perLevelLimit={perLevelLimit}&perSubtreeLimit={perSubtreeLimit}&subtreesLimit={subtreesLimit}&root={root}&itemType={itemType}&filter={filter}")]
    CollectionContext<DynamicContent> GetItemsAsTreeInXml(
      string[] leafIds,
      string provider,
      int childLimit,
      int perLevelLimit,
      int perSubtreeLimit,
      int subtreesLimit,
      string root,
      string itemType,
      string filter);

    /// <summary>
    /// Gets the child items which belong to the specified parent in JSON format.
    /// </summary>
    /// <param name="parentId">The parent id.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="filter">The filter.</param>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "children/{parentId}/?provider={provider}&filter={filter}&itemType={itemType}")]
    CollectionContext<DynamicContent> GetChildItems(
      string parentId,
      string provider,
      string filter,
      string itemType);

    /// <summary>
    /// Gets the child items which belong to the specified parent in XML format.
    /// </summary>
    /// <param name="parentId">The parent id.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="filter">The filter.</param>
    /// <param name="itemType">Type of the item.</param>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/children/{parentId}/?provider={provider}&filter={filter}&itemType={itemType}")]
    CollectionContext<DynamicContent> GetChildItemsInXml(
      string parentId,
      string provider,
      string filter,
      string itemType);
  }
}
