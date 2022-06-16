// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.Events.ICommentCreatedEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Modules.GenericContent.Events
{
  /// <summary>This event is raised after a comment is created.</summary>
  [Obsolete("Use Telerik.Sitefinity.Services.Comments.ICommentCreatedEvent")]
  public interface ICommentCreatedEvent : 
    ICommentCreateEvent,
    ICommentEvent,
    IEvent,
    IPostProcessingEvent
  {
    /// <summary>The ID of the created comment.</summary>
    Guid CommentId { get; }
  }
}
