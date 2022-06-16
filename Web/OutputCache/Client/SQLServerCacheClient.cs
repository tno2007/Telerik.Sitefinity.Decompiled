// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.OutputCache.Client.SQLServerCacheClient
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using ServiceStack.Caching;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Security;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.Configuration;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web.OutputCache.Client
{
  internal class SQLServerCacheClient : ICacheClient, IDisposable
  {
    private bool disposedValue;
    [SecurityCritical]
    private readonly OrmLiteCacheClient client;

    public SQLServerCacheClient()
    {
      string connectionString = Config.Get<SystemConfig>().CacheSettings.CacheProviders.SqlServer.ConnectionString;
      if (string.IsNullOrEmpty(connectionString))
        connectionString = Config.Get<DataConfig>().ConnectionStrings["Sitefinity"].ConnectionString;
      this.client = new OrmLiteCacheClient();
      this.client.DbFactory = (IDbConnectionFactory) new OrmLiteConnectionFactory(connectionString, SqlServerDialect.Provider);
      this.client.InitSchema();
    }

    public bool Add<T>(string key, T value, DateTime expiresAt) => this.client.Add<T>(key, value, expiresAt);

    public T Get<T>(string key) => this.client.Get<T>(key);

    public bool Remove(string key) => this.client.Remove(key);

    public bool Set<T>(string key, T value, DateTime expiresAt) => this.client.Set<T>(key, value, expiresAt);

    protected virtual void Dispose(bool disposing)
    {
      if (this.disposedValue)
        return;
      if (disposing)
        this.client.Dispose();
      this.disposedValue = true;
    }

    public void Dispose() => this.Dispose(true);
  }
}
