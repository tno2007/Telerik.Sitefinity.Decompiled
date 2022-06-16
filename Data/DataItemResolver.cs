// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.DataItemResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Data
{
  /// <summary>
  /// Helps to resolve IDataItem object from item type, provider and id
  /// </summary>
  public class DataItemResolver
  {
    /// <summary>
    /// Resolves the data item from item type name, provider and id.
    /// </summary>
    /// <param name="itemTypeName">Name of the item type.</param>
    /// <param name="itemProvider">The item provider.</param>
    /// <param name="itemId">The item id.</param>
    /// <returns>The data item</returns>
    public static IDataItem ResolveDataItem(
      string itemTypeName,
      string itemProvider,
      Guid itemId)
    {
      Type itemType = TypeResolutionService.ResolveType(itemTypeName, false);
      return itemType != (Type) null ? DataItemResolver.ResolveDataItem(itemType, itemProvider, itemId) : (IDataItem) null;
    }

    /// <summary>Resolves the data items.</summary>
    /// <param name="itemTypeName">Name of the item type.</param>
    /// <param name="itemProvider">The item provider.</param>
    /// <param name="itemIds">The item ids.</param>
    /// <returns>A collection of resolved data items.</returns>
    public static IEnumerable<IDataItem> ResolveDataItems(
      string itemTypeName,
      string itemProvider,
      IEnumerable<Guid> itemIds)
    {
      Type itemType = TypeResolutionService.ResolveType(itemTypeName, false);
      return itemType != (Type) null ? DataItemResolver.ResolveDataItems(itemType, itemProvider, itemIds) : (IEnumerable<IDataItem>) null;
    }

    /// <summary>
    /// Resolves the data item from item type, provider and id.
    /// </summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="itemProvider">The item provider.</param>
    /// <param name="itemId">The item id.</param>
    /// <returns>The data item</returns>
    public static IDataItem ResolveDataItem(
      Type itemType,
      string itemProvider,
      Guid itemId)
    {
      return ObjectFactory.Resolve<DataItemResolver>().ResolveItem(itemType, itemProvider, itemId);
    }

    /// <summary>Resolves the data items.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="itemProvider">The item provider.</param>
    /// <param name="itemIds">The item ids.</param>
    /// <returns>A collection of resolved data items.</returns>
    public static IEnumerable<IDataItem> ResolveDataItems(
      Type itemType,
      string itemProvider,
      IEnumerable<Guid> itemIds)
    {
      return ObjectFactory.Resolve<DataItemResolver>().ResolveItems(itemType, itemProvider, itemIds);
    }

    /// <summary>Resolves the item.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="itemProvider">The item provider.</param>
    /// <param name="itemId">The item id.</param>
    /// <returns>The data item</returns>
    public virtual IDataItem ResolveItem(Type itemType, string itemProvider, Guid itemId)
    {
      IManager manager;
      if (ManagerBase.TryGetMappedManager(itemType, itemProvider, out manager))
      {
        try
        {
          return manager.GetItem(itemType, itemId) as IDataItem;
        }
        catch (ItemNotFoundException ex)
        {
        }
      }
      return (IDataItem) null;
    }

    /// <summary>Resolves the items.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="itemProvider">The item provider.</param>
    /// <param name="itemIds">The item ids.</param>
    /// <returns>A collection of resolved data items.</returns>
    public virtual IEnumerable<IDataItem> ResolveItems(
      Type itemType,
      string itemProvider,
      IEnumerable<Guid> itemIds)
    {
      IManager manager;
      if (ManagerBase.TryGetMappedManager(itemType, itemProvider, out manager))
      {
        try
        {
          Guid[] ids = itemIds.ToArray<Guid>();
          return manager.GetItems(itemType, string.Empty, string.Empty, 0, 0).OfType<IDataItem>().Where<IDataItem>((Func<IDataItem, bool>) (i => ((IEnumerable<Guid>) ids).Contains<Guid>(i.Id)));
        }
        catch (ItemNotFoundException ex)
        {
        }
      }
      return (IEnumerable<IDataItem>) null;
    }
  }
}
