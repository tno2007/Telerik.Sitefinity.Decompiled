// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.SiteMapBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Configuration.Provider;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.UrlLocalizationStrategies;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Web.Utilities;

namespace Telerik.Sitefinity.Web
{
  /// <summary>
  /// Provides implementation for <see cref="T:System.Web.SiteMapProvider" /> for Sitefinity pages.
  /// </summary>
  public abstract class SiteMapBase : SiteMapProvider
  {
    private static IList<ISitemapNodeFilter> filters = (IList<ISitemapNodeFilter>) null;
    private static readonly string NodeFiltersCacheKey = "SiteMapBase|NodeFiltersCache|";
    private static readonly object nodeFiltersCacheSynch = new object();
    /// <summary>Gets the items key for the current site map.</summary>
    public const string SiteMapContextKeyPrefix = "SF_SiteMapContext_";
    /// <summary>Gets the items key for the current site map.</summary>
    public const string ProviderKey = "SF_SiteMap";
    /// <summary>Gets the items key for the current node.</summary>
    public const string CurrentNodeKey = "ServedPageNode";
    /// <summary>Gets the items key for the last found node.</summary>
    public const string LastFoundNodeKey = "LastFoundPageNode";
    /// <summary>
    /// Gets the name of the default SiteMap provider for sitefinity frontend pages
    /// </summary>
    public const string DefaultSiteMapProviderName = "SitefinitySiteMap";
    /// <summary>
    /// Name of the key that stores the culture of the PageUrlData for the requested url
    /// </summary>
    public const string AdditionalUrlCulture = "AdditionalUrlCulture";
    /// <summary>
    /// The sitemap cache key for authenticated external user with no internal Id.
    /// </summary>
    private static readonly Guid AuthenticatedExternalUserKey = Guid.Parse("EC3A1BCF-C2F8-43D9-A9B3-B4C5BDE49EC8");
    private readonly object urlLock = new object();
    private readonly object addUrlLock = new object();
    private readonly object nodeLock = new object();
    private readonly object childLock = new object();
    private readonly object pageDataContextLock = new object();
    private readonly object personaLizedPagesLock = new object();
    private static readonly object providersLock = new object();
    private bool preLoaded;
    private int cacheExp;
    private int maxPageNodes;
    private string pageProviderName;
    private static string appPath;
    private bool disableBatchLoading;
    private bool resolvePageWithoutParentTranslation = true;
    private const string BatchPagesKey = "sf_BatchPagesKey";
    private static readonly IDictionary<string, SiteMapProvider> providers = SystemManager.CreateStaticCache<string, SiteMapProvider>();
    private SiteMapBase.ISiteMapKeyResolver siteMapKeyResolver;

    /// <summary>Gets the cache expiration period in seconds.</summary>
    /// <value>The cache expiration.</value>
    public virtual int CacheExpiration => this.cacheExp;

    /// <summary>
    /// Gets the name of the page provider used by this instance.
    /// </summary>
    /// <value>The name of the page provider.</value>
    public virtual string PageProviderName => this.pageProviderName;

    /// <summary>Gets the key of the root node for this provider.</summary>
    /// <value>The root node key.</value>
    public abstract string RootNodeKey { get; }

    /// <summary>
    /// Gets the number of maximum page nodes allowed for a given parent node for this provider.
    /// If this number is different then 0 the number of retrieved page nodes will be up to the specified value.
    /// If 0 is set all pages for the parent node will be retrieved. The default value is 100.
    /// </summary>
    /// <value>The max page nodes.</value>
    public virtual int MaxPageNodes => this.maxPageNodes;

    /// <summary>
    /// Gets the <see cref="T:System.Web.SiteMapNode" /> object that represents the currently requested page.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// A <see cref="T:System.Web.SiteMapNode" /> that represents the currently requested page; otherwise, null, if the <see cref="T:System.Web.SiteMapNode" /> is not found or cannot be returned for the current user.
    /// </returns>
    public override SiteMapNode CurrentNode
    {
      get
      {
        if (!(SystemManager.CurrentHttpContext.Items[(object) "ServedPageNode"] is SiteMapNode siteMapNode) || !(siteMapNode.Provider is SiteMapBase provider))
          return base.CurrentNode;
        return provider.Name.Equals(this.Name) ? siteMapNode : (SiteMapNode) null;
      }
    }

    /// <summary>
    /// Determine that the current sitemap provider expects am URL root segment, that have to be stripped from the URL, when finding the sitemap node by URL.
    /// </summary>
    /// <value>The has URL root.</value>
    protected virtual bool HasUrlRoot => false;

    protected internal virtual Guid BackendLandingPageId => Guid.Empty;

    internal static ICacheManager cache => SystemManager.GetCacheManager(CacheManagerInstance.SiteMap);

    internal static ICacheManager pageDataCache => SystemManager.GetCacheManager(CacheManagerInstance.SiteMapPageData);

    internal static ICacheManager urlCache => SystemManager.GetCacheManager(CacheManagerInstance.SiteMapNodeUrl);

    internal static bool TryGetPageSiteNode(
      string url,
      out SiteMapNode siteMapNode,
      out Telerik.Sitefinity.Multisite.ISite site)
    {
      System.Web.HttpResponse response = new System.Web.HttpResponse((TextWriter) new StringWriter(new StringBuilder()));
      HttpContext httpContext = new HttpContext(new System.Web.HttpRequest("", url, ""), response);
      HttpContextWrapper httpContextWrapper = new HttpContextWrapper(httpContext);
      HttpContext current = HttpContext.Current;
      HttpContext.Current = httpContext;
      bool pageSiteNode = false;
      siteMapNode = (SiteMapNode) null;
      site = (Telerik.Sitefinity.Multisite.ISite) null;
      try
      {
        SiteMapBase currentProvider = SiteMapBase.GetCurrentProvider() as SiteMapBase;
        siteMapNode = LinkParser.FindSiteMapNode(new Uri(url).AbsolutePath, currentProvider);
        if (siteMapNode != null)
        {
          site = SystemManager.CurrentContext.CurrentSite;
          pageSiteNode = true;
        }
      }
      finally
      {
        SystemManager.ClearCurrentTransactions();
        HttpContext.Current = current;
      }
      return pageSiteNode;
    }

    internal static void Reset()
    {
      SiteMapBase.filters = (IList<ISitemapNodeFilter>) null;
      SiteMapBase.cache.Flush();
      SiteMapBase.pageDataCache.Flush();
      SiteMapBase.urlCache.Flush();
      SiteMapBase sitefinityProvider = SiteMapBase.TryGetSitefinityProvider("SitefinitySiteMap");
      if (sitefinityProvider != null && !(sitefinityProvider.siteMapKeyResolver is SiteMapBase.DynamicSiteMapKeyResolver))
        sitefinityProvider.siteMapKeyResolver = (SiteMapBase.ISiteMapKeyResolver) new SiteMapBase.DynamicSiteMapKeyResolver();
      foreach (SiteMapBase siteMapBase in SiteMapBase.Providers.Values.OfType<SiteMapBase>())
        siteMapBase.preLoaded = false;
    }

    /// <summary>Tries to get the provider with the given name</summary>
    /// <param name="name">The name.</param>
    /// <returns></returns>
    private static SiteMapBase TryGetSitefinityProvider(string name)
    {
      SiteMapProvider sitefinityProvider;
      if (!SiteMapBase.Providers.TryGetValue(name, out sitefinityProvider) && SiteMap.Enabled)
      {
        foreach (object provider in (ProviderCollection) SiteMap.Providers)
        {
          if (provider is SiteMapBase siteMapBase && (siteMapBase.Name == name || siteMapBase.GetRootNodeName() == name))
          {
            sitefinityProvider = (SiteMapProvider) siteMapBase;
            break;
          }
        }
      }
      return sitefinityProvider as SiteMapBase;
    }

    internal PageDataContext FindDataContext(PageSiteNode node)
    {
      string str1 = this.GetDataKeyPrefix() + node.Key;
      ICacheManager pageDataCache = SiteMapBase.pageDataCache;
      PageDataContext pageDataContext1 = (PageDataContext) pageDataCache[str1];
      if (pageDataContext1 == null)
      {
        lock (this.pageDataContextLock)
        {
          pageDataContext1 = (PageDataContext) pageDataCache[str1];
          if (pageDataContext1 == null)
          {
            HashSet<string> stringSet = SystemManager.CurrentHttpContext != null ? SystemManager.CurrentHttpContext.Items[(object) "sf_BatchPagesKey"] as HashSet<string> : (HashSet<string>) null;
            if (stringSet != null && stringSet.Contains(node.ParentKey) && node.ParentNode is PageSiteNode)
            {
              IEnumerable<PageSiteNode> pageSiteNodes = node.ParentNode.ChildNodes.OfType<PageSiteNode>();
              List<PageSiteNode> nodes = new List<PageSiteNode>();
              foreach (PageSiteNode pageSiteNode in pageSiteNodes)
              {
                string str2 = this.GetDataKeyPrefix() + pageSiteNode.Key;
                if ((PageDataContext) pageDataCache[str1] == null)
                  nodes.Add(pageSiteNode);
              }
              IList<PageDataContext> pageDataContext2 = this.GetPageDataContext((IEnumerable<PageSiteNode>) nodes);
              foreach (PageSiteNode pageSiteNode1 in nodes)
              {
                PageSiteNode pageSiteNode = pageSiteNode1;
                PageDataContext pageDataContext3 = pageDataContext2.SingleOrDefault<PageDataContext>((Func<PageDataContext, bool>) (pdc => pdc.PageNodeId == pageSiteNode.Id)) ?? new PageDataContext();
                this.AddPageDataContextToCache(this.GetDataKeyPrefix() + pageSiteNode.Key, pageDataContext3, pageSiteNode.Key);
                if (pageSiteNode.Id == node.Id)
                  pageDataContext1 = pageDataContext3;
              }
            }
            else
            {
              pageDataContext1 = this.GetPageDataContext(node.Id, node.NodeType);
              this.AddPageDataContextToCache(str1, pageDataContext1, node.Key);
            }
          }
        }
      }
      return pageDataContext1;
    }

    internal PageDataProxy FindPersonalizedPageProxy(
      Guid masterPageDataId,
      Guid personalizedPageDataId)
    {
      IList<PageDataProxy> personalizedPages = this.GetPersonalizedPages(masterPageDataId);
      return personalizedPages != null ? personalizedPages.FirstOrDefault<PageDataProxy>((Func<PageDataProxy, bool>) (x => x.Id == personalizedPageDataId)) : (PageDataProxy) null;
    }

    internal PageDataProxy FindPageProxyVariation(PageSiteNode node, Guid variationKey)
    {
      IList<PageDataProxy> personalizedPages = this.GetPersonalizedPages(node.PageId);
      return personalizedPages != null ? personalizedPages.FirstOrDefault<PageDataProxy>((Func<PageDataProxy, bool>) (x => x.PersonalizationSegmentId == variationKey)) : (PageDataProxy) null;
    }

    private IList<PageDataProxy> GetPersonalizedPages(Guid pageDataId)
    {
      string key = this.GetPersonalizedDataKeyPrefix() + PageSiteNode.GetKey(pageDataId);
      if (!(SiteMapBase.pageDataCache[key] is IList<PageDataProxy> list1))
      {
        lock (this.personaLizedPagesLock)
        {
          if (!(SiteMapBase.pageDataCache[key] is IList<PageDataProxy> list1))
          {
            PageManager pageManager = this.GetPageManager();
            list1 = (IList<PageDataProxy>) this.LoadPersonalizedPages(pageDataId, pageManager).Select<PageData, PageDataProxy>((Func<PageData, PageDataProxy>) (x => new PageDataProxy(x, pageManager))).ToList<PageDataProxy>();
            CompoundCacheDependency compoundCacheDependency = new CompoundCacheDependency();
            compoundCacheDependency.CacheDependencies.Add((ICacheItemExpiration) new DataItemCacheDependency(typeof (SitemapCachePersonalizedPageObject), pageDataId));
            foreach (Guid trackedItemId in list1.SelectMany<PageDataProxy, Guid>((Func<PageDataProxy, IEnumerable<Guid>>) (x => (IEnumerable<Guid>) x.TemplatesIds)).Distinct<Guid>())
              compoundCacheDependency.CacheDependencies.Add((ICacheItemExpiration) new DataItemCacheDependency(typeof (PageTemplate), trackedItemId));
            ICacheItemExpiration[] cacheItemExpirationArray = new ICacheItemExpiration[2]
            {
              (ICacheItemExpiration) new SlidingTime(TimeSpan.FromSeconds((double) this.cacheExp)),
              (ICacheItemExpiration) compoundCacheDependency
            };
            SiteMapBase.pageDataCache.Add(key, (object) list1, CacheItemPriority.Low, (ICacheItemRefreshAction) null, cacheItemExpirationArray);
          }
        }
      }
      return list1;
    }

