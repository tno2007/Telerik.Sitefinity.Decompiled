// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.ContentManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Telerik.Sitefinity.Clients;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent.Archive;
using Telerik.Sitefinity.Modules.GenericContent.Configuration;
using Telerik.Sitefinity.Modules.GenericContent.Web.UI;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Web.UrlEvaluation;
using Telerik.Sitefinity.Web.Utilities;
using Telerik.Sitefinity.Workflow;
using Telerik.Sitefinity.Workflow.Activities;

namespace Telerik.Sitefinity.Modules.GenericContent
{
  /// <summary>
  /// Represents an intermediary between content objects and content data.
  /// </summary>
  public class ContentManager : 
    ContentManagerBase<ContentDataProvider>,
    IContentLifecycleManager<ContentItem>,
    IContentLifecycleManager,
    IManager,
    IDisposable,
    IProviderResolver,
    ILifecycleManager,
    ILanguageDataManager
  {
    private const string removeContentRelationsKey = "remove-content-relations";
    /// <summary>An array of ObjectData with 0 length</summary>
    protected static readonly ObjectData[] EmptyObjectDataArray = new ObjectData[0];
    /// <summary>
    /// Guid.Empty in the format of '00000000-0000-0000-0000-000000000000'
    /// </summary>
    protected static readonly string EmptyGuidString = Guid.Empty.ToString("D", (IFormatProvider) CultureInfo.InvariantCulture);
    /// <summary>
    /// Full type name of the ContentBlock fontent control (widget)
    /// </summary>
    protected static readonly string ContentBlockTypeName = typeof (ContentBlock).FullName;
    /// <summary>
    /// Property of ContentBlock that stores the ID of the shared content it uses
    /// </summary>
    public static readonly string SharedContentIdPropertyName = "SharedContentID";
    /// <summary>
    /// Property of ContentBlock that stores teh ID of the shared content it uses
    /// </summary>
    public static readonly string HtmlPropertyName = "Html";
    /// <summary>
    /// Property of ContentBlock that stores the name of the provider that was used to create the shared content used by that ContentBlock
    /// </summary>
    protected internal static readonly string ProviderNamePropertyName = "ProviderName";
    /// <summary>Name of the System.Web.Control.ID property</summary>
    protected static readonly string IDPropertyName = "ID";

    /// <summary>
    /// Initializes a new instance of <see cref="T:Telerik.Sitefinity.Modules.GenericContent.ContentManager" /> class with the default provider.
    /// </summary>
    public ContentManager()
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="T:Telerik.Sitefinity.Modules.GenericContent.ContentManager" /> class and sets the specified provider.
    /// </summary>
    /// <param name="providerName">
    /// The name of the provider. If empty string or null the default provider is set.
    /// </param>
    public ContentManager(string providerName)
      : base(providerName)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.GenericContent.ContentManager" /> class.
    /// </summary>
    /// <param name="providerName">
    /// The name of the provider. If empty string or null the default provider is set
    /// </param>
    /// <param name="transactionName">
    /// The name of a distributed transaction. If empty string or null this manager will use separate transaction.
    /// </param>
    public ContentManager(string providerName, string transactionName)
      : base(providerName, transactionName)
    {
    }

    static ContentManager()
    {
      ManagerBase<ContentDataProvider>.Executing += new EventHandler<ExecutingEventArgs>(ContentManager.Provider_Executing);
      ManagerBase<ContentDataProvider>.Executed += new EventHandler<ExecutedEventArgs>(ContentManager.Provider_Executed);
    }

    /// <summary>Creates new content item.</summary>
    /// <returns>The new content item.</returns>
    public virtual ContentItem CreateContent() => this.Provider.CreateContent();

    /// <summary>Creates new content item with the specified ID.</summary>
    /// <param name="id">The id of the new content.</param>
    /// <returns>The new content item.</returns>
    public virtual ContentItem CreateContent(Guid id) => this.Provider.CreateContent(id);

    /// <summary>Gets a content with the specified ID.</summary>
    /// <param name="id">The ID to search for.</param>
    /// <returns>A content item.</returns>
    public virtual ContentItem GetContent(Guid id) => this.Provider.GetContent(id);

    /// <summary>Gets a query for content items.</summary>
    /// <returns>The query for content items.</returns>
    public virtual IQueryable<ContentItem> GetContent() => this.Provider.GetContent();

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The content item to delete.</param>
    public virtual void Delete(ContentItem item) => this.Provider.Delete(item);

    /// <summary>Gets the items.</summary>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    /// <returns></returns>
    public override IQueryable<TItem> GetItems<TItem>()
    {
      if (typeof (Content).IsAssignableFrom(typeof (TItem)))
        return this.GetContent() as IQueryable<TItem>;
      throw new ArgumentException("Unable to cast TItem to Content");
    }

    /// <summary>
    /// Checks in the content in the temp state. Content becomes master after the check in.
    /// </summary>
    /// <param name="item">Content in temp state that is to be checked in.</param>
    /// <returns>An item in master state.</returns>
    [Obsolete("Use the Lifecycle property")]
    public virtual ContentItem CheckIn(ContentItem contentItem) => (ContentItem) this.Lifecycle.CheckIn((ILifecycleDataItem) contentItem);

    /// <summary>
    /// Checks out the content in master state. Content becomes temp after the check out.
    /// </summary>
    /// <param name="item">Content in master state that is to be checked out.</param>
    /// <returns>A content that was checked out in temp state.</returns>
    [Obsolete("Use the Lifecycle property")]
    public virtual ContentItem CheckOut(ContentItem contentItem) => (ContentItem) this.Lifecycle.CheckOut((ILifecycleDataItem) contentItem);

    /// <summary>
    /// Edits the content in live state. Content becomes master after the edit.
    /// </summary>
    /// <param name="item">Content in live state that is to be edited.</param>
    /// <returns>A content that was edited in master state.</returns>
    [Obsolete("Use the Lifecycle property")]
    public virtual ContentItem Edit(ContentItem contentItem) => (ContentItem) this.Lifecycle.Edit((ILifecycleDataItem) contentItem);

    /// <summary>
    /// Publishes the content in master state. Content becomes live after the publish.
    /// </summary>
    /// <param name="item">Content in master state that is to be published.</param>
    [Obsolete("Use the Lifecycle property")]
    public virtual ContentItem Publish(ContentItem contentItem) => (ContentItem) this.Lifecycle.Publish((ILifecycleDataItem) contentItem);

    /// <summary>Unpublish a content item in live state.</summary>
    /// <param name="item">Live item to unpublish.</param>
    /// <returns>Master (draft) state.</returns>
    [Obsolete("Use the Lifecycle property")]
    public virtual ContentItem Unpublish(ContentItem cnt) => (ContentItem) this.Lifecycle.Unpublish((ILifecycleDataItem) cnt);

    /// <summary>
    /// Returns ID of the user that checked out the item, or Guid.Empty if it is not checked out
    /// </summary>
    /// <param name="item">Item to get the user ID it is locked by</param>
    /// <param name="itemsQuery">Query of all ContentItems</param>
    /// <returns>ID of the user that ckecked out the item or Guid.Empty if the item is not checked out.</returns>
    [Obsolete("Use the Lifecycle property")]
    public virtual Guid GetCheckedOutBy(ContentItem item) => this.Lifecycle.GetCheckedOutBy((ILifecycleDataItem) item);

    /// <summary>
    /// Returns true or false, depending on whether the <paramref name="item" /> is checked out or not
    /// </summary>
    /// <param name="item">Item to test</param>
    /// <param name="itemsQuery">Query of all ContentItems</param>
    /// <returns>True if the item is checked out, false otherwize.</returns>
    [Obsolete("Use the Lifecycle property")]
    public bool IsCheckedOut(ContentItem item) => this.Lifecycle.IsCheckedOut((ILifecycleDataItem) item);

    /// <summary>
    /// Checks if <paramref name="item" /> is checked out by user with a specified id
    /// </summary>
    /// <param name="item">Item to test</param>
    /// <param name="userId">Id of the user to check if he/she checked out <paramref name="item" /></param>
    /// <param name="itemsQuery">Query of all <typeparamref name="TItem" />-s.</param>
    /// <returns>True if it was checked out by a user with the specified id, false otherwize</returns>
    [Obsolete("Use the Lifecycle property")]
    public bool IsCheckedOutBy(ContentItem item, Guid userId) => this.Lifecycle.IsCheckedOutBy((ILifecycleDataItem) item, userId);

    /// <summary>
    /// Copy one content item to another. USed by content lifecycle.
    /// </summary>
    /// <param name="source">Item to copy to.</param>
    /// <param name="destination">Item to copy from</param>
    [Obsolete("Use the Copy method with the culture parameter")]
    public void Copy(ContentItem source, ContentItem destination)
    {
      this.CopyUrls<ContentItemUrlData>((Content) source, (Content) destination, source.Urls, destination.Urls);
      if (source.Status == ContentLifecycleStatus.PartialTemp)
        return;
      destination.Author = source.Author;
      destination.Name = source.Name;
    }

    [Obsolete("Use the Copy method with the culture parameter")]
    public void Copy(Content source, Content destination)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      if (destination == null)
        throw new ArgumentNullException(nameof (destination));
      Type type;
      if ((type = source.GetType()) != destination.GetType())
        throw new ArgumentException("source and destination must be of the same type");
      if (type == typeof (ContentItem))
        this.Copy((ContentItem) source, (ContentItem) destination);
      else
        throw new NotSupportedException("Type {0} is not supported".Arrange((object) type));
    }

    /// <summary>
    /// Updates the content blocks related to a shared content item accross all pages.
    /// This requires ContentManager to be in retrieved in transaction, since it involves changes performed on controls, via PageManager.
    /// </summary>
    /// <param name="item">The update content item.</param>
    [Obsolete("Do not use. Pages are always up to date with the shared content.")]
    public void UpdateContentBlocks(ContentItem item) => this.UpdateContentBlocks(item, Guid.Empty);

    [Obsolete("Do not use. Pages are always up to date with the shared content.")]
    public PageManager UpdateContentBlocks(
      ContentItem item,
      Guid currentlyEditedPageDraftId)
    {
      return this.UpdateContentBlocks(item, currentlyEditedPageDraftId, SharedContentStatisticsMode.ExcludeTemps);
    }

