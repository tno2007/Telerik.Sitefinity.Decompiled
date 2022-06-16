// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.OutputCache.TaskWrapperAsyncResult
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Threading;
using System.Threading.Tasks;

namespace Telerik.Sitefinity.Web.OutputCache
{
  internal sealed class TaskWrapperAsyncResult : IAsyncResult
  {
    private bool forceCompletedSynchronously;

    public object AsyncState { get; private set; }

    public WaitHandle AsyncWaitHandle => ((IAsyncResult) this.Task).AsyncWaitHandle;

    public bool CompletedSynchronously => this.forceCompletedSynchronously || ((IAsyncResult) this.Task).CompletedSynchronously;

    public bool IsCompleted => this.Task.IsCompleted;

    public Task Task { get; private set; }

    public TaskWrapperAsyncResult(Task task, object asyncState)
    {
      this.Task = task;
      this.AsyncState = asyncState;
    }

    public void ForceCompletedSynchronously() => this.forceCompletedSynchronously = true;
  }
}
