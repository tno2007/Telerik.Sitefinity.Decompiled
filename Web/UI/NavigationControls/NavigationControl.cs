// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.NavigationControls.NavigationControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.UrlLocalizationStrategies;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Web.UI;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Web;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.NavigationControls
{
  /// <summary>
  /// Shows navigation based on a sitemap. Supports predefined navigational modes like Menu,Tree,Tab
  /// </summary>
  [ControlDesigner(typeof (NavigationDesigner))]
  [PropertyEditorTitle(typeof (Labels), "NavigationDesignerTitle")]
  [RequireScriptManager(true)]
  public class NavigationControl : SimpleView, IScriptControl, IHasCacheDependency
  {
    internal SiteMapDataSource currentSiteMapDataSource;
    private List<SiteMapNode> accessedNodes = new List<SiteMapNode>();
    private List<SiteMapNode> childNodes = new List<SiteMapNode>();
    private string startingNodeResolvedUrl;
    private SiteMapProvider provider;
    private List<CacheDependencyKey> cacheDependencyNotifiedObjects = new List<CacheDependencyKey>();
    public static readonly string defaultEmbeddedLayoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.PublicControls.NavigationControl.ascx");
    private Collection<SelectedPage> selectedPages;
    private CollectionJsonTypeConverter<SelectedPage> selectedPagesConverter;
    private bool highlightPath = true;
    private bool groupPageIsLink = true;
    private SiteMapNode parentNode;
    private string siteMapProviderName = "SitefinitySiteMap";

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.
    /// </returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Gets or sets the template of the field control.</summary>
    /// <value>The template.</value>
    protected virtual ConditionalTemplateContainer ConditionalTemplates { get; set; }

    /// <summary>Gets or sets the current navigation control.</summary>
    /// <value>The current navigation control.</value>
    internal virtual BaseDataBoundControl CurrentNavigationControl { get; set; }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets or sets the sitemap levels to bind.</summary>
    /// <value>The levels to bind.</value>
    public int MaxDataBindDepth { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether [allow collapsing].
    /// </summary>
    /// <value><c>true</c> if [allow collapsing]; otherwise, <c>false</c>.</value>
    public bool AllowCollapsing { get; set; }

    /// <summary>Gets or sets the navigation action.</summary>
    /// <value>The navigation action.</value>
    public NavigationAction NavigationAction { get; set; }

    /// <summary>Gets or sets the sitemap levels to expand.</summary>
    /// <value>The levels to expand.</value>
    public int LevelsToExpand { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether [show expanded].
    /// </summary>
    /// <value><c>true</c> if [show expanded]; otherwise, <c>false</c>.</value>
    public bool ShowExpanded { get; set; }

    /// <summary>Gets or sets whether to highlight item's path</summary>
    public bool HighlightPath
    {
      get => this.highlightPath;
      set => this.highlightPath = value;
    }

    /// <summary>
    /// Gets or sets whether the GroupPage to be a link or will expand/collapse on click.
    /// </summary>
    public bool GroupPageIsLink
    {
      get => this.groupPageIsLink;
      set => this.groupPageIsLink = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether column/row headings should be links
    /// </summary>
    public bool HeadingsShouldBeLinks { get; set; }

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

    /// <summary>Gets or sets the root page pageId(Guid) or url.</summary>
    /// <value>The root page pageId.</value>
    public string StartingNodeUrl { get; set; }

    /// <summary>Gets or sets the name of the site map provider.</summary>
    /// <value>The name of the site map provider.</value>
    public string SiteMapProviderName
    {
      get => this.siteMapProviderName;
      set => this.siteMapProviderName = value;
    }

    [TypeConverter(typeof (CollectionJsonTypeConverter<SelectedPage>))]
    protected Collection<SelectedPage> CustomSelectedPagesInternal
    {
      get
      {
        if (this.selectedPages == null)
          this.selectedPages = new Collection<SelectedPage>();
        return this.selectedPages;
      }
      set => this.selectedPages = value;
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

    public string SelectedPageQualifiedName => typeof (SelectedPage).FullName;

    /// <summary>
    /// Gets or sets the navigation mode,e.g. tree , menu, hroizontal, vertical
    /// </summary>
    /// <value>The navigation mode.</value>
    public NavigationModes NavigationMode { get; set; }

    /// <summary>
    /// Gets or sets the page links to display selection mode.
    /// </summary>
    /// <value>The page display mode.</value>
    public PageSelectionModes SelectionMode { get; set; }

    /// <summary>Gets or sets whether to show parent page</summary>
    public bool ShowParentPage { get; set; }

    /// <summary>
    /// Gets or sets the path of the external template to be used by the dialog.
    /// </summary>
    /// <value></value>
    [Browsable(false)]
    public override string LayoutTemplatePath
    {
      get
      {
        string layoutTemplatePath = (string) null;
        switch (this.NavigationMode)
        {
          case NavigationModes.HorizontalSimple:
            layoutTemplatePath = this.CustomTemplateHorizontalSimple;
            break;
          case NavigationModes.HorizontalDropDownMenu:
            layoutTemplatePath = this.CustomTemplateHorizontalDropDownMenu;
            break;
          case NavigationModes.HorizontalTabs:
            layoutTemplatePath = this.CustomTemplateHorizontalTabs;
            break;
          case NavigationModes.VerticalSimple:
            layoutTemplatePath = this.CustomTemplateVerticalSimple;
            break;
          case NavigationModes.VerticalTree:
            layoutTemplatePath = this.CustomTemplateVerticalTree;
            break;
          case NavigationModes.SiteMapInColumns:
            layoutTemplatePath = this.CustomTemplateSiteMapInColumns;
            break;
          case NavigationModes.SiteMapInRows:
            layoutTemplatePath = this.CustomTemplateSiteMapInRows;
            break;
          case NavigationModes.CustomNavigation:
            layoutTemplatePath = this.CustomTemplateCustomNavigation;
            break;
        }
        if (layoutTemplatePath == null)
          layoutTemplatePath = this.CustomLayoutTemplatePath;
        if (string.IsNullOrWhiteSpace(layoutTemplatePath) && string.IsNullOrWhiteSpace(this.LayoutTemplateName))
          layoutTemplatePath = NavigationControl.defaultEmbeddedLayoutTemplatePath;
        return layoutTemplatePath;
      }
      set
      {
      }
    }

    /// <summary>Gets or sets the custom layout template path.</summary>
    /// <value>The custom layout template path.</value>
    public string CustomLayoutTemplatePath { get; set; }

    /// <summary>
    /// Gets or sets the custom template fiel path for horizontal simple mode
    /// </summary>
    public string CustomTemplateHorizontalSimple { get; set; }

    /// <summary>
    /// Gets or sets the custom template path for horizontal drop down menu mode.
    /// </summary>
    public string CustomTemplateHorizontalDropDownMenu { get; set; }

    /// <summary>
    /// Gets or sets the custom template path for horizontal tabs mode.
    /// </summary>
    public string CustomTemplateHorizontalTabs { get; set; }

    /// <summary>
    /// Gets or sets the custom template file path for vertical simple mode.
    /// </summary>
    public string CustomTemplateVerticalSimple { get; set; }

    /// <summary>
    /// Gets or sets the custom template file path for vertical tree mode.
    /// </summary>
    public string CustomTemplateVerticalTree { get; set; }

    /// <summary>
    /// Gets or sets the custom template file path for the site map in columns mode.
    /// </summary>
    public string CustomTemplateSiteMapInColumns { get; set; }

    /// <summary>
    /// Gets or sets the custom template file path for the site map in rows mode.
    /// </summary>
    public string CustomTemplateSiteMapInRows { get; set; }

    /// <summary>
    /// Gets or sets the custom template path for custom navigation  mode.
    /// </summary>
    public string CustomTemplateCustomNavigation { get; set; }

    /// <summary>
    /// Gets or sets the skin applied if the navigational control is telerik ISkinnableControl.
    /// </summary>
    /// <value>The skin.</value>
    public string Skin { get; set; }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.InitializeCurrentNavigationControl();
      this.InitializeSiteMapDataSource();
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

    private void InitializeCurrentNavigationControl()
    {
      this.CurrentNavigationControl = this.Container.GetControl<BaseDataBoundControl>();
      if (this.CurrentNavigationControl == null)
        throw new ArgumentException("Unable to find a BaseDataBoundControl derived control in the template");
    }

    /// <summary>Initializes the site map data source.</summary>
    internal void InitializeSiteMapDataSource()
    {
      this.currentSiteMapDataSource = (SiteMapDataSource) new SitefinitySiteMapDataSource();
      this.currentSiteMapDataSource.Provider = this.GetProvider();
      ((SitefinitySiteMapDataSource) this.currentSiteMapDataSource).OnGetChildNodesDelegate += new SitefinitySiteMapDataSource.OnNodeAccessed(this.currentSiteMapDataSource_OnGetChildNodesDelegate);
      ((SitefinitySiteMapDataSource) this.currentSiteMapDataSource).OnNodePropAccessed += new SitefinitySiteMapDataSource.OnNodeAccessed(this.currentSiteMapDataSource_OnNodePropAccessed);
      this.currentSiteMapDataSource.ShowStartingNode = false;
      SiteMapProvider provider = this.currentSiteMapDataSource.Provider;
      bool flag = provider != null && this.ShowParentPage;
      switch (this.SelectionMode)
      {
        case PageSelectionModes.SelectedPageChildren:
          string startingNodeResolvedUrl = this.StartingNodeResolvedUrl;
          if (startingNodeResolvedUrl != null)
          {
            this.currentSiteMapDataSource.StartingNodeUrl = startingNodeResolvedUrl;
            if (flag)
            {
              SiteMapNode siteMapNode = this.currentSiteMapDataSource.Provider.FindSiteMapNode(startingNodeResolvedUrl);
              if (siteMapNode != null)
              {
                this.childNodes.Add(provider.CurrentNode.ParentNode);
                this.accessedNodes.Add(siteMapNode);
              }
              if (siteMapNode != null && siteMapNode.Key != SiteInitializer.CurrentFrontendRootNodeId.ToString().ToUpperInvariant())
              {
                this.parentNode = siteMapNode;
                break;
              }
              break;
            }
            break;
          }
          this.currentSiteMapDataSource.StartingNodeUrl = "";
          break;
        case PageSelectionModes.SelectedPages:
          List<SiteMapNode> siteMapNodeList = new List<SiteMapNode>();
          foreach (SelectedPage selectedPage in this.CustomSelectedPagesInternal)
          {
            SiteMapNode node;
            if (selectedPage.IsExternal)
            {
              node = new SiteMapNode(this.currentSiteMapDataSource.Provider, "ext", selectedPage.Url, selectedPage.Title);
            }
            else
            {
              node = this.currentSiteMapDataSource.Provider.FindSiteMapNodeFromKey(selectedPage.Id);
              if (node != null)
              {
                this.accessedNodes.Add(node);
                SiteMapNode specificLanguage = ((SiteMapBase) this.GetProvider()).FindSiteMapNodeForSpecificLanguage(node, SystemManager.CurrentContext.Culture);
                if (specificLanguage == null || RouteHelper.CheckSiteMapNode(specificLanguage))
                {
                  node = specificLanguage;
                  this.cacheDependencyNotifiedObjects.Add(new CacheDependencyKey()
                  {
                    Type = typeof (SiteMapNode),
                    Key = node.Key
                  });
                }
                else
                  continue;
              }
            }
            if (node != null)
              siteMapNodeList.Add(node);
          }
          this.CurrentNavigationControl.DataSource = (object) siteMapNodeList;
          return;
        case PageSelectionModes.CurrentPageChildren:
          this.currentSiteMapDataSource.StartFromCurrentNode = true;
          if (flag && provider.CurrentNode != null)
            this.parentNode = provider.CurrentNode;
          this.accessedNodes.Add(provider.CurrentNode);
          this.childNodes.Add(provider.CurrentNode);
          break;
        case PageSelectionModes.CurrentPageSiblings:
          this.currentSiteMapDataSource.StartFromCurrentNode = true;
          this.currentSiteMapDataSource.StartingNodeOffset = -1;
          if (flag && provider.CurrentNode != null)
          {
            SiteMapNode parentNode = provider.CurrentNode.ParentNode;
            if (parentNode != null && parentNode.Key != SiteInitializer.CurrentFrontendRootNodeId.ToString().ToUpperInvariant())
              this.parentNode = parentNode;
          }
          if (provider.CurrentNode != null && provider.CurrentNode.ParentNode != null)
          {
            this.childNodes.Add(provider.CurrentNode.ParentNode);
            this.accessedNodes.Add(provider.CurrentNode.ParentNode);
            break;
          }
          break;
      }
      this.CurrentNavigationControl.DataSource = (object) this.currentSiteMapDataSource;
    }

    private void currentSiteMapDataSource_OnNodePropAccessed(SiteMapNode Node) => this.accessedNodes.Add(Node);

    private void currentSiteMapDataSource_OnGetChildNodesDelegate(SiteMapNode Node) => this.childNodes.Add(Node);

    internal PageSiteNode GetNodeForLanguage(
      PageManager pageManager,
      SiteMapNode pageSiteNode,
      CultureInfo language,
      SiteMapBase smb)
    {
      PageNode pageNodeForLanguage = PageHelper.GetPageNodeForLanguage(pageManager.GetPageNode((pageSiteNode as PageSiteNode).Id), language);
      return pageNodeForLanguage != null ? new PageSiteNode(smb, pageNodeForLanguage, pageManager.Provider.Name) : (PageSiteNode) null;
    }

    /// <summary>
    /// Returns a value indicating whether the given sitemap node has a translation in the given language.
    /// </summary>
    /// <param name="pageSiteNode">The node to be checked.</param>
    /// <param name="language">The culture to be looked for.</param>
    /// <returns></returns>
    protected bool PageNodeHasTranslation(
      PageManager pageManager,
      SiteMapNode pageSiteNode,
      CultureInfo language)
    {
      return PageHelper.PageNodeHasTranslation(pageManager.GetPageNode((pageSiteNode as PageSiteNode).Id), language);
    }

    /// <summary>Sets the resolved navigational control settings.</summary>
    internal void SetControlSettings()
    {
      if (this.CurrentNavigationControl is IExpandableSiteMapControl)
      {
        IExpandableSiteMapControl navigationControl = (IExpandableSiteMapControl) this.CurrentNavigationControl;
        navigationControl.AllowCollapsing = this.AllowCollapsing;
        navigationControl.LevelsToExpand = this.LevelsToExpand;
        if (this.NavigationMode == NavigationModes.HorizontalSimple || this.NavigationMode == NavigationModes.VerticalSimple)
          navigationControl.MaxDataBindDepth = 1;
        else if (this.MaxDataBindDepth != 0)
          navigationControl.MaxDataBindDepth = this.MaxDataBindDepth;
        navigationControl.NavigationAction = this.NavigationAction;
        navigationControl.ShowExpanded = this.ShowExpanded;
        if (this.currentSiteMapDataSource != null)
        {
          SiteMapNode currentNode = this.currentSiteMapDataSource.Provider.CurrentNode;
          if (currentNode != null)
            navigationControl.CurrentPageURL = currentNode.Url;
        }
      }
      if (this.currentSiteMapDataSource != null && this.CurrentNavigationControl is IHighlightPathSiteMapControl)
      {
        SiteMapNode currentNode = this.currentSiteMapDataSource.Provider.CurrentNode;
        Guid result;
        if (currentNode != null && Guid.TryParse(currentNode.Key, out result))
        {
          ((IHighlightPathSiteMapControl) this.CurrentNavigationControl).CurrentPageNodeId = result;
          ((IHighlightPathSiteMapControl) this.CurrentNavigationControl).HighlightPath = this.HighlightPath;
        }
      }
      if (this.CurrentNavigationControl is IParentPageSiteMapControl)
        ((IParentPageSiteMapControl) this.CurrentNavigationControl).ParentNode = this.parentNode;
      if (this.CurrentNavigationControl is IGroupPageNavigationSiteMapControl)
        ((IGroupPageNavigationSiteMapControl) this.CurrentNavigationControl).GroupPageIsLink = this.GroupPageIsLink;
      if (this.CurrentNavigationControl is RadSiteMap && !this.GroupPageIsLink)
        ((RadSiteMap) this.CurrentNavigationControl).NodeDataBound += (RadSiteMapNodeEventHandler) ((sender, e) =>
        {
          if (!((SiteMapNode) e.Node.DataItem is PageSiteNode dataItem2) || !dataItem2.IsGroupPage)
            return;
          e.Node.CssClass = "sfNoGroupPageNavigation";
          e.Node.NavigateUrl = "javascript: void(0)";
        });
      if (string.IsNullOrEmpty(this.Skin) || !(this.CurrentNavigationControl is ISkinnableControl navigationControl1))
        return;
      navigationControl1.EnableEmbeddedSkins = navigationControl1.GetEmbeddedSkinNames().Exists((Predicate<string>) (name => name == this.Skin));
      navigationControl1.Skin = this.Skin;
    }

    /// <summary>Creates the container.</summary>
    /// <param name="template">The template.</param>
    /// <returns></returns>
    protected internal override GenericContainer CreateContainer(ITemplate template)
    {
      GenericContainer container = base.CreateContainer(template);
      ConditionalTemplateContainer control = container.GetControl<ConditionalTemplateContainer>();
      if (control == null)
        return container;
      control.Evaluate((object) this);
      return container;
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
    /// <returns></returns>
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

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      PageManager.ConfigureScriptManager(this.Page, ScriptRef.Empty);
      if (!this.EnsureStartingNodeIsAccessible())
        return;
      try
      {
        this.CurrentNavigationControl.DataBind();
      }
      catch (ArgumentException ex)
      {
        if (this.SelectionMode != PageSelectionModes.SelectedPageChildren)
          throw ex;
        if (this.IsDesignMode())
          this.Controls.Add((Control) new Literal()
          {
            Text = Res.Get<Labels>().StartingPageUnpublished
          });
      }
      this.SubsribeCacheDependency();
    }

    /// <summary>Renders the specified writer.</summary>
    /// <param name="writer">The writer.</param>
    protected override void Render(HtmlTextWriter writer)
    {
      if (this.GetIndexRenderMode() != IndexRenderModes.Normal)
        return;
      base.Render(writer);
    }

    /// <summary>Determines whether the page node is accessible.</summary>
    /// <param name="node">The node.</param>
    /// <returns>
    /// 	<c>true</c> if the page node is accessible; otherwise, <c>false</c>.
    /// </returns>
    protected bool IsPageNodeAccessible(SiteMapNode node)
    {
      if (this.currentSiteMapDataSource.StartingNodeUrl == node.Url.Replace("~/", "") && !string.IsNullOrEmpty(node.Title))
        return true;
      if (!(node is PageSiteNode pageSiteNode))
        throw new NotSupportedException("The supported types are 'TaxonSiteNode' or 'PageSiteNode'.");
      return (!string.IsNullOrEmpty(pageSiteNode.Title) || !(pageSiteNode.Id != SiteInitializer.CurrentFrontendRootNodeId)) && (!pageSiteNode.IsGroupPage || pageSiteNode.ChildNodes.Count >= 1);
    }

    /// <summary>Ensures if the starting node is accessible.</summary>
    /// <returns></returns>
    protected bool EnsureStartingNodeIsAccessible()
    {
      bool flag = true;
      if (this.SelectionMode == PageSelectionModes.SelectedPageChildren)
      {
        if (string.IsNullOrEmpty(this.StartingNodeUrl) || !this.StartingNodeUrl.IsGuid())
          return false;
        SiteMapNode siteMapNodeFromKey = this.GetProvider().FindSiteMapNodeFromKey(this.StartingNodeUrl);
        if (siteMapNodeFromKey == null)
          return false;
        if (!this.IsPageNodeAccessible(siteMapNodeFromKey))
        {
          if (this.IsDesignMode())
            this.Controls.Add((Control) new Literal()
            {
              Text = Res.Get<Labels>().StartingPageIsNotAccessible
            });
          flag = false;
        }
      }
      return flag;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public IEnumerable<ScriptDescriptor> GetScriptDescriptors() => (IEnumerable<ScriptDescriptor>) new List<ScriptDescriptor>();

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>();

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
      return new CacheDependencyKey()
      {
        Type = objectType,
        Key = node.Key + (object) SystemManager.CurrentContext.Culture
      };
    }
  }
}
