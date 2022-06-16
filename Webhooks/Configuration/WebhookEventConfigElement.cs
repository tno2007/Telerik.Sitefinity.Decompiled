// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Webhooks.Configuration.WebhookEventConfigElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Services.Events;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Webhooks.Configuration
{
  internal class WebhookEventConfigElement : ConfigElement
  {
    public WebhookEventConfigElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets or sets the post url for the event.</summary>
    [ConfigurationProperty("eventType", IsKey = true)]
    [ObjectInfo(typeof (WebhookResources), Description = "EventTypeDescription", Title = "EventTypeTitle")]
    public string EventType
    {
      get => (string) this["eventType"];
      set => this["eventType"] = this.IsEventTypeValid(value) ? (object) value : throw new ArgumentException(string.Format(Res.Get<WebhookResources>().EventTypeErrorMessage, (object) value));
    }

    /// <summary>Gets the urls for the event.</summary>
    [ConfigurationProperty("urls")]
    [ObjectInfo(typeof (WebhookResources), Description = "UrlsDescription", Title = "UrlsTitle")]
    public ConfigElementDictionary<string, WebhookUrlConfigElement> Urls => (ConfigElementDictionary<string, WebhookUrlConfigElement>) this["urls"];

    private bool IsEventTypeValid(string typeName)
    {
      if (!string.IsNullOrEmpty(typeName))
      {
        Type c = TypeResolutionService.ResolveType(typeName);
        if (c != (Type) null && c.IsPublic && typeof (IEvent).IsAssignableFrom(c))
          return c.IsInterface;
      }
      return false;
    }
  }
}
