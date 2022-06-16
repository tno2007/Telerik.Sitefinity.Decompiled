// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.UserActivityBuffer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using Telerik.OpenAccess.Exceptions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;

namespace Telerik.Sitefinity.Security
{
  internal class UserActivityBuffer : IDisposable
  {
    private Timer internalTimer;
    private static UserActivityBuffer instance = (UserActivityBuffer) null;
    private static readonly object SingletonLock = new object();
    private ConcurrentDictionary<string, DateTime> userActivities = new ConcurrentDictionary<string, DateTime>();
    private double interval = 60000.0;
    private ReaderWriterLockWrapper userActivitiesRWLock = new ReaderWriterLockWrapper();
    protected static readonly char ProviderObjectIdDelimeter = '|';

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.UserActivityBuffer" /> class.
    /// </summary>
    internal UserActivityBuffer()
    {
      this.internalTimer = new Timer();
      this.internalTimer.Interval = this.TimerInterval;
      this.internalTimer.AutoReset = false;
      this.internalTimer.Elapsed += new ElapsedEventHandler(this.InternalTimer_Elapsed);
      UserActivityBuffer.IsInTransaction = false;
    }

    public static UserActivityBuffer Instance
    {
      get
      {
        if (UserActivityBuffer.instance == null)
        {
          lock (UserActivityBuffer.SingletonLock)
          {
            if (UserActivityBuffer.instance == null)
              UserActivityBuffer.instance = new UserActivityBuffer();
          }
        }
        return UserActivityBuffer.instance;
      }
    }

    public double TimerInterval
    {
      get => this.interval;
      set
      {
        this.interval = value;
        if (this.internalTimer == null)
          return;
        this.internalTimer.Interval = this.TimerInterval;
      }
    }

    private static bool IsInTransaction { get; set; }

    /// <summary>
    /// Releases unmanaged and - optionally - managed resources
    /// </summary>
    public void Dispose() => this.internalTimer.Dispose();

    public void AddUserActivity(string providerName, Guid userId, DateTime lastActivityDate)
    {
      string userProviderAndId = UserActivityBuffer.ConcatProviderAndId(providerName, userId);
      int updateTries = 100;
      if (!this.userActivitiesRWLock.EnterForRead((Action) (() =>
      {
        while (updateTries > 0)
        {
          while (updateTries > 0)
          {
            if (this.userActivities.TryUpdate(userProviderAndId, lastActivityDate, this.userActivities.GetOrAdd(userProviderAndId, lastActivityDate)))
              updateTries = 0;
            else
              --updateTries;
          }
        }
      }), 60000) || UserActivityBuffer.IsInTransaction || this.internalTimer.Enabled)
        return;
      this.internalTimer.Start();
    }

    /// <summary>Concatenates the Provider name and User Id.</summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="userId">The user id.</param>
    /// <returns>Concatenated string from Provider name and User id</returns>
    internal static string ConcatProviderAndId(string providerName, Guid userId)
    {
      if (providerName.IsNullOrWhitespace())
        throw new ArgumentException("providerName can not be null or white space");
      return providerName + (object) UserActivityBuffer.ProviderObjectIdDelimeter + userId.ToString();
    }

    /// <summary>Splits the Provider name and User Id.</summary>
    /// <param name="key">The string to be split.</param>
    /// <param name="providerName">Provider name.</param>
    /// <param name="userId">User Id.</param>
    internal static void SplitProviderAndId(string key, out string providerName, out Guid userId)
    {
      string[] source = key.Split(UserActivityBuffer.ProviderObjectIdDelimeter);
      string input = ((IEnumerable<string>) source).Count<string>() == 2 ? source[1] : throw new ArgumentException("Provider name and User Id must be concatenated in the key");
      providerName = source[0];
      ref Guid local = ref userId;
      if (!Guid.TryParse(input, out local))
        throw new ArgumentException("Guid for User Id is not parsable");
    }

    protected void InternalTimer_Elapsed(object sender, ElapsedEventArgs e)
    {
      try
      {
        UserActivityBuffer.IsInTransaction = true;
        this.internalTimer.Stop();
        ConcurrentDictionary<string, DateTime> userActivitiesInternal = (ConcurrentDictionary<string, DateTime>) null;
        if (!this.userActivitiesRWLock.EnterForWrite((Action) (() =>
        {
          userActivitiesInternal = new ConcurrentDictionary<string, DateTime>((IEnumerable<KeyValuePair<string, DateTime>>) this.userActivities);
          this.userActivities = new ConcurrentDictionary<string, DateTime>();
        }), 300000))
        {
          UserActivityBuffer.IsInTransaction = false;
          if (this.internalTimer.Enabled)
            return;
          this.internalTimer.Start();
        }
        else
          this.FlushInDatabase((IDictionary<string, DateTime>) userActivitiesInternal);
      }
      finally
      {
        UserActivityBuffer.IsInTransaction = false;
        if (this.userActivities.Count > 0)
          this.internalTimer.Start();
      }
    }

    protected virtual void FlushInDatabase(
      IDictionary<string, DateTime> userActivitiesInternal)
    {
      foreach (KeyValuePair<string, DateTime> keyValuePair in (IEnumerable<KeyValuePair<string, DateTime>>) userActivitiesInternal)
      {
        string transactionName = "userActivitiesBuffer";
        string providerName;
        Guid userId;
        try
        {
          UserActivityBuffer.SplitProviderAndId(keyValuePair.Key, out providerName, out userId);
        }
        catch (ArgumentException ex)
        {
          continue;
        }
        try
        {
          int num = 3;
          while (num > 0)
          {
            try
            {
              UserActivityManager manager = UserActivityManager.GetManager("OpenAccessUserActivityProvider", transactionName);
              using (new ElevatedModeRegion((IManager) manager))
              {
                using (new DataSyncModeRegion((IManager) manager))
                {
                  manager.GetUserActivity(userId, providerName).LastActivityDate = keyValuePair.Value;
                  TransactionManager.CommitTransaction(transactionName);
                  num = 0;
                }
              }
            }
            catch (OptimisticVerificationException ex)
            {
              TransactionManager.RollbackTransaction(transactionName);
              --num;
            }
          }
        }
        catch (Exception ex)
        {
          if (Telerik.Sitefinity.Abstractions.Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
            throw;
        }
        finally
        {
          TransactionManager.DisposeTransaction(transactionName);
        }
      }
    }
  }
}
