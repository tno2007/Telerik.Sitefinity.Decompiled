// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.WorkflowLevelProxy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Workflow.Model;

namespace Telerik.Sitefinity.Workflow
{
  /// <summary>
  /// Wrapper for <see cref="T:Telerik.Sitefinity.Workflow.IWorkflowExecutionLevel" />
  /// </summary>
  public class WorkflowLevelProxy : IWorkflowExecutionLevel
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Workflow.WorkflowLevelProxy" /> class.
    /// Copies relevant fields from a <see cref="T:Telerik.Sitefinity.Workflow.Model.WorkflowLevel" /> instance.
    /// </summary>
    /// <param name="wl">Workflow level as defined in the database.</param>
    public WorkflowLevelProxy(WorkflowLevel wl)
    {
      this.Id = wl.Id;
      this.ActionName = wl.ActionName;
      this.Ordinal = wl.Ordinal;
      this.NotifyApprovers = wl.NotifyApprovers;
      this.NotifyAdministrators = wl.NotifyAdministrators;
      this.CustomEmailRecipients = (IEnumerable<string>) wl.CustomEmailRecipients;
      List<WorkflowExecutionPermissionProxy> executionPermissionProxyList = new List<WorkflowExecutionPermissionProxy>();
      foreach (WorkflowPermission permission in (IEnumerable<WorkflowPermission>) wl.Permissions)
        executionPermissionProxyList.Add(new WorkflowExecutionPermissionProxy(permission));
      this.Permissions = (IEnumerable<IWorkflowExecutionPermission>) executionPermissionProxyList;
    }

    /// <inheritdoc />
    public IEnumerable<string> CustomEmailRecipients { get; private set; }

    /// <inheritdoc />
    public Guid Id { get; private set; }

    /// <inheritdoc />
    public string ActionName { get; private set; }

    /// <inheritdoc />
    public float Ordinal { get; private set; }

    /// <inheritdoc />
    public bool NotifyApprovers { get; private set; }

    /// <inheritdoc />
    public bool NotifyAdministrators { get; private set; }

    /// <inheritdoc />
    public IEnumerable<IWorkflowExecutionPermission> Permissions { get; private set; }
  }
}
