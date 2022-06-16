// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions.DialogDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Diagnostics;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Config;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions
{
  /// <summary>Represents a definition of the Sitefinity dialog</summary>
  [ParseChildren(true)]
  [DebuggerDisplay("DialogDefinition {Name}, OpenOnCommandName={OpenOnCommandName}")]
  public class DialogDefinition : DefinitionBase, IDialogDefinition, IDefinition
  {
    private string controlDefinitionName;
    private string viewName;
    private string dialogName;
    private WindowBehaviors? initialBehaviors;
    private WindowBehaviors? behaviors;
    private WindowAutoSizeBehaviors? autoSizeBehaviors;
    private string parameters;
    private string skin = "Default";
    private Unit? width;
    private Unit? height;
    private string name;
    private string openOnCommandName;
    private bool isFullScreen;
    private bool visibleStatusBar;
    private bool visibleTitlebar;
    private bool isModal;
    private bool? destroyOnClose;
    private bool? reloadOnShow;
    private string cssClass;
    private bool? isBlackListed;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions.DialogDefinition" /> class.
    /// </summary>
    public DialogDefinition()
      : base((ConfigElement) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions.DialogDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public DialogDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public DialogDefinition GetDefinition() => this;

    /// <summary>Gets or sets the name of the control definition.</summary>
    /// <value>The name of the control definition.</value>
    public string ControlDefinitionName
    {
      get => this.controlDefinitionName;
      set => this.controlDefinitionName = value;
    }

    /// <summary>
    /// Gets or sets the name of the view. Used for resolving property values.
    /// </summary>
    /// <value>The name of the view.</value>
    public string ViewName
    {
      get => this.viewName;
      set => this.viewName = value;
    }

    /// <summary>Gets or sets the name of the dialog.</summary>
    /// <value>The name of the dialog.</value>
    public string DialogName
    {
      get => this.dialogName;
      set => this.dialogName = value;
    }

    /// <summary>Gets or sets the name.</summary>
    /// <value>The name.</value>
    public string Name
    {
      get => this.ResolveProperty<string>(nameof (Name), this.name);
      set => this.name = value;
    }

    /// <summary>
    /// Gets or sets the name of the command that will cause the dialog defined by this definition
    /// to be opened.
    /// </summary>
    /// <value>Name of the command.</value>
    public string OpenOnCommandName
    {
      get => this.ResolveProperty<string>(nameof (OpenOnCommandName), this.openOnCommandName);
      set => this.openOnCommandName = value;
    }

    /// <summary>Gets or sets the URL.</summary>
    /// <value>The URL.</value>
    public string NavigateUrl => this.GetNavigateUrl(this.Name, this.Parameters);

    /// <summary>
    /// Gets or sets the height of the window. Default is 100%
    /// </summary>
    /// <value>The height.</value>
    public Unit Height
    {
      get
      {
        if (!this.height.HasValue)
          this.height = this.ConfigDefinition == null ? new Unit?(new Unit(100.0, UnitType.Percentage)) : new Unit?(((DialogElement) this.ConfigDefinition).Height);
        return this.height.Value;
      }
      set => this.height = new Unit?(value);
    }

    /// <summary>Gets or sets the width of the window. Default is 100%</summary>
    /// <value>The width.</value>
    public Unit Width
    {
      get
      {
        if (!this.width.HasValue)
          this.width = this.ConfigDefinition == null ? new Unit?(new Unit(100.0, UnitType.Percentage)) : new Unit?(((DialogElement) this.ConfigDefinition).Width);
        return this.width.Value;
      }
      set => this.width = new Unit?(value);
    }

    /// <summary>
    /// Gets or sets the initial behavior. Default is Maximize.
    /// </summary>
    /// <value>The initial behavior.</value>
    public WindowBehaviors InitialBehaviors
    {
      get
      {
        if (!this.initialBehaviors.HasValue)
          this.initialBehaviors = this.ConfigDefinition == null ? new WindowBehaviors?(WindowBehaviors.Maximize) : new WindowBehaviors?(((DialogElement) this.ConfigDefinition).InitialBehaviors);
        return this.initialBehaviors.Value;
      }
      set => this.initialBehaviors = new WindowBehaviors?(value);
    }

    /// <summary>Gets or sets the behaviors. Default is None.</summary>
    /// <value>The behaviors.</value>
    public WindowBehaviors Behaviors
    {
      get
      {
        if (!this.behaviors.HasValue)
          this.behaviors = this.ConfigDefinition == null ? new WindowBehaviors?(WindowBehaviors.None) : new WindowBehaviors?(((DialogElement) this.ConfigDefinition).Behaviors);
        return this.behaviors.Value;
      }
      set => this.behaviors = new WindowBehaviors?(value);
    }

    /// <summary>
    /// Gets or sets the autoSizeBehaviors. Default is Default.
    /// </summary>
    /// <value>The behaviors.</value>
    public WindowAutoSizeBehaviors AutoSizeBehaviors
    {
      get
      {
        if (!this.autoSizeBehaviors.HasValue)
          this.autoSizeBehaviors = this.ConfigDefinition == null ? new WindowAutoSizeBehaviors?(WindowAutoSizeBehaviors.Width | WindowAutoSizeBehaviors.Height) : new WindowAutoSizeBehaviors?(((DialogElement) this.ConfigDefinition).AutoSizeBehaviors);
        return this.autoSizeBehaviors.Value;
      }
      set => this.autoSizeBehaviors = new WindowAutoSizeBehaviors?(value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is full screen.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is full screen; otherwise, <c>false</c>.
    /// </value>
    public bool IsFullScreen
    {
      get => this.ResolveProperty<bool>(nameof (IsFullScreen), this.isFullScreen);
      set => this.isFullScreen = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether [visible status bar].
    /// </summary>
    /// <value><c>true</c> if [visible status bar]; otherwise, <c>false</c>.</value>
    public bool VisibleStatusBar
    {
      get => this.ResolveProperty<bool>(nameof (VisibleStatusBar), this.visibleStatusBar);
      set => this.visibleStatusBar = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether [visible title bar].
    /// </summary>
    /// <value><c>true</c> if [visible title bar]; otherwise, <c>false</c>.</value>
    public bool VisibleTitleBar
    {
      get => this.ResolveProperty<bool>(nameof (VisibleTitleBar), this.visibleTitlebar);
      set => this.visibleTitlebar = value;
    }

    /// <summary>
    /// Gets or sets a collection of querystring-like parameters to pass to the dialog callback function.
    /// </summary>
    /// <value>The params.</value>
    public string Parameters
    {
      get => this.ResolveProperty<string>(nameof (Parameters), this.parameters);
      set => this.parameters = value;
    }

    /// <summary>Gets or sets the skin.</summary>
    /// <value>The skin.</value>
    public string Skin
    {
      get => this.ResolveProperty<string>(nameof (Skin), this.skin);
      set => this.skin = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is modal.
    /// </summary>
    /// <value><c>true</c> if this instance is modal; otherwise, <c>false</c>.</value>
    public bool IsModal
    {
      get => this.ResolveProperty<bool>(nameof (IsModal), this.isModal);
      set => this.isModal = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the dialog's window will be disposed and made
    /// inaccessible once it is closed.
    /// If property is set to true, the next time the dialog is requested,
    /// a new window with default settings is created and returned.
    /// </summary>
    /// <value>The default value is <strong>false</strong>.</value>
    public bool? DestroyOnClose
    {
      get => this.ResolveProperty<bool?>(nameof (DestroyOnClose), this.destroyOnClose, new bool?(false));
      set => this.destroyOnClose = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the page that is loaded in the dialog's window
    /// should be loaded everytime from the server or
    /// will leave the browser default behaviour.
    /// </summary>
    /// <value>The default value is <strong>false</strong>.</value>
    public bool? ReloadOnShow
    {
      get => this.ResolveProperty<bool?>(nameof (ReloadOnShow), this.reloadOnShow, new bool?(false));
      set => this.reloadOnShow = value;
    }

    /// <summary>Gets or sets the CSS class.</summary>
    /// <value>The CSS class.</value>
    public string CssClass
    {
      get => this.ResolveProperty<string>(nameof (CssClass), this.cssClass);
      set => this.cssClass = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the dialog is added to the black list.
    /// </summary>
    /// <value></value>
    public bool? IsBlackListed
    {
      get => this.ResolveProperty<bool?>(nameof (IsBlackListed), this.isBlackListed);
      set => this.isBlackListed = value;
    }

    protected override ConfigElement GetConfigurationDefinition() => string.IsNullOrEmpty(this.controlDefinitionName) ? (ConfigElement) null : (ConfigElement) ((MasterGridViewElement) Telerik.Sitefinity.Configuration.Config.Get<ContentViewConfig>().ContentViewControls[this.controlDefinitionName].ViewsConfig[this.viewName]).DialogsConfig.Where<DialogElement>((Func<DialogElement, bool>) (d => d.Name == this.dialogName)).SingleOrDefault<DialogElement>();
  }
}
