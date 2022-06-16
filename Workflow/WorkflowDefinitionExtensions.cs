// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.WorkflowDefinitionExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Workflow;
using Telerik.Sitefinity.Workflow.Model;

namespace Telerik.Sitefinity
{
  /// <summary>
  /// Extension methods for the <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowDefinition" /> type.
  /// </summary>
  public static class WorkflowDefinitionExtensions
  {
    /// <summary>
    /// Determines whether user has a permission to do certain workflow action.
    /// </summary>
    /// <param name="workflowDefinition">Instance of <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowDefinition" /> defining the workflow.</param>
    /// <param name="actionName">Name of the action.</param>
    /// <returns>True if user can perform the action; otherwise false.</returns>
    public static bool CanUser(this Telerik.Sitefinity.Workflow.Model.WorkflowDefinition workflowDefinition, string actionName) => new WorkflowDefinitionProxy(workflowDefinition).CanUser(actionName);

    /// <summary>
    /// Determines whether user has a permission to do certain workflow action.
    /// </summary>
    /// <param name="workflowExecutionDefinition">Instance of <see cref="T:Telerik.Sitefinity.Workflow.IWorkflowExecutionDefinition" /> defining the workflow's execution params.</param>
    /// <param name="actionName">Name of the action.</param>
    /// <returns>Whether user is allowed to perform the action.</returns>
    public static bool CanUser(this IWorkflowExecutionDefinition wed, string actionName)
    {
      SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
      if (currentIdentity.Roles.Any<RoleInfo>((Func<RoleInfo, bool>) (r => r.Id == SecurityManager.AdminRole.Id)))
        return true;
      Guid currentUserId = SecurityManager.GetCurrentUserId();
      IWorkflowExecutionLevel workflowExecutionLevel = wed.Levels.Where<IWorkflowExecutionLevel>((Func<IWorkflowExecutionLevel, bool>) (wl => wl.ActionName == actionName)).FirstOrDefault<IWorkflowExecutionLevel>();
      if (workflowExecutionLevel != null)
      {
        foreach (IWorkflowExecutionPermission permission in workflowExecutionLevel.Permissions)
        {
          if (permission.PrincipalType == WorkflowPrincipalType.User && permission.PrincipalId == currentUserId.ToString())
            return true;
          Guid id = new Guid(permission.PrincipalId);
          if (permission.PrincipalType == WorkflowPrincipalType.Role && currentIdentity.Roles.Any<RoleInfo>((Func<RoleInfo, bool>) (r => r.Id == id)))
            return true;
        }
      }
      return false;
    }

    /// <summary>
    /// Determines whether current user is allowed to execute the last step
    /// of the workflow approval process.
    /// </summary>
    /// <param name="wed">Instance of <see cref="T:Telerik.Sitefinity.Workflow.IWorkflowExecutionDefinition" /> definining workflow's execution params.</param>
    /// <param name="itemWorkflowStatus">Current workflow status of the item.</param>
    /// <returns>Whether user is allowed to execute the last step.</returns>
    internal static bool CanUserPublish(
      this IWorkflowExecutionDefinition wed,
      string itemWorkflowStatus = null)
    {
      bool flag = false;
      if (wed.WorkflowType == WorkflowType.Default)
        flag = true;
      else if (wed.WorkflowType == WorkflowType.StandardOneStep)
        flag = (itemWorkflowStatus == null || itemWorkflowStatus.Equals("AwaitingApproval", StringComparison.OrdinalIgnoreCase)) && wed.CanUser("Approve");
      else if (wed.WorkflowType == WorkflowType.StandardTwoStep || wed.WorkflowType == WorkflowType.StandardThreeStep)
        flag = (itemWorkflowStatus == null || itemWorkflowStatus.Equals("AwaitingPublishing", StringComparison.OrdinalIgnoreCase)) && wed.CanUser("Publish");
      return flag;
    }

    internal static bool CanUserAdvance(
      this IWorkflowExecutionDefinition wed,
      string itemWorkflowStatus)
    {
      if (wed.WorkflowType == WorkflowType.Default || wed.CanUserSkip())
        return true;
      if (itemWorkflowStatus == "AwaitingReview")
        return wed.CanUser("Review");
      if (itemWorkflowStatus == "AwaitingApproval" || itemWorkflowStatus == "RejectedForPublishing")
        return wed.CanUser("Approve");
      if (itemWorkflowStatus == "AwaitingPublishing")
        return wed.CanUser("Publish");
      if (!(itemWorkflowStatus == "RejectedForApproval"))
      {
        if (itemWorkflowStatus == "Rejected" && wed.WorkflowType == WorkflowType.StandardThreeStep)
          return wed.CanUser("Review");
      }
      else if (wed.WorkflowType == WorkflowType.StandardThreeStep)
        return wed.CanUser("Approve");
      return true;
    }

    /// <summary>
    /// Determines whether current user skips the workflow approvals.
    /// </summary>
    /// <param name="wed">Instance of <see cref="T:Telerik.Sitefinity.Workflow.IWorkflowExecutionDefinition" /> definining workflow's execution params.</param>
    /// <returns>Whether user publishes directly.</returns>
    public static bool CanUserSkip(this IWorkflowExecutionDefinition wed)
    {
      if (wed.AllowAdministratorsToSkipWorkflow && ClaimsManager.IsUnrestricted())
        return true;
      return wed.AllowPublishersToSkipWorkflow && wed.CanUserPublish();
    }

