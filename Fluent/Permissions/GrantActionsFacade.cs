// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Permissions.GrantActionsFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Fluent.Permissions
{
  /// <summary>
  /// Grants actions available in the General permissions set for the configured principals and secured object id.
  /// </summary>
  public class GrantActionsFacade : ActionsFacadeBase
  {
    /// <summary>
    /// Grants the specified <paramref name="action" /> for the specified <paramref name="permission" />.
    /// The implementation of this method should decide whether to grant or deny the specified action.
    /// </summary>
    /// <param name="action">The action to apply.</param>
    /// <param name="permission">A permission object.</param>
    protected override void ModifyActionOnPermission(string action, Permission permission)
    {
      permission.UndenyActions(action);
      permission.GrantActions(true, action);
    }
  }
}
