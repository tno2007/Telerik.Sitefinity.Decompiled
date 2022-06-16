// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Config
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Web.Hosting;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.SiteSettings.Configuration;
using Telerik.Sitefinity.Utilities;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Configuration
{
  /// <summary>Helper class for reading configurations.</summary>
  public static class Config
  {
    /// <summary>
    /// No need for thread safety here as these are always evaluated to the same value for the entire life of theappdomain
    /// </summary>
    private static IDictionary<string, bool> sectionsWithConfiguredSiteSpecificPropsCache = SystemManager.CreateStaticCache<string, bool>();
    private static readonly ConcurrentProperty<SectionHandler> sectionHandler = new ConcurrentProperty<SectionHandler>(new Func<SectionHandler>(Config.BuildSectionHandler));
    private static readonly ConcurrentProperty<ConfigProvider> defaultProvider = new ConcurrentProperty<ConfigProvider>((Func<ConfigProvider>) (() => Config.GetManager().Provider));
    private static IList<ISecretDataResolver> secretResolvers;
    private const string FileSystemModeKey = "sfConfFileSystemMode";
    private const string ConfigSiteContextKey = "sfConfigSiteContextKey";
    private const string SafeModeKey = "sfConfSafeMode";
    private static readonly ConcurrentProperty<LocalDataStoreSlot> fileSystemModeSlot = new ConcurrentProperty<LocalDataStoreSlot>(new Func<LocalDataStoreSlot>(Thread.AllocateDataSlot));
    private static readonly ConcurrentProperty<LocalDataStoreSlot> configSiteContext = new ConcurrentProperty<LocalDataStoreSlot>(new Func<LocalDataStoreSlot>(Thread.AllocateDataSlot));
    private static readonly ConcurrentProperty<LocalDataStoreSlot> safeModeSlot = new ConcurrentProperty<LocalDataStoreSlot>(new Func<LocalDataStoreSlot>(Thread.AllocateDataSlot));
    private static readonly object statLock = new object();
    private static readonly ConcurrentProperty<RestrictionLevel> restrictionLevel = new ConcurrentProperty<RestrictionLevel>(new Func<RestrictionLevel>(Config.GetRestrictionLevel));
    private static readonly ConcurrentProperty<ConfigStorageMode> configStorageMode = new ConcurrentProperty<ConfigStorageMode>(new Func<ConfigStorageMode>(Config.GetConfigStorageMode));
    private static bool suppressRestriction = false;
    private static IDictionary<string, WeakReference> containerControlledLifetimeManagers = (IDictionary<string, WeakReference>) new Dictionary<string, WeakReference>();

    /// <summary>
    /// Gets or sets a value indicating whether [file system mode].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [file system mode]; otherwise, <c>false</c>.
    /// </value>
    internal static bool FileSystemMode
    {
      get
      {
        object obj = !HostingEnvironment.IsHosted || SystemManager.CurrentHttpContext == null ? Thread.GetData(Config.FileSystemModeSlot) : SystemManager.CurrentHttpContext.Items[(object) "sfConfFileSystemMode"];
        return obj != null && (bool) obj;
      }
      set
      {
        if (HostingEnvironment.IsHosted && SystemManager.CurrentHttpContext != null)
          SystemManager.CurrentHttpContext.Items[(object) "sfConfFileSystemMode"] = (object) value;
        else
          Thread.SetData(Config.FileSystemModeSlot, (object) value);
      }
    }

    /// <summary>
    /// Gets or sets a value containing the site for which any config persistence operations are performed.
    /// </summary>
    internal static ConfigSiteContext SiteContext
    {
      get => (!HostingEnvironment.IsHosted || SystemManager.CurrentHttpContext == null ? (ConfigSiteContext) Thread.GetData(Config.ConfigSiteContextSlot) : (ConfigSiteContext) SystemManager.CurrentHttpContext.Items[(object) "sfConfigSiteContextKey"]) ?? (ConfigSiteContext) null;
      set
      {
        if (HostingEnvironment.IsHosted && SystemManager.CurrentHttpContext != null)
          SystemManager.CurrentHttpContext.Items[(object) "sfConfigSiteContextKey"] = (object) value;
        else
          Thread.SetData(Config.ConfigSiteContextSlot, (object) value);
      }
    }

    internal static bool HasDefaultDatabaseConnection => Config.Get<DataConfig>(true).IsInitalized();

    /// <summary>Gets the file system mode slot.</summary>
    /// <value>The file system mode slot.</value>
    private static LocalDataStoreSlot FileSystemModeSlot => Config.fileSystemModeSlot.Value;

    /// <summary>
    /// Gets the config site context mode slot. It is used to store the <see cref="P:Telerik.Sitefinity.Configuration.Config.SiteContext" />
    /// </summary>
    /// <value>The file system mode slot.</value>
    private static LocalDataStoreSlot ConfigSiteContextSlot => Config.configSiteContext.Value;

    /// <summary>Gets the configuration restriction level.</summary>
    /// <value>The configuration restriction level.</value>
    internal static RestrictionLevel RestrictionLevel => Config.restrictionLevel.Value;

    /// <summary>Gets the configuration storage mode.</summary>
    /// <value>The configuration storage mode.</value>
    internal static ConfigStorageMode ConfigStorageMode => Config.configStorageMode.Value;

    /// <summary>
    /// Gets or sets a value indicating whether [suppress restriction].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [suppress restriction]; otherwise, <c>false</c>.
    /// </value>
    internal static bool SuppressRestriction
    {
      get => Config.suppressRestriction;
      set => Config.suppressRestriction = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the configuration should behave in safe mode in order to avoid stack overflow exception.
    /// Used in database persistence mode
    /// </summary>
    /// <value><c>true</c> if [safe mode]; otherwise, <c>false</c>.</value>
    internal static bool SafeMode
    {
      get
      {
        object safeMode = !HostingEnvironment.IsHosted || SystemManager.CurrentHttpContext == null ? Thread.GetData(Config.SafeModeSlot) : SystemManager.CurrentHttpContext.Items[(object) "sfConfSafeMode"];
        if (safeMode != null)
          return (bool) safeMode;
        return !Bootstrapper.IsDataInitialized;
      }
      set
      {
        if (HostingEnvironment.IsHosted && SystemManager.CurrentHttpContext != null)
          SystemManager.CurrentHttpContext.Items[(object) "sfConfSafeMode"] = (object) value;
        else
          Thread.SetData(Config.SafeModeSlot, (object) value);
      }
    }

    private static LocalDataStoreSlot SafeModeSlot => Config.safeModeSlot.Value;

    /// <summary>Gets a list of configured secret data resolvers.</summary>
    internal static IList<ISecretDataResolver> SecretResolvers
    {
      get
      {
        IList<ISecretDataResolver> list = Config.secretResolvers;
        if (list == null)
        {
          lock (Config.statLock)
          {
            list = Config.secretResolvers;
            if (list == null)
            {
              ProviderSettingsCollection secretResolvers = Config.SectionHandler.Settings.SecretResolvers;
              list = (IList<ISecretDataResolver>) new List<ISecretDataResolver>(secretResolvers.Count);
              foreach (ProviderSettings providerSettings in (ConfigurationElementCollection) secretResolvers)
              {
                ISecretDataResolver instance = (ISecretDataResolver) Activator.CreateInstance(TypeResolutionService.ResolveType(providerSettings.Type));
                instance.Initialize(providerSettings.Name, providerSettings.Parameters);
                list.Add(instance);
              }
              list = (IList<ISecretDataResolver>) new ReadOnlyCollection<ISecretDataResolver>(list);
              Config.secretResolvers = list;
            }
          }
        }
        return list;
      }
    }

    internal static ISecretDataResolver GetDefaultSecretResolver() => Config.GetSecretResolver(Config.SectionHandler.Settings.DefaultSecretResolver);

    internal static ISecretDataResolver GetSecretResolver(
      string secretResolverName)
    {
      if (string.IsNullOrEmpty(secretResolverName))
        return (ISecretDataResolver) null;
      ISecretDataResolver resolver = (ISecretDataResolver) null;
      if (!Config.TryGetSecretResolver(secretResolverName, out resolver))
        throw new ArgumentException("Invalid secret resolver name '{0}'.".Arrange((object) secretResolverName), nameof (secretResolverName));
      return resolver;
    }

    internal static bool TryGetSecretResolver(
      string secretResolverName,
      out ISecretDataResolver resolver)
    {
      resolver = (ISecretDataResolver) null;
      foreach (ISecretDataResolver secretResolver in (IEnumerable<ISecretDataResolver>) Config.SecretResolvers)
      {
        if (secretResolver.Name.Equals(secretResolverName, StringComparison.OrdinalIgnoreCase))
        {
          resolver = secretResolver;
          break;
        }
      }
      return resolver != null;
    }

    /// <summary>
    /// Static property that returns Section Handler for telerik/framework web.config
    /// section.
    /// </summary>
    public static SectionHandler SectionHandler => Config.sectionHandler.Value;

    internal static string ResolveSecretValue(string resolverName, string key) => Config.GetSecretResolver(resolverName).Resolve(key);

    internal static string GenerateSecretKey(string resolverName, string value) => Config.GetSecretResolver(resolverName).GenerateKey(value);

    private static SectionHandler BuildSectionHandler()
    {
      SectionHandler sectionHandler = (SectionHandler) ConfigurationManager.GetSection("telerik/sitefinity");
      if (sectionHandler == null)
      {
        sectionHandler = new SectionHandler();
        sectionHandler.LoadDefaults();
      }
      return sectionHandler;
    }

    /// <summary>Gets the restriction level.</summary>
    /// <returns></returns>
    private static RestrictionLevel GetRestrictionLevel()
    {
      RestrictionLevel restrictionLevel = RestrictionLevel.Default;
      if (Config.SectionHandler.Settings.StorageMode == ConfigStorageMode.Auto)
        restrictionLevel = Config.SectionHandler.Settings.RestrictionLevel;
      return restrictionLevel;
    }

    /// <summary>Gets the config storage mode.</summary>
    /// <returns></returns>
    private static ConfigStorageMode GetConfigStorageMode()
    {
      int storageMode = (int) Config.SectionHandler.Settings.StorageMode;
      return Config.SectionHandler.Settings.StorageMode;
    }

    /// <summary>Gets the default configuration data provider.</summary>
    public static ConfigProvider DefaultProvider => Config.defaultProvider.Value;

    /// <summary>Gets a manger instance for the default data provider.</summary>
    /// <returns>The manager instance.</returns>
    public static ConfigManager GetManager() => ManagerBase<ConfigProvider>.GetManager<ConfigManager>();

    /// <summary>
    /// Gets a manger instance for the specified data provider.
    /// </summary>
    /// <param name="providerName">The name of the data provider.</param>
    /// <returns>The manager instance.</returns>
    public static ConfigManager GetManager(string providerName) => ManagerBase<ConfigProvider>.GetManager<ConfigManager>(providerName);

    /// <summary>
    /// Gets configuration section for the specified type using the default provider.
    /// </summary>
    /// <typeparam name="TSection">The type of the configuration section.</typeparam>
    /// <returns>Configuration section.</returns>
    public static TSection Get<TSection>() where TSection : ConfigSection, new() => Config.Get<TSection>(false);

    /// <summary>
    /// Gets configuration section for the specified type using the default provider.
    /// </summary>
    /// <typeparam name="TSection">The type of the section.</typeparam>
    /// <param name="safeMode">if set to <c>true</c> returns the configuration in safe mode.</param>
    /// <returns></returns>
    internal static TSection Get<TSection>(bool safeMode) where TSection : ConfigSection, new() => Config.GetSectionPrivate<TSection>(safeMode);

    /// <summary>
    /// Gets configuration section for the specified type using the default provider.
    /// </summary>
    /// <typeparam name="TSection">The type of the configuration section.</typeparam>
    /// <returns>Configuration section.</returns>
    public static ConfigSection Get(Type sectionType) => typeof (ConfigSection).IsAssignableFrom(sectionType) ? Config.GetSectionPrivate(sectionType, false, sectionType.Name) : throw new ArgumentException("Invalid type", nameof (sectionType));

    /// <summary>
    /// Gets configuration section for the specified type and section name.
    /// </summary>
    /// <remarks>
    /// This method is used so that a base configuration section can be resolved by the
    /// name of the inheriting section.
    /// </remarks>
    /// <typeparam name="TSection">The type of the configuration section.</typeparam>
    /// <param name="sectionName">
    /// The name that will be used when resolving from the Unity container.
    /// </param>
    /// <returns>Configuration section.</returns>
    public static TSection Get<TSection>(string sectionName) where TSection : ConfigSection => (TSection) Config.GetConfigSection(sectionName);

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.Configuration.ConfigElement" /> by its path.
    /// </summary>
    /// <typeparam name="TConfigElement">The type of the config element.</typeparam>
    /// <param name="path">The path.</param>
    /// <returns></returns>
    public static TConfigElement GetByPath<TConfigElement>(string path) where TConfigElement : ConfigElement
    {
      ConfigSection startingElement = !string.IsNullOrEmpty(path) ? Config.GetConfigSection(Config.ProcessFirstPathSection(ref path)) : throw new ArgumentException("Invalid path", nameof (path));
      return Config.GetByPath<TConfigElement>(path, (ConfigElement) startingElement);
    }

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.Configuration.ConfigElement" /> by its path from a staringElement.
    /// </summary>
    /// <typeparam name="TConfigElement">The type of the config element to return.</typeparam>
    /// <param name="path">The path.</param>
    /// <param name="startingElement">The starting element.</param>
    /// <returns></returns>
    public static TConfigElement GetByPath<TConfigElement>(
      string path,
      ConfigElement startingElement)
      where TConfigElement : ConfigElement
    {
      if (startingElement == null)
        throw new ArgumentNullException(nameof (startingElement));
      if (string.IsNullOrEmpty(path))
        return startingElement is TConfigElement configElement ? configElement : throw new Exception("The config element: \"{0}\" specified by the path: \"{2}\" is not of type \"{3}\".".Arrange((object) startingElement.GetType().Name, (object) path, (object) typeof (TConfigElement).Name));
      string key = Config.ProcessFirstPathSection(ref path);
      ConfigElement elementByKey = startingElement.GetElementByKey(key);
      return elementByKey != null ? Config.GetByPath<TConfigElement>(path, elementByKey) : default (TConfigElement);
    }

    /// <summary>
    /// Registers a configuration section with the type system.
    /// </summary>
    /// <typeparam name="TSection">The type of the configuration section class to register</typeparam>
    public static void RegisterSection<TSection>() where TSection : ConfigSection, new() => Config.RegisterSectionInternal(typeof (TSection));

    /// <summary>
    /// Registers a configuration section with the type system.
    /// </summary>
    /// <param name="sectionType">The CLR type of the section.</param>
    public static void RegisterSection(Type sectionType)
    {
      if (!typeof (ConfigSection).IsAssignableFrom(sectionType))
        throw new ArgumentException("Invalid type", nameof (sectionType));
      Config.RegisterSectionInternal(sectionType);
    }

    private static void RegisterSectionInternal(Type sectionType)
    {
      string name = sectionType.Name;
      Config.RegisterSectionInternal(sectionType, name);
    }

    private static void RegisterSectionInternal(Type sectionType, string name)
    {
      lock (Config.containerControlledLifetimeManagers)
      {
        foreach (string key in Config.containerControlledLifetimeManagers.Keys.Where<string>((Func<string, bool>) (x => x.StartsWith(name))))
        {
          WeakReference weakReference;
          if (Config.containerControlledLifetimeManagers.TryGetValue(key, out weakReference) && weakReference.IsAlive && weakReference.Target is ContainerControlledLifetimeManager target)
            target.RemoveValue();
        }
        ContainerControlledLifetimeManager target1 = new ContainerControlledLifetimeManager();
        ObjectFactory.Container.RegisterType(sectionType, name, (LifetimeManager) target1, (InjectionMember) new InjectionConstructor(Array.Empty<object>()));
        Config.containerControlledLifetimeManagers[name] = new WeakReference((object) target1);
      }
    }

    internal static void Clean() => Config.containerControlledLifetimeManagers.Clear();

    /// <summary>
    /// Invokes the <paramref name="action" /> delegate with the <typeparamref name="TSection" /> configuration section
    /// and saves the section afterwards.
    /// </summary>
    /// <typeparam name="TSection">The type of the configuration section being updated.</typeparam>
    /// <param name="update">A delegate that receives the section and modifies it.</param>
    public static void UpdateSection<TSection>(Action<TSection> update) where TSection : ConfigSection, new()
    {
      ConfigManager manager = ConfigManager.GetManager();
      TSection section = manager.GetSection<TSection>();
      update(section);
      manager.SaveSection((ConfigSection) section);
    }

    /// <summary>
    /// Invokes, suppressing security checks, the <paramref name="action" /> delegate with the <typeparamref name="TSection" /> configuration section
    /// and saves the section afterwards.
    /// </summary>
    /// <typeparam name="TSection">The type of the configuration section being updated.</typeparam>
    /// <param name="update">A delegate that receives the section and modifies it.</param>
    internal static void ElevatedUpdateSection<TSection>(Action<TSection> update) where TSection : ConfigSection, new()
    {
      bool disableSecurityChecks = ConfigProvider.DisableSecurityChecks;
      ConfigProvider.DisableSecurityChecks = true;
      try
      {
        Config.UpdateSection<TSection>(update);
      }
      finally
      {
        ConfigProvider.DisableSecurityChecks = disableSecurityChecks;
      }
    }

    internal static bool IsSectionAllowedSiteSpecific(string sectionTypeName) => !(sectionTypeName == typeof (SiteSettingsConfig).Name) && !(sectionTypeName == "SitemapConfig");

    private static TSection GetSectionPrivate<TSection>(bool safeMode) where TSection : ConfigSection, new() => (TSection) Config.GetSectionPrivate(typeof (TSection), safeMode, typeof (TSection).Name);

    private static ConfigSection GetSectionPrivate(
      Type sectionType,
      bool safeMode,
      string sectionName)
    {
      if (!safeMode && SystemManager.Initializing && SystemManager.HttpContextItems != null)
      {
        InstallContext httpContextItem = SystemManager.HttpContextItems[(object) "sf_InstallContext"] as InstallContext;
        ConfigSection section = (ConfigSection) null;
        if (httpContextItem != null && httpContextItem.TryGetConfig(sectionType, out section))
          return section;
      }
      return Config.GetSectionInternal(sectionType, safeMode, sectionName);
    }

    private static ConfigSection GetSectionInternal(
      Type sectionType,
      bool safeMode,
      string sectionName)
    {
      Config.VerifySectionRegistered(sectionType, sectionName);
      Guid siteId = Guid.Empty;
      if (!safeMode && Bootstrapper.IsReady && Config.IsSectionAllowedSiteSpecific(sectionName) && Config.AreSiteSpecificPathsConfiguredForSection(sectionName))
      {
        if (Config.SiteContext != null)
        {
          siteId = Config.SiteContext.SiteId;
        }
        else
        {
          using (new ConfigSiteContextRegion(Guid.Empty))
            siteId = SystemManager.CurrentContext.CurrentSite.Id;
        }
        if (siteId != Guid.Empty)
        {
          sectionName = Config.AppendSiteSpecificRegistrationSuffix(sectionName, siteId);
          if (ObjectFactory.GetArgsByName(sectionName, sectionType) == null)
            Config.RegisterSectionInternal(sectionType, sectionName);
        }
      }
      using (new ConfigSiteContextRegion(siteId))
      {
        ConfigSection section = (ConfigSection) ObjectFactory.Resolve(sectionType, sectionName);
        if (!section.Initialized)
        {
          ConfigProvider defaultProvider = Config.DefaultProvider;
          if (safeMode && defaultProvider.StorageMode != ConfigStorageMode.FileSystem)
            section.SafeMode = new bool?(true);
          section.Initialize(defaultProvider);
        }
        else if (!safeMode && section.SafeMode.HasValue && section.SafeMode.Value)
        {
          bool? safeMode1 = section.SafeMode;
          Config.DefaultProvider.EnsureNormalMode(section);
          bool? nullable = safeMode1;
          bool? safeMode2 = section.SafeMode;
          if (!(nullable.GetValueOrDefault() == safeMode2.GetValueOrDefault() & nullable.HasValue == safeMode2.HasValue))
            section.OnSectionChanged();
        }
        return section;
      }
    }

    private static bool AreSiteSpecificPathsConfiguredForSection(string sectionName)
    {
      if (!Config.sectionsWithConfiguredSiteSpecificPropsCache.ContainsKey(sectionName))
        Config.sectionsWithConfiguredSiteSpecificPropsCache[sectionName] = Config.Get<SiteSettingsConfig>().SiteSpecificProperties.Values.Any<PropertyPath>((Func<PropertyPath, bool>) (x => x.Path.ToLower().StartsWith(sectionName.ToLower())));
      return Config.sectionsWithConfiguredSiteSpecificPropsCache[sectionName];
    }

    private static string AppendSiteSpecificRegistrationSuffix(string sectionName, Guid siteId) => sectionName + "_" + siteId.ToString();

    private static void VerifySectionRegistered(Type sectionType, string sectionName)
    {
      if (string.IsNullOrEmpty(sectionName))
        sectionName = sectionType.Name;
      if (ObjectFactory.GetArgsByName(sectionName, sectionType) == null)
        throw new Exception("The configuration '{0}' is not registered".Arrange((object) sectionType.Name));
    }

    internal static ConfigSection GetConfigSection(string name)
    {
      RegisterEventArgs argsByName = ObjectFactory.GetArgsByName(name, typeof (ConfigSection));
      if (argsByName != null)
        return Config.GetSectionPrivate(argsByName.TypeTo, false, argsByName.TypeTo.Name);
      throw new ArgumentException("There is no configuration section with the name:\"{0}\"".Arrange((object) name));
    }

    private static string ProcessFirstPathSection(ref string path)
    {
      if (string.IsNullOrEmpty(path))
        throw new ArgumentNullException("Cannot process null or empty path", nameof (path));
      char[] separator = new char[1]{ '/' };
      string[] strArray = path.Trim(separator).Split(separator, 2);
      string str = strArray[0];
      path = strArray.Length <= 1 ? string.Empty : strArray[1];
      return !string.IsNullOrEmpty(str) ? str : throw new ArgumentException("Invalid path section: {0}".Arrange((object) path), nameof (path));
    }

    internal static bool IsDatabaseMode(ConfigStorageMode storageMode) => storageMode != ConfigStorageMode.Auto ? storageMode == ConfigStorageMode.Database : !Config.FileSystemMode;
  }
}
