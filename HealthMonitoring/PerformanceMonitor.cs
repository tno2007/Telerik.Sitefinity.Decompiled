// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.HealthMonitoring.PerformanceMonitor
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.HealthMonitoring.Configuration;
using Telerik.Sitefinity.Security;

namespace Telerik.Sitefinity.HealthMonitoring
{
  /// <summary>
  /// 
  /// </summary>
  public static class PerformanceMonitor
  {
    private static readonly PerformanceMonitoringMode mode;
    private static readonly IDictionary<string, IDictionary<string, PerformanceMonitor.Counter>> categories;

    /// <summary>
    /// Initializes the <see cref="T:Telerik.Sitefinity.HealthMonitoring.PerformanceMonitor" /> class.
    /// </summary>
    static PerformanceMonitor()
    {
      HealthMonitoringElement healthMonitoring = Config.SectionHandler.HealthMonitoring;
      PerformanceMonitor.mode = healthMonitoring.PerformanceMonitoringMode;
      if (PerformanceMonitor.mode == PerformanceMonitoringMode.Disabled)
        return;
      PerformanceMonitor.categories = (IDictionary<string, IDictionary<string, PerformanceMonitor.Counter>>) new Dictionary<string, IDictionary<string, PerformanceMonitor.Counter>>();
      foreach (CounterCategoryElement performanceCounter in (ConfigurationElementCollection) healthMonitoring.PerformanceCounters)
        PerformanceMonitor.CreateCounters(performanceCounter);
    }

    public static PerformanceMonitoringMode Mode => PerformanceMonitor.mode;

    public static long Begin() => PerformanceMonitor.mode != PerformanceMonitoringMode.Disabled ? Stopwatch.GetTimestamp() : 0L;

    public static void End(string category, string counter, long startTime) => PerformanceMonitor.End(category, counter, startTime, (Func<string>) null);

    public static void End(
      string category,
      string counter,
      long startTime,
      Func<string> limitationExceededMessage)
    {
      if (PerformanceMonitor.mode == PerformanceMonitoringMode.Disabled)
        return;
      long timestamp = Stopwatch.GetTimestamp();
      IDictionary<string, PerformanceMonitor.Counter> dictionary;
      PerformanceMonitor.Counter counter1;
      if (!PerformanceMonitor.categories.TryGetValue(category, out dictionary) || !dictionary.TryGetValue(counter, out counter1) || !counter1.Increment(timestamp - startTime))
        ;
    }

    private static void CreateCounters(CounterCategoryElement category)
    {
      lock (PerformanceMonitor.categories)
      {
        IDictionary<string, PerformanceMonitor.Counter> dictionary;
        if (PerformanceMonitor.categories.TryGetValue(category.CategoryName, out dictionary))
          return;
        dictionary = (IDictionary<string, PerformanceMonitor.Counter>) new Dictionary<string, PerformanceMonitor.Counter>();
        PerformanceMonitor.categories.Add(category.CategoryName, dictionary);
        foreach (CounterElement counter in (ConfigurationElementCollection) category.Counters)
          dictionary.Add(counter.CounterName, new PerformanceMonitor.Counter(category.CategoryName, counter.CounterName, counter.InstanceName, counter.InstanceLifetime));
      }
    }

    public static bool InstallCounters(bool reinstall)
    {
      HealthMonitoringElement healthMonitoring = Config.SectionHandler.HealthMonitoring;
      if (PerformanceMonitor.mode != PerformanceMonitoringMode.Windows && PerformanceMonitor.mode != PerformanceMonitoringMode.Both)
        return false;
      bool flag;
      if (healthMonitoring.Impersonate)
      {
        CredentialsElement credentials = healthMonitoring.Credentials;
        using (Impersonation impersonation = new Impersonation(credentials.Domain, credentials.UserName, credentials.Password))
        {
          if (!impersonation.Impersonate())
            throw new InvalidOperationException("Could not impersonate the configured user account.");
          flag = PerformanceMonitor.InstallCounters(healthMonitoring, reinstall);
        }
      }
      else
        flag = PerformanceMonitor.InstallCounters(healthMonitoring, reinstall);
      return flag;
    }

    private static bool InstallCounters(HealthMonitoringElement config, bool reinstall)
    {
      bool flag = false;
      foreach (CounterCategoryElement performanceCounter in (ConfigurationElementCollection) config.PerformanceCounters)
      {
        if (PerformanceMonitor.InstallCounters(performanceCounter, reinstall))
          flag = true;
      }
      return flag;
    }