    /// <summary>
    /// Updates the content blocks related to a shared content item accross all pages.
    /// This requires ContentManager to be in retrieved in transaction, since it involves changes performed on controls, via PageManager.
    /// </summary>
    /// <param name="item">The update content item.</param>
    /// <param name="currentlyEditedPageDraftId">The currently edited page draft id.</param>
    [Obsolete("Do not use. Pages are always up to date with the shared content.")]
    public PageManager UpdateContentBlocks(
      ContentItem item,
      Guid currentlyEditedPageDraftId,
      SharedContentStatisticsMode statsMode)
    {
      Guid id = item.Id;
      PageManager pageManager = this.GetPageManager((string) null);
      string name = pageManager.Provider.Name;
      foreach (PageNode pageNode in this.GetPagesThatUseSharedContent(name, id, statsMode))
      {
        if (pageNode != null)
        {
          if (pageNode.IsGranted("Pages", "Modify") && (pageNode.GetPageData().LockedBy == Guid.Empty || pageNode.GetPageData().LockedBy == SecurityManager.CurrentUserId))
          {
            PageDraft pageDraft = (PageDraft) null;
            if (currentlyEditedPageDraftId != Guid.Empty)
              pageDraft = pageNode.GetPageData().Drafts.Where<PageDraft>((Func<PageDraft, bool>) (draft => draft.Id == currentlyEditedPageDraftId)).FirstOrDefault<PageDraft>();
            if (pageDraft == null)
            {
              IDictionary<string, DecisionActivity> currentDecisions = WorkflowManager.GetCurrentDecisions(typeof (PageNode), name, pageNode.Id);
              if (currentDecisions != null && currentDecisions.Values.Where<DecisionActivity>((Func<DecisionActivity, bool>) (action => action.ActionName == "UpdateContent")).FirstOrDefault<DecisionActivity>() != null)
                WorkflowManager.MessageWorkflow(pageNode.Id, typeof (PageNode), name, "UpdateContent", true, new Dictionary<string, string>()
                {
                  {
                    "ContentItemId",
                    id.ToString()
                  },
                  {
                    "ContentItemProviderName",
                    this.Provider.Name
                  }
                });
            }
            else
              this.PushUpdatedContentToPageDraft(item.Id, pageDraft);
          }
        }
      }
      return pageManager;
    }

    /// <summary>
    /// Pushes the updated content to content blocks on a page draft.
    /// </summary>
    /// <param name="contentItemId">The content item id to update.</param>
    /// <param name="pageDraft">The page draft.</param>
    public void PushUpdatedContentToPageDraft(Guid contentItemId, PageDraft pageDraft)
    {
      string contentBlockTypeName = typeof (ContentBlock).FullName;
      ContentItem content = this.GetContent(contentItemId);
      foreach (PageDraftControl pageDraftControl in pageDraft.Controls.Where<PageDraftControl>((Func<PageDraftControl, bool>) (contentBlock => contentBlock.ObjectType == contentBlockTypeName)).ToArray<PageDraftControl>())
      {
        if (pageDraftControl.IsGranted("Controls", "EditControlProperties"))
        {
          Guid contentBlockId = pageDraftControl.Id;
          string contentItemIdstr = contentItemId.ToString();
          if (pageDraftControl.Properties.Where<ControlProperty>((Func<ControlProperty, bool>) (prop => prop.Name == ContentManager.SharedContentIdPropertyName && prop.Value.ToString() == contentItemIdstr)).FirstOrDefault<ControlProperty>() != null)
          {
            ControlProperty dataItem = pageDraftControl.Properties.Where<ControlProperty>((Func<ControlProperty, bool>) (prop => prop.Control.Id == contentBlockId && prop.Name == "Html")).FirstOrDefault<ControlProperty>();
            if (dataItem != null)
            {
              if (SystemManager.CurrentContext.AppSettings.Multilingual)
              {
                Lstring lstring = content.GetString("Content", SystemManager.CurrentContext.Culture);
                dataItem.GetString("MultilingualValue").SetString(SystemManager.CurrentContext.Culture, (string) lstring);
              }
              else
                dataItem.Value = content.Content.Value;
            }
          }
        }
      }
    }

    /// <summary>Gets the default provider for this manager.</summary>
    /// <value></value>
    protected internal override GetDefaultProvider DefaultProviderDelegate => (GetDefaultProvider) (() => Config.Get<ContentConfig>().DefaultProvider);

    /// <summary>The name of the module that this manager belongs to.</summary>
    /// <value></value>
    public override string ModuleName => "GenericContent";

    /// <summary>Collection of data provider settings.</summary>
    /// <value></value>
    protected internal override ConfigElementDictionary<string, DataProviderSettings> ProvidersSettings => Config.Get<ContentConfig>().Providers;

    /// <summary>Gets a manger instance for the default data provider.</summary>
    /// <returns>The manager instance.</returns>
    public static ContentManager GetManager() => ManagerBase<ContentDataProvider>.GetManager<ContentManager>();

    /// <summary>
    /// Get an instance of the blogs manager by explicitly specifying the required provider to use
    /// </summary>
    /// <param name="providerName">Name of the provider to use, or null/empty string to use the default provider.</param>
    /// <param name="transactionName">Name of the transaction.</param>
    /// <returns>Instance of the blogs manager</returns>
    public static ContentManager GetManager(
      string providerName,
      string transactionName)
    {
      return ManagerBase<ContentDataProvider>.GetManager<ContentManager>(providerName, transactionName);
    }

    /// <summary>
    /// Gets a manger instance for the specified data provider.
    /// </summary>
    /// <param name="providerName">The name of the data provider.</param>
    /// <returns>The manager instance.</returns>
    public static ContentManager GetManager(string providerName) => ManagerBase<ContentDataProvider>.GetManager<ContentManager>(providerName);

    /// <summary>Gets the archieve items.</summary>
    /// <param name="contentType">Type of the content.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="dateBuildOptions">The archive groping date option.</param>
    /// <param name="propertyName">Property name used to build the filter expression when evaluating URLs by date.</param>
    /// <returns></returns>
    public virtual List<ArchiveItem> GetArchieveItems(
      Type contentType,
      string providerName,
      DateBuildOptions dateBuildOptions,
      string propertyName)
    {
      return this.Provider.GetArchieveItems(contentType, providerName, dateBuildOptions, propertyName);
    }

    /// <summary>
    /// Gets the public (live) version of <paramref name="cnt" />, if it exists
    /// </summary>
    /// <param name="cnt">Type of the content item</param>
    /// <returns>Public (live) version of <paramref name="cnt" />, if it exists</returns>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="cnt" /> is <c>null</c>.</exception>
    [Obsolete("Use the Lifecycle property")]
    public virtual ContentItem GetLive(ContentItem cnt) => (ContentItem) this.Lifecycle.GetLive((ILifecycleDataItem) cnt);

    /// <summary>
    /// Get a temp for <paramref name="cnt" />, if it exists.
    /// </summary>
    /// <param name="cnt">Content item to get a temp for</param>
    /// <returns>Temp version of <paramref name="cnt" />, if it exists.</returns>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="cnt" /> is <c>null</c>.</exception>
    [Obsolete("Use the Lifecycle property")]
    public virtual ContentItem GetTemp(ContentItem cnt) => (ContentItem) this.Lifecycle.GetTemp((ILifecycleDataItem) cnt);

    /// <summary>
    /// Accepts a content item and returns an item in master state
    /// </summary>
    /// <typeparam name="TContent">Type of the content item</typeparam>
    /// <param name="cnt">Content item whose master to get</param>
    /// <returns>
    /// If <paramref name="cnt" /> is master itself, returns cnt.
    /// Otherwize, looks up the master associated with <paramref name="cnt" /> and returns it.
    /// When there is no master, an exception will be thrown.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">When no master can be found for <paramref name="cnt" />.</exception>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="cnt" /> is <c>null</c>.</exception>
    [Obsolete("Use the Lifecycle property")]
    public virtual ContentItem GetMaster(ContentItem cnt) => (ContentItem) this.Lifecycle.GetMaster((ILifecycleDataItem) cnt);

    /// <summary>
    /// Schedule a content item - to be published from one date to another
    /// </summary>
    /// <param name="item">Content item in master state</param>
    /// <param name="publicationDate">Point in time at which the item will be visible on the public side</param>
    /// <param name="expirationDate">Point in time at which the item will no longer be visible on the public side or null if the item should never expire</param>
    [Obsolete("Use the Lifecycle property")]
    public ContentItem Schedule(
      ContentItem item,
      DateTime publicationDate,
      DateTime? expirationDate)
    {
      return this.Provider.Schedule<ContentItem>(item, publicationDate, expirationDate, new Action<ContentItem, ContentItem>(this.Copy), this.GetContent());
    }

    /// <summary>
    /// Checks in the content in the temp state. Content becomes master after the check in.
    /// </summary>
    /// <param name="item">Content in temp state that is to be checked in.</param>
    /// <returns>An item in master state.</returns>
    [Obsolete("Use the Lifecycle property")]
    public Content CheckIn(Content item) => item is ContentItem ? (Content) this.CheckIn((ContentItem) item) : throw new NotSupportedException("Type {0}is not supported by this manager".Arrange((object) item.GetType()));

    /// <summary>
    /// Checks out the content in master state. Content becomes temp after the check out.
    /// </summary>
    /// <param name="item">Content in master state that is to be checked out.</param>
    /// <returns>A content that was checked out in temp state.</returns>
    [Obsolete("Use the Lifecycle property")]
    public Content CheckOut(Content item) => item is ContentItem ? (Content) this.CheckOut((ContentItem) item) : throw new NotSupportedException("Type {0}is not supported by this manager".Arrange((object) item.GetType()));

    /// <summary>
    /// Edits the content in live state. Content becomes master after the edit.
    /// </summary>
    /// <param name="item">Content in live state that is to be edited.</param>
    /// <returns>A content that was edited in master state.</returns>
    [Obsolete("Use the Lifecycle property")]
    public Content Edit(Content item) => item is ContentItem ? (Content) this.Edit((ContentItem) item) : throw new NotSupportedException("Type {0}is not supported by this manager".Arrange((object) item.GetType()));

