// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.NavigationModelBase`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI.NavigationControls;

namespace Telerik.Sitefinity.Web
{
  /// <summary>
  ///     This class represents model used for Navigation widget.
  /// </summary>
  /// <typeparam name="T">The frontend page node type.</typeparam>
  public class NavigationModelBase<T> where T : FrontendPageNodeBase<T>
  {
    private static readonly Type CacheDependencyPageNodeObjectType = Type.GetType("Telerik.Sitefinity.Pages.Model.CacheDependencyPageNodeObject, Telerik.Sitefinity.Model");
    private static readonly Type CacheDependencyObjectForAllSitesType = Type.GetType("Telerik.Sitefinity.Pages.Model.CacheDependencyObjectForAllSites, Telerik.Sitefinity.Model");
    private static readonly Type CacheDependencyPageNodeStateChangeType = Type.GetType("Telerik.Sitefinity.Pages.Model.CacheDependencyPageNodeStateChange, Telerik.Sitefinity.Model");
    private HashSet<string> viewModelNodeIds = new HashSet<string>();
    private HashSet<PageSiteNode> pageSiteNodes = new HashSet<PageSiteNode>();
    private IList<T> frontendNodes;
    /// <summary>The provider</summary>
    private SiteMapProvider provider;
    private SiteMapNode currentSiteMapNode;
    private string siteMapProviderName = "SitefinitySiteMap";
    private Guid selectedPageId;
    private SelectedPageModelBase[] selectedPages;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.NavigationModelBase`1" /> class.
    /// </summary>
    public NavigationModelBase()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.NavigationModelBase`1" /> class.
    /// </summary>
    /// <param name="settings">The navigation settings.</param>
    public NavigationModelBase(NavigationSettings settings)
    {
      this.SelectionModeString = settings.SelectionModeString;
      this.LevelsToInclude = settings.LevelsToInclude;
      this.ShowParentPage = settings.ShowParentPage;
      this.selectedPageId = settings.SelectedPageId;
      this.OpenExternalPageInNewTab = settings.OpenExternalPageInNewTab;
      this.currentSiteMapNode = this.GetCurrentPageNode(settings.CurrentSiteMapNodeKey);
      this.PopulateSelectedPages(settings);
    }

    private void PopulateSelectedPages(NavigationSettings settings)
    {
      if (string.IsNullOrEmpty(settings.SelectedPagesSerialized))
        return;
      string[] strArray = JsonConvert.DeserializeObject<string[]>(HttpUtility.UrlDecode(settings.SelectedPagesSerialized));
      if (strArray == null || strArray.Length == 0)
        return;
      List<SelectedPageModelBase> selectedPageModelBaseList = new List<SelectedPageModelBase>();
      SiteMapProvider provider = this.GetProvider();
      foreach (string str in strArray)
      {
        Guid result;
        if (Guid.TryParse(str, out result))
        {
          SiteMapNode siteMapNodeFromKey = provider.FindSiteMapNodeFromKey(str);
          if (siteMapNodeFromKey != null)
            selectedPageModelBaseList.Add(new SelectedPageModelBase()
            {
              Id = result,
              Title = siteMapNodeFromKey.Title,
              Url = siteMapNodeFromKey.Url,
              IsExternal = false
            });
        }
        else
          selectedPageModelBaseList.Add(new SelectedPageModelBase()
          {
            Title = str,
            IsExternal = true
          });
      }
      this.selectedPages = selectedPageModelBaseList.ToArray();
    }

    /// <summary>
    ///     Gets or sets the CSS class that will be applied on the wrapper div of the NavigationWidget (if such is presented).
    /// </summary>
    /// <value>The CSS class.</value>
    [Browsable(false)]
    public string CssClass { get; set; }

    /// <summary>Gets the current site map node.</summary>
    /// <value>The current site map node.</value>
    [Browsable(false)]
    public virtual SiteMapNode CurrentSiteMapNode => this.currentSiteMapNode != null ? this.currentSiteMapNode : this.SiteMap.CurrentNode;

    /// <summary>
    /// Gets or sets a serialized array of the selected pages.
    /// </summary>
    /// <value>The a serialized array of selected pages.</value>
    [Browsable(false)]
    public SelectedPageModelBase[] SelectedPages
    {
      get => this.selectedPages;
      set => this.selectedPages = value;
    }

    /// <summary>Gets or sets the levels to include.</summary>
    [Browsable(false)]
    public virtual int? LevelsToInclude { get; set; }

    /// <summary>
    /// Gets the list of site map nodes that will be displayed in the navigation widget.
    /// </summary>
    /// <value>The nodes.</value>
    [Browsable(false)]
    public IList<T> Nodes => this.frontendNodes ?? (this.frontendNodes = (IList<T>) new List<T>());

