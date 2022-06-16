// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.ModuleBuilderModule
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Routing;
using System.Web.UI;
using Telerik.Microsoft.Practices.Unity;
using Telerik.OpenAccess.Exceptions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Dashboard;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Configuration;
using Telerik.Sitefinity.Data.DataSource;
using Telerik.Sitefinity.Data.Events;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Data.Statistic;
using Telerik.Sitefinity.DynamicModules.Builder.Configuration;
using Telerik.Sitefinity.DynamicModules.Builder.Install;
using Telerik.Sitefinity.DynamicModules.Builder.Install.DefinitionGenerators;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Builder.Upgrade;
using Telerik.Sitefinity.DynamicModules.Builder.Web;
using Telerik.Sitefinity.DynamicModules.Builder.Web.UI;
using Telerik.Sitefinity.DynamicModules.Configuration;
using Telerik.Sitefinity.DynamicModules.Data;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.DynamicModules.Web;
using Telerik.Sitefinity.DynamicModules.Web.Services;
using Telerik.Sitefinity.DynamicModules.Web.UI.Backend;
using Telerik.Sitefinity.DynamicModules.Web.UI.Frontend;
using Telerik.Sitefinity.Fluent.Modules;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.GeoLocations;
using Telerik.Sitefinity.GeoLocations.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.Localization;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Modules.Security.Permissions.Web.UI.Views;
using Telerik.Sitefinity.Packaging.Content;
using Telerik.Sitefinity.Packaging.Structure;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.RecycleBin;
using Telerik.Sitefinity.RecycleBin.ItemFactories;
using Telerik.Sitefinity.Restriction;
using Telerik.Sitefinity.Scheduling;
using Telerik.Sitefinity.Scheduling.Model;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Events;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.UsageTracking.TrackingReporters;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Versioning;
using Telerik.Sitefinity.Versioning.Web.UI.Config;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Services.Contracts;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Config;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Enums;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Enums;
using Telerik.Sitefinity.Workflow.Configuration;

