// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Health.SystemIsBootstrapped
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Telerik.Sitefinity.Abstractions;

namespace Telerik.Sitefinity.Health
{
  /// <summary>
  /// Defines a functionality that checks if the system is up and running.
  /// </summary>
  internal class SystemIsBootstrapped : HealthCheckBase
  {
    private readonly string healthyMessage = "Bootstrapped.";
    private readonly string unhealthyMessage = "Bootstrapping...";

    /// <inheritdoc />
    [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Not needed")]
    [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Not needed")]
    [SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", Justification = "Not needed")]
    protected override async Task<HealthCheckResult> RunCore()
    {
      SystemIsBootstrapped systemIsBootstrapped = this;
      // ISSUE: explicit non-virtual call
      // ISSUE: explicit non-virtual call
      return await Task.FromResult<HealthCheckResult>(Bootstrapper.FinalEventsExecuted ? HealthCheckResult.Healthy(__nonvirtual (systemIsBootstrapped.Name), systemIsBootstrapped.healthyMessage) : HealthCheckResult.Unhealthy(__nonvirtual (systemIsBootstrapped.Name), systemIsBootstrapped.unhealthyMessage));
    }
  }
}
