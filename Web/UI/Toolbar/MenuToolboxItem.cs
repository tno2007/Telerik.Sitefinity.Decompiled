// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.MenuToolboxItem
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>Menu button that provides commands in form of a menu</summary>
  [ParseChildren(true)]
  public class MenuToolboxItem : ToolboxItemBase, ICommandButton
  {
    private GenericContainer itemContainer;
    private ITemplate itemTemplate;
    public static readonly string layoutTemplateName = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Buttons.MenuButton.ascx");
    private List<MenuCommandItem> commandItems;

    /// <summary>Gets or sets the item template for the toolbox item</summary>
    /// <value></value>
    public override ITemplate ItemTemplate
    {
      get
      {
        if (this.itemTemplate == null)
          this.itemTemplate = ControlUtilities.GetTemplate(MenuToolboxItem.layoutTemplateName, (string) null, Config.Get<ControlsConfig>().ResourcesAssemblyInfo, (string) null);
        return this.itemTemplate;
      }
      set => this.itemTemplate = value;
    }

    /// <summary>Gets or sets the text of the button</summary>
    public string Text { get; set; }

    /// <summary>Gets or sets the name of the command.</summary>
    /// <value></value>
    public string CommandName { get; set; }

    /// <summary>Gets the client pageId of the button control</summary>
    public string ButtonClientId => this.itemContainer != null ? this.itemContainer.GetControl<RadMenu>("menuButton", true).ClientID : throw new InvalidOperationException("CommandItem must be generated before calling this property.");

    /// <summary>
    /// Gets the collection of command items offered by the menu toolbox item.
    /// </summary>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public List<MenuCommandItem> CommandItems
    {
      get
      {
        if (this.commandItems == null)
          this.commandItems = new List<MenuCommandItem>();
        return this.commandItems;
      }
    }

    public override string ItemType => nameof (MenuToolboxItem);

    /// <summary>
    /// Generates the command item and returns instantiated control.
    /// </summary>
    /// <returns>Instance of the command button</returns>
    public Control GenerateCommandItem()
    {
      this.itemContainer = new GenericContainer();
      this.ItemTemplate.InstantiateIn((Control) this.itemContainer);
      RadMenu control = this.itemContainer.GetControl<RadMenu>("menuButton", true);
      RadMenuItem radMenuItem1 = new RadMenuItem(this.Text);
      control.Items.Add(radMenuItem1);
      foreach (MenuCommandItem commandItem in this.CommandItems)
      {
        RadMenuItem radMenuItem2 = new RadMenuItem(commandItem.Text);
        radMenuItem2.Value = commandItem.CommandName;
        radMenuItem1.Items.Add(radMenuItem2);
      }
      return (Control) this.itemContainer;
    }
  }
}
