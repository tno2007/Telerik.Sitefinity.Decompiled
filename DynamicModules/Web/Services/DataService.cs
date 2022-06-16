// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Web.Services.DataService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.ContentLinks;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Builder.Web.UI;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Fluent;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.GeoLocations;
using Telerik.Sitefinity.GeoLocations.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.Localization;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.RelatedData.Messages;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Versioning;
using Telerik.Sitefinity.Versioning.Model;
using Telerik.Sitefinity.Versioning.Web.Services;
using Telerik.Sitefinity.Web.Services;
using Telerik.Sitefinity.Workflow;

namespace Telerik.Sitefinity.DynamicModules.Web.Services
{
  /// <summary>
  /// Web service that provides methods for working with dynamic data of the dynamic modules.
  /// </summary>
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  internal class DataService : IDataService
  {
    internal const string ContentFiltersKey = "sfContentFilters";

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
    public DynamicItemContext GetDataItem(
      string itemId,
      string providerName,
      string version,
      string itemType,
      bool duplicate,
      bool checkOut)
    {
      ServiceUtility.RequestAuthentication();
      return this.GetDataItemInternal(itemId, providerName, version, itemType, duplicate, checkOut: checkOut);
    }

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
    public DynamicItemContext GetDataItemInXml(
      string itemId,
      string providerName,
      string itemType,
      bool duplicate)
    {
      ServiceUtility.RequestAuthentication();
      return this.GetDataItemInternal(itemId, providerName, string.Empty, itemType, duplicate);
    }

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
    public CollectionContext<DynamicContent> GetDataItems(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string itemType,
      bool hierarchyMode = false)
    {
      ServiceUtility.RequestAuthentication();
      return this.GetDataItemsInternal(provider, sortExpression, skip, take, filter, itemType, hierarchyMode);
    }

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
    public CollectionContext<DynamicContent> GetDataItemsInXml(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string itemType,
      bool hierarchyMode = false)
    {
      ServiceUtility.RequestAuthentication();
      return this.GetDataItemsInternal(provider, sortExpression, skip, take, filter, itemType, hierarchyMode);
    }

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
    public CollectionContext<DynamicContent> GetLiveDataItems(
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
      bool published = false)
    {
      ServiceUtility.RequestAuthentication();
      return this.GetLiveDataItemsInternal(provider, sortExpression, skip, take, filter, itemType, latitude, longitude, radius, geoLocationProperty, published);
    }

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
    public CollectionContext<DynamicContent> GetLiveDataItemsInXml(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string itemType)
    {
      ServiceUtility.RequestAuthentication();
      return this.GetLiveDataItemsInternal(provider, sortExpression, skip, take, filter, itemType);
    }

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
    public DynamicItemContext SaveDataItem(
      DynamicItemContext dataItem,
      string dataItemId,
      string provider,
      string itemType,
      string workflowOperation)
    {
      ServiceUtility.ProtectBackendServices();
      ServiceUtility.RequestBackendUserAuthentication();
      return this.SaveDataItemInternal(dataItem, itemType, provider, workflowOperation);
    }

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
    public DynamicItemContext SaveDataItemInXml(
      DynamicItemContext dataItem,
      string dataItemId,
      string provider,
      string itemType,
      string workflowOperation)
    {
      ServiceUtility.ProtectBackendServices();
      ServiceUtility.RequestBackendUserAuthentication();
      return this.SaveDataItemInternal(dataItem, itemType, provider, workflowOperation);
    }

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
    public CollectionContext<DynamicContent> GetChildDataItems(
      string parentId,
      string itemType,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      ServiceUtility.RequestAuthentication();
      return this.GetChildDataItemsInternal(parentId, itemType, provider, sortExpression, skip, take, filter);
    }

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
    /// <returns>An instance of CollectionContext object that contains the collection of dynamic data items.</returns>
    public CollectionContext<DynamicContent> GetChildDataItemsInXml(
      string parentId,
      string itemType,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      ServiceUtility.RequestAuthentication();
      return this.GetChildDataItemsInternal(parentId, itemType, provider, sortExpression, skip, take, filter);
    }

    /// <summary>
    /// Gets the single dynamic data item which is a child of the specified parent and returns it in JSON format.
    /// </summary>
    /// <param name="parentId">The id parent of the parent item.</param>
    /// <param name="itemId">Id of the dynamic data item that ought to be retrieved.</param>
    /// <param name="providerName">Name of the provider that is to be used to retrieve the dynamic data item.</param>
    /// <param name="version">History version id of the item.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="checkOut">The check out.</param>
    /// <param name="duplicate">Determines whether the returned item will be duplicated later.</param>
    /// <returns>
    /// An instance of ItemContext that contains the dynamic data item to be retrieved.
    /// </returns>
    public DynamicItemContext GetChildDataItem(
      string parentId,
      string itemId,
      string providerName,
      string version,
      string itemType,
      bool duplicate,
      bool checkOut)
    {
      ServiceUtility.RequestAuthentication();
      return this.GetDataItemInternal(itemId, providerName, version, itemType, duplicate, parentId, checkOut);
    }

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
    public DynamicItemContext GetChildDataItemInXml(
      string parentId,
      string itemId,
      string providerName,
      string version,
      string itemType,
      bool duplicate)
    {
      ServiceUtility.RequestAuthentication();
      return this.GetDataItemInternal(itemId, providerName, version, itemType, duplicate, parentId);
    }

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
    public DynamicItemContext SaveChildDataItem(
      DynamicItemContext dataItem,
      string parentId,
      string dataItemId,
      string provider,
      string itemType,
      string workflowOperation,
      string parentType,
      string newParentId)
    {
      ServiceUtility.ProtectBackendServices();
      ServiceUtility.RequestBackendUserAuthentication();
      return this.SaveDataItemInternal(dataItem, itemType, provider, workflowOperation, parentId, parentType, newParentId);
    }

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
    public DynamicItemContext SaveChildDataItemInXml(
      DynamicItemContext dataItem,
      string parentId,
      string dataItemId,
      string provider,
      string itemType,
      string workflowOperation,
      string parentType,
      string newParentId)
    {
      ServiceUtility.ProtectBackendServices();
      ServiceUtility.RequestBackendUserAuthentication();
      return this.SaveDataItemInternal(dataItem, itemType, provider, workflowOperation, parentId, parentType, newParentId);
    }

    /// <summary>
    /// Deletes a dynamic data item and returns true if the dynamic data item has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="dataItemId">Id of the data item that ought to be deleted.</param>
    /// <param name="provider">Name of the dynamic module provider to be used when deleting the dynamic data item.</param>
    /// <param name="itemType">The type of the data item to be deleted.</param>
    /// <param name="deletedLanguage">The language version to delete. If null, all versions are deleted(e.g. the item itself is deleted).</param>
    /// <param name="version">The data item history version.</param>
    public bool DeleteDataItem(
      string dataItemId,
      string provider,
      string itemType,
      string deletedLanguage,
      bool checkRelatingData,
      string version)
    {
      ServiceUtility.ProtectBackendServices();
      ServiceUtility.RequestBackendUserAuthentication();
      return this.DeleteDataItemInternal(dataItemId, provider, itemType, deletedLanguage, checkRelatingData, version);
    }

    /// <summary>
    /// Deletes a dynamic data item and returns true if the dynamic data item has been deleted; otherwise false.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="dataItemId">Id of the data item that ought to be deleted.</param>
    /// <param name="provider">Name of the dynamic module provider to be used when deleting the dynamic data item.</param>
    /// <param name="itemType">The type of the data item to be deleted.</param>
    /// <param name="deletedLanguage">The language version to delete. If null, all versions are deleted(e.g. the item itself is deleted).</param>
    /// <param name="version">The data item history version.</param>
    public bool DeleteDataItemInXml(
      string dataItemId,
      string provider,
      string itemType,
      string deletedLanguage,
      bool checkRelatingData,
      string version)
    {
      ServiceUtility.ProtectBackendServices();
      ServiceUtility.RequestBackendUserAuthentication();
      return this.DeleteDataItemInternal(dataItemId, provider, itemType, deletedLanguage, checkRelatingData, version);
    }

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
    public bool DeleteChildDataItem(
      string parentId,
      string dataItemId,
      string providerName,
      string itemType,
      string parentItemType,
      string parentType,
      string deletedLanguage,
      bool checkRelatingData,
      string version)
    {
      ServiceUtility.ProtectBackendServices();
      ServiceUtility.RequestBackendUserAuthentication();
      return this.DeleteDataItemInternal(dataItemId, providerName, itemType, deletedLanguage, checkRelatingData, version);
    }

    /// <summary>
    /// Deletes a child dynamic data item and returns true if the dynamic data item has been deleted; otherwise false.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="parentId">Id of the parent data item that ought to be deleted.</param>
    /// <param name="dataItemId">Id of the data item that ought to be deleted.</param>
    /// <param name="provider">Name of the dynamic module provider to be used when deleting the dynamic data item.</param>
    /// <param name="itemType">The type of the data item to be deleted.</param>
    /// <param name="parentItemType">The parent item type of the data item to be deleted.</param>
    /// <param name="parentType">The parent type of the data item to be deleted.</param>
    /// <param name="deletedLanguage">The language version to delete. If null, all versions are deleted(e.g. the item itself is deleted).</param>
    /// <param name="version">The data item history version.</param>
    public bool DeleteChildDataItemXml(
      string parentId,
      string dataItemId,
      string providerName,
      string itemType,
      string parentItemType,
      string parentType,
      string deletedLanguage,
      bool checkRelatingData,
      string version)
    {
      ServiceUtility.ProtectBackendServices();
      ServiceUtility.RequestBackendUserAuthentication();
      return this.DeleteDataItemInternal(dataItemId, providerName, itemType, deletedLanguage, checkRelatingData, version);
    }

    /// <summary>
    /// Deletes a temp dynamic data item and returns true if the item has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="dataItemId">Id of the data item that ought to be deleted.</param>
    /// <param name="provider">Name of the dynamic module provider to be used when deleting the dynamic data item.</param>
    /// <param name="force">Forces the deletion of the item</param>
    public bool DeleteTempItem(string dataItemId, string provider, bool force, string itemType)
    {
      ServiceUtility.ProtectBackendServices();
      ServiceUtility.RequestBackendUserAuthentication();
      return this.DeleteTempItemInternal(dataItemId, provider, force, itemType);
    }

    /// <summary>
    /// Deletes a temp dynamic data item and returns true if the item has been deleted; otherwise false.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="dataItemId">Id of the data item that ought to be deleted.</param>
    /// <param name="provider">Name of the dynamic module provider to be used when deleting the dynamic data item.</param>
    /// <param name="force">Forces the deletion of the item</param>
    public bool DeleteTempItemInXml(
      string dataItemId,
      string provider,
      bool force,
      string itemType)
    {
      ServiceUtility.ProtectBackendServices();
      ServiceUtility.RequestBackendUserAuthentication();
      return this.DeleteTempItemInternal(dataItemId, provider, force, itemType);
    }

    /// <summary>
    /// Deletes an array of dynamic items. Result is returned in JSON format.
    /// </summary>
    /// <param name="Ids">An array containing the Ids of the dynamic items to be deleted.</param>
    /// <param name="providerName">Name of the dynamic module provider to be used when deleting the item.</param>
    /// <param name="workflowOperationm">The workflow operation.</param>
    /// <param name="deletedLanguage">The language version to delete. If null, all versions are deleted(e.g. the item itself is deleted).</param>
    /// <param name="itemType">Full name of the type to be deleted.</param>
    public bool BatchDeleteItems(
      string[] Ids,
      string providerName,
      string workflowOperation,
      string deletedLanguage,
      string itemType,
      bool checkRelatingData)
    {
      ServiceUtility.ProtectBackendServices();
      ServiceUtility.RequestBackendUserAuthentication();
      return this.BatchDeleteItemsInternal(Ids, providerName, workflowOperation, deletedLanguage, itemType, checkRelatingData);
    }

    /// <summary>
    /// Deletes an array of dynamic items. Result is returned in XML format.
    /// </summary>
    /// <param name="Ids">An array containing the Ids of the dynamic items to be deleted.</param>
    /// <param name="providerName">Name of the dynamic module provider to be used when deleting the item.</param>
    /// <param name="workflowOperationm">The workflow operation.</param>
    /// <param name="deletedLanguage">The language version to delete. If null, all versions are deleted(e.g. the item itself is deleted).</param>
    /// <param name="itemType">Full name of the type to be deleted.</param>
    public bool BatchDeleteItemsInXml(
      string[] Ids,
      string providerName,
      string workflowOperation,
      string deletedLanguage,
      string itemType,
      bool checkRelatingData = false)
    {
      ServiceUtility.ProtectBackendServices();
      ServiceUtility.RequestBackendUserAuthentication();
      return this.BatchDeleteItemsInternal(Ids, providerName, workflowOperation, deletedLanguage, itemType, checkRelatingData);
    }

    /// <summary>
    /// Deletes an array of dynamic items. Result is returned in JSON format.
    /// </summary>
    /// <param name="Ids">An array containing the Ids of the dynamic items to be deleted.</param>
    /// <param name="parentId">The id of the parent item.</param>
    /// <param name="providerName">Name of the dynamic module provider to be used when deleting the item.</param>
    /// <param name="workflowOperationm">The workflow operation.</param>
    /// <param name="deletedLanguage">The language version to delete. If null, all versions are deleted(e.g. the item itself is deleted).</param>
    /// <param name="itemType">Full name of the type to be deleted.</param>
    public bool BatchDeleteChildItems(
      string[] ids,
      string parentId,
      string providerName,
      string workflowOperation,
      string deletedLanguage,
      string itemType,
      bool checkRelatingData)
    {
      ServiceUtility.ProtectBackendServices();
      ServiceUtility.RequestBackendUserAuthentication();
      return this.BatchDeleteItemsInternal(ids, providerName, workflowOperation, deletedLanguage, itemType, checkRelatingData);
    }

    /// <summary>
    /// Deletes an array of dynamic items. Result is returned in XML format.
    /// </summary>
    /// <param name="Ids">An array containing the Ids of the dynamic items to be deleted.</param>
    /// <param name="parentId">The id of the parent item.</param>
    /// <param name="providerName">Name of the dynamic module provider to be used when deleting the item.</param>
    /// <param name="workflowOperationm">The workflow operation.</param>
    /// <param name="deletedLanguage">The language version to delete. If null, all versions are deleted(e.g. the item itself is deleted).</param>
    /// <param name="itemType">Full name of the type to be deleted.</param>
    public bool BatchDeleteChildItemsInXml(
      string[] ids,
      string parentId,
      string providerName,
      string workflowOperation,
      string deletedLanguage,
      string itemType,
      bool checkRelatingData)
    {
      ServiceUtility.ProtectBackendServices();
      ServiceUtility.RequestBackendUserAuthentication();
      return this.BatchDeleteItemsInternal(ids, providerName, workflowOperation, deletedLanguage, itemType, checkRelatingData);
    }

    /// <summary>Publishes a list of items, identitified by their IDs</summary>
    /// <param name="ids">Strigified IDs of the content items to publish</param>
    /// <param name="providerName">Name of the data provider that should be used</param>
    /// <returns>True on success, false on failure</returns>
    public bool BatchPublish(
      string[] ids,
      string providerName,
      string workflowOperation,
      string itemType)
    {
      ServiceUtility.ProtectBackendServices();
      ServiceUtility.RequestBackendUserAuthentication();
      return this.BatchPublishInternal(ids, providerName, workflowOperation, itemType);
    }

    /// <summary>Publishes a list of items, identitified by their IDs</summary>
    /// <param name="ids">Strigified IDs of the content items to publish</param>
    /// <param name="providerName">Name of the data provider that should be used</param>
    /// <returns>True on success, false on failure</returns>
    public bool BatchPublishInXml(
      string[] ids,
      string providerName,
      string workflowOperation,
      string itemType)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      return this.BatchPublishInternal(ids, providerName, workflowOperation, itemType);
    }

