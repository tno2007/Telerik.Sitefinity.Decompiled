// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Claims.CustomIssuerNameRegistry
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Security.Claims.SWT;
using Telerik.Sitefinity.Security.Configuration;

namespace Telerik.Sitefinity.Security.Claims
{
  /// <summary>
  /// IssuerNameRegistry that overrides the GetIssuerName in order to extract it from the given SimpleWebToken
  /// </summary>
  public class CustomIssuerNameRegistry : ConfigurationBasedIssuerNameRegistry
  {
    public override string GetIssuerName(SecurityToken securityToken)
    {
      if (securityToken is SimpleWebToken simpleWebToken)
      {
        if (simpleWebToken.Issuer.Equals(ClaimsManager.CurrentAuthenticationModule.GetLocalService()))
          return simpleWebToken.Issuer;
        SecurityTokenKeyElement securityTokenIssuer = Config.Get<SecurityConfig>().SecurityTokenIssuers[simpleWebToken.Issuer];
        if (securityTokenIssuer != null)
        {
          if (!securityTokenIssuer.MembershipProvider.IsNullOrEmpty())
          {
            Claim claim = simpleWebToken.Claims.FirstOrDefault<Claim>((Func<Claim, bool>) (c => c.Type == "http://schemas.sitefinity.com/ws/2011/06/identity/claims/domain"));
            if (claim != null)
            {
              if (!claim.Value.Equals(securityTokenIssuer.MembershipProvider, StringComparison.OrdinalIgnoreCase))
              {
                simpleWebToken.Claims.Remove(claim);
                simpleWebToken.Claims.Add(this.CreateDomainClaim(securityTokenIssuer.MembershipProvider));
              }
            }
            else
              simpleWebToken.Claims.Add(this.CreateDomainClaim(securityTokenIssuer.MembershipProvider));
          }
          return securityTokenIssuer.Realm;
        }
      }
      return (string) null;
    }

    private Claim CreateDomainClaim(string domianName) => new Claim("http://schemas.sitefinity.com/ws/2011/06/identity/claims/domain", domianName, "http://www.w3.org/2001/XMLSchema#string", ClaimsManager.CurrentAuthenticationModule.GetIssuer());
  }
}
