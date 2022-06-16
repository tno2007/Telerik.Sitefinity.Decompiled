// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Content.Web.Services.Obsolete.ContentService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.GenericContent;
using Telerik.Sitefinity.Data.WcfHelpers;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.Libraries.Documents;
using Telerik.Sitefinity.Modules.Libraries.Images;
using Telerik.Sitefinity.Modules.Libraries.Videos;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services.Content.Data;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Versioning;
using Telerik.Sitefinity.Versioning.Model;
using Telerik.Sitefinity.Versioning.Serialization.Interfaces;
using Telerik.Sitefinity.Versioning.Web.Services;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Services.Content.Web.Services.Obsolete
{
  /// <summary>
  /// The WCF web service that is used to work with all types that inherit from base <see cref="N:Telerik.Sitefinity.Services.Content" />
  /// class.
  /// </summary>
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  public class ContentService : IContentService
  {
    private Regex taxonFilterRegex;

    /// <summary>
    /// Gets the single content item and returs it in JSON format.
    /// </summary>
    /// <param name="contentId">Id of the content item that ought to be retrieved.</param>
    /// <param name="itemType">The actual type of the content that ought to be retrieved.</param>
    /// <param name="providerName">Name of the content provider that is to be used to retrieve the content item.</param>
    /// <param name="managerType">The manager type</param>
    /// <param name="version">The version id. If not specified returns the current draft version, otherwise the specified version from the history</param>
    /// <param name="published"></param>
    /// <returns>
    /// An instance of ContentItemContext that contains the content item to be retrieved.
    /// </returns>
    /// 
    ///             ///
    public ContentItemContext<Telerik.Sitefinity.GenericContent.Model.Content> GetContent(
      string contentId,
      string itemType,
      string providerName,
      string managerType,
      string version,
      bool published)
    {
      return this.GetContentInternal(contentId, itemType, providerName, managerType, version);
    }

    /// <summary>
    /// Gets the single content item and returs it in XML format.
    /// </summary>
    /// <param name="contentId">Id of the content item that ought to be retrieved.</param>
    /// <param name="itemType">The actual type of the content that ought to be retrieved.</param>
    /// <param name="providerName">Name of the content provider that is to be used to retrieve the content item.</param>
    /// <param name="managerType">The manager type</param>
    /// <returns>
    /// An instance of ContentItemContext that contains the content item to be retrieved.
    /// </returns>
    public ContentItemContext<Telerik.Sitefinity.GenericContent.Model.Content> GetContentInXml(
      string contentId,
      string itemType,
      string providerName,
      string managerType)
    {
      return this.GetContentInternal(contentId, itemType, providerName, managerType, (string) null);
    }

    /// <summary>
    /// Gets a single child content item and returns it in JSON format.
    /// </summary>
    /// <param name="parentId">Id of the parent content item.</param>
    /// <param name="contentId">Id of the content item to be retrieved.</param>
    /// <param name="itemType">Actual type of the content item to be retrieved.</param>
    /// <param name="parentItemType">Actual type of the parent content item.</param>
    /// <param name="providerName">Name of the content provider that is to be used to retrieved the child content item.</param>
    /// <param name="newParentId">The new parent id.</param>
    /// <param name="version"></param>
    /// <returns>
    /// An instance of ContentItemContext that contains the content item to be retrieved.
    /// </returns>
    public ContentItemContext<Telerik.Sitefinity.GenericContent.Model.Content> GetChildContent(
      string parentId,
      string contentId,
      string itemType,
      string parentItemType,
      string providerName,
      string newParentId,
      string version)
    {
      return this.GetChildContentInternal(parentId, contentId, itemType, parentItemType, providerName, version);
    }

    /// <summary>
    /// Gets a single child content item and returns it in XML format.
    /// </summary>
    /// <param name="parentId">Id of the parent content item.</param>
    /// <param name="contentId">Id of the content item to be retrieved.</param>
    /// <param name="itemType">Actual type of the content item to be retrieved.</param>
    /// <param name="parentItemType">Actual type of the parent content item.</param>
    /// <param name="providerName">Name of the content provider that is to be used to retrieve the child content item.</param>
    /// <param name="newParentId">The new parent id.</param>
    /// <returns>
    /// An instance of ContentItemContext that contains the content item to be retrieved.
    /// </returns>
    public ContentItemContext<Telerik.Sitefinity.GenericContent.Model.Content> GetChildContentInXml(
      string parentId,
      string contentId,
      string itemType,
      string parentItemType,
      string providerName,
      string newParentId)
    {
      return this.GetChildContentInternal(parentId, contentId, itemType, parentItemType, providerName, (string) null);
    }

    /// <summary>
    /// Saves the content item and returns the saved content item in JSON format. If the content item
    /// with the specified pageId exists the content item will be updates; otherwise new content item will
    /// be created.
    /// </summary>
    /// <param name="content">The content object to be saved.</param>
    /// <param name="contentId">The pageId of the content item to be saved.</param>
    /// <param name="itemType">The actual type of the content to be saved.</param>
    /// <param name="providerName">Name of the content provider that is to be used to save the content item.</param>
    /// <param name="managerType">The manager type</param>
    /// <param name="version">The version id. If not specified returns the current draft version, otherwise the specified version from the history</param>
    /// <param name="published"></param>
    /// <returns>
    /// An instance of ContentItemContext that contains the content item that was saved.
    /// </returns>
    public ContentItemContext<Telerik.Sitefinity.GenericContent.Model.Content> SaveContent(
      ContentItemContext<Telerik.Sitefinity.GenericContent.Model.Content> content,
      string contentId,
      string itemType,
      string providerName,
      string managerType,
      string version,
      bool published)
    {
      return this.SaveContentInternal(content, contentId, itemType, providerName, managerType);
    }

    /// <summary>
    /// Saves the content item and returns the saved content item in XML format. If the content item
    /// with the specified pageId exists the content item will be updated; otherwise a new content item will
    /// be created.
    /// </summary>
    /// <param name="content">The content object to be saved.</param>
    /// <param name="contentId">The pageId of the content item to be saved.</param>
    /// <param name="itemType">The actual type of the content to be saved.</param>
    /// <param name="providerName">Name of the content provider that is to be used to save the content item.</param>
    /// <param name="managerType">The manager type</param>
    /// <returns>
    /// An instance of ContentItemContext that contains the content item that was saved.
    /// </returns>
    public ContentItemContext<Telerik.Sitefinity.GenericContent.Model.Content> SaveContentInXml(
      ContentItemContext<Telerik.Sitefinity.GenericContent.Model.Content> content,
      string contentId,
      string itemType,
      string providerName,
      string managerType)
    {
      return this.SaveContentInternal(content, contentId, itemType, providerName, managerType);
    }

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
    /// <param name="newParentId"></param>
    /// <param name="version"></param>
    /// <returns>
    /// An instance of ContentItemContext that contains the content item that was saved.
    /// </returns>
    public ContentItemContext<Telerik.Sitefinity.GenericContent.Model.Content> SaveChildContent(
      ContentItemContext<Telerik.Sitefinity.GenericContent.Model.Content> content,
      string parentId,
      string contentId,
      string itemType,
      string parentItemType,
      string providerName,
      string newParentId,
      string version)
    {
      return this.SaveChildContentInternal(content, parentId, contentId, itemType, parentItemType, providerName, newParentId);
    }

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
    /// <returns>
    /// An instance of ContentItemContext that contains the content item that was saved.
    /// </returns>
    public ContentItemContext<Telerik.Sitefinity.GenericContent.Model.Content> SaveChildContentInXml(
      ContentItemContext<Telerik.Sitefinity.GenericContent.Model.Content> content,
      string parentId,
      string contentId,
      string itemType,
      string parentItemType,
      string providerName,
      string newParentId)
    {
      return this.SaveChildContentInternal(content, parentId, contentId, itemType, parentItemType, providerName, newParentId);
    }

    /// <summary>
    /// Gets the collection of content item summary objects and returns them in JSON format.
    /// </summary>
    /// <param name="itemType">The actual type of the content to be retrieved.</param>
    /// <param name="sortExpression">Sort expression used to order the content items in the result set.</param>
    /// <param name="skip">The number of items to skip before starting to take the items in the result set.</param>
    /// <param name="take">The number of items to take into the result set.</param>
    /// <param name="filter">Filter expression to be used to filter the items that will be taken into the result set.</param>
    /// <param name="providerName">Name of the content provider to be used to get the content items.</param>
    /// <param name="managerType">The manager type</param>
    /// <returns>
    /// An instance of CollectionContext object that contains the collection of ContentSummary items.
    /// </returns>
    public CollectionContext<ContentViewModelBase> GetContentItems(
      string itemType,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string providerName,
      string managerType)
    {
      return this.GetContentItemsInternal(itemType, sortExpression, skip, take, filter, providerName, managerType);
    }

    /// <summary>
    /// Gets the collection of content item summary objects and returns them in XML format.
    /// </summary>
    /// <param name="itemType">The actual type of the content to be retrieved.</param>
    /// <param name="sortExpression">Sort expression used to order the content items in the result set.</param>
    /// <param name="skip">The number of items to skip before starting to take the items in the result set.</param>
    /// <param name="take">The number of items to take into the result set.</param>
    /// <param name="filter">Filter expression to be used to filter the items that will be taken into the result set.</param>
    /// <param name="providerName">Name of the content provider to be used to get the content items.</param>
    /// <returns>
    /// An instance of CollectionContext object that contains the collection of ContentSummary items.
    /// </returns>
    public CollectionContext<ContentViewModelBase> GetContentItemsInXml(
      string itemType,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string providerName,
      string managerType)
    {
      return this.GetContentItemsInternal(itemType, sortExpression, skip, take, filter, providerName, managerType);
    }

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
    /// <returns>
    /// An instance of CollectionContext object that contains the collection of ContentSummary items.
    /// </returns>
    public CollectionContext<ContentViewModelBase> GetChildrenContentItems(
      string parentId,
      string providerName,
      string itemType,
      string parentItemType,
      string sortExpression,
      string filter,
      int skip,
      int take)
    {
      return this.GetChildrenContentItemsInternal(parentId, providerName, itemType, parentItemType, sortExpression, filter, skip, take);
    }

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
    /// <returns>
    /// An instance of CollectionContext object that contains the collection of ContentSummary items.
    /// </returns>
    public CollectionContext<ContentViewModelBase> GetChildrenContentItemsInXml(
      string parentId,
      string providerName,
      string itemType,
      string parentItemType,
      string sortExpression,
      string filter,
      int skip,
      int take)
    {
      return this.GetChildrenContentItemsInternal(parentId, providerName, itemType, parentItemType, sortExpression, filter, skip, take);
    }

    /// <summary>
    /// Deletes the content item and returns true if the content item has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="Id">Id of the content item to be deleted.</param>
    /// <param name="itemType">The actual type of the content item to be deleted.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the item.</param>
    /// <param name="managerType">The manager type</param>
    /// <returns></returns>
    public bool DeleteContent(
      string Id,
      string itemType,
      string providerName,
      string managerType,
      string version,
      bool published)
    {
      return this.DeleteContentInternal(Id, itemType, providerName, managerType, version);
    }

    /// <summary>
    /// Deletes the content item and returns true if the content item has been deleted; otherwise false.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="Id">Id of the content item to be deleted.</param>
    /// <param name="itemType">The actual type of the content item to be deleted.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the item.</param>
    /// <param name="managerType">The manager type</param>
    /// <returns></returns>
    public bool DeleteContentInXml(
      string Id,
      string itemType,
      string providerName,
      string managerType)
    {
      return this.DeleteContentInternal(Id, itemType, providerName, managerType, (string) null);
    }

    /// <summary>
    /// Deletes an array of content items.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="Ids">An array containing the Ids of the content items to be deleted.</param>
    /// <param name="itemsType">The actual type of the content items to be deleted.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the item.</param>
    /// <param name="managerType"></param>
    /// <returns></returns>
    public bool BatchDeleteContent(
      string[] Ids,
      string itemType,
      string providerName,
      string managerType)
    {
      return this.BatchDeleteContentInternal(Ids, itemType, providerName, managerType);
    }

    /// <summary>
    /// Deletes an array of content items.
    /// Result is returned in Xml format.
    /// </summary>
    /// <param name="Ids">An array containing the Ids of the content items to be deleted.</param>
    /// <param name="itemsType">The actual type of the content items to be deleted.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the item.</param>
    /// <param name="managerType"></param>
    /// <returns></returns>
    public bool BatchDeleteContentInXml(
      string[] Ids,
      string itemType,
      string providerName,
      string managerType)
    {
      return this.BatchDeleteContentInternal(Ids, itemType, providerName, managerType);
    }

    /// <summary>
    /// Deletes the child content item and returns true if the child content item has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="pageId">Id of the child content item to be deleted.</param>
    /// <param name="parentId">Id of the parent content item who's child ought to be deleted.</param>
    /// <param name="itemType">The actual type of the content item to be deleted.</param>
    /// <param name="parentItemType">The actula type of the parent content item.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the item.</param>
    /// <returns></returns>
    public bool DeleteChildContent(
      string id,
      string parentId,
      string itemType,
      string parentItemType,
      string providerName,
      string newParentId,
      string version)
    {
      return this.DeleteChildContentInternal(id, parentId, itemType, parentItemType, providerName, version);
    }

    /// <summary>
    /// Deletes the child content item and returns true if the child content item has been deleted; otherwise false.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="pageId">Id of the child content item to be deleted.</param>
    /// <param name="parentId">Id of the parent content item who's child ought to be deleted.</param>
    /// <param name="itemType">The actual type of the content item to be deleted.</param>
    /// <param name="parentItemType">The actula type of the parent content item.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the item.</param>
    /// <returns></returns>
    public bool DeleteChildContentInXml(
      string id,
      string parentId,
      string itemType,
      string parentItemType,
      string providerName,
      string newParentId)
    {
      return this.DeleteChildContentInternal(id, parentId, itemType, parentItemType, providerName, (string) null);
    }

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
    public bool BatchDeleteChildContent(
      string[] Ids,
      string parentId,
      string itemType,
      string providerName,
      string managerType)
    {
      return this.BatchDeleteContentInternal(Ids, itemType, providerName, managerType);
    }

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
    public bool BatchDeleteChildContentInXml(
      string[] Ids,
      string parentId,
      string itemType,
      string providerName,
      string managerType)
    {
      return this.BatchDeleteContentInternal(Ids, itemType, providerName, managerType);
    }

    /// <summary>
    /// Reorders the content item. The item should implement IOrderableItem
    /// </summary>
    /// <param name="contentId">The content pageId.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="managerType">Type of the manager.</param>
    /// <param name="oldPosition">The old position.</param>
    /// <param name="newPosition">The new position.</param>
    /// <returns>True if reordering is successful</returns>
    public void ReorderContent(
      string contentId,
      string itemType,
      string providerName,
      string managerType,
      float oldPosition,
      float newPosition)
    {
      this.ReorderContentInternal(contentId, itemType, providerName, managerType, oldPosition, newPosition);
    }

    /// <summary>Reorders multpile content items</summary>
    /// <param name="contentIdnewOrdinal">Dictionary where the key is the content item and the value is the offset</param>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="managerType">Type of the manager.</param>
    public void BatchReorderContent(
      Dictionary<string, float> contentIdnewOrdinal,
      string itemType,
      string providerName,
      string managerType)
    {
      this.BatchReorderContentInternal(contentIdnewOrdinal, itemType, providerName, managerType);
    }

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
    public bool BatchChangeParent(
      string[] ids,
      string newParentId,
      string providerName,
      string itemType,
      string parentItemType)
    {
      return this.BatchChangeParentInternal(ids, newParentId, providerName, itemType, parentItemType);
    }

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
    public bool BatchChangeParentInXml(
      string[] ids,
      string newParentId,
      string providerName,
      string itemType,
      string parentItemType)
    {
      return this.BatchChangeParentInternal(ids, newParentId, providerName, itemType, parentItemType);
    }

    private ContentItemContext<Telerik.Sitefinity.GenericContent.Model.Content> GetContentInternal(
      string contentid,
      string itemType,
      string providerName,
      string managerType,
      string version)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      Type itemType1 = TypeResolutionService.ResolveType(itemType);
      Telerik.Sitefinity.GenericContent.Model.Content content = (!string.IsNullOrEmpty(managerType) ? ManagerBase.GetManager(managerType, providerName) : ManagerBase.GetMappedManager(itemType1, providerName)).GetItem(itemType1, new Guid(contentid)) as Telerik.Sitefinity.GenericContent.Model.Content;
      ContentItemContext<Telerik.Sitefinity.GenericContent.Model.Content> contentItemContext = new ContentItemContext<Telerik.Sitefinity.GenericContent.Model.Content>();
      contentItemContext.Item = content;
      ContentItemContext<Telerik.Sitefinity.GenericContent.Model.Content> result = contentItemContext;
      if (version != null)
        this.RestoreItemFromHistory(result, version);
      ServiceUtility.DisableCache();
      return result;
    }

    /// <summary>Restores the item from history.</summary>
    /// <param name="result">The result.</param>
    /// <param name="version">The version.</param>
    private void RestoreItemFromHistory(ContentItemContext<Telerik.Sitefinity.GenericContent.Model.Content> result, string version)
    {
      VersionManager manager = VersionManager.GetManager();
      Guid guid = new Guid(version);
      manager.GetSpecificVersionByChangeId((object) result.Item, guid);
      Change change = manager.GetItem(typeof (Change), guid) as Change;
      result.VersionInfo = new WcfChange(change);
      Change nextChange = manager.GetNextChange(change);
      if (nextChange != null)
        result.VersionInfo.NextId = nextChange.Id.ToString();
      Change previousChange = manager.GetPreviousChange(change);
      if (previousChange == null)
        return;
      result.VersionInfo.PreviousId = previousChange.Id.ToString();
    }

    private ContentItemContext<Telerik.Sitefinity.GenericContent.Model.Content> GetChildContentInternal(
      string parentId,
      string contentId,
      string itemType,
      string parentItemType,
      string providerName,
      string version)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      Type itemType1 = TypeResolutionService.ResolveType(itemType);
      Telerik.Sitefinity.GenericContent.Model.Content content = ManagerBase.GetMappedManager(itemType1, providerName).GetItem(itemType1, new Guid(contentId)) as Telerik.Sitefinity.GenericContent.Model.Content;
      ContentItemContext<Telerik.Sitefinity.GenericContent.Model.Content> contentItemContext = new ContentItemContext<Telerik.Sitefinity.GenericContent.Model.Content>();
      contentItemContext.Item = content;
      ContentItemContext<Telerik.Sitefinity.GenericContent.Model.Content> result = contentItemContext;
      if (version != null)
        this.RestoreItemFromHistory(result, version);
      ServiceUtility.DisableCache();
      return result;
    }

    private CollectionContext<ContentViewModelBase> GetContentItemsInternal(
      string itemType,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string providerName,
      string managerType)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      Type type = TypeResolutionService.ResolveType(itemType);
      IManager manager = !string.IsNullOrEmpty(managerType) ? ManagerBase.GetManager(managerType, providerName) : ManagerBase.GetMappedManager(type, providerName);
      int? totalCount = new int?(0);
      ContentDataProviderBase provider = manager.Provider as ContentDataProviderBase;
      IEnumerable items1 = (IEnumerable) null;
      Guid taxonId = Guid.Empty;
      string propertyName = string.Empty;
      if (!string.IsNullOrEmpty(filter) && this.MatchTaxonFilter(ref filter, out taxonId, out propertyName))
      {
        if (OrganizerBase.GetProperty(type, propertyName) is TaxonomyPropertyDescriptor property)
          items1 = provider.GetItemsByTaxon(taxonId, property.MetaField.IsSingleTaxon, property.Name, type, filter, sortExpression, skip, take, ref totalCount);
      }
      else
        items1 = manager.GetItems(type, filter, sortExpression, skip, take, ref totalCount);
      IList<ContentViewModelBase> items2 = this.DispatchContentSummary(items1, provider, type);
      ServiceUtility.DisableCache();
      return new CollectionContext<ContentViewModelBase>((IEnumerable<ContentViewModelBase>) items2)
      {
        TotalCount = totalCount.Value
      };
    }

    private CollectionContext<ContentViewModelBase> GetChildrenContentItemsInternal(
      string parentId,
      string providerName,
      string itemType,
      string parentItemType,
      string sortExpression,
      string filter,
      int skip,
      int take)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      Type type = TypeResolutionService.ResolveType(itemType);
      IManager mappedManager = ManagerBase.GetMappedManager(type, providerName);
      int? totalCount = new int?(0);
      ContentDataProviderBase provider = mappedManager.Provider as ContentDataProviderBase;
      Guid parentGuid = new Guid(parentId);
      IEnumerable items1 = (IEnumerable) null;
      Guid taxonId = Guid.Empty;
      string propertyName = string.Empty;
      if (!string.IsNullOrEmpty(filter) && this.MatchTaxonFilter(ref filter, out taxonId, out propertyName))
      {
        if (OrganizerBase.GetProperty(type, propertyName) is TaxonomyPropertyDescriptor property)
          items1 = provider.GetItemsByTaxon(taxonId, property.MetaField.IsSingleTaxon, property.Name, type, filter, sortExpression, skip, take, ref totalCount);
      }
      else
        items1 = (IEnumerable) mappedManager.GetItems(type, filter, sortExpression, skip, take, ref totalCount).Cast<IHasParent>().Where<IHasParent>((Func<IHasParent, bool>) (p => p.Parent.Id == parentGuid));
      IList<ContentViewModelBase> items2 = this.DispatchContentSummary(items1, provider, type);
      ServiceUtility.DisableCache();
      return new CollectionContext<ContentViewModelBase>((IEnumerable<ContentViewModelBase>) items2)
      {
        TotalCount = totalCount.Value
      };
    }

    /// <summary>Deletes an item version from history.</summary>
    /// <param name="version">The version.</param>
    private void DeleteVersion(string version)
    {
      VersionManager manager = VersionManager.GetManager();
      manager.DeleteChange(new Guid(version));
      manager.SaveChanges();
    }

    /// <summary>Deletes the content internal.</summary>
    /// <param name="contentId">The content id.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="managerType">Type of the manager.</param>
    /// <param name="version">The content version.</param>
    /// <returns></returns>
    private bool DeleteContentInternal(
      string contentId,
      string itemType,
      string providerName,
      string managerType,
      string version)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      Type itemType1 = TypeResolutionService.ResolveType(itemType);
      if (version != null)
      {
        this.DeleteVersion(version);
        return true;
      }
      IManager manager = !string.IsNullOrEmpty(managerType) ? ManagerBase.GetManager(managerType, providerName) : ManagerBase.GetMappedManager(itemType1, providerName);
      object obj = manager.GetItem(itemType1, new Guid(contentId));
      manager.DeleteItem(obj);
      manager.SaveChanges();
      return true;
    }

    private bool BatchDeleteContentInternal(
      string[] ids,
      string itemType,
      string providerName,
      string managerType)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      Type itemType1 = TypeResolutionService.ResolveType(itemType);
      IManager manager = !string.IsNullOrEmpty(managerType) ? ManagerBase.GetManager(managerType, providerName) : ManagerBase.GetMappedManager(itemType1, providerName);
      foreach (string id in ids)
      {
        object obj = manager.GetItem(itemType1, new Guid(id));
        manager.DeleteItem(obj);
      }
      manager.SaveChanges();
      return true;
    }

    private bool DeleteChildContentInternal(
      string contentId,
      string parentId,
      string itemType,
      string parentItemType,
      string providerName,
      string version)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      Type itemType1 = TypeResolutionService.ResolveType(itemType);
      IManager mappedManager = ManagerBase.GetMappedManager(itemType1, providerName);
      object obj = mappedManager.GetItem(itemType1, new Guid(contentId));
      if (version != null)
      {
        this.DeleteVersion(version);
        return true;
      }
      mappedManager.DeleteItem(obj);
      mappedManager.SaveChanges();
      return true;
    }

    private IManager GetManagerForSpecificTypeAndProvider(
      Type contType,
      string providerName,
      string managerType)
    {
      return string.IsNullOrEmpty(managerType) ? ManagerBase.GetMappedManager(contType, providerName) : ManagerBase.GetManager(managerType, providerName);
    }

    private ContentItemContext<Telerik.Sitefinity.GenericContent.Model.Content> SaveContentInternal(
      ContentItemContext<Telerik.Sitefinity.GenericContent.Model.Content> content,
      string contentId,
      string itemType,
      string providerName,
      string managerType)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      Type contType = TypeResolutionService.ResolveType(itemType);
      IManager specificTypeAndProvider = this.GetManagerForSpecificTypeAndProvider(contType, providerName, managerType);
      MethodInfo urlRecompilationMethod = (MethodInfo) null;
      if (content.Item is ILocatable)
      {
        ILocatable locatable = (ILocatable) content.Item;
        if (string.IsNullOrEmpty(locatable.UrlName.Value))
          locatable.UrlName = (Lstring) Regex.Replace((string) content.Item.Title, "[^\\d\\w]+", "_");
        urlRecompilationMethod = specificTypeAndProvider.Provider.GetType().GetMethod("RecompileItemUrls").MakeGenericMethod(content.Item.GetType());
        urlRecompilationMethod.Invoke((object) specificTypeAndProvider.Provider, new object[1]
        {
          (object) content.Item
        });
      }
      this.ValidateUrlConstraints(specificTypeAndProvider, content.Item, urlRecompilationMethod);
      specificTypeAndProvider.SaveChanges();
      if (contType.GetInterface(typeof (IVersionSerializable).FullName) != (Type) null)
      {
        VersionManager manager = VersionManager.GetManager();
        manager.CreateVersion((IDataItem) content.Item, false);
        manager.SaveChanges();
      }
      ContentItemContext<Telerik.Sitefinity.GenericContent.Model.Content> contentItemContext = new ContentItemContext<Telerik.Sitefinity.GenericContent.Model.Content>();
      contentItemContext.Item = content.Item;
      return contentItemContext;
    }

    private ContentItemContext<Telerik.Sitefinity.GenericContent.Model.Content> SaveChildContentInternal(
      ContentItemContext<Telerik.Sitefinity.GenericContent.Model.Content> content,
      string parentId,
      string contentId,
      string itemType,
      string parentItemType,
      string providerName,
      string newParentId)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      Type itemType1 = TypeResolutionService.ResolveType(parentItemType);
      IManager mappedManager = ManagerBase.GetMappedManager(itemType, providerName);
      if (!string.IsNullOrEmpty(newParentId))
        parentId = newParentId;
      Guid id = new Guid(parentId);
      Telerik.Sitefinity.GenericContent.Model.Content content1 = (Telerik.Sitefinity.GenericContent.Model.Content) mappedManager.GetItem(itemType1, id);
      ((IHasParent) content.Item).Parent = content1;
      this.RecompileMethodInfo(content.Item, mappedManager);
      mappedManager.SaveChanges();
      if (content.Item.GetType().GetInterface(typeof (IVersionSerializable).FullName) != (Type) null && !(content.Item.GetType() == typeof (Telerik.Sitefinity.Libraries.Model.Video)) && !(content.Item.GetType() == typeof (Telerik.Sitefinity.Libraries.Model.Document)))
      {
        VersionManager manager = VersionManager.GetManager();
        manager.CreateVersion((IDataItem) content.Item, false);
        manager.SaveChanges();
      }
      ContentItemContext<Telerik.Sitefinity.GenericContent.Model.Content> contentItemContext = new ContentItemContext<Telerik.Sitefinity.GenericContent.Model.Content>();
      contentItemContext.Item = content.Item;
      return contentItemContext;
    }

    private bool BatchChangeParentInternal(
      string[] ids,
      string newParentId,
      string providerName,
      string itemType,
      string parentItemType)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      Type itemType1 = TypeResolutionService.ResolveType(itemType);
      Type itemType2 = TypeResolutionService.ResolveType(parentItemType);
      IManager mappedManager = ManagerBase.GetMappedManager(itemType, providerName);
      Guid id1 = new Guid(newParentId);
      Telerik.Sitefinity.GenericContent.Model.Content content1 = (Telerik.Sitefinity.GenericContent.Model.Content) mappedManager.GetItem(itemType2, id1);
      foreach (string id2 in ids)
      {
        Telerik.Sitefinity.GenericContent.Model.Content content2 = (Telerik.Sitefinity.GenericContent.Model.Content) mappedManager.GetItem(itemType1, new Guid(id2));
        ((IHasParent) content2).Parent = content1;
        this.RecompileMethodInfo(content2, mappedManager);
      }
      mappedManager.SaveChanges();
      return true;
    }

    private void BatchReorderContentInternal(
      Dictionary<string, float> contentIdnewOrdinal,
      string itemType,
      string providerName,
      string managerType)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      Type itemType1 = TypeResolutionService.ResolveType(itemType);
      IManager manager = !string.IsNullOrEmpty(managerType) ? ManagerBase.GetManager(managerType, providerName) : ManagerBase.GetMappedManager(itemType1, providerName);
      foreach (KeyValuePair<string, float> keyValuePair in contentIdnewOrdinal)
        manager.Provider.ReorderItem(itemType1, new Guid(keyValuePair.Key), keyValuePair.Value);
      manager.SaveChanges();
    }

    private void ReorderContentInternal(
      string contentId,
      string itemType,
      string providerName,
      string managerType,
      float oldPosition,
      float newPosition)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      Type itemType1 = TypeResolutionService.ResolveType(itemType);
      IManager manager = !string.IsNullOrEmpty(managerType) ? ManagerBase.GetManager(managerType, providerName) : ManagerBase.GetMappedManager(itemType1, providerName);
      manager.Provider.ReorderItem(itemType1, new Guid(contentId), oldPosition, newPosition);
      manager.SaveChanges();
    }

    private void RecompileMethodInfo(Telerik.Sitefinity.GenericContent.Model.Content content, IManager manager)
    {
      MethodInfo urlRecompilationMethod = (MethodInfo) null;
      if (content is ILocatable)
      {
        ILocatable locatable = (ILocatable) content;
        if (string.IsNullOrEmpty(locatable.UrlName.Value))
          locatable.UrlName = (Lstring) Regex.Replace((string) content.Title, "[^\\d\\w]+", "_");
        urlRecompilationMethod = manager.Provider.GetType().GetMethod("RecompileItemUrls").MakeGenericMethod(content.GetType());
        urlRecompilationMethod.Invoke((object) manager.Provider, new object[1]
        {
          (object) content
        });
      }
      this.ValidateUrlConstraints(manager, content, urlRecompilationMethod);
    }

    private void ValidateUrlConstraints(
      IManager manager,
      Telerik.Sitefinity.GenericContent.Model.Content item,
      MethodInfo urlRecompilationMethod)
    {
      if (!(item is ILocatable) || !(manager.Provider is ContentDataProviderBase))
        return;
      ILocatable locatable = (ILocatable) item;
      ContentDataProviderBase provider = (ContentDataProviderBase) manager.Provider;
      Guid id = item.Id;
      List<UrlData> list = locatable.Urls.ToList<UrlData>();
      if (list.Count <= 0)
        return;
      Type type = item.GetType();
      Type urlTypeFor = provider.GetUrlTypeFor(type);
      foreach (UrlData urlData in list)
      {
        string url = urlData.Url;
        if (provider.GetUrls(urlTypeFor).Where<UrlData>((Expression<Func<UrlData, bool>>) (u => u.Url == url && u.Parent.Id != id)).FirstOrDefault<UrlData>() != null)
        {
          if (locatable.AutoGenerateUniqueUrl)
          {
            locatable.UrlName = (Lstring) ((string) locatable.UrlName + SecurityManager.GetRandomKey(6));
            urlRecompilationMethod.Invoke((object) manager.Provider, new object[1]
            {
              (object) locatable
            });
          }
          else
          {
            manager.CancelChanges();
            this.ThrowDuplicateUrlException(url);
          }
        }
      }
    }

    private void ThrowDuplicateUrlException(string url) => throw new WebProtocolException(HttpStatusCode.InternalServerError, string.Format(Res.Get<ContentResources>().DuplicateUrlException, (object) url), (Exception) null);

    private IList<ContentViewModelBase> DispatchContentSummary(
      IEnumerable items,
      ContentDataProviderBase contentProvider,
      Type contentType)
    {
      List<ContentViewModelBase> contentViewModelBaseList = new List<ContentViewModelBase>();
      foreach (object contentItem in items)
      {
        bool isEditable = true;
        if (contentItem.GetType().GetInterface(typeof (ISecuredObject).FullName) != (Type) null)
          isEditable = ((ISecuredObject) contentItem).IsEditable((DataProviderBase) contentProvider);
        switch (contentItem)
        {
          case Comment _:
            contentViewModelBaseList.Add((ContentViewModelBase) new CommentViewModel((Comment) contentItem, contentProvider, isEditable));
            continue;
          case Telerik.Sitefinity.Libraries.Model.Image _:
            contentViewModelBaseList.Add((ContentViewModelBase) new ImageViewModel((Telerik.Sitefinity.Libraries.Model.Image) contentItem, contentProvider, isEditable));
            continue;
          case Telerik.Sitefinity.Libraries.Model.Document _:
            contentViewModelBaseList.Add((ContentViewModelBase) new DocumentViewModel((Telerik.Sitefinity.Libraries.Model.Document) contentItem, contentProvider, isEditable));
            continue;
          case Telerik.Sitefinity.Libraries.Model.Video _:
            contentViewModelBaseList.Add((ContentViewModelBase) new VideoViewModel((Telerik.Sitefinity.Libraries.Model.Video) contentItem, contentProvider, isEditable));
            continue;
          default:
            if (typeof (Library).IsAssignableFrom(contentType))
            {
              contentViewModelBaseList.Add((ContentViewModelBase) new LibraryViewModel_old((Library) contentItem, contentProvider, isEditable));
              continue;
            }
            contentViewModelBaseList.Add((ContentViewModelBase) new ContentListsViewModel((Telerik.Sitefinity.GenericContent.Model.Content) contentItem, contentProvider));
            continue;
        }
      }
      return (IList<ContentViewModelBase>) contentViewModelBaseList;
    }

    private bool MatchTaxonFilter(ref string filter, out Guid taxonId, out string propertyName)
    {
      taxonId = Guid.Empty;
      propertyName = string.Empty;
      Match match = this.TaxonFilterRegex.Match(filter);
      int num = match == null || !match.Groups[0].Success ? 0 : (match.Groups[1].Success ? 1 : 0);
      if (num == 0)
        return num != 0;
      taxonId = new Guid(match.Groups[2].ToString());
      propertyName = match.Groups[1].ToString();
      filter = this.TaxonFilterRegex.Replace(filter, "");
      return num != 0;
    }

    private Regex TaxonFilterRegex
    {
      get
      {
        if (this.taxonFilterRegex == null)
          this.taxonFilterRegex = new Regex("TaxonId.(\\w+)==(^?[\\da-f]{8}-([\\da-f]{4}-){3}[\\da-f]{12}?$)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        return this.taxonFilterRegex;
      }
    }

    /// <summary>Gets the rating.</summary>
    /// <param name="itemTypeName">Name of the item type.</param>
    /// <param name="contentId">The content pageId.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns></returns>
    public Decimal GetRating(string itemTypeName, string contentId, string providerName)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      Type itemType = TypeResolutionService.ResolveType(itemTypeName);
      Guid id = new Guid(contentId);
      object component = ManagerBase.GetMappedManager(itemType, providerName).GetItem(itemType, id);
      PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(component);
      if (properties["VotesSum"] == null || properties["VotesCount"] == null)
        return 0.0M;
      Decimal num1 = (Decimal) properties["VotesSum"].GetValue(component);
      uint num2 = (uint) properties["VotesCount"].GetValue(component);
      return num2 <= 0U ? 0.0M : num1 / (Decimal) num2;
    }

    /// <summary>XMLs the get rating.</summary>
    /// <param name="itemTypeName">Name of the item type.</param>
    /// <param name="contentId">The content pageId.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns></returns>
    public Decimal XmlGetRating(string itemTypeName, string contentId, string providerName) => this.GetRating(itemTypeName, contentId, providerName);

    /// <summary>Sets the rating.</summary>
    /// <param name="value">The value.</param>
    /// <param name="itemTypeName">Name of the item type.</param>
    /// <param name="contentId">The content pageId.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns></returns>
    public RatingResult SetRating(
      Decimal value,
      string itemTypeName,
      string contentId,
      string providerName)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      if (string.IsNullOrEmpty(itemTypeName) || string.IsNullOrEmpty(contentId) || contentId.Length < 32)
        return new RatingResult(0U, -1.0M, string.Empty);
      Type itemType = TypeResolutionService.ResolveType(itemTypeName, false);
      if (itemType == (Type) null)
        return new RatingResult(0U, -1.0M, string.Empty);
      Guid guid = new Guid(contentId);
      IManager mappedManager = ManagerBase.GetMappedManager(itemType, providerName);
      object obj = mappedManager.GetItem(itemType, guid);
      if (!RatingHelper.CanUserRate(TypeSurrogateFactory.Instance.GetIdentity(obj.GetType(), obj, mappedManager)))
        return new RatingResult(0U, -1.0M, string.Empty);
      PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(obj);
      if (properties["VotesSum"] == null || properties["VotesCount"] == null)
        return new RatingResult(0U, -1.0M, string.Empty);
      if (RatingHelper.CanUserRate(guid))
      {
        uint num1 = (uint) properties["VotesCount"].GetValue(obj);
        Decimal num2 = (Decimal) properties["VotesSum"].GetValue(obj);
        properties["VotesCount"].SetValue(obj, (object) (uint) ((int) num1 + 1));
        properties["VotesSum"].SetValue(obj, (object) (num2 + value));
        RatingHelper.RateWithCurrentUser(guid, value);
        mappedManager.SaveChanges();
      }
      uint votesCount = (uint) properties["VotesCount"].GetValue(obj);
      Decimal num = (Decimal) properties["VotesSum"].GetValue(obj);
      Decimal average = 0.0M;
      if ((Decimal) votesCount > 0.0M)
        average = num / (Decimal) votesCount;
      return new RatingResult(votesCount, average, RatingHelper.GetSubtitleMessage(obj as Telerik.Sitefinity.GenericContent.Model.Content));
    }

    /// <summary>Resets the rating for a particular item.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="contentId">The content pageId.</param>
    /// <param name="providerName">Name of the provider.</param>
    public RatingResult ResetRating(
      string itemType,
      string contentId,
      string providerName)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      if (string.IsNullOrEmpty(itemType))
        return new RatingResult(0U, 0.0M, string.Empty);
      Type itemType1 = TypeResolutionService.ResolveType(itemType, false);
      if (itemType1 == (Type) null)
        return new RatingResult(0U, 0.0M, string.Empty);
      IManager mappedManager = ManagerBase.GetMappedManager(itemType1, providerName);
      object obj = mappedManager.GetItem(itemType1, new Guid(contentId));
      if (obj == null)
        return new RatingResult(0U, 0.0M, string.Empty);
      PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(obj);
      if (properties["VotesCount"] == null || properties["VotesSum"] == null)
        return new RatingResult(0U, 0.0M, string.Empty);
      properties["VotesCount"].SetValue(obj, (object) 0U);
      properties["VotesSum"].SetValue(obj, (object) 0.0M);
      mappedManager.SaveChanges();
      RatingHelper.DeleteForCurrentUser(TypeSurrogateFactory.Instance.GetIdentity(obj.GetType(), obj, mappedManager));
      return new RatingResult(0U, 0.0M, RatingHelper.GetSubtitleMessage(obj as Telerik.Sitefinity.GenericContent.Model.Content));
    }

    /// <summary>Resets the rating for a particular item.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="contentId">The content pageId.</param>
    /// <param name="providerName">Name of the provider.</param>
    public RatingResult XmlResetRating(
      string itemType,
      string contentId,
      string providerName)
    {
      return this.ResetRating(itemType, contentId, providerName);
    }

    /// <summary>XMLs the set rating.</summary>
    /// <param name="value">The value.</param>
    /// <param name="itemTypeName">Name of the item type.</param>
    /// <param name="contentId">The content pageId.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns></returns>
    public RatingResult XmlSetRating(
      Decimal value,
      string itemTypeName,
      string contentId,
      string providerName)
    {
      return this.SetRating(value, itemTypeName, contentId, providerName);
    }

    /// <summary>
    /// Determines whether the current user can rate by pageId or ip
    /// </summary>
    /// <param name="contentId">Id of the item type</param>
    /// <param name="itemType">CLR type of the item to rate</param>
    /// <param name="providerName">name of the content provider to use</param>
    /// <returns></returns>
    public bool CanRate(string contentId, string itemType, string providerName)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      return RatingHelper.CanUserRate(new Guid(contentId));
    }

    /// <summary>
    /// Determines whether the current user can rate by pageId or ip [xml responce]
    /// </summary>
    /// <param name="contentId">Id of the item type</param>
    /// <param name="itemType">CLR type of the item to rate</param>
    /// <param name="providerName">name of the content provider to use</param>
    /// <returns></returns>
    public bool XmlCanRate(string contentId, string itemType, string providerName) => this.CanRate(contentId, itemType, providerName);
  }
}
