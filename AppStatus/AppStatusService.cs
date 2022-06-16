// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.AppStatus.AppStatusService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Script.Serialization;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.AppStatus
{
  /// <summary>
  /// Service which displays app status page during initializing and upgrading of the application.
  /// </summary>
  internal static class AppStatusService
  {
    internal const string AppStatusUrlKey = "sf:appStatusUrl";
    internal const string DefaultAppStatusUrl = "/sitefinity/status";
    internal const string AppStatusReportResourceName = "Telerik.Sitefinity.AppStatus.appStatusReport.js";
    internal const string AppStatusSimpleResourceName = "Telerik.Sitefinity.AppStatus.appStatusSimple.js";
    internal const string AngularResourceName = "Telerik.Sitefinity.Resources.Scripts.AngularJS.angular-1.6.6.min.js";
    internal const string SitefinityAssemblyName = "Telerik.Sitefinity";
    internal const string ResourceAssemblyName = "Telerik.Sitefinity.Resources";
    private static string appStatusServiceUrl;
    private static string appStatusFullScriptUrl;
    private static string appStatusSimpleScriptUrl;
    private static string appStatusAngularJsScriptUrl;
    private static string appStatePageSource = "<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html ng-app=\"appStatusApp\">\r\n<head>\r\n<link href=\"//fonts.googleapis.com/css?family=Open+Sans:300italic,400italic,600italic,700italic,800italic,400,300,600,700,800\" rel=\"stylesheet\" type=\"text/css\" />\r\n<link href=\"//fonts.googleapis.com/css?family=Open+Sans+Condensed:300,300italic,700\" rel=\"stylesheet\" type=\"text/css\" />\r\n<script src=\"//ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js\"></script>\r\n<script type=\"text/javascript\">{0}</script>\r\n<script type=\"text/javascript\">{1}</script>  \r\n<style>\r\n    body{{\r\n\t    font: 13px \"Open Sans\", Arial;\r\n\t    height: 100%;\r\n\t    margin: 0;\r\n    }}\r\n   \r\n    h1{{\r\n\t    font-size: 18px;\r\n    }}\r\n    a{{\r\n\t    color: #105AB5;\r\n\t\ttext-decoration: none;\r\n    }}\r\n    a:hover{{\r\n\t    color: #384460;\r\n    }}\r\n\r\n\t.upgrade{{\r\n\t\twidth: 100%;\r\n\t\tposition: absolute;\r\n\t\theight: 100%;\r\n\t\toverflow: hidden;\r\n\t\ttransition: 0.2s;\r\n\t}}\r\n\t.upgrade.view-details {{\r\n\t\toverflow-y: scroll;\r\n\t\tbackground: #f2f2f2;\t\r\n\t}}\r\n\t.upgrade.view-details .holder{{\r\n\t\tposition: static;\r\n\t\toverflow: visible;\r\n\t}}\r\n\t.holder{{\r\n\t\twidth: 100%;\r\n\t\theight: 100%;\r\n\t\toverflow: hidden;\r\n\t\tposition: absolute;\r\n\t\ttransition: 0.2s;\r\n\t}}\r\n\r\n    .logo{{\r\n\t    width: 30px;\r\n\t    height: auto;\r\n\t    top: 30px;\r\n\t    left: 30px;\r\n\t    position: fixed;\r\n    }}\r\n    .process{{\r\n\t    max-width: 500px;\r\n\t    margin: -150px auto 0 auto;\r\n\t    top: 50%;\r\n\t    position: relative;\r\n\t    text-align: center;\r\n\t    transition: 0.3s;\r\n    }}\r\n    .process img{{\r\n\t    width: 200px;\r\n    }}\r\n    .version{{\r\n\t    font-size: 13px;\r\n\t    color: #666;\r\n    }}\r\n\t.operation{{\r\n\t\twidth: 100%;\r\n\t    overflow: hidden;\r\n\t\twhite-space: nowrap;\r\n\t\ttext-overflow: ellipsis;\r\n\t}}\r\n\r\n    .details{{\r\n\t    font-size: 15px;\r\n\t    max-width: 600px;\r\n\t    margin: 0 auto;\r\n\t    padding: 40px;\r\n\t    top: 100%;\r\n\t    position: relative;\r\n\t    border-radius: 4px 4px 0 0;\r\n\t    background: rgba(255,255,255,0.9);\r\n\t    z-index: 9;\r\n\t    transition: 0.3s;\r\n    }}\r\n    .details h1{{\r\n\t    margin: 0;\r\n    }}\r\n    .details a{{\r\n\t    font-size: 13px;\r\n    }}\r\n    .details p{{\r\n\t    margin-top: 5px;\r\n    }}\r\n\t.details .btn{{\r\n\t\tmargin-right: 10px;\r\n\t}}\r\n    table{{\r\n\t    font-size: 13px;\r\n\t    margin: 80px -40px 0;\r\n\t    border-collapse: collapse;\r\n\t    border-bottom: 1px solid #E4E4E4;\r\n    }}\r\n    table tr td{{\r\n\t    padding: 15px 40px;\r\n\t    vertical-align: top;\r\n\t    border-top: 1px solid #E4E4E4;\r\n\t    transition: 0.2s;\r\n    }}\r\n    table tr td:last-child{{\r\n\t    text-align: right;\r\n    }}\r\n    table tr:hover td{{\r\n\t    background: #f5f5f5;\r\n    }}\r\n    .hide-details{{\r\n\t    float: right;\r\n    }}\r\n\r\n    .view-details .details{{\r\n\t    display: block;\r\n\t    top: 0;\r\n    }}\r\n    .view-details .process{{\r\n\t    top: 50px;\r\n\t    margin-top: 0;\r\n    }}\r\n    .view-details .process h1,\r\n    .view-details .process p,\r\n    .view-details .process a,\r\n\t.view-details .process .warning-indicator,\r\n\t.view-details .process .error-indicator{{\r\n\t\tdisplay: none;\r\n\t}}\r\n\t\r\n\t.user{{\r\n\t\tcolor: #666;\r\n\t}}\r\n\t.user svg{{\r\n\t\twidth: 100px;\r\n\t\theight: 100px;\r\n\t}}\r\n\r\n\ta.btn{{\r\n\t\tfont-size: 15px;\r\n\t\tmargin-bottom: 7px;\t\r\n\t\tpadding: 6px 15px;\r\n\t\tdisplay: inline-block;\r\n\t\ttext-decoration: none;\r\n\t\tcolor: #222;\r\n\t\tborder-radius: 3px;\r\n\t\tborder: 1px solid #384460;\r\n\t\ttransition: 0.2s;\r\n\t}}\r\n\t.btn:hover{{\r\n\t\tbackground: #E1E5EA;\r\n\t}}\r\n\r\n    .warning-indicator,\r\n    .error-indicator{{\r\n\t    padding: 0px 8px;\r\n\t    line-height: 22px;\r\n\t    display: inline-block;\r\n\t    text-align: center;\r\n\t    color: #000;\r\n\t    border-radius: 12px;\r\n\t    background: yellow;\r\n    }}\r\n    .error-indicator{{\r\n\t    background: #FF9494;\r\n    }}\r\n    .error-details{{\r\n\t    font-size: 13px;\r\n\t    margin-top: 20px;\r\n    }}    \r\n\t.error-details div{{\r\n\t\tmargin-right: 15px;\r\n\t\tdisplay: inline-block;\r\n\t}}\r\n    table .error td{{\r\n\t\tbackground: #FFCCCC;\r\n\t}}\r\n    table .error:hover td{{\r\n\t    background: #FFBFBF;\r\n    }}\r\n    table .warning td{{\r\n\t\tbackground: #FFFFCC;\r\n\t}}\r\n    table .warning:hover td{{\r\n\t    background: #FFFFAA;\r\n    }}\r\n    table .warning-indicator{{\r\n\t    left: -85px;\r\n        position: absolute;\r\n    }}\r\n    table .error-indicator{{\r\n\t    left: -65px;\r\n        position: absolute;\r\n    }}\r\n\r\n\t.log-item{{\r\n\t\twidth: 465px;\r\n\t\tword-wrap: break-word;\r\n\t}}\r\n\t.long-text.log-item{{\t\t\r\n\t\tmax-height: 50px;\r\n\t\tmax-height: 100px;\r\n\t\toverflow: hidden;\r\n\t\tposition: relative;\r\n\t\tmargin-bottom: 5px;\r\n\t\ttransition: 0.2s;\r\n\t}}\t\r\n\t.long-text.log-item:before {{\r\n\t    bottom: 0;\r\n\t    content: \";\r\n\t    display: block;\r\n\t    height: 50px;\r\n\t    position: absolute;\r\n\t    width: 100%;\r\n\t    background: linear-gradient(to bottom, rgba(255,255,255,0), rgba(255,255,255,1));\r\n\t}}\r\n\t.long-text.active{{\r\n\t\tmax-height: none;\r\n\t}}\r\n\t.long-text.active:before{{\r\n\t\tdisplay: none;\r\n\t}}\r\n\t\r\n\ttable tr:hover .long-text.log-item:before{{\r\n\t\tbackground: linear-gradient(to bottom, rgba(245,245,245,0), rgba(245,245,245,1));\r\n\t}}\r\n\ttable tr.warning .long-text.log-item:before{{\r\n\t\tbackground: linear-gradient(to bottom, rgba(255,255,204,0), rgba(255,255,204,1));\r\n\t}}\r\n\ttable tr.warning:hover .long-text.log-item:before{{\r\n\t\tbackground: linear-gradient(to bottom, rgba(255,255,170,0), rgba(255,255,170,1));\r\n\t}}\r\n\ttable tr.error .long-text.log-item:before{{\r\n\t\tbackground: linear-gradient(to bottom, rgba(255,204,204,0), rgba(255,204,204,1));\r\n\t}}\r\n\ttable tr.error:hover .long-text.log-item:before{{\r\n\t\tbackground: linear-gradient(to bottom, rgba(255,191,191,0), rgba(255,191,191,1));\r\n\t}}\r\n\r\n\t@media (max-width: 640px) {{\r\n\t\tbody{{\r\n\t\t}}\r\n\t\th1{{\r\n\t\t\tfont-size: 16px;\r\n\t\t\tfont-weight: normal;\r\n\t\t}}\r\n\t\t.logo,\r\n\t\t.process img{{\r\n\t\t\tdisplay: none;\r\n\t\t}}\r\n\t\t.process{{\r\n\t\t\tmargin-top: -40px;\r\n\t\t\tcolor: #888;\r\n\t\t}}\r\n\t}}\r\n</style>\r\n<base href=\"/\">\r\n</head>\r\n<body >\r\n{2}\r\n<input type=\"hidden\" id=\"applicationVirtualPath\" value=\"{3}\" />\r\n</body>\r\n</html>";

    /// <summary>
    /// Initializes static members of the <see cref="T:Telerik.Sitefinity.AppStatus.AppStatusService" /> class.
    /// </summary>
    static AppStatusService() => AppStatusService.InitializeStaticUrls();

    /// <summary>Checks if the app status service is enabled.</summary>
    /// <returns>Whether the app status service is enabled.</returns>
    public static bool IsEnabled()
    {
      bool flag = true;
      string appSetting = ConfigurationManager.AppSettings["sf:AppStatusPageMode"];
      AppStatusPageMode result;
      if (!string.IsNullOrWhiteSpace(appSetting) && Enum.TryParse<AppStatusPageMode>(appSetting, true, out result))
        flag = result != AppStatusPageMode.Disabled;
      return flag;
    }

    /// <summary>Checks if the request is app status service request.</summary>
    /// <param name="context">Http context.</param>
    /// <returns>Whether the request is app status service request.</returns>
    public static bool IsValidServiceRequest(HttpContext context) => AppStatusService.IsValidServiceUri(context.Request.Url);

    /// <summary>Gets response for the app status service request.</summary>
    /// <param name="context">Http context.</param>
    public static void GetServiceResponse(HttpContext context)
    {
      AppStatusService.SetServiceCallResponseSettings(context);
      if (AppStatusService.IsVersioningInfoRequest(context.Request))
      {
        context.Response.Write(AppStatusService.GetAppVersioningInfo());
      }
      else
      {
        int countValue = AppStatusService.GetCountValue(context.Request);
        bool adminMode = AppStatusService.IsAppStatusInAdminMode(context);
        context.Response.Write(AppStatusService.GetAppStatus(countValue, adminMode));
      }
      context.ApplicationInstance.CompleteRequest();
    }

    /// <summary>
    /// Returns (out) the default html and script URL for the static report page.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    /// <param name="html">The returned (out) HTML.</param>
    /// <param name="scriptUrl">The returned (out) script URL.</param>
    public static void GetDefaultPageResources(
      HttpContext context,
      out string html,
      out string scriptUrl)
    {
      if (!AppStatusService.IsBrowserSupported(context.Request))
      {
        string path = context.Server.MapPath("~/App_Data/appStatusSimple.html");
        if (File.Exists(path))
        {
          html = File.ReadAllText(path);
        }
        else
        {
          using (StreamReader streamReader = new StreamReader(typeof (AppStatusService).Assembly.GetManifestResourceStream("Telerik.Sitefinity.AppStatus.appStatusSimple.html")))
            html = streamReader.ReadToEnd();
          File.WriteAllText(path, html);
        }
        scriptUrl = AppStatusService.appStatusSimpleScriptUrl;
      }
      else
      {
        string path = context.Server.MapPath("~/App_Data/appStatusReport.html");
        if (File.Exists(path))
        {
          html = File.ReadAllText(path);
        }
        else
        {
          html = AppStatusService.GetDefaultAppStatusPageContent();
          File.WriteAllText(path, html);
        }
        scriptUrl = AppStatusService.appStatusFullScriptUrl;
      }
    }

    /// <summary>
    /// Displays a static HTML page to the client with option to refer a script file. Completes the request.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    /// <param name="bodyHtml">The body HTML.</param>
    /// <param name="scriptUrl">Optional script url to be referred by the page.</param>
    public static void DisplayStaticPage(HttpContext context, string bodyHtml, string scriptUrl = null)
    {
      string appStatePageSource = AppStatusService.appStatePageSource;
      string scriptContent = AppStatusService.GetScriptContent(scriptUrl);
      string s;
      if (AppStatusService.IsBrowserSupported(context.Request))
      {
        string resourceContent = AppStatusService.GetResourceContent("Telerik.Sitefinity.Resources", "Telerik.Sitefinity.Resources.Scripts.AngularJS.angular-1.6.6.min.js");
        s = string.Format(appStatePageSource, (object) resourceContent, (object) scriptContent, (object) bodyHtml, (object) (HostingEnvironment.ApplicationVirtualPath.TrimEnd('/') ?? string.Empty));
      }
      else
        s = string.Format(appStatePageSource, (object) string.Empty, (object) scriptContent, (object) bodyHtml, (object) (HostingEnvironment.ApplicationVirtualPath.TrimEnd('/') ?? string.Empty));
      context.Response.CacheControl = "no-cache";
      context.Response.AddHeader("Content-Type", "text/html; charset=" + context.Response.Charset);
      context.Response.Write(s);
      context.Response.StatusCode = int.Parse(ConfigurationManager.AppSettings["sf:AppStatusPageResponseCode"] ?? "200");
      context.ApplicationInstance.CompleteRequest();
    }

    internal static bool IsValidServiceUri(Uri uri) => uri.AbsolutePath == AppStatusService.appStatusServiceUrl;

    internal static string GetResourceContent(string typeName, string resourceName)
    {
      string empty = string.Empty;
      using (StreamReader streamReader = new StreamReader(Assembly.Load(typeName).GetManifestResourceStream(resourceName)))
        return streamReader.ReadToEnd();
    }

    internal static void InitializeStaticUrls()
    {
      AppStatusService.appStatusServiceUrl = (HostingEnvironment.ApplicationVirtualPath.TrimEnd('/') ?? string.Empty) + "/appstatus";
      AppStatusService.appStatusFullScriptUrl = (HostingEnvironment.ApplicationVirtualPath.TrimEnd('/') ?? string.Empty) + "/fulljs";
      AppStatusService.appStatusSimpleScriptUrl = (HostingEnvironment.ApplicationVirtualPath.TrimEnd('/') ?? string.Empty) + "/simplejs";
      AppStatusService.appStatusAngularJsScriptUrl = (HostingEnvironment.ApplicationVirtualPath.TrimEnd('/') ?? string.Empty) + "/angularjs";
    }

    internal static string GetAppStatusPageRelativeUrl() => WebConfigurationManager.AppSettings["sf:appStatusUrl"] ?? "/sitefinity/status";

    internal static string GetDefaultAppStatusPageContent()
    {
      using (StreamReader streamReader = new StreamReader(typeof (AppStatusService).Assembly.GetManifestResourceStream("Telerik.Sitefinity.AppStatus.appStatusReport.html")))
        return streamReader.ReadToEnd();
    }

    private static void SetServiceCallResponseSettings(HttpContext context)
    {
      context.Response.StatusCode = 200;
      context.Response.CacheControl = "no-cache";
      context.Response.AddHeader("Content-Type", "application/json; charset=" + context.Response.Charset);
    }

    private static bool IsVersioningInfoRequest(HttpRequest request) => string.Equals(request.QueryString["info"], "versioning");

    private static bool IsBrowserSupported(HttpRequest request)
    {
      HttpBrowserCapabilities browser = request.Browser;
      return !(browser.Browser == "IE") || Convert.ToDouble(browser.Version) > 9.0;
    }

    private static int GetCountValue(HttpRequest request)
    {
      int result;
      return int.TryParse(request.QueryString["count"], out result) ? result : 0;
    }

    private static string GetAppVersioningInfo() => new JavaScriptSerializer().Serialize((object) new
    {
      Current = Assembly.GetExecutingAssembly().GetName().Version.ToString()
    });

    private static string GetAppStatus(int startIndex, bool adminMode)
    {
      JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
      AppStatusDto appStatusDto1 = new AppStatusDto()
      {
        State = "Running",
        AdminMode = adminMode
      };
      if (adminMode)
      {
        IEnumerable<AppStatusEntry> collection = SystemManager.AppStatusBuffer.BatchTake<AppStatusEntry>(startIndex, 20);
        appStatusDto1.Info = new List<AppStatusEntry>(collection);
      }
      if (SystemManager.IsUpgrading && AppStatusService.GetBuildVersion() > 0)
        appStatusDto1.State = "Upgrading";
      else if (!Bootstrapper.FinalEventsExecuted)
        appStatusDto1.State = "Initializing";
      AppStatusDto appStatusDto2 = appStatusDto1;
      return scriptSerializer.Serialize((object) appStatusDto2);
    }

    private static bool IsAppStatusInAdminMode(HttpContext context)
    {
      if (WebConfigurationManager.GetSection("system.web/customErrors") is CustomErrorsSection section && section.Mode == CustomErrorsMode.Off)
        return true;
      return section != null && section.Mode == CustomErrorsMode.RemoteOnly && context.Request.IsLocal;
    }

    private static int GetBuildVersion()
    {
      int buildVersion = 0;
      try
      {
        ModuleVersion moduleVersion = MetadataManager.GetManager().GetModuleVersion("Sitefinity");
        if (moduleVersion != null)
        {
          if (moduleVersion.PreviousVersion != (Version) null)
            buildVersion = moduleVersion.PreviousVersion.Build;
        }
      }
      catch (Exception ex)
      {
        Log.Write((object) string.Format("App status page can not get previous build version. Exception: {0}", (object) ex), ConfigurationPolicy.ErrorLog);
      }
      return buildVersion;
    }

    private static string GetScriptContent(string scriptUrl)
    {
      string scriptContent = string.Empty;
      if (scriptUrl == AppStatusService.appStatusFullScriptUrl)
        scriptContent = AppStatusService.GetResourceContent("Telerik.Sitefinity", "Telerik.Sitefinity.AppStatus.appStatusReport.js");
      else if (scriptUrl == AppStatusService.appStatusSimpleScriptUrl)
        scriptContent = AppStatusService.GetResourceContent("Telerik.Sitefinity", "Telerik.Sitefinity.AppStatus.appStatusSimple.js");
      return scriptContent;
    }
  }
}
