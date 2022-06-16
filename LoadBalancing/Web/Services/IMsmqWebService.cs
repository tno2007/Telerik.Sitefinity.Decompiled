// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.LoadBalancing.Web.Services.IMsmqWebService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ServiceModel;
using System.ServiceModel.MsmqIntegration;

namespace Telerik.Sitefinity.LoadBalancing.Web.Services
{
  /// <summary>
  /// Msmq for handling internal communication between Sitefinity instances at load balanced environment.
  /// </summary>
  [ServiceContract]
  [ServiceKnownType(typeof (SystemMessageBase))]
  public interface IMsmqWebService
  {
    [OperationContract(Action = "*", IsOneWay = true)]
    void HandleSystemMessage(MsmqMessage<SystemMessageBase> msg);
  }
}