    /// <summary>
    /// Publishes the content in master state. Content becomes live after the publish.
    /// </summary>
    /// <param name="item">Content in master state that is to be published.</param>
    /// <returns>Published content item</returns>
    [Obsolete("Use the Lifecycle property")]
    public Content Publish(Content item) => item is ContentItem ? (Content) this.Publish((ContentItem) item) : throw new NotSupportedException("Type {0}is not supported by this manager".Arrange((object) item.GetType()));

    /// <summary>Unpublish a content item in live state.</summary>
    /// <param name="item">Live item to unpublish.</param>
    /// <returns>Master (draft) state.</returns>
    [Obsolete("Use the Lifecycle property")]
    public Content Unpublish(Content item) => item is ContentItem ? (Content) this.Unpublish((ContentItem) item) : throw new NotSupportedException("Type {0}is not supported by this manager".Arrange((object) item.GetType()));

    /// <summary>
    /// Schedule a content item - to be published from one date to another
    /// </summary>
    /// <param name="item">Content item in master state</param>
    /// <param name="publicationDate">Point in time at which the item will be visible on the public side</param>
    /// <param name="expirationDate">Point in time at which the item will no longer be visible on the public side or null if the item should never expire</param>
    /// <returns>Scheduled content item</returns>
    [Obsolete("Use the Lifecycle property")]
    public Content Schedule(
      Content item,
      DateTime publicationDate,
      DateTime? expirationDate)
    {
      if (item is ContentItem)
        return (Content) this.Schedule((ContentItem) item, publicationDate, expirationDate);
      throw new NotSupportedException("Type {0}is not supported by this manager".Arrange((object) item.GetType()));
    }

    /// <summary>
    /// Returns ID of the user that checked out the item, or Guid.Empty if it is not checked out
    /// </summary>
    /// <param name="item">Item to get the user ID it is locked by</param>
    /// <param name="itemsQuery">Query of all <typeparamref name="Content" />-s</param>
    /// <returns>
    /// ID of the user that ckecked out the item or Guid.Empty if the item is not checked out.
    /// </returns>
    [Obsolete("Use the Lifecycle property")]
    public Guid GetCheckedOutBy(Content item) => item is ContentItem ? this.GetCheckedOutBy((ContentItem) item) : throw new NotSupportedException("Type {0}is not supported by this manager".Arrange((object) item.GetType()));

    /// <summary>
    /// Returns true or false, depending on whether the <paramref name="item" /> is checked out or not
    /// </summary>
    /// <param name="item">Item to test</param>
    /// <param name="itemsQuery">Query of all <typeparamref name="Content" />-s</param>
    /// <returns>True if the item is checked out, false otherwize.</returns>
    [Obsolete("Use the Lifecycle property")]
    public bool IsCheckedOut(Content item) => item is ContentItem ? this.IsCheckedOut((ContentItem) item) : throw new NotSupportedException("Type {0}is not supported by this manager".Arrange((object) item.GetType()));

    /// <summary>
    /// Checks if <paramref name="item" /> is checked out by user with a specified id
    /// </summary>
    /// <param name="item">Item to test</param>
    /// <param name="userId">Id of the user to check if he/she checked out <paramref name="item" /></param>
    /// <param name="itemsQuery">Query of all <typeparamref name="Content" />-s.</param>
    /// <returns>
    /// True if it was checked out by a user with the specified id, false otherwize
    /// </returns>
    [Obsolete("Use the Lifecycle property")]
    public bool IsCheckedOutBy(Content item, Guid userId) => item is ContentItem ? this.IsCheckedOutBy((ContentItem) item, userId) : throw new NotSupportedException("Type {0}is not supported by this manager".Arrange((object) item.GetType()));

    /// <summary>
    /// Gets the public (live) version of <paramref name="cnt" />, if it exists
    /// </summary>
    /// <param name="cnt">Type of the content item</param>
    /// <returns>
    /// Public (live) version of <paramref name="cnt" />, if it exists
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="cnt" /> is <c>null</c>.</exception>
    [Obsolete("Use the Lifecycle property")]
    public Content GetLive(Content cnt) => cnt is ContentItem ? (Content) this.GetLive((ContentItem) cnt) : throw new NotSupportedException("Type {0}is not supported by this manager".Arrange((object) cnt.GetType()));

    /// <summary>
    /// Get a temp for <paramref name="cnt" />, if it exists.
    /// </summary>
    /// <param name="cnt">Content item to get a temp for</param>
    /// <returns>
    /// Temp version of <paramref name="cnt" />, if it exists.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="cnt" /> is <c>null</c>.</exception>
    [Obsolete("Use the Lifecycle property")]
    public Content GetTemp(Content cnt) => cnt is ContentItem ? (Content) this.GetTemp((ContentItem) cnt) : throw new NotSupportedException("Type {0}is not supported by this manager".Arrange((object) cnt.GetType()));

    /// <summary>
    /// Accepts a content item and returns an item in master state
    /// </summary>
    /// <param name="cnt">Content item whose master to get</param>
    /// <returns>
    /// If <paramref name="cnt" /> is master itself, returns cnt.
    /// Otherwize, looks up the master associated with <paramref name="cnt" /> and returns it.
    /// When there is no master, an exception will be thrown.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">When no master can be found for <paramref name="cnt" />.</exception>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="cnt" /> is <c>null</c>.</exception>
    [Obsolete("Use the Lifecycle property")]
    public Content GetMaster(Content cnt) => cnt is ContentItem ? (Content) this.GetMaster((ContentItem) cnt) : throw new NotSupportedException("Type {0}is not supported by this manager".Arrange((object) cnt.GetType()));

    public bool IsContentItemUsedOnAnyPage(Guid contentId) => this.IsContentItemUsedOnAnyPage(contentId, (string) null);

    public bool IsContentItemUsedOnAnyPage(Guid contentId, string pageProviderName) => this.GetCountOfPagesThatUseContent(contentId, pageProviderName) > 0;

    /// <summary>Get all shared content items used on a page</summary>
    /// <param name="pageDataID">ID of the page to test</param>
    /// <returns>All shared content items that are hosted on a page using the default page provider</returns>
    public IQueryable<ContentItem> GetContentByPage(Guid pageDataID) => this.GetContentByPage(pageDataID, (string) null);

    /// <summary>Get all shared content items used on a page</summary>
    /// <param name="pageDataID">ID of the page to test</param>
    /// <param name="pageProviderName">Name of the page provider to use</param>
    /// <returns>All shared content items that are hosted on a pge using the default page provider</returns>
    public IQueryable<ContentItem> GetContentByPage(
      Guid pageDataID,
      string pageProviderName)
    {
      return this.GetContentByPage(pageDataID, pageProviderName, SystemManager.CurrentContext.Culture);
    }

