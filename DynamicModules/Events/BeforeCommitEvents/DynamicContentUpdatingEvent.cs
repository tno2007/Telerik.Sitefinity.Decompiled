// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Events.DynamicContentUpdatingEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.DynamicModules.Events
{
  /// <summary>
  /// Updating event for dynamic content that is raised before committing transaction/save changes
  /// </summary>
  internal class DynamicContentUpdatingEvent : 
    DynamicContentEventBase,
    IDynamicContentUpdatingEvent,
    IDynamicContentEvent,
    IEvent,
    IPreProcessingEvent
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Events.DynamicContentUpdatingEvent" /> class.
    /// </summary>
    /// <param name="item">The item for which event is constructed.</param>
    /// <param name="itemState">The state of the item.</param>
    /// <param name="dataProviderName">Name of the data provider.</param>
    /// <param name="originalContentId">The original content id of the master item.</param>
    /// <param name="userId">The id of the user who raises the event.</param>
    /// <param name="lifecycleStatus">The lifecycle status of the item.</param>
    /// <param name="language">The language for which the event is constructed.</param>
    public DynamicContentUpdatingEvent(
      DynamicContent item,
      string itemState,
      string dataProviderName,
      Guid originalContentId,
      Guid userId,
      string lifecycleStatus,
      string language)
      : base(item.Id, item.GetType(), itemState, dataProviderName, originalContentId, lifecycleStatus, language)
    {
      ((IDynamicContentUpdatingEvent) this).Item = item;
      this.UserId = userId;
    }

    /// <summary>Gets or sets the dynamic item processed in the event.</summary>
    /// <value>The item.</value>
    DynamicContent IDynamicContentUpdatingEvent.Item { get; set; }

    /// <summary>Gets or sets the id of the user who raises the event.</summary>
    public Guid UserId { get; set; }
  }
}
