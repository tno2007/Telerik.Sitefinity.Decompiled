// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Data.XmlConfigProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Web.Hosting;
using System.Xml;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration.Environment;
using Telerik.Sitefinity.Configuration.Events;
using Telerik.Sitefinity.Configuration.Model;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Events;
using Telerik.Sitefinity.ModuleEditor.Web.Services.Model;
using Telerik.Sitefinity.Packaging;
using Telerik.Sitefinity.Packaging.Package;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Events;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI.Fields.Config;

namespace Telerik.Sitefinity.Configuration.Data
{
  /// <summary>
  /// Represents configuration data provider that uses XML file to store and retrieve settings.
  /// </summary>
  public class XmlConfigProvider : ConfigProvider
  {
    /// <summary>The file extension.</summary>
    private string fileExtension;
    /// <summary>The storage folder.</summary>
    private string storageFolder;
    private ConfigStorageMode storageMode;
    private IXmlConfigStorageProvider storageProvider;
    private const string xmlnsConfigURI = "urn:telerik:sitefinity:configuration";
    private const string xmlnsConfigPrefix = "config";
    private const string versionAttributeName = "version";
    private const string valueAttributeName = "value";
    private const string keyAttributeName = "key";
    private const string originAttributeName = "origin";
    private const string elementSecretPrefixAttributeName = "secret-";
    private const string elementFlagsAttributeName = "flags";
    private const string elementKeyAttributeName = "key";
    private const string xmlnsTypeURI = "urn:telerik:sitefinity:configuration:type";
    private const string xmlnsTypePrefix = "type";
    private const string elementTypeAttributeName = "this";
    private const string linkElementName = "link";
    internal const string linkItemConfigSourcePropertyName = "configSource";
    private const string lazyItemElementName = "lazy";
    private const string lazyItemConfigFilePropertyName = "configFile";
    private static readonly IList<string> NonMigratableConfigsInFilesystemMode = (IList<string>) new List<string>()
    {
      "Telerik.Sitefinity.Data.Configuration.DataConfig",
      "Telerik.Sitefinity.Localization.Configuration.ResourcesConfig",
      "Telerik.Sitefinity.SalesForceConnector.Configuration.SalesForceConnectorConfig",
      "Telerik.Sitefinity.SharepointConnector.Configuration.SharepointConnectorConfig",
      "Telerik.Sitefinity.MarketoConnector.Configuration.MarketoConnectorConfig",
      "Telerik.Sitefinity.DataIntelligenceConnector.Configuration.DigitalExperienceCloudConnectorConfig",
      "Telerik.Sitefinity.Modules.Libraries.Configuration.LibrariesConfig",
      "Telerik.Sitefinity.Personalization.Impl.Configuration.PersonalizationConfig",
      "Telerik.Sitefinity.Modules.ResponsiveDesign.Configuration.ResponsiveDesignConfig"
    };
    private string[] systemKeys = new string[1]
    {
      CustomFieldsContext.CustomFieldsSectionName
    };

    /// <inheritdoc />
    public override ConfigStorageMode StorageMode => this.storageMode;

    /// <summary>
    /// Exports all settings that are different from the default values to external resource.
    /// </summary>
    /// <param name="section">The configuration section to be exported.</param>
    public override void SaveSection(ConfigSection section)
    {
      bool isDatabaseMode = Telerik.Sitefinity.Configuration.Config.IsDatabaseMode(this.StorageMode);
      if (!isDatabaseMode && !SystemManager.IsOperationEnabled(RestrictionLevel.ReadOnlyConfigFile))
        throw new NotSupportedException("Configuration update is not allowed.");
      if (section == null)
        throw new ArgumentNullException(nameof (section));
      this.DemandManagePermission();
      ExecutingEventArgs args = new ExecutingEventArgs(nameof (SaveSection), (object) section);
      this.OnExecuting(args);
      if (args.Cancel)
        return;
      if (this.StorageMode == ConfigStorageMode.Auto)
        this.SaveSectionAuto(section, isDatabaseMode);
      else
        this.SaveSectionSpecific(section, isDatabaseMode);
      CacheDependency.Notify((IList<CacheDependencyKey>) new CacheDependencyKey[1]
      {
        new CacheDependencyKey()
        {
          Type = section.GetType(),
          Key = section.GetPath()
        }
      });
      this.OnExecuted(new ExecutedEventArgs(nameof (SaveSection), (object) section, (object) null));
      EventHub.Raise((IEvent) new ConfigEvent()
      {
        ConfigName = section.TagName,
        ProviderName = this.Name,
        Action = DataEventAction.Updated
      }, false);
    }

    /// <summary>Exports configuration section as string</summary>
    /// <param name="section">Configuration section to be exported</param>
    /// <returns>The content of exported section as XML string</returns>
    public override string Export(ConfigSection section) => this.Export(section, false);

    /// <summary>Exports configuration section as string</summary>
    /// <param name="section">Configuration section to be exported</param>
    /// <param name="skipLoadFromFile">Defines whether to skip loading the default configurations from the file system</param>
    /// <returns>The content of exported section as XML string</returns>
    public override string Export(ConfigSection section, bool skipLoadFromFile)
    {
      if (section == null)
        throw new ArgumentNullException(nameof (section));
      this.DemandManagePermission();
      return this.ExportInternal(section, skipLoadFromFile);
    }

    /// <inheritdoc />
    internal override string Export(
      IEnumerable<ConfigElement> configElementsToBeExported,
      bool skipLoadFromFile = false,
      ExportMode exportMode = ExportMode.Default)
    {
      this.DemandManagePermission();
      return configElementsToBeExported.Any<ConfigElement>() ? this.ExportInternal(configElementsToBeExported.First<ConfigElement>().Section, skipLoadFromFile, configElementsToBeExported, exportMode) : string.Empty;
    }

    /// <summary>Imports XML string to specified section</summary>
    /// <param name="sectionType">Section where the XML string to be imported in</param>
    /// <param name="xml">Content as XML string</param>
    public override void Import(Type sectionType, string xml, bool saveInFileSystemMode = false) => this.ImportInternal(Telerik.Sitefinity.Configuration.Config.Get(sectionType), xml, saveInFileSystemMode);

    /// <inheritdoc />
    internal override void Import(
      Type sectionType,
      string xml,
      string origin,
      bool saveInFileSystemMode = false,
      bool overrideOrigin = false,
      IEnumerable<string> pathsOfElementsToOverride = null)
    {
      this.ImportInternal(Telerik.Sitefinity.Configuration.Config.Get(sectionType), xml, saveInFileSystemMode, origin, overrideOrigin, pathsOfElementsToOverride);
    }

    /// <inheritdoc />
    internal override void MigrateSection(ConfigSection section)
    {
      if (this.StorageMode == ConfigStorageMode.Auto || this.ShouldSkipMigrationFor(section))
        return;
      using (new XmlConfigProvider.MigrationStorageModeRegion(this))
      {
        SaveOptions options1 = new SaveOptions(true, true);
        SaveOptions options2 = new SaveOptions(skipLoadFromFile: true, inheritDatabase: true);
        this.SaveSectionInternal(section, options1);
        this.SaveSectionInternal(section, options2);
      }
    }

    /// <inheritdoc />
    internal override void MoveElement(ConfigElement element, ConfigSource target)
    {
      if (this.StorageMode != ConfigStorageMode.Auto)
        throw new InvalidOperationException("Cannot move sections when not in auto storage mode.");
      if (Telerik.Sitefinity.Configuration.Config.RestrictionLevel == RestrictionLevel.ReadOnlyConfigFile)
        throw new InvalidOperationException("Cannot move sections when in read only mode.");
      if (element.Source == ConfigSource.Default)
        throw new InvalidOperationException("Cannot move elements from sitefinity assemblies (with default values).");
      if (element.Parent != null && element.Parent.Source == ConfigSource.Database)
        throw new InvalidOperationException("Cannot move elements with parents in database.");
      if (element.Source == target)
        return;
      if (target == ConfigSource.Database)
        throw new NotSupportedException("Moving configuration elements to Database is not supported, because of inability to automatically deliver the changes to the next environment during continuous delivery process.");
      element.Source = target == ConfigSource.FileSystem ? target : throw new InvalidOperationException("Invalid destination to move configuration element to.");
      ConfigSection section = element.Section;
      SaveOptions options1 = new SaveOptions(skipLoadFromFile: true, inheritDatabase: true);
      options1.OperationType = OperationType.Move;
      SaveOptions options2 = new SaveOptions(true);
      this.SaveSectionInternal(section, options1);
      this.SaveSectionInternal(section, options2);
    }

    private bool ShouldSkipMigrationFor(ConfigSection section) => this.StorageMode == ConfigStorageMode.FileSystem && XmlConfigProvider.NonMigratableConfigsInFilesystemMode.Contains(section.GetType().FullName);

    private void SaveSectionSpecific(ConfigSection section, bool isDatabaseMode)
    {
      SaveOptions options = new SaveOptions(isDatabaseMode, !isDatabaseMode)
      {
        IsRuntime = isDatabaseMode
      };
      this.SaveSectionInternal(section, options);
    }

    private void SaveSectionAuto(ConfigSection section, bool isDatabaseMode)
    {
      bool flag = section.SafeMode.HasValue && section.SafeMode.Value;
      if (!isDatabaseMode)
      {
        if (Telerik.Sitefinity.Configuration.Config.SiteContext != null && Telerik.Sitefinity.Configuration.Config.SiteContext.SiteId != Guid.Empty)
          throw new InvalidOperationException("Saving configurations for site in file system mode is not supported.");
        SaveOptions options1 = new SaveOptions(skipLoadFromFile: true);
        this.SaveSectionInternal(section, options1);
        if (flag)
          return;
        SaveOptions options2 = new SaveOptions(true);
        this.SaveSectionInternal(section, options2);
      }
      else
      {
        if (flag)
          return;
        SaveOptions options = new SaveOptions(true)
        {
          IsRuntime = true
        };
        if (Telerik.Sitefinity.Configuration.Config.SiteContext != null)
          options.SiteId = Telerik.Sitefinity.Configuration.Config.SiteContext.SiteId;
        this.SaveSectionInternal(section, options);
      }
    }

    private void SaveSectionInternal(ConfigSection section, SaveOptions options) => this.SaveElement((ConfigElement) section, (ConfigElement) section.GetDefaultConfig(options), options);

    private string SaveElement(
      ConfigElement config,
      ConfigElement defaultConfig,
      SaveOptions options)
    {
      return this.SaveElement(config, (Func<XmlDocument, XmlElement>) (xmlDoc => this.GetXmlElement(xmlDoc, config, defaultConfig, config.TagName, config.GetType(), options, new XmlConfigProvider.XmlElementFactory(this.RootXmlElementFactory)) ?? xmlDoc.CreateElement(config.TagName)), options);
    }

    private string ExportInternal(
      ConfigSection config,
      bool skipLoadFromFile = false,
      IEnumerable<ConfigElement> elementsToExport = null,
      ExportMode exportMode = ExportMode.Default)
    {
      SaveOptions options = new SaveOptions(Telerik.Sitefinity.Configuration.Config.IsDatabaseMode(this.StorageMode), skipLoadFromFile, exportMode: exportMode);
      options.OperationType = OperationType.Export;
      options.ElementsToExport = elementsToExport;
      options.IsRuntime = true;
      ConfigElement defaultConfig = (ConfigElement) config.GetDefaultConfig(options);
      XmlDocument xmlDocument = this.GetXmlDocument((Func<XmlDocument, XmlElement>) (xmlDoc => this.GetXmlElement(xmlDoc, (ConfigElement) config, defaultConfig, config.TagName, config.GetType(), options, new XmlConfigProvider.XmlElementFactory(this.RootXmlElementFactory)) ?? xmlDoc.CreateElement(config.TagName)));
      return xmlDocument == null ? string.Empty : xmlDocument.OuterXml;
    }

