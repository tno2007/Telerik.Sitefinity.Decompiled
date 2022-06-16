// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.DefinitionDictionary`2
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections;
using System.Collections.Generic;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// This class represents a dictionary of definition objects (objects that implement <see cref="!:IDefiniton" /> interface).
  /// </summary>
  /// <typeparam name="TKey">Type of the key implemented by the definition dictionary.</typeparam>
  /// <typeparam name="TValue">Type of the value (definition) implemented by the definition dictionary.</typeparam>
  public class DefinitionDictionary<TKey, TValue> : 
    IDictionary<TKey, TValue>,
    ICollection<KeyValuePair<TKey, TValue>>,
    IEnumerable<KeyValuePair<TKey, TValue>>,
    IEnumerable
    where TValue : IDefinition
  {
    private IDictionary<TKey, TValue> internalDictionary = (IDictionary<TKey, TValue>) new Dictionary<TKey, TValue>();
    private ConfigElement configDictionary;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.DefinitionDictionary`2" /> class.
    /// </summary>
    /// <param name="configDictionary">The config dictionary.</param>
    public DefinitionDictionary(ConfigElement configDictionary) => this.configDictionary = configDictionary;

    /// <summary>
    /// Adds an element with the provided key and value to the <see cref="T:System.Collections.Generic.IDictionary`2" />.
    /// </summary>
    /// <param name="key">The object to use as the key of the element to add.</param>
    /// <param name="value">The object to use as the value of the element to add.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// 	<paramref name="key" /> is null.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    /// An element with the same key already exists in the <see cref="T:System.Collections.Generic.IDictionary`2" />.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    /// The <see cref="T:System.Collections.Generic.IDictionary`2" /> is read-only.
    /// </exception>
    public void Add(TKey key, TValue value) => this.internalDictionary.Add(key, value);

    /// <summary>
    /// Determines whether the <see cref="T:System.Collections.Generic.IDictionary`2" /> contains an element with the specified key.
    /// </summary>
    /// <param name="key">The key to locate in the <see cref="T:System.Collections.Generic.IDictionary`2" />.</param>
    /// <returns>
    /// true if the <see cref="T:System.Collections.Generic.IDictionary`2" /> contains an element with the key; otherwise, false.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// 	<paramref name="key" /> is null.
    /// </exception>
    public bool ContainsKey(TKey key) => this.internalDictionary.ContainsKey(key);

    /// <summary>
    /// Gets an <see cref="T:System.Collections.Generic.ICollection`1" /> containing the keys of the <see cref="T:System.Collections.Generic.IDictionary`2" />.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// An <see cref="T:System.Collections.Generic.ICollection`1" /> containing the keys of the object that implements <see cref="T:System.Collections.Generic.IDictionary`2" />.
    /// </returns>
    public ICollection<TKey> Keys => this.internalDictionary.Keys;

    /// <summary>
    /// Removes the element with the specified key from the <see cref="T:System.Collections.Generic.IDictionary`2" />.
    /// </summary>
    /// <param name="key">The key of the element to remove.</param>
    /// <returns>
    /// true if the element is successfully removed; otherwise, false.  This method also returns false if <paramref name="key" /> was not found in the original <see cref="T:System.Collections.Generic.IDictionary`2" />.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// 	<paramref name="key" /> is null.
    /// </exception>
    /// <exception cref="T:System.NotSupportedException">
    /// The <see cref="T:System.Collections.Generic.IDictionary`2" /> is read-only.
    /// </exception>
    public bool Remove(TKey key) => this.internalDictionary.Remove(key);

    /// <summary>Gets the value associated with the specified key.</summary>
    /// <param name="key">The key whose value to get.</param>
    /// <param name="value">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value" /> parameter. This parameter is passed uninitialized.</param>
    /// <returns>
    /// true if the object that implements <see cref="T:System.Collections.Generic.IDictionary`2" /> contains an element with the specified key; otherwise, false.
    /// </returns>
    /// <exception cref="T:System.ArgumentNullException">
    /// 	<paramref name="key" /> is null.
    /// </exception>
    public bool TryGetValue(TKey key, out TValue value) => this.internalDictionary.TryGetValue(key, out value);

    /// <summary>
    /// Gets an <see cref="T:System.Collections.Generic.ICollection`1" /> containing the values in the <see cref="T:System.Collections.Generic.IDictionary`2" />.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// An <see cref="T:System.Collections.Generic.ICollection`1" /> containing the values in the object that implements <see cref="T:System.Collections.Generic.IDictionary`2" />.
    /// </returns>
    public ICollection<TValue> Values => this.internalDictionary.Values;

    /// <summary>
    /// Gets or sets the <see cref="!:TValue" /> with the specified key.
    /// </summary>
    /// <value></value>
    public TValue this[TKey key]
    {
      get => this.internalDictionary[key];
      set => this.internalDictionary[key] = value;
    }

    /// <summary>
    /// Adds an item to the <see cref="T:System.Collections.Generic.ICollection`1" />.
    /// </summary>
    /// <param name="item">The object to add to the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
    /// <exception cref="T:System.NotSupportedException">
    /// The <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.
    /// </exception>
    public void Add(KeyValuePair<TKey, TValue> item) => this.internalDictionary.Add(item);

    /// <summary>
    /// Removes all items from the <see cref="T:System.Collections.Generic.ICollection`1" />.
    /// </summary>
    /// <exception cref="T:System.NotSupportedException">
    /// The <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.
    /// </exception>
    public void Clear() => this.internalDictionary.Clear();

    /// <summary>
    /// Determines whether the <see cref="T:System.Collections.Generic.ICollection`1" /> contains a specific value.
    /// </summary>
    /// <param name="item">The object to locate in the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
    /// <returns>
    /// true if <paramref name="item" /> is found in the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false.
    /// </returns>
    public bool Contains(KeyValuePair<TKey, TValue> item) => this.internalDictionary.Contains(item);

    /// <summary>
    /// Copies the elements of the <see cref="T:System.Collections.Generic.ICollection`1" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.
    /// </summary>
    /// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.Generic.ICollection`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
    /// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// 	<paramref name="array" /> is null.
    /// </exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">
    /// 	<paramref name="arrayIndex" /> is less than 0.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    /// 	<paramref name="array" /> is multidimensional.
    /// -or-
    /// <paramref name="arrayIndex" /> is equal to or greater than the length of <paramref name="array" />.
    /// -or-
    /// The number of elements in the source <see cref="T:System.Collections.Generic.ICollection`1" /> is greater than the available space from <paramref name="arrayIndex" /> to the end of the destination <paramref name="array" />.
    /// -or-
    /// Type <paramref name="T" /> cannot be cast automatically to the type of the destination <paramref name="array" />.
    /// </exception>
    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => this.internalDictionary.CopyTo(array, arrayIndex);

    /// <summary>
    /// Gets the number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// The number of elements contained in the <see cref="T:System.Collections.Generic.ICollection`1" />.
    /// </returns>
    public int Count => this.internalDictionary.Count;

    /// <summary>
    /// Gets a value indicating whether the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.
    /// </summary>
    /// <value></value>
    /// <returns>true if the <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only; otherwise, false.
    /// </returns>
    public bool IsReadOnly => false;

    /// <summary>
    /// Removes the first occurrence of a specific object from the <see cref="T:System.Collections.Generic.ICollection`1" />.
    /// </summary>
    /// <param name="item">The object to remove from the <see cref="T:System.Collections.Generic.ICollection`1" />.</param>
    /// <returns>
    /// true if <paramref name="item" /> was successfully removed from the <see cref="T:System.Collections.Generic.ICollection`1" />; otherwise, false. This method also returns false if <paramref name="item" /> is not found in the original <see cref="T:System.Collections.Generic.ICollection`1" />.
    /// </returns>
    /// <exception cref="T:System.NotSupportedException">
    /// The <see cref="T:System.Collections.Generic.ICollection`1" /> is read-only.
    /// </exception>
    public bool Remove(KeyValuePair<TKey, TValue> item) => ((ICollection<KeyValuePair<TKey, TValue>>) this.internalDictionary).Remove(item);

    /// <summary>
    /// Returns an enumerator that iterates through the collection.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through the collection.
    /// </returns>
    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => this.internalDictionary.GetEnumerator();

    /// <summary>
    /// Returns an enumerator that iterates through a collection.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerator" /> object that can be used to iterate through the collection.
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.internalDictionary.GetEnumerator();

    protected internal ConfigElement GetConfigDefinition(TKey key)
    {
      if (!(this.configDictionary is IDictionary configDictionary))
        return (ConfigElement) null;
      foreach (object key1 in (IEnumerable) configDictionary.Keys)
      {
        if (key1.Equals((object) key))
          return (ConfigElement) configDictionary[(object) key];
      }
      return (ConfigElement) null;
    }
  }
}
