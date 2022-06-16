// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.ContentDataProviderBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Decorators;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Licensing;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.Modules.GenericContent.Archive;
using Telerik.Sitefinity.Modules.GenericContent.Data;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UrlEvaluation;

namespace Telerik.Sitefinity.Modules.GenericContent
{
  /// <summary>
  /// Represents base class for Generic Content data providers.
  /// </summary>
  public abstract class ContentDataProviderBase : 
    UrlDataProviderBase,
    IContentFilterProvider,
    IOrganizableProvider,
    IHierarchyProvider,
    IDataEventProvider,
    IRelatedDataSource
  {
    /// <summary>
    /// Represents the method that compares two objects of <see cref="T:Telerik.Sitefinity.Modules.GenericContent.Archive.ArchiveItem" /> type by date.
    /// </summary>
    public static Comparison<ArchiveItem> DateDescendingComparison = (Comparison<ArchiveItem>) ((x, y) => y.Date.CompareTo(x.Date));
    private IContentProviderDecorator contentDecorator;
    private string[] supportedPermissionSets = new string[2]
    {
      "General",
      "SitemapGeneration"
    };
    internal const string enableCommentsBackwardCompatibility = "enableCommentsBackwardCompatibility";
    private static object archiveItemsSync = new object();

    /// <summary>
    /// Gets or sets a value indicating whether native comments are enabled.
    /// </summary>
    /// <value>
    /// <c>true</c> if [are native comments enabled]; otherwise, <c>false</c>.
    /// </value>
    internal virtual bool EnableCommentsBackwardCompatibility { get; set; }

    /// <summary>
    /// Override this method in order to return the type of the Parent object of the specified content type.
    /// If the type has no parent type, return null.
    /// </summary>
    /// <param name="contentType">Type of the content.</param>
    /// <returns></returns>
    [Obsolete("Use GetParentType method instead.")]
    public virtual Type GetParentTypeFor(Type contentType) => (Type) null;

    /// <summary>Gets the items by taxon.</summary>
    /// <param name="taxonId">The taxon id.</param>
    /// <param name="isSingleTaxon">A value indicating if it is a single taxon.</param>
    /// <param name="propertyName">Name of the property.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="orderExpression">The order expression.</param>
    /// <param name="skip">Items to skip.</param>
    /// <param name="take">Items to take.</param>
    /// <param name="totalCount">The total count.</param>
    /// <returns></returns>
    public abstract IEnumerable GetItemsByTaxon(
      Guid taxonId,
      bool isSingleTaxon,
      string propertyName,
      Type itemType,
      string filterExpression,
      string orderExpression,
      int skip,
      int take,
      ref int? totalCount);

    protected internal override string GetUrlPart<T>(string key, string format, T item) => key == "Owner" ? this.GetItemOwner((IDataItem) item) : base.GetUrlPart<T>(key, format, item);

    /// <summary>
    /// Blocks the comments from the user with the specified email.
    /// </summary>
    /// <param name="email">The email.</param>
    [Obsolete("This feature is not available.")]
    public virtual void BlockCommentsForEmail(string email)
    {
      if (this.contentDecorator == null)
        throw new MissingDecoratorException((DataProviderBase) this, "BlockCommentsForEmail(string email)");
      this.contentDecorator.BlockCommentsForEmail(email);
    }

    /// <summary>Blocks the comments coming from the given IP</summary>
    /// <param name="ip">The ip.</param>
    [Obsolete("This feature is not available.")]
    public virtual void BlockCommentsForIP(string ip)
    {
      if (this.contentDecorator == null)
        throw new MissingDecoratorException((DataProviderBase) this, "BlockCommentsForIP(string ip)");
      this.contentDecorator.BlockCommentsForIP(ip);
    }

    /// <summary>Creates a comment for the specified commented item</summary>
    /// <param name="commentedItem">The commented item.</param>
    /// <returns></returns>
    [Obsolete("Use SystemManager.GetCommentService().CreateComment")]
    public virtual Comment CreateComment(ICommentable commentedItem) => this.CreateComment(commentedItem, this.GetNewGuid());

    /// <summary>Creates a comment for the specified commented item</summary>
    /// <param name="commentedItem">The commented item.</param>
    /// <param name="commentId">The comment item id.</param>
    /// <returns></returns>
    [Obsolete("Use SystemManager.GetCommentService().CreateComment")]
    public virtual Comment CreateComment(ICommentable commentedItem, Guid commentId)
    {
      if (this.contentDecorator == null)
        throw new MissingDecoratorException((DataProviderBase) this, "CreateComment(ICommentable commentedItem)");
      return this.contentDecorator.CreateComment(commentedItem, commentId);
    }

    /// <summary>
    /// Creates a comment for the commented item with the given type and pageId.
    /// </summary>
    /// <param name="commentedItemType">Type of the commented item.</param>
    /// <param name="commentedItemId">The commented item pageId.</param>
    /// <param name="commentId">The comment item id.</param>
    /// <returns></returns>
    [Obsolete("Use SystemManager.GetCommentService().CreateComment")]
    public Comment CreateComment(
      Type commentedItemType,
      Guid commentedItemId,
      Guid commentId)
    {
      IManager managerInTransaction = ManagerBase.GetMappedManagerInTransaction(commentedItemType, this.Name, this.TransactionName);
      ICommentsManager commentsManager = managerInTransaction as ICommentsManager;
      if (managerInTransaction == null)
        throw new InvalidCastException(string.Format("Manager of type {0} must implement {1} in order to create comments", (object) managerInTransaction.GetType().FullName, (object) typeof (ICommentsManager).FullName));
      Type itemType = commentedItemType;
      Guid itemId = commentedItemId;
      return this.CreateComment(commentsManager.GetCommentedItem(itemType, itemId), commentId);
    }

    /// <summary>
    /// Creates a comment for the commented item with the given type and pageId.
    /// </summary>
    /// <param name="commentedItemType">Type of the commented item.</param>
    /// <param name="commentedItemId">The commented item pageId.</param>
    /// <returns></returns>
    [Obsolete("Use SystemManager.GetCommentService().CreateComment")]
    public virtual Comment CreateComment(Type commentedItemType, Guid commentedItemId) => this.CreateComment(commentedItemType, commentedItemId, this.GetNewGuid());

    /// <summary>Deletes the specified comment.</summary>
    /// <param name="comment">The comment.</param>
    /// <returns></returns>
    [Obsolete("Use SystemManager.GetCommentService().DeleteComment")]
    public virtual void Delete(Comment comment)
    {
      if (this.contentDecorator == null)
        throw new MissingDecoratorException((DataProviderBase) this, "Delete(Comment comment)");
      this.contentDecorator.Delete(comment);
    }

    /// <summary>Deletes the specified comment.</summary>
    /// <param name="comment">The comment.</param>
    /// <returns></returns>
    [Obsolete("Use SystemManager.GetCommentService().DeleteComment")]
    internal virtual void Delete_Old(Comment comment)
    {
      if (!(this.contentDecorator is OpenAccessContentDecorator contentDecorator))
        return;
      contentDecorator.Delete_Old(comment);
    }

    /// <summary>Deletes all comments made for an item.</summary>
    /// <param name="commentedItemType">Type of the commented item.</param>
    /// <param name="commentedItemId">The commented item id.</param>
    [Obsolete("Use SystemManager.GetCommentService().DeleteComment")]
    public virtual void DeleteItemComments(Type commentedItemType, Guid commentedItemId)
    {
      string typeName = commentedItemType.FullName;
      IQueryable<Comment> comments = this.GetComments(commentedItemId);
      Expression<Func<Comment, bool>> predicate = (Expression<Func<Comment, bool>>) (c => c.CommentedItemType == typeName);
      foreach (Comment comment in (IEnumerable<Comment>) comments.Where<Comment>(predicate))
        this.Delete(comment);
    }

    /// <summary>Gets a comment by the specified pageId.</summary>
    /// <param name="commentId">The comment pageId.</param>
    /// <returns></returns>
    [Obsolete("Use SystemManager.GetCommentService().GetComment")]
    public virtual Comment GetComment(Guid commentId) => this.contentDecorator != null ? this.contentDecorator.GetComment(commentId) : throw new MissingDecoratorException((DataProviderBase) this, "GetComment(Guid commentId)");

    /// <summary>Gets an IQueryable of comments.</summary>
    /// <returns></returns>
    [Obsolete("Use SystemManager.GetCommentService().GetComments")]
    public virtual IQueryable<Comment> GetComments() => this.contentDecorator != null ? this.contentDecorator.GetComments() : throw new MissingDecoratorException((DataProviderBase) this, "GetComments()");

    /// <summary>Gets the old comments.</summary>
    /// <returns></returns>
    [Obsolete("Use SystemManager.GetCommentService().GetComments")]
    internal IQueryable<Comment> GetComments_Old() => this.contentDecorator is OpenAccessContentDecorator contentDecorator ? contentDecorator.GetComments_Old() : (IQueryable<Comment>) null;

    /// <summary>Gets an IQueryable of comments.</summary>
    /// <param name="commentableId">The commented item id.</param>
    /// <returns></returns>
    [Obsolete("Use SystemManager.GetCommentService().GetComments")]
    public virtual IQueryable<Comment> GetComments(Guid commentableId) => this.contentDecorator != null ? this.contentDecorator.GetComments(commentableId) : throw new MissingDecoratorException((DataProviderBase) this, "GetComments(Guid commentableId)");

    /// <summary>Demands the commented object permissions.</summary>
    /// <param name="commentedItem">The commented item.</param>
    /// <param name="permissionSetName">Name of the permission set.</param>
    /// <param name="actionNames">The action names.</param>
    protected internal virtual void DemandCommentedObjectPermissions(
      ICommentable commentedItem,
      string permissionSetName,
      params string[] actionNames)
    {
      if (commentedItem == null || !(commentedItem.GetType().GetInterface(typeof (ISecuredObject).FullName) != (Type) null))
        return;
      ((ISecuredObject) commentedItem).Demand(permissionSetName, actionNames);
    }

    /// <summary>
    /// Clears the content links of a content item. Note that this is not done with the same transaction that the current provider is running, since content links are managed by another provider
    /// </summary>
    /// <param name="item">The item.</param>
    public virtual void ClearContentLinks(object item)
    {
      foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(item))
      {
        if (property is ContentLinksPropertyDescriptor)
          property.SetValue(item, (object) null);
      }
    }