    private IList<PageData> LoadPersonalizedPages(Guid pageId, PageManager manager)
    {
      using (new ElevatedModeRegion((IManager) manager))
        return (IList<PageData>) this.GetPageDataQuery(manager).Where<PageData>((Expression<Func<PageData, bool>>) (pd => pd.PersonalizationMasterId == pageId)).ToList<PageData>();
    }

    internal void AddPageDataContextToCache(
      string cacheKey,
      PageDataContext pageDataContext,
      string nodeKey)
    {
      CompoundCacheDependency compoundCacheDependency = new CompoundCacheDependency();
      compoundCacheDependency.CacheDependencies.Add((ICacheItemExpiration) new DataItemCacheDependency(typeof (SitemapCachePageDataObject), nodeKey));
      compoundCacheDependency.CacheDependencies.AddRange((IEnumerable<ICacheItemExpiration>) pageDataContext.GetCacheDependencyObjects());
      ICacheItemExpiration[] cacheItemExpirationArray = new ICacheItemExpiration[2]
      {
        (ICacheItemExpiration) new SlidingTime(TimeSpan.FromSeconds((double) this.cacheExp)),
        (ICacheItemExpiration) compoundCacheDependency
      };
      SiteMapBase.pageDataCache.Add(cacheKey, (object) pageDataContext, CacheItemPriority.Low, (ICacheItemRefreshAction) null, cacheItemExpirationArray);
    }

    internal PageDataContext GetPageDataContext(
      Guid nodeId,
      NodeType nodeType,
      IList<PageData> pageDataList = null,
      PageManager manager = null)
    {
      if (nodeType != NodeType.Standard && nodeType != NodeType.External)
        return new PageDataContext();
      if (manager == null)
        manager = this.GetPageManager();
      using (new SiteMapBase.SiteMapContext(this, manager.Provider.TransactionName))
        return new PageDataContext(pageDataList ?? this.LoadPageDataList(nodeId, manager), manager, nodeId);
    }

    internal IList<PageDataContext> GetPageDataContext(
      IEnumerable<PageSiteNode> nodes)
    {
      List<PageDataContext> pageDataContext = new List<PageDataContext>();
      List<Guid> nodeIds = new List<Guid>();
      foreach (PageSiteNode node in nodes)
      {
        if (node.NodeType == NodeType.Standard || node.NodeType == NodeType.External)
          nodeIds.Add(node.Id);
      }
      if (nodeIds.Count > 0)
      {
        PageManager pageManager = this.GetPageManager();
        using (new SiteMapBase.SiteMapContext(this, pageManager.Provider.TransactionName))
        {
          foreach (IGrouping<Guid, PageData> source in (IEnumerable<IGrouping<Guid, PageData>>) this.LoadPageDataList(nodeIds, pageManager).ToLookup<PageData, Guid>((Func<PageData, Guid>) (pd => pd.NavigationNodeId)))
            pageDataContext.Add(new PageDataContext((IList<PageData>) source.ToList<PageData>(), pageManager, source.Key));
        }
      }
      return (IList<PageDataContext>) pageDataContext;
    }

    private IList<PageData> LoadPageDataList(Guid nodeId, PageManager manager)
    {
      List<PageData> source1 = new List<PageData>();
      List<PageData> source2 = (List<PageData>) null;
      if (SystemManager.CurrentHttpContext.Items.Contains((object) "pageDataCache"))
        source2 = SystemManager.CurrentHttpContext.Items[(object) "pageDataCache"] as List<PageData>;
      if (source2 != null && source2.Any<PageData>((Func<PageData, bool>) (pd => pd.NavigationNode.Id == nodeId)))
        source1 = source2.Where<PageData>((Func<PageData, bool>) (pd => pd.NavigationNode.Id == nodeId)).ToList<PageData>();
      if (source1 == null || !source1.Any<PageData>())
      {
        source1 = (List<PageData>) null;
        if (SystemManager.CurrentHttpContext.Items.Contains((object) "pageids"))
        {
          List<Guid> pageIds = SystemManager.CurrentHttpContext.Items[(object) "pageids"] as List<Guid>;
          if (pageIds.Contains(nodeId))
          {
            List<PageData> list = this.GetPageDataQuery(manager).Where<PageData>((Expression<Func<PageData, bool>>) (pd => pageIds.Contains(pd.NavigationNode.Id))).ToList<PageData>();
            SystemManager.CurrentHttpContext.Items[(object) "pageDataCache"] = (object) list;
            source1 = list.Where<PageData>((Func<PageData, bool>) (pd => pd.NavigationNode.Id == nodeId)).ToList<PageData>();
          }
        }
      }
      if (source1 == null)
      {
        using (new ElevatedModeRegion((IManager) manager))
          source1 = this.GetPageDataQuery(manager).Where<PageData>((Expression<Func<PageData, bool>>) (pd => pd.NavigationNode.Id == nodeId)).ToList<PageData>();
      }
      return (IList<PageData>) source1;
    }

    private IList<PageData> LoadPageDataList(List<Guid> nodeIds, PageManager manager)
    {
      using (new ElevatedModeRegion((IManager) manager))
        return (IList<PageData>) this.GetPageDataQuery(manager).Where<PageData>((Expression<Func<PageData, bool>>) (pd => nodeIds.Contains(pd.NavigationNode.Id))).ToList<PageData>();
    }

    private IQueryable<PageData> GetPageDataQuery(PageManager manager) => manager.GetPageDataList().Include<PageData>((Expression<Func<PageData, object>>) (x => x.PublishedTranslations)).Include<PageData>((Expression<Func<PageData, object>>) (x => x.LanguageData)).Include<PageData>((Expression<Func<PageData, object>>) (x => x.Template)).Include<PageData>((Expression<Func<PageData, object>>) (x => x.Drafts)).Include<PageData>((Expression<Func<PageData, object>>) (x => x.NavigationNode)).Include<PageData>((Expression<Func<PageData, object>>) (x => x.Drafts.Select<PageDraft, IList<LanguageData>>((Func<PageDraft, IList<LanguageData>>) (pd => pd.LanguageData))));

    internal void PreLoad()
    {
      if (this.preLoaded || SystemManager.Initializing)
        return;
      lock (this.pageDataContextLock)
      {
        if (this.preLoaded || SystemManager.Initializing)
          return;
        using (new CultureRegion(this.AppSettings.DefaultFrontendLanguage))
        {
          Guid rootNodeId = this.GetRootNode();
          PageManager manager = PageManager.GetManager();
          using (new ReadUncommitedRegion((IManager) manager))
          {
            using (new ElevatedModeRegion((IManager) manager))
            {
              IEnumerable<PageData> list1 = (IEnumerable<PageData>) this.GetPageDataQuery(manager).Where<PageData>((Expression<Func<PageData, bool>>) (x => x.NavigationNode.RootNodeId == rootNodeId)).ToList<PageData>();
              IEnumerable<PageNode> list2 = (IEnumerable<PageNode>) this.GetPageNodesQuery(manager).Include<PageNode>((Expression<Func<PageNode, object>>) (p => p.Permissions)).Include<PageNode>((Expression<Func<PageNode, object>>) (p => p.Attributes)).Include<PageNode>((Expression<Func<PageNode, object>>) (p => p.Urls)).Where<PageNode>((Expression<Func<PageNode, bool>>) (x => x.RootNodeId == rootNodeId)).ToList<PageNode>();
              this.PreLoadPagesRecursively((PageSiteNode) this.GetRootNodeCore(true), list2);
              foreach (IGrouping<PageNode, PageData> source in (IEnumerable<IGrouping<PageNode, PageData>>) list1.ToLookup<PageData, PageNode>((Func<PageData, PageNode>) (x => x.NavigationNode)))
              {
                PageNode key1 = source.Key;
                string key2 = PageSiteNode.GetKey(key1);
                string str = this.GetDataKeyPrefix() + key2;
                if (!(SiteMapBase.pageDataCache[str] is PageDataContext))
                {
                  PageDataContext pageDataContext = this.GetPageDataContext(key1.Id, key1.NodeType, (IList<PageData>) source.ToList<PageData>(), manager);
                  this.AddPageDataContextToCache(str, pageDataContext, key2);
                }
              }
            }
          }
        }
        this.preLoaded = true;
      }
    }

    private void PreLoadPagesRecursively(PageSiteNode parent, IEnumerable<PageNode> source)
    {
      if (parent == null)
        return;
      List<PageNode> list = source.Where<PageNode>((Func<PageNode, bool>) (x => x.ParentId == parent.Id)).OrderBy<PageNode, float>((Func<PageNode, float>) (x => x.Ordinal)).ToList<PageNode>();
      foreach (PageSiteNode childNode in this.GetChildNodes((SiteMapNode) parent, true, true, list.AsQueryable<PageNode>()))
        this.PreLoadPagesRecursively(childNode, source);
    }

    internal bool HasAnyChildNodes(PageSiteNode node)
    {
      PageManager pageManager = this.GetPageManager();
      using (new ElevatedModeRegion((IManager) pageManager))
        return this.GetPageNodesQuery(pageManager).Where<PageNode>((Expression<Func<PageNode, bool>>) (p => p.ParentId == node.Id)).Any<PageNode>();
    }

    private IQueryable<PageNode> GetPageNodesQuery(PageManager pageManager) => pageManager.GetPageNodes().Where<PageNode>((Expression<Func<PageNode, bool>>) (p => !p.IsDeleted));

    /// <summary>
    /// When overridden in a derived class, retrieves a <see cref="T:System.Web.SiteMapNode" /> object that represents the page at the specified URL.
    /// </summary>
    /// <param name="rawUrl">A URL that identifies the page for which to retrieve a <see cref="T:System.Web.SiteMapNode" />.</param>
    /// <returns>
    /// A <see cref="T:System.Web.SiteMapNode" /> that represents the page identified by <paramref name="rawURL" />; otherwise, null, if no corresponding <see cref="T:System.Web.SiteMapNode" /> is found or if security trimming is enabled and the <see cref="T:System.Web.SiteMapNode" /> cannot be returned for the current user.
    /// </returns>
    public override SiteMapNode FindSiteMapNode(string rawUrl) => this.FindSiteMapNode(rawUrl, true);

    /// <summary>Returns a site map node in specific culture.</summary>
    /// <param name="node">The related site map node.</param>
    /// <param name="cultureInfo">The culture of the node to get.</param>
    /// <returns>A site map node for the specific culture.</returns>
    [Obsolete("There is no need to call this method as from Sitefinity version 7.0 there is only one node even for the different cultures. Use Available cultures in PageSiteNode.")]
    public virtual SiteMapNode FindSiteMapNodeForSpecificLanguage(
      SiteMapNode node,
      CultureInfo cultureInfo)
    {
      return this.FindSiteMapNodeForSpecificLanguage(node, cultureInfo, true);
    }

    /// <summary>Returns a site map node in specific culture.</summary>
    /// <param name="node">The related site map node.</param>
    /// <param name="cultureInfo">The culture of the node to get.</param>
    /// <param name="returnOnlyIfAccessible">If set to true, will return the node only if accessible to the current user. Otherwise will always return the node (if exists).</param>
    /// <returns>A site map node for the specific culture.</returns>
    [Obsolete("There is no need to call this method as from Sitefinity version 7.0 there is only one node even for the different cultures. Use Available cultures in PageSiteNode.")]
    public virtual SiteMapNode FindSiteMapNodeForSpecificLanguage(
      SiteMapNode node,
      CultureInfo cultureInfo,
      bool returnOnlyIfAccessible)
    {
      return node;
    }

    /// <summary>
    /// When overridden in a derived class, retrieves a <see cref="T:System.Web.SiteMapNode" /> object that represents the page at the specified URL.
    /// </summary>
    /// <param name="rawUrl">A URL that identifies the page for which to retrieve a <see cref="T:System.Web.SiteMapNode" />.</param>
    /// <param name="ifAccessible">
    /// if set to <c>true</c> checks if the node is accessible to the user
    /// making the current request and if node is not accessible it returns null even if the node exists.
    /// </param>
    /// <returns>
    /// A <see cref="T:System.Web.SiteMapNode" /> that represents the page identified by <paramref name="rawURL" />; otherwise, null, if no corresponding <see cref="T:System.Web.SiteMapNode" /> is found or if security trimming is enabled and the <see cref="T:System.Web.SiteMapNode" /> cannot be returned for the current user.
    /// </returns>
    public virtual SiteMapNode FindSiteMapNode(string rawUrl, bool ifAccessible) => this.FindSiteMapNode(rawUrl, ifAccessible, out bool _, out string[] _);

