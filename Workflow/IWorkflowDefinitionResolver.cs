// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.IWorkflowDefinitionResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Workflow
{
  /// <summary>
  /// Contains method to resolve which workflow to be applied for a particular content item.
  /// </summary>
  public interface IWorkflowDefinitionResolver
  {
    /// <summary>Determines which workflow to be applied for an item.</summary>
    /// <param name="context">Item's context</param>
    /// <returns>Workflow to be applied.</returns>
    IWorkflowExecutionDefinition ResolveWorkflowExecutionDefinition(
      IWorkflowResolutionContext context);

    /// <summary>
    /// From the set maintained workflows returns one by its id.
    /// </summary>
    /// <param name="id">Definition's id</param>
    /// <returns>Workflow definition.</returns>
    IWorkflowExecutionDefinition GetWorkflowExecutionDefinition(Guid id);
  }
}
