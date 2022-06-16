// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Events.DataProviderEventFactory
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Data.Events
{
  /// <summary>A factory for creating data provider events.</summary>
  internal static class DataProviderEventFactory
  {
    /// <summary>
    /// Creates an event for notifying that a data provider has been created.
    /// </summary>
    /// <param name="managerType">Type of the manager.</param>
    /// <param name="providerName">Name of the data provider.</param>
    /// <param name="moduleName">Name of the dynamic module. Set to null if the provider is not a dynamic module provider.</param>
    /// <returns>IEvent item</returns>
    public static IEvent CreateDataProviderCreatedEvent(
      Type managerType,
      string providerName,
      string moduleName = null)
    {
      return (IEvent) new DataProviderCreatedEvent()
      {
        ManagerType = managerType,
        ProviderName = providerName,
        ModuleName = moduleName
      };
    }
  }
}
