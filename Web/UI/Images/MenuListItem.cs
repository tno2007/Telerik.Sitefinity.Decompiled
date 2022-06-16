// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Images.MenuListItem
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Web.UI.Images
{
  /// <summary>Represents items class for the MenuList control</summary>
  public class MenuListItem
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Images.MenuListItem" /> class.
    /// </summary>
    public MenuListItem()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Images.MenuListItem" /> class.
    /// </summary>
    /// <param name="text">Text of the item</param>
    /// <param name="command">Command of the item</param>
    public MenuListItem(string text, string command)
    {
      this.Text = text;
      this.Command = command;
    }

    /// <summary>Gets or sets the text of the item</summary>
    public string Text { get; set; }

    /// <summary>Gets or sets the command of the item</summary>
    public string Command { get; set; }
  }
}
