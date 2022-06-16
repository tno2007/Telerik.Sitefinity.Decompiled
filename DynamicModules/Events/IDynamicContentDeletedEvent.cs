// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Events.IDynamicContentDeletedEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.DynamicModules.Events
{
  /// <summary>
  /// A contract for events notifying for delete operation performed on an instance of type <see cref="!:ForumGroup" />
  /// </summary>
  public interface IDynamicContentDeletedEvent : IDynamicContentEvent, IEvent, IPostProcessingEvent
  {
    /// <summary>Gets the deletion date.</summary>
    DateTime DeletionDate { get; }

    /// <summary>Gets item's id.</summary>
    Guid ItemId { get; }
  }
}
