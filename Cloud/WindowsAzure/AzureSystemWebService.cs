// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Cloud.WindowsAzure.AzureSystemWebService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Telerik.Sitefinity.LoadBalancing;
using Telerik.Sitefinity.LoadBalancing.Web.Services;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Cloud.WindowsAzure
{
  /// <summary>
  /// A service that receives system messages sent from other Sitefinity instances in Windows Azure load balanced environment.
  /// </summary>
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
  public class AzureSystemWebService : ISystemWebService
  {
    /// <summary>Service endpoint URL.</summary>
    public const string EndpointUrl = "/Sitefinity/Public/Services/LoadBalancing/AzureSystemWebService.svc";

    /// <summary>Handles a system message.</summary>
    /// <remarks>
    /// Discards requests that are not sent to the internal endpoint.
    /// </remarks>
    public void HandleSystemMessage(SystemMessageBase message)
    {
      this.AuthenticateRequest();
      SystemMessageDispatcher.HandleSystemMessage(message);
    }

    /// <inheritdoc />
    public void HandleSystemMessages(SystemMessageBase[] messages)
    {
      this.AuthenticateRequest();
      SystemMessageDispatcher.HandleSystemMessages(messages);
    }

    private void AuthenticateRequest()
    {
      if (SystemMessageDispatcher.SystemMessagingRequiresAuthentication)
      {
        try
        {
          ServiceUtility.RequestBackendUserAuthentication();
        }
        catch (Exception ex)
        {
          SystemMessageDispatcher.LogTestingMessage(ex.ToString(), "NLB/SECURITY");
          throw;
        }
      }
      else
      {
        Uri to = OperationContext.Current.IncomingMessageHeaders.To;
        Uri internalEndpointUri = AzureRuntime.CurrentRoleInternalEndpointUri;
        if ((to.Port == internalEndpointUri.Port ? 0 : (!string.Equals(to.Host, Environment.MachineName, StringComparison.InvariantCultureIgnoreCase) ? 1 : 0)) == 0)
          return;
        SystemMessageDispatcher.LogTestingMessage(string.Format("NLB notification discarded. It was sent to {0}, while the internal endpoint is bound to {1} and the machine name is {2}.", (object) to, (object) internalEndpointUri, (object) Environment.MachineName), "NLB/SECURITY");
      }
    }
  }
}
