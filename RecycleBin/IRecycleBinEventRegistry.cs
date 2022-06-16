// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.RecycleBin.IRecycleBinEventRegistry
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.RecycleBin
{
  /// <summary>
  /// Defines the interface for registering Recycle Bin related events
  /// for the specified data items.
  /// </summary>
  public interface IRecycleBinEventRegistry
  {
    /// <summary>
    /// Registers move to Recycle Bin operation for the specified <paramref name="dataItem" />.
    /// </summary>
    /// <param name="dataItem">The data item for which to register the operation.</param>
    /// <param name="languages">The affected languages.</param>
    void RegisterMoveToRecycleBinOperation(IRecyclableDataItem dataItem, string[] languages);

    /// <summary>
    /// Registers a move to Recycle Bin operation for the specified <paramref name="dataItem" />,
    /// specifying that the parent item was also moved to the Recycle Bin.
    /// This means that the <paramref name="dataItem" /> is
    /// moved to the Recycle Bin because its parent was moved to the Recycle bin.
    /// </summary>
    /// <param name="dataItem">The data item for which to register the operation.</param>
    /// <param name="languages">The affected languages.</param>
    void RegisterMoveToRecycleBinWithParentOperation(
      IRecyclableDataItem dataItem,
      string[] languages);

    /// <summary>
    /// Registers restore from Recycle Bin operation for the specified <paramref name="dataItem" />.
    /// </summary>
    /// <param name="dataItem">The data item.</param>
    /// <param name="languages">The languages.</param>
    void RegisterRestoreFromRecycleBinOperation(IRecyclableDataItem dataItem, string[] languages);
  }
}
