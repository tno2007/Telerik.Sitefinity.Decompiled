// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Definitions.ViewModeDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Definitions
{
  /// <summary>
  /// A base class which provides all information needed to construct the column.
  /// </summary>
  public abstract class ViewModeDefinition : DefinitionBase, IViewModeDefinition, IDefinition
  {
    private string name;
    private bool? enableDragAndDrop;
    private bool? enableInitialExpanding;
    private string expandedNodesCookieName;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Definitions.ColumnDefinition" /> class.
    /// </summary>
    public ViewModeDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Definitions.ViewModeDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public ViewModeDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public ViewModeDefinition GetDefinition() => this;

    /// <summary>Gets or sets the name of the control definition.</summary>
    /// <value>The name of the control definition.</value>
    public string ControlDefinitionName { get; private set; }

    /// <summary>
    /// Gets or sets the name of the view. Used for resolving property values.
    /// </summary>
    /// <value>The name of the view.</value>
    public string ViewName { get; private set; }

    /// <summary>Gets or sets the name of the column.</summary>
    /// <value></value>
    /// <remarks>
    /// This name has to be unique inside of a collection of columns.
    /// </remarks>
    public string Name
    {
      get => this.ResolveProperty<string>(nameof (Name), this.name);
      set => this.name = value;
    }

    /// <summary>When set to true enables drag-and-drop functionality</summary>
    /// <value></value>
    public bool? EnableDragAndDrop
    {
      get => this.ResolveProperty<bool?>(nameof (EnableDragAndDrop), this.enableDragAndDrop);
      set => this.enableDragAndDrop = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to store the expansion of the tree per user.
    /// </summary>
    public bool? EnableInitialExpanding
    {
      get => this.ResolveProperty<bool?>(nameof (EnableInitialExpanding), this.enableInitialExpanding);
      set => this.enableInitialExpanding = value;
    }

    /// <summary>
    /// Gets or sets the name of the cookie that will contain the information of the expanded nodes.
    /// </summary>
    public string ExpandedNodesCookieName
    {
      get => this.ResolveProperty<string>(nameof (ExpandedNodesCookieName), this.expandedNodesCookieName);
      set => this.expandedNodesCookieName = value;
    }
  }
}
