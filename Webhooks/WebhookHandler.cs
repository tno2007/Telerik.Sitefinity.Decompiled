// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Webhooks.WebhookHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Events;
using Telerik.Sitefinity.Utilities;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Webhooks.Configuration;

namespace Telerik.Sitefinity.Webhooks
{
  internal class WebhookHandler
  {
    private static readonly ConcurrentProperty<HashSet<Type>> WebhookEventsCache = new ConcurrentProperty<HashSet<Type>>(new Func<HashSet<Type>>(WebhookHandler.BuildPropertyCache));

    internal void Initialize()
    {
      if (Config.Get<WebhookConfig>().Events.Values.Any<WebhookEventConfigElement>())
      {
        EventHub.Subscribe<IEvent>(new SitefinityEventHandler<IEvent>(this.OnEventRaised));
        WebhookHandler.WebhookEventsCache.Reset();
      }
      CacheDependency.Subscribe(typeof (WebhookConfig), new ChangedCallback(this.ConfigChangedCallback));
    }

    internal void Destroy()
    {
      CacheDependency.Unsubscribe(typeof (WebhookConfig), new ChangedCallback(this.ConfigChangedCallback));
      EventHub.Unsubscribe<IEvent>(new SitefinityEventHandler<IEvent>(this.OnEventRaised));
    }

    private static HashSet<Type> BuildPropertyCache()
    {
      HashSet<Type> typeSet = new HashSet<Type>();
      foreach (WebhookEventConfigElement eventConfigElement in (IEnumerable<WebhookEventConfigElement>) Config.Get<WebhookConfig>().Events.Values)
      {
        if (!string.IsNullOrEmpty(eventConfigElement.EventType))
        {
          Type type = TypeResolutionService.ResolveType(eventConfigElement.EventType);
          if (type != (Type) null)
            typeSet.Add(type);
        }
      }
      return typeSet;
    }

    private void ConfigChangedCallback(
      ICacheDependencyHandler caller,
      Type trackedItemType,
      string trackedItemKey)
    {
      if (Config.Get<WebhookConfig>().Events.Values.Any<WebhookEventConfigElement>())
      {
        EventHub.Unsubscribe<IEvent>(new SitefinityEventHandler<IEvent>(this.OnEventRaised));
        EventHub.Subscribe<IEvent>(new SitefinityEventHandler<IEvent>(this.OnEventRaised));
        WebhookHandler.WebhookEventsCache.Reset();
      }
      else
        EventHub.Unsubscribe<IEvent>(new SitefinityEventHandler<IEvent>(this.OnEventRaised));
    }

    private void OnEventRaised(IEvent webhookEvent)
    {
      if (!Bootstrapper.IsReady)
        return;
      Type webhookEventType = webhookEvent.GetType();
      if (!WebhookHandler.WebhookEvents.Overlaps(EventTypesCache.GetAllAssignableEventTypes(webhookEventType)))
        return;
      SystemManager.BackgroundTasksService.EnqueueTask((Action) (async () =>
      {
        try
        {
          ConfigElementDictionary<string, WebhookEventConfigElement> events = Config.Get<WebhookConfig>().Events;
          using (WebhookDataSender dataSender = this.GetWebhookDataSender())
            await dataSender.SendEventDataAsync(webhookEvent, WebhookHandler.WebhookEvents, events);
        }
        catch (Exception ex)
        {
          Log.Error("Could not execute webhook for event: {0}. Error: {1}", (object) webhookEventType?.FullName, (object) ex.Message);
        }
      }));
    }

    internal virtual WebhookDataSender GetWebhookDataSender() => new WebhookDataSender();

    internal static HashSet<Type> WebhookEvents => WebhookHandler.WebhookEventsCache.Value;
  }
}
