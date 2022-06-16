// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Claims.ClaimsResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.IdentityModel.Configuration;
using System.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Services;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Security.Claims.SWT;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Security.Sanitizers;

namespace Telerik.Sitefinity.Security.Claims
{
  /// <summary>
  /// Helper class for working with SitefinityPrincipal and SitefinityIdentity.
  /// </summary>
  internal class ClaimsResolver
  {
    private const string IdentityExpected = "The current principal must provide at least one identity.";

    public virtual SitefinityPrincipal GetPrincipal(HttpContextBase context)
    {
      IPrincipal user = context.User;
      if (user is SitefinityPrincipal)
      {
        if (Thread.CurrentPrincipal != user)
          Thread.CurrentPrincipal = user;
        return user as SitefinityPrincipal;
      }
      SitefinityPrincipal principal = user != null ? new SitefinityPrincipal(user as ClaimsPrincipal) : this.GetAnonymous();
      context.User = (IPrincipal) principal;
      Thread.CurrentPrincipal = (IPrincipal) principal;
      return principal;
    }

    public virtual SitefinityPrincipal GetAnonymous() => new SitefinityPrincipal((ClaimsIdentity) SitefinityIdentity.GetAnonymous());

    public void Logout(HttpContextBase context, ClaimsPrincipal principal = null)
    {
      bool flag = false;
      SitefinityPrincipal principal1 = this.GetPrincipal(context);
      if (principal == null || principal.Identity.Name == principal1.Identity.Name)
      {
        flag = true;
        principal = (ClaimsPrincipal) principal1;
      }
      this.ClearUserActivity(principal);
      if (!flag)
        return;
      if (FederatedAuthentication.SessionAuthenticationModule != null)
        FederatedAuthentication.SessionAuthenticationModule.SignOut();
      SecurityManager.DeleteCookie(SecurityManager.TokenIdCookieName, "/", string.Empty, false);
      context.User = (IPrincipal) this.GetAnonymous();
    }

    private void ClearUserActivity(ClaimsPrincipal principal)
    {
      if (!principal.Identity.IsAuthenticated)
        return;
      SitefinityIdentity identity = ClaimsManager.GetIdentity(principal);
      string membershipProvider = identity.MembershipProvider;
      Guid userId = identity.UserId;
      SecurityManager.ClearUserActivity(membershipProvider, userId);
      SecurityManager.RaiseLogoutEvent(userId.ToString(), identity.Name, membershipProvider, isBackend: identity.IsBackendUser);
    }

    public virtual void AdjustClaims(ClaimsPrincipal principal)
    {
      IEnumerable<ClaimsIdentity> source = principal != null ? principal.Identities : throw new ArgumentNullException(nameof (principal));
      if (source == null || !source.Any<ClaimsIdentity>())
        throw new ArgumentException("The current principal must provide at least one identity.", "HttpContext.Current.User.Identity");
      foreach (ClaimsIdentity identity in source)
        this.AdjustClaims(identity);
    }

