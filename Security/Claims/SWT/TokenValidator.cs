// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Claims.SWT.TokenValidator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Telerik.Sitefinity.Security.Cryptography;

namespace Telerik.Sitefinity.Security.Claims.SWT
{
  /// <summary>Provides a method for validating a SimpleWebToken</summary>
  public class TokenValidator
  {
    private readonly string issuerLabel = "Issuer";
    private readonly string expiresLabel = "ExpiresOn";
    private readonly string audienceLabel = "Audience";
    private readonly string hmacSha256Label = "HMACSHA256";
    private readonly string hmacsha1Label = "HMACSHA1";
    private readonly byte[] trustedSigningKey;
    private readonly string trustedTokenIssuer;
    private readonly IList<Uri> trustedAudienceUris;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.Claims.SWT.TokenValidator" /> class.
    /// </summary>
    /// <param name="trustedSigningKey">The trusted signing key.</param>
    /// <param name="trustedTokenIssuer">The trusted token issuer.</param>
    /// <param name="trustedAudienceUris">The trusted audience uris.</param>
    public TokenValidator(
      byte[] trustedSigningKey,
      string trustedTokenIssuer,
      IList<Uri> trustedAudienceUris)
    {
      this.trustedSigningKey = trustedSigningKey;
      this.trustedTokenIssuer = trustedTokenIssuer;
      this.trustedAudienceUris = trustedAudienceUris;
    }

    public bool Validate(string token) => this.IsHmacValid(token) && !this.IsExpired(token) && this.IsIssuerTrusted(token) && this.IsAudienceTrusted(token);

    private static ulong GenerateTimeStamp() => Convert.ToUInt64((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds);

    public bool IsAudienceTrusted(string token)
    {
      KeyValuePair<string, string> keyValuePair = SWTParser.Parse(token).FirstOrDefault<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (p => p.Key == this.audienceLabel));
      string uriString = keyValuePair.Equals((object) new KeyValuePair<string, string>()) ? string.Empty : keyValuePair.Value;
      return !string.IsNullOrEmpty(uriString) && this.trustedAudienceUris.Contains(new Uri(uriString));
    }

    public bool IsIssuerTrusted(string token)
    {
      KeyValuePair<string, string> keyValuePair = SWTParser.Parse(token).FirstOrDefault<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (p => p.Key == this.issuerLabel));
      string str = keyValuePair.Equals((object) new KeyValuePair<string, string>()) ? string.Empty : keyValuePair.Value;
      return !string.IsNullOrEmpty(str) && str.Equals(this.trustedTokenIssuer);
    }

    public bool IsHmacValid(string swt)
    {
      byte[] trustedSigningKey = this.trustedSigningKey;
      bool flag = true;
      string[] strArray = swt.Split(new string[1]
      {
        "&" + this.hmacsha1Label + "="
      }, StringSplitOptions.None);
      if (strArray == null || strArray.Length != 2)
      {
        strArray = swt.Split(new string[1]
        {
          "&" + this.hmacSha256Label + "="
        }, StringSplitOptions.None);
        if (strArray == null || strArray.Length != 2)
          throw new Exception("Invalid token format. Unable to extract signature from the token.");
        flag = false;
      }
      return Convert.ToBase64String(!flag ? new HmacSha256Unmanaged(trustedSigningKey).ComputeHash(Encoding.UTF8.GetBytes(strArray[0])) : new HMACSHA1(trustedSigningKey).ComputeHash(Encoding.UTF8.GetBytes(strArray[0]))).UrlEncode() == strArray[1];
    }

    public bool IsExpired(string swt)
    {
      try
      {
        ulong uint64 = Convert.ToUInt64(SWTParser.Parse(swt).First<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (p => p.Key == this.expiresLabel)).Value);
        return Convert.ToUInt64(TokenValidator.GenerateTimeStamp()) > uint64;
      }
      catch (KeyNotFoundException ex)
      {
        throw new ArgumentException();
      }
    }
  }
}
