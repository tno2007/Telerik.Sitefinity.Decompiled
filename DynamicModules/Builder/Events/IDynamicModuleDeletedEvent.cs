// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Events.IDynamicModuleDeletedEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.DynamicModules.Builder.Events
{
  /// <summary>
  /// A contract for events notifying for delete operations performed on instances of type <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" />.
  /// </summary>
  public interface IDynamicModuleDeletedEvent : 
    IDynamicModuleEvent,
    IModuleBuilderEvent,
    IEvent,
    IPostProcessingEvent
  {
    /// <summary>
    /// Gets the Id of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" /> object.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// Gets the name of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" /> object.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets the title of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" /> object.
    /// </summary>
    string Title { get; }

    /// <summary>
    /// Gets the description of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" /> object.
    /// </summary>
    string Description { get; }

    /// <summary>
    /// Gets the application name of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" /> object.
    /// </summary>
    string ApplicationName { get; }

    /// <summary>
    /// Gets the status of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" /> object.
    /// </summary>
    DynamicModuleStatus Status { get; }

    /// <summary>
    /// Gets a collection of the IDs of the child <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" /> objects that belongs to the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" /> object.
    /// </summary>
    IEnumerable<Guid> Types { get; }
  }
}
