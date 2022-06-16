// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.NavigationControls.LightNavigationControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.UrlLocalizationStrategies;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.ResponsiveDesign;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.RelatedData.Web.UI;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.NavigationControls.Cache;

namespace Telerik.Sitefinity.Web.UI.NavigationControls
{
  /// <summary>Shows navigation based on a sitemap.</summary>
  [ControlTemplateInfo("Labels", "Navigation", "Navigation")]
  [ControlDesigner(typeof (LightNavigationDesigner))]
  [RequiresLayoutTransformation]
  [IndexRenderMode(IndexRenderModes.NoOutput)]
  public class LightNavigationControl : SimpleView, IHasCacheDependency, IRelatedDataView
  {
    private Collection<SelectedPage> selectedPages;
    private CollectionJsonTypeConverter<SelectedPage> selectedPagesConverter;
    private List<SiteMapNode> accessedNodes = new List<SiteMapNode>();
    private List<SiteMapNode> childNodes = new List<SiteMapNode>();
    private List<CacheDependencyKey> cacheDependencyNotifiedObjects = new List<CacheDependencyKey>();
    private string startingNodeResolvedUrl;
    private SiteMapProvider provider;
    private NavigationOutputCacheVariation outputCacheVariation;
    private NavigationOutputCacheVariationSettings outputCacheSettings;
    private SiteMapNode parentNode;
    private string siteMapProviderName = "SitefinitySiteMap";
    internal const string horizontalTemplateName = "Telerik.Sitefinity.Resources.Templates.Frontend.Navigation.HorizontalList.ascx";
    internal const string horizontalWithDropDownMenusTemplateName = "Telerik.Sitefinity.Resources.Templates.Frontend.Navigation.HorizontalWithDropDownMenusList.ascx";
    internal const string horizontalWithTabsTemplateName = "Telerik.Sitefinity.Resources.Templates.Frontend.Navigation.HorizontalWithTabsList.ascx";
    internal const string verticalTemplateName = "Telerik.Sitefinity.Resources.Templates.Frontend.Navigation.VerticalList.ascx";
    internal const string verticalWithSubLevelsTemplateName = "Telerik.Sitefinity.Resources.Templates.Frontend.Navigation.VerticalWithSubLevelsList.ascx";
    internal const string sitemapInColumnsTemplateName = "Telerik.Sitefinity.Resources.Templates.Frontend.Navigation.SitemapInColumnsList.ascx";
    internal const string sitemapInRowsTemplateName = "Telerik.Sitefinity.Resources.Templates.Frontend.Navigation.SitemapInRowsList.ascx";
    internal const string toggleMenuTemplateName = "Telerik.Sitefinity.Resources.Templates.Frontend.Navigation.ToggleMenu.ascx";
    internal const string dropdownTemplateName = "Telerik.Sitefinity.Resources.Templates.Frontend.Navigation.Dropdown.ascx";
    internal static readonly string horizontalTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Frontend.Navigation.HorizontalList.ascx");
    internal static readonly string horizontalWithDropDownMenusTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Frontend.Navigation.HorizontalWithDropDownMenusList.ascx");
    internal static readonly string horizontalWithTabsTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Frontend.Navigation.HorizontalWithTabsList.ascx");
    internal static readonly string verticalTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Frontend.Navigation.VerticalList.ascx");
    internal static readonly string verticalWithSubLevelsTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Frontend.Navigation.VerticalWithSubLevelsList.ascx");
    internal static readonly string sitemapInColumnsTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Frontend.Navigation.SitemapInColumnsList.ascx");
    internal static readonly string sitemapInRowsTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Frontend.Navigation.SitemapInRowsList.ascx");
    internal const string horizontalFriendlyName = "Horizontal (one-level)";
    internal const string horizontalWithDropDownMenusFriendlyName = "Horizontal with dropdown menus";
    internal const string horizontalWithTabsFriendlyName = "Horizontal with tabs (up to 2 levels)";
    internal const string verticalFriendlyName = "Vertical (one-level)";
    internal const string verticalWithSubLevelsFriendlyName = "Vertical with sublevels (treeview)";
    internal const string sitemapInColumnsFriendlyName = "Sitemap in columns (up to 2 levels)";
    internal const string sitemapInRowsFriendlyName = "Sitemap in rows (up to 2 levels)";
    internal const string toggleMenuFriendlyName = "Toggle menu";
    internal const string dropdownFriendlyName = "Dropdown";
    private RelatedDataDefinition relatedDataDefinition;

