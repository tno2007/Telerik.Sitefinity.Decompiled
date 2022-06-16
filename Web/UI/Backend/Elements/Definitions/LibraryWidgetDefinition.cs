// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions.LibraryWidgetDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions
{
  /// <summary>Represents the definition for the LibraryWidget</summary>
  public class LibraryWidgetDefinition : 
    ContentItemWidgetDefinition,
    ILibraryWidgetDefinition,
    IContentItemWidgetDefinition,
    IWidgetDefinition,
    IDefinition
  {
    private bool showActionMenu;
    private string itemName;
    private string itemsName;
    private string libraryName;
    private bool supportsReordering;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions.LibraryWidgetDefinition" /> class.
    /// </summary>
    public LibraryWidgetDefinition()
      : base((ConfigElement) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions.LibraryWidgetDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public LibraryWidgetDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) this;

    /// <summary>
    /// Gets or sets a value indicating whether the action menu with commands about the displayed library should be visible.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if the action menu with commands about the displayed library is visible; otherwise, <c>false</c>.
    /// </value>
    public bool ShowActionMenu
    {
      get => this.ResolveProperty<bool>(nameof (ShowActionMenu), this.showActionMenu);
      set => this.showActionMenu = value;
    }

    /// <summary>Gets or sets the name of a single item in a library.</summary>
    /// <value>The name of the item.</value>
    public string ItemName
    {
      get => this.ResolveProperty<string>(nameof (ItemName), this.itemName);
      set => this.itemName = value;
    }

    /// <summary>
    /// Gets or sets the name of the items in a library in plural.
    /// </summary>
    /// <value>The name of the items.</value>
    public string ItemsName
    {
      get => this.ResolveProperty<string>(nameof (ItemsName), this.itemsName);
      set => this.itemsName = value;
    }

    /// <summary>Gets or sets the name of the library.</summary>
    /// <value>The name of the library.</value>
    public string LibraryName
    {
      get => this.ResolveProperty<string>(nameof (LibraryName), this.libraryName);
      set => this.libraryName = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether widget supports reordering.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if widget supports reordering; otherwise, <c>false</c>.
    /// </value>
    public bool SupportsReordering
    {
      get => this.ResolveProperty<bool>(nameof (SupportsReordering), this.supportsReordering);
      set => this.SupportsReordering = value;
    }
  }
}
