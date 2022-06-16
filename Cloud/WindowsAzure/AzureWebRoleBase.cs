// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Cloud.WindowsAzure.AzureWebRoleBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Microsoft.WindowsAzure.ServiceRuntime;
using System;
using System.Diagnostics;

namespace Telerik.Sitefinity.Cloud.WindowsAzure
{
  /// <summary>
  /// Sitefinity's Windows Azure role entry point base functionality.
  /// </summary>
  /// <remarks>
  /// On Windows Azure 1.3+ with "full IIS" enabled, the code of the entry point is called in a different process
  /// (the IIS host) and not in the process (IIS working process) in which the web application is running!
  /// </remarks>
  public abstract class AzureWebRoleBase : RoleEntryPoint
  {
    /// <inheritdoc />
    public override bool OnStart()
    {
      try
      {
        AzureDiagnostics.Initialize(false);
        Trace.TraceInformation("Starting web role instance {0}.", (object) RoleEnvironment.CurrentRoleInstance.Id);
        return base.OnStart();
      }
      catch (Exception ex)
      {
        Trace.TraceError("Error starting web role instance {0}: {1}", (object) RoleEnvironment.CurrentRoleInstance.Id, (object) ex);
        throw;
      }
    }

    /// <inheritdoc />
    public override void OnStop()
    {
      try
      {
        Trace.TraceInformation("Stopping web role instance {0}.", (object) RoleEnvironment.CurrentRoleInstance.Id);
        base.OnStop();
      }
      catch (Exception ex)
      {
        Trace.TraceError("Error stopping web role instance {0}: {1}", (object) RoleEnvironment.CurrentRoleInstance.Id, (object) ex);
        throw;
      }
    }
  }
}