    /// <summary>
    /// Gets a string which represents the owner of the content item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    protected internal virtual string GetItemOwner(IDataItem item)
    {
      if (item == null)
        throw new ArgumentNullException(nameof (item));
      string itemOwner = string.Empty;
      if (item is IContent)
        itemOwner = UserProfilesHelper.GetUserDisplayName(((IOwnership) item).Owner);
      return itemOwner;
    }

    /// <summary>Gets the archieve items for a specified content type.</summary>
    /// <param name="contentType">Type of the content.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="dateBuildOptions">The date groping options for grouping a content.</param>
    /// <param name="propertyName">Property name used to build the filter expression when evaluating URLs by date.</param>
    /// <returns></returns>
    public virtual List<ArchiveItem> GetArchieveItems(
      Type contentType,
      string providerName,
      DateBuildOptions dateBuildOptions,
      string propertyName)
    {
      if (propertyName.IsNullOrEmpty())
        throw new ArgumentException("propertName cannot be empty.");
      ICacheManager cacheManager = SystemManager.GetCacheManager(CacheManagerInstance.Global);
      string key = contentType.FullName + providerName + (object) dateBuildOptions + propertyName;
      if (!(cacheManager[key] is List<ArchiveItem> archieveItemsInternal1))
      {
        lock (ContentDataProviderBase.archiveItemsSync)
        {
          if (!(cacheManager[key] is List<ArchiveItem> archieveItemsInternal1))
          {
            archieveItemsInternal1 = this.GetArchieveItemsInternal(contentType, providerName, dateBuildOptions, propertyName);
            cacheManager.Add(key, (object) archieveItemsInternal1, CacheItemPriority.Low, (ICacheItemRefreshAction) null, (ICacheItemExpiration) new DataItemCacheDependency(contentType, ContentLifecycleStatus.Live.ToString() + providerName), (ICacheItemExpiration) new SlidingTime(TimeSpan.FromMinutes(60.0)));
          }
        }
      }
      return archieveItemsInternal1;
    }

