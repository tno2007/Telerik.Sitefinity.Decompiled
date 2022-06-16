// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.NavigationControls.SiteMapNavigations.SiteMapNavigationMenu
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
  /// A navigation control used to display a menu in a web page.
  /// </summary>
  public class SiteMapNavigationMenu : 
    RadMenu,
    IExpandableSiteMapControl,
    IHighlightPathSiteMapControl,
    IGroupPageNavigationSiteMapControl
  {
    internal const string clientScriptPath = "Telerik.Sitefinity.Web.UI.NavigationControls.SiteMapNavigations.Scripts.SiteMapNavigationMenu.js";
    internal const string PageIdAttribute = "Sitefinity.PageGUID";

    /// <summary>
    /// Handles the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      if (this.Page != null)
        this.Page.ClientScript.RegisterClientScriptResource(this.GetType(), "Telerik.Sitefinity.Web.UI.NavigationControls.SiteMapNavigations.Scripts.SiteMapNavigationMenu.js");
      RadMenuItem itemByAttribute = this.FindItemByAttribute("Sitefinity.PageGUID", this.CurrentPageNodeId.ToString().ToUpperInvariant());
      if (itemByAttribute == null || !this.HighlightPath)
        return;
      itemByAttribute.HighlightPath();
    }

    /// <summary>
    /// Handles the <see cref="E:System.Web.UI.Control.Init" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      if (!this.GroupPageIsLink)
        this.OnClientItemClicked = "function(sender, args) {" + "var item = args.get_item();" + "if (!item.get_navigateUrl() || item.get_navigateUrl()==\"#\") { " + "if (item.get_isOpen()) " + "item.close();" + "else " + "item.open();" + "}" + "}";
      this.ItemDataBound += new RadMenuEventHandler(this.SiteMapNavigationMenu_ItemDataBound);
      if (this.NavigationAction != NavigationAction.OnClick)
        return;
      this.ClickToOpen = true;
      this.OnClientItemClicked = "radMenuOnClick";
      this.OnClientItemOpening = "radMenuOnOpening";
    }

    protected virtual void SiteMapNavigationMenu_ItemDataBound(object sender, RadMenuEventArgs e)
    {
      this.SetSelectedItem((SiteMapNode) e.Item.DataItem, (Action) (() => e.Item.Selected = true));
      NavigationUtilities.SetNavigationItemTarget((NavigationItem) e.Item);
      if (!(this.DataSource is SiteMapDataSource))
        return;
      SiteMapNode dataItem1 = e.Item.DataItem as SiteMapNode;
      if (this.NavigationAction == NavigationAction.OnClick)
      {
        e.Item.Attributes.Add("ExpandOnClick", "false");
        int num1 = dataItem1.Depth();
        int num2 = this.MaxDataBindDepth + ((SiteMapDataSource) this.DataSource).RootDepth();
        int num3 = num2;
        if ((num1 < num3 || num2 <= 0) && ((SiteMapNode) e.Item.DataItem).ChildNodes.Count != 0)
          e.Item.NavigateUrl = "";
      }
      e.Item.Attributes["Sitefinity.PageGUID"] = dataItem1.Key;
      if (this.GroupPageIsLink || !((SiteMapNode) e.Item.DataItem is PageSiteNode dataItem2) || !dataItem2.IsGroupPage)
        return;
      e.Item.CssClass = "sfNoGroupPageNavigation";
      e.Item.NavigateUrl = "";
    }

    /// <summary>Find item by attribute.</summary>
    /// <param name="attributeName">attribute name ("Sitefinity.PageGUID")</param>
    /// <param name="attributeValue">GUID of the page pointed by the MenuItem</param>
    /// <returns></returns>
    public RadMenuItem FindItemByAttribute(string attributeName, string attributeValue) => this.FindChildByAttribute<RadMenuItem>(attributeName, attributeValue);

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

    /// <summary>
    /// Gets or sets whether the GroupPage to be a link or will expand/collapse on click.
    /// </summary>
    public bool GroupPageIsLink { get; set; }

    /// <summary>Gets or sets whether to highlight MenuItem's path</summary>
    public bool HighlightPath { get; set; }

    /// <summary>Gets or sets guid of the current page</summary>
    public Guid CurrentPageNodeId { get; set; }
  }
}
