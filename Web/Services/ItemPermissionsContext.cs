// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.ItemPermissionsContext`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Web.Services
{
  /// <summary>
  /// Provides context information for item that is exposed in a web service and set the permissions for that item
  /// </summary>
  /// <typeparam name="T">Type of the item</typeparam>
  [DataContract]
  public class ItemPermissionsContext<T> : ItemContext<T>
  {
    private bool? isEditable;

    /// <summary>
    /// Gets or sets whether users have permission to edit this content item
    /// </summary>
    [DataMember]
    public virtual bool IsEditable
    {
      get => !this.isEditable.HasValue ? this.IsContentItemEditable((object) this.Item as ISecuredObject) : this.isEditable.Value;
      set => this.isEditable = new bool?(value);
    }

    /// <summary>Checks if a secured object</summary>
    /// <param name="secObj">Checks if a secured object is modifiable (editable). Can be null.</param>
    /// <returns>true if modify is granted, or if secObj is null (it is not a secured object)</returns>
    protected virtual bool IsContentItemEditable(ISecuredObject secObj)
    {
      if (secObj == null)
        return true;
      string permissionSetName = secObj.SupportedPermissionSets.Length != 0 ? secObj.SupportedPermissionSets[0] : "";
      if (secObj.IsSecurityActionSupported(permissionSetName, SecurityActionTypes.Manage))
        return secObj.IsSecurityActionTypeGranted(permissionSetName, SecurityActionTypes.Manage);
      if (secObj.IsSecurityActionSupported(permissionSetName, SecurityActionTypes.Modify))
        return secObj.IsSecurityActionTypeGranted(permissionSetName, SecurityActionTypes.Modify);
      return secObj is IHasContentChildren && secObj.SupportedPermissionSets.Length != 0 && secObj.IsSecurityActionSupported(secObj.SupportedPermissionSets[1], SecurityActionTypes.Manage);
    }
  }
}
