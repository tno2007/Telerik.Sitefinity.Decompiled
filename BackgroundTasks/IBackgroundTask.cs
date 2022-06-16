// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.BackgroundTasks.IBackgroundTask
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.BackgroundTasks
{
  /// <summary>
  /// This interface should be implemented by any class whose instances
  /// are going to be passed for execution on a separate thread.
  /// </summary>
  public interface IBackgroundTask
  {
    /// <summary>
    /// Contains the work that will be executed in a separate thread.
    /// </summary>
    /// <param name="context">Contains information for the calling context.
    /// This parameter could be used to receive information from the calling host.</param>
    void Run(IBackgroundTaskContext context);
  }
}
