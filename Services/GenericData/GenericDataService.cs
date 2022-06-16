// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.GenericData.GenericDataService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using ServiceStack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.ContentLinks;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services.GenericData.Messages;
using Telerik.Sitefinity.Services.GenericData.Responses;
using Telerik.Sitefinity.Services.RelatedData;
using Telerik.Sitefinity.Services.RelatedData.Responses;
using Telerik.Sitefinity.Services.ServiceStack.Filters;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Services.GenericData
{
  /// <summary>Generic data service</summary>
  /// <seealso cref="!:ServiceStack.Service" />
  [RequestBackendAuthenticationFilter(false)]
  public class GenericDataService : Service
  {
    internal const string WebServiceUrl = "restapi/sitefinity/generic-data";
    private const string LiteralUnder = "<span class='sfSep'>|</span> under ";

    /// <summary>Gets list of available items to be related</summary>
    /// 
    ///             GET: "/sitefinity/generic-data/data-items"
    public object Get(DataItemMessage message)
    {
      Type itemType = TypeResolutionService.ResolveType(message.ItemType);
      int? totalCount = new int?(0);
      IEnumerable enumerable = (IEnumerable) new ArrayList();
      Guid result;
      if (Guid.TryParse(message.ItemId, out result))
      {
        DataItemResponse itemResponse = this.GetItemResponse(message, itemType, result);
        if (itemResponse != null)
          (enumerable as ArrayList).Add((object) itemResponse);
      }
      else if (itemType == typeof (PageNode))
      {
        PageManager manager = PageManager.GetManager();
        Guid rootNodeId = SiteInitializer.CurrentFrontendRootNodeId;
        enumerable = this.GetPagesRelatedDataItemsResponse(DataProviderBase.SetExpressions<PageNode>(manager.GetPageNodes().Where<PageNode>((Expression<Func<PageNode, bool>>) (n => n.RootNodeId == rootNodeId && n.Owner != Guid.Empty && !n.IsDeleted)), message.Filter, message.Order, new int?(message.Skip), new int?(message.Take), ref totalCount), this.GetRelatedItemsLinks(message, (IManager) manager));
      }
      else
      {
        ILifecycleManager mappedManager = ManagerBase.GetMappedManager(itemType, message.ItemProvider) as ILifecycleManager;
        IEnumerable items = mappedManager.GetItems(itemType, message.Filter, message.Order, message.Skip, message.Take, ref totalCount);
        IQueryable<ContentLink> relatedItemsLinks = this.GetRelatedItemsLinks(message, (IManager) mappedManager);
        enumerable = this.GetRelatedDataItemsResponse(items, mappedManager, itemType, relatedItemsLinks);
      }
      return (object) new DataItemsResponse()
      {
        Items = enumerable,
        TotalCount = totalCount.Value
      };
    }

    /// <summary>Delete all temps of specific item</summary>
    /// 
    ///             DELETE: "/sitefinity/generic-data/temp-items"
    public void Delete(DataItemMessage message)
    {
      Guid id = new Guid(message.ItemId);
      Type type = TypeResolutionService.ResolveType(message.ItemType, false);
      if (!(type != (Type) null))
        return;
      this.DeleteItem(id, type, message.ItemProvider);
    }

    private DataItemResponse GetItemResponse(
      DataItemMessage message,
      Type itemType,
      Guid itemId)
    {
      string name = SystemManager.CurrentContext.Culture.Name;
      DataItemResponse itemResponse;
      if (itemType == typeof (PageNode))
      {
        PageManager manager = PageManager.GetManager();
        PageNode pageNode = manager.GetPageNode(itemId);
        IQueryable<ContentLink> relatedItemsLinks = this.GetRelatedItemsLinks(message, (IManager) manager);
        List<Guid> list = Enumerable.Empty<Guid>().ToList<Guid>();
        Expression<Func<ContentLink, bool>> predicate = (Expression<Func<ContentLink, bool>>) (cl => cl.ChildItemId == itemId);
        if (relatedItemsLinks.Any<ContentLink>(predicate))
          list.Add(itemId);
        itemResponse = this.GetPagesRelatedDataItemResponse(list, pageNode);
      }
      else
      {
        ILifecycleManager mappedManager = ManagerBase.GetMappedManager(itemType, message.ItemProvider) as ILifecycleManager;
        object obj = mappedManager.GetItem(itemType, itemId);
        IQueryable<ContentLink> relatedItemsLinks = this.GetRelatedItemsLinks(message, (IManager) mappedManager);
        List<Guid> list = Enumerable.Empty<Guid>().ToList<Guid>();
        Expression<Func<ContentLink, bool>> predicate = (Expression<Func<ContentLink, bool>>) (cl => cl.ChildItemId == itemId);
        if (relatedItemsLinks.Any<ContentLink>(predicate))
          list.Add(itemId);
        if (typeof (DynamicContent).IsAssignableFromType(itemType))
        {
          DynamicContent dynamicContent = obj as DynamicContent;
          itemResponse = this.GetDynamicRelatedItemResponse(mappedManager, itemType, name, list, dynamicContent);
          itemResponse.LifecycleStatus = RelatedDataHelper.GetItemLifecycleStatus((ILifecycleDataItem) dynamicContent, mappedManager);
        }
        else
        {
          IContent content = obj as IContent;
          itemResponse = this.GetStaticRelatedItemResponse(mappedManager, itemType, name, list, content);
        }
      }
      return itemResponse;
    }

    private IQueryable<ContentLink> GetRelatedItemsLinks(
      DataItemMessage message,
      IManager manager)
    {
      IQueryable<ContentLink> relatedItemsLinks = Enumerable.Empty<ContentLink>().AsQueryable<ContentLink>();
      if (!string.IsNullOrEmpty(message.RelatedItemId) && !string.IsNullOrEmpty(message.RelatedItemType))
      {
        ContentLinksManager relatedManager = manager.Provider.GetRelatedManager<ContentLinksManager>((string) null);
        Guid originalItemId = new Guid(message.RelatedItemId);
        relatedItemsLinks = RelatedDataHelper.ApplyLinksFilters(RelatedDataHelper.ApplyDeletedLinksFilters(relatedManager.GetContentLinks().Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => cl.ParentItemId == originalItemId && cl.ParentItemType == message.RelatedItemType && cl.ParentItemProviderName == message.RelatedItemProvider && cl.ComponentPropertyName == message.FieldName)), RelationDirection.Child), (string) null, message.RelationStatus);
      }
      return relatedItemsLinks;
    }

    private IEnumerable GetRelatedDataItemsResponse(
      IEnumerable items,
      ILifecycleManager manager,
      Type itemType,
      IQueryable<ContentLink> links)
    {
      ArrayList dataItemsResponse = new ArrayList();
      if (items == null)
        return (IEnumerable) dataItemsResponse;
      return typeof (DynamicContent).IsAssignableFromType(itemType) ? this.GetDynamicRelatedItemsResponse(items, manager, itemType, links) : this.GetStaticRelatedItemsResponse(items, manager, itemType, links);
    }

    private IEnumerable GetDynamicRelatedItemsResponse(
      IEnumerable items,
      ILifecycleManager manager,
      Type itemType,
      IQueryable<ContentLink> links)
    {
      string name = SystemManager.CurrentContext.Culture.Name;
      List<DynamicContent> list1 = items.OfType<DynamicContent>().ToList<DynamicContent>();
      IEnumerable<Guid> itemsIds = list1.Select<DynamicContent, Guid>((Func<DynamicContent, Guid>) (i => i.Id));
      List<Guid> list2 = links.Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => itemsIds.Contains<Guid>(cl.ChildItemId))).Select<ContentLink, Guid>((Expression<Func<ContentLink, Guid>>) (cl => cl.ChildItemId)).ToList<Guid>();
      ArrayList relatedItemsResponse = new ArrayList();
      RelatedDataHelper.PopulateLifecycleInformation(list1, name);
      foreach (DynamicContent dynamicContent in list1)
      {
        DataItemResponse relatedItemResponse = this.GetDynamicRelatedItemResponse(manager, itemType, name, list2, dynamicContent);
        relatedItemsResponse.Add((object) relatedItemResponse);
      }
      return (IEnumerable) relatedItemsResponse;
    }

    private DataItemResponse GetDynamicRelatedItemResponse(
      ILifecycleManager manager,
      Type itemType,
      string currentUiCulture,
      List<Guid> selectedItemsIds,
      DynamicContent item)
    {
      string name = ((IDataProviderBase) item.Provider).Name;
      DataItemResponse relatedItemResponse = new DataItemResponse()
      {
        Id = item.Id,
        Title = DynamicContentExtensions.GetTitle(item),
        SubTitle = this.GetDynamicContentPath(item),
        ProviderName = name,
        Status = item.Status,
        LifecycleStatus = new LifecycleResponse()
        {
          WorkflowStatus = item.Lifecycle.WorkflowStatus,
          DisplayStatus = item.Lifecycle.Message
        },
        LastModified = item.LastModified.ToSitefinityUITime(),
        Owner = item.GetUserDisplayName(),
        DetailsViewUrl = RelatedDataResponseHelper.GetDetailsViewUrl(item.GetType()),
        PreviewUrl = RelatedDataResponseHelper.GetItemViewUrl(item.Id.ToString(), itemType.FullName, name, currentUiCulture),
        IsRelated = selectedItemsIds.Contains(item.Id),
        IsEditable = (!item.IsGranted("General", "Modify") ? 0 : (!item.Lifecycle.IsLocked ? 1 : (item.Lifecycle.IsLockedByMe ? 1 : 0))) != 0
      };
      ILocalizable localizable = (ILocalizable) item;
      if (localizable != null)
        relatedItemResponse.AvailableLanguages = localizable.AvailableLanguages;
      return relatedItemResponse;
    }

    private IEnumerable GetStaticRelatedItemsResponse(
      IEnumerable items,
      ILifecycleManager manager,
      Type itemType,
      IQueryable<ContentLink> links)
    {
      string name = SystemManager.CurrentContext.Culture.Name;
      List<IContent> list1 = items.OfType<IContent>().ToList<IContent>();
      IEnumerable<Guid> itemsIds = list1.Select<IContent, Guid>((Func<IContent, Guid>) (i => i.Id));
      List<Guid> list2 = links.Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => itemsIds.Contains<Guid>(cl.ChildItemId))).Select<ContentLink, Guid>((Expression<Func<ContentLink, Guid>>) (cl => cl.ChildItemId)).ToList<Guid>();
      ArrayList relatedItemsResponse = new ArrayList();
      foreach (IContent content in list1)
      {
        DataItemResponse relatedItemResponse = this.GetStaticRelatedItemResponse(manager, itemType, name, list2, content);
        relatedItemsResponse.Add((object) relatedItemResponse);
      }
      return (IEnumerable) relatedItemsResponse;
    }

    private DataItemResponse GetStaticRelatedItemResponse(
      ILifecycleManager manager,
      Type itemType,
      string currentUiCulture,
      List<Guid> selectedItemsIds,
      IContent item)
    {
      string name = ((IDataProviderBase) item.Provider).Name;
      ISecuredObject secObj = item as ISecuredObject;
      LifecycleResponse lifecycleResponse = this.GetStaticItemLifecycleResponse(item, manager);
      DataItemResponse relatedItemResponse = new DataItemResponse()
      {
        Id = item.Id,
        Title = item is IHasTitle ? ((IHasTitle) item).GetTitle() : item.Title.ToString(),
        SubTitle = this.GetStaticContentPath(item),
        ProviderName = name,
        Status = item is ILifecycleDataItem ? ((ILifecycleDataItem) item).Status : ContentLifecycleStatus.Live,
        LifecycleStatus = lifecycleResponse,
        LastModified = item.LastModified.ToSitefinityUITime(),
        Owner = item.GetUserDisplayName(),
        DetailsViewUrl = RelatedDataResponseHelper.GetDetailsViewUrl(item.GetType()),
        PreviewUrl = RelatedDataResponseHelper.GetItemViewUrl(item.Id.ToString(), itemType.FullName, name, currentUiCulture),
        IsRelated = selectedItemsIds.Contains(item.Id),
        IsEditable = RelatedDataHelper.IsContentItemEditable(secObj) && (!lifecycleResponse.IsLocked || lifecycleResponse.IsLockedByMe)
      };
      if (item is ILocalizable localizable)
        relatedItemResponse.AvailableLanguages = localizable.AvailableLanguages;
      return relatedItemResponse;
    }

    private IEnumerable GetPagesRelatedDataItemsResponse(
      IQueryable<PageNode> pageNodes,
      IQueryable<ContentLink> links)
    {
      List<Guid> itemsIds = pageNodes.Select<PageNode, Guid>((Expression<Func<PageNode, Guid>>) (i => i.Id)).ToList<Guid>();
      List<Guid> list = links.Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => itemsIds.Contains(cl.ChildItemId))).Select<ContentLink, Guid>((Expression<Func<ContentLink, Guid>>) (cl => cl.ChildItemId)).ToList<Guid>();
      ArrayList dataItemsResponse = new ArrayList();
      foreach (PageNode pageNode in (IEnumerable<PageNode>) pageNodes)
      {
        DataItemResponse dataItemResponse = this.GetPagesRelatedDataItemResponse(list, pageNode);
        dataItemsResponse.Add((object) dataItemResponse);
      }
      return (IEnumerable) dataItemsResponse;
    }

    private DataItemResponse GetPagesRelatedDataItemResponse(
      List<Guid> selectedItemsIds,
      PageNode pageNode)
    {
      LifecycleResponse pageLifecycleStatus = RelatedDataHelper.GetPageLifecycleStatus(pageNode);
      DataItemResponse dataItemResponse = new DataItemResponse()
      {
        Id = pageNode.Id,
        Title = (string) pageNode.Title,
        SubTitle = this.GetPagePath(pageNode),
        LifecycleStatus = pageLifecycleStatus,
        LastModified = pageNode.LastModified.ToSitefinityUITime(),
        Owner = pageNode.GetUserDisplayName(),
        DetailsViewUrl = RelatedDataResponseHelper.GetDetailsViewUrl(pageNode.GetType()),
        IsRelated = selectedItemsIds.Contains(pageNode.Id),
        PreviewUrl = RouteHelper.ResolveUrl(pageNode.GetBackendUrl("Preview", SystemManager.CurrentContext.Culture), UrlResolveOptions.Rooted),
        IsEditable = (!pageNode.IsGranted("Pages", "Modify") ? 0 : (!pageLifecycleStatus.IsLocked ? 1 : (pageLifecycleStatus.IsLockedByMe ? 1 : 0))) != 0
      };
      ILocalizable localizable = (ILocalizable) pageNode;
      if (localizable != null)
        dataItemResponse.AvailableLanguages = localizable.AvailableLanguages;
      return dataItemResponse;
    }

    private string GetStaticContentPath(IContent content)
    {
      string staticContentPath = string.Empty;
      if (content is IHasParent hasParent && hasParent.Parent != null)
        staticContentPath = "<span class='sfSep'>|</span> under " + hasParent.Parent.Title.ToString();
      return staticContentPath;
    }

    private string GetDynamicContentPath(DynamicContent content)
    {
      string str = string.Empty;
      if (content.SystemParentItem != null)
        str = content.SystemParentItem.GetHierarchycalTitlesAsBreadcrumb();
      return !string.IsNullOrEmpty(str) ? "<span class='sfSep'>|</span> under <i class='sfParent'>" + str + "</i>" : string.Empty;
    }

    private string GetPagePath(PageNode pageNode)
    {
      string fullTitlesPath = pageNode.Parent.GetFullTitlesPath(" > ");
      return !string.IsNullOrEmpty(fullTitlesPath) ? "<span class='sfSep'>|</span> under <i class='sfParent'>" + fullTitlesPath + "</i>" : "<span class='sfSep'>|<span> " + Res.Get<PageResources>().OnTopLevel;
    }

    private void DeleteItem(Guid id, Type type, string provider)
    {
      if (!(ManagerBase.GetMappedManager(type, provider) is ILifecycleManager mappedManager))
        return;
      ILifecycleDataItem cnt;
      try
      {
        cnt = mappedManager.GetItem(type, id) as ILifecycleDataItem;
      }
      catch (ItemNotFoundException ex)
      {
        return;
      }
      Guid currentUserId = SecurityManager.CurrentUserId;
      bool flag1 = ClaimsManager.IsUnrestricted();
      if (cnt == null || cnt.Status == ContentLifecycleStatus.Deleted)
        return;
      Guid masterId = mappedManager.Lifecycle.GetMaster(cnt).Id;
      IEnumerable<ILifecycleDataItemGeneric> lifecycleDataItemGenerics = mappedManager.GetItems(type, (string) null, (string) null, 0, 0).OfType<ILifecycleDataItemGeneric>().Where<ILifecycleDataItemGeneric>((Func<ILifecycleDataItemGeneric, bool>) (i => (i.Status == ContentLifecycleStatus.Temp || i.Status == ContentLifecycleStatus.PartialTemp) && i.OriginalContentId == masterId));
      bool flag2 = false;
      foreach (ILifecycleDataItemGeneric lifecycleDataItemGeneric in lifecycleDataItemGenerics)
      {
        if (lifecycleDataItemGeneric.Owner == currentUserId | flag1)
        {
          mappedManager.DeleteItem((object) lifecycleDataItemGeneric);
          flag2 = true;
        }
      }
      if (!flag2)
        return;
      mappedManager.SaveChanges();
    }

    private LifecycleResponse GetStaticItemLifecycleResponse(
      IContent content,
      ILifecycleManager manager)
    {
      if (content is ILifecycleDataItem)
        return RelatedDataHelper.GetItemLifecycleStatus((ILifecycleDataItem) content, manager);
      return new LifecycleResponse()
      {
        DisplayStatus = string.Empty,
        WorkflowStatus = string.Empty
      };
    }
  }
}