namespace Telerik.Sitefinity.DynamicModules.Builder
{
  /// <summary>
  /// The module class for module builder module which allows users to build dynamic modules through
  /// user interface.
  /// </summary>
  internal class ModuleBuilderModule : 
    SecuredModuleBase,
    IDashboardEnabledModule,
    ITypeSettingsProvider,
    ITrackingReporter
  {
    public const string ModuleName = "ModuleBuilder";
    public static readonly Guid moduleBuilderNodeId = new Guid("634869EE-B21F-4A41-89DA-011A0AB26CC2");
    public static readonly Guid moduleBuilderDashboardPageId = new Guid("85BAE629-AACF-456A-8B5B-BA1497A383E6");
    public static readonly Guid contentTypePageGroupId = new Guid("B45F2334-7887-4E69-BB93-29E84497536B");
    public static readonly Guid contentTypeDashboardPageId = new Guid("70AEF8E7-1BF8-49D0-A7C9-21B90FB9C7A6");
    public static readonly Guid contentTypeCodeReferencePageId = new Guid("33101D6E-5D9B-4E3E-BF73-25F4F30C2B8A");
    public static readonly Guid backendScreensTweaksPageId = new Guid("93FD65BA-1E94-4AB1-B388-54ACC7BE7142");
    public const string FrontEndDataServiceUrl = "Sitefinity/Frontend/Services/DynamicModules/Data.svc/";

    /// <summary>
    /// Gets the landing page id for each module inherit from <see cref="T:Telerik.Sitefinity.Services.SecuredModuleBase" /> class.
    /// </summary>
    /// <value>The landing page id.</value>
    public override Guid LandingPageId => ModuleBuilderModule.moduleBuilderDashboardPageId;

    /// <summary>
    /// Gets the CLR types of all data managers provided by this module.
    /// </summary>
    /// <value>An array of <see cref="T:System.Type" /> objects.</value>
    public override Type[] Managers => new Type[2]
    {
      typeof (ModuleBuilderManager),
      typeof (DynamicModuleManager)
    };

    protected internal override ManagersInitializationMode ManagersInitializationMode => ManagersInitializationMode.OnStartup;

    /// <inheritdoc />
    public override void Initialize(ModuleSettings settings)
    {
      base.Initialize(settings);
      App.WorkWith().Module(settings.Name).Initialize().Configuration<ModuleBuilderConfig>().Configuration<DynamicModulesConfig>().Localization<ModuleBuilderResources>().SitemapFilter<DynamicModuleNodeFilter>().WebService(typeof (DataService), "Sitefinity/Frontend/Services/DynamicModules/Data.svc/").Route("ModuleBuilderExportModule", (RouteBase) new Route("Sitefinity/import-export-module/{action}/{moduleId}", (IRouteHandler) new ImportExportModuleRouteHandler())).Route("ContentTypeExportItems", (RouteBase) new Route("Sitefinity/export-contenttype-items/{moduleId}", (IRouteHandler) new ContentTypeItemsExportRouteHandler()));
      SystemManager.TypeRegistry.RegisterInitializer(new SitefinityTypeRegistryInitializer(this.RegisterDynamicModuleTypes));
      this.RegisterIoCTypes();
      this.InterceptDataEvents();
      this.RegisterConfigRestrictions();
    }

    /// <inheritdoc />
    public override void Load()
    {
      base.Load();
      foreach (IDynamicModule dynamicModule in ModuleBuilderManager.GetModules().Active())
        ModuleBuilderModule.LoadModule(dynamicModule);
      EventHub.Subscribe<IGeoLocatableEvent>(new SitefinityEventHandler<IGeoLocatableEvent>(this.HandleGeoLocationEvent));
    }

    internal static void LoadModule(IDynamicModule dynamicModule)
    {
      if (!(SystemManager.InitializeDynamicModule(dynamicModule, dynamicModule.Types) is DynamicAppModule))
        return;
      string name = dynamicModule.Name;
      if (!SystemManager.DataSourceRegistry.IsDataSourceRegistered(name))
      {
        DynamicModuleManager manager = DynamicModuleManager.GetManager();
        SystemManager.DataSourceRegistry.RegisterDataSource((Telerik.Sitefinity.Data.DataSource.IDataSource) new DynamicModuleDataSourceProxy(name, dynamicModule.Name, true, true, manager.GetProviderInfos(dynamicModule.Name)));
      }
      StatisticCache.Current.RegisterStatisticSupport<DynamicModuleStatisticSupport>(name);
      foreach (IDynamicModuleType type in dynamicModule.Types)
        ModuleInstallerHelper.InstallSearch(type);
    }

    internal static void UnloadModule(string moduleName, List<string> types)
    {
      SystemManager.RemoveDynamicModule(moduleName);
      SystemManager.DataSourceRegistry.UnregisterDataSource(moduleName);
      ModuleBuilderModule.NotifyDynamicTypeCacheDependencies((IEnumerable<string>) types);
    }

    private static void NotifyDynamicTypeCacheDependencies(IEnumerable<string> types)
    {
      IList<CacheDependencyKey> items = (IList<CacheDependencyKey>) new List<CacheDependencyKey>();
      foreach (string type in types)
      {
        CacheDependencyKey cacheDependencyKey = new CacheDependencyKey()
        {
          Key = type,
          Type = typeof (DynamicModule)
        };
        items.Add(cacheDependencyKey);
      }
      CacheDependency.Notify(items);
    }

    private void RegisterIoCTypes()
    {
      ObjectFactory.Container.RegisterType<IRecycleBinStrategy, RecycleBinDynamicContentStrategy>(typeof (DynamicModuleManager).FullName);
      ObjectFactory.Container.RegisterType<IRecycleBinStrategy, RecycleBinLibrariesStrategy>(typeof (LibrariesManager).FullName);
      ObjectFactory.Container.RegisterType<IRecycleBinItemFactory, DynamicContentRecycleBinFactory>(typeof (DynamicModuleManager).FullName);
      ObjectFactory.Container.RegisterType<DynamicModulePermissionSyncronizer>(typeof (DynamicModulePermissionSyncronizer).FullName);
      if (!ObjectFactory.IsTypeRegistered<IModuleBuilderModuleEventInterceptor>())
        ObjectFactory.Container.RegisterType<IModuleBuilderModuleEventInterceptor, ModuleBuilderModuleEventInterceptor>((LifetimeManager) new ContainerControlledLifetimeManager());
      ObjectFactory.Container.RegisterType<IDynamicModulePermissionHolderResolver, DynamicModulePermissionHolderResolver>((LifetimeManager) new ContainerControlledLifetimeManager());
      ContainerControlledLifetimeManager controlledLifetimeManager = new ContainerControlledLifetimeManager();
      try
      {
        ObjectFactory.Container.RegisterType<IStructureTransfer, DynamicStructureTransfer>(new DynamicStructureTransfer().GroupName, (LifetimeManager) controlledLifetimeManager);
      }
      catch
      {
        controlledLifetimeManager.Dispose();
        throw;
      }
      ObjectFactory.Container.RegisterType<IContentTransfer, DynamicContentTransfer>(new DynamicContentTransfer().Area, (LifetimeManager) new ContainerControlledLifetimeManager());
    }

    /// <summary>
    /// Subscribes the currently registered <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.IModuleBuilderModuleEventInterceptor" />.
    /// </summary>
    private void InterceptDataEvents() => ObjectFactory.Container.Resolve<IModuleBuilderModuleEventInterceptor>().Subscribe();

    private void RegisterDynamicModuleTypes(SitefinityTypeRegistry registry)
    {
      foreach (IDynamicModule module in ModuleBuilderManager.GetModules().Active())
        ModuleBuilderModule.RegisterDynamicModule(module, registry);
    }

    internal static void RegisterDynamicModule(
      IDynamicModule module,
      SitefinityTypeRegistry registry = null)
    {
      if (registry == null)
        registry = SystemManager.TypeRegistry;
      registry.Register(module.Name, new SitefinityType()
      {
        Parent = (string) null,
        PluralTitle = PluralsResolver.Instance.ToPlural(module.Title),
        SingularTitle = module.Title,
        ModuleName = module.Name,
        Kind = SitefinityTypeKind.Module
      });
      foreach (IDynamicModuleType type in module.Types)
        ModuleBuilderModule.RegisterDynamicModuleType(type, registry);
    }

    internal static void RegisterDynamicModuleType(
      IDynamicModuleType moduleType,
      SitefinityTypeRegistry registry = null)
    {
      if (registry == null)
        registry = SystemManager.TypeRegistry;
      string moduleName = moduleType.ModuleName;
      IDynamicModuleType parentType = moduleType.ParentType;
      string str = parentType != null ? parentType.GetFullTypeName() : moduleName;
      registry.Register(moduleType.GetFullTypeName(), new SitefinityType()
      {
        Parent = str,
        PluralTitle = PluralsResolver.Instance.ToPlural(moduleType.DisplayName),
        SingularTitle = moduleType.DisplayName,
        ModuleName = moduleName,
        Kind = SitefinityTypeKind.Type
      });
      if (!SystemManager.IsModuleEnabled("Dashboard"))
        return;
      ObjectFactory.Resolve<IDashboardInitializer>().RegisterType(moduleType.GetFullTypeName());
    }

    internal static void UpdateDynamicModuleType(IDynamicModuleType moduleType)
    {
      ModuleBuilderModule.UnregisterDynamicModuleType(moduleType);
      ModuleBuilderModule.RegisterDynamicModuleType(moduleType);
    }

    internal static void UnregisterDynamicModule(
      IDynamicModule module,
      SitefinityTypeRegistry registry = null)
    {
      if (registry == null)
        registry = SystemManager.TypeRegistry;
      registry.Unregister(module.Name);
      foreach (IDynamicModuleType type in module.Types)
        ModuleBuilderModule.UnregisterDynamicModuleType(type, registry);
    }

    internal static void UnregisterDynamicModuleType(
      IDynamicModuleType moduleType,
      SitefinityTypeRegistry registry = null)
    {
      if (registry == null)
        registry = SystemManager.TypeRegistry;
      registry.Unregister(moduleType.GetFullTypeName());
    }

    private void RegisterConfigRestrictions() => CommandWidgetReadOnlyConfigRestrictionStrategy.Add("NavigateToContentTypes", RestrictionLevel.ReadOnlyConfigFile);

    void IDashboardEnabledModule.Configure(IDashboardInitializer initializer)
    {
      foreach (IDynamicModule dynamicModule in ModuleBuilderManager.GetModules().Active())
      {
        foreach (IDynamicModuleType type1 in dynamicModule.Types)
        {
          Type type2 = TypeResolutionService.ResolveType(type1.GetFullTypeName(), false);
          if (type2 != (Type) null)
            initializer.RegisterType(type2.FullName);
        }
      }
    }

    private string ResolveMapping(DynamicModuleField field)
    {
      if (field.FieldType == FieldType.Classification)
        return typeof (Taxon).FullName;
      if (field.FieldType != FieldType.Media && field.FieldType != FieldType.RelatedMedia)
        return (string) null;
      string lowerInvariant = field.MediaType.ToLowerInvariant();
      if (lowerInvariant == "image")
        return typeof (Image).FullName;
      if (lowerInvariant == "video")
        return typeof (Video).FullName;
      return lowerInvariant == "file" ? typeof (Document).FullName : string.Empty;
    }

    /// <inheritdoc />
    public override void Install(SiteInitializer initializer)
    {
      this.InstallPages(initializer);
      this.InstallFieldPermissionsConfiguration(initializer);
      ModuleBuilderManager.GetManager();
    }

    private void InstallPages(SiteInitializer initializer) => initializer.Installer.CreateModuleGroupPage(ModuleBuilderModule.moduleBuilderNodeId, "ContentTypes").PlaceUnder(CommonNode.Tools).SetOrdinal(1.5f).LocalizeUsing<ModuleBuilderResources>().SetTitleLocalized("ContentTypesModuleTitle").SetDescriptionLocalized("ContentTypesModuleDescription").SetUrlNameLocalized("ContentTypesModuleUrlName").AddChildPage(ModuleBuilderModule.moduleBuilderDashboardPageId, "ContentTypesDashboard").LocalizeUsing<ModuleBuilderResources>().SetTitleLocalized("ContentTypesModuleTitle").SetDescriptionLocalized("ContentTypesModuleDescription").SetUrlNameLocalized("ContentTypesDashboardUrlName").SetTemplate(SiteInitializer.BackendHtml5TemplateId).AddControl((Control) new ModuleBuilderDashboard()).Done().AddChildGroup(ModuleBuilderModule.contentTypePageGroupId, "ContentTypePageGroup").LocalizeUsing<ModuleBuilderResources>().SetTitleLocalized("ContentTypePageGroupTitle").SetDescriptionLocalized("ContentTypePageGroupDescription").SetUrlNameLocalized("ContentTypePageGroupUrlName").HideFromNavigation().AddChildPage(ModuleBuilderModule.contentTypeDashboardPageId, "ContentTypeDashboard").LocalizeUsing<ModuleBuilderResources>().SetTitleLocalized("ContentTypeDashboardTitle").SetDescriptionLocalized("ContentTypeDashboardDescription").SetUrlNameLocalized("ContentTypeDashboardUrlName").SetTemplate(SiteInitializer.BackendHtml5TemplateId).AddControl((Control) new ContentTypeDashboard()).Done().AddChildPage(ModuleBuilderModule.contentTypeCodeReferencePageId, "CodeReference").LocalizeUsing<ModuleBuilderResources>().SetTitleLocalized("CodeReferenceTitle").SetDescriptionLocalized("CodeReferenceDescription").SetUrlNameLocalized("CodeReferenceUrlName").SetTemplate(SiteInitializer.BackendHtml5TemplateId).AddControl((Control) new CodeReference()).Done().AddChildPage(ModuleBuilderModule.backendScreensTweaksPageId, "BackendScreensTweaks").LocalizeUsing<ModuleBuilderResources>().SetTitleLocalized("BackendScreensTweaksTitle").SetDescriptionLocalized("BackendScreensTweaksPageDescription").SetUrlNameLocalized("BackendScreensTweaksUrlName").SetTemplate(SiteInitializer.BackendHtml5TemplateId).AddControl((Control) new BackendScreensTweaks());

    /// <summary>
    /// Installs the permissions configuration for the specified module type field.
    /// </summary>
    /// <param name="field">The field.</param>
    private void InstallFieldPermissionsConfiguration(SiteInitializer initializer)
    {
      ConfigElementDictionary<string, Telerik.Sitefinity.Security.Configuration.Permission> permissions = initializer.Context.GetConfig<SecurityConfig>().Permissions;
      ModuleBuilderResources builderResources = Res.Get<ModuleBuilderResources>();
      string key = "DynamicFields";
      if (permissions.TryGetValue(key, out Telerik.Sitefinity.Security.Configuration.Permission _))
        return;
      Telerik.Sitefinity.Security.Configuration.Permission element = new Telerik.Sitefinity.Security.Configuration.Permission((ConfigElement) permissions)
      {
        Name = key,
        Title = builderResources.TypePermissions,
        Description = builderResources.TypePermissionsDescription
      };
      permissions.Add(element);
      ConfigElementDictionary<string, SecurityAction> actions = element.Actions;
      actions.Add(new SecurityAction((ConfigElement) actions)
      {
        Name = "View",
        Type = SecurityActionTypes.View,
        Title = builderResources.ViewItemAction,
        Description = builderResources.ViewItemDescription
      });
      actions.Add(new SecurityAction((ConfigElement) actions)
      {
        Name = "Modify",
        Type = SecurityActionTypes.Modify,
        Title = builderResources.ModifyItemAction,
        Description = string.Format(builderResources.ModifyItemDescription)
      });
      actions.Add(new SecurityAction((ConfigElement) actions)
      {
        Name = "ChangePermissions",
        Type = SecurityActionTypes.ChangePermissions,
        Title = builderResources.ChangeItemPermissions,
        Description = builderResources.ChangeItemPermissionsDescription
      });
    }

    /// <inheritdoc />
    public override void Upgrade(SiteInitializer initializer, Version upgradeFrom)
    {
      ModuleBuilderManager.GetManager();
      if (upgradeFrom.Build < 2368)
      {
        PageManager pageManager = initializer.PageManager;
        ModuleBuilderManager managerInTransaction = initializer.GetManagerInTransaction<ModuleBuilderManager>();
        foreach (DynamicModule dynamicModule in (IEnumerable<DynamicModule>) managerInTransaction.GetDynamicModules())
        {
          DynamicModule module = dynamicModule;
          managerInTransaction.LoadDynamicModuleGraph(module);
          if (module.Types != null && module.Types.Length != 0 && module.Types[0] != null)
          {
            DynamicModuleType type = module.Types[0];
            string dynamicMasterView = typeof (DynamicContentViewMaster).FullName;
            string dynamicDetailView = typeof (DynamicContentViewDetail).FullName;
            IQueryable<ControlPresentation> presentationItems = pageManager.GetPresentationItems<ControlPresentation>();
            Expression<Func<ControlPresentation, bool>> predicate = (Expression<Func<ControlPresentation, bool>>) (p => p.DataType == "ASP_NET_TEMPLATE" && (p.ControlType == dynamicMasterView || p.ControlType == dynamicDetailView) && p.AreaName == module.Title);
            foreach (ControlPresentation controlPresentation in (IEnumerable<ControlPresentation>) presentationItems.Where<ControlPresentation>(predicate))
              controlPresentation.Condition = type.GetFullTypeName();
          }
        }
      }
      if (upgradeFrom.Build < 3040)
      {
        ModuleBuilderManager managerInTransaction = initializer.GetManagerInTransaction<ModuleBuilderManager>();
        IQueryable<DynamicModule> dynamicModules = managerInTransaction.GetDynamicModules();
        if (dynamicModules.Count<DynamicModule>() > 0)
        {
          ContentViewConfig config1 = initializer.Context.GetConfig<ContentViewConfig>();
          ToolboxesConfig config2 = initializer.Context.GetConfig<ToolboxesConfig>();
          WorkflowConfig config3 = initializer.Context.GetConfig<WorkflowConfig>();
          DynamicModulesConfig config4 = initializer.Context.GetConfig<DynamicModulesConfig>();
          foreach (DynamicModule dynamicModule in (IEnumerable<DynamicModule>) dynamicModules)
          {
            Guid moduleId = dynamicModule.Id;
            IQueryable<DynamicModuleType> queryable = managerInTransaction.GetDynamicModuleTypes().Where<DynamicModuleType>((Expression<Func<DynamicModuleType, bool>>) (t => t.ParentModuleId == moduleId));
            string name = dynamicModule.Name;
            foreach (DynamicModuleType dynamicModuleType in (IEnumerable<DynamicModuleType>) queryable)
            {
              if (dynamicModule.Status != DynamicModuleStatus.NotInstalled)
              {
                this.UpgradeUrlData(dynamicModuleType, initializer);
                this.UpgradeAreaName(dynamicModule, dynamicModuleType, initializer);
                this.UpgradeConfigurations(dynamicModuleType, config1, name, config2, config3);
                this.UpgradePermissions(dynamicModule, dynamicModuleType, initializer, config4);
                this.UpgradeModulePages(dynamicModule, dynamicModuleType, initializer);
              }
              if (dynamicModule.Status == DynamicModuleStatus.Inactive)
                this.UpgradeInactiveModuleType(initializer, dynamicModule, dynamicModuleType, managerInTransaction, config2, config3);
              this.UpgradeFieldNamespace(dynamicModuleType, managerInTransaction);
            }
          }
        }
      }
      if (upgradeFrom.Build < 3595)
      {
        this.Upgrade_To_5_2(initializer);
        initializer.SaveChanges(false);
      }
      if (upgradeFrom.Build < 3860)
      {
        ModuleBuilderManager managerInTransaction = initializer.GetManagerInTransaction<ModuleBuilderManager>();
        IQueryable<DynamicModule> dynamicModules = managerInTransaction.GetDynamicModules();
        if (dynamicModules.Count<DynamicModule>() > 0)
        {
          foreach (DynamicModule dynamicModule in (IEnumerable<DynamicModule>) dynamicModules)
          {
            string[] array = DynamicModuleManager.GetManager().GetProviderInfos(dynamicModule.Name).Select<DataProviderInfo, string>((Func<DataProviderInfo, string>) (p => p.ProviderName)).ToArray<string>();
            Guid moduleId = dynamicModule.Id;
            IQueryable<DynamicModuleType> dynamicModuleTypes = managerInTransaction.GetDynamicModuleTypes();
            Expression<Func<DynamicModuleType, bool>> predicate = (Expression<Func<DynamicModuleType, bool>>) (t => t.ParentModuleId == moduleId);
            foreach (DynamicModuleType dynamicModuleType in (IEnumerable<DynamicModuleType>) dynamicModuleTypes.Where<DynamicModuleType>(predicate))
            {
              Type type = TypeResolutionService.ResolveType(dynamicModuleType.GetFullTypeName(), false);
              if (type != (Type) null)
                initializer.TryFixTaxonomyStatistics(type, array);
            }
          }
        }
        this.UpgradeTo53(initializer);
      }
      if (upgradeFrom.Build < 3910)
        this.UpgradeTo53SP1ModuleName(initializer);
      if (upgradeFrom.Build < 4100)
        this.AddContentLocatinonsLinks(initializer);
      if (upgradeFrom < SitefinityVersion.Sitefinity6_1)
        this.UpgradeContentTypesPagesModuleName(initializer);
      if (upgradeFrom < SitefinityVersion.Sitefinity6_2)
        this.UpgradeDynamicModuleDefinitions(initializer);
      if (upgradeFrom < SitefinityVersion.Sitefinity6_3)
        this.UpgradeDynamicModuleMirrorFieldInDefinitions(initializer);
      if (upgradeFrom < SitefinityVersion.Sitefinity7_0)
      {
        this.UpgradeDynamicModuleFieldAttributes(initializer);
        try
        {
          this.AddStatusFilterSidebarSection(initializer);
          this.AddSortingToTheToobarSection(initializer);
        }
        catch (Exception ex)
        {
          Log.Write((object) string.Format("FAILED : Failed to upgrade dynamic modules sidebar filters and sorting - {0}", (object) ex.Message), ConfigurationPolicy.UpgradeTrace);
          if (Telerik.Sitefinity.Abstractions.Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
            throw;
        }
        try
        {
          this.CreateHistoryVersionViewConfigs(initializer);
        }
        catch (Exception ex)
        {
          Log.Write((object) string.Format("FAILED : Failed to upgrade dynamic modules history config sections - {0}", (object) ex.Message), ConfigurationPolicy.UpgradeTrace);
          if (Telerik.Sitefinity.Abstractions.Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
            throw;
        }
      }
      if (upgradeFrom < SitefinityVersion.Sitefinity7_1)
      {
        this.UpgradeContentDefaultUrls(initializer);
        this.UpgradeDynamicModule(initializer, SitefinityVersion.Sitefinity7_1);
        this.UpgradeMetadataTypesWithParentId(initializer);
        this.AddDuplicateContentItems(initializer);
        this.AddRecycleBinLinkToDynamicTypesSideBar(initializer);
      }
      if (upgradeFrom < SitefinityVersion.Sitefinity7_3)
        this.UpgradeDynamicModulePermissions(initializer);
      if (!(upgradeFrom < SitefinityVersion.Sitefinity8_1))
        return;
      this.UpgradeDynamicModule(initializer, SitefinityVersion.Sitefinity8_1);
    }

    private void UpgradeDynamicModulePermissions(SiteInitializer initializer)
    {
      new DynamicModulesUpgradeStrategyTo73(initializer).Upgrade();
      SchedulingManager manager = SchedulingManager.GetManager();
      string taskName = typeof (DynamicContentPermissionsUpgradeTask).FullName;
      if (manager.GetTaskData().Any<ScheduledTaskData>((Expression<Func<ScheduledTaskData, bool>>) (t => t.TaskName == taskName)))
        return;
      DynamicContentPermissionsUpgradeTask task = new DynamicContentPermissionsUpgradeTask();
      task.Id = DynamicContentPermissionsUpgradeTask.TaskUpgradeGuid;
      task.ExecuteTime = DateTime.UtcNow.AddMinutes(1.0);
      task.NumberOfAttempts = 3;
      try
      {
        manager.AddTask((ScheduledTask) task);
        manager.SaveChanges();
      }
      catch (DuplicateKeyException ex)
      {
      }
    }

    private void UpgradeContentTypesPagesModuleName(SiteInitializer initializer)
    {
      ModuleBuilderManager managerInTransaction = initializer.GetManagerInTransaction<ModuleBuilderManager>();
      IQueryable<DynamicModule> dynamicModules = managerInTransaction.GetDynamicModules();
      Expression<Func<DynamicModule, bool>> predicate = (Expression<Func<DynamicModule, bool>>) (m => (int) m.Status != 0);
      foreach (DynamicModule dynamicModule1 in (IEnumerable<DynamicModule>) dynamicModules.Where<DynamicModule>(predicate))
      {
        DynamicModule dynamicModule = dynamicModule1;
        Guid moduleId = dynamicModule.Id;
        IQueryable<DynamicModuleType> queryable = managerInTransaction.GetDynamicModuleTypes().Where<DynamicModuleType>((Expression<Func<DynamicModuleType, bool>>) (t => t.ParentModuleId == moduleId));
        string name = dynamicModule.Name;
        PageManager pageManager = initializer.PageManager;
        PageNode pageNode1 = pageManager.GetPageNodes().FirstOrDefault<PageNode>((Expression<Func<PageNode, bool>>) (n => n.Id == dynamicModule.PageId));
        if (pageNode1 != null && string.IsNullOrEmpty(pageNode1.ModuleName))
          pageNode1.ModuleName = name;
        foreach (DynamicModuleType dynamicModuleType in (IEnumerable<DynamicModuleType>) queryable)
        {
          DynamicModuleType moduleType = dynamicModuleType;
          PageNode pageNode2 = pageManager.GetPageNodes().FirstOrDefault<PageNode>((Expression<Func<PageNode, bool>>) (n => n.Id == moduleType.PageId));
          if (pageNode2 != null)
            pageNode2.ModuleName = name;
        }
      }
    }

    private void HandleGeoLocationEvent(IGeoLocatableEvent geoLocationEvent)
    {
      if (!Telerik.Sitefinity.Configuration.Config.Get<SystemConfig>().GeoLocationSettings.EnableGeoLocationSearch)
        return;
      IGeoLocationService geoLocationService1 = SystemManager.GetGeoLocationService();
      switch (geoLocationEvent)
      {
        case GeoLocatableCreatedEvent _:
          using (Dictionary<string, Address>.Enumerator enumerator = geoLocationEvent.GeoLocations.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              KeyValuePair<string, Address> current = enumerator.Current;
              Address address = current.Value;
              if (address != null)
              {
                double? nullable = address.Latitude;
                if (nullable.HasValue)
                {
                  nullable = address.Longitude;
                  if (nullable.HasValue)
                  {
                    IGeoLocationService geoLocationService2 = geoLocationService1;
                    Guid empty = Guid.Empty;
                    string fullName = geoLocationEvent.ItemType.FullName;
                    string providerName = geoLocationEvent.ProviderName;
                    string key = current.Key;
                    Guid itemId = geoLocationEvent.ItemId;
                    nullable = address.Latitude;
                    double latitude = nullable.Value;
                    nullable = address.Longitude;
                    double longitude = nullable.Value;
                    geoLocationService2.UpdateLocation(empty, fullName, providerName, key, itemId, latitude, longitude);
                  }
                }
              }
            }
            break;
          }
        case GeoLocatableUpdatedEvent _:
          using (Dictionary<string, Address>.Enumerator enumerator = geoLocationEvent.GeoLocations.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              KeyValuePair<string, Address> current = enumerator.Current;
              Address address = current.Value;
              IGeoLocation location = geoLocationService1.GetLocation(geoLocationEvent.ItemId, geoLocationEvent.ItemType.FullName, current.Key, geoLocationEvent.ProviderName);
              Guid guid = location == null ? Guid.Empty : location.Id;
              if (address != null)
              {
                double? nullable = address.Latitude;
                if (nullable.HasValue)
                {
                  nullable = address.Longitude;
                  if (nullable.HasValue)
                  {
                    IGeoLocationService geoLocationService3 = geoLocationService1;
                    Guid id = guid;
                    string fullName = geoLocationEvent.ItemType.FullName;
                    string providerName = geoLocationEvent.ProviderName;
                    string key = current.Key;
                    Guid itemId = geoLocationEvent.ItemId;
                    nullable = address.Latitude;
                    double latitude = nullable.Value;
                    nullable = address.Longitude;
                    double longitude = nullable.Value;
                    geoLocationService3.UpdateLocation(id, fullName, providerName, key, itemId, latitude, longitude);
                    continue;
                  }
                }
              }
              if (location != null)
                geoLocationService1.DeleteLocation(location.Id);
            }
            break;
          }
        case GeoLocatableDeletedEvent _:
          using (Dictionary<string, Address>.Enumerator enumerator = geoLocationEvent.GeoLocations.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              KeyValuePair<string, Address> current = enumerator.Current;
              IGeoLocation location = geoLocationService1.GetLocation(geoLocationEvent.ItemId, geoLocationEvent.ItemType.FullName, current.Key, geoLocationEvent.ProviderName);
              if (location != null)
                geoLocationService1.DeleteLocation(location.Id);
            }
            break;
          }
      }
    }

