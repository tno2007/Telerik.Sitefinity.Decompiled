// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ToolboxItemBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>Base class for items that are placed in the toolbox</summary>
  public abstract class ToolboxItemBase
  {
    private string itemType;
    private bool visible = true;

    /// <summary>
    /// Gets or sets the pageId of the server control in which the toolbox item ought to be
    /// instantiated
    /// </summary>
    public string ContainerId { get; set; }

    /// <summary>
    /// Name of the wrapping tag, or empty/null if you don't want a wrapper tag
    /// </summary>
    public string WrapperTagName { get; set; }

    /// <summary>
    /// If the wrapper tag has a name - this will manipulate the wrapper tag's css class
    /// </summary>
    public string WrapperTagCssClass { get; set; }

    /// <summary>Css class of the toolbox item</summary>
    public string CssClass { get; set; }

    /// <summary>
    /// If the wrapper tag has a name - this will manipulate the wrapper tag's server-side pageId
    /// </summary>
    public string WrapperTagId { get; set; }

    /// <summary>Gets or set the path or the item template</summary>
    public virtual string ItemTemplatePath { get; set; }

    /// <summary>Gets or sets the item template for the toolbox item</summary>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public virtual ITemplate ItemTemplate { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="T:Telerik.Sitefinity.Web.UI.ToolboxItemBase" /> is visible.
    /// </summary>
    /// <value><c>true</c> if visible; otherwise, <c>false</c>.</value>
    public virtual bool Visible
    {
      get => this.visible;
      set => this.visible = value;
    }

    public virtual string ItemType => this.itemType;
  }
}
