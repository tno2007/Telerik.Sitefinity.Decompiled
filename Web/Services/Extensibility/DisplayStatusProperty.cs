// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Extensibility.DisplayStatusProperty
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Workflow;
using Telerik.Sitefinity.Workflow.Model.Tracking;

namespace Telerik.Sitefinity.Web.Services.Extensibility
{
  /// <summary>
  /// A locked by property for retrieving which item is locked.
  /// </summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  internal class DisplayStatusProperty : CalculatedProperty
  {
    internal const string ScheduledPublishUnpublishMessage = "Scheduled publish ${PublicationDate}, unpublish ${ExpirationDate}";
    internal const string ScheduledPublishMessage = "Scheduled publish ${PublicationDate}";
    internal const string ScheduledUnpublishMessage = "Scheduled unpublish ${ExpirationDate}";
    private string[] propertiesToIncludeInFetch = new string[2]
    {
      "LanguageData",
      "PublishedTranslations"
    };

    /// <inheritdoc />
    public override Type ReturnType => typeof (IEnumerable<DisplayStatus>);

    internal override string[] PropertiesToIncludeInFetch => this.propertiesToIncludeInFetch;

    /// <inheritdoc />
    public override IDictionary<object, object> GetValues(
      IEnumerable items,
      IManager manager)
    {
      Dictionary<object, object> values = new Dictionary<object, object>();
      List<ILifecycleDataItemGeneric> list1 = items.Cast<ILifecycleDataItemGeneric>().ToList<ILifecycleDataItemGeneric>();
      CultureInfo culture = SystemManager.CurrentContext.Culture;
      IList<ILifecycleDataItemGeneric> masterItems = DisplayStatusProperty.GetMasterItems((IList<ILifecycleDataItemGeneric>) list1, manager, culture);
      IList<ILifecycleDataItemGeneric> liveItems = DisplayStatusProperty.GetLiveItems((IList<ILifecycleDataItemGeneric>) list1, manager, culture);
      ILookup<Guid, ApprovalTrackingRecord> lookup = IApprovalWorkflowExtensions.GetApprovalRecords((IList<IWorkflowItem>) masterItems.Cast<IWorkflowItem>().ToList<IWorkflowItem>(), (IOpenAccessDataProvider) manager.Provider).ToLookup<ApprovalTrackingRecord, Guid, ApprovalTrackingRecord>((Func<ApprovalTrackingRecord, Guid>) (x => x.WorkflowItemId), (Func<ApprovalTrackingRecord, ApprovalTrackingRecord>) (y => y));
      foreach (ILifecycleDataItemGeneric key1 in list1)
      {
        List<DisplayStatus> statuses = new List<DisplayStatus>();
        List<ApprovalTrackingRecord> list2 = lookup[key1.Id].ToList<ApprovalTrackingRecord>();
        IList<DisplayStatus> collection = this.ResolveStatuses(key1, culture, (IList<ApprovalTrackingRecord>) list2, masterItems, liveItems);
        statuses.AddRange((IEnumerable<DisplayStatus>) collection);
        Status status = StatusResolver.Resolve(key1.GetType(), manager.Provider.Name, key1.Id);
        if (status != null)
          statuses.Add(new DisplayStatus()
          {
            Name = status.Text,
            Source = status.PrimaryProvider,
            Label = status.Text,
            DetailedLabel = status.Text
          });
        values.Add((object) key1, (object) statuses);
        if (statuses != null)
        {
          Guid key2 = key1.Status == ContentLifecycleStatus.Master ? key1.Id : key1.OriginalContentId;
          List<ApprovalTrackingRecord> list3 = lookup[key2].ToList<ApprovalTrackingRecord>();
          this.PopulateStatusDetails(key1, (IList<DisplayStatus>) statuses, (IList<ApprovalTrackingRecord>) list3, masterItems, liveItems);
        }
      }
      return (IDictionary<object, object>) values;
    }

    internal static IList<ILifecycleDataItemGeneric> GetLiveItems(
      IList<ILifecycleDataItemGeneric> items,
      IManager manager,
      CultureInfo culture)
    {
      IList<ILifecycleDataItemGeneric> liveItems = items;
      if (items.Count > 0 && items.First<ILifecycleDataItemGeneric>().Status != ContentLifecycleStatus.Live)
      {
        Type type = items.First<ILifecycleDataItemGeneric>().GetType();
        List<Guid> masterIds = items.Select<ILifecycleDataItemGeneric, Guid>((Func<ILifecycleDataItemGeneric, Guid>) (x => !(x.OriginalContentId == Guid.Empty) ? x.OriginalContentId : x.Id)).ToList<Guid>();
        liveItems = (IList<ILifecycleDataItemGeneric>) (manager.GetItems(type, (string) null, (string) null, 0, 0) as IQueryable<ILifecycleDataItemGeneric>).Where<ILifecycleDataItemGeneric>((Expression<Func<ILifecycleDataItemGeneric, bool>>) (x => masterIds.Contains(x.OriginalContentId) && (int) x.Status == 2)).LoadWith<ILifecycleDataItemGeneric>(manager.GetFetchStrategyForItem(type, (string[]) null, true)).ToList<ILifecycleDataItemGeneric>();
      }
      return liveItems;
    }

    internal static IList<ILifecycleDataItemGeneric> GetMasterItems(
      IList<ILifecycleDataItemGeneric> items,
      IManager manager,
      CultureInfo culture)
    {
      IList<ILifecycleDataItemGeneric> masterItems = items;
      if (items.Count > 0 && items.First<ILifecycleDataItemGeneric>().Status != ContentLifecycleStatus.Master)
      {
        Type type = items.First<ILifecycleDataItemGeneric>().GetType();
        List<Guid> masterIds = items.Select<ILifecycleDataItemGeneric, Guid>((Func<ILifecycleDataItemGeneric, Guid>) (x => x.OriginalContentId)).ToList<Guid>();
        masterItems = (IList<ILifecycleDataItemGeneric>) (manager.GetItems(type, (string) null, (string) null, 0, 0) as IQueryable<ILifecycleDataItemGeneric>).Where<ILifecycleDataItemGeneric>((Expression<Func<ILifecycleDataItemGeneric, bool>>) (x => masterIds.Contains(x.Id))).LoadWith<ILifecycleDataItemGeneric>(manager.GetFetchStrategyForItem(type, (string[]) null, true)).ToList<ILifecycleDataItemGeneric>();
      }
      return masterItems;
    }

    internal static ItemEventInfo GetFallBackItemInfo(
      string workflowState,
      ILifecycleDataItemGeneric item,
      IList<ILifecycleDataItemGeneric> masterItems,
      IList<ILifecycleDataItemGeneric> liveItems)
    {
      ItemEventInfo fallBackItemInfo = (ItemEventInfo) null;
      if (workflowState == "Published")
      {
        ILifecycleDataItemGeneric lifecycleDataItemGeneric = item;
        if (item.Status != ContentLifecycleStatus.Live)
          lifecycleDataItemGeneric = liveItems.Where<ILifecycleDataItemGeneric>((Func<ILifecycleDataItemGeneric, bool>) (x => x.OriginalContentId == item.Id)).FirstOrDefault<ILifecycleDataItemGeneric>();
        fallBackItemInfo = DisplayStatusProperty.ExtractFallBackValue(lifecycleDataItemGeneric);
      }
      else if (workflowState != null)
      {
        ILifecycleDataItemGeneric lifecycleDataItemGeneric = item;
        if (item.Status != ContentLifecycleStatus.Master)
          lifecycleDataItemGeneric = masterItems.Where<ILifecycleDataItemGeneric>((Func<ILifecycleDataItemGeneric, bool>) (x => x.Id == item.OriginalContentId)).First<ILifecycleDataItemGeneric>();
        fallBackItemInfo = DisplayStatusProperty.ExtractFallBackValue(lifecycleDataItemGeneric);
      }
      return fallBackItemInfo;
    }

    private static ItemEventInfo ExtractFallBackValue(ILifecycleDataItemGeneric item)
    {
      if (item == null)
        return (ItemEventInfo) null;
      return new ItemEventInfo()
      {
        Date = !(item is IContent content) ? item.LastModified : content.DateCreated,
        User = UserProfilesHelper.GetUserDisplayName(item.Owner)
      };
    }

    [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Reviewed")]
    private IList<DisplayStatus> ResolveStatuses(
      ILifecycleDataItemGeneric item,
      CultureInfo culture,
      IList<ApprovalTrackingRecord> approvalRecords,
      IList<ILifecycleDataItemGeneric> masterItems,
      IList<ILifecycleDataItemGeneric> liveItems)
    {
      List<DisplayStatus> itemStatuses = new List<DisplayStatus>();
      string statusKey;
      IStatusInfo statusInfo;
      string localizedStatus = (item as IApprovalWorkflowItem).GetLocalizedStatus(out statusKey, out statusInfo, culture);
      if (statusKey.IsNullOrEmpty())
        statusKey = "Draft";
      if (statusKey == "Published" || statusKey == "Draft" || statusKey == "Unpublished")
      {
        DisplayStatus displayStatus = new DisplayStatus()
        {
          Name = statusKey,
          Source = StatusSource.Lifecycle.ToString(),
          Label = this.SetNewerThanPublishedStatus(item, DisplayStatusProperty.GetStatusLabel(statusKey), masterItems, liveItems),
          DetailedLabel = this.SetNewerThanPublishedStatus(item, DisplayStatusProperty.GetDetailedStatusLabel(statusKey), masterItems, liveItems)
        };
        itemStatuses.Add(displayStatus);
        this.ResolveScheduledUnpublishStatus(statusKey, statusInfo, localizedStatus, itemStatuses);
      }
      else if (statusKey == "Scheduled")
      {
        string label = this.SetNewerThanPublishedStatus(item, localizedStatus, masterItems, liveItems);
        this.ResolveScheduledStatus(statusKey, statusInfo, localizedStatus, itemStatuses, label);
      }
      else if (statusKey == "Rejected")
        this.ResolveWorkflowStatus(statusKey, itemStatuses, approvalRecords, localizedStatus);
      else
        this.ResolveWorkflowStatus(statusKey, itemStatuses, approvalRecords);
      return (IList<DisplayStatus>) itemStatuses;
    }

    private string SetNewerThanPublishedStatus(
      ILifecycleDataItemGeneric item,
      string status,
      IList<ILifecycleDataItemGeneric> masterItems,
      IList<ILifecycleDataItemGeneric> liveItems)
    {
      ILifecycleDataItemGeneric liveItem;
      if (item.Status == ContentLifecycleStatus.Master)
      {
        liveItem = liveItems.Where<ILifecycleDataItemGeneric>((Func<ILifecycleDataItemGeneric, bool>) (x => x.OriginalContentId == item.Id)).FirstOrDefault<ILifecycleDataItemGeneric>();
        ILifecycleDataItemGeneric lifecycleDataItemGeneric = item;
      }
      else
      {
        liveItem = item;
        masterItems.Where<ILifecycleDataItemGeneric>((Func<ILifecycleDataItemGeneric, bool>) (x => x.Id == liveItem.OriginalContentId)).First<ILifecycleDataItemGeneric>();
      }
      return liveItem != null && liveItem.Visible && liveItem.IsPublished() && item.IsNewer((ILifecycleDataItem) liveItem) ? string.Format("{0}, {1}", (object) status, (object) Res.Get<Labels>().NewerThanPublished) : status;
    }

    private void PopulateStatusDetails(
      ILifecycleDataItemGeneric item,
      IList<DisplayStatus> statuses,
      IList<ApprovalTrackingRecord> records,
      IList<ILifecycleDataItemGeneric> masterItems,
      IList<ILifecycleDataItemGeneric> liveItems)
    {
      IOrderedEnumerable<ApprovalTrackingRecord> source = records.OrderByDescending<ApprovalTrackingRecord, DateTime>((Func<ApprovalTrackingRecord, DateTime>) (x => x.DateCreated));
      foreach (DisplayStatus statuse in (IEnumerable<DisplayStatus>) statuses)
      {
        DisplayStatus entry = statuse;
        ApprovalTrackingRecord approvalTrackingRecord = source.FirstOrDefault<ApprovalTrackingRecord>((Func<ApprovalTrackingRecord, bool>) (x => x.Status == entry.Name));
        if (approvalTrackingRecord != null)
        {
          entry.Date = approvalTrackingRecord.DateCreated;
          entry.User = UserProfilesHelper.GetUserDisplayName(approvalTrackingRecord.UserId);
        }
        else if (entry.Name == "Scheduled")
        {
          entry.Date = item.LastModified;
          entry.User = UserProfilesHelper.GetUserDisplayName(item.LastModifiedBy);
        }
        else if (records.Count == 0)
        {
          ItemEventInfo fallBackItemInfo = DisplayStatusProperty.GetFallBackItemInfo(entry.Name, item, masterItems, liveItems);
          if (fallBackItemInfo != null)
          {
            entry.Date = fallBackItemInfo.Date;
            entry.User = fallBackItemInfo.User;
          }
        }
      }
    }

    protected void ResolveWorkflowStatus(
      string workflowState,
      List<DisplayStatus> itemStatuses,
      IList<ApprovalTrackingRecord> approvalRecords,
      string workflowStateLabel = null)
    {
      DisplayStatus displayStatus = new DisplayStatus()
      {
        Name = workflowState,
        Source = StatusSource.Workflow.ToString(),
        Label = string.IsNullOrEmpty(workflowStateLabel) ? DisplayStatusProperty.GetStatusLabel(workflowState) : workflowStateLabel,
        DetailedLabel = DisplayStatusProperty.GetDetailedStatusLabel(workflowState)
      };
      string str1 = string.Empty;
      string str2 = workflowState;
      if (!(str2 == "AwaitingApproval") && !(str2 == "AwaitingPublishing") && !(str2 == "AwaitingReview"))
      {
        if (str2 == "Rejected" || str2 == "RejectedForPublishing" || str2 == "RejectedForReview")
          str1 = "Reason to reject";
      }
      else
        str1 = "Notes for approvers";
      if (!str1.IsNullOrWhitespace())
      {
        ApprovalTrackingRecord approvalTrackingRecord = approvalRecords.OrderByDescending<ApprovalTrackingRecord, DateTime>((Func<ApprovalTrackingRecord, DateTime>) (x => x.DateCreated)).FirstOrDefault<ApprovalTrackingRecord>((Func<ApprovalTrackingRecord, bool>) (x => x.Status == workflowState));
        if (approvalTrackingRecord != null)
          displayStatus.Message = new Message()
          {
            Title = str1,
            Description = approvalTrackingRecord.Note
          };
      }
      itemStatuses.Add(displayStatus);
    }

    protected void ResolveScheduledStatus(
      string workflowState,
      IStatusInfo statusInfo,
      string workflowStateMessage,
      List<DisplayStatus> itemStatuses,
      string label)
    {
      ISchedulingStatus schedulingStatus = statusInfo != null ? statusInfo.Data as ISchedulingStatus : (ISchedulingStatus) null;
      if (schedulingStatus == null)
        return;
      DisplayStatus displayStatus1 = new DisplayStatus()
      {
        Name = workflowState,
        Source = StatusSource.Scheduling.ToString(),
        Label = label,
        DetailedLabel = DisplayStatusProperty.GetDetailedStatusLabel(workflowState),
        PublicationDate = schedulingStatus.PublicationDate,
        ExpirationDate = schedulingStatus.ExpirationDate
      };
      DisplayStatus displayStatus2 = displayStatus1;
      Message message1 = new Message();
      message1.Operations = new ItemOperation[1]
      {
        new ItemOperation()
        {
          Title = Res.Get<WorkflowResources>().StopSchedulingAction,
          Name = "StopSchedule",
          Category = new OperationCategory("Workflow"),
          ExecuteOnServer = true
        }
      };
      Message message2 = message1;
      displayStatus2.Message = message2;
      if (schedulingStatus.PublicationDate.HasValue)
      {
        displayStatus1.Message.Description = "Scheduled publish ${PublicationDate}";
        if (schedulingStatus.ExpirationDate.HasValue)
          displayStatus1.Message.Description = "Scheduled publish ${PublicationDate}, unpublish ${ExpirationDate}";
      }
      itemStatuses.Add(displayStatus1);
    }

    protected void ResolveScheduledUnpublishStatus(
      string workflowState,
      IStatusInfo statusInfo,
      string workflowStateMessage,
      List<DisplayStatus> itemStatuses)
    {
      if (!(workflowState == "Published"))
        return;
      ISchedulingStatus schedulingStatus = statusInfo != null ? statusInfo.Data as ISchedulingStatus : (ISchedulingStatus) null;
      if (schedulingStatus == null || !schedulingStatus.ExpirationDate.HasValue)
        return;
      string source = workflowStateMessage.Split(new char[1]
      {
        ','
      }, StringSplitOptions.RemoveEmptyEntries)[1].Trim();
      string str = source.First<char>().ToString().ToUpper() + source.Substring(1);
      DisplayStatus displayStatus1 = new DisplayStatus()
      {
        Name = "Scheduled",
        Source = StatusSource.Scheduling.ToString(),
        Label = str,
        PublicationDate = schedulingStatus.PublicationDate,
        ExpirationDate = schedulingStatus.ExpirationDate
      };
      DisplayStatus displayStatus2 = displayStatus1;
      Message message1 = new Message();
      message1.Description = "Scheduled unpublish ${ExpirationDate}";
      message1.Operations = new ItemOperation[1]
      {
        new ItemOperation()
        {
          Title = Res.Get<WorkflowResources>().StopSchedulingAction,
          Name = "StopScheduleUnpublish",
          Category = new OperationCategory("Workflow"),
          ExecuteOnServer = true
        }
      };
      Message message2 = message1;
      displayStatus2.Message = message2;
      itemStatuses.Add(displayStatus1);
    }

    protected static string GetStatusLabel(string status)
    {
      switch (status)
      {
        case "AwaitingApproval":
          return Res.Get<WorkflowResources>().WaitingForApproval;
        case "AwaitingPublishing":
          return Res.Get<ApprovalWorkflowResources>().AwaitingPublishing;
        case "AwaitingReview":
          return Res.Get<ApprovalWorkflowResources>().AwaitingReview;
        case "Draft":
          return "Draft";
        case "Published":
          return "Published";
        case "Rejected":
          return "Rejected";
        case "RejectedForApproval":
          return "Rejected approval";
        case "RejectedForPublishing":
          return "Rejected publishing";
        case "RejectedForReview":
          return "Rejected";
        case "Scheduled":
          return "Scheduled";
        case "Unpublished":
          return "Unpublished";
        default:
          return (string) null;
      }
    }

    protected static string GetDetailedStatusLabel(string status)
    {
      switch (status)
      {
        case "AwaitingApproval":
          return "Sent for approval";
        case "AwaitingPublishing":
          return "Sent for publishing";
        case "AwaitingReview":
          return "Sent for review";
        case "Draft":
          return "Saved as draft";
        case "Published":
          return "Published";
        case "Rejected":
          return "Rejected";
        case "RejectedForApproval":
          return "Rejected approval";
        case "RejectedForPublishing":
          return "Rejected publishing";
        case "RejectedForReview":
          return "Rejected";
        case "Scheduled":
          return "Scheduled";
        case "Unpublished":
          return "Unpublished";
        default:
          return (string) null;
      }
    }
  }
}
