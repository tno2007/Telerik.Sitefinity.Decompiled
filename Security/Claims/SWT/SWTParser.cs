// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Claims.SWT.SWTParser
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Security.Claims.SWT
{
  /// <summary>
  /// Provides methods for parsing SimpleWebToken from QueryString or Authorization Header.
  /// Gives access to Claims collection and token properties.
  /// </summary>
  public class SWTParser
  {
    public const string IssuerLabel = "Issuer";
    public const string ExpiresLabel = "ExpiresOn";
    public const string AudienceLabel = "Audience";
    public const string TokenIdLabel = "TokenId";
    public const string TokenPrefix = "WRAP access_token";
    private readonly IList<KeyValuePair<string, string>> keyValueCollection;
    private readonly DateTime validFrom;
    private const int tokenLifeTime = 3600;

    internal static int TokenLifeTime => !SystemManager.ServiceTokenLifetime.HasValue ? 3600 : SystemManager.ServiceTokenLifetime.Value;

    public SWTParser(string urlDecodedSWT)
    {
      this.validFrom = DateTime.UtcNow;
      this.keyValueCollection = SWTParser.Parse(urlDecodedSWT);
    }

    public static string EncryptionLabel => "HMACSHA256";

    public List<Claim> Claims => this.keyValueCollection.Where<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (e => e.Key != "TokenId" && e.Key != "Issuer" && e.Key != "ExpiresOn" && e.Key != "Audience" && e.Key != SWTParser.EncryptionLabel)).Select<KeyValuePair<string, string>, Claim>((Func<KeyValuePair<string, string>, Claim>) (kv => new Claim(kv.Key, kv.Value))).ToList<Claim>();

    public string TokenId => this.keyValueCollection.First<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (p => p.Key == nameof (TokenId))).Value;

    public string Issuer => this.keyValueCollection.First<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (p => p.Key == nameof (Issuer))).Value;

    public string Audience => this.keyValueCollection.First<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (p => p.Key == nameof (Audience))).Value;

    public DateTime ExpiresOn => new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds((double) Convert.ToInt64(this.keyValueCollection.First<KeyValuePair<string, string>>((Func<KeyValuePair<string, string>, bool>) (p => p.Key == nameof (ExpiresOn))).Value)).ToUniversalTime();

    public DateTime ValidFrom => this.ExpiresOn.AddSeconds((double) -SWTParser.TokenLifeTime);

    public static IList<KeyValuePair<string, string>> Parse(string token)
    {
      if (string.IsNullOrEmpty(token))
        throw new ArgumentException();
      return (IList<KeyValuePair<string, string>>) ((IEnumerable<string>) token.Split('&')).Aggregate<string, List<KeyValuePair<string, string>>>(new List<KeyValuePair<string, string>>(), (Func<List<KeyValuePair<string, string>>, string, List<KeyValuePair<string, string>>>) ((dict, rawNameValue) =>
      {
        if (rawNameValue == string.Empty)
          return dict;
        string[] strArray = rawNameValue.Split('=');
        if (strArray.Length != 2)
          throw new ArgumentException("Invalid formEncodedstring - contains a name/value pair missing an = character", strArray.Length != 0 ? strArray[0] : "");
        dict.Add(new KeyValuePair<string, string>(strArray[0].UrlDecode(), strArray[1].UrlDecode()));
        return dict;
      }));
    }

    public static string ExtractAndDecodeAccessToken(string authorizationHeader)
    {
      if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("WRAP access_token", StringComparison.OrdinalIgnoreCase))
        return (string) null;
      authorizationHeader = authorizationHeader.Remove(0, "WRAP access_token".Length).TrimStart(' ');
      if (authorizationHeader[0] != '=')
        return (string) null;
      return authorizationHeader.TrimStart('=', ' ').Trim('"');
    }
  }
}
