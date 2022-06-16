// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Health.IHealthCheck
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace Telerik.Sitefinity.Health
{
  /// <summary>Defines functionality for health checks.</summary>
  internal interface IHealthCheck
  {
    /// <summary>Gets the name of the health check.</summary>
    string Name { get; }

    /// <summary>Gets the health check timeout.</summary>
    TimeSpan? Timeout { get; }

    /// <summary>Gets a value indicating whether check is critical.</summary>
    bool Critical { get; }

    /// <summary>Gets a value for additional parameters.</summary>
    NameValueCollection Parameters { get; }

    /// <summary>Gets the available groups.</summary>
    IEnumerable<string> Groups { get; }

    /// <summary>
    /// Executes the health check and returns the aggregated result.
    /// </summary>
    /// <returns>The health check result.</returns>
    Task<HealthCheckResult> Run();
  }
}
