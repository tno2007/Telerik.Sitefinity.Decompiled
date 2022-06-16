// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.CommandToolboxItem
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>Base class for toolbox command items</summary>
  public class CommandToolboxItem : ToolboxItemBase, ICommandButton
  {
    public static readonly string createButtonTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Buttons.CreateButton.ascx");
    public static readonly string normalButtonTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Buttons.NormalButton.ascx");
    public static readonly string saveButtonTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Buttons.SaveButton.ascx");
    public static readonly string deleteButtonTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Buttons.DeleteButton.ascx");
    public static readonly string saveAndContinueTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Buttons.SaveAndContinueButton.ascx");
    public static readonly string cancelButtonTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Buttons.CancelButton.ascx");
    private ITemplate itemTemplate;
    private GenericContainer itemContainer;

    /// <summary>Gets or sets the name of the command.</summary>
    public string CommandName { get; set; }

    /// <summary>
    /// Gets or sets the type of the command that ought to be rendered
    /// </summary>
    public CommandType CommandType { get; set; }

    /// <summary>Gets or sets the text of the command.</summary>
    /// <remarks>
    /// This property is disregarded in case custom template for the command is supplied.
    /// </remarks>
    public string Text { get; set; }

    /// <summary>
    /// Gets or sets boolean whether validation for this command should be caused
    /// </summary>
    /// <remarks>
    /// If true, this property will cause validation on the client with JavaScript
    /// </remarks>
    public bool CausesValidation { get; set; }

    /// <summary>Gets or sets the item template for the toolbox item</summary>
    /// <value></value>
    public override ITemplate ItemTemplate
    {
      get
      {
        if (this.itemTemplate == null)
        {
          if (string.IsNullOrEmpty(this.ItemTemplatePath))
          {
            switch (this.CommandType)
            {
              case CommandType.NormalButton:
                this.ItemTemplatePath = CommandToolboxItem.normalButtonTemplatePath;
                break;
              case CommandType.Custom:
                throw new InvalidOperationException("ItemTemplate property must be declared when CommandType is set to Custom");
              case CommandType.CreateButton:
                this.ItemTemplatePath = CommandToolboxItem.createButtonTemplatePath;
                break;
              case CommandType.CancelButton:
                this.ItemTemplatePath = CommandToolboxItem.cancelButtonTemplatePath;
                break;
              case CommandType.SaveButton:
                this.ItemTemplatePath = CommandToolboxItem.saveButtonTemplatePath;
                break;
              case CommandType.SaveAndContinueButton:
                this.ItemTemplatePath = CommandToolboxItem.saveAndContinueTemplatePath;
                break;
              case CommandType.DeleteButton:
                this.ItemTemplatePath = CommandToolboxItem.deleteButtonTemplatePath;
                break;
            }
          }
          this.itemTemplate = ControlUtilities.GetTemplate(this.ItemTemplatePath, (string) null, Config.Get<ControlsConfig>().ResourcesAssemblyInfo, (string) null);
        }
        return this.itemTemplate;
      }
      set
      {
        this.itemTemplate = value;
        this.CommandType = CommandType.Custom;
      }
    }

    /// <summary>Gets the client pageId of the button control</summary>
    public string ButtonClientId => this.itemContainer.GetControl<HtmlAnchor>("buttonLink", true).ClientID;

    /// <summary>Gets the client pageId of the button text control</summary>
    public string TextClientId => this.itemContainer.GetControl<Control>("buttonText", true).ClientID;

    /// <summary>
    /// Generates the command item and returns instantiated control.
    /// </summary>
    /// <returns>Instance of the command button</returns>
    public Control GenerateCommandItem()
    {
      this.itemContainer = new GenericContainer();
      this.ItemTemplate.InstantiateIn((Control) this.itemContainer);
      if (this.CommandType != CommandType.Custom)
      {
        ITextControl control = this.itemContainer.GetControl<ITextControl>("buttonText", true);
        if (!string.IsNullOrEmpty(this.Text))
          control.Text = this.Text;
      }
      HtmlAnchor control1 = this.itemContainer.GetControl<HtmlAnchor>("buttonLink", true);
      if (!string.IsNullOrEmpty(this.CssClass))
      {
        AttributeCollection attributes = control1.Attributes;
        attributes["class"] = attributes["class"] + " " + this.CssClass;
      }
      control1.Attributes["onclick"] = "return false;";
      control1.Attributes["href"] = "javascript:void(0)";
      if (!this.Visible)
        this.itemContainer.Visible = false;
      return (Control) this.itemContainer;
    }

    internal static CommandToolboxItem FromDefinition(
      ICommandToolboxItemDefinition def)
    {
      CommandToolboxItem commandToolboxItem = new CommandToolboxItem();
      commandToolboxItem.ContainerId = def.ContainerId;
      commandToolboxItem.CssClass = def.CssClass;
      commandToolboxItem.ItemTemplatePath = def.ItemTemplatePath;
      commandToolboxItem.Visible = def.Visible;
      commandToolboxItem.WrapperTagCssClass = def.WrapperTagCssClass;
      commandToolboxItem.WrapperTagId = def.WrapperTagId;
      commandToolboxItem.WrapperTagName = def.WrapperTagName;
      commandToolboxItem.CausesValidation = def.CausesValidation;
      commandToolboxItem.CommandName = def.CommandName;
      commandToolboxItem.CommandType = def.CommandType;
      commandToolboxItem.Text = DefinitionsHelper.GetLabel(def.ResourceClassId, def.Text);
      return commandToolboxItem;
    }
  }
}
