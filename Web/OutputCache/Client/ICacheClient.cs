// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.OutputCache.Client.ICacheClient
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Web.OutputCache.Client
{
  /// <summary>
  /// A common interface for cache providers implementation
  /// </summary>
  internal interface ICacheClient : IDisposable
  {
    /// <summary>
    /// Adds a new item into the cache at the specified cache key only if the cache is empty.
    /// </summary>
    /// <typeparam name="T">The item type</typeparam>
    /// <param name="key">The key used to reference the item.</param>
    /// <param name="value">The object to be inserted into the cache.</param>
    /// <param name="expiresAt">The expiration date of the object.</param>
    /// <returns>true if the item was successfully stored in the cache; false otherwise.</returns>
    bool Add<T>(string key, T value, DateTime expiresAt);

    /// <summary>Retrieves the specified item from the cache.</summary>
    /// <typeparam name="T">The item type</typeparam>
    /// <param name="key">The identifier for the item to retrieve.</param>
    /// <returns>The retrieved item, or null if the key was not found.</returns>
    T Get<T>(string key);

    /// <summary>Removes the specified item from the cache.</summary>
    /// <param name="key">The identifier for the item to delete.</param>
    /// <returns>true if the item was successfully removed from the cache; false otherwise.</returns>
    bool Remove(string key);

    /// <summary>
    /// Sets an item into the cache at the cache key specified regardless if it already exists or not.
    /// </summary>
    /// <typeparam name="T">The item type</typeparam>
    /// <param name="key">The key used to reference the item.</param>
    /// <param name="value">The object to be set into the cache.</param>
    /// <param name="expiresAt">The expiration date of the object.</param>
    /// <returns>true if the item was successfully stored in the cache; false otherwise.</returns>
    bool Set<T>(string key, T value, DateTime expiresAt);
  }
}
