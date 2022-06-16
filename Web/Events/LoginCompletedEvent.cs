// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Events.LoginCompletedEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Web.Events
{
  /// <inheritdoc />
  internal class LoginCompletedEvent : LoginEventBase, ILoginCompletedEvent, ILoginEventBase, IEvent
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.Events.LoginCompletedEvent" /> class.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <param name="ip">The IP.</param>
    /// <param name="loginResult">The login result.</param>
    /// <param name="origin">The origin.</param>
    public LoginCompletedEvent(User user, string ip, UserLoggingReason loginResult, string origin)
    {
      if (user == null)
        this.Initialize((string) null, (string) null, (string) null, (string) null, ip, loginResult, origin);
      else
        this.Initialize(user.Id.ToString(), user.UserName, user.Email, user.ProviderName, ip, loginResult, origin, user.IsBackendUser);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.Events.LoginCompletedEvent" /> class.
    /// </summary>
    /// <param name="userId">The user id.</param>
    /// <param name="username">The username.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="ip">The IP.</param>
    /// <param name="loginResult">The login result.</param>
    /// <param name="origin">The origin.</param>
    /// <param name="isBackend">The is backend.</param>
    /// <param name="email">The is user email.</param>
    public LoginCompletedEvent(
      string userId = null,
      string username = null,
      string email = null,
      string providerName = null,
      string ip = null,
      UserLoggingReason loginResult = UserLoggingReason.Success,
      string origin = "Sitefinity",
      bool isBackend = false)
    {
      this.Initialize(userId, username, email, providerName, ip, loginResult, origin, isBackend);
    }

    private void Initialize(
      string userId,
      string username,
      string email,
      string providerName,
      string ip,
      UserLoggingReason loginResult,
      string origin,
      bool isBackend = false)
    {
      this.UserId = userId;
      this.Username = username;
      this.ProviderName = providerName;
      this.IpAddress = ip;
      this.IsBackendUser = isBackend;
      this.LoginResult = loginResult;
      this.Origin = origin;
      this.Email = email;
    }

    /// <inheritdoc />
    public UserLoggingReason LoginResult { get; set; }
  }
}
