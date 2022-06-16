// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Data.StaticRoot
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Security.Data
{
  /// <summary>
  /// This class is used to cache security root information.
  /// </summary>
  public class StaticRoot : ISecuredObject
  {
    private string[] supportedPermissionSets = new string[1]
    {
      "General"
    };
    private bool canInheritPermissions;
    private IDictionary<string, string> permissionsetObjectTitleResKeys = (IDictionary<string, string>) new Dictionary<string, string>();

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.Data.StaticRoot" /> class.
    /// </summary>
    /// <param name="root">The root.</param>
    public StaticRoot(SecurityRoot root)
    {
      List<Permission> list = new List<Permission>(root.Permissions.Count);
      foreach (Permission permission in (IEnumerable<Permission>) root.Permissions)
        list.Add((Permission) new StaticPermission(permission));
      this.Id = root.Id;
      this.Key = root.Key;
      this.Permissions = (IList<Permission>) new ReadOnlyCollection<Permission>((IList<Permission>) list);
      this.ManagerType = root.ManagerType;
      this.DataProviderName = root.DataProviderName;
      this.canInheritPermissions = root.CanInheritPermissions;
      this.permissionsetObjectTitleResKeys = (IDictionary<string, string>) new Dictionary<string, string>(root.PermissionsetObjectTitleResKeys);
      this.supportedPermissionSets = new List<string>((IEnumerable<string>) root.SupportedPermissionSets).ToArray();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.Data.StaticRoot" /> class with an ISecuredObject, in case it's initialized by an object of different type than SecurityRoot (e.g. Taxon)
    /// </summary>
    /// <param name="root">The root as a secured object.</param>
    public StaticRoot(ISecuredObject root)
    {
      List<Permission> list = new List<Permission>(root.Permissions.Count);
      foreach (Permission permission in (IEnumerable<Permission>) root.Permissions)
        list.Add((Permission) new StaticPermission(permission));
      this.Id = root.Id;
      this.Key = root.GetType().FullName;
      this.Permissions = (IList<Permission>) new ReadOnlyCollection<Permission>((IList<Permission>) list);
      this.ManagerType = (Type) null;
      this.DataProviderName = string.Empty;
      this.canInheritPermissions = root.CanInheritPermissions;
      this.permissionsetObjectTitleResKeys = root.PermissionsetObjectTitleResKeys;
    }

    /// <summary>Gets the key.</summary>
    /// <value>The key.</value>
    public string Key { get; private set; }

    /// <summary>
    /// Gets or sets the CLR type of the data manager for this security root.
    /// </summary>
    public virtual Type ManagerType { get; set; }

    /// <summary>
    /// Gets or sets the name of the data provider for this security root.
    /// </summary>
    public virtual string DataProviderName { get; set; }

    /// <summary>Gets the identifier of the secured object.</summary>
    /// <value></value>
    public Guid Id { get; private set; }

    /// <summary>
    /// Defines if the implemented type inherits permissions from a parent object.
    /// </summary>
    /// <value></value>
    public bool InheritsPermissions
    {
      get => false;
      set => throw new NotSupportedException();
    }

    /// <summary>Gets the permissions for this object.</summary>
    /// <value>A list of defined permissions.</value>
    public IList<Permission> Permissions { get; private set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance can inherit permissions.
    /// </summary>
    /// <value></value>
    public bool CanInheritPermissions
    {
      get => this.canInheritPermissions;
      set => this.canInheritPermissions = value;
    }

    /// <summary>
    /// Gets the permission sets relevant to this specific secured object.
    /// </summary>
    /// <value>The supported permission sets.</value>
    public string[] SupportedPermissionSets
    {
      get => this.supportedPermissionSets;
      set => this.supportedPermissionSets = value;
    }

    /// <summary>
    /// Gets a dictionary:
    /// Key is a name of a permission set supported by this provider,
    /// Value is a resource key of the SecurityResources title which is to be used for titles of permissions, if defined in resources as placeholders.
    /// </summary>
    /// <value>The permissionset object titles.</value>
    public virtual IDictionary<string, string> PermissionsetObjectTitleResKeys
    {
      get => this.permissionsetObjectTitleResKeys;
      set => this.permissionsetObjectTitleResKeys = value;
    }
  }
}
