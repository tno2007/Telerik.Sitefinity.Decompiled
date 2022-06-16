// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.PageSiteNode
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching;
using Telerik.Microsoft.Practices.Unity.Utility;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Descriptors;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.UrlLocalizationStrategies;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities;

namespace Telerik.Sitefinity.Web
{
  /// <summary>Represents a SiteMap node for pages.</summary>
  [SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1204:StaticElementsMustAppearBeforeInstanceElements", Justification = "Ignored so that the file can be included in StyleCop")]
  public class PageSiteNode : SiteMapNode, ISitefinitySiteMapNode, ICustomTypeDescriptor
  {
    private HashSet<CultureInfo> allowMultipleUrlsPerLang;
    private bool allowMultipleUrls;
    private readonly PageDataContext pageDataItemContext;
    private static string[] usedActions = new string[7]
    {
      "Modify",
      "EditContent",
      "Create",
      "View",
      "Unlock",
      "CreateChildControls",
      "ChangeOwner"
    };
    private const int MaxVersionKeyLength = 50;
    private bool requireSsl;
    private bool crawlable;
    private bool? isBackend;
    private bool? isAccessibleForEveryone;
    private ILstring urlName;
    private ILstring title;
    private ILstring extension;
    private ILstring redirectUrl;
    private string outputCacheProfile;
    private CultureInfo[] availableLanguages;
    private LocalizationStrategy localizationStrategy;
    private IList<string> publishedTranslations;
    private Dictionary<CultureInfo, string> urls;
    private Dictionary<CultureInfo, string> urlsWithoutExtension;
    private readonly object urlLockObject = new object();
    private readonly object customFieldsLock = new object();
    private string urlWithoutExtension;
    private string moduleName;
    private IDictionary<string, object> customFields;
    private Dictionary<string, List<Guid>> allowedPrincipals;
    private Dictionary<string, List<Guid>> deniedPrincipals;
    private static object attributesLock = new object();
    private const string RequireSslAttributeName = "RequireSsl";
    private IRelatedDataHolder relatedDataHolder;
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1311:StaticReadonlyFieldsMustBeginWithUpperCaseLetter", Justification = "Ignored so that the file can be included in StyleCop")]
    private static readonly ConcurrentProperty<PropertyDescriptorCollection> propertyCache = new ConcurrentProperty<PropertyDescriptorCollection>(new Func<PropertyDescriptorCollection>(PageSiteNode.BuildPropertyCache));

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.PageSiteNode" /> class.
    /// </summary>
    /// <param name="provider">The provider.</param>
    /// <param name="key">The key.</param>
    protected PageSiteNode(SiteMapProvider provider, string key)
      : base(provider, key)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.PageSiteNode" /> class.
    /// </summary>
    /// <param name="provider">The sitemap provider.</param>
    /// <param name="pageNode">The page node.</param>
    /// <param name="pageProviderName">The page provider name.</param>
    public PageSiteNode(SiteMapBase provider, PageNode pageNode, string pageProviderName)
      : this(provider, pageNode, (SiteMapNode) null, pageProviderName)
    {
    }

    internal PageSiteNode(
      SiteMapBase provider,
      PageNode pageNode,
      SiteMapNode parent,
      string pageProviderName)
      : base((SiteMapProvider) provider, PageSiteNode.GetKey(pageNode))
    {
      Guard.ArgumentNotNull((object) provider, nameof (provider));
      Guard.ArgumentNotNull((object) pageNode, nameof (pageNode));
      this.SubscribeCacheDependencies(pageNode, parent);
      this.PopulateStandartProperties(pageNode, pageProviderName);
      this.PopulateMultilingualProps(pageNode);
      this.PopulatePermissions(pageNode);
      if (!this.HasPageData())
        this.pageDataItemContext = new PageDataContext();
      Log.Write((object) string.Format("PageSiteNode with key {0} was created.", (object) pageNode.Id.ToString().ToUpperInvariant()), ConfigurationPolicy.TestTracing);
    }

    private bool HasPageData() => this.NodeType == NodeType.Standard || this.NodeType == NodeType.External;

    private void PopulatePermissions(PageNode pageNode)
    {
      this.allowedPrincipals = new Dictionary<string, List<Guid>>();
      this.deniedPrincipals = new Dictionary<string, List<Guid>>();
      foreach (string usedAction in PageSiteNode.usedActions)
      {
        this.allowedPrincipals[usedAction] = new List<Guid>();
        this.deniedPrincipals[usedAction] = new List<Guid>();
      }
      IQueryable<Permission> activePermissions = pageNode.GetActivePermissions();
      List<Guid> guidList1 = new List<Guid>(activePermissions.Count<Permission>());
      List<Guid> guidList2 = new List<Guid>();
      foreach (Permission permission in (IEnumerable<Permission>) activePermissions)
      {
        if (permission.SetName == "General")
        {
          if (permission.IsGranted("View"))
            goto label_9;
        }
        if (permission.SetName == "Pages")
        {
          if (!permission.IsGranted("View"))
            goto label_10;
        }
        else
          goto label_10;
label_9:
        guidList1.Add(permission.PrincipalId);
label_10:
        if (permission.SetName == "General")
        {
          if (permission.IsDenied("View"))
            goto label_14;
        }
        if (permission.SetName == "Pages")
        {
          if (!permission.IsDenied("View"))
            goto label_15;
        }
        else
          goto label_15;
label_14:
        guidList2.Add(permission.PrincipalId);
label_15:
        if (permission.SetName == "Pages")
        {
          foreach (string usedAction in PageSiteNode.usedActions)
          {
            if (permission.IsGranted(usedAction))
              this.AllowedPrincipals[usedAction].Add(permission.PrincipalId);
            else if (permission.IsDenied(usedAction))
              this.DeniedPrincipals[usedAction].Add(permission.PrincipalId);
          }
        }
      }
      this.Roles = (IList) guidList1;
      this.DeniedRoles = (IList<Guid>) guidList2;
    }

    private void PopulateMultilingualProps(PageNode pageNode)
    {
      IAppSettings appSettings = SystemManager.CurrentContext.AppSettings;
      this.IsMultilingual = pageNode.IsBackend ? ((IEnumerable<CultureInfo>) appSettings.DefinedBackendLanguages).Count<CultureInfo>() > 1 : appSettings.Multilingual;
      if (this.IsMultilingual)
      {
        this.availableLanguages = pageNode.AvailableCultures;
        this.allowMultipleUrlsPerLang = new HashSet<CultureInfo>(pageNode.Urls.Where<PageUrlData>((Func<PageUrlData, bool>) (u => !u.IsDefault)).Select<PageUrlData, int>((Func<PageUrlData, int>) (u => u.Culture)).Distinct<int>().Select<int, CultureInfo>((Func<int, CultureInfo>) (c => AppSettings.CurrentSettings.GetCultureByLcid(c))));
      }
      else
      {
        this.availableLanguages = new CultureInfo[0];
        this.allowMultipleUrls = pageNode.Urls.Where<PageUrlData>((Func<PageUrlData, bool>) (u => !u.IsDefault && u.Culture == CultureInfo.InvariantCulture.LCID)).Any<PageUrlData>();
      }
      base.Title = pageNode.Title[CultureInfo.InvariantCulture];
      if (pageNode.IsBackend)
      {
        this.title = (ILstring) new BackendLstringProxy(pageNode.Title.PersistedValue);
        this.urlName = (ILstring) new BackendUrlNameLstringProxy(pageNode.UrlName.PersistedValue);
        this.redirectUrl = (ILstring) new BackendLstringProxy(pageNode.RedirectUrl.PersistedValue);
        this.extension = (ILstring) new BackendLstringProxy(pageNode.Extension.PersistedValue);
      }
      else
      {
        this.title = (ILstring) new LstringProxy(pageNode.Title, this.IsMultilingual);
        this.urlName = (ILstring) new LstringProxy(pageNode.UrlName, this.IsMultilingual);
        this.redirectUrl = (ILstring) new LstringProxy(pageNode.RedirectUrl, this.IsMultilingual);
        this.extension = (ILstring) new LstringProxy(pageNode.Extension, this.IsMultilingual);
      }
    }

    private void PopulateStandartProperties(PageNode pageNode, string pageProviderName)
    {
      this.Id = pageNode.Id;
      this.Ordinal = pageNode.Ordinal;
      this.Priority = pageNode.Priority;
      this.ShowInNavigation = pageNode.ShowInNavigation;
      this.AllowParametersValidation = pageNode.AllowParametersValidation;
      this.RenderAsLink = pageNode.RenderAsLink;
      this.NodeType = pageNode.NodeType;
      this.PageProviderName = pageProviderName;
      this.LinkedNodeId = pageNode.LinkedNodeId;
      this.LinkedNodeProvider = pageNode.LinkedNodeProvider;
      this.AdditionalUrlsRedirectToDefault = pageNode.AdditionalUrlsRedirectToDefaultOne;
      this.EnableDefaultCanonicalUrl = pageNode.EnableDefaultCanonicalUrl;
      this.moduleName = pageNode.ModuleName;
      this.requireSsl = pageNode.RequireSsl;
      this.crawlable = pageNode.Crawlable;
      this.localizationStrategy = pageNode.LocalizationStrategy;
      if (pageNode.ParentId != Guid.Empty)
        this.ParentKey = pageNode.ParentId.ToString();
      this.RootKey = pageNode.RootNodeId;
      if (base.Attributes == null)
        base.Attributes = new NameValueCollection();
      foreach (KeyValuePair<string, string> attribute in (IEnumerable<KeyValuePair<string, string>>) pageNode.Attributes)
        base.Attributes[attribute.Key] = attribute.Value;
      if (pageNode.OpenNewWindow)
        base.Attributes["target"] = "_blank";
      this.DateCreated = pageNode.DateCreated;
      this.LastModified = pageNode.LastModified;
      this.Owner = pageNode.Owner;
    }

    /// <summary>Gets the pageId.</summary>
    /// <value>The pageId.</value>
    [CommonProperty]
    public virtual Guid Id { get; private set; }

    /// <summary>Gets the page data item.</summary>
    /// <value>The page data item.</value>
    internal PageDataProxy CurrentPageDataItem => this.PageDataItemContext.Current;

    private PageDataContext PageDataItemContext => this.pageDataItemContext != null ? this.pageDataItemContext : (this.Provider as SiteMapBase).FindDataContext(this);

    /// <summary>Gets the name of the page provider.</summary>
    /// <value>The name of the page provider.</value>
    public virtual string PageProviderName { get; private set; }

    /// <summary>
    /// Gets a value indicating whether this node should be displayed in navigational controls.
    /// </summary>
    /// <value><c>true</c> if show in navigation; otherwise, <c>false</c>.</value>
    public virtual bool ShowInNavigation { get; private set; }

    /// <summary>
    /// Gets a value indicating whether this <see cref="T:Telerik.Sitefinity.Web.PageSiteNode" /> is hidden (unpublished or not published yet).
    /// This property is obsolete. Please use the <see cref="P:Telerik.Sitefinity.Web.PageSiteNode.Visible" /> property instead!
    /// </summary>
    /// <value><c>true</c> if hidden; otherwise, <c>false</c>.</value>
    [Obsolete("This property is obsolete. Please use the Visible property instead!")]
    public bool Hidden => !this.Visible;

    /// <summary>
    /// Gets a value indicating whether the parameter validation is allowed
    /// </summary>
    public bool AllowParametersValidation { get; internal set; }

    /// <summary>
    /// Gets a value indicating whether multiple urls are allowed
    /// </summary>
    [Obsolete("If we don't allow non-default additional URLs, then we clear them.")]
    public bool AllowMultipleUrls => this.allowMultipleUrlsPerLang != null ? this.allowMultipleUrlsPerLang.Contains(SystemManager.CurrentContext.Culture) : this.allowMultipleUrls;

    /// <summary>
    /// Gets or sets the title of the <see cref="T:System.Web.SiteMapNode" /> object.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// A string that represents the title of the node. The default is <see cref="F:System.String.Empty" />.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    /// The node is read-only.
    /// </exception>
    [CommonProperty]
    public override string Title
    {
      get
      {
        string str;
        return this.title.TryGetValue(out str) ? str : base.Title;
      }
      set => throw new NotSupportedException();
    }

    internal ILstring GetTitle() => this.title;

    /// <summary>
    /// Gets or sets a description for the <see cref="T:System.Web.SiteMapNode" />.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// A string that represents a description of the node; otherwise, <see cref="F:System.String.Empty" />.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    /// The node is read-only.
    /// </exception>
    [CommonProperty]
    public override string Description
    {
      get => this.CurrentPageDataItem.Description;
      set => throw new NotSupportedException();
    }

    /// <summary>
    /// Gets or sets the URL of the page that the <see cref="T:System.Web.SiteMapNode" /> object represents.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// The URL of the page that the node represents. The default is <see cref="F:System.String.Empty" />.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    /// The node is read-only.
    /// </exception>
    public override string Url
    {
      get => this.GetUrl(false, true);
      set => base.Url = value;
    }

    /// <summary>Gets the URL without its extension</summary>
    public string UrlWithoutExtension => this.GetUrl(true, true);

    /// <summary>Gets the URL name of this node.</summary>
    /// <value>The URL name.</value>
    [CommonProperty]
    public virtual string UrlName
    {
      get
      {
        string urlName;
        this.urlName.TryGetValue(out urlName);
        return urlName;
      }
    }

    /// <summary>Gets the redirect URL.</summary>
    /// <value>The redirect URL.</value>
    public virtual string RedirectUrl
    {
      get
      {
        string redirectUrl;
        if (!this.redirectUrl.TryGetValue(out redirectUrl))
          this.redirectUrl.TryGetValue(out redirectUrl, CultureInfo.InvariantCulture);
        return redirectUrl;
      }
    }

    /// <summary>
    /// Gets a value indicating whether the additional URLs redirect to the default.
    /// </summary>
    /// <value>
    ///  <c>true</c> if additional URLs should redirect to the default; otherwise, <c>false</c>.
    /// </value>
    public virtual bool AdditionalUrlsRedirectToDefault { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the canonical URL will be set if no widget has set it and the system allows canonical URLs.
    /// </summary>
    /// <value>
    ///  If set to <c>true</c> a canonical URL will be added on the page.
    ///  If <c>false</c> - it won't be added.
    ///  If not set - the global settings will take place.
    /// </value>
    public virtual bool? EnableDefaultCanonicalUrl { get; private set; }

    /// <summary>Gets the parent id.</summary>
    /// <value>The parent id.</value>
    public virtual string ParentKey { get; private set; }

    internal Guid RootKey { get; private set; }

    /// <summary>
    /// Gets or sets the node to which this node will redirect.
    /// </summary>
    /// <value>The node to redirect to.</value>
    public virtual Guid LinkedNodeId { get; set; }

    /// <summary>Gets or sets the redirection page provider.</summary>
    /// <value>The redirection page provider.</value>
    public virtual string LinkedNodeProvider { get; set; }

    /// <summary>Gets or sets the ordinal number of the node.</summary>
    /// <value>The ordinal.</value>
    public virtual float Ordinal { get; set; }

    /// <summary>
    /// Gets a value indicating whether this page node
    /// should be part of the url generation.
    /// </summary>
    /// <value><c>True</c>c if the node will participate in the url; otherwise <c>false</c>. The default is <c>true</c>.</value>
    public virtual bool RenderAsLink { get; private set; }

    /// <summary>
    /// Gets the version.
    /// NOTE: Reading this property will cause page data to be retrieved from the database.
    /// </summary>
    /// <value>The version.</value>
    public virtual int Version => this.CurrentPageDataItem.Version;

    /// <summary>
    /// Gets the template name, used by the external renderer.
    /// NOTE: Reading this property will cause page data to be retrieved from the database.
    /// </summary>
    /// <value>The version.</value>
    internal string TemplateName => this.CurrentPageDataItem.TemplateName;

    /// <summary>
    /// Gets the current status of the page.
    /// NOTE: Reading this property will cause page data to be retrieved from the database.
    /// </summary>
    /// <value>The status.</value>
    public virtual ContentLifecycleStatus Status => this.CurrentPageDataItem.Status;

    /// <summary>
    /// Gets a value indicating whether this <see cref="T:Telerik.Sitefinity.Web.PageSiteNode" /> is crawlable.
    /// NOTE: Reading this property will cause page data to be retrieved from the database.
    /// </summary>
    /// <value><c>true</c> if crawlable; otherwise, <c>false</c>.</value>
    public virtual bool Crawlable => this.crawlable;

    /// <summary>Checks if the page has variations for given type key.</summary>
    /// <param name="key">The key</param>
    /// <returns>A value, indicating if the page has variations for given type key</returns>
    internal virtual bool IsPersonalized(string key = null)
    {
      IDictionary<string, List<string>> variationKeys = this.PageDataItemContext.VariationKeys;
      if (variationKeys == null)
        return false;
      string name = SystemManager.CurrentContext.Culture.Name;
      if (string.IsNullOrEmpty(key))
        return variationKeys.ContainsKey(name);
      return variationKeys.ContainsKey(name) && variationKeys[name].Contains(key);
    }

    internal IEnumerable<string> GetVariationTypes()
    {
      string name = SystemManager.CurrentContext.Culture.Name;
      return !this.PageDataItemContext.VariationKeys.ContainsKey(name) ? (IEnumerable<string>) new List<string>() : (IEnumerable<string>) this.PageDataItemContext.VariationKeys[name];
    }

    internal float Priority { get; private set; }

    /// <summary>
    /// Returns whether the page is published in the specified culture. If no culture is specified, the value
    /// for the current UI culture is returned.
    /// </summary>
    /// <param name="culture">The culture.</param>
    /// <returns>Whether the page is published in the specified culture</returns>
    public virtual bool IsPublished(CultureInfo culture = null)
    {
      IAppSettings appSettings = SystemManager.CurrentContext.AppSettings;
      bool flag = this.IsBackend ? ((IEnumerable<CultureInfo>) appSettings.DefinedBackendLanguages).Count<CultureInfo>() > 1 : appSettings.Multilingual;
      culture = culture.GetSitefinityCulture();
      if (this.HasPageData())
      {
        PageDataProxy pageData = this.PageDataItemContext.GetPageData(culture);
        if (flag && pageData.PublishedTranslations != null)
        {
          CultureInfo cultureInfo = this.IsBackend ? appSettings.DefaultBackendLanguage : appSettings.DefaultFrontendLanguage;
          return pageData.PublishedTranslations.Count == 0 && culture != null && culture.Equals((object) cultureInfo) ? pageData.Visible && pageData.Version > 0 : this.IsBackend && pageData.Visible && pageData.Version > 0 || pageData.PublishedTranslations.Contains(culture.GetLanguageKey());
        }
        return pageData.Visible && pageData.Version > 0;
      }
      return !flag || ((IEnumerable<CultureInfo>) this.AvailableLanguages).Contains<CultureInfo>(culture);
    }

    /// <summary>
    /// Determines whether the page is hidden for the specified culture. If no culture is specified, the value
    /// for the current UI culture is returned.
    /// </summary>
    /// <param name="culture">The culture.</param>
    /// <returns>Whether the page is hidden for the specified culture</returns>
    public virtual bool IsHidden(CultureInfo culture = null) => !RouteHelper.CheckSiteNodePublished(this) || !this.IsPublished(culture);

    /// <summary>
    /// Gets a value indicating whether this <see cref="T:Telerik.Sitefinity.Web.PageSiteNode" /> is visible.
    /// NOTE: Reading this property will cause page data to be retrieved from the database.
    /// </summary>
    /// <value><c>true</c> if visible; otherwise, <c>false</c>.</value>
    public virtual bool Visible => this.CurrentPageDataItem.Visible;

    /// <summary>
    /// Gets a collection of additional attributes beyond the strongly typed properties that are defined for the <see cref="T:System.Web.SiteMapNode" /> class.
    /// NOTE: Reading this property will cause page data to be retrieved from the database.
    /// </summary>
    /// <value></value>
    /// <returns>A <see cref="T:System.Collections.Specialized.NameValueCollection" /> of additional attributes for the <see cref="T:System.Web.SiteMapNode" /> beyond <see cref="P:System.Web.SiteMapNode.Title" />, <see cref="P:System.Web.SiteMapNode.Description" />, <see cref="P:System.Web.SiteMapNode.Url" />, and <see cref="P:System.Web.SiteMapNode.Roles" />; otherwise, null, if no attributes exist.</returns>
    /// <exception cref="T:System.InvalidOperationException">The node is read-only.</exception>
    public new virtual NameValueCollection Attributes
    {
      get
      {
        NameValueCollection attributes = this.CurrentPageDataItem.GetAttributes(base.Attributes);
        if (attributes["RequireSsl"] == null)
        {
          lock (PageSiteNode.attributesLock)
          {
            if (attributes["RequireSsl"] == null)
              attributes["RequireSsl"] = this.requireSsl.ToString();
          }
        }
        return attributes;
      }
      private set => base.Attributes = value;
    }

    NameValueCollection ISitefinitySiteMapNode.Attributes => this.Attributes;

    /// <summary>
    /// Gets a value indicating whether the page requires SSL.
    /// </summary>
    /// <value>The require SSL.</value>
    public bool RequireSsl => this.requireSsl;

    /// <summary>
    /// Gets a value indicating whether the page should include a script manager.
    /// </summary>
    /// <value>The include script manager.</value>
    public bool IncludeScriptManager => this.CurrentPageDataItem.IncludeScriptManager;

    /// <summary>Gets the content of the head tag.</summary>
    /// <value>The content of the head tag.</value>
    public string HeadTagContent => this.CurrentPageDataItem.HeadTagContent;

    /// <summary>
    /// Gets the available languages.
    /// NOTE: Reading this property will cause page data to be retrieved from the database.
    /// </summary>
    /// <value>The available languages.</value>
    public virtual CultureInfo[] AvailableLanguages => this.availableLanguages;

    /// <summary>Gets the page data id.</summary>
    /// <value>The page data id.</value>
    public virtual Guid PageId => this.CurrentPageDataItem.Id;

    /// <summary>
    /// Gets the theme of the page.
    /// NOTE: Reading this property will cause page data to be retrieved from the database.
    /// </summary>
    public virtual string Theme => this.CurrentPageDataItem.Themes != null ? PageHelper.GetThemeName(this.CurrentPageDataItem.Themes) : string.Empty;

    /// <summary>
    /// Gets the name of the output cache profile.
    /// NOTE: Reading this property will cause page data to be retrieved from the database.
    /// </summary>
    /// <value>The output cache profile name.</value>
    public virtual string OutputCacheProfile => this.CurrentPageDataItem.OutputCacheProfile;

    /// <summary>Gets the type of the page.</summary>
    /// <value>The type of the page.</value>
    public virtual NodeType NodeType { get; private set; }

    /// <summary>
    /// Gets a value indicating whether this node is a backend node.
    /// </summary>
    /// <value>
    ///  <c>true</c> if this node is a backend node; otherwise, <c>false</c>.
    /// </value>
    public virtual bool IsBackend
    {
      get
      {
        if (!this.isBackend.HasValue)
        {
          PageSiteNode rootNodeCore = (PageSiteNode) ((SiteMapBase) this.Provider).GetRootNodeCore(false);
          this.isBackend = rootNodeCore == null ? new bool?(false) : new bool?(rootNodeCore.Id == SiteInitializer.BackendRootNodeId);
        }
        return this.isBackend.Value;
      }
    }

    /// <summary>
    /// Gets a value indicating whether this node is a group page - does not have page date associated with it.
    /// Contains the translations in which the page is published. In monolingual is null.
    /// </summary>
    /// <value>The published translations of the item.</value>
    public virtual IList<string> PublishedTranslations => this.CurrentPageDataItem.PublishedTranslations;

    /// <summary>
    /// Gets a value indicating whether this node is a group page - does not have page date associated with it.
    /// </summary>
    /// <value>
    ///  <c>true</c> if this instance is group page; otherwise, <c>false</c>.
    /// </value>
    [Obsolete("Use NodeType property instead.")]
    public virtual bool IsGroupPage => this.NodeType == NodeType.Group;

    /// <summary>
    /// Gets or sets a collection of roles with denied access to this node that are associated with the <see cref="T:System.Web.SiteMapNode" />
    /// object, used during security trimming.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IList" /> of roles.
    /// </returns>
    public virtual IList<Guid> DeniedRoles { get; set; }

    /// <summary>
    /// Gets or sets the cache dependencies of a page site node. This property should be modified when a page site node is added to cache.
    /// </summary>
    /// <value>The cache dependencies.</value>
    public virtual CompoundCacheDependency CacheDependency { get; set; }

    /// <summary>Gets or sets the cache key.</summary>
    /// <value>The cache key.</value>
    public virtual string CacheKey { get; set; }

    /// <summary>Gets or sets the UI culture.</summary>
    /// <value>The UI culture.</value>
    public virtual string UiCulture
    {
      get => this.CurrentPageDataItem.Culture;
      set
      {
      }
    }

    /// <summary>Gets or sets the localization strategy.</summary>
    /// <value>The localization strategy.</value>
    public LocalizationStrategy LocalizationStrategy
    {
      get => this.localizationStrategy;
      set => this.localizationStrategy = value;
    }

    /// <summary>Gets or sets the page links ids.</summary>
    /// <value>The page links ids.</value>
    [Obsolete("There are no more linked nodes. It returns the only one navigation node id for the page.")]
    public virtual List<Guid> PageLinksIds
    {
      get => new List<Guid>() { this.Id };
      set
      {
      }
    }

    /// <summary>
    ///  Gets or sets the URL evaluation mode. Used by content view controls for paging and URLs for master/detail mode.
    /// </summary>
    /// <value>The URL evaluation mode.</value>
    public virtual UrlEvaluationMode UrlEvaluationMode
    {
      get => this.CurrentPageDataItem.UrlEvaluationMode;
      set
      {
      }
    }

    /// <summary>
    /// Gets an extension to be used with the current page, with the dot (".") - .aspx, .html, .php etc
    /// </summary>
    public virtual string Extension
    {
      get
      {
        string extension;
        this.extension.TryGetValue(out extension);
        return extension;
      }
    }

    /// <summary>Gets the related data holder object for current sitemap node.</summary>
    /// <value>The related data holder.</value>
    public IRelatedDataHolder RelatedDataHolder
    {
      get
      {
        if (this.relatedDataHolder == null)
          this.relatedDataHolder = (IRelatedDataHolder) new Telerik.Sitefinity.RelatedData.RelatedDataHolder()
          {
            ItemId = this.Id,
            FullTypeName = typeof (PageNode).FullName,
            ProviderName = ((SiteMapBase) this.Provider).PageProviderName,
            Status = ContentLifecycleStatus.Live
          };
        return this.relatedDataHolder;
      }
    }

    /// <summary>
    /// Gets the version key of the node. The version key is composed by the version of the page data
    /// and the hash codes and the versions of the page templates it uses.
    /// </summary>
    /// <value>The version.</value>
    internal virtual string VersionKey => this.CurrentPageDataItem.VersionKey;

    /// <summary>Gets the framework</summary>
    public PageTemplateFramework Framework => this.CurrentPageDataItem.TemplateFramework;

    internal string ModuleName => this.moduleName;

    internal string UnresolvedUrl => this.GetCachedUrl(false);

    internal virtual bool IsAccessibleForEveryone
    {
      get
      {
        if (!this.isAccessibleForEveryone.HasValue)
          this.isAccessibleForEveryone = new bool?(this.Roles.Cast<Guid>().Contains<Guid>(SecurityManager.EveryoneRole.Id) && !this.DeniedRoles.Any<Guid>());
        return this.isAccessibleForEveryone.Value;
      }
    }

    internal bool IsMultilingual { get; private set; }

    /// <summary>Gets the custom field value for this page.</summary>
    /// <param name="name">The name of the custom field.</param>
    /// <returns>The value of the custom field or null if there is no such field.</returns>
    public object GetCustomFieldValue(string name)
    {
      if (name.IsNullOrEmpty())
        return (object) null;
      this.LoadCustomFields();
      object customFieldValue;
      this.customFields.TryGetValue(name, out customFieldValue);
      switch (customFieldValue)
      {
        case PageSiteNode.DynamicValue _:
          return ((PageSiteNode.DynamicValue) customFieldValue).GetValue();
        case ILstring lstring:
          string str = (string) null;
          if (!lstring.TryGetValue(out str, SystemManager.CurrentContext.Culture))
            return (object) null;
          customFieldValue = (object) str;
          break;
      }
      return customFieldValue;
    }

    private PageNode GetPageNode()
    {
      PageManager pageManager = ((SiteMapBase) this.Provider).GetPageManager();
      using (new ElevatedModeRegion((IManager) pageManager))
      {
        using (new ReadUncommitedRegion((IManager) pageManager))
          return pageManager.GetPageNode(this.Id);
      }
    }

    private void LoadCustomFields()
    {
      if (this.customFields != null)
        return;
      lock (this.customFieldsLock)
      {
        if (this.customFields != null)
          return;
        Dictionary<string, object> dictionary = new Dictionary<string, object>();
        PageNode component = (PageNode) null;
        foreach (PropertyDescriptor customFieldProp in (IEnumerable<PropertyDescriptor>) PageNode.CustomFieldProps)
        {
          object obj;
          if (customFieldProp is RelatedDataPropertyDescriptor)
          {
            obj = (object) new PageSiteNode.DynamicValue((RelatedDataPropertyDescriptor) customFieldProp, this.RelatedDataHolder);
          }
          else
          {
            if (component == null)
              component = this.GetPageNode();
            obj = customFieldProp.GetValue((object) component);
          }
          Lstring source = obj as Lstring;
          if (source == (Lstring) null)
            dictionary.Add(customFieldProp.Name, obj);
          else
            dictionary.Add(customFieldProp.Name, (object) new LstringProxy(source));
        }
        this.customFields = (IDictionary<string, object>) dictionary;
      }
    }

    internal Dictionary<string, List<Guid>> AllowedPrincipals => this.allowedPrincipals;

    internal Dictionary<string, List<Guid>> DeniedPrincipals => this.deniedPrincipals;

    internal DateTime DateCreated { get; set; }

    internal DateTime LastModified { get; set; }

    internal Guid Owner { get; set; }

    /// <summary>Ensures the page data is loaded.</summary>
    [Obsolete("Not needed anymore, PageData is loaded on demand")]
    public virtual void EnsurePageDataLoaded()
    {
    }

    /// <summary>
    /// Adds additional cache dependencies at later phase when data for a page node are loaded.
    /// </summary>
    protected virtual void AddAdditionalCacheDependencies()
    {
    }

    /// <summary>
    /// Gets a collection of cached and changed dependent items which has subscribed dependencies that need to be invalidated.
    /// </summary>
    /// <returns>The collection of cached and changed dependent items.</returns>
    /// <value>A collection of  instances of type <see cref="T:Telerik.Sitefinity.Data.CacheDependencyKey" />.</value>
    public virtual IList<CacheDependencyKey> GetCacheDependencyObjects()
    {
      PageDataProxy currentPageDataItem = this.CurrentPageDataItem;
      if (currentPageDataItem.TemplatesIds == null)
        return (IList<CacheDependencyKey>) new List<CacheDependencyKey>();
      List<CacheDependencyKey> cacheDependencyNotifiedObjects = new List<CacheDependencyKey>();
      Type type = typeof (CacheDependencyPageTemplateObject);
      foreach (Guid templatesId in (IEnumerable<Guid>) currentPageDataItem.TemplatesIds)
        this.AddCachedItem(cacheDependencyNotifiedObjects, templatesId.ToString().ToUpperInvariant(), type);
      return (IList<CacheDependencyKey>) cacheDependencyNotifiedObjects;
    }

    /// <inheritdoc />
    public override bool Equals(object obj) => obj is PageSiteNode pageSiteNode && this.Key.Equals(pageSiteNode.Key);

    /// <inheritdoc />
    public override int GetHashCode() => this.Id.GetHashCode();

    /// <summary>
    /// Gets a value indicating whether the current node has any child nodes, including inaccessible ones.
    /// </summary>
    /// <value>The has any child nodes.</value>
    internal bool HasAnyChildNodes
    {
      get
      {
        if (!this.HasAnyChildNodesInternal.HasValue)
          this.HasAnyChildNodesInternal = new bool?(((SiteMapBase) this.Provider).HasAnyChildNodes(this));
        return this.HasAnyChildNodesInternal.Value;
      }
    }

    /// <summary>Gets or sets the has child nodes internal.</summary>
    /// <value>The has child nodes internal.</value>
    internal bool? HasAnyChildNodesInternal { get; set; }

    internal bool HasSecuredWidgets => this.CurrentPageDataItem != null && this.CurrentPageDataItem.HasSecuredWidgets;

    /// <summary>
    /// Creates a new node that is a copy of the current node.
    /// </summary>
    /// <returns>A new node that is a copy of the current node.</returns>
    public override SiteMapNode Clone()
    {
      PageSiteNode pageSiteNode = new PageSiteNode(this.Provider, this.Key);
      pageSiteNode.CopyFrom(this);
      return (SiteMapNode) pageSiteNode;
    }

    internal void CopyFrom(PageSiteNode source)
    {
      this.Id = source.Id;
      this.title = source.title;
      this.SetTitle(source.Title);
      this.urlName = source.urlName;
      this.redirectUrl = source.redirectUrl;
      if (source.Roles != null)
        this.Roles = (IList) new ArrayList((ICollection) source.Roles);
      if (source.DeniedRoles != null)
        this.DeniedRoles = (IList<Guid>) new List<Guid>((IEnumerable<Guid>) source.DeniedRoles);
      if (base.Attributes != null)
        this.Attributes = new NameValueCollection(base.Attributes);
      this.ResourceKey = source.ResourceKey;
      this.ShowInNavigation = source.ShowInNavigation;
      this.AllowParametersValidation = source.AllowParametersValidation;
      this.RenderAsLink = source.RenderAsLink;
      this.availableLanguages = source.availableLanguages;
      this.localizationStrategy = source.localizationStrategy;
      this.outputCacheProfile = source.outputCacheProfile;
      this.NodeType = source.NodeType;
      this.publishedTranslations = source.publishedTranslations;
      this.ParentKey = source.ParentKey;
      this.AdditionalUrlsRedirectToDefault = source.AdditionalUrlsRedirectToDefault;
      this.extension = source.extension;
      this.moduleName = source.moduleName;
      this.customFields = source.customFields;
      this.HasAnyChildNodesInternal = source.HasAnyChildNodesInternal;
      this.Ordinal = source.Ordinal;
      this.Priority = source.Priority;
    }

    /// <summary>Gets the terminal node for a redirecting node.</summary>
    /// <param name="ifAccessible">A flag, indicating if the node should be accessible</param>
    /// <returns>The terminal node for a redirecting node</returns>
    public virtual PageSiteNode GetTerminalNode(bool ifAccessible)
    {
      if (this.NodeType != NodeType.InnerRedirect)
        return (PageSiteNode) null;
      PageSiteNode pageSiteNode = this;
      while (pageSiteNode.NodeType == NodeType.InnerRedirect)
      {
        pageSiteNode = (PageSiteNode) ((SiteMapBase) pageSiteNode.Provider).FindSiteMapNodeFromKey(pageSiteNode.LinkedNodeId.ToString(), ifAccessible);
        if (pageSiteNode == null)
          break;
      }
      return pageSiteNode != this ? pageSiteNode : (PageSiteNode) null;
    }

    /// <summary>Gets the URL for the specified culture.</summary>
    /// <param name="culture">The culture.</param>
    /// <returns>The URL.</returns>
    public virtual string GetUrl(CultureInfo culture) => this.GetUrl(culture, false);

    /// <summary>Gets the URL for the specified culture.</summary>
    /// <param name="culture">The culture.</param>
    /// <param name="fallbackToAnyLanguage">A flag, indicating if a fallback should be used</param>
    /// <param name="withExtension">A flag, indicating if the extension should be attached</param>
    /// <returns>The URL for the specified culture</returns>
    [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Ignored so that the file can be included in StyleCop")]
    public virtual string GetUrl(
      CultureInfo culture,
      bool fallbackToAnyLanguage,
      bool withExtension)
    {
      if (culture == null)
        throw new ArgumentNullException(nameof (culture));
      SiteMapBase provider = (SiteMapBase) this.Provider;
      List<string> stringList = new List<string>();
      PageSiteNode childNode = this;
      Dictionary<CultureInfo, string> urlsCache = withExtension ? this.urls : this.urlsWithoutExtension;
      string url;
      if (urlsCache != null)
      {
        url = this.GetCachedUrl(culture, urlsCache);
        if (url != null)
          return url;
      }
      lock (this.urlLockObject)
      {
        if (urlsCache != null)
        {
          url = this.GetCachedUrl(culture, urlsCache);
          if (url != null)
            return url;
        }
        else
        {
          urlsCache = new Dictionary<CultureInfo, string>();
          if (withExtension)
            this.urls = urlsCache;
          else
            this.urlsWithoutExtension = urlsCache;
        }
        bool flag = false;
        for (; childNode != null; childNode = (PageSiteNode) provider.GetParentNode((SiteMapNode) childNode, false))
        {
          if (childNode.RenderAsLink || childNode == this && childNode.ParentNode != null)
          {
            string str;
            if (childNode.urlName == null || !childNode.urlName.TryGetValue(out str, culture))
              str = childNode.UrlName;
            if (string.IsNullOrEmpty(str) & fallbackToAnyLanguage)
              str = childNode.GetValueAnyLanguage(childNode.urlName);
            if (!string.IsNullOrEmpty(str))
            {
              stringList.Add(str);
              if (str.StartsWith("~"))
              {
                flag = true;
                break;
              }
            }
          }
        }
        if (!flag && stringList.Count > 0)
          stringList.Add("~");
        stringList.Reverse();
        url = !withExtension ? string.Join("/", stringList.ToArray()) : string.Join("/", stringList.ToArray()) + this.Extension;
        if (!this.urlName.IsMultilingual())
        {
          if (withExtension)
            base.Url = url;
          else
            this.urlWithoutExtension = url;
        }
        else
          urlsCache.Add(culture, url);
      }
      return url;
    }

    /// <summary>
    /// Gets the URL for the specified culture with the extension.
    /// </summary>
    /// <param name="culture">The culture.</param>
    /// <param name="fallbackToAnyLanguage">A flag indicating whether the fallback should be used.</param>
    /// <returns>The URL.</returns>
    public virtual string GetUrl(CultureInfo culture, bool fallbackToAnyLanguage) => this.GetUrl(culture, fallbackToAnyLanguage, true);

    /// <summary>
    /// Determines whether this node contains the specified URL name.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <returns>
    ///     <c>true</c> if [contains URL name] [the specified name]; otherwise, <c>false</c>.
    /// </returns>
    public virtual bool ContainsUrlName(string name)
    {
      if (this.urlName != null)
      {
        foreach (CultureInfo availableLanguage in this.urlName.GetAvailableLanguages())
        {
          string a;
          this.urlName.TryGetValue(out a, availableLanguage);
          if (string.Equals(a, name, StringComparison.CurrentCultureIgnoreCase))
            return true;
        }
      }
      return false;
    }

    /// <summary>Gets the parents only titles path.</summary>
    /// <param name="separator">The separator.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>The parents only titles path.</returns>
    internal string GetParentsOnlyTitlesPath(string separator = " > ", CultureInfo culture = null)
    {
      List<string> list = this.GetTitlesHierarchy(culture, false).Skip<string>(1).Reverse<string>().ToList<string>();
      return string.Join(separator, (IEnumerable<string>) list);
    }

    private void AddCachedItem(
      List<CacheDependencyKey> cacheDependencyNotifiedObjects,
      string key,
      Type type)
    {
      if (cacheDependencyNotifiedObjects.Any<CacheDependencyKey>((Func<CacheDependencyKey, bool>) (itm => itm.Key == key && itm.Type == type)))
        return;
      cacheDependencyNotifiedObjects.Add(new CacheDependencyKey()
      {
        Key = key,
        Type = type
      });
    }

    internal string GetUrl(bool withoutExtension, bool resolve)
    {
      string url = withoutExtension ? this.GetCachedUrlWithoutExtension(resolve) : this.GetCachedUrl(resolve);
      if (resolve)
        url = this.ResolveUrl(url);
      return url;
    }

    private string GetCachedUrl(bool resolve = true)
    {
      if (!string.IsNullOrEmpty(base.Url))
        return base.Url;
      string url = this.GetUrl(SystemManager.CurrentContext.Culture, true);
      return resolve && !this.IsBackend ? ObjectFactory.Resolve<UrlLocalizationService>().ResolveUrl(url) : url;
    }

    private string GetCachedUrlWithoutExtension(bool resolve = true)
    {
      if (!string.IsNullOrEmpty(this.urlWithoutExtension))
        return this.urlWithoutExtension;
      string url = this.GetUrl(SystemManager.CurrentContext.Culture, true, false);
      return resolve && !this.IsBackend ? ObjectFactory.Resolve<UrlLocalizationService>().ResolveUrl(url) : url;
    }

    private string ResolveUrl(string url)
    {
      string str = url;
      IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
      if (!this.IsBackend && multisiteContext != null)
        str = multisiteContext.ResolveUrl(url);
      return str;
    }

    private string GetCachedUrl(CultureInfo culture, Dictionary<CultureInfo, string> urlsCache)
    {
      string str;
      return urlsCache.TryGetValue(culture, out str) ? str : (string) null;
    }

    internal IEnumerable<string> GetTitlesHierarchy(
      CultureInfo culture,
      bool fallbackToAnyLanguage)
    {
      SiteMapBase provider = (SiteMapBase) this.Provider;
      PageSiteNode childNode = this;
      List<string> titlesHierarchy = new List<string>();
      string str = string.Empty;
      for (; childNode != null; childNode = (PageSiteNode) provider.GetParentNode((SiteMapNode) childNode, false))
      {
        if (childNode.RenderAsLink)
        {
          if (!childNode.title.IsMultilingual() || !childNode.title.TryGetValue(out str, culture))
            str = childNode.Title;
          if (string.IsNullOrEmpty(str) & fallbackToAnyLanguage)
            str = childNode.GetValueAnyLanguage(childNode.title);
          if (!string.IsNullOrEmpty(str))
            titlesHierarchy.Add(str);
        }
      }
      return (IEnumerable<string>) titlesHierarchy;
    }

    /// <summary>
    /// Gets a value. First tries to get it for the current thread UI language. If the value for this language is empty or
    /// does not exist, the values for other languages are checked in random order and the first non-empty is returned.
    /// </summary>
    /// <param name="sourceVal">The value to be searched.</param>
    /// <returns>The value.</returns>
    internal string GetValueAnyLanguage(ILstring sourceVal)
    {
      string valueAnyLanguage;
      if (!sourceVal.TryGetValue(out valueAnyLanguage, SystemManager.CurrentContext.Culture) && !sourceVal.TryGetValue(out valueAnyLanguage, SystemManager.CurrentContext.CurrentSite.DefaultCulture))
      {
        CultureInfo[] availableLanguages = sourceVal.GetAvailableLanguages();
        bool flag = ((IEnumerable<CultureInfo>) availableLanguages).Count<CultureInfo>() > 1;
        foreach (CultureInfo culture in availableLanguages)
        {
          if (!flag || !culture.Equals((object) CultureInfo.InvariantCulture))
          {
            sourceVal.TryGetValue(out valueAnyLanguage, culture);
            return valueAnyLanguage;
          }
        }
      }
      return valueAnyLanguage;
    }

    internal bool HasTranslation(CultureInfo culture)
    {
      if (!this.urlName.IsMultilingual() || this.IsBackend)
        return true;
      if (this.LocalizationStrategy == LocalizationStrategy.Split)
        return this.UiCulture == culture.Name;
      CultureInfo[] availableLanguages = this.urlName.GetAvailableLanguages();
      if (((IEnumerable<CultureInfo>) availableLanguages).Contains<CultureInfo>(culture))
        return true;
      return SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage.Equals((object) culture) && ((IEnumerable<CultureInfo>) availableLanguages).Contains<CultureInfo>(CultureInfo.InvariantCulture);
    }

    /// <summary>
    /// Determines whether the page is part of the specified module.
    /// </summary>
    /// <param name="moduleName">Name of the module.</param>
    /// <returns>
    ///  <c>true</c> if [is module page] [the specified module name]; otherwise, <c>false</c>.
    /// </returns>
    public bool IsModulePage(string moduleName) => moduleName != null && !this.ModuleName.IsNullOrEmpty() && moduleName.Equals(this.ModuleName);

    private static bool CopyAllLanguages(
      Lstring source,
      IDictionary<CultureInfo, string> destination)
    {
      if (source != (Lstring) null && destination != null)
      {
        CultureInfo[] availableLanguages = source.GetAvailableLanguages();
        if (availableLanguages.Length != 0)
        {
          foreach (CultureInfo cultureInfo in availableLanguages)
            destination.Add(cultureInfo, source[cultureInfo]);
          return true;
        }
      }
      return false;
    }

    private void SetTitle(string title)
    {
      IAppSettings appSettings = SystemManager.CurrentContext.AppSettings;
      if ((this.IsBackend ? (((IEnumerable<CultureInfo>) appSettings.DefinedBackendLanguages).Count<CultureInfo>() > 1 ? 1 : 0) : (appSettings.Multilingual ? 1 : 0)) != 0)
        return;
      base.Title = title;
    }

    internal static string GenerateVersionKey(PageData pageData, IList<PageTemplate> templates)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(pageData.Version);
      stringBuilder.Append(pageData.BuildStamp);
      foreach (PageTemplate template in (IEnumerable<PageTemplate>) templates)
      {
        stringBuilder.Append(template.Key);
        stringBuilder.Append(template.Version);
      }
      return stringBuilder.Length < 50 ? stringBuilder.ToString() : stringBuilder.ToString().GetHashCode().ToString();
    }

    internal string GetPersonalizedVersionKey(Guid? variationId)
    {
      if (variationId.HasValue)
      {
        PageDataProxy pageProxyVariation = (this.Provider as SiteMapBase).FindPageProxyVariation(this, variationId.Value);
        if (pageProxyVariation != null)
          return pageProxyVariation.VersionKey;
      }
      return this.VersionKey;
    }

    internal bool HasUrlName(string name, out bool matchCulture)
    {
      matchCulture = true;
      if (this.urlName != null)
      {
        string a;
        if (this.urlName.TryGetValue(out a) && string.Equals(a, name, StringComparison.CurrentCultureIgnoreCase))
          return true;
        matchCulture = false;
        foreach (CultureInfo availableLanguage in this.urlName.GetAvailableLanguages())
        {
          this.urlName.TryGetValue(out a, availableLanguage);
          if (string.Equals(a, name, StringComparison.CurrentCultureIgnoreCase))
            return true;
        }
      }
      return false;
    }

    private void SubscribeCacheDependencies(PageNode current, SiteMapNode siteMapNode)
    {
      this.CacheDependency = new CompoundCacheDependency();
      this.CacheDependency.CacheDependencies.Add((ICacheItemExpiration) new DataItemCacheDependency(current.GetType(), current.Id));
      Type nodeUrlCacheDependencyType = typeof (CacheDependencyPageNodeUrl);
      if (!(siteMapNode is PageSiteNode pageSiteNode))
      {
        for (PageNode parent = current.Parent; parent != null; parent = parent.Parent)
          this.CacheDependency.CacheDependencies.Add((ICacheItemExpiration) new DataItemCacheDependency(nodeUrlCacheDependencyType, parent.Id));
      }
      else
      {
        foreach (DataItemCacheDependency itemCacheDependency in pageSiteNode.CacheDependency.CacheDependencies.OfType<DataItemCacheDependency>().Where<DataItemCacheDependency>((Func<DataItemCacheDependency, bool>) (d => d.ItemType.Equals(nodeUrlCacheDependencyType))))
          this.CacheDependency.CacheDependencies.Add((ICacheItemExpiration) new DataItemCacheDependency(itemCacheDependency.ItemType, itemCacheDependency.ItemKey));
      }
      this.CacheDependency.CacheDependencies.Add((ICacheItemExpiration) new DataItemCacheDependency(nodeUrlCacheDependencyType, current.Id));
    }

    internal static string GetKey(PageNode node) => PageSiteNode.GetKey(node.Id);

    internal static string GetKey(Guid id) => id.ToString().ToUpperInvariant();

    /// <summary>Gets site node's attributes</summary>
    /// <returns>The attributes</returns>
    public AttributeCollection GetAttributes() => (AttributeCollection) null;

    /// <summary>Gets the class name</summary>
    /// <returns>The class name</returns>
    public string GetClassName() => this.GetType().Name;

    /// <summary>Gets the component name</summary>
    /// <returns>The component name</returns>
    public string GetComponentName() => (string) null;

    /// <summary>Gets the type converter</summary>
    /// <returns>The type converter</returns>
    public TypeConverter GetConverter() => (TypeConverter) null;

    /// <summary>Gets the default event descriptor</summary>
    /// <returns>The default event descriptor</returns>
    public EventDescriptor GetDefaultEvent() => (EventDescriptor) null;

    /// <summary>Gets the default property</summary>
    /// <returns>The default property</returns>
    public PropertyDescriptor GetDefaultProperty() => (PropertyDescriptor) null;

    /// <summary>Gets the editor</summary>
    /// <param name="editorBaseType">The base editor type</param>
    /// <returns>The editor</returns>
    public object GetEditor(Type editorBaseType) => (object) null;

    /// <summary>Gets the events</summary>
    /// <param name="attributes">The attributes</param>
    /// <returns>The events</returns>
    public EventDescriptorCollection GetEvents(Attribute[] attributes) => new EventDescriptorCollection((EventDescriptor[]) null);

    /// <summary>Gets the events</summary>
    /// <returns>The events</returns>
    public EventDescriptorCollection GetEvents() => new EventDescriptorCollection((EventDescriptor[]) null);

    /// <summary>Gets the properties</summary>
    /// <param name="attributes">The attributes</param>
    /// <returns>The properties</returns>
    public PropertyDescriptorCollection GetProperties(
      Attribute[] attributes)
    {
      return this.GetPropertyDescriptorCollection();
    }

    private PropertyDescriptorCollection GetPropertyDescriptorCollection() => PageSiteNode.PropertyCache;

    /// <summary>Gets the properties</summary>
    /// <returns>The properties</returns>
    public PropertyDescriptorCollection GetProperties() => this.GetPropertyDescriptorCollection();

    /// <summary>Gets the property owner</summary>
    /// <param name="propertyDescriptor">The property descriptor</param>
    /// <returns>The property owner</returns>
    public object GetPropertyOwner(PropertyDescriptor propertyDescriptor) => (object) this;

    internal static void ClearPropCache() => PageSiteNode.propertyCache.Reset();

    internal static PropertyDescriptorCollection PropertyCache => PageSiteNode.propertyCache.Value;

    private static PropertyDescriptorCollection BuildPropertyCache()
    {
      List<PropertyDescriptor> propertyDescriptorList = new List<PropertyDescriptor>(TypeDescriptor.GetProperties(typeof (PageSiteNode)).OfType<PropertyDescriptor>().AsEnumerable<PropertyDescriptor>());
      foreach (PropertyDescriptor customFieldProp in (IEnumerable<PropertyDescriptor>) PageNode.CustomFieldProps)
      {
        PageSiteNidePropertyDescriptor propertyDescriptor = new PageSiteNidePropertyDescriptor(customFieldProp.Name, customFieldProp.Attributes.OfType<Attribute>().ToArray<Attribute>());
        propertyDescriptorList.Add((PropertyDescriptor) propertyDescriptor);
      }
      return new PropertyDescriptorCollection(propertyDescriptorList.ToArray());
    }

    private class DynamicValue
    {
      private readonly IRelatedDataHolder relatedDataHolder;
      private readonly RelatedDataPropertyDescriptor prop;

      public DynamicValue(RelatedDataPropertyDescriptor prop, IRelatedDataHolder relatedDataHolder)
      {
        this.prop = prop;
        this.relatedDataHolder = relatedDataHolder;
      }

      public object GetValue() => this.prop.GetValue((object) this.relatedDataHolder);
    }
  }
}
