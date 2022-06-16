// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.PermissionApplierEnumeratorBase`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Linq;

namespace Telerik.Sitefinity.Security
{
  /// <summary>Base class for demanding permissions on collections</summary>
  /// <typeparam name="TItem">The type of the enumerated item.</typeparam>
  public abstract class PermissionApplierEnumeratorBase<TItem> : 
    IEnumerator<TItem>,
    IDisposable,
    IEnumerator
  {
    private IEnumerator<TItem> enumerator;
    private DataProviderBase dataProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.PermissionApplierEnumeratorBase`1" /> class.
    /// </summary>
    /// <param name="enumerable">The enumerable.</param>
    public PermissionApplierEnumeratorBase(IEnumerable<TItem> enumerable) => this.enumerator = enumerable.GetEnumerator();

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.PermissionApplierEnumeratorBase`1" /> class.
    /// </summary>
    /// <param name="enumerable">The enumerator.</param>
    public PermissionApplierEnumeratorBase(IEnumerator<TItem> enumerator) => this.enumerator = enumerator;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.PermissionApplierEnumeratorBase`1" /> class.
    /// </summary>
    /// <param name="enumerator">The enumerator.</param>
    /// <param name="dataProvider">The data provider.</param>
    public PermissionApplierEnumeratorBase(
      IEnumerator<TItem> enumerator,
      DataProviderBase dataProvider)
    {
      if (enumerator == null)
        throw new ArgumentNullException(nameof (enumerator));
      if (dataProvider == null)
        throw new ArgumentNullException(nameof (dataProvider));
      this.enumerator = enumerator;
      this.dataProvider = dataProvider;
    }

    /// <summary>
    /// Gets the element in the collection at the current position of the enumerator.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// The element in the collection at the current position of the enumerator.
    /// </returns>
    public TItem Current => (TItem) DataItemEnumerator.EnsureProviderSet((object) this.enumerator.Current, (IDataProviderBase) this.dataProvider);

    /// <summary>Gets the prvider of the current enumerator</summary>
    internal DataProviderBase DataProvider => this.dataProvider;

    /// <summary>
    /// Gets the element in the collection at the current position of the enumerator.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// The element in the collection at the current position of the enumerator.
    /// </returns>
    object IEnumerator.Current => (object) this.Current;

    /// <summary>
    /// Advances the enumerator to the next element of the collection.
    /// </summary>
    /// <returns>
    /// true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">
    /// The collection was modified after the enumerator was created.
    /// </exception>
    public bool MoveNext()
    {
      int num = this.enumerator.MoveNext() ? 1 : 0;
      if (num == 0)
        return num != 0;
      this.Demand(this.enumerator.Current);
      return num != 0;
    }

    /// <summary>Demand permission for the current item</summary>
    /// <param name="forItem">Current item</param>
    protected abstract void Demand(TItem forItem);

    /// <summary>
    /// Sets the enumerator to its initial position, which is before the first element in the collection.
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">
    /// The collection was modified after the enumerator was created.
    /// </exception>
    public void Reset() => this.enumerator.Reset();

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose() => this.enumerator.Dispose();
  }
}
