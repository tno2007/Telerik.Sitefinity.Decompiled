// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Dashboard.IDashboardInitializer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Dashboard
{
  /// <summary>
  /// Exposes an API object to the <see cref="M:Telerik.Sitefinity.Dashboard.IDashboardEnabledModule.Configure(Telerik.Sitefinity.Dashboard.IDashboardInitializer)" /> method.
  /// </summary>
  internal interface IDashboardInitializer
  {
    /// <summary>
    /// Registers a type to be tracked and shown in the dashboard.
    /// </summary>
    /// <param name="type">The type to be registered.</param>
    void RegisterType(string type);
  }
}
