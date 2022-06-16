// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.IWorkflowExecutionScope
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;

namespace Telerik.Sitefinity.Workflow
{
  /// <summary>Holds the parameters for a workflow scope.</summary>
  public interface IWorkflowExecutionScope
  {
    /// <summary>Gets the id of the workflow definition.</summary>
    Guid Id { get; }

    /// <summary>Gets the workflow definition.</summary>
    IWorkflowExecutionDefinition Definition { get; }

    /// <summary>Gets the language cultures.</summary>
    IEnumerable<string> Cultures { get; }

    /// <summary>Gets the site id.</summary>
    Guid SiteId { get; }

    /// <summary>Gets the last modified.</summary>
    DateTime LastModified { get; }

    /// <summary>Gets the workflow type scopes.</summary>
    IList<WorkflowTypeScopeProxy> TypeScopes { get; }
  }
}
