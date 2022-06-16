// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.StartupType
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Services
{
  /// <summary>Defines how the service or module is started.</summary>
  public enum StartupType
  {
    /// <summary>
    /// The services is automatically started when the application starts.
    /// </summary>
    OnApplicationStart,
    /// <summary>
    /// The service is automatically started when it is requested for the first time.
    /// </summary>
    OnFirstCall,
    /// <summary>
    /// The services has to be started manually through the UI.
    /// </summary>
    Manual,
    /// <summary>
    /// The service is disabled and will not be loaded at all.
    /// </summary>
    Disabled,
  }
}
