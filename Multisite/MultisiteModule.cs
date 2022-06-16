// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Multisite.MultisiteModule
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.UI;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.DataSource;
using Telerik.Sitefinity.DynamicModules.Builder.Web.UI;
using Telerik.Sitefinity.Fluent.Modules;
using Telerik.Sitefinity.Licensing;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Localization.UrlLocalizationStrategies;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Multisite.Configuration;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Multisite.Web.Services;
using Telerik.Sitefinity.Multisite.Web.UI;
using Telerik.Sitefinity.Owin;
using Telerik.Sitefinity.Packaging.Content;
using Telerik.Sitefinity.Project.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Events;
using Telerik.Sitefinity.UsageTracking.TrackingReporters;
using Telerik.Sitefinity.Web.Services.Contracts;

namespace Telerik.Sitefinity.Multisite
{
  /// <summary>
  /// Manage multiple sites through one Sitefinity instance. Share content and templates  across the sites.
  /// </summary>
  internal sealed class MultisiteModule : SecuredModuleBase, ITypeSettingsProvider, ITrackingReporter
  {
    /// <summary>
    /// Name of the multisite module. (e.g. used in MultisiteManager)
    /// </summary>
    public const string ModuleName = "MultisiteInternal";
    /// <summary>Id of the module. Used for licensing.</summary>
    public const string ModuleId = "FBD4773B-8688-4C75-8563-28BFDA27A185";
    /// <summary>
    /// The identifier of the OLD Multisite management site node
    /// </summary>
    public static readonly Guid HomePageId = new Guid("C97C0070-6D6F-4E25-9D43-CFC2D6294ED2");
    /// <summary>The name of the Multisite management site node</summary>
    public const string HomePageName = "MultisiteManagement";
    /// <summary>
    /// The identifier of the Multisite administration group page node
    /// </summary>
    public static readonly Guid AdministrationGroupPageId = new Guid("26C8497B-E76C-40E4-B156-FADAB675EC0B");
    /// <summary>
    /// The name of the Multisite administration group page node
    /// </summary>
    public const string AdministrationGroupPageName = "MultisiteAdministration";
    /// <summary>
    /// The identifier of the Multisite management site settings node
    /// </summary>
    public static readonly Guid SiteSettingsPageId = new Guid("6DFCE83F-148E-459A-9ED8-5C33872323BA");
    /// <summary>
    /// The name of the Multisite management site settings node
    /// </summary>
    public const string SiteSettingsPageName = "SiteSettings";
    /// <summary>
    /// The identifier of the Multisite management site settings node
    /// </summary>
    public static readonly Guid ProvidersPageId = new Guid("D54C439E-29C6-4847-8D15-67CBCEDF4148");
    /// <summary>
    /// The name of the Multisite management site settings node
    /// </summary>
    public const string ProvidersPageName = "Configure Providers";
    /// <summary>Name of the multisite group page</summary>
    public static readonly string MultisiteGroupName = "MultisiteManagement";
    /// <summary>Identity of the multisite group page</summary>
    public static readonly Guid MultisitePageGroupId = new Guid("B60376F1-9B64-45D1-8708-E3D710CA416B");
    /// <summary>Relative URL of the multisite web service</summary>
    public const string WebServiceUrl = "Sitefinity/Services/Multisite/Multisite.svc/";
    private static readonly Type[] managerTypes = new Type[1]
    {
      typeof (MultisiteManager)
    };

    /// <summary>
    /// Gets the landing page id for each module inherit from <see cref="T:Telerik.Sitefinity.Services.SecuredModuleBase" /> class.
    /// </summary>
    /// <value>The landing page id.</value>
    public override Guid LandingPageId => MultisiteModule.HomePageId;

    /// <summary>
    /// Gets the CLR types of all data managers provided by this module.
    /// </summary>
    /// <value>An array of <see cref="T:System.Type" /> objects.</value>
    public override Type[] Managers => MultisiteModule.managerTypes;

    /// <summary>Gets the title displayed for this service.</summary>
    /// <value>The title.</value>
    public override string Title => Res.Get(typeof (MultisiteResources).Name, "ModuleTitle", SystemManager.CurrentContext.Culture, true, false);

    protected internal override ManagersInitializationMode ManagersInitializationMode => ManagersInitializationMode.OnStartup;

