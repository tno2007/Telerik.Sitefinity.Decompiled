// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.ContentLinks.Events.ContentLinkDeletedEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Data.ContentLinks.Events
{
  /// <summary>
  /// A class for events notifying for delete operations performed on the instances of type <see cref="!:ContentLink" />.
  /// </summary>
  internal class ContentLinkDeletedEvent : 
    EventBase,
    IContentLinkDeletedEvent,
    IContentLinkEvent,
    IEvent
  {
    /// <inheritdoc />
    public ContentLink Item { get; set; }

    /// <inheritdoc />
    public Guid UserId { get; set; }

    /// <inheritdoc />
    public DateTime DeletionDate { get; set; }
  }
}
