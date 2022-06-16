// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Claims.SFClaimsAuthenticationManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Microsoft.Owin.Security;
using System;
using System.Net;
using System.Security.Principal;
using System.Threading;
using System.Web;
using Telerik.Sitefinity.Security.Claims.SWT;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Security.Claims
{
  /// <summary>
  /// Provides methods for Redirection to the sign out handler.
  /// </summary>
  public class SFClaimsAuthenticationManager
  {
    internal const string RejectionReasonContextKey = "sf-rejectionReason";

    internal static SitefinityPrincipal ValidateLimitations(
      SitefinityPrincipal principal,
      HttpContextBase context)
    {
      if (principal.Identity.IsAuthenticated)
      {
        string userHostAddress = context.Request.UserHostAddress;
        SitefinityIdentity identity = (SitefinityIdentity) principal.Identity;
        string lastLoginStamp = identity.LastLoginDate.ToString("u");
        UserLoggingReason userLoggingReason = SecurityManager.VerifyAuthenticateRequest(principal, userHostAddress, lastLoginStamp);
        switch (userLoggingReason)
        {
          case UserLoggingReason.Success:
            return principal;
          case UserLoggingReason.UserNotFound:
            if (!identity.IsExternalUser)
              break;
            goto case UserLoggingReason.Success;
        }
        if (!SFClaimsAuthenticationManager.AllowServiceRequest(context, userLoggingReason))
          return SFClaimsAuthenticationManager.RejectAuthentication(context, principal, userLoggingReason);
      }
      return principal;
    }

    internal static bool IsServiceRequest() => SystemManager.CurrentContext.IsServiceRequest;

    internal static bool AllowServiceRequest(HttpContextBase context, UserLoggingReason reason) => (reason == UserLoggingReason.UserAlreadyLoggedIn || reason == UserLoggingReason.UserLoggedFromDifferentComputer ? 1 : (reason == UserLoggingReason.UserLoggedFromDifferentIp ? 1 : 0)) != 0 && SFClaimsAuthenticationManager.IsServiceRequest();

    internal static SitefinityPrincipal RejectAuthentication(
      HttpContextBase context,
      SitefinityPrincipal principal,
      UserLoggingReason loginResult,
      string[] removeParams = null)
    {
      if (context.Request.RawUrl.Contains("Sitefinity/SignOut") || SFClaimsAuthenticationManager.AllowServiceRequest(context, loginResult))
        return principal;
      string str1 = context.Request.AppRelativeCurrentExecutionFilePath.Substring(2) + context.Request.PathInfo;
      string str2 = "Sitefinity" + "/Services";
      string str3 = "Sitefinity" + "/Frontend/Services";
      string str4 = "RestApi/Sitefinity";
      if ((str1.StartsWith(str2, StringComparison.OrdinalIgnoreCase) || str1.StartsWith(str3, StringComparison.OrdinalIgnoreCase) || str1.StartsWith(str4, StringComparison.OrdinalIgnoreCase) || RouteManager.RequireBasicAuthentication(context) || str1.StartsWith("Sitefinity/CMIS/RestAtom/") ? 1 : (SFClaimsAuthenticationManager.IsServiceRequest() ? 1 : 0)) != 0)
        ClaimsManager.SendErrorResponse(HttpStatusCode.Forbidden, loginResult, context);
      else
        SFClaimsAuthenticationManager.SetRejectionInformation(context, principal, loginResult, removeParams);
      SitefinityPrincipal anonymous = ClaimsManager.GetAnonymous();
      context.User = (IPrincipal) anonymous;
      Thread.CurrentPrincipal = (IPrincipal) null;
      return anonymous;
    }

    internal static void SetRejectionInformation(
      HttpContextBase context,
      SitefinityPrincipal rejectedPrincipal,
      UserLoggingReason reason,
      string[] removeParams = null)
    {
      context.Items[(object) "sf-rejectionReason"] = (object) new RejectionReason()
      {
        Reason = reason,
        RejectedPrincipal = rejectedPrincipal,
        RemoveParams = removeParams
      };
    }

    /// <summary>
    /// Determines whether a user has been authenticated but rejected by the system (e.g. because of licensing limitations).
    /// </summary>
    /// <param name="context">The context.</param>
    /// <returns></returns>
    internal static bool HasRejectedUser(HttpContextBase context) => context.Items[(object) "sf-rejectionReason"] is RejectionReason;

    /// <summary>
    /// Check if there is a rejected user and redirets him/her to the appropriate informational page.
    /// <para>When the user returns from the sts and he/she has not been rejected - clears the query string.</para>
    /// </summary>
    /// <param name="currentContext">The current context.</param>
    /// <param name="returnUrl">If there is a request for rejected user handling with a return url after the user rejection is handled the user is redirected to that return url.</param>
    internal static void ProcessRejectedUserClearQueryString(
      HttpContextBase currentContext,
      string returnUrl = null)
    {
      if (!ClaimsManager.GetCurrentIdentity().IsAuthenticated || string.IsNullOrWhiteSpace(returnUrl))
        return;
      if (returnUrl.StartsWith("//"))
        returnUrl = "/" + returnUrl.TrimStart('/');
      if (!returnUrl.StartsWith("/") && !returnUrl.StartsWith("%2F", StringComparison.InvariantCultureIgnoreCase))
        SWTIssuer.GetRelyingPartyKey(returnUrl);
      currentContext.Response.Redirect(returnUrl, true);
    }

    /// <summary>
    /// If the user was authenticated with a valid token but has not been accepted by the RP (e.g. because of licensing limitations)
    /// redirects the user to the appropriate information page or sends the appropriate error message. And it will return the user back.
    /// </summary>
    /// <param name="context">The current http context.</param>
    /// <param name="returnUrl">If a return Url has been given it will be loaded when the user successfully handles the rejection reason. If return Url is not specified the current one will be used.</param>
    public static void ProcessRejectedUser(HttpContextBase context, string returnUrl = null)
    {
      if (!(context.Items[(object) "sf-rejectionReason"] is RejectionReason rejectionReason))
        return;
      switch (rejectionReason.Reason)
      {
        case UserLoggingReason.Success:
          break;
        case UserLoggingReason.UserNotFound:
          break;
        case UserLoggingReason.SessionExpired:
        case UserLoggingReason.UserLoggedOff:
        case UserLoggingReason.UserRevoked:
          Microsoft.Owin.Security.IAuthenticationManager authentication1 = context.GetOwinContext().Authentication;
          AuthenticationProperties properties1 = new AuthenticationProperties();
          properties1.IssuedUtc = new DateTimeOffset?(DateTimeOffset.UtcNow);
          properties1.RedirectUri = context.GetOwinContext().Request.Uri.AbsoluteUri;
          string[] strArray1 = new string[1]
          {
            ClaimsManager.CurrentAuthenticationModule.STSAuthenticationType
          };
          authentication1.Challenge(properties1, strArray1);
          context.ApplicationInstance.CompleteRequest();
          break;
        case UserLoggingReason.RejectedCustom:
          break;
        default:
          Microsoft.Owin.Security.IAuthenticationManager authentication2 = context.GetOwinContext().Authentication;
          AuthenticationProperties properties2 = new AuthenticationProperties();
          properties2.IssuedUtc = new DateTimeOffset?(DateTimeOffset.UtcNow);
          properties2.RedirectUri = context.GetOwinContext().Request.Uri.AbsoluteUri;
          string[] strArray2 = new string[1]
          {
            ClaimsManager.CurrentAuthenticationModule.RPAuthenticationType
          };
          authentication2.Challenge(properties2, strArray2);
          context.ApplicationInstance.CompleteRequest();
          break;
      }
    }
  }
}
