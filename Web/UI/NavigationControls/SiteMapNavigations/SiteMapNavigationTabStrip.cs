// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.NavigationControls.SiteMapNavigations.SiteMapNavigationTabStrip
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.NavigationControls.SiteMapNavigations
{
  /// <summary>
  /// A navigation control used to create tabbed interfaces.
  /// </summary>
  public class SiteMapNavigationTabStrip : 
    RadTabStrip,
    IExpandableSiteMapControl,
    IParentPageSiteMapControl,
    IGroupPageNavigationSiteMapControl
  {
    private const string ParentNodeHighlightClass = "rtsRoot";

    /// <summary>
    /// Handles the <see cref="E:System.Web.UI.Control.Init" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      if (this.NavigationAction == NavigationAction.OnMouseOver)
      {
        this.OnClientMouseOver = "function (sender, eventArgs) {" + ("if (sender._defaultSelectedIndex === undefined) {" + "sender._defaultSelectedIndex = sender.get_selectedIndex();" + "}" + "if (sender._revertToDefaultSelectionTimer) {" + "clearTimeout(sender._revertToDefaultSelectionTimer);" + "}") + "eventArgs.get_tab().select();" + "}";
        this.OnClientMouseOut = "function (sender, eventArgs) {" + "if (sender._defaultSelectedIndex !== undefined) {" + ("sender._revertToDefaultSelectionTimer = setTimeout(" + (object) '"' + ("if ($find('\" + sender.get_id() + \"').get_selectedIndex() === \" + sender.get_selectedIndex() + \") {" + "$find('\" + sender.get_id() + \"').set_selectedIndex($find('\" + sender.get_id() + \"')._defaultSelectedIndex);" + "}") + (object) '"' + (object) ',' + (object) 750 + ");") + "}" + "}";
      }
      this.TabDataBound += new RadTabStripEventHandler(this.SiteMapNavigationTabStrip_TabDataBound);
      this.DataBound += new EventHandler(this.SiteMapNavigationTabStrip_DataBound);
    }

    private void SiteMapNavigationTabStrip_DataBound(object sender, EventArgs e)
    {
      if (this.ParentNode == null)
        return;
      RadTab radTab = new RadTab();
      radTab.Text = this.ParentNode.Title;
      radTab.Value = this.ParentNode.Title;
      radTab.NavigateUrl = this.ParentNode.Url;
      RadTab tab = radTab;
      tab.CssClass = "rtsRoot";
      this.Tabs.Insert(0, tab);
    }

    protected virtual void SiteMapNavigationTabStrip_TabDataBound(
      object sender,
      RadTabStripEventArgs e)
    {
      this.SetSelectedItem((SiteMapNode) e.Tab.DataItem, (Action) (() => e.Tab.Selected = true));
      NavigationUtilities.SetNavigationItemTarget((NavigationItem) e.Tab);
      if (!(this.DataSource is SiteMapDataSource))
        return;
      this.SelectParentTab(((SiteMapDataSource) this.DataSource).Provider.CurrentNode);
      RadTab tab = e.Tab;
      if (this.NavigationAction == NavigationAction.OnClick)
      {
        int num1 = (e.Tab.DataItem as SiteMapNode).Depth();
        int num2 = this.MaxDataBindDepth + ((SiteMapDataSource) this.DataSource).RootDepth();
        int num3 = num2;
        if ((num1 < num3 || num2 <= 0) && this.MaxDataBindDepth != 1 && ((SiteMapNode) e.Tab.DataItem).ChildNodes.Count != 0)
          tab.NavigateUrl = "";
      }
      if (this.GroupPageIsLink || !((SiteMapNode) e.Tab.DataItem is PageSiteNode dataItem) || !dataItem.IsGroupPage)
        return;
      e.Tab.CssClass = "sfNoGroupPageNavigation";
      e.Tab.NavigateUrl = "";
    }

    private void SelectParentTab(SiteMapNode currentNode)
    {
      if (currentNode == null || currentNode.ParentNode == null)
        return;
      for (SiteMapNode parentNode = currentNode.ParentNode; parentNode != null; parentNode = parentNode.ParentNode)
      {
        RadTab tab = this.Tabs.FindTab((Predicate<RadTab>) (t => t.NavigateUrl == parentNode.Url));
        if (tab != null)
          tab.Selected = true;
      }
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

    /// <summary>Gets or sets whether to show current tab's parent</summary>
    public bool ShowParentTab { get; set; }

    /// <summary>
    /// Gets or sets whether the GroupPage to be a link or will expand/collapse on click.
    /// </summary>
    public bool GroupPageIsLink { get; set; }

    /// <summary>Gets or sets the parent node of the current node</summary>
    public SiteMapNode ParentNode { get; set; }
  }
}
