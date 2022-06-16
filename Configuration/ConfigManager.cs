// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.ConfigManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration.Data;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Packaging.Package;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Configuration
{
  /// <summary>
  /// Represents an intermediary between application configuration and configuration data providers.
  /// All configurations should be read and managed through this class.
  /// </summary>
  public sealed class ConfigManager : ManagerBase<ConfigProvider>, IConfigManager
  {
    private IDictionary<string, IDataProviderSettings> providerSettings;

    /// <summary>
    /// Initializes a new instance of <see cref="T:Telerik.Sitefinity.Configuration.ConfigManager" /> calss with the default provider.
    /// </summary>
    public ConfigManager()
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="T:Telerik.Sitefinity.Configuration.ConfigManager" /> calss and sets the specified provider.
    /// </summary>
    /// <param name="providerName">The name of the provider. If empty string or null the default provider is set.</param>
    public ConfigManager(string providerName)
      : base(providerName)
    {
    }

    public ConfigStorageMode StorageMode => this.Provider.StorageMode;

    /// <summary>Gets all registered configuration section classes.</summary>
    /// <returns>A string array of class names.</returns>
    public ConfigSection[] GetAllConfigSections() => this.Provider.GetAllConfigSections();

    /// <summary>Gets configuration section for the specified type.</summary>
    /// <typeparam name="TSection">The type of the configuration section.</typeparam>
    /// <returns>Configuration section.</returns>
    public TSection GetSection<TSection>() where TSection : ConfigSection, new() => this.Provider.GetSection<TSection>();

    /// <summary>Gets configuration section for the specified type.</summary>
    /// <param name="sectionName">Name of the section.</param>
    /// <returns>Configuration section.</returns>
    public ConfigSection GetSection(string sectionName) => this.Provider.GetSection(sectionName);

    /// <summary>
    /// Exports all settings that are different from the default values to external resource.
    /// </summary>
    /// <param name="section">The configuration section to be exported.</param>
    /// <param name="saveInFileSystemMode">if set to <c>true</c> exports the configuration to file system, if <c>false</c> exports the configuration according the storage mode of the system.</param>
    public void SaveSection(ConfigSection section, bool saveInFileSystemMode)
    {
      if (saveInFileSystemMode)
      {
        using (new FileSystemModeRegion())
          this.SaveSection(section);
      }
      else
        this.SaveSection(section);
    }

    /// <summary>
    /// Exports all settings that are different from the default values to external resource.
    /// </summary>
    /// <param name="section">The configuration section to be exported.</param>
    public void SaveSection(ConfigSection section)
    {
      this.Provider.SaveSection(section);
      if (SystemManager.Initializing || section.GetType().GetCustomAttributes(typeof (RestartAppOnChangeAttribute), true).Length == 0)
        return;
      OperationReason restartReason = OperationReason.FromKey("ConfigChange");
      restartReason.AddInfo(section.TagName);
      SystemManager.RestartApplication(restartReason);
    }

    /// <summary>Exports configuration section as string</summary>
    /// <typeparam name="TSection">The type of the section to be exported</typeparam>
    /// <param name="filter">Recursively applied filter for each element of the provided section.
    /// If the filter returns true the element will be exported.
    /// </param>
    /// <returns>The content of exported section as xml string</returns>
    public string Export<TSection>(Func<ConfigElement, bool> filter) where TSection : ConfigSection, new() => this.Export(typeof (TSection), filter);

    /// <summary>Exports configuration section as string</summary>
    /// <typeparam name="TSection">The type of the section to be exported</typeparam>
    /// <param name="elements">Collection of configuration elements to be exported</param>
    /// <returns>The content of exported section as xml string</returns>
    public string Export<TSection>(IEnumerable<ConfigElement> elements) where TSection : ConfigSection, new() => this.Export(typeof (TSection), elements);

    /// <summary>Exports configuration section as string</summary>
    /// <typeparam name="configType">The type of the section to be exported</typeparam>
    /// <param name="filter">Recursively applied filter for each element of the provided section.
    /// If the filter returns true the element will be exported.
    /// </param>
    /// <returns>The content of exported section as xml string</returns>
    public string Export(Type configType, Func<ConfigElement, bool> filter)
    {
      IEnumerable<ConfigElement> configElementsToBeExported = this.FilterConfigElements((ConfigElement) Config.Get(configType), filter);
      return this.Export(configType, configElementsToBeExported);
    }

    /// <summary>Exports configuration section as string</summary>
    /// <typeparam name="configType">The type of the section to be exported</typeparam>
    /// <param name="elements">Collection of configuration elements to be exported</param>
    /// <param name="skipLoadFromFile">Defines whether to skip loading the default configurations from the file system</param>
    /// <returns>The content of exported section as xml string</returns>
    public string Export(
      Type configType,
      IEnumerable<ConfigElement> configElementsToBeExported,
      bool skipLoadFromFile = false)
    {
      return this.Provider.Export(this.ExportConfigElements(configType, configElementsToBeExported), skipLoadFromFile);
    }

    /// <summary>Exports the whole section</summary>
    /// <param name="section"></param>
    /// <returns>Section`s content as xml string</returns>
    internal string Export(ConfigSection section) => this.Provider.Export(section);

    /// <summary>Exports the specified configuration elements.</summary>
    /// <param name="configElementsToBeExported">The configuration elements to be exported.</param>
    /// <param name="skipLoadFromFile">Defines whether to skip loading the default configurations from the file system</param>
    /// <param name="exportMode">The export mode.</param>
    /// <returns>Specified configuration elements as XML</returns>
    internal string Export(
      IEnumerable<ConfigElement> configElementsToBeExported,
      bool skipLoadFromFile = false,
      ExportMode exportMode = ExportMode.Default)
    {
      return this.Provider.Export(configElementsToBeExported, skipLoadFromFile, exportMode);
    }

    /// <summary>Imports xml string to specified section</summary>
    /// <param name="sectionType">Section where the xml string to be imported in</param>
    /// <param name="xml">Content as xml string</param>
    public void Import(Type sectionType, string xml, bool saveInFileSystemMode = false) => this.Provider.Import(sectionType, xml, saveInFileSystemMode);

    /// <summary>
    /// Gets a manager instance for the default data provider.
    /// </summary>
    /// <returns>The manager instance.</returns>
    public static ConfigManager GetManager() => ManagerBase<ConfigProvider>.GetManager<ConfigManager>();

    /// <summary>
    /// Gets a manger instance for the specified data provider.
    /// </summary>
    /// <param name="providerName">The name of the data provider.</param>
    /// <returns>The manager instance.</returns>
    public static ConfigManager GetManager(string providerName) => ManagerBase<ConfigProvider>.GetManager<ConfigManager>(providerName);

    /// <summary>Gets the manager.</summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="transactionName">Name of the transaction.</param>
    /// <returns></returns>
    public static ConfigManager GetManager(string providerName, string transactionName) => ManagerBase<ConfigProvider>.GetManager<ConfigManager>(providerName, transactionName);

    /// <summary>
    /// Restores the default values and saves the restored config. Do not USE for AuthenticationConfig
    /// </summary>
    /// <param name="section"></param>
    internal void RestoreSection(ConfigSection section)
    {
      SaveOptions saveOptions = new SaveOptions();
      if (Config.ConfigStorageMode == ConfigStorageMode.Auto)
        saveOptions = new SaveOptions(skipLoadFromFile: true);
      ConfigSection defaultConfig = section.GetDefaultConfig(saveOptions);
      defaultConfig.Provider = section.Provider;
      this.SaveSection(defaultConfig, Config.ConfigStorageMode == ConfigStorageMode.Auto);
    }

    /// <summary>Migrate the section to AUTO storage mode.</summary>
    /// <param name="section">The configuration section to be migrated.</param>
    internal void MigrateSection(ConfigSection section) => this.Provider.MigrateSection(section);

    /// <summary>Imports xml string to specified section</summary>
    /// <param name="sectionType">Section where the xml string to be imported in</param>
    /// <param name="xml">Content as xml string</param>
    /// <param name="origin">The origin of the XML.</param>
    /// <param name="saveInFileSystemMode">if set to <c>true</c> exports the configuration to file system, if <c>false</c> exports the configuration according the storage mode of the system.</param>
    /// <param name="overrideOrigin">if set to <c>true</c> items from the same origin will be overridden.</param>
    /// <param name="pathsOfElementsToOverride">The paths of the elements to override. If null, all elements with same origin will be overriden.</param>
    internal void Import(
      Type sectionType,
      string xml,
      string origin,
      bool saveInFileSystemMode = false,
      bool overrideOrigin = false,
      IEnumerable<string> pathsOfElementsToOverride = null)
    {
      this.Provider.Import(sectionType, xml, origin, saveInFileSystemMode, overrideOrigin, pathsOfElementsToOverride);
    }

    /// <summary>
    /// Move the element to different storage location in AUTO storage mode.
    /// </summary>
    /// <param name="section">The configuration section to be migrated.</param>
    internal void MoveElement(ConfigElement element, ConfigSource target) => this.Provider.MoveElement(element, target);

    internal bool LoadSection(ConfigSection section, LoadContext loadContext) => this.Provider.LoadSection(section, loadContext);

    /// <summary>
    /// Saves any changes made to objects retrieved with this manager.
    /// This method is not supported by <see cref="T:Telerik.Sitefinity.Configuration.ConfigManager" />. Please use SaveSection() instead.
    /// </summary>
    public override void SaveChanges() => throw new NotSupportedException("This method is not supported by ConfigManager. Please use SaveSection() instead.");

    /// <summary>
    /// Gets the name of the default provider for this manager.
    /// </summary>
    protected internal override GetDefaultProvider DefaultProviderDelegate => (GetDefaultProvider) (() => Config.SectionHandler.Settings.DefaultProvider);

    /// <summary>
    /// Gets the name of the module to which this manager belongs.
    /// </summary>
    public override string ModuleName => (string) null;

    /// <summary>Gets all provider settings.</summary>
    protected internal override ConfigElementDictionary<string, DataProviderSettings> ProvidersSettings => throw new NotSupportedException("This property is not supported by ConfigManager. Please use ProvidersSettingsInternal instead.");

    /// <summary>Gets all provider settings.</summary>
    protected internal IDictionary<string, IDataProviderSettings> ProvidersSettingsInternal
    {
      get
      {
        if (this.providerSettings == null)
        {
          this.providerSettings = (IDictionary<string, IDataProviderSettings>) new Dictionary<string, IDataProviderSettings>();
          foreach (ProviderSettings provider in (ConfigurationElementCollection) Config.SectionHandler.Settings.Providers)
          {
            DummyDataProviderSettings providerSettings = new DummyDataProviderSettings()
            {
              Name = provider.Name,
              ProviderType = TypeResolutionService.ResolveType(provider.Type, true),
              Parameters = provider.Parameters,
              Enabled = true
            };
            this.providerSettings.Add(providerSettings.Name, (IDataProviderSettings) providerSettings);
          }
        }
        return this.providerSettings;
      }
    }

    /// <summary>Gets the providers settings.</summary>
    /// <returns></returns>
    protected override IEnumerable<IDataProviderSettings> GetProvidersSettings() => (IEnumerable<IDataProviderSettings>) this.ProvidersSettingsInternal.Values;

    /// <summary>Tries the get provider settings.</summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="settings">The settings.</param>
    /// <returns></returns>
    protected override bool TryGetProviderSettings(
      string providerName,
      out IDataProviderSettings settings)
    {
      return this.ProvidersSettingsInternal.TryGetValue(providerName, out settings);
    }

    internal static IEnumerable<Type> GetTypeImplementations(Type type)
    {
      IEnumerable<Type> source = Config.Get<SystemConfig>().GetTypeImplementations(type);
      if (type.IsAbstract && source == null)
        source = type.GetAssignableTypes().Where<Type>((Func<Type, bool>) (t => t != type && !t.IsAbstract));
      List<Type> typeImplementations = source != null ? source.ToList<Type>() : new List<Type>();
      if (!type.IsAbstract)
        typeImplementations.Insert(0, type);
      return (IEnumerable<Type>) typeImplementations;
    }

    private IEnumerable<ConfigElement> FilterConfigElements(
      ConfigElement configElement,
      Func<ConfigElement, bool> filter)
    {
      foreach (ConfigProperty property in (Collection<ConfigProperty>) configElement.Properties)
      {
        object obj = configElement[property];
        if (typeof (ConfigElement).IsAssignableFrom(property.Type))
        {
          if (obj is ConfigElement configElement1 && filter(configElement1))
            yield return configElement1;
          else if (typeof (ConfigElementCollection).IsAssignableFrom(property.Type))
          {
            foreach (IConfigElementItem configElementItem in ((ConfigElementCollection) obj).Items)
            {
              if (configElementItem.Element != null)
              {
                if (filter(configElementItem.Element))
                {
                  yield return configElementItem.Element;
                }
                else
                {
                  foreach (ConfigElement filterConfigElement in this.FilterConfigElements(configElementItem.Element, filter))
                    yield return filterConfigElement;
                }
              }
            }
          }
          else
          {
            foreach (ConfigElement filterConfigElement in this.FilterConfigElements(obj as ConfigElement, filter))
              yield return filterConfigElement;
          }
        }
      }
    }

    private ConfigSection ExportConfigElements(
      Type configType,
      IEnumerable<ConfigElement> configElements)
    {
      ConfigSection instance = (ConfigSection) Activator.CreateInstance(configType);
      instance.Provider = this.Provider;
      foreach (ConfigElement configElement1 in configElements)
      {
        Stack<ConfigElement> configElementStack = new Stack<ConfigElement>();
        configElementStack.Push(configElement1);
        ConfigElement parent1;
        for (parent1 = configElement1.Parent; parent1 != null && parent1.GetKey() != instance.GetKey(); parent1 = parent1.Parent)
          configElementStack.Push(parent1);
        if (parent1 == null)
        {
          ConfigElement lastParent = configElementStack.Pop();
          ConfigElement configElement2 = (ConfigElement) ((IEnumerable<ConfigSection>) this.GetAllConfigSections()).FirstOrDefault<ConfigSection>((Func<ConfigSection, bool>) (s => s.GetKey() == lastParent.GetKey()));
          if (configElement2 != null)
          {
            ConfigElement configElement3 = (ConfigElement) null;
            ConfigElement parent2 = (ConfigElement) instance;
            while (configElementStack.Count > 0)
            {
              ConfigElement element = configElementStack.Pop();
              if (configElementStack.Count != 0 && configElement2 != null)
                configElement3 = configElement2.GetElementByKey(element.GetKey());
              ConfigElement linkParent = configElementStack.Count == 0 ? configElement3 : (ConfigElement) null;
              configElement2 = configElement3;
              parent2 = this.CreateOrInsertElement(parent2, element, configElementStack.Count == 0, linkParent);
            }
          }
        }
        else
        {
          ConfigElement parent3 = (ConfigElement) instance;
          while (configElementStack.Count > 0)
          {
            ConfigElement element = configElementStack.Pop();
            parent3 = this.CreateOrInsertElement(parent3, element, configElementStack.Count == 0);
          }
        }
      }
      return instance;
    }

    private ConfigElement CreateOrInsertElement(
      ConfigElement parent,
      ConfigElement element,
      bool replaceExisting,
      ConfigElement linkParent = null)
    {
      ConfigElement elementByKey = parent.GetElementByKey(element.GetKey());
      if (elementByKey == null)
      {
        if (parent != null)
        {
          if (parent is ConfigElementCollection)
          {
            ConfigElementCollection withParent = (ConfigElementCollection) parent;
            if (withParent.GetElementByKey(element.GetKey()) == null)
            {
              ConfigElement element1;
              if (linkParent != null)
              {
                element1 = element.Clone(linkParent, replaceExisting);
                element1.LinkModuleName = element.LinkModuleName;
              }
              else
                element1 = element.Clone((ConfigElement) withParent, replaceExisting);
              withParent.Add(element1);
              return element1;
            }
            if (!replaceExisting)
              ;
          }
          else
            parent[element.GetKey()] = (object) element;
        }
        return (ConfigElement) null;
      }
      int num = replaceExisting ? 1 : 0;
      return elementByKey;
    }
  }
}