    /// <summary>Gets or sets the title of the control.</summary>
    /// <value>The title of the control.</value>
    public virtual string Title { get; set; }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.
    /// </returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? LightNavigationControl.horizontalTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets or sets the full type name of the navigation control.
    /// </summary>
    public string ControlType => typeof (LightNavigationControl).FullName;

    /// <summary>
    /// Gets or sets the page links to display selection mode.
    /// </summary>
    /// <value>The page display mode.</value>
    public PageSelectionModes SelectionMode { get; set; }

    /// <summary>Gets or sets the name of the site map provider.</summary>
    /// <value>The name of the site map provider.</value>
    public string SiteMapProviderName
    {
      get => this.siteMapProviderName;
      set => this.siteMapProviderName = value;
    }

    /// <summary>Gets or sets whether to show parent page</summary>
    public bool ShowParentPage { get; set; }

    /// <summary>Gets or sets the starting node URL.</summary>
    public string StartingNodeUrl { get; set; }

    /// <summary>Gets or sets the custom selected pages internal.</summary>
    [TypeConverter(typeof (CollectionJsonTypeConverter<SelectedPage>))]
    protected Collection<SelectedPage> CustomSelectedPagesInternal
    {
      get
      {
        if (this.selectedPages == null)
          this.selectedPages = new Collection<SelectedPage>();
        return this.selectedPages;
      }
      set
      {
        this.selectedPages = value;
        this.ChildControlsCreated = false;
      }
    }

    /// <summary>Gets or sets the selected page ids.</summary>
    public string[] SelectedPageIds
    {
      get => this.CustomSelectedPagesInternal.Select<SelectedPage, string>((Func<SelectedPage, string>) (x => x.Id)).ToArray<string>();
      set => this.CustomSelectedPagesInternal = new Collection<SelectedPage>((IList<SelectedPage>) ((IEnumerable<string>) value).Select<string, SelectedPage>((Func<string, SelectedPage>) (x => new SelectedPage()
      {
        Id = x
      })).ToList<SelectedPage>());
    }

    /// <summary>
    /// Stores CustomSelectedPagesInternal in an asp.net parser-friendly way
    /// Use CustomSelectedPagesInternal instead in the code-behind
    /// </summary>
    public string CustomSelectedPages
    {
      get => this.SelectedPagesConverter.ConvertToString((object) this.CustomSelectedPagesInternal);
      set => this.selectedPages = this.SelectedPagesConverter.ConvertFrom((object) value) as Collection<SelectedPage>;
    }

    private CollectionJsonTypeConverter<SelectedPage> SelectedPagesConverter
    {
      get
      {
        if (this.selectedPagesConverter == null)
          this.selectedPagesConverter = new CollectionJsonTypeConverter<SelectedPage>();
        return this.selectedPagesConverter;
      }
    }

    /// <summary>Gets the name of the selected page qualified.</summary>
    public string SelectedPageQualifiedName => typeof (SelectedPage).FullName;

    /// <summary>Gets the selected page title.</summary>
    /// <value>The selected page title.</value>
    public string SelectedPageTitle
    {
      get
      {
        string selectedPageTitle = string.Empty;
        if (!this.StartingNodeUrl.IsNullOrEmpty())
        {
          CultureInfo currentUiCulture = this.GetCurrentUiCulture();
          if (this.GetProvider().FindSiteMapNodeFromKey(this.StartingNodeUrl) is PageSiteNode siteMapNodeFromKey)
            selectedPageTitle = siteMapNodeFromKey.GetTitlesHierarchy(currentUiCulture, true).FirstOrDefault<string>();
        }
        return selectedPageTitle;
      }
    }

