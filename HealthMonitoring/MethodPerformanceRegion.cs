// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.HealthMonitoring.MethodPerformanceRegion
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;

namespace Telerik.Sitefinity.HealthMonitoring
{
  /// <summary>
  /// Region that measures performance of the code which is executed inside.
  /// </summary>
  /// <seealso cref="T:System.IDisposable" />
  public class MethodPerformanceRegion : IDisposable
  {
    internal const string SystemOperationsCategory = "SystemOperation";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.HealthMonitoring.MethodPerformanceRegion" /> class.
    /// </summary>
    /// <param name="key">The key.</param>
    public MethodPerformanceRegion(string key)
      : this(key, "SystemOperation", (IDictionary<string, object>) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.HealthMonitoring.MethodPerformanceRegion" /> class.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="category">The category of the performance sample.</param>
    public MethodPerformanceRegion(string key, string category)
      : this(key, category, (IDictionary<string, object>) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.HealthMonitoring.MethodPerformanceRegion" /> class.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="category">The category of the performance sample.</param>
    /// <param name="additionalData">Additional data to be recorded together with method performance metrics.</param>
    public MethodPerformanceRegion(
      string key,
      string category,
      IDictionary<string, object> additionalData)
    {
      MethodPerformanceLogger.Current.Start(key, category, additionalData);
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose() => MethodPerformanceLogger.Current.Stop();
  }
}
