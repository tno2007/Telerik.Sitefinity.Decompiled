// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Claims.SWT.SWTIssuer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel;
using System.IdentityModel.Configuration;
using System.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Security.Configuration;

namespace Telerik.Sitefinity.Security.Claims.SWT
{
  /// <summary>STS service that issues a SimpleWebToken</summary>
  public class SWTIssuer : SecurityTokenService
  {
    private static IDictionary<string, byte[]> relyingParties = (IDictionary<string, byte[]>) new Dictionary<string, byte[]>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);

    static SWTIssuer()
    {
      foreach (SecurityTokenKeyElement securityTokenKeyElement in (IEnumerable<SecurityTokenKeyElement>) Config.Get<SecurityConfig>().RelyingParties.Values)
      {
        byte[] numArray = securityTokenKeyElement.Encoding != BinaryEncoding.Hexadecimal ? Convert.FromBase64String(securityTokenKeyElement.Key) : SecurityManager.HexToByte(securityTokenKeyElement.Key);
        SWTIssuer.relyingParties.Add(VirtualPathUtility.RemoveTrailingSlash(securityTokenKeyElement.Realm), numArray);
      }
    }

    public SWTIssuer(SecurityTokenServiceConfiguration config)
      : base(config)
    {
    }

    protected override ClaimsIdentity GetOutputClaimsIdentity(
      ClaimsPrincipal principal,
      RequestSecurityToken request,
      Scope scope)
    {
      return principal != null ? (ClaimsIdentity) principal.Identity : throw new ArgumentNullException(nameof (principal));
    }

    protected override Scope GetScope(ClaimsPrincipal principal, RequestSecurityToken request)
    {
      string url = VirtualPathUtility.RemoveTrailingSlash(request.AppliesTo.Uri.OriginalString);
      byte[] relyingPartyKey = SWTIssuer.GetRelyingPartyKey(url);
      return new Scope()
      {
        AppliesToAddress = url,
        ReplyToAddress = url,
        TokenEncryptionRequired = false,
        SymmetricKeyEncryptionRequired = false,
        SigningCredentials = (SigningCredentials) new SymmetricSigningCredentials(relyingPartyKey)
      };
    }

    /// <summary>
    /// Gets the encryption key of the relying party whose realm matches.
    /// </summary>
    /// <param name="url">URL needed to match with relying parties' realms.</param>
    /// <returns>Encryption key of the found relying party.</returns>
    /// <exception cref="T:System.Configuration.ConfigurationException">Throw if there is no relying party with matching realm.</exception>
    /// "
    public static byte[] GetRelyingPartyKey(string url)
    {
      byte[] key;
      if (!SWTIssuer.TryGetRelyingPartyKey(url, out key))
        throw new ConfigurationException(Res.Get<SecurityResources>().MissingConfigurationForRelyingParty.Arrange((object) url));
      return key;
    }

    /// <summary>
    /// Try to get the encryption key of the relying party whose realm matches
    /// </summary>
    /// <param name="url">URL needed to match with relying parties' realms.</param>
    /// <param name="key">Encryption key of the found relying party</param>
    /// <returns>Returns true if relying party key was found otherwise returns false </returns>
    public static bool TryGetRelyingPartyKey(string url, out byte[] key)
    {
      Uri uri = new Uri(url);
      key = new byte[0];
      bool relyingPartyKey = false;
      KeyValuePair<string, byte[]> keyValuePair = SWTIssuer.relyingParties.FirstOrDefault<KeyValuePair<string, byte[]>>((Func<KeyValuePair<string, byte[]>, bool>) (kvp => SWTIssuer.AreEqual(uri, new Uri(kvp.Key))));
      if (keyValuePair.Value != null)
      {
        key = keyValuePair.Value;
        relyingPartyKey = true;
      }
      else
      {
        Uri relayingPartyUri = new Uri(VirtualPathUtility.RemoveTrailingSlash(ClaimsManager.CurrentAuthenticationModule.GetRealm()));
        if (SWTIssuer.AreEqual(uri, relayingPartyUri) && SWTIssuer.relyingParties.ContainsKey("http://localhost"))
        {
          key = SWTIssuer.relyingParties["http://localhost"];
          relyingPartyKey = true;
        }
      }
      return relyingPartyKey;
    }

    private static bool AreEqual(Uri uri, Uri relayingPartyUri) => uri.Authority.Equals(relayingPartyUri.Authority, StringComparison.OrdinalIgnoreCase) && uri.AbsoluteUri.StartsWith(relayingPartyUri.AbsoluteUri, StringComparison.OrdinalIgnoreCase);
  }
}
