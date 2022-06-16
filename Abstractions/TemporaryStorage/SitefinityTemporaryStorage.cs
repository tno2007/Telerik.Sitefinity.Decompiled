// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.TemporaryStorage.SitefinityTemporaryStorage
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using Telerik.Sitefinity.Abstractions.TemporaryStorage.CacheMessagingService.Utils;
using Telerik.Sitefinity.Abstractions.TemporaryStorage.Models;
using Telerik.Sitefinity.LoadBalancing;

namespace Telerik.Sitefinity.Abstractions.TemporaryStorage
{
  /// <inheritdoc />
  /// 
  ///             The implementation is based on the messaging infrastructure for NLB and internal memomory cache
  internal class SitefinityTemporaryStorage : ITemporaryStorage
  {
    private const string InvalidKeyMessage = "Invalid key. Must not be null or whitespace.";
    private MemoryCache store = new MemoryCache("SitefinityInternalTemporaryStore");
    private MessageConverter converter = new MessageConverter();

    /// <inheritdoc />
    public void AddOrUpdate(string key, string data, DateTime expiresAtUtc)
    {
      this.AddOrUpdateInternalStoreEntry(key, data, expiresAtUtc);
      this.SendSystemMessage(new SyncMessage(key, data, SyncOperation.Add, expiresAtUtc));
    }

    /// <inheritdoc />
    public void Remove(string key)
    {
      this.RemoveInternalStoreEntry(key);
      this.SendSystemMessage(new SyncMessage(key, string.Empty, SyncOperation.Remove, new DateTime()));
    }

    /// <inheritdoc />
    public string Get(string key) => !string.IsNullOrWhiteSpace(key) ? this.store.Get(key, (string) null) as string : throw new ArgumentException("Invalid key. Must not be null or whitespace.");

    /// <summary>
    /// Removes all values with keys starting with the given keyword from cache.
    /// </summary>
    /// <param name="keyword">The keyword.</param>
    public void RemoveAllStartingWith(string keyword)
    {
      foreach (KeyValuePair<string, object> keyValuePair in this.store.Where<KeyValuePair<string, object>>((Func<KeyValuePair<string, object>, bool>) (x => x.Key.StartsWith(keyword))))
        this.Remove(keyValuePair.Key);
    }

    public void ProcessSystemMessage(SystemMessageBase systemMessage)
    {
      SyncMessage syncMessage = this.converter.Convert(systemMessage);
      switch (syncMessage.Operation)
      {
        case SyncOperation.Add:
          this.AddOrUpdateInternalStoreEntry(syncMessage.Key, syncMessage.Data, syncMessage.ExpiresAtUtc);
          break;
        case SyncOperation.Remove:
          this.RemoveInternalStoreEntry(syncMessage.Key);
          break;
      }
    }

    private void SendSystemMessage(SyncMessage cacheMsg) => SystemMessageDispatcher.SendSystemMessage(this.converter.Convert(cacheMsg));

    private void AddOrUpdateInternalStoreEntry(string key, string data, DateTime expiresAtUtc)
    {
      if (expiresAtUtc.Kind != DateTimeKind.Utc)
        throw new ArgumentException("The expiration datetime value must be of UTC kind.");
      if (string.IsNullOrWhiteSpace(key))
        throw new ArgumentException("Invalid key. Must not be null or whitespace.");
      if (data == null)
        throw new ArgumentNullException(nameof (data));
      this.store.Set(key, (object) data, (DateTimeOffset) expiresAtUtc, (string) null);
    }

    private void RemoveInternalStoreEntry(string key)
    {
      if (string.IsNullOrWhiteSpace(key))
        throw new ArgumentException("Invalid key. Must not be null or whitespace.");
      this.store.Remove(key, (string) null);
    }
  }
}
