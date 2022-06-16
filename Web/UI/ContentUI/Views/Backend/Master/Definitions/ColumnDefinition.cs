// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Definitions.ColumnDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Definitions
{
  /// <summary>
  /// A base class which provides all information needed to construct the column.
  /// </summary>
  public abstract class ColumnDefinition : DefinitionBase, IColumnDefinition, IDefinition
  {
    private string name;
    private string headerCssClass;
    private string headerText;
    private string resourceClassId;
    private string titleText;
    private string itemCssClass;
    private int width;
    private bool? disableSorting;
    private string sortExpression;
    private RestrictionLevel restrictionLevel;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Definitions.ColumnDefinition" /> class.
    /// </summary>
    public ColumnDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Definitions.ViewModeDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public ColumnDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public ColumnDefinition GetDefinition() => this;

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

    /// <summary>
    /// Determines the css class of the header for this data item.
    /// </summary>
    /// <value>The css class of the header.</value>
    public string HeaderCssClass
    {
      get => this.ResolveProperty<string>(nameof (HeaderCssClass), this.headerCssClass);
      set => this.headerCssClass = value;
    }

    /// <summary>Determines the header text for this data item.</summary>
    /// <value>The header text.</value>
    public string HeaderText
    {
      get => this.ResolveProperty<string>(nameof (HeaderText), this.headerText);
      set => this.headerText = value;
    }

    /// <summary>Determines the header text for this data item.</summary>
    /// <value>The header text.</value>
    public string ResourceClassId
    {
      get => this.ResolveProperty<string>(nameof (ResourceClassId), this.resourceClassId);
      set => this.resourceClassId = value;
    }

    /// <summary>Determines the title text for this data item.</summary>
    /// <value>The title text.</value>
    public string TitleText
    {
      get => this.ResolveProperty<string>(nameof (TitleText), this.titleText);
      set => this.titleText = value;
    }

    /// <summary>Determines the css class for the item.</summary>
    /// <value>The item css class.</value>
    public string ItemCssClass
    {
      get => this.ResolveProperty<string>(nameof (ItemCssClass), this.itemCssClass);
      set => this.itemCssClass = value;
    }

    /// <summary>
    /// Determines the width of the item (when displayed as a column).
    /// </summary>
    /// <value>The width.</value>
    public int Width
    {
      get => this.ResolveProperty<int>(nameof (Width), this.width);
      set => this.width = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether sorting is disabled for the column.
    /// </summary>
    /// <value>A value indicating whether sorting is disabled.</value>
    public bool? DisableSorting
    {
      get => this.ResolveProperty<bool?>(nameof (DisableSorting), this.disableSorting);
      set => this.disableSorting = value;
    }

    /// <summary>
    /// Gets or sets the sort expression for the column in ItemsGrid
    /// </summary>
    /// <value></value>
    public string SortExpression
    {
      get => this.ResolveProperty<string>(nameof (SortExpression), this.sortExpression);
      set => this.sortExpression = value;
    }

    /// <summary>
    /// This is an abstract method that each concrete implementation of the ColumnDefinition
    /// class must implement. Namely, since our grid is bound on the client side (ItemsGrid), we don't
    /// need actual controls from these definitions, but rather only the markup.
    /// </summary>
    /// <returns></returns>
    public abstract string RenderMarkup();
  }
}