    /// <returns>The CSS class rendered by the Web server control on the client. The default is <see cref="F:System.String.Empty" />.</returns>
    public override string CssClass
    {
      get => base.CssClass;
      set => base.CssClass = value;
    }

    /// <summary>Gets or sets the levels to include.</summary>
    public virtual int? LevelsToInclude { get; set; }

    [TypeConverter(typeof (ExpandableObjectConverter))]
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public NavigationOutputCacheVariationSettings OutputCache
    {
      get
      {
        if (this.outputCacheSettings == null)
          this.outputCacheSettings = new NavigationOutputCacheVariationSettings();
        return this.outputCacheSettings;
      }
      set => this.outputCacheSettings = value;
    }

    /// <summary>Gets the data source.</summary>
    protected virtual SitefinitySiteMapDataSource DataSource => this.Container.GetControl<SitefinitySiteMapDataSource>("dataSource", true);

    /// <summary>
    /// Gets the reference to the control that represents the title of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value>The control displaying the title of the item.</value>
    protected virtual SitefinityLabel TitleLabel => this.Container.GetControl<SitefinityLabel>("title", false);

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      try
      {
        this.Container.DataBind();
      }
      catch (FileNotFoundException ex)
      {
        Exceptions.HandleException((Exception) ex, ExceptionPolicyName.IgnoreExceptions);
      }
      this.SubsribeCacheDependency();
    }

    /// <summary>
    /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
    /// </summary>
    protected override void CreateChildControls()
    {
      try
      {
        if (this.OutputCache.VaryByAuthenticationStatus || this.OutputCache.VaryByUserRoles)
        {
          this.outputCacheVariation = new NavigationOutputCacheVariation(this.OutputCache);
          PageRouteHandler.RegisterCustomOutputCacheVariation((ICustomOutputCacheVariation) this.outputCacheVariation);
        }
        base.CreateChildControls();
      }
      catch (FileNotFoundException ex)
      {
        this.ProcessInitializationException((Exception) ex);
      }
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.InitializeSiteMapDataSource();
      if (this.TitleLabel == null || this.DataSource.Nodes.Count <= 0)
        return;
      this.TitleLabel.Text = this.Title;
    }

