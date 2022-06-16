// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.ManagerExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Telerik.OpenAccess.FetchOptimization;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.DataSource;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Data
{
  /// <summary>Extension methods for managers.</summary>
  public static class ManagerExtensions
  {
    /// <summary>Gets all data provider infos.</summary>
    /// <param name="manager">The manager.</param>
    /// <param name="dataSourceName">Name of the data source. It could be used as an additional filter.</param>
    /// <returns></returns>
    public static IEnumerable<IDataProviderInfo> GetAllDataProviderInfos(
      this IManager manager,
      string dataSourceName = null)
    {
      if (!(manager is IDataSource dataSource) || !(manager is DynamicModuleManager) && !SystemManager.DataSourceRegistry.IsDataSourceRegistered(dataSource.Name) || !(SystemManager.CurrentContext.MultisiteContext is MultisiteContext multisiteContext))
        return (IEnumerable<IDataProviderInfo>) manager.StaticProviders;
      IEnumerable<IMultisiteDataProviderInfo> source = multisiteContext.GetAllDataProviders(manager.GetType().FullName, (Func<IEnumerable<DataProviderBase>>) (() => manager.StaticProviders));
      if (!string.IsNullOrEmpty(dataSourceName))
        source = source.Where<IMultisiteDataProviderInfo>((Func<IMultisiteDataProviderInfo, bool>) (p => p.DataSources.Contains<string>(dataSourceName)));
      return (IEnumerable<IDataProviderInfo>) source;
    }

    internal static IEnumerable<DataProviderBase> GetAllProviders(
      this IManager manager,
      string dataSourceName = null)
    {
      HashSet<DataProviderBase> allProviders = new HashSet<DataProviderBase>(manager.StaticProviders);
      if (manager is IDataSource dataSource && (manager is DynamicModuleManager || SystemManager.DataSourceRegistry.IsDataSourceRegistered(dataSource.Name)) && SystemManager.CurrentContext.MultisiteContext is MultisiteContext multisiteContext && manager is IVirtualProviderResolver providerResolver)
      {
        IEnumerable<IMultisiteDataProviderInfo> source = multisiteContext.GetAllDataProviders(manager.GetType().FullName, (Func<IEnumerable<DataProviderBase>>) (() => manager.StaticProviders)).Where<IMultisiteDataProviderInfo>((Func<IMultisiteDataProviderInfo, bool>) (p => p.IsVirtual));
        if (!string.IsNullOrEmpty(dataSourceName))
          source = source.Where<IMultisiteDataProviderInfo>((Func<IMultisiteDataProviderInfo, bool>) (p => p.DataSources.Contains<string>(dataSourceName)));
        foreach (IMultisiteDataProviderInfo dataProviderInfo in source)
        {
          try
          {
            allProviders.Add(providerResolver.ResolveVirtualProvider(dataProviderInfo.Name));
          }
          catch
          {
          }
        }
      }
      return (IEnumerable<DataProviderBase>) allProviders;
    }

    internal static IEnumerable<string> GetProviderNames(
      this IManager manager,
      ProviderBindingOptions bindingOptions = ProviderBindingOptions.NoFilter,
      params Guid[] siteFilter)
    {
      IEnumerable<IDataProviderInfo> source = manager.GetAllDataProviderInfos();
      if ((bindingOptions & ProviderBindingOptions.SkipSystem) > ProviderBindingOptions.NoFilter)
        source = source.Where<IDataProviderInfo>((Func<IDataProviderInfo, bool>) (p => !p.IsSystem));
      if (siteFilter != null && siteFilter.Length != 0)
        source = source.Where<IDataProviderInfo>((Func<IDataProviderInfo, bool>) (p => !(p is IMultisiteDataProviderInfo) || ((IMultisiteDataProviderInfo) p).Sites.Any<Guid>((Func<Guid, bool>) (s => ((IEnumerable<Guid>) siteFilter).Contains<Guid>(s)))));
      return source.Select<IDataProviderInfo, string>((Func<IDataProviderInfo, string>) (p => p.Name));
    }

    /// <summary>Registers the provider for the given manager.</summary>
    /// <typeparam name="TProviderBase">The type of the T provider base.</typeparam>
    /// <param name="dataSource">The data source.</param>
    /// <param name="manager">The manager.</param>
    /// <param name="configType">Type of the config.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="providerTitle">The provider title.</param>
    /// <param name="parameters">The parameters.</param>
    /// <param name="applyProviderDefaultSettings">The apply provider default settings.</param>
    internal static void RegisterProvider<TProviderBase>(
      this ManagerBase<TProviderBase> manager,
      Type configType,
      string providerName,
      string providerTitle,
      NameValueCollection parameters = null,
      Action<IModuleConfig, DataProviderSettings> applyProviderDefaultSettings = null)
      where TProviderBase : DataProviderBase
    {
      DataProviderSettings configProvider = ManagerExtensions.RegisterProviderSettings(configType, providerName, providerTitle, parameters, applyProviderDefaultSettings);
      manager.InstatiateProvider((IDataProviderSettings) configProvider);
    }

    /// <summary>
    /// Returns the name of the provider associated with a specific secured object.
    /// </summary>
    /// <param name="objectManagerInstance">Instance of the manager associated with the secured object.</param>
    /// <param name="objectType">Type of the secured object.</param>
    /// <param name="ObjectId">Id of the secured object.</param>
    /// <returns>The name of the provider associated with a specific secured object.</returns>
    internal static string ResolveSecuredObjectProviderName(
      this IManager objectManagerInstance,
      Type objectType,
      Guid ObjectId)
    {
      string str = string.Empty;
      foreach (string providerName in objectManagerInstance.GetProviderNames(ProviderBindingOptions.NoFilter))
      {
        if (ManagerBase.GetMappedManager(objectType, providerName).GetItems(objectType, "Id=" + ObjectId.ToString(), "", -1, -1).AsQueryable().Count() > 0)
        {
          str = providerName;
          break;
        }
      }
      return str;
    }

    /// <summary>Gets the valid providers for the given site</summary>
    /// <returns></returns>
    internal static IEnumerable<DataProviderBase> GetSiteProviders(
      this IManager manager,
      Guid siteId = default (Guid),
      string dataSourceName = null)
    {
      if (!(manager is IDataSource dataSource) || !SystemManager.DataSourceRegistry.IsDataSourceRegistered(dataSourceName ?? dataSource.Name) || !(SystemManager.CurrentContext.MultisiteContext is MultisiteContext multisiteContext) || !(manager is IVirtualProviderResolver providerResolver))
        return manager.StaticProviders;
      if (siteId.IsEmpty())
        siteId = multisiteContext.CurrentSite.Id;
      IEnumerable<ISiteDataSource> siteDataSources = multisiteContext.GetDataSourcesByName(dataSourceName ?? dataSource.Name).Where<ISiteDataSource>((Func<ISiteDataSource, bool>) (ds => ds.Sites.Contains<Guid>(siteId)));
      HashSet<DataProviderBase> siteProviders = new HashSet<DataProviderBase>();
      foreach (ISiteDataSource siteDataSource in siteDataSources)
      {
        try
        {
          siteProviders.Add(providerResolver.ResolveVirtualProvider(siteDataSource.Provider, siteDataSource.Name));
        }
        catch
        {
        }
      }
      return (IEnumerable<DataProviderBase>) siteProviders;
    }

    /// <summary>Checks if a Guid value is Guid.Empty.</summary>
    /// <param name="value">The Guid value.</param>
    /// <returns>If the value is Guid.Empty.</returns>
    internal static bool IsEmpty(this Guid value) => value == Guid.Empty;

    /// <summary>
    /// Executes the action and commits the transaction of the manager. Retries if an exception is thrown.
    /// </summary>
    /// <param name="manager">The manager.</param>
    /// <param name="action">The action to be executed.</param>
    /// <param name="retryCount">The retry count.</param>
    /// <exception cref="T:System.InvalidOperationException">Cannot retry operation without a transaction.</exception>
    internal static void ExecuteAndCommitWithRetries(
      this IManager manager,
      Action action,
      int retryCount)
    {
      if (manager.TransactionName.IsNullOrEmpty())
        throw new InvalidOperationException("Cannot retry operation without a transaction.");
      int num = retryCount - 1;
      while (num > 0)
      {
        try
        {
          action();
          TransactionManager.CommitTransaction(manager.TransactionName);
          break;
        }
        catch (Exception ex)
        {
          TransactionManager.RollbackTransaction(manager.TransactionName);
          --num;
          if (num == 0)
            Log.Write((object) string.Format("Error while trying to commit transaction {0}: Message: {1}, StackTrace: {2}", (object) manager.TransactionName, (object) ex.Message, (object) ex.StackTrace), ConfigurationPolicy.ErrorLog);
        }
      }
    }

    internal static Guid GenerateGuid(this string key)
    {
      using (MD5 md5 = MD5.Create())
        return new Guid(md5.ComputeHash(Encoding.Default.GetBytes(key)));
    }

    /// <summary>Gets information for all static providers.</summary>
    /// <typeparam name="TProvider">The type of the provider.</typeparam>
    /// <param name="manager">The manager.</param>
    /// <returns></returns>
    internal static IEnumerable<DataProviderInfo> GetProviderInfos<TProvider>(
      this ManagerBase<TProvider> manager)
      where TProvider : DataProviderBase
    {
      return manager.GetProviderInfos<TProvider>((IEnumerable<TProvider>) manager.StaticProviders);
    }

    internal static IEnumerable<DataProviderInfo> GetProviderInfos<TProvider>(
      this ManagerBase<TProvider> manager,
      IEnumerable<TProvider> providers)
      where TProvider : DataProviderBase
    {
      List<DataProviderInfo> providerInfos = new List<DataProviderInfo>();
      foreach (TProvider provider in providers)
      {
        DataProviderBase dataProviderBase = (DataProviderBase) provider;
        NameValueCollection providerParameters = (NameValueCollection) null;
        DataProviderSettings providerSettings;
        if (manager.ProvidersSettings.TryGetValue(dataProviderBase.Name, out providerSettings))
          providerParameters = providerSettings.Parameters;
        providerInfos.Add(new DataProviderInfo(dataProviderBase.Name, dataProviderBase.Title, dataProviderBase.GetType(), providerParameters));
      }
      return (IEnumerable<DataProviderInfo>) providerInfos;
    }

    internal static IEnumerable<DataProviderInfo> GetAllProviderInfos<TProvider, TConfig>(
      this ManagerBase<TProvider> manager)
      where TProvider : DataProviderBase
      where TConfig : ConfigSection, IModuleConfig, new()
    {
      return ManagerExtensions.GetAllProviderInfos((IModuleConfig) Config.Get<TConfig>());
    }

    internal static IEnumerable<DataProviderInfo> GetAllProviderInfos<TProvider>(
      this ManagerBase<TProvider> manager,
      string sectionName)
      where TProvider : DataProviderBase
    {
      return ManagerExtensions.GetAllProviderInfos((IModuleConfig) Config.Get<ConfigSection>(sectionName));
    }

    internal static string GenerateProviderName(
      string baseProviderName,
      IEnumerable<string> existringProviderNames)
    {
      string providerName = Regex.Replace(baseProviderName.ToLowerInvariant(), "[^[a-z0-9]+", "");
      if (string.IsNullOrWhiteSpace(providerName))
        providerName = DateTime.UtcNow.Ticks.ToString();
      if (existringProviderNames.Contains<string>(providerName))
      {
        baseProviderName = providerName;
        int num = 2;
        while (true)
        {
          providerName = baseProviderName + (object) num;
          if (existringProviderNames.Contains<string>(providerName))
            ++num;
          else
            break;
        }
      }
      return providerName;
    }

    internal static string GenerateProviderName(IDataSource dataSource, string prefix = null)
    {
      List<string> list = dataSource.Providers.Select<DataProviderInfo, string>((Func<DataProviderInfo, string>) (p => p.ProviderName)).ToList<string>();
      if (SystemManager.CurrentContext is MultisiteContext currentContext)
      {
        string managerType = dataSource.Name.Contains<char>('.') ? dataSource.Name : typeof (DynamicModuleManager).FullName;
        list.AddRange(currentContext.GetDataSourcesByManager(managerType).Select<ISiteDataSource, string>((Func<ISiteDataSource, string>) (ds => ds.Provider)));
      }
      return ManagerExtensions.GenerateProviderName(prefix ?? dataSource.ProviderNameDefaultPrefix, (IEnumerable<string>) list);
    }

    internal static FetchStrategy GetFetchStrategyFromCurrent(this IManager manager)
    {
      if (!(manager.Provider is IOpenAccessDataProvider provider))
        return (FetchStrategy) null;
      FetchStrategy fetchStrategy = provider.GetContext().FetchStrategy;
      return fetchStrategy != null ? FetchStrategyResolver.Clone(fetchStrategy) : new FetchStrategy();
    }

    internal static FetchStrategy GetFetchStrategyForItem(
      this IManager manager,
      Type itemType,
      string[] fetchProperties,
      bool includeAutoProperties)
    {
      bool flag1 = itemType.IsILifecycle();
      bool flag2 = typeof (ISecuredObject).IsAssignableFrom(itemType);
      FetchStrategy strategyFromCurrent = manager.GetFetchStrategyFromCurrent();
      if (strategyFromCurrent != null)
      {
        if (includeAutoProperties)
        {
          if (flag2 && !ClaimsManager.GetCurrentIdentity().IsUnrestricted)
            strategyFromCurrent.LoadWith(itemType.FullName, "Permissions");
          if (flag1)
          {
            strategyFromCurrent.LoadWith(itemType.FullName, "LanguageData");
            strategyFromCurrent.LoadWith(itemType.FullName, "PublishedTranslations");
          }
        }
        if (fetchProperties != null)
        {
          foreach (string fetchProperty in fetchProperties)
            strategyFromCurrent.LoadWith(itemType.FullName, fetchProperty);
        }
      }
      return strategyFromCurrent;
    }

    private static DataProviderSettings RegisterProviderSettings(
      Type configType,
      string providerName,
      string providerTitle,
      NameValueCollection parameters = null,
      Action<IModuleConfig, DataProviderSettings> applyProviderDefaultSettings = null)
    {
      ConfigManager manager = ConfigManager.GetManager();
      IModuleConfig section = manager.GetSection(configType.Name) as IModuleConfig;
      DataProviderSettings element = !section.Providers.ContainsKey(providerName) ? new DataProviderSettings() : section.Providers[providerName];
      DataProviderSettings provider = section.Providers[section.DefaultProvider];
      element.Parameters = new NameValueCollection(provider.Parameters);
      element.ProviderType = provider.ProviderType;
      element.Parameters["applicationName"] = providerName;
      element.Name = providerName;
      element.Title = providerTitle;
      if (applyProviderDefaultSettings != null)
        applyProviderDefaultSettings(section, element);
      if (parameters != null)
      {
        foreach (object key in parameters.Keys)
          element.Parameters[key.ToString()] = parameters[key.ToString()];
      }
      section.Providers.Add(element);
      manager.SaveSection((ConfigSection) section);
      return element;
    }

    private static IEnumerable<DataProviderInfo> GetAllProviderInfos(
      IModuleConfig configSection)
    {
      List<DataProviderInfo> allProviderInfos = new List<DataProviderInfo>();
      foreach (string key in (IEnumerable<string>) configSection.Providers.Keys)
      {
        DataProviderSettings provider = configSection.Providers[key];
        allProviderInfos.Add(new DataProviderInfo(provider.Name, provider.Title, provider.GetType(), provider.Parameters));
      }
      return (IEnumerable<DataProviderInfo>) allProviderInfos;
    }
  }
}
