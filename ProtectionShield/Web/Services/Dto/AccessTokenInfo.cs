// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ProtectionShield.Web.Services.Dto.AccessTokenInfo
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.ProtectionShield.Model;

namespace Telerik.Sitefinity.ProtectionShield.Web.Services.Dto
{
  /// <summary>Access token response message</summary>
  internal class AccessTokenInfo
  {
    /// <summary>Gets or sets the Access token id</summary>
    public Guid Id { get; set; }

    /// <summary>Gets or sets the token.</summary>
    /// <value>The token.</value>
    public string Token { get; set; }

    /// <summary>Gets or sets the Email</summary>
    public string Email { get; set; }

    /// <summary>Gets or sets the status</summary>
    public AccessTokenStatus Status { get; set; }

    /// <summary>
    /// Gets or sets a value indicating the number of used devices
    /// </summary>
    public int NumberOfUsedDevices { get; set; }

    /// <summary>Gets or sets an expiration date</summary>
    public DateTime ExpiresOn { get; set; }

    /// <summary>Gets the access token information.</summary>
    /// <param name="accessToken">The access token.</param>
    /// <returns>The access token info</returns>
    public static AccessTokenInfo GetAccessTokenInfo(AccessToken accessToken)
    {
      if (accessToken == null)
        return (AccessTokenInfo) null;
      return new AccessTokenInfo()
      {
        Id = accessToken.Id,
        Email = accessToken.Email,
        Status = accessToken.Status,
        ExpiresOn = accessToken.ExpiresOn,
        Token = accessToken.Token
      };
    }
  }
}
