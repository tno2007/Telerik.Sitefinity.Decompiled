// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Cloud.WindowsAzure.AzureDiagnostics
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.Diagnostics.Management;
using Microsoft.WindowsAzure.ServiceRuntime;
using System;
using System.Diagnostics;

namespace Telerik.Sitefinity.Cloud.WindowsAzure
{
  /// <summary>A wrapper around Windows Azure Diagnostics.</summary>
  public class AzureDiagnostics
  {
    /// <summary>
    /// Configures the Diagnostic Monitor to transfer log data every minute,
    /// adds a <see cref="T:Microsoft.WindowsAzure.Diagnostics.DiagnosticMonitorTraceListener" /> to the <see cref="!:Trace.Listners" /> collection,
    /// and for <c>DEBUG</c> compilation tries to add a listener that writes to the Compute Emulator console.
    /// </summary>
    /// <param name="throwOnError">When <c>true</c>, caught exceptions are rethrown.</param>
    /// <returns><c>true</c> on success; <c>false</c> otherwise (only if <paramref name="throwOnError" /> is <c>false</c>).</returns>
    public static bool Initialize(bool throwOnError = true)
    {
      try
      {
        RoleInstanceDiagnosticManager diagnosticManager = CloudAccountDiagnosticMonitorExtensions.CreateRoleInstanceDiagnosticManager(RoleEnvironment.GetConfigurationSettingValue("Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString"), RoleEnvironment.DeploymentId, RoleEnvironment.CurrentRoleInstance.Role.Name, RoleEnvironment.CurrentRoleInstance.Id);
        DiagnosticMonitorConfiguration currentConfiguration = diagnosticManager.GetCurrentConfiguration();
        currentConfiguration.Logs.ScheduledTransferPeriod = TimeSpan.FromMinutes(1.0);
        diagnosticManager.SetCurrentConfiguration(currentConfiguration);
        Trace.Listeners.Add((TraceListener) new DiagnosticMonitorTraceListener());
        return true;
      }
      catch
      {
        if (!throwOnError)
          return false;
        throw;
      }
    }
  }
}