    private void ImportInternal(
      ConfigSection section,
      string xml,
      bool saveInFileSystemMode,
      string origin = null,
      bool overrideElementsWithSameOrigin = false,
      IEnumerable<string> pathsOfElementsToOverride = null)
    {
      using (XmlReader reader = (XmlReader) new XmlTextReader((TextReader) new StringReader(xml)))
      {
        if (reader == null)
          return;
        LoadContext loadContext = new LoadContext(true, ConfigSource.Import)
        {
          ImportContext = new ConfigImportContext(origin, overrideElementsWithSameOrigin, pathsOfElementsToOverride)
        };
        this.LoadSectionFromReader(section, reader, loadContext);
        if (saveInFileSystemMode)
        {
          using (new FileSystemModeRegion())
            this.SaveSection(section);
        }
        else
          this.SaveSection(section);
      }
    }

    private string SaveElement(
      ConfigElement config,
      Func<XmlDocument, XmlElement> getXmlElement,
      SaveOptions options,
      string relativePath = null)
    {
      XmlDocument xmlDocument = this.GetXmlDocument(getXmlElement);
      if (xmlDocument == null)
        return (string) null;
      if (relativePath == null)
        relativePath = this.GetRelativeFilePath(config, true);
      if (options != null && options.SiteId != Guid.Empty)
        relativePath = options.SiteId.ToString() + "_" + relativePath;
      this.SaveXml(xmlDocument, relativePath, options);
      return relativePath;
    }

    private void SaveXml(XmlDocument xml, string relativePath, SaveOptions options)
    {
      if (!options.DatabaseMode || Telerik.Sitefinity.Configuration.Config.SafeMode || !Telerik.Sitefinity.Configuration.Config.HasDefaultDatabaseConnection)
      {
        string filePath = Path.Combine(this.storageFolder, relativePath);
        this.SaveXmlToFile(xml, filePath);
      }
      else
        this.storageProvider.SaveElement((IXmlConfigItem) new TempXmlConfigItem()
        {
          Path = relativePath,
          Data = xml.OuterXml
        });
    }

    private void SaveXmlToFile(XmlDocument xml, string filePath)
    {
      int num = 0;
      while (num < 10)
      {
        ++num;
        try
        {
          if (!this.ShouldSaveFile(xml, filePath))
            break;
          if (!SystemManager.IsOperationEnabled(RestrictionLevel.ReadOnlyConfigFile))
          {
            num = 10;
            throw new NotSupportedException("Configuration update is not allowed. (SaveXmlToFile)");
          }
          using (XmlWriter configurationWriter = this.GetConfigurationWriter(filePath))
          {
            xml.WriteTo(configurationWriter);
            configurationWriter.Flush();
            break;
          }
        }
        catch (Exception ex)
        {
          if (num == 10)
            throw;
          else
            Thread.Sleep(300);
        }
      }
    }

    private XmlDocument GetXmlDocument(Func<XmlDocument, XmlElement> getXmlElement)
    {
      XmlDocument xmlDocument = new XmlDocument();
      XmlElement newChild = getXmlElement(xmlDocument);
      if (newChild == null)
        return (XmlDocument) null;
      xmlDocument.AppendChild((XmlNode) newChild);
      return xmlDocument;
    }

    protected internal static XmlElement CreateXmlElement(
      XmlDocument xmlDoc,
      string tagName,
      XmlConfigProvider.XmlElementFactory factory = null)
    {
      return factory != null ? factory(xmlDoc, tagName) : xmlDoc.CreateElement(tagName);
    }

    private XmlElement RootXmlElementFactory(XmlDocument xmlDoc, string tagName)
    {
      XmlElement element = xmlDoc.CreateElement(tagName);
      element.SetAttribute("xmlns:config", "urn:telerik:sitefinity:configuration");
      element.SetAttribute("xmlns:type", "urn:telerik:sitefinity:configuration:type");
      element.SetAttribute("version", "urn:telerik:sitefinity:configuration", this.Version.ToString());
      return element;
    }

    private XmlElement GetXmlElement(
      XmlDocument xmlDoc,
      ConfigElement config,
      ConfigElement defaultConfig,
      string tagName,
      Type definedType,
      SaveOptions options,
      XmlConfigProvider.XmlElementFactory xmlElementFactory = null,
      bool setEditFlag = false)
    {
      bool isNew = defaultConfig == null;
      bool flag1 = this.ShouldPersistElement(config, options, isNew);
      bool flag2 = this.HandleMoveOperation(config, defaultConfig, options);
      XmlElement xmlElement1 = (XmlElement) null;
      IList<Tuple<string, string>> tupleList = (IList<Tuple<string, string>>) new List<Tuple<string, string>>();
      Type type1 = config.GetType();
      string name = (string) null;
      string str1 = (string) null;
      foreach (ConfigProperty property in (Collection<ConfigProperty>) config.Properties)
      {
        PersistedValueWrapper valueWrapper;
        object rawValue = config.GetRawValue(property, out valueWrapper);
        object obj = this.GetDefaultRawValue(config, defaultConfig, property);
        XmlElement xmlElement2 = (XmlElement) null;
        if (typeof (ConfigElement).IsAssignableFrom(property.Type))
        {
          if (config is IHasDefaultConfigElements && (!options.DatabaseMode || options.IsRuntime))
            obj = (object) ((IHasDefaultConfigElements) config).GetDefaultConfig(property);
          if (typeof (ConfigElementCollection).IsAssignableFrom(property.Type))
          {
            ConfigElementCollection collection = (ConfigElementCollection) rawValue;
            ConfigElementCollection defaultCollection = (ConfigElementCollection) obj;
            xmlElement2 = this.GetXmlCollection(xmlDoc, property, collection, defaultCollection, options);
          }
          else
            xmlElement2 = this.GetXmlElement(xmlDoc, rawValue as ConfigElement, obj as ConfigElement, property.Name, property.Type, options);
        }
        else if (rawValue != null)
        {
          bool flag3 = options.SiteId != Guid.Empty;
          if (!flag3 || config.PersistsSiteSpecificValues)
          {
            if (!flag1 || flag3 && !property.IsSiteSpecific(config))
            {
              if (property.IsKey)
              {
                name = property.Name;
                str1 = ConfigElement.GetStringValue(rawValue, property);
                continue;
              }
              continue;
            }
            if (property.IsKey || this.ShouldPersistPropertyValue(config, property, options, valueWrapper))
            {
              if (rawValue is LazyValue lazyValue)
              {
                if (!lazyValue.Equals(obj) || property.IsKey)
                {
                  if (xmlElement1 == null)
                    xmlElement1 = XmlConfigProvider.CreateXmlElement(xmlDoc, tagName, xmlElementFactory);
                  string str2;
                  if (lazyValue is SecretValue secretValue)
                  {
                    str2 = this.GeneretateSecretKeyValue(secretValue.Key, secretValue.ResolverName);
                    if (rawValue is EnvironmentValue environmentValue && environmentValue.ResolverName == "EnvVariables")
                    {
                      str2 = environmentValue.GetOriginalValue();
                      if (str2 == null)
                        continue;
                    }
                  }
                  else
                    str2 = lazyValue.Key;
                  xmlElement1.SetAttribute(property.Name, str2);
                  continue;
                }
                continue;
              }
              Type type2 = rawValue.GetType();
              if (type2.ImplementsGenericInterface(typeof (IDictionary<,>)))
                xmlElement2 = this.GetXmlGenericCollection(xmlDoc, property, rawValue, obj, "GetXmlIDictionary");
              else if (type2.ImplementsGenericInterface(typeof (ICollection<>)))
                xmlElement2 = this.GetXmlGenericCollection(xmlDoc, property, rawValue, obj, "GetXmlICollection");
              else if (rawValue is NameValueCollection && property.Name == "parameters")
              {
                NameValueCollection nameValueCollection1 = obj as NameValueCollection;
                NameValueCollection nameValueCollection2 = rawValue as NameValueCollection;
                SecretNameValueCollection nameValueCollection3 = nameValueCollection2 as SecretNameValueCollection;
                foreach (string allKey in nameValueCollection2.AllKeys)
                {
                  string[] strArray = nameValueCollection2.GetValues(allKey);
                  if (nameValueCollection1 != null)
                  {
                    string[] values = nameValueCollection1.GetValues(allKey);
                    if (values != null)
                    {
                      List<string> stringList = new List<string>();
                      foreach (string str3 in strArray)
                      {
                        if (!((IEnumerable<string>) values).Contains<string>(str3))
                          stringList.Add(str3);
                      }
                      strArray = stringList.ToArray();
                    }
                  }
                  if (strArray != null && strArray.Length != 0)
                  {
                    if (xmlElement1 == null)
                      xmlElement1 = XmlConfigProvider.CreateXmlElement(xmlDoc, tagName, xmlElementFactory);
                    string key = string.Join(",", strArray);
                    string resolver;
                    if (nameValueCollection3 != null && nameValueCollection3.IsSecret(allKey, out resolver))
                    {
                      key = this.GeneretateSecretKeyValue(key, resolver);
                      if (resolver == "EnvVariables")
                      {
                        string environmentOriginalValue = nameValueCollection3.GetEnvironmentOriginalValue(allKey);
                        if (environmentOriginalValue != null)
                          key = environmentOriginalValue;
                        else
                          continue;
                      }
                    }
                    xmlElement1.SetAttribute(allKey, key);
                  }
                }
              }
              else
              {
                bool flag4 = false;
                if (obj != null)
                {
                  if (property.Type == typeof (Type) && type2 == typeof (string))
                  {
                    string str4 = obj is string ? (string) obj : ((Type) obj).FullName;
                    flag4 = ((string) rawValue).StartsWith(str4);
                  }
                  else
                    flag4 = rawValue.Equals(obj);
                }
                else if (rawValue.GetType().Equals(typeof (string)) && ((string) rawValue).IsNullOrEmpty())
                  flag4 = true;
                bool flag5 = ((!flag4 ? 1 : (options.MoveWithParent ? 1 : 0)) | (flag2 ? 1 : 0)) != 0 || flag3 && config.PersistsSiteSpecificValues;
                if (property.IsKey | flag5)
                {
                  string stringValue = ConfigElement.GetStringValue(rawValue, property);
                  if (xmlElement1 == null & flag5)
                    xmlElement1 = XmlConfigProvider.CreateXmlElement(xmlDoc, tagName, xmlElementFactory);
                  if (property.IsKey)
                  {
                    name = property.Name;
                    str1 = stringValue;
                  }
                  else
                    this.GetXmlValue(xmlElement1, property.Name, stringValue, property.Type, type2);
                }
              }
            }
            else
              continue;
          }
          else
            continue;
        }
        else if (((!property.Type.IsNullable() ? 0 : (obj != null ? 1 : 0)) & (flag1 ? 1 : 0)) != 0)
        {
          if (xmlElement1 == null)
            xmlElement1 = XmlConfigProvider.CreateXmlElement(xmlDoc, tagName, xmlElementFactory);
          xmlElement1.SetAttribute(property.Name, "");
        }
        if (xmlElement2 != null)
        {
          xmlElement1 = xmlElement1 ?? XmlConfigProvider.CreateXmlElement(xmlDoc, tagName, xmlElementFactory);
          xmlElement1.AppendChild((XmlNode) xmlElement2);
          if (!config.Origin.IsNullOrWhitespace() && options.OperationType != OperationType.Export)
            XmlConfigProvider.PersistOrigin(xmlElement2, config.Origin);
        }
      }
      if (name == null & isNew & flag1 && xmlElement1 == null)
        xmlElement1 = XmlConfigProvider.CreateXmlElement(xmlDoc, tagName, xmlElementFactory);
      if (xmlElement1 != null)
      {
        if (str1 != null)
          xmlElement1.SetAttribute(name, str1);
        else if (setEditFlag && config.Parent is ConfigElementCollection parent)
        {
          IConfigElementItem configElementItem = parent.Items.First<IConfigElementItem>((Func<IConfigElementItem, bool>) (i => i.Element == config));
          int num = parent.Items.ToList<IConfigElementItem>().IndexOf(configElementItem);
          if (num != -1)
            XmlConfigProvider.PersistDynamicKey(xmlElement1, num.ToString());
        }
        if (tupleList.Any<Tuple<string, string>>())
          this.PersistSecretProperties(xmlElement1, tupleList);
        XmlConfigProvider.Flags flags = XmlConfigProvider.Flags.None;
        if (config.PersistsSiteSpecificValues)
          flags |= XmlConfigProvider.Flags.HasSiteSpecificValuesPersisted;
        if (setEditFlag && !options.MoveWithParent)
          flags |= XmlConfigProvider.Flags.Edit;
        if (flags != XmlConfigProvider.Flags.None)
          XmlConfigProvider.PersistFlags(xmlElement1, flags);
        if (!config.Origin.IsNullOrWhitespace() && options.OperationType != OperationType.Export)
          XmlConfigProvider.PersistOrigin(xmlElement1, config.Origin);
        if (defaultConfig == null)
          XmlConfigProvider.PersistPropertyType(xmlElement1, definedType, type1);
      }
      if (flag2)
        options.MoveWithParent = false;
      return xmlElement1;
    }

