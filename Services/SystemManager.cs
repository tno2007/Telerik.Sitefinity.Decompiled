// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.SystemManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using ServiceStack;
using ServiceStack.Text;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Principal;
using System.ServiceModel.Activation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Routing;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Logging;
using Telerik.Microsoft.Practices.Unity;
using Telerik.OpenAccess;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Abstractions.VirtualPath;
using Telerik.Sitefinity.BackgroundTasks;
using Telerik.Sitefinity.Cloud.WindowsAzure;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Configuration.Data;
using Telerik.Sitefinity.Configuration.Web.UI.Basic;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Configuration;
using Telerik.Sitefinity.Data.ContentLinks;
using Telerik.Sitefinity.Data.DataSource;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Data.Statistic;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.DynamicModules.Web.UI.Frontend;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.GeoLocations;
using Telerik.Sitefinity.HealthMonitoring;
using Telerik.Sitefinity.Licensing;
using Telerik.Sitefinity.Lifecycle.Cleanup;
using Telerik.Sitefinity.LoadBalancing;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Comments.Configuration;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Mvc;
using Telerik.Sitefinity.Packaging;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Personalization;
using Telerik.Sitefinity.Project.Configuration;
using Telerik.Sitefinity.ProtectionShield.Configuration;
using Telerik.Sitefinity.Scheduling;
using Telerik.Sitefinity.Scheduling.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Events;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Security.Web.UI;
using Telerik.Sitefinity.Services.Comments;
using Telerik.Sitefinity.Services.Configuration;
using Telerik.Sitefinity.Services.Events;
using Telerik.Sitefinity.Services.Notifications;
using Telerik.Sitefinity.Services.Operation;
using Telerik.Sitefinity.Services.RecycleBin;
using Telerik.Sitefinity.Services.Statistics;
using Telerik.Sitefinity.SiteSettings;
using Telerik.Sitefinity.SyncLock;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Web.Services;
using Telerik.Sitefinity.Upgrades.To5100;
using Telerik.Sitefinity.UsageTracking.Scheduling;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Versioning;
using Telerik.Sitefinity.Versioning.Cleaner;
using Telerik.Sitefinity.Versioning.Upgrade;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.OutputCache;
using Telerik.Sitefinity.Web.OutputCache.Data;
using Telerik.Sitefinity.Web.Services;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.NavigationControls;
using Telerik.Sitefinity.Workflow;
using Telerik.Sitefinity.Workflow.Activities.MessageTemplates;

namespace Telerik.Sitefinity.Services
{
  /// <summary>
  /// Represents a helper class for working with Sitefinity system.
  /// </summary>
  public static class SystemManager
  {
    internal static ISet<Type> cachedManagerTypes = (ISet<Type>) new HashSet<Type>();
    private static LockRegion<PackagingManager> region = (LockRegion<PackagingManager>) null;
    /// <summary>
    /// Gets the key for HttpContext.Items dictionary that contains a value indicating whether the current page is in design mode.
    /// </summary>
    public const string PageDesignModeKey = "SfPageDesignMode";
    /// <summary>
    /// Gets the key for HttpContext.Items dictionary that contains a value indicating whether the current page is in design mode.
    /// </summary>
    public const string PagePreviewModeKey = "SfPagePreviewMode";
    /// <summary>Gets the name of the backend root directory.</summary>
    public const string BackendRootDirectory = "Sitefinity";
    /// <summary>Gets the name of the ServiceStack app host.</summary>
    public const string RestApiPath = "RestApi";
    /// <summary>
    /// The name of the key that specifies whether the request is run in backend mode
    /// </summary>
    public const string IsBackendRequestKey = "IsBackendRequest";
    /// <summary>
    /// The name of the key that specifies whether the request runs from edit mode in frontend page.
    /// </summary>
    public const string IsFrontendPageEdit = "IsFrontendPageEdit";
    /// <summary>
    /// The name of the key that specifies the current culture of a backend request
    /// </summary>
    internal const string CurrentBackendCulture = "sfCurrentBackendCulture";
    /// <summary>
    /// Name of the key that keeps the language fallback mode of the current request
    /// </summary>
    public const string FallbackModeKey = "RequestFallback";
    /// <summary>
    /// Name of the key that keeps the language fallback mode of the current request
    /// </summary>
    public const string RenderInMemoryKey = "RenderInMemory";
    /// <summary>
    /// Name of the key that keeps information whether the current request is in Browse And Edit mode
    /// </summary>
    public const string IsBrowseAndEditModeKey = "IsBrowseAndEditMode";
    /// <summary>
    /// The name of the key that specifies whether the request requires authentication
    /// </summary>
    internal const string SuppressAuthenticationRequirement = "SuppressAuthenticationRequirement";
    /// <summary>
    /// Name of the key that keeps information whether the current request is in Inline Editing mode
    /// </summary>
    internal const string IsInlineEditingModeKey = "IsInlineEditingMode";
    internal const string DataTrackingDisabledKey = "DataTrackingDisabled";
    internal const string IsUpgradingdKey = "SF_IsUpgradingdKey";
    /// <summary>
    /// Name of the key that keeps information whether the current request is in Browse And Edit mode
    /// </summary>
    public const string TransactionsContextKey = "SfTransactions";
    /// <summary>Gets the name of the ServiceStack protected route.</summary>
    internal const string SitefinityProtectedRestApiPath = "RestApi/Sitefinity";
    private const string sitefinityBackgroundThreadPoolName = "SitefinityThreadPool";
    internal const string FrontEndControlRender = "sfFrontEndControlRender";
    internal const string DelayedDatabaseInitKey = "sfDelayedDatabaseInit";
    internal const string SitefinityFeatherPath = "Telerik.Sitefinity.Frontend";
    internal const string SitefinityRestPath = "sitefinityRest";
    internal const string SitefinityResourcesPath = "Res";
    internal const string SitefinityHtml5UploadHandlerPath = "Telerik.Sitefinity.Html5UploadHandler.ashx";
    internal const string TemplateThumbnailsPath = "template-thumbnails";
    internal const string SitefinitySignOutPath = "Sitefinity/SignOut";
    internal const string GlobalTransactionCommitSuffix = "#Commit#";
    internal const string OdataGlobalTransaction = "sfOdata#Commit#";
    /// <summary>Gets the name of the upgrade trace log source.</summary>
    internal const string UpgradeTraceKey = "UpgradeTrace";
    internal const string ErrorTraceKey = "ErrorLog";
    internal const string PackagingTraceKey = "PackagingTrace";
    internal const string SitefinityModuleName = "Sitefinity";
    internal const string ConfigMigrationModuleName = "f2984670-c099-4157-9fad-f6915db28ad6";
    internal const string DetailItem = "detailItem";
    private const string ApplicationModulesKey = "applicationModules";
    /// <summary>Module Not Licensed Error Message Text</summary>
    private const string ModuleNotLicensedErrorMessage = "Module is not licensed.";
    [ThreadStatic]
    private static HttpContextBase httpContextMock;
    [ThreadStatic]
    private static bool delayedDataBaseInit;
    private static string appDataFolderPhysicalPath;
    private static bool restarted;
    private static bool pendingRestart;
    private static readonly object syncLock = new object();
    private static readonly object cacheSyncLock = new object();
    private static DateTime startedOn;
    private static Dictionary<string, IService> services;
    private static Dictionary<string, IModule> appModules;
    private static Dictionary<string, Func<ScheduledTask>> crontabTasksToRun;
    private static Dictionary<string, IModule> dynamicModules;
    private static readonly Dictionary<string, WeakReference> cacheManagers = new Dictionary<string, WeakReference>();
    private static readonly ConcurrentDictionary<string, ServiceRoute> serviceRoutes = new ConcurrentDictionary<string, ServiceRoute>();
    private static readonly List<BasicSettingsRegistration> basicSettings = new List<BasicSettingsRegistration>();
    internal static readonly List<RouteRegistration> PendingRouteRegistrations = new List<RouteRegistration>();
    internal static readonly List<IPlugin> PendingServiceStackPlugins = new List<IPlugin>();
    private static readonly List<Type> registeredUserProfileTypes = new List<Type>();
    private static List<IActionMessageTemplate> actionMessageTemplates;
    private static readonly object actionMessageTemplatesSyncLock = new object();
    private static readonly object serviceRegSyncLock = new object();
    private static bool mvcEnabled = false;
    private static bool isAdminAppEnabled = false;
    private static bool isBridgeEnabledForCurrentUser = false;
    private static SitefinityContextBase context;
    private static IDataSourceRegistry dataSourceRegistry;
    private static readonly List<Type> multisiteEnabledManagers = new List<Type>();
    private static SitefinityTypeRegistry typeRegistry;
    private static List<System.Action> installDefaultsDelegates = new List<System.Action>();
    private static volatile bool isUpgrading = false;
    private static Version upgradeFromVersion = (Version) null;
    private static BlockingCollection<AppStatusEntry> appStatusBuffer;
    private static bool suppressRestriction = false;
    private static bool allSitesProviders = false;
    private static bool isDBPMode = false;
    private static int? serviceTokenLifetime = new int?();
    private static Dictionary<string, string> notLoadedModulesErrors = new Dictionary<string, string>();
    private static string[] siteDomains = (string[]) null;

    internal static bool HasDefaultDatabaseConnection => Telerik.Sitefinity.Configuration.Config.Get<DataConfig>(true).IsInitalized();

    /// <summary>
    /// The system-wide instances of <see cref="T:Telerik.Sitefinity.Services.SitefinityTypeRegistry" />.
    /// </summary>
    public static SitefinityTypeRegistry TypeRegistry
    {
      get
      {
        if (SystemManager.typeRegistry == null)
          SystemManager.typeRegistry = new SitefinityTypeRegistry();
        return SystemManager.typeRegistry;
      }
      internal set => SystemManager.typeRegistry = value;
    }

    internal static IStatusProviderRegistry StatusProviderRegistry => ObjectFactory.Resolve<IStatusProviderRegistry>();

    /// <summary>Gets or sets the root URL.</summary>
    /// <value>The root URL.</value>
    public static string RootUrl
    {
      get
      {
        if (SystemManager.CurrentHttpContext != null && SystemManager.CurrentHttpContext.Request != null)
          return SystemManager.CurrentHttpContext.Request.Url.GetLeftPart(UriPartial.Authority) + SystemManager.CurrentHttpContext.Request.ApplicationPath;
        return !SystemManager.AbsolutePathRootUrlOfFirstRequest.IsNullOrWhitespace() ? SystemManager.AbsolutePathRootUrlOfFirstRequest : HostingEnvironment.ApplicationVirtualPath;
      }
    }

    /// <summary>
    /// Gets or sets the absolute path root URL of first request statically.
    /// This is stored to retrieve the RootUrl or consequent requests in order to get the root url,
    /// in order to initiate requests when we have no context.
    /// </summary>
    /// <value>The absolute path root URL of first request.</value>
    public static string AbsolutePathRootUrlOfFirstRequest { get; set; }

    internal static Uri LocalUri { get; set; }

    internal static bool TryResolveWarmupUrl(out Uri uri)
    {
      string name = "sf:warmupUrl";
      string appSetting = ConfigurationManager.AppSettings[name];
      if (!string.IsNullOrEmpty(appSetting))
      {
        try
        {
          uri = new Uri(appSetting);
          return true;
        }
        catch (Exception ex)
        {
          if (Exceptions.HandleException((Exception) new ApplicationException("Unable to resolve the URL from the application setting with key: '{0}'".Arrange((object) name), ex), ExceptionPolicyName.IgnoreExceptions))
            throw ex;
        }
      }
      uri = (Uri) null;
      return false;
    }

