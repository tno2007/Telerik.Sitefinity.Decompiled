// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.ConfigCacheDependency
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.InteropServices;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching;
using Telerik.Sitefinity.Data;

namespace Telerik.Sitefinity.Configuration
{
  /// <summary>This class tracks a data item cache dependency.</summary>
  [ComVisible(false)]
  [Serializable]
  public class ConfigCacheDependency : ICacheItemExpiration
  {
    private bool expired;

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
    public void Initialize(CacheItem owningCacheItem) => CacheDependency.Subscribe(typeof (ConfigCacheDependencyHandler), (object) owningCacheItem, new ChangedCallback(this.Expire));

    private void Expire(ICacheDependencyHandler caller, Type type, string path)
    {
      this.expired = true;
      CacheDependency.Unsubscribe(typeof (ConfigCacheDependencyHandler), type, new ChangedCallback(this.Expire));
      CacheDependency.Unsubscribe(typeof (ConfigCacheDependencyHandler), (object) path, new ChangedCallback(this.Expire));
    }
  }
}
