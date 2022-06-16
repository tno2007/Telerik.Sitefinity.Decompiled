// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.SystemRestartFlags
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Services
{
  /// <summary>
  /// Flags passed to the SystemManager.RestartApplication() method
  /// </summary>
  [Flags]
  public enum SystemRestartFlags
  {
    /// <summary>Default restart</summary>
    Default = 0,
    /// <summary>
    /// If set, attempts to make full restart - unloads the application from the app domain
    /// </summary>
    AttemptFullRestart = 1,
    /// <summary>
    /// If set, clears the current connections cache, so it will be re-created.
    /// Equals to to call OpenAccessConnection.ResetModel()
    /// </summary>
    ResetModel = 2,
  }
}
