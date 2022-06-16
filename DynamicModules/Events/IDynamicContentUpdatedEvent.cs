// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Events.IDynamicContentUpdatedEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.DynamicModules.Events
{
  /// <summary>
  /// A contract for events notifying for update operation performed on an instance of type <see cref="!:ForumGroup" />
  /// </summary>
  public interface IDynamicContentUpdatedEvent : IDynamicContentEvent, IEvent, IPostProcessingEvent
  {
    /// <summary>
    /// Gets a value indicating whether the item related to this event is visible or not.
    /// </summary>
    bool Visible { get; }

    /// <summary>Gets the modification date.</summary>
    DateTime ModificationDate { get; }

    /// <summary>Gets or sets the item.</summary>
    /// <value>The item.</value>
    DynamicContent Item { get; set; }
  }
}
