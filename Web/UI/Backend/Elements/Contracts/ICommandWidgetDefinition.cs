// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts.ICommandWidgetDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Web.UI.Backend.Elements.Enums;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts
{
  /// <summary>
  /// An interface the provides the common members for all definitions that define a widget
  /// that can fire a command.
  /// </summary>
  public interface ICommandWidgetDefinition : IWidgetDefinition, IDefinition
  {
    /// <summary>
    /// Gets or sets the name of the command that widget fires.
    /// </summary>
    string CommandName { get; set; }

    /// <summary>Gets or sets the command argument</summary>
    string CommandArgument { get; set; }

    /// <summary>
    /// Gets or sets the type of the commmand button that ought to represent the command widget
    /// </summary>
    CommandButtonType ButtonType { get; set; }

    /// <summary>
    /// Gets or sets the CSS class of the command button that ought to represent the command widget
    /// </summary>
    string ButtonCssClass { get; set; }

    /// <summary>
    /// Gets or sets the Navigate Url for the command button to redirect.
    /// </summary>
    string NavigateUrl { get; set; }

    /// <summary>
    /// Determines this command is a filter command (e.g. a filter on the sidebar)
    /// </summary>
    bool IsFilterCommand { get; set; }

    /// <summary>Gets or sets the text displayed when the mouse pointer hovers over the
    /// Web server control.
    /// </summary>
    /// <returns>The text displayed when the mouse pointer hovers over the Web server
    /// control. The default is <see cref="F:System.String.Empty" />.</returns>
    string ToolTip { get; set; }

    /// <summary>Gets or sets the provider name</summary>
    /// <returns>the provider name</returns>
    string ObjectProviderName { get; set; }

    /// <summary>
    /// Gets or set whether to redirect in new window when NavigateUrl is specified.
    /// </summary>
    bool OpenInSameWindow { get; set; }

    /// <summary>Gets or sets the condition for the item to be shown.</summary>
    /// <value>The condition.</value>
    string Condition { get; set; }
  }
}