    private void PersistSecretProperties(
      XmlElement xmlElement,
      IList<Tuple<string, string>> secretProperties)
    {
      foreach (Tuple<string, string> secretProperty in (IEnumerable<Tuple<string, string>>) secretProperties)
        xmlElement.SetAttribute("secret-" + secretProperty.Item1, "urn:telerik:sitefinity:configuration", secretProperty.Item2);
    }

    private bool HandleMoveOperation(
      ConfigElement config,
      ConfigElement defaultConfig,
      SaveOptions options)
    {
      if (options.OperationType != OperationType.Move)
        return false;
      bool flag = false;
      if (this.StorageMode != ConfigStorageMode.Auto)
        throw new InvalidOperationException("Moving config elements is allowed only in Auto storage mode.");
      if (Telerik.Sitefinity.Configuration.Config.RestrictionLevel == RestrictionLevel.ReadOnlyConfigFile)
        throw new InvalidOperationException("Moving config elements is not allowed if in ReadOnlyConfigFile restriction level.");
      if (defaultConfig != null && defaultConfig.Source != config.Source)
      {
        flag = true;
        options.MoveWithParent = true;
      }
      if (options.MoveWithParent && !flag)
        config.Source = config.Parent.Source;
      return flag;
    }

    private bool ShouldPersistPropertyValue(
      ConfigElement config,
      ConfigProperty prop,
      SaveOptions options,
      PersistedValueWrapper wrapperValue)
    {
      if (options.OperationType == OperationType.Save && this.StorageMode == ConfigStorageMode.Auto)
      {
        ConfigSource source = wrapperValue != null ? wrapperValue.Source : ConfigSource.NotSet;
        return this.ShouldPersist(options, source);
      }
      return options.OperationType != OperationType.Export || !prop.SkipOnExport;
    }

    private bool ShouldPersistElement(ConfigElement config, SaveOptions options, bool isNew)
    {
      if (options.OperationType == OperationType.Save)
      {
        if (options.SiteId != Guid.Empty && !config.PersistsSiteSpecificValues)
          return false;
        if (this.StorageMode == ConfigStorageMode.Auto)
        {
          if (isNew && (config.Source == ConfigSource.Default || config.Source == ConfigSource.Import))
            config.Source = options.DatabaseMode ? ConfigSource.Database : ConfigSource.FileSystem;
          return this.ShouldPersist(options, config.Source);
        }
      }
      else if (options.OperationType == OperationType.Export && options.ElementsToExport != null && options.ElementsToExport.Count<ConfigElement>() != 0)
        return (options.ExportMode != ExportMode.Deployment || config.Origin.IsNullOrWhitespace()) && (options.ExportMode != ExportMode.AddOn || isNew) && this.ShouldExportPath(config.GetPath(), options.ElementsToExport);
      return true;
    }

    private bool ShouldPersistItem(
      IConfigElementItem configItem,
      ConfigElementCollection collection,
      ConfigElementCollection defaultCollection,
      SaveOptions options)
    {
      if (options.OperationType == OperationType.Save)
      {
        if (this.StorageMode == ConfigStorageMode.Auto)
        {
          if (options.SiteId != Guid.Empty && !configItem.Element.PersistsSiteSpecificValues)
            return false;
          if (configItem is ILazyConfigElementItem configElementItem)
          {
            ConfigSource configSource = options.DatabaseMode ? ConfigSource.Database : ConfigSource.FileSystem;
            return configElementItem.Sources.Contains<ConfigSource>(configSource);
          }
          ConfigSource source;
          if (configItem.ItemProperties.ContainsKey("configSource"))
          {
            source = (ConfigSource) configItem.ItemProperties["configSource"];
            if (source == ConfigSource.Import)
            {
              source = options.DatabaseMode ? ConfigSource.Database : ConfigSource.FileSystem;
              configItem.ItemProperties["configSource"] = (object) source;
            }
          }
          else if (defaultCollection == null || !defaultCollection.Contains(configItem.Key))
          {
            source = options.DatabaseMode ? ConfigSource.Database : ConfigSource.FileSystem;
            configItem.ItemProperties["configSource"] = (object) source;
          }
          else
            source = collection.Source;
          return this.ShouldPersist(options, source);
        }
      }
      else if (options.OperationType == OperationType.Export && options.ElementsToExport != null && options.ElementsToExport.Count<ConfigElement>() != 0 && configItem is IConfigElementLink)
        return this.ShouldExportPath(((IConfigElementLink) configItem).Path, options.ElementsToExport);
      return true;
    }

    private bool ShouldPersist(SaveOptions options, ConfigSource source)
    {
      if (options.DatabaseMode)
      {
        if (!options.IsRuntime && source == ConfigSource.FileSystem)
          return false;
      }
      else if (source == ConfigSource.Database)
        return false;
      return true;
    }

    private bool ShouldExportPath(string configPath, IEnumerable<ConfigElement> elementsToExport)
    {
      foreach (ConfigElement configElement in elementsToExport)
      {
        if (configPath.StartsWith(configElement.GetPath()))
          return true;
      }
      return false;
    }

    private bool ShouldPersistCollectionChange(
      ConfigElementCollection collection,
      SaveOptions options,
      string operation,
      string key = null)
    {
      if (options.OperationType == OperationType.Save)
      {
        if (this.StorageMode == ConfigStorageMode.Auto)
        {
          ConfigSource source = collection.GetChangeSource(operation, key);
          if (source == ConfigSource.NotSet)
          {
            source = options.DatabaseMode ? ConfigSource.Database : ConfigSource.FileSystem;
            collection.SetChangeSource(source, operation, key);
          }
          return this.ShouldPersist(options, source);
        }
      }
      else if (options.OperationType == OperationType.Export && options.ElementsToExport != null && options.ElementsToExport.Count<ConfigElement>() != 0)
      {
        string configPath = collection.GetPath();
        if (key != null)
          configPath = configPath + (object) '/' + key;
        return this.ShouldExportPath(configPath, options.ElementsToExport);
      }
      return true;
    }

    private void GetXmlValue(
      XmlElement xmlElement,
      string propertyName,
      string propertyStringValue,
      Type definedType,
      Type actualType)
    {
      xmlElement.SetAttribute(propertyName, propertyStringValue);
      XmlConfigProvider.PersistPropertyType(xmlElement, definedType, actualType, propertyName);
    }

    private XmlElement GetXmlGenericCollection(
      XmlDocument xmlDoc,
      ConfigProperty property,
      object value,
      object defaultValue,
      string getXmlMethodName)
    {
      XmlElement xmlElement = (XmlElement) null;
      Type type = value.GetType();
      Type[] typeArray1;
      if (!type.IsArray)
        typeArray1 = type.GetGenericArguments();
      else
        typeArray1 = new Type[1]{ type.GetElementType() };
      Type[] typeArray2 = typeArray1;
      foreach (XmlElement newChild in (IEnumerable<XmlElement>) this.GetType().GetMethod(getXmlMethodName, BindingFlags.Instance | BindingFlags.NonPublic).MakeGenericMethod(typeArray2).Invoke((object) this, new object[3]
      {
        (object) xmlDoc,
        value,
        defaultValue
      }))
      {
        if (xmlElement == null)
        {
          xmlElement = xmlDoc.CreateElement(property.Name);
          XmlConfigProvider.PersistPropertyType(xmlElement, property.Type, type);
        }
        xmlElement.AppendChild((XmlNode) newChild);
      }
      return xmlElement;
    }

    private IEnumerable<XmlElement> GetXmlICollection<T>(
      XmlDocument xmlDoc,
      ICollection<T> collection,
      ICollection<T> defaultCollection)
    {
      List<XmlElement> xmlIcollection = new List<XmlElement>();
      if (defaultCollection != null)
      {
        foreach (T obj in (IEnumerable<T>) defaultCollection)
        {
          if (!collection.Contains(obj))
          {
            XmlElement element = xmlDoc.CreateElement("remove");
            this.GetXmlValue(element, "value", ConfigElement.GetStringValue((object) obj), typeof (T), obj.GetType());
            xmlIcollection.Add(element);
          }
        }
      }
      foreach (T obj in (IEnumerable<T>) collection)
      {
        if (defaultCollection == null || !defaultCollection.Contains(obj))
        {
          XmlElement element = xmlDoc.CreateElement("add");
          this.GetXmlValue(element, "value", ConfigElement.GetStringValue((object) obj), typeof (T), obj.GetType());
          xmlIcollection.Add(element);
        }
      }
      return (IEnumerable<XmlElement>) xmlIcollection;
    }

    private IEnumerable<XmlElement> GetXmlIDictionary<TKey, TValue>(
      XmlDocument xmlDoc,
      IDictionary<TKey, TValue> dictionary,
      IDictionary<TKey, TValue> defaultDictionary)
    {
      List<XmlElement> xmlIdictionary = new List<XmlElement>();
      if (defaultDictionary != null)
      {
        foreach (KeyValuePair<TKey, TValue> keyValuePair in (IEnumerable<KeyValuePair<TKey, TValue>>) defaultDictionary)
        {
          if (!dictionary.ContainsKey(keyValuePair.Key))
          {
            XmlElement element = xmlDoc.CreateElement("remove");
            this.GetXmlValue(element, "key", ConfigElement.GetStringValue((object) keyValuePair.Key), typeof (TKey), keyValuePair.Key.GetType());
            xmlIdictionary.Add(element);
          }
        }
      }
      foreach (KeyValuePair<TKey, TValue> keyValuePair in (IEnumerable<KeyValuePair<TKey, TValue>>) dictionary)
      {
        TValue obj1;
        TValue obj2;
        if (defaultDictionary != null && defaultDictionary.TryGetValue(keyValuePair.Key, out obj1))
        {
          obj2 = keyValuePair.Value;
          if (obj2.Equals((object) obj1))
            continue;
        }
        XmlElement element = xmlDoc.CreateElement("add");
        this.GetXmlValue(element, "key", ConfigElement.GetStringValue((object) keyValuePair.Key), typeof (TKey), keyValuePair.Key.GetType());
        XmlElement xmlElement = element;
        string stringValue = ConfigElement.GetStringValue((object) keyValuePair.Value);
        Type definedType = typeof (TValue);
        obj2 = keyValuePair.Value;
        Type type = obj2.GetType();
        this.GetXmlValue(xmlElement, "value", stringValue, definedType, type);
        xmlIdictionary.Add(element);
      }
      return (IEnumerable<XmlElement>) xmlIdictionary;
    }

