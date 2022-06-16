// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.Services.Data.WorkflowVisualElementsCollection
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Workflow.Services.Data
{
  [DataContract]
  public class WorkflowVisualElementsCollection
  {
    private IList<WorkflowVisualElement> items;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Workflow.Services.Data.WorkflowVisualElementsCollection" /> class.
    /// </summary>
    /// <param name="items">The visual element items</param>
    /// <param name="context">The context.</param>
    public WorkflowVisualElementsCollection(
      IList<WorkflowVisualElement> items,
      WorkflowVisualElementsContext context)
    {
      this.items = items;
      this.Context = context;
    }

    /// <summary>Represents a collection of visual elements</summary>
    /// <value>The items.</value>
    [DataMember]
    public IList<WorkflowVisualElement> Items
    {
      get
      {
        if (this.items == null)
          this.items = (IList<WorkflowVisualElement>) new List<WorkflowVisualElement>();
        return this.items;
      }
    }

    /// <summary>Gets or sets the context.</summary>
    /// <value>The context.</value>
    [DataMember]
    public WorkflowVisualElementsContext Context { get; set; }

    /// <summary>Gets or sets the workflow definition.</summary>
    [DataMember]
    public WcfWorkflowDefinition WorkflowDefinition { get; set; }
  }
}
