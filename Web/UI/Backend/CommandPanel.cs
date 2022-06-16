// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.CommandPanel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web.UI.Templates;
using Telerik.Web;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.Backend
{
  /// <summary>
  /// Represents a command panel for a Control Panel (<see cref="T:Telerik.Sitefinity.Web.UI.Backend.IControlPanel" />) implementation.
  /// </summary>
  [ClientScriptResource("Telerik.Sitefinity.Web.UI.Backend.CommandPanel", "Telerik.Sitefinity.Web.Scripts.ControlPanelCommon.js")]
  public class CommandPanel : RadWebControl, ICommandPanel
  {
    private int selectedIndex = -1;
    private bool? hasDescription;
    private string name;
    private string title;
    private ITemplate layoutTemplate;
    private CommandPanel.LayoutContainer container;
    private IControlPanel controlPanel;
    private IList<CommandItem> commands;
    private Control[] commandElements;
    private IPartialRouteHandler routeHandler;
    private const string selectedCssClass = "sfSel";
    private const string clickFunction = "commandPanels['{0}'].execute({1});";
    /// <summary>The name of the layout template.</summary>
    public static readonly string DefinitionListTemplate = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.CmdPanelDefList.ascx");
    /// <summary>The name of the layout template.</summary>
    public static readonly string UnorderedListTemplate = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.CmdPanelUnorderdList.ascx");

    /// <summary>Initializes new instance of CommandPanel class.</summary>
    public CommandPanel() => this.EnableEmbeddedSkins = false;

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets or sets the index of the selected command item within the pannel.
    /// </summary>
    public virtual int SelectedItemIndex
    {
      get => this.selectedIndex;
      set => this.ViewState[nameof (SelectedItemIndex)] = (object) value;
    }

    /// <summary>Gets a list of defined commands for this panel.</summary>
    public virtual IList<CommandItem> Commands
    {
      get
      {
        if (this.commands == null)
          this.commands = (IList<CommandItem>) new List<CommandItem>();
        return this.commands;
      }
    }

    /// <summary>
    /// Gets or sets the path to a custom layout template for the control.
    /// </summary>
    [DescriptionResource(typeof (TemplateDescriptions), "CommandPanelLayout")]
    public virtual string LayoutTemplatePath
    {
      get
      {
        string layouteTemplatePath = this.ViewState[nameof (LayoutTemplatePath)] as string;
        if (string.IsNullOrEmpty(this.LayoutTemplateName) && string.IsNullOrEmpty(layouteTemplatePath))
          layouteTemplatePath = this.DefaultLayouteTemplatePath;
        return layouteTemplatePath;
      }
      set => this.ViewState[nameof (LayoutTemplatePath)] = (object) value;
    }

    /// <summary>
    /// Value of LayoutTemplatePath if both LayoutTemplatePath and LayoutTemplateName are not set (null or empty)
    /// </summary>
    protected virtual string DefaultLayouteTemplatePath => this.HasDescription ? CommandPanel.DefinitionListTemplate : CommandPanel.UnorderedListTemplate;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    public virtual string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path to a custom layout template for the control.
    /// </summary>
    [Browsable(false)]
    [PersistenceMode(PersistenceMode.InnerProperty)]
    [DefaultValue(typeof (ITemplate), "")]
    [DescriptionResource(typeof (PageResources), "LayoutTemplateDescription")]
    [TemplateContainer(typeof (CommandPanel))]
    public virtual ITemplate LayoutTemplate
    {
      get
      {
        if (this.layoutTemplate == null)
          this.layoutTemplate = this.CreateLayoutTemplate();
        return this.layoutTemplate;
      }
      set => this.layoutTemplate = value;
    }

    /// <summary>
    /// Gets an instance of <see cref="T:Telerik.Sitefinity.Web.UI.Backend.CommandPanel.LayoutContainer" /> object that will be used as
    /// container control for the layout template.
    /// </summary>
    protected virtual CommandPanel.LayoutContainer Container
    {
      get
      {
        if (this.container == null)
        {
          this.container = this.CreateContainer();
          this.LayoutTemplate.InstantiateIn((Control) this.container);
        }
        return this.container;
      }
    }

    /// <summary>
    /// Determines if any of the commands defined for this panel has description.
    /// If the default template is used and there is present description,
    /// the control will render definition list else as an ordered list.
    /// </summary>
    protected virtual bool HasDescription
    {
      get
      {
        if (!this.hasDescription.HasValue)
        {
          this.hasDescription = new bool?(false);
          foreach (CommandItem command in (IEnumerable<CommandItem>) this.Commands)
          {
            if (!string.IsNullOrEmpty(command.Description))
            {
              this.hasDescription = new bool?(true);
              break;
            }
          }
        }
        return this.hasDescription.Value;
      }
    }

    /// <summary>
    /// Gets or sets the global resource class ID to use for localized strings.
    /// If this property is set the Title is assumed to
    /// contain resource keys instead of actual value.
    /// </summary>
    public virtual string ResourceClassId { get; set; }

    /// <summary>
    /// Initializes all controls instantiated in the layout container.
    /// This method is called at appropriate time for setting initial values and
    /// subscribing for events of layout controls.
    /// </summary>
    protected virtual void InitializeControls()
    {
      if (this.Commands.Count > 0)
      {
        this.routeHandler = this.ControlPanel as IPartialRouteHandler;
        this.commandElements = new Control[this.Commands.Count];
        this.Container.Repeater.ItemDataBound += new RepeaterItemEventHandler(this.Repeater_ItemDataBound);
        this.Container.Repeater.DataSource = (object) this.Commands;
        this.Container.Repeater.DataBind();
      }
      if (this.Container.Title == null)
        return;
      Control title = this.Container.Title;
      title.Visible = !string.IsNullOrEmpty(this.Title);
      if (!title.Visible)
        return;
      if (title is ITextControl textControl)
        textControl.Text = this.Title;
      else
        title.Controls.Add((Control) new LiteralControl(this.Title));
    }

    /// <summary>
    /// Called by the ASP.NET page framework to notify server
    /// controls that use composition-based implementation to
    /// create any child controls they contain in preparation for posting back or rendering.
    /// </summary>
    protected override void CreateChildControls()
    {
      this.container = (CommandPanel.LayoutContainer) null;
      this.Controls.Clear();
      this.InitializeControls();
      this.Controls.Add((Control) this.Container);
    }

    /// <summary>
    /// Creates an instance of container control.
    /// The container must inherit from <see cref="T:Telerik.Sitefinity.Web.UI.GenericContainer" />.
    /// </summary>
    /// <returns>
    /// An instance of <see cref="T:Telerik.Sitefinity.Web.UI.GenericContainer" /> object.
    /// </returns>
    protected virtual CommandPanel.LayoutContainer CreateContainer() => new CommandPanel.LayoutContainer();

    /// <summary>
    /// Creates a layout template from a specified
    /// user control (external template) or embedded resource.
    /// </summary>
    /// <returns>
    /// An instance of <see cref="T:System.Web.UI.ITemplate" /> object.
    /// </returns>
    protected virtual ITemplate CreateLayoutTemplate() => ControlUtilities.GetTemplate(new TemplateInfo()
    {
      TemplatePath = this.LayoutTemplatePath,
      TemplateName = this.LayoutTemplateName,
      ControlType = this.GetType()
    });

    /// <summary>
    /// Describes an object to a ScriptComponentDescriptor based on its reflected properties and methods
    /// </summary>
    /// <param name="descriptor">The script descriptor to fill.</param>
    protected override void DescribeComponent(IScriptDescriptor descriptor)
    {
      descriptor.AddProperty("name", (object) this.Name);
      descriptor.AddProperty("selectedCssClass", (object) "sfSel");
      descriptor.AddProperty("selectedItem", (object) this.SelectedItemIndex);
      if (this.Commands.Count > 0)
      {
        StringBuilder stringBuilder = new StringBuilder();
        for (int index = 0; index < this.commandElements.Length; ++index)
        {
          stringBuilder.Append(this.commandElements[index].ClientID);
          if (index < this.commandElements.Length - 1)
            stringBuilder.Append(",");
        }
        JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
        descriptor.AddProperty("items", (object) scriptSerializer.Serialize((object) this.Commands));
        descriptor.AddProperty("commandElements", (object) stringBuilder.ToString());
      }
      base.DescribeComponent(descriptor);
    }

    /// <summary>Reference to the control panel tied to the command panel instance.</summary>
    /// <remarks>
    /// This property is used for communication between the command panel and its control
    /// panel.
    /// </remarks>
    /// <example>
    ///     You can refer to <see cref="T:Telerik.Sitefinity.Web.UI.Backend.ICommandPanel">ICommandPanel</see> interface for more
    ///     complicated example implementing the whole
    ///     <see cref="T:Telerik.Sitefinity.Web.UI.Backend.ICommandPanel">ICommandPanel</see> interface.
    /// </example>
    public IControlPanel ControlPanel
    {
      get => this.controlPanel;
      set => this.controlPanel = value;
    }

    /// <summary>Name for the command panel.</summary>
    /// <value>Command panel name.</value>
    /// <remarks>
    /// Could be used as command panel identifier if there are more than one command
    /// panels for a module.
    /// </remarks>
    /// <example>
    ///     You can refer to <see cref="T:Telerik.Sitefinity.Web.UI.Backend.ICommandPanel">ICommandPanel</see> interface for more
    ///     complicated example implementing the whole
    ///     <see cref="T:Telerik.Sitefinity.Web.UI.Backend.ICommandPanel">ICommandPanel</see> interface.
    /// </example>
    public string Name
    {
      get => this.name;
      set => this.name = value;
    }

    /// <summary>Title of the command panel.</summary>
    public string Title
    {
      get
      {
        if (string.IsNullOrEmpty(this.ResourceClassId))
          return this.title ?? this.name;
        if (string.IsNullOrEmpty(this.title))
          this.title = this.name + nameof (Title);
        return Res.Get(this.ResourceClassId, this.title);
      }
      set => this.title = value;
    }

    /// <summary>Item data bound handler</summary>
    /// <param name="sender"><see cref="T:System.Web.UI.WebControls.Repeater" /></param>
    /// <param name="e"><see cref="T:System.Web.UI.WebControls.RepeaterItemEventArgs" /></param>
    protected virtual void Repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem && e.Item.ItemType != ListItemType.SelectedItem)
        return;
      CommandItem dataItem = (CommandItem) e.Item.DataItem;
      if (!(e.Item.FindControl("command") is HyperLink control1))
        throw new TemplateException(string.IsNullOrEmpty(this.LayoutTemplatePath) ? (this.HasDescription ? CommandPanel.DefinitionListTemplate : CommandPanel.UnorderedListTemplate) : this.LayoutTemplatePath, typeof (HyperLink).FullName, "command");
      control1.Text = dataItem.Title;
      HtmlGenericControl control2 = e.Item.FindControl("defTerm") as HtmlGenericControl;
      HtmlGenericControl control3 = e.Item.FindControl("listItem") as HtmlGenericControl;
      if (control2 != null)
        control2.Attributes["class"] = dataItem.CssClass;
      else if (control3 != null)
        control3.Attributes["class"] = dataItem.CssClass;
      else
        control1.CssClass = dataItem.CssClass;
      if (dataItem.Selected || e.Item.ItemIndex == this.SelectedItemIndex)
      {
        control1.CssClass += control1.CssClass.Length == 0 ? "sfSel" : " sfSel";
        this.selectedIndex = e.Item.ItemIndex;
      }
      control1.NavigateUrl = string.IsNullOrEmpty(dataItem.NavigateUrl) ? (this.routeHandler == null ? "javascript:void(0);" : RouteHelper.GetVirtualPath(this.routeHandler, dataItem.GetRouteInfo())) : dataItem.NavigateUrl;
      control1.ToolTip = dataItem.Description;
      if (!string.IsNullOrEmpty(dataItem.ClientFunction))
      {
        string str = string.Format((IFormatProvider) CultureInfo.InvariantCulture, "commandPanels['{0}'].execute({1});", (object) dataItem.PanelName, (object) e.Item.ItemIndex);
        if (dataItem.CancelNavigation)
          str += "return false;";
        control1.Attributes["onclick"] = str;
      }
      this.commandElements[e.Item.ItemIndex] = (Control) control1;
      if (!(e.Item.FindControl("description") is ITextControl control4))
        return;
      if (!string.IsNullOrEmpty(dataItem.Description))
        control4.Text = dataItem.Description;
      else
        ((Control) control4).Visible = false;
    }

    /// <summary>
    /// Represents the layout template conatainer for <see cref="T:Telerik.Sitefinity.Web.UI.Backend.CommandPanel" />.
    /// </summary>
    protected class LayoutContainer : GenericContainer
    {
      /// <summary>Required control; type: Repeater, ID: commandList</summary>
      public virtual Repeater Repeater => this.GetControl<Repeater>("commandsList", true);

      /// <summary>Optional control; type: Control, ID: title</summary>
      public virtual Control Title => this.GetControl<Control>("title", false);
    }
  }
}
