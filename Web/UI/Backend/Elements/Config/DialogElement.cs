// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Config.DialogElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Config
{
  /// <summary>
  /// Represents a configuration element for Sitefinity Dialog elemet.
  /// </summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "DialogDescription", Title = "DialogTitle")]
  [DebuggerDisplay("DialogElement {Name}, OpenOnCommandName={OpenOnCommandName}")]
  public class DialogElement : DefinitionConfigElement, IDialogDefinition, IDefinition
  {
    private string controlDefinitionName;
    private string viewName;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Config.DialogElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public DialogElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions.DialogDefinition((ConfigElement) this);

    public string ControlDefinitionName
    {
      get => this.controlDefinitionName;
      set => this.controlDefinitionName = value;
    }

    public string ViewName
    {
      get => this.viewName;
      set => this.viewName = value;
    }

    /// <summary>
    /// Artificial unique key - combination of dialog's name and command name.
    /// </summary>
    [ConfigurationProperty("id", IsKey = true)]
    public string ID
    {
      get => (string) this["id"];
      set => this["id"] = (object) value;
    }

    private void SetID() => this.ID = DialogElement.ConstructDialogIDFrom(this.Name, this.OpenOnCommandName);

    /// <summary>Gets or sets the name.</summary>
    /// <value>The name.</value>
    [ConfigurationProperty("name", IsRequired = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DialogNameDescription", Title = "DialogNameCaption")]
    public string Name
    {
      get => this["name"] as string;
      set
      {
        this["name"] = (object) value;
        this.SetID();
      }
    }

    /// <summary>
    /// Gets or sets the name of the command that will fire this dialog.
    /// </summary>
    /// <value>The name of the command.</value>
    [ConfigurationProperty("openOnCommand", IsRequired = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DialogCommandNameDescription", Title = "DialogCommandNameCaption")]
    public string OpenOnCommandName
    {
      get => this["openOnCommand"] as string;
      set
      {
        this["openOnCommand"] = (object) value;
        this.SetID();
      }
    }

    /// <summary>Gets or sets the URL.</summary>
    /// <value>The URL.</value>
    public string NavigateUrl => this.GetNavigateUrl(this.Name, this.Parameters);

    /// <summary>
    /// Gets or sets the height of the window. Default is 100%
    /// </summary>
    /// <value>The height.</value>
    [ConfigurationProperty("height")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DialogHeightDescription", Title = "DialogHeightCaption")]
    public Unit Height
    {
      get => (Unit) (this["height"] ?? (object) Unit.Percentage(100.0));
      set => this["height"] = (object) value;
    }

    /// <summary>Gets or sets the width of the window. Default is 100%</summary>
    /// <value>The width.</value>
    [ConfigurationProperty("width")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DialogWidthDescription", Title = "DialogWidthCaption")]
    public Unit Width
    {
      get => (Unit) (this["width"] ?? (object) Unit.Percentage(100.0));
      set => this["width"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the initial behavior. Default is Maximize.
    /// </summary>
    /// <value>The initial behavior.</value>
    [ConfigurationProperty("initialBehaviors", DefaultValue = WindowBehaviors.Default)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DialogInitialBehaviorsDescription", Title = "DialogInitialBehaviorsCaption")]
    public WindowBehaviors InitialBehaviors
    {
      get => (WindowBehaviors) this["initialBehaviors"];
      set => this["initialBehaviors"] = (object) value;
    }

    /// <summary>Gets or sets the behaviors. Default is None.</summary>
    /// <value>The behaviors.</value>
    [ConfigurationProperty("behaviors", DefaultValue = WindowBehaviors.Default)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DialogBehaviorsDescription", Title = "DialogBehaviorsCaption")]
    public WindowBehaviors Behaviors
    {
      get => (WindowBehaviors) this["behaviors"];
      set => this["behaviors"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the autoSizeBehaviors. Default is Default.
    /// </summary>
    /// <value>The autoSizeBehaviors.</value>
    [ConfigurationProperty("autoSizeBehaviors", DefaultValue = WindowAutoSizeBehaviors.Default)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DialogAutoSizeBehaviorsDescription", Title = "DialogAutoSizeBehaviorsCaption")]
    public WindowAutoSizeBehaviors AutoSizeBehaviors
    {
      get => (WindowAutoSizeBehaviors) this["autoSizeBehaviors"];
      set => this["autoSizeBehaviors"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is full screen.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is full screen; otherwise, <c>false</c>.
    /// </value>
    [ConfigurationProperty("isfullscreen", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DialogIsFullScreenDescription", Title = "DialogIsFullScreenCaption")]
    public bool IsFullScreen
    {
      get => (bool) this["isfullscreen"];
      set => this["isfullscreen"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether [visible status bar].
    /// </summary>
    /// <value><c>true</c> if [visible status bar]; otherwise, <c>false</c>.</value>
    [ConfigurationProperty("visiblestatusbar", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DialogVisibleStatusBarDescription", Title = "DialogVisibleStatusBarCaption")]
    public bool VisibleStatusBar
    {
      get => (bool) this["visiblestatusbar"];
      set => this["visiblestatusbar"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether [visible tool bar].
    /// </summary>
    /// <value><c>true</c> if [visible tool bar]; otherwise, <c>false</c>.</value>
    [ConfigurationProperty("visibletitlebar", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DialogVisibleTitleBarDescription", Title = "DialogVisibleTitleBarCaption")]
    public bool VisibleTitleBar
    {
      get => (bool) this["visibletitlebar"];
      set => this["visibletitlebar"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a collection of querystring-like parameters to pass to the dialog callback function.
    /// </summary>
    /// <value>The params.</value>
    [ConfigurationProperty("params")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DialogParametersDescription", Title = "DialogParametersCaption")]
    public string Parameters
    {
      get => this["params"] as string;
      set => this["params"] = (object) value;
    }

    /// <summary>Gets or sets the skin.</summary>
    /// <value>The skin.</value>
    [ConfigurationProperty("skin")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DialogSkinDescription", Title = "DialogSkinCaption")]
    public string Skin
    {
      get => this["skin"] as string;
      set => this["skin"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is modal.
    /// </summary>
    /// <value><c>true</c> if this instance is modal; otherwise, <c>false</c>.</value>
    [ConfigurationProperty("ismodal", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DialogIsModalDescription", Title = "DialogIsModalCaption")]
    public bool IsModal
    {
      get => (bool) this["ismodal"];
      set => this["ismodal"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the dialog's window will be disposed and made
    /// inaccessible once it is closed.
    /// If property is set to true, the next time the dialog is requested,
    /// a new window with default settings is created and returned.
    /// </summary>
    /// <value>The default value is <strong>false</strong>.</value>
    [ConfigurationProperty("destroyOnClose", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DialogDestroyOnCloseDescription", Title = "DialogDestroyOnCloseCaption")]
    public bool? DestroyOnClose
    {
      get => (bool?) this["destroyOnClose"];
      set => this["destroyOnClose"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the page that is loaded in the dialog's window
    /// should be loaded everytime from the server or
    /// will leave the browser default behaviour.
    /// </summary>
    /// <value>The default value is <strong>false</strong>.</value>
    [ConfigurationProperty("ReloadOnShow", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DialogReloadOnShowDescription", Title = "DialogReloadOnShowCaption")]
    public bool? ReloadOnShow
    {
      get => (bool?) this[nameof (ReloadOnShow)];
      set => this[nameof (ReloadOnShow)] = (object) value;
    }

    /// <summary>Gets or sets the CSS class.</summary>
    /// <value>The CSS class.</value>
    [ConfigurationProperty("cssclass")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DialogCssClassDescription", Title = "DialogCssClassCaption")]
    public string CssClass
    {
      get => this["cssclass"] as string;
      set => this["cssclass"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the dialog is added to the black list.
    /// </summary>
    /// <value></value>
    [ConfigurationProperty("isBlackListed")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "IsBlackListedDescription", Title = "IsBlackListedCaption")]
    public bool? IsBlackListed
    {
      get => (bool?) this["isBlackListed"];
      set => this["isBlackListed"] = (object) value;
    }

    protected internal static string ConstructDialogIDFrom(
      string dialogName,
      string openOnCommandName)
    {
      return string.Format("{0} on {1}", (object) dialogName, (object) openOnCommandName);
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct DialogProps
    {
      public const string ID = "id";
      public const string Name = "name";
      public const string OpenOnCommandName = "openOnCommand";
      public const string Url = "url";
      public const string Height = "height";
      public const string Ordinal = "ordinal";
      public const string Width = "width";
      public const string InitialBehaviors = "initialBehaviors";
      public const string Behaviors = "behaviors";
      public const string AutoSizeBehaviors = "autoSizeBehaviors";
      public const string IsFullScreen = "isfullscreen";
      public const string VisibleStatusBar = "visiblestatusbar";
      public const string VisibleTitleBar = "visibletitlebar";
      public const string Parameters = "params";
      public const string Skin = "skin";
      public const string IsModal = "ismodal";
      public const string DestroyOnClose = "destroyOnClose";
      public const string ReloadOnShow = "ReloadOnShow";
      public const string CssClass = "cssclass";
      public const string IsBlackListed = "isBlackListed";
    }
  }
}
