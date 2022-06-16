// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.BackgroundTasks.IBackgroundTasksService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.BackgroundTasks
{
  /// <summary>
  /// Provides common methods for service that could execute tasks in parallel tasks in background
  /// and/or queue them for later execution.
  /// </summary>
  public interface IBackgroundTasksService
  {
    /// <summary>Adds the task in the queue for execution.</summary>
    /// <param name="task">The task.</param>
    void EnqueueTask(IBackgroundTask task);

    /// <summary>Adds the action in the queue for execution.</summary>
    /// <param name="action">The action.</param>
    void EnqueueTask(Action action);

    /// <summary>
    /// Adds the action in the queue for execution and accepts parameter the execution context
    /// (in order to be notified when the task should stop).
    /// </summary>
    /// <param name="action">The action.</param>
    void EnqueueTask(Action<IBackgroundTaskContext> action);

    /// <summary>
    /// Tries to run the given task if there is available slot.
    /// Returns true if it starts the task successfully otherwise false.
    /// </summary>
    /// <param name="action">The action to be added for execution.</param>
    /// <returns>Returns true if it starts the task successfully otherwise false.</returns>
    bool TryRunTask(Action<IBackgroundTaskContext> action);

    /// <summary>
    /// Tries to run the given task if there is available slot.
    /// Returns true if it starts the task successfully otherwise false.
    /// </summary>
    /// <param name="task">The <see cref="T:Telerik.Sitefinity.BackgroundTasks.IBackgroundTask" /> to be added for execution.</param>
    /// <returns>Returns true if it starts the task successfully otherwise false.</returns>
    bool TryRunTask(IBackgroundTask task);

    /// <summary>
    /// Tries to run the given task. If there is no available slot waits and tries to run on the first available.
    /// Returns true when the task starts. Otherwise false.
    /// </summary>
    /// <param name="task">The <see cref="T:Telerik.Sitefinity.BackgroundTasks.IBackgroundTask" /> to be added for execution.</param>
    bool RunTask(IBackgroundTask task);
  }
}
