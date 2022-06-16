// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Events.RoleUnassigned
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Security.Events
{
  /// <summary>
  /// an event notifying that a role was unassigned to a user
  /// </summary>
  public class RoleUnassigned : RoleAssignEventBase
  {
    internal RoleUnassigned(UserLinkInfo userLinkInfo)
      : base(userLinkInfo)
    {
    }
  }
}
