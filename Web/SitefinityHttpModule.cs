// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.SitefinityHttpModule
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Routing;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Unity;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Logging;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.AppStatus;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Health;
using Telerik.Sitefinity.Logging;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Events;
using Telerik.Sitefinity.Services.Operation;
using Telerik.Sitefinity.TrackingConsent;
using Telerik.Sitefinity.Utilities;
using Telerik.Sitefinity.Web.CustomErrorPages;
using Telerik.Sitefinity.Web.OutputCache;
using Telerik.Sitefinity.Workflow.Configuration;

namespace Telerik.Sitefinity.Web
{
  /// <summary>Handles requests processed by Sitefinity.</summary>
  public class SitefinityHttpModule : UrlRoutingModule
  {
    public const string ServiceRequestHeader = "X-SF-Service-Request";
    internal const int SitefinityInitValue = 0;
    internal const string SitefinityInitKey = "RadControlRandomNumber";
    private static volatile bool isFirstInstance = true;
    private static volatile bool isFirstRequest = true;
    private static volatile bool isInitialRequest = true;
    private static volatile string appVirtualPath;
    private static readonly object initSyncLock = new object();
    private static readonly object schedulerSyncLock = new object();
    private static bool loadedCacheSettings = false;
    private static readonly ConcurrentProperty<bool> waitForPageOutputCacheToFill = new ConcurrentProperty<bool>((Func<bool>) (() => Config.Get<SystemConfig>().CacheSettings.WaitForPageOutputCacheToFill));
    private static volatile bool statusPageShown = false;
    private readonly string maxAge = 172800.ToString();
    private const string accessControlAllowOrigin = "Access-Control-Allow-Origin";
    private static ErrorPagesHandler customErrorPagesHandler = new ErrorPagesHandler();

    /// <summary>
    /// Indicates whether page should be served only once before its output cache is filled.
    /// </summary>
    internal static bool WaitForPageOutputCacheToFill => SitefinityHttpModule.waitForPageOutputCacheToFill.Value;

    protected override void Init(HttpApplication application)
    {
      base.Init(application);
      if (SitefinityHttpModule.isFirstInstance)
      {
        SitefinityHttpModule.isFirstInstance = false;
        SystemManager.AppDataFolderPhysicalPath = application.Context.Server.MapPath("~/App_Data");
        SitefinityHttpModule.appVirtualPath = HostingEnvironment.ApplicationVirtualPath;
        if (!SitefinityHttpModule.appVirtualPath.EndsWith("/"))
          SitefinityHttpModule.appVirtualPath += "/";
        SystemManager.LocalUri = this.ResolveLocalUrl(application.Context);
        SystemManager.AbsolutePathRootUrlOfFirstRequest = application.Context.Request.Url.GetLeftPart(UriPartial.Authority) + application.Context.Request.ApplicationPath;
        Task task = SitefinityHttpModule.BootstrapAsync();
        if (!AppStatusService.IsEnabled())
          task.Wait();
      }
      application.Error += new EventHandler(this.context_Error);
      application.BeginRequest += new EventHandler(this.context_BeginRequest);
      application.AuthenticateRequest += new EventHandler(this.AppContext_AuthenticateRequest);
      application.PostAuthenticateRequest += new EventHandler(this.AppContext_PostAuthenticateRequest);
      application.EndRequest += new EventHandler(this.Context_EndRequest);
      application.PreSendRequestHeaders += new EventHandler(this.Context_PreSendRequestHeaders);
      application.PostAuthorizeRequest += new EventHandler(this.Context_PostAuthorizeRequest);
    }

    internal static Task BootstrapAsync()
    {
      Task task = Task.Factory.StartNew((Action<object>) (ctx =>
      {
        HttpContextBase httpContextBase = (HttpContextBase) ctx;
        HttpContext httpContext = new HttpContext(new System.Web.HttpRequest(string.Empty, httpContextBase.Request.Url.AbsoluteUri, string.Empty), new System.Web.HttpResponse((TextWriter) new StringWriter(new StringBuilder())));
        httpContext.ApplicationInstance = httpContextBase.ApplicationInstance;
        HttpContext.Current = httpContext;
        httpContext.Items[(object) "BootstrapStart"] = (object) DateTime.UtcNow;
        try
        {
          Bootstrapper.Bootstrap();
          EventHub.Raise((IEvent) new BackgroundOperationEndEvent("Bootstrap"));
        }
        catch (Exception ex)
        {
          SitefinityHttpModule.HandleBootstrapException(ex);
        }
        finally
        {
          SystemManager.ClearCurrentTransactions();
        }
      }), (object) SystemManager.CurrentHttpContext);
      Thread.Sleep(3000);
      return task;
    }

