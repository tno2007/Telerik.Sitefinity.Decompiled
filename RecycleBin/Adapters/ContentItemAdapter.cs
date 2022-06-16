// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.RecycleBin.Adapters.ContentItemAdapter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.RecycleBin.Adapters
{
  /// <summary>
  /// Populates the properties of a specified <see cref="T:Telerik.Sitefinity.RecycleBin.IRecycleBinDataItem" /> from the properties of a <see cref="T:Telerik.Sitefinity.Model.IContent" /> item.
  /// </summary>
  public class ContentItemAdapter : IRecycleBinItemAdapter
  {
    /// <summary>
    /// Populates the properties of the specified <paramref name="recycleBinItem" /> from the specified <paramref name="dataItem" /> casting it to <see cref="T:Telerik.Sitefinity.Model.IContent" />.
    /// </summary>
    /// <param name="recycleBinItem">The recycle bin item which properties will be populated.</param>
    /// <param name="dataItem">The data item to get values from.</param>
    public virtual void FillProperties(IRecycleBinDataItem recycleBinItem, IDataItem dataItem)
    {
      if (dataItem == null)
        throw new ArgumentNullException(nameof (dataItem));
      if (recycleBinItem == null)
        throw new ArgumentNullException(nameof (recycleBinItem));
      recycleBinItem.DeletedItemTitle = dataItem is IContent content ? (string) content.Title : throw new Exception(string.Format("ContentItemAdapter received data item with id: {0} and type:{1} that is not IContent.", (object) dataItem.Id, (object) dataItem.GetType().FullName));
    }
  }
}
