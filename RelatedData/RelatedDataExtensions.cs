// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.RelatedData.RelatedDataExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.ContentLinks;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI.NavigationControls;

namespace Telerik.Sitefinity.RelatedData
{
  /// <summary>Provides public API for managing related items</summary>
  public static class RelatedDataExtensions
  {
    /// <summary>Sets items collection to the current context.</summary>
    /// <param name="items">Collection of items to set in the context.</param>
    public static void SetRelatedDataSourceContext(this IEnumerable<IDataItem> items)
    {
      if (!(SystemManager.CurrentHttpContext.Items[(object) RelatedItemsApplicationState.ContextItemName] is RelatedItemsApplicationState applicationState))
        applicationState = new RelatedItemsApplicationState();
      applicationState.SetRelatedItemsDataSource(items);
      SystemManager.CurrentHttpContext.Items[(object) RelatedItemsApplicationState.ContextItemName] = (object) applicationState;
    }

    /// <summary>Sets items collection to the current context.</summary>
    /// <param name="items">Collection of items to set in the context.</param>
    /// <param name="itemType">Items type.</param>
    /// <param name="itemProviderName">Items provider name.</param>
    public static void SetRelatedDataSourceContext(
      this IEnumerable<IDataItem> items,
      string itemType,
      string itemProviderName)
    {
      if (!(SystemManager.CurrentHttpContext.Items[(object) RelatedItemsApplicationState.ContextItemName] is RelatedItemsApplicationState applicationState))
        applicationState = new RelatedItemsApplicationState();
      applicationState.SetRelatedItemsDataSource(items, itemType, itemProviderName);
      SystemManager.CurrentHttpContext.Items[(object) RelatedItemsApplicationState.ContextItemName] = (object) applicationState;
    }

    /// <summary>Gets all related items for a specified item</summary>
    /// <param name="item">The parent item</param>
    /// <param name="fieldName">The name of the related field.</param>
    public static IQueryable<IDataItem> GetRelatedItems(
      this object item,
      string fieldName)
    {
      return item.GetRelatedItems<IDataItem>(fieldName);
    }

    /// <summary>Gets all related items for a specified item</summary>
    /// <typeparam name="T">Type of the child items</typeparam>
    /// <param name="item">The parent item</param>
    /// <param name="fieldName">The name of the related field</param>
    public static IQueryable<T> GetRelatedItems<T>(this object item, string fieldName) where T : IDataItem
    {
      RelatedItemsApplicationState applicationState = RelatedItemsApplicationState.GetCurrentContextApplicationState();
      IQueryable relatedItems;
      if (applicationState != null)
      {
        relatedItems = (IQueryable) applicationState.GetRelatedItems(item as IDataItem, fieldName, typeof (T));
      }
      else
      {
        Guid id = RelatedDataExtensions.GetId(item);
        string provider = RelatedDataExtensions.GetProvider(item);
        ContentLifecycleStatus status = RelatedDataExtensions.GetStatus(item);
        relatedItems = RelatedDataHelper.GetRelatedItems(RelatedDataExtensions.GetTypeName(item), id, provider, fieldName, new ContentLifecycleStatus?(status));
      }
      return relatedItems != null ? Queryable.OfType<T>(relatedItems) : Enumerable.Empty<T>().AsQueryable<T>();
    }

    /// <summary>
    /// Gets a list of items, from specified type and provider, relating to the specified item
    /// </summary>
    /// <param name="item">The parent item</param>
    /// <param name="parentItemsTypeName">Name of the parent items type.</param>
    /// <param name="parentItemProviderName">Name of the parent item provider.</param>
    /// <param name="fieldName">The name of the related field</param>
    public static IList GetRelatedParentItemsList(
      this object item,
      string parentItemsTypeName,
      string parentItemProviderName = null,
      string fieldName = null)
    {
      IQueryable<IDataItem> source = item.GetRelatedParentItems(parentItemsTypeName, parentItemProviderName, fieldName);
      Type type = TypeResolutionService.ResolveType(parentItemsTypeName);
      if (type != (Type) null && type.ImplementsInterface(typeof (ILifecycleDataItemGeneric)))
      {
        ContentLifecycleStatus status = RelatedDataExtensions.GetStatus(item);
        source = (IQueryable<IDataItem>) Queryable.OfType<ILifecycleDataItemGeneric>(source).Where<ILifecycleDataItemGeneric>((Expression<Func<ILifecycleDataItemGeneric, bool>>) (i => (int) i.Status == (int) status));
        CultureInfo culture = (CultureInfo) null;
        if (!SystemManager.IsBackendRequest(out culture))
          source = (IQueryable<IDataItem>) Queryable.OfType<ILifecycleDataItemGeneric>(source).Where<ILifecycleDataItemGeneric>((Expression<Func<ILifecycleDataItemGeneric, bool>>) (i => i.Visible == true));
        if (SystemManager.CurrentContext.AppSettings.Multilingual)
        {
          CultureInfo uiCulture = SystemManager.CurrentContext.Culture;
          CultureInfo defaultCulture = SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage;
          source = (IQueryable<IDataItem>) Queryable.OfType<ILifecycleDataItemGeneric>(source).Where<ILifecycleDataItemGeneric>((Expression<Func<ILifecycleDataItemGeneric, bool>>) (i => !i.PublishedTranslations.Any<string>() && uiCulture.Equals(defaultCulture) || i.PublishedTranslations.Contains(uiCulture.Name)));
        }
      }
      return (IList) source.ToList<IDataItem>().Distinct<IDataItem>().ToList<IDataItem>();
    }

