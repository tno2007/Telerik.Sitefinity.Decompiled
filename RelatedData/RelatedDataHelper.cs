// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.RelatedData.RelatedDataHelper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.ContentLinks;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.Descriptors;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.Model.Localization;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.RelatedData.Configuration;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.RelatedData.Messages;
using Telerik.Sitefinity.Services.RelatedData.Responses;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.RelatedData
{
  /// <summary>
  /// This class contains helper methods for working with RelatedData.
  /// </summary>
  public class RelatedDataHelper
  {
    internal const UrlEvaluationMode RelatedDataUrlEvaluationMode = UrlEvaluationMode.QueryString;
    internal const string InlineControlValue = "inline";
    internal const string SiteDefaultProvider = "sf-site-default-provider";
    internal const string AnySiteProvider = "sf-any-site-provider";

    /// <summary>
    /// Gets the main identifier field of the related type that will be used for search
    /// </summary>
    /// <param name="relatedTypeName">Name of the related type.</param>
    /// <returns>A string that contains the title of the main identifier field of the related type.</returns>
    /// <exception cref="T:System.Exception">The exception thrown when the selected data type does not exist.</exception>
    public static string GetRelatedTypeIdentifierField(string relatedTypeName)
    {
      Type c1 = TypeResolutionService.ResolveType(relatedTypeName, false);
      if (c1 == (Type) null || typeof (DynamicContent).IsAssignableFrom(c1))
      {
        IDynamicModuleType dynamicModuleType = ModuleBuilderManager.GetModules().SelectMany<IDynamicModule, IDynamicModuleType>((Func<IDynamicModule, IEnumerable<IDynamicModuleType>>) (t => t.Types)).FirstOrDefault<IDynamicModuleType>((Func<IDynamicModuleType, bool>) (t => t.GetFullTypeName() == relatedTypeName));
        if (dynamicModuleType != null)
          return dynamicModuleType.MainShortTextFieldName;
        if (SystemManager.CurrentHttpContext != null && SystemManager.CurrentHttpContext.Items != null && SystemManager.CurrentHttpContext.Items.Contains((object) "ImportedMainShortTextFields") && SystemManager.CurrentHttpContext.Items[(object) "ImportedMainShortTextFields"] is Dictionary<string, string> dictionary && dictionary.ContainsKey(relatedTypeName))
          return dictionary[relatedTypeName];
        throw new Exception(string.Format("Selected related data type {0} doesn't exists", (object) relatedTypeName));
      }
      if (typeof (IContent).IsAssignableFrom(c1))
        return LinqHelper.MemberName<IContent>((Expression<Func<IContent, object>>) (c => c.Title));
      if (!typeof (PageNode).IsAssignableFrom(c1))
        return (string) null;
      return LinqHelper.MemberName<PageNode>((Expression<Func<PageNode, object>>) (c => c.Title));
    }

    /// <summary>Gets the related items via contains query.</summary>
    /// <param name="itemType">The type of the item.</param>
    /// <param name="itemProviderName">The name of the item provider.</param>
    /// <param name="parentItemIds">Collection of parent items identifiers.</param>
    /// <param name="fieldName">Name of the related field.</param>
    /// <param name="relatedItemsType">The type of the related items.</param>
    /// <param name="childTypeProvider">Provider for the child items.</param>
    /// <param name="status">The status of the item.</param>
    /// <returns>
    /// Dictionary:
    /// Key - the item's id
    /// Value - collection of related items.
    /// </returns>
    public static Dictionary<Guid, List<IDataItem>> GetRelatedItemsViaContains(
      string itemType,
      string itemProviderName,
      List<Guid> parentItemIds,
      string fieldName,
      Type relatedItemsType,
      DataProviderBase childTypeProvider,
      ContentLifecycleStatus? status)
    {
      IQueryable<ContentLink> queryable = ContentLinksManager.GetManager().GetContentLinks().Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => cl.ParentItemType == itemType && cl.ParentItemProviderName == itemProviderName && cl.ChildItemType == relatedItemsType.FullName && cl.ChildItemProviderName == childTypeProvider.Name));
      if (parentItemIds != null && parentItemIds.Count<Guid>() > 0)
        queryable = queryable.Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => parentItemIds.Contains(cl.ParentItemId)));
      IQueryable<ContentLink> source1 = RelatedDataHelper.ApplyLinksFilters(RelatedDataHelper.ApplyDeletedLinksFilters(queryable, RelationDirection.Child), fieldName, status);
      List<Guid> itemIds = source1.Select<ContentLink, Guid>((Expression<Func<ContentLink, Guid>>) (cl => cl.ChildItemId)).ToList<Guid>();
      IQueryable<IDataItem> source2;
      if (relatedItemsType.ImplementsInterface(typeof (ILifecycleDataItemGeneric)))
        source2 = (IQueryable<IDataItem>) RelatedDataHelper.ApplyRelatedItemsContainsFilter(SitefinityQuery.Get<ILifecycleDataItemGeneric>(relatedItemsType, childTypeProvider), itemIds, status);
      else
        source2 = SitefinityQuery.Get<IDataItem>(relatedItemsType, childTypeProvider).Where<IDataItem>((Expression<Func<IDataItem, bool>>) (i => itemIds.Contains(i.Id)));
      IEnumerable<IGrouping<Guid, ContentLink>> groupings = source1.ToList<ContentLink>().GroupBy<ContentLink, Guid>((Func<ContentLink, Guid>) (l => l.ParentItemId));
      List<IDataItem> list1 = source2.ToList<IDataItem>();
      Dictionary<Guid, List<IDataItem>> itemsViaContains = new Dictionary<Guid, List<IDataItem>>();
      foreach (IGrouping<Guid, ContentLink> grouping in groupings)
      {
        IGrouping<Guid, ContentLink> linkGroup = grouping;
        List<IDataItem> list2 = list1.Where<IDataItem>((Func<IDataItem, bool>) (ri => linkGroup.Any<ContentLink>((Func<ContentLink, bool>) (l => l.ChildItemId == RelatedDataExtensions.GetId((object) ri))))).ToList<IDataItem>();
        itemsViaContains.Add(linkGroup.Key, list2);
      }
      return itemsViaContains;
    }

    /// <summary>Gets the related items via contains query.</summary>
    /// <param name="itemType">The type of the item.</param>
    /// <param name="itemProviderName">The name of the item provider.</param>
    /// <param name="parentItemIds">Collection of parent items identifiers.</param>
    /// <param name="fieldName">Name of the related field.</param>
    /// <param name="relatedItemsType">The type of the related items.</param>
    /// <param name="childTypeProvider">Provider for the child items.</param>
    /// <param name="status">The status of the item.</param>
    /// <returns>Returns a collection of related items by parent item ids.</returns>
    internal static IEnumerable<IDataItem> GetRelatedItemsListViaContains(
      string itemType,
      string itemProviderName,
      Collection<Guid> parentItemIds,
      string fieldName,
      Type relatedItemsType,
      DataProviderBase childTypeProvider,
      ContentLifecycleStatus? status)
    {
      IQueryable<ContentLink> queryable = ContentLinksManager.GetManager().GetContentLinks().Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => cl.ParentItemType == itemType && cl.ParentItemProviderName == itemProviderName && cl.ChildItemType == relatedItemsType.FullName && cl.ChildItemProviderName == childTypeProvider.Name));
      if (parentItemIds != null && parentItemIds.Count<Guid>() > 0)
        queryable = queryable.Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => parentItemIds.Contains(cl.ParentItemId)));
      List<Guid> itemIds = RelatedDataHelper.ApplyLinksFilters(RelatedDataHelper.ApplyDeletedLinksFilters(queryable, RelationDirection.Child), fieldName, status).Select<ContentLink, Guid>((Expression<Func<ContentLink, Guid>>) (cl => cl.ChildItemId)).ToList<Guid>();
      IQueryable<IDataItem> source;
      if (relatedItemsType.ImplementsInterface(typeof (ILifecycleDataItemGeneric)))
        source = (IQueryable<IDataItem>) RelatedDataHelper.ApplyRelatedItemsContainsFilter(SitefinityQuery.Get<ILifecycleDataItemGeneric>(relatedItemsType, childTypeProvider), itemIds, status);
      else
        source = SitefinityQuery.Get<IDataItem>(relatedItemsType, childTypeProvider).Where<IDataItem>((Expression<Func<IDataItem, bool>>) (i => itemIds.Contains(i.Id)));
      return (IEnumerable<IDataItem>) source.ToList<IDataItem>();
    }

    /// <summary>
    /// Copies item relations from <paramref name="source" /> to <paramref name="destination" />
    /// </summary>
    /// <param name="source">Source to copy relations from</param>
    /// <param name="destination">Destination where relations should be copied to</param>
    /// <param name="contentLinksManager">The content links manager.</param>
    internal static void CopyItemRelations(
      ILifecycleDataItemGeneric source,
      ILifecycleDataItemGeneric destination,
      IContentLinksManager contentLinksManager)
    {
      foreach (PropertyDescriptor property in TypeDescriptor.GetProperties((object) source))
      {
        if (property is RelatedDataPropertyDescriptor && source != null && destination != null)
        {
          Guid parentItemId = source.Status == ContentLifecycleStatus.Master ? source.Id : source.OriginalContentId;
          Type type = source.GetType();
          switch (source.Status)
          {
            case ContentLifecycleStatus.Master:
              switch (destination.Status)
              {
                case ContentLifecycleStatus.Temp:
                  RelatedDataHelper.CopyFieldRelations(contentLinksManager, parentItemId, type, property.Name, (Func<ContentLink, bool>) (cl => cl.AvailableForTemp != cl.AvailableForMaster), ContentLifecycleStatus.Master, ContentLifecycleStatus.Temp);
                  continue;
                case ContentLifecycleStatus.Live:
                  RelatedDataHelper.CopyFieldRelations(contentLinksManager, parentItemId, type, property.Name, (Func<ContentLink, bool>) (cl => cl.AvailableForLive != cl.AvailableForMaster), ContentLifecycleStatus.Master, ContentLifecycleStatus.Live);
                  continue;
                default:
                  continue;
              }
            case ContentLifecycleStatus.Temp:
              switch (destination.Status)
              {
                case ContentLifecycleStatus.Master:
                  RelatedDataHelper.CopyFieldRelations(contentLinksManager, parentItemId, type, property.Name, (Func<ContentLink, bool>) (cl => cl.AvailableForMaster != cl.AvailableForTemp), ContentLifecycleStatus.Temp, ContentLifecycleStatus.Master);
                  continue;
                default:
                  continue;
              }
            case ContentLifecycleStatus.Live:
              switch (destination.Status)
              {
                case ContentLifecycleStatus.Master:
                  RelatedDataHelper.CopyFieldRelations(contentLinksManager, parentItemId, type, property.Name, (Func<ContentLink, bool>) (cl => cl.AvailableForMaster != cl.AvailableForLive), ContentLifecycleStatus.Live, ContentLifecycleStatus.Master);
                  continue;
                default:
                  continue;
              }
            default:
              continue;
          }
        }
      }
    }

    /// <summary>
    /// Delete all relations for specific component property of parent type
    /// </summary>
    /// <param name="contentLinksManager">The content links manager.</param>
    /// <param name="parentItemType">The type of the parent item.</param>
    /// <param name="componentPropertyName">The specific component property.</param>
    internal static void DeleteFieldRelations(
      IContentLinksManager contentLinksManager,
      string parentItemType,
      string componentPropertyName)
    {
      IQueryable<ContentLink> contentLinks = contentLinksManager.GetContentLinks();
      Expression<Func<ContentLink, bool>> predicate = (Expression<Func<ContentLink, bool>>) (c => c.ParentItemType == parentItemType && c.ComponentPropertyName == componentPropertyName);
      foreach (ContentLink contentLink in (IEnumerable<ContentLink>) contentLinks.Where<ContentLink>(predicate))
        contentLinksManager.Delete(contentLink);
    }

    /// <summary>Deletes all item relations.</summary>
    /// <param name="item">The item.</param>
    /// <param name="contentLinksManager">Content links manager.</param>
    /// <param name="handleLifecycle">The handle lifecycle.</param>
    internal static void DeleteItemRelations(
      IDataItem item,
      IContentLinksManager contentLinksManager,
      bool handleLifecycle = false)
    {
      string[] properties = RelatedDataHelper.GetRelatedDataProperties((object) item);
      IQueryable<ContentLink> contentLinks = contentLinksManager.GetContentLinks();
      string itemTypeName = item.GetType().FullName;
      Guid parentItemId = item.Id;
      ContentLifecycleStatus status = ContentLifecycleStatus.Live;
      if (item is ILifecycleDataItemGeneric lifecycleDataItemGeneric)
      {
        status = lifecycleDataItemGeneric.Status;
        if (lifecycleDataItemGeneric.OriginalContentId != Guid.Empty)
        {
          if (!handleLifecycle)
            return;
          parentItemId = lifecycleDataItemGeneric.OriginalContentId;
        }
      }
      IQueryable<ContentLink> source1 = contentLinks.Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => cl.ParentItemId == parentItemId && cl.ParentItemProviderName == item.GetProviderName() && cl.ParentItemType == itemTypeName && properties.Contains<string>(cl.ComponentPropertyName)));
      IQueryable<ContentLink> source2 = contentLinks.Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => cl.ChildItemId == parentItemId && cl.ChildItemProviderName == item.GetProviderName() && cl.ChildItemType == itemTypeName && (cl.AvailableForLive || cl.AvailableForMaster || cl.AvailableForTemp)));
      foreach (ContentLink contentLink in (IEnumerable<ContentLink>) source1.Concat<ContentLink>((IEnumerable<ContentLink>) source2))
      {
        if (handleLifecycle)
        {
          contentLink[status] = false;
          if (contentLink.AvailableForLive || contentLink.AvailableForMaster || contentLink.AvailableForTemp)
            continue;
        }
        contentLinksManager.Delete(contentLink);
      }
    }

    /// <summary>Deletes all item relations for the specified field.</summary>
    /// <param name="item">The item.</param>
    /// <param name="contentLinksManager">Content links manager.</param>
    /// <param name="fieldName">The name of the field.</param>
    /// <param name="handleLifecycle">The handle lifecycle.</param>
    internal static void DeleteItemRelations(
      IDataItem item,
      IContentLinksManager contentLinksManager,
      string fieldName,
      bool handleLifecycle = false)
    {
      IQueryable<ContentLink> contentLinks = contentLinksManager.GetContentLinks();
      string itemTypeName = item.GetType().FullName;
      Guid parentItemId = item.Id;
      ContentLifecycleStatus status = ContentLifecycleStatus.Live;
      if (item is ILifecycleDataItemGeneric lifecycleDataItemGeneric)
      {
        status = lifecycleDataItemGeneric.Status;
        if (status != ContentLifecycleStatus.Master)
        {
          if (!handleLifecycle)
            return;
          parentItemId = lifecycleDataItemGeneric.OriginalContentId;
        }
      }
      IQueryable<ContentLink> source = contentLinks;
      Expression<Func<ContentLink, bool>> predicate = (Expression<Func<ContentLink, bool>>) (cl => cl.ParentItemId == parentItemId && cl.ParentItemProviderName == item.GetProviderName() && cl.ParentItemType == itemTypeName && cl.ComponentPropertyName == fieldName);
      foreach (ContentLink contentLink in (IEnumerable<ContentLink>) source.Where<ContentLink>(predicate))
      {
        if (handleLifecycle)
        {
          contentLink[status] = false;
          if (contentLink.AvailableForLive || contentLink.AvailableForMaster || contentLink.AvailableForTemp)
            continue;
        }
        contentLinksManager.Delete(contentLink);
      }
    }

    internal static void SetItemRelationsDeleteState(
      IDataItem item,
      IContentLinksManager contentLinksManager,
      bool isDeleted)
    {
      string[] properties = RelatedDataHelper.GetRelatedDataProperties((object) item);
      IQueryable<ContentLink> contentLinks = contentLinksManager.GetContentLinks();
      string itemTypeName = item.GetType().FullName;
      IQueryable<ContentLink> source1 = contentLinks;
      Expression<Func<ContentLink, bool>> predicate1 = (Expression<Func<ContentLink, bool>>) (cl => cl.ParentItemId == item.Id && cl.ParentItemProviderName == item.GetProviderName() && cl.ParentItemType == itemTypeName && properties.Contains<string>(cl.ComponentPropertyName));
      foreach (ContentLink contentLink in (IEnumerable<ContentLink>) source1.Where<ContentLink>(predicate1))
        contentLink.IsParentDeleted = isDeleted;
      IQueryable<ContentLink> source2 = contentLinks;
      Expression<Func<ContentLink, bool>> predicate2 = (Expression<Func<ContentLink, bool>>) (cl => cl.ChildItemId == item.Id && cl.ChildItemProviderName == item.GetProviderName() && cl.ChildItemType == itemTypeName);
      foreach (ContentLink contentLink in (IEnumerable<ContentLink>) source2.Where<ContentLink>(predicate2))
        contentLink.IsChildDeleted = isDeleted;
    }

    /// <summary>
    /// Deletes all relations that are not used in the master and live item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="contentLinksManager">Content links manager.</param>
    internal static void DeleteNotUsedRelations(
      IDataItem item,
      IContentLinksManager contentLinksManager)
    {
      string[] properties = RelatedDataHelper.GetRelatedDataProperties((object) item);
      Guid itemId = item.Id;
      if (item is ILifecycleDataItemGeneric)
      {
        ILifecycleDataItemGeneric lifecycleDataItemGeneric = item as ILifecycleDataItemGeneric;
        if (lifecycleDataItemGeneric.OriginalContentId != Guid.Empty)
          itemId = lifecycleDataItemGeneric.OriginalContentId;
      }
      IQueryable<ContentLink> contentLinks = contentLinksManager.GetContentLinks();
      Expression<Func<ContentLink, bool>> predicate = (Expression<Func<ContentLink, bool>>) (cl => (cl.ParentItemId == itemId || cl.ChildItemId == itemId) && properties.Contains<string>(cl.ComponentPropertyName) && !cl.AvailableForMaster && !cl.AvailableForLive);
      foreach (ContentLink contentLink in contentLinks.Where<ContentLink>(predicate).ToList<ContentLink>().Where<ContentLink>((Func<ContentLink, bool>) (cl => !cl.AvailableForMaster && !cl.AvailableForLive)))
        contentLinksManager.Delete(contentLink);
    }

    /// <summary>Saves changed related data items.</summary>
    /// <param name="manager">The manager that saving the Main related item. It is used to get the related IContentLinksManager.</param>
    /// <param name="item">The main relating item.</param>
    /// <param name="changedRelations">Added or Removed related items.</param>
    /// <param name="copyTempRelations">A value that indicates whether to copy temp relations or not.</param>
    internal static void SaveRelatedDataChanges(
      IManager manager,
      IDataItem item,
      ContentLinkChange[] changedRelations,
      bool copyTempRelations = false)
    {
      if (item == null)
        return;
      IManager mappedRelatedManager = manager.Provider.GetMappedRelatedManager<ContentLink>(string.Empty);
      OpenAccessContentLinksProvider provider = mappedRelatedManager.Provider as OpenAccessContentLinksProvider;
      string parentProviderName = manager.Provider.Name;
      string parentItemType = item.GetType().FullName;
      Guid parentItemId = item.Id;
      ContentLifecycleStatus contentLifecycleStatus = ContentLifecycleStatus.Temp;
      if (changedRelations != null && changedRelations.Length != 0)
      {
        if (item is ILifecycleDataItemGeneric)
        {
          ILifecycleDataItemGeneric lifecycleDataItemGeneric = item as ILifecycleDataItemGeneric;
          contentLifecycleStatus = lifecycleDataItemGeneric.Status;
          parentItemId = contentLifecycleStatus == ContentLifecycleStatus.Master ? lifecycleDataItemGeneric.Id : lifecycleDataItemGeneric.OriginalContentId;
        }
        RelatedDataHelper.SaveItemRelations(item, changedRelations, new ContentLifecycleStatus?(contentLifecycleStatus), mappedRelatedManager as IContentLinksManager, parentProviderName, parentItemType, parentItemId);
      }
      if (!copyTempRelations || provider == null)
        return;
      string appName = provider.ApplicationName;
      IEnumerable<ContentLink> contentRelations1 = provider.GetDirtyItems().OfType<ContentLink>().Where<ContentLink>((Func<ContentLink, bool>) (c => c.ApplicationName == appName)).Where<ContentLink>((Func<ContentLink, bool>) (c => c.ParentItemId == parentItemId && c.ParentItemType == parentItemType && c.ParentItemProviderName == parentProviderName));
      RelatedDataHelper.CopyTempRelations(provider, contentRelations1);
      IQueryable<ContentLink> contentRelations2 = (mappedRelatedManager as IContentLinksManager).GetContentLinks().Where<ContentLink>((Expression<Func<ContentLink, bool>>) (c => c.ParentItemId == parentItemId && c.ParentItemType == parentItemType && c.ParentItemProviderName == parentProviderName));
      RelatedDataHelper.CopyTempRelations(provider, (IEnumerable<ContentLink>) contentRelations2);
    }

    /// <summary>
    /// When item does not support Lifecycle, it's relations must be copied from master to temp state, when item is opened for edit.
    /// </summary>
    /// <param name="manager">The items type manager.</param>
    /// <param name="item">The item which relations we are copying.</param>
    /// <returns>A value indicating whether, when item does not support Lifecycle, it's relations must be copied from master to temp state, when it is opened for edit.</returns>
    internal static bool CopyMasterItemRelations(IManager manager, IDataItem item)
    {
      bool flag1 = false;
      if (item != null)
      {
        IContentLinksManager mappedRelatedManager = manager.Provider.GetMappedRelatedManager<ContentLink>(string.Empty) as IContentLinksManager;
        string parentProviderName = manager.Provider.Name;
        string parentItemType = item.GetType().FullName;
        Guid parentItemId = item.Id;
        IQueryable<ContentLink> source = mappedRelatedManager.GetContentLinks().Where<ContentLink>((Expression<Func<ContentLink, bool>>) (c => c.ParentItemId == parentItemId && c.ParentItemType == parentItemType && c.ParentItemProviderName == parentProviderName && c.AvailableForMaster != c.AvailableForTemp));
        if (source.Count<ContentLink>() > 0)
        {
          flag1 = true;
          foreach (ContentLink contentLink in source.ToList<ContentLink>())
          {
            bool flag2 = contentLink[ContentLifecycleStatus.Master];
            contentLink[ContentLifecycleStatus.Temp] = flag2;
            if (!contentLink.AvailableForLive && !contentLink.AvailableForMaster && !contentLink.AvailableForTemp)
              mappedRelatedManager.Delete(contentLink);
          }
        }
      }
      return flag1;
    }

    /// <summary>Gets the related items.</summary>
    /// <param name="parentItemTypeName">Type of the parent item.</param>
    /// <param name="parentItemId">The parent item identifier.</param>
    /// <param name="parentItemProviderName">Name of the parent item provider.</param>
    /// <param name="fieldName">Name of the field.</param>
    /// <param name="status">The status of the item.</param>
    /// <returns>Collection of related items.</returns>
    internal static IQueryable GetRelatedItems(
      string parentItemTypeName,
      Guid parentItemId,
      string parentItemProviderName,
      string fieldName,
      ContentLifecycleStatus? status)
    {
      int? totalCount = new int?();
      return RelatedDataHelper.GetRelatedItems(parentItemTypeName, parentItemProviderName, parentItemId, fieldName, status, string.Empty, string.Empty, new int?(0), new int?(0), ref totalCount);
    }

    /// <summary>Gets the related items.</summary>
    /// <param name="parentItemTypeName">Type of the parent item.</param>
    /// <param name="parentItemProviderName">Name of the parent item provider.</param>
    /// <param name="parentItemId">The parent item identifier.</param>
    /// <param name="fieldName">Name of the field.</param>
    /// <param name="status">The status of the item.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="orderExpression">The order expression.</param>
    /// <param name="skip">The number of items to skip.</param>
    /// <param name="take">The number of items to take.</param>
    /// <param name="totalCount">Total count of the items that are filtered by <paramref name="filterExpression" /></param>
    /// <param name="childItemTypeName">Type of the child item.</param>
    /// <param name="childItemProviderName">Name of the child item provider.</param>
    /// <returns>Collection of related items.</returns>
    internal static IQueryable GetRelatedItems(
      string parentItemTypeName,
      string parentItemProviderName,
      Guid parentItemId,
      string fieldName,
      ContentLifecycleStatus? status,
      string filterExpression,
      string orderExpression,
      int? skip,
      int? take,
      ref int? totalCount,
      string childItemTypeName = null,
      string childItemProviderName = null)
    {
      IQueryable relatedItems = (IQueryable) null;
      if ((childItemTypeName == null || childItemProviderName == null) && TypeDescriptor.GetProperties(TypeResolutionService.ResolveType(parentItemTypeName))[fieldName] is RelatedDataPropertyDescriptor property && property.Attributes[typeof (MetaFieldAttributeAttribute)] is MetaFieldAttributeAttribute attribute)
      {
        attribute.Attributes.TryGetValue(DynamicAttributeNames.RelatedType, out childItemTypeName);
        attribute.Attributes.TryGetValue(DynamicAttributeNames.RelatedProviders, out childItemProviderName);
      }
      if (childItemProviderName == "sf-site-default-provider")
        childItemProviderName = RelatedDataHelper.ResolveProvider(childItemTypeName);
      if (childItemTypeName != null && childItemProviderName != null)
      {
        Type type = TypeResolutionService.ResolveType(childItemTypeName, false);
        if (childItemProviderName != "sf-any-site-provider")
        {
          IManager mappedManager = ManagerBase.GetMappedManager(type, childItemProviderName);
          relatedItems = !(mappedManager is IRelatedDataSource relatedDataSource) ? RelatedDataHelper.GetRelatedItemsViaContains(parentItemTypeName, parentItemProviderName, parentItemId, fieldName, type, status, mappedManager.Provider, filterExpression, orderExpression, skip, take, ref totalCount) : relatedDataSource.GetRelatedItems(parentItemTypeName, parentItemProviderName, parentItemId, fieldName, type, status, filterExpression, orderExpression, skip, take, ref totalCount);
        }
        else
          relatedItems = RelatedDataHelper.GetCrossProvidersRelatedItems(type, parentItemTypeName, parentItemProviderName, parentItemId, fieldName, status, filterExpression, orderExpression, skip, take, ref totalCount);
      }
      return relatedItems;
    }

    internal static IQueryable GetCrossProvidersRelatedItems(
      Type childItemType,
      string parentItemTypeName,
      string parentItemProviderName,
      Guid parentItemId,
      string fieldName,
      ContentLifecycleStatus? status,
      string filterExpression,
      string orderExpression,
      int? skip,
      int? take,
      ref int? totalCount)
    {
      IQueryable<ContentLink> queryable = ContentLinksManager.GetManager().GetContentLinks().Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => cl.ParentItemType == parentItemTypeName && cl.ParentItemProviderName == parentItemProviderName && cl.ChildItemType == childItemType.FullName));
      if (parentItemId != Guid.Empty)
        queryable = queryable.Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => cl.ParentItemId == parentItemId));
      List<ContentLink> list1 = RelatedDataHelper.ApplyLinksFilters(RelatedDataHelper.ApplyDeletedLinksFilters(queryable, RelationDirection.Child), fieldName, status).OrderBy<ContentLink, float>((Expression<Func<ContentLink, float>>) (cl => cl.Ordinal)).ToList<ContentLink>();
      List<string> list2 = list1.GroupBy<ContentLink, string>((Func<ContentLink, string>) (cl => cl.ChildItemProviderName)).Select<IGrouping<string, ContentLink>, string>((Func<IGrouping<string, ContentLink>, string>) (g => g.Key)).ToList<string>();
      List<IDataItem> items = new List<IDataItem>();
      foreach (string providerName in list2)
      {
        if (RelatedDataHelper.IsModuleEnabledForCurrentSite(childItemType, providerName))
        {
          IManager mappedManager = ManagerBase.GetMappedManager(childItemType, providerName);
          int? totalCount1 = new int?();
          IQueryable source = !(mappedManager is IRelatedDataSource) ? RelatedDataHelper.GetRelatedItemsViaContains(parentItemTypeName, parentItemProviderName, parentItemId, fieldName, childItemType, status, mappedManager.Provider, filterExpression, orderExpression, new int?(), new int?(), ref totalCount1) : (mappedManager as IRelatedDataSource).GetRelatedItems(parentItemTypeName, parentItemProviderName, parentItemId, fieldName, childItemType, status, filterExpression, orderExpression, new int?(), new int?(), ref totalCount1);
          items.AddRange((IEnumerable<IDataItem>) Queryable.OfType<IDataItem>(source));
        }
      }
      int? nullable;
      if (totalCount.HasValue)
      {
        totalCount = new int?(items.Count);
        nullable = totalCount;
        int num = 0;
        if (nullable.GetValueOrDefault() == num & nullable.HasValue)
          return (IQueryable) Enumerable.Empty<IDataItem>().AsQueryable<IDataItem>();
      }
      IQueryable<IDataItem> source1 = RelatedDataHelper.SortRelatedItems<IDataItem>((IEnumerable<ContentLink>) list1, (IEnumerable<IDataItem>) items, status).AsQueryable<IDataItem>();
      if (!string.IsNullOrEmpty(orderExpression))
        source1 = source1.OrderBy<IDataItem>(orderExpression);
      if (skip.HasValue)
      {
        nullable = skip;
        int num = 0;
        if (nullable.GetValueOrDefault() > num & nullable.HasValue)
          source1 = source1.Skip<IDataItem>(skip.Value);
      }
      if (take.HasValue)
      {
        nullable = take;
        int num = 0;
        if (nullable.GetValueOrDefault() > num & nullable.HasValue)
          source1 = source1.Take<IDataItem>(take.Value);
      }
      return (IQueryable) source1;
    }

    internal static Dictionary<Guid, List<IDataItem>> GetRelatedItems(
      string parentItemTypeName,
      string parentItemProviderName,
      List<Guid> parentItemIds,
      string fieldName,
      ContentLifecycleStatus? status,
      string childItemTypeName = null,
      string childItemProviderName = null)
    {
      Dictionary<Guid, List<IDataItem>> relatedItems = new Dictionary<Guid, List<IDataItem>>();
      if ((childItemTypeName == null || childItemProviderName == null) && TypeDescriptor.GetProperties(TypeResolutionService.ResolveType(parentItemTypeName))[fieldName] is RelatedDataPropertyDescriptor property && property.Attributes[typeof (MetaFieldAttributeAttribute)] is MetaFieldAttributeAttribute attribute)
      {
        attribute.Attributes.TryGetValue(DynamicAttributeNames.RelatedType, out childItemTypeName);
        attribute.Attributes.TryGetValue(DynamicAttributeNames.RelatedProviders, out childItemProviderName);
      }
      if (childItemProviderName == "sf-site-default-provider")
        childItemProviderName = RelatedDataHelper.ResolveProvider(childItemTypeName);
      if (childItemTypeName != null && childItemProviderName != null)
      {
        Type type = TypeResolutionService.ResolveType(childItemTypeName, false);
        if (childItemProviderName != "sf-any-site-provider")
        {
          IManager mappedManager = ManagerBase.GetMappedManager(type, childItemProviderName);
          relatedItems = !(mappedManager is IRelatedDataSource) ? RelatedDataHelper.GetRelatedItemsViaContains(parentItemTypeName, parentItemProviderName, parentItemIds, fieldName, type, mappedManager.Provider, status) : (mappedManager as IRelatedDataSource).GetRelatedItems(parentItemTypeName, parentItemProviderName, parentItemIds, fieldName, type, status);
        }
        else
          relatedItems = RelatedDataHelper.GetCrossProvidersRelatedItems(parentItemTypeName, parentItemProviderName, new Collection<Guid>((IList<Guid>) parentItemIds), fieldName, type, status);
      }
      return relatedItems;
    }

    internal static Dictionary<Guid, List<IDataItem>> GetCrossProvidersRelatedItems(
      string itemType,
      string itemProviderName,
      Collection<Guid> parentItemIds,
      string fieldName,
      Type relatedItemsType,
      ContentLifecycleStatus? status)
    {
      IQueryable<ContentLink> queryable = ContentLinksManager.GetManager().GetContentLinks().Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => cl.ParentItemType == itemType && cl.ParentItemProviderName == itemProviderName && cl.ChildItemType == relatedItemsType.FullName));
      if (parentItemIds != null && parentItemIds.Count<Guid>() > 0)
        queryable = queryable.Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => parentItemIds.Contains(cl.ParentItemId)));
      List<ContentLink> list1 = RelatedDataHelper.ApplyLinksFilters(RelatedDataHelper.ApplyDeletedLinksFilters(queryable, RelationDirection.Child), fieldName, status).ToList<ContentLink>();
      List<string> list2 = list1.GroupBy<ContentLink, string>((Func<ContentLink, string>) (cl => cl.ChildItemProviderName)).Select<IGrouping<string, ContentLink>, string>((Func<IGrouping<string, ContentLink>, string>) (g => g.Key)).ToList<string>();
      List<IDataItem> source = new List<IDataItem>();
      foreach (string providerName in list2)
      {
        if (RelatedDataHelper.IsModuleEnabledForCurrentSite(relatedItemsType, providerName))
        {
          IManager mappedManager = ManagerBase.GetMappedManager(relatedItemsType, providerName);
          IEnumerable<IDataItem> collection = !(mappedManager is IRelatedDataSource relatedDataSource) ? RelatedDataHelper.GetRelatedItemsListViaContains(itemType, itemProviderName, parentItemIds, fieldName, relatedItemsType, mappedManager.Provider, status) : relatedDataSource.GetRelatedItemsList(itemType, itemProviderName, parentItemIds, fieldName, relatedItemsType, status);
          source.AddRange(collection);
        }
      }
      IEnumerable<IGrouping<Guid, ContentLink>> groupings = list1.GroupBy<ContentLink, Guid>((Func<ContentLink, Guid>) (l => l.ParentItemId));
      Dictionary<Guid, List<IDataItem>> providersRelatedItems = new Dictionary<Guid, List<IDataItem>>();
      foreach (IGrouping<Guid, ContentLink> grouping in groupings)
      {
        IGrouping<Guid, ContentLink> linkGroup = grouping;
        IEnumerable<IDataItem> items = source.Where<IDataItem>((Func<IDataItem, bool>) (ri => linkGroup.Any<ContentLink>((Func<ContentLink, bool>) (l => l.ChildItemId == RelatedDataExtensions.GetId((object) ri)))));
        List<IDataItem> list3 = RelatedDataHelper.SortRelatedItems<IDataItem>((IEnumerable<ContentLink>) linkGroup, items, status).ToList<IDataItem>();
        providersRelatedItems.Add(linkGroup.Key, list3);
      }
      return providersRelatedItems;
    }

    /// <summary>Gets the items relating to the specified item.</summary>
    /// <param name="childItemType">Type of the child item.</param>
    /// <param name="childItemProviderName">Name of the child item provider.</param>
    /// <param name="childItemId">The child item identifier.</param>
    /// <param name="relatingFieldName">Name of the relating field.</param>
    /// <param name="status">The lifecycle status.</param>
    /// <returns>Collection of related parent items.</returns>
    internal static IEnumerable GetRelatedParentItems(
      string childItemType,
      string childItemProviderName,
      Guid childItemId,
      string relatingFieldName,
      ContentLifecycleStatus status)
    {
      IEnumerable<IDataItem> first = Enumerable.Empty<IDataItem>();
      IQueryable<ContentLink> source1 = ContentLinksManager.GetManager().GetContentLinks().Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => cl.ChildItemType == childItemType && cl.ChildItemProviderName == childItemProviderName));
      if (childItemId != Guid.Empty)
        source1 = source1.Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => cl.ChildItemId == childItemId));
      if (!string.IsNullOrEmpty(relatingFieldName))
        source1 = source1.Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => cl.ComponentPropertyName == relatingFieldName));
      IQueryable<ContentLink> source2 = source1;
      Expression<Func<ContentLink, \u003C\u003Ef__AnonymousType31<string, string>>> keySelector = cl => new
      {
        ParentItemType = cl.ParentItemType,
        ParentItemProviderName = cl.ParentItemProviderName
      };
      foreach (IGrouping<\u003C\u003Ef__AnonymousType31<string, string>, ContentLink> grouping in source2.GroupBy(keySelector))
      {
        List<IDataItem> list = Queryable.Cast<IDataItem>(RelatedDataHelper.GetRelatedParentItems(childItemType, childItemProviderName, childItemId, grouping.Key.ParentItemType, grouping.Key.ParentItemProviderName, relatingFieldName, status)).ToList<IDataItem>();
        first = first.Concat<IDataItem>((IEnumerable<IDataItem>) list);
      }
      return (IEnumerable) first;
    }

    /// <summary>Gets the items relating to the specified item.</summary>
    /// <param name="childItemType">Type of the child item.</param>
    /// <param name="childItemProviderName">Name of the child item provider.</param>
    /// <param name="childItemId">The child item identifier.</param>
    /// <param name="itemTypeName">Name of the item type.</param>
    /// <param name="itemProviderName">Name of the item provider.</param>
    /// <param name="relatingFieldName">Name of the relating field.</param>
    /// <param name="status">The lifecycle status.</param>
    /// <returns>Collection of related parent items.</returns>
    internal static IQueryable GetRelatedParentItems(
      string childItemType,
      string childItemProviderName,
      Guid childItemId,
      string itemTypeName,
      string itemProviderName,
      string relatingFieldName,
      ContentLifecycleStatus status)
    {
      int? totalCount = new int?();
      return RelatedDataHelper.GetRelatedParentItems(childItemType, childItemProviderName, childItemId, itemTypeName, itemProviderName, relatingFieldName, status, (string) null, (string) null, new int?(0), new int?(0), ref totalCount);
    }

    /// <summary>Gets the items relating the specified item.</summary>
    /// <param name="childItemType">Type of the child item.</param>
    /// <param name="childItemProviderName">Name of the child item provider.</param>
    /// <param name="childItemId">The child item identifier.</param>
    /// <param name="itemTypeName">Name of the item type.</param>
    /// <param name="itemProviderName">Name of the item provider.</param>
    /// <param name="relatingFieldName">Name of the relating field.</param>
    /// <param name="status">The status.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="orderExpression">The order expression.</param>
    /// <param name="skip">Items to skip</param>
    /// <param name="take">Items to take</param>
    /// <param name="totalCount">The total count.</param>
    /// <returns>Collection of related parent items.</returns>
    internal static IQueryable GetRelatedParentItems(
      string childItemType,
      string childItemProviderName,
      Guid childItemId,
      string itemTypeName,
      string itemProviderName,
      string relatingFieldName,
      ContentLifecycleStatus status,
      string filterExpression,
      string orderExpression,
      int? skip,
      int? take,
      ref int? totalCount)
    {
      IManager mappedManager = ManagerBase.GetMappedManager(itemTypeName, itemProviderName);
      return !(mappedManager is IRelatedDataSource) ? RelatedDataHelper.GetRelatedParentItemsViaContains(childItemType, childItemProviderName, childItemId, relatingFieldName, TypeResolutionService.ResolveType(itemTypeName), new ContentLifecycleStatus?(status), mappedManager.Provider, filterExpression, orderExpression, skip, take, ref totalCount) : (mappedManager as IRelatedDataSource).GetRelatedItems(childItemType, childItemProviderName, childItemId, relatingFieldName, TypeResolutionService.ResolveType(itemTypeName), new ContentLifecycleStatus?(status), filterExpression, orderExpression, skip, take, ref totalCount, RelationDirection.Parent);
    }

    /// <summary>Gets the related content links.</summary>
    /// <param name="parentItemTypeName">Name of the parent item type.</param>
    /// <param name="parentItemId">The parent item identifier.</param>
    /// <param name="parentItemProviderName">Name of the parent item provider.</param>
    /// <param name="fieldName">Name of the field.</param>
    /// <param name="status">The status.</param>
    /// <returns>Collection of related content links.</returns>
    internal static IEnumerable GetRelatedContentLinks(
      string parentItemTypeName,
      Guid parentItemId,
      string parentItemProviderName,
      string fieldName,
      ContentLifecycleStatus status)
    {
      IQueryable<ContentLink> source = ContentLinksManager.GetManager().GetContentLinks().Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => cl.ParentItemType == parentItemTypeName && cl.ParentItemProviderName == parentItemProviderName));
      if (parentItemId != Guid.Empty)
        source = source.Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => cl.ParentItemId == parentItemId));
      if (!string.IsNullOrEmpty(fieldName))
        source = source.Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => cl.ComponentPropertyName == fieldName));
      switch (status)
      {
        case ContentLifecycleStatus.Master:
          source = source.Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => cl.AvailableForMaster));
          break;
        case ContentLifecycleStatus.Temp:
          source = source.Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => cl.AvailableForTemp));
          break;
        case ContentLifecycleStatus.Live:
          source = source.Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => cl.AvailableForLive));
          break;
      }
      return (IEnumerable) source;
    }

    /// <summary>
    /// Joins the provided collection of items with the provided links, taking into consideration the status.
    /// </summary>
    /// <typeparam name="T">The generic type parameter.</typeparam>
    /// <param name="items">Collection of items to join with the provided links.</param>
    /// <param name="links">Collection of links to join with the provided items.</param>
    /// <param name="status">The lifecycle status of the items.</param>
    /// <returns>Collection joined from the provided items and content links.</returns>
    internal static IQueryable<T> JoinRelatedItems<T>(
      IQueryable<T> items,
      IQueryable<ContentLink> links,
      ContentLifecycleStatus? status)
      where T : ILifecycleDataItemGeneric
    {
      if (status.HasValue)
      {
        ContentLifecycleStatus? nullable = status;
        ContentLifecycleStatus contentLifecycleStatus = ContentLifecycleStatus.Live;
        if (nullable.GetValueOrDefault() == contentLifecycleStatus & nullable.HasValue)
          return items.Where<T>((Expression<Func<T, bool>>) (i => (int?) i.Status == (int?) status)).Join<T, ContentLink, Guid, T>((IEnumerable<ContentLink>) links, (Expression<Func<T, Guid>>) (i => i.OriginalContentId), (Expression<Func<ContentLink, Guid>>) (cl => cl.ChildItemId), (Expression<Func<T, ContentLink, T>>) ((i, cl) => i));
      }
      return items.Join<T, ContentLink, Guid, T>((IEnumerable<ContentLink>) links, (Expression<Func<T, Guid>>) (i => i.Id), (Expression<Func<ContentLink, Guid>>) (cl => cl.ChildItemId), (Expression<Func<T, ContentLink, T>>) ((i, cl) => i));
    }

    /// <summary>
    /// Sorts the provided collection of items based on the ordinal of the content links, taking into consideration the status.
    /// </summary>
    /// <typeparam name="T">The generic type parameter.</typeparam>
    /// <param name="links">Collection of links to join with the provided items.</param>
    /// <param name="items">Collection of items to be sorted.</param>
    /// <param name="status">The requested lifecycle status of the items, if applicable.</param>
    /// <returns>Ordered collection from the provided items.</returns>
    internal static IOrderedEnumerable<T> SortRelatedItems<T>(
      IEnumerable<ContentLink> links,
      IEnumerable<T> items,
      ContentLifecycleStatus? status)
      where T : IDataItem
    {
      List<Guid> orderedItemsIds = links.OrderBy<ContentLink, float>((Func<ContentLink, float>) (cl => cl.Ordinal)).Select<ContentLink, Guid>((Func<ContentLink, Guid>) (cl => cl.ChildItemId)).ToList<Guid>();
      T obj1 = items.FirstOrDefault<T>();
      ref T local1 = ref obj1;
      Type type1;
      if ((object) default (T) == null)
      {
        T obj2 = local1;
        ref T local2 = ref obj2;
        if ((object) obj2 == null)
        {
          type1 = (Type) null;
          goto label_4;
        }
        else
          local1 = ref local2;
      }
      type1 = local1.GetType();
label_4:
      Type type2 = type1;
      IOrderedEnumerable<T> orderedEnumerable;
      if (type2 != (Type) null && type2.ImplementsInterface(typeof (ILifecycleDataItemGeneric)) && status.HasValue)
      {
        ContentLifecycleStatus? nullable = status;
        ContentLifecycleStatus contentLifecycleStatus = ContentLifecycleStatus.Live;
        if (nullable.GetValueOrDefault() == contentLifecycleStatus & nullable.HasValue)
        {
          orderedEnumerable = items.OrderBy<T, int>((Func<T, int>) (i => orderedItemsIds.IndexOf(((ILifecycleDataItemGeneric) (object) i).OriginalContentId)));
          goto label_8;
        }
      }
      orderedEnumerable = items.OrderBy<T, int>((Func<T, int>) (i => orderedItemsIds.IndexOf(i.Id)));
label_8:
      return orderedEnumerable;
    }

    /// <summary>
    /// Joins the provided collection of items with the provided links, taking into consideration the status.
    /// </summary>
    /// <typeparam name="T">The generic type parameter.</typeparam>
    /// <param name="items">Collection of items to join with the provided links.</param>
    /// <param name="links">Collection of links to join with the provided items.</param>
    /// <param name="status">The lifecycle status of the items.</param>
    /// <returns>Collection joined from the provided items and content links.</returns>
    internal static IQueryable<T> JoinRelatingItems<T>(
      IQueryable<T> items,
      IQueryable<ContentLink> links,
      ContentLifecycleStatus? status)
      where T : ILifecycleDataItemGeneric
    {
      if (status.HasValue)
      {
        ContentLifecycleStatus? nullable = status;
        ContentLifecycleStatus contentLifecycleStatus = ContentLifecycleStatus.Live;
        if (nullable.GetValueOrDefault() == contentLifecycleStatus & nullable.HasValue)
          return items.Where<T>((Expression<Func<T, bool>>) (i => (int?) i.Status == (int?) status)).Join<T, ContentLink, Guid, T>((IEnumerable<ContentLink>) links, (Expression<Func<T, Guid>>) (i => i.OriginalContentId), (Expression<Func<ContentLink, Guid>>) (cl => cl.ParentItemId), (Expression<Func<T, ContentLink, T>>) ((i, cl) => i));
      }
      return items.Join<T, ContentLink, Guid, T>((IEnumerable<ContentLink>) links, (Expression<Func<T, Guid>>) (i => i.Id), (Expression<Func<ContentLink, Guid>>) (cl => cl.ParentItemId), (Expression<Func<T, ContentLink, T>>) ((i, cl) => i));
    }

    /// <summary>
    /// Applies the links filter expression according to the item's status and fieldName.
    /// </summary>
    /// <param name="links">Collection of content links to filter.</param>
    /// <param name="fieldName">The item's field name filter.</param>
    /// <param name="status">The lifecycle status filter.</param>
    /// <returns>Collection of filtered content links.</returns>
    internal static IQueryable<ContentLink> ApplyLinksFilters(
      IQueryable<ContentLink> links,
      string fieldName,
      ContentLifecycleStatus? status)
    {
      if (!string.IsNullOrEmpty(fieldName))
        links = links.Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => cl.ComponentPropertyName == fieldName));
      if (!status.HasValue)
      {
        links = links.Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => cl.AvailableForTemp));
      }
      else
      {
        ContentLifecycleStatus? nullable;
        if (((IContentLocatableView) null).IsPreviewRequested())
        {
          nullable = status;
          ContentLifecycleStatus contentLifecycleStatus = ContentLifecycleStatus.Live;
          if (nullable.GetValueOrDefault() == contentLifecycleStatus & nullable.HasValue)
          {
            links = links.Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => cl.AvailableForTemp));
            goto label_12;
          }
        }
        nullable = status;
        if (nullable.HasValue)
        {
          switch (nullable.GetValueOrDefault())
          {
            case ContentLifecycleStatus.Master:
              links = links.Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => cl.AvailableForMaster));
              break;
            case ContentLifecycleStatus.Temp:
              links = links.Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => cl.AvailableForTemp));
              break;
            case ContentLifecycleStatus.Live:
              links = links.Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => cl.AvailableForLive));
              break;
          }
        }
      }
