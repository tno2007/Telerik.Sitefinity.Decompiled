// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Events.IFormEntryEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Modules.Forms.Events
{
  /// <summary>Contract for events related to form entries.</summary>
  public interface IFormEntryEvent : IFormsModuleEvent, IEvent
  {
    /// <summary>Gets the ID of the submitted form entry.</summary>
    Guid EntryId { get; }

    /// <summary>Gets the ReferralCode of the submitted form entry.</summary>
    string ReferralCode { get; }

    /// <summary>Gets the id of the user submitting the form.</summary>
    /// <value>The user id.</value>
    Guid UserId { get; }

    /// <summary>Gets the user name of the user submitting the form.</summary>
    /// <value>The user name.</value>
    string Username { get; }

    /// <summary>
    /// Gets the IP address of the client that submits the form entry.
    /// </summary>
    string IpAddress { get; }

    /// <summary>Gets the date/time of the form entry submission.</summary>
    DateTime SubmissionTime { get; }

    /// <summary>Gets the ID of the form being submitted.</summary>
    Guid FormId { get; }

    /// <summary>
    /// Gets the unique (code) name of the form being submitted.
    /// </summary>
    string FormName { get; }

    /// <summary>Gets the title of the form being submitted.</summary>
    string FormTitle { get; }

    /// <summary>
    /// Gets the ID of the list of submission notification subscribers (in terms of the NotificationService) for this form.
    /// </summary>
    Guid FormSubscriptionListId { get; }

    /// <summary>
    /// Gets a value indicating whether to send a confirmation email to the user who submitted the form
    /// </summary>
    bool SendConfirmationEmail { get; }

    /// <summary>
    /// Gets a collection of the form controls, which can be used to retrieve their title, values, etc.
    /// </summary>
    /// <remarks>
    /// The instances populated by Sitefinity also implement the <see cref="T:Telerik.Sitefinity.Modules.Forms.Events.IFormEntryEventNamedControl" /> interfaces.
    /// </remarks>
    IEnumerable<IFormEntryEventControl> Controls { get; }

    /// <summary>Gets the notification emails.</summary>
    /// <value>The notification emails.</value>
    IEnumerable<string> NotificationEmails { get; }
  }
}
