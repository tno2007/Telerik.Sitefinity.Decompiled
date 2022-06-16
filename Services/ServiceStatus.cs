// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.ServiceStatus
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Services
{
  /// <summary>
  /// Defines the current status of a Sitefinity system service.
  /// </summary>
  public enum ServiceStatus
  {
    /// <summary>
    /// Indicates the services will not process any requests.
    /// </summary>
    Stopped,
    /// <summary>Indicates the services is currently paused.</summary>
    Paused,
    /// <summary>
    /// Indicates the services is started and ready to accept requests.
    /// </summary>
    Started,
  }
}
