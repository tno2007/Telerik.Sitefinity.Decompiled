// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Warmup.WarmupModule
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.AppStatus;
using Telerik.Sitefinity.BackgroundTasks;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.HealthMonitoring;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.UsageTracking.TrackingReporters;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Warmup.Configuration;
using Telerik.Sitefinity.Warmup.Plugins;

namespace Telerik.Sitefinity.Warmup
{
  /// <summary>
  /// Defines the warmup module for plugging page warmup functionality
  /// </summary>
  internal class WarmupModule : ModuleBase, ITrackingReporter
  {
    /// <summary>The name of the Warmup module.</summary>
    public const string ModuleName = "Warmup";
    private static bool forceStart;

    /// <summary>Gets the landing page id.</summary>
    /// <value>The landing page id.</value>
    public override Guid LandingPageId => Guid.Empty;

    /// <summary>Gets the managers.</summary>
    /// <value>The managers.</value>
    public override Type[] Managers => new Type[0];

    /// <summary>
    /// Gets a value indicating whether the <see cref="T:Telerik.Sitefinity.Warmup.WarmupModule" /> should start the warmup process.
    /// </summary>
    /// <value>
    /// <c>true</c> if the <see cref="T:Telerik.Sitefinity.Warmup.WarmupModule" /> should start the warmup process; otherwise, <c>false</c>.
    /// </value>
    internal bool ShouldRun => WarmupModule.forceStart || Bootstrapper.IsFirstBoot;

    /// <summary>Installs the specified initializer.</summary>
    /// <param name="initializer">The initializer.</param>
    public override void Install(SiteInitializer initializer)
    {
    }

    /// <summary>Initializes the specified settings.</summary>
    /// <param name="settings">The settings.</param>
    public override void Initialize(ModuleSettings settings)
    {
      base.Initialize(settings);
      App.WorkWith().Module("Warmup").Initialize().Configuration<WarmupConfig>().Localization<WarmupResources>();
    }

    /// <summary>
    /// Loads the module dependencies after the module has been initialized and installed.
    /// </summary>
    public override void Load()
    {
      base.Load();
      if (!this.ShouldRun)
        return;
      Bootstrapper.Bootstrapped += new EventHandler<EventArgs>(this.Bootstrapper_Bootstrapped);
      WarmupModule.forceStart = false;
    }

    /// <summary>Get tracking report for Warmup module</summary>
    /// <returns>Report for Warmup module</returns>
    public object GetReport()
    {
      WarmupConfig warmupConfig = Config.Get<WarmupConfig>();
      WarmupModuleReport report = new WarmupModuleReport();
      IEnumerable<WarmupPluginElement> plugins = warmupConfig.Plugins.Values.Where<WarmupPluginElement>((Func<WarmupPluginElement, bool>) (p => p.Enabled));
      report.ModuleName = "Warmup";
      report.IsUsed = this.IsModuleInUse(plugins);
      return (object) report;
    }

    /// <summary>
    /// Resets the <see cref="T:Telerik.Sitefinity.Warmup.WarmupModule" />.
    /// </summary>
    internal void Reset() => WarmupModule.forceStart = true;

    /// <summary>Runs the warmup</summary>
    protected void Run()
    {
      using (new MethodPerformanceRegion("Warmup module"))
      {
        HttpContext current = HttpContext.Current;
        try
        {
          if (current != null)
            HttpContext.Current = new HttpContext(new HttpRequest(string.Empty, SystemManager.AbsolutePathRootUrlOfFirstRequest, string.Empty), new HttpResponse((TextWriter) new StringWriter(new StringBuilder())));
          this.WarmupModules();
          WarmupConfig config = Config.Get<WarmupConfig>();
          WarmupModule.WarmupContext warmupContext = new WarmupModule.WarmupContext(config);
          List<WarmupUrl> urls = this.GetUniqueUrls(config.Plugins).Values.OrderByDescending<WarmupUrl, WarmupPriority>((Func<WarmupUrl, WarmupPriority>) (u => u.Priority)).ToList<WarmupUrl>();
          int startupUrlsCount = this.WarmupUrls("Startup", urls.Where<WarmupUrl>((Func<WarmupUrl, bool>) (u => u.Priority == WarmupPriority.High)).Take<WarmupUrl>(config.MaxRequestsOnStartup), warmupContext);
          if (urls.Count <= startupUrlsCount)
            return;
          ObjectFactory.Resolve<IBackgroundTasksService>()?.EnqueueTask((Action) (() => this.WarmupUrls("Background", urls.Skip<WarmupUrl>(startupUrlsCount), warmupContext)));
        }
        catch (Exception ex)
        {
          ApplicationException exceptionToHandle = new ApplicationException(string.Format("Warmup module execution failed. Actual err: {0}", (object) ex.Message));
          if (Exceptions.HandleException((Exception) exceptionToHandle, ExceptionPolicyName.IgnoreExceptions))
            throw exceptionToHandle;
        }
        finally
        {
          if (current != null)
            HttpContext.Current = current;
        }
      }
    }

