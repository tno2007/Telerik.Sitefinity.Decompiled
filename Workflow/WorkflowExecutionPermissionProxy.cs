// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.WorkflowExecutionPermissionProxy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Workflow.Model;

namespace Telerik.Sitefinity.Workflow
{
  /// <summary>
  /// Wrapper for <see cref="T:Telerik.Sitefinity.Workflow.IWorkflowExecutionPermission" />
  /// </summary>
  public class WorkflowExecutionPermissionProxy : IWorkflowExecutionPermission
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Workflow.WorkflowExecutionPermissionProxy" /> class.
    /// Copies relevant fields from a <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowPermission" /> instance.
    /// </summary>
    /// <param name="wp">Workflow permission as defined in the database.</param>
    public WorkflowExecutionPermissionProxy(WorkflowPermission wp)
    {
      this.ActionName = wp.ActionName;
      this.PrincipalId = wp.PrincipalId;
      this.PrincipalName = wp.PrincipalName;
      this.PrincipalType = wp.PrincipalType;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Workflow.WorkflowExecutionPermissionProxy" /> class
    /// with each field filled from the respective parameter.
    /// </summary>
    /// <param name="actionName">Name of the workflow action ("Approve" or "Publish")</param>
    /// <param name="principalId">Id of the role or the user who is allowed to execute the action</param>
    /// <param name="principalName">Name of the role or the user who is allowed to execute the action</param>
    /// <param name="principalType">Type of the principal (role or user)</param>
    public WorkflowExecutionPermissionProxy(
      string actionName,
      string principalId,
      string principalName,
      WorkflowPrincipalType principalType)
    {
      this.ActionName = actionName;
      this.PrincipalId = principalId;
      this.PrincipalName = principalName;
      this.PrincipalType = principalType;
    }

    /// <inheritdoc />
    public string ActionName { get; private set; }

    /// <inheritdoc />
    public string PrincipalId { get; private set; }

    /// <inheritdoc />
    public string PrincipalName { get; private set; }

    /// <inheritdoc />
    public WorkflowPrincipalType PrincipalType { get; private set; }
  }
}
