// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SyncLock.ISyncLockSupport
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Linq;

namespace Telerik.Sitefinity.SyncLock
{
  internal interface ISyncLockSupport
  {
    /// <summary>
    /// Creates new instance of <see cref="T:Telerik.Sitefinity.SyncLock.Lock" />.
    /// </summary>
    /// <param name="lockId">The lock id.</param>
    /// <returns>New instance of <see cref="T:Telerik.Sitefinity.SyncLock.Lock" /></returns>
    Lock CreateLock(string lockId);

    /// <summary>
    /// Get a query for all <see cref="T:Telerik.Sitefinity.SyncLock.Lock" /> items.
    /// </summary>
    /// <returns>Queryable object for all <see cref="T:Telerik.Sitefinity.SyncLock.Lock" /> items</returns>
    IQueryable<Lock> GetLocks();

    /// <summary>
    /// Deletes the provided <see cref="T:Telerik.Sitefinity.SyncLock.Lock" />.
    /// </summary>
    /// <param name="obj">The <see cref="T:Telerik.Sitefinity.SyncLock.Lock" /> to delete.</param>
    void DeleteLock(Lock obj);
  }
}
