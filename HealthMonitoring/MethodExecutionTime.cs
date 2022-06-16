// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.HealthMonitoring.MethodExecutionTime
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Telerik.Sitefinity.HealthMonitoring
{
  internal class MethodExecutionTime
  {
    private readonly long timestamp;
    private readonly Stopwatch stopwatch;
    private TimeSpan executionTime;
    private DateTime endTime;

    internal MethodExecutionTime(string key)
      : this(key, DateTime.UtcNow)
    {
    }

    internal MethodExecutionTime(string key, DateTime start)
    {
      this.StartTime = start;
      this.Key = key;
      this.timestamp = Stopwatch.GetTimestamp();
      this.stopwatch = Stopwatch.StartNew();
      this.Children = (IList<MethodExecutionTime>) new List<MethodExecutionTime>();
    }

    public long Timestamp => this.timestamp;

    public IDictionary<string, object> AdditionalData { get; set; }

    public string Category { get; set; }

    internal string Key { get; set; }

    internal DateTime StartTime { get; set; }

    internal DateTime EndTime
    {
      get => this.endTime;
      set
      {
        this.endTime = value;
        this.executionTime = this.stopwatch.Elapsed;
      }
    }

    internal TimeSpan ExecutionTime => this.executionTime;

    internal IList<MethodExecutionTime> Children { get; set; }
  }
}
