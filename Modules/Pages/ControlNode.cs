// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.ControlNode
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Modules.Pages
{
  /// <summary>
  /// A class that will be used to organize layouts(placeholders) and controls when render a page
  /// in a hierarchical presentation.
  /// </summary>
  public class ControlNode
  {
    private List<ControlNode> children;
    private ControlNodeType nodeType;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.ControlNode" /> class.
    /// </summary>
    public ControlNode()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.ControlNode" /> class.
    /// </summary>
    /// <param name="control">The control.</param>
    public ControlNode(ControlData control) => this.Control = control;

    /// <summary>Gets the sibling id.</summary>
    /// <value>The sibling id.</value>
    public Guid SiblingId => this.Control.SiblingId;

    /// <summary>Gets or sets a reference to all child nodes.</summary>
    /// <value>The children.</value>
    public List<ControlNode> Children
    {
      get
      {
        if (this.children == null)
          this.children = new List<ControlNode>();
        return this.children;
      }
      private set => this.children = value;
    }

    /// <summary>Gets or sets the data of the node.</summary>
    /// <value>The control.</value>
    public ControlData Control { get; set; }

    /// <summary>
    /// Gets or sets the index of the container (template, page) at which template belongs.
    /// </summary>
    /// <value>The index of the container.</value>
    public int ContainerIndex { get; set; }

    /// <summary>Gets or sets the type of the node.</summary>
    /// <value>The type of the node.</value>
    public ControlNodeType NodeType
    {
      get => this.nodeType;
      set => this.nodeType = value;
    }
  }
}
