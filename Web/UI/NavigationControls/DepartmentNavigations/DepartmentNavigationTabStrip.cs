// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.NavigationControls.DepartmentNavigations.DepartmentNavigationTabStrip
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.NavigationControls.DepartmentNavigations
{
  /// <summary>
  /// A navigation control used to create tabbed interfaces.
  /// </summary>
  public class DepartmentNavigationTabStrip : RadTabStrip, IExpandableDepartmentControl
  {
    /// <summary>
    /// Handles the <see cref="E:System.Web.UI.Control.Init" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      if (this.NavigationAction != NavigationAction.OnMouseOver)
        return;
      this.OnClientMouseOver = "function (sender, eventArgs) {" + ("if (sender._defaultSelectedIndex === undefined) {" + "sender._defaultSelectedIndex = sender.get_selectedIndex();" + "}" + "if (sender._revertToDefaultSelectionTimer) {" + "clearTimeout(sender._revertToDefaultSelectionTimer);" + "}") + "eventArgs.get_tab().select();" + "}";
      this.OnClientMouseOut = "function (sender, eventArgs) {" + "if (sender._defaultSelectedIndex !== undefined) {" + ("sender._revertToDefaultSelectionTimer = setTimeout(" + (object) '"' + ("if ($find('\" + sender.get_id() + \"').get_selectedIndex() === \" + sender.get_selectedIndex() + \") {" + "$find('\" + sender.get_id() + \"').set_selectedIndex($find('\" + sender.get_id() + \"')._defaultSelectedIndex);" + "}") + (object) '"' + (object) ',' + (object) 750 + ");") + "}" + "}";
    }

    /// <summary>Gets or sets the allow collapsing.</summary>
    /// <value>The allow collapsing.</value>
    public bool AllowCollapsing { get; set; }

    /// <summary>Gets or sets the show expanded.</summary>
    /// <value>The show expanded.</value>
    public bool ShowExpanded { get; set; }

    /// <summary>
    /// Gets or sets how many the levels to expand 0 - for all levels.
    /// </summary>
    /// <value>The levels to expand.</value>
    public int LevelsToExpand { get; set; }

    /// <summary>Gets or sets the navigation action.</summary>
    /// <value>The navigation action.</value>
    public NavigationAction NavigationAction { get; set; }

    /// <summary>Gets or sets the current page URL.</summary>
    /// <value>The current page URL.</value>
    public string CurrentPageURL { get; set; }
  }
}
