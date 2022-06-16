// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts.IDialogDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts
{
  /// <summary>
  /// An interface that provides all common properties to construct a Sitefinity dialog.
  /// </summary>
  public interface IDialogDefinition : IDefinition
  {
    /// <summary>Gets or sets the name of the control definition.</summary>
    /// <value>The name of the control definition.</value>
    string ControlDefinitionName { get; set; }

    /// <summary>Gets or sets the name of the view.</summary>
    /// <value>The name of the view.</value>
    string ViewName { get; set; }

    /// <summary>Gets or sets the name.</summary>
    /// <value>The name.</value>
    string Name { get; set; }

    /// <summary>
    /// Gets or sets the name of the command that will cause the dialog defined by this definition to be opened.
    /// </summary>
    string OpenOnCommandName { get; set; }

    /// <summary>Gets or sets the URL.</summary>
    /// <value>The URL.</value>
    string NavigateUrl { get; }

    /// <summary>
    /// Gets or sets the height of the window. Default is 100%
    /// </summary>
    /// <value>The height.</value>
    Unit Height { get; set; }

    /// <summary>Gets or sets the width of the window. Default is 100%</summary>
    /// <value>The width.</value>
    Unit Width { get; set; }

    /// <summary>
    /// Gets or sets the initial behavior. Default is Maximize.
    /// </summary>
    /// <value>The initial behavior.</value>
    WindowBehaviors InitialBehaviors { get; set; }

    /// <summary>Gets or sets the behaviors. Default is None.</summary>
    /// <value>The behaviors.</value>
    WindowBehaviors Behaviors { get; set; }

    /// <summary>Gets or sets the behaviors. Default is None.</summary>
    /// <value>The behaviors.</value>
    WindowAutoSizeBehaviors AutoSizeBehaviors { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is full screen.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is full screen; otherwise, <c>false</c>.
    /// </value>
    bool IsFullScreen { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether [visible status bar].
    /// </summary>
    /// <value><c>true</c> if [visible status bar]; otherwise, <c>false</c>.</value>
    bool VisibleStatusBar { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether [visible title bar].
    /// </summary>
    /// <value><c>true</c> if [visible tool bar]; otherwise, <c>false</c>.</value>
    bool VisibleTitleBar { get; set; }

    /// <summary>
    /// Gets or sets a collection of querystring-like parameters to pass to the dialog callback function.
    /// </summary>
    /// <value>The params.</value>
    string Parameters { get; set; }

    /// <summary>Gets or sets the skin.</summary>
    /// <value>The skin.</value>
    string Skin { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is modal.
    /// </summary>
    /// <value><c>true</c> if this instance is modal; otherwise, <c>false</c>.</value>
    bool IsModal { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the dialog's window will be disposed and made
    /// inaccessible once it is closed.
    /// If property is set to true, the next time the dialog is requested,
    /// a new window with default settings is created and returned.
    /// </summary>
    /// <value>The default value is <strong>false</strong>.</value>
    bool? DestroyOnClose { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the page that is loaded in the dialog's window
    /// should be loaded everytime from the server or
    /// will leave the browser default behaviour.
    /// </summary>
    /// <value>The default value is <strong>false</strong>.</value>
    bool? ReloadOnShow { get; set; }

    /// <summary>Gets or sets the CSS class.</summary>
    /// <value>The CSS class.</value>
    string CssClass { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the dialog is added to the black list.
    /// </summary>
    bool? IsBlackListed { get; set; }
  }
}
