// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.IWorkflowUtils
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Workflow
{
  internal interface IWorkflowUtils
  {
    /// <summary>
    /// Checks if the user is allowed approval workflow action on given item.
    /// </summary>
    /// <param name="item">The item that participates in approval workflow.</param>
    /// <param name="action">The action. to check.</param>
    /// <returns>A value indicating whether </returns>
    bool CanUser(IWorkflowItem item, string action);

    /// <summary>
    /// Gets the required action that the user must have for editing items in given approval workflow status.
    /// The action is assigned during creation of the workflow permissions. Actions can be review (first level), approve (second level), publish (third level.
    /// </summary>
    /// <param name="workflowState">The approval workflow status.</param>
    /// <returns>Action name.</returns>
    string GetRequiredActionForWorkflowState(string workflowState);
  }
}
