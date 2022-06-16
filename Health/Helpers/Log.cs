// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Health.Helpers.Log
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Health.Helpers
{
  /// <summary>A class for writing a log entry.</summary>
  internal class Log : ILog
  {
    /// <inheritdoc />
    public virtual void WriteInfo(string message, bool healthCheckResult)
    {
      if (!Config.Get<SystemConfig>().HealthCheckConfig.Logging && healthCheckResult)
        return;
      Telerik.Sitefinity.Abstractions.Log.Write((object) message, ConfigurationPolicy.HealthCheckLog);
    }
  }
}
