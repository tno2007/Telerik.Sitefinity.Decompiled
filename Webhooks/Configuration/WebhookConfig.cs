// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Webhooks.Configuration.WebhookConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Webhooks.Configuration
{
  [ObjectInfo(typeof (WebhookResources), Description = "WebhookConfigDescription", Title = "WebhookConfigTitle")]
  internal class WebhookConfig : ConfigSection
  {
    /// <summary>Gets the web hook events</summary>
    [ObjectInfo(typeof (WebhookResources), Description = "EventsDescription", Title = "EventsTitle")]
    [ConfigurationProperty("events")]
    public ConfigElementDictionary<string, WebhookEventConfigElement> Events => (ConfigElementDictionary<string, WebhookEventConfigElement>) this["events"];
  }
}
