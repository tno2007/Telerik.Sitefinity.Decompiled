﻿// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Events.DynamicModuleTypeCreatedEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Diagnostics.CodeAnalysis;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.DynamicModules.Builder.Events
{
  /// <summary>
  /// This event is raised when <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" /> object has been created.
  /// </summary>
  [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
  internal class DynamicModuleTypeCreatedEvent : 
    DynamicModuleTypeItemEvent,
    IDynamicModuleTypeCreatedEvent,
    IDynamicModuleTypeItemEvent,
    IDynamicModuleTypeEvent,
    IModuleBuilderEvent,
    IEvent,
    IPostProcessingEvent
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Events.DynamicModuleTypeCreatedEvent" /> class.
    /// </summary>
    /// <param name="item">The <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" /> object that has been created.</param>
    public DynamicModuleTypeCreatedEvent(DynamicModuleType item)
      : base(item)
    {
    }
  }
}