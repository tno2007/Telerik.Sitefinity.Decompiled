// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.SiteInitializer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions.VirtualPath.Configuration;
using Telerik.Sitefinity.AppStatus;
using Telerik.Sitefinity.Clients;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Configuration.Web.UI;
using Telerik.Sitefinity.Configuration.Web.UI.Basic;
using Telerik.Sitefinity.ContentLocations.Web.UI;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Configuration;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicTypes.Model;
using Telerik.Sitefinity.Fluent.Modules;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Licensing.Web.UI;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Localization.Web.UI;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.ModuleEditor.Web.Services.Model;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.Comments.Configuration;
using Telerik.Sitefinity.Modules.Files.Web.UI;
using Telerik.Sitefinity.Modules.Libraries.Configuration;
using Telerik.Sitefinity.Modules.Libraries.UserFiles;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Modules.Pages.Web.UI;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Project.Configuration;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Scheduling;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Security.Web.UI;
using Telerik.Sitefinity.Security.Web.UI.Principals;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Notifications;
using Telerik.Sitefinity.Services.Notifications.Configuration;
using Telerik.Sitefinity.Services.Statistics;
using Telerik.Sitefinity.Services.Web.UI;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Upgrades.To5100;
using Telerik.Sitefinity.Utilities.Security;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Versioning;
using Telerik.Sitefinity.Versioning.Model;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.CustomErrorPages;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend;
using Telerik.Sitefinity.Web.UI.ContentUI;
using Telerik.Sitefinity.Web.UI.NavigationControls;
using Telerik.Sitefinity.Web.Utilities;
using Telerik.Sitefinity.Workflow.Data;
using Telerik.Sitefinity.Workflow.UI;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Abstractions
{
  /// <summary>
  /// Creates Sitefinity's default page structures and backend pages.
  /// </summary>
  public class SiteInitializer : IDisposable
  {
    private ModuleUninstallFacade uninstaller;
    private ModuleInstallFacade moduleInstaller;
    private PageTemplate backendTemplate;
    private Dictionary<Guid, int> pageCount;
    private ResourcesConfig resConfig;
    private InstallContext installContext;
    private MetadataManager metadataManager;
    private TaxonomyManager taxonomyManager;
    private PageManager pageManager;
    private VersionManager versionManager;
    private bool suppressSecurityChecks;
    private IDictionary<DataProviderBase, bool> providers = (IDictionary<DataProviderBase, bool>) new Dictionary<DataProviderBase, bool>();
    private readonly List<string> upgradeLogMessages = new List<string>();
    /// <summary>The name of the backend page template.</summary>
    public const string BackendTemplateName = "DefaultBackend";
    /// <summary>The name of the backend html 5 based template.</summary>
    public const string BackendHtml5TemplateName = "BackendHtml5";
    /// <summary>The name of the empty backend page template.</summary>
    public const string BackendTemplateEmptyName = "DefaultBackendEmpty";
    /// <summary>The identifier of the default backend template.</summary>
    public static readonly Guid DefaultBackendTemplateId = new Guid("F669D9A7-009D-4d83-CCCC-000000000001");
    /// <summary>The identified of the backend html 5 template.</summary>
    public static readonly Guid BackendHtml5TemplateId = new Guid("45FEF908-AF79-4568-B950-1D76768177CE");
    /// <summary>The identifier of the empty backend template.</summary>
    public static readonly Guid DefaultBackendTemplateEmptyId = new Guid("E2B5894C-F3F8-49e8-BCA7-ACDFD5352E74");
    /// <summary>
    /// The identifier of the default backend templates category.
    /// </summary>
    public static readonly Guid BackendTemplatesCategoryId = new Guid("F669D9A7-009D-4d83-AABB-000000000003");
    /// <summary>
    /// The identifier of the default custom templates category.
    /// </summary>
    public static readonly Guid CustomTemplatesCategoryId = new Guid("F669D9A7-009D-4d83-AABB-000000000002");
    /// <summary>
    /// The identifier of the basic custom templates category.
    /// </summary>
    public static readonly Guid BasicTemplatesCategoryId = new Guid("F669D9A7-009D-4d83-AABB-000000000001");
    /// <summary>The identifier of the pages taxonomy.</summary>
    public static readonly Guid PagesNodeId = new Guid("F669D9A7-009D-4d83-DDAA-000000000001");
    /// <summary>The identifier of the page templates taxonomy.</summary>
    public static readonly Guid PageTemplatesTaxonomyId = new Guid("C09A8A55-E07F-448e-8412-3458770A6652");
    /// <summary>
    /// The identifier of the backend root pageNode (site node).
    /// </summary>
    [Obsolete("Use CurrentFrontendRootNodeId static property to get the root not ID for the current site (in context of multisite management)")]
    public static readonly Guid FrontendRootNodeId = new Guid("F669D9A7-009D-4d83-DDAA-000000000002");
    /// <summary>The identifier of the "1ColumnHeaderFooter" template.</summary>
    public static readonly Guid TemplateId1ColumnHeaderFooter = new Guid("F669D9A7-009D-4d83-BBBB-000000000001");
    /// <summary>The identifier of the "LeftbarHeaderFooter" template.</summary>
    public static readonly Guid TemplateIdLeftbarHeaderFooter = new Guid("F669D9A7-009D-4d83-BBBB-000000000002");
    /// <summary>
    /// The identifier of the "RightSidebarHeaderFooter" template.
    /// </summary>
    public static readonly Guid TemplateIdRightSidebarHeaderFooter = new Guid("F669D9A7-009D-4d83-BBBB-000000000003");
    /// <summary>The identifier of the "LeftSideBar" template.</summary>
    public static readonly Guid TemplateIdLeftSideBar = new Guid("F669D9A7-009D-4d83-BBBB-000000000004");
    /// <summary>The identifier of the "RightSideBar" template.</summary>
    public static readonly Guid TemplateIdRightSideBar = new Guid("F669D9A7-009D-4d83-BBBB-000000000005");
    /// <summary>The identifier of the "2EqualHeaderFooter" template.</summary>
    public static readonly Guid TemplateId2EqualHeaderFooter = new Guid("F669D9A7-009D-4d83-BBBB-000000000006");
    /// <summary>The identifier of the "3EqualHeaderFooter" template.</summary>
    public static readonly Guid TemplateId3EqualHeaderFooter = new Guid("F669D9A7-009D-4d83-BBBB-000000000007");
    /// <summary>The identifier of the "2Sidebars" template.</summary>
    public static readonly Guid TemplateId2Sidebars = new Guid("F669D9A7-009D-4d83-BBBB-000000000008");
    /// <summary>
    /// The identifier of the "Promo3ColumnsHeaderFooter" template.
    /// </summary>
    public static readonly Guid TemplateIdPromo3ColumnsHeaderFooter = new Guid("F669D9A7-009D-4d83-BBBB-000000000009");
    /// <summary>
    /// The identifier of the backend root pageNode (site node).
    /// </summary>
    public static readonly Guid BackendRootNodeId = new Guid("F669D9A7-009D-4d83-DDAA-000000000003");
    /// <summary>The identifier of the Sitefinity site node.</summary>
    public static readonly Guid SitefinityNodeId = new Guid("F669D9A7-009D-4d83-DDAA-000000000004");
    /// <summary>The identifier of the Content site node.</summary>
    public static readonly Guid ContentNodeId = new Guid("F669D9A7-009D-4d83-DDAA-000000000005");
    /// <summary>The identified of the Ecommerce site node.</summary>
    public static readonly Guid EcommerceNodeId = new Guid("5370FB40-25E4-4B69-B077-90E07EFB3DE3");
    /// <summary>The identifier of the Design site node.</summary>
    public static readonly Guid DesignNodeId = new Guid("F669D9A7-009D-4d83-DDAA-000000000007");
    /// <summary>The identifier of the Templates site node.</summary>
    public static readonly Guid TemplatesNodeId = new Guid("F669D9A7-009D-4d83-DDAA-000000000008");
    /// <summary>The identifier of the Themes site node.</summary>
    public static readonly Guid ThemesNodeId = new Guid("F669D9A7-009D-4d83-DDAA-000000000009");
    /// <summary>The identifier of the Settings site node.</summary>
    public static readonly Guid AdministrationNodeId = new Guid("F669D9A7-009D-4d83-DDAA-000000000010");
    /// <summary>The identifier of the Marketing node.</summary>
    public static readonly Guid MarketingNodeId = new Guid("C5AE5247-3EE8-4A83-AC5F-B9447CECFFFB");
    /// <summary>The identifier of the Marketing tools node.</summary>
    public static readonly Guid MarketingToolsNodeId = new Guid("C5AE5247-3EE8-4A83-AC5F-B9447CECFFFC");
    /// <summary>The identifier of the Modules site node.</summary>
    public static readonly Guid ModulesNodeId = new Guid("F669D9A7-009D-4d83-DDAA-000000000011");
    /// <summary>The identifier of the User Management site node.</summary>
    public static readonly Guid UsersNodeId = new Guid("F669D9A7-009D-4d83-DDAA-000000000012");
    /// <summary>The identifier of the Services site node.</summary>
    public static readonly Guid ServicesNodeId = new Guid("F669D9A7-009D-4d83-DDAA-000000000013");
    /// <summary>The identifier of the System site node.</summary>
    public static readonly Guid SystemNodeId = new Guid("F669D9A7-009D-4d83-DDAA-000000000014");
    /// <summary>The identifier of the Tools site node.</summary>
    public static readonly Guid ToolsNodeId = new Guid("F669D9A7-009D-4d83-DDAA-000000000015");
    /// <summary>The identifier of the Help site node.</summary>
    public static readonly Guid HelpNodeId = new Guid("F669D9A7-009D-4d83-DDAA-000000000016");
    /// <summary>The identifier of the Backend Pages site node.</summary>
    public static readonly Guid BackendPagesNodeId = new Guid("F669D9A7-009D-4d83-DDAA-000000000017");
    /// <summary>
    /// The identifier of the Alternative publishing site node.
    /// </summary>
    public static readonly Guid AlternativePublishingNodeId = new Guid("F669D9A7-009D-4d83-DDAA-000000000018");
    public static readonly Guid SettingsAndConfigurationsNodeId = new Guid("F669D9A7-009D-4d83-DDAA-000000000019");
    public static readonly Guid StagingAndSyncingNodeId = new Guid("F669D9A7-009D-4d83-DDAA-000000000020");
    /// <summary>The identifier of the Settings site node.</summary>
    public static readonly Guid SettingsNodeId = new Guid("F669D9A7-009D-4d83-AAAA-000000000006");
    /// <summary>The identifier of the Basic Settings site node.</summary>
    public static readonly Guid BasicSettingsNodeId = new Guid("F669D9A7-009D-4d83-AAAA-000000000066");
    /// <summary>The identifier of the Advanced Settings site node.</summary>
    public static readonly Guid AdvancedSettingsNodeId = new Guid("F669D9A7-009D-4d83-AAAA-000000000067");
    public static readonly Guid BackendPagesWarningPageId = new Guid("F669D9A7-009D-4d83-AAAA-000000000020");
    public static readonly Guid PageTemplatesNodeId = new Guid("F669D9A7-009D-4d83-AAAA-000000000010");
    public static readonly Guid PageTemplatesNodeId_New = new Guid("CD9D57F6-60C1-4444-9206-53FDE8132D0C");
    public static readonly Guid BackendPagesActualNodeId = new Guid("F669D9A7-009D-4d83-AAAA-000000000013");
    public static readonly Guid BackendPageTemplatesNodeId = new Guid("F669D9A7-009D-4d83-AAAA-000000000014");
    public static readonly Guid HelpAndResourcesNodeId = new Guid("D3494884-836D-431F-97BB-4B63CCF31981");
    public static readonly Guid BackendPageModulesAndServicesNodeId = new Guid("F669D9A7-009D-4d83-AAAA-000000000023");
    internal const string BackendPageModulesAndServicesNodeName = "ModulesAndServices";
    /// <summary>The identifier of the Profile site node</summary>
    public static readonly Guid ProfileNodeId = new Guid("F669D9A7-009D-4d83-AAAA-000000000021");
    /// <summary>The identifier of the Login site node</summary>
    public static readonly Guid LoginNodeId = new Guid("a3e0ba8e-f564-4b2d-8214-d22314f6da2e");
    /// <summary>The identifier of the Login form page</summary>
    public static readonly Guid LoginFormPageId = new Guid("1E55595F-F34B-4B39-B281-24E08A8AF72F");
    /// <summary>The identifier of the Logged Users page</summary>
    public static readonly Guid UserLimitReachedPageId = new Guid("1339E9C0-7DFF-4458-8D9D-E0B40DB12E71");
    /// <summary>The identifier of the Already Logged In Users page</summary>
    public static readonly Guid UserAlreadyLoggedInPageId = new Guid("6B5EF8BD-7D82-456F-B008-0E2291D71594");
    /// <summary>The identifier of the Login failed page</summary>
    public static readonly Guid LoginFailedPageId = new Guid("BA48F1C6-9DD4-4B8D-9DFC-944FEDB9CDEB");
    /// <summary>The identifier of the Need Admin Rights page</summary>
    public static readonly Guid NeedAdminRightsPageId = new Guid("027EBA8B-C35C-466E-AE91-C08E27EC9B28");
    /// <summary>The identifier of the Site not accessible page</summary>
    public static readonly Guid SiteNotAccessiblePageId = new Guid("{25A54809-F0CC-4EE5-A822-5AAB100AB77B}");
    /// <summary>The identifier of the Taxonomy site node</summary>
    public static readonly Guid TaxonomyNodeId = new Guid("22D02484-B564-493a-8734-F1D63AEC8B10");
    /// <summary>The identifier of the Taxonomies site node.</summary>
    public static readonly Guid TaxonomiesNodeId = new Guid("F669D9A7-009D-4d83-DDAA-000000000006");
    /// <summary>The Flat Taxonomy Page Identifier</summary>
    public static readonly Guid FlatTaxonomyPageId = new Guid("9AADC795-AF35-45f7-B544-5C6E19D7DFFD");
    /// <summary>The Departments Taxonomy Page Identifier</summary>
    public static readonly Guid DepartmentsPageId = new Guid("AF127EDA-4CB8-42E4-ACDA-6EFE7CB74A8D");
    /// <summary>The Hierarchical Taxonomy Page Identifier</summary>
    public static readonly Guid HierarchicalTaxonomyPageId = new Guid("92A485CE-3A84-49ba-8B3A-E100061E21C6");
    /// <summary>The Network Taxonomy Page Identifier</summary>
    public static readonly Guid NetworkTaxonomyPageId = new Guid("5A1257A3-D49E-4698-9379-80DC5D931C28");
    /// <summary>The Facet Taxonomy Page Identifier</summary>
    public static readonly Guid FacetTaxonomyPageId = new Guid("CCDAAA6D-ED9B-4bfe-A1C9-C30CA5C7B69D");
    /// <summary>The Marked Items Taxonomy Page Identifier</summary>
    public static readonly Guid MarkedItemsPageId = new Guid("595BE3F0-1B9C-409d-AC17-6316E5CACA79");
    /// <summary>The Hierarchical Taxonomy Url Name</summary>
    public static readonly string HierarchicalTaxonomyUrlName = "Hierarchical";
    /// <summary>The Flat Taxonomy Url Name</summary>
    public static readonly string FlatTaxonomyUrlName = "Flat";
    /// <summary>
    /// The identifier of content locations pages' the group site node
    /// </summary>
    public static readonly Guid ContentLocationsGroupNodePageId = new Guid("EE490775-9E0F-48A6-8EA6-1AAAC19BA637");
    /// <summary>The Content Locations management Page Identifier</summary>
    public static readonly Guid ContentLocationsPageId = new Guid("80F500C2-1A55-4D98-8333-DCB766982A26");
    /// <summary>The name of the Content locations site node</summary>
    internal const string ContentLocationsNodeName = "ContentLocations";
    /// <summary>ID of the page that manages workflows</summary>
    public static readonly Guid WorkflowPageId = new Guid("F669D9A7-009D-4d83-AAAA-000000000011");
    /// <summary>ID of the page that manages backend labels</summary>
    public static readonly Guid LabelsPageId = new Guid("F669D9A7-009D-4d83-AAAA-000000000007");
    /// <summary>ID of the page that shows files</summary>
    public static readonly Guid FilesPageId = new Guid("F669D9A7-009D-4d83-AAAA-000000000008");
    /// <summary>
    /// ID of the page that shows the sitefinity license and version
    /// </summary>
    public static readonly Guid LicensePageId = new Guid("F669D9A7-009D-4d83-AAAA-000000000019");
    /// <summary>ID of the page that manages users</summary>
    public static readonly Guid UsersPageId = new Guid("F669D9A7-009D-4d83-AAAA-000000000003");
    /// <summary>ID of the page that manages roles</summary>
    public static readonly Guid RolesPageId = new Guid("F669D9A7-009D-4d83-AAAA-000000000004");
    /// <summary>ID of the page that manages global permissions</summary>
    public static readonly Guid PermissionsPageId = new Guid("F669D9A7-009D-4d83-AAAA-000000000005");
    /// <summary>ID of the page that manages global permissions</summary>
    public static readonly Guid ProfileTypesPageId = new Guid("F669D9A7-009D-4d83-AAAA-000000000022");
    /// <summary>The ID of the dashboard group page node.</summary>
    public static readonly Guid DashboardPageNodeId = new Guid("F669D9A7-009D-4d83-AAAA-700000000001");
    /// <summary>The name of the dashboard group page node.</summary>
    public const string DashboardPageNodeName = "DashboardGroup";
    /// <summary>Id of the legacy (pre 6.2) dashboard page node.</summary>
    public static readonly Guid LegacyDashboardPageNodeId = new Guid("F669D9A7-009D-4d83-AAAA-000000000001");
    /// <summary>Id of the Connectivity page in Administration</summary>
    public static readonly Guid ConnectivityPageNodeId = new Guid("5FB917D6-8655-4BAE-98F8-7A6DC0703A66");
    /// <summary>Page group taxon for the search module</summary>
    public static readonly Guid SearchPageGroupId = new Guid("C55522BE-9B6B-4F8F-BE68-20FA99BD6D91");
    /// <summary>Id of the Connectivity page in Administration</summary>
    public static readonly Guid DiscussionsPageNodeId = new Guid("9b576176-5652-4496-b08b-fbdeb9a8d811");
    /// <summary>Id for Continuous delivery page</summary>
    public static readonly Guid ContinuousDeliveryPageId = new Guid("c1d985ab-2de4-44a9-81b7-ad22ecf38a7f");
    /// <summary>
    /// The identifier of the basic webforms templates category.
    /// </summary>
    internal static readonly Guid WebFormsBasicTemplatesCategoryId = new Guid("F669D9A7-009D-4d83-AABB-000000000004");
    /// <summary>Identifier for the empty Webforms template.</summary>
    internal static readonly Guid WebFormsEmptyTemplateId = new Guid("F669D9A7-009D-4d83-AABB-000000000005");
    /// <summary>Identifier for the empty Webforms template.</summary>
    internal static readonly Guid HybridEmptyTemplateId = new Guid("F669D9A7-009D-4d83-AABB-000000000006");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Abstractions.SiteInitializer" /> class.
    /// </summary>
    public SiteInitializer()
      : this((string) null, (InstallContext) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Abstractions.SiteInitializer" /> class.
    /// </summary>
    public SiteInitializer(string transactionName)
      : this(transactionName, (InstallContext) null)
    {
    }

    internal SiteInitializer(InstallContext installContext)
      : this((string) null, installContext)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Abstractions.SiteInitializer" /> class.
    /// </summary>
    public SiteInitializer(string transactionName, InstallContext installContext)
    {
      this.Context = installContext;
      if (string.IsNullOrEmpty(transactionName))
        transactionName = "Install";
      this.TransactionName = transactionName;
      this.metadataManager = MetadataManager.GetManager((string) null, this.TransactionName);
      this.RegisterProvider((DataProviderBase) this.metadataManager.Provider);
      this.taxonomyManager = TaxonomyManager.GetManager(this.PagesConfig.PageTaxonomyProvider, this.TransactionName);
      this.RegisterProvider((DataProviderBase) this.taxonomyManager.Provider);
      this.pageManager = PageManager.GetManager((string) null, this.TransactionName);
      this.RegisterProvider((DataProviderBase) this.pageManager.Provider);
      this.versionManager = VersionManager.GetManager((string) null, this.TransactionName);
      this.RegisterProvider((DataProviderBase) this.versionManager.Provider);
    }

    /// <summary>Gets the request static Initializer.</summary>
    /// <returns></returns>
    public static SiteInitializer GetInitializer()
    {
      if (!ObjectFactory.IsTypeRegistered<SiteInitializer>())
        ObjectFactory.Container.RegisterType<SiteInitializer, SiteInitializer>((LifetimeManager) new HttpRequestLifetimeManager(), (InjectionMember) new InjectionConstructor(Array.Empty<object>()));
      return ObjectFactory.Resolve<SiteInitializer>();
    }

    /// <summary>Gets the current frontend root node id.</summary>
    /// <value>The current frontend root node id.</value>
    public static Guid CurrentFrontendRootNodeId
    {
      get
      {
        Telerik.Sitefinity.Multisite.ISite currentSite = SystemManager.CurrentContext.CurrentSite;
        return currentSite != null ? currentSite.SiteMapRootNodeId : SiteInitializer.FrontendRootNodeId;
      }
    }

    /// <summary>Gets the module uninstaller.</summary>
    /// <value>The module.</value>
    public ModuleUninstallFacade Uninstaller
    {
      get
      {
        if (this.uninstaller == null)
          this.uninstaller = App.WorkWith().Module(this.ModuleName).UninstallWithContext((IModuleInstallContext) new ModuleInstallContext(this));
        return this.uninstaller;
      }
    }

    /// <summary>Gets the module installer.</summary>
    /// <value>The module.</value>
    public ModuleInstallFacade Installer
    {
      get
      {
        if (this.moduleInstaller == null)
          this.moduleInstaller = App.WorkWith().Module(this.ModuleName).InstallWithContext((IModuleInstallContext) new ModuleInstallContext(this));
        return this.moduleInstaller;
      }
    }

    /// <summary>
    /// The name of the module installing with this SiteInitializer.
    /// </summary>
    public string ModuleName { get; internal set; }

    /// <summary>
    /// The name of the SiteInitializer distributed transaction.
    /// </summary>
    public string TransactionName { get; private set; }

    /// <summary>Gets the pages configuration.</summary>
    /// <value>The pages config.</value>
    public PagesConfig PagesConfig => Config.Get<PagesConfig>();

    /// <summary>Gets the metadata manager.</summary>
    /// <value>The metadata manager.</value>
    public MetadataManager MetadataManager => this.metadataManager;

    /// <summary>Gets the taxonomy manager.</summary>
    /// <value>The taxonomy manager.</value>
    public TaxonomyManager TaxonomyManager => this.taxonomyManager;

    /// <summary>Gets the page manager.</summary>
    /// <value>The page manager.</value>
    public PageManager PageManager => this.pageManager;

    /// <summary>Gets or sets the version manager.</summary>
    /// <value>The version manager.</value>
    public VersionManager VersionManager => this.versionManager;

    public TManager GetManagerInTransaction<TManager>() where TManager : IManager => this.GetManagerInTransaction<TManager>((string) null);

    /// <summary>Gets the manager in the module installation.</summary>
    /// <typeparam name="TManager">The type of the manager.</typeparam>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns></returns>
    public TManager GetManagerInTransaction<TManager>(string providerName) where TManager : IManager
    {
      TManager managerInTransaction = (TManager) ManagerBase.GetManagerInTransaction(typeof (TManager), providerName, this.TransactionName);
      this.RegisterProvider(managerInTransaction.Provider);
      return managerInTransaction;
    }

    /// <summary>
    /// Gets or sets the context used when installing a module.
    /// </summary>
    /// <value>The context.</value>
    public InstallContext Context
    {
      get
      {
        if (this.installContext == null)
          this.installContext = new InstallContext(this);
        return this.installContext;
      }
      internal set => this.installContext = value;
    }

    /// <summary>Gets the backend template.</summary>
    /// <value>The backend template.</value>
    public virtual PageTemplate BackendTemplate
    {
      get
      {
        if (this.backendTemplate == null)
        {
          this.backendTemplate = this.PageManager.GetTemplates().SingleOrDefault<PageTemplate>((Expression<Func<PageTemplate, bool>>) (t => t.Id == SiteInitializer.DefaultBackendTemplateId));
          if (this.backendTemplate == null)
            this.backendTemplate = new TemplateInitializer(this.PageManager).CreateBackendDefaultTemplate();
        }
        return this.backendTemplate;
      }
    }

    /// <summary>Creates the template controls.</summary>
    /// <param name="template">The template.</param>
    protected virtual void CreateTemplateControls(PageTemplate template)
    {
      MainMenuPanel component1 = new MainMenuPanel();
      component1.ID = "MainMenuPanel";
      component1.ExpandAnimationType = AnimationType.None;
      component1.CollapseAnimationType = AnimationType.None;
      Telerik.Sitefinity.Pages.Model.TemplateControl control1 = this.PageManager.CreateControl<Telerik.Sitefinity.Pages.Model.TemplateControl>(false);
      control1.ObjectType = component1.GetType().FullName;
      control1.PlaceHolder = "Header";
      control1.Caption = "MainMenu";
      control1.Description = "Represents the main menu in Sitefinity's backend.";
      this.PageManager.ReadProperties((object) component1, (ObjectData) control1);
      this.PageManager.SetControlId((IControlsContainer) template, (ObjectData) control1, (CultureInfo) null);
      control1.SetDefaultPermissions((IControlManager) this.PageManager);
      template.Controls.Add(control1);
      Header component2 = new Header();
      Telerik.Sitefinity.Pages.Model.TemplateControl control2 = this.PageManager.CreateControl<Telerik.Sitefinity.Pages.Model.TemplateControl>(false);
      control2.ObjectType = component2.GetType().FullName;
      control2.PlaceHolder = "Header";
      control2.Caption = "Header";
      control2.Description = "Represents the header and the top menu of all backend pages.";
      this.PageManager.ReadProperties((object) component2, (ObjectData) control2);
      this.PageManager.SetControlId((IControlsContainer) template, (ObjectData) control2, (CultureInfo) null);
      control2.SetDefaultPermissions((IControlManager) this.PageManager);
      control2.SiblingId = control1.Id;
      template.Controls.Add(control2);
      Footer component3 = new Footer();
      Telerik.Sitefinity.Pages.Model.TemplateControl control3 = this.PageManager.CreateControl<Telerik.Sitefinity.Pages.Model.TemplateControl>(false);
      control3.ObjectType = component3.GetType().FullName;
      control3.PlaceHolder = "Footer";
      control3.Caption = "Footer";
      control3.Description = "Represents the footer of all backend pages.";
      this.PageManager.ReadProperties((object) component3, (ObjectData) control3);
      this.PageManager.SetControlId((IControlsContainer) template, (ObjectData) control3, (CultureInfo) null);
      control3.SetDefaultPermissions((IControlManager) this.PageManager);
      template.Controls.Add(control3);
    }

    /// <summary>
    /// Creates and installs all default pages, templates, taxonomies and etc.
    /// </summary>
    /// <returns></returns>
    internal void InstallDefaults()
    {
      this.CreateConfigMigrationFakeModule();
      this.SuppressSecurity();
      this.CreateVersionTrunks();
      this.CreatePageTemplates();
      this.CreateFrontendRoot();
      this.CreateBackendRoot();
      this.CreateDashboardGroupPage();
      this.InitializeUserProfiles();
      this.InitializeNavigationControlTemplates();
      this.InitializeLightNavigationControlTemplates();
      this.SaveChanges();
    }

    [UpgradeInfo(Description = "Upgrade backend pages accessible to all in Backend role", UpgradeTo = 1106)]
    private bool UpgradeTo1106()
    {
      int actions = Config.Get<SecurityConfig>().Permissions["Pages"].Actions["View"].Value;
      Guid[] guidArray = new Guid[4]
      {
        SiteInitializer.PagesNodeId,
        SiteInitializer.ContentNodeId,
        SiteInitializer.DesignNodeId,
        SiteInitializer.AdministrationNodeId
      };
      foreach (Guid id in guidArray)
      {
        PageNode pageNode = this.PageManager.GetPageNode(id);
        if (!pageNode.IsGranted("Pages", new Guid[1]
        {
          SecurityManager.BackEndUsersRole.Id
        }, actions))
        {
          if (pageNode.InheritsPermissions)
            this.pageManager.BreakPermiossionsInheritance((ISecuredObject) pageNode);
          Telerik.Sitefinity.Security.Model.Permission permission = this.pageManager.GetPermission("Pages", pageNode.Id, SecurityManager.BackEndUsersRole.Id) ?? this.pageManager.CreatePermission("Pages", pageNode.Id, SecurityManager.BackEndUsersRole.Id);
          permission.GrantActions(true, "View");
          if (!pageNode.Permissions.Contains(permission))
            pageNode.Permissions.Add(permission);
        }
      }
      return true;
    }

    [UpgradeInfo(UpgradeTo = 1700)]
    private bool UpgradeTo1700_FixPublishingPoinItemsContentDynamicLinks()
    {
      foreach (PublishingDataProviderBase dataProviderBase in (Collection<PublishingDataProviderBase>) (ManagerBase<PublishingDataProviderBase>.StaticProvidersCollection ?? PublishingManager.GetManager().StaticProviders))
      {
        if (dataProviderBase.ApplicationName == "/Publishing")
        {
          PublishingManager manager = PublishingManager.GetManager(dataProviderBase.Name, this.TransactionName);
          bool suppressSecurityChecks = manager.Provider.SuppressSecurityChecks;
          try
          {
            manager.Provider.SuppressSecurityChecks = true;
            IQueryable<PublishingPoint> publishingPoints = manager.GetPublishingPoints();
            Expression<Func<PublishingPoint, bool>> predicate = (Expression<Func<PublishingPoint, bool>>) (pp => pp.PublishingPointBusinessObjectName == "Persistent");
            foreach (PublishingPoint publishingPoint in (IEnumerable<PublishingPoint>) publishingPoints.Where<PublishingPoint>(predicate))
            {
              IQueryable<DynamicTypeBase> source = Queryable.Cast<DynamicTypeBase>(manager.GetDynamicTypeManager().GetDataItems((IPublishingPoint) publishingPoint));
              int count = 20;
              int num1 = source.Count<DynamicTypeBase>();
              int num2 = 0;
              if (num1 > 0)
                num2 = (int) Math.Ceiling((double) num1 / (double) count);
              bool flag = false;
              PropertyDescriptor propertyDescriptor = (PropertyDescriptor) null;
              for (int index = 0; index < num2; ++index)
              {
                foreach (DynamicTypeBase component in (IEnumerable<DynamicTypeBase>) source.Skip<DynamicTypeBase>(index * count).Take<DynamicTypeBase>(count))
                {
                  if (propertyDescriptor == null)
                  {
                    propertyDescriptor = TypeDescriptor.GetProperties((object) component).Find("Content", false);
                    if (propertyDescriptor == null || !propertyDescriptor.PropertyType.Equals(typeof (string)))
                    {
                      flag = true;
                      break;
                    }
                  }
                  string html = propertyDescriptor.GetValue((object) component) as string;
                  if (!html.IsNullOrWhitespace())
                    propertyDescriptor.SetValue((object) component, (object) LinkParser.UnresolveLinks(html));
                }
                if (!flag)
                  this.SaveChanges(false);
                else
                  break;
              }
            }
          }
          finally
          {
            manager.Provider.SuppressSecurityChecks = suppressSecurityChecks;
          }
        }
      }
      return true;
    }

    [UpgradeInfo(UpgradeTo = 1107)]
    private bool UpgradeTo1107()
    {
      this.UpdateTo1107_UpdatePages(Config.Get<PagesConfig>().BackendPages.Values);
      return true;
    }

    private void UpdateTo1107_UpdatePages(ICollection<PageElement> backendPagesNodes)
    {
      foreach (PageElement backendPagesNode in (IEnumerable<PageElement>) backendPagesNodes)
      {
        if (!string.IsNullOrEmpty(backendPagesNode.ResourceClassId))
        {
          Guid pageNodeId = backendPagesNode.PageId;
          PageNode pageNode = this.PageManager.GetPageNodes().SingleOrDefault<PageNode>((Expression<Func<PageNode, bool>>) (p => p.Id == pageNodeId));
          if (pageNode != null)
            this.UpdateTo1107_ResetTheNamesFrom(pageNode, backendPagesNode);
        }
        if (backendPagesNode.Pages.Count > 0)
          this.UpdateTo1107_UpdatePages(backendPagesNode.Pages.Values);
      }
    }

    private void UpdateTo1107_ResetTheNamesFrom(PageNode pageNode, PageElement pageConfig)
    {
      string str1 = Res.Get(pageConfig.ResourceClassId, pageConfig.UrlName, CultureInfo.InvariantCulture, false, false);
      if (!string.IsNullOrEmpty(str1) && pageNode.UrlName[CultureInfo.InvariantCulture] == str1)
        pageNode.UrlName.PersistedValue = Res.Expression(pageConfig.ResourceClassId, pageConfig.UrlName);
      string str2 = Res.Get(pageConfig.ResourceClassId, pageConfig.MenuName, CultureInfo.InvariantCulture, false, false);
      if (!string.IsNullOrEmpty(str2) && pageNode.Title == (Lstring) str2)
        pageNode.Title.PersistedValue = Res.Expression(pageConfig.ResourceClassId, pageConfig.MenuName);
      string str3 = Res.Get(pageConfig.ResourceClassId, pageConfig.Description, CultureInfo.InvariantCulture, false, false);
      if (!string.IsNullOrEmpty(str3) && pageNode.Description == (Lstring) str3)
        pageNode.Description.PersistedValue = Res.Expression(pageConfig.ResourceClassId, pageConfig.Description);
      if (!(pageConfig is PageDataElement pageDataElement) || string.IsNullOrEmpty(pageDataElement.HtmlTitle))
        return;
      string str4 = Res.Get(pageConfig.ResourceClassId, pageDataElement.HtmlTitle, CultureInfo.InvariantCulture, false, false);
      PageData pageData = pageNode.GetPageData(CultureInfo.InvariantCulture);
      if (pageData == null || string.IsNullOrEmpty(str4) || !(pageData.HtmlTitle == (Lstring) str4))
        return;
      pageData.HtmlTitle.PersistedValue = Res.Expression(pageConfig.ResourceClassId, pageDataElement.HtmlTitle);
    }

    [UpgradeInfo(UpgradeTo = 1108)]
    private bool UpgradeTo1108()
    {
      foreach (DataProviderSettings provider in (ConfigElementCollection) Config.Get<PagesConfig>().Providers)
      {
        PageManager manager = PageManager.GetManager(provider.Name, this.TransactionName);
        ISecuredObject securityRoot = manager.GetSecurityRoot();
        Telerik.Sitefinity.Security.Model.Permission permission = manager.GetPermission("PageTemplates", securityRoot.Id, SecurityManager.DesignersRole.Id);
        if (permission == null)
        {
          permission = manager.CreatePermission("PageTemplates", securityRoot.Id, SecurityManager.DesignersRole.Id);
          securityRoot.Permissions.Add(permission);
        }
        permission.GrantActions(true, "Create", "Delete", "Modify");
      }
      SecurityManager manager1 = SecurityManager.GetManager((string) null, this.TransactionName);
      SecurityRoot securityRoot1 = manager1.GetSecurityRoot("ApplicationSecurityRoot");
      if (securityRoot1 == null)
        securityRoot1 = manager1.CreateSecurityRoot("ApplicationSecurityRoot", "Backend");
      Telerik.Sitefinity.Security.Model.Permission permission1 = manager1.GetPermission("Backend", securityRoot1.Id, SecurityManager.DesignersRole.Id);
      if (permission1 == null)
      {
        permission1 = manager1.CreatePermission("Backend", securityRoot1.Id, SecurityManager.DesignersRole.Id);
        securityRoot1.Permissions.Add(permission1);
      }
      permission1.GrantActions(true, "ManageWidgets");
      return true;
    }

    [UpgradeInfo(UpgradeTo = 1109)]
    private bool UpgradeTo1109()
    {
      PageTemplate pageTemplate = this.PageManager.GetTemplates().SingleOrDefault<PageTemplate>((Expression<Func<PageTemplate, bool>>) (t => t.Id == SiteInitializer.DefaultBackendTemplateId));
      if (pageTemplate != null && pageTemplate.Controls.Any<Telerik.Sitefinity.Pages.Model.TemplateControl>((Func<Telerik.Sitefinity.Pages.Model.TemplateControl, bool>) (c => c.Caption == "MainMenu")) && pageTemplate.Controls.Any<Telerik.Sitefinity.Pages.Model.TemplateControl>((Func<Telerik.Sitefinity.Pages.Model.TemplateControl, bool>) (c => c.Caption == "Header")))
        pageTemplate.Controls.First<Telerik.Sitefinity.Pages.Model.TemplateControl>((Func<Telerik.Sitefinity.Pages.Model.TemplateControl, bool>) (c => c.Caption == "Header")).SiblingId = pageTemplate.Controls.First<Telerik.Sitefinity.Pages.Model.TemplateControl>((Func<Telerik.Sitefinity.Pages.Model.TemplateControl, bool>) (c => c.Caption == "MainMenu")).Id;
      return true;
    }

    [UpgradeInfo(UpgradeTo = 1136)]
    private bool UpgradeTo1136()
    {
      PageNode pageNode = this.PageManager.GetPageNodes().SingleOrDefault<PageNode>((Expression<Func<PageNode, bool>>) (p => p.Id == SiteInitializer.BackendPageTemplatesNodeId));
      if (pageNode == null)
        return false;
      foreach (ControlData controlData in new List<PageControl>((IEnumerable<PageControl>) pageNode.GetPageData().Controls))
        this.PageManager.Delete(controlData);
      pageNode.GetPageData().Controls.Clear();
      ContentView controlPanel = new ContentView()
      {
        ControlDefinitionName = "BackendPageTemplates"
      };
      pageNode.GetPageData().Controls.Add(this.CreateControlPanel((Control) controlPanel, pageNode.IsBackend));
      return true;
    }

    [UpgradeInfo(UpgradeTo = 1166)]
    private bool UpgradeTo1166()
    {
      this.SuppressSecurity();
      AppSettings currentSettings = AppSettings.CurrentSettings;
      string appName = this.VersionManager.Provider.ApplicationName;
      IQueryable<Change> queryable = SitefinityQuery.Get<Change>((DataProviderBase) this.VersionManager.Provider).Where<Change>((Expression<Func<Change, bool>>) (c => c.ApplicationName == appName));
      if (!currentSettings.Multilingual)
      {
        int lcid = CultureInfo.InvariantCulture.LCID;
        foreach (Change change in (IEnumerable<Change>) queryable)
          change.Culture = lcid;
      }
      else
      {
        int num = 0;
        foreach (Change change in (IEnumerable<Change>) queryable)
          change.Culture = num;
      }
      return true;
    }

    [UpgradeInfo(UpgradeTo = 1215)]
    private bool UpgradeTo1215()
    {
      string dynBaseType = typeof (DynamicTypeBase).FullName;
      IQueryable<MetaType> queryable = this.MetadataManager.GetMetaTypes().Where<MetaType>((Expression<Func<MetaType, bool>>) (mt => mt.BaseClassName == dynBaseType));
      Action<MetaType, string> action = (Action<MetaType, string>) ((dynType, fieldName) =>
      {
        MetaField metaField = dynType.Fields.SingleOrDefault<MetaField>((Func<MetaField, bool>) (f => f.FieldName == fieldName));
        if (metaField == null)
          return;
        metaField.DBSqlType = "NVARCHAR(MAX)";
        metaField.DBType = "LONGVARCHAR";
      });
      foreach (MetaType metaType in (IEnumerable<MetaType>) queryable)
      {
        action(metaType, "Summary");
        action(metaType, "Categories");
      }
      return true;
    }

    [UpgradeInfo(UpgradeTo = 1251)]
    private bool UpgradeTo1251() => this.UpgradeTo1251_LayoutControls();

    private bool UpgradeTo1251_LayoutControls()
    {
      string[] knownTemplates = new string[10]
      {
        "Telerik.Sitefinity.Resources.Templates.Layouts.Column1Template.ascx",
        "Telerik.Sitefinity.Resources.Templates.Layouts.Column2Template1.ascx",
        "Telerik.Sitefinity.Resources.Templates.Layouts.Column2Template2.ascx",
        "Telerik.Sitefinity.Resources.Templates.Layouts.Column2Template3.ascx",
        "Telerik.Sitefinity.Resources.Templates.Layouts.Column2Template4.ascx",
        "Telerik.Sitefinity.Resources.Templates.Layouts.Column2Template5.ascx",
        "Telerik.Sitefinity.Resources.Templates.Layouts.Column3Template1.ascx",
        "Telerik.Sitefinity.Resources.Templates.Layouts.Column3Template2.ascx",
        "Telerik.Sitefinity.Resources.Templates.Layouts.Column4Template1.ascx",
        "Telerik.Sitefinity.Resources.Templates.Layouts.Column5Template1.ascx"
      };
      IQueryable<ControlProperty> properties = this.PageManager.GetProperties();
      Expression<Func<ControlProperty, bool>> predicate = (Expression<Func<ControlProperty, bool>>) (prop => prop.Name == "Layout" && prop.Control.ObjectType == "Telerik.Sitefinity.Web.UI.LayoutControl" && knownTemplates.Contains<string>(prop.Value));
      foreach (ControlProperty controlProperty in properties.Where<ControlProperty>(predicate).ToArray<ControlProperty>())
        controlProperty.Value = ControlUtilities.ToVppPath(controlProperty.Value);
      return true;
    }

    [UpgradeInfo(UpgradeTo = 1270)]
    private bool UpgradeTo1270()
    {
      foreach (DataProviderBase dataProviderBase in (Collection<PageDataProvider>) (ManagerBase<PageDataProvider>.StaticProvidersCollection ?? this.PageManager.StaticProviders))
      {
        PageManager manager = PageManager.GetManager(dataProviderBase.Name, this.TransactionName);
        bool suppressSecurityChecks = manager.Provider.SuppressSecurityChecks;
        manager.Provider.SuppressSecurityChecks = true;
        IQueryable<PageData> pageDataList = manager.GetPageDataList();
        Expression<Func<PageData, bool>> predicate = (Expression<Func<PageData, bool>>) (x => x.OutputCacheProfile == "Default");
        foreach (PageData pageData in (IEnumerable<PageData>) pageDataList.Where<PageData>(predicate))
          pageData.OutputCacheProfile = "Standard Caching";
        manager.Provider.SuppressSecurityChecks = suppressSecurityChecks;
      }
      return true;
    }

    [UpgradeInfo(UpgradeTo = 1299)]
    private bool UpgradeTo1299()
    {
      this.InitializeUserProfiles();
      return true;
    }

    [UpgradeInfo(Description = "Create profile types page and default user profiles", UpgradeTo = 1300)]
    private bool UpgradeTo1300()
    {
      PageElement pageInfo = this.PagesConfig.BackendPages.Values.Where<PageElement>((Func<PageElement, bool>) (p => p.PageId == SiteInitializer.AdministrationNodeId)).Single<PageElement>().Pages.Values.Where<PageElement>((Func<PageElement, bool>) (p => p.PageId == SiteInitializer.UsersNodeId)).Single<PageElement>().Pages.Values.Where<PageElement>((Func<PageElement, bool>) (p => p.PageId == SiteInitializer.ProfileTypesPageId)).Single<PageElement>();
      this.PageManager.Provider.SuppressSecurityChecks = true;
      PageNode pageNode = this.PageManager.GetPageNode(SiteInitializer.UsersNodeId);
      this.CreatePageFromConfiguration(pageInfo, pageNode);
      this.SaveChanges(false);
      string profileTypeName = UserProfilesHelper.GetProfileTypeName(typeof (SitefinityProfile));
      UserProfileManager manager1 = UserProfileManager.GetManager(UserProfilesHelper.GetUserProfilesProvider(profileTypeName), this.TransactionName);
      bool suppressSecurityChecks1 = manager1.Provider.SuppressSecurityChecks;
      manager1.Provider.SuppressSecurityChecks = true;
      UserManager manager2 = UserManager.GetManager((string) null, this.TransactionName);
      bool suppressSecurityChecks2 = manager2.Provider.SuppressSecurityChecks;
      manager2.Provider.SuppressSecurityChecks = true;
      foreach (User user in (IEnumerable<User>) manager2.GetUsers())
      {
        if (manager1.GetUserProfile(user, profileTypeName) == null)
        {
          SitefinityProfile profile = manager1.CreateProfile(user, profileTypeName) as SitefinityProfile;
          profile.About = user.Comment;
          profile.FirstName = user.FirstName;
          profile.LastName = user.LastName;
          manager1.RecompileItemUrls<SitefinityProfile>(profile);
        }
      }
      manager2.Provider.SuppressSecurityChecks = suppressSecurityChecks2;
      manager1.Provider.SuppressSecurityChecks = suppressSecurityChecks1;
      this.SaveChanges(false);
      return false;
    }

    /// <summary>
    /// This upgrade method is to handle cases when people are upgrading 1238...1338, then 1339 (4.1) then something else
    /// </summary>
    [UpgradeInfo(UpgradeTo = 1341)]
    private bool UpgradeTo1341()
    {
      if (this.MetadataManager.GetMetaType(typeof (SitefinityProfile)) != null)
        return false;
      this.InitializeUserProfiles();
      return true;
    }

    /// <summary>
    /// <para>
    /// This upgrades from lastest 4_1_SP2 (as of 2011/07/14) internal build to what is to be 4_1_SP2_Hotfix1
    /// </para>
    /// <para>
    /// Up until this upgrade, no layout control had the 'Drop On' action allowed to admins only.
    /// This upgrade takes care of this thing by hunting down all layout controls and granting
    /// that action to them. New layout controls should be created by ZoneEditor only, which invokes
    /// ControlDataExtensions.SetDefaultPermissions. Any custom code that creates layout controls should
    /// grant those permissions.
    /// </para>
    /// <para>
    /// This case is applicable to sites that are upgraded only. New sites should not be affected by this,
    /// because both SiteInitializer and TemplateInitializer call ControlDataExtensions.SetDefaultPermissions
    /// when creating all of their layout controls.
    /// </para>
    /// </summary>
    [UpgradeInfo(UpgradeTo = 1547)]
    private bool UpgradeTo1547()
    {
      foreach (DataProviderSettings provider in (ConfigElementCollection) Config.Get<PagesConfig>().Providers)
      {
        PageManager manager = PageManager.GetManager(provider.Name);
        manager.Provider.SuppressSecurityChecks = true;
        SecurityConfig securityConfig = Config.Get<SecurityConfig>();
        string setName = "LayoutElement";
        int actionsMask = securityConfig.Permissions[setName].Actions["DropOnLayout"].Value;
        Guid[] principals = new Guid[1]
        {
          securityConfig.ApplicationRoles["BackendUsers"].Id
        };
        List<ObjectData> objectDataList = new List<ObjectData>();
        Telerik.Sitefinity.Pages.Model.TemplateControl[] array1 = ((IEnumerable<Telerik.Sitefinity.Pages.Model.TemplateControl>) manager.GetControls<Telerik.Sitefinity.Pages.Model.TemplateControl>().Where<Telerik.Sitefinity.Pages.Model.TemplateControl>((Expression<Func<Telerik.Sitefinity.Pages.Model.TemplateControl, bool>>) (c => c.IsLayoutControl)).ToArray<Telerik.Sitefinity.Pages.Model.TemplateControl>()).Where<Telerik.Sitefinity.Pages.Model.TemplateControl>((Func<Telerik.Sitefinity.Pages.Model.TemplateControl, bool>) (c => !c.IsGranted(setName, principals, actionsMask))).ToArray<Telerik.Sitefinity.Pages.Model.TemplateControl>();
        objectDataList.AddRange((IEnumerable<ObjectData>) array1);
        TemplateDraftControl[] array2 = ((IEnumerable<TemplateDraftControl>) manager.GetControls<TemplateDraftControl>().Where<TemplateDraftControl>((Expression<Func<TemplateDraftControl, bool>>) (c => c.IsLayoutControl)).ToArray<TemplateDraftControl>()).Where<TemplateDraftControl>((Func<TemplateDraftControl, bool>) (c => !c.IsGranted(setName, principals, actionsMask))).ToArray<TemplateDraftControl>();
        objectDataList.AddRange((IEnumerable<ObjectData>) array2);
        PageControl[] array3 = ((IEnumerable<PageControl>) manager.GetControls<PageControl>().Where<PageControl>((Expression<Func<PageControl, bool>>) (c => c.IsLayoutControl)).ToArray<PageControl>()).Where<PageControl>((Func<PageControl, bool>) (c => !c.IsGranted(setName, principals, actionsMask))).ToArray<PageControl>();
        objectDataList.AddRange((IEnumerable<ObjectData>) array3);
        PageDraftControl[] array4 = ((IEnumerable<PageDraftControl>) manager.GetControls<PageDraftControl>().Where<PageDraftControl>((Expression<Func<PageDraftControl, bool>>) (c => c.IsLayoutControl)).ToArray<PageDraftControl>()).Where<PageDraftControl>((Func<PageDraftControl, bool>) (c => !c.IsGranted(setName, principals, actionsMask))).ToArray<PageDraftControl>();
        objectDataList.AddRange((IEnumerable<ObjectData>) array4);
        foreach (ObjectData objectData in objectDataList)
        {
          ObjectData item = objectData;
          ISecuredObject securedObject = (ISecuredObject) item;
          Telerik.Sitefinity.Security.Model.Permission permission = securedObject.Permissions.Where<Telerik.Sitefinity.Security.Model.Permission>((Func<Telerik.Sitefinity.Security.Model.Permission, bool>) (p => p.SetName == setName && p.PrincipalId == principals[0] && p.ObjectId == item.Id)).SingleOrDefault<Telerik.Sitefinity.Security.Model.Permission>() ?? manager.GetPermission(setName, item.Id, principals[0]) ?? manager.CreatePermission(setName, item.Id, principals[0]);
          permission.GrantActions(true, "DropOnLayout");
          permission.UndenyActions("DropOnLayout");
          if (!securedObject.Permissions.Contains(permission))
            securedObject.Permissions.Add(permission);
        }
        manager.SaveChanges();
      }
      return false;
    }

    [UpgradeInfo(UpgradeTo = 1600)]
    private bool UpgradeBackendPages_AddHelpAndResourcesPage()
    {
      if (this.PageManager.GetPageNodes().SingleOrDefault<PageNode>((Expression<Func<PageNode, bool>>) (p => p.Id == SiteInitializer.HelpAndResourcesNodeId)) == null)
      {
        PageNode parentNode = this.PageManager.GetPageNodes().SingleOrDefault<PageNode>((Expression<Func<PageNode, bool>>) (p => p.Id == SiteInitializer.SystemNodeId));
        if (parentNode != null)
        {
          this.CreatePageFromConfiguration(Config.Get<PagesConfig>().BackendPages.Values.Where<PageElement>((Func<PageElement, bool>) (p => p.PageId == SiteInitializer.AdministrationNodeId)).SingleOrDefault<PageElement>().Pages.Values.Where<PageElement>((Func<PageElement, bool>) (p => p.PageId == SiteInitializer.SystemNodeId)).SingleOrDefault<PageElement>().Pages.Values.Where<PageElement>((Func<PageElement, bool>) (p => p.PageId == SiteInitializer.HelpAndResourcesNodeId)).SingleOrDefault<PageElement>(), parentNode);
          return true;
        }
      }
      return false;
    }

    [UpgradeInfo(UpgradeTo = 1600)]
    private bool UpgradeAnyVersionBefore42_MakeSureOnlyDesignersHaveAccessToPageTemplates()
    {
      PageManager pageManager = this.PageManager;
      PageNode pageNode = pageManager.GetPageNode(SiteInitializer.PageTemplatesNodeId);
      if (pageNode == null || !pageNode.InheritsPermissions)
        return false;
      this.UnsetPermissionsForPageNode(pageNode, pageManager);
      this.SetPermissionsForPageNode(pageNode, new Guid[1]
      {
        SecurityManager.DesignersRole.Id
      }, "Pages", new string[1]{ "View" }, pageManager);
      return true;
    }

    /// <summary>
    /// This upgrade method is to handle lifecycle chnages in v4.2.
    /// </summary>
    [UpgradeInfo(UpgradeTo = 1600)]
    private bool UpgradeLifecycle()
    {
      PageManager pageManager = this.PageManager;
      LifecycleExtensions.UpgradeLiveLifecycleItems<PageData>((ILanguageDataManager) pageManager, pageManager.GetPageDataList());
      LifecycleExtensions.UpgradeLiveLifecycleItems<PageTemplate>((ILanguageDataManager) pageManager, pageManager.GetTemplates());
      return true;
    }

    /// <summary>
    /// This upgrade method is to handle adding the new default templates
    /// </summary>
    [UpgradeInfo(UpgradeTo = 2080)]
    private bool UpgradeTo2080_Adding_the_NewTemplates()
    {
      HierarchicalTaxon taxon = this.TaxonomyManager.GetTaxon<HierarchicalTaxon>(SiteInitializer.BackendTemplatesCategoryId);
      new TemplateInitializer(this.PageManager).InvokeMethod(SiteInitializer.BackendHtml5TemplateId, taxon, (short) 2);
      return true;
    }

    /// <summary>
    /// This upgrade method is to handle adding the new default templates
    /// </summary>
    [UpgradeInfo(UpgradeTo = 2171)]
    private bool UpgradeTo2171RecalculatingCategoryOrdinals()
    {
      foreach (Taxonomy taxonomy in (IEnumerable<HierarchicalTaxonomy>) this.TaxonomyManager.GetTaxonomies<HierarchicalTaxonomy>())
      {
        IList<Taxon> taxa = taxonomy.Taxa;
        if (taxa != null)
        {
          int num = 1;
          foreach (Taxon taxon in (IEnumerable<Taxon>) taxa)
          {
            taxon.Ordinal = (float) num;
            ++num;
          }
          if (num != 1)
            this.SaveChanges(false);
          foreach (HierarchicalTaxon parentTaxon in (IEnumerable<Taxon>) taxa)
            this.EnsureUniqueOrdinalsForChildTaxonesOf(parentTaxon);
        }
      }
      return false;
    }

    [UpgradeInfo(Description = "Updates the permissions list for all workflows in order to configure permissions for all users in the administrator role.", UpgradeTo = 5703)]
    private bool UpgradeWorklowPermissionList()
    {
      try
      {
        WorkflowNewModelUpgradeScript.UpgradeDefaultAdministratorsPermissions_Old80Upgrade();
      }
      catch (Exception ex)
      {
        if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
          throw ex;
        return false;
      }
      return true;
    }

    [UpgradeInfo(UpgradeTo = 5800)]
    private bool UpgradeDecimalCustomFields()
    {
      MetadataManager manager = MetadataManager.GetManager();
      IQueryable<MetaField> metafields = manager.GetMetafields();
      Expression<Func<MetaField, bool>> predicate = (Expression<Func<MetaField, bool>>) (mf => mf.DBType == "DECIMAL" || mf.DBType == "NUMERIC");
      foreach (MetaField metaField in (IEnumerable<MetaField>) metafields.Where<MetaField>(predicate))
        metaField.ClrType = typeof (Decimal?).FullName;
      manager.SaveChanges();
      return true;
    }

    private void EnsureUniqueOrdinalsForChildTaxonesOf(HierarchicalTaxon parentTaxon)
    {
      IList<HierarchicalTaxon> subtaxa = parentTaxon.Subtaxa;
      if (subtaxa == null)
        return;
      int num = 1;
      foreach (Taxon taxon in (IEnumerable<HierarchicalTaxon>) subtaxa)
      {
        taxon.Ordinal = (float) num;
        ++num;
      }
      if (num != 1)
        this.SaveChanges(false);
      foreach (HierarchicalTaxon parentTaxon1 in (IEnumerable<HierarchicalTaxon>) subtaxa)
        this.EnsureUniqueOrdinalsForChildTaxonesOf(parentTaxon1);
    }

    /// <summary>
    /// This upgrade method adds the backend login pages for claims authentication.
    /// </summary>
    [UpgradeInfo(UpgradeTo = 2137)]
    private bool UpgradeTo2137_AddBackendLoginPages()
    {
      if (this.PageManager.GetPageNodes().SingleOrDefault<PageNode>((Expression<Func<PageNode, bool>>) (p => p.Id == SiteInitializer.LoginNodeId)) == null)
      {
        PageNode parentNode = this.PageManager.GetPageNodes().SingleOrDefault<PageNode>((Expression<Func<PageNode, bool>>) (p => p.Id == SiteInitializer.SitefinityNodeId));
        if (parentNode != null)
        {
          PageElement pageInfo1 = Config.Get<PagesConfig>().BackendPages.Values.Where<PageElement>((Func<PageElement, bool>) (p => p.PageId == SiteInitializer.LoginNodeId)).SingleOrDefault<PageElement>();
          PageNode fromConfiguration = this.CreatePageFromConfiguration(pageInfo1, parentNode);
          PageElement pageInfo2 = pageInfo1.Pages.Values.Where<PageElement>((Func<PageElement, bool>) (p => p.PageId == SiteInitializer.LoginFormPageId)).SingleOrDefault<PageElement>();
          PageElement pageInfo3 = pageInfo1.Pages.Values.Where<PageElement>((Func<PageElement, bool>) (p => p.PageId == SiteInitializer.UserLimitReachedPageId)).SingleOrDefault<PageElement>();
          PageElement pageInfo4 = pageInfo1.Pages.Values.Where<PageElement>((Func<PageElement, bool>) (p => p.PageId == SiteInitializer.UserAlreadyLoggedInPageId)).SingleOrDefault<PageElement>();
          PageElement pageInfo5 = pageInfo1.Pages.Values.Where<PageElement>((Func<PageElement, bool>) (p => p.PageId == SiteInitializer.NeedAdminRightsPageId)).SingleOrDefault<PageElement>();
          this.CreatePageFromConfiguration(pageInfo2, fromConfiguration);
          this.CreatePageFromConfiguration(pageInfo3, fromConfiguration);
          this.CreatePageFromConfiguration(pageInfo4, fromConfiguration);
          this.CreatePageFromConfiguration(pageInfo5, fromConfiguration);
          return true;
        }
      }
      return false;
    }

    [UpgradeInfo(UpgradeTo = 2137)]
    private bool UpgradeUserProfiles()
    {
      UserProfileManager managerInTransaction1 = this.GetManagerInTransaction<UserProfileManager>(UserProfilesHelper.GetUserProfilesProvider(UserProfilesHelper.GetProfileTypeName(typeof (SitefinityProfile))));
      using (new ElevatedModeRegion((IManager) managerInTransaction1))
      {
        UserManager managerInTransaction2 = this.GetManagerInTransaction<UserManager>();
        using (new ElevatedModeRegion((IManager) managerInTransaction2))
        {
          foreach (User user in (IEnumerable<User>) managerInTransaction2.GetUsers())
          {
            SitefinityProfile userProfile = managerInTransaction1.GetUserProfile<SitefinityProfile>(user);
            if (userProfile != null)
              userProfile.Nickname = user.UserName;
          }
        }
      }
      return true;
    }

    [UpgradeInfo(UpgradeTo = 1340)]
    private bool UpgradeTo1340()
    {
      this.UpdateControlTemplate("Telerik.Sitefinity.Resources.Templates.Frontend.Security.UserProfileReadView.ascx", typeof (UserProfileDetailReadView), "Article-like");
      this.UpdateControlTemplate("Telerik.Sitefinity.Resources.Templates.Frontend.Security.UserProfileReadListLikeView.ascx", typeof (UserProfileDetailReadView), "List-like");
      this.UpdateControlTemplate("Telerik.Sitefinity.Resources.Templates.Frontend.Security.UserProfileReadAutoGeneratedFieldsView.ascx", typeof (UserProfileDetailReadView), "Auto generated fields");
      this.UpdateControlTemplate("Telerik.Sitefinity.Resources.Templates.Frontend.Security.UserProfileReadViewNoUser.ascx", typeof (UserProfileDetailReadView), "Not logged", friendlyControlName: Res.Get<UserProfilesResources>().ProfileNotLoggedUser);
      this.UpdateControlTemplate("Telerik.Sitefinity.Resources.Templates.Frontend.Security.UserProfileWriteView.ascx", typeof (UserProfileDetailWriteView), "Profile form");
      this.UpdateControlTemplate("Telerik.Sitefinity.Resources.Templates.Frontend.Security.UserProfileWriteAutoGeneratedFieldsView.ascx", typeof (UserProfileDetailWriteView), "Auto generated fields");
      this.UpdateControlTemplate("Telerik.Sitefinity.Resources.Templates.Frontend.Security.UserChangePasswordView.ascx", typeof (UserChangePasswordView), "Default");
      this.UpdateControlTemplate("Telerik.Sitefinity.Resources.Templates.Frontend.Security.UserProfileMasterView.ascx", typeof (UserProfileMasterView), "Names and details");
      this.UpdateControlTemplate("Telerik.Sitefinity.Resources.Templates.Frontend.Security.UserProfileMasterViewNamesOnly.ascx", typeof (UserProfileMasterView), "Names only");
      this.UpdateControlTemplate("Telerik.Sitefinity.Resources.Templates.Frontend.Security.RegistrationFormBasic.ascx", typeof (RegistrationForm), "Basic fields only");
      this.UpdateControlTemplate("Telerik.Sitefinity.Resources.Templates.Frontend.Security.RegistrationForm.ascx", typeof (RegistrationForm), "All fields");
      this.UpdateControlTemplate("Telerik.Sitefinity.Resources.Templates.Frontend.Security.RegistrationSuccessEmail.ascx", typeof (RegistrationForm), "Success email");
      this.UpdateControlTemplate("Telerik.Sitefinity.Resources.Templates.Frontend.Security.RegistrationConfirmationEmail.ascx", typeof (RegistrationForm), "Confirmation email");
      return false;
    }

    private void UpdateControlTemplate(
      string embeddedResourcePath,
      Type type,
      string name,
      string areaName = null,
      string friendlyControlName = null)
    {
      string dataType = "ASP_NET_TEMPLATE";
      string resourceAssemblyName = "Telerik.Sitefinity.Resources";
      string controlType = type.FullName;
      ControlPresentation template = this.PageManager.GetPresentationItems<ControlPresentation>().FirstOrDefault<ControlPresentation>((Expression<Func<ControlPresentation, bool>>) (t => t.DataType == dataType && t.EmbeddedTemplateName == embeddedResourcePath && t.ResourceAssemblyName == resourceAssemblyName && t.Name == name && t.ControlType == controlType));
      if (template == null)
        return;
      SiteInitializer.SetAreaAndFriendlyName(template, type, areaName, friendlyControlName);
    }

    internal static void SetAreaAndFriendlyName(
      ControlPresentation template,
      Type type,
      string areaName,
      string friendlyControlName)
    {
      Type attributeType = typeof (ControlTemplateInfoAttribute);
      if (TypeDescriptor.GetAttributes(type)[attributeType] is ControlTemplateInfoAttribute attribute)
      {
        if (string.IsNullOrEmpty(attribute.ResourceClassId))
        {
          template.AreaName = attribute.AreaName;
          template.FriendlyControlName = attribute.ControlDisplayName;
        }
        else
        {
          template.AreaName = Res.Get(attribute.ResourceClassId, attribute.AreaName);
          template.FriendlyControlName = Res.Get(attribute.ResourceClassId, attribute.ControlDisplayName);
        }
      }
      if (areaName != null)
        template.AreaName = areaName;
      if (friendlyControlName == null)
        return;
      template.FriendlyControlName = friendlyControlName;
    }

    [UpgradeInfo(UpgradeTo = 1362)]
    private bool UpgradeTo1362()
    {
      PageNode pageNode = this.PageManager.GetPageNode(SiteInitializer.FrontendRootNodeId);
      if (pageNode != null)
        this.EnsureUniqueOrdinalsForChildNodesOf(pageNode);
      return false;
    }

    private void EnsureUniqueOrdinalsForChildNodesOf(PageNode parentNode)
    {
      IList<PageNode> nodes = parentNode.Nodes;
      int num = 1;
      foreach (PageNode pageNode in (IEnumerable<PageNode>) nodes)
      {
        pageNode.Ordinal = (float) num;
        ++num;
      }
      this.SaveChanges(false);
      foreach (PageNode parentNode1 in (IEnumerable<PageNode>) nodes)
        this.EnsureUniqueOrdinalsForChildNodesOf(parentNode1);
    }

    private bool UpgradeScript_NotUed_Bug_113582()
    {
      string oldFontendAscx = "<%@ Page AutoEventWireup=\"true\" Language=\"C#\" %>\r\n<%@ Register TagPrefix=\"sf\" Namespace=\"Telerik.Sitefinity.Web.UI\" Assembly=\"Telerik.Sitefinity\" %>\r\n<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head runat=\"server\">\r\n    <meta http-equiv=\"content-type\" content=\"text/html; charset=utf-8\" />\r\n</head>\r\n<body>\r\n    <form id=\"aspnetForm\" runat=\"server\">\r\n        <div class=\"sfPublicWrapper\" id=\"PublicWrapper\" runat=\"server\">\r\n            <sf:SitefinityPlaceHolder ID=\"Body\" runat=\"server\"></sf:SitefinityPlaceHolder>\r\n        </div>\r\n    </form>\r\n</body>\r\n</html>\r\n";
      string str = "<%@ Page AutoEventWireup=\"true\" Language=\"C#\" %>\r\n<%@ Register TagPrefix=\"sf\" Namespace=\"Telerik.Sitefinity.Web.UI\" Assembly=\"Telerik.Sitefinity\" %>\r\n<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head runat=\"server\">\r\n    <meta http-equiv=\"content-type\" content=\"text/html; charset=utf-8\" />\r\n</head>\r\n<body>\r\n    <form id=\"aspnetForm\" runat=\"server\">\r\n        <div class=\"sfPublicWrapper\" id=\"PublicWrapper\" runat=\"server\">\r\n            <sf:SitefinityPlaceHolder ID=\"Body\" runat=\"server\"></sf:SitefinityPlaceHolder>\r\n        </div>\r\n    </form>\r\n</body>\r\n</html>\r\n";
      IQueryable<TemplatePresentation> presentationItems = this.PageManager.GetPresentationItems<TemplatePresentation>();
      Expression<Func<TemplatePresentation, bool>> predicate = (Expression<Func<TemplatePresentation, bool>>) (tp => tp.DataType == "HTML_DOCUMENT" && tp.Data == oldFontendAscx);
      foreach (PresentationData presentationData in presentationItems.Where<TemplatePresentation>(predicate).ToArray<TemplatePresentation>())
        presentationData.Data = str;
      return true;
    }

    [UpgradeInfo(UpgradeTo = 1700)]
    private bool UpgradeTaxonomyUIToUseDefinitions()
    {
      ModuleVersion moduleVersion = MetadataManager.GetManager().GetModuleVersion("Sitefinity");
      int num1 = 0;
      int num2 = 0;
      if (moduleVersion.Version != (Version) null)
        num1 = moduleVersion.Version.Build;
      if (moduleVersion.PreviousVersion != (Version) null)
        num2 = moduleVersion.PreviousVersion.Build;
      if (num2 == 0 && num1 > 1600 || num2 > 1600)
        return true;
      PageData pageData1 = this.PageManager.GetPageNode(SiteInitializer.FlatTaxonomyPageId).GetPageData();
      PageControl ctrl = pageData1.Controls.Where<PageControl>((Func<PageControl, bool>) (c => c.ObjectType == "Telerik.Sitefinity.Taxonomies.Web.UI.Flat.FlatTaxonomyPanel")).FirstOrDefault<PageControl>();
      if (ctrl != null)
      {
        PageControl backendViewControl = this.CreateBackendViewControl(this.PageManager, "FlatTaxonBackend");
        backendViewControl.PlaceHolder = ctrl.PlaceHolder;
        backendViewControl.SiblingId = ctrl.SiblingId;
        PageControl pageControl = pageData1.Controls.Where<PageControl>((Func<PageControl, bool>) (c2 => c2.SiblingId == ctrl.Id)).FirstOrDefault<PageControl>();
        if (pageControl != null)
          pageControl.SiblingId = backendViewControl.Id;
        backendViewControl.SetDefaultPermissions((IControlManager) this.PageManager);
        pageData1.Controls.Remove(ctrl);
        this.PageManager.Delete((ControlData) ctrl);
        pageData1.Controls.Add(backendViewControl);
      }
      PageData pageData2 = this.PageManager.GetPageNode(SiteInitializer.HierarchicalTaxonomyPageId).GetPageData();
      ctrl = pageData2.Controls.Where<PageControl>((Func<PageControl, bool>) (c => c.ObjectType == "Telerik.Sitefinity.Taxonomies.Web.UI.Hierarchical.HierarchicalTaxonomyPanel")).FirstOrDefault<PageControl>();
      if (ctrl != null)
      {
        PageControl backendViewControl = this.CreateBackendViewControl(this.PageManager, "HierarchicalTaxonBackend");
        backendViewControl.PlaceHolder = ctrl.PlaceHolder;
        backendViewControl.SiblingId = ctrl.SiblingId;
        PageControl pageControl = pageData2.Controls.Where<PageControl>((Func<PageControl, bool>) (c2 => c2.SiblingId == ctrl.Id)).FirstOrDefault<PageControl>();
        if (pageControl != null)
          pageControl.SiblingId = backendViewControl.Id;
        backendViewControl.SetDefaultPermissions((IControlManager) this.PageManager);
        pageData2.Controls.Remove(ctrl);
        this.PageManager.Delete((ControlData) ctrl);
        pageData2.Controls.Add(backendViewControl);
      }
      PageData pageData3 = this.PageManager.GetPageNode(SiteInitializer.MarkedItemsPageId).GetPageData();
      ctrl = pageData3.Controls.Where<PageControl>((Func<PageControl, bool>) (c => c.ObjectType == "Telerik.Sitefinity.Taxonomies.Web.UI.MarkedItemsPanel")).FirstOrDefault<PageControl>();
      if (ctrl != null)
      {
        PageControl backendViewControl = this.CreateBackendViewControl(this.PageManager, MarkedItemsDefinitions.Name);
        backendViewControl.PlaceHolder = ctrl.PlaceHolder;
        backendViewControl.SiblingId = ctrl.SiblingId;
        PageControl pageControl = pageData3.Controls.Where<PageControl>((Func<PageControl, bool>) (c2 => c2.SiblingId == ctrl.Id)).FirstOrDefault<PageControl>();
        if (pageControl != null)
          pageControl.SiblingId = backendViewControl.Id;
        backendViewControl.SetDefaultPermissions((IControlManager) this.PageManager);
        pageData3.Controls.Remove(ctrl);
        this.PageManager.Delete((ControlData) ctrl);
        pageData3.Controls.Add(backendViewControl);
      }
      PageData pageData4 = this.PageManager.GetPageNode(SiteInitializer.TaxonomiesNodeId).GetPageData();
      ctrl = pageData4.Controls.Where<PageControl>((Func<PageControl, bool>) (c => c.ObjectType == "Telerik.Sitefinity.Taxonomies.Web.UI.TaxonomiesPanel")).FirstOrDefault<PageControl>();
      if (ctrl != null)
      {
        PageControl backendViewControl = this.CreateBackendViewControl(this.PageManager, "TaxonomyBackend");
        backendViewControl.PlaceHolder = ctrl.PlaceHolder;
        backendViewControl.SiblingId = ctrl.SiblingId;
        PageControl pageControl = pageData4.Controls.Where<PageControl>((Func<PageControl, bool>) (c2 => c2.SiblingId == ctrl.Id)).FirstOrDefault<PageControl>();
        if (pageControl != null)
          pageControl.SiblingId = backendViewControl.Id;
        backendViewControl.SetDefaultPermissions((IControlManager) this.PageManager);
        pageData4.Controls.Remove(ctrl);
        this.PageManager.Delete((ControlData) ctrl);
        pageData4.Controls.Add(backendViewControl);
      }
      return true;
    }

    /// <summary>
    /// Upgrade script for making sure that the taxonomy backend views all use the correct default permissions (visible to everyone).
    /// This may be needed if taxonomy backend views are already set to use definitions (e.g. an upgrade from 4.1.1339 to 4.2.1650), but their default permissions are not correctly.
    /// Such a scenario may occur because the call to SetDefaultPermissions was added after 4.2, and will be added only on direct upgrade to 4.2 SP1.
    /// Otherwise, this fix will be applied when going 4.1 -&gt; 4.2 -&gt; 4.2 SP1, and the taxonomy screens should be visible again for non-admins (e.g. editors).
    /// The method SetDefaultPermissions itself was updated to function correctly in case it's called more than once on a specific control.
    /// </summary>
    /// <returns>Returns true, indicating a successful action</returns>
    [UpgradeInfo(UpgradeTo = 1700)]
    private bool UpgradeTaxonomyUIDefaultPermissions()
    {
      PageControl ctrlData1 = this.PageManager.GetPageNode(SiteInitializer.FlatTaxonomyPageId).GetPageData().Controls.Where<PageControl>((Func<PageControl, bool>) (c => c.ObjectType == "Telerik.Sitefinity.Web.UI.ContentUI.BackendContentView")).FirstOrDefault<PageControl>();
      if (ctrlData1 != null)
        ctrlData1.SetDefaultPermissions((IControlManager) this.PageManager);
      PageControl ctrlData2 = this.PageManager.GetPageNode(SiteInitializer.HierarchicalTaxonomyPageId).GetPageData().Controls.Where<PageControl>((Func<PageControl, bool>) (c => c.ObjectType == "Telerik.Sitefinity.Web.UI.ContentUI.BackendContentView")).FirstOrDefault<PageControl>();
      if (ctrlData2 != null)
        ctrlData2.SetDefaultPermissions((IControlManager) this.PageManager);
      PageControl ctrlData3 = this.PageManager.GetPageNode(SiteInitializer.MarkedItemsPageId).GetPageData().Controls.Where<PageControl>((Func<PageControl, bool>) (c => c.ObjectType == "Telerik.Sitefinity.Web.UI.ContentUI.BackendContentView")).FirstOrDefault<PageControl>();
      if (ctrlData3 != null)
        ctrlData3.SetDefaultPermissions((IControlManager) this.PageManager);
      PageControl ctrlData4 = this.PageManager.GetPageNode(SiteInitializer.TaxonomiesNodeId).GetPageData().Controls.Where<PageControl>((Func<PageControl, bool>) (c => c.ObjectType == "Telerik.Sitefinity.Web.UI.ContentUI.BackendContentView")).FirstOrDefault<PageControl>();
      if (ctrlData4 != null)
        ctrlData4.SetDefaultPermissions((IControlManager) this.PageManager);
      return true;
    }

    [UpgradeInfo(UpgradeTo = 1395)]
    private bool UpgradeTo1395()
    {
      PageNode pageNode = this.PageManager.GetPageNode(SiteInitializer.BackendRootNodeId);
      Guid id = SecurityManager.BackEndUsersRole.Id;
      string setName = "Pages";
      string str = "View";
      this.EnsureObjectIsGrantedPermission((IManager) this.PageManager, (ISecuredObject) pageNode, setName, new Guid[1]
      {
        id
      }, str);
      this.SaveChanges(false);
      this.EnsureObjectIsGrantedPermission((IManager) this.PageManager, (ISecuredObject) this.PageManager.GetPageNode(SiteInitializer.PagesNodeId), setName, new Guid[1]
      {
        id
      }, str);
      return true;
    }

    [UpgradeInfo(UpgradeTo = 2325)]
    private bool UpgradeTo2325()
    {
      this.InitializeNavigationControlTemplates();
      return true;
    }

    [UpgradeInfo(UpgradeTo = 3040)]
    private bool UpgradeModuleNameForBackendPages()
    {
      IQueryable<PageNode> pageNodes = this.PageManager.GetPageNodes();
      Expression<Func<PageNode, bool>> predicate = (Expression<Func<PageNode, bool>>) (p => p.RootNodeId == SiteInitializer.BackendRootNodeId);
      foreach (PageNode pageNode in (IEnumerable<PageNode>) pageNodes.Where<PageNode>(predicate))
      {
        string str;
        if (pageNode.Attributes.TryGetValue("ModuleName", out str))
        {
          if (str == "Orders" || str == "Catalog" || str == "Shipping")
            str = "Ecommerce";
          pageNode.ModuleName = str;
          pageNode.Attributes.Remove("ModuleName");
        }
      }
      return true;
    }

    [UpgradeInfo(UpgradeTo = 3040)]
    private bool FixDecoupledModulesControls()
    {
      PageManager pageManager = this.PageManager;
      this.FixControlTypeAssembly(pageManager, "Telerik.Sitefinity.Modules.News.", "Telerik.Sitefinity", "Telerik.Sitefinity.ContentModules");
      this.FixControlTypeAssembly(pageManager, "Telerik.Sitefinity.Modules.Blogs.", "Telerik.Sitefinity", "Telerik.Sitefinity.ContentModules");
      this.FixControlTypeAssembly(pageManager, "Telerik.Sitefinity.Modules.Events.", "Telerik.Sitefinity", "Telerik.Sitefinity.ContentModules");
      this.FixControlTypeAssembly(pageManager, "Telerik.Sitefinity.Modules.Lists.", "Telerik.Sitefinity", "Telerik.Sitefinity.ContentModules");
      this.FixControlTypeAssembly(pageManager, "Telerik.Sitefinity.Modules.Ecommerce.", "Telerik.Sitefinity", "Telerik.Sitefinity.Ecommerce");
      this.FixControlTypeAssembly(pageManager, "Telerik.Sitefinity.Services.Search.", "Telerik.Sitefinity", "Telerik.Sitefinity.Search.Impl");
      return true;
    }

    [UpgradeInfo(UpgradeTo = 3040)]
    private bool AddModulesAndServicesBackendPage()
    {
      this.CreatePageFromConfiguration(this.PagesConfig.BackendPages.Values.Where<PageElement>((Func<PageElement, bool>) (p => p.PageId == SiteInitializer.AdministrationNodeId)).Single<PageElement>().Pages.Values.Where<PageElement>((Func<PageElement, bool>) (p => p.PageId == SiteInitializer.SystemNodeId)).Single<PageElement>().Pages.Values.Where<PageElement>((Func<PageElement, bool>) (p => p.PageId == SiteInitializer.BackendPageModulesAndServicesNodeId)).Single<PageElement>(), this.PageManager.GetPageNode(SiteInitializer.SystemNodeId), true);
      return true;
    }

    [UpgradeInfo(UpgradeTo = 4000)]
    private bool UpgradeSplitTablesIgnoredCultures()
    {
      CultureInfo[] configuredCultures = MetadataSourceAggregator.GetConfiguredCultures();
      if (configuredCultures.Length <= 1)
        return false;
      this.Context.GetConfig<DataConfig>().DatabaseMappingOptions.SplitTablesIgnoredCultures = string.Join(",", ((IEnumerable<CultureInfo>) configuredCultures).Select<CultureInfo, string>((Func<CultureInfo, string>) (c => c.Name)));
      return true;
    }

    [UpgradeInfo(UpgradeTo = 4701)]
    private bool AddDiscussionsGroupPage()
    {
      this.CreatePageFromConfiguration(this.PagesConfig.BackendPages.Values.Where<PageElement>((Func<PageElement, bool>) (p => p.PageId == SiteInitializer.ContentNodeId)).Single<PageElement>().Pages.Values.Where<PageElement>((Func<PageElement, bool>) (p => p.PageId == SiteInitializer.DiscussionsPageNodeId)).Single<PageElement>(), this.PageManager.GetPageNode(SiteInitializer.ContentNodeId), true);
      return true;
    }

    [UpgradeInfo(UpgradeTo = 3040)]
    [SuppressMessage("Telerik.Sitefinity", "SF1001:AvoidToListOnIQueryable")]
    private bool FixDecoupledModulesCustomTemplates()
    {
      IQueryable<ControlPresentation> presentationItems = this.PageManager.GetPresentationItems<ControlPresentation>();
      Expression<Func<ControlPresentation, bool>> predicate = (Expression<Func<ControlPresentation, bool>>) (cp => cp.ControlType.StartsWith("Telerik.Sitefinity.Modules.Ecommerce."));
      foreach (ControlPresentation controlPresentation in presentationItems.Where<ControlPresentation>(predicate).ToList<ControlPresentation>())
      {
        if (controlPresentation.EmbeddedTemplateName.IsNullOrEmpty() || controlPresentation.IsDifferentFromEmbedded)
        {
          string input = controlPresentation.Data;
          MatchCollection matchCollection = Regex.Matches(input, "<%@\\s*Register[^<]*?Namespace\\s*=\\s*\"Telerik.Sitefinity.Modules.Ecommerce[^\"]*\".*?%>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
          if (matchCollection.Count > 0)
          {
            foreach (Match match in matchCollection)
            {
              string newValue = Regex.Replace(match.Value, "Assembly\\s*=\\s*\"Telerik.Sitefinity\"", "Assembly=\"Telerik.Sitefinity.Ecommerce\"");
              input = input.Replace(match.Value, newValue);
            }
            controlPresentation.Data = input;
          }
        }
      }
      return true;
    }

    [UpgradeInfo(Description = "Clean duplicated and orhaned additional URLs", UpgradeTo = 4913)]
    private bool CleanDuplicateAndOrphanedAdditionalUrls()
    {
      CleanDuplicateAdditionalUrlsTask additionalUrlsTask = new CleanDuplicateAdditionalUrlsTask();
      additionalUrlsTask.Id = Guid.NewGuid();
      additionalUrlsTask.Manager = this.PageManager;
      additionalUrlsTask.ExecuteTask();
      return false;
    }

    [UpgradeInfo(UpgradeTo = 3595)]
    private bool InstallDefaultSiteSettings()
    {
      this.InstallDefaultSiteSettings(true);
      return false;
    }

    private void InstallDefaultSiteSettings(bool upgrade)
    {
      SiteSettings defaultSite = this.Context.GetConfig<ProjectConfig>().DefaultSite;
      defaultSite.Name = "Default";
      defaultSite.Id = Guid.NewGuid();
      if (upgrade)
      {
        if (defaultSite.SiteMapRootNodeId == Guid.Empty)
          defaultSite.SiteMapRootNodeId = SiteInitializer.FrontendRootNodeId;
        defaultSite.HomePageId = Config.Get<PagesConfig>().HomePageId;
      }
      else
        defaultSite.SiteMapRootNodeId = SiteInitializer.FrontendRootNodeId;
      this.Context.SaveConfig<ProjectConfig>();
      SystemManager.CurrentContext.InvalidateCache();
    }

    [UpgradeInfo(UpgradeTo = 3595)]
    private bool UpgradeMainMenuControl()
    {
      PageTemplate template1 = this.PageManager.GetTemplates().Where<PageTemplate>((Expression<Func<PageTemplate, bool>>) (t => t.Id == SiteInitializer.DefaultBackendTemplateId)).SingleOrDefault<PageTemplate>();
      PageTemplate template2 = this.PageManager.GetTemplates().Where<PageTemplate>((Expression<Func<PageTemplate, bool>>) (t => t.Id == SiteInitializer.BackendHtml5TemplateId)).SingleOrDefault<PageTemplate>();
      this.UpgradeMainMenuControl(this.PageManager, template1);
      this.UpgradeMainMenuControl(this.PageManager, template2);
      return true;
    }

    [UpgradeInfo(UpgradeTo = 3595)]
    private bool AddMarketingBackendPage()
    {
      PageElement pageInfo = this.PagesConfig.BackendPages.Values.Where<PageElement>((Func<PageElement, bool>) (p => p.PageId == SiteInitializer.MarketingNodeId)).Single<PageElement>();
      this.PageManager.Provider.SuppressSecurityChecks = true;
      PageNode pageNode = this.PageManager.GetPageNode(SiteInitializer.SitefinityNodeId);
      this.CreatePageFromConfiguration(pageInfo, pageNode, true);
      return true;
    }

    [UpgradeInfo(UpgradeTo = 3710)]
    private bool Upgrade_SetAllowedPageExtensions()
    {
      if (this.PagesConfig.KnownPageExtensions.IsNullOrEmpty())
      {
        string[] array = this.PageManager.GetPageNodes().Where<PageNode>((Expression<Func<PageNode, bool>>) (p => !string.IsNullOrEmpty((string) p.Extension))).Select<PageNode, string>((Expression<Func<PageNode, string>>) (p => (string) p.Extension)).Distinct<string>().ToArray<string>();
        if (array.Length != 0)
        {
          this.Context.GetConfig<PagesConfig>().KnownPageExtensions = string.Join(", ", array);
          this.Context.SaveConfig<PagesConfig>();
        }
      }
      return false;
    }

    private void UpgradeMainMenuControl(PageManager manager, PageTemplate template)
    {
      if (template == null)
        return;
      Telerik.Sitefinity.Pages.Model.TemplateControl templateControl = template.Controls.FirstOrDefault<Telerik.Sitefinity.Pages.Model.TemplateControl>((Func<Telerik.Sitefinity.Pages.Model.TemplateControl, bool>) (c => c.ObjectType == typeof (MainMenu).FullName));
      if (templateControl == null)
        return;
      foreach (ControlProperty controlProperty in templateControl.Properties.ToList<ControlProperty>())
      {
        templateControl.Properties.Remove(controlProperty);
        manager.Delete(controlProperty);
      }
      MainMenuPanel component = new MainMenuPanel();
      component.ID = "MainMenuPanel";
      component.ExpandAnimationType = AnimationType.None;
      component.CollapseAnimationType = AnimationType.None;
      templateControl.ObjectType = typeof (MainMenuPanel).FullName;
      manager.ReadProperties((object) component, (ObjectData) templateControl);
    }

    private void FixControlTypeAssembly(
      PageManager pageManager,
      string typeStartsWith,
      string oldAssemblyName,
      string newAssemblyName)
    {
      try
      {
        this.FixControlTypeAssembly<ControlData>(pageManager, typeStartsWith, oldAssemblyName, newAssemblyName);
      }
      catch
      {
        this.FixControlTypeAssembly<PageControl>(pageManager, typeStartsWith, oldAssemblyName, newAssemblyName);
        this.FixControlTypeAssembly<PageDraftControl>(pageManager, typeStartsWith, oldAssemblyName, newAssemblyName);
        this.FixControlTypeAssembly<Telerik.Sitefinity.Pages.Model.TemplateControl>(pageManager, typeStartsWith, oldAssemblyName, newAssemblyName);
        this.FixControlTypeAssembly<TemplateDraftControl>(pageManager, typeStartsWith, oldAssemblyName, newAssemblyName);
      }
    }

    private void FixControlTypeAssembly<TControlData>(
      PageManager pageManager,
      string typeStartsWith,
      string oldAssemblyName,
      string newAssemblyName)
      where TControlData : ControlData
    {
      IQueryable<TControlData> controls = pageManager.GetControls<TControlData>();
      Expression<Func<TControlData, bool>> predicate = (Expression<Func<TControlData, bool>>) (c => c.ObjectType.StartsWith(typeStartsWith));
      foreach (TControlData controlData in (IEnumerable<TControlData>) controls.Where<TControlData>(predicate))
      {
        string[] strArray = controlData.ObjectType.Split(',');
        if (strArray.Length > 1 && (oldAssemblyName.IsNullOrEmpty() || strArray[1].Trim().Equals(oldAssemblyName)))
          controlData.ObjectType = strArray[0] + ", " + newAssemblyName;
      }
    }

    internal void TryFixTaxonomyStatistics(Type type, string[] providers = null)
    {
      string str = "Scheduling task to syncronize taxonomy statistics for type: {0}".Arrange((object) type.FullName);
      try
      {
        SchedulingManager manager = SchedulingManager.GetManager();
        TaxonomyStatisticsSyncTask task = new TaxonomyStatisticsSyncTask();
        task.Id = Guid.NewGuid();
        task.TypeName = type.FullName;
        task.Providers = providers;
        task.ExecuteTime = DateTime.UtcNow;
        manager.AddTask((ScheduledTask) task);
        manager.SaveChanges();
        Log.Write((object) string.Format("PASSED : {0}", (object) str), ConfigurationPolicy.UpgradeTrace);
      }
      catch (Exception ex)
      {
        Log.Write((object) string.Format("FAILED : {0} - {1}", (object) str, (object) ex.Message), ConfigurationPolicy.UpgradeTrace);
        if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
          throw ex;
      }
    }

    private void EnsureObjectIsGrantedPermission(
      IManager manager,
      ISecuredObject toObject,
      string setName,
      Guid[] principals,
      params string[] actions)
    {
      Telerik.Sitefinity.Security.Configuration.Permission permission1 = Config.Get<SecurityConfig>().Permissions[setName];
      int actions1 = 0;
      foreach (string action1 in actions)
      {
        SecurityAction action2 = permission1.Actions[action1];
        actions1 |= action2.Value;
      }
      if (toObject.IsGranted(setName, principals, actions1))
        return;
      if (toObject.InheritsPermissions)
        manager.BreakPermiossionsInheritance(toObject);
      foreach (Guid principal in principals)
      {
        Telerik.Sitefinity.Security.Model.Permission permission2 = manager.GetPermission(setName, toObject.Id, principal) ?? manager.CreatePermission(setName, toObject.Id, principal);
        permission2.UndenyActions(actions);
        permission2.GrantActions(true, actions);
        if (!toObject.Permissions.Contains(permission2))
          toObject.Permissions.Add(permission2);
      }
    }

    private PageControl CreateBackendViewControl(
      PageManager manager,
      string defiitionName)
    {
      PageControl control = manager.CreateControl<PageControl>(false);
      control.ObjectType = "Telerik.Sitefinity.Web.UI.ContentUI.BackendContentView";
      ControlProperty property = manager.CreateProperty();
      property.Name = "ControlDefinitionName";
      property.Value = defiitionName;
      control.Properties.Add(property);
      return control;
    }

    [UpgradeInfo(Description = "After upgrading, we set the invariant page node extension to all translations", UpgradeTo = 3001)]
    private bool UpgradePageNodeExtensions()
    {
      AppSettings currentSettings = AppSettings.CurrentSettings;
      if (currentSettings.Multilingual)
      {
        foreach (PageNode pageNode in (IEnumerable<PageNode>) this.PageManager.GetPageNodes())
        {
          string str = pageNode.Extension.GetString(CultureInfo.InvariantCulture, false) ?? string.Empty;
          foreach (KeyValuePair<int, CultureInfo> allLanguage in (IEnumerable<KeyValuePair<int, CultureInfo>>) currentSettings.AllLanguages)
            pageNode.Extension.SetString(allLanguage.Value, str);
        }
      }
      return true;
    }

    [UpgradeInfo(UpgradeTo = 3701)]
    private bool UpgradeTo3701()
    {
      ControlPresentation controlPresentation = SiteInitializer.RegisterControlTemplate(this.PageManager, "Telerik.Sitefinity.Resources.Templates.Frontend.Security.UserChangePasswordWidget.ascx", typeof (UserChangePasswordWidget).FullName, "Change password", this.PageManager.Provider.GetNewGuid());
      ProjectConfig projectConfig = Config.Get<ProjectConfig>();
      if (projectConfig.DefaultSite.Id != Guid.Empty)
        this.PageManager.LinkPresentationItemToSite((PresentationData) controlPresentation, projectConfig.DefaultSite.Id);
      return true;
    }

    [UpgradeInfo(UpgradeTo = 3860)]
    private bool FixContentBlockControlsAfterUpgradeTo_5_3()
    {
      try
      {
        SystemManager.InitializeService(Config.Get<SystemConfig>().SystemServices["Statistics"], true);
        ContentWidgetResolver contentWidgetResolver = new ContentWidgetResolver();
        ContentRelationStatisticsClient contentRelationStatistics = new ContentRelationStatisticsClient();
        IQueryable<PageControl> contentBlockData1 = this.PageManager.GetControls<PageControl>().Where<PageControl>((Expression<Func<PageControl, bool>>) (c => c.ObjectType == "Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlock" && c.Page != default (object)));
        this.CreateContentStatistics(contentWidgetResolver, contentRelationStatistics, (IQueryable<ControlData>) contentBlockData1);
        IQueryable<PageDraftControl> contentBlockData2 = this.PageManager.GetControls<PageDraftControl>().Where<PageDraftControl>((Expression<Func<PageDraftControl, bool>>) (c => c.ObjectType == "Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlock" && c.Page != default (object)));
        this.CreateContentStatistics(contentWidgetResolver, contentRelationStatistics, (IQueryable<ControlData>) contentBlockData2);
        return true;
      }
      finally
      {
        ServiceBus.Clear();
      }
    }

    private void CreateContentStatistics(
      ContentWidgetResolver contentWidgetResolver,
      ContentRelationStatisticsClient contentRelationStatistics,
      IQueryable<ControlData> contentBlockData)
    {
      foreach (ControlData widget in (IEnumerable<ControlData>) contentBlockData)
      {
        IContentRelation relationOrNull = contentWidgetResolver.GetRelationOrNull(widget);
        if (relationOrNull != null)
          contentRelationStatistics.UpdateContentRelation(relationOrNull);
      }
    }

    [UpgradeInfo(UpgradeTo = 4100)]
    private bool AddContentLocationsBackendPage()
    {
      PageElement pageInfo = this.PagesConfig.BackendPages.Values.Where<PageElement>((Func<PageElement, bool>) (p => p.PageId == SiteInitializer.ContentNodeId)).Single<PageElement>().Pages.Values.Where<PageElement>((Func<PageElement, bool>) (p => p.PageId == SiteInitializer.ContentLocationsGroupNodePageId)).Single<PageElement>();
      this.PageManager.Provider.SuppressSecurityChecks = true;
      PageNode pageNode = this.PageManager.GetPageNode(SiteInitializer.ContentNodeId);
      this.CreatePageFromConfiguration(pageInfo, pageNode, true);
      return true;
    }

    /// <summary>Upgrades the Administration menu ordering for 6.0.</summary>
    [UpgradeInfo(UpgradeTo = 4100)]
    private bool ReorderAdminMenu_6_0()
    {
      foreach (PageElement pageInfo1 in (IEnumerable<PageElement>) this.PagesConfig.BackendPages.Values.Where<PageElement>((Func<PageElement, bool>) (p => p.PageId == SiteInitializer.AdministrationNodeId)).Single<PageElement>().Pages.Values)
      {
        this.SetPageFromConfiguration(pageInfo1, Guid.Empty);
        foreach (PageElement pageInfo2 in (IEnumerable<PageElement>) pageInfo1.Pages.Values)
          this.SetPageFromConfiguration(pageInfo2, pageInfo1.PageId);
      }
      PageElement pageInfo3 = new PageElement()
      {
        ResourceClassId = "SiteSyncResources",
        UrlName = "SyncUrlName",
        MenuName = "ModuleTitle",
        Description = "SyncDescription"
      };
      PageElement pageInfo4 = new PageElement()
      {
        ResourceClassId = typeof (PageResources).Name,
        MenuName = "AlternativePublishingTitle",
        UrlName = "AlternativePublishingUrlName",
        Description = "AlternativePublishingDescription"
      };
      PageElement pageInfo5 = new PageElement()
      {
        ResourceClassId = typeof (UserFilesResources).Name,
        MenuName = "UserFilesModuleTitle",
        UrlName = "UserFileModuleUrlName",
        Description = "UserFilesModuleDescription"
      };
      PageElement pageInfo6 = new PageElement()
      {
        ResourceClassId = "SearchResources",
        MenuName = "ModuleTitle",
        UrlName = "SearchUrlName",
        Description = "PageGroupNodeDescription"
      };
      this.SetPageFromModule(SiteInitializer.SearchPageGroupId, SiteInitializer.SettingsAndConfigurationsNodeId, 2f, pageInfo6);
      this.SetPageFromModule(UserFilesConstants.UserFilesNodeId, SiteInitializer.SettingsAndConfigurationsNodeId, 3f, pageInfo5);
      this.SetPageFromModule(ModuleBuilderModule.moduleBuilderNodeId, SiteInitializer.ToolsNodeId, 1.5f);
      this.SetPageFromModule(PublishingModule.PublishingPageGroupId, SiteInitializer.ToolsNodeId, 4f, pageInfo4);
      this.SetPageFromModule(SiteInitializer.StagingAndSyncingNodeId, SiteInitializer.ToolsNodeId, 5f, pageInfo3);
      return true;
    }

    [UpgradeInfo(UpgradeTo = 4200)]
    private bool UpgradeBackendTemplatesRemoveIE8Compatibility()
    {
      Type resourcesAssemblyInfo = Config.Get<ControlsConfig>().ResourcesAssemblyInfo;
      this.UpdateHtmlDocumentPresentationData(this.PageManager.GetTemplates().Where<PageTemplate>((Expression<Func<PageTemplate, bool>>) (t => t.Id == SiteInitializer.DefaultBackendTemplateId)).SingleOrDefault<PageTemplate>(), "Telerik.Sitefinity.Resources.Pages.Backend.master", resourcesAssemblyInfo);
      this.UpdateHtmlDocumentPresentationData(this.PageManager.GetTemplates().Where<PageTemplate>((Expression<Func<PageTemplate, bool>>) (t => t.Id == SiteInitializer.BackendHtml5TemplateId)).SingleOrDefault<PageTemplate>(), "Telerik.Sitefinity.Resources.Pages.BackendHtml5.master", resourcesAssemblyInfo);
      return true;
    }

    private void UpdateHtmlDocumentPresentationData(
      PageTemplate template,
      string resourceName,
      Type assemblyInfo)
    {
      template.Presentation.Where<TemplatePresentation>((Func<TemplatePresentation, bool>) (p => p.DataType == "HTML_DOCUMENT")).SingleOrDefault<TemplatePresentation>().Data = ControlUtilities.GetTextResource(resourceName, assemblyInfo);
    }

    [UpgradeInfo(UpgradeTo = 4300)]
    private bool Upgrade_InitializeNavigationWidgetTemplates()
    {
      ControlPresentation controlPresentation1 = SiteInitializer.RegisterControlTemplate(this.PageManager, "Telerik.Sitefinity.Resources.Templates.Frontend.Navigation.HorizontalList.ascx", typeof (LightNavigationControl).FullName, "Horizontal (one-level)", this.PageManager.Provider.GetNewGuid());
      ControlPresentation controlPresentation2 = SiteInitializer.RegisterControlTemplate(this.PageManager, "Telerik.Sitefinity.Resources.Templates.Frontend.Navigation.HorizontalWithDropDownMenusList.ascx", typeof (LightNavigationControl).FullName, "Horizontal with dropdown menus", this.PageManager.Provider.GetNewGuid());
      ControlPresentation controlPresentation3 = SiteInitializer.RegisterControlTemplate(this.PageManager, "Telerik.Sitefinity.Resources.Templates.Frontend.Navigation.HorizontalWithTabsList.ascx", typeof (LightNavigationControl).FullName, "Horizontal with tabs (up to 2 levels)", this.PageManager.Provider.GetNewGuid());
      ControlPresentation controlPresentation4 = SiteInitializer.RegisterControlTemplate(this.PageManager, "Telerik.Sitefinity.Resources.Templates.Frontend.Navigation.VerticalList.ascx", typeof (LightNavigationControl).FullName, "Vertical (one-level)", this.PageManager.Provider.GetNewGuid());
      ControlPresentation controlPresentation5 = SiteInitializer.RegisterControlTemplate(this.PageManager, "Telerik.Sitefinity.Resources.Templates.Frontend.Navigation.VerticalWithSubLevelsList.ascx", typeof (LightNavigationControl).FullName, "Vertical with sublevels (treeview)", this.PageManager.Provider.GetNewGuid());
      ControlPresentation controlPresentation6 = SiteInitializer.RegisterControlTemplate(this.PageManager, "Telerik.Sitefinity.Resources.Templates.Frontend.Navigation.SitemapInColumnsList.ascx", typeof (LightNavigationControl).FullName, "Sitemap in columns (up to 2 levels)", this.PageManager.Provider.GetNewGuid());
      ControlPresentation controlPresentation7 = SiteInitializer.RegisterControlTemplate(this.PageManager, "Telerik.Sitefinity.Resources.Templates.Frontend.Navigation.SitemapInRowsList.ascx", typeof (LightNavigationControl).FullName, "Sitemap in rows (up to 2 levels)", this.PageManager.Provider.GetNewGuid());
      ControlPresentation controlPresentation8 = SiteInitializer.RegisterControlTemplate(this.PageManager, "Telerik.Sitefinity.Resources.Templates.Frontend.Navigation.ToggleMenu.ascx", typeof (NavTransformationTemplate).FullName, "Toggle menu", this.PageManager.Provider.GetNewGuid());
      controlPresentation8.NameForDevelopers = "ToggleMenu";
      ControlPresentation controlPresentation9 = SiteInitializer.RegisterControlTemplate(this.PageManager, "Telerik.Sitefinity.Resources.Templates.Frontend.Navigation.Dropdown.ascx", typeof (NavTransformationTemplate).FullName, "Dropdown", this.PageManager.Provider.GetNewGuid());
      controlPresentation9.NameForDevelopers = "Dropdown";
      ProjectConfig projectConfig = Config.Get<ProjectConfig>();
      if (projectConfig.DefaultSite.Id != Guid.Empty)
      {
        this.PageManager.LinkPresentationItemToSite((PresentationData) controlPresentation1, projectConfig.DefaultSite.Id);
        this.PageManager.LinkPresentationItemToSite((PresentationData) controlPresentation2, projectConfig.DefaultSite.Id);
        this.PageManager.LinkPresentationItemToSite((PresentationData) controlPresentation3, projectConfig.DefaultSite.Id);
        this.PageManager.LinkPresentationItemToSite((PresentationData) controlPresentation4, projectConfig.DefaultSite.Id);
        this.PageManager.LinkPresentationItemToSite((PresentationData) controlPresentation5, projectConfig.DefaultSite.Id);
        this.PageManager.LinkPresentationItemToSite((PresentationData) controlPresentation6, projectConfig.DefaultSite.Id);
        this.PageManager.LinkPresentationItemToSite((PresentationData) controlPresentation7, projectConfig.DefaultSite.Id);
        this.PageManager.LinkPresentationItemToSite((PresentationData) controlPresentation8, projectConfig.DefaultSite.Id);
        this.PageManager.LinkPresentationItemToSite((PresentationData) controlPresentation9, projectConfig.DefaultSite.Id);
      }
      return true;
    }

    [UpgradeInfo(UpgradeTo = 4900)]
    private bool AddDashboardGroupPage()
    {
      this.CreateDashboardGroupPage();
      return true;
    }

    [UpgradeInfo(UpgradeTo = 5100)]
    private bool UpgradeMarketingPageNode()
    {
      PageElement pageInfo = this.PagesConfig.BackendPages.Values.Single<PageElement>((Func<PageElement, bool>) (p => p.PageId == SiteInitializer.MarketingNodeId)).Pages.Values.Single<PageElement>((Func<PageElement, bool>) (p => p.PageId == SiteInitializer.MarketingToolsNodeId));
      this.PageManager.Provider.SuppressSecurityChecks = true;
      PageNode pageNode = this.PageManager.GetPageNode(SiteInitializer.MarketingNodeId);
      this.CreatePageFromConfiguration(pageInfo, pageNode, true);
      return true;
    }

    [UpgradeInfo(Description = "Migrates Split Page Nodes into one Page Node with separate PageData for each translation", UpgradeTo = 5100)]
    private bool MigrateSplitPages() => new PageMigrator().UpgradePages();

    [UpgradeInfo(Description = "Upgrades taxonomy field control tag to use the new front-end widget template.", UpgradeTo = 5400)]
    private bool UpgradeTaxonomyFieldControlTags()
    {
      IQueryable<MetaField> source = this.MetadataManager.GetMetafields().Where<MetaField>((Expression<Func<MetaField, bool>>) (f => (Guid?) f.TaxonomyId != new Guid?() && f.TaxonomyId != Guid.Empty));
      if (source.Count<MetaField>() == 0)
        return false;
      foreach (MetaField metaField in (IEnumerable<MetaField>) source)
      {
        ITaxonomy taxonomy = this.TaxonomyManager.GetTaxonomy(metaField.TaxonomyId);
        MetaFieldAttribute metaFieldAttribute = metaField.MetaAttributes.SingleOrDefault<MetaFieldAttribute>((Func<MetaFieldAttribute, bool>) (attr => attr.Name == DynamicAttributeNames.ControlTag));
        if (metaFieldAttribute != null)
        {
          string fieldControlTemplate = TaxonomyManager.GetTaxonomyFieldControlTemplate(metaField.FieldName, taxonomy);
          metaFieldAttribute.Value = fieldControlTemplate;
        }
      }
      return true;
    }

    [UpgradeInfo(Description = "Sets the azure identifier limitation policy for projects prior to 7.3.5614", UpgradeTo = 5614)]
    private bool SetAzureIdentifierGeneration()
    {
      try
      {
        ConfigManager manager = ConfigManager.GetManager();
        DataConfig section = manager.GetSection<DataConfig>();
        section.DatabaseMappingOptions.AzureOptions.UseMsSqlIdentifierLimitations = false;
        manager.SaveSection((ConfigSection) section);
      }
      catch (Exception ex)
      {
        if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
          throw ex;
        return false;
      }
      return true;
    }

    [UpgradeInfo(UpgradeTo = 5800)]
    private bool RegisterChangeQuestionAndAnswerTemplate()
    {
      this.RegisterControlTemplate("Telerik.Sitefinity.Resources.Templates.Frontend.Security.UserChangePasswordQuestionAndAnswerView.ascx", typeof (UserChangePasswordQuestionAndAnswerView).FullName, "Default", "changeQuestionAndAnswerMode");
      return true;
    }

    [UpgradeInfo(UpgradeTo = 5800)]
    private bool UpgradePresentationData()
    {
      string oldFrontendBody = "\r\n<body>\r\n    <form id=\"aspnetForm\" runat=\"server\">\r\n        <div class=\"sfPublicWrapper\" id=\"PublicWrapper\" runat=\"server\">\r\n            <sf:SitefinityPlaceHolder ID=\"Body\" runat=\"server\"></sf:SitefinityPlaceHolder>\r\n        </div>\r\n    </form>\r\n</body>";
      Type resourcesAssemblyInfo = Config.Get<ControlsConfig>().ResourcesAssemblyInfo;
      IQueryable<TemplatePresentation> presentationItems = this.PageManager.GetPresentationItems<TemplatePresentation>();
      Expression<Func<TemplatePresentation, bool>> predicate = (Expression<Func<TemplatePresentation, bool>>) (tp => tp.DataType == "HTML_DOCUMENT" && tp.Data.Contains(oldFrontendBody));
      foreach (PresentationData presentationData in presentationItems.Where<TemplatePresentation>(predicate).ToArray<TemplatePresentation>())
        presentationData.Data = ControlUtilities.GetTextResource("Telerik.Sitefinity.Resources.Pages.Frontend.aspx", resourcesAssemblyInfo);
      return true;
    }

    [UpgradeInfo(Description = "Cleans the SystemConfig from the depricated properties.", UpgradeTo = 6000)]
    private bool UpgradeSystemConfig()
    {
      if (this.metadataManager.GetModuleVersions().Count<ModuleVersion>() > 0)
      {
        SystemConfig config = this.Context.GetConfig<SystemConfig>();
        config.Build = 0;
        config.PreviousBuild = 0;
        foreach (ModuleSettings applicationModule in (ConfigElementCollection) config.ApplicationModules)
        {
          applicationModule.Version = (Version) null;
          applicationModule.ErrorMessage = string.Empty;
        }
      }
      return true;
    }

    [UpgradeInfo(Description = "Removing Sitefinity logo from appStatus page", FailMassage = "Failed to remove Sitefinity logo from appStatus page", Id = "E8C33739-EB6C-47CA-B492-1E857703F80C", UpgradeTo = 6100)]
    private bool RemoveSitefinityLogo()
    {
      string path = HttpContext.Current.Server.MapPath("~/App_Data/appStatusReport.html");
      if (File.Exists(path))
      {
        string oldValue = "<img class=\"logo\" src=\"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAH8AAAB4CAYAAADbuC2rAAAABHNCSVQICAgIfAhkiAAAAAlwSFlzAAALEgAACxIB0t1+/AAAABZ0RVh0Q3JlYXRpb24gVGltZQAwOS8xOC8xNRrgZiIAAAAcdEVYdFNvZnR3YXJlAEFkb2JlIEZpcmV3b3JrcyBDUzVxteM2AAAGwElEQVR4nO2dzXEbORBGn5yAFIIzsDaC5ZkXayOQyAgsJrC7CbDkBGgpApcO4tXcDOwMtBlYEXAPmnaNJP7MD7rRDexX5YMlmwD43mBmMACG7XZLrj+z64dJzvIzt/1sdv1wnrMO78iU+WJ9BXybL9a3ueqQK/PF+gzYAJv5Yn2eqx5Z4DfgvzR/vaxJgBb4D8ApGQUwh/8KvKQKAV6Bl4gAE+v6mMLfA15StAB7wEtOeT4FXlnWyQz+EfCSIgU4Ar6dL5YCmMDvCF5SlAA9wEvMBFCH3xO8pAgBBoCXmAhwst1u1T68uYj5NuIj7lbL6VWa2thmBPh2Zqvl9DZJhXZEDX5z+7Lh+WJmTH4Ak9Vy+nN0pYySCLxETQCVbj8heHj+AjfNF+o+icGD4ikgOfzE4CUhBGi1PRV4iYoASbt9JfDtuD0FGLQdEp8Ckh35Ro132QMYtR0S9wBJ4Bs2HpwJYNx2SCjAaPgZGg9OBMjUdkgkwCj4GRsPmQXI3HZIIMBg+A4aD5kEcNJ2eBbgr6H/edDVvqPGS8zuAhy2HQaOhPY+8p023qQHcNp2GPgspBd8x40HZQGctx0GCNC522++1Ef8Nl6S/BQQAHw7nU8BnY781nh1hMYn7QGCgYcePcBR+AoPKizyAXgcOzEyIHhJJwEOwg8KXjJqZmxg8JKjAuyFbwT+B3Cn+PmDBJgv1hfYgL8HnhQ//6AAO+Ebgp80FyduBGhGzb6iD/5utZxeAO95/i60sleAN/CNwf8E8CLAwPmGQ/K3XJE338GEDAK8uNXLAb6dpoKXimU/NWV/31H2FTbgdz6TN/ruX9wG/oKfG7xkvlhvgN8V6/BGgNzgW/UwFeCdYaFdB18u0O0CX5wCvIAH+1PAyez6wRN4wEzGJ+AG+FOxDEmv6VdG7Z9ZrNi5p+dwq9ERcIpD8GDS/rvVcnp7st1uNU0btegi+CATjJxwqdT+X73wO3hh2j8JCxm92saoB9DK6Jm2rfanug1+cfp981Qv0e1W0mVWAXuA5KtsEnB5c92185HuyIJU1tcFEWDvOEKKjODyBJyvltPH9g93XvCNGHFTW1jZ6gL/1fj8BFEFD4O5SL0eX/9i79X+gILUV9Q2Alyg+zBkSNTBS3pyOVivg7d6PQoyW0rdNGSCHwHMwEs6cjlar6P3+R0K+my9ht6RAObgJUe4dKpXp0GepqDrHb+arZbTT10+I3UcCJANvGSPAJ3r1XmEb7Wc3gCz1o9Ud43okowCZAcveXVg9qpX70UbzVYr73ODb8d4ypUb8O00D6i+96mX6p48lkmw/0/X3DczcMIn2967KdMMAN0YFfexhJ3CoAD4mUb+itgqLjT8zEO+4QUIC9/JWH9oAULCdwJeElaAcPCdgZeEFCAUfKfgJeEECAPfOXhJKAFCwA8CXhJGAPfwg4GXhBDANfyg4CXuBXALPzh4yaXVWzOGxCV8Q/AWj4JN35vTJ+7gG4K/Q39tvMSlAK4e6VqCb089M1gaLsk+AaYdN0d+LvBgsjmExFUP4AJ+TvCSGgXIDt8DeEltAmSF7wm8pCYBcr5C3R14SS0C5HqFulvwkhoEyPEKdffgJaULYP0K9TDgJQdWK6WOuQBmgzwRwbfjadeuVDGBHx28xFCA3yxWBFm8Qr0I8ADNETk79u8SZPBu4X2iCr8k8BIjAUZtF981avBLBC8pRQCtV6gXC15SggAar1AvHrwkugAaR/6GCsBLGgH+QHdWkIoAGvBdbVpglDMCvosnOXyjIVE3M2ON7v1VdgNRG+QxmhqVtfs32g5GbRsYtVu90nuA6OBBeZCnVAFKAA8Gw7ulCVAKeDB6pFuKACWBB8Pn+dEFKA08GE/miCpAieAhwzSuaAKUCh4yTeCMIkDznOKWAsFDxqnb3gUwfLdftn18sy7a8CpADeDBwXItbwLUAh4cwAc/AtQEHpzAh/wCGE5C+eQBPDiCD/kEMAT//+YMh2ItQK3gwdm2LO1YzQfgeRbOR+Vy3IEHx/DBdK8czbgEDw67/XYMV8lqxS14cA4fQgvgGjwEgA8hBXAPHoLAh1AChAAPgeBDCAHCgIdg8MG1AKHAQ0D44FKAcOAhKHxwJUBI8BAYPrgQICx4CA4fsgoQGjwUAB+yCBAePBQCH0wFKAI8FAQfTAQoBjwUBh9UBfhcEnhw/kh3TBI/DnazDUzKFHfkSxL2AEWCh4LhQxIBigUPhcOHUQIUDR4qgA+DBCgePFQCH3oJUAV4qAg+dBKgGvBQGXw4KEBV4KFC+LBTgOrAQ8GDPF0iq3ZqBA/wHzHELaQRZMyEAAAAAElFTkSuQmCC\" alt=\"logo.png\" />";
        string str = File.ReadAllText(path);
        if (str.Contains(oldValue))
        {
          string contents = str.Replace(oldValue, "");
          File.WriteAllText(path, contents);
        }
      }
      return true;
    }

    [UpgradeInfo(Description = "Clearing Page Templates History", FailMassage = "Failed to clear Page Templates History", Id = "1b5e1592-1002-4d90-997d-b6ce4a2137e6", UpgradeTo = 6200)]
    private bool RemovePageTemplateHistory()
    {
      VersionManager versionManager = this.VersionManager;
      int num1 = 0;
      int num2 = 100;
      IQueryable<Change> source = versionManager.GetChanges().Where<Change>((Expression<Func<Change, bool>>) (c => c.Parent.TypeName == typeof (TemplateDraft).FullName));
      Expression<Func<Change, Guid>> selector = (Expression<Func<Change, Guid>>) (i => i.Id);
      foreach (Guid changeId in source.Select<Change, Guid>(selector).ToList<Guid>())
      {
        versionManager.DeleteChange(changeId);
        if ((num1 + 1) % num2 == 0)
          this.FlushChanges();
        ++num1;
      }
      return true;
    }

    [UpgradeInfo(Description = "Setting template_id column with template's base template id for draft templates", FailMassage = "Failed to set template_id column for draft templates", Id = "9ae2049a-3a6e-429f-ab9b-4180cd01b1e9", UpgradeTo = 6200)]
    private bool SetParentTemplateIdForDraftTemplates()
    {
      PageManager pageManager = this.PageManager;
      foreach (PageTemplate pageTemplate in pageManager.GetTemplates().ToList<PageTemplate>())
      {
        PageTemplate template = pageTemplate;
        if (template.ParentTemplate != null && template.ParentTemplate.Id != Guid.Empty)
        {
          IQueryable<TemplateDraft> drafts = pageManager.GetDrafts<TemplateDraft>();
          Expression<Func<TemplateDraft, bool>> predicate = (Expression<Func<TemplateDraft, bool>>) (d => d.ParentTemplate.Id == template.Id);
          foreach (TemplateDraft templateDraft in drafts.Where<TemplateDraft>(predicate).ToList<TemplateDraft>())
            templateDraft.TemplateId = template.ParentTemplate.Id;
        }
      }
      return true;
    }

    [UpgradeInfo(Description = "Creating a config migration fake module in mode other than AUTO.", FailMassage = "Failed to create a config migration fake module in mode other than AUTO.", Id = "5c949fdc-1864-4adc-a70a-fe1cfffb4e29", UpgradeTo = 6200)]
    private bool CreateConfigMigrationFakeModule()
    {
      if (Config.ConfigStorageMode == ConfigStorageMode.FileSystem)
        this.MetadataManager.CreateModuleVersion("f2984670-c099-4157-9fad-f6915db28ad6").VersionString = "1.0";
      return true;
    }

    [UpgradeInfo(Description = "Creating a login failed page.", FailMassage = "Failed to create a login failed page.", Id = "54299B23-B77D-4BCB-AA5F-3270DAFCDD7A", UpgradeTo = 6400)]
    private bool CreateLoginFailedPage()
    {
      this.CreatePageFromConfiguration(Config.Get<PagesConfig>().BackendPages.Values.Where<PageElement>((Func<PageElement, bool>) (p => p.PageId == SiteInitializer.LoginNodeId)).SingleOrDefault<PageElement>().Pages.Values.Where<PageElement>((Func<PageElement, bool>) (p => p.PageId == SiteInitializer.LoginFailedPageId)).SingleOrDefault<PageElement>(), this.PageManager.GetPageNodes().Where<PageNode>((Expression<Func<PageNode, bool>>) (p => p.Id == SiteInitializer.LoginNodeId)).First<PageNode>());
      return true;
    }

    [UpgradeInfo(Description = "Creating root permissions inheritance for page templates", FailMassage = "Failed to create root permissions inheritance for page templates.", Id = "DC22D06F-F17E-4333-BA8D-F08529D6E9A8", UpgradeTo = 6700)]
    private void SetRootPermissionsInheritanceForPageTemplates()
    {
      PageManager managerInTransaction = this.GetManagerInTransaction<PageManager>();
      ISecuredObject securityRoot = managerInTransaction.GetSecurityRoot();
      if (!managerInTransaction.GetPermissions().Any<Telerik.Sitefinity.Security.Model.Permission>((Expression<Func<Telerik.Sitefinity.Security.Model.Permission, bool>>) (p => p.SetName == "PageTemplates" && p.ObjectId == securityRoot.Id && p.PrincipalId == SecurityManager.EveryoneRole.Id)))
      {
        Telerik.Sitefinity.Security.Model.Permission permission = managerInTransaction.CreatePermission("PageTemplates", securityRoot.Id, SecurityManager.EveryoneRole.Id);
        permission.GrantActions(true, "View");
        securityRoot.Permissions.Add(permission);
      }
      foreach (PageTemplate template in (IEnumerable<PageTemplate>) managerInTransaction.GetTemplates())
      {
        template.CanInheritPermissions = true;
        managerInTransaction.CreatePermissionInheritanceAssociation(securityRoot, (ISecuredObject) template);
      }
      PageNode pageNode = managerInTransaction.GetPageNodes().FirstOrDefault<PageNode>((Expression<Func<PageNode, bool>>) (p => p.Id == SiteInitializer.PageTemplatesNodeId));
      if (pageNode == null)
        return;
      managerInTransaction.RestorePermissionsInheritance((ISecuredObject) pageNode);
    }

    [UpgradeInfo(Description = "Creating not accessible site page.", FailMassage = "Failed to create not accessible site page.", Id = "07C1958C-D6C1-4487-BC02-E843675353A0", UpgradeTo = 6700)]
    private bool Upgrade_AddSiteNotAccessiblePage()
    {
      this.CreatePageFromConfiguration(Config.Get<PagesConfig>().BackendPages.Values.Where<PageElement>((Func<PageElement, bool>) (p => p.PageId == SiteInitializer.LoginNodeId)).SingleOrDefault<PageElement>().Pages.Values.Where<PageElement>((Func<PageElement, bool>) (p => p.PageId == SiteInitializer.SiteNotAccessiblePageId)).SingleOrDefault<PageElement>(), this.PageManager.GetPageNodes().Where<PageNode>((Expression<Func<PageNode, bool>>) (p => p.Id == SiteInitializer.LoginNodeId)).First<PageNode>());
      return true;
    }

    [UpgradeInfo(Description = "Update user profile urls.", FailMassage = "Could not upgrade user profile urls.", Id = "4235106E-6300-416F-B179-13FEC339CFE5", UpgradeTo = 6700)]
    private void UpgradeProfileUrls()
    {
      UserProfileManager managerInTransaction = this.GetManagerInTransaction<UserProfileManager>();
      foreach (User user in (IEnumerable<User>) this.GetManagerInTransaction<UserManager>().GetUsers())
      {
        SitefinityProfile userProfile = managerInTransaction.GetUserProfile<SitefinityProfile>(user);
        if (userProfile != null)
        {
          foreach (UserProfileUrlData url in (IEnumerable<UserProfileUrlData>) userProfile.Urls)
          {
            if (url.IsDefault)
              url.Url = managerInTransaction.Provider.CompileItemUrl<SitefinityProfile>(userProfile);
          }
        }
      }
    }

    [UpgradeInfo(Description = "Update app status report page.", FailMassage = "Failed to update app status report page.", Id = "894EBEF2-C2FD-4120-9195-A551C86D686C", UpgradeTo = 6700)]
    private bool UpdateAppStatusReport()
    {
      string path = HttpContext.Current.Server.MapPath("~/App_Data/appStatusReport.html");
      if (File.Exists(path) && File.ReadAllText(path).ComputeSha256Hash() == "8voYIIPBo8dFCv2bj0UbLPPQcbI6wwixbKWRBEstQFM1")
      {
        string statusPageContent = AppStatusService.GetDefaultAppStatusPageContent();
        File.WriteAllText(path, statusPageContent);
      }
      return true;
    }

    [UpgradeInfo(Description = "Removing IE 10 meta tag.", FailMassage = "Could not remove the IE 10 meta tag.", Id = "57050C0F-42B0-4D30-892E-F3632568AF29", UpgradeTo = 6800)]
    private void RemoveIE10Tag()
    {
      Guid[] templateIds = new Guid[3]
      {
        SiteInitializer.DefaultBackendTemplateEmptyId,
        SiteInitializer.BackendHtml5TemplateId,
        SiteInitializer.DefaultBackendTemplateId
      };
      string oldValue = "<meta http-equiv=\"X-UA-Compatible\" content=\"IE=10\" />";
      IQueryable<TemplatePresentation> presentationItems = this.GetManagerInTransaction<PageManager>().GetPresentationItems<TemplatePresentation>();
      Expression<Func<TemplatePresentation, bool>> predicate = (Expression<Func<TemplatePresentation, bool>>) (tp => tp.DataType == "HTML_DOCUMENT" && templateIds.Contains<Guid>(tp.Template.Id));
      foreach (TemplatePresentation templatePresentation in presentationItems.Where<TemplatePresentation>(predicate).ToArray<TemplatePresentation>())
      {
        if (templatePresentation.Data.Contains(oldValue))
          templatePresentation.Data = templatePresentation.Data.Replace(oldValue, string.Empty);
      }
    }

    [UpgradeInfo(Description = "Clearing PersonalizationMasterId and PersonalizationSegmentId from draft pages of original variations.", FailMassage = "Failed to clear PersonalizationMasterId and PersonalizationSegmentId from draft pages of original variations.", Id = "479E8744-2BFA-4778-9C29-C7D11AE26662", UpgradeTo = 6800)]
    private void UpgradePageDraftsPersonalizationProperties()
    {
      IQueryable<PageDraft> source = this.GetManagerInTransaction<PageManager>().GetPageDataList().Where<PageData>((Expression<Func<PageData, bool>>) (p => p.PersonalizationMasterId == Guid.Empty)).SelectMany<PageData, PageDraft>((Expression<Func<PageData, IEnumerable<PageDraft>>>) (p => p.Drafts));
      Expression<Func<PageDraft, bool>> predicate = (Expression<Func<PageDraft, bool>>) (p => p.PersonalizationMasterId != Guid.Empty || p.PersonalizationSegmentId != Guid.Empty);
      foreach (PageDraft pageDraft in (IEnumerable<PageDraft>) source.Where<PageDraft>(predicate))
      {
        pageDraft.PersonalizationMasterId = Guid.Empty;
        pageDraft.PersonalizationSegmentId = Guid.Empty;
      }
    }

    [UpgradeInfo(Description = "Creating a notifications profile from the original SMTP (Email Settings).", FailMassage = "Failed to create a notifications profile from the original SMTP (Email Settings)", Id = "7E0F6E61-61DC-4CB0-B769-16AF2ADA9638", UpgradeTo = 7000)]
    private bool CreateNotificationsProfileFromSmtpSettings()
    {
      try
      {
        SystemConfig config = this.Context.GetConfig<SystemConfig>();
        SystemManager.InitializeService(config.SystemServices["Notifications"], true);
        INotificationService notificationService = SystemManager.GetNotificationService();
        if (notificationService.GetSenderProfile((ServiceContext) null, "SystemConfigSmtpSettingsMigrated") == null)
        {
          SmtpElement smtpSettings = config.SmtpSettings;
          string host = smtpSettings.Host;
          string str1 = smtpSettings.Port.ToString();
          string userName = smtpSettings.UserName;
          string password = smtpSettings.Password;
          string domain = smtpSettings.Domain;
          string str2 = smtpSettings.DeliveryMethod.ToString();
          string str3 = smtpSettings.EnableSSL.ToString();
          string str4 = smtpSettings.Timeout.ToString();
          string directoryLocation = smtpSettings.PickupDirectoryLocation;
          string senderEmailAddress = smtpSettings.DefaultSenderEmailAddress;
          string emailBodyEncoding = smtpSettings.EmailBodyEncoding;
          string emailSubjectEncoding = smtpSettings.EmailSubjectEncoding;
          bool flag = !userName.IsNullOrEmpty() || !password.IsNullOrEmpty();
          if (!host.IsNullOrEmpty() && !senderEmailAddress.IsNullOrEmpty())
          {
            SmtpSenderProfileProxy senderProfileProxy = new SmtpSenderProfileProxy();
            senderProfileProxy.ProfileType = "smtp";
            SmtpSenderProfileProxy profile = senderProfileProxy;
            profile.CustomProperties["host"] = host;
            profile.CustomProperties["port"] = str1;
            profile.CustomProperties["username"] = userName;
            profile.CustomProperties["password"] = password;
            profile.CustomProperties["domain"] = domain;
            profile.CustomProperties["deliveryMethod"] = str2;
            profile.CustomProperties["useSSL"] = str3;
            profile.CustomProperties["timeout"] = str4;
            profile.CustomProperties["pickupDirectoryLocation"] = directoryLocation;
            profile.CustomProperties["defaultSenderEmailAddress"] = senderEmailAddress;
            profile.CustomProperties["emailBodyEncoding"] = emailBodyEncoding;
            profile.CustomProperties["emailSubjectEncoding"] = emailSubjectEncoding;
            profile.CustomProperties["useAuthentication"] = flag.ToString();
            profile.ProfileName = "SystemConfigSmtpSettingsMigrated";
            notificationService.SaveSenderProfile((ServiceContext) null, (ISenderProfile) profile);
            ConfigManager manager = ConfigManager.GetManager();
            SecurityConfig section = manager.GetSection<SecurityConfig>();
            section.Notifications.SenderProfile = profile.ProfileName;
            manager.SaveSection((ConfigSection) section);
          }
        }
        return true;
      }
      finally
      {
        ServiceBus.Clear();
      }
    }

    [UpgradeInfo(Description = "Resetting the available page template frameworks.", FailMassage = "Failed to reset the available page template frameworks.", Id = "966AE351-3F6F-42D0-8106-0FDF77255F83", UpgradeTo = 7000)]
    private void UpgradePageTemplatesAvailableFrameworks()
    {
      ConfigManager manager = ConfigManager.GetManager();
      PagesConfig section = manager.GetSection<PagesConfig>();
      if (section.PageTemplatesFrameworks == PageTemplatesAvailability.All)
        return;
      section.PageTemplatesFrameworks = PageTemplatesAvailability.All;
      manager.SaveSection((ConfigSection) section, true);
    }

    [UpgradeInfo(Description = "Resetting enabling of Inline Editing.", FailMassage = "Failed to reset enabling of Inline Editing.", Id = "d6516309-da66-4409-9a87-aa135b0f28b0", UpgradeTo = 7000)]
    private void UpgradeEnableBrowseAndEdit()
    {
      ConfigManager manager = ConfigManager.GetManager();
      PagesConfig section = manager.GetSection<PagesConfig>();
      ConfigProperty prop;
      if (section.Properties.TryGetValue("enableBrowseAndEdit", out prop))
      {
        PersistedValueWrapper valueWrapper;
        object rawValue = section.GetRawValue(prop, out valueWrapper, true);
        if (rawValue == null && (valueWrapper == null || valueWrapper.Source == ConfigSource.Default))
          section.EnableBrowseAndEdit = new bool?(true);
        else if (rawValue == null)
          section.EnableBrowseAndEdit = new bool?(false);
      }
      manager.SaveSection((ConfigSection) section, true);
    }

    [UpgradeInfo(Description = "Rename Basic template catory taxon.", FailMassage = "Failed to rename Basic template catory taxon.", Id = "10f93179-1f42-4e90-b36f-9e021fff09ca", UpgradeTo = 7000)]
    private void RenameBasicTemplatesTaxon() => Res.SetLstring(this.TaxonomyManager.GetTaxon<HierarchicalTaxon>(SiteInitializer.BasicTemplatesCategoryId).Title, typeof (PageResources), "BasicTemplates");

    [UpgradeInfo(Description = "Upgrades legacy multiple choice fields for static types and pages.", FailMassage = "Failed to upgrades legacy multiple choice fields for static types and pages.", Id = "91538291-3DAF-4D0C-A69B-99516CE1749E", UpgradeTo = 7100)]
    private void UpgradeLegacyMultipleChoiceCustomFields() => this.UpgradeLegacyMultipleChoiceCustomFields(typeof (PageNode));

    [UpgradeInfo(Description = "Upgrades legacy resizing images functionality.", FailMassage = "Failed to upgrades legacy(unsigned) resizing images functionality.", Id = "e0e6b309-01d5-4e4e-9c12-8964a5dfefe8", UpgradeTo = 7200)]
    private void UpgradeLegacyImageResizing()
    {
      ConfigManager manager = ConfigManager.GetManager();
      LibrariesConfig section = manager.GetSection<LibrariesConfig>();
      if (section.Images.AllowUnsignedDynamicResizing == section.Images.AllowDynamicResizing)
        return;
      section.Images.AllowUnsignedDynamicResizing = section.Images.AllowDynamicResizing;
      manager.SaveSection((ConfigSection) section, true);
    }

    [UpgradeInfo(Description = "Configuring Custom Error Pages", FailMassage = "Unable to configure Custom Error Pages", Id = "0B4F58B2-0C36-4F6E-8FDE-949FF481FF0B", UpgradeTo = 7200)]
    private void UpgradeCustomErrorPages()
    {
      ConfigManager manager = ConfigManager.GetManager();
      PagesConfig section = manager.GetSection<PagesConfig>();
      if (section.ErrorPages.Mode == CustomErrorPagesMode.Disabled)
        return;
      section.ErrorPages.Mode = CustomErrorPagesMode.Disabled;
      manager.SaveSection((ConfigSection) section, true);
    }

    [UpgradeInfo(Description = "Configuring comments date time settings", FailMassage = "Unable to configure the comment service", Id = "f035e7e2-8c72-4508-beaf-fa5920338cc5", UpgradeTo = 7200)]
    private void UpgradeCommentsDateSettings()
    {
      ConfigManager manager = ConfigManager.GetManager();
      CommentsModuleConfig section = manager.GetSection<CommentsModuleConfig>();
      if (!section.AlwaysUseUTC)
        return;
      section.AlwaysUseUTC = false;
      manager.SaveSection((ConfigSection) section, true);
    }

    [UpgradeInfo(Description = "To assure that DBType in custom fields SEO Description and OpenGraph Description is correctly assigned", FailMassage = "Unable to set DBType correctly in custom fields SEO Description and OpenGraph Description", Id = "F96BF594-8D1F-447C-BF0B-66BEB48F3D5F", UpgradeTo = 7400)]
    private void UpgradeCustomFieldsDBType()
    {
      foreach (MetaType metaType in (IEnumerable<MetaType>) this.MetadataManager.GetMetaTypes())
      {
        foreach (MetaField field in (IEnumerable<MetaField>) metaType.Fields)
        {
          if ((field.FieldName == "OpenGraphDescription" || field.FieldName == "MetaDescription") && field.DBType != "VARCHAR" && field.DBType != "CLOB")
            field.DBType = "VARCHAR";
        }
      }
    }

    [UpgradeInfo(Description = "All Lstring properties of backend pages and templates that do not use resource strings, must use only the system default language to persist data.", FailMassage = "Unable to update Lstring properties of some backend pages and/or templates to use only the system default culture for persistence when not using resource expressions.", Id = "1C64DA4F-964E-446A-8877-FB019D772FF9", UpgradeTo = 7426)]
    private void UpgradeLstringPropertiesOfBackendPagesAndTemplatesToPersisteOnlyInSystemDefaultCulture()
    {
      IQueryable<PageNode> pageNodes = this.PageManager.GetPageNodes();
      Expression<Func<PageNode, bool>> predicate1 = (Expression<Func<PageNode, bool>>) (x => x.RootNodeId == SiteInitializer.BackendRootNodeId);
      foreach (object node in (IEnumerable<PageNode>) pageNodes.Where<PageNode>(predicate1))
        this.UpdateNullLstringPropertiesInvariantValuesFromFirstAvailableLanguage(node);
      IQueryable<PageTemplate> templates = this.PageManager.GetTemplates();
      Expression<Func<PageTemplate, bool>> predicate2 = (Expression<Func<PageTemplate, bool>>) (x => x.Category == SiteInitializer.BackendTemplatesCategoryId);
      foreach (object node in (IEnumerable<PageTemplate>) templates.Where<PageTemplate>(predicate2))
        this.UpdateNullLstringPropertiesInvariantValuesFromFirstAvailableLanguage(node);
    }

    private void UpdateNullLstringPropertiesInvariantValuesFromFirstAvailableLanguage(object node)
    {
      foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(node))
      {
        if (property.PropertyType == typeof (Lstring))
        {
          Lstring lstring = property.GetValue(node) as Lstring;
          if (lstring != (Lstring) null && lstring[CultureInfo.InvariantCulture] == null)
          {
            string str = ((IEnumerable<string>) lstring.GetAllValues()).FirstOrDefault<string>();
            if (str != null)
            {
              lstring[CultureInfo.InvariantCulture] = str;
              property.SetValue(node, (object) lstring);
            }
          }
        }
      }
    }

    [SuppressMessage("Telerik.Sitefinity", "SF1001:AvoidToListOnIQueryable")]
    internal void UpgradeContentDefaultUrls(IEnumerable<ILocatableExtended> items, IManager manager)
    {
      if (items.Count<ILocatableExtended>() == 0)
        return;
      using (new DataSyncModeRegion(manager))
      {
        try
        {
          foreach (ILocatableExtended locatableExtended in items.ToList<ILocatableExtended>())
          {
            foreach (UrlData urlData in locatableExtended.Urls.Where<UrlData>((Func<UrlData, bool>) (u => u.IsDefault)))
            {
              int culture = urlData.Culture;
              if (culture == CultureInfo.InvariantCulture.LCID || AppSettings.CurrentSettings.AllLanguages.Keys.Contains(culture))
              {
                CultureInfo cultureByLcid = AppSettings.CurrentSettings.GetCultureByLcid(culture);
                locatableExtended.ItemDefaultUrl.SetString(cultureByLcid, urlData.Url);
              }
            }
            if (!string.IsNullOrEmpty(manager.TransactionName))
              TransactionManager.FlushTransaction(manager.TransactionName);
          }
          if (!string.IsNullOrEmpty(manager.TransactionName))
            TransactionManager.CommitTransaction(manager.TransactionName);
          else
            manager.SaveChanges();
          Log.Write((object) string.Format("PASSED : Upgrade DefaultUrls for type: '{0}' and manager: '{1}'", (object) items.GetType().GetGenericArguments()[0].FullName, (object) manager.GetType().FullName), ConfigurationPolicy.UpgradeTrace);
        }
        catch (Exception ex)
        {
          if (!string.IsNullOrEmpty(manager.TransactionName))
            TransactionManager.RollbackTransaction(manager.TransactionName);
          else
            manager.CancelChanges();
          Log.Write((object) string.Format("FAILED : Upgrade DefaultUrls for type: '{0}' and manager: '{1}'. Exception: {2}", (object) items.GetType().GetGenericArguments()[0].FullName, (object) manager.GetType().FullName, (object) ex.Message), ConfigurationPolicy.UpgradeTrace);
          if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
            throw ex;
        }
      }
    }

    /// <summary>Executes the upgrade methods in the current instance.</summary>
    /// <returns></returns>
    internal void Upgrade(int upgradeFrom) => SiteInitializer.Upgrade((object) this, upgradeFrom, (object) null);

    /// <summary>
    /// Executes the upgrade methods in the specified instance.
    /// </summary>
    /// <param name="instance">The instance.</param>
    /// <param name="upgradeFrom">The version of the build related to the upgrade method.</param>
    /// <param name="commitChanges">Specified whether to commit changes once executed or not.</param>
    internal void Upgrade(object instance, int upgradeFrom) => SiteInitializer.Upgrade(instance, upgradeFrom, (object) this);

    /// <summary>
    /// If specified that the message should be logged directly, then it is written to the log file, otherwise it is added a collection with messages which is logged after in save changes method.
    /// </summary>
    /// <param name="upgradeMsg">The upgrade MSG.</param>
    private void LogPendingUpgradeMessage(string upgradeMsg) => this.upgradeLogMessages.Add(upgradeMsg);

    /// <summary>
    /// Executes the upgrade methods marked with the specified attribute in the specified instance and context.
    /// </summary>
    /// <param name="instance">The instance.</param>
    /// <param name="upgradeFrom">The version of the build related to the upgrade method.</param>
    /// <param name="context">The context.</param>
    /// <param name="upgradeAttributeType">Type of the upgrade attribute.</param>
    internal static void Upgrade(object instance, int upgradeFrom, object upgradeContext) => SiteInitializer.Upgrade<UpgradeInfoAttribute>(instance, upgradeFrom, upgradeContext);

    /// <summary>
    /// Executes the upgrade methods marked with the specified attribute in the specified instance and context.
    /// </summary>
    /// <param name="instance">The instance.</param>
    /// <param name="upgradeFrom">The version of the build related to the upgrade method.</param>
    /// <param name="context">The context.</param>
    /// <param name="upgradeAttributeType">Type of the upgrade attribute.</param>
    internal static void Upgrade<TUpgradeInfoAttribute>(
      object instance,
      int upgradeFrom,
      object upgradeContext)
      where TUpgradeInfoAttribute : UpgradeInfoAttribute
    {
      List<UpgradeMethod> upgrades = SiteInitializer.UpgradeMethodFactory.GetUpgrades<TUpgradeInfoAttribute>(upgradeFrom, instance);
      if (upgrades.Count <= 0)
        return;
      SiteInitializer siteInitializer1 = instance as SiteInitializer;
      SiteInitializer siteInitializer2 = upgradeContext as SiteInitializer;
      if (siteInitializer1 != null)
      {
        siteInitializer2 = (SiteInitializer) null;
        siteInitializer1.SuppressSecurity();
      }
      upgrades.Sort((Comparison<UpgradeMethod>) ((upgr1, upgr2) => upgr1.UpgradeTo.CompareTo(upgr2.UpgradeTo)));
      foreach (UpgradeMethod upgrade in upgrades)
      {
        try
        {
          upgrade.Execute(upgradeContext);
          siteInitializer1?.SaveChanges(false);
          if (!upgrade.UpgradeInfo.HideFromUpgradeLog)
          {
            string upgradeSuccessMessage = SiteInitializer.GenerateUpgradeSuccessMessage(upgrade);
            if (siteInitializer2 != null)
              siteInitializer2.LogPendingUpgradeMessage(upgradeSuccessMessage);
            else
              Log.Write((object) upgradeSuccessMessage, ConfigurationPolicy.UpgradeTrace);
          }
        }
        catch (Exception ex)
        {
          string upgradeFailMessage = SiteInitializer.GenerateUpgradeFailMessage(upgrade, ex);
          if (!upgrade.UpgradeInfo.HideFromUpgradeLog)
          {
            if (siteInitializer2 != null)
              siteInitializer2.LogPendingUpgradeMessage(upgradeFailMessage);
            else
              Log.Write((object) upgradeFailMessage, ConfigurationPolicy.UpgradeTrace);
          }
          Exception exceptionToHandle = new Exception(upgradeFailMessage, ex);
          if (Exceptions.HandleException(exceptionToHandle, ExceptionPolicyName.IgnoreExceptions))
            throw exceptionToHandle;
        }
        finally
        {
          siteInitializer1?.UndoChanges(false);
        }
      }
      siteInitializer1?.SuppressSecurity();
    }

    private static string GenerateUpgradeSuccessMessage(UpgradeMethod upgrade) => string.Format("PASSED : {0} : Upgrade to {1} ({2})", (object) upgrade.Instance.GetType().Name, (object) upgrade.UpgradeTo, (object) (upgrade.UpgradeInfo.Description ?? "method: " + upgrade.MethodName));

    private static string GenerateUpgradeFailMessage(UpgradeMethod upgrade, Exception err) => string.Format("FAILED : {0} : Upgrade to {1} ({2}) - {3}", (object) upgrade.Instance.GetType().Name, (object) upgrade.UpgradeTo, (object) (upgrade.UpgradeInfo.Description ?? "method: " + upgrade.MethodName), (object) (upgrade.UpgradeInfo.FailMassage ?? err.Message));

    private void RegisterProvider(DataProviderBase provider)
    {
      if (this.providers.ContainsKey(provider))
        return;
      bool suppressSecurityChecks = provider.SuppressSecurityChecks;
      if (this.suppressSecurityChecks)
        provider.SuppressSecurityChecks = true;
      this.providers.Add(provider, suppressSecurityChecks);
    }

    /// <summary>Suppresses the security checks.</summary>
    protected internal virtual void SuppressSecurity()
    {
      this.suppressSecurityChecks = true;
      foreach (KeyValuePair<DataProviderBase, bool> provider in (IEnumerable<KeyValuePair<DataProviderBase, bool>>) this.providers)
        provider.Key.SuppressSecurityChecks = this.suppressSecurityChecks;
    }

    protected internal virtual void RestoreSecurity()
    {
      this.suppressSecurityChecks = false;
      foreach (KeyValuePair<DataProviderBase, bool> provider in (IEnumerable<KeyValuePair<DataProviderBase, bool>>) this.providers)
        provider.Key.SuppressSecurityChecks = provider.Value;
    }

    internal void SaveChanges(bool restoreSecurityCheck)
    {
      TransactionManager.CommitTransaction(this.TransactionName);
      foreach (object upgradeLogMessage in this.upgradeLogMessages)
        Log.Write(upgradeLogMessage, ConfigurationPolicy.UpgradeTrace);
      if (!restoreSecurityCheck)
        return;
      this.RestoreSecurity();
    }

    /// <summary>Saves the changes.</summary>
    public void SaveChanges() => this.SaveChanges(true);

    /// <summary>Undoes the changes.</summary>
    public void UndoChanges() => this.UndoChanges(true);

    internal void UndoChanges(bool restoreSecurityCheck = true)
    {
      TransactionManager.RollbackTransaction(this.TransactionName);
      if (!restoreSecurityCheck)
        return;
      this.RestoreSecurity();
    }

    /// <summary>Flush the changes.</summary>
    public virtual void FlushChanges() => TransactionManager.FlushTransaction(this.TransactionName);

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose() => this.Dispose(true);

    private void Dispose(bool disposing)
    {
      if (!disposing)
        return;
      TransactionManager.DisposeTransaction(this.TransactionName);
      this.installContext = (InstallContext) null;
      this.moduleInstaller = (ModuleInstallFacade) null;
      this.RestoreSecurity();
    }

    /// <summary>
    /// Resets the backend root to the default.
    /// Important! All customizations on the backend will be lost.
    /// </summary>
    internal void ResetBackendRoot() => this.ResetBackendRoot(true);

    /// <summary>
    /// Resets the backend root to the default.
    /// Important! All customizations on the backend will be lost.
    /// </summary>
    /// <param name="checkPermissions">if set to <c>true</c> [check permissions].</param>
    private void ResetBackendRoot(bool checkPermissions)
    {
      SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
      if (checkPermissions)
      {
        if (currentIdentity == null || !currentIdentity.IsUnrestricted)
          throw new UnauthorizedAccessException(Res.Get<ErrorMessages>().AuthorizationFailedOnlyAdminsAllowed.Arrange((object) "Reset Backend Root"));
      }
      else
      {
        this.PageManager.Provider.SuppressSecurityChecks = true;
        this.TaxonomyManager.Provider.SuppressSecurityChecks = true;
      }
      PageNode pageNode = this.PageManager.GetPageNodes().SingleOrDefault<PageNode>((Expression<Func<PageNode, bool>>) (n => n.Id == SiteInitializer.BackendRootNodeId));
      if (pageNode != null)
      {
        this.PageManager.Delete(pageNode);
        this.FlushChanges();
      }
      this.CreateBackendRoot();
    }

    /// <summary>Creates the version trunks.</summary>
    protected virtual void CreateVersionTrunks()
    {
      if (this.VersionManager.GetTrunks().Where<Trunk>((Expression<Func<Trunk, bool>>) (t => t.Name == "Pages")).SingleOrDefault<Trunk>() != null)
        return;
      Trunk trunk = this.VersionManager.CreateTrunk();
      trunk.Name = "Pages";
      trunk.Description = "A trunk for page, template and control versions.";
    }

    /// <summary>Creates the page node.</summary>
    /// <param name="id">The id.</param>
    /// <param name="parent">The parent.</param>
    /// <returns></returns>
    public PageNode CreatePageNode(Guid id, PageNode parent, NodeType nodeType) => this.CreatePageNode(id, parent, 0.0f, nodeType);

    /// <summary>Creates the page node.</summary>
    /// <param name="id">The id.</param>
    /// <param name="parent">The parent.</param>
    /// <returns></returns>
    public PageNode CreatePageNode(
      Guid id,
      PageNode parent,
      float ordinal,
      NodeType nodeType)
    {
      return this.CreatePageNode(id, parent, ordinal, true, true, nodeType);
    }

    /// <summary>Creates the page node.</summary>
    /// <param name="id">The id.</param>
    /// <param name="parent">The parent.</param>
    /// <param name="canInheritPermissions">if set to <c>true</c> [can inherit permissions].</param>
    /// <param name="inheritsPermissions">if set to <c>true</c> [inherits permissions].</param>
    /// <returns></returns>
    public PageNode CreatePageNode(
      Guid id,
      PageNode parent,
      bool canInheritPermissions,
      bool inheritsPermissions,
      NodeType nodeType)
    {
      return this.CreatePageNode(id, parent, 0.0f, canInheritPermissions, inheritsPermissions, nodeType);
    }

    /// <summary>Creates the page node.</summary>
    /// <param name="id">The id.</param>
    /// <param name="parent">The parent.</param>
    /// <param name="ordinal">The ordinal.</param>
    /// <param name="canInheritPermissions">if set to <c>true</c> [can inherit permissions].</param>
    /// <param name="inheritsPermissions">if set to <c>true</c> [inherits permissions].</param>
    /// <returns></returns>
    public PageNode CreatePageNode(
      Guid id,
      PageNode parent,
      float ordinal,
      bool canInheritPermissions,
      bool inheritsPermissions,
      NodeType nodeType)
    {
      PageNode pageNode = this.CreatePageNode(id, new string[0], canInheritPermissions, inheritsPermissions, nodeType);
      if (parent != null)
      {
        this.PageManager.ChangeParent(pageNode, parent);
        if ((double) ordinal == 0.0)
          ordinal = (float) (parent.Nodes.Count + 1);
        parent.Nodes.Add(pageNode);
      }
      pageNode.Ordinal = ordinal;
      return pageNode;
    }

    /// <summary>Creates the page node.</summary>
    /// <param name="id">The id.</param>
    /// <param name="supportedPermissionSetNames">The supported permission set names.</param>
    /// <param name="canInheritPermissions">if set to <c>true</c> [can inherit permissions].</param>
    /// <param name="inheritsPermissions">if set to <c>true</c> [inherits permissions].</param>
    /// <param name="rootkeyObjectTitleKeys">The rootkey object title keys.</param>
    /// <returns></returns>
    public PageNode CreatePageNode(
      Guid id,
      string[] supportedPermissionSetNames,
      bool canInheritPermissions,
      bool inheritsPermissions,
      NodeType nodeType)
    {
      PageNode pageNode = this.PageManager.CreatePageNode(id);
      pageNode.NodeType = nodeType;
      if (supportedPermissionSetNames != null && supportedPermissionSetNames.Length != 0)
        pageNode.SupportedPermissionSets = supportedPermissionSetNames;
      pageNode.CanInheritPermissions = canInheritPermissions;
      pageNode.InheritsPermissions = inheritsPermissions;
      return pageNode;
    }

    public PageNode CreateSiteRoot(string name, string title) => this.CreateSiteRoot(this.PageManager.Provider.GetNewGuid(), name, title);

    public PageNode CreateSiteRoot(Guid rootId, string name, string title)
    {
      string[] supportedPermissionSetNames = new string[1]
      {
        "Pages"
      };
      PageNode pageNode = this.CreatePageNode(rootId, supportedPermissionSetNames, false, false, NodeType.Group);
      pageNode.Name = name;
      pageNode.Title = (Lstring) title;
      pageNode.UrlName = (Lstring) title;
      pageNode.RenderAsLink = false;
      Telerik.Sitefinity.Security.Model.Permission permission1 = this.PageManager.Provider.CreatePermission("Pages", pageNode.Id, SecurityManager.EveryoneRole.Id);
      permission1.GrantActions(true, "View");
      pageNode.Permissions.Add(permission1);
      Telerik.Sitefinity.Security.Model.Permission permission2 = this.PageManager.Provider.CreatePermission("Pages", pageNode.Id, SecurityManager.EditorsRole.Id);
      permission2.GrantActions(true, "ChangeOwner", "Create", "CreateChildControls", "Delete", "EditContent", "Modify");
      pageNode.Permissions.Add(permission2);
      Telerik.Sitefinity.Security.Model.Permission permission3 = this.PageManager.Provider.CreatePermission("Pages", pageNode.Id, SecurityManager.DesignersRole.Id);
      permission3.GrantActions(true, "ChangeOwner", "Create", "CreateChildControls", "Delete", "EditContent", "Modify");
      pageNode.Permissions.Add(permission3);
      Telerik.Sitefinity.Security.Model.Permission permission4 = this.PageManager.Provider.CreatePermission("Pages", pageNode.Id, SecurityManager.AuthorsRole.Id);
      permission4.GrantActions(true, "Create");
      pageNode.Permissions.Add(permission4);
      Telerik.Sitefinity.Security.Model.Permission permission5 = this.PageManager.Provider.CreatePermission("Pages", pageNode.Id, SecurityManager.OwnerRole.Id);
      permission5.GrantActions(true, "CreateChildControls", "Delete", "EditContent", "Modify");
      pageNode.Permissions.Add(permission5);
      return pageNode;
    }

    /// <summary>Creates the frontend root.</summary>
    /// <returns></returns>
    protected virtual PageNode CreateFrontendRoot()
    {
      this.InstallDefaultSiteSettings(false);
      PageNode siteRoot = this.CreateSiteRoot(this.Context.GetConfig<ProjectConfig>().DefaultSite.SiteMapRootNodeId, Config.Get<PagesConfig>().FrontendRootNode, "Pages");
      siteRoot.Description = (Lstring) "Represents the default frontend page structure.";
      this.PageManager.Provider.CommitTransaction();
      return siteRoot;
    }

    private void CreateDashboardGroupPage()
    {
      if (this.pageManager.GetPageNodes().FirstOrDefault<PageNode>((Expression<Func<PageNode, bool>>) (p => p.Id == SiteInitializer.DashboardPageNodeId)) != null)
        return;
      PageNode pageNode1 = this.pageManager.CreatePageNode(SiteInitializer.DashboardPageNodeId);
      pageNode1.Name = "DashboardGroup";
      pageNode1.Title = (Lstring) Res.Expression<PageResources>("DashboardGroupPageTitle");
      pageNode1.UrlName = (Lstring) Res.Expression<PageResources>("DashboardGroupPageUrlName");
      pageNode1.Description = (Lstring) Res.Expression<PageResources>("DashboardGroupPageDescription");
      pageNode1.NodeType = NodeType.Group;
      pageNode1.RenderAsLink = true;
      pageNode1.Attributes.Add("VisibleGroupPage", "true");
      pageNode1.RootNodeId = SystemManager.CurrentContext.AppSettings.BackendRootNodeId;
      PageNode pageNode2 = this.pageManager.GetPageNode(SiteInitializer.SitefinityNodeId);
      this.PageManager.ChangeParent(pageNode1, pageNode2);
      pageNode1.Ordinal = -1f;
      PageNode childNode = (PageNode) null;
      try
      {
        childNode = this.PageManager.GetPageNode(SiteInitializer.LegacyDashboardPageNodeId);
      }
      catch
      {
      }
      if (childNode == null)
        return;
      childNode.Name = "LegacyDashboard";
      childNode.Title = (Lstring) Res.Expression<PageResources>("LegacyDashboardTitle");
      childNode.GetPageData().HtmlTitle = (Lstring) Res.Expression<PageResources>("LegacyDashboardHtmlTitle");
      childNode.UrlName = (Lstring) Res.Expression<PageResources>("LegacyDashboardUrlName");
      childNode.Ordinal = 1f;
      childNode.ShowInNavigation = false;
      this.PageManager.ChangeParent(childNode, pageNode1);
      this.Context.GetConfig<PagesConfig>().BackendHomePageId = SiteInitializer.DashboardPageNodeId;
    }

    /// <summary>Creates the backend root.</summary>
    /// <returns></returns>
    protected virtual PageNode CreateBackendRoot()
    {
      PagesConfig pagesConfig = Config.Get<PagesConfig>();
      string[] supportedPermissionSetNames = new string[1]
      {
        "Pages"
      };
      PageNode pageNode = this.CreatePageNode(SiteInitializer.BackendRootNodeId, supportedPermissionSetNames, false, false, NodeType.Group);
      pageNode.Name = pagesConfig.BackendRootNode;
      pageNode.Title = (Lstring) "Backend";
      pageNode.RenderAsLink = false;
      pageNode.UrlName = (Lstring) "Backend";
      pageNode.Description = (Lstring) "Represents the default backend page structure.";
      Telerik.Sitefinity.Security.Model.Permission permission1 = this.PageManager.CreatePermission("Pages", pageNode.Id, SecurityManager.BackEndUsersRole.Id);
      permission1.GrantActions(false, "View");
      pageNode.Permissions.Add(permission1);
      this.PageManager.Provider.CommitTransaction();
      foreach (DataProviderSettings provider in (ConfigElementCollection) Config.Get<PagesConfig>().Providers)
      {
        PageManager manager = PageManager.GetManager(provider.Name, this.TransactionName);
        ISecuredObject securityRoot = manager.GetSecurityRoot();
        Telerik.Sitefinity.Security.Model.Permission permission2 = manager.GetPermission("PageTemplates", securityRoot.Id, SecurityManager.DesignersRole.Id);
        if (permission2 == null)
        {
          permission2 = manager.CreatePermission("PageTemplates", securityRoot.Id, SecurityManager.DesignersRole.Id);
          securityRoot.Permissions.Add(permission2);
        }
        permission2.GrantActions(true, "Create", "Delete", "Modify");
      }
      foreach (DataProviderSettings securityProvider in (ConfigElementCollection) Config.Get<SecurityConfig>().SecurityProviders)
      {
        SecurityManager manager = SecurityManager.GetManager(securityProvider.Name, this.TransactionName);
        SecurityRoot securityRoot = manager.GetSecurityRoot("ApplicationSecurityRoot");
        if (securityRoot == null)
          securityRoot = manager.CreateSecurityRoot("ApplicationSecurityRoot", "Backend");
        Telerik.Sitefinity.Security.Model.Permission permission3 = manager.GetPermission("Backend", securityRoot.Id, SecurityManager.DesignersRole.Id);
        if (permission3 == null)
        {
          permission3 = manager.CreatePermission("Backend", securityRoot.Id, SecurityManager.DesignersRole.Id);
          securityRoot.Permissions.Add(permission3);
        }
        permission3.GrantActions(true, "ManageWidgets");
      }
      this.CreateBackendPagesRecursive(this.GetSitefinityNode(pageNode), pagesConfig.BackendPages);
      return pageNode;
    }

    private void CreateBackendPagesRecursive(
      PageNode root,
      ConfigElementDictionary<string, PageElement> elements)
    {
      foreach (PageElement element in (ConfigElementCollection) elements)
        this.CreateBackendPagesRecursive(this.CreatePageFromConfiguration(element, root), element.Pages);
    }

    private void SetPageFromModule(
      Guid pageId,
      Guid parentPageId,
      float ordinal,
      PageElement pageInfo = null)
    {
      PageManager pageManager = this.PageManager;
      PageNode pageNode = pageManager.GetPageNodes().FirstOrDefault<PageNode>((Expression<Func<PageNode, bool>>) (p => p.Id == pageId));
      PageNode newParent = pageManager.GetPageNodes().FirstOrDefault<PageNode>((Expression<Func<PageNode, bool>>) (p => p.Id == parentPageId));
      if (pageNode == null)
        return;
      if (newParent != null)
        pageManager.ChangeParent(pageNode, newParent);
      if (pageInfo != null)
        this.SetNames(pageNode, pageInfo);
      pageNode.Ordinal = ordinal;
      this.SaveChanges(false);
    }

    private void SetPageFromConfiguration(
      PageElement pageInfo,
      Guid parentPageId,
      bool createPageNodeIfNull = true)
    {
      if (pageInfo == null)
        return;
      PageManager pageManager = this.PageManager;
      PageNode pageNode = pageManager.GetPageNodes().FirstOrDefault<PageNode>((Expression<Func<PageNode, bool>>) (p => p.Id == pageInfo.PageId));
      if (pageNode == null && createPageNodeIfNull)
      {
        NodeType nodeType = pageInfo.Pages.Values.Count > 0 ? NodeType.Group : NodeType.Standard;
        pageNode = pageManager.CreatePageNode(pageInfo.PageId);
        pageNode.NodeType = nodeType;
        pageNode.RootNodeId = DataExtensions.AppSettings.BackendRootNodeId;
      }
      if (parentPageId != Guid.Empty)
      {
        PageNode newParent = pageManager.GetPageNodes().FirstOrDefault<PageNode>((Expression<Func<PageNode, bool>>) (p => p.Id == parentPageId));
        if (newParent != null)
          pageManager.ChangeParent(pageNode, newParent);
      }
      this.SetNames(pageNode, pageInfo);
      foreach (string allKey in pageInfo.Parameters.AllKeys)
        pageNode.Attributes[allKey] = pageInfo.Parameters[allKey];
      pageNode.Name = pageInfo.Name;
      pageNode.RenderAsLink = pageInfo.RenderAsLink;
      pageNode.ShowInNavigation = pageInfo.ShowInNavigation;
      pageNode.Ordinal = pageInfo.Ordinal;
      this.SaveChanges(false);
    }

    private void CreatePageFromConfiguration(
      PageElement pageInfo,
      PageNode parentNode,
      bool recursive)
    {
      PageNode fromConfiguration = this.CreatePageFromConfiguration(pageInfo, parentNode);
      if (!recursive)
        return;
      this.CreateBackendPagesRecursive(fromConfiguration, pageInfo.Pages);
    }

    /// <summary>Creates a page from configuration element.</summary>
    /// <param name="pageInfo">The page configuration element.</param>
    /// <param name="parentNode">The parent node of this page.</param>
    /// <param name="controls">The controls to be added to the page.</param>
    [Obsolete("Use fluent API App.WorkWith().Module().Install() instead.")]
    public PageNode CreatePageFromConfiguration(
      PageElement pageInfo,
      PageNode parentNode,
      params Control[] controls)
    {
      return this.CreatePageFromConfiguration(pageInfo, parentNode, (PageTemplate) null, controls);
    }

    /// <summary>Creates a page from configuration element.</summary>
    /// <param name="pageInfo">The page configuration element.</param>
    /// <param name="template">The page template. If not null, the TemplateName configuration will be ignored.</param>
    /// <param name="controls">The controls to be added to the page.</param>
    [Obsolete("Use fluent API App.WorkWith().Module().Install() instead.")]
    public PageNode CreatePageFromConfiguration(
      PageElement pageInfo,
      PageNode parentNode,
      PageTemplate template,
      params Control[] controls)
    {
      if (pageInfo == null)
        throw new ArgumentNullException(nameof (pageInfo));
      if (pageInfo.PageId == Guid.Empty)
        throw new ArgumentException("PageId cannot be empty GUID.");
      Guid pageId = pageInfo.PageId;
      PageNode pageNode = (PageNode) null;
      if (!this.PageManager.GetPageNodes().Where<PageNode>((Expression<Func<PageNode, bool>>) (p => p.Id == pageId)).Any<PageNode>())
      {
        NodeType nodeType = !(pageInfo is PageDataElement pageElement) ? NodeType.Group : NodeType.Standard;
        pageNode = this.CreatePageNode(pageInfo.PageId, parentNode, pageInfo.Ordinal, nodeType);
        pageNode.Name = pageInfo.Name;
        pageNode.RenderAsLink = pageInfo.RenderAsLink;
        pageNode.ShowInNavigation = pageInfo.ShowInNavigation;
        foreach (string allKey in pageInfo.Parameters.AllKeys)
          pageNode.Attributes[allKey] = pageInfo.Parameters[allKey];
        if (pageElement != null)
        {
          PageData pageData = this.PageManager.CreatePageData();
          pageData.NavigationNode = pageNode;
          if (template == null)
          {
            string templateName = pageElement.TemplateName;
            if (!string.IsNullOrEmpty(templateName))
            {
              if (pageElement.TemplateName == "DefaultBackend")
                template = this.BackendTemplate;
              else
                template = this.PageManager.GetTemplates().Where<PageTemplate>((Expression<Func<PageTemplate, bool>>) (t => t.Name == templateName)).SingleOrDefault<PageTemplate>();
            }
            else
              template = (PageTemplate) null;
          }
          pageData.Template = template;
          pageData.EnableViewState = pageElement.EnableViewState;
          pageData.IncludeScriptManager = pageElement.IncludeScriptManager;
          if (controls != null && controls.Length != 0 || this.TryGetPageControls(pageElement, out controls))
          {
            foreach (Control control in controls)
              pageData.Controls.Add(this.CreateControlPanel(control, pageData.NavigationNode != null && pageData.NavigationNode.IsBackend));
          }
          foreach (PresentationElement presentationElement in (IEnumerable<PresentationElement>) pageInfo.Presentation.Values)
          {
            PagePresentation presentationItem = this.PageManager.CreatePresentationItem<PagePresentation>();
            presentationItem.Name = presentationElement.Name;
            presentationItem.Theme = presentationElement.Theme;
            presentationItem.DataType = presentationElement.DataType;
            presentationItem.Data = presentationElement.Data;
            pageData.Presentation.Add(presentationItem);
          }
          pageData.Status = ContentLifecycleStatus.Live;
          pageData.Visible = true;
          pageData.Version = 1;
          LanguageData publishedLanguageData = this.pageManager.CreatePublishedLanguageData();
          pageData.LanguageData.Add(publishedLanguageData);
        }
        this.SetNames(pageNode, pageInfo);
      }
      return pageNode;
    }

    /// <summary>Tries the get page controls.</summary>
    /// <param name="pageElement">The page element.</param>
    /// <param name="controls">The controls.</param>
    /// <returns></returns>
    protected virtual bool TryGetPageControls(PageDataElement pageElement, out Control[] controls)
    {
      if (pageElement.Controls.Count > 0)
      {
        List<Control> controlList = new List<Control>(pageElement.Controls.Count);
        foreach (PageControlElement control in pageElement.Controls)
        {
          Control instance = (Control) Activator.CreateInstance(control.Type);
          if (instance != null)
          {
            foreach (ControlPropertyElement property1 in (ConfigElementCollection) control.Properties)
            {
              PropertyInfo property2 = instance.GetType().GetProperty(property1.Name);
              if (property2 != (PropertyInfo) null)
                property2.SetValue((object) instance, property1.Value, (object[]) null);
            }
            controlList.Add(instance);
          }
        }
        controls = controlList.ToArray();
        return true;
      }
      Control control1;
      switch (pageElement.Name)
      {
        case "AdvancedSettings":
          control1 = (Control) new ConfigurationPanel();
          break;
        case "BackendPages":
          control1 = (Control) new ContentView()
          {
            ControlDefinitionName = "BackendPages"
          };
          break;
        case "BackendPagesWarning":
          PagesPanel pagesPanel = new PagesPanel();
          pagesPanel.ViewMode = "BackendPagesWarning";
          control1 = (Control) pagesPanel;
          break;
        case "BackendTemplates":
          control1 = (Control) new ContentView()
          {
            ControlDefinitionName = "BackendPageTemplates"
          };
          break;
        case "BasicSettings":
          control1 = (Control) new BasicSettingsPanel();
          break;
        case "Classifications":
          BackendContentView backendContentView1 = new BackendContentView();
          backendContentView1.ControlDefinitionName = "TaxonomyBackend";
          control1 = (Control) backendContentView1;
          break;
        case "ContentLocations":
          control1 = (Control) new ManageContentLocations();
          break;
        case "Dashboard":
          control1 = (Control) new Dashboard();
          break;
        case "Files":
          control1 = (Control) new FilesPanel();
          break;
        case "FlatTaxonomy":
          BackendContentView backendContentView2 = new BackendContentView();
          backendContentView2.ControlDefinitionName = "FlatTaxonBackend";
          control1 = (Control) backendContentView2;
          break;
        case "HelpAndResources":
          control1 = (Control) new HelpAndResourcesView();
          break;
        case "HierarchicalTaxonomy":
          BackendContentView backendContentView3 = new BackendContentView();
          backendContentView3.ControlDefinitionName = "HierarchicalTaxonBackend";
          control1 = (Control) backendContentView3;
          break;
        case "Labels":
          control1 = (Control) new LocalizationPanel();
          break;
        case "LoginFailed":
          control1 = (Control) new LoginFailedPanel();
          break;
        case "LoginForm":
          control1 = (Control) new StsLoginFormPanel();
          break;
        case "MarkedItems":
          BackendContentView backendContentView4 = new BackendContentView();
          backendContentView4.ControlDefinitionName = MarkedItemsDefinitions.Name;
          control1 = (Control) backendContentView4;
          break;
        case "ModulesAndServices":
          control1 = (Control) new ManageModules();
          break;
        case "NeedAdminRights":
          control1 = (Control) new NeedAdminRightsPanel();
          break;
        case "PageTemplates":
          control1 = (Control) new ContentView()
          {
            ControlDefinitionName = "FrontendPageTemplates"
          };
          break;
        case "Pages":
          control1 = (Control) new ContentView()
          {
            ControlDefinitionName = "FrontendPages"
          };
          break;
        case "Permissions":
          control1 = (Control) new GlobalPermissionsPanel();
          break;
        case "Profile":
          control1 = (Control) new ProfilePanel();
          break;
        case "Roles":
          control1 = (Control) new RolesPanel();
          break;
        case "SiteNotAccessible":
          control1 = (Control) new SiteNotAccessiblePanel();
          break;
        case "UserAlreadyLoggedIn":
          control1 = (Control) new UserAlreadyLoggedInPanel();
          break;
        case "UserLimitReached":
          control1 = (Control) new UserLimitReachedPanel();
          break;
        case "UserProfileTypes":
          control1 = (Control) new ContentView()
          {
            ControlDefinitionName = "UserProfileTypesBackend"
          };
          break;
        case "Users":
          control1 = (Control) new UsersPanel();
          break;
        case "VersionAndLicensing":
          control1 = (Control) new LicenseInformationControl();
          break;
        case "Workflow":
          control1 = (Control) new WorkflowControlPanel();
          break;
        default:
          control1 = new Control();
          break;
      }
      if (control1 != null)
      {
        controls = new Control[1]{ control1 };
        return true;
      }
      controls = (Control[]) null;
      return false;
    }

    /// <summary>
    /// Resets permissions (no grant &amp; no deny) on a specific page node, for a collection of principals.
    /// </summary>
    /// <param name="node">The page node to reset.</param>
    /// <param name="principals">The principals.</param>
    public void UnsetPermissionsForPageNode(
      PageNode node,
      Guid[] principals,
      PageManager pageManagerInstance = null)
    {
      if (node == null)
        return;
      if (pageManagerInstance == null)
        pageManagerInstance = this.PageManager;
      pageManagerInstance.BreakPermiossionsInheritance((ISecuredObject) node);
      foreach (Guid principal1 in principals)
      {
        Guid principal = principal1;
        Telerik.Sitefinity.Security.Model.Permission permission = node.GetActivePermissions().Where<Telerik.Sitefinity.Security.Model.Permission>((Expression<Func<Telerik.Sitefinity.Security.Model.Permission, bool>>) (p => p.PrincipalId == principal && p.ObjectId == node.Id)).FirstOrDefault<Telerik.Sitefinity.Security.Model.Permission>();
        if (permission != null)
        {
          permission.Grant = 0;
          permission.Deny = 0;
        }
      }
    }

    /// <summary>
    /// Resets permissions (no grant &amp; no deny) on a specific page node, for all principals.
    /// </summary>
    /// <param name="node">The page node to reset.</param>
    public void UnsetPermissionsForPageNode(PageNode node, PageManager pageManagerInstance = null)
    {
      if (node == null)
        return;
      if (pageManagerInstance == null)
        pageManagerInstance = this.PageManager;
      pageManagerInstance.BreakPermiossionsInheritance((ISecuredObject) node);
      foreach (Telerik.Sitefinity.Security.Model.Permission activePermission in (IEnumerable<Telerik.Sitefinity.Security.Model.Permission>) node.GetActivePermissions())
      {
        activePermission.Grant = 0;
        activePermission.Deny = 0;
      }
    }

    /// <summary>
    /// Sets permissions (grant) on a specific page node, for a collection of principals and a specific collection of actions in a permission set.
    /// </summary>
    /// <param name="node">The page node to set permissions for.</param>
    /// <param name="principals">The principals.</param>
    /// <param name="permissionSet">The permission set.</param>
    /// <param name="actions">The actions to grant.</param>
    public void SetPermissionsForPageNode(
      PageNode node,
      Guid[] principals,
      string permissionSet,
      string[] actions,
      PageManager pageManagerInstance = null)
    {
      if (node == null)
        return;
      if (pageManagerInstance == null)
        pageManagerInstance = this.PageManager;
      pageManagerInstance.BreakPermiossionsInheritance((ISecuredObject) node);
      foreach (Guid principal1 in principals)
      {
        Guid principal = principal1;
        Telerik.Sitefinity.Security.Model.Permission permission = node.GetActivePermissions().Where<Telerik.Sitefinity.Security.Model.Permission>((Expression<Func<Telerik.Sitefinity.Security.Model.Permission, bool>>) (p => p.PrincipalId == principal && p.ObjectId == node.Id && p.SetName == permissionSet)).FirstOrDefault<Telerik.Sitefinity.Security.Model.Permission>() ?? pageManagerInstance.CreatePermission(permissionSet, node.Id, principal);
        permission.GrantActions(true, actions);
        node.Permissions.Add(permission);
      }
    }

    private void SetNames(PageNode pageNode, PageElement pageConfig)
    {
      if (!string.IsNullOrEmpty(pageConfig.ResourceClassId))
      {
        this.SetNames(pageNode, pageConfig, CultureInfo.InvariantCulture);
        foreach (CultureElement culture in (ConfigElementCollection) this.ResConfig.Cultures)
        {
          CultureInfo cultureInfo = CultureInfo.GetCultureInfo(culture.UICulture);
          this.SetNames(pageNode, pageConfig, cultureInfo);
        }
        if (!string.IsNullOrEmpty(pageNode.UrlName[""]))
          return;
        pageNode.UrlName[""] = pageConfig.UrlName;
      }
      else
      {
        pageNode.UrlName = (Lstring) pageConfig.UrlName;
        pageNode.Title = (Lstring) pageConfig.MenuName;
        PageDataElement pageDataElement = pageConfig as PageDataElement;
        PageData pageData = pageNode.GetPageData();
        if (pageData == null || pageDataElement == null)
          return;
        pageData.Title = (Lstring) pageConfig.MenuName;
        pageData.Description = (Lstring) pageConfig.Description;
        pageData.HtmlTitle = (Lstring) pageDataElement.HtmlTitle;
      }
    }

    private void SetNames(PageNode pageNode, PageElement pageConfig, CultureInfo culture)
    {
      string str1 = Res.Get(pageConfig.ResourceClassId, pageConfig.UrlName, culture, false, false);
      if (!string.IsNullOrEmpty(str1))
        pageNode.UrlName[culture.Name] = str1;
      if (!string.IsNullOrEmpty(Res.Get(pageConfig.ResourceClassId, pageConfig.MenuName, culture, false, false)))
        pageNode.Title = (Lstring) Res.Expression(pageConfig.ResourceClassId, pageConfig.MenuName);
      if (!string.IsNullOrEmpty(Res.Get(pageConfig.ResourceClassId, pageConfig.Description, culture, false, false)))
        pageNode.Description = (Lstring) Res.Expression(pageConfig.ResourceClassId, pageConfig.Description);
      if (!(pageConfig is PageDataElement pageDataElement) || string.IsNullOrEmpty(pageDataElement.HtmlTitle))
        return;
      string str2 = Res.Get(pageConfig.ResourceClassId, pageDataElement.HtmlTitle, culture, false, false);
      PageData pageData = pageNode.GetPageData(culture);
      if (pageData == null || string.IsNullOrEmpty(str2))
        return;
      pageData.HtmlTitle = (Lstring) Res.Expression(pageConfig.ResourceClassId, pageDataElement.HtmlTitle);
    }

    /// <summary>Gets the Sitefinity node.</summary>
    /// <param name="rootNode">The root node.</param>
    /// <returns></returns>
    protected virtual PageNode GetSitefinityNode(PageNode rootNode)
    {
      PageNode sitefinityNode = rootNode.Nodes.FirstOrDefault<PageNode>((Func<PageNode, bool>) (n => n.Id == SiteInitializer.SitefinityNodeId));
      if (sitefinityNode == null)
      {
        sitefinityNode = this.CreatePageNode(SiteInitializer.SitefinityNodeId, rootNode, NodeType.Group);
        sitefinityNode.Name = "Sitefinity";
        sitefinityNode.Title = (Lstring) "Sitefinity";
        sitefinityNode.UrlName = (Lstring) "Sitefinity";
        sitefinityNode.Description = (Lstring) "By default all admin pages are below Sitefinity node.";
      }
      return sitefinityNode;
    }

    /// <summary>Creates the control panel.</summary>
    /// <param name="module">The module.</param>
    /// <returns></returns>
    protected virtual PageControl CreateControlPanel(
      IModule module,
      bool isBackendObject)
    {
      PageControl control = this.PageManager.CreateControl<PageControl>(isBackendObject);
      control.ObjectType = typeof (ControlPanelBuilder).FullName;
      control.PlaceHolder = "Content";
      this.PageManager.ReadProperties((object) new ControlPanelBuilder()
      {
        ModuleName = module.Name,
        IsApplicationModule = module.IsApplicationModule
      }, (ObjectData) control);
      return control;
    }

    /// <summary>Creates the control panel.</summary>
    /// <param name="controlPanel">The control panel.</param>
    /// <returns></returns>
    protected virtual PageControl CreateControlPanel(
      Control controlPanel,
      bool isBackendObject)
    {
      PageControl control = this.PageManager.CreateControl<PageControl>(isBackendObject);
      control.ObjectType = controlPanel.GetType().AssemblyQualifiedName;
      control.PlaceHolder = "Content";
      this.PageManager.ReadProperties((object) controlPanel, (ObjectData) control);
      control.SetDefaultPermissions((IControlManager) this.PageManager);
      return control;
    }

    /// <summary>Creates the basic page templates.</summary>
    protected virtual void CreatePageTemplates()
    {
      HierarchicalTaxonomy taxonomy = this.TaxonomyManager.CreateTaxonomy<HierarchicalTaxonomy>(SiteInitializer.PageTemplatesTaxonomyId);
      taxonomy.Name = Config.Get<PagesConfig>().PageTemplatesTaxonomyName;
      taxonomy.Title = (Lstring) "Page Templates";
      taxonomy.TaxonName = (Lstring) "Page template category";
      taxonomy.Description = (Lstring) "Provides page templates categorization.";
      Telerik.Sitefinity.Security.Model.Permission permission1 = this.TaxonomyManager.Provider.CreatePermission("General", taxonomy.Id, SecurityManager.EveryoneRole.Id);
      permission1.GrantActions(true, "View");
      taxonomy.Permissions.Add(permission1);
      Telerik.Sitefinity.Security.Model.Permission permission2 = this.TaxonomyManager.Provider.CreatePermission("PageTemplates", taxonomy.Id, SecurityManager.BackEndUsersRole.Id);
      permission2.GrantActions(true, "View", "Create");
      taxonomy.Permissions.Add(permission2);
      HierarchicalTaxon taxon1 = this.TaxonomyManager.CreateTaxon<HierarchicalTaxon>(SiteInitializer.BackendTemplatesCategoryId);
      taxon1.Name = "Backend";
      taxon1.UrlName = (Lstring) "Backend";
      taxon1.RenderAsLink = false;
      Res.SetLstring(taxon1.Title, typeof (PageResources), "BackendTemplates");
      taxon1.Description = (Lstring) "Represents category for backend page templates.";
      taxonomy.Taxa.Add((Taxon) taxon1);
      HierarchicalTaxon taxon2 = this.TaxonomyManager.CreateTaxon<HierarchicalTaxon>(SiteInitializer.CustomTemplatesCategoryId);
      taxon2.Name = "Custom";
      taxon2.UrlName = (Lstring) "Custom";
      taxon2.RenderAsLink = false;
      Res.SetLstring(taxon2.Title, typeof (PageResources), "CustomTemplates");
      taxon2.Description = (Lstring) "Represents category for custom page templates.";
      taxonomy.Taxa.Add((Taxon) taxon2);
      HierarchicalTaxon taxon3 = this.TaxonomyManager.CreateTaxon<HierarchicalTaxon>(SiteInitializer.BasicTemplatesCategoryId);
      taxon3.Name = "Basic";
      taxon3.UrlName = (Lstring) "Basic";
      taxon3.RenderAsLink = false;
      Res.SetLstring(taxon3.Title, typeof (PageResources), "BasicTemplates");
      taxon3.Description = (Lstring) "Represents category for basic page templates.";
      taxonomy.Taxa.Add((Taxon) taxon3);
      new TemplateInitializer(this.PageManager).InvokeAllMethods(taxon3);
    }

    /// <summary>
    /// Copies an embedded control template from the given embedded resource to the database.
    /// </summary>
    /// <param name="initializer">The initializer.</param>
    /// <param name="embeddedResourcePath">The embedded resource path.</param>
    /// <param name="controlType">Type of the control to which the template belongs.</param>
    /// <param name="name">The name of the template.</param>
    public void RegisterControlTemplate(
      string embeddedResourcePath,
      string controlType,
      string name,
      string condition = null,
      string areaName = null,
      string dataType = "ASP_NET_TEMPLATE",
      string resourceAssemblyName = "Telerik.Sitefinity.Resources",
      string friendlyControlName = null)
    {
      this.RegisterControlTemplate(embeddedResourcePath, controlType, name, this.PageManager.Provider.GetNewGuid(), condition, areaName, dataType, resourceAssemblyName, friendlyControlName);
    }

    /// <summary>
    /// Copies an embedded control template from the given embedded resource to the database.
    /// </summary>
    /// <param name="initializer">The initializer.</param>
    /// <param name="embeddedResourcePath">The embedded resource path.</param>
    /// <param name="controlType">Type of the control to which the template belongs.</param>
    /// <param name="name">The name of the template.</param>
    /// <param name="id">The id of the newly created template in the database</param>
    /// <param name="condition">Parameter containing a string data used to filter a given presentation data.</param>
    public void RegisterControlTemplate(
      string embeddedResourcePath,
      string controlType,
      string name,
      Guid id,
      string condition = null,
      string areaName = null,
      string dataType = "ASP_NET_TEMPLATE",
      string resourceAssemblyName = "Telerik.Sitefinity.Resources",
      string friendlyControlName = null)
    {
      SiteInitializer.RegisterControlTemplate(this.PageManager, embeddedResourcePath, controlType, name, id, condition, areaName, dataType, resourceAssemblyName, friendlyControlName);
    }

    /// <summary>
    /// Copies an embedded control template from the given embedded resource to the database.
    /// </summary>
    /// <param name="initializer">The initializer.</param>
    /// <param name="embeddedResourcePath">The embedded resource path.</param>
    /// <param name="controlType">Type of the control to which the template belongs.</param>
    /// <param name="name">The name of the template.</param>
    /// <param name="id">The id of the newly created template in the database</param>
    /// <param name="condition">Parameter containing a string data used to filter a given presentation data.</param>
    internal static ControlPresentation RegisterControlTemplate(
      PageManager manager,
      string embeddedResourcePath,
      string controlType,
      string name,
      Guid id,
      string condition = null,
      string areaName = null,
      string dataType = "ASP_NET_TEMPLATE",
      string resourceAssemblyName = "Telerik.Sitefinity.Resources",
      string friendlyControlName = null)
    {
      ControlPresentation template = manager.GetPresentationItems<ControlPresentation>().Where<ControlPresentation>((Expression<Func<ControlPresentation, bool>>) (t => t.DataType == dataType && t.ControlType == controlType && t.EmbeddedTemplateName == embeddedResourcePath)).SingleOrDefault<ControlPresentation>();
      if (template == null)
      {
        template = manager.CreatePresentationItem<ControlPresentation>(id);
        template.DataType = dataType;
        template.ControlType = controlType;
        template.EmbeddedTemplateName = embeddedResourcePath;
        template.IsDifferentFromEmbedded = false;
      }
      template.Name = name;
      template.Condition = condition;
      Type type = TypeResolutionService.ResolveType(controlType);
      SiteInitializer.SetAreaAndFriendlyName(template, type, areaName, friendlyControlName);
      template.ResourceAssemblyName = resourceAssemblyName;
      return template;
    }

    public bool InstallEmbeddedVirtualPath(
      string virtualPath,
      string embeddedPath,
      Assembly assembly)
    {
      if (virtualPath == null)
        throw new ArgumentNullException(nameof (virtualPath));
      if (embeddedPath == null)
        throw new ArgumentNullException(nameof (embeddedPath));
      if (assembly != (Assembly) null)
        embeddedPath = embeddedPath + "," + assembly.GetName().Name;
      VirtualPathSettingsConfig config = this.Context.GetConfig<VirtualPathSettingsConfig>();
      if (config.VirtualPaths.ContainsKey(virtualPath))
        return false;
      VirtualPathElement element = new VirtualPathElement((ConfigElement) config.VirtualPaths)
      {
        VirtualPath = virtualPath,
        ResourceLocation = embeddedPath,
        ResolverName = "EmbeddedResourceResolver"
      };
      config.VirtualPaths.Add(element);
      return true;
    }

    /// <summary>Gets the page count.</summary>
    /// <value>The page count.</value>
    protected IDictionary<Guid, int> PageCount
    {
      get
      {
        if (this.pageCount == null)
          this.pageCount = new Dictionary<Guid, int>();
        return (IDictionary<Guid, int>) this.pageCount;
      }
    }

    /// <summary>Gets the resources configuration element.</summary>
    /// <value>The res config.</value>
    protected ResourcesConfig ResConfig
    {
      get
      {
        if (this.resConfig == null)
          this.resConfig = Config.Get<ResourcesConfig>();
        return this.resConfig;
      }
    }

    /// <summary>Initializes user profiles</summary>
    private void InitializeUserProfiles()
    {
      if (this.MetadataManager.GetMetaType(typeof (SitefinityProfile)) == null)
        this.MetadataManager.CreateMetaTypeDescription(this.MetadataManager.CreateMetaType(typeof (SitefinityProfile)).Id).UserFriendlyName = "Basic profile";
      this.InitializeUserProfilesTemplates();
    }

    private void InitializeUserProfilesTemplates()
    {
      this.RegisterControlTemplate("Telerik.Sitefinity.Resources.Templates.Frontend.Security.UserProfileReadView.ascx", typeof (UserProfileDetailReadView).FullName, "Article-like");
      this.RegisterControlTemplate("Telerik.Sitefinity.Resources.Templates.Frontend.Security.UserProfileReadListLikeView.ascx", typeof (UserProfileDetailReadView).FullName, "List-like");
      this.RegisterControlTemplate("Telerik.Sitefinity.Resources.Templates.Frontend.Security.UserProfileReadAutoGeneratedFieldsView.ascx", typeof (UserProfileDetailReadView).FullName, "Auto generated fields");
      this.RegisterControlTemplate("Telerik.Sitefinity.Resources.Templates.Frontend.Security.UserProfileReadViewNoUser.ascx", typeof (UserProfileDetailReadView).FullName, "Not logged", "anonymousUser", friendlyControlName: Res.Get<UserProfilesResources>().ProfileNotLoggedUser);
      this.RegisterControlTemplate("Telerik.Sitefinity.Resources.Templates.Frontend.Security.UserProfileWriteView.ascx", typeof (UserProfileDetailWriteView).FullName, "Profile form", "editMode");
      this.RegisterControlTemplate("Telerik.Sitefinity.Resources.Templates.Frontend.Security.UserProfileWriteAutoGeneratedFieldsView.ascx", typeof (UserProfileDetailWriteView).FullName, "Auto generated fields", "editMode");
      this.RegisterControlTemplate("Telerik.Sitefinity.Resources.Templates.Frontend.Security.UserChangePasswordView.ascx", typeof (UserChangePasswordView).FullName, "Default", "changePasswordMode");
      this.RegisterControlTemplate("Telerik.Sitefinity.Resources.Templates.Frontend.Security.UserChangePasswordQuestionAndAnswerView.ascx", typeof (UserChangePasswordQuestionAndAnswerView).FullName, "Default", "changeQuestionAndAnswerMode");
      this.RegisterControlTemplate("Telerik.Sitefinity.Resources.Templates.Frontend.Security.UserChangePasswordWidget.ascx", typeof (UserChangePasswordWidget).FullName, "Change password");
      this.RegisterControlTemplate("Telerik.Sitefinity.Resources.Templates.Frontend.Security.UserProfileMasterView.ascx", typeof (UserProfileMasterView).FullName, "Names and details");
      this.RegisterControlTemplate("Telerik.Sitefinity.Resources.Templates.Frontend.Security.UserProfileMasterViewNamesOnly.ascx", typeof (UserProfileMasterView).FullName, "Names only");
      this.RegisterControlTemplate("Telerik.Sitefinity.Resources.Templates.Frontend.Security.RegistrationFormBasic.ascx", typeof (RegistrationForm).FullName, "Basic fields only");
      this.RegisterControlTemplate("Telerik.Sitefinity.Resources.Templates.Frontend.Security.RegistrationForm.ascx", typeof (RegistrationForm).FullName, "All fields");
      this.RegisterControlTemplate("Telerik.Sitefinity.Resources.Templates.Frontend.Security.RegistrationSuccessEmail.ascx", typeof (RegistrationForm).FullName, "Success email", RegistrationEmailType.Success.ToString(), dataType: "EMAIL_TEMPLATE");
      this.RegisterControlTemplate("Telerik.Sitefinity.Resources.Templates.Frontend.Security.RegistrationConfirmationEmail.ascx", typeof (RegistrationForm).FullName, "Confirmation email", RegistrationEmailType.Confirmation.ToString(), dataType: "EMAIL_TEMPLATE");
    }

    internal void InitializeNavigationControlTemplates() => this.RegisterControlTemplate("Telerik.Sitefinity.Resources.Templates.PublicControls.Breadcrumb.ascx", typeof (Telerik.Sitefinity.Web.UI.NavigationControls.Breadcrumb.Breadcrumb).FullName, "Breadcrumb");

    private void InitializeLightNavigationControlTemplates()
    {
      this.RegisterControlTemplate("Telerik.Sitefinity.Resources.Templates.Frontend.Navigation.HorizontalList.ascx", typeof (LightNavigationControl).FullName, "Horizontal (one-level)");
      this.RegisterControlTemplate("Telerik.Sitefinity.Resources.Templates.Frontend.Navigation.HorizontalWithDropDownMenusList.ascx", typeof (LightNavigationControl).FullName, "Horizontal with dropdown menus");
      this.RegisterControlTemplate("Telerik.Sitefinity.Resources.Templates.Frontend.Navigation.HorizontalWithTabsList.ascx", typeof (LightNavigationControl).FullName, "Horizontal with tabs (up to 2 levels)");
      this.RegisterControlTemplate("Telerik.Sitefinity.Resources.Templates.Frontend.Navigation.VerticalList.ascx", typeof (LightNavigationControl).FullName, "Vertical (one-level)");
      this.RegisterControlTemplate("Telerik.Sitefinity.Resources.Templates.Frontend.Navigation.VerticalWithSubLevelsList.ascx", typeof (LightNavigationControl).FullName, "Vertical with sublevels (treeview)");
      this.RegisterControlTemplate("Telerik.Sitefinity.Resources.Templates.Frontend.Navigation.SitemapInColumnsList.ascx", typeof (LightNavigationControl).FullName, "Sitemap in columns (up to 2 levels)");
      this.RegisterControlTemplate("Telerik.Sitefinity.Resources.Templates.Frontend.Navigation.SitemapInRowsList.ascx", typeof (LightNavigationControl).FullName, "Sitemap in rows (up to 2 levels)");
      SiteInitializer.RegisterControlTemplate(this.PageManager, "Telerik.Sitefinity.Resources.Templates.Frontend.Navigation.ToggleMenu.ascx", typeof (NavTransformationTemplate).FullName, "Toggle menu", this.PageManager.Provider.GetNewGuid()).NameForDevelopers = "ToggleMenu";
      SiteInitializer.RegisterControlTemplate(this.PageManager, "Telerik.Sitefinity.Resources.Templates.Frontend.Navigation.Dropdown.ascx", typeof (NavTransformationTemplate).FullName, "Dropdown", this.PageManager.Provider.GetNewGuid()).NameForDevelopers = "Dropdown";
    }

    /// <summary>Adds the supported permission sets to security root.</summary>
    /// <typeparam name="TManager">The type of the manager.</typeparam>
    /// <param name="supportedPermissionSets">The supported permission sets.</param>
    /// <exception cref="T:System.ArgumentNullException">supportedPermissionSets is null or empty.</exception>
    internal void AddSupportedPermissionSetsToSecurityRoot<TManager>(
      params string[] supportedPermissionSets)
      where TManager : IManager
    {
      if (supportedPermissionSets == null || ((IEnumerable<string>) supportedPermissionSets).Count<string>() == 0)
        throw new ArgumentNullException(nameof (supportedPermissionSets));
      foreach (DataProviderBase staticProvider in ManagerBase.GetManager(typeof (TManager)).StaticProviders)
      {
        TManager manager;
        SecurityRoot realSecurityRoot = this.GetRealSecurityRoot<TManager>(staticProvider.Name, out manager);
        List<string> list = ((IEnumerable<string>) realSecurityRoot.SupportedPermissionSets).ToList<string>();
        foreach (string supportedPermissionSet in supportedPermissionSets)
        {
          if (!list.Contains(supportedPermissionSet))
            list.Add(supportedPermissionSet);
        }
        if (((IEnumerable<string>) realSecurityRoot.SupportedPermissionSets).Count<string>() < list.Count<string>())
        {
          realSecurityRoot.SupportedPermissionSets = list.ToArray<string>();
          manager.Provider.CommitTransaction();
        }
      }
    }

    /// <summary>
    /// Upgrades legacy multiple choice fields for static types and pages
    /// </summary>
    internal void UpgradeLegacyMultipleChoiceCustomFields(Type clrType)
    {
      MetadataManager manager = MetadataManager.GetManager();
      MetaType metaType = manager.GetMetaTypes().Where<MetaType>((Expression<Func<MetaType, bool>>) (type => !type.IsDynamic && type.ClassName == clrType.Name)).FirstOrDefault<MetaType>();
      if (metaType == null)
        return;
      IEnumerable<MetaField> metaFields = metaType.Fields.Where<MetaField>((Func<MetaField, bool>) (x => x.ClrType == typeof (string).FullName));
      PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(TypeResolutionService.ResolveType(metaType.FullTypeName));
      foreach (MetaField metaField in metaFields)
      {
        PropertyDescriptor descriptor = properties.Find(metaField.FieldName, false);
        if (descriptor.IsMultipleChoice())
        {
          WcfFieldDefinition wcfDefinition = WcfDefinitionBuilder.GetWcfDefinition(descriptor.PropertyType, descriptor.ComponentType, metaField.FieldName, "editCustomField", descriptor.IsLocalizable);
          metaField.ChoiceFieldDefinition = wcfDefinition.Choices;
        }
      }
      manager.SaveChanges();
    }

    /// <summary>
    /// Removes the supported permission sets to security root.
    /// </summary>
    /// <typeparam name="TManager">The type of the manager.</typeparam>
    /// <param name="supportedPermissionSets">The supported permission sets.</param>
    /// <exception cref="T:System.ArgumentNullException">supportedPermissionSets is null or empty.</exception>
    internal void RemoveSupportedPermissionSetsToSecurityRoot<TManager>(
      params string[] supportedPermissionSets)
      where TManager : IManager
    {
      if (supportedPermissionSets == null || ((IEnumerable<string>) supportedPermissionSets).Count<string>() == 0)
        throw new ArgumentNullException(nameof (supportedPermissionSets));
      foreach (DataProviderBase staticProvider in ManagerBase.GetManager(typeof (TManager)).StaticProviders)
      {
        TManager manager;
        SecurityRoot realSecurityRoot = this.GetRealSecurityRoot<TManager>(staticProvider.Name, out manager);
        List<string> list = ((IEnumerable<string>) realSecurityRoot.SupportedPermissionSets).ToList<string>();
        foreach (string supportedPermissionSet in supportedPermissionSets)
        {
          if (list.Contains(supportedPermissionSet))
            list.Remove(supportedPermissionSet);
        }
        if (((IEnumerable<string>) realSecurityRoot.SupportedPermissionSets).Count<string>() > list.Count<string>())
        {
          realSecurityRoot.SupportedPermissionSets = list.ToArray<string>();
          manager.Provider.CommitTransaction();
        }
      }
    }

    private SecurityRoot GetRealSecurityRoot<TManager>(
      string providerName,
      out TManager manager)
      where TManager : IManager
    {
      manager = (TManager) ManagerBase.GetManagerInTransaction(typeof (TManager), providerName, "upgrade_view_backend_link_80");
      Guid id = manager.Provider.GetSecurityRoot().Id;
      return manager.GetItem(typeof (SecurityRoot), id) as SecurityRoot;
    }

    /// <summary>
    /// Describes a factory for upgrade methods based on specific instance.
    /// </summary>
    internal class UpgradeMethodFactory
    {
      /// <summary>
      /// Gets the upgrades methods of the specified instance marked with the specified attribute.
      /// </summary>
      /// <param name="upgradeFrom">The upgrade from.</param>
      /// <param name="instance">The instance.</param>
      /// <param name="upgradeAttributeType">Type of the upgrade attribute.</param>
      /// <returns></returns>
      internal static List<UpgradeMethod> GetUpgrades<TUpgradeInfoAttribute>(
        int upgradeFrom,
        object instance)
        where TUpgradeInfoAttribute : UpgradeInfoAttribute
      {
        List<UpgradeMethod> upgrades = new List<UpgradeMethod>();
        ((IEnumerable<MethodInfo>) instance.GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic)).ToList<MethodInfo>().ForEach((Action<MethodInfo>) (method =>
        {
          if (!(((IEnumerable<Attribute>) Attribute.GetCustomAttributes((MemberInfo) method)).Where<Attribute>((Func<Attribute, bool>) (attrib => attrib.GetType() == typeof (TUpgradeInfoAttribute))).FirstOrDefault<Attribute>() is TUpgradeInfoAttribute upgradeAttribute2) || upgradeFrom >= upgradeAttribute2.UpgradeTo)
            return;
          upgrades.Add(new UpgradeMethod(instance, method, (UpgradeInfoAttribute) upgradeAttribute2));
        }));
        return upgrades;
      }
    }
  }
}