    /// <summary>Processes the initialization exception.</summary>
    /// <param name="ex">The exception.</param>
    protected virtual void ProcessInitializationException(Exception ex)
    {
      if (this.IsBackend())
        this.Controls.Add((Control) new Label()
        {
          Text = Res.Get<ErrorMessages>().ErrorParsingTheTemplate
        });
      Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions);
    }

    /// <summary>Initializes the site map data source.</summary>
    private void InitializeSiteMapDataSource()
    {
      SitefinitySiteMapDataSource dataSource = this.DataSource;
      dataSource.OnGetChildNodesDelegate += new SitefinitySiteMapDataSource.OnNodeAccessed(this.currentSiteMapDataSource_OnGetChildNodesDelegate);
      dataSource.OnNodePropAccessed += new SitefinitySiteMapDataSource.OnNodeAccessed(this.currentSiteMapDataSource_OnNodePropAccessed);
      dataSource.Provider = this.GetProvider();
      dataSource.LevelsToInclude = this.LevelsToInclude;
      dataSource.ShowStartingNode = false;
      dataSource.Nodes = (IList<SiteMapNode>) new List<SiteMapNode>();
      if (this.DisplayRelatedData())
      {
        IEnumerable liveContentLinks = this.GetRelatedLiveContentLinks();
        dataSource.DoNotLoadNodesWhenEmpty = true;
        List<SiteMapNode> siteMapNodeList = new List<SiteMapNode>();
        foreach (object obj in liveContentLinks)
        {
          if (obj is ContentLink contentLink)
          {
            SiteMapNode node = dataSource.Provider.FindSiteMapNodeFromKey(contentLink.ChildItemId.ToString());
            if (node != null)
            {
              this.accessedNodes.Add(node);
              SiteMapNode specificLanguage = ((SiteMapBase) this.GetProvider()).FindSiteMapNodeForSpecificLanguage(node, SystemManager.CurrentContext.Culture);
              if (specificLanguage == null || RouteHelper.CheckSiteMapNode(specificLanguage))
                node = specificLanguage;
              else
                continue;
            }
            if (node != null)
              siteMapNodeList.Add(node);
          }
        }
        dataSource.Nodes = (IList<SiteMapNode>) siteMapNodeList;
      }
      else
      {
        SiteMapProvider provider = dataSource.Provider;
        bool flag = provider != null && this.ShowParentPage;
        this.DataSource.AddParentItem = flag;
        SiteMapNode siteMapNode1 = provider.FindSiteMapNode(this.Context);
        switch (this.SelectionMode)
        {
          case PageSelectionModes.SelectedPageChildren:
            string startingNodeResolvedUrl = this.StartingNodeResolvedUrl;
            if (startingNodeResolvedUrl != null)
            {
              dataSource.StartingNodeUrl = startingNodeResolvedUrl;
              SiteMapNode siteMapNode2 = dataSource.Provider.FindSiteMapNode(startingNodeResolvedUrl);
              if (siteMapNode2 != null)
              {
                if (siteMapNode1 != null)
                  this.childNodes.Add(siteMapNode1.ParentNode);
                this.accessedNodes.Add(siteMapNode2);
              }
              if (!flag || siteMapNode2 == null || !(siteMapNode2.Key != SiteInitializer.CurrentFrontendRootNodeId.ToString().ToUpperInvariant()))
                break;
              this.parentNode = siteMapNode2;
              dataSource.ParentNode = this.parentNode;
              break;
            }
            dataSource.StartingNodeUrl = "";
            break;
          case PageSelectionModes.SelectedPages:
            List<SiteMapNode> siteMapNodeList = new List<SiteMapNode>();
            foreach (SelectedPage selectedPage in this.CustomSelectedPagesInternal)
            {
              SiteMapNode node;
              if (selectedPage.IsExternal)
              {
                node = (SiteMapNode) new ExternalPageSiteNode(dataSource.Provider, "ext", selectedPage.Url, selectedPage.Title)
                {
                  Attributes = {
                    {
                      "target",
                      "blank"
                    }
                  }
                };
              }
              else
              {
                node = dataSource.Provider.FindSiteMapNodeFromKey(selectedPage.Id);
                if (node != null)
                {
                  this.accessedNodes.Add(node);
                  SiteMapNode specificLanguage = ((SiteMapBase) this.GetProvider()).FindSiteMapNodeForSpecificLanguage(node, SystemManager.CurrentContext.Culture);
                  if (specificLanguage == null || RouteHelper.CheckSiteMapNode(specificLanguage))
                    node = specificLanguage;
                  else
                    continue;
                }
              }
              if (node != null)
                siteMapNodeList.Add(node);
            }
            dataSource.Nodes = (IList<SiteMapNode>) siteMapNodeList;
            break;
          case PageSelectionModes.CurrentPageChildren:
            dataSource.StartFromCurrentNode = true;
            if (flag && siteMapNode1 != null)
            {
              this.parentNode = siteMapNode1;
              dataSource.ParentNode = this.parentNode;
            }
            if (siteMapNode1 == null)
              break;
            this.accessedNodes.Add(siteMapNode1);
            this.childNodes.Add(siteMapNode1);
            break;
          case PageSelectionModes.CurrentPageSiblings:
            dataSource.StartFromCurrentNode = true;
            dataSource.StartingNodeOffset = -1;
            if (flag && siteMapNode1 != null)
            {
              SiteMapNode parentNode = siteMapNode1.ParentNode;
              if (parentNode != null && parentNode.Key != SiteInitializer.CurrentFrontendRootNodeId.ToString().ToUpperInvariant())
              {
                this.parentNode = parentNode;
                dataSource.ParentNode = this.parentNode;
              }
            }
            if (siteMapNode1 == null || siteMapNode1.ParentNode == null)
              break;
            this.childNodes.Add(siteMapNode1.ParentNode);
            this.accessedNodes.Add(siteMapNode1.ParentNode);
            break;
        }
      }
    }

    private void currentSiteMapDataSource_OnNodePropAccessed(SiteMapNode Node) => this.accessedNodes.Add(Node);

    private void currentSiteMapDataSource_OnGetChildNodesDelegate(SiteMapNode Node) => this.childNodes.Add(Node);

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

    /// <summary>
    /// Renders the HTML opening tag of the control to the specified writer. This method is used primarily by control developers.
    /// </summary>
    /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    public override void RenderBeginTag(HtmlTextWriter writer)
    {
    }

    /// <summary>
    /// Renders the HTML closing tag of the control into the specified writer. This method is used primarily by control developers.
    /// </summary>
    /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    public override void RenderEndTag(HtmlTextWriter writer)
    {
    }

    protected override void Render(HtmlTextWriter writer)
    {
      if (this.GetIndexRenderMode() != IndexRenderModes.Normal)
        return;
      base.Render(writer);
    }

    /// <summary>
    /// Gets a collection of cached and changed items that need to be invalidated for the specific views that display all types inheriting from
    /// the abstract type <see cref="T:Telerik.Sitefinity.GenericContent.Model.Content" />.
    /// </summary>
    /// <returns></returns>
    public virtual IList<CacheDependencyKey> GetCacheDependencyObjects()
    {
      if (this.parentNode != null)
        this.accessedNodes.Add(this.parentNode);
      foreach (SiteMapNode node in this.accessedNodes.ToList<SiteMapNode>())
      {
        this.cacheDependencyNotifiedObjects.Add(this.BuildCacheDependencyKey(node, typeof (CacheDependencyPageNodeStateChange)));
        List<CacheDependencyKey> dependencyNotifiedObjects1 = this.cacheDependencyNotifiedObjects;
        CacheDependencyKey cacheDependencyKey1 = new CacheDependencyKey();
        cacheDependencyKey1.Type = typeof (CacheDependencyObjectForAllSites);
        cacheDependencyKey1.Key = node.Key.ToString();
        CacheDependencyKey cacheDependencyKey2 = cacheDependencyKey1;
        dependencyNotifiedObjects1.Add(cacheDependencyKey2);
        this.cacheDependencyNotifiedObjects.Add(this.BuildCacheDependencyKey(node, typeof (CacheDependencyPageNodeObject)));
        if (node is PageSiteNode page && page.NodeType == NodeType.Group)
        {
          SiteMapNode firstChildNode = this.FindFirstChildNode((SiteMapNode) page);
          if (firstChildNode != null)
          {
            this.cacheDependencyNotifiedObjects.Add(this.BuildCacheDependencyKey(firstChildNode, typeof (CacheDependencyPageNodeStateChange)));
            List<CacheDependencyKey> dependencyNotifiedObjects2 = this.cacheDependencyNotifiedObjects;
            cacheDependencyKey1 = new CacheDependencyKey();
            cacheDependencyKey1.Type = typeof (CacheDependencyObjectForAllSites);
            cacheDependencyKey1.Key = firstChildNode.Key.ToString();
            CacheDependencyKey cacheDependencyKey3 = cacheDependencyKey1;
            dependencyNotifiedObjects2.Add(cacheDependencyKey3);
          }
        }
      }
      foreach (SiteMapNode node in this.childNodes.ToList<SiteMapNode>())
      {
        this.cacheDependencyNotifiedObjects.Add(this.BuildCacheDependencyKey(node, typeof (CacheDependencyPageNodeStateChange)));
        this.cacheDependencyNotifiedObjects.Add(new CacheDependencyKey()
        {
          Type = typeof (CacheDependencyObjectForAllSites),
          Key = node.Key.ToString()
        });
      }
      return (IList<CacheDependencyKey>) this.cacheDependencyNotifiedObjects;
    }

    internal SiteMapNode FindFirstChildNode(SiteMapNode page)
    {
      foreach (SiteMapNode childNode in page.ChildNodes)
      {
        if (childNode is PageSiteNode && ((PageSiteNode) childNode).NodeType != NodeType.Group && this.provider.IsAccessibleToUser(this.Context, childNode))
          return childNode;
      }
      foreach (SiteMapNode childNode in page.ChildNodes)
      {
        if (childNode is PageSiteNode && ((PageSiteNode) childNode).NodeType == NodeType.Group)
        {
          SiteMapNode firstChildNode = this.FindFirstChildNode(childNode);
          if (firstChildNode != null)
            return firstChildNode;
        }
      }
      return (SiteMapNode) null;
    }

    internal CacheDependencyKey BuildCacheDependencyKey(
      SiteMapNode node,
      Type objectType)
    {
      CacheDependencyKey cacheDependencyKey;
      if (SystemManager.CurrentContext.AppSettings.Multilingual)
        cacheDependencyKey = new CacheDependencyKey()
        {
          Type = objectType,
          Key = node.Key + (object) SystemManager.CurrentContext.Culture
        };
      else
        cacheDependencyKey = new CacheDependencyKey()
        {
          Type = objectType,
          Key = node.Key
        };
      return cacheDependencyKey;
    }

    /// <summary>Resolves the page URL.</summary>
    /// <param name="pageUrl">The page URL.</param>
    /// <returns></returns>
    internal virtual string StartingNodeResolvedUrl
    {
      get
      {
        if (this.startingNodeResolvedUrl == null && this.StartingNodeUrl.IsGuid())
        {
          SiteMapProvider provider = this.GetProvider();
          if (provider != null)
          {
            if (!(provider.FindSiteMapNodeFromKey(this.StartingNodeUrl) is PageSiteNode siteMapNodeFromKey))
              return (string) null;
            string url = siteMapNodeFromKey.GetUrl(SystemManager.CurrentContext.Culture, true);
            if (ObjectFactory.Resolve<UrlLocalizationService>().UnResolveUrl(url) == "~/")
              return (string) null;
            this.startingNodeResolvedUrl = url.Replace("~", "");
          }
        }
        return this.startingNodeResolvedUrl;
      }
    }

    /// <summary>Gets the sitemap provider.</summary>
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
        }
      }
      return this.provider;
    }

    private CultureInfo GetCurrentUiCulture()
    {
      string name = HttpUtility.ParseQueryString(this.Context.Request.Url.Query)["propertyValueCulture"];
      return name.IsNullOrEmpty() ? SystemManager.CurrentContext.Culture : CultureInfo.GetCultureInfo(name);
    }

    /// <summary>Gets the ResourceLinks controls.</summary>
    /// <returns></returns>
    public IEnumerable<ResourceLinks> GetResourceLinks()
    {
      List<ResourceLinks> source = new List<ResourceLinks>();
      foreach (ResourceLinks resourceLinks in this.Container.GetControls<ResourceLinks>().Values)
        source.Add(resourceLinks);
      return source.AsEnumerable<ResourceLinks>();
    }

    /// <inheritdoc />
    public bool? DisplayRelatedData { get; set; }

    /// <inheritdoc />
    [TypeConverter(typeof (ExpandableObjectConverter))]
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public RelatedDataDefinition RelatedDataDefinition
    {
      get
      {
        if (this.relatedDataDefinition == null)
          this.relatedDataDefinition = new RelatedDataDefinition();
        return this.relatedDataDefinition;
      }
      set => this.relatedDataDefinition = value;
    }

    /// <inheritdoc />
    public string GetContentType() => this.ControlType;

    /// <inheritdoc />
    public string GetProviderName() => ManagerBase<PageDataProvider>.GetDefaultProviderName();

    /// <inheritdoc />
    public string UrlKeyPrefix { get; set; }
  }
}