    /// <summary>
    /// When overridden in a derived class, retrieves a <see cref="T:System.Web.SiteMapNode" /> object that represents the page at the specified URL.
    /// </summary>
    /// <param name="rawUrl">A URL that identifies the page for which to retrieve a <see cref="T:System.Web.SiteMapNode" />.</param>
    /// <param name="ifAccessible">if set to <c>true</c> checks if the node is accessible to the user
    /// making the current request and if node is not accessible it returns null even if the node exists.</param>
    /// <param name="fallbackToOtherLanguageVersion">This method tries to find the exact node for the specified URL, using the current thread language.
    /// if this argument is set to <c>true</c>, the method will fallback to a version of the node for another language, if such exists.</param>
    /// <returns>
    /// A <see cref="T:System.Web.SiteMapNode" /> that represents the page identified by <paramref name="rawURL" />; otherwise, null, if no corresponding <see cref="T:System.Web.SiteMapNode" /> is found or if security trimming is enabled and the <see cref="T:System.Web.SiteMapNode" /> cannot be returned for the current user.
    /// </returns>
    public virtual SiteMapNode FindSiteMapNode(
      string rawUrl,
      bool ifAccessible,
      out bool isAdditionalUrl,
      out string[] urlParameters)
    {
      return this.FindSiteMapNode(rawUrl, ifAccessible, true, out isAdditionalUrl, out urlParameters, out bool _);
    }

    /// <summary>
    /// Finds the site map node by exact URL. Returns null if no such node has been found.
    /// </summary>
    /// <param name="rawUrl">The exact raw URL.</param>
    /// <returns></returns>
    internal virtual SiteMapNode FindSiteMapNodeByExactUrl(
      string rawUrl,
      out bool isAdditional,
      bool ignoreExtension = false)
    {
      return this.FindSiteMapNode(rawUrl, true, true, out isAdditional, out string[] _, out bool _, ignoreExtension, true) ?? (SiteMapNode) null;
    }

    internal SiteMapNode FindSiteMapNode(
      string rawUrl,
      bool ifAccessible,
      bool unresolveUrl,
      out bool isAdditionalUrl,
      out string[] urlParameters,
      out bool isHomePageAutoResolved,
      bool ignoreExtension = false,
      bool isExactUrl = false)
    {
      if (rawUrl == null)
        throw new ArgumentNullException(nameof (rawUrl));
      if (unresolveUrl)
      {
        IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
        if (multisiteContext != null)
          rawUrl = multisiteContext.UnresolveUrl(rawUrl);
        rawUrl = ObjectFactory.Resolve<UrlLocalizationService>().UnResolveUrl(rawUrl);
      }
      int length = rawUrl.IndexOf("?", StringComparison.Ordinal);
      if (length != -1)
        rawUrl = rawUrl.Substring(0, length);
      if (SiteMapBase.appPath.Length > 1 && !string.IsNullOrEmpty(rawUrl) && rawUrl.StartsWith(SiteMapBase.appPath, StringComparison.OrdinalIgnoreCase))
        rawUrl = rawUrl.Substring(SiteMapBase.appPath.Length);
      if (rawUrl.StartsWith("/"))
        rawUrl = rawUrl.Substring(1);
      if (rawUrl.EndsWith("/"))
        rawUrl = rawUrl.Left(rawUrl.Length - 1);
      SiteMapNode node;
      if (string.IsNullOrEmpty(rawUrl) || rawUrl.Equals("default.aspx", StringComparison.OrdinalIgnoreCase))
      {
        node = string.IsNullOrEmpty(this.RootNodeKey) ? (SiteMapNode) null : this.FindSiteMapNodeFromKey(this.RootNodeKey, ifAccessible);
        isAdditionalUrl = false;
        urlParameters = (string[]) null;
        isHomePageAutoResolved = true;
      }
      else
      {
        isHomePageAutoResolved = false;
        string key = SiteMapBase.BuildUrlCacheKey(this.GetUrlKeyPrefix(), rawUrl, Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.Multilingual);
        SiteMapBase.UrlNode urlNode = (SiteMapBase.UrlNode) SiteMapBase.urlCache[key];
        if (urlNode == null)
        {
          lock (this.urlLock)
          {
            urlNode = (SiteMapBase.UrlNode) SiteMapBase.urlCache[key];
            if (urlNode == null)
            {
              PageSiteNode rootNodeCore = (PageSiteNode) this.GetRootNodeCore(ifAccessible);
              PageSiteNode parent1 = rootNodeCore;
              if (parent1 != null)
              {
                string[] array = RouteHelper.SplitUrlToPathSegmentStrings(rawUrl, true).ToArray<string>();
                if (this.HasUrlRoot)
                {
                  if (array.Length != 0)
                  {
                    if (array[0].Equals(rootNodeCore.UrlName, StringComparison.OrdinalIgnoreCase))
                      array = ((IEnumerable<string>) array).Skip<string>(1).ToArray<string>();
                    else
                      goto label_64;
                  }
                  else
                    goto label_64;
                }
                string expectedExtension;
                int extensionSegmentIndex;
                this.ResolveExtension(array, out expectedExtension, out extensionSegmentIndex);
                int regularNodesCount1;
                PageSiteNode parent2 = this.ResolveNode(array, parent1, out regularNodesCount1);
                if (extensionSegmentIndex >= 0)
                  array[extensionSegmentIndex] = array[extensionSegmentIndex] + expectedExtension;
                string[] parameters = (string[]) null;
                bool flag1 = false;
                bool paramsMismatch = false;
                bool flag2 = false;
                bool flag3 = false;
                SiteMapNode siteMapNode;
                if (regularNodesCount1 < array.Length)
                {
                  ISiteMapAdditionalUrl additionalUrlInfo;
                  PageSiteNode nodeByAdditionalUrl = this.FindNodeByAdditionalUrl(rawUrl, array, out additionalUrlInfo);
                  if (nodeByAdditionalUrl != null && additionalUrlInfo != null)
                  {
                    flag1 = true;
                    flag2 = additionalUrlInfo.IsDefault;
                    if (parent2 != rootNodeCore && parent2.UnresolvedUrl.Length >= additionalUrlInfo.Url.Length)
                    {
                      siteMapNode = (SiteMapNode) parent2;
                      flag1 = false;
                      parameters = this.GetParameters(siteMapNode, array, regularNodesCount1, out paramsMismatch);
                    }
                    else
                    {
                      string[] additionalUrlParameters = this.GetAdditionalUrlParameters(nodeByAdditionalUrl, additionalUrlInfo, rawUrl, out paramsMismatch);
                      if (paramsMismatch)
                      {
                        SystemManager.HttpContextItems[(object) "LastFoundPageNode"] = (object) this.ResolveNode(array, parent2, out regularNodesCount1);
                        goto label_64;
                      }
                      else
                      {
                        siteMapNode = (SiteMapNode) nodeByAdditionalUrl;
                        if (additionalUrlParameters != null && additionalUrlParameters.Length != 0)
                        {
                          this.ResolveExtension(additionalUrlParameters, out expectedExtension, out extensionSegmentIndex);
                          int regularNodesCount2;
                          siteMapNode = (SiteMapNode) this.ResolveNode(additionalUrlParameters, nodeByAdditionalUrl, out regularNodesCount2);
                          if (extensionSegmentIndex >= 0)
                            additionalUrlParameters[extensionSegmentIndex] = additionalUrlParameters[extensionSegmentIndex] + expectedExtension;
                          parameters = this.GetParameters(siteMapNode, additionalUrlParameters, regularNodesCount2, out paramsMismatch);
                        }
                        else
                          parameters = additionalUrlParameters;
                        if (siteMapNode == nodeByAdditionalUrl && this.IsMultilingual && !nodeByAdditionalUrl.IsBackend)
                        {
                          if (!nodeByAdditionalUrl.HasTranslation(SystemManager.CurrentContext.Culture))
                            goto label_64;
                        }
                      }
                    }
                  }
                  else if (parent2 != rootNodeCore)
                  {
                    siteMapNode = (SiteMapNode) parent2;
                    flag1 = false;
                    parameters = this.GetParameters(siteMapNode, array, regularNodesCount1, out paramsMismatch);
                  }
                  else
                    goto label_64;
                }
                else
                {
                  flag3 = ((parent2 == rootNodeCore ? 0 : (!parent2.Extension.IsNullOrEmpty() ? 1 : 0)) & (extensionSegmentIndex == -1 ? (true ? 1 : 0) : (!parent2.Extension.Equals(expectedExtension) ? 1 : 0))) != 0;
                  siteMapNode = (SiteMapNode) parent2;
                }
                if (paramsMismatch)
                {
                  SystemManager.HttpContextItems[(object) "LastFoundPageNode"] = (object) this.ResolveNode(array, parent2, out regularNodesCount1);
                  goto label_64;
                }
                else
                {
                  if (!flag1 && !flag2 && extensionSegmentIndex > -1 && !ControlExtensions.IsBackend() && (extensionSegmentIndex == regularNodesCount1 - 1 || regularNodesCount1 == array.Length) && !this.ExtensionIsSameAsRequested(siteMapNode, expectedExtension, rawUrl, parameters))
                  {
                    ISiteMapAdditionalUrl additionalUrlInfo;
                    PageSiteNode nodeByAdditionalUrl = this.FindNodeByAdditionalUrl(rawUrl, array, out additionalUrlInfo);
                    if (nodeByAdditionalUrl != null)
                    {
                      parameters = this.GetAdditionalUrlParameters(nodeByAdditionalUrl, additionalUrlInfo, rawUrl, out paramsMismatch);
                      if (paramsMismatch)
                      {
                        SystemManager.HttpContextItems[(object) "LastFoundPageNode"] = (object) this.ResolveNode(array, parent2, out regularNodesCount1);
                        goto label_64;
                      }
                      else
                      {
                        flag1 = !flag2;
                        siteMapNode = (SiteMapNode) nodeByAdditionalUrl;
                      }
                    }
                    else
                      goto label_64;
                  }
                  if (siteMapNode != null)
                  {
                    if (isExactUrl && parameters != null)
                    {
                      if (parameters.Length != 0)
                        goto label_64;
                    }
                    urlNode = new SiteMapBase.UrlNode((PageSiteNode) siteMapNode)
                    {
                      IsAdditional = flag1 && !flag2,
                      Parameters = parameters,
                      ExtensionMismatch = flag3
                    };
                    if (SystemManager.CurrentHttpContext != null)
                      urlNode.AdditionalUrlCulture = SystemManager.CurrentHttpContext.Items[(object) "AdditionalUrlCulture"] as CultureInfo;
                    ICacheItemExpiration[] cacheItemExpirationArray = new ICacheItemExpiration[2]
                    {
                      (ICacheItemExpiration) new SlidingTime(TimeSpan.FromSeconds((double) this.cacheExp)),
                      (ICacheItemExpiration) urlNode.CacheDependency
                    };
                    SiteMapBase.urlCache.Add(key, (object) urlNode, CacheItemPriority.Normal, (ICacheItemRefreshAction) null, cacheItemExpirationArray);
                  }
                }
              }
              else
                goto label_64;
            }
          }
        }
        if (urlNode != null && (ignoreExtension || !urlNode.ExtensionMismatch))
        {
          node = (SiteMapNode) urlNode.SiteNode;
          isAdditionalUrl = urlNode.IsAdditional;
          urlParameters = urlNode.Parameters;
          if (SystemManager.CurrentHttpContext != null)
          {
            SystemManager.CurrentHttpContext.Items[(object) "AdditionalUrlCulture"] = (object) urlNode.AdditionalUrlCulture;
            goto label_61;
          }
          else
            goto label_61;
        }
label_64:
        isAdditionalUrl = false;
        urlParameters = (string[]) null;
        return (SiteMapNode) null;
      }
label_61:
      return ifAccessible ? this.ReturnNodeIfAccessible(node) : node;
    }

    internal void ResolveExtension(
      string[] segments,
      out string expectedExtension,
      out int extensionSegmentIndex)
    {
      expectedExtension = string.Empty;
      extensionSegmentIndex = -1;
      ISet<string> knownExtensions = Config.Get<PagesConfig>().KnownExtensions;
      if (knownExtensions.Count <= 0)
        return;
      for (int index = 0; index < segments.Length; ++index)
      {
        string segment = segments[index];
        int extIndex;
        string ext;
        if (PageManager.HasExtension(segment, knownExtensions, out extIndex, out ext))
        {
          expectedExtension = ext;
          segments[index] = segment.Left(extIndex);
          extensionSegmentIndex = index;
        }
      }
    }

