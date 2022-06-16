// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Scheduling.Web.Services.SchedulingService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Threading;
using Telerik.Sitefinity.Scheduling.Model;

namespace Telerik.Sitefinity.Scheduling.Web.Services
{
  /// <summary>The WCF service used for managing background tasks.</summary>
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  public class SchedulingService : ISchedulingService
  {
    /// <inheritdoc />
    public WcfScheduledTask GetTaskProgress(string taskId, string providerName)
    {
      Guid parsedId = Guid.Parse(taskId);
      ScheduledTaskData data = SchedulingManager.GetManager(providerName).GetTaskData().Where<ScheduledTaskData>((Expression<Func<ScheduledTaskData, bool>>) (x => x.Id == parsedId)).FirstOrDefault<ScheduledTaskData>();
      return data != null ? new WcfScheduledTask(data) : new WcfScheduledTask();
    }

    /// <inheritdoc />
    public WcfScheduledTask GetTaskProgressByName(
      string taskName,
      string providerName)
    {
      ScheduledTaskData data = SchedulingManager.GetManager(providerName).GetTaskData().Where<ScheduledTaskData>((Expression<Func<ScheduledTaskData, bool>>) (x => x.TaskName == taskName)).FirstOrDefault<ScheduledTaskData>();
      return data != null ? new WcfScheduledTask(data) : new WcfScheduledTask();
    }

    /// <inheritdoc />
    public void ManageTask(string taskId, SchedulingTaskCommand taskCommand, string providerName)
    {
      if (string.IsNullOrEmpty(taskId))
        return;
      Guid parsedId = Guid.Parse(taskId);
      SchedulingManager manager = SchedulingManager.GetManager(providerName);
      ScheduledTaskData taskData = manager.GetTaskData().Where<ScheduledTaskData>((Expression<Func<ScheduledTaskData, bool>>) (x => x.Id == parsedId)).FirstOrDefault<ScheduledTaskData>();
      if (taskData == null)
        return;
      this.ManageTaskInternal(taskCommand, manager, taskData);
    }

    internal void ManageTaskInternal(
      SchedulingTaskCommand taskCommand,
      SchedulingManager manager,
      ScheduledTaskData taskData)
    {
      Scheduler instance = Scheduler.Instance;
      switch (taskCommand)
      {
        case SchedulingTaskCommand.Restart:
        case SchedulingTaskCommand.Resume:
          instance.RestartTask(taskData.Id);
          break;
        case SchedulingTaskCommand.Stop:
          instance.StopTask(taskData.Id);
          break;
        case SchedulingTaskCommand.Delete:
          instance.StopTask(taskData.Id);
          Thread.Sleep(200);
          manager.DeleteTaskData(taskData);
          manager.SaveChanges();
          break;
        case SchedulingTaskCommand.Start:
          instance.UpdateNextTaskRun(taskData, DateTime.UtcNow, taskData.ScheduleData);
          manager.SaveChanges();
          break;
      }
    }
  }
}
