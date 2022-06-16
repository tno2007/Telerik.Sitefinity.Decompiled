// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.RecycleBin.Adapters.DataItemAdapter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Utilities;

namespace Telerik.Sitefinity.RecycleBin.Adapters
{
  /// <summary>
  /// Generic <see cref="T:Telerik.Sitefinity.Model.IDataItem" /> to <see cref="T:Telerik.Sitefinity.RecycleBin.IRecycleBinDataItem" /> adapter.
  /// </summary>
  public class DataItemAdapter : IRecycleBinItemAdapter
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.RecycleBin.Adapters.DataItemAdapter" /> class.
    /// </summary>
    public DataItemAdapter()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.RecycleBin.Adapters.DataItemAdapter" /> class.
    /// </summary>
    /// <param name="providerNameResolver">The provider name resolver to use.</param>
    public DataItemAdapter(IProviderNameResolver providerNameResolver) => this.ProviderNameResolver = providerNameResolver;

    /// <summary>
    /// Gets or sets the provider name resolver that will be used when acquiring the provider name of a <see cref="T:Telerik.Sitefinity.Model.IDataItem" />.
    /// </summary>
    /// <value>The provider name resolver.</value>
    public IProviderNameResolver ProviderNameResolver { get; set; }

    /// <summary>
    /// Populating the properties of the specified <paramref name="recycleBinItem" /> from the specified <paramref name="dataItem" />.
    /// </summary>
    /// <param name="recycleBinItem">The recycle bin item which properties will be populated.</param>
    /// <param name="dataItem">The data item to get values from.</param>
    public virtual void FillProperties(IRecycleBinDataItem recycleBinItem, IDataItem dataItem)
    {
      if (this.ProviderNameResolver == null)
        throw new ArgumentNullException("this.ProviderNameResolver");
      if (dataItem == null)
        throw new ArgumentNullException(nameof (dataItem));
      if (recycleBinItem == null)
        throw new ArgumentNullException(nameof (recycleBinItem));
      recycleBinItem.DeletedItemId = dataItem.Id;
      recycleBinItem.Owner = SecurityManager.CurrentUserId;
      recycleBinItem.DeletedItemTypeName = dataItem.GetType().FullName;
      recycleBinItem.DeletedItemProviderName = this.ProviderNameResolver.GetProviderName(dataItem.Provider);
    }
  }
}