    internal static bool HandleBootstrapException(Exception exception)
    {
      if (exception is TargetInvocationException && exception.InnerException != null)
        exception = exception.InnerException;
      if (exception is ReflectionTypeLoadException typeLoadException && typeLoadException.LoaderExceptions.Length != 0)
        exception = typeLoadException.LoaderExceptions[0];
      string restartReason = exception.Message;
      string str = exception.StackTrace;
      for (Exception innerException = exception.InnerException; innerException != null; innerException = innerException.InnerException)
      {
        restartReason = restartReason + " ---> " + innerException.Message;
        str = str + Environment.NewLine + " ---> " + Environment.NewLine + innerException.StackTrace;
      }
      SystemManager.AppStatusBuffer.TryAdd(new AppStatusEntry()
      {
        Message = restartReason,
        StackTrace = str,
        SeverityString = TraceEventType.Critical.ToString(),
        TimestampString = DateTime.UtcNow.ToString((IFormatProvider) CultureInfo.CurrentCulture)
      });
      LogEntry message = new LogEntry()
      {
        Message = exception.ToString(),
        Severity = TraceEventType.Critical,
        TimeStamp = DateTime.UtcNow
      };
      try
      {
        Telerik.Sitefinity.Abstractions.Log.Write((object) message, ConfigurationPolicy.ErrorLog);
      }
      catch (ActivationException ex)
      {
        ConfigurationSourceBuilder configBuilder = new ConfigurationSourceBuilder();
        ObjectFactory.Container.RegisterInstance<ISitefinityLogCategoryConfigurator>(Telerik.Sitefinity.Abstractions.Log.GetDefaultCategoryConfigurator(true));
        Telerik.Sitefinity.Abstractions.Log.Configure((IConfigurationSourceBuilder) configBuilder);
        DictionaryConfigurationSource source = new DictionaryConfigurationSource();
        configBuilder.UpdateConfigurationWithReplace((IConfigurationSource) source);
        ObjectFactory.Container.AddExtension((UnityContainerExtension) new EnterpriseLibraryCoreExtension((IConfigurationSource) source));
        EnterpriseLibraryContainer.Current = (IServiceLocator) new UnityServiceLocator(ObjectFactory.Container);
        Telerik.Sitefinity.Abstractions.Log.Write((object) message, ConfigurationPolicy.ErrorLog);
      }
      Thread.Sleep(10000);
      SystemManager.TryRestartApplication(restartReason);
      return false;
    }

    protected virtual void AppContext_PostAuthenticateRequest(object sender, EventArgs e)
    {
      string encryptedValidationKey = ((HttpApplication) sender).Context.Request.QueryStringGet("sf-auth");
      if (encryptedValidationKey == null)
        return;
      SecurityManager.AuthenticateUser(encryptedValidationKey, true);
    }

    protected virtual void AppContext_AuthenticateRequest(object sender, EventArgs e)
    {
      if (SecurityManager.AuthenticationMode == AuthenticationMode.Forms)
      {
        HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
        SecurityManager.AuthenticateRequest(currentHttpContext);
        Thread.CurrentPrincipal = currentHttpContext.User;
      }
      else
      {
        HttpContext context = ((HttpApplication) sender).Context;
        System.Web.HttpRequest request = context.Request;
        if (request.Headers["SF-Sys-Message"].IsNullOrEmpty() || !(SecurityManager.DecryptData(request.Headers["SF-Sys-Message"]) == SecurityManager.SystemAccountIDs[0]))
          return;
        string str = context.Request.AppRelativeCurrentExecutionFilePath.Substring(1) + context.Request.PathInfo;
        if ((!SystemManager.IsInLoadBalancingMode || !str.StartsWith("/Sitefinity/Services/LoadBalancing/SystemWebService.svc/", StringComparison.OrdinalIgnoreCase)) && (!Config.Get<WorkflowConfig>().RunWorkflowAsService || !str.EndsWith(".xamlx")))
          return;
        SecurityManager.AuthenticateSystemRequest((HttpContextBase) new HttpContextWrapper(context));
      }
    }

