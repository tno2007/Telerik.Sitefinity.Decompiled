// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.WorkflowCommandWidgetRestrictionStrategy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Restriction;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Workflow.Model;

namespace Telerik.Sitefinity.Workflow
{
  internal class WorkflowCommandWidgetRestrictionStrategy : CommandWidgetRestrictionStrategyBase
  {
    protected virtual IWorkflowDefinitionsCache WorkflowCache => (IWorkflowDefinitionsCache) WorkflowDefinitionsCache.GetCache();

    public override bool IsRestricted(ICommandWidgetDefinition def, Type contentType)
    {
      WorkflowType[] commandWorkflowTypes = this.GetCommandWorkflowTypes(def.CommandName);
      return commandWorkflowTypes != null && !this.CheckExistanceOfActiveWorkflowDefinitions(contentType, commandWorkflowTypes);
    }

    protected virtual WorkflowType[] GetCommandWorkflowTypes(string command) => DefinitionsHelper.AllowedWorkflowTypesForCommandMap.ContainsKey(command) ? DefinitionsHelper.AllowedWorkflowTypesForCommandMap[command] : (WorkflowType[]) null;

    private bool CheckExistanceOfActiveWorkflowDefinitions(
      Type contentType,
      params WorkflowType[] definitionTypes)
    {
      return this.GetActiveWorkflows(contentType).Any<IWorkflowExecutionDefinition>((Func<IWorkflowExecutionDefinition, bool>) (d => ((IEnumerable<WorkflowType>) definitionTypes).Any<WorkflowType>((Func<WorkflowType, bool>) (t => d.WorkflowType == t))));
    }

    private IEnumerable<IWorkflowExecutionDefinition> GetActiveWorkflows(
      Type contentType)
    {
      IEnumerable<IWorkflowExecutionDefinition> executionDefinitions = (IEnumerable<IWorkflowExecutionDefinition>) new List<IWorkflowExecutionDefinition>();
      return (!(contentType != (Type) null) ? (IEnumerable<IWorkflowExecutionDefinition>) this.WorkflowCache.GetAll() : this.WorkflowCache.GetAllScopes().Where<WorkflowTypeScopeProxy>((Func<WorkflowTypeScopeProxy, bool>) (s => s.ContentType == contentType.FullName || string.IsNullOrEmpty(s.ContentType))).Select<WorkflowTypeScopeProxy, IWorkflowExecutionDefinition>((Func<WorkflowTypeScopeProxy, IWorkflowExecutionDefinition>) (s => s.Scope.Definition))).Where<IWorkflowExecutionDefinition>((Func<IWorkflowExecutionDefinition, bool>) (d => d.IsActive));
    }
  }
}
