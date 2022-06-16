// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.TransactionPermissionAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Security
{
  /// <summary>
  /// Specifies required permissions that will be checked against dirty items contained in data provider transaction.
  /// </summary>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
  public class TransactionPermissionAttribute : TypedPermissionAttribute
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.TransactionPermissionAttribute" /> class.
    /// </summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="permissionSetName">Name of the permission set to verify against.</param>
    /// <param name="actions">The actions to verify.</param>
    public TransactionPermissionAttribute(
      Type itemType,
      string permissionSetName,
      SecurityConstants.TransactionActionType actionType,
      params string[] actions)
      : this(itemType, permissionSetName, string.Empty, actionType, actions)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.TransactionPermissionAttribute" /> class.
    /// </summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="permissionSetName">Name of the permission set to verify against.</param>
    /// <param name="securedObjectPropertyName">If the object in the transaction is not ISecuredObject, this argument contains the name of the object's property, which a reference to the relevant ISecuredObject.</param>
    /// <param name="actionType">Type of the action, to relate to objects in the transaction.</param>
    /// <param name="actions">The actions to verify.</param>
    public TransactionPermissionAttribute(
      Type itemType,
      string permissionSetName,
      string securedObjectPropertyName,
      SecurityConstants.TransactionActionType actionType,
      params string[] actions)
      : base(itemType, permissionSetName, actions)
    {
      this.SecuredObjectPropertyName = securedObjectPropertyName;
      this.ActionType = actionType;
    }

    /// <summary>
    /// Gets or sets the name of the secured object property.
    /// If the object in the transaction is not ISecuredObject, this argument contains the name of the object's property, which a reference to the relevant ISecuredObject.
    /// </summary>
    /// <value>The name of the secured object property.</value>
    public string SecuredObjectPropertyName { get; private set; }

    /// <summary>
    /// Gets or sets the type of the action, to relate to objects in the transaction.
    /// </summary>
    /// <value>The type of the action.</value>
    public SecurityConstants.TransactionActionType ActionType { get; private set; }
  }
}
