// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Linq.DataItemEnumerator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Data.Linq
{
  /// <summary>
  /// This class is a specific enumerator that is to be applied to types that implement
  /// <see cref="T:Telerik.Sitefinity.Model.IDataItem" /> interface.
  /// </summary>
  public class DataItemEnumerator : IEnumerator
  {
    private IEnumerator parentEnumerator;
    private DataProviderBase dataProvider;

    /// <summary>
    /// Creates a new instance of <see cref="T:Telerik.Sitefinity.Data.Linq.DataItemEnumerator" />.
    /// </summary>
    /// <param name="parentEnumerator">
    /// The instance of the parent enumerator that this enumerator wraps.
    /// </param>
    /// <param name="dataProvider">
    /// The instance of data provider that returns this enumerator.
    /// </param>
    public DataItemEnumerator(IEnumerator parentEnumerator, DataProviderBase dataProvider)
    {
      if (parentEnumerator == null)
        throw new ArgumentNullException(nameof (parentEnumerator));
      if (dataProvider == null)
        throw new ArgumentNullException(nameof (dataProvider));
      this.parentEnumerator = parentEnumerator;
      this.dataProvider = dataProvider;
    }

    /// <summary>Gets the element in the collection at the current position of the enumerator.
    /// </summary>
    /// <returns>The element in the collection at the current position of the enumerator.
    /// </returns>
    public object Current => DataItemEnumerator.EnsureProviderSet(this.parentEnumerator.Current, (IDataProviderBase) this.dataProvider);

    /// <summary>Gets the current element in the collection.</summary>
    /// <returns>The current element in the collection.</returns>
    /// <exception cref="T:System.InvalidOperationException">The enumerator is positioned
    /// before the first element of the collection or after the last element.</exception>
    /// <filterpriority>2</filterpriority>
    object IEnumerator.Current => this.Current;

    /// <summary>Advances the enumerator to the next element of the collection.</summary>
    /// <returns>true if the enumerator was successfully advanced to the next element;
    /// false if the enumerator has passed the end of the collection.</returns>
    /// <exception cref="T:System.InvalidOperationException">The collection was modified
    /// after the enumerator was created. </exception>
    /// <filterpriority>2</filterpriority>
    public bool MoveNext() => this.parentEnumerator.MoveNext();

    /// <summary>Sets the enumerator to its initial position, which is before the first
    /// element in the collection.</summary>
    /// <exception cref="T:System.InvalidOperationException">The collection was modified
    /// after the enumerator was created. </exception>
    /// <filterpriority>2</filterpriority>
    public void Reset() => this.parentEnumerator.Reset();

    internal static object EnsureProviderSet(object item, IDataProviderBase dataProvider)
    {
      if (item is IDataItem dataItem)
        dataItem.Provider = (object) dataProvider;
      return item;
    }
  }
}
