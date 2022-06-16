// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.VirtualPath.DataItemSystemCacheDependency
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Data;

namespace Telerik.Sitefinity.Abstractions.VirtualPath
{
  /// <summary>
  /// Establishes a dependency relationship between a Sitefinity data item with specified id and type and an object of type
  /// <see cref="T:System.Web.Caching.CacheDependency" />.
  /// The <see cref="T:System.Web.Caching.CacheDependency" /> class monitors
  /// the dependency relationships so that when any of them changes, the cached item
  /// will be automatically removed.
  /// </summary>
  public class DataItemSystemCacheDependency : System.Web.Caching.CacheDependency
  {
    private string itemId;
    private Type itemType;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Abstractions.VirtualPath.DataItemSystemCacheDependency" /> class.
    /// </summary>
    public DataItemSystemCacheDependency()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Abstractions.VirtualPath.DataItemSystemCacheDependency" /> class.
    /// </summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="itemId">The page id.</param>
    /// <param name="uniquePrefix">Unique key prefix used for generating a recognizable unique key for the current cache dependency.</param>
    public DataItemSystemCacheDependency(Type itemType, string itemId, string uniquePrefix = "")
    {
      this.itemId = itemId;
      this.itemType = itemType;
      this.UniquePrefix = uniquePrefix;
      this.Expire = new ChangedCallback(this.ExpireItem);
      Telerik.Sitefinity.Data.CacheDependency.Subscribe(typeof (CacheDependencyHandler), this.itemType, itemId, this.Expire);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Abstractions.VirtualPath.DataItemSystemCacheDependency" /> class.
    /// </summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="uniquePrefix">Unique key prefix used for generating a recognizable unique key for the current cache dependency.</param>
    public DataItemSystemCacheDependency(Type itemType, string uniquePrefix = "")
    {
      this.itemType = itemType;
      this.UniquePrefix = uniquePrefix;
      this.Expire = new ChangedCallback(this.ExpireItem);
      Telerik.Sitefinity.Data.CacheDependency.Subscribe(typeof (CacheDependencyHandler), itemType, this.Expire);
    }

    /// <summary>Gets or sets the type of the item.</summary>
    /// <value>The type of the item.</value>
    public Type ItemType
    {
      get => this.itemType;
      set => this.itemType = value;
    }

    /// <summary>Gets or sets the unique prefix.</summary>
    /// <value>The unique prefix.</value>
    public string UniquePrefix { get; set; }

    /// <summary>Gets the expiration callback.</summary>
    /// <value>The expiration callback.</value>
    public ChangedCallback Expire { get; private set; }

    /// <summary>Notifies the base <see cref="T:System.Web.Caching.CacheDependency" />
    /// object that the dependency represented by a derived <see cref="T:System.Web.Caching.CompositeCacheDependency" />
    /// class has changed.</summary>
    /// <param name="caller">The caller.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="itemKey">The item key.</param>
    public void ExpireItem(ICacheDependencyHandler caller, Type itemType, string itemKey)
    {
      this.NotifyDependencyChanged((object) caller, EventArgs.Empty);
      if (!itemKey.IsNullOrEmpty())
        Telerik.Sitefinity.Data.CacheDependency.Unsubscribe(typeof (CacheDependencyHandler), itemType, this.itemId, this.Expire);
      else
        Telerik.Sitefinity.Data.CacheDependency.Unsubscribe(typeof (CacheDependencyHandler), itemType, this.Expire);
    }

    /// <summary>
    /// Subscribes for notification when an item of the provided type and key is changed.
    /// This overload subscribes only to handlers that inherit from the specified type and has specified key.
    /// </summary>
    /// <param name="trackedType">Type of the tracked.</param>
    /// <param name="key">The key.</param>
    /// <param name="callback">The callback.</param>
    public void Subscribe(Type trackedType, string key, ChangedCallback callback)
    {
      this.itemType = trackedType;
      Telerik.Sitefinity.Data.CacheDependency.Subscribe(typeof (CacheDependencyHandler), trackedType, key, callback);
    }

    /// <summary>
    /// Subscribes for notification when an item of the provided type is changed.
    /// This overload subscribes only to handlers that inherit from the specified type.
    /// </summary>
    /// <param name="trackedType">Type to track.</param>
    /// <param name="callback">The callback.</param>
    public void Subscribe(Type trackedType, ChangedCallback callback)
    {
      this.itemType = trackedType;
      Telerik.Sitefinity.Data.CacheDependency.Subscribe(typeof (CacheDependencyHandler), trackedType, callback);
    }

    /// <summary>Gets the unique ID.</summary>
    /// <returns>
    /// The unique identifier for the <see cref="T:System.Web.Caching.CacheDependency" /> object.
    /// </returns>
    public override string GetUniqueID()
    {
      if (!this.itemId.IsNullOrEmpty())
        return this.UniquePrefix + ":" + this.itemId;
      return this.itemType != (Type) null ? this.UniquePrefix + ":" + this.itemType.FullName : Guid.NewGuid().ToString();
    }
  }
}
