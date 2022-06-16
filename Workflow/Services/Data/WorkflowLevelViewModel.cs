// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.Services.Data.WorkflowLevelViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Workflow.Services.Data
{
  /// <summary>ViewModel class for the workflow scope.</summary>
  [DataContract]
  public class WorkflowLevelViewModel
  {
    /// <summary>Gets or sets the id of the workflow scope.</summary>
    /// <value>The id of the workflow scope.</value>
    [DataMember]
    public Guid Id { get; set; }

    /// <summary>Gets or sets the name of the workflow level.</summary>
    /// <value>The name.</value>
    [DataMember]
    public string ActionName { get; set; }

    /// <summary>Gets or sets the ordinal of the workflow level.</summary>
    /// <value>The name.</value>
    [DataMember]
    public float Ordinal { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether level approvers ought
    /// to be notified when there is a new item waiting for their approval.
    /// </summary>
    [DataMember]
    public bool NotifyApprovers { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether administrators ought
    /// to be notified when there is a new item waiting for their approval.
    /// </summary>
    [DataMember]
    public bool NotifyAdministrators { get; set; }

    /// <summary>
    /// Gets or sets a list of additional email addresses to be notified
    /// when an item is waiting for level reviewing.
    /// </summary>
    [DataMember]
    public IList<string> CustomEmailRecipients { get; set; }

    /// <summary>
    /// Gets or sets the dictionary with workflow permissions.
    /// </summary>
    [DataMember]
    public IList<WorkflowPermissionViewModel> Permissions { get; set; }
  }
}
