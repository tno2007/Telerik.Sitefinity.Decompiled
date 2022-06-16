// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Extensibility.WorkflowPageStatusProperty
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
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Fluent;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Workflow.Model.Tracking;

namespace Telerik.Sitefinity.Web.Services.Extensibility
{
  /// <summary>A workflow page status property.</summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  internal class WorkflowPageStatusProperty : WorkflowStatusProperty
  {
    /// <inheritdoc />
    public override IDictionary<object, object> GetValues(
      IEnumerable items,
      IManager manager)
    {
      CultureInfo culture = SystemManager.CurrentContext.Culture;
      Dictionary<object, object> values = new Dictionary<object, object>();
      IQueryable<ApprovalTrackingRecord> approvalRecords = IApprovalWorkflowExtensions.GetApprovalRecords((IList<IWorkflowItem>) items.Cast<IWorkflowItem>().ToList<IWorkflowItem>(), manager.Provider as IOpenAccessDataProvider).Where<ApprovalTrackingRecord>((Expression<Func<ApprovalTrackingRecord, bool>>) (x => x.Status == this.WorkflowState));
      CommonMethods.ExecuteMlLogic<object>((Action<object>) (item =>
      {
        int cultureLcid = Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.GetCultureLcid(CultureInfo.InvariantCulture);
        approvalRecords = approvalRecords.Where<ApprovalTrackingRecord>((Expression<Func<ApprovalTrackingRecord, bool>>) (x => x.Culture == cultureLcid));
      }), (Action<object>) (item =>
      {
        int cultureLcid = Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.GetCultureLcid(SystemManager.CurrentContext.Culture);
        int cultureLcidInv = Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.GetCultureLcid(CultureInfo.InvariantCulture);
        approvalRecords = approvalRecords.Where<ApprovalTrackingRecord>((Expression<Func<ApprovalTrackingRecord, bool>>) (x => x.Culture == cultureLcid || x.Culture == cultureLcidInv));
      }), (Action<object>) (item =>
      {
        int cultureLcid = Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.GetCultureLcid(SystemManager.CurrentContext.Culture);
        approvalRecords = approvalRecords.Where<ApprovalTrackingRecord>((Expression<Func<ApprovalTrackingRecord, bool>>) (x => x.Culture == cultureLcid));
      }));
      if (this.OrderAscending)
        approvalRecords = (IQueryable<ApprovalTrackingRecord>) approvalRecords.OrderBy<ApprovalTrackingRecord, DateTime>((Expression<Func<ApprovalTrackingRecord, DateTime>>) (x => x.DateCreated));
      else
        approvalRecords = (IQueryable<ApprovalTrackingRecord>) approvalRecords.OrderByDescending<ApprovalTrackingRecord, DateTime>((Expression<Func<ApprovalTrackingRecord, DateTime>>) (x => x.DateCreated));
      ILookup<Guid, ApprovalTrackingRecord> lookup = approvalRecords.ToLookup<ApprovalTrackingRecord, Guid, ApprovalTrackingRecord>((Func<ApprovalTrackingRecord, Guid>) (x => x.WorkflowItemId), (Func<ApprovalTrackingRecord, ApprovalTrackingRecord>) (y => y));
      foreach (PageNode pageNode in items.Cast<PageNode>().ToList<PageNode>())
      {
        ItemEventInfo itemEventInfo1 = (ItemEventInfo) null;
        List<CultureInfo> list = ((IEnumerable<CultureInfo>) pageNode.AvailableCultures).Where<CultureInfo>((Func<CultureInfo, bool>) (x => x != CultureInfo.InvariantCulture)).ToList<CultureInfo>();
        if (list.Count > 0 && !list.Contains(culture))
          values.Add((object) pageNode, (object) itemEventInfo1);
        else if (pageNode.NodeType == NodeType.Group || pageNode.NodeType == NodeType.InnerRedirect || pageNode.NodeType == NodeType.OuterRedirect)
        {
          ItemEventInfo itemEventInfo2 = new ItemEventInfo()
          {
            Date = pageNode.DateCreated,
            User = pageNode.GetUserDisplayName()
          };
          values.Add((object) pageNode, (object) itemEventInfo2);
        }
        else
        {
          Guid id = pageNode.Id;
          ApprovalTrackingRecord approvalTrackingRecord = lookup[id].FirstOrDefault<ApprovalTrackingRecord>();
          if (approvalTrackingRecord != null)
          {
            itemEventInfo1 = new ItemEventInfo()
            {
              Date = approvalTrackingRecord.DateCreated,
              User = UserProfilesHelper.GetUserDisplayName(approvalTrackingRecord.UserId)
            };
          }
          else
          {
            PageData pageData = pageNode.GetPageData(culture);
            PageManager manager1 = manager as PageManager;
            if (pageData != null && manager != null)
              itemEventInfo1 = DisplayPageStatusProperty.GetFallBackInfo(new PageDataProxy(pageData, manager1), this.WorkflowState);
          }
          values.Add((object) pageNode, (object) itemEventInfo1);
        }
      }
      return (IDictionary<object, object>) values;
    }
  }
}
