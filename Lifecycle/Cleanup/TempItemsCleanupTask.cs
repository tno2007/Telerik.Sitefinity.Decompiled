// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Lifecycle.Cleanup.TempItemsCleanupTask
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.Lifecycle.Cleanup.Configuration;
using Telerik.Sitefinity.Scheduling;
using Telerik.Sitefinity.Scheduling.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities;

namespace Telerik.Sitefinity.Lifecycle.Cleanup
{
  internal class TempItemsCleanupTask : ScheduledTask
  {
    private const int BatchSize = 100;

    static TempItemsCleanupTask() => CacheDependency.Subscribe(typeof (SystemConfig), new ChangedCallback(TempItemsCleanupTask.ConfigUpdated));

    public override string TaskName => TempItemsCleanupTask.GetTaskName();

    public override void ExecuteTask()
    {
      this.ProcessStaticModules();
      this.ProcessDynamicModules();
    }

    internal static void RemoveScheduledTasks()
    {
      SchedulingManager manager = SchedulingManager.GetManager();
      ParameterExpression parameterExpression;
      // ISSUE: method reference
      foreach (ScheduledTaskData tasksFromAllProvider in (IEnumerable<ScheduledTaskData>) SchedulingManager.GetTasksFromAllProviders(Expression.Lambda<Func<ScheduledTaskData, bool>>((Expression) Expression.Equal(t.TaskName, (Expression) Expression.Call((Expression) null, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (TempItemsCleanupTask.GetTaskName)), Array.Empty<Expression>())), parameterExpression)))
        manager.DeleteItem((object) tasksFromAllProvider);
      manager.SaveChanges();
    }

    private static void ConfigUpdated(
      ICacheDependencyHandler caller,
      Type trackedItemType,
      string trackedItemKey)
    {
      TempItemsCleanupTask.Schedule();
    }

    private void ProcessStaticModules()
    {
      foreach (IModule module in (IEnumerable<IModule>) SystemManager.ApplicationModules.Values)
      {
        if (module.Managers != null && !(module is ModuleBuilderModule))
        {
          foreach (Type manager1 in module.Managers)
          {
            IManager manager2 = ManagerBase.GetManager(manager1);
            if (manager2 is ILifecycleManager)
            {
              foreach (string providerName in manager2.GetProviderNames(ProviderBindingOptions.NoFilter))
              {
                DataProviderBase provider = ManagerBase.GetManager(manager1, providerName).Provider;
                Type[] knownTypes = provider.GetKnownTypes();
                this.RemoveTempItems(provider, (IEnumerable<Type>) knownTypes);
              }
            }
          }
        }
      }
    }

    private void ProcessDynamicModules()
    {
      foreach (DynamicAppModule dynamicModule in SystemManager.GetDynamicModules())
      {
        foreach (string providerName in DynamicModuleManager.GetManager().GetProviderNames(ProviderBindingOptions.NoFilter))
          this.RemoveTempItems((DataProviderBase) DynamicModuleManager.GetManager(providerName).Provider, dynamicModule.GetTypes().Select<DynamicTypeInfo, Type>((Func<DynamicTypeInfo, Type>) (t => t.Type)));
      }
    }

    private void RemoveTempItems(DataProviderBase provider, IEnumerable<Type> types)
    {
      int tempItemsMaxAge = Config.Get<SystemConfig>().LifecycleConfig.TempItemsCleanup.TempItemsMaxAge;
      foreach (Type type in types)
      {
        if (typeof (ILifecycleDataItem).IsAssignableFrom(type))
        {
          DateTime date = DateTime.UtcNow.AddDays((double) (tempItemsMaxAge * -1));
          IQueryable<ILifecycleDataItem> items = provider.GetItems(type, (string) null, (string) null, 0, 0) as IQueryable<ILifecycleDataItem>;
          Expression<Func<ILifecycleDataItem, bool>> predicate = (Expression<Func<ILifecycleDataItem, bool>>) (i => ((int) i.Status == 1 || (int) i.Status == 8) && i.Owner == Guid.Empty && i.LastModified < date);
          foreach (ILifecycleDataItem[] lifecycleDataItemArray in items.Where<ILifecycleDataItem>(predicate).OnBatchesOf<ILifecycleDataItem>(100))
          {
            foreach (ILifecycleDataItem lifecycleDataItem in lifecycleDataItemArray)
              provider.DeleteItem((object) lifecycleDataItem);
            provider.CommitTransaction();
          }
        }
      }
    }

    internal static ScheduledTask NewInstance()
    {
      TempItemsCleanupElement tempItemsCleanup = Config.Get<SystemConfig>().LifecycleConfig.TempItemsCleanup;
      if (!tempItemsCleanup.EnableTempItemsCleanup)
        return (ScheduledTask) null;
      TempItemsCleanupTask itemsCleanupTask = new TempItemsCleanupTask();
      itemsCleanupTask.Id = Guid.NewGuid();
      itemsCleanupTask.ScheduleSpecType = "crontab";
      itemsCleanupTask.ScheduleSpec = tempItemsCleanup.AutoCleanupCronSpec;
      itemsCleanupTask.IsSystem = true;
      return (ScheduledTask) itemsCleanupTask;
    }

    internal static void Schedule()
    {
      TempItemsCleanupTask.RemoveScheduledTasks();
      ScheduledTask task = TempItemsCleanupTask.NewInstance();
      if (task == null)
        return;
      Scheduler.Instance.TryToScheduleNextTaskRun(task);
    }

    internal static string GetTaskName() => typeof (TempItemsCleanupTask).FullName;
  }
}
