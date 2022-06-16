// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.PermissionApplierEnumerable`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections;
using System.Collections.Generic;

namespace Telerik.Sitefinity.Security
{
  /// <summary>
  /// Represents enumerable object that can apply permissions on the enumerated items.
  /// </summary>
  /// <typeparam name="TItem">The type of the item.</typeparam>
  public class PermissionApplierEnumerable<TItem> : IEnumerable<TItem>, IEnumerable
  {
    private PermissionAttribute[] permissionsInfo;
    private IEnumerable<TItem> enumerable;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.PermissionApplierEnumerable`1" /> class.
    /// </summary>
    /// <param name="enumerable">The enumerable.</param>
    public PermissionApplierEnumerable(
      IEnumerable<TItem> enumerable,
      params PermissionAttribute[] permissionsInfo)
    {
      this.enumerable = enumerable;
      this.permissionsInfo = permissionsInfo;
    }

    /// <summary>
    /// Returns an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
    /// </returns>
    public IEnumerator<TItem> GetEnumerator() => (IEnumerator<TItem>) new PermissionApplierEnumerator<TItem>(this.enumerable.GetEnumerator(), this.permissionsInfo);

    /// <summary>
    /// Returns an enumerator that iterates through a collection.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();
  }
}
