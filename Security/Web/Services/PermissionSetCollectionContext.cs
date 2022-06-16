// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.Services.PermissionSetCollectionContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Security.Web.Services
{
  /// <summary>Extends CollectionContext with</summary>
  [DataContract(Name = "PermissionSetCollectionContext", Namespace = "Telerik.Sitefinity.Security.Web.Services")]
  public class PermissionSetCollectionContext : CollectionContext<WcfPermissionSetPermission>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.Web.Services.PermissionSetCollectionContext" /> class.
    /// </summary>
    /// <param name="permissions">The permissions.</param>
    /// <param name="editablePermissionSets">Permission sets that have their 'change permissions' action granted</param>
    /// <param name="isAdmin">Is the current user an adimistrator</param>
    /// <param name="secObj">Secured object from which to take CanInheritPermissions and InheritsPermissions</param>
    public PermissionSetCollectionContext(
      IEnumerable<WcfPermissionSetPermission> permissions,
      ISecuredObject secObj,
      bool isAdmin,
      string[] editablePermissionSets,
      Guid mainSecuredObjectId = default (Guid))
      : base(permissions)
    {
      this.CanInheritPermissions = secObj != null ? secObj.CanInheritPermissions : throw new ArgumentNullException();
      this.InheritsPermissions = secObj.InheritsPermissions;
      this.isCurrentPrincipalAdministrator = isAdmin;
      this.EditablePermissionSets = editablePermissionSets;
      if (mainSecuredObjectId == Guid.Empty)
      {
        this.ActualSecuredObjectId = Guid.Empty;
        this.SecuredObjectId = secObj.Id;
      }
      else
      {
        this.ActualSecuredObjectId = secObj.Id;
        this.SecuredObjectId = mainSecuredObjectId;
      }
    }

    /// <summary>
    /// Specify whether the secured object has broken the inheritance chain or not
    /// </summary>
    [DataMember]
    public virtual bool InheritsPermissions { get; set; }

    /// <summary>
    /// Specifies whether the secured object supports permission inheritance
    /// </summary>
    [DataMember]
    public virtual bool CanInheritPermissions { get; set; }

    [DataMember]
    public bool isCurrentPrincipalAdministrator { get; set; }

    [DataMember]
    public virtual string[] EditablePermissionSets { get; set; }

    [DataMember]
    public virtual Guid SecuredObjectId { get; set; }

    [DataMember]
    public virtual Guid ActualSecuredObjectId { get; set; }
  }
}
