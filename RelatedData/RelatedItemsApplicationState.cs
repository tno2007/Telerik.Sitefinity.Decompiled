// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.RelatedData.RelatedItemsApplicationState
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.RelatedData
{
  internal class RelatedItemsApplicationState : IRelatedItemsApplicationState
  {
    private static string contextItemName = nameof (RelatedItemsApplicationState);
    private Dictionary<string, List<RelatedDataWrapper>> items = new Dictionary<string, List<RelatedDataWrapper>>();

    /// <inheritdoc />
    public static RelatedItemsApplicationState GetCurrentContextApplicationState() => SystemManager.CurrentHttpContext.Items[(object) RelatedItemsApplicationState.ContextItemName] as RelatedItemsApplicationState;

    /// <inheritdoc />
    public string GenerateCacheKey(string itemTypeName, string itemProviderName) => itemTypeName + itemProviderName;

    /// <inheritdoc />
    public Dictionary<string, List<RelatedDataWrapper>> Items => this.items;

    /// <inheritdoc />
    public void SetDataSourceToContext(IEnumerable<IDataItem> items)
    {
      this.SetRelatedItemsDataSource(items);
      SystemManager.CurrentHttpContext.Items[(object) RelatedItemsApplicationState.ContextItemName] = (object) this;
    }

    /// <inheritdoc />
    public void SetRelatedItemsDataSource(IEnumerable<IDataItem> items)
    {
      if (items.Count<IDataItem>() <= 0)
        return;
      IDataItem dataItem = items.First<IDataItem>();
      string typeName = RelatedDataExtensions.GetTypeName((object) dataItem);
      string provider = RelatedDataExtensions.GetProvider((object) dataItem);
      this.SetRelatedItemsDataSource(items, typeName, provider);
    }

    /// <inheritdoc />
    public void SetRelatedItemsDataSource(
      IEnumerable<IDataItem> items,
      string itemTypeName,
      string itemProviderName)
    {
      if (items.Count<IDataItem>() <= 0)
        return;
      string cacheKey = this.GenerateCacheKey(itemTypeName, itemProviderName);
      List<RelatedDataWrapper> second = new List<RelatedDataWrapper>();
      foreach (IDataItem theInstance in items)
      {
        RelatedDataWrapper relatedDataWrapper = new RelatedDataWrapper((object) theInstance, RelatedDataExtensions.GetId((object) theInstance));
        second.Add(relatedDataWrapper);
      }
      if (this.items.ContainsKey(cacheKey))
        this.items[cacheKey] = this.items[cacheKey].Union<RelatedDataWrapper>((IEnumerable<RelatedDataWrapper>) second).ToList<RelatedDataWrapper>();
      else
        this.items[cacheKey] = second;
    }

    /// <inheritdoc />
    public IQueryable<IDataItem> GetRelatedItems(
      string itemTypeName,
      string itemProviderName,
      string fieldName,
      Guid relatedItemId,
      ContentLifecycleStatus status,
      ref int? totalCount,
      Type childItemType = null)
    {
      string cacheKey = this.GenerateCacheKey(itemTypeName, itemProviderName);
      List<RelatedDataWrapper> source1 = this.Items.ContainsKey(cacheKey) ? this.Items[cacheKey] : (List<RelatedDataWrapper>) null;
      string childItemTypeName = childItemType != (Type) null ? childItemType.FullName : (string) null;
      if (source1 != null)
      {
        RelatedDataWrapper relatedDataWrapper1 = source1.FirstOrDefault<RelatedDataWrapper>((Func<RelatedDataWrapper, bool>) (ro => ro.Id == relatedItemId));
        if (relatedDataWrapper1 != null)
        {
          IEnumerable<IDataItem> source2;
          if (!relatedDataWrapper1.TryGetProperty<IEnumerable<IDataItem>>(fieldName + "-list", out source2))
          {
            List<Guid> list = source1.Select<RelatedDataWrapper, Guid>((Func<RelatedDataWrapper, Guid>) (ri => ri.Id)).ToList<Guid>();
            Dictionary<Guid, List<IDataItem>> relatedItems = RelatedDataHelper.GetRelatedItems(itemTypeName, itemProviderName, list, fieldName, new ContentLifecycleStatus?(status), childItemTypeName);
            foreach (RelatedDataWrapper relatedDataWrapper2 in source1)
            {
              List<IDataItem> dataItemList = relatedItems.ContainsKey(relatedDataWrapper2.Id) ? relatedItems[relatedDataWrapper2.Id] : (List<IDataItem>) null;
              relatedDataWrapper2.SetOrAddProperty(fieldName + "-list", (object) dataItemList);
            }
            SystemManager.CurrentHttpContext.Items[(object) RelatedItemsApplicationState.ContextItemName] = (object) this;
          }
          relatedDataWrapper1.TryGetProperty<IEnumerable<IDataItem>>(fieldName + "-list", out source2);
          source2 = source2 ?? (IEnumerable<IDataItem>) new List<IDataItem>();
          totalCount = new int?(source2.Count<IDataItem>());
          return source2.AsQueryable<IDataItem>();
        }
      }
      return Queryable.OfType<IDataItem>(RelatedDataHelper.GetRelatedItems(itemTypeName, itemProviderName, relatedItemId, fieldName, new ContentLifecycleStatus?(status), (string) null, (string) null, new int?(), new int?(), ref totalCount, childItemTypeName)).AsQueryable<IDataItem>();
    }

    /// <inheritdoc />
    public IQueryable<IDataItem> GetRelatedItems(
      IDataItem item,
      string fieldName,
      Type childItemType)
    {
      string typeName = RelatedDataExtensions.GetTypeName((object) item);
      string provider = RelatedDataExtensions.GetProvider((object) item);
      string cacheKey = this.GenerateCacheKey(typeName, provider);
      List<RelatedDataWrapper> source1 = this.Items.ContainsKey(cacheKey) ? this.Items[cacheKey] : (List<RelatedDataWrapper>) null;
      string childItemTypeName = childItemType != (Type) null ? childItemType.FullName : (string) null;
      Guid itemId = RelatedDataExtensions.GetId((object) item);
      ContentLifecycleStatus status = RelatedDataExtensions.GetStatus((object) item);
      if (source1 != null)
      {
        RelatedDataWrapper relatedDataWrapper1 = source1.FirstOrDefault<RelatedDataWrapper>((Func<RelatedDataWrapper, bool>) (ro => ro.Id == itemId));
        if (relatedDataWrapper1 != null)
        {
          IEnumerable<IDataItem> source2;
          if (!relatedDataWrapper1.TryGetProperty<IEnumerable<IDataItem>>(fieldName + "-list", out source2))
          {
            List<Guid> list = source1.Select<RelatedDataWrapper, Guid>((Func<RelatedDataWrapper, Guid>) (ri => ri.Id)).ToList<Guid>();
            Dictionary<Guid, List<IDataItem>> relatedItems = RelatedDataHelper.GetRelatedItems(typeName, provider, list, fieldName, new ContentLifecycleStatus?(status), childItemTypeName);
            foreach (RelatedDataWrapper relatedDataWrapper2 in source1)
            {
              List<IDataItem> dataItemList = relatedItems.ContainsKey(relatedDataWrapper2.Id) ? relatedItems[relatedDataWrapper2.Id] : (List<IDataItem>) null;
              relatedDataWrapper2.SetOrAddProperty(fieldName + "-list", (object) dataItemList);
            }
            SystemManager.CurrentHttpContext.Items[(object) RelatedItemsApplicationState.ContextItemName] = (object) this;
          }
          relatedDataWrapper1.TryGetProperty<IEnumerable<IDataItem>>(fieldName + "-list", out source2);
          return source2 == null ? (IQueryable<IDataItem>) null : source2.AsQueryable<IDataItem>();
        }
      }
      return Queryable.OfType<IDataItem>(RelatedDataHelper.GetRelatedItems(typeName, itemId, provider, fieldName, new ContentLifecycleStatus?(status)));
    }

    /// <summary>Gets the context item name</summary>
    public static string ContextItemName => RelatedItemsApplicationState.contextItemName;
  }
}
