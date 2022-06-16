// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Events.NewsletterSubscriptionDeletedEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Modules.Newsletters.Events
{
  internal class NewsletterSubscriptionDeletedEvent : 
    INewsletterSubscriptionDeletedEvent,
    INewsletterSubscriptionEvent,
    IEvent
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Events.NewsletterSubscriptionDeletedEvent" /> class.
    /// </summary>
    /// <param name="email">The email.</param>
    /// <param name="listId">The list id.</param>
    /// <param name="campaignId">The campaign id.</param>
    /// <param name="origin">The origin.</param>
    public NewsletterSubscriptionDeletedEvent(
      string email,
      Guid listId,
      Guid campaignId,
      string origin = null)
    {
      this.Initialize(email, listId, campaignId, origin);
    }

    private void Initialize(string email, Guid listId, Guid campaignId, string origin)
    {
      this.Email = email;
      this.ListId = listId;
      this.CampaignId = campaignId;
      this.Origin = origin;
    }

    public string Email { get; set; }

    public Guid ListId { get; set; }

    public Guid CampaignId { get; set; }

    public string Origin { get; set; }
  }
}
