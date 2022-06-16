// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.Bootstrapper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using ServiceStack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web.Hosting;
using System.Web.Routing;
using Telerik.Sitefinity.Abstractions.VirtualPath;
using Telerik.Sitefinity.AppStatus;
using Telerik.Sitefinity.Cloud.WindowsAzure;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Configuration.Web.Services;
using Telerik.Sitefinity.Configuration.Web.Services.CacheProfiles;
using Telerik.Sitefinity.ContentLocations.Web;
using Telerik.Sitefinity.ContentLocations.Web.Services;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GeoLocations.Web.Services;
using Telerik.Sitefinity.Health;
using Telerik.Sitefinity.HealthMonitoring;
using Telerik.Sitefinity.Licensing;
using Telerik.Sitefinity.LoadBalancing;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Restriction;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Captcha;
using Telerik.Sitefinity.Services.Data;
using Telerik.Sitefinity.Services.GenericData;
using Telerik.Sitefinity.Services.InlineEditing;
using Telerik.Sitefinity.Services.RelatedData;
using Telerik.Sitefinity.Services.ServiceStack;
using Telerik.Sitefinity.Services.UserSession;
using Telerik.Sitefinity.Services.Web.Services;
using Telerik.Sitefinity.SiteSettings.Web.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Compilation.Services;
using Telerik.Sitefinity.Web.OutputCache.Services;
using Telerik.Sitefinity.Web.UI.Backend;

namespace Telerik.Sitefinity.Abstractions
{
  /// <summary>Initializes application services.</summary>
  public static class Bootstrapper
  {
    internal const string ContextualHelpRouteUrl = "contextual-help";
    internal const string BootstrapOperationKey = "Bootstrap";
    internal const string BootstrapStartKey = "BootstrapStart";
    internal const string WarmupCodeHeaderName = "Warmup-Code";
    private static bool firstBoot = true;
    private static volatile bool isDataInitialized = false;
    private static volatile bool hasStarted = false;
    private static volatile bool finalEventsExecuted = false;
    private static readonly object syncLock = new object();
    private static volatile bool isSystemInitialized = false;
    private static Dictionary<string, RouteRegistration> permanentRoutes = new Dictionary<string, RouteRegistration>();
    private static string currentWarmupCode = string.Empty;
    private static readonly string integrationTestsRouteKey = "integration-tests";
    private static readonly string uiTestsRouteKey = "ui-tests";
    private const string ContextualHelpRouteName = "ContextualHelp";

    /// <summary>Restarts all services.</summary>
    public static void Restart()
    {
      Bootstrapper.Stop();
      Bootstrapper.Bootstrap();
    }

    internal static void Stop()
    {
      if (!Bootstrapper.hasStarted)
        return;
      lock (Bootstrapper.syncLock)
      {
        if (!Bootstrapper.hasStarted)
          return;
        Bootstrapper.hasStarted = false;
        Bootstrapper.finalEventsExecuted = false;
      }
    }

