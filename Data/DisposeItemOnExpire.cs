// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.DisposeItemOnExpire
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching;

namespace Telerik.Sitefinity.Data
{
  /// <summary>
  /// 
  /// </summary>
  public class DisposeItemOnExpire : ICacheItemRefreshAction
  {
    /// <summary>
    /// Called when an item expires from the cache. This method can be used to notify an application that
    /// the expiration occurred, cause the item to be refetched and refreshed from its original location, or
    /// perform any other application-specific action.
    /// </summary>
    /// <param name="removedKey">Key of item removed from cache. Will never be null.</param>
    /// <param name="expiredValue">Value from cache item that was just expired</param>
    /// <param name="removalReason">Reason the item was removed from the cache. See <see cref="T:Telerik.Microsoft.Practices.EnterpriseLibrary.Caching.CacheItemRemovedReason" /></param>
    /// <remarks>This method should catch and handle any exceptions thrown during its operation. No exceptions should leak
    /// out of it.</remarks>
    public virtual void Refresh(
      string removedKey,
      object expiredValue,
      CacheItemRemovedReason removalReason)
    {
      if (!(expiredValue is IDisposable disposable))
        return;
      disposable.Dispose();
    }
  }
}