    private void context_Error(object sender, EventArgs e)
    {
      HttpApplication httpApplication = (HttpApplication) sender;
      HttpServerUtility server = httpApplication.Server;
      Exception err = server.GetLastError();
      if (err is HttpException exceptionToHandle1)
      {
        if (this.IsUnauthorizedAccessException(err))
        {
          int num = !SystemManager.CurrentHttpContext.User.Identity.IsAuthenticated ? 401 : 403;
          httpApplication.Context.Response.StatusCode = num;
          server.ClearError();
        }
        if (exceptionToHandle1.GetHttpCode() == 404)
        {
          Exceptions.HandleException((Exception) exceptionToHandle1, ExceptionPolicyName.Http404);
          return;
        }
        Exception exceptionToHandle = (Exception) exceptionToHandle1;
        if (exceptionToHandle1.InnerException != null)
          exceptionToHandle = exceptionToHandle1.InnerException;
        Exceptions.HandleException(exceptionToHandle, ExceptionPolicyName.UnhandledExceptions);
      }
      for (; err != null; err = err.InnerException)
      {
        if (err is CryptographicException)
        {
          server.ClearError();
          Thread.Sleep(0);
          break;
        }
      }
    }

    private bool IsUnauthorizedAccessException(Exception err)
    {
      for (; err != null; err = err.InnerException)
      {
        if (err is UnauthorizedAccessException)
          return true;
      }
      return false;
    }

    private void context_BeginRequest(object sender, EventArgs e)
    {
      HttpApplication app = (HttpApplication) sender;
      if (SitefinityHttpModule.isInitialRequest)
      {
        lock (SitefinityHttpModule.initSyncLock)
        {
          if (SitefinityHttpModule.isInitialRequest)
          {
            SystemManager.LocalUri = this.ResolveLocalUrl(app.Context);
            SystemManager.AbsolutePathRootUrlOfFirstRequest = app.Context.Request.Url.GetLeftPart(UriPartial.Authority) + app.Context.Request.ApplicationPath;
            TrackingConsentManager.EnsureConsentDialogExists();
            SitefinityHttpModule.EnableTrustedServerCertificateHosts();
            SitefinityHttpModule.isInitialRequest = false;
          }
        }
      }
      if (HealthCheckService.CanHandle(app.Context.Request))
        HealthCheckService.ProcessRequest(app.Context);
      else if (!Bootstrapper.FinalEventsExecuted && AppStatusService.IsEnabled() && !this.IsWarmupRequest((HttpContextBase) new HttpContextWrapper(app.Context)))
      {
        string str = app.Context.Request.AppRelativeCurrentExecutionFilePath.Substring(1) + app.Context.Request.PathInfo;
        if (Bootstrapper.IsNLBComunicationSetup && SystemManager.IsInLoadBalancingMode && str.StartsWith("/Sitefinity/Services/LoadBalancing/SystemWebService.svc/", StringComparison.OrdinalIgnoreCase))
          return;
        if (AppStatusService.IsValidServiceRequest(app.Context))
        {
          AppStatusService.GetServiceResponse(app.Context);
        }
        else
        {
          string statusPageRelativeUrl = AppStatusService.GetAppStatusPageRelativeUrl();
          if (app.Context.Request.Url.AbsolutePath != SitefinityHttpModule.AppVirtualPathAwareUrl(statusPageRelativeUrl))
          {
            string url = SitefinityHttpModule.AppVirtualPathAwareUrl(statusPageRelativeUrl + "?ReturnUrl=") + HttpUtility.UrlEncode(app.Context.Request.Url.AbsoluteUri);
            app.Context.Response.Redirect(url, false);
            app.CompleteRequest();
          }
          else if (SystemManager.IsUpgrading)
            this.OnSystemUpgrading(app.Context);
          else
            this.OnSystemRestarting(app.Context);
        }
      }
      else
      {
        if (!Bootstrapper.IsSystemInitialized)
          return;
        if (SitefinityHttpModule.isFirstRequest)
        {
          lock (SitefinityHttpModule.schedulerSyncLock)
          {
            if (SitefinityHttpModule.isFirstRequest)
            {
              SystemManager.CleanUpAppStatusBuffer();
              SystemManager.OnFirstRequestBegin();
              SitefinityHttpModule.isFirstRequest = false;
            }
          }
        }
        if (this.ProcessRedirect())
        {
          app.CompleteRequest();
        }
        else
        {
          this.InitalizeRequestContext();
          if (SitefinityHttpModule.WaitForPageOutputCacheToFill)
            this.WaitForCacheToFill(app);
          EventHub.Raise((IEvent) new RequestStartEvent(app.Context));
          this.HandleServiceRequest(app.Context);
        }
      }
    }