    /// <summary>Initializes the service with specified settings.</summary>
    /// <param name="settings">The settings.</param>
    public override void Initialize(ModuleSettings settings)
    {
      App.WorkWith().Module(settings.Name).Initialize().Configuration<MultisiteConfig>().Localization<MultisiteResources>().SitemapFilter<MultisiteNodeFilter>().WebService(typeof (MultisiteService), "Sitefinity/Services/Multisite/Multisite.svc/").Dialog<SitesUsageDialog>().Dialog<SiteChoiceSelectorDialog>().Dialog<ShareItemDialog>().Dialog<SetTaxonomyDialog>().Dialog<TaxonomySitesUsageDialog>().Dialog<MultisiteTaxonomiesSiteSelectorDialog>().Dialog<PermissionsSitesUsageDialog>();
      SystemManager.CurrentContext = (SitefinityContextBase) new MultisiteContext();
      base.Initialize(settings);
      SitefinityMiddlewareFactory.Current.AddIfNotPresentMiddleware<RedirectLoginMiddleware>(stage: PipelineStage.PostAuthenticate);
      SystemManager.TypeRegistry.Register(typeof (Site).FullName, new SitefinityType()
      {
        Parent = (string) null,
        PluralTitle = "Sites",
        SingularTitle = "Site",
        ModuleName = this.Name,
        ResourceClassId = typeof (MultisiteResources).Name,
        Kind = SitefinityTypeKind.Type
      });
      ObjectFactory.Container.RegisterType<IContentTransfer, MultisiteContentTransfer>(new MultisiteContentTransfer().Area, (LifetimeManager) new ContainerControlledLifetimeManager());
    }

    /// <summary>
    /// Installs this module in Sitefinity system for the first time.
    /// </summary>
    /// <param name="initializer">The Site Initializer. A helper class for installing Sitefinity modules.</param>
    public override void Install(SiteInitializer initializer)
    {
      if (this.ShouldInstallDefaultSite(initializer))
        this.InstallDefaultSite();
      else
        initializer.Upgrade((object) this, 7399);
      this.CreatePagesAndRegisterControls(initializer);
      SystemManager.ModulesInitialized += new EventHandler<SystemInitializationEventArgs>(this.SystemManager_ModulesInitialized);
    }

