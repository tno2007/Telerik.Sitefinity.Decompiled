// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Health.StartUpFinished
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Routing;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Health
{
  /// <summary>
  /// Defines a functionality that checks if the Sitefinity StartUp finished.
  /// </summary>
  internal class StartUpFinished : HealthCheckBase
  {
    private readonly string healthyMessage = "Finished.";
    private readonly string unhealthyMessage = "Sitefinity StartUp is still running...";

    /// <inheritdoc />
    [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Not needed")]
    [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Not needed")]
    [SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", Justification = "Not needed")]
    protected override async Task<HealthCheckResult> RunCore()
    {
      StartUpFinished startUpFinished = this;
      // ISSUE: explicit non-virtual call
      // ISSUE: explicit non-virtual call
      return await Task.FromResult<HealthCheckResult>(RouteManager.GetRegisteredRoutes().Any<RouteRegistration>((Func<RouteRegistration, bool>) (r => StartUpFinished.IsStartupRoute(r.Route))) ? HealthCheckResult.Unhealthy(__nonvirtual (startUpFinished.Name), startUpFinished.unhealthyMessage) : HealthCheckResult.Healthy(__nonvirtual (startUpFinished.Name), startUpFinished.healthyMessage));
    }

    private static bool IsStartupRoute(RouteBase routeBase) => routeBase is Route route && string.Equals(route.Url, "Sitefinity/Startup");
  }
}
