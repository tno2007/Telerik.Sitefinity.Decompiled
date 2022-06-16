// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Events.AfterCommitEvents.PermissionEvents.PermissionCreatedEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Data.Events;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.DynamicModules.Builder.Events.AfterCommitEvents.PermissionEvents
{
  /// <summary>
  /// Event for created permission that is raised after committing transaction/save changes
  /// </summary>
  internal class PermissionCreatedEvent : 
    PermissionDataEventBase,
    IPermissionCreatedEvent,
    IPermissionDataEvent,
    IDataEvent,
    IEvent,
    IModuleBuilderEvent,
    IPostProcessingEvent
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Events.AfterCommitEvents.PermissionEvents.PermissionCreatedEvent" /> class.
    /// </summary>
    /// <param name="item">The item for which event is constructed.</param>
    /// <param name="action">The action.</param>
    /// <param name="dataProviderName">Name of the data provider.</param>
    /// <param name="userId">The id of the user who raises the event.</param>
    /// <param name="creationDate">The creation date.</param>
    public PermissionCreatedEvent(
      Permission item,
      string action,
      string dataProviderName,
      Guid userId,
      DateTime creationDate)
      : base(item.Id, item.GetType(), action, dataProviderName, userId)
    {
      ((IPermissionCreatedEvent) this).Item = item;
      this.CreationDate = creationDate;
    }

    /// <summary>Gets or sets the permission processed in the event.</summary>
    /// <value>The item.</value>
    Permission IPermissionCreatedEvent.Item { get; set; }

    /// <summary>Gets or sets the creation date.</summary>
    /// <value>The creation date.</value>
    public DateTime CreationDate { get; set; }
  }
}