    /// <summary>
    /// Creates incomplete <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowDefinition" /> from
    /// <see cref="T:Telerik.Sitefinity.Workflow.IWorkflowExecutionDefinition" />
    /// </summary>
    /// <param name="source">Where field will be taken from.</param>
    /// <returns><see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowDefinition" /> with fields that specify how workflow
    /// will be executed filled.</returns>
    internal static Telerik.Sitefinity.Workflow.Model.WorkflowDefinition FromIWorkflowExecutionDefinition(
      IWorkflowExecutionDefinition source)
    {
      Telerik.Sitefinity.Workflow.Model.WorkflowDefinition workflowDefinition = new Telerik.Sitefinity.Workflow.Model.WorkflowDefinition();
      workflowDefinition.Id = source.Id;
      workflowDefinition.Title = source.Title;
      workflowDefinition.WorkflowType = source.WorkflowType;
      workflowDefinition.AllowAdministratorsToSkipWorkflow = source.AllowAdministratorsToSkipWorkflow;
      workflowDefinition.AllowPublishersToSkipWorkflow = source.AllowPublishersToSkipWorkflow;
      workflowDefinition.CustomXamlxUrl = source.CustomXamlxUrl;
      workflowDefinition.AllowNotes = source.AllowNotes;
      workflowDefinition.IsActive = source.IsActive;
      foreach (IWorkflowExecutionScope scope in source.Scopes)
      {
        WorkflowScope workflowScope = WorkflowDefinitionExtensions.FromIWorkflowExecutionScope(scope);
        workflowDefinition.Scopes.Add(workflowScope);
      }
      foreach (IWorkflowExecutionLevel level in source.Levels)
      {
        WorkflowLevel workflowLevel = WorkflowDefinitionExtensions.FromIWorkflowExecutionLevel(level);
        workflowDefinition.Levels.Add(workflowLevel);
      }
      return workflowDefinition;
    }

    internal static WorkflowLevel FromIWorkflowExecutionLevel(
      IWorkflowExecutionLevel source)
    {
      WorkflowLevel workflowLevel = new WorkflowLevel();
      workflowLevel.Id = source.Id;
      workflowLevel.ActionName = source.ActionName;
      workflowLevel.Ordinal = source.Ordinal;
      workflowLevel.NotifyApprovers = source.NotifyApprovers;
      workflowLevel.NotifyAdministrators = source.NotifyAdministrators;
      workflowLevel.CustomEmailRecipients = (IList<string>) source.CustomEmailRecipients.ToList<string>();
      workflowLevel.Permissions = (IList<WorkflowPermission>) new List<WorkflowPermission>();
      foreach (IWorkflowExecutionPermission permission in source.Permissions)
        workflowLevel.Permissions.Add(WorkflowPermissionExtensions.FromIWorkflowExecutionPermission(permission));
      return workflowLevel;
    }

    internal static WorkflowScope FromIWorkflowExecutionScope(
      IWorkflowExecutionScope source)
    {
      WorkflowScope scope = new WorkflowScope();
      scope.Id = source.Id;
      scope.SetLanguages(source.Cultures);
      foreach (IWorkflowExecutionTypeScope typeScope in (IEnumerable<WorkflowTypeScopeProxy>) source.TypeScopes)
      {
        WorkflowTypeScope workflowTypeScope = WorkflowDefinitionExtensions.FromIWorkflowExecutionTypeScope(typeScope);
        if (workflowTypeScope != null)
          scope.TypeScopes.Add(workflowTypeScope);
      }
      return scope;
    }

    internal static WorkflowTypeScope FromIWorkflowExecutionTypeScope(
      IWorkflowExecutionTypeScope source)
    {
      if (source.ContentType.IsNullOrEmpty())
        return (WorkflowTypeScope) null;
      return new WorkflowTypeScope()
      {
        Id = source.Id,
        ContentType = source.ContentType,
        ContentFilter = source.ContentFilter
      };
    }

    internal static void UpdateWorkflowScopesOnContentFilterItemDeleted(Guid deletedItem)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      WorkflowDefinitionExtensions.\u003C\u003Ec__DisplayClass9_0 cDisplayClass90 = new WorkflowDefinitionExtensions.\u003C\u003Ec__DisplayClass9_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass90.deletedItem = deletedItem;
      WorkflowManager manager = WorkflowManager.GetManager();
      IQueryable<WorkflowTypeScope> workflowTypeScopes = manager.GetWorkflowTypeScopes();
      ParameterExpression parameterExpression;
      // ISSUE: method reference
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method reference
      Expression<Func<WorkflowTypeScope, bool>> predicate = Expression.Lambda<Func<WorkflowTypeScope, bool>>((Expression) Expression.Call(ts.ContentFilter, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (string.Contains)), new Expression[1]
      {
        (Expression) Expression.Call(cDisplayClass90.deletedItem, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.ToString)), Array.Empty<Expression>())
      }), parameterExpression);
      foreach (WorkflowTypeScope workflowTypeScope in (IEnumerable<WorkflowTypeScope>) workflowTypeScopes.Where<WorkflowTypeScope>(predicate))
      {
        IList<string> contentFilter = workflowTypeScope.GetContentFilter();
        // ISSUE: reference to a compiler-generated field
        contentFilter.Remove(cDisplayClass90.deletedItem.ToString());
        workflowTypeScope.SetContentFilter((IEnumerable<string>) contentFilter);
        if (contentFilter.Count == 0)
        {
          WorkflowScope workflowScope = workflowTypeScope.WorkflowScope;
          manager.Delete(workflowTypeScope);
          if (workflowScope.TypeScopes.Count == 0)
            manager.Delete(workflowScope);
        }
      }
      manager.SaveChanges();
    }
  }
}