    private XmlElement GetXmlCollection(
      XmlDocument xmlDoc,
      ConfigProperty property,
      ConfigElementCollection collection,
      ConfigElementCollection defaultCollection,
      SaveOptions options)
    {
      string xml;
      if (collection.TryGetPendingLoad(options, out xml))
      {
        XmlElement element = xmlDoc.CreateElement(property.Name);
        element.InnerXml = xml;
        return element;
      }
      LinkedList<XmlNode> linkedList = new LinkedList<XmlNode>();
      if (collection.IsCleared)
      {
        if (this.ShouldPersistCollectionChange(collection, options, collection.ClearElementName))
          linkedList.AddLast((XmlNode) xmlDoc.CreateElement(collection.ClearElementName));
      }
      else if (defaultCollection != null)
      {
        foreach (string removedKey in (IEnumerable<string>) collection.RemovedKeys)
        {
          if (defaultCollection.GetElementByKey(removedKey) != null && this.ShouldPersistCollectionChange(collection, options, collection.RemoveElementName, removedKey))
          {
            XmlElement element = xmlDoc.CreateElement(collection.RemoveElementName);
            element.SetAttribute(collection.GetKeyName(), removedKey);
            linkedList.AddLast((XmlNode) element);
          }
        }
        foreach (IConfigElementItem configElementItem in collection.Items.Where<IConfigElementItem>((Func<IConfigElementItem, bool>) (i => i.IsDeleted)))
        {
          string key = configElementItem.Element.GetKey();
          if (defaultCollection.Contains(key) && this.ShouldPersistCollectionChange(collection, options, collection.RemoveElementName, key))
          {
            XmlElement element = xmlDoc.CreateElement(collection.RemoveElementName);
            XmlConfigProvider.PersistDynamicKey(element, key);
            linkedList.AddLast((XmlNode) element);
          }
        }
      }
      foreach (IConfigElementItem configItem in collection.Items)
      {
        if (!configItem.IsDeleted)
        {
          ConfigElementCollection defaultCollection1 = (collection.IsCleared ? 1 : (collection.RemovedKeys.Contains(configItem.Key) ? 1 : 0)) != 0 ? (ConfigElementCollection) null : defaultCollection;
          XmlNode xmlNode = (XmlNode) null;
          if (configItem.IsLazy && !configItem.IsLoaded && options.OperationType == OperationType.Save)
          {
            if (configItem.ItemProperties.TryGetValue("configFile", out object _) && this.ShouldPersistItem(configItem, collection, defaultCollection, options))
              xmlNode = (XmlNode) this.GetXmlLazyItem(xmlDoc, configItem);
          }
          else
            xmlNode = this.GetXmlCollectionItem(xmlDoc, configItem, collection, defaultCollection1, options);
          if (xmlNode != null)
            linkedList.AddLast(xmlNode);
        }
      }
      XmlElement xmlCollection = (XmlElement) null;
      foreach (XmlNode newChild in linkedList)
      {
        if (newChild != null)
        {
          if (xmlCollection == null)
            xmlCollection = xmlDoc.CreateElement(property.Name);
          xmlCollection.AppendChild(newChild);
        }
      }
      if (collection.IsDynamic)
      {
        IList<string> inactiveElements = collection.GetInactiveElements();
        if (inactiveElements != null)
        {
          if (xmlCollection == null)
            xmlCollection = xmlDoc.CreateElement(property.Name);
          foreach (string str in (IEnumerable<string>) inactiveElements)
          {
            XmlDocumentFragment documentFragment = xmlDoc.CreateDocumentFragment();
            documentFragment.InnerXml = str;
            xmlCollection.AppendChild((XmlNode) documentFragment);
          }
        }
      }
      return xmlCollection;
    }

    private XmlNode GetXmlCollectionItem(
      XmlDocument xmlDoc,
      IConfigElementItem item,
      ConfigElementCollection collection,
      ConfigElementCollection defaultCollection,
      SaveOptions options)
    {
      if (item.LoadingError != null && !item.IsLazy)
      {
        if (item.LoadingError.RawXml.IsNullOrEmpty() || !this.ShouldPersistItem(item, collection, defaultCollection, options))
          return (XmlNode) null;
        XmlDocumentFragment documentFragment = xmlDoc.CreateDocumentFragment();
        documentFragment.InnerXml = item.LoadingError.RawXml;
        return (XmlNode) documentFragment;
      }
      XmlElement xmlCollectionItem;
      if (item is IConfigElementLink configElementLink)
      {
        if (!this.ShouldPersistItem((IConfigElementItem) configElementLink, collection, defaultCollection, options))
          return (XmlNode) null;
        xmlCollectionItem = this.GetXmlLinkedElement(xmlDoc, configElementLink, collection, defaultCollection);
      }
      else if (!item.IsLazy || options.OperationType == OperationType.Export)
      {
        xmlCollectionItem = this.GetXmlCollectionElement(xmlDoc, item.Element, collection, defaultCollection, options);
      }
      else
      {
        ConfigElement element = item.Element;
        if (element == null)
          return (XmlNode) null;
        string relativePath = (string) null;
        if (item.ItemProperties.ContainsKey("configFile"))
        {
          string itemProperty = item.ItemProperties["configFile"] as string;
          if (!itemProperty.IsNullOrEmpty())
            relativePath = itemProperty;
        }
        string str = this.SaveElement(element, (Func<XmlDocument, XmlElement>) (doc => this.GetXmlCollectionElement(doc, element, collection, defaultCollection, options, new XmlConfigProvider.XmlElementFactory(this.RootXmlElementFactory))), options, relativePath);
        if (str == null)
        {
          xmlCollectionItem = (XmlElement) null;
        }
        else
        {
          item.Key = element.GetKey();
          item.ItemProperties["configFile"] = (object) str;
          xmlCollectionItem = this.GetXmlLazyItem(xmlDoc, item);
        }
      }
      return (XmlNode) xmlCollectionItem;
    }

    private XmlElement GetXmlLazyItem(XmlDocument xmlDoc, IConfigElementItem item)
    {
      XmlElement element = xmlDoc.CreateElement("config", "lazy", "urn:telerik:sitefinity:configuration");
      if (!item.ItemProperties.ContainsKey("configFile"))
        throw new ProviderException("Cannot persist configuration element item with no 'configFile' item property specified.");
      if (!string.IsNullOrEmpty(item.Key))
        element.SetAttribute("key", item.Key);
      foreach (KeyValuePair<string, object> itemProperty in (IEnumerable<KeyValuePair<string, object>>) item.ItemProperties)
      {
        string stringValue = ConfigElement.GetStringValue(itemProperty.Value);
        element.SetAttribute(itemProperty.Key, stringValue);
      }
      return element;
    }

    private XmlElement GetXmlCollectionElement(
      XmlDocument xmlDoc,
      ConfigElement element,
      ConfigElementCollection collection,
      ConfigElementCollection defaultCollection,
      SaveOptions options,
      XmlConfigProvider.XmlElementFactory xmlElementFactory = null)
    {
      string tagName = collection.KeepRemoveItems ? element.CollectionItemName : collection.AddElementName;
      bool setEditFlag = false;
      ConfigElement defaultConfig = (ConfigElement) null;
      if (defaultCollection != null)
      {
        string key = element.GetKey();
        if (!string.IsNullOrEmpty(key))
        {
          defaultConfig = defaultCollection.GetElementByKey(key);
          if (defaultConfig != null)
            setEditFlag = true;
        }
      }
      return this.GetXmlElement(xmlDoc, element, defaultConfig, tagName, collection.ElementType, options, xmlElementFactory, setEditFlag);
    }

    private XmlElement GetXmlLinkedElement(
      XmlDocument xmlDoc,
      IConfigElementLink link,
      ConfigElementCollection collection,
      ConfigElementCollection defaultCollection)
    {
      if (defaultCollection != null && defaultCollection.Contains(link.Key))
        return (XmlElement) null;
      XmlElement element = xmlDoc.CreateElement("config", nameof (link), "urn:telerik:sitefinity:configuration");
      element.SetAttribute(collection.GetKeyName(), link.Key);
      element.SetAttribute("path", link.Path);
      if (link.ModuleName != null)
        element.SetAttribute("module", link.ModuleName);
      return element;
    }

    private object GetDefaultRawValue(
      ConfigElement config,
      ConfigElement defaultConfig,
      ConfigProperty property)
    {
      if (defaultConfig != null)
      {
        defaultConfig.EnsurePropertiesInitialized();
        return defaultConfig.GetRawValue(property);
      }
      try
      {
        return config.ResolveDefaultValue(property);
      }
      catch (Exception ex)
      {
        if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
          throw ex;
        return (object) null;
      }
    }

    private static void PersistPropertyType(
      XmlElement xmlElement,
      Type definedType,
      Type actualType,
      string propertyName = null)
    {
      if (actualType == definedType || definedType.Equals(typeof (Type)) || definedType.IsGenericType && definedType.GetGenericTypeDefinition() == typeof (Nullable<>) && actualType == definedType.GetGenericArguments()[0])
        return;
      string str = string.Format("{0}, {1}", (object) actualType.FullName, (object) actualType.Assembly.GetName().Name);
      string localName = propertyName != null ? propertyName : "this";
      xmlElement.SetAttribute(localName, "urn:telerik:sitefinity:configuration:type", str);
    }

    private static void PersistFlags(XmlElement xmlElement, XmlConfigProvider.Flags flags) => xmlElement.SetAttribute(nameof (flags), "urn:telerik:sitefinity:configuration", ((int) flags).ToString());

    private static XmlConfigProvider.Flags LoadFlags(XmlReader reader)
    {
      string attribute = reader.GetAttribute("flags", "urn:telerik:sitefinity:configuration");
      XmlConfigProvider.Flags result;
      return !string.IsNullOrEmpty(attribute) && Enum.TryParse<XmlConfigProvider.Flags>(attribute, out result) ? result : XmlConfigProvider.Flags.None;
    }

    private string GeneretateSecretKeyValue(string key, string resolver) => ConfigElement.GeneretateSecretKeyValue(key, resolver);

    private string GetStringValue(XmlReader reader, out string resolver) => ConfigElement.GetActualStringValue(reader.Value, out resolver);

    private static bool TryGetSecretResolver(
      XmlReader reader,
      string propName,
      out string secretResolver)
    {
      secretResolver = reader.GetAttribute("secret-" + propName, "urn:telerik:sitefinity:configuration");
      return secretResolver != null;
    }

    private static void PersistOrigin(XmlElement xmlElement, string origin) => xmlElement.SetAttribute(nameof (origin), "urn:telerik:sitefinity:configuration", origin);

    private static string LoadOrigin(XmlReader reader) => reader.GetAttribute("origin", "urn:telerik:sitefinity:configuration");

    private static void PersistDynamicKey(XmlElement xmlElement, string value) => xmlElement.SetAttribute("key", "urn:telerik:sitefinity:configuration", value);

    private static string LoadDynamicKey(XmlReader reader) => reader.GetAttribute("key", "urn:telerik:sitefinity:configuration");

