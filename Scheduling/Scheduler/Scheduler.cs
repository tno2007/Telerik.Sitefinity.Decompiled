// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Scheduling.Scheduler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Timers;
using System.Web.Hosting;
using Telerik.OpenAccess.Exceptions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.LoadBalancing;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Scheduling.Configuration;
using Telerik.Sitefinity.Scheduling.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Events;
using Telerik.Sitefinity.SyncLock;

namespace Telerik.Sitefinity.Scheduling
{
  /// <summary>
  /// A class for managing scheduled tasks for all providers.
  /// </summary>
  public class Scheduler : IDisposable, IRegisteredObject
  {
    private int ApplicationStopRetryCount = 3;
    private readonly System.Timers.Timer internalTimer;
    private static Scheduler instance = (Scheduler) null;
    private static readonly object SingletonLock = new object();
    private static readonly object ThreadLock = new object();
    private static readonly object ApplicationStopLock = new object();
    private static readonly string LockId = nameof (Scheduler);
    private static Dictionary<Guid, ScheduledTask> RunningTasks = new Dictionary<Guid, ScheduledTask>();

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Scheduling.Scheduler" /> class.
    /// </summary>
    internal Scheduler()
    {
      if (!Config.Get<SchedulingConfig>().DisableScheduledTasksExecution)
      {
        this.internalTimer = new System.Timers.Timer();
        this.internalTimer.AutoReset = false;
        this.internalTimer.Interval = 10000.0;
        this.internalTimer.Elapsed += new ElapsedEventHandler(this.InternalTimer_Elapsed);
      }
      string transactionName = Guid.NewGuid().ToString();
      try
      {
        SchedulingManager.DeleteAllTasks((Expression<Func<ScheduledTaskData, bool>>) (item => item.IsRecurring == true), false, transactionName);
        TransactionManager.CommitTransaction(transactionName);
      }
      catch (Exception ex)
      {
        this.LogMessage("InitialDeletingTasks", ex);
      }
      HostingEnvironment.RegisterObject((IRegisteredObject) this);
    }

    /// <summary>
    /// Gets an instance of the SchedulerService class - only one instance can exist at a time in the application
    /// </summary>
    public static Scheduler Instance
    {
      get
      {
        lock (Scheduler.SingletonLock)
        {
          if (Scheduler.instance == null)
            Scheduler.instance = new Scheduler();
          return Scheduler.instance;
        }
      }
    }

    /// <summary>
    /// Releases unmanaged and - optionally - managed resources
    /// </summary>
    public void Dispose() => this.internalTimer.Dispose();

