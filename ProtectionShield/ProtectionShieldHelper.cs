// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ProtectionShield.ProtectionShieldHelper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.ProtectionShield.Configuration;
using Telerik.Sitefinity.ProtectionShield.Model;
using Telerik.Sitefinity.ProtectionShield.Web.Services.Dto;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.ProtectionShield
{
  /// <summary>
  /// Helper class that provides helper methods for protection shield functionality.
  /// </summary>
  /// <remarks>
  /// Multithreaded Singleton:
  /// <a href="https://msdn.microsoft.com/en-us/library/ff650316.aspx">See more</a>
  /// </remarks>
  internal class ProtectionShieldHelper
  {
    private static volatile ProtectionShieldHelper instance;
    private static object syncRoot = new object();

    private ProtectionShieldHelper()
    {
    }

    /// <summary>
    /// Gets an instance of the <see cref="T:Telerik.Sitefinity.ProtectionShield.ProtectionShieldHelper" /> class.
    /// </summary>
    /// <returns>Instance of the <see cref="T:Telerik.Sitefinity.ProtectionShield.ProtectionShieldHelper" /> class.</returns>
    public static ProtectionShieldHelper GetHelper()
    {
      if (ProtectionShieldHelper.instance == null)
      {
        lock (ProtectionShieldHelper.syncRoot)
        {
          if (ProtectionShieldHelper.instance == null)
            ProtectionShieldHelper.instance = new ProtectionShieldHelper();
        }
      }
      return ProtectionShieldHelper.instance;
    }

    /// <summary>Gets the access token devices count.</summary>
    /// <param name="accessToken">The access token.</param>
    /// <param name="protectionShieldManager">The <see cref="T:Telerik.Sitefinity.ProtectionShield.ProtectionShieldManager" /> manager.</param>
    /// <returns>
    /// The count of the <see cref="T:Telerik.Sitefinity.ProtectionShield.Model.AccessTokenDevice" /> items for the given <see cref="T:Telerik.Sitefinity.ProtectionShield.Model.AccessToken" />
    /// </returns>
    public static int GetAccessTokenDevicesCount(
      string accessToken,
      ProtectionShieldManager protectionShieldManager)
    {
      return protectionShieldManager.GetAccessTokenDevices().Where<AccessTokenDevice>((Expression<Func<AccessTokenDevice, bool>>) (d => d.AccessToken == accessToken)).Count<AccessTokenDevice>();
    }

    /// <summary>Gets the access token devices count.</summary>
    /// <param name="accessTokens">The access tokens.</param>
    /// <param name="protectionShieldManager">The protection shield manager.</param>
    /// <returns>
    /// The count of the <see cref="T:Telerik.Sitefinity.ProtectionShield.Model.AccessTokenDevice" /> items grouped by <see cref="T:Telerik.Sitefinity.ProtectionShield.Model.AccessToken" /> Id.
    /// </returns>
    public static IDictionary<string, int> GetAccessTokenDevicesCount(
      IEnumerable<string> accessTokens,
      ProtectionShieldManager protectionShieldManager)
    {
      Dictionary<string, int> tokenDevicesCount = new Dictionary<string, int>();
      IQueryable<AccessTokenDevice> source1 = protectionShieldManager.GetAccessTokenDevices().Where<AccessTokenDevice>((Expression<Func<AccessTokenDevice, bool>>) (d => accessTokens.Contains<string>(d.AccessToken)));
      Expression<Func<AccessTokenDevice, string>> keySelector = (Expression<Func<AccessTokenDevice, string>>) (d => d.AccessToken);
      foreach (IGrouping<string, AccessTokenDevice> source2 in (IEnumerable<IGrouping<string, AccessTokenDevice>>) source1.GroupBy<AccessTokenDevice, string>(keySelector))
      {
        string key = source2.Key;
        int num = source2.Count<AccessTokenDevice>();
        tokenDevicesCount.Add(key, num);
      }
      return (IDictionary<string, int>) tokenDevicesCount;
    }

    /// <summary>Get protection shield images</summary>
    /// <returns>The response.</returns>
    public string GetProtectionShieldImg()
    {
      string empty = string.Empty;
      if (SystemManager.IsShieldEnabled && !AccessTokenHandler.GetHandler().IsAccessGranted(SystemManager.CurrentHttpContext))
      {
        string str1 = "<img style='display: none; width: 1px; height: 1px;' src='";
        string str2 = "'/>";
        string format = "{0}/RestApi/{1}?AccessToken={2}";
        StringBuilder stringBuilder = new StringBuilder();
        ISite currentSite = SystemManager.CurrentContext.CurrentSite;
        string absoluteUri1 = ProtectionShieldHelper.GetUri(currentSite.LiveUrl, currentSite.RequiresSsl).AbsoluteUri;
        string absoluteUri2 = ProtectionShieldHelper.GetUri(currentSite.StagingUrl, currentSite.RequiresSsl).AbsoluteUri;
        Guid currentUserId = SecurityManager.GetCurrentUserId();
        string currentUserEmail = this.GetCurrentUserEmail(currentUserId);
        if (currentUserId != Guid.Empty && !currentUserEmail.IsNullOrEmpty())
        {
          string accessToken = this.GetAccessToken(currentSite.Id, currentUserEmail);
          if (!accessToken.IsNullOrEmpty())
          {
            AccessTokenHandler.GetHandler().TrySetAccessCookie(accessToken);
            stringBuilder.Append(str1);
            stringBuilder.Append(string.Format(format, (object) absoluteUri1, (object) "/shield/set-cookie", (object) accessToken.UrlEncode()));
            stringBuilder.Append(str2);
            if (!string.IsNullOrEmpty(absoluteUri2) && absoluteUri2 != absoluteUri1)
            {
              stringBuilder.Append(str1);
              stringBuilder.Append(string.Format(format, (object) absoluteUri2, (object) "/shield/set-cookie", (object) accessToken.UrlEncode()));
              stringBuilder.Append(str2);
            }
            empty = stringBuilder.ToString();
          }
        }
      }
      return empty;
    }

    /// <summary>Get an access token</summary>
    /// <param name="siteId">The site id.</param>
    /// <param name="userEmail">The user email.</param>
    /// <returns>The access token.</returns>
    public string GetAccessToken(Guid siteId, string userEmail)
    {
      IAccessTokenIssuer accessTokenIssuer = AccessTokenIssuerResolver.ResolveIssuer();
      string accessToken1 = (string) null;
      Guid siteId1 = siteId;
      string userEmail1 = userEmail;
      AccessToken accessToken2 = accessTokenIssuer.GetAccessToken(siteId1, userEmail1);
      if (accessToken2 != null)
        accessToken1 = accessToken2.Token;
      return accessToken1;
    }

    /// <summary>Gets a collection of access tokens</summary>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <returns>A collection of access tokens.</returns>
    public CollectionContext<AccessTokenInfo> GetAccessTokensCollection(
      int skip,
      int take)
    {
      Guid id = SystemManager.CurrentContext.CurrentSite.Id;
      IQueryable<AccessToken> source = AccessTokenIssuerResolver.ResolveIssuer().GetAccessTokens(id).Where<AccessToken>((Expression<Func<AccessToken, bool>>) (a => (int) a.Reason != 1));
      int num = source.Count<AccessToken>();
      if (skip > 0)
        source = source.Skip<AccessToken>(skip);
      if (take > 0)
        source = source.Take<AccessToken>(take);
      List<AccessTokenInfo> list = source.Select<AccessToken, AccessTokenInfo>(new Func<AccessToken, AccessTokenInfo>(AccessTokenInfo.GetAccessTokenInfo)).ToList<AccessTokenInfo>();
      IDictionary<string, int> tokenDevicesCount = ProtectionShieldHelper.GetAccessTokenDevicesCount(list.Select<AccessTokenInfo, string>((Func<AccessTokenInfo, string>) (a => a.Token)), ProtectionShieldManager.GetManager());
      foreach (AccessTokenInfo accessTokenInfo in list)
      {
        if (tokenDevicesCount.ContainsKey(accessTokenInfo.Token))
          accessTokenInfo.NumberOfUsedDevices = tokenDevicesCount[accessTokenInfo.Token];
      }
      return new CollectionContext<AccessTokenInfo>((IEnumerable<AccessTokenInfo>) list)
      {
        TotalCount = num
      };
    }

    /// <summary>
    /// Creates access tokens and send email invitations to their owners
    /// </summary>
    /// <param name="usersEmails">The user emails list.</param>
    /// <param name="expiresOn">The common expiration date.</param>
    public void CreateAccessTokensAndSendInvitations(
      ICollection<string> usersEmails,
      DateTime expiresOn)
    {
      IAccessTokenIssuer accessTokenIssuer = AccessTokenIssuerResolver.ResolveIssuer();
      AccessTokenInvitationsEmailSender invitationsEmailSender = AccessTokenInvitationsEmailSender.Get();
      Guid id = SystemManager.CurrentContext.CurrentSite.Id;
      Guid currentUserId = SecurityManager.GetCurrentUserId();
      string navigateUrl = this.GetNavigateUrl();
      IEnumerable<string> emails = usersEmails.Select<string, string>((Func<string, string>) (e => e.Trim())).Distinct<string>();
      foreach (AccessToken token in accessTokenIssuer.GenerateTokens(emails, id, currentUserId, expiresOn, AccessTokenReason.FrondEndUser))
      {
        try
        {
          string subject = this.ConstructEmailSubject(navigateUrl);
          string body = this.ConstructEmailBody(navigateUrl, token);
          invitationsEmailSender.SendInvitation(token.Email, subject, body);
        }
        catch (Exception ex)
        {
          accessTokenIssuer.SetAccessTokenStatus(token, AccessTokenStatus.NotSent);
        }
      }
    }

    /// <summary>Activates / Deactivate the protection shield</summary>
    /// <param name="activate">A value indicating whether protection shield should be activated.</param>
    public void ToggleProtectionShieldInternal(bool activate)
    {
      if ((Config.RestrictionLevel != RestrictionLevel.ReadOnlyConfigFile ? 0 : (!Config.SuppressRestriction ? 1 : 0)) != 0)
        return;
      ConfigManager manager = ConfigManager.GetManager();
      ProtectionShieldConfig section = manager.GetSection<ProtectionShieldConfig>();
      string name = SystemManager.CurrentContext.CurrentSite.Name;
      bool flag = section.EnabledForSites.ContainsKey(name);
      if (activate && !flag)
      {
        EnabledSitesConfigElement element = new EnabledSitesConfigElement((ConfigElement) section.EnabledForSites)
        {
          SiteName = name
        };
        section.EnabledForSites.Add(element);
      }
      else if (!activate & flag)
        section.EnabledForSites.Remove(name);
      using (new FileSystemModeRegion())
        manager.SaveSection((ConfigSection) section);
    }

    /// <summary>Get current navigation site url</summary>
    /// <returns>The current site navigation url.</returns>
    public string GetNavigateUrl()
    {
      ISite currentSite = SystemManager.CurrentContext.CurrentSite;
      return ProtectionShieldHelper.GetUri(currentSite.LiveUrl, currentSite.RequiresSsl).AbsoluteUri;
    }

    /// <summary>A helper method that constructs an email subject</summary>
    /// <param name="navigateUrl">Current site navigate url.</param>
    /// <returns>A constructed email subject.</returns>
    public string ConstructEmailSubject(string navigateUrl)
    {
      string str = navigateUrl.Replace("http://", string.Empty).Replace("https://", string.Empty).TrimEnd('/');
      return Res.Get<ProtectionShieldResources>().AccessEmailTitle.Arrange((object) str);
    }

    /// <summary>A helper method that constructs an email body</summary>
    /// <param name="navigateUrl">Current site navigate url.</param>
    /// <param name="accessToken">The access token.</param>
    /// <returns>A constructed email body.</returns>
    public string ConstructEmailBody(string navigateUrl, AccessToken accessToken)
    {
      string str1 = navigateUrl.Replace("http://", string.Empty).Replace("https://", string.Empty).TrimEnd('/');
      string str2 = navigateUrl + "?" + "access_token" + "=" + accessToken.Token.UrlEncode();
      return Res.Get<ProtectionShieldResources>().UseTheFollowingLinkEmailBody.Arrange((object) str1, (object) accessToken.Email, (object) str2, (object) accessToken.ExpiresOn.ToSitefinityUITime());
    }

    /// <summary>
    /// Gets expiration date if is valid or return the default expiration date
    /// </summary>
    /// <param name="expiresOn">The expires on.</param>
    /// <returns>The expiration date.</returns>
    public DateTime GetExpirationDateOrDefault(DateTime? expiresOn)
    {
      DateTime expirationDateOrDefault;
      if (expiresOn.HasValue)
      {
        DateTime dateTime = expiresOn.Value;
        expirationDateOrDefault = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
      }
      else
        expirationDateOrDefault = AccessTokenIssuerResolver.ResolveIssuer().ExpiresOn;
      return expirationDateOrDefault;
    }

    public string GetCurrentUserEmail(Guid currentUserId)
    {
      if (currentUserId != Guid.Empty)
      {
        User user = UserManager.FindUser(currentUserId);
        if (user != null)
          return user.Email;
      }
      return string.Empty;
    }

    private static Uri GetUri(string domainUrl, bool requireSsl)
    {
      if (string.IsNullOrEmpty(domainUrl))
        domainUrl = SystemManager.CurrentHttpContext == null ? "localhost" : SystemManager.CurrentHttpContext.Request.Url.Authority;
      int num = domainUrl.IndexOf("//");
      if (num > 0)
        domainUrl = domainUrl.Substring(num + 2);
      UriBuilder uriBuilder = new UriBuilder();
      int length1 = domainUrl.IndexOf('/');
      string str;
      if (length1 > 0)
      {
        str = domainUrl.Substring(0, length1);
        uriBuilder.Path = domainUrl.Substring(length1 + 1);
      }
      else
        str = domainUrl;
      int length2 = str.IndexOf(':');
      if (length2 != -1)
      {
        uriBuilder.Port = int.Parse(str.Substring(length2 + 1));
        uriBuilder.Host = str.Substring(0, length2);
      }
      else
        uriBuilder.Host = str;
      uriBuilder.Scheme = !requireSsl ? "http" : "https";
      return uriBuilder.Uri;
    }
  }
}
