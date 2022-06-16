// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.UserSession.UserSessionService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using ServiceStack;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services.UserSession.DTO;

namespace Telerik.Sitefinity.Services.UserSession
{
  /// <summary>
  /// User session service. Used to get information about current user status
  /// </summary>
  internal class UserSessionService : Service
  {
    internal const string WebServiceUrl = "RestApi/session";

    /// <summary>Determines whether the current user is authenticated.</summary>
    /// <returns></returns>
    public StatusResponse Get(StatusRequest request) => new StatusResponse()
    {
      IsAuthenticated = ClaimsManager.GetCurrentIdentity().IsAuthenticated
    };
  }
}
