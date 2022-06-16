// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.EnumeratorPermissionAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Diagnostics;

namespace Telerik.Sitefinity.Security
{
  /// <summary>
  /// Specifies required permissions for the items in an IEnumerable method value.
  /// </summary>
  [DebuggerDisplay("Enumerator Attribute: {PermissionSetName} = {Value}")]
  public sealed class EnumeratorPermissionAttribute : PermissionAttribute
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.EnumeratorPermissionAttribute" /> class.
    /// </summary>
    /// <param name="permissionSetName">Name of the permission set to verify against.</param>
    /// <param name="actions">The actions to verify.</param>
    public EnumeratorPermissionAttribute(string permissionSetName, params string[] actions)
      : base(permissionSetName, actions)
    {
    }
  }
}
