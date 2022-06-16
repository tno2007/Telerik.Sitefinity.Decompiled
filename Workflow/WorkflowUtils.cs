// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.WorkflowUtils
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Workflow
{
  internal class WorkflowUtils : IWorkflowUtils
  {
    public bool CanUser(IWorkflowItem x, string action) => WorkflowManager.GetWorkflowExecutionDefinition(x).CanUser(action);

    public string GetRequiredActionForWorkflowState(string workflowState)
    {
      if (workflowState == "AwaitingApproval")
        return "Approve";
      if (workflowState == "AwaitingPublishing")
        return "Publish";
      return workflowState == "AwaitingReview" ? "Review" : (string) null;
    }
  }
}
