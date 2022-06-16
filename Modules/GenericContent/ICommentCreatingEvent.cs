// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.Events.ICommentCreatingEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Modules.GenericContent.Events
{
  /// <summary>
  /// This event is raised during event creation, before the transaction is completed.
  /// Throwing exception by the handler will cancel the action and rollback the current transaction.
  /// </summary>
  [Obsolete("Use Telerik.Sitefinity.Services.Comments.ICommentCreatedEvent")]
  public interface ICommentCreatingEvent : 
    ICommentCreateEvent,
    ICommentEvent,
    IEvent,
    IPreProcessingEvent
  {
    /// <summary>The comment being created.</summary>
    Comment DataItem { get; }
  }
}
