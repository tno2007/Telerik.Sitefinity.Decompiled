// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.UsageTracking.Scheduling.UsageTrackingTask
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Scheduling;
using Telerik.Sitefinity.Scheduling.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.UsageTracking.Configuration;
using Telerik.Sitefinity.UsageTracking.DataSending;
using Telerik.Sitefinity.UsageTracking.HttpClients;

namespace Telerik.Sitefinity.UsageTracking.Scheduling
{
  internal class UsageTrackingTask : ScheduledTask
  {
    private const string DefaultCronExpression = "0 0 * * 0";

    static UsageTrackingTask() => CacheDependency.Subscribe(typeof (SystemConfig), new ChangedCallback(UsageTrackingTask.ConfigUpdated));

    public override void ExecuteTask()
    {
      using (DataSender dataSender = new DataSender())
        dataSender.SendReport();
    }

    public override string TaskName => UsageTrackingTask.GetTaskName();

    internal static ScheduledTask NewInstance()
    {
      UsageTrackingElement usageTracking = Config.Get<SystemConfig>().UsageTracking;
      if (!usageTracking.EnableUsageTracking)
        return (ScheduledTask) null;
      string str = usageTracking.AutoSyncCronSpec;
      if (string.IsNullOrEmpty(str))
      {
        try
        {
          using (TrackingClient trackingClient = new TrackingClient())
            str = trackingClient.GetTrackingConfiguration();
        }
        catch (Exception ex)
        {
          Log.Trace("Usage tracking configuration could not be retrieved due to the following exception {0}.", (object) ex.Message);
          str = "0 0 * * 0";
        }
      }
      UsageTrackingTask usageTrackingTask = new UsageTrackingTask();
      usageTrackingTask.Id = Guid.NewGuid();
      usageTrackingTask.ScheduleSpecType = "crontab";
      usageTrackingTask.ScheduleSpec = str;
      usageTrackingTask.IsSystem = true;
      return (ScheduledTask) usageTrackingTask;
    }

    internal static void Schedule()
    {
      UsageTrackingTask.RemoveScheduledTasks();
      ScheduledTask task = UsageTrackingTask.NewInstance();
      if (task == null)
        return;
      Scheduler.Instance.TryToScheduleNextTaskRun(task);
    }

    internal static void RemoveScheduledTasks()
    {
      SchedulingManager manager = SchedulingManager.GetManager();
      ParameterExpression parameterExpression;
      // ISSUE: method reference
      foreach (ScheduledTaskData tasksFromAllProvider in (IEnumerable<ScheduledTaskData>) SchedulingManager.GetTasksFromAllProviders(Expression.Lambda<Func<ScheduledTaskData, bool>>((Expression) Expression.Equal(t.TaskName, (Expression) Expression.Call((Expression) null, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (UsageTrackingTask.GetTaskName)), Array.Empty<Expression>())), parameterExpression)))
        manager.DeleteItem((object) tasksFromAllProvider);
      manager.SaveChanges();
    }

    internal static string GetTaskName() => typeof (UsageTrackingTask).FullName;

    private static void ConfigUpdated(
      ICacheDependencyHandler caller,
      Type trackedItemType,
      string trackedItemKey)
    {
      UsageTrackingTask.Schedule();
    }
  }
}
