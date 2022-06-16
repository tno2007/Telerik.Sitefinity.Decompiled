// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.BackgroundTasks.IBackgroundTaskContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.BackgroundTasks
{
  /// <summary>
  /// Represents the context information for the background task.
  /// Can be used to pass information to the executing thread.
  /// </summary>
  public interface IBackgroundTaskContext
  {
    /// <summary>
    /// Provides information if the context object is stopping.
    /// It could be an indicator to exit the run method safely
    /// before the thread being Aborted.
    /// </summary>
    /// <value>The shut down.</value>
    bool ShuttingDown { get; }
  }
}
