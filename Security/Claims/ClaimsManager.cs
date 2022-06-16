// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Claims.ClaimsManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Web;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Security.Claims.SWT;
using Telerik.Sitefinity.Security.Ldap;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Security.Claims
{
  /// <summary>
  /// Contains static methods for storing and reading information from claims and ClaimsPrincipalProxy,
  /// setting the supported authentication methods in the response.
  /// Provides a mechanism for a logout.
  /// </summary>
  public static class ClaimsManager
  {
    private static ClaimsResolver resolver;
    private const string authenticationTransactionName = "Authenticate";
    public const string AuthorizationHeader = "Authorization";
    private const string claimsPrincipalExpected = "ClaimsPrinicpal is expected, but the principal of the current HTTP context was either not set or it's not a ClaimsPrincipal.";
    internal const string StsLoginEventSource = "Sitefinity Login - STS";

    static ClaimsManager() => ClaimsManager.SetResolver();

    public static string GetName(IEnumerable<Claim> claims, bool throwIfMissing = true) => ClaimsManager.resolver.GetName(claims, throwIfMissing);

    public static string GetName(string label = null, bool throwIfMissing = true)
    {
      SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity(label);
      if (currentIdentity != null)
        return ClaimsManager.GetName(currentIdentity.Claims, throwIfMissing);
      if (throwIfMissing)
        throw ClaimsManager.GetMissingIdentityExcception(label);
      return string.Empty;
    }

    public static void SetName(ClaimsIdentity identity, string name) => ClaimsManager.resolver.SetName(identity, name);

    /// <summary>Gets or sets the current authentication module</summary>
    public static ISitefinityClaimsAuthenticationModule CurrentAuthenticationModule { get; internal set; }

    public static Guid GetUserId(IEnumerable<Claim> claims, bool throwIfMissing = true) => ClaimsManager.resolver.GetUserId(claims, throwIfMissing);

    public static Guid GetUserId(string label = null, bool throwIfMissing = true)
    {
      SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity(label);
      if (currentIdentity != null)
        return ClaimsManager.GetUserId(currentIdentity.Claims, throwIfMissing);
      if (throwIfMissing)
        throw ClaimsManager.GetMissingIdentityExcception(label);
      return Guid.Empty;
    }

    public static void SetUserId(ClaimsIdentity identity, Guid userId) => ClaimsManager.SetUserId(identity, userId.ToString());

    public static void SetUserId(ClaimsIdentity identity, string userId) => ClaimsManager.resolver.SetUserId(identity, userId);

    internal static Guid GetTokenId(IEnumerable<Claim> claims, bool throwIfMissing = true) => ClaimsManager.resolver.GetTokenId(claims, throwIfMissing);

    public static void SetTokenId(ICollection<Claim> claims, Guid tokenId) => ClaimsManager.resolver.SetTokenId(claims, tokenId);

    public static DateTime GetIssueDate(IEnumerable<Claim> claims, bool throwIfMissing = true) => ClaimsManager.resolver.GetIssueDate(claims, throwIfMissing);

    public static DateTime GetLastLoginDate(IEnumerable<Claim> claims, bool throwIfMissing = true) => ClaimsManager.resolver.GetLastLoginDate(claims, throwIfMissing);

    public static DateTime GetIssueDate(string label = null, bool throwIfMissing = true)
    {
      SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity(label);
      if (currentIdentity != null)
        return ClaimsManager.GetIssueDate(currentIdentity.Claims, throwIfMissing);
      if (throwIfMissing)
        throw ClaimsManager.GetMissingIdentityExcception(label);
      return DateTime.MinValue;
    }

    public static void SetIssueDate(ClaimsIdentity identity, DateTime date) => ClaimsManager.resolver.SetIssueDate(identity, date);

    public static void SetClaim(ClaimsIdentity identity, string claimType, string value) => ClaimsManager.resolver.SetClaim(identity, claimType, value);

    public static DateTime GetLastLoginDate(string label = null, bool throwIfMissing = true)
    {
      SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity(label);
      if (currentIdentity != null)
        return ClaimsManager.GetLastLoginDate(currentIdentity.Claims, throwIfMissing);
      if (throwIfMissing)
        throw ClaimsManager.GetMissingIdentityExcception(label);
      return DateTime.MinValue;
    }

    public static void SetLastLoginDate(ClaimsIdentity identity, DateTime date) => ClaimsManager.resolver.SetLastLoginDate(identity, date);

    public static string GetMembershipProvider(IEnumerable<Claim> claims, bool throwIfMissing = true) => ClaimsManager.resolver.GetMembershipProvider(claims, throwIfMissing);

    public static string GetMembershipProvider(string label = null, bool throwIfMissing = true)
    {
      SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity(label);
      if (currentIdentity != null)
        return ClaimsManager.GetMembershipProvider(currentIdentity.Claims, throwIfMissing);
      if (throwIfMissing)
        throw ClaimsManager.GetMissingIdentityExcception(label);
      return string.Empty;
    }

    internal static void SetMembershipProvider(ClaimsIdentity identity, string provider) => ClaimsManager.resolver.SetMembershipProvider(identity, provider);

    internal static RoleInfo GetSitefinityRoleInfo(Claim claim)
    {
      if (claim == null)
        throw new ArgumentNullException(nameof (claim));
      return ClaimsManager.resolver.GetRoleInfo(claim.Value);
    }

    public static IEnumerable<RoleInfo> GetSitefinityRoles(
      IEnumerable<Claim> claims,
      bool throwIfMissing = true)
    {
      return ClaimsManager.resolver.GetSitefinityRoles(claims, throwIfMissing);
    }

    public static IEnumerable<RoleInfo> GetSitefinityRoles(
      string label = null,
      bool throwIfMissing = true)
    {
      SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity(label);
      if (currentIdentity != null)
        return ClaimsManager.GetSitefinityRoles(currentIdentity.Claims, throwIfMissing);
      if (throwIfMissing)
        throw ClaimsManager.GetMissingIdentityExcception(label);
      return (IEnumerable<RoleInfo>) new RoleInfo[0];
    }

    public static void SetSitefinityRoles(ClaimsIdentity identity, IEnumerable<RoleInfo> roles) => ClaimsManager.resolver.SetSitefinityRoles(identity, roles);

    public static void SetSitefinityRoles(ClaimsIdentity identity, params RoleInfo[] roles) => ClaimsManager.resolver.SetSitefinityRoles(identity, (IEnumerable<RoleInfo>) roles);

    public static void ParseSitefinityClaims(
      IEnumerable<Claim> claims,
      out string tokenId,
      out Guid userId,
      out string name,
      out string membership,
      out DateTime issueDate,
      out DateTime lastLoginDate,
      out IEnumerable<RoleInfo> roles,
      out bool isUnrestricted,
      out bool isBackendUser)
    {
      ClaimsManager.resolver.ParseSitefinityClaims(claims, out tokenId, out userId, out name, out membership, out issueDate, out lastLoginDate, out roles, out isUnrestricted, out isBackendUser);
    }

    public static void ParseSitefinityClaims(
      IEnumerable<Claim> claims,
      out string tokenId,
      out Guid userId,
      out string name,
      out string membership,
      out DateTime issueDate,
      out DateTime lastLoginDate,
      out IEnumerable<RoleInfo> roles,
      out bool isUnrestricted,
      out bool isBackendUser,
      out string stsType)
    {
      ClaimsManager.resolver.ParseSitefinityClaims(claims, out tokenId, out userId, out name, out membership, out issueDate, out lastLoginDate, out roles, out isUnrestricted, out isBackendUser, out stsType);
    }

    public static void ParseSitefinityClaims(
      IEnumerable<Claim> claims,
      out string tokenId,
      out Guid userId,
      out string name,
      out string membership,
      out DateTime issueDate,
      out DateTime lastLoginDate,
      out IEnumerable<RoleInfo> roles,
      out bool isUnrestricted,
      out bool isBackendUser,
      out string stsType,
      out bool isTrustedUser)
    {
      ClaimsManager.resolver.ParseSitefinityClaims(claims, out tokenId, out userId, out name, out membership, out issueDate, out lastLoginDate, out roles, out isUnrestricted, out isBackendUser, out stsType, out isTrustedUser);
    }

    public static SimpleWebToken BuildSimpleWebToken(string realm = "") => SystemManager.CurrentHttpContext.User is ClaimsPrincipal user ? ClaimsManager.resolver.BuildSimpleWebToken(user, realm) : (SimpleWebToken) null;

    public static SimpleWebToken BuildSimpleWebToken(
      ClaimsPrincipal principal,
      string realm = "")
    {
      return ClaimsManager.resolver.BuildSimpleWebToken(principal, realm);
    }

    /// <summary>
    /// Creates claim which value will be set to the specified field of user's profile
    /// </summary>
    /// <param name="profile">The profile of the user</param>
    /// <param name="field">The field of the profile</param>
    /// <param name="value">The value which will be set to user's profile field</param>
    public static Claim GenerateProfileFieldClaim(string profile, string field, string value) => ClaimsManager.GenerateProfileFieldClaim(string.Format("{0}.{1}", (object) profile, (object) field), value);

    internal static Claim GenerateProfileFieldClaim(string claimMap, string value) => new Claim("ClaimsMapping:" + claimMap, value);

    public static byte[] DeflateString(string str)
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        using (DeflateStream deflateStream = new DeflateStream((Stream) memoryStream, CompressionMode.Compress))
        {
          using (StreamWriter streamWriter = new StreamWriter((Stream) deflateStream, Encoding.UTF8))
            streamWriter.Write(str);
        }
        return memoryStream.ToArray();
      }
    }

    public static string InflateString(byte[] input)
    {
      using (MemoryStream memoryStream = new MemoryStream(input))
      {
        using (DeflateStream deflateStream = new DeflateStream((Stream) memoryStream, CompressionMode.Decompress))
        {
          using (StreamReader streamReader = new StreamReader((Stream) deflateStream, Encoding.UTF8))
            return streamReader.ReadToEnd();
        }
      }
    }

    internal static void SetResolver() => ClaimsManager.SetResolver(ObjectFactory.Resolve<ClaimsResolver>());

    internal static void SetResolver(ClaimsResolver resolver) => ClaimsManager.resolver = resolver;

    public static void AdjustClaims() => ClaimsManager.AdjustClaims(SystemManager.CurrentHttpContext);

    public static void AdjustClaims(HttpContext context) => ClaimsManager.AdjustClaims((HttpContextBase) new HttpContextWrapper(context));

    public static void AdjustClaims(HttpContextBase context)
    {
      if (context == null)
        throw new ArgumentNullException(nameof (context));
      if (!(context.User is ClaimsPrincipal user))
        throw new ArgumentException("ClaimsPrinicpal is expected, but the principal of the current HTTP context was either not set or it's not a ClaimsPrincipal.", "HttpContext.Current.User");
      ClaimsManager.resolver.AdjustClaims(user);
    }

    public static void AdjustClaims(ClaimsPrincipal principal) => ClaimsManager.resolver.AdjustClaims(principal);

    public static SitefinityPrincipal GetAnonymous() => ClaimsManager.resolver.GetAnonymous();

    public static SitefinityPrincipal GetCurrentPrincipal() => ClaimsManager.resolver.GetPrincipal(SystemManager.CurrentHttpContext);

    public static SitefinityPrincipal GetPrincipal(HttpContext context) => ClaimsManager.GetPrincipal((HttpContextBase) new HttpContextWrapper(context));

    public static SitefinityPrincipal GetPrincipal(HttpContextBase context) => ClaimsManager.resolver.GetPrincipal(context);

    public static void Logout(HttpContext context = null, ClaimsPrincipal principal = null)
    {
      if (context != null)
        ClaimsManager.Logout((HttpContextBase) new HttpContextWrapper(context), principal);
      else
        ClaimsManager.Logout(SystemManager.CurrentHttpContext, principal);
    }

    public static void Logout(HttpContextBase context, ClaimsPrincipal principal = null) => ClaimsManager.resolver.Logout(context, principal);

    public static SitefinityIdentity GetCurrentIdentity(string label = null)
    {
      HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
      return currentHttpContext == null ? (SitefinityIdentity) null : ClaimsManager.GetIdentity(currentHttpContext, label);
    }

    public static SitefinityIdentity GetIdentity(
      HttpContext context,
      string label = null)
    {
      return ClaimsManager.GetIdentity((HttpContextBase) new HttpContextWrapper(context), label);
    }

    public static SitefinityIdentity GetIdentity(
      HttpContextBase context,
      string label = null)
    {
      return ClaimsManager.GetIdentity((ClaimsPrincipal) ClaimsManager.resolver.GetPrincipal(context), label);
    }

    public static SitefinityIdentity GetIdentity(
      ClaimsPrincipal principal,
      string label = null)
    {
      if (principal == null)
        return (SitefinityIdentity) null;
      ClaimsIdentity identity = !string.IsNullOrEmpty(label) ? principal.Identities.FirstOrDefault<ClaimsIdentity>((Func<ClaimsIdentity, bool>) (i => i.Label == label)) : (ClaimsIdentity) principal.Identity;
      return identity is SitefinityIdentity ? (SitefinityIdentity) identity : new SitefinityIdentity(identity);
    }

    public static bool IsUnrestricted()
    {
      SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
      return currentIdentity != null && currentIdentity.IsUnrestricted;
    }

    public static bool IsBackendUser()
    {
      SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
      return currentIdentity != null && currentIdentity.IsBackendUser;
    }

    /// <summary>Gets the ID of the currently logged user.</summary>
    /// <returns><c>Guid.Empty</c> if no user is logged on.</returns>
    public static Guid GetCurrentUserId()
    {
      SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
      return currentIdentity != null ? currentIdentity.UserId : Guid.Empty;
    }

    private static InvalidOperationException GetMissingIdentityExcception(
      string label)
    {
      string message;
      if (string.IsNullOrEmpty(label))
        message = "The current request does not have identity.";
      else
        message = "An identity with the specified label ({0}) was not found.".Arrange((object) label);
      return new InvalidOperationException(message);
    }

    public static void SendUnauthorizedResponse(HttpContextBase context) => ClaimsManager.resolver.SendUnauthorizedResponse(context);

    public static void SendErrorResponse(HttpStatusCode statusCode, UserLoggingReason loggngError) => ClaimsManager.resolver.SendErrorResponse(SystemManager.CurrentHttpContext, statusCode, loggngError);

    public static void SendErrorResponse(HttpStatusCode statusCode, string errorMessage) => ClaimsManager.resolver.SendErrorResponse(SystemManager.CurrentHttpContext, statusCode, errorMessage);

    public static void SendErrorResponse(
      HttpStatusCode statusCode,
      UserLoggingReason loggngError,
      HttpContext context,
      bool endRequest = false)
    {
      ClaimsManager.SendErrorResponse(statusCode, loggngError, (HttpContextBase) new HttpContextWrapper(context), endRequest);
    }

    public static void SendErrorResponse(
      HttpStatusCode statusCode,
      UserLoggingReason loggngError,
      HttpContextBase context,
      bool endRequest = false)
    {
      ClaimsManager.resolver.SendErrorResponse(context, statusCode, loggngError, endRequest);
    }

    public static void SendErrorResponse(
      HttpStatusCode statusCode,
      string errorMessage,
      HttpContext context)
    {
      ClaimsManager.SendErrorResponse(statusCode, errorMessage, (HttpContextBase) new HttpContextWrapper(context));
    }

    public static void SendErrorResponse(
      HttpStatusCode statusCode,
      string errorMessage,
      HttpContextBase context)
    {
      ClaimsManager.resolver.SendErrorResponse(context, statusCode, errorMessage);
    }

    public static void RenderLoginForm(HttpContext context, string message = "") => ClaimsManager.RenderLoginForm((HttpContextBase) new HttpContextWrapper(context), message);

    public static void RenderLoginForm(HttpContextBase context, string message = "") => ClaimsManager.resolver.RenderLoginForm(context, message);

    public static bool IsHttps(string urlPath)
    {
      Uri result;
      return Uri.TryCreate(urlPath, UriKind.Absolute, out result) && ClaimsManager.IsHttps(result);
    }

    public static bool IsHttps(Uri url) => url.IsAbsoluteUri && StringComparer.OrdinalIgnoreCase.Equals(url.Scheme, Uri.UriSchemeHttps);

    /// <summary>
    /// Tries to parse the given name value collection and extract the basic auth user if present.
    /// </summary>
    /// <param name="values"></param>
    /// <param name="user">If the given credentials are valid returns the user.</param>
    /// <returns>True if there are credentials in the basic authentication.</returns>
    public static bool TryParseBasicAuth(NameValueCollection values, out User user)
    {
      user = (User) null;
      bool basicAuth = false;
      string str1 = values["Authorization"];
      if (!str1.IsNullOrEmpty())
      {
        string str2 = str1.Trim();
        if (str2.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
        {
          string[] strArray = Encoding.UTF8.GetString(Convert.FromBase64String(str2.Substring(5))).Split(':');
          user = strArray.Length == 2 ? ClaimsManager.ValidateUser(strArray[0], strArray[1]) : throw new HttpException(400, "Incorrect Basic Authentication formatting.");
          basicAuth = true;
        }
      }
      return basicAuth;
    }

    internal static User ValidateUser(string username, string password)
    {
      int length = !username.IsNullOrEmpty() ? username.IndexOf("\\") : throw new ArgumentNullException(username);
      string domain;
      if (length != -1)
      {
        domain = username.Substring(0, length);
        username = username.Substring(length + 1);
      }
      else
        domain = string.Empty;
      return ClaimsManager.ValidateUser(domain, username, password);
    }

    internal static User ValidateUser(string domain, string username, string password = null)
    {
      UserManager manager = UserManager.GetManager(domain, "Authenticate");
      User user = (User) null;
      using (new ElevatedModeRegion((IManager) manager))
      {
        bool flag = manager.Provider is ILdapProviderMarker;
        User byEmailOrUsername = UsersUtils.GetUserByEmailOrUsername(manager, username, !flag);
        if (byEmailOrUsername != null && (password == null || manager.ValidateUser(byEmailOrUsername, password)))
          user = byEmailOrUsername;
        else if (byEmailOrUsername == null)
          SecurityManager.RaiseLoginEvent(username: username, providerName: domain, loginResult: UserLoggingReason.UserNotFound, eventSource: "Sitefinity Login - STS");
        else
          SecurityManager.RaiseLoginEvent(byEmailOrUsername.Id.ToString(), username, byEmailOrUsername.Email, domain, UserLoggingReason.Unknown, "Sitefinity Login - STS");
        TransactionManager.CommitTransaction("Authenticate");
      }
      return user;
    }

    /// <summary>Gets the logout URL.</summary>
    /// <returns>the logout URL</returns>
    public static string GetLogoutUrl(string redirectUri = "") => ClaimsManager.CurrentAuthenticationModule.GetLogoutUrl(redirectUri);
  }
}
