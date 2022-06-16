// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.TypedMethodPermissionAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Diagnostics;

namespace Telerik.Sitefinity.Security
{
  /// <summary>
  /// Specifies required permissions for a generic method execution,
  /// where the exact type of the generic parameter might require a different permission.
  /// </summary>
  /// <remarks>
  /// The attribute is taken into consideration with generic methods only. If the method
  /// is generic and you have both <see cref="T:Telerik.Sitefinity.Security.TypedMethodPermissionAttribute" /> and
  /// <see cref="T:Telerik.Sitefinity.Security.MethodPermissionAttribute" />, an exception will be thrown at the method
  /// invocation. Also, only the first generic argument is checked.
  /// </remarks>
  [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
  [DebuggerDisplay("Method Attribute: {PermissionSetName} = {Value}, ItemType = {ItemType}")]
  public sealed class TypedMethodPermissionAttribute : TypedPermissionAttribute
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.TypedMethodPermissionAttribute" /> class.
    /// </summary>
    /// <param name="itemType">Type of the method's generic argument.</param>
    /// <param name="permissionSetName">Name of the permission set.</param>
    /// <param name="actions">Security action names.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// When <paramref name="itemType" /> is <c>null</c>
    /// </exception>
    public TypedMethodPermissionAttribute(
      Type itemType,
      string permissionSetName,
      params string[] actions)
      : base(itemType, permissionSetName, actions)
    {
    }
  }
}
