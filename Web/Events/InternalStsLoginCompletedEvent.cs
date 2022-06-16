// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Events.InternalStsLoginCompletedEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Web.Events
{
  internal class InternalStsLoginCompletedEvent : 
    LoginEventBase,
    IInternalStsLoginCompletedEvent,
    ILoginEventBase,
    IEvent
  {
    private const string Origin = "Sitefinity STS";

    public InternalStsLoginCompletedEvent(
      string userId = null,
      string username = null,
      string email = null,
      string providerName = null,
      string ip = null,
      InternalStsUserLoginResult loginResult = InternalStsUserLoginResult.Success,
      string externalProviderName = null)
    {
      this.UserId = userId;
      this.Username = username;
      this.Email = email;
      this.ProviderName = providerName;
      this.IpAddress = ip;
      this.LoginResult = loginResult;
      this.ExternalProviderName = externalProviderName;
      this.Origin = "Sitefinity STS";
    }

    public InternalStsUserLoginResult LoginResult { get; set; }

    public string ExternalProviderName { get; set; }
  }
}
