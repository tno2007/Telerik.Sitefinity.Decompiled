// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ProtectionShield.AccessTokenIssuer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using Telerik.Sitefinity.ProtectionShield.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.ProtectionShield
{
  /// <summary>Access token issuer class for processing tokens.</summary>
  internal class AccessTokenIssuer : IAccessTokenIssuer
  {
    private readonly Lazy<AccessTokenManager> accessTokenManager = new Lazy<AccessTokenManager>((Func<AccessTokenManager>) (() => AccessTokenManager.GetManager()), LazyThreadSafetyMode.ExecutionAndPublication);
    /// <summary>By default generates tokens for 10 years</summary>
    private DateTime? expiresOn;

    /// <inheritdoc />
    public DateTime ExpiresOn
    {
      get => this.expiresOn.HasValue ? this.expiresOn.Value : this.GetDefaultExpirationDateTime();
      set => this.expiresOn = new DateTime?(value);
    }

    /// <inheritdoc />
    public AccessToken GenerateToken(
      string email,
      Guid siteId,
      Guid issuerId,
      AccessTokenReason reason)
    {
      return this.GenerateToken(email, siteId, issuerId, this.ExpiresOn, reason);
    }

    /// <inheritdoc />
    public AccessToken GenerateToken(
      string email,
      Guid siteId,
      Guid issuerId,
      DateTime expiresOn,
      AccessTokenReason reason)
    {
      if (siteId == Guid.Empty)
        throw new ArgumentNullException("siteId cannot be empty Guid");
      if (string.IsNullOrWhiteSpace(email))
        throw new ArgumentNullException("email cannot be null or empty");
      AccessToken token = this.TokenManager.GetAccessTokens().FirstOrDefault<AccessToken>((Expression<Func<AccessToken, bool>>) (a => a.Email == email && a.SiteId == siteId));
      if (token == null)
      {
        token = this.TokenManager.CreateAccessToken(AccessTokenStringGenerator.GenerateTokenString(), email, siteId, issuerId, expiresOn, reason);
        this.TokenManager.SaveChanges();
      }
      return token;
    }

    /// <inheritdoc />
    public IEnumerable<AccessToken> GenerateTokens(
      IEnumerable<string> emails,
      Guid siteId,
      Guid issuerId,
      DateTime expiresOn,
      AccessTokenReason reason)
    {
      if (siteId == Guid.Empty)
        throw new ArgumentNullException("siteId cannot be empty Guid");
      if (emails == null)
        throw new ArgumentNullException("emails cannot be null");
      List<AccessToken> tokens = new List<AccessToken>();
      foreach (string email in emails)
      {
        string userEmail = email;
        AccessToken accessToken = this.TokenManager.GetAccessTokens().FirstOrDefault<AccessToken>((Expression<Func<AccessToken, bool>>) (a => a.Email == userEmail && a.SiteId == siteId));
        if (accessToken == null)
        {
          accessToken = this.TokenManager.CreateAccessToken(AccessTokenStringGenerator.GenerateTokenString(), userEmail, siteId, issuerId, expiresOn, reason);
        }
        else
        {
          accessToken.ExpiresOn = expiresOn;
          accessToken.IssuedBy = issuerId;
          accessToken.Status = AccessTokenStatus.Allowed;
        }
        tokens.Add(accessToken);
      }
      this.TokenManager.SaveChanges();
      return (IEnumerable<AccessToken>) tokens;
    }

    /// <inheritdoc />
    public IQueryable<AccessToken> GetAccessTokens(Guid siteId)
    {
      if (siteId == Guid.Empty)
        throw new ArgumentNullException("siteId cannot be empty Guid");
      return this.TokenManager.GetAccessTokens().Where<AccessToken>((Expression<Func<AccessToken, bool>>) (t => t.SiteId == siteId));
    }

    /// <inheritdoc />
    public AccessToken GetAccessToken(string token)
    {
      if (string.IsNullOrWhiteSpace(token))
        throw new ArgumentNullException("token cannot be null or empty");
      return this.TokenManager.GetAccessTokens().FirstOrDefault<AccessToken>((Expression<Func<AccessToken, bool>>) (t => t.Token == token));
    }

    /// <inheritdoc />
    public AccessToken GetAccessToken(Guid siteId, string userEmail)
    {
      if (siteId == Guid.Empty)
        throw new ArgumentNullException("siteId cannot be empty Guid");
      if (string.IsNullOrWhiteSpace(userEmail))
        throw new ArgumentNullException("userEmail cannot be null or empty");
      return this.TokenManager.GetAccessTokens().FirstOrDefault<AccessToken>((Expression<Func<AccessToken, bool>>) (t => t.SiteId == siteId && t.Email == userEmail));
    }

    /// <inheritdoc />
    public bool DeleteToken(AccessToken token)
    {
      AccessToken accessToken = token != null ? this.GetAccessToken(token.Token) : throw new ArgumentNullException("token cannot be null");
      if (accessToken == null)
        return false;
      this.TokenManager.DeleteAccessToken(accessToken);
      this.TokenManager.SaveChanges();
      return true;
    }

    /// <inheritdoc />
    public bool ValidateToken(string token, Guid siteId, out DateTime expiresOn)
    {
      bool flag = false;
      expiresOn = DateTime.MinValue;
      if (!AccessTokenStringGenerator.IsValidTokenString(token))
        return false;
      AccessToken accessToken = AccessTokenManager.GetManager().GetAccessTokens().FirstOrDefault<AccessToken>((Expression<Func<AccessToken, bool>>) (t => t.Token == token && t.SiteId == siteId));
      if (accessToken != null)
      {
        if (accessToken.Reason == AccessTokenReason.BackEndUser)
        {
          Guid currentUserId = SecurityManager.GetCurrentUserId();
          User user = currentUserId != Guid.Empty ? UserManager.FindUserById(currentUserId) : (User) null;
          if (user != null && user.IsBackendUser && accessToken.Email == user.Email)
          {
            flag = true;
            expiresOn = accessToken.ExpiresOn;
          }
        }
        else if (accessToken.Status != AccessTokenStatus.Blocked && accessToken.ExpiresOn > DateTime.Now)
        {
          flag = true;
          expiresOn = accessToken.ExpiresOn;
        }
      }
      return flag;
    }

    /// <inheritdoc />
    public bool SetAccessTokenStatus(AccessToken token, AccessTokenStatus status)
    {
      AccessToken accessToken = this.GetAccessToken(token.Token);
      if (accessToken == null)
        return false;
      accessToken.Status = status;
      this.TokenManager.SaveChanges();
      return true;
    }

    /// <inheritdoc />
    public void Initialize(NameValueCollection config)
    {
    }

    /// <summary>
    /// Gets the instance of <see cref="T:Telerik.Sitefinity.ProtectionShield.AccessTokenManager" />
    /// </summary>
    protected AccessTokenManager TokenManager => this.accessTokenManager.Value;

    private DateTime GetDefaultExpirationDateTime()
    {
      DateTime now = DateTime.Now;
      return new DateTime(now.AddYears(10).Year, now.Month, now.Day);
    }
  }
}
