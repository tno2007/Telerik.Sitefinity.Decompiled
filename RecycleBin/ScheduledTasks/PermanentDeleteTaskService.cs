// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.RecycleBin.ScheduledTasks.PermanentDeleteTaskService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.RecycleBin.Configuration;
using Telerik.Sitefinity.Scheduling;
using Telerik.Sitefinity.Scheduling.Model;
using Telerik.Sitefinity.SitefinityExceptions;

namespace Telerik.Sitefinity.RecycleBin.ScheduledTasks
{
  /// <summary>
  /// A service for managing <see cref="T:Telerik.Sitefinity.RecycleBin.ScheduledTasks.PermanentDeleteTask" /> objects which are used as scheduled tasks for permanent deletion
  /// of a <see cref="!:IDataItem" /> objects marked as deleted.
  /// </summary>
  public class PermanentDeleteTaskService : IPermanentDeleteTaskService
  {
    /// <summary>
    /// Creates a <see cref="T:Telerik.Sitefinity.RecycleBin.ScheduledTasks.PermanentDeleteTask" /> to be used as scheduled task for permanent deletion
    /// of a <see cref="!:IDataItem" /> object marked as deleted by a given <see cref="T:Telerik.Sitefinity.RecycleBin.IRecycleBinDataItem" />.
    /// </summary>
    /// <param name="recycleBinDataItem">The <see cref="T:Telerik.Sitefinity.RecycleBin.IRecycleBinDataItem" /> object for which we constructed a <see cref="T:Telerik.Sitefinity.RecycleBin.ScheduledTasks.PermanentDeleteTask" /></param>
    /// <param name="providerName">Name of the scheduled task provider.</param>
    /// <param name="transactionName">Name of the transaction.</param>
    /// <returns>
    /// The GUID of the <see cref="T:Telerik.Sitefinity.RecycleBin.ScheduledTasks.PermanentDeleteTask" /> that was constructed.
    /// </returns>
    public Guid CreateTask(
      IRecycleBinDataItem recycleBinDataItem,
      string providerName = null,
      string transactionName = null)
    {
      SchedulingManager manager = SchedulingManager.GetManager(providerName, transactionName);
      PermanentDeleteTask task = this.ConstructTask(recycleBinDataItem);
      manager.AddTask((ScheduledTask) task);
      manager.SaveChanges();
      return task.Id;
    }

    /// <summary>
    /// Creates <see cref="T:Telerik.Sitefinity.RecycleBin.ScheduledTasks.PermanentDeleteTask" /> objects to be used as scheduled task for permanent deletion
    /// for all <see cref="!:IDataItem" /> object marked as deleted by <see cref="T:Telerik.Sitefinity.RecycleBin.IRecycleBinDataItem" />.
    /// </summary>
    /// <param name="recycleBinItemProviderName">Name of the recycle bin item provider.</param>
    /// <param name="scheduledTaskProviderName">Name of the scheduled task provider.</param>
    /// <param name="transactionName">Name of the transaction.</param>
    public void CreateTasksForAllRecycleBinItems(
      string recycleBinItemProviderName = null,
      string scheduledTaskProviderName = null,
      string transactionName = null)
    {
      IQueryable<IRecycleBinDataItem> recycleBinItems = RecycleBinManagerFactory.GetManager(recycleBinItemProviderName, transactionName).GetRecycleBinItems();
      SchedulingManager manager = SchedulingManager.GetManager(scheduledTaskProviderName, transactionName);
      int num = 0;
      foreach (IRecycleBinDataItem recycleBinDataItem in (IEnumerable<IRecycleBinDataItem>) recycleBinItems)
      {
        PermanentDeleteTask task = this.ConstructTask(recycleBinDataItem);
        manager.AddTask((ScheduledTask) task);
        ++num;
        if (num >= 500)
        {
          manager.SaveChanges();
          num = 0;
        }
      }
      manager.SaveChanges();
    }

