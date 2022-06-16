// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.ChallengeProperties
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Microsoft.Owin.Security;

namespace Telerik.Sitefinity.Security
{
  /// <summary>Authentication properties for challenges.</summary>
  public static class ChallengeProperties
  {
    internal const string IsExternalProvider = "isExternalProvider";
    internal const string ExternalProviderName = "externalProviderName";
    internal const string RememberMe = "rememberMe";
    internal const string Username = "username";
    internal const string Password = "password";
    internal const string ErrorRedirectUrl = "errorRedirectUrl";
    internal const string AssignmentRoles = "assignmentRoles";
    internal const string MembershipProvider = "membershipProvider";
    internal const string InitialLoginUserName = "initialLoginUserName";
    internal const string SkipAuthentication = "SkipAuthentication";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Microsoft.Owin.Security.AuthenticationProperties" /> class for initial user.
    /// </summary>
    /// <param name="username">The email or user-name.</param>
    /// <returns>An instance of <see cref="T:Microsoft.Owin.Security.AuthenticationProperties" /> containing the required properties</returns>
    public static AuthenticationProperties ForInitialUser(string username) => new AuthenticationProperties()
    {
      Dictionary = {
        ["initialLoginUserName"] = username,
        ["isExternalProvider"] = false.ToString(),
        ["errorRedirectUrl"] = string.Empty
      }
    };

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Microsoft.Owin.Security.AuthenticationProperties" /> class for a local user.
    /// </summary>
    /// <param name="username">The email or user-name.</param>
    /// <param name="password">The password.</param>
    /// <param name="membershipProvider">The name of the membership provider.</param>
    /// <param name="rememberMe">Whether to remember the user or not.</param>
    /// <param name="errorRedirectUrlParameter">The error redirect url parameter.</param>
    /// <returns>An instance of <see cref="T:Microsoft.Owin.Security.AuthenticationProperties" /> containing the required properties for a local user log-in.</returns>
    public static AuthenticationProperties ForLocalUser(
      string username,
      string password,
      string membershipProvider,
      bool rememberMe,
      string errorRedirectUrlParameter)
    {
      return new AuthenticationProperties()
      {
        Dictionary = {
          [nameof (username)] = username,
          [nameof (password)] = password,
          [nameof (membershipProvider)] = membershipProvider,
          [nameof (rememberMe)] = rememberMe.ToString(),
          ["errorRedirectUrl"] = errorRedirectUrlParameter,
          ["isExternalProvider"] = false.ToString()
        }
      };
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Microsoft.Owin.Security.AuthenticationProperties" /> class for an external user.
    /// </summary>
    /// <param name="externalProvider">The external provider name.</param>
    /// <param name="errorRedirectUrlParameter">The error redirect url parameter.</param>
    /// <param name="assignmentRoles">The additional roles that will be assigned to the user.</param>
    /// <returns>An instance of <see cref="T:Microsoft.Owin.Security.AuthenticationProperties" /> containing the required properties for an external user log-in.</returns>
    public static AuthenticationProperties ForExternalUser(
      string externalProvider,
      string errorRedirectUrlParameter,
      string assignmentRoles = "")
    {
      return new AuthenticationProperties()
      {
        Dictionary = {
          ["externalProviderName"] = externalProvider,
          ["errorRedirectUrl"] = errorRedirectUrlParameter,
          ["isExternalProvider"] = true.ToString(),
          [nameof (assignmentRoles)] = assignmentRoles
        }
      };
    }
  }
}
