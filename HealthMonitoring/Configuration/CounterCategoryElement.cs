// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.HealthMonitoring.Configuration.CounterCategoryElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using System.Diagnostics;

namespace Telerik.Sitefinity.HealthMonitoring.Configuration
{
  public class CounterCategoryElement : ConfigurationElement
  {
    [ConfigurationProperty("categoryName", IsKey = true, IsRequired = true)]
    public string CategoryName
    {
      get => (string) this["categoryName"];
      set => this["categoryName"] = (object) value;
    }

    [ConfigurationProperty("description")]
    public string Description
    {
      get => (string) this["description"];
      set => this["description"] = (object) value;
    }

    [ConfigurationProperty("categoryType", DefaultValue = PerformanceCounterCategoryType.SingleInstance)]
    public PerformanceCounterCategoryType CategoryType
    {
      get => (PerformanceCounterCategoryType) this["categoryType"];
      set => this["categoryType"] = (object) value;
    }

    [ConfigurationProperty("counters")]
    public CounterElementCollecton Counters
    {
      get => (CounterElementCollecton) this["counters"];
      set => this["counters"] = (object) value;
    }
  }
}
