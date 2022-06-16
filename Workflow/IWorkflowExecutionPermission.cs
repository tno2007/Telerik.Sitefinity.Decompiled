// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.IWorkflowExecutionPermission
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Workflow
{
  /// <summary>Describes who is allowed to execute a workflow step.</summary>
  public interface IWorkflowExecutionPermission
  {
    /// <summary>
    /// Gets workflow's action. Standard workflows support:
    /// 1. "Approve"
    /// 2. "Publish" (for 2-step workflows only).
    /// </summary>
    string ActionName { get; }

    /// <summary>
    /// Gets the id of the user/role who is allowed to execute workflow's step.
    /// </summary>
    string PrincipalId { get; }

    /// <summary>Gets the name of the user/role.</summary>
    string PrincipalName { get; }

    /// <summary>Gets whether this is user or role.</summary>
    WorkflowPrincipalType PrincipalType { get; }
  }
}
