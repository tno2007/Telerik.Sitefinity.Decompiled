// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Events.INewsletterSubscriptionEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Modules.Newsletters.Events
{
  /// <summary>
  /// Declares the contract for the base Newsletter Subscribe / Unsubscribe Event.
  /// </summary>
  public interface INewsletterSubscriptionEvent : IEvent
  {
    /// <summary>Gets the email address of the subscriber.</summary>
    string Email { get; }

    /// <summary>Gets the mailing lists id.</summary>
    Guid ListId { get; }
  }
}
