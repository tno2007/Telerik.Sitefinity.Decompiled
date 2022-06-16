// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.Activities.ClearScheduledOperationsActivity
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
using Telerik.Sitefinity.Data.Events;
using Telerik.Sitefinity.Model.Localization;
using Telerik.Sitefinity.Scheduling;
using Telerik.Sitefinity.Scheduling.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Events;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Workflow.Activities.Designers;

namespace Telerik.Sitefinity.Workflow.Activities
{
  [Designer(typeof (ClearScheduledOperationsActivityDesigner))]
  public class ClearScheduledOperationsActivity : CodeActivity
  {
    internal const string ExecutionSourceKey = "ExecutionSource";

    /// <summary>
    /// This is a filtering condition for the activity.
    /// The activity will be executed only when the workflow is ran with an operation matching this condition
    /// The condtion can be left unspecified(empty) than it will not be considered
    ///  </summary>
    /// <value>One or more comma delimited workflow operation names</value>
    public string OperationsCondition { get; set; }

    /// <summary>
    /// Gets or sets the scheduled operations to clear.
    /// This is a required property of the actvitiy
    /// </summary>
    /// <value>One or more comma delimited workflow operation names.</value>
    public string OperationsToClear { get; set; }

    /// <summary>
    /// This is a filtering condition for the activity.
    /// The activity will be executed only when the content item approval status when starting the workflow matches the condition
    /// The condtion can be left unspecified(empty) than it will not be considered
    ///  </summary>
    /// <value>One or more comma delimited workflow status names</value>
    public string StatusCondition { get; set; }

    /// <summary>
    /// When implemented in a derived class, performs the execution of the activity.
    /// </summary>
    /// <param name="context">The execution context under which the activity executes.</param>
    protected override void Execute(CodeActivityContext context)
    {
      WorkflowDataContext dataContext = context.DataContext;
      Dictionary<string, string> dictionary = dataContext.GetProperties()["contextBag"].GetValue((object) dataContext) as Dictionary<string, string>;
      string str1;
      if (dictionary.TryGetValue("ExecutionSource", out str1) && str1 == "WorkflowCallTask")
        return;
      string itemStatus = (string) dataContext.GetProperties()["itemStatus"].GetValue((object) dataContext);
      if (!string.IsNullOrEmpty(this.StatusCondition))
      {
        if (!((IEnumerable<string>) this.StatusCondition.Split(new char[1]
        {
          ','
        }, StringSplitOptions.RemoveEmptyEntries)).Any<string>((Func<string, bool>) (item => item == itemStatus)))
          return;
      }
      if (itemStatus == "Unpublished")
        return;
      string currentOperation = (string) dataContext.GetProperties()["operationName"].GetValue((object) dataContext);
      if (!((IEnumerable<string>) this.OperationsCondition.Split(new char[1]
      {
        ','
      }, StringSplitOptions.RemoveEmptyEntries)).Any<string>((Func<string, bool>) (item => item.Trim() == currentOperation)))
        return;
      Type itemtype = TypeResolutionService.ResolveType(dictionary["ContentType"]);
      Guid itemId = (Guid) dataContext.GetProperties()["itemId"].GetValue((object) dataContext);
      string itemProvider = (string) dataContext.GetProperties()["providerName"].GetValue((object) dataContext);
      CultureInfo culture = (CultureInfo) null;
      WorkflowCallTask workflowCallTask = new WorkflowCallTask();
      workflowCallTask.ContentType = itemtype;
      workflowCallTask.ContentProviderName = itemProvider;
      workflowCallTask.Language = culture.GetLanguageKey();
      List<string> keysToDelete = new List<string>();
      string[] strArray;
      if (!(currentOperation == "Publish") && !(currentOperation == "Unpublish"))
        strArray = this.OperationsToClear.Split(new char[1]
        {
          ','
        }, StringSplitOptions.RemoveEmptyEntries);
      else
        strArray = new string[1]{ currentOperation };
      foreach (string str2 in strArray)
      {
        workflowCallTask.ContentItemMasterId = itemId;
        workflowCallTask.OperationName = str2 == "ScheduledPublish" ? "Publish" : str2;
        keysToDelete.Add(workflowCallTask.BuildUniqueKey());
      }
      if (!keysToDelete.Any<string>())
        return;
      bool flag = false;
      SchedulingManager schedulingManager = ScheduleWorkflowCallActivity.GetSchedulingManager();
      IQueryable<ScheduledTaskData> taskData = schedulingManager.GetTaskData();
      Expression<Func<ScheduledTaskData, bool>> predicate = (Expression<Func<ScheduledTaskData, bool>>) (item => keysToDelete.Contains(item.Key));
      foreach (ScheduledTaskData scheduledTaskData in (IEnumerable<ScheduledTaskData>) taskData.Where<ScheduledTaskData>(predicate))
      {
        schedulingManager.DeleteItem((object) scheduledTaskData);
        flag = true;
      }
      if (!flag)
        return;
      DataEvent dataEvent = (DataEvent) null;
      if (currentOperation == "StopSchedule")
        dataEvent = ScheduleWorkflowCallActivity.GetDummySchedulingDataEvent(itemId, itemtype, itemProvider, culture ?? SystemManager.CurrentContext.Culture, "Draft", "Scheduled");
      if (schedulingManager.TransactionName.IsNullOrEmpty())
      {
        schedulingManager.SaveChanges();
        if (dataEvent == null)
          return;
        EventHub.Raise((IEvent) dataEvent);
      }
      else
      {
        if (dataEvent == null)
          return;
        TransactionManager.TryAddPostCommitAction(schedulingManager.TransactionName, (Action) (() => EventHub.Raise((IEvent) dataEvent)));
      }
    }
  }
}