    public override IList<SecurityRoot> GetSecurityRoots(bool getContextRootsOnly = true)
    {
      ModuleBuilderManager manager = ModuleBuilderManager.GetManager();
      IEnumerable<DataProviderBase> moduleProviders = this.GetModuleProviders((IManager) manager, getContextRootsOnly);
      Type type = manager.GetType();
      List<SecurityRoot> securityRoots = new List<SecurityRoot>();
      foreach (DataProviderBase dataProviderBase in moduleProviders)
      {
        if (dataProviderBase.GetSecurityRoot(true) is SecurityRoot securityRoot)
        {
          securityRoot.DataProviderName = dataProviderBase.Name;
          securityRoot.ManagerType = type;
          securityRoots.Add(securityRoot);
        }
      }
      return (IList<SecurityRoot>) securityRoots;
    }

    private IEnumerable<DataProviderBase> GetModuleProviders(
      IManager manager,
      bool getContextProvidersOnly = true)
    {
      return !getContextProvidersOnly ? manager.GetAllProviders() : manager.GetContextProviders();
    }

    [UpgradeInfo(Description = "Update module backend pages title localization.", FailMassage = "Failed to update module backend pages title localization.", Id = "154C1C25-9B0F-4BF1-8CKF-81345CF3BE9D", UpgradeTo = 7400)]
    private void UpdateBackendPagesTitleLocalization(SiteInitializer initializer) => this.InstallPages(initializer);

    [UpgradeInfo(Description = "Upgrades the dynamic module child types with the 'sfParent' field. Creates a column 'parent_id' in the DB for every child type.", FailMassage = "The dynamic module fields couldn't be updated with the 'sfParent' field.", Id = "98E91228-DC41-4C1F-BE00-CA6824328F91", UpgradeTo = 6800)]
    private void UpgradeSfParentField(SiteInitializer initializer)
    {
      ModuleBuilderManager managerInTransaction1 = initializer.GetManagerInTransaction<ModuleBuilderManager>();
      IQueryable<DynamicModule> dynamicModules = managerInTransaction1.Provider.GetDynamicModules();
      Expression<Func<DynamicModule, bool>> predicate1 = (Expression<Func<DynamicModule, bool>>) (x => (int) x.Status != 0);
      foreach (DynamicModule dynamicModule in (IEnumerable<DynamicModule>) dynamicModules.Where<DynamicModule>(predicate1))
      {
        managerInTransaction1.LoadDynamicModuleGraph(dynamicModule);
        Guid moduleId = dynamicModule.Id;
        IQueryable<DynamicModuleType> dynamicModuleTypes = managerInTransaction1.GetDynamicModuleTypes();
        Expression<Func<DynamicModuleType, bool>> predicate2 = (Expression<Func<DynamicModuleType, bool>>) (t => t.ParentModuleId == moduleId && t.ParentModuleTypeId != Guid.Empty);
        using (IEnumerator<DynamicModuleType> enumerator = dynamicModuleTypes.Where<DynamicModuleType>(predicate2).GetEnumerator())
        {
label_16:
          while (enumerator.MoveNext())
          {
            DynamicModuleType current = enumerator.Current;
            DynamicModuleManager managerInTransaction2 = initializer.GetManagerInTransaction<DynamicModuleManager>();
            using (new ElevatedModeRegion((IManager) managerInTransaction2))
            {
              Type itemType = TypeResolutionService.ResolveType(current.GetFullTypeName());
              int count = 0;
              while (true)
              {
                List<DynamicContent> list = managerInTransaction2.GetDataItems(itemType).Skip<DynamicContent>(count).Take<DynamicContent>(100).ToList<DynamicContent>();
                if (list.Count != 0)
                {
                  foreach (DynamicContent dynamicContent in list)
                  {
                    Guid systemParentId = dynamicContent.SystemParentId;
                    dynamicContent.SystemParentId = Guid.Empty;
                    dynamicContent.SystemParentId = systemParentId;
                  }
                  initializer.FlushChanges();
                  count += 100;
                }
                else
                  goto label_16;
              }
            }
          }
        }
      }
    }

    [UpgradeInfo(Description = "Upgrades the dynamic module fields with the 'IncludeInSitemap' field.", FailMassage = "The dynamic module fields couldn't be updated with the 'IncludeInSitemap' field.", Id = "5CFF973C-8945-44C0-B52A-3CE64BDEAB57", UpgradeTo = 6700)]
    private void UpgradeIncludeInSitemapField(SiteInitializer initializer)
    {
      ModuleBuilderManager managerInTransaction = initializer.GetManagerInTransaction<ModuleBuilderManager>();
      foreach (DynamicModule dynamicModule in (IEnumerable<DynamicModule>) managerInTransaction.Provider.GetDynamicModules())
      {
        if (dynamicModule.Status != DynamicModuleStatus.NotInstalled)
        {
          managerInTransaction.LoadDynamicModuleGraph(dynamicModule);
          Guid moduleId = dynamicModule.Id;
          IQueryable<DynamicModuleType> dynamicModuleTypes = managerInTransaction.GetDynamicModuleTypes();
          Expression<Func<DynamicModuleType, bool>> predicate = (Expression<Func<DynamicModuleType, bool>>) (t => t.ParentModuleId == moduleId);
          foreach (DynamicModuleType dynamicModuleType in (IEnumerable<DynamicModuleType>) dynamicModuleTypes.Where<DynamicModuleType>(predicate))
          {
            DynamicModuleType moduleType = dynamicModuleType;
            FieldsBackendSection section = managerInTransaction.GetFieldsBackendSections().Where<FieldsBackendSection>((Expression<Func<FieldsBackendSection, bool>>) (s => s.ParentTypeId == moduleType.Id && s.Name == "MoreOptions")).First<FieldsBackendSection>();
            List<DynamicModuleField> source = new List<DynamicModuleField>((IEnumerable<DynamicModuleField>) moduleType.Fields);
            if (source.Where<DynamicModuleField>((Func<DynamicModuleField, bool>) (f => f.Name == "IncludeInSitemap")).FirstOrDefault<DynamicModuleField>() == null)
            {
              DynamicModuleField includeInSitemapField = managerInTransaction.CreateIncludeInSitemapField(moduleType, section);
              includeInSitemapField.Ordinal = 0;
              source.Add(includeInSitemapField);
              moduleType.Fields = source.ToArray();
            }
          }
        }
      }
    }

    [UpgradeInfo(Description = "Upgrades the DynamicModuleConfig file to change the value of the 'allowMultiple' taxonomy field to true. This field was not taken into account until now.", FailMassage = "The taxonomy fields for the dynamic were not updated to allow creation of new taxa. So they should be manually updated in the from Administration > Settings > Advanced > DynamicModules settings.", Id = "6D7A17E6-533B-4D54-83B8-5D8A33B652A6", UpgradeTo = 5900)]
    private void UpgradeTaxonomyFields(SiteInitializer initializer)
    {
      ModuleBuilderManager managerInTransaction = initializer.GetManagerInTransaction<ModuleBuilderManager>();
      IQueryable<DynamicModule> dynamicModules = managerInTransaction.GetDynamicModules();
      if (dynamicModules.Count<DynamicModule>() <= 0)
        return;
      DynamicModulesConfig config = initializer.Context.GetConfig<DynamicModulesConfig>();
      foreach (DynamicModule dynamicModule in (IEnumerable<DynamicModule>) dynamicModules)
      {
        if (dynamicModule.Status != DynamicModuleStatus.NotInstalled)
        {
          Guid moduleId = dynamicModule.Id;
          IQueryable<DynamicModuleType> dynamicModuleTypes = managerInTransaction.GetDynamicModuleTypes();
          Expression<Func<DynamicModuleType, bool>> predicate = (Expression<Func<DynamicModuleType, bool>>) (t => t.ParentModuleId == moduleId);
          foreach (DynamicModuleType dynamicModuleType in (IEnumerable<DynamicModuleType>) dynamicModuleTypes.Where<DynamicModuleType>(predicate))
          {
            managerInTransaction.LoadDynamicModuleGraph(dynamicModule);
            ContentViewControlElement contentViewControl = config.ContentViewControls[ModuleNamesGenerator.GenerateContentViewDefinitionName(dynamicModuleType.GetFullTypeName())];
            if (contentViewControl != null)
            {
              this.UpgradeTaxonomyFieldsInView(contentViewControl, ModuleNamesGenerator.GenerateBackendEditViewName(dynamicModuleType.DisplayName));
              this.UpgradeTaxonomyFieldsInView(contentViewControl, ModuleNamesGenerator.GenerateBackendInsertViewName(dynamicModuleType.DisplayName));
              this.UpgradeTaxonomyFieldsInView(contentViewControl, ModuleNamesGenerator.GenerateBackendDuplicateViewName(dynamicModuleType.DisplayName));
            }
            foreach (DynamicModuleField dynamicModuleField in ((IEnumerable<DynamicModuleField>) dynamicModuleType.Fields).Where<DynamicModuleField>((Func<DynamicModuleField, bool>) (x => x.WidgetTypeName == typeof (TaxonField).FullName)).ToList<DynamicModuleField>())
              dynamicModuleField.CanCreateItemsWhileSelecting = true;
          }
        }
      }
    }

    private void UpgradeTaxonomyFieldsInView(
      ContentViewControlElement contentView,
      string detailViewName)
    {
      if (!(contentView.ViewsConfig[detailViewName] is DetailFormViewElement detailFormViewElement))
        return;
      foreach (ContentViewSectionElement section in (ConfigElementCollection) detailFormViewElement.Sections)
      {
        foreach (TaxonFieldDefinitionElement definitionElement in section.Fields.OfType<TaxonFieldDefinitionElement>().ToList<TaxonFieldDefinitionElement>())
        {
          if (object.Equals((object) definitionElement.FieldType, (object) typeof (FlatTaxonField)) || object.Equals((object) definitionElement.FieldType, (object) typeof (HierarchicalTaxonField)))
            definitionElement.AllowCreating = true;
        }
      }
    }

    /// <summary>
    /// Upgrade definitions for Dynamic Modules. Add new property "Message"
    /// </summary>
    /// <param name="initializer"></param>
    private void UpgradeDynamicModule(SiteInitializer initializer, Version upgradeTo)
    {
      ModuleBuilderManager managerInTransaction = initializer.GetManagerInTransaction<ModuleBuilderManager>();
      IQueryable<DynamicModule> dynamicModules = managerInTransaction.GetDynamicModules();
      if (dynamicModules.Count<DynamicModule>() <= 0)
        return;
      DynamicModulesConfig config = initializer.Context.GetConfig<DynamicModulesConfig>();
      foreach (DynamicModule dynamicModule in (IEnumerable<DynamicModule>) dynamicModules)
      {
        if (dynamicModule.Status != DynamicModuleStatus.NotInstalled)
        {
          Guid moduleId = dynamicModule.Id;
          IQueryable<DynamicModuleType> dynamicModuleTypes = managerInTransaction.GetDynamicModuleTypes();
          Expression<Func<DynamicModuleType, bool>> predicate = (Expression<Func<DynamicModuleType, bool>>) (t => t.ParentModuleId == moduleId);
          foreach (DynamicModuleType dynamicModuleType in (IEnumerable<DynamicModuleType>) dynamicModuleTypes.Where<DynamicModuleType>(predicate))
          {
            DynamicModuleType moduleType = dynamicModuleType;
            managerInTransaction.LoadDynamicModuleGraph(dynamicModule);
            ContentViewControlElement contentViewControl = config.ContentViewControls[ModuleNamesGenerator.GenerateContentViewDefinitionName(moduleType.GetFullTypeName())];
            if (contentViewControl != null)
            {
              if (contentViewControl.ViewsConfig[ModuleNamesGenerator.GenerateListViewName(moduleType.DisplayName)] is MasterGridViewElement masterGridView && masterGridView.ViewModesConfig.Elements.FirstOrDefault<ViewModeElement>((Func<ViewModeElement, bool>) (e => e.Name == "Grid")) is GridViewModeElement gridViewModeElement && gridViewModeElement.Columns.FirstOrDefault<IColumnDefinition>((Func<IColumnDefinition, bool>) (c => c.Name == moduleType.MainShortTextFieldName)) is DataColumnElement dataColumnElement)
              {
                DynamicModuleField moduleField = ((IEnumerable<DynamicModuleField>) moduleType.Fields).FirstOrDefault<DynamicModuleField>((Func<DynamicModuleField, bool>) (f => f.Name == moduleType.MainShortTextFieldName));
                if (moduleField != null)
                {
                  List<DynamicModuleType> list = ((IEnumerable<DynamicModuleType>) dynamicModule.Types).Where<DynamicModuleType>((Func<DynamicModuleType, bool>) (t => t.ParentModuleTypeId == moduleType.Id)).ToList<DynamicModuleType>();
                  if (list != null && list.Count > 0)
                  {
                    if (upgradeTo == SitefinityVersion.Sitefinity7_1)
                    {
                      dataColumnElement.ClientTemplate = MasterListViewGenerator.CreateMainColumnClientTemplateHierarchy((IDynamicModuleField) moduleField, (IEnumerable<IDynamicModuleType>) list);
                      this.UpdateMasterGridViewLinks(list, masterGridView);
                    }
                    else if (upgradeTo == SitefinityVersion.Sitefinity8_1)
                      dataColumnElement.ClientTemplate = MasterListViewGenerator.CreateMainColumnClientTemplateHierarchy((IDynamicModuleField) moduleField, (IEnumerable<IDynamicModuleType>) list);
                  }
                  else if (upgradeTo == SitefinityVersion.Sitefinity8_1)
                    dataColumnElement.ClientTemplate = MasterListViewGenerator.CreateMainColumnClientTemplate((IDynamicModuleField) moduleField);
                }
              }
              if (contentViewControl.ViewsConfig[ModuleNamesGenerator.GenerateBackendEditViewName(moduleType.DisplayName)] is DetailFormViewElement view1 && upgradeTo == SitefinityVersion.Sitefinity8_1)
                DetailsViewGenerator.CreateWarningsSection(view1);
              if (contentViewControl.ViewsConfig[ModuleNamesGenerator.GenerateBackendInsertViewName(moduleType.DisplayName)] is DetailFormViewElement view2 && upgradeTo == SitefinityVersion.Sitefinity8_1)
                DetailsViewGenerator.CreateWarningsSection(view2);
              if (contentViewControl.ViewsConfig[ModuleNamesGenerator.GenerateBackendDuplicateViewName(moduleType.DisplayName)] is DetailFormViewElement view3 && upgradeTo == SitefinityVersion.Sitefinity8_1)
                DetailsViewGenerator.CreateWarningsSection(view3);
            }
          }
        }
      }
    }

