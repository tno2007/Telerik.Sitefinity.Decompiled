// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.ElevatedModeRegion
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Data
{
  public class ElevatedModeRegion : IDisposable
  {
    private IManager manager;
    private bool prevMode;

    public ElevatedModeRegion(IManager manager)
    {
      this.manager = manager != null ? manager : throw new ArgumentNullException(nameof (manager));
      if (this.manager.Provider == null)
        return;
      this.prevMode = this.manager.Provider.SuppressSecurityChecks;
      this.manager.Provider.SuppressSecurityChecks = true;
    }

    void IDisposable.Dispose()
    {
      if (this.manager == null || this.manager.Provider == null)
        return;
      this.manager.Provider.SuppressSecurityChecks = this.prevMode;
    }
  }
}
