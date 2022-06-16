// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Events.IDynamicModuleTypeUpdatedEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Data.Events;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.DynamicModules.Builder.Events
{
  /// <summary>
  /// A contract for events notifying for update operation performed on instances of type <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" />.
  /// </summary>
  public interface IDynamicModuleTypeUpdatedEvent : 
    IDynamicModuleTypeItemEvent,
    IDynamicModuleTypeEvent,
    IModuleBuilderEvent,
    IEvent,
    IPostProcessingEvent,
    IPropertyChangeDataEvent
  {
    /// <summary>
    /// Gets a value indicating whether widget templates of the module type should be updated.
    /// </summary>
    bool ShouldUpdateWidgetTemplates { get; }
  }
}
