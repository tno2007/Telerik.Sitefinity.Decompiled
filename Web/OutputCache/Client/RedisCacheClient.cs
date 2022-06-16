// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.OutputCache.Client.RedisCacheClient
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using ServiceStack.Redis;
using System;
using System.Security;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web.OutputCache.Client
{
  internal class RedisCacheClient : ICacheClient, IDisposable
  {
    private bool disposedValue;
    [SecurityCritical]
    private readonly RedisManagerPool redisManagerPool;

    public RedisCacheClient()
    {
      SystemConfig systemConfig = Config.Get<SystemConfig>();
      string connectionString = systemConfig.CacheSettings.CacheProviders.Redis.ConnectionString;
      if (string.IsNullOrEmpty(connectionString))
        connectionString = systemConfig.LoadBalancingConfig.RedisSettings.ConnectionString;
      this.redisManagerPool = new RedisManagerPool(connectionString);
    }

    public bool Add<T>(string key, T value, DateTime expiresAt) => this.redisManagerPool.GetCacheClient().Add<T>(key, value, expiresAt);

    public T Get<T>(string key) => this.redisManagerPool.GetCacheClient().Get<T>(key);

    public bool Remove(string key) => this.redisManagerPool.GetCacheClient().Remove(key);

    public bool Set<T>(string key, T value, DateTime expiresAt) => this.redisManagerPool.GetCacheClient().Set<T>(key, value, expiresAt);

    protected virtual void Dispose(bool disposing)
    {
      if (this.disposedValue)
        return;
      if (disposing)
        this.redisManagerPool.Dispose();
      this.disposedValue = true;
    }

    public void Dispose() => this.Dispose(true);
  }
}
