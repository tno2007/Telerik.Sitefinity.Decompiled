// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Cloud.WindowsAzure.AzureSystemMessageSender
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Microsoft.WindowsAzure.ServiceRuntime;
using System.Collections.Generic;
using System.Net;
using Telerik.Sitefinity.LoadBalancing;

namespace Telerik.Sitefinity.Cloud.WindowsAzure
{
  /// <summary>
  /// An <see cref="T:Telerik.Sitefinity.LoadBalancing.ISystemMessageSender" /> implementation which is active only on Windows Azure
  /// and which sends system messages to all other web role instances, using an internal endpoint.
  /// </summary>
  internal class AzureSystemMessageSender : WebServiceSystemMessageSender
  {
    /// <inheritdoc />
    public override bool IsActive => AzureRuntime.IsRunning;

    /// <inheritdoc />
    public override void SendSystemMessage(SystemMessageBase message)
    {
      if (message != null && message.MessageData != null && message.MessageData.Contains("Type=\"Telerik.Sitefinity.Licensing.Model.UserActivity, Telerik.Sitefinity.Model"))
        return;
      SystemMessageDispatcher.LogTestingMessage(message, "NLB/BROADCAST");
      base.SendSystemMessage(message);
    }

    /// <inheritdoc />
    protected override void Authenticate(HttpWebRequest request)
    {
      if (!SystemMessageDispatcher.SystemMessagingRequiresAuthentication)
        return;
      base.Authenticate(request);
    }

    /// <inheritdoc />
    protected internal override IEnumerable<string> GetUrlsToReceiveMessage()
    {
      RoleInstance currentRoleInstance = RoleEnvironment.CurrentRoleInstance;
      Role role = currentRoleInstance.Role;
      List<string> toReceiveMessage = new List<string>(role.Instances.Count);
      foreach (RoleInstance instance in role.Instances)
      {
        RoleInstanceEndpoint instanceEndpoint;
        if (!(instance.Id == currentRoleInstance.Id) && instance.InstanceEndpoints.TryGetValue("SitefinityInternalEndpoint", out instanceEndpoint))
        {
          string str = string.Format("{0}://{1}{2}/{3}", (object) instanceEndpoint.Protocol, (object) instanceEndpoint.IPEndpoint, (object) "/Sitefinity/Public/Services/LoadBalancing/AzureSystemWebService.svc", (object) "HandleMessages");
          toReceiveMessage.Add(str);
        }
      }
      return (IEnumerable<string>) toReceiveMessage;
    }
  }
}
