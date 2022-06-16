// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.IWorkflowDefinitionsCache
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;

namespace Telerik.Sitefinity.Workflow
{
  internal interface IWorkflowDefinitionsCache
  {
    /// <summary>
    /// Returns workflow definition scopes ordered by priority and last modified date.
    /// </summary>
    /// <returns>All workflow scopes from cache</returns>
    IEnumerable<WorkflowTypeScopeProxy> GetAllScopes();

    /// <summary>
    /// Returns workflow definitions ordered by priority and last modified date.
    /// </summary>
    /// <returns>TODO workflow document return</returns>
    IEnumerable<WorkflowDefinitionProxy> GetAll();
  }
}