    /// <summary>Starts all services.</summary>
    public static bool Bootstrap()
    {
      long startTime = Telerik.Sitefinity.HealthMonitoring.PerformanceMonitor.Begin();
      try
      {
        if (!Bootstrapper.hasStarted)
        {
          lock (Bootstrapper.syncLock)
          {
            if (!Bootstrapper.hasStarted)
            {
              using (new MethodPerformanceRegion(nameof (Bootstrap)))
              {
                CultureInfo currentUiCulture = CultureInfo.CurrentUICulture;
                Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;
                SystemManager.InitAppStatusBuffer();
                SystemManager.AppStatusBuffer.TryAdd(AppStatusTraceListener.GetEntry((object) "System is initializing..."));
                using (new MethodPerformanceRegion("Bootstrapping"))
                {
                  Bootstrapper.OnInitializing((object) null, new ExecutingEventArgs("Bootstrapping", (object) null));
                  Bootstrapper.OnBootstrapping((object) null, EventArgs.Empty);
                }
                if (Bootstrapper.firstBoot && AzureRuntime.IsRunning)
                {
                  AzureRuntime.RevealHttpRequestResponse();
                  AzureDiagnostics.Initialize(false);
                }
                using (new MethodPerformanceRegion("RegisterIoCTypes"))
                  ObjectFactory.RegisterIoCTypes();
                AppStatusTraceListener listener = SystemManager.RegisterAppStatusTraceListener();
                try
                {
                  if (Bootstrapper.firstBoot)
                    HostingEnvironment.RegisterVirtualPathProvider((VirtualPathProvider) new SitefinityVirtualPathProvider());
                  bool flag;
                  using (new MethodPerformanceRegion("RegisterRoutes"))
                  {
                    flag = Bootstrapper.RegisterRoutes();
                    Bootstrapper.firstBoot = false;
                  }
                  if (!Bootstrapper.IsSystemInitialized)
                  {
                    Bootstrapper.hasStarted = true;
                    Bootstrapper.finalEventsExecuted = true;
                    return true;
                  }
                  if (flag)
                  {
                    using (new MethodPerformanceRegion("CleanApplication"))
                      SystemManager.CleanApplication();
                    Bootstrapper.Bootstrap();
                    return false;
                  }
                  Bootstrapper.hasStarted = true;
                  using (new MethodPerformanceRegion("Bootstrap Events"))
                  {
                    Bootstrapper.currentWarmupCode = new Random().Next(1, 9999).ToString();
                    using (new MethodPerformanceRegion("On Initialized (Bootstrapped)"))
                      Bootstrapper.OnInitialized((object) null, new ExecutedEventArgs("Bootstrapped", (object) null));
                    using (new MethodPerformanceRegion("On Bootstrapped"))
                      Bootstrapper.OnBootstrapped((object) null, EventArgs.Empty);
                    Bootstrapper.currentWarmupCode = string.Empty;
                  }
                  Bootstrapper.finalEventsExecuted = true;
                  SystemManager.FireApplicationStart();
                  ReplicationMessageSynchronizer.Initialize();
                  if (!string.IsNullOrWhiteSpace(HealthCheckService.Endpoint))
                    HealthCheckFactory.Initialize();
                  Thread.CurrentThread.CurrentUICulture = currentUiCulture;
                }
                finally
                {
                  SystemManager.UnRegisterAppStatusTraceListener((TraceListener) listener, "ErrorLog", "UpgradeTrace", "PackagingTrace");
                }
              }
            }
          }
        }
        return false;
      }
      finally
      {
        Telerik.Sitefinity.HealthMonitoring.PerformanceMonitor.End("Sitefinity Core", nameof (Bootstrap), startTime);
      }
    }

    /// <summary>
    /// Fired before initializing Sitefinity application.
    /// Note this event could be fired multiple times by different
    /// process and services upon starting their initialization.
    /// </summary>
    public static event EventHandler<ExecutingEventArgs> Initializing;

    /// <summary>
    /// Fired after initialization of Sitefinity application.
    /// Note this event could be fired multiple times by different
    /// process and services upon finishing their initialization.
    /// </summary>
    public static event EventHandler<ExecutedEventArgs> Initialized;

    private static void OnInitializing(object sender, ExecutingEventArgs args)
    {
      if (Bootstrapper.Initializing == null)
        return;
      Bootstrapper.Initializing(sender, args);
    }

    private static void OnInitialized(object sender, ExecutedEventArgs args)
    {
      if (Bootstrapper.Initialized == null)
        return;
      Bootstrapper.Initialized(sender, args);
    }

    /// <summary>
    /// Occurs when the Sitefinity application is about to be initialized.
    /// <remarks>
    /// Same as when the Initializing event is fired with an argument command name of
    /// 'Bootstrapping'.
    /// </remarks>
    /// </summary>
    public static event EventHandler<EventArgs> Bootstrapping;

    /// <summary>
    /// Occurs when the Sitefinity application has been completely initialized.
    /// <remarks>
    /// Same as when the Initialized event is fired with an argument command name of
    /// 'Bootstrapped' which is the last Initialized event that is fired from this component.
    /// </remarks>
    /// </summary>
    public static event EventHandler<EventArgs> Bootstrapped;

