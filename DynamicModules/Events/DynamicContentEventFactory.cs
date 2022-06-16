// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Events.DynamicContentEventFactory
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Globalization;
using Telerik.Sitefinity.DynamicModules.Events;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Data.Events
{
  internal class DynamicContentEventFactory
  {
    public static IDataEvent CreateEvent(
      DynamicContent dataItem,
      SecurityConstants.TransactionActionType action)
    {
      return DynamicContentEventFactory.CreateEvent(dataItem, action, (string) null);
    }

    public static IDataEvent CreateEvent(
      DynamicContent dataItem,
      SecurityConstants.TransactionActionType action,
      string language)
    {
      ILifecycleDataItemGeneric lifecycleDataItemGeneric = (ILifecycleDataItemGeneric) dataItem;
      Guid originalContentId = Guid.Empty;
      if (lifecycleDataItemGeneric != null && lifecycleDataItemGeneric.OriginalContentId != Guid.Empty)
        originalContentId = lifecycleDataItemGeneric.OriginalContentId;
      string providerName = DataEventFactory.GetProviderName(dataItem.Provider);
      if (language == null && SystemManager.CurrentContext.AppSettings.Multilingual)
        language = SystemManager.CurrentContext.Culture.Name;
      return (IDataEvent) DynamicContentEventFactory.CreateEvent(dataItem, dataItem.GetType(), action, providerName, originalContentId, language);
    }

    internal static void SetEventProperties(
      DynamicContentEventBase dynamicContentEvent,
      DynamicContent dynamicContent,
      SecurityConstants.TransactionActionType action,
      CultureInfo culture)
    {
      string str = (string) null;
      ILifecycleDataItem lifecycleDataItem = (ILifecycleDataItem) dynamicContent;
      bool flag = false;
      if (lifecycleDataItem != null)
      {
        flag = lifecycleDataItem.Visible;
        str = lifecycleDataItem.Status.ToString();
      }
      dynamicContentEvent.Visible = flag;
      dynamicContentEvent.Status = str;
      IApprovalWorkflowItem approvalWorkflowItem = (IApprovalWorkflowItem) dynamicContent;
      if (approvalWorkflowItem != null)
        dynamicContentEvent.ApprovalWorkflowState = approvalWorkflowItem.ApprovalWorkflowState.GetString(culture, true);
      dynamicContentEvent.Title = DataEventFactory.GetItemTitle((IDataItem) dynamicContent, action.ToString(), culture);
      dynamicContentEvent.SetIApprovalWorkflowEventPropertiesFromTrackingContext((IDataItem) dynamicContent);
      dynamicContentEvent.SetIRecyclableEventPropertiesFromTrackingContext((IDataItem) dynamicContent);
      if (action != SecurityConstants.TransactionActionType.Updated)
        return;
      DataEventFactory.SetChangedProperties((IPropertyChangeDataEvent) dynamicContentEvent, (IDataItem) dynamicContent, culture);
    }

    private static DynamicContentEventBase CreateEvent(
      DynamicContent dynamicContent,
      Type itemType,
      SecurityConstants.TransactionActionType action,
      string dataProviderName,
      Guid originalContentId,
      string language)
    {
      CultureInfo culture = string.IsNullOrEmpty(language) ? (CultureInfo) null : CultureInfo.GetCultureInfo(language);
      string actualItemAction = DataEventFactory.GetActualItemAction((IDataItem) dynamicContent, action.ToString(), culture);
      action = (SecurityConstants.TransactionActionType) Enum.Parse(typeof (SecurityConstants.TransactionActionType), actualItemAction);
      Guid currentUserId = ClaimsManager.GetCurrentUserId();
      string status = dynamicContent.Status.ToString();
      DynamicContentEventBase dynamicContentEvent;
      switch (action)
      {
        case SecurityConstants.TransactionActionType.New:
          dynamicContentEvent = (DynamicContentEventBase) new DynamicContentCreatedEvent(dynamicContent, itemType, actualItemAction, dataProviderName, originalContentId, status, language, currentUserId, dynamicContent.DateCreated);
          break;
        case SecurityConstants.TransactionActionType.Updated:
          dynamicContentEvent = (DynamicContentEventBase) new DynamicContentUpdatedEvent(dynamicContent, itemType, actualItemAction, dataProviderName, originalContentId, status, language, currentUserId, dynamicContent.LastModified);
          break;
        case SecurityConstants.TransactionActionType.Deleted:
          dynamicContentEvent = (DynamicContentEventBase) new DynamicContentDeletedEvent(dynamicContent, itemType, actualItemAction, dataProviderName, originalContentId, status, language, currentUserId, dynamicContent.LastModified);
          break;
        default:
          dynamicContentEvent = new DynamicContentEventBase(dynamicContent.Id, itemType, actualItemAction, dataProviderName, originalContentId, status, language);
          break;
      }
      DynamicContentEventFactory.SetEventProperties(dynamicContentEvent, dynamicContent, action, culture);
      return dynamicContentEvent;
    }
  }
}
