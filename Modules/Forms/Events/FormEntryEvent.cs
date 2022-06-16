// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Events.FormEntryEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Modules.Forms.Events
{
  /// <summary>Raised during form entry events.</summary>
  internal class FormEntryEvent : IFormEntryEvent, IFormsModuleEvent, IEvent
  {
    /// <summary>Gets or sets The ID of the submitted form entry.</summary>
    public Guid EntryId { get; set; }

    /// <summary>
    /// Gets or sets the ReferralCode of the submitted form entry.
    /// </summary>
    public string ReferralCode { get; set; }

    /// <summary>Gets or sets the id of the user submitting the form.</summary>
    /// <value>The user id.</value>
    public Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the user name of the user submitting the form.
    /// </summary>
    /// <value>The user name.</value>
    public string Username { get; set; }

    /// <summary>
    /// Gets or sets the IP address of the client that submits the form entry.
    /// </summary>
    public string IpAddress { get; set; }

    /// <summary>
    /// Gets or sets the date/time of the form entry submission.
    /// </summary>
    public DateTime SubmissionTime { get; set; }

    /// <summary>Gets or sets the ID of the form being submitted.</summary>
    public Guid FormId { get; set; }

    /// <summary>
    /// Gets or sets the unique (code) name of the form being submitted.
    /// </summary>
    public string FormName { get; set; }

    /// <summary>Gets or sets the title of the form being submitted.</summary>
    public string FormTitle { get; set; }

    /// <summary>
    /// Gets or sets the ID of the list of submission notification subscribers (in terms of the NotificationService) for this form.
    /// </summary>
    public Guid FormSubscriptionListId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to send a confirmation email to the user who submitted the form
    /// </summary>
    public bool SendConfirmationEmail { get; set; }

    /// <summary>
    /// Gets or sets a value specifying the origin of the event.
    /// </summary>
    public string Origin { get; set; }

    /// <summary>
    /// Gets or sets a collection of the form controls, which can be used to retrieve their title, values, etc.
    /// </summary>
    public IEnumerable<IFormEntryEventControl> Controls { get; set; }

    /// <summary>Gets or sets the notification emails.</summary>
    /// <value>The notification emails.</value>
    public IEnumerable<string> NotificationEmails { get; set; }
  }
}
