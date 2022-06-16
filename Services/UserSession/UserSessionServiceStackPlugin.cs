// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.UserSession.UserSessionServiceStackPlugin
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using ServiceStack;
using Telerik.Sitefinity.Services.UserSession.DTO;

namespace Telerik.Sitefinity.Services.UserSession
{
  internal class UserSessionServiceStackPlugin : IPlugin
  {
    private const string ServiceRoute = "/session";

    /// <summary>Adding the comment service routes</summary>
    /// <param name="appHost"></param>
    public void Register(IAppHost appHost)
    {
      appHost.RegisterService(typeof (UserSessionService));
      appHost.Routes.Add<StatusRequest>("/session" + "/" + "is-authenticated", "GET");
    }
  }
}
