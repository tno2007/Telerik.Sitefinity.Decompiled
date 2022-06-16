// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.ParameterPermissionAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Diagnostics;

namespace Telerik.Sitefinity.Security
{
  /// <summary>Specifies required permissions for method parameters.</summary>
  [DebuggerDisplay("Parameter Attribute: {PermissionSet} = {Value}, {ParameterName}/{ParameterIndex}")]
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
  public sealed class ParameterPermissionAttribute : PermissionAttribute
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="!:ParemeterPermissionAttribute" /> class.
    /// </summary>
    /// <param name="permissionSetName">Name of the permission set to verify against.</param>
    /// <param name="actions">The actions to verify.</param>
    public ParameterPermissionAttribute(
      string parameterName,
      string permissionSetName,
      params string[] actions)
      : base(permissionSetName, actions)
    {
      this.ParameterName = parameterName;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ParemeterPermissionAttribute" /> class.
    /// </summary>
    /// <param name="parameterIndex">Index of the parameter.</param>
    /// <param name="permissionSetName">Name of the permission set.</param>
    /// <param name="actions">The actions.</param>
    public ParameterPermissionAttribute(
      int parameterIndex,
      string permissionSetName,
      params string[] actions)
      : base(permissionSetName, actions)
    {
      this.ParameterIndex = parameterIndex;
    }

    /// <summary>
    /// Gets or sets the name of the parameter to check permissions for.
    /// </summary>
    /// <value>The name of the parameter.</value>
    public string ParameterName { get; private set; }

    /// <summary>
    /// Gets or sets the index of the parameter to check permissions for.
    /// </summary>
    /// <value>The index of the parameter.</value>
    public int ParameterIndex { get; private set; }
  }
}
