// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.IAccessPermission
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Security
{
  /// <summary>Defines properties implemented by permission types.</summary>
  public interface IAccessPermission
  {
    /// <summary>Gets or sets the identifier of the secured object.</summary>
    Guid SecuredObjectId { get; set; }

    /// <summary>
    /// Gets or sets the identifier of a principal (user, role or other organization unit).
    /// </summary>
    string PrincipalKey { get; set; }

    /// <summary>
    /// Gets or sets a number indicating the type of the principal.
    /// 0 and 1 are reserved corresponding to Role and User.
    /// Custom principal types should be assigned with numbers above 125.
    /// </summary>
    byte PrincipalType { get; set; }

    /// <summary>Gets or sets granted rights</summary>
    int Grant { get; set; }

    /// <summary>Gets or sets denied rights</summary>
    int Deny { get; set; }
  }
}
