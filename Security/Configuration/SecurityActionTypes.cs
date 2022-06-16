// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Configuration.SecurityActionTypes
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Security.Configuration
{
  /// <summary>Most basic types of security actions</summary>
  [Flags]
  public enum SecurityActionTypes
  {
    /// <summary>Unknown (unspecified)</summary>
    None = 0,
    /// <summary>Grants/denies viewing an item</summary>
    View = 1,
    /// <summary>Grants/denies creating an item</summary>
    Create = 2,
    /// <summary>Grants/denies modifiying an item</summary>
    Modify = 4,
    /// <summary>
    /// Managing items. Used for modifying, creating and deleting specific item types, based on their parent permissions
    /// (e.g. "manage" a blog = modify blog and manage its posts = modify blog, and create, delete, and modify posts).
    /// </summary>
    Manage = 8,
    /// <summary>Grants/denies the deletion of an item</summary>
    Delete = 16, // 0x00000010
    /// <summary>Grants/denies changing the ownership of an item</summary>
    ChangeOwner = 32, // 0x00000020
    /// <summary>Grants/denies changing permissions of an item</summary>
    ChangePermissions = 64, // 0x00000040
    /// <summary>Grants/denies unlocking an item</summary>
    Unlock = 128, // 0x00000080
  }
}
