// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.IWorkflowExecutionDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Telerik.Sitefinity.Workflow.Model;

namespace Telerik.Sitefinity.Workflow
{
  /// <summary>Holds the parameters for a workflow execution.</summary>
  public interface IWorkflowExecutionDefinition
  {
    /// <summary>Gets the id of the workflow definition.</summary>
    Guid Id { get; }

    /// <summary>Gets the given name of the workflow.</summary>
    string Title { get; }

    /// <summary>
    /// Gets the type of the workflow (1-step, 2-step, 3-step, no-approval-needed, custom)
    /// </summary>
    WorkflowType WorkflowType { get; }

    /// <summary>
    /// Gets a value indicating whether this instance is active.
    /// </summary>
    bool IsActive { get; }

    /// <summary>
    /// Gets a value indicating whether users with administrative privileges
    /// publish items directly.
    /// </summary>
    bool AllowAdministratorsToSkipWorkflow { get; }

    /// <summary>
    /// Gets a value indicating whether users in the last level
    /// publish items directly.
    /// </summary>
    bool AllowPublishersToSkipWorkflow { get; }

    /// <summary>
    /// Gets a path to a *.xamlx file which will be used to override
    /// the default. Can be null.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Xamlx is ok.")]
    string CustomXamlxUrl { get; }

    /// <summary>Gets the workflow scopes.</summary>
    IEnumerable<IWorkflowExecutionScope> Scopes { get; }

    /// <summary>Gets the workflow levels.</summary>
    IEnumerable<IWorkflowExecutionLevel> Levels { get; }

    /// <summary>Gets a value indicating whether notes are allowed.</summary>
    bool AllowNotes { get; }
  }
}
