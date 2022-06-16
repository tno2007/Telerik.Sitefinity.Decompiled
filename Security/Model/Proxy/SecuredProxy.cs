// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Model.Proxy.SecuredProxy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Security.Model.Proxy
{
  /// <summary>
  /// Holds information about a secured object without any tracking or persistence
  /// </summary>
  public class SecuredProxy : ISecuredObject, IOwnership
  {
    /// <summary>
    /// Create an in-memory copy of an existing secured object
    /// </summary>
    /// <param name="original">Secured object to copy</param>
    public SecuredProxy(ISecuredObject original)
    {
      if (original is IOwnership ownership)
        this.Owner = ownership.Owner;
      this.Id = original.Id;
      this.InheritsPermissions = original.InheritsPermissions;
      this.CanInheritPermissions = original.CanInheritPermissions;
      this.Permissions = (IList<Permission>) original.Permissions.Select<Permission, PermissionProxy>((Func<Permission, PermissionProxy>) (p => new PermissionProxy(p))).Cast<Permission>().ToList<Permission>();
      this.SupportedPermissionSets = original.SupportedPermissionSets;
      this.PermissionsetObjectTitleResKeys = original.PermissionsetObjectTitleResKeys;
      this.SecuredObjectType = original.GetType();
      if (!(original is IDataItem dataItem) || !(dataItem.Provider is DataProviderBase provider))
        return;
      this.ManagerType = provider.TheManagerType;
    }

    internal Type SecuredObjectType { get; private set; }

    internal Type ManagerType { get; private set; }

    /// <summary>Gets the identifier of the secured object.</summary>
    /// <value></value>
    public Guid Id { get; private set; }

    /// <summary>
    /// Defines if the implemented type inherits permissions from a parent object.
    /// </summary>
    /// <value></value>
    public bool InheritsPermissions { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance can inherit permissions.
    /// </summary>
    /// <value></value>
    public bool CanInheritPermissions { get; set; }

    /// <summary>Gets the permissions.</summary>
    /// <value>The permissions.</value>
    public IList<Permission> Permissions { get; private set; }

    /// <summary>
    /// Gets the permission sets relevant to this specific secured object.
    /// </summary>
    /// <value>The supported permission sets.</value>
    public string[] SupportedPermissionSets { get; set; }

    /// <summary>
    /// Gets a dictionary:
    /// Key is a name of a permission set supported by this provider,
    /// Value is a string title which is to be used for titles of permissions, if defined in resources as placeholders.
    /// </summary>
    /// <value>The permissionset object titles.</value>
    public IDictionary<string, string> PermissionsetObjectTitleResKeys { get; set; }

    /// <summary>
    /// Gets or sets the identity of the user that owns this item.
    /// </summary>
    /// <value></value>
    public Guid Owner { get; set; }
  }
}
