// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ContentServiceBase`5
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text.RegularExpressions;
using System.Web;
using Telerik.OpenAccess.FetchOptimization;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.GenericContent;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.Data.WcfHelpers;
using Telerik.Sitefinity.Fluent;
using Telerik.Sitefinity.Folders;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.Localization;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.Libraries.BlobStorage;
using Telerik.Sitefinity.Modules.Libraries.Web.Services;
using Telerik.Sitefinity.RecycleBin;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Content.Data;
using Telerik.Sitefinity.Services.Content.Web.Services;
using Telerik.Sitefinity.Services.RelatedData.Messages;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Utilities;
using Telerik.Sitefinity.Versioning;
using Telerik.Sitefinity.Versioning.Model;
using Telerik.Sitefinity.Versioning.Web.Services;
using Telerik.Sitefinity.Web.Services;
using Telerik.Sitefinity.Workflow;

namespace Telerik.Sitefinity.Modules
{
  /// <summary>
  /// The WCF web service that is used to work with all types that inherit from base <see cref="T:Telerik.Sitefinity.GenericContent.Model.Content" />
  /// class.
  /// </summary>
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  public abstract class ContentServiceBase<TContent, TParentContent, TContentViewModel, TParentContentViewModel, TContentManager> : 
    IContentService<TContent, TContentViewModel>
    where TContent : Telerik.Sitefinity.GenericContent.Model.Content
    where TParentContent : Telerik.Sitefinity.GenericContent.Model.Content
    where TContentViewModel : ContentViewModelBase
    where TParentContentViewModel : ContentViewModelBase
    where TContentManager : class, IContentManager, IContentLifecycleManager<TContent>
  {
    private Regex taxonFilterRegex;
    private bool skipProtect;

    /// <summary>
    /// Gets the content of a specified type from the given provider.
    /// </summary>
    /// <param name="providerName">
    /// The provider from which the content ought to be retrieved.
    /// </param>
    /// <returns>Query of the content.</returns>
    public abstract IQueryable<TContent> GetContentItems(string providerName);

    /// <summary>
    /// Gets the child content of a specified content item from the given provider.
    /// </summary>
    /// <param name="parentId">Id of the parent content for which the children ought to be retrieved.</param>
    /// <param name="providerName">The provider from which the content ought to be retrieved.</param>
    /// <returns>Query of the child content.</returns>
    public abstract IQueryable<TContent> GetChildContentItems(
      Guid parentId,
      string providerName);

    /// <summary>
    /// Gets all descendants of a specified library from the given provider and applies a filter.
    /// </summary>
    /// <param name="parentId">Id of the parent content for which the descendants ought to be retrieved.</param>
    /// <param name="providerName">The provider from which the content ought to be retrieved.</param>
    /// <param name="filterExpression">The filter expression to apply on the results.</param>
    /// <returns>Query for the descendants.</returns>
    protected virtual IQueryable<TContent> GetDescendants(
      Guid parentId,
      string providerName,
      string filterExpression)
    {
      return (IQueryable<TContent>) null;
    }

    /// <summary>
    /// Gets all descendants of a specified library from the given provider.
    /// </summary>
    /// <param name="parentId">Id of the parent content for which the descendants ought to be retrieved.</param>
    /// <param name="providerName">The provider from which the content ought to be retrieved.</param>
    /// <returns>Query for the descendants.</returns>
    protected virtual IQueryable<TContent> GetDescendants(
      Guid parentId,
      string providerName)
    {
      return (IQueryable<TContent>) null;
    }

    /// <summary>Gets a single content.</summary>
    /// <param name="id">Id of the content to be retrieved.</param>
    /// <param name="providerName">Name of the provider from which the content ought to be retrieved.</param>
    /// <returns>A single content.</returns>
    public abstract TContent GetContentItem(Guid id, string providerName);

    public abstract TParentContent GetParentContentItem(Guid id, string providerName);

    /// <summary>
    /// Gets the manager to be used by the service. Concrete implementation of the service must provide this.
    /// </summary>
    /// <param name="providerName">Name of the provider with which the manager ought to be instantiated.</param>
    /// <returns>An instance of the manager.</returns>
    public abstract TContentManager GetManager(string providerName);

    /// <summary>Gets the list of view model content.</summary>
    /// <param name="contentList">List of the actual content.</param>
    /// <returns>An enumerable of view model content objects.</returns>
    [Obsolete("Please use GetViewModelList with four args. Date: 2011/5/20.")]
    public abstract IEnumerable<TContentViewModel> GetViewModelList(
      IEnumerable<TContent> contentList,
      ContentDataProviderBase dataProvider);

    /// <summary>Gets the list of view model content.</summary>
    /// <param name="contentList">The content list.</param>
    /// <param name="dataProvider">The data provider.</param>
    /// <param name="liveContentDictionary">The live content dictionary.</param>
    /// <param name="tempContentDictionary">The temp content dictionary.</param>
    /// <returns>An enumerable of view model content objects.</returns>
    public virtual IEnumerable<TContentViewModel> GetViewModelList(
      IEnumerable<TContent> contentList,
      ContentDataProviderBase dataProvider,
      IDictionary<Guid, TContent> liveContentDictionary,
      IDictionary<Guid, TContent> tempContentDictionary)
    {
      ServiceUtility.ProtectBackendServices();
      return this.GetViewModelList(contentList, dataProvider);
    }

    /// <summary>Gets the live items list.</summary>
    /// <param name="contentList">The content list.</param>
    /// <param name="manager">The manager.</param>
    /// <returns></returns>
    public virtual IDictionary<Guid, TContent> GetLiveItemsList(
      List<TContent> contentList,
      TContentManager manager,
      CultureInfo culture = null)
    {
      return typeof (TContent).IsILifecycle() ? this.GetRelatedLifecycleItems(contentList, manager, ContentLifecycleStatus.Live, culture) : this.GetRelevantItemsList(contentList, manager, ContentLifecycleStatus.Live);
    }

    /// <summary>
    /// Gets the temp items relevant to the specified content items in the content list
    /// </summary>
    /// <param name="contentList">The content list.</param>
    /// <param name="manager">The content manager.</param>
    public virtual IDictionary<Guid, TContent> GetTempItemsList(
      List<TContent> contentList,
      TContentManager manager,
      CultureInfo culture = null)
    {
      return typeof (TContent).IsILifecycle() ? this.GetRelatedLifecycleItems(contentList, manager, ContentLifecycleStatus.Temp, culture) : this.GetRelevantItemsList(contentList, manager, ContentLifecycleStatus.Temp);
    }

    /// <summary>
    /// Gets items which are live or temp or master items (depending on the specified ContentLifecycleStatus)
    /// of the items in the specified list in the specified culture
    /// </summary>
    /// <param name="contentList">The list of content items.</param>
    /// <param name="manager">The content manager.</param>
    /// <param name="status">The content status.</param>
    public IDictionary<Guid, TContent> GetRelatedLifecycleItems(
      List<TContent> contentList,
      TContentManager manager,
      ContentLifecycleStatus status,
      CultureInfo culture = null)
    {
      Dictionary<Guid, TContent> dictionary = new Dictionary<Guid, TContent>();
      Guid[] contentItemsIds = contentList.Select<TContent, Guid>((Func<TContent, Guid>) (cl => cl.Id)).ToArray<Guid>();
      if (contentItemsIds.Length != 0)
      {
        FetchStrategy strategyFromCurrent = this.GetFetchStrategyFromCurrent((IManager) manager);
        string language = culture.GetLanguageKey();
        IQueryable<TContent> source = manager.GetItems<TContent>().Where<TContent>((Expression<Func<TContent, bool>>) (t => contentItemsIds.Contains<Guid>(t.OriginalContentId)));
        IQueryable<TContent> queryable;
        if (DataExtensions.AppSettings.ContextSettings.Multilingual)
        {
          if (status == ContentLifecycleStatus.Temp || status == ContentLifecycleStatus.PartialTemp)
          {
            queryable = source.Where<TContent>((Expression<Func<TContent, bool>>) (t => ((int) t.Status == 1 || (int) t.Status == 8) && ((ILifecycleDataItem) t).LanguageData.Any<LanguageData>((Func<LanguageData, bool>) (l => l.Language == language))));
          }
          else
          {
            queryable = source.Where<TContent>((Expression<Func<TContent, bool>>) (t => (int) t.Status == 2));
            strategyFromCurrent?.LoadWith(typeof (TContent).FullName, "PublishedTranslations");
          }
        }
        else
          queryable = source.Where<TContent>((Expression<Func<TContent, bool>>) (t => (int) t.Status == (int) status));
        if (strategyFromCurrent != null)
        {
          strategyFromCurrent.LoadWith(typeof (TContent).FullName, "LanguageData");
          strategyFromCurrent.LoadWith(typeof (TContent).FullName, "PublishedTranslations");
          queryable = queryable.LoadWith<TContent>(strategyFromCurrent);
        }
        foreach (TContent content in queryable.ToArray<TContent>())
        {
          if (!dictionary.ContainsKey(content.OriginalContentId))
            dictionary.Add(content.OriginalContentId, content);
        }
      }
      return dictionary.Count <= 0 ? (IDictionary<Guid, TContent>) null : (IDictionary<Guid, TContent>) dictionary;
    }

    /// <summary>
    /// Gets the content items relevant to the specified master content items
    /// </summary>
    /// <param name="contentList">The content list.</param>
    /// <param name="manager">The manager.</param>
    /// <param name="status">The status.</param>
    /// <returns></returns>
    public virtual IDictionary<Guid, TContent> GetRelevantItemsList(
      List<TContent> contentList,
      TContentManager manager,
      ContentLifecycleStatus status)
    {
      if (!this.SkipProtect)
        ServiceUtility.ProtectBackendServices();
      Dictionary<Guid, TContent> dictionary = new Dictionary<Guid, TContent>();
      Guid[] contentItemsIds = contentList.Select<TContent, Guid>((Func<TContent, Guid>) (cl => cl.Id)).ToArray<Guid>();
      if (contentItemsIds.Length != 0)
      {
        IQueryable<TContent> items = manager.GetItems<TContent>();
        Expression<Func<TContent, bool>> predicate = (Expression<Func<TContent, bool>>) (t => contentItemsIds.Contains<Guid>(t.OriginalContentId) && (int) t.Status == (int) status);
        foreach (TContent content in (IEnumerable<TContent>) items.Where<TContent>(predicate))
          dictionary.Add(content.OriginalContentId, content);
      }
      return dictionary.Count <= 0 ? (IDictionary<Guid, TContent>) null : (IDictionary<Guid, TContent>) dictionary;
    }

    /// <summary>Gets the item from dictionary.</summary>
    /// <param name="dictionary">The dictionary.</param>
    /// <param name="key">The key.</param>
    public virtual V GetItemFromDictionary<K, V>(IDictionary<K, V> dictionary, K key) => dictionary.GetValueOrDefault<K, V>(key);

    /// <summary>
    /// Publish a list of child items, identified by their IDs
    /// </summary>
    /// <param name="ids">Strigified IDs of the child content items to publish</param>
    /// <param name="providerName">Name of the data provider to use</param>
    /// <param name="parentId">ID of the parent</param>
    /// <returns>True if all are published</returns>
    public virtual bool BatchPublishChildItem(
      string[] ids,
      string providerName,
      string parentId,
      string workflowOperation)
    {
      ServiceUtility.ProtectBackendServices();
      return this.BatchPublishImpl(ids, providerName);
    }

    /// <summary>
    /// Publish a list of child items, identified by their IDs
    /// </summary>
    /// <param name="ids">Strigified IDs of the child content items to publish</param>
    /// <param name="providerName">Name of the data provider to use</param>
    /// <param name="parentId">ID of the parent</param>
    /// <returns>True if all are published</returns>
    public virtual bool BatchPublishChildItemInXml(
      string[] ids,
      string providerName,
      string parentID,
      string workflowOperation)
    {
      ServiceUtility.ProtectBackendServices();
      return this.BatchPublishImpl(ids, providerName);
    }

    /// <summary>Publishes a list of items, identitified by their IDs</summary>
    /// <param name="ids">Strigified IDs of the content items to publish</param>
    /// <param name="providerName">Name of the data provider that should be used</param>
    /// <returns>True on success, false on failure</returns>
    public virtual bool BatchPublish(string[] ids, string providerName, string workflowOperation)
    {
      ServiceUtility.ProtectBackendServices();
      return this.BatchPublishImpl(ids, providerName);
    }

    /// <summary>Publishes a list of items, identitified by their IDs</summary>
    /// <param name="ids">Strigified IDs of the content items to publish</param>
    /// <param name="providerName">Name of the data provider that should be used</param>
    /// <returns>True on success, false on failure</returns>
    public virtual bool BatchPublishInXml(
      string[] ids,
      string providerName,
      string workflowOperation)
    {
      ServiceUtility.ProtectBackendServices();
      return this.BatchPublishImpl(ids, providerName);
    }

    /// <summary>
    /// Unpublishes a list of items, identitified by their IDs
    /// </summary>
    /// <param name="ids">Strigified IDs of the content items to unpublish</param>
    /// <param name="providerName">Name of the data provider that should be used</param>
    /// <returns>True on success, false on failure</returns>
    public virtual bool BatchUnpublish(string[] ids, string providerName, string workflowOperation)
    {
      ServiceUtility.ProtectBackendServices();
      return this.BatchUnpublishImpl(ids, providerName);
    }

    /// <summary>
    /// Unpublishes a list of items, identitified by their IDs
    /// </summary>
    /// <param name="ids">Strigified IDs of the content items to unpublish</param>
    /// <param name="providerName">Name of the data provider that should be used</param>
    /// <returns>True on success, false on failure</returns>
    public virtual bool BatchUnpublishInXml(
      string[] ids,
      string providerName,
      string workflowOperation)
    {
      ServiceUtility.ProtectBackendServices();
      return this.BatchUnpublishImpl(ids, providerName);
    }

    /// <summary>
    /// Unpublish a list of child items, identified by their IDs
    /// </summary>
    /// <param name="ids">Strigified IDs of the child content items to unpublish</param>
    /// <param name="providerName">Name of the data provider to use</param>
    /// <param name="parentId">ID of the parent</param>
    /// <returns>True if all are unpublished</returns>
    public virtual bool BatchUnpublishChildItem(
      string[] ids,
      string providerName,
      string parentId,
      string workflowOperation)
    {
      ServiceUtility.ProtectBackendServices();
      return this.BatchUnpublishImpl(ids, providerName);
    }

    /// <summary>
    /// Unpublish a list of child items, identified by their IDs
    /// </summary>
    /// <param name="ids">Strigified IDs of the child content items to unpublish</param>
    /// <param name="providerName">Name of the data provider to use</param>
    /// <param name="parentId">ID of the parent</param>
    /// <returns>True if all are unpublished</returns>
    public virtual bool BatchUnpublishChildItemInXml(
      string[] ids,
      string providerName,
      string parentID,
      string workflowOperation)
    {
      ServiceUtility.ProtectBackendServices();
      return this.BatchUnpublishImpl(ids, providerName);
    }

    /// <summary>
    /// Deletes the temp of a content item with <paramref name="contentId" />
    /// </summary>
    /// <param name="contentId">String that is parsalbe by the Guid constructor. Used for designating the content ID whose temp to delete (or the temp's ID itself)</param>
    /// <param name="providerName">Name of the provider to use</param>
    /// <param name="force">Force deletion of temp (e.g. when not owner of temp, but admin)</param>
    /// <returns>True if it were deleted, false otherwize</returns>
    public virtual bool DeleteTemp(
      string contentIdString,
      string providerName,
      bool force,
      string workflowOperation)
    {
      ServiceUtility.ProtectBackendServices();
      return this.DeleteTempInternal(contentIdString, providerName, force);
    }

    /// <summary>
    /// Deletes the temp of a content item with <paramref name="contentId" />
    /// </summary>
    /// <param name="contentId">String that is parsalbe by the Guid constructor. Used for designating the content ID whose temp to delete (or the temp's ID itself)</param>
    /// <param name="providerName">Name of the provider to use</param>
    /// <param name="force">Force deletion of temp (e.g. when not owner of temp, but admin)</param>
    /// <returns>True if it were deleted, false otherwize</returns>
    public virtual bool DeleteTempInXml(
      string contentIdString,
      string providerName,
      bool force,
      string workflowOperation)
    {
      ServiceUtility.ProtectBackendServices();
      return this.DeleteTempInternal(contentIdString, providerName, force);
    }

    /// <summary>
    /// Gets the single content item and returs it in JSON format.
    /// </summary>
    /// <param name="contentId">Id of the content item that ought to be retrieved.</param>
    /// <param name="providerName">Name of the content provider that is to be used to retrieve the content item.</param>
    /// <param name="version">The version id. If not specified returns the current master version, otherwise the specified version from the history</param>
    /// <param name="published">True if only published content is desired</param>
    /// <param name="checkOut">True to check out for the currently logged in user</param>
    /// <param name="workflowOperation">The workflow operation.</param>
    /// <param name="duplicate">Indicates whether the service should return a duplicate item.</param>
    /// 
    ///             ///
    ///             <returns>
    /// An instance of ContentItemContext that contains the content item to be retrieved.
    /// </returns>
    public virtual ContentItemContext<TContent> GetContent(
      string contentId,
      string providerName,
      string version,
      bool published,
      bool checkOut,
      string workflowOperation,
      bool duplicate)
    {
      ServiceUtility.ProtectBackendServices();
      return this.GetContentInternal(contentId, providerName, version, published, checkOut, duplicate);
    }

    /// <summary>
    /// Gets the single content item and returs it in XML format.
    /// </summary>
    /// <param name="contentId">Id of the content item that ought to be retrieved.</param>
    /// <param name="providerName">Name of the content provider that is to be used to retrieve the content item.</param>
    /// <param name="version">The version id. If not specified returns the current master version, otherwise the specified version from the history</param>
    /// <param name="published">True if only published content is desired</param>
    /// <param name="checkOut">True to check out for the currently logged in user</param>
    /// <param name="workflowOperation">The workflow operation.</param>
    /// <param name="duplicate">Indicates whether the service should return a duplicate item.</param>
    /// <returns>
    /// An instance of ContentItemContext that contains the content item to be retrieved.
    /// </returns>
    public virtual ContentItemContext<TContent> GetContentInXml(
      string contentId,
      string providerName,
      string version,
      bool published,
      bool checkOut,
      string workflowOperation,
      bool duplicate)
    {
      ServiceUtility.ProtectBackendServices();
      return this.GetContentInternal(contentId, providerName, version, published, checkOut, duplicate);
    }

    /// <summary>Gets the single content item in live state.</summary>
    /// <param name="contentId">Id of the content item that ought to be retrieved.</param>
    /// <param name="providerName">Name of the content provider that is to be used to retrieve the content item.</param>
    /// <returns>An instance of ContentItemContext that contains the content item to be retrieved.</returns>
    public ContentItemContext<TContent> GetLiveContent(
      string contentId,
      string providerName)
    {
      return this.GetLiveContentInternal(contentId, providerName);
    }

    /// <summary>Gets the single content item in live state.</summary>
    /// <param name="contentId">Id of the content item that ought to be retrieved.</param>
    /// <param name="providerName">Name of the content provider that is to be used to retrieve the content item.</param>
    /// <returns>An instance of ContentItemContext that contains the content item to be retrieved.</returns>
    public ContentItemContext<TContent> GetLiveContentInXml(
      string contentId,
      string providerName)
    {
      return this.GetLiveContentInternal(contentId, providerName);
    }

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
    /// <returns>
    /// An instance of ContentItemContext that contains the content item to be retrieved.
    /// </returns>
    public virtual ContentItemContext<TContent> GetChildContent(
      string parentId,
      string contentId,
      string providerName,
      string newParentId,
      string version,
      bool published,
      bool checkOut,
      string workflowOperation,
      bool duplicate)
    {
      ServiceUtility.ProtectBackendServices();
      return this.GetChildContentInternal(parentId, contentId, providerName, version, published, checkOut, duplicate);
    }

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
    /// <returns>
    /// An instance of ContentItemContext that contains the content item to be retrieved.
    /// </returns>
    public virtual ContentItemContext<TContent> GetChildContentInXml(
      string parentId,
      string contentId,
      string providerName,
      string newParentId,
      string version,
      bool published,
      bool checkOut,
      string workflowOperation,
      bool duplicate)
    {
      ServiceUtility.ProtectBackendServices();
      return this.GetChildContentInternal(parentId, contentId, providerName, version, published, checkOut, duplicate);
    }

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
    /// <returns>
    /// An instance of ContentItemContext that contains the content item that was saved.
    /// </returns>
    public virtual ContentItemContext<TContent> SaveContent(
      ContentItemContext<TContent> content,
      string contentId,
      string providerName,
      string version,
      bool published,
      bool checkOut,
      string workflowOperation)
    {
      if (this.ShouldProtectBackendServices())
        ServiceUtility.ProtectBackendServices();
      return this.SaveContentInternal(content, providerName, workflowOperation);
    }

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
    /// <returns>
    /// An instance of ContentItemContext that contains the content item that was saved.
    /// </returns>
    public virtual ContentItemContext<TContent> SaveContentInXml(
      ContentItemContext<TContent> content,
      string contentId,
      string providerName,
      string version,
      bool published,
      bool checkOut,
      string workflowOperation)
    {
      ServiceUtility.ProtectBackendServices();
      return this.SaveContentInternal(content, providerName, workflowOperation);
    }

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
    /// <returns>
    /// An instance of ContentItemContext that contains the content item that was saved.
    /// </returns>
    public virtual ContentItemContext<TContent> SaveChildContent(
      ContentItemContext<TContent> content,
      string parentId,
      string contentId,
      string providerName,
      string newParentId,
      string version,
      bool published,
      bool checkOut,
      string workflowOperation)
    {
      ServiceUtility.ProtectBackendServices();
      return this.SaveChildContentInternal(content, parentId, providerName, newParentId, workflowOperation);
    }

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
    /// <returns>
    /// An instance of ContentItemContext that contains the content item that was saved.
    /// </returns>
    public virtual ContentItemContext<TContent> SaveChildContentInXml(
      ContentItemContext<TContent> content,
      string parentId,
      string contentId,
      string providerName,
      string newParentId,
      string version,
      bool published,
      bool checkOut,
      string workflowOperation)
    {
      ServiceUtility.ProtectBackendServices();
      return this.SaveChildContentInternal(content, parentId, providerName, newParentId, workflowOperation);
    }

    /// <summary>
    /// Gets the collection of content item summary objects and returns them in JSON format.
    /// </summary>
    /// <param name="sortExpression">Sort expression used to order the content items in the result set.</param>
    /// <param name="skip">The number of items to skip before starting to take the items in the result set.</param>
    /// <param name="take">The number of items to take into the result set.</param>
    /// <param name="filter">Filter expression to be used to filter the items that will be taken into the result set.</param>
    /// <param name="providerName">Name of the content provider to be used to get the content items.</param>
    /// <returns>
    /// An instance of CollectionContext object that contains the collection of ContentSummary items.
    /// </returns>
    public virtual CollectionContext<TContentViewModel> GetContentItems(
      string sortExpression,
      int skip,
      int take,
      string filter,
      string providerName,
      string workflowOperation)
    {
      ServiceUtility.ProtectBackendServices();
      return this.GetContentItemsInternal(sortExpression, skip, take, filter, providerName);
    }

    /// <summary>
    /// Gets the collection of content item summary objects and returns them in XML format.
    /// </summary>
    /// <param name="sortExpression">Sort expression used to order the content items in the result set.</param>
    /// <param name="skip">The number of items to skip before starting to take the items in the result set.</param>
    /// <param name="take">The number of items to take into the result set.</param>
    /// <param name="filter">Filter expression to be used to filter the items that will be taken into the result set.</param>
    /// <param name="providerName">Name of the content provider to be used to get the content items.</param>
    /// <returns>
    /// An instance of CollectionContext object that contains the collection of ContentSummary items.
    /// </returns>
    public virtual CollectionContext<TContentViewModel> GetContentItemsInXml(
      string sortExpression,
      int skip,
      int take,
      string filter,
      string providerName,
      string workflowOperation)
    {
      ServiceUtility.ProtectBackendServices();
      return this.GetContentItemsInternal(sortExpression, skip, take, filter, providerName);
    }

    /// <summary>
    /// Gets the collection of child content item summary objects and returns them in JSON format.
    /// </summary>
    /// <param name="parentId">The if of the parent content item for which the children ought to be retrieved.</param>
    /// <param name="providerName">Name of the content provider to be used to get the child content items.</param>
    /// <param name="sortExpression">Sort expression used to order the child content items in the result set.</param>
    /// <param name="filter">Filter expression to be used to filter the items that will be taken into the results set.</param>
    /// <param name="skip">The number of items to skip before starting to take the items in the result set.</param>
    /// <param name="take">The number of items to take into the result set.</param>
    /// <param name="excludeFolders">Default value is false. If set to true gets only the specific content items, else gets the media content items and the folders of the same level.</param>
    /// <param name="includeSubFoldersItems">Default value is false. If set to true then gets all content items including those in the library subfolders.</param>
    /// <returns>
    /// An instance of CollectionContext object that contains the collection of ContentSummary items.
    /// </returns>
    public virtual CollectionContext<TContentViewModel> GetChildrenContentItems(
      string parentId,
      string providerName,
      string sortExpression,
      string filter,
      int skip,
      int take,
      string workflowOperation,
      bool excludeFolders = false,
      bool includeSubFoldersItems = false)
    {
      if (!this.SkipProtect)
        ServiceUtility.ProtectBackendServices();
      return this.GetChildrenContentItemsInternal(parentId, providerName, sortExpression, filter, skip, take, includeSubFoldersItems);
    }

    /// <summary>
    /// Gets the collection of child content item summary objects and returns them in XML format.
    /// </summary>
    /// <param name="parentId">The if of the parent content item for which the children ought to be retrieved.</param>
    /// <param name="providerName">Name of the content provider to be used to get the child content items.</param>
    /// <param name="sortExpression">Sort expression used to order the child content items in the result set.</param>
    /// <param name="filter">Filter expression to be used to filter the items that will be taken into the results set.</param>
    /// <param name="skip">The number of items to skip before starting to take the items in the result set.</param>
    /// <param name="take">The number of items to take into the result set.</param>
    /// <param name="excludeFolders">Default value is false. If set to true gets only the specific content items, else gets the media content items and the folders of the same level.</param>
    /// <param name="includeSubFoldersItems">Default value is false. If set to true then gets all content items including those in the library subfolders.</param>
    /// <returns>
    /// An instance of CollectionContext object that contains the collection of ContentSummary items.
    /// </returns>
    public virtual CollectionContext<TContentViewModel> GetChildrenContentItemsInXml(
      string parentId,
      string providerName,
      string sortExpression,
      string filter,
      int skip,
      int take,
      string workflowOperation,
      bool excludeFolders = false,
      bool includeSubFoldersItems = false)
    {
      ServiceUtility.ProtectBackendServices();
      return this.GetChildrenContentItemsInternal(parentId, providerName, sortExpression, filter, skip, take, includeSubFoldersItems);
    }

    /// <summary>
    /// Deletes the content item and returns true if the content item has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="Id">Id of the content item to be deleted.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the item.</param>
    /// <param name="checkOut">Ignored</param>
    /// <param name="published">Ignored</param>
    public virtual bool DeleteContent(
      string Id,
      string providerName,
      string version,
      bool published,
      bool checkOut,
      string workflowOperation,
      string deletedLanguage,
      bool checkRelatingData)
    {
      ServiceUtility.ProtectBackendServices();
      return this.DeleteContentInternal(Id, providerName, version, deletedLanguage, checkRelatingData);
    }

    /// <summary>
    /// Deletes the content item and returns true if the content item has been deleted; otherwise false.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="Id">Id of the content item to be deleted.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the item.</param>
    /// <param name="checkOut">Ignored</param>
    /// <param name="published">Ignored</param>
    public virtual bool DeleteContentInXml(
      string Id,
      string providerName,
      string version,
      bool published,
      bool checkOut,
      string workflowOperation,
      string deletedLanguage,
      bool checkRelatingData)
    {
      ServiceUtility.ProtectBackendServices();
      return this.DeleteContentInternal(Id, providerName, version, deletedLanguage, checkRelatingData);
    }

    /// <summary>
    /// Deletes an array of content items.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="Ids">An array containing the Ids of the content items to be deleted.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the item.</param>
    public virtual bool BatchDeleteContent(
      string[] Ids,
      string providerName,
      string workflowOperation,
      string deletedLanguage,
      bool checkRelatingData)
    {
      ServiceUtility.ProtectBackendServices();
      return this.BatchDeleteContentInternal(Ids, providerName, deletedLanguage, checkRelatingData);
    }

    /// <summary>
    /// Deletes an array of content items.
    /// Result is returned in Xml format.
    /// </summary>
    /// <param name="Ids">An array containing the Ids of the content items to be deleted.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the item.</param>
    public virtual bool BatchDeleteContentInXml(
      string[] Ids,
      string providerName,
      string workflowOperation,
      string deletedLanguage,
      bool checkRelatingData)
    {
      ServiceUtility.ProtectBackendServices();
      return this.BatchDeleteContentInternal(Ids, providerName, deletedLanguage, checkRelatingData);
    }

    /// <summary>
    /// Deletes the child content item and returns true if the child content item has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="pageId">Id of the child content item to be deleted.</param>
    /// <param name="parentId">Id of the parent content item who's child ought to be deleted.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the item.</param>
    /// <param name="checkOut">Ignored</param>
    /// <param name="published">Ignored</param>
    /// <returns></returns>
    public virtual bool DeleteChildContent(
      string id,
      string parentId,
      string providerName,
      string newParentId,
      string version,
      bool published,
      bool checkOut,
      string workflowOperation,
      bool checkRelatingData)
    {
      ServiceUtility.ProtectBackendServices();
      return this.DeleteChildContentInternal(id, parentId, providerName, version, checkRelatingData);
    }

    /// <summary>
    /// Deletes the child content item and returns true if the child content item has been deleted; otherwise false.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="pageId">Id of the child content item to be deleted.</param>
    /// <param name="parentId">Id of the parent content item who's child ought to be deleted.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the item.</param>
    /// <param name="checkOut">Ignored</param>
    /// <param name="published">Ignored</param>
    /// <returns></returns>
    public virtual bool DeleteChildContentInXml(
      string id,
      string parentId,
      string providerName,
      string newParentId,
      string version,
      bool published,
      bool checkOut,
      string workflowOperation,
      bool checkRelatingData)
    {
      ServiceUtility.ProtectBackendServices();
      return this.DeleteChildContentInternal(id, parentId, providerName, version, checkRelatingData);
    }

    /// <summary>
    /// Deletes an array of content items.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="Ids">An array containing the Ids of the content items to be deleted.</param>
    /// <param name="parentId">Id of the parent content item who's child ought to be deleted.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the item.</param>
    public virtual bool BatchDeleteChildContent(
      string[] Ids,
      string parentId,
      string providerName,
      string workflowOperation,
      string deletedLanguage,
      bool checkRelatingData)
    {
      ServiceUtility.ProtectBackendServices();
      return this.BatchDeleteContentInternal(Ids, providerName, deletedLanguage, checkRelatingData);
    }

    /// <summary>
    /// Deletes an array of content items.
    /// Result is returned in Xml format.
    /// </summary>
    /// <param name="Ids">An array containing the Ids of the content items to be deleted.</param>
    /// <param name="parentId">Id of the parent content item who's child ought to be deleted.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the item.</param>
    public virtual bool BatchDeleteChildContentInXml(
      string[] Ids,
      string parentId,
      string providerName,
      string workflowOperation,
      string deletedLanguage,
      bool checkRelatingData)
    {
      ServiceUtility.ProtectBackendServices();
      return this.BatchDeleteContentInternal(Ids, providerName, deletedLanguage, checkRelatingData);
    }

    /// <summary>
    /// Reorders the content item. The item should implement IOrderableItem
    /// </summary>
    /// <param name="contentId">The content pageId.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="oldPosition">The old position.</param>
    /// <param name="newPosition">The new position.</param>
    /// <returns>True if reordering is successful</returns>
    public virtual void ReorderContent(
      string contentId,
      string providerName,
      float oldPosition,
      float newPosition,
      string workflowOperation)
    {
      ServiceUtility.ProtectBackendServices();
      this.ReorderContentInternal(contentId, providerName, oldPosition, newPosition);
    }

    /// <summary>Reorders multpile content items</summary>
    /// <param name="contentIdnewOrdinal">Dictionary where the key is the content item and the value is the offset</param>
    /// <param name="providerName">Name of the provider.</param>
    public virtual void BatchReorderContent(
      Dictionary<string, float> contentIdnewOrdinal,
      string providerName,
      string workflowOperation)
    {
      ServiceUtility.ProtectBackendServices();
      this.BatchReorderContentInternal(contentIdnewOrdinal, providerName);
    }

    /// <summary>
    /// Changes the parent for an array of content items.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="ids">An array of content items ids.</param>
    /// <param name="newParentId">The new parent pageId.</param>
    /// <param name="providerName">Name of the provider.</param>
    public virtual bool BatchChangeParent(
      string[] ids,
      string newParentId,
      string providerName,
      string workflowOperation)
    {
      ServiceUtility.ProtectBackendServices(true);
      return this.BatchChangeParentInternal(ids, newParentId, providerName);
    }

    /// <summary>
    /// Changes the parent for an array of content items.
    /// Result is returned in Xml format.
    /// </summary>
    /// <param name="ids">An array of content items ids.</param>
    /// <param name="newParentId">The new parent pageId.</param>
    /// <param name="providerName">Name of the provider.</param>
    public virtual bool BatchChangeParentInXml(
      string[] ids,
      string newParentId,
      string providerName,
      string workflowOperation)
    {
      ServiceUtility.ProtectBackendServices();
      return this.BatchChangeParentInternal(ids, newParentId, providerName);
    }

    public virtual CollectionContext<TContentViewModel> GetPredecessorItems(
      string contentId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      bool onlyPath,
      string itemType)
    {
      ServiceUtility.ProtectBackendServices();
      return this.GetContentItemsInternal(sortExpression, skip, take, filter, provider);
    }

    public CollectionContext<TContentViewModel> GetPredecessorItemsInXml(
      string contentId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      bool onlyPath,
      string itemType)
    {
      ServiceUtility.ProtectBackendServices();
      return this.GetContentItemsInternal(sortExpression, skip, take, filter, provider);
    }

    public CollectionContext<TContentViewModel> BatchPlaceContent(
      string[] sourceContentIds,
      string parentId,
      string providerName,
      string placePosition,
      string destination)
    {
      ServiceUtility.ProtectBackendServices();
      return this.BatchPlaceContentInternal(sourceContentIds, parentId, providerName, placePosition, destination);
    }

    public CollectionContext<TContentViewModel> BatchPlaceContentInXml(
      string[] sourceContentIds,
      string parentId,
      string providerName,
      string placePosition,
      string destination)
    {
      ServiceUtility.ProtectBackendServices();
      return this.BatchPlaceContentInternal(sourceContentIds, parentId, providerName, placePosition, destination);
    }

    public void BatchMoveContent(
      string[] sourceContentIds,
      string parentId,
      string providerName,
      string direction)
    {
      ServiceUtility.ProtectBackendServices();
      this.BatchMoveContentInternal(sourceContentIds, parentId, providerName, direction);
    }

    public void BatchMoveContentInXml(
      string[] sourceContentIds,
      string parentId,
      string providerName,
      string direction)
    {
      ServiceUtility.ProtectBackendServices();
      this.BatchMoveContentInternal(sourceContentIds, parentId, providerName, direction);
    }

    protected virtual bool ShouldProtectBackendServices() => true;

    protected virtual void SaveChanges(IManager manager)
    {
      if (manager.Provider.TransactionName.IsNullOrWhitespace())
        manager.SaveChanges();
      else
        TransactionManager.CommitTransaction(manager.Provider.TransactionName);
    }

    protected virtual void CancelChanges(IManager manager)
    {
      if (manager.Provider.TransactionName.IsNullOrWhitespace())
        manager.CancelChanges();
      else
        TransactionManager.RollbackTransaction(manager.Provider.TransactionName);
    }

    /// <summary>
    /// Common (for JSON and XML) implementation for unpublishing content items
    /// </summary>
    /// <param name="ids">List of  content IDs</param>
    /// <param name="providerName">Provider name</param>
    /// <returns>True if all were unpublished</returns>
    protected virtual bool BatchUnpublishImpl(string[] ids, string providerName)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      return this.CallWorkflowBatch(ids, providerName, "Unpublish");
    }