    private List<ArchiveItem> GetArchieveItemsInternal(
      Type contentType,
      string providerName,
      DateBuildOptions dateBuildOptions,
      string propertyName)
    {
      IManager mappedManager = ManagerBase.GetMappedManager(contentType, providerName);
      string str = propertyName + " ASC";
      string orderExpression = propertyName + " DESC";
      string filterExpression = DefinitionsHelper.PublishedFilterExpression;
      PropertyDescriptor dateProp = TypeDescriptor.GetProperties(contentType).Find(propertyName, true);
      if (dateProp != null)
      {
        if (!(dateProp.PropertyType == typeof (DateTime)) && !(dateProp.PropertyType == typeof (DateTime?)))
          throw new ArgumentException(string.Format("{0} property is not of type DateTime.", (object) propertyName));
        if (dateProp.PropertyType == typeof (DateTime?))
          filterExpression = filterExpression + " AND null != " + propertyName;
      }
      IEnumerable<object> source1 = mappedManager.GetItems(contentType, filterExpression, str, 0, 1).Cast<object>();
      IEnumerable<object> source2 = mappedManager.GetItems(contentType, filterExpression, orderExpression, 0, 1).Cast<object>();
      List<ArchiveItem> archieveItemsInternal = new List<ArchiveItem>();
      object component1 = source1.FirstOrDefault<object>();
      object component2 = source2.FirstOrDefault<object>();
      if (component1 != null && component2 != null)
      {
        IList<TrackedInterval> intervals = new SplitterResolver().GetIntervals((DateTime) dateProp.GetValue(component1), (DateTime) dateProp.GetValue(component2), dateBuildOptions);
        archieveItemsInternal = dateBuildOptions != DateBuildOptions.YearMonthDay ? this.GetArchieveItemsLongPeriods(intervals, mappedManager, contentType, propertyName) : this.GetArchieveItemsShortPeriods(intervals, mappedManager, contentType, str, dateProp);
      }
      return archieveItemsInternal;
    }

    /// <summary>
    /// Calculates the archive items by iterating the periods and making queries about each period. This will be faster for long periods, even if there are lots of items
    /// </summary>
    /// <param name="trackedIntervals">The calculated tracked intervals</param>
    /// <param name="manager">The content manager</param>
    /// <param name="contentType">The content type</param>
    /// <param name="propertyName">The date property</param>
    /// <returns>The archive items</returns>
    private List<ArchiveItem> GetArchieveItemsLongPeriods(
      IList<TrackedInterval> trackedIntervals,
      IManager manager,
      Type contentType,
      string propertyName)
    {
      List<ArchiveItem> itemsLongPeriods = new List<ArchiveItem>();
      foreach (TrackedInterval trackedInterval in (IEnumerable<TrackedInterval>) trackedIntervals)
      {
        string filterExpression = this.GetArchiveFilterExpression(trackedInterval.StartDate, trackedInterval.EndDate, propertyName);
        int? totalCount = new int?(0);
        manager.GetItems(contentType, filterExpression, (string) null, 0, 0, ref totalCount);
        int? nullable = totalCount;
        int num = 0;
        if (nullable.GetValueOrDefault() > num & nullable.HasValue)
          itemsLongPeriods.Add(new ArchiveItem(trackedInterval.StartDate, totalCount.Value));
      }
      itemsLongPeriods.Sort(ContentDataProviderBase.DateDescendingComparison);
      return itemsLongPeriods;
    }

    /// <summary>
    /// Calculates the archive items by iterating all of the items and distributing them in the intervals. This will be faster for short periods, containing small batches of items.
    /// </summary>
    /// <param name="trackedIntervals">The calculated tracked intervals</param>
    /// <param name="manager">The content manager</param>
    /// <param name="contentType">The content type</param>
    /// <param name="queryMinOrderExpr">The sort expression from older to newer dates</param>
    /// <param name="dateProp">The date property</param>
    /// <returns>The archive items</returns>
    private List<ArchiveItem> GetArchieveItemsShortPeriods(
      IList<TrackedInterval> trackedIntervals,
      IManager manager,
      Type contentType,
      string queryMinOrderExpr,
      PropertyDescriptor dateProp)
    {
      List<ArchiveItem> itemsShortPeriods = new List<ArchiveItem>();
      IEnumerator enumerator = manager.GetItems(contentType, DefinitionsHelper.PublishedFilterExpression, queryMinOrderExpr, 0, 0).GetEnumerator();
      bool flag = enumerator.MoveNext();
      foreach (TrackedInterval trackedInterval in (IEnumerable<TrackedInterval>) trackedIntervals)
      {
        if (flag)
        {
          int itemsCount = 0;
          while (flag && enumerator.Current != null)
          {
            DateTime? nullable = (DateTime?) dateProp.GetValue(enumerator.Current);
            if (!nullable.HasValue)
              flag = enumerator.MoveNext();
            else if (trackedInterval.StartDate <= nullable.Value && trackedInterval.EndDate > nullable.Value)
            {
              ++itemsCount;
              flag = enumerator.MoveNext();
            }
            else
              break;
          }
          if (itemsCount > 0)
            itemsShortPeriods.Add(new ArchiveItem(trackedInterval.StartDate, itemsCount));
        }
        else
          break;
      }
      itemsShortPeriods.Reverse();
      return itemsShortPeriods;
    }

    private string GetArchiveFilterExpression(
      DateTime startDate,
      DateTime endDate,
      string propertyName)
    {
      return string.Format("{0} >= ({1}) AND {2} < ({3}) AND {4}", (object) propertyName, (object) startDate.ToString("yyyy-MM-dd HH:mm:ss"), (object) propertyName, (object) endDate.ToString("yyyy-MM-dd HH:mm:ss"), (object) DefinitionsHelper.PublishedFilterExpression);
    }

    /// <summary>Initializes the specified provider name.</summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="config">The config.</param>
    /// <param name="managerType">Type of the manager.</param>
    protected internal override void Initialize(
      string providerName,
      NameValueCollection config,
      Type managerType)
    {
      base.Initialize(providerName, config, managerType);
      ContentProviderDecoratorAttribute attribute = (ContentProviderDecoratorAttribute) TypeDescriptor.GetAttributes((object) this)[typeof (ContentProviderDecoratorAttribute)];
      if (attribute != null)
      {
        this.contentDecorator = ObjectFactory.Resolve<IContentProviderDecorator>(attribute.DecoratorType.FullName);
        this.contentDecorator.DataProvider = this;
      }
      bool result = false;
      if (config.Keys.Contains("enableCommentsBackwardCompatibility"))
        bool.TryParse(config["enableCommentsBackwardCompatibility"], out result);
      this.EnableCommentsBackwardCompatibility = result;
    }

    /// <summary>
    /// Gets the permission sets relevant to this specific secured object.
    /// To be overridden by relevant providers (which involve security roots)
    /// </summary>
    /// <value>The supported permission sets.</value>
    public override string[] SupportedPermissionSets
    {
      get => this.supportedPermissionSets;
      set => this.supportedPermissionSets = value;
    }

