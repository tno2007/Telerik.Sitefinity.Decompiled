// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.RecycleBin.RecycleBinEventRegistry
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.RecycleBin
{
  /// <summary>
  /// Defines common operations for registering Recycle Bin related events
  /// for the specified data items.
  /// </summary>
  internal class RecycleBinEventRegistry : IRecycleBinEventRegistry
  {
    /// <summary>
    /// Registers move to Recycle Bin operation for the specified <paramref name="dataItem" />.
    /// </summary>
    /// <param name="dataItem">The data item for which to register the operation.</param>
    /// <param name="languages">The affected languages.</param>
    public void RegisterMoveToRecycleBinOperation(IRecyclableDataItem dataItem, string[] languages)
    {
      if (!(dataItem is IHasTrackingContext))
        return;
      (dataItem as IHasTrackingContext).TrackingContext.RegisterOperation(OperationStatus.MovedToRecycleBin, languages);
    }

    /// <summary>
    /// Registers a move to Recycle Bin with operation for the specified <paramref name="dataItem" />,
    /// specifying that the parent item was also moved to the Recycle Bin.
    /// This means that the <paramref name="dataItem" /> is
    /// moved to the Recycle Bin because its parent was moved to the Recycle bin.
    /// </summary>
    /// <param name="dataItem">The data item for which to register the operation.</param>
    /// <param name="languages">The affected languages.</param>
    public void RegisterMoveToRecycleBinWithParentOperation(
      IRecyclableDataItem dataItem,
      string[] languages)
    {
      if (!(dataItem is IHasTrackingContext))
        return;
      (dataItem as IHasTrackingContext).TrackingContext.RegisterOperation(OperationStatus.MovedToRecycleBinWithParent, languages);
    }

    /// <summary>
    /// Registers restore from Recycle Bin operation for the specified <paramref name="dataItem" />.
    /// </summary>
    /// <param name="dataItem">The data item.</param>
    /// <param name="languages">The languages.</param>
    public void RegisterRestoreFromRecycleBinOperation(
      IRecyclableDataItem dataItem,
      string[] languages)
    {
      if (!(dataItem is IHasTrackingContext))
        return;
      (dataItem as IHasTrackingContext).TrackingContext.RegisterOperation(OperationStatus.RestoreFromRecycleBin, languages);
    }
  }
}
