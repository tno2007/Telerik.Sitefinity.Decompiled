// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.Activities.ActivitiesHelper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Activities;
using System.ComponentModel;
using Telerik.Sitefinity.Workflow.Model;

namespace Telerik.Sitefinity.Workflow.Activities
{
  /// <summary>Utility methods for use by activities.</summary>
  public static class ActivitiesHelper
  {
    /// <summary>
    /// Extracts the <see cref="T:Telerik.Sitefinity.Workflow.IWorkflowExecutionDefinition" /> from activity data context,
    /// with fallback to obsolete keys.
    /// </summary>
    /// <param name="context">Activity's data context</param>
    /// <returns>Extracted IWorkflowExecutionDefinition</returns>
    public static IWorkflowExecutionDefinition ExecutionDefinitionFromDataContext(
      WorkflowDataContext context)
    {
      PropertyDescriptor property = context.GetProperties()["workflowExecutionDefinition"];
      return property != null ? (IWorkflowExecutionDefinition) property.GetValue((object) context) : (IWorkflowExecutionDefinition) new WorkflowDefinitionProxy((WorkflowDefinition) context.GetProperties()["workflowDefinition"].GetValue((object) context));
    }
  }
}
