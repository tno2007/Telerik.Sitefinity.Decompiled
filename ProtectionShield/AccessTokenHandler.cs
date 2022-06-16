// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ProtectionShield.AccessTokenHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web;
using Telerik.Sitefinity.ProtectionShield.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.ProtectionShield
{
  /// <summary>
  /// Class responsible to handle requests when the site is under shield protection
  /// </summary>
  /// <remarks>
  /// Multithreaded Singleton:
  /// <a href="https://msdn.microsoft.com/en-us/library/ff650316.aspx">See more</a>
  /// </remarks>
  internal sealed class AccessTokenHandler
  {
    internal const string AccessTokenRequestParam = "access_token";
    internal const string CookieKeyFormat = "sf_shield_token_{0}";
    private static volatile AccessTokenHandler instance;
    private static object syncRoot = new object();

    private AccessTokenHandler()
    {
    }

    /// <summary>
    /// Gets an instance of the <see cref="T:Telerik.Sitefinity.ProtectionShield.AccessTokenHandler" /> class.
    /// </summary>
    /// <returns>Instance of the <see cref="T:Telerik.Sitefinity.ProtectionShield.AccessTokenHandler" /> class.</returns>
    public static AccessTokenHandler GetHandler()
    {
      if (AccessTokenHandler.instance == null)
      {
        lock (AccessTokenHandler.syncRoot)
        {
          if (AccessTokenHandler.instance == null)
            AccessTokenHandler.instance = new AccessTokenHandler();
        }
      }
      return AccessTokenHandler.instance;
    }

    /// <summary>
    /// This method checks whether the request has a valid token
    /// </summary>
    /// <param name="httpContext">The context</param>
    /// <returns>Returns if the access is granted</returns>
    internal bool IsAccessGranted(HttpContextBase httpContext)
    {
      string tokenFromRequest = this.TryGetTokenFromRequest(httpContext);
      bool isCookieSet;
      int num = this.TrySetAccessCookieInternal(tokenFromRequest, httpContext, out isCookieSet) ? 1 : 0;
      if ((num & (isCookieSet ? 1 : 0)) == 0)
        return num != 0;
      AccessTokenDeviceTracker.Get().TrackDevice(tokenFromRequest, httpContext.Request);
      return num != 0;
    }

    /// <summary>Sets an access cookie</summary>
    /// <param name="token">The access token</param>
    /// <returns>Returns if the access is granted</returns>
    internal bool TrySetAccessCookie(string token)
    {
      HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
      bool isCookieSet;
      int num = this.TrySetAccessCookieInternal(token, currentHttpContext, out isCookieSet) ? 1 : 0;
      if ((num & (isCookieSet ? 1 : 0)) == 0)
        return num != 0;
      AccessTokenDeviceTracker.Get().TrackDevice(token, currentHttpContext.Request);
      return num != 0;
    }

    /// <summary>This method grants access for current user</summary>
    /// <param name="reason">The access token reason</param>
    internal void GrantCurrentUserAccess(AccessTokenReason reason = AccessTokenReason.FrondEndUser)
    {
      Guid currentUserId = SecurityManager.GetCurrentUserId();
      string currentUserEmail = ProtectionShieldHelper.GetHelper().GetCurrentUserEmail(currentUserId);
      Guid id = SystemManager.CurrentContext.CurrentSite.Id;
      if (!(currentUserId != Guid.Empty) || currentUserEmail.IsNullOrEmpty() || !ProtectionShieldHelper.GetHelper().GetAccessToken(id, currentUserEmail).IsNullOrEmpty())
        return;
      AccessTokenIssuerResolver.ResolveIssuer().GenerateToken(currentUserEmail, id, currentUserId, reason);
    }

    private string TryGetTokenFromRequest(HttpContextBase httpContext)
    {
      HttpRequestBase request = httpContext.Request;
      string currentSiteInternal = this.GetCookieNameForCurrentSiteInternal();
      string tokenFromRequest = request.Cookies[currentSiteInternal] != null ? request.Cookies[currentSiteInternal].Value : string.Empty;
      string str = request.QueryStringGet("access_token");
      if (tokenFromRequest.IsNullOrEmpty() || !str.IsNullOrEmpty())
        tokenFromRequest = str;
      return tokenFromRequest;
    }

    private bool TrySetAccessCookieInternal(
      string token,
      HttpContextBase httpContext,
      out bool isCookieSet)
    {
      Guid id = SystemManager.CurrentContext.CurrentSite.Id;
      DateTime expiresOn;
      int num = AccessTokenIssuerResolver.ResolveIssuer().ValidateToken(token, id, out expiresOn) ? 1 : 0;
      if (num != 0)
      {
        this.SetCookieToResponseInternal(token, httpContext, expiresOn, out isCookieSet);
        return num != 0;
      }
      isCookieSet = false;
      return num != 0;
    }

    private void SetCookieToResponseInternal(
      string token,
      HttpContextBase httpContext,
      DateTime expiresOn,
      out bool isCookieSet)
    {
      string currentSiteInternal = this.GetCookieNameForCurrentSiteInternal();
      HttpCookie cookie1 = httpContext.Request.Cookies[currentSiteInternal];
      if (cookie1 == null || cookie1 != null && cookie1.Value != token)
      {
        HttpCookie cookie2 = new HttpCookie(currentSiteInternal)
        {
          Value = token,
          HttpOnly = true,
          Expires = expiresOn
        };
        httpContext.Response.Cookies.Set(cookie2);
        isCookieSet = true;
      }
      else
        isCookieSet = false;
    }

    private string GetCookieNameForCurrentSiteInternal() => string.Format("sf_shield_token_{0}", (object) SystemManager.CurrentContext.CurrentSite.Name);
  }
}
