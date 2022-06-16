// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.Activities.IWorkflowNotifier
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Workflow.Activities
{
  /// <summary>
  /// Sends notifications about changes to workflow items' statuses.
  /// </summary>
  public interface IWorkflowNotifier
  {
    /// <summary>
    /// Sends the notification to a collection of subscribers.
    /// </summary>
    /// <param name="context">Context of the workflow item.</param>
    void SendNotification(WorkflowNotificationContext context);
  }
}
