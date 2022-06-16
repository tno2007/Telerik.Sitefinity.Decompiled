// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.GenericContent.AddOnlyDictionary`2
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Telerik.Sitefinity.Data.GenericContent
{
  /// <summary>
  /// Provides wrap around an IDictionary of <typeparamref name="TKey" /> and <typeparamref name="TValue" />.
  /// The hash table will support only adding items to the collection.
  /// </summary>
  /// <typeparam name="TKey">Type that should be used for key in the hash table.</typeparam>
  /// <typeparam name="TValue">Type of the values that will be held in the hash table.</typeparam>
  public class AddOnlyDictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>, IEnumerable
  {
    private IDictionary<TKey, TValue> hash;

    /// <summary>
    /// Used to set the internal hash table that is used by this add-only dictionary
    /// </summary>
    internal IDictionary<TKey, TValue> Hash
    {
      get => this.hash;
      set => this.hash = value;
    }

    /// <summary>
    /// Create a new read-only hash-table with default settings
    /// </summary>
    public AddOnlyDictionary() => this.hash = (IDictionary<TKey, TValue>) new Dictionary<TKey, TValue>();

    /// <summary>
    /// Creates a new read-only hash table with a number indicatiing the initial capacity
    /// </summary>
    /// <param name="capacity">Initial capacity</param>
    public AddOnlyDictionary(int capacity) => this.hash = (IDictionary<TKey, TValue>) new Dictionary<TKey, TValue>(capacity);

    /// <summary>
    /// Creates a new read-only hash table from another hash-table
    /// </summary>
    /// <param name="initialHash">Hash-table to copu from</param>
    public AddOnlyDictionary(IDictionary<TKey, TValue> initialHash) => this.hash = initialHash != null ? (IDictionary<TKey, TValue>) new Dictionary<TKey, TValue>(initialHash) : throw new ArgumentNullException(nameof (initialHash));

    /// <summary>
    /// Add <paramref name="value" />, which is to be looked up by <paramref name="key" />
    /// </summary>
    /// <param name="key">Key to use for <paramref name="value" /> look-up</param>
    /// <param name="value">Value initially assocaited with <paramref name="key" /></param>
    [MethodImpl(MethodImplOptions.Synchronized)]
    public void Add(TKey key, TValue value) => this.hash.Add(key, value);

    /// <summary>Gets a value by key</summary>
    /// <param name="key">Key to use for the look up</param>
    /// <returns>Value</returns>
    public TValue this[TKey key] => this.hash[key];

    /// <summary>
    /// Gets the value associated with the given <paramref name="key" />
    /// </summary>
    /// <param name="key">Key to use for the look-up</param>
    /// <param name="value">Value to accept the result of the look-up</param>
    /// <returns>Indicates the success of the operation</returns>
    public bool TryGetValue(TKey key, out TValue value) => this.hash.TryGetValue(key, out value);

    /// <summary>
    /// Determines whether the hash table contains an item with this <paramref name="key" />
    /// </summary>
    /// <param name="key">Key to check for</param>
    /// <returns>True if there is an item with key equal to <paramref name="key" /></returns>
    public bool ContainsKey(TKey key) => this.hash.ContainsKey(key);

    /// <summary>Number of elements in the collection</summary>
    public int Count => this.hash.Count;

    /// <summary>
    /// Returns a generic enumerator that iterates through the collection
    /// </summary>
    /// <returns>Generic enumerator</returns>
    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => this.hash.GetEnumerator();

    /// <summary>
    /// Returns a non-generic enumerator that iterates through the collection
    /// </summary>
    /// <returns>Non-generic enumerator</returns>
    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

    /// <summary>
    /// Determines whether two add-only dictionaries are equal
    /// </summary>
    /// <param name="obj">Other object to compare with</param>
    /// <returns>Result of the comparision</returns>
    public override bool Equals(object obj) => obj is AddOnlyDictionary<TKey, TValue> ? this.hash.Equals((object) ((AddOnlyDictionary<TKey, TValue>) obj).hash) : base.Equals(obj);

    /// <summary>Serves as a hash function for the add-only collection</summary>
    /// <returns>A hash code for the current add-only collection</returns>
    public override int GetHashCode() => this.hash.GetHashCode();
  }
}
