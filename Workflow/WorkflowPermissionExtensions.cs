// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.WorkflowPermissionExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Workflow;
using Telerik.Sitefinity.Workflow.Model;

namespace Telerik.Sitefinity
{
  /// <summary>
  /// Extension methods for the <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowPermission" /> type.
  /// </summary>
  public static class WorkflowPermissionExtensions
  {
    /// <summary>
    /// Creates incomplete <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowPermission" /> from
    /// <see cref="T:Telerik.Sitefinity.Workflow.IWorkflowExecutionPermission" />
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    internal static WorkflowPermission FromIWorkflowExecutionPermission(
      IWorkflowExecutionPermission source)
    {
      return new WorkflowPermission()
      {
        ActionName = source.ActionName,
        PrincipalId = source.PrincipalId,
        PrincipalName = source.PrincipalName,
        PrincipalType = source.PrincipalType
      };
    }
  }
}