    protected virtual bool AdjustClaims(ClaimsIdentity identity)
    {
      IEnumerable<Claim> source = identity != null ? identity.Claims : throw new ArgumentNullException(nameof (identity));
      if (source.Any<Claim>((Func<Claim, bool>) (c => c.Type == "http://schemas.sitefinity.com/ws/2011/06/identity/claims/adjusted")))
        return false;
      bool flag = false;
      List<RoleInfo> roles = new List<RoleInfo>();
      string str1 = (string) null;
      string userName = (string) null;
      string str2 = (string) null;
      foreach (Claim claim in source)
      {
        string type = claim.Type;
        if (type == "http://schemas.sitefinity.com/ws/2011/06/identity/claims/adjusted")
          return false;
        if (!(type == "http://schemas.sitefinity.com/ws/2011/06/identity/claims/userid"))
        {
          if (!(type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"))
          {
            if (!(type == "http://schemas.sitefinity.com/ws/2011/06/identity/claims/domain"))
            {
              if (!(type == "http://schemas.sitefinity.com/ws/2011/06/identity/claims/role"))
              {
                if (type == "http://schemas.sitefinity.com/ws/2011/06/identity/claims/lastlogindate")
                  flag = true;
              }
              else
                roles.Add(this.GetRoleInfo(claim.Value));
            }
            else
              str2 = claim.Value;
          }
          else
            userName = claim.Value;
        }
        else
          str1 = claim.Value;
      }
      if (!flag)
      {
        Claim claim = new Claim("http://schemas.sitefinity.com/ws/2011/06/identity/claims/lastlogindate", DateTime.UtcNow.ToString("u"));
        identity.AddClaim(claim);
      }
      Claim claim1 = new Claim("http://schemas.sitefinity.com/ws/2011/06/identity/claims/adjusted", string.Empty);
      identity.AddClaim(claim1);
      int length = !userName.IsNullOrEmpty() ? userName.IndexOf('\\') : throw new ArgumentException("An authenticated ClaimsIdentity must have a name claim.");
      if (length != -1)
      {
        string str3 = userName.Substring(0, length);
        userName = userName.Substring(length + 1);
        Claim claim2 = source.Single<Claim>((Func<Claim, bool>) (c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"));
        identity.RemoveClaim(claim2);
        identity.AddClaim(new Claim(claim2.Type, userName, claim2.ValueType, claim2.Issuer, claim2.OriginalIssuer));
        if (str2.IsNullOrEmpty())
        {
          str2 = str3;
          this.SetClaim(identity, "http://schemas.sitefinity.com/ws/2011/06/identity/claims/domain", str2);
        }
      }
      UserManager manager = UserManager.GetManager(str2 ?? string.Empty);
      User user = manager.GetUser(userName);
      if (user == null)
      {
        this.SetLocalRoles(identity, roles, Guid.Empty);
        identity.AddClaim(this.CreateRoleClaim(SecurityManager.AuthenticatedRole));
        identity.AddClaim(new Claim("http://schemas.sitefinity.com/ws/2011/06/identity/claims/isexternaluser", string.Empty));
        return false;
      }
      Guid id = user.Id;
      if (str1.IsNullOrEmpty())
        this.SetUserId(identity, id);
      else if (!str1.Equals(id.ToString(), StringComparison.OrdinalIgnoreCase))
      {
        Claim claim3 = source.Single<Claim>((Func<Claim, bool>) (c => c.Type == "http://schemas.sitefinity.com/ws/2011/06/identity/claims/userid"));
        identity.RemoveClaim(claim3);
        this.SetUserId(identity, id);
      }
      this.SetClaim(identity, "http://schemas.sitefinity.com/ws/2011/06/identity/claims/domain", manager.Provider.Name);
      this.SetLocalRoles(identity, roles, id);
      return true;
    }

    private void SetLocalRoles(ClaimsIdentity identity, List<RoleInfo> roles, Guid userId)
    {
      foreach (RoleInfo roleInfo in RoleManager.GetAllRolesOfUser(userId))
      {
        RoleInfo localRole = roleInfo;
        if (!roles.Any<RoleInfo>((Func<RoleInfo, bool>) (r => r.Id == localRole.Id)))
          identity.AddClaim(this.CreateRoleClaim(localRole));
      }
    }

    public virtual string GetName(IEnumerable<Claim> claims, bool throwIfMissing)
    {
      string name;
      if (this.TryGetValue(claims, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", out name))
        return name;
      if (throwIfMissing)
        throw this.GetMissingClaimExcepton("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name");
      return string.Empty;
    }

    public virtual void SetName(ClaimsIdentity identity, string name)
    {
      if (identity == null)
        throw new ArgumentNullException(nameof (identity));
      string claimsIssuer = this.GetClaimsIssuer();
      identity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", name, "http://www.w3.org/2001/XMLSchema#string", claimsIssuer, claimsIssuer));
    }

    public virtual Guid GetUserId(IEnumerable<Claim> claims, bool throwIfMissing)
    {
      string input;
      if (this.TryGetValue(claims, "http://schemas.sitefinity.com/ws/2011/06/identity/claims/userid", out input))
        return Guid.Parse(input);
      if (throwIfMissing)
        throw this.GetMissingClaimExcepton("http://schemas.sitefinity.com/ws/2011/06/identity/claims/userid");
      return Guid.Empty;
    }

    public virtual void SetUserId(ClaimsIdentity identity, Guid id) => this.SetUserId(identity, id.ToString());

    public virtual void SetUserId(ClaimsIdentity identity, string id)
    {
      if (identity == null)
        throw new ArgumentNullException(nameof (identity));
      string claimsIssuer = this.GetClaimsIssuer();
      identity.AddClaim(new Claim("http://schemas.sitefinity.com/ws/2011/06/identity/claims/userid", id, "http://www.w3.org/2001/XMLSchema#string", claimsIssuer, claimsIssuer));
    }

    internal Guid GetTokenId(IEnumerable<Claim> claims, bool throwIfMissing)
    {
      string input;
      if (this.TryGetValue(claims, "http://schemas.sitefinity.com/ws/2011/06/identity/claims/tokenid", out input))
        return Guid.Parse(input);
      if (throwIfMissing)
        throw this.GetMissingClaimExcepton("http://schemas.sitefinity.com/ws/2011/06/identity/claims/tokenid");
      return Guid.Empty;
    }

    public virtual void SetTokenId(ICollection<Claim> claims, Guid tokenId)
    {
      if (claims == null)
        throw new ArgumentNullException(nameof (claims));
      string claimsIssuer = this.GetClaimsIssuer();
      claims.Add(new Claim("http://schemas.sitefinity.com/ws/2011/06/identity/claims/tokenid", tokenId.ToString(), "http://www.w3.org/2001/XMLSchema#string", claimsIssuer, claimsIssuer));
    }

    public virtual DateTime GetIssueDate(IEnumerable<Claim> claims, bool throwIfMissing)
    {
      string str;
      if (this.TryGetValue(claims, "http://schemas.sitefinity.com/ws/2011/06/identity/claims/issuedate", out str))
        return SecurityManager.GetUtcDate(str);
      if (throwIfMissing)
        throw this.GetMissingClaimExcepton("http://schemas.sitefinity.com/ws/2011/06/identity/claims/issuedate");
      return DateTime.MinValue;
    }

    public virtual void SetIssueDate(ClaimsIdentity identity, DateTime date)
    {
      if (identity == null)
        throw new ArgumentNullException(nameof (identity));
      string claimsIssuer = this.GetClaimsIssuer();
      identity.AddClaim(new Claim("http://schemas.sitefinity.com/ws/2011/06/identity/claims/issuedate", date.ToString("u"), "http://www.w3.org/2001/XMLSchema#dateTime", claimsIssuer, claimsIssuer));
    }

    public virtual DateTime GetLastLoginDate(IEnumerable<Claim> claims, bool throwIfMissing)
    {
      string str;
      if (this.TryGetValue(claims, "http://schemas.sitefinity.com/ws/2011/06/identity/claims/lastlogindate", out str))
        return SecurityManager.GetUtcDate(str);
      if (throwIfMissing)
        throw this.GetMissingClaimExcepton("http://schemas.sitefinity.com/ws/2011/06/identity/claims/lastlogindate");
      return DateTime.MinValue;
    }

    public virtual void SetLastLoginDate(ClaimsIdentity identity, DateTime date)
    {
      if (identity.Claims == null)
        throw new ArgumentNullException("claims");
      string claimsIssuer = this.GetClaimsIssuer();
      identity.AddClaim(new Claim("http://schemas.sitefinity.com/ws/2011/06/identity/claims/lastlogindate", date.ToString("u"), "http://www.w3.org/2001/XMLSchema#dateTime", claimsIssuer, claimsIssuer));
    }

    public virtual string GetMembershipProvider(IEnumerable<Claim> claims, bool throwIfMissing)
    {
      string membershipProvider;
      if (!this.TryGetValue(claims, "http://schemas.sitefinity.com/ws/2011/06/identity/claims/domain", out membershipProvider) & throwIfMissing)
        throw this.GetMissingClaimExcepton("http://schemas.sitefinity.com/ws/2011/06/identity/claims/domain");
      return membershipProvider;
    }

    internal void SetMembershipProvider(ClaimsIdentity identity, string provider)
    {
      if (identity == null)
        throw new ArgumentNullException(nameof (identity));
      string claimsIssuer = this.GetClaimsIssuer();
      identity.AddClaim(new Claim("http://schemas.sitefinity.com/ws/2011/06/identity/claims/domain", provider, "http://www.w3.org/2001/XMLSchema#string", claimsIssuer, claimsIssuer));
    }

    public virtual IEnumerable<RoleInfo> GetSitefinityRoles(
      IEnumerable<Claim> claims,
      bool throwIfMissing)
    {
      if (claims == null)
        throw new ArgumentNullException(nameof (claims));
      List<RoleInfo> sitefinityRoles = new List<RoleInfo>();
      foreach (Claim claim in claims)
      {
        if (claim.Type == "http://schemas.sitefinity.com/ws/2011/06/identity/claims/role")
          sitefinityRoles.Add(this.GetRoleInfo(claim.Value));
      }
      if (sitefinityRoles.Count == 0 & throwIfMissing)
        throw this.GetMissingClaimExcepton("http://schemas.sitefinity.com/ws/2011/06/identity/claims/role");
      return (IEnumerable<RoleInfo>) sitefinityRoles;
    }

    public virtual void SetSitefinityRoles(ClaimsIdentity identity, IEnumerable<RoleInfo> roles)
    {
      if (identity.Claims == null)
        throw new ArgumentNullException("claims");
      if (roles == null)
        throw new ArgumentNullException(nameof (roles));
      foreach (RoleInfo role in roles)
        identity.AddClaim(this.CreateRoleClaim(role));
    }

    public virtual Claim CreateRoleClaim(RoleInfo roleInfo)
    {
      string str = roleInfo.Id.ToString() + ";" + roleInfo.Name + ";" + roleInfo.Provider;
      string claimsIssuer = this.GetClaimsIssuer();
      return new Claim("http://schemas.sitefinity.com/ws/2011/06/identity/claims/role", str, "http://www.w3.org/2001/XMLSchema#string", claimsIssuer, claimsIssuer);
    }

    public virtual void SetClaim(ClaimsIdentity identity, string claimType, string value)
    {
      if (identity.Claims == null)
        throw new ArgumentNullException("claims");
      if (claimType.IsNullOrEmpty())
        throw new ArgumentNullException(nameof (claimType));
      if (value.IsNullOrEmpty())
        throw new ArgumentNullException(nameof (value));
      string claimsIssuer = this.GetClaimsIssuer();
      identity.AddClaim(new Claim(claimType, value ?? string.Empty, "http://www.w3.org/2001/XMLSchema#string", claimsIssuer, claimsIssuer));
    }

    public virtual void ParseSitefinityClaims(
      IEnumerable<Claim> claims,
      out string tokenId,
      out Guid userId,
      out string name,
      out string membership,
      out DateTime issueDate,
      out DateTime lastLoginDate,
      out IEnumerable<RoleInfo> roles,
      out bool isUnrestricted,
      out bool isBackendUser)
    {
      this.ParseSitefinityClaims(claims, out tokenId, out userId, out name, out membership, out issueDate, out lastLoginDate, out roles, out isUnrestricted, out isBackendUser, out string _, out bool _);
    }

    public virtual void ParseSitefinityClaims(
      IEnumerable<Claim> claims,
      out string tokenId,
      out Guid userId,
      out string name,
      out string membership,
      out DateTime issueDate,
      out DateTime lastLoginDate,
      out IEnumerable<RoleInfo> roles,
      out bool isUnrestricted,
      out bool isBackendUser,
      out string stsType)
    {
      this.ParseSitefinityClaims(claims, out tokenId, out userId, out name, out membership, out issueDate, out lastLoginDate, out roles, out isUnrestricted, out isBackendUser, out stsType, out bool _);
    }

    public virtual void ParseSitefinityClaims(
      IEnumerable<Claim> claims,
      out string tokenId,
      out Guid userId,
      out string name,
      out string membership,
      out DateTime issueDate,
      out DateTime lastLoginDate,
      out IEnumerable<RoleInfo> roles,
      out bool isUnrestricted,
      out bool isBackendUser,
      out string stsType,
      out bool isExternalUser)
    {
      if (claims == null)
        throw new ArgumentNullException(nameof (claims));
      tokenId = string.Empty;
      userId = Guid.Empty;
      name = (string) null;
      membership = (string) null;
      issueDate = DateTime.MinValue;
      lastLoginDate = DateTime.MinValue;
      isUnrestricted = false;
      isBackendUser = false;
      stsType = (string) null;
      isExternalUser = false;
      List<RoleInfo> roleInfoList = new List<RoleInfo>();
      Guid id = this.GetSecurityConfig().ApplicationRoles["BackendUsers"].Id;
      string membershipProvider = this.GetMembershipProvider(claims, false);
      Claim claim1 = claims.FirstOrDefault<Claim>((Func<Claim, bool>) (c => c.Type == "http://schemas.sitefinity.com/ws/2011/06/identity/claims/userid"));
      foreach (Claim claim2 in claims)
      {
        switch (claim2.Type)
        {
          case "http://schemas.sitefinity.com/ws/2011/06/identity/claims/domain":
            membership = claim2.Value;
            continue;
          case "http://schemas.sitefinity.com/ws/2011/06/identity/claims/isexternaluser":
            isExternalUser = true;
            continue;
          case "http://schemas.sitefinity.com/ws/2011/06/identity/claims/issuedate":
            issueDate = SecurityManager.GetUtcDate(claim2.Value);
            continue;
          case "http://schemas.sitefinity.com/ws/2011/06/identity/claims/lastlogindate":
            lastLoginDate = SecurityManager.GetUtcDate(claim2.Value);
            continue;
          case "http://schemas.sitefinity.com/ws/2011/06/identity/claims/role":
            RoleInfo roleInfo = this.GetRoleInfo(claim2.Value);
            if (!isUnrestricted)
            {
              if (SecurityManager.IsAdministrativeRole(roleInfo.Id))
              {
                if (userId == Guid.Empty && claim1 != null)
                  userId = Guid.Parse(claim1.Value);
                isUnrestricted = SecurityManager.IsGlobalUserProvider(membershipProvider) || ((IEnumerable<string>) SecurityManager.SystemAccountIDs).Contains<string>(userId.ToString().ToUpperInvariant());
                isBackendUser = true;
              }
              else if (roleInfo.Id == id)
                isBackendUser = true;
            }
            roleInfoList.Add(roleInfo);
            continue;
          case "http://schemas.sitefinity.com/ws/2011/06/identity/claims/ststype":
            stsType = claim2.Value;
            continue;
          case "http://schemas.sitefinity.com/ws/2011/06/identity/claims/tokenid":
            tokenId = claim2.Value;
            continue;
          case "http://schemas.sitefinity.com/ws/2011/06/identity/claims/userid":
            userId = Guid.Parse(claim2.Value);
            continue;
          case "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name":
            name = claim2.Value;
            continue;
          default:
            continue;
        }
      }
      roles = (IEnumerable<RoleInfo>) roleInfoList;
    }

    private SecurityConfig GetSecurityConfig() => Config.Get<SecurityConfig>();

    public virtual SimpleWebToken BuildSimpleWebToken(
      ClaimsPrincipal principal,
      string realm)
    {
      if (principal == null)
        throw new ArgumentNullException(nameof (principal));
      if (realm.IsNullOrEmpty())
        realm = ClaimsManager.CurrentAuthenticationModule.GetRealm();
      RequestSecurityToken requestSecurityToken = new RequestSecurityToken();
      requestSecurityToken.AppliesTo = new EndpointReference(realm);
      requestSecurityToken.RequestType = "http://schemas.microsoft.com/idfx/requesttype/issue";
      requestSecurityToken.TokenType = "http://schemas.microsoft.com/ws/2010/07/identitymodel/tokens/SWT";
      RequestSecurityToken request = requestSecurityToken;
      return (SimpleWebToken) new SWTIssuer((SecurityTokenServiceConfiguration) new SWTIssuerConfiguration()).Issue(principal, request).RequestedSecurityToken.SecurityToken;
    }

    public virtual void SendUnauthorizedResponse(HttpContext context) => this.SendUnauthorizedResponse((HttpContextBase) new HttpContextWrapper(context));

    public virtual void SendUnauthorizedResponse(HttpContextBase context)
    {
      HttpResponseBase response = context.Response;
      response.Clear();
      response.StatusCode = 401;
      context.ApplicationInstance.CompleteRequest();
    }

    public virtual void SendErrorResponse(
      HttpContextBase context,
      HttpStatusCode statusCode,
      UserLoggingReason loggngError,
      bool endRequest = false)
    {
      string name = System.Enum.GetName(typeof (UserLoggingReason), (object) loggngError);
      this.SendErrorResponse(context, statusCode, name, endRequest);
    }

    public virtual void SendErrorResponse(
      HttpContextBase context,
      HttpStatusCode statusCode,
      string errorMessage,
      bool endRequest = false)
    {
      string error = ObjectFactory.Resolve<IHtmlSanitizer>().Sanitize(errorMessage);
      HttpResponseBase response = context.Response;
      response.StatusCode = (int) statusCode;
      response.AddHeader("X-Authentication-Error", error);
      string errorPageContent = this.GetErrorPageContent(error);
      response.ClearContent();
      response.ContentType = "text/html";
      response.AppendHeader("Realm", ClaimsManager.CurrentAuthenticationModule.GetRealm(context));
      response.AppendHeader("Issuer", ClaimsManager.CurrentAuthenticationModule.GetIssuer(context));
      response.AppendHeader("SF-AuthProtocol", ClaimsManager.CurrentAuthenticationModule.AuthenticationProtocol);
      response.Write(errorPageContent);
      if (endRequest)
        response.End();
      else
        context.ApplicationInstance.CompleteRequest();
    }

    protected virtual string GetErrorPageContent(string error)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendFormat("<html><body><h1>{0}</h1></body></html>", (object) error);
      return stringBuilder.ToString();
    }

    public virtual void RenderLoginForm(HttpContextBase context, string errorMessage)
    {
      string sanitizedErrorMessage = ObjectFactory.Resolve<IHtmlSanitizer>().Sanitize(errorMessage);
      PageManager.RenderPage(context, SiteInitializer.LoginFormPageId, (System.Action<Page>) (p => p.Items[(object) "sf_message"] = (object) sanitizedErrorMessage));
      if (string.IsNullOrEmpty(sanitizedErrorMessage))
        return;
      context.Response.AddHeader("X-Authentication-Error", sanitizedErrorMessage);
    }

    internal virtual RoleInfo GetRoleInfo(string value)
    {
      string[] strArray = value.Split(';');
      return new RoleInfo()
      {
        Id = Guid.Parse(strArray[0]),
        Name = strArray[1],
        Provider = strArray[2]
      };
    }

    protected virtual bool TryGetValue(
      IEnumerable<Claim> claims,
      string claimType,
      out string value)
    {
      if (claims == null)
        throw new ArgumentNullException(nameof (claims));
      Claim claim = claims.SingleOrDefault<Claim>((Func<Claim, bool>) (c => c.Type == claimType));
      if (claim != null)
      {
        value = claim.Value;
        return true;
      }
      value = (string) null;
      return false;
    }

    protected virtual IEnumerable<string> GetValues(
      IEnumerable<Claim> claims,
      string claimType)
    {
      if (claims == null)
        throw new ArgumentNullException(nameof (claims));
      return claims.Where<Claim>((Func<Claim, bool>) (c => c.Type == claimType)).Select<Claim, string>((Func<Claim, string>) (c => c.Value));
    }

    protected virtual ArgumentException GetMissingClaimExcepton(string claimType) => new ArgumentException("The required claim \"{0}\" is missing.".Arrange((object) claimType));

    private string GetClaimsIssuer()
    {
      ISitefinityClaimsAuthenticationModule authenticationModule = ClaimsManager.CurrentAuthenticationModule;
      return authenticationModule != null ? authenticationModule.GetIssuer() : "http://localhost";
    }
  }
}
