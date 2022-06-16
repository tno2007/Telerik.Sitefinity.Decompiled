// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Scheduling.SchedulingManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.LoadBalancing;
using Telerik.Sitefinity.Scheduling.Configuration;
using Telerik.Sitefinity.Scheduling.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.SyncLock;

namespace Telerik.Sitefinity.Scheduling
{
  /// <summary>
  /// Manages the data layer for Sitefinity's scheduling syb-system
  /// </summary>
  public class SchedulingManager : 
    ManagerBase<SchedulingDataProvider>,
    ISyncLockSupportManager,
    IManager,
    IDisposable,
    IProviderResolver,
    ISyncLockSupport
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="!:PublishingManager" /> class.
    /// </summary>
    public SchedulingManager()
      : this((string) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:PublishingManager" /> class.
    /// </summary>
    /// <param name="providerName">Name of the provider.</param>
    public SchedulingManager(string providerName)
      : base(providerName)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:PublishingManager" /> class.
    /// </summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="transactionName">Name of the transaction.</param>
    public SchedulingManager(string providerName, string transactionName)
      : base(providerName, transactionName)
    {
    }

    /// <summary>Gets the default provider delegate.</summary>
    /// <value>The default provider delegate.</value>
    protected internal override GetDefaultProvider DefaultProviderDelegate => new GetDefaultProvider(this.GetDefaultProviderName);

    /// <summary>
    /// Retrieves the default provider name through the configuration
    /// </summary>
    /// <returns>Default provider name, as set by SchedulingConfig.DefaultProvider</returns>
    protected virtual string GetDefaultProviderName() => Config.Get<SchedulingConfig>().DefaultProvider;

    /// <summary>Get the module this manager is associated with</summary>
    public override string ModuleName => SchedulingModule.ModuleName;

    /// <summary>Gets the providers settings.</summary>
    /// <value>The providers settings.</value>
    protected internal override ConfigElementDictionary<string, DataProviderSettings> ProvidersSettings => this.GetDefaultProviderSettings();

    /// <summary>Gets default provider settings from configuration</summary>
    /// <returns>Default provider settings</returns>
    protected virtual ConfigElementDictionary<string, DataProviderSettings> GetDefaultProviderSettings() => Config.Get<SchedulingConfig>().ProviderSettings;

    /// <summary>
    /// Get an instance of the scheduling manager using the default provider
    /// </summary>
    /// <returns>Instance of scheduling manager</returns>
    public static SchedulingManager GetManager() => ManagerBase<SchedulingDataProvider>.GetManager<SchedulingManager>();

    /// <summary>
    /// Get an instance of the scheduling manager by explicitly specifying the required provider to use
    /// </summary>
    /// <param name="providerName">Name of the provider to use, or null/empty string to use the default provider.</param>
    /// <returns>Instance of the scheduling manager</returns>
    public static SchedulingManager GetManager(string providerName) => ManagerBase<SchedulingDataProvider>.GetManager<SchedulingManager>(providerName);

    /// <summary>Get publishing manager in named transaction</summary>
    /// <param name="providerName">Name of the provider</param>
    /// <param name="transactionName">Name of the transaction</param>
    /// <returns>Manager in named transaction. Don't use SaveChanges with it, use TransactionManager.CommitTransaction, instead.</returns>
    public static SchedulingManager GetManager(
      string providerName,
      string transactionName)
    {
      return ManagerBase<SchedulingDataProvider>.GetManager<SchedulingManager>(providerName, transactionName);
    }

    /// <summary>Gets the tasks from all providers.</summary>
    /// <param name="whereFilter">The where filter.</param>
    /// <param name="take">How many items to get.</param>
    /// <returns>List of scheduled tasks.</returns>
    public static IList<ScheduledTaskData> GetTasksFromAllProviders(
      Expression<Func<ScheduledTaskData, bool>> whereFilter,
      int take = 0)
    {
      return SchedulingManager.GetTasksFromAllProviders(whereFilter, string.Empty, take);
    }

    /// <summary>Gets the tasks from all providers.</summary>
    /// <param name="whereFilter">The where filter.</param>
    /// <param name="transactionName">Name of the transaction.</param>
    /// <param name="take">How many items to get.</param>
    /// <returns>List of scheduled tasks.</returns>
    public static IList<ScheduledTaskData> GetTasksFromAllProviders(
      Expression<Func<ScheduledTaskData, bool>> whereFilter,
      string transactionName,
      int take = 0)
    {
      List<ScheduledTaskData> fromAllProviders = new List<ScheduledTaskData>();
      foreach (DataProviderBase staticProvider in (Collection<SchedulingDataProvider>) SchedulingManager.GetManager().StaticProviders)
      {
        IQueryable<ScheduledTaskData> source = SchedulingManager.GetManager(staticProvider.Name, transactionName).GetTaskData().Where<ScheduledTaskData>(whereFilter);
        if (take > 0)
          source = source.Take<ScheduledTaskData>(take - fromAllProviders.Count);
        IQueryable<ScheduledTaskData> collection = (IQueryable<ScheduledTaskData>) source.OrderBy<ScheduledTaskData, DateTime>((Expression<Func<ScheduledTaskData, DateTime>>) (task => task.ExecuteTime));
        fromAllProviders.AddRange((IEnumerable<ScheduledTaskData>) collection);
        if (take > 0)
        {
          if (fromAllProviders.Count >= take)
            break;
        }
      }
      return (IList<ScheduledTaskData>) fromAllProviders;
    }

    internal static void DeleteAllTasks(
      Expression<Func<ScheduledTaskData, bool>> predicate,
      bool throwException = false)
    {
      string transactionName = "ScheduledTaskDelete_" + Guid.NewGuid().ToString();
      try
      {
        SchedulingManager.DeleteAllTasks(predicate, throwException, transactionName);
        TransactionManager.CommitTransaction(transactionName);
      }
      catch
      {
        TransactionManager.RollbackTransaction(transactionName);
      }
    }

    /// <summary>Deletes all tasks.</summary>
    /// <param name="whereFilter">The where filter.</param>
    /// <param name="throwException">if set to <c>true</c> [throw exception].</param>
    /// <param name="transactionName">Name of the transaction.</param>
    public static void DeleteAllTasks(
      Expression<Func<ScheduledTaskData, bool>> predicate,
      bool throwException,
      string transactionName)
    {
      try
      {
        foreach (DataProviderBase staticProvider in (Collection<SchedulingDataProvider>) SchedulingManager.GetManager().StaticProviders)
        {
          SchedulingManager manager = SchedulingManager.GetManager(staticProvider.Name, transactionName);
          foreach (ScheduledTaskData scheduledTaskData in (IEnumerable<ScheduledTaskData>) manager.GetTaskData().Where<ScheduledTaskData>(predicate))
            manager.DeleteItem((object) scheduledTaskData);
        }
      }
      catch (Exception ex)
      {
        if (throwException)
          throw ex;
      }
    }

    /// <summary>Gets the next task.</summary>
    /// <param name="transactionName">Name of the transaction.</param>
    /// <returns></returns>
    public static ScheduledTaskData GetNextTask(string transactionName) => SchedulingManager.GetNextTask(transactionName, false);

    /// <summary>Gets the next task.</summary>
    /// <param name="transactionName">Name of the transaction.</param>
    /// <param name="ignoreConcurrencyRestrictions">if set to <c>true</c> the ConcurrentTasksCount property is disregarded.</param>
    /// <returns></returns>
    public static ScheduledTaskData GetNextTask(
      string transactionName,
      bool ignoreConcurrencyRestrictions)
    {
      try
      {
        List<ScheduledTaskData> source = new List<ScheduledTaskData>();
        foreach (SchedulingDataProvider staticProvider in (Collection<SchedulingDataProvider>) SchedulingManager.GetManager().StaticProviders)
        {
          ScheduledTaskData nextTaskForProvider = SchedulingManager.GetNextTaskForProvider(transactionName, staticProvider, ignoreConcurrencyRestrictions: ignoreConcurrencyRestrictions);
          if (nextTaskForProvider != null)
            source.Add(nextTaskForProvider);
        }
        return source.OrderBy<ScheduledTaskData, DateTime>((Func<ScheduledTaskData, DateTime>) (item => item.ExecuteTime)).FirstOrDefault<ScheduledTaskData>();
      }
      catch (Exception ex)
      {
        return (ScheduledTaskData) null;
      }
    }

    private static ScheduledTaskData GetNextTaskForProvider(
      string transactionName,
      SchedulingDataProvider provider,
      int skip = 0,
      bool ignoreConcurrencyRestrictions = false)
    {
      SchedulingManager manager = SchedulingManager.GetManager(provider.Name, transactionName);
      IQueryable<ScheduledTaskData> source = manager.GetTaskData().Where<ScheduledTaskData>((Expression<Func<ScheduledTaskData, bool>>) (item => item.IsRunning == false));
      if (!ignoreConcurrencyRestrictions)
      {
        IQueryable<string> runningConcurrentKeys = manager.GetTaskData().Where<ScheduledTaskData>((Expression<Func<ScheduledTaskData, bool>>) (item => item.IsRunning == true && (int) item.Status == 1 && item.ConcurrentTasksKey != default (string))).Select<ScheduledTaskData, string>((Expression<Func<ScheduledTaskData, string>>) (item => item.ConcurrentTasksKey));
        source = source.Where<ScheduledTaskData>((Expression<Func<ScheduledTaskData, bool>>) (item => item.ConcurrentTasksKey == default (string) || !runningConcurrentKeys.Contains<string>(item.ConcurrentTasksKey)));
      }
      return source.OrderBy<ScheduledTaskData, DateTime>((Expression<Func<ScheduledTaskData, DateTime>>) (item => item.ExecuteTime)).Skip<ScheduledTaskData>(skip).FirstOrDefault<ScheduledTaskData>();
    }

    /// <summary>Gets the first task from all providers.</summary>
    /// <param name="whereFilter">The where filter.</param>
    /// <param name="transactionName">Name of the transaction.</param>
    /// <returns></returns>
    public static ScheduledTaskData GetFirstTaskFromAllProviders(
      Expression<Func<ScheduledTaskData, bool>> whereFilter,
      string transactionName)
    {
      foreach (DataProviderBase staticProvider in (Collection<SchedulingDataProvider>) SchedulingManager.GetManager().StaticProviders)
      {
        ScheduledTaskData fromAllProviders = SchedulingManager.GetManager(staticProvider.Name, transactionName).GetTaskData().Where<ScheduledTaskData>(whereFilter).OrderBy<ScheduledTaskData, DateTime>((Expression<Func<ScheduledTaskData, DateTime>>) (task => task.ExecuteTime)).FirstOrDefault<ScheduledTaskData>();
        if (fromAllProviders != null)
          return fromAllProviders;
      }
      return (ScheduledTaskData) null;
    }

    /// <summary>Create a new task data with random ID</summary>
    /// <returns>Newly created task data</returns>
    public ScheduledTaskData CreateTaskData() => this.Provider.CreateTaskData();

    /// <summary>Create a new task data with specific ID</summary>
    /// <param name="id">Primary key</param>
    /// <returns>Newly created task data</returns>
    public ScheduledTaskData CreateTaskData(Guid id) => this.Provider.CreateTaskData(id);

    /// <summary>Search task data by primary key</summary>
    /// <param name="id">Primary key</param>
    /// <returns>Task data with primary key equal to <paramref name="id" /></returns>
    /// <exception cref="T:Telerik.Sitefinity.SitefinityExceptions.ItemNotFoundException">
    /// When no task data is found by <paramref name="id" />
    /// </exception>
    public ScheduledTaskData GetTaskData(Guid id) => this.Provider.GetTaskData(id);

    /// <summary>
    /// Get a query of all task data items stored by the current provider
    /// </summary>
    /// <returns>All task data items stored by the current provider</returns>
    public IQueryable<ScheduledTaskData> GetTaskData() => this.Provider.GetTaskData();

    /// <summary>
    /// Remove a task data item from the current transaction that will be deleted when the transaction is commited
    /// </summary>
    /// <param name="task">Task data to delete</param>
    public void DeleteTaskData(ScheduledTaskData task) => this.Provider.DeleteTaskData(task);

    public void AddTask(ScheduledTask task)
    {
      ScheduledTaskData persistedData = !(task.Id == Guid.Empty) ? this.CreateTaskData(task.Id) : this.CreateTaskData();
      if (task.SiteId == Guid.Empty)
        task.SiteId = SystemManager.CurrentContext.CurrentSite.Id;
      CultureInfo culture = SystemManager.CurrentContext.Culture;
      if (task.Language == null && culture != null)
        task.Language = culture.Name;
      task.CopyToTaskData(persistedData);
      if (!(persistedData.Owner == Guid.Empty))
        return;
      persistedData.Owner = SecurityManager.GetCurrentUserId();
    }

    public override void SaveChanges()
    {
      base.SaveChanges();
      SchedulingManager.RescheduleNextRun();
    }

    Lock ISyncLockSupport.CreateLock(string lockId) => this.Provider.CreateLock(lockId);

    IQueryable<Lock> ISyncLockSupport.GetLocks() => this.Provider.GetLocks();

    void ISyncLockSupport.DeleteLock(Lock obj) => this.Provider.DeleteLock(obj);

    /// <summary>Reschedules the next run.</summary>
    public static void RescheduleNextRun()
    {
      SystemMessageDispatcher.SendSystemMessage(new SystemMessageBase()
      {
        Key = "SchedulingKey"
      });
      Scheduler.Instance.RescheduleNextRun();
    }
  }
}