label_12:
      return links;
    }

    /// <summary>Applies a filter by deleted content links.</summary>
    /// <param name="links">Collection of content links to filter.</param>
    /// <param name="direction">The direction of the relation filter.</param>
    /// <returns>Collection of filtered content links.</returns>
    internal static IQueryable<ContentLink> ApplyDeletedLinksFilters(
      IQueryable<ContentLink> links,
      RelationDirection direction)
    {
      if (direction == RelationDirection.Parent)
        links = links.Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => !cl.IsParentDeleted));
      else
        links = links.Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => !cl.IsChildDeleted));
      return links;
    }

    /// <summary>Returns the lifecycle status of an item.</summary>
    /// <param name="item">The item.</param>
    /// <param name="manager">The <see cref="T:Telerik.Sitefinity.Lifecycle.ILifecycleManager" /> manager.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Services.RelatedData.Responses.LifecycleResponse" /> object with the status of the item.</returns>
    internal static LifecycleResponse GetItemLifecycleStatus(
      ILifecycleDataItem item,
      ILifecycleManager manager)
    {
      LifecycleResponse itemLifecycleStatus = new LifecycleResponse();
      bool flag = false;
      switch (item)
      {
        case DynamicContent _:
          if (!(item is DynamicContent dynamicContent))
            throw new ArgumentNullException("dynamicItem");
          dynamicContent.PopulateLifecycleInformation();
          itemLifecycleStatus.WorkflowStatus = dynamicContent.Lifecycle.WorkflowStatus;
          itemLifecycleStatus.DisplayStatus = dynamicContent.Lifecycle.Message;
          itemLifecycleStatus.IsLocked = dynamicContent.Lifecycle.IsLocked;
          itemLifecycleStatus.IsLockedByMe = dynamicContent.Lifecycle.IsLockedByMe;
          break;
        case Content _:
          flag = true;
          break;
        default:
          Type type = TypeResolutionService.ResolveType("Telerik.Sitefinity.Ecommerce.Catalog.Model.Product", false);
          if (type != (Type) null && type.IsAssignableFrom(item.GetType()))
          {
            flag = true;
            break;
          }
          break;
      }
      if (flag)
      {
        if (item == null)
          throw new ArgumentNullException("lifecycleDataItem");
        if (manager == null)
          throw new ArgumentNullException(nameof (manager));
        CultureInfo culture = SystemManager.CurrentContext.Culture;
        LifecycleSimpleInfo lifecycleSimpleInfo = item.GetLifecycleSimpleInfo(manager, culture);
        itemLifecycleStatus.WorkflowStatus = lifecycleSimpleInfo.WorkflowStatus;
        itemLifecycleStatus.DisplayStatus = lifecycleSimpleInfo.DisplayStatus;
        itemLifecycleStatus.IsLocked = lifecycleSimpleInfo.IsLocked;
        itemLifecycleStatus.IsLockedByMe = lifecycleSimpleInfo.IsLockedByMe;
      }
      return itemLifecycleStatus;
    }

    /// <summary>Returns the lifecycle status of a page.</summary>
    /// <param name="pageNode">The page.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Services.RelatedData.Responses.LifecycleResponse" /> object with the status of the page.</returns>
    internal static LifecycleResponse GetPageLifecycleStatus(PageNode pageNode)
    {
      if (pageNode == null)
        throw new ArgumentNullException(nameof (pageNode));
      LifecycleResponse pageLifecycleStatus = new LifecycleResponse();
      string statusKey = pageNode.ApprovalWorkflowState.ToString();
      string statusText = pageNode.GetLocalizedStatus();
      PageData pageData = pageNode.GetPageData();
      if (pageData != null)
      {
        LifecycleExtensions.GetOverallStatus((ILifecycleDataItemLive) pageData, ((CultureInfo) null).GetSitefinityCulture(), ref statusKey, ref statusText);
        SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
        pageLifecycleStatus.IsLocked = pageData.LockedBy != Guid.Empty;
        pageLifecycleStatus.IsLockedByMe = pageData.LockedBy == currentIdentity.UserId;
      }
      else if (pageNode.NodeType == NodeType.Group)
      {
        statusKey = "group";
        statusText = Res.Get<PageResources>().Group;
      }
      else if (pageNode.NodeType == NodeType.InnerRedirect || pageNode.NodeType == NodeType.OuterRedirect)
      {
        statusKey = "available";
        statusText = Res.Get<PageResources>().RedirectingPage;
      }
      pageLifecycleStatus.WorkflowStatus = statusKey;
      pageLifecycleStatus.DisplayStatus = statusText;
      return pageLifecycleStatus;
    }

    internal static void CheckRelatingData(
      Dictionary<string, string> workflowContextBag,
      Guid itemId,
      string itemType)
    {
      RelatedDataHelper.CheckRelatingData(workflowContextBag.ContainsKey(nameof (CheckRelatingData)) && Convert.ToBoolean(workflowContextBag[nameof (CheckRelatingData)]), itemId, itemType);
    }

    internal static void CheckRelatingData(bool checkRelatingData, Guid itemId, string itemType)
    {
      if (checkRelatingData && RelatedDataHelper.HasRelatingDataItems(itemId, itemType))
        throw new WebProtocolException(HttpStatusCode.PreconditionFailed, Res.Get<Labels>().CheckRelatingDataPreconditionsFailed, (Exception) null);
    }

    internal static bool IsTypeSupportCheckRelatingData(Type sourceType)
    {
      if (!Config.Get<RelatedDataConfig>().CheckRelatingDataWarning)
        return false;
      return TypeResolutionService.ResolveType("Telerik.Sitefinity.News.Model.NewsItem").IsAssignableFrom(sourceType) || TypeResolutionService.ResolveType("Telerik.Sitefinity.Blogs.Model.BlogPost").IsAssignableFrom(sourceType) || TypeResolutionService.ResolveType("Telerik.Sitefinity.Events.Model.Event").IsAssignableFrom(sourceType) || typeof (DynamicContent).IsAssignableFrom(sourceType) || typeof (PageNode).IsAssignableFrom(sourceType) || SystemManager.IsModuleEnabled("Ecommerce") && TypeResolutionService.ResolveType("Telerik.Sitefinity.Ecommerce.Catalog.Model.Product").IsAssignableFrom(sourceType) || typeof (Telerik.Sitefinity.Libraries.Model.Image).IsAssignableFrom(sourceType) || typeof (Telerik.Sitefinity.Libraries.Model.Document).IsAssignableFrom(sourceType) || typeof (Telerik.Sitefinity.Libraries.Model.Video).IsAssignableFrom(sourceType);
    }

    internal static bool IsModuleEnabledForCurrentSite(Type contentType, string providerName)
    {
      if (contentType == (Type) null)
        return false;
      if (TypeResolutionService.ResolveType("Telerik.Sitefinity.News.Model.NewsItem").IsAssignableFrom(contentType))
        return RelatedDataHelper.IsModuleEnabledForCurrentSite("Telerik.Sitefinity.Modules.News.NewsManager", "News", providerName);
      if (TypeResolutionService.ResolveType("Telerik.Sitefinity.Blogs.Model.BlogPost").IsAssignableFrom(contentType))
        return RelatedDataHelper.IsModuleEnabledForCurrentSite("Telerik.Sitefinity.Modules.Blogs.BlogsManager", "Blogs", providerName);
      if (TypeResolutionService.ResolveType("Telerik.Sitefinity.Events.Model.Event").IsAssignableFrom(contentType))
        return RelatedDataHelper.IsModuleEnabledForCurrentSite("Telerik.Sitefinity.Modules.Events.EventsManager", "Events", providerName);
      if (typeof (PageNode).IsAssignableFrom(contentType))
        return true;
      if (SystemManager.IsModuleEnabled("Ecommerce") && TypeResolutionService.ResolveType("Telerik.Sitefinity.Ecommerce.Catalog.Model.Product").IsAssignableFrom(contentType))
        return RelatedDataHelper.IsModuleEnabledForCurrentSite("Telerik.Sitefinity.Modules.Ecommerce.Catalog.CatalogManager", "Ecommerce", providerName);
      if (typeof (DynamicContent).IsAssignableFrom(contentType))
      {
        DynamicModuleType dynamicModuleType = ModuleBuilderManager.GetManager().GetDynamicModuleType(contentType);
        return RelatedDataHelper.IsModuleEnabledForCurrentSite(dynamicModuleType.ModuleName, dynamicModuleType.ModuleName, providerName);
      }
      return !typeof (Telerik.Sitefinity.Libraries.Model.Image).IsAssignableFrom(contentType) && !typeof (Telerik.Sitefinity.Libraries.Model.Video).IsAssignableFrom(contentType) && !typeof (Telerik.Sitefinity.Libraries.Model.Document).IsAssignableFrom(contentType) || RelatedDataHelper.IsModuleEnabledForCurrentSite(typeof (LibrariesManager).FullName, "Libraries", providerName);
    }

    /// <summary>
    /// Checks if a module is enabled for the current site with specific provider
    /// </summary>
    /// <param name="dataSourceName">The name of the data source of the module</param>
    /// <param name="moduleName">The name of the module</param>
    /// <param name="providerName">The name of the provider</param>
    /// <returns>A value that indicates whether the module is enabled for the current site.</returns>
    internal static bool IsModuleEnabledForCurrentSite(
      string dataSourceName,
      string moduleName,
      string providerName)
    {
      if (!SystemManager.IsModuleAccessible(moduleName))
        return false;
      return string.IsNullOrEmpty(providerName) || !(providerName != "sf-any-site-provider") || !(providerName != "sf-site-default-provider") || dataSourceName == typeof (LibrariesManager).FullName && providerName == "SystemLibrariesProvider" || SystemManager.CurrentContext.CurrentSite.GetProviders(dataSourceName).Any<MultisiteContext.SiteDataSourceLinkProxy>((Func<MultisiteContext.SiteDataSourceLinkProxy, bool>) (ds => ds.ProviderName == providerName));
    }

    /// <summary>Gets the related items via contains.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="itemProviderName">Name of the item provider.</param>
    /// <param name="itemId">The item identifier.</param>
    /// <param name="fieldName">Name of the field.</param>
    /// <param name="relatedItemsType">Type of the related items.</param>
    /// <param name="status">The status.</param>
    /// <param name="itemsProvider">The items provider.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="orderExpression">The order expression.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <param name="totalCount">The total count.</param>
    /// <param name="relationDirection">The relation direction.</param>
    /// <returns>Collection of related items.</returns>
    internal static IQueryable GetRelatedItemsViaContains(
      string itemType,
      string itemProviderName,
      Guid itemId,
      string fieldName,
      Type relatedItemsType,
      ContentLifecycleStatus? status,
      DataProviderBase itemsProvider,
      string filterExpression,
      string orderExpression,
      int? skip,
      int? take,
      ref int? totalCount,
      RelationDirection relationDirection = RelationDirection.Child)
    {
      return relationDirection == RelationDirection.Child ? RelatedDataHelper.GetRelatedItemsViaContains(itemType, itemProviderName, itemId, fieldName, relatedItemsType, status, itemsProvider, filterExpression, orderExpression, skip, take, ref totalCount) : RelatedDataHelper.GetRelatedParentItemsViaContains(itemType, itemProviderName, itemId, fieldName, relatedItemsType, status, itemsProvider, filterExpression, orderExpression, skip, take, ref totalCount);
    }

    /// <summary>Gets the item type title.</summary>
    /// <param name="defaultTitle">The default title. Will be used if itemType cannot be resolved.</param>
    /// <param name="itemTypeFullName">Full name of the item type.</param>
    /// <param name="toPlural">To plural.</param>
    /// <returns>The item type's title.</returns>
    internal static string GetItemTypeTitle(
      string defaultTitle,
      string itemTypeFullName,
      bool toPlural)
    {
      SitefinityType type = SystemManager.TypeRegistry.GetType(itemTypeFullName);
      if (type == null)
        return defaultTitle;
      return !toPlural ? type.SingularTitle : type.PluralTitle;
    }

    /// <summary>Populates the dynamic items lifecycle information</summary>
    /// <param name="dynamicItems">Collection of dynamic items</param>
    /// <param name="currentUiCulture">The current UI culture.</param>
    internal static void PopulateLifecycleInformation(
      List<DynamicContent> dynamicItems,
      string currentUiCulture)
    {
      bool multilingual = SystemManager.CurrentContext.AppSettings.Multilingual;
      foreach (object obj in dynamicItems.GroupBy<DynamicContent, object>((Func<DynamicContent, object>) (i => i.Provider)).Select<IGrouping<object, DynamicContent>, object>((Func<IGrouping<object, DynamicContent>, object>) (g => g.Key)).ToList<object>())
      {
        object provider = obj;
        string name = ((DataProviderBase) provider).Name;
        List<DynamicContent> list = dynamicItems.Where<DynamicContent>((Func<DynamicContent, bool>) (di => di.Provider == provider)).ToList<DynamicContent>();
        Guid[] itemIds = RelatedDataHelper.GetItemIDs((IList<DynamicContent>) list);
        Type[] array = list.Select<DynamicContent, Type>((Func<DynamicContent, Type>) (x => x.GetType())).Distinct<Type>().ToArray<Type>();
        List<DynamicContent> tempAndLiveRecords = RelatedDataHelper.GetTempAndLiveRecords(itemIds, name, array);
        foreach (DynamicContent dynamicContent in list)
        {
          DynamicContent dynamicItem = dynamicContent;
          if (tempAndLiveRecords == null)
          {
            dynamicItem.PopulateLifecycleInformation();
          }
          else
          {
            DynamicContent live1 = tempAndLiveRecords.FirstOrDefault<DynamicContent>((Func<DynamicContent, bool>) (live => live.OriginalContentId == dynamicItem.Id && live.Status == ContentLifecycleStatus.Live));
            IEnumerable<DynamicContent> dynamicContents = tempAndLiveRecords.Where<DynamicContent>((Func<DynamicContent, bool>) (temp => temp.OriginalContentId == dynamicItem.Id && temp.Status == ContentLifecycleStatus.Temp));
            ILifecycleDataItemGeneric temp1 = (ILifecycleDataItemGeneric) null;
            foreach (ILifecycleDataItemGeneric lifecycleDataItemGeneric in dynamicContents)
            {
              if (multilingual)
              {
                if (lifecycleDataItemGeneric.PublishedTranslations.Any<string>((Func<string, bool>) (l => l == currentUiCulture)))
                {
                  temp1 = lifecycleDataItemGeneric;
                  break;
                }
              }
              else
              {
                temp1 = lifecycleDataItemGeneric;
                break;
              }
            }
            LifecycleItemExtensions.SetStatus((ILifecycleDataItem) live1, (ILifecycleDataItem) dynamicItem, (ILifecycleDataItem) temp1);
          }
        }
      }
    }

    /// <summary>Checks if a secured object is modifiable (editable).</summary>
    /// <param name="secObj">The secured object.Can be null.</param>
    /// <returns>true if modify is granted, or if secured object is null (it is not a secured object)</returns>
    internal static bool IsContentItemEditable(ISecuredObject secObj)
    {
      if (secObj == null)
        return true;
      string empty = string.Empty;
      string permissionSetName = !"Telerik.Sitefinity.Blogs.Model.Blog".Equals(secObj.GetType().FullName) ? (secObj.SupportedPermissionSets.Length != 0 ? secObj.SupportedPermissionSets[0] : string.Empty) : "BlogPost";
      if (secObj.IsSecurityActionSupported(permissionSetName, SecurityActionTypes.Manage))
        return secObj.IsSecurityActionTypeGranted(permissionSetName, SecurityActionTypes.Manage);
      return secObj.IsSecurityActionSupported(permissionSetName, SecurityActionTypes.Modify) && secObj.IsSecurityActionTypeGranted(permissionSetName, SecurityActionTypes.Modify);
    }

    internal static string ResolveProvider(string relatedDataType)
    {
      Type mappedManagerType = ManagerBase.GetMappedManagerType(relatedDataType);
      Telerik.Sitefinity.Multisite.ISite currentSite = SystemManager.CurrentContext.CurrentSite;
      Type itemType = TypeResolutionService.ResolveType(relatedDataType);
      string siteId = currentSite.Id.ToString();
      return RelatedDataHelper.ResolveProvider(mappedManagerType, itemType, siteId);
    }

    internal static string ResolveProvider(Type managerType, Type itemType, string siteId)
    {
      IManager manager = ManagerBase.GetManager(managerType);
      string str = string.Empty;
      switch (manager)
      {
        case null:
        case null:
          return str;
        case DynamicModuleManager _:
          str = DynamicModuleManager.GetDefaultProviderName(ModuleBuilderManager.GetModuleNameFromType(itemType.FullName, siteId));
          goto case null;
        default:
          str = manager.GetDefaultContextProvider().Name;
          goto case null;
      }
    }

    /// <summary>Returns a relating item's id</summary>
    /// <param name="item">The item.</param>
    /// <param name="status">The lifecycle status of the item.</param>
    /// <returns>The item's id.</returns>
    internal static Guid GetItemRelationId(IDataItem item, out ContentLifecycleStatus status)
    {
      if (item is ILifecycleDataItemGeneric lifecycleDataItemGeneric)
      {
        status = lifecycleDataItemGeneric.Status;
        return status != ContentLifecycleStatus.Master ? lifecycleDataItemGeneric.OriginalContentId : lifecycleDataItemGeneric.Id;
      }
      status = ContentLifecycleStatus.Live;
      return item.Id;
    }

    internal static IEnumerable<DataProviderBase> ResolveProviders(
      string relatedDataType)
    {
      return ManagerBase.GetManager(ManagerBase.GetMappedManagerType(relatedDataType)).GetSiteProviders(SystemManager.CurrentContext.CurrentSite.Id);
    }

    /// <summary>
    /// When item does not support Lifecycle, it's relations must be copied from temp to master and live state, when item is saved.
    /// </summary>
    /// <param name="contentLinksProvider">The provider for content links.</param>
    /// <param name="contentRelations">Collection of item's content links.</param>
    private static void CopyTempRelations(
      OpenAccessContentLinksProvider contentLinksProvider,
      IEnumerable<ContentLink> contentRelations)
    {
      foreach (ContentLink contentRelation in contentRelations)
      {
        bool flag = contentRelation[ContentLifecycleStatus.Temp];
        contentRelation[ContentLifecycleStatus.Master] = flag;
        contentRelation[ContentLifecycleStatus.Live] = flag;
        if (!contentRelation.AvailableForLive && !contentRelation.AvailableForMaster && !contentRelation.AvailableForTemp)
          contentLinksProvider.Delete(contentRelation);
      }
    }

    private static void SaveItemRelations(
      IDataItem item,
      ContentLinkChange[] changedRelations,
      ContentLifecycleStatus? parentItemStatus,
      IContentLinksManager contentLinksManager,
      string parentProviderName,
      string parentItemType,
      Guid parentItemId)
    {
      foreach (ContentLinkChange changedRelation in changedRelations)
      {
        ContentLinkChange linkContext = changedRelation;
        ContentLink contentLink = contentLinksManager.GetContentLinks().Where<ContentLink>((Expression<Func<ContentLink, bool>>) (c => c.ParentItemId == parentItemId && c.ParentItemType == parentItemType && c.ParentItemProviderName == parentProviderName && c.ComponentPropertyName == linkContext.ComponentPropertyName && c.ChildItemId == linkContext.ChildItemId && c.ChildItemProviderName == linkContext.ChildItemProviderName && c.ChildItemType == linkContext.ChildItemType)).FirstOrDefault<ContentLink>();
        if (contentLink != null)
        {
          if (linkContext.State == ContentLinkChangeState.Removed)
            RelatedDataHelper.SetContentLinkStatus(item, contentLink, parentItemStatus, false);
          else if (linkContext.State == ContentLinkChangeState.Added)
            RelatedDataHelper.SetContentLinkStatus(item, contentLink, parentItemStatus, true);
          else if (linkContext.State == ContentLinkChangeState.Updated)
            RelatedDataHelper.SetContentLinkOrdinal(contentLink, linkContext, contentLinksManager, parentProviderName, parentItemType, parentItemId);
        }
        else if (linkContext.State == ContentLinkChangeState.Added)
        {
          contentLink = contentLinksManager.CreateContentLink(linkContext.ComponentPropertyName, parentItemId, linkContext.ChildItemId, parentProviderName, linkContext.ChildItemProviderName, item.GetType().FullName, linkContext.ChildItemType);
          contentLink.ChildItemAdditionalInfo = linkContext.ChildItemAdditionalInfo;
          contentLink.IsChildDeleted = linkContext.IsChildDeleted;
          RelatedDataHelper.SetContentLinkOrdinal(contentLink, linkContext, contentLinksManager, parentProviderName, parentItemType, parentItemId);
          RelatedDataHelper.SetContentLinkStatus(item, contentLink, parentItemStatus, true);
        }
        if (contentLink != null && !contentLink.AvailableForLive && !contentLink.AvailableForMaster && !contentLink.AvailableForTemp)
          contentLinksManager.Delete(contentLink);
      }
    }

    private static void SetContentLinkOrdinal(
      ContentLink contentLink,
      ContentLinkChange contentLinkContext,
      IContentLinksManager contentLinksManager,
      string parentProviderName,
      string parentItemType,
      Guid parentItemId)
    {
      if (contentLinkContext.Ordinal.HasValue)
      {
        contentLink.Ordinal = contentLinkContext.Ordinal.Value;
      }
      else
      {
        float num = contentLinksManager.GetContentLinks().Where<ContentLink>((Expression<Func<ContentLink, bool>>) (c => c.ParentItemId == parentItemId && c.ParentItemType == parentItemType && c.ParentItemProviderName == parentProviderName && c.ComponentPropertyName == contentLinkContext.ComponentPropertyName)).Select<ContentLink, float>((Expression<Func<ContentLink, float>>) (c => c.Ordinal)).Max<float>();
        contentLink.Ordinal = num + 1f;
      }
    }

    private static void SetContentLinkStatus(
      IDataItem item,
      ContentLink contentLink,
      ContentLifecycleStatus? parentItemStatus,
      bool value)
    {
      if (parentItemStatus.HasValue)
      {
        contentLink[parentItemStatus.Value] = value;
      }
      else
      {
        if (item is ILifecycleDataItem)
          return;
        contentLink[ContentLifecycleStatus.Temp] = value;
      }
    }

    /// <summary>
    /// Applies the filter expression, required for retrieving related data items.
    /// </summary>
    /// <param name="items">Collection of items to filter.</param>
    /// <param name="itemIds">Collection of item ids filter.</param>
    /// <param name="status">The lifecycle status filter.</param>
    /// <returns>Collection of filtered related items.</returns>
    private static IQueryable<ILifecycleDataItemGeneric> ApplyRelatedItemsContainsFilter(
      IQueryable<ILifecycleDataItemGeneric> items,
      List<Guid> itemIds,
      ContentLifecycleStatus? status)
    {
      if (status.HasValue && status.Value == ContentLifecycleStatus.Live)
        return items.Where<ILifecycleDataItemGeneric>((Expression<Func<ILifecycleDataItemGeneric, bool>>) (i => itemIds.Contains(i.OriginalContentId)));
      return items.Where<ILifecycleDataItemGeneric>((Expression<Func<ILifecycleDataItemGeneric, bool>>) (i => itemIds.Contains(i.Id)));
    }

    /// <summary>Gets the related items via contains query.</summary>
    /// <param name="parentItemType">Type of the parent item.</param>
    /// <param name="parentItemProviderName">Name of the parent item provider.</param>
    /// <param name="parentItemId">The parent item identifier.</param>
    /// <param name="fieldName">Name of the related field.</param>
    /// <param name="childItemType">Type of the child item.</param>
    /// <param name="status">The status of the item.</param>
    /// <param name="childTypeProvider">Provider for the child items.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="orderExpression">The order expression.</param>
    /// <param name="skip">The number of items to skip.</param>
    /// <param name="take">The number of items to take.</param>
    /// <param name="totalCount">The total count.</param>
    /// <returns>Collection of related items.</returns>
    private static IQueryable GetRelatedItemsViaContains(
      string parentItemType,
      string parentItemProviderName,
      Guid parentItemId,
      string fieldName,
      Type childItemType,
      ContentLifecycleStatus? status,
      DataProviderBase childTypeProvider,
      string filterExpression,
      string orderExpression,
      int? skip,
      int? take,
      ref int? totalCount)
    {
      IQueryable<ContentLink> queryable1 = ContentLinksManager.GetManager().GetContentLinks().Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => cl.ParentItemType == parentItemType && cl.ParentItemProviderName == parentItemProviderName && cl.ChildItemType == childItemType.FullName && cl.ChildItemProviderName == childTypeProvider.Name));
      if (parentItemId != Guid.Empty)
        queryable1 = queryable1.Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => cl.ParentItemId == parentItemId));
      List<Guid> itemIds = RelatedDataHelper.ApplyLinksFilters(RelatedDataHelper.ApplyDeletedLinksFilters(queryable1, RelationDirection.Child), fieldName, status).OrderBy<ContentLink, float>((Expression<Func<ContentLink, float>>) (cl => cl.Ordinal)).Select<ContentLink, Guid>((Expression<Func<ContentLink, Guid>>) (cl => cl.ChildItemId)).ToList<Guid>();
      IQueryable queryable2;
      if (childItemType.ImplementsInterface(typeof (ILifecycleDataItemGeneric)))
        queryable2 = (IQueryable) RelatedDataHelper.ApplyRelatedItemsContainsFilter(SitefinityQuery.Get<ILifecycleDataItemGeneric>(childItemType, childTypeProvider), itemIds, status);
      else
        queryable2 = (IQueryable) SitefinityQuery.Get<IDataItem>(childItemType, childTypeProvider).Where<IDataItem>((Expression<Func<IDataItem, bool>>) (i => itemIds.Contains(i.Id)));
      MethodCallExpression methodCallExpression = Expression.Call(typeof (Queryable), "Cast", new Type[1]
      {
        queryable2.ElementType
      }, queryable2.Expression);
      IQueryable source = queryable2.Provider.CreateQuery((Expression) methodCallExpression);
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

    /// <summary>Gets the items relating the specified item.</summary>
    /// <param name="childItemType">Type of the child item.</param>
    /// <param name="childItemProviderName">Name of the child item provider.</param>
    /// <param name="childItemId">The child item identifier.</param>
    /// <param name="fieldName">Name of the field.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="status">The status of the item.</param>
    /// <param name="parentTypeProvider">The parent type provider.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="orderExpression">The order expression.</param>
    /// <param name="skip">Items to skip.</param>
    /// <param name="take">Items to take.</param>
    /// <param name="totalCount">Total count of items.</param>
    /// <returns>Collection of related parent items.</returns>
    private static IQueryable GetRelatedParentItemsViaContains(
      string childItemType,
      string childItemProviderName,
      Guid childItemId,
      string fieldName,
      Type itemType,
      ContentLifecycleStatus? status,
      DataProviderBase parentTypeProvider,
      string filterExpression,
      string orderExpression,
      int? skip,
      int? take,
      ref int? totalCount)
    {
      IQueryable<ContentLink> queryable1 = ContentLinksManager.GetManager().GetContentLinks().Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => cl.ChildItemType == childItemType && cl.ChildItemProviderName == childItemProviderName && cl.ParentItemType == itemType.FullName && cl.ParentItemProviderName == parentTypeProvider.Name));
      if (childItemId != Guid.Empty)
        queryable1 = queryable1.Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => cl.ChildItemId == childItemId));
      List<Guid> itemIds = RelatedDataHelper.ApplyLinksFilters(RelatedDataHelper.ApplyDeletedLinksFilters(queryable1, RelationDirection.Parent), fieldName, status).Select<ContentLink, Guid>((Expression<Func<ContentLink, Guid>>) (cl => cl.ParentItemId)).ToList<Guid>();
      IQueryable queryable2;
      if (itemType.ImplementsInterface(typeof (ILifecycleDataItemGeneric)))
        queryable2 = (IQueryable) RelatedDataHelper.ApplyRelatedItemsContainsFilter(SitefinityQuery.Get<ILifecycleDataItemGeneric>(itemType, parentTypeProvider), itemIds, status);
      else
        queryable2 = (IQueryable) SitefinityQuery.Get<IDataItem>(itemType, parentTypeProvider).Where<IDataItem>((Expression<Func<IDataItem, bool>>) (i => itemIds.Contains(i.Id)));
      MethodCallExpression methodCallExpression = Expression.Call(typeof (Queryable), "Cast", new Type[1]
      {
        queryable2.ElementType
      }, queryable2.Expression);
      IQueryable source = queryable2.Provider.CreateQuery((Expression) methodCallExpression);
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

    private static bool HasRelatingDataItems(Guid childItemId, string childItemType) => ContentLinksManager.GetManager().GetContentLinks().Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => cl.ChildItemType == childItemType && cl.ChildItemId == childItemId)).Any<ContentLink>();

    private static void CopyFieldRelations(
      IContentLinksManager contentLinksManager,
      Guid parentItemId,
      Type parentItemType,
      string propertyName,
      Func<ContentLink, bool> filter,
      ContentLifecycleStatus sourceStatus,
      ContentLifecycleStatus destinationStatus)
    {
      List<ContentLink> source1 = new List<ContentLink>();
      if (contentLinksManager is IManager)
      {
        OpenAccessContentLinksProvider contentLinksProvider = ((IManager) contentLinksManager).Provider as OpenAccessContentLinksProvider;
        if (contentLinksProvider != null)
        {
          string appName = contentLinksProvider.ApplicationName;
          source1.AddRange(contentLinksProvider.GetDirtyItems().OfType<ContentLink>().Where<ContentLink>((Func<ContentLink, bool>) (c => c.ApplicationName == appName && contentLinksProvider.GetDirtyItemStatus((object) c) != SecurityConstants.TransactionActionType.Deleted && c.ParentItemId == parentItemId && c.ParentItemType == parentItemType.FullName && c.ComponentPropertyName == propertyName)).Where<ContentLink>(filter));
        }
      }
      IQueryable<ContentLink> source2 = contentLinksManager.GetContentLinks(parentItemId, parentItemType, propertyName).Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => !cl.IsChildDeleted && !cl.IsParentDeleted));
      if (filter != null)
        source2.Where<ContentLink>(filter);
      IList<ContentLink> contentLinkList = (IList<ContentLink>) null;
      using (new ReadUncommitedRegion((IManager) contentLinksManager))
        contentLinkList = (IList<ContentLink>) source2.ToList<ContentLink>();
      foreach (ContentLink contentLink1 in (IEnumerable<ContentLink>) contentLinkList)
      {
        ContentLink contentLink = contentLink1;
        if (!source1.Any<ContentLink>((Func<ContentLink, bool>) (c => c.Id == contentLink.Id)))
          source1.Add(contentLink);
      }
      foreach (ContentLink contentLink in source1)
      {
        contentLink[destinationStatus] = contentLink[sourceStatus];
        if (!contentLink.AvailableForLive && !contentLink.AvailableForMaster)
          contentLinksManager.Delete(contentLink);
      }
    }

    /// <summary>Gets the related data properties.</summary>
    /// <param name="item">The item.</param>
    /// <returns>Collection of strings containing the names of the related data properties.</returns>
    private static string[] GetRelatedDataProperties(object item) => TypeDescriptor.GetProperties(item).OfType<RelatedDataPropertyDescriptor>().Select<RelatedDataPropertyDescriptor, string>((Func<RelatedDataPropertyDescriptor, string>) (p => p.Name)).ToArray<string>();

    /// <summary>
    /// Returns only temp and live records for dynamic module type
    /// </summary>
    /// <param name="ids">Collection of dynamic module type records ids.</param>
    /// <param name="providerName">The provider name.</param>
    /// <param name="itemTypes">The types of the items.</param>
    /// <returns>Collection of temp and live records.</returns>
    private static List<DynamicContent> GetTempAndLiveRecords(
      Guid[] ids,
      string providerName,
      Type[] itemTypes)
    {
      DynamicModuleManager manager = DynamicModuleManager.GetManager(providerName);
      List<DynamicContent> tempAndLiveRecords = new List<DynamicContent>();
      foreach (Type itemType in itemTypes)
      {
        List<DynamicContent> list = manager.GetDataItems(itemType).Where<DynamicContent>((Expression<Func<DynamicContent, bool>>) (d => ids.Contains<Guid>(d.OriginalContentId) && (int) d.Status != 0)).ToList<DynamicContent>();
        tempAndLiveRecords.AddRange((IEnumerable<DynamicContent>) list);
      }
      return tempAndLiveRecords;
    }

    /// <summary>
    /// Returns an array with the ids of dynamic content collection
    /// </summary>
    /// <param name="items">Collection of dynamic module type items.</param>
    /// <returns>Array with the ids of the passed dynamic content collection</returns>
    private static Guid[] GetItemIDs(IList<DynamicContent> items)
    {
      Guid[] itemIds = new Guid[items.Count];
      for (int index = 0; index < items.Count; ++index)
        itemIds[index] = items[index].Id;
      return itemIds;
    }
  }
}
