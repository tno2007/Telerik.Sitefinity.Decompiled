// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.SecuritySettings
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Utilities;

namespace Telerik.Sitefinity.Security
{
  internal class SecuritySettings : ISecuritySettings
  {
    private ConcurrentProperty<IDictionary<string, RoleInfo>> appRoles;
    private IAppRolesStrategy appRolesStrategy;

    public SecuritySettings()
      : this((IAppRolesStrategy) new DefaultAppRolesStrategy())
    {
    }

    public SecuritySettings(IAppRolesStrategy appRolesStrategy)
    {
      this.appRolesStrategy = appRolesStrategy;
      this.appRoles = new ConcurrentProperty<IDictionary<string, RoleInfo>>(new Func<IDictionary<string, RoleInfo>>(this.BuildAppRoles));
      this.appRolesStrategy.Changed += new EventHandler(this.AppRolesStrategy_Changed);
    }

    public IDictionary<string, RoleInfo> AppRoles => this.appRoles.Value;

    public IEnumerable<RoleInfo> UnassignableRoles => this.AppRoles.Values.Where<RoleInfo>((Func<RoleInfo, bool>) (r => ((IEnumerable<string>) SecurityConstants.UnassignableRolesNames).Contains<string>(r.Name)));

    public void Reset() => this.appRoles.Reset();

    private IDictionary<string, RoleInfo> BuildAppRoles()
    {
      Dictionary<string, RoleInfo> dictionary = new Dictionary<string, RoleInfo>();
      foreach (RoleInfo role in this.appRolesStrategy.GetRoles())
        dictionary.Add(role.Name, role);
      return (IDictionary<string, RoleInfo>) dictionary;
    }

    private void AppRolesStrategy_Changed(object sender, EventArgs e) => this.Reset();
  }
}