    /// <summary>
    /// Deletes the permanent delete task related to a given <see cref="T:Telerik.Sitefinity.RecycleBin.IRecycleBinDataItem" />.
    /// </summary>
    /// <param name="recycleBinDataItem">The <see cref="T:Telerik.Sitefinity.RecycleBin.IRecycleBinDataItem" /> to which the scheduled task is related.</param>
    /// <param name="providerName">Name of the scheduled task provider.</param>
    /// <param name="transactionName">Name of the transaction.</param>
    public void DeleteTask(
      IRecycleBinDataItem recycleBinDataItem,
      string providerName = null,
      string transactionName = null)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      PermanentDeleteTaskService.\u003C\u003Ec__DisplayClass2_0 cDisplayClass20 = new PermanentDeleteTaskService.\u003C\u003Ec__DisplayClass2_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass20.recycleBinDataItem = recycleBinDataItem;
      SchedulingManager manager = SchedulingManager.GetManager(providerName, transactionName);
      ParameterExpression parameterExpression;
      // ISSUE: reference to a compiler-generated field
      // ISSUE: method reference
      // ISSUE: reference to a compiler-generated field
      manager.DeleteTaskData(this.GetTaskData(manager).SingleOrDefault<ScheduledTaskData>(Expression.Lambda<Func<ScheduledTaskData, bool>>((Expression) Expression.Equal(std.Key, (Expression) Expression.Call(cDisplayClass20.recycleBinDataItem.Id, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.ToString)), Array.Empty<Expression>())), parameterExpression)) ?? throw new ItemNotFoundException(string.Format("Scheduled task data was not found for IRecycleBinDataItem with id: {0}", (object) cDisplayClass20.recycleBinDataItem.Id)));
      manager.SaveChanges();
    }

    /// <summary>
    /// Deletes all the permanent delete tasks related to <see cref="T:Telerik.Sitefinity.RecycleBin.IRecycleBinDataItem" /> objects.
    /// </summary>
    /// <param name="providerName">Name of the scheduled task provider.</param>
    /// <param name="transactionName">Name of the transaction.</param>
    public void DeleteAllTasks(string providerName = null, string transactionName = null)
    {
      SchedulingManager manager = SchedulingManager.GetManager(providerName, transactionName);
      IQueryable<ScheduledTaskData> taskData = this.GetTaskData(manager);
      int num = 0;
      foreach (ScheduledTaskData task in (IEnumerable<ScheduledTaskData>) taskData)
      {
        manager.DeleteTaskData(task);
        ++num;
        if (num >= 500)
        {
          manager.SaveChanges();
          num = 0;
        }
      }
      manager.SaveChanges();
    }

    /// <summary>
    /// Constructs a <see cref="T:Telerik.Sitefinity.RecycleBin.ScheduledTasks.PermanentDeleteTask" /> to be used as scheduled task for permanent deletion
    /// of a <see cref="!:IDataItem" /> object marked as deleted by a given <see cref="T:Telerik.Sitefinity.RecycleBin.IRecycleBinDataItem" />.
    /// </summary>
    /// <param name="recycleBinDataItem">The <see cref="T:Telerik.Sitefinity.RecycleBin.IRecycleBinDataItem" /> object for which we constructed a <see cref="T:Telerik.Sitefinity.RecycleBin.ScheduledTasks.PermanentDeleteTask" /></param>
    /// <returns>The <see cref="T:Telerik.Sitefinity.RecycleBin.ScheduledTasks.PermanentDeleteTask" /> that was constructed.</returns>
    internal PermanentDeleteTask ConstructTask(
      IRecycleBinDataItem recycleBinDataItem)
    {
      PermanentDeleteTask permanentDeleteTask = ObjectFactory.Container.Resolve<PermanentDeleteTask>((ResolverOverride) new ParameterOverride(nameof (recycleBinDataItem), (object) recycleBinDataItem));
      permanentDeleteTask.ExecuteTime = (recycleBinDataItem.DateCreated != new DateTime() ? recycleBinDataItem.DateCreated : DateTime.UtcNow).Add(this.GetRecycleBinConfig().RetentionPeriod);
      return permanentDeleteTask;
    }

    internal RecycleBinConfigBase GetRecycleBinConfig() => Config.Get<RecycleBinConfigBase>("RecycleBinConfig");

    private IQueryable<ScheduledTaskData> GetTaskData(
      SchedulingManager schedulingManager)
    {
      string taskName = this.GetTaskName();
      return schedulingManager.GetTaskData().Where<ScheduledTaskData>((Expression<Func<ScheduledTaskData, bool>>) (std => std.TaskName == taskName));
    }

    private string GetTaskName() => ObjectFactory.GetArgsForType(typeof (PermanentDeleteTask)).SingleOrDefault<RegisterEventArgs>((Func<RegisterEventArgs, bool>) (arg => arg.Name == null)).TypeTo.FullName;
  }
}
