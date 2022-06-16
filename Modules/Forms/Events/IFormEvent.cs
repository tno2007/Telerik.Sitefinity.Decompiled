// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Events.IFormEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Modules.Forms.Events
{
  /// <summary>A contract for events that contain all the form data.</summary>
  public interface IFormEvent : IFormsModuleEvent, IEvent
  {
    /// <summary>Gets or sets the id of the user submitting the form.</summary>
    /// <value>The user id.</value>
    Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the user name of the user submitting the form.
    /// </summary>
    /// <value>The user name.</value>
    string Username { get; set; }

    /// <summary>
    /// Gets or sets the IP address of the client that submits the form entry.
    /// </summary>
    string IpAddress { get; set; }

    /// <summary>Gets or sets the ID of the form being submitted.</summary>
    Guid FormId { get; set; }

    /// <summary>
    /// Gets or sets the unique (code) name of the form being submitted.
    /// </summary>
    string FormName { get; set; }

    /// <summary>Gets or sets the title of the form being submitted.</summary>
    string FormTitle { get; set; }

    /// <summary>
    /// Gets or sets a collection of the form controls, which can be used to retrieve their title, values, etc.
    /// </summary>
    IEnumerable<IFormEntryValidationEventControl> Controls { get; set; }

    /// <summary>
    /// Gets or sets a value specifying the origin of the event.
    /// </summary>
    new string Origin { get; set; }
  }
}
