// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.NavigationControls.SitefinitySiteMapDataSource
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Web.UI.NavigationControls
{
  /// <summary>
  /// Provides a data source control that Web server controls and other controls
  /// can use to bind to the Sitefinity site map data.
  /// </summary>
  public class SitefinitySiteMapDataSource : SiteMapDataSource
  {
    private SitefinitySiteMapDataSource.InnerSiteMapDataSourceView dataSourceView;
    public static SiteMapNodeCollection EmptyCollection = SiteMapNodeCollection.ReadOnly(new SiteMapNodeCollection());
    private static readonly object EventNodesFiltering = new object();
    private static readonly object EventNodeChecking = new object();
    private bool addParentItem;

    /// <summary>Gets or sets the parent node.</summary>
    public SiteMapNode ParentNode { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether [add parent item].
    /// </summary>
    public bool AddParentItem
    {
      get => this.addParentItem;
      set => this.addParentItem = value;
    }

    /// <summary>Gets or sets the custom selected Nodes.</summary>
    public virtual IList<SiteMapNode> Nodes { get; set; }

    /// <summary>Gets or sets the levels to include.</summary>
    public virtual int? LevelsToInclude { get; set; }

    /// <summary>
    /// Gets or sets value indicating whether to show all pages when Nodes collection is empty. Introduced with Related data.
    /// </summary>
    /// <value>The do not load nodes when empty.</value>
    internal bool DoNotLoadNodesWhenEmpty { get; set; }

    /// <summary>
    /// Occurs before a collection of site map nodes is filtered.
    /// </summary>
    public event EventHandler<NodeCollectionOperationEventArgs> NodeCollectionFiltering
    {
      add => this.Events.AddHandler(SitefinitySiteMapDataSource.EventNodesFiltering, (Delegate) value);
      remove => this.Events.RemoveHandler(SitefinitySiteMapDataSource.EventNodesFiltering, (Delegate) value);
    }

    /// <summary>
    /// Occurs before a site map node is checked whether it meets given criteria.
    /// </summary>
    public event EventHandler<NodeOperationEventArgs> NodeChecking
    {
      add => this.Events.AddHandler(SitefinitySiteMapDataSource.EventNodeChecking, (Delegate) value);
      remove => this.Events.RemoveHandler(SitefinitySiteMapDataSource.EventNodeChecking, (Delegate) value);
    }

    /// <summary>Retrieves a single view on the site map data for the <see cref="T:System.Web.SiteMapProvider" />
    /// object according to the starting node and other properties of the data source.
    /// </summary>
    /// <returns>A <see cref="T:System.Web.UI.HierarchicalDataSourceView" /> helper object
    /// on the site map data, starting with the node that is identified by the <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.StartingNodeUrl" />
    /// or its child, if the <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.ShowStartingNode" />
    /// is false.</returns>
    /// <param name="viewPath">The URL of the starting node, specified by the <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.StartingNodeUrl" />.
    /// </param>
    /// <exception cref="T:System.Web.HttpException">No <see cref="T:System.Web.SiteMapProvider" />
    /// is configured or available for the site. </exception>
    /// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.StartFromCurrentNode" />
    /// is true but the <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.StartingNodeUrl" />
    /// is set.</exception>
    /// <exception cref="T:System.ArgumentException">The <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.StartingNodeUrl" />
    /// is set but the <see cref="T:System.Web.UI.WebControls.SiteMapDataSource" /> cannot
    /// resolve a node for the specified URL.</exception>
    protected override HierarchicalDataSourceView GetHierarchicalView(
      string viewPath)
    {
      return this.GetTreeView(viewPath);
    }

    /// <summary>Retrieves a named view on the site map data of the site map provider
    /// according to the starting node and other properties of the data source.</summary>
    /// <returns>A <see cref="T:System.Web.UI.HierarchicalDataSourceView" /> helper object
    /// on the site map data, according to the starting node that is identified by the
    /// <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.StartingNodeUrl" /> property
    /// or its child, if the <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.ShowStartingNode" />
    /// is false.</returns>
    /// <param name="viewName">The name of the data source view to retrieve.</param>
    /// <exception cref="T:System.Web.HttpException">The <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.Provider" />
    /// is null.</exception>
    /// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.StartFromCurrentNode" />
    /// is true but the <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.StartingNodeUrl" />
    /// is set.</exception>
    /// <exception cref="T:System.ArgumentException">The <see cref="P:System.Web.UI.WebControls.SiteMapDataSource.StartingNodeUrl" />
    /// is set but the <see cref="T:System.Web.UI.WebControls.SiteMapDataSource" /> cannot
    /// resolve a node for the specified URL.</exception>
    public override DataSourceView GetView(string viewName)
    {
      if (this.dataSourceView == null)
        this.dataSourceView = new SitefinitySiteMapDataSource.InnerSiteMapDataSourceView(this, viewName, this.GetPathNodeCollection(viewName));
      return (DataSourceView) this.dataSourceView;
    }

    /// <summary>Returns a collection of nodes based on the view path.</summary>
    /// <param name="viewPath">The view path.</param>
    /// <returns></returns>
    protected new virtual SiteMapNodeCollection GetPathNodeCollection(
      string viewPath)
    {
      SiteMapNodeCollection pathNodeCollection = (SiteMapNodeCollection) null;
      if (string.IsNullOrEmpty(viewPath))
      {
        pathNodeCollection = this.GetNodes();
      }
      else
      {
        SiteMapNode siteMapNodeFromKey = this.Provider.FindSiteMapNodeFromKey(viewPath);
        if (siteMapNodeFromKey != null)
          pathNodeCollection = this.FilterNodes(siteMapNodeFromKey.ChildNodes);
      }
      if (pathNodeCollection == null)
        pathNodeCollection = SitefinitySiteMapDataSource.EmptyCollection;
      return pathNodeCollection;
    }

    /// <summary>Returns a hierarchical view based on the view path.</summary>
    /// <param name="viewPath">The view path.</param>
    /// <returns></returns>
    protected virtual HierarchicalDataSourceView GetTreeView(
      string viewPath)
    {
      if (string.IsNullOrEmpty(viewPath))
      {
        SiteMapNodeCollection nodes = this.GetNodes();
        if (nodes != null)
          return (HierarchicalDataSourceView) new SitefinitySiteMapDataSource.InnerSiteMapHierarchicalDataSourceView(nodes, this);
      }
      else
      {
        SiteMapNode siteMapNodeFromKey = this.Provider.FindSiteMapNodeFromKey(viewPath);
        if (siteMapNodeFromKey != null)
          return (HierarchicalDataSourceView) new SitefinitySiteMapDataSource.InnerSiteMapHierarchicalDataSourceView(this.ConvertSiteMapNode(siteMapNodeFromKey).ChildNodes, this);
      }
      return (HierarchicalDataSourceView) SitefinitySiteMapDataSource.EmptyCollection.GetHierarchicalDataSourceView();
    }

    /// <summary>
    /// Returns a collection of site map nodes based on the current settings of the instance.
    /// </summary>
    /// <returns></returns>
    protected internal virtual SiteMapNodeCollection GetNodes()
    {
      int startingNodeOffset = this.StartingNodeOffset;
      if (!string.IsNullOrEmpty(this.StartingNodeUrl) && this.StartFromCurrentNode)
        throw new InvalidOperationException("StartingNodeUrl may not be set when StartFromCurrentNode is true.");
      SiteMapNode node1;
      if (this.StartFromCurrentNode)
        node1 = this.Provider.CurrentNode;
      else if (!string.IsNullOrEmpty(this.StartingNodeUrl))
      {
        node1 = this.Provider.FindSiteMapNode(this.MakeUrlAbsolute(this.StartingNodeUrl));
        if (node1 == null)
          throw new ArgumentException(string.Format("Could not find the sitemap node with URL '{0}'.", (object) this.StartingNodeUrl));
      }
      else
        node1 = this.Provider.RootNode;
      if (node1 == null)
        return (SiteMapNodeCollection) null;
      if (startingNodeOffset <= 0)
      {
        if (startingNodeOffset != 0)
        {
          this.Provider.HintNeighborhoodNodes(node1, Math.Abs(startingNodeOffset), 0);
          for (SiteMapNode parentNode = node1.ParentNode; startingNodeOffset < 0 && parentNode != null; ++startingNodeOffset)
          {
            node1 = node1.ParentNode;
            parentNode = node1.ParentNode;
          }
        }
        return this.GetNodes(node1);
      }
      SiteMapNode hintAncestorNodes = this.Provider.GetCurrentNodeAndHintAncestorNodes(-1);
      if (hintAncestorNodes == null || !hintAncestorNodes.IsDescendantOf(node1) || hintAncestorNodes.Equals((object) node1))
        return (SiteMapNodeCollection) null;
      SiteMapNode siteMapNode = hintAncestorNodes;
      for (int index = 0; index < startingNodeOffset; ++index)
      {
        siteMapNode = siteMapNode.ParentNode;
        if (siteMapNode == null || siteMapNode.Equals((object) node1))
          return this.GetNodes(hintAncestorNodes);
      }
      SiteMapNode node2 = hintAncestorNodes;
      for (; siteMapNode != null && !siteMapNode.Equals((object) node1); siteMapNode = siteMapNode.ParentNode)
        node2 = node2.ParentNode;
      return this.GetNodes(node2);
    }

    /// <summary>
    /// Returns a collection of site map nodes based on the given site map node.
    /// </summary>
    /// <param name="node">The site map node.</param>
    /// <returns></returns>
    protected virtual SiteMapNodeCollection GetNodes(SiteMapNode node)
    {
      SiteMapNode siteMapNode = this.ConvertSiteMapNode(node);
      return this.FilterNodes(!this.ShowStartingNode ? siteMapNode.ChildNodes : new SiteMapNodeCollection(siteMapNode));
    }

    /// <summary>Filters a collection of nodes.</summary>
    /// <param name="nodes">The nodes.</param>
    /// <returns></returns>
    protected virtual SiteMapNodeCollection FilterNodes(
      SiteMapNodeCollection nodes,
      bool modifyCollection = false)
    {
      NodeCollectionOperationEventArgs e = new NodeCollectionOperationEventArgs(nodes);
      this.OnNodeCollectionFiltering(e);
      if (e.Cancel)
        return e.Collection;
      if (!modifyCollection || nodes.IsReadOnly)
      {
        SiteMapNodeCollection mapNodeCollection = new SiteMapNodeCollection(nodes.Count);
        foreach (SiteMapNode node in nodes)
        {
          if (this.CheckNode(node))
            mapNodeCollection.Add(node);
        }
        return mapNodeCollection;
      }
      for (int index = nodes.Count - 1; index >= 0; --index)
      {
        if (!this.CheckNode(nodes[index]))
          nodes.RemoveAt(index);
      }
      return nodes;
    }

    /// <summary>
    /// Checks the passed site map node whether it meets given criteria.
    /// </summary>
    /// <param name="node">The site map node.</param>
    /// <returns></returns>
    protected virtual bool CheckNode(SiteMapNode node)
    {
      NodeOperationEventArgs e = new NodeOperationEventArgs(node);
      this.OnNodeChecking(e);
      return e.Cancel ? e.DoOperation : RouteHelper.CheckSiteMapNode(node);
    }

    /// <summary>
    /// Raises the <see cref="E:NodeCollectionFiltering" /> event.
    /// </summary>
    /// <param name="e">The <see cref="T:Telerik.Sitefinity.Web.UI.NavigationControls.NodeCollectionOperationEventArgs" /> instance containing the event data.</param>
    protected virtual void OnNodeCollectionFiltering(NodeCollectionOperationEventArgs e)
    {
      if (!(this.Events[SitefinitySiteMapDataSource.EventNodesFiltering] is EventHandler<NodeCollectionOperationEventArgs> eventHandler))
        return;
      eventHandler((object) this, e);
    }

    /// <summary>
    /// Raises the <see cref="E:NodeChecking" /> event.
    /// </summary>
    /// <param name="e">The <see cref="T:Telerik.Sitefinity.Web.UI.NavigationControls.NodeOperationEventArgs" /> instance containing the event data.</param>
    protected virtual void OnNodeChecking(NodeOperationEventArgs e)
    {
      if (!(this.Events[SitefinitySiteMapDataSource.EventNodeChecking] is EventHandler<NodeOperationEventArgs> eventHandler))
        return;
      eventHandler((object) this, e);
    }

    /// <summary>
    /// Converts the site map node to one of the internal types.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <returns></returns>
    protected virtual SiteMapNode ConvertSiteMapNode(SiteMapNode node)
    {
      SiteMapNode siteMapNode;
      switch (node)
      {
        case null:
          return (SiteMapNode) null;
        case SitefinitySiteMapDataSource.InnerSiteMapNode _:
        case SitefinitySiteMapDataSource.InnerPageSiteNode _:
          siteMapNode = node;
          break;
        case PageSiteNode _:
          siteMapNode = (SiteMapNode) new SitefinitySiteMapDataSource.InnerPageSiteNode((PageSiteNode) node, this);
          ((SitefinitySiteMapDataSource.InnerPageSiteNode) siteMapNode).OnGetChildNodesDelegate += new SitefinitySiteMapDataSource.InnerPageSiteNode.OnNodeAccessed(this.wrappedNode_OnGetChildNodesDelegate);
          ((SitefinitySiteMapDataSource.InnerPageSiteNode) siteMapNode).OnNodePropAccessed += new SitefinitySiteMapDataSource.InnerPageSiteNode.OnNodeAccessed(this.wrappedNode_OnNodePropAccessed);
          break;
        default:
          siteMapNode = (SiteMapNode) new SitefinitySiteMapDataSource.InnerSiteMapNode(node, this);
          ((SitefinitySiteMapDataSource.InnerPageSiteNode) siteMapNode).OnGetChildNodesDelegate += new SitefinitySiteMapDataSource.InnerPageSiteNode.OnNodeAccessed(this.wrappedNode_OnGetChildNodesDelegate);
          ((SitefinitySiteMapDataSource.InnerPageSiteNode) siteMapNode).OnNodePropAccessed += new SitefinitySiteMapDataSource.InnerPageSiteNode.OnNodeAccessed(this.wrappedNode_OnNodePropAccessed);
          break;
      }
      return siteMapNode;
    }

    private void wrappedNode_OnNodePropAccessed(SiteMapNode Node)
    {
      if (this.OnNodePropAccessed == null)
        return;
      this.OnNodePropAccessed(Node);
    }

    private void wrappedNode_OnGetChildNodesDelegate(SiteMapNode Node)
    {
      if (this.OnGetChildNodesDelegate == null)
        return;
      this.OnGetChildNodesDelegate(Node);
    }

    internal event SitefinitySiteMapDataSource.OnNodeAccessed OnGetChildNodesDelegate;

    internal event SitefinitySiteMapDataSource.OnNodeAccessed OnNodePropAccessed;

    private string MakeUrlAbsolute(string url)
    {
      if (url.Length == 0 || !VirtualPathUtility.IsAppRelative(url))
        return url;
      string templateSourceDirectory = this.AppRelativeTemplateSourceDirectory;
      return templateSourceDirectory.Length == 0 ? url : VirtualPathUtility.ToAbsolute(url, templateSourceDirectory);
    }

    internal delegate void OnNodeAccessed(SiteMapNode Node);

    protected class InnerSiteMapNode : SiteMapNode, IHierarchyData, INavigateUIData
    {
      private SitefinitySiteMapDataSource dataSource;
      private IHierarchyData hierarchyData;
      private INavigateUIData navigateUIData;
      private SiteMapNode sourceNode;
      private SiteMapNodeCollection childNodes;

      internal event SitefinitySiteMapDataSource.InnerSiteMapNode.OnNodeAccessed OnGetChildNodesDelegate;

      internal event SitefinitySiteMapDataSource.InnerSiteMapNode.OnNodeAccessed OnNodePropAccessed;

      public InnerSiteMapNode(SiteMapNode siteNode, SitefinitySiteMapDataSource dataSource)
        : base(siteNode.Provider, siteNode.Key)
      {
        this.sourceNode = siteNode;
        this.hierarchyData = (IHierarchyData) siteNode;
        this.navigateUIData = (INavigateUIData) siteNode;
        this.dataSource = dataSource;
      }

      public override SiteMapNode Clone() => this.dataSource.ConvertSiteMapNode(this.sourceNode.Clone());

      public override SiteMapNode Clone(bool cloneParentNodes) => this.dataSource.ConvertSiteMapNode(this.sourceNode.Clone(cloneParentNodes));

      public override SiteMapNodeCollection ChildNodes
      {
        get
        {
          if (this.OnGetChildNodesDelegate != null)
            this.OnGetChildNodesDelegate((SiteMapNode) this);
          if (this.childNodes == null)
            this.childNodes = SitefinitySiteMapDataSource.Helper.FilterChildNodes(this.sourceNode, this.dataSource);
          return this.childNodes;
        }
        set => this.sourceNode.ChildNodes = value;
      }

      IHierarchicalEnumerable IHierarchyData.GetChildren() => (IHierarchicalEnumerable) this.ChildNodes;

      IHierarchyData IHierarchyData.GetParent() => (IHierarchyData) this.dataSource.ConvertSiteMapNode(this.hierarchyData.GetParent() as SiteMapNode);

      bool IHierarchyData.HasChildren => this.hierarchyData.HasChildren;

      object IHierarchyData.Item => (object) this.dataSource.ConvertSiteMapNode(this.hierarchyData.Item as SiteMapNode);

      string IHierarchyData.Path => this.hierarchyData.Path;

      string IHierarchyData.Type => this.hierarchyData.Type;

      string INavigateUIData.Description => this.navigateUIData.Description;

      string INavigateUIData.Name => this.navigateUIData.Name;

      string INavigateUIData.NavigateUrl
      {
        get
        {
          if (this.OnNodePropAccessed != null)
            this.OnNodePropAccessed((SiteMapNode) this);
          return this.navigateUIData.NavigateUrl;
        }
      }

      string INavigateUIData.Value => this.navigateUIData.Value;

      public override bool Equals(object obj) => this.sourceNode.Equals(obj);

      public override int GetHashCode() => this.sourceNode.GetHashCode();

      public override string Title
      {
        get => base.Title;
        set => base.Title = value;
      }

      internal delegate void OnNodeAccessed(SiteMapNode Node);
    }

    protected class InnerPageSiteNode : PageSiteNode, IHierarchyData, INavigateUIData
    {
      private SitefinitySiteMapDataSource dataSource;
      private IHierarchyData hierarchyData;
      private INavigateUIData navigateUIData;
      private PageSiteNode sourceNode;
      private SiteMapNodeCollection childNodes;

      internal event SitefinitySiteMapDataSource.InnerPageSiteNode.OnNodeAccessed OnGetChildNodesDelegate;

      internal event SitefinitySiteMapDataSource.InnerPageSiteNode.OnNodeAccessed OnNodePropAccessed;

      public InnerPageSiteNode(PageSiteNode siteNode, SitefinitySiteMapDataSource dataSource)
        : base(siteNode.Provider, siteNode.Key)
      {
        this.sourceNode = siteNode;
        this.hierarchyData = (IHierarchyData) siteNode;
        this.navigateUIData = (INavigateUIData) siteNode;
        this.dataSource = dataSource;
        this.LocalizationStrategy = siteNode.LocalizationStrategy;
      }

      public override SiteMapNode Clone() => this.dataSource.ConvertSiteMapNode(this.sourceNode.Clone());

      public override SiteMapNode Clone(bool cloneParentNodes) => this.dataSource.ConvertSiteMapNode(this.sourceNode.Clone(cloneParentNodes));

      public override SiteMapNodeCollection ChildNodes
      {
        get
        {
          if (this.NodeType == NodeType.Group && this.OnGetChildNodesDelegate != null)
            this.OnGetChildNodesDelegate((SiteMapNode) this);
          if (this.childNodes == null)
            this.childNodes = SitefinitySiteMapDataSource.Helper.FilterChildNodes((SiteMapNode) this.sourceNode, this.dataSource);
          return this.childNodes;
        }
        set => this.sourceNode.ChildNodes = value;
      }

      IHierarchicalEnumerable IHierarchyData.GetChildren() => (IHierarchicalEnumerable) this.ChildNodes;

      IHierarchyData IHierarchyData.GetParent() => (IHierarchyData) this.dataSource.ConvertSiteMapNode(this.hierarchyData.GetParent() as SiteMapNode);

      bool IHierarchyData.HasChildren => this.hierarchyData.HasChildren;

      object IHierarchyData.Item => (object) this.dataSource.ConvertSiteMapNode(this.hierarchyData.Item as SiteMapNode);

      string IHierarchyData.Path => this.hierarchyData.Path;

      string IHierarchyData.Type => this.hierarchyData.Type;

      string INavigateUIData.Description => this.navigateUIData.Description;

      string INavigateUIData.Name => this.navigateUIData.Name;

      string INavigateUIData.NavigateUrl => this.navigateUIData.NavigateUrl;

      string INavigateUIData.Value => this.navigateUIData.Value;

      public override bool Equals(object obj) => this.sourceNode.Equals(obj);

      public override bool HasChildNodes => this.sourceNode.HasChildNodes;

      public override bool IsAccessibleToUser(HttpContext context) => this.sourceNode.IsAccessibleToUser(context);

      public override bool IsDescendantOf(SiteMapNode node) => this.sourceNode.IsDescendantOf(node);

      public override SiteMapNode NextSibling => this.dataSource.ConvertSiteMapNode(this.sourceNode.NextSibling);

      public override SiteMapNode ParentNode
      {
        get => this.dataSource.ConvertSiteMapNode(this.sourceNode.ParentNode);
        set => this.sourceNode.ParentNode = value;
      }

      public override SiteMapNode PreviousSibling => this.dataSource.ConvertSiteMapNode(this.sourceNode.PreviousSibling);

      public override SiteMapNode RootNode => this.dataSource.ConvertSiteMapNode(this.sourceNode.RootNode);

      public override string this[string key]
      {
        get => this.sourceNode[key];
        set => this.sourceNode[key] = value;
      }

      public override int GetHashCode() => this.sourceNode.GetHashCode();

      public override string ToString() => this.sourceNode.ToString();

      public new IList Roles
      {
        get => this.sourceNode.Roles;
        set => this.sourceNode.Roles = value;
      }

      public override NameValueCollection Attributes => this.sourceNode.Attributes;

      public override CultureInfo[] AvailableLanguages => this.sourceNode.AvailableLanguages;

      public override bool Crawlable => this.sourceNode.Crawlable;

      public override bool ContainsUrlName(string name) => this.sourceNode.ContainsUrlName(name);

      public override void EnsurePageDataLoaded() => this.sourceNode.EnsurePageDataLoaded();

      public override string GetUrl(CultureInfo culture) => this.sourceNode.GetUrl(culture);

      public override string GetUrl(CultureInfo culture, bool fallbackToAnyLanguage) => this.sourceNode.GetUrl(culture, fallbackToAnyLanguage);

      public override Guid Id => this.sourceNode.Id;

      public override bool IsBackend => this.sourceNode.IsBackend;

      public override bool IsGroupPage => this.sourceNode.IsGroupPage;

      public override NodeType NodeType => this.sourceNode.NodeType;

      public override float Ordinal
      {
        get => this.sourceNode.Ordinal;
        set => this.sourceNode.Ordinal = value;
      }

      public override string OutputCacheProfile => this.sourceNode.OutputCacheProfile;

      public override Guid PageId => this.sourceNode.PageId;

      public override string PageProviderName => this.sourceNode.PageProviderName;

      public override bool RenderAsLink => this.sourceNode.RenderAsLink;

      public override bool ShowInNavigation => this.sourceNode.ShowInNavigation;

      public override ContentLifecycleStatus Status => this.sourceNode.Status;

      public override string Theme => this.sourceNode.Theme;

      public override string Title
      {
        get => this.sourceNode.Title;
        set => this.sourceNode.Title = value;
      }

      public override string Url
      {
        get
        {
          if (this.OnNodePropAccessed != null)
            this.OnNodePropAccessed((SiteMapNode) this);
          return this.sourceNode.Url;
        }
        set => this.sourceNode.Url = value;
      }

      public override string UrlName => this.sourceNode.UrlName;

      public override int Version => this.sourceNode.Version;

      public override bool Visible => this.sourceNode.Visible;

      public override IList<Guid> DeniedRoles
      {
        get => this.sourceNode.DeniedRoles;
        set => this.sourceNode.DeniedRoles = value;
      }

      public override Guid LinkedNodeId
      {
        get => this.sourceNode.LinkedNodeId;
        set => this.sourceNode.LinkedNodeId = value;
      }

      public override string LinkedNodeProvider
      {
        get => this.sourceNode.LinkedNodeProvider;
        set => this.sourceNode.LinkedNodeProvider = value;
      }

      public override string RedirectUrl => this.sourceNode.RedirectUrl;

      public override IList<string> PublishedTranslations => this.sourceNode.PublishedTranslations;

      public override bool IsPublished(CultureInfo culture = null) => this.sourceNode.IsPublished(culture);

      public override bool IsHidden(CultureInfo culture = null) => this.sourceNode.IsHidden(culture);

      public override bool AdditionalUrlsRedirectToDefault => this.sourceNode.AdditionalUrlsRedirectToDefault;

      public override string CacheKey
      {
        get => this.sourceNode.CacheKey;
        set => this.sourceNode.CacheKey = value;
      }

      public override List<Guid> PageLinksIds
      {
        get => this.sourceNode.PageLinksIds;
        set => this.sourceNode.PageLinksIds = value;
      }

      public override string ParentKey => this.sourceNode.ParentKey;

      public override string UiCulture
      {
        get => this.sourceNode.UiCulture;
        set => this.sourceNode.UiCulture = value;
      }

      public override UrlEvaluationMode UrlEvaluationMode
      {
        get => this.sourceNode.UrlEvaluationMode;
        set => this.sourceNode.UrlEvaluationMode = value;
      }

      public override IList<CacheDependencyKey> GetCacheDependencyObjects() => this.sourceNode.GetCacheDependencyObjects();

      public override CompoundCacheDependency CacheDependency
      {
        get => this.sourceNode.CacheDependency;
        set => this.sourceNode.CacheDependency = value;
      }

      public override PageSiteNode GetTerminalNode(bool ifAccessible) => this.sourceNode.GetTerminalNode(ifAccessible);

      /// <summary>
      /// Represents an extension to be used with the current page, with the dot (".") - .aspx, .html, .php etc
      /// </summary>
      /// <value></value>
      public override string Extension => this.sourceNode.Extension;

      internal override string VersionKey => this.sourceNode.VersionKey;

      internal delegate void OnNodeAccessed(SiteMapNode Node);
    }

    protected class InnerSiteMapHierarchicalDataSourceView : HierarchicalDataSourceView
    {
      private SitefinitySiteMapDataSource dataSource;
      private SiteMapNodeCollection _collection;

      public InnerSiteMapHierarchicalDataSourceView(
        SiteMapNode node,
        SitefinitySiteMapDataSource dataSource)
      {
        this._collection = new SiteMapNodeCollection(node);
        this.dataSource = dataSource;
      }

      public InnerSiteMapHierarchicalDataSourceView(
        SiteMapNodeCollection collection,
        SitefinitySiteMapDataSource dataSource)
      {
        this._collection = collection;
        this.dataSource = dataSource;
      }

      public override IHierarchicalEnumerable Select() => (IHierarchicalEnumerable) new SitefinitySiteMapDataSource.InnerHierarchicalEnumerable((IHierarchicalEnumerable) this._collection, this.dataSource);
    }

    protected class InnerSiteMapDataSourceView : DataSourceView
    {
      private SiteMapNodeCollection _collection;
      private SitefinitySiteMapDataSource _owner;

      public InnerSiteMapDataSourceView(
        SitefinitySiteMapDataSource owner,
        string name,
        SiteMapNode node)
        : base((IDataSource) owner, name)
      {
        this._owner = owner;
        this._collection = new SiteMapNodeCollection(node);
      }

      public InnerSiteMapDataSourceView(
        SitefinitySiteMapDataSource owner,
        string name,
        SiteMapNodeCollection collection)
        : base((IDataSource) owner, name)
      {
        this._owner = owner;
        this._collection = collection;
      }

      protected override IEnumerable ExecuteSelect(DataSourceSelectArguments arguments)
      {
        arguments.RaiseUnsupportedCapabilitiesError((DataSourceView) this);
        return (IEnumerable) new SitefinitySiteMapDataSource.InnerEnumerable((IEnumerable) this._collection, this._owner);
      }

      protected override void OnDataSourceViewChanged(EventArgs e)
      {
        this._collection = this._owner.GetPathNodeCollection(this.Name);
        base.OnDataSourceViewChanged(e);
      }

      public IEnumerable Select(DataSourceSelectArguments arguments) => this.ExecuteSelect(arguments);
    }

    protected class InnerEnumerable : IEnumerable
    {
      private SitefinitySiteMapDataSource dataSource;
      private IEnumerable sourceEnumerable;

      public InnerEnumerable(IEnumerable sourceEnumerable, SitefinitySiteMapDataSource dataSource)
      {
        this.sourceEnumerable = sourceEnumerable;
        this.dataSource = dataSource;
      }

      public IEnumerator GetEnumerator() => (IEnumerator) new SitefinitySiteMapDataSource.InnerEnumerator(this.sourceEnumerable.GetEnumerator(), this.dataSource);
    }

    protected class InnerHierarchicalEnumerable : IHierarchicalEnumerable, IEnumerable
    {
      private SitefinitySiteMapDataSource dataSource;
      private IHierarchicalEnumerable sourceHierarchicalEnumerable;

      public InnerHierarchicalEnumerable(
        IHierarchicalEnumerable sourceHierarchicalEnumerable,
        SitefinitySiteMapDataSource dataSource)
      {
        this.sourceHierarchicalEnumerable = sourceHierarchicalEnumerable;
        this.dataSource = dataSource;
      }

      public IHierarchyData GetHierarchyData(object enumeratedItem) => (IHierarchyData) this.dataSource.ConvertSiteMapNode(enumeratedItem as SiteMapNode);

      public IEnumerator GetEnumerator() => (IEnumerator) new SitefinitySiteMapDataSource.InnerEnumerator(this.sourceHierarchicalEnumerable.GetEnumerator(), this.dataSource);
    }

    protected class InnerEnumerator : IEnumerator
    {
      private IEnumerator sourceEnumerator;
      private SitefinitySiteMapDataSource dataSource;

      public InnerEnumerator(IEnumerator sourceEnumerator, SitefinitySiteMapDataSource dataSource)
      {
        this.sourceEnumerator = sourceEnumerator;
        this.dataSource = dataSource;
      }

      public object Current => (object) this.dataSource.ConvertSiteMapNode(this.sourceEnumerator.Current as SiteMapNode);

      public bool MoveNext() => this.sourceEnumerator.MoveNext();

      public void Reset() => this.sourceEnumerator.Reset();
    }

    protected static class Helper
    {
      public static SiteMapNodeCollection FilterChildNodes(
        SiteMapNode node,
        SitefinitySiteMapDataSource dataSource)
      {
        SiteMapNodeCollection childNodes = node.ChildNodes;
        SiteMapNodeCollection nodes;
        if (childNodes is PageSiteNodeCollection)
          nodes = (SiteMapNodeCollection) new PageSiteNodeCollection()
          {
            Overflowed = ((PageSiteNodeCollection) childNodes).Overflowed
          };
        else
          nodes = new SiteMapNodeCollection(childNodes.Count);
        foreach (SiteMapNode node1 in childNodes)
          nodes.Add(dataSource.ConvertSiteMapNode(node1));
        return dataSource.FilterNodes(nodes, true);
      }
    }
  }
}
