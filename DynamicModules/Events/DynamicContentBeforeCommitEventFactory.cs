// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Events.DynamicContentBeforeCommitEventFactory
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

namespace Telerik.Sitefinity.Data.Events
{
  /// <summary>
  /// Factory for constructing events for dynamic content that are raised before commit transaction.
  /// </summary>
  public class DynamicContentBeforeCommitEventFactory : IDynamicContentBeforeCommitEventFactory
  {
    /// <summary>Constructs event from item.</summary>
    /// <param name="dataItem">The data item.</param>
    /// <param name="actionType">State of the item (new, updated, deleted).</param>
    /// <param name="dataProviderName">Name of the data provide for the item.</param>
    /// <returns>An <see cref="T:Telerik.Sitefinity.Data.Events.IDataEvent" /> constructed from the data item.</returns>
    public IDataEvent CreateEvent(
      DynamicContent dataItem,
      SecurityConstants.TransactionActionType actionType,
      string dataProviderName)
    {
      return this.CreateEvent(dataItem, actionType, dataProviderName, (string) null);
    }

    /// <summary>Constructs event from item for specific language.</summary>
    /// <param name="dataItem">The data item.</param>
    /// <param name="actionType">State of the item (new, updated, deleted).</param>
    /// <param name="dataProviderName">Name of the data provide for the item.</param>
    /// <param name="language">The language for which the event is constructed.</param>
    /// <returns>An <see cref="T:Telerik.Sitefinity.Data.Events.IDataEvent" /> constructed from the data item for specified language.</returns>
    public IDataEvent CreateEvent(
      DynamicContent dataItem,
      SecurityConstants.TransactionActionType actionType,
      string dataProviderName,
      string language)
    {
      CultureInfo culture = string.IsNullOrEmpty(language) ? (CultureInfo) null : CultureInfo.GetCultureInfo(language);
      SecurityConstants.TransactionActionType transactionActionType = (SecurityConstants.TransactionActionType) Enum.Parse(typeof (SecurityConstants.TransactionActionType), DataEventFactory.GetActualItemAction((IDataItem) dataItem, actionType.ToString(), culture));
      ILifecycleDataItemGeneric lifecycleDataItemGeneric = (ILifecycleDataItemGeneric) dataItem;
      Guid originalContentId = Guid.Empty;
      if (lifecycleDataItemGeneric != null)
        originalContentId = lifecycleDataItemGeneric.OriginalContentId;
      string lifecycleStatus = dataItem.Status.ToString();
      DynamicContentEventBase contentEventByAction = this.CreateDynamicContentEventByAction(dataItem, transactionActionType, dataProviderName, originalContentId, lifecycleStatus, language);
      DynamicContentEventFactory.SetEventProperties(contentEventByAction, dataItem, transactionActionType, culture);
      return (IDataEvent) contentEventByAction;
    }

    private DynamicContentEventBase CreateDynamicContentEventByAction(
      DynamicContent dataItem,
      SecurityConstants.TransactionActionType actionType,
      string dataProviderName,
      Guid originalContentId,
      string lifecycleStatus,
      string language)
    {
      string str = actionType.ToString();
      Guid currentUserId = ClaimsManager.GetCurrentUserId();
      switch (actionType)
      {
        case SecurityConstants.TransactionActionType.New:
          return (DynamicContentEventBase) new DynamicContentCreatingEvent(dataItem, str, dataProviderName, originalContentId, currentUserId, lifecycleStatus, language);
        case SecurityConstants.TransactionActionType.Updated:
          return (DynamicContentEventBase) new DynamicContentUpdatingEvent(dataItem, str, dataProviderName, originalContentId, currentUserId, lifecycleStatus, language);
        case SecurityConstants.TransactionActionType.Deleted:
          return (DynamicContentEventBase) new DynamicContentDeletingEvent(dataItem, str, dataProviderName, originalContentId, currentUserId, lifecycleStatus, language);
        default:
          return new DynamicContentEventBase(dataItem.Id, dataItem.GetType(), str, dataProviderName, originalContentId, lifecycleStatus, language);
      }
    }
  }
}
