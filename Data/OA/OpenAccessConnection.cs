// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.OA.OpenAccessConnection
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Timers;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Telerik.Microsoft.Practices.Unity;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Diagnostics;
using Telerik.OpenAccess.Exceptions;
using Telerik.OpenAccess.FetchOptimization;
using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.Metadata.Fluent;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.AppStatus;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Configuration.Data;
using Telerik.Sitefinity.Data.Configuration;
using Telerik.Sitefinity.Data.ContentLinks;
using Telerik.Sitefinity.Data.Decorators;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Descriptors;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.HealthMonitoring;
using Telerik.Sitefinity.Licensing;
using Telerik.Sitefinity.LoadBalancing;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Utilities;

namespace Telerik.Sitefinity.Data.OA
{
  /// <summary>
  /// 
  /// </summary>
  public class OpenAccessConnection : IDisposable
  {
    private static readonly System.Timers.Timer timer;
    private static readonly List<Database> trash = new List<Database>();
    internal const string SkipMappingAggregationOperationKey = "SkipMappingAggregation";
    internal const string FullCleanKey = "FullClean";
    private const string DropConstraintStatement = "DROP CONSTRAINT";
    private const string DropForeignKeyStatement = "DROP FOREIGN KEY";
    private static readonly char[] closingTags = new char[3]
    {
      '\'',
      ']',
      '"'
    };
    private const string DatabaseConnStringUniqueKeyPrefix = ";Backend=";
    private static string defaultSchemaUpgradeProviderName = (string) null;
    private static bool metadataInitalized = false;
    private const string SitefinityModuleName = "Sitefinity";
    private static int previousSitefinityVersionNumber = 0;
    private static readonly object statLock = new object();
    private static int dbCounter = 0;
    private static bool isFirstTime = true;
    private static readonly IDictionary<string, OpenAccessConnection> connections = (IDictionary<string, OpenAccessConnection>) new Dictionary<string, OpenAccessConnection>();
    private static bool executePostInitializationUpgrade;
    private static readonly object schemaCacheLock = new object();
    internal static EventHandler<EventArgs> metadataChanged;
    private readonly OpenAccessConnection.IMetadataUpdateStrategy metadataUpdateStrategy;
    private readonly List<DynamicTypeInfo> dynamicTypesToRegister = new List<DynamicTypeInfo>();
    private readonly DatabaseType dbType;
    private Database currentDatabase;
    private readonly string originalConnString;
    private string currentConnectionString;
    private BackendConfiguration backendConfiguration;
    private MetadataContainer currentMetadataContainer;
    private string connectionHash;
    private readonly string connectionName;
    private readonly HashSet<string> registeredModules = new HashSet<string>();
    private bool loadCommonMapping = true;
    private readonly bool ignoreDowngradeExceptions;
    private readonly OpenAccessConnection.ReplicationMode replication;
    private readonly bool readOnly;
    private FetchStrategyResolver fetchStrategyCache;
    private IDictionary<string, IOpenAccessMetadataProvider> providersInGroup;
    private OpenAccessConnection.OAConnectionState initialState;
    protected bool isMetadataContainer;
    internal const byte MaxIncrementalGuidRange = 100;

    static OpenAccessConnection()
    {
      OpenAccessConnection.timer = new System.Timers.Timer(1000.0);
      OpenAccessConnection.timer.Elapsed += new ElapsedEventHandler(OpenAccessConnection.TimerElapsed);
      OpenAccessConnection.timer.AutoReset = false;
      OpenAccessConnection.DiagnosticMode = OpenAccessConnection.OADiagnosticMode.Minimal;
      string str = ConfigurationManager.AppSettings.Get("sf:setOAConnectionDiagnosticMode") ?? (string) null;
      if (string.IsNullOrEmpty(str))
        return;
      if (str.Equals("true", StringComparison.OrdinalIgnoreCase))
      {
        OpenAccessConnection.DiagnosticMode = OpenAccessConnection.OADiagnosticMode.Detailed;
      }
      else
      {
        OpenAccessConnection.OADiagnosticMode result;
        if (!Enum.TryParse<OpenAccessConnection.OADiagnosticMode>(str, out result))
          return;
        OpenAccessConnection.DiagnosticMode = result;
      }
    }

    private static int GetUniqueNumber()
    {
      lock (OpenAccessConnection.statLock)
        return OpenAccessConnection.dbCounter++;
    }

    private static Database GetNewDatabase(
      OpenAccessConnection connection,
      MetadataContainer metadataContainer,
      out string connString)
    {
      connString = OpenAccessConnection.GetUniqueConnectionString(connection.originalConnString);
      Database database = Database.Get(connString, connection.backendConfiguration, metadataContainer);
      if (OpenAccessConnection.DiagnosticMode >= OpenAccessConnection.OADiagnosticMode.Detailed)
      {
        Log.Write((object) OpenAccessConnection.GetCreateDeleteDatabaseObjectMessage(connection, database, connection.backendConfiguration, "created"), ConfigurationPolicy.Trace);
        OpenAccessConnection.MetadataExtendMonitor metadataExtendMonitor = new OpenAccessConnection.MetadataExtendMonitor("initialize", connection, metadataContainer, (IEnumerable<string>) new string[0]);
      }
      return database;
    }

    private static string GetCreateDeleteDatabaseObjectMessage(
      OpenAccessConnection connection,
      Database database,
      BackendConfiguration configuration,
      string operation)
    {
      return "{0}: Database object ({1}) for connection '{2}' with configuration: SecondLevelCache={3}; CommandTimeout(s)={4}; PrepareCommands={5}; LoggingLevel={6};".Arrange((object) operation.ToUpper(), (object) database.GetHashCode(), (object) connection.GetLogKey(), (object) configuration.SecondLevelCache.Enabled, (object) configuration.Runtime.CommandTimeout, (object) configuration.Runtime.PrepareCommands, (object) configuration.Logging.LogEvents);
    }

    /// <summary>
    /// Gets the "range" byte of the OpenAccess GUID generator.
    /// Different values of the range byte ensure generating of independent GUID sequences that will not overlap in any cases.
    /// </summary>
    /// <exception cref="T:System.NullReferenceException">When a "default" <see cref="T:Telerik.OpenAccess.OpenAccessContext" /> cannot be obtained.</exception>
    internal static byte GetIncrementalGuidRange() => OpenAccessConnection.UsingIncrementalGuidContext<byte>((Func<OpenAccessContext, byte>) (context => context.KeyGenerators.GetIncrementalGuidRange()));

    /// <summary>
    /// Sets a new value the "range" byte of the OpenAccess GUID generator.
    /// Different values of the range byte ensure generating of independent GUID sequences that will not overlap in any cases.
    /// </summary>
    /// <param name="range">The range to be set as current. If <c>null</c> the current one will be incremented by <c>1</c>.</param>
    /// <exception cref="T:System.NullReferenceException">When a "default" <see cref="T:Telerik.OpenAccess.OpenAccessContext" /> cannot be obtained.</exception>
    /// <exception cref="T:System.OverflowException">In case the new range byte value overflows the range of the <see cref="T:System.Byte" /> type and would otherwise be reset to zero.</exception>
    /// <returns>The new range.</returns>
    internal static byte UpdateIncrementalGuidRange(byte? range = null) => OpenAccessConnection.UsingIncrementalGuidContext<byte>((Func<OpenAccessContext, byte>) (context =>
    {
      byte incrementalGuidRange = context.KeyGenerators.GetIncrementalGuidRange();
      byte range1;
      if (range.HasValue)
        range1 = range.Value;
      else if ((range1 = (byte) ((uint) incrementalGuidRange + 1U)) > (byte) 100)
        throw new OverflowException("Incremental GUID range overflow. Contact support!");
      if ((int) range1 != (int) incrementalGuidRange)
      {
        context.KeyGenerators.SetIncrementalGuidRange(range1);
        if (OpenAccessConnection.DiagnosticMode >= OpenAccessConnection.OADiagnosticMode.Minimal)
          Log.Write((object) string.Format("Incremental GUID range (re)set to {0}.", (object) range1), ConfigurationPolicy.Trace);
      }
      return range1;
    }));

    private static TResult UsingIncrementalGuidContext<TResult>(Func<OpenAccessContext, TResult> fn)
    {
      if (!(MetadataManager.GetManager().Provider is IOpenAccessDataProvider provider))
        throw new NullReferenceException("Unable to get an OpenAccessContext instance supporting incremental GUID key generators");
      using (OpenAccessContext context = (OpenAccessContext) OpenAccessConnection.GetContext(provider))
        return fn(context);
    }

    private static IDictionary<string, Version> GetAssembliesVersionsFromString(
      string assembliesString)
    {
      string[] strArray1 = assembliesString.Split(';');
      Dictionary<string, Version> versionsFromString = new Dictionary<string, Version>();
      foreach (string str in strArray1)
      {
        char[] chArray = new char[1]{ ',' };
        string[] strArray2 = str.Split(chArray);
        if (strArray2.Length == 2)
        {
          string key = strArray2[0];
          Version result;
          if (Version.TryParse(strArray2[1], out result))
            versionsFromString.Add(key, result);
        }
      }
      return (IDictionary<string, Version>) versionsFromString;
    }

    private static string GetAssembliesString(Assembly[] assemblies)
    {
      StringBuilder stringBuilder = new StringBuilder();
      foreach (Assembly assembly in assemblies)
      {
        if (stringBuilder.Length > 0)
          stringBuilder.Append(";");
        AssemblyName name = assembly.GetName();
        stringBuilder.Append(name.Name);
        stringBuilder.Append(",");
        stringBuilder.Append(name.Version.ToString());
      }
      return stringBuilder.ToString();
    }

    internal static bool CanUpgradeDbSchema(OpenAccessConnection connection) => connection.Replication != OpenAccessConnection.ReplicationMode.Slave && OpenAccessConnection.defaultSchemaUpgradeProviderName != null;

    /// <summary>
    /// Gets the unique connection string in order to be able to apply the specific mappings for the same database connection.
    /// </summary>
    /// <param name="connection">The connection.</param>
    /// <returns>The connection string with appended unique information (that forces DataAccess to create new database Object with its mappings).</returns>
    public static string GetUniqueConnectionString(string connectionString) => connectionString + ";Backend=" + (object) OpenAccessConnection.GetUniqueNumber();

    private static void TimerElapsed(object sender, ElapsedEventArgs e) => OpenAccessConnection.Recycle(false);

    private static void CloseDatabase(Database db, OpenAccessConnection connection)
    {
      lock (OpenAccessConnection.trash)
      {
        OpenAccessConnection.trash.Add(db);
        OpenAccessConnection.timer.Start();
        if (OpenAccessConnection.DiagnosticMode < OpenAccessConnection.OADiagnosticMode.Detailed)
          return;
        Log.Write((object) OpenAccessConnection.GetCreateDeleteDatabaseObjectMessage(connection, db, connection.backendConfiguration, "disposing"), ConfigurationPolicy.Trace);
      }
    }

    internal static void Recycle(bool forceDispose)
    {
      lock (OpenAccessConnection.trash)
      {
        OpenAccessConnection.timer.Stop();
        bool flag = false;
        foreach (Database database in new List<Database>((IEnumerable<Database>) OpenAccessConnection.trash))
        {
          if (forceDispose || database.Adapter.OpenScopesCount() == 0)
          {
            database.Dispose();
            OpenAccessConnection.trash.Remove(database);
            flag = true;
          }
        }
        if (flag)
          GC.Collect();
        OpenAccessConnection.timer.Enabled = OpenAccessConnection.trash.Count<Database>() > 0;
      }
    }

    [Obsolete("Use ResetModel with reason")]
    public static void ResetModel(bool notify = true) => OpenAccessConnection.ResetModel(OperationReason.UnknownReason(), notify);

    public static void ResetModel(OperationReason reason, bool notify = true)
    {
      OpenAccessConnection.CleanAll(reason, false);
      SystemManager.RaiseModelReset(EventArgs.Empty);
      if (notify)
        SystemMessageDispatcher.SendSystemMessage((SystemMessageBase) new ResetModelMessage(reason));
      if (OpenAccessConnection.DiagnosticMode < OpenAccessConnection.OADiagnosticMode.Minimal)
        return;
      SystemManager.LogRestartOperation(SystemManager.RestartOperationKind.ModelReset, reason, new NameValueCollection()
      {
        {
          nameof (notify),
          notify.ToString()
        }
      });
    }

    internal static IEnumerable<OpenAccessConnection> GetConnections() => (IEnumerable<OpenAccessConnection>) OpenAccessConnection.connections.Values;

    /// <summary>Cleans all OpenAccess connections.</summary>
    [Obsolete("Use the overload to provide a reason")]
    public static void CleanAll()
    {
      OperationReason reason = OperationReason.UnknownReason();
      reason.AddInfo("FullClean");
      OpenAccessConnection.CleanAll(reason);
    }

    /// <summary>Cleans all OpenAccess connections.</summary>
    public static void CleanAll(OperationReason reason, bool logMessage = true)
    {
      ResetModelReason from = ResetModelReason.CreateFrom(reason);
      lock (OpenAccessConnection.connections)
      {
        List<OpenAccessConnection> source = new List<OpenAccessConnection>();
        try
        {
          foreach (OpenAccessConnection accessConnection in (IEnumerable<OpenAccessConnection>) OpenAccessConnection.connections.Values)
          {
            if (accessConnection.ShouldBeReset((IEnumerable<string>) from.UpdatedModules))
            {
              source.Add(accessConnection);
              accessConnection.Lock();
            }
          }
          if (source.Count > 0)
          {
            bool flag = reason.AdditionalInfo.ContainsKey("FullClean");
            foreach (OpenAccessConnection accessConnection in source)
            {
              if (flag)
              {
                if (accessConnection.isMetadataContainer)
                  OpenAccessConnection.metadataInitalized = false;
                accessConnection.Dispose();
                OpenAccessConnection.connections.Remove(accessConnection.Name);
              }
              else
                accessConnection.Reset();
            }
          }
          DynamicFieldsTypeDescriptionProvider.ClearCache();
          PageSiteNode.ClearPropCache();
          if (!reason.AdditionalInfo.ContainsKey("SkipMappingAggregation"))
            OpenAccessConnection.EnsureMetadataAggregation((IList<string>) source.Select<OpenAccessConnection, string>((Func<OpenAccessConnection, string>) (c => c.Name)).ToList<string>(), (IList<string>) from.UpdatedModules, from.HasDeletedTypes || from.HasDeletedFields);
        }
        finally
        {
          foreach (OpenAccessConnection accessConnection in source)
          {
            if (accessConnection.IsLocked())
              accessConnection.Unlock();
          }
        }
      }
      if (!logMessage || OpenAccessConnection.DiagnosticMode < OpenAccessConnection.OADiagnosticMode.Minimal)
        return;
      SystemManager.LogRestartOperation(SystemManager.RestartOperationKind.ModelReset, reason, (NameValueCollection) null);
    }

    internal static void EnsureMetadataAggregation(
      IList<string> affectedConnections = null,
      IList<string> updatedModules = null,
      bool hasDeletedTypesOrFields = false)
    {
      ISet<Type> cachedManagerTypes = SystemManager.CachedManagerTypes;
      if (cachedManagerTypes.Count <= 0)
        return;
      if (affectedConnections == null)
        affectedConnections = (IList<string>) OpenAccessConnection.connections.Keys.ToList<string>();
      using (new DelayedDatabaseInitRegion())
      {
        if (updatedModules != null & hasDeletedTypesOrFields)
        {
          foreach (Type managerType in (IEnumerable<Type>) cachedManagerTypes)
          {
            foreach (DataProviderBase staticProvider in ManagerBase.GetManager(managerType).StaticProviders)
            {
              if (staticProvider is IOpenAccessDataProvider provider && affectedConnections.Contains(provider.Context.ConnectionId) && updatedModules.Contains(provider.ModuleName))
                OpenAccessConnection.InitializeProvider((IOpenAccessMetadataProvider) provider, provider.Context.ConnectionId);
            }
          }
          foreach (string affectedConnection in (IEnumerable<string>) affectedConnections)
          {
            OpenAccessConnection accessConnection;
            if (OpenAccessConnection.connections.TryGetValue(affectedConnection, out accessConnection))
              accessConnection.Reset();
          }
        }
        foreach (Type managerType in (IEnumerable<Type>) cachedManagerTypes)
        {
          foreach (DataProviderBase staticProvider in ManagerBase.GetManager(managerType).StaticProviders)
          {
            if (staticProvider is IOpenAccessDataProvider provider && affectedConnections.Contains(provider.Context.ConnectionId))
              OpenAccessConnection.InitializeProvider((IOpenAccessMetadataProvider) provider, provider.Context.ConnectionId);
          }
        }
      }
    }

    public static IObjectScope GetObjectScope(IOpenAccessDataProvider provider)
    {
      if (provider.Context == null)
        throw new Exception("The provider '{0}' of type '{1}' is not initialized".Arrange((object) provider.Name, (object) provider.ModuleName));
      string connectionName = provider.Context.ConnectionId;
      if (string.IsNullOrEmpty(connectionName))
        connectionName = "Sitefinity";
      return OpenAccessConnection.GetConnection((IOpenAccessMetadataProvider) provider, connectionName).GetObjectScope((IOpenAccessMetadataProvider) provider);
    }

    public static SitefinityOAContext GetContext(IOpenAccessDataProvider provider)
    {
      if (provider.Context == null)
        throw new Exception("The provider '{0}' of type '{1}' is not initialized".Arrange((object) provider.Name, (object) provider.ModuleName));
      return OpenAccessConnection.GetContext((IOpenAccessMetadataProvider) provider, provider.Context.ConnectionId);
    }

