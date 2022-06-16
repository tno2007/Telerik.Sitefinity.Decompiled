// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.HealthMonitoring.Configuration.HealthMonitoringElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;

namespace Telerik.Sitefinity.HealthMonitoring.Configuration
{
  public class HealthMonitoringElement : ConfigurationElement
  {
    [ConfigurationProperty("performanceMonitoringMode", DefaultValue = PerformanceMonitoringMode.Disabled)]
    public PerformanceMonitoringMode PerformanceMonitoringMode
    {
      get => (PerformanceMonitoringMode) this["performanceMonitoringMode"];
      set => this["performanceMonitoringMode"] = (object) value;
    }

    [ConfigurationProperty("impersonate", DefaultValue = false)]
    public bool Impersonate
    {
      get => (bool) this["impersonate"];
      set => this["impersonate"] = (object) value;
    }

    [ConfigurationProperty("credentials")]
    public CredentialsElement Credentials
    {
      get => (CredentialsElement) this["credentials"];
      set => this["credentials"] = (object) value;
    }

    [ConfigurationProperty("performanceCounters")]
    public CounterCategoryElementCollection PerformanceCounters
    {
      get => (CounterCategoryElementCollection) this["performanceCounters"];
      set => this["performanceCounters"] = (object) value;
    }
  }
}
