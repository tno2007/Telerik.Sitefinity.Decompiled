// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.ContentLinks.OpenAccessContentLinksProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Microsoft.Practices.Unity.InterceptionExtension;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;
using Telerik.Sitefinity.Data.ContentLinks.Events;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Data.ContentLinks
{
  /// <summary>
  /// Represents Open access implementation of the content links provider
  /// </summary>
  public class OpenAccessContentLinksProvider : 
    ContentLinksProviderBase,
    IOpenAccessDataProvider,
    IDataProviderBase,
    IDisposable,
    ICloneable,
    IDataProviderEventsCall,
    IOpenAccessMetadataProvider
  {
    private const string ItemsStateKey = "ItemsStateKey";
    internal IObjectScope Scope;

    /// <summary>Creates the content link.</summary>
    /// <param name="id">The id of the link</param>
    /// <param name="parentItemId">The parent item id.</param>
    /// <param name="childItemId">The child item id.</param>
    /// <param name="parentItemProviderName">Name of the parent item provider.</param>
    /// <param name="childItemProviderName">Name of the child item provider.</param>
    /// <param name="parentItemType">Type of the parent item.</param>
    /// <param name="childItemType">Type of the child item.</param>
    /// <returns>content link</returns>
    public override ContentLink CreateContentLink(
      Guid id,
      string propertyName,
      Guid parentItemId,
      Guid childItemId,
      string parentItemProviderName,
      string childItemProviderName,
      Type parentItemType,
      Type childItemType)
    {
      if (parentItemId == Guid.Empty)
        throw new ArgumentNullException("parentItemId cannot be an Empty Guid");
      if (childItemId == Guid.Empty)
        throw new ArgumentNullException("childItemId cannot be an Empty Guid");
      if (parentItemType == (Type) null)
        throw new ArgumentNullException(nameof (parentItemType));
      if (childItemType == (Type) null)
        throw new ArgumentNullException(nameof (childItemType));
      ContentLink entity = new ContentLink(parentItemId, childItemId)
      {
        Id = id,
        ParentItemProviderName = parentItemProviderName,
        ParentItemType = parentItemType.FullName,
        ChildItemProviderName = childItemProviderName,
        ChildItemType = childItemType.FullName,
        ApplicationName = this.ApplicationName,
        ComponentPropertyName = propertyName
      };
      ((IDataItem) entity).Provider = (object) this;
      if (id != Guid.Empty)
        this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>Creates the content link.</summary>
    /// <param name="parentItemId">The parent item id.</param>
    /// <param name="childItemId">The child item id.</param>
    /// <param name="parentItemProviderName">Name of the parent item provider.</param>
    /// <param name="childItemProviderName">Name of the child item provider.</param>
    /// <param name="parentItemType">Type of the parent item.</param>
    /// <param name="childItemType">Type of the child item.</param>
    /// <returns></returns>
    public override ContentLink CreateContentLink(
      string propertyName,
      Guid parentItemId,
      Guid childItemId,
      string parentItemProviderName,
      string childItemProviderName,
      Type parentItemType,
      Type childItemType)
    {
      return this.CreateContentLink(this.GetNewGuid(), propertyName, parentItemId, childItemId, parentItemProviderName, childItemProviderName, parentItemType, childItemType);
    }

    /// <summary>Creates new data item.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="pageId">The pageId.</param>
    /// <returns></returns>
    public override object CreateItem(Type itemType, Guid id)
    {
      ContentLink entity = new ContentLink()
      {
        Id = id,
        ApplicationName = this.ApplicationName
      };
      if (id != Guid.Empty)
        this.GetContext().Add((object) entity);
      return (object) entity;
    }

    /// <summary>Gets a content link by its id</summary>
    /// <param name="id">The id.</param>
    /// <returns></returns>
    public override ContentLink GetContentLink(Guid id)
    {
      ContentLink contentLink = !(id == Guid.Empty) ? this.GetContext().GetItemById<ContentLink>(id.ToString()) : throw new ArgumentException("Id cannot be Empty Guid");
      ((IDataItem) contentLink).Provider = (object) this;
      return contentLink;
    }

    /// <summary>Returns all content links</summary>
    /// <returns></returns>
    public override IQueryable<ContentLink> GetContentLinks()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<ContentLink>((DataProviderBase) this).Where<ContentLink>((Expression<Func<ContentLink, bool>>) (c => c.ApplicationName == appName));
    }

    /// <summary>Returns the content links for a the specified item id</summary>
    /// <param name="parentItemId">The parent item id.</param>
    /// <param name="parentItemType"></param>
    /// <param name="propertyName"></param>
    public override IQueryable<ContentLink> GetContentLinks(
      Guid parentItemId,
      Type parentItemType,
      string propertyName)
    {
      string typeName = parentItemType.FullName;
      return this.GetContentLinks().Where<ContentLink>((Expression<Func<ContentLink, bool>>) (c => c.ParentItemId == parentItemId && c.ParentItemType == typeName && c.ComponentPropertyName == propertyName));
    }

    /// <summary>Deletes the specified meta type.</summary>
    /// <param name="metaType">Type of the meta.</param>
    public override void Delete(ContentLink contentLink) => this.GetContext().Remove((object) contentLink);

    /// <summary>Gets the meta data source.</summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public MetadataSource GetMetaDataSource(IDatabaseMappingContext context) => (MetadataSource) new ContentLinksMetadataSource(context);

    /// <summary>
    /// Gets or sets the OpenAccess context. Alternative to Database.
    /// </summary>
    /// <value>The context.</value>
    public OpenAccessProviderContext Context { get; set; }

    public override void FlushTransaction()
    {
      this.AccumulateDirtyItemsStateData();
      base.FlushTransaction();
    }

    public override void CommitTransaction()
    {
      List<IEvent> eventList = (List<IEvent>) null;
      if (this.ShouldRaiseDataEvents())
        eventList = this.GetDataEvents(this.GetDirtyItemsStateData() ?? this.GetDataEventItems(new Func<IDataItem, bool>(this.ApplyDataEventItemFilter)));
      base.CommitTransaction();
      if (eventList == null)
        return;
      foreach (IEvent @event in eventList)
      {
        IDictionary<string, object> executionStateBag = this.GetContext().ExecutionStateBag;
        string key = "EventOriginKey";
        if (executionStateBag != null && executionStateBag.ContainsKey(key))
        {
          object obj = executionStateBag[key];
          @event.Origin = (string) obj;
        }
        EventHub.Raise(@event, false);
      }
    }

    [ApplyNoPolicies]
    protected new virtual void AccumulateDirtyItemsStateData()
    {
      if (!this.ShouldRaiseDataEvents())
        return;
      ICollection<IDataItem> dataItems = this.GetDataEventItems(new Func<IDataItem, bool>(this.ApplyDataEventItemFilter));
      if (dataItems == null || !dataItems.Any<IDataItem>())
        return;
      ICollection<IDataItem> dirtyItemsStateData = this.GetDirtyItemsStateData();
      if (dirtyItemsStateData != null && dirtyItemsStateData.Any<IDataItem>())
      {
        foreach (IDataItem dataItem in (IEnumerable<IDataItem>) dataItems)
          dirtyItemsStateData.Add(dataItem);
        dataItems = dirtyItemsStateData;
      }
      this.SetExecutionStateData("ItemsStateKey", (object) dataItems);
    }

    [ApplyNoPolicies]
    protected virtual ICollection<IDataItem> GetDirtyItemsStateData() => this.GetExecutionStateData("ItemsStateKey") as ICollection<IDataItem>;

    [ApplyNoPolicies]
    protected virtual List<IEvent> GetDataEvents(ICollection<IDataItem> items)
    {
      List<IEvent> dataEvents = new List<IEvent>();
      if (this.ShouldRaiseDataEvents() && items != null && items.Any<IDataItem>())
      {
        Guid currentUserId = ClaimsManager.GetCurrentUserId();
        foreach (IDataItem itemInTransaction in (IEnumerable<IDataItem>) items)
        {
          SecurityConstants.TransactionActionType dirtyItemStatus = this.providerDecorator.GetDirtyItemStatus((object) itemInTransaction);
          if (itemInTransaction is ContentLink contentLink)
          {
            switch (dirtyItemStatus)
            {
              case SecurityConstants.TransactionActionType.New:
                dataEvents.Add((IEvent) new ContentLinkCreatedEvent()
                {
                  Item = contentLink,
                  UserId = currentUserId
                });
                continue;
              case SecurityConstants.TransactionActionType.Deleted:
                dataEvents.Add((IEvent) new ContentLinkDeletedEvent()
                {
                  Item = contentLink,
                  UserId = currentUserId,
                  DeletionDate = contentLink.LastModified
                });
                continue;
              default:
                continue;
            }
          }
        }
      }
      return dataEvents;
    }

    [ApplyNoPolicies]
    protected virtual ICollection<IDataItem> GetDataEventItems(
      Func<IDataItem, bool> filterPredicate)
    {
      IList dirtyItems = this.GetDirtyItems();
      List<IDataItem> dataEventItems = new List<IDataItem>(dirtyItems.Count);
      foreach (object obj in (IEnumerable) dirtyItems)
      {
        if (obj is IDataItem dataItem && filterPredicate(dataItem))
          dataEventItems.Add(dataItem);
      }
      return (ICollection<IDataItem>) dataEventItems;
    }

    [ApplyNoPolicies]
    internal bool ShouldRaiseDataEvents() => !SystemManager.Initializing;

    /// <inheritdoc />
    private bool ApplyDataEventItemFilter(IDataItem item) => item is ContentLink;
  }
}
