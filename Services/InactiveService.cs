// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.InactiveService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Services
{
  /// <summary>Represents information about inactive system service.</summary>
  public sealed class InactiveService : ServiceBase
  {
    /// <summary>Loads and initializes this service.</summary>
    public override void Start()
    {
      if (this.Startup == StartupType.Disabled)
        throw new InvalidOperationException(Res.Get<ErrorMessages>().DisabledServiceCannotStart.Arrange((object) this.Name));
      if (this.Status != ServiceStatus.Stopped)
        return;
      SystemManager.InitializeService(this.Settings, true);
      base.Start();
    }

    public override Type[] Interfaces => (Type[]) null;
  }
}
