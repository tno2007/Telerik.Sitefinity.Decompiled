// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.NoCacheManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching;

namespace Telerik.Sitefinity.Abstractions
{
  /// <summary>
  /// This is the cache manager that is used, prior to the configuration of the actual cache managers.
  /// This is cache manager is used during the system install.
  /// </summary>
  public class NoCacheManager : ICacheManager
  {
    public void Add(string key, object value)
    {
    }

    public void Add(
      string key,
      object value,
      CacheItemPriority scavengingPriority,
      ICacheItemRefreshAction refreshAction,
      params ICacheItemExpiration[] expirations)
    {
    }

    public bool Contains(string key) => false;

    public int Count => 0;

    public void Flush()
    {
    }

    public object GetData(string key) => (object) null;

    public void Remove(string key)
    {
    }

    public object this[string key] => (object) null;
  }
}
