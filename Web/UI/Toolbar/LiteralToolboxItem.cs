// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.LiteralToolboxItem
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Represents a toolbox item that will render its template without encompassing contacts
  /// </summary>
  public class LiteralToolboxItem : ToolboxItemBase
  {
    /// <summary>
    /// Gets or sets the text to be displayed by the Literal toolbox item.
    /// </summary>
    /// <value>The text.</value>
    public string Text { get; set; }

    /// <summary>Generate a server-side control for this literal</summary>
    /// <returns>Server-side control out ot the item template</returns>
    public Control GenerateItem()
    {
      PlaceHolder container = new PlaceHolder();
      if (this.ItemTemplate != null)
      {
        this.ItemTemplate.InstantiateIn((Control) container);
        return (Control) container;
      }
      return (Control) new Literal()
      {
        Text = this.Text
      };
    }
  }
}
