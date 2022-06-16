// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Health.HealthCheckRunner
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Telerik.Sitefinity.Health
{
  /// <summary>
  /// Defines functionality for running a list of health checks.
  /// </summary>
  internal class HealthCheckRunner
  {
    private readonly IEnumerable<IHealthCheck> healthCheckers;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Health.HealthCheckRunner" /> class.
    /// </summary>
    /// <param name="healthCheckers">The list of health checks to be executed.</param>
    public HealthCheckRunner(IEnumerable<IHealthCheck> healthCheckers) => this.healthCheckers = healthCheckers;

    /// <summary>
    /// Runs the provided health checks in parallel and returns the computed status.
    /// </summary>
    /// <returns>The health check status.</returns>
    [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Need to catching all exceptions in this case.")]
    [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Not needed")]
    [SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", Justification = "Not needed")]
    public async Task<HealthCheckStatus> Run()
    {
      int num;
      if (num != 0 && (this.healthCheckers == null || this.healthCheckers.Count<IHealthCheck>() == 0))
        return new HealthCheckStatus(Enumerable.Empty<HealthCheckResult>());
      try
      {
        return new HealthCheckStatus((IEnumerable<HealthCheckResult>) await Task.WhenAll<HealthCheckResult>((IEnumerable<Task<HealthCheckResult>>) this.healthCheckers.AsParallel<IHealthCheck>().Select<IHealthCheck, Task<HealthCheckResult>>((Func<IHealthCheck, Task<HealthCheckResult>>) (check =>
        {
          try
          {
            return check.Run();
          }
          catch (Exception ex)
          {
            return Task.FromResult<HealthCheckResult>(HealthCheckResult.Unhealthy(check.Name, ex.Message));
          }
        }))).ConfigureAwait(false));
      }
      catch (AggregateException ex)
      {
        return new HealthCheckStatus(ex.Flatten().InnerExceptions.Select<Exception, HealthCheckResult>((Func<Exception, HealthCheckResult>) (inner => HealthCheckResult.Unhealthy("HealthCheck", inner.Message))));
      }
      catch (Exception ex)
      {
        return new HealthCheckStatus((IEnumerable<HealthCheckResult>) new HealthCheckResult[1]
        {
          HealthCheckResult.Unhealthy("HealthCheck", ex.Message)
        });
      }
    }
  }
}
