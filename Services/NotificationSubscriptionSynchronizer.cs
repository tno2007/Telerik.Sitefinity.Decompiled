// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.NotificationSubscriptionSynchronizer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Events;
using Telerik.Sitefinity.Services.Events;
using Telerik.Sitefinity.Services.Notifications;

namespace Telerik.Sitefinity.Services
{
  /// <summary>
  /// Provides synchronization of subscribers from registered modules, when an user is deleted, deactivated or activated.
  /// </summary>
  internal class NotificationSubscriptionSynchronizer : INotificationSubscriptionSynchronizer
  {
    private readonly ICollection<ServiceContext> contextCache = (ICollection<ServiceContext>) new List<ServiceContext>();

    public NotificationSubscriptionSynchronizer()
    {
      this.NotificationService = SystemManager.GetNotificationService();
      this.SubscribeUserEvents();
    }

    public INotificationService NotificationService { get; set; }

    public void SubscribeUserEvents()
    {
      EventHub.Subscribe<UserUpdated>(new SitefinityEventHandler<UserUpdated>(this.HandleUserUpdated));
      EventHub.Subscribe<UserDeleted>(new SitefinityEventHandler<UserDeleted>(this.HandleUserDeleted));
    }

    public void UnsubscribeUserEvents()
    {
      EventHub.Unsubscribe<UserUpdated>(new SitefinityEventHandler<UserUpdated>(this.HandleUserUpdated));
      EventHub.Unsubscribe<UserDeleted>(new SitefinityEventHandler<UserDeleted>(this.HandleUserDeleted));
    }

    private void HandleUserUpdated(UserUpdated eventData)
    {
      ISubscriberRequest subscriber = this.GetSubscriber(eventData.UserId);
      subscriber.Disabled = !eventData.IsApproved;
      foreach (ServiceContext context in (IEnumerable<ServiceContext>) this.contextCache)
      {
        if (this.NotificationService.GetSubscriber(context, subscriber.ResolveKey) != null)
          this.NotificationService.UpdateSubscriber(context, subscriber.ResolveKey, subscriber);
      }
    }

    private void HandleUserDeleted(UserDeleted eventData)
    {
      ISubscriberRequest subscriber = this.CreateSubscriber((UserEventBase) eventData);
      foreach (ServiceContext context in (IEnumerable<ServiceContext>) this.contextCache)
      {
        if (this.NotificationService.GetSubscriber(context, subscriber.ResolveKey) != null)
          this.NotificationService.DeleteSubscriber(context, subscriber.ResolveKey);
      }
    }

    /// <summary>
    /// Registers the specified module name for subscribes synchronization;
    /// </summary>
    /// <param name="moduleName">Name of the module.</param>
    public void Register(ServiceContext serviceContext)
    {
      if (this.contextCache.Contains(serviceContext))
        return;
      this.contextCache.Add(serviceContext);
    }

    internal ISubscriberRequest CreateSubscriber(UserEventBase eventData) => (ISubscriberRequest) new SubscriberRequestProxy()
    {
      Email = eventData.Email,
      ResolveKey = this.GetUserKey(eventData.UserId, eventData.MembershipProviderName)
    };

    internal string GetUserKey(Guid userId, string providerName) => userId.ToString() + ":" + providerName;

    internal ISubscriberRequest GetSubscriber(Guid userId) => SecurityManager.GetSubscriberObject(userId);

    internal ICollection<ServiceContext> GetRegisteredContexts() => this.contextCache;
  }
}
