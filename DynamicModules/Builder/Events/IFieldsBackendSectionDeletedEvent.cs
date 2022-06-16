// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Events.IFieldsBackendSectionDeletedEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.DynamicModules.Builder.Events
{
  /// <summary>
  /// A contract for events notifying for delete operations performed on instances of type <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.FieldsBackendSection" />.
  /// </summary>
  public interface IFieldsBackendSectionDeletedEvent : 
    IFieldsBackendSectionEvent,
    IModuleBuilderEvent,
    IEvent,
    IPostProcessingEvent
  {
    /// <summary>
    /// Gets the Id of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.FieldsBackendSection" /> object.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    /// Gets the name of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.FieldsBackendSection" /> object.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets the title of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.FieldsBackendSection" /> object.
    /// </summary>
    string Title { get; }

    /// <summary>
    /// Gets the ordinal of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.FieldsBackendSection" /> object.
    /// </summary>
    int Ordinal { get; }

    /// <summary>
    /// Gets the Id of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" /> to which the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.FieldsBackendSection" /> object belongs to.
    /// </summary>
    Guid ParentTypeId { get; }

    /// <summary>
    /// Gets the application name of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.FieldsBackendSection" /> object.
    /// </summary>
    string ApplicationName { get; }
  }
}
