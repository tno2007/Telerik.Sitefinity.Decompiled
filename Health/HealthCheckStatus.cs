// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Health.HealthCheckStatus
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;

namespace Telerik.Sitefinity.Health
{
  /// <summary>Defines the health check status data.</summary>
  internal class HealthCheckStatus
  {
    private IEnumerable<HealthCheckResult> checks;
    private bool healthy;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Health.HealthCheckStatus" /> class.
    /// </summary>
    /// <param name="results">The list of health check results to be processed.</param>
    public HealthCheckStatus(IEnumerable<HealthCheckResult> results)
    {
      if (results == null)
        return;
      this.checks = results;
      if (this.checks.Any<HealthCheckResult>((Func<HealthCheckResult, bool>) (p => p.Critical)))
        this.healthy = this.checks.Where<HealthCheckResult>((Func<HealthCheckResult, bool>) (r => r.Critical)).All<HealthCheckResult>((Func<HealthCheckResult, bool>) (r => r.Passed));
      else
        this.healthy = this.checks.All<HealthCheckResult>((Func<HealthCheckResult, bool>) (r => r.Passed));
    }

    /// <summary>
    /// Gets or sets a list with the aggregated health check results.
    /// </summary>
    public IEnumerable<HealthCheckResult> Checks
    {
      get => this.checks;
      set => this.checks = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the health checks passed successfully.
    /// </summary>
    public bool Healthy
    {
      get => this.healthy;
      set => this.healthy = value;
    }
  }
}
