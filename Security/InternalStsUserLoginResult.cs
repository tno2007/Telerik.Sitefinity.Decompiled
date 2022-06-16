// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.InternalStsUserLoginResult
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Security
{
  /// <summary>
  /// Explains the result of the login process taking place on the internal STS that ships with Sitefinity. (IdentityServer3), These events should not be fired if an external separate STS is configured to be used instead of the internal one.
  /// </summary>
  public enum InternalStsUserLoginResult
  {
    /// <summary>User was successfully registered as logged in</summary>
    Success,
    /// <summary>User not found in any provider</summary>
    UserNotFound,
    /// <summary>Invalid username or password specified.</summary>
    InvalidCredentials,
    /// <summary>
    /// An exception is thrown or some other unexpected condition during the login
    /// </summary>
    LoginFailedWithError,
    /// <summary>
    /// No registered provider in Sitefinity STS configuration that matches the external provider (Google, Facebook etc.) identifier
    /// </summary>
    ExternalProviderNotFound,
  }
}
