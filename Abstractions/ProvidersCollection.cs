// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.ProvidersCollection`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Telerik.Sitefinity.Data;

namespace Telerik.Sitefinity.Abstractions
{
  /// <summary>Represents a collection of data providers.</summary>
  /// <typeparam name="TProvider">The type of the data provider.</typeparam>
  public class ProvidersCollection<TProvider> : KeyedCollection<string, TProvider> where TProvider : IDataProviderBase
  {
    private bool locked;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Abstractions.ProvidersCollection`1" /> class that uses case insensitive equality comparer.
    /// </summary>
    public ProvidersCollection()
      : base((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase)
    {
    }

    /// <summary>
    /// When implemented in a derived class, extracts the key from the specified element.
    /// </summary>
    /// <param name="item">The element from which to extract the key.</param>
    /// <returns>The key for the specified element.</returns>
    protected override string GetKeyForItem(TProvider item) => item.Name;

    /// <summary>Gets the value associated with the specified key.</summary>
    /// <returns>true if the collection contains an element with the specified key; otherwise, false.
    /// </returns>
    /// <param name="key">The key of the value to get.</param>
    /// <param name="value">
    /// When this method returns, contains the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value" /> parameter. This parameter is passed uninitialized.
    /// </param>
    public bool TryGetValue(string key, out TProvider value)
    {
      if (key == null)
        throw new ArgumentNullException(nameof (key));
      if (this.Dictionary != null)
        return this.Dictionary.TryGetValue(key, out value);
      foreach (TProvider provider in (IEnumerable<TProvider>) this.Items)
      {
        if (this.Comparer.Equals(this.GetKeyForItem(provider), key))
        {
          value = provider;
          return true;
        }
      }
      value = default (TProvider);
      return false;
    }

    /// <summary>
    /// Gets a Boolean value indicating if the collection is read only.
    /// </summary>
    public bool IsReadOnly => this.locked;

    internal void Lock() => this.locked = true;

    internal void Unlock() => this.locked = false;

    /// <summary>
    /// Removes all elements from the <see cref="T:Telerik.Sitefinity.Abstractions.ProvidersCollection`1" />.
    /// </summary>
    protected override void ClearItems()
    {
      if (this.locked)
        throw new InvalidOperationException("The collection is read only.");
      base.ClearItems();
    }

    /// <summary>
    /// Removes the element at the specified index of the <see cref="T:Telerik.Sitefinity.Abstractions.ProvidersCollection`1" />.
    /// </summary>
    /// <param name="index">The index of the element to remove.</param>
    protected override void RemoveItem(int index)
    {
      if (this.locked)
        throw new InvalidOperationException("The collection is read only.");
      base.RemoveItem(index);
    }

    /// <summary>
    /// Inserts an element into the <see cref="T:Telerik.Sitefinity.Abstractions.ProvidersCollection`1" /> at the specified index.
    /// </summary>
    /// <param name="index">
    /// The zero-based index at which <paramref name="item" /> should be inserted.
    /// </param>
    /// <param name="item">The object to insert.</param>
    protected override void InsertItem(int index, TProvider item)
    {
      if (this.locked)
        throw new InvalidOperationException("The collection is read only.");
      base.InsertItem(index, item);
    }

    /// <summary>
    /// Replaces the item at the specified index with the specified item.
    /// </summary>
    /// <param name="index">
    /// The zero-based index of the item to be replaced.
    /// </param>
    /// <param name="item">The new item.</param>
    protected override void SetItem(int index, TProvider item)
    {
      if (this.locked)
        throw new InvalidOperationException("The collection is read only.");
      base.SetItem(index, item);
    }
  }
}
