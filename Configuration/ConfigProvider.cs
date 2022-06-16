// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.ConfigProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Microsoft.Practices.Unity.InterceptionExtension;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration.Data;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Packaging.Package;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Configuration
{
  /// <summary>
  /// Represents base class for configuration data providers.
  /// IMPORTANT! This data provider is not intercepted by security and event policies, for performance reasons,
  /// therefore security should be handled manually in the actual provider implementations.
  /// </summary>
  [ApplyNoPolicies]
  public abstract class ConfigProvider : DataProviderBase
  {
    /// <summary>
    /// Gets the storage mode.
    /// Specifies how to read/write the configuration.
    /// </summary>
    /// <value>The storage mode.</value>
    public abstract ConfigStorageMode StorageMode { get; }

    /// <summary>Gets a unique key for each data provider base.</summary>
    /// <value></value>
    public override string RootKey => nameof (ConfigProvider);

    /// <summary>Gets all registered configuration section classes.</summary>
    /// <returns>A string array of class names.</returns>
    public virtual ConfigSection[] GetAllConfigSections()
    {
      this.DemandViewPermission();
      List<ConfigSection> configSectionList = new List<ConfigSection>();
      foreach (RegisterEventArgs registerEventArgs in ObjectFactory.GetArgsForType(typeof (ConfigSection)))
      {
        if (registerEventArgs.Name == registerEventArgs.TypeTo.Name)
          configSectionList.Add((ConfigSection) ObjectFactory.Resolve(registerEventArgs.TypeTo));
      }
      return configSectionList.ToArray();
    }

    /// <summary>Gets configuration section for the specified type.</summary>
    /// <typeparam name="TSection">The type of the configuration section.</typeparam>
    /// <returns>Configuration section.</returns>
    public virtual TSection GetSection<TSection>() where TSection : ConfigSection, new()
    {
      this.DemandViewPermission();
      TSection section = new TSection();
      section.Initialize(this);
      return section;
    }

    /// <summary>Gets configuration section for the specified type.</summary>
    /// <param name="sectionName">The name of the section.</param>
    /// <returns>Configuration section.</returns>
    public virtual ConfigSection GetSection(string sectionName)
    {
      this.DemandViewPermission();
      Type namedType = ObjectFactory.GetNamedType(sectionName, typeof (ConfigSection));
      ConfigSection section = !(namedType == (Type) null) ? (ConfigSection) Activator.CreateInstance(namedType) : throw new ArgumentException("Invalid section name.", nameof (sectionName));
      section.Initialize(this);
      return section;
    }

    /// <summary>
    /// Loads external settings and overrides the default values of the provided section.
    /// </summary>
    /// <param name="section">The section which should be populated with external settings.</param>
    /// <returns></returns>
    public abstract bool LoadSection(ConfigSection section);

    /// <summary>
    /// Exports all settings that are different from the default values to external resource.
    /// </summary>
    /// <param name="section">The configuration section to be exported.</param>
    public abstract void SaveSection(ConfigSection section);

    /// <summary>Exports configuration section as string</summary>
    /// <param name="section">Configuration section to be exported</param>
    /// <param name="skipLoadFromFile">Defines whether to skip loading the default configurations from the file system</param>
    /// <returns>The content of exported section as xml string</returns>
    public virtual string Export(ConfigSection section) => string.Empty;

    /// <summary>Exports the specified configuration elements.</summary>
    /// <param name="configElementsToBeExported">The configuration elements to be exported.</param>
    /// <param name="skipLoadFromFile">Defines whether to skip loading the default configurations from the file system</param>
    /// <param name="exportMode">The export mode.</param>
    /// <returns>Specified configuration elements as XML</returns>
    internal virtual string Export(
      IEnumerable<ConfigElement> configElementsToBeExported,
      bool skipLoadFromFile = false,
      ExportMode exportMode = ExportMode.Default)
    {
      return string.Empty;
    }

    /// <summary>Exports configuration section as string</summary>
    /// <param name="section">Configuration section to be exported</param>
    /// <param name="skipLoadFromFile">Defines whether to skip loading the default configurations from the file system</param>
    /// <returns>The content of exported section as xml string</returns>
    public virtual string Export(ConfigSection section, bool skipLoadFromFile) => string.Empty;

    /// <summary>Imports xml string to specified section</summary>
    /// <param name="sectionType">Section where the xml string to be imported in</param>
    /// <param name="xml">Content as xml string</param>
    public virtual void Import(Type sectionType, string xml, bool saveInFileSystemMode = false)
    {
    }

    /// <summary>Imports xml string to specified section</summary>
    /// <param name="sectionType">Section where the xml string to be imported in</param>
    /// <param name="xml">Content as xml string</param>
    /// <param name="origin">The origin of the XML.</param>
    /// <param name="saveInFileSystemMode">if set to <c>true</c> exports the configuration to file system, if <c>false</c> exports the configuration according the storage mode of the system.</param>
    /// <param name="overrideOrigin">if set to <c>true</c> items from the same origin will be overridden.</param>
    /// <param name="pathsOfElementsToOverride">The paths of the elements to override. If null, all elements with same origin will be overriden.</param>
    internal virtual void Import(
      Type sectionType,
      string xml,
      string origin,
      bool saveInFileSystemMode = false,
      bool overrideOrigin = false,
      IEnumerable<string> pathsOfElementsToOverride = null)
    {
    }

    /// <summary>Migrate the section to AUTO storage mode.</summary>
    /// <param name="section">The configuration section to be migrated.</param>
    internal abstract void MigrateSection(ConfigSection section);

    /// <summary>
    /// Move the config element to different persistance location.
    /// </summary>
    /// <param name="section">The configuration section to be moved.</param>
    /// <param name="target">The storage destination.</param>
    internal abstract void MoveElement(ConfigElement element, ConfigSource target);

    protected internal virtual bool EnsureNormalMode(ConfigSection section) => true;

    protected internal virtual bool LoadSectionFromFile(ConfigSection section) => false;

    internal virtual bool LoadSection(ConfigSection section, LoadContext loadContext) => false;

    protected internal virtual bool RestoreSection(ConfigSection section) => false;

    protected internal virtual IXmlConfigStorageProvider GetDatabaseStorageProvider() => (IXmlConfigStorageProvider) null;

    /// <summary>Creates new data item.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="pageId">The pageId.</param>
    /// <returns></returns>
    public override object CreateItem(Type itemType, Guid id) => throw new NotSupportedException();

    /// <summary>
    /// Gets the data item with the specified ID.
    /// An exception should be thrown if an item with the specified ID does not exist.
    /// </summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="pageId">The ID of the item to return.</param>
    /// <returns></returns>
    public override object GetItem(Type itemType, Guid id) => throw new NotSupportedException();

    /// <summary>
    /// Get item by primary key without throwing exceptions if it doesn't exist
    /// </summary>
    /// <param name="itemType">Type of the item to get</param>
    /// <param name="id">Primary key</param>
    /// <returns>Item or default value</returns>
    public override object GetItemOrDefault(Type itemType, Guid id) => throw new NotSupportedException();

    /// <summary>Gets the items.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="orderExpression">The order expression.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <param name="totalCount">Totlal count of the items that are filtered by <paramref name="filterExpression" /></param>
    /// <returns></returns>
    public override IEnumerable GetItems(
      Type itemType,
      string filterExpression,
      string orderExpression,
      int skip,
      int take,
      ref int? totalCount)
    {
      throw new NotSupportedException();
    }

    /// <summary>
    /// Marks the provided persistent item for deletion.
    /// The item is deleted form the storage when the transaction is committed.
    /// </summary>
    /// <param name="item">The item to be deleted.</param>
    public override void DeleteItem(object item) => throw new NotSupportedException();

    /// <summary>Get a list of types served by this manager</summary>
    /// <returns></returns>
    public override Type[] GetKnownTypes() => new Type[2]
    {
      typeof (ConfigSection),
      typeof (ConfigElement)
    };

    /// <summary>
    /// Helper method for demanding view permission for configuration
    /// </summary>
    protected virtual void DemandViewPermission()
    {
    }

    /// <summary>
    /// Helper method for demanding manage permission for configuration
    /// </summary>
    protected virtual void DemandManagePermission()
    {
      if (this.SuppressPermissions)
        return;
      AppPermission.Root.Demand("Backend", "ChangeConfigurations");
    }

    protected internal virtual bool SuppressPermissions
    {
      get
      {
        int num1 = ConfigProvider.DisableSecurityChecks ? 1 : (this.SuppressSecurityChecks ? 1 : 0);
        bool initializing = SystemManager.Initializing;
        bool registering = ObjectFactory.Registering;
        int num2 = initializing ? 1 : 0;
        return (num1 | num2 | (registering ? 1 : 0)) != 0;
      }
    }

    internal static bool DisableSecurityChecks { get; set; }
  }
}