    /// <summary>
    /// Upgrade Metadata types with the newly introduced field parent type id
    /// </summary>
    /// <param name="initializer"></param>
    private void UpgradeMetadataTypesWithParentId(SiteInitializer initializer)
    {
      ModuleBuilderManager managerInTransaction = initializer.GetManagerInTransaction<ModuleBuilderManager>();
      MetadataManager metadataManager = initializer.MetadataManager;
      IQueryable<DynamicModuleType> dynamicModuleTypes = managerInTransaction.GetDynamicModuleTypes();
      Expression<Func<DynamicModuleType, bool>> predicate = (Expression<Func<DynamicModuleType, bool>>) (t => t.ParentModuleTypeId != Guid.Empty);
      foreach (DynamicModuleType dynamicModuleType in (IEnumerable<DynamicModuleType>) dynamicModuleTypes.Where<DynamicModuleType>(predicate))
      {
        DynamicModuleType dynamicType = dynamicModuleType;
        MetaType metaType = metadataManager.GetMetaTypes().FirstOrDefault<MetaType>((Expression<Func<MetaType, bool>>) (mt => mt.Id == dynamicType.Id));
        if (metaType != null)
          metaType.ParentTypeId = dynamicType.ParentModuleTypeId;
      }
    }

    private void UpdateMasterGridViewLinks(
      List<DynamicModuleType> childModuleTypes,
      MasterGridViewElement masterGridView)
    {
      foreach (DynamicModuleType childModuleType in childModuleTypes)
      {
        string key = "viewType" + childModuleType.TypeName;
        if (masterGridView.LinksConfig[key] == null)
          masterGridView.LinksConfig.Add(new LinkElement((ConfigElement) masterGridView.LinksConfig)
          {
            Name = key,
            CommandName = childModuleType.GenerateChiltTypeCommandName(),
            NavigateUrl = RouteHelper.CreateNodeReference(childModuleType.PageId) + "{{SystemUrl}}/?provider={{ProviderName}}"
          });
      }
    }

    private void UpgradeContentDefaultUrls(SiteInitializer initializer)
    {
      ProvidersCollection<DynamicModuleDataProvider> providersCollection = ManagerBase<DynamicModuleDataProvider>.StaticProvidersCollection ?? initializer.GetManagerInTransaction<DynamicModuleManager>().StaticProviders;
      foreach (DynamicModuleType dynamicModuleType in initializer.GetManagerInTransaction<ModuleBuilderManager>().GetDynamicModuleTypes().ToList<DynamicModuleType>())
      {
        Type itemType = TypeResolutionService.ResolveType(dynamicModuleType.GetFullTypeName(), false);
        if (itemType != (Type) null)
        {
          foreach (DataProviderBase dataProviderBase in (Collection<DynamicModuleDataProvider>) providersCollection)
          {
            DynamicModuleManager manager = DynamicModuleManager.GetManager(dataProviderBase.Name, "sftran_upgrade_defaulturls");
            using (new ElevatedModeRegion((IManager) manager))
            {
              IQueryable<DynamicContent> items = manager.GetDataItems(itemType).Where<DynamicContent>((Expression<Func<DynamicContent, bool>>) (x => string.IsNullOrEmpty((string) x.ItemDefaultUrl)));
              initializer.UpgradeContentDefaultUrls((IEnumerable<ILocatableExtended>) items, (IManager) manager);
            }
          }
        }
      }
    }

    private void AddDuplicateContentItems(SiteInitializer initializer)
    {
      ModuleBuilderManager managerInTransaction = initializer.GetManagerInTransaction<ModuleBuilderManager>();
      IQueryable<DynamicModule> dynamicModules = managerInTransaction.GetDynamicModules();
      if (dynamicModules.Count<DynamicModule>() <= 0)
        return;
      DynamicModulesConfig config = initializer.Context.GetConfig<DynamicModulesConfig>();
      foreach (DynamicModule dynamicModule in (IEnumerable<DynamicModule>) dynamicModules)
      {
        Guid moduleId = dynamicModule.Id;
        IQueryable<DynamicModuleType> dynamicModuleTypes = managerInTransaction.GetDynamicModuleTypes();
        Expression<Func<DynamicModuleType, bool>> predicate = (Expression<Func<DynamicModuleType, bool>>) (t => t.ParentModuleId == moduleId);
        foreach (DynamicModuleType dynamicModuleType in (IEnumerable<DynamicModuleType>) dynamicModuleTypes.Where<DynamicModuleType>(predicate))
        {
          if (dynamicModule.Status != DynamicModuleStatus.NotInstalled)
          {
            ContentViewControlElement contentViewControl = config.ContentViewControls[ModuleNamesGenerator.GenerateContentViewDefinitionName(dynamicModuleType.GetFullTypeName())];
            if (contentViewControl != null)
            {
              if (contentViewControl.ViewsConfig[ModuleNamesGenerator.GenerateListViewName(dynamicModuleType.DisplayName)] is MasterGridViewElement masterGridViewElement)
              {
                MasterListViewGenerator.AddDuplicateItemDialog((IDynamicModuleType) dynamicModuleType, masterGridViewElement);
                this.AddDuplicateActionMenuCommand(masterGridViewElement);
              }
              this.AddDuplicateDetailsView(dynamicModuleType, managerInTransaction, contentViewControl);
            }
          }
        }
      }
    }

    private void AddDuplicateDetailsView(
      DynamicModuleType dynamicType,
      ModuleBuilderManager manager,
      ContentViewControlElement contentViewControlElement)
    {
      if (dynamicType.Fields == null || dynamicType.Sections == null)
        manager.LoadDynamicModuleTypeGraph(dynamicType, false);
      DetailFormViewElement insertDetailsView = DetailsViewGenerator.GenerateInsertDetailsView((IDynamicModuleType) dynamicType, contentViewControlElement, true);
      if (contentViewControlElement.ViewsConfig.ContainsKey(insertDetailsView.ViewName))
        return;
      contentViewControlElement.ViewsConfig.Add(insertDetailsView.ViewName, (ContentViewDefinitionElement) insertDetailsView);
    }

    private void AddDuplicateActionMenuCommand(MasterGridViewElement masterGridView)
    {
      if (!(masterGridView.ViewModesConfig.Elements.FirstOrDefault<ViewModeElement>((Func<ViewModeElement, bool>) (e => e.Name == "Grid")) is GridViewModeElement gridViewModeElement))
        return;
      foreach (IColumnDefinition column in gridViewModeElement.Columns)
      {
        if (column is ActionMenuColumnElement menuColumnElement && !(menuColumnElement.MenuItems.Elements.FirstOrDefault<WidgetElement>((Func<WidgetElement, bool>) (e => e.Name == "Duplicate")) is CommandWidgetElement))
        {
          ConfigElementList<WidgetElement> menuItems = menuColumnElement.MenuItems;
          CommandWidgetElement element = new CommandWidgetElement((ConfigElement) menuColumnElement.MenuItems);
          element.Name = "Duplicate";
          element.WrapperTagKey = HtmlTextWriterTag.Li;
          element.CommandName = "duplicate";
          element.Text = "DuplicateDetailsViewTitle";
          element.ResourceClassId = typeof (ModuleBuilderResources).Name;
          element.CssClass = "sfDuplicateItm";
          element.WidgetType = typeof (CommandWidget);
          menuItems.Add((WidgetElement) element);
        }
      }
    }

    /// <summary>Adds the recycle bin link to dynamic types side bar.</summary>
    /// <param name="initializer">The initializer.</param>
    private void AddRecycleBinLinkToDynamicTypesSideBar(SiteInitializer initializer)
    {
      foreach (ConfigElement contentViewControl in (ConfigElementCollection) initializer.Context.GetConfig<DynamicModulesConfig>().ContentViewControls)
      {
        if (contentViewControl is ContentViewControlElement viewControlElement)
        {
          string contentTypeName = viewControlElement.ContentTypeName;
          if (!string.IsNullOrWhiteSpace(contentTypeName))
          {
            foreach (ContentViewDefinitionElement definitionElement in (IEnumerable<ContentViewDefinitionElement>) viewControlElement.ViewsConfig.Values)
            {
              if (definitionElement is MasterGridViewElement masterGridViewElement)
              {
                try
                {
                  DefinitionsHelper.CreateRecycleBinLink(masterGridViewElement.SidebarConfig.Sections, "~/Sitefinity/dashboard/RecycleBin/RecycleBin", contentTypeName);
                }
                catch (Exception ex)
                {
                  Log.Write((object) string.Format("FAILED : Failed to upgrade dynamic type {0} recycle bin widget - {1}", (object) contentTypeName, (object) ex.Message), ConfigurationPolicy.UpgradeTrace);
                }
              }
            }
          }
        }
      }
    }

