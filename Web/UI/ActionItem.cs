// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ActionItem
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Defines a single action item that can be displayed by the DecisionScreen control
  /// </summary>
  public class ActionItem
  {
    private bool visible = true;

    /// <summary>Gets or sets the title of the action item.</summary>
    public string Title { get; set; }

    /// <summary>Gets or sets the css class of the action item.</summary>
    public string CssClass { get; set; }

    /// <summary>
    /// Gets or sets the url where the user should be navigated upon clicking on the action item.
    /// </summary>
    public string NavigateUrl { get; set; }

    /// <summary>
    /// Gets or sets the client side function to be called upon clicking on the action item.
    /// </summary>
    public string OnClientClick { get; set; }

    /// <summary>
    /// Gets or sets the name of the command that distinguished this action item from other.
    /// </summary>
    public string CommandName { get; set; }

    /// <summary>
    /// Gets or sets the client pageId of the action item link.
    /// </summary>
    public string LinkClientId { get; internal set; }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="T:Telerik.Sitefinity.Web.UI.ActionItem" /> is visible.
    /// </summary>
    /// <value><c>true</c> if visible; otherwise, <c>false</c>.</value>
    public bool Visible
    {
      get => this.visible;
      internal set => this.visible = value;
    }
  }
}
