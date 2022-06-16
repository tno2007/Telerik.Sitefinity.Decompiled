// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.IWorkflowExecutionTypeScope
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;

namespace Telerik.Sitefinity.Workflow
{
  /// <summary>Holds the parameters for a workflow type scope.</summary>
  public interface IWorkflowExecutionTypeScope
  {
    /// <summary>Gets the id of the workflow definition.</summary>
    Guid Id { get; }

    /// <summary>Gets the scope.</summary>
    IWorkflowExecutionScope Scope { get; }

    /// <summary>Gets a value indicating whether the scope is active.</summary>
    bool IsActive { get; }

    /// <summary>Gets the type of the content.</summary>
    string ContentType { get; }

    /// <summary>Gets the language cultures.</summary>
    IEnumerable<string> Cultures { get; }

    /// <summary>Gets the site id.</summary>
    Guid SiteId { get; }

    /// <summary>Gets the content filter.</summary>
    string ContentFilter { get; }

    /// <summary>Gets a value indicating whether to include children.</summary>
    /// <value>
    ///   <c>true</c> if include children; otherwise, <c>false</c>.
    /// </value>
    bool IncludeChildren { get; }

    /// <summary>
    /// Determines whether specified context item is in the workflow scope.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <returns>
    ///   <c>true</c> if the specified item is in the scope; otherwise, <c>false</c>.
    /// </returns>
    bool IsItemInScope(IWorkflowResolutionContext context);
  }
}