    /// <summary>Applies the filters for the current context.</summary>
    /// <param name="content">The content.</param>
    /// <returns></returns>
    public virtual string ApplyFilters(Content content, string html) => HtmlFilterProvider.ApplyFilters(html);

    /// <summary>
    /// Checks in the content in the temp state. Content becomes master after the check in.
    /// </summary>
    /// <typeparam name="TContent">Type of the content item.</typeparam>
    /// <param name="temp">Content in temp state that is to be checked in.</param>
    /// <param name="copy">Method that copies data from <paramref name="temp" /> to the master.</param>
    /// <returns>An item in master state.</returns>
    /// <exception cref="T:System.InvalidOperationException">When the status of <paramref name="temp" /> is not temp.</exception>
    public virtual TContent CheckIn<TContent>(TContent temp, Action<TContent, TContent> copy) where TContent : Content, IContentLifecycle
    {
      TContent content = temp.Status == ContentLifecycleStatus.Temp ? (TContent) this.GetItem<TContent>(temp.OriginalContentId) : throw new InvalidOperationException(Res.Get<ContentLifecycleMessages>().TempExpectedForCheckIn);
      if (content.Status != ContentLifecycleStatus.Master)
        throw new InvalidOperationException(Res.Get<ContentLifecycleMessages>().MasterNotFound);
      SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
      if (currentIdentity == null || !currentIdentity.IsUnrestricted && !(currentIdentity.UserId == temp.Owner))
        throw new InvalidOperationException(Res.Get<ContentLifecycleMessages>().CannotCheckIn);
      copy(temp, content);
      content.PublicationDate = temp.PublicationDate;
      bool suppressSecurityChecks = this.SuppressSecurityChecks;
      this.SuppressSecurityChecks = true;
      this.DeleteItem((object) temp);
      this.SuppressSecurityChecks = suppressSecurityChecks;
      content.UIStatus = ContentUIStatus.Draft;
      content.LastModifiedBy = SecurityManager.CurrentUserId;
      return content;
    }

    /// <summary>
    /// Checks out the content in master state. Content becomes temp after the check out.
    /// </summary>
    /// <typeparam name="TContent">Type of the content item.</typeparam>
    /// <param name="master">Content in master state that is to be checked out.</param>
    /// <param name="copy">Method that copies data from <paramref name="master" /> to the temp.</param>
    /// <param name="itemsQuery">Queryable</param>
    /// <returns>A content that was checked out in temp state.</returns>
    /// <exception cref="T:System.InvalidOperationException">When the status of <paramref name="master" /> is not master.</exception>
    public TContent CheckOut<TContent>(
      TContent master,
      Action<TContent, TContent> copy,
      IQueryable<TContent> itemsQuery)
      where TContent : Content, IContentLifecycle
    {
      Guid masterId = master.Status == ContentLifecycleStatus.Master ? master.Id : throw new InvalidOperationException(Res.Get<ContentLifecycleMessages>().MasterExpectedForCheckOut);
      TContent content = Queryable.FirstOrDefault<TContent>(itemsQuery.Where<TContent>((Expression<Func<TContent, bool>>) (c => c.OriginalContentId == masterId && (int) c.Status == 1)));
      if ((object) content == null)
      {
        bool suppressSecurityChecks = this.SuppressSecurityChecks;
        this.SuppressSecurityChecks = true;
        content = (TContent) this.CreateItem(typeof (TContent));
        content.Status = ContentLifecycleStatus.Temp;
        content.OriginalContentId = masterId;
        this.SuppressSecurityChecks = suppressSecurityChecks;
      }
      else
      {
        SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
        ISecuredObject secObj = (object) master as ISecuredObject;
        if (currentIdentity == null || secObj == null || !(currentIdentity.UserId == content.Owner) && !secObj.IsSecurityActionTypeGranted(SecurityActionTypes.Unlock))
          throw new InvalidOperationException(Res.Get<ContentLifecycleMessages>().AlreadyCheckedOut);
      }
      copy(master, content);
      content.Owner = SecurityManager.CurrentUserId;
      content.PublicationDate = master.PublicationDate;
      content.UIStatus = ContentUIStatus.PrivateCopy;
      return content;
    }

    /// <summary>
    /// Edits the content in live state. Content becomes master after the edit.
    /// </summary>
    /// <typeparam name="TContent">Type of the content item.</typeparam>
    /// <param name="liveItem">Content in live state that is to be edited.</param>
    /// <param name="copy">Method that copies information from the <paramref name="liveItem" /> to the master</param>
    /// <param name="itemsQuery">Query for all <typeparamref name="TContent" /> items.</param>
    /// <returns>A content that was edited in master state.</returns>
    /// <exception cref="T:System.InvalidOperationException">
    /// When
    /// <list type="bullet">
    ///     <item>
    ///         <description>
    ///             <paramref name="liveItem" /> is not in live state, or
    ///         </description>
    ///     </item>
    ///     <item>
    ///         <description>
    ///             There is no master associated with the <paramref name="liveItem" />.
    ///         </description>
    ///     </item>
    /// </list>
    /// </exception>
    public TContent Edit<TContent>(
      TContent liveItem,
      Action<TContent, TContent> copy,
      IQueryable<TContent> itemsQuery)
      where TContent : Content, IContentLifecycle
    {
      Guid masterId = liveItem.Status == ContentLifecycleStatus.Live ? liveItem.OriginalContentId : throw new InvalidOperationException(Res.Get<ContentLifecycleMessages>().LiveExpectedForEdit);
      TContent content = Queryable.First<TContent>(itemsQuery.Where<TContent>((Expression<Func<TContent, bool>>) (c => (int) c.Status == 0 && c.Id == masterId)));
      if (content.Status != ContentLifecycleStatus.Master)
        throw new InvalidOperationException(Res.Get<ContentLifecycleMessages>().MasterNotFound);
      copy(liveItem, content);
      content.UIStatus = ContentUIStatus.Draft;
      return content;
    }

