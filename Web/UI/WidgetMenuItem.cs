// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.WidgetMenuItem
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Menu items that will be displayed in the context menu inside the dock elements in the ZoneEditor.
  /// </summary>
  public class WidgetMenuItem
  {
    /// <summary>Gets or sets the text displayed for the command.</summary>
    /// <value>The text.</value>
    public string Text { set; get; }

    /// <summary>
    /// Gets or sets the name of the command that will be invoked.
    /// </summary>
    /// <value>The name of the command.</value>
    public string CommandName { set; get; }

    /// <summary>
    /// Gets or sets the url of the action that will be invoked on click.
    /// </summary>
    /// <value>The name of the action.</value>
    public string ActionUrl { set; get; }

    /// <summary>Gets or sets the CSS class.</summary>
    /// <value>The CSS class.</value>
    public string CssClass { set; get; }

    /// <summary>
    /// Gets or sets a value indicating whether the content should be loaded in a modal window.
    /// </summary>
    /// <value>
    ///   <c>true</c> if the content should be added inside modal; otherwise, <c>false</c>.
    /// </value>
    public bool NeedsModal { set; get; }
  }
}
