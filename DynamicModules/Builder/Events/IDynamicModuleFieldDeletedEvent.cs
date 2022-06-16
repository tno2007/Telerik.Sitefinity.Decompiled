// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Events.IDynamicModuleFieldDeletedEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.DynamicModules.Builder.Events
{
  /// <summary>
  /// A contract for events notifying for delete operation performed on instances of type <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleField" />.
  /// </summary>
  public interface IDynamicModuleFieldDeletedEvent : 
    IDynamicModuleFieldEvent,
    IModuleBuilderEvent,
    IEvent,
    IPostProcessingEvent
  {
    /// <summary>
    /// Gets the Id of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleField" /> object.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// Gets the name of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleField" /> object.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets the title of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleField" /> object.
    /// </summary>
    string Title { get; }

    /// <summary>
    /// Gets the <see cref="P:Telerik.Sitefinity.DynamicModules.Builder.Events.IDynamicModuleFieldDeletedEvent.FieldType" /> of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleField" /> object.
    /// </summary>
    FieldType FieldType { get; }

    /// <summary>
    /// Gets the Id of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" /> to which the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleField" /> object belongs to.
    /// </summary>
    Guid ParentTypeId { get; }

    /// <summary>
    /// Gets the full name of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" /> to which the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleField" /> object belongs to.
    /// </summary>
    string ParentTypeFullName { get; }

    /// <summary>
    /// Gets the Id of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.FieldsBackendSection" /> to which the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleField" /> object belongs to.
    /// </summary>
    Guid ParentSectionId { get; }

    /// <summary>
    /// Gets the name of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" /> to which the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" />, that is parent of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleField" /> object, belongs to.
    /// </summary>
    string ModuleName { get; }

    /// <summary>
    /// Gets the application name of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleField" /> object.
    /// </summary>
    string ApplicationName { get; }
  }
}