    /// <summary>Registers first site data source registration</summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SystemManager_ModulesInitialized(object sender, SystemInitializationEventArgs e)
    {
      try
      {
        MultisiteModule.InitializeSingleSiteSystemResources();
      }
      catch (Exception ex)
      {
        if (!Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
          return;
        throw;
      }
      finally
      {
        SystemManager.ModulesInitialized -= new EventHandler<SystemInitializationEventArgs>(this.SystemManager_ModulesInitialized);
      }
    }

    /// <summary>
    /// Initializes the single site resources (data sources and managers).
    /// They are registered by default in the system and by modules and have to be registered in the site.
    /// </summary>
    /// <param name="manager">The manager.</param>
    /// <param name="sitesList">The sites list.</param>
    /// <returns></returns>
    private static void InitializeSingleSiteSystemResources()
    {
      string transactionName = "InitializeDefaultSiteResources";
      MultisiteManager manager = MultisiteManager.GetManager((string) null, transactionName);
      using (new ElevatedModeRegion((IManager) manager))
      {
        List<Site> list = manager.GetSites().ToList<Site>();
        if (list.Count<Site>() != 1)
          return;
        Site singleSite = list.First<Site>();
        Guid id = singleSite.Id;
        MultisiteModule.InitializeSiteDataSources(transactionName, manager, singleSite, id);
        MultisiteModule.InitializeSiteManagers(transactionName, id);
        TransactionManager.CommitTransaction(transactionName);
      }
    }

    private static void InitializeSiteDataSources(
      string transactionName,
      MultisiteManager manager,
      Site singleSite,
      Guid siteId)
    {
      if (singleSite.SiteDataSourceLinks != null && singleSite.SiteDataSourceLinks.Any<SiteDataSourceLink>())
        return;
      IEnumerable<Telerik.Sitefinity.Data.DataSource.IDataSource> dataSources = SystemManager.DataSourceRegistry.GetDataSources();
      string empty = string.Empty;
      if (dataSources.Count<Telerik.Sitefinity.Data.DataSource.IDataSource>() <= 0)
        return;
      foreach (Telerik.Sitefinity.Data.DataSource.IDataSource dataSource in dataSources)
      {
        string defaultProvider = dataSource.GetDefaultProvider();
        foreach (DataProviderInfo providerInfo in dataSource.ProviderInfos)
        {
          SiteDataSourceLink siteDataSourceLink = manager.CreateSiteDataSourceLink();
          siteDataSourceLink.DataSource = manager.GetOrCreateDataSource(dataSource.Name, providerInfo.ProviderName, providerInfo.ProviderTitle);
          siteDataSourceLink.IsDefault = !string.IsNullOrEmpty(defaultProvider) && providerInfo.ProviderName.Equals(defaultProvider);
          singleSite.SiteDataSourceLinks.Add(siteDataSourceLink);
        }
      }
      TransactionManager.FlushTransaction(transactionName);
    }

    /// <summary>Gets the type settings.</summary>
    /// <returns>The type settings.</returns>
    public IDictionary<string, ITypeSettings> GetTypeSettings()
    {
      Type clrType = typeof (Site);
      string plural = PluralsResolver.Instance.ToPlural(clrType.Name.ToLower());
      ITypeSettings typeSettings = ContractFactory.Instance.Create(clrType, plural);
      IList<IPropertyMapping> properties = typeSettings.Properties;
      CalculatedPropertyMappingProxy propertyMappingProxy = new CalculatedPropertyMappingProxy();
      propertyMappingProxy.Name = "CulturesMap";
      propertyMappingProxy.ResolverType = typeof (CulturesMapProperty).FullName;
      propertyMappingProxy.SelectedByDefault = false;
      properties.Add((IPropertyMapping) propertyMappingProxy);
      return (IDictionary<string, ITypeSettings>) new Dictionary<string, ITypeSettings>()
      {
        {
          typeSettings.ClrType,
          typeSettings
        }
      };
    }

    private static void InitializeSiteManagers(string transactionName, Guid siteId)
    {
      foreach (Type multisiteEnabledManager in (IEnumerable<Type>) SystemManager.MultisiteEnabledManagers)
      {
        try
        {
          IMultisiteEnabledManager managerInTransaction1 = (IMultisiteEnabledManager) ManagerBase.GetManagerInTransaction(multisiteEnabledManager, (string) null, transactionName);
          IEnumerable<Type> types = ((IEnumerable<Type>) managerInTransaction1.GetShareableTypes()).Union<Type>((IEnumerable<Type>) managerInTransaction1.GetSiteSpecificTypes());
          foreach (DataProviderBase staticProvider in managerInTransaction1.StaticProviders)
          {
            IMultisiteEnabledManager managerInTransaction2 = (IMultisiteEnabledManager) ManagerBase.GetManagerInTransaction(multisiteEnabledManager, staticProvider.Name, transactionName);
            foreach (Type itemType1 in types)
            {
              foreach (IDataItem dataItem in managerInTransaction2.GetItems(itemType1, string.Empty, string.Empty, 0, 0).OfType<IDataItem>().ToList<IDataItem>())
              {
                Guid itemId = dataItem.Id;
                string itemType = itemType1.FullName;
                if (!managerInTransaction2.GetSiteItemLinks().Where<SiteItemLink>((Expression<Func<SiteItemLink, bool>>) (l => l.ItemId == itemId && l.ItemType == itemType && l.SiteId == siteId)).Any<SiteItemLink>())
                {
                  SiteItemLink siteItemLink = managerInTransaction2.CreateSiteItemLink();
                  siteItemLink.ItemId = itemId;
                  siteItemLink.ItemType = itemType;
                  siteItemLink.SiteId = siteId;
                  if (managerInTransaction2.Provider.GetDirtyItems().Count > 50)
                    TransactionManager.FlushTransaction(transactionName);
                }
              }
            }
          }
        }
        catch (TargetInvocationException ex)
        {
          Log.Write((object) ex);
        }
      }
    }

    private void InstallDefaultSite() => SystemManager.MultisiteContext.ChangeCurrentSite(SystemManager.MultisiteContext.GetSiteById(this.GetOrCreateDefaultSite().Id));

    /// <summary>
    /// Loads the module dependencies after the module has been initialized and installed.
    /// </summary>
    public override void Load()
    {
      ObjectFactory.Container.RegisterType<UrlLocalizationService, MultisiteUrlLocalizationService>((LifetimeManager) new ContainerControlledLifetimeManager());
      ManagerBase<ConfigProvider>.Executed += new EventHandler<ExecutedEventArgs>(this.ConfigManager_Executed);
    }

    /// <inheritdoc />
    public override void Unload()
    {
      ManagerBase<ConfigProvider>.Executed -= new EventHandler<ExecutedEventArgs>(this.ConfigManager_Executed);
      base.Unload();
    }

    /// <summary>
    /// Cleaning up sites' cultures which were removed from the system's basic settings (resources config).
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="args">The <see cref="T:Telerik.Sitefinity.Data.ExecutedEventArgs" /> instance containing the event data.</param>
    private void ConfigManager_Executed(object sender, ExecutedEventArgs args)
    {
      if (args == null || !(args.CommandName == "SaveSection") || !(args.CommandArguments.GetType() == typeof (ResourcesConfig)))
        return;
      ResourcesConfig commandArguments = (ResourcesConfig) args.CommandArguments;
      if (!commandArguments.Initialized)
        return;
      MultisiteModule.UpdateSitesCultures(commandArguments);
    }

    private static void UpdateSitesCultures(ResourcesConfig resourcesConfig)
    {
      ConfigElementDictionary<string, CultureElement> cultures = resourcesConfig.Cultures;
      MultisiteManager manager = MultisiteManager.GetManager();
      IQueryable<Site> sites = manager.GetSites();
      bool flag1 = false;
      bool flag2 = false;
      foreach (Site site1 in (IEnumerable<Site>) sites)
      {
        Site site = site1;
        if (!site.CultureKeys.Any<string>())
          flag2 = true;
        for (int currentCultureKey = site.CultureKeys.Count - 1; currentCultureKey >= 0; currentCultureKey--)
        {
          if (!cultures.Values.Any<CultureElement>((Func<CultureElement, bool>) (cult => cult.Key == site.CultureKeys[currentCultureKey])))
          {
            site.CultureKeys.Remove(site.CultureKeys[currentCultureKey]);
            flag1 = true;
          }
        }
      }
      if (flag1)
      {
        manager.SaveChanges();
      }
      else
      {
        if (!flag2)
          return;
        CacheDependency.Notify((IList<CacheDependencyKey>) new List<CacheDependencyKey>()
        {
          new CacheDependencyKey()
          {
            Type = typeof (Site),
            Key = (string) null
          }
        });
      }
    }

    private void CreatePagesAndRegisterControls(SiteInitializer initializer) => initializer.Installer.CreateModuleGroupPage(MultisiteModule.AdministrationGroupPageId, "MultisiteAdministration").PlaceUnder(CommonNode.Root).HideFromNavigation().SetOrdinal(11).LocalizeUsing<MultisiteResources>().SetTitleLocalized("AdministrationGroupPageTitle").SetDescriptionLocalized("AdministrationGroupPageDescription").SetUrlNameLocalized("AdministrationGroupPageUrlName").AddChildPage(MultisiteModule.HomePageId, "MultisiteManagement").HideFromNavigation().SetOrdinal(1).LocalizeUsing<MultisiteResources>().SetTitleLocalized("MultisiteManagementTitle").SetUrlNameLocalized("MultisiteManagementUrlName").SetDescriptionLocalized("MultisiteManagementDescription").SetHtmlTitleLocalized("MultisiteManagementHtmlTitle").SetTemplate(SiteInitializer.BackendHtml5TemplateId).AddControl((Control) new SitesView()).Done().AddChildPage(MultisiteModule.SiteSettingsPageId, "SiteSettings").PlaceUnder(MultisiteModule.AdministrationGroupPageId).HideFromNavigation().SetOrdinal(2).LocalizeUsing<MultisiteResources>().SetTitleLocalized("SiteSettingsTitle").SetUrlNameLocalized("SiteSettingsUrlName").SetDescriptionLocalized("SiteSettingsDescription").SetHtmlTitleLocalized("SiteSettingsHtmlTitle").SetTemplate(SiteInitializer.BackendHtml5TemplateId).AddControl((Control) new SiteSettingsView()).Done().Done().PageToolbox().NavigationControlsSection().LoadOrAddWidget<SiteSelectorControl>("SiteSelectorControlName").SetTitle("SiteSelectorControlTitle").SetDescription("SiteSelectorControlDescription").LocalizeUsing<MultisiteResources>().SetCssClass("sfSiteSelectorIcn").Done().Done().Done().RegisterControlTemplate<SiteSelectorControl>("Telerik.Sitefinity.Resources.Templates.Frontend.Multisite.SiteSelectorControl.ascx", "Site selector template");

    /// <summary>Gets the module config.</summary>
    /// <returns></returns>
    protected override ConfigSection GetModuleConfig() => (ConfigSection) Config.Get<MultisiteConfig>();

    internal bool ShouldInstallDefaultSite(SiteInitializer siteInitializer)
    {
      string transactionName = "CleanSites";
      MultisiteManager manager = MultisiteManager.GetManager((string) null, transactionName);
      using (new ElevatedModeRegion((IManager) manager))
      {
        if (manager.GetSites().Any<Site>())
        {
          if (SystemManager.ApplicationModules.ContainsKey("Multisite") && LicenseState.CheckIsModuleLicensedInAnyDomain("FBD4773B-8688-4C75-8563-28BFDA27A185"))
            return false;
          bool flag = false;
          if (!LicenseState.CheckIsModuleLicensedInAnyDomain("FBD4773B-8688-4C75-8563-28BFDA27A185"))
          {
            flag = true;
          }
          else
          {
            ModuleVersion moduleVersion = siteInitializer.MetadataManager.GetModuleVersion("Multisite");
            if (moduleVersion == null || moduleVersion.Version != Assembly.GetAssembly(typeof (MultisiteModule_Obsolete)).GetName().Version)
              flag = true;
          }
          if (!flag)
          {
            ModuleVersion moduleVersion = siteInitializer.MetadataManager.GetModuleVersion("Multisite");
            if (moduleVersion == null || moduleVersion.Version != Assembly.GetAssembly(typeof (MultisiteModule_Obsolete)).GetName().Version)
              flag = true;
          }
          if (flag)
          {
            foreach (Site site in manager.GetSites().ToList<Site>())
              manager.Provider.Delete(site);
            foreach (SiteDataSource source in manager.GetDataSources().ToList<SiteDataSource>())
              manager.Provider.Delete(source);
            TransactionManager.CommitTransaction(transactionName);
          }
        }
      }
      return true;
    }

    internal Site GetOrCreateDefaultSite()
    {
      string transactionName = "CreateDefaultSite";
      MultisiteManager manager = MultisiteManager.GetManager((string) null, transactionName);
      using (new ElevatedModeRegion((IManager) manager))
      {
        ProjectConfig projectConfig = Config.Get<ProjectConfig>();
        Guid siteMapRootId = projectConfig.DefaultSite.SiteMapRootNodeId;
        Site defaultSite = manager.GetSites().Where<Site>((Expression<Func<Site, bool>>) (s => s.SiteMapRootNodeId == siteMapRootId)).FirstOrDefault<Site>();
        if (defaultSite == null)
        {
          defaultSite = !(projectConfig.DefaultSite.Id != Guid.Empty) ? manager.CreateSite() : manager.CreateSite(projectConfig.DefaultSite.Id);
          defaultSite.IsDefault = true;
          defaultSite.IsOffline = false;
          defaultSite.HomePageId = projectConfig.DefaultSite.HomePageId;
          defaultSite.SiteMapRootNodeId = siteMapRootId;
          defaultSite.Name = !string.IsNullOrEmpty(projectConfig.DefaultSite.Name) ? projectConfig.DefaultSite.Name : "Default";
          SiteUrlSettings siteUrlSettings = Config.Get<SystemConfig>().SiteUrlSettings;
          string str = siteUrlSettings.Host;
          if (str.IsNullOrEmpty())
            str = "localhost";
          if (siteUrlSettings.EnableNonDefaultSiteUrlSettings)
          {
            if (!string.IsNullOrEmpty(siteUrlSettings.NonDefaultHttpsPort))
              str = str + ":" + siteUrlSettings.NonDefaultHttpsPort;
            else if (!string.IsNullOrEmpty(siteUrlSettings.NonDefaultHttpPort))
              str = str + ":" + siteUrlSettings.NonDefaultHttpPort;
          }
          defaultSite.LiveUrl = str;
          string frontEndLoginPageUrl = projectConfig.DefaultSite.FrontEndLoginPageUrl;
          Guid frontEndLoginPageId = projectConfig.DefaultSite.FrontEndLoginPageId;
          if (!string.IsNullOrEmpty(frontEndLoginPageUrl))
            defaultSite.FrontEndLoginPageUrl = frontEndLoginPageUrl;
          else if (frontEndLoginPageId != Guid.Empty)
            defaultSite.FrontEndLoginPageId = frontEndLoginPageId;
          Guid id = defaultSite.Id;
          TransactionManager.CommitTransaction(transactionName);
        }
        return defaultSite;
      }
    }

    object ITrackingReporter.GetReport()
    {
      IEnumerable<ISite> sites = SystemManager.CurrentContext.GetSites();
      int num = sites.Count<ISite>();
      List<int> intList = sites.Select<ISite, int>((Func<ISite, int>) (x => x.PublicCultures.Count)).ToList<int>();
      if (num == 1 && intList[0] == 0)
        intList = new List<int>(Config.Get<ResourcesConfig>().Cultures.Count);
      List<string> list = sites.Select<ISite, string>((Func<ISite, string>) (x => x.DefaultCulture.Name)).ToList<string>();
      return (object) new MultisiteModuleReport()
      {
        ModuleName = "Multisite",
        SitesCount = num,
        MultiSiteLanguagesDistrubution = intList,
        DefaultLanguagesPerSite = list
      };
    }

    [UpgradeInfo(Description = "Update module backend pages title localization.", FailMassage = "Failed to update module backend pages title localization.", Id = "154C1C45-9B0F-4BF1-8CAF-81345CF3BE9D", UpgradeTo = 7400)]
    private void UpdateBackendPagesTitleLocalization(SiteInitializer initializer) => this.CreatePagesAndRegisterControls(initializer);

    [UpgradeInfo(Description = "Update site data source links.", FailMassage = "Failed to update Update site data source links.", Id = "B420ADA5-F684-4C20-828C-7C45BDC24958", UpgradeTo = 7600)]
    private void UpgradeDatSourceLinks(SiteInitializer initializer)
    {
      SystemManager.ModulesInitialized -= new EventHandler<SystemInitializationEventArgs>(this.SystemManager_ModulesInitialized_UpgradeDatSourceLink);
      SystemManager.ModulesInitialized += new EventHandler<SystemInitializationEventArgs>(this.SystemManager_ModulesInitialized_UpgradeDatSourceLink);
    }

    private void SystemManager_ModulesInitialized_UpgradeDatSourceLink(
      object sender,
      SystemInitializationEventArgs e)
    {
      MultisiteManager manager = MultisiteManager.GetManager();
      IQueryable<SiteDataSource> dataSources = manager.GetDataSources();
      IEnumerable<ISite> sites = SystemManager.CurrentContext.MultisiteContext.GetSites();
      foreach (SiteDataSource siteDataSource1 in (IEnumerable<SiteDataSource>) dataSources)
      {
        SiteDataSource siteDataSource = siteDataSource1;
        if (string.IsNullOrEmpty(siteDataSource.Title))
        {
          Telerik.Sitefinity.Data.DataSource.IDataSource dataSource = SystemManager.DataSourceRegistry.GetDataSources().SingleOrDefault<Telerik.Sitefinity.Data.DataSource.IDataSource>((Func<Telerik.Sitefinity.Data.DataSource.IDataSource, bool>) (ds => ds.Name == siteDataSource.Name));
          if (dataSource != null)
          {
            DataProviderInfo dataProviderInfo = dataSource.ProviderInfos.FirstOrDefault<DataProviderInfo>((Func<DataProviderInfo, bool>) (pi => pi.ProviderName.Equals(siteDataSource.Provider, StringComparison.OrdinalIgnoreCase)));
            if (dataProviderInfo != null)
            {
              siteDataSource.Title = dataProviderInfo.ProviderTitle;
              IEnumerable<ISite> source = sites.Where<ISite>((Func<ISite, bool>) (s => siteDataSource.Title.StartsWith(s.Name)));
              if (source.Count<ISite>() == 1)
              {
                ISite site = source.First<ISite>();
                siteDataSource.OwnerSiteId = site.Id;
              }
              else if (source.Count<ISite>() > 1)
              {
                ISite site = source.OrderByDescending<ISite, int>((Func<ISite, int>) (s => s.Name.Length)).First<ISite>();
                siteDataSource.OwnerSiteId = site.Id;
              }
              else if (siteDataSource.Sites.Any<SiteDataSourceLink>())
                siteDataSource.OwnerSiteId = siteDataSource.Sites.FirstOrDefault<SiteDataSourceLink>().SiteId;
            }
          }
        }
      }
      manager.SaveChanges();
      SystemManager.ModulesInitialized -= new EventHandler<SystemInitializationEventArgs>(this.SystemManager_ModulesInitialized_UpgradeDatSourceLink);
    }
  }
}
