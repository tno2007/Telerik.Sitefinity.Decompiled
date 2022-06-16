// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContextualHelp.Http.ContextualHelpHttpHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Concurrent;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.ContextualHelp.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.ContextualHelp.Http
{
  /// <summary>HTTP handler for contextual help route requests.</summary>
  /// <seealso cref="T:System.Web.IHttpHandler" />
  internal class ContextualHelpHttpHandler : IHttpHandler
  {
    private const string ConfigPath = "Telerik.Sitefinity.ContextualHelp.Configuration.tooltip_config.json";
    private string scriptBody;
    private string config;
    private ConcurrentDictionary<string, string> serviceContentCache = new ConcurrentDictionary<string, string>();
    private readonly IResponseHandler responseHandler;
    private static HtmlLink elementsCssControl;
    private static readonly string ContextualHelpRouteName = "ContextualHelp";
    public const string ContextualHelpKey = "enable-contextual-help";
    private const string ServiceScriptName = "Telerik.Sitefinity.ContextualHelp.Scripts.tooltip-service.js";
    private const string ModuleName = "ContextualHelp";
    private const string ContextualHelpRouteUrl = "contextual-help";
    private const string ScriptTemplate = "<script type=\"text/javascript\">//<![CDATA[\r\n{0}\r\n//]]></script>";

    /// <summary>
    /// Initializes static members of the <see cref="T:Telerik.Sitefinity.ContextualHelp.Http.ContextualHelpHttpHandler" /> class.
    /// </summary>
    static ContextualHelpHttpHandler() => ContextualHelpHttpHandler.Enabled = true;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.ContextualHelp.Http.ContextualHelpHttpHandler" /> class.
    /// </summary>
    public ContextualHelpHttpHandler() => this.responseHandler = ObjectFactory.Resolve<IResponseHandler>();

    /// <summary>
    /// Gets or sets a value indicating whether the contextual help is enabled
    /// </summary>
    internal static bool Enabled { get; set; }

    /// <summary>
    /// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler" /> instance.
    /// </summary>
    public bool IsReusable => true;

    /// <summary>
    /// Enables processing of HTTP Web requests by a custom <see langword="HttpHandler" /> that implements the <see cref="T:System.Web.IHttpHandler" /> interface.
    /// </summary>
    /// <param name="context">An <see cref="T:System.Web.HttpContext" /> object that provides references to the intrinsic server objects (for example, <see langword="Request" />, <see langword="Response" />, <see langword="Session" />, and <see langword="Server" />) used to service HTTP requests.</param>
    public void ProcessRequest(HttpContext context)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      string resolvedServiceContent = this.GetResolvedServiceContent();
      this.responseHandler.Configure(context.Request, context.Response, resolvedServiceContent).AddCaching().AddGZip().Send();
    }

    private string GetResolvedServiceContent()
    {
      if (!Telerik.Sitefinity.Configuration.Config.Get<ContextualHelpConfig>().Enabled || !ContextualHelpHttpHandler.Enabled)
        return "[]";
      string key = SystemManager.CurrentHttpContext.Request.Url.ToString();
      string empty = string.Empty;
      if (this.serviceContentCache.TryGetValue(key, out empty))
        return empty;
      string newValue1 = VirtualPathUtility.AppendTrailingSlash(VirtualPathUtility.ToAbsolute("~/" + "RestApi" + "/contextual-help/get-tooltips"));
      string newValue2 = VirtualPathUtility.AppendTrailingSlash(VirtualPathUtility.ToAbsolute("~/" + "RestApi" + "/contextual-help/mark-tooltips"));
      string content = this.AddImagesToContent(this.ScriptBody.Replace("{{GET_DATA_ROUTE}}", newValue1).Replace("{{MARK_TOOLTIPS_ROUTE}}", newValue2).Replace("\"{{TOOLTIP_CONFIG}}\"", this.Config));
      this.serviceContentCache.TryAdd(key, content);
      return content;
    }

    private string AddImagesToContent(string content)
    {
      string content1 = content;
      MatchCollection matchCollection = new Regex("src\\=\\\\\\\"{{([\\w.]*)}}\\\\\\\"").Matches(content);
      for (int i = 0; i < matchCollection.Count; ++i)
      {
        Match match = matchCollection[i];
        string newValue = UrlPath.ResolveAbsoluteUrlWithoutNonDefaultUrlSettings(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "~/SFRes/images/Telerik.Sitefinity.Resources/{0}.png", (object) match.Groups[1].Value), SystemManager.CurrentHttpContext.Request.Url.Host);
        content1 = content1.Replace(string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{{{{{0}}}}}", (object) match.Groups[1].Value), newValue);
      }
      return content1;
    }

    private string GetEmbeddedResource(string resourcePath)
    {
      using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath))
      {
        using (StreamReader streamReader = new StreamReader(manifestResourceStream))
          return streamReader.ReadToEnd();
      }
    }

    private string Config
    {
      get
      {
        if (this.config == null)
          this.config = this.GetEmbeddedResource("Telerik.Sitefinity.ContextualHelp.Configuration.tooltip_config.json");
        return this.config;
      }
    }

    private string ScriptBody
    {
      get
      {
        if (this.scriptBody == null)
          this.scriptBody = this.GetEmbeddedResource("Telerik.Sitefinity.ContextualHelp.Scripts.tooltip-service.js");
        return this.scriptBody;
      }
    }
  }
}
