// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Events.DynamicModuleTypeDeletedEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.DynamicModules.Builder.Events
{
  /// <summary>
  /// This event is raised when <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" /> object has been deleted.
  /// </summary>
  [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
  internal class DynamicModuleTypeDeletedEvent : 
    EventBase,
    IDynamicModuleTypeDeletedEvent,
    IDynamicModuleTypeEvent,
    IModuleBuilderEvent,
    IEvent,
    IPostProcessingEvent
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Events.DynamicModuleTypeDeletedEvent" /> class.
    /// </summary>
    /// <param name="item">The <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" /> object that has been deleted.</param>
    public DynamicModuleTypeDeletedEvent(DynamicModuleType item)
    {
      this.Id = item.Id;
      this.Name = item.TypeName;
      this.Namespace = item.TypeNamespace;
      this.DisplayName = item.DisplayName;
      this.ParentModuleId = item.ParentModuleId;
      this.ParentModuleTypeId = item.ParentModuleTypeId;
      this.ApplicationName = item.ApplicationName;
      this.Fields = item.Fields != null ? (IEnumerable<Guid>) ((IEnumerable<DynamicModuleField>) item.Fields).Select<DynamicModuleField, Guid>((Func<DynamicModuleField, Guid>) (x => x.Id)).ToList<Guid>() : (IEnumerable<Guid>) new Guid[0];
      if (item.Sections == null)
        this.Sections = (IEnumerable<Guid>) new Guid[0];
      else
        this.Sections = (IEnumerable<Guid>) ((IEnumerable<FieldsBackendSection>) item.Sections).Select<FieldsBackendSection, Guid>((Func<FieldsBackendSection, Guid>) (s => s.Id)).ToList<Guid>();
    }

    /// <inheritdoc />
    public Guid Id { get; private set; }

    /// <inheritdoc />
    public string Name { get; private set; }

    /// <inheritdoc />
    public string Namespace { get; private set; }

    /// <inheritdoc />
    public string DisplayName { get; private set; }

    /// <inheritdoc />
    public Guid ParentModuleId { get; private set; }

    /// <inheritdoc />
    public Guid ParentModuleTypeId { get; private set; }

    /// <inheritdoc />
    public string ApplicationName { get; private set; }

    /// <inheritdoc />
    public IEnumerable<Guid> Fields { get; private set; }

    /// <inheritdoc />
    public IEnumerable<Guid> Sections { get; private set; }
  }
}
