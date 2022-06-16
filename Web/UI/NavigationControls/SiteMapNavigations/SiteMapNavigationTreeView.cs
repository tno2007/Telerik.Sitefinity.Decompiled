// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.NavigationControls.SiteMapNavigations.SiteMapNavigationTreeView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.NavigationControls.SiteMapNavigations
{
  /// <summary>Displays a list of nodes in a Web Forms page.</summary>
  public class SiteMapNavigationTreeView : 
    RadTreeView,
    IExpandableSiteMapControl,
    IGroupPageNavigationSiteMapControl
  {
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

    /// <summary>Gets or sets the parent node of the current node</summary>
    public SiteMapNode ParentNode { get; set; }

    /// <summary>
    /// Gets or sets whether the GroupPage to be a link or will expand/collapse on click.
    /// </summary>
    public bool GroupPageIsLink { get; set; }

    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      if (!this.AllowCollapsing)
        this.OnClientNodeCollapsing = "function(sender, eventArgs){ eventArgs.set_cancel(true);}";
      if (!this.GroupPageIsLink)
        this.OnClientNodeClicked = "function(sender, args) {" + "var node = args.get_node();" + "if (!node._getNavigateUrl() || node._getNavigateUrl()==\"#\") " + "node.toggle();" + "}";
      this.NodeDataBound += new RadTreeViewEventHandler(this.SiteMapNavigationTreeView_NodeDataBound);
    }

    protected virtual void ExpandPredecessorsOfSelectedRadTreeNode(RadTreeNode treeNode) => this.ExpandPredecessorsOfSelectedNode(treeNode);

    protected virtual void SiteMapNavigationTreeView_NodeDataBound(
      object sender,
      RadTreeNodeEventArgs e)
    {
      this.SetSelectedItem((SiteMapNode) e.Node.DataItem, (Action) (() => e.Node.Selected = true));
      this.ExpandPredecessorsOfSelectedRadTreeNode(e.Node);
      NavigationUtilities.SetNavigationItemTarget((NavigationItem) e.Node);
      if (!(this.DataSource is SiteMapDataSource))
        return;
      e.Node.Expanded = this.LevelsToExpand == 0 || e.Node.Level < this.LevelsToExpand - 1;
      if (this.GroupPageIsLink || !((SiteMapNode) e.Node.DataItem is PageSiteNode dataItem) || !dataItem.IsGroupPage)
        return;
      e.Node.CssClass = "sfNoGroupPageNavigation";
      e.Node.NavigateUrl = "";
    }
  }
}
