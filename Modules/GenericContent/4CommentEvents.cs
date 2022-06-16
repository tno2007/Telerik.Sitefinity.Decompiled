// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.Events.CommentDeletingEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Modules.GenericContent.Events
{
  internal class CommentDeletingEvent : 
    EventBase,
    ICommentDeletingEvent,
    ICommentDeleteEvent,
    ICommentEvent,
    IEvent,
    IPreProcessingEvent
  {
    /// <inheritdoc />
    public Comment DataItem { get; set; }

    /// <inheritdoc />
    public DateTime DeletionDate { get; set; }
  }
}
