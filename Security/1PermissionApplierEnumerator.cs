// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.PermissionApplierEnumerator`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Security
{
  /// <summary>
  /// Represents enumerator object that can apply permissions on the enumerated items.
  /// </summary>
  /// <typeparam name="TItem">The type of the enumerated item.</typeparam>
  public sealed class PermissionApplierEnumerator<T> : PermissionApplierEnumeratorBase<T>
  {
    private PermissionAttribute[] permissionsInfo;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.PermissionApplierEnumerator`1" /> class.
    /// </summary>
    /// <param name="enumerable">The enumerable.</param>
    /// <param name="pemissionsInfo">
    /// The information about the permissions that will be applied.
    /// This parameter can be null, in which case no permissions will be applied and the enumerator will work as simple enumerator.
    /// </param>
    public PermissionApplierEnumerator(
      IEnumerator<T> enumerable,
      DataProviderBase dataProvider,
      params PermissionAttribute[] permissionsInfo)
      : base(enumerable, dataProvider)
    {
      this.permissionsInfo = permissionsInfo;
    }

    public PermissionApplierEnumerator(
      IEnumerator<T> enumerable,
      params PermissionAttribute[] permissionsInfo)
      : this(enumerable, (DataProviderBase) null, permissionsInfo)
    {
    }

    public PermissionApplierEnumerator(IEnumerator<T> enumerator, DataProviderBase dataProvider)
      : base(enumerator, dataProvider)
    {
    }

    /// <summary>Demand permission for the current item</summary>
    /// <param name="forItem">Current item</param>
    protected override void Demand(T forItem)
    {
      if (this.DataProvider != null && this.DataProvider.SuppressSecurityChecks || this.permissionsInfo == null || this.permissionsInfo.Length == 0)
        return;
      ISecuredObject securedObject = (ISecuredObject) (object) forItem;
      foreach (PermissionAttribute permissionAttribute in this.permissionsInfo)
      {
        if (securedObject.IsPermissionSetSupported(permissionAttribute.PermissionSetName) && (!(permissionAttribute is TypedEnumeratorPermissionAttribute) || ((TypedPermissionAttribute) permissionAttribute).ItemType == securedObject.GetType()))
          securedObject.Demand(permissionAttribute.PermissionSetName, permissionAttribute.Value);
      }
    }
  }
}
