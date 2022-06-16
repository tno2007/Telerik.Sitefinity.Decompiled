// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.RecycleBin.IRecycleBinItemFactory
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.RecycleBin
{
  /// <summary>
  /// Defines the common operations for <see cref="T:Telerik.Sitefinity.RecycleBin.IRecycleBinItemAdapter" /> factories.
  /// </summary>
  public interface IRecycleBinItemFactory
  {
    /// <summary>
    /// Fills in the properties of the specified <paramref name="recycleBinItem" /> from the specified <paramref name="item" />.
    /// </summary>
    /// <param name="recycleBinItem">The recycle bin item which properties will be filled.</param>
    /// <param name="item">The data item which values will be used to compose the recycle bin item.</param>
    /// <returns>The composed <paramref name="recycleBinItem" />.</returns>
    IRecycleBinDataItem FillRecycleBinItem(
      IRecycleBinDataItem recycleBinItem,
      IDataItem item);
  }
}
