// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.ControlPanel`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;

namespace Telerik.Sitefinity.Web.UI.Backend
{
  /// <summary>
  ///     Base class for control panel controls. Implements
  ///     <see cref="T:Telerik.Sitefinity.Web.UI.Backend.IControlPanel" /> interface.
  /// </summary>
  /// <typeparam name="THost">
  /// Defines the host of the current instance.
  /// Usually the host is the immediate parent of the current control.
  /// If the host is at no importance, <see cref="T:System.Web.UI.Control" /> type maybe specified.
  /// </typeparam>
  public class ControlPanel<THost> : ViewModeControl<THost>, IControlPanel
    where THost : Control
  {
    private string status;
    private bool autoGenerateViewCommands = true;
    private ICommandPanel[] commandPnls;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.ControlPanel.ascx");

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Web.UI.Backend.ControlPanel`1" />.
    /// </summary>
    public ControlPanel()
      : this(true)
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Web.UI.Backend.ControlPanel`1" />.
    /// </summary>
    /// <param name="autoGenerateViewCommands">
    /// Indicates if view command should be automatically generated.
    /// </param>
    public ControlPanel(bool autoGenerateViewCommands) => this.AutoGenerateViewCommands = autoGenerateViewCommands;

    /// <summary>Outer container for command panels.</summary>
    protected virtual Control CommandPanelsArea => this.Container.GetControl<Control>(nameof (CommandPanelsArea), true);

    /// <summary>Inner container for command panels.</summary>
    protected virtual PlaceHolder CommandPanelsContainer => this.Container.GetControl<PlaceHolder>(nameof (CommandPanelsContainer), true);

    /// <summary>
    /// Gets the view container control for the current layout.
    /// This control is required.
    /// </summary>
    protected virtual PlaceHolder ControlPanelViewContainer => this.Container.GetControl<PlaceHolder>(nameof (ControlPanelViewContainer), true);

    /// <summary>
    /// Gets the
    /// This control is not required.
    /// </summary>
    [Browsable(false)]
    public ITextControl PanelTitle => this.Container.GetControl<ITextControl>(nameof (PanelTitle), false);

    /// <summary>
    /// Gets the Breadcrumb for this control panel.
    /// This control is not required.
    /// </summary>
    [Browsable(false)]
    public SiteMapPath Breadcrumb => this.Container.GetControl<SiteMapPath>(nameof (Breadcrumb), false);

    /// <summary>
    /// Gets the container for search control
    /// This control is not required.
    /// </summary>
    [Browsable(false)]
    public HtmlControl SearchControlContainer => this.Container.GetControl<HtmlControl>("searchControlContainer", false);

    /// <summary>Gets or sets the search control</summary>
    public Control SearchControl { get; set; }

    public override string Title
    {
      get => base.Title;
      set
      {
        if (this.PanelTitle.Text != value)
          this.PanelTitle.Text = value;
        base.Title = value;
      }
    }

    /// <summary>The status of the control panel.</summary>
    public virtual string Status
    {
      get => this.status;
      set => this.status = value;
    }

    /// <summary>
    /// Gets an array of command panels containing the defined commands for the current View Mode.
    /// </summary>
    public virtual ICollection<ICommandPanel> CommandPanels
    {
      get
      {
        if (this.commandPnls == null)
          this.commandPnls = this.GetCommandPanels(this.ViewMode).ToArray<ICommandPanel>();
        return (ICollection<ICommandPanel>) this.commandPnls;
      }
    }

    /// <summary>
    /// Gets or sets the path to a custom layout template for the control.
    /// </summary>
    [DescriptionResource(typeof (TemplateDescriptions), "ControlPanelLayout")]
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ControlPanel<THost>.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets the name of the embedded layout template. This property must be overridden to provide the path (key) to an embedded resource file.
    /// </summary>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Overridden. Cancels the rendering of a beginning HTML tag for the control.
    /// </summary>
    /// <param name="writer">The HtmlTextWriter object used to render the markup.</param>
    public override void RenderBeginTag(HtmlTextWriter writer)
    {
    }

    /// <summary>
    /// Overridden. Cancels the rendering of an ending HTML tag for the control.
    /// </summary>
    /// <param name="writer">The HtmlTextWriter object used to render the markup.</param>
    public override void RenderEndTag(HtmlTextWriter writer)
    {
    }

    /// <summary>
    /// Determines whether View Commands should be automatically generated
    /// </summary>
    public bool AutoGenerateViewCommands
    {
      get => this.autoGenerateViewCommands;
      set => this.autoGenerateViewCommands = value;
    }

    /// <summary>
    /// Initializes all controls instantiated in the layout container. This method is called at appropriate time for setting initial values and subscribing for events of layout controls.
    /// </summary>
    /// <param name="viewContainer">The control that will host the current view.</param>
    protected override void InitializeControls(Control viewContainer)
    {
      foreach (Control commandPanel in (IEnumerable<ICommandPanel>) this.CommandPanels)
        this.CommandPanelsContainer.Controls.Add(commandPanel);
      this.CommandPanelsArea.Visible = this.CommandPanelsContainer.Controls.Count > 0;
      if (this.PanelTitle != null)
        this.PanelTitle.Text = this.Title;
      if (this.Breadcrumb != null)
      {
        if (this is IPartialRouteHandler)
          this.Breadcrumb.Provider = (SiteMapProvider) BackendSiteMap.GetCurrentProvider();
        else
          this.Breadcrumb.Visible = false;
      }
      if (this.SearchControlContainer != null && this.SearchControl != null)
        this.SearchControlContainer.Controls.Add(this.SearchControl);
      base.InitializeControls((Control) this.ControlPanelViewContainer);
    }

    /// <summary>Gets the specified command panel.</summary>
    /// <param name="name">The name of the panel to return.</param>
    /// <returns><see cref="T:Telerik.Sitefinity.Web.UI.Backend.ICommandPanel" /></returns>
    protected ICommandPanel GetCommandPanel(string name)
    {
      if (name != null)
      {
        foreach (ICommandPanel commandPanel in (IEnumerable<ICommandPanel>) this.CommandPanels)
        {
          if (name.Equals(commandPanel.Name, StringComparison.OrdinalIgnoreCase))
            return commandPanel;
        }
      }
      return (ICommandPanel) null;
    }

    /// <summary>
    /// When overridden this method returns a list of standard Command Panels.
    /// </summary>
    /// <param name="viewMode">The view mode.</param>
    /// <param name="commandsInfo">
    /// A list of <see cref="T:Telerik.Sitefinity.Web.UI.Backend.CommandItem">ControlPanel.CommandItem</see> objects.
    /// </param>
    /// <param name="commandPanels">
    /// A list of Command Panels for this Control Panel.
    /// This list can be used to add and remove Command Panels.
    /// </param>
    /// <remarks>
    /// This method will automatically create Command Panels
    /// based on the information passed with CommandItem classes.
    /// One command panel will be created per each
    /// unique panel name specified in the collection of command items.
    /// Command panels will appear in the order they were created.
    /// </remarks>
    protected virtual void CreateStandardCommandPanels(
      string viewMode,
      IList<CommandItem> commandsInfo,
      IList<ICommandPanel> commandPanels)
    {
      if (commandsInfo == null || commandsInfo.Count <= 0)
        return;
      CommandPanel commandPanel = (CommandPanel) null;
      foreach (CommandItem commandItem in (IEnumerable<CommandItem>) commandsInfo)
      {
        CommandItem cmdItem = commandItem;
        if (commandPanel != null && commandPanel.Name != cmdItem.PanelName)
          commandPanel = (CommandPanel) commandPanels.FirstOrDefault<ICommandPanel>((Func<ICommandPanel, bool>) (e => e.Name == cmdItem.PanelName));
        if (commandPanel == null)
        {
          commandPanel = new CommandPanel();
          commandPanel.Name = cmdItem.PanelName;
          commandPanel.Title = cmdItem.PanelName;
          commandPanels.Add((ICommandPanel) commandPanel);
        }
        commandPanel.Commands.Add(cmdItem);
      }
    }

    /// <summary>
    /// When overridden this method returns an array of user controls implementing ICommandPanel interface.
    /// </summary>
    /// <param name="viewMode">ViewMode</param>
    /// <param name="userControls">A list of virtual paths to user controls implementing <see cref="T:Telerik.Sitefinity.Web.UI.Backend.ICommandPanel">ICommandPanel</see> interface.</param>
    /// <param name="list">A list of Command Panels for this Control Panel. This list can be used to add and remove Command Panels. </param>
    protected virtual void CreateCommandPanelsFromUserControls(
      string viewMode,
      IList<string> userControls,
      IList<ICommandPanel> list)
    {
      if (userControls == null || userControls.Count <= 0)
        return;
      foreach (string userControl in (IEnumerable<string>) userControls)
        list.Add((ICommandPanel) ControlUtilities.LoadControl(userControl, this.Page));
    }

    /// <summary>
    /// When overridden this method returns a list of custom Command Panels.
    /// </summary>
    /// <param name="viewMode">Specifies the current View Mode</param>
    /// <param name="list">
    /// A list of Command Panels for this Control Panel. This list can be used to add and remove Command Panels.
    /// </param>
    protected virtual void CreateCustomCommandPanels(string viewMode, IList<ICommandPanel> list)
    {
    }

    /// <summary>
    /// Collects all Command Panels from the different methods for creation,
    /// combines them in to single list, initializes them and returns the list.
    /// </summary>
    /// <param name="viewMode">Specifies the current View Mode</param>
    /// <returns>A list of command panels containing the defined commands for the current View Mode.</returns>
    /// <remarks>
    /// The methods are called in the flowing order: CreateStandardCommandPanels,
    /// CreateCommandPanelsFromUserControls, CreateCustomCommandPanels.
    /// This method can be overridden to change the order.
    /// Implementers of this method should check each Command Panel if it implements
    /// <see cref="T:Telerik.Sitefinity.Web.UI.Backend.ICommandPanel" />
    /// interface and assign this instance to ControlPanel property.
    /// </remarks>
    protected virtual IList<ICommandPanel> GetCommandPanels(string viewMode)
    {
      List<ICommandPanel> commandPanels = new List<ICommandPanel>();
      this.GenerateViewCommands((IList<ICommandPanel>) commandPanels);
      this.CreateStandardCommandPanels(viewMode, (IList<CommandItem>) null, (IList<ICommandPanel>) commandPanels);
      this.CreateCommandPanelsFromUserControls(viewMode, (IList<string>) null, (IList<ICommandPanel>) commandPanels);
      this.CreateCustomCommandPanels(viewMode, (IList<ICommandPanel>) commandPanels);
      foreach (ICommandPanel commandPanel in commandPanels)
        commandPanel.ControlPanel = (IControlPanel) this;
      return (IList<ICommandPanel>) commandPanels;
    }

    /// <summary>
    /// Generates navigation for all views if autoGenerateViewCommands option is enabled.
    /// </summary>
    /// <param name="list">A list of Command Panels for this Control Panel. This list can be used to add and remove Command Panels. </param>
    protected virtual void GenerateViewCommands(IList<ICommandPanel> list)
    {
      if (!this.AutoGenerateViewCommands || this.Views.Count <= 0)
        return;
      CommandPanel commandPanel = new CommandPanel();
      commandPanel.Name = "Views";
      foreach (IViewInfo view in (Collection<IViewInfo>) this.Views)
        ;
      list.Add((ICommandPanel) commandPanel);
    }
  }
}
