// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.RecycleBin.Conflicts.IRecycleBinUrlValidator`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Data;

namespace Telerik.Sitefinity.RecycleBin.Conflicts
{
  /// <summary>
  /// Represents a common interface for Recycle Bin URL validations.
  /// </summary>
  /// <typeparam name="T">Data item type.</typeparam>
  public interface IRecycleBinUrlValidator<T>
  {
    /// <summary>
    /// Determines whether the specified <paramref name="dataItem" />'s URL is unique.
    /// </summary>
    /// <param name="manager">The manager for specified dataItem.</param>
    /// <param name="dataItem">The data item which URL will be checked.</param>
    /// <returns>Returns a URL conflict if any.</returns>
    IRestoreConflict GetUrlConflict(IManager manager, T dataItem);

    /// <summary>
    /// Asserts that the specified <paramref name="dataItem" />'s URL is unique.
    /// If the a URL conflict is encountered an Exception containing the
    /// conflict information will be thrown.
    /// </summary>
    /// <param name="manager">The manager for current <see cref="!:dataItem" />.</param>
    /// <param name="dataItem">The data item which URL will be checked.</param>
    void AssertNoUrlConflicts(IManager manager, T dataItem);
  }
}