    /// <summary>Adds the sorting to the toobar section.</summary>
    /// <param name="initializer">The initializer.</param>
    /// <exception cref="T:System.NotImplementedException"></exception>
    private void AddSortingToTheToobarSection(SiteInitializer initializer)
    {
      DynamicModulesConfig config = initializer.Context.GetConfig<DynamicModulesConfig>();
      foreach (ConfigElement contentViewControl in (ConfigElementCollection) config.ContentViewControls)
      {
        if (contentViewControl is ContentViewControlElement viewControlElement)
        {
          foreach (ContentViewDefinitionElement definitionElement in (IEnumerable<ContentViewDefinitionElement>) viewControlElement.ViewsConfig.Values)
          {
            if (definitionElement is MasterGridViewElement masterGridViewElement && masterGridViewElement.ViewType.Equals(typeof (DynamicContentMasterGridView)) && masterGridViewElement.ToolbarConfig.Sections.Count > 0)
            {
              WidgetBarSectionElement section = masterGridViewElement.ToolbarConfig.Sections[0];
              Type contentType = viewControlElement.ContentType;
              if (!(contentType == (Type) null))
              {
                string fullName = contentType.FullName;
                try
                {
                  DynamicModuleType dynamicModuleType = ModuleBuilderManager.GetManager().GetDynamicModuleType(fullName);
                  if (!((Func<ConfigElementList<WidgetElement>, string, Guid, bool>) ((widgetElements, name, dynamicTypeId) => widgetElements.Elements.FirstOrDefault<WidgetElement>((Func<WidgetElement, bool>) (item => item is DynamicCommandWidgetElement commandWidgetElement1 && (commandWidgetElement1.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase) || commandWidgetElement1.DynamicModuleTypeId.Equals(dynamicTypeId)))) != null))(section.Items, "EditCustomSorting", dynamicModuleType.Id))
                  {
                    DynamicCommandWidgetElement commandWidgetElement = new DynamicCommandWidgetElement((ConfigElement) section.Items);
                    commandWidgetElement.Name = "EditCustomSorting";
                    commandWidgetElement.Text = "Sort";
                    commandWidgetElement.BindTo = BindCommandListTo.ComboBox;
                    commandWidgetElement.HeaderText = "Sort";
                    commandWidgetElement.PageSize = 10;
                    commandWidgetElement.WrapperTagKey = HtmlTextWriterTag.Li;
                    commandWidgetElement.WidgetType = typeof (SortWidget);
                    commandWidgetElement.ResourceClassId = typeof (ModuleBuilderResources).Name;
                    commandWidgetElement.CssClass = "sfQuickSort sfNoMasterViews";
                    commandWidgetElement.DynamicModuleTypeId = dynamicModuleType.Id;
                    DynamicCommandWidgetElement element = commandWidgetElement;
                    section.Items.Add((WidgetElement) element);
                    List<SortingExpressionElement> expressionForAdynamicType = this.GetSortingExpressionForADynamicType(dynamicModuleType, (ConfigSection) config);
                    foreach (SortingExpressionElement expressionElement in expressionForAdynamicType.Where<SortingExpressionElement>((Func<SortingExpressionElement, bool>) (s => !s.IsCustom)))
                    {
                      string str = expressionElement.SortingExpression;
                      string title = expressionElement.SortingExpressionTitle;
                      if (expressionElement.SortingExpression == "{0} ASC" || expressionElement.SortingExpression == "{0} DESC")
                      {
                        title = string.Format(expressionElement.SortingExpressionTitle, (object) dynamicModuleType.MainShortTextFieldName);
                        str = string.Format(expressionElement.SortingExpression, (object) dynamicModuleType.MainShortTextFieldName);
                      }
                      DynamicItemElement dynamicItemElement = DefinitionsHelper.CreateDynamicItemElement((ConfigElement) element.Items, title, str, (string) null, (NameValueCollection) null);
                      element.Items.Add(dynamicItemElement);
                      element.DesignTimeItems.Add(dynamicItemElement.GetKey());
                    }
                    foreach (SortingExpressionElement expressionElement in expressionForAdynamicType.Where<SortingExpressionElement>((Func<SortingExpressionElement, bool>) (s => s.IsCustom)))
                    {
                      DynamicItemElement dynamicItemElement = DefinitionsHelper.CreateDynamicItemElement((ConfigElement) element.CustomItems, expressionElement.SortingExpressionTitle, expressionElement.SortingExpression, expressionElement.ResourceClassId, (NameValueCollection) null);
                      element.CustomItems.Add(dynamicItemElement);
                    }
                  }
                }
                catch (Exception ex)
                {
                  Log.Write((object) string.Format("FAILED : Failed to upgrade dynamic type {0} sorting - {1}", (object) fullName, (object) ex.Message), ConfigurationPolicy.UpgradeTrace);
                }
              }
            }
          }
        }
      }
    }

    /// <summary>
    /// Adds the type of the sorting expression for a dynamic.
    /// </summary>
    /// <param name="moduleType">Type of the module.</param>
    /// <param name="section">The section.</param>
    /// <returns></returns>
    private List<SortingExpressionElement> GetSortingExpressionForADynamicType(
      DynamicModuleType moduleType,
      ConfigSection section)
    {
      List<SortingExpressionElement> expressionForAdynamicType = new List<SortingExpressionElement>();
      string fullTypeName = moduleType.GetFullTypeName();
      SortingExpressionElement expressionElement1 = new SortingExpressionElement((ConfigElement) section);
      expressionElement1.ContentType = fullTypeName;
      expressionElement1.SortingExpressionTitle = "NewModifiedFirst";
      expressionElement1.ResourceClassId = typeof (ModuleBuilderResources).Name;
      expressionElement1.SortingExpression = "LastModified DESC";
      expressionForAdynamicType.Add(expressionElement1);
      SortingExpressionElement expressionElement2 = new SortingExpressionElement((ConfigElement) section);
      expressionElement2.ContentType = fullTypeName;
      expressionElement2.SortingExpressionTitle = "NewCreatedFirst";
      expressionElement2.ResourceClassId = typeof (ModuleBuilderResources).Name;
      expressionElement2.SortingExpression = "DateCreated DESC";
      expressionForAdynamicType.Add(expressionElement2);
      SortingExpressionElement expressionElement3 = new SortingExpressionElement((ConfigElement) section);
      expressionElement3.ContentType = fullTypeName;
      expressionElement3.SortingExpressionTitle = "By {0} (Ascending)";
      expressionElement3.SortingExpression = "{0} ASC";
      expressionForAdynamicType.Add(expressionElement3);
      SortingExpressionElement expressionElement4 = new SortingExpressionElement((ConfigElement) section);
      expressionElement4.ContentType = fullTypeName;
      expressionElement4.SortingExpressionTitle = "By {0} (Descending)";
      expressionElement4.SortingExpression = "{0} DESC";
      expressionForAdynamicType.Add(expressionElement4);
      SortingExpressionElement expressionElement5 = new SortingExpressionElement((ConfigElement) section);
      expressionElement5.ContentType = fullTypeName;
      expressionElement5.SortingExpressionTitle = "CustomSorting";
      expressionElement5.ResourceClassId = typeof (ModuleBuilderResources).Name;
      expressionElement5.SortingExpression = "Custom";
      expressionElement5.IsCustom = true;
      expressionForAdynamicType.Add(expressionElement5);
      return expressionForAdynamicType;
    }

    /// <summary>
    /// Add status filter section to the dynamic sidebar config.
    /// </summary>
    /// <param name="initializer">The initializer.</param>
    private void AddStatusFilterSidebarSection(SiteInitializer initializer)
    {
      foreach (ConfigElement contentViewControl in (ConfigElementCollection) initializer.Context.GetConfig<DynamicModulesConfig>().ContentViewControls)
      {
        if (contentViewControl is ContentViewControlElement viewControlElement)
        {
          foreach (ContentViewDefinitionElement definitionElement in (IEnumerable<ContentViewDefinitionElement>) viewControlElement.ViewsConfig.Values)
          {
            if (definitionElement is MasterGridViewElement masterGridViewElement)
            {
              bool flag = false;
              try
              {
                foreach (WidgetBarSectionElement section in masterGridViewElement.SidebarConfig.Sections)
                {
                  if (!string.IsNullOrEmpty(section.Name) && section.Name.Equals("Filter", StringComparison.OrdinalIgnoreCase) || !string.IsNullOrEmpty(section.WrapperTagId) && section.WrapperTagId.Equals("filterSection", StringComparison.OrdinalIgnoreCase))
                  {
                    flag = true;
                    this.InsertStatusFilterIntoDynamicModuleSidebar(this.PredictInsertIndexOfStatusFilterSection(section.Items), viewControlElement.ContentTypeName, section.Items);
                    break;
                  }
                }
                if (!flag)
                {
                  WidgetBarSectionElement element = new WidgetBarSectionElement((ConfigElement) masterGridViewElement.SidebarConfig.Sections)
                  {
                    Name = "Filter",
                    Title = string.Format(Res.Get<ModuleBuilderResources>().FilterSectionTitle, (object) viewControlElement.ContentTypeName),
                    CssClass = "sfFirst sfWidgetsList sfSeparator sfModules",
                    WrapperTagId = "filterSection"
                  };
                  this.InsertStatusFilterIntoDynamicModuleSidebar(0, viewControlElement.ContentTypeName, element.Items);
                  WidgetBarSectionElement barSectionElement = masterGridViewElement.SidebarConfig.Sections.FirstOrDefault<WidgetBarSectionElement>();
                  if (barSectionElement != null && barSectionElement.Title.Equals("Languages", StringComparison.OrdinalIgnoreCase))
                    masterGridViewElement.SidebarConfig.Sections.Insert(1, element);
                  else
                    masterGridViewElement.SidebarConfig.Sections.Insert(0, element);
                }
              }
              catch (Exception ex)
              {
                Log.Write((object) string.Format("FAILED : Failed to upgrade dynamic type {0} status filtering - {1}", (object) viewControlElement.ContentTypeName, (object) ex.Message), ConfigurationPolicy.UpgradeTrace);
              }
            }
          }
        }
      }
    }

    /// <summary>
    /// Creates s status filter section to be placed in the dynamic module sidebar section.
    /// </summary>
    /// <param name="startindex">The startindex.</param>
    /// <param name="moduleName">Name of the module.</param>
    /// <param name="widgetElements">The filter section.</param>
    private void InsertStatusFilterIntoDynamicModuleSidebar(
      int startindex,
      string moduleName,
      ConfigElementList<WidgetElement> widgetElements)
    {
      Func<ConfigElementList<WidgetElement>, string, string, bool> func = (Func<ConfigElementList<WidgetElement>, string, string, bool>) ((section, name, commandName) => section.Elements.FirstOrDefault<WidgetElement>((Func<WidgetElement, bool>) (item => item is CommandWidgetElement commandWidgetElement && (commandWidgetElement.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase) || commandWidgetElement.CommandName.Equals(commandName, StringComparison.InvariantCultureIgnoreCase)))) != null);
      if (!func(widgetElements, string.Format("Draft{0}", (object) moduleName), "showMasterItems"))
      {
        ConfigElementList<WidgetElement> configElementList = widgetElements;
        int index = startindex++;
        CommandWidgetElement element = new CommandWidgetElement((ConfigElement) widgetElements);
        element.Name = string.Format("Draft{0}", (object) moduleName);
        element.CommandName = "showMasterItems";
        element.ButtonType = CommandButtonType.SimpleLinkButton;
        element.Text = "Draft";
        element.CssClass = "";
        element.ResourceClassId = typeof (ModuleBuilderResources).Name;
        element.WidgetType = typeof (CommandWidget);
        element.IsSeparator = false;
        configElementList.Insert(index, (WidgetElement) element);
      }
      if (!func(widgetElements, string.Format("Published{0}", (object) moduleName), "showPublishedItems"))
      {
        ConfigElementList<WidgetElement> configElementList = widgetElements;
        int index = startindex++;
        CommandWidgetElement element = new CommandWidgetElement((ConfigElement) widgetElements);
        element.Name = string.Format("Published{0}", (object) moduleName);
        element.CommandName = "showPublishedItems";
        element.ButtonType = CommandButtonType.SimpleLinkButton;
        element.Text = "Published";
        element.ResourceClassId = typeof (ModuleBuilderResources).Name;
        element.CssClass = "";
        element.WidgetType = typeof (CommandWidget);
        element.IsSeparator = false;
        configElementList.Insert(index, (WidgetElement) element);
      }
      if (func(widgetElements, string.Format("Scheduled{0}", (object) moduleName), "showScheduledItems"))
        return;
      ConfigElementList<WidgetElement> configElementList1 = widgetElements;
      int index1 = startindex;
      CommandWidgetElement element1 = new CommandWidgetElement((ConfigElement) widgetElements);
      element1.Name = string.Format("Scheduled{0}", (object) moduleName);
      element1.CommandName = "showScheduledItems";
      element1.ButtonType = CommandButtonType.SimpleLinkButton;
      element1.Text = "Scheduled";
      element1.ResourceClassId = typeof (ModuleBuilderResources).Name;
      element1.CssClass = "";
      element1.WidgetType = typeof (CommandWidget);
      element1.IsSeparator = false;
      configElementList1.Insert(index1, (WidgetElement) element1);
    }

    /// <summary>
    /// Return a place index where to insert the filter status section.
    /// </summary>
    /// <param name="configElementList">The configuration element list.</param>
    /// <returns></returns>
    private int PredictInsertIndexOfStatusFilterSection(
      ConfigElementList<WidgetElement> configElementList)
    {
      int num = 0;
      for (int index = 0; index < configElementList.Count; ++index)
      {
        if (configElementList[index] is CommandWidgetElement configElement)
        {
          if (configElement.CommandName == "showAllItems")
            num = index + 1;
          else if (configElement.CommandName == "showMyItems")
          {
            num = index + 1;
            break;
          }
        }
      }
      return num;
    }

    /// <summary>Upgrades the dynamic module field attributes.</summary>
    /// <param name="initializer">The initializer.</param>
    private void UpgradeDynamicModuleFieldAttributes(SiteInitializer initializer)
    {
      ModuleBuilderManager managerInTransaction = initializer.GetManagerInTransaction<ModuleBuilderManager>();
      MetadataManager metadataManager = initializer.MetadataManager;
      IQueryable<DynamicModule> dynamicModules = managerInTransaction.GetDynamicModules();
      Expression<Func<DynamicModule, bool>> predicate1 = (Expression<Func<DynamicModule, bool>>) (x => (int) x.Status != 0);
      foreach (DynamicModule dynamicModule1 in (IEnumerable<DynamicModule>) dynamicModules.Where<DynamicModule>(predicate1))
      {
        DynamicModule dynamicModule = dynamicModule1;
        IQueryable<DynamicModuleType> dynamicModuleTypes = managerInTransaction.GetDynamicModuleTypes();
        Expression<Func<DynamicModuleType, bool>> predicate2 = (Expression<Func<DynamicModuleType, bool>>) (t => t.ParentModuleId == dynamicModule.Id);
        foreach (DynamicModuleType dynamicModuleType in (IEnumerable<DynamicModuleType>) dynamicModuleTypes.Where<DynamicModuleType>(predicate2))
        {
          DynamicModuleType dynamicType = dynamicModuleType;
          IQueryable<DynamicModuleField> dynamicModuleFields = managerInTransaction.GetDynamicModuleFields();
          Expression<Func<DynamicModuleField, bool>> predicate3 = (Expression<Func<DynamicModuleField, bool>>) (x => x.ParentTypeId == dynamicType.Id && (int) x.FieldType == 9);
          foreach (DynamicModuleField field in (IEnumerable<DynamicModuleField>) dynamicModuleFields.Where<DynamicModuleField>(predicate3))
          {
            MetaField metafield = metadataManager.GetMetafield(field.Id);
            if (metafield.MetaAttributes.FirstOrDefault<MetaFieldAttribute>((Func<MetaFieldAttribute, bool>) (x => x.Name == DynamicAttributeNames.ControlTag)) == null)
            {
              MetaFieldAttribute metaFieldAttribute1 = new MetaFieldAttribute();
              metaFieldAttribute1.Name = DynamicAttributeNames.ControlTag;
              metaFieldAttribute1.Value = WidgetTemplateInstaller.GetMediaFieldTypeTemplate(field);
              MetaFieldAttribute metaFieldAttribute2 = metaFieldAttribute1;
              metafield.MetaAttributes.Add(metaFieldAttribute2);
            }
          }
        }
      }
    }

    /// <summary>
    /// Create a history version configs for the dynamic modules.
    /// </summary>
    /// <param name="initializer">The initializer.</param>
    private void CreateHistoryVersionViewConfigs(SiteInitializer initializer)
    {
      DynamicModulesConfig config = initializer.Context.GetConfig<DynamicModulesConfig>();
      ModuleBuilderManager manager = ModuleBuilderManager.GetManager();
      foreach (ConfigElement contentViewControl in (ConfigElementCollection) config.ContentViewControls)
      {
        if (contentViewControl is ContentViewControlElement backendContentView)
        {
          try
          {
            Type contentType = backendContentView.ContentType;
            if (contentType == (Type) null)
            {
              Log.Write((object) string.Format("FAILED : Failed to get the dynamic content type {0}", (object) backendContentView.ContentTypeName), ConfigurationPolicy.UpgradeTrace);
            }
            else
            {
              string fullName = contentType.FullName;
              DynamicModuleType dynamicModuleType = manager.GetDynamicModuleType(fullName);
              if (dynamicModuleType.Fields == null || dynamicModuleType.Sections == null)
                manager.LoadDynamicModuleTypeGraph(dynamicModuleType, false);
              bool flag1 = false;
              bool flag2 = false;
              MasterGridViewElement masterGridViewElement = (MasterGridViewElement) null;
              foreach (ContentViewDefinitionElement definitionElement in (IEnumerable<ContentViewDefinitionElement>) backendContentView.ViewsConfig.Values)
              {
                if (definitionElement is MasterGridViewElement)
                  masterGridViewElement = definitionElement as MasterGridViewElement;
                if (definitionElement.ViewName.Equals("ContentBackendVersionPreview", StringComparison.OrdinalIgnoreCase))
                  flag1 = true;
                else if (definitionElement.ViewName.Equals("ContentBackendVersionComparisonView", StringComparison.OrdinalIgnoreCase))
                  flag2 = true;
                if (flag1 & flag2)
                  break;
              }
              if (!(flag1 & flag2))
              {
                if (masterGridViewElement == null)
                {
                  Log.Write((object) string.Format("FAILED : Failed to get the MasterGridViewElement for a specific content type {0}", (object) backendContentView.ContentTypeName), ConfigurationPolicy.UpgradeTrace);
                }
                else
                {
                  DetailFormViewElement versionDetailsView = DetailsViewGenerator.GenerateHistoryVersionDetailsView((IDynamicModuleType) dynamicModuleType, backendContentView);
                  backendContentView.ViewsConfig.Add((ContentViewDefinitionElement) versionDetailsView);
                  ComparisonViewElement element = DetailsViewGenerator.DefineBackendVersioningComparisonView((IDynamicModuleType) dynamicModuleType, backendContentView);
                  backendContentView.ViewsConfig.Add((ContentViewDefinitionElement) element);
                  MasterListViewGenerator.AddHistoryVersionComparerDialog((IDynamicModuleType) dynamicModuleType, masterGridViewElement.DialogsConfig);
                  MasterListViewGenerator.AddHistoryVersionPreviewDialog((IDynamicModuleType) dynamicModuleType, masterGridViewElement.DialogsConfig);
                  if (!(masterGridViewElement.ViewModesConfig.FirstOrDefault<ViewModeElement>((Func<ViewModeElement, bool>) (e => e is GridViewModeElement)) is GridViewModeElement gridViewModeElement))
                  {
                    Log.Write((object) string.Format("FAILED : Failed to get the GridViewModeElement for a specific module {0}", (object) masterGridViewElement.ModuleName), ConfigurationPolicy.UpgradeTrace);
                  }
                  else
                  {
                    foreach (ConfigElement configElement in (ConfigElementCollection) gridViewModeElement.ColumnsConfig)
                    {
                      if (configElement is ActionMenuColumnElement menuColumnElement)
                      {
                        CommandWidgetElement actionMenuCommand = DefinitionsHelper.CreateActionMenuCommand((ConfigElement) menuColumnElement.MenuItems, "History", HtmlTextWriterTag.Li, "historygrid", "HistoryMenuItemTitle", typeof (VersionResources).Name);
                        menuColumnElement.MenuItems.Add((WidgetElement) actionMenuCommand);
                      }
                    }
                  }
                }
              }
            }
          }
          catch (Exception ex)
          {
            Log.Write((object) string.Format("FAILED : Failed to upgrade dynamic type {0} sorting - {1}", (object) backendContentView.ContentTypeName, (object) ex.Message), ConfigurationPolicy.UpgradeTrace);
          }
        }
      }
    }

    /// <summary>
    /// Upgrade definitions for Dynamic Modules. Change field type to be ContentUrlField.
    /// </summary>
    /// <param name="initializer"></param>
    private void UpgradeDynamicModuleMirrorFieldInDefinitions(SiteInitializer initializer)
    {
      foreach (ConfigElement contentViewControl in (ConfigElementCollection) initializer.Context.GetConfig<DynamicModulesConfig>().ContentViewControls)
      {
        if (contentViewControl is ContentViewControlElement viewControlElement)
        {
          foreach (ContentViewDefinitionElement definitionElement in (IEnumerable<ContentViewDefinitionElement>) viewControlElement.ViewsConfig.Values)
          {
            if (definitionElement is DetailFormViewElement detailFormViewElement)
            {
              foreach (ContentViewSectionElement viewSectionElement in (IEnumerable<ContentViewSectionElement>) detailFormViewElement.Sections.Values)
              {
                foreach (ConfigElement field in (ConfigElementCollection) viewSectionElement.Fields)
                {
                  if (field is MirrorTextFieldElement textFieldElement && textFieldElement.FieldType == typeof (MirrorTextField) && textFieldElement.DataFieldName == "UrlName.PersistedValue")
                    textFieldElement.FieldType = typeof (ContentUrlField);
                }
              }
            }
          }
        }
      }
    }

    /// <summary>
    /// Upgrade definitions for Dynamic Modules. Add new property "Message"
    /// </summary>
    /// <param name="initializer"></param>
    private void UpgradeDynamicModuleDefinitions(SiteInitializer initializer)
    {
      ModuleBuilderManager managerInTransaction = initializer.GetManagerInTransaction<ModuleBuilderManager>();
      IQueryable<DynamicModule> dynamicModules = managerInTransaction.GetDynamicModules();
      if (dynamicModules.Count<DynamicModule>() <= 0)
        return;
      DynamicModulesConfig config = initializer.Context.GetConfig<DynamicModulesConfig>();
      foreach (DynamicModule dynamicModule in (IEnumerable<DynamicModule>) dynamicModules)
      {
        Guid moduleId = dynamicModule.Id;
        IQueryable<DynamicModuleType> dynamicModuleTypes = managerInTransaction.GetDynamicModuleTypes();
        Expression<Func<DynamicModuleType, bool>> predicate = (Expression<Func<DynamicModuleType, bool>>) (t => t.ParentModuleId == moduleId);
        foreach (DynamicModuleType dynamicModuleType in (IEnumerable<DynamicModuleType>) dynamicModuleTypes.Where<DynamicModuleType>(predicate))
        {
          DynamicModuleType moduleType = dynamicModuleType;
          if (dynamicModule.Status != DynamicModuleStatus.NotInstalled)
          {
            ContentViewControlElement contentViewControl = config.ContentViewControls[ModuleNamesGenerator.GenerateContentViewDefinitionName(moduleType.GetFullTypeName())];
            if (contentViewControl != null && contentViewControl.ViewsConfig[ModuleNamesGenerator.GenerateListViewName(moduleType.DisplayName)] is MasterGridViewElement masterGridViewElement && masterGridViewElement.ViewModesConfig.Elements.FirstOrDefault<ViewModeElement>((Func<ViewModeElement, bool>) (e => e.Name == "Grid")) is GridViewModeElement gridViewModeElement && gridViewModeElement.Columns.FirstOrDefault<IColumnDefinition>((Func<IColumnDefinition, bool>) (c => c.Name == moduleType.MainShortTextFieldName)) is DataColumnElement dataColumnElement)
            {
              managerInTransaction.LoadDynamicModuleGraph(dynamicModule);
              DynamicModuleField moduleField = ((IEnumerable<DynamicModuleField>) moduleType.Fields).FirstOrDefault<DynamicModuleField>((Func<DynamicModuleField, bool>) (f => f.Name == moduleType.MainShortTextFieldName));
              if (moduleField != null)
              {
                List<DynamicModuleType> list = ((IEnumerable<DynamicModuleType>) dynamicModule.Types).Where<DynamicModuleType>((Func<DynamicModuleType, bool>) (t => t.ParentModuleTypeId == moduleType.Id)).ToList<DynamicModuleType>();
                string str = list == null || list.Count <= 0 ? MasterListViewGenerator.CreateMainColumnClientTemplate((IDynamicModuleField) moduleField) : MasterListViewGenerator.CreateMainColumnClientTemplateHierarchy((IDynamicModuleField) moduleField, (IEnumerable<IDynamicModuleType>) list);
                dataColumnElement.ClientTemplate = str;
              }
            }
          }
        }
      }
    }

    /// <summary>
    /// Upgrade configurations for manageContentLocations link and ContentLocationInfoField link
    /// </summary>
    /// <param name="initializer"></param>
    private void AddContentLocatinonsLinks(SiteInitializer initializer)
    {
      ModuleBuilderManager managerInTransaction = initializer.GetManagerInTransaction<ModuleBuilderManager>();
      IQueryable<DynamicModule> dynamicModules = managerInTransaction.GetDynamicModules();
      if (dynamicModules.Count<DynamicModule>() <= 0)
        return;
      DynamicModulesConfig config = initializer.Context.GetConfig<DynamicModulesConfig>();
      foreach (DynamicModule dynamicModule in (IEnumerable<DynamicModule>) dynamicModules)
      {
        Guid moduleId = dynamicModule.Id;
        IQueryable<DynamicModuleType> dynamicModuleTypes = managerInTransaction.GetDynamicModuleTypes();
        Expression<Func<DynamicModuleType, bool>> predicate = (Expression<Func<DynamicModuleType, bool>>) (t => t.ParentModuleId == moduleId);
        foreach (DynamicModuleType moduleType in (IEnumerable<DynamicModuleType>) dynamicModuleTypes.Where<DynamicModuleType>(predicate))
        {
          if (dynamicModule.Status != DynamicModuleStatus.NotInstalled)
            this.UpgradeContentLocaitonLinks(moduleType, config);
        }
      }
    }

    private void UpgradeContentLocaitonLinks(
      DynamicModuleType moduleType,
      DynamicModulesConfig dynamicModulesConfig)
    {
      ContentViewControlElement contentViewControl = dynamicModulesConfig.ContentViewControls[ModuleNamesGenerator.GenerateContentViewDefinitionName(moduleType.GetFullTypeName())];
      if (contentViewControl == null)
        return;
      ModuleBuilderModule.AddLocationInfoField(contentViewControl.ViewsConfig[ModuleNamesGenerator.GenerateBackendEditViewName(moduleType.DisplayName)] as DetailFormViewElement);
      ModuleBuilderModule.AddLocationInfoField(contentViewControl.ViewsConfig[ModuleNamesGenerator.GenerateBackendInsertViewName(moduleType.DisplayName)] as DetailFormViewElement);
      if (!(contentViewControl.ViewsConfig[ModuleNamesGenerator.GenerateListViewName(moduleType.DisplayName)] is MasterGridViewElement masterGridViewElement))
        return;
      if (masterGridViewElement.ViewModesConfig.Elements.FirstOrDefault<ViewModeElement>((Func<ViewModeElement, bool>) (e => e.Name == "Grid")) is GridViewModeElement gridViewModeElement && gridViewModeElement.ColumnsConfig["Actions"] is ActionMenuColumnElement menuColumnElement && menuColumnElement.MenuItems.Elements.FirstOrDefault<WidgetElement>((Func<WidgetElement, bool>) (e => e.Name == "Properties")) is CommandWidgetElement commandWidgetElement1 && commandWidgetElement1.Text != "Edit")
        commandWidgetElement1.Text = "Edit";
      if (masterGridViewElement.LinksConfig != null && !masterGridViewElement.LinksConfig.Contains("manageContentLocations"))
      {
        string fullTypeName = moduleType.GetFullTypeName();
        masterGridViewElement.LinksConfig.Add(new LinkElement((ConfigElement) masterGridViewElement.LinksConfig)
        {
          Name = "manageContentLocations",
          CommandName = "manageContentLocations",
          NavigateUrl = RouteHelper.CreateNodeReference(SiteInitializer.ContentLocationsPageId) + "?item_type=" + fullTypeName
        });
      }
      if (masterGridViewElement.SidebarConfig == null || masterGridViewElement.SidebarConfig.Sections == null || !masterGridViewElement.SidebarConfig.Sections.Contains("Settings"))
        return;
      IConfigElementItem itemByKey = masterGridViewElement.SidebarConfig.Sections.GetItemByKey("Settings");
      if (itemByKey == null || !(itemByKey.Element is WidgetBarSectionElement element1) || element1.Items == null || element1.Items.Contains("ManageContentLocations"))
        return;
      CommandWidgetElement commandWidgetElement2 = new CommandWidgetElement((ConfigElement) element1.Items);
      commandWidgetElement2.Name = "ManageContentLocations";
      commandWidgetElement2.CommandName = "manageContentLocations";
      commandWidgetElement2.ButtonType = CommandButtonType.SimpleLinkButton;
      commandWidgetElement2.Text = "PagesWhereTheseItemsArePublished";
      commandWidgetElement2.ResourceClassId = typeof (ModuleBuilderResources).Name;
      commandWidgetElement2.WidgetType = typeof (CommandWidget);
      CommandWidgetElement element2 = commandWidgetElement2;
      if (element1.Items.GetElementByKey("Permissions") is WidgetElement elementByKey)
        element1.Items.Insert(element1.Items.IndexOf(elementByKey), (WidgetElement) element2);
      else
        element1.Items.Add((WidgetElement) element2);
    }

    private static void AddLocationInfoField(DetailFormViewElement view)
    {
      if (view == null)
        return;
      ConfigElementDictionary<string, ContentViewSectionElement> sections = view.Sections;
      if (sections == null || sections.Keys == null || !sections.Keys.Contains("SidebarSection"))
        return;
      ContentViewSectionElement viewSectionElement = sections["SidebarSection"];
      if (viewSectionElement == null || viewSectionElement.Fields == null || viewSectionElement.Fields.Contains("ContentLocationInfoField"))
        return;
      ConfigElementDictionary<string, FieldDefinitionElement> fields = viewSectionElement.Fields;
      ContentLocationInfoFieldElement element = new ContentLocationInfoFieldElement((ConfigElement) viewSectionElement.Fields);
      element.FieldName = "ContentLocationInfoField";
      element.WrapperTag = HtmlTextWriterTag.Li;
      element.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Write);
      fields.Add((FieldDefinitionElement) element);
    }

    private void UpgradeTo53SP1ModuleName(SiteInitializer initializer)
    {
      MetadataManager manager = MetadataManager.GetManager();
      ModuleBuilderManager managerInTransaction = initializer.GetManagerInTransaction<ModuleBuilderManager>();
      using (new ElevatedModeRegion((IManager) manager))
      {
        foreach (DynamicModuleType dynamicModuleType in (IEnumerable<DynamicModuleType>) managerInTransaction.GetDynamicModuleTypes())
        {
          DynamicModuleType type = dynamicModuleType;
          MetaType metaType = manager.GetMetaTypes().FirstOrDefault<MetaType>((Expression<Func<MetaType, bool>>) (mt => mt.Namespace == type.TypeNamespace && mt.ClassName == type.TypeName));
          if (metaType != null)
          {
            if (type.ModuleName.IsNullOrEmpty())
            {
              DynamicModule dynamicModule = managerInTransaction.GetDynamicModule(type.ParentModuleId);
              type.ModuleName = dynamicModule.Name;
            }
            if (!metaType.MetaAttributes.Any<MetaTypeAttribute>((Func<MetaTypeAttribute, bool>) (a => a.Name == "moduleName")))
            {
              IList<MetaTypeAttribute> metaAttributes = metaType.MetaAttributes;
              MetaTypeAttribute metaTypeAttribute = new MetaTypeAttribute();
              metaTypeAttribute.Name = "moduleName";
              metaTypeAttribute.Value = ModuleBuilderManager.ModuleNameValidationRegex.Replace(type.ModuleName, "");
              metaAttributes.Add(metaTypeAttribute);
            }
          }
        }
        manager.SaveChanges();
      }
    }

    private void UpgradeTo53(SiteInitializer initializer)
    {
      ModuleBuilderManager managerInTransaction = initializer.GetManagerInTransaction<ModuleBuilderManager>();
      IQueryable<DynamicModule> dynamicModules = managerInTransaction.GetDynamicModules();
      if (dynamicModules.Count<DynamicModule>() <= 0)
        return;
      foreach (DynamicModule module in (IEnumerable<DynamicModule>) dynamicModules)
      {
        Guid moduleId = module.Id;
        this.UpgradeModulePageIds(initializer, module);
        IQueryable<DynamicModuleType> dynamicModuleTypes = managerInTransaction.GetDynamicModuleTypes();
        Expression<Func<DynamicModuleType, bool>> predicate = (Expression<Func<DynamicModuleType, bool>>) (t => t.ParentModuleId == moduleId);
        foreach (DynamicModuleType moduleType in (IEnumerable<DynamicModuleType>) dynamicModuleTypes.Where<DynamicModuleType>(predicate))
          this.UpgradePreviewParameters(initializer, moduleType);
      }
    }

    private void UpgradeModulePageIds(SiteInitializer initializer, DynamicModule module)
    {
      Guid moduleId = module.Id;
      PageNode source = initializer.PageManager.GetPageNodes().FirstOrDefault<PageNode>((Expression<Func<PageNode, bool>>) (p => p.Id == moduleId));
      if (source == null || source.NodeType != NodeType.Group)
        return;
      PageNode pageNode1 = initializer.PageManager.CreatePageNode();
      List<PageNode> list = initializer.PageManager.GetPageNodes().Where<PageNode>((Expression<Func<PageNode, bool>>) (p => p.ParentId == moduleId)).OrderBy<PageNode, float>((Expression<Func<PageNode, float>>) (c => c.Ordinal)).ToList<PageNode>();
      this.CopyPageNode(source, pageNode1, (CultureInfo) null, (CultureInfo) null, true, initializer.PageManager);
      foreach (PageNode childNode in list)
        initializer.PageManager.ChangeParent(childNode, pageNode1, false);
      module.PageId = pageNode1.Id;
      PageNode pageNode2 = initializer.PageManager.GetPageNode(moduleId);
      pageNode2.Permissions.Clear();
      IDataProviderBase provider = ((IDataItem) pageNode2).Provider as IDataProviderBase;
      foreach (PermissionsInheritanceMap permissionChild in (IEnumerable<PermissionsInheritanceMap>) provider.GetPermissionChildren(pageNode2.Id))
      {
        if (permissionChild.ChildObjectTypeName == typeof (PageNode).Name)
          provider.DeletePermissionsInheritanceMap(permissionChild);
      }
      initializer.PageManager.Delete(pageNode2);
    }

    private void CopyPageNode(
      PageNode source,
      PageNode target,
      CultureInfo sourceLanguage,
      CultureInfo targetLanguage,
      bool changeParent,
      PageManager manager)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      if (target == null)
        throw new ArgumentNullException(nameof (target));
      foreach (KeyValuePair<string, string> attribute in (IEnumerable<KeyValuePair<string, string>>) source.Attributes)
        target.Attributes.Add(attribute);
      target.Description.CopyFrom(source.Description);
      target.Name = source.Name;
      if (changeParent)
        manager.ChangeParent(target, source.Parent);
      target.RenderAsLink = source.RenderAsLink;
      target.ShowInNavigation = source.ShowInNavigation;
      PageData pageData = source.GetPageData();
      if (pageData != null)
        pageData.NavigationNode = target;
      target.PageDataList.Clear();
      target.Title.CopyFrom(source.Title);
      target.UrlName.CopyFrom(source.UrlName);
      target.NodeType = source.NodeType;
      target.LinkedNodeId = source.LinkedNodeId;
      target.LinkedNodeProvider = source.LinkedNodeProvider;
      target.RedirectUrl.CopyFrom(source.RedirectUrl);
      target.OpenNewWindow = source.OpenNewWindow;
      target.Ordinal = source.Ordinal;
      target.EnableDefaultCanonicalUrl = source.EnableDefaultCanonicalUrl;
      IDataProviderBase provider1 = ((IDataItem) target).Provider as IDataProviderBase;
      IDataProviderBase provider2 = ((IDataItem) source).Provider as IDataProviderBase;
      if (provider1 != null && provider2 != null)
        this.CopySecurityFrom((ISecuredObject) target, (ISecuredObject) source, provider1, provider2);
      target.Extension.CopyFrom(source.Extension);
      target.Urls.ClearDestinationUrls<PageUrlData>(source.Urls, new Action<PageUrlData>(((ContentManagerBase<PageDataProvider>) manager).Delete));
      source.Urls.CopyTo<PageUrlData>(target.Urls, (IDataItem) target);
      if ((sourceLanguage != null ? 1 : (targetLanguage != null ? 1 : 0)) == 0)
        return;
      LocalizationHelper.CopyLstringProperties((IDynamicFieldsContainer) source, (IDynamicFieldsContainer) target, sourceLanguage, targetLanguage, false, true);
    }

    private void CopySecurityFrom(
      ISecuredObject destination,
      ISecuredObject source,
      IDataProviderBase destinationProvider,
      IDataProviderBase sourceProvider)
    {
      if (destination == null)
        throw new ArgumentNullException(nameof (destination));
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      if (destinationProvider == null)
      {
        if (!(destination is IDataItem dataItem) || !(dataItem.Provider is DataProviderBase))
          throw new ArgumentException("Cannot infer destinationProvider.");
        destinationProvider = (IDataProviderBase) dataItem.Provider;
      }
      if (sourceProvider == null)
      {
        if (!(source is IDataItem dataItem) || !(dataItem.Provider is DataProviderBase))
          throw new ArgumentException("Cannot infer sourceProvider.");
        sourceProvider = (IDataProviderBase) dataItem.Provider;
      }
      destination.CanInheritPermissions = source.CanInheritPermissions;
      destination.InheritsPermissions = source.InheritsPermissions;
      destination.SupportedPermissionSets = new string[source.SupportedPermissionSets.Length];
      Array.Copy((Array) source.SupportedPermissionSets, (Array) destination.SupportedPermissionSets, source.SupportedPermissionSets.Length);
      foreach (PermissionsInheritanceMap permissionChild in (IEnumerable<PermissionsInheritanceMap>) sourceProvider.GetPermissionChildren(source.Id))
      {
        if (permissionChild.ChildObjectTypeName == typeof (PageNode).Name)
          destinationProvider.CreatePermissionsInheritanceMap(ModuleBuilderModule.GetIdExcept(permissionChild.ObjectId, source.Id, destination.Id), ModuleBuilderModule.GetIdExcept(permissionChild.ChildObjectId, source.Id, destination.Id), permissionChild.ChildObjectTypeName);
      }
      DataProviderBase dataProviderBase = destinationProvider as DataProviderBase;
      foreach (Telerik.Sitefinity.Security.Model.Permission permission1 in (IEnumerable<Telerik.Sitefinity.Security.Model.Permission>) source.Permissions)
      {
        Telerik.Sitefinity.Security.Model.Permission srcPermissions = permission1;
        if (srcPermissions.ObjectId == source.Id)
        {
          Telerik.Sitefinity.Security.Model.Permission permission2 = destination.Permissions.Where<Telerik.Sitefinity.Security.Model.Permission>((Func<Telerik.Sitefinity.Security.Model.Permission, bool>) (destPerm => destPerm.ObjectId == destination.Id && destPerm.SetName == srcPermissions.SetName && destPerm.PrincipalId == srcPermissions.PrincipalId)).FirstOrDefault<Telerik.Sitefinity.Security.Model.Permission>();
          if (permission2 == null && dataProviderBase != null)
          {
            Telerik.Sitefinity.Security.Model.Permission permission3 = dataProviderBase.CreatePermission(srcPermissions.SetName, destination.Id, srcPermissions.PrincipalId);
            permission3.Deny = srcPermissions.Deny;
            permission3.Grant = srcPermissions.Grant;
            destination.Permissions.Add(permission3);
          }
          else
          {
            permission2.Grant = srcPermissions.Grant;
            permission2.Deny = srcPermissions.Deny;
          }
        }
        else if (destination.Permissions.Where<Telerik.Sitefinity.Security.Model.Permission>((Func<Telerik.Sitefinity.Security.Model.Permission, bool>) (destPerm => destPerm.ObjectId == srcPermissions.ObjectId && destPerm.SetName == srcPermissions.SetName && destPerm.PrincipalId == srcPermissions.PrincipalId)).FirstOrDefault<Telerik.Sitefinity.Security.Model.Permission>() == null)
          destination.Permissions.Add(srcPermissions);
      }
    }

    private void UpgradePreviewParameters(SiteInitializer initializer, DynamicModuleType moduleType)
    {
      DynamicModulesConfig config = initializer.Context.GetConfig<DynamicModulesConfig>();
      if (config.ContentViewControls == null)
        return;
      ContentViewControlElement contentViewControl = config.ContentViewControls[ModuleNamesGenerator.GenerateContentViewDefinitionName(moduleType.GetFullTypeName())];
      if (contentViewControl == null)
        return;
      DetailFormViewElement detailFormViewElement = (DetailFormViewElement) contentViewControl.ViewsConfig[ModuleNamesGenerator.GenerateBackendPreviewViewName(moduleType.DisplayName)];
      if (detailFormViewElement == null)
        return;
      detailFormViewElement.ShowNavigation = new bool?(false);
    }

    private static Guid GetIdExcept(Guid val, Guid exception, Guid replacement)
    {
      val = val != exception ? val : replacement;
      return val;
    }

    private void Upgrade_To_5_2(SiteInitializer initializer)
    {
      this.InstallFieldPermissionsConfiguration(initializer);
      ModuleBuilderManager managerInTransaction = initializer.GetManagerInTransaction<ModuleBuilderManager>();
      IQueryable<DynamicModule> dynamicModules = managerInTransaction.GetDynamicModules();
      if (dynamicModules.Count<DynamicModule>() <= 0)
        return;
      foreach (DynamicModule dynamicModule in (IEnumerable<DynamicModule>) dynamicModules)
      {
        Guid moduleId = dynamicModule.Id;
        IQueryable<DynamicModuleType> queryable = managerInTransaction.GetDynamicModuleTypes().Where<DynamicModuleType>((Expression<Func<DynamicModuleType, bool>>) (t => t.ParentModuleId == moduleId));
        string name = dynamicModule.Name;
        foreach (DynamicModuleType dynamicModuleType in (IEnumerable<DynamicModuleType>) queryable)
        {
          if (dynamicModule.Status != DynamicModuleStatus.NotInstalled)
          {
            managerInTransaction.LoadDynamicModuleTypeGraph(dynamicModuleType, true);
            this.UpgradeBackendDefinitions(initializer, dynamicModuleType, name);
            this.UpgradeDynamicTypePermissions(initializer, dynamicModuleType, dynamicModule, managerInTransaction);
            this.UpgradeFieldPermissions(initializer, dynamicModuleType);
            this.UpgradeDynamicContentDateCreated(initializer, dynamicModuleType);
            this.UpgradeModuleName(dynamicModuleType, name);
          }
        }
      }
    }

    private void UpgradeDynamicContentDateCreated(
      SiteInitializer initializer,
      DynamicModuleType dynamicModuleType)
    {
      string fullTypeName = dynamicModuleType.GetFullTypeName();
      DynamicModuleManager managerInTransaction = initializer.GetManagerInTransaction<DynamicModuleManager>();
      try
      {
        using (new ElevatedModeRegion((IManager) managerInTransaction))
        {
          foreach (DynamicContent dataItem in (IEnumerable<DynamicContent>) managerInTransaction.GetDataItems(TypeResolutionService.ResolveType(fullTypeName)))
            dataItem.DateCreated = new DateTime(2011, 12, 20);
        }
      }
      catch (Exception ex)
      {
        Log.Write((object) string.Format("FAILED : {0} - {1}", (object) ex.Message, (object) ex.StackTrace), ConfigurationPolicy.UpgradeTrace);
      }
    }

    private void UpgradeBackendDefinitions(
      SiteInitializer initializer,
      DynamicModuleType moduleType,
      string moduleName)
    {
      DynamicModulesConfig config = initializer.Context.GetConfig<DynamicModulesConfig>();
      if (config.ContentViewControls == null)
        return;
      ContentViewControlElement contentViewControl = config.ContentViewControls[ModuleNamesGenerator.GenerateContentViewDefinitionName(moduleType.GetFullTypeName())];
      if (contentViewControl == null)
        return;
      if (contentViewControl.ManagerType == (Type) null)
        contentViewControl.ManagerType = typeof (DynamicModuleManager);
      MasterGridViewElement masterGridViewElement = (MasterGridViewElement) contentViewControl.ViewsConfig[ModuleNamesGenerator.GenerateListViewName(moduleType.DisplayName)];
      if (masterGridViewElement != null)
      {
        if (masterGridViewElement.ViewType == typeof (MasterGridView))
          masterGridViewElement.ViewType = typeof (DynamicContentMasterGridView);
        IDecisionScreenDefinition screenDefinition = masterGridViewElement.DecisionScreens.FirstOrDefault<IDecisionScreenDefinition>((Func<IDecisionScreenDefinition, bool>) (ds => ds.Name == "NoItemsExistScreen"));
        if (screenDefinition != null && screenDefinition.Actions.FirstOrDefault<ICommandWidgetDefinition>((Func<ICommandWidgetDefinition, bool>) (a => a.Name == "Create")) is CommandWidgetElement commandWidgetElement1)
        {
          commandWidgetElement1.RelatedSecuredObjectId = moduleType.Id.ToString();
          commandWidgetElement1.ActionName = "Create";
          commandWidgetElement1.PermissionSet = "General";
          commandWidgetElement1.RelatedSecuredObjectTypeName = typeof (DynamicModuleType).FullName;
        }
        WidgetBarSectionElement barSectionElement = masterGridViewElement.ToolbarConfig.Sections.FirstOrDefault<WidgetBarSectionElement>((Func<WidgetBarSectionElement, bool>) (s => s.Name == "toolbar"));
        if (barSectionElement != null)
        {
          if (barSectionElement.Items.FirstOrDefault<WidgetElement>((Func<WidgetElement, bool>) (i => i.Name == ModuleNamesGenerator.GenerateWidgetElementName("Create"))) is CommandWidgetElement commandWidgetElement2)
          {
            commandWidgetElement2.RelatedSecuredObjectId = moduleType.Id.ToString();
            commandWidgetElement2.ActionName = "Create";
            commandWidgetElement2.PermissionSet = "General";
            commandWidgetElement2.RelatedSecuredObjectTypeName = typeof (DynamicModuleType).FullName;
          }
          if (barSectionElement.Items.FirstOrDefault<WidgetElement>((Func<WidgetElement, bool>) (i => i.Name == ModuleNamesGenerator.GenerateWidgetElementName("Delete"))) is CommandWidgetElement commandWidgetElement3)
          {
            commandWidgetElement3.RelatedSecuredObjectId = moduleType.Id.ToString();
            commandWidgetElement3.ActionName = "Delete";
            commandWidgetElement3.PermissionSet = "General";
            commandWidgetElement3.RelatedSecuredObjectTypeName = typeof (DynamicModuleType).FullName;
          }
        }
        DialogElement dialogElement = masterGridViewElement.DialogsConfig.FirstOrDefault<DialogElement>((Func<DialogElement, bool>) (d => d.Name == typeof (ModulePermissionsDialog).Name));
        if (dialogElement != null)
        {
          string str = "?moduleName=" + moduleName + "&typeName=" + typeof (DynamicModuleType).FullName + "&securedObjectId=" + moduleType.Id.ToString() + "&backLabelText=" + "Back to items" + "&title=" + "Permissions" + "&permissionSetName=" + "General";
          if (moduleType.CheckFieldPermissions)
            str = str + "&relatedSecuredObjectTypeName=" + typeof (DynamicModuleField).FullName + "&relatedSecuredObjects=" + MasterListViewGenerator.GetFieldPermissionSets((IDynamicModuleType) moduleType);
          dialogElement.Parameters = str;
        }
      }
      string[] strArray = new string[3]
      {
        ModuleNamesGenerator.GenerateBackendInsertViewName(moduleType.DisplayName),
        ModuleNamesGenerator.GenerateBackendPreviewViewName(moduleType.DisplayName),
        ModuleNamesGenerator.GenerateBackendEditViewName(moduleType.DisplayName)
      };
      foreach (string key in strArray)
      {
        DetailFormViewElement detailFormViewElement = (DetailFormViewElement) contentViewControl.ViewsConfig[key];
        if (detailFormViewElement != null && detailFormViewElement.ViewType == typeof (DetailFormView))
          detailFormViewElement.ViewType = typeof (DynamicContentDetailFormView);
      }
    }

    private void UpgradeDynamicTypePermissions(
      SiteInitializer initializer,
      DynamicModuleType moduleType,
      DynamicModule dynamicModule,
      ModuleBuilderManager moduleBuilderManager)
    {
      moduleType.CanInheritPermissions = true;
      moduleType.InheritsPermissions = false;
      moduleBuilderManager.CreatePermissionInheritanceAssociation((ISecuredObject) dynamicModule, (ISecuredObject) moduleType);
      foreach (Telerik.Sitefinity.Security.Model.Permission permission in (IEnumerable<Telerik.Sitefinity.Security.Model.Permission>) moduleType.Permissions)
        permission.SetName = "General";
      string permissionSetName = moduleType.TypeNamespace.Replace("Telerik.Sitefinity.DynamicTypes.Model.", string.Empty) + "." + moduleType.TypeName;
      this.DeletePermissionsConfiguration(initializer, permissionSetName);
    }

    private void UpgradeFieldPermissions(SiteInitializer initializer, DynamicModuleType moduleType)
    {
      foreach (DynamicModuleField field in moduleType.Fields)
      {
        if (!field.IsTransient && field.Permissions != null && field.Permissions.Count > 0)
        {
          foreach (Telerik.Sitefinity.Security.Model.Permission permission in (IEnumerable<Telerik.Sitefinity.Security.Model.Permission>) field.Permissions)
            permission.SetName = "DynamicFields";
          if (field.FieldNamespace != null)
          {
            string permissionSetName = field.FieldNamespace.Replace("Telerik.Sitefinity.DynamicTypes.Model.", string.Empty) + "." + field.Name;
            this.DeletePermissionsConfiguration(initializer, permissionSetName);
          }
        }
      }
    }

    private void DeletePermissionsConfiguration(
      SiteInitializer initializer,
      string permissionSetName)
    {
      ConfigElementDictionary<string, Telerik.Sitefinity.Security.Configuration.Permission> permissions = initializer.Context.GetConfig<SecurityConfig>().Permissions;
      if (!permissions.TryGetValue(permissionSetName, out Telerik.Sitefinity.Security.Configuration.Permission _))
        return;
      permissions.Remove(permissionSetName);
    }

    private void UpgradeModuleName(DynamicModuleType moduleType, string moduleName)
    {
      if (moduleType == null)
        return;
      moduleType.ModuleName = moduleName;
      if (moduleType.Fields == null)
        return;
      foreach (DynamicModuleField field in moduleType.Fields)
        field.ModuleName = moduleName;
    }

    protected override void InstallContentViews(SiteInitializer initializer, Version upgradeFrom)
    {
    }

    private void UpgradeModulePages(
      DynamicModule dynamicModule,
      DynamicModuleType dynamicModuleType,
      SiteInitializer initializer)
    {
      PageNode pageNode1 = initializer.PageManager.GetPageNodes().FirstOrDefault<PageNode>((Expression<Func<PageNode, bool>>) (n => n.Id == dynamicModule.Id));
      if (pageNode1 == null || pageNode1.Nodes.Count != 1)
        return;
      PageNode pageNode2 = pageNode1.Nodes.First<PageNode>();
      dynamicModuleType.PageId = pageNode2.Id;
    }

    private void UpgradeConfigurations(
      DynamicModuleType moduleType,
      ContentViewConfig contentViewConfig,
      string moduleName,
      ToolboxesConfig toolboxesConfig,
      WorkflowConfig workflowConfig)
    {
      string fullTypeName = moduleType.GetFullTypeName();
      string viewDefinitionName = ModuleNamesGenerator.GenerateContentViewDefinitionName(fullTypeName);
      IConfigElementItem configElementItem;
      if (contentViewConfig.ContentViewControls.TryGetItem(viewDefinitionName, out configElementItem) && configElementItem is IConfigElementLink configElementLink && configElementLink.ModuleName == "DynamicModule")
        configElementLink.ModuleName = "ModuleBuilder";
      Toolbox toolbox;
      if (toolboxesConfig.Toolboxes.TryGetValue("PageControls", out toolbox))
      {
        ToolboxSection section = toolbox.GetSection("ContentToolboxSection");
        if (section != null)
        {
          ToolboxItem tool = section.GetTool(fullTypeName);
          if (tool != null)
            tool.ModuleName = moduleName;
        }
      }
      WorkflowElement workflowElement;
      if (!workflowConfig.Workflows.TryGetValue(fullTypeName, out workflowElement))
        return;
      workflowElement.ModuleName = moduleName;
    }

    private void UpgradeUrlData(DynamicModuleType dynamicModuleType, SiteInitializer initializer)
    {
      string fullTypeName = dynamicModuleType.GetFullTypeName();
      DynamicModuleManager managerInTransaction = initializer.GetManagerInTransaction<DynamicModuleManager>();
      using (new ElevatedModeRegion((IManager) managerInTransaction))
      {
        foreach (DynamicContent dataItem in (IEnumerable<DynamicContent>) managerInTransaction.GetDataItems(TypeResolutionService.ResolveType(dynamicModuleType.GetFullTypeName())))
        {
          DynamicContent item = dataItem;
          IQueryable<DynamicContentUrlData> urls = managerInTransaction.Provider.GetUrls<DynamicContentUrlData>();
          Expression<Func<DynamicContentUrlData, bool>> predicate = (Expression<Func<DynamicContentUrlData, bool>>) (u => u.Parent.Id == item.Id);
          foreach (DynamicContentUrlData dynamicContentUrlData in (IEnumerable<DynamicContentUrlData>) urls.Where<DynamicContentUrlData>(predicate))
            dynamicContentUrlData.ItemType = fullTypeName;
        }
      }
    }

    private void UpgradeAreaName(
      DynamicModule dynamicModule,
      DynamicModuleType dynamicModuleType,
      SiteInitializer initializer)
    {
      PageManager managerInTransaction = initializer.GetManagerInTransaction<PageManager>();
      string str = dynamicModule.Title + " - " + dynamicModuleType.DisplayName;
      string dynamicMasterView = typeof (DynamicContentViewMaster).FullName;
      string dynamicDetailView = typeof (DynamicContentViewDetail).FullName;
      IQueryable<ControlPresentation> presentationItems = managerInTransaction.GetPresentationItems<ControlPresentation>();
      Expression<Func<ControlPresentation, bool>> predicate = (Expression<Func<ControlPresentation, bool>>) (p => p.DataType == "ASP_NET_TEMPLATE" && (p.ControlType == dynamicMasterView || p.ControlType == dynamicDetailView) && p.AreaName == dynamicModule.Title);
      foreach (ControlPresentation controlPresentation in (IEnumerable<ControlPresentation>) presentationItems.Where<ControlPresentation>(predicate))
        controlPresentation.AreaName = str;
    }

    private void UpgradeFieldNamespace(
      DynamicModuleType dynamicModuleType,
      ModuleBuilderManager manager)
    {
      manager.LoadDynamicModuleTypeGraph(dynamicModuleType, true);
      foreach (DynamicModuleField field in dynamicModuleType.Fields)
        field.FieldNamespace = dynamicModuleType.GetFullTypeName();
    }

    private void UpgradePermissions(
      DynamicModule dynamicModule,
      DynamicModuleType dynamicModuleType,
      SiteInitializer initializer,
      DynamicModulesConfig dynamicModulesConfig)
    {
      ConfigElementDictionary<string, Telerik.Sitefinity.Security.Configuration.Permission> permissions = initializer.Context.GetConfig<SecurityConfig>().Permissions;
      string permissionSetName1 = "General";
      PermissionsInstaller.AddPermissionSet(dynamicModuleType.DisplayName, permissionSetName1, permissions);
      if (dynamicModuleType.CheckFieldPermissions)
      {
        foreach (DynamicModuleField field in dynamicModuleType.Fields)
        {
          if (!field.IsTransient)
          {
            string permissionSetName2 = field.GetPermissionSetName();
            PermissionsInstaller.AddFieldPermissionSet(field.Name, permissionSetName2, permissions);
          }
        }
      }
      if (dynamicModuleType.Permissions == null || dynamicModuleType.Permissions.Count == 0)
        new PermissionsInstaller(initializer.GetManagerInTransaction<ModuleBuilderManager>()).InstallModuleTypePermissions(dynamicModule, dynamicModuleType);
      this.UpgradeBackendDefinitionsConfiguration(dynamicModuleType, dynamicModulesConfig);
    }

    private void UpgradeBackendDefinitionsConfiguration(
      DynamicModuleType dynamicModuleType,
      DynamicModulesConfig dynamicModulesConfig)
    {
      ContentViewControlElement contentViewControl = dynamicModulesConfig.ContentViewControls[ModuleNamesGenerator.GenerateContentViewDefinitionName(dynamicModuleType.GetFullTypeName())];
      if (contentViewControl == null)
        return;
      MasterGridViewElement masterGridViewElement = (MasterGridViewElement) contentViewControl.ViewsConfig[ModuleNamesGenerator.GenerateListViewName(dynamicModuleType.DisplayName)];
      if (masterGridViewElement == null)
        return;
      IDecisionScreenDefinition screenDefinition = masterGridViewElement.DecisionScreens.FirstOrDefault<IDecisionScreenDefinition>((Func<IDecisionScreenDefinition, bool>) (ds => ds.Name == "NoItemsExistScreen"));
      if (screenDefinition != null && screenDefinition.Actions.FirstOrDefault<ICommandWidgetDefinition>((Func<ICommandWidgetDefinition, bool>) (a => a.Name == "Create")) is CommandWidgetElement commandWidgetElement1)
      {
        commandWidgetElement1.RelatedSecuredObjectId = dynamicModuleType.Id.ToString();
        commandWidgetElement1.ActionName = "Create";
        commandWidgetElement1.PermissionSet = "General";
        commandWidgetElement1.RelatedSecuredObjectTypeName = typeof (DynamicModuleType).FullName;
      }
      WidgetBarSectionElement barSectionElement = masterGridViewElement.ToolbarConfig.Sections.FirstOrDefault<WidgetBarSectionElement>((Func<WidgetBarSectionElement, bool>) (s => s.Name == "toolbar"));
      if (barSectionElement != null)
      {
        if (barSectionElement.Items.FirstOrDefault<WidgetElement>((Func<WidgetElement, bool>) (i => i.Name == ModuleNamesGenerator.GenerateWidgetElementName("Create"))) is CommandWidgetElement commandWidgetElement2)
        {
          commandWidgetElement2.RelatedSecuredObjectId = dynamicModuleType.Id.ToString();
          commandWidgetElement2.ActionName = "Create";
          commandWidgetElement2.PermissionSet = "General";
          commandWidgetElement2.RelatedSecuredObjectTypeName = typeof (DynamicModuleType).FullName;
        }
        if (barSectionElement.Items.FirstOrDefault<WidgetElement>((Func<WidgetElement, bool>) (i => i.Name == ModuleNamesGenerator.GenerateWidgetElementName("Delete"))) is CommandWidgetElement commandWidgetElement3)
        {
          commandWidgetElement3.RelatedSecuredObjectId = dynamicModuleType.Id.ToString();
          commandWidgetElement3.ActionName = "Delete";
          commandWidgetElement3.PermissionSet = "General";
          commandWidgetElement3.RelatedSecuredObjectTypeName = typeof (DynamicModuleType).FullName;
        }
      }
      DialogElement dialogElement = masterGridViewElement.DialogsConfig.FirstOrDefault<DialogElement>((Func<DialogElement, bool>) (d => d.Name == typeof (ModulePermissionsDialog).Name));
      if (dialogElement == null)
        return;
      string str = "?moduleName=" + "ModuleBuilder" + "&typeName=" + typeof (DynamicModuleType).FullName + "&securedObjectId=" + dynamicModuleType.Id.ToString() + "&backLabelText=" + "Back to items" + "&title=" + "Permissions" + "&permissionSetName=" + "General";
      dialogElement.Parameters = str;
    }

    private void UpgradeInactiveModuleType(
      SiteInitializer initializer,
      DynamicModule dynamicModule,
      DynamicModuleType dynamicModuleType,
      ModuleBuilderManager moduleBuilderManager,
      ToolboxesConfig toolboxesConfig,
      WorkflowConfig workflowConfig)
    {
      ModulePagesInstaller modulePagesInstaller = new ModulePagesInstaller(moduleBuilderManager.TransactionName);
      WidgetInstaller widgetInstaller = new WidgetInstaller(initializer.PageManager, moduleBuilderManager);
      moduleBuilderManager.LoadDynamicModuleGraph(dynamicModule);
      modulePagesInstaller.CreateModulePages(dynamicModule, initializer.PageManager);
      DynamicModule dynamicModule1 = dynamicModule;
      DynamicModuleType moduleType = dynamicModuleType;
      ToolboxesConfig toolboxesConfig1 = toolboxesConfig;
      widgetInstaller.Install(dynamicModule1, moduleType, toolboxesConfig1);
      if (workflowConfig.Workflows.ContainsKey(dynamicModuleType.GetFullTypeName()))
        return;
      workflowConfig.Workflows.Add(new WorkflowElement()
      {
        ContentType = dynamicModuleType.GetFullTypeName(),
        Title = dynamicModuleType.DisplayName,
        ModuleName = dynamicModule.Name
      });
    }

    /// <inheritdoc />
    protected override ConfigSection GetModuleConfig() => (ConfigSection) Telerik.Sitefinity.Configuration.Config.Get<ModuleBuilderConfig>();

    /// <inheritdoc />
    protected internal override Type[] GetConfigTypes() => new Type[2]
    {
      typeof (ModuleBuilderConfig),
      typeof (DynamicModulesConfig)
    };

    public IDictionary<string, ITypeSettings> GetTypeSettings()
    {
      IOrderedEnumerable<IDynamicModuleType> orderedEnumerable = ModuleBuilderManager.GetModules().Active().SelectMany<IDynamicModule, IDynamicModuleType>((Func<IDynamicModule, IEnumerable<IDynamicModuleType>>) (x => x.Types)).OrderBy<IDynamicModuleType, string>((Func<IDynamicModuleType, string>) (t => t.DisplayName));
      Dictionary<string, ITypeSettings> typeSettings1 = new Dictionary<string, ITypeSettings>();
      foreach (IDynamicModuleType dynamicModuleType in (IEnumerable<IDynamicModuleType>) orderedEnumerable)
      {
        Type clrType = TypeResolutionService.ResolveType(dynamicModuleType.GetFullTypeName(), false);
        if (!(clrType == (Type) null))
        {
          string plural = PluralsResolver.Instance.ToPlural(clrType.Name.ToLowerInvariant());
          ITypeSettings typeSettings2 = ContractFactory.Instance.Create(clrType, plural);
          if (typeSettings2 != null)
          {
            string parentPropName = LinqHelper.MemberName<DynamicContent>((Expression<Func<DynamicContent, object>>) (y => y.SystemParentItem));
            if (typeSettings2.Properties.FirstOrDefault<IPropertyMapping>((Func<IPropertyMapping, bool>) (x => x.Name == parentPropName)) is NavigationPropertyMappingProxy propertyMappingProxy1)
            {
              if (dynamicModuleType.ParentType != null)
              {
                propertyMappingProxy1.Parameters.Add("relatedType", dynamicModuleType.ParentType.GetFullTypeName());
                propertyMappingProxy1.Parameters.Add("propName", "sfParent");
                propertyMappingProxy1.Parameters.Add("isParentReference", bool.TrueString);
                propertyMappingProxy1.Name = "Parent";
                string propParentIdName = LinqHelper.MemberName<DynamicContent>((Expression<Func<DynamicContent, object>>) (y => (object) y.SystemParentId));
                if (!(typeSettings2.Properties.FirstOrDefault<IPropertyMapping>((Func<IPropertyMapping, bool>) (x => x.Name == propParentIdName)) is PersistentPropertyMappingProxy propertyMappingProxy))
                {
                  propertyMappingProxy = new PersistentPropertyMappingProxy();
                  typeSettings2.Properties.Add((IPropertyMapping) propertyMappingProxy);
                }
                propertyMappingProxy.PersistentName = propParentIdName;
                propertyMappingProxy.Name = "ParentId";
              }
              else
                typeSettings2.Properties.Remove((IPropertyMapping) propertyMappingProxy1);
            }
            typeSettings1.Add(typeSettings2.ClrType, typeSettings2);
          }
        }
      }
      return (IDictionary<string, ITypeSettings>) typeSettings1;
    }

    /// <inheritdoc />
    object ITrackingReporter.GetReport()
    {
      bool flag = false;
      foreach (DynamicModuleDataProvider staticProviders in (Collection<DynamicModuleDataProvider>) ManagerBase<DynamicModuleDataProvider>.StaticProvidersCollection)
      {
        flag = flag || staticProviders.FilterQueriesByViewPermissions;
        if (flag)
          break;
      }
      return (object) new ModuleBuilderModuleReport()
      {
        ModuleName = "ModuleBuilder",
        FilterQueriesByViewPermissions = flag
      };
    }
  }
}