    /// <summary>
    /// Gets items, from specified type and provider, relating to a specified item
    /// </summary>
    /// <param name="item">The parent item</param>
    /// <param name="parentItemsTypeName">Name of the parent items type.</param>
    /// <param name="parentItemProviderName">Name of the parent item provider.</param>
    /// <param name="fieldName">The name of the related field linking to this item, in the parent item.</param>
    public static IQueryable<IDataItem> GetRelatedParentItems(
      this object item,
      string parentItemsTypeName,
      string parentItemProviderName = null,
      string fieldName = null)
    {
      IQueryable<IDataItem> relatedParentItems1 = Enumerable.Empty<IDataItem>().AsQueryable<IDataItem>();
      Guid id = RelatedDataExtensions.GetId(item);
      string provider = RelatedDataExtensions.GetProvider(item);
      ContentLifecycleStatus status = RelatedDataExtensions.GetStatus(item);
      IQueryable relatedParentItems2 = RelatedDataHelper.GetRelatedParentItems(RelatedDataExtensions.GetTypeName(item), provider, id, parentItemsTypeName, parentItemProviderName, fieldName, status);
      if (relatedParentItems2 != null)
        relatedParentItems1 = Queryable.OfType<IDataItem>(relatedParentItems2);
      return relatedParentItems1;
    }

    /// <summary>
    /// Gets items from specified type and provider, relating to a specified item
    /// </summary>
    /// <typeparam name="T">Type of the parent items</typeparam>
    /// <param name="item">The parent item</param>
    /// <param name="parentItemProviderName">Name of the parent item provider.</param>
    /// <param name="fieldName">The name of the related field</param>
    public static IQueryable<T> GetRelatedParentItems<T>(
      this object item,
      string parentItemProviderName = null,
      string fieldName = null)
      where T : IDataItem
    {
      IQueryable<T> relatedParentItems1 = Enumerable.Empty<T>().AsQueryable<T>();
      Guid id = RelatedDataExtensions.GetId(item);
      string fullName = typeof (T).FullName;
      string provider = RelatedDataExtensions.GetProvider(item);
      ContentLifecycleStatus status = RelatedDataExtensions.GetStatus(item);
      IQueryable relatedParentItems2 = RelatedDataHelper.GetRelatedParentItems(RelatedDataExtensions.GetTypeName(item), provider, id, fullName, parentItemProviderName, fieldName, status);
      if (relatedParentItems2 != null)
        relatedParentItems1 = Queryable.OfType<T>(relatedParentItems2);
      return relatedParentItems1;
    }

    /// <summary>Creates a relation between two items</summary>
    /// <param name="item">Parent item</param>
    /// <param name="relatedItem">Related Item</param>
    /// <param name="fieldName">Related data field name, which is property of the parent item</param>
    public static ContentLink CreateRelation(
      this IDataItem item,
      IDataItem relatedItem,
      string fieldName)
    {
      string name = ((DataProviderBase) relatedItem.Provider).Name;
      string fullName = relatedItem.GetType().FullName;
      Guid relatedItemId = relatedItem.Id;
      if (relatedItem is ILifecycleDataItemGeneric lifecycleDataItemGeneric && lifecycleDataItemGeneric.OriginalContentId != Guid.Empty)
        relatedItemId = lifecycleDataItemGeneric.OriginalContentId;
      return item.CreateRelation(relatedItemId, name, fullName, fieldName);
    }

