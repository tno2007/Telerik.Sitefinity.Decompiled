// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.LoadBalancing.SchedulingHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Scheduling;

namespace Telerik.Sitefinity.LoadBalancing
{
  /// <summary>
  /// A class responsible for handling scheduling task system message sent to Sitefinity instances at load balanced environment.
  ///  </summary>
  public class SchedulingHandler : ISystemMessageHandler
  {
    /// <summary>
    /// Determines whether this instance [can process system message] the specified system message.
    /// </summary>
    /// <param name="message">The message.</param>
    /// <returns>
    /// 	<c>true</c> if this instance [can process system message] the specified message; otherwise, <c>false</c>.
    /// </returns>
    public bool CanProcessSystemMessage(SystemMessageBase message) => message.Key == "SchedulingKey";

    /// <summary>Processes the system message.</summary>
    /// <param name="message">The message.</param>
    public void ProcessSystemMessage(SystemMessageBase message) => Scheduler.Instance.RescheduleNextRun();
  }
}
