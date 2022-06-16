// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.OutputCache.Client.AwsDynamoDBCacheClient
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Amazon;
using Amazon.DynamoDBv2;
using ServiceStack.Aws.DynamoDb;
using System;
using System.Security;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web.OutputCache.Client
{
  internal class AwsDynamoDBCacheClient : ICacheClient, IDisposable
  {
    private bool disposedValue;
    private readonly AmazonDynamoDBClient awsDb;
    [SecurityCritical]
    private readonly DynamoDbCacheClient client;

    public AwsDynamoDBCacheClient()
    {
      this.awsDb = new AmazonDynamoDBClient(Config.Get<SystemConfig>().CacheSettings.CacheProviders.AwsDynamoDB.AccessKey, Config.Get<SystemConfig>().CacheSettings.CacheProviders.AwsDynamoDB.SecretKey, RegionEndpoint.GetBySystemName(Config.Get<SystemConfig>().CacheSettings.CacheProviders.AwsDynamoDB.Region));
      this.client = new DynamoDbCacheClient((IPocoDynamo) new PocoDynamo((IAmazonDynamoDB) this.awsDb));
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
      {
        this.client.Dispose();
        this.awsDb.Dispose();
      }
      this.disposedValue = true;
    }

    public void Dispose() => this.Dispose(true);
  }
}
