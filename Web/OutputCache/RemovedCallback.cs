// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.OutputCache.RemovedCallback
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Caching;
using System.Web.Caching;

namespace Telerik.Sitefinity.Web.OutputCache
{
  /// <summary>
  /// This class convert System.Web.Caching.CacheItemRemovedCallback into format of System.Runtime.Caching.CacheEntryRemovedCallback
  /// </summary>
  internal sealed class RemovedCallback
  {
    private CacheItemRemovedCallback callback;

    public RemovedCallback(CacheItemRemovedCallback callback) => this.callback = callback;

    public void CacheEntryRemovedCallback(CacheEntryRemovedArguments arguments)
    {
      string key = arguments.CacheItem.Key;
      object obj = arguments.CacheItem.Value;
      CacheItemRemovedReason reason;
      switch (arguments.RemovedReason)
      {
        case CacheEntryRemovedReason.Removed:
          reason = CacheItemRemovedReason.Removed;
          break;
        case CacheEntryRemovedReason.Expired:
          reason = CacheItemRemovedReason.Expired;
          break;
        case CacheEntryRemovedReason.Evicted:
          reason = CacheItemRemovedReason.Underused;
          break;
        case CacheEntryRemovedReason.ChangeMonitorChanged:
          reason = CacheItemRemovedReason.DependencyChanged;
          break;
        default:
          reason = CacheItemRemovedReason.Removed;
          break;
      }
      this.callback(key, obj, reason);
    }
  }
}
