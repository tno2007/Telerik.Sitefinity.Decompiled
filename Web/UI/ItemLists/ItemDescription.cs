// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ItemLists.ItemDescription
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI;

namespace Telerik.Sitefinity.Web.UI.ItemLists
{
  /// <summary>Class representing a data member of the item selector</summary>
  [ParseChildren(true, "Markup")]
  public class ItemDescription
  {
    /// <summary>Gets or sets the data member name</summary>
    public string Name { get; set; }

    /// <summary>Gets or sets the header label for the data member</summary>
    public string HeaderText { get; set; }

    /// <summary>
    /// Gets or sets value indicating whether this data member is a search field
    /// </summary>
    public bool IsSearchField { get; set; }

    /// <summary>
    /// Gets or sets a custom CSS class to be applied on this item
    /// </summary>
    public string ItemCssClass { get; set; }

    /// <summary>
    /// Gets or sets a custom CSS class to be applied to the header of this item
    /// </summary>
    public string HeaderCssClass { get; set; }

    /// <summary>Markup for the data member template</summary>
    [PersistenceMode(PersistenceMode.InnerDefaultProperty)]
    public string Markup { get; set; }

    public int Width { get; set; }

    public bool DisableSorting { get; set; }

    /// <summary>
    /// Gets or sets the sort expression for a column in ItemsGrid
    /// </summary>
    public string SortExpression { get; set; }
  }
}
