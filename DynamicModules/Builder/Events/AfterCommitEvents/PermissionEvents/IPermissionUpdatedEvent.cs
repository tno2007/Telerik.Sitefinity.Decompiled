// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Events.AfterCommitEvents.PermissionEvents.IPermissionUpdatedEvent
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
  /// Event for updated permission that is raised after committing transaction/save changes
  /// </summary>
  public interface IPermissionUpdatedEvent : 
    IPermissionDataEvent,
    IDataEvent,
    IEvent,
    IModuleBuilderEvent,
    IPostProcessingEvent,
    IPropertyChangeDataEvent
  {
    /// <summary>Gets the modification date.</summary>
    DateTime ModificationDate { get; }

    /// <summary>Gets or sets the permission processed in the event.</summary>
    /// <value>The item.</value>
    Permission Item { get; set; }
  }
}
