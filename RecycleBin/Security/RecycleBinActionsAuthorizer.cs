// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.RecycleBin.Security.RecycleBinActionsAuthorizer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.RecycleBin.Security
{
  internal class RecycleBinActionsAuthorizer : IRecycleBinActionsAuthorizer
  {
    /// <summary>
    /// Ensures that there is a 'Delete' permission for the recyclable item when move data item to Recycle Bin.
    /// </summary>
    /// <remarks>If the specified <paramref name="item" /> does not implement <see cref="T:Telerik.Sitefinity.Security.Model.ISecuredObject" /> no authorization will be performed.</remarks>
    /// <param name="item">The recyclable item that is checked for permissions.</param>
    public virtual void EnsureMoveToRecycleBinPermissions(object item)
    {
      if (!(item is ISecuredObject secObj))
        return;
      string permissionSetName = secObj.SupportedPermissionSets.Length != 0 ? secObj.SupportedPermissionSets[0] : string.Empty;
      if (secObj.IsSecurityActionSupported(permissionSetName, SecurityActionTypes.Delete) && !secObj.IsSecurityActionTypeGranted(permissionSetName, SecurityActionTypes.Delete))
        throw new SecurityDemandFailException("You are not allowed to move this item into the Recycle Bin.");
    }

    /// <summary>
    /// Ensures that there is a 'Create' permission for the recyclable item when restore data item from Recycle Bin.
    /// </summary>
    /// <remarks>If the specified <paramref name="item" /> does not implement <see cref="T:Telerik.Sitefinity.Security.Model.ISecuredObject" /> no authorization will be performed.</remarks>
    /// <param name="item">The recyclable item that is checked for permissions.</param>
    public virtual void EnsureRestoreFromRecycleBinPermissions(object item)
    {
      if (!(item is ISecuredObject secObj))
        return;
      string permissionSetName = secObj.SupportedPermissionSets.Length != 0 ? secObj.SupportedPermissionSets[0] : string.Empty;
      if (!secObj.IsSecurityActionSupported(permissionSetName, SecurityActionTypes.Create))
        return;
      using (new SecuredObjectSettingsRegion(secObj).WithInheritsPermissions())
      {
        if (!secObj.IsSecurityActionTypeGranted(permissionSetName, SecurityActionTypes.Create))
          throw new SecurityDemandFailException("You are not allowed to restore this item from the Recycle Bin.");
      }
    }
  }
}
