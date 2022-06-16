// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.SecuredObjectInfo
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Holds information about a secured object without any tracking or persistence and can be used in asp.net markup
  /// </summary>
  [PersistChildren(false)]
  [ParseChildren(true)]
  public class SecuredObjectInfo : ISecuredObject, IOwnership
  {
    private List<Telerik.Sitefinity.Security.Model.Permission> permissions;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.SecuredObjectInfo" /> class.
    /// </summary>
    public SecuredObjectInfo()
    {
    }

    /// <summary>
    /// Create an in-memory copy of an existing secured object
    /// </summary>
    /// <param name="original">Secured object to copy</param>
    public SecuredObjectInfo(ISecuredObject original)
    {
      if (original is IOwnership ownership)
        this.Owner = ownership.Owner;
      this.Id = original.Id;
      this.InheritsPermissions = original.InheritsPermissions;
      this.CanInheritPermissions = original.CanInheritPermissions;
      this.Permissions = original.Permissions.Select<Telerik.Sitefinity.Security.Model.Permission, PermissionInfo>((Func<Telerik.Sitefinity.Security.Model.Permission, PermissionInfo>) (p => new PermissionInfo(p))).Cast<Telerik.Sitefinity.Security.Model.Permission>().ToList<Telerik.Sitefinity.Security.Model.Permission>();
      this.SupportedPermissionSets = original.SupportedPermissionSets;
      this.PermissionsetObjectTitleResKeys = original.PermissionsetObjectTitleResKeys.ToDictionary<KeyValuePair<string, string>, string, string>((Func<KeyValuePair<string, string>, string>) (e => e.Key), (Func<KeyValuePair<string, string>, string>) (e => e.Value));
    }

    /// <summary>Gets the identifier of the secured object.</summary>
    /// <value></value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Guid Id { get; private set; }

    /// <summary>
    /// Gets or sets the unique identifier for this permission.
    /// </summary>
    /// <value>The id.</value>
    public Guid ObjectId
    {
      get => this.Id;
      set => this.Id = value;
    }

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
    IList<Telerik.Sitefinity.Security.Model.Permission> ISecuredObject.Permissions => (IList<Telerik.Sitefinity.Security.Model.Permission>) this.Permissions;

    /// <summary>Gets the permissions.</summary>
    /// <value>The permissions.</value>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public List<Telerik.Sitefinity.Security.Model.Permission> Permissions
    {
      get
      {
        if (this.permissions == null)
          this.permissions = new List<Telerik.Sitefinity.Security.Model.Permission>();
        return this.permissions;
      }
      set => this.permissions = value;
    }

    /// <summary>
    /// Gets the permission sets relevant to this specific secured object.
    /// </summary>
    /// <value>The supported permission sets.</value>
    [TypeConverter(typeof (StringArrayConverter))]
    public string[] SupportedPermissionSets { get; set; }

    /// <summary>
    /// Gets a dictionary:
    /// Key is a name of a permission set supported by this provider,
    /// Value is a string title which is to be used for titles of permissions, if defined in resources as placeholders.
    /// </summary>
    /// <value>The permissionset object titles.</value>
    IDictionary<string, string> ISecuredObject.PermissionsetObjectTitleResKeys
    {
      get => (IDictionary<string, string>) this.PermissionsetObjectTitleResKeys;
      set => this.PermissionsetObjectTitleResKeys = (Dictionary<string, string>) value;
    }

    /// <summary>
    /// Gets a dictionary:
    /// Key is a name of a permission set supported by this provider,
    /// Value is a string title which is to be used for titles of permissions, if defined in resources as placeholders.
    /// </summary>
    /// <value>The permissionset object titles.</value>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public Dictionary<string, string> PermissionsetObjectTitleResKeys { get; set; }

    /// <summary>
    /// Gets or sets the identity of the user that owns this item.
    /// </summary>
    /// <value></value>
    public Guid Owner { get; set; }
  }
}
