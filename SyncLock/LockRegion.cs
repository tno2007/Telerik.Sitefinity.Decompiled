// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SyncLock.LockRegion`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using Telerik.Sitefinity.Data;

namespace Telerik.Sitefinity.SyncLock
{
  internal class LockRegion<TManager> : IDisposable where TManager : ISyncLockSupportManager
  {
    private string lockId;
    private Guid lockerId;
    private TimeSpan activityTimeout;
    private bool deleteOnDispose;
    private DateTime lastActivityTime;
    private string transactionName = "lockRegionTransaction";

    private TManager GetManager() => (TManager) ManagerBase.GetManagerInTransaction(typeof (TManager), string.Empty, this.transactionName);

    public LockRegion(string lockId, TimeSpan activityTimeout, bool deleteOnDispose = false)
    {
      this.lockId = lockId;
      this.activityTimeout = activityTimeout;
      this.lockerId = Guid.NewGuid();
      this.deleteOnDispose = deleteOnDispose;
    }

    public void Update()
    {
      lock (this)
      {
        DateTime now = DateTime.UtcNow;
        if (!(now - this.lastActivityTime > new TimeSpan(this.activityTimeout.Ticks / 2L)) || !this.UpdateLock((Action<Lock>) (x => x.LastModified = now)))
          return;
        this.lastActivityTime = now;
      }
    }

    public void Dispose()
    {
      if (this.deleteOnDispose)
        this.DeleteLock();
      else
        this.Release();
    }

    internal void WaitToAcquire()
    {
      while (!this.TryAcquire())
        Thread.Sleep(1000);
    }

    internal bool TryAcquire()
    {
      using (TManager manager = this.GetManager())
      {
        Lock @lock = manager.GetLocks().FirstOrDefault<Lock>((Expression<Func<Lock, bool>>) (x => x.Id == this.lockId));
        if (@lock == null)
        {
          @lock = manager.CreateLock(this.lockId);
          @lock.LastModified = DateTime.UtcNow;
        }
        TimeSpan timeSpan = DateTime.UtcNow - @lock.LastModified;
        if (@lock.Locker == Guid.Empty || timeSpan > this.activityTimeout)
        {
          @lock.Locker = this.lockerId;
          @lock.LastModified = DateTime.UtcNow;
        }
        try
        {
          if (@lock.Locker == this.lockerId)
          {
            TransactionManager.CommitTransaction(this.transactionName);
            this.lastActivityTime = DateTime.UtcNow;
            return true;
          }
        }
        catch
        {
          TransactionManager.RollbackTransaction(this.transactionName);
        }
      }
      TransactionManager.DisposeTransaction(this.transactionName);
      return false;
    }

    private bool Release() => this.UpdateLock((Action<Lock>) (x => x.Locker = Guid.Empty));

    private bool UpdateLock(Action<Lock> action)
    {
      bool flag = false;
      using (TManager manager = this.GetManager())
      {
        Lock @lock = manager.GetLocks().FirstOrDefault<Lock>((Expression<Func<Lock, bool>>) (x => x.Id == this.lockId));
        if (@lock != null)
        {
          if (@lock.Locker == this.lockerId)
          {
            try
            {
              action(@lock);
              TransactionManager.CommitTransaction(this.transactionName);
              flag = true;
            }
            catch
            {
              TransactionManager.RollbackTransaction(this.transactionName);
            }
          }
        }
      }
      TransactionManager.DisposeTransaction(this.transactionName);
      return flag;
    }

    private void DeleteLock()
    {
      using (TManager manager = this.GetManager())
      {
        Lock @lock = manager.GetLocks().FirstOrDefault<Lock>((Expression<Func<Lock, bool>>) (x => x.Id == this.lockId));
        if (@lock != null)
        {
          if (@lock.Locker == this.lockerId)
          {
            try
            {
              manager.DeleteLock(@lock);
              TransactionManager.CommitTransaction(this.transactionName);
            }
            catch
            {
              TransactionManager.RollbackTransaction(this.transactionName);
            }
          }
        }
      }
      TransactionManager.DisposeTransaction(this.transactionName);
    }
  }
}
