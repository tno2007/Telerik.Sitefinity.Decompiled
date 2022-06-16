// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.BackgroundTasks.WorkerThread
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading;

namespace Telerik.Sitefinity.BackgroundTasks
{
  /// <summary>A Worker loops, waiting to Execute tasks.</summary>
  internal class WorkerThread
  {
    /// <summary>The instance of System.Threading.Thread</summary>
    private readonly Thread thread;
    /// <summary>A flag that signals the WorkerThread to terminate.</summary>
    private volatile bool run = true;
    private IBackgroundTask task;
    private readonly IThreadPool tp;
    private readonly bool runOnce;

    /// <summary>Gets or sets the name of the thread</summary>
    public string Name
    {
      get => this.thread.Name;
      protected set => this.thread.Name = value;
    }

    /// <summary>Gets a background task.</summary>
    public IBackgroundTask BackgroundTask => this.task;

    /// <summary>
    /// Gets or sets a value indicating the scheduling priority of a thread
    /// </summary>
    protected ThreadPriority Priority
    {
      get => this.thread.Priority;
      set => this.thread.Priority = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether or not a thread is a background thread.
    /// </summary>
    protected bool IsBackground
    {
      set => this.thread.IsBackground = value;
      get => this.thread.IsBackground;
    }

    /// <summary>Initializes a new instance of the WorkerThread class</summary>
    internal WorkerThread() => this.thread = new Thread(new ParameterizedThreadStart(this.Run));

    /// <summary>Initializes a new instance of the Thread class.</summary>
    /// <param name="name">The name of the thread</param>
    internal WorkerThread(string name)
      : this()
    {
      this.Name = name;
    }

    /// <summary>
    /// Create a worker thread and start it. Waiting for the next Task,
    /// executing it, and waiting for the next Task, until the Shutdown
    /// flag is set.
    /// </summary>
    internal WorkerThread(IThreadPool tp, string name, ThreadPriority prio, bool isBackground)
      : this(tp, name, prio, isBackground, (IBackgroundTask) null)
    {
    }

    /// <summary>
    /// Create a worker thread, start it, Execute the task and terminate
    /// the thread (one time execution).
    /// </summary>
    internal WorkerThread(
      IThreadPool tp,
      string name,
      ThreadPriority prio,
      bool isBackground,
      IBackgroundTask task)
      : this(name)
    {
      this.tp = tp;
      this.task = task;
      if (task != null)
        this.runOnce = true;
      this.Priority = prio;
      this.IsBackground = isBackground;
    }

    /// <summary>
    /// Causes the operating system to change the state of the current thread instance to ThreadState.Running
    /// </summary>
    public void Start() => this.thread.Start((object) this.tp);

    /// <summary>
    /// Interrupts a thread that is in the WaitSleepJoin thread state
    /// </summary>
    protected void Interrupt() => this.thread.Interrupt();

    /// <summary>Blocks the calling thread until a thread terminates</summary>
    public void Join() => this.thread.Join();

    /// <summary>Raises a <see cref="T:System.Threading.ThreadAbortException" /> in the
    /// thread on which it is invoked, to begin the process of terminating the thread.
    /// Calling this method usually terminates the thread.</summary>
    public void Abort() => this.thread.Abort();

    /// <summary>Obtain a string that represents the current object</summary>
    /// <returns>A string that represents the current object</returns>
    public override string ToString() => string.Format((IFormatProvider) CultureInfo.InvariantCulture, "Thread[{0},{1},]", (object) this.Name, (object) this.Priority);

    /// <summary>Signal the thread that it should terminate.</summary>
    internal virtual void Shutdown() => this.run = false;

    /// <summary>
    /// Sets the given newTask to be run from the internal Thread.
    /// </summary>
    /// <param name="newTask">The new task.</param>
    public void Run(IBackgroundTask newTask)
    {
      lock (this)
      {
        this.task = this.task == null ? newTask : throw new ArgumentException("Already running a Task!");
        Monitor.PulseAll((object) this);
      }
    }

    private void Run(object context) => this.Run(context as IBackgroundTaskContext);

    /// <summary>Loop, executing targets as they are received.</summary>
    private void Run(IBackgroundTaskContext context)
    {
      bool flag = false;
      for (bool run = this.run; run; run = this.run)
      {
        try
        {
          lock (this)
          {
            while (this.task == null && this.run)
              Monitor.Wait((object) this, 500);
            if (this.task != null)
            {
              flag = true;
              this.task.Run(context);
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
        finally
        {
          lock (this)
            this.task = (IBackgroundTask) null;
          if (this.Priority != this.tp.ThreadPriority)
            this.Priority = this.tp.ThreadPriority;
          if (this.runOnce)
          {
            this.run = false;
            this.tp.ClearFromBusyWorkers(this);
          }
          else if (flag)
          {
            flag = false;
            this.tp.MakeAvailable(this);
          }
        }
      }
      SafeLogger.TryLog("WorkerThread is shut down", TraceEventType.Information);
    }
  }
}
