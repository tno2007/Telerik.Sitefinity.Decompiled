// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Events.UserLinkInfo
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Security.Data;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Security.Events
{
  internal class UserLinkInfo
  {
    internal UserLinkInfo(UserLink userLink, RoleDataProvider provider)
      : this(userLink, userLink.Role, provider)
    {
    }

    internal UserLinkInfo(UserLink userLink, Role role, RoleDataProvider provider)
    {
      if (role != null)
      {
        this.RoleId = role.Id;
        this.RoleName = role.Name;
      }
      this.RoleProviderName = provider.Name;
      this.UserId = userLink.UserId;
      this.MembershipProviderName = userLink.MembershipManagerInfo.ProviderName;
    }

    public Guid RoleId { get; set; }

    public string RoleName { get; set; }

    public string RoleProviderName { get; set; }

    public Guid UserId { get; set; }

    public string MembershipProviderName { get; set; }
  }
}
