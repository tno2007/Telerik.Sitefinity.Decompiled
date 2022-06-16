// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.ElevatedConfigModeRegion
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Configuration
{
  /// <summary>
  /// Region where configuration permissions are not checked.
  /// </summary>
  public sealed class ElevatedConfigModeRegion : IDisposable
  {
    private readonly bool prevMode;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Configuration.ElevatedConfigModeRegion" /> class.
    /// </summary>
    public ElevatedConfigModeRegion()
    {
      this.prevMode = ConfigProvider.DisableSecurityChecks;
      ConfigProvider.DisableSecurityChecks = true;
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    void IDisposable.Dispose() => ConfigProvider.DisableSecurityChecks = this.prevMode;
  }
}
