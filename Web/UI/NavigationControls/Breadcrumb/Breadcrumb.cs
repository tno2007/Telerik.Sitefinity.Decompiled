// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.NavigationControls.Breadcrumb.Breadcrumb
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages.Web.UI;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.NavigationControls.Breadcrumb
{
  /// <summary>
  /// Represents a control rendering a breadcrumb navigation
  /// </summary>
  [ControlDesigner(typeof (BreadcrumbDesigner))]
  [RequireScriptManager(true)]
  [IndexRenderMode(IndexRenderModes.NoOutput)]
  public class Breadcrumb : SimpleView, IHasCacheDependency
  {
    private List<SiteMapNode> accessedNodes = new List<SiteMapNode>();
    private List<SiteMapNode> childNodes = new List<SiteMapNode>();
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.PublicControls.Breadcrumb.ascx");
    internal const string layoutTemplateName = "Telerik.Sitefinity.Resources.Templates.PublicControls.Breadcrumb.ascx";
    internal const string frontEndFriendlyName = "Breadcrumb";
    internal SiteMapDataSource currentSiteMapDataSource;
    private SiteMapProvider provider;
    private List<SiteMapNode> breadcrumbDataSource;
    private string nodeSeparatorMarkup = "<span class='sfBreadcrumbNodeSeparator'>/</span>";
    private bool showHomePage = true;
    private bool showCurrentPage = true;
    private bool showFullPath = true;

    /// <summary>Gets the layout template path</summary>
    public override string LayoutTemplatePath => Telerik.Sitefinity.Web.UI.NavigationControls.Breadcrumb.Breadcrumb.layoutTemplatePath;

    /// <summary>Gets the layout template name</summary>
    protected override string LayoutTemplateName => string.Empty;

    /// <summary>Gets or sets the root page pageId(Guid) or url.</summary>
    /// <value>The root page pageId.</value>
    public virtual Guid StartingNodeId { get; set; }

    /// <summary>Gets the selected page title.</summary>
    /// <value>The selected page title.</value>
    public virtual string SelectedPageTitle => this.SelectedPageNode != null ? this.SelectedPageNode.Title : (string) null;

    /// <summary>
    /// Gets or sets whether to show the full path to the currnet page.
    /// </summary>
    public virtual bool ShowFullPath
    {
      get => this.showFullPath;
      set => this.showFullPath = value;
    }

    /// <summary>
    /// Gets or sets the node text separator in the breadcrumb
    /// </summary>
    public virtual string NodeSeparatorMarkup
    {
      get => this.nodeSeparatorMarkup;
      set => this.nodeSeparatorMarkup = value;
    }

    /// <summary>
    /// Gets or sets whether to show the home page in the breadcrumb
    /// </summary>
    public virtual bool ShowHomePage
    {
      get => this.showHomePage;
      set => this.showHomePage = value;
    }

    /// <summary>
    /// Gets or sets whether to show the current page in the breadcrumb
    /// </summary>
    public virtual bool ShowCurrentPage
    {
      get => this.showCurrentPage;
      set => this.showCurrentPage = value;
    }

    /// <summary>
    /// Gets or sets whether to show group pages in the breadcrumb.
    /// </summary>
    public bool ShowGroupPages { get; set; }

    /// <summary>Gets or sets the breadcrumb label.</summary>
    public virtual string BreadcrumbLabelText { get; set; }

    /// <summary>Gets or sets the name of the site map provider.</summary>
    /// <value>The name of the site map provider.</value>
    public string SiteMapProviderName { get; set; }

    /// <summary>
    /// Gets or sets the allow virtual nodes.
    /// For example, the last control on the page,
    /// which registers a breadcrumb extender with Telerik.Sitefinity.Web.RegisterBreadcrumbExtender(this Page page, IBreadcrumExtender extender) extension method of the page,
    /// will be applied.
    /// </summary>
    /// <value>The allow virtual nodes.</value>
    public bool AllowVirtualNodes { get; set; }

    /// <summary>Gets the selected page node.</summary>
    private PageSiteNode SelectedPageNode => this.StartingNodeId != Guid.Empty ? (PageSiteNode) ((SiteMapBase) this.GetProvider()).FindSiteMapNodeFromKey(this.StartingNodeId.ToString(), false) : (PageSiteNode) null;

    /// <summary>
    /// Reference to the RadSiteMap control used for breadcrum.
    /// </summary>
    protected virtual RadSiteMap SiteMapBreadcrumb => this.Container.GetControl<RadSiteMap>(nameof (Breadcrumb), true);

    /// <summary>Reference to the Label control before the RadSiteMap.</summary>
    protected virtual Label BreadcrumbLabel => this.Container.GetControl<Label>(nameof (BreadcrumbLabel), true);

    /// <summary>Initialize Breadcrumb's controls.</summary>
    /// <param name="container"></param>
    protected override void InitializeControls(GenericContainer container)
    {
      if (this.GetIndexRenderMode() == IndexRenderModes.NoOutput)
        return;
      this.InitializeBreadcrumbDataSource();
      this.SetControlSettings();
    }

    /// <summary>
    /// Subscribes the for cache dependency, when an item of type <see cref="T:Telerik.Sitefinity.Pages.Model.PageNode" /> is changed all cached instances of type
    /// <see cref="T:Telerik.Sitefinity.Pages.Model.PageNode" /> should be notified for cache invalidation.
    /// </summary>
    protected virtual void SubsribeCacheDependency()
    {
      if (this.IsBackend())
        return;
      IList<CacheDependencyKey> dependencyObjects = this.GetCacheDependencyObjects();
      if (!SystemManager.CurrentHttpContext.Items.Contains((object) "PageNodesCacheDependencyName"))
        SystemManager.CurrentHttpContext.Items.Add((object) "PageNodesCacheDependencyName", (object) new List<CacheDependencyKey>());
      ((List<CacheDependencyKey>) SystemManager.CurrentHttpContext.Items[(object) "PageNodesCacheDependencyName"]).AddRange((IEnumerable<CacheDependencyKey>) dependencyObjects);
    }

    /// <summary>OnPreRender stage of the control's life cycle</summary>
    /// <param name="e"></param>
    /// <remarks>In the following method the SiteMapBreadcrumb control is bound.</remarks>
    protected override void OnPreRender(EventArgs e)
    {
      if (this.currentSiteMapDataSource is SitefinitySiteMapDataSource)
      {
        ((SitefinitySiteMapDataSource) this.currentSiteMapDataSource).OnNodePropAccessed += new SitefinitySiteMapDataSource.OnNodeAccessed(this.Breadcrumb_OnNodePropAccessed);
        ((SitefinitySiteMapDataSource) this.currentSiteMapDataSource).OnGetChildNodesDelegate += new SitefinitySiteMapDataSource.OnNodeAccessed(this.Breadcrumb_OnGetChildNodesDelegate);
      }
      if (this.AllowVirtualNodes)
        this.Page.PreRenderComplete += new EventHandler(this.Page_PreRenderComplete);
      else
        this.SiteMapBreadcrumb.DataBind();
      base.OnPreRender(e);
      this.SubsribeCacheDependency();
    }

    private void Breadcrumb_OnNodePropAccessed(SiteMapNode Node) => this.accessedNodes.Add(Node);

    private void Breadcrumb_OnGetChildNodesDelegate(SiteMapNode Node) => this.childNodes.Add(Node);

    private void Page_PreRenderComplete(object sender, EventArgs e)
    {
      IBreadcrumExtender breadcrumbExtender = this.Page.GetBreadcrumbExtender();
      if (breadcrumbExtender != null)
        this.breadcrumbDataSource.AddRange(breadcrumbExtender.GetVirtualNodes(this.currentSiteMapDataSource.Provider));
      this.SiteMapBreadcrumb.DataBind();
    }

    /// <summary>Get the tag key of the Breadcrumb control.</summary>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    protected override void Render(HtmlTextWriter writer)
    {
      if (this.GetIndexRenderMode() != IndexRenderModes.Normal)
        return;
      base.Render(writer);
    }

    private void InitializeBreadcrumbDataSource()
    {
      this.currentSiteMapDataSource = (SiteMapDataSource) new SitefinitySiteMapDataSource();
      this.currentSiteMapDataSource.Provider = this.GetProvider();
      if (!(this.currentSiteMapDataSource.Provider is SiteMapBase provider))
        return;
      SiteMapNode currentNode = provider.CurrentNode;
      if (currentNode == null)
        return;
      ISite currentSite = SystemManager.CurrentContext.CurrentSite;
      string strB = currentSite.SiteMapRootNodeId.ToString();
      this.breadcrumbDataSource = new List<SiteMapNode>();
      string str = currentNode.Key;
      Guid homePageId = currentSite.HomePageId;
      PageSiteNode node = homePageId != Guid.Empty ? (PageSiteNode) provider.FindSiteMapNodeFromKey(homePageId.ToString()) : (PageSiteNode) null;
      PageSiteNode pageSiteNode = (PageSiteNode) provider.FindSiteMapNodeForSpecificLanguage((SiteMapNode) node, SystemManager.CurrentContext.Culture) ?? node;
      bool flag1 = this.SelectedPageNode != null && !this.ShowFullPath;
      bool flag2 = false;
      PageSiteNode siteMapNodeFromKey;
      for (; !string.IsNullOrEmpty(str) && string.Compare(str, strB, StringComparison.OrdinalIgnoreCase) != 0; str = siteMapNodeFromKey.ParentKey)
      {
        siteMapNodeFromKey = (PageSiteNode) provider.FindSiteMapNodeFromKey(str, false);
        if (siteMapNodeFromKey != pageSiteNode && siteMapNodeFromKey.IsAccessibleToUser(HttpContext.Current) && siteMapNodeFromKey.Visible)
        {
          if (this.ShowGroupPages)
            this.breadcrumbDataSource.Insert(0, (SiteMapNode) siteMapNodeFromKey);
          else if (!siteMapNodeFromKey.IsGroupPage)
            this.breadcrumbDataSource.Insert(0, (SiteMapNode) siteMapNodeFromKey);
        }
        if (flag1 && siteMapNodeFromKey == this.SelectedPageNode)
        {
          flag2 = true;
          break;
        }
      }
      if (!flag2 && !this.ShowFullPath)
        this.breadcrumbDataSource.Clear();
      else if (pageSiteNode != null)
        this.breadcrumbDataSource.Insert(0, (SiteMapNode) pageSiteNode);
      if (this.breadcrumbDataSource.Count <= 0)
        return;
      if (!this.ShowHomePage)
        this.breadcrumbDataSource.RemoveAt(0);
      int index = this.breadcrumbDataSource.Count - 1;
      if (this.ShowCurrentPage || index <= -1)
        return;
      this.breadcrumbDataSource.RemoveAt(index);
    }

    private void SetControlSettings()
    {
      this.SiteMapBreadcrumb.DataBound += new EventHandler(this.SiteMapBreadcrumb_DataBound);
      this.SiteMapBreadcrumb.NodeDataBound += new RadSiteMapNodeEventHandler(this.SiteMapBreadcrumb_NodeDataBound);
      this.SiteMapBreadcrumb.DefaultLevelSettings.SeparatorText = this.NodeSeparatorMarkup;
      this.SiteMapBreadcrumb.DataSource = (object) this.breadcrumbDataSource;
      if (this.BreadcrumbLabel == null)
        return;
      this.BreadcrumbLabel.Text = this.BreadcrumbLabelText;
    }

    private void SiteMapBreadcrumb_NodeDataBound(object sender, RadSiteMapNodeEventArgs e)
    {
      SiteMapNode dataItem = e.Node.DataItem as SiteMapNode;
      e.Node.Text = HttpUtility.HtmlEncode(dataItem.Title);
    }

    private void SiteMapBreadcrumb_DataBound(object sender, EventArgs e)
    {
      RadSiteMapNodeCollection nodes = this.SiteMapBreadcrumb.Nodes;
      int count = nodes.Count;
      int num = count - 1;
      for (int index = 0; index < count; ++index)
      {
        if (this.ShowCurrentPage)
        {
          if (index == num)
          {
            nodes[index].NavigateUrl = "javascript: void(0)";
            nodes[index].CssClass = "sfNoBreadcrumbNavigation";
          }
          else
            nodes[index].CssClass = "sfBreadcrumbNavigation";
        }
        else
          nodes[index].CssClass = "sfBreadcrumbNavigation";
      }
      if (this.breadcrumbDataSource != null)
        return;
      ControlCollection controls = this.Controls;
      Label child = new Label();
      child.Text = Res.Get<Labels>().BreadcrumbOnTemplateMessage;
      child.CssClass = "sfBreadcrumbOnTemplateMessage";
      controls.Add((Control) child);
    }

    private SiteMapProvider GetProvider()
    {
      if (this.provider == null)
        this.provider = !string.IsNullOrEmpty(this.SiteMapProviderName) ? SiteMapBase.GetSiteMapProvider(this.SiteMapProviderName) : SiteMapBase.GetSiteMapProvider("SitefinitySiteMap");
      return this.provider;
    }

    /// <summary>
    /// Gets a collection of cached and changed items that need to be invalidated for the specific views that display all types inheriting from
    /// the abstract type <see cref="T:Telerik.Sitefinity.GenericContent.Model.Content" />.
    /// </summary>
    /// <returns></returns>
    public virtual IList<CacheDependencyKey> GetCacheDependencyObjects()
    {
      List<CacheDependencyKey> dependencyObjects = new List<CacheDependencyKey>();
      foreach (SiteMapNode siteMapNode in this.accessedNodes.ToList<SiteMapNode>())
        dependencyObjects.Add(new CacheDependencyKey()
        {
          Type = typeof (CacheDependencyPageNodeObject),
          Key = siteMapNode.Key + (object) SystemManager.CurrentContext.Culture
        });
      foreach (SiteMapNode siteMapNode in this.childNodes.ToList<SiteMapNode>())
      {
        List<CacheDependencyKey> cacheDependencyKeyList1 = dependencyObjects;
        CacheDependencyKey cacheDependencyKey1 = new CacheDependencyKey();
        cacheDependencyKey1.Type = typeof (CacheDependencyPageNodeStateChange);
        cacheDependencyKey1.Key = siteMapNode.Key + (object) SystemManager.CurrentContext.Culture;
        CacheDependencyKey cacheDependencyKey2 = cacheDependencyKey1;
        cacheDependencyKeyList1.Add(cacheDependencyKey2);
        foreach (SiteMapNode childNode in siteMapNode.ChildNodes)
        {
          List<CacheDependencyKey> cacheDependencyKeyList2 = dependencyObjects;
          cacheDependencyKey1 = new CacheDependencyKey();
          cacheDependencyKey1.Type = typeof (CacheDependencyPageNodeStateChange);
          cacheDependencyKey1.Key = childNode.Key + (object) SystemManager.CurrentContext.Culture;
          CacheDependencyKey cacheDependencyKey3 = cacheDependencyKey1;
          cacheDependencyKeyList2.Add(cacheDependencyKey3);
        }
      }
      return (IList<CacheDependencyKey>) dependencyObjects;
    }

    internal delegate IList<SiteMapNode> GetAdditionalSiteMapNodes(
      SiteMapProvider provider);
  }
}
