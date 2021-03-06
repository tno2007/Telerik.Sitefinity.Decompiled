// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Events.IDynamicModuleDeletingEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.DynamicModules.Builder.Events
{
  /// <summary>
  /// A contract for event that is raised during the deletion of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" /> object, before the transaction is completed.
  /// </summary>
  public interface IDynamicModuleDeletingEvent : 
    IDynamicModuleItemEvent,
    IDynamicModuleEvent,
    IModuleBuilderEvent,
    IEvent,
    IPreProcessingEvent
  {
  }
}
