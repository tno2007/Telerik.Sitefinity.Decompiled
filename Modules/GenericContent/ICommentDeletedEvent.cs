// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.Events.ICommentDeletedEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Modules.GenericContent.Events
{
  /// <summary>This event is raised after a comment is deleted.</summary>
  [Obsolete("Use Telerik.Sitefinity.Services.Comments.ICommentDeletingEvent and ICommentDeletedEvent.")]
  public interface ICommentDeletedEvent : 
    ICommentDeleteEvent,
    ICommentEvent,
    IEvent,
    IPostProcessingEvent
  {
    /// <summary>The ID of the deleted comment.</summary>
    Guid CommentId { get; }
  }
}