    private static void OnBootstrapping(object sender, EventArgs args)
    {
      if (Bootstrapper.Bootstrapping == null)
        return;
      Bootstrapper.Bootstrapping(sender, args);
    }

    private static void OnBootstrapped(object sender, EventArgs args)
    {
      if (Bootstrapper.Bootstrapped != null)
      {
        Bootstrapper.Bootstrapped(sender, args);
        SystemManager.RegisterRegisterScheduledTasks();
      }
      ServiceStackAppHost.Initialize();
      Bootstrapper.RegisterConfigRestrictions();
    }

    /// <summary>Registers the configuration restrictions.</summary>
    private static void RegisterConfigRestrictions()
    {
      bool isCloudLicense = LicenseState.Current.LicenseInfo.LicenseType == "CL";
      PageNodeRestrictionStrategy.Add(SiteInitializer.LicensePageId, (Func<bool>) (() => !isCloudLicense && !SystemManager.IsOperationEnabled(RestrictionLevel.ReadOnlyConfigFile)));
      PageNodeRestrictionStrategy.Add(SiteInitializer.FilesPageId, RestrictionLevel.ReadOnlyConfigFile);
      PageNodeRestrictionStrategy.Add(SiteInitializer.ProfileTypesPageId, RestrictionLevel.ReadOnlyConfigFile);
      bool isLabelsAndMessagesReadOnly = Config.Get<ResourcesConfig>().LabelsAndMessagesReadOnly;
      PageNodeRestrictionStrategy.Add(SiteInitializer.LabelsPageId, (Func<bool>) (() => isLabelsAndMessagesReadOnly));
      CommandWidgetReadOnlyConfigRestrictionStrategy.Add("Settings", RestrictionLevel.ReadOnlyConfigFile);
    }

    internal static bool IsNLBComunicationSetup { get; private set; }

    internal static void SetupNLBComunication()
    {
      Bootstrapper.IsNLBComunicationSetup = true;
      MsmqSystemMessageSender.StartMsmqReceiver();
      RedisSystemMessageSender.StartRedisReceiver();
    }