    /// <summary>
    /// Common (for XML and JSON) implementation for publishing an item
    /// </summary>
    /// <param name="ids">List of content IDs</param>
    /// <param name="providerName">Name of the provider</param>
    /// <returns>True if all were published</returns>
    protected virtual bool BatchPublishImpl(string[] ids, string providerName)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      return this.CallWorkflowBatch(ids, providerName, "Publish");
    }

    protected internal virtual bool CallWorkflowBatch(
      string[] ids,
      string providerName,
      string operationName,
      string deletedLanguage = null,
      bool checkRelatingData = false)
    {
      string empty = string.Empty;
      CultureInfo language = (CultureInfo) null;
      if (!string.IsNullOrEmpty(deletedLanguage) && SystemManager.CurrentContext.AppSettings.Multilingual)
        language = new CultureInfo(deletedLanguage);
      WorkflowBatchExceptionHandler exceptionHandler = new WorkflowBatchExceptionHandler();
      foreach (string id in ids)
      {
        try
        {
          TContent contentItem = this.GetContentItem(new Guid(id), providerName);
          TContentManager manager = this.GetManager(providerName);
          if (operationName == "Delete")
          {
            if ((object) contentItem is IWorkflowItem)
            {
              empty = contentItem.Title.ToString();
              Dictionary<string, string> dictionary = new Dictionary<string, string>();
              dictionary.Add("ContentType", typeof (TContent).AssemblyQualifiedName);
              WorkflowManager.AddLanguageToWorkflowContext(dictionary, language);
              dictionary.Add("CheckRelatingData", checkRelatingData.ToString());
              WorkflowManager.MessageWorkflow(contentItem.Id, contentItem.GetType(), providerName, operationName, true, dictionary);
            }
            else
            {
              manager.DeleteItem((object) contentItem, language);
              manager.SaveChanges();
            }
          }
          else
          {
            if ((object) contentItem is IWorkflowItem)
            {
              if (!((IWorkflowItem) (object) contentItem).IsOperationSupported(operationName))
                continue;
            }
            empty = contentItem.Title.ToString();
            WorkflowManager.MessageWorkflow(contentItem.Id, contentItem.GetType(), providerName, operationName, false, new Dictionary<string, string>()
            {
              {
                "ContentType",
                contentItem.GetType().FullName
              }
            });
          }
        }
        catch (Exception ex)
        {
          exceptionHandler.RegisterException(ex, empty);
        }
      }
      exceptionHandler.ThrowAccumulatedErrorForContent(((IEnumerable<string>) ids).Count<string>(), operationName);
      return true;
    }

    /// <summary>
    /// Internal implementation for deleting a temp for an item
    /// </summary>
    /// <param name="contentIdString">Id of the item whose temp to delete in string format</param>
    /// <param name="providerName">Name of the provider</param>
    /// <param name="force">Force deletion of temp (e.g. when not owner of temp, but admin)</param>
    /// <returns></returns>
    protected virtual bool DeleteTempInternal(
      string contentIdString,
      string providerName,
      bool force)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      try
      {
        Guid id = new Guid(contentIdString);
        TContentManager manager = this.GetManager(providerName);
        TContent itemOrDefault = manager.GetItemOrDefault(typeof (TContent), id) as TContent;
        Guid currentUserId = SecurityManager.CurrentUserId;
        bool flag = force && ClaimsManager.IsUnrestricted();
        if ((object) itemOrDefault == null || !itemOrDefault.SupportsContentLifecycle)
          return false;
        if (manager is ILifecycleManager lifecycleManager)
        {
          bool suppressSecurityChecks = false;
          if (((object) itemOrDefault as ISecuredObject).IsSecurityActionTypeGranted(SecurityActionTypes.Unlock))
            suppressSecurityChecks = true;
          ILifecycleDataItem master = lifecycleManager.Lifecycle.GetMaster((ILifecycleDataItem) (object) itemOrDefault);
          using (new ManagerSettingsRegion((IManager) lifecycleManager).SuppressSecurityChecks(suppressSecurityChecks))
            lifecycleManager.Lifecycle.DiscardTemp(master);
          this.SaveChanges((IManager) manager);
          return true;
        }
        TContent temp = manager.GetTemp(itemOrDefault);
        if ((object) temp == null)
          return true;
        if (!(temp.Owner == currentUserId | flag))
          return false;
        temp.Owner = Guid.Empty;
        this.SaveChanges((IManager) manager);
        return true;
      }
      catch (Exception ex)
      {
        if (force)
          throw ex;
        return false;
      }
    }

    protected virtual ContentItemContext<TContent> GetContentInternal(
      string contentid,
      string providerName,
      string version,
      bool published,
      bool checkOut,
      bool duplicate)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      SystemManager.HttpContextItems.Add((object) "sfContentFilters", (object) new string[1]
      {
        "DraftLinksParser"
      });
      Guid id = new Guid(contentid);
      TContentManager manager1 = this.GetManager(providerName);
      manager1.Provider.FetchAllLanguagesData();
      TContent contentItem = this.GetContentItem(id, providerName);
      ContentItemContext<TContent> itemContext;
      if (checkOut && contentItem.SupportsContentLifecycle)
      {
        this.OnBeforeCheckOutItem(contentItem);
        TContent temp = manager1.GetTemp(contentItem);
        TContent live = manager1.GetLive(contentItem);
        TContent master = manager1.GetMaster(contentItem);
        CultureInfo sitefinityCulture = ((CultureInfo) null).GetSitefinityCulture();
        int languageVersion1 = live.GetLanguageVersion(sitefinityCulture);
        int languageVersion2 = master.GetLanguageVersion(sitefinityCulture);
        TContent lockedTemp;
        TContent content1 = this.DeleteTempInCulture(temp, manager1, sitefinityCulture, out lockedTemp);
        if ((object) lockedTemp != null)
          return WcfContentLifecycleStatusFactory.CreateItemContext<TContent>(content1, (IContentLifecycleManager<TContent>) manager1, sitefinityCulture);
        if ((object) live != null && live.IsPublishedInCulture(sitefinityCulture))
        {
          if ((object) content1 == null || content1.Owner == Guid.Empty)
            content1 = manager1.CheckOut(master);
          if (languageVersion1 == languageVersion2)
          {
            this.SaveChanges((IManager) manager1);
            content1.UIStatus = live.ContentState == "SCHEDULED" ? ContentUIStatus.Scheduled : ContentUIStatus.Published;
          }
          else
          {
            if (languageVersion1 >= languageVersion2)
              throw new InvalidOperationException(Res.Get<ContentLifecycleMessages>().LiveVersionGreaterThanMasterVersion);
            this.SaveChanges((IManager) manager1);
            content1.UIStatus = ContentUIStatus.Draft;
          }
          itemContext = WcfContentLifecycleStatusFactory.CreateItemContext<TContent>(content1, (IContentLifecycleManager<TContent>) manager1, sitefinityCulture);
        }
        else if ((object) content1 != null && content1.Owner != Guid.Empty)
        {
          SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
          if (currentIdentity != null && (content1.Owner == currentIdentity.UserId || currentIdentity.IsUnrestricted))
          {
            content1.UIStatus = ContentUIStatus.Draft;
            itemContext = WcfContentLifecycleStatusFactory.CreateItemContext<TContent>(content1, (IContentLifecycleManager<TContent>) manager1, sitefinityCulture);
          }
          else
            throw new InvalidOperationException(Res.Get<ContentLifecycleMessages>().AlreadyLockedBy.Arrange((object) SecurityManager.GetFormattedUserName(content1.Owner)));
        }
        else
        {
          TContent content2 = manager1.CheckOut(master);
          this.SaveChanges((IManager) manager1);
          content2.UIStatus = ContentUIStatus.Draft;
          itemContext = WcfContentLifecycleStatusFactory.CreateItemContext<TContent>(content2, (IContentLifecycleManager<TContent>) manager1, sitefinityCulture);
        }
      }
      else if (contentItem.Status == ContentLifecycleStatus.Master)
      {
        itemContext = WcfContentLifecycleStatusFactory.CreateItemContext<TContent>(contentItem, (IContentLifecycleManager<TContent>) manager1);
      }
      else
      {
        if (!(contentItem.Status == ContentLifecycleStatus.Live & published))
          throw new InvalidOperationException(Res.Get<ContentLifecycleMessages>().MasterExpected);
        itemContext = WcfContentLifecycleStatusFactory.CreateItemContext<TContent>(contentItem, (IContentLifecycleManager<TContent>) manager1);
      }
      if (version != null)
      {
        VersionManager manager2 = VersionManager.GetManager();
        Guid guid = new Guid(version);
        manager2.GetSpecificVersionByChangeId((object) itemContext.Item, guid);
        itemContext.Item.Status = ContentLifecycleStatus.Temp;
        Change change = manager2.GetItem(typeof (Change), guid) as Change;
        itemContext.VersionInfo = new WcfChange(change);
        Change nextChange = manager2.GetNextChange(change);
        if (nextChange != null)
          itemContext.VersionInfo.NextId = nextChange.Id.ToString();
        Change previousChange = manager2.GetPreviousChange(change);
        if (previousChange != null)
          itemContext.VersionInfo.PreviousId = previousChange.Id.ToString();
      }
      this.LoadContextInformation(itemContext, manager1);
      ServiceUtility.DisableCache();
      if (duplicate)
        ServiceUtility.SetUpAsDuplicate<TContent>(itemContext);
      if (contentItem.GetType().IsAssignableFrom(typeof (ContentItem)))
      {
        bool flag = ((object) contentItem as ContentItem).IsGranted("General", "Modify");
        itemContext.SfAdditionalInfo["PermissionsEdit"] = (object) flag;
      }
      return itemContext;
    }

    /// <summary>
    /// Loads additional information in the result content item context based on the item in it.
    /// </summary>
    /// <param name="result">The result.</param>
    /// <param name="manager">The manager.</param>
    private void LoadContextInformation(
      ContentItemContext<TContent> result,
      TContentManager manager)
    {
      this.CalculateAdditionalUrlNames(result);
      this.LoadAdditionalInfo(manager, result);
    }

    protected virtual ContentItemContext<TContent> GetLiveContentInternal(
      string contentId,
      string providerName)
    {
      ServiceUtility.RequestAuthentication();
      TContent itemOrDefault = this.GetManager(providerName).GetItemOrDefault(typeof (TContent), new Guid(contentId)) as TContent;
      ContentItemContext<TContent> liveContentInternal = new ContentItemContext<TContent>();
      liveContentInternal.Item = itemOrDefault;
      return liveContentInternal;
    }

    protected virtual void OnBeforeCheckOutItem(TContent item)
    {
    }

    protected virtual void CalculateAdditionalUrlNames(ContentItemContext<TContent> context)
    {
      if (!(context.Item is ILocatable locatable))
        return;
      List<string> source = new List<string>();
      IAppSettings appSettings = SystemManager.CurrentContext.AppSettings;
      CultureInfo culture = SystemManager.CurrentContext.Culture;
      int cultureLcid = Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.GetCultureLcid(culture);
      foreach (UrlData url in locatable.Urls)
      {
        bool flag = !appSettings.Multilingual || url.Culture == cultureLcid || url.Culture == CultureInfo.InvariantCulture.LCID && culture.Equals((object) appSettings.DefaultFrontendLanguage);
        if (!url.IsDefault & flag)
        {
          source.Add(url.Url);
          context.AdditionalUrlsRedirectToDefault = url.RedirectToDefault;
        }
        else if (url.IsDefault && (!appSettings.Multilingual || url.Culture == cultureLcid))
          context.DefaultUrl = url.Url;
      }
      context.AllowMultipleUrls = source.Any<string>();
      context.AdditionalUrlNames = source.Distinct<string>().ToArray<string>();
    }

    protected virtual ContentItemContext<TContent> GetChildContentInternal(
      string parentId,
      string contentId,
      string providerName,
      string version,
      bool published,
      bool checkOut,
      bool duplicate)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      SystemManager.HttpContextItems.Add((object) "sfContentFilters", (object) new string[1]
      {
        "DraftLinksParser"
      });
      try
      {
        this.GetParentContentItem(new Guid(parentId), providerName);
      }
      catch (ItemNotFoundException ex)
      {
        throw new InvalidOperationException(Res.Get<ErrorMessages>().ItemNotFoundFormated.Arrange((object) typeof (TParentContent).GetTypeUISingleName()));
      }
      catch (KeyNotFoundException ex)
      {
        throw new InvalidOperationException(Res.Get<ErrorMessages>().ItemNotFoundFormated.Arrange((object) typeof (TParentContent).GetTypeUISingleName()));
      }
      TContent contentItem = this.GetContentItem(new Guid(contentId), providerName);
      TContentManager manager1 = this.GetManager(providerName);
      ContentItemContext<TContent> childContentInternal;
      if (checkOut && contentItem.SupportsContentLifecycle)
      {
        this.OnBeforeCheckOutItem(contentItem);
        TContent temp = manager1.GetTemp(contentItem);
        TContent live = manager1.GetLive(contentItem);
        TContent master = manager1.GetMaster(contentItem);
        CultureInfo culture = (CultureInfo) null;
        CultureInfo sitefinityCulture = culture.GetSitefinityCulture();
        int languageVersion1 = live.GetLanguageVersion(sitefinityCulture);
        int languageVersion2 = master.GetLanguageVersion(sitefinityCulture);
        TContent lockedTemp;
        TContent content1 = this.DeleteTempInCulture(temp, manager1, sitefinityCulture, out lockedTemp);
        if ((object) lockedTemp != null)
          return WcfContentLifecycleStatusFactory.CreateItemContext<TContent>(content1, (IContentLifecycleManager<TContent>) manager1, sitefinityCulture);
        if ((object) live != null && live.IsPublishedInCulture(culture))
        {
          TContent content2 = manager1.CheckOut(master);
          if (languageVersion1 == languageVersion2)
          {
            this.SaveChanges((IManager) manager1);
            content2.UIStatus = live.ContentState == "SCHEDULED" ? ContentUIStatus.Scheduled : ContentUIStatus.Published;
          }
          else
          {
            if (languageVersion1 >= languageVersion2)
              throw new InvalidOperationException(Res.Get<ContentLifecycleMessages>().LiveVersionGreaterThanMasterVersion);
            this.SaveChanges((IManager) manager1);
            content2.UIStatus = ContentUIStatus.Draft;
          }
          childContentInternal = WcfContentLifecycleStatusFactory.CreateItemContext<TContent>(content2, (IContentLifecycleManager<TContent>) manager1, sitefinityCulture);
        }
        else if ((object) content1 != null && content1.Owner != Guid.Empty)
        {
          SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
          if (currentIdentity != null && (content1.Owner == currentIdentity.UserId || currentIdentity.IsUnrestricted))
          {
            content1.UIStatus = ContentUIStatus.Draft;
            childContentInternal = WcfContentLifecycleStatusFactory.CreateItemContext<TContent>(content1, (IContentLifecycleManager<TContent>) manager1, sitefinityCulture);
          }
          else
            throw new InvalidOperationException(Res.Get<ContentLifecycleMessages>().AlreadyLockedBy.Arrange((object) SecurityManager.GetFormattedUserName(content1.Owner)));
        }
        else
        {
          TContent content3 = manager1.CheckOut(master);
          this.SaveChanges((IManager) manager1);
          content3.UIStatus = ContentUIStatus.Draft;
          childContentInternal = WcfContentLifecycleStatusFactory.CreateItemContext<TContent>(content3, (IContentLifecycleManager<TContent>) manager1, sitefinityCulture);
        }
      }
      else
        childContentInternal = contentItem.Status == ContentLifecycleStatus.Master ? WcfContentLifecycleStatusFactory.CreateItemContext<TContent>(contentItem, (IContentLifecycleManager<TContent>) manager1) : throw new InvalidOperationException(Res.Get<ContentLifecycleMessages>().MasterExpected);
      if (version != null)
      {
        VersionManager manager2 = VersionManager.GetManager();
        Guid guid = new Guid(version);
        manager2.GetSpecificVersionByChangeId((object) childContentInternal.Item, guid);
        childContentInternal.Item.Status = ContentLifecycleStatus.Temp;
        Change change = manager2.GetItem(typeof (Change), guid) as Change;
        childContentInternal.VersionInfo = new WcfChange(change);
        Change nextChange = manager2.GetNextChange(change);
        if (nextChange != null)
          childContentInternal.VersionInfo.NextId = nextChange.Id.ToString();
        Change previousChange = manager2.GetPreviousChange(change);
        if (previousChange != null)
          childContentInternal.VersionInfo.PreviousId = previousChange.Id.ToString();
        if (checkOut)
          this.SaveChanges((IManager) manager1);
      }
      this.LoadContextInformation(childContentInternal, manager1);
      ServiceUtility.DisableCache();
      if (duplicate)
        ServiceUtility.SetUpAsDuplicate<TContent>(childContentInternal);
      return childContentInternal;
    }

    protected internal virtual CollectionContext<TContentViewModel> GetContentItemsInternal(
      string sortExpression,
      int skip,
      int take,
      string filter,
      string providerName)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      string filterName;
      bool namedFilter = this.TryGetNamedFilter(typeof (TContent).IsILifecycle(), ref filter, out filterName);
      CultureInfo culture = (CultureInfo) null;
      CultureInfo sitefinityCulture = culture.GetSitefinityCulture();
      int? totalCount = new int?(0);
      object context = this.ProcessFilter(ref filter);
      ProcessFilterExtendedResult<TContent> filterExtendedResult = this.ProcessFilterExtended(sortExpression, skip, take, ref filter, providerName, ref totalCount);
      if (filterExtendedResult.PostProcessingContext != null)
        context = filterExtendedResult.PostProcessingContext;
      IQueryable<TContent> result = filterExtendedResult.CustomQuery;
      if (!filterExtendedResult.SkipRegularLogic)
      {
        Guid taxonId = Guid.Empty;
        string propertyName = string.Empty;
        if (!string.IsNullOrEmpty(filter) && this.MatchTaxonFilter(ref filter, out taxonId, out propertyName))
        {
          filter = filter.IsNullOrWhitespace() ? "Status = Master" : filter + " AND (Status = Master)";
          ContentDataProviderBase provider = this.GetManager(providerName).Provider as ContentDataProviderBase;
          if (OrganizerBase.GetProperty(typeof (TContent), propertyName) is TaxonomyPropertyDescriptor property && provider != null)
            result = (IQueryable<TContent>) provider.GetItemsByTaxon(taxonId, property.MetaField.IsSingleTaxon, property.Name, typeof (TContent), filter, sortExpression, skip, take, ref totalCount);
        }
        else
        {
          if (result == null)
          {
            result = this.GetContentItems(providerName);
            if ((filter == null || !filter.Contains("Status")) && !NamedFiltersHandler.IsLifecycleFilter(filterName))
              result = result.Where<TContent>((Expression<Func<TContent, bool>>) (c => (int) c.Status == 0));
            if (namedFilter)
              result = NamedFiltersHandler.ApplyFilter<TContent>(result, filterName, sitefinityCulture, providerName);
          }
          CommonMethods.MatchCultureFilter(ref filter, out culture);
          CultureRegion cultureRegion = (CultureRegion) null;
          if (typeof (Library).IsAssignableFrom(typeof (TContent)))
            cultureRegion = new CultureRegion(CultureInfo.InvariantCulture);
          try
          {
            filter = ContentHelper.AdaptMultilingualFilterExpressionRaw(filter, culture);
            result = DataProviderBase.SetExpressions<TContent>(result, filter, sortExpression, new int?(skip), new int?(take), ref totalCount);
          }
          finally
          {
            cultureRegion?.Dispose();
          }
        }
      }
      this.ProcessResultQuery(ref result, context);
      TContentManager manager1 = this.GetManager(providerName);
      List<TContent> contentList = this.FetchItemsList((IManager) manager1, result);
      IDictionary<Guid, TContent> liveItemsList = this.GetLiveItemsList(contentList, manager1, sitefinityCulture);
      IDictionary<Guid, TContent> tempItemsList = this.GetTempItemsList(contentList, manager1, sitefinityCulture);
      IDictionary<Guid, int> dictionary1 = (IDictionary<Guid, int>) null;
      if (manager1 is ICommentsManager commentsManager && liveItemsList != null)
      {
        IEnumerable<Guid> liveIds = liveItemsList.Values.Select<TContent, Guid>((Func<TContent, Guid>) (live => live.Id));
        dictionary1 = commentsManager.GetCommentCounts(liveIds);
      }
      IEnumerable<TContentViewModel> viewModelList = this.GetViewModelList((IEnumerable<TContent>) contentList, (ContentDataProviderBase) manager1.Provider, liveItemsList, tempItemsList);
      Dictionary<Guid, IEnumerable<StatusInfo>> dictionary2 = SystemManager.StatusProviderRegistry.GetStatuses(contentList.Select<TContent, Guid>((Func<TContent, Guid>) (i => i.Id)).ToArray<Guid>(), typeof (TContent), providerName, (string) null, sitefinityCulture, StatusBehaviour.Additional).GroupBy<StatusInfo, Guid>((Func<StatusInfo, Guid>) (i => i.Data.ItemId)).ToDictionary<IGrouping<Guid, StatusInfo>, Guid, IEnumerable<StatusInfo>>((Func<IGrouping<Guid, StatusInfo>, Guid>) (g => g.Key), (Func<IGrouping<Guid, StatusInfo>, IEnumerable<StatusInfo>>) (g => g.AsEnumerable<StatusInfo>()));
      foreach (TContentViewModel contentViewModel in viewModelList)
      {
        IEnumerable<StatusInfo> statusInfo;
        if (dictionary2.TryGetValue(contentViewModel.Id, out statusInfo))
          contentViewModel.AdditionalStatus = StatusResolver.Resolve(statusInfo);
      }
      IContentLifecycleManager<TContent> manager2 = (IContentLifecycleManager<TContent>) manager1;
      if (manager2 != null)
      {
        foreach (TContentViewModel contentViewModel in viewModelList)
        {
          TContent valueOrDefault1 = liveItemsList.GetValueOrDefault<Guid, TContent>(contentViewModel.Id);
          TContent valueOrDefault2 = tempItemsList.GetValueOrDefault<Guid, TContent>(contentViewModel.Id);
          contentViewModel.LifecycleStatus = WcfContentLifecycleStatusFactory.Create<TContent>((TContent) contentViewModel.ContentItem, manager2, valueOrDefault1, valueOrDefault2, sitefinityCulture);
          if ((object) valueOrDefault1 != null)
            contentViewModel.CommentsCount = dictionary1.GetValueOrDefault<Guid, int>(valueOrDefault1.Id);
        }
      }
      ServiceUtility.DisableCache();
      return new CollectionContext<TContentViewModel>(viewModelList)
      {
        TotalCount = totalCount.Value
      };
    }

    protected virtual object ProcessFilter(ref string filter) => (object) null;

    protected virtual ProcessFilterExtendedResult<TContent> ProcessFilterExtended(
      string sortExpression,
      int skip,
      int take,
      ref string filter,
      string providerName,
      ref int? totalCount)
    {
      return new ProcessFilterExtendedResult<TContent>()
      {
        CustomQuery = (IQueryable<TContent>) null,
        PostProcessingContext = (object) null,
        SkipRegularLogic = false
      };
    }

    protected virtual void ProcessResultQuery(ref IQueryable<TContent> result, object context)
    {
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

    protected CollectionContext<TContentViewModel> GetChildrenContentItemsInternal(
      string parentId,
      string providerName,
      string sortExpression,
      string filter,
      int skip,
      int take,
      bool includeSubFoldersItems)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      string filterName;
      bool namedFilter = this.TryGetNamedFilter(typeof (TContent).IsILifecycle(), ref filter, out filterName);
      CultureInfo culture = (CultureInfo) null;
      CultureInfo sitefinityCulture = culture.GetSitefinityCulture();
      int? totalCount = new int?(0);
      Guid parentId1 = new Guid(parentId);
      IQueryable<TContent> items = (IQueryable<TContent>) null;
      Guid taxonId = Guid.Empty;
      string propertyName = string.Empty;
      if (!string.IsNullOrEmpty(filter) && this.MatchTaxonFilter(ref filter, out taxonId, out propertyName))
      {
        filter = filter.IsNullOrWhitespace() ? "Status = Master" : filter + " AND (Status = Master)";
        ContentDataProviderBase provider = this.GetManager(providerName).Provider as ContentDataProviderBase;
        if (OrganizerBase.GetProperty(typeof (TContent), propertyName) is TaxonomyPropertyDescriptor property && provider != null)
          items = (IQueryable<TContent>) provider.GetItemsByTaxon(taxonId, property.MetaField.IsSingleTaxon, property.Name, typeof (TContent), filter, sortExpression, skip, take, ref totalCount);
      }
      else
      {
        CommonMethods.MatchCultureFilter(ref filter, out culture);
        IQueryable<TContent> queryable = !includeSubFoldersItems ? this.GetChildContentItems(parentId1, providerName) : this.GetDescendants(parentId1, providerName, filter);
        if (filter == null || !filter.Contains("Status"))
          queryable = queryable.Where<TContent>((Expression<Func<TContent, bool>>) (c => (int) c.Status == 0));
        if (namedFilter)
          queryable = NamedFiltersHandler.ApplyFilter<TContent>(queryable, filterName, sitefinityCulture, (string) null);
        filter = ContentHelper.AdaptMultilingualFilterExpressionRaw(filter, culture);
        items = DataProviderBase.SetExpressions<TContent>(queryable, !includeSubFoldersItems ? filter : string.Empty, sortExpression, new int?(skip), new int?(take), ref totalCount);
      }
      TContentManager manager1 = this.GetManager(providerName);
      List<TContent> contentList = this.FetchItemsList((IManager) manager1, items);
      IDictionary<Guid, TContent> liveItemsList = this.GetLiveItemsList(contentList, manager1, sitefinityCulture);
      IDictionary<Guid, TContent> tempItemsList = this.GetTempItemsList(contentList, manager1, sitefinityCulture);
      IEnumerable<TContentViewModel> viewModelList = this.GetViewModelList((IEnumerable<TContent>) contentList, (ContentDataProviderBase) manager1.Provider, liveItemsList, tempItemsList);
      Dictionary<Guid, Guid> dictionary1 = viewModelList.ToDictionary<TContentViewModel, Guid, Guid>((Func<TContentViewModel, Guid>) (item => item.Id), (Func<TContentViewModel, Guid>) (item => item.LiveContentItem == null ? item.ContentItem.Id : item.LiveContentItem.Id));
      IDictionary<Guid, int> dictionary2 = (IDictionary<Guid, int>) null;
      if (manager1 is ICommentsManager commentsManager)
      {
        Dictionary<Guid, Guid>.ValueCollection values = dictionary1.Values;
        dictionary2 = commentsManager.GetCommentCounts((IEnumerable<Guid>) values);
      }
      IContentLifecycleManager<TContent> manager2 = (IContentLifecycleManager<TContent>) manager1;
      if (manager2 != null)
      {
        foreach (TContentViewModel contentViewModel in viewModelList)
        {
          TContent valueOrDefault1 = liveItemsList.GetValueOrDefault<Guid, TContent>(contentViewModel.Id);
          TContent valueOrDefault2 = tempItemsList.GetValueOrDefault<Guid, TContent>(contentViewModel.Id);
          contentViewModel.LifecycleStatus = WcfContentLifecycleStatusFactory.Create<TContent>((TContent) contentViewModel.ContentItem, manager2, valueOrDefault1, valueOrDefault2, sitefinityCulture);
          Guid valueOrDefault3 = dictionary1.GetValueOrDefault<Guid, Guid>(contentViewModel.Id);
          if (valueOrDefault3 != new Guid())
            contentViewModel.CommentsCount = dictionary2.GetValueOrDefault<Guid, int>(valueOrDefault3);
        }
      }
      ServiceUtility.DisableCache();
      return new CollectionContext<TContentViewModel>(viewModelList)
      {
        TotalCount = totalCount.Value
      };
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
      string providerName,
      string version,
      string deletedLanguage,
      bool checkRelatingData)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      TContent cnt = this.GetContentItem(new Guid(contentId), providerName);
      RelatedDataHelper.CheckRelatingData(checkRelatingData, new Guid(contentId), cnt.GetType().FullName);
      if (version != null)
      {
        this.DeleteVersion(version);
        return true;
      }
      CultureInfo cultureInfo = (CultureInfo) null;
      if (!string.IsNullOrEmpty(deletedLanguage))
        cultureInfo = new CultureInfo(deletedLanguage);
      TContentManager manager = this.GetManager(providerName);
      if (cnt.SupportsContentLifecycle)
        cnt = manager.GetMaster(cnt);
      ISupportRecyclingManager recyclingManager = this.GetDataItemRecyclingManager((IManager) manager);
      if (recyclingManager != null && recyclingManager.RecycleBin.TryMoveToRecycleBin((IDataItem) cnt, recyclingManager, cultureInfo))
      {
        recyclingManager.SaveChanges();
        return true;
      }
      manager.DeleteItem((object) cnt, cultureInfo);
      this.SaveChanges((IManager) manager);
      return true;
    }

    protected ISupportRecyclingManager GetDataItemRecyclingManager(
      IManager manager)
    {
      return manager as ISupportRecyclingManager;
    }

    /// <summary>
    /// Setups the item publishing pipes according to the settings in the context.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns>true if item publishing was created</returns>
    protected virtual bool SetupItemPublishing(ContentItemContext<TContent> context) => false;

    private bool BatchDeleteContentInternal(
      string[] ids,
      string providerName,
      string deletedLanguage,
      bool checkRelatingData)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      string operationName = "Delete";
      return this.CallWorkflowBatch(ids, providerName, operationName, deletedLanguage, checkRelatingData);
    }

    private bool DeleteChildContentInternal(
      string contentId,
      string parentId,
      string providerName,
      string version,
      bool checkRelatingData)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      TContent contentItem = this.GetContentItem(new Guid(contentId), providerName);
      RelatedDataHelper.CheckRelatingData(checkRelatingData, new Guid(contentId), contentItem.GetType().FullName);
      if (version != null)
      {
        this.DeleteVersion(version);
        return true;
      }
      TContentManager manager = this.GetManager(providerName);
      TContent master = manager.GetMaster(contentItem);
      manager.DeleteItem((object) master);
      this.SaveChanges((IManager) manager);
      return true;
    }

    /// <summary>Deletes an item version from history.</summary>
    /// <param name="version">The version.</param>
    protected void DeleteVersion(string version)
    {
      VersionManager manager = VersionManager.GetManager();
      manager.DeleteChange(new Guid(version));
      manager.SaveChanges();
    }

    private TContent DeleteTempInCulture(
      TContent temp,
      TContentManager manager,
      CultureInfo currentCulture,
      out TContent lockedTemp)
    {
      lockedTemp = default (TContent);
      if ((object) temp != null)
      {
        if (temp.Owner == Guid.Empty)
          return default (TContent);
        SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
        if (currentIdentity != null && currentIdentity.UserId == temp.Owner)
        {
          if (temp is ILifecycleDataItem lifecycleDataItem && lifecycleDataItem.GetLanguageData(currentCulture) == null)
          {
            manager.DeleteItem((object) temp);
            this.SaveChanges((IManager) manager);
            temp = default (TContent);
          }
        }
        else
          lockedTemp = temp is ISecuredObject secObj && secObj.IsSecurityActionTypeGranted(SecurityActionTypes.Unlock) ? temp : throw new InvalidOperationException(Res.Get<ContentLifecycleMessages>().AlreadyCheckedOut);
      }
      return temp;
    }

    private void FixContentStatus(
      TContent content,
      string providerName,
      out ContentUIStatus uiStatus)
    {
      uiStatus = content.UIStatus;
      if (content.OriginalContentId == Guid.Empty)
        content.Status = ContentLifecycleStatus.Master;
      else
        content.Status = ContentLifecycleStatus.Temp;
    }

    private ContentItemContext<TContent> SaveContentInternal(
      ContentItemContext<TContent> context,
      string providerName,
      string workflowOperation)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      return this.SaveContentUsingWorkflow(context, string.Empty, providerName, workflowOperation);
    }

    private ContentItemContext<TContent> SaveChildContentInternal(
      ContentItemContext<TContent> context,
      string parentId,
      string providerName,
      string newParentId,
      string workflowOperation)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      if (!string.IsNullOrEmpty(newParentId))
        parentId = newParentId;
      return this.SaveContentUsingWorkflow(context, parentId, providerName, workflowOperation);
    }

    internal ContentItemContext<TContent> SaveContentUsingWorkflow(
      ContentItemContext<TContent> context,
      string parentId,
      string providerName,
      string workflowOperation)
    {
      TContentManager manager1 = this.GetManager(providerName);
      TContent content1 = context.Item;
      ILocatable locatable = (object) content1 as ILocatable;
      Guid guid = content1.OriginalContentId;
      if (!string.IsNullOrEmpty(parentId))
      {
        if ((object) content1 is IHasParent)
        {
          try
          {
            TParentContent parentContentItem = this.GetParentContentItem(new Guid(parentId), providerName);
            this.ChangeItemParent(content1, parentContentItem, manager1, false);
          }
          catch (ItemNotFoundException ex)
          {
            throw new InvalidOperationException(Res.Get<ErrorMessages>().ItemNotFoundFormated.Arrange((object) typeof (TParentContent).GetTypeUISingleName()));
          }
          catch (KeyNotFoundException ex)
          {
            throw new InvalidOperationException(Res.Get<ErrorMessages>().ItemNotFoundFormated.Arrange((object) typeof (TParentContent).GetTypeUISingleName()));
          }
        }
      }
      if (!context.AllowMultipleUrls && locatable != null)
        context.AdditionalUrlNames = new string[0];
      List<string> additionalUrlNames = (List<string>) null;
      if (context.AdditionalUrlNames != null)
        additionalUrlNames = ((IEnumerable<string>) context.AdditionalUrlNames).ToList<string>();
      this.SaveAdditionalInfo(manager1, content1, context.SfAdditionalInfo);
      CommonMethods.RecompileItemUrls((Telerik.Sitefinity.GenericContent.Model.Content) content1, (IManager) manager1, additionalUrlNames, context.AdditionalUrlsRedirectToDefault);
      if (!content1.SupportsContentLifecycle || !((object) content1 is IWorkflowItem))
      {
        bool copyTempRelations = true;
        if ((object) content1 is MediaContent && !((object) content1 as MediaContent).Uploaded)
          copyTempRelations = false;
        RelatedDataHelper.SaveRelatedDataChanges((IManager) manager1, (IDataItem) content1, context.ChangedRelatedData, copyTempRelations);
        context.ChangedRelatedData = (ContentLinkChange[]) null;
        Guid id = content1.Id;
        if (string.IsNullOrEmpty(manager1.TransactionName))
          manager1.SaveChanges();
        else
          TransactionManager.CommitTransaction(manager1.TransactionName);
        if (this.SetupItemPublishing(context))
        {
          try
          {
            manager1 = this.GetManager(providerName);
            content1 = this.GetContentItem(id, providerName);
          }
          catch
          {
            manager1 = this.GetManager(providerName);
            content1 = this.GetContentItem(id, providerName);
          }
        }
        ServiceUtility.DisableCache();
        ContentItemContext<TContent> contentItemContext = new ContentItemContext<TContent>();
        contentItemContext.Item = content1;
        ContentItemContext<TContent> result = contentItemContext;
        this.LoadContextInformation(result, manager1);
        return result;
      }
      if (content1.Status == ContentLifecycleStatus.Master)
      {
        guid = content1.Id;
        content1 = manager1.CheckOut(content1);
      }
      else
      {
        SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
        if (currentIdentity == null || !currentIdentity.IsUnrestricted && !(currentIdentity.UserId == content1.Owner))
          throw new InvalidOperationException(Res.Get<ContentLifecycleMessages>().CannotCheckIn);
      }
      RelatedDataHelper.SaveRelatedDataChanges((IManager) manager1, (IDataItem) content1, context.ChangedRelatedData);
      context.ChangedRelatedData = (ContentLinkChange[]) null;
      Dictionary<string, string> contextBag = new Dictionary<string, string>();
      CommonMethods.FillContextBagFromCurrentRequest((IDictionary<string, string>) contextBag);
      if (workflowOperation == "Schedule")
        CommonMethods.TryUpdateItemBeforeWorkflowScheduleOperation((IDataItem) content1, (IDictionary<string, string>) contextBag);
      manager1.SaveChanges();
      if (workflowOperation == "SaveTemp")
      {
        ServiceUtility.DisableCache();
        ContentItemContext<TContent> contentItemContext = new ContentItemContext<TContent>();
        contentItemContext.Item = content1;
        ContentItemContext<TContent> result = contentItemContext;
        this.LoadContextInformation(result, manager1);
        return result;
      }
      manager1.Dispose();
      if (string.IsNullOrEmpty(workflowOperation))
        workflowOperation = "SaveDraft";
      contextBag["ContentType"] = content1.GetType().FullName;
      WorkflowManager.MessageWorkflow(guid, content1.GetType(), providerName, workflowOperation, false, contextBag);
      if (!SystemManager.HttpContextItems.Contains((object) "sfContentFilters"))
        SystemManager.HttpContextItems.Add((object) "sfContentFilters", (object) new string[1]
        {
          "DraftLinksParser"
        });
      TContent content2 = this.GetContentItem(guid, providerName);
      TContentManager manager2 = this.GetManager(providerName);
      Guid currentUserId = ClaimsManager.GetCurrentUserId();
      TContent temp = manager2.GetTemp(content2);
      if ((object) temp != null && temp.Owner == currentUserId)
        content2 = temp;
      ContentItemContext<TContent> itemContext = WcfContentLifecycleStatusFactory.CreateItemContext<TContent>(content2, (IContentLifecycleManager<TContent>) manager1);
      this.LoadContextInformation(itemContext, manager1);
      ServiceUtility.DisableCache();
      return itemContext;
    }

    /// <summary>Saves the additional info.</summary>
    /// <param name="manager">The manager.</param>
    /// <param name="item">The item.</param>
    /// <param name="additionalInfo">The additional information for this item that has to be persisted.</param>
    protected virtual void SaveAdditionalInfo(
      TContentManager manager,
      TContent item,
      Dictionary<string, object> additionalInfo)
    {
    }

    /// <summary>
    /// Loads the additional info in the result ContentItemContext property using the Item.
    /// </summary>
    /// <param name="manager">The manager that will be used to load the additional information in the result item.</param>
    /// <param name="result">The result.</param>
    protected virtual void LoadAdditionalInfo(
      TContentManager manager,
      ContentItemContext<TContent> result)
    {
    }

    private bool BatchChangeParentInternal(string[] ids, string newParentId, string providerName)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
      if (currentHttpContext != null)
        currentHttpContext.Server.ScriptTimeout = 600;
      Guid guid = new Guid(newParentId);
      TContentManager manager = this.GetManager(providerName);
      if (manager is IFolderManager manager2)
      {
        this.BatchChangeParentFoldersInternal(manager2, ids, guid);
      }
      else
      {
        ILifecycleManager manager1 = (object) manager as ILifecycleManager;
        TParentContent parentContentItem = this.GetParentContentItem(guid, providerName);
        foreach (Guid id in ((IEnumerable<string>) ids).Select<string, Guid>((Func<string, Guid>) (id => new Guid(id))).ToList<Guid>())
        {
          TContent contentItem = this.GetContentItem(id, providerName);
          if (this.ChangeItemParent(contentItem, parentContentItem, manager, false))
          {
            if ((object) contentItem is ILocatable)
            {
              ILifecycleDataItem lifecycleDataItem = (object) contentItem as ILifecycleDataItem;
              if (manager1 != null && lifecycleDataItem != null && lifecycleDataItem.Status != ContentLifecycleStatus.Temp)
                manager1.DoForAllVersions(lifecycleDataItem, (System.Action<ILifecycleDataItem>) (li => manager.RecompileAndValidateUrls<ILocatable>((ILocatable) li)));
              else
                manager.RecompileAndValidateUrls<ILocatable>((ILocatable) (object) contentItem);
            }
            this.SaveChanges((IManager) manager);
          }
        }
      }
      return true;
    }

    private void BatchChangeParentFoldersInternal(
      IFolderManager manager,
      string[] ids,
      Guid parentId)
    {
      Guid originalParentId = parentId;
      Folder folder1 = manager.GetFolders().FirstOrDefault<Folder>((Expression<Func<Folder, bool>>) (f => f.Id == originalParentId));
      Guid[] array = ((IEnumerable<string>) ids).Select<string, Guid>((Func<string, Guid>) (id => Guid.Parse(id))).ToArray<Guid>();
      if (folder1 != null)
      {
        parentId = folder1.RootId;
        foreach (Guid guid in array)
        {
          Guid id = guid;
          Folder folder2 = manager.GetFolders().FirstOrDefault<Folder>((Expression<Func<Folder, bool>>) (f => f.Id == id));
          if (folder2 != null && folder1.Path.StartsWith(folder2.Path))
            throw new InvalidOperationException(Res.Get<ErrorMessages>().CannotChangeParentBecauseOfRecursiveRelation.Arrange((object) folder2.Title, (object) folder1.Title));
        }
      }
      List<Guid> ids1 = new List<Guid>();
      foreach (Guid guid in array)
      {
        Guid id = guid;
        Folder folder3 = manager.GetFolders().FirstOrDefault<Folder>((Expression<Func<Folder, bool>>) (f => f.Id == id));
        if (folder3 != null)
        {
          folder3.RootId = parentId;
          folder3.Parent = folder1;
          manager.ValidateFolderUrl(folder3);
          this.EnsureItemParentChangeRecursive(folder3, manager.Provider.Name, ids1);
        }
        else
          ids1.Add(id);
      }
      if (ids1.Count > 0)
      {
        TContentManager contentManager = manager as TContentManager;
        ILifecycleManager manager1 = manager as ILifecycleManager;
        TParentContent parentContentItem = this.GetParentContentItem(parentId, manager.Provider.Name);
        foreach (Guid id in ids1)
        {
          TContent contentItem = this.GetContentItem(id, manager.Provider.Name);
          if ((object) contentItem is IFolderItem)
            manager.ChangeItemFolder((IFolderItem) (object) contentItem, folder1?.Id);
          if ((object) parentContentItem == null)
            parentContentItem = this.GetParentContentItem(parentId, manager.Provider.Name);
          if (this.ChangeItemParent(contentItem, parentContentItem, contentManager, false))
          {
            if ((object) contentItem is ILocatable)
            {
              ILifecycleDataItem lifecycleDataItem = (object) contentItem as ILifecycleDataItem;
              if (manager1 != null && lifecycleDataItem != null && lifecycleDataItem.Status != ContentLifecycleStatus.Temp)
                manager1.DoForAllVersions(lifecycleDataItem, (System.Action<ILifecycleDataItem>) (li => contentManager.RecompileAndValidateUrls<ILocatable>((ILocatable) li)));
              else
                contentManager.RecompileAndValidateUrls<ILocatable>((ILocatable) (object) contentItem);
            }
            if ((object) contentItem is MediaContent)
            {
              LibrariesManager manager2 = new LibrariesManager();
              MediaContent mediaContent = (object) contentItem as MediaContent;
              BlobStorageManager blobStorageManager = manager2.Provider.GetBlobStorageManager(mediaContent);
              if (blobStorageManager.Provider is IExternalBlobStorageProvider provider)
              {
                if (mediaContent.Status == ContentLifecycleStatus.Master)
                  mediaContent = manager2.Lifecycle.GetLive((ILifecycleDataItem) mediaContent) as MediaContent;
                if (mediaContent == null)
                  mediaContent = (object) contentItem as MediaContent;
                LibraryTasksUtilities.UpdateExternalLibraryItemLocation(manager2, provider, blobStorageManager.Provider, mediaContent);
              }
            }
          }
        }
      }
      manager.SaveChanges();
    }

    private void EnsureItemParentChangeRecursive(
      Folder parentFolder,
      string providerName,
      List<Guid> ids)
    {
      ids.AddRange((IEnumerable<Guid>) Queryable.OfType<TContent>(Queryable.OfType<IFolderItem>(this.GetContentItems(providerName)).Where<IFolderItem>((Expression<Func<IFolderItem, bool>>) (c => c.FolderId == (Guid?) parentFolder.Id))).Select<TContent, Guid>((Expression<Func<TContent, Guid>>) (c => c.Id)));
      foreach (Folder child in (IEnumerable<Folder>) parentFolder.Children)
        this.EnsureItemParentChangeRecursive(child, providerName, ids);
    }

    private void BatchReorderContentInternal(
      Dictionary<string, float> contentIdnewOrdinal,
      string providerName)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      TContentManager manager = this.GetManager(providerName);
      foreach (KeyValuePair<string, float> keyValuePair in contentIdnewOrdinal)
      {
        Guid id = new Guid(keyValuePair.Key);
        TContent contentItem = this.GetContentItem(id, providerName);
        manager.Provider.ReorderItem(typeof (TContent), id, keyValuePair.Value);
        TContent content = default (TContent);
        if (contentItem.Status == ContentLifecycleStatus.Master)
          content = manager.GetLive(contentItem);
        else if (contentItem.Status == ContentLifecycleStatus.Live)
          content = manager.GetMaster(contentItem);
        if ((object) content != null)
          manager.Provider.ReorderItem<TContent>(content.Id, keyValuePair.Value);
      }
      this.SaveChanges((IManager) manager);
    }

    private void ReorderContentInternal(
      string contentId,
      string providerName,
      float oldPosition,
      float newPosition)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      TContentManager manager = this.GetManager(providerName);
      manager.Provider.ReorderItem(typeof (TContent), new Guid(contentId), oldPosition, newPosition);
      this.SaveChanges((IManager) manager);
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

    internal bool SkipProtect
    {
      get => this.skipProtect;
      set => this.skipProtect = value;
    }

    protected virtual bool ChangeItemParent(
      TContent content,
      TParentContent parent,
      TContentManager manager,
      bool recompileUrls)
    {
      if (manager is IContentManagerExtended contentManagerExtended)
      {
        contentManagerExtended.ChangeItemParent((Telerik.Sitefinity.GenericContent.Model.Content) content, (Telerik.Sitefinity.GenericContent.Model.Content) parent, recompileUrls);
      }
      else
      {
        ((IHasParent) (object) content).Parent = (Telerik.Sitefinity.GenericContent.Model.Content) parent;
        if (recompileUrls)
          CommonMethods.RecompileItemUrls((Telerik.Sitefinity.GenericContent.Model.Content) content, (IManager) manager);
      }
      return true;
    }

    protected virtual CollectionContext<TContentViewModel> BatchPlaceContentInternal(
      string[] sourceContentIds,
      string parentId,
      string providerName,
      string placePosition,
      string destination)
    {
      return (CollectionContext<TContentViewModel>) null;
    }

    protected virtual void BatchMoveContentInternal(
      string[] sourceContentIds,
      string parentId,
      string providerName,
      string direction)
    {
    }

    /// <summary>Gets the rating.</summary>
    /// <param name="itemTypeName">Name of the item type.</param>
    /// <param name="contentId">The content pageId.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns></returns>
    public Decimal GetRating(string contentId, string providerName)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      TContent contentItem = this.GetContentItem(new Guid(contentId), providerName);
      PropertyDescriptorCollection properties = TypeDescriptor.GetProperties((object) contentItem);
      if (properties["VotesSum"] == null || properties["VotesCount"] == null)
        return 0.0M;
      Decimal num1 = (Decimal) properties["VotesSum"].GetValue((object) contentItem);
      uint num2 = (uint) properties["VotesCount"].GetValue((object) contentItem);
      return num2 <= 0U ? 0.0M : num1 / (Decimal) num2;
    }

    /// <summary>XMLs the get rating.</summary>
    /// <param name="itemTypeName">Name of the item type.</param>
    /// <param name="contentId">The content pageId.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns></returns>
    public Decimal XmlGetRating(string contentId, string providerName) => this.GetRating(contentId, providerName);

    /// <summary>Sets the rating.</summary>
    /// <param name="value">The value.</param>
    /// <param name="itemTypeName">Name of the item type.</param>
    /// <param name="contentId">The content pageId.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns></returns>
    public RatingResult SetRating(Decimal value, string contentId, string providerName)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      Guid dataItemId = new Guid(contentId);
      TContent contentItem = this.GetContentItem(new Guid(contentId), providerName);
      if (!RatingHelper.CanUserRate(TypeSurrogateFactory.Instance.GetIdentity(contentItem.GetType(), (object) contentItem, (IManager) this.GetManager(providerName))))
        return new RatingResult(0U, -1.0M, string.Empty);
      PropertyDescriptorCollection properties = TypeDescriptor.GetProperties((object) contentItem);
      if (properties["VotesSum"] == null || properties["VotesCount"] == null)
        return new RatingResult(0U, -1.0M, string.Empty);
      if (RatingHelper.CanUserRate(dataItemId))
      {
        uint num1 = (uint) properties["VotesCount"].GetValue((object) contentItem);
        Decimal num2 = (Decimal) properties["VotesSum"].GetValue((object) contentItem);
        properties["VotesCount"].SetValue((object) contentItem, (object) (uint) ((int) num1 + 1));
        properties["VotesSum"].SetValue((object) contentItem, (object) (num2 + value));
        RatingHelper.RateWithCurrentUser(dataItemId, value);
        this.SaveChanges((IManager) this.GetManager(providerName));
      }
      uint votesCount = (uint) properties["VotesCount"].GetValue((object) contentItem);
      Decimal num = (Decimal) properties["VotesSum"].GetValue((object) contentItem);
      Decimal average = 0.0M;
      if ((Decimal) votesCount > 0.0M)
        average = num / (Decimal) votesCount;
      return new RatingResult(votesCount, average, RatingHelper.GetSubtitleMessage((Telerik.Sitefinity.GenericContent.Model.Content) contentItem));
    }

    /// <summary>Resets the rating for a particular item.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="contentId">The content pageId.</param>
    /// <param name="providerName">Name of the provider.</param>
    public RatingResult ResetRating(string contentId, string providerName)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      TContent contentItem = this.GetContentItem(new Guid(contentId), providerName);
      if ((object) contentItem == null)
        return new RatingResult(0U, 0.0M, string.Empty);
      PropertyDescriptorCollection properties = TypeDescriptor.GetProperties((object) contentItem);
      if (properties["VotesCount"] == null || properties["VotesSum"] == null)
        return new RatingResult(0U, 0.0M, string.Empty);
      properties["VotesCount"].SetValue((object) contentItem, (object) 0U);
      properties["VotesSum"].SetValue((object) contentItem, (object) 0.0M);
      this.SaveChanges((IManager) this.GetManager(providerName));
      RatingHelper.DeleteForCurrentUser(TypeSurrogateFactory.Instance.GetIdentity(contentItem.GetType(), (object) contentItem, (IManager) this.GetManager(providerName)));
      return new RatingResult(0U, 0.0M, RatingHelper.GetSubtitleMessage((Telerik.Sitefinity.GenericContent.Model.Content) contentItem));
    }

    /// <summary>Resets the rating for a particular item.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="contentId">The content pageId.</param>
    /// <param name="providerName">Name of the provider.</param>
    public RatingResult XmlResetRating(string contentId, string providerName) => this.ResetRating(contentId, providerName);

    /// <summary>XMLs the set rating.</summary>
    /// <param name="value">The value.</param>
    /// <param name="itemTypeName">Name of the item type.</param>
    /// <param name="contentId">The content pageId.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns></returns>
    public RatingResult XmlSetRating(
      Decimal value,
      string contentId,
      string providerName)
    {
      return this.SetRating(value, contentId, providerName);
    }

    /// <summary>
    /// Determines whether the current user can rate by pageId or ip
    /// </summary>
    /// <param name="contentId">Id of the item type</param>
    /// <param name="itemType">CLR type of the item to rate</param>
    /// <param name="providerName">name of the content provider to use</param>
    /// <returns></returns>
    public bool CanRate(string contentId, string providerName)
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
    public bool XmlCanRate(string contentId, string providerName) => this.CanRate(contentId, providerName);

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
    public virtual ItemContext<FolderDetailViewModel> SaveChildFolder(
      ItemContext<FolderDetailViewModel> content,
      string parentId,
      string contentId,
      string providerName)
    {
      return this.SaveChildFolderInternal(content, contentId, providerName);
    }

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
    public virtual ItemContext<FolderDetailViewModel> SaveChildFolderInXml(
      ItemContext<FolderDetailViewModel> content,
      string parentId,
      string contentId,
      string providerName)
    {
      return this.SaveChildFolderInternal(content, contentId, providerName);
    }

    /// <summary>Gets the single folder and returns it in JSON format.</summary>
    /// <param name="contentId">Id of the folder that ought to be retrieved.</param>
    /// <param name="providerName">Name of the content provider that is to be used to retrieve the folder.</param>
    /// <returns>An instance of ItemContext that contains the content item to be retrieved.</returns>
    public virtual ItemContext<FolderDetailViewModel> GetChildFolder(
      string parentId,
      string contentId,
      string providerName)
    {
      return this.GetChildFolderInternal(contentId, providerName);
    }

    /// <summary>Gets the single folder and returns it in XML format.</summary>
    /// <param name="contentId">Id of the folder that ought to be retrieved.</param>
    /// <param name="providerName">Name of the content provider that is to be used to retrieve the folder.</param>
    /// <returns>
    /// An instance of ItemContext that contains the content item to be retrieved.
    /// </returns>
    public virtual ItemContext<FolderDetailViewModel> GetChildFolderInXml(
      string parentId,
      string contentId,
      string providerName)
    {
      return this.GetChildFolderInternal(contentId, providerName);
    }

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
    public virtual CollectionContext<FolderViewModel> GetFolders(
      string sortExpression,
      int skip,
      int take,
      string filter,
      string providerName,
      bool hierarchyMode)
    {
      return new CollectionContext<FolderViewModel>();
    }

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
    public virtual CollectionContext<FolderViewModel> GetFoldersInXml(
      string sortExpression,
      int skip,
      int take,
      string filter,
      string providerName,
      bool hierarchyMode)
    {
      return new CollectionContext<FolderViewModel>();
    }

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
    public virtual CollectionContext<FolderViewModel> GetSubFolders(
      string folderId,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string providerName,
      bool hierarchyMode)
    {
      return new CollectionContext<FolderViewModel>();
    }

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
    public virtual CollectionContext<FolderViewModel> GetSubFoldersInXml(
      string folderId,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string providerName,
      bool hierarchyMode)
    {
      return new CollectionContext<FolderViewModel>();
    }

    /// <summary>
    /// Gets the collection of predecessor folders objects and returns them in JSON format.
    /// </summary>
    /// <param name="folderId">The parent folder ID</param>
    /// <param name="providerName">Name of the content provider to be used to get the content items.</param>
    /// <param name="sortExpression">Sort expression used to order the content items in the result set.</param>
    /// <returns>An instance of CollectionContext object that contains the collection of FolderViewModel items.</returns>
    public virtual CollectionContext<FolderViewModel> GetPredecessorFolders(
      string folderId,
      string providerName,
      string sortExpression,
      bool excludeNeighbours = false)
    {
      return this.GetPredecessorFoldersInternal(folderId, providerName, sortExpression, excludeNeighbours);
    }

    /// <summary>
    /// Gets the collection of predecessor folders objects and returns them in XML format.
    /// </summary>
    /// <param name="folderId">The parent folder ID</param>
    /// <param name="providerName">Name of the content provider to be used to get the content items.</param>
    /// <param name="sortExpression">Sort expression used to order the content items in the result set.</param>
    /// <returns>An instance of CollectionContext object that contains the collection of FolderViewModel items.</returns>
    public CollectionContext<FolderViewModel> GetPredecessorFoldersInXml(
      string folderId,
      string providerName,
      string sortExpression,
      bool excludeNeighbours = false)
    {
      return this.GetPredecessorFoldersInternal(folderId, providerName, sortExpression, excludeNeighbours);
    }

    /// <summary>Gets folders as tree.</summary>
    /// <param name="provider">name of the provider</param>
    /// <param name="sortExpression">sort expresion</param>
    /// <param name="skip">number of items to skip</param>
    /// <param name="take">number of items to take</param>
    /// <param name="filter">filter to apply</param>
    /// <param name="hierarchyMode">Checks for the hierarchical content</param>
    /// <returns>An instance of CollectionContext object that contains the collection of FolderViewModel items</returns>
    public CollectionContext<FolderViewModel> GetFoldersAsTree(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      bool hierarchyMode)
    {
      return this.GetFoldersAsTreeInternal(provider, sortExpression, skip, take, filter, hierarchyMode);
    }

    /// <summary>Gets folders as tree in XML.</summary>
    /// <param name="provider">name of the provider</param>
    /// <param name="sortExpression">sort expresion</param>
    /// <param name="skip">number of items to skip</param>
    /// <param name="take">number of items to take</param>
    /// <param name="filter">filter to apply</param>
    /// <param name="hierarchyMode">Checks for the hierarchical content</param>
    /// <returns>An instance of CollectionContext object that contains the collection of FolderViewModel items</returns>
    public CollectionContext<FolderViewModel> GetFoldersAsTreeInXml(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      bool hierarchyMode)
    {
      return this.GetFoldersAsTreeInternal(provider, sortExpression, skip, take, filter, hierarchyMode);
    }

    /// <summary>Sets the image as cover of its parent.</summary>
    /// <param name="contentId">The Id of the image.</param>
    /// <param name="providerName">The name of the libraries provider.</param>
    public virtual void SetAsCover(string contentId, string providerName) => throw new NotImplementedException();

    /// <summary>Gets the file links collection for this item</summary>
    /// <param name="contentId">The Id of the content.</param>
    /// <param name="providerName">The name of the libraries provider.</param>
    /// <param name="culture">The name of the culture.</param>
    /// <returns>The file links collection for this item</returns>
    public virtual CollectionContext<MediaFileLinkViewModel> GetMediaFileLinks(
      string contentId,
      string providerName,
      string culture)
    {
      throw new NotImplementedException();
    }

    public virtual void CopyFileLink(string contentId, string providerName, string fileId) => throw new NotImplementedException();

    /// <summary>Gets the folder path.</summary>
    /// <param name="folder">The folder.</param>
    /// <param name="manager">The manager.</param>
    /// <returns></returns>
    internal virtual string GetFolderPath(Folder folder, IFolderManager manager) => manager.GetFolderTitlePath<TContent>(folder);

    internal void SetFolders(
      IFolderManager foldersManger,
      List<FolderViewModel> hierarchicalFolders,
      IQueryable<Folder> folders,
      string providerName,
      string baseUrl,
      bool hierarchyMode = true)
    {
      foreach (Folder folder1 in (IEnumerable<Folder>) folders)
      {
        Folder folder = folder1;
        int num;
        if (hierarchyMode)
          num = foldersManger.GetFolders().Any<Folder>((Expression<Func<Folder, bool>>) (f => f.ParentId == (Guid?) folder.Id)) ? 1 : 0;
        else
          num = 0;
        bool flag = num != 0;
        string str = string.Empty;
        if (!string.IsNullOrEmpty(baseUrl))
          str = string.IsNullOrEmpty(providerName) ? string.Format("{0}/?folderId={1}", (object) baseUrl, (object) folder.Id) : string.Format("{0}/?provider={1}&folderId={2}", (object) baseUrl, (object) providerName, (object) folder.Id);
        hierarchicalFolders.Add(new FolderViewModel()
        {
          Title = (string) folder.Title,
          Id = folder.Id,
          RootId = folder.RootId,
          ParentId = !folder.ParentId.HasValue ? new Guid?(folder.RootId) : folder.ParentId,
          HasChildren = flag,
          Url = str,
          Path = foldersManger.GetFolderTitlePath<TContent>(folder)
        });
      }
    }

    internal void SetHasChildren(
      IEnumerable<FolderViewModel> hierarchicalFolders,
      IQueryable<Folder> folders)
    {
      Guid[] ids = hierarchicalFolders.Select<FolderViewModel, Guid>((Func<FolderViewModel, Guid>) (f => f.Id)).ToArray<Guid>();
      folders = folders.Where<Folder>((Expression<Func<Folder, bool>>) (f => f.ParentId == new Guid?() && ids.Contains<Guid>(f.RootId) || f.ParentId != new Guid?() && ids.Contains<Guid>(f.ParentId.Value)));
      IQueryable<IGrouping<Guid?, Folder>> source = folders.GroupBy<Folder, Guid?>((Expression<Func<Folder, Guid?>>) (folder => folder.ParentId != new Guid?() ? folder.ParentId : (Guid?) folder.RootId)).Where<IGrouping<Guid?, Folder>>((Expression<Func<IGrouping<Guid?, Folder>, bool>>) (childFoldersGroup => childFoldersGroup.Count<Folder>() > 0));
      Expression<Func<IGrouping<Guid?, Folder>, Guid?>> selector = (Expression<Func<IGrouping<Guid?, Folder>, Guid?>>) (childFoldersGroup => childFoldersGroup.Key);
      foreach (Guid? nullable in (IEnumerable<Guid?>) source.Select<IGrouping<Guid?, Folder>, Guid?>(selector))
      {
        Guid? folderId = nullable;
        hierarchicalFolders.FirstOrDefault<FolderViewModel>((Func<FolderViewModel, bool>) (f => f.Id == folderId.Value)).HasChildren = true;
      }
    }

    internal void SetChildFoldersCount(
      IEnumerable<LibraryViewModel> contentLibraryViewModelList,
      IQueryable<Folder> folders)
    {
      Guid[] array1 = contentLibraryViewModelList.Select<LibraryViewModel, Guid>((Func<LibraryViewModel, Guid>) (f => f.Id)).ToArray<Guid>();
      List<FolderChildrenCount> source;
      if (array1.Length <= 200)
      {
        source = ContentServiceBase<TContent, TContent, TContentViewModel, TContentViewModel, TContentManager>.GetFoldersChildrenCount(array1, folders);
      }
      else
      {
        List<FolderChildrenCount> folderChildrenCountList = new List<FolderChildrenCount>(array1.Length);
        int num = (array1.Length + 200 - 1) / 200;
        for (int index = 0; index < num; ++index)
        {
          Guid[] array2 = ((IEnumerable<Guid>) array1).Skip<Guid>(index * 200).Take<Guid>(200).ToArray<Guid>();
          folderChildrenCountList.AddRange((IEnumerable<FolderChildrenCount>) ContentServiceBase<TContent, TContent, TContentViewModel, TContentViewModel, TContentManager>.GetFoldersChildrenCount(array2, folders));
        }
        source = folderChildrenCountList;
      }
      foreach (LibraryViewModel libraryViewModel in contentLibraryViewModelList)
      {
        LibraryViewModel viewModel = libraryViewModel;
        FolderChildrenCount folderChildrenCount = source.FirstOrDefault<FolderChildrenCount>((Func<FolderChildrenCount, bool>) (a => a.Id == viewModel.Id));
        if (folderChildrenCount != null)
          LibraryViewModel.SetLibrariesCount(viewModel, folderChildrenCount.Count);
      }
    }

    internal virtual void SetChildItemsInformation<TContent, TParentContent, TParentContentViewModel>(
      IEnumerable<TParentContentViewModel> hierarchicalViewModelList,
      IQueryable<TContent> contentItems)
      where TContent : Telerik.Sitefinity.GenericContent.Model.Content, IHasParent
      where TParentContent : Telerik.Sitefinity.GenericContent.Model.Content
      where TParentContentViewModel : ContentViewModelBase
    {
      Guid[] array1 = hierarchicalViewModelList.Select<TParentContentViewModel, Guid>((Func<TParentContentViewModel, Guid>) (f => f.Id)).ToArray<Guid>();
      IEnumerable<LibraryViewModel> libraryViewModels = hierarchicalViewModelList.OfType<LibraryViewModel>();
      IQueryable<TContent> childItems = contentItems.Where<TContent>((Expression<Func<TContent, bool>>) (i => (int) i.Status == 0));
      List<FolderChildrenDateCreated> source;
      if (array1.Length <= 200)
      {
        source = ContentServiceBase<TContent, TParentContent, TContentViewModel, TParentContentViewModel, TContentManager>.GetChildItemsInformation<TContent>(array1, childItems);
      }
      else
      {
        List<FolderChildrenDateCreated> childrenDateCreatedList = new List<FolderChildrenDateCreated>(array1.Length);
        int num = (array1.Length + 200 - 1) / 200;
        for (int index = 0; index < num; ++index)
        {
          Guid[] array2 = ((IEnumerable<Guid>) array1).Skip<Guid>(index * 200).Take<Guid>(200).ToArray<Guid>();
          childrenDateCreatedList.AddRange((IEnumerable<FolderChildrenDateCreated>) ContentServiceBase<TContent, TParentContent, TContentViewModel, TParentContentViewModel, TContentManager>.GetChildItemsInformation<TContent>(array2, childItems));
        }
        source = childrenDateCreatedList;
      }
      foreach (LibraryViewModel libraryViewModel in libraryViewModels)
      {
        LibraryViewModel viewModel = libraryViewModel;
        FolderChildrenDateCreated childrenDateCreated = source.FirstOrDefault<FolderChildrenDateCreated>((Func<FolderChildrenDateCreated, bool>) (f => f.Id == viewModel.Id));
        if (childrenDateCreated != null)
          viewModel.LastUploadedDate = new DateTime?(childrenDateCreated.Date);
      }
    }

    private ItemContext<FolderDetailViewModel> SaveChildFolderInternal(
      ItemContext<FolderDetailViewModel> content,
      string contentId,
      string providerName)
    {
      ServiceUtility.RequestAuthentication();
      TContentManager contentManager = this.GetManager(providerName);
      ILifecycleManager manager1 = (object) contentManager as ILifecycleManager;
      IFolderManager manager2 = (IFolderManager) (object) contentManager;
      Folder folder1 = manager2.GetFolders().FirstOrDefault<Folder>((Expression<Func<Folder, bool>>) (f => f.Id == content.Item.Id)) ?? (!(content.Item.Id != Guid.Empty) ? manager2.CreateFolder() : manager2.CreateFolder(content.Item.Id));
      string persistedValue1 = content.Item.Title.PersistedValue;
      string persistedValue2 = content.Item.Description.PersistedValue;
      folder1.Title = (Lstring) persistedValue1;
      folder1.Description = (Lstring) persistedValue2;
      foreach (CultureInfo availableLanguage in folder1.Title.GetAvailableLanguages())
      {
        folder1.Title[availableLanguage] = persistedValue1;
        folder1.Description[availableLanguage] = persistedValue2;
      }
      folder1.Title = (Lstring) content.Item.Title.PersistedValue;
      folder1.Description = (Lstring) content.Item.Description.PersistedValue;
      Guid rootId = folder1.RootId;
      Folder folder2 = manager2.GetFolders().FirstOrDefault<Folder>((Expression<Func<Folder, bool>>) (f => f.Id == content.Item.ParentId));
      if (folder2 != null)
      {
        if (folder2.Path.StartsWith(folder1.Path))
          throw new InvalidOperationException(Res.Get<ErrorMessages>().CannotChangeParentBecauseOfRecursiveRelation.Arrange((object) folder1.Title, (object) folder2.Title));
        folder1.Parent = folder2;
      }
      else
      {
        folder1.Parent = (Folder) null;
        folder1.RootId = content.Item.ParentId;
      }
      manager2.ValidateFolderUrl(folder1);
      if (folder1.RootId != rootId)
      {
        List<Guid> ids = new List<Guid>();
        this.EnsureItemParentChangeRecursive(folder1, providerName, ids);
        TParentContent parentContentItem = this.GetParentContentItem(folder1.RootId, providerName);
        foreach (Guid id in ids)
        {
          if (this.ChangeItemParent(this.GetContentItem(id, providerName), parentContentItem, contentManager, true))
          {
            if (content is ILocatable)
            {
              ILifecycleDataItem lifecycleDataItem = content as ILifecycleDataItem;
              if (manager1 != null && lifecycleDataItem != null && lifecycleDataItem.Status != ContentLifecycleStatus.Temp)
                manager1.DoForAllVersions(lifecycleDataItem, (System.Action<ILifecycleDataItem>) (li => contentManager.RecompileAndValidateUrls<ILocatable>((ILocatable) li)));
              else
                contentManager.RecompileAndValidateUrls<ILocatable>((ILocatable) content);
            }
            this.SaveChanges((IManager) manager2);
          }
        }
      }
      this.SaveChanges((IManager) manager2);
      ServiceUtility.DisableCache();
      return content;
    }

    private ItemContext<FolderDetailViewModel> GetChildFolderInternal(
      string contentId,
      string providerName)
    {
      ServiceUtility.RequestAuthentication();
      FolderDetailViewModel folderDetailViewModel = new FolderDetailViewModel(((IFolderManager) (object) this.GetManager(providerName)).GetFolder(Guid.Parse(contentId)));
      ServiceUtility.DisableCache();
      return new ItemContext<FolderDetailViewModel>()
      {
        Item = folderDetailViewModel
      };
    }

    /// <summary>Gets the collection of predecessor folders objects.</summary>
    /// <param name="folderId">The parent folder ID</param>
    /// <param name="providerName">Name of the content provider to be used to get the content items.</param>
    /// <param name="sortExpression">Sort expression used to order the content items in the result set.</param>
    /// <returns>An instance of CollectionContext object that contains the collection of ContentSummary items.</returns>
    private CollectionContext<FolderViewModel> GetPredecessorFoldersInternal(
      string folderId,
      string providerName,
      string sortExpression,
      bool excludeNeighbours)
    {
      ServiceUtility.RequestAuthentication();
      int result1;
      if (!int.TryParse(SystemManager.CurrentHttpContext.Request.QueryString["skip"], out result1))
        result1 = 0;
      int result2;
      if (!int.TryParse(SystemManager.CurrentHttpContext.Request.QueryString["take"], out result2))
        result2 = 0;
      TContentManager manager1 = this.GetManager(providerName);
      IFolderManager manager2 = (IFolderManager) (object) manager1;
      LinkedList<FolderViewModel> linkedList = new LinkedList<FolderViewModel>();
      IDictionary<string, HierarchyLevelState> LevelStates = (IDictionary<string, HierarchyLevelState>) new Dictionary<string, HierarchyLevelState>();
      Guid folderGuidId = Guid.Empty;
      if (Guid.TryParse(folderId, out folderGuidId))
      {
        Folder folder = manager2.GetFolders().FirstOrDefault<Folder>((Expression<Func<Folder, bool>>) (f => f.Id == folderGuidId));
        if (folder == null)
        {
          if (!excludeNeighbours)
          {
            LibrariesManager manager3 = LibrariesManager.GetManager(providerName);
            if (result2 != 0)
            {
              ParameterExpression parameterExpression;
              // ISSUE: method reference
              List<string> list = manager3.GetLibraryNeighbours(Guid.Parse(folderId), sortExpression).Select<Library, string>(Expression.Lambda<Func<Library, string>>((Expression) Expression.Call(l.Id, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.ToString)), Array.Empty<Expression>()), parameterExpression)).ToList<string>();
              result1 = this.CalculateSkipParameter(folderId, (IList<string>) list, result2);
              LevelStates[Guid.Empty.ToString()] = new HierarchyLevelState()
              {
                Skip = result1,
                Take = result2,
                Total = list.Count
              };
            }
            foreach (TContentViewModel contentViewModel in this.GetContentItems(sortExpression, result1, result2, "", providerName, "").Items)
              linkedList.AddLast(new FolderViewModel()
              {
                Id = contentViewModel.Id,
                Title = contentViewModel.Title
              });
            this.SetHasChildren((IEnumerable<FolderViewModel>) linkedList, manager2.GetFolders());
          }
          else
          {
            TContent contentItem = this.GetContentItem(folderGuidId, providerName);
            linkedList.AddLast(new FolderViewModel()
            {
              Id = contentItem.Id,
              Title = (string) contentItem.Title
            });
          }
        }
        else
        {
          bool hasChildren = manager2.GetFolders().Any<Folder>((Expression<Func<Folder, bool>>) (f => f.ParentId == (Guid?) folder.Id));
          LibrariesManager manager4 = LibrariesManager.GetManager(providerName);
          if (result2 != 0)
          {
            ParameterExpression parameterExpression;
            // ISSUE: method reference
            List<string> list = manager4.GetFolderNeighbours(folder.Id, sortExpression).Select<IFolder, string>(Expression.Lambda<Func<IFolder, string>>((Expression) Expression.Call(f.Id, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.ToString)), Array.Empty<Expression>()), parameterExpression)).ToList<string>();
            result1 = this.CalculateSkipParameter(folder.Id.ToString(), (IList<string>) list, result2);
            Guid? parentId = folder.ParentId;
            string str;
            if (!parentId.HasValue)
            {
              str = folder.RootId.ToString();
            }
            else
            {
              parentId = folder.ParentId;
              str = parentId.ToString();
            }
            string key = str;
            LevelStates[key] = new HierarchyLevelState()
            {
              Skip = result1,
              Take = result2,
              Total = list.Count
            };
          }
          this.SetPredecessorFolders(folder, linkedList, LevelStates, (IFolderManager) (object) manager1, hasChildren, sortExpression, excludeNeighbours, result1, result2);
        }
      }
      ServiceUtility.DisableCache();
      if (result2 != 0)
      {
        FoldersCollectionContext predecessorFoldersInternal = new FoldersCollectionContext((IEnumerable<FolderViewModel>) linkedList);
        predecessorFoldersInternal.TotalCount = 0;
        predecessorFoldersInternal.FoldersContext = LevelStates;
        return (CollectionContext<FolderViewModel>) predecessorFoldersInternal;
      }
      return new CollectionContext<FolderViewModel>((IEnumerable<FolderViewModel>) linkedList)
      {
        TotalCount = 0
      };
    }

    /// <summary>Gets folders as tree.</summary>
    /// <param name="provider">The provider.</param>
    /// <returns></returns>
    private CollectionContext<FolderViewModel> GetFoldersAsTreeInternal(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      bool hierarchyMode)
    {
      ServiceUtility.RequestAuthentication();
      IFolderManager manager = (IFolderManager) (object) this.GetManager(provider);
      List<FolderViewModel> hierarchicalFolders = new List<FolderViewModel>();
      int? totalCount = new int?(0);
      IDictionary<string, HierarchyLevelState> dictionary = (IDictionary<string, HierarchyLevelState>) null;
      IQueryable<TContent> query = this.GetContentItems(provider);
      IEnumerable<FolderViewModel> items;
      if (hierarchyMode)
      {
        using (new CultureRegion(CultureInfo.InvariantCulture))
          query = DataProviderBase.SetExpressions<TContent>(query, filter, sortExpression, new int?(skip), new int?(take), ref totalCount);
        foreach (TContent content in (IEnumerable<TContent>) query)
          hierarchicalFolders.Add(new FolderViewModel()
          {
            Title = (string) content.Title,
            Id = content.Id
          });
        this.SetHasChildren((IEnumerable<FolderViewModel>) hierarchicalFolders, manager.GetFolders());
        if (take != 0)
        {
          if (dictionary == null)
            dictionary = (IDictionary<string, HierarchyLevelState>) new Dictionary<string, HierarchyLevelState>();
          dictionary[Guid.Empty.ToString()] = new HierarchyLevelState()
          {
            Skip = skip,
            Take = take,
            Total = totalCount.Value
          };
        }
        items = (IEnumerable<FolderViewModel>) hierarchicalFolders;
      }
      else
      {
        List<Guid> guidList = new List<Guid>();
        foreach (TContent content in (IEnumerable<TContent>) query)
        {
          hierarchicalFolders.Add(new FolderViewModel()
          {
            Title = (string) content.Title,
            Id = content.Id
          });
          guidList.Add(content.Id);
        }
        items = (IEnumerable<FolderViewModel>) hierarchicalFolders;
        this.SetHasChildren((IEnumerable<FolderViewModel>) hierarchicalFolders, manager.GetFolders());
      }
      ServiceUtility.DisableCache();
      if (dictionary != null)
      {
        FoldersCollectionContext foldersAsTreeInternal = new FoldersCollectionContext(items);
        foldersAsTreeInternal.TotalCount = totalCount.HasValue ? totalCount.Value : 0;
        foldersAsTreeInternal.FoldersContext = dictionary;
        return (CollectionContext<FolderViewModel>) foldersAsTreeInternal;
      }
      return new CollectionContext<FolderViewModel>(items)
      {
        TotalCount = totalCount.HasValue ? totalCount.Value : 0
      };
    }

    private void SetPredecessorFolders(
      Folder folder,
      LinkedList<FolderViewModel> hierarchicalFolders,
      IDictionary<string, HierarchyLevelState> LevelStates,
      IFolderManager manager,
      bool hasChildren,
      string sortExpression,
      bool excludeNeighbours,
      int skip,
      int take)
    {
      Guid? parentId;
      if (excludeNeighbours)
      {
        hierarchicalFolders.AddFirst(new FolderViewModel()
        {
          Id = folder.Id,
          ParentId = !folder.ParentId.HasValue ? new Guid?(folder.RootId) : folder.ParentId,
          RootId = folder.RootId,
          Title = (string) folder.Title,
          HasChildren = hasChildren,
          Path = this.GetFolderPath(folder, manager)
        });
      }
      else
      {
        IQueryable<Folder> source = manager.GetFolders().Where<Folder>((Expression<Func<Folder, bool>>) (f => f.ParentId == folder.ParentId && f.RootId == folder.RootId));
        if (!string.IsNullOrEmpty(sortExpression))
        {
          using (new CultureRegion(CultureInfo.InvariantCulture))
            source = source.OrderBy<Folder>(sortExpression);
        }
        if (skip != 0)
          source = source.Skip<Folder>(skip);
        if (take != 0)
          source = source.Take<Folder>(take);
        Folder[] array = source.ToArray<Folder>();
        List<Guid> guidList = new List<Guid>();
        for (int index = array.Length - 1; index >= 0; --index)
        {
          Folder folder1 = array[index];
          LinkedList<FolderViewModel> linkedList = hierarchicalFolders;
          FolderViewModel folderViewModel = new FolderViewModel();
          folderViewModel.Id = folder1.Id;
          parentId = folder1.ParentId;
          folderViewModel.ParentId = !parentId.HasValue ? new Guid?(folder1.RootId) : folder1.ParentId;
          folderViewModel.RootId = folder1.RootId;
          folderViewModel.Title = (string) folder1.Title;
          folderViewModel.Path = this.GetFolderPath(folder1, manager);
          linkedList.AddFirst(folderViewModel);
          guidList.Add(folder1.Id);
        }
        this.SetHasChildren((IEnumerable<FolderViewModel>) hierarchicalFolders, manager.GetFolders());
      }
      parentId = folder.ParentId;
      if (!parentId.HasValue)
      {
        if (!excludeNeighbours)
        {
          if (take != 0)
          {
            LibrariesManager librariesManager = (LibrariesManager) manager ?? LibrariesManager.GetManager();
            Guid rootId = folder.RootId;
            ParameterExpression parameterExpression;
            // ISSUE: method reference
            List<string> list = librariesManager.GetLibraryNeighbours(rootId, sortExpression).Select<Library, string>(Expression.Lambda<Func<Library, string>>((Expression) Expression.Call(l.Id, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.ToString)), Array.Empty<Expression>()), parameterExpression)).ToList<string>();
            skip = this.CalculateSkipParameter(rootId.ToString(), (IList<string>) list, take);
            LevelStates[Guid.Empty.ToString()] = new HierarchyLevelState()
            {
              Skip = skip,
              Take = take,
              Total = list.Count
            };
          }
          TContentViewModel[] array = this.GetContentItems(sortExpression, skip, take, "", manager.Provider.Name, "").Items.ToArray<TContentViewModel>();
          for (int index = array.Length - 1; index >= 0; --index)
          {
            TContentViewModel contentViewModel = array[index];
            hierarchicalFolders.AddFirst(new FolderViewModel()
            {
              Id = contentViewModel.Id,
              Title = contentViewModel.Title,
              HasChildren = contentViewModel.Id == folder.RootId
            });
            this.SetHasChildren((IEnumerable<FolderViewModel>) hierarchicalFolders, manager.GetFolders());
          }
        }
        else
        {
          TContent contentItem = this.GetContentItem(folder.RootId, manager.Provider.Name);
          hierarchicalFolders.AddFirst(new FolderViewModel()
          {
            Id = contentItem.Id,
            Title = (string) contentItem.Title
          });
        }
      }
      else
      {
        IFolderManager manager1 = manager;
        parentId = folder.ParentId;
        Guid id = parentId.Value;
        folder = manager1.GetFolder(id);
        if (take != 0)
        {
          ParameterExpression parameterExpression;
          // ISSUE: method reference
          List<string> list = ((LibrariesManager) manager).GetFolderNeighbours(folder.Id, sortExpression).Select<IFolder, string>(Expression.Lambda<Func<IFolder, string>>((Expression) Expression.Call(f.Id, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.ToString)), Array.Empty<Expression>()), parameterExpression)).ToList<string>();
          skip = this.CalculateSkipParameter(folder.Id.ToString(), (IList<string>) list, take);
          parentId = folder.ParentId;
          string str;
          if (!parentId.HasValue)
          {
            str = folder.RootId.ToString();
          }
          else
          {
            parentId = folder.ParentId;
            str = parentId.ToString();
          }
          string key = str;
          LevelStates[key] = new HierarchyLevelState()
          {
            Skip = skip,
            Take = take,
            Total = list.Count
          };
        }
        this.SetPredecessorFolders(folder, hierarchicalFolders, LevelStates, manager, true, sortExpression, excludeNeighbours, skip, take);
      }
    }

    private int CalculateSkipParameter(string id, IList<string> neighbours, int take)
    {
      int count = neighbours.Count;
      int skipParameter = neighbours.IndexOf(id) / take * take;
      if (skipParameter + take > count)
        skipParameter = count - take;
      return skipParameter;
    }

    internal FetchStrategy GetFetchStrategyFromCurrent(IManager manager) => manager.GetFetchStrategyFromCurrent();

    private List<TContent> FetchItemsList(IManager manager, IQueryable<TContent> items)
    {
      Type type = typeof (TContent);
      FetchStrategy fetchStrategyForItem = manager.GetFetchStrategyForItem(type, this.GetFetchProperties(), true);
      if (type.IsILifecycle())
      {
        IQueryable<TContent> queryable = this.GetContentItems(manager.Provider.Name);
        if (fetchStrategyForItem != null)
          queryable = queryable.LoadWith<TContent>(fetchStrategyForItem);
        return items.IncludeToList<TContent>(queryable);
      }
      if (fetchStrategyForItem != null)
        items = items.LoadWith<TContent>(fetchStrategyForItem);
      return items.ToList<TContent>();
    }

    internal virtual string[] GetFetchProperties() => (string[]) null;

    private static List<FolderChildrenCount> GetFoldersChildrenCount(
      Guid[] ids,
      IQueryable<Folder> folders)
    {
      folders = folders.Where<Folder>((Expression<Func<Folder, bool>>) (f => f.ParentId == new Guid?() && ids.Contains<Guid>(f.RootId)));
      return folders.GroupBy<Folder, Guid>((Expression<Func<Folder, Guid>>) (f => f.RootId)).Where<IGrouping<Guid, Folder>>((Expression<Func<IGrouping<Guid, Folder>, bool>>) (chf => chf.Count<Folder>() > 0)).Select<IGrouping<Guid, Folder>, FolderChildrenCount>((Expression<Func<IGrouping<Guid, Folder>, FolderChildrenCount>>) (chf => new FolderChildrenCount(chf.Key, chf.Count<Folder>()))).ToList<FolderChildrenCount>();
    }

    private static List<FolderChildrenDateCreated> GetChildItemsInformation<T>(
      Guid[] ids,
      IQueryable<T> childItems)
      where T : Telerik.Sitefinity.GenericContent.Model.Content, IHasParent
    {
      childItems = childItems.Where<T>((Expression<Func<T, bool>>) (p => ids.Contains<Guid>(p.Parent.Id)));
      return childItems.GroupBy<T, Guid>((Expression<Func<T, Guid>>) (p => p.Parent.Id)).Where<IGrouping<Guid, T>>((Expression<Func<IGrouping<Guid, T>, bool>>) (ch => ch.Count<T>() > 0)).Select<IGrouping<Guid, T>, FolderChildrenDateCreated>((Expression<Func<IGrouping<Guid, T>, FolderChildrenDateCreated>>) (chi => new FolderChildrenDateCreated(chi.Key, chi.Max<T, DateTime>((Func<T, DateTime>) (i => i.DateCreated))))).ToList<FolderChildrenDateCreated>();
    }

    /// <summary>
    /// Appends parameters to the specified URL in the order that they were passed. If the specified parameter has no value,
    /// the parameter is not appended
    /// </summary>
    /// <param name="baseUrl">The url to append parameters to</param>
    /// <param name="urlParams">the parameters to append</param>
    /// <returns></returns>
    protected internal string AppendUrlParameters(
      string baseUrl,
      Dictionary<string, string> urlParams)
    {
      string str1 = baseUrl.TrimEnd('/');
      if (!string.IsNullOrEmpty(baseUrl))
      {
        string str2 = string.Format("{0}/?", (object) str1);
        foreach (KeyValuePair<string, string> urlParam in urlParams)
        {
          if (!string.IsNullOrEmpty(urlParam.Value))
            str2 = string.Format("{0}{1}={2}&", (object) str2, (object) urlParam.Key, (object) urlParam.Value);
        }
        str1 = str2.TrimEnd('&', '?');
      }
      return str1;
    }
  }
}