    /// <summary>Gets the module config.</summary>
    /// <returns>The module config</returns>
    protected override ConfigSection GetModuleConfig() => (ConfigSection) Config.Get<WarmupConfig>();

    private void WarmupModules()
    {
      foreach (IWarmupModule warmupModule in SystemManager.ApplicationModules.Values.OfType<IWarmupModule>())
      {
        using (new MethodPerformanceRegion(string.Format("Warming up {0}", (object) warmupModule.GetType().FullName)))
          warmupModule.Run();
      }
    }

    private int WarmupUrls(
      string title,
      IEnumerable<WarmupUrl> urls,
      WarmupModule.WarmupContext context)
    {
      int num1 = urls.Count<WarmupUrl>();
      if (num1 == 0)
        return num1;
      Stopwatch stopwatch = new Stopwatch();
      stopwatch.Start();
      Telerik.Sitefinity.Abstractions.Log.Write((object) string.Format("Warmup '{0} - {1} URLs' started at {2}", (object) title, (object) num1, (object) DateTime.UtcNow.ToLongDateTimeString()), TraceEventType.Information);
      foreach (WarmupUrl url in urls)
      {
        try
        {
          string publicDoamin;
          this.MakeRequest(this.ResolveLocalUrl(context.LocalUri, url.Url, out publicDoamin), publicDoamin, url.Url, url.VaryByUserAgent, context);
        }
        catch (Exception ex)
        {
          if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
            throw;
        }
      }
      stopwatch.Stop();
      double num2 = Math.Round(stopwatch.Elapsed.TotalSeconds);
      Telerik.Sitefinity.Abstractions.Log.Write((object) string.Format("Warmup '{0} - {1} URLs' finished in {2} seconds", (object) title, (object) num1, (object) num2), TraceEventType.Information);
      return num1;
    }