    /// <summary>Registers Sitefinity’s system routes.</summary>
    /// <param name="routes">
    /// A collection of defined routes for the ASP.NET application.
    /// </param>
    private static bool RegisterRoutes()
    {
      bool flag = false;
      using (new ElevatedConfigModeRegion())
      {
        ExecutingEventArgs args = new ExecutingEventArgs(nameof (RegisterRoutes), (object) RouteManager.GetRegisteredRoutes().Select<RouteRegistration, RouteBase>((Func<RouteRegistration, RouteBase>) (r => r.Route)));
        Bootstrapper.OnInitializing((object) null, args);
        string moduleName = "System";
        if (!args.Cancel)
        {
          Bootstrapper.DeterminePermanentRoutes();
          RouteManager.ClearRegisteredRoutes();
          SystemManager.PendingRouteRegistrations.Clear();
          RouteManager.RouteExistingFiles = true;
          SystemConfig systemConfig = Config.Get<SystemConfig>(true);
          if (systemConfig.UIElmahSettings.IsElmahLoggingTurnedOn)
            RouteManager.RegisterRoute("ElmahRoute", (RouteBase) ObjectFactory.Resolve<ElmahRoute>(), moduleName, false);
          FileExtensionStopRoute route = ObjectFactory.Resolve<FileExtensionStopRoute>();
          route.Extensions.UnionWith((IEnumerable<string>) new string[3]
          {
            ".axd",
            ".ashx",
            ".xamlx"
          });
          RouteManager.RegisterRoute("FileExtensionStopRoute", (RouteBase) route, moduleName, false);
          string str = "SitefinityMvcRoute";
          if (ObjectFactory.IsTypeRegistered<RouteBase>(str))
            RouteManager.RegisterRoute("MvcRoute", ObjectFactory.Resolve<RouteBase>(str), moduleName, false);
          Bootstrapper.isDataInitialized = Config.HasDefaultDatabaseConnection;
          Bootstrapper.isSystemInitialized = Bootstrapper.isDataInitialized && Config.Get<SecurityConfig>().ApplicationRoles.Values.First<ApplicationRole>().Id != Guid.Empty;
          if (Bootstrapper.isSystemInitialized)
          {
            SecurityManager.CurrentSettings.Reset();
            flag = SystemManager.Initialize();
            if (flag)
              return flag;
            RouteManager.RegisterRoute("Themes", (RouteBase) new Route("Sitefinity/Themes/{*Params}", (IRouteHandler) new ThemeRouteHandler()), moduleName, false);
            RouteManager.RegisterRoute("WebsiteTemplates", (RouteBase) new Route("Sitefinity/WebsiteTemplates/{*Params}", (IRouteHandler) new WebsiteTemplatesRouteHandler()), moduleName, false);
            RouteManager.RegisterRoute("LicensingPage", (RouteBase) new Route("Sitefinity/Licensing", (IRouteHandler) new LicensingRouteHandler()), moduleName, false);
            RouteManager.RegisterRoute("Resources", (RouteBase) new Route("~/Res/".Trim('~', '/') + "/{resourceName}", (IRouteHandler) ObjectFactory.Resolve<ResourceRouteHandler>()), moduleName, false);
            RouteManager.RegisterRoute("ExternalResources", (RouteBase) new Route("~/ExtRes/".Trim('~', '/') + "/{resourceName}", (IRouteHandler) ObjectFactory.Resolve<ResourceRouteHandler>()), moduleName, false);
            RouteManager.RegisterRoute("ResourceImages", (RouteBase) new Route("SFRes/Images/{assemblyName}/{resourceName}", (IRouteHandler) ObjectFactory.Resolve<ResourceImagesRouteHandler>()), moduleName, false);
            RouteManager.RegisterRoute("WebStatistics", (RouteBase) new Route("Sitefinity/WebCounter/{*Params}", (IRouteHandler) ObjectFactory.Resolve<StatisticsRouteHandler>()), moduleName, false);
            if (Config.Get<SecurityConfig>().AuthenticationMode == AuthenticationMode.Forms)
            {
              RouteManager.RegisterRoute("LoginUtil", (RouteBase) new Route("Sitefinity/Login/{View}", (IRouteHandler) new LoginRouteHandler()), moduleName, false);
              RouteManager.RegisterRoute("Login", (RouteBase) new Route("Sitefinity/Login", new RouteValueDictionary()
              {
                {
                  "View",
                  (object) "Login"
                }
              }, (IRouteHandler) new LoginRouteHandler()), moduleName, false);
              RouteManager.RegisterRoute("SignOutService", (RouteBase) new Route("Sitefinity/SignOut/{*method}", (IRouteHandler) new SignOutRouteHandler()), moduleName, false);
              RouteManager.RegisterRoute("SecurityRedirectRoute", (RouteBase) ObjectFactory.Resolve<SecurityRedirectRoute>(), moduleName, false);
            }
            else
            {
              RouteManager.RegisterRoute("SignOutService", (RouteBase) new Route("Sitefinity/SignOut/{*method}", (IRouteHandler) new SignOutRouteHandler()), moduleName, false);
              RouteManager.RegisterRoute("ProtectedRoute", (RouteBase) new ProtectedRoute("Sitefinity/{*EverythingElse}"), moduleName, false);
            }
            int num = !systemConfig.DisableBackendUI ? 1 : 0;
            RouteManager.RegisterRoute("Log", (RouteBase) new Route("Sitefinity/Logs/{*Params}", (IRouteHandler) new LogRouteHandler()), moduleName, false);
            RouteManager.RegisterRoute("Licensing", (RouteBase) new LicensingRoute(), moduleName, false);
            RouteManager.RegisterRoute("Versioning", (RouteBase) new Route("Sitefinity/Versioning/{itemId}/{VersionNumber}", (IRouteHandler) new VersioningRouteHandler()), moduleName, false);
            if (num != 0)
              RouteManager.RegisterRoute("Dialogs", (RouteBase) ObjectFactory.Resolve<DialogRoute>(), moduleName, false);
            RouteManager.RegisterRoute("TemplateEditor", (RouteBase) ObjectFactory.Resolve<TemplateRoute>(), moduleName, false);
            SystemManager.RegisterWebService(typeof (ModulesService), "Sitefinity/Services/ModulesService", moduleName);
            SystemManager.RegisterWebService(typeof (DataSourceService), "Sitefinity/Services/DataSourceService");
            SystemManager.RegisterWebService(typeof (MembershipProviderService), "Sitefinity/Services/MembershipProviderService");
            SystemManager.RegisterWebService(typeof (BasicSettingsService), "Sitefinity/Services/BasicSettings.svc");
            SystemManager.RegisterWebService(typeof (EmailSettingsService), "Sitefinity/Services/Configuration/EmailSettings.svc");
            SystemManager.RegisterWebService(typeof (SystemEmailsService), "Sitefinity/Services/SystemEmails/Settings.svc");
            SystemManager.RegisterWebService(typeof (ContentItemLocationService), "Sitefinity/Services/LocationService");
            SystemManager.RegisterWebService(typeof (CountryLocationService), "Sitefinity/Services/CountryLocationService");
            SystemManager.RegisterWebService(typeof (CacheProfilesService), "Sitefinity/Services/CacheProfiles/Settings.svc");
            RouteManager.RegisterRoute("AppThemes", (RouteBase) new Route("App_Themes/{ThemeName}/{resourceName}.less", (IRouteHandler) new ThemeRouteHandler()), moduleName, false);
            SystemManager.PendingRouteRegistrations.ForEach((Action<RouteRegistration>) (r => RouteManager.RegisterRoute(r)));
            SystemManager.PendingRouteRegistrations.Clear();
            if (num != 0)
              RouteManager.RegisterRoute("Backend", (RouteBase) ObjectFactory.Resolve<BackendRoute>(), moduleName, false);
            RouteManager.RegisterRoute("LanguagePack", (RouteBase) new Route("Sitefinity/LanguagePack/{*Params}", (IRouteHandler) ObjectFactory.Resolve<LanguagePackRouteHandler>()), moduleName, false);
            RouteManager.RegisterRoute("Frontend", (RouteBase) ObjectFactory.Resolve<SitefinityRoute>(), moduleName, false);
            RouteManager.RegisterRoute("ContentLocation", (RouteBase) ObjectFactory.Resolve<ContentLocationRoute>(), moduleName, false);
            RouteManager.RegisterRoute("RestApiProtectedRoute", (RouteBase) new ProtectedRoute("RestApi/Sitefinity" + "/{*EverythingElse}"), moduleName, false);
            SystemManager.RegisterServiceStackPlugin((IPlugin) new InlineEditingServiceStackPlugin());
            SystemManager.RegisterServiceStackPlugin((IPlugin) new UserSessionServiceStackPlugin());
            SystemManager.RegisterServiceStackPlugin((IPlugin) new RelatedDataServiceStackPlugin());
            SystemManager.RegisterServiceStackPlugin((IPlugin) new GenericDataServiceStackPlugin());
            SystemManager.RegisterServiceStackPlugin((IPlugin) new MarkupGeneratorServiceStackPlugin());
            SystemManager.RegisterServiceStackPlugin((IPlugin) new CacheServiceStackPlugin());
            SystemManager.RegisterServiceStackPlugin((IPlugin) new CaptchaServiceStackPlugin());
            RouteManager.RegisterRoute("AppStatusPage", (RouteBase) new Route(AppStatusService.GetAppStatusPageRelativeUrl().Trim('/'), (IRouteHandler) new AppStatusRouteHandler()), moduleName, false);
          }
          else
          {
            RouteManager.RegisterRoute("LicensingPage", (RouteBase) new Route("Sitefinity/Licensing", (IRouteHandler) new LicensingRouteHandler()), moduleName, false);
            RouteManager.RegisterRoute("Licensing", (RouteBase) new LicensingRoute(), moduleName, false);
            RouteManager.RegisterRoute("Startup", (RouteBase) new Route("Sitefinity/Startup", (IRouteHandler) new StartupRouteHandler()), moduleName, false);
            RouteManager.RegisterRoute("RootRedirect", (RouteBase) new Route("Sitefinity/{*Params}", (IRouteHandler) new BackendRedirectRouteHandler("Startup", "Sitefinity", false)), moduleName, false);
            RouteManager.RegisterRoute("Frontend", (RouteBase) new Route("{*Params}", (IRouteHandler) new BackendRedirectRouteHandler("Startup", "Sitefinity", false)), moduleName, false);
          }
        }
        if (!args.Cancel)
          Bootstrapper.RegisterPermanentRoutes();
        if (Bootstrapper.IsSystemInitialized)
          Bootstrapper.OnInitialized((object) null, new ExecutedEventArgs(nameof (RegisterRoutes), (object) RouteManager.GetRegisteredRoutes().Select<RouteRegistration, RouteBase>((Func<RouteRegistration, RouteBase>) (r => r.Route))));
      }
      return flag;
    }

