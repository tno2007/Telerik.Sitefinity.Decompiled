// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Extensibility.DisplayPageStatusProperty
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
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages.Web.Services.Model;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Workflow.Model.Tracking;

namespace Telerik.Sitefinity.Web.Services.Extensibility
{
  /// <summary>A property for retrieving page display status</summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  internal class DisplayPageStatusProperty : DisplayStatusProperty
  {
    internal const string DisplayStatusGroupName = "Group page";
    internal const string DisplayStatusRedirectName = "Redirecting page";
    internal const string DisplayStatusSourcePages = "Pages";

    /// <inheritdoc />
    public override Type ReturnType => typeof (IEnumerable<DisplayStatus>);

    /// <inheritdoc />
    public override IDictionary<object, object> GetValues(
      IEnumerable items,
      IManager manager)
    {
      Dictionary<object, object> values = new Dictionary<object, object>();
      CultureInfo culture = SystemManager.CurrentContext.Culture;
      ILookup<Guid, ApprovalTrackingRecord> lookup = IApprovalWorkflowExtensions.GetApprovalRecords((IList<IWorkflowItem>) items.Cast<object>().Select<object, PageSiteNode>((Func<object, PageSiteNode>) (x => PropertyHelpers.GetSiteMapNode(x))).Select<PageSiteNode, WorkflowItemWrapper>((Func<PageSiteNode, WorkflowItemWrapper>) (y => new WorkflowItemWrapper(y))).ToList<WorkflowItemWrapper>().Cast<IWorkflowItem>().ToList<IWorkflowItem>(), (IOpenAccessDataProvider) manager.Provider).ToLookup<ApprovalTrackingRecord, Guid, ApprovalTrackingRecord>((Func<ApprovalTrackingRecord, Guid>) (x => x.WorkflowItemId), (Func<ApprovalTrackingRecord, ApprovalTrackingRecord>) (y => y));
      foreach (object key in items)
      {
        PageSiteNode siteMapNode = PropertyHelpers.GetSiteMapNode(key);
        List<DisplayStatus> statuses = new List<DisplayStatus>();
        if (siteMapNode.NodeType == NodeType.Standard || siteMapNode.NodeType == NodeType.External)
        {
          List<ApprovalTrackingRecord> list = lookup[siteMapNode.Id].ToList<ApprovalTrackingRecord>();
          IList<DisplayStatus> collection = this.ResolveStatuses(siteMapNode, culture, (IList<ApprovalTrackingRecord>) list);
          statuses.AddRange((IEnumerable<DisplayStatus>) collection);
          Status status = StatusResolver.Resolve(typeof (PageNode), manager.Provider.Name, siteMapNode.Id);
          if (status != null)
            statuses.Add(new DisplayStatus()
            {
              Name = status.Text,
              Source = status.PrimaryProvider,
              Label = status.Text,
              DetailedLabel = status.Text
            });
          this.PopulateStatusDetails(siteMapNode, (IList<DisplayStatus>) statuses, (IList<ApprovalTrackingRecord>) list);
        }
        else if (siteMapNode.NodeType == NodeType.Group)
          statuses.Add(new DisplayStatus()
          {
            Name = "Group page",
            Source = "Pages",
            Label = "Group page",
            DetailedLabel = "Group page"
          });
        else if (siteMapNode.NodeType == NodeType.InnerRedirect || siteMapNode.NodeType == NodeType.OuterRedirect || siteMapNode.NodeType == NodeType.Rewriting)
          statuses.Add(new DisplayStatus()
          {
            Name = "Redirecting page",
            Source = "Pages",
            Label = "Redirecting page",
            DetailedLabel = "Redirecting page"
          });
        values.Add(key, (object) statuses);
      }
      return (IDictionary<object, object>) values;
    }

    [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Reviewed")]
    private IList<DisplayStatus> ResolveStatuses(
      PageSiteNode siteNode,
      CultureInfo culture,
      IList<ApprovalTrackingRecord> approvalRecords)
    {
      List<DisplayStatus> itemStatuses = new List<DisplayStatus>();
      string statusKey;
      IStatusInfo statusInfo;
      string statusMessage = siteNode.CurrentPageDataItem.GetLocalizedStatus(out statusKey, out statusInfo, culture);
      if (statusKey.IsNullOrEmpty())
        statusKey = new PageViewModel(siteNode).Status;
      if (statusKey == "Published" || statusKey == "Draft" || statusKey == "Unpublished")
      {
        DisplayStatus displayStatus = new DisplayStatus()
        {
          Name = statusKey,
          Source = StatusSource.Lifecycle.ToString(),
          Label = this.SetNewerThanPublishedStatus(siteNode, DisplayStatusProperty.GetStatusLabel(statusKey), culture),
          DetailedLabel = this.SetNewerThanPublishedStatus(siteNode, DisplayStatusProperty.GetDetailedStatusLabel(statusKey), culture)
        };
        itemStatuses.Add(displayStatus);
        this.ResolveScheduledUnpublishStatus(statusKey, statusInfo, statusMessage, itemStatuses);
      }
      else if (statusKey == "Scheduled")
      {
        string label = this.SetNewerThanPublishedStatus(siteNode, statusMessage, culture);
        this.ResolveScheduledStatus(statusKey, statusInfo, statusMessage, itemStatuses, label);
      }
      else
      {
        siteNode.CurrentPageDataItem.GetOverallStatus(out statusMessage);
        if (statusKey == "PageBrokenMessage")
        {
          DisplayStatus displayStatus = new DisplayStatus()
          {
            Name = statusKey,
            Source = StatusSource.Lifecycle.ToString(),
            Label = statusMessage,
            DetailedLabel = statusMessage
          };
          itemStatuses.Add(displayStatus);
        }
        else
          this.ResolveWorkflowStatus(statusKey, itemStatuses, approvalRecords);
      }
      return (IList<DisplayStatus>) itemStatuses;
    }

    private string SetNewerThanPublishedStatus(
      PageSiteNode item,
      string status,
      CultureInfo culture)
    {
      PageDataProxy currentPageDataItem = item.CurrentPageDataItem;
      string empty = string.Empty;
      return currentPageDataItem.IsPublished(culture) && currentPageDataItem.HasDraftNewerThanPublished(culture) ? string.Format("{0}, {1}", (object) status, (object) Res.Get<Labels>().NewerThanPublished) : status;
    }

    private void PopulateStatusDetails(
      PageSiteNode item,
      IList<DisplayStatus> statuses,
      IList<ApprovalTrackingRecord> records)
    {
      PageDataProxy currentPageDataItem = item.CurrentPageDataItem;
      IOrderedEnumerable<ApprovalTrackingRecord> source = records.OrderByDescending<ApprovalTrackingRecord, DateTime>((Func<ApprovalTrackingRecord, DateTime>) (x => x.DateCreated));
      foreach (DisplayStatus statuse in (IEnumerable<DisplayStatus>) statuses)
      {
        string statusName = statuse.Name.StartsWith("Draft") ? "Draft" : statuse.Name;
        ApprovalTrackingRecord approvalTrackingRecord = source.FirstOrDefault<ApprovalTrackingRecord>((Func<ApprovalTrackingRecord, bool>) (x => x.Status == statusName));
        if (approvalTrackingRecord != null)
        {
          statuse.Date = approvalTrackingRecord.DateCreated;
          statuse.User = UserProfilesHelper.GetUserDisplayName(approvalTrackingRecord.UserId);
        }
        else if (statusName == "Draft")
        {
          statuse.Date = currentPageDataItem.LastModified;
          statuse.User = UserProfilesHelper.GetUserDisplayName(currentPageDataItem.LastModifiedBy);
        }
        else if (records.Count == 0)
        {
          ItemEventInfo fallBackInfo = DisplayPageStatusProperty.GetFallBackInfo(currentPageDataItem, statuse.Name);
          if (fallBackInfo != null)
          {
            statuse.Date = fallBackInfo.Date;
            statuse.User = fallBackInfo.User;
          }
        }
      }
    }

    internal static ItemEventInfo GetFallBackInfo(
      PageDataProxy pageData,
      string workflowState)
    {
      ItemEventInfo fallBackInfo = (ItemEventInfo) null;
      if (workflowState == "Published")
      {
        if (pageData.Status != ContentLifecycleStatus.Live || !pageData.Visible)
          return (ItemEventInfo) null;
        fallBackInfo = new ItemEventInfo()
        {
          Date = pageData.LastModified,
          User = UserProfilesHelper.GetUserDisplayName(pageData.LastModifiedBy)
        };
      }
      else if (workflowState != null && pageData.GetMaster() is PageDataProxy master)
        fallBackInfo = new ItemEventInfo()
        {
          Date = master.LastModified,
          User = UserProfilesHelper.GetUserDisplayName(master.LastModifiedBy)
        };
      return fallBackInfo;
    }
  }
}
