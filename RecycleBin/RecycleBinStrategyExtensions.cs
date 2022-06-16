// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.RecycleBin.RecycleBinStrategyExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Globalization;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.RecycleBin
{
  /// <summary>IRecycleBinStrategy extension methods</summary>
  public static class RecycleBinStrategyExtensions
  {
    /// <summary>
    /// Determines whether the specified <paramref name="item" /> can be sent to the Recycle Bin.
    /// </summary>
    /// <param name="strategy">The Recycle Bin strategy.</param>
    /// <param name="item">The item.</param>
    /// <param name="languageName">The language in which the item is marked as deleted. When <c>null</c> this means the item is deleted in all languages.</param>
    /// <returns>
    /// <c>false</c> if the specified item should be permanently deleted otherwise <c>true</c>.
    /// </returns>
    public static bool ShouldMoveToRecycleBin(
      this IRecycleBinStrategy strategy,
      object item,
      string languageName = null)
    {
      return ObjectFactory.Resolve<IRecycleBinStateResolver>().ShouldMoveToRecycleBin(item, languageName);
    }

    /// <summary>
    /// Determines whether the specified <paramref name="itemType" /> can be sent to the Recycle Bin.
    /// </summary>
    /// <param name="strategy">The Recycle Bin strategy.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <returns>
    /// <c>false</c> if the specified item should be permanently deleted otherwise <c>true</c>.
    /// </returns>
    public static bool ShouldMoveToRecycleBin(this IRecycleBinStrategy strategy, Type itemType) => ObjectFactory.Resolve<IRecycleBinStateResolver>().ShouldMoveToRecycleBin(itemType);

    /// <summary>
    /// Tries to move item to recycle bin. Returns true on success and false otherwise.
    /// </summary>
    /// <param name="strategy">The Recycle Bin strategy.</param>
    /// <param name="item">The item to move.</param>
    /// <param name="recyclingManager">The recycling manager</param>
    /// <param name="culture">The culture</param>
    /// <returns>True on successful move and false otherwise</returns>
    public static bool TryMoveToRecycleBin(
      this IRecycleBinStrategy strategy,
      IDataItem item,
      ISupportRecyclingManager recyclingManager,
      CultureInfo culture)
    {
      if (recyclingManager != null && recyclingManager.RecycleBin.ShouldMoveToRecycleBin(item.GetType()) && item is IRecyclableDataItem)
      {
        IRecyclableDataItem dataItem = (IRecyclableDataItem) item;
        string name = culture?.Name;
        if (recyclingManager.RecycleBin.ShouldMoveToRecycleBin((object) dataItem, name))
        {
          recyclingManager.RecycleBin.MoveToRecycleBin(dataItem);
          return true;
        }
      }
      return false;
    }

    /// <summary>
    /// Tries to move item to recycle bin. Returns true on success and false otherwise.
    /// </summary>
    /// <param name="strategy">The Recycle Bin strategy.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="itemId">Id of the item.</param>
    /// <param name="recyclingManager">The recycling manager.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>True on successful move and false otherwise</returns>
    public static bool TryMoveToRecycleBin(
      this IRecycleBinStrategy strategy,
      Type itemType,
      Guid itemId,
      ISupportRecyclingManager recyclingManager,
      CultureInfo culture)
    {
      if (recyclingManager != null && recyclingManager.RecycleBin.ShouldMoveToRecycleBin(itemType))
      {
        object obj = recyclingManager.GetItem(itemType, itemId);
        if (obj == null)
          throw new Exception(string.Format("Item with id:{0} and type:{1} was not found.", (object) itemId, (object) itemType));
        string name = culture?.Name;
        IRecyclableDataItem dataItem = obj as IRecyclableDataItem;
        if (recyclingManager.RecycleBin.ShouldMoveToRecycleBin((object) dataItem, name))
        {
          recyclingManager.RecycleBin.MoveToRecycleBin(dataItem);
          return true;
        }
      }
      return false;
    }
  }
}
