// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Upgrade.DynamicContentPermissionsUpgradeTask
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Scheduling;
using Telerik.Sitefinity.Scheduling.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.DynamicModules.Builder.Upgrade
{
  /// <summary>
  /// Upgrade task that modifies dynamic content permissions asynchronously.
  /// </summary>
  public class DynamicContentPermissionsUpgradeTask : UpgradeScheduledTask
  {
    /// <summary>
    /// The Guid used for the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Upgrade.DynamicContentPermissionsUpgradeTask" /> when the actual upgrade is performed
    /// </summary>
    public const string TaskUpgradeId = "447A4631-FCA8-4EA6-93C5-A5F07AE4047A";
    /// <summary>
    /// The Guid used for the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Upgrade.DynamicContentPermissionsUpgradeTask" /> when the task will reschedule a new <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Upgrade.DynamicContentPermissionsUpgradeTask" /> task
    /// </summary>
    public const string TaskReschedulingId = "48C08631-C40C-4D54-8376-4BA221F496F1";
    /// <summary>
    /// The Guid used for the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Upgrade.DynamicContentPermissionsUpgradeTask" /> when the actual upgrade is performed
    /// </summary>
    public static readonly Guid TaskUpgradeGuid = new Guid("447A4631-FCA8-4EA6-93C5-A5F07AE4047A");
    /// <summary>
    /// The Guid used for the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Upgrade.DynamicContentPermissionsUpgradeTask" /> when the task will reschedule a new <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Upgrade.DynamicContentPermissionsUpgradeTask" /> task
    /// </summary>
    public static readonly Guid TaskReschedulingGuid = new Guid("48C08631-C40C-4D54-8376-4BA221F496F1");

    /// <summary>
    /// Message to be logged when the upgrade fails or succeeds.
    /// </summary>
    public override string UpgradeMessage => "Upgrading dynamic content permissions";

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Upgrade.DynamicContentPermissionsUpgradeTask" /> with updated NumberOfAttempts.
    /// </summary>
    /// <returns>New <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Upgrade.DynamicContentPermissionsUpgradeTask" /> instance.</returns>
    protected override ScheduledTask CreateTaskInstance() => this.IsReschedulingTask() ? this.CreateUpgradeTask() : this.CreateReschedulingTask();

    /// <summary>
    /// Does the actual upgrade. If Sitefinity is still initializing, reschedules the task.
    /// </summary>
    protected override void Upgrade()
    {
      if (SystemManager.Initializing || this.IsReschedulingTask())
      {
        ++this.NumberOfAttempts;
        SchedulingManager manager = SchedulingManager.GetManager();
        ScheduledTask taskInstance = this.CreateTaskInstance();
        if (taskInstance == null)
          return;
        manager.AddTask(taskInstance);
        manager.SaveChanges();
      }
      else
        new DynamicContentUpgradePermissionsStrategyTo73().Upgrade();
    }

    /// <summary>
    /// Determines whether current task is a Rescheduling task.
    /// </summary>
    /// <returns><c>True</c> if the current task is a Rescheduling task and <c>False</c> otherwise.</returns>
    private bool IsReschedulingTask() => this.Id == DynamicContentPermissionsUpgradeTask.TaskReschedulingGuid;

    /// <summary>
    /// Creates an Upgrade task - a task that when executed will try to perform the actual upgrade.
    /// </summary>
    /// <returns>Returns the newly created Upgrade task.</returns>
    private ScheduledTask CreateUpgradeTask()
    {
      if (SchedulingManager.GetManager().GetTaskData().Any<ScheduledTaskData>((Expression<Func<ScheduledTaskData, bool>>) (t => t.Id == DynamicContentPermissionsUpgradeTask.TaskUpgradeGuid)))
        return (ScheduledTask) null;
      DynamicContentPermissionsUpgradeTask upgradeTask = new DynamicContentPermissionsUpgradeTask();
      upgradeTask.Id = DynamicContentPermissionsUpgradeTask.TaskUpgradeGuid;
      upgradeTask.ExecuteTime = DateTime.UtcNow.AddMinutes(1.0);
      upgradeTask.NumberOfAttempts = this.NumberOfAttempts;
      upgradeTask.UpgradeFrom = this.UpgradeFrom;
      return (ScheduledTask) upgradeTask;
    }

    /// <summary>
    /// Creates a Rescheduling task - a task that when executed will only reschedule a new Upgrade task.
    /// </summary>
    /// <returns>Returns the newly created Rescheduling task.</returns>
    private ScheduledTask CreateReschedulingTask()
    {
      if (SchedulingManager.GetManager().GetTaskData().Any<ScheduledTaskData>((Expression<Func<ScheduledTaskData, bool>>) (t => t.Id == DynamicContentPermissionsUpgradeTask.TaskReschedulingGuid)))
        return (ScheduledTask) null;
      DynamicContentPermissionsUpgradeTask reschedulingTask = new DynamicContentPermissionsUpgradeTask();
      reschedulingTask.Id = DynamicContentPermissionsUpgradeTask.TaskReschedulingGuid;
      reschedulingTask.ExecuteTime = DateTime.UtcNow.AddMinutes(0.0);
      reschedulingTask.NumberOfAttempts = this.NumberOfAttempts;
      reschedulingTask.UpgradeFrom = this.UpgradeFrom;
      return (ScheduledTask) reschedulingTask;
    }
  }
}
