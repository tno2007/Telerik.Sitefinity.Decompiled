// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Events.AfterCommitEvents.PermissionEvents.PermissionUpdatedEvent
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
  /// Updated event for permission that is raised after committing transaction/save changes
  /// </summary>
  internal class PermissionUpdatedEvent : 
    PermissionDataEventBase,
    IPermissionUpdatedEvent,
    IPermissionDataEvent,
    IDataEvent,
    IEvent,
    IModuleBuilderEvent,
    IPostProcessingEvent,
    IPropertyChangeDataEvent
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Events.AfterCommitEvents.PermissionEvents.PermissionUpdatedEvent" /> class.
    /// </summary>
    /// <param name="item">The item for which event is constructed.</param>
    /// <param name="itemState">The state of the item.</param>
    /// <param name="dataProviderName">Name of the data provider.</param>
    /// <param name="userId">The id of the user who raises the event.</param>
    /// <param name="modificationDate">The modification date.</param>
    public PermissionUpdatedEvent(
      Permission item,
      string itemState,
      string dataProviderName,
      Guid userId,
      DateTime modificationDate)
      : base(item.Id, item.GetType(), itemState, dataProviderName, userId)
    {
      ((IPermissionUpdatedEvent) this).Item = item;
      this.ModificationDate = modificationDate;
    }

    /// <summary>Gets or sets the permission processed in the event.</summary>
    /// <value>The item.</value>
    Permission IPermissionUpdatedEvent.Item { get; set; }

    /// <summary>Gets or sets the modification date.</summary>
    /// <value>The modification date.</value>
    public DateTime ModificationDate { get; set; }
  }
}