    /// <summary>Writes an element to the XML document.</summary>
    /// <param name="element"><see cref="T:Telerik.Sitefinity.Configuration.ConfigElement" /></param>
    /// <param name="xml"><see cref="T:System.Xml.XmlWriter" /></param>
    /// <param name="tagName">The name of the tag</param>
    protected void WriteElement(
      ConfigElement element,
      XmlWriter xml,
      string tagName,
      Type objectType)
    {
      if (element == null)
        throw new ArgumentNullException(nameof (element));
      if (xml == null)
        throw new ArgumentNullException(nameof (xml));
      if (string.IsNullOrEmpty(tagName))
        throw new ArgumentNullException(nameof (tagName));
      xml.WriteStartElement(tagName);
      if (element is ConfigSection)
        xml.WriteAttributeString("version", this.GetType().Assembly.GetName().Version.ToString());
      foreach (ConfigProperty property in (Collection<ConfigProperty>) element.Properties)
      {
        if (!typeof (ConfigElement).IsAssignableFrom(property.Type))
        {
          object obj = element[property];
          if (obj != null)
          {
            if (!obj.Equals(property.DefaultValue))
            {
              string str;
              try
              {
                Type type = property.Type;
                if (property.Name == "parameters" && obj is NameValueCollection)
                {
                  NameValueCollection nameValueCollection = obj as NameValueCollection;
                  foreach (string allKey in nameValueCollection.AllKeys)
                  {
                    string[] values = nameValueCollection.GetValues(allKey);
                    xml.WriteAttributeString(allKey, string.Join(",", values));
                  }
                  continue;
                }
                str = !(type == typeof (DateTime)) ? (type == typeof (float) || type == typeof (double) || type == typeof (Decimal) ? Convert.ToString(obj, (IFormatProvider) CultureInfo.InvariantCulture) : (property.Converter == null || !property.Converter.CanConvertTo(typeof (string)) ? obj.ToString() : property.Converter.ConvertToString(obj))) : ((DateTime) obj).ToString("u");
              }
              catch (Exception ex)
              {
                throw new ConfigurationErrorsException(string.Format("There is no suitable converter for the type {0} to convert it to string. This type cannot be persisted through the configuration.", (object) property.GetType()), ex);
              }
              xml.WriteAttributeString(property.Name, str);
            }
          }
        }
      }
      if (objectType != (Type) null)
        xml.WriteAttributeString("objActType", objectType.AssemblyQualifiedName);
      foreach (ConfigProperty property in (Collection<ConfigProperty>) element.Properties)
      {
        if (typeof (ConfigElement).IsAssignableFrom(property.Type))
        {
          object element1 = element[property];
          if (typeof (ConfigElementCollection).IsAssignableFrom(property.Type))
          {
            xml.WriteStartElement(property.Name);
            ConfigElementCollection elementCollection = (ConfigElementCollection) element1;
            Type[] genericArguments = elementCollection.GetType().GetGenericArguments();
            Type type = genericArguments.Length != 2 ? genericArguments[0] : genericArguments[1];
            foreach (ConfigElement element2 in elementCollection)
            {
              string tagName1 = elementCollection.KeepRemoveItems ? element2.CollectionItemName : elementCollection.AddElementName;
              Type objectType1 = element2.GetType();
              if (objectType1 == type)
                objectType1 = (Type) null;
              this.WriteElement(element2, xml, tagName1, objectType1);
            }
            xml.WriteEndElement();
          }
          else
            this.WriteElement((ConfigElement) element1, xml, property.Name, (Type) null);
        }
      }
      xml.WriteEndElement();
    }

    /// <summary>
    /// Loads external settings and overrides the default values of the provided section.
    /// </summary>
    /// <param name="section">The section which should be populated with external settings.</param>
    /// <returns><c>true</c> if the section was loaded from a file; <c>false</c> - only the defaults are loaded.</returns>
    /// <example>/configuration/localization</example>
    public override bool LoadSection(ConfigSection section)
    {
      if (section == null)
        throw new ArgumentNullException(nameof (section));
      this.DemandViewPermission();
      LoadContext loadContext = new LoadContext()
      {
        CheckForUpgrade = true
      };
      bool flag = this.LoadSectionFromFile(section, loadContext);
      if (this.StorageMode != ConfigStorageMode.FileSystem)
        flag = this.TryLoadSectionFromDatabase(section, true, (LoadContext) null) | flag;
      section.OnPropertiesLoaded();
      XmlConfigProvider.PopulateEnvironmentVariables(section);
      return flag;
    }

    private static void PopulateEnvironmentVariables(ConfigSection section)
    {
      try
      {
        IList<EnvironmentVariable> sectionSettings = EnvironmentVariables.Current.GetSectionSettings(section.TagName);
        ISecretDataResolver resolver = (ISecretDataResolver) null;
        Telerik.Sitefinity.Configuration.Config.TryGetSecretResolver("EnvVariables", out resolver);
        if (resolver == null)
          return;
        foreach (EnvironmentVariable environmentVariable in (IEnumerable<EnvironmentVariable>) sectionSettings)
        {
          try
          {
            ConfigElement configElement = Telerik.Sitefinity.Configuration.Config.GetByPath<ConfigElement>(environmentVariable.Path, (ConfigElement) section);
            if (configElement == null && environmentVariable.IsRoot)
              configElement = (ConfigElement) section;
            ConfigProperty configProperty;
            configElement.Properties.TryGetValue(environmentVariable.PropertyName, out configProperty);
            PersistedValueWrapper valueWrapper;
            if (configProperty != null)
            {
              object originalValue = (object) null;
              ConfigSource source = ConfigSource.Default;
              configElement.GetRawValue(configProperty, out valueWrapper);
              if (valueWrapper != null)
              {
                source = valueWrapper.Source;
                originalValue = valueWrapper.Value;
              }
              EnvironmentValue environmentValue = new EnvironmentValue(environmentVariable.RawKey, configProperty, resolver.Name, originalValue);
              configElement.SetPersistedRawValue(configProperty, (object) environmentValue, source);
            }
            else
            {
              ConfigProperty prop;
              configElement.Properties.TryGetValue("parameters", out prop);
              if (prop != null)
              {
                configElement.GetRawValue(prop, out valueWrapper);
                string parameter = configElement.GetParameter(environmentVariable.PropertyName);
                configElement.SetEnvironmentParameter(environmentVariable.PropertyName, environmentVariable.RawKey, "EnvVariables", parameter);
              }
            }
          }
          catch (Exception ex)
          {
            throw new ConfigurationErrorsException(string.Format("Exception occured while processing environment key setting: {0}", (object) environmentVariable.RawKey), ex);
          }
        }
      }
      catch (Exception ex)
      {
        throw new ConfigurationErrorsException("Exception occured while processing environment configurations", ex);
      }
    }

    internal override bool LoadSection(ConfigSection section, LoadContext loadContext)
    {
      if (section == null)
        throw new ArgumentNullException(nameof (section));
      this.DemandViewPermission();
      bool flag = this.LoadSectionFromFile(section, loadContext);
      if (this.StorageMode != ConfigStorageMode.FileSystem)
        flag = this.LoadElementFromDatabase(section, loadContext) | flag;
      section.OnPropertiesLoaded();
      return flag;
    }

    private ConfigUpgradeContext GetUpgradingInfo(
      XmlReader reader,
      LoadContext loadContext)
    {
      string attribute = reader.GetAttribute("version", "urn:telerik:sitefinity:configuration");
      if (!attribute.IsNullOrEmpty())
      {
        Version version1 = Version.Parse(attribute);
        Version version2 = this.Version;
        if (version2 > version1)
        {
          if (version2.Major == version1.Major && version2.Minor == version1.Minor && version2.Build == version1.Build)
            return (ConfigUpgradeContext) null;
          return new ConfigUpgradeContext()
          {
            UpgradeFrom = version1,
            Source = loadContext.Source
          };
        }
      }
      return (ConfigUpgradeContext) null;
    }

    protected internal override bool LoadSectionFromFile(ConfigSection section) => this.LoadSectionFromFile(section, (LoadContext) null);

    protected internal override bool EnsureNormalMode(ConfigSection section)
    {
      if (this.StorageMode != ConfigStorageMode.FileSystem)
      {
        if (!this.TryLoadSectionFromDatabase(section, false, (LoadContext) null))
          return false;
        XmlConfigProvider.PopulateEnvironmentVariables(section);
        return true;
      }
      section.SafeMode = new bool?(false);
      return true;
    }

    private bool LoadSectionFromFile(ConfigSection section, LoadContext loadContext)
    {
      bool flag = false;
      if (loadContext == null)
        loadContext = new LoadContext();
      loadContext.Source = ConfigSource.FileSystem;
      using (XmlReader configurationReader = this.GetConfigurationReader((ConfigElement) section))
      {
        if (configurationReader != null)
          flag = this.LoadSectionFromReader(section, configurationReader, loadContext);
      }
      if (flag && loadContext.CheckForUpgrade && loadContext.UpgradeContext != null && (this.StorageMode != ConfigStorageMode.Database || Telerik.Sitefinity.Configuration.Config.SectionHandler.Settings.UpgradeFilesWhenDatabaseMode))
      {
        section.UpgradeContext = loadContext.UpgradeContext;
        section.Upgrade(loadContext.UpgradeContext.UpgradeFrom, this.Version);
        this.SaveSectionInternal(section, new SaveOptions(skipLoadFromFile: true));
        section.UpgradeContext = (ConfigUpgradeContext) null;
      }
      else if (loadContext.SaveNeeded)
        this.SaveSectionInternal(section, new SaveOptions(skipLoadFromFile: true));
      return true;
    }

    protected internal override bool RestoreSection(ConfigSection section)
    {
      XmlDocument xml = new XmlDocument();
      xml.AppendChild((XmlNode) xml.CreateElement(section.TagName));
      string relativeFilePath = this.GetRelativeFilePath((ConfigElement) section, true);
      if (Telerik.Sitefinity.Configuration.Config.ConfigStorageMode == ConfigStorageMode.Auto)
      {
        this.SaveXml(xml, relativeFilePath, new SaveOptions(true));
        this.SaveXml(xml, relativeFilePath, new SaveOptions());
      }
      else
        this.SaveXml(xml, relativeFilePath, new SaveOptions(Telerik.Sitefinity.Configuration.Config.IsDatabaseMode(this.StorageMode)));
      return true;
    }

    protected internal override IXmlConfigStorageProvider GetDatabaseStorageProvider() => this.storageProvider;

    private bool LoadSectionFromReader(
      ConfigSection section,
      XmlReader reader,
      LoadContext loadContext)
    {
      if (!XmlConfigProvider.ReadNextTag(reader))
        return false;
      if (reader.Name != section.TagName)
        throw new ConfigurationErrorsException(string.Format("Invalid configuration section root element \"{0}\". Section \"{1}\" expects element \"{2}\".", (object) reader.Name, (object) section.GetType().FullName, (object) section.TagName));
      if (loadContext.CheckForUpgrade)
        loadContext.UpgradeContext = this.GetUpgradingInfo(reader, loadContext);
      bool flag;
      if (loadContext.UpgradeContext != null)
      {
        section.InitUpgradeContext(loadContext.UpgradeContext.UpgradeFrom, loadContext.UpgradeContext);
        try
        {
          flag = this.LoadElement((ConfigElement) section, reader, loadContext, true);
        }
        catch (Exception ex)
        {
          int num1 = !(reader.LocalName == "lazy") ? 0 : (reader.NamespaceURI == "urn:telerik:sitefinity:configuration" ? 1 : 0);
          IXmlLineInfo xmlLineInfo = reader as IXmlLineInfo;
          int num2 = -1;
          int num3 = -1;
          if (xmlLineInfo != null && xmlLineInfo.HasLineInfo())
          {
            num2 = xmlLineInfo.LineNumber;
            num3 = xmlLineInfo.LinePosition;
          }
          string message = string.Format("Unable to load config section \"{0}\", line - {1}, position - {2}.", (object) section.TagName, (object) num2, (object) num3);
          if (num1 != 0)
            message += string.Format("Exception occured for element with config file name - {0}", (object) reader.GetAttribute("configFile"));
          throw new ConfigurationErrorsException(message, ex);
        }
      }
      else
        flag = this.LoadElement((ConfigElement) section, reader, loadContext, true);
      return flag;
    }

