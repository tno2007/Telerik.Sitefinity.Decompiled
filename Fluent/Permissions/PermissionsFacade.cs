// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Permissions.PermissionsFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Data;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.SitefinityExceptions;

namespace Telerik.Sitefinity.Fluent.Permissions
{
  /// <summary>Facade that manages secured object's permissions.</summary>
  public class PermissionsFacade : IPermissionsFacade
  {
    private IList<Guid> principalIds;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Permissions.PermissionsFacade" /> class.
    /// </summary>
    public PermissionsFacade()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Permissions.PermissionsFacade" /> class.
    /// </summary>
    /// <param name="securedObject">The secured object.</param>
    public PermissionsFacade(ISecuredObject securedObject) => this.SecuredItem = securedObject;

    /// <summary>
    /// Specifies a role by its id that will be used to perform operations on the permissions collection of the current item.
    /// </summary>
    /// <param name="roleId">The role id.</param>
    /// <returns>A facade implementing role specific permissions operations.</returns>
    public IPermissionsForPrincipalFacade ForRole(Guid roleId)
    {
      if (roleId == Guid.Empty)
        throw new ArgumentNullException(nameof (roleId));
      this.PrincipalIds.Add(roleId);
      return this.GetPrincipalFacade();
    }

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
    public IPermissionsForPrincipalFacade ForRole(
      string roleName,
      bool throwException = true)
    {
      if (roleName.IsNullOrEmpty())
        throw new ArgumentNullException(nameof (roleName));
      return this.ForRole((Func<RoleDataProvider, Role>) (roleProvider => roleProvider.GetRole(roleName)), throwException);
    }

    /// <summary>
    /// Specifies a role by its name and provider name that will be used to perform operations on the permissions collection of the current item.
    /// </summary>
    /// <param name="roleName">The role name.</param>
    /// <param name="roleProviderName">The role provider in which to look for.</param>
    /// <param name="throwException">Indicates whether exception must be thrown when the role is not found in the specified provider.</param>
    /// <returns>A facade implementing role specific permissions operations.</returns>
    public IPermissionsForPrincipalFacade ForRole(
      string roleName,
      string roleProviderName,
      bool throwException = true)
    {
      if (roleName.IsNullOrEmpty())
        throw new ArgumentNullException(nameof (roleName));
      return this.ForRole(roleProviderName, (Func<RoleManager, Role>) (roleManager => roleManager.GetRole(roleName)), throwException);
    }

    /// <summary>
    /// Specifies a delegate function which provides the role for which actions will be granted or denied.
    /// </summary>
    /// <param name="getRole">The role that match the criteria.</param>
    /// <param name="throwException">Indicates whether exception must be thrown when the role is not found in any provider.</param>
    /// <returns>A facade implementing role specific permissions operations.</returns>
    public IPermissionsForPrincipalFacade ForRole(
      Func<RoleDataProvider, Role> getRole,
      bool throwException = false)
    {
      ProvidersCollection<RoleDataProvider> staticProviders = RoleManager.GetManager().StaticProviders;
      bool flag = false;
      foreach (RoleDataProvider roleDataProvider in (Collection<RoleDataProvider>) staticProviders)
      {
        Role role = getRole(roleDataProvider);
        if (role != null)
        {
          this.PrincipalIds.Add(role.Id);
          flag = true;
        }
      }
      if (flag)
        return this.GetPrincipalFacade();
      if (throwException)
        throw new ItemNotFoundException(string.Format("No role was found in any of the configured providers."));
      return this.GetEmptyPrincipalFacade();
    }

