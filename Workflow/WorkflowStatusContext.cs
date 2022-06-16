// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.WorkflowStatusContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Workflow
{
  /// <summary>
  /// Keeps contextual information about the current item - that will be used to hide or show visual decisions(buttons)
  /// </summary>
  public class WorkflowStatusContext
  {
    /// <summary>Gets or sets the workflow status.</summary>
    /// <value>The workflow status.</value>
    public string WorkflowStatus { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this item published.
    /// </summary>
    public bool IsItemPublished { get; set; }
  }
}
