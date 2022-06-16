// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.LoadBalancing.Web.Services.ISystemWebService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ServiceModel;
using System.ServiceModel.Web;

namespace Telerik.Sitefinity.LoadBalancing.Web.Services
{
  /// <summary>
  /// Base contract for handling internal communication between Sitefinity instances at load balanced environment.
  /// </summary>
  [ServiceContract]
  public interface ISystemWebService
  {
    /// <summary>Handles a system message.</summary>
    /// <param name="message">The message.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/HandleMessage")]
    void HandleSystemMessage(SystemMessageBase message);

    /// <summary>Handles the system messages.</summary>
    /// <param name="message">The system messages.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/HandleMessages")]
    void HandleSystemMessages(SystemMessageBase[] messages);
  }
}
