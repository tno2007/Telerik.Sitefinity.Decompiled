// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ProtectionShield.Web.Services.ProtectionShieldWebService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using ServiceStack;
using System;
using Telerik.Sitefinity.ProtectionShield.Model;
using Telerik.Sitefinity.ProtectionShield.Web.Services.Dto;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.ServiceStack.Filters;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.ProtectionShield.Web.Services
{
  /// <summary>
  ///  Represents the web service for the protection shield functionality.
  /// </summary>
  [RequestNonGlobalAdministrationAuthenticationFilter]
  internal class ProtectionShieldWebService : Service
  {
    /// <summary>
    /// Gets a collection of all access tokens. The results are returned in JSON format.
    /// </summary>
    /// <param name="message">Access tokens message</param>
    /// <returns>Collection context object of <see cref="T:Telerik.Sitefinity.ProtectionShield.Web.Services.Dto.AccessTokenInfo" /> objects.</returns>
    public CollectionContext<AccessTokenInfo> Get(
      AccessTokensMessage message)
    {
      return ProtectionShieldHelper.GetHelper().GetAccessTokensCollection(message.Skip, message.Take);
    }

    /// <summary>Activate/Deactivate a protection shield</summary>
    /// <param name="message">The message.</param>
    /// <returns>Shield info</returns>
    public ProtectionShieldInfo Post(ProtectionShieldMessage message)
    {
      ProtectionShieldHelper.GetHelper().ToggleProtectionShieldInternal(message.Activate);
      return new ProtectionShieldInfo()
      {
        IsActiveForCurrentSite = SystemManager.IsShieldEnabled
      };
    }

    /// <summary>Invite users by emails</summary>
    /// <param name="message">The message.</param>
    public void Post(EmailInvitationMessage message)
    {
      if (message.Emails == null || message.Emails.Count == 0)
      {
        this.Response.StatusCode = 400;
      }
      else
      {
        DateTime? expiresOn = message.ExpiresOn;
        if (expiresOn.HasValue)
        {
          expiresOn = message.ExpiresOn;
          DateTime minValue = DateTime.MinValue;
          if ((expiresOn.HasValue ? (expiresOn.HasValue ? (expiresOn.GetValueOrDefault() == minValue ? 1 : 0) : 1) : 0) == 0)
          {
            expiresOn = message.ExpiresOn;
            DateTime maxValue = DateTime.MaxValue;
            if ((expiresOn.HasValue ? (expiresOn.HasValue ? (expiresOn.GetValueOrDefault() == maxValue ? 1 : 0) : 1) : 0) == 0)
              goto label_6;
          }
          this.Response.StatusCode = 400;
          return;
        }
label_6:
        ProtectionShieldHelper helper = ProtectionShieldHelper.GetHelper();
        DateTime expirationDateOrDefault = helper.GetExpirationDateOrDefault(message.ExpiresOn);
        helper.CreateAccessTokensAndSendInvitations(message.Emails, expirationDateOrDefault);
      }
    }

    /// <summary>Resend email invitation to specified access token</summary>
    /// <param name="message">The message.</param>
    /// <returns>Access token info</returns>
    public AccessTokenInfo Post(ResendEmailInvitationMessage message)
    {
      AccessTokenInfo accessTokenInfo1 = (AccessTokenInfo) null;
      if (string.IsNullOrWhiteSpace(message.Token))
      {
        this.Response.StatusCode = 400;
        return accessTokenInfo1;
      }
      IAccessTokenIssuer accessTokenIssuer = AccessTokenIssuerResolver.ResolveIssuer();
      AccessToken accessToken = accessTokenIssuer.GetAccessToken(message.Token);
      if (accessToken == null)
      {
        this.Response.StatusCode = 404;
        return accessTokenInfo1;
      }
      ProtectionShieldHelper helper = ProtectionShieldHelper.GetHelper();
      AccessTokenInvitationsEmailSender invitationsEmailSender = AccessTokenInvitationsEmailSender.Get();
      string navigateUrl = helper.GetNavigateUrl();
      string subject = helper.ConstructEmailSubject(navigateUrl);
      string body = helper.ConstructEmailBody(navigateUrl, accessToken);
      try
      {
        invitationsEmailSender.SendInvitation(accessToken.Email, subject, body);
        accessTokenIssuer.SetAccessTokenStatus(accessToken, AccessTokenStatus.Allowed);
      }
      catch (Exception ex)
      {
        accessTokenIssuer.SetAccessTokenStatus(accessToken, AccessTokenStatus.NotSent);
        this.Response.StatusCode = 500;
      }
      AccessTokenInfo accessTokenInfo2 = AccessTokenInfo.GetAccessTokenInfo(accessToken);
      ProtectionShieldManager manager = ProtectionShieldManager.GetManager();
      accessTokenInfo2.NumberOfUsedDevices = ProtectionShieldHelper.GetAccessTokenDevicesCount(accessTokenInfo2.Token, manager);
      return accessTokenInfo2;
    }

    /// <summary>Block specified access token</summary>
    /// <param name="message">The message.</param>
    /// <returns>Access token info</returns>
    public AccessTokenInfo Post(BlockAccessTokenMessage message)
    {
      AccessTokenInfo accessTokenInfo1 = (AccessTokenInfo) null;
      if (string.IsNullOrWhiteSpace(message.Token))
      {
        this.Response.StatusCode = 400;
        return accessTokenInfo1;
      }
      IAccessTokenIssuer accessTokenIssuer = AccessTokenIssuerResolver.ResolveIssuer();
      AccessToken accessToken = accessTokenIssuer.GetAccessToken(message.Token);
      if (accessToken == null)
      {
        this.Response.StatusCode = 404;
        return accessTokenInfo1;
      }
      if (accessToken.Status != AccessTokenStatus.Blocked)
        accessTokenIssuer.SetAccessTokenStatus(accessToken, AccessTokenStatus.Blocked);
      AccessTokenInfo accessTokenInfo2 = AccessTokenInfo.GetAccessTokenInfo(accessToken);
      ProtectionShieldManager manager = ProtectionShieldManager.GetManager();
      accessTokenInfo2.NumberOfUsedDevices = ProtectionShieldHelper.GetAccessTokenDevicesCount(accessTokenInfo2.Token, manager);
      return accessTokenInfo2;
    }

    /// <summary>Unblock specified access token</summary>
    /// <param name="message">The message.</param>
    /// <returns>Access token info</returns>
    public AccessTokenInfo Post(UnblockAccessTokenMessage message)
    {
      AccessTokenInfo accessTokenInfo1 = (AccessTokenInfo) null;
      if (string.IsNullOrWhiteSpace(message.Token))
      {
        this.Response.StatusCode = 400;
        return accessTokenInfo1;
      }
      IAccessTokenIssuer accessTokenIssuer = AccessTokenIssuerResolver.ResolveIssuer();
      AccessToken accessToken = accessTokenIssuer.GetAccessToken(message.Token);
      if (accessToken == null)
      {
        this.Response.StatusCode = 404;
        return accessTokenInfo1;
      }
      if (accessToken.Status == AccessTokenStatus.Blocked)
        accessTokenIssuer.SetAccessTokenStatus(accessToken, AccessTokenStatus.Allowed);
      AccessTokenInfo accessTokenInfo2 = AccessTokenInfo.GetAccessTokenInfo(accessToken);
      ProtectionShieldManager manager = ProtectionShieldManager.GetManager();
      accessTokenInfo2.NumberOfUsedDevices = ProtectionShieldHelper.GetAccessTokenDevicesCount(accessTokenInfo2.Token, manager);
      return accessTokenInfo2;
    }

    /// <summary>Remove specified access token</summary>
    /// <param name="message">The message.</param>
    /// <returns>Access token info</returns>
    public bool Post(RemoveAccessTokenMessage message)
    {
      if (string.IsNullOrWhiteSpace(message.Token))
      {
        this.Response.StatusCode = 400;
        return false;
      }
      IAccessTokenIssuer accessTokenIssuer = AccessTokenIssuerResolver.ResolveIssuer();
      AccessToken accessToken = accessTokenIssuer.GetAccessToken(message.Token);
      if (accessToken == null)
      {
        this.Response.StatusCode = 404;
        return false;
      }
      accessTokenIssuer.DeleteToken(accessToken);
      return true;
    }
  }
}