    private Uri ResolveLocalUrl(HttpContext context)
    {
      Uri uri;
      if (SystemManager.TryResolveWarmupUrl(out uri))
        return uri;
      Uri url = context.Request.Url;
      UriBuilder uriBuilder = new UriBuilder(url.Scheme, url.Host, url.Port);
      if (!context.Request.IsLocal)
      {
        try
        {
          HttpWorkerRequest service = (HttpWorkerRequest) ((IServiceProvider) context).GetService(typeof (HttpWorkerRequest));
          if (service != null)
          {
            string str = service.GetLocalAddress();
            if (str.IsNullOrEmpty())
              str = "127.0.0.1";
            int num = service.GetLocalPort();
            if (num == 0)
              num = 80;
            string protocol = service.GetProtocol();
            if (!protocol.IsNullOrEmpty())
              uriBuilder.Scheme = protocol;
            uriBuilder.Port = num;
            uriBuilder.Host = str;
          }
        }
        catch (Exception ex)
        {
          if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
            throw ex;
        }
      }
      return uriBuilder.Uri;
    }

    private bool IsAutomationTestArrangementRequest(HttpContext context) => context.Request.Url.AbsolutePath.Contains("AutomationTests/AutomationTestsArrangemets.svc");

    private void HandleServiceRequest(HttpContext context)
    {
      string header = context.Request.Headers["X-SF-Service-Request"];
      bool flag = false;
      ref bool local = ref flag;
      bool.TryParse(header, out local);
      if (!flag)
        return;
      SystemManager.CurrentContext.IsServiceRequest = true;
    }

    private bool ProcessRedirect()
    {
      if (SitefinityHttpModule.appVirtualPath.Length > 1)
      {
        HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
        if (currentHttpContext.Request.Url.Segments.Length != 0 && !currentHttpContext.Request.Url.AbsolutePath.StartsWith(SitefinityHttpModule.appVirtualPath, StringComparison.Ordinal) && currentHttpContext.Request.Url.AbsolutePath.StartsWith(SitefinityHttpModule.appVirtualPath, StringComparison.OrdinalIgnoreCase))
        {
          string str = currentHttpContext.Request.Url.AbsolutePath.Substring(SitefinityHttpModule.appVirtualPath.Length);
          if (str.Equals("default.aspx", StringComparison.OrdinalIgnoreCase))
            str = string.Empty;
          RouteHelper.RedirectPermanent(currentHttpContext, "~/" + str);
          return true;
        }
      }
      return false;
    }

    /// <summary>
    /// Called on begin request during system restarting. The request should be completed here, because the system will not be able to handle the request at this time.
    /// By default sends 'The system is initializing/restarting...' message and completes the request.
    /// Can be overridden to implement different behavior.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    protected virtual void OnSystemRestarting(HttpContext context, string html = null, string scriptUrl = null)
    {
      if (html == null)
        AppStatusService.GetDefaultPageResources(context, out html, out scriptUrl);
      AppStatusService.DisplayStaticPage(context, html, scriptUrl);
    }

    /// <summary>
    /// Called on begin request during system upgrading. The request should be completed here, because the system will not be able to handle the request at this time.
    /// By default sends 'The system is upgrading...' message and completes the request.
    /// Can be overridden to implement different behavior.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    protected virtual void OnSystemUpgrading(HttpContext context, string html = null, string scriptUrl = null)
    {
      if (html == null)
        AppStatusService.GetDefaultPageResources(context, out html, out scriptUrl);
      AppStatusService.DisplayStaticPage(context, html, scriptUrl);
    }

