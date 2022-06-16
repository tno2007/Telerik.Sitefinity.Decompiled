// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.WorkflowDefinitionViewModelExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Workflow.Services.Data;

namespace Telerik.Sitefinity
{
  /// <summary>
  /// Extension methods for the <see cref="T:Telerik.Sitefinity.Workflow.Services.Data.WorkflowDefinitionViewModel" /> type.
  /// </summary>
  public static class WorkflowDefinitionViewModelExtensions
  {
    /// <summary>
    /// Determines whether user has a permission to do certain workflow action.
    /// </summary>
    /// <param name="workflowDefinition">Instance of <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowDefinition" /> defining the workflow.</param>
    /// <param name="actionName">Name of the action.</param>
    /// <returns>True if user can perform the action; otherwise false.</returns>
    public static bool CanUser(
      this WorkflowDefinitionViewModel workflowDefinition,
      string actionName)
    {
      SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
      WorkflowLevelViewModel workflowLevelViewModel = workflowDefinition.Levels.Where<WorkflowLevelViewModel>((Func<WorkflowLevelViewModel, bool>) (wp => wp.ActionName == actionName)).FirstOrDefault<WorkflowLevelViewModel>();
      if (workflowLevelViewModel != null)
      {
        foreach (WorkflowPermissionViewModel permissionViewModel in workflowLevelViewModel.Permissions.Where<WorkflowPermissionViewModel>((Func<WorkflowPermissionViewModel, bool>) (p => p.ActionName == actionName)))
        {
          if (permissionViewModel.PrincipalType == WorkflowPrincipalType.User && permissionViewModel.Id == currentIdentity.UserId)
            return true;
          Guid id = Guid.Parse(permissionViewModel.PrincipalId);
          if (permissionViewModel.PrincipalType == WorkflowPrincipalType.Role && currentIdentity.Roles.Any<RoleInfo>((Func<RoleInfo, bool>) (r => r.Id == id)))
            return true;
        }
      }
      return false;
    }
  }
}