    private void MakeRequest(
      string url,
      string host,
      string liveUrl,
      bool varyByUserAgent,
      WarmupModule.WarmupContext context,
      bool retry = true)
    {
      List<string> source = new List<string>(context.UserAgents);
      if (!source.Any<string>())
        source.Add("Warmup module");
      foreach (string userAgent in context.UserAgents)
      {
        HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create(url);
        httpWebRequest.Host = host;
        httpWebRequest.Method = "GET";
        httpWebRequest.Timeout = context.RequestTimeout;
        httpWebRequest.Headers.Add("Warmup-Code", Bootstrapper.CurrentWarmupCode);
        httpWebRequest.UserAgent = userAgent;
        Stopwatch stopwatch = new Stopwatch();
        try
        {
          stopwatch.Start();
          try
          {
            using (HttpWebResponse response = (HttpWebResponse) httpWebRequest.GetResponse())
            {
              if (response.StatusCode == HttpStatusCode.OK)
              {
                if (!AppStatusService.IsValidServiceUri(response.ResponseUri))
                {
                  if (!(response.ResponseUri.AbsolutePath == "/sitefinity/status"))
                    goto label_17;
                }
                throw new WebException("Wrong warmup instance.");
              }
            }
          }
          finally
          {
            stopwatch.Stop();
          }
label_17:
          Telerik.Sitefinity.Abstractions.Log.Write((object) string.Format("The page '{0}' has been warmed up in {1} seconds with User-Agent '{2}'. Requested URL: {3}", (object) liveUrl, (object) stopwatch.Elapsed.TotalSeconds, (object) userAgent, (object) url), TraceEventType.Information);
        }
        catch (WebException ex)
        {
          if (retry)
          {
            this.MakeRequest(liveUrl, host, liveUrl, varyByUserAgent, context, false);
            break;
          }
          Telerik.Sitefinity.Abstractions.Log.Write((object) string.Format("The page '{0}' failed to warmup with error: {1}. Requested URL: {2}", (object) liveUrl, (object) ex.Message, (object) url), TraceEventType.Information);
          break;
        }
        catch (Exception ex)
        {
          if (!Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
            break;
          throw;
        }
        if (!varyByUserAgent)
          break;
      }
    }

    private IDictionary<string, WarmupUrl> GetUniqueUrls(
      ConfigElementDictionary<string, WarmupPluginElement> pluginsConfig)
    {
      Dictionary<string, WarmupUrl> uniqueUrls = new Dictionary<string, WarmupUrl>();
      foreach (WarmupPluginElement pluginElement in (IEnumerable<WarmupPluginElement>) pluginsConfig.Values.Where<WarmupPluginElement>((Func<WarmupPluginElement, bool>) (p => p.Enabled)).OrderByDescending<WarmupPluginElement, WarmupPriority>((Func<WarmupPluginElement, WarmupPriority>) (p => p.Priority)))
      {
        IWarmupPlugin plugin;
        if (this.TryResolvePlugin(pluginElement, out plugin))
        {
          foreach (WarmupUrl url in plugin.GetUrls())
          {
            if (!uniqueUrls.ContainsKey(url.Url) || uniqueUrls[url.Url].Priority < url.Priority)
              uniqueUrls[url.Url] = url;
          }
        }
      }
      return (IDictionary<string, WarmupUrl>) uniqueUrls;
    }

    /// <summary>Check if the warmup module is in use.</summary>
    /// <param name="plugins">The enabled plugins</param>
    /// <returns>Is Warmup module is in use</returns>
    private bool IsModuleInUse(IEnumerable<WarmupPluginElement> plugins)
    {
      bool flag = false;
      if (plugins.Any<WarmupPluginElement>())
      {
        foreach (WarmupPluginElement plugin in plugins)
        {
          Type c = TypeResolutionService.ResolveType(plugin.Type, false);
          if (c != (Type) null)
          {
            flag = true;
            if (typeof (SitemapPlugin).IsAssignableFrom(c))
              flag = this.CheckSumOfSiteMapPluginParametersIsLargerThenZero(plugin);
          }
          if (flag)
            break;
        }
      }
      return flag;
    }

    /// <summary>
    /// Check the sum of parameters values is larger than zero if the only plugin is the default one (Sitemap plugin)
    /// </summary>
    /// <param name="warmupPluginElement">The warmup plugin element</param>
    /// <returns>True if the sum of the sitemap parameters is larger then zero </returns>
    private bool CheckSumOfSiteMapPluginParametersIsLargerThenZero(
      WarmupPluginElement warmupPluginElement)
    {
      return this.GetParamIntValue(warmupPluginElement.Parameters, "maxPagesAfterInitializationPerSite") + this.GetParamIntValue(warmupPluginElement.Parameters, "maxPagesOnStartupPerSite") > 0;
    }

    private bool TryResolvePlugin(WarmupPluginElement pluginElement, out IWarmupPlugin plugin)
    {
      Type type = TypeResolutionService.ResolveType(pluginElement.Type, false);
      if (type != (Type) null)
      {
        plugin = (IWarmupPlugin) Activator.CreateInstance(type);
        plugin.Initialize(pluginElement.Name, new NameValueCollection(pluginElement.Parameters));
        return true;
      }
      plugin = (IWarmupPlugin) null;
      return false;
    }

    private int GetParamIntValue(NameValueCollection parameters, string key)
    {
      int result;
      int.TryParse(parameters[key], out result);
      return result;
    }

    private string ResolveLocalUrl(Uri localUri, string publicUrl, out string publicDoamin)
    {
      UriBuilder uriBuilder = new UriBuilder(publicUrl);
      publicDoamin = uriBuilder.Uri.Authority;
      uriBuilder.Scheme = localUri.Scheme;
      uriBuilder.Port = localUri.Port;
      uriBuilder.Host = localUri.Host;
      return uriBuilder.Uri.AbsoluteUri;
    }

    private void Bootstrapper_Bootstrapped(object sender, EventArgs e)
    {
      Bootstrapper.Bootstrapped -= new EventHandler<EventArgs>(this.Bootstrapper_Bootstrapped);
      this.Run();
    }

    private class WarmupContext
    {
      public WarmupContext(WarmupConfig config)
      {
        this.RequestTimeout = config.RequestTimeout;
        IEnumerable<string> source = config.UserAgents.Elements.Select<UserAgentElement, string>((Func<UserAgentElement, string>) (e => e.Value));
        if (source.Any<string>())
        {
          this.UserAgents = source;
        }
        else
        {
          HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
          this.UserAgents = (IEnumerable<string>) new string[1]
          {
            currentHttpContext != null ? currentHttpContext.Request.UserAgent : "Warmup module"
          };
        }
      }

      public int RequestTimeout { get; private set; }

      public Uri LocalUri => SystemManager.LocalUri;

      public IEnumerable<string> UserAgents { get; private set; }
    }
  }
}
