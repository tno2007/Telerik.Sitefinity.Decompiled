// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.SitefinityIdentity
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Security
{
  /// <summary>
  /// Represents a user identity authenticated by Sitefinity
  /// </summary>
  [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "The overriden properties have no setters and the properties are used in FormsSitefinityIdentity.")]
  public class SitefinityIdentity : ClaimsIdentity
  {
    /// <summary>The authentication type.</summary>
    protected string authenticationType;
    /// <summary>Whether the identity is authenticated or not.</summary>
    protected bool isAuthenticated;
    private SitefinityIdentity.BackendUserInfo userInfo;
    private string name;
    private IList<RoleInfo> roles;

    /// <inheritdoc />
    public override string AuthenticationType => this.authenticationType;

    /// <inheritdoc />
    public override bool IsAuthenticated => this.isAuthenticated;

    /// <inheritdoc />
    public override string Name => this.name;

    /// <summary>Gets the ID of the user.</summary>
    /// <value>The ID.</value>
    public virtual Guid UserId { get; private set; }

    /// <summary>Gets the roles of the current identity.</summary>
    /// <value>The roles.</value>
    public virtual IEnumerable<RoleInfo> Roles => (IEnumerable<RoleInfo>) this.roles;

    /// <summary>Gets the membership data provider name.</summary>
    /// <value>The provider name.</value>
    public virtual string MembershipProvider { get; private set; }

    /// <summary>Gets the issue date.</summary>
    public DateTime IssueDate { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the identity has unrestricted access.
    /// </summary>
    public bool IsUnrestricted => this.UserInfo.IsUnrestricted;

    /// <summary>
    /// Gets a value indicating whether the identity has access to the system across defined user group's sites
    /// </summary>
    internal bool IsAdmin => this.UserInfo.IsAdmin;

    /// <summary>
    /// Gets a value indicating whether the identity has global access to the system across all sites, depending on permissions.
    /// </summary>
    internal bool IsGlobalUser => this.UserInfo.IsGlobalUser;

    /// <summary>
    /// Gets a value indicating whether the identity is a backend user.
    /// </summary>
    public bool IsBackendUser => this.UserInfo.IsBackendUser;

    /// <summary>
    /// Gets a value indicating whether the identity is an external user.
    /// </summary>
    public bool IsExternalUser { get; private set; }

    /// <summary>Gets the STS type.</summary>
    public string StsType { get; private set; }

    /// <summary>Gets the last log-in date.</summary>
    public virtual DateTime LastLoginDate { get; private set; }

    /// <summary>Gets the TokenId.</summary>
    public string TokenId { get; private set; }

    /// <summary>Gets the BootstrapToken</summary>
    public SecurityToken BootstrapToken => !(this.BootstrapContext is BootstrapContext bootstrapContext) ? (SecurityToken) null : bootstrapContext.SecurityToken;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.SitefinityIdentity" /> class.
    /// </summary>
    protected SitefinityIdentity()
      : base((string) null, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", "http://schemas.sitefinity.com/ws/2011/06/identity/claims/role")
    {
      this.TokenId = string.Empty;
      this.roles = (IList<RoleInfo>) new List<RoleInfo>();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.SitefinityIdentity" /> class.
    /// </summary>
    /// <param name="user">The user</param>
    /// <param name="includeMappedExternalRoles">Whether to include mapped external roles.</param>
    /// <exception cref="T:System.ArgumentNullException">If the <paramref name="user" /> is null.</exception>
    public SitefinityIdentity(User user, bool includeMappedExternalRoles)
      : this(user)
    {
      if (!includeMappedExternalRoles)
        return;
      this.GetMappedRoles(user);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.SitefinityIdentity" /> class.
    /// </summary>
    /// <param name="user">The user</param>
    /// <param name="newLoginDate">New log-in date</param>
    /// <param name="authenticationType">The authentication type</param>
    /// <exception cref="T:System.ArgumentNullException">If the <paramref name="user" /> is null.</exception>
    public SitefinityIdentity(User user, bool newLoginDate = false, string authenticationType = "")
      : this()
    {
      if (user == null)
        throw new ArgumentNullException(nameof (user));
      this.isAuthenticated = true;
      this.authenticationType = authenticationType;
      ClaimsManager.SetUserId((ClaimsIdentity) this, user.Id);
      ClaimsManager.SetName((ClaimsIdentity) this, user.UserName);
      ClaimsManager.SetMembershipProvider((ClaimsIdentity) this, user.ProviderName);
      ClaimsManager.SetLastLoginDate((ClaimsIdentity) this, newLoginDate ? DateTime.UtcNow : user.LastLoginDate);
      ClaimsManager.SetSitefinityRoles((ClaimsIdentity) this, this.GetRoles());
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.SitefinityIdentity" /> class.
    /// </summary>
    /// <param name="identity">The claims identity</param>
    /// <param name="authenticationType">The authentication type. If null or empty, will use the one from the identity.</param>
    /// <exception cref="T:System.ArgumentNullException">If the <paramref name="identity" /> is null.</exception>
    public SitefinityIdentity(ClaimsIdentity identity, string authenticationType = null)
      : this()
    {
      if (identity == null)
        throw new ArgumentNullException(nameof (identity));
      if (identity.IsAuthenticated)
      {
        this.isAuthenticated = true;
        this.authenticationType = authenticationType.IsNullOrEmpty() ? identity.AuthenticationType : authenticationType;
        this.Label = identity.Label;
        this.AddClaims(identity.Claims);
      }
      else
        this.SetAnonymous();
    }

    /// <summary>
    /// Gets an instance of <see cref="T:Telerik.Sitefinity.Security.SitefinityIdentity" /> representing an Anonymous user.
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Security.SitefinityIdentity" /> class.</returns>
    public static SitefinityIdentity GetAnonymous()
    {
      SitefinityIdentity anonymous = new SitefinityIdentity();
      anonymous.SetAnonymous();
      return anonymous;
    }

    /// <summary>Copies the identity</summary>
    /// <returns>A new instance of <see cref="T:Telerik.Sitefinity.Security.SitefinityIdentity" /> with the same properties.</returns>
    public override ClaimsIdentity Clone() => (ClaimsIdentity) new SitefinityIdentity((ClaimsIdentity) this);

    /// <summary>
    /// Adds the given claim to the identity.
    /// <para>Parses the claim value and caches it in the corresponding property.</para>
    /// </summary>
    /// <param name="claim">The claim to add.</param>
    /// <exception cref="T:System.ArgumentNullException">If the <paramref name="claim" /> argument is null.</exception>
    public override void AddClaim(Claim claim)
    {
      base.AddClaim(claim);
      string type = claim.Type;
      // ISSUE: reference to a compiler-generated method
      switch (\u003CPrivateImplementationDetails\u003E.ComputeStringHash(type))
      {
        case 917292843:
          if (!(type == "http://schemas.sitefinity.com/ws/2011/06/identity/claims/ststype"))
            break;
          this.StsType = claim.Value;
          break;
        case 1049104815:
          if (!(type == "http://schemas.sitefinity.com/ws/2011/06/identity/claims/tokenid"))
            break;
          this.TokenId = claim.Value;
          break;
        case 1260171917:
          if (!(type == "http://schemas.sitefinity.com/ws/2011/06/identity/claims/isexternaluser"))
            break;
          this.IsExternalUser = true;
          break;
        case 1419742362:
          if (!(type == "http://schemas.sitefinity.com/ws/2011/06/identity/claims/lastlogindate"))
            break;
          this.LastLoginDate = SecurityManager.GetUtcDate(claim.Value);
          break;
        case 1461547748:
          if (!(type == "http://schemas.sitefinity.com/ws/2011/06/identity/claims/issuedate"))
            break;
          this.IssueDate = SecurityManager.GetUtcDate(claim.Value);
          break;
        case 2554998159:
          if (!(type == "http://schemas.sitefinity.com/ws/2011/06/identity/claims/userid"))
            break;
          this.UserId = Guid.Parse(claim.Value);
          break;
        case 2655309417:
          if (!(type == "http://schemas.sitefinity.com/ws/2011/06/identity/claims/role"))
            break;
          this.roles.Add(ClaimsManager.GetSitefinityRoleInfo(claim));
          this.userInfo = (SitefinityIdentity.BackendUserInfo) null;
          break;
        case 3628872289:
          if (!(type == "http://schemas.sitefinity.com/ws/2011/06/identity/claims/domain"))
            break;
          this.MembershipProvider = claim.Value;
          break;
        case 3795800007:
          if (!(type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"))
            break;
          this.name = claim.Value;
          break;
      }
    }

    internal void SetAuthenticationType(string authenticationType) => this.authenticationType = authenticationType;

    /// <summary>Gets the identity roles.</summary>
    /// <returns>Returns a roles collection</returns>
    protected virtual IEnumerable<RoleInfo> GetRoles() => RoleManager.GetAllRolesOfUser(this.UserId);

    /// <summary>Gets the mapped external identity roles.</summary>
    /// <param name="user">The user needed to identify external claims.</param>
    protected virtual void GetMappedRoles(User user)
    {
      if (!ObjectFactory.IsTypeRegistered<IExternalClaimsResolver>())
        return;
      ObjectFactory.Resolve<IExternalClaimsResolver>().AddExternalClaims((ClaimsIdentity) this, user);
    }

    /// <summary>Sets anonymous user</summary>
    protected void SetAnonymous()
    {
      this.isAuthenticated = false;
      this.authenticationType = (string) null;
      this.name = "Anonymous";
      ClaimsManager.SetSitefinityRoles((ClaimsIdentity) this, this.GetAnonymousRoles());
    }

    private IEnumerable<RoleInfo> GetAnonymousRoles() => (IEnumerable<RoleInfo>) new RoleInfo[2]
    {
      SecurityManager.EveryoneRole,
      SecurityManager.AnonymousRole
    };

    private SitefinityIdentity.BackendUserInfo UserInfo
    {
      get
      {
        if (this.userInfo == null)
          this.userInfo = new SitefinityIdentity.BackendUserInfo(this);
        return this.userInfo;
      }
    }

    private class BackendUserInfo
    {
      public BackendUserInfo(SitefinityIdentity identity)
      {
        this.IsGlobalUser = SecurityManager.IsGlobalUserProvider(identity.MembershipProvider);
        if (identity.Roles.Any<RoleInfo>((Func<RoleInfo, bool>) (r => SecurityManager.IsAdministrativeRole(r.Id))))
        {
          if (!identity.MembershipProvider.IsNullOrEmpty() && this.IsGlobalUser || ((IEnumerable<string>) SecurityManager.SystemAccountIDs).Contains<string>(identity.UserId.ToString().ToUpperInvariant()))
            this.IsUnrestricted = true;
          this.IsAdmin = true;
          this.IsBackendUser = true;
        }
        else
        {
          if (!identity.Roles.Any<RoleInfo>((Func<RoleInfo, bool>) (r => r.Id == SecurityManager.BackEndUsersRole.Id)))
            return;
          this.IsBackendUser = true;
        }
      }

      public bool IsUnrestricted { get; private set; }

      public bool IsBackendUser { get; private set; }

      public bool IsAdmin { get; private set; }

      public bool IsGlobalUser { get; private set; }
    }
  }
}
