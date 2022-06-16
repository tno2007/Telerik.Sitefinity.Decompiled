// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.Services.Data.WorkflowTypeScopeViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Workflow.Services.Data
{
  /// <summary>ViewModel class for the workflow type scope.</summary>
  [DataContract]
  public class WorkflowTypeScopeViewModel
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Workflow.Services.Data.WorkflowTypeScopeViewModel" /> class.
    /// </summary>
    public WorkflowTypeScopeViewModel() => this.ContentFilter = (IList<string>) new List<string>();

    /// <summary>Gets or sets the id of the workflow scope.</summary>
    /// <value>The id of the workflow scope.</value>
    [DataMember]
    public Guid Id { get; set; }

    /// <summary>Gets or sets the title of the workflow scope.</summary>
    /// <value>The title.</value>
    [DataMember]
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the content type for which the workflow ought to be applied.
    /// </summary>
    /// <value>The type of the content.</value>
    [DataMember]
    public string ContentType { get; set; }

    /// <summary>
    /// Gets or sets the collection of content item filter expressions.
    /// </summary>
    [DataMember]
    public IList<string> ContentFilter { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to include children.
    /// </summary>
    [DataMember]
    public bool IncludeChildren { get; set; }
  }
}
