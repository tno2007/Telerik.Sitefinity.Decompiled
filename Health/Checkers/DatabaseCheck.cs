// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Health.DatabaseCheck
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Metadata.Model;

namespace Telerik.Sitefinity.Health
{
  /// <summary>
  /// Defines a functionality that checks if database is working.
  /// </summary>
  internal class DatabaseCheck : HealthCheckBase
  {
    private readonly string noConnectionMessage = "No Database connection";
    private readonly string healthyMessage = "Database is up";

    /// <inheritdoc />
    [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Not needed")]
    [SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", Justification = "Not needed")]
    protected override async Task<HealthCheckResult> RunCore()
    {
      DatabaseCheck databaseCheck = this;
      // ISSUE: explicit non-virtual call
      // ISSUE: explicit non-virtual call
      return MetadataManager.GetManager().GetModuleVersions().Count<ModuleVersion>() < 1 ? await Task.FromResult<HealthCheckResult>(HealthCheckResult.Unhealthy(__nonvirtual (databaseCheck.Name), databaseCheck.noConnectionMessage)) : await Task.FromResult<HealthCheckResult>(HealthCheckResult.Healthy(__nonvirtual (databaseCheck.Name), databaseCheck.healthyMessage));
    }
  }
}
