// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts.IColumnDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts
{
  /// <summary>
  /// An interface which provides all information needed to construct the column.
  /// </summary>
  public interface IColumnDefinition : IDefinition
  {
    /// <summary>Gets or sets the name of the column.</summary>
    /// <remarks>
    /// This name has to be unique inside of a collection of columns.
    /// </remarks>
    string Name { get; set; }

    /// <summary>
    /// Determines the css class of the header for this data item.
    /// </summary>
    /// <value>The css class of the header.</value>
    string HeaderCssClass { get; set; }

    /// <summary>Determines the header text for this data item.</summary>
    /// <value>The header text.</value>
    string HeaderText { get; set; }

    /// <summary>Determines the resource class name</summary>
    /// <value>The reasource class name.</value>
    string ResourceClassId { get; set; }

    /// <summary>Determines the header text for this data item.</summary>
    /// <value>The header text.</value>
    string TitleText { get; set; }

    /// <summary>Determines the css class for the item.</summary>
    /// <value>The item css class.</value>
    string ItemCssClass { get; set; }

    /// <summary>
    /// Determines the width of the item (when displayed as a column).
    /// </summary>
    /// <value>The width.</value>
    int Width { get; set; }

    bool? DisableSorting { get; set; }

    /// <summary>
    /// Gets or sets the sort expression for the column in ItemsGrid
    /// </summary>
    string SortExpression { get; set; }
  }
}
