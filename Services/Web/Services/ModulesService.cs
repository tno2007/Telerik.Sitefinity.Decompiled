// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Web.Services.ModulesService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.DataSource;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Data;
using Telerik.Sitefinity.DynamicModules.Builder.Install;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Licensing;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Services.Web.Services.ViewModel;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Services.Web.Services
{
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  internal class ModulesService : IModulesService
  {
    /// <summary>
    ///     <para>The web service relative URL of the Modules Service.</para>
    ///     <para>'Sitefinity/Services/ModulesService'</para>
    /// </summary>
    internal const string WebServiceUrl = "Sitefinity/Services/ModulesService";
    private const string defaultModuleSort = "Title";

    /// <inheritdoc />
    public CollectionContext<ModuleViewModel> GetModules(
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      ServiceUtility.RequestAuthentication(true);
      CollectionContext<ModuleViewModel> modules = (CollectionContext<ModuleViewModel>) null;
      try
      {
        List<ModuleViewModel> moduleViewModelList = new List<ModuleViewModel>();
        ModulesService.AppendStaticModules(moduleViewModelList);
        ModulesService.AppendDynamicModules(moduleViewModelList);
        IQueryable<ModuleViewModel> source = moduleViewModelList.AsQueryable<ModuleViewModel>();
        int num = source.Count<ModuleViewModel>();
        if (string.IsNullOrWhiteSpace(sortExpression))
          sortExpression = "Title";
        IQueryable<ModuleViewModel> queryable = source.OrderBy<ModuleViewModel>(sortExpression);
        if (!string.IsNullOrEmpty(filter))
          queryable = queryable.Where<ModuleViewModel>(filter);
        if (skip > 0)
          queryable = queryable.Skip<ModuleViewModel>(skip);
        if (take > 0)
          queryable = queryable.Take<ModuleViewModel>(take);
        modules = new CollectionContext<ModuleViewModel>((IEnumerable<ModuleViewModel>) queryable)
        {
          TotalCount = num
        };
      }
      catch (Exception ex)
      {
        if (Exceptions.HandleException(ex, ExceptionPolicyName.UnhandledExceptions))
          throw;
      }
      ServiceUtility.DisableCache();
      return modules;
    }

    /// <inheritdoc />
    public ModuleViewModel Execute(ModuleViewModel module, ModuleOperation operation)
    {
      ServiceUtility.RequestAuthentication(true);
      ModuleViewModel moduleViewModel = (ModuleViewModel) null;
      try
      {
        switch (module.ModuleType)
        {
          case ModuleType.Static:
            moduleViewModel = ModulesService.ExecuteStaticModuleOperation(module, operation);
            break;
          case ModuleType.Dynamic:
            moduleViewModel = ModulesService.ExecuteDynamicModuleOperation(module, operation);
            break;
          default:
            throw new ArgumentException(string.Format("Unsupported module type: {0}", (object) Enum.GetName(typeof (ModuleType), (object) module.ModuleType)));
        }
      }
      catch (Exception ex)
      {
        if (Exceptions.HandleException(ex, ExceptionPolicyName.UnhandledExceptions))
          throw;
      }
      ServiceUtility.DisableCache();
      return moduleViewModel;
    }

    /// <inheritdoc />
    public bool BatchExecute(string[] ids, string operation)
    {
      ServiceUtility.RequestAuthentication(true);
      ServiceUtility.DisableCache();
      throw new NotImplementedException();
    }

    /// <summary>
    /// /// Append all standard (static) modules to the given <paramref name="modulesList" />
    /// </summary>
    /// <param name="modulesList"></param>
    private static void AppendStaticModules(List<ModuleViewModel> modulesList)
    {
      List<AppModuleSettings> list = Config.Get<SystemConfig>().ApplicationModules.Values.Where<AppModuleSettings>((Func<AppModuleSettings, bool>) (m => !m.Hidden)).ToList<AppModuleSettings>();
      IQueryable<ModuleVersion> moduleVersions = MetadataManager.GetManager().GetModuleVersions();
      foreach (AppModuleSettings appModuleSettings in list)
      {
        AppModuleSettings configModule = appModuleSettings;
        string str;
        if (SystemManager.NotLoadedModulesErrors.TryGetValue(configModule.Name, out str))
        {
          configModule.ErrorMessage = str;
        }
        else
        {
          ModuleVersion moduleVersion = moduleVersions.SingleOrDefault<ModuleVersion>((Expression<Func<ModuleVersion, bool>>) (m => string.Equals(m.ModuleName, configModule.Name)));
          if (moduleVersion != null)
          {
            configModule.Version = moduleVersion.Version;
            configModule.ErrorMessage = moduleVersion.ErrorMessage;
          }
        }
        modulesList.Add(new ModuleViewModel(configModule));
      }
    }

    /// <summary>
    /// Append all dynamic modules to the given <paramref name="modulesList" />
    /// </summary>
    /// <param name="modulesList"></param>
    private static void AppendDynamicModules(List<ModuleViewModel> modulesList)
    {
      if (!SystemManager.IsModuleEnabled("ModuleBuilder"))
        return;
      foreach (DataProviderBase dataProviderBase in (Collection<ModuleBuilderDataProvider>) (ManagerBase<ModuleBuilderDataProvider>.StaticProvidersCollection ?? ModuleBuilderManager.GetManager().StaticProviders))
      {
        using (ModuleBuilderManager manager = ModuleBuilderManager.GetManager(dataProviderBase.Name))
          manager.GetDynamicModules().ToList<DynamicModule>().ForEach((Action<DynamicModule>) (dm => modulesList.Add(new ModuleViewModel(dm))));
      }
    }

    private static ModuleViewModel ExecuteDynamicModuleOperation(
      ModuleViewModel module,
      ModuleOperation operation)
    {
      ModuleViewModel moduleViewModel = module;
      string transactionName = ModuleInstaller.GetTransactionName(module.ModuleId);
      using (ModuleBuilderManager manager1 = ModuleBuilderManager.GetManager(module.ProviderName, transactionName))
      {
        DynamicModule dynamicModule1 = manager1.GetDynamicModule(module.ModuleId);
        if (dynamicModule1 == null)
          throw new ArgumentException(string.Format("There is no such module with Id {0}", (object) module.ModuleId));
        ModuleInstaller moduleInstaller = new ModuleInstaller(module.ProviderName, transactionName);
        switch (operation)
        {
          case ModuleOperation.Install:
          case ModuleOperation.Activate:
            moduleInstaller.InstallModule(dynamicModule1);
            break;
          case ModuleOperation.Uninstall:
          case ModuleOperation.Deactivate:
            moduleInstaller.UninstallModule(dynamicModule1);
            break;
          case ModuleOperation.Delete:
            DeleteModuleContext settings = new DeleteModuleContext();
            moduleInstaller.DeleteModuleData(dynamicModule1, settings);
            moduleInstaller.DeleteModule(dynamicModule1, settings);
            break;
          case ModuleOperation.Edit:
            ModulesService.UpdateDynamicModule(module, manager1);
            TransactionManager.CommitTransaction(transactionName);
            break;
          default:
            throw new ArgumentException(string.Format("Unsupported operation: {0}", (object) operation));
        }
        using (ModuleBuilderManager manager2 = ModuleBuilderManager.GetManager(module.ProviderName, string.Format("Operation: {0}, Module Update Result {1}", (object) operation, (object) module.Name)))
        {
          if (manager2.ModuleExists(module.ModuleId))
          {
            DynamicModule dynamicModule2 = manager2.GetDynamicModule(module.ModuleId);
            if (dynamicModule2 != null)
              moduleViewModel = new ModuleViewModel(dynamicModule2);
          }
        }
      }
      return moduleViewModel;
    }

    private static void UpdateDynamicModule(ModuleViewModel module, ModuleBuilderManager mbManager)
    {
      ContentTypeSimpleContext context = new ContentTypeSimpleContext()
      {
        ContentTypeTitle = module.Title,
        ContentTypeDescription = module.Description
      };
      mbManager.UpdateDynamicModuleNameAndDescription(module.ModuleId, context);
    }

    private static ModuleViewModel ExecuteStaticModuleOperation(
      ModuleViewModel module,
      ModuleOperation operation)
    {
      bool flag = !(module.ModuleId != Guid.Empty) || LicenseState.CheckIsModuleLicensedInAnyDomain(module.ModuleId);
      ModuleViewModel module1;
      switch (operation)
      {
        case ModuleOperation.Install:
          if (!flag)
            throw new ArgumentException(Res.Get(typeof (Labels), "ModuleNotSupportedByLicenseMessage") + Res.Get(typeof (Labels), "LicenseNotGrantedInstallMessage"));
          module1 = ModulesService.AddStaticModule(module);
          break;
        case ModuleOperation.Uninstall:
          SystemManager.UninstallModule(module.Name);
          module.StartupType = StartupType.Disabled;
          module1 = ModulesService.UpdateStaticModule(module);
          break;
        case ModuleOperation.Activate:
          if (!flag)
            throw new ArgumentException(Res.Get(typeof (Labels), "ModuleNotSupportedByLicenseMessage") + Res.Get(typeof (Labels), "LicenseNotGrantedActivateMessage"));
          module.StartupType = StartupType.OnApplicationStart;
          module1 = ModulesService.UpdateStaticModule(module);
          break;
        case ModuleOperation.Deactivate:
          module.StartupType = StartupType.Disabled;
          module1 = ModulesService.UpdateStaticModule(module);
          break;
        case ModuleOperation.Delete:
          SystemManager.UninstallModule(module.Name);
          ModulesService.DeleteStaticModule(module);
          module1 = module;
          break;
        case ModuleOperation.Edit:
          if (!flag)
            throw new ArgumentException(Res.Get(typeof (Labels), "ModuleNotSupportedByLicenseMessage") + Res.Get(typeof (Labels), "LicenseNotGrantedEditMessage"));
          module1 = ModulesService.UpdateStaticModule(module);
          break;
        default:
          throw new ArgumentException(string.Format("Unsupported operation: {0}", (object) operation));
      }
      OperationReason restartReason = OperationReason.FromKey("StaticModulesUpdate");
      restartReason.AddInfo(module.Name);
      restartReason.AddInfo("Operation", operation.ToString());
      if (module.Type.StartsWith(typeof (MultisiteModule).FullName) || module.Type.StartsWith("Telerik.Sitefinity.WebSecurity.WebSecurityModule"))
      {
        SystemManager.TryRestartApplication(restartReason.ToString());
      }
      else
      {
        SystemManager.RestartApplication(restartReason);
        SystemManager.InvalidateModuleDependencies(module.Name);
        if (operation == ModuleOperation.Install)
          ModulesService.AddDataSourceLinksToDefaultSite(module1);
      }
      return module1;
    }

    private static void AddDataSourceLinksToDefaultSite(ModuleViewModel module)
    {
      if (!SystemManager.CurrentContext.IsOneSiteMode)
        return;
      string transactionName = nameof (AddDataSourceLinksToDefaultSite);
      MultisiteManager manager = MultisiteManager.GetManager((string) null, transactionName);
      Site defaultSite = manager.GetSites().FirstOrDefault<Site>((Expression<Func<Site, bool>>) (x => x.IsDefault));
      IList<SiteDataSourceLink> siteDataSourceLinks = defaultSite.SiteDataSourceLinks;
      IEnumerable<IDataSource> dataSources = SystemManager.DataSourceRegistry.GetDataSources();
      string empty = string.Empty;
      if (!dataSources.Any<IDataSource>())
        return;
      IEnumerable<IDataSource> source = dataSources.Where<IDataSource>((Func<IDataSource, bool>) (x => x.ModuleName == module.Name));
      if (!source.Any<IDataSource>())
        return;
      bool flag = false;
      foreach (IDataSource dataSource in source)
      {
        IDataSource moduleDataSource = dataSource;
        string defaultProvider = moduleDataSource.GetDefaultProvider();
        foreach (DataProviderInfo providerInfo in moduleDataSource.ProviderInfos)
        {
          DataProviderInfo provider = providerInfo;
          if (!siteDataSourceLinks.Any<SiteDataSourceLink>((Func<SiteDataSourceLink, bool>) (x => x.DataSource.Name == moduleDataSource.Name && x.Site.Id == defaultSite.Id && x.DataSource.Provider == provider.ProviderName)))
          {
            SiteDataSourceLink siteDataSourceLink = manager.CreateSiteDataSourceLink();
            siteDataSourceLink.DataSource = manager.GetOrCreateDataSource(moduleDataSource.Name, provider.ProviderName, provider.ProviderTitle);
            siteDataSourceLink.IsDefault = !string.IsNullOrEmpty(defaultProvider) && provider.ProviderName.Equals(defaultProvider);
            defaultSite.SiteDataSourceLinks.Add(siteDataSourceLink);
            flag = true;
          }
        }
      }
      if (!flag)
        return;
      TransactionManager.CommitTransaction(transactionName);
    }

    private static ModuleViewModel AddStaticModule(ModuleViewModel module)
    {
      AppModuleSettings appModuleSettings;
      if (Config.Get<SystemConfig>().ApplicationModules.TryGetValue(module.Name, out appModuleSettings))
      {
        MetadataManager manager = MetadataManager.GetManager();
        ModuleVersion moduleVersion = manager.GetModuleVersion(module.Name);
        if (moduleVersion != null && !string.IsNullOrEmpty(moduleVersion.ErrorMessage))
        {
          moduleVersion.ErrorMessage = string.Empty;
          manager.SaveChanges();
          if (appModuleSettings.StartupType == module.StartupType)
            return new ModuleViewModel(appModuleSettings);
        }
      }
      ConfigManager manager1 = ConfigManager.GetManager();
      SystemConfig section = manager1.GetSection<SystemConfig>();
      if (!section.ApplicationModules.TryGetValue(module.Name, out appModuleSettings))
      {
        appModuleSettings = ModulesService.GetAppModuleSettings(section, module);
        ModulesService.ValidateModule(module, section.ApplicationModules, appModuleSettings);
        section.ApplicationModules.Add(appModuleSettings);
      }
      appModuleSettings.StartupType = module.StartupType;
      manager1.SaveSection((ConfigSection) section, appModuleSettings.Source != ConfigSource.Database);
      return new ModuleViewModel(appModuleSettings);
    }

    private static void DeleteStaticModule(ModuleViewModel module)
    {
      ConfigManager manager1 = ConfigManager.GetManager();
      SystemConfig section = manager1.GetSection<SystemConfig>();
      MetadataManager manager2 = MetadataManager.GetManager();
      ModuleVersion moduleVersion = manager2.GetModuleVersion(module.Name);
      AppModuleSettings appModuleSettings;
      if (section.ApplicationModules.TryGetValue(module.Name, out appModuleSettings))
      {
        section.ApplicationModules.Remove(module.Name);
        manager1.SaveSection((ConfigSection) section, appModuleSettings.Source != ConfigSource.Database);
      }
      if (moduleVersion == null)
        return;
      manager2.DeleteModuleVersion(moduleVersion);
      manager2.SaveChanges();
    }

    private static void ValidateModule(
      ModuleViewModel module,
      ConfigElementDictionary<string, AppModuleSettings> configElementDictionary,
      AppModuleSettings moduleSettings)
    {
      Type c = !string.IsNullOrEmpty(module.Type) ? TypeResolutionService.ResolveType(module.Type, false) : throw new ArgumentException(Res.Get<Labels>("ModuleTypeCannotBeEmpty"));
      if (c == (Type) null)
        throw new ArgumentException(string.Format(Res.Get<Labels>("InstallModuleCouleNotLoadType"), (object) module.Type));
      if (!typeof (IModule).IsAssignableFrom(c))
        throw new ArgumentException(string.Format(Res.Get<Labels>("InstallModuleTypeShouldDerrive"), (object) module.Type, (object) typeof (IModule).FullName));
      if (configElementDictionary.Values.Contains<AppModuleSettings>(moduleSettings, (IEqualityComparer<AppModuleSettings>) new ModulesService.AppModuleSettingsTypeComparer()))
        throw new ArgumentException(string.Format(Res.Get<Labels>("ModuleTypeAlreadyInUse"), (object) moduleSettings.Type));
    }

    private static AppModuleSettings GetAppModuleSettings(
      SystemConfig sysConfig,
      ModuleViewModel module)
    {
      AppModuleSettings appModuleSettings = new AppModuleSettings((ConfigElement) sysConfig.ApplicationModules);
      appModuleSettings.Name = module.Name;
      appModuleSettings.Type = module.Type;
      appModuleSettings.StartupType = module.StartupType;
      appModuleSettings.Title = module.Title;
      appModuleSettings.Description = module.Description;
      appModuleSettings.ModuleId = module.ModuleId;
      appModuleSettings.Version = module.Version;
      return appModuleSettings;
    }

    private static ModuleViewModel UpdateStaticModule(
      ModuleViewModel newModuleSettings)
    {
      ConfigManager manager = ConfigManager.GetManager();
      SystemConfig section = manager.GetSection<SystemConfig>();
      if (string.IsNullOrWhiteSpace(newModuleSettings.Key))
        newModuleSettings.Key = newModuleSettings.Name;
      AppModuleSettings applicationModule = section.ApplicationModules[newModuleSettings.Key];
      if (applicationModule == null)
        throw new ArgumentException(string.Format(Res.Get<Labels>("ModuleDoesNotExist"), (object) newModuleSettings.Key));
      applicationModule.Title = newModuleSettings.Title;
      applicationModule.Description = newModuleSettings.Description;
      applicationModule.StartupType = newModuleSettings.StartupType;
      manager.SaveSection((ConfigSection) section, applicationModule.Source != ConfigSource.Database);
      return newModuleSettings;
    }

    internal static string GetProviderName(DynamicModule module) => !(module.Provider is ModuleBuilderDataProvider provider) ? string.Empty : provider.Name;

    private class AppModuleSettingsTypeComparer : IEqualityComparer<AppModuleSettings>
    {
      public bool Equals(AppModuleSettings x, AppModuleSettings y) => x.Type.Equals(y.Type);

      public int GetHashCode(AppModuleSettings obj) => obj.Type.GetHashCode();
    }
  }
}
