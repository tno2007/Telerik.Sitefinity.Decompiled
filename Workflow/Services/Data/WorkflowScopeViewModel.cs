// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.Services.Data.WorkflowScopeViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Configuration.Web;

namespace Telerik.Sitefinity.Workflow.Services.Data
{
  /// <summary>ViewModel class for the workflow scope.</summary>
  [DataContract]
  public class WorkflowScopeViewModel
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Workflow.Services.Data.WorkflowScopeViewModel" /> class.
    /// </summary>
    public WorkflowScopeViewModel()
    {
      this.Language = (IList<CultureViewModel>) new List<CultureViewModel>();
      this.TypeScopes = (IList<WorkflowTypeScopeViewModel>) new List<WorkflowTypeScopeViewModel>();
    }

    /// <summary>Gets or sets the id of the workflow scope.</summary>
    /// <value>The id of the workflow scope.</value>
    [DataMember]
    public Guid Id { get; set; }

    /// <summary>Gets or sets the language.</summary>
    [DataMember]
    public IList<CultureViewModel> Language { get; set; }

    /// <summary>Gets or sets the site identifier.</summary>
    [DataMember]
    public WorkflowSimpleSiteSelectorModel Site { get; set; }

    /// <summary>Gets or sets the workflow type scopes.</summary>
    [DataMember]
    public IList<WorkflowTypeScopeViewModel> TypeScopes { get; set; }
  }
}
