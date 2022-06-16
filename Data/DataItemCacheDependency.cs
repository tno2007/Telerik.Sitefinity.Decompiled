// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.DataItemCacheDependency
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.InteropServices;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Data
{
  /// <summary>This class tracks a data item cache dependency.</summary>
  [ComVisible(false)]
  [Serializable]
  public class DataItemCacheDependency : ICacheItemExpiration
  {
    private string itemKey;
    private Type itemType;
    private bool initialized;
    private bool expired;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.DataItemCacheDependency" /> class.
    /// </summary>
    public DataItemCacheDependency() => this.Expire = new ChangedCallback(this.ExpireMethod);

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.DataItemCacheDependency" /> class.
    /// </summary>
    /// <param name="trackedItem">The tracked item.</param>
    public DataItemCacheDependency(IDataItem trackedItem)
    {
      if (trackedItem == null)
        throw new ArgumentNullException(nameof (trackedItem));
      this.Expire = new ChangedCallback(this.ExpireMethod);
      this.InternalSubscribe(trackedItem);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.DataItemCacheDependency" /> class.
    /// </summary>
    /// <param name="trackedItemType">Type of the tracked item.</param>
    /// <param name="trackedItemId">The ID of tracked item.</param>
    public DataItemCacheDependency(Type trackedItemType, Guid trackedItemId)
    {
      if (trackedItemType == (Type) null)
        throw new ArgumentNullException(nameof (trackedItemType));
      this.Expire = new ChangedCallback(this.ExpireMethod);
      string key = trackedItemId == Guid.Empty ? (string) null : trackedItemId.ToString();
      this.InternalSubscribe(trackedItemType, key);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.DataItemCacheDependency" /> class.
    /// </summary>
    /// <param name="trackedItemType">Type of the tracked item.</param>
    /// <param name="key">The primary key of the tracked item.</param>
    public DataItemCacheDependency(Type trackedItemType, string key)
    {
      if (trackedItemType == (Type) null)
        throw new ArgumentNullException(nameof (trackedItemType));
      this.Expire = new ChangedCallback(this.ExpireMethod);
      this.InternalSubscribe(trackedItemType, key);
    }

    internal Type ItemType => this.itemType;

    internal string ItemKey => this.itemKey;

    private void InternalSubscribe(Type type, string key)
    {
      this.initialized = true;
      this.itemKey = key;
      this.itemType = type;
      CacheDependency.Subscribe(type, key, this.Expire);
    }

    private void InternalSubscribe(IDataItem trackedItem)
    {
      this.initialized = true;
      this.itemType = trackedItem.GetType();
      this.itemKey = trackedItem.Id.ToString();
      CacheDependency.Subscribe((object) trackedItem, this.Expire);
    }

    /// <summary>Specifies if item has expired or not.</summary>
    /// <returns>
    /// Returns true if the item has expired, otherwise false.
    /// </returns>
    public bool HasExpired() => this.expired;

    /// <summary>
    /// Called to tell the expiration that the CacheItem to which this expiration belongs has been touched by the user.
    /// </summary>
    public void Notify()
    {
    }

    /// <summary>
    /// Called to give the instance the opportunity to initialize itself from information contained in the CacheItem.
    /// </summary>
    /// <param name="owningCacheItem">CacheItem that owns this expiration object</param>
    public void Initialize(CacheItem owningCacheItem)
    {
      if (this.initialized)
        return;
      if (!(owningCacheItem.Value is IDataItem))
        throw new InvalidOperationException("The cached item does not implement IDataItem interface.");
      this.InternalSubscribe((IDataItem) owningCacheItem.Value);
    }

    public ChangedCallback Expire { get; private set; }

    private void ExpireMethod(ICacheDependencyHandler caller, Type itemType, string itemKey)
    {
      this.expired = true;
      CacheDependency.Unsubscribe(caller.GetType(), this.itemType, this.itemKey, this.Expire);
    }
  }
}
