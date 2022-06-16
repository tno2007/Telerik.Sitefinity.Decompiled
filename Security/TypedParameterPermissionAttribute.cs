// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.TypedParameterPermissionAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Diagnostics;

namespace Telerik.Sitefinity.Security
{
  /// <summary>
  /// Will demand a permission for a parameter of specific type
  /// </summary>
  [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
  [DebuggerDisplay("Parameter Attribute: {PermissionSetName} = {Value}, {ParameterName}/{ParameterIndex}, ItemType = {ItemType}")]
  public sealed class TypedParameterPermissionAttribute : TypedPermissionAttribute
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.TypedParameterPermissionAttribute" /> class.
    /// </summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="parameterName">Name of the parameter.</param>
    /// <param name="permissionSetName">Name of the permission set.</param>
    /// <param name="actions">The actions.</param>
    public TypedParameterPermissionAttribute(
      Type itemType,
      string parameterName,
      string permissionSetName,
      params string[] actions)
      : base(itemType, permissionSetName, actions)
    {
      this.ParameterName = parameterName;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.TypedParameterPermissionAttribute" /> class.
    /// </summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="parameterIndex">Index of the parameter.</param>
    /// <param name="permissionSetName">Name of the permission set.</param>
    /// <param name="actions">The actions.</param>
    public TypedParameterPermissionAttribute(
      Type itemType,
      int parameterIndex,
      string permissionSetName,
      params string[] actions)
      : base(itemType, permissionSetName, actions)
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
