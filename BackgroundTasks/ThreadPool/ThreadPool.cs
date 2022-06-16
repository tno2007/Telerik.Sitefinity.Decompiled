// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.BackgroundTasks.ThreadPool
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace Telerik.Sitefinity.BackgroundTasks
{
  /// <summary>
  /// Container for a limited number of pre-created threads available for running tasks in the background.
  /// </summary>
  internal class ThreadPool : ITaskRunnerService, IThreadPool, IBackgroundTaskContext
  {
    private readonly object nextTaskLock = new object();
    private readonly LinkedList<WorkerThread> availWorkers = new LinkedList<WorkerThread>();
    private readonly LinkedList<WorkerThread> busyWorkers = new LinkedList<WorkerThread>();
    private bool handoffPending;
    private volatile bool isShutdown;
    private string threadNamePrefix;
    private List<WorkerThread> workers;

    /// <summary>Gets the number of worker threads in the pool.</summary>
    public virtual int ThreadCount { get; protected set; }

    /// <inheritdoc />
    public virtual ThreadPriority ThreadPriority { get; protected set; }

    /// <summary>Gets the thread name prefix.</summary>
    /// <value>The thread name prefix.</value>
    public virtual string ThreadNamePrefix
    {
      get
      {
        if (this.threadNamePrefix == null)
          this.threadNamePrefix = string.Format("{0}-{1}", (object) (this.Name ?? this.GetType().Name), (object) "Thread");
        return this.threadNamePrefix;
      }
      protected set => this.threadNamePrefix = value;
    }

    /// <summary>Gets if the threads are running in the background.</summary>
    /// <value>Makes threads background.</value>
    public virtual bool AreBackgroundThreads { get; protected set; }

    /// <summary>
    /// Inform the <see cref="T:Telerik.Sitefinity.BackgroundTasks.IThreadPool" /> of the Scheduler instance's Id,
    /// prior to initialize being invoked.
    /// </summary>
    public virtual string Id { get; protected set; }

    /// <summary>
    /// Inform the <see cref="T:Telerik.Sitefinity.BackgroundTasks.IThreadPool" /> of the Scheduler instance's name,
    /// prior to initialize being invoked.
    /// </summary>
    public virtual string Name { get; protected set; }

    /// <inheritdoc />
    public bool ShuttingDown => this.isShutdown;

    /// <summary>
    /// Create a new <see cref="T:Telerik.Sitefinity.BackgroundTasks.ThreadPool" /> with the specified number
    /// of <see cref="T:System.Threading.Thread" /> s that have the given priority.
    /// </summary>
    /// <param name="threadCount">
    /// the number of worker <see cref="T:System.Threading.Thread" />s in the pool, must
    /// be &gt;= 0. If it is set to 0 the number of processors of the current machine will be used.
    /// </param>
    /// <param name="threadPriority">the thread priority for the worker threads.</param>
    /// <param name="name">The name of the thread pool. It appears also as a prefix to the thread name.</param>
    /// <param name="background">A value indicating if the threads should be marked to run in the background.</param>
    /// <param name="threadNamePrefix">The prefix used to name the thread. (Used in logging).</param>
    public ThreadPool(
      int threadCount = 0,
      string name = null,
      string threadNamePrefix = null,
      ThreadPriority threadPriority = ThreadPriority.Normal,
      bool background = false)
    {
      if (threadCount < 0)
        throw new ThreadPoolException("The threadCount value must be >= 0");
      this.ThreadCount = threadCount == 0 ? (int) Math.Ceiling((double) Environment.ProcessorCount / 2.0) : threadCount;
      this.Name = name ?? typeof (ThreadPool).Name;
      this.Id = Guid.NewGuid().ToString();
      this.ThreadPriority = threadPriority;
      this.AreBackgroundThreads = background;
      this.ThreadNamePrefix = threadNamePrefix;
      this.Initialize();
    }

    /// <summary>
    /// Run the given <see cref="T:Telerik.Sitefinity.BackgroundTasks.IBackgroundTask" /> object in the next available
    /// <see cref="T:System.Threading.Thread" />. If while waiting the thread pool is asked to
    /// shut down, the task is executed immediately within a new additional
    /// thread.
    /// </summary>
    /// <param name="task">The <see cref="T:Telerik.Sitefinity.BackgroundTasks.IBackgroundTask" /> to be added.</param>
    public virtual bool RunTask(IBackgroundTask task) => this.RunTaskInternal(task, true);

    /// <summary>
    /// Checks for available <see cref="T:System.Threading.Thread" /> and if there is - runs the given
    /// <see cref="T:Telerik.Sitefinity.BackgroundTasks.IBackgroundTask" /> object in the next available <see cref="T:System.Threading.Thread" />.
    /// Returns false if there is no <see cref="T:Telerik.Sitefinity.BackgroundTasks.IBackgroundTask" />
    /// specified or if there is no available <see cref="T:System.Threading.Thread" /> or if the pool is shut down.
    /// </summary>
    /// <param name="task">The <see cref="T:Telerik.Sitefinity.BackgroundTasks.IBackgroundTask" /> to be added.</param>
    /// <returns>Returns false if there is no <see cref="T:Telerik.Sitefinity.BackgroundTasks.IBackgroundTask" /> specified or if there is no available <see cref="T:System.Threading.Thread" />.</returns>
    public virtual bool TryRunTask(IBackgroundTask task) => this.RunTaskInternal(task);

    /// <summary>
    /// Terminate any worker threads in this thread group.
    /// Jobs currently in progress will complete.
    /// The instance of the thread pool can't be reused after shut down.
    /// You have to create a new one.
    /// </summary>
    public virtual void Shutdown() => this.Shutdown(ThreadPool.ThreadPoolShutDownOptions.ShutDownAndWaitJobs);

    /// <summary>
    /// Shutdowns the thread pool. The instance of the thread pool can't be reused after shut down.
    /// You have to create a new one.
    /// </summary>
    /// <param name="shutDownOption">The shut down option.</param>
    /// <param name="abortTimeout">If the shut down option is AbortJobs - The thread sleeps this timeout before calling Abort() on them.</param>
    public virtual void Shutdown(
      ThreadPool.ThreadPoolShutDownOptions shutDownOption,
      int abortTimeout = 0)
    {
      SafeLogger.TryLog("Shutting down threadpool...", TraceEventType.Information);
      this.isShutdown = true;
      if (this.workers == null)
        return;
      for (int index = 0; index < this.workers.Count; ++index)
      {
        if (this.workers[index] != null)
          this.workers[index].Shutdown();
      }
      string message;
      switch (shutDownOption)
      {
        case ThreadPool.ThreadPoolShutDownOptions.BeginShutDown:
          message = string.Format("Thread pool '{0}': Shutdown of thread pool completed. All currently running threads will finish their work and stop.", (object) this.Name);
          break;
        case ThreadPool.ThreadPoolShutDownOptions.ShutDownAndWaitJobs:
          lock (this.nextTaskLock)
          {
            Monitor.PulseAll(this.nextTaskLock);
            while (this.handoffPending)
            {
              try
              {
                Monitor.Wait(this.nextTaskLock, 100);
              }
              catch (ThreadInterruptedException ex)
              {
              }
            }
            while (this.busyWorkers.Count > 0)
            {
              LinkedListNode<WorkerThread> first = this.busyWorkers.First;
              try
              {
                SafeLogger.TryLog(string.Format("Waiting for thread {0} to shut down", (object) first.Value.Name), TraceEventType.Information);
                Monitor.Wait(this.nextTaskLock, 2000);
              }
              catch (ThreadInterruptedException ex)
              {
              }
            }
            while (this.workers.Count > 0)
            {
              int index = this.workers.Count - 1;
              WorkerThread worker = this.workers[index];
              try
              {
                worker.Join();
                this.workers.RemoveAt(index);
              }
              catch (ThreadStateException ex)
              {
              }
            }
          }
          message = string.Format("Thread pool '{0}': Shutdown of thread pool completed. No executing jobs remaining, all threads finished their work and stopped.", (object) this.Name);
          break;
        case ThreadPool.ThreadPoolShutDownOptions.AbortJobs:
          int millisecondsTimeout = 1000;
          for (; this.busyWorkers.Count > 0 && abortTimeout >= 0; abortTimeout -= millisecondsTimeout)
            Thread.Sleep(millisecondsTimeout);
          this.workers.ForEach((Action<WorkerThread>) (wt => wt.Abort()));
          this.workers.Clear();
          this.busyWorkers.Clear();
          message = string.Format("Thread pool '{0}': Shutdown of thread pool completed. Threads aborted.", (object) this.Name);
          break;
        default:
          throw new ArgumentException(string.Format("Unsupported shut down option: '{0}'", (object) Enum.GetName(typeof (ThreadPool.ThreadPoolShutDownOptions), (object) shutDownOption)));
      }
      this.availWorkers.Clear();
      SafeLogger.TryLog(message, TraceEventType.Information);
    }

    /// <summary>
    /// Blocks the current thread until there is at least one available worker thread.
    /// </summary>
    /// <returns>The number of available threads.</returns>
    public virtual int BlockForAvailableThreads()
    {
      lock (this.nextTaskLock)
      {
        while (this.availWorkers.Count < 1 || this.handoffPending)
        {
          if (!this.isShutdown)
          {
            try
            {
              Monitor.Wait(this.nextTaskLock, 500);
            }
            catch (ThreadInterruptedException ex)
            {
            }
          }
          else
            break;
        }
        return this.availWorkers.Count;
      }
    }

    /// <summary>Gets the number of the available threads.</summary>
    /// <returns></returns>
    public virtual int GetAvailableThreadsCount()
    {
      lock (this.nextTaskLock)
        return this.availWorkers.Count;
    }

    /// <inheritdoc />
    public virtual void MakeAvailable(WorkerThread wt)
    {
      lock (this.nextTaskLock)
      {
        if (!this.isShutdown)
          this.availWorkers.AddLast(wt);
        this.busyWorkers.Remove(wt);
        Monitor.PulseAll(this.nextTaskLock);
      }
    }

    /// <inheritdoc />
    public virtual void ClearFromBusyWorkers(WorkerThread wt)
    {
      lock (this.nextTaskLock)
      {
        this.busyWorkers.Remove(wt);
        Monitor.PulseAll(this.nextTaskLock);
      }
    }

    internal IEnumerable<WorkerThread> GetRunningWorkerThreads()
    {
      lock (this.nextTaskLock)
        return (IEnumerable<WorkerThread>) this.busyWorkers;
    }

    internal int GetRunningSequentialsTaskCount() => this.GetRunningWorkerThreads().Select<WorkerThread, IBackgroundTask>((Func<WorkerThread, IBackgroundTask>) (worker => worker.BackgroundTask)).OfType<ISequentialTask>().Count<ISequentialTask>((Func<ISequentialTask, bool>) (p => p.IsSequential));

    /// <summary>
    /// Creates the threads and starts them in order to be ready for accepting jobs
    /// </summary>
    private void Initialize()
    {
      if (this.workers != null && this.workers.Count > 0)
        return;
      foreach (WorkerThread workerThread in this.CreateWorkerThreads(this.ThreadCount))
      {
        workerThread.Start();
        this.availWorkers.AddLast(workerThread);
      }
    }

    /// <summary>Creates the worker threads.</summary>
    /// <param name="threadCount">The thread count.</param>
    /// <returns></returns>
    private List<WorkerThread> CreateWorkerThreads(int threadCount)
    {
      this.workers = new List<WorkerThread>();
      for (int index = 1; index <= threadCount; ++index)
        this.workers.Add(new WorkerThread((IThreadPool) this, string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0}-{1}", (object) this.ThreadNamePrefix, (object) index), this.ThreadPriority, this.AreBackgroundThreads));
      return this.workers;
    }

    private bool RunTaskInternal(IBackgroundTask task, bool waitForAvailableThread = false)
    {
      if (task == null || this.isShutdown)
        return false;
      lock (this.nextTaskLock)
      {
        this.handoffPending = true;
        if (this.availWorkers.Count < 1)
        {
          if (waitForAvailableThread)
          {
            while (this.availWorkers.Count < 1)
            {
              if (!this.isShutdown)
              {
                try
                {
                  Monitor.Wait(this.nextTaskLock, 500);
                }
                catch (ThreadInterruptedException ex)
                {
                }
              }
              else
                break;
            }
          }
          else
          {
            this.handoffPending = false;
            return false;
          }
        }
        if (task is ISequentialTask sequentialTask && sequentialTask.IsSequential && this.GetRunningSequentialsTaskCount() > 0)
        {
          this.handoffPending = false;
          return false;
        }
        if (!this.isShutdown)
        {
          WorkerThread workerThread = this.availWorkers.First.Value;
          this.availWorkers.RemoveFirst();
          this.busyWorkers.AddLast(workerThread);
          workerThread.Run(task);
        }
        else
        {
          WorkerThread workerThread = new WorkerThread((IThreadPool) this, "SitefinityWorkerThread-LastJob", this.ThreadPriority, this.AreBackgroundThreads, task);
          this.busyWorkers.AddLast(workerThread);
          this.workers.Add(workerThread);
          workerThread.Start();
        }
        Monitor.PulseAll(this.nextTaskLock);
        this.handoffPending = false;
      }
      return true;
    }

    /// <summary>
    /// Contains the available options for shutting down the thread pool.
    /// </summary>
    public enum ThreadPoolShutDownOptions
    {
      /// <summary>
      /// Removes the available workers and stops the thread pool.
      /// All started tasks will finish and the threads will be destroyed after that.
      /// </summary>
      BeginShutDown,
      /// <summary>
      /// The thread pool waits for all jobs to complete before stopping.
      /// </summary>
      ShutDownAndWaitJobs,
      /// <summary>
      /// Gives the threads a period of time to stop and if they are not finished calls Abort() on them.
      /// </summary>
      AbortJobs,
    }
  }
}
