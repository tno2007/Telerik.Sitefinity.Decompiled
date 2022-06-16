// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Events.FieldsBackendSectionDeletedEvent
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
  /// This event is raised when <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.FieldsBackendSection" /> object has been deleted.
  /// </summary>
  [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
  internal class FieldsBackendSectionDeletedEvent : 
    EventBase,
    IFieldsBackendSectionDeletedEvent,
    IFieldsBackendSectionEvent,
    IModuleBuilderEvent,
    IEvent,
    IPostProcessingEvent
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Events.FieldsBackendSectionDeletedEvent" /> class.
    /// </summary>
    /// <param name="item">The <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.FieldsBackendSection" /> object that has been deleted.</param>
    public FieldsBackendSectionDeletedEvent(FieldsBackendSection item)
    {
      this.Id = item.Id;
      this.Name = item.Name;
      this.Title = item.Title;
      this.ParentTypeId = item.ParentTypeId;
      this.ApplicationName = item.ApplicationName;
      this.Ordinal = item.Ordinal;
    }

    /// <inheritdoc />
    public Guid Id { get; private set; }

    /// <inheritdoc />
    public string Name { get; private set; }

    /// <inheritdoc />
    public string Title { get; private set; }

    /// <inheritdoc />
    public int Ordinal { get; private set; }

    /// <inheritdoc />
    public Guid ParentTypeId { get; private set; }

    /// <inheritdoc />
    public string ApplicationName { get; private set; }
  }
}
