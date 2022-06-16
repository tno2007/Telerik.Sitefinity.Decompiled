// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.RecycleBin.IRecycleBinStateResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.RecycleBin
{
  /// <summary>Resolves Recycle Bin related states</summary>
  public interface IRecycleBinStateResolver
  {
    /// <summary>
    /// Determines whether the specified <paramref name="item" /> can be sent to the Recycle Bin.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="languageName">The language in which the item is marked as deleted. When <c>null</c> this means the item is deleted in all languages.</param>
    /// <returns>
    /// <c>false</c> if the specified item should be permanently deleted otherwise <c>true</c>.
    /// </returns>
    bool ShouldMoveToRecycleBin(object item, string languageName = null);

    /// <summary>
    /// Determines whether the specified <paramref name="dataItem" /> can be sent to the Recycle Bin.
    /// </summary>
    /// <param name="itemType">Type of the item.</param>
    /// <returns>
    /// <c>false</c> if the specified item should be permanently deleted otherwise <c>true</c>.
    /// </returns>
    bool ShouldMoveToRecycleBin(Type itemType);

    /// <summary>
    /// Determines whether items should be sent to the Recycle Bin.
    /// </summary>
    /// <returns>
    /// <c>false</c> if items should be permanently deleted otherwise <c>true</c>.
    /// </returns>
    bool ShouldMoveToRecycleBin();
  }
}
