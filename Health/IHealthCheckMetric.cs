// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Health.IHealthCheckMetric`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Threading.Tasks;

namespace Telerik.Sitefinity.Health
{
  /// <summary>
  /// Defines the functionality for retrieving health check metric.
  /// </summary>
  /// <typeparam name="TResult">The metric value type.</typeparam>
  internal interface IHealthCheckMetric<TResult>
  {
    /// <summary>Returns a metric value.</summary>
    /// <returns>The metric value.</returns>
    Task<TResult> Get();
  }
}
