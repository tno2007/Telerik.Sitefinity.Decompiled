// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Webhooks.WebhookModule
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.UsageTracking.TrackingReporters;
using Telerik.Sitefinity.Webhooks.Configuration;
using Telerik.Sitefinity.Webhooks.UsageTracking;

namespace Telerik.Sitefinity.Webhooks
{
  internal class WebhookModule : ModuleBase, ITrackingReporter
  {
    internal const string ModuleName = "WebhookModule";
    private WebhookHandler handler;

    public override Guid LandingPageId { get; }

    public override Type[] Managers { get; }

    public override void Install(SiteInitializer initializer)
    {
    }

    public override void Initialize(ModuleSettings settings)
    {
      base.Initialize(settings);
      App.WorkWith().Module(nameof (WebhookModule)).Initialize().Localization<WebhookResources>().Configuration<WebhookConfig>();
    }

    public override void Load()
    {
      base.Load();
      this.Handler.Initialize();
    }

    public override void Unload()
    {
      base.Unload();
      this.Handler.Destroy();
    }

    public override void Uninstall(SiteInitializer initializer)
    {
      base.Uninstall(initializer);
      this.Handler.Destroy();
    }

    protected override ConfigSection GetModuleConfig() => (ConfigSection) Config.Get<WebhookConfig>();

    public object GetReport()
    {
      WebhookConfig webhookConfig = Config.Get<WebhookConfig>();
      WebhookModuleReport report = new WebhookModuleReport()
      {
        ModuleName = nameof (WebhookModule),
        EventsCount = webhookConfig.Events.Values.Where<WebhookEventConfigElement>((Func<WebhookEventConfigElement, bool>) (e => e.Urls.Count > 0)).Count<WebhookEventConfigElement>(),
        Events = new Dictionary<string, int>()
      };
      foreach (WebhookEventConfigElement eventConfigElement in (IEnumerable<WebhookEventConfigElement>) webhookConfig.Events.Values)
        report.Events.Add(eventConfigElement.EventType, eventConfigElement.Urls.Count);
      return (object) report;
    }

    private WebhookHandler Handler
    {
      get
      {
        if (this.handler == null)
          this.handler = new WebhookHandler();
        return this.handler;
      }
    }
  }
}
