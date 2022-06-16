﻿// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.TypedValuePermissionAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Diagnostics;

namespace Telerik.Sitefinity.Security
{
  /// <summary>Requests a permission</summary>
  [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
  [DebuggerDisplay("Value Attribute: {PermissionSetName} = {Value}, ItemType = {ItemType}")]
  public sealed class TypedValuePermissionAttribute : TypedPermissionAttribute
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.TypedValuePermissionAttribute" /> class.
    /// </summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="permissionSetName">Name of the permission set.</param>
    /// <param name="actions">The actions.</param>
    public TypedValuePermissionAttribute(
      Type itemType,
      string permissionSetName,
      params string[] actions)
      : base(itemType, permissionSetName, actions)
    {
    }
  }
}
