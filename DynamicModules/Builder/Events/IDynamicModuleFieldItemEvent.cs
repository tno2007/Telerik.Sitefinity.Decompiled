// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Events.IDynamicModuleFieldItemEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.DynamicModules.Builder.Events
{
  /// <summary>
  /// A contract for events notifying for operations that are performed on instances of type <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleField" /> and during which there is existing <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleField" /> object.
  /// </summary>
  public interface IDynamicModuleFieldItemEvent : 
    IDynamicModuleFieldEvent,
    IModuleBuilderEvent,
    IEvent
  {
    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleField" /> object that is manipulated.
    /// </summary>
    DynamicModuleField Item { get; }
  }
}
