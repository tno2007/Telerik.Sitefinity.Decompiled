// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.DefaultAppRolesStrategy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Security.Configuration;

namespace Telerik.Sitefinity.Security
{
  internal class DefaultAppRolesStrategy : IAppRolesStrategy
  {
    public IEnumerable<RoleInfo> GetRoles() => Config.Get<SecurityConfig>().ApplicationRoles.Values.Select<ApplicationRole, RoleInfo>((Func<ApplicationRole, RoleInfo>) (c => new RoleInfo()
    {
      Id = c.Id,
      Name = c.Name,
      Provider = "AppRoles"
    }));

    public event EventHandler Changed;

    private void AppRolesChanged(ICacheDependencyHandler handler, Type item, string key)
    {
      if (this.Changed == null)
        return;
      this.Changed((object) this, EventArgs.Empty);
    }
  }
}
