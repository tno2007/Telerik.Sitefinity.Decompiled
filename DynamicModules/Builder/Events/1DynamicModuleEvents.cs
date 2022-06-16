// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Events.DynamicModuleCreatingEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Diagnostics.CodeAnalysis;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.DynamicModules.Builder.Events
{
  /// <summary>
  /// This event is raised during the creation of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" /> object, before the transaction is completed.
  /// </summary>
  [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
  internal class DynamicModuleCreatingEvent : 
    DynamicModuleItemEvent,
    IDynamicModuleCreatingEvent,
    IDynamicModuleItemEvent,
    IDynamicModuleEvent,
    IModuleBuilderEvent,
    IEvent,
    IPreProcessingEvent
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Events.DynamicModuleCreatingEvent" /> class.
    /// </summary>
    /// <param name="item">The <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" /> object that is going to be created.</param>
    public DynamicModuleCreatingEvent(DynamicModule item)
      : base(item)
    {
    }
  }
}
