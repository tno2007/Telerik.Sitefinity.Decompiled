// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Dashboard.IDashboardEnabledModule
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Dashboard
{
  /// <summary>
  /// This interface should be implemented by modules, which would like to register types to be shown on the dashboard.
  /// </summary>
  internal interface IDashboardEnabledModule
  {
    /// <summary>
    /// This method is called after the dashboard module is initialized,
    /// letting the module implementing the interface to register
    /// its types which should be shown on the dashboard.
    /// </summary>
    /// <param name="initializer">An object exposing dashboard initialization API.</param>
    void Configure(IDashboardInitializer initializer);
  }
}
