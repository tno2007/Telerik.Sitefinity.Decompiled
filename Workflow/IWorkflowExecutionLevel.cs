// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.IWorkflowExecutionLevel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;

namespace Telerik.Sitefinity.Workflow
{
  /// <summary>Holds the parameters for a workflow execution level.</summary>
  public interface IWorkflowExecutionLevel
  {
    /// <summary>Gets the id of the workflow definition.</summary>
    Guid Id { get; }

    /// <summary>Gets the given name of the workflow level.</summary>
    string ActionName { get; }

    /// <summary>Gets the ordinal of the workflow level.</summary>
    float Ordinal { get; }

    /// <summary>
    /// Gets a value indicating whether approvers should get an email prompting
    /// that a new item is waiting for their action.
    /// </summary>
    bool NotifyApprovers { get; }

    /// <summary>
    /// Gets a value indicating whether administrators should get an email prompting
    /// that a new item is waiting for their action.
    /// </summary>
    bool NotifyAdministrators { get; }

    /// <summary>
    /// Gets a list of custom email addresses, which will receive a message
    /// </summary>
    IEnumerable<string> CustomEmailRecipients { get; }

    /// <summary>
    /// Gets a list specifying which users/roles are allowed to execute
    /// which steps of the workflow.
    /// </summary>
    IEnumerable<IWorkflowExecutionPermission> Permissions { get; }
  }
}
