// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.TypedPermissionAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Security
{
  /// <summary>Demand permissions, filtering on the item type</summary>
  public abstract class TypedPermissionAttribute : PermissionAttribute
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.TypedPermissionAttribute" /> class.
    /// </summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="permissionSetName">Name of the permission set.</param>
    /// <param name="actions">The actions.</param>
    public TypedPermissionAttribute(
      Type itemType,
      string permissionSetName,
      params string[] actions)
      : base(permissionSetName, actions)
    {
      this.ItemType = itemType;
    }

    /// <summary>
    /// Type filter -&gt; only items with this type will demand this permission
    /// </summary>
    public Type ItemType { get; private set; }
  }
}
