// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Events.DynamicModuleDeletedEvent
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
  /// This event is raised when <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" /> object has been deleted.
  /// </summary>
  [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
  internal class DynamicModuleDeletedEvent : 
    EventBase,
    IDynamicModuleDeletedEvent,
    IDynamicModuleEvent,
    IModuleBuilderEvent,
    IEvent,
    IPostProcessingEvent
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Events.DynamicModuleDeletedEvent" /> class.
    /// </summary>
    /// <param name="item">The <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" /> object that has been deleted.</param>
    public DynamicModuleDeletedEvent(DynamicModule item)
    {
      this.Id = item.Id;
      this.Name = item.Name;
      this.Title = item.Title;
      this.Description = item.Description;
      this.ApplicationName = item.ApplicationName;
      this.Status = item.Status;
      this.Types = (IEnumerable<Guid>) ((IEnumerable<DynamicModuleType>) item.Types).Select<DynamicModuleType, Guid>((Func<DynamicModuleType, Guid>) (t => t.Id)).ToList<Guid>();
    }

    /// <inheritdoc />
    public Guid Id { get; private set; }

    /// <inheritdoc />
    public string Name { get; private set; }

    /// <inheritdoc />
    public string Title { get; private set; }

    /// <inheritdoc />
    public string Description { get; private set; }

    /// <inheritdoc />
    public string ApplicationName { get; private set; }

    /// <inheritdoc />
    public DynamicModuleStatus Status { get; private set; }

    /// <inheritdoc />
    public IEnumerable<Guid> Types { get; private set; }
  }
}