    /// <summary>
    /// Publishes the content in master state. Content becomes live after the publish.
    /// </summary>
    /// <typeparam name="TContent">Type of the content item.</typeparam>
    /// <param name="masterItem">Content in master state that is to be published.</param>
    /// <param name="copy">Method that copies information from the <paramref name="masterItem" /> to the master</param>
    /// <param name="itemsQuery">Query for all <typeparamref name="TContent" /> items.</param>
    /// <exception cref="T:System.InvalidOperationException">
    /// When <paramref name="masterItem" /> is not a master.
    /// </exception>
    /// <returns>Published content item</returns>
    public TContent Publish<TContent>(
      TContent masterItem,
      Action<TContent, TContent> copy,
      IQueryable<TContent> itemsQuery)
      where TContent : Content, IContentLifecycle
    {
      Guid masterItemId = masterItem.Status == ContentLifecycleStatus.Master ? masterItem.Id : throw new InvalidOperationException(Res.Get<ContentLifecycleMessages>().MasterExpectedForPublish);
      TContent liveItem = Queryable.FirstOrDefault<TContent>(itemsQuery.Where<TContent>((Expression<Func<TContent, bool>>) (c => c.OriginalContentId == masterItemId && (int) c.Status == 2)));
      int newItemsCount = 0;
      if ((object) liveItem == null || (object) liveItem != null && !liveItem.Visible)
        newItemsCount = 1;
      LicenseLimitations.CanSaveItems(masterItem.GetType(), newItemsCount);
      return this.ExecuteOnPublish<TContent>(masterItem, copy, liveItem);
    }

    protected virtual TContent ExecuteOnPublish<TContent>(
      TContent masterItem,
      Action<TContent, TContent> copy,
      TContent liveItem)
      where TContent : Content, IContentLifecycle
    {
      Guid guid = masterItem.Status == ContentLifecycleStatus.Master ? masterItem.Id : throw new InvalidOperationException(Res.Get<ContentLifecycleMessages>().MasterExpectedForPublish);
      if ((object) liveItem == null)
      {
        liveItem = (TContent) this.CreateItem(typeof (TContent));
        liveItem.OriginalContentId = guid;
        liveItem.Status = ContentLifecycleStatus.Live;
        liveItem.PublicationDate = DateTime.UtcNow;
      }
      else if (!liveItem.Visible)
        liveItem.PublicationDate = DateTime.UtcNow;
      copy(masterItem, liveItem);
      liveItem.UIStatus = ContentUIStatus.Published;
      liveItem.ExpirationDate = new DateTime?();
      liveItem.ContentState = (string) null;
      liveItem.Visible = true;
      liveItem.Owner = masterItem.Owner;
      liveItem.LastModifiedBy = SecurityManager.CurrentUserId;
      masterItem.PublicationDate = liveItem.PublicationDate;
      masterItem.ContentState = "PUBLISHED";
      if (liveItem is ILocatable locatable)
        this.RecompileItemUrls<ILocatable>(locatable);
      return liveItem;
    }

    /// <summary>Unpublishes a live item</summary>
    /// <typeparam name="TContent">Type of the content to unpublish</typeparam>
    /// <param name="liveItem">Live item</param>
    /// <param name="itemsQuery">Query of all <typeparamref name="TContent" /> items</param>
    /// <returns>Master state of <paramref name="liveItem" /></returns>
    public TContent Unpublish<TContent>(TContent liveItem, IQueryable<TContent> itemsQuery) where TContent : Content, IContentLifecycle
    {
      Guid masterId = liveItem.Status == ContentLifecycleStatus.Live ? liveItem.OriginalContentId : throw new InvalidOperationException(Res.Get<ContentLifecycleMessages>().LiveExpectedForUnpublish);
      TContent content = Queryable.Single<TContent>(itemsQuery.Where<TContent>((Expression<Func<TContent, bool>>) (c => c.Id == masterId)));
      content.ContentState = "";
      liveItem.Visible = false;
      return content;
    }

    /// <summary>Schedule an item to be visible on the public side</summary>
    /// <typeparam name="TContent">Type of the content item to schedule</typeparam>
    /// <param name="masterItem">Content in master state</param>
    /// <param name="publicationDate">When the item will become visible on the public side</param>
    /// <param name="expirationDate">When the item will stop being visible on the public side, null to never expire</param>
    /// <param name="copy">Method that will perform a copy from the master to the scheduled item</param>
    /// <param name="itemsQuery">Query containing all <typeparamref name="TContent" /> items</param>
    /// <exception cref="T:System.InvalidOperationException">
    /// When <paramref name="masterItem" /> is not in master state., OR
    /// when publication date is in the past, OR
    /// when expiration date is set and is before publication date
    /// </exception>
    [Obsolete]
    public TContent Schedule<TContent>(
      TContent masterItem,
      DateTime publicationDate,
      DateTime? expirationDate,
      Action<TContent, TContent> copy,
      IQueryable<TContent> itemsQuery)
      where TContent : Content, IContentLifecycle
    {
      throw new NotSupportedException("Unsupported opration");
    }

    public Guid GetCheckedOutBy<TContent>(TContent item, IQueryable<TContent> itemsQuery) where TContent : Content
    {
      TContent tempContent = this.GetTempContent<TContent>(item.Id, itemsQuery);
      return (object) tempContent != null ? tempContent.Owner : Guid.Empty;
    }

    public bool IsCheckedOut<TContent>(TContent item, IQueryable<TContent> itemsQuery) where TContent : Content => (object) this.GetTempContent<TContent>(item.Id, itemsQuery) != null;

    public bool IsCheckedOutBy<TContent>(
      TContent item,
      Guid userId,
      IQueryable<TContent> itemsQuery)
      where TContent : Content
    {
      TContent tempContent = this.GetTempContent<TContent>(item.Id, itemsQuery);
      return (object) tempContent != null && tempContent.Owner == userId;
    }

    private TContent GetTempContent<TContent>(Guid itemId, IQueryable<TContent> itemsQuery) where TContent : Content => Queryable.SingleOrDefault<TContent>(itemsQuery.Where<TContent>((Expression<Func<TContent, bool>>) (c => (int) c.Status == 1 && c.OriginalContentId == itemId)));

    /// <summary>
    /// Deletes all redundant content lifecycle objects related to <paramref name="item" />
    /// </summary>
    /// <typeparam name="TContent">Type of the content item</typeparam>
    /// <param name="item">Content item that is to be deleted</param>
    /// <param name="itemsQuery">Query of all <typeparamref name="TContent" /> managed by this provider.</param>
    /// <remarks>
    /// Call this method in your delete methods for every item that is to be removed.
    /// </remarks>
    protected virtual void ClearLifecycle<TContent>(TContent item, IQueryable<TContent> itemsQuery) where TContent : Content
    {
      if ((item.Status == ContentLifecycleStatus.Master ? 1 : (item.Status == ContentLifecycleStatus.Deleted ? 1 : 0)) == 0)
        return;
      Guid id = item.Id;
      IQueryable<TContent> source = itemsQuery;
      Expression<Func<TContent, bool>> predicate = (Expression<Func<TContent, bool>>) (c => (int) c.Status != (int) item.Status && c.OriginalContentId == id);
      foreach (TContent content in (IEnumerable<TContent>) source.Where<TContent>(predicate))
      {
        content.Owner = item.Owner;
        this.DeleteItem((object) content);
      }
    }

