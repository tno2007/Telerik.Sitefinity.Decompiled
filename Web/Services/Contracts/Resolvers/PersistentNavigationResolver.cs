// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Contracts.PersistentNavigationResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Web.Services.Contracts
{
  internal class PersistentNavigationResolver : PropertyRelationResolverBase
  {
    private LifecycleStatus status;

    public override void Init(NameValueCollection parameters)
    {
      base.Init(parameters);
      Enum.TryParse<LifecycleStatus>(parameters["status"], out this.status);
    }

    public override object GetRelatedItem(object item, Guid relatedItemKey)
    {
      object relatedItem;
      this.GetRelatedItemAndManager(item, relatedItemKey, out relatedItem, out IManager _);
      return relatedItem;
    }

    public override void CreateRelation(
      object item,
      Guid relatedItemKey,
      string relatedItemprovider,
      object persistentItem)
    {
      object relatedItem;
      IManager manager;
      this.GetRelatedItemAndManager(item, relatedItemKey, out relatedItem, out manager);
      if (!this.IsMultipleRelation)
      {
        this.Descriptor.SetValue(item, relatedItem);
      }
      else
      {
        IList list = (IList) this.Descriptor.GetValue(item);
        ILifecycleDataItem lifecycleDataItem = item as ILifecycleDataItem;
        if (relatedItem is ILifecycleDataItem cnt)
        {
          if (lifecycleDataItem != null)
          {
            if (lifecycleDataItem.Status != cnt.Status)
              throw new InvalidOperationException("Content lifecycle statuses of main and related items must be the same.");
          }
          else
          {
            ILifecycleManager lifecycleManager = (ILifecycleManager) manager;
            if (cnt.Status != ContentLifecycleStatus.Master)
            {
              ILifecycleDataItem master = lifecycleManager.Lifecycle.GetMaster(cnt);
              if (master != null)
                list.Add((object) master);
            }
            if (cnt.Status != ContentLifecycleStatus.Live)
            {
              ILifecycleDataItem live = lifecycleManager.Lifecycle.GetLive(cnt);
              if (live != null)
                list.Add((object) live);
            }
            if (cnt.Status != ContentLifecycleStatus.Temp)
            {
              ILifecycleDataItem temp = lifecycleManager.Lifecycle.GetTemp(cnt);
              if (temp != null)
                list.Add((object) temp);
            }
          }
        }
        list.Add(relatedItem);
      }
    }

    public override void DeleteRelation(object item, Guid relatedItemKey)
    {
      if (!this.IsMultipleRelation)
      {
        this.Descriptor.SetValue(item, (object) null);
      }
      else
      {
        object relatedItem;
        IManager manager;
        this.GetRelatedItemAndManager(item, relatedItemKey, out relatedItem, out manager);
        IList list = (IList) this.Descriptor.GetValue(item);
        ILifecycleDataItem lifecycleDataItem = item as ILifecycleDataItem;
        if (relatedItem is ILifecycleDataItem cnt)
        {
          if (lifecycleDataItem != null)
          {
            if (lifecycleDataItem.Status != cnt.Status)
              throw new InvalidOperationException("Content lifecycle statuses of main and related items must be the same.");
          }
          else
          {
            ILifecycleManager lifecycleManager = (ILifecycleManager) manager;
            if (cnt.Status != ContentLifecycleStatus.Master)
            {
              ILifecycleDataItem master = lifecycleManager.Lifecycle.GetMaster(cnt);
              if (master != null)
                list.Remove((object) master);
            }
            if (cnt.Status != ContentLifecycleStatus.Live)
            {
              ILifecycleDataItem live = lifecycleManager.Lifecycle.GetLive(cnt);
              if (live != null)
                list.Remove((object) live);
            }
            if (cnt.Status != ContentLifecycleStatus.Temp)
            {
              ILifecycleDataItem temp = lifecycleManager.Lifecycle.GetTemp(cnt);
              if (temp != null)
                list.Remove((object) temp);
            }
          }
        }
        list.Remove(relatedItem);
      }
    }

    public override void DeleteAllRelations(object item)
    {
      if (!this.IsMultipleRelation)
        this.Descriptor.SetValue(item, (object) null);
      else
        ((IList) this.Descriptor.GetValue(item)).Clear();
    }

    public override IQueryable GetRelatedItems(object item)
    {
      object source1 = this.Descriptor.GetValue(item);
      if (!this.IsMultipleRelation)
        return (IQueryable) ((IEnumerable<object>) new object[1]
        {
          source1
        }).AsQueryable<object>();
      IQueryable source2 = ((IEnumerable) source1).AsQueryable();
      if (!typeof (ILifecycleDataItem).IsAssignableFrom(this.RelatedType))
        return source2;
      ContentLifecycleStatus status = this.status == LifecycleStatus.Live ? ContentLifecycleStatus.Live : ContentLifecycleStatus.Master;
      return (source2 as IQueryable<ILifecycleDataItem>).Where<ILifecycleDataItem>((Expression<Func<ILifecycleDataItem, bool>>) (i => (int) i.Status == (int) status)).Cast(this.RelatedType);
    }

    private void GetRelatedItemAndManager(
      object item,
      Guid relatedItemKey,
      out object relatedItem,
      out IManager manager)
    {
      DataProviderBase provider = (DataProviderBase) ((IDataItem) item).GetProvider();
      manager = ManagerBase.GetMappedManagerInTransaction(this.RelatedType, provider.TransactionName);
      relatedItem = manager.GetItemOrDefault(this.RelatedType, relatedItemKey);
    }
  }
}
