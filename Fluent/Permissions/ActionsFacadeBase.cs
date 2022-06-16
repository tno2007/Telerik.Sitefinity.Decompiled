// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Permissions.ActionsFacadeBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Fluent.Permissions
{
  /// <summary>
  /// The base actions facade that is used to grant or deny actions on a SecuredObject.
  /// </summary>
  public abstract class ActionsFacadeBase : IActionsFacade
  {
    private IList<Guid> principalIds;

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

    /// <summary>
    /// Gets or sets the <see cref="T:Telerik.Sitefinity.Security.Model.ISecuredObject" /> item which the facade is using.
    /// </summary>
    /// <value>The securable item that the facade is operating on.</value>
    public ISecuredObject SecuredItem { get; set; }

    /// <summary>Grants view action for the configured principals.</summary>
    /// <returns>A facade implementing role specific permissions operations.</returns>
    public IPermissionsForPrincipalFacade View() => this.Ensure(nameof (View));

    /// <summary>
    /// Grants or denies create action for the configured principals.
    /// </summary>
    /// <returns>A facade implementing role specific permissions operations.</returns>
    /// <remarks>Create action only applies to parent secured objects(e.g. dynamic content type) to define the child object behavior.</remarks>
    public IPermissionsForPrincipalFacade Create() => this.Ensure(nameof (Create));

    /// <summary>Grants modify action for the configured principals.</summary>
    /// <returns>A facade implementing role specific permissions operations.</returns>
    public IPermissionsForPrincipalFacade Modify() => this.Ensure(nameof (Modify));

    /// <summary>Grants delete action for the configured principals.</summary>
    /// <returns>A facade implementing role specific permissions operations.</returns>
    public IPermissionsForPrincipalFacade Delete() => this.Ensure(nameof (Delete));

    /// <summary>
    /// Grants 'change owner' action for the configured principals.
    /// </summary>
    /// <returns>A facade implementing role specific permissions operations.</returns>
    public IPermissionsForPrincipalFacade ChangeOwner() => this.Ensure(nameof (ChangeOwner));

    /// <summary>
    /// Grants 'change permissions' action for the configured principals.
    /// </summary>
    /// <returns>A facade implementing role specific permissions operations.</returns>
    public IPermissionsForPrincipalFacade ChangePermissions() => this.Ensure(nameof (ChangePermissions));

    /// <summary>Occurs when a permission is created.</summary>
    internal event Action<object, PermissionCreatedEventArgs> PermissionCreated;

    /// <summary>Sets the plug-in for this facade.</summary>
    /// <param name="plugin">The plug-in.</param>
    /// <returns>A facade implementing role specific permissions operations.</returns>
    internal IPermissionsForPrincipalFacade SetPlugin(
      IActionsFacadePlugin plugin)
    {
      plugin.Subscribe(this);
      return this.GetPermissionsForPrincipalFacade();
    }

    /// <summary>
    /// Ensures the specified action is applied for the secured item and principals
    /// that are part of the state of the facade.
    /// </summary>
    /// <param name="action">The action.</param>
    /// <returns>A facade implementing role specific permissions operations.</returns>
    protected virtual IPermissionsForPrincipalFacade Ensure(
      string action)
    {
      this.EnsureSecuredObjectSupportsGeneralPermissionSet();
      DataProviderBase objectDataProvider = this.GetSecuredObjectDataProvider();
      foreach (Guid principalId in (IEnumerable<Guid>) this.PrincipalIds)
      {
        Permission permissionForPrincipal = this.GetOrCreatePermissionForPrincipal(principalId, objectDataProvider);
        this.ModifyActionOnPermission(action, permissionForPrincipal);
      }
      return this.GetPermissionsForPrincipalFacade();
    }

    /// <summary>
    /// Modifies the specified <paramref name="action" /> for the specified <paramref name="permission" />.
    /// The implementation of this method should decide how and whether to grant or deny the specified action.
    /// </summary>
    /// <param name="action">The action to apply.</param>
    /// <param name="permission">The permission object that will be modified.</param>
    protected abstract void ModifyActionOnPermission(string action, Permission permission);

    private IPermissionsForPrincipalFacade GetPermissionsForPrincipalFacade()
    {
      IPermissionsForPrincipalFacade forPrincipalFacade = ObjectFactory.Container.Resolve<IPermissionsForPrincipalFacade>((ResolverOverride) new ParameterOverride("securedObject", (object) this.SecuredItem));
      forPrincipalFacade.PrincipalIds = this.PrincipalIds;
      return forPrincipalFacade;
    }

    private Permission GetOrCreatePermissionForPrincipal(
      Guid principalId,
      DataProviderBase dataProvider)
    {
      Permission permissionForPrincipal = this.SecuredItem.Permissions.Where<Permission>((Func<Permission, bool>) (perm => perm.SetName.Equals("General") && perm.ObjectId.Equals(this.SecuredItem.Id) && perm.PrincipalId.Equals(principalId))).SingleOrDefault<Permission>();
      if (permissionForPrincipal == null)
      {
        permissionForPrincipal = dataProvider.CreatePermission("General", this.SecuredItem.Id, principalId);
        this.SecuredItem.Permissions.Add(permissionForPrincipal);
        this.RaisePermissionCreated(permissionForPrincipal);
      }
      return permissionForPrincipal;
    }

    private void RaisePermissionCreated(Permission permissionForPrincipal)
    {
      if (this.PermissionCreated == null)
        return;
      this.PermissionCreated((object) this, new PermissionCreatedEventArgs()
      {
        CreatedPermission = permissionForPrincipal
      });
    }

    private DataProviderBase GetSecuredObjectDataProvider() => (DataProviderBase) ((IDataItem) this.SecuredItem).Provider;

    private void EnsureSecuredObjectSupportsGeneralPermissionSet()
    {
      if (!((IEnumerable<string>) this.SecuredItem.SupportedPermissionSets).Contains<string>("General"))
        throw new InvalidOperationException(string.Format("Can't modify permissions for item of type: '{0}' that do not support General permission set.", (object) this.SecuredItem.GetType().FullName));
    }
  }
}
