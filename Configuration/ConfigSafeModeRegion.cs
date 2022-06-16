// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.ConfigSafeModeRegion
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Configuration
{
  /// <summary>
  /// Determine a region for executing a code in Safe Mode for loading configuration.
  /// Safe Mode of the configuration means that if the Configuration is configured to be persisted in the database, it does not load the setting from database,
  /// for preventing infinity recursion
  /// </summary>
  internal class ConfigSafeModeRegion : IDisposable
  {
    private readonly bool? prevSafeMode;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Configuration.ConfigSafeModeRegion" /> class.
    /// </summary>
    /// <param name="safeMode">Specify whether to switch to safe mode, or leave it as it is</param>
    public ConfigSafeModeRegion(bool safeMode)
    {
      this.prevSafeMode = new bool?(Config.SafeMode);
      Config.SafeMode = safeMode;
    }

    void IDisposable.Dispose()
    {
      if (!this.prevSafeMode.HasValue)
        return;
      Config.SafeMode = this.prevSafeMode.Value;
    }
  }
}