    /// <summary>
    /// Specifies a delegate function which provides the role for which actions will be granted or denied.
    /// </summary>
    /// <param name="roleProviderName">The role provider in which to look for.</param>
    /// <param name="getRole">The get role delegate function.</param>
    /// <param name="throwException">Indicates whether exception must be thrown when the role is not found in the specified provider.</param>
    /// <returns>A facade implementing role specific permissions operations.</returns>
    public IPermissionsForPrincipalFacade ForRole(
      string roleProviderName,
      Func<RoleManager, Role> getRole,
      bool throwException = false)
    {
      RoleManager manager = RoleManager.GetManager(roleProviderName);
      Role role = getRole(manager);
      if (role != null)
      {
        this.PrincipalIds.Add(role.Id);
        return this.GetPrincipalFacade();
      }
      if (throwException)
        throw new ItemNotFoundException(string.Format("No role with was found in the provider: {0}.", (object) roleProviderName));
      return this.GetEmptyPrincipalFacade();
    }

    /// <summary>
    /// Specifies a user by its id that will be used to perform operations on the permissions collection of the current item.
    /// </summary>
    /// <param name="userId">The user id.</param>
    /// <returns>A facade implementing role specific permissions operations.</returns>
    public IPermissionsForPrincipalFacade ForUser(Guid userId)
    {
      this.PrincipalIds.Add(userId);
      return this.GetPrincipalFacade();
    }

    /// <summary>
    /// Specifies a user for which actions will be granted or denied.
    /// </summary>
    /// <param name="userName">The user name.</param>
    /// <remarks>
    /// It's better to use the overload with userProviderName otherwise all configured membership providers will be iterated which
    /// depending on the providers speed is a potential performance bottleneck.
    /// </remarks>
    /// <returns>A facade implementing role specific permissions operations.</returns>
    public IPermissionsForPrincipalFacade ForUser(string userName) => throw new NotImplementedException();

    /// <summary>
    /// Specifies a user for which actions will be granted or denied.
    /// </summary>
    /// <param name="userName">The user name.</param>
    /// <param name="userProviderName">The user provider in which to look for.</param>
    /// <returns>A facade implementing role specific permissions operations.</returns>
    public IPermissionsForPrincipalFacade ForUser(
      string userName,
      string userProviderName)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Clears the grant and deny values of the secured item's own permissions.
    /// Breaks the permission inheritance if the secured object is inheriting its permissions from a permission parent.
    /// </summary>
    /// <returns>A facade that manages secured object's permissions.</returns>
    public IPermissionsFacade ClearAll()
    {
      if (this.SecuredItem == null)
        throw new InvalidOperationException("No secured object configured.");
      ObjectFactory.Resolve<PermissionInheritanceManagementFacade>().BreakInheritance(this.SecuredItem);
      foreach (Permission ownPermission in this.SecuredItem.GetOwnPermissions())
      {
        ownPermission.Grant = 0;
        ownPermission.Deny = 0;
      }
      return (IPermissionsFacade) this;
    }

    /// <summary>
    /// Gets or sets the <see cref="T:Telerik.Sitefinity.Security.Model.ISecuredObject" /> item which the facade is using.
    /// </summary>
    /// <value>The securable item that the facade is operating on.</value>
    public ISecuredObject SecuredItem { get; set; }

    /// <summary>Gets or sets the principal id.</summary>
    /// <value>The principal id.</value>
    public IList<Guid> PrincipalIds
    {
      get
      {
        if (this.principalIds == null)
          this.principalIds = (IList<Guid>) new List<Guid>();
        return this.principalIds;
      }
      set => this.principalIds = value;
    }

    private IPermissionsForPrincipalFacade GetPrincipalFacade()
    {
      IPermissionsForPrincipalFacade principalFacade = ObjectFactory.Container.Resolve<IPermissionsForPrincipalFacade>((ResolverOverride) new ParameterOverride("securedObject", (object) this.SecuredItem));
      principalFacade.PrincipalIds = this.PrincipalIds;
      return principalFacade;
    }

    private IPermissionsForPrincipalFacade GetEmptyPrincipalFacade() => ObjectFactory.Resolve<IPermissionsForPrincipalFacade>("Empty");
  }
}
