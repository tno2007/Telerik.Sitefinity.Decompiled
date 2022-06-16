// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.SimpleTransactionList`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Data
{
  /// <summary>
  /// Represent simple transaction list.
  /// Transaction is used to store temporary references to data items.
  /// </summary>
  /// <typeparam name="TDataItem"></typeparam>
  public class SimpleTransactionList<TDataItem> : KeyedCollection<object, TDataItem>
  {
    internal void AddItem(TDataItem dataItem)
    {
      if (this.Contains(this.GetKeyForItem(dataItem)))
        return;
      base.InsertItem(this.Count, dataItem);
    }

    /// <summary>
    /// Inserts an element into the collection at the specified index.
    /// </summary>
    /// <param name="index">
    /// The zero-based index at which <paramref name="item" /> should be inserted.
    /// </param>
    /// <param name="item">The object to insert.</param>
    protected override void InsertItem(int index, TDataItem item) => throw new InvalidOperationException(Res.Get<ErrorMessages>().AddingDirectlyNotSupported);

    /// <summary>
    /// When implemented in a derived class, extracts the key from the specified element.
    /// </summary>
    /// <param name="item">The element from which to extract the key.</param>
    /// <returns>The key for the specified element.</returns>
    protected override object GetKeyForItem(TDataItem item) => (object) item;

    /// <summary>Gets the value associated with the specified key.</summary>
    /// <returns>true if the collection contains an element with the specified key; otherwise, false.
    /// </returns>
    /// <param name="key">The key of the value to get.</param>
    /// <param name="value">
    /// When this method returns, contains the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value" /> parameter. This parameter is passed uninitialized.
    /// </param>
    public bool TryGetValue(object key, out TDataItem value)
    {
      if (key == null)
        throw new ArgumentNullException(nameof (key));
      if (this.Dictionary != null)
        return this.Dictionary.TryGetValue(key, out value);
      foreach (TDataItem dataItem in (IEnumerable<TDataItem>) this.Items)
      {
        if (this.Comparer.Equals(this.GetKeyForItem(dataItem), key))
        {
          value = dataItem;
          return true;
        }
      }
      value = default (TDataItem);
      return false;
    }
  }
}
