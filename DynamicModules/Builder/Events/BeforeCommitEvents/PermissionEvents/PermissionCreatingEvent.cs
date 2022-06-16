﻿// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Events.BeforeCommitEvents.PermissionEvents.PermissionCreatingEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Data.Events;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.DynamicModules.Builder.Events.BeforeCommitEvents.PermissionEvents
{
  /// <summary>
  /// Event for creating permission that is raised before committing transaction/save changes
  /// </summary>
  internal class PermissionCreatingEvent : 
    PermissionDataEventBase,
    IPermissionCreatingEvent,
    IPermissionDataEvent,
    IDataEvent,
    IEvent,
    IModuleBuilderEvent,
    IPreProcessingEvent
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Events.BeforeCommitEvents.PermissionEvents.PermissionCreatingEvent" /> class.
    /// </summary>
    /// <param name="item">The item for which event is constructed.</param>
    /// <param name="action">The action.</param>
    /// <param name="dataProviderName">Name of the data provider.</param>
    /// <param name="userId">The id of the user who raises the event.</param>
    public PermissionCreatingEvent(
      Permission item,
      string action,
      string dataProviderName,
      Guid userId)
      : base(item.Id, item.GetType(), action, dataProviderName, userId)
    {
      ((IPermissionCreatingEvent) this).Item = item;
    }

    /// <summary>Gets or sets the permission processed in the event.</summary>
    /// <value>The item.</value>
    Permission IPermissionCreatingEvent.Item { get; set; }
  }
}