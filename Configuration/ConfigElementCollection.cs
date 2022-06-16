// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.ConfigElementCollection
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Telerik.Sitefinity.Configuration.Data;

namespace Telerik.Sitefinity.Configuration
{
  /// <summary>
  /// Represents base class for configuration element collections.
  /// </summary>
  public abstract class ConfigElementCollection : 
    ConfigElement,
    ICollection<ConfigElement>,
    IEnumerable<ConfigElement>,
    IEnumerable
  {
    protected ConfigProperty[] keyProperties;
    private bool isCleared;
    private ISet<string> removedKeys;
    private IList<Tuple<string, ConfigSource>> changes;
    private ConfigElementCollection.TryInitializeDefaults initializeDefaultsHandler;
    private IList<Tuple<string, Action<ConfigElement, string>, ConfigSource>> pendingLoads;
    private IList<string> inactiveElements;

    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    protected ConfigElementCollection(ConfigElement parent)
      : base(parent)
    {
      this.keyProperties = ConfigElement.GetKeyPropertiesFromType(this.ElementType);
    }

    /// <summary>Gets the type of the element.</summary>
    /// <value>The type of the element.</value>
    public abstract Type ElementType { get; }

    /// <summary>Creates a new element.</summary>
    /// <param name="type">
    /// The type of the element to create - must be inheritor of the collection element's type.
    /// When <c>null</c>, collection's element type is used.
    /// </param>
    /// <returns>The newly created element.</returns>
    public abstract ConfigElement CreateNew(Type type);

    /// <summary>Adds element to the collection.</summary>
    /// <param name="item">ConfigElement</param>
    public abstract void Add(ConfigElement element);

    /// <summary>
    /// Adds a lazy (loaded on demand) element to the collection.
    /// </summary>
    /// <param name="key">The key of the element.</param>
    /// <param name="initializer">A delegate, that should produce the element itself, when demanded.</param>
    /// <param name="itemProperties">Non-lazy properties, that will be accessible, even when the lazy element is not loaded.</param>
    public virtual void AddLazy(
      object key,
      Func<ConfigElement> initializer,
      params KeyValuePair<string, object>[] itemProperties)
    {
      this.AddLazyInternal(key, initializer, !this.IsLoadingDefaults, itemProperties);
    }

    /// <summary>Adds a link element to the collection.</summary>
    /// <param name="key">The key of the element.</param>
    /// <param name="path">The configuration path, to the real element.</param>
    /// <param name="moduleName">The name of the module, that should be initialized, to make the linked element accessible.</param>
    public abstract void AddLinkedElement(object key, string path, string moduleName = null);

    /// <summary>Determines whether the specified key contains key.</summary>
    /// <param name="key">The key.</param>
    /// <returns>
    /// 	<c>true</c> if the specified key contains key; otherwise, <c>false</c>.
    /// </returns>
    public abstract bool Contains(string key);

    /// <summary>Returns an enumerator for the entire Collection.</summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerator" /> for the entire collection.
    /// </returns>
    protected abstract IEnumerator<ConfigElement> GetEnumeratorInternal();

    /// <summary>Returns an enumeration of collection's items.</summary>
    protected internal abstract IEnumerable<IConfigElementItem> Items { get; }

    /// <summary>Gets a collection item by its key.</summary>
    /// <param name="key">The key of the item.</param>
    /// <returns>The item.</returns>
    protected internal abstract IConfigElementItem GetItemByKey(string key);

    protected internal abstract void AddLazyInternal(
      object key,
      Func<ConfigElement> initializer,
      bool loadElement,
      params KeyValuePair<string, object>[] itemProperties);

    protected internal abstract void AddFailedElementItem(
      string key,
      string errMessage,
      string elementXml);

    /// <summary>
    /// Specifies the name of the element when adding items to the collection.
    /// The default value is "add".
    /// </summary>
    [ConfigurationProperty("addElementName", DefaultValue = "add")]
    protected internal string AddElementName
    {
      get => (string) this["addElementName"];
      set => this["addElementName"] = (object) value;
    }

    /// <summary>
    /// Specifies the name of the element when removing items from the collection.
    /// The default value is "remove".
    /// </summary>
    [ConfigurationProperty("removeElementName", DefaultValue = "remove")]
    protected internal string RemoveElementName
    {
      get => (string) this["removeElementName"];
      set => this["removeElementName"] = (object) value;
    }

    /// <summary>
    /// Specifies the name of the element for clearing all items from the collection.
    /// The default value is "clear".
    /// </summary>
    [ConfigurationProperty("clearElementName", DefaultValue = "clear")]
    protected internal string ClearElementName
    {
      get => (string) this["clearElementName"];
      set => this["clearElementName"] = (object) value;
    }

    /// <summary>
    /// Specifies the name of the element when editing items to the collection.
    /// The default value is "edit".
    /// </summary>
    [ConfigurationProperty("editElementName", DefaultValue = "edit")]
    protected internal string EditElementName
    {
      get => (string) this["editElementName"];
      set => this["editElementName"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value defining the remove and clear behavior of the collection.
    /// When Keep property is false, if clear element is encountered the entire collection is cleared,
    /// if remove element is encountered and there is a match in the collection to that
    /// element the item is removed.
    /// When Keep is true instead of clearing or removing items from the collection,
    /// new item is added that can be used at runtime to perform operation.
    /// The default value is false.
    /// </summary>
    [ConfigurationProperty("keepRemoveItems", DefaultValue = false)]
    protected internal bool KeepRemoveItems
    {
      get => (bool) this["keepRemoveItems"];
      set => this["keepRemoveItems"] = (object) value;
    }

    /// <summary>Indicates whether the collection has been cleared.</summary>
    protected internal virtual bool IsCleared => this.isCleared;

    /// <summary>
    /// The keys removed from the collection since it was initialized or cleared.
    /// </summary>
    protected internal ISet<string> RemovedKeys => this.removedKeys ?? (ISet<string>) new HashSet<string>();

    /// <summary>
    /// When <c>true</c> change tracking is disabled.
    /// </summary>
    protected internal bool SuppressChangeTracking { get; set; }

    /// <summary>Creates a new element.</summary>
    /// <returns>The newly created element.</returns>
    public virtual ConfigElement CreateNew() => this.CreateNew((Type) null);

    /// <summary>Clears all elements form the collection.</summary>
    public virtual void Clear()
    {
      if (!this.IsTrackingChanges())
        return;
      this.isCleared = true;
      if (this.removedKeys == null)
        return;
      this.removedKeys.Clear();
    }

    protected internal void SuppressingChangeTracking(Action action)
    {
      bool suppressChangeTracking = this.SuppressChangeTracking;
      try
      {
        this.SuppressChangeTracking = true;
        action();
      }
      finally
      {
        this.SuppressChangeTracking = suppressChangeTracking;
      }
    }

    /// <summary>
    /// Determines whether an element is in the collection. /&gt;.
    /// </summary>
    /// <returns>true if <paramref name="item" /> is found in the collection; otherwise, false.
    /// </returns>
    /// <param name="item">The object to locate in the collection.</param>
    public abstract bool Contains(ConfigElement item);

    /// <summary>
    /// Copies the entire collection to a compatible one-dimensional array, starting at the specified index of the target array.
    /// </summary>
    /// <param name="array">
    /// The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from collection. The <see cref="T:System.Array" /> must have zero-based indexing.
    /// </param>
    /// <param name="arrayIndex">
    /// The zero-based index in <paramref name="array" /> at which copying begins.
    /// </param>
    public abstract void CopyTo(ConfigElement[] array, int arrayIndex);

    /// <summary>
    /// Gets the number of elements actually contained in the collection.
    /// </summary>
    /// <returns>
    /// The number of elements actually contained in the collection.
    /// </returns>
    public abstract int Count { get; }

    /// <summary>
    /// Gets a value indicating whether the collection is read-only.
    /// </summary>
    /// <returns>true if the collection is read-only; otherwise, false. The default is false.
    /// </returns>
    public abstract bool IsReadOnly { get; }

    /// <summary>
    /// Removes the first occurrence of a specific object from the collection.
    /// </summary>
    /// <param name="item">
    /// The <see cref="T:Telerik.Sitefinity.Configuration.ConfigElement" /> to remove from the collection.
    /// The value can be null.
    /// </param>
    public abstract bool Remove(ConfigElement item);

    /// <summary>Returns an enumerator for the entire Colleciton.</summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerator" /> for the entire collection.
    /// </returns>
    public virtual IEnumerator<ConfigElement> GetEnumerator() => this.GetEnumeratorInternal();

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumeratorInternal();

    protected internal ConfigProperty GetKeyProperty() => this.keyProperties != null && this.keyProperties.Length == 1 ? this.keyProperties[0] : (ConfigProperty) null;

    protected internal string GetKeyName()
    {
      ConfigProperty keyProperty = this.GetKeyProperty();
      return keyProperty == null ? string.Empty : keyProperty.Name;
    }

    protected internal string GenerateChildKey(ConfigElement child) => this.GenerateChildKey(child, -1);

    protected internal virtual string GenerateChildKey(ConfigElement child, int index)
    {
      if (this.keyProperties != null && this.keyProperties.Length == 1)
      {
        ConfigProperty keyProperty = this.keyProperties[0];
        object obj = child[keyProperty.Name];
        if (obj != null)
          return ConfigElement.GetStringValue(obj, keyProperty);
      }
      else if (index >= 0)
        return index.ToString();
      return string.Empty;
    }

    protected virtual void OnItemInserted(IConfigElementItem item, string key)
    {
    }

    protected virtual void OnElementRemoved(ConfigElement element) => this.TrackRemovedKey(element.GetKey());

    protected internal virtual bool IsTrackingChanges() => !this.SuppressChangeTracking && !this.IsLoadingDefaults;

    protected internal virtual void TrackRemovedKey(string key)
    {
      if (!this.IsTrackingChanges())
        return;
      if (this.removedKeys == null)
        this.removedKeys = (ISet<string>) new HashSet<string>();
      this.removedKeys.Add(key);
    }

    protected internal bool HandleLinkedElement(ConfigElement item)
    {
      if (item.Parent == null || item.Parent == this)
        return false;
      if (!this.SupportLinkedElements)
        throw new NotSupportedException("This collection does not support linked elements");
      this.AddLinkedElement((object) item.GetKey(), item.GetPath(), item.LinkModuleName);
      return true;
    }

    protected internal override ConfigElement Clone(
      ConfigElement parent = null,
      bool deepCopy = true)
    {
      ConfigElementCollection withParent = (ConfigElementCollection) base.Clone(parent, deepCopy);
      foreach (ConfigElement configElement in this)
      {
        ConfigElement element = configElement.Clone((ConfigElement) withParent, deepCopy);
        withParent.Add(element);
      }
      return (ConfigElement) withParent;
    }

    internal string GenerateChildPathForKey(string childKey) => this.GetPath() + (object) '/' + childKey;

    internal bool SupportLinkedElements => this.keyProperties != null && (uint) this.keyProperties.Length > 0U;

    internal bool IsDefaultLoadingPostponed => this.initializeDefaultsHandler != null;

    internal bool EnsureDelayedInitialization()
    {
      if (this.initializeDefaultsHandler != null && !this.IsLoadingDefaults)
      {
        lock (this)
        {
          if (this.initializeDefaultsHandler != null)
          {
            ConfigSection section = this.Section;
            try
            {
              if (section != null)
                section.isLoadingDefaults = true;
              if (!this.initializeDefaultsHandler((ConfigElement) this))
                return false;
              this.initializeDefaultsHandler = (ConfigElementCollection.TryInitializeDefaults) null;
              if (this.pendingLoads != null)
              {
                foreach (Tuple<string, Action<ConfigElement, string>, ConfigSource> pendingLoad in (IEnumerable<Tuple<string, Action<ConfigElement, string>, ConfigSource>>) this.pendingLoads)
                  pendingLoad.Item2((ConfigElement) this, pendingLoad.Item1);
                this.pendingLoads = (IList<Tuple<string, Action<ConfigElement, string>, ConfigSource>>) null;
              }
            }
            finally
            {
              if (section != null)
                section.isLoadingDefaults = false;
            }
          }
        }
      }
      return true;
    }

    internal bool TryGetPendingLoad(SaveOptions options, out string xml)
    {
      ConfigSource source = ConfigSource.FileSystem;
      if (options.DatabaseMode)
        source = ConfigSource.Database;
      if (this.pendingLoads != null)
      {
        Tuple<string, Action<ConfigElement, string>, ConfigSource> tuple = this.pendingLoads.Where<Tuple<string, Action<ConfigElement, string>, ConfigSource>>((Func<Tuple<string, Action<ConfigElement, string>, ConfigSource>, bool>) (x => x.Item3 == source)).LastOrDefault<Tuple<string, Action<ConfigElement, string>, ConfigSource>>();
        if (tuple != null)
        {
          xml = tuple.Item1;
          return true;
        }
      }
      xml = (string) null;
      return false;
    }

    internal void RegisterDelayedInitializer(
      ConfigElementCollection.TryInitializeDefaults initializerDefaultsHandler,
      bool tryRunNow = false,
      bool skipValidation = false)
    {
      if (!skipValidation && !this.IsLoadingDefaults)
        throw new InvalidOperationException("RegisterDelayedInitializer can be called only on loading default properties (within OnPropertiesInitialized method)");
      if (tryRunNow && initializerDefaultsHandler((ConfigElement) this))
        return;
      this.initializeDefaultsHandler = initializerDefaultsHandler;
    }

    internal void RegisterPendingLoad(
      string xmlString,
      Action<ConfigElement, string> pendingLoad,
      LoadContext loadContext)
    {
      if (this.pendingLoads == null)
        this.pendingLoads = (IList<Tuple<string, Action<ConfigElement, string>, ConfigSource>>) new List<Tuple<string, Action<ConfigElement, string>, ConfigSource>>();
      this.pendingLoads.Add(new Tuple<string, Action<ConfigElement, string>, ConfigSource>(xmlString, pendingLoad, loadContext.Source));
    }

    internal void RegisterInactiveElement(string xmlString)
    {
      if (this.inactiveElements == null)
        this.inactiveElements = (IList<string>) new List<string>();
      this.inactiveElements.Add(xmlString);
    }

    internal IList<string> GetInactiveElements() => this.inactiveElements;

    internal bool IsDynamic { get; set; }

    internal void SetChangeSource(ConfigSource source, string operation, string key = null)
    {
      if (this.changes == null)
        this.changes = (IList<Tuple<string, ConfigSource>>) new List<Tuple<string, ConfigSource>>();
      this.changes.Add(new Tuple<string, ConfigSource>(operation + key, source));
    }

    internal ConfigSource GetChangeSource(string operation, string key = null)
    {
      if (this.changes != null)
      {
        Tuple<string, ConfigSource> tuple = this.changes.FirstOrDefault<Tuple<string, ConfigSource>>((Func<Tuple<string, ConfigSource>, bool>) (i => i.Item1 == operation + key));
        if (tuple != null)
          return tuple.Item2;
      }
      return ConfigSource.NotSet;
    }

    /// <summary>Used to provide ConfigElement default initializer.</summary>
    /// <param name="element"></param>
    /// <returns></returns>
    internal delegate bool TryInitializeDefaults(ConfigElement element);
  }
}
