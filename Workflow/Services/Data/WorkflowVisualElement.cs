// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.Services.Data.WorkflowVisualElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Workflow.UI;

namespace Telerik.Sitefinity.Workflow.Services.Data
{
  [DataContract]
  public class WorkflowVisualElement
  {
    [DataMember]
    public virtual string Title { get; set; }

    [DataMember]
    public virtual string OperationName { get; set; }

    [DataMember]
    public virtual string ArgumentDialogName { get; set; }

    [DataMember]
    public virtual WorkflowVisualType VisualType { get; set; }

    [DataMember]
    public virtual string DecisionType { get; set; }

    [DataMember]
    public virtual bool PersistOnDecision { get; set; }

    [DataMember]
    public virtual bool ClosesForm { get; set; }

    [DataMember]
    public virtual string ContentCommandName { get; set; }

    [DataMember]
    public virtual string CssClass { get; set; }

    [DataMember]
    public virtual int Ordinal { get; set; }

    /// <summary>
    /// Warning to be displayed on the client in monolingual mode
    /// </summary>
    [DataMember]
    public string WarningMessage { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether [run as normal command].
    /// </summary>
    /// <value><c>true</c> if [run as normal command]; otherwise, <c>false</c>.</value>
    [DataMember]
    public virtual bool RunAsUICommand { get; set; }

    /// <summary>
    /// Gets or sets a value indicating optional parameters for visual element
    /// </summary>
    [DataMember]
    public virtual Hashtable Parameters { get; set; }
  }
}
