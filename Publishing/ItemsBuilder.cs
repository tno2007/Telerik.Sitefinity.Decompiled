// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.ItemsBuilder
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Globalization;
using Telerik.Sitefinity.Data.Events;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Publishing
{
  internal static class ItemsBuilder
  {
    public static PublishingSystemEventInfo ToPubSysEventInfo(
      this IDataEvent dataEvent,
      SecurityConstants.TransactionActionType? changedAction = null)
    {
      SecurityConstants.TransactionActionType transactionActionType = (SecurityConstants.TransactionActionType) Enum.Parse(typeof (SecurityConstants.TransactionActionType), dataEvent.Action);
      if (changedAction.HasValue)
        transactionActionType = changedAction.Value;
      switch (transactionActionType)
      {
        case SecurityConstants.TransactionActionType.New:
        case SecurityConstants.TransactionActionType.Updated:
          return ItemsBuilder.CreateModifiedObjectActionArgs(dataEvent);
        case SecurityConstants.TransactionActionType.Deleted:
          return ItemsBuilder.CreateRemovedObjectActionArgs(dataEvent, "SystemObjectDeleted");
        default:
          return (PublishingSystemEventInfo) null;
      }
    }

    private static PublishingSystemEventInfo CreateRemovedObjectActionArgs(
      IDataEvent item,
      string action)
    {
      Guid itemId = item.ItemId;
      Guid guid = !(item is ILifecycleEvent lifecycleItem) || !(lifecycleItem.OriginalContentId != Guid.Empty) ? itemId : lifecycleItem.OriginalContentId;
      ContentLifecycleStatus objectLifecycleStatus = ItemsBuilder.GetRemoveObjectLifecycleStatus(lifecycleItem);
      WrapperObject wrapperObject = new WrapperObject((object) null);
      wrapperObject.AddProperty("OriginalContentId", (object) guid);
      wrapperObject.AddProperty("Id", (object) itemId);
      wrapperObject.AddProperty("ItemId", (object) itemId);
      wrapperObject.AddProperty("OriginalItemId", (object) itemId);
      wrapperObject.AddProperty("Provider", (object) (item.ProviderName ?? string.Empty));
      wrapperObject.AddProperty("LifecycleStatus", (object) objectLifecycleStatus);
      return new PublishingSystemEventInfo()
      {
        Item = (object) wrapperObject,
        ItemAction = action,
        ItemType = item.ItemType.FullName,
        Language = ItemsBuilder.GetCulture(item).Name
      };
    }

    private static ContentLifecycleStatus GetRemoveObjectLifecycleStatus(
      ILifecycleEvent lifecycleItem)
    {
      ContentLifecycleStatus objectLifecycleStatus = ContentLifecycleStatus.Live;
      if (lifecycleItem != null)
      {
        string status1 = lifecycleItem.Status;
        ContentLifecycleStatus contentLifecycleStatus = ContentLifecycleStatus.Master;
        string str1 = contentLifecycleStatus.ToString();
        if (!(status1 == str1))
        {
          string status2 = lifecycleItem.Status;
          contentLifecycleStatus = ContentLifecycleStatus.Deleted;
          string str2 = contentLifecycleStatus.ToString();
          if (!(status2 == str2))
            goto label_4;
        }
        objectLifecycleStatus = ContentLifecycleStatus.Master;
      }
label_4:
      return objectLifecycleStatus;
    }

    private static PublishingSystemEventInfo CreateModifiedObjectActionArgs(
      IDataEvent dataEvent)
    {
      if (typeof (IFolder).IsAssignableFrom(dataEvent.ItemType))
        return (PublishingSystemEventInfo) null;
      CultureInfo culture = ItemsBuilder.GetCulture(dataEvent);
      PublishingSystemEventInfo objectActionArgs = new PublishingSystemEventInfo()
      {
        ItemAction = "SystemObjectModified",
        Language = culture.Name
      };
      if (typeof (IDataItem).IsAssignableFrom(dataEvent.ItemType))
      {
        WrapperObjectWithDataItemLoader withDataItemLoader1 = new WrapperObjectWithDataItemLoader();
        withDataItemLoader1.parent = objectActionArgs;
        withDataItemLoader1.ItemId = dataEvent.ItemId;
        withDataItemLoader1.ProviderName = dataEvent.ProviderName;
        withDataItemLoader1.Language = culture.Name;
        withDataItemLoader1.TransactionName = dataEvent is DataEvent ? ((DataEvent) dataEvent).TransactionName : (string) null;
        WrapperObjectWithDataItemLoader withDataItemLoader2 = withDataItemLoader1;
        objectActionArgs.Item = (object) withDataItemLoader2;
      }
      objectActionArgs.ItemType = dataEvent.ItemType.FullName;
      return objectActionArgs;
    }

    [Obsolete("Use IEvent.GetCultureOrCurrent() extension method instead.")]
    internal static CultureInfo GetCulture(IDataEvent item) => !(item is IMultilingualEvent multilingualEvent) || multilingualEvent.Language == null ? SystemManager.CurrentContext.Culture : CultureInfo.GetCultureInfo(multilingualEvent.Language);
  }
}