    /// <summary>
    /// Accepts a content item and returns an item in master (draft) state
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
    public virtual TContent GetMasterBase<TContent>(TContent cnt) where TContent : Content => (TContent) this.GetContentMasterBase((Content) cnt);

    /// <summary>
    /// Accepts a content item and returns an item in master (draft) state
    /// </summary>
    /// <param name="cnt">Content item whose master to get</param>
    /// <returns>
    /// If <paramref name="cnt" /> is master itself, returns cnt.
    /// Otherwize, looks up the master associated with <paramref name="cnt" /> and returns it.
    /// When there is no master, an exception will be thrown.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">When no master can be found for <paramref name="cnt" />.</exception>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="cnt" /> is <c>null</c>.</exception>
    public Content GetContentMasterBase(Content cnt) => (Content) this.GetContentMasterBase((IDataItem) cnt);

    /// <summary>
    /// Get a temp for <paramref name="cnt" />, if it exists.
    /// </summary>
    /// <typeparam name="TContent">Type of the content item</typeparam>
    /// <param name="cnt">Content item to get a temp for</param>
    /// <returns>Temp version of <paramref name="cnt" />, if it exists.</returns>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="cnt" /> is <c>null</c>.</exception>
    public virtual TContent GetTempBase<TContent>(TContent cnt) where TContent : Content
    {
      if ((object) cnt == null)
        throw new ArgumentNullException(nameof (cnt));
      if (cnt.Status != ContentLifecycleStatus.Temp)
      {
        Guid id = Guid.Empty;
        id = cnt.Status != ContentLifecycleStatus.Master ? cnt.OriginalContentId : cnt.Id;
        cnt = Queryable.FirstOrDefault<TContent>(((IQueryable<TContent>) this.GetItems(typeof (TContent), (string) null, (string) null, 0, 0)).Where<TContent>((Expression<Func<TContent, bool>>) (c => (int) c.Status == 1 && c.OriginalContentId == id)));
      }
      return cnt;
    }

    /// <summary>
    /// Gets the public (live) version of <paramref name="cnt" />, if it exists
    /// </summary>
    /// <param name="cnt">Type of the content item</param>
    /// <returns>Public (live) version of <paramref name="cnt" />, if it exists</returns>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="cnt" /> is <c>null</c>.</exception>
    /// <remarks>If there </remarks>
    public virtual TContent GetLiveBase<TContent>(TContent cnt) where TContent : Content
    {
      if ((object) cnt == null)
        throw new ArgumentNullException(nameof (cnt));
      if (cnt.Status != ContentLifecycleStatus.Live)
      {
        IQueryable<TContent> items = (IQueryable<TContent>) this.GetItems(typeof (TContent), (string) null, (string) null, 0, 0);
        Guid id = cnt.Status != ContentLifecycleStatus.Master ? cnt.OriginalContentId : cnt.Id;
        Expression<Func<TContent, bool>> predicate = (Expression<Func<TContent, bool>>) (c => (int) c.Status == 2 && c.OriginalContentId == id && c.Visible == true);
        cnt = Queryable.FirstOrDefault<TContent>(items.Where<TContent>(predicate));
      }
      return cnt;
    }

    /// <summary>
    /// Gets the public (live) version of <paramref name="cnt" />, if it exists
    /// </summary>
    /// <param name="cnt">Type of the content item</param>
    /// <returns>Public (live) version of <paramref name="cnt" />, if it exists</returns>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="cnt" /> is <c>null</c>.</exception>
    public Content GetLiveContentBase(Content cnt) => ((IEnumerable<MethodInfo>) this.GetType().GetMethods()).Where<MethodInfo>((Func<MethodInfo, bool>) (m => m.Name == "GetLiveBase" && m.GetGenericArguments().Length == 1)).Single<MethodInfo>().MakeGenericMethod(cnt.GetType()).Invoke((object) this, new object[1]
    {
      (object) cnt
    }) as Content;

    /// <summary>Copy one content item into another</summary>
    /// <param name="desitnation">Item to be copied to</param>
    /// <param name="source">Item to copy from</param>
    /// <remarks>
    /// Will try to copy urls if both items are ILocatable
    /// Will copy dynamic fields.
    /// Will try to copy security properties if both objects are secured
    /// </remarks>
    public void CopyContent(Content source, Content destination)
    {
      destination.ApplicationName = source.ApplicationName;
      destination.Description.CopyFrom(source.GetString("Description"));
      destination.ExpirationDate = source.ExpirationDate;
      destination.Title.CopyFrom(source.GetString("Title"));
      destination.UrlName.CopyFrom(source.GetString("UrlName"));
      destination.Version = source.Version;
      destination.DefaultPageId = source.DefaultPageId;
      destination.PostRights = source.PostRights;
      destination.AllowTrackBacks = source.AllowTrackBacks;
      destination.AllowComments = source.AllowComments;
      destination.ApproveComments = source.ApproveComments;
      destination.EmailAuthor = source.EmailAuthor;
      if (destination is IOrderedItem)
        ((IOrderedItem) destination).Ordinal = ((IOrderedItem) source).Ordinal;
      ContentLifecycleManagerExtensions.CopyDynamicFieldsRecursively((IDynamicFieldsContainer) source, (IDynamicFieldsContainer) destination);
      IDataProviderBase provider = source.Provider as IDataProviderBase;
      if (!(destination is ISecuredObject) || provider == null)
        return;
      ((ISecuredObject) destination).CopySecurityFrom((ISecuredObject) source, (IDataProviderBase) this, provider);
    }

    public override object Clone()
    {
      ContentDataProviderBase dataProviderBase = (ContentDataProviderBase) base.Clone();
      dataProviderBase.contentDecorator = (IContentProviderDecorator) this.contentDecorator.Clone();
      dataProviderBase.contentDecorator.DataProvider = dataProviderBase;
      return (object) dataProviderBase;
    }

    /// <inheritdoc />
    public override string GetItemTitleValue(IDataItem item) => (string) this.GetItemTitle(item);

    /// <inheritdoc />
    public override string GetItemTitleValue(IDataItem item, CultureInfo culture) => this.GetItemTitle(item)[culture];

