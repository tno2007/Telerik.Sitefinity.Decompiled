// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.LoadBalancing.Web.Services.SystemWebService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Web;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.LoadBalancing.Web.Services
{
  /// <summary>
  /// A class for handling internal communication between Sitefinity instances in load balanced environment.
  /// </summary>
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  public class SystemWebService : ISystemWebService
  {
    private static string localId = (string) null;
    private static readonly object syncLock = new object();
    /// <summary>Service endpoint URL.</summary>
    public const string EndpointUrl = "/Sitefinity/Services/LoadBalancing/SystemWebService.svc/";

    /// <inheritdoc />
    public void HandleSystemMessage(SystemMessageBase message)
    {
      if (!this.ShouldHandleMessage(message))
        return;
      ServiceUtility.RequestBackendUserAuthentication();
      SystemMessageDispatcher.HandleSystemMessage(message);
    }

    /// <summary>Handles the system messages.</summary>
    /// <param name="message">The system messages.</param>
    public void HandleSystemMessages(SystemMessageBase[] messages)
    {
      if (messages.Length == 0 || !this.ShouldHandleMessage(messages[0]))
        return;
      ServiceUtility.RequestBackendUserAuthentication();
      SystemMessageDispatcher.HandleSystemMessages(messages);
    }

    private bool ShouldHandleMessage(SystemMessageBase message)
    {
      if (!(message.SenderId == SystemWebService.LocalId))
        return true;
      HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
      SystemWebService.LocalUrl = currentHttpContext == null ? (string) null : currentHttpContext.Request.Url.Scheme + "://" + currentHttpContext.Request.Url.Authority;
      return false;
    }

    /// <summary>Gets or sets the local URL.</summary>
    /// <value>The local URL.</value>
    internal static string LocalUrl { get; private set; }

    /// <summary>Gets the unique local id for the current instance.</summary>
    /// <value>The local id.</value>
    internal static string LocalId
    {
      get
      {
        if (SystemWebService.localId == null)
        {
          lock (SystemWebService.syncLock)
          {
            if (SystemWebService.localId == null)
              SystemWebService.localId = Guid.NewGuid().ToString();
          }
        }
        return SystemWebService.localId;
      }
    }
  }
}
