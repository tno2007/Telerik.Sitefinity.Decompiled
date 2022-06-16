// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.ConfigElementList`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Utilities;

namespace Telerik.Sitefinity.Configuration
{
  /// <summary>Represents a list of configuration elements.</summary>
  public class ConfigElementList<TElement> : 
    ConfigElementCollection,
    IList<TElement>,
    ICollection<TElement>,
    IEnumerable<TElement>,
    IEnumerable,
    IConfigElementCollection<TElement>
    where TElement : ConfigElement
  {
    private Dictionary<string, ConfigElementItem<TElement>> dictionary = new Dictionary<string, ConfigElementItem<TElement>>();
    private List<ConfigElementItem<TElement>> list = new List<ConfigElementItem<TElement>>();

    /// <summary>Initializes new instance.</summary>
    internal ConfigElementList()
      : this((ConfigElement) null)
    {
    }

    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public ConfigElementList(ConfigElement parent)
      : base(parent)
    {
    }

    /// <inheritdoc />
    public override Type ElementType => typeof (TElement);

    /// <inheritdoc />
    public override ConfigElement CreateNew(Type type) => (ConfigElement) ConfigUtils.CreateInstance<TElement>(type, (object) this);

    /// <inheritdoc />
    public override void Add(ConfigElement element) => this.Add((TElement) element);

    /// <inheritdoc />
    public override void AddLazy(
      object key,
      Func<ConfigElement> initializer,
      params KeyValuePair<string, object>[] itemProperties)
    {
      this.AddLazy(ConfigElement.GetStringValue(key, this.GetKeyProperty()), (Func<TElement>) (() => (TElement) initializer()), itemProperties);
    }

    /// <inheritdoc />
    public override void AddLinkedElement(object key, string path, string moduleName = null) => this.AddLinkedElement(ConfigElement.GetStringValue(key, this.GetKeyProperty()), path, moduleName);

    /// <inheritdoc />
    protected override IEnumerator<ConfigElement> GetEnumeratorInternal() => (IEnumerator<ConfigElement>) new BaseEnumerator<ConfigElement, TElement>(this.GetEnumerator());

    /// <inheritdoc />
    protected internal override IEnumerable<IConfigElementItem> Items => (IEnumerable<IConfigElementItem>) this.list;

    /// <inheritdoc />
    protected internal override IConfigElementItem GetItemByKey(string key)
    {
      ConfigElementItem<TElement> itemByKey = (ConfigElementItem<TElement>) null;
      this.dictionary.TryGetValue(key, out itemByKey);
      return (IConfigElementItem) itemByKey;
    }

    protected internal override void AddLazyInternal(
      object key,
      Func<ConfigElement> initializer,
      bool loadElement,
      params KeyValuePair<string, object>[] itemProperties)
    {
      this.AddLazyInternal(ConfigElement.GetStringValue(key, this.GetKeyProperty()), (Func<TElement>) (() => (TElement) initializer()), loadElement, itemProperties);
    }

    protected internal void AddLazyInternal(
      string key,
      Func<TElement> initializer,
      bool loadElement,
      params KeyValuePair<string, object>[] itemProperties)
    {
      ConfigElementLazyItem<TElement> configElementLazyItem = new ConfigElementLazyItem<TElement>((ConfigElement) this, key, initializer);
      foreach (KeyValuePair<string, object> itemProperty in itemProperties)
        configElementLazyItem.ItemProperties[itemProperty.Key] = itemProperty.Value;
      if (loadElement)
        configElementLazyItem.Load();
      this.list.Add((ConfigElementItem<TElement>) configElementLazyItem);
      this.OnItemInserted((IConfigElementItem) configElementLazyItem, key);
    }

    /// <summary>
    /// A convenience method that returns the <see cref="T:System.Collections.Generic.IEnumerable`1" /> implementation of the current instance.
    /// </summary>
    /// <returns>The <see cref="T:System.Collections.Generic.IEnumerable`1" /> implementation of the current instance</returns>
    public IEnumerable<TElement> AsEnumerable() => (IEnumerable<TElement>) this;

    /// <summary>
    /// Searches for the specified object and returns the zero-based index of the first occurrence within the entire list.
    /// </summary>
    /// <returns>
    /// The zero-based index of the first occurrence of <paramref name="item" /> within the entire list, if found; otherwise, –1.
    /// </returns>
    /// <param name="item">
    /// The object to locate in the list. The value can be null for reference types.
    /// </param>
    public int IndexOf(TElement item) => this.Elements.ToList<TElement>().IndexOf(item);

    /// <summary>
    /// Inserts an element into the list at the specified index.
    /// </summary>
    /// <param name="index">
    /// The zero-based index at which <paramref name="item" /> should be inserted.
    /// </param>
    /// <param name="item">
    /// The object to insert. The value can be null for reference types.
    /// </param>
    public void Insert(int index, TElement element)
    {
      string childKey = this.GenerateChildKey((ConfigElement) element, this.list.Count);
      ConfigElementItem<TElement> configElementItem = new ConfigElementItem<TElement>(childKey, element);
      this.list.Insert(index, configElementItem);
      this.OnItemInserted((IConfigElementItem) configElementItem, childKey);
    }

    /// <summary>
    /// Removes the element at the specified index of the list.
    /// </summary>
    /// <param name="index">
    /// The zero-based index of the element to remove.
    /// </param>
    public void RemoveAt(int index)
    {
      ConfigElementItem<TElement> configElementItem = this.list[index];
      this.list.RemoveAt(index);
      this.OnElementRemoved((ConfigElement) configElementItem.Element);
    }

    /// <summary>Gets or sets the element at the specified index.</summary>
    /// <returns>The element at the specified index.</returns>
    /// <param name="index">
    /// The zero-based index of the element to get or set.
    /// </param>
    public TElement this[int index]
    {
      get
      {
        ConfigElementItem<TElement> configElementItem = this.list[index];
        return configElementItem != null ? configElementItem.Element : default (TElement);
      }
      set
      {
        ConfigElementItem<TElement> configElementItem = this.list[index];
        if (configElementItem == null)
        {
          this.Add(value);
        }
        else
        {
          configElementItem.Element = value;
          if (this.GenerateChildKey((ConfigElement) value, this.list.Count) != configElementItem.Key)
            throw new NotSupportedException("Element assignment, changing item's key is not supported.");
        }
      }
    }

    /// <summary>Adds element to the collection.</summary>
    /// <param name="item">ConfigElement</param>
    public void Add(TElement element)
    {
      if (this.HandleLinkedElement((ConfigElement) element))
        return;
      if (element.Parent == null)
        element.Parent = (ConfigElement) this;
      string childKey = this.GenerateChildKey((ConfigElement) element, this.list.Count);
      ConfigElementItem<TElement> configElementItem = new ConfigElementItem<TElement>(childKey, element);
      this.list.Add(configElementItem);
      this.OnItemInserted((IConfigElementItem) configElementItem, childKey);
    }

    public virtual void AddLazy(
      string key,
      Func<TElement> initializer,
      params KeyValuePair<string, object>[] itemProperties)
    {
      this.AddLazyInternal(key, initializer, !this.IsLoadingDefaults, itemProperties);
    }

    public void AddLinkedElement(string key, string path, string moduleName = null)
    {
      ConfigElementLink<TElement> configElementLink = new ConfigElementLink<TElement>(key, path, moduleName);
      this.list.Add((ConfigElementItem<TElement>) configElementLink);
      this.OnItemInserted((IConfigElementItem) configElementLink, key);
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
      this.list.Add(configElementItem);
      this.OnItemInserted((IConfigElementItem) configElementItem, key);
    }

    /// <summary>Clears all elements form the collection.</summary>
    public override void Clear()
    {
      base.Clear();
      this.list.Clear();
      this.dictionary.Clear();
    }

    /// <summary>
    /// Determines whether an element is in the collection. /&gt;.
    /// </summary>
    /// <returns>true if <paramref name="item" /> is found in the collection; otherwise, false.
    /// </returns>
    /// <param name="item">The object to locate in the collection.</param>
    public bool Contains(TElement item) => this.Elements.Contains<TElement>(item);

    /// <summary>
    /// Determines whether an element is in the collection. /&gt;.
    /// </summary>
    /// <returns>true if <paramref name="item" /> is found in the collection; otherwise, false.
    /// </returns>
    /// <param name="item">The object to locate in the collection.</param>
    public override bool Contains(ConfigElement item) => this.Contains((TElement) item);

    /// <summary>Determines whether the specified key contains key.</summary>
    /// <param name="key">The key.</param>
    /// <returns>
    /// 	<c>true</c> if the specified key contains key; otherwise, <c>false</c>.
    /// </returns>
    public override bool Contains(string key) => this.dictionary.ContainsKey(key);

    /// <summary>
    /// Copies the entire collection to a compatible one-dimensional array, starting at the specified index of the target array.
    /// </summary>
    /// <param name="array">
    /// The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from collection. The <see cref="T:System.Array" /> must have zero-based indexing.
    /// </param>
    /// <param name="arrayIndex">
    /// The zero-based index in <paramref name="array" /> at which copying begins.
    /// </param>
    public void CopyTo(TElement[] array, int arrayIndex) => this.Elements.ToArray<TElement>().CopyTo((Array) array, arrayIndex);

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
    public override int Count => this.list.Count;

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
    public bool Remove(TElement item)
    {
      if ((object) item == null)
        return false;
      int index = this.Elements.ToList<TElement>().IndexOf(item);
      if (index < 0)
        return false;
      if (!string.IsNullOrEmpty(this.GetKeyName()))
      {
        this.list.RemoveAt(index);
        this.OnElementRemoved((ConfigElement) item);
        this.TrackRemovedKey(this.GetKey());
      }
      else
      {
        IConfigElementItem itemByKey = this.GetItemByKey(item.GetKey());
        if (itemByKey != null)
          itemByKey.IsDeleted = true;
      }
      return true;
    }

    /// <summary>
    /// Removes the first occurrence of a specific object from the collection.
    /// </summary>
    /// <param name="item">
    /// The <see cref="T:Telerik.Sitefinity.Configuration.ConfigElement" /> to remove from the collection.
    /// The value can be null.
    /// </param>
    public override bool Remove(ConfigElement item) => this.Remove((TElement) item);

    /// <summary>Returns an enumerator for the entire Colleciton.</summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerator" /> for the entire collection.
    /// </returns>
    public IEnumerator<TElement> GetEnumerator() => (IEnumerator<TElement>) this.Elements.ToList<TElement>().GetEnumerator();

    protected override void OnItemInserted(IConfigElementItem item, string key)
    {
      if (key == null)
        key = item.Element.GetKey();
      base.OnItemInserted(item, key);
      this.dictionary[key] = (ConfigElementItem<TElement>) item;
    }

    protected override void OnElementRemoved(ConfigElement element)
    {
      base.OnElementRemoved(element);
      this.dictionary.Remove(element.GetKey());
    }

    protected internal override ConfigElement GetElementByKey(string key)
    {
      ConfigElementItem<TElement> configElementItem;
      return this.dictionary.TryGetValue(key, out configElementItem) ? (ConfigElement) configElementItem.Element : (ConfigElement) null;
    }

    public IEnumerable<TElement> Elements => this.list.Where<ConfigElementItem<TElement>>((Func<ConfigElementItem<TElement>, bool>) (item => !item.IsDeleted)).Select<ConfigElementItem<TElement>, TElement>((Func<ConfigElementItem<TElement>, TElement>) (item => item.Element));
  }
}