    /// <summary>
    /// Gets the open access context to the specified connection and with the mapping specified by IOpenAccessMetadataProvider.
    /// </summary>
    /// <param name="prov">The prov.</param>
    /// <param name="connectionName">Name of the connection.</param>
    /// <returns></returns>
    public static SitefinityOAContext GetContext(
      IOpenAccessMetadataProvider provider,
      string connectionName)
    {
      OpenAccessConnection accessConnection;
      if (!OpenAccessConnection.connections.TryGetValue(connectionName, out accessConnection))
      {
        lock (OpenAccessConnection.connections)
        {
          if (!OpenAccessConnection.connections.TryGetValue(connectionName, out accessConnection))
          {
            accessConnection = OpenAccessConnection.InitializeProvider(provider, connectionName);
            return accessConnection.GetCurrentContext(provider);
          }
        }
      }
      return accessConnection.GetContext(provider);
    }

    private static OpenAccessConnection GetConnection(
      IOpenAccessMetadataProvider provider,
      string connectionName)
    {
      OpenAccessConnection connection;
      if (!OpenAccessConnection.connections.TryGetValue(connectionName, out connection))
      {
        lock (OpenAccessConnection.connections)
        {
          if (!OpenAccessConnection.connections.TryGetValue(connectionName, out connection))
            return OpenAccessConnection.InitializeProvider(provider, connectionName);
        }
      }
      return connection;
    }

    /// <summary>Initializes the provider.</summary>
    /// <param name="provider">The provider.</param>
    /// <param name="connectionName">Name of the connection.</param>
    /// <returns></returns>
    public static OpenAccessConnection InitializeProvider(
      IOpenAccessMetadataProvider provider,
      string connectionName)
    {
      OpenAccessConnection.EnsureInitialState(provider, connectionName);
      OpenAccessConnection accessConnection;
      if (!OpenAccessConnection.connections.TryGetValue(connectionName, out accessConnection))
      {
        lock (OpenAccessConnection.connections)
        {
          if (!OpenAccessConnection.connections.TryGetValue(connectionName, out accessConnection))
          {
            accessConnection = new OpenAccessConnection(OpenAccessConnection.GetConnectionStringSettings(connectionName));
            OpenAccessConnection.connections.Add(connectionName, accessConnection);
          }
        }
      }
      accessConnection.RegisterProvider(provider);
      return accessConnection;
    }

    /// <summary>Creates the backend configuration.</summary>
    /// <param name="backend">The backend.</param>
    /// <param name="connectionName">If specified, the Name of the connection this configuration is created for.</param>
    /// <returns></returns>
    public static BackendConfiguration CreateBackendConfiguration(
      DatabaseType backend,
      string connectionName = null)
    {
      BackendConfiguration backendConfiguration = new BackendConfiguration()
      {
        Backend = OABackendNameAttribute.GetName((Enum) backend)
      };
      backendConfiguration.Runtime.AllowReadAfterDelete = new bool?(true);
      backendConfiguration.Runtime.UseUTCForAutoSetValues = true;
      backendConfiguration.Runtime.UseUTCForReadValues = true;
      backendConfiguration.Runtime.LockTimeoutMSec = 30000;
      backendConfiguration.Runtime.AllowCascadeDelete = true;
      backendConfiguration.Runtime.StatementBatchingEnabled = true;
      if (backend == DatabaseType.Oracle)
        backendConfiguration.ConnectionPool.Pool = ConnectionPoolType.ADOManaged;
      backendConfiguration.ConnectionPool.MaxActive = 100;
      backendConfiguration.Logging.LogErrorsToWindowsEventLog = true;
      if (OpenAccessConnection.isFirstTime)
      {
        TraceAdapter.Instance.ResetLevel("none");
        OpenAccessConnection.isFirstTime = false;
      }
      backendConfiguration.Logging.LogEvents = LoggingLevel.Errors;
      BackendConfiguration.SecondLevelCacheConfiguration secondLevelCache = backendConfiguration.SecondLevelCache;
      bool? enableDataCaching = Config.Get<DataConfig>().EnableDataCaching;
      if (!enableDataCaching.HasValue)
        OpenAccessConnection.ConfigureDefaultCaching(secondLevelCache, connectionName);
      else if (enableDataCaching.GetValueOrDefault())
        OpenAccessConnection.ConfigureDefaultCaching(secondLevelCache, connectionName);
      return backendConfiguration;
    }

    private static void EnsureInitialState(
      IOpenAccessMetadataProvider provider,
      string connectionName)
    {
      if (OpenAccessConnection.metadataInitalized)
        return;
      lock (OpenAccessConnection.connections)
      {
        if (OpenAccessConnection.metadataInitalized)
          return;
        using (new ConfigSafeModeRegion(true))
        {
          if (provider is MetaDataProvider provider2)
          {
            if (!OpenAccessConnection.connections.ContainsKey(connectionName))
            {
              OpenAccessConnection metadataConnection = OpenAccessConnection.CreateMetadataConnection(OpenAccessConnection.GetConnectionStringSettings(connectionName), provider2);
              metadataConnection.RegisterProvider(provider);
              metadataConnection.SetAsInitialState();
              OpenAccessConnection.connections.Add(connectionName, metadataConnection);
              if (OpenAccessConnection.defaultSchemaUpgradeProviderName == null)
              {
                if (!metadataConnection.ReadOnly)
                  OpenAccessConnection.defaultSchemaUpgradeProviderName = provider2.Name;
              }
            }
            else
              OpenAccessConnection.metadataInitalized = true;
          }
          else
          {
            foreach (MetaDataProvider staticProvider in (Collection<MetaDataProvider>) MetadataManager.GetManager().StaticProviders)
            {
              if (staticProvider is IOpenAccessDataProvider provider1)
                OpenAccessConnection.InitializeProvider((IOpenAccessMetadataProvider) provider1, provider1.Context.ConnectionId);
            }
          }
        }
        if (!OpenAccessConnection.executePostInitializationUpgrade)
          return;
        try
        {
          using (new ConfigSafeModeRegion(false))
            OpenAccessConnection.ExecutePostInitializationUpgradeScripts();
        }
        finally
        {
          OpenAccessConnection.executePostInitializationUpgrade = false;
        }
      }
    }

    private static void ConfigureDefaultCaching(
      BackendConfiguration.SecondLevelCacheConfiguration cacheConfig,
      string connectionName)
    {
      cacheConfig.Enabled = true;
      cacheConfig.CacheQueryResults = true;
      cacheConfig.Synchronization.Name = typeof (CacheSyncClusterTransport).AssemblyQualifiedName;
      if (string.IsNullOrEmpty(connectionName))
        return;
      cacheConfig.Synchronization.Localpath = connectionName;
    }

    /// <summary>Creates the fluent mapping context.</summary>
    /// <param name="backend">The backend.</param>
    /// <returns></returns>
    public static IDatabaseMappingContext CreateFluentMappingContext(
      DatabaseType backend)
    {
      return backend != DatabaseType.Unspecified ? ObjectFactory.Container.Resolve<IDatabaseMappingContext>(backend.ToString()) : throw new ArgumentException("Unspecified database backend.", nameof (backend));
    }

    internal static void RegisterDatabaseMappingContext<TMappingContext>(DatabaseType backend) where TMappingContext : IDatabaseMappingContext => ObjectFactory.Container.RegisterType(typeof (IDatabaseMappingContext), typeof (TMappingContext), backend.ToString());

    private static MetadataContainer GetMetadataModel(
      IDatabaseMappingContext context)
    {
      MetadataContainer model = new MetadataMetadataSource(context).GetModel();
      OpenAccessConnection.InitMetadataContainer(model);
      return model;
    }

    private static void InitMetadataContainer(MetadataContainer metadataContainer)
    {
      metadataContainer.DefaultMapping.AlwaysCreateIndexOnJoinTableValueColumns = true;
      metadataContainer.UniqueIdGenerator.CreateTable = true;
    }

    private static OpenAccessConnection CreateMetadataConnection(
      IConnectionStringSettings connectionSettings,
      MetaDataProvider provider)
    {
      string moduleName = "Sitefinity";
      string applicationName = provider.ApplicationName;
      OpenAccessConnection oaConnection = (OpenAccessConnection) new MetadataOpenAccessConnection(connectionSettings);
      MetadataContainer metadataContainer = OpenAccessConnection.GetMetadataModel(oaConnection.GetFluentMappingContext());
      if (ConfigManager.GetManager().Provider.GetDatabaseStorageProvider() is OpenAccessXmlConfigStorageProvider databaseStorageProvider && connectionSettings.Name.Equals(databaseStorageProvider.ConnectionName))
      {
        MetadataSourceAggregator sourceAggregator = new MetadataSourceAggregator(metadataContainer);
        sourceAggregator.Extend((MetadataSource) new XmlConfigStorageMetadataSource(oaConnection.GetFluentMappingContext()));
        metadataContainer = sourceAggregator.CurrentModel;
        OpenAccessConnection.InitMetadataContainer(metadataContainer);
      }
      int previousBuild = 0;
      string connString;
      Database metadataDatabase = OpenAccessConnection.GetAndUpgradeMetadataDatabase(connectionSettings, oaConnection, metadataContainer, applicationName, moduleName, out connString, out previousBuild);
      oaConnection.currentMetadataContainer = metadataContainer;
      oaConnection.currentDatabase = metadataDatabase;
      oaConnection.currentConnectionString = connString;
      if (databaseStorageProvider != null)
        oaConnection.RegisterProviderInternal((IOpenAccessMetadataProvider) databaseStorageProvider, true);
      OpenAccessConnection.previousSitefinityVersionNumber = previousBuild;
      return oaConnection;
    }

