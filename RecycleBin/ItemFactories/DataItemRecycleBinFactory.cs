// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.RecycleBin.ItemFactories.DataItemRecycleBinFactory
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.RecycleBin.Adapters;

namespace Telerik.Sitefinity.RecycleBin.ItemFactories
{
  /// <summary>
  /// Recycle Bin item factory that can fill the properties of <see cref="T:Telerik.Sitefinity.RecycleBin.IRecycleBinDataItem" /> from a <see cref="T:Telerik.Sitefinity.Model.IDataItem" /> object.
  /// </summary>
  public class DataItemRecycleBinFactory : IRecycleBinItemFactory
  {
    /// <summary>
    /// Fills in the properties of the specified <paramref name="recycleBinItem" /> from the specified <paramref name="item" />.
    /// </summary>
    /// <param name="recycleBinItem">The recycle bin item which properties will be filled.</param>
    /// <param name="item">The data item which values will be used to compose the recycle bin item.</param>
    /// <returns>The populated <see cref="T:Telerik.Sitefinity.RecycleBin.IRecycleBinDataItem" /> object.</returns>
    public virtual IRecycleBinDataItem FillRecycleBinItem(
      IRecycleBinDataItem recycleBinItem,
      IDataItem item)
    {
      foreach (IRecycleBinItemAdapter recycleBinItemAdapter in (IEnumerable<IRecycleBinItemAdapter>) this.GetRecycleBinItemAdapters(item))
        recycleBinItemAdapter.FillProperties(recycleBinItem, item);
      return recycleBinItem;
    }

    /// <summary>
    /// Gets a list of <see cref="T:Telerik.Sitefinity.RecycleBin.IRecycleBinItemAdapter" /> for the specified <paramref name="item" />.
    /// </summary>
    /// <param name="item">The item that will be inspected to configure what adapters will be used.</param>
    /// <returns>List of <see cref="T:Telerik.Sitefinity.RecycleBin.IRecycleBinItemAdapter" /> for the specified <paramref name="item" />.</returns>
    protected virtual IList<IRecycleBinItemAdapter> GetRecycleBinItemAdapters(
      IDataItem item)
    {
      return (IList<IRecycleBinItemAdapter>) new List<IRecycleBinItemAdapter>()
      {
        (IRecycleBinItemAdapter) ObjectFactory.Container.Resolve<DataItemAdapter>()
      };
    }
  }
}