    /// <summary>Creates a relation between two items</summary>
    /// <param name="item">Parent item</param>
    /// <param name="relatedItemId">Related item id</param>
    /// <param name="relatedItemProviderName">Related item provider name</param>
    /// <param name="relatedItemType">Related item type</param>
    /// <param name="fieldName">Related data field name, which is property of the parent item</param>
    public static ContentLink CreateRelation(
      this IDataItem item,
      Guid relatedItemId,
      string relatedItemProviderName,
      string relatedItemType,
      string fieldName)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      RelatedDataExtensions.\u003C\u003Ec__DisplayClass8_0 cDisplayClass80 = new RelatedDataExtensions.\u003C\u003Ec__DisplayClass8_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass80.item = item;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass80.fieldName = fieldName;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass80.relatedItemId = relatedItemId;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass80.relatedItemType = relatedItemType;
      // ISSUE: reference to a compiler-generated field
      DataProviderBase provider = (DataProviderBase) cDisplayClass80.item.Provider;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass80.parentProviderName = provider.Name;
      ContentLifecycleStatus status = ContentLifecycleStatus.Live;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      cDisplayClass80.parentItemId = cDisplayClass80.item.Id;
      // ISSUE: reference to a compiler-generated field
      if (cDisplayClass80.item is ILifecycleDataItemGeneric lifecycleDataItemGeneric)
      {
        status = lifecycleDataItemGeneric.Status;
        // ISSUE: reference to a compiler-generated field
        cDisplayClass80.parentItemId = status == ContentLifecycleStatus.Master ? lifecycleDataItemGeneric.Id : lifecycleDataItemGeneric.OriginalContentId;
      }
      ContentLinksManager mappedRelatedManager = provider.GetMappedRelatedManager<ContentLink>(string.Empty) as ContentLinksManager;
      ParameterExpression parameterExpression1;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: field reference
      // ISSUE: method reference
      // ISSUE: field reference
      // ISSUE: method reference
      // ISSUE: field reference
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: field reference
      Expression<Func<ContentLink, bool>> predicate1 = Expression.Lambda<Func<ContentLink, bool>>((Expression) Expression.AndAlso((Expression) Expression.AndAlso((Expression) Expression.AndAlso((Expression) Expression.AndAlso((Expression) Expression.AndAlso(c.ParentItemId == cDisplayClass80.parentItemId, (Expression) Expression.Equal(c.ParentItemType, (Expression) Expression.Property((Expression) Expression.Call(cDisplayClass80.item, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.GetType)), Array.Empty<Expression>()), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Type.get_FullName))))), (Expression) Expression.Equal((Expression) Expression.Property((Expression) parameterExpression1, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ContentLink.get_ParentItemProviderName))), (Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass80, typeof (RelatedDataExtensions.\u003C\u003Ec__DisplayClass8_0)), FieldInfo.GetFieldFromHandle(__fieldref (RelatedDataExtensions.\u003C\u003Ec__DisplayClass8_0.parentProviderName))))), (Expression) Expression.Equal((Expression) Expression.Property((Expression) parameterExpression1, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ContentLink.get_ComponentPropertyName))), (Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass80, typeof (RelatedDataExtensions.\u003C\u003Ec__DisplayClass8_0)), FieldInfo.GetFieldFromHandle(__fieldref (RelatedDataExtensions.\u003C\u003Ec__DisplayClass8_0.fieldName))))), (Expression) Expression.Equal((Expression) Expression.Property((Expression) parameterExpression1, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ContentLink.get_ChildItemId))), (Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass80, typeof (RelatedDataExtensions.\u003C\u003Ec__DisplayClass8_0)), FieldInfo.GetFieldFromHandle(__fieldref (RelatedDataExtensions.\u003C\u003Ec__DisplayClass8_0.relatedItemId))), false, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Guid.op_Equality)))), (Expression) Expression.Equal((Expression) Expression.Property((Expression) parameterExpression1, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ContentLink.get_ChildItemType))), (Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass80, typeof (RelatedDataExtensions.\u003C\u003Ec__DisplayClass8_0)), FieldInfo.GetFieldFromHandle(__fieldref (RelatedDataExtensions.\u003C\u003Ec__DisplayClass8_0.relatedItemType))))), parameterExpression1);
      ParameterExpression parameterExpression2;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: field reference
      // ISSUE: method reference
      // ISSUE: field reference
      // ISSUE: method reference
      // ISSUE: field reference
      Expression<Func<ContentLink, bool>> predicate2 = Expression.Lambda<Func<ContentLink, bool>>((Expression) Expression.AndAlso((Expression) Expression.AndAlso((Expression) Expression.AndAlso((Expression) Expression.AndAlso(c.ParentItemId == cDisplayClass80.parentItemId, (Expression) Expression.Equal(c.ParentItemType, (Expression) Expression.Property((Expression) Expression.Call(cDisplayClass80.item, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.GetType)), Array.Empty<Expression>()), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Type.get_FullName))))), (Expression) Expression.Equal((Expression) Expression.Property((Expression) parameterExpression2, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ContentLink.get_ParentItemProviderName))), (Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass80, typeof (RelatedDataExtensions.\u003C\u003Ec__DisplayClass8_0)), FieldInfo.GetFieldFromHandle(__fieldref (RelatedDataExtensions.\u003C\u003Ec__DisplayClass8_0.parentProviderName))))), (Expression) Expression.Equal((Expression) Expression.Property((Expression) parameterExpression2, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ContentLink.get_ComponentPropertyName))), (Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass80, typeof (RelatedDataExtensions.\u003C\u003Ec__DisplayClass8_0)), FieldInfo.GetFieldFromHandle(__fieldref (RelatedDataExtensions.\u003C\u003Ec__DisplayClass8_0.fieldName))))), (Expression) Expression.Equal((Expression) Expression.Property((Expression) parameterExpression2, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ContentLink.get_ChildItemType))), (Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass80, typeof (RelatedDataExtensions.\u003C\u003Ec__DisplayClass8_0)), FieldInfo.GetFieldFromHandle(__fieldref (RelatedDataExtensions.\u003C\u003Ec__DisplayClass8_0.relatedItemType))))), parameterExpression2);
      ContentLink relation = mappedRelatedManager.GetContentLinks().Where<ContentLink>(predicate1).FirstOrDefault<ContentLink>();
      IQueryable<float> source = mappedRelatedManager.GetContentLinks().Where<ContentLink>(predicate2).Select<ContentLink, float>((Expression<Func<ContentLink, float>>) (c => c.Ordinal));
      float num = source.Count<float>() == 0 ? 0.0f : source.Max<float>() + 1f;
      if (relation == null)
      {
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        relation = mappedRelatedManager.Provider.GetDirtyItems().OfType<ContentLink>().AsQueryable<ContentLink>().Where<ContentLink>(predicate1).FirstOrDefault<ContentLink>() ?? mappedRelatedManager.CreateContentLink(cDisplayClass80.fieldName, cDisplayClass80.parentItemId, cDisplayClass80.relatedItemId, cDisplayClass80.parentProviderName, relatedItemProviderName, cDisplayClass80.item.GetType().FullName, cDisplayClass80.relatedItemType);
      }
      relation.Ordinal = num;
      if (lifecycleDataItemGeneric == null)
      {
        relation[ContentLifecycleStatus.Temp] = true;
        relation[ContentLifecycleStatus.Master] = true;
        relation[ContentLifecycleStatus.Live] = true;
      }
      else
      {
        if (status == ContentLifecycleStatus.PartialTemp)
          status = ContentLifecycleStatus.Temp;
        relation[status] = true;
      }
      mappedRelatedManager.SaveChanges();
      return relation;
    }

    /// <summary>Deletes the relation between two items</summary>
    /// <param name="item">Parent item</param>
    /// <param name="relatedItem">Related Item</param>
    /// <param name="fieldName">Related data field name, which is property of the parent item</param>
    public static void DeleteRelation(this IDataItem item, IDataItem relatedItem, string fieldName)
    {
      string name = ((DataProviderBase) relatedItem.Provider).Name;
      string fullName = relatedItem.GetType().FullName;
      item.DeleteRelation(relatedItem.Id, name, fullName, fieldName);
    }

    /// <summary>Deletes the relation between two items</summary>
    /// <param name="item">Parent item</param>
    /// <param name="relatedItemId">Related item id</param>
    /// <param name="relatedItemProviderName">Related item provider name</param>
    /// <param name="relatedItemType">Related item type</param>
    /// <param name="fieldName">Related data field name, which is property of the parent item</param>
    public static void DeleteRelation(
      this IDataItem item,
      Guid relatedItemId,
      string relatedItemProviderName,
      string relatedItemType,
      string fieldName)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      RelatedDataExtensions.\u003C\u003Ec__DisplayClass10_0 cDisplayClass100 = new RelatedDataExtensions.\u003C\u003Ec__DisplayClass10_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass100.item = item;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass100.fieldName = fieldName;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass100.relatedItemId = relatedItemId;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass100.relatedItemProviderName = relatedItemProviderName;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass100.relatedItemType = relatedItemType;
      // ISSUE: reference to a compiler-generated field
      DataProviderBase provider = (DataProviderBase) cDisplayClass100.item.Provider;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass100.parentProviderName = provider.Name;
      ContentLifecycleStatus status;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      cDisplayClass100.parentItemId = RelatedDataHelper.GetItemRelationId(cDisplayClass100.item, out status);
      IContentLinksManager mappedRelatedManager = provider.GetMappedRelatedManager<ContentLink>(string.Empty) as IContentLinksManager;
      ParameterExpression parameterExpression;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: field reference
      // ISSUE: method reference
      // ISSUE: field reference
      // ISSUE: method reference
      // ISSUE: field reference
      // ISSUE: method reference
      // ISSUE: method reference
      // ISSUE: field reference
      // ISSUE: method reference
      // ISSUE: field reference
      ContentLink contentLink = mappedRelatedManager.GetContentLinks().Where<ContentLink>(Expression.Lambda<Func<ContentLink, bool>>((Expression) Expression.AndAlso((Expression) Expression.AndAlso((Expression) Expression.AndAlso((Expression) Expression.AndAlso((Expression) Expression.AndAlso((Expression) Expression.AndAlso(c.ParentItemId == cDisplayClass100.parentItemId, (Expression) Expression.Equal(c.ParentItemType, (Expression) Expression.Property((Expression) Expression.Call(cDisplayClass100.item, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.GetType)), Array.Empty<Expression>()), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Type.get_FullName))))), (Expression) Expression.Equal((Expression) Expression.Property((Expression) parameterExpression, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ContentLink.get_ParentItemProviderName))), (Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass100, typeof (RelatedDataExtensions.\u003C\u003Ec__DisplayClass10_0)), FieldInfo.GetFieldFromHandle(__fieldref (RelatedDataExtensions.\u003C\u003Ec__DisplayClass10_0.parentProviderName))))), (Expression) Expression.Equal((Expression) Expression.Property((Expression) parameterExpression, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ContentLink.get_ComponentPropertyName))), (Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass100, typeof (RelatedDataExtensions.\u003C\u003Ec__DisplayClass10_0)), FieldInfo.GetFieldFromHandle(__fieldref (RelatedDataExtensions.\u003C\u003Ec__DisplayClass10_0.fieldName))))), (Expression) Expression.Equal((Expression) Expression.Property((Expression) parameterExpression, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ContentLink.get_ChildItemId))), (Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass100, typeof (RelatedDataExtensions.\u003C\u003Ec__DisplayClass10_0)), FieldInfo.GetFieldFromHandle(__fieldref (RelatedDataExtensions.\u003C\u003Ec__DisplayClass10_0.relatedItemId))), false, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Guid.op_Equality)))), (Expression) Expression.Equal((Expression) Expression.Property((Expression) parameterExpression, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ContentLink.get_ChildItemProviderName))), (Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass100, typeof (RelatedDataExtensions.\u003C\u003Ec__DisplayClass10_0)), FieldInfo.GetFieldFromHandle(__fieldref (RelatedDataExtensions.\u003C\u003Ec__DisplayClass10_0.relatedItemProviderName))))), (Expression) Expression.Equal((Expression) Expression.Property((Expression) parameterExpression, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ContentLink.get_ChildItemType))), (Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass100, typeof (RelatedDataExtensions.\u003C\u003Ec__DisplayClass10_0)), FieldInfo.GetFieldFromHandle(__fieldref (RelatedDataExtensions.\u003C\u003Ec__DisplayClass10_0.relatedItemType))))), parameterExpression)).FirstOrDefault<ContentLink>();
      if (contentLink == null)
        return;
      contentLink[status] = false;
      if (contentLink.AvailableForLive || contentLink.AvailableForMaster || contentLink.AvailableForTemp)
        return;
      mappedRelatedManager.Delete(contentLink);
      mappedRelatedManager.SaveChanges();
    }

    /// <summary>Deletes all item relations</summary>
    /// <param name="item">Parent item</param>
    public static void DeleteRelations(this IDataItem item)
    {
      IContentLinksManager mappedRelatedManager = ((DataProviderBase) item.Provider).GetMappedRelatedManager<ContentLink>(string.Empty) as IContentLinksManager;
      bool handleLifecycle = item is ILifecycleDataItem;
      RelatedDataHelper.DeleteItemRelations(item, mappedRelatedManager, handleLifecycle);
      mappedRelatedManager.SaveChanges();
    }

    /// <summary>Deletes all item relations for the specified field</summary>
    /// <param name="item">Parent item</param>
    /// <param name="fieldName">Related data field name, that contains the relations to be deleted</param>
    public static void DeleteRelations(this IDataItem item, string fieldName)
    {
      IContentLinksManager mappedRelatedManager = ((DataProviderBase) item.Provider).GetMappedRelatedManager<ContentLink>(string.Empty) as IContentLinksManager;
      bool handleLifecycle = item is ILifecycleDataItem;
      RelatedDataHelper.DeleteItemRelations(item, mappedRelatedManager, fieldName, handleLifecycle);
      mappedRelatedManager.SaveChanges();
    }

    /// <summary>
    /// Gets the count of related items for specified field name.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="fieldName">Name of the field.</param>
    /// <exception cref="T:System.ArgumentNullException">item</exception>
    public static int GetRelatedItemsCountByField(this object item, string fieldName = null)
    {
      Guid id = item != null ? RelatedDataExtensions.GetId(item) : throw new ArgumentNullException(nameof (item));
      ContentLifecycleStatus status = RelatedDataExtensions.GetStatus(item);
      string provider = RelatedDataExtensions.GetProvider(item);
      string type = RelatedDataExtensions.GetTypeName(item);
      IQueryable<ContentLink> queryable = ContentLinksManager.GetManager().GetContentLinks().Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => cl.ParentItemType == type && cl.ParentItemId == id));
      if (!string.IsNullOrEmpty(provider))
        queryable = queryable.Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => provider.Equals(cl.ParentItemProviderName)));
      return RelatedDataHelper.ApplyLinksFilters(RelatedDataHelper.ApplyDeletedLinksFilters(queryable, RelationDirection.Child), fieldName, new ContentLifecycleStatus?(status)).Count<ContentLink>();
    }

    /// <summary>Gets the count of related items from specified type.</summary>
    /// <param name="item">The item.</param>
    /// <param name="typeName">Name of the type.</param>
    /// <param name="getDeletedItems">Indicates whether to get deleted items.</param>
    /// <exception cref="T:System.ArgumentNullException">item</exception>
    public static int GetRelatedItemsCountByType(this object item, string typeName = null)
    {
      Guid id = item != null ? RelatedDataExtensions.GetId(item) : throw new ArgumentNullException(nameof (item));
      ContentLifecycleStatus status = RelatedDataExtensions.GetStatus(item);
      string provider = RelatedDataExtensions.GetProvider(item);
      string type = RelatedDataExtensions.GetTypeName(item);
      IQueryable<ContentLink> queryable = ContentLinksManager.GetManager().GetContentLinks().Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => cl.ParentItemType == type && cl.ParentItemId == id));
      if (!string.IsNullOrEmpty(provider))
        queryable = queryable.Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => provider.Equals(cl.ParentItemProviderName)));
      IQueryable<ContentLink> source = RelatedDataHelper.ApplyLinksFilters(RelatedDataHelper.ApplyDeletedLinksFilters(queryable, RelationDirection.Child), (string) null, new ContentLifecycleStatus?(status));
      if (!string.IsNullOrEmpty(typeName))
        source = source.Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => typeName.Equals(cl.ChildItemType)));
      return source.Count<ContentLink>();
    }

    /// <summary>Gets the default URL for specified item.</summary>
    /// <param name="item">The item.</param>
    public static string GetDefaultUrl(this object item)
    {
      string defaultUrl = string.Empty;
      switch (item)
      {
        case PageNode _:
          PageNode pageNode = item as PageNode;
          if (pageNode.RootNodeId != SiteInitializer.CurrentFrontendRootNodeId)
          {
            using (SiteRegion.FromSiteMapRoot(pageNode.RootNodeId, SiteContextResolutionTypes.ByParam))
            {
              SiteMapNode siteMapNodeFromKey = SitefinitySiteMap.GetCurrentProvider().FindSiteMapNodeFromKey(pageNode.Id.ToString());
              if (siteMapNodeFromKey != null)
              {
                defaultUrl = NavigationUtilities.ResolveUrl(siteMapNodeFromKey, new bool?(), (string[]) null);
                break;
              }
              break;
            }
          }
          else
          {
            SiteMapNode siteMapNodeFromKey = SitefinitySiteMap.GetCurrentProvider().FindSiteMapNodeFromKey(pageNode.Id.ToString());
            if (siteMapNodeFromKey != null)
            {
              defaultUrl = NavigationUtilities.ResolveUrl(siteMapNodeFromKey, new bool?(), (string[]) null);
              break;
            }
            break;
          }
        case IDataItem _:
          IDataItem dataItem = item as IDataItem;
          ContentLocationService locationServiceInternal = SystemManager.GetContentLocationServiceInternal();
          IContentItemLocation contentItemLocation = locationServiceInternal.GetItemDefaultLocation(dataItem, (CultureInfo) null) ?? locationServiceInternal.GetItemLocations(dataItem, (CultureInfo) null).FirstOrDefault<IContentItemLocation>((Func<IContentItemLocation, bool>) (l => l.IsAccessibleForCurrentUser()));
          if (contentItemLocation != null)
          {
            defaultUrl = contentItemLocation.ItemAbsoluteUrl;
            break;
          }
          break;
      }
      return defaultUrl;
    }

    /// <summary>
    /// Returns other items from a specified type with at least one matching value for the specified taxonomy field.
    /// </summary>
    /// <param name="item"> the current item </param>
    /// <param name="itemTaxonomyFieldName">the custom field holding taxons </param>
    /// <param name="relatedItemsTypeFullName">the full type name of items we want to load</param>
    /// <param name="skip">how many to skip</param>
    /// <param name="take">how many to take (default 10)</param>
    /// <returns></returns>
    public static IEnumerable GetItemsWithSameTaxons(
      this object item,
      string itemTaxonomyFieldName,
      string relatedItemsTypeFullName,
      int skip = 0,
      int take = 10)
    {
      return item.GetItemsWithSameTaxons(itemTaxonomyFieldName, relatedItemsTypeFullName, (string) null, (string) null, skip, take, (string) null, (string) null);
    }

    /// <summary>
    /// Returns other items from a specified type with at least one matching value for the specified taxonomy field.
    /// </summary>
    /// <param name="item">the current item</param>
    /// <param name="itemTaxonomyFieldName">the custom field holding taxons </param>
    /// <param name="relatedItemsTypeFullName">the full type name of items we want to load</param>
    /// <param name="relatedItemsTaxonomyFieldName">the custom field of the items we want to load if it is different then itemTaxonomyFieldName</param>
    /// <param name="relatedItemsProviderName">the provider used when loading the related items</param>
    /// <param name="skip">how many to skip</param>
    /// <param name="take">how many to take</param>
    /// <param name="additionalFilterExpression">additional filter expression</param>
    /// <param name="orderExpression">optional order expression</param>
    /// <returns></returns>
    public static IEnumerable GetItemsWithSameTaxons(
      this object item,
      string itemTaxonomyFieldName,
      string itemsTypeFullName,
      string relatedItemsTaxonomyFieldName = null,
      string itemsProviderName = null,
      int skip = 0,
      int take = 10,
      string additionalFilterExpression = null,
      string orderExpression = null)
    {
      IDataItem dataItem = item as IDataItem;
      bool isSingleTaxon = false;
      Type type;
      try
      {
        type = TypeResolutionService.ResolveType(itemsTypeFullName);
      }
      catch (ArgumentException ex)
      {
        Log.Error("TaxonomiesHelper.GetItemsWithSameTaxons : type cannot be resolved", (object) ex);
        return (IEnumerable) Enumerable.Empty<IDataItem>().AsQueryable<IDataItem>();
      }
      if (dataItem != null)
      {
        relatedItemsTaxonomyFieldName = relatedItemsTaxonomyFieldName != null ? relatedItemsTaxonomyFieldName : itemTaxonomyFieldName;
        itemsProviderName = itemsProviderName ?? ((DataProviderBase) dataItem.Provider).Name;
        IManager mappedManager = ManagerBase.GetMappedManager(type, itemsProviderName);
        string applicationName = itemsProviderName == null ? dataItem.ApplicationName : mappedManager.Provider.ApplicationName;
        IList<Guid> taxonIds;
        IQueryable queryable1;
        if (mappedManager is PageManager)
        {
          if (TypeDescriptor.GetProperties(typeof (PageNode)).Find(relatedItemsTaxonomyFieldName.Trim(), true) is TaxonomyPropertyDescriptor propertyDescriptor)
            isSingleTaxon = propertyDescriptor.MetaField.IsSingleTaxon;
          taxonIds = ((PageData) dataItem).NavigationNode.GetValue(relatedItemsTaxonomyFieldName.Trim()) as IList<Guid>;
          IQueryable<Guid> pageDataIds = SitefinityQuery.Get<PageData>(mappedManager.Provider).Where<PageData>((Expression<Func<PageData, bool>>) (c => c.ApplicationName == applicationName && (int) c.Status == 2 && c.Id != dataItem.Id)).Select<PageData, Guid>((Expression<Func<PageData, Guid>>) (c => c.Id));
          IQueryable queryable2 = (IQueryable) SitefinityQuery.Get<PageNode>(mappedManager.Provider).Where<PageNode>((Expression<Func<PageNode, bool>>) (c => c.ApplicationName == applicationName && pageDataIds.Contains<Guid>(c.PageId)));
          MethodCallExpression methodCallExpression = Expression.Call(typeof (Queryable), "Cast", new Type[1]
          {
            typeof (PageNode)
          }, queryable2.Expression);
          queryable1 = queryable2.Provider.CreateQuery((Expression) methodCallExpression);
        }
        else
        {
          if (TypeDescriptor.GetProperties(type).Find(relatedItemsTaxonomyFieldName.Trim(), true) is TaxonomyPropertyDescriptor propertyDescriptor)
            isSingleTaxon = propertyDescriptor.MetaField.IsSingleTaxon;
          taxonIds = ((IDynamicFieldsContainer) dataItem).GetValue(relatedItemsTaxonomyFieldName.Trim()) as IList<Guid>;
          queryable1 = SitefinityQuery.Get(type, mappedManager.Provider).Where("((ApplicationName=@0) AND (Id!=@1))", (object) applicationName, (object) dataItem.Id);
          if (typeof (ILifecycleDataItem).IsAssignableFrom(type))
            queryable1 = queryable1.Where("Status=@0", (object) ContentLifecycleStatus.Live);
        }
        if (taxonIds != null && taxonIds.Count > 0)
        {
          if (!string.IsNullOrEmpty(additionalFilterExpression))
            queryable1 = queryable1.Where(additionalFilterExpression);
          if (!string.IsNullOrEmpty(orderExpression))
            queryable1 = queryable1.OrderBy(orderExpression);
          return (IEnumerable) RelatedDataExtensions.AppendQueryFilterExpression(queryable1, itemTaxonomyFieldName.Trim(), taxonIds, isSingleTaxon).Skip(skip).Take(take);
        }
      }
      return (IEnumerable) Enumerable.Empty<IDataItem>().AsQueryable<IDataItem>();
    }

    public static Guid GetId(object item)
    {
      Guid id = Guid.Empty;
      switch (item)
      {
        case IRelatedDataHolder _:
          id = ((IRelatedDataHolder) item).ItemId;
          break;
        case ILifecycleDataItemGeneric lifecycleDataItemGeneric:
          id = lifecycleDataItemGeneric.Status != ContentLifecycleStatus.Master ? lifecycleDataItemGeneric.OriginalContentId : lifecycleDataItemGeneric.Id;
          break;
        case IDataItem dataItem:
          id = dataItem.Id;
          break;
        default:
          PropertyDescriptor property = TypeDescriptor.GetProperties(item)["Id"];
          if (property != null && property.PropertyType == typeof (Guid))
          {
            id = (Guid) property.GetValue(item);
            break;
          }
          break;
      }
      return id;
    }

    internal static string GetTypeName(object item)
    {
      string typeName = item.GetType().FullName;
      if (item is IRelatedDataHolder)
        typeName = ((IRelatedDataHolder) item).FullTypeName;
      else if ("ExtendedBlog".Equals(item.GetType().Name))
        typeName = "Telerik.Sitefinity.Blogs.Model.Blog";
      return typeName;
    }

    internal static ContentLifecycleStatus GetStatus(object item)
    {
      ContentLifecycleStatus status;
      switch (item)
      {
        case IRelatedDataHolder _:
          status = ((IRelatedDataHolder) item).Status;
          break;
        case ILifecycleDataItem lifecycleDataItem:
          status = ((IContentLocatableView) null).IsPreviewRequested() ? ContentLifecycleStatus.Live : lifecycleDataItem.Status;
          break;
        default:
          status = SystemManager.IsBackendRequest(out CultureInfo _) ? ContentLifecycleStatus.Master : ContentLifecycleStatus.Live;
          break;
      }
      return status;
    }

    internal static string GetProvider(object item)
    {
      string provider1 = string.Empty;
      switch (item)
      {
        case IRelatedDataHolder _:
          provider1 = ((IRelatedDataHolder) item).ProviderName;
          break;
        case IDataItem dataItem:
          if (dataItem.Provider is IDataItemProvider provider2)
          {
            provider1 = provider2.Name;
            break;
          }
          break;
      }
      return provider1;
    }

    private static IQueryable AppendQueryFilterExpression(
      IQueryable query,
      string taxonomyName,
      IList<Guid> taxonIds,
      bool isSingleTaxon)
    {
      if (isSingleTaxon)
      {
        string predicate = string.Format((IFormatProvider) CultureInfo.InvariantCulture, "({0}==(@0)) }", (object) taxonomyName);
        query = query.Where(predicate, (object) taxonIds.FirstOrDefault<Guid>());
      }
      else
      {
        string format = "{0}.Contains((@{1}))";
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("(");
        int num = 0;
        foreach (Guid taxonId in (IEnumerable<Guid>) taxonIds)
        {
          if (num > 0)
            stringBuilder.Append(" Or ");
          stringBuilder.Append(string.Format((IFormatProvider) CultureInfo.InvariantCulture, format, (object) taxonomyName, (object) num));
          ++num;
        }
        stringBuilder.Append(")");
        query = query.Where(stringBuilder.ToString(), taxonIds.Cast<object>().ToArray<object>());
      }
      return query;
    }
  }
}
