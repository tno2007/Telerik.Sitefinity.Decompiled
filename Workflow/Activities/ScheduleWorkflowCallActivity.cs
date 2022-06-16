// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.Activities.ScheduleWorkflowCallActivity
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
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.Localization;
using Telerik.Sitefinity.Scheduling;
using Telerik.Sitefinity.Scheduling.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Events;
using Telerik.Sitefinity.Workflow.Activities.Designers;

namespace Telerik.Sitefinity.Workflow.Activities
{
  /// <summary>Activity used to publish/unpublish operation</summary>
  [Designer(typeof (ScheduleWorkflowCallDesigner))]
  public class ScheduleWorkflowCallActivity : CodeActivity
  {
    /// <summary>Gets or sets the name of the operation.</summary>
    /// <value>The name of the operation.</value>
    public string OperationName { get; set; }

    /// <summary>Gets or sets the date to execute on.</summary>
    /// <value>The execute on.</value>
    public InArgument<object> ExecuteOn { get; set; }

    /// <summary>
    /// When implemented in a derived class, performs the execution of the activity.
    /// </summary>
    /// <param name="context">The execution context under which the activity executes.</param>
    protected override void Execute(CodeActivityContext context)
    {
      string[] strArray;
      if (!this.OperationName.IsNullOrEmpty())
        strArray = new string[1]
        {
          this.OperationName == "ScheduledPublish" ? "Publish" : this.OperationName
        };
      else
        strArray = new string[1]{ "Publush, Unpublish" };
      WorkflowDataContext dataContext = context.DataContext;
      Dictionary<string, string> contextBag = dataContext.GetProperties()["contextBag"].GetValue((object) dataContext) as Dictionary<string, string>;
      string str1 = (string) dataContext.GetProperties()["itemStatus"].GetValue((object) dataContext);
      foreach (string str2 in strArray)
      {
        DateTime? date = new DateTime?();
        if (contextBag != null)
        {
          string itemName = str2.Equals("Publish") ? "PublicationDate" : (str2.Equals("Unpublish") ? "ExpirationDate" : (string) null);
          if (itemName != null)
            ScheduleWorkflowCallActivity.TryGetScheduledDate((IDictionary<string, string>) contextBag, itemName, out date);
        }
        if (!date.HasValue)
        {
          object obj = this.ExecuteOn.Get((ActivityContext) context);
          if (obj != null)
          {
            DateTime? nullable = obj as DateTime?;
            if (nullable.HasValue)
              dateTime = nullable.GetValueOrDefault();
            else if (!(obj is DateTime dateTime))
              throw new ApplicationException("Invalid ExecuteOn parameter type for the ScheduleWorkflowCallActivity. Should be DateTime or DateTime?.");
            date = new DateTime?(DateTime.SpecifyKind(dateTime, DateTimeKind.Utc));
          }
        }
        IWorkflowItem workflowItem = (IWorkflowItem) dataContext.GetProperties()["workflowItem"].GetValue((object) dataContext);
        IHasTitle hasTitle = workflowItem as IHasTitle;
        Guid itemId = (Guid) dataContext.GetProperties()["itemId"].GetValue((object) dataContext);
        string itemProviderName = (string) dataContext.GetProperties()["providerName"].GetValue((object) dataContext);
        if (itemProviderName.IsNullOrEmpty())
          itemProviderName = workflowItem.GetWorkflowItemProviderName();
        SchedulingManager schedulingManager = ScheduleWorkflowCallActivity.GetSchedulingManager();
        WorkflowCallTask task1 = new WorkflowCallTask();
        task1.ContentItemMasterId = itemId;
        task1.ContentProviderName = itemProviderName;
        task1.ContentType = workflowItem.GetType();
        task1.OperationName = str2;
        task1.Title = hasTitle != null ? hasTitle.GetTitle() : string.Empty;
        task1.SiteId = SystemManager.CurrentContext.CurrentSite.Id;
        task1.UserId = SecurityManager.CurrentUserId.ToString();
        CultureInfo culture = SystemManager.CurrentContext.Culture;
        task1.Language = culture.GetLanguageKey();
        string taskKey = task1.Key;
        IQueryable<ScheduledTaskData> taskData = schedulingManager.GetTaskData();
        Expression<Func<ScheduledTaskData, bool>> predicate = (Expression<Func<ScheduledTaskData, bool>>) (task => task.Key == taskKey);
        foreach (ScheduledTaskData task2 in (IEnumerable<ScheduledTaskData>) taskData.Where<ScheduledTaskData>(predicate))
          schedulingManager.DeleteTaskData(task2);
        if (date.HasValue)
        {
          task1.ExecuteTime = date.Value;
          if (str2 == "Publish" && date.Value <= DateTime.UtcNow)
            task1.ExecuteTask();
          else
            schedulingManager.AddTask((ScheduledTask) task1);
        }
        DataEvent dataEvent = (DataEvent) null;
        if (str2 == "Publish")
          dataEvent = ScheduleWorkflowCallActivity.GetDummySchedulingDataEvent(itemId, workflowItem.GetType(), itemProviderName, culture, "Scheduled", "Draft");
        if (schedulingManager.TransactionName.IsNullOrEmpty())
        {
          schedulingManager.SaveChanges();
          if (dataEvent != null)
            EventHub.Raise((IEvent) dataEvent);
        }
        else if (dataEvent != null)
          TransactionManager.TryAddPostCommitAction(schedulingManager.TransactionName, (Action) (() =>
          {
            EventHub.Raise((IEvent) dataEvent);
            Scheduler.Instance.RescheduleNextRun();
          }));
      }
    }