    private static bool InstallCounters(CounterCategoryElement configuraton, bool reinstall)
    {
      if (PerformanceCounterCategory.Exists(configuraton.CategoryName))
      {
        if (!reinstall)
          return false;
        PerformanceCounterCategory.Delete(configuraton.CategoryName);
      }
      CounterCreationDataCollection creationDataCollection = new CounterCreationDataCollection();
      foreach (CounterElement counter in (ConfigurationElementCollection) configuraton.Counters)
      {
        creationDataCollection.Add(new CounterCreationData()
        {
          CounterName = string.Format("[{0}] # operations executed", (object) counter.CounterName),
          CounterHelp = "Total number of operations executed. " + counter.Description,
          CounterType = PerformanceCounterType.NumberOfItems32
        });
        creationDataCollection.Add(new CounterCreationData()
        {
          CounterName = string.Format("[{0}] # operations / sec", (object) counter.CounterName),
          CounterHelp = "Number of operations executed per second. " + counter.Description,
          CounterType = PerformanceCounterType.RateOfCountsPerSecond32
        });
        creationDataCollection.Add(new CounterCreationData()
        {
          CounterName = string.Format("[{0}] average time per operation", (object) counter.CounterName),
          CounterHelp = "Average duration per operation execution. " + counter.Description,
          CounterType = PerformanceCounterType.AverageTimer32
        });
        creationDataCollection.Add(new CounterCreationData()
        {
          CounterName = string.Format("[{0}] average time per operation base", (object) counter.CounterName),
          CounterHelp = "Average duration per operation execution base. " + counter.Description,
          CounterType = PerformanceCounterType.AverageBase
        });
      }
      string categoryName = configuraton.CategoryName;
      string description = configuraton.Description;
      PerformanceCounterCategoryType categoryType1 = configuraton.CategoryType;
      string categoryHelp = description;
      int categoryType2 = (int) categoryType1;
      CounterCreationDataCollection counterData = creationDataCollection;
      PerformanceCounterCategory.Create(categoryName, categoryHelp, (PerformanceCounterCategoryType) categoryType2, counterData);
      return true;
    }

    private class Counter
    {
      private readonly PerformanceCounter totalOperations;
      private readonly PerformanceCounter operationsPerSecond;
      private readonly PerformanceCounter averageDuration;
      private readonly PerformanceCounter averageDurationBase;
      public const string TotalOperationsName = "[{0}] # operations executed";
      public const string OperationsPerSecondName = "[{0}] # operations / sec";
      public const string AverageDurationName = "[{0}] average time per operation";
      public const string AverageDurationBaseName = "[{0}] average time per operation base";

      public Counter(
        string categoryName,
        string counterName,
        string instanceName,
        PerformanceCounterInstanceLifetime lifetime)
      {
        this.totalOperations = new PerformanceCounter();
        this.totalOperations.CategoryName = categoryName;
        this.totalOperations.CounterName = string.Format("[{0}] # operations executed", (object) counterName);
        this.totalOperations.MachineName = ".";
        this.totalOperations.ReadOnly = false;
        this.totalOperations.RawValue = 0L;
        this.totalOperations.InstanceName = instanceName;
        this.operationsPerSecond = new PerformanceCounter();
        this.operationsPerSecond.CategoryName = categoryName;
        this.operationsPerSecond.CounterName = string.Format("[{0}] # operations / sec", (object) counterName);
        this.operationsPerSecond.MachineName = ".";
        this.operationsPerSecond.ReadOnly = false;
        this.operationsPerSecond.RawValue = 0L;
        this.operationsPerSecond.InstanceName = instanceName;
        this.averageDuration = new PerformanceCounter();
        this.averageDuration.CategoryName = categoryName;
        this.averageDuration.CounterName = string.Format("[{0}] average time per operation", (object) counterName);
        this.averageDuration.MachineName = ".";
        this.averageDuration.ReadOnly = false;
        this.averageDuration.RawValue = 0L;
        this.averageDuration.InstanceName = instanceName;
        this.averageDurationBase = new PerformanceCounter();
        this.averageDurationBase.CategoryName = categoryName;
        this.averageDurationBase.CounterName = string.Format("[{0}] average time per operation base", (object) counterName);
        this.averageDurationBase.MachineName = ".";
        this.averageDurationBase.ReadOnly = false;
        this.averageDurationBase.RawValue = 0L;
        this.averageDurationBase.InstanceName = instanceName;
      }

      public bool Increment(long ticks)
      {
        this.totalOperations.Increment();
        this.operationsPerSecond.Increment();
        this.averageDuration.IncrementBy(ticks);
        this.averageDurationBase.Increment();
        return false;
      }
    }
  }
}
