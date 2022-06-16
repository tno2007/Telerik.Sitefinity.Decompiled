// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ProtectionShield.IAccessTokenIssuer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Telerik.Sitefinity.ProtectionShield.Model;

namespace Telerik.Sitefinity.ProtectionShield
{
  /// <summary>Access token issuer interface.</summary>
  internal interface IAccessTokenIssuer
  {
    /// <summary>Gets or sets the expiration date</summary>
    /// <value>The expiration date.</value>
    DateTime ExpiresOn { get; set; }

    /// <summary>Generates a new token.</summary>
    /// <param name="email">The user's email.</param>
    /// <param name="siteId">The site id.</param>
    /// <param name="issuerId">The issuer id.</param>
    /// <param name="reason">The access token reason.</param>
    /// <returns>The generated access token.</returns>
    AccessToken GenerateToken(
      string email,
      Guid siteId,
      Guid issuerId,
      AccessTokenReason reason);

    /// <summary>Generates a new token.</summary>
    /// <param name="email">The user's email.</param>
    /// <param name="siteId">The site id.</param>
    /// <param name="issuerId">The issuer id.</param>
    /// <param name="expiresOn">The expiration date.</param>
    /// <param name="reason">The access token reason.</param>
    /// <returns>The generated access token.</returns>
    AccessToken GenerateToken(
      string email,
      Guid siteId,
      Guid issuerId,
      DateTime expiresOn,
      AccessTokenReason reason);

    /// <summary>Generates tokens for the given emails.</summary>
    /// <param name="emails">The emails.</param>
    /// <param name="siteId">The site id.</param>
    /// <param name="issuerId">The issuer id.</param>
    /// <param name="expiresOn">The expires on.</param>
    /// <param name="reason">The reason.</param>
    /// <returns>The newly created access tokens.</returns>
    IEnumerable<AccessToken> GenerateTokens(
      IEnumerable<string> emails,
      Guid siteId,
      Guid issuerId,
      DateTime expiresOn,
      AccessTokenReason reason);

    /// <summary>Gets the access tokens filtered by site.</summary>
    /// <param name="siteId">The site id.</param>
    /// <returns>The access tokens filtered by site.</returns>
    IQueryable<AccessToken> GetAccessTokens(Guid siteId);

    /// <summary>Gets the access token by token string.</summary>
    /// <param name="token">The token.</param>
    /// <returns>The access token</returns>
    AccessToken GetAccessToken(string token);

    /// <summary>Gets the access token by site id and user email.</summary>
    /// <param name="siteId">The site id.</param>
    /// <param name="userEmail">The user email.</param>
    /// <returns>The access token</returns>
    AccessToken GetAccessToken(Guid siteId, string userEmail);

    /// <summary>Deletes the token.</summary>
    /// <param name="token">The token.</param>
    /// <returns>A value indicating whether the delete was successful.</returns>
    bool DeleteToken(AccessToken token);

    /// <summary>Validates the specified access token.</summary>
    /// <param name="token">The token</param>
    /// <param name="siteId">The site id</param>
    /// <param name="expiresOn">The expiration date.</param>
    /// <returns>A value indicating whether the token is valid.</returns>
    bool ValidateToken(string token, Guid siteId, out DateTime expiresOn);

    /// <summary>Sets the access token status.</summary>
    /// <param name="token">The token.</param>
    /// <param name="status">The status.</param>
    /// <returns>A value indicating whether the operation was successful.</returns>
    bool SetAccessTokenStatus(AccessToken token, AccessTokenStatus status);

    /// <summary>Initializes the access token issuer.</summary>
    /// <param name="config">The config.</param>
    void Initialize(NameValueCollection config);
  }
}