    /// <summary>Resolves the node.</summary>
    /// <param name="segments">The segments.</param>
    /// <param name="parent">The parent.</param>
    /// <param name="expectedExtension">The expected extension.</param>
    /// <param name="extensionSegmentIndex">Index of the extension segment.</param>
    /// <param name="regularNodesCount">The regular nodes count.</param>
    /// <param name="additionalUrls">The additional urls.</param>
    internal PageSiteNode ResolveNode(
      string[] segments,
      PageSiteNode parent,
      out int regularNodesCount)
    {
      Stack stack = new Stack();
      stack.Push((object) new SiteMapBase.PageSiteNodeWrapper()
      {
        Node = parent,
        HasTranslation = true
      });
      for (int index = 0; index < segments.Length; ++index)
      {
        SiteMapBase.PageSiteNodeWrapper childNodeByName = this.GetChildNodeByName((SiteMapBase.PageSiteNodeWrapper) stack.Peek(), segments[index]);
        if (childNodeByName != null && (this.resolvePageWithoutParentTranslation || childNodeByName.HasTranslation))
          stack.Push((object) childNodeByName);
        else
          break;
      }
      PageSiteNode pageSiteNode = parent;
      do
      {
        SiteMapBase.PageSiteNodeWrapper pageSiteNodeWrapper = (SiteMapBase.PageSiteNodeWrapper) stack.Pop();
        if (pageSiteNodeWrapper.HasTranslation)
        {
          pageSiteNode = pageSiteNodeWrapper.Node;
          break;
        }
      }
      while (stack.Count > 0);
      regularNodesCount = stack.Count;
      return pageSiteNode;
    }

    private SiteMapBase.PageSiteNodeWrapper GetChildNodeByName(
      SiteMapBase.PageSiteNodeWrapper parent,
      string urlName)
    {
      return this.GetChildNodeByName(parent.Node, urlName);
    }

    private SiteMapBase.PageSiteNodeWrapper GetChildNodeByName(
      PageSiteNode parent,
      string urlName)
    {
      SiteMapBase.PageSiteNodeWrapper childNodeByName1 = (SiteMapBase.PageSiteNodeWrapper) null;
      SiteMapNodeCollection childNodes = this.GetChildNodes((SiteMapNode) parent, false, false);
      foreach (PageSiteNode parent1 in childNodes)
      {
        bool matchCulture;
        if (parent1.HasUrlName(urlName, out matchCulture))
        {
          childNodeByName1 = new SiteMapBase.PageSiteNodeWrapper()
          {
            Node = parent1,
            HasTranslation = matchCulture
          };
          if (matchCulture)
            return childNodeByName1;
        }
        if (!parent1.RenderAsLink)
        {
          SiteMapBase.PageSiteNodeWrapper childNodeByName2 = this.GetChildNodeByName(parent1, urlName);
          if (childNodeByName2 != null)
            return childNodeByName2;
        }
      }
      if (childNodes is PageSiteNodeCollection siteNodeCollection && siteNodeCollection.Overflowed)
      {
        Guid parentId = parent.Id;
        PageManager pageManager = this.GetPageManager();
        using (new ElevatedModeRegion((IManager) pageManager))
        {
          using (new ReadUncommitedRegion((IManager) pageManager))
          {
            PageNode pageNode = pageManager.GetPageNodes().FirstOrDefault<PageNode>((Expression<Func<PageNode, bool>>) (p => p.Parent.Id == parentId && p.UrlName == (Lstring) urlName && !p.IsDeleted));
            if (pageNode != null)
            {
              PageSiteNode pageSiteNode = new PageSiteNode(this, pageNode, this.PageProviderName);
              childNodes.Add((SiteMapNode) pageSiteNode);
              return new SiteMapBase.PageSiteNodeWrapper()
              {
                Node = pageSiteNode,
                HasTranslation = true
              };
            }
          }
        }
      }
      return childNodeByName1;
    }

    /// <summary>
    /// Verifies if the extension of the node is the same as the one in the requested url
    /// </summary>
    private bool ExtensionIsSameAsRequested(
      SiteMapNode node,
      string expectedExtension,
      string rawUrl,
      string[] parameters)
    {
      if (string.IsNullOrEmpty(rawUrl) || !(node is PageSiteNode pageSiteNode))
        return true;
      string extension = pageSiteNode.Extension;
      if (this.IsMultilingual && parameters != null && parameters.Length > 2 && "Action".Equals(parameters[0], StringComparison.OrdinalIgnoreCase))
      {
        using (new CultureRegion(new CultureInfo(parameters[2])))
          extension = pageSiteNode.Extension;
      }
      return string.IsNullOrEmpty(expectedExtension) && string.IsNullOrEmpty(extension) || string.Compare(expectedExtension, extension, StringComparison.OrdinalIgnoreCase) == 0;
    }

    private string[] GetParameters(
      SiteMapNode node,
      string[] segments,
      int index,
      out bool paramsMismatch)
    {
      int length = segments.Length - index;
      if (length > 0)
      {
        paramsMismatch = ((PageSiteNode) node).NodeType != NodeType.Standard;
        string[] destinationArray = new string[length];
        Array.Copy((Array) segments, index, (Array) destinationArray, 0, length);
        return destinationArray;
      }
      paramsMismatch = false;
      return (string[]) null;
    }

    internal ISiteMapAdditionalUrl StripExtension(ISiteMapAdditionalUrl url)
    {
      foreach (string knownExtension in (IEnumerable<string>) Config.Get<PagesConfig>().KnownExtensions)
      {
        if (url.Url.EndsWith(knownExtension))
        {
          string str = url.Url.Left(url.Url.Length - knownExtension.Length);
          return (ISiteMapAdditionalUrl) new SiteMapBase.AdditionalUrlProxy()
          {
            IsDefault = url.IsDefault,
            Language = url.Language,
            NodeKey = url.NodeKey,
            Url = str
          };
        }
      }
      return (ISiteMapAdditionalUrl) null;
    }

    /// <summary>Finds the node by additional URL.</summary>
    /// <param name="rawUrl">The raw URL.</param>
    /// <param name="urlSegments">The URL segments.</param>
    /// <param name="additionalUrlInfo">The additional URL info.</param>
    /// <returns></returns>
    protected virtual PageSiteNode FindNodeByAdditionalUrl(
      string rawUrl,
      string[] urlSegments,
      out ISiteMapAdditionalUrl additionalUrlInfo)
    {
      additionalUrlInfo = (ISiteMapAdditionalUrl) null;
      if (urlSegments.Length == 0)
        return (PageSiteNode) null;
      IList<ISiteMapAdditionalUrl> additionalUrlsByRoot = this.GetAdditionalUrlsByRoot("~/" + urlSegments[0]);
      if (additionalUrlsByRoot != null && additionalUrlsByRoot.Count > 0)
      {
        string[] searchlUrls = this.GenerateAdditionalUrlsVariations(urlSegments);
        CultureInfo culture = SystemManager.CurrentContext.Culture;
        IEnumerable<ISiteMapAdditionalUrl> source = additionalUrlsByRoot.Where<ISiteMapAdditionalUrl>((Func<ISiteMapAdditionalUrl, bool>) (u => ((IEnumerable<string>) searchlUrls).Contains<string>(u.Url, (IEqualityComparer<string>) StringComparer.CurrentCultureIgnoreCase)));
        foreach (ISiteMapAdditionalUrl mapAdditionalUrl in !SystemManager.CurrentContext.CurrentSite.DefaultCulture.Equals((object) culture) ? source.Where<ISiteMapAdditionalUrl>((Func<ISiteMapAdditionalUrl, bool>) (u => u.Language == culture.Name)) : source.Where<ISiteMapAdditionalUrl>((Func<ISiteMapAdditionalUrl, bool>) (u => u.Language == culture.Name || u.Language == string.Empty)).ToLookup(u => new
        {
          NodeKey = u.NodeKey,
          Language = u.Language
        }).Select<IGrouping<\u003C\u003Ef__AnonymousType70<string, string>, ISiteMapAdditionalUrl>, ISiteMapAdditionalUrl>(gr => SiteMapBase.ResolveUrlData((IEnumerable<ISiteMapAdditionalUrl>) gr, culture)))
        {
          if (this.FindSiteMapNodeFromKey(mapAdditionalUrl.NodeKey, false) is PageSiteNode siteMapNodeFromKey)
          {
            additionalUrlInfo = mapAdditionalUrl;
            return siteMapNodeFromKey;
          }
        }
        if (this.IsMultilingual)
        {
          nodeByAdditionalUrl = (PageSiteNode) null;
          foreach (ISiteMapAdditionalUrl mapAdditionalUrl in source.Where<ISiteMapAdditionalUrl>((Func<ISiteMapAdditionalUrl, bool>) (u => u.IsDefault)))
          {
            if (this.FindSiteMapNodeFromKey(mapAdditionalUrl.NodeKey, false) is PageSiteNode nodeByAdditionalUrl && nodeByAdditionalUrl.IsGroupPage)
            {
              if (mapAdditionalUrl.Language == SystemManager.CurrentContext.CurrentSite.DefaultCulture.Name)
              {
                additionalUrlInfo = mapAdditionalUrl;
                break;
              }
              if (additionalUrlInfo == null)
                additionalUrlInfo = mapAdditionalUrl;
            }
          }
          return nodeByAdditionalUrl;
        }
      }
      return (PageSiteNode) null;
    }

    private static ISiteMapAdditionalUrl ResolveUrlData(
      IEnumerable<ISiteMapAdditionalUrl> gr,
      CultureInfo defaultCulture)
    {
      return gr.Count<ISiteMapAdditionalUrl>() > 1 ? gr.OrderByDescending<ISiteMapAdditionalUrl, string>((Func<ISiteMapAdditionalUrl, string>) (u => u.Language)).First<ISiteMapAdditionalUrl>() : gr.First<ISiteMapAdditionalUrl>();
    }

    /// <summary>Gets the additional URL parameters.</summary>
    /// <param name="node">The node.</param>
    /// <param name="additionalUrlInfo">The additional URL info.</param>
    /// <param name="rawUrl">The raw URL.</param>
    /// <param name="paramsMismatch">The params mismatch.</param>
    /// <returns></returns>
    protected virtual string[] GetAdditionalUrlParameters(
      PageSiteNode node,
      ISiteMapAdditionalUrl additionalUrlInfo,
      string rawUrl,
      out bool paramsMismatch)
    {
      int startIndex = additionalUrlInfo.Url.Length - 2;
      if (this.IsMultilingual && SystemManager.CurrentHttpContext != null)
        SystemManager.CurrentHttpContext.Items[(object) "AdditionalUrlCulture"] = (object) CultureInfo.GetCultureInfo(additionalUrlInfo.Language);
      string[] additionalUrlParameters;
      if (rawUrl.Length > startIndex)
      {
        additionalUrlParameters = rawUrl.Substring(startIndex).Split(new char[1]
        {
          '/'
        }, StringSplitOptions.RemoveEmptyEntries);
        paramsMismatch = node.NodeType != NodeType.Standard && node.NodeType != NodeType.Group && !additionalUrlInfo.IsDefault;
      }
      else
      {
        paramsMismatch = false;
        additionalUrlParameters = (string[]) null;
      }
      return additionalUrlParameters;
    }

