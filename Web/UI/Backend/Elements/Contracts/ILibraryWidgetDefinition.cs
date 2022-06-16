// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts.ILibraryWidgetDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts
{
  /// <summary>
  /// Represents the definition contract for the LibraryWidget definitions and config elements
  /// </summary>
  public interface ILibraryWidgetDefinition : 
    IContentItemWidgetDefinition,
    IWidgetDefinition,
    IDefinition
  {
    /// <summary>
    /// Gets or sets a value indicating whether the action menu with commands about the displayed library should be visible.
    /// </summary>
    /// <value><c>true</c> if the action menu with commands about the displayed library is visible; otherwise, <c>false</c>.</value>
    bool ShowActionMenu { get; set; }

    /// <summary>Gets or sets the name of a single item in a library.</summary>
    /// <value>The name of the item.</value>
    string ItemName { get; set; }

    /// <summary>
    /// Gets or sets the name of the items in a library in plural.
    /// </summary>
    /// <value>The name of the items.</value>
    string ItemsName { get; set; }

    /// <summary>Gets or sets the name of the library.</summary>
    /// <value>The name of the library.</value>
    string LibraryName { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether widget supports reordering.
    /// </summary>
    /// <value><c>true</c> if widget supports reordering; otherwise, <c>false</c>.</value>
    bool SupportsReordering { get; set; }
  }
}
