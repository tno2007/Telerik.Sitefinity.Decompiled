// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.IPermissionApplier
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Security
{
  /// <summary>
  /// Represents interface for objects that perform security checks on items.
  /// </summary>
  public interface IPermissionApplier
  {
    /// <summary>
    /// Gets or sets the information about the permission that should be applied.
    /// </summary>
    /// <value>The permission.</value>
    PermissionAttribute[] PermissionsInfo { get; set; }
  }
}
