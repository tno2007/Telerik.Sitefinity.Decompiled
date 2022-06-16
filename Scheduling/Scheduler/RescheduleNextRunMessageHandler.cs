// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Scheduling.RescheduleNextRunMessageHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.LoadBalancing;
using Telerik.Sitefinity.Scheduling.Configuration;

namespace Telerik.Sitefinity.Scheduling
{
  /// <summary>
  /// A class responsible for handling rescheduling next run system message sent to Sitefinity instances at load balanced environment.
  /// </summary>
  public class RescheduleNextRunMessageHandler : ISystemMessageHandler
  {
    /// <summary>
    /// Determines whether this instance [can process system message] the specified system message.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <returns>
    ///     <c>true</c> if this instance [can process system message] the specified message; otherwise, <c>false</c>.
    /// </returns>
    public bool CanProcessSystemMessage(SystemMessageBase message) => message.Key == "RescheduleNextRunKey";

    /// <summary>Processes the system message.</summary>
    /// <param name="message">The message.</param>
    public void ProcessSystemMessage(SystemMessageBase message)
    {
      if (Config.Get<SchedulingConfig>().DisableScheduledTasksExecution)
        return;
      Scheduler.Instance.RescheduleNextRun();
    }
  }
}
