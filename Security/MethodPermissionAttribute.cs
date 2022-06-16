// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.MethodPermissionAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Diagnostics;

namespace Telerik.Sitefinity.Security
{
  /// <summary>
  /// Specifies required permissions to allow method execution.
  /// </summary>
  [AttributeUsage(AttributeTargets.Method)]
  [DebuggerDisplay("Method Attribute: {PermissionSetName} = {Value}")]
  public sealed class MethodPermissionAttribute : PermissionAttribute
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.MethodPermissionAttribute" /> class.
    /// </summary>
    /// <param name="permissionSetName">Name of the permission set to verify against.</param>
    /// <param name="actions">The actions to verify.</param>
    public MethodPermissionAttribute(string permissionSetName, params string[] actions)
      : base(permissionSetName, actions)
    {
    }
  }
}
