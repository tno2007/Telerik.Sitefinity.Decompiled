// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.RecycleBin.IRecycleBinManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using Telerik.Sitefinity.Data;

namespace Telerik.Sitefinity.RecycleBin
{
  /// <summary>
  /// Defines the common interface for Recycle Bin managers.
  /// </summary>
  public interface IRecycleBinManager : IManager, IDisposable, IProviderResolver
  {
    /// <summary>
    /// Creates a persistent <see cref="T:Telerik.Sitefinity.RecycleBin.IRecycleBinDataItem" />.
    /// </summary>
    /// <returns>Instance of <see cref="T:Telerik.Sitefinity.RecycleBin.IRecycleBinDataItem" /></returns>
    IRecycleBinDataItem CreateRecycleBinItem();

    /// <summary>
    /// Creates a persistent <see cref="T:Telerik.Sitefinity.RecycleBin.IRecycleBinDataItem" />.
    /// </summary>
    /// <param name="id">The id of the item to create.</param>
    /// <returns>New instance of <see cref="T:Telerik.Sitefinity.RecycleBin.IRecycleBinDataItem" /></returns>
    IRecycleBinDataItem CreateRecycleBinItem(Guid id);

    /// <summary>
    /// Get a query for all <see cref="T:Telerik.Sitefinity.RecycleBin.IRecycleBinDataItem" /> objects.
    /// </summary>
    /// <returns><see cref="T:System.Linq.IQueryable" /> object for all <see cref="T:Telerik.Sitefinity.RecycleBin.IRecycleBinDataItem" /> objects.</returns>
    IQueryable<IRecycleBinDataItem> GetRecycleBinItems();

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.RecycleBin.IRecycleBinDataItem" /> with the specified <paramref name="id" />.
    /// </summary>
    /// <param name="id">The id of the requested <see cref="T:Telerik.Sitefinity.RecycleBin.IRecycleBinDataItem" /></param>
    /// <returns>
    /// The requested <see cref="T:Telerik.Sitefinity.RecycleBin.IRecycleBinDataItem" />
    /// </returns>
    /// <exception cref="T:Telerik.Sitefinity.SitefinityExceptions.ItemNotFoundException">
    /// If the item with the specified <paramref name="id" /> was not found.</exception>
    IRecycleBinDataItem GetRecycleBinItem(Guid id);

    /// <summary>
    /// Gets the <see cref="!:RecycleBinDataItem" /> for a <see cref="T:Telerik.Sitefinity.RecycleBin.IRecyclableDataItem" /> with <paramref name="dataItemId" />.
    /// </summary>
    /// <param name="dataItemId">The identifier of the <see cref="T:Telerik.Sitefinity.RecycleBin.IRecyclableDataItem" /></param>
    /// <returns>
    /// The requested <see cref="!:RecycleBinDataItem" />
    /// </returns>
    IRecycleBinDataItem GetRecycleBinItemForDataItem(Guid dataItemId);

    /// <summary>
    /// Deletes the specified <paramref name="item" /> from the Recycle Bin.
    /// </summary>
    /// <param name="item">The item to delete.</param>
    void Delete(IRecycleBinDataItem item);
  }
}
