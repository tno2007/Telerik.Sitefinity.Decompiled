// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.INotificationSubscriptionSynchronizer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Services.Notifications;

namespace Telerik.Sitefinity.Services
{
  /// <summary>
  /// The contract for operations that will ensure synchronization of subscribers from registered modules, when an user is deleted, deactivated or activated.
  /// </summary>
  internal interface INotificationSubscriptionSynchronizer
  {
    void Register(ServiceContext serviceContext);

    INotificationService NotificationService { get; set; }
  }
}
