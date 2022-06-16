﻿// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Events.IPermissionDataEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Data.Events;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.DynamicModules.Builder.Events
{
  /// <summary>
  /// A base contract for events notifying for changes on permission objects.
  /// </summary>
  public interface IPermissionDataEvent : IDataEvent, IEvent, IModuleBuilderEvent
  {
    /// <summary>Gets the user id.</summary>
    Guid UserId { get; }
  }
}
