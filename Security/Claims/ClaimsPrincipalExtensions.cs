// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Claims.ClaimsPrincipalExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Security.Claims;

namespace Telerik.Sitefinity.Security.Claims
{
  /// <summary>Extension methods for ClaimsPrincipal</summary>
  public static class ClaimsPrincipalExtensions
  {
    /// <summary>
    /// Checks whether the given principal is in a specific role by role id.
    /// </summary>
    /// <param name="principal">The principal.</param>
    /// <param name="roleId">The id of the role.</param>
    /// <returns>True if is in the specified role, False otherwise.</returns>
    public static bool IsInRole(this ClaimsPrincipal principal, Guid roleId)
    {
      if (roleId == SecurityManager.EveryoneRole.Id)
        return true;
      foreach (ClaimsIdentity identity in principal.Identities)
      {
        if (identity is SitefinityIdentity sitefinityIdentity && sitefinityIdentity.Roles.Any<RoleInfo>((Func<RoleInfo, bool>) (e => e.Id.Equals(roleId))))
          return true;
      }
      return false;
    }
  }
}
