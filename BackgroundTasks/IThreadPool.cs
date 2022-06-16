// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.BackgroundTasks.IThreadPool
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Threading;

namespace Telerik.Sitefinity.BackgroundTasks
{
  internal interface IThreadPool : IBackgroundTaskContext
  {
    /// <summary>
    /// Get the thread priority of worker threads in the pool.
    /// </summary>
    ThreadPriority ThreadPriority { get; }

    /// <summary>
    /// The given <see cref="T:Telerik.Sitefinity.BackgroundTasks.WorkerThread" /> is marked as available for running more tasks.
    /// (e.g. it could me moved in a set of available resources.)
    /// </summary>
    /// <param name="wt"></param>
    void MakeAvailable(WorkerThread wt);

    /// <summary>
    /// The given <see cref="T:Telerik.Sitefinity.BackgroundTasks.WorkerThread" /> is removed from the set of busy resources.
    /// (it could me moved in a set of available resources either.)
    /// </summary>
    /// <param name="wt"></param>
    void ClearFromBusyWorkers(WorkerThread wt);
  }
}
