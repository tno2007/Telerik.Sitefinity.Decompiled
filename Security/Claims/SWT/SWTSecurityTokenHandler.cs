// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Claims.SWT.SWTSecurityTokenHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.ServiceModel.Security;
using System.ServiceModel.Web;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Telerik.Sitefinity.Security.Cryptography;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Security.Claims.SWT
{
  /// <summary>
  /// SecurityToken Handler for the SimpleWebToken - provides methods for creating, validating and reading SWT.
  /// </summary>
  public class SWTSecurityTokenHandler : SecurityTokenHandler
  {
    public const string TokenTypeIdentifier = "http://schemas.microsoft.com/ws/2010/07/identitymodel/tokens/SWT";

    public override string[] GetTokenTypeIdentifiers() => new string[1]
    {
      "http://schemas.microsoft.com/ws/2010/07/identitymodel/tokens/SWT"
    };

    public override Type TokenType => typeof (SimpleWebToken);

    public override bool CanReadToken(XmlReader reader) => reader.IsStartElement("BinarySecurityToken", "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd") && reader.GetAttribute("ValueType") == "http://schemas.xmlsoap.org/ws/2009/11/swt-token-profile-1.0";

    public override SecurityToken ReadToken(XmlReader reader)
    {
      string rawToken = this.CanReadToken(reader) ? Encoding.UTF8.GetString(Convert.FromBase64String(reader.ReadElementContentAsString())) : throw new InvalidOperationException("Handler cannot read this token.");
      try
      {
        TraceManager.TraceVerbose("{0} is read by SWTTokenHandler.", (object) rawToken);
        TraceManager.TraceInformation("SimpleWebToken is created.");
      }
      catch
      {
      }
      return (SecurityToken) new SimpleWebToken(rawToken);
    }

    public override bool CanValidateToken => true;

    public override bool CanWriteToken => true;

    public override void WriteToken(XmlWriter writer, SecurityToken token)
    {
      TraceManager.TraceInformation(SR.WritingToken);
      if (!(token is SimpleWebToken simpleWebToken))
        throw new InvalidOperationException(SR.MustBeSWT);
      SWTSecurityTokenHandler.WrapInsideBinarySecurityToken(simpleWebToken.RawToken).WriteTo(writer);
    }

    public static XElement WrapInsideBinarySecurityToken(
      string accessToken,
      bool isBase64Encoded = false)
    {
      return new XElement(XNamespace.Get("http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd").GetName("BinarySecurityToken"), new object[3]
      {
        (object) new XAttribute((XName) "ValueType", (object) "http://schemas.xmlsoap.org/ws/2009/11/swt-token-profile-1.0"),
        (object) new XAttribute((XName) "EncodingType", (object) "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-soap-message-security-1.0#Base64Binary"),
        isBase64Encoded ? (object) accessToken : (object) accessToken.Base64Encode()
      });
    }

    public override ReadOnlyCollection<ClaimsIdentity> ValidateToken(
      SecurityToken token)
    {
      TraceManager.TraceInformation(SR.VerificationStarted);
      if (!(token is SimpleWebToken simpleWebToken))
        throw new InvalidOperationException(SR.MustBeSWT);
      InMemorySymmetricSecurityKey symmetricSecurityKey;
      try
      {
        symmetricSecurityKey = (InMemorySymmetricSecurityKey) this.Configuration.IssuerTokenResolver.ResolveSecurityKey((SecurityKeyIdentifierClause) new KeyNameIdentifierClause(simpleWebToken.Issuer));
        TraceManager.TraceInformation(SR.ResolvingIssuerKey);
      }
      catch (InvalidOperationException ex)
      {
        TraceManager.TraceError((Exception) ex);
        throw new WebFaultException<HttpError>(new HttpError()
        {
          Code = HttpStatusCode.Unauthorized,
          Description = SR.ResolveIssuerKeyFailed
        }, HttpStatusCode.Unauthorized);
      }
      Collection<Uri> allowedAudienceUris = this.Configuration.AudienceRestriction.AllowedAudienceUris;
      TokenValidator tokenValidator = new TokenValidator(symmetricSecurityKey.GetSymmetricKey(), simpleWebToken.Issuer, (IList<Uri>) allowedAudienceUris);
      TraceManager.TraceInformation(SR.ValidatingIssuerSignature);
      if (!tokenValidator.IsHmacValid(simpleWebToken.RawToken))
        throw new WebFaultException<HttpError>(new HttpError()
        {
          Code = HttpStatusCode.Unauthorized,
          Description = SR.SigVerificationFailed
        }, HttpStatusCode.Unauthorized);
      TraceManager.TraceInformation(SR.ValidatingTokenLifetime);
      if (tokenValidator.IsExpired(simpleWebToken.RawToken))
        throw new WebFaultException<HttpError>(new HttpError()
        {
          Code = HttpStatusCode.Unauthorized,
          Description = SR.TokenExpired
        }, HttpStatusCode.Unauthorized);
      if (this.Configuration.IssuerNameRegistry != null)
      {
        string issuerName = this.Configuration.IssuerNameRegistry.GetIssuerName(token);
        TraceManager.TraceInformation(string.Format(SR.IncomingTokenIssuerResolvedAs, (object) issuerName));
        if (issuerName.IsNullOrEmpty() || !issuerName.Equals(simpleWebToken.Issuer, StringComparison.OrdinalIgnoreCase))
          throw new WebFaultException<HttpError>(new HttpError()
          {
            Code = HttpStatusCode.Unauthorized,
            Description = SR.UntrustedIssuer
          }, HttpStatusCode.Unauthorized);
      }
      if (this.Configuration.AudienceRestriction.AudienceMode != AudienceUriMode.Never)
      {
        TraceManager.TraceInformation(SR.ValidatingAudienceInfo);
        if (!tokenValidator.IsAudienceTrusted(simpleWebToken.RawToken))
          throw new WebFaultException<HttpError>(new HttpError()
          {
            Code = HttpStatusCode.Unauthorized,
            Description = SR.AudienceMismatch
          }, HttpStatusCode.Unauthorized);
      }
      Claim newClaim1 = new Claim("http://schemas.sitefinity.com/ws/2011/06/identity/claims/tokenid", simpleWebToken.Id);
      this.Refresh(simpleWebToken.Claims, newClaim1);
      Claim newClaim2 = new Claim("http://schemas.sitefinity.com/ws/2011/06/identity/claims/issuedate", simpleWebToken.ValidFrom.ToString("u"));
      this.Refresh(simpleWebToken.Claims, newClaim2);
      ClaimsIdentity claimsIdentity = new ClaimsIdentity((IEnumerable<Claim>) simpleWebToken.Claims, "OAUTH-SWT");
      if (this.Configuration.SaveBootstrapContext)
      {
        TraceManager.TraceInformation(SR.SavingBootstrapTokenInfo);
        claimsIdentity.BootstrapContext = (object) token;
      }
      TraceManager.TraceInformation(SR.CreatingClaimsInfo);
      return new List<ClaimsIdentity>() { claimsIdentity }.AsReadOnly();
    }

    /// <summary>
    /// Updates the claims with the newClaim value. If it is already there doesn't change the collection.
    /// </summary>
    /// <param name="claims"></param>
    /// <param name="newClaim"></param>
    private void Refresh(IList<Claim> claims, Claim newClaim)
    {
      bool flag = false;
      Claim claim1 = (Claim) null;
      foreach (Claim claim2 in (IEnumerable<Claim>) claims)
      {
        if (claim2.Type == newClaim.Type)
        {
          flag = true;
          if (claim2.Value != newClaim.Value)
          {
            claim1 = claim2;
            break;
          }
          break;
        }
      }
      if (claim1 != null)
      {
        claims.Remove(claim1);
        flag = false;
      }
      if (flag)
        return;
      claims.Add(newClaim);
    }

    public override SecurityToken CreateToken(SecurityTokenDescriptor tokenDescriptor)
    {
      TraceManager.TraceInformation(SR.CreatingSWT);
      StringBuilder stringBuilder = new StringBuilder();
      string str = (string) null;
      foreach (Claim claim in tokenDescriptor.Subject.Claims)
      {
        if (claim.Type == "http://schemas.sitefinity.com/ws/2011/06/identity/claims/tokenid")
          str = claim.Value;
        stringBuilder.AppendFormat("{0}={1}&", (object) claim.Type.UrlEncode(), (object) claim.Value.UrlEncode());
      }
      string source = str ?? Guid.NewGuid().ToString();
      stringBuilder.AppendFormat("TokenId={0}&", (object) source.UrlEncode()).AppendFormat("Issuer={0}&", (object) tokenDescriptor.TokenIssuerName.UrlEncode()).AppendFormat("Audience={0}&", (object) tokenDescriptor.AppliesToAddress.UrlEncode());
      Claim claim1 = tokenDescriptor.Subject.Claims.FirstOrDefault<Claim>((Func<Claim, bool>) (x => x.ValueType == "http://schemas.sitefinity.com/ws/2011/06/identity/claims/lastlogindate"));
      DateTime result = DateTime.UtcNow;
      if (claim1 != null && !DateTime.TryParseExact(claim1.Value, "u", (IFormatProvider) null, DateTimeStyles.None, out result))
        result = DateTime.UtcNow;
      stringBuilder.AppendFormat("ExpiresOn={0:0}", (object) ((result - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds + (double) SWTParser.TokenLifeTime));
      string s = stringBuilder.ToString();
      byte[] hash = new HmacSha256Unmanaged(((SymmetricSecurityKey) tokenDescriptor.SigningCredentials.SigningKey).GetSymmetricKey()).ComputeHash(Encoding.ASCII.GetBytes(s));
      string rawToken = string.Format("{0}&HMACSHA256={1}", (object) s, (object) Convert.ToBase64String(hash).UrlEncode());
      TraceManager.TraceVerbose(SR.RawSTSFormat, (object) rawToken);
      return (SecurityToken) new SimpleWebToken(rawToken);
    }

    public override SecurityKeyIdentifierClause CreateSecurityTokenReference(
      SecurityToken token,
      bool attached)
    {
      return token is SimpleWebToken simpleWebToken ? (SecurityKeyIdentifierClause) new KeyNameIdentifierClause(simpleWebToken.Issuer) : throw new InvalidOperationException("Token must be of type SimpleWebToken");
    }
  }
}