    /// <inheritdoc />
    bool IDataEventProvider.DataEventsEnabled => true;

    /// <inheritdoc />
    bool IDataEventProvider.ApplyDataEventItemFilter(IDataItem item) => item is Content && !(item is Comment);

    /// <inheritdoc />
    public virtual Type GetParentType(Type childType) => (Type) null;

    /// <inheritdoc />
    public virtual IDataItem GetParent(IDataItem child) => (IDataItem) null;

    /// <inheritdoc />
    public virtual IList<IDataItem> GetChildren(IDataItem parent) => (IList<IDataItem>) null;

    /// <summary>Gets the item title as an Lstring.</summary>
    /// <param name="item">The item.</param>
    /// <returns>The Title's Lstring.</returns>
    private Lstring GetItemTitle(IDataItem item) => ((IContent) item).Title;

    /// <inheritdoc />
    public virtual IQueryable<T> GetRelatedItems<T>(
      string itemType,
      string itemProviderName,
      Guid itemId,
      string fieldName,
      ContentLifecycleStatus? status,
      string filterExpression,
      string orderExpression,
      int? skip,
      int? take,
      ref int? totalCount,
      RelationDirection relationDirection = RelationDirection.Child)
      where T : IDataItem
    {
      return Queryable.OfType<T>(this.GetRelatedItems(itemType, itemProviderName, itemId, fieldName, typeof (T), status, filterExpression, orderExpression, skip, take, ref totalCount, relationDirection));
    }

    /// <inheritdoc />
    public virtual IQueryable GetRelatedItems(
      string itemType,
      string itemProviderName,
      Guid itemId,
      string fieldName,
      Type relatedItemsType,
      ContentLifecycleStatus? status,
      string filterExpression,
      string orderExpression,
      int? skip,
      int? take,
      ref int? totalCount,
      RelationDirection relationDirection = RelationDirection.Child)
    {
      return relationDirection == RelationDirection.Child ? this.GetRelatedItems(itemType, itemProviderName, itemId, fieldName, relatedItemsType, status, filterExpression, orderExpression, skip, take, ref totalCount) : this.GetRelatingItems(itemType, itemProviderName, itemId, fieldName, relatedItemsType, status, filterExpression, orderExpression, skip, take, ref totalCount);
    }

