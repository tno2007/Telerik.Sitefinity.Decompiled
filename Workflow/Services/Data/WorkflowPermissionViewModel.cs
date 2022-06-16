// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.Services.Data.WorkflowPermissionViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Workflow.Services.Data
{
  /// <summary>View model for workflow permission</summary>
  [DataContract]
  public class WorkflowPermissionViewModel
  {
    /// <summary>Gets or sets the id of the workflow permission.</summary>
    /// <value>The id of the workflow permission.</value>
    [DataMember]
    public Guid Id { get; set; }

    /// <summary>Gets or sets the name of the action.</summary>
    [DataMember]
    public string ActionName { get; set; }

    /// <summary>Gets or sets the id of the principal.</summary>
    [DataMember]
    public string PrincipalId { get; set; }

    /// <summary>Gets or sets the name of the principal.</summary>
    [DataMember]
    public string PrincipalName { get; set; }

    /// <summary>Gets or sets the type of the principal.</summary>
    [DataMember]
    public WorkflowPrincipalType PrincipalType { get; set; }
  }
}