    /// <summary>
    /// Stores the routes in the routes table that we want to keep if the collection is cleared.
    /// </summary>
    private static void DeterminePermanentRoutes()
    {
      Bootstrapper.RegisterPermanentRoutes(Bootstrapper.integrationTestsRouteKey);
      Bootstrapper.RegisterPermanentRoutes(Bootstrapper.uiTestsRouteKey);
    }

    /// <summary>Registers the system permanent routes.</summary>
    /// <param name="key">Route name</param>
    private static void RegisterPermanentRoutes(string key)
    {
      RouteBase route = RouteTable.Routes[key];
      if (route == null)
        return;
      Bootstrapper.permanentRoutes[key] = new RouteRegistration(key, route, "System", false);
    }

    /// <summary>
    /// Registers any routes that were saved by the method <see cref="M:Telerik.Sitefinity.Abstractions.Bootstrapper.DeterminePermanentRoutes" />
    /// </summary>
    private static void RegisterPermanentRoutes()
    {
      foreach (KeyValuePair<string, RouteRegistration> permanentRoute in Bootstrapper.permanentRoutes)
        RouteManager.RegisterRoute(permanentRoute.Value);
      Bootstrapper.permanentRoutes.Clear();
    }

    /// <summary>
    /// Gets a value indicating whether the application is (re)starting at this time.
    /// </summary>
    /// <value>
    ///     <c>true</c> if this application is restarting; otherwise, <c>false</c>.
    /// </value>
    [Obsolete("This is for internal use only and will be removed from future versions of Sitefinity. Use !IsReady instead.")]
    public static bool IsAppRestarting => !Bootstrapper.hasStarted;