    private static Database GetAndUpgradeMetadataDatabase(
      IConnectionStringSettings connectionSettings,
      OpenAccessConnection oaConnection,
      MetadataContainer metadataContainer,
      string appName,
      string moduleName,
      out string connString,
      out int previousBuild,
      bool isSecondCall = false)
    {
      previousBuild = 0;
      Database newDatabase = OpenAccessConnection.GetNewDatabase(oaConnection, metadataContainer, out connString);
      int build = typeof (SchemaVersion).Assembly.GetName().Version.Build;
      string connId = oaConnection.connectionName;
      bool flag1 = false;
      OpenAccessConnection.executePostInitializationUpgrade = false;
      bool flag2;
      try
      {
        using (IObjectScope objectScope = newDatabase.GetObjectScope())
        {
          SchemaVersion schemaVersion = objectScope.Extent<SchemaVersion>().Where<SchemaVersion>((Expression<Func<SchemaVersion, bool>>) (s => s.ApplicationName == appName && s.ModuleName == moduleName && s.ConnectionId == connId)).FirstOrDefault<SchemaVersion>();
          if (schemaVersion != null)
          {
            if (schemaVersion.VersionNumber > build)
            {
              if (!oaConnection.IgnoreDowngradeExceptions)
                throw new System.InvalidOperationException("The database schema version ({0}) is higher than the running Sitefinity version ({1}). Downgrade is not allowed.".Arrange((object) schemaVersion.VersionNumber.ToString(), (object) build.ToString()));
              if (OpenAccessConnection.DiagnosticMode >= OpenAccessConnection.OADiagnosticMode.Minimal)
                Log.Write((object) "IGNORED DOWNGRADE EXCEPTION: The database schema version ({0}) is higher than the running Sitefinity version ({1}).".Arrange((object) schemaVersion.VersionNumber.ToString(), (object) build.ToString()), ConfigurationPolicy.Trace);
            }
            else if (schemaVersion.VersionNumber < build)
              flag1 = true;
            else
              previousBuild = schemaVersion.PreviousVersionNumber;
          }
          else
          {
            flag1 = true;
            if (isSecondCall)
              previousBuild = OpenAccessConnection.TryUpgradeFrom_4_0(connectionSettings, appName);
          }
        }
      }
      catch (Telerik.OpenAccess.Exceptions.MetadataException ex)
      {
        flag1 = true;
      }
      catch (TypeLoadException ex)
      {
        flag1 = true;
      }
      catch (DatabaseNotFoundException ex)
      {
        flag1 = true;
      }
      catch (DataStoreException ex1)
      {
        if (isSecondCall)
        {
          throw;
        }
        else
        {
          flag2 = true;
          try
          {
            OpenAccessConnection.UpgradeDatabase(newDatabase, oaConnection);
          }
          catch (Exception ex2)
          {
            Log.Write((object) string.Format("Initial install FAILED: {0}. See the error log for more details.", (object) ex2.Message), ConfigurationPolicy.UpgradeTrace);
            if (Telerik.Sitefinity.Abstractions.Exceptions.HandleException(ex2, ExceptionPolicyName.IgnoreExceptions))
              throw;
          }
          finally
          {
            newDatabase.Dispose();
          }
          return OpenAccessConnection.GetAndUpgradeMetadataDatabase(connectionSettings, oaConnection, metadataContainer, appName, moduleName, out connString, out previousBuild, true);
        }
      }
      catch (System.InvalidOperationException ex)
      {
        switch (ex.InnerException)
        {
          case null:
            throw;
          case DataStoreException _:
          case Telerik.OpenAccess.Exceptions.MetadataException _:
          case TypeLoadException _:
          case DatabaseNotFoundException _:
            flag2 = true;
            break;
        }
        throw;
      }
      catch
      {
        throw;
      }
      if (flag1)
      {
        OpenAccessConnection.UpgradeDatabase(newDatabase, oaConnection);
        using (IObjectScope objectScope = newDatabase.GetObjectScope())
        {
          Guid itemToMigrateId = Guid.Empty;
          objectScope.Transaction.Begin();
          SchemaVersion persistenceCapableObject = objectScope.Extent<SchemaVersion>().Where<SchemaVersion>((Expression<Func<SchemaVersion, bool>>) (s => s.ApplicationName == appName && s.ModuleName == moduleName && s.ConnectionId == connId)).FirstOrDefault<SchemaVersion>();
          if (persistenceCapableObject == null)
          {
            ExtentQuery<SchemaVersion> source1 = objectScope.Extent<SchemaVersion>();
            Expression<Func<SchemaVersion, bool>> predicate1 = (Expression<Func<SchemaVersion, bool>>) (s => s.ApplicationName == appName && s.ModuleName == moduleName);
            foreach (SchemaVersion schemaVersion in (IEnumerable<SchemaVersion>) source1.Where<SchemaVersion>(predicate1))
            {
              if (persistenceCapableObject == null || schemaVersion.VersionNumber > persistenceCapableObject.VersionNumber || schemaVersion.VersionNumber == persistenceCapableObject.VersionNumber && schemaVersion.LastUpgradeDate > persistenceCapableObject.LastUpgradeDate)
                persistenceCapableObject = schemaVersion;
            }
            if (persistenceCapableObject != null && persistenceCapableObject.VersionNumber < 1430)
            {
              itemToMigrateId = persistenceCapableObject.Id;
              string connHash = persistenceCapableObject.ConnectionId;
              ExtentQuery<SchemaVersion> source2 = objectScope.Extent<SchemaVersion>();
              Expression<Func<SchemaVersion, bool>> predicate2 = (Expression<Func<SchemaVersion, bool>>) (s => s.ApplicationName == appName && s.ConnectionId == connHash);
              foreach (SchemaVersion schemaVersion in (IEnumerable<SchemaVersion>) source2.Where<SchemaVersion>(predicate2))
              {
                schemaVersion.ConnectionId = connId;
                schemaVersion.ConnectionHash = oaConnection.ConnectionHash;
              }
              persistenceCapableObject.ConnectionId = connId;
              persistenceCapableObject.ConnectionHash = oaConnection.ConnectionHash;
            }
          }
          if (persistenceCapableObject == null)
          {
            persistenceCapableObject = new SchemaVersion(appName, moduleName, connId);
            objectScope.Add((object) persistenceCapableObject);
            persistenceCapableObject.PreviousVersionNumber = previousBuild;
          }
          else
            persistenceCapableObject.PreviousVersionNumber = persistenceCapableObject.VersionNumber;
          persistenceCapableObject.ConnectionHash = oaConnection.ConnectionHash;
          persistenceCapableObject.VersionNumber = build;
          objectScope.Transaction.Commit();
          if (itemToMigrateId != Guid.Empty)
          {
            List<IConnectionStringSettings> connectionStringSettingsList = new List<IConnectionStringSettings>();
            foreach (ConnStringSettings connStringSettings in (IEnumerable<ConnStringSettings>) Config.Get<DataConfig>(true).ConnectionStrings.Values)
            {
              if (!connStringSettings.Name.Equals(connId))
                connectionStringSettingsList.Add((IConnectionStringSettings) connStringSettings);
            }
            if (connectionStringSettingsList.Count > 0)
            {
              objectScope.Transaction.Begin();
              foreach (IConnectionStringSettings connectionStringSettings in connectionStringSettingsList)
              {
                string connHash = OpenAccessConnection.ComputeConnectionHash(connectionStringSettings.ConnectionString, OpenAccessConnection.ReplicationMode.None);
                ExtentQuery<SchemaVersion> source = objectScope.Extent<SchemaVersion>();
                Expression<Func<SchemaVersion, bool>> predicate = (Expression<Func<SchemaVersion, bool>>) (s => s.ApplicationName == appName && s.ConnectionId == connHash);
                foreach (SchemaVersion schemaVersion in (IEnumerable<SchemaVersion>) source.Where<SchemaVersion>(predicate))
                {
                  schemaVersion.ConnectionId = connectionStringSettings.Name;
                  schemaVersion.ConnectionHash = connHash;
                }
              }
              objectScope.Transaction.Commit();
            }
            objectScope.Transaction.Begin();
            List<SchemaVersion> schemaVersionList = new List<SchemaVersion>();
            List<string> source3 = new List<string>();
            ExtentQuery<SchemaVersion> source4 = objectScope.Extent<SchemaVersion>();
            Expression<Func<SchemaVersion, bool>> predicate3 = (Expression<Func<SchemaVersion, bool>>) (s => s.ApplicationName == appName && s.ModuleName == moduleName && s.Id != itemToMigrateId);
            foreach (SchemaVersion schemaVersion in (IEnumerable<SchemaVersion>) source4.Where<SchemaVersion>(predicate3))
            {
              source3.Add(schemaVersion.ConnectionId);
              schemaVersionList.Add(schemaVersion);
            }
            if (source3.Count<string>() > 0)
            {
              foreach (string str in source3)
              {
                string conn = str;
                ExtentQuery<SchemaVersion> source5 = objectScope.Extent<SchemaVersion>();
                Expression<Func<SchemaVersion, bool>> predicate4 = (Expression<Func<SchemaVersion, bool>>) (s => s.ApplicationName == appName && s.ConnectionId == conn);
                foreach (SchemaVersion schemaVersion1 in (IEnumerable<SchemaVersion>) source5.Where<SchemaVersion>(predicate4))
                {
                  SchemaVersion schemaVersion2 = schemaVersion1;
                  string itemModule = schemaVersion1.ModuleName;
                  IQueryable<SchemaVersion> source6 = objectScope.Extent<SchemaVersion>().Where<SchemaVersion>((Expression<Func<SchemaVersion, bool>>) (s => s.ApplicationName == appName && s.ModuleName == itemModule && s.ConnectionId == connId));
                  if (source6.Count<SchemaVersion>() > 0)
                  {
                    foreach (SchemaVersion schemaVersion3 in (IEnumerable<SchemaVersion>) source6)
                    {
                      if (schemaVersion3.VersionNumber > schemaVersion2.VersionNumber || schemaVersion3.VersionNumber == schemaVersion2.VersionNumber && schemaVersion3.LastUpgradeDate > schemaVersion2.LastUpgradeDate)
                      {
                        schemaVersionList.Add(schemaVersion2);
                        schemaVersion2 = schemaVersion3;
                      }
                    }
                  }
                  schemaVersion2.ConnectionId = connId;
                  schemaVersion2.ConnectionHash = oaConnection.ConnectionHash;
                }
              }
            }
            schemaVersionList.AddRange((IEnumerable<SchemaVersion>) objectScope.Extent<SchemaVersion>().Where<SchemaVersion>((Expression<Func<SchemaVersion, bool>>) (s => s.ApplicationName == default (string) || s.ApplicationName == appName && s.ConnectionHash == default (string))));
            foreach (SchemaVersion persistentObject in schemaVersionList)
              objectScope.Remove((object) persistentObject);
            objectScope.Transaction.Commit();
          }
          previousBuild = persistenceCapableObject.PreviousVersionNumber;
          if (previousBuild > 1)
          {
            OpenAccessConnection.executePostInitializationUpgrade = true;
            if (previousBuild < 3040)
            {
              objectScope.Transaction.Begin();
              ExtentQuery<Telerik.Sitefinity.Metadata.Model.MetaType> source = objectScope.Extent<Telerik.Sitefinity.Metadata.Model.MetaType>();
              Expression<Func<Telerik.Sitefinity.Metadata.Model.MetaType, bool>> predicate = (Expression<Func<Telerik.Sitefinity.Metadata.Model.MetaType, bool>>) (t => t.ApplicationName == appName && !t.IsDynamic && t.AssemblyName == "Telerik.Sitefinity.Model");
              foreach (Telerik.Sitefinity.Metadata.Model.MetaType metaType in (IEnumerable<Telerik.Sitefinity.Metadata.Model.MetaType>) source.Where<Telerik.Sitefinity.Metadata.Model.MetaType>(predicate))
              {
                Type type = TypeResolutionService.ResolveType(metaType.Namespace + "." + metaType.ClassName, false);
                if (type != (Type) null)
                  metaType.AssemblyName = type.Assembly.GetName().Name;
              }
              objectScope.Transaction.Commit();
            }
            if (previousBuild < 3860)
            {
              string[] strArray;
              if (oaConnection.DbType == DatabaseType.MySQL)
                strArray = new string[22]
                {
                  "sf_blog_posts_sf_permissions_inheritance_map",
                  "sf_blogs_sf_permissions_inheritance_map",
                  "sf_events_sf_permissions_inheritance_map",
                  "sf_form_description_sf_permissions_inheritance_map",
                  "sf_forum_groups_sf_permissions_inheritance_map",
                  "sf_forums_sf_permissions_inheritance_map",
                  "sf_libraries_sf_permissions_inheritance_map",
                  "sf_list_items_sf_permissions_inheritance_map",
                  "sf_lists_sf_permissions_inheritance_map",
                  "sf_mb_dnc_cnt_provider_sf_permissions_inheritance_map",
                  "sf_mb_dynamic_module_field_sf_permissions_inheritance_map",
                  "sf_mb_dynamic_module_sf_permissions_inheritance_map",
                  "sf_mb_dynamic_module_type_sf_permissions_inheritance_map",
                  "sf_media_content_sf_permissions_inheritance_map",
                  "sf_news_items_sf_permissions_inheritance_map",
                  "sf_object_data_sf_permissions_inheritance_map",
                  "sf_page_node_sf_permissions_inheritance_map",
                  "sf_page_templates_sf_permissions_inheritance_map",
                  "sf_security_roots_sf_permissions_inheritance_map",
                  "sf_sites_sf_permissions_inheritance_map",
                  "sf_taxonomies_sf_permissions_inheritance_map",
                  "sf_workflow_definition_sf_permissions_inheritance_map"
                };
              else if (oaConnection.DbType == DatabaseType.Oracle)
                strArray = new string[22]
                {
                  "sf_mb_dnc_cnt_prvdr_sf_prmssn2",
                  "sf_mb_dynmc_mdl_fld_sf_prmssn2",
                  "sf_mb_dynmc_mdl_sf_prmssns_nhr",
                  "sf_mb_dynmc_mdl_typ_sf_prmssn2",
                  "sf_blg_psts_sf_prmssns_nhrtnc_",
                  "sf_blogs_sf_prmssns_inhrtnc_mp",
                  "sf_wrkflw_dfntn_sf_prmssns_nhr",
                  "sf_lbrrs_sf_prmssns_inhrtnc_mp",
                  "sf_md_cntnt_sf_prmssns_nhrtnc_",
                  "sf_vents_sf_prmssns_inhrtnc_mp",
                  "sf_frm_grps_sf_prmssns_nhrtnc_",
                  "sf_frums_sf_prmssns_inhrtnc_mp",
                  "sf_nws_tms_sf_prmssns_nhrtnc_m",
                  "sf_sites_sf_prmssns_inhrtnc_mp",
                  "sf_frm_dscrptn_sf_prmssns_nhrt",
                  "sf_lists_sf_prmssns_inhrtnc_mp",
                  "sf_lst_tms_sf_prmssns_nhrtnc_m",
                  "sf_bjct_dt_sf_prmssns_nhrtnc_m",
                  "sf_pg_nd_sf_prmssns_inhrtnc_mp",
                  "sf_pg_tmplts_sf_prmssns_nhrtnc",
                  "sf_scrty_rts_sf_prmssns_nhrtnc",
                  "sf_txnms_sf_prmssns_inhrtnc_mp"
                };
              else
                strArray = new string[22]
                {
                  "sf_lst_tms_sf_prmssns_nhrtnc_m",
                  "sf_lsts_sf_prmssns_nhrtnce_map",
                  "sf_lbrrs_sf_prmssns_nhrtnc_map",
                  "sf_bjct_dt_sf_prmssns_nhrtnc_m",
                  "sf_md_cntnt_sf_prmssns_nhrtnc_",
                  "sf_scrty_rts_sf_prmssns_nhrtnc",
                  "sf_frm_dscrptn_sf_prmssns_nhrt",
                  "sf_wrkflw_dfntn_sf_prmssns_nhr",
                  "sf_vnts_sf_prmssns_nhrtnce_map",
                  "sf_sts_sf_prmssns_nhrtance_map",
                  "sf_mb_dynmc_mdl_typ_sf_prmssn2",
                  "sf_blg_psts_sf_prmssns_nhrtnc_",
                  "sf_blgs_sf_prmssns_nhrtnce_map",
                  "sf_frm_grps_sf_prmssns_nhrtnc_",
                  "sf_frms_sf_prmssns_nhrtnce_map",
                  "sf_txnms_sf_prmssns_nhrtnc_map",
                  "sf_mb_dnc_cnt_prvdr_sf_prmssn2",
                  "sf_mb_dynmc_mdl_fld_sf_prmssn2",
                  "sf_nws_tms_sf_prmssns_nhrtnc_m",
                  "sf_pg_nd_sf_prmssns_nhrtnc_map",
                  "sf_mb_dynmc_mdl_sf_prmssns_nhr",
                  "sf_pg_tmplts_sf_prmssns_nhrtnc"
                };
              string format = oaConnection.DbType == DatabaseType.Oracle ? "drop table \"{0}\"" : (oaConnection.DbType == DatabaseType.MsSql ? "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'{0}') AND type in (N'U')) DROP TABLE {0}" : "drop table {0}");
              foreach (string str in strArray)
              {
                try
                {
                  newDatabase.GetSchemaHandler().ForceExecuteDDLScript(string.Format(format, (object) str));
                }
                catch (Exception ex)
                {
                  Log.Write((object) string.Format("Drop table '{0}' FAILED: {1}. See the error log for more details.", (object) str, (object) ex.Message), ConfigurationPolicy.UpgradeTrace);
                  if (Telerik.Sitefinity.Abstractions.Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
                    throw;
                }
              }
            }
            if (previousBuild < 3910)
            {
              objectScope.Transaction.Begin();
              ExtentQuery<MetaField> source7 = objectScope.Extent<MetaField>();
              Expression<Func<MetaField, bool>> predicate5 = (Expression<Func<MetaField, bool>>) (f => f.ApplicationName == appName && f.ClrType == typeof (ContentLink[]).FullName);
              foreach (MetaField metaField in (IEnumerable<MetaField>) source7.Where<MetaField>(predicate5))
              {
                IList<MetaFieldAttribute> metaAttributes = metaField.MetaAttributes;
                MetaFieldAttribute metaFieldAttribute = new MetaFieldAttribute();
                metaFieldAttribute.Name = "useAutoGeneratedTableName";
                metaAttributes.Add(metaFieldAttribute);
              }
              ExtentQuery<Telerik.Sitefinity.Metadata.Model.MetaType> source8 = objectScope.Extent<Telerik.Sitefinity.Metadata.Model.MetaType>();
              Expression<Func<Telerik.Sitefinity.Metadata.Model.MetaType, bool>> predicate6 = (Expression<Func<Telerik.Sitefinity.Metadata.Model.MetaType, bool>>) (t => t.ApplicationName == appName && t.BaseClassName == typeof (DynamicContent).FullName);
              foreach (Telerik.Sitefinity.Metadata.Model.MetaType metaType in (IEnumerable<Telerik.Sitefinity.Metadata.Model.MetaType>) source8.Where<Telerik.Sitefinity.Metadata.Model.MetaType>(predicate6))
              {
                IList<MetaTypeAttribute> metaAttributes = metaType.MetaAttributes;
                MetaTypeAttribute metaTypeAttribute = new MetaTypeAttribute();
                metaTypeAttribute.Name = "useAutoGeneratedTableName";
                metaAttributes.Add(metaTypeAttribute);
              }
              objectScope.Transaction.Commit();
            }
            if (previousBuild < SitefinityVersion.Sitefinity10_2.Build)
            {
              Dictionary<string, string[]> dictionary = new Dictionary<string, string[]>();
              string format1;
              string format2;
              string format3;
              if (oaConnection.DbType == DatabaseType.MySQL)
              {
                dictionary.Add("sf_meta_fields_sf_meta_attribute", new string[2]
                {
                  "ref_sf_meta_fields_sf_meta_attribute_sf_meta_fields",
                  "ref_sf_meta_fields_sf_meta_attribute_sf_meta_attribute"
                });
                dictionary.Add("sf_meta_types_sf_meta_attribute", new string[2]
                {
                  "ref_sf_meta_types_sf_meta_attribute_sf_meta_attribute",
                  "ref_sf_meta_types_sf_meta_attribute_sf_meta_types"
                });
                format1 = "update sf_meta_attribute attr inner join {0} link on link.id2 = attr.id set attr.id2 = link.id";
                format2 = "drop table {0}";
                format3 = "alter table {0} drop foreign key {1}";
              }
              else if (oaConnection.DbType == DatabaseType.Oracle)
              {
                dictionary.Add("sf_mt_fields_sf_meta_attribute", new string[2]
                {
                  "ref_sf_mt_flds_sf_mt__EC958E9B",
                  "ref_sf_mt_flds_sf_mt__D9634507"
                });
                dictionary.Add("sf_mta_types_sf_meta_attribute", new string[2]
                {
                  "ref_sf_mt_typs_sf_mt__6697360C",
                  "ref_sf_mt_typs_sf_mt__D9A516DC"
                });
                format1 = "UPDATE \"sf_meta_attribute\" SET \"sf_meta_attribute\".\"id2\" = (SELECT \"{0}\".\"id\" FROM \"{0}\" WHERE \"sf_meta_attribute\".\"id\" = \"{0}\".\"id2\") WHERE EXISTS(SELECT \"{0}\".\"id\" FROM \"{0}\" WHERE \"sf_meta_attribute\".\"id\" = \"{0}\".\"id2\")";
                format2 = "drop table \"{0}\"";
                format3 = "alter table \"{0}\" DROP CONSTRAINT \"{1}\"";
              }
              else
              {
                dictionary.Add("sf_mt_fields_sf_meta_attribute", new string[2]
                {
                  "ref_sf_mt_flds_sf_mt__EC958E9B",
                  "ref_sf_mt_flds_sf_mt__D9634507"
                });
                dictionary.Add("sf_mta_types_sf_meta_attribute", new string[2]
                {
                  "ref_sf_mt_typs_sf_mt__6697360C",
                  "ref_sf_mt_typs_sf_mt__D9A516DC"
                });
                format1 = "update sf_meta_attribute set id2 = link.id from sf_meta_attribute attr inner join {0} link on link.id2 = attr.id";
                format2 = "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'{0}') AND type in (N'U')) DROP TABLE {0}";
                format3 = "IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'{1}') AND type in (N'F')) ALTER TABLE {0} DROP CONSTRAINT {1}";
              }
              Queue<string> values = new Queue<string>();
              if (previousBuild < SitefinityVersion.Sitefinity10_1.Build)
              {
                foreach (string key in dictionary.Keys)
                  values.Enqueue(string.Format(format1, (object) key));
                foreach (string key in dictionary.Keys)
                {
                  foreach (string str in dictionary[key])
                    values.Enqueue(string.Format(format3, (object) key, (object) str));
                }
              }
              else
              {
                foreach (string key in dictionary.Keys)
                  values.Enqueue(string.Format(format2, (object) key));
              }
              while (values.Count > 0)
              {
                try
                {
                  string ddl = values.Peek();
                  newDatabase.GetSchemaHandler().ForceExecuteDDLScript(ddl);
                  values.Dequeue();
                }
                catch (Exception ex)
                {
                  Log.Write((object) string.Format("FAILED to execute upgrade scripts: '{0}'. Error: '{1}'. Try to execute the script manually. See the error log for more details.", (object) string.Join("; ", (IEnumerable<string>) values), (object) ex.Message), ConfigurationPolicy.UpgradeTrace);
                  if (Telerik.Sitefinity.Abstractions.Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
                    throw;
                  else
                    break;
                }
              }
            }
            if (previousBuild < SitefinityVersion.Sitefinity11_0.Build && (oaConnection.DbType == DatabaseType.MsSql || oaConnection.DbType == DatabaseType.SqlAzure))
            {
              string[] strArray = new string[5]
              {
                "delete\r\n [sf_permissions_inheritance_map]\r\nfrom \r\n [sf_permissions_inheritance_map]\r\ninner join\r\n(\r\n select object_id, child_object_id, child_object_type_name, count(*) as cnt, min(sf_prmssons_inheritance_map_id) as minId\r\n from [sf_permissions_inheritance_map] \r\n group by object_id, child_object_id, child_object_type_name\r\n having count(*) > 1\r\n\r\n) as t1\r\non\r\n [sf_permissions_inheritance_map].object_id = t1.object_id and [sf_permissions_inheritance_map].child_object_id = t1.child_object_id\r\nwhere\r\n [sf_permissions_inheritance_map].sf_prmssons_inheritance_map_id <> t1.minId",
                "delete\r\n  FROM [sf_permissions_inheritance_map]\r\n  where child_object_type_name in (1053642113, 675916353, 1719647903) and child_object_id not in (select content_id from sf_media_content)",
                "delete\r\n  FROM [sf_permissions_inheritance_map]\r\n  where child_object_type_name = 1003093577 and child_object_id not in (select id from sf_page_node)",
                "delete\r\n [sf_permissions_inheritance_map]\r\nfrom\r\n [sf_permissions_inheritance_map] as m\r\ninner join \r\n sf_media_content as c on child_object_type_name in (1053642113, 675916353, 1719647903) and m.child_object_id = c.content_id\r\nwhere \r\n m.object_id <> c.parent_id",
                "delete\r\n [sf_permissions_inheritance_map]\r\nfrom\r\n [sf_permissions_inheritance_map] as m\r\ninner join \r\n sf_page_node as c on m.child_object_type_name = 1003093577 and m.child_object_id = c.id\r\nwhere \r\n m.object_id <> c.parent_id"
              };
              ISchemaHandler schemaHandler = newDatabase.GetSchemaHandler();
              foreach (string ddl in strArray)
              {
                try
                {
                  schemaHandler.ForceExecuteDDLScript(ddl);
                }
                catch (Exception ex)
                {
                  break;
                }
              }
            }
            if (previousBuild < SitefinityVersion.Sitefinity12_2.Build)
            {
              objectScope.Transaction.Begin();
              ExtentQuery<Telerik.Sitefinity.Metadata.Model.MetaType> source = objectScope.Extent<Telerik.Sitefinity.Metadata.Model.MetaType>();
              Expression<Func<Telerik.Sitefinity.Metadata.Model.MetaType, bool>> predicate = (Expression<Func<Telerik.Sitefinity.Metadata.Model.MetaType, bool>>) (t => t.ApplicationName == appName && t.BaseClassName == typeof (FormEntry).FullName);
              foreach (Telerik.Sitefinity.Metadata.Model.MetaType metaType in (IEnumerable<Telerik.Sitefinity.Metadata.Model.MetaType>) source.Where<Telerik.Sitefinity.Metadata.Model.MetaType>(predicate))
              {
                IList<MetaTypeAttribute> metaAttributes = metaType.MetaAttributes;
                MetaTypeAttribute metaTypeAttribute = new MetaTypeAttribute();
                metaTypeAttribute.Name = "useAutoGeneratedTableName";
                metaAttributes.Add(metaTypeAttribute);
              }
              objectScope.Transaction.Commit();
            }
          }
        }
      }
      return newDatabase;
    }

    internal static void TryFixDuplicateRecords(
      UpgradingContext context,
      string tableName,
      string groupBy,
      string aggregateColumn,
      string aggregateFunction)
    {
      string str = string.Format("Fixing duplicate records on '{0}' table", (object) tableName);
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendFormat("\r\ndelete\r\n   {0}\r\nfrom \r\n   {0}\r\ninner join\r\n(\r\n select {1}, count(*) as cnt, {3}({2}) as aggrcolumn\r\n from {0} \r\n group by {1}\r\n having count(*) > 1\r\n) as t1\r\non", (object) tableName, (object) groupBy, (object) aggregateColumn, (object) aggregateFunction);
      string[] strArray = groupBy.Split(',');
      for (int index = 0; index < strArray.Length; ++index)
      {
        if (index > 0)
          stringBuilder.Append("and");
        stringBuilder.AppendFormat(" {0}.{1} = t1.{1} ", (object) tableName, (object) strArray[index].Trim());
      }
      stringBuilder.AppendFormat("\r\nwhere\r\n {0}.{1} <> t1.aggrcolumn", (object) tableName, (object) aggregateColumn);
      try
      {
        context.ExecuteSQL(stringBuilder.ToString());
        Log.Write((object) string.Format("PASSED : {0}", (object) str), ConfigurationPolicy.UpgradeTrace);
      }
      catch (Exception ex)
      {
        Log.Write((object) string.Format("FAILED: {0} - {1}", (object) str, (object) ex.Message), ConfigurationPolicy.UpgradeTrace);
      }
    }

    internal static void FixDynamicLinks<TContent>(
      UpgradingContext context,
      IOpenAccessMetadataProvider provider,
      IQueryable<TContent> items = null,
      string propertyName = "Content")
      where TContent : IDataItem
    {
      if (!(TypeDescriptor.GetProperties(typeof (TContent)).Find(propertyName, false) is LstringPropertyDescriptor prop))
        return;
      string str = string.Format("Fix dynamic links for '{0}.{1}' in '{2}'", (object) typeof (TContent).FullName, (object) prop.Name, (object) provider.ModuleName);
      try
      {
        if (items == null)
          items = context.GetAll<TContent>();
        int count = 20;
        int num1 = Queryable.Count<TContent>(items);
        int num2 = 0;
        if (num1 > 0)
          num2 = (int) Math.Ceiling((double) num1 / (double) count);
        for (int index = 0; index < num2; ++index)
        {
          foreach (TContent content in (IEnumerable<TContent>) Queryable.Take<TContent>(Queryable.Skip<TContent>(items, index * count), count))
          {
            OpenAccessConnection.UnresolveDynamicLinks(prop, (IDataItem) content, CultureInfo.InvariantCulture);
            foreach (CultureInfo culture in (IEnumerable<CultureInfo>) AppSettings.CurrentSettings.AllLanguages.Values)
              OpenAccessConnection.UnresolveDynamicLinks(prop, (IDataItem) content, culture);
          }
          context.SaveChanges();
        }
        if (string.IsNullOrEmpty(str))
          return;
        Log.Write((object) string.Format("PASSED : {0}", (object) str), ConfigurationPolicy.UpgradeTrace);
      }
      catch (Exception ex)
      {
        context.ClearChanges();
        if (!string.IsNullOrEmpty(str))
          Log.Write((object) string.Format("FAILED: {0} - {1}", (object) str, (object) ex.Message), ConfigurationPolicy.UpgradeTrace);
        if (!Telerik.Sitefinity.Abstractions.Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
          return;
        throw;
      }
    }

    private static void UnresolveDynamicLinks(
      LstringPropertyDescriptor prop,
      IDataItem item,
      CultureInfo culture)
    {
      string html = prop.GetString((object) item, culture, false);
      if (html.IsNullOrWhitespace())
        return;
      prop.SetStringRaw((object) item, culture, LinkParser.UnresolveLinks(html));
    }

    private static int TryUpgradeFrom_4_0(
      IConnectionStringSettings connectionSettings,
      string appName)
    {
      int buildNumber = 0;
      OpenAccessConnection.UsingMsSqlTransaction(connectionSettings, (string) null, (System.Action<IDbCommand>) (cmd => buildNumber = OpenAccessConnection.GetPreviousBuildNumber(cmd, appName)));
      if (buildNumber > 0 && buildNumber < 1300)
        OpenAccessConnection.UsingMsSqlTransaction(connectionSettings, "OpenAccessConnection: Upgrade to 1300 from versions 4.0, 4.0 SP1", (System.Action<IDbCommand>) (cmd =>
        {
          cmd.ExecuteNonQuery("EXEC sp_rename 'page_language_link', 'sf_page_language_link'");
          cmd.ExecuteNonQuery("EXEC sp_rename 'subscription_info', 'sf_subscription_info'");
          cmd.ExecuteNonQuery("EXEC sp_rename 'sf_subscription_info.subscription_info_id', 'sf_subscription_info_id', 'COLUMN'");
          cmd.ExecuteNonQuery("EXEC sp_rename 'unsubscription_info', 'sf_unsubscription_info'");
          cmd.ExecuteNonQuery("EXEC sp_rename 'sf_unsubscription_info.unsubscription_info_id', 'sf_unsubscription_info_id', 'COLUMN'");
          try
          {
            cmd.ExecuteNonQuery("ALTER TABLE sf_meta_types ADD module_name VARCHAR(255) NULL");
          }
          catch
          {
          }
          cmd.ExecuteNonQuery("UPDATE sf_meta_types SET module_name = 'Telerik.Sitefinity.Publishing.Data.OpenAccessPublishingPointDynamicTypeProvider'\r\nWHERE name_space = 'Telerik.Sitefinity.Publishing.Model'");
          cmd.ExecuteNonQuery("UPDATE sf_meta_fields SET clr_type = 'System.String' WHERE clr_type = 'System.String[]' AND db_type IS NULL");
          cmd.ExecuteNonQuery("ALTER TABLE sf_url_data ADD is_default TINYINT NULL");
          cmd.ExecuteNonQuery("UPDATE sf_url_data SET [is_default] = 1 - [redirect]");
          cmd.ExecuteNonQuery("ALTER TABLE sf_url_data ALTER COLUMN is_default TINYINT NOT NULL");
          try
          {
            cmd.ExecuteNonQuery("alter table sf_taxa_attrbutes drop CONSTRAINT  pk_sf_taxa_attrbutes");
            cmd.ExecuteNonQuery("alter table sf_taxa_attrbutes alter column mapkey VARCHAR(255)");
          }
          catch
          {
          }
        }));
      return buildNumber;
    }

    private static void UpgradeDatabase(Database database, OpenAccessConnection connection) => OpenAccessConnection.UpgradeDatabase(database, connection, out ISchemaHandler _);

    private static void UpgradeDatabase(
      Database database,
      OpenAccessConnection connection,
      out ISchemaHandler schemaHandler)
    {
      schemaHandler = database.GetSchemaHandler();
      OpenAccessConnection.UpgradeDatabaseSchema(schemaHandler, connection);
    }

    private static void UpgradeDatabaseSchema(
      ISchemaHandler schemaHandler,
      OpenAccessConnection connection)
    {
      using (new MethodPerformanceRegion("Upgrade Database Schema"))
      {
        if (schemaHandler.DatabaseExists())
        {
          SchemaUpdateProperties props = new SchemaUpdateProperties();
          props.CheckExtraColumns = false;
          props.CheckExtraIndexes = false;
          int num = 3;
          while (num > 0)
          {
            --num;
            try
            {
              SchemaUpdateInfo updateInfo = schemaHandler.CreateUpdateInfo(props);
              if (updateInfo != null)
              {
                if (updateInfo.HasScript)
                {
                  try
                  {
                    NameValueCollection appSettings = ConfigurationManager.AppSettings;
                    if ((appSettings.Get("PreserveDBExtraConstraints") ?? "").Equals("true", StringComparison.InvariantCultureIgnoreCase))
                    {
                      string prefix = appSettings.Get("PreserveDBObjectsPrefix");
                      string[] array = ((IEnumerable<string>) updateInfo.Statements).Where<string>((Func<string, bool>) (p => !OpenAccessConnection.FilterOutDropDdlStatement(p, prefix))).ToArray<string>();
                      updateInfo.Statements = array;
                    }
                    if (connection != null)
                    {
                      if (connection.DbType != DatabaseType.MsSql)
                      {
                        if (connection.DbType != DatabaseType.SqlAzure)
                          goto label_15;
                      }
                      IEnumerable<string> source = ((IEnumerable<string>) updateInfo.Statements).Where<string>((Func<string, bool>) (s => !s.EndsWith(" FOREIGN KEY ([base_id]) REFERENCES [sf_dynamic_content]([base_id]) ON DELETE CASCADE") || s.StartsWith("ALTER TABLE [sf_")));
                      updateInfo.Statements = source.ToArray<string>();
                    }
                  }
                  catch (Exception ex)
                  {
                    if (Telerik.Sitefinity.Abstractions.Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
                      throw;
                  }
label_15:
                  if (updateInfo.HasScript)
                  {
                    OpenAccessConnection.LogSchemaInfo(updateInfo);
                    schemaHandler.ForceUpdateSchema(updateInfo);
                    foreach (object statement in updateInfo.Statements)
                      Log.Write(statement, ConfigurationPolicy.UpgradeTrace);
                  }
                }
              }
              break;
            }
            catch (Exception ex)
            {
              if (num != 0)
              {
                if (ex.Message.Contains("Timeout expired."))
                  continue;
              }
              throw;
            }
          }
        }
        else
        {
          schemaHandler.CreateDatabase();
          string ddlScript = schemaHandler.CreateDDLScript();
          schemaHandler.ExecuteDDLScript(ddlScript);
        }
      }
    }

    private static void ExecutePostInitializationUpgradeScripts()
    {
      if (OpenAccessConnection.previousSitefinityVersionNumber >= SitefinityVersion.Sitefinity11_1.Build)
        return;
      try
      {
        ConfigManager manager = ConfigManager.GetManager();
        DataConfig section = manager.GetSection<DataConfig>();
        OpenAccessConnection.UpgradeSplitTablesOptions(section);
        manager.SaveSection((ConfigSection) section);
      }
      catch (Exception ex)
      {
        if (Telerik.Sitefinity.Abstractions.Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
          throw ex;
      }
    }

    internal static void UpgradeSplitTablesOptions(DataConfig section)
    {
      IEnumerable<string> strings1 = ((IEnumerable<CultureInfo>) Config.Get<ResourcesConfig>().FrontendAndBackendCultures).Select<CultureInfo, string>((Func<CultureInfo, string>) (c => c.Name));
      DatabaseMappingOptionsElement databaseMappingOptions = section.DatabaseMappingOptions;
      if (!(databaseMappingOptions.MainFieldsIgnoredCultures == "ALLCULTURES") && databaseMappingOptions.UseMultilingualSplitTables)
        return;
      if (databaseMappingOptions.UseMultilingualSplitTables)
      {
        IEnumerable<string> strings2 = strings1;
        if (!databaseMappingOptions.SplitTablesIgnoredCultures.IsNullOrEmpty())
        {
          string[] second = databaseMappingOptions.SplitTablesIgnoredCultures.Replace(" ", "").Split(new char[1]
          {
            ','
          }, StringSplitOptions.RemoveEmptyEntries);
          strings2 = strings1.Except<string>((IEnumerable<string>) second);
        }
        if (strings2.Any<string>())
          databaseMappingOptions.MainFieldsIgnoredCultures = string.Join(",", strings2);
      }
      else
      {
        databaseMappingOptions.UseMultilingualSplitTables = true;
        if (strings1.Any<string>())
          databaseMappingOptions.SplitTablesIgnoredCultures = string.Join(",", strings1);
      }
      if (!(databaseMappingOptions.MainFieldsIgnoredCultures == "ALLCULTURES"))
        return;
      databaseMappingOptions.MainFieldsIgnoredCultures = string.Empty;
    }

    /// <summary>
    /// Method is used to filter out OpenAccess generated DDL statements that drop custom db constraints
    /// </summary>
    /// <param name="statement">The DDL statement to check for DROP CONSTRAINT keyword</param>
    /// <param name="prefix">The db objects prefix to exclude from dropping. if not set any drop constraint is filtered out</param>
    /// <returns>true if the statement is not to be executed and should be removed from the final script</returns>
    private static bool FilterOutDropDdlStatement(string statement, string prefix)
    {
      int length1 = "DROP CONSTRAINT".Length;
      int num1 = statement.IndexOf("DROP CONSTRAINT", 0, StringComparison.InvariantCultureIgnoreCase);
      if (num1 < 0)
      {
        length1 = "DROP FOREIGN KEY".Length;
        num1 = statement.IndexOf("DROP FOREIGN KEY", 0, StringComparison.InvariantCultureIgnoreCase);
      }
      if (num1 < 0)
        return false;
      if (string.IsNullOrEmpty(prefix))
        return true;
      int startIndex = num1 + length1 + 2;
      int num2 = statement.IndexOfAny(OpenAccessConnection.closingTags, startIndex);
      if (num2 < 0)
        return false;
      int length2 = num2 - startIndex;
      return statement.Substring(startIndex, length2).StartsWith(prefix);
    }

    private static void LogSchemaInfo(SchemaUpdateInfo updateInfo)
    {
      SchemaMigrationReport report = new SchemaMigrationReportGenerator(updateInfo).GenerateReport();
      Log.Write((object) string.Format("Db Migration Complexity: {0}", (object) report.MigrationType), ConfigurationPolicy.UpgradeTrace);
      OpenAccessConnection.LogMigrationOperation(report.NewEntities);
      OpenAccessConnection.LogMigrationOperation(report.ModifiedEntities);
      OpenAccessConnection.LogMigrationOperation(report.RemovedEntities);
      OpenAccessConnection.LogMigrationOperation(report.TempTableUsage);
    }

    private static void LogMigrationOperation(IEnumerable<string> operations)
    {
      foreach (object operation in operations)
        Log.Write(operation, ConfigurationPolicy.UpgradeTrace);
    }

    internal static IConnectionStringSettings GetConnectionStringSettings(
      string connectionStringName)
    {
      DataConfig dataConfig = Config.Get<DataConfig>();
      IConnectionStringSettings connString;
      if (dataConfig.TryGetConnectionString(connectionStringName, out connString) || dataConfig.TryGetConnectionString("SitefinityStartupTempConnection", out connString))
        return connString;
      return connectionStringName.StartsWith("_Sitefinity_") ? (IConnectionStringSettings) new ConnectionStringSettingsWrapper(connectionStringName, OpenAccessConnection.GetConnectionStringSettings("Sitefinity")) : throw new ConnectionNotFoundException(connectionStringName);
    }

    internal static IConnectionStringSettings GetConnectionStringSettings(
      IOpenAccessDataProvider oaProvider)
    {
      return OpenAccessConnection.GetConnectionStringSettings(oaProvider.Context.ConnectionId);
    }

    private static int GetPreviousBuildNumber(IDbCommand cmd, string appName)
    {
      int previousBuildNumber = 0;
      try
      {
        cmd.CommandText = string.Format("select max([version_number]) \r\nFROM [sf_schema_vrsns] \r\nwhere app_name = '{0}' and module_name = '{1}'", (object) appName, (object) "Sitefinity");
        previousBuildNumber = (int) cmd.ExecuteScalar();
      }
      catch
      {
        try
        {
          cmd.CommandText = "SELECT TOP 1 version_number FROM sf_schema_versions where assembly_name = 'Telerik.Sitefinity.Model'";
          previousBuildNumber = (int) cmd.ExecuteScalar();
        }
        catch
        {
        }
      }
      return previousBuildNumber;
    }

    private static OpenAccessConnection.IMetadataUpdateStrategy GetMetadataUpdateStrategy(
      string metadataUpdateStrategyName)
    {
      if (ObjectFactory.IsTypeRegistered<OpenAccessConnection.IMetadataUpdateStrategy>(metadataUpdateStrategyName))
        return ObjectFactory.Resolve<OpenAccessConnection.IMetadataUpdateStrategy>(metadataUpdateStrategyName);
      if (!metadataUpdateStrategyName.IsNullOrEmpty())
      {
        if (metadataUpdateStrategyName == "Isolated")
          return (OpenAccessConnection.IMetadataUpdateStrategy) new OpenAccessConnection.IsolatedMetadataUpdateStrategy();
        Type type = TypeResolutionService.ResolveType(metadataUpdateStrategyName, false);
        if (type != (Type) null && typeof (OpenAccessConnection.IMetadataUpdateStrategy).IsAssignableFrom(type))
          return (OpenAccessConnection.IMetadataUpdateStrategy) Activator.CreateInstance(type);
      }
      return (OpenAccessConnection.IMetadataUpdateStrategy) new OpenAccessConnection.OptimizedMetadataUpdateStrategy();
    }

    private static SqlConnection OpenMsSqlConnection(string connectionString)
    {
      SqlConnection sqlConnection = new SqlConnection(connectionString);
      sqlConnection.Open();
      return sqlConnection;
    }

    internal static SqlConnection OpenMsSqlConnection(OpenAccessConnection oaConnection) => oaConnection.DbType != DatabaseType.MsSql ? (SqlConnection) null : OpenAccessConnection.OpenMsSqlConnection(oaConnection.ConnectionString);

    internal static SqlConnection OpenMsSqlConnection(
      IConnectionStringSettings connectionSettings)
    {
      return connectionSettings.DatabaseType != DatabaseType.MsSql ? (SqlConnection) null : OpenAccessConnection.OpenMsSqlConnection(connectionSettings.ConnectionString);
    }

    internal static SqlConnection OpenMsSqlConnection(
      IOpenAccessDataProvider oaProvider)
    {
      return OpenAccessConnection.OpenMsSqlConnection(OpenAccessConnection.GetConnectionStringSettings(oaProvider));
    }

    internal static SqlConnection OpenMsSqlConnection(Database database) => database.BackendConfiguration.Backend != OABackendNameAttribute.GetName((Enum) DatabaseType.MsSql) ? (SqlConnection) null : OpenAccessConnection.OpenMsSqlConnection(database.GetEffectiveConnectionString());

    internal static void UsingMsSqlTransaction(
      OpenAccessConnection oaConnection,
      string description,
      System.Action<IDbCommand> action)
    {
      OpenAccessConnection.UsingTransaction((IDbConnection) OpenAccessConnection.OpenMsSqlConnection(oaConnection), description, action);
    }

    internal static void UsingMsSqlTransaction(
      IConnectionStringSettings connectionSettings,
      string description,
      System.Action<IDbCommand> action)
    {
      OpenAccessConnection.UsingTransaction((IDbConnection) OpenAccessConnection.OpenMsSqlConnection(connectionSettings), description, action);
    }

    internal static void UsingMsSqlTransaction(
      IOpenAccessDataProvider oaProvider,
      string description,
      System.Action<IDbCommand> action)
    {
      OpenAccessConnection.UsingTransaction((IDbConnection) OpenAccessConnection.OpenMsSqlConnection(oaProvider), description, action);
    }

    internal static void UsingMsSqlTransaction(
      Database database,
      string description,
      System.Action<IDbCommand> action)
    {
      OpenAccessConnection.UsingTransaction((IDbConnection) OpenAccessConnection.OpenMsSqlConnection(database), description, action);
    }

    /// <summary>Usings the ms SQL transaction.</summary>
    /// <param name="connection">The connection.</param>
    /// <param name="description">The description. This is used when logging pass/fail in the UpgradeTrace.log</param>
    /// <param name="action">The action.</param>
    internal static void UsingTransaction(
      IDbConnection connection,
      string description,
      System.Action<IDbCommand> action)
    {
      if (connection == null)
        return;
      using (connection)
      {
        IDbTransaction dbTransaction = connection.BeginTransaction();
        try
        {
          using (IDbCommand command = connection.CreateCommand())
          {
            command.Transaction = dbTransaction;
            action(command);
          }
          dbTransaction.Commit();
          if (string.IsNullOrEmpty(description))
            return;
          Log.Write((object) string.Format("PASSED : {0}", (object) description), ConfigurationPolicy.UpgradeTrace);
        }
        catch (Exception ex)
        {
          dbTransaction.Rollback();
          if (!string.IsNullOrEmpty(description))
            Log.Write((object) string.Format("FAILED: {0} - {1}", (object) description, (object) ex.Message), ConfigurationPolicy.UpgradeTrace);
          if (!Telerik.Sitefinity.Abstractions.Exceptions.HandleException(ex, ExceptionPolicyName.DataProviders))
            return;
          throw;
        }
      }
    }

    internal static void Upgrade(
      UpgradingContext context,
      string upgradeDescription,
      string upgradeScript)
    {
      OpenAccessConnection.Upgrade(new DatabaseType?(), context, upgradeDescription, upgradeScript);
    }

    internal static void Upgrade(
      DatabaseType? targetDbType,
      UpgradingContext context,
      string upgradeDescription,
      string upgradeScript)
    {
      if (targetDbType.HasValue)
      {
        DatabaseType? nullable = targetDbType;
        DatabaseType dbType = context.Connection.DbType;
        if (!(nullable.GetValueOrDefault() == dbType & nullable.HasValue))
          return;
      }
      try
      {
        context.ExecuteNonQuery(upgradeScript);
        context.SaveChanges();
        Log.Write((object) string.Format("PASSED : {0}", (object) upgradeDescription), ConfigurationPolicy.UpgradeTrace);
      }
      catch (Exception ex)
      {
        context.ClearChanges();
        Log.Write((object) string.Format("FAILED: {0} - {1}", (object) upgradeDescription, (object) ex.Message), ConfigurationPolicy.UpgradeTrace);
        if (!Telerik.Sitefinity.Abstractions.Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
          return;
        throw;
      }
    }

    internal static void MsSqlUpgrade(
      OpenAccessConnection oaConnection,
      string upgradeDescription,
      System.Action<IDbCommand> action)
    {
      OpenAccessConnection.MsSqlUpgrade(OpenAccessConnection.OpenMsSqlConnection(oaConnection), upgradeDescription, action);
    }

    internal static void MsSqlUpgrade(
      IOpenAccessDataProvider oaProvider,
      string upgradeDescription,
      System.Action<IDbCommand> action)
    {
      OpenAccessConnection.MsSqlUpgrade(OpenAccessConnection.OpenMsSqlConnection(oaProvider), upgradeDescription, action);
    }

    internal static void MsSqlUpgrade(
      Database database,
      string upgradeDescription,
      System.Action<IDbCommand> action)
    {
      OpenAccessConnection.MsSqlUpgrade(OpenAccessConnection.OpenMsSqlConnection(database), upgradeDescription, action);
    }

    internal static void MsSqlUpgrade(
      SqlConnection connection,
      string upgradeDescription,
      System.Action<IDbCommand> action)
    {
      OpenAccessConnection.UsingTransaction((IDbConnection) connection, upgradeDescription, (System.Action<IDbCommand>) (cmd => action(cmd)));
    }

    /// <summary>
    /// Upgrades LString columns in Oracle database (including its multilingual versions if any).
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="tableName">Name of the table.</param>
    /// <param name="columnName">Name of the column that will be upgraded.</param>
    /// <param name="newDBType">The target db type.</param>
    /// <param name="upgradeDescription">The message that will be logged while upgrading.</param>
    public static void OracleUpgradeLStringColumn(
      UpgradingContext context,
      string tableName,
      string columnName,
      string newDBType,
      string upgradeDescription)
    {
      OpenAccessConnection.OracleUpgrade(new DatabaseType?(DatabaseType.Oracle), context, upgradeDescription, context.DatabaseContext.GetColumnTypeMigrationScript(tableName, columnName, newDBType));
      string format = columnName + "{0}";
      foreach (CultureInfo andBackendCulture in Config.Get<ResourcesConfig>().FrontendAndBackendCultures)
      {
        string cultureSuffix = LstringPropertyDescriptor.GetCultureSuffix(andBackendCulture);
        string columnName1 = string.Format(format, (object) cultureSuffix);
        OpenAccessConnection.OracleUpgrade(new DatabaseType?(DatabaseType.Oracle), context, upgradeDescription, context.DatabaseContext.GetColumnTypeMigrationScript(tableName, columnName1, newDBType));
      }
    }

    /// <summary>
    /// Performs an upgrade calling ExecuteNonQuery context method against an Oracle database.
    /// </summary>
    /// <param name="targetDbType">Type of the target db.</param>
    /// <param name="context">The context.</param>
    /// <param name="upgradeDescription">The upgrade description.</param>
    /// <param name="upgradeScript">The upgrade script.</param>
    internal static void OracleUpgrade(
      DatabaseType? targetDbType,
      UpgradingContext context,
      string upgradeDescription,
      string upgradeScript)
    {
      if (targetDbType.HasValue)
      {
        DatabaseType? nullable = targetDbType;
        DatabaseType dbType = context.Connection.DbType;
        if (!(nullable.GetValueOrDefault() == dbType & nullable.HasValue))
          return;
      }
      if (string.IsNullOrWhiteSpace(upgradeScript))
        return;
      try
      {
        context.ExecuteNonQuery(upgradeScript);
        Log.Write((object) string.Format("PASSED : {0}", (object) upgradeDescription), ConfigurationPolicy.UpgradeTrace);
      }
      catch (Exception ex)
      {
        Log.Write((object) string.Format("FAILED: {0} - {1}", (object) upgradeDescription, (object) ex.Message), ConfigurationPolicy.UpgradeTrace);
        if (!Telerik.Sitefinity.Abstractions.Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
          return;
        throw;
      }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.OA.OpenAccessConnection" /> class.
    /// </summary>
    /// <param name="connectionSettings">The connection settings.</param>
    public OpenAccessConnection(IConnectionStringSettings connectionSettings)
      : this(connectionSettings, (BackendConfiguration) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.OA.OpenAccessConnection" /> class.
    /// </summary>
    /// <param name="connectionSettings">The connection settings.</param>
    /// <param name="backendConfiguration">The backend configuration.</param>
    public OpenAccessConnection(
      IConnectionStringSettings connectionSettings,
      BackendConfiguration backend)
      : this(connectionSettings.Name, connectionSettings.ConnectionString, connectionSettings.DatabaseType, backend, connectionSettings.Parameters)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.OA.OpenAccessConnection" /> class.
    /// </summary>
    /// <param name="connectionName">Name of the connection.</param>
    /// <param name="connectionString">The connection string.</param>
    /// <param name="dbType">Type of the db.</param>
    /// <param name="backendConfiguration">The backend configuration.</param>
    public OpenAccessConnection(
      string connectionName,
      string connectionString,
      DatabaseType dbType,
      BackendConfiguration backend)
      : this(connectionName, connectionString, dbType, backend, (NameValueCollection) null)
    {
    }

    internal OpenAccessConnection(
      string connectionName,
      string connectionString,
      DatabaseType dbType,
      BackendConfiguration backend,
      NameValueCollection config)
    {
      this.connectionName = connectionName;
      this.originalConnString = connectionString;
      this.dbType = dbType;
      string metadataUpdateStrategyName = (string) null;
      if (config != null)
      {
        metadataUpdateStrategyName = config[nameof (metadataUpdateStrategy)];
        string str1 = config[nameof (ignoreDowngradeExceptions)];
        bool result1;
        if (!string.IsNullOrEmpty(str1) && bool.TryParse(str1, out result1))
          this.ignoreDowngradeExceptions = result1;
        string str2 = config[nameof (replication)];
        if (!string.IsNullOrEmpty(str2))
        {
          if (!LicenseState.Current.LicenseInfo.CheckIsModuleLicensed("81346463-63E8-41E0-894F-A34955B377CC"))
          {
            Log.Write((object) string.Format("No Multi-regional deployment license. Connection '{0}' will have replication of None.", (object) connectionName), ConfigurationPolicy.Trace);
          }
          else
          {
            OpenAccessConnection.ReplicationMode result2;
            if (Enum.TryParse<OpenAccessConnection.ReplicationMode>(str2, out result2))
              this.replication = result2;
          }
        }
        string str3 = config[nameof (readOnly)];
        if (!string.IsNullOrEmpty(str3))
        {
          if (!LicenseState.Current.LicenseInfo.CheckIsModuleLicensed("81346463-63E8-41E0-894F-A34955B377CC"))
          {
            Log.Write((object) string.Format("No Multi-regional deployment license. Connection '{0}' will not be initialized as read-only.", (object) connectionName), ConfigurationPolicy.Trace);
          }
          else
          {
            bool result3;
            if (bool.TryParse(str3, out result3))
              this.readOnly = result3;
          }
        }
      }
      if (backend == null)
      {
        backend = OpenAccessConnection.CreateBackendConfiguration(dbType, connectionName);
        if (OpenAccessConnection.BackendConfigurationInit != null)
          OpenAccessConnection.BackendConfigurationInit(this, backend);
        if (SystemManager.IsUpgrading)
        {
          if (backend.Runtime.CommandTimeout < 300)
          {
            backend.ConnectionPool.ActiveConnectionTimeout = 3600;
            backend.Runtime.CommandTimeout = 3600;
          }
          backend.ConnectionPool.IsolationLevel = IsolationLevel.ReadUncommitted;
        }
        if (this.ReadOnly)
        {
          backend.Runtime.ReadOnly = true;
          backend.Runtime.HasTempTables = false;
        }
      }
      this.backendConfiguration = backend;
      this.metadataUpdateStrategy = OpenAccessConnection.GetMetadataUpdateStrategy(metadataUpdateStrategyName);
    }

    /// <summary>
    /// Gets the name of the connection used as a key to get this instance.
    /// </summary>
    /// <value>The name.</value>
    public string Name => this.connectionName;

    /// <summary>Gets the connection string.</summary>
    /// <value>The connection string.</value>
    public string ConnectionString => this.originalConnString;

    /// <summary>Gets the of the current db type.</summary>
    /// <value>The type of the db.</value>
    public DatabaseType DbType => this.dbType;

    public BackendConfiguration Backend => this.backendConfiguration;

    /// <summary>
    /// Gets the current context and ensures that it has the mapping returned by the IOpenAccessMetadataProvider.
    /// </summary>
    /// <param name="provider">The provider.</param>
    /// <returns></returns>
    public SitefinityOAContext GetContext(IOpenAccessMetadataProvider provider)
    {
      if (provider == null)
        throw new ArgumentException("provider cannot be null");
      this.RegisterProviderInternal(provider, true);
      return this.GetCurrentContext(provider);
    }

    public IObjectScope GetObjectScope(IOpenAccessMetadataProvider provider)
    {
      if (provider == null)
        throw new ArgumentException("provider cannot be null");
      this.RegisterProviderInternal(provider, true);
      return this.currentDatabase.GetObjectScope();
    }

    /// <summary>
    /// Gets the current context without ensuring that it has required mapping.
    /// </summary>
    /// <returns></returns>
    internal SitefinityOAContext GetCurrentContext(
      IOpenAccessMetadataProvider provider)
    {
      SitefinityOAContext currentContext = !(provider is IOpenAccessCustomContextProvider customContextProvider) ? new SitefinityOAContext(this.currentConnectionString, this.backendConfiguration, this.currentMetadataContainer) : customContextProvider.GetContext(this.currentConnectionString, this.backendConfiguration, this.currentMetadataContainer);
      currentContext.OpenAccessConnection = this;
      currentContext.Provider = provider;
      currentContext.FetchLanguadeSpecificData(CultureInfo.CurrentUICulture);
      return currentContext;
    }

    internal bool IgnoreDowngradeExceptions => this.ignoreDowngradeExceptions;

    internal OpenAccessConnection.ReplicationMode Replication => this.replication;

    internal bool ReadOnly => this.readOnly;

    internal MetadataContainer CurrentMetadataContainer => this.currentMetadataContainer;

    internal bool AllowFetchStrategy => this.fetchStrategyCache != null;

    internal FetchStrategyResolver FetchStrategyCache => this.fetchStrategyCache;

    /// <summary>
    /// Registers the provider to this connection, if it si not registered, and extends it with the provider model.
    /// </summary>
    /// <param name="provider">The provider.</param>
    public void RegisterProvider(IOpenAccessMetadataProvider provider) => this.RegisterProviderInternal(provider, false);

    private static SchemaVersion GetSchemaVersion(
      string moduleName,
      string connectionName)
    {
      ICacheManager cacheManager = SystemManager.GetCacheManager(CacheManagerInstance.Internal);
      string cacheDependencyKey = SchemaVersion.GetCacheDependencyKey(connectionName);
      if (!(cacheManager[cacheDependencyKey] is IEnumerable<SchemaVersion> schemaVersions1))
      {
        lock (OpenAccessConnection.schemaCacheLock)
        {
          if (!(cacheManager[cacheDependencyKey] is IEnumerable<SchemaVersion> schemaVersions1))
          {
            MetadataManager manager = MetadataManager.GetManager(OpenAccessConnection.defaultSchemaUpgradeProviderName, "OAC_Upgrading_0123");
            TransactionManager.DisposeTransaction("OAC_Upgrading_0123");
            schemaVersions1 = (IEnumerable<SchemaVersion>) manager.Provider.GetSchemaVersions().Where<SchemaVersion>((Expression<Func<SchemaVersion, bool>>) (x => x.ConnectionId == connectionName)).ToList<SchemaVersion>();
            if (manager.Provider is IOpenAccessDataProvider provider)
            {
              schemaVersions1 = provider.GetContext().CreateDetachedCopy<SchemaVersion>(schemaVersions1, (FetchStrategy) null);
              cacheManager.Add(cacheDependencyKey, (object) schemaVersions1, CacheItemPriority.Normal, (ICacheItemRefreshAction) null, (ICacheItemExpiration) new DataItemCacheDependency(typeof (SchemaVersion), cacheDependencyKey), (ICacheItemExpiration) new SlidingTime(TimeSpan.FromMinutes(5.0)));
            }
          }
        }
      }
      return schemaVersions1 != null ? schemaVersions1.FirstOrDefault<SchemaVersion>((Func<SchemaVersion, bool>) (x => x.ModuleName == moduleName)) : (SchemaVersion) null;
    }

    private bool TryAddProviderInAggregationGroup(
      IOpenAccessMetadataProvider provider,
      string moduleName)
    {
      if (provider is IOpenAccessSelfUpgradableProvider)
        return false;
      if (this.providersInGroup != null && this.providersInGroup.ContainsKey(moduleName))
        return true;
      if (!(provider.GetMetaDataSource(this.GetFluentMappingContext()) is SitefinityMetadataSourceBase))
        return false;
      SchemaVersion schemaVersion = MetadataManager.GetManager(OpenAccessConnection.defaultSchemaUpgradeProviderName, "OAC_Upgrading_0123").GetSchemaVersions().Where<SchemaVersion>((Expression<Func<SchemaVersion, bool>>) (s => s.ModuleName == moduleName && s.ConnectionId == this.connectionName)).SingleOrDefault<SchemaVersion>();
      if (schemaVersion == null)
        return false;
      OpenAccessConnection.UpgradeParams upgradeParams = OpenAccessConnection.MetadataUpgradeFacade.CheckForUpgrade(provider, this, moduleName, (ISitefinityMetadataSource) null, schemaVersion);
      if (upgradeParams == null || (upgradeParams.UpgradeProvider ? 1 : (upgradeParams.UpgradeSchema ? 1 : 0)) != 0)
        return false;
      if (this.providersInGroup == null)
        this.providersInGroup = (IDictionary<string, IOpenAccessMetadataProvider>) new Dictionary<string, IOpenAccessMetadataProvider>();
      this.providersInGroup[moduleName] = provider;
      return true;
    }

    private bool TryRemoveProviderFromAggregationGroup(string moduleName)
    {
      if (this.providersInGroup == null || !this.providersInGroup.ContainsKey(moduleName))
        return false;
      this.providersInGroup.Remove(moduleName);
      return true;
    }

    internal void RegisterProviderInternal(
      IOpenAccessMetadataProvider provider,
      bool forceDatabaseInit)
    {
      string moduleName = provider.GetModuleName();
      if (this.registeredModules.Contains(moduleName))
        return;
      lock (this.registeredModules)
      {
        if (this.registeredModules.Contains(moduleName) || !forceDatabaseInit && SystemManager.DelayedDatabaseInit && this.TryAddProviderInAggregationGroup(provider, moduleName))
          return;
        MetadataSource metadataSource = (MetadataSource) null;
        if (forceDatabaseInit && SystemManager.DelayedDatabaseInit || !this.TryGetCombinedMetadataSource(moduleName, out metadataSource))
        {
          IDatabaseMappingContext fluentMappingContext = this.GetFluentMappingContext();
          fluentMappingContext.ModuleName = moduleName;
          metadataSource = provider.GetMetaDataSource(fluentMappingContext);
        }
        OpenAccessConnection.CombinedMetadataSource combinedMetadataSource = (OpenAccessConnection.CombinedMetadataSource) null;
        if (metadataSource != null)
        {
          combinedMetadataSource = metadataSource as OpenAccessConnection.CombinedMetadataSource;
          this.Extend(metadataSource, moduleName, provider, combinedMetadataSource?.Modules);
        }
        if (combinedMetadataSource != null)
        {
          foreach (KeyValuePair<string, string[]> module in (IEnumerable<KeyValuePair<string, string[]>>) combinedMetadataSource.Modules)
            this.registeredModules.Add(module.Key);
        }
        else
        {
          if (SystemManager.DelayedDatabaseInit)
            this.TryRemoveProviderFromAggregationGroup(moduleName);
          this.registeredModules.Add(moduleName);
        }
      }
    }

    /// <summary>
    /// Gets the fluent mapping context for the current connection.
    /// </summary>
    /// <returns></returns>
    public IDatabaseMappingContext GetFluentMappingContext() => OpenAccessConnection.CreateFluentMappingContext(this.dbType);

    private void Extend(
      MetadataSource metadataSource,
      string moduleName,
      IOpenAccessMetadataProvider provider,
      IDictionary<string, string[]> combinedModules = null)
    {
      using (new MethodPerformanceRegion("Extend metadata container. Module: ({0}).".Arrange((object) moduleName)))
      {
        this.AddCommonMappings(metadataSource);
        this.UpdateMetadata(metadataSource, moduleName, provider, (IEnumerable<string>) combinedModules?.Keys);
        if (this.dynamicTypesToRegister.Count > 0)
        {
          string[] array = this.dynamicTypesToRegister.Where<DynamicTypeInfo>((Func<DynamicTypeInfo, bool>) (t => t.IsArtificial && !t.IsDeleted)).Select<DynamicTypeInfo, string>((Func<DynamicTypeInfo, string>) (t => t.Name)).ToArray<string>();
          if (array.Length != 0)
          {
            using (SitefinityOAContext currentContext = this.GetCurrentContext(provider))
            {
              foreach (string typeName in array)
              {
                IPersistentTypeDescriptor persistentTypeDescriptor = currentContext.PersistentMetaData.GetPersistentTypeDescriptor(typeName);
                if (persistentTypeDescriptor != null)
                  TypeResolutionService.RegisterType(persistentTypeDescriptor.DescribedType);
              }
            }
          }
        }
        if (OpenAccessConnection.metadataInitalized && this.GetMappingOptions(metadataSource).UseMultilingualFetchStrategy)
        {
          if (this.fetchStrategyCache == null)
            this.fetchStrategyCache = new FetchStrategyResolver();
          ICollection<CultureInfo> values = AppSettings.CurrentSettings.AllLanguages.Values;
          if (combinedModules != null)
          {
            foreach (KeyValuePair<string, string[]> combinedModule in (IEnumerable<KeyValuePair<string, string[]>>) combinedModules)
              this.fetchStrategyCache.RegisterModuleFetchPlanFragments(combinedModule.Key, (IEnumerable<string>) combinedModule.Value, (IEnumerable<CultureInfo>) values);
          }
          else
          {
            IEnumerable<string> strings = metadataSource.GetModel().PersistentTypes.Select<MetaPersistentType, string>((Func<MetaPersistentType, string>) (p => p.FullName));
            if (strings.Count<string>() > 0)
              this.fetchStrategyCache.RegisterModuleFetchPlanFragments(moduleName, strings, (IEnumerable<CultureInfo>) values);
          }
        }
        OpenAccessConnection.Connection_MetadataChanged((object) this, EventArgs.Empty);
      }
    }

    private void AddBaseMappings(
      MetadataSource metadataSource,
      SitefinityMetadataSourceBase commonDataSource,
      string moduleName)
    {
      if (metadataSource is SitefinityMetadataSourceBase metadataSourceBase)
        metadataSourceBase.Combine(commonDataSource);
      else
        this.UpdateMetadata((MetadataSource) commonDataSource, moduleName, (IOpenAccessMetadataProvider) null);
    }

    private void AddCommonMappings(MetadataSource metadataSource)
    {
      if (!this.loadCommonMapping)
        return;
      IDatabaseMappingContext fluentMappingContext = this.GetFluentMappingContext();
      this.AddBaseMappings(metadataSource, (SitefinityMetadataSourceBase) new DynamicBaseMetadataSource(fluentMappingContext), "SitefinityDynamicBase");
      this.loadCommonMapping = false;
    }

    private IDatabaseMappingOptions GetMappingOptions(
      MetadataSource metadataSource)
    {
      if (metadataSource is SitefinityMetadataSourceBase metadataSourceBase)
        return metadataSourceBase.MappingOptions;
      DatabaseMappingOptions mappingOptions = new DatabaseMappingOptions();
      mappingOptions.LoadDefaults();
      return (IDatabaseMappingOptions) mappingOptions;
    }

    private void UpdateMetadata(
      MetadataSource metadataSource,
      string moduleName,
      IOpenAccessMetadataProvider provider,
      IEnumerable<string> combinedModules = null)
    {
      using (new MethodPerformanceRegion("Update meta data. MetadataSource: {0}".Arrange((object) metadataSource.GetType().Name)))
      {
        ISitefinityMetadataSource sitefinityMetaDataSource = OpenAccessConnection.GetSitefinityMetaDataSource(metadataSource, this.GetFluentMappingContext());
        OpenAccessConnection.MetadataExtendMonitor metadataExtendMonitor = (OpenAccessConnection.MetadataExtendMonitor) null;
        string[] currentlyRegisteredModules = this.registeredModules.ToArray<string>();
        int num = 0;
        MetadataContainer currentModel;
        while (true)
        {
          do
          {
            do
            {
              MetadataSourceAggregator sourceAggregator;
              using (new MethodPerformanceRegion("Aggregation"))
              {
                sourceAggregator = new MetadataSourceAggregator(this.currentMetadataContainer);
                try
                {
                  if (OpenAccessConnection.DiagnosticMode >= OpenAccessConnection.OADiagnosticMode.Detailed)
                  {
                    MetadataContainer model = metadataSource.GetModel();
                    object newModules = (object) combinedModules;
                    if (newModules == null)
                      newModules = (object) new string[1]
                      {
                        moduleName
                      };
                    metadataExtendMonitor = new OpenAccessConnection.MetadataExtendMonitor("Extend", this, model, (IEnumerable<string>) newModules);
                  }
                  using (new MethodPerformanceRegion("Extend"))
                    sourceAggregator.Extend(metadataSource);
                }
                catch (Exception ex)
                {
                  throw new Exception("Unable to merge MetadataSource provided by '{0}': {1}".Arrange((object) moduleName, (object) ex.Message), ex);
                }
              }
              currentModel = sourceAggregator.CurrentModel;
              OpenAccessConnection.InitMetadataContainer(currentModel);
              metadataExtendMonitor?.WriteAggregatedContainer(currentModel);
            }
            while (!this.metadataUpdateStrategy.ReplaceMetadata(this, currentModel, moduleName, sitefinityMetaDataSource, provider));
            if (this.registeredModules.Count == currentlyRegisteredModules.Length || num++ >= 2)
              goto label_23;
          }
          while (OpenAccessConnection.DiagnosticMode < OpenAccessConnection.OADiagnosticMode.Minimal);
          IEnumerable<string> values = this.registeredModules.Where<string>((Func<string, bool>) (module => !((IEnumerable<string>) currentlyRegisteredModules).Contains<string>(module)));
          Log.Write((object) "Meta data aggregation dependency problem has been detected: '{0}' depends on '{1}', which is causing the updating the meta data to be executed twice. Please make sure that there is no module dependent logic in the default constructors of the persisted types".Arrange((object) (combinedModules != null ? string.Join(",", combinedModules) : moduleName), (object) string.Join(",", values)), ConfigurationPolicy.Trace);
        }
label_23:
        this.currentMetadataContainer = currentModel;
        DynamicTypeInfo[] dynamicTypes = sitefinityMetaDataSource.DynamicTypes;
        if (dynamicTypes != null && dynamicTypes.Length != 0)
          this.dynamicTypesToRegister.AddRange((IEnumerable<DynamicTypeInfo>) dynamicTypes);
        if (metadataExtendMonitor == null)
          return;
        metadataExtendMonitor.WriteCurrentDatabaseTypes();
        metadataExtendMonitor.Flush();
      }
    }

    private bool TryGetCombinedMetadataSource(string moduleName, out MetadataSource metadataSource)
    {
      metadataSource = (MetadataSource) null;
      if (this.providersInGroup == null || !this.providersInGroup.ContainsKey(moduleName))
        return false;
      IDatabaseMappingContext fluentMappingContext = this.GetFluentMappingContext();
      List<MappingConfiguration> mapConfigs = new List<MappingConfiguration>();
      Dictionary<string, string[]> dictionary = new Dictionary<string, string[]>();
      foreach (KeyValuePair<string, IOpenAccessMetadataProvider> keyValuePair in (IEnumerable<KeyValuePair<string, IOpenAccessMetadataProvider>>) this.providersInGroup)
      {
        if (keyValuePair.Value.GetMetaDataSource(fluentMappingContext) is SitefinityMetadataSourceBase metaDataSource)
        {
          mapConfigs.AddRange((IEnumerable<MappingConfiguration>) metaDataSource.CustomMappingConfiguration);
          string[] array = metaDataSource.GetModel().PersistentTypes.Select<MetaPersistentType, string>((Func<MetaPersistentType, string>) (x => x.FullName)).ToArray<string>();
          dictionary.Add(keyValuePair.Key, array);
        }
      }
      if (this.loadCommonMapping)
      {
        mapConfigs.AddRange((IEnumerable<MappingConfiguration>) new DynamicBaseMetadataSource(fluentMappingContext).CustomMappingConfiguration);
        this.loadCommonMapping = false;
      }
      metadataSource = (MetadataSource) new OpenAccessConnection.CombinedMetadataSource(fluentMappingContext, (IList<MappingConfiguration>) mapConfigs);
      ((OpenAccessConnection.CombinedMetadataSource) metadataSource).Modules = (IDictionary<string, string[]>) dictionary;
      this.providersInGroup = (IDictionary<string, IOpenAccessMetadataProvider>) null;
      return true;
    }

    private bool ShouldBeReset(IEnumerable<string> modulesFilter)
    {
      if (modulesFilter == null || modulesFilter.Count<string>() == 0)
        return true;
      foreach (string str in modulesFilter)
      {
        if (this.registeredModules.Contains(str) || this.providersInGroup != null && this.providersInGroup.Keys.Contains(str))
          return true;
      }
      return false;
    }

    internal void Unlock() => Monitor.Exit((object) this.registeredModules);

    internal void Lock() => Monitor.Enter((object) this.registeredModules);

    private bool IsLocked() => Monitor.IsEntered((object) this.registeredModules);

    private void Reset()
    {
      lock (this.registeredModules)
      {
        this.registeredModules.Clear();
        if (this.providersInGroup != null)
          this.providersInGroup.Clear();
        this.fetchStrategyCache = (FetchStrategyResolver) null;
        this.currentMetadataContainer = (MetadataContainer) null;
        if (this.initialState == null)
          return;
        this.currentMetadataContainer = this.initialState.MetadataContainer;
        foreach (string registeredModule in this.initialState.RegisteredModules)
          this.registeredModules.Add(registeredModule);
      }
    }

    private void SetAsInitialState() => this.initialState = new OpenAccessConnection.OAConnectionState(this.currentMetadataContainer, (IEnumerable<string>) this.registeredModules);

    internal static ISitefinityMetadataSource GetSitefinityMetaDataSource(
      MetadataSource original,
      IDatabaseMappingContext mappingContext)
    {
      if (!(original is ISitefinityMetadataSource sitefinityMetaDataSource))
        sitefinityMetaDataSource = (ISitefinityMetadataSource) new SitefinityMetadataSourceWrapper(original, mappingContext);
      return sitefinityMetaDataSource;
    }

    private void HandleDatabaseUpgrade(
      MetadataContainer newMetadataContainer,
      string moduleName,
      ISitefinityMetadataSource sitefinityMetaDataSource,
      IOpenAccessMetadataProvider provider,
      Database database,
      string connString)
    {
      using (OpenAccessConnection.MetadataUpgradeFacade metadataUpgradeFacade = new OpenAccessConnection.MetadataUpgradeFacade(this, newMetadataContainer, moduleName, sitefinityMetaDataSource, provider, database, connString))
      {
        metadataUpgradeFacade.UpgradeSchema();
        if (metadataUpgradeFacade.Error != null)
          throw metadataUpgradeFacade.Error;
        metadataUpgradeFacade.UpgradeProvider();
        metadataUpgradeFacade.SaveChanges();
      }
    }

    private string ConnectionHash
    {
      get
      {
        if (this.connectionHash == null)
          this.connectionHash = OpenAccessConnection.ComputeConnectionHash(this.originalConnString, this.Replication);
        return this.connectionHash;
      }
    }

    private string GetLogKey() => string.Format("{0} ({1})", (object) this.Name, (object) this.GetHashCode());

    private static string ComputeConnectionHash(
      string connectionString,
      OpenAccessConnection.ReplicationMode replication)
    {
      using (SHA1CryptoServiceProvider cryptoServiceProvider = new SHA1CryptoServiceProvider())
      {
        byte[] bytes = Encoding.UTF8.GetBytes(replication == OpenAccessConnection.ReplicationMode.None ? connectionString : connectionString + ";replication=" + (object) replication);
        return Convert.ToBase64String(cryptoServiceProvider.ComputeHash(bytes));
      }
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
      if (this.currentDatabase == null)
        return;
      OpenAccessConnection.CloseDatabase(this.currentDatabase, this);
      this.currentDatabase = (Database) null;
    }

    /// <summary>
    /// This event provides a way to customize the backend configuration. For example one can subscribe in Global.asax and turn of/on second level cache or modify connection pooling
    /// </summary>
    public static event OpenAccessConnection.BackendConfigurationInitializer BackendConfigurationInit;

    private static void Connection_MetadataChanged(object sender, EventArgs args)
    {
      if (OpenAccessConnection.metadataChanged == null)
        return;
      OpenAccessConnection.metadataChanged(sender, args);
    }

    private static OpenAccessConnection.OADiagnosticMode DiagnosticMode { get; set; }

    public delegate void BackendConfigurationInitializer(
      OpenAccessConnection connection,
      BackendConfiguration configuration);

    internal class UpgradeParams
    {
      internal int PreviousVersionNumber { get; set; }

      internal int CurrentVersionNumber { get; set; }

      internal bool UpgradeSchema { get; set; }

      internal bool UpgradeProvider { get; set; }

      internal string AssembliesString { get; set; }
    }

    internal class MetadataUpgradeFacade : IDisposable
    {
      private bool resetModelAfterCommit;
      private MetadataManager metadataManager;
      private SchemaVersion schemaVersion;
      private bool upgradeSchema;
      private bool upgradeProvider;
      private int prevVersionNumber;
      private int currentVersionNumber;
      private MethodInfo getColumnNameForFieldName;
      private object openAccessNameGenerator;
      private readonly Database database;
      private readonly string connString;
      private readonly MetadataContainer newMetadataContainer;
      private readonly OpenAccessConnection connection;
      private readonly IOpenAccessMetadataProvider provider;
      private readonly ISitefinityMetadataSource sitefinityMetaDataSource;
      private readonly string moduleName;
      internal const string TransactionName = "OAC_Upgrading_0123";

      public MetadataUpgradeFacade(
        OpenAccessConnection connection,
        MetadataContainer metadataContainer,
        string moduleName,
        ISitefinityMetadataSource sitefinityMetaDataSource,
        IOpenAccessMetadataProvider provider)
        : this(connection, metadataContainer, moduleName, sitefinityMetaDataSource, provider, connection.currentDatabase, connection.currentConnectionString)
      {
      }

      public MetadataUpgradeFacade(
        OpenAccessConnection connection,
        MetadataContainer metadataContainer,
        string moduleName,
        ISitefinityMetadataSource sitefinityMetaDataSource,
        IOpenAccessMetadataProvider provider,
        Database database,
        string connString)
      {
        this.connection = connection;
        this.provider = provider;
        this.moduleName = moduleName;
        this.sitefinityMetaDataSource = sitefinityMetaDataSource;
        this.newMetadataContainer = metadataContainer;
        this.database = database;
        this.connString = connString;
      }

      public bool ResetModelAfterCommit => this.resetModelAfterCommit;

      public Exception Error { get; set; }

      public void UpgradeSchema(ISchemaHandler schemaHandler = null)
      {
        this.resetModelAfterCommit = false;
        int num = 3;
        while (num > 0)
        {
          --num;
          try
          {
            if (this.provider is IOpenAccessSelfUpgradableProvider provider1)
            {
              provider1.Update(this.connection, this.database, this.connString, this.newMetadataContainer, new UpgradeDatabaseScheme(OpenAccessConnection.UpgradeDatabase));
            }
            else
            {
              IOpenAccessUpgradableProvider provider = this.provider as IOpenAccessUpgradableProvider;
              this.metadataManager = MetadataManager.GetManager(OpenAccessConnection.defaultSchemaUpgradeProviderName, "OAC_Upgrading_0123");
              this.schemaVersion = this.metadataManager.GetSchemaVersions().Where<SchemaVersion>((Expression<Func<SchemaVersion, bool>>) (s => s.ModuleName == this.moduleName && s.ConnectionId == this.connection.connectionName)).SingleOrDefault<SchemaVersion>();
              OpenAccessConnection.UpgradeParams upgradeParams = OpenAccessConnection.MetadataUpgradeFacade.CheckForUpgrade(this.provider, this.connection, this.moduleName, this.sitefinityMetaDataSource, this.schemaVersion);
              if (upgradeParams == null)
                break;
              this.upgradeProvider = upgradeParams.UpgradeProvider;
              this.upgradeSchema = upgradeParams.UpgradeSchema;
              this.currentVersionNumber = upgradeParams.CurrentVersionNumber;
              this.prevVersionNumber = upgradeParams.PreviousVersionNumber;
              string assembliesString = upgradeParams.AssembliesString;
              if (this.upgradeSchema || this.upgradeProvider)
              {
                try
                {
                  DynamicTypeInfo[] dynamicTypes = this.sitefinityMetaDataSource.DynamicTypes;
                  if (this.upgradeProvider && provider != null && this.prevVersionNumber > 0)
                  {
                    using (UpgradingContext context = new UpgradingContext(this.connString, this.connection, this.newMetadataContainer))
                    {
                      provider.OnUpgrading(context, this.prevVersionNumber);
                      this.UpdateNumberAndDatetimeCustomFields(dynamicTypes);
                    }
                  }
                  if (schemaHandler != null)
                    OpenAccessConnection.UpgradeDatabaseSchema(schemaHandler, this.connection);
                  else
                    OpenAccessConnection.UpgradeDatabase(this.database, this.connection, out schemaHandler);
                  if (schemaHandler != null && provider != null && provider is IOpenAccessUpgradableProviderExtended && this.prevVersionNumber > 0)
                    ((IOpenAccessUpgradableProviderExtended) provider).OnSchemaUpgrade(this.connection, schemaHandler, this.prevVersionNumber);
                  if (this.schemaVersion == null)
                    this.schemaVersion = this.metadataManager.CreateSchemaVersion(this.moduleName, this.connection.connectionName);
                  this.schemaVersion.Cultures = string.Join<CultureInfo>(",", (IEnumerable<CultureInfo>) OpenAccessConnection.MetadataUpgradeFacade.GetCultures());
                  this.schemaVersion.Assembly = assembliesString;
                  this.schemaVersion.MetaDataChanged = false;
                  this.schemaVersion.ConnectionHash = this.connection.ConnectionHash;
                  this.schemaVersion.MetaTypes = dynamicTypes != null ? (IList<string>) ((IEnumerable<DynamicTypeInfo>) dynamicTypes).Select<DynamicTypeInfo, string>((Func<DynamicTypeInfo, string>) (t => t.Name)).ToArray<string>() : (IList<string>) (string[]) null;
                  this.schemaVersion.LastUpgradeDate = DateTime.UtcNow;
                  if (dynamicTypes != null)
                  {
                    foreach (DynamicTypeInfo dynamicTypeInfo in dynamicTypes)
                    {
                      if (this.metadataManager.GetItemOrDefault(typeof (Telerik.Sitefinity.Metadata.Model.MetaType), dynamicTypeInfo.MetaTypeId) is Telerik.Sitefinity.Metadata.Model.MetaType itemOrDefault)
                      {
                        MetaTypeAttribute metaTypeAttribute = itemOrDefault.MetaAttributes.FirstOrDefault<MetaTypeAttribute>((Func<MetaTypeAttribute, bool>) (a => a.Name == "moduleName"));
                        string moduleName = metaTypeAttribute == null ? "#NA" : metaTypeAttribute.Value;
                        if (dynamicTypeInfo.IsDeleted)
                        {
                          this.metadataManager.Delete(itemOrDefault);
                          MetadataMapping metadataMapping1 = this.metadataManager.GetMetadataMapping(moduleName, itemOrDefault.ClassName, "#NA");
                          if (metadataMapping1 != null)
                            this.metadataManager.Delete(metadataMapping1);
                          foreach (MetaField field in (IEnumerable<MetaField>) itemOrDefault.Fields)
                          {
                            MetadataMapping metadataMapping2 = this.metadataManager.GetMetadataMapping(moduleName, itemOrDefault.ClassName, field.FieldName);
                            if (metadataMapping2 != null)
                              this.metadataManager.Delete(metadataMapping2);
                          }
                          this.resetModelAfterCommit = true;
                        }
                        else if (itemOrDefault.Fields != null)
                        {
                          List<MetaField> list = itemOrDefault.Fields.Where<MetaField>((Func<MetaField, bool>) (f => f.IsDeleted)).ToList<MetaField>();
                          if (list.Count > 0)
                          {
                            this.resetModelAfterCommit = true;
                            Dictionary<string, string> dictionary = new Dictionary<string, string>();
                            foreach (MetaField metafield in list)
                            {
                              string fieldName = metafield.FieldName;
                              string fullTypeName = itemOrDefault.FullTypeName;
                              this.metadataManager.Delete(metafield);
                              MetadataMapping metadataMapping = this.metadataManager.GetMetadataMapping(moduleName, itemOrDefault.ClassName, metafield.FieldName);
                              if (metadataMapping != null)
                                this.metadataManager.Delete(metadataMapping);
                              if (metafield.ClrType == typeof (ContentLink[]).FullName && fullTypeName != null && fieldName != null)
                                dictionary.Add(fieldName, fullTypeName);
                            }
                            if (dictionary.Count > 0)
                            {
                              ContentLinksManager manager = ContentLinksManager.GetManager((string) null, "OAC_Upgrading_0123");
                              foreach (KeyValuePair<string, string> keyValuePair in dictionary)
                                RelatedDataHelper.DeleteFieldRelations((IContentLinksManager) manager, keyValuePair.Value, keyValuePair.Key);
                            }
                          }
                        }
                      }
                    }
                  }
                }
                catch (Exception ex)
                {
                  this.upgradeProvider = false;
                  TransactionManager.RollbackTransaction("OAC_Upgrading_0123");
                  throw;
                }
              }
            }
            break;
          }
          catch (Exception ex)
          {
            if (num == 0)
            {
              TransactionManager.RollbackTransaction("OAC_Upgrading_0123");
              this.Error = new Exception("Unable to upgrade database schema metadataSource provided by '{0}': {1}".Arrange((object) this.moduleName, (object) ex.Message), ex);
              break;
            }
            if (num > 2)
              Thread.Sleep(new Random().Next(0, 500));
          }
        }
      }

      public void UpgradeProvider()
      {
        if (!this.upgradeProvider || !(this.provider is IOpenAccessUpgradableProvider provider))
          return;
        if (this.prevVersionNumber > 0)
        {
          using (UpgradingContext context = new UpgradingContext(this.connString, this.connection, this.newMetadataContainer))
          {
            try
            {
              provider.OnUpgraded(context, this.prevVersionNumber);
            }
            catch (Exception ex)
            {
              Log.Write((object) string.Format("FAILED: Executing OnUpgraded method of '{0}': {1}", (object) this.moduleName, (object) ex.Message), ConfigurationPolicy.UpgradeTrace);
            }
          }
        }
        this.schemaVersion.PreviousVersionNumber = this.prevVersionNumber;
        this.schemaVersion.VersionNumber = this.currentVersionNumber;
      }

      public void SaveChanges() => TransactionManager.CommitTransaction("OAC_Upgrading_0123");

      private MethodInfo GetColumnNameForFieldName
      {
        get
        {
          if (this.getColumnNameForFieldName == (MethodInfo) null)
            this.getColumnNameForFieldName = this.OpenAccessNameGenerator.GetType().GetMethod("getColumnNameForFieldName", BindingFlags.Instance | BindingFlags.NonPublic, (Binder) null, new Type[2]
            {
              typeof (string),
              typeof (int)
            }, (ParameterModifier[]) null);
          return this.getColumnNameForFieldName;
        }
      }

      private object OpenAccessNameGenerator
      {
        get
        {
          if (this.openAccessNameGenerator == null)
          {
            object obj1 = this.database.GetType().GetProperty("Adapter").GetValue((object) this.database, (object[]) null);
            object obj2 = obj1.GetType().GetMethod("GetImpl", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(obj1, (object[]) null);
            object obj3 = obj2.GetType().GetField("smf", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(obj2);
            object obj4 = obj3.GetType().GetMethod("getInnerStorageManagerFactory", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Invoke(obj3, (object[]) null);
            object obj5 = obj4.GetType().GetMethod("getSqlDriver").Invoke(obj4, (object[]) null);
            this.openAccessNameGenerator = obj5.GetType().GetMethod("createJdbcNameGenerator").Invoke(obj5, (object[]) null);
          }
          return this.openAccessNameGenerator;
        }
      }

      private void UpdateNumberAndDatetimeCustomFields(DynamicTypeInfo[] dynamicTypes)
      {
        if (SitefinityVersion.Sitefinity11_2.Build > this.prevVersionNumber || this.prevVersionNumber >= SitefinityVersion.Sitefinity11_2_IB_6925.Build || dynamicTypes == null)
          return;
        Queue<string> values = new Queue<string>();
        foreach (DynamicTypeInfo dynamicType in dynamicTypes)
        {
          Telerik.Sitefinity.Metadata.Model.MetaType metaType = this.metadataManager.GetItemOrDefault(typeof (Telerik.Sitefinity.Metadata.Model.MetaType), dynamicType.MetaTypeId) as Telerik.Sitefinity.Metadata.Model.MetaType;
          if (metaType != null && metaType.Fields != null)
          {
            foreach (MetaField field in (IEnumerable<MetaField>) metaType.Fields)
            {
              if (!field.ClrType.IsNullOrEmpty())
              {
                Type objA = TypeResolutionService.ResolveType(field.ClrType, false);
                if (objA != (Type) null && !object.Equals((object) objA, (object) typeof (DateTime?)) && !object.Equals((object) objA, (object) typeof (Decimal?)))
                  continue;
              }
              if (string.IsNullOrEmpty(field.ColumnName))
              {
                MetaPersistentType metaPersistentType = this.newMetadataContainer.PersistentTypes.Where<MetaPersistentType>((Func<MetaPersistentType, bool>) (x => x.FullName == metaType.FullTypeName)).FirstOrDefault<MetaPersistentType>();
                if (metaPersistentType != null)
                {
                  List<CultureInfo> cultureInfoList = new List<CultureInfo>()
                  {
                    CultureInfo.InvariantCulture
                  };
                  if (field.IsLocalizable)
                    cultureInfoList.AddRange((IEnumerable<CultureInfo>) ((IEnumerable<CultureInfo>) OpenAccessConnection.MetadataUpgradeFacade.GetCultures()).ToList<CultureInfo>());
                  foreach (CultureInfo cultureInfo in cultureInfoList)
                  {
                    object obj = this.GetColumnNameForFieldName.Invoke(this.openAccessNameGenerator, new object[2]
                    {
                      (object) field.FieldName,
                      (object) 0
                    });
                    string fieldName = field.FieldName;
                    string str1 = "_" + LstringPropertyDescriptor.GetCultureSuffix(cultureInfo);
                    if (!object.Equals((object) cultureInfo, (object) CultureInfo.InvariantCulture))
                    {
                      fieldName += str1;
                      obj = (object) (obj.ToString() + str1);
                    }
                    string str2 = string.IsNullOrEmpty(metaPersistentType.Table.Name) ? metaPersistentType.Name : metaPersistentType.Table.Name;
                    if (this.connection.DbType == DatabaseType.MySQL)
                    {
                      string str3 = "DATETIME";
                      Type objA = TypeResolutionService.ResolveType(field.ClrType, false);
                      if (objA != (Type) null && object.Equals((object) objA, (object) typeof (Decimal?)))
                        str3 = string.Format((IFormatProvider) CultureInfo.InvariantCulture, "DECIMAL(20, {0})", string.IsNullOrEmpty(field.DBScale) ? (object) "0" : (object) field.DBScale);
                      string format = "DROP PROCEDURE IF EXISTS rename_column; CREATE DEFINER=CURRENT_USER PROCEDURE rename_column ( ) BEGIN DECLARE EXIT HANDLER FOR 1060 SELECT 'MySQL error code 1060 invoked'; DECLARE EXIT HANDLER FOR 1054 SELECT 'MySQL error code 1054 invoked'; {0}; END; CALL rename_column; DROP PROCEDURE rename_column;";
                      string str4 = string.Format((IFormatProvider) CultureInfo.InvariantCulture, "ALTER TABLE {0} CHANGE {1} {2} {3}", (object) str2, (object) fieldName, obj, (object) str3);
                      values.Enqueue(string.Format((IFormatProvider) CultureInfo.InvariantCulture, format, (object) str4));
                    }
                    else if (this.connection.DbType == DatabaseType.Oracle)
                    {
                      string format = "BEGIN DECLARE duplicate_column EXCEPTION; column_not_exists EXCEPTION; PRAGMA EXCEPTION_INIT (column_not_exists , -00904); PRAGMA EXCEPTION_INIT (duplicate_column , -00957); BEGIN EXECUTE IMMEDIATE '{0}'; EXCEPTION WHEN column_not_exists OR duplicate_column THEN NULL; END; END;";
                      string str5 = string.Format((IFormatProvider) CultureInfo.InvariantCulture, "ALTER TABLE \"{0}\" RENAME COLUMN \"{1}\" TO \"{2}\"", (object) str2, (object) fieldName, obj);
                      values.Enqueue(string.Format((IFormatProvider) CultureInfo.InvariantCulture, format, (object) str5));
                    }
                    else
                    {
                      string format = "IF COL_LENGTH('{0}','{1}') IS NOT NULL AND COL_LENGTH('{0}','{2}') IS NULL BEGIN {3} END";
                      string str6 = string.Format((IFormatProvider) CultureInfo.InvariantCulture, "EXEC sp_rename '{0}.{1}', '{2}', 'COLUMN'", (object) str2, (object) fieldName, obj);
                      values.Enqueue(string.Format((IFormatProvider) CultureInfo.InvariantCulture, format, (object) str2, (object) fieldName, obj, (object) str6));
                    }
                  }
                }
              }
            }
          }
        }
        while (values.Count > 0)
        {
          try
          {
            string ddl = values.Peek();
            this.database.GetSchemaHandler().ForceExecuteDDLScript(ddl);
            values.Dequeue();
          }
          catch (Exception ex)
          {
            Log.Write((object) string.Format("FAILED to execute upgrade scripts: '{0}'. Error: '{1}'. Try to execute the script manually. See the error log for more details.", (object) string.Join("; ", (IEnumerable<string>) values), (object) ex.Message), ConfigurationPolicy.UpgradeTrace);
            if (!Telerik.Sitefinity.Abstractions.Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
              break;
            throw;
          }
        }
      }

      private static string GetAssembliesString(ISitefinityMetadataSource metadataSource)
      {
        StringBuilder stringBuilder = new StringBuilder();
        foreach (Assembly assembly in metadataSource.Assemblies)
        {
          if (stringBuilder.Length > 0)
            stringBuilder.Append(";");
          AssemblyName name = assembly.GetName();
          stringBuilder.Append(name.Name);
          stringBuilder.Append(",");
          stringBuilder.Append(name.Version.ToString());
        }
        return stringBuilder.ToString();
      }

      private static CultureInfo[] GetCultures() => MetadataSourceAggregator.GetConfiguredCultures();

      private static IDictionary<string, Version> GetAssembliesVersionsFromString(
        string assembliesString)
      {
        string[] strArray1 = assembliesString.Split(';');
        Dictionary<string, Version> versionsFromString = new Dictionary<string, Version>();
        foreach (string str in strArray1)
        {
          char[] chArray = new char[1]{ ',' };
          string[] strArray2 = str.Split(chArray);
          if (strArray2.Length == 2)
          {
            string key = strArray2[0];
            Version result;
            if (Version.TryParse(strArray2[1], out result))
              versionsFromString.Add(key, result);
          }
        }
        return (IDictionary<string, Version>) versionsFromString;
      }

      private static bool ValidateUpgrade(Version newV, Version oldV) => oldV.Major <= newV.Major && (oldV.Major < newV.Major || oldV.Minor <= newV.Minor && (oldV.Minor < newV.Minor || oldV.Build <= newV.Build));

      internal static OpenAccessConnection.UpgradeParams CheckForUpgrade(
        IOpenAccessMetadataProvider provider,
        OpenAccessConnection connection,
        string moduleName,
        ISitefinityMetadataSource sitefinityMetaDataSource,
        SchemaVersion schemaVersion)
      {
        string assembliesString = string.Empty;
        int num1 = 0;
        int num2 = 0;
        bool flag1 = false;
        bool flag2 = false;
        IDatabaseMappingContext fluentMappingContext = connection.GetFluentMappingContext();
        if (sitefinityMetaDataSource == null || sitefinityMetaDataSource is OpenAccessConnection.CombinedMetadataSource)
          sitefinityMetaDataSource = OpenAccessConnection.GetSitefinityMetaDataSource(provider.GetMetaDataSource(fluentMappingContext), fluentMappingContext);
        if (sitefinityMetaDataSource.Assemblies != null && sitefinityMetaDataSource.Assemblies.Length != 0)
          assembliesString = OpenAccessConnection.MetadataUpgradeFacade.GetAssembliesString(sitefinityMetaDataSource);
        IOpenAccessUpgradableProvider upgradableProvider = provider as IOpenAccessUpgradableProvider;
        if (schemaVersion == null)
        {
          flag1 = true;
          if (upgradableProvider != null)
          {
            if (OpenAccessConnection.previousSitefinityVersionNumber < 1500 && sitefinityMetaDataSource.GetType().Assembly.GetName().Name.StartsWith("Telerik.Sitefinity") || moduleName.Equals("Telerik.Sitefinity.Modules.Libraries.BlobStorage.OpenAccessBlobStorageProvider"))
              num1 = OpenAccessConnection.previousSitefinityVersionNumber;
            num2 = upgradableProvider.CurrentSchemaVersionNumber;
            flag2 = true;
          }
        }
        else
        {
          num1 = schemaVersion.VersionNumber;
          if (upgradableProvider != null)
          {
            num2 = upgradableProvider.CurrentSchemaVersionNumber;
            if (num1 == 0)
              num1 = 1;
            else if (num1 > num2)
            {
              if (!connection.IgnoreDowngradeExceptions)
                throw new System.InvalidOperationException("The provider '{0}' database schema version ({1}) is higher than the currently running version ({2}). Downgrade is not allowed.".Arrange((object) moduleName, (object) num1.ToString(), (object) num2.ToString()));
              Log.Write((object) "IGNORED DOWNGRADE EXCEPTION: The provider '{0}' database schema version ({1}) is higher than the currently running version ({2}).".Arrange((object) moduleName, (object) num1.ToString(), (object) num2.ToString()), ConfigurationPolicy.UpgradeTrace);
            }
            if (num2 > num1)
              flag2 = true;
          }
          if (!flag2)
          {
            if (schemaVersion.Assembly != null && !schemaVersion.Assembly.Equals(assembliesString))
            {
              IDictionary<string, Version> versionsFromString1 = OpenAccessConnection.MetadataUpgradeFacade.GetAssembliesVersionsFromString(schemaVersion.Assembly);
              IDictionary<string, Version> versionsFromString2 = OpenAccessConnection.MetadataUpgradeFacade.GetAssembliesVersionsFromString(assembliesString);
              foreach (KeyValuePair<string, Version> keyValuePair in (IEnumerable<KeyValuePair<string, Version>>) versionsFromString1)
              {
                Version newV;
                if (versionsFromString2.TryGetValue(keyValuePair.Key, out newV) && !OpenAccessConnection.MetadataUpgradeFacade.ValidateUpgrade(newV, keyValuePair.Value))
                {
                  if (!connection.IgnoreDowngradeExceptions)
                    throw new System.InvalidOperationException("The mapping of the provider '{0}' contains assembly '{1}' with version {2}, which is older than the currently installed in the database ({3}) for this provider. Downgrade is not allowed.".Arrange((object) moduleName, (object) newV.ToString(), (object) keyValuePair.Value.ToString(), (object) keyValuePair.Value.ToString()));
                  Log.Write((object) "IGNORED DOWNGRADE EXCEPTION: The mapping of the provider '{0}' contains assembly '{1}' with version {2}, which is older than the currently installed in the database ({3}) for this provider.".Arrange((object) moduleName, (object) newV.ToString(), (object) keyValuePair.Value.ToString(), (object) keyValuePair.Value.ToString()), ConfigurationPolicy.UpgradeTrace);
                  return (OpenAccessConnection.UpgradeParams) null;
                }
              }
              flag1 = true;
            }
            else if (!string.IsNullOrEmpty(schemaVersion.ConnectionHash) && !schemaVersion.ConnectionHash.Equals(connection.ConnectionHash))
              flag1 = true;
            else if (schemaVersion.MetaDataChanged)
            {
              flag1 = true;
            }
            else
            {
              CultureInfo[] cultures = OpenAccessConnection.MetadataUpgradeFacade.GetCultures();
              if (schemaVersion.Cultures != null)
              {
                string[] source = schemaVersion.Cultures.Split(new char[2]
                {
                  ',',
                  ' '
                }, StringSplitOptions.RemoveEmptyEntries);
                foreach (CultureInfo cultureInfo in cultures)
                {
                  if (!((IEnumerable<string>) source).Contains<string>(cultureInfo.Name, (IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase))
                  {
                    flag1 = true;
                    break;
                  }
                }
              }
              else if (cultures.Length != 0)
                flag1 = true;
            }
          }
        }
        return new OpenAccessConnection.UpgradeParams()
        {
          UpgradeProvider = flag2,
          UpgradeSchema = flag1,
          CurrentVersionNumber = num2,
          PreviousVersionNumber = num1,
          AssembliesString = assembliesString
        };
      }

      /// <summary>
      /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
      /// </summary>
      public void Dispose() => TransactionManager.DisposeTransaction("OAC_Upgrading_0123");
    }

    private interface IMetadataUpdateStrategy
    {
      bool ReplaceMetadata(
        OpenAccessConnection connection,
        MetadataContainer newMetadataContainer,
        string moduleName,
        ISitefinityMetadataSource sitefinityMetaDataSource,
        IOpenAccessMetadataProvider provider);
    }

    private class IsolatedMetadataUpdateStrategy : OpenAccessConnection.IMetadataUpdateStrategy
    {
      public virtual bool ReplaceMetadata(
        OpenAccessConnection connection,
        MetadataContainer newMetadataContainer,
        string moduleName,
        ISitefinityMetadataSource sitefinityMetaDataSource,
        IOpenAccessMetadataProvider provider)
      {
        string connString;
        Database newDatabase = OpenAccessConnection.GetNewDatabase(connection, newMetadataContainer, out connString);
        try
        {
          connection.HandleDatabaseUpgrade(newMetadataContainer, moduleName, sitefinityMetaDataSource, provider, newDatabase, connString);
        }
        catch
        {
          OpenAccessConnection.CloseDatabase(newDatabase, connection);
          throw;
        }
        connection.currentConnectionString = connString;
        if (connection.currentDatabase != null)
          OpenAccessConnection.CloseDatabase(connection.currentDatabase, connection);
        connection.currentDatabase = newDatabase;
        return true;
      }
    }

    private class OptimizedMetadataUpdateStrategy : OpenAccessConnection.IMetadataUpdateStrategy
    {
      public virtual bool ReplaceMetadata(
        OpenAccessConnection connection,
        MetadataContainer newMetadataContainer,
        string moduleName,
        ISitefinityMetadataSource sitefinityMetaDataSource,
        IOpenAccessMetadataProvider provider)
      {
        if (connection.currentDatabase == null)
        {
          string connString;
          Database newDatabase = OpenAccessConnection.GetNewDatabase(connection, newMetadataContainer, out connString);
          if (OpenAccessConnection.CanUpgradeDbSchema(connection))
            connection.HandleDatabaseUpgrade(newMetadataContainer, moduleName, sitefinityMetaDataSource, provider, newDatabase, connString);
          connection.currentConnectionString = connString;
          connection.currentDatabase = newDatabase;
        }
        else
        {
          using (OpenAccessConnection.MetadataUpgradeFacade upgradeFacade = new OpenAccessConnection.MetadataUpgradeFacade(connection, newMetadataContainer, moduleName, sitefinityMetaDataSource, provider))
          {
            OpenAccessConnection.OptimizedMetadataUpdateStrategy.ReplaceMetadataCallbackWrapper metadataCallbackWrapper = new OpenAccessConnection.OptimizedMetadataUpdateStrategy.ReplaceMetadataCallbackWrapper(upgradeFacade);
            using (IObjectScope objectScope = connection.currentDatabase.GetObjectScope())
            {
              SchemaUpdateCallback callback = !OpenAccessConnection.CanUpgradeDbSchema(connection) || sitefinityMetaDataSource is OpenAccessConnection.CombinedMetadataSource ? (SchemaUpdateCallback) null : new SchemaUpdateCallback(metadataCallbackWrapper.ReplaceMetadataCallback);
              using (new MethodPerformanceRegion(nameof (ReplaceMetadata)))
                Database.ReplaceMetadata(objectScope, newMetadataContainer, callback);
            }
            if (upgradeFacade.Error != null)
              throw upgradeFacade.Error;
            using (new MethodPerformanceRegion("Upgrade provider"))
            {
              upgradeFacade.UpgradeProvider();
              upgradeFacade.SaveChanges();
            }
          }
        }
        return true;
      }

      private class ReplaceMetadataCallbackWrapper
      {
        private readonly OpenAccessConnection.MetadataUpgradeFacade upgradeFacade;

        public ReplaceMetadataCallbackWrapper(
          OpenAccessConnection.MetadataUpgradeFacade upgradeFacade)
        {
          this.upgradeFacade = upgradeFacade;
        }

        public void ReplaceMetadataCallback(object sender, SchemaUpdateArgs args)
        {
          this.upgradeFacade.UpgradeSchema(args.SchemaHandler);
          if (this.upgradeFacade.Error == null)
            return;
          args.Cancel = true;
        }
      }
    }

    internal class CombinedMetadataSource : SitefinityMetadataSourceBase
    {
      private readonly IList<MappingConfiguration> mapConfigs;

      internal CombinedMetadataSource(
        IDatabaseMappingContext context,
        IList<MappingConfiguration> mapConfigs)
        : base(context)
      {
        this.mapConfigs = (IList<MappingConfiguration>) new List<MappingConfiguration>((IEnumerable<MappingConfiguration>) mapConfigs);
      }

      public IDictionary<string, string[]> Modules { get; set; }

      protected override IList<IOpenAccessFluentMapping> BuildCustomMappings() => (IList<IOpenAccessFluentMapping>) new List<IOpenAccessFluentMapping>()
      {
        (IOpenAccessFluentMapping) new OpenAccessConnection.CombinedMetadataSource.CombinedFluentMapping(this.Context, this.mapConfigs)
      };

      internal class CombinedFluentMapping : OpenAccessFluentMappingBase
      {
        private readonly IList<MappingConfiguration> mapConfigs;

        public CombinedFluentMapping(
          IDatabaseMappingContext context,
          IList<MappingConfiguration> mapConfigs)
          : base(context)
        {
          this.mapConfigs = (IList<MappingConfiguration>) new List<MappingConfiguration>(mapConfigs.Distinct<MappingConfiguration>((IEqualityComparer<MappingConfiguration>) new OpenAccessConnection.CombinedMetadataSource.CombinedFluentMapping.MapConfigEqualityComparer()));
        }

        public override IList<MappingConfiguration> GetMapping() => this.mapConfigs;

        internal class MapConfigEqualityComparer : IEqualityComparer<MappingConfiguration>
        {
          public bool Equals(MappingConfiguration x, MappingConfiguration y) => object.Equals((object) x.ConfiguredType, (object) y.ConfiguredType);

          public int GetHashCode(MappingConfiguration obj) => obj.ConfiguredType.GetHashCode();
        }
      }
    }

    private class MetadataExtendMonitor
    {
      private readonly StringBuilder strBuilder;
      private readonly OpenAccessConnection connection;
      private readonly MetadataContainer newContainer;
      private readonly MetadataContainer oldContainer;
      private readonly IEnumerable<string> newModules;

      public MetadataExtendMonitor(
        string operation,
        OpenAccessConnection connection,
        MetadataContainer newContainer,
        IEnumerable<string> newModules)
      {
        this.connection = connection;
        this.newContainer = newContainer;
        this.oldContainer = connection.currentMetadataContainer;
        this.newModules = newModules;
        this.strBuilder = new StringBuilder();
        this.Init(operation);
      }

      public void WriteAggregatedContainer(MetadataContainer container) => this.WriteContainerTypes("Types in the container after the aggregation:", container);

      public void WriteCurrentDatabaseTypes() => this.WriteList("Types in the Database object after replacing the container:", this.connection.currentDatabase != null ? this.connection.currentDatabase.MetaData.PersistentTypes.Select<MetaPersistentType, string>((Func<MetaPersistentType, string>) (t => t.FullName)) : (IEnumerable<string>) new List<string>());

      public void Flush()
      {
        this.Validate();
        Log.Write((object) this.strBuilder.ToString(), ConfigurationPolicy.Trace);
      }

      private void Validate()
      {
        if (this.connection.currentMetadataContainer == null)
          return;
        HashSet<string> stringSet1 = new HashSet<string>(this.connection.currentMetadataContainer.PersistentTypes.Where<MetaPersistentType>((Func<MetaPersistentType, bool>) (t => !t.IsArtificial)).Select<MetaPersistentType, string>((Func<MetaPersistentType, string>) (t => t.FullName)));
        foreach (string str1 in this.newContainer.PersistentTypes.Where<MetaPersistentType>((Func<MetaPersistentType, bool>) (t => !t.IsArtificial)).Select<MetaPersistentType, string>((Func<MetaPersistentType, string>) (t => t.FullName)).ToArray<string>())
        {
          if (!stringSet1.Contains(str1))
          {
            string str2 = "Failed to extend the connection ('{0}') metadata with type '{1}' after aggregating the metadata container.".Arrange((object) this.connection.Name, (object) str1);
            this.strBuilder.AppendLine(str2);
            Exception exceptionToHandle = new Exception(str2 + " See the trace log.");
            if (Telerik.Sitefinity.Abstractions.Exceptions.HandleException(exceptionToHandle, ExceptionPolicyName.IgnoreExceptions))
              throw exceptionToHandle;
          }
        }
        if (this.oldContainer != null)
        {
          foreach (string str3 in this.oldContainer.PersistentTypes.Where<MetaPersistentType>((Func<MetaPersistentType, bool>) (t => !t.IsArtificial)).Select<MetaPersistentType, string>((Func<MetaPersistentType, string>) (t => t.FullName)).ToArray<string>())
          {
            if (!stringSet1.Contains(str3))
            {
              string str4 = "Missing type '{1}' after aggregating the metadata container of the connection '{0}'.".Arrange((object) this.connection.Name, (object) str3);
              this.strBuilder.AppendLine(str4);
              Exception exceptionToHandle = new Exception(str4 + " See the trace log.");
              if (Telerik.Sitefinity.Abstractions.Exceptions.HandleException(exceptionToHandle, ExceptionPolicyName.IgnoreExceptions))
                throw exceptionToHandle;
            }
          }
        }
        if (this.connection.currentDatabase == null)
          return;
        HashSet<string> stringSet2 = new HashSet<string>(this.connection.currentDatabase.MetaData.PersistentTypes.Where<MetaPersistentType>((Func<MetaPersistentType, bool>) (t => !t.IsArtificial)).Select<MetaPersistentType, string>((Func<MetaPersistentType, string>) (t => t.FullName)));
        foreach (string str5 in stringSet1)
        {
          if (!stringSet2.Contains(str5))
          {
            string str6 = "Failed to extend the connection ('{0}') metadata with type '{1}' after replacing the database container.".Arrange((object) this.connection.Name, (object) str5);
            this.strBuilder.AppendLine(str6);
            Exception exceptionToHandle = new Exception(str6 + " See the trace log.");
            if (Telerik.Sitefinity.Abstractions.Exceptions.HandleException(exceptionToHandle, ExceptionPolicyName.IgnoreExceptions))
              throw exceptionToHandle;
          }
        }
        this.strBuilder.AppendLine();
      }

      private void Init(string operation)
      {
        this.strBuilder.Clear();
        this.strBuilder.AppendFormat("{0}: connection '{1}'.", (object) operation.ToUpper(), (object) this.connection.GetLogKey());
        this.strBuilder.AppendLine();
        this.WriteModules("Currently registered modules in the connection:", (IEnumerable<string>) this.connection.registeredModules);
        this.WriteContainerTypes("Currently registered types in the container:", this.oldContainer);
        this.WriteModules("New modules:", this.newModules);
        this.WriteContainerTypes("New types:", this.newContainer);
      }

      private void WriteModules(string title, IEnumerable<string> modules) => this.WriteList(title, modules);

      private void WriteContainerTypes(string title, MetadataContainer container) => this.WriteList(title, container != null ? (IEnumerable<string>) container.PersistentTypes.Select<MetaPersistentType, string>((Func<MetaPersistentType, string>) (t => t.FullName)).ToArray<string>() : (IEnumerable<string>) new string[0]);

      private void WriteList(string title, IEnumerable<string> list)
      {
        this.strBuilder.AppendLine(title);
        if (list.Any<string>())
        {
          foreach (object obj in list)
            this.strBuilder.AppendFormat("{0}, ", obj);
        }
        else
          this.strBuilder.AppendLine("Empty");
        this.strBuilder.AppendLine();
      }
    }

    private enum OADiagnosticMode
    {
      Quiet = 0,
      Minimal = 10, // 0x0000000A
      Normal = 20, // 0x00000014
      Detailed = 30, // 0x0000001E
    }

    internal enum ReplicationMode
    {
      None,
      Master,
      Slave,
    }

    private class OAConnectionState
    {
      public OAConnectionState(MetadataContainer container, IEnumerable<string> registerdModules)
      {
        this.MetadataContainer = container;
        this.RegisteredModules = (IEnumerable<string>) new List<string>(registerdModules);
      }

      public IEnumerable<string> RegisteredModules { get; private set; }

      public MetadataContainer MetadataContainer { get; private set; }
    }
  }
}
