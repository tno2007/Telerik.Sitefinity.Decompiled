// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Events.DynamicModuleTypeItemEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Diagnostics.CodeAnalysis;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.DynamicModules.Builder.Events
{
  /// <summary>
  /// This is a base class for events that notify for operations that are performed on instances of type <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" /> and during which there is existing <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" /> object.
  /// </summary>
  [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
  internal abstract class DynamicModuleTypeItemEvent : 
    EventBase,
    IDynamicModuleTypeItemEvent,
    IDynamicModuleTypeEvent,
    IModuleBuilderEvent,
    IEvent
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Events.DynamicModuleTypeItemEvent" /> class.
    /// </summary>
    /// <param name="item">The <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" /> object that is going to be manipulated.</param>
    public DynamicModuleTypeItemEvent(DynamicModuleType item) => this.Item = item;

    /// <inheritdoc />
    public DynamicModuleType Item { get; private set; }
  }
}