    /// <summary>Publishes a list of items, identitified by their IDs</summary>
    /// <param name="ids">Strigified IDs of the content items to publish</param>
    /// <param name="parentId">The id of the parent item.</param>
    /// <param name="providerName">Name of the data provider that should be used</param>
    /// <returns>True on success, false on failure</returns>
    public bool BatchPublishChildItems(
      string[] ids,
      string parentId,
      string providerName,
      string workflowOperation,
      string itemType)
    {
      ServiceUtility.ProtectBackendServices();
      ServiceUtility.RequestBackendUserAuthentication();
      return this.BatchPublishInternal(ids, providerName, workflowOperation, itemType);
    }

    /// <summary>Publishes a list of items, identitified by their IDs</summary>
    /// <param name="ids">Strigified IDs of the content items to publish</param>
    /// <param name="parentId">The id of the parent item.</param>
    /// <param name="providerName">Name of the data provider that should be used</param>
    /// <returns>True on success, false on failure</returns>
    public bool BatchPublishChildItemsInXml(
      string[] ids,
      string parentId,
      string providerName,
      string workflowOperation,
      string itemType)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      return this.BatchPublishInternal(ids, providerName, workflowOperation, itemType);
    }

    /// <summary>
    /// Unpublishes a list of items, identitified by their IDs
    /// </summary>
    /// <param name="ids">Strigified IDs of the content items to unpublish</param>
    /// <param name="providerName">Name of the data provider that should be used</param>
    /// <returns>True on success, false on failure</returns>
    public bool BatchUnpublish(
      string[] ids,
      string providerName,
      string workflowOperation,
      string itemType)
    {
      ServiceUtility.ProtectBackendServices();
      ServiceUtility.RequestBackendUserAuthentication();
      return this.BatchUnpublishInternal(ids, providerName, workflowOperation, itemType);
    }

    /// <summary>
    /// Unpublishes a list of items, identitified by their IDs
    /// </summary>
    /// <param name="ids">Strigified IDs of the content items to unpublish</param>
    /// <param name="providerName">Name of the data provider that should be used</param>
    /// <returns>True on success, false on failure</returns>
    public bool BatchUnpublishInXml(
      string[] ids,
      string providerName,
      string workflowOperation,
      string itemType)
    {
      ServiceUtility.ProtectBackendServices();
      ServiceUtility.RequestBackendUserAuthentication();
      return this.BatchUnpublishInternal(ids, providerName, workflowOperation, itemType);
    }

    /// <summary>
    /// Unpublishes a list of items, identitified by their IDs
    /// </summary>
    /// <param name="ids">Strigified IDs of the content items to unpublish</param>
    /// <param name="parentId">The id of the parent item.</param>
    /// <param name="providerName">Name of the data provider that should be used</param>
    /// <returns>True on success, false on failure</returns>
    public bool BatchUnpublishChildItems(
      string[] ids,
      string parentId,
      string providerName,
      string workflowOperation,
      string itemType)
    {
      ServiceUtility.ProtectBackendServices();
      ServiceUtility.RequestBackendUserAuthentication();
      return this.BatchUnpublishInternal(ids, providerName, workflowOperation, itemType);
    }

    /// <summary>
    /// Unpublishes a list of items, identitified by their IDs
    /// </summary>
    /// <param name="ids">Strigified IDs of the content items to unpublish</param>
    /// <param name="parentId">The id of the parent item.</param>
    /// <param name="providerName">Name of the data provider that should be used</param>
    /// <returns>True on success, false on failure</returns>
    public bool BatchUnpublishChildItemsInXml(
      string[] ids,
      string parentId,
      string providerName,
      string workflowOperation,
      string itemType)
    {
      ServiceUtility.ProtectBackendServices();
      ServiceUtility.RequestBackendUserAuthentication();
      return this.BatchUnpublishInternal(ids, providerName, workflowOperation, itemType);
    }

    /// <summary>
    /// Gets the path of items up to the root item, starting with the designated item and returns a collection
    /// of <see cref="T:Telerik.Sitefinity.DynamicModules.Model.DynamicContent" /> in JSON format.
    /// </summary>
    /// <param name="itemId">The items id.</param>
    /// <param name="pageFilter">The page filter.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="sortExpression">The sort expression.</param>
    public CollectionContext<DynamicContent> GetPredecessorItems(
      string itemId,
      string itemType,
      string provider,
      string filter)
    {
      ServiceUtility.RequestAuthentication();
      return this.GetPredecessorItemsInternal(itemId, itemType, provider, filter);
    }

    /// <summary>
    /// Gets the path of items up to the root item, starting with the designated item and returns a collection
    /// of <see cref="T:Telerik.Sitefinity.DynamicModules.Model.DynamicContent" /> in XML format.
    /// </summary>
    /// <param name="itemId">The items id.</param>
    /// <param name="pageFilter">The page filter.</param>
    /// <param name="provider">The provider.</param>
    public CollectionContext<DynamicContent> GetPredecessorItemsInXml(
      string itemId,
      string itemType,
      string provider,
      string filter)
    {
      ServiceUtility.RequestAuthentication();
      return this.GetPredecessorItemsInternal(itemId, itemType, provider, filter);
    }

    /// <summary>Gets the items as tree in JSON format.</summary>
    /// <param name="leafIds">The leaf ids.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="childLimit">The child limit.</param>
    /// <param name="perLevelLimit">The per level limit.</param>
    /// <param name="perSubtreeLimit">The per subtree limit.</param>
    /// <param name="subtreesLimit">The subtrees limit.</param>
    /// <param name="root">The root.</param>
    public CollectionContext<DynamicContent> GetItemsAsTree(
      string[] leafIds,
      string provider,
      int childLimit,
      int perLevelLimit,
      int perSubtreeLimit,
      int subtreesLimit,
      string root,
      string itemType,
      string filter)
    {
      ServiceUtility.RequestAuthentication();
      return this.GetItemsAsTreeInternal(leafIds, provider, childLimit, perSubtreeLimit, itemType, filter);
    }

    /// <summary>Gets the items as tree in XML format.</summary>
    /// <param name="leafIds">The leaf ids.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="childLimit">The child limit.</param>
    /// <param name="perLevelLimit">The per level limit.</param>
    /// <param name="perSubtreeLimit">The per subtree limit.</param>
    /// <param name="subtreesLimit">The subtrees limit.</param>
    /// <param name="root">The root.</param>
    public CollectionContext<DynamicContent> GetItemsAsTreeInXml(
      string[] leafIds,
      string provider,
      int childLimit,
      int perLevelLimit,
      int perSubtreeLimit,
      int subtreesLimit,
      string root,
      string itemType,
      string filter)
    {
      ServiceUtility.RequestAuthentication();
      return this.GetItemsAsTreeInternal(leafIds, provider, childLimit, perSubtreeLimit, itemType, filter);
    }

    /// <summary>
    /// Gets the child items which belong to the specified parent in JSON format.
    /// </summary>
    /// <param name="parentId">The parent id.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="filter">The filter.</param>
    /// <param name="itemType"></param>
    public CollectionContext<DynamicContent> GetChildItems(
      string parentId,
      string provider,
      string filter,
      string itemType)
    {
      ServiceUtility.RequestAuthentication();
      return this.GetChildDataItemsInternal(parentId, itemType, provider, (string) null, 0, 0, (string) null);
    }

    /// <summary>
    /// Gets the child items which belong to the specified parent in XML format.
    /// </summary>
    /// <param name="parentId">The parent id.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="filter">The filter.</param>
    /// <param name="itemType">Type of the item.</param>
    public CollectionContext<DynamicContent> GetChildItemsInXml(
      string parentId,
      string provider,
      string filter,
      string itemType)
    {
      ServiceUtility.RequestAuthentication();
      return this.GetChildDataItemsInternal(parentId, itemType, provider, (string) null, 0, 0, (string) null);
    }

    private DynamicItemContext GetDataItemInternal(
      string itemId,
      string providerName,
      string version,
      string itemType,
      bool duplicate,
      string parentId = null,
      bool checkOut = false)
    {
      DynamicItemContext context = new DynamicItemContext();
      if (parentId != null && !parentId.IsGuid())
        throw new ArgumentException("parentId must be a valid GUID.");
      DynamicModuleManager manager1 = DynamicModuleManager.GetManager(providerName);
      Type type = TypeResolutionService.ResolveType(itemType);
      DynamicContent dataItem = manager1.GetDataItem(type, new Guid(itemId));
      this.GetDynamicModuleType(type);
      if (parentId != null && dataItem.SystemParentId != new Guid(parentId))
        throw new ArgumentException("The specified items is not a child of the specified parent");
      if (checkOut)
      {
        DynamicContent dynamicContent = manager1.Lifecycle.GetTemp((ILifecycleDataItem) dataItem) as DynamicContent;
        ILifecycleDataItem master = manager1.Lifecycle.GetMaster((ILifecycleDataItem) dataItem);
        if (dynamicContent == null || dynamicContent.Owner == Guid.Empty)
        {
          dynamicContent = manager1.Lifecycle.CheckOut(master) as DynamicContent;
          manager1.SaveChanges();
        }
        context.Item = dynamicContent;
      }
      else
        context.Item = dataItem;
      this.PopulateItems(context.Item, type, manager1);
      ILifecycleDataItem live = manager1.Lifecycle.GetLive((ILifecycleDataItem) dataItem);
      if (live != null && live.IsPublishedInCulture())
        context.HasLiveVersion = true;
      if (!string.IsNullOrWhiteSpace(version))
      {
        VersionManager manager2 = VersionManager.GetManager();
        Guid guid = new Guid(version);
        context.Item.ResetPropertiesToDefaultValue();
        manager2.GetSpecificVersionByChangeId((object) context.Item, guid);
        context.Item.Status = ContentLifecycleStatus.Temp;
        Change change = manager2.GetItem(typeof (Change), guid) as Change;
        context.VersionInfo = new WcfChange(change);
        Change nextChange = manager2.GetNextChange(change);
        if (nextChange != null)
          context.VersionInfo.NextId = nextChange.Id.ToString();
        Change previousChange = manager2.GetPreviousChange(change);
        if (previousChange != null)
          context.VersionInfo.PreviousId = previousChange.Id.ToString();
      }
      context.Warnings = SystemManager.StatusProviderRegistry.GetWarnings(new Guid(itemId), type, providerName).Select<WarningInfo, ItemWarning>((Func<WarningInfo, ItemWarning>) (w => new ItemWarning(w)));
      ServiceUtility.DisableCache();
      if (duplicate)
        ServiceUtility.SetUpAsDuplicate(context);
      if (!SystemManager.HttpContextItems.Contains((object) "sfContentFilters"))
        SystemManager.HttpContextItems.Add((object) "sfContentFilters", (object) new string[1]
        {
          "DraftLinksParser"
        });
      return context;
    }

    private CollectionContext<DynamicContent> GetDataItemsInternal(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string itemType,
      bool hierarchyMode)
    {
      Type dataItemType = TypeResolutionService.ResolveType(itemType);
      if (string.IsNullOrEmpty(provider))
      {
        ISite currentSite = SystemManager.CurrentContext.CurrentSite;
        provider = DynamicModuleManager.GetDefaultProviderName(ModuleBuilderManager.GetModuleNameFromType(itemType, currentSite.Id.ToString()));
      }
      DynamicModuleManager manager = DynamicModuleManager.GetManager(provider);
      if (hierarchyMode)
        return this.GetChildDataItemsInternal(Guid.Empty.ToString(), itemType, provider, sortExpression, skip, take, filter);
      int? totalCount = new int?(0);
      CultureInfo culture;
      CommonMethods.MatchCultureFilter(ref filter, out culture);
      IQueryable<DynamicContent> queryable = LifecycleFilters.OnlyPublishedMasterItems<DynamicContent>(this.FilterItemsByDynamicProperty(manager, dataItemType, ref filter));
      string filterName = string.Empty;
      if (this.TryGetNamedFilter(dataItemType.BaseType.IsILifecycle(), ref filter, out filterName))
      {
        CultureInfo sitefinityCulture = culture.GetSitefinityCulture();
        queryable = NamedFiltersHandler.ApplyFilterToDynamicContent<DynamicContent>(queryable, filterName, sitefinityCulture, provider);
      }
      filter = ContentHelper.AdaptMultilingualFilterExpressionRaw(filter, culture);
      List<DynamicContent> list = DataProviderBase.SetExpressions<DynamicContent>(queryable, filter, sortExpression, new int?(skip), new int?(take), ref totalCount).ToList<DynamicContent>();
      this.PopulateItems(list, dataItemType, manager);
      if (!SystemManager.HttpContextItems.Contains((object) "sfContentFilters"))
        SystemManager.HttpContextItems.Add((object) "sfContentFilters", (object) new string[1]
        {
          "DraftLinksParser"
        });
      ServiceUtility.DisableCache();
      return new CollectionContext<DynamicContent>((IEnumerable<DynamicContent>) list)
      {
        TotalCount = totalCount.Value
      };
    }

    private CollectionContext<DynamicContent> GetLiveDataItemsInternal(
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
      bool published = false)
    {
      Type dataItemType = TypeResolutionService.ResolveType(itemType);
      if (string.IsNullOrEmpty(provider))
      {
        ISite currentSite = SystemManager.CurrentContext.CurrentSite;
        provider = DynamicModuleManager.GetDefaultProviderName(ModuleBuilderManager.GetModuleNameFromType(itemType, currentSite.Id.ToString()));
      }
      DynamicModuleManager manager = DynamicModuleManager.GetManager(provider);
      int? totalCount = new int?(0);
      CultureInfo culture;
      CommonMethods.MatchCultureFilter(ref filter, out culture);
      IQueryable<DynamicContent> queryable = LifecycleFilters.OnlyLiveItems<DynamicContent>(this.FilterItemsByDynamicProperty(manager, dataItemType, ref filter));
      if (published)
      {
        culture = culture.GetCurrent();
        if (SystemManager.CurrentContext.AppSettings.Multilingual)
          queryable = queryable.Where<DynamicContent>((Expression<Func<DynamicContent, bool>>) (i => i.PublishedTranslations.Contains(culture.Name) || !i.PublishedTranslations.Any<string>() && i.Visible && culture == SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage));
        else
          queryable = queryable.Where<DynamicContent>((Expression<Func<DynamicContent, bool>>) (i => i.Visible));
      }
      List<DynamicContent> list1;
      if (Config.Get<SystemConfig>().GeoLocationSettings.EnableGeoLocationSearch && radius > 0.0)
      {
        DistanceSorting distanceSorting = DistanceSorting.Asc;
        if (sortExpression == "Distance DESC" || sortExpression == "Distance ASC")
        {
          distanceSorting = sortExpression == "Distance DESC" ? DistanceSorting.Desc : DistanceSorting.Asc;
          sortExpression = (string) null;
        }
        IEnumerable<IGeoLocation> geoLocationsList;
        List<DynamicContent> list2 = DataProviderBase.SetExpressions<DynamicContent>(((IGeoLocationManager) manager).FilterByGeoLocation<DynamicContent>(queryable, latitude, longitude, radius, out geoLocationsList, new ItemFilter()
        {
          ProviderName = provider,
          ContentType = itemType,
          CustomKey = geoLocationProperty
        }), filter, sortExpression, new int?(skip), new int?(take), ref totalCount).ToList<DynamicContent>();
        list1 = ((IGeoLocationManager) manager).SortByDistance<DynamicContent>((IEnumerable<DynamicContent>) list2, geoLocationsList, latitude, longitude, distanceSorting).ToList<DynamicContent>();
      }
      else
        list1 = DataProviderBase.SetExpressions<DynamicContent>(queryable, filter, sortExpression, new int?(skip), new int?(take), ref totalCount).ToList<DynamicContent>();
      this.PopulateItems(list1, dataItemType, manager);
      if (!SystemManager.HttpContextItems.Contains((object) "sfContentFilters"))
        SystemManager.HttpContextItems.Add((object) "sfContentFilters", (object) new string[1]
        {
          "LinksParser"
        });
      ServiceUtility.DisableCache();
      return new CollectionContext<DynamicContent>((IEnumerable<DynamicContent>) list1)
      {
        TotalCount = totalCount.Value
      };
    }

    private CollectionContext<DynamicContent> GetChildDataItemsInternal(
      string parentId,
      string itemType,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      if (!parentId.IsGuid())
        throw new ArgumentException("parentId must be a valid GUID.");
      DynamicModuleManager manager = DynamicModuleManager.GetManager(provider);
      Type dataItemType = TypeResolutionService.ResolveType(itemType);
      int? totalCount = new int?(0);
      Guid parentGuid = new Guid(parentId);
      CultureInfo culture;
      CommonMethods.MatchCultureFilter(ref filter, out culture);
      IQueryable<DynamicContent> source1 = this.FilterItemsByDynamicProperty(manager, dataItemType, ref filter);
      IQueryable<DynamicContent> source2;
      if (!(parentGuid == Guid.Empty))
        source2 = source1.Where<DynamicContent>((Expression<Func<DynamicContent, bool>>) (i => i.SystemParentId == parentGuid));
      else
        source2 = source1.Where<DynamicContent>((Expression<Func<DynamicContent, bool>>) (i => (Guid?) i.SystemParentId == new Guid?()));
      IQueryable<DynamicContent> queryable = LifecycleFilters.OnlyPublishedMasterItems<DynamicContent>(source2);
      bool supportsLifecycle = dataItemType.BaseType.IsILifecycle();
      string filterName = string.Empty;
      if (this.TryGetNamedFilter(supportsLifecycle, ref filter, out filterName))
      {
        CultureInfo sitefinityCulture = culture.GetSitefinityCulture();
        queryable = NamedFiltersHandler.ApplyFilterToDynamicContent<DynamicContent>(queryable, filterName, sitefinityCulture, provider);
      }
      filter = ContentHelper.AdaptMultilingualFilterExpressionRaw(filter, culture);
      List<DynamicContent> list = DataProviderBase.SetExpressions<DynamicContent>(queryable, filter, sortExpression, new int?(skip), new int?(take), ref totalCount).ToList<DynamicContent>();
      this.PopulateItems(list, dataItemType, manager);
      if (!SystemManager.HttpContextItems.Contains((object) "sfContentFilters"))
        SystemManager.HttpContextItems.Add((object) "sfContentFilters", (object) new string[1]
        {
          "DraftLinksParser"
        });
      ServiceUtility.DisableCache();
      return new CollectionContext<DynamicContent>((IEnumerable<DynamicContent>) list)
      {
        TotalCount = totalCount.Value
      };
    }

    private void PopulateItems(
      DynamicContent item,
      Type dataItemType,
      DynamicModuleManager manager)
    {
      this.PopulateItems(new List<DynamicContent>() { item }, dataItemType, manager);
    }

    private void PopulateItems(
      List<DynamicContent> items,
      Type dataItemType,
      DynamicModuleManager manager)
    {
      if (items.Count == 0)
        return;
      Guid[] itemIds = this.GetItemIDs((IList<DynamicContent>) items);
      List<DynamicContent> tempAndLiveRecords = this.GetTempAndLiveRecords(dataItemType, itemIds, manager);
      DynamicModuleType dynamicModuleType = this.GetDynamicModuleType(dataItemType);
      this.PopulateAdditionalInfo((IEnumerable<DynamicContent>) items, dynamicModuleType, manager, (IEnumerable<DynamicContent>) tempAndLiveRecords, this.GetItemsWithChildren(dataItemType, manager, itemIds));
    }

    private Guid[] GetItemIDs(IList<DynamicContent> items)
    {
      Guid[] itemIds = new Guid[items.Count];
      for (int index = 0; index < items.Count; ++index)
        itemIds[index] = items[index].Id;
      return itemIds;
    }

    private IList<Guid> GetItemsWithChildren(
      Type dataItemType,
      DynamicModuleManager manager,
      Guid[] itemIds)
    {
      IEnumerable<Type> childTypes = ModuleBuilderManager.GetManager().GetChildTypes(dataItemType);
      List<Guid> itemsWithChildren = new List<Guid>();
      if (childTypes != null)
      {
        foreach (Type itemType in childTypes)
          itemsWithChildren = manager.GetDataItems(itemType).Where<DynamicContent>((Expression<Func<DynamicContent, bool>>) (d => itemIds.Contains<Guid>(d.SystemParentId))).GroupBy<DynamicContent, Guid>((Expression<Func<DynamicContent, Guid>>) (d => d.SystemParentId)).Select<IGrouping<Guid, DynamicContent>, Guid>((Expression<Func<IGrouping<Guid, DynamicContent>, Guid>>) (item => item.Key)).ToList<Guid>();
      }
      return (IList<Guid>) itemsWithChildren;
    }

    private List<DynamicContent> GetTempAndLiveRecords(
      Type dataItemType,
      Guid[] ids,
      DynamicModuleManager manager)
    {
      IQueryable<DynamicContent> source1 = manager.GetDataItems(dataItemType).Where<DynamicContent>((Expression<Func<DynamicContent, bool>>) (d => ids.Contains<Guid>(d.OriginalContentId) || ids.Contains<Guid>(d.Id)));
      IQueryable<DynamicContent> source2;
      if (SystemManager.CurrentContext.AppSettings.Multilingual)
      {
        CultureInfo currentUiCulture = SystemManager.CurrentContext.Culture;
        source2 = source1.Where<DynamicContent>((Expression<Func<DynamicContent, bool>>) (d => (int) d.Status == 2 || ((int) d.Status == 1 || (int) d.Status == 8) && d.LanguageData.Any<LanguageData>((Func<LanguageData, bool>) (l => l.Language == currentUiCulture.Name))));
      }
      else
        source2 = source1.Where<DynamicContent>((Expression<Func<DynamicContent, bool>>) (d => (int) d.Status == 2 || (int) d.Status == 1));
      return source2.ToList<DynamicContent>();
    }

    private void PopulateAdditionalInfo(
      IEnumerable<DynamicContent> items,
      DynamicModuleType moduleType,
      DynamicModuleManager manager,
      IEnumerable<DynamicContent> draftAndLiveItems,
      IList<Guid> itemsWithChildren)
    {
      foreach (DynamicContent dynamicContent in items)
      {
        this.PopulateAdditionalInfo(dynamicContent, moduleType, manager, draftAndLiveItems, itemsWithChildren);
        dynamicContent.IsViewable = (dynamicContent.IsGranted("General", "View") ? 1 : 0) != 0;
        dynamicContent.IsEditable = (dynamicContent.IsGranted("General", "Modify") ? 1 : 0) != 0;
        dynamicContent.IsDeletable = (dynamicContent.IsGranted("General", "Delete") ? 1 : 0) != 0;
        dynamicContent.IsUnlockable = (dynamicContent.IsGranted("General", "Unlock") ? 1 : 0) != 0;
      }
    }

    private void PopulateAdditionalInfo(
      DynamicContent item,
      DynamicModuleType moduleType,
      DynamicModuleManager manager,
      IEnumerable<DynamicContent> tempAndLiveItems,
      IList<Guid> itemsWithChildren)
    {
      item.ProviderName = item.GetProviderName();
      if (item.Status != ContentLifecycleStatus.Live)
      {
        DynamicContent dynamicContent = item.Status == ContentLifecycleStatus.Master ? item : manager.GetDataItem(item.GetType(), item.OriginalContentId);
        item.Lifecycle.LastModified = new DateTime?(dynamicContent.LastModified);
        item.Lifecycle.LastModifiedBy = UserProfilesHelper.GetUserDisplayName(dynamicContent.LastModifiedBy);
        item.Lifecycle.PublicationDate = new DateTime?(dynamicContent.PublicationDate);
        if (tempAndLiveItems == null)
        {
          item.PopulateLifecycleInformation(manager.Provider.Name);
        }
        else
        {
          DynamicContent live1 = tempAndLiveItems.FirstOrDefault<DynamicContent>((Func<DynamicContent, bool>) (live => live.OriginalContentId == item.Id && live.Status == ContentLifecycleStatus.Live));
          if (live1 != null && live1.IsPublishedInCulture())
            item.HasLiveVersion = true;
          DynamicContent temp1 = tempAndLiveItems.FirstOrDefault<DynamicContent>((Func<DynamicContent, bool>) (temp =>
          {
            if ((item.Status == ContentLifecycleStatus.Master ? (temp.OriginalContentId == item.Id ? 1 : 0) : (temp.Id == item.Id ? 1 : 0)) == 0)
              return false;
            return temp.Status == ContentLifecycleStatus.Temp || temp.Status == ContentLifecycleStatus.PartialTemp;
          }));
          LifecycleItemExtensions.SetStatus((ILifecycleDataItem) live1, (ILifecycleDataItem) item, (ILifecycleDataItem) temp1);
        }
        moduleType.Owner = item.Owner;
        if (itemsWithChildren != null && itemsWithChildren.Count > 0)
          item.SystemHasChildItems = itemsWithChildren.Contains(item.Id);
        if (item.Status == ContentLifecycleStatus.Temp || item.Status == ContentLifecycleStatus.PartialTemp)
          item.MasterItemAvailableCultures = manager.GetDataItem(item.GetType(), item.OriginalContentId).AvailableCultures;
      }
      item.Author = item.GetUserDisplayName();
    }

    private DynamicItemContext SaveDataItemInternal(
      DynamicItemContext dataItem,
      string itemType,
      string provider,
      string workflowOperation,
      string parentId = null,
      string parentType = null,
      string newParentId = null)
    {
      DynamicModuleManager manager1 = DynamicModuleManager.GetManager(provider);
      Guid guid = Guid.Empty;
      ContentLifecycleStatus status = dataItem.Item.Status;
      bool flag = false;
      if (manager1.Provider != null)
      {
        flag = manager1.Provider.SuppressViewFieldsPermissionsCheck;
        manager1.Provider.SuppressViewFieldsPermissionsCheck = true;
      }
      try
      {
        Type itemType1 = TypeResolutionService.ResolveType(itemType);
        guid = status == ContentLifecycleStatus.Master ? dataItem.Item.Id : dataItem.Item.OriginalContentId;
        ILifecycleDataItem lifecycleDataItem1 = (ILifecycleDataItem) dataItem.Item;
        if (!parentId.IsNullOrEmpty())
        {
          Guid parentId1 = new Guid(parentId);
          dataItem.Item.SetParent(parentId1, parentType);
        }
        switch (status)
        {
          case ContentLifecycleStatus.Master:
            lifecycleDataItem1 = manager1.Lifecycle.CheckOut((ILifecycleDataItem) dataItem.Item);
            break;
          case ContentLifecycleStatus.Live:
            ILifecycleDataItem lifecycleDataItem2 = manager1.Lifecycle.Edit((ILifecycleDataItem) dataItem.Item);
            lifecycleDataItem1 = manager1.Lifecycle.CheckOut(lifecycleDataItem2);
            manager1.RefreshItem(dataItem.Item);
            break;
        }
        this.CheckIfUrlIsValid(manager1, dataItem.Item, itemType1, guid);
        if (!newParentId.IsNullOrEmpty())
        {
          Guid parentId2 = new Guid(newParentId);
          ((DynamicContent) lifecycleDataItem1).SetParent(parentId2, parentType);
        }
        ContentLinksManager manager2 = ContentLinksManager.GetManager();
        manager1.UpdateContentLinks(dataItem.Item, manager2);
        RelatedDataHelper.SaveRelatedDataChanges((IManager) manager1, (IDataItem) lifecycleDataItem1, dataItem.ChangedRelatedData);
        dataItem.ChangedRelatedData = (ContentLinkChange[]) null;
        Dictionary<string, string> contextBag = new Dictionary<string, string>();
        CommonMethods.FillContextBagFromCurrentRequest((IDictionary<string, string>) contextBag);
        if (workflowOperation == "Schedule")
          CommonMethods.TryUpdateItemBeforeWorkflowScheduleOperation((IDataItem) lifecycleDataItem1, (IDictionary<string, string>) contextBag);
        manager1.SaveChanges();
        if (workflowOperation == "SaveTemp")
        {
          manager1.Dispose();
          dataItem.Item = manager1.GetDataItem(dataItem.Item.GetType(), dataItem.Item.Id);
          ServiceUtility.DisableCache();
          return dataItem;
        }
        contextBag.Add("ContentType", dataItem.Item.GetType().FullName);
        if (string.IsNullOrEmpty(workflowOperation))
          workflowOperation = "SaveDraft";
        WorkflowManager.MessageWorkflow(guid, itemType1, provider, workflowOperation, false, contextBag);
      }
      finally
      {
        if (manager1.Provider != null)
          manager1.Provider.SuppressViewFieldsPermissionsCheck = flag;
      }
      manager1.Dispose();
      string str;
      if (status == ContentLifecycleStatus.Live)
      {
        dataItem.Item = manager1.GetDataItem(dataItem.Item.GetType(), dataItem.Item.Id);
        str = "LinksParser";
      }
      else
      {
        dataItem.Item = manager1.GetDataItem(dataItem.Item.GetType(), guid);
        ILifecycleDataItem temp = manager1.Lifecycle.GetTemp((ILifecycleDataItem) dataItem.Item);
        if (temp != null && temp.Owner == SecurityManager.CurrentUserId)
          dataItem.Item = temp as DynamicContent;
        str = "DraftLinksParser";
      }
      ILifecycleDataItem live = manager1.Lifecycle.GetLive((ILifecycleDataItem) dataItem.Item);
      if (live != null && live.IsPublishedInCulture())
        dataItem.HasLiveVersion = true;
      if (!SystemManager.HttpContextItems.Contains((object) "sfContentFilters"))
        SystemManager.HttpContextItems.Add((object) "sfContentFilters", (object) new string[1]
        {
          str
        });
      ServiceUtility.DisableCache();
      return dataItem;
    }

    private bool DeleteDataItemInternal(
      string dataItemId,
      string provider,
      string itemType,
      string deletedLanguage,
      bool checkRelatingData,
      string version)
    {
      DynamicModuleManager manager = DynamicModuleManager.GetManager(provider);
      Type itemType1 = TypeResolutionService.ResolveType(itemType);
      DynamicContent dataItem = manager.GetDataItem(itemType1, new Guid(dataItemId));
      RelatedDataHelper.CheckRelatingData(checkRelatingData, new Guid(dataItemId), dataItem.GetType().FullName);
      if (!string.IsNullOrWhiteSpace(version))
      {
        this.DeleteVersion(version);
        return true;
      }
      CultureInfo language = (CultureInfo) null;
      if (!string.IsNullOrEmpty(deletedLanguage))
        language = CultureInfo.GetCultureInfo(deletedLanguage);
      manager.DeleteDataItem(dataItem, language);
      manager.SaveChanges();
      ServiceUtility.DisableCache();
      return true;
    }

    private bool DeleteTempItemInternal(
      string dataItemIdString,
      string provider,
      bool force,
      string itemTypeFullName)
    {
      DynamicModuleManager manager = DynamicModuleManager.GetManager(provider);
      Guid dataItemId = new Guid(dataItemIdString);
      Type itemType = TypeResolutionService.ResolveType(itemTypeFullName, false);
      if (itemType == (Type) null)
        itemType = typeof (DynamicContent);
      DynamicContent cnt = manager.GetDataItems(itemType).Where<DynamicContent>((Expression<Func<DynamicContent, bool>>) (t => t.Id == dataItemId)).FirstOrDefault<DynamicContent>();
      bool flag1 = true;
      Guid currentUserId = SecurityManager.CurrentUserId;
      int num;
      if (force)
        num = cnt.IsGranted("General", "Unlock") ? 1 : 0;
      else
        num = 0;
      bool flag2 = num != 0;
      if (cnt != null)
      {
        ILifecycleDataItem master = manager.Lifecycle.GetMaster((ILifecycleDataItem) cnt);
        ILifecycleDataItem temp = manager.Lifecycle.GetTemp(master);
        if (temp == null || temp.Owner == Guid.Empty)
          return true;
        bool flag3 = false;
        if (temp.Owner == currentUserId | flag2)
        {
          using (new ManagerSettingsRegion((IManager) manager).SuppressSecurityChecks())
            manager.DeleteDataItem(temp as DynamicContent);
          flag3 = true;
        }
        else
          flag1 = false;
        if (flag3)
          manager.SaveChanges();
      }
      ServiceUtility.DisableCache();
      return flag1;
    }

    private bool BatchDeleteItemsInternal(
      string[] ids,
      string providerName,
      string workflowOperation,
      string deletedLanguage,
      string itemType,
      bool checkRelatingData)
    {
      return this.CallWorkflowBatch(providerName, itemType, ids, "Delete", deletedLanguage, checkRelatingData);
    }

    private bool BatchPublishInternal(
      string[] ids,
      string providerName,
      string workflowOperation,
      string itemType)
    {
      return this.CallWorkflowBatch(providerName, itemType, ids, "Publish");
    }

    private bool BatchUnpublishInternal(
      string[] ids,
      string providerName,
      string workflowOperation,
      string itemType)
    {
      return this.CallWorkflowBatch(providerName, itemType, ids, "Unpublish");
    }

    private bool CallWorkflowBatch(
      string providerName,
      string itemType,
      string[] Ids,
      string workflowOperation,
      string deletedLanguage = null,
      bool checkRelatingData = false)
    {
      DynamicModuleManager manager = DynamicModuleManager.GetManager(providerName);
      Type itemType1 = TypeResolutionService.ResolveType(itemType);
      string currentItemTitle = string.Empty;
      CultureInfo language = (CultureInfo) null;
      if (!string.IsNullOrEmpty(deletedLanguage) && SystemManager.CurrentContext.AppSettings.Multilingual)
        language = new CultureInfo(deletedLanguage);
      WorkflowBatchExceptionHandler exceptionHandler = new WorkflowBatchExceptionHandler();
      foreach (string id in Ids)
      {
        try
        {
          DynamicContent dataItem = manager.GetDataItem(itemType1, new Guid(id));
          currentItemTitle = DynamicContentExtensions.GetTitle(dataItem);
          if (workflowOperation == "Delete")
          {
            if (dataItem != null)
            {
              Dictionary<string, string> dictionary = new Dictionary<string, string>();
              dictionary.Add("ContentType", itemType1.FullName);
              WorkflowManager.AddLanguageToWorkflowContext(dictionary, language);
              dictionary.Add("CheckRelatingData", checkRelatingData.ToString());
              WorkflowManager.MessageWorkflow(dataItem.Id, dataItem.GetType(), providerName, workflowOperation, true, dictionary);
            }
          }
          else if (dataItem.IsOperationSupported(workflowOperation))
            WorkflowManager.MessageWorkflow(dataItem.Id, dataItem.GetType(), providerName, workflowOperation, false, new Dictionary<string, string>()
            {
              {
                "ContentType",
                itemType1.FullName
              }
            });
        }
        catch (Exception ex)
        {
          exceptionHandler.RegisterException(ex, currentItemTitle);
        }
      }
      exceptionHandler.ThrowAccumulatedErrorForContent(((IEnumerable<string>) Ids).Count<string>(), workflowOperation);
      return true;
    }

    private DynamicModuleType GetDynamicModuleType(Type dataItemType) => ModuleBuilderManager.GetManager().GetDynamicModuleType(dataItemType);

    private CollectionContext<DynamicContent> GetPredecessorItemsInternal(
      string itemId,
      string itemType,
      string providerName,
      string filter)
    {
      DynamicModuleManager manager = DynamicModuleManager.GetManager(providerName);
      Type type = TypeResolutionService.ResolveType(itemType);
      List<IHierarchicalItem> source = this.ConstructItemSubTree(manager.GetDataItem(type, new Guid(itemId)).ConstructPath((HashSet<Guid>) null), manager, type, filter);
      this.GetDynamicModuleType(type);
      List<DynamicContent> list = source.Cast<DynamicContent>().ToList<DynamicContent>();
      this.PopulateItems(list, type, manager);
      ServiceUtility.DisableCache();
      return new CollectionContext<DynamicContent>((IEnumerable<DynamicContent>) list);
    }

    private List<IHierarchicalItem> ConstructItemSubTree(
      List<IHierarchicalItem> itemPath,
      DynamicModuleManager manager,
      Type itemType,
      string filter = null)
    {
      List<IHierarchicalItem> source = new List<IHierarchicalItem>(itemPath.Count);
      bool flag = false;
      int? totalCount = new int?();
      foreach (IHierarchicalItem hierarchicalItem in itemPath)
      {
        IHierarchicalItem taxon = hierarchicalItem;
        if (taxon.Parent != null)
        {
          IQueryable<DynamicContent> collection = DataProviderBase.SetExpressions<DynamicContent>(this.FilterItemsByDynamicProperty(manager, itemType, ref filter), filter, (string) null, new int?(0), new int?(0), ref totalCount).Where<DynamicContent>((Expression<Func<DynamicContent, bool>>) (i => i.SystemParentId == taxon.ParentId && (int) i.Status == 0));
          source.AddRange((IEnumerable<IHierarchicalItem>) collection);
        }
        else if (!flag)
        {
          IQueryable<DynamicContent> collection = DataProviderBase.SetExpressions<DynamicContent>(this.FilterItemsByDynamicProperty(manager, itemType, ref filter), filter, (string) null, new int?(0), new int?(0), ref totalCount).Where<DynamicContent>((Expression<Func<DynamicContent, bool>>) (i => i.SystemParentId == taxon.ParentId && (int) i.Status == 0));
          source.AddRange((IEnumerable<IHierarchicalItem>) collection);
          flag = true;
        }
      }
      return source.ToList<IHierarchicalItem>();
    }

    private CollectionContext<DynamicContent> GetItemsAsTreeInternal(
      string[] leafIds,
      string provider,
      int itemsLimit,
      int perSubtreeLimit,
      string itemType,
      string filter)
    {
      List<DynamicContent> items = new List<DynamicContent>();
      bool flag1 = !string.IsNullOrEmpty(filter);
      bool flag2 = false;
      bool flag3 = false;
      if (leafIds.Length != 0)
      {
        HashSet<Guid> previouslyVisitedItems = new HashSet<Guid>();
        DynamicModuleManager manager = DynamicModuleManager.GetManager(provider);
        foreach (string leafId in leafIds)
        {
          Guid id = new Guid(leafId);
          Type itemType1 = TypeResolutionService.ResolveType(itemType);
          DynamicContent dataItem = manager.GetDataItem(itemType1, id);
          List<IHierarchicalItem> itemPath = dataItem.ConstructPath(previouslyVisitedItems);
          flag2 = this.CheckLimit(this.GetNodesPerSubtreeLimit(perSubtreeLimit), itemPath.Count, previouslyVisitedItems.Count);
          if (!flag2)
          {
            List<IHierarchicalItem> hierarchicalItemList = this.ConstructItemSubTree(itemPath, manager, itemType1, filter);
            flag2 = ((((flag2 ? 1 : 0) & (this.CheckLimit(this.GetNodesPerSubtreeLimit(perSubtreeLimit), hierarchicalItemList.Count) ? 1 : 0)) != 0 ? 1 : 0) & (this.CheckLimit(this.GetTotalNoadstLimit(itemsLimit), hierarchicalItemList.Count, previouslyVisitedItems.Count) ? 1 : 0)) != 0;
            if (!flag2)
            {
              foreach (IHierarchicalItem hierarchicalItem in hierarchicalItemList)
              {
                if (!previouslyVisitedItems.Contains(hierarchicalItem.Id))
                {
                  previouslyVisitedItems.Add(hierarchicalItem.Id);
                  items.Add((DynamicContent) hierarchicalItem);
                }
              }
              IEnumerable<Type> childItemTypes = DynamicContentExtensions.GetChildItemTypes(dataItem.GetType());
              List<DynamicContent> dynamicContentList = new List<DynamicContent>();
              foreach (Type childType in childItemTypes)
              {
                IQueryable<DynamicContent> queryable = manager.GetChildItems(dataItem, childType);
                if (flag1)
                  queryable = queryable.Where<DynamicContent>(filter);
                dynamicContentList.AddRange((IEnumerable<DynamicContent>) queryable);
              }
              flag2 = ((flag2 ? 1 : 0) & (this.CheckLimit(itemsLimit, dynamicContentList.Count, previouslyVisitedItems.Count) ? 1 : 0)) != 0;
              if (!flag2)
              {
                foreach (DynamicContent dynamicContent in dynamicContentList)
                {
                  if (!previouslyVisitedItems.Contains(dynamicContent.Id))
                  {
                    previouslyVisitedItems.Add(dynamicContent.Id);
                    items.Add(dynamicContent);
                  }
                }
                flag3 = true;
              }
              else
                break;
            }
            else
              break;
          }
          else
            break;
        }
        if (!flag2 & flag3)
        {
          ServiceUtility.DisableCache();
          Type dataItemType = TypeResolutionService.ResolveType(itemType);
          this.GetDynamicModuleType(dataItemType);
          this.PopulateItems(items, dataItemType, manager);
          return new CollectionContext<DynamicContent>((IEnumerable<DynamicContent>) items);
        }
      }
      return this.GetDataItemsInternal(provider, (string) null, 0, 0, (string) null, itemType, true);
    }

    private int GetNodesPerSubtreeLimit(int perSubtreeLimit) => perSubtreeLimit == 0 ? 50 : perSubtreeLimit;

    private int GetTotalNoadstLimit(int nodesLimit) => nodesLimit == 0 ? 100 : nodesLimit;

    private bool CheckLimit(int nodesLimit, params int[] countsToSum)
    {
      int num1 = 0;
      foreach (int num2 in countsToSum)
        num1 += num2;
      return num1 >= nodesLimit;
    }

    internal void CheckIfUrlIsValid(
      DynamicModuleManager manager,
      DynamicContent dataItem,
      Type itemType,
      Guid itemMasterId)
    {
      string url = manager.Provider.CompileItemUrl<DynamicContent>(dataItem);
      Regex regex = new Regex(DefinitionsHelper.UrlRegularExpressionDotNetValidator);
      string input = !url.IsNullOrWhitespace() ? url : throw new WebProtocolException(HttpStatusCode.InternalServerError, Res.Get<ModuleBuilderResources>().NotValidUrlException, (Exception) null);
      if (!regex.IsMatch(input))
        this.ThrowDuplicateUrlException(Res.Get<ModuleBuilderResources>().NotValidUrlException, url);
      this.ValidateUrlExist(manager, dataItem, itemType, itemMasterId);
    }

    private IQueryable<DynamicContent> FilterItemsByDynamicProperty(
      DynamicModuleManager manager,
      Type dataItemType,
      ref string filter)
    {
      Guid taxonId = Guid.Empty;
      string propertyName = string.Empty;
      int? totalCount = new int?();
      IQueryable<DynamicContent> queryable = (IQueryable<DynamicContent>) null;
      if (!string.IsNullOrEmpty(filter) && CommonMethods.MatchTaxonFilter(ref filter, out taxonId, out propertyName))
      {
        if (manager.Provider is IOrganizableProvider provider)
        {
          if (!(OrganizerBase.GetProperty(dataItemType, propertyName) is TaxonomyPropertyDescriptor property))
          {
            string singular = PluralsResolver.Instance.ToSingular(propertyName);
            string name = singular.Substring(0, 1).ToUpper() + singular.Substring(1).ToLower();
            property = OrganizerBase.GetProperty(dataItemType, name) as TaxonomyPropertyDescriptor;
          }
          if (property != null && provider != null)
            queryable = (IQueryable<DynamicContent>) provider.GetItemsByTaxon(taxonId, property.MetaField.IsSingleTaxon, property.Name, dataItemType, filter, (string) null, 0, 0, ref totalCount);
        }
      }
      else
        queryable = manager.GetDataItems(dataItemType);
      return queryable;
    }

    private bool TryGetNamedFilter(
      bool supportsLifecycle,
      ref string filter,
      out string filterName)
    {
      filterName = (string) null;
      if (!NamedFiltersHandler.TryParseFilterName(filter, out filterName))
        return false;
      if (supportsLifecycle && filterName == "PublishedDrafts")
        filterName = "LifecyclePublishedDrafts";
      filter = (string) null;
      return true;
    }

    /// <summary>Deletes an item version from history.</summary>
    /// <param name="version">The version.</param>
    private void DeleteVersion(string version)
    {
      VersionManager manager = VersionManager.GetManager();
      manager.DeleteChange(new Guid(version));
      manager.SaveChanges();
    }

    private void ValidateUrlExist(
      DynamicModuleManager manager,
      DynamicContent dataItem,
      Type itemType,
      Guid itemMasterId)
    {
      string url = manager.Provider.CompileItemUrl<DynamicContent>(dataItem);
      IQueryable<DynamicContent> source = manager.GetDataItems(itemType).Where<DynamicContent>((Expression<Func<DynamicContent, bool>>) (i => i.Urls.Any<DynamicContentUrlData>((Func<DynamicContentUrlData, bool>) (u => u.Url == url))));
      if (dataItem.SystemParentItem != null)
        source = source.Where<DynamicContent>((Expression<Func<DynamicContent, bool>>) (i => i.SystemParentId == dataItem.SystemParentId));
      if (source == null)
        return;
      foreach (DynamicContent dynamicContent in (IEnumerable<DynamicContent>) source)
      {
        if (!dynamicContent.IsDeleted && (dynamicContent.Status == ContentLifecycleStatus.Master ? dynamicContent.Id : dynamicContent.OriginalContentId) != itemMasterId)
          this.ThrowDuplicateUrlException(Res.Get<ModuleBuilderResources>().DuplicateUrlException, dataItem.UrlName.ToString());
      }
    }

    private void ThrowDuplicateUrlException(string messageFormat, string url)
    {
      WebProtocolException protocolException = new WebProtocolException(HttpStatusCode.InternalServerError, string.Format(messageFormat, (object) url), (Exception) null);
      protocolException.Data.Add((object) "Url", (object) url);
      throw protocolException;
    }

    internal void ValidateDublicateUrl(
      DynamicModuleManager manager,
      DynamicContent dataItem,
      Type itemType,
      Guid itemMasterId)
    {
      this.ValidateUrlExist(manager, dataItem, itemType, itemMasterId);
    }
  }
}
