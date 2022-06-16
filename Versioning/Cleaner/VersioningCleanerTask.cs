// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Versioning.Cleaner.VersioningCleanerTask
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Scheduling;
using Telerik.Sitefinity.Scheduling.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Versioning.Configuration;
using Telerik.Sitefinity.Versioning.Model;

namespace Telerik.Sitefinity.Versioning.Cleaner
{
  /// <summary>
  /// The task is used to schedule versioning cleanup for all items in versioning. All <see cref="T:Telerik.Sitefinity.Versioning.Model.Item" /> have their <see cref="T:Telerik.Sitefinity.Versioning.Model.Change" /> cleaned if they do not meet a given requirement enforced by <see cref="T:Telerik.Sitefinity.Versioning.Cleaner.VersioningCleanerUtils" />.
  /// </summary>
  internal class VersioningCleanerTask : ScheduledTask
  {
    public static readonly string RevisionHistoryCleanerTaskKey = "3d54205e-c593-444c-bd9f-16ec9fadd5f0";

    public VersioningCleanerTask()
    {
      this.Key = VersioningCleanerTask.RevisionHistoryCleanerTaskKey;
      CacheDependency.Unsubscribe(typeof (VersionConfig), new ChangedCallback(VersioningCleanerTask.OnConfigUpdated));
      CacheDependency.Subscribe(typeof (VersionConfig), new ChangedCallback(VersioningCleanerTask.OnConfigUpdated));
    }

    public override string TaskName => this.GetType().FullName;

    public static ScheduledTask NewInstance()
    {
      string cronTabSpecs = VersioningCleanerTask.ParseCronTabSpecs();
      VersioningCleanerTask versioningCleanerTask = new VersioningCleanerTask();
      versioningCleanerTask.Id = Guid.NewGuid();
      versioningCleanerTask.ScheduleSpecType = "crontab";
      versioningCleanerTask.ScheduleSpec = cronTabSpecs;
      return (ScheduledTask) versioningCleanerTask;
    }

    public static void ScheduleTask()
    {
      VersioningCleanerTask.DeleteExistingTasks();
      ScheduledTask task = VersioningCleanerTask.NewInstance();
      if (task == null)
        return;
      Scheduler.Instance.TryToScheduleNextTaskRun(task);
    }

    internal static string GetTaskName() => typeof (VersioningCleanerTask).FullName;

    internal static string ParseCronTabSpecs()
    {
      VersionConfig versionConfig = Config.Get<VersionConfig>();
      DateTime utcNow = DateTime.UtcNow;
      int year = utcNow.Year;
      utcNow = DateTime.UtcNow;
      int month = utcNow.Month;
      utcNow = DateTime.UtcNow;
      int day = utcNow.Day;
      DateTime dateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.SpecifyKind(new DateTime(year, month, day) + versionConfig.Cleaner.TimeToRunCleanupTask, DateTimeKind.Utc), SystemManager.CurrentContext.GetCurrentTimeZone());
      return string.Format("{0} {1} * * *", (object) dateTime.Minute, (object) dateTime.Hour);
    }

    public override void ExecuteTask()
    {
      if (Config.Get<VersionConfig>().Cleaner.HistoryLimitEnabled)
      {
        Log.Write((object) "Revision History cleanup task started.", ConfigurationPolicy.Debug);
        try
        {
          VersionManager.GetManager().CleanUpVersioning();
        }
        catch (ThreadAbortException ex)
        {
          VersioningCleanerTask.ScheduleTask();
          Log.Write((object) string.Format("Revision history cleanup task run was interrupted due to system restart. Task was scheduled to run again after 1 hour."), ConfigurationPolicy.ErrorLog);
        }
        catch (Exception ex)
        {
          Log.Write((object) string.Format("Revision history cleanup task run was unsuccessful. Exception: {0}", (object) ex), ConfigurationPolicy.ErrorLog);
        }
        Log.Write((object) "Revision History cleanup task finished execution successfuly.", ConfigurationPolicy.Debug);
      }
      else
        Log.Write((object) "Revision History cleanup not configured.", ConfigurationPolicy.Debug);
      VersioningCleanerTask.CleanDependencyData();
    }

    internal static void CleanDependencyData()
    {
      string str = nameof (CleanDependencyData);
      VersionManager manager = VersionManager.GetManager((string) null, str);
      IQueryable<Dependency> dependencies = manager.Provider.GetDependencies();
      Expression<Func<Dependency, bool>> predicate = (Expression<Func<Dependency, bool>>) (d => d.Changes.Count == 0);
      foreach (Dependency dependency in (IEnumerable<Dependency>) dependencies.Where<Dependency>(predicate))
      {
        VersioningCleanerTask.ScheduleDependencyCleanerTask(dependency, str);
        manager.Provider.DeleteDependency(dependency);
      }
      TransactionManager.CommitTransaction(str);
    }

    private static void ScheduleDependencyCleanerTask(
      Dependency orphanedDependency,
      string trasnactionName)
    {
      Type type = TypeResolutionService.ResolveType(orphanedDependency.CleanUpTaskType, false);
      if (!(type != (Type) null))
        return;
      ScheduledTask instance = (ScheduledTask) Activator.CreateInstance(type);
      instance.Key = orphanedDependency.Key;
      instance.SetCustomData(orphanedDependency.Data);
      SchedulingManager.GetManager((string) null, trasnactionName).AddTask(instance);
    }

    internal static void DeleteExistingTasks()
    {
      SchedulingManager manager = SchedulingManager.GetManager();
      IQueryable<ScheduledTaskData> taskData = manager.GetTaskData();
      Expression<Func<ScheduledTaskData, bool>> predicate = (Expression<Func<ScheduledTaskData, bool>>) (t => t.Key == VersioningCleanerTask.RevisionHistoryCleanerTaskKey && !t.IsRunning);
      foreach (ScheduledTaskData scheduledTaskData in (IEnumerable<ScheduledTaskData>) taskData.Where<ScheduledTaskData>(predicate))
        manager.DeleteItem((object) scheduledTaskData);
      manager.SaveChanges();
    }

    internal static DateTime GetNextExecuteTime()
    {
      ScheduledTaskData scheduledTaskData = SchedulingManager.GetManager().GetTaskData().Where<ScheduledTaskData>((Expression<Func<ScheduledTaskData, bool>>) (x => x.Key == VersioningCleanerTask.RevisionHistoryCleanerTaskKey)).FirstOrDefault<ScheduledTaskData>();
      return scheduledTaskData != null ? scheduledTaskData.ExecuteTime : DateTime.MaxValue;
    }

    internal static bool IsTaskRunning()
    {
      ScheduledTaskData scheduledTaskData = SchedulingManager.GetManager().GetTaskData().FirstOrDefault<ScheduledTaskData>((Expression<Func<ScheduledTaskData, bool>>) (x => x.Key == VersioningCleanerTask.RevisionHistoryCleanerTaskKey));
      return scheduledTaskData != null && scheduledTaskData.IsRunning;
    }

    internal static void OnConfigUpdated(
      ICacheDependencyHandler caller,
      Type trackedItemType,
      string trackedItemKey)
    {
      VersioningCleanerTask.ScheduleTask();
    }
  }
}
