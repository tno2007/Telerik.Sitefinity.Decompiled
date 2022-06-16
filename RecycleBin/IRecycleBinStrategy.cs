// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.RecycleBin.IRecycleBinStrategy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Globalization;
using Telerik.Sitefinity.RecycleBin.Conflicts;

namespace Telerik.Sitefinity.RecycleBin
{
  /// <summary>Defines the common Recycle Bin operations.</summary>
  public interface IRecycleBinStrategy
  {
    /// <summary>
    /// Marks the specified <paramref name="dataItem" /> as deleted.
    /// The opposite of RestoreFromRecycleBin.
    /// </summary>
    /// <param name="dataItem">The data item.</param>
    /// <param name="language">The specific language translation that will be sent to the Recycle Bin.</param>
    void MoveToRecycleBin(IRecyclableDataItem dataItem, CultureInfo language = null);

    /// <summary>
    /// Restores the specified <see cref="T:Telerik.Sitefinity.RecycleBin.IRecyclableDataItem" />.
    /// The opposite of MoveToRecycleBin.
    /// </summary>
    /// <param name="dataItem">The data item to restore.</param>
    /// <param name="language">The specific language translation that will be restored from the Recycle Bin.</param>
    void RestoreFromRecycleBin(IRecyclableDataItem dataItem, CultureInfo language = null);

    /// <summary>
    /// Validates whether the specified <paramref name="dataItem" /> can be restored.
    /// </summary>
    /// <param name="dataItem">The data item which restoration will be validated.</param>
    /// <param name="language">The specific language translation that will be validated for restore from the Recycle Bin.</param>
    /// <returns>Return a list of <see cref="T:Telerik.Sitefinity.RecycleBin.Conflicts.IRestoreConflict" /> containing invalid restore reasons.</returns>
    IList<IRestoreConflict> ValidateRestore(
      IRecyclableDataItem dataItem,
      CultureInfo language = null);

    /// <summary>Permanently deletes item from recycle bin.</summary>
    /// <param name="dataItem">The data item.</param>
    /// <param name="language">The specific language translation that will be permanently deleted.</param>
    /// <remarks>Same as invoking IManager.DeleteItem method.</remarks>
    void PermanentlyDeleteFromRecycleBin(IRecyclableDataItem dataItem, CultureInfo language = null);
  }
}
