// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.BackgroundTasks.BackgroundTasksService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web.Hosting;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.BackgroundTasks
{
  /// <summary>
  /// Provides the ability to run tasks in background in a web applications.
  /// It provides a safe mechanism for stopping when the application restart occurs.
  /// </summary>
  internal class BackgroundTasksService : IBackgroundTasksService, IRegisteredObject
  {
    private DateTime stopBeginAt;
    private const int softRestartMaxMillisecondsWait = 30000;
    private const string Increasing = "Increasing";
    private readonly ConcurrentQueue<IBackgroundTask> parallelTasksQueue = new ConcurrentQueue<IBackgroundTask>();
    private readonly ConcurrentQueue<IBackgroundTask> sequentialTasksQueue = new ConcurrentQueue<IBackgroundTask>();
    private Thread schedulerThread;
    /// <summary>
    /// A flag that signals the schedulerThread to terminate.
    /// </summary>
    private volatile bool stop;

    internal ThreadPool ThreadPool { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.BackgroundTasks.BackgroundTasksService" /> class.
    /// </summary>
    public BackgroundTasksService() => this.Initialize(Config.Get<SystemConfig>().BackgroundTasks.MaxBackgroundParallelTasksPerNode);

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.BackgroundTasks.BackgroundTasksService" /> class.
    /// </summary>
    /// <param name="maxParallelTasks">The max parallel tasks that could be executed.</param>
    /// <param name="name">The name of the service.</param>
    internal BackgroundTasksService(int maxParallelTasks = 0, string name = null) => this.Initialize(maxParallelTasks, name);

    /// <inheritdoc />
    public bool RunTask(IBackgroundTask task) => this.ThreadPool.RunTask(task);

    /// <inheritdoc />
    public bool TryRunTask(IBackgroundTask task) => this.ThreadPool.TryRunTask(task);

    /// <inheritdoc />
    public bool TryRunTask(Action<IBackgroundTaskContext> action) => this.ThreadPool.TryRunTask((IBackgroundTask) new BackgroundTasksService.BackgroundTask(action));

    /// <inheritdoc />
    public void EnqueueTask(IBackgroundTask task)
    {
      lock (this)
      {
        this.LogWaitingTasksCount("Increasing");
        this.parallelTasksQueue.Enqueue(task);
        Monitor.PulseAll((object) this);
      }
    }

    /// <inheritdoc />
    public void EnqueueTask(Action action)
    {
      lock (this)
      {
        this.LogWaitingTasksCount("Increasing");
        this.parallelTasksQueue.Enqueue((IBackgroundTask) new BackgroundTasksService.BackgroundTask(action));
        Monitor.PulseAll((object) this);
      }
    }

    /// <summary>Adds the action in the queue for execution.</summary>
    /// <param name="action">The action.</param>
    /// <param name="isSequential">Whether background task is sequential.</param>
    public virtual void EnqueueTask(Action action, bool isSequential)
    {
      lock (this)
      {
        this.LogWaitingTasksCount("Increasing");
        BackgroundTasksService.BackgroundTask backgroundTask = new BackgroundTasksService.BackgroundTask(action);
        ((ISequentialTask) backgroundTask).IsSequential = isSequential;
        if (isSequential)
          this.sequentialTasksQueue.Enqueue((IBackgroundTask) backgroundTask);
        else
          this.parallelTasksQueue.Enqueue((IBackgroundTask) backgroundTask);
        Monitor.PulseAll((object) this);
      }
    }

    /// <inheritdoc />
    public void EnqueueTask(Action<IBackgroundTaskContext> action)
    {
      lock (this)
      {
        this.LogWaitingTasksCount("Increasing");
        this.parallelTasksQueue.Enqueue((IBackgroundTask) new BackgroundTasksService.BackgroundTask(action));
        Monitor.PulseAll((object) this);
      }
    }

    /// <summary>
    /// Stops the thread pool and unregisters it from the hosting environment to allow
    /// the application domain to be unloaded. Called automatically by the ASP.NET.
    /// </summary>
    /// <param name="immediate">true to indicate the registered object should unregister
    /// from the hosting environment before returning; otherwise, false.</param>
    public void Stop(bool immediate)
    {
      int beforeAbortTasks = Config.Get<SystemConfig>().BackgroundTasks.WaitBeforeAbortTasks;
      this.Stop(immediate, beforeAbortTasks);
    }

    /// <summary>Stops the specified immediate.</summary>
    /// <param name="immediate">The immediate.</param>
    /// <param name="waitTimeout">Timeout given to the tasks to stop in milliseconds.</param>
    internal void Stop(bool immediate, int waitTimeout)
    {
      this.stop = true;
      if (immediate)
      {
        int abortTimeout = 0;
        int milliseconds = (DateTime.UtcNow - this.stopBeginAt).Milliseconds;
        if (waitTimeout > 0 && waitTimeout > milliseconds)
          abortTimeout = waitTimeout - milliseconds;
        this.ThreadPool.Shutdown(ThreadPool.ThreadPoolShutDownOptions.AbortJobs, abortTimeout);
        this.ShutDownScheduler(abortTimeout);
        HostingEnvironment.UnregisterObject((IRegisteredObject) this);
      }
      else
      {
        this.stopBeginAt = DateTime.UtcNow;
        this.ThreadPool.Shutdown(ThreadPool.ThreadPoolShutDownOptions.BeginShutDown);
      }
    }

    /// <summary>
    /// Gets if the background tasks service has been stopped.
    /// </summary>
    /// <value>The stopped.</value>
    internal bool IsStopped => this.stop;

    private void Initialize(int maxParallelTasks, string name = null)
    {
      if (maxParallelTasks < 0)
        throw new ArgumentException(string.Format("maxParallelTasks should be greater than or equal to {0}", (object) 0));
      if (string.IsNullOrEmpty(name))
        name = this.GetType().Name;
      this.ThreadPool = new ThreadPool(maxParallelTasks, name);
      this.StartScheduler();
      HostingEnvironment.RegisterObject((IRegisteredObject) this);
      SystemManager.ShuttingDown += new EventHandler<CancelEventArgs>(this.SystemManager_ShuttingDown);
      SystemManager.Shutdown += new EventHandler<EventArgs>(this.SystemManager_Shutdown);
    }

    private void SystemManager_ShuttingDown(object sender, CancelEventArgs e)
    {
      this.Stop(false);
      SystemManager.ShuttingDown -= new EventHandler<CancelEventArgs>(this.SystemManager_ShuttingDown);
    }

    private void SystemManager_Shutdown(object sender, EventArgs e)
    {
      this.Stop(true, 30000);
      SystemManager.Shutdown -= new EventHandler<EventArgs>(this.SystemManager_Shutdown);
    }

    private void StartScheduler()
    {
      this.schedulerThread = new Thread(new ThreadStart(this.RunBackgroundScheduler));
      this.schedulerThread.Start();
    }

    private void ShutDownScheduler(int abortTimeout)
    {
      int millisecondsTimeout = 500;
      for (; abortTimeout > 0; abortTimeout -= millisecondsTimeout)
      {
        if (this.schedulerThread.ThreadState == System.Threading.ThreadState.Stopped || this.schedulerThread.ThreadState == System.Threading.ThreadState.Aborted || this.schedulerThread.ThreadState == System.Threading.ThreadState.Unstarted)
          return;
        Thread.Sleep(millisecondsTimeout);
      }
      this.schedulerThread.Abort();
    }

    private void RunBackgroundScheduler()
    {
      ConcurrentQueue<IBackgroundTask>[] source = new ConcurrentQueue<IBackgroundTask>[2]
      {
        this.parallelTasksQueue,
        this.sequentialTasksQueue
      };
      while (!this.stop)
      {
        try
        {
          this.ThreadPool.BlockForAvailableThreads();
          lock (this)
          {
            while (((IEnumerable<ConcurrentQueue<IBackgroundTask>>) source).All<ConcurrentQueue<IBackgroundTask>>((Func<ConcurrentQueue<IBackgroundTask>, bool>) (q => q.Count == 0)) && !this.stop)
              Monitor.Wait((object) this, 500);
            foreach (ConcurrentQueue<IBackgroundTask> concurrentQueue in source)
            {
              IBackgroundTask result;
              if (concurrentQueue.Count > 0 && concurrentQueue.TryPeek(out result) && result != null && this.ThreadPool.TryRunTask(result))
              {
                this.LogWaitingTasksCount("Decreasing...");
                concurrentQueue.TryDequeue(out result);
              }
            }
          }
        }
        catch (ThreadAbortException ex)
        {
        }
        catch (Exception ex)
        {
          SafeLogger.TryLog(string.Format("Error while executing the Task: {0}, StackTrace: {1}", (object) ex.Message, (object) ex.StackTrace), TraceEventType.Error);
        }
      }
      SafeLogger.TryLog("Background task scheduler shut down", TraceEventType.Information);
    }

    /// <summary>
    /// If testing is enabled and there are existing tasks in the queue - logs their count.
    /// </summary>
    private void LogWaitingTasksCount(string message)
    {
      if (!Config.SectionHandler.Testing.Enabled)
        return;
      int num = this.parallelTasksQueue.Count + this.sequentialTasksQueue.Count;
      if (num <= 0)
        return;
      Telerik.Sitefinity.Abstractions.Log.Write((object) string.Format("{0} There are {1} waiting tasks. {2}", (object) DateTime.UtcNow, (object) num, (object) message));
    }

    private class BackgroundTask : IBackgroundTask, ISequentialTask
    {
      private readonly Action action;
      private readonly Action<IBackgroundTaskContext> actionContext;

      bool ISequentialTask.IsSequential { get; set; }

      public BackgroundTask(Action action) => this.action = action;

      public BackgroundTask(Action<IBackgroundTaskContext> action) => this.actionContext = action;

      public void Run(IBackgroundTaskContext context)
      {
        BackgroundOperationStartEvent operationStartEvent = this.CreateBackgroundOperationStartEvent();
        EventHub.Raise((IEvent) operationStartEvent);
        if (this.action != null)
          this.action();
        if (this.actionContext != null)
          this.actionContext(context);
        EventHub.Raise((IEvent) new BackgroundOperationEndEvent(operationStartEvent.OperationKey));
      }

      private BackgroundOperationStartEvent CreateBackgroundOperationStartEvent()
      {
        string methodName = string.Empty;
        string className = string.Empty;
        string empty = string.Empty;
        if (this.action != null)
        {
          methodName = this.action.Method.Name;
          className = this.action.Method.DeclaringType.FullName;
          if (this.action.Target != null)
            empty = this.action.Target.GetHashCode().ToString((IFormatProvider) CultureInfo.InvariantCulture);
        }
        else if (this.actionContext != null)
        {
          methodName = this.actionContext.Method.Name;
          className = this.actionContext.Method.DeclaringType.FullName;
          if (this.actionContext.Target != null)
            empty = this.actionContext.Target.GetHashCode().ToString((IFormatProvider) CultureInfo.InvariantCulture);
        }
        return new BackgroundOperationStartEvent(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "Class: {0}|Method: {1}|Instance hash code: {2}", (object) className, (object) methodName, (object) empty), className, methodName, empty, "Background");
      }
    }
  }
}