    /// <summary>
    /// Stops the thread pool and unregisters it from the hosting environment to allow
    /// the application domain to be unloaded. Called automatically by the ASP.NET.
    /// </summary>
    /// <param name="immediate">true to indicate the registered object should unregister
    /// from the hosting environment before returning; otherwise, false.</param>
    void IRegisteredObject.Stop(bool immediate)
    {
      lock (Scheduler.ApplicationStopLock)
      {
        try
        {
          this.internalTimer.Stop();
          foreach (ScheduledTask scheduledTask in Scheduler.RunningTasks.Values.ToList<ScheduledTask>())
          {
            for (int index = 0; index < this.ApplicationStopRetryCount; ++index)
            {
              try
              {
                ScheduledTaskData taskData = SchedulingManager.GetManager(scheduledTask.ProviderName, scheduledTask.TransactionName).GetTaskData(scheduledTask.Id);
                taskData.Status = TaskStatus.Pending;
                taskData.IsRunning = false;
                taskData.ExecuteTime = DateTime.UtcNow.AddMinutes(1.0);
                TransactionManager.CommitTransaction(scheduledTask.TransactionName);
                break;
              }
              catch (ConcurrencyControlException ex)
              {
              }
              catch (Exception ex)
              {
                if (Telerik.Sitefinity.Abstractions.Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
                  throw ex;
                break;
              }
            }
          }
          Scheduler.RunningTasks.Clear();
        }
        catch (Exception ex)
        {
          if (Telerik.Sitefinity.Abstractions.Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
            throw ex;
        }
        finally
        {
          HostingEnvironment.UnregisterObject((IRegisteredObject) this);
        }
      }
    }

    /// <summary>Stops the task.</summary>
    /// <param name="taskId">The task identifier.</param>
    public void StopTask(Guid taskId)
    {
      SchedulingManager manager = SchedulingManager.GetManager();
      for (int index = 0; index < 100; ++index)
      {
        try
        {
          ScheduledTaskData taskData = manager.GetTaskData(taskId);
          if (!taskData.IsRunning || taskData.Status == TaskStatus.Stopped)
            break;
          taskData.Status = TaskStatus.Stopped;
          manager.SaveChanges();
          break;
        }
        catch (ConcurrencyControlException ex)
        {
        }
      }
    }

    /// <summary>Restarts the task.</summary>
    /// <param name="taskId">The task identifier.</param>
    public void RestartTask(Guid taskId)
    {
      SchedulingManager manager = SchedulingManager.GetManager();
      ScheduledTaskData taskData = manager.GetTaskData(taskId);
      if (taskData.IsRunning && (taskData.Status == TaskStatus.Failed || taskData.Status == TaskStatus.Stopped))
      {
        taskData.IsRunning = false;
        taskData.Progress = 0;
        taskData.Status = TaskStatus.Pending;
        taskData.StatusMessage = string.Empty;
        manager.SaveChanges();
      }
      this.RescheduleNextRun();
    }

    /// <summary>Reschedules the next run.</summary>
    public void RescheduleNextRun()
    {
      if (!Config.Get<SchedulingConfig>().DisableScheduledTasksExecution)
      {
        if (!Bootstrapper.IsReady)
        {
          this.RescheduleTimer();
        }
        else
        {
          try
          {
            ScheduledTaskData nextTask = SchedulingManager.GetNextTask((string) null, true);
            if (nextTask == null)
              return;
            this.internalTimer.Stop();
            double num = (nextTask.ExecuteTime.AddSeconds((double) new Random().Next(0, 10)) - DateTime.UtcNow).TotalMilliseconds;
            if (num > (double) int.MaxValue)
              num = (double) int.MaxValue;
            this.internalTimer.Interval = num >= 0.0 ? num : 1.0;
            this.internalTimer.Start();
          }
          catch (Exception ex)
          {
            this.LogMessage("RescheduleTask", ex);
          }
        }
      }
      else
        SystemMessageDispatcher.SendSystemMessage((SystemMessageBase) new RescheduleNextRunMessage());
    }

    internal static BackgroundOperationStartEvent CreateBackgroundOperationStartEvent(
      string taskName,
      Guid taskId)
    {
      return new BackgroundOperationStartEvent(Scheduler.GetOperationKey(taskName, taskId), taskName, (string) null, taskId.ToString(), "Scheduled");
    }

    internal static string GetOperationKey(string taskName, Guid taskId) => "ScheduledTask|" + taskName + "|" + (object) taskId;

    internal DateTime? GetNextOccurence(
      string scheduleSpec,
      string scheduleSpecType = null,
      DateTime? baseTime = null)
    {
      string type;
      string expr;
      this.ParseScheduleSpec(scheduleSpec, out type, out expr, scheduleSpecType);
      return this.ResolveScheduleCalculator(type).GetNextOccurrence(expr, baseTime ?? DateTime.UtcNow);
    }

    internal void ParseScheduleSpec(
      string spec,
      out string type,
      out string expr,
      string defaultType = null)
    {
      defaultType = defaultType ?? "crontab";
      int length = spec.IndexOf(':');
      if (length == -1)
      {
        type = defaultType;
        expr = spec;
      }
      else
      {
        type = spec.Substring(0, length);
        expr = spec.Substring(length + 1);
      }
    }

    internal DateTime? TryToScheduleNextTaskRun(ScheduledTask task)
    {
      if (string.IsNullOrEmpty(task.ScheduleSpec) || !task.CanReschedule())
        return new DateTime?();
      DateTime? inCurrentTimeZone = this.GetNextOccurenceInCurrentTimeZone(task.ScheduleSpec, "crontab");
      if (inCurrentTimeZone.HasValue)
        this.ScheduleNextTaskRun(task, inCurrentTimeZone.Value);
      return inCurrentTimeZone;
    }

    internal DateTime? TryToScheduleNextTaskRun(
      ScheduledTask task,
      SchedulingManager schedulingManager)
    {
      if (string.IsNullOrEmpty(task.ScheduleSpec) || !task.CanReschedule())
        return new DateTime?();
      DateTime? inCurrentTimeZone = this.GetNextOccurenceInCurrentTimeZone(task.ScheduleSpec, "crontab");
      if (inCurrentTimeZone.HasValue)
        this.ScheduleNextTaskRun(task, inCurrentTimeZone.Value, schedulingManager);
      return inCurrentTimeZone;
    }

    internal DateTime? TryToUpdateNextTaskRun(
      ScheduledTaskData taskData,
      string scheduleSpec)
    {
      DateTime? inCurrentTimeZone = this.GetNextOccurenceInCurrentTimeZone(scheduleSpec, "crontab");
      if (inCurrentTimeZone.HasValue)
      {
        DateTime? nullable = inCurrentTimeZone;
        DateTime executeTime = taskData.ExecuteTime;
        if ((nullable.HasValue ? (nullable.HasValue ? (nullable.GetValueOrDefault() != executeTime ? 1 : 0) : 0) : 1) != 0)
          this.UpdateNextTaskRun(taskData, inCurrentTimeZone.Value, scheduleSpec);
      }
      return inCurrentTimeZone;
    }

    internal DateTime? GetNextOccurenceInCurrentTimeZone(
      string scheduleSpec,
      string scheduleSpecType)
    {
      TimeZoneInfo currentTimeZone = SystemManager.CurrentContext.GetCurrentTimeZone();
      DateTime dateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, currentTimeZone);
      DateTime? inCurrentTimeZone = Scheduler.Instance.GetNextOccurence(scheduleSpec, scheduleSpecType, new DateTime?(dateTime));
      try
      {
        if (inCurrentTimeZone.HasValue)
          inCurrentTimeZone = new DateTime?(TimeZoneInfo.ConvertTimeToUtc(inCurrentTimeZone.Value, currentTimeZone));
      }
      catch (Exception ex)
      {
        this.LogMessage("RescheduleTask", ex);
        inCurrentTimeZone = Scheduler.Instance.GetNextOccurence(scheduleSpec, scheduleSpecType);
      }
      return inCurrentTimeZone;
    }

    internal void ScheduleNextTaskRun(ScheduledTask task, DateTime nextRun)
    {
      Scheduler.PersistTaskData(task, nextRun);
      this.RescheduleNextRun();
    }

    internal void ScheduleNextTaskRun(
      ScheduledTask task,
      DateTime nextRun,
      SchedulingManager schedulingManager)
    {
      Scheduler.PersistTaskData(task, nextRun, schedulingManager);
      this.RescheduleNextRun();
    }

    internal void UpdateNextTaskRun(
      ScheduledTaskData taskData,
      DateTime nextRun,
      string scheduleSpec)
    {
      taskData.ScheduleData = scheduleSpec;
      taskData.ExecuteTime = nextRun;
      taskData.Status = TaskStatus.Pending;
    }

    /// <summary>Internals the timer elapsed.</summary>
    /// <param name="parameters">The parameters.</param>
    internal void InternalTimerElapsed(object[] parameters)
    {
      lock (Scheduler.ThreadLock)
      {
        bool flag1 = true;
        this.internalTimer.Stop();
        string transactionName = Guid.NewGuid().ToString();
        try
        {
          ScheduledTaskData data = (ScheduledTaskData) null;
          using (LockRegion<SchedulingManager> lockRegion = new LockRegion<SchedulingManager>(Scheduler.LockId, TimeSpan.FromSeconds(30.0)))
          {
            int num = 5;
            bool flag2 = false;
            while (num-- > 0)
            {
              if (!flag2)
              {
                if (lockRegion.TryAcquire())
                {
                  flag2 = true;
                  data = SchedulingManager.GetNextTask(transactionName);
                  if (data != null)
                  {
                    if (!(data.ExecuteTime > DateTime.UtcNow))
                    {
                      try
                      {
                        data.Status = TaskStatus.Started;
                        data.IsRunning = true;
                        TransactionManager.CommitTransaction(transactionName);
                      }
                      catch (ConcurrencyControlException ex)
                      {
                        flag1 = false;
                        throw ex;
                      }
                      catch (Exception ex)
                      {
                        throw;
                      }
                    }
                    else
                      break;
                  }
                  else
                    break;
                }
                else
                  Thread.Sleep(100);
              }
              else
                break;
            }
          }
          if (data == null || data.ExecuteTime > DateTime.UtcNow)
          {
            Thread.Sleep(new Random().Next(1000, 1200));
          }
          else
          {
            SystemManager.RunWithElevatedPrivilegeDelegate doWork = new SystemManager.RunWithElevatedPrivilegeDelegate(this.ExecuteTasks);
            List<ScheduledTask> tasks = new List<ScheduledTask>()
            {
              SchedulingTaskFactory.ResolveTask(data)
            };
            ThreadPool.QueueUserWorkItem((WaitCallback) (tmp =>
            {
              try
              {
                SystemManager.RunWithElevatedPrivilege(doWork, new object[2]
                {
                  (object) tasks,
                  (object) transactionName
                });
              }
              catch (Exception ex)
              {
                this.LogMessage("ExecuteAllTasksRun", ex);
              }
            }));
          }
        }
        catch (Exception ex)
        {
          if (!flag1)
            return;
          this.LogMessage("ExecuteAllTasks", ex);
        }
        finally
        {
          try
          {
            TransactionManager.DisposeTransaction(transactionName);
          }
          catch (Exception ex)
          {
          }
          this.RescheduleNextRun();
        }
      }
    }

    private static void PersistTaskData(ScheduledTask task, DateTime nextRun)
    {
      SchedulingManager manager = SchedulingManager.GetManager();
      ScheduledTaskData taskData = manager.CreateTaskData();
      task.CopyToTaskData(taskData);
      taskData.ExecuteTime = nextRun;
      taskData.LastExecutedTime = task.LastExecutedTime;
      taskData.Status = TaskStatus.Pending;
      manager.SaveChanges();
    }

    private static void PersistTaskData(
      ScheduledTask task,
      DateTime nextRun,
      SchedulingManager schedulingManager)
    {
      ScheduledTaskData taskData = schedulingManager.CreateTaskData();
      task.CopyToTaskData(taskData);
      taskData.ExecuteTime = nextRun;
      taskData.LastExecutedTime = task.LastExecutedTime;
      taskData.Status = TaskStatus.Pending;
    }

    private IScheduleCalculator ResolveScheduleCalculator(string type)
    {
      if (type.ToLowerInvariant() == "crontab")
        return (IScheduleCalculator) new CrontabScheduleCalculator();
      throw new NotImplementedException("Unsupported scheduling spec type: " + type);
    }

    /// <summary>Executes the tasks.</summary>
    /// <param name="parameters">The parameters.</param>
    private void ExecuteTasks(object[] parameters)
    {
      IList<ScheduledTask> parameter1 = (IList<ScheduledTask>) parameters[0];
      string parameter2 = (string) parameters[1];
      foreach (ScheduledTask task in (IEnumerable<ScheduledTask>) parameter1)
        this.ExecuteTask(task, parameter2);
    }

    private void ExecuteTask(ScheduledTask task, string transactionName)
    {
      BackgroundOperationStartEvent operationStartEvent = Scheduler.CreateBackgroundOperationStartEvent(task.TaskName, task.Id);
      string str = "Succeeded";
      bool flag = false;
      try
      {
        EventHub.Raise((IEvent) operationStartEvent);
        this.SetThreadCultureBeforeTaskExecute(task.Language);
        task.TransactionName = transactionName;
        task.ProgressChanged += new EventHandler<TaskProgressEventArgs>(this.Task_ProgressChanged);
        if (!Scheduler.RunningTasks.ContainsKey(task.Id))
          Scheduler.RunningTasks.Add(task.Id, task);
        DateTime utcNow1 = DateTime.UtcNow;
        using (SiteRegion.FromSiteId(task.SiteId))
        {
          using (new CultureRegion(task.Language))
            task.ExecuteTask();
        }
        task.LastExecutedTime = new DateTime?(DateTime.UtcNow);
        Scheduler.Instance.TryToScheduleNextTaskRun(task);
        flag = true;
        this.DeleteTask(task, transactionName);
        DateTime utcNow2 = DateTime.UtcNow;
        this.LogMessage(string.Format("Scheduler: Task executed: '{0}'. Start time: {1}. End time: {2}. Task data: '{3}'", (object) task.TaskName, (object) utcNow1, (object) utcNow2, (object) task.GetCustomData()));
      }
      catch (Exception ex1)
      {
        ScheduledTaskData taskData = SchedulingManager.GetManager(task.ProviderName, transactionName).GetTaskData(task.Id);
        taskData.LastExecutedTime = new DateTime?(DateTime.UtcNow);
        if (ex1 is TaskStoppedException)
        {
          taskData.Status = TaskStatus.Stopped;
          taskData.StatusMessage = "Stopped by user";
        }
        else
        {
          if (taskData.Status == TaskStatus.Pending)
            return;
          if (!flag)
            Scheduler.Instance.TryToScheduleNextTaskRun(task);
          taskData.Status = TaskStatus.Failed;
          taskData.StatusMessage = ex1.Message;
        }
        str = taskData.Status.ToString();
        try
        {
          TransactionManager.CommitTransaction(transactionName);
        }
        catch (ConcurrencyControlException ex2)
        {
        }
        catch (Exception ex3)
        {
          throw;
        }
        this.ThrowTaskFailedException(task, ex1);
      }
      finally
      {
        Scheduler.RunningTasks.Remove(task.Id);
        task.ProgressChanged -= new EventHandler<TaskProgressEventArgs>(this.Task_ProgressChanged);
        SystemManager.ClearCurrentTransactions();
        EventHub.Raise((IEvent) new BackgroundOperationEndEvent(operationStartEvent.OperationKey)
        {
          Status = str
        });
      }
    }

    private void Task_ProgressChanged(object sender, TaskProgressEventArgs e)
    {
      ScheduledTask scheduledTask = sender as ScheduledTask;
      string transactionName = scheduledTask.TransactionName;
      ScheduledTaskData taskData = SchedulingManager.GetManager(scheduledTask.ProviderName, transactionName).GetTaskData(scheduledTask.Id);
      if (taskData.Status == TaskStatus.Stopped)
      {
        e.Stopped = true;
      }
      else
      {
        taskData.Progress = e.Progress;
        taskData.StatusMessage = e.StatusMessage;
        try
        {
          TransactionManager.CommitTransaction(transactionName);
        }
        catch (ConcurrencyControlException ex)
        {
          e.Stopped = true;
        }
      }
    }

    private void DeleteTask(ScheduledTask task, string transactionName)
    {
      SchedulingManager manager = SchedulingManager.GetManager(task.ProviderName, transactionName);
      try
      {
        ScheduledTaskData taskData = manager.GetTaskData(task.Id);
        manager.DeleteTaskData(taskData);
        TransactionManager.CommitTransaction(transactionName);
      }
      catch (Exception ex)
      {
        TransactionManager.RollbackTransaction(transactionName);
        if (Telerik.Sitefinity.Abstractions.Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
          throw ex;
      }
    }

    private void ThrowTaskFailedException(ScheduledTask task, Exception ex)
    {
      string str = task.Title.IsNullOrEmpty() ? task.TaskName : task.Title;
      TaskFailedException exceptionToHandle = new TaskFailedException("Task '{0}' (aka '{1}') has FAILED: {2}".Arrange((object) task.Id, (object) str, (object) ex.Message), ex);
      if (Telerik.Sitefinity.Abstractions.Exceptions.HandleException((Exception) exceptionToHandle, ExceptionPolicyName.IgnoreExceptions))
        throw exceptionToHandle;
    }

    private void SetThreadCultureBeforeTaskExecute(string language)
    {
      CultureInfo orDefaultLanguage = this.GetCultureOrDefaultLanguage(language);
      if (SystemManager.CurrentContext.Culture.Equals((object) orDefaultLanguage))
        return;
      SystemManager.CurrentContext.Culture = orDefaultLanguage;
    }

    private CultureInfo GetCultureOrDefaultLanguage(string language) => !language.IsNullOrEmpty() ? new CultureInfo(language) : SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage;

    private void LogMessage(string customMessage, Exception ex) => this.LogMessage(string.Format("Scheduler : {0} , Exception Message {1} : StackTrace : {2}", (object) customMessage, (object) ex.Message, (object) ex.StackTrace));

    private void LogMessage(string message) => Log.Write((object) message);

    private void RescheduleTimer(double millisecondsInterval = 60000.0)
    {
      this.internalTimer.Interval = millisecondsInterval;
      this.internalTimer.Start();
    }

    /// <summary>
    /// Handles the Elapsed event of the internalTimer control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:System.Timers.ElapsedEventArgs" /> instance containing the event data.</param>
    private void InternalTimer_Elapsed(object sender, ElapsedEventArgs e)
    {
      if (!Bootstrapper.IsReady)
        this.RescheduleTimer();
      else
        SystemManager.RunWithElevatedPrivilege(new SystemManager.RunWithElevatedPrivilegeDelegate(this.InternalTimerElapsed), (object[]) null);
    }
  }
}
