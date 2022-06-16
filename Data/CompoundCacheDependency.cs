// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.CompoundCacheDependency
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching;

namespace Telerik.Sitefinity.Data
{
  /// <summary>
  /// A container of collection of instances of type <see cref="T:Telerik.Microsoft.Practices.EnterpriseLibrary.Caching.ICacheItemExpiration" />.
  /// </summary>
  public class CompoundCacheDependency : ICacheItemExpiration
  {
    private List<ICacheItemExpiration> cacheDependencies;

    /// <summary>Gets the cache dependencies.</summary>
    /// <value>The collection of instances of type <see cref="T:Telerik.Microsoft.Practices.EnterpriseLibrary.Caching.ICacheItemExpiration" />.</value>
    public virtual List<ICacheItemExpiration> CacheDependencies
    {
      get
      {
        if (this.cacheDependencies == null)
          this.cacheDependencies = new List<ICacheItemExpiration>();
        return this.cacheDependencies;
      }
    }

    /// <summary>Specifies if item has expired or not.</summary>
    /// <returns>
    /// Returns true if the item has expired, otherwise false.
    /// </returns>
    public bool HasExpired() => this.CacheDependencies.Any<ICacheItemExpiration>((Func<ICacheItemExpiration, bool>) (i => i.HasExpired()));

    /// <summary>
    /// Called to tell the expiration that the CacheItem to which this expiration belongs has been touched by the user
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
    }
  }
}
