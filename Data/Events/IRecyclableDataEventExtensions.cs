// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Events.IRecyclableDataEventExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Data.Events
{
  /// <summary>
  /// Contains extension methods on <see cref="T:Telerik.Sitefinity.Data.Events.IRecyclableDataEvent" /> objects.
  /// </summary>
  internal static class IRecyclableDataEventExtensions
  {
    /// <summary>
    /// Sets the <see cref="T:Telerik.Sitefinity.Data.Events.IRecyclableDataEvent" /> properties from the tracking context
    /// of the specified <paramref name="dataItem" />.
    /// </summary>
    /// <param name="evt">The <see cref="T:Telerik.Sitefinity.Data.Events.IRecyclableDataEvent" /> which properties will be set.</param>
    /// <param name="dataItem">
    /// The data item which context will be used
    /// to fill in the <see cref="T:Telerik.Sitefinity.Data.Events.IRecyclableDataEvent" /> properties.
    /// </param>
    public static void SetIRecyclableEventPropertiesFromTrackingContext(
      this IRecyclableDataEvent evt,
      IDataItem dataItem)
    {
      if (!(dataItem is IHasTrackingContext context))
        return;
      if (context.HasOperation(OperationStatus.MovedToRecycleBin))
      {
        evt.RecycleBinAction = RecycleBinAction.MoveToRecycleBin;
        evt.WithParent = false;
      }
      else if (context.HasOperation(OperationStatus.MovedToRecycleBinWithParent))
      {
        evt.RecycleBinAction = RecycleBinAction.MoveToRecycleBin;
        evt.WithParent = true;
      }
      else if (context.HasOperation(OperationStatus.Deleted))
      {
        evt.RecycleBinAction = RecycleBinAction.PermanentDelete;
        evt.WithParent = false;
      }
      else if (context.HasOperation(OperationStatus.RestoreFromRecycleBin))
      {
        evt.RecycleBinAction = RecycleBinAction.RestoreFromRecycleBin;
        evt.WithParent = false;
      }
      evt.AffectedLanguages = context.TrackingContext.Languages.ToArray();
    }
  }
}