    /// <summary>Gets the additional urls by root.</summary>
    /// <param name="urlRoot">The URL root.</param>
    /// <returns></returns>
    /// JavaScriptEmbedControl
    protected internal virtual IList<ISiteMapAdditionalUrl> GetAdditionalUrlsByRoot(
      string urlRoot)
    {
      if (!urlRoot.StartsWith("~/"))
        urlRoot = !urlRoot.StartsWith("/") ? "~/" + urlRoot : "~" + urlRoot;
      string key = SiteMapBase.BuildUrlCacheKey(this.GetAddUrlKeyPrefix(), urlRoot, this.IsMultilingual);
      if (!(SiteMapBase.cache[key] is IList<ISiteMapAdditionalUrl> source1))
      {
        lock (this.addUrlLock)
        {
          if (!(SiteMapBase.cache[key] is IList<ISiteMapAdditionalUrl> source1))
          {
            source1 = (IList<ISiteMapAdditionalUrl>) new List<ISiteMapAdditionalUrl>();
            PageManager pageManager = this.GetPageManager();
            using (new ReadUncommitedRegion((IManager) pageManager))
            {
              using (new ElevatedModeRegion((IManager) pageManager))
              {
                Guid rootNodeId = this.GetRootNode();
                ParameterExpression parameterExpression;
                // ISSUE: method reference
                // ISSUE: method reference
                // ISSUE: method reference
                // ISSUE: method reference
                // ISSUE: method reference
                // ISSUE: method reference
                // ISSUE: method reference
                // ISSUE: method reference
                // ISSUE: method reference
                // ISSUE: method reference
                // ISSUE: method reference
                // ISSUE: method reference
                source1 = (IList<ISiteMapAdditionalUrl>) pageManager.GetUrls<PageUrlData>().Where<PageUrlData>((Expression<Func<PageUrlData, bool>>) (u => u.Node.RootNodeId == rootNodeId && !u.Disabled && (u.Url.Equals(urlRoot) || u.Url.StartsWith(string.Concat(urlRoot, "/")) || u.Url.StartsWith(string.Concat(urlRoot, "."))))).Select<PageUrlData, ISiteMapAdditionalUrl>(Expression.Lambda<Func<PageUrlData, ISiteMapAdditionalUrl>>((Expression) Expression.Convert((Expression) Expression.MemberInit(Expression.New(typeof (SiteMapBase.AdditionalUrlProxy)), (MemberBinding) Expression.Bind((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (SiteMapBase.AdditionalUrlProxy.set_NodeKey)), (Expression) Expression.Call(u.Node.Id, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.ToString)), Array.Empty<Expression>())), (MemberBinding) Expression.Bind((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (SiteMapBase.AdditionalUrlProxy.set_Url)), (Expression) Expression.Property((Expression) parameterExpression, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (UrlData.get_Url)))), (MemberBinding) Expression.Bind((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (SiteMapBase.AdditionalUrlProxy.set_Language)), (Expression) Expression.Property((Expression) Expression.Call((Expression) Expression.Property((Expression) Expression.Property((Expression) Expression.Constant((object) this, typeof (SiteMapBase)), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (SiteMapBase.get_AppSettings))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (IAppSettings.get_Current))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (IAppSettings.GetCultureByLcid)), (Expression) Expression.Property((Expression) parameterExpression, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (UrlData.get_Culture)))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (CultureInfo.get_Name)))), (MemberBinding) Expression.Bind((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (SiteMapBase.AdditionalUrlProxy.set_IsDefault)), (Expression) Expression.Property((Expression) parameterExpression, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (UrlData.get_IsDefault))))), typeof (ISiteMapAdditionalUrl)), parameterExpression)).OrderByDescending<ISiteMapAdditionalUrl, int>((Expression<Func<ISiteMapAdditionalUrl, int>>) (u => u.Url.Length)).ToList<ISiteMapAdditionalUrl>();
                List<ISiteMapAdditionalUrl> list = source1.Select<ISiteMapAdditionalUrl, ISiteMapAdditionalUrl>((Func<ISiteMapAdditionalUrl, ISiteMapAdditionalUrl>) (x => this.StripExtension(x))).Where<ISiteMapAdditionalUrl>((Func<ISiteMapAdditionalUrl, bool>) (x => x != null)).ToList<ISiteMapAdditionalUrl>();
                if (list.Count > 0)
                {
                  foreach (ISiteMapAdditionalUrl mapAdditionalUrl1 in list)
                  {
                    ISiteMapAdditionalUrl url = mapAdditionalUrl1;
                    ISiteMapAdditionalUrl mapAdditionalUrl2 = source1.FirstOrDefault<ISiteMapAdditionalUrl>((Func<ISiteMapAdditionalUrl, bool>) (x => x.Url == url.Url && x.NodeKey == url.NodeKey && x.Language == url.Language));
                    if (mapAdditionalUrl2 != null)
                    {
                      if (!mapAdditionalUrl2.IsDefault && url.IsDefault)
                        source1.Remove(mapAdditionalUrl2);
                      else
                        continue;
                    }
                    source1.Add(url);
                  }
                  source1 = (IList<ISiteMapAdditionalUrl>) source1.OrderByDescending<ISiteMapAdditionalUrl, int>((Func<ISiteMapAdditionalUrl, int>) (u => u.Url.Length)).ToList<ISiteMapAdditionalUrl>();
                }
              }
            }
            ICacheItemExpiration[] cacheItemExpirationArray = new ICacheItemExpiration[2]
            {
              (ICacheItemExpiration) new SlidingTime(TimeSpan.FromSeconds((double) this.cacheExp)),
              (ICacheItemExpiration) new DataItemCacheDependency(typeof (PageUrlData), (string) null)
            };
            SiteMapBase.cache.Add(key, (object) source1, CacheItemPriority.Low, (ICacheItemRefreshAction) null, cacheItemExpirationArray);
          }
        }
      }
      return source1;
    }

    /// <summary>
    /// Generates all variation of the additional urls from segemnts starting from the specified index
    /// </summary>
    /// <param name="segments">The segments.</param>
    /// <param name="startIndex">The start index.</param>
    /// <returns></returns>
    protected string[] GenerateAdditionalUrlsVariations(string[] segments, int startIndex = 0)
    {
      string[] additionalUrlsVariations = new string[segments.Length - startIndex];
      for (int index = 0; index < segments.Length; ++index)
        additionalUrlsVariations[index - startIndex] = "~/" + string.Join("/", segments, 0, index + 1);
      return additionalUrlsVariations;
    }

    protected virtual PageSiteNode GetChildNode(PageSiteNode parent, string urlName)
    {
      PageSiteNode childNode1 = (PageSiteNode) null;
      SiteMapNodeCollection childNodes = this.GetChildNodes((SiteMapNode) parent, false, false);
      foreach (PageSiteNode parent1 in childNodes)
      {
        if (parent1.ContainsUrlName(urlName))
        {
          if (parent1.HasTranslation(SystemManager.CurrentContext.Culture))
            return parent1;
          childNode1 = parent1;
        }
        if (!parent1.RenderAsLink)
        {
          PageSiteNode childNode2 = this.GetChildNode(parent1, urlName);
          if (childNode2 != null)
            return childNode2;
        }
      }
      if (childNodes is PageSiteNodeCollection siteNodeCollection && siteNodeCollection.Overflowed)
      {
        Guid parentId = parent.Id;
        PageManager pageManager = this.GetPageManager();
        using (new ReadUncommitedRegion((IManager) pageManager))
        {
          using (new ElevatedModeRegion((IManager) pageManager))
          {
            PageNode pageNode = pageManager.GetPageNodes().FirstOrDefault<PageNode>((Expression<Func<PageNode, bool>>) (p => p.Parent.Id == parentId && p.UrlName == (Lstring) urlName && !p.IsDeleted));
            if (pageNode != null)
            {
              PageSiteNode childNode3 = new PageSiteNode(this, pageNode, this.PageProviderName);
              childNodes.Add((SiteMapNode) childNode3);
              return childNode3;
            }
          }
        }
      }
      return childNode1;
    }

    /// <summary>
    /// Retrieves a <see cref="T:System.Web.SiteMapNode" /> object based on a specified key.
    /// </summary>
    /// <param name="key">A lookup key with which a <see cref="T:System.Web.SiteMapNode" /> is created.</param>
    /// <returns>
    /// A <see cref="T:System.Web.SiteMapNode" /> that represents the page identified by <paramref name="key" />; otherwise, null, if no corresponding <see cref="T:System.Web.SiteMapNode" /> is found or if security trimming is enabled and the <see cref="T:System.Web.SiteMapNode" /> cannot be returned for the current user. The default is null.
    /// </returns>
    public override SiteMapNode FindSiteMapNodeFromKey(string key) => this.FindSiteMapNodeFromKey(key, true);

    /// <summary>
    /// Retrieves a <see cref="T:System.Web.SiteMapNode" /> object based on a specified key.
    /// </summary>
    /// <param name="key">A lookup key with which a <see cref="T:System.Web.SiteMapNode" /> is created.</param>
    /// <param name="ifAccessible">
    /// if set to <c>true</c> checks if the node is accessible to the user
    /// making the current request and if node is not accessible it returns null even if the node exists.
    /// </param>
    /// <returns>
    /// A <see cref="T:System.Web.SiteMapNode" /> that represents the page identified by <paramref name="key" />; otherwise, null, if no corresponding <see cref="T:System.Web.SiteMapNode" /> is found or if security trimming is enabled and the <see cref="T:System.Web.SiteMapNode" /> cannot be returned for the current user. The default is null.
    /// </returns>
    public virtual SiteMapNode FindSiteMapNodeFromKey(string key, bool ifAccessible)
    {
      if (string.IsNullOrEmpty(key))
        throw new ArgumentNullException(nameof (key));
      string nodeKeyPrefix = this.GetNodeKeyPrefix();
      bool flag = key.StartsWith(nodeKeyPrefix);
      string key1 = flag ? key : nodeKeyPrefix + key.ToUpperInvariant();
      SiteMapNode node = (SiteMapNode) SiteMapBase.cache[key1];
      if (node == null)
      {
        lock (this.nodeLock)
        {
          node = (SiteMapNode) SiteMapBase.cache[key1];
          if (node == null)
          {
            if (flag)
              key = key.Substring(nodeKeyPrefix.Length);
            Guid id;
            if (!Guid.TryParse(key, out id))
              throw new ArgumentException("Invalid format for SiteMapNode key.");
            Guid rootNode = this.GetRootNode();
            if (id == rootNode)
              return this.GetRootNodeCore(ifAccessible);
            PageManager pageManager = this.GetPageManager();
            using (new ReadUncommitedRegion((IManager) pageManager))
            {
              using (new ElevatedModeRegion((IManager) pageManager))
              {
                PageNode pageNode = pageManager.GetPageNodes().Where<PageNode>((Expression<Func<PageNode, bool>>) (pn => pn.Id == id && pn.RootNodeId == rootNode)).Include<PageNode>((Expression<Func<PageNode, object>>) (p => p.Parent)).FirstOrDefault<PageNode>();
                if (pageNode == null && pageManager.Provider is IOpenAccessDataProvider)
                {
                  if (Bootstrapper.IsReady)
                  {
                    try
                    {
                      PageNodeReference itemById = (pageManager.Provider as IOpenAccessDataProvider).GetContext().GetItemById<PageNodeReference>(id.ToString());
                      if (itemById.PageNode != null)
                      {
                        pageNode = itemById.PageNode;
                        ((IDataItem) pageNode).Provider = (object) pageManager.Provider;
                      }
                    }
                    catch (ItemNotFoundException ex)
                    {
                    }
                  }
                }
                if (pageNode != null)
                {
                  if (!pageNode.ModuleName.IsNullOrEmpty())
                  {
                    if (!SystemManager.IsModuleEnabled(pageNode.ModuleName))
                      goto label_31;
                  }
                  node = (SiteMapNode) new PageSiteNode(this, pageNode, this.PageProviderName)
                  {
                    CacheKey = key1
                  };
                  ICacheItemExpiration[] cacheItemExpirationArray = new ICacheItemExpiration[2]
                  {
                    (ICacheItemExpiration) new SlidingTime(TimeSpan.FromSeconds((double) this.cacheExp)),
                    (ICacheItemExpiration) ((PageSiteNode) node).CacheDependency
                  };
                  SiteMapBase.cache.Add(key1, (object) node, CacheItemPriority.Normal, (ICacheItemRefreshAction) null, cacheItemExpirationArray);
                }
              }
            }
          }
        }
      }
label_31:
      return ifAccessible ? this.ReturnNodeIfAccessible(node) : node;
    }

    /// <summary>
    /// When overridden in a derived class, retrieves the child nodes of a specific <see cref="T:System.Web.SiteMapNode" />.
    /// </summary>
    /// <param name="node">The <see cref="T:System.Web.SiteMapNode" /> for which to retrieve all child nodes.</param>
    /// <returns>
    /// A read-only <see cref="T:System.Web.SiteMapNodeCollection" /> that contains the immediate child nodes of the specified <see cref="T:System.Web.SiteMapNode" />; otherwise, null or an empty collection, if no child nodes exist.
    /// </returns>
    public override SiteMapNodeCollection GetChildNodes(SiteMapNode node) => this.GetChildNodes(node, true);

    /// <summary>
    /// When overridden in a derived class, retrieves the child nodes of a specific <see cref="T:System.Web.SiteMapNode" />.
    /// </summary>
    /// <param name="parent">The <see cref="T:System.Web.SiteMapNode" /> for which to retrieve all child nodes.</param>
    /// <param name="ifAccessible">
    /// if set to <c>true</c> checks if the child nodes are accessible to the user
    /// making the current request and returns only the accessible ones.
    /// </param>
    /// <returns>
    /// A read-only <see cref="T:System.Web.SiteMapNodeCollection" /> that contains the immediate child nodes of the specified <see cref="T:System.Web.SiteMapNode" />; otherwise, null or an empty collection, if no child nodes exist.
    /// </returns>
    public virtual SiteMapNodeCollection GetChildNodes(
      SiteMapNode parent,
      bool ifAccessible)
    {
      return this.GetChildNodes(parent, ifAccessible, true);
    }

    /// <summary>
    /// When overridden in a derived class, retrieves the child nodes of a specific <see cref="T:System.Web.SiteMapNode" />.
    /// </summary>
    /// <param name="parent">The <see cref="T:System.Web.SiteMapNode" /> for which to retrieve all child nodes.</param>
    /// <param name="ifAccessible">if set to <c>true</c> checks if the child nodes are accessible to the user
    /// making the current request and returns only the accessible ones.</param>
    /// <param name="batchLoading">if set to <c>true</c> [batch loading].</param>
    /// <returns>
    /// A read-only <see cref="T:System.Web.SiteMapNodeCollection" /> that contains the immediate child nodes of the specified <see cref="T:System.Web.SiteMapNode" />; otherwise, null or an empty collection, if no child nodes exist.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">node</exception>
    internal SiteMapNodeCollection GetChildNodes(
      SiteMapNode parent,
      bool ifAccessible,
      bool batchLoading)
    {
      return this.GetChildNodes(parent, ifAccessible, batchLoading, (IQueryable<PageNode>) null);
    }

    /// <summary>
    /// When overridden in a derived class, retrieves the child nodes of a specific <see cref="T:System.Web.SiteMapNode" />.
    /// </summary>
    /// <param name="parent">The <see cref="T:System.Web.SiteMapNode" /> for which to retrieve all child nodes.</param>
    /// <param name="ifAccessible">if set to <c>true</c> checks if the child nodes are accessible to the user
    /// making the current request and returns only the accessible ones.</param>
    /// <param name="batchLoading">if set to <c>true</c> [batch loading].</param>
    /// <param name="pageNodes">A collection of page nodes to be populated.</param>
    /// <returns>
    /// A read-only <see cref="T:System.Web.SiteMapNodeCollection" /> that contains the immediate child nodes of the specified <see cref="T:System.Web.SiteMapNode" />; otherwise, null or an empty collection, if no child nodes exist.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">node</exception>
    internal SiteMapNodeCollection GetChildNodes(
      SiteMapNode parent,
      bool ifAccessible,
      bool batchLoading,
      IQueryable<PageNode> pageNodes)
    {
      if (parent == null)
        throw new ArgumentNullException("node");
      string key = this.GetChildKeyPrefix() + parent.Key;
      SiteMapNodeCollection childNodes = (SiteMapNodeCollection) SiteMapBase.cache[key];
      if (childNodes == null)
      {
        lock (this.childLock)
        {
          childNodes = (SiteMapNodeCollection) SiteMapBase.cache[key];
          if (childNodes == null)
          {
            PageSiteNodeCollection collection = new PageSiteNodeCollection();
            if (parent is PageSiteNode pageSiteNode)
            {
              if (!pageSiteNode.HasAnyChildNodesInternal.HasValue || pageSiteNode.HasAnyChildNodesInternal.Value)
              {
                Guid parentId = pageSiteNode.Id;
                if (pageNodes == null)
                {
                  PageManager pageManager = this.GetPageManager();
                  using (new ElevatedModeRegion((IManager) pageManager))
                  {
                    using (new ReadUncommitedRegion((IManager) pageManager))
                    {
                      pageNodes = (IQueryable<PageNode>) this.GetPageNodesQuery(pageManager).Include<PageNode>((Expression<Func<PageNode, object>>) (p => p.Permissions)).Include<PageNode>((Expression<Func<PageNode, object>>) (p => p.Attributes)).Include<PageNode>((Expression<Func<PageNode, object>>) (p => p.Urls)).Where<PageNode>((Expression<Func<PageNode, bool>>) (p => p.ParentId == parentId)).OrderBy<PageNode, float>((Expression<Func<PageNode, float>>) (p => p.Ordinal));
                      this.PopulatePageNodes(collection, pageNodes, parent);
                    }
                  }
                }
                else
                  this.PopulatePageNodes(collection, pageNodes, parent);
                pageSiteNode.HasAnyChildNodesInternal = new bool?(collection.Count > 0);
              }
              if (!this.disableBatchLoading & batchLoading && SystemManager.CurrentHttpContext != null)
              {
                if (!(SystemManager.CurrentHttpContext.Items[(object) "sf_BatchPagesKey"] is HashSet<string> stringSet))
                {
                  stringSet = new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
                  SystemManager.CurrentHttpContext.Items[(object) "sf_BatchPagesKey"] = (object) stringSet;
                }
                stringSet.Add(pageSiteNode.Key);
              }
            }
            childNodes = (SiteMapNodeCollection) collection;
            ICacheItemExpiration[] cacheItemExpirationArray = new ICacheItemExpiration[2]
            {
              (ICacheItemExpiration) new SlidingTime(TimeSpan.FromSeconds((double) this.cacheExp)),
              (ICacheItemExpiration) new DataItemCacheDependency(typeof (PageNode), parent.Key)
            };
            SiteMapBase.cache.Add(key, (object) childNodes, CacheItemPriority.Normal, (ICacheItemRefreshAction) new Telerik.Sitefinity.Data.DisposeItemOnExpire(), cacheItemExpirationArray);
          }
        }
      }
      if (this.SecurityTrimmingEnabled & ifAccessible)
      {
        HttpContext current = HttpContext.Current;
        SiteMapNodeCollection mapNodeCollection = new SiteMapNodeCollection(childNodes.Count);
        foreach (SiteMapNode siteMapNode in childNodes)
        {
          if (siteMapNode.IsAccessibleToUser(current))
            mapNodeCollection.Add(siteMapNode);
        }
        childNodes = mapNodeCollection;
      }
      return childNodes;
    }

    private void PopulatePageNodes(
      PageSiteNodeCollection collection,
      IQueryable<PageNode> pageNodes,
      SiteMapNode parent)
    {
      if (this.maxPageNodes > 0)
      {
        IQueryable<Guid> source = pageNodes.Select<PageNode, Guid>((Expression<Func<PageNode, Guid>>) (p => p.Id));
        if (source.Count<Guid>() > this.maxPageNodes)
        {
          List<Guid> pageIdsToLoad = source.Take<Guid>(this.maxPageNodes).ToList<Guid>();
          pageNodes = pageNodes.Where<PageNode>((Expression<Func<PageNode, bool>>) (pn => pageIdsToLoad.Contains(pn.Id)));
          collection.Overflowed = true;
        }
      }
      bool isReady = Bootstrapper.IsReady;
      foreach (PageNode pageNode in (IEnumerable<PageNode>) pageNodes)
      {
        if (!isReady || this.IsPageNodeEnabled(pageNode))
        {
          SiteMapNode siteMapNodeForPage = this.GetSiteMapNodeForPage(pageNode, parent);
          collection.Add(siteMapNodeForPage);
        }
      }
    }

    public virtual SiteMapNode GetSiteMapNodeForPage(PageNode pageNode) => this.GetSiteMapNodeForPage(pageNode, (SiteMapNode) null);

    internal SiteMapNode GetSiteMapNodeForPage(
      PageNode pageNode,
      SiteMapNode parentSiteNode)
    {
      string key = this.GetNodeKeyPrefix() + pageNode.Id.ToString().ToUpperInvariant();
      SiteMapNode siteMapNodeForPage = (SiteMapNode) SiteMapBase.cache[key];
      if (siteMapNodeForPage == null)
      {
        lock (this.nodeLock)
        {
          siteMapNodeForPage = (SiteMapNode) SiteMapBase.cache[key];
          if (siteMapNodeForPage == null)
          {
            siteMapNodeForPage = (SiteMapNode) new PageSiteNode(this, pageNode, parentSiteNode, this.PageProviderName)
            {
              CacheKey = key
            };
            ICacheItemExpiration[] cacheItemExpirationArray = new ICacheItemExpiration[2]
            {
              (ICacheItemExpiration) new SlidingTime(TimeSpan.FromSeconds((double) this.cacheExp)),
              (ICacheItemExpiration) ((PageSiteNode) siteMapNodeForPage).CacheDependency
            };
            SiteMapBase.cache.Add(key, (object) siteMapNodeForPage, CacheItemPriority.Normal, (ICacheItemRefreshAction) null, cacheItemExpirationArray);
          }
        }
      }
      return siteMapNodeForPage;
    }

    /// <summary>
    /// When overridden in a derived class, retrieves the parent node of a specific <see cref="T:System.Web.SiteMapNode" /> object.
    /// </summary>
    /// <param name="node">The <see cref="T:System.Web.SiteMapNode" /> for which to retrieve the parent node.</param>
    /// <returns>
    /// A <see cref="T:System.Web.SiteMapNode" /> that represents the parent of <paramref name="node" />; otherwise, null, if the <see cref="T:System.Web.SiteMapNode" /> has no parent or security trimming is enabled and the parent node is not accessible to the current user.
    /// Note:<see cref="M:System.Web.SiteMapProvider.GetParentNode(System.Web.SiteMapNode)" /> might also return null if the parent node belongs to a different provider. In this case, use the <see cref="P:System.Web.SiteMapNode.ParentNode" /> property of <paramref name="node" /> instead.
    /// </returns>
    public override SiteMapNode GetParentNode(SiteMapNode node) => this.GetParentNode(node, true);

    /// <summary>
    /// When overridden in a derived class, retrieves the parent node of a specific <see cref="T:System.Web.SiteMapNode" /> object.
    /// </summary>
    /// <param name="node">The <see cref="T:System.Web.SiteMapNode" /> for which to retrieve the parent node.</param>
    /// <param name="ifAccessible">
    /// if set to <c>true</c> checks if the node is accessible to the user
    /// making the current request and if node is not accessible it returns null even if the node exists.
    /// </param>
    /// <returns>
    /// A <see cref="T:System.Web.SiteMapNode" /> that represents the parent of <paramref name="node" />; otherwise, null, if the <see cref="T:System.Web.SiteMapNode" /> has no parent or security trimming is enabled and the parent node is not accessible to the current user.
    /// Note:<see cref="M:System.Web.SiteMapProvider.GetParentNode(System.Web.SiteMapNode)" /> might also return null if the parent node belongs to a different provider. In this case, use the <see cref="P:System.Web.SiteMapNode.ParentNode" /> property of <paramref name="node" /> instead.
    /// </returns>
    public virtual SiteMapNode GetParentNode(SiteMapNode childNode, bool ifAccessible)
    {
      if (childNode == null)
        throw new ArgumentNullException("node");
      if (!(childNode is PageSiteNode pageSiteNode))
        throw new ArgumentException("Invalid SiteMapNode Type.");
      return !string.IsNullOrEmpty(pageSiteNode.ParentKey) ? this.FindSiteMapNodeFromKey(pageSiteNode.ParentKey, ifAccessible) : (SiteMapNode) null;
    }

    /// <summary>
    /// When overridden in a derived class, retrieves the root node of all the nodes that are currently managed by the current provider.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.Web.SiteMapNode" /> that represents the root node of the set of nodes that the current provider manages.
    /// </returns>
    protected override SiteMapNode GetRootNodeCore() => this.GetRootNodeCore(true);

    /// <summary>
    /// When overridden in a derived class, retrieves the root node of all the nodes that are currently managed by the current provider.
    /// </summary>
    /// <param name="ifAccessible">
    /// if set to <c>true</c> checks if the node is accessible to the user
    /// making the current request and if node is not accessible it returns null even if the node exists.
    /// </param>
    /// <returns>
    /// A <see cref="T:System.Web.SiteMapNode" /> that represents the root node of the set of nodes that the current provider manages.
    /// </returns>
    protected internal virtual SiteMapNode GetRootNodeCore(bool ifAccessible)
    {
      string rootNodeKey = this.GetRootNodeKey();
      SiteMapNode node = (SiteMapNode) SiteMapBase.cache[rootNodeKey];
      if (node == null)
      {
        lock (this.nodeLock)
        {
          node = (SiteMapNode) SiteMapBase.cache[rootNodeKey];
          if (node == null)
          {
            PageManager pageManager = this.GetPageManager();
            using (new ReadUncommitedRegion((IManager) pageManager))
            {
              using (new ElevatedModeRegion((IManager) pageManager))
              {
                node = (SiteMapNode) new PageSiteNode(this, pageManager.GetPageNode(this.GetRootNode()), this.PageProviderName)
                {
                  CacheKey = rootNodeKey
                };
                ICacheItemExpiration[] cacheItemExpirationArray = new ICacheItemExpiration[2]
                {
                  (ICacheItemExpiration) new SlidingTime(TimeSpan.FromSeconds((double) this.cacheExp)),
                  (ICacheItemExpiration) ((PageSiteNode) node).CacheDependency
                };
                SiteMapBase.cache.Add(rootNodeKey, (object) node, CacheItemPriority.Normal, (ICacheItemRefreshAction) null, cacheItemExpirationArray);
              }
            }
          }
        }
      }
      return ifAccessible ? this.ReturnNodeIfAccessible(node) : node;
    }

    /// <summary>
    /// Determines whether [is accessible to user internal] [the specified node].
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="user">The user.</param>
    /// <returns>
    /// 	<c>true</c> if [is accessible to user internal] [the specified node]; otherwise, <c>false</c>.
    /// </returns>
    internal virtual bool IsAccessibleToUserInternal(SiteMapNode node, SitefinityIdentity user)
    {
      bool userInternal = false;
      if (!user.IsUnrestricted)
      {
        if (node.Roles != null && node.Roles.Count > 0)
        {
          userInternal = node.Roles.Cast<Guid>().Any<Guid>((Func<Guid, bool>) (p => user.UserId == p || user.Roles.Any<RoleInfo>((Func<RoleInfo, bool>) (r => r.Id == p))));
          if (userInternal)
          {
            if (node is PageSiteNode pageNode)
            {
              if (pageNode.DeniedRoles != null && pageNode.DeniedRoles.Count > 0)
                userInternal = !pageNode.DeniedRoles.Any<Guid>((Func<Guid, bool>) (p => user.UserId == p || user.Roles.Any<RoleInfo>((Func<RoleInfo, bool>) (r => r.Id == p))));
              if (userInternal)
              {
                if (SiteMapBase.filters == null)
                  SiteMapBase.filters = (IList<ISitemapNodeFilter>) ObjectFactory.Container.ResolveAll<ISitemapNodeFilter>().ToList<ISitemapNodeFilter>();
                foreach (ISitemapNodeFilter filter in (IEnumerable<ISitemapNodeFilter>) SiteMapBase.filters)
                {
                  userInternal &= !filter.IsNodeAccessPrevented(pageNode);
                  if (!userInternal)
                    break;
                }
              }
            }
          }
          else
            userInternal = false;
        }
      }
      else
        userInternal = true;
      return userInternal;
    }

    /// <summary>
    /// Retrieves a Boolean value indicating whether the specified <see cref="T:System.Web.SiteMapNode" /> object can be viewed by the user in the specified context.
    /// </summary>
    /// <param name="context">The <see cref="T:System.Web.HttpContext" /> that contains user information.</param>
    /// <param name="node">The <see cref="T:System.Web.SiteMapNode" /> that is requested by the user.</param>
    /// <returns>
    /// true if security trimming is enabled and <paramref name="node" /> can be viewed by the user or security trimming is not enabled; otherwise, false.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// 	<paramref name="context" /> is null.
    /// - or -
    /// <paramref name="node" /> is null.
    /// </exception>
    public override bool IsAccessibleToUser(HttpContext context, SiteMapNode node)
    {
      if (node == null)
        throw new ArgumentNullException(nameof (node));
      if (context == null)
        throw new ArgumentNullException(nameof (context));
      if (!this.SecurityTrimmingEnabled)
        return true;
      SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
      bool isAccessible = false;
      if (currentIdentity != null)
      {
        if (currentIdentity.IsUnrestricted)
          return true;
        isAccessible = SiteMapBase.GetNodeFiltersCache(currentIdentity).IsAccessible(this, node, currentIdentity);
      }
      EventHandler<IsAccessibleArgs> eventHandler = (EventHandler<IsAccessibleArgs>) this.Events[(object) "IsNodeAccessible"];
      if (eventHandler == null)
        return isAccessible;
      IsAccessibleArgs e = new IsAccessibleArgs(node, (ClaimsPrincipal) null, isAccessible);
      eventHandler((object) this, e);
      return e.IsAccessible;
    }

    /// <summary>
    /// Clears cached ISitemapNodeFilter results for all users
    /// </summary>
    public static void FlushNodeFiltersCacheForAllUsers()
    {
      if (!(SiteMapBase.cache[SiteMapBase.NodeFiltersCacheKey] is IDictionary<Guid, SiteMapBase.NodeFiltersCache> dictionary))
        return;
      lock (SiteMapBase.nodeFiltersCacheSynch)
        dictionary.Clear();
    }

    private static SiteMapBase.NodeFiltersCache GetNodeFiltersCache(
      SitefinityIdentity identity)
    {
      Guid key = identity.UserId;
      if (identity.IsAuthenticated && identity.UserId == Guid.Empty)
        key = SiteMapBase.AuthenticatedExternalUserKey;
      if (!(SiteMapBase.cache[SiteMapBase.NodeFiltersCacheKey] is IDictionary<Guid, SiteMapBase.NodeFiltersCache> dictionary1))
      {
        lock (SiteMapBase.nodeFiltersCacheSynch)
        {
          if (!(SiteMapBase.cache[SiteMapBase.NodeFiltersCacheKey] is IDictionary<Guid, SiteMapBase.NodeFiltersCache> dictionary1))
          {
            dictionary1 = (IDictionary<Guid, SiteMapBase.NodeFiltersCache>) new Dictionary<Guid, SiteMapBase.NodeFiltersCache>();
            SiteMapBase.cache.Add(SiteMapBase.NodeFiltersCacheKey, (object) dictionary1, CacheItemPriority.Normal, (ICacheItemRefreshAction) null, (ICacheItemExpiration) new DataItemCacheDependency(typeof (Permission), (string) null), (ICacheItemExpiration) new SlidingTime(TimeSpan.FromMinutes(5.0)));
          }
        }
      }
      SiteMapBase.NodeFiltersCache nodeFiltersCache;
      if (!dictionary1.TryGetValue(key, out nodeFiltersCache))
      {
        lock (SiteMapBase.nodeFiltersCacheSynch)
        {
          if (!dictionary1.TryGetValue(key, out nodeFiltersCache))
          {
            nodeFiltersCache = new SiteMapBase.NodeFiltersCache();
            dictionary1.Add(key, nodeFiltersCache);
          }
        }
      }
      return nodeFiltersCache;
    }

    /// <summary>
    /// Occurs when evaluating a SiteMap node whether it is accessible to the current user.
    /// </summary>
    [Obsolete("This event should not be used! It will be removed in future version of Sitefinity.")]
    public event EventHandler<IsAccessibleArgs> IsNodeAccessible
    {
      add => this.Events.AddHandler((object) "IsNodeAccessible", (Delegate) value);
      remove => this.Events.RemoveHandler((object) "IsNodeAccessible", (Delegate) value);
    }

    private EventHandlerList Events
    {
      get
      {
        EventHandlerList events = (EventHandlerList) SystemManager.CurrentHttpContext.Items[(object) "SiteMapEvents"];
        if (events == null)
        {
          events = new EventHandlerList();
          SystemManager.CurrentHttpContext.Items[(object) "SiteMapEvents"] = (object) events;
        }
        return events;
      }
    }

    /// <summary>Returns the node if accessible to user.</summary>
    /// <param name="node">The node.</param>
    /// <returns></returns>
    protected internal new virtual SiteMapNode ReturnNodeIfAccessible(SiteMapNode node) => node != null && node.IsAccessibleToUser(HttpContext.Current) ? node : (SiteMapNode) null;

    /// <summary>
    /// Gets a specific page site node depending on the passed page site node.
    /// </summary>
    /// <param name="node">The page site node to be examined.</param>
    /// <returns></returns>
    protected virtual PageSiteNode GetPageSiteNode(PageSiteNode node) => node;

    protected virtual bool IsPageNodeEnabled(PageNode pageNode) => true;

    /// <summary>Gets all initialized SiteMap providers.</summary>
    /// <value>The providers.</value>
    protected static IDictionary<string, SiteMapProvider> Providers => SiteMapBase.providers;

    /// <summary>
    /// Returns the PageSiteNode that is currently being displayed. This is either SiteMapBase.GetCurrentProvider().CurrentNode
    /// if it is not a group page node, or its first child node.
    /// </summary>
    /// <returns></returns>
    public static PageSiteNode GetActualCurrentNode()
    {
      node = (PageSiteNode) null;
      SiteMapProvider currentProvider = SiteMapBase.GetCurrentProvider();
      if (currentProvider != null && currentProvider.CurrentNode is PageSiteNode node)
      {
        node = RouteHelper.GetFirstPageDataNode(node, true);
        SiteMapBase siteMapBase = currentProvider as SiteMapBase;
        if (node != null && siteMapBase != null && siteMapBase.FindSiteMapNodeForSpecificLanguage((SiteMapNode) node, SystemManager.CurrentContext.Culture) is PageSiteNode specificLanguage)
          return specificLanguage;
      }
      return node;
    }

    /// <summary>
    /// Returns the current node. Note: this returns the current node even if it is a group page node.
    /// To get the actual displayed node, use the GetCurrentlyDisplayedNode method.
    /// </summary>
    /// <returns></returns>
    public static PageSiteNode GetCurrentNode() => SiteMapBase.GetCurrentProvider().CurrentNode as PageSiteNode;

    /// <summary>
    /// Returns the last found current node. Note: this returns the last found node even if it is a group page node.
    /// To get the actual displayed node, use the GetCurrentlyDisplayedNode method.
    /// </summary>
    /// <returns></returns>
    internal static PageSiteNode GetLastFoundNode() => SiteMapBase.GetActualCurrentNode() ?? SystemManager.HttpContextItems[(object) "LastFoundPageNode"] as PageSiteNode;

    /// <summary>Specifies if the sitemap is in multilingual mode</summary>
    public virtual bool IsMultilingual => this.AppSettings.Multilingual;

    /// <summary>Gets the default application settings information.</summary>
    public IAppSettings AppSettings => SystemManager.CurrentContext.AppSettings;

    public static string BuildUrlCacheKey(string urlKey, string rawUrl, bool isMultilingual) => !isMultilingual ? urlKey + rawUrl.ToUpperInvariant() : urlKey + rawUrl.ToUpperInvariant() + "|" + SystemManager.CurrentContext.Culture.Name.ToUpperInvariant();

    public static string GetUrlKey(string name) => SiteMapBase.GetKey(name, "|urls|");

    public static string GetNodeKey(string name) => SiteMapBase.GetKey(name, "|nodes|");

    public static string GetChildKey(string name) => SiteMapBase.GetKey(name, "|child|");

    public static string GetAddUrlKey(string name) => SiteMapBase.GetKey(name, "|addurls|");

    internal static string GetDataKey(string name) => SiteMapBase.GetKey(name, "|data|");

    internal static string GetPersonalizedDataKey(string name) => SiteMapBase.GetKey(name, "|pers_data|");

    public static string GetKey(string name, string suffix) => name + suffix;

    public static string GetChildKey(string name, string parentKey) => SiteMapBase.GetChildKey(name) + parentKey;

    /// <summary>
    /// Initializes the <see cref="T:System.Web.SiteMapProvider" /> implementation, including any
    /// resources that are needed to load site map data from persistent storage.
    /// </summary>
    /// <param name="name">The name of the provider to initialize.</param>
    /// <param name="attributes">
    /// A <see cref="T:System.Collections.Specialized.NameValueCollection" /> that can contain additional attributes to
    /// help initialize the provider. These attributes are read from the site map provider
    /// configuration in the Web.config file.
    /// </param>
    public override void Initialize(string name, NameValueCollection attributes)
    {
      try
      {
        string attribute1 = attributes["cacheExpiration"];
        this.cacheExp = !string.IsNullOrEmpty(attribute1) ? int.Parse(attribute1) : 600;
        attributes.Remove("cacheExpiration");
        this.ResourceKey = typeof (PageResources).Name;
        string attribute2 = attributes["enableLocalization"];
        if (attribute2 == null)
          this.EnableLocalization = WebConfig.ResourceProviderFactoryType != (Type) null && typeof (ExtendedResourceProviderFactory).IsAssignableFrom(WebConfig.ResourceProviderFactoryType);
        else
          this.EnableLocalization = bool.Parse(attribute2);
        attributes.Remove("enableLocalization");
        if (attributes["securityTrimmingEnabled"] == null)
          attributes.Add("securityTrimmingEnabled", "true");
        base.Initialize(name, attributes);
        string attribute3 = attributes["maxPageNodes"];
        this.maxPageNodes = !string.IsNullOrEmpty(attribute3) ? int.Parse(attribute3) : 0;
        string attribute4 = attributes["resolvePageWithoutParentTranslation"];
        if (!string.IsNullOrEmpty(attribute4))
          this.resolvePageWithoutParentTranslation = bool.Parse(attribute4);
        this.pageProviderName = attributes["pageProvider"];
        if (string.IsNullOrEmpty(this.pageProviderName))
          this.pageProviderName = PageManager.GetManager().Provider.Name;
        string attribute5 = attributes["disableBatchLoading"];
        if (!string.IsNullOrEmpty(attribute5))
          this.disableBatchLoading = bool.Parse(attribute5);
        if (this.Name == "SitefinitySiteMap")
        {
          this.siteMapKeyResolver = (SiteMapBase.ISiteMapKeyResolver) new SiteMapBase.DynamicSiteMapKeyResolver();
        }
        else
        {
          if (Config.Get<PagesConfig>().BackendRootNode.Equals(name, StringComparison.OrdinalIgnoreCase))
            this.disableBatchLoading = true;
          string attribute6 = attributes["rootNode"];
          if (string.IsNullOrEmpty(attribute6))
            throw new ConfigurationErrorsException("The attribute \"rootNode\" is required for eny Sitefinity provider except '{0}'.".Arrange((object) "SitefinitySiteMap"));
          PageManager pageManager = this.GetPageManager();
          using (new ElevatedModeRegion((IManager) pageManager))
          {
            PageNode pageNode;
            if (attribute6.IsGuid())
            {
              // ISSUE: object of a compiler-generated type is created
              // ISSUE: variable of a compiler-generated type
              SiteMapBase.\u003C\u003Ec__DisplayClass107_0 displayClass1070 = new SiteMapBase.\u003C\u003Ec__DisplayClass107_0();
              // ISSUE: reference to a compiler-generated field
              displayClass1070.rootNodeId = Guid.Parse(attribute6);
              // ISSUE: reference to a compiler-generated field
              pageNode = pageManager.GetPageNodes().FirstOrDefault<PageNode>((Expression<Func<PageNode, bool>>) (n => n.Id == displayClass1070.rootNodeId));
            }
            else
            {
              // ISSUE: object of a compiler-generated type is created
              // ISSUE: variable of a compiler-generated type
              SiteMapBase.\u003C\u003Ec__DisplayClass107_1 displayClass1071 = new SiteMapBase.\u003C\u003Ec__DisplayClass107_1();
              // ISSUE: reference to a compiler-generated field
              displayClass1071.lowerRootNodeName = attribute6.ToLowerInvariant();
              ParameterExpression parameterExpression;
              // ISSUE: method reference
              // ISSUE: field reference
              pageNode = pageManager.GetPageNodes().FirstOrDefault<PageNode>(Expression.Lambda<Func<PageNode, bool>>((Expression) Expression.AndAlso(n.Parent == default (object), (Expression) Expression.Equal((Expression) Expression.Call(n.Name, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (string.ToLower)), Array.Empty<Expression>()), (Expression) Expression.Field((Expression) Expression.Constant((object) displayClass1071, typeof (SiteMapBase.\u003C\u003Ec__DisplayClass107_1)), FieldInfo.GetFieldFromHandle(__fieldref (SiteMapBase.\u003C\u003Ec__DisplayClass107_1.lowerRootNodeName))))), parameterExpression));
            }
            if (pageNode == null)
              throw new ConfigurationErrorsException(Res.Get<ErrorMessages>().InvalidRootSiteNode.Arrange((object) attribute6));
            this.siteMapKeyResolver = (SiteMapBase.ISiteMapKeyResolver) new SiteMapBase.StaticSiteMapKeyResolver(attribute6, pageNode.Id);
          }
        }
        SiteMapBase.appPath = HttpRuntime.AppDomainAppVirtualPath;
      }
      catch (Exception ex)
      {
        if (!Exceptions.HandleException(ex, ExceptionPolicyName.Global))
          return;
        throw;
      }
    }

    /// <summary>Gets the page manager.</summary>
    /// <returns></returns>
    protected internal virtual PageManager GetPageManager()
    {
      PageManager pageManager = (PageManager) null;
      IDictionary httpContextItems = SystemManager.HttpContextItems;
      if (httpContextItems != null && httpContextItems[(object) this.GetSiteMapContextKey()] is SiteMapBase.SiteMapContext siteMapContext)
        pageManager = PageManager.GetManager(this.PageProviderName, siteMapContext.TransactionName);
      if (pageManager == null)
      {
        pageManager = PageManager.GetManager(this.PageProviderName, "SF_SiteMap");
        TransactionManager.DisposeTransaction("SF_SiteMap");
      }
      pageManager.Provider.FetchAllLanguagesData();
      return pageManager;
    }

    /// <summary>Gets the current site map provider.</summary>
    /// <returns></returns>
    public static SiteMapProvider GetCurrentProvider()
    {
      HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
      IDictionary items = currentHttpContext.Items;
      SiteMapProvider siteMapProvider = (SiteMapProvider) items[(object) "SF_SiteMap"];
      if (siteMapProvider == null && !items.Contains((object) "SF_SiteMap"))
      {
        siteMapProvider = SiteMapBase.GetSiteMapProvider(currentHttpContext.Request.RequestContext);
        items[(object) "SF_SiteMap"] = (object) siteMapProvider;
      }
      return siteMapProvider;
    }

    /// <summary>Gets the site map provider.</summary>
    /// <param name="context">The context.</param>
    /// <returns></returns>
    public static SiteMapProvider GetSiteMapProvider(RequestContext context)
    {
      if (context == null)
        throw new ArgumentNullException(nameof (context));
      return context.RouteData.Route is BackendRoute ? (SiteMapProvider) BackendSiteMap.GetCurrentProvider() : SiteMapBase.GetSiteMapProviderForRequest();
    }

    /// <summary>
    /// Gets the site map provider for the provided site root.
    /// </summary>
    /// <param name="rootName">Name of the root.</param>
    /// <returns></returns>
    public static SiteMapProvider GetSiteMapProvider(string rootName) => SiteMapBase.GetSiteMapProvider<SitefinitySiteMap>(rootName);

    internal static SiteMapProvider GetSiteMapProviderForRequest(
      HttpContextBase context = null)
    {
      return SiteMapProviderResolver.Current.GetSiteMapProviderForRequest(context);
    }

    internal static SiteMapProvider GetSiteMapProviderForPageNode(PageNode node) => SiteMapProviderResolver.Current.GetSiteMapProviderForPageNode(node);

    internal static SiteMapProvider GetSiteMapProviderForUrl(string url) => SiteMapProviderResolver.Current.GetSiteMapProviderForUrl(url);

    /// <summary>
    /// Gets the site map provider for the provided site root.
    /// </summary>
    /// <typeparam name="T">Type of the sitemap provider</typeparam>
    /// <param name="rootName">Name of the root.</param>
    /// <returns></returns>
    public static SiteMapProvider GetSiteMapProvider<T>(string rootName) where T : SiteMapBase, new()
    {
      string key = rootName;
      SiteMapProvider siteMapProvider;
      if (!SiteMapBase.Providers.TryGetValue(key, out siteMapProvider))
      {
        lock (SiteMapBase.providersLock)
        {
          if (!SiteMapBase.Providers.TryGetValue(key, out siteMapProvider))
          {
            if (SiteMap.Enabled)
            {
              foreach (object provider in (ProviderCollection) SiteMap.Providers)
              {
                if (provider is SiteMapBase siteMapBase && (siteMapBase.Name == rootName || siteMapBase.GetRootNodeName() == rootName))
                {
                  siteMapProvider = (SiteMapProvider) siteMapBase;
                  break;
                }
              }
            }
            if (siteMapProvider == null)
            {
              PagesConfig pagesConfig = Config.Get<PagesConfig>();
              NameValueCollection config = new NameValueCollection();
              config["rootNode"] = rootName;
              config["pageProvider"] = pagesConfig.DefaultProvider;
              siteMapProvider = (SiteMapProvider) new T();
              siteMapProvider.Initialize(rootName, config);
            }
            SiteMapBase.Providers.Add(key, siteMapProvider);
          }
        }
      }
      return siteMapProvider;
    }

    private Guid GetRootNode() => this.siteMapKeyResolver.RootNode;

    private string GetRootNodeName() => this.siteMapKeyResolver.RootNodeName;

    private string GetRootNodeKey() => this.siteMapKeyResolver.RootNodeKey;

    private string GetUrlKeyPrefix() => this.siteMapKeyResolver.UrlKey;

    internal string GetNodeKeyPrefix() => this.siteMapKeyResolver.NodeKey;

    private string GetChildKeyPrefix() => this.siteMapKeyResolver.ChildKey;

    private string GetAddUrlKeyPrefix() => this.siteMapKeyResolver.AddUrlKey;

    private string GetDataKeyPrefix() => this.siteMapKeyResolver.DataKey;

    private string GetPersonalizedDataKeyPrefix() => this.siteMapKeyResolver.PersonalizedDataKey;

    private string GetSiteMapContextKey() => "SF_SiteMapContext_" + this.Name;

    private class PageSiteNodeWrapper
    {
      public PageSiteNode Node { get; set; }

      public bool HasTranslation { get; set; }
    }

    protected class AdditionalUrlProxy : ISiteMapAdditionalUrl
    {
      public string NodeKey { get; set; }

      public string Url { get; set; }

      public string Language { get; set; }

      public bool IsDefault { get; set; }
    }

    private class NodeFiltersCache
    {
      private DateTime nodeCacheCreationDate = DateTime.MinValue;
      private readonly Dictionary<string, bool> nodes = new Dictionary<string, bool>();

      public bool IsAccessible(SiteMapBase sitemap, SiteMapNode node, SitefinityIdentity user)
      {
        bool flag = false;
        if (node is ISitefinitySiteMapNode sitefinitySiteMapNode)
        {
          this.Validate(user);
          string key = SystemManager.CurrentContext.CurrentSite.Id.ToString() + "|" + sitefinitySiteMapNode.Id.ToString();
          if (!this.nodes.TryGetValue(key, out flag))
          {
            lock (this.nodes)
            {
              if (!this.nodes.TryGetValue(key, out flag))
              {
                flag = sitemap.IsAccessibleToUserInternal(node, user);
                this.nodes.Add(key, flag);
              }
            }
          }
        }
        return flag;
      }

      private void Validate(SitefinityIdentity user)
      {
        if (!(user.LastLoginDate > this.nodeCacheCreationDate))
          return;
        this.nodes.Clear();
        this.nodeCacheCreationDate = user.LastLoginDate;
      }
    }

    internal class SiteMapContext : IDisposable
    {
      private readonly string siteMapContextKey;

      public SiteMapContext(SiteMapBase siteMap, string transactionName)
      {
        this.siteMapContextKey = siteMap.GetSiteMapContextKey();
        IDictionary httpContextItems = SystemManager.HttpContextItems;
        if (httpContextItems == null)
          return;
        this.TransactionName = transactionName;
        httpContextItems[(object) this.siteMapContextKey] = (object) this;
      }

      public string TransactionName { get; private set; }

      public void Dispose() => SystemManager.HttpContextItems?.Remove((object) this.siteMapContextKey);
    }

    private interface ISiteMapKeyResolver
    {
      Guid RootNode { get; }

      string RootNodeName { get; }

      string RootNodeKey { get; }

      string NodeKey { get; }

      string UrlKey { get; }

      string ChildKey { get; }

      string AddUrlKey { get; }

      string DataKey { get; }

      string PersonalizedDataKey { get; }
    }

    private class StaticSiteMapKeyResolver : SiteMapBase.ISiteMapKeyResolver
    {
      private readonly string nodeKey;
      private readonly string urlKey;
      private readonly string childKey;
      private readonly string addUrlKey;
      private readonly string dataKey;
      private readonly string personalizedDataKey;
      private readonly Guid rootNode;
      private readonly string rootNodeKey;
      private readonly string rootNodeName;

      public StaticSiteMapKeyResolver(string rootName, Guid rootId)
      {
        this.rootNodeName = rootName;
        this.nodeKey = SiteMapBase.GetNodeKey(rootName);
        this.urlKey = SiteMapBase.GetUrlKey(rootName);
        this.childKey = SiteMapBase.GetChildKey(rootName);
        this.addUrlKey = SiteMapBase.GetAddUrlKey(rootName);
        this.dataKey = SiteMapBase.GetDataKey(rootName);
        this.rootNode = rootId;
        this.rootNodeKey = this.nodeKey + this.rootNode.ToString().ToUpperInvariant();
      }

      public Guid RootNode => this.rootNode;

      public string RootNodeName => this.rootNodeName;

      public string RootNodeKey => this.rootNodeKey;

      public string NodeKey => this.nodeKey;

      public string UrlKey => this.urlKey;

      public string ChildKey => this.childKey;

      public string AddUrlKey => this.addUrlKey;

      public string DataKey => this.dataKey;

      public string PersonalizedDataKey => this.personalizedDataKey;
    }

    private class DynamicSiteMapKeyResolver : SiteMapBase.ISiteMapKeyResolver
    {
      public Guid RootNode => SystemManager.CurrentContext.CurrentSite.SiteMapRootNodeId;

      public string RootNodeName => SystemManager.CurrentContext.CurrentSite.SiteMapName;

      public string RootNodeKey => SystemManager.CurrentContext.CurrentSite.SiteMapKey;

      public string NodeKey => SiteMapBase.GetNodeKey(this.RootNodeName);

      public string UrlKey => SiteMapBase.GetUrlKey(this.RootNodeName);

      public string ChildKey => SiteMapBase.GetChildKey(this.RootNodeName);

      public string AddUrlKey => SiteMapBase.GetAddUrlKey(this.RootNodeName);

      public string DataKey => SiteMapBase.GetDataKey(this.RootNodeName);

      public string PersonalizedDataKey => SiteMapBase.GetPersonalizedDataKey(this.RootNodeName);
    }

    protected class UrlNode
    {
      public UrlNode(PageSiteNode siteNode) => this.SiteNode = siteNode;

      public PageSiteNode SiteNode { get; private set; }

      /// <summary>
      /// Gets the cache dependencies of a page site node. This property should be modified when a page site node is added to cache.
      /// </summary>
      /// <value>The cache dependencies.</value>
      public virtual CompoundCacheDependency CacheDependency => this.SiteNode.CacheDependency;

      /// <summary>Gets or sets the cache key.</summary>
      /// <value>The cache key.</value>
      public string CacheKey
      {
        get => this.SiteNode.CacheKey;
        set => this.SiteNode.CacheKey = value;
      }

      /// <summary>
      /// Gets or sets a value indicating whether this instance is additional.
      /// </summary>
      /// <value>
      /// 	<c>true</c> if this instance is additional; otherwise, <c>false</c>.
      /// </value>
      public bool IsAdditional { get; set; }

      public string[] Parameters { get; set; }

      public CultureInfo AdditionalUrlCulture { get; set; }

      internal bool ExtensionMismatch { get; set; }
    }
  }
}
