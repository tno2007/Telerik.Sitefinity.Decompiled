// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.LoadBalancing.Web.Services.MsmqWebService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ServiceModel;
using System.ServiceModel.MsmqIntegration;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.LoadBalancing.Web.Services
{
  /// <summary>
  /// A class for handling internal communication between sitefinity instances at load balanced environment.
  /// </summary>
  public class MsmqWebService : IMsmqWebService
  {
    [OperationBehavior]
    public void HandleSystemMessage(MsmqMessage<SystemMessageBase> message)
    {
      if (!(message.Body.SenderId != Environment.MachineName))
        return;
      ServiceUtility.RequestBackendUserAuthentication();
      SystemMessageDispatcher.HandleSystemMessage(message.Body);
    }
  }
}