    private bool TryLoadSectionFromDatabase(
      ConfigSection section,
      bool initial,
      LoadContext loadContext)
    {
      if (section.SafeMode.HasValue)
      {
        if (!section.SafeMode.Value)
          return true;
        if (initial && section.SafeMode.Value)
          return false;
      }
      lock (section)
      {
        if (section.SafeMode.HasValue)
        {
          if (!section.SafeMode.Value)
            return true;
          if (initial && section.SafeMode.Value)
            return false;
        }
        if (Telerik.Sitefinity.Configuration.Config.SafeMode || !Telerik.Sitefinity.Configuration.Config.HasDefaultDatabaseConnection)
        {
          section.SafeMode = new bool?(true);
          return false;
        }
        section.SafeMode = new bool?(false);
        this.LoadElementFromDatabase(section, loadContext);
        if (this.StorageMode == ConfigStorageMode.Auto && Telerik.Sitefinity.Configuration.Config.SiteContext != null && Telerik.Sitefinity.Configuration.Config.SiteContext.SiteId != Guid.Empty)
        {
          if (loadContext == null)
            loadContext = new LoadContext()
            {
              SiteId = Telerik.Sitefinity.Configuration.Config.SiteContext.SiteId
            };
          this.LoadElementFromDatabase(section, loadContext);
        }
        return true;
      }
    }

    private bool LoadElementFromDatabase(ConfigSection section, LoadContext loadContext)
    {
      bool flag = false;
      string path = this.GetRelativeFilePath((ConfigElement) section, false);
      if (loadContext != null && loadContext.SiteId != Guid.Empty)
        path = loadContext.SiteId.ToString() + "_" + path;
      XmlReader elementDatabaseReader = this.GetElementDatabaseReader(path);
      if (elementDatabaseReader != null)
      {
        if (loadContext == null)
          loadContext = new LoadContext();
        loadContext.CheckForUpgrade = true;
        loadContext.Source = ConfigSource.Database;
        using (elementDatabaseReader)
          flag = this.LoadSectionFromReader(section, elementDatabaseReader, loadContext);
        if (flag && loadContext.UpgradeContext != null)
        {
          section.UpgradeContext = loadContext.UpgradeContext;
          section.Upgrade(loadContext.UpgradeContext.UpgradeFrom, this.Version);
          this.SaveSectionInternal(section, new SaveOptions(true));
          section.UpgradeContext = (ConfigUpgradeContext) null;
        }
        else if (loadContext.SaveNeeded)
          this.SaveSectionInternal(section, new SaveOptions(true));
      }
      return flag;
    }

    private ConfigElement LoadElement(
      string relativeFilePath,
      Func<XmlReader, ConfigElement> elementFactory,
      LoadContext loadContext,
      bool isDefault)
    {
      if (isDefault && this.StorageMode == ConfigStorageMode.FileSystem)
        return (ConfigElement) null;
      ConfigElement element = (ConfigElement) null;
      XmlReader reader = loadContext.Source == ConfigSource.Database ? this.GetElementDatabaseReader(relativeFilePath) : this.GetElementReader(relativeFilePath);
      if (reader != null)
      {
        using (reader)
        {
          if (XmlConfigProvider.ReadNextTag(reader))
          {
            element = elementFactory(reader);
            if (element != null)
              this.LoadElement(element, reader, loadContext, true);
          }
        }
      }
      return element;
    }

    private void LoadDelayedElement(
      ConfigElement element,
      string xmlString,
      LoadContext loadContext,
      bool isDefault)
    {
      XmlDocument xmlDocument = new XmlDocument();
      XmlElement element1 = xmlDocument.CreateElement(element.TagName);
      element1.InnerXml = xmlString;
      xmlDocument.AppendChild((XmlNode) element1);
      StringBuilder output = new StringBuilder();
      xmlDocument.Save(XmlWriter.Create(output));
      XmlTextReader reader = new XmlTextReader((TextReader) new StringReader(output.ToString()));
      if (reader == null)
        return;
      using (reader)
      {
        if (!XmlConfigProvider.ReadNextTag((XmlReader) reader))
          return;
        this.LoadElement(element, (XmlReader) reader, loadContext, true);
      }
    }

    private XmlReader GetElementDatabaseReader(string path)
    {
      IXmlConfigItem element = this.storageProvider.GetElement(path);
      return element != null ? XmlReader.Create((Stream) new MemoryStream(Encoding.UTF8.GetBytes(element.Data))) : (XmlReader) null;
    }

    private XmlReader GetElementReader(string relativeFilePath) => this.GetConfigurationReader(Path.Combine(this.storageFolder, relativeFilePath));

    /// <summary>
    /// Recursively reads a <see cref="T:Telerik.Sitefinity.Configuration.ConfigElement" />.
    /// </summary>
    /// <param name="element">The configuration element currently read.</param>
    /// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> in use.</param>
    /// <param name="options">The options.</param>
    /// <param name="upgradingInfo">The upgrading information.</param>
    /// <param name="isNew">if set to <c>true</c> [is new].</param>
    /// <param name="validateTagName">if set to <c>true</c> [validate tag name].</param>
    /// <returns>
    ///   <c>true</c> on success; <c>false</c> to indicate error which is not thrown and cause unwinding of the stack/recursion.
    /// </returns>
    /// <exception cref="T:System.Configuration.ConfigurationErrorsException"></exception>
    private bool LoadElement(
      ConfigElement element,
      XmlReader reader,
      LoadContext loadContext,
      bool isNew,
      bool validateTagName = false)
    {
      bool flag1 = true;
      bool flag2 = false;
      if (validateTagName && !string.IsNullOrEmpty(element.TagName) && reader.Name != element.TagName)
        throw new ConfigurationErrorsException(string.Format("<{0}> is invalid XML tag name for configuration element of type '{1}', which requires <{2}> tag.", (object) reader.Name, (object) element.GetType().FullName, (object) element.TagName));
      bool isEmptyElement = reader.IsEmptyElement;
      if (!flag2)
      {
        this.LoadAttributes(element, reader, loadContext, isNew);
        if (loadContext.UpgradeContext != null)
          loadContext.UpgradeContext.OnLoadElement(element);
        this.SetOriginOnLoad(element, reader, loadContext, isNew);
        element.PersistsSiteSpecificValues = (XmlConfigProvider.LoadFlags(reader) & XmlConfigProvider.Flags.HasSiteSpecificValuesPersisted) > XmlConfigProvider.Flags.None;
      }
      if (!isEmptyElement)
      {
        ConfigElementCollection collection = element as ConfigElementCollection;
        if (collection != null && collection.IsDefaultLoadingPostponed && (loadContext.UpgradeContext == null || !collection.EnsureDelayedInitialization()))
        {
          string xmlString = reader.ReadInnerXml();
          Action<ConfigElement, string> pendingLoad = (Action<ConfigElement, string>) ((el, xml) => this.LoadDelayedElement(el, xml, loadContext, collection.Section.IsDefaultConfig));
          collection.RegisterPendingLoad(xmlString, pendingLoad, loadContext);
          return true;
        }
        int depth = reader.Depth + 1;
        bool skipNextRead = false;
        List<string> stringList1 = new List<string>();
        List<string> stringList2 = new List<string>();
        if (loadContext.Source == ConfigSource.Import && collection != null && loadContext.ImportContext != null && loadContext.ImportContext.OverrideElementsWithSameOrigin)
          stringList1 = collection.Where<ConfigElement>((Func<ConfigElement, bool>) (ce => loadContext.ImportContext.Origin == ce.Origin)).Select<ConfigElement, string>((Func<ConfigElement, string>) (ce => ce.GetKey())).ToList<string>();
        while ((skipNextRead || XmlConfigProvider.ReadNextTag(reader, depth)) && reader.NodeType != XmlNodeType.EndElement)
        {
          if (collection != null)
          {
            ConfigElement element1;
            this.LoadCollectionElement(collection, reader, loadContext, out skipNextRead, out element1);
            if (skipNextRead && reader.NodeType != XmlNodeType.Element && reader.NodeType != XmlNodeType.EndElement)
              skipNextRead = false;
            if (element1 != null)
              stringList2.Add(element1.GetKey());
          }
          else
          {
            skipNextRead = false;
            if (flag2)
              reader.ReadInnerXml();
            else
              this.LoadPropertyElement(element, reader, loadContext);
          }
        }
        if (loadContext.Source == ConfigSource.Import && collection != null && loadContext.ImportContext != null && loadContext.ImportContext.OverrideElementsWithSameOrigin)
        {
          bool suppressChangeTracking = collection.SuppressChangeTracking;
          try
          {
            collection.SuppressChangeTracking = true;
            foreach (string key in stringList1)
            {
              if (!stringList2.Contains(key))
              {
                ConfigElement elementToRemove = collection.GetElementByKey(key);
                if (elementToRemove != null && elementToRemove.Source != ConfigSource.Default && (loadContext.ImportContext.PathsOfElementsToOverride == null || loadContext.ImportContext.PathsOfElementsToOverride.Any<string>((Func<string, bool>) (p => elementToRemove.GetPath().StartsWith(p)))))
                  collection.Remove(elementToRemove);
              }
            }
          }
          finally
          {
            collection.SuppressChangeTracking = suppressChangeTracking;
          }
        }
      }
      return flag1;
    }

    private bool AttemptLoadCustomTypeElement(
      object element,
      XmlReader reader,
      LoadContext loadContext,
      bool isNew,
      bool validateTagName = false)
    {
      bool flag = true;
      if (!reader.IsEmptyElement)
      {
        MethodInfo methodInfo = (MethodInfo) null;
        Type type = element.GetType();
        string name;
        if (type.ImplementsGenericInterface(typeof (IDictionary<,>)))
        {
          name = "LoadIDictionaryElement";
        }
        else
        {
          if (!type.ImplementsGenericInterface(typeof (ICollection<>)))
            return XmlConfigProvider.ThrowUnknownElementException(reader);
          name = "LoadICollectionElement";
        }
        if (name != null)
          methodInfo = typeof (XmlConfigProvider).GetMethod(name, BindingFlags.Static | BindingFlags.NonPublic).MakeGenericMethod(type.GetGenericArguments());
        while (XmlConfigProvider.ReadNextTag(reader) && reader.NodeType != XmlNodeType.EndElement)
        {
          if (methodInfo != (MethodInfo) null)
            flag = (bool) methodInfo.Invoke((object) null, new object[2]
            {
              element,
              (object) reader
            });
        }
      }
      return flag;
    }

    private static bool LoadICollectionElement<T>(ICollection<T> collection, XmlReader reader)
    {
      T obj = (T) XmlConfigProvider.LoadAttributeValue(reader, "value", typeof (T));
      if (reader.Name == "remove")
        collection.Remove(obj);
      else
        collection.Add(obj);
      return true;
    }

    private static bool LoadIDictionaryElement<TKey, TValue>(
      IDictionary<TKey, TValue> dictionary,
      XmlReader reader)
    {
      TKey key = (TKey) XmlConfigProvider.LoadAttributeValue(reader, "key", typeof (TKey));
      if (reader.Name == "remove")
        dictionary.Remove(key);
      else
        dictionary[key] = (TValue) XmlConfigProvider.LoadAttributeValue(reader, "value", typeof (TValue));
      return true;
    }

