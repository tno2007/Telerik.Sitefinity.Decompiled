// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.Activities.SetApprovalStatusActivity
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Fluent;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Workflow.Activities.Designers;
using Telerik.Sitefinity.Workflow.Model.Tracking;

namespace Telerik.Sitefinity.Workflow.Activities
{
  /// <summary>
  /// Changes the approval status of the currently processed worfklow item
  /// </summary>
  [Designer(typeof (SetApprovalStatusActivityDesigner))]
  public class SetApprovalStatusActivity : CodeActivity
  {
    public SetApprovalStatusActivity() => this.SynchronizeMultiligualStatuses = false;

    public string ResultStatus { get; set; }

    /// <summary>
    /// Specifies whether to syncronize the statuses for all the translations in multilingual mode
    /// </summary>
    /// <value>The synchronize multiligual statuses.</value>
    [DefaultValue(false)]
    [Description("Specifies whether to syncronize the statuses for all the translations in multilingual mode")]
    public bool SynchronizeMultiligualStatuses { get; set; }

    /// <summary>
    /// When implemented in a derived class, performs the execution of the activity.
    /// </summary>
    /// <param name="context">The execution context under which the activity executes.</param>
    protected override void Execute(CodeActivityContext context)
    {
      WorkflowDataContext dataContext = context.DataContext;
      string str1 = (string) dataContext.GetProperties()["operationName"].GetValue((object) dataContext);
      object obj1 = dataContext.GetProperties()["workflowItem"].GetValue((object) dataContext);
      IWorkflowItem workflowItem = (IWorkflowItem) obj1;
      IDataItem dataItem = obj1 as IDataItem;
      DataProviderBase provider = (DataProviderBase) null;
      if (dataItem != null)
        provider = dataItem.Provider as DataProviderBase;
      string applicationName = dataContext.GetProperties()["applicationName"].GetValue((object) dataContext) as string;
      Dictionary<string, string> dictionary = dataContext.GetProperties()["contextBag"].GetValue((object) dataContext) as Dictionary<string, string>;
      string str2 = dictionary.ContainsKey("Note") ? dictionary["Note"] : "";
      if (workflowItem == null)
        return;
      string newStatus = string.Empty;
      if (!this.ResultStatus.IsNullOrWhitespace())
      {
        if (this.ResultStatus.StartsWith("#PreviousStatus"))
        {
          string currentStatus = workflowItem.GetCurrentApprovalTrackingRecord().Status;
          string[] source = this.ResultStatus.Split(',');
          string str3 = currentStatus;
          if (((IEnumerable<string>) source).Count<string>() > 1)
            str3 = source[1];
          ApprovalTrackingRecord approvalTrackingRecord = workflowItem.GetApprovalRecords().Where<ApprovalTrackingRecord>((Expression<Func<ApprovalTrackingRecord, bool>>) (item => item.Status != currentStatus)).OrderByDescending<ApprovalTrackingRecord, DateTime>((Expression<Func<ApprovalTrackingRecord, DateTime>>) (item => item.LastModified)).FirstOrDefault<ApprovalTrackingRecord>();
          newStatus = approvalTrackingRecord == null ? str3 : approvalTrackingRecord.Status;
        }
        else
        {
          if (this.ResultStatus == "Draft" && str1 == "StopSchedule" && workflowItem is IApprovalWorkflowItem approvalWorkflowItem1)
            newStatus = (string) approvalWorkflowItem1.ApprovalWorkflowState;
          if (newStatus.IsNullOrEmpty())
            newStatus = this.ResultStatus;
        }
      }
      if (newStatus.IsNullOrWhitespace())
        return;
      IApprovalWorkflowItem approvalWorkflowItem = workflowItem as IApprovalWorkflowItem;
      if (approvalWorkflowItem != null)
      {
        ApprovalTrackingRecord approvalTrackingRecord = approvalWorkflowItem.CreateApprovalTrackingRecord(applicationName, newStatus, provider.GetNewGuid());
        approvalTrackingRecord.Note = str2;
        dictionary["StateChangerId"] = approvalTrackingRecord.UserId.ToString();
        bool flag1 = SystemManager.CurrentContext.AppSettings.Multilingual && this.SynchronizeMultiligualStatuses && workflowItem is ILocalizable;
        IEnumerable<CultureInfo> culturesToSync = (IEnumerable<CultureInfo>) null;
        bool flag2 = obj1.GetType().MustSyncWorkflowStatus();
        if (flag1 & flag2)
          culturesToSync = workflowItem.SyncMultilingualRecords(approvalTrackingRecord, provider, culturesToSync);
        if (str1 != "Schedule" && str1 != "StopSchedule")
        {
          bool isDiffrent = true;
          CommonMethods.ExecuteMlLogic<object>((Action<object>) (obj => isDiffrent = approvalWorkflowItem.ApprovalWorkflowState != (Lstring) newStatus), (Action<object>) (obj => isDiffrent = approvalWorkflowItem.ApprovalWorkflowState.GetStringNoFallback(SystemManager.CurrentContext.Culture) != newStatus), (Action<object>) (obj => isDiffrent = approvalWorkflowItem.ApprovalWorkflowState.GetString(SystemManager.CurrentContext.Culture, false) != newStatus));
          if (isDiffrent)
            approvalWorkflowItem.ApprovalWorkflowState = (Lstring) newStatus;
          if (flag1 && culturesToSync != null)
          {
            foreach (CultureInfo culture in culturesToSync)
              approvalWorkflowItem.ApprovalWorkflowState.SetString(culture, newStatus);
          }
        }
      }
      dataContext.GetProperties()["workflowItem"].SetValue((object) dataContext, (object) workflowItem);
    }

    /// <summary>
    /// Adds clones of the specified tracking record for all cultures
    /// </summary>
    /// <param name="recordMap">The record map.</param>
    /// <param name="recordToClone">Tracking record to be cloned</param>
    protected void AddMultilingualSynchronizedApprovalRecrords(
      ApprovalTrackingRecordMap recordMap,
      ApprovalTrackingRecord recordToClone,
      IEnumerable<CultureInfo> culturesToSync,
      DataProviderBase provider = null)
    {
      foreach (CultureInfo culture in culturesToSync)
      {
        ApprovalTrackingRecord approvalTrackingRecord = new ApprovalTrackingRecord()
        {
          ApplicationName = recordToClone.ApplicationName,
          Culture = Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.GetCultureLcid(culture),
          DateCreated = DateTime.Now,
          Id = provider != null ? provider.GetNewGuid() : Guid.NewGuid(),
          LastModified = DateTime.Now,
          Note = recordToClone.Note,
          Status = recordToClone.Status,
          UserId = SecurityManager.CurrentUserId,
          WorkflowItemId = recordToClone.WorkflowItemId
        };
        recordMap.ApprovalRecords.Add(approvalTrackingRecord);
      }
    }

    /// <summary>
    /// Gets or sets the name of the action fro this activity, in order not to rely on Title when scanning the workflow for a specific one.
    /// </summary>
    public string ActionName { get; set; }
  }
}
