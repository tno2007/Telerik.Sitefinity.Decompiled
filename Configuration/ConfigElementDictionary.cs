// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.ConfigElementDictionary`2
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Telerik.Sitefinity.Utilities;

namespace Telerik.Sitefinity.Configuration
{
  /// <summary>Represents dictionary of configuration elements.</summary>
  /// <typeparam name="TKey">The type of the key.</typeparam>
  /// <typeparam name="TElement">The type of the element.</typeparam>
  public class ConfigElementDictionary<TKey, TElement> : 
    ConfigElementCollection,
    IDictionary<TKey, TElement>,
    ICollection<KeyValuePair<TKey, TElement>>,
    IEnumerable<KeyValuePair<TKey, TElement>>,
    IEnumerable,
    IConfigElementCollection<TElement>
    where TElement : ConfigElement
  {
    private Dictionary<TKey, ConfigElementItem<TElement>> dictionary;

    /// <summary>Initializes new instance of ConfigElementDictionary.</summary>
    internal ConfigElementDictionary()
      : this((ConfigElement) null)
    {
    }

    /// <summary>Initializes new instance of ConfigElementDictionary.</summary>
    public ConfigElementDictionary(ConfigElement parent)
      : this(parent, (IEqualityComparer<TKey>) null)
    {
    }

    /// <summary>
    /// Creates a new configuration dictionary by explicitly specifying equality
    /// comparer to use for key lookup
    /// </summary>
    /// <param name="parent">Parent configuration element</param>
    /// <param name="keyComparer">Equality comparer to used when looking up by key name</param>
    public ConfigElementDictionary(ConfigElement parent, IEqualityComparer<TKey> keyComparer)
      : base(parent)
    {
      if (this.keyProperties == null || this.keyProperties.Length == 0)
        throw new ConfigurationErrorsException("No key specified.");
      if (this.keyProperties.Length > 1)
        throw new ConfigurationErrorsException("Multiple keys specified.");
      if (keyComparer == null)
      {
        if (typeof (TKey) == typeof (string))
          this.dictionary = new Dictionary<TKey, ConfigElementItem<TElement>>((IEqualityComparer<TKey>) StringComparer.OrdinalIgnoreCase);
        else
          this.dictionary = new Dictionary<TKey, ConfigElementItem<TElement>>();
      }
      else
        this.dictionary = new Dictionary<TKey, ConfigElementItem<TElement>>(keyComparer);
    }

    /// <inheritdoc />
    public override Type ElementType => typeof (TElement);

    /// <inheritdoc />
    public override ConfigElement CreateNew(Type type) => (ConfigElement) ConfigUtils.CreateInstance<TElement>(type, (object) this);

    /// <inheritdoc />
    public override void Add(ConfigElement item) => this.Add((TElement) item);

    /// <inheritdoc />
    public override void AddLinkedElement(object key, string path, string moduleName = null) => this.AddLinkedElement((TKey) key, path, moduleName);

    /// <inheritdoc />
    protected override IEnumerator<ConfigElement> GetEnumeratorInternal() => (IEnumerator<ConfigElement>) new BaseEnumerator<ConfigElement, TElement>(this.Elements.GetEnumerator());

    /// <inheritdoc />
    protected internal override IEnumerable<IConfigElementItem> Items => (IEnumerable<IConfigElementItem>) this.dictionary.Values;

    /// <inheritdoc />
    protected internal override IConfigElementItem GetItemByKey(string key)
    {
      TKey valueFromString = ConfigElement.GetValueFromString<TKey>(key, this.GetKeyProperty());
      ConfigElementItem<TElement> itemByKey = (ConfigElementItem<TElement>) null;
      this.dictionary.TryGetValue(valueFromString, out itemByKey);
      return (IConfigElementItem) itemByKey;
    }

    protected internal override void AddLazyInternal(
      object key,
      Func<ConfigElement> initializer,
      bool loadElement,
      params KeyValuePair<string, object>[] itemProperties)
    {
      this.AddLazyInternal((TKey) key, (Func<TElement>) (() => (TElement) initializer()), loadElement, itemProperties);
    }

    protected internal void AddLazyInternal(
      TKey key,
      Func<TElement> initializer,
      bool loadElement,
      params KeyValuePair<string, object>[] itemProperties)
    {
      string stringValue = ConfigElement.GetStringValue((object) key);
      ConfigElementLazyItem<TElement> configElementLazyItem = new ConfigElementLazyItem<TElement>((ConfigElement) this, stringValue, initializer);
      foreach (KeyValuePair<string, object> itemProperty in itemProperties)
        configElementLazyItem.ItemProperties[itemProperty.Key] = itemProperty.Value;
      this.dictionary[key] = (ConfigElementItem<TElement>) configElementLazyItem;
      if (loadElement)
        configElementLazyItem.Load();
      this.OnItemInserted((IConfigElementItem) configElementLazyItem, stringValue);
    }

    protected internal override void AddFailedElementItem(
      string key,
      string errMessage,
      string elementXml)
    {
      ConfigElementItem<TElement> configElementItem = new ConfigElementItem<TElement>(key, default (TElement));
      configElementItem.LoadingError = new ConfigElementLoadException()
      {
        ErrorMessage = errMessage,
        RawXml = elementXml
      };
      this.dictionary.Add(ConfigElement.GetValueFromString<TKey>(key, this.GetKeyProperty()), configElementItem);
      this.OnItemInserted((IConfigElementItem) configElementItem, key);
    }

    /// <summary>Adds element to the collection.</summary>
    /// <param name="item">The item.</param>
    public void Add(TElement element) => this.Add(this.GetKey(element), element);

    public void AddLazy(
      TKey key,
      Func<TElement> initializer,
      params KeyValuePair<string, object>[] itemProperties)
    {
      this.AddLazyInternal(key, initializer, !this.IsLoadingDefaults, itemProperties);
    }

    public void AddLinkedElement(TKey key, string path, string moduleName = null)
    {
      string stringValue = ConfigElement.GetStringValue((object) key);
      ConfigElementLink<TElement> configElementLink = new ConfigElementLink<TElement>(stringValue, path, moduleName);
      this.dictionary[key] = (ConfigElementItem<TElement>) configElementLink;
      this.OnItemInserted((IConfigElementItem) configElementLink, stringValue);
    }

    /// <summary>Clears all elements form the collection.</summary>
    public override void Clear()
    {
      base.Clear();
      this.dictionary.Clear();
    }

    /// <summary>
    /// Determines whether an element is in the collection. /&gt;.
    /// </summary>
    /// <returns>true if <paramref name="item" /> is found in the collection; otherwise, false.
    /// </returns>
    /// <param name="item">The object to locate in the collection.</param>
    public bool Contains(TElement item) => this.dictionary.ContainsKey((TKey) item[this.keyProperties[0].Name]);

    /// <summary>
    /// Determines whether an element is in the collection. /&gt;.
    /// </summary>
    /// <returns>true if <paramref name="item" /> is found in the collection; otherwise, false.
    /// </returns>
    /// <param name="item">The object to locate in the collection.</param>
    public override bool Contains(ConfigElement item) => this.Contains((TElement) item);

    /// <summary>
    /// Copies the entire collection to a compatible one-dimensional array, starting at the specified index of the target array.
    /// </summary>
    /// <param name="array">
    /// The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from collection. The <see cref="T:System.Array" /> must have zero-based indexing.
    /// </param>
    /// <param name="arrayIndex">
    /// The zero-based index in <paramref name="array" /> at which copying begins.
    /// </param>
    public void CopyTo(TElement[] array, int arrayIndex) => this.Values.CopyTo(array, arrayIndex);

    /// <summary>
    /// Copies the entire collection to a compatible one-dimensional array, starting at the specified index of the target array.
    /// </summary>
    /// <param name="array">
    /// The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from collection. The <see cref="T:System.Array" /> must have zero-based indexing.
    /// </param>
    /// <param name="arrayIndex">
    /// The zero-based index in <paramref name="array" /> at which copying begins.
    /// </param>
    public override void CopyTo(ConfigElement[] array, int arrayIndex) => this.CopyTo((TElement[]) array, arrayIndex);

    /// <summary>
    /// Gets the number of elements actually contained in the collection.
    /// </summary>
    /// <returns>
    /// The number of elements actually contained in the collection.
    /// </returns>
    public override int Count => this.dictionary.Count;

    /// <summary>
    /// Gets a value indicating whether the collection is read-only.
    /// </summary>
    /// <returns>true if the collection is read-only; otherwise, false. The default is false.
    /// </returns>
    public override bool IsReadOnly => false;

    /// <summary>
    /// Removes the first occurrence of a specific object from the collection.
    /// </summary>
    /// <param name="item">
    /// The <see cref="T:Telerik.Sitefinity.Configuration.ConfigElement" /> to remove from the collection.
    /// The value can be null.
    /// </param>
    public bool Remove(TElement item) => this.Remove((TKey) item[this.keyProperties[0].Name]);

    /// <summary>
    /// Removes the first occurrence of a specific object from the collection.
    /// </summary>
    /// <param name="item">
    /// The <see cref="T:Telerik.Sitefinity.Configuration.ConfigElement" /> to remove from the collection.
    /// The value can be null.
    /// </param>
    public override bool Remove(ConfigElement item) => this.Remove((TElement) item);

    /// <summary>
    /// Determines whether the collection contains an item with the specified key.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns>
    /// 	<c>true</c> if contains an item with the specified key; otherwise, <c>false</c>.
    /// </returns>
    protected internal override ConfigElement GetElementByKey(string key)
    {
      TElement element;
      return this.TryGetValue(ConfigElement.GetValueFromString<TKey>(key, this.GetKeyProperty()), out element) ? (ConfigElement) element : (ConfigElement) null;
    }

    /// <summary>Adds the specified key and value to the dictionary.</summary>
    /// <param name="key">The key of the element to add.</param>
    /// <param name="value">
    /// The value of the element to add. The value can be null for reference types.
    /// </param>
    public void Add(TKey key, TElement element)
    {
      if (this.HandleLinkedElement((ConfigElement) element))
        return;
      if (element.Parent == null)
        element.Parent = (ConfigElement) this;
      string stringValue = ConfigElement.GetStringValue((object) key);
      ConfigElementItem<TElement> configElementItem = new ConfigElementItem<TElement>(stringValue, element);
      this.dictionary.Add(key, configElementItem);
      this.OnItemInserted((IConfigElementItem) configElementItem, stringValue);
    }

    /// <summary>
    /// Determines whether the dictionary contains the specified key.
    /// </summary>
    /// <returns>true if the dictionary contains an element with the specified key; otherwise, false.
    /// </returns>
    /// <param name="key">The key to locate in the dictionary.</param>
    public bool ContainsKey(TKey key) => this.dictionary.ContainsKey(key);

    /// <summary>Determines whether the specified key contains key.</summary>
    /// <param name="key">The key.</param>
    /// <returns>
    /// 	<c>true</c> if the specified key contains key; otherwise, <c>false</c>.
    /// </returns>
    public override bool Contains(string key) => this.ContainsKey(ConfigElement.GetValueFromString<TKey>(key, this.GetKeyProperty()));

    /// <summary>
    /// Gets a collection containing the keys in the dictionary.
    /// </summary>
    /// <returns>A collection containing the keys in the dictionary.</returns>
    public ICollection<TKey> Keys => (ICollection<TKey>) this.dictionary.Keys;

    /// <summary>
    /// Removes the value with the specified key from the dictionary.
    /// </summary>
    /// <returns>true if the element is successfully found and removed; otherwise, false.  This method returns false if <paramref name="key" /> is not found in the dictionary.
    /// </returns>
    /// <param name="key">The key of the element to remove.</param>
    public bool Remove(TKey key)
    {
      ConfigElementItem<TElement> configElementItem;
      if (!this.dictionary.TryGetValue(key, out configElementItem))
        return false;
      int num = this.dictionary.Remove(key) ? 1 : 0;
      if (num == 0)
        return num != 0;
      if (configElementItem is IConfigElementLink)
        return num != 0;
      this.OnElementRemoved((ConfigElement) configElementItem.Element);
      return num != 0;
    }

    /// <summary>Gets the value associated with the specified key.</summary>
    /// <returns>true if the dictionary contains an element with the specified key; otherwise, false.
    /// </returns>
    /// <param name="key">The key of the value to get.</param>
    /// <param name="value">
    /// When this method returns, contains the value associated with the specified key, if the key is found; otherwise, the default value for the type of the <paramref name="value" /> parameter. This parameter is passed un initialized.
    /// </param>
    public bool TryGetValue(TKey key, out TElement value) => this.TryGetValueInternal(key, out value);

    /// <summary>
    /// Gets a collection containing the values in the dictionary.
    /// </summary>
    /// <returns>A collection containing the values in the dictionary.</returns>
    public ICollection<TElement> Values => (ICollection<TElement>) this.Elements.ToArray<TElement>();

    /// <summary>
    /// Gets or sets the value associated with the specified key.
    /// </summary>
    /// <returns>
    /// The value associated with the specified key. If the specified key is not found, a get operation throws a <see cref="T:System.Collections.Generic.KeyNotFoundException" />, and a set operation creates a new element with the specified key.
    /// </returns>
    /// <param name="key">The key of the value to get or set.</param>
    public TElement this[TKey key]
    {
      get
      {
        TElement element = default (TElement);
        return this.TryGetValueInternal(key, out element) ? element : default (TElement);
      }
      set
      {
        if (!this.dictionary.ContainsKey(key))
          this.Add(key, value);
        else
          this.dictionary[key].Element = value;
      }
    }

    void ICollection<KeyValuePair<TKey, TElement>>.Add(
      KeyValuePair<TKey, TElement> item)
    {
      this.Add(item.Value);
    }

    bool ICollection<KeyValuePair<TKey, TElement>>.Contains(
      KeyValuePair<TKey, TElement> item)
    {
      return ((ICollection<KeyValuePair<TKey, TElement>>) this.dictionary).Contains(item);
    }

    void ICollection<KeyValuePair<TKey, TElement>>.CopyTo(
      KeyValuePair<TKey, TElement>[] array,
      int arrayIndex)
    {
      ((ICollection<KeyValuePair<TKey, TElement>>) this.dictionary).CopyTo(array, arrayIndex);
    }

    bool ICollection<KeyValuePair<TKey, TElement>>.Remove(
      KeyValuePair<TKey, TElement> item)
    {
      return this.Remove(item.Key);
    }

    IEnumerator<KeyValuePair<TKey, TElement>> IEnumerable<KeyValuePair<TKey, TElement>>.GetEnumerator() => (IEnumerator<KeyValuePair<TKey, TElement>>) this.dictionary.ToDictionary<KeyValuePair<TKey, ConfigElementItem<TElement>>, TKey, TElement>((Func<KeyValuePair<TKey, ConfigElementItem<TElement>>, TKey>) (i => i.Key), (Func<KeyValuePair<TKey, ConfigElementItem<TElement>>, TElement>) (i => i.Value.Element)).GetEnumerator();

    protected virtual TKey GetKey(TElement item) => (TKey) item[this.keyProperties[0].Name];

    private bool TryGetValueInternal(TKey key, out TElement element)
    {
      ConfigElementItem<TElement> configElementItem;
      if ((object) key != null && this.dictionary.TryGetValue(key, out configElementItem))
      {
        element = configElementItem.Element;
        return (object) element != null;
      }
      element = default (TElement);
      return false;
    }

    internal bool TryGetItem(TKey key, out IConfigElementItem item)
    {
      ConfigElementItem<TElement> configElementItem;
      if (this.dictionary.TryGetValue(key, out configElementItem))
      {
        item = (IConfigElementItem) configElementItem;
        return true;
      }
      item = (IConfigElementItem) null;
      return false;
    }

    /// <summary>
    /// Using <see cref="P:Telerik.Sitefinity.Configuration.ConfigElementDictionary`2.Values" /> loads all lazy items, as the IEnumerable is traversed by a ToArray call.
    /// Use this property to avoid that, as it returns a lazy collection of values.
    /// </summary>
    public IEnumerable<TElement> Elements => this.dictionary.Values.Select<ConfigElementItem<TElement>, TElement>((Func<ConfigElementItem<TElement>, TElement>) (item => item.Element)).Where<TElement>((Func<TElement, bool>) (e => (object) e != null));
  }
}
