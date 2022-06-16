// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.MainMenu
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Licensing;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Web.UI.NavigationControls;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.Backend
{
  /// <summary>Represents the main menu in the backend area</summary>
  public class MainMenu : RadMenu
  {
    private Guid backendHomePageId = Config.Get<PagesConfig>().BackendHomePageId;
    private Guid frontendHomePageId = Config.Get<PagesConfig>().HomePageId;
    private Guid workflowPageId = Guid.Parse(Config.Get<PagesConfig>().workflowPageId);
    private bool hasLicenseForWorkflow = LicenseLimitations.ValidateWorkflow(false);
    private Guid synchronizationPageId = SiteInitializer.StagingAndSyncingNodeId;
    private bool hasLicenseForSynchronization = LicenseState.CheckIsModuleLicensedInCurrentDomain("20FF9B05-F217-495D-A1B0-DD747232B0F3");
    private PageManager pageManager;
    private SiteMapNode currentNode;
    private RadMenuItem selectedItem;
    private SiteMapProvider siteMap;
    private string script = "\r\nfunction MainMenuMouseOver(sender, eventArgs) {\r\n\tsender.set_clicked(false);\r\n}\r\n";
    internal const string AlwaysVisibleGroupPageProperty = "VisibleGroupPage";

    /// <summary>
    /// Gets or sets a value indicating whether to trim pages which have ShowInNavigation property set to false.
    /// The default value is true.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if [trim non navigation pages]; otherwise, <c>false</c>.
    /// </value>
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether to trim pages which have ShowInNavigation property set to false.")]
    public bool TrimNonNavigationPages
    {
      get
      {
        object obj = this.ViewState[nameof (TrimNonNavigationPages)];
        return obj == null || (bool) obj;
      }
      set => this.ViewState[nameof (TrimNonNavigationPages)] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether disable expand on mouse over.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if [disable expand on mouse over]; otherwise, <c>false</c>.
    /// </value>
    [Category("Behavior")]
    [Description("Gets or sets a value indicating whether to disable expand on mouse over.")]
    public bool DisableExpandOnMouseOver
    {
      get
      {
        object obj = this.ViewState[nameof (DisableExpandOnMouseOver)];
        return obj != null && (bool) obj;
      }
      set => this.ViewState[nameof (DisableExpandOnMouseOver)] = (object) value;
    }

    /// <summary>
    /// Gets or sets the depth level at which the hierarchy will be flattened.
    /// </summary>
    /// <value>The depth level.</value>
    [Category("Behavior")]
    [Description("Gets or sets the depth level at which the hierarchy will be flattened.")]
    public int StartFlatLevel
    {
      get
      {
        object obj = this.ViewState[nameof (StartFlatLevel)];
        return obj != null ? (int) obj : 0;
      }
      set => this.ViewState[nameof (StartFlatLevel)] = (object) value;
    }

    /// <summary>
    /// Gets or sets the depth level till which the hierarchy will be flattened.
    /// </summary>
    /// <value>The depth level.</value>
    [Category("Behavior")]
    [Description("Gets or sets the depth level till which the hierarchy will be flattened.")]
    public int EndFlatLevel
    {
      get
      {
        object obj = this.ViewState[nameof (EndFlatLevel)];
        return obj != null ? (int) obj : 3;
      }
      set => this.ViewState[nameof (EndFlatLevel)] = (object) value;
    }

    /// <summary>Gets a reference of the page manager.</summary>
    /// <value>The page manager.</value>
    protected PageManager PageManager
    {
      get
      {
        if (this.pageManager == null)
          this.pageManager = PageManager.GetManager(Config.Get<PagesConfig>().DefaultProvider);
        return this.pageManager;
      }
    }

    /// <summary>
    /// Handles the <see cref="E:System.Web.UI.Control.Load" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains event data.</param>
    protected override void OnLoad(EventArgs e)
    {
      this.EnableViewState = false;
      if (this.DataSource == null)
      {
        this.siteMap = (SiteMapProvider) BackendSiteMap.GetCurrentProvider();
        if (this.siteMap is BackendSiteMap siteMap)
          siteMap.PreLoad();
        this.currentNode = this.siteMap.CurrentNode;
        SiteMapNode siteMapNodeFromKey = this.siteMap.FindSiteMapNodeFromKey(SiteInitializer.SitefinityNodeId.ToString());
        if (siteMapNodeFromKey != null)
        {
          foreach (SiteMapNode childNode in siteMapNodeFromKey.ChildNodes)
          {
            if ((!(childNode is PageSiteNode pageSiteNode) || pageSiteNode.ShowInNavigation) && LicenseState.SitefinityNodeAllowedInCurrentDomain((ISitefinitySiteMapNode) pageSiteNode))
            {
              RadMenuItem menuItemFromNode = this.CreateRadMenuItemFromNode(childNode);
              if (menuItemFromNode != null)
              {
                this.Items.Add(menuItemFromNode);
                menuItemFromNode.Target = NavigationUtilities.GetLinkTarget((object) pageSiteNode);
                this.CreateChildItems(menuItemFromNode, pageSiteNode, 0);
                if (!pageSiteNode.Attributes.Keys.Contains("VisibleGroupPage") && pageSiteNode.PageId == Guid.Empty && pageSiteNode.ChildNodes.Count > 0)
                  menuItemFromNode.NavigateUrl = "javascript:void(0);";
              }
            }
          }
        }
      }
      RadMenuItem control = this.selectedItem;
      if (control != null)
        control.Selected = true;
      for (; control != null; control = control.Parent as RadMenuItem)
        ControlUtilities.AddCssClass((WebControl) control, "rmSelected");
      if (this.DisableExpandOnMouseOver && this.Page != null)
      {
        this.Page.ClientScript.RegisterClientScriptBlock(typeof (MainMenu), "MainMenuScript", this.script, true);
        this.OnClientMouseOver = "MainMenuMouseOver";
      }
      this.OnClientItemClicking = "MainMenuMouseClicking";
      this.Page.ClientScript.RegisterClientScriptBlock(typeof (MainMenu), "click-script", "\r\n                function MainMenuMouseClicking(sender, eventArgs) {\r\n                    try {\r\n                        var event = new CustomEvent('menu-item-click', { detail: { sender: sender, eventArgs: eventArgs } });\r\n                        window.dispatchEvent(event);\r\n                    } catch (err) {\r\n                        \r\n                    }\r\n                }\r\n            ", true);
      base.OnLoad(e);
    }

    private void CreateChildItems(RadMenuItem parentItem, PageSiteNode parentNode, int level)
    {
      foreach (SiteMapNode childNode in parentNode.ChildNodes)
      {
        if ((!(childNode is ISitefinitySiteMapNode) || LicenseState.SitefinityNodeAllowedInCurrentDomain(childNode as ISitefinitySiteMapNode)) && (!(childNode is PageSiteNode pageSiteNode) || pageSiteNode.ShowInNavigation) && (childNode.ChildNodes.Count != 0 || pageSiteNode.NodeType != NodeType.Group))
        {
          RadMenuItem menuItemFromNode = this.CreateRadMenuItemFromNode(childNode);
          if (menuItemFromNode != null)
          {
            NameValueCollection attributes = ((ISitefinitySiteMapNode) childNode).Attributes;
            ControlUtilities.AddCssClass((WebControl) menuItemFromNode, attributes["NodeCssClass"]);
            if (level > this.StartFlatLevel && level <= this.EndFlatLevel)
              ControlUtilities.AddCssClass((WebControl) menuItemFromNode, "sfLevel" + (object) (level - this.StartFlatLevel));
            menuItemFromNode.Target = NavigationUtilities.GetLinkTarget((object) pageSiteNode);
            parentItem.Items.Add(menuItemFromNode);
            if (childNode is PageSiteNode parentNode1)
            {
              RadMenuItem parentItem1;
              if (level < this.StartFlatLevel || level >= this.EndFlatLevel)
              {
                parentItem1 = menuItemFromNode;
                this.DisableExpandOnMouseOver = false;
              }
              else
                parentItem1 = parentItem;
              this.CreateChildItems(parentItem1, parentNode1, level + 1);
            }
          }
        }
      }
      if (parentNode.Attributes.Keys.Contains("VisibleGroupPage") || parentNode.NodeType != NodeType.Group || parentItem.Items.Count != 0)
        return;
      parentItem.Visible = false;
    }

    private bool IsNodeEmpty(PageSiteNode node)
    {
      if (node.IsGroupPage)
      {
        if (!node.HasChildNodes)
          return true;
        PageSiteNode[] array = node.ChildNodes.OfType<PageSiteNode>().Where<PageSiteNode>((Func<PageSiteNode, bool>) (c => c.IsGroupPage)).ToArray<PageSiteNode>();
        if (array.Length == node.ChildNodes.Count && ((IEnumerable<PageSiteNode>) array).Where<PageSiteNode>((Func<PageSiteNode, bool>) (c => this.IsNodeEmpty(c))).Count<PageSiteNode>() == array.Length)
          return true;
      }
      return false;
    }

    /// <summary>Creates RadMenuItem from SiteMapNode object</summary>
    /// <param name="node"></param>
    /// <returns></returns>
    protected virtual RadMenuItem CreateRadMenuItemFromNode(SiteMapNode node)
    {
      bool flag = true;
      if (node is PageSiteNode pageSiteNode)
      {
        if (pageSiteNode.Id == this.workflowPageId && !this.hasLicenseForWorkflow)
          return (RadMenuItem) null;
        if (pageSiteNode.Id == this.synchronizationPageId && !this.hasLicenseForSynchronization)
          return (RadMenuItem) null;
        flag = pageSiteNode.RenderAsLink;
        if (this.TrimNonNavigationPages && !pageSiteNode.ShowInNavigation)
          return (RadMenuItem) null;
      }
      if (this.IsNodeEmpty(pageSiteNode))
        return (RadMenuItem) null;
      string url = node.Url;
      if (url.StartsWith("~"))
        url = UrlPath.ResolveUrl(url);
      RadMenuItem radMenuItem = new RadMenuItem(HttpUtility.HtmlEncode(node.Title));
      radMenuItem.NavigateUrl = url;
      radMenuItem.DataItem = (object) node;
      RadMenuItem menuItemFromNode = radMenuItem;
      if (!flag)
        menuItemFromNode.ItemTemplate = (ITemplate) new MainMenu.Template(pageSiteNode);
      if (node.Equals((object) this.currentNode) || this.currentNode == null && this.NodeIsHomePage(pageSiteNode) || pageSiteNode.NodeType == NodeType.Group && RouteHelper.GetFirstPageDataNode(pageSiteNode, true) == this.currentNode)
      {
        if (new Guid(node.Key) == SiteInitializer.HierarchicalTaxonomyPageId)
        {
          string[] urlParameters = this.GetUrlParameters();
          if (urlParameters == null)
            this.selectedItem = menuItemFromNode;
          else if (((IEnumerable<string>) urlParameters).Count<string>() > 0 && urlParameters[0].Equals("Categories", StringComparison.InvariantCultureIgnoreCase))
            this.selectedItem = menuItemFromNode;
        }
        else
          this.selectedItem = menuItemFromNode;
      }
      if (this.SelectAlwaysVisibleGroupPage(pageSiteNode))
        this.selectedItem = menuItemFromNode;
      return menuItemFromNode;
    }

    private bool NodeIsHomePage(PageSiteNode node)
    {
      if (node == null)
        return false;
      return node.Id == this.backendHomePageId || node.Id == this.frontendHomePageId;
    }

    private bool SelectAlwaysVisibleGroupPage(PageSiteNode pageNode) => pageNode != null && this.currentNode is PageSiteNode && this.currentNode.ParentNode is PageSiteNode parentNode && parentNode.Attributes.Keys.Contains("VisibleGroupPage") && pageNode.Id == parentNode.Id && this.NodeIsHomePage(parentNode);

    private class Template : ITemplate
    {
      private PageSiteNode node;

      public Template(PageSiteNode node) => this.node = node;

      public void InstantiateIn(Control container)
      {
        HtmlGenericControl child = new HtmlGenericControl("strong");
        child.InnerHtml = this.node.Title;
        container.Controls.Add((Control) child);
      }
    }
  }
}