    private bool LoadPropertyElement(
      ConfigElement element,
      XmlReader reader,
      LoadContext loadContext)
    {
      ConfigProperty prop;
      if (!element.Properties.TryGetValue(reader.Name, out prop))
        return element.ThrowInvalidPropetyName(reader.Name);
      object element1 = element[prop];
      if (element1 == null)
        return XmlConfigProvider.ThrowUnknownElementException(reader);
      if (!(element1 is ConfigElement element2))
        return this.AttemptLoadCustomTypeElement(element1, reader, loadContext, true);
      Type actualType = XmlConfigProvider.LoadElementType(reader);
      if (actualType != (Type) null)
      {
        element2 = (ConfigElement) ConfigUtils.CreateInstance(element2.GetType(), actualType, (object) element);
        element2.Source = element.Source;
        element[prop] = (object) element2;
      }
      return this.LoadElement(element2, reader, loadContext, true);
    }

    private ConfigElement GetElementKeyWhenUpgrading(
      ConfigElementCollection collection,
      string key,
      Version upgradeFrom)
    {
      if (upgradeFrom.Build < 1371 && collection.ElementType.IsAssignableFrom(typeof (FieldControlDefinitionElement)))
      {
        int length = key.IndexOf('.');
        if (length > 0 && !key.StartsWith("$"))
          key = key.Substring(0, length);
      }
      return collection.GetElementByKey(key);
    }

    private bool LoadCollectionElement(
      ConfigElementCollection collection,
      XmlReader reader,
      LoadContext loadContext,
      out bool skipNextRead,
      out ConfigElement element)
    {
      skipNextRead = false;
      element = (ConfigElement) null;
      string keyName = collection.GetKeyName();
      bool flag1 = reader.LocalName == "link" && reader.NamespaceURI == "urn:telerik:sitefinity:configuration";
      bool flag2 = reader.LocalName == "lazy" && reader.NamespaceURI == "urn:telerik:sitefinity:configuration";
      bool flag3 = collection.AddElementName == reader.Name || collection.KeepRemoveItems;
      bool flag4 = (XmlConfigProvider.LoadFlags(reader) & XmlConfigProvider.Flags.Edit) > XmlConfigProvider.Flags.None;
      string originArray = XmlConfigProvider.LoadOrigin(reader);
      if (!originArray.IsNullOrWhitespace() && this.AddonDeleted(originArray))
      {
        loadContext.SaveNeeded = true;
        reader.ReadOuterXml();
        skipNextRead = true;
        return true;
      }
      if (flag1)
      {
        string attribute1 = reader.GetAttribute("path");
        if (attribute1 == null)
          return false;
        string attribute2 = reader.GetAttribute("module");
        string attribute3 = reader.GetAttribute(keyName);
        collection.AddLinkedElement((object) attribute3, attribute1, attribute2);
        collection.GetItemByKey(attribute3).ItemProperties["configSource"] = (object) loadContext.Source;
        return true;
      }
      bool flag5 = false;
      if (flag3 | flag2 | flag4)
      {
        bool isNew = false;
        string str = (string) null;
        if (flag2)
          str = reader.GetAttribute("key");
        else if (!string.IsNullOrEmpty(keyName))
          str = reader.GetAttribute(keyName);
        else if (loadContext.ImportContext != null || loadContext.UpgradeContext != null && loadContext.UpgradeContext.UpgradeFrom.Build < SitefinityVersion.Sitefinity9_0.Build)
        {
          Type type = XmlConfigProvider.LoadElementType(reader);
          ConfigElement element1 = collection.CreateNew(type);
          if (element1 is IKeyLessElement)
          {
            this.LoadElement(element1, reader, loadContext, true);
            string hash = ((IKeyLessElement) element1).GetHash();
            element = collection.FirstOrDefault<ConfigElement>((Func<ConfigElement, bool>) (e => hash.Equals(((IKeyLessElement) e).GetHash())));
            if (element != null)
              return true;
            element = element1;
            isNew = true;
            flag5 = true;
          }
        }
        else
        {
          str = XmlConfigProvider.LoadDynamicKey(reader);
          if (string.IsNullOrEmpty(str) && loadContext.Source == ConfigSource.Import && collection.Parent.Source == ConfigSource.Default)
            return true;
        }
        if (!string.IsNullOrEmpty(str))
        {
          if (loadContext.UpgradeContext != null)
          {
            element = this.GetElementKeyWhenUpgrading(collection, str, loadContext.UpgradeContext.UpgradeFrom);
            if (element == null & flag4)
              return true;
          }
          else if (!flag2 || loadContext.LoadLazyElements)
          {
            element = collection.GetElementByKey(str);
            if (element == null & flag4)
            {
              string xmlString = reader.ReadOuterXml();
              if (collection.IsDynamic)
                collection.RegisterInactiveElement(xmlString);
              skipNextRead = true;
              return true;
            }
          }
        }
        if (element == null)
        {
          if (!flag2)
          {
            Type type;
            try
            {
              type = XmlConfigProvider.LoadElementType(reader);
            }
            catch (Exception ex)
            {
              string elementXml = reader.ReadOuterXml();
              collection.AddFailedElementItem(str, ex.Message, elementXml);
              Exception exceptionToHandle = (Exception) new ConfigurationErrorsException(string.Format("Failed to load element type of '{0}{1}{2}': {3}.", (object) collection.GetPath(), (object) '/', (object) str, (object) ex.Message), ex);
              if (Exceptions.HandleException(exceptionToHandle, ExceptionPolicyName.IgnoreExceptions))
                throw exceptionToHandle;
              skipNextRead = true;
              return true;
            }
            element = collection.CreateNew(type);
            isNew = true;
          }
          else
            isNew = !collection.Contains(str);
          if (isNew && element != null)
            element.Source = this.GetCollectionElementSource(loadContext, true);
        }
        bool flag6;
        if (!flag2)
        {
          if (element == null && loadContext.UpgradeContext != null)
            return true;
          element.CollectionItemName = reader.Name;
          flag6 = flag5 || this.LoadElement(element, reader, loadContext, isNew);
          if (isNew)
          {
            element.Source = this.GetCollectionElementSource(loadContext, true);
            this.SetOriginOnImport(element, loadContext);
            collection.Add(element);
          }
        }
        else
        {
          string relativeFilePath = reader.GetAttribute("configFile");
          object valueFromString = ConfigElement.GetValueFromString(str, collection.GetKeyProperty());
          Func<XmlReader, ConfigElement> elementFactory;
          if (isNew)
          {
            elementFactory = (Func<XmlReader, ConfigElement>) (xmlReader =>
            {
              ConfigElement configElement = collection.CreateNew(XmlConfigProvider.LoadElementType(xmlReader));
              configElement.Source = this.GetCollectionElementSource(loadContext, true);
              return configElement;
            });
          }
          else
          {
            Func<ConfigElement> baseInitializer = ((ILazyConfigElementItem) collection.GetItemByKey(str)).GetBaseInitializer();
            elementFactory = (Func<XmlReader, ConfigElement>) (XmlReader => baseInitializer());
          }
          Func<ConfigElement> func = (Func<ConfigElement>) (() => this.LoadElement(relativeFilePath, elementFactory, loadContext, collection.Section.IsDefaultConfig));
          if (isNew)
          {
            collection.AddLazyInternal(valueFromString, func, false, new KeyValuePair<string, object>("configFile", (object) relativeFilePath), new KeyValuePair<string, object>("configSource", (object) loadContext.Source));
          }
          else
          {
            IConfigElementItem itemByKey = collection.GetItemByKey(str);
            itemByKey.ItemProperties["configFile"] = (object) relativeFilePath;
            itemByKey.Unload(func);
            ((ILazyConfigElementItem) itemByKey).AddSource(loadContext.Source);
          }
          if (loadContext.UpgradeContext != null || loadContext.LoadLazyElements)
            collection.GetElementByKey(str);
          flag6 = true;
        }
        return flag6;
      }
      if (collection.ClearElementName == reader.Name)
      {
        collection.Clear();
        collection.SetChangeSource(loadContext.Source, collection.ClearElementName);
        return true;
      }
      if (!(collection.RemoveElementName == reader.Name))
        return XmlConfigProvider.ThrowUnknownElementException(reader);
      if (!reader.IsEmptyElement)
        throw new ProviderException("Non-empty remove element.");
      string key = string.IsNullOrEmpty(keyName) ? XmlConfigProvider.LoadDynamicKey(reader) : reader.GetAttribute(keyName);
      ConfigElement configElement1 = !string.IsNullOrEmpty(key) ? collection.GetElementByKey(key) : throw new ProviderException("Key in a remove operation is either missing or empty.");
      if (configElement1 == null)
        return true;
      collection.SetChangeSource(loadContext.Source, collection.RemoveElementName, key);
      collection.Remove(configElement1);
      return true;
    }

    private ConfigSource GetCollectionElementSource(LoadContext loadContext, bool isNew) => ((!XmlConfigProvider.IsMigrationMode ? 0 : (this.StorageMode == ConfigStorageMode.FileSystem ? 1 : 0)) & (isNew ? 1 : 0)) != 0 ? ConfigSource.Database : loadContext.Source;

    private void LoadAttributes(
      ConfigElement element,
      XmlReader reader,
      LoadContext loadContext,
      bool isNew)
    {
      col = (NameValueCollection) null;
      while (reader.MoveToNextAttribute())
      {
        if (!(reader.Prefix == "xmlns") && !(reader.NamespaceURI == "urn:telerik:sitefinity:configuration") && !(reader.NamespaceURI == "urn:telerik:sitefinity:configuration:type"))
        {
          ConfigProperty configProperty;
          element.Properties.TryGetValue(reader.Name, out configProperty);
          if (configProperty != null)
          {
            object obj;
            if (configProperty.Type == typeof (Type))
            {
              obj = (object) new LazyValue(reader.Value, configProperty);
            }
            else
            {
              Type propertyActualType = XmlConfigProvider.GetPropertyActualType(reader, configProperty.Name, configProperty.Type);
              string resolver;
              string stringValue = this.GetStringValue(reader, out resolver);
              if (resolver != null)
              {
                obj = (object) new SecretValue(stringValue, configProperty, resolver);
              }
              else
              {
                try
                {
                  obj = ConfigElement.GetValueFromString(stringValue, propertyActualType, configProperty);
                }
                catch (NotSupportedException ex)
                {
                  throw new ConfigurationErrorsException(string.Format("Cannot convert attribute \"{0}\" to type \"{1}\". {2}", (object) reader.Name, (object) propertyActualType.FullName, (object) XmlConfigProvider.GetLineNumber(reader)), (Exception) ex);
                }
              }
            }
            element.SetPersistedRawValue(configProperty, obj, loadContext.Source);
          }
          else if (col != null || element.Properties.TryGetValue("parameters", out configProperty) && configProperty != null && typeof (NameValueCollection).IsAssignableFrom(configProperty.Type))
          {
            if (col == null && !(element[configProperty] is NameValueCollection col))
            {
              col = new NameValueCollection();
              element[configProperty] = (object) col;
            }
            string resolver;
            string stringValue = this.GetStringValue(reader, out resolver);
            if (resolver != null)
            {
              if (!(col is SecretNameValueCollection nameValueCollection))
              {
                nameValueCollection = new SecretNameValueCollection(col);
                col = (NameValueCollection) nameValueCollection;
              }
              nameValueCollection.SetSecretValue(reader.Name, stringValue, resolver);
            }
            else
              col[reader.Name] = stringValue;
          }
          else if (loadContext.UpgradeContext == null)
            throw new ConfigurationErrorsException(string.Format("Invalid attribute \"{0}\" for element \"{1}\". {2}", (object) reader.Name, (object) element.GetType().FullName, (object) XmlConfigProvider.GetLineNumber(reader)));
        }
      }
      if (col == null)
        return;
      element.SetPersistedRawValue("parameters", (object) col, loadContext.Source);
    }

