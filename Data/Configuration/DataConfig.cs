// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Configuration.DataConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Web;
using System.Web.Hosting;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Configuration.Environment;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Processors.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.DataResolving;
using Telerik.Sitefinity.Web.UrlEvaluation;

namespace Telerik.Sitefinity.Data.Configuration
{
  /// <summary>Configuration section for data related configurations</summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "DataConfigDescription", Title = "DataConfigTitle")]
  public class DataConfig : ConfigSection, IHaveConfigProcessors
  {
    private bool? hasSitefinityConnection;
    /// <summary>
    /// Default connection string name of the Sitefinity database.
    /// </summary>
    public const string DefaultConnectionName = "Sitefinity";
    private const string DummyConnectionsKeyPrefix = "Sitefinity.DummyConnections.";
    private Dictionary<string, IConnectionStringSettings> dummyConnectionStrings;

    /// <summary>
    /// Gets a <see cref="T:System.Boolean" /> value indicating if this instance have been initialized.
    /// </summary>
    /// <value></value>
    [ConfigurationProperty("initialized", DefaultValue = false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [Obsolete("Use Bootstrapper.IsReady.")]
    public new bool Initialized
    {
      get => (bool) this["initialized"];
      set => this["initialized"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the enable data caching.
    /// If not explicitly set caching is turned off for Load balancing mode and turned on for Single instance mode.
    /// </summary>
    /// <value>The enable data caching.</value>
    [ConfigurationProperty("enableDataCaching")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EnableDataCachingDescription", Title = "EnableDataCachingTitle")]
    public bool? EnableDataCaching
    {
      get => (bool?) this["enableDataCaching"];
      set => this["enableDataCaching"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the GUID generation strategy used by OpenAccessDecorator in OpenAccess data providers.
    /// </summary>
    /// <value>The GUID generation strategy.</value>
    [ObjectInfo(typeof (ConfigDescriptions), Description = "GuidGenerationStrategyDescription", Title = "GuidGenerationStrategyTitle")]
    [ConfigurationProperty("guidGenerationStrategy", DefaultValue = GuidGenerationStrategies.Random)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public GuidGenerationStrategies GuidGenerationStrategy
    {
      get => (GuidGenerationStrategies) this["guidGenerationStrategy"];
      set => this["guidGenerationStrategy"] = (object) value;
    }

    [ConfigurationProperty("incrementalGuidRange", DefaultValue = 0)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "IncrementalGuidRangeDescription", Title = "IncrementalGuidRangeTitle")]
    public byte IncrementalGuidRange
    {
      get => (byte) this["incrementalGuidRange"];
      set => this["incrementalGuidRange"] = (object) value;
    }

    [ConfigurationProperty("disableMetaTypeCache")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DisableMetaTypeCacheDescription", Title = "DisableMetaTypeCacheTitle")]
    public bool DisableMetaTypeCache
    {
      get => (bool) this["disableMetaTypeCache"];
      set => this["disableMetaTypeCache"] = (object) value;
    }

    [ConfigurationProperty("databaseMappingOptions")]
    [ObjectInfo(typeof (ConfigDescriptions), Title = "DatabaseMappingOptionsTitle")]
    public DatabaseMappingOptionsElement DatabaseMappingOptions => (DatabaseMappingOptionsElement) this["databaseMappingOptions"];

    /// <summary>
    /// Gets the collection of the connection strings declared in the Sitefinity configuration
    /// </summary>
    [ConfigurationProperty("connectionStrings")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DataConfigConnectionStringsDescription", Title = "DataConfigConnectionStringsTitle")]
    public ConfigElementDictionary<string, ConnStringSettings> ConnectionStrings => (ConfigElementDictionary<string, ConnStringSettings>) this["connectionStrings"];

    /// <summary>Gets the configured data resolvers.</summary>
    /// <value>The resolvers.</value>
    [ConfigurationProperty("resolvers")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ResolversDescription", Title = "ResolversTitle")]
    public ConfigElementDictionary<string, DataProviderSettings> Resolvers => (ConfigElementDictionary<string, DataProviderSettings>) this["resolvers"];

    /// <summary>Gets the configured URL evaluators.</summary>
    /// <value>The URL evaluators.</value>
    [ConfigurationProperty("urlEvaluators")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "UrlEvaluatorsDescription", Title = "UrlEvaluatorsTitle")]
    public ConfigElementDictionary<string, DataProviderSettings> UrlEvaluators => (ConfigElementDictionary<string, DataProviderSettings>) this["urlEvaluators"];

    /// <summary>Gets a collection of DataProcessors settings.</summary>
    [ConfigurationProperty("dataProcessors")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DataProcessorsDescription", Title = "DataProcessorsTitle")]
    public virtual ConfigElementDictionary<string, ProcessorConfigElement> DataProcessors => (ConfigElementDictionary<string, ProcessorConfigElement>) this["dataProcessors"] ?? new ConfigElementDictionary<string, ProcessorConfigElement>();

    /// <summary>
    /// Called when the corresponding XML element is read and properties loaded.
    /// </summary>
    protected override void OnPropertiesInitialized()
    {
      string name = typeof (DataResources).Name;
      if (!this.Resolvers.ContainsKey("URL"))
        this.Resolvers.Add(new DataProviderSettings((ConfigElement) this.Resolvers)
        {
          Name = "URL",
          Description = "UrlResolverDescription",
          ResourceClassId = name,
          ProviderType = typeof (UrlResolver)
        });
      if (!this.Resolvers.ContainsKey("Author"))
        this.Resolvers.Add(new DataProviderSettings((ConfigElement) this.Resolvers)
        {
          Name = "Author",
          Description = "AuthorResolverDescription",
          ResourceClassId = name,
          ProviderType = typeof (AuthorResolver)
        });
      if (!this.UrlEvaluators.ContainsKey("Date"))
        this.UrlEvaluators.Add(new DataProviderSettings((ConfigElement) this.UrlEvaluators)
        {
          Name = "Date",
          Description = "DateEvaluatorDescription",
          ResourceClassId = name,
          ProviderType = typeof (DateEvaluator)
        });
      if (!this.UrlEvaluators.ContainsKey("PageNumber"))
        this.UrlEvaluators.Add(new DataProviderSettings((ConfigElement) this.UrlEvaluators)
        {
          Name = "PageNumber",
          Description = "PageNumberEvaluatorDescription",
          ResourceClassId = name,
          ProviderType = typeof (PageNumberEvaluator)
        });
      if (!this.UrlEvaluators.ContainsKey("DepartmentPageNumber"))
        this.UrlEvaluators.Add(new DataProviderSettings((ConfigElement) this.UrlEvaluators)
        {
          Name = "DepartmentPageNumber",
          Description = "DepartmentPageNumberEvaluatorDescription",
          ResourceClassId = name,
          ProviderType = typeof (DepartmentPageNumberEvaluator)
        });
      if (!this.UrlEvaluators.ContainsKey("ItemsPerPage"))
        this.UrlEvaluators.Add(new DataProviderSettings((ConfigElement) this.UrlEvaluators)
        {
          Name = "ItemsPerPage",
          Description = "ItemsPerPageEvaluatorDescription",
          ResourceClassId = name,
          ProviderType = typeof (ItemsPerPageEvaluator)
        });
      if (!this.UrlEvaluators.ContainsKey("Author"))
        this.UrlEvaluators.Add(new DataProviderSettings((ConfigElement) this.UrlEvaluators)
        {
          Name = "Author",
          Description = "AuthorEvaluatorDescription",
          ResourceClassId = name,
          ProviderType = typeof (AuthorEvaluator)
        });
      if (!this.UrlEvaluators.ContainsKey("Taxonomy"))
        this.UrlEvaluators.Add(new DataProviderSettings((ConfigElement) this.UrlEvaluators)
        {
          Name = "Taxonomy",
          Description = "TaxonomyEvaluatorDescription",
          ResourceClassId = name,
          ProviderType = typeof (TaxonomyEvaluator)
        });
      if (!this.UrlEvaluators.ContainsKey("Forms"))
        this.UrlEvaluators.Add(new DataProviderSettings((ConfigElement) this.UrlEvaluators)
        {
          Name = "Forms",
          Description = "FormsEvaluatorDescription",
          ResourceClassId = name,
          ProviderType = typeof (FormsEvaluator)
        });
      if (this.UrlEvaluators.ContainsKey("OrderGuid"))
        return;
      this.UrlEvaluators.Add(new DataProviderSettings((ConfigElement) this.UrlEvaluators)
      {
        Name = "OrderGuid",
        Description = "OrderGuidEvaluatorDescription",
        ResourceClassId = name,
        ProviderType = typeof (OrderGuidEvaluator)
      });
    }

    public override void Upgrade(Version oldVersion, Version newVersion)
    {
      base.Upgrade(oldVersion, newVersion);
      ConfigProperty prop1;
      PersistedValueWrapper valueWrapper1;
      if (oldVersion.Build >= 3900 && oldVersion.Build < SitefinityVersion.Sitefinity10_0.Build && this.Properties.TryGetValue("guidGenerationStrategy", out prop1) && ((GuidGenerationStrategies) this.GetRawValue(prop1, out valueWrapper1)).Equals(prop1.DefaultValue) && (valueWrapper1 == null || valueWrapper1.Source == ConfigSource.Default))
        this.GuidGenerationStrategy = GuidGenerationStrategies.Incremental;
      if (!(oldVersion < SitefinityVersion.Sitefinity11_1) || (this.UpgradeContext.Source != ConfigSource.Database || this.Provider.StorageMode != ConfigStorageMode.Database) && (this.UpgradeContext.Source != ConfigSource.FileSystem || this.Provider.StorageMode == ConfigStorageMode.Database))
        return;
      DatabaseMappingOptionsElement databaseMappingOptions = this.DatabaseMappingOptions;
      databaseMappingOptions.MainFieldsIgnoredCultures = "ALLCULTURES";
      databaseMappingOptions.UseMultilingualFetchStrategy = true;
      ConfigProperty prop2;
      PersistedValueWrapper valueWrapper2;
      if (databaseMappingOptions.Properties.TryGetValue("useMultilingualSplitTables", out prop2) && ((bool) databaseMappingOptions.GetRawValue(prop2, out valueWrapper2)).Equals(prop2.DefaultValue) && (valueWrapper2 == null || valueWrapper2.Source == ConfigSource.Default))
        databaseMappingOptions.UseMultilingualSplitTables = false;
      if (this.UpgradeContext.Source != ConfigSource.FileSystem || this.Provider.StorageMode == ConfigStorageMode.Database)
        return;
      OpenAccessConnection.UpgradeSplitTablesOptions(this);
    }

    internal bool IsInitalized() => this.HasSitefinityConnection;

    private bool HasSitefinityConnection
    {
      get
      {
        if (!this.hasSitefinityConnection.HasValue)
          this.hasSitefinityConnection = new bool?(this.TryGetConnectionString("Sitefinity", out IConnectionStringSettings _));
        return this.hasSitefinityConnection.Value;
      }
    }

    /// <summary>
    /// Tries to resolve the connections string from the DataConfig with fallback to the web.config file
    /// </summary>
    /// <param name="name">The name of the connection string.</param>
    /// <param name="connString">The results.</param>
    /// <returns></returns>
    public bool TryGetConnectionString(string name, out IConnectionStringSettings connString)
    {
      if (this.TryGetDummyConnectionString(name, out connString))
        return true;
      string connectionString1 = EnvironmentVariables.Current.GetConnectionString(name);
      if (connectionString1 != null)
      {
        this.AddStaticDummyConnection(name, (IConnectionStringSettings) new ExternalConnStringSettings(new ConnectionStringSettings(name, connectionString1)));
        if (this.TryGetDummyConnectionString(name, out connString))
          return true;
      }
      ConnStringSettings connStringSettings;
      if (this.ConnectionStrings.TryGetValue(name, out connStringSettings))
      {
        connString = (IConnectionStringSettings) connStringSettings;
        return true;
      }
      foreach (ConnectionStringSettings connectionString2 in (ConfigurationElementCollection) ConfigurationManager.ConnectionStrings)
      {
        if (connectionString2.Name.Equals(name))
        {
          connString = (IConnectionStringSettings) new ExternalConnStringSettings(connectionString2);
          this.AddStaticDummyConnection(name, connString);
          return true;
        }
      }
      connString = (IConnectionStringSettings) null;
      return false;
    }

    private bool TryGetDummyConnectionString(string name, out IConnectionStringSettings connString)
    {
      if (this.dummyConnectionStrings != null && this.dummyConnectionStrings.TryGetValue(name, out connString))
        return true;
      if (HostingEnvironment.IsHosted)
      {
        HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
        if (currentHttpContext != null)
        {
          string key = "Sitefinity.DummyConnections." + name;
          connString = currentHttpContext.Items[(object) key] as IConnectionStringSettings;
          if (connString != null)
          {
            this.AddStaticDummyConnection(name, connString);
            return true;
          }
        }
      }
      connString = (IConnectionStringSettings) null;
      return false;
    }

    internal void AddDummyConnection(string name, IConnectionStringSettings connString)
    {
      this.AddStaticDummyConnection(name, connString);
      if (!HostingEnvironment.IsHosted)
        return;
      HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
      if (currentHttpContext == null)
        return;
      string key = "Sitefinity.DummyConnections." + name;
      currentHttpContext.Items[(object) key] = (object) connString;
    }

    private void AddStaticDummyConnection(string name, IConnectionStringSettings connString)
    {
      if (this.dummyConnectionStrings == null)
        this.dummyConnectionStrings = new Dictionary<string, IConnectionStringSettings>();
      this.dummyConnectionStrings[name] = connString;
      this.hasSitefinityConnection = new bool?();
    }

    public ConfigElementDictionary<string, ProcessorConfigElement> GetConfigProcessors() => this.DataProcessors;

    internal class PropNames
    {
      internal const string GuidGenerationStrategy = "guidGenerationStrategy";
    }
  }
}
