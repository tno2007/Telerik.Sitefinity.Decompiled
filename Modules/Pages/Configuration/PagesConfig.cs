// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Configuration.PagesConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Reflection;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.GenericContent.Configuration;
using Telerik.Sitefinity.Modules.Pages.Data;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Web.UI.Facet;
using Telerik.Sitefinity.Taxonomies.Web.UI.Network;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Pages.Configuration
{
  /// <summary>Defines Generic Content configuration settings.</summary>
  [DescriptionResource(typeof (ConfigDescriptions), "PagesConfig")]
  public class PagesConfig : ModuleConfigBase
  {
    private ISet<string> knownExtensions;
    internal string workflowPageId = SiteInitializer.WorkflowPageId.ToString();
    private ISet<string> ignoredExtensions;
    internal const string DefaultProviderName = "OpenAccessDataProvider";
    internal const int OneDayInSeconds = 86400;

    /// <summary>
    /// 
    /// </summary>
    /// <value></value>
    [ConfigurationProperty("defaultTheme", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DefaultThemeDescription", Title = "DefaultThemeTitle")]
    public virtual string DefaultTheme
    {
      get => (string) this["defaultTheme"];
      set => this["defaultTheme"] = (object) value;
    }

    /// <summary>Gets or sets the home page pageId.</summary>
    /// <value>The home page pageId.</value>
    [ConfigurationProperty("homePageId", DefaultValue = "00000000-0000-0000-0000-000000000000")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "HomePageIdDescription", Title = "HomePageIdTitle")]
    [Obsolete("Actual home page Id is persisted in the database")]
    public virtual Guid HomePageId
    {
      get => (Guid) this["homePageId"];
      set => this["homePageId"] = (object) value;
    }

    /// <summary>Gets or sets the id of the default frontend template.</summary>
    /// <value>The id of the template to set as default for frontend.</value>
    [ConfigurationProperty("defaultFrontendTemplateId", DefaultValue = "00000000-0000-0000-0000-000000000000")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DefaultFrontendTemplateIdDescription", Title = "DefaultFrontendTemplateIdTitle")]
    public virtual Guid DefaultFrontendTemplateId
    {
      get => (Guid) this["defaultFrontendTemplateId"];
      set => this["defaultFrontendTemplateId"] = (object) value;
    }

    /// <summary>Gets or sets the id of the default backend template.</summary>
    /// <value>The id of the template to set as default for backend.</value>
    [ConfigurationProperty("defaultBackendTemplateId", DefaultValue = "00000000-0000-0000-0000-000000000000")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DefaultBackendTemplateIdDescription", Title = "DefaultBackendTemplateIdTitle")]
    public virtual Guid DefaultBackendTemplateId
    {
      get
      {
        Guid backendTemplateId = (Guid) this["defaultBackendTemplateId"];
        if (backendTemplateId.Equals(Guid.Empty))
          backendTemplateId = SiteInitializer.DefaultBackendTemplateId;
        return backendTemplateId;
      }
      set => this["defaultBackendTemplateId"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the home page pageId for the backend area.
    /// </summary>
    /// <value>The home page pageId.</value>
    [ConfigurationProperty("backendHomePageId", DefaultValue = "F669D9A7-009D-4d83-AAAA-700000000001")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "BackendHomePageIdDescription", Title = "BackendHomePageIdTitle")]
    public virtual Guid BackendHomePageId
    {
      get => (Guid) this["backendHomePageId"];
      set => this["backendHomePageId"] = (object) value;
    }

    /// <summary>
    /// Defines the Taxonomy data provider that is used to store page structures. If this value is left empty the default Taxonomy provider will be used.
    /// </summary>
    /// <value>The page taxonomy provider.</value>
    [ConfigurationProperty("pageTaxonomyProvider", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "PageTaxonomyProviderDescription", Title = "PageTaxonomyProviderTitle")]
    public virtual string PageTaxonomyProvider
    {
      get => (string) this["pageTaxonomyProvider"];
      set => this["pageTaxonomyProvider"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the default taxonomy used for page navigation.
    /// The taxonomy has to be hierarchical type.
    /// </summary>
    /// <value>The page taxonomy.</value>
    [ConfigurationProperty("pageTaxonomyName", DefaultValue = "Pages")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "PageTaxonomyDescription", Title = "PageTaxonomyTitle")]
    public virtual string PageTaxonomyName
    {
      get => (string) this["pageTaxonomyName"];
      set => this["pageTaxonomyName"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the default taxonomy used for page templates categorization.
    /// The taxonomy has to be hierarchical type.
    /// </summary>
    /// <value>The page taxonomy.</value>
    [ConfigurationProperty("pageTemplatesTaxonomyName", DefaultValue = "PageTemplates")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "PageTemplatesTaxonomyDescription", Title = "PageTemplatesTaxonomyTitle")]
    public virtual string PageTemplatesTaxonomyName
    {
      get => (string) this["pageTemplatesTaxonomyName"];
      set => this["pageTemplatesTaxonomyName"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the taxon used as root for page navigation.
    /// </summary>
    /// <value>The root taxon name.</value>
    [ConfigurationProperty("frontendRootNode", DefaultValue = "FrontendSiteMap")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "RootNodeDescription", Title = "RootNodeTitle")]
    public virtual string FrontendRootNode
    {
      get => (string) this["frontendRootNode"];
      set => this["frontendRootNode"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the taxon used as root for page navigation.
    /// </summary>
    /// <value>The root taxon name.</value>
    [ConfigurationProperty("backendRootNode", DefaultValue = "BackendSiteMap")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "BackendRootNodeDescription", Title = "BackendRootNodeTitle")]
    public virtual string BackendRootNode
    {
      get => (string) this["backendRootNode"];
      set => this["backendRootNode"] = (object) value;
    }

    /// <summary>Gets or sets the max page site map nodes.</summary>
    /// <value>The max page site map nodes.</value>
    [ConfigurationProperty("maxPageSiteMapNodes", DefaultValue = 100)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "MaxPageSiteMapNodesDescription", Title = "MaxPageSiteMapNodesTitle")]
    public virtual int MaxPageSiteMapNodes
    {
      get => (int) this["maxPageSiteMapNodes"];
      set => this["maxPageSiteMapNodes"] = (object) value;
    }

    /// <summary>Gets or sets the page browser date format.</summary>
    /// <value>The page browser date format.</value>
    [ConfigurationProperty("pageBrowserDateFormat", DefaultValue = "m")]
    public virtual string PageBrowserDateFormat
    {
      get => (string) this["pageBrowserDateFormat"];
      set => this["pageBrowserDateFormat"] = (object) value;
    }

    /// <summary>Defines the default ScriptManager settings.</summary>
    /// <value>The script manager configuration element.</value>
    [ConfigurationProperty("scriptManager")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ScriptManagerElementDescription", Title = "ScriptManagerElementTitle")]
    public virtual ScriptManagerElement ScriptManager
    {
      get => (ScriptManagerElement) this["scriptManager"];
      set => this["scriptManager"] = (object) value;
    }

    /// <summary>Defines the default Compilation settings.</summary>
    /// <value>The compilation configuration element.</value>
    [ConfigurationProperty("Compilation")]
    [ObjectInfo(typeof (PageResources), Description = "CompilationElementDescription", Title = "CompilationElementTitle")]
    public virtual CompilationElement Compilation
    {
      get => (CompilationElement) this[nameof (Compilation)];
      set => this[nameof (Compilation)] = (object) value;
    }

    /// <summary>Gets a collection of predefined backend pages.</summary>
    /// <value>The backend pages.</value>
    [Browsable(false)]
    [ConfigurationProperty("backendPages")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "BackendPagesDescription", Title = "BackendPagesTitle")]
    public virtual ConfigElementDictionary<string, PageElement> BackendPages
    {
      get
      {
        ConfigElementDictionary<string, PageElement> pages = (ConfigElementDictionary<string, PageElement>) this["backendPages"];
        if (pages.Count == 0)
          this.LoadBackendPages(pages);
        return pages;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to combine script resources.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if script resources must be combined; otherwise, <c>false</c>.
    /// </value>
    [ConfigurationProperty("combineScripts", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CombineScriptsDescription", Title = "CombineScriptsCaption")]
    [Browsable(false)]
    [Obsolete("Use the new properties CombineScriptsFrontEnd and CombineScriptsBackEnd, to manage Sitefinity script combining")]
    public bool? CombineScripts
    {
      get => (bool?) this["combineScripts"];
      set => this["combineScripts"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to combine script resources in Frontend.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if script resources must be combined; otherwise, <c>false</c>.
    /// </value>
    [ConfigurationProperty("combineScriptsFrontEnd", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CombineScriptsDescriptionFrontEnd", Title = "CombineScriptsCaptionFrontEnd")]
    public bool? CombineScriptsFrontEnd
    {
      get => (bool?) this["combineScriptsFrontEnd"];
      set => this["combineScriptsFrontEnd"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to compress script resources in Frontend.
    /// </summary>
    /// <value>
    ///     There are 3 possible choices - Disabled (do not compress), Automatic (only compress if the same setting is not enabled in IIS), and Forced (always comrpess)
    /// </value>
    [ConfigurationProperty("compressScriptsFrontEnd", DefaultValue = OutputCompression.Disabled)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CompressScriptResourcesFrontEndDescription", Title = "CompressScriptResourcesFrontEndCaption")]
    public OutputCompression CompressScriptsFrontEnd
    {
      get => (OutputCompression) this["compressScriptsFrontEnd"];
      set => this["compressScriptsFrontEnd"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to combine script resources in Backend.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if script resources must be combined; otherwise, <c>false</c>.
    /// </value>
    [ConfigurationProperty("combineScriptsBackEnd", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CombineScriptsDescriptionBackEnd", Title = "CombineScriptsCaptionBackEnd")]
    public bool? CombineScriptsBackEnd
    {
      get => (bool?) this["combineScriptsBackEnd"];
      set => this["combineScriptsBackEnd"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating if ViewState will be enabled by default.
    /// </summary>
    /// <value>The value indicating if ViewState will be enabled.</value>
    [ConfigurationProperty("viewStateMode", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ViewStateModeDescription", Title = "ViewStateModeCaption")]
    public bool ViewStateMode
    {
      get => (bool) this["viewStateMode"];
      set => this["viewStateMode"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to combine style sheet resources.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if style sheet resources must be combined; otherwise, <c>false</c>.
    /// </value>
    [ConfigurationProperty("combineStyleSheets", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CombineStyleSheetsDescription", Title = "CombineStyleSheetsCaption")]
    public bool? CombineStyleSheets
    {
      get => (bool?) this["combineStyleSheets"];
      set => this["combineStyleSheets"] = (object) value;
    }

    [ConfigurationProperty("enableBrowseAndEdit", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EnableBrowseAndEditDescription", Title = "EnableBrowseAndEditCaption")]
    public bool? EnableBrowseAndEdit
    {
      get => (bool?) this["enableBrowseAndEdit"];
      set => this["enableBrowseAndEdit"] = (object) value;
    }

    [Obsolete("This property must not be used anymore. All widgets can have different properties per language.")]
    public bool? EnableWidgetTranslations
    {
      get => new bool?(true);
      set
      {
      }
    }

    /// <summary>Gets the base url for Microsoft Ajax CDN</summary>
    [ObjectInfo(typeof (ConfigDescriptions), Description = "MicrosoftAjaxCdnBaseUrlDescription", Title = "MicrosoftAjaxCdnBaseUrlTitle")]
    [ConfigurationProperty("microsoftAjaxCdnBaseUrl", DefaultValue = "https://ajax.microsoft.com/ajax/beta/0910/")]
    public virtual string MicrosoftAjaxCdnBaseUrl
    {
      get => (string) this["microsoftAjaxCdnBaseUrl"];
      set => this["microsoftAjaxCdnBaseUrl"] = (object) value;
    }

    /// <summary>Globally turn on or off sitemap filtering</summary>
    [ObjectInfo(typeof (ConfigDescriptions), Description = "PagesIsSitemapNodeFilteringEnabledDescription", Title = "PagesIsSitemapNodeFilteringEnabledTitle")]
    [ConfigurationProperty("isSitemapNodeFilteringEnabled", DefaultValue = true)]
    [RestartAppOnChange]
    public bool IsSitemapNodeFilteringEnabled
    {
      get => this["isSitemapNodeFilteringEnabled"] as bool? ?? true;
      set => this["isSitemapNodeFilteringEnabled"] = (object) value;
    }

    /// <summary>
    /// Gets or sets whether to redirect or rewrite to the first accessible node(page) inside a group page. If true - redirect if false -rewrite.
    /// </summary>
    [ObjectInfo(typeof (ConfigDescriptions), Description = "IsToRedirectFromGroupPageDescription", Title = "IsToRedirectFromGroupPageTitle")]
    [ConfigurationProperty("isToRedirectFromGroupPage", DefaultValue = false)]
    public bool IsToRedirectFromGroupPage
    {
      get => (bool) this["isToRedirectFromGroupPage"];
      set => this["isToRedirectFromGroupPage"] = (object) value;
    }

    /// <summary>
    /// Configure individual sitemap filters (e.g. turn them off and on)
    /// </summary>
    [ConfigurationProperty("sitemapNodeFilters")]
    [ConfigurationCollection(typeof (Toolbox), AddItemName = "filter")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "PagesSitemapNodeFiltersDescription", Title = "PagesSitemapNodeFiltersTitle")]
    [RestartAppOnChange]
    public ConfigElementDictionary<string, SitemapNodeFilterElement> SitemapNodeFilters => (ConfigElementDictionary<string, SitemapNodeFilterElement>) this["sitemapNodeFilters"];

    /// <summary>
    /// Gets or sets the not allowed page extensions - those will be filtered directly by the route without any processing
    /// </summary>
    [ConfigurationProperty("notAllowedPageExtensions")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "NotAllowedPageExtensionsDescription", Title = "NotAllowedPageExtensionsTitle")]
    public virtual string NotAllowedPageExtensions
    {
      get => (string) this["notAllowedPageExtensions"];
      set => this["notAllowedPageExtensions"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the old algorithm for ordering controls is used.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if the old algorithm for ordering controls is used; otherwise, <c>false</c>.
    /// </value>
    [ConfigurationProperty("useOldControlsOrderAlgorithm", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "UseOldControlsOrderAlgorithmDescription", Title = "UseOldControlsOrderAlgorithmCaption")]
    public bool UseOldControlsOrderAlgorithm
    {
      get => (bool) this["useOldControlsOrderAlgorithm"];
      set => this["useOldControlsOrderAlgorithm"] = (object) value;
    }

    [Obsolete]
    [ConfigurationProperty("enableBackwardCompatabilityForPagesUrls", DefaultValue = false)]
    public bool EnableBackwardCompatabilityForPagesUrls
    {
      get => (bool) this["enableBackwardCompatabilityForPagesUrls"];
      set => this["enableBackwardCompatabilityForPagesUrls"] = (object) value;
    }

    /// <summary>Gets or sets the known page extensions</summary>
    [ConfigurationProperty("knownPageExtensions")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "KnownPageExtensionsDescription", Title = "KnownPageExtensionsTitle")]
    public virtual string KnownPageExtensions
    {
      get => (string) this["knownPageExtensions"];
      set => this["knownPageExtensions"] = (object) value;
    }

    /// <summary>
    /// Gets or sets if the split pages could have different page order for the different languages.
    /// </summary>
    [ConfigurationProperty("allowPageOrderingPerLanguage")]
    [Browsable(false)]
    public virtual bool AllowPageOrderingPerLanguage
    {
      get => (bool) this["allowPageOrderingPerLanguage"];
      set => this["allowPageOrderingPerLanguage"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether a url extension will be used.
    /// </summary>
    [ConfigurationProperty("defaultExtension", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DefaultExtensionDescription", Title = "DefaultExtension")]
    public virtual string DefaultExtension
    {
      get => (string) this["defaultExtension"];
      set => this["defaultExtension"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether a url extension will be used.
    /// </summary>
    [ConfigurationProperty("DisableInvalidateOutputCacheOnPermissionsChange", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DisableInvalidateOutputCacheOnPermissionsChangeDescription", Title = "DisableInvalidateOutputCacheOnPermissionsChange")]
    public virtual bool DisableInvalidateOutputCacheOnPermissionsChange
    {
      get => (bool) this[nameof (DisableInvalidateOutputCacheOnPermissionsChange)];
      set => this[nameof (DisableInvalidateOutputCacheOnPermissionsChange)] = (object) value;
    }

    /// <summary>Gets or sets the open home page as site root.</summary>
    /// <value>The open home page as site root.</value>
    [ConfigurationProperty("openHomePageAsSiteRoot", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "OpenHomePageAsSiteRootDescription", Title = "OpenHomePageAsSiteRootTitle")]
    public virtual bool OpenHomePageAsSiteRoot
    {
      get => (bool) this["openHomePageAsSiteRoot"];
      set => this["openHomePageAsSiteRoot"] = (object) value;
    }

    /// <summary>Gets or sets if link sharing of pages is enabled.</summary>
    /// <value>True if is enabled, false if is disabled.</value>
    [ConfigurationProperty("enableLinkSharing", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EnableLinkSharingDescription", Title = "EnableLinkSharingTitle")]
    public virtual bool EnableLinkSharing
    {
      get => (bool) this["enableLinkSharing"];
      set => this["enableLinkSharing"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the time in hours after which shared link is valid.
    /// </summary>
    /// <value>Time in hours.</value>
    [ConfigurationProperty("sharedLinkExpirationTime", DefaultValue = 24.0)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "SharedLinkExpirationTimeDescription", Title = "SharedLinkExpirationTimeTitle")]
    public virtual double SharedLinkExpirationTime
    {
      get => (double) this["sharedLinkExpirationTime"];
      set => this["sharedLinkExpirationTime"] = value > 0.0 && value <= 2400.0 ? (object) value : throw new ConfigurationErrorsException(Res.Get<PageResources>().SharedLinkExpirationTimeOutOfRangeMessage);
    }

    /// <summary>Gets or sets the time for redirect cache to expired</summary>
    /// <value>Time in seconds.</value>
    [ConfigurationProperty("redirectCacheExpired", DefaultValue = 86400)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "RedirectCacheExpiredDescription", Title = "RedirectCacheExpiredTitle")]
    public virtual int RedirectCacheExpired
    {
      get => (int) this["redirectCacheExpired"];
      set => this["redirectCacheExpired"] = (object) value;
    }

    /// <summary>
    /// Gets or sets whether the available templates framework for page templates
    /// </summary>
    /// <value>Tempalte frameworks availability.</value>
    [ConfigurationProperty("pageTemplatesFrameworks", DefaultValue = PageTemplatesAvailability.HybridAndMvc)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "PageTemplatesFrameworksDescription", Title = "PageTemplatesFrameworksTitle")]
    public virtual PageTemplatesAvailability PageTemplatesFrameworks
    {
      get => (PageTemplatesAvailability) this["pageTemplatesFrameworks"];
      set => this["pageTemplatesFrameworks"] = (object) value;
    }

    /// <summary>
    /// Gets or sets whether the created templates can change their framework based on a parent template with different framework.
    /// </summary>
    /// <value>True if is enabled, false if is disabled.</value>
    [ConfigurationProperty("аllowPageTemplatesFrameworkChange", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "AllowPageTemplatesFrameworkChangeDescription", Title = "AllowPageTemplatesFrameworkChangeTitle")]
    public virtual bool AllowPageTemplatesFrameworkChange
    {
      get => (bool) this["аllowPageTemplatesFrameworkChange"];
      set => this["аllowPageTemplatesFrameworkChange"] = (object) value;
    }

    /// <summary>
    /// Gets or sets whether the created templates can change their framework based on a parent template with different framework.
    /// </summary>
    /// <value>True if is enabled, false if is disabled.</value>
    [ConfigurationProperty("allowChangePageThemeAtRuntime", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "AllowChangePageThemeAtRuntimeDescription", Title = "AllowChangePageThemeAtRuntimeTitle")]
    public virtual bool AllowChangePageThemeAtRuntime
    {
      get => (bool) this["allowChangePageThemeAtRuntime"];
      set => this["allowChangePageThemeAtRuntime"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the group of users the page output cache will be ignored for.
    /// </summary>
    [ConfigurationProperty("ignoreOutputCacheUsers", DefaultValue = KnownUserGroups.Administrators)]
    [ObjectInfo(typeof (ConfigDescriptions), Title = "IgnoreOutputCacheUserGroupTitle")]
    public virtual KnownUserGroups IgnoreOutputCacheUserGroup
    {
      get => (KnownUserGroups) this["ignoreOutputCacheUsers"];
      set => this["ignoreOutputCacheUsers"] = (object) value;
    }

    /// <summary>
    /// Gets or sets whether the new Admin App widget designers to be displayed instead of the classic webforms and mvc onces.
    /// </summary>
    /// <value>True if is enabled, False if is disabled.</value>
    [ConfigurationProperty("enableAdminAppWidgetEditors", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EnableAdminAppWidgetEditorsDescription", Title = "EnableAdminAppWidgetEditorsTitle")]
    public virtual bool EnableAdminAppWidgetEditors
    {
      get => (bool) this["enableAdminAppWidgetEditors"];
      set => this["enableAdminAppWidgetEditors"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a comma separated list of widgets that will be opend when the new AdminApp editor is enabled.
    /// </summary>
    [ConfigurationProperty("whitelistedAdminAppWidgetEditors", DefaultValue = null)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "WhitelistedAdminAppWidgetEditorsDescription", Title = "WhitelistedAdminAppWidgetEditorsTitle")]
    public string WhitelistedAdminAppWidgetEditors
    {
      get => (string) this["whitelistedAdminAppWidgetEditors"];
      set => this["whitelistedAdminAppWidgetEditors"] = (object) value;
    }

    /// <summary>Gets a collection of data provider settings.</summary>
    [DescriptionResource(typeof (ConfigDescriptions), "Providers")]
    [ConfigurationProperty("providers")]
    public new virtual ConfigElementDictionary<string, DataProviderSettings> Providers => (ConfigElementDictionary<string, DataProviderSettings>) this["providers"];

    /// <summary>Gets or sets the default ErrorPages settings.</summary>
    /// <value>The error pages configuration element.</value>
    [ConfigurationProperty("errorPages")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ErrorPagesElementDescription", Title = "ErrorPagesElementTitle")]
    public virtual ErrorPageElement ErrorPages
    {
      get => (ErrorPageElement) this["errorPages"];
      set => this["errorPages"] = (object) value;
    }

    /// <summary>
    /// Called when the corresponding XML element is read and properties loaded.
    /// </summary>
    protected override void OnPropertiesInitialized()
    {
      base.OnPropertiesInitialized();
      SitemapNodeFilterElement element = new SitemapNodeFilterElement((ConfigElement) this.SitemapNodeFilters)
      {
        FilterName = "Admin",
        IsEnabled = true
      };
      element.Parameters.Add(new SitemapNodeFilterParameterElement((ConfigElement) element.Parameters)
      {
        ParameterName = "IsDesignSectionFilteringEnabled",
        ParamterValue = "false"
      });
      element.Parameters.Add(new SitemapNodeFilterParameterElement((ConfigElement) element.Parameters)
      {
        ParameterName = "IsFrontendPageManagementFilteringEnabled",
        ParamterValue = "true"
      });
      element.Parameters.Add(new SitemapNodeFilterParameterElement((ConfigElement) element.Parameters)
      {
        ParameterName = "IsSystemSectionFilteringEnabled",
        ParamterValue = "true"
      });
      element.Parameters.Add(new SitemapNodeFilterParameterElement((ConfigElement) element.Parameters)
      {
        ParameterName = "IsTaxonomySectionFilteringEnabled",
        ParamterValue = "true"
      });
      element.Parameters.Add(new SitemapNodeFilterParameterElement((ConfigElement) element.Parameters)
      {
        ParameterName = "IsUserManagementSectionFilteringEnabled",
        ParamterValue = "true"
      });
      element.Parameters.Add(new SitemapNodeFilterParameterElement((ConfigElement) element.Parameters)
      {
        ParameterName = "IsSettingsAndConfigurationFilteringEnabled",
        ParamterValue = "true"
      });
      this.SitemapNodeFilters.Add(element);
      this.SitemapNodeFilters.Add(new SitemapNodeFilterElement((ConfigElement) this.SitemapNodeFilters)
      {
        FilterName = "Blogs",
        IsEnabled = true
      });
      this.SitemapNodeFilters.Add(new SitemapNodeFilterElement((ConfigElement) this.SitemapNodeFilters)
      {
        FilterName = "News",
        IsEnabled = true
      });
      this.SitemapNodeFilters.Add(new SitemapNodeFilterElement((ConfigElement) this.SitemapNodeFilters)
      {
        FilterName = "Events",
        IsEnabled = true
      });
      this.SitemapNodeFilters.Add(new SitemapNodeFilterElement((ConfigElement) this.SitemapNodeFilters)
      {
        FilterName = "Libraries",
        IsEnabled = true
      });
      this.SitemapNodeFilters.Add(new SitemapNodeFilterElement((ConfigElement) this.SitemapNodeFilters)
      {
        FilterName = "Forms",
        IsEnabled = true
      });
      this.SitemapNodeFilters.Add(new SitemapNodeFilterElement((ConfigElement) this.SitemapNodeFilters)
      {
        FilterName = "Lists",
        IsEnabled = true
      });
      this.SitemapNodeFilters.Add(new SitemapNodeFilterElement((ConfigElement) this.SitemapNodeFilters)
      {
        FilterName = "Newsletters",
        IsEnabled = true
      });
      this.SitemapNodeFilters.Add(new SitemapNodeFilterElement((ConfigElement) this.SitemapNodeFilters)
      {
        FilterName = "GenericContent",
        IsEnabled = true
      });
      this.SitemapNodeFilters.Add(new SitemapNodeFilterElement((ConfigElement) this.SitemapNodeFilters)
      {
        FilterName = "ControlTemplates",
        IsEnabled = true
      });
      this.SitemapNodeFilters.Add(new SitemapNodeFilterElement((ConfigElement) this.SitemapNodeFilters)
      {
        FilterName = "Search",
        IsEnabled = true
      });
      this.SitemapNodeFilters.Add(new SitemapNodeFilterElement((ConfigElement) this.SitemapNodeFilters)
      {
        FilterName = "Publishing",
        IsEnabled = true
      });
      this.SitemapNodeFilters.Add(new SitemapNodeFilterElement((ConfigElement) this.SitemapNodeFilters)
      {
        FilterName = "MultisiteInternal",
        IsEnabled = true
      });
      string name = Config.Get<ControlsConfig>().ResourcesAssemblyInfo.Assembly.GetName().Name;
      string str = name;
      ConfigElementDictionary<string, ScriptReferenceElement> scriptReferences = this.ScriptManager.ScriptReferences;
      scriptReferences.Add(new ScriptReferenceElement((ConfigElement) scriptReferences)
      {
        Key = "JQuery",
        Name = "Telerik.Sitefinity.Resources.Scripts.jquery-3.5.1.min.js",
        Path = "https://code.jquery.com/jquery-3.5.1.min.js",
        DebugPath = "https://code.jquery.com/jquery-3.5.1.js",
        IgnoreScriptPath = false,
        Assembly = name,
        Combine = false
      });
      scriptReferences.Add(new ScriptReferenceElement((ConfigElement) scriptReferences)
      {
        Key = "JQueryMigrate",
        Name = "Telerik.Sitefinity.Resources.Scripts.jquery-migrate-1.2.1.min.js",
        Path = "https://code.jquery.com/jquery-migrate-1.2.1.min.js",
        DebugPath = "https://code.jquery.com/jquery-migrate-1.2.1.js",
        IgnoreScriptPath = true,
        Assembly = name
      });
      scriptReferences.Add(new ScriptReferenceElement((ConfigElement) scriptReferences)
      {
        Key = "JQueryValidate",
        Name = "Telerik.Sitefinity.Resources.Scripts.jquery.validate.min.js",
        Path = "https://ajax.microsoft.com/ajax/jQuery.Validate/1.8.1/jQuery.Validate.min.js",
        DebugPath = "https://ajax.microsoft.com/ajax/jQuery.Validate/1.8.1/jQuery.Validate.js",
        IgnoreScriptPath = true,
        Assembly = name
      });
      scriptReferences.Add(new ScriptReferenceElement((ConfigElement) scriptReferences)
      {
        Key = "JQueryUI",
        Name = "Telerik.Sitefinity.Resources.Scripts.jquery-ui-1.12.1.custom.min.js",
        Path = "",
        DebugPath = "",
        IgnoreScriptPath = true,
        Assembly = name
      });
      scriptReferences.Add(new ScriptReferenceElement((ConfigElement) scriptReferences)
      {
        Key = "JQueryFancyBox",
        Name = "Telerik.Sitefinity.Resources.Scripts.jquery.fancybox.js",
        Path = "",
        DebugPath = "",
        IgnoreScriptPath = true,
        Assembly = name
      });
      scriptReferences.Add(new ScriptReferenceElement((ConfigElement) scriptReferences)
      {
        Key = "JQueryCookie",
        Name = "Telerik.Sitefinity.Resources.Scripts.jquery.cookie.js",
        Path = "",
        DebugPath = "",
        IgnoreScriptPath = true,
        Assembly = name
      });
      scriptReferences.Add(new ScriptReferenceElement((ConfigElement) scriptReferences)
      {
        Key = "JQueryGalleria",
        Name = "Telerik.Sitefinity.Resources.Scripts.galleria.js",
        Path = "",
        DebugPath = "",
        IgnoreScriptPath = true,
        Assembly = name
      });
      scriptReferences.Add(new ScriptReferenceElement((ConfigElement) scriptReferences)
      {
        Key = "JQueryGalleriaHistory",
        Name = "Telerik.Sitefinity.Resources.Scripts.galleria.history.js",
        Path = "",
        DebugPath = "",
        IgnoreScriptPath = true,
        Assembly = name
      });
      scriptReferences.Add(new ScriptReferenceElement((ConfigElement) scriptReferences)
      {
        Key = "JQueryGalleriaClassicTemplate",
        Name = "Telerik.Sitefinity.Resources.Scripts.galleria.classic.js",
        Path = "",
        DebugPath = "",
        IgnoreScriptPath = true,
        Assembly = name
      });
      scriptReferences.Add(new ScriptReferenceElement((ConfigElement) scriptReferences)
      {
        Key = "MicrosoftAjax",
        Name = "Telerik.Sitefinity.Resources.Scripts.MicrosoftAjax.js",
        DebugName = "Telerik.Sitefinity.Resources.Scripts.MicrosoftAjax.debug.js",
        Path = Url.Combine(this.MicrosoftAjaxCdnBaseUrl, "MicrosoftAjax.js"),
        DebugPath = Url.Combine(this.MicrosoftAjaxCdnBaseUrl, "MicrosoftAjax.js"),
        IgnoreScriptPath = false,
        Assembly = str,
        Combine = false,
        OutputPosition = ScriptReferenceOutputPosition.Beginning
      });
      scriptReferences.Add(new ScriptReferenceElement((ConfigElement) scriptReferences)
      {
        Key = "MicrosoftAjaxAdoNet",
        Name = "Telerik.Sitefinity.Resources.Scripts.MicrosoftAjaxAdoNet.js",
        Path = Url.Combine(this.MicrosoftAjaxCdnBaseUrl, "MicrosoftAjaxAdoNet.js"),
        DebugPath = Url.Combine(this.MicrosoftAjaxCdnBaseUrl, "MicrosoftAjaxAdoNet.debug.js"),
        IgnoreScriptPath = false,
        Assembly = name
      });
      scriptReferences.Add(new ScriptReferenceElement((ConfigElement) scriptReferences)
      {
        Key = "MicrosoftAjaxApplicationServices",
        Name = "Telerik.Sitefinity.Resources.Scripts.MicrosoftAjaxApplicationServices.js",
        Path = Url.Combine(this.MicrosoftAjaxCdnBaseUrl, "MicrosoftAjaxApplicationServices.js"),
        DebugPath = Url.Combine(this.MicrosoftAjaxCdnBaseUrl, "MicrosoftAjaxApplicationServices.debug.js"),
        IgnoreScriptPath = false,
        Assembly = str
      });
      scriptReferences.Add(new ScriptReferenceElement((ConfigElement) scriptReferences)
      {
        Key = "MicrosoftAjaxComponentModel",
        Name = "Telerik.Sitefinity.Resources.Scripts.MicrosoftAjaxComponentModel.js",
        Path = Url.Combine(this.MicrosoftAjaxCdnBaseUrl, "MicrosoftAjaxComponentModel.js"),
        DebugPath = Url.Combine(this.MicrosoftAjaxCdnBaseUrl, "MicrosoftAjaxComponentModel.debug.js"),
        IgnoreScriptPath = false,
        Assembly = str
      });
      scriptReferences.Add(new ScriptReferenceElement((ConfigElement) scriptReferences)
      {
        Key = "MicrosoftAjaxCore",
        Name = "Telerik.Sitefinity.Resources.Scripts.MicrosoftAjaxCore.js",
        Path = Url.Combine(this.MicrosoftAjaxCdnBaseUrl, "MicrosoftAjaxCore.js"),
        DebugPath = Url.Combine(this.MicrosoftAjaxCdnBaseUrl, "MicrosoftAjaxCore.debug.js"),
        IgnoreScriptPath = false,
        Assembly = str
      });
      scriptReferences.Add(new ScriptReferenceElement((ConfigElement) scriptReferences)
      {
        Key = "MicrosoftAjaxDataContext",
        Name = "Telerik.Sitefinity.Resources.Scripts.MicrosoftAjaxDataContext.js",
        Path = Url.Combine(this.MicrosoftAjaxCdnBaseUrl, "MicrosoftAjaxDataContext.js"),
        DebugPath = Url.Combine(this.MicrosoftAjaxCdnBaseUrl, "MicrosoftAjaxDataContext.debug.js"),
        IgnoreScriptPath = false,
        Assembly = name
      });
      scriptReferences.Add(new ScriptReferenceElement((ConfigElement) scriptReferences)
      {
        Key = "MicrosoftAjaxGlobalization",
        Name = "Telerik.Sitefinity.Resources.Scripts.MicrosoftAjaxGlobalization.js",
        Path = Url.Combine(this.MicrosoftAjaxCdnBaseUrl, "MicrosoftAjaxGlobalization.js"),
        DebugPath = Url.Combine(this.MicrosoftAjaxCdnBaseUrl, "MicrosoftAjaxGlobalization.debug.js"),
        IgnoreScriptPath = false,
        Assembly = str
      });
      scriptReferences.Add(new ScriptReferenceElement((ConfigElement) scriptReferences)
      {
        Key = "MicrosoftAjaxHistory",
        Name = "Telerik.Sitefinity.Resources.Scripts.MicrosoftAjaxHistory.js",
        Path = Url.Combine(this.MicrosoftAjaxCdnBaseUrl, "MicrosoftAjaxHistory.js"),
        DebugPath = Url.Combine(this.MicrosoftAjaxCdnBaseUrl, "MicrosoftAjaxHistory.debug.js"),
        IgnoreScriptPath = false,
        Assembly = str
      });
      scriptReferences.Add(new ScriptReferenceElement((ConfigElement) scriptReferences)
      {
        Key = "MicrosoftAjaxNetwork",
        Name = "Telerik.Sitefinity.Resources.Scripts.MicrosoftAjaxNetwork.js",
        Path = Url.Combine(this.MicrosoftAjaxCdnBaseUrl, "MicrosoftAjaxNetwork.js"),
        DebugPath = Url.Combine(this.MicrosoftAjaxCdnBaseUrl, "MicrosoftAjaxNetwork.debug.js"),
        IgnoreScriptPath = false,
        Assembly = str
      });
      scriptReferences.Add(new ScriptReferenceElement((ConfigElement) scriptReferences)
      {
        Key = "MicrosoftAjaxSerialization",
        Name = "Telerik.Sitefinity.Resources.Scripts.MicrosoftAjaxSerialization.js",
        Path = Url.Combine(this.MicrosoftAjaxCdnBaseUrl, "MicrosoftAjaxSerialization.js"),
        DebugPath = Url.Combine(this.MicrosoftAjaxCdnBaseUrl, "MicrosoftAjaxSerialization.debug.js"),
        IgnoreScriptPath = false,
        Assembly = str
      });
      scriptReferences.Add(new ScriptReferenceElement((ConfigElement) scriptReferences)
      {
        Key = "MicrosoftAjaxTemplates",
        Name = "Telerik.Sitefinity.Resources.Scripts.MicrosoftAjaxTemplates.js",
        Path = Url.Combine(this.MicrosoftAjaxCdnBaseUrl, "MicrosoftAjaxTemplates.js"),
        DebugPath = Url.Combine(this.MicrosoftAjaxCdnBaseUrl, "MicrosoftAjaxTemplates.debug.js"),
        IgnoreScriptPath = false,
        Assembly = name,
        Combine = false
      });
      scriptReferences.Add(new ScriptReferenceElement((ConfigElement) scriptReferences)
      {
        Key = "MicrosoftAjaxTimer",
        Name = "Telerik.Sitefinity.Resources.Scripts.MicrosoftAjaxTimer.js",
        Path = Url.Combine(this.MicrosoftAjaxCdnBaseUrl, "MicrosoftAjaxTimer.js"),
        DebugPath = Url.Combine(this.MicrosoftAjaxCdnBaseUrl, "MicrosoftAjaxTimer.debug.js"),
        IgnoreScriptPath = false,
        Assembly = str
      });
      scriptReferences.Add(new ScriptReferenceElement((ConfigElement) scriptReferences)
      {
        Key = "MicrosoftAjaxWebForms",
        Name = "Telerik.Sitefinity.Resources.Scripts.MicrosoftAjaxWebForms.js",
        Path = Url.Combine(this.MicrosoftAjaxCdnBaseUrl, "MicrosoftAjaxWebForms.js"),
        DebugPath = Url.Combine(this.MicrosoftAjaxCdnBaseUrl, "MicrosoftAjaxWebForms.js"),
        IgnoreScriptPath = false,
        Assembly = str,
        Combine = false
      });
      scriptReferences.Add(new ScriptReferenceElement((ConfigElement) scriptReferences)
      {
        Key = "MicrosoftAjaxWebServices",
        Name = "Telerik.Sitefinity.Resources.Scripts.MicrosoftAjaxWebServices.js",
        Path = Url.Combine(this.MicrosoftAjaxCdnBaseUrl, "MicrosoftAjaxWebServices.js"),
        DebugPath = Url.Combine(this.MicrosoftAjaxCdnBaseUrl, "MicrosoftAjaxWebServices.debug.js"),
        IgnoreScriptPath = false,
        Assembly = str
      });
      scriptReferences.Add(new ScriptReferenceElement((ConfigElement) scriptReferences)
      {
        Key = "Mootools",
        Name = "Telerik.Sitefinity.Resources.Scripts.mootools-1.2.2-core-yc.js",
        Path = "https://ajax.googleapis.com/ajax/libs/mootools/1.2.4/mootools-yui-compressed.js",
        DebugPath = "https://ajax.googleapis.com/ajax/libs/mootools/1.2.4/mootools.js ",
        IgnoreScriptPath = false,
        Assembly = name
      });
      scriptReferences.Add(new ScriptReferenceElement((ConfigElement) scriptReferences)
      {
        Key = "Prototype",
        Name = "Telerik.Sitefinity.Resources.Scripts.prototype-1.6.0.3.js",
        Path = "https://ajax.googleapis.com/ajax/libs/prototype/1.6.0.3/prototype.js",
        DebugPath = "",
        IgnoreScriptPath = false,
        Assembly = name
      });
      scriptReferences.Add(new ScriptReferenceElement((ConfigElement) scriptReferences)
      {
        Key = "DialogManager",
        Name = "Telerik.Sitefinity.Web.SitefinityJS.Utility.DialogManager.js",
        Path = "",
        DebugPath = "",
        IgnoreScriptPath = true,
        Assembly = Assembly.GetExecutingAssembly().GetName().Name
      });
      scriptReferences.Add(new ScriptReferenceElement((ConfigElement) scriptReferences)
      {
        Key = "TelerikSitefinity",
        Name = "Telerik.Sitefinity.Web.SitefinityJS.Telerik.Sitefinity.js",
        Path = "",
        DebugPath = "",
        IgnoreScriptPath = true,
        Assembly = Assembly.GetExecutingAssembly().GetName().Name
      });
      scriptReferences.Add(new ScriptReferenceElement((ConfigElement) scriptReferences)
      {
        Key = "QueryString",
        Name = "Telerik.Sitefinity.Web.SitefinityJS.Utility.Querystring.js",
        Path = "",
        DebugPath = "",
        IgnoreScriptPath = true,
        Assembly = Assembly.GetExecutingAssembly().GetName().Name
      });
      scriptReferences.Add(new ScriptReferenceElement((ConfigElement) scriptReferences)
      {
        Key = "KendoAll",
        Name = "Telerik.Sitefinity.Resources.Scripts.Kendo.kendo.all.min.js",
        Path = "https://kendo.cdn.telerik.com/2021.1.119/js/kendo.all.min.js",
        DebugPath = "",
        IgnoreScriptPath = true,
        Assembly = name
      });
      scriptReferences.Add(new ScriptReferenceElement((ConfigElement) scriptReferences)
      {
        Key = "KendoWeb",
        Name = "Telerik.Sitefinity.Resources.Scripts.Kendo.kendo.web.min.js",
        Path = "https://kendo.cdn.telerik.com/2021.1.119/js/kendo.web.min.js",
        DebugPath = "",
        IgnoreScriptPath = true,
        Assembly = name
      });
      scriptReferences.Add(new ScriptReferenceElement((ConfigElement) scriptReferences)
      {
        Key = "KendoTimezones",
        Name = "Telerik.Sitefinity.Resources.Scripts.Kendo.kendo.timezones.min.js",
        Path = "https://kendo.cdn.telerik.com/2021.1.119/js/kendo.timezones.min.js",
        DebugPath = "",
        IgnoreScriptPath = true,
        Assembly = name
      });
      scriptReferences.Add(new ScriptReferenceElement((ConfigElement) scriptReferences)
      {
        Key = "DecJsClient",
        Name = "Telerik.Sitefinity.Resources.Scripts.sitefinity-insight-client.min.3.1.2.js",
        Path = "https://cdn.insight.sitefinity.com/sdk/sitefinity-insight-client.min.3.1.2.js",
        DebugPath = "https://cdn.insight.sitefinity.com/sdk/sitefinity-insight-client.3.1.2.js",
        IgnoreScriptPath = false,
        Combine = false,
        Assembly = name,
        EnableCdn = new bool?(true)
      });
      scriptReferences.Add(new ScriptReferenceElement((ConfigElement) scriptReferences)
      {
        Key = "AngularJS",
        Name = "Telerik.Sitefinity.Resources.Scripts.AngularJS.angular-1.2.16.min.js",
        Path = "https://code.angularjs.org/1.2.16/angular.min.js",
        DebugPath = "https://code.angularjs.org/1.2.16/angular.js",
        IgnoreScriptPath = false,
        Assembly = name
      });
      this.AddNotAllowedPagesExtensions();
      this.KnownPageExtensions = ".aspx";
    }

    protected override void InitializeDefaultProviders(
      ConfigElementDictionary<string, DataProviderSettings> providers)
    {
      providers.Add(new DataProviderSettings((ConfigElement) providers)
      {
        Name = "OpenAccessDataProvider",
        Description = "A provider that stores content data in database using OpenAccess ORM.",
        ProviderType = typeof (OpenAccessPageProvider)
      });
    }

    public override void Upgrade(Version oldVersion, Version newVersion)
    {
      bool? nullable;
      if (oldVersion.Build <= 3900)
      {
        nullable = this.CombineScripts;
        bool flag = nullable.Value;
        this.CombineScriptsBackEnd = new bool?(flag);
        this.CombineScriptsFrontEnd = new bool?(flag);
      }
      ConfigProperty prop;
      if (oldVersion.Build < 7000 && this.Properties.TryGetValue("enableBrowseAndEdit", out prop))
      {
        PersistedValueWrapper valueWrapper;
        if (this.GetRawValue(prop, out valueWrapper).Equals(prop.DefaultValue) && (valueWrapper == null || valueWrapper.Source == ConfigSource.Default))
        {
          this.EnableBrowseAndEdit = new bool?(true);
        }
        else
        {
          nullable = new bool?();
          this.EnableBrowseAndEdit = nullable;
        }
      }
      base.Upgrade(oldVersion, newVersion);
    }

    protected virtual void AddNotAllowedPagesExtensions() => this.NotAllowedPageExtensions = ".bmp, .ico, .jpg, .jfif, .jpe, .jpeg, .tif, .gif, .tiff, .png, .avi, .asf, .ivf, .lsx, .lsf, .mpv2, .mpeg, .m1v, .mpa, .movie, .mpe, .mp2, .mov, .mpg, .qt, .swf, .m4v, .ogv, .pdf, .scd, .txt, .doc, .dot, .docx, .dotx, .docm, .dotm, .xls, .xlt, .xla, .xlsx, .xltx, .xlsm, .xltm, .xlam, .xlsb, .ppt, .pot, .pps, .ppa, .pptx, .potx, .ppsx, .ppam, .pptm, .potm, .ppsm, .odb, .odc, .odf, .odg, .odi, .odm, .odp, .ods, .odt, .otg, .oth, .otp, .ots, .ott, .*, .zip, .svc, .xamlx, .css, .js";

    /// <summary>
    /// Retrieves the not allowed extensions, that will not be processed from sitemap.
    /// </summary>
    /// <returns>Set of not allowed extensions.</returns>
    public ISet<string> GetNotAllowedExtensions()
    {
      if (this.ignoredExtensions == null)
      {
        this.ignoredExtensions = (ISet<string>) new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
        if (!this.NotAllowedPageExtensions.IsNullOrEmpty())
          this.ignoredExtensions.UnionWith((IEnumerable<string>) this.NotAllowedPageExtensions.Replace(" ", string.Empty).Split(new char[1]
          {
            ','
          }, StringSplitOptions.RemoveEmptyEntries));
      }
      return this.ignoredExtensions;
    }

    /// <summary>
    /// Gets the allowed extensions, that will not be processed from sitemap.
    /// </summary>
    /// <returns>Set of knowns extensions.</returns>
    public ISet<string> KnownExtensions
    {
      get
      {
        if (this.knownExtensions == null)
        {
          this.knownExtensions = (ISet<string>) new HashSet<string>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
          if (!this.KnownPageExtensions.IsNullOrEmpty())
            this.knownExtensions.UnionWith((IEnumerable<string>) this.KnownPageExtensions.Replace(" ", string.Empty).Split(new char[1]
            {
              ','
            }, StringSplitOptions.RemoveEmptyEntries));
        }
        return this.knownExtensions;
      }
    }

    /// <summary>Adds a new extension to the allowed page extensions.</summary>
    /// <param name="extensions">The extensions.</param>
    internal void AddKnownExtensions(params string[] extensions)
    {
      List<string> values = new List<string>((IEnumerable<string>) this.KnownExtensions);
      values.AddRange((IEnumerable<string>) extensions);
      this.KnownPageExtensions = string.Join(", ", (IEnumerable<string>) values);
    }

    private void LoadBackendPages(ConfigElementDictionary<string, PageElement> pages)
    {
      string name = typeof (PageResources).Name;
      PageDataElement pageDataElement = new PageDataElement((ConfigElement) pages);
      pageDataElement.Name = "Dashboard";
      pageDataElement.PageId = SiteInitializer.LegacyDashboardPageNodeId;
      pageDataElement.UrlName = "Dashboard";
      pageDataElement.MenuName = "Dashboard";
      pageDataElement.HtmlTitle = "DashboardHtmlTitle";
      pageDataElement.Description = "DashboardDescription";
      pageDataElement.TemplateName = "DefaultBackend";
      pageDataElement.IncludeScriptManager = true;
      pageDataElement.Ordinal = -1f;
      pageDataElement.ResourceClassId = name;
      PageElement element1 = (PageElement) pageDataElement;
      pages.Add(element1);
      ConfigElementDictionary<string, PageElement> elementDictionary1 = pages;
      PageDataElement element2 = new PageDataElement((ConfigElement) pages);
      element2.Name = "Pages";
      element2.PageId = SiteInitializer.PagesNodeId;
      element2.UrlName = "Pages";
      element2.MenuName = "Pages";
      element2.HtmlTitle = "PagesHtmlTitle";
      element2.Description = "PagesDescription";
      element2.TemplateName = "DefaultBackend";
      element2.IncludeScriptManager = true;
      element2.EnableViewState = true;
      element2.Ordinal = -0.5f;
      element2.ResourceClassId = name;
      element2.ShowInNavigation = true;
      elementDictionary1.Add((PageElement) element2);
      PageElement pageElement1 = new PageElement((ConfigElement) pages)
      {
        Name = "Content",
        PageId = SiteInitializer.ContentNodeId,
        UrlName = "ContentNodeUrlName",
        MenuName = "ContentNodeTitle",
        Description = "ContentNodeDescription",
        ResourceClassId = name,
        Ordinal = -0.2f
      };
      pages.Add(pageElement1);
      this.CreateContentPages(pageElement1);
      PageElement pageElement2 = new PageElement((ConfigElement) pages)
      {
        Name = "Design",
        PageId = SiteInitializer.DesignNodeId,
        UrlName = "DesignNodeUrlName",
        MenuName = "DesignNodeTitle",
        Description = "DesignNodeDescription",
        ResourceClassId = name,
        Ordinal = 1.5f
      };
      pages.Add(pageElement2);
      this.CreateDesignPages(pageElement2);
      PageElement pageElement3 = new PageElement((ConfigElement) pages)
      {
        Name = "Admin",
        PageId = SiteInitializer.AdministrationNodeId,
        UrlName = "AdminTitle",
        MenuName = "AdminTitle",
        Description = "AdminDescription",
        ResourceClassId = name,
        Ordinal = 2f
      };
      pages.Add(pageElement3);
      this.CreateAdminPages(pageElement3);
      PageElement pageElement4 = new PageElement((ConfigElement) pages)
      {
        Name = "Marketing",
        PageId = SiteInitializer.MarketingNodeId,
        UrlName = "MarketingUrlName",
        MenuName = "MarketingTitle",
        Description = "MarketingDescription",
        ResourceClassId = name,
        Ordinal = 2.5f
      };
      pages.Add(pageElement4);
      this.CreateMarketingPages(pageElement4);
      PageElement pageElement5 = new PageElement((ConfigElement) pages)
      {
        Name = "Help",
        PageId = SiteInitializer.HelpNodeId,
        UrlName = "HelpNodeTitle",
        MenuName = "HelpNodeTitle",
        Description = "HelpNodeDescription",
        ResourceClassId = name,
        ShowInNavigation = false
      };
      pages.Add(pageElement5);
      this.CreateHelpPages(pageElement5);
      ConfigElementDictionary<string, PageElement> elementDictionary2 = pages;
      PageDataElement element3 = new PageDataElement((ConfigElement) pages);
      element3.Name = "Profile";
      element3.PageId = SiteInitializer.ProfileNodeId;
      element3.UrlName = "ProfileUrlName";
      element3.MenuName = "ProfileTitle";
      element3.HtmlTitle = "ProfileHtmlTitle";
      element3.Description = "ProfileDescription";
      element3.TemplateName = "DefaultBackend";
      element3.IncludeScriptManager = true;
      element3.ShowInNavigation = false;
      element3.EnableViewState = false;
      element3.ResourceClassId = name;
      elementDictionary2.Add((PageElement) element3);
      PageElement pageElement6 = new PageElement((ConfigElement) pages)
      {
        Name = "Login",
        PageId = SiteInitializer.LoginNodeId,
        UrlName = "LoginNodeUrlName",
        MenuName = "LoginNodeTitle",
        Description = "LoginNodeDescription",
        ShowInNavigation = false,
        ResourceClassId = name
      };
      pages.Add(pageElement6);
      this.CreateLoginPages(pageElement6);
    }

    private void CreateLoginPages(PageElement root)
    {
      ConfigElementDictionary<string, PageElement> pages = root.Pages;
      string name = typeof (PageResources).Name;
      PageDataElement element1 = new PageDataElement((ConfigElement) pages);
      element1.Name = "LoginForm";
      element1.PageId = SiteInitializer.LoginFormPageId;
      element1.UrlName = "LoginFormUrlName";
      element1.MenuName = "LoginFormTitle";
      element1.HtmlTitle = "LoginFormTitle";
      element1.Description = "LoginFormDescription";
      element1.TemplateName = "DefaultBackendEmpty";
      element1.ShowInNavigation = false;
      element1.ResourceClassId = name;
      pages.Add((PageElement) element1);
      PageDataElement element2 = new PageDataElement((ConfigElement) pages);
      element2.Name = "UserLimitReached";
      element2.PageId = SiteInitializer.UserLimitReachedPageId;
      element2.UrlName = "UserLimitReachedUrlName";
      element2.MenuName = "UserLimitReachedTitle";
      element2.HtmlTitle = "UserLimitReachedTitle";
      element2.Description = "UserLimitReachedDescription";
      element2.TemplateName = "DefaultBackendEmpty";
      element2.IncludeScriptManager = true;
      element2.ShowInNavigation = false;
      element2.ResourceClassId = name;
      pages.Add((PageElement) element2);
      PageDataElement element3 = new PageDataElement((ConfigElement) pages);
      element3.Name = "UserAlreadyLoggedIn";
      element3.PageId = SiteInitializer.UserAlreadyLoggedInPageId;
      element3.UrlName = "UserAlreadyLoggedInUrlName";
      element3.MenuName = "UserAlreadyLoggedInTitle";
      element3.HtmlTitle = "UserAlreadyLoggedInTitle";
      element3.Description = "UserAlreadyLoggedInDescription";
      element3.TemplateName = "DefaultBackendEmpty";
      element3.IncludeScriptManager = true;
      element3.ShowInNavigation = false;
      element3.ResourceClassId = name;
      pages.Add((PageElement) element3);
      PageDataElement element4 = new PageDataElement((ConfigElement) pages);
      element4.Name = "NeedAdminRights";
      element4.PageId = SiteInitializer.NeedAdminRightsPageId;
      element4.UrlName = "NeedAdminRightsUrlName";
      element4.MenuName = "NeedAdminRightsTitle";
      element4.HtmlTitle = "NeedAdminRightsTitle";
      element4.Description = "NeedAdminRightsDescription";
      element4.TemplateName = "DefaultBackendEmpty";
      element4.IncludeScriptManager = true;
      element4.ShowInNavigation = false;
      element4.ResourceClassId = name;
      pages.Add((PageElement) element4);
      PageDataElement element5 = new PageDataElement((ConfigElement) pages);
      element5.Name = "SiteNotAccessible";
      element5.PageId = SiteInitializer.SiteNotAccessiblePageId;
      element5.UrlName = "SiteNotAccessibleUrlName";
      element5.MenuName = "SiteNotAccessibleTitle";
      element5.HtmlTitle = "SiteNotAccessibleTitle";
      element5.Description = "SiteNotAccessibleDescription";
      element5.TemplateName = "DefaultBackendEmpty";
      element5.IncludeScriptManager = true;
      element5.ShowInNavigation = false;
      element5.ResourceClassId = name;
      pages.Add((PageElement) element5);
      PageDataElement element6 = new PageDataElement((ConfigElement) pages);
      element6.Name = "LoginFailed";
      element6.PageId = SiteInitializer.LoginFailedPageId;
      element6.UrlName = "LoginFailedUrlName";
      element6.MenuName = "LoginFailedTitle";
      element6.HtmlTitle = "LoginFailedTitle";
      element6.Description = "LoginFailedDescription";
      element6.TemplateName = "DefaultBackendEmpty";
      element6.IncludeScriptManager = true;
      element6.ShowInNavigation = false;
      element6.ResourceClassId = name;
      pages.Add((PageElement) element6);
    }

    private void CreateContentPages(PageElement root)
    {
      ConfigElementDictionary<string, PageElement> pages = root.Pages;
      string name = typeof (PageResources).Name;
      pages.Add(new PageElement((ConfigElement) pages)
      {
        Name = "Modules",
        PageId = SiteInitializer.ModulesNodeId,
        UrlName = "ModulesNodeUrlName",
        MenuName = "ModulesNodeTitle",
        Description = "ModulesNodeDescription",
        ResourceClassId = name,
        RenderAsLink = false
      });
      pages.Add(new PageElement((ConfigElement) pages)
      {
        Name = "Discussions",
        PageId = SiteInitializer.DiscussionsPageNodeId,
        UrlName = "DiscussionsGroupPageUrlName",
        MenuName = "DiscussionsGroupPageTitle",
        Description = "DiscussionsGroupPageDescription",
        ResourceClassId = name,
        RenderAsLink = false,
        ShowInNavigation = true
      });
      PageElement pageElement1 = new PageElement((ConfigElement) pages)
      {
        Name = "Taxonomy",
        PageId = SiteInitializer.TaxonomyNodeId,
        UrlName = "TaxonomyNodeUrlName",
        MenuName = "TaxonomyNodeTitle",
        Description = "TaxonomyNodeDescription",
        ResourceClassId = name,
        RenderAsLink = false,
        ShowInNavigation = true,
        Ordinal = 11f
      };
      pages.Add(pageElement1);
      this.CreateTaxonomyPages(pageElement1);
      PageElement pageElement2 = new PageElement((ConfigElement) pages)
      {
        Name = "ContentLocationsManagementNodeTitle",
        PageId = SiteInitializer.ContentLocationsGroupNodePageId,
        UrlName = "ContentLocationsManagementUrlName",
        MenuName = "ContentLocationsManagementNodeTitle",
        Description = "ContentLocationsDescription",
        ResourceClassId = typeof (ContentLocationsResources).Name,
        RenderAsLink = false,
        ShowInNavigation = false,
        Ordinal = 12f
      };
      pages.Add(pageElement2);
      this.CreateContentLocationPages(pageElement2);
    }

    private void CreateTaxonomyPages(PageElement root)
    {
      ConfigElementDictionary<string, PageElement> pages = root.Pages;
      string name = typeof (PageResources).Name;
      PageDataElement element1 = new PageDataElement((ConfigElement) pages);
      element1.Name = "FlatTaxonomy";
      element1.PageId = SiteInitializer.FlatTaxonomyPageId;
      element1.MenuName = "FlatTaxonomyMenuName";
      element1.UrlName = SiteInitializer.FlatTaxonomyUrlName;
      element1.Description = "FlatTaxonomyDescription";
      element1.ResourceClassId = typeof (TaxonomyResources).Name;
      element1.IncludeScriptManager = true;
      element1.ShowInNavigation = true;
      element1.EnableViewState = false;
      element1.Ordinal = 11.1f;
      element1.TemplateName = "DefaultBackend";
      pages.Add((PageElement) element1);
      PageDataElement element2 = new PageDataElement((ConfigElement) pages);
      element2.Name = "HierarchicalTaxonomy";
      element2.PageId = SiteInitializer.HierarchicalTaxonomyPageId;
      element2.MenuName = "HierarchicalTaxonomyMenuName";
      element2.UrlName = SiteInitializer.HierarchicalTaxonomyUrlName;
      element2.Description = "HierarchicalTaxonomyDescription";
      element2.ResourceClassId = typeof (TaxonomyResources).Name;
      element2.IncludeScriptManager = true;
      element2.ShowInNavigation = true;
      element2.EnableViewState = false;
      element2.Ordinal = 11.2f;
      element2.TemplateName = "DefaultBackend";
      pages.Add((PageElement) element2);
      PageDataElement pageDataElement1 = new PageDataElement((ConfigElement) pages);
      pageDataElement1.Name = "NetworkTaxonomy";
      pageDataElement1.PageId = SiteInitializer.NetworkTaxonomyPageId;
      pageDataElement1.MenuName = "NetworkTaxonomyUserFriendly";
      pageDataElement1.UrlName = "Network";
      pageDataElement1.Description = "NetworkTaxonomyDescription";
      pageDataElement1.ResourceClassId = typeof (TaxonomyResources).Name;
      pageDataElement1.IncludeScriptManager = true;
      pageDataElement1.ShowInNavigation = false;
      pageDataElement1.EnableViewState = false;
      pageDataElement1.Ordinal = 11.3f;
      pageDataElement1.TemplateName = "DefaultBackend";
      PageDataElement element3 = pageDataElement1;
      PageControlElement element4 = new PageControlElement((ConfigElement) element3.Controls)
      {
        Type = typeof (NetworkTaxonomyPanel)
      };
      element3.Controls.Add(element4);
      pages.Add((PageElement) element3);
      PageDataElement pageDataElement2 = new PageDataElement((ConfigElement) pages);
      pageDataElement2.Name = "FacetTaxonomy";
      pageDataElement2.PageId = SiteInitializer.FacetTaxonomyPageId;
      pageDataElement2.MenuName = "FacetTaxonomyUserFriendly";
      pageDataElement2.UrlName = "Facet";
      pageDataElement2.Description = "FacetTaxonomyDescription";
      pageDataElement2.ResourceClassId = typeof (TaxonomyResources).Name;
      pageDataElement2.IncludeScriptManager = true;
      pageDataElement2.ShowInNavigation = false;
      pageDataElement2.EnableViewState = false;
      pageDataElement2.Ordinal = 11.4f;
      pageDataElement2.TemplateName = "DefaultBackend";
      PageDataElement element5 = pageDataElement2;
      PageControlElement element6 = new PageControlElement((ConfigElement) element5.Controls)
      {
        Type = typeof (FacetTaxonomyPanel)
      };
      element5.Controls.Add(element6);
      pages.Add((PageElement) element5);
      PageDataElement pageDataElement3 = new PageDataElement((ConfigElement) pages);
      pageDataElement3.Name = "Classifications";
      pageDataElement3.PageId = SiteInitializer.TaxonomiesNodeId;
      pageDataElement3.UrlName = "List";
      pageDataElement3.MenuName = "AllClassifications";
      pageDataElement3.HtmlTitle = "ClassificationsHtmlTitle";
      pageDataElement3.Description = "ClassificationsDescription";
      pageDataElement3.TemplateName = "DefaultBackend";
      pageDataElement3.IncludeScriptManager = true;
      pageDataElement3.ResourceClassId = name;
      pageDataElement3.Ordinal = 11.5f;
      pageDataElement3.ShowInNavigation = true;
      PageDataElement element7 = pageDataElement3;
      element7.Presentation.Add(new PresentationElement((ConfigElement) element7.Presentation)
      {
        Name = "NodeCssClass",
        DataType = "CSS_CLASS",
        Data = "sfSettings"
      });
      pages.Add((PageElement) element7);
      PageDataElement element8 = new PageDataElement((ConfigElement) pages);
      element8.Name = "MarkedItems";
      element8.PageId = SiteInitializer.MarkedItemsPageId;
      element8.UrlName = "MarkedItems";
      element8.MenuName = "MarkedItems";
      element8.Description = "MarkedItemsDescription";
      element8.HtmlTitle = "MarkedItemsHtmlTitle";
      element8.TemplateName = "DefaultBackend";
      element8.IncludeScriptManager = true;
      element8.ShowInNavigation = false;
      element8.ResourceClassId = name;
      pages.Add((PageElement) element8);
    }

    private void CreateContentLocationPages(PageElement root)
    {
      ConfigElementDictionary<string, PageElement> pages = root.Pages;
      string name = typeof (ContentLocationsResources).Name;
      PageDataElement element = new PageDataElement((ConfigElement) pages);
      element.Name = "ContentLocations";
      element.PageId = SiteInitializer.ContentLocationsPageId;
      element.UrlName = "ContentLocationsManagementUrlName";
      element.MenuName = "ContentLocationsManagementNodeTitle";
      element.Description = "ContentLocationsDescription";
      element.HtmlTitle = "ContentLocationsManagementNodeTitle";
      element.TemplateName = "DefaultBackend";
      element.IncludeScriptManager = true;
      element.ShowInNavigation = false;
      element.ResourceClassId = name;
      pages.Add((PageElement) element);
    }

    private void CreateDesignPages(PageElement root)
    {
      ConfigElementDictionary<string, PageElement> pages1 = root.Pages;
      string name = typeof (PageResources).Name;
      PageElement element1 = new PageElement((ConfigElement) pages1)
      {
        Name = "Templates",
        PageId = SiteInitializer.TemplatesNodeId,
        UrlName = "TemplatesNodeUrlName",
        MenuName = "TemplatesNodeTitle",
        Description = "TemplatesNodeDescription",
        ResourceClassId = name,
        RenderAsLink = false
      };
      pages1.Add(element1);
      ConfigElementDictionary<string, PageElement> pages2 = element1.Pages;
      PageDataElement element2 = new PageDataElement((ConfigElement) element1.Pages);
      element2.Name = "PageTemplates";
      element2.PageId = SiteInitializer.PageTemplatesNodeId;
      element2.UrlName = "PageTemplatesUrlName";
      element2.MenuName = "PageTemplatesTitle";
      element2.HtmlTitle = "PageTemplatesHtmlTitle";
      element2.Description = "PageTemplatesDescription";
      element2.TemplateName = "DefaultBackend";
      element2.IncludeScriptManager = true;
      element2.Ordinal = 1f;
      element2.ResourceClassId = name;
      pages2.Add((PageElement) element2);
    }

    private void CreateAdminPages(PageElement root)
    {
      ConfigElementDictionary<string, PageElement> pages = root.Pages;
      string name = typeof (PageResources).Name;
      PageElement pageElement1 = new PageElement((ConfigElement) pages)
      {
        Name = "UserManagement",
        PageId = SiteInitializer.UsersNodeId,
        UrlName = "UserManagementNodeUrlName",
        MenuName = "UserManagementNodeTitle",
        Description = "UserManagementNodeDescription",
        ResourceClassId = name,
        RenderAsLink = false,
        Ordinal = 1f
      };
      pages.Add(pageElement1);
      this.CreateUserManagementPages(pageElement1);
      PageElement pageElement2 = new PageElement((ConfigElement) pages)
      {
        Name = "SettingsAndConfigurations",
        PageId = SiteInitializer.SettingsAndConfigurationsNodeId,
        UrlName = "SettingsAndConfigurationsNodeUrlName",
        MenuName = "SettingsAndConfigurationsNodeTitle",
        Description = "SettingsAndConfigurationsNodeDescription",
        ResourceClassId = name,
        RenderAsLink = false,
        Ordinal = 1.1f
      };
      pages.Add(pageElement2);
      this.CreateSettingsPages(pageElement2);
      PageElement pageElement3 = new PageElement((ConfigElement) pages)
      {
        Name = "System",
        PageId = SiteInitializer.SystemNodeId,
        UrlName = "SystemNodeUrlName",
        MenuName = "SystemNodeTitle",
        Description = "SystemNodeDescription",
        ResourceClassId = name,
        RenderAsLink = false,
        Ordinal = 1.2f
      };
      pages.Add(pageElement3);
      this.CreateSystemPages(pageElement3);
      PageElement pageElement4 = new PageElement((ConfigElement) pages)
      {
        Name = "Tools",
        PageId = SiteInitializer.ToolsNodeId,
        UrlName = "ToolsNodeUrlName",
        MenuName = "ToolsNodeTitle",
        Description = "ToolsNodeDescription",
        ResourceClassId = name,
        RenderAsLink = false,
        Ordinal = 1.3f
      };
      pages.Add(pageElement4);
      this.CreateToolsPages(pageElement4);
      pages.Add(new PageElement((ConfigElement) pages)
      {
        Name = "Services",
        PageId = SiteInitializer.ServicesNodeId,
        UrlName = "ServicesNodeUrlName",
        MenuName = "ServicesNodeTitle",
        Description = "ServicesNodeDescription",
        ResourceClassId = name,
        Ordinal = 1.4f
      });
    }

    private void CreateUserManagementPages(PageElement root)
    {
      ConfigElementDictionary<string, PageElement> pages = root.Pages;
      string name = typeof (PageResources).Name;
      PageDataElement element1 = new PageDataElement((ConfigElement) pages);
      element1.Name = "Users";
      element1.PageId = SiteInitializer.UsersPageId;
      element1.UrlName = "Users";
      element1.MenuName = "Users";
      element1.HtmlTitle = "UsersHtmlTitle";
      element1.Description = "UsersDescription";
      element1.TemplateName = "DefaultBackend";
      element1.IncludeScriptManager = true;
      element1.ResourceClassId = name;
      element1.ViewActionName = "ManageUsers";
      element1.Ordinal = 1f;
      pages.Add((PageElement) element1);
      PageDataElement element2 = new PageDataElement((ConfigElement) pages);
      element2.Name = "Roles";
      element2.PageId = SiteInitializer.RolesPageId;
      element2.UrlName = "Roles";
      element2.MenuName = "Roles";
      element2.HtmlTitle = "RolesHtmltitle";
      element2.Description = "RolesDescription";
      element2.TemplateName = "DefaultBackend";
      element2.IncludeScriptManager = true;
      element2.ResourceClassId = name;
      element2.ViewActionName = "ManageRoles";
      element2.Ordinal = 1.1f;
      pages.Add((PageElement) element2);
      PageDataElement element3 = new PageDataElement((ConfigElement) pages);
      element3.PageId = SiteInitializer.PermissionsPageId;
      element3.Name = "Permissions";
      element3.UrlName = "Permissions";
      element3.MenuName = "Permissions";
      element3.HtmlTitle = "PermissionsHtmlTitle";
      element3.Description = "PermissionsDescription";
      element3.TemplateName = "DefaultBackend";
      element3.IncludeScriptManager = true;
      element3.ResourceClassId = name;
      element3.ViewActionName = "ViewPermissions";
      element3.Ordinal = 1.2f;
      pages.Add((PageElement) element3);
      this.AddUserProfileTypesPages(root);
    }

    private void AddUserProfileTypesPages(PageElement root)
    {
      ConfigElementDictionary<string, PageElement> pages = root.Pages;
      PageDataElement element = new PageDataElement((ConfigElement) pages);
      element.PageId = SiteInitializer.ProfileTypesPageId;
      element.Name = "UserProfileTypes";
      element.UrlName = "UserProfileTypesUrlName";
      element.MenuName = "UserProfileTypes";
      element.HtmlTitle = "UserProfileTypesHtmlTitle";
      element.Description = "UserProfileTypesDescription";
      element.TemplateName = "DefaultBackend";
      element.IncludeScriptManager = true;
      element.ResourceClassId = typeof (UserProfilesResources).Name;
      element.ShowInNavigation = false;
      element.RenderAsLink = true;
      element.ViewActionName = "ManageUserProfiles";
      element.Ordinal = 1.3f;
      pages.Add((PageElement) element);
    }

    private void CreateSettingsPages(PageElement root)
    {
      ConfigElementDictionary<string, PageElement> pages = root.Pages;
      string name = typeof (PageResources).Name;
      PageElement pageElement = new PageElement((ConfigElement) pages)
      {
        Name = "Settings",
        PageId = SiteInitializer.SettingsNodeId,
        UrlName = "SettingsUrlName",
        MenuName = "SettingsTitle",
        Description = "SettingsDescription",
        ResourceClassId = name,
        Ordinal = 1f
      };
      pages.Add(pageElement);
      this.CreateConfigSettingsPages(pageElement);
    }

    private void CreateToolsPages(PageElement root)
    {
      ConfigElementDictionary<string, PageElement> pages = root.Pages;
      string name = typeof (PageResources).Name;
      PageDataElement element = new PageDataElement((ConfigElement) pages);
      element.Name = "Files";
      element.PageId = SiteInitializer.FilesPageId;
      element.UrlName = "FilesUrlName";
      element.MenuName = "Files";
      element.HtmlTitle = "FilesHtmlTitle";
      element.Description = "FilesDescription";
      element.TemplateName = "DefaultBackend";
      element.IncludeScriptManager = true;
      element.EnableViewState = true;
      element.ResourceClassId = name;
      element.ViewActionName = "ManageFiles";
      element.Ordinal = 3f;
      pages.Add((PageElement) element);
    }

    private void CreateConfigSettingsPages(PageElement root)
    {
      ConfigElementDictionary<string, PageElement> pages = root.Pages;
      string name = typeof (PageResources).Name;
      PageDataElement element1 = new PageDataElement((ConfigElement) pages);
      element1.Name = "BasicSettings";
      element1.PageId = SiteInitializer.BasicSettingsNodeId;
      element1.UrlName = "BasicSettingsUrlName";
      element1.MenuName = "BasicSettingsTitle";
      element1.HtmlTitle = "BasicSettingsHtmlTitle";
      element1.Description = "BasicSettingsDescription";
      element1.TemplateName = "DefaultBackend";
      element1.IncludeScriptManager = true;
      element1.ResourceClassId = name;
      element1.ShowInNavigation = false;
      element1.Ordinal = 1f;
      pages.Add((PageElement) element1);
      PageDataElement element2 = new PageDataElement((ConfigElement) pages);
      element2.Name = "AdvancedSettings";
      element2.PageId = SiteInitializer.AdvancedSettingsNodeId;
      element2.UrlName = "SettingsNodeUrlName";
      element2.MenuName = "Settings";
      element2.HtmlTitle = "SettingsTitle";
      element2.Description = "SettingsDescriptionMenu";
      element2.TemplateName = "DefaultBackend";
      element2.IncludeScriptManager = true;
      element2.ResourceClassId = name;
      element2.ViewActionName = "ChangeConfigurations";
      element2.ShowInNavigation = false;
      element2.Ordinal = 1.1f;
      pages.Add((PageElement) element2);
    }

    private void CreateSystemPages(PageElement root)
    {
      ConfigElementDictionary<string, PageElement> pages1 = root.Pages;
      string name = typeof (PageResources).Name;
      PageDataElement element1 = new PageDataElement((ConfigElement) pages1);
      element1.Name = "ModulesAndServices";
      element1.PageId = SiteInitializer.BackendPageModulesAndServicesNodeId;
      element1.UrlName = "BackendModulesAndServicesUrlName";
      element1.MenuName = "BackendModulesAndServicesTitle";
      element1.HtmlTitle = "BackendModulesAndServicesHtmlTitle";
      element1.Description = "BackendModulesAndServicesDescription";
      element1.TemplateName = "DefaultBackend";
      element1.IncludeScriptManager = true;
      element1.ShowInNavigation = true;
      element1.EnableViewState = true;
      element1.ResourceClassId = name;
      element1.Ordinal = 0.9f;
      pages1.Add((PageElement) element1);
      PageDataElement element2 = new PageDataElement((ConfigElement) pages1);
      element2.Name = "Workflow";
      element2.PageId = SiteInitializer.WorkflowPageId;
      element2.UrlName = "Workflow";
      element2.MenuName = "Workflow";
      element2.HtmlTitle = "WorkflowTitle";
      element2.Description = "WorkflowDescriptionMenu";
      element2.TemplateName = "DefaultBackend";
      element2.IncludeScriptManager = true;
      element2.ResourceClassId = name;
      element2.Ordinal = 1f;
      pages1.Add((PageElement) element2);
      PageDataElement element3 = new PageDataElement((ConfigElement) pages1);
      element3.Name = "Labels";
      element3.PageId = SiteInitializer.LabelsPageId;
      element3.UrlName = "Labels";
      element3.MenuName = "Localization";
      element3.HtmlTitle = "LocalizationHtmlTitle";
      element3.Description = "LocalizationDescription";
      element3.TemplateName = "DefaultBackend";
      element3.IncludeScriptManager = true;
      element3.ResourceClassId = name;
      element3.ViewActionName = "ManageLabels";
      element3.Ordinal = 1.1f;
      pages1.Add((PageElement) element3);
      PageElement element4 = new PageElement((ConfigElement) pages1)
      {
        PageId = SiteInitializer.BackendPagesNodeId,
        Name = "BackendPagesNode",
        UrlName = "BackendPagesNodeUrlName",
        MenuName = "BackendPagesNodeTitle",
        Description = "BackendPagesDescription",
        ResourceClassId = name,
        Ordinal = 1.5f
      };
      pages1.Add(element4);
      ConfigElementDictionary<string, PageElement> pages2 = element4.Pages;
      PageDataElement element5 = new PageDataElement((ConfigElement) element4.Pages);
      element5.Name = "BackendPagesWarning";
      element5.PageId = SiteInitializer.BackendPagesWarningPageId;
      element5.UrlName = "BackendPagesWarningUrlName";
      element5.MenuName = "BackendPagesWarningTitle";
      element5.HtmlTitle = "BackendPagesWarningHtmlTitle";
      element5.Description = "BackendPagesDescription";
      element5.TemplateName = "DefaultBackend";
      element5.IncludeScriptManager = true;
      element5.EnableViewState = true;
      element5.ResourceClassId = name;
      element5.ShowInNavigation = false;
      element5.Ordinal = 1f;
      pages2.Add((PageElement) element5);
      ConfigElementDictionary<string, PageElement> pages3 = element4.Pages;
      PageDataElement element6 = new PageDataElement((ConfigElement) element4.Pages);
      element6.Name = "BackendPages";
      element6.PageId = SiteInitializer.BackendPagesActualNodeId;
      element6.UrlName = "BackendPagesUrlName";
      element6.MenuName = "BackendPagesTitle";
      element6.HtmlTitle = "BackendPagesHtmlTitle";
      element6.Description = "BackendPagesDescription";
      element6.TemplateName = "DefaultBackend";
      element6.IncludeScriptManager = true;
      element6.EnableViewState = true;
      element6.ResourceClassId = name;
      element6.ShowInNavigation = false;
      element6.Ordinal = 1.1f;
      pages3.Add((PageElement) element6);
      PageDataElement element7 = new PageDataElement((ConfigElement) pages1);
      element7.Name = "BackendTemplates";
      element7.PageId = SiteInitializer.BackendPageTemplatesNodeId;
      element7.UrlName = "BackendTemplatesUrlName";
      element7.MenuName = "BackendTemplatesTitle";
      element7.HtmlTitle = "BackendTemplatesHtmlTitle";
      element7.Description = "BackendTemplatesDescription";
      element7.TemplateName = "DefaultBackend";
      element7.IncludeScriptManager = true;
      element7.ShowInNavigation = false;
      element7.EnableViewState = true;
      element7.ResourceClassId = name;
      element7.Ordinal = 1.2f;
      pages1.Add((PageElement) element7);
      PageDataElement element8 = new PageDataElement((ConfigElement) pages1);
      element8.Name = "HelpAndResources";
      element8.PageId = SiteInitializer.HelpAndResourcesNodeId;
      element8.UrlName = "HelpAndResourcesUrlName";
      element8.MenuName = "HelpAndResourcesTitle";
      element8.HtmlTitle = "HelpAndResourcesHtmlTitle";
      element8.Description = "HelpAndResourcesDescription";
      element8.TemplateName = "DefaultBackend";
      element8.IncludeScriptManager = true;
      element8.ShowInNavigation = false;
      element8.EnableViewState = true;
      element8.ResourceClassId = name;
      element8.Ordinal = 1.3f;
      pages1.Add((PageElement) element8);
      PageDataElement element9 = new PageDataElement((ConfigElement) pages1);
      element9.Name = "VersionAndLicensing";
      element9.PageId = SiteInitializer.LicensePageId;
      element9.UrlName = "VersionAndLicensingUrlName";
      element9.MenuName = "VersionAndLicensingTitle";
      element9.HtmlTitle = "VersionAndLicensingHtmlTitle";
      element9.Description = "VersionAndLicensingDescription";
      element9.TemplateName = "DefaultBackend";
      element9.IncludeScriptManager = true;
      element9.ShowInNavigation = true;
      element9.EnableViewState = false;
      element9.ResourceClassId = name;
      element9.ViewActionName = "ManageLicenses";
      element9.Ordinal = 2f;
      pages1.Add((PageElement) element9);
    }

    private void CreateHelpPages(PageElement root)
    {
      ConfigElementDictionary<string, PageElement> pages = root.Pages;
      string name = typeof (PageResources).Name;
      PageDataElement element1 = new PageDataElement((ConfigElement) pages);
      element1.Name = "HelpContents";
      element1.PageId = new Guid("F669D9A7-009D-4d83-AAAA-000000000015");
      element1.UrlName = "HelpContentsUrlName";
      element1.MenuName = "HelpContentsTitle";
      element1.HtmlTitle = "HelpContentsHtmlTitle";
      element1.Description = "HelpContentsDescription";
      element1.TemplateName = "DefaultBackend";
      element1.IncludeScriptManager = true;
      element1.ShowInNavigation = true;
      element1.EnableViewState = false;
      element1.ResourceClassId = name;
      pages.Add((PageElement) element1);
      PageDataElement element2 = new PageDataElement((ConfigElement) pages);
      element2.Name = "OnlineResources";
      element2.PageId = new Guid("F669D9A7-009D-4d83-AAAA-000000000016");
      element2.UrlName = "OnlineResourcesUrlName";
      element2.MenuName = "OnlineResourcesTitle";
      element2.HtmlTitle = "OnlineResourcesHtmlTitle";
      element2.Description = "OnlineResourcesDescription";
      element2.TemplateName = "DefaultBackend";
      element2.IncludeScriptManager = true;
      element2.ShowInNavigation = true;
      element2.EnableViewState = false;
      element2.ResourceClassId = name;
      pages.Add((PageElement) element2);
      PageDataElement element3 = new PageDataElement((ConfigElement) pages);
      element3.Name = "TechnicalSupport";
      element3.PageId = new Guid("F669D9A7-009D-4d83-AAAA-000000000017");
      element3.UrlName = "TechnicalSupportUrlName";
      element3.MenuName = "TechnicalSupportTitle";
      element3.HtmlTitle = "TechnicalSupportHtmlTitle";
      element3.Description = "TechnicalSupportDescription";
      element3.TemplateName = "DefaultBackend";
      element3.IncludeScriptManager = true;
      element3.ShowInNavigation = true;
      element3.EnableViewState = false;
      element3.ResourceClassId = name;
      pages.Add((PageElement) element3);
      PageDataElement element4 = new PageDataElement((ConfigElement) pages);
      element4.Name = "CustomerFeedback";
      element4.PageId = new Guid("F669D9A7-009D-4d83-AAAA-000000000018");
      element4.UrlName = "CustomerFeedbackUrlName";
      element4.MenuName = "CustomerFeedbackTitle";
      element4.HtmlTitle = "CustomerFeedbackHtmlTitle";
      element4.Description = "CustomerFeedbackDescription";
      element4.TemplateName = "DefaultBackend";
      element4.IncludeScriptManager = true;
      element4.ShowInNavigation = true;
      element4.EnableViewState = false;
      element4.ResourceClassId = name;
      pages.Add((PageElement) element4);
    }

    private void CreateMarketingPages(PageElement root)
    {
      ConfigElementDictionary<string, PageElement> pages = root.Pages;
      string name = typeof (PageResources).Name;
      pages.Add(new PageElement()
      {
        Name = "MarketingTools",
        PageId = SiteInitializer.MarketingToolsNodeId,
        UrlName = "MarketingToolsUrlName",
        MenuName = "MarketingToolsTitle",
        Description = "MarketingToolsDescription",
        ResourceClassId = name,
        Ordinal = 1f,
        RenderAsLink = false
      });
    }

    private class PropNames
    {
      internal const string Compilation = "Compilation";
      internal const string IgnoreOutputCacheUsers = "ignoreOutputCacheUsers";
      internal const string EnableBrowseAndEdit = "enableBrowseAndEdit";
    }
  }
}