    private static Type GetPropertyActualType(
      XmlReader reader,
      string propertyName,
      Type definedType)
    {
      Type c = XmlConfigProvider.LoadElementType(reader, propertyName);
      return c != (Type) null && definedType.IsAssignableFrom(c) ? c : definedType;
    }

    private static object LoadAttributeValue(
      XmlReader reader,
      string attributeName,
      Type definedType)
    {
      Type propertyActualType = XmlConfigProvider.GetPropertyActualType(reader, attributeName, definedType);
      try
      {
        return ConfigElement.GetValueFromString(reader.GetAttribute(attributeName), propertyActualType);
      }
      catch (NotSupportedException ex)
      {
        throw new ConfigurationErrorsException(string.Format("Cannot convert attribute \"{0}\" to type \"{1}\". {2}", (object) attributeName, (object) propertyActualType.FullName, (object) XmlConfigProvider.GetLineNumber(reader)), (Exception) ex);
      }
    }

    private static Type LoadElementType(XmlReader reader, string propertyName = null)
    {
      string name = propertyName != null ? propertyName : "this";
      string attribute = reader.GetAttribute(name, "urn:telerik:sitefinity:configuration:type");
      return !string.IsNullOrEmpty(attribute) ? TypeResolutionService.ResolveType(attribute) : (Type) null;
    }

    private static bool ReadNextTag(XmlReader reader, int depth = 0)
    {
      while (reader.Read())
      {
        if ((reader.NodeType == XmlNodeType.Element || reader.NodeType == XmlNodeType.EndElement) && (depth == 0 || reader.Depth <= depth))
          return true;
      }
      return false;
    }

    /// <summary>
    /// Gets the file path based on the configuration section from which the configuration should be read.
    /// </summary>
    /// <param name="section">The Configuration Section for which the file path should be returned.</param>
    /// <returns></returns>
    protected virtual string GetFilePath(ConfigElement element, bool useElementPath = false) => Path.Combine(this.storageFolder, this.GetRelativeFilePath(element, useElementPath));

    protected virtual string GetRelativeFilePath(ConfigElement element, bool useElementPath)
    {
      string fileName;
      if (useElementPath)
      {
        if (!element.TryGenerateFileName(out fileName))
        {
          fileName = element.GetPath().Replace('/', '.');
          if (fileName.Length > 0)
            fileName = char.ToUpperInvariant(fileName[0]).ToString() + fileName.Substring(1);
        }
      }
      else
        fileName = element.GetType().Name;
      fileName += this.fileExtension;
      return fileName;
    }

    /// <summary>Gets the configuration reader.</summary>
    /// <param name="section">The section.</param>
    /// <returns></returns>
    protected virtual XmlReader GetConfigurationReader(ConfigElement element) => this.GetConfigurationReader(this.GetFilePath(element));

    protected virtual XmlReader GetConfigurationReader(string path)
    {
      if (File.Exists(path))
      {
        string s = (string) null;
        int num = 0;
        while (num < 10)
        {
          try
          {
            s = File.ReadAllText(path);
            break;
          }
          catch (Exception ex)
          {
            ++num;
            Thread.Sleep(300);
          }
        }
        if (s != null)
          return (XmlReader) new XmlTextReader((TextReader) new StringReader(s));
      }
      return (XmlReader) null;
    }

    /// <summary>Gets the XML writer.</summary>
    /// <param name="section">The section.</param>
    /// <returns></returns>
    protected virtual XmlWriter GetConfigurationWriter(string path)
    {
      string directoryName = Path.GetDirectoryName(path);
      if (!Directory.Exists(directoryName))
        Directory.CreateDirectory(directoryName);
      XmlWriterSettings settings = new XmlWriterSettings()
      {
        Indent = true,
        IndentChars = "\t"
      };
      return XmlWriter.Create(path, settings);
    }

    /// <summary>Should save file.</summary>
    /// <param name="xml">The XML content.</param>
    /// <param name="path">The file path.</param>
    /// <returns></returns>
    protected virtual bool ShouldSaveFile(XmlDocument xml, string path)
    {
      bool flag = true;
      if (!File.Exists(path))
      {
        XmlElement documentElement = xml.DocumentElement;
        if (documentElement.ChildNodes.Count == 0 && documentElement.Attributes.Count == 0)
          flag = false;
      }
      return flag;
    }

    /// <summary>Initializes the provider.</summary>
    /// <param name="providerName">The friendly name of the provider.</param>
    /// <param name="config">
    /// A collection of the name/value pairs representing the provider-specific attributes specified in the configuration for this provider.
    /// </param>
    /// <param name="managerType">
    /// The type of the manger initialized this provider.
    /// </param>
    protected internal override void Initialize(
      string providerName,
      NameValueCollection config,
      Type managerType)
    {
      base.Initialize(providerName, config, managerType);
      this.fileExtension = config["fileExtension"];
      if (string.IsNullOrEmpty(this.fileExtension))
        this.fileExtension = ".config";
      config.Remove("fileExtension");
      this.storageFolder = config["storageFolder"];
      if (string.IsNullOrEmpty(this.storageFolder))
        this.storageFolder = Telerik.Sitefinity.Configuration.Config.SectionHandler.Settings.StorageFolder;
      config.Remove("storageFolder");
      if (HostingEnvironment.IsHosted)
      {
        this.storageFolder = HostingEnvironment.MapPath(this.storageFolder);
      }
      else
      {
        string str = this.storageFolder;
        if (str.StartsWith("~/"))
          str = str.Substring(2);
        this.storageFolder = Path.Combine(SystemManager.AppDataFolderPhysicalPath, str.Replace('/', '\\'));
      }
      string str1 = config["storageMode"];
      this.storageMode = string.IsNullOrEmpty(str1) ? Telerik.Sitefinity.Configuration.Config.SectionHandler.Settings.StorageMode : (ConfigStorageMode) Enum.Parse(typeof (ConfigStorageMode), str1);
      config.Remove("storageMode");
      if (this.StorageMode == ConfigStorageMode.FileSystem && !XmlConfigProvider.IsMigrationMode)
        return;
      config["applicationName"] = this.ApplicationName;
      this.storageProvider = (IXmlConfigStorageProvider) new OpenAccessXmlConfigStorageProvider();
      this.storageProvider.Initialize(this.Name, config, managerType);
    }

    /// <summary>
    /// The version of the <see cref="T:Telerik.Sitefinity.Configuration.Data.XmlConfigProvider" />, which is actually the version of the Telerik.Sitefinity assembly.
    /// </summary>
    protected internal Version Version => this.GetType().Assembly.GetName().Version;

    /// <summary>Gets the specified permission.</summary>
    /// <param name="permissionSet">The permission set.</param>
    /// <param name="objectId">The object pageId.</param>
    /// <param name="principalId">The principal pageId.</param>
    /// <returns></returns>
    public override Permission GetPermission(
      string permissionSet,
      Guid objectId,
      Guid principalId)
    {
      throw new NotSupportedException();
    }

    /// <summary>Gets a query for permissions.</summary>
    /// <returns></returns>
    public override IQueryable<Permission> GetPermissions() => throw new NotSupportedException();

    public override Permission CreatePermission(
      string permissionSet,
      Guid objectId,
      Guid principalId)
    {
      throw new NotImplementedException();
    }

    /// <summary>Deletes the permission.</summary>
    /// <param name="permission">The permission.</param>
    public override void DeletePermission(Permission permission) => throw new NotSupportedException();

    /// <summary>Gets the security root.</summary>
    /// <returns></returns>
    public override ISecuredObject GetSecurityRoot() => throw new NotSupportedException();

    /// <summary>Gets the security root.</summary>
    /// <param name="create">if set to <c>true</c> a security root will be created for the provider if there is no root.</param>
    /// <returns></returns>
    public override ISecuredObject GetSecurityRoot(bool create) => throw new NotSupportedException();

    private static string GetLineNumber(XmlReader reader) => reader is XmlTextReader xmlTextReader ? string.Format("Line number {0}.", (object) xmlTextReader.LineNumber) : string.Empty;

    private static bool ThrowUnknownElementException(XmlReader reader)
    {
      Exception exceptionToHandle = (Exception) new ConfigurationErrorsException(string.Format("Unknown element \"{0}\". {1}", (object) reader.Name, (object) XmlConfigProvider.GetLineNumber(reader)));
      if (Exceptions.HandleException(exceptionToHandle, ExceptionPolicyName.IgnoreExceptions))
        throw exceptionToHandle;
      return false;
    }

    private void SetOriginOnLoad(
      ConfigElement element,
      XmlReader reader,
      LoadContext loadContext,
      bool isNew)
    {
      if (!isNew || element == null || ((IEnumerable<string>) this.systemKeys).Contains<string>(element.GetKey()) || element is IKeyLessElement)
        return;
      string originArray = XmlConfigProvider.LoadOrigin(reader);
      if (originArray.IsNullOrWhitespace() || !this.AddonExists(originArray))
        return;
      element.Origin = originArray;
    }

    private void SetOriginOnImport(ConfigElement element, LoadContext loadContext)
    {
      if (loadContext.Source != ConfigSource.Import || element == null || !string.IsNullOrWhiteSpace(element.Origin) || ((IEnumerable<string>) this.systemKeys).Contains<string>(element.GetKey()) || element is IKeyLessElement)
        return;
      element.Origin = loadContext.ImportContext.Origin;
    }

    private bool AddonExists(string originArray)
    {
      try
      {
        AddonOrigin addonOrigin = this.GetAddonOrigin(originArray);
        return addonOrigin == null || PackagingManager.InstalledAddons.Contains<string>(addonOrigin.Name);
      }
      catch (Exception ex)
      {
        if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
          throw ex;
        return false;
      }
    }

    private bool AddonDeleted(string originArray)
    {
      try
      {
        AddonOrigin addonOrigin = this.GetAddonOrigin(originArray);
        return addonOrigin != null && !PackagingManager.InstalledAddons.Contains<string>(addonOrigin.Name);
      }
      catch (Exception ex)
      {
        if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
          throw ex;
        return false;
      }
    }

    private AddonOrigin GetAddonOrigin(string originArray)
    {
      OriginWrapperObject origin = OriginWrapperObject.ParseJsonArray(originArray).FirstOrDefault<OriginWrapperObject>();
      AddonOrigin addonOrigin = (AddonOrigin) null;
      if (origin != null && AddonOrigin.IsAddonOrigin(origin))
        addonOrigin = AddonOrigin.Parse(origin);
      return addonOrigin;
    }

    private static bool IsMigrationMode
    {
      get
      {
        bool result = false;
        bool.TryParse(ConfigurationSettings.AppSettings["sf:IsMigrationMode"], out result);
        return result;
      }
    }

    protected internal delegate XmlElement XmlElementFactory(
      XmlDocument xmlDoc,
      string tagName);

    [System.Flags]
    private enum Flags
    {
      None = 0,
      Edit = 1,
      HasSiteSpecificValuesPersisted = 2,
    }

    private class MigrationStorageModeRegion : IDisposable
    {
      private readonly ConfigStorageMode previousStorageMode;
      private readonly XmlConfigProvider provider;

      public MigrationStorageModeRegion(XmlConfigProvider provider, ConfigStorageMode storageMode = ConfigStorageMode.Auto)
      {
        this.previousStorageMode = provider.StorageMode;
        this.provider = provider;
        this.provider.storageMode = storageMode;
      }

      public void Dispose() => this.provider.storageMode = this.previousStorageMode;
    }
  }
}
