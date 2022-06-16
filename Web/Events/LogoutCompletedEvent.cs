// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Events.LogoutCompletedEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Web.Events
{
  /// <inheritdoc />
  internal class LogoutCompletedEvent : 
    LoginEventBase,
    ILogoutCompletedEvent,
    ILoginEventBase,
    IEvent
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.Events.LogoutCompletedEvent" /> class.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <param name="ip">The IP.</param>
    /// <param name="origin">The origin.</param>
    public LogoutCompletedEvent(User user, string ip, string origin)
    {
      if (user == null)
        this.Initialize((string) null, (string) null, (string) null, ip, origin);
      else
        this.Initialize(user.Id.ToString(), user.UserName, user.ProviderName, ip, origin, user.IsBackendUser);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.Events.LogoutCompletedEvent" /> class.
    /// </summary>
    /// <param name="userId">The user id.</param>
    /// <param name="username">The user name.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="ip">The IP.</param>
    /// <param name="origin">The origin.</param>
    /// <param name="isBackend">True if the user is back end user.</param>
    public LogoutCompletedEvent(
      string userId = null,
      string username = null,
      string providerName = null,
      string ip = null,
      string origin = "Sitefinity",
      bool isBackend = false)
    {
      this.Initialize(userId, username, providerName, ip, origin, isBackend);
    }

    private void Initialize(
      string userId,
      string username,
      string providerName,
      string ip,
      string origin,
      bool isBackend = false)
    {
      this.UserId = userId;
      this.Username = username;
      this.ProviderName = providerName;
      this.IpAddress = ip;
      this.IsBackendUser = isBackend;
      this.Origin = origin;
    }
  }
}
