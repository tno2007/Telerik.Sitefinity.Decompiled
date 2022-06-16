// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Permissions.IPermissionsFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Data;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Fluent.Permissions
{
  /// <summary>
  /// Defines the common contract for facades that manage data items permissions.
  /// </summary>
  public interface IPermissionsFacade
  {
    /// <summary>
    /// Specifies a role for which actions will be granted or denied.
    /// </summary>
    /// <remarks>
    /// It's better to use the overload with roleProviderName otherwise all configured role providers will be iterated which
    /// depending on the providers speed is a potential performance bottleneck.
    /// </remarks>
    /// <param name="roleId">The role id.</param>
    /// <returns>A facade implementing role specific permissions operations.</returns>
    IPermissionsForPrincipalFacade ForRole(Guid roleId);

    /// <summary>
    /// Specifies a role for which actions will be granted or denied.
    /// </summary>
    /// <param name="roleName">The role name.</param>
    /// <param name="throwException">Indicates whether exception must be thrown when the role is not found in any provider.</param>
    /// <remarks>
    /// It's better to use the overload with roleProviderName otherwise all configured role providers will be iterated which
    /// depending on the providers speed is a potential performance bottleneck.
    /// </remarks>
    /// <returns>A facade implementing role specific permissions operations.</returns>
    IPermissionsForPrincipalFacade ForRole(
      string roleName,
      bool throwException = true);

    /// <summary>
    /// Specifies a role for which actions will be granted or denied.
    /// </summary>
    /// <param name="roleName">The role name.</param>
    /// <param name="roleProviderName">The role provider in which to look for.</param>
    /// <param name="throwException">Indicates whether exception must be thrown when the role is not found in the specified provider.</param>
    /// <returns>A facade implementing role specific permissions operations.</returns>
    IPermissionsForPrincipalFacade ForRole(
      string roleName,
      string roleProviderName,
      bool throwException = true);

    /// <summary>
    /// Specifies a delegate function which provides the role for which actions will be granted or denied.
    /// </summary>
    /// <param name="getRole">The role that match the criteria.</param>
    /// <param name="throwException">Indicates whether exception must be thrown when the role is not found in any provider.</param>
    /// <returns>A facade implementing role specific permissions operations.</returns>
    IPermissionsForPrincipalFacade ForRole(
      Func<RoleDataProvider, Role> getRole,
      bool throwException = false);

    /// <summary>
    /// Specifies a delegate function which provides the role for which actions will be granted or denied.
    /// </summary>
    /// <param name="roleProviderName">The role provider in which to look for.</param>
    /// <param name="getRole">The get role delegate function.</param>
    /// <param name="throwException">Indicates whether exception must be thrown when the role is not found in the specified provider.</param>
    /// <returns>A facade implementing role specific permissions operations.</returns>
    IPermissionsForPrincipalFacade ForRole(
      string roleProviderName,
      Func<RoleManager, Role> getRole,
      bool throwException = false);

    /// <summary>
    /// Specifies a user for which actions will be granted or denied.
    /// </summary>
    /// <param name="userId">The user id.</param>
    /// <returns>A facade implementing role specific permissions operations.</returns>
    IPermissionsForPrincipalFacade ForUser(Guid userId);

    /// <summary>
    /// Specifies a user for which actions will be granted or denied.
    /// </summary>
    /// <remarks>
    /// It's better to use the overload with userProviderName otherwise all configured membership providers will be iterated which
    /// depending on the providers speed is a potential performance bottleneck.
    /// </remarks>
    /// <param name="userName">The user name.</param>
    /// <returns>A facade implementing role specific permissions operations.</returns>
    IPermissionsForPrincipalFacade ForUser(string userName);

    /// <summary>
    /// Specifies a user for which actions will be granted or denied.
    /// </summary>
    /// <param name="userName">The user name.</param>
    /// <param name="userProviderName">The user provider in which to look for.</param>
    /// <returns>A facade implementing role specific permissions operations.</returns>
    IPermissionsForPrincipalFacade ForUser(
      string userName,
      string userProviderName);

    /// <summary>
    /// Clears the grant and deny values of the secured item's own permissions.
    /// Breaks the permission inheritance if the secured object is inheriting its permissions from a permission parent.
    /// </summary>
    /// <returns>A facade that manages secured object's permissions.</returns>
    IPermissionsFacade ClearAll();

    /// <summary>
    /// Gets or sets the <see cref="T:Telerik.Sitefinity.Security.Model.ISecuredObject" /> item which the facade is using.
    /// </summary>
    /// <value>The securable item that the facade is operating on.</value>
    ISecuredObject SecuredItem { get; set; }
  }
}
