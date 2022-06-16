// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Permissions.EmptyPermissionsFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Data;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Fluent.Permissions
{
  /// <summary>
  /// A mocked facade that is used when no actions should be performed on a specific item or another facade.
  /// </summary>
  internal class EmptyPermissionsFacade : IPermissionsFacade
  {
    /// <summary>Does not perform any actions.</summary>
    /// <param name="roleId">The role id.</param>
    /// <returns>An empty facade that does not perform any actions.</returns>
    public IPermissionsForPrincipalFacade ForRole(Guid roleId) => this.GetEmptyPermissionsForPrincipalFacade();

    /// <summary>Does not perform any actions.</summary>
    /// <param name="roleName">The role name.</param>
    /// <param name="throwException">Indicates whether exception must be thrown when the role is not found in any provider.</param>
    /// <returns>An empty facade that does not perform any actions.</returns>
    public IPermissionsForPrincipalFacade ForRole(
      string roleName,
      bool throwException = true)
    {
      return this.GetEmptyPermissionsForPrincipalFacade();
    }

    /// <summary>Does not perform any actions.</summary>
    /// <param name="roleName">The role name.</param>
    /// <param name="roleProviderName">The role provider in which to look for.</param>
    /// <param name="throwException">Indicates whether exception must be thrown when the role is not found in the specified provider.</param>
    /// <returns>An empty facade that does not perform any actions.</returns>
    public IPermissionsForPrincipalFacade ForRole(
      string roleName,
      string roleProviderName,
      bool throwException = true)
    {
      return this.GetEmptyPermissionsForPrincipalFacade();
    }

    /// <summary>Does not perform any actions.</summary>
    /// <param name="getRole">The role that match the criteria.</param>
    /// <param name="throwException">Indicates whether exception must be thrown when the role is not found in any provider.</param>
    /// <returns>An empty facade that does not perform any actions.</returns>
    public IPermissionsForPrincipalFacade ForRole(
      Func<RoleDataProvider, Role> getRole,
      bool throwException = false)
    {
      return this.GetEmptyPermissionsForPrincipalFacade();
    }

    /// <summary>Does not perform any actions.</summary>
    /// <param name="roleProviderName">The role provider in which to look for.</param>
    /// <param name="getRole">The get role delegate function.</param>
    /// <param name="throwException">Indicates whether exception must be thrown when the role is not found in the specified provider.</param>
    /// <returns>An empty facade that does not perform any actions.</returns>
    public IPermissionsForPrincipalFacade ForRole(
      string roleProviderName,
      Func<RoleManager, Role> getRole,
      bool throwException = false)
    {
      return this.GetEmptyPermissionsForPrincipalFacade();
    }

    /// <summary>
    /// Specifies a user for which actions will be granted or denied.
    /// </summary>
    /// <param name="userId">The user id.</param>
    /// <returns>A facade implementing role specific permissions operations.</returns>
    public IPermissionsForPrincipalFacade ForUser(Guid userId) => this.GetEmptyPermissionsForPrincipalFacade();

    /// <summary>Does not perform any actions.</summary>
    /// <param name="userName">The user name.</param>
    /// <returns>An empty facade that does not perform any actions.</returns>
    public IPermissionsForPrincipalFacade ForUser(string userName) => this.GetEmptyPermissionsForPrincipalFacade();

    /// <summary>Does not perform any actions.</summary>
    /// <param name="userName">The user name.</param>
    /// <param name="userProviderName">The user provider in which to look for.</param>
    /// <returns>An empty facade that does not perform any actions.</returns>
    public IPermissionsForPrincipalFacade ForUser(
      string userName,
      string userProviderName)
    {
      return this.GetEmptyPermissionsForPrincipalFacade();
    }

    /// <summary>Does not perform any actions.</summary>
    /// <returns>An empty facade that does not perform any actions.</returns>
    public IPermissionsFacade ClearAll() => (IPermissionsFacade) this;

    /// <summary>
    /// Gets or sets the <see cref="T:Telerik.Sitefinity.Security.Model.ISecuredObject" /> item which the facade is using.
    /// </summary>
    /// <value>The securable item that the facade is operating on.</value>
    public ISecuredObject SecuredItem
    {
      get => (ISecuredObject) null;
      set
      {
      }
    }

    /// <summary>Gets the empty permissions for principal facade.</summary>
    /// <returns>An empty permissions for principal facade.</returns>
    private IPermissionsForPrincipalFacade GetEmptyPermissionsForPrincipalFacade() => ObjectFactory.Resolve<IPermissionsForPrincipalFacade>("Empty");
  }
}
