// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Events.IDynamicModuleItemEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.DynamicModules.Builder.Events
{
  /// <summary>
  /// A contract for events notifying for operations that are performed on instances of type <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" /> and during which there is existing <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" /> object.
  /// </summary>
  public interface IDynamicModuleItemEvent : IDynamicModuleEvent, IModuleBuilderEvent, IEvent
  {
    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" /> object that is manipulated.
    /// </summary>
    DynamicModule Item { get; }
  }
}