    private void Context_EndRequest(object sender, EventArgs e)
    {
      HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
      string str = "Sitefinity/Authenticate/OpenID/content";
      string absolutePath = currentHttpContext.Request.Url.AbsolutePath;
      if (absolutePath != null && (absolutePath.EndsWith(".css") || absolutePath.EndsWith(".woff") || absolutePath.EndsWith(".woff2")) && absolutePath.Contains(str))
      {
        NameValueCollection headers = currentHttpContext.Response.Headers;
        if (!headers.Keys.Contains("Access-Control-Allow-Origin"))
          headers.Add("Access-Control-Allow-Origin", "*");
        headers.Add("Cache-Control", "public, max-age=" + this.maxAge);
      }
      if (Bootstrapper.FinalEventsExecuted || this.IsWarmupRequest(currentHttpContext))
      {
        EventHub.Raise((IEvent) new RequestEndEvent(((HttpApplication) sender).Context));
        SecurityManager.UpdateCookies(currentHttpContext);
      }
      else if (currentHttpContext.Response.StatusCode == 200 && currentHttpContext.Request.Url.AbsolutePath.EndsWith("/default.aspx"))
      {
        string statusPageRelativeUrl = AppStatusService.GetAppStatusPageRelativeUrl();
        currentHttpContext.Response.Redirect(SitefinityHttpModule.AppVirtualPathAwareUrl(statusPageRelativeUrl + "?ReturnUrl=") + currentHttpContext.Request.Url.AbsoluteUri.Substring(0, currentHttpContext.Request.Url.AbsoluteUri.IndexOf("/default.aspx")));
      }
      SitefinityHttpModule.customErrorPagesHandler.ProcessRequest();
      SystemManager.CurrentTransactions?.Dispose();
    }

    private void Context_PreSendRequestHeaders(object sender, EventArgs e)
    {
      PageRouteHandler.TryEnchanceResponseCache(((HttpApplication) sender).Context);
      EventHub.Raise((IEvent) new SendResponseHeadersEvent(this.GetType().Name, sender));
    }

    [Obsolete("Delayed redirects are not supported anymore. They are replaced by periodical AJAX calls that will display the state of the application. Please override OnSystemRestarting and OnSystemUpgrading in order to control the application state messages displayed to the user.Currently this method will render the HTML you pass to it, but it wont do periodical reload")]
    public static void SendDelayedRedirect(
      HttpContext context,
      string bodyHtml,
      int timeout = 3000,
      string redirectUrl = null)
    {
      AppStatusService.DisplayStaticPage(context, bodyHtml);
    }

    private void Context_PostAuthorizeRequest(object sender, EventArgs e)
    {
      if (SitefinityOutputCacheModule.Initialized)
        return;
      HttpContext context = ((HttpApplication) sender).Context;
      IList<ICustomOutputCacheVariation> cacheVariations;
      if (context == null || context.Request == null || !PageRouteHandler.TryGetCurrentCustomCacheVariations(out cacheVariations))
        return;
      PageRouteHandler.InitializeCacheHeaders(cacheVariations);
    }

    private void InitalizeRequestContext() => SystemManager.CurrentHttpContext.Items[(object) "RadControlRandomNumber"] = (object) 0;

    /// <summary>
    /// Waits for page  output cache to fill. This method blocks the execution of a request until the output cache of this page is filled.
    /// This is done in order to avoid unnecessary page processing - that can lead to high processor usage or database usage
    /// </summary>
    /// <param name="app">The app.</param>
    private void WaitForCacheToFill(HttpApplication app)
    {
      System.Web.HttpRequest request = app.Request;
      object obj = (object) null;
      PageHandlerWrapper.PageRequestsToBeCached.TryGetValue(request.Url.OriginalString, out obj);
      if (obj == null || !Monitor.TryEnter(obj, 60000))
        return;
      Monitor.Exit(obj);
    }

    private bool IsWarmupRequest(HttpContextBase context)
    {
      if (Bootstrapper.IsReady)
      {
        string header = context.Request.Headers["Warmup-Code"];
        if (header != null && header.Equals(Bootstrapper.CurrentWarmupCode))
          return true;
      }
      return false;
    }

    private static string AppVirtualPathAwareUrl(string rawUrl) => SitefinityHttpModule.appVirtualPath + rawUrl.TrimStart('/');

    private static void EnableTrustedServerCertificateHosts() => ServicePointManager.ServerCertificateValidationCallback = (RemoteCertificateValidationCallback) ((sender, certificate, chain, errors) => errors == SslPolicyErrors.None || sender is HttpWebRequest httpWebRequest && SystemManager.IsDomainTrusted(httpWebRequest.RequestUri));
  }
}
