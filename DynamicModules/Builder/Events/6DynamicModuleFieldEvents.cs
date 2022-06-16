// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Events.DynamicModuleFieldDeletedEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Diagnostics.CodeAnalysis;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.DynamicModules.Builder.Events
{
  /// <summary>
  /// This event is raised when <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleField" /> object has been deleted.
  /// </summary>
  [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
  internal class DynamicModuleFieldDeletedEvent : 
    EventBase,
    IDynamicModuleFieldDeletedEvent,
    IDynamicModuleFieldEvent,
    IModuleBuilderEvent,
    IEvent,
    IPostProcessingEvent
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Events.DynamicModuleFieldDeletedEvent" /> class.
    /// </summary>
    /// <param name="item">The <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleField" /> object that has been deleted.</param>
    public DynamicModuleFieldDeletedEvent(DynamicModuleField item)
    {
      this.Id = item.Id;
      this.Name = item.Name;
      this.ParentTypeFullName = item.FieldNamespace;
      this.Title = item.Title;
      this.FieldType = item.FieldType;
      this.ParentTypeId = item.ParentTypeId;
      this.ParentSectionId = item.ParentSectionId;
      this.ModuleName = item.ModuleName;
      this.ApplicationName = item.ApplicationName;
    }

    /// <inheritdoc />
    public Guid Id { get; private set; }

    /// <inheritdoc />
    public string Name { get; private set; }

    /// <inheritdoc />
    public string ParentTypeFullName { get; private set; }

    /// <inheritdoc />
    public string Title { get; private set; }

    /// <inheritdoc />
    public FieldType FieldType { get; private set; }

    /// <inheritdoc />
    public Guid ParentTypeId { get; private set; }

    /// <inheritdoc />
    public Guid ParentSectionId { get; private set; }

    /// <inheritdoc />
    public string ModuleName { get; private set; }

    /// <inheritdoc />
    public string ApplicationName { get; private set; }
  }
}