    /// <summary>Get all shared content items used on a page</summary>
    /// <param name="pageDataID">ID of the page to test</param>
    /// <param name="pageProviderName">Name of the page provider to use</param>
    /// <param name="culture">Filter by this culture</param>
    /// <returns>All shared content items that are hosted on a pge using the default page provider</returns>
    public IQueryable<ContentItem> GetContentByPage(
      Guid pageDataID,
      string pageProviderName,
      CultureInfo culture)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      ContentManager.\u003C\u003Ec__DisplayClass54_0 cDisplayClass540 = new ContentManager.\u003C\u003Ec__DisplayClass54_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass540.pageDataID = pageDataID;
      PageManager pageManager = this.GetPageManager(pageProviderName);
      // ISSUE: reference to a compiler-generated field
      PageData pageData = pageManager.GetPageDataList().Where<PageData>((Expression<Func<PageData, bool>>) (pd => pd.Id == cDisplayClass540.pageDataID)).SingleOrDefault<PageData>();
      if (pageData == null || SystemManager.CurrentContext.AppSettings.Multilingual && !((IEnumerable<CultureInfo>) pageData.NavigationNode.AvailableCultures).Contains<CultureInfo>(SystemManager.CurrentContext.Culture))
        return Enumerable.Empty<ContentItem>().AsQueryable<ContentItem>();
      // ISSUE: reference to a compiler-generated field
      Guid[] array1 = pageManager.GetControls<PageControl>().Where<PageControl>((Expression<Func<PageControl, bool>>) (c => c.Page.Id == cDisplayClass540.pageDataID && c.ObjectType == ContentManager.ContentBlockTypeName)).Select<PageControl, Guid>((Expression<Func<PageControl, Guid>>) (c => c.Id)).ToArray<Guid>();
      // ISSUE: reference to a compiler-generated field
      Guid[] array2 = pageManager.GetControls<PageDraftControl>().Where<PageDraftControl>((Expression<Func<PageDraftControl, bool>>) (c => c.Page.IsTempDraft == false && c.Page.ParentId == cDisplayClass540.pageDataID)).Select<PageDraftControl, Guid>((Expression<Func<PageDraftControl, Guid>>) (c => c.Id)).ToArray<Guid>();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass540.contentBlockControlIDs = ((IEnumerable<Guid>) array1).Union<Guid>((IEnumerable<Guid>) array2).Distinct<Guid>().ToArray<Guid>();
      // ISSUE: reference to a compiler-generated field
      if (cDisplayClass540.contentBlockControlIDs.Length == 0)
        return Enumerable.Empty<ContentItem>().AsQueryable<ContentItem>();
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      ContentManager.\u003C\u003Ec__DisplayClass54_1 cDisplayClass541 = new ContentManager.\u003C\u003Ec__DisplayClass54_1();
      ParameterExpression parameterExpression;
      // ISSUE: method reference
      // ISSUE: field reference
      // ISSUE: method reference
      // ISSUE: field reference
      // ISSUE: method reference
      // ISSUE: method reference
      ControlProperty[] array3 = pageManager.GetProperties().Where<ControlProperty>(Expression.Lambda<Func<ControlProperty, bool>>((Expression) Expression.AndAlso((Expression) Expression.AndAlso(p.Name == ContentManager.SharedContentIdPropertyName, (Expression) Expression.NotEqual((Expression) Expression.Call(p.Value, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.ToString)), Array.Empty<Expression>()), (Expression) Expression.Field((Expression) null, FieldInfo.GetFieldFromHandle(__fieldref (ContentManager.EmptyGuidString))))), (Expression) Expression.Call((Expression) null, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Enumerable.Contains)), new Expression[2]
      {
        (Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass540, typeof (ContentManager.\u003C\u003Ec__DisplayClass54_0)), FieldInfo.GetFieldFromHandle(__fieldref (ContentManager.\u003C\u003Ec__DisplayClass54_0.contentBlockControlIDs))),
        (Expression) Expression.Property((Expression) Expression.Property((Expression) parameterExpression, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ControlProperty.get_Control))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ObjectData.get_Id)))
      })), parameterExpression)).ToArray<ControlProperty>();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass541.contentIDs = ((IEnumerable<ControlProperty>) array3).Select<ControlProperty, Guid>((Func<ControlProperty, Guid>) (p => new Guid(p.Value))).ToArray<Guid>();
      // ISSUE: reference to a compiler-generated field
      return this.GetContent().Where<ContentItem>((Expression<Func<ContentItem, bool>>) (c => cDisplayClass541.contentIDs.Contains<Guid>(c.Id))).WhereQueryTypeIsTranslatedInCulture<ContentItem>(culture);
    }

    /// <summary>
    /// Gets the number of shared content items on a page for the default page provider
    /// </summary>
    /// <param name="pageDataID">ID of the page to test</param>
    /// <param name="pageProviderName">Name of the page provider to use or null for default</param>
    /// <returns>Number of shared content items on a page</returns>
    public int GetCountOfContentItemsByPage(Guid pageDataID) => this.GetCountOfContentItemsByPage(pageDataID, (string) null);

    /// <summary>Gets the number of shared content items on a page</summary>
    /// <param name="pageDataID">ID of the page to test</param>
    /// <param name="pageProviderName">Name of the page provider to use or null for default</param>
    /// <returns>Number of shared content items on a page</returns>
    public int GetCountOfContentItemsByPage(Guid pageDataID, string pageProviderName)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      ContentManager.\u003C\u003Ec__DisplayClass56_0 cDisplayClass560 = new ContentManager.\u003C\u003Ec__DisplayClass56_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass560.pageDataID = pageDataID;
      PageManager pageManager = this.GetPageManager(pageProviderName);
      // ISSUE: reference to a compiler-generated field
      PageData pageData = pageManager.GetPageDataList().Where<PageData>((Expression<Func<PageData, bool>>) (pd => pd.Id == cDisplayClass560.pageDataID)).SingleOrDefault<PageData>();
      if (pageData == null || SystemManager.CurrentContext.AppSettings.Multilingual && !((IEnumerable<CultureInfo>) pageData.NavigationNode.AvailableCultures).Contains<CultureInfo>(SystemManager.CurrentContext.Culture))
        return 0;
      // ISSUE: reference to a compiler-generated field
      Guid[] array = pageManager.GetControls<PageControl>().Where<PageControl>((Expression<Func<PageControl, bool>>) (c => c.Page.Id == cDisplayClass560.pageDataID && c.ObjectType == ContentManager.ContentBlockTypeName)).Select<PageControl, Guid>((Expression<Func<PageControl, Guid>>) (c => c.Id)).ToArray<Guid>();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass560.contentBlockControlIDs = array;
      // ISSUE: reference to a compiler-generated field
      if (cDisplayClass560.contentBlockControlIDs.Length == 0)
        return 0;
      ParameterExpression parameterExpression;
      // ISSUE: method reference
      // ISSUE: field reference
      // ISSUE: method reference
      // ISSUE: field reference
      // ISSUE: method reference
      // ISSUE: method reference
      return ((IEnumerable<string>) pageManager.GetProperties().Where<ControlProperty>(Expression.Lambda<Func<ControlProperty, bool>>((Expression) Expression.AndAlso((Expression) Expression.AndAlso(p.Name == ContentManager.SharedContentIdPropertyName, (Expression) Expression.NotEqual((Expression) Expression.Call(p.Value, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.ToString)), Array.Empty<Expression>()), (Expression) Expression.Field((Expression) null, FieldInfo.GetFieldFromHandle(__fieldref (ContentManager.EmptyGuidString))))), (Expression) Expression.Call((Expression) null, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Enumerable.Contains)), new Expression[2]
      {
        (Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass560, typeof (ContentManager.\u003C\u003Ec__DisplayClass56_0)), FieldInfo.GetFieldFromHandle(__fieldref (ContentManager.\u003C\u003Ec__DisplayClass56_0.contentBlockControlIDs))),
        (Expression) Expression.Property((Expression) Expression.Property((Expression) parameterExpression, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ControlProperty.get_Control))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ObjectData.get_Id)))
      })), parameterExpression)).Select<ControlProperty, string>((Expression<Func<ControlProperty, string>>) (p => p.Value)).ToArray<string>()).Count<string>();
    }

    /// <summary>
    /// Gets the count of pages that use obsolete shared content.
    /// </summary>
    /// <param name="pageProviderName">Name of the page provider.</param>
    /// <param name="contentId">The content id.</param>
    /// <returns></returns>
    [Obsolete("Do not use. Pages are always up to date with the shared content.")]
    public int GetCountOfPagesThatUseObsoleteSharedContent(string pageProviderName, Guid contentId) => this.GetPagesThatUseObsoleteSharedContent(pageProviderName, contentId).Length;

    /// <summary>
    /// Returns true if the content itemis obsolete on any page (for the current culture)
    /// </summary>
    /// <param name="pageProviderName">name of the pages provider to use or null for default</param>
    /// <param name="contentId">Id of the content item to check for</param>
    /// <param name="culture">If in monolongual, this will be ignored. Otherwize, test for a specific culture.</param>
    /// <returns>True if obsolete in any control on any page, false otherwize (and on error)</returns>
    [Obsolete("Do not use. Pages are always up to date with the shared content.")]
    public bool IsContentItemObsoleteOnAnyPage(Guid contentId, string pageProviderName) => this.IsContentItemObsoleteOnAnyPage(contentId, pageProviderName, SystemManager.CurrentContext.Culture);

    /// <summary>
    /// Returns true if the content itemis obsolete on any page
    /// </summary>
    /// <param name="pageProviderName">name of the pages provider to use or null for default</param>
    /// <param name="contentId">Id of the content item to check for</param>
    /// <param name="culture">If in monolongual, this will be ignored. Otherwize, test for a specific culture.</param>
    /// <returns>True if obsolete in any control on any page, false otherwize (and on error)</returns>
    [Obsolete("Do not use. Pages are always up to date with the shared content.")]
    public bool IsContentItemObsoleteOnAnyPage(
      Guid contentId,
      string pageProviderName,
      CultureInfo culture)
    {
      bool isMonolingual = !SystemManager.CurrentContext.AppSettings.Multilingual;
      bool isObsolete1 = false;
      string contentIdString = contentId.ToString();
      ContentItem dataItem;
      try
      {
        dataItem = this.GetContent(contentId);
      }
      catch (ItemNotFoundException ex)
      {
        dataItem = (ContentItem) null;
      }
      if (dataItem == null)
        return false;
      string contentHTML = LinkParser.UnresolveLinks(!isMonolingual ? dataItem.GetString("Content").GetString(culture, false) : dataItem.Content.Value);
      PageManager pageManager = this.GetPageManager(pageProviderName);
      bool isObsolete2 = ContentManager.DoesControlCollectionContainObsoleteContentItem((ObjectData[]) pageManager.GetControls<PageDraftControl>().Where<PageDraftControl>((Expression<Func<PageDraftControl, bool>>) (c => c.Page != default (object) && c.ObjectType == ContentManager.ContentBlockTypeName && c.Properties.Any<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == ContentManager.SharedContentIdPropertyName && p.Value == contentIdString)) && c.Properties.Any<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == ContentManager.HtmlPropertyName)))).ToArray<PageDraftControl>(), contentHTML, isMonolingual, isObsolete1, culture);
      if (!isObsolete2)
        isObsolete2 = ContentManager.DoesControlCollectionContainObsoleteContentItem((ObjectData[]) pageManager.GetControls<PageControl>().Where<PageControl>((Expression<Func<PageControl, bool>>) (c => c.Page != default (object) && c.ObjectType == ContentManager.ContentBlockTypeName && c.Properties.Any<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == ContentManager.SharedContentIdPropertyName && p.Value == contentIdString)) && c.Properties.Any<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == ContentManager.HtmlPropertyName)))).ToArray<PageControl>(), contentHTML, isMonolingual, isObsolete2, culture);
      return isObsolete2;
    }

    private static bool DoesControlCollectionContainObsoleteContentItem(
      ObjectData[] drafts,
      string contentHTML,
      bool isMonolingual,
      bool isObsolete,
      CultureInfo culture)
    {
      foreach (ObjectData draft in drafts)
      {
        ControlProperty dataItem = draft.Properties.First<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == ContentManager.HtmlPropertyName));
        string str = LinkParser.UnresolveLinks(!isMonolingual ? dataItem.GetString("MultilingualValue").GetString(culture, false) : dataItem.Value);
        if (contentHTML != str)
        {
          isObsolete = true;
          break;
        }
      }
      return isObsolete;
    }

    /// <summary>Gets page nodes that use shared content.</summary>
    /// <param name="pageProviderName">Name of the page provider.</param>
    /// <param name="contentId">The content id.</param>
    /// <returns></returns>
    [Obsolete("Use GetPagesByContent(Guid, PageManager) instead.")]
    public PageNode[] GetPagesThatUseSharedContent(string pageProviderName, Guid contentId)
    {
      CultureInfo culture = SystemManager.CurrentContext.Culture;
      return this.GetPagesThatUseSharedContent(pageProviderName, contentId, culture);
    }

    [Obsolete("Use GetPagesByContent(Guid, PageManager) instead.")]
    public PageNode[] GetPagesThatUseSharedContent(
      string pageProviderName,
      Guid contentId,
      SharedContentStatisticsMode shareMode)
    {
      return this.GetPagesThatUseSharedContent(pageProviderName, contentId, SystemManager.CurrentContext.Culture, shareMode);
    }

    [Obsolete("Use GetPagesByContent(Guid, PageManager) instead.")]
    public PageNode[] GetPagesThatUseSharedContent(
      string pageProviderName,
      Guid contentId,
      CultureInfo culture)
    {
      return this.GetPagesThatUseSharedContent(pageProviderName, contentId, culture, SharedContentStatisticsMode.ExcludeTemps);
    }

    [Obsolete("Use GetPagesByContent(Guid, PageManager) instead.")]
    public PageNode[] GetPagesThatUseSharedContent(
      string pageProviderName,
      Guid contentId,
      CultureInfo culture,
      SharedContentStatisticsMode shareMode)
    {
      ObjectData[] withSharedContent = this.GetContentBlocksWithSharedContent(pageProviderName, contentId, shareMode);
      List<PageNode> pageNodeList = new List<PageNode>();
      bool isMonoLingual = !SystemManager.CurrentContext.AppSettings.Multilingual;
      foreach (ObjectData objectData in withSharedContent)
        this.AddPageNode((IList<PageNode>) pageNodeList, objectData, culture, isMonoLingual, shareMode);
      return pageNodeList.Distinct<PageNode>().ToArray<PageNode>();
    }

    /// <summary>Gets page nodes that use shared content.</summary>
    /// <param name="pageProviderName">Name of the page provider.</param>
    /// <returns></returns>
    [Obsolete("Use GetPagesByContent(Guid, PageManager) instead.")]
    public PageNode[] GetPagesThatUseSharedContent(string pageProviderName) => this.GetPagesThatUseSharedContent(pageProviderName, Guid.Empty);

    /// <summary>Gets out dated page nodes that use shared content.</summary>
    /// <param name="pageProviderName">Name of the page provider.</param>
    /// <returns></returns>
    [Obsolete("Do not use. Pages are always up to date with the shared content.")]
    public PageNode[] GetPagesThatUseObsoleteSharedContent(string pageProviderName) => this.GetPagesThatUseObsoleteSharedContent(pageProviderName, Guid.Empty);

    /// <summary>Get all pages that use obsolete shared content</summary>
    /// <param name="pageProviderName">Name of the page provider</param>
    /// <param name="culture">Specific culture to use</param>
    /// <returns></returns>
    /// <remarks>Very slow operation. Avoid repetitive calls.</remarks>
    [Obsolete("Do not use. Pages are always up to date with the shared content.")]
    public PageNode[] GetPagesThatUseObsoleteSharedContent(
      string pageProviderName,
      CultureInfo culture)
    {
      return this.GetPagesThatUseObsoleteSharedContent(pageProviderName, Guid.Empty, culture);
    }

    /// <summary>Gets out dated page nodes that use shared content.</summary>
    /// <param name="pageProviderName">Name of the page provider.</param>
    /// <param name="contentId">The content id.</param>
    /// <returns></returns>
    [Obsolete("Do not use. Pages are always up to date with the shared content.")]
    public PageNode[] GetPagesThatUseObsoleteSharedContent(
      string pageProviderName,
      Guid contentId)
    {
      return this.GetPagesThatUseObsoleteSharedContent(pageProviderName, contentId, SystemManager.CurrentContext.Culture);
    }

    [Obsolete("Do not use. Pages are always up to date with the shared content.")]
    public PageNode[] GetPagesThatUseObsoleteSharedContent(
      string pageProviderName,
      Guid contentID,
      CultureInfo culture)
    {
      return this.GetPagesThatUseObsoleteSharedContent(pageProviderName, contentID, culture, SharedContentStatisticsMode.ExcludeTemps);
    }

    [Obsolete("Do not use. Pages are always up to date with the shared content.")]
    public PageNode[] GetPagesThatUseObsoleteSharedContent(
      string pageProviderName,
      Guid contentID,
      CultureInfo culture,
      SharedContentStatisticsMode shareMode)
    {
      return this.GetPagesThatUseContentInState(pageProviderName, contentID, culture, ContentManager.SharedContentState.Obsolete, shareMode);
    }

    /// <summary>
    ///  Gets page nodes that use shared content which is up-to-date.
    /// </summary>
    /// <param name="pageProviderName">Name of the page provider.</param>
    /// <returns></returns>
    [Obsolete("Do not use. Pages are always up to date with the shared content.")]
    public PageNode[] GetPagesThatUseUpToDateSharedContent(string pageProviderName) => this.GetPagesThatUseUpToDateSharedContent(pageProviderName, Guid.Empty);

    /// <summary>
    /// Gets page nodes that use shared content which is up-to-date.
    /// </summary>
    /// <param name="pageProviderName">Name of the page provider.</param>
    /// <param name="contentId">The content id.</param>
    [Obsolete("Do not use. Pages are always up to date with the shared content.")]
    public PageNode[] GetPagesThatUseUpToDateSharedContent(
      string pageProviderName,
      Guid contentId)
    {
      return this.GetPagesThatUseUpToDateSharedContent(pageProviderName, contentId, SystemManager.CurrentContext.Culture);
    }

    [Obsolete("Do not use. Pages are always up to date with the shared content.")]
    public PageNode[] GetPagesThatUseUpToDateSharedContent(
      string pageProviderName,
      Guid contentID,
      CultureInfo culture)
    {
      return this.GetPagesThatUseUpToDateSharedContent(pageProviderName, contentID, culture, SharedContentStatisticsMode.ExcludeTemps);
    }

    [Obsolete("Do not use. Pages are always up to date with the shared content.")]
    public PageNode[] GetPagesThatUseUpToDateSharedContent(
      string pageProviderName,
      Guid contentID,
      CultureInfo culture,
      SharedContentStatisticsMode shareMode)
    {
      return this.GetPagesThatUseContentInState(pageProviderName, contentID, culture, ContentManager.SharedContentState.UpToDate, shareMode);
    }

    /// <summary>
    /// Determines whether a content block has obsolete shared content
    /// </summary>
    /// <param name="serverSideControlID">ID of the control (i.e. System.Web.Control.ID)</param>
    /// <param name="clcStatus">Query is made only against this lifecycle state of the page</param>
    /// <returns>
    /// True if the ContentBlock.Html property has the same value as ContentItem.Content, false in any other case (incl. errors)
    /// </returns>
    /// <remarks>Uses current UI culture and default pages provider.</remarks>
    [Obsolete("Do not use. Pages are always up to date with the shared content.")]
    public bool IsContentBlockHtmlObsolete(
      string serverSideControlID,
      Guid pageId,
      ContentLifecycleStatus clcStatus)
    {
      return this.IsContentBlockHtmlObsolete(serverSideControlID, pageId, clcStatus, SystemManager.CurrentContext.Culture);
    }

    /// <summary>
    /// Determines whether a content block has obsolete shared content
    /// </summary>
    /// <param name="serverSideControlID">ID of the control (i.e. System.Web.Control.ID)</param>
    /// <param name="clcStatus">Query is made only against this lifecycle state of the page</param>
    /// <param name="culture">Query is made only against this culture</param>
    /// <returns>
    /// True if the ContentBlock.Html property has the same value as ContentItem.Content, false in any other case (incl. errors)
    /// </returns>
    /// <remarks>Uses default pages provider</remarks>
    [Obsolete("Do not use. Pages are always up to date with the shared content.")]
    public bool IsContentBlockHtmlObsolete(
      string serverSideControlID,
      Guid pageId,
      ContentLifecycleStatus clcStatus,
      CultureInfo culture)
    {
      return this.IsContentBlockHtmlObsolete(serverSideControlID, pageId, clcStatus, culture, (string) null);
    }

    /// <summary>
    /// Determines whether a content block has obsolete shared content
    /// </summary>
    /// <param name="serverSideControlID">ID of the control (i.e. System.Web.Control.ID)</param>
    /// <param name="clcStatus">Query is made only against this lifecycle state of the page</param>
    /// <param name="culture">Query is made only against this culture</param>
    /// <param name="pageProviderName">Name of the page provider to use or null for default</param>
    /// <returns>
    /// True if the ContentBlock.Html property has the same value as ContentItem.Content, false in any other case (incl. errors)
    /// </returns>
    [Obsolete("Do not use. Pages are always up to date with the shared content.")]
    public bool IsContentBlockHtmlObsolete(
      string serverSideControlID,
      Guid pageId,
      ContentLifecycleStatus clcStatus,
      CultureInfo culture,
      string pageProviderName)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      ContentManager.\u003C\u003Ec__DisplayClass77_0 cDisplayClass770 = new ContentManager.\u003C\u003Ec__DisplayClass77_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass770.pageId = pageId;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass770.serverSideControlID = serverSideControlID;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass770.culture = culture;
      PageManager pageManager = this.GetPageManager(pageProviderName);
      ObjectData objectData;
      switch (clcStatus)
      {
        case ContentLifecycleStatus.Master:
          ParameterExpression parameterExpression1;
          ParameterExpression parameterExpression2;
          ParameterExpression parameterExpression3;
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method reference
          // ISSUE: method reference
          // ISSUE: field reference
          // ISSUE: method reference
          // ISSUE: method reference
          // ISSUE: method reference
          // ISSUE: field reference
          objectData = (ObjectData) pageManager.GetControls<PageDraftControl>().Where<PageDraftControl>(Expression.Lambda<Func<PageDraftControl, bool>>((Expression) Expression.AndAlso((Expression) Expression.AndAlso(pdc.ObjectType == ContentManager.ContentBlockTypeName && pdc.Page.ParentId == cDisplayClass770.pageId && pdc.Page.IsTempDraft == false, (Expression) Expression.Call((Expression) null, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Enumerable.Any)), new Expression[2]
          {
            pdc.Properties,
            (Expression) Expression.Lambda<Func<ControlProperty, bool>>((Expression) Expression.AndAlso(p.Name == ContentManager.SharedContentIdPropertyName, (Expression) Expression.NotEqual((Expression) Expression.Call(p.Value, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.ToString)), Array.Empty<Expression>()), (Expression) Expression.Field((Expression) null, FieldInfo.GetFieldFromHandle(__fieldref (ContentManager.EmptyGuidString))))), parameterExpression2)
          })), (Expression) Expression.Call((Expression) null, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Enumerable.Any)), new Expression[2]
          {
            (Expression) Expression.Property((Expression) parameterExpression1, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ObjectData.get_Properties))),
            (Expression) Expression.Lambda<Func<ControlProperty, bool>>((Expression) Expression.AndAlso(p.Name == "ID", (Expression) Expression.Equal((Expression) Expression.Call(p.Value, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.ToString)), Array.Empty<Expression>()), (Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass770, typeof (ContentManager.\u003C\u003Ec__DisplayClass77_0)), FieldInfo.GetFieldFromHandle(__fieldref (ContentManager.\u003C\u003Ec__DisplayClass77_0.serverSideControlID))))), parameterExpression3)
          })), parameterExpression1)).SingleOrDefault<PageDraftControl>();
          break;
        case ContentLifecycleStatus.Temp:
          ParameterExpression parameterExpression4;
          ParameterExpression parameterExpression5;
          ParameterExpression parameterExpression6;
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method reference
          // ISSUE: method reference
          // ISSUE: field reference
          // ISSUE: method reference
          // ISSUE: method reference
          // ISSUE: method reference
          // ISSUE: field reference
          objectData = (ObjectData) pageManager.GetControls<PageDraftControl>().Where<PageDraftControl>(Expression.Lambda<Func<PageDraftControl, bool>>((Expression) Expression.AndAlso((Expression) Expression.AndAlso(pdc.ObjectType == ContentManager.ContentBlockTypeName && pdc.Page.ParentId == cDisplayClass770.pageId && pdc.Page.IsTempDraft == true, (Expression) Expression.Call((Expression) null, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Enumerable.Any)), new Expression[2]
          {
            pdc.Properties,
            (Expression) Expression.Lambda<Func<ControlProperty, bool>>((Expression) Expression.AndAlso(p.Name == ContentManager.SharedContentIdPropertyName, (Expression) Expression.NotEqual((Expression) Expression.Call(p.Value, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.ToString)), Array.Empty<Expression>()), (Expression) Expression.Field((Expression) null, FieldInfo.GetFieldFromHandle(__fieldref (ContentManager.EmptyGuidString))))), parameterExpression5)
          })), (Expression) Expression.Call((Expression) null, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Enumerable.Any)), new Expression[2]
          {
            (Expression) Expression.Property((Expression) parameterExpression4, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ObjectData.get_Properties))),
            (Expression) Expression.Lambda<Func<ControlProperty, bool>>((Expression) Expression.AndAlso(p.Name == "ID", (Expression) Expression.Equal((Expression) Expression.Call(p.Value, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.ToString)), Array.Empty<Expression>()), (Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass770, typeof (ContentManager.\u003C\u003Ec__DisplayClass77_0)), FieldInfo.GetFieldFromHandle(__fieldref (ContentManager.\u003C\u003Ec__DisplayClass77_0.serverSideControlID))))), parameterExpression6)
          })), parameterExpression4)).SingleOrDefault<PageDraftControl>();
          break;
        case ContentLifecycleStatus.Live:
          ParameterExpression parameterExpression7;
          ParameterExpression parameterExpression8;
          ParameterExpression parameterExpression9;
          // ISSUE: reference to a compiler-generated field
          // ISSUE: method reference
          // ISSUE: method reference
          // ISSUE: field reference
          // ISSUE: method reference
          // ISSUE: method reference
          // ISSUE: method reference
          // ISSUE: field reference
          objectData = (ObjectData) pageManager.GetControls<PageControl>().Where<PageControl>(Expression.Lambda<Func<PageControl, bool>>((Expression) Expression.AndAlso((Expression) Expression.AndAlso(pc.ObjectType == ContentManager.ContentBlockTypeName && pc.Id == cDisplayClass770.pageId, (Expression) Expression.Call((Expression) null, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Enumerable.Any)), new Expression[2]
          {
            pc.Properties,
            (Expression) Expression.Lambda<Func<ControlProperty, bool>>((Expression) Expression.AndAlso(p.Name == ContentManager.SharedContentIdPropertyName, (Expression) Expression.NotEqual((Expression) Expression.Call(p.Value, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.ToString)), Array.Empty<Expression>()), (Expression) Expression.Field((Expression) null, FieldInfo.GetFieldFromHandle(__fieldref (ContentManager.EmptyGuidString))))), parameterExpression8)
          })), (Expression) Expression.Call((Expression) null, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Enumerable.Any)), new Expression[2]
          {
            (Expression) Expression.Property((Expression) parameterExpression7, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ObjectData.get_Properties))),
            (Expression) Expression.Lambda<Func<ControlProperty, bool>>((Expression) Expression.AndAlso(p.Name == "ID", (Expression) Expression.Equal((Expression) Expression.Call(p.Value, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.ToString)), Array.Empty<Expression>()), (Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass770, typeof (ContentManager.\u003C\u003Ec__DisplayClass77_0)), FieldInfo.GetFieldFromHandle(__fieldref (ContentManager.\u003C\u003Ec__DisplayClass77_0.serverSideControlID))))), parameterExpression9)
          })), parameterExpression7)).SingleOrDefault<PageControl>();
          break;
        default:
          throw new NotImplementedException();
      }
      // ISSUE: reference to a compiler-generated field
      if (objectData == null || !Guid.TryParse(objectData.Properties.Where<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == ContentManager.SharedContentIdPropertyName)).Select<ControlProperty, string>((Func<ControlProperty, string>) (p => p.Value)).First<string>(), out cDisplayClass770.sharedContentID))
        return false;
      // ISSUE: reference to a compiler-generated method
      ControlProperty controlProperty = objectData.Properties.Where<ControlProperty>(new Func<ControlProperty, bool>(cDisplayClass770.\u003CIsContentBlockHtmlObsolete\u003Eb__5)).FirstOrDefault<ControlProperty>();
      if (controlProperty == null)
        return false;
      // ISSUE: reference to a compiler-generated field
      ContentItem dataItem = this.GetContent().Where<ContentItem>((Expression<Func<ContentItem, bool>>) (c => c.Id == cDisplayClass770.sharedContentID && (int) c.Status == 2)).SingleOrDefault<ContentItem>();
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      return dataItem != null && (!SystemManager.CurrentContext.AppSettings.Multilingual || ((IEnumerable<CultureInfo>) dataItem.AvailableCultures).Contains<CultureInfo>(cDisplayClass770.culture) && ((IEnumerable<CultureInfo>) ((ILocalizable) controlProperty).AvailableCultures).Contains<CultureInfo>(cDisplayClass770.culture)) && dataItem.GetString("Content")[cDisplayClass770.culture] != controlProperty.Value;
    }

    /// <summary>
    /// Returns a query of content items currently not shared on any page
    /// </summary>
    /// <returns>Queryable object of all content items not shared on a page</returns>
    /// <remarks>Uses default page provider</remarks>
    public IQueryable<ContentItem> GetContentNotUsedOnAnyPage() => this.GetContentNotUsedOnAnyPage((string) null);

    /// <summary>
    /// Returns a query of content items currently not shared on any page
    /// </summary>
    /// <param name="pageProviderName">Name of the page provider to use or null to use default</param>
    /// <returns>Queryable object of all content items not shared on a page</returns>
    public IQueryable<ContentItem> GetContentNotUsedOnAnyPage(
      string pageProviderName)
    {
      IEnumerable<Guid> itemBIds = new ContentRelationStatisticsClient().GetContentRelationSubjectIds("RelationType == \"{0}\" && SubjectType == \"{1}\"".Arrange((object) "Contains", (object) typeof (ContentItem).FullName));
      return this.GetContent().Where<ContentItem>((Expression<Func<ContentItem, bool>>) (c => (int) c.Status == 2 && !itemBIds.Contains<Guid>(c.Id)));
    }

    /// <summary>
    /// Get the number of pages that share a content item using the default pages provider
    /// </summary>
    /// <param name="contentId">ID of the shared content</param>
    /// <returns>Number of pages in the default provider</returns>
    public int GetCountOfPagesThatUseContent(Guid contentId) => this.GetCountOfPagesThatUseContent(contentId, (string) null);

    /// <summary>Get the number of pages that share a content item</summary>
    /// <param name="contentId">ID of the shared content</param>
    /// <param name="pageProviderName">Name of the pages provider to use or null for default</param>
    /// <returns>Number of pages</returns>
    public int GetCountOfPagesThatUseContent(Guid contentId, string pageProviderName)
    {
      ContentRelationStatisticsClient statisticsClient = new ContentRelationStatisticsClient();
      string filter;
      if (pageProviderName.IsNullOrEmpty())
        filter = "ObjectType == \"{0}\" && RelationType == \"{1}\" && SubjectId == \"{2}\"".Arrange((object) typeof (PageData).FullName, (object) "Contains", (object) contentId);
      else
        filter = "ObjectType == \"{0}\" && ObjectProvider == \"{1}\" && RelationType == \"{2}\" && SubjectId == \"{3}\"".Arrange((object) typeof (PageData).FullName, (object) pageProviderName, (object) "Contains", (object) contentId);
      return statisticsClient.GetContentRelationsCount(filter, "ObjectId");
    }

    /// <summary>
    /// Get the number of page templates that share a content item
    /// </summary>
    /// <param name="contentId">ID of the shared content</param>
    /// <returns>Number of pages</returns>
    internal int GetCountOfPageTemplatesThatUseContent(Guid contentId) => new ContentRelationStatisticsClient().GetContentRelationsCount("ObjectType == \"{0}\" && RelationType == \"{1}\" && SubjectId == \"{2}\"".Arrange((object) typeof (PageTemplate).FullName, (object) "Contains", (object) contentId), "ObjectId");

    /// <summary>Gets page nodes that use shared content.</summary>
    /// <param name="contentId">The content id.</param>
    /// <param name="pageManager">The page manager.</param>
    internal IEnumerable<PageNode> GetPagesByContent(
      Guid contentId,
      PageManager pageManager)
    {
      if (pageManager == null)
        throw new ArgumentNullException(nameof (pageManager));
      IEnumerable<Guid> pageDataIds = new ContentRelationStatisticsClient().GetContentRelationObjectIds("ObjectType == \"{0}\" && RelationType == \"{1}\" && SubjectId == \"{2}\"".Arrange((object) typeof (PageData).FullName, (object) "Contains", (object) contentId));
      return (IEnumerable<PageNode>) pageManager.GetPageDataList().Where<PageData>((Expression<Func<PageData, bool>>) (x => pageDataIds.Contains<Guid>(x.Id))).Select<PageData, PageNode>((Expression<Func<PageData, PageNode>>) (x => x.NavigationNode));
    }

    internal IEnumerable<PageData> GetPageDataByContent(
      Guid contentId,
      PageManager pageManager)
    {
      if (pageManager == null)
        throw new ArgumentNullException(nameof (pageManager));
      IEnumerable<Guid> pageDataIds = new ContentRelationStatisticsClient().GetContentRelationObjectIds("ObjectType == \"{0}\" && RelationType == \"{1}\" && SubjectId == \"{2}\"".Arrange((object) typeof (PageData).FullName, (object) "Contains", (object) contentId));
      return (IEnumerable<PageData>) pageManager.GetPageDataList().Where<PageData>((Expression<Func<PageData, bool>>) (x => pageDataIds.Contains<Guid>(x.Id)));
    }

    /// <summary>Gets page templates that use shared content.</summary>
    /// <param name="contentId">The content id.</param>
    /// <param name="pageManager">The page manager.</param>
    internal IEnumerable<PageTemplate> GetPageTemplatesByContent(
      Guid contentId,
      PageManager pageManager)
    {
      if (pageManager == null)
        throw new ArgumentNullException(nameof (pageManager));
      IEnumerable<Guid> pageTemplateIds = new ContentRelationStatisticsClient().GetContentRelationObjectIds("ObjectType == \"{0}\" && RelationType == \"{1}\" && SubjectId == \"{2}\"".Arrange((object) typeof (PageTemplate).FullName, (object) "Contains", (object) contentId));
      return (IEnumerable<PageTemplate>) pageManager.GetTemplates().Where<PageTemplate>((Expression<Func<PageTemplate, bool>>) (t => pageTemplateIds.Contains<Guid>(t.Id)));
    }

    /// <summary>Unshares the content on all pages.</summary>
    /// <param name="contentItemId">The content item id.</param>
    internal void UnshareContentOnAllPages(Guid contentItemId)
    {
      PageManager pageManager = this.GetPageManager((string) null);
      this.UnshareContentOnAllPages(contentItemId, pageManager);
    }

    /// <summary>Unshares the content on all pages.</summary>
    /// <param name="contentItemId">The content item id.</param>
    /// <param name="pageManager">The page manager.</param>
    internal void UnshareContentOnAllPages(Guid contentItemId, PageManager pageManager)
    {
      HashSet<Guid> controlIds = new HashSet<Guid>(new ContentRelationStatisticsClient().GetContentRelationKeys("RelationType == \"{0}\" && SubjectId == \"{1}\"".Arrange((object) "Contains", (object) contentItemId)));
      IQueryable<ControlData> queryable = pageManager.GetControls<ControlData>().Where<ControlData>((Expression<Func<ControlData, bool>>) (c => controlIds.Contains(c.Id)));
      string contentId = contentItemId.ToString();
      foreach (ControlData contentBlock in (IEnumerable<ControlData>) queryable)
      {
        this.UnshareContentBlock(contentBlock, contentId);
        ++contentBlock.Version;
        if (contentBlock is PageDraftControl pageDraftControl)
        {
          pageDraftControl.Page.ClearFlag(1);
          pageDraftControl.Page.Synced = false;
        }
      }
    }

    /// <summary>
    /// Sets the SharedContentID property to empty GUID and clears the content property of a given content block control.
    /// </summary>
    /// <param name="contentBlock">The content block control data.</param>
    /// <param name="contentId">The content item id.</param>
    private void UnshareContentBlock(ControlData contentBlock, string contentId)
    {
      foreach (ControlProperty controlProperty1 in contentBlock.Properties.Where<ControlProperty>((Func<ControlProperty, bool>) (prop => prop.Value != null && prop.Value.ToString() == contentId && prop.Name == ContentManager.SharedContentIdPropertyName)))
      {
        ControlProperty prop = controlProperty1;
        prop.Value = Guid.Empty.ToString();
        ControlProperty controlProperty2 = contentBlock.Properties.FirstOrDefault<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == "Html" && p.Language == prop.Language));
        if (controlProperty2 != null)
          controlProperty2.Value = string.Empty;
        ControlProperty controlProperty3 = contentBlock.Properties.FirstOrDefault<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == "ProviderName" && p.Language == prop.Language));
        if (controlProperty3 != null)
          controlProperty3.Value = string.Empty;
      }
    }

    /// <summary>
    /// Gets page manager in transaction if the current manager is in global transaction, or in a local
    /// transaction if the current manager is not in a global transaction
    /// </summary>
    /// <param name="providerName">Name of the page manager to use</param>
    /// <returns>Page manager that can be either in global or local transaction</returns>
    protected PageManager GetPageManager(string providerName) => this.Provider.TransactionName.IsNullOrEmpty() ? PageManager.GetManager(providerName) : PageManager.GetManager(providerName, this.Provider.TransactionName);

    private void AddPageNode(
      IList<PageNode> nodes,
      ObjectData item,
      CultureInfo culture,
      bool isMonoLingual,
      SharedContentStatisticsMode statMode)
    {
      if (nodes == null)
        nodes = (IList<PageNode>) new List<PageNode>();
      if (culture == null)
        culture = SystemManager.CurrentContext.Culture;
      PageData pageDataFromControl = this.ExtractPageDataFromControl(item, culture, isMonoLingual, statMode);
      if (pageDataFromControl == null)
        return;
      nodes.Add(pageDataFromControl.NavigationNode);
    }

    private ObjectData[] GetContentBlocksWithSharedContent(
      string pageProviderName,
      Guid contentId,
      SharedContentStatisticsMode shareMode)
    {
      PageManager pageManager = this.GetPageManager(pageProviderName);
      IQueryable<PageData> queryable1 = pageManager.GetPageDataList().Where<PageData>((Expression<Func<PageData, bool>>) (pd => pd.Controls.Any<PageControl>((Func<PageControl, bool>) (c => c.ObjectType == ContentManager.ContentBlockTypeName))));
      IQueryable<PageDraft> queryable2 = pageManager.GetDrafts<PageDraft>().Where<PageDraft>((Expression<Func<PageDraft, bool>>) (pd => pd.IsTempDraft == false && pd.Controls.Any<PageDraftControl>((Func<PageDraftControl, bool>) (c => c.ObjectType == ContentManager.ContentBlockTypeName))));
      IQueryable<PageDraft> queryable3 = pageManager.GetDrafts<PageDraft>().Where<PageDraft>((Expression<Func<PageDraft, bool>>) (pd => pd.IsTempDraft == true && pd.Controls.Any<PageDraftControl>((Func<PageDraftControl, bool>) (c => c.ObjectType == ContentManager.ContentBlockTypeName))));
      List<ObjectData> result = new List<ObjectData>();
      switch (shareMode)
      {
        case SharedContentStatisticsMode.ExcludeTemps:
          foreach (PageData pageData in (IEnumerable<PageData>) queryable1)
            ContentManager.ExtractSharedContentControls(contentId, (ObjectData[]) pageData.Controls.ToArray<PageControl>(), result);
          using (IEnumerator<PageDraft> enumerator = queryable2.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              PageDraft current = enumerator.Current;
              ContentManager.ExtractSharedContentControls(contentId, (ObjectData[]) current.Controls.ToArray<PageDraftControl>(), result);
            }
            break;
          }
        case SharedContentStatisticsMode.ZoneEditor:
          foreach (PageData pageData in (IEnumerable<PageData>) queryable1)
            ContentManager.ExtractSharedContentControls(contentId, (ObjectData[]) pageData.Controls.ToArray<PageControl>(), result);
          foreach (PageDraft pageDraft in (IEnumerable<PageDraft>) queryable2)
            ContentManager.ExtractSharedContentControls(contentId, (ObjectData[]) pageDraft.Controls.ToArray<PageDraftControl>(), result);
          using (IEnumerator<PageDraft> enumerator = queryable3.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              PageDraft current = enumerator.Current;
              ContentManager.ExtractSharedContentControls(contentId, (ObjectData[]) current.Controls.ToArray<PageDraftControl>(), result);
            }
            break;
          }
        case SharedContentStatisticsMode.TempsOnly:
          using (IEnumerator<PageDraft> enumerator = queryable3.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              PageDraft current = enumerator.Current;
              ContentManager.ExtractSharedContentControls(contentId, (ObjectData[]) current.Controls.ToArray<PageDraftControl>(), result);
            }
            break;
          }
      }
      return result.ToArray();
    }

    private static void ExtractSharedContentControls(
      Guid contentId,
      ObjectData[] controls,
      List<ObjectData> result)
    {
      foreach (ObjectData control in controls)
      {
        Guid guid = control.Properties.Where<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == ContentManager.SharedContentIdPropertyName && p.Value.ToString() != Guid.Empty.ToString())).Select<ControlProperty, Guid>((Func<ControlProperty, Guid>) (p => new Guid(p.Value))).FirstOrDefault<Guid>();
        if (guid != Guid.Empty && (contentId == Guid.Empty || contentId == guid))
          result.Add(control);
      }
    }

    private PageData ExtractPageDataFromControl(
      ObjectData control,
      CultureInfo culture,
      bool isMonoLingual,
      SharedContentStatisticsMode statMode)
    {
      switch (control)
      {
        case PageControl pageControl when pageControl.Page != null && pageControl.Page.NavigationNode != null && this.IsPageDataDedicatedToCulture(pageControl.Page, culture, isMonoLingual):
          return pageControl.Page;
        case PageDraftControl pageDraftControl when pageDraftControl.Page != null && pageDraftControl.Page.ParentPage != null && pageDraftControl.Page.ParentPage.NavigationNode != null && this.IsPageDataDedicatedToCulture(pageDraftControl.Page.ParentPage, culture, isMonoLingual):
          switch (statMode)
          {
            case SharedContentStatisticsMode.ExcludeTemps:
              if (!pageDraftControl.Page.IsTempDraft)
                return pageDraftControl.Page.ParentPage;
              break;
            case SharedContentStatisticsMode.ZoneEditor:
              return pageDraftControl.Page.ParentPage;
            case SharedContentStatisticsMode.TempsOnly:
              if (pageDraftControl.Page.IsTempDraft)
                return pageDraftControl.Page.ParentPage;
              break;
            default:
              throw new NotImplementedException();
          }
          break;
      }
      return (PageData) null;
    }

    private bool IsPageDataDedicatedToCulture(
      PageData page,
      CultureInfo culture,
      bool isMonoLingual)
    {
      if (isMonoLingual)
        return true;
      if (page.NavigationNode.LocalizationStrategy == LocalizationStrategy.Split)
        return CultureInfo.GetCultureInfo(page.Culture).Equals((object) culture);
      return page.NavigationNode.LocalizationStrategy == LocalizationStrategy.NotSelected && ((IEnumerable<CultureInfo>) page.NavigationNode.AvailableCultures).Count<CultureInfo>() == 0 || ((IEnumerable<CultureInfo>) page.NavigationNode.AvailableCultures).Contains<CultureInfo>(culture);
    }

    [Obsolete("Do not use. Pages are always up to date with the shared content.")]
    protected virtual PageNode[] GetPagesThatUseContentInState(
      string pageProviderName,
      Guid contentID,
      CultureInfo culture,
      ContentManager.SharedContentState state,
      SharedContentStatisticsMode shareMode)
    {
      if (culture == null)
        throw new ArgumentNullException(nameof (culture));
      ContentItem dataItem1;
      if (contentID != Guid.Empty)
      {
        dataItem1 = this.GetContent().Where<ContentItem>((Expression<Func<ContentItem, bool>>) (c => c.Id == contentID && (int) c.Status == 2)).SingleOrDefault<ContentItem>();
        if (dataItem1 == null)
          return Enumerable.Empty<PageNode>().ToArray<PageNode>();
      }
      else
        dataItem1 = (ContentItem) null;
      bool isMonoLingual = !SystemManager.CurrentContext.AppSettings.Multilingual;
      List<PageNode> source1 = new List<PageNode>();
      List<PageNode> draftPagesResult = new List<PageNode>();
      List<PageNode> tempPagesResult = new List<PageNode>();
      string str1 = LinkParser.UnresolveLinks(!isMonoLingual ? dataItem1.GetString("Content").GetString(culture, false) : dataItem1.Content.Value);
      foreach (ObjectData control in this.GetContentBlocksWithSharedContent(pageProviderName, contentID, shareMode))
      {
        PageData pageDataFromControl = this.ExtractPageDataFromControl(control, culture, isMonoLingual, shareMode);
        if (pageDataFromControl != null)
        {
          ControlProperty dataItem2 = control.Properties.FirstOrDefault<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == ContentManager.HtmlPropertyName));
          if (contentID == Guid.Empty)
          {
            Guid id = new Guid(control.Properties.First<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == ContentManager.SharedContentIdPropertyName)).Value);
            dataItem1 = this.GetContent().Where<ContentItem>((Expression<Func<ContentItem, bool>>) (c => (int) c.Status == 2 && c.Id == id)).SingleOrDefault<ContentItem>();
            if (dataItem1 == null)
              continue;
          }
          string str2 = string.Empty;
          if (dataItem2 != null)
          {
            string html;
            if (isMonoLingual)
            {
              html = dataItem2.Value;
              if (contentID == Guid.Empty)
                str1 = LinkParser.UnresolveLinks(dataItem1.Content.Value);
            }
            else
            {
              html = dataItem2.GetString("MultilingualValue").GetString(culture, false);
              if (contentID == Guid.Empty)
                str1 = dataItem1.GetString("Content").GetString(culture, false);
            }
            str2 = LinkParser.UnresolveLinks(html);
          }
          PageNode pageNode = (PageNode) null;
          if (state != ContentManager.SharedContentState.Obsolete)
          {
            if (state != ContentManager.SharedContentState.UpToDate)
              throw new NotImplementedException();
            if (str1 == str2)
              pageNode = pageDataFromControl.NavigationNode;
          }
          else if (str1 != str2)
            pageNode = pageDataFromControl.NavigationNode;
          if (pageNode != null)
          {
            if (control is PageControl)
              source1.Add(pageNode);
            if (control is PageDraftControl ctrl)
            {
              if (ctrl.Page.IsTempDraft)
              {
                tempPagesResult.Add(pageNode);
              }
              else
              {
                draftPagesResult.Add(pageNode);
                if (this.IsTempDraft(ctrl))
                  tempPagesResult.Add(pageNode);
              }
            }
          }
        }
      }
      List<PageNode> source2 = new List<PageNode>(source1.Distinct<PageNode>());
      draftPagesResult = new List<PageNode>(draftPagesResult.Distinct<PageNode>());
      tempPagesResult = new List<PageNode>(tempPagesResult.Distinct<PageNode>());
      switch (shareMode)
      {
        case SharedContentStatisticsMode.ExcludeTemps:
          List<PageNode> pageNodeList = new List<PageNode>();
          pageNodeList.AddRange((IEnumerable<PageNode>) draftPagesResult);
          foreach (PageNode pageNode in source2)
          {
            if (((pageNode.GetPageData().Drafts.Count == 0 ? 1 : 0) | (pageNode.GetPageData().Drafts.FirstOrDefault<PageDraft>((Func<PageDraft, bool>) (d => !d.IsTempDraft)) == null ? (false ? 1 : 0) : (draftPagesResult.Contains(pageNode) ? 1 : 0))) != 0 && !pageNodeList.Contains(pageNode))
              pageNodeList.Add(pageNode);
          }
          return pageNodeList.ToArray();
        case SharedContentStatisticsMode.ZoneEditor:
          IEnumerable<PageNode> second1 = source2.Where<PageNode>((Func<PageNode, bool>) (item => tempPagesResult.Any<PageNode>((Func<PageNode, bool>) (tp => tp.GetPageData().Id == item.Id)) || draftPagesResult.Any<PageNode>((Func<PageNode, bool>) (tp => tp.GetPageData().Id == item.Id))));
          IEnumerable<PageNode> second2 = draftPagesResult.Where<PageNode>((Func<PageNode, bool>) (item => tempPagesResult.Any<PageNode>((Func<PageNode, bool>) (tp => tp.GetPageData().Id == item.Id))));
          return tempPagesResult.Union<PageNode>(second1).Union<PageNode>(second2).Distinct<PageNode>().ToArray<PageNode>();
        case SharedContentStatisticsMode.TempsOnly:
          return tempPagesResult.Distinct<PageNode>().ToArray<PageNode>();
        default:
          throw new NotImplementedException();
      }
    }

    protected virtual bool IsTempDraft(PageDraftControl ctrl) => this.IsTempDraft(ctrl.Page);

    protected virtual bool IsTempDraft(PageDraft page)
    {
      if (page.IsTempDraft)
        return true;
      return page.ParentPage.Drafts.FirstOrDefault<PageDraft>((Func<PageDraft, bool>) (d => d.IsTempDraft)) == null && page.ParentPage.Drafts.Count <= 1;
    }

    /// <summary>Gets the lifecycle decorator</summary>
    /// <value>The lifecycle.</value>
    public ILifecycleDecorator Lifecycle => LifecycleFactory.CreateLifecycle<ContentItem>((ILifecycleManager) this, new Action<Content, Content>(this.Copy));

    /// <summary>Creates a language data instance</summary>
    /// <returns></returns>
    public LanguageData CreateLanguageData() => this.Provider.CreateLanguageData();

    /// <summary>Creates a language data instance</summary>
    /// <param name="id">The id.</param>
    /// <returns></returns>
    public LanguageData CreateLanguageData(Guid id) => this.Provider.CreateLanguageData(id);

    /// <summary>Gets language data instance by its Id</summary>
    /// <param name="id">The id.</param>
    /// <returns></returns>
    public LanguageData GetLanguageData(Guid id) => this.Provider.GetLanguageData(id);

    /// <summary>
    /// Executes before flushing or committing a Content transactions and records which content relations should be updated.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="args">The <see cref="T:Telerik.Sitefinity.Data.ExecutingEventArgs" /> instance containing the event data.</param>
    private static void Provider_Executing(object sender, ExecutingEventArgs args)
    {
      if (!(args.CommandName == "CommitTransaction") && !(args.CommandName == "FlushTransaction"))
        return;
      ContentDataProvider contentDataProvider = sender as ContentDataProvider;
      if (!(contentDataProvider.GetExecutionStateData("remove-content-relations") is List<Guid> data))
        data = new List<Guid>();
      foreach (object dirtyItem in (IEnumerable) contentDataProvider.GetDirtyItems())
      {
        if (contentDataProvider.GetDirtyItemStatus(dirtyItem) == SecurityConstants.TransactionActionType.Deleted && dirtyItem is ContentItem contentItem && contentItem.Status == ContentLifecycleStatus.Live)
          data.Add(contentItem.Id);
      }
      contentDataProvider.SetExecutionStateData("remove-content-relations", (object) data);
    }

    /// <summary>
    /// Handles the post commit event of the Content provider and updates the content relations if necessary
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="args">The <see cref="T:Telerik.Sitefinity.Data.ExecutedEventArgs" /> instance containing the event data.</param>
    private static void Provider_Executed(object sender, ExecutedEventArgs args)
    {
      if (args.CommandName != "CommitTransaction")
        return;
      ContentDataProvider contentDataProvider = sender as ContentDataProvider;
      List<Guid> executionStateData = contentDataProvider.GetExecutionStateData("remove-content-relations") as List<Guid>;
      contentDataProvider.SetExecutionStateData("remove-content-relations", (object) null);
      if (executionStateData == null || executionStateData.Count <= 0)
        return;
      ContentRelationStatisticsClient statisticsClient = new ContentRelationStatisticsClient();
      foreach (Guid deletedItemId in executionStateData)
        statisticsClient.RemoveAffectedContentRelations(deletedItemId);
    }

    /// <inheritdoc />
    protected override Type GetConfigType() => typeof (ContentConfig);

    /// <inheritdoc />
    public override string ProviderNameDefaultPrefix => "contentProvider";

    /// <summary>
    /// Defines how the GetPagesThatUseContentInState method will work
    /// </summary>
    [Obsolete("Do not use. Pages are always up to date with the shared content.")]
    protected enum SharedContentState
    {
      /// <summary>Get stats for obsolete content</summary>
      Obsolete,
      /// <summary>Get stats for up-to-date content</summary>
      UpToDate,
    }
  }
}
