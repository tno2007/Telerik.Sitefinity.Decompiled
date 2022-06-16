// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Toolbar.DropDownToolboxItem
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Web.UI.Toolbar
{
  /// <summary>
  /// Drop-down toolbox item that can be added to an <see cref="!:ItemsList" />
  /// </summary>
  [ParseChildren(true)]
  [EditorBrowsable(EditorBrowsableState.Never)]
  public class DropDownToolboxItem : ToolboxItemBase
  {
    private static readonly string LayoutTemplateName = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Buttons.DropDownToolboxItem.ascx");
    private ITemplate itemTemplate;
    private GenericContainer container;
    private List<ListItem> items;

    /// <inheritdoc />
    public override ITemplate ItemTemplate
    {
      get
      {
        if (this.itemTemplate == null)
          this.itemTemplate = ControlUtilities.GetTemplate(DropDownToolboxItem.LayoutTemplateName, (string) null, Config.Get<ControlsConfig>().ResourcesAssemblyInfo, (string) null);
        return this.itemTemplate;
      }
      set => this.itemTemplate = value;
    }

    /// <summary>
    /// Gets the collection of list items offered by the drop-down toolbox item.
    /// </summary>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public List<ListItem> Items
    {
      get
      {
        if (this.items == null)
          this.items = new List<ListItem>();
        return this.items;
      }
    }

    /// <inheritdoc />
    public override string ItemType => typeof (DropDownToolboxItem).Name;

    /// <summary>Gets or sets the name of the command.</summary>
    public string CommandName { get; set; }

    /// <summary>Gets or sets the text of the command.</summary>
    public string Text { get; set; }

    internal string DropDownClientId => this.container.GetControl<DropDownList>("dropDown", true).ClientID;

    internal Control GenerateItem()
    {
      if (this.container == null)
      {
        this.container = new GenericContainer();
        this.ItemTemplate.InstantiateIn((Control) this.container);
        Label control1 = this.container.GetControl<Label>("textLabel", true);
        if (!string.IsNullOrEmpty(this.Text))
          control1.Text = this.Text;
        else
          control1.Visible = false;
        DropDownList control2 = this.container.GetControl<DropDownList>("dropDown", true);
        foreach (ListItem listItem in this.Items)
          control2.Items.Add(listItem);
      }
      return (Control) this.container;
    }
  }
}