    /// <summary>
    /// Gets a value indicating whether the application is ready to handle requests.
    /// <para>N.B. It can be True even if there is no database yet (the system still can handle requests - e.g. status page).</para>
    /// </summary>
    /// <value>
    ///     <c>true</c> if this application is ready to handle requests; otherwise, <c>false</c>.
    /// </value>
    internal static bool FinalEventsExecuted => Bootstrapper.finalEventsExecuted;

    /// <summary>
    /// Gets a value indicating whether the system data initialized.
    /// </summary>
    /// <value>
    ///     <c>true</c> if the system data is initialized; otherwise, <c>false</c>.
    /// </value>
    [Obsolete("This is for internal use only and will be removed from future versions of Sitefinity. Use IsReady instead.")]
    public static bool IsDataInitialized => Bootstrapper.isDataInitialized;

    /// <summary>
    /// Gets a value indicating whether this instance is setup (database has been configured).
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is setup and ready; otherwise, <c>false</c>.
    /// </value>
    public static bool IsReady => Bootstrapper.isSystemInitialized && Bootstrapper.hasStarted;

    internal static bool IsFirstBoot => Bootstrapper.firstBoot;

    /// <summary>
    /// Gets a value indicating whether this instance is initialized (database has been congiured).
    /// </summary>
    /// <value>
    ///   <c>true</c> if this instance is system initialized; otherwise, <c>false</c>.
    /// </value>
    [Obsolete("This is for internal use only and will be removed from future versions of Sitefinity. Use IsReady instead.")]
    public static bool IsSystemInitialized => Bootstrapper.isSystemInitialized;

    internal static string CurrentWarmupCode => Bootstrapper.currentWarmupCode;
  }
}