    /// <summary>
    ///     Gets or sets the page links to display selection mode.
    /// </summary>
    /// <value>The page display mode.</value>
    [Browsable(false)]
    public string SelectionModeString { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether [show parent page].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [show parent page]; otherwise, <c>false</c>.
    /// </value>
    [Browsable(false)]
    public bool ShowParentPage { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether should open external page in new tab.
    /// </summary>
    /// <value>
    /// <c>true</c> if should open external page in new tab; otherwise, <c>false</c>.
    /// </value>
    [Browsable(false)]
    public bool OpenExternalPageInNewTab { get; set; }

    /// <summary>Gets the site map.</summary>
    /// <value>The site map.</value>
    [Browsable(false)]
    public virtual SiteMapBase SiteMap => (SiteMapBase) SitefinitySiteMap.GetCurrentProvider();

    /// <summary>Gets or sets the name of the site map provider.</summary>
    /// <value>The name of the site map provider.</value>
    public string SiteMapProviderName
    {
      get => this.siteMapProviderName;
      set => this.siteMapProviderName = value;
    }

    /// <summary>
    /// Gets or sets the identifier of the page that is selected if SelectionMode is SelectedPageChildren.
    /// </summary>
    /// <value>The identifier of the page that is selected if SelectionMode is SelectedPageChildren.</value>
    [Browsable(false)]
    public Guid SelectedPageId
    {
      get => this.selectedPageId;
      set => this.selectedPageId = value;
    }

    /// <summary>Gets the nodes collection.</summary>
    /// <returns>The collection.</returns>
    public IList<T> GetNodesCollection()
    {
      this.InitializeNavigationWidgetSettings();
      return this.Nodes;
    }

    /// <summary>
    /// Gets a collection of cached and changed items that need to be invalidated for the specific views that display all types inheriting from
    /// the abstract type <see cref="T:Telerik.Sitefinity.GenericContent.Model.Content" />.
    /// </summary>
    /// <returns>The cache dependency keys.</returns>
    public virtual IList<CacheDependencyKey> GetCacheDependencyObjects()
    {
      List<CacheDependencyKey> objects = new List<CacheDependencyKey>();
      foreach (string viewModelNodeId in this.viewModelNodeIds)
      {
        List<CacheDependencyKey> cacheDependencyKeyList1 = objects;
        CacheDependencyKey cacheDependencyKey1 = new CacheDependencyKey();
        cacheDependencyKey1.Type = NavigationModelBase<T>.CacheDependencyObjectForAllSitesType;
        cacheDependencyKey1.Key = viewModelNodeId;
        CacheDependencyKey cacheDependencyKey2 = cacheDependencyKey1;
        cacheDependencyKeyList1.Add(cacheDependencyKey2);
        string str = viewModelNodeId + (object) SystemManager.CurrentContext.Culture;
        List<CacheDependencyKey> cacheDependencyKeyList2 = objects;
        cacheDependencyKey1 = new CacheDependencyKey();
        cacheDependencyKey1.Type = NavigationModelBase<T>.CacheDependencyPageNodeStateChangeType;
        cacheDependencyKey1.Key = str;
        CacheDependencyKey cacheDependencyKey3 = cacheDependencyKey1;
        cacheDependencyKeyList2.Add(cacheDependencyKey3);
        List<CacheDependencyKey> cacheDependencyKeyList3 = objects;
        cacheDependencyKey1 = new CacheDependencyKey();
        cacheDependencyKey1.Type = NavigationModelBase<T>.CacheDependencyPageNodeObjectType;
        cacheDependencyKey1.Key = str;
        CacheDependencyKey cacheDependencyKey4 = cacheDependencyKey1;
        cacheDependencyKeyList3.Add(cacheDependencyKey4);
      }
      this.SubscribeCacheDependency(objects);
      return (IList<CacheDependencyKey>) objects;
    }

    /// <summary>
    ///     Initializes the settings for the navigation widget.
    /// </summary>
    public void InitializeNavigationWidgetSettings()
    {
      this.Nodes.Clear();
      this.viewModelNodeIds.Clear();
      SiteMapProvider provider = this.GetProvider();
      string selectionModeString = this.SelectionModeString;
      if (!(selectionModeString == "SelectedPageChildren"))
      {
        if (!(selectionModeString == "CurrentPageChildren"))
        {
          if (!(selectionModeString == "CurrentPageSiblings"))
          {
            if (!(selectionModeString == "SelectedPages"))
            {
              if (selectionModeString == "TopLevelPages")
                ;
              if (provider.RootNode != null)
                this.AddChildNodes(provider.RootNode, false);
            }
            else if (this.selectedPages != null)
            {
              string target = this.OpenExternalPageInNewTab ? "_blank" : "_self";
              foreach (SelectedPageModelBase selectedPage in this.selectedPages)
              {
                bool flag = selectedPage.NodeType == NodeType.Rewriting || selectedPage.NodeType == NodeType.InnerRedirect || selectedPage.NodeType == NodeType.OuterRedirect;
                if (selectedPage.Id != new Guid() && !selectedPage.IsExternal | flag)
                {
                  SiteMapNode siteMapNodeFromKey = provider.FindSiteMapNodeFromKey(selectedPage.Id.ToString("D"));
                  if (siteMapNodeFromKey != null && this.CheckSiteMapNode(siteMapNodeFromKey))
                  {
                    this.viewModelNodeIds.Add(siteMapNodeFromKey.Key);
                    if (siteMapNodeFromKey is PageSiteNode)
                      this.pageSiteNodes.Add((PageSiteNode) siteMapNodeFromKey);
                    this.Nodes.Add(this.CreateNodeViewModelRecursive(siteMapNodeFromKey, this.LevelsToInclude));
                  }
                }
                else
                {
                  T obj = this.InstantiateNodeViewModel(selectedPage.Url, target);
                  obj.Title = selectedPage.TitlesPath;
                  this.Nodes.Add(obj);
                }
              }
            }
          }
          else if (this.CurrentSiteMapNode != null)
          {
            SiteMapNode parentNode = this.CurrentSiteMapNode.ParentNode;
            if (parentNode != null)
              this.AddChildNodes(parentNode, this.ShowParentPage);
          }
        }
        else if (this.CurrentSiteMapNode != null)
          this.AddChildNodes(this.CurrentSiteMapNode, this.ShowParentPage);
      }
      else if (!object.Equals((object) this.selectedPageId, (object) Guid.Empty))
      {
        SiteMapNode siteMapNodeFromKey = provider.FindSiteMapNodeFromKey(this.selectedPageId.ToString("D"));
        if (siteMapNodeFromKey != null)
          this.AddChildNodes(siteMapNodeFromKey, this.ShowParentPage);
      }
      ((IEnumerable<IDataItem>) this.pageSiteNodes.Select<PageSiteNode, IRelatedDataHolder>((Func<PageSiteNode, IRelatedDataHolder>) (pn => pn.RelatedDataHolder))).SetRelatedDataSourceContext();
    }

    /// <summary>Gets the sitemap provider.</summary>
    /// <returns>
    ///     The <see cref="T:System.Web.SiteMapProvider" />.
    /// </returns>
    internal virtual SiteMapProvider GetProvider()
    {
      if (this.provider == null)
      {
        try
        {
          this.provider = SiteMapBase.GetSiteMapProvider(this.SiteMapProviderName);
        }
        catch (Exception ex)
        {
          this.provider = (SiteMapProvider) null;
          throw;
        }
      }
      return this.provider;
    }

    /// <summary>
    /// Adds the child nodes to the <see cref="P:Telerik.Sitefinity.Web.NavigationModelBase`1.Nodes" /> collection.
    /// </summary>
    /// <param name="startNode">The start node.</param>
    /// <param name="addParentNode">
    /// if set to <c>true</c> adds parent node.
    /// </param>
    protected void AddChildNodes(SiteMapNode startNode, bool addParentNode)
    {
      this.viewModelNodeIds.Add(startNode.Key);
      if (startNode is PageSiteNode)
        this.pageSiteNodes.Add((PageSiteNode) startNode);
      int? levelsToInclude = this.LevelsToInclude;
      int num = 0;
      if (levelsToInclude.GetValueOrDefault() == num & levelsToInclude.HasValue || startNode == null)
        return;
      if (addParentNode && this.CheckSiteMapNode(startNode) && startNode.Key != this.RootNodeId.ToString().ToUpperInvariant())
      {
        T viewModelRecursive = this.CreateNodeViewModelRecursive(startNode, this.LevelsToInclude);
        if ((object) viewModelRecursive == null)
          return;
        this.Nodes.Add(viewModelRecursive);
      }
      else
      {
        foreach (SiteMapNode childNode in startNode.ChildNodes)
        {
          T viewModelRecursive = this.CreateNodeViewModelRecursive(childNode, this.LevelsToInclude);
          if ((object) viewModelRecursive != null)
            this.Nodes.Add(viewModelRecursive);
        }
      }
    }

    /// <summary>Checks the site map node.</summary>
    /// <param name="node">The node.</param>
    /// <returns>
    /// The <see cref="T:System.Boolean" />.
    /// </returns>
    protected virtual bool CheckSiteMapNode(SiteMapNode node) => RouteHelper.CheckSiteMapNode(node);

    /// <summary>Gets the link target.</summary>
    /// <param name="node">The node.</param>
    /// <returns>
    /// The <see cref="T:System.String" />.
    /// </returns>
    protected virtual string GetLinkTarget(SiteMapNode node)
    {
      string linkTarget = NavigationUtilities.GetLinkTarget((object) node);
      if (linkTarget.IsNullOrEmpty())
        linkTarget = "_self";
      return linkTarget;
    }

    /// <summary>Gets the root node identifier.</summary>
    /// <returns>
    ///     The <see cref="T:System.Guid" />.
    /// </returns>
    protected virtual Guid RootNodeId => SiteInitializer.CurrentFrontendRootNodeId;

    /// <summary>Resolves the URL.</summary>
    /// <param name="node">The node.</param>
    /// <returns>
    /// The <see cref="T:System.String" />.
    /// </returns>
    protected virtual string ResolveUrl(SiteMapNode node) => NavigationUtilities.ResolveUrl(node, new bool?(), (string[]) null);

    /// <summary>Instantiates a node view model.</summary>
    /// <param name="node">The node.</param>
    /// <returns>An instance of a node view model.</returns>
    protected virtual T InstantiateNodeViewModel(SiteMapNode node)
    {
      bool isCurrentlyOpened = this.CurrentSiteMapNode != null && this.CurrentSiteMapNode.Key == node.Key;
      string url = this.ResolveUrl(node);
      string linkTarget = this.GetLinkTarget(node);
      return new FrontendPageNode(node, url, linkTarget, isCurrentlyOpened, this.HasSelectedChild(node)) as T;
    }

    /// <summary>Instantiates a node view model.</summary>
    /// <param name="url">The URL.</param>
    /// <param name="target">The target.</param>
    /// <returns>An instance of a node view model.</returns>
    [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings", Justification = "By design", MessageId = "0#")]
    protected virtual T InstantiateNodeViewModel(string url, string target) => new FrontendPageNode((SiteMapNode) null, url, target, false, false) as T;

    /// <summary>
    /// Creates the <see cref="!:NodeViewModel" /> from the SiteMapNode and populates recursive their child nodes.
    /// </summary>
    /// <param name="node">The original site map node.</param>
    /// <param name="levelsToInclude">The levels to include.</param>
    /// <returns>
    /// The <see cref="!:NodeViewModel" />.
    /// </returns>
    private T CreateNodeViewModelRecursive(SiteMapNode node, int? levelsToInclude)
    {
      int? nullable1 = levelsToInclude;
      int num = 0;
      if (nullable1.GetValueOrDefault() == num & nullable1.HasValue || !this.CheckSiteMapNode(node))
        return default (T);
      this.viewModelNodeIds.Add(node.Key);
      if (node is PageSiteNode)
        this.pageSiteNodes.Add((PageSiteNode) node);
      T viewModelRecursive1 = this.InstantiateNodeViewModel(node);
      int? nullable2 = levelsToInclude;
      levelsToInclude = nullable2.HasValue ? new int?(nullable2.GetValueOrDefault() - 1) : new int?();
      foreach (SiteMapNode childNode in node.ChildNodes)
      {
        T viewModelRecursive2 = this.CreateNodeViewModelRecursive(childNode, levelsToInclude);
        if ((object) viewModelRecursive2 != null)
          viewModelRecursive1.ChildNodes.Add(viewModelRecursive2);
      }
      return viewModelRecursive1;
    }

    /// <summary>
    /// Determines whether the current node is descendant of the <see cref="T:System.Web.SiteMapNode" /> instance.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <returns>
    /// The <see cref="T:System.Boolean" />.
    /// </returns>
    protected bool HasSelectedChild(SiteMapNode node) => this.CurrentSiteMapNode != null && this.CurrentSiteMapNode.IsDescendantOf(node);

    private SiteMapNode GetCurrentPageNode(string currentPageNodeId)
    {
      if (currentPageNodeId != null)
      {
        SiteMapNode siteMapNodeFromKey = SiteMapBase.GetCurrentProvider().FindSiteMapNodeFromKey(currentPageNodeId);
        if (siteMapNodeFromKey != null)
          return siteMapNodeFromKey;
      }
      return (SiteMapNode) null;
    }

    private void SubscribeCacheDependency(List<CacheDependencyKey> objects)
    {
      if (!SystemManager.CurrentHttpContext.Items.Contains((object) "PageNodesCacheDependencyName"))
        SystemManager.CurrentHttpContext.Items.Add((object) "PageNodesCacheDependencyName", (object) new List<CacheDependencyKey>());
      ((List<CacheDependencyKey>) SystemManager.CurrentHttpContext.Items[(object) "PageNodesCacheDependencyName"]).AddRange((IEnumerable<CacheDependencyKey>) objects);
    }
  }
}
