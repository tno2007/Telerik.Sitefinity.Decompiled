// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.HealthMonitoring.Configuration.CounterElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using System.Diagnostics;

namespace Telerik.Sitefinity.HealthMonitoring.Configuration
{
  public class CounterElement : ConfigurationElement
  {
    [ConfigurationProperty("counterName", IsKey = true, IsRequired = true)]
    public string CounterName
    {
      get => (string) this["counterName"];
      set => this["counterName"] = (object) value;
    }

    [ConfigurationProperty("description")]
    public string Description
    {
      get => (string) this["description"];
      set => this["description"] = (object) value;
    }

    [ConfigurationProperty("instanceName", DefaultValue = "")]
    public string InstanceName
    {
      get => (string) this["instanceName"];
      set => this["instanceName"] = (object) value;
    }

    [ConfigurationProperty("instanceLifetime", DefaultValue = PerformanceCounterInstanceLifetime.Global)]
    public PerformanceCounterInstanceLifetime InstanceLifetime
    {
      get => (PerformanceCounterInstanceLifetime) this["instanceLifetime"];
      set => this["instanceLifetime"] = (object) value;
    }
  }
}