    internal static bool TryGetScheduledDate(
      IDictionary<string, string> contextBag,
      string itemName,
      out DateTime? date)
    {
      string s;
      DateTime result;
      if (contextBag.TryGetValue(itemName, out s) && DateTime.TryParse(s, (IFormatProvider) SystemManager.CurrentContext.Culture, DateTimeStyles.AdjustToUniversal, out result))
      {
        date = new DateTime?(DateTime.SpecifyKind(result, DateTimeKind.Utc));
        return true;
      }
      date = new DateTime?();
      return false;
    }

    internal static SchedulingManager GetSchedulingManager() => !SystemManager.CurrentContext.GlobalTransaction.IsNullOrEmpty() ? SchedulingManager.GetManager((string) null, SystemManager.CurrentContext.GlobalTransaction) : SchedulingManager.GetManager();

    internal static DataEvent GetDummySchedulingDataEvent(
      Guid itemId,
      Type itemtype,
      string itemProvider,
      CultureInfo culture,
      string newStatus,
      string oldStatus)
    {
      if (!((!SystemManager.CurrentContext.GlobalTransaction.IsNullOrEmpty() ? ManagerBase.GetMappedManagerInTransaction(itemtype, itemProvider, SystemManager.CurrentContext.GlobalTransaction) : ManagerBase.GetMappedManager(itemtype, itemProvider)).GetItem(itemtype, itemId) is IWorkflowItem workflowItem))
        return (DataEvent) null;
      if (!(workflowItem is IDataItem dataItem))
        return (DataEvent) null;
      DataEvent eventInternal = DataEventFactory.CreateEventInternal(dataItem, SecurityConstants.TransactionActionType.Updated.ToString(), culture);
      if (dataItem is IHasTitle hasTitle)
        eventInternal.Title = hasTitle.GetTitle(culture);
      eventInternal.ChangedProperties.Add("ApprovalWorkflowState", new PropertyChange()
      {
        PropertyName = "ApprovalWorkflowState",
        NewValue = (object) newStatus,
        OldValue = (object) oldStatus
      });
      eventInternal.ApprovalWorkflowState = newStatus;
      return eventInternal;
    }
  }
}
