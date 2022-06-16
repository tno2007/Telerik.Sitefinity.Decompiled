// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.RecycleBin.IRecycleBinItemAdapter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.RecycleBin
{
  /// <summary>
  /// Defines the common interface for <see cref="T:Telerik.Sitefinity.Model.IDataItem" /> to <see cref="T:Telerik.Sitefinity.RecycleBin.IRecycleBinDataItem" /> adapters.
  /// </summary>
  public interface IRecycleBinItemAdapter
  {
    /// <summary>
    /// Populates the properties of the specified <paramref name="recycleBinItem" /> from the specified <paramref name="dataItem" />.
    /// </summary>
    /// <param name="recycleBinItem">The recycle bin item which properties will be populated.</param>
    /// <param name="dataItem">The data item to get values from.</param>
    void FillProperties(IRecycleBinDataItem recycleBinItem, IDataItem dataItem);
  }
}
