// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.LoadBalancing.SystemMessagesBackgroundService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.BackgroundTasks;

namespace Telerik.Sitefinity.LoadBalancing
{
  /// <summary>
  /// A background task service dedicated to system messages dispatching between NLB nodes.
  /// </summary>
  internal class SystemMessagesBackgroundService : BackgroundTasksService
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.LoadBalancing.SystemMessagesBackgroundService" /> class.
    /// </summary>
    /// <param name="maxParallelTasks">The max parallel tasks.</param>
    /// <param name="name">The name.</param>
    public SystemMessagesBackgroundService(int maxParallelTasks = 0, string name = null)
      : base(maxParallelTasks, name)
    {
    }
  }
}