    /// <inheritdoc />
    private IQueryable GetRelatedItems(
      string parentItemType,
      string parentItemProviderName,
      Guid parentItemId,
      string fieldName,
      Type itemType,
      ContentLifecycleStatus? status,
      string filterExpression,
      string orderExpression,
      int? skip,
      int? take,
      ref int? totalCount)
    {
      IQueryable<ContentLink> queryable1 = SitefinityQuery.Get<ContentLink>((DataProviderBase) this).Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => cl.ParentItemType == parentItemType && cl.ParentItemProviderName == parentItemProviderName && cl.ChildItemProviderName == this.Name));
      if (parentItemId != Guid.Empty)
        queryable1 = queryable1.Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => cl.ParentItemId == parentItemId));
      IQueryable<ContentLink> queryable2 = (IQueryable<ContentLink>) RelatedDataHelper.ApplyLinksFilters(RelatedDataHelper.ApplyDeletedLinksFilters(queryable1, RelationDirection.Child), fieldName, status).OrderBy<ContentLink, float>((Expression<Func<ContentLink, float>>) (cl => cl.Ordinal));
      IQueryable queryable3;
      if (itemType.ImplementsInterface(typeof (ILifecycleDataItemGeneric)))
        queryable3 = (IQueryable) RelatedDataHelper.JoinRelatedItems<ILifecycleDataItemGeneric>(SitefinityQuery.Get<ILifecycleDataItemGeneric>(itemType, (DataProviderBase) this), queryable2, status);
      else
        queryable3 = (IQueryable) SitefinityQuery.Get<IDataItem>(itemType, (DataProviderBase) this).Join<IDataItem, ContentLink, Guid, IDataItem>((IEnumerable<ContentLink>) queryable2, (Expression<Func<IDataItem, Guid>>) (i => i.Id), (Expression<Func<ContentLink, Guid>>) (cl => cl.ChildItemId), (Expression<Func<IDataItem, ContentLink, IDataItem>>) ((i, cl) => i));
      MethodCallExpression methodCallExpression = Expression.Call(typeof (Queryable), "Cast", new Type[1]
      {
        queryable3.ElementType
      }, queryable3.Expression);
      IQueryable source = queryable3.Provider.CreateQuery((Expression) methodCallExpression);
      if (!string.IsNullOrEmpty(filterExpression))
        source = source.Where(filterExpression);
      int? nullable;
      if (totalCount.HasValue)
      {
        totalCount = new int?(source.Count());
        nullable = totalCount;
        int num = 0;
        if (nullable.GetValueOrDefault() == num & nullable.HasValue)
          return (IQueryable) Enumerable.Empty<IDataItem>().AsQueryable<IDataItem>();
      }
      if (!string.IsNullOrEmpty(orderExpression))
        source = source.OrderBy(orderExpression);
      if (skip.HasValue)
      {
        nullable = skip;
        int num = 0;
        if (nullable.GetValueOrDefault() > num & nullable.HasValue)
          source = source.Skip(skip.Value);
      }
      if (take.HasValue)
      {
        nullable = take;
        int num = 0;
        if (nullable.GetValueOrDefault() > num & nullable.HasValue)
          source = source.Take(take.Value);
      }
      return source;
    }

    /// <inheritdoc />
    private IQueryable GetRelatingItems(
      string childItemType,
      string childItemProviderName,
      Guid childItemId,
      string fieldName,
      Type itemType,
      ContentLifecycleStatus? status,
      string filterExpression,
      string orderExpression,
      int? skip,
      int? take,
      ref int? totalCount)
    {
      IQueryable<ContentLink> queryable1 = SitefinityQuery.Get<ContentLink>((DataProviderBase) this).Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => cl.ChildItemType == childItemType && cl.ChildItemProviderName == childItemProviderName && cl.ParentItemProviderName == this.Name));
      if (childItemId != Guid.Empty)
        queryable1 = queryable1.Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => cl.ChildItemId == childItemId));
      IQueryable<ContentLink> queryable2 = RelatedDataHelper.ApplyLinksFilters(RelatedDataHelper.ApplyDeletedLinksFilters(queryable1, RelationDirection.Parent), fieldName, status);
      IQueryable queryable3;
      if (itemType.ImplementsInterface(typeof (ILifecycleDataItemGeneric)))
        queryable3 = (IQueryable) RelatedDataHelper.JoinRelatingItems<ILifecycleDataItemGeneric>(SitefinityQuery.Get<ILifecycleDataItemGeneric>(itemType, (DataProviderBase) this), queryable2, status);
      else
        queryable3 = (IQueryable) SitefinityQuery.Get<IDataItem>(itemType, (DataProviderBase) this).Join<IDataItem, ContentLink, Guid, IDataItem>((IEnumerable<ContentLink>) queryable2, (Expression<Func<IDataItem, Guid>>) (i => i.Id), (Expression<Func<ContentLink, Guid>>) (cl => cl.ParentItemId), (Expression<Func<IDataItem, ContentLink, IDataItem>>) ((i, cl) => i));
      MethodCallExpression methodCallExpression = Expression.Call(typeof (Queryable), "Cast", new Type[1]
      {
        queryable3.ElementType
      }, queryable3.Expression);
      IQueryable source = queryable3.Provider.CreateQuery((Expression) methodCallExpression);
      if (!string.IsNullOrEmpty(filterExpression))
        source = source.Where(filterExpression);
      int? nullable;
      if (totalCount.HasValue)
      {
        totalCount = new int?(source.Count());
        nullable = totalCount;
        int num = 0;
        if (nullable.GetValueOrDefault() == num & nullable.HasValue)
          return (IQueryable) Enumerable.Empty<IDataItem>().AsQueryable<IDataItem>();
      }
      if (!string.IsNullOrEmpty(orderExpression))
        source = source.OrderBy(orderExpression);
      if (skip.HasValue)
      {
        nullable = skip;
        int num = 0;
        if (nullable.GetValueOrDefault() > num & nullable.HasValue)
          source = source.Skip(skip.Value);
      }
      if (take.HasValue)
      {
        nullable = take;
        int num = 0;
        if (nullable.GetValueOrDefault() > num & nullable.HasValue)
          source = source.Take(take.Value);
      }
      return source;
    }

    /// <inheritdoc />
    public Dictionary<Guid, List<IDataItem>> GetRelatedItems(
      string itemType,
      string itemProviderName,
      List<Guid> parentItemIds,
      string fieldName,
      Type relatedItemsType,
      ContentLifecycleStatus? status)
    {
      IQueryable<ContentLink> queryable1 = SitefinityQuery.Get<ContentLink>((DataProviderBase) this).Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => cl.ParentItemType == itemType && cl.ParentItemProviderName == itemProviderName && cl.ChildItemProviderName == this.Name));
      if (parentItemIds != null && parentItemIds.Count > 0)
        queryable1 = queryable1.Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => parentItemIds.Contains(cl.ParentItemId)));
      IQueryable<ContentLink> queryable2 = RelatedDataHelper.ApplyLinksFilters(RelatedDataHelper.ApplyDeletedLinksFilters(queryable1, RelationDirection.Child), fieldName, status);
      IEnumerable<IGrouping<Guid, ContentLink>> source1 = queryable2.ToList<ContentLink>().GroupBy<ContentLink, Guid>((Func<ContentLink, Guid>) (l => l.ParentItemId));
      Dictionary<Guid, List<IDataItem>> relatedItems = new Dictionary<Guid, List<IDataItem>>();
      if (source1.Any<IGrouping<Guid, ContentLink>>())
      {
        IQueryable<IDataItem> childItems = SitefinityQuery.Get<IDataItem>(relatedItemsType, (DataProviderBase) this);
        IEnumerable<IDataItem> source2 = (IEnumerable<IDataItem>) ContentDataProviderBase.JoinRelatedItems(queryable2, childItems, relatedItemsType, status);
        foreach (IGrouping<Guid, ContentLink> grouping in source1)
        {
          IGrouping<Guid, ContentLink> linkGroup = grouping;
          IEnumerable<IDataItem> items = source2.Where<IDataItem>((Func<IDataItem, bool>) (ri => linkGroup.Any<ContentLink>((Func<ContentLink, bool>) (l => l.ChildItemId == RelatedDataExtensions.GetId((object) ri)))));
          List<IDataItem> list = RelatedDataHelper.SortRelatedItems<IDataItem>((IEnumerable<ContentLink>) linkGroup, items, status).ToList<IDataItem>();
          relatedItems.Add(linkGroup.Key, list);
        }
      }
      return relatedItems;
    }

    /// <inheritdoc />
    public IEnumerable<IDataItem> GetRelatedItemsList(
      string itemType,
      string itemProviderName,
      Collection<Guid> parentItemIds,
      string fieldName,
      Type relatedItemsType,
      ContentLifecycleStatus? status)
    {
      IQueryable<ContentLink> queryable = SitefinityQuery.Get<ContentLink>((DataProviderBase) this).Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => cl.ParentItemType == itemType && cl.ParentItemProviderName == itemProviderName && cl.ChildItemProviderName == this.Name));
      if (parentItemIds != null && parentItemIds.Count<Guid>() > 0)
        queryable = queryable.Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => parentItemIds.Contains(cl.ParentItemId)));
      return (IEnumerable<IDataItem>) ContentDataProviderBase.JoinRelatedItems(RelatedDataHelper.ApplyLinksFilters(RelatedDataHelper.ApplyDeletedLinksFilters(queryable, RelationDirection.Child), fieldName, status), SitefinityQuery.Get<IDataItem>(relatedItemsType, (DataProviderBase) this), relatedItemsType, status);
    }

    private static List<IDataItem> JoinRelatedItems(
      IQueryable<ContentLink> links,
      IQueryable<IDataItem> childItems,
      Type relatedItemsType,
      ContentLifecycleStatus? status)
    {
      IEnumerable<IDataItem> source;
      if (relatedItemsType.ImplementsInterface(typeof (ILifecycleDataItemGeneric)))
        source = (IEnumerable<IDataItem>) RelatedDataHelper.JoinRelatedItems<ILifecycleDataItemGeneric>(Queryable.Cast<ILifecycleDataItemGeneric>(childItems), links, status);
      else
        source = (IEnumerable<IDataItem>) childItems.Join<IDataItem, ContentLink, Guid, IDataItem>((IEnumerable<ContentLink>) links, (Expression<Func<IDataItem, Guid>>) (i => i.Id), (Expression<Func<ContentLink, Guid>>) (cl => cl.ChildItemId), (Expression<Func<IDataItem, ContentLink, IDataItem>>) ((i, cl) => i));
      return source.Distinct<IDataItem>().ToList<IDataItem>();
    }
  }
}
