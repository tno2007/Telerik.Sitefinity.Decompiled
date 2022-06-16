// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Events.FieldsBackendSectionUpdatedEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Telerik.Sitefinity.Data.Events;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.DynamicModules.Builder.Events
{
  /// <summary>
  /// This event is raised when <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.FieldsBackendSection" /> object has been updated.
  /// </summary>
  [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
  internal class FieldsBackendSectionUpdatedEvent : 
    FieldsBackendSectionItemEvent,
    IFieldsBackendSectionUpdatedEvent,
    IFieldsBackendSectionItemEvent,
    IFieldsBackendSectionEvent,
    IModuleBuilderEvent,
    IEvent,
    IPostProcessingEvent,
    IPropertyChangeDataEvent
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Events.FieldsBackendSectionUpdatedEvent" /> class.
    /// </summary>
    /// <param name="item">The <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.FieldsBackendSection" /> object that has been updated.</param>
    public FieldsBackendSectionUpdatedEvent(FieldsBackendSection item)
      : base(item)
    {
      this.ChangedProperties = (IDictionary<string, PropertyChange>) new Dictionary<string, PropertyChange>();
    }

    /// <inheritdoc />
    public IDictionary<string, PropertyChange> ChangedProperties { get; private set; }
  }
}
