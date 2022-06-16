// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.TemporaryStorage.ITemporaryStorage
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Abstractions.TemporaryStorage
{
  /// <summary>
  /// In memory storage mechanism that is safe to use in load balancing hosting scenarios.
  /// </summary>
  public interface ITemporaryStorage
  {
    /// <summary>Adds or updates the given key with the given value.</summary>
    /// <param name="key">The key.</param>
    /// <param name="data">The value.</param>
    /// <param name="expiresAtUtc">The expiration time.</param>
    void AddOrUpdate(string key, string data, DateTime expiresAtUtc);

    /// <summary>Removes the value with the given key from cache.</summary>
    /// <param name="key">The key for which to remove the value.</param>
    void Remove(string key);

    /// <summary>Returns the value for the given key.</summary>
    /// <param name="key">The key for which to return the value.</param>
    /// <returns>The value for the given key or null if the key is not found.</returns>
    string Get(string key);
  }
}
