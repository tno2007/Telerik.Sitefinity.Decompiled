// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.OutputCache.CacheProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Diagnostics.CodeAnalysis;

namespace Telerik.Sitefinity.Web.OutputCache
{
  /// <summary>Cache provider enumerator</summary>
  [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Reviewed")]
  public enum CacheProvider
  {
    /// <summary>In memory cache provider</summary>
    InMemory = 0,
    /// <summary>SQL server cache provider</summary>
    SQLServer = 1,
    /// <summary>Redis cache provider</summary>
    Redis = 2,
    /// <summary>$Memcached$ cache provider</summary>
    Memcached = 3,
    /// <summary>AWS Dynamo DB cache provider</summary>
    AwsDynamoDB = 4,
    /// <summary>Cache provider registered in the object factory</summary>
    Custom = 100, // 0x00000064
  }
}
