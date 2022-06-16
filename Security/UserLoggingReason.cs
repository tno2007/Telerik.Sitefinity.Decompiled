// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.UserLoggingReason
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Security
{
  /// <summary>Explains the result of the logging process</summary>
  public enum UserLoggingReason
  {
    /// <summary>User was successfully registered as logged in</summary>
    Success,
    /// <summary>
    /// The limit of maximum simultaneous logged in users is reached
    /// </summary>
    UserLimitReached,
    /// <summary>User not found in any provider</summary>
    UserNotFound,
    /// <summary>User is already logged in from different IP address</summary>
    UserLoggedFromDifferentIp,
    /// <summary>Indicates that the user logical session has expired</summary>
    SessionExpired,
    /// <summary>
    /// User have the authentication cookie but does not have logged in the database
    /// or user is already logged out.
    /// </summary>
    UserLoggedOff,
    /// <summary>
    /// More than one users trying to login from the same IP but from different computers.
    /// </summary>
    UserLoggedFromDifferentComputer,
    /// <summary>Invalid username or password specified.</summary>
    Unknown,
    /// <summary>User is not administrator to logout other users</summary>
    NeedAdminRights,
    /// <summary>
    /// User is already logged in. We need to ask the user to logout someone or himself
    /// </summary>
    UserAlreadyLoggedIn,
    /// <summary>
    /// User was revoked. The reason is that the user was deleted or user rights and role membership was changed.
    /// </summary>
    UserRevoked,
    /// <summary>User is not allowed to access the current site</summary>
    SiteAccessNotAllowed,
    /// <summary>User is not allowed to access the current site</summary>
    SiteNotAccessible,
    /// <summary>An exception is thrown during the login</summary>
    LoginFailedWithError,
    /// <summary>
    /// Could be used for external (non-Sitefinity) validation.
    /// </summary>
    RejectedCustom,
  }
}