    /// <summary>Gets or sets the app data folder physical path.</summary>
    /// <value>The app data folder physical path.</value>
    public static string AppDataFolderPhysicalPath
    {
      get => SystemManager.appDataFolderPhysicalPath;
      set => SystemManager.appDataFolderPhysicalPath = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether this <see cref="T:Telerik.Sitefinity.Services.SystemManager" /> is initializing.
    /// </summary>
    /// <value><c>true</c> if initializing; otherwise, <c>false</c>.</value>
    public static bool Initializing { get; private set; }

    /// <summary>
    /// Gets or sets a value indicating whether Sitefinity is currently installing a module
    /// </summary>
    /// <remarks>
    /// This is used to suppress actions that Sitefinity would normally take if it weren't installing a module,
    /// (e.g. not quitting the response when establishing connection with OpenAccess)
    /// or circumventing known ASP.NET bugs (e.g. HttpApplication.CompleteRequest() throwing NullReferenceException,
    /// because its private field _stepManager is null).
    /// </remarks>
    [Obsolete("Use InstallingModule != null")]
    public static bool IsInstallingModule => SystemManager.InstallingModule != null;

    /// <summary>
    /// Gets the date and time when the system was last stated or restarted.
    /// </summary>
    public static DateTime StartedOn => SystemManager.startedOn;

    /// <summary>Gets a collection of system services.</summary>
    /// <value>The services.</value>
    public static IList<IService> SystemServices => SystemManager.services == null ? (IList<IService>) new List<IService>() : (IList<IService>) new ReadOnlyCollection<IService>((IList<IService>) SystemManager.services.Values.ToList<IService>());

    /// <summary>Gets a collection of service modules.</summary>
    /// <value>The modules.</value>
    [Obsolete("Service module is not supported eny more. Please use SystemServices or ApplicationModules")]
    public static IDictionary<string, IModule> ServiceModules => (IDictionary<string, IModule>) new Dictionary<string, IModule>();

    /// <summary>Gets a collection of application modules.</summary>
    /// <value>The modules.</value>
    public static IDictionary<string, IModule> ApplicationModules
    {
      get
      {
        if (SystemManager.appModules == null)
        {
          Dictionary<string, IModule> dictionary = new Dictionary<string, IModule>();
        }
        return (IDictionary<string, IModule>) SystemManager.appModules;
      }
    }

    /// <summary>
    /// Gets crontab tasks that are registered during the site initialization
    /// </summary>
    public static IDictionary<string, Func<ScheduledTask>> CrontabTasksToRun
    {
      get
      {
        if (SystemManager.crontabTasksToRun == null)
          SystemManager.crontabTasksToRun = new Dictionary<string, Func<ScheduledTask>>();
        return (IDictionary<string, Func<ScheduledTask>>) SystemManager.crontabTasksToRun;
      }
    }

    internal static BlockingCollection<AppStatusEntry> AppStatusBuffer => SystemManager.appStatusBuffer;

    internal static bool EnablePostgreSQLOnStartup
    {
      get
      {
        bool result = false;
        bool.TryParse(ConfigurationSettings.AppSettings["sf:EnablePostgreSQLOnStartup"], out result);
        return result;
      }
    }

    internal static void InitAppStatusBuffer()
    {
      if (SystemManager.appStatusBuffer != null)
        return;
      SystemManager.appStatusBuffer = new BlockingCollection<AppStatusEntry>();
    }

    internal static void CleanUpAppStatusBuffer()
    {
      if (SystemManager.appStatusBuffer == null)
        return;
      SystemManager.appStatusBuffer.Dispose();
      SystemManager.appStatusBuffer = (BlockingCollection<AppStatusEntry>) null;
    }

    /// <summary>Gets the requested system service.</summary>
    /// <value>The services.</value>
    public static IService GetSystemService(string name)
    {
      if (string.IsNullOrEmpty(name))
        throw new ArgumentNullException(nameof (name));
      if (SystemManager.services == null)
        return (IService) null;
      IService service;
      if (SystemManager.services.TryGetValue(name, out service) && service.Status == ServiceStatus.Stopped && service.Startup == StartupType.OnFirstCall)
      {
        lock (SystemManager.services)
        {
          service = SystemManager.services[name];
          if (service is InactiveService)
          {
            service.Start();
            service = SystemManager.services[name];
          }
        }
      }
      return service;
    }

    internal static IModule InstallingModule { get; private set; }

    internal static void InvalidateModuleDependencies(string moduleName)
    {
      PageManager manager = PageManager.GetManager();
      using (new DataSyncModeRegion((IManager) manager))
      {
        IEnumerable<PageData> source;
        if (moduleName == "ResponsiveDesign")
        {
          source = (IEnumerable<PageData>) manager.GetPageDataList().Where<PageData>((Expression<Func<PageData, bool>>) (x => x.NavigationNode.RootNodeId != SiteInitializer.BackendRootNodeId));
        }
        else
        {
          string[] moduleWidgets = ToolboxesConfig.GetModuleWidgets(moduleName);
          source = manager.GetPagesContainingWidget(moduleWidgets);
        }
        IEnumerable<PageData> items = source.Where<PageData>((Func<PageData, bool>) (p => p.Version > 0));
        SystemManager.BulkUpdateItems<PageData>((IManager) manager, items, (System.Action<PageData>) (p => ++p.BuildStamp));
        manager.SaveChanges();
      }
    }

    internal static void BulkUpdateItems<TItem>(
      IManager manger,
      IEnumerable<TItem> items,
      System.Action<TItem> setAction,
      int pageSize = 20,
      bool commitOnPage = false)
    {
      int num1 = items.Count<TItem>();
      int num2 = 0;
      if (num1 > 0)
        num2 = (int) Math.Ceiling((double) num1 / (double) pageSize);
      for (int index = 0; index < num2; ++index)
      {
        foreach (TItem obj in items.Skip<TItem>(index * pageSize).Take<TItem>(pageSize))
          setAction(obj);
        if (string.IsNullOrEmpty(manger.TransactionName))
        {
          if (commitOnPage)
            manger.SaveChanges();
          else
            manger.Provider.FlushTransaction();
        }
        else if (commitOnPage)
          TransactionManager.CommitTransaction(manger.TransactionName);
        else
          TransactionManager.FlushTransaction(manger.TransactionName);
      }
    }

    internal static bool IsItemAccessble(object item) => !(item is IModuleDependentItem moduleDependentItem) || SystemManager.ValidateModuleItem(moduleDependentItem);

    internal static bool ValidateModuleItem(IModuleDependentItem item)
    {
      string moduleName = item.ModuleName;
      return string.IsNullOrEmpty(moduleName) || SystemManager.IsModuleAccessible(moduleName);
    }

    /// <summary>
    /// Determines whether the specified module is enabled for the application instance.
    /// </summary>
    /// <param name="moduleName"></param>
    /// <returns></returns>
    internal static bool IsModuleEnabled(string moduleName) => SystemManager.IsModuleEnabled(moduleName, out IModule _);

    /// <summary>
    /// Determines whether the specified application module is enabled for the application instance.
    /// </summary>
    /// <param name="applicationModuleName"></param>
    /// <returns></returns>
    internal static bool IsApplicationModuleEnabled(string applicationModuleName) => SystemManager.GetApplicationModule(applicationModuleName) != null;

    /// <summary>
    /// Determines whether the specified module is enabled for the application instance.
    /// </summary>
    /// <param name="moduleName"></param>
    /// <param name="module"></param>
    /// <returns></returns>
    internal static bool IsModuleEnabled(string moduleName, out IModule module)
    {
      module = SystemManager.GetModule(moduleName);
      return module != null;
    }

    /// <summary>Gets the default provider name for the provided type.</summary>
    /// <param name="typeName">Name of the type.</param>
    /// <returns></returns>
    internal static string GetDefaultProvider(string typeName)
    {
      Type c = TypeResolutionService.ResolveType(typeName, false);
      if (c == (Type) null)
        return (string) null;
      IManager mappedManager = ManagerBase.GetMappedManager(typeName);
      Telerik.Sitefinity.Multisite.MultisiteContext.SiteDataSourceLinkProxy defaultProvider = SystemManager.CurrentContext.CurrentSite.GetDefaultProvider(!typeof (DynamicContent).IsAssignableFrom(c) ? mappedManager.GetType().FullName : ModuleBuilderManager.GetModules().GetTypeByFullName(typeName).ModuleName);
      return defaultProvider != null ? defaultProvider.ProviderName : mappedManager.Provider.Name;
    }

    /// <summary>
    /// Determines whether the specified module is accessible in context of this site.
    /// </summary>
    /// <param name="moduleName"></param>
    /// <returns></returns>
    internal static bool IsModuleAccessible(string moduleName)
    {
      IModule module;
      return SystemManager.IsModuleEnabled(moduleName, out module) && SystemManager.CurrentContext.CurrentSite.IsModuleAccessible(module);
    }

    /// <summary>
    /// Determines whether the restriction level is enabled/disabled.
    /// </summary>
    /// <param name="restrictionLevel">The restriction level to check for.</param>
    /// <returns>True if restrictionLevel is NOT enabled and false if it is.</returns>
    internal static bool IsOperationEnabled(RestrictionLevel restrictionLevel) => Telerik.Sitefinity.Configuration.Config.SuppressRestriction || (Telerik.Sitefinity.Configuration.Config.RestrictionLevel & restrictionLevel) == RestrictionLevel.Default;

    /// <summary>Gets the requested application module.</summary>
    public static IModule GetApplicationModule(string name)
    {
      if (string.IsNullOrEmpty(name))
        throw new ArgumentNullException(nameof (name));
      if (SystemManager.appModules == null)
        return (IModule) null;
      if (SystemManager.Initializing && SystemManager.InstallingModule != null && SystemManager.InstallingModule.Name == name)
        return SystemManager.InstallingModule;
      IModule appModule;
      if (SystemManager.appModules.TryGetValue(name, out appModule) && appModule is InactiveModule && appModule.Startup == StartupType.OnFirstCall)
      {
        lock (SystemManager.appModules)
        {
          appModule = SystemManager.appModules[name];
          if (appModule is InactiveModule inactiveModule)
          {
            if (inactiveModule.Startup == StartupType.Manual)
              throw new InvalidOperationException("The requested module \"{0}\" is not started.".Arrange((object) name));
            inactiveModule.Start();
            appModule = SystemManager.appModules[name];
          }
        }
      }
      return appModule;
    }

    public static IModule GetDynamicModule(string name)
    {
      if (string.IsNullOrEmpty(name))
        throw new ArgumentNullException(nameof (name));
      if (SystemManager.dynamicModules == null)
        return (IModule) null;
      IModule module;
      return SystemManager.dynamicModules.TryGetValue(name, out module) ? module : (IModule) null;
    }

    internal static IEnumerable<DynamicAppModule> GetDynamicModules() => !Bootstrapper.IsReady ? (IEnumerable<DynamicAppModule>) new List<DynamicAppModule>() : SystemManager.dynamicModules.Values.OfType<DynamicAppModule>();

    /// <summary>Gets the requested module.</summary>
    public static IModule GetModule(string name) => SystemManager.GetApplicationModule(name) ?? SystemManager.GetDynamicModule(name);

    /// <summary>Restarts the application</summary>
    /// <param name="atemptFullRestart">if set to <c>true</c> [attempt full restart].</param>
    /// <param name="sendRestartApplicationSystemMessage">if set to <c>true</c> will send restart application message.</param>
    /// <returns></returns>
    [Obsolete("Use the overload methods to specify the reason for the restart")]
    public static bool RestartApplication(
      bool attemptFullRestart,
      bool sendRestartApplicationSystemMessage = true)
    {
      SystemRestartFlags flags = SystemRestartFlags.Default;
      if (attemptFullRestart)
        flags |= SystemRestartFlags.AttemptFullRestart;
      return SystemManager.RestartApplication(OperationReason.UnknownReason(), flags, sendRestartApplicationSystemMessage);
    }

    /// <summary>
    /// Restarts the application.
    /// A reason for the restart should be provider in order to have useful logging.
    /// </summary>
    /// <param name="restartReason">The restart reason.</param>
    /// <param name="flags">The flags.</param>
    /// <param name="sendRestartApplicationSystemMessage">The send restart application system message.</param>
    /// <returns></returns>
    public static bool RestartApplication(
      string restartReason,
      SystemRestartFlags flags = SystemRestartFlags.Default,
      bool sendRestartApplicationSystemMessage = true)
    {
      return SystemManager.RestartApplication(OperationReason.Parse(restartReason), flags, sendRestartApplicationSystemMessage);
    }

    /// <summary>
    /// Restarts the application.
    /// A reason for the restart should be provider in order to have useful logging.
    /// </summary>
    /// <param name="restartReason">The restart reason.</param>
    /// <param name="flags">The flags.</param>
    /// <param name="sendRestartApplicationSystemMessage">The send restart application system message.</param>
    /// <returns></returns>
    public static bool RestartApplication(
      OperationReason restartReason,
      SystemRestartFlags flags = SystemRestartFlags.Default,
      bool sendRestartApplicationSystemMessage = true)
    {
      SystemManager.restarted = false;
      if (!SystemManager.Initializing)
      {
        lock (SystemManager.syncLock)
        {
          if (!SystemManager.restarted)
          {
            SystemManager.pendingRestart = false;
            bool flag = (flags & SystemRestartFlags.AttemptFullRestart) > SystemRestartFlags.Default;
            if (SystemManager.ShuttingDown != null)
            {
              CancelEventArgs e = new CancelEventArgs();
              SystemManager.ShuttingDown((object) null, e);
              if (e.Cancel)
                return SystemManager.restarted;
            }
            Bootstrapper.Stop();
            OperationReason reason = restartReason;
            NameValueCollection operationParameters = new NameValueCollection();
            operationParameters.Add(nameof (flags), flags.ToString());
            operationParameters.Add(nameof (sendRestartApplicationSystemMessage), sendRestartApplicationSystemMessage.ToString());
            int flags1 = (int) flags;
            SystemManager.LogRestartOperation(SystemManager.RestartOperationKind.ApplicationRestart, reason, operationParameters, (SystemRestartFlags) flags1);
            if (flag && SystemManager.TryRestartApplication(restartReason.ToString()))
            {
              Thread.Sleep(300);
              if (SystemManager.Shutdown != null)
                SystemManager.Shutdown((object) null, EventArgs.Empty);
            }
            else
            {
              Thread.Sleep(2000);
              if ((flags & SystemRestartFlags.ResetModel) > SystemRestartFlags.Default)
              {
                restartReason.AddInfo("SkipMappingAggregation");
                OpenAccessConnection.CleanAll(restartReason, false);
              }
              SystemManager.CleanApplication();
              Bootstrapper.Bootstrap();
            }
            SystemManager.restarted = true;
            if (sendRestartApplicationSystemMessage)
              SystemMessageDispatcher.QueueSystemMessage((SystemMessageBase) new ResetApplicationMessage(restartReason, flags));
          }
        }
      }
      else
        SystemManager.pendingRestart = true;
      return SystemManager.restarted;
    }

    internal static void CleanApplication()
    {
      if (SystemManager.Shutdown != null)
        SystemManager.Shutdown((object) null, EventArgs.Empty);
      if (SystemManager.services != null)
      {
        foreach (IService service in SystemManager.services.Values)
          service.Stop();
      }
      lock (SystemManager.cacheSyncLock)
      {
        foreach (KeyValuePair<string, WeakReference> keyValuePair in new List<KeyValuePair<string, WeakReference>>((IEnumerable<KeyValuePair<string, WeakReference>>) SystemManager.cacheManagers))
        {
          WeakReference weakReference = keyValuePair.Value;
          if (weakReference.Target == null)
            SystemManager.cacheManagers.Remove(keyValuePair.Key);
          else if (weakReference.Target is ICacheManager target3)
            target3.Flush();
          else if (weakReference.Target is IDictionary target2)
            target2.Clear();
          else if (weakReference.Target is IList target1)
          {
            target1.Clear();
          }
          else
          {
            InvalidCastException exceptionToHandle = new InvalidCastException("Unsupported cache type.");
            if (Exceptions.HandleException((Exception) exceptionToHandle, ExceptionPolicyName.IgnoreExceptions))
              throw exceptionToHandle;
          }
        }
        SystemManager.cacheManagers.Clear();
      }
      DataExtensions.ClearAppSettings();
      AppSettings.Clear();
      if (SystemManager.PendingServiceStackPlugins != null)
        SystemManager.PendingServiceStackPlugins.Clear();
      if (ServiceStackHost.Instance != null)
        ServiceStackHost.Instance.Dispose();
      if (SystemManager.appModules != null)
      {
        foreach (IModule module in SystemManager.appModules.Values.AsEnumerable<IModule>())
        {
          module.Unload();
          if (module is IDisposable disposable)
            disposable.Dispose();
        }
        SystemManager.appModules.Clear();
        SystemManager.appModules = (Dictionary<string, IModule>) null;
      }
      if (SystemManager.crontabTasksToRun != null)
      {
        SystemManager.crontabTasksToRun.Clear();
        SystemManager.crontabTasksToRun = (Dictionary<string, Func<ScheduledTask>>) null;
      }
      if (SystemManager.dynamicModules != null)
      {
        SystemManager.dynamicModules.Clear();
        SystemManager.dynamicModules = (Dictionary<string, IModule>) null;
      }
      if (SystemManager.services != null)
      {
        SystemManager.services.Clear();
        SystemManager.services = (Dictionary<string, IService>) null;
      }
      SystemManager.actionMessageTemplates = (List<IActionMessageTemplate>) null;
      SystemManager.context = (SitefinityContextBase) null;
      SystemManager.DataSourceRegistry.Clear();
      ObjectFactory.Clear();
      Telerik.Sitefinity.Configuration.Config.Clean();
      SystemManager.TypeRegistry.Clear();
      SystemManager.TypeRegistry = (SitefinityTypeRegistry) null;
      Telerik.Sitefinity.Modules.ControlTemplates.ControlTemplates.Clear();
      StatisticCache.Current = (StatisticCache) null;
      ManagerBase.ClearRegisteredManagersCache();
      ServiceBus.Clear();
      SystemManager.basicSettings.Clear();
      SystemManager.cachedManagerTypes.Clear();
      SystemManager.NotLoadedModulesErrors.Clear();
      SystemManager.FlushDefaultDataInitializers(false);
      GC.Collect();
    }

    internal static void LogRestartOperation(
      SystemManager.RestartOperationKind operationKind,
      OperationReason reason,
      NameValueCollection operationParameters,
      SystemRestartFlags flags = SystemRestartFlags.Default)
    {
      SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
      StringBuilder stringBuilder = new StringBuilder();
      StackTrace stackTrace = new StackTrace();
      for (int index = 1; index < stackTrace.FrameCount; ++index)
      {
        MethodBase method = stackTrace.GetFrame(index).GetMethod();
        string str = method.DeclaringType != (Type) null ? method.DeclaringType.FullName : "unknown";
        if (!str.StartsWith("Telerik.Microsoft"))
        {
          stringBuilder.AppendLine();
          stringBuilder.AppendFormat("{0}: {1}", (object) str, (object) method.ToString());
        }
      }
      string str1 = stringBuilder.ToString();
      Telerik.Sitefinity.Abstractions.Log.Write((object) string.Format("Operation: '{0}'. Parameters: '{1}'. Reason: '{2}'. User: '{3}'. StackTrace: '{4}'.", (object) operationKind.ToString(), (object) SystemManager.FormatParameters(operationParameters), (object) reason.ToString(), currentIdentity != null ? (object) currentIdentity.Name : (object) string.Empty, (object) str1), ConfigurationPolicy.Trace);
      EventHub.Raise((IEvent) new RestartOperationEvent()
      {
        OperationKind = operationKind,
        Parameters = operationParameters,
        Reason = reason,
        StackTrace = str1,
        User = (currentIdentity != null ? currentIdentity.Name : string.Empty),
        Flags = flags
      });
    }

    private static string FormatParameters(NameValueCollection parameters)
    {
      if (parameters == null || parameters.Count <= 0)
        return string.Empty;
      StringBuilder stringBuilder = new StringBuilder();
      foreach (object key in parameters.Keys)
      {
        if (key is string name)
        {
          if (stringBuilder.Length > 0)
            stringBuilder.Append(';');
          stringBuilder.Append(key);
          stringBuilder.Append(':');
          stringBuilder.Append(parameters[name]);
        }
      }
      return stringBuilder.ToString();
    }

    /// <summary>
    /// Gets an instance of type <see cref="T:Telerik.Microsoft.Practices.EnterpriseLibrary.Caching.CacheManager" />.
    /// </summary>
    /// <value>The cache.</value>
    public static ICacheManager Cache => SystemManager.GetCacheManager(CacheManagerInstance.Global);

    /// <summary>
    /// Gets an instance of cache manager with the specified name.
    /// </summary>
    /// <param name="instanceName">The name of the cache manager instance to get.</param>
    /// <returns></returns>
    public static ICacheManager GetCacheManager(CacheManagerInstance instanceName) => SystemManager.GetCacheManager(instanceName.ToString());

    /// <summary>
    /// Gets an instance of cache manager with the specified name.
    /// </summary>
    /// <param name="instanceName">The name of the cache manager instance to get.</param>
    /// <returns></returns>
    public static ICacheManager GetCacheManager(string instanceName)
    {
      WeakReference weakReference1;
      if (!SystemManager.cacheManagers.TryGetValue(instanceName, out weakReference1))
      {
        lock (SystemManager.cacheSyncLock)
        {
          if (!SystemManager.cacheManagers.TryGetValue(instanceName, out weakReference1))
          {
            ICacheManager target = ObjectFactory.Resolve<ICacheManager>(instanceName.ToString());
            weakReference1 = new WeakReference((object) target);
            SystemManager.cacheManagers.Add(instanceName, weakReference1);
            return target;
          }
        }
      }
      ICacheManager target1 = (ICacheManager) weakReference1.Target;
      if (target1 != null)
        return target1;
      lock (SystemManager.cacheSyncLock)
      {
        WeakReference weakReference2 = (WeakReference) null;
        if (SystemManager.cacheManagers.TryGetValue(instanceName, out weakReference2))
        {
          if (weakReference1 == weakReference2)
            SystemManager.cacheManagers.Remove(instanceName);
        }
      }
      return SystemManager.GetCacheManager(instanceName);
    }

    /// <summary>
    /// Creates new dictionary and registers it with the system.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <returns></returns>
    public static IDictionary<TKey, TValue> CreateStaticCache<TKey, TValue>() => SystemManager.CreateStaticCache<TKey, TValue>((IEqualityComparer<TKey>) EqualityComparer<TKey>.Default);

    /// <summary>
    /// Creates new dictionary and registers it with the system.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <returns></returns>
    public static IDictionary<TKey, TValue> CreateStaticCache<TKey, TValue>(
      IEqualityComparer<TKey> comperer)
    {
      Dictionary<TKey, TValue> cache = new Dictionary<TKey, TValue>(comperer);
      SystemManager.RegisterCache((IDictionary) cache);
      return (IDictionary<TKey, TValue>) cache;
    }

    /// <summary>
    /// Registers the a cache with the system so it can be cleared when the system is restarted.
    /// The SystemManager will maintain week reference with the cache.
    /// </summary>
    /// <param name="cache">The cache.</param>
    public static void RegisterCache(IDictionary cache) => SystemManager.RegisterCacheInternal((object) cache);

    /// <summary>
    /// Registers the a cache with the system so it can be cleared when the system is restarted.
    /// The SystemManager will maintain week reference with the cache.
    /// </summary>
    /// <param name="cache">The cache.</param>
    public static void RegisterCache(IList cache) => SystemManager.RegisterCacheInternal((object) cache);

    public static void RegisterCacheInternal(object cache)
    {
      string key = cache != null ? cache.GetHashCode().ToString() : throw new ArgumentNullException(nameof (cache));
      WeakReference reference = (WeakReference) null;
      if (SystemManager.cacheManagers.TryGetValue(key, out reference) && !SystemManager.TryCleanupCache(key, reference))
        return;
      lock (SystemManager.cacheSyncLock)
      {
        if (SystemManager.cacheManagers.TryGetValue(key, out reference) && !SystemManager.TryCleanupCache(key, reference))
          return;
        WeakReference weakReference = new WeakReference(cache);
        SystemManager.cacheManagers.Add(key, weakReference);
      }
    }

    private static bool TryCleanupCache(string key, WeakReference reference)
    {
      if (reference.Target == null)
      {
        lock (SystemManager.cacheSyncLock)
        {
          WeakReference weakReference = (WeakReference) null;
          if (SystemManager.cacheManagers.TryGetValue(key, out weakReference))
          {
            if (reference == weakReference)
            {
              SystemManager.cacheManagers.Remove(key);
              return true;
            }
          }
        }
      }
      return false;
    }

    public static void RunWithElevatedPrivilege(
      SystemManager.RunWithElevatedPrivilegeDelegate delegateToRun)
    {
      SystemManager.RunWithElevatedPrivilege(delegateToRun, new object[0]);
    }

    public static void RunWithElevatedPrivilege(
      SystemManager.RunWithElevatedPrivilegeDelegate delegateToRun,
      object[] parameters)
    {
      SystemManager.RunWithElevatedPrivilege(delegateToRun, parameters, SystemManager.RootUrl);
    }

    public static void RunWithElevatedPrivilege(
      SystemManager.RunWithElevatedPrivilegeDelegate delegateToRun,
      object[] parameters,
      string urlRequest)
    {
      HttpContext httpContext = SystemManager.GetHttpContext(urlRequest);
      HttpContextWrapper context = new HttpContextWrapper(httpContext);
      HttpContext current = HttpContext.Current;
      if (current != null)
        httpContext.ApplicationInstance = current.ApplicationInstance;
      HttpContext.Current = httpContext;
      SecurityManager.AuthenticateSystemRequest((HttpContextBase) context);
      try
      {
        delegateToRun(parameters);
      }
      finally
      {
        SystemManager.ClearCurrentTransactions();
        HttpContext.Current = current;
      }
    }

    internal static HttpContext GetHttpContext(string urlRequest)
    {
      System.Web.HttpResponse response = new System.Web.HttpResponse((TextWriter) new StringWriter(new StringBuilder()));
      HttpContext httpContext = new HttpContext(new System.Web.HttpRequest("", urlRequest, ""), response);
      HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
      if (currentHttpContext != null)
      {
        foreach (DictionaryEntry dictionaryEntry in currentHttpContext.Items.OfType<DictionaryEntry>().Where<DictionaryEntry>((Func<DictionaryEntry, bool>) (e => e.Key is string && (e.Key as string).StartsWith("sf_method_"))))
          httpContext.Items[dictionaryEntry.Key] = dictionaryEntry.Value;
      }
      httpContext.Items[(object) "RadControlRandomNumber"] = (object) 0;
      return httpContext;
    }

    /// <summary>
    /// Registers a ServiceStack plugin.
    /// This plugin will be added to ServiceStack AppHost on bootstrapper initialized.
    /// ServiceStack plugins can register new service with new routes.
    /// Here we can also add plugins that represent filter as well.
    /// If your plugin also implements IPreInitPlugin it will get run before any plugins are registered.
    /// </summary>
    /// <param name="plugin">The ServiceStack plugin.</param>
    /// <param name="isHighPriority">If the priority level is hight we insert the plugin on the first place</param>
    public static void RegisterServiceStackPlugin(IPlugin plugin, bool isHighPriority = false)
    {
      if (isHighPriority)
        SystemManager.PendingServiceStackPlugins.Insert(0, plugin);
      else
        SystemManager.PendingServiceStackPlugins.Add(plugin);
    }

    /// <summary>
    /// Registers a dynamic WCF restful service. Works only for Http based services that are handled by the Route Handling
    /// Using this method allows to skip the distribution of physical svc files
    /// </summary>
    /// <param name="serviceType">Type of the service.</param>
    /// <param name="appRelativeUrl">The app relative URL.</param>
    /// <remarks>The registration of this service should be before any attempt to call it. A good place for this registration is on Module.Intialize
    /// </remarks>
    public static void RegisterWebService(Type serviceType, string appRelativeUrl) => SystemManager.RegisterWebService(serviceType, appRelativeUrl, (ServiceHostFactory) new WcfHostFactory(), "undefined", false);

    /// <summary>
    /// Registers a dynamic WCF restful service. Works only for Http based services that are handled by the Route Handling
    /// Using this method allows to skip the distribution of physical svc files
    /// </summary>
    /// <param name="serviceType">Type of the service.</param>
    /// <param name="appRelativeUrl">The app relative URL.</param>
    /// <param name="moduleName">The name of the module to which the service is related.</param>
    /// <param name="requireBasicAuthentication">Whether or not this service supports Basic authentication</param>
    /// <remarks>The registration of this service should be before any attempt to call it. A good place for this registration is on Module.Intialize
    /// </remarks>
    public static void RegisterWebService(
      Type serviceType,
      string appRelativeUrl,
      string moduleName,
      bool requireBasicAuthentication = false)
    {
      SystemManager.RegisterWebService(serviceType, appRelativeUrl, (ServiceHostFactory) new WcfHostFactory(), moduleName, requireBasicAuthentication);
    }

    /// <summary>
    /// Registers a dynamic WCF restful service. Works only for Http based services that are handled by the Route Handling
    /// Using this method allows to skip the distribution of physical svc files
    /// </summary>
    /// <param name="serviceType">Type of the service.</param>
    /// <param name="appRelativeUrl">The app relative URL.</param>
    /// <param name="hostFactory">The host factory which will be used.</param>
    /// <param name="moduleName">The name of Rethe module to which the service is related.</param>
    /// <param name="requireBasicAuthentication">Whether or not this service supports Basic authentication</param>
    /// <remarks>The registration of this service should be before any attempt to call it. A good place for this registration is on Module.Intialize
    /// </remarks>
    public static void RegisterWebService(
      Type serviceType,
      string appRelativeUrl,
      ServiceHostFactory hostFactory,
      string moduleName,
      bool requireBasicAuthentication)
    {
      ServiceRoute route = (ServiceRoute) null;
      lock (SystemManager.serviceRegSyncLock)
      {
        if (!SystemManager.serviceRoutes.TryGetValue(appRelativeUrl, out route))
        {
          route = new ServiceRoute(appRelativeUrl, (ServiceHostFactoryBase) hostFactory, serviceType);
          SystemManager.serviceRoutes.TryAdd(appRelativeUrl, route);
        }
      }
      SystemManager.RegisterRoute(appRelativeUrl, (RouteBase) route, moduleName, requireBasicAuthentication);
    }

    /// <summary>Adds or replaces a route in the route table.</summary>
    /// <param name="route">The route that will be added.</param>
    /// <remarks>The registration of this route should be before any attempt to call it. A good place for this registration is on Module.Intialize
    /// </remarks>
    [Obsolete("Use the overload of the RegisterRoute method, which accepts: routeName, route, moduleName")]
    public static void RegisterRoute(Route route) => SystemManager.RegisterRoute(Guid.NewGuid().ToString(), (RouteBase) route, "undefinied", false);

    /// <summary>Adds a route in the route table.</summary>
    /// <param name="routeName">The name of the route.</param>
    /// <param name="route">The route that will be added.</param>
    /// <param name="moduleName">The name of the module to which the route is related.</param>
    /// <param name="requireBasicAuthentication">Whether or not this route supports Basic authentication</param>
    /// <remarks>The registration of this route should be before any attempt to call it. A good place for this registration is on Module.Intialize
    /// </remarks>
    public static void RegisterRoute(
      string routeName,
      RouteBase route,
      string moduleName,
      bool requireBasicAuthentication)
    {
      RouteRegistration routeRegistration = new RouteRegistration(routeName, route, moduleName, requireBasicAuthentication);
      if (!SystemManager.Initializing)
      {
        int indexOf = RouteManager.GetIndexOf("Frontend");
        if (indexOf >= 0)
          RouteManager.RegisterRoute(routeRegistration, indexOf);
        else
          RouteManager.RegisterRoute(routeRegistration);
      }
      else
        SystemManager.PendingRouteRegistrations.Add(routeRegistration);
    }

    /// <summary>Registers the basic settings.</summary>
    /// <typeparam name="TSettingsControl">The type of the settings control.</typeparam>
    /// <typeparam name="TDataContact">The type of the data contact.</typeparam>
    /// <param name="settingsName">Name of the settings.</param>
    /// <param name="settingsTitle">The settings title.</param>
    /// <param name="settingsResourceClass">The settings resource class.</param>
    /// <param name="allowSettingPerSite">if set to <c>true</c> [allow setting per site].</param>
    internal static void RegisterBasicSettings<TSettingsControl, TDataContact>(
      string settingsName,
      string settingsTitle,
      string settingsResourceClass,
      bool allowSettingPerSite = false)
    {
      SystemManager.RegisterBasicSettings(settingsName, typeof (TSettingsControl), settingsTitle, settingsResourceClass, typeof (TDataContact), allowSettingPerSite);
    }

    /// <summary>
    /// Registers basics settings view. This represents user friendly module administration UI that is accessible from Sitefinity basic settings screen
    /// </summary>
    /// <typeparam name="TSettingsControl">The CLR type of the settings view control. For example: GeneralBasicSettingsView</typeparam>
    /// <param name="settingsName">Name of the settings. This is also used as the last part of the Url that opens the settings</param>
    /// <param name="settingsTitle">The settings title.</param>
    /// <param name="settingsResourceClass">The settings title resource class. If null the title is not localized and is taken as it is</param>
    public static void RegisterBasicSettings<TSettingsControl>(
      string settingsName,
      string settingsTitle,
      string settingsResourceClass,
      bool allowSettingPerSite = false)
    {
      SystemManager.RegisterBasicSettings(settingsName, typeof (TSettingsControl), settingsTitle, settingsResourceClass, allowSettingPerSite: allowSettingPerSite);
    }

    /// <summary>
    /// Registers basics settings view. This represents user friendly module administration UI that is accessible from Sitefinity basic settings screen
    /// </summary>
    /// <param name="settingsName">Name of the settings. This is also used as the last part of the Url that opens the settings</param>
    /// <param name="viewType">The CLR type of the settings view control. For example: typeof(GeneralBasicSettingsView)</param>
    /// <param name="settingsTitle">The settings title.</param>
    /// <param name="settingsResourceClass">The settings title resource class. If null the title is not localized and is taken as it is</param>
    public static void RegisterBasicSettings(
      string settingsName,
      Type viewType,
      string settingsTitle,
      string settingsResourceClass,
      Type dataContractType = null,
      bool allowSettingPerSite = false)
    {
      BasicSettingsRegistration settingsRegistration = new BasicSettingsRegistration(settingsName, viewType, settingsTitle, settingsResourceClass);
      settingsRegistration.AllowSettingsPerSite = allowSettingPerSite;
      if (dataContractType != (Type) null)
      {
        if (!typeof (ISettingsDataContract).IsAssignableFrom(dataContractType))
          throw new Exception("The type '{0}' must be assignable from '{1}'".Arrange((object) dataContractType.FullName, (object) typeof (ISettingsDataContract).FullName));
      }
      else if (viewType.IsGenericType)
      {
        foreach (Type genericArgument in viewType.GetGenericArguments())
        {
          if (typeof (ISettingsDataContract).IsAssignableFrom(genericArgument))
          {
            dataContractType = genericArgument;
            break;
          }
        }
      }
      settingsRegistration.DataContractType = dataContractType;
      lock (SystemManager.basicSettings)
        SystemManager.basicSettings.Add(settingsRegistration);
    }

    /// <summary>
    /// Register crontab task that needs to be invoked after the system is bootstraped
    /// </summary>
    public static void RegisterCrontabTask(string taskName, Func<ScheduledTask> getNewInstance) => SystemManager.CrontabTasksToRun.Add(taskName, getNewInstance);

    internal static void RegisterUserProfileType<TUserProfile>() where TUserProfile : UserProfile
    {
      if (SystemManager.registeredUserProfileTypes.Contains(typeof (TUserProfile)))
        return;
      lock (SystemManager.registeredUserProfileTypes)
      {
        if (SystemManager.registeredUserProfileTypes.Contains(typeof (TUserProfile)))
          return;
        SystemManager.registeredUserProfileTypes.Add(typeof (TUserProfile));
      }
    }

    internal static IEnumerable<BasicSettingsRegistration> GetBasicSettingsRegistrations() => (IEnumerable<BasicSettingsRegistration>) SystemManager.basicSettings;

    internal static IEnumerable<BasicSettingsRegistration> GetEditableBasicSettingsRegistrations()
    {
      List<BasicSettingsRegistration> source = SystemManager.basicSettings;
      if (!SystemManager.IsOperationEnabled(RestrictionLevel.ReadOnlyConfigFile) || !ClaimsManager.GetCurrentIdentity().IsGlobalUser)
      {
        SitefinityContextBase currentContext = SystemManager.CurrentContext;
        if (currentContext != null && currentContext.GetSites().Count<Telerik.Sitefinity.Multisite.ISite>() == 1)
          return Enumerable.Empty<BasicSettingsRegistration>();
        source = source.Where<BasicSettingsRegistration>((Func<BasicSettingsRegistration, bool>) (s => s.AllowSettingsPerSite)).ToList<BasicSettingsRegistration>();
      }
      return (IEnumerable<BasicSettingsRegistration>) source;
    }

    internal static IEnumerable<IActionMessageTemplate> GetSystemActionMessageTemplates()
    {
      if (SystemManager.actionMessageTemplates == null)
      {
        lock (SystemManager.actionMessageTemplatesSyncLock)
        {
          if (SystemManager.actionMessageTemplates == null)
          {
            ConfigElementDictionary<string, AppModuleSettings> applicationModules = ConfigManager.GetManager().GetSection<SystemConfig>().ApplicationModules;
            SystemManager.actionMessageTemplates = new List<IActionMessageTemplate>();
            foreach (IActionMessageTemplatesProvider templatesProvider in SystemManager.ApplicationModules.Values.Where<IModule>((Func<IModule, bool>) (m => m is IActionMessageTemplatesProvider && SystemManager.IsModuleEnabled(m.Name))).Cast<IActionMessageTemplatesProvider>().Concat<IActionMessageTemplatesProvider>(SystemManager.SystemServices.Where<IService>((Func<IService, bool>) (s => s is IActionMessageTemplatesProvider && applicationModules[s.Name].StartupType != StartupType.Disabled)).Cast<IActionMessageTemplatesProvider>()))
              SystemManager.actionMessageTemplates.AddRange(templatesProvider.GetActionMessageTemplates());
            SystemManager.actionMessageTemplates.AddRange((IEnumerable<IActionMessageTemplate>) new IActionMessageTemplate[2]
            {
              (IActionMessageTemplate) new ItemRejectedMessageTemplate(),
              (IActionMessageTemplate) new ItemAwaitingForActionMessageTemplate()
            });
          }
        }
      }
      return (IEnumerable<IActionMessageTemplate>) SystemManager.actionMessageTemplates;
    }

    internal static bool TryRestartApplication(string restartReason)
    {
      try
      {
        Telerik.Sitefinity.Abstractions.Log.Write((object) string.Format("Attempting application domain unload. Reason: '{0}'. StackTrace: '{1}'", (object) restartReason, (object) Environment.StackTrace), TraceEventType.Information);
        Thread.CurrentPrincipal = (IPrincipal) null;
        if (HttpContext.Current != null)
          HttpContext.Current.User = (IPrincipal) null;
        HttpRuntime.UnloadAppDomain();
        Telerik.Sitefinity.Abstractions.Log.Write((object) string.Format("Application domain unload succeeded. StackTrace: '{0}'", (object) Environment.StackTrace), TraceEventType.Information);
        return true;
      }
      catch (Exception ex)
      {
        Telerik.Sitefinity.Abstractions.Log.Write((object) string.Format("Application domain unload was unsuccessful. Exception: {0}", (object) ex), TraceEventType.Error);
      }
      if (SystemManager.CurrentHttpContext != null)
      {
        string path = SystemManager.CurrentHttpContext.Request.PhysicalApplicationPath + "\\web.config";
        try
        {
          Telerik.Sitefinity.Abstractions.Log.Write((object) string.Format("Attempting web.config change in order to restart application. StackTrace: {0}", (object) Environment.StackTrace), TraceEventType.Information);
          File.SetLastWriteTimeUtc(path, DateTime.UtcNow);
          return true;
        }
        catch
        {
        }
      }
      return false;
    }

    internal static void OnFirstRequestBegin()
    {
      SystemManager.AbsolutePathRootUrlOfFirstRequest = SystemManager.CurrentHttpContext.Request.Url.GetLeftPart(UriPartial.Authority) + SystemManager.CurrentHttpContext.Request.ApplicationPath;
      if (!SystemManager.IsModuleEnabled(SchedulingModule.ModuleName))
        return;
      Scheduler.Instance.RescheduleNextRun();
    }

    internal static bool Initialize()
    {
      if (!SystemManager.Initializing)
      {
        lock (SystemManager.syncLock)
        {
          if (!SystemManager.Initializing)
          {
            SystemManager.Initializing = true;
            if (SystemManager.CurrentHttpContext != null)
              SystemManager.CurrentHttpContext.Server.ScriptTimeout = 1200;
            try
            {
              using (new MethodPerformanceRegion("MetadataManager"))
                MetadataManager.GetManager();
            }
            catch (InvalidOperationException ex)
            {
              bool flag = ObjectFactory.IsTypeRegistered<MetadataManager>(ManagerBase<MetaDataProvider>.GetDefaultProviderName().ToUpper());
              string message = "Exception on SystemManager.Initialize(): {0}; Initialized: {1}, ProvidersCount: {2}, IsTypeRegistered: {3}, Stack Trace: {4}".Arrange((object) ex.Message, (object) ManagerBase<MetaDataProvider>.initialized, (object) (ManagerBase<MetaDataProvider>.StaticProvidersCollection != null ? ManagerBase<MetaDataProvider>.StaticProvidersCollection.Count : 0), (object) flag, (object) Environment.StackTrace);
              if (ManagerBase<MetaDataProvider>.initialized && !flag)
              {
                Telerik.Sitefinity.Abstractions.Log.Write((object) message, ConfigurationPolicy.ErrorLog);
                ManagerBase<MetaDataProvider>.Uninitialize();
                MetadataManager.GetManager();
              }
              else if (Exceptions.HandleException((Exception) ex, ExceptionPolicyName.UnhandledExceptions))
                throw;
            }
            try
            {
              MetadataManager manager = MetadataManager.GetManager();
              IDictionary<string, SystemManager.ModuleVersionInfo> upgradeModuleVersions = SystemManager.GetOrUpgradeModuleVersions(manager);
              if (Telerik.Sitefinity.Configuration.Config.ConfigStorageMode == ConfigStorageMode.Auto)
              {
                OpenAccessXmlConfigStorageProvider databaseStorageProvider = ConfigManager.GetManager().Provider.GetDatabaseStorageProvider() as OpenAccessXmlConfigStorageProvider;
                using (SitefinityOAContext context = OpenAccessConnection.GetContext((IOpenAccessMetadataProvider) databaseStorageProvider, databaseStorageProvider.ConnectionName))
                {
                  ConfigMigrationImporter migrationImporter = new ConfigMigrationImporter();
                  if (migrationImporter.CanHandle(upgradeModuleVersions))
                    migrationImporter.Handle((OpenAccessContext) context, manager);
                }
              }
              SystemManager.appModules = new Dictionary<string, IModule>();
              SystemManager.dynamicModules = new Dictionary<string, IModule>();
              SystemManager.RegisterCrontabTasks();
              InstallContext installContext = new InstallContext();
              SystemConfig config = Telerik.Sitefinity.Configuration.Config.Get<SystemConfig>();
              int num1 = 0;
              SystemManager.ModuleVersionInfo moduleVersionInfo;
              if (upgradeModuleVersions.TryGetValue("Sitefinity", out moduleVersionInfo) && moduleVersionInfo.Version != (Version) null)
                num1 = moduleVersionInfo.Version.Build;
              int num2 = -1;
              Version version = Assembly.GetExecutingAssembly().GetName().Version;
              int build = version.Build;
              if (num1 < build)
              {
                SystemManager.upgradeFromVersion = moduleVersionInfo?.Version;
                SystemManager.EnterUpgrading();
                Telerik.Sitefinity.Abstractions.Log.Write((object) string.Format("Upgrading Progress Sitefinity CMS from build version {0} to {1}.", (object) (num1 == 0 ? 1 : num1), (object) build), ConfigurationPolicy.UpgradeTrace);
                config = installContext.GetConfig<SystemConfig>();
                SiteInitializer initializer;
                using (initializer = SiteInitializer.GetInitializer())
                {
                  initializer.Context = installContext;
                  ModuleVersion moduleVersion = initializer.MetadataManager.GetModuleVersion("Sitefinity") ?? initializer.MetadataManager.CreateModuleVersion("Sitefinity");
                  num2 = num1;
                  if (num2 == 0 && initializer.PageManager.GetPageNodes().Count<PageNode>() == 0)
                  {
                    initializer.InstallDefaults();
                    installContext.GetConfig<ProjectConfig>().DateCreated = DateTime.UtcNow;
                  }
                  else
                  {
                    num2 = num2 == 0 ? 1 : num2;
                    initializer.Upgrade(num2);
                    moduleVersion.PreviousVersion = moduleVersion.Version;
                  }
                  moduleVersion.Version = version;
                  initializer.SaveChanges();
                  installContext.UpgradeInfo = new UpgradeContext(num2, build);
                  installContext.SaveChanges();
                }
                SecurityManager.GetManager();
                UserProfileManager.GetManager();
                RoleManager.GetManager();
                UserManager.GetManager();
                UserActivityManager.GetManager();
                WorkflowManager.GetManager();
                SiteSettingsManager.GetManager();
                ContentLocationsManager.GetManager();
                OutputCacheRelationsManager.GetManager();
                SystemManager.RestartApplication(OperationReason.FromKey("InternalRestartOnUpgrade"), sendRestartApplicationSystemMessage: false);
              }
              Bootstrapper.SetupNLBComunication();
              SystemManager.ApplyIncrementalGuidRange();
              SystemManager.RegisterUserProfileType<SitefinityProfile>();
              Telerik.Sitefinity.Modules.ControlTemplates.ControlTemplates.RegisterTemplatableControl(typeof (UserChangePasswordWidget), typeof (UserChangePasswordWidget));
              Telerik.Sitefinity.Modules.ControlTemplates.ControlTemplates.RegisterTemplatableControl(typeof (LightNavigationControl), typeof (PageSiteNode));
              Telerik.Sitefinity.Modules.ControlTemplates.ControlTemplates.RegisterTemplatableControl(typeof (RenderTemplate), typeof (RenderTemplate));
              Telerik.Sitefinity.Modules.ControlTemplates.ControlTemplates.RegisterTemplatableControl(typeof (NavTransformationTemplate), typeof (NavTransformationTemplate));
              using (new MethodPerformanceRegion("Initialize Services:"))
              {
                SystemManager.services = new Dictionary<string, IService>();
                foreach (ModuleSettings systemService in (ConfigElementCollection) config.SystemServices)
                {
                  using (new MethodPerformanceRegion(systemService.Name))
                    SystemManager.InitializeService(systemService, systemService.StartupType == StartupType.OnApplicationStart);
                }
              }
              SystemManager.RegisterWebService(typeof (FlatTaxonService), "Sitefinity/Frontend/Services/Taxonomies/FlatTaxon.svc/");
              using (new MethodPerformanceRegion("Initialize Modules:"))
              {
                foreach (ModuleSettings applicationModule in (ConfigElementCollection) config.ApplicationModules)
                {
                  SystemManager.ModuleVersionInfo moduleVersion;
                  upgradeModuleVersions.TryGetValue(applicationModule.Name, out moduleVersion);
                  SystemManager.InitializeModule(applicationModule, installContext, moduleVersion);
                }
              }
              using (new MethodPerformanceRegion("Initialize taxonomy"))
                new TaxonomyInitializer().Initialize();
              SystemManager.RegisterManagerTypes();
              Type type = typeof (WorkflowManager);
              if (!SystemManager.MultisiteEnabledManagers.Contains(type))
                SystemManager.MultisiteEnabledManagers.Add(type);
              using (new MethodPerformanceRegion("Load Modules:"))
              {
                IModule module1;
                if (SystemManager.appModules.TryGetValue("PackagingModule", out module1))
                {
                  using (new MethodPerformanceRegion(module1.Name))
                    module1.Load();
                }
                List<Task> taskList = new List<Task>();
                foreach (IModule module2 in SystemManager.appModules.Values)
                {
                  IModule module = module2;
                  if (!(module.Name == "PackagingModule"))
                  {
                    Task task = new Task((System.Action) (() =>
                    {
                      using (new MethodPerformanceRegion(module.Name))
                        module.Load();
                    }));
                    task.Start();
                    taskList.Add(task);
                  }
                }
                Task.WaitAll(taskList.ToArray());
              }
              if (SecurityManager.AllowSeparateUsersPerSite)
                SystemManager.DataSourceRegistry.RegisterDataSource((IDataSource) new DataSourceProxy(typeof (UserManager)));
              if (!SystemManager.mvcEnabled)
              {
                using (new MethodPerformanceRegion("Enable MVC"))
                {
                  try
                  {
                    if (ObjectFactory.IsTypeRegistered<IMvcInitializer>())
                    {
                      Telerik.Sitefinity.Configuration.Config.DefaultProvider.SuppressSecurityChecks = true;
                      ObjectFactory.Resolve<IMvcInitializer>().EnableMvcSupport();
                      Telerik.Sitefinity.Configuration.Config.DefaultProvider.SuppressSecurityChecks = false;
                      SystemManager.mvcEnabled = true;
                    }
                  }
                  catch (Exception ex)
                  {
                    if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
                      throw;
                  }
                }
              }
              if (num2 != -1)
              {
                MetadataManager.GetManager().Dispose();
                if (num2 > 0)
                {
                  if (num2 < 4100)
                    SystemManager.ScheduleUpdateContentLocationsServiceTask();
                  else if (num2 < SitefinityVersion.Sitefinity11_0.Build)
                    SystemManager.ScheduleSetControlIdToContentLocationsTask();
                  if (num2 < SitefinityVersion.Sitefinity6_2.Build)
                    CommentsModuleConfig.MigrateCommentsGlobalSettings();
                  if (num2 < SitefinityVersion.Sitefinity7_0.Build)
                  {
                    ApprovalRecordsUpgrader.UpgradeAllAvailableDbs();
                    new ContentLocationsUpgrader().UpgradeMultilingualSplitPagesContentLocations(SitefinityVersion.Sitefinity7_0);
                  }
                  if (num2 < SitefinityVersion.Sitefinity12_0_IB_7021.Build)
                    SystemManager.ScheduleCreateMissingDependenciesTask();
                }
                foreach (Type sectionType in ObjectFactory.GetArgsForType(typeof (ConfigSection)).Select<RegisterEventArgs, Type>((Func<RegisterEventArgs, Type>) (a => a.TypeTo)))
                  Telerik.Sitefinity.Configuration.Config.Get(sectionType);
              }
              using (new MethodPerformanceRegion("On Modules Initialized Event"))
                SystemManager.OnModulesInitialized(installContext);
              SystemManager.SubscribeForEvents();
              SystemManager.startedOn = DateTime.UtcNow;
              installContext.Dispose();
              SystemManager.Initializing = false;
              SystemManager.ExitUpgrading();
              SystemManager.upgradeFromVersion = (Version) null;
              SiteMapBase.Reset();
              VirtualPathManager.Reset();
              OutputCacheWorker.Initialize();
              int num3 = SystemManager.pendingRestart ? 1 : 0;
              SystemManager.pendingRestart = false;
              return num3 != 0;
            }
            catch
            {
              SystemManager.UnlockUpgrade();
              throw;
            }
          }
        }
      }
      return false;
    }

    /// <summary>
    /// Register crontab scheduled tasks that needs to be invoked after the system is bootstraped
    /// </summary>
    private static void RegisterCrontabTasks()
    {
      SystemManager.CrontabTasksToRun.Add(VersioningCleanerTask.GetTaskName(), new Func<ScheduledTask>(VersioningCleanerTask.NewInstance));
      SystemManager.CrontabTasksToRun.Add(UsageTrackingTask.GetTaskName(), new Func<ScheduledTask>(UsageTrackingTask.NewInstance));
      SystemManager.CrontabTasksToRun.Add(TempItemsCleanupTask.GetTaskName(), new Func<ScheduledTask>(TempItemsCleanupTask.NewInstance));
    }

    private static IDictionary<string, SystemManager.ModuleVersionInfo> GetOrUpgradeModuleVersions(
      MetadataManager metadataManager)
    {
      Dictionary<string, SystemManager.ModuleVersionInfo> dictionary = metadataManager.GetModuleVersions().Where<ModuleVersion>((Expression<Func<ModuleVersion, bool>>) (m => string.IsNullOrEmpty(m.ErrorMessage))).ToDictionary<ModuleVersion, string, SystemManager.ModuleVersionInfo>((Func<ModuleVersion, string>) (v => v.ModuleName), (Func<ModuleVersion, SystemManager.ModuleVersionInfo>) (v => new SystemManager.ModuleVersionInfo(v)));
      if (dictionary.Count == 0)
      {
        ConfigManager manager = Telerik.Sitefinity.Configuration.Config.GetManager();
        SystemConfig systemConfig = Telerik.Sitefinity.Configuration.Config.Get<SystemConfig>();
        MigrationConfig section1 = manager.GetSection<MigrationConfig>();
        int build1 = section1.GetItem<int>("build");
        if (systemConfig.Build > 0 || build1 > 0)
        {
          List<AppModuleSettingsDto> applicationModules = SystemManager.GetApplicationModules(section1);
          Version version1 = (Version) null;
          Version version2;
          if (build1 == 0)
          {
            SystemConfig section2 = manager.GetSection<SystemConfig>();
            int num1 = 0;
            int num2 = 0;
            int build2 = section2.Build;
            section2.Build = 0;
            section2.PreviousBuild = 0;
            foreach (ModuleSettings applicationModule in (ConfigElementCollection) section2.ApplicationModules)
            {
              if (applicationModule.Version != (Version) null)
              {
                applicationModules.Add(applicationModule.ToDto());
                if (applicationModule.Type.StartsWith("Telerik.Sitefinity."))
                {
                  num1 = Math.Max(num1, applicationModule.Version.Major);
                  num2 = Math.Max(num2, applicationModule.Version.Minor);
                }
              }
              applicationModule.Version = (Version) null;
              applicationModule.ErrorMessage = string.Empty;
            }
            version2 = new Version(num1, num2, build2, 0);
            section1.SetItem<int>("build", build2);
            section1.SetItem<string>("version", version2.ToString());
            section1.SetJsonItem<List<AppModuleSettingsDto>>("applicationModules", applicationModules, version2);
            manager.SaveSection((ConfigSection) section1, true);
            manager.SaveSection((ConfigSection) section2, true);
          }
          else
          {
            string version3 = section1.GetItem<string>("version");
            version2 = !(version1 == (Version) null) ? new Version(version3) : new Version(0, 0, build1, 0);
          }
          ModuleVersion moduleVersion1 = metadataManager.CreateModuleVersion("Sitefinity");
          moduleVersion1.Version = version2;
          dictionary.Add(moduleVersion1.ModuleName, new SystemManager.ModuleVersionInfo(moduleVersion1));
          foreach (AppModuleSettingsDto moduleSettingsDto in applicationModules)
          {
            if (moduleSettingsDto.Version != (Version) null)
            {
              ModuleVersion moduleVersion2 = metadataManager.CreateModuleVersion(new ModuleSettings((ConfigElement) section1)
              {
                Name = moduleSettingsDto.Name,
                Version = moduleSettingsDto.Version,
                ErrorMessage = moduleSettingsDto.ErrorMessage
              });
              dictionary.Add(moduleVersion2.ModuleName, new SystemManager.ModuleVersionInfo(moduleVersion2));
            }
          }
          metadataManager.SaveChanges();
        }
      }
      return (IDictionary<string, SystemManager.ModuleVersionInfo>) dictionary;
    }

    private static List<AppModuleSettingsDto> GetApplicationModules(
      MigrationConfig config)
    {
      return config.GetJsonItem<List<AppModuleSettingsDto>>("applicationModules") ?? new List<AppModuleSettingsDto>();
    }

    internal static AppStatusTraceListener RegisterAppStatusTraceListener()
    {
      if (!(WebConfigurationManager.GetSection("system.web/customErrors") is CustomErrorsSection section) || section.Mode == CustomErrorsMode.On)
        return (AppStatusTraceListener) null;
      AppStatusTraceListener listener = new AppStatusTraceListener();
      if (SystemManager.AddListenerToLogSources((TraceListener) listener, "ErrorLog", "UpgradeTrace", "PackagingTrace"))
        return listener;
      listener.Dispose();
      return (AppStatusTraceListener) null;
    }

    private static bool AddListenerToLogSources(
      TraceListener listener,
      params string[] logSourceKeys)
    {
      if (logSourceKeys == null)
        return false;
      bool logSources = false;
      foreach (LogSource logSource in Telerik.Sitefinity.Abstractions.Log.Writer.TraceSources.Where<KeyValuePair<string, LogSource>>((Func<KeyValuePair<string, LogSource>, bool>) (s => ((IEnumerable<string>) logSourceKeys).Contains<string>(s.Key))).Select<KeyValuePair<string, LogSource>, LogSource>((Func<KeyValuePair<string, LogSource>, LogSource>) (l => l.Value)))
      {
        logSource.Listeners.Add(listener);
        logSources = true;
      }
      return logSources;
    }

    internal static void UnRegisterAppStatusTraceListener(
      TraceListener listener,
      params string[] logSourceKeys)
    {
      if (listener == null || logSourceKeys == null)
        return;
      foreach (LogSource logSource in Telerik.Sitefinity.Abstractions.Log.Writer.TraceSources.Where<KeyValuePair<string, LogSource>>((Func<KeyValuePair<string, LogSource>, bool>) (s => ((IEnumerable<string>) logSourceKeys).Contains<string>(s.Key))).Select<KeyValuePair<string, LogSource>, LogSource>((Func<KeyValuePair<string, LogSource>, LogSource>) (l => l.Value)))
        logSource.Listeners.Remove(listener);
    }

    internal static ISet<Type> CachedManagerTypes
    {
      get
      {
        lock (SystemManager.cachedManagerTypes)
          return SystemManager.cachedManagerTypes;
      }
    }

    internal static void AddCachedManagerTypes(IEnumerable<Type> managerTypes)
    {
      lock (SystemManager.cachedManagerTypes)
      {
        foreach (Type managerType in managerTypes)
          SystemManager.cachedManagerTypes.Add(managerType);
      }
    }

    private static void RegisterManagerTypes()
    {
      using (new MethodPerformanceRegion(nameof (RegisterManagerTypes)))
      {
        if (Bootstrapper.IsFirstBoot && !SystemManager.isUpgrading)
        {
          SystemManager.RegisterManagerTypes(ManagersInitializationMode.OnStartup);
          Task.Run((System.Action) (() =>
          {
            using (new MethodPerformanceRegion("ASYNC: RegisterManagerTypes on bootstrap"))
            {
              SystemManager.RegisterManagerTypes(ManagersInitializationMode.OnStartupAsync);
              SystemManager.FlushDefaultDataInitializers();
            }
          }));
        }
        else
        {
          SystemManager.RegisterManagerTypes(ManagersInitializationMode.OnStartupAsync | ManagersInitializationMode.OnStartup);
          SystemManager.FlushDefaultDataInitializers();
        }
      }
    }

    private static void RegisterManagerTypes(ManagersInitializationMode mode)
    {
      List<Type> managerTypes = new List<Type>();
      if ((mode & ManagersInitializationMode.OnStartup) > ManagersInitializationMode.OnDemand)
      {
        managerTypes.Add(typeof (PackagingManager));
        managerTypes.Add(typeof (SiteSettingsManager));
        managerTypes.Add(typeof (PageManager));
        managerTypes.Add(typeof (SecurityManager));
        managerTypes.Add(typeof (RoleManager));
        managerTypes.Add(typeof (UserManager));
        managerTypes.Add(typeof (UserActivityManager));
        managerTypes.Add(typeof (UserProfileManager));
      }
      if ((mode & ManagersInitializationMode.OnStartupAsync) > ManagersInitializationMode.OnDemand)
      {
        managerTypes.Add(typeof (WorkflowManager));
        managerTypes.Add(typeof (ContentLinksManager));
        managerTypes.Add(typeof (TaxonomyManager));
        managerTypes.Add(typeof (ContentLocationsManager));
        managerTypes.Add(typeof (VersionManager));
        managerTypes.Add(typeof (OutputCacheRelationsManager));
        foreach (Type type in (IEnumerable<Type>) SystemManager.OnManagerTypesRegistered())
          managerTypes.Add(type);
      }
      foreach (ModuleBase moduleBase in SystemManager.appModules.Values.OfType<ModuleBase>().Where<ModuleBase>((Func<ModuleBase, bool>) (x => (x.ManagersInitializationMode & mode) > ManagersInitializationMode.OnDemand)))
      {
        Type[] managers = moduleBase.Managers;
        if (managers != null)
        {
          foreach (Type type in managers)
            managerTypes.Add(type);
        }
      }
      IList<Type> typeList = (IList<Type>) new List<Type>();
      using (new DelayedDatabaseInitRegion())
      {
        foreach (Type type in managerTypes)
        {
          if (type.GetConstructor(new Type[0]) == (ConstructorInfo) null || !typeof (IManager).IsAssignableFrom(type) || !SystemManager.TryInitializeManager(type))
            typeList.Add(type);
        }
      }
      foreach (Type type in (IEnumerable<Type>) typeList)
        managerTypes.Remove(type);
      SystemManager.AddCachedManagerTypes((IEnumerable<Type>) managerTypes);
    }

    internal static void RegisterRegisterScheduledTasks() => Task.Run((System.Action) (() => SystemManager.RunWithElevatedPrivilege((SystemManager.RunWithElevatedPrivilegeDelegate) (p =>
    {
      bool flag = false;
      ICollection<string> scheduledTaskKeys = SystemManager.CrontabTasksToRun.Keys;
      SchedulingManager manager = SchedulingManager.GetManager();
      IQueryable<ScheduledTaskData> source = manager.GetTaskData().Where<ScheduledTaskData>((Expression<Func<ScheduledTaskData, bool>>) (t => scheduledTaskKeys.Contains(t.TaskName)));
      foreach (KeyValuePair<string, Func<ScheduledTask>> keyValuePair in (IEnumerable<KeyValuePair<string, Func<ScheduledTask>>>) SystemManager.CrontabTasksToRun)
      {
        KeyValuePair<string, Func<ScheduledTask>> task = keyValuePair;
        try
        {
          IQueryable<ScheduledTaskData> queryable1 = source.Where<ScheduledTaskData>((Expression<Func<ScheduledTaskData, bool>>) (t => t.TaskName == task.Key));
          ScheduledTaskData taskData = source.FirstOrDefault<ScheduledTaskData>((Expression<Func<ScheduledTaskData, bool>>) (t => t.TaskName == task.Key && !t.IsRunning));
          IQueryable<ScheduledTaskData> queryable2;
          if (taskData == null)
            queryable2 = queryable1;
          else
            queryable2 = source.Where<ScheduledTaskData>((Expression<Func<ScheduledTaskData, bool>>) (t => t.TaskName == task.Key && t.Id != taskData.Id));
          foreach (ScheduledTaskData task1 in (IEnumerable<ScheduledTaskData>) queryable2)
          {
            manager.DeleteTaskData(task1);
            flag = true;
          }
          ScheduledTask task2 = task.Value();
          if (task2 != null)
          {
            if (taskData != null)
            {
              if (taskData.ScheduleData != task2.ScheduleSpec)
              {
                Scheduler.Instance.TryToUpdateNextTaskRun(taskData, task2.ScheduleSpec);
                flag = true;
              }
            }
            else
            {
              Scheduler.Instance.TryToScheduleNextTaskRun(task2, manager);
              flag = true;
            }
          }
          else if (taskData != null)
          {
            manager.DeleteTaskData(taskData);
            flag = true;
          }
        }
        catch (Exception ex)
        {
          if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
            throw;
        }
      }
      if (!flag)
        return;
      manager.SaveChanges();
    }))));

    private static Task<bool> TryInitializeManagerAsync(Type managerType) => Task.Run<bool>((Func<bool>) (() =>
    {
      using (new DelayedDatabaseInitRegion())
        return SystemManager.TryInitializeManager(managerType);
    }));

    private static bool TryInitializeManager(Type managerType)
    {
      try
      {
        ManagerBase.GetManager(managerType);
      }
      catch (Exception ex)
      {
        if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
          throw;
      }
      return true;
    }

    private static void OnModulesInitialized(InstallContext installContext)
    {
      System.EventHandler<SystemInitializationEventArgs> modulesInitialized = SystemManager.ModulesInitialized;
      if (modulesInitialized == null)
        return;
      modulesInitialized((object) null, new SystemInitializationEventArgs(installContext));
    }

    internal static void SubscribeForEvents()
    {
      EventHub.Unsubscribe<UserDeleted>(new SitefinityEventHandler<UserDeleted>(SystemManager.UserDeletedEvent));
      EventHub.Subscribe<UserDeleted>(new SitefinityEventHandler<UserDeleted>(SystemManager.UserDeletedEvent));
      EventHub.Unsubscribe<RequestStartEvent>(new SitefinityEventHandler<RequestStartEvent>(SystemManager.RequestStart));
      EventHub.Subscribe<RequestStartEvent>(new SitefinityEventHandler<RequestStartEvent>(SystemManager.RequestStart));
      EventHub.Unsubscribe<RequestEndEvent>(new SitefinityEventHandler<RequestEndEvent>(SystemManager.RequestEnd));
      EventHub.Subscribe<RequestEndEvent>(new SitefinityEventHandler<RequestEndEvent>(SystemManager.RequestEnd));
    }

    internal static void RequestStart(RequestStartEvent eventData) => SystemManager.SetCorrelationActivityId();

    internal static void RequestEnd(RequestEndEvent eventData) => SystemManager.ResetCorrelationActivityId();

    private static void SetCorrelationActivityId()
    {
      SystemManager.CurrentHttpContext.Items[(object) "CorrelationManagerActivityId"] = (object) Trace.CorrelationManager.ActivityId;
      Trace.CorrelationManager.ActivityId = Guid.NewGuid();
    }

    private static void ResetCorrelationActivityId()
    {
      object obj = SystemManager.CurrentHttpContext.Items[(object) "CorrelationManagerActivityId"];
      Guid result;
      if (obj == null || !Guid.TryParse(obj.ToString(), out result))
        return;
      Trace.CorrelationManager.ActivityId = result;
    }

    internal static void UserDeletedEvent(UserDeleted eventData)
    {
      UserActivityManager manager = UserActivityManager.GetManager();
      if (manager.GetUserActivity(eventData.UserId, eventData.MembershipProviderName) == null)
        return;
      manager.DeleteUserActivity(eventData.UserId, eventData.MembershipProviderName);
      manager.SaveChanges();
    }

    private static ISet<Type> OnManagerTypesRegistered()
    {
      HashSet<Type> typeSet = new HashSet<Type>();
      Func<EventArgs, IEnumerable<Type>> managerTypesRegistered = SystemManager.ManagerTypesRegistered;
      if (managerTypesRegistered != null)
      {
        List<Delegate> delegateList = new List<Delegate>((IEnumerable<Delegate>) managerTypesRegistered.GetInvocationList());
        EventArgs eventArgs = new EventArgs();
        foreach (Delegate @delegate in delegateList)
        {
          if (@delegate.Method.Invoke(@delegate.Target, new object[1]
          {
            (object) eventArgs
          }) is IEnumerable<Type> types)
          {
            foreach (Type type in types)
              typeSet.Add(type);
          }
        }
      }
      return (ISet<Type>) typeSet;
    }

    private static void ScheduleUpdateContentLocationsServiceTask()
    {
      UpdateContentLocationsTask task = new UpdateContentLocationsTask();
      task.Id = Guid.NewGuid();
      task.NumberOfAttempts = 3;
      SystemManager.ScheduleTask((ScheduledTask) task, "Scheduling task for updating content locations");
    }

    private static void ScheduleSetControlIdToContentLocationsTask()
    {
      SetControlIdToContentLocationsTask task = new SetControlIdToContentLocationsTask();
      task.Id = Guid.NewGuid();
      task.NumberOfAttempts = 3;
      SystemManager.ScheduleTask((ScheduledTask) task, "Scheduling task for setting ControlId to content locations");
    }

    private static void ScheduleCreateMissingDependenciesTask()
    {
      CreateMissingDependenciesTask task = new CreateMissingDependenciesTask();
      task.Id = Guid.NewGuid();
      task.ExecuteTime = DateTime.UtcNow;
      task.NumberOfAttempts = 5;
      SystemManager.ScheduleTask((ScheduledTask) task, "Scheduling task to create dependencies for existing MediaContent's changes.");
    }

    private static void ScheduleTask(ScheduledTask task, string upgradeMsg)
    {
      try
      {
        SchedulingManager manager = SchedulingManager.GetManager();
        manager.AddTask(task);
        manager.SaveChanges();
        Telerik.Sitefinity.Abstractions.Log.Write((object) string.Format("PASSED : {0}", (object) upgradeMsg), ConfigurationPolicy.UpgradeTrace);
      }
      catch (Exception ex)
      {
        Telerik.Sitefinity.Abstractions.Log.Write((object) string.Format("FAILED : {0} - {1}", (object) upgradeMsg, (object) ex.Message), ConfigurationPolicy.UpgradeTrace);
        if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
          throw ex;
      }
    }

    private static void ScheduleFixControlPropertiesTask()
    {
      string str = "Scheduling task for optimizing controls properties";
      try
      {
        SchedulingManager manager = SchedulingManager.GetManager();
        FixControlPropertiesTask task = new FixControlPropertiesTask();
        task.Id = Guid.NewGuid();
        task.NumberOfAttempts = 30;
        manager.AddTask((ScheduledTask) task);
        manager.SaveChanges();
        Telerik.Sitefinity.Abstractions.Log.Write((object) string.Format("PASSED : {0}", (object) str), ConfigurationPolicy.UpgradeTrace);
      }
      catch (Exception ex)
      {
        Telerik.Sitefinity.Abstractions.Log.Write((object) string.Format("FAILED : {0} - {1}", (object) str, (object) ex.Message), ConfigurationPolicy.UpgradeTrace);
        if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
          throw ex;
      }
    }

    internal static void InitializeService(ModuleSettings settings, bool start)
    {
      IService service;
      if (start)
      {
        service = (IService) ObjectFactory.Resolve(TypeResolutionService.ResolveType(settings.Type));
        if (service.Interfaces != null)
        {
          foreach (Type type in service.Interfaces)
            ServiceBus.RegisterService(type, (object) service);
        }
      }
      else
        service = (IService) new InactiveService();
      service.Initialize(settings);
      if (SystemManager.services == null)
        SystemManager.services = new Dictionary<string, IService>();
      SystemManager.services[settings.Name] = service;
      if (!start)
        return;
      service.Start();
    }

    internal static IModule InitializeModule(
      ModuleSettings settings,
      InstallContext installContext,
      SystemManager.ModuleVersionInfo moduleVersion,
      bool? start = null,
      bool skipModuleRegistration = false)
    {
      if (moduleVersion == null)
        moduleVersion = new SystemManager.ModuleVersionInfo();
      if (!start.HasValue)
      {
        if (settings.StartupType == StartupType.Disabled || !string.IsNullOrEmpty(moduleVersion.ErrorMessage) && moduleVersion.ErrorMessage != "Module is not licensed.")
          return (IModule) null;
        start = new bool?(settings.StartupType == StartupType.OnApplicationStart || moduleVersion.Version == (Version) null);
      }
      if (settings.ModuleId != Guid.Empty && !LicenseState.CheckIsModuleLicensedInAnyDomain(settings.ModuleId))
      {
        if (!LicenseState.Current.InvalidLicense && settings.StartupType != StartupType.Disabled)
        {
          SiteInitializer siteInitializer = SystemManager.CreateSiteInitializer(installContext);
          siteInitializer.ModuleName = settings.Name;
          ModuleVersion moduleVersion1 = siteInitializer.MetadataManager.GetModuleVersion(settings.Name) ?? siteInitializer.MetadataManager.CreateModuleVersion(settings.Name);
          if (moduleVersion1.ErrorMessage != "Module is not licensed.")
            moduleVersion1.ErrorMessage = "Module is not licensed.";
          siteInitializer.SaveChanges();
        }
        return (IModule) null;
      }
      using (new MethodPerformanceRegion(settings.Name))
      {
        Version version1 = (Version) null;
        IModule module;
        if (start.Value)
        {
          Type type;
          try
          {
            type = TypeResolutionService.ResolveType(settings.Type, true);
          }
          catch (Exception ex)
          {
            Exception exception = ex;
            if (exception is ReflectionTypeLoadException && ((IEnumerable<Exception>) ((ReflectionTypeLoadException) exception).LoaderExceptions).Count<Exception>() > 0)
              exception = ((ReflectionTypeLoadException) exception).LoaderExceptions[0];
            SystemManager.notLoadedModulesErrors[settings.Name] = exception.Message;
            return (IModule) null;
          }
          module = (IModule) ObjectFactory.Resolve(type);
          version1 = Assembly.GetAssembly(type).GetName().Version;
        }
        else
          module = (IModule) new InactiveModule();
        module.Initialize(settings);
        if (skipModuleRegistration)
          return module;
        if (version1 != (Version) null && (moduleVersion.Version == (Version) null || version1 > moduleVersion.Version))
        {
          SystemManager.EnterUpgrading();
          CultureInfo culture = SystemManager.CurrentContext.Culture;
          SystemManager.CurrentContext.Culture = CultureInfo.InvariantCulture;
          if (installContext == null)
            installContext = new InstallContext(settings.Name);
          SystemManager.InstallingModule = module;
          SiteInitializer siteInitializer = SystemManager.CreateSiteInitializer(installContext);
          siteInitializer.ModuleName = settings.Name;
          ModuleVersion moduleVersion2 = siteInitializer.MetadataManager.GetModuleVersion(settings.Name) ?? siteInitializer.MetadataManager.CreateModuleVersion(settings.Name);
          try
          {
            Version version2 = moduleVersion.Version;
            module.Install(siteInitializer, version2);
            Version version3 = moduleVersion2.Version;
            moduleVersion2.PreviousVersion = version3;
            moduleVersion2.Version = version1;
            moduleVersion2.ErrorMessage = string.Empty;
            siteInitializer.SaveChanges();
            installContext.SaveChanges(false);
            if (module is ModuleBase moduleBase)
              moduleBase.EnsureManagersInitialized();
          }
          catch (Exception ex1)
          {
            siteInitializer.UndoChanges();
            installContext.UndoChanges();
            try
            {
              ModuleVersion moduleVersion3 = siteInitializer.MetadataManager.GetModuleVersion(settings.Name) ?? siteInitializer.MetadataManager.CreateModuleVersion(settings.Name);
              if (!settings.Hidden)
                moduleVersion3.ErrorMessage = ex1.Message;
              siteInitializer.SaveChanges();
              installContext.SaveChanges(false);
            }
            catch (Exception ex2)
            {
              if (Exceptions.HandleException(ex2, ExceptionPolicyName.UnhandledExceptions))
                throw;
            }
            if (!Exceptions.HandleException(ex1, ExceptionPolicyName.IgnoreExceptions))
              return (IModule) null;
            throw;
          }
          finally
          {
            SystemManager.CurrentContext.Culture = culture;
            SystemManager.InstallingModule = (IModule) null;
            siteInitializer.Dispose();
            installContext.SiteInitializer = (SiteInitializer) null;
          }
        }
        SystemManager.appModules[settings.Name] = module;
        return module;
      }
    }

    internal static void UninstallModule(string moduleName)
    {
      AppModuleSettings settings;
      if (!Telerik.Sitefinity.Configuration.Config.Get<SystemConfig>().ApplicationModules.TryGetValue(moduleName, out settings))
        throw new InvalidOperationException(string.Format("Module '{0}' is not found in Settings -> System -> ApplicationModules", (object) moduleName));
      IModule module = SystemManager.GetApplicationModule(moduleName) ?? SystemManager.InitializeModule((ModuleSettings) settings, (InstallContext) null, (SystemManager.ModuleVersionInfo) null, new bool?(true), true);
      CultureInfo culture = SystemManager.CurrentContext.Culture;
      SystemManager.CurrentContext.Culture = CultureInfo.InvariantCulture;
      InstallContext installContext = new InstallContext(moduleName);
      SiteInitializer siteInitializer = SystemManager.CreateSiteInitializer(installContext, moduleName);
      try
      {
        module.Uninstall(siteInitializer);
        ModuleVersion moduleVersion = siteInitializer.MetadataManager.GetModuleVersion(moduleName);
        if (moduleVersion != null)
          siteInitializer.MetadataManager.DeleteModuleVersion(moduleVersion);
        siteInitializer.SaveChanges();
        installContext.SaveChanges(false);
        try
        {
          if (!SystemManager.appModules.ContainsKey(moduleName))
            return;
          lock (SystemManager.appModules)
          {
            if (!SystemManager.appModules.ContainsKey(moduleName))
              return;
            SystemManager.appModules.Remove(moduleName);
          }
        }
        catch
        {
        }
      }
      catch (Exception ex)
      {
        siteInitializer.UndoChanges();
        installContext.UndoChanges();
        if (!Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
          return;
        throw;
      }
      finally
      {
        SystemManager.CurrentContext.Culture = culture;
        siteInitializer.Dispose();
      }
    }

    private static SiteInitializer CreateSiteInitializer(
      InstallContext installContext,
      string moduleName = null)
    {
      SiteInitializer siteInitializer;
      if (moduleName != null)
      {
        siteInitializer = new SiteInitializer(moduleName, installContext);
        siteInitializer.ModuleName = moduleName;
      }
      else
        siteInitializer = new SiteInitializer(installContext);
      siteInitializer.SuppressSecurity();
      installContext.SiteInitializer = siteInitializer;
      return siteInitializer;
    }

    /// <summary>
    /// Initializes a dynamic module created by the ModuleBuilder.
    /// </summary>
    /// <param name="dynamicModule">The dynamic module.</param>
    /// <returns></returns>
    internal static IModule InitializeDynamicModule(
      IDynamicModule dynamicModule,
      IEnumerable<IDynamicModuleType> dynamicModuleTypes)
    {
      foreach (IDynamicModuleType dynamicModuleType in dynamicModuleTypes)
      {
        Type dataItemType = TypeResolutionService.ResolveType(dynamicModuleType.GetFullTypeName(), false);
        if (dataItemType == (Type) null)
          return (IModule) null;
        string areaName = dynamicModule.Title + " - " + dynamicModuleType.DisplayName;
        Telerik.Sitefinity.Modules.ControlTemplates.ControlTemplates.RegisterTemplatableControl(typeof (DynamicContentViewMaster), dataItemType, (string) null, areaName, string.Format("{0} - list", (object) areaName));
        Telerik.Sitefinity.Modules.ControlTemplates.ControlTemplates.RegisterTemplatableControl(typeof (DynamicContentViewDetail), dataItemType, (string) null, areaName, string.Format("{0} - single", (object) areaName));
      }
      DynamicAppModule dynamicAppModule = new DynamicAppModule(dynamicModule, dynamicModuleTypes);
      SystemManager.dynamicModules[dynamicAppModule.Name] = (IModule) dynamicAppModule;
      return (IModule) dynamicAppModule;
    }

    /// <summary>
    /// Removes a dynamic module from the list of initialized modules
    /// </summary>
    /// <param name="name">The module name</param>
    internal static void RemoveDynamicModule(string name) => SystemManager.dynamicModules.Remove(name);

    private static ModuleSettings GetModuleSettingsForEdit(
      SystemConfig systemConfig,
      ModuleSettings readOnlySettings)
    {
      return !(readOnlySettings is ServiceModuleSettings) ? (ModuleSettings) systemConfig.ApplicationModules[readOnlySettings.Name] : (ModuleSettings) systemConfig.ServicesModules[readOnlySettings.Name];
    }

    /// <summary>
    /// Updates the incremental GUID range to the one specified in the DataConfig.
    /// </summary>
    private static void ApplyIncrementalGuidRange()
    {
      int num = (int) OpenAccessConnection.UpdateIncrementalGuidRange(new byte?(Telerik.Sitefinity.Configuration.Config.Get<DataConfig>().IncrementalGuidRange));
    }

    /// <summary>
    /// Gets the HTTP context items from the current request object.
    /// </summary>
    /// <value>The HTTP context items.</value>
    public static IDictionary HttpContextItems => SystemManager.CurrentHttpContext != null ? SystemManager.CurrentHttpContext.Items : (IDictionary) null;

    /// <summary>Sets the design mode.</summary>
    /// <param name="isDesignMode">if set to <c>true</c> the mode is design.</param>
    internal static void SetPageDesignMode(bool isDesignMode)
    {
      if (SystemManager.CurrentHttpContext == null)
        return;
      SystemManager.CurrentHttpContext.Items[(object) "SfPageDesignMode"] = (object) isDesignMode;
    }

    internal static void RaiseModelReset(EventArgs e)
    {
      if (SystemManager.ModelReset == null)
        return;
      SystemManager.ModelReset((object) null, e);
    }

    /// <summary>Sets the preview mode.</summary>
    /// <param name="isPreviewMode">if set to <c>true</c> the mode is preview.</param>
    internal static void SetPagePreviewMode(bool isPreviewMode)
    {
      if (SystemManager.CurrentHttpContext == null)
        return;
      SystemManager.CurrentHttpContext.Items[(object) "SfPagePreviewMode"] = (object) isPreviewMode;
    }

    /// <summary>Sets the inline editing mode.</summary>
    /// <param name="isPreviewMode">if set to <c>true</c> the mode is preview.</param>
    internal static void SetInlineEditingMode(bool isInlineEditingMode)
    {
      if (SystemManager.CurrentHttpContext == null)
        return;
      SystemManager.CurrentHttpContext.Items[(object) "IsInlineEditingMode"] = (object) isInlineEditingMode;
      if (!isInlineEditingMode)
        return;
      SystemManager.CurrentHttpContext.Items[(object) "sf-lc-status"] = (object) ContentLifecycleStatus.Temp.ToString();
      SystemManager.CurrentHttpContext.Items[(object) "sfContentFilters"] = (object) new string[1]
      {
        "DraftLinksParser"
      };
    }

    /// <summary>
    /// Gets a value indicating whether the current request is for page in design mode.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is page in design mode; otherwise, <c>false</c>.
    /// </value>
    public static bool IsDesignMode
    {
      get
      {
        if (SystemManager.CurrentHttpContext != null)
        {
          object isDesignMode = SystemManager.CurrentHttpContext.Items[(object) "SfPageDesignMode"];
          if (isDesignMode != null)
            return (bool) isDesignMode;
        }
        return false;
      }
    }

    /// <summary>
    /// Gets a value indicating whether the current request is for page in design mode.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is page in design mode; otherwise, <c>false</c>.
    /// </value>
    public static bool IsShieldEnabled
    {
      get
      {
        if (!SystemManager.IsModuleEnabled("ProtectionShield"))
          return false;
        ProtectionShieldConfig protectionShieldConfig = Telerik.Sitefinity.Configuration.Config.Get<ProtectionShieldConfig>();
        bool isShieldEnabled;
        if (protectionShieldConfig.EnabledForAllSites)
        {
          isShieldEnabled = true;
        }
        else
        {
          string name = SystemManager.CurrentContext.MultisiteContext.CurrentSiteContext.Site.Name;
          isShieldEnabled = protectionShieldConfig.EnabledForSites.ContainsKey(name);
        }
        return isShieldEnabled;
      }
    }

    public static bool IsShieldEnabledForSite(Guid siteId)
    {
      if (SystemManager.IsModuleEnabled("ProtectionShield") && siteId != Guid.Empty)
      {
        ProtectionShieldConfig protectionShieldConfig = Telerik.Sitefinity.Configuration.Config.Get<ProtectionShieldConfig>();
        if (protectionShieldConfig.EnabledForAllSites)
          return true;
        IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
        Telerik.Sitefinity.Multisite.ISite site = multisiteContext == null ? SystemManager.CurrentContext.CurrentSite : multisiteContext.GetSiteById(siteId);
        if (site != null)
        {
          string name = site.Name;
          return protectionShieldConfig.EnabledForSites.ContainsKey(name);
        }
      }
      return false;
    }

    public static bool IsSiteOffline(Guid siteId)
    {
      IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
      Telerik.Sitefinity.Multisite.ISite site = multisiteContext == null ? SystemManager.CurrentContext.CurrentSite : multisiteContext.GetSiteById(siteId);
      return site != null && site.IsOffline;
    }

    /// <summary>
    /// Gets a value indicating whether the current request is for page in preview mode.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is page in preview mode; otherwise, <c>false</c>.
    /// </value>
    public static bool IsPreviewMode
    {
      get
      {
        if (SystemManager.CurrentHttpContext != null)
        {
          object isPreviewMode = SystemManager.CurrentHttpContext.Items[(object) "SfPagePreviewMode"];
          if (isPreviewMode != null)
            return (bool) isPreviewMode;
        }
        return false;
      }
    }

    /// <summary>
    /// Determines whether authentication requirement is suppressed for the request.
    /// </summary>
    /// <remarks>Example for request for which authentication is suppress is request to ~/sitefinity/public/ and ~/sitefinity/frontend/.</remarks>
    /// <param name="httpContext">The HTTP context.</param>
    /// <returns><c>True</c> if authentication required is suppressed for the request and <c>false</c> otherwise.</returns>
    internal static bool IsAuthenticationRequirementSuppressed(HttpContextBase httpContext)
    {
      object obj = httpContext.Items[(object) "SuppressAuthenticationRequirement"];
      return obj != null && (bool) obj;
    }

    /// <summary>
    /// Determines whether the request should be served in a backend context.
    /// (E.g. /Sitefinity/... or /frontendPage/Action/Preview etc.)
    /// </summary>
    /// <returns>True if the request is for Sitefinity's backend otherwise - false.</returns>
    internal static bool IsBackendRequest() => SystemManager.IsBackendRequest(out CultureInfo _);

    /// <summary>
    /// Determines whether the request should be served in a backend context.
    /// (E.g. /Sitefinity/... or /frontendPage/Action/Preview etc.)
    /// </summary>
    /// <param name="culture">Returns the current culture if it could be determined by the url while in action mode (e.g. action/preview/bg).</param>
    /// <returns>True if the request is for Sitefinity's backend otherwise - false.</returns>
    internal static bool IsBackendRequest(out CultureInfo culture)
    {
      culture = (CultureInfo) null;
      HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
      if (currentHttpContext == null)
        return false;
      object obj = currentHttpContext.Items[(object) nameof (IsBackendRequest)];
      culture = currentHttpContext.Items[(object) "sfCurrentBackendCulture"] as CultureInfo;
      if (obj == null)
      {
        if (!Bootstrapper.IsReady)
        {
          obj = (object) true;
        }
        else
        {
          Uri uriToTest = currentHttpContext.Request.Url;
          if (uriToTest.Segments.Length != 0)
          {
            string str = ((IEnumerable<string>) uriToTest.Segments).FirstOrDefault<string>((Func<string, bool>) (s => VirtualPathUtility.RemoveTrailingSlash(s).EndsWith(".svc", StringComparison.OrdinalIgnoreCase) || "RestApi".Equals(VirtualPathUtility.RemoveTrailingSlash(s), StringComparison.OrdinalIgnoreCase) || "Telerik.Sitefinity.Frontend".Equals(VirtualPathUtility.RemoveTrailingSlash(s), StringComparison.OrdinalIgnoreCase) || "Res".Equals(VirtualPathUtility.RemoveTrailingSlash(s), StringComparison.OrdinalIgnoreCase) || "Telerik.Sitefinity.Html5UploadHandler.ashx".Equals(VirtualPathUtility.RemoveTrailingSlash(s), StringComparison.OrdinalIgnoreCase) || "template-thumbnails".Equals(VirtualPathUtility.RemoveTrailingSlash(s), StringComparison.OrdinalIgnoreCase) || "sitefinityRest".Equals(VirtualPathUtility.RemoveTrailingSlash(s), StringComparison.OrdinalIgnoreCase)));
            if (string.IsNullOrEmpty(str) && VirtualPathUtility.RemoveTrailingSlash(uriToTest.AbsolutePath).EndsWith("Sitefinity/SignOut", StringComparison.OrdinalIgnoreCase))
              str = uriToTest.AbsolutePath;
            if (!string.IsNullOrEmpty(str))
            {
              try
              {
                if (currentHttpContext.Request.UrlReferrer != (Uri) null)
                  uriToTest = currentHttpContext.Request.UrlReferrer;
              }
              catch
              {
              }
            }
          }
          CultureInfo culture1;
          obj = (object) SystemManager.IsBackendUri(currentHttpContext, uriToTest, out culture1);
          if (culture1 != null)
            culture = culture1;
          if (obj == null)
          {
            obj = (object) false;
            if (SystemManager.CurrentHttpContext.Request.Headers[nameof (IsBackendRequest)] != null)
            {
              try
              {
                obj = SystemManager.CurrentHttpContext.Request.Headers[nameof (IsBackendRequest)].To(typeof (bool));
              }
              catch
              {
              }
            }
          }
        }
        currentHttpContext.Items[(object) nameof (IsBackendRequest)] = obj;
        currentHttpContext.Items[(object) "sfCurrentBackendCulture"] = (object) culture;
      }
      return (bool) obj;
    }

    /// <summary>
    /// Explicitly set whether the current request is a backend, or not.
    /// </summary>
    internal static bool TrySetBackendRequest()
    {
      HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
      if (currentHttpContext != null && (!currentHttpContext.Items.Contains((object) "IsBackendRequest") || !(bool) currentHttpContext.Items[(object) "IsBackendRequest"]))
      {
        SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
        if (currentIdentity != null && currentIdentity.IsAuthenticated && currentIdentity.IsBackendUser)
        {
          currentHttpContext.Items[(object) "IsBackendRequest"] = (object) true;
          SystemManager.CurrentContext.MultisiteContext.CurrentSiteContext = (ISiteContext) null;
          return true;
        }
      }
      return false;
    }

    /// <summary>
    /// Determines whether the request should be served in a backend context.
    /// (E.g. /Sitefinity/... or /frontendPage/Action/Preview etc.)
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="uriToTest">The URI to test.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>True if the request is for Sitefinity's backend otherwise - false.</returns>
    internal static bool? IsBackendUri(
      HttpContextBase context,
      Uri uriToTest,
      out CultureInfo culture)
    {
      culture = (CultureInfo) null;
      bool? nullable = new bool?();
      int length = uriToTest.Segments.Length;
      int index1 = 1;
      if (context.Request.ApplicationPath != "/")
        index1 = ((IEnumerable<string>) context.Request.ApplicationPath.Split('/')).Count<string>();
      if (length > index1)
      {
        if (VirtualPathUtility.RemoveTrailingSlash(((IEnumerable<string>) uriToTest.Segments).ElementAt<string>(index1)).Equals("Sitefinity", StringComparison.OrdinalIgnoreCase))
        {
          nullable = new bool?(true);
        }
        else
        {
          for (int index2 = 0; index2 < length; ++index2)
          {
            if (VirtualPathUtility.RemoveTrailingSlash(uriToTest.Segments[index2]).Equals("Action", StringComparison.OrdinalIgnoreCase) && index2 + 1 < length)
            {
              string str = VirtualPathUtility.RemoveTrailingSlash(uriToTest.Segments[index2 + 1]);
              if (str.Equals("Edit", StringComparison.OrdinalIgnoreCase) || str.Equals("Preview", StringComparison.OrdinalIgnoreCase) || str.Equals("MobilePreview", StringComparison.OrdinalIgnoreCase))
              {
                nullable = new bool?(true);
                if (index2 + 2 < length)
                {
                  string name = VirtualPathUtility.RemoveTrailingSlash(uriToTest.Segments[index2 + 2]);
                  try
                  {
                    culture = CultureInfo.GetCultureInfo(name);
                    break;
                  }
                  catch (Exception ex)
                  {
                    break;
                  }
                }
                else
                  break;
              }
            }
          }
        }
      }
      return nullable;
    }

    internal static bool IsDetailsView() => SystemManager.IsDetailsView(out object _);

    internal static bool IsDetailsView(out object item)
    {
      item = SystemManager.CurrentHttpContext.Items[(object) "detailItem"];
      return item != null;
    }

    /// <summary>Fire ApplicationStart event</summary>
    internal static void FireApplicationStart()
    {
      if (SystemManager.ApplicationStart == null)
        return;
      SystemManager.ApplicationStart((object) null, EventArgs.Empty);
    }

    /// <summary>
    /// Specifies fallback mode valid for the current request only
    /// Highest priority has the Fallback mode attribute, then the request one
    /// and the is the default behavior
    /// </summary>
    public static FallbackMode RequestLanguageFallbackMode
    {
      get
      {
        if (SystemManager.CurrentHttpContext != null)
        {
          object languageFallbackMode = SystemManager.CurrentHttpContext.Items[(object) "RequestFallback"];
          if (languageFallbackMode != null)
            return (FallbackMode) languageFallbackMode;
        }
        return FallbackMode.SystemDefault;
      }
      set
      {
        if (SystemManager.CurrentHttpContext == null)
          return;
        SystemManager.CurrentHttpContext.Items[(object) "RequestFallback"] = (object) value;
      }
    }

    /// <summary>Returns if the application is starting</summary>
    [Obsolete("Use Bootstrapper.IsReady")]
    public static bool IsStarting => !Bootstrapper.IsReady;

    /// <summary>
    /// Determines whether Sitefinity is running in a load balancing mode.
    /// </summary>
    public static bool IsInLoadBalancingMode
    {
      get
      {
        LoadBalancingConfig loadBalancingConfig = Telerik.Sitefinity.Configuration.Config.Get<SystemConfig>().LoadBalancingConfig;
        return loadBalancingConfig.URLS.Count > 0 || AzureRuntime.IsRunning && AzureRuntime.InstanceCount > 1 || !string.IsNullOrEmpty(loadBalancingConfig.RedisSettings.ConnectionString) || loadBalancingConfig.ReplicationSyncSettings.Enabled;
      }
    }

    public static void ClearCurrentTransactions() => SystemManager.CurrentTransactions?.Dispose();

    internal static ContextTransactions CurrentTransactions
    {
      get
      {
        IDictionary httpContextItems = SystemManager.HttpContextItems;
        if (httpContextItems == null)
          return (ContextTransactions) null;
        if (!(httpContextItems[(object) "SfTransactions"] is ContextTransactions currentTransactions))
        {
          currentTransactions = new ContextTransactions();
          httpContextItems[(object) "SfTransactions"] = (object) currentTransactions;
        }
        return currentTransactions;
      }
      set
      {
        IDictionary httpContextItems = SystemManager.HttpContextItems;
        if (httpContextItems == null)
          return;
        httpContextItems[(object) "SfTransactions"] = (object) value;
      }
    }

    /// <summary>
    /// Gets or sets the current <see cref="T:System.Web.HttpContextBase" /> instance.
    /// </summary>
    /// <remarks>
    /// It is recommended to use this property instead of HttpContext.Current when writing new code. The advantage of this approach is that
    /// this method returns an instance of type <see cref="T:System.Web.HttpContextBase" /> which allows you to write unit testable
    /// code. The original <see cref="T:System.Web.HttpContext" /> type has no base class and isn't virtual, and hence is unusable for testing
    /// because you can't mock it. When calling this property runtime an instance of <see cref="T:System.Web.HttpContextWrapper" />
    /// will be returned that inherits <see cref="T:System.Web.HttpContextBase" /> and wraps the HttpContext.Current object.
    /// 
    /// Getting <see cref="T:System.Web.HttpContext" /> from <see cref="T:System.Web.HttpContextBase" />'s instance is easy:
    /// HttpContext httpContext = httpContextBase.ApplicationInstance.Context;
    /// 
    ///  To get <see cref="T:System.Web.HttpContextBase" /> from <see cref="T:System.Web.HttpContext" /> we have to wrap it in <see cref="T:System.Web.HttpContextWrapper" />:
    /// HttpContextBase httpContextBaseInstance = new HttpContextWrapper(httpContextInstace);
    /// </remarks>
    /// <value>The current HTTP context.</value>
    public static HttpContextBase CurrentHttpContext
    {
      get
      {
        if (SystemManager.httpContextMock != null)
          return SystemManager.httpContextMock;
        return HttpContext.Current != null ? (HttpContextBase) new HttpContextWrapper(HttpContext.Current) : (HttpContextBase) null;
      }
      set
      {
        if (value == null || value.ApplicationInstance == null)
          HttpContext.Current = (HttpContext) null;
        else
          HttpContext.Current = value.ApplicationInstance.Context;
      }
    }

    /// <summary>
    /// Gets or sets the current <see cref="T:System.Web.HttpContextBase" /> mock. The mocked context is stored per thread and application domain.
    /// Use this property to set a mocked context in your unit tests.
    /// </summary>
    /// <remarks>If this property is set the SystemManager.CurrentHttpContext getter will always return the mocked context instead
    /// of the actual <see cref="T:System.Web.HttpContext" /> instance from HttpContext.Current (present or not).
    /// Since the value of the mocked HttpContext could be preserved between tests it is best to use the method <see cref="M:Telerik.Sitefinity.Services.SystemManager.RunWithHttpContext(System.Web.HttpContextBase,System.Action)" />
    /// </remarks>
    /// <value>
    /// The current <see cref="T:System.Web.HttpContextBase" /> mock.
    /// </value>
    private static HttpContextBase CurrentHttpContextMock
    {
      get => SystemManager.httpContextMock;
      set => SystemManager.httpContextMock = value;
    }

    /// <summary>
    /// Executes the specified delegate with replaced SystemManager.CurrentHttpContext instance.
    /// </summary>
    /// <param name="contextToRunWith">The <see cref="T:System.Web.HttpContextBase" /> context to run with.</param>
    /// <param name="delegateToExecute">The delegate to execute.</param>
    /// <seealso cref="P:Telerik.Sitefinity.Services.SystemManager.CurrentHttpContextMock" />
    public static void RunWithHttpContext(
      HttpContextBase contextToRunWith,
      System.Action delegateToExecute)
    {
      HttpContextBase currentHttpContextMock = SystemManager.CurrentHttpContextMock;
      SystemManager.CurrentHttpContextMock = contextToRunWith;
      try
      {
        delegateToExecute();
      }
      finally
      {
        SystemManager.CurrentHttpContextMock = currentHttpContextMock;
      }
    }

    /// <summary>
    /// The <c>Host</c> component of the site, if any. It is determined by the first request
    /// and also can be explicitly specified in the <see cref="T:Telerik.Sitefinity.Services.SystemConfig" />.
    /// </summary>
    public static string Host
    {
      get
      {
        string host = string.Empty;
        if (Telerik.Sitefinity.Configuration.Config.Get<SystemConfig>().SiteUrlSettings.EnableNonDefaultSiteUrlSettings)
          host = SystemManager.CurrentContext.CurrentSite.GetUri().Host;
        if (string.IsNullOrEmpty(host))
          host = SystemManager.CurrentHttpContext == null ? (string) null : (SystemManager.CurrentHttpContext.Request == null || !(SystemManager.CurrentHttpContext.Request.Url != (Uri) null) ? SystemManager.CurrentContext.CurrentSite.GetUri().Host : SystemManager.CurrentHttpContext.Request.Url.Host);
        return host;
      }
    }

    /// <summary>Gets the site URI.</summary>
    /// <returns>The site URI.</returns>
    internal static Uri GetSiteUri(bool skipHostResolvingFromCurrentRequest = false)
    {
      Uri result1 = (Uri) null;
      HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
      if (currentHttpContext != null && !skipHostResolvingFromCurrentRequest)
      {
        HttpRequestBase request = currentHttpContext.Request;
        Uri url = request.Url;
        if (url != (Uri) null && (request.IsLocal || SystemManager.IsDomainTrusted(url)))
        {
          Uri uri = SystemManager.CurrentContext.CurrentSite.GetUri();
          if (url.AbsoluteUri.StartsWith(uri.AbsoluteUri, StringComparison.InvariantCultureIgnoreCase))
            result1 = uri;
          else
            Uri.TryCreate(url.GetLeftPart(UriPartial.Authority), UriKind.Absolute, out result1);
        }
      }
      if (result1 == (Uri) null)
      {
        result1 = SystemManager.CurrentContext.CurrentSite.GetUri();
        if (result1 == (Uri) null)
          result1 = SystemManager.CurrentContext.DefaultSite.GetUri();
      }
      if (result1 == (Uri) null && !string.IsNullOrEmpty(SystemManager.AbsolutePathRootUrlOfFirstRequest))
      {
        Uri result2 = (Uri) null;
        if (Uri.TryCreate(SystemManager.AbsolutePathRootUrlOfFirstRequest, UriKind.Absolute, out result2))
          Uri.TryCreate(result2.GetLeftPart(UriPartial.Authority), UriKind.Absolute, out result1);
      }
      if (result1 == (Uri) null && currentHttpContext != null)
      {
        Uri.TryCreate(currentHttpContext.Request.Url.GetLeftPart(UriPartial.Authority), UriKind.Absolute, out result1);
        result1 = currentHttpContext.Request.Url;
      }
      return result1;
    }

    /// <summary>
    /// Compares the given uri's host with lists of trusted domains (defined in license, site domain aliases, configured site domains) and returns true if there is a match.
    /// </summary>
    /// <param name="requestUri">The uri that will be checked.</param>
    /// <returns>True the domain is: 1. Licensed OR 2. Explicitly defined in site's domain aliases OR 3. White-listed in applicaiton configuration.</returns>
    internal static bool IsDomainTrusted(Uri requestUri)
    {
      if (requestUri == (Uri) null)
        return false;
      bool flag = false;
      if (!LicenseState.Current.LicenseInfo.SkipDomainValidation && LicenseState.CheckDomainIsLicensed(requestUri.Host, requestUri.HostNameType) || LicenseState.CheckDomain(requestUri.Host, (IEnumerable<string>) SystemManager.CurrentContext.CurrentSite.DomainAliases, LicenseState.Current.LicenseInfo.AllowSubDomains, requestUri.HostNameType) || LicenseState.CheckDomain(requestUri.Host, (IEnumerable<string>) SystemManager.ConfiguredSiteDomains, LicenseState.Current.LicenseInfo.AllowSubDomains, requestUri.HostNameType))
        flag = true;
      return flag;
    }

    private static string[] ConfiguredSiteDomains
    {
      get
      {
        if (SystemManager.siteDomains == null)
        {
          string str = ConfigurationManager.AppSettings.Get("sf:siteDomains");
          if (!string.IsNullOrEmpty(str))
          {
            try
            {
              SystemManager.siteDomains = str.Split(',');
            }
            catch (Exception ex)
            {
              Telerik.Sitefinity.Abstractions.Log.Write((object) string.Format("Failed to parse 'sf:siteDomains' key. Exception: {0}", (object) ex.Message), TraceEventType.Error);
              SystemManager.siteDomains = new string[0];
            }
          }
          else
            SystemManager.siteDomains = new string[0];
        }
        return SystemManager.siteDomains;
      }
    }

    /// <summary>
    /// Specifies if the page is rendered inmemory - for script combining
    /// </summary>
    public static bool RenderInMemory
    {
      get
      {
        if (SystemManager.CurrentHttpContext != null)
        {
          object renderInMemory = SystemManager.CurrentHttpContext.Items[(object) nameof (RenderInMemory)];
          if (renderInMemory != null)
            return (bool) renderInMemory;
        }
        return false;
      }
      set
      {
        if (SystemManager.CurrentHttpContext == null)
          return;
        SystemManager.CurrentHttpContext.Items[(object) nameof (RenderInMemory)] = (object) value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the current request is in Browse And Edit mode.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is in Browse And Edit mode; otherwise, <c>false</c>.
    /// </value>
    [Obsolete("Use SystemManager.IsInlineEditingMode instead.")]
    public static bool IsBrowseAndEditMode
    {
      get => SystemManager.IsInlineEditingMode;
      set => SystemManager.SetInlineEditingMode(value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the current request is in Inline editing mode.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is in Inline editing mode; otherwise, <c>false</c>.
    /// </value>
    public static bool IsInlineEditingMode
    {
      get
      {
        if (SystemManager.CurrentHttpContext != null)
        {
          object inlineEditingMode = SystemManager.CurrentHttpContext.Items[(object) nameof (IsInlineEditingMode)];
          if (inlineEditingMode != null)
            return (bool) inlineEditingMode;
        }
        return false;
      }
      set => SystemManager.SetInlineEditingMode(value);
    }

    /// <summary>
    /// When set to <c>true</c> for the current request, all <see cref="T:Telerik.Sitefinity.Data.ITracker" /> implementers should do nothing when <see cref="M:Telerik.Sitefinity.Data.ITracker.Track(System.Object,Telerik.Sitefinity.Data.DataProviderBase)" /> is called.
    /// </summary>
    internal static bool DataTrackingDisabled
    {
      get
      {
        if (SystemManager.CurrentHttpContext == null)
          return false;
        object obj = SystemManager.CurrentHttpContext.Items[(object) nameof (DataTrackingDisabled)];
        return obj != null && (bool) obj;
      }
      set
      {
        if (SystemManager.CurrentContext == null)
          return;
        SystemManager.CurrentHttpContext.Items[(object) nameof (DataTrackingDisabled)] = (object) value;
      }
    }

    internal static void LockUpgrade()
    {
      if (SystemManager.region == null)
        SystemManager.region = new LockRegion<PackagingManager>("Upgrading", TimeSpan.FromMinutes(10.0));
      SystemManager.region.WaitToAcquire();
    }

    internal static void UnlockUpgrade()
    {
      if (SystemManager.region == null)
        return;
      SystemManager.region.Dispose();
      SystemManager.region = (LockRegion<PackagingManager>) null;
    }

    private static void EnterUpgrading()
    {
      if (SystemManager.isUpgrading)
        return;
      SystemManager.LockUpgrade();
      if (SystemManager.isUpgrading)
        return;
      SystemManager.isUpgrading = true;
      Telerik.Sitefinity.Abstractions.Log.Write((object) "Enter upgrade mode");
      OperationReason reason = new OperationReason("EnterUpgrade");
      reason.AddInfo("FullClean");
      OpenAccessConnection.CleanAll(reason, false);
    }

    private static void ExitUpgrading()
    {
      if (!SystemManager.isUpgrading)
        return;
      SystemManager.UnlockUpgrade();
      SystemManager.isUpgrading = false;
      Telerik.Sitefinity.Abstractions.Log.Write((object) "Exit upgrade mode");
      OperationReason reason = new OperationReason("ExitUpgrade");
      reason.AddInfo("FullClean");
      OpenAccessConnection.CleanAll(reason, false);
    }

    internal static bool IsUpgrading => SystemManager.isUpgrading;

    internal static Version UpgradeFromVersion => SystemManager.upgradeFromVersion;

    internal static bool IsFrontEndControlRender
    {
      get
      {
        if (SystemManager.CurrentHttpContext == null)
          return false;
        object obj = SystemManager.CurrentHttpContext.Items[(object) "sfFrontEndControlRender"];
        return obj != null && (bool) obj;
      }
      set
      {
        if (SystemManager.CurrentContext == null)
          return;
        SystemManager.CurrentHttpContext.Items[(object) "sfFrontEndControlRender"] = (object) value;
      }
    }

    [Obsolete("Use SystemManager.CurrentContext.MultisiteContext instead.")]
    internal static IMultisiteContext MultisiteContext => SystemManager.CurrentContext as IMultisiteContext;

    public static SitefinityContextBase CurrentContext
    {
      get
      {
        if (SystemManager.context == null)
          SystemManager.context = (SitefinityContextBase) new SingleSiteContext();
        return SystemManager.context;
      }
      internal set => SystemManager.context = value;
    }

    /// <summary>
    /// Gets the data source registry, which is used for managing all data sources inside the system
    /// </summary>
    internal static IDataSourceRegistry DataSourceRegistry
    {
      get
      {
        if (SystemManager.dataSourceRegistry == null)
          SystemManager.dataSourceRegistry = ObjectFactory.Resolve<IDataSourceRegistry>();
        return SystemManager.dataSourceRegistry;
      }
    }

    /// <summary>Gets the multisite enabled managers.</summary>
    internal static ICollection<Type> MultisiteEnabledManagers => (ICollection<Type>) SystemManager.multisiteEnabledManagers;

    /// <summary>
    /// Gets the background task service that can execute tasks asynchronously in parallel
    /// </summary>
    public static IBackgroundTasksService BackgroundTasksService => ObjectFactory.Resolve<IBackgroundTasksService>();

    internal static bool DelayedDatabaseInit
    {
      get => SystemManager.delayedDataBaseInit;
      set => SystemManager.delayedDataBaseInit = value;
    }

    internal static void AddDefaultDataInitializer(System.Action initializer)
    {
      lock (SystemManager.installDefaultsDelegates)
        SystemManager.installDefaultsDelegates.Add(initializer);
    }

    internal static void FlushDefaultDataInitializers(bool execute = true)
    {
      lock (SystemManager.installDefaultsDelegates)
      {
        if (execute)
        {
          using (new MethodPerformanceRegion("Execute {0} default data initializers".Arrange((object) SystemManager.installDefaultsDelegates.Count)))
          {
            foreach (System.Action defaultsDelegate in SystemManager.installDefaultsDelegates)
              defaultsDelegate();
          }
        }
        SystemManager.installDefaultsDelegates.Clear();
      }
    }

    internal static IDictionary<string, string> NotLoadedModulesErrors => (IDictionary<string, string>) SystemManager.notLoadedModulesErrors;

    internal static int? ServiceTokenLifetime
    {
      get => SystemManager.serviceTokenLifetime;
      set => SystemManager.serviceTokenLifetime = value;
    }

    internal static bool IsDBPMode
    {
      get => SystemManager.isDBPMode;
      set => SystemManager.isDBPMode = value;
    }

    /// <summary>Gets the notification service able to send messages.</summary>
    /// <returns></returns>
    public static INotificationService GetNotificationService() => ServiceBus.ResolveService<INotificationService>();

    /// <summary>
    /// Gets the statistics service which is able to write arbitrary sentences.
    /// </summary>
    /// <returns>
    /// An instance of the <see cref="T:Telerik.Sitefinity.Services.Statistics.IStatisticsService" />.
    /// </returns>
    public static IStatisticsService GetStatisticsService() => ServiceBus.ResolveService<IStatisticsService>();

    /// <summary>
    /// Gets the personalization service which is provides functionality for
    /// personalizing Sitefinity.
    /// </summary>
    /// <returns>
    /// An instance of the <see cref="T:Telerik.Sitefinity.Personalization.IPersonalizationService" />.
    /// </returns>
    public static IPersonalizationService GetPersonalizationService() => ServiceBus.ResolveService<IPersonalizationService>();

    /// <summary>
    /// Gets the geo-location service which provides functionality for
    /// working with spatial data
    /// </summary>
    /// <returns>An instance of the <see cref="!:IGeoLocationService" /></returns>
    public static IGeoLocationService GetGeoLocationService() => ServiceBus.ResolveService<IGeoLocationService>();

    /// <summary>
    /// Gets the content locations service which is provides functionality for
    /// canonical URLs in Sitefinity.
    /// </summary>
    /// <returns>
    /// An instance of the <see cref="T:Telerik.Sitefinity.ContentLocations.IContentLocationService" />.
    /// </returns>
    public static IContentLocationService GetContentLocationService() => (IContentLocationService) SystemManager.GetContentLocationServiceInternal();

    /// <summary>Gets the comments service.</summary>
    public static ICommentService GetCommentsService() => ServiceBus.ResolveService<ICommentService>();

    /// <summary>Gets the Recycle Bin service.</summary>
    /// <returns>The configured instance of <see cref="T:Telerik.Sitefinity.Services.RecycleBin.IRecycleBinService" /></returns>
    public static IRecycleBinService GetRecycleBinService() => ServiceBus.ResolveService<IRecycleBinService>();

    internal static ContentLocationService GetContentLocationServiceInternal() => ServiceBus.ResolveService<ContentLocationService>();

    internal static void SetNoCache(HttpCachePolicy cache)
    {
      cache.SetCacheability(HttpCacheability.NoCache);
      cache.SetNoStore();
      cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
    }

    /// <summary>Occurs when Sitefinity system is shutting down.</summary>
    public static event System.EventHandler<CancelEventArgs> ShuttingDown;

    /// <summary>Occurs when Sitefinity system is shutdown.</summary>
    public static event System.EventHandler<EventArgs> Shutdown;

    /// <summary>
    /// Occurs when Sitefinity updates the model during the runtime.
    /// </summary>
    public static event System.EventHandler<EventArgs> ModelReset;

    /// <summary>
    /// Occurs when Sitefinity application has been initialized.
    /// </summary>
    [Obsolete("Use Bootstrapper.Bootstrapped")]
    public static event System.EventHandler<EventArgs> ApplicationStart;

    /// <summary>
    /// Internal event fired after initialization of all Sitefinity modules
    /// </summary>
    internal static event System.EventHandler<SystemInitializationEventArgs> ModulesInitialized;

    internal static event Func<EventArgs, IEnumerable<Type>> ManagerTypesRegistered;

    internal enum RestartOperationKind
    {
      ApplicationRestart,
      ModelReset,
    }

    public delegate void RunWithElevatedPrivilegeDelegate(object[] parameters);

    internal class ModuleVersionInfo
    {
      public ModuleVersionInfo()
      {
      }

      public ModuleVersionInfo(ModuleVersion moduleVersion)
      {
        this.Version = moduleVersion.Version;
        this.ErrorMessage = moduleVersion.ErrorMessage;
      }

      public Version Version { get; private set; }

      public string ErrorMessage { get; private set; }
    }
  }
}
