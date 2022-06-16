// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.NavigationControls.DepartmentNavigations.DepartmentNavigationMenu
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.NavigationControls.DepartmentNavigations
{
  /// <summary>
  /// A navigation control used to display a menu in a web page.
  /// </summary>
  public class DepartmentNavigationMenu : RadMenu, IExpandableDepartmentControl
  {
    internal const string clientScriptPath = "Telerik.Sitefinity.Web.UI.NavigationControls.SiteMapNavigations.Scripts.SiteMapNavigationMenu.js";

    /// <summary>
    /// Handles the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      if (this.Page == null)
        return;
      this.Page.ClientScript.RegisterClientScriptResource(this.GetType(), "Telerik.Sitefinity.Web.UI.NavigationControls.SiteMapNavigations.Scripts.SiteMapNavigationMenu.js");
    }

    /// <summary>
    /// Handles the <see cref="E:System.Web.UI.Control.Init" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      this.ItemDataBound += new RadMenuEventHandler(this.DepartmentNavigationMenu_ItemDataBound);
      if (this.NavigationAction != NavigationAction.OnClick)
        return;
      this.ClickToOpen = true;
      this.OnClientItemClicked = "radMenuOnClick";
      this.OnClientItemOpening = "radMenuOnOpening";
    }

    public void SetSelectedItem(
      IExpandableDepartmentControl control,
      SiteMapNode node,
      Action performSelection)
    {
      if (!(node.Url == control.CurrentPageURL))
        return;
      if (node is PageSiteNode)
      {
        if ((node as PageSiteNode).NodeType == NodeType.InnerRedirect)
          return;
        performSelection();
      }
      else
        performSelection();
    }

    protected virtual void DepartmentNavigationMenu_ItemDataBound(object sender, RadMenuEventArgs e) => this.SetSelectedItem((IExpandableDepartmentControl) this, (SiteMapNode) e.Item.DataItem, (Action) (() => e.Item.Selected = true));

    /// <summary>
    /// Gets or sets a value indicating whether the control will allow collapsing.
    /// </summary>
    /// <value></value>
    public bool AllowCollapsing { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the control shows expanded levels
    /// </summary>
    /// <value><c>true</c> if [show expanded]; otherwise, <c>false</c>.</value>
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
