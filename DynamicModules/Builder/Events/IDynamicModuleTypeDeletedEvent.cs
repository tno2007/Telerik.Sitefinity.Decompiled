// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Events.IDynamicModuleTypeDeletedEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.DynamicModules.Builder.Events
{
  /// <summary>
  /// A contract for events notifying for delete operation performed on instances of type <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" />.
  /// </summary>
  public interface IDynamicModuleTypeDeletedEvent : 
    IDynamicModuleTypeEvent,
    IModuleBuilderEvent,
    IEvent,
    IPostProcessingEvent
  {
    /// <summary>
    /// Gets the Id of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" /> object.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// Gets the name of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" /> object.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets the namespace of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" /> object.
    /// </summary>
    string Namespace { get; }

    /// <summary>
    /// Gets the display name of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" /> object.
    /// </summary>
    string DisplayName { get; }

    /// <summary>
    /// Gets the Id of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" /> to which the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" /> object belongs to.
    /// </summary>
    Guid ParentModuleId { get; }

    /// <summary>
    /// Gets the Id of the parent <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" /> object in the module hierarchy.
    /// </summary>
    Guid ParentModuleTypeId { get; }

    /// <summary>
    /// Gets the application name <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" /> object.
    /// </summary>
    string ApplicationName { get; }

    /// <summary>
    /// Gets a collection of the IDs of the child <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleField" /> objects that belongs to the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" /> object.
    /// </summary>
    IEnumerable<Guid> Fields { get; }

    /// <summary>
    /// Gets a collection of the IDs of the child <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.FieldsBackendSection" /> objects that belongs to the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" /> object.
    /// </summary>
    IEnumerable<Guid> Sections { get; }
  }
}
