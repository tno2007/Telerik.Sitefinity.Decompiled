// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContextualHelp.ContextualHelpModule
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using ServiceStack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using System.Web.Routing;
using System.Web.UI;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.ContextualHelp.Configuration;
using Telerik.Sitefinity.ContextualHelp.Http;
using Telerik.Sitefinity.ContextualHelp.Web.Services;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Events;
using Telerik.Sitefinity.Web.Events;

namespace Telerik.Sitefinity.ContextualHelp
{
  /// <summary>Contextual help and on-boarding tooltips manager</summary>
  internal class ContextualHelpModule : ModuleBase
  {
    public const string ContextualHelpKey = "enable-contextual-help";
    public const string ContextualHelpRouteUrl = "contextual-help";
    public const string ContextualHelpPreferenceKey = "contextual-help";
    private const string ConfigUrl = "~/Res/Telerik.Sitefinity.ContextualHelp.Scripts.tooltip-config.js";
    private static readonly string ServiceUrl = "~/contextual-help";
    private static readonly ScriptReference ElementsRef = new ScriptReference();
    private static readonly ScriptReference ConfigRef = new ScriptReference("~/Res/Telerik.Sitefinity.ContextualHelp.Scripts.tooltip-config.js");
    private static readonly ScriptReference ServiceRef = new ScriptReference(ContextualHelpModule.ServiceUrl);
    private const string ModuleName = "ContextualHelp";
    private const string ContextualHelpRouteName = "ContextualHelp";
    private const string ScriptTemplate = "<script type=\"text/javascript\">//<![CDATA[\r\n{0}\r\n//]]></script>";

    /// <summary>
    /// Gets the landing page id for each module inherit from <see cref="T:Telerik.Sitefinity.Services.SecuredModuleBase" /> class.
    /// </summary>
    /// <value>The landing page id.</value>
    public override Guid LandingPageId { get; }

    /// <summary>
    /// Gets the CLR types of all data managers provided by this module.
    /// </summary>
    /// <value>
    /// An array of <see cref="T:System.Type" /> objects.
    /// </value>
    public override Type[] Managers { get; }

    /// <summary>
    /// Installs this module in Sitefinity system for the first time.
    /// </summary>
    /// <param name="initializer">The Site Initializer. A helper class for installing Sitefinity modules.</param>
    public override void Install(SiteInitializer initializer)
    {
    }

    /// <summary>Initializes the service with specified settings.</summary>
    /// <param name="settings">The settings.</param>
    public override void Initialize(ModuleSettings settings)
    {
      base.Initialize(settings);
      App.WorkWith().Module("ContextualHelp").Initialize().Localization<ContextualHelpResources>().Configuration<ContextualHelpConfig>().Route("ContextualHelp", (RouteBase) new Route("contextual-help", (IRouteHandler) new ContextualHelpRouteHandler())).ServiceStackPlugin((IPlugin) new ServiceStackPlugin());
    }

    /// <summary>
    /// Loads the module dependencies after the module has been initialized and installed.
    /// TODO: Uncomment when we need to have contextual help in Sitefinity pages
    /// </summary>
    public override void Load()
    {
      base.Load();
      EventHub.Subscribe<IPagePreRenderCompleteEvent>(new SitefinityEventHandler<IPagePreRenderCompleteEvent>(this.OnPagePreRenderCompleteEventHandler));
    }

    public override void Unload()
    {
      base.Unload();
      EventHub.Unsubscribe<IPagePreRenderCompleteEvent>(new SitefinityEventHandler<IPagePreRenderCompleteEvent>(this.OnPagePreRenderCompleteEventHandler));
    }

    /// <summary>Gets the module config.</summary>
    /// <returns>The module config.</returns>
    protected override ConfigSection GetModuleConfig() => (ConfigSection) Config.Get<ContextualHelpConfig>();

    private void OnPagePreRenderCompleteEventHandler(IPagePreRenderCompleteEvent evt)
    {
      if (evt == null || evt.PageSiteNode == null || evt.Page == null || !evt.PageSiteNode.IsBackend || !Config.Get<ContextualHelpConfig>().Enabled)
        return;
      IDictionary items = SystemManager.CurrentHttpContext.Items;
      if (items.Contains((object) "enable-contextual-help") && !(bool) items[(object) "enable-contextual-help"])
        return;
      ScriptManager current = ScriptManager.GetCurrent(evt.Page);
      if (current == null)
        return;
      if (!current.Scripts.Contains(ContextualHelpModule.ServiceRef))
        current.Scripts.Add(ContextualHelpModule.ServiceRef);
      if (!current.Scripts.Contains(ContextualHelpModule.ElementsRef))
      {
        string virtualPath = "~/adminapp/";
        FileInfo fileInfo = ((IEnumerable<string>) Directory.GetFiles(HostingEnvironment.MapPath(virtualPath), "elements.*.bundle.js")).Select<string, FileInfo>((Func<string, FileInfo>) (f => new FileInfo(f))).OrderByDescending<FileInfo, DateTime>((Func<FileInfo, DateTime>) (f => f.LastWriteTimeUtc)).FirstOrDefault<FileInfo>();
        if (fileInfo != null)
          ContextualHelpModule.ElementsRef.Path = virtualPath + fileInfo.Name;
        if (!string.IsNullOrEmpty(ContextualHelpModule.ElementsRef.Path))
          current.Scripts.Add(ContextualHelpModule.ElementsRef);
      }
      if (current.Scripts.Contains(ContextualHelpModule.ConfigRef))
        return;
      current.Scripts.Add(ContextualHelpModule.ConfigRef);
    }
  }
}
