// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.SecurityManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Abstractions.TemporaryStorage;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Configuration.Environment;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Licensing;
using Telerik.Sitefinity.Licensing.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Cryptography;
using Telerik.Sitefinity.Security.Data;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Events;
using Telerik.Sitefinity.Services.Notifications;
using Telerik.Sitefinity.Utilities;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Events;
using Telerik.Sitefinity.Web.UrlEvaluation;

namespace Telerik.Sitefinity.Security
{
  /// <summary>
  /// Represents an intermediary between security objects and security data.
  /// </summary>
  public class SecurityManager : ManagerBase<SecurityDataProvider>
  {
    private static readonly object userActivitySyncObject = new object();
    private static readonly ConcurrentProperty<Dictionary<Guid, string>> adminRoles = new ConcurrentProperty<Dictionary<Guid, string>>(new Func<Dictionary<Guid, string>>(SecurityManager.BuildAdminRoles));
    private static readonly RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
    private ConfigElementDictionary<string, DataProviderSettings> providerSettings;
    private static ISecuritySettings currentSettings;
    internal static bool filterQueriesByViewPermissions;
    private static Telerik.Sitefinity.Security.Configuration.AuthenticationMode? authenticationMode = new Telerik.Sitefinity.Security.Configuration.AuthenticationMode?();
    private static bool? allowSeparateUsersPerSite = new bool?();
    private static ConcurrentProperty<string[]> globalUserProviders = new ConcurrentProperty<string[]>(new Func<string[]>(SecurityManager.GetGlobalUserProviders));
    internal const string AppSecurityRootName = "ApplicationSecurityRoot";
    [Obsolete("Use SecurityConstants.ApplicationRolesProviderName instead.")]
    public const string ApplicationRolesProviderName = "AppRoles";
    /// <summary>The name of the build-in administrative role.</summary>
    public const string AdminRoleName = "Administrators";
    /// <summary>Backend users role name</summary>
    public const string BackendUsersRoleName = "BackendUsers";
    private const string authTranName = "Authenticate";
    internal static string[] SystemAccountIDs = new string[2]
    {
      "2638176E-8686-4A95-B004-2463D20369FF",
      "B2F8CDD5-3B08-4770-9DA8-09257909625C"
    };
    public const string DateTimeUniversalFormat = "u";
    public const string DateTimeFormatStringSeconds = "yyyyMMddHHmmss";
    private const string SFTokenIdCookieName = "SF-TokenId";
    public const string TokenIdCookiePath = "/";
    internal const string SystemAuthorizationHeaderKey = "SF-Sys-Message";
    /// <summary>Represents "ReturnUrl" query string parameter</summary>
    private const string ReturnUrlConst = "ReturnUrl";
    internal const string LoginEventSource = "Sitefinity Login - RP";
    internal const string LogoutEventSource = "Sitefinity Logout - RP";
    internal const string AuthenticationKeyUrlParamName = "sf-auth";
    internal const char AuthenticationKeyCharSeparator = ',';
    internal const int KeyValidDurationInHours = 1;
    private static object recordLock = new object();

    /// <summary>
    /// Initializes the <see cref="T:Telerik.Sitefinity.Security.SecurityManager" /> class.
    /// </summary>
    static SecurityManager()
    {
      DateTime dateTime = DateTime.Now;
      dateTime = dateTime.AddYears(-1);
      SecurityManager.LastPermissionChange = dateTime.ToUniversalTime();
      SecurityManager.currentSettings = (ISecuritySettings) new SecuritySettings();
    }

    /// <summary>
    /// Initializes a new instance of <see cref="T:Telerik.Sitefinity.Security.SecurityManager" /> class with the default provider.
    /// </summary>
    public SecurityManager()
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="T:Telerik.Sitefinity.Security.SecurityManager" /> class and sets the specified provider.
    /// </summary>
    /// <param name="providerName">
    /// The name of the provider. If empty string or null the default provider is set.
    /// </param>
    public SecurityManager(string providerName)
      : base(providerName)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.SecurityManager" /> class.
    /// </summary>
    /// <param name="providerName">
    /// The name of the provider. If empty string or null the default provider is set
    /// </param>
    /// <param name="transactionName">
    /// The name of a distributed transaction. If empty string or null this manager will use separate transaction.
    /// </param>
    public SecurityManager(string providerName, string transactionName)
      : base(providerName, transactionName)
    {
    }

    /// <summary>Creates new permission.</summary>
    /// <param name="permissionSet">The permission set name.</param>
    /// <param name="objectId">The secured object identifier.</param>
    /// <param name="principalId">The principal identifier.</param>
    /// <returns></returns>
    public new virtual Telerik.Sitefinity.Security.Model.Permission CreatePermission(
      string permissionSet,
      Guid objectId,
      Guid principalId)
    {
      return this.Provider.CreatePermission(permissionSet, objectId, principalId);
    }

    /// <summary>Gets a query for permissions.</summary>
    /// <returns></returns>
    public new virtual IQueryable<Telerik.Sitefinity.Security.Model.Permission> GetPermissions() => this.Provider.GetPermissions();

    /// <summary>Gets the specified permission.</summary>
    /// <param name="permissionSet">The permission set name.</param>
    /// <param name="objectId">The secured object identifier.</param>
    /// <param name="principalId">The principal identifier.</param>
    /// <returns></returns>
    public new Telerik.Sitefinity.Security.Model.Permission GetPermission(
      string permissionSet,
      Guid objectId,
      Guid principalId)
    {
      return this.Provider.GetPermission(permissionSet, objectId, principalId);
    }

    /// <summary>Deletes the permission.</summary>
    /// <param name="permission">The permission.</param>
    public new virtual void DeletePermission(Telerik.Sitefinity.Security.Model.Permission permission) => this.Provider.DeletePermission(permission);

    /// <summary>Gets the security root with the provided key.</summary>
    /// <param name="key">The key of the security root to retrieve.</param>
    /// <returns></returns>
    public virtual SecurityRoot GetSecurityRoot(string key) => this.Provider.GetSecurityRoot(key);

    /// <summary>Gets the security root with the specified ID.</summary>
    /// <param name="id">The ID of the root.</param>
    public virtual SecurityRoot GetSecurityRoot(Guid id) => this.Provider.GetSecurityRoot(id);

    /// <summary>Gets a query for security roots.</summary>
    /// <returns></returns>
    public virtual IQueryable<SecurityRoot> GetSecurityRoots() => this.Provider.GetSecurityRoots();

    /// <summary>Creates new security root with the specified key.</summary>
    /// <param name="key">The key.</param>
    /// <returns></returns>
    public virtual SecurityRoot CreateSecurityRoot(string key) => this.Provider.CreateSecurityRoot(key);

    /// <summary>
    /// Creates a security root by secifying a key and supported permission sets
    /// </summary>
    /// <param name="key">Security root key name</param>
    /// <param name="supportedPermissionSets">Permission sets that should be supported by the security root</param>
    /// <returns>Security root that has a proper <paramref name="key" /> and <paramref name="supportedPermissionSets" /> </returns>
    public virtual SecurityRoot CreateSecurityRoot(
      string key,
      params string[] supportedPermissionSets)
    {
      return this.Provider.CreateSecurityRoot(key, supportedPermissionSets);
    }

    /// <summary>Deletes the security root.</summary>
    /// <param name="root">The root.</param>
    public virtual void DeleteSecurityRoot(SecurityRoot root) => this.Provider.DeleteSecurityRoot(root);

    /// <summary>
    /// Expires the previously generated security token for resetting the user password.
    /// </summary>
    /// <param name="securityParams">The security context for which to expire the token. Usually query string parameters received from the reset password request.</param>
    public void ExpireResetPasswordToken(NameValueCollection securityParams)
    {
      ITemporaryStorage temporaryStorage = ObjectFactory.Resolve<ITemporaryStorage>();
      string fromSecurityParams = SecurityManager.GetUserValidationTokenFromSecurityParams(securityParams);
      if (string.IsNullOrWhiteSpace(fromSecurityParams))
        return;
      temporaryStorage.Remove(fromSecurityParams);
    }

    /// <summary>
    /// Gets the user, from the passed security parameters, that will be used for password recovery / reset.
    /// </summary>
    /// <param name="securityParams">The security parameters.</param>
    /// <returns>
    /// An instance of <see cref="T:Telerik.Sitefinity.Security.SitefinityIdentity" />.
    /// </returns>
    public virtual SitefinityIdentity GetPasswordRecoveryUser(
      NameValueCollection securityParams = null)
    {
      if (securityParams == null)
        securityParams = SystemManager.CurrentHttpContext.Request.QueryString;
      string fromSecurityParams = SecurityManager.GetUserValidationTokenFromSecurityParams(securityParams);
      if (!string.IsNullOrWhiteSpace(fromSecurityParams))
      {
        if (ObjectFactory.Resolve<ITemporaryStorage>().Get(fromSecurityParams) == null)
          return (SitefinityIdentity) null;
        string[] strArray = SecurityManager.DecryptData(fromSecurityParams).Split(',');
        if (strArray.Length == 3)
        {
          string providerName = strArray[0];
          string input = strArray[1];
          string s = strArray[2];
          DateTime result1;
          Guid result2;
          if (!string.IsNullOrWhiteSpace(s) && DateTime.TryParse(s, out result1) && result1.AddHours(1.0) > DateTime.UtcNow && Guid.TryParse(input, out result2))
            return new SitefinityIdentity(UserManager.GetManager(providerName).GetUser(result2));
        }
      }
      return (SitefinityIdentity) null;
    }

    /// <summary>
    /// Gets the name of the default provider for this manager.
    /// </summary>
    protected internal override GetDefaultProvider DefaultProviderDelegate => (GetDefaultProvider) (() => Config.Get<SecurityConfig>().DefaultSecurityProvider);

    /// <summary>
    /// Gets the name of the module to which this manager belongs.
    /// </summary>
    public override string ModuleName => "Security";

    /// <summary>Gets all provider settings.</summary>
    protected internal override ConfigElementDictionary<string, DataProviderSettings> ProvidersSettings
    {
      get
      {
        if (this.providerSettings == null)
          this.providerSettings = Config.Get<SecurityConfig>().SecurityProviders;
        return this.providerSettings;
      }
    }

    internal static ISecuritySettings CurrentSettings
    {
      get => SecurityManager.currentSettings;
      set => SecurityManager.currentSettings = value;
    }

    /// <summary>Gets the cached security configuration.</summary>
    internal static Telerik.Sitefinity.Security.Configuration.AuthenticationMode AuthenticationMode
    {
      get
      {
        if (!SecurityManager.authenticationMode.HasValue)
        {
          if (!Bootstrapper.IsReady)
            return Telerik.Sitefinity.Security.Configuration.AuthenticationMode.Claims;
          SecurityManager.authenticationMode = new Telerik.Sitefinity.Security.Configuration.AuthenticationMode?(Config.Get<SecurityConfig>().AuthenticationMode);
        }
        return SecurityManager.authenticationMode.Value;
      }
    }

    /// <summary>Gets the cached security configuration.</summary>
    internal static bool AllowSeparateUsersPerSite
    {
      get
      {
        if (!SecurityManager.allowSeparateUsersPerSite.HasValue)
        {
          SecurityManager.allowSeparateUsersPerSite = new bool?(false);
          if (LicenseState.Current.LicenseInfo.LicenseType == "PU")
            SecurityManager.allowSeparateUsersPerSite = new bool?(Config.Get<SecurityConfig>().UsersPerSiteSettings.AllowSeparateUsersPerSite);
        }
        return SecurityManager.allowSeparateUsersPerSite.Value;
      }
    }

    /// <summary>
    /// Ensures that the non global admin has access to the site.
    /// </summary>
    internal static void EnsureNonGlobalAdminHasSiteAccess(Guid siteId)
    {
      if (SecurityManager.AllowSeparateUsersPerSite && !ClaimsManager.GetCurrentIdentity().IsGlobalUser && (siteId == Guid.Empty || !SystemManager.CurrentContext.GetSites(true).Any<ISite>((Func<ISite, bool>) (s => s.Id == siteId))))
        throw new UnauthorizedAccessException();
    }

    /// <summary>Get formatted user name (e.g. FirstName LastName)</summary>
    /// <param name="userID">User ID</param>
    /// <returns>Formatted user name.</returns>
    public static string GetFormattedUserName(Guid userID)
    {
      User user = SecurityManager.GetUser(userID);
      return user == null ? Res.Get<Labels>().Unknown : SecurityManager.GetFormattedUserName(user);
    }

    /// <summary>Get formatted user name (e.g. FistName LastName);</summary>
    /// <param name="user">User whose FirstName, LastName and UserName to format into a single string.</param>
    /// <returns>Formatted user name.</returns>
    /// <exception cref="T:System.ArgumentNullException">
    ///     When <paramref name="user" /> is <c>null</c>.
    /// </exception>
    public static string GetFormattedUserName(User user) => user != null ? UserProfilesHelper.GetUserDisplayName(user.Id) : throw new ArgumentNullException(nameof (user));

    /// <summary>Returns the cookie name for the claims token</summary>
    public static string TokenIdCookieName => SitefinityTokenIdCookieHelper.GetSitefinityCookieName("SF-TokenId", SystemManager.CurrentHttpContext);

    /// <summary>
    /// Gets or sets the last permission change. The time is local.
    /// </summary>
    public static DateTime LastPermissionChange { get; private set; }

    /// <summary>Gets the owner role.</summary>
    /// <value>The owner role.</value>
    public static RoleInfo OwnerRole => SecurityManager.GetAppRoleOrDefault("Owner");

    public static RoleInfo AnonymousRole => SecurityManager.GetAppRoleOrDefault("Anonymous");

    public static RoleInfo AuthenticatedRole => SecurityManager.GetAppRoleOrDefault("Authenticated");

    /// <summary>Gets the application roles configuration elements.</summary>
    /// <value>The application roles configuration elements.</value>
    public static ConfigElementDictionary<string, ApplicationRole> ApplicationRoles => Config.Get<SecurityConfig>().ApplicationRoles;

    /// <summary>Returns the name of the current user</summary>
    public static string GetCurrentUserName()
    {
      SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
      return currentIdentity != null ? currentIdentity.Name : string.Empty;
    }

    /// <summary>Gets the current user ID.</summary>
    /// <returns>The ID of the current user.</returns>
    public static Guid GetCurrentUserId()
    {
      SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
      return currentIdentity != null ? currentIdentity.UserId : Guid.Empty;
    }

    /// <summary>
    /// Determines whether the current request is made by a user member of BackendUsers role or an administrator.
    /// </summary>
    /// <returns>
    /// 	<c>true</c> if [is backend user]; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsBackendUser()
    {
      SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
      return currentIdentity != null && currentIdentity.IsBackendUser;
    }

    /// <summary>Gets the user id.</summary>
    /// <param name="fullName">
    /// The full user name.
    /// The full user name includes the membership provider name and the user name separated by backslash.
    /// Example: (Backend Users\john)
    /// </param>
    /// <returns>The user id.</returns>
    public static Guid GetUserId(string fullName)
    {
      string[] strArray = fullName.Split(new char[1]{ '\\' }, StringSplitOptions.RemoveEmptyEntries);
      return strArray.Length == 2 ? SecurityManager.GetUserId(strArray[0], strArray[1]) : throw new ArgumentException("Invalid full name format.", nameof (fullName));
    }

    /// <summary>Gets the user id.</summary>
    /// <param name="membershipProviderName">The name of the membership provider.</param>
    /// <param name="userName">The user name.</param>
    /// <returns>The user ID.</returns>
    public static Guid GetUserId(string membershipProviderName, string userName)
    {
      User user = UserManager.GetManager(membershipProviderName).GetUser(userName);
      return user != null ? user.Id : Guid.Empty;
    }

    /// <summary>
    /// Gets the user with the specified ID.
    /// This method searches all membership providers.
    /// Returns null if id is empty or if user cannot be found.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <param name="transactionName">The name of the transaction.</param>
    /// <returns></returns>
    public static User GetUser(Guid id, string transactionName = null)
    {
      string provider = (string) null;
      return SecurityManager.GetUser(id, transactionName, out provider);
    }

    /// <summary>
    /// Gets the user with the specified ID.
    /// This method searches all membership providers and returns the name of the provider the user belongs to.
    /// Returns null if id is empty of if user cannot be found.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <param name="provider">The provider.</param>
    /// <returns>The membership user</returns>
    public static User GetUser(Guid id, string transactionName, out string provider)
    {
      if (id == Guid.Empty)
      {
        provider = (string) null;
        return (User) null;
      }
      foreach (string providerName in UserManager.GetManager().GetProviderNames(ProviderBindingOptions.NoFilter))
      {
        User user = UserManager.GetManager(providerName, transactionName).GetUser(id);
        if (user != null)
        {
          provider = providerName;
          return user;
        }
      }
      provider = (string) null;
      return (User) null;
    }

    /// <summary>
    /// Gets the role with the specified ID.
    /// This method searches all role providers.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <returns>The role</returns>
    public static Role GetRole(Guid id)
    {
      string provider = (string) null;
      return SecurityManager.GetRole(id, out provider);
    }

    /// <summary>
    /// Get the role with the specified ID. This searches configuration
    /// and all role providers.
    /// </summary>
    /// <param name="id">Role ID</param>
    /// <returns>Information about the role</returns>
    public static IRoleInfo GetRoleOrAppRole(Guid id) => (IRoleInfo) Config.Get<SecurityConfig>().ApplicationRoles.Values.SingleOrDefault<ApplicationRole>((Func<ApplicationRole, bool>) (r => r.Id == id)) ?? (IRoleInfo) SecurityManager.GetRole(id);

    /// <summary>
    /// Gets the role with the specified ID.
    /// This method searches all role providers and returns the name of the provider the role belongs to.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <param name="provider">The provider.</param>
    /// <returns>The role</returns>
    public static Role GetRole(Guid id, out string provider)
    {
      if (ManagerBase<RoleDataProvider>.StaticProvidersCollection != null)
      {
        foreach (RoleDataProvider staticProviders in (Collection<RoleDataProvider>) ManagerBase<RoleDataProvider>.StaticProvidersCollection)
        {
          Role role = staticProviders.GetRoles().Where<Role>((Expression<Func<Role, bool>>) (r => r.Id == id)).FirstOrDefault<Role>();
          if (role != null)
          {
            provider = staticProviders.Name;
            return role;
          }
        }
      }
      provider = (string) null;
      return (Role) null;
    }

    /// <summary>
    /// Gets the roles for the specified user ID.
    /// This method searches all role providers and returns the roles for the user ID.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <returns>List of roles</returns>
    internal static IQueryable<Role> GetRolesForUser(Guid id)
    {
      List<Role> source = new List<Role>();
      if (ManagerBase<RoleDataProvider>.StaticProvidersCollection != null)
      {
        foreach (RoleDataProvider staticProviders in (Collection<RoleDataProvider>) ManagerBase<RoleDataProvider>.StaticProvidersCollection)
        {
          IQueryable<Role> rolesForUser = staticProviders.GetRolesForUser(id);
          if (rolesForUser != null)
            source.AddRange((IEnumerable<Role>) rolesForUser);
        }
      }
      return source.AsQueryable<Role>();
    }

    /// <summary>Gets the role id.</summary>
    /// <param name="fullName">
    /// The full name of the role.
    /// The full name includes the role provider name and the role name separated by backslash.
    /// Example: (Backend Roles\Content Editors)
    /// </param>
    /// <returns>The role id.</returns>
    public static Guid GetRoleId(string fullName)
    {
      string[] strArray = fullName.Split(new char[1]{ '\\' }, StringSplitOptions.RemoveEmptyEntries);
      return strArray.Length == 2 ? SecurityManager.GetRoleId(strArray[0], strArray[1]) : throw new ArgumentException("Invalid full name format.", nameof (fullName));
    }

    /// <summary>Gets the role id.</summary>
    /// <param name="roleProviderName">Name of the role provider.</param>
    /// <param name="roleName">Name of the role.</param>
    /// <returns>The role id.</returns>
    public static Guid GetRoleId(string roleProviderName, string roleName)
    {
      if (roleProviderName == "AppRoles")
        return Config.Get<SecurityConfig>().ApplicationRoles[roleName].Id;
      return (RoleManager.GetManager(roleProviderName).GetRole(roleName) ?? throw new ApplicationException("Role '{0}' not found in provider '{1}'".Arrange((object) roleName, (object) roleProviderName))).Id;
    }

    /// <summary>Gets the name of the principal.</summary>
    /// <param name="id">The id.</param>
    /// <returns>The name of the principal.</returns>
    public static string GetPrincipalName(Guid id)
    {
      string principalName = string.Empty;
      if (SecurityManager.IsPrincipalUser(id))
      {
        MembershipUser user = (MembershipUser) SecurityManager.GetUser(id);
        if (user != null)
          principalName = user.UserName;
      }
      else if (SecurityManager.IsPrincipalRole(id))
      {
        ApplicationRole applicationRole = SecurityManager.ApplicationRoles.Values.Where<ApplicationRole>((Func<ApplicationRole, bool>) (approle => approle.Id == id)).FirstOrDefault<ApplicationRole>();
        if (applicationRole != null)
        {
          principalName = Res.Get(applicationRole.ResourceClassId, applicationRole.Name);
        }
        else
        {
          Role role = SecurityManager.GetRole(id);
          if (role != null)
            principalName = role.Name;
        }
      }
      return principalName;
    }

    /// <summary>Check if a specific principal represents a user</summary>
    /// <param name="id">id of the principal to check</param>
    /// <returns>true if the principal represents a user, false otherwise</returns>
    public static bool IsPrincipalUser(Guid id) => UserManager.UserExistsInAnyProvider(id);

    /// <summary>Check if a specific principal represents a role</summary>
    /// <param name="id">id of the principal to check</param>
    /// <returns>true if the principal represents a role, false otherwise</returns>
    public static bool IsPrincipalRole(Guid id) => RoleManager.RoleExistsInAnyProvider(id);

    /// <summary>
    /// Determines whether the specified role is an administrative role.
    /// </summary>
    /// <param name="roleId">The role ID.</param>
    /// <returns>
    /// 	<c>true</c> if the role is administrative; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsAdministrativeRole(Guid roleId)
    {
      if (roleId == SecurityManager.AdminRole.Id)
        return true;
      return Bootstrapper.IsReady && SecurityManager.adminRoles.Value.ContainsKey(roleId);
    }

    /// <summary>Gets whether the user is currently online.</summary>
    /// <param name="providerName">The provider name.</param>
    /// <param name="user">The User to check.</param>
    /// <returns> true: if the specified user is is currently online; otherwise, false.</returns>
    public static bool IsUserOnline(string providerName, User user)
    {
      UserActivity userActivity = UserActivityManager.GetManager().GetUserActivity(user.Id, providerName);
      return userActivity != null && SecurityManager.IsUserOnline(providerName, user, userActivity);
    }

    /// <summary>Shows if the user is "Online".</summary>
    internal static bool IsUserOnline(string providerName, User user, UserActivity userActivity)
    {
      if (userActivity == null)
        return user.IsOnline;
      if (!userActivity.IsLoggedIn)
        return false;
      DateTime lastActivityDate = user.LastActivityDate;
      if (userActivity.LastActivityDate > DateTime.MinValue)
        lastActivityDate = userActivity.LastActivityDate;
      MembershipDataProvider membershipDataProvider;
      TimeSpan timeSpan = !UserManager.GetManager().Providers.TryGetValue(providerName, out membershipDataProvider) ? Config.Get<SecurityConfig>().UserIsOnlineTimeWindow : membershipDataProvider.UserIsOnlineTimeWindow;
      return DateTime.UtcNow - lastActivityDate < timeSpan;
    }

    /// <summary>Refreshes the administrative roles.</summary>
    public static void RefreshAdministrativeRoles() => SecurityManager.adminRoles.Reset();

    private static string GetUserValidationTokenFromSecurityParams(
      NameValueCollection securityParams)
    {
      return securityParams["vk"];
    }

    private static Dictionary<Guid, string> BuildAdminRoles()
    {
      Dictionary<Guid, string> dictionary = new Dictionary<Guid, string>();
      dictionary.Add(SecurityManager.AdminRole.Id, "Administrators");
      foreach (AdministrativeRole administrativeRole in Config.Get<SecurityConfig>().AdministrativeRoles)
      {
        try
        {
          string str = administrativeRole.RoleProvider + "\\" + administrativeRole.RoleName;
          Guid roleId = SecurityManager.GetRoleId(administrativeRole.RoleProvider, administrativeRole.RoleName);
          dictionary.Add(roleId, str);
        }
        catch (Exception ex)
        {
          Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions);
        }
      }
      return dictionary;
    }

    /// <summary>Gets an instance for RoleManager.</summary>
    /// <returns>An instance of SecurityManager.</returns>
    public static SecurityManager GetManager() => ManagerBase<SecurityDataProvider>.GetManager<SecurityManager>();

    /// <summary>
    /// Gets an instance for RoleManager for the specified data provider.
    /// </summary>
    /// <param name="providerName">The name of the data provider (see remarks)</param>
    /// <returns>An instance of SecurityManager.</returns>
    /// <remarks>
    /// Although you can work with different providers, Sitefinity takes into account only the default
    /// provider, which can be changed int he configuration.
    /// </remarks>
    public static SecurityManager GetManager(string providerName) => ManagerBase<SecurityDataProvider>.GetManager<SecurityManager>(providerName);

    /// <summary>
    /// Gets an instance of the security manager that works in a global transaction
    /// </summary>
    /// <param name="providerName">Name of the provider to use (see remarks)</param>
    /// <param name="transactionName">Name of the global transaction</param>
    /// <returns>Instance of the security manager in a global transaction</returns>
    /// <remarks>
    /// Although you can work with different providers, Sitefinity takes into account only the default
    /// provider, which can be changed int he configuration.
    /// </remarks>
    public static SecurityManager GetManager(
      string providerName,
      string transactionName)
    {
      return ManagerBase<SecurityDataProvider>.GetManager<SecurityManager>(providerName, transactionName);
    }

    internal static void SetSystemAuthenticaitonCookie(HttpResponseBase response) => SecurityManager.SetAuthenticationCookieInt(response, "system", "system", SecurityManager.SystemAccountIDs[0], DateTime.UtcNow, false);

    /// <summary>Adds in authentication ticket in user data.</summary>
    /// <param name="response">The HTTP response.</param>
    /// <param name="membershipProvider">The membership provider.</param>
    /// <param name="userName">Name of the user.</param>
    /// <param name="persistent">if set to <c>true</c> [persistent].</param>
    internal static void SetAuthenticationCookie(
      HttpResponseBase response,
      string membershipProvider,
      string userName,
      bool persistent,
      DateTime issueDate)
    {
      User user = UserManager.GetManager(membershipProvider).GetUser(userName);
      UserActivity userActivity = UserActivityManager.GetManager().GetUserActivity(user.Id, membershipProvider);
      SecurityManager.SetAuthenticationCookieInt(response, membershipProvider, userName, user.Id.ToString(), userActivity.LastLoginDate, persistent);
    }

    /// <summary>Sets the authentication cookie.</summary>
    /// <param name="response">The response.</param>
    /// <param name="membershipProvider">The membership provider.</param>
    /// <param name="user">The user.</param>
    /// <param name="persistent">if set to <c>true</c> [persistent].</param>
    internal static void SetAuthenticationCookie(
      HttpResponseBase response,
      string membershipProvider,
      User user,
      bool persistent,
      DateTime issueDate)
    {
      UserActivity userActivity = UserActivityManager.GetManager().GetUserActivity(user.Id, user.ProviderName);
      SecurityManager.SetAuthenticationCookieInt(response, membershipProvider, user.UserName, user.Id.ToString(), userActivity.LastLoginDate, persistent);
    }

    /// <summary>Sets the authentication cookie internal method.</summary>
    /// <param name="response">The response.</param>
    /// <param name="membershipProvider">The membership provider.</param>
    /// <param name="userName">Name of the user.</param>
    /// <param name="userId">The user id.</param>
    /// <param name="lastLoggedInDate">The last logged in date.</param>
    /// <param name="persistent">if set to <c>true</c> [persistent].</param>
    private static void SetAuthenticationCookieInt(
      HttpResponseBase response,
      string membershipProvider,
      string userName,
      string userId,
      DateTime lastLoggedInDate,
      bool persistent)
    {
      if (response == null)
        throw new ArgumentNullException(nameof (response));
      SecurityConfig config = Config.Get<SecurityConfig>();
      response.Cookies.Set(SecurityManager.CreateAuthCookie(membershipProvider, userName, userId, lastLoggedInDate, persistent, config));
      if (config.AuthCookieName != FormsAuthentication.FormsCookieName)
        SecurityManager.FormsAuthSignOut();
      SecurityManager.DeleteCookie(config.RolesCookieName, config.RolesCookiePath, config.RolesCookieDomain, config.RolesCookieRequireSsl);
    }

    internal static HttpCookie CreateAuthCookie(
      string membershipProvider,
      string userName,
      string userId,
      DateTime lastLoggedInDate,
      bool persistent,
      SecurityConfig config)
    {
      if (config == null)
        config = Config.Get<SecurityConfig>();
      HttpCookie authCookie = new HttpCookie(config.AuthCookieName)
      {
        HttpOnly = true,
        Path = config.AuthCookiePath,
        Domain = config.AuthCookieDomain,
        Secure = config.AuthCookieRequireSsl
      };
      FormsAuthenticationTicket ticket = SecurityManager.CreateTicket(userName, userId, membershipProvider, lastLoggedInDate, config, persistent);
      authCookie.Value = FormsAuthentication.Encrypt(ticket);
      if (ticket.IsPersistent)
        authCookie.Expires = ticket.Expiration;
      return authCookie;
    }

    internal static FormsAuthenticationTicket CreateTicket(
      string userName,
      string userId,
      string provider,
      DateTime timeStamp,
      SecurityConfig config,
      bool persistent)
    {
      return new FormsAuthenticationTicket(1, userName, DateTime.UtcNow, DateTime.UtcNow.Add(config.AuthCookieTimeout), (persistent ? 1 : 0) != 0, userId + ";" + provider + ";" + timeStamp.ToString("u"), config.AuthCookiePath);
    }

    internal static FormsAuthenticationTicket RenewTicket(
      FormsAuthenticationTicket oldTicket,
      TimeSpan newTimeout)
    {
      string[] strArray = oldTicket.UserData.Split(';');
      string str1 = strArray.Length == 3 ? strArray[0] : throw new InvalidDataException("Invalid authenticaiotn ticket data.");
      string str2 = strArray[1];
      string str3 = strArray[2];
      return new FormsAuthenticationTicket(1, oldTicket.Name, DateTime.UtcNow, DateTime.UtcNow.Add(newTimeout), (oldTicket.IsPersistent ? 1 : 0) != 0, str1 + ";" + str2 + ";" + str3, oldTicket.CookiePath);
    }

    /// <summary>
    ///     <para>Claims: Sends HTTP 401 (Unauthorized) and sets the location of the STS.</para>
    ///     <para>Forms: Redirects to the login form.</para>
    /// </summary>
    /// <param name="httpContext"></param>
    public static void RedirectToLogin(HttpContextBase httpContext)
    {
      Telerik.Sitefinity.Security.Configuration.AuthenticationMode authenticationMode = SecurityManager.AuthenticationMode;
      switch (authenticationMode)
      {
        case Telerik.Sitefinity.Security.Configuration.AuthenticationMode.Claims:
          ClaimsManager.SendUnauthorizedResponse(httpContext);
          break;
        case Telerik.Sitefinity.Security.Configuration.AuthenticationMode.Forms:
          string pathAndQuery = httpContext.Request.Url.PathAndQuery;
          if (pathAndQuery.ToLower().Contains(SecurityManager.AuthenticationReturnUrl.ToLower()))
            break;
          string url = UrlPath.AppendQueryString(AppPermission.LoginUrl, SecurityManager.AuthenticationReturnUrl + "=" + pathAndQuery);
          httpContext.Response.Redirect(url, true);
          break;
        default:
          throw new ApplicationException(string.Format("Unsupported authentication mode: {0}", (object) authenticationMode));
      }
    }

    /// <summary>Logs out the user making the current request</summary>
    public static void Logout()
    {
      SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
      if (currentIdentity != null && currentIdentity.UserId != Guid.Empty)
        SecurityManager.Logout(currentIdentity.MembershipProvider, currentIdentity.UserId);
      SecurityManager.DeleteAuthCookies();
      ClaimsManager.Logout();
    }

    /// <summary>
    /// Logs out the user with the specified credentials.
    /// This request doesn't have to be authenticated but the credentials must be valid.
    /// </summary>
    /// <param name="credentials">The credentials.</param>
    public static void Logout(Credentials credentials)
    {
      if (credentials == null)
        throw new ArgumentNullException(nameof (credentials));
      SecurityManager.LogoutImpl(credentials.MembershipProvider, Guid.Empty, credentials.UserName, credentials);
    }

    /// <summary>
    /// Logs out the specified user.
    /// The user making the current request must either be administrator to logout other user or the specified user must be himself,
    /// otherwise an <see cref="T:System.UnauthorizedAccessException" /> will be thrown.
    /// </summary>
    /// <param name="providerName">The name of the membership provider.</param>
    /// <param name="userName">The username of the user to be logged out.</param>
    public static void Logout(string providerName, string userName)
    {
      if (string.IsNullOrEmpty(userName))
        throw new ArgumentNullException(nameof (userName));
      SecurityManager.LogoutImpl(providerName, Guid.Empty, userName, (Credentials) null);
    }

    /// <summary>
    /// Logs out the specified user.
    /// The user making the current request must either be administrator to logout other user or the specified user must be himself,
    /// otherwise an <see cref="T:System.UnauthorizedAccessException" /> will be thrown.
    /// </summary>
    /// <param name="providerName">The name of the membership provider.</param>
    /// <param name="userId">The ID of the user to be logged out.</param>
    public static void Logout(string providerName, Guid userId)
    {
      if (userId == Guid.Empty)
        throw new ArgumentNullException(nameof (userId));
      SecurityManager.LogoutImpl(providerName, userId, (string) null, (Credentials) null);
    }

    /// <summary>
    /// Logs out the specified user.
    /// The provided credentials must either have administrative rights to logout other user or match the specified user,
    /// otherwise an <see cref="T:System.UnauthorizedAccessException" /> will be thrown.
    /// </summary>
    /// <param name="providerName">The name of the membership provider.</param>
    /// <param name="userName">The username of the user to be logged out.</param>
    public static void Logout(string providerName, string userName, Credentials credentials)
    {
      if (string.IsNullOrEmpty(userName))
        throw new ArgumentNullException(nameof (userName));
      if (credentials == null)
        throw new ArgumentNullException(nameof (credentials));
      SecurityManager.LogoutImpl(providerName, Guid.Empty, userName, credentials);
    }

    /// <summary>
    /// Logs out the specified user.
    /// The provided credentials must either have administrative rights to logout other user or match the specified user,
    /// otherwise an <see cref="T:System.UnauthorizedAccessException" /> will be thrown.
    /// </summary>
    /// <param name="providerName">The name of the membership provider.</param>
    /// <param name="userId">The ID of the user to be logged out.</param>
    public static void Logout(string providerName, Guid userId, Credentials credentials)
    {
      if (userId == Guid.Empty)
        throw new ArgumentNullException(nameof (userId));
      if (credentials == null)
        throw new ArgumentNullException(nameof (credentials));
      SecurityManager.LogoutImpl(providerName, userId, (string) null, credentials);
    }

    private static void LogoutImpl(
      string providerName,
      Guid userId,
      string userName,
      Credentials credentials)
    {
      UserManager manager = UserManager.GetManager(providerName, "Authenticate");
      User user1;
      if (userId == Guid.Empty)
      {
        user1 = manager.GetUser(userName);
        if (user1 == null)
          throw new ArgumentException("Invalid username specified.");
      }
      else
        user1 = manager.GetUser(userId);
      if (user1 == null)
      {
        UserActivityManager.GetManager().RemoveFromCache(userId);
      }
      else
      {
        userId = user1.Id;
        providerName = manager.Provider.Name;
        if (credentials != null)
        {
          UserManager userManager = !(credentials.MembershipProvider != manager.Provider.Name) ? manager : UserManager.GetManager(credentials.MembershipProvider, "Authenticate");
          User user2 = userManager.GetUser(credentials.UserName);
          if (!userManager.ValidateUser(user2, credentials.Password))
            throw new ArgumentException("Invalid credentials specified.");
          if (user1.Id != user2.Id && !SecurityManager.IsUserUnrestricted(user2.Id))
            throw new UnauthorizedAccessException("Only administrators can logout other users.");
        }
        else
        {
          SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
          if (currentIdentity == null || !currentIdentity.IsAuthenticated)
            throw new UnauthorizedAccessException("The current request must be authenticated to logout a user.");
          if (!currentIdentity.IsUnrestricted && user1.Id != currentIdentity.UserId)
            throw new UnauthorizedAccessException("Only administrators can logout other users.");
        }
        string userHostAddress = SystemManager.CurrentHttpContext.Request.UserHostAddress;
        SecurityManager.RaiseLogoutEvent(user1, userHostAddress);
        SecurityManager.ClearUserActivity(providerName, user1.Id);
      }
    }

    /// <summary>
    /// Log out user and sets the reason for log out in cookie.
    /// </summary>
    /// <param name="reason">The reason.</param>
    /// <param name="context">The HttpContextBase object</param>
    internal static void Logout(UserLoggingReason reason, HttpContextBase context)
    {
      SecurityManager.BuildLogoutCookie(reason, context);
      SecurityManager.Logout();
    }

    /// <summary>Clears the user activity from cache and database.</summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="userId">The user id.</param>
    /// <param name="manager">The manager.</param>
    internal static void ClearUserActivity(string userProviderName, Guid userId)
    {
      UserActivityManager manager = UserActivityManager.GetManager();
      manager.RemoveFromCache(userId);
      UserActivity userActivity = manager.GetUserActivity(userId, userProviderName);
      if (userActivity == null)
        return;
      userActivity.LastActivityDate = DateTime.UtcNow;
      userActivity.TokenId = (string) null;
      userActivity.IsLoggedIn = false;
      manager.SaveChanges();
    }

    /// <summary>
    /// Ensures the current user is unrestricted. An exception is thrown if the user is not unrestricted.
    /// </summary>
    /// <returns></returns>
    public static Guid EnsureCurrentUserIsUnrestricted()
    {
      SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
      return currentIdentity != null && currentIdentity.IsUnrestricted ? currentIdentity.UserId : throw new InvalidOperationException("The current user does not have permission for this action.");
    }

    /// <summary>
    /// Ensures the current user is unrestricted. An exception is thrown if the user is not unrestricted.
    /// </summary>
    /// <returns></returns>
    public static bool IsUserUnrestricted(Guid userId)
    {
      SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
      return currentIdentity != null && userId == currentIdentity.UserId ? currentIdentity.IsUnrestricted : RoleManager.IsUserUnrestricted(userId);
    }

    /// <summary>Deletes the authentication and roles cookies.</summary>
    public static void DeleteAuthCookies()
    {
      SecurityConfig securityConfig = Config.Get<SecurityConfig>();
      SecurityManager.DeleteCookie(securityConfig.AuthCookieName, securityConfig.AuthCookiePath, securityConfig.AuthCookieDomain, securityConfig.AuthCookieRequireSsl);
      SecurityManager.DeleteCookie(securityConfig.RolesCookieName, securityConfig.RolesCookiePath, securityConfig.RolesCookieDomain, securityConfig.RolesCookieRequireSsl);
      AuthenticationSection section = (AuthenticationSection) WebConfigurationManager.GetSection("system.web/authentication");
      if (section.Mode != System.Web.Configuration.AuthenticationMode.Forms)
        return;
      SecurityManager.DeleteCookie(section.Forms.Name, section.Forms.Path, section.Forms.Domain, section.Forms.RequireSSL);
    }

    /// <summary>
    /// Builds the logout cookie.
    /// Cookie is used to pass the reason to login form and to display the reason
    /// for log out.
    /// </summary>
    /// <param name="reason">The reason.</param>
    /// <param name="context">The current HttpContext</param>
    internal static void BuildLogoutCookie(UserLoggingReason reason, HttpContextBase context)
    {
      SecurityConfig securityConfig = Config.Get<SecurityConfig>();
      SecurityManager.DeleteCookie(securityConfig.LoggingCookieName, securityConfig.AuthCookiePath, securityConfig.AuthCookieDomain, securityConfig.AuthCookieRequireSsl);
      HttpCookie cookie = new HttpCookie(securityConfig.LoggingCookieName)
      {
        HttpOnly = true,
        Path = securityConfig.AuthCookiePath,
        Domain = securityConfig.AuthCookieDomain,
        Secure = securityConfig.AuthCookieRequireSsl,
        Value = reason.ToString()
      };
      context.Response.Cookies.Add(cookie);
    }

    /// <summary>Gets the logging cookie.</summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public static string GetLoggingCookie(HttpContextBase context)
    {
      SecurityConfig securityConfig = Config.Get<SecurityConfig>();
      HttpCookie cookie = context.Request.Cookies[securityConfig.LoggingCookieName];
      return cookie != null && !string.IsNullOrEmpty(cookie.Value) ? cookie.Value : (string) null;
    }

    internal static FormsSitefinityIdentity BuildIdentity(
      HttpContextBase context)
    {
      if (context.User == null || !(context.User.Identity is FormsSitefinityIdentity))
      {
        SecurityConfig securityConfig = Config.Get<SecurityConfig>();
        if (Bootstrapper.IsSystemInitialized)
        {
          HttpCookie cookie1 = context.Request.Cookies[securityConfig.AuthCookieName];
          if (cookie1 != null)
          {
            if (!string.IsNullOrEmpty(cookie1.Value))
            {
              FormsAuthenticationTicket authenticationTicket;
              try
              {
                authenticationTicket = FormsAuthentication.Decrypt(cookie1.Value);
              }
              catch
              {
                authenticationTicket = (FormsAuthenticationTicket) null;
                SecurityManager.Logout();
              }
              if (authenticationTicket != null && !authenticationTicket.Expired)
              {
                if (securityConfig.AuthCookieSlidingExpiration)
                {
                  HttpCookie cookie2 = new HttpCookie(securityConfig.AuthCookieName)
                  {
                    HttpOnly = true,
                    Path = securityConfig.AuthCookiePath,
                    Domain = securityConfig.AuthCookieDomain,
                    Secure = securityConfig.AuthCookieRequireSsl
                  };
                  authenticationTicket = SecurityManager.RenewTicket(authenticationTicket, securityConfig.AuthCookieTimeout);
                  cookie2.Value = FormsAuthentication.Encrypt(authenticationTicket);
                  if (authenticationTicket.IsPersistent)
                    cookie2.Expires = authenticationTicket.Expiration;
                  context.Response.Cookies.Remove(cookie2.Name);
                  context.Response.Cookies.Add(cookie2);
                }
                return new FormsSitefinityIdentity(authenticationTicket);
              }
            }
          }
        }
      }
      return new FormsSitefinityIdentity((ClaimsIdentity) SitefinityIdentity.GetAnonymous());
    }

    internal static SitefinityPrincipal BuildPrincipal(HttpContextBase context) => new SitefinityPrincipal((ClaimsIdentity) SecurityManager.BuildIdentity(context));

    /// <summary>Authenticates the user with the provided credentials.</summary>
    /// <param name="credentials">The credentials.</param>
    /// <returns></returns>
    public static UserLoggingReason AuthenticateUser(Credentials credentials)
    {
      if (credentials == null)
        throw new ArgumentNullException(nameof (credentials));
      return SecurityManager.AuthenticateUser(credentials.MembershipProvider, credentials.UserName, credentials.Password, credentials.Persistent);
    }

    /// <summary>Authenticates the user with the provided credentials.</summary>
    /// <param name="membershipProviderName">Name of the membership provider.</param>
    /// <param name="userName">Name of the user.</param>
    /// <param name="password">The password.</param>
    /// <param name="persistent">if set to <c>true</c> [persistent].</param>
    /// <returns></returns>
    public static UserLoggingReason AuthenticateUser(
      string membershipProviderName,
      string userName,
      string password,
      bool persistent)
    {
      return SecurityManager.AuthenticateUser(membershipProviderName, userName, password, persistent, out User _);
    }

    /// <summary>Authenticates the user with the provided credentials.</summary>
    /// <param name="membershipProviderName">Name of the membership provider.</param>
    /// <param name="userName">Name of the user.</param>
    /// <param name="password">The password.</param>
    /// <param name="persistent">if set to <c>true</c> [persistent].</param>
    /// <param name="user">The user.</param>
    /// <returns>
    /// The authentication can fail for several reasons. Check the UserLoggingReason enumeration.
    /// Only the UserLoggingReason.Success result confirms a successful authentication
    /// </returns>
    public static UserLoggingReason AuthenticateUser(
      string membershipProviderName,
      string userName,
      string password,
      bool persistent,
      out User user)
    {
      UserManager manager = UserManager.GetManager(membershipProviderName, "Authenticate");
      UserLoggingReason loginResult;
      using (new ElevatedModeRegion((IManager) manager))
      {
        user = manager.GetUser(userName);
        if (user == null)
        {
          loginResult = UserLoggingReason.Unknown;
          SecurityManager.RaiseLoginEvent(username: userName, providerName: membershipProviderName, loginResult: loginResult);
          return loginResult;
        }
        if (manager.ValidateUser(user, password))
        {
          loginResult = SecurityManager.AuthenticateUser(user, manager, persistent);
        }
        else
        {
          loginResult = UserLoggingReason.Unknown;
          SecurityManager.RaiseLoginEvent(username: userName, providerName: membershipProviderName, loginResult: loginResult);
          user = (User) null;
        }
        TransactionManager.CommitTransaction("Authenticate");
      }
      return loginResult;
    }

    /// <summary>
    /// This method can be used to integrate with external security systems providing custom authentication and skip the default password based authentication of Sitefinity. It will log the user in both Sitefinity as Relying party and Sitefinity`s internal Identity Provider.
    /// </summary>
    /// <param name="membershipProviderName">Name of the membership provider.</param>
    /// <param name="userName">Username.</param>
    /// <param name="persistent">Whether to keep the user logged in for the current user agent session only or persist between user agent sessions.</param>
    /// <param name="successRedirectUrl">The url to which the user agent will be redirected if the login was successful.</param>
    /// <param name="errorRedirectUri">The url to which the user agent will be redirected in case the login was unsuccessful.</param>
    /// <returns>
    /// <see cref="F:Telerik.Sitefinity.Security.UserLoggingReason.Success" /> if all login checks have passed. Note that all the redirects triggerred by the method must complete before user is actually logged in the system. The user is considered logged in once the user agent is redirected to the provided <paramref name="successRedirectUrl" />. Any other <see cref="T:Telerik.Sitefinity.Security.UserLoggingReason" /> value is considered unsuccessful. Inspect the returned result for more info. Note that the current response will be redirected to the <paramref name="errorRedirectUrl" /> in case of unsuccessful attempted login.
    /// </returns>
    /// <remarks>
    /// This authentication is still subject of the license limitations of backend users
    /// </remarks>
    public static UserLoggingReason SkipAuthenticationAndLogin(
      string membershipProviderName,
      string userName,
      bool persistent,
      string successRedirectUrl,
      string errorRedirectUrl)
    {
      IRedirectUriValidator redirectUriValidator = ObjectFactory.Resolve<IRedirectUriValidator>();
      if (!Uri.IsWellFormedUriString(successRedirectUrl, UriKind.Absolute))
        throw new ArgumentException("Invalid redirect url. Must be absolute.");
      if (!redirectUriValidator.IsValid(successRedirectUrl))
        throw new ArgumentException("Unsafe success redirect url.");
      if (!Uri.IsWellFormedUriString(errorRedirectUrl, UriKind.Absolute))
        throw new ArgumentException("Invalid error redirect url. Must be absolute.");
      if (!redirectUriValidator.IsValid(errorRedirectUrl))
        throw new ArgumentException("Unsafe error redirect url.");
      UserManager manager = UserManager.GetManager(membershipProviderName, "Authenticate");
      using (new ElevatedModeRegion((IManager) manager))
      {
        User user = manager.GetUser(userName);
        if (user == null)
        {
          SecurityManager.RaiseLoginEvent(username: userName, providerName: membershipProviderName, loginResult: UserLoggingReason.Unknown);
          SystemManager.CurrentHttpContext.Response.Redirect(errorRedirectUrl, false);
          return UserLoggingReason.Unknown;
        }
        if (SecurityManager.AuthenticationMode == Telerik.Sitefinity.Security.Configuration.AuthenticationMode.Forms)
        {
          int num = (int) SecurityManager.AuthenticateUser(user, manager, persistent);
          TransactionManager.CommitTransaction("Authenticate");
          if (num == 0)
            SystemManager.CurrentHttpContext.Response.Redirect(successRedirectUrl, false);
          else
            SystemManager.CurrentHttpContext.Response.Redirect(errorRedirectUrl, false);
          return (UserLoggingReason) num;
        }
        if (SecurityManager.AuthenticationMode != Telerik.Sitefinity.Security.Configuration.AuthenticationMode.Claims)
          throw new InvalidOperationException("Unknown authentication mode configured.");
        string userHostAddress = SystemManager.CurrentHttpContext.Request.UserHostAddress;
        UserLoggingReason result = UserLoggingReason.Success;
        if (SecurityManager.IsBackend(user))
          result = SecurityManager.VerifyLoginRequest(user, userHostAddress, DateTime.UtcNow, out List<Guid> _);
        if (result != UserLoggingReason.Success)
        {
          SecurityManager.RaiseLoginEvent(user, userHostAddress, result);
          SystemManager.CurrentHttpContext.Response.Redirect(errorRedirectUrl, false);
          return result;
        }
        SecurityManager.TriggerForcedUserAuthenticationInIdentityProvider(membershipProviderName, userName, persistent, successRedirectUrl, errorRedirectUrl);
        return UserLoggingReason.Success;
      }
    }

    private static bool IsBackend(User user) => new SitefinityIdentity(user).IsBackendUser;

    private static void TriggerForcedUserAuthenticationInIdentityProvider(
      string membershipProviderName,
      string userName,
      bool persistent,
      string redirectUri,
      string errrorRedirectUrl)
    {
      string password = Guid.NewGuid().ToString();
      AuthenticationProperties properties = ChallengeProperties.ForLocalUser(userName, password, membershipProviderName, persistent, errrorRedirectUrl);
      properties.RedirectUri = redirectUri;
      properties.Dictionary.Add("SkipAuthentication", "true");
      SystemManager.CurrentHttpContext.GetOwinContext().Authentication.Challenge(properties, ClaimsManager.CurrentAuthenticationModule.STSAuthenticationType);
    }

    /// <summary>
    /// Forces authentication of a user by username and provider, without checking credentials
    /// This method can be used to integrate with external security systems providing custom authentication and skip the default password based authentication of Sitefinity.
    /// If successful the  method execution results in setting the current thread principal to the authenticated user and issuing a authentication cookie
    /// </summary>
    /// <param name="membershipProviderName">Name of the membership provider.</param>
    /// <param name="userName">Name of the user.</param>
    /// <param name="persistent">if set to <c>true</c> [persistent].</param>
    /// <param name="user">The user.</param>
    /// <returns>
    /// The authentication can fail for several reasons. Check the UserLoggingReason enumeration.
    /// Only the UserLoggingReason.Success result confirms a successful authentication
    /// </returns>
    /// <remarks>
    /// This authentication is still subject of the license limitations of backend users
    /// </remarks>
    [Obsolete("This method is deprecated. Use SecurityManager.SkipAuthenticationAndLogin instead.")]
    public static UserLoggingReason AuthenticateUser(
      string membershipProviderName,
      string userName,
      bool persistent,
      out User user)
    {
      UserManager manager = UserManager.GetManager(membershipProviderName, "Authenticate");
      UserLoggingReason loginResult;
      using (new ElevatedModeRegion((IManager) manager))
      {
        user = manager.GetUser(userName);
        if (user == null)
        {
          loginResult = UserLoggingReason.Unknown;
          SecurityManager.RaiseLoginEvent(username: userName, providerName: membershipProviderName, loginResult: loginResult);
          return loginResult;
        }
        loginResult = SecurityManager.AuthenticateUser(user, manager, persistent);
        TransactionManager.CommitTransaction("Authenticate");
      }
      return loginResult;
    }

    internal static UserLoggingReason AuthenticateUser(
      User user,
      UserManager manager,
      bool persistent)
    {
      SitefinityIdentity identity = new SitefinityIdentity(user);
      Claim claim = new Claim("http://schemas.sitefinity.com/ws/2011/06/identity/claims/issuedate", DateTime.UtcNow.ToString("u"));
      identity.AddClaim(claim);
      user.IsBackendUser = identity.IsBackendUser;
      UserLoggingReason reason;
      if (SecurityManager.AuthenticationMode == Telerik.Sitefinity.Security.Configuration.AuthenticationMode.Claims)
      {
        string tokenId = Guid.NewGuid().ToString();
        identity.AddClaim(new Claim("http://schemas.sitefinity.com/ws/2011/06/identity/claims/tokenid", tokenId));
        reason = SecurityManager.Login(user, manager, persistent, identity.IsBackendUser, identity.IssueDate, tokenId);
        ClaimsManager.AdjustClaims((ClaimsPrincipal) new SitefinityPrincipal((ClaimsIdentity) identity));
        if (reason == UserLoggingReason.Success)
        {
          identity = new SitefinityIdentity((ClaimsIdentity) identity, ClaimsManager.CurrentAuthenticationModule.RPAuthenticationType);
          SystemManager.CurrentHttpContext.GetOwinContext().Authentication.SignIn(SignInProperties.ForAlreadyLoggedUser(reason, persistent), (ClaimsIdentity) identity);
        }
      }
      else
        reason = SecurityManager.Login(user, manager, persistent, identity.IsBackendUser, identity.IssueDate);
      IPrincipal principal = (IPrincipal) new SitefinityPrincipal((ClaimsIdentity) identity);
      SystemManager.CurrentHttpContext.User = principal;
      Thread.CurrentPrincipal = principal;
      return reason;
    }

    internal static UserLoggingReason Login(
      SitefinityPrincipal claimsPrincipal,
      string tokenId,
      DateTime issueDate)
    {
      if (!(claimsPrincipal.Identity is SitefinityIdentity identity))
        throw new ArgumentException("claimsPrincipal parameter should contain a SitefinityIdentity as Identity property");
      UserLoggingReason loginResult = UserLoggingReason.Success;
      Guid userId = identity.UserId;
      if (string.Compare(userId.ToString(), SecurityManager.SystemAccountIDs[0], true) == 0)
        return loginResult;
      using (UserManager manager = UserManager.GetManager(identity.MembershipProvider))
      {
        using (new ElevatedModeRegion((IManager) manager))
        {
          User user;
          if (identity.UserId != Guid.Empty && manager.TryGetUser(identity.UserId, out user))
          {
            bool persistent = false;
            user.IsBackendUser = identity.IsBackendUser;
            bool loginsLimitation = SecurityManager.IsToDisableActiveUserLoginsLimitation((ClaimsPrincipal) claimsPrincipal);
            loginResult = SecurityManager.Login(user, manager, persistent, identity.IsBackendUser, issueDate, tokenId, loginsLimitation);
          }
          else
          {
            if (!string.IsNullOrWhiteSpace(tokenId))
              SecurityManager.SetTokenIdCookie(tokenId);
            loginResult = UserLoggingReason.UserNotFound;
            userId = identity.UserId;
            SecurityManager.RaiseLoginEvent(userId.ToString(), identity.Name, providerName: identity.MembershipProvider, loginResult: loginResult);
          }
          if (loginResult == UserLoggingReason.Success)
            manager.SaveChanges();
        }
      }
      return loginResult;
    }

    internal static UserLoggingReason Login(
      User user,
      UserManager manager,
      bool persistent,
      bool isBackend,
      DateTime issueDate,
      string tokenId = null,
      bool isToDisableUsersLoginsLimitation = false)
    {
      string userHostAddress = SystemManager.CurrentHttpContext.Request.UserHostAddress;
      UserActivityManager manager1 = UserActivityManager.GetManager();
      UserLoggingReason result;
      using (new ElevatedModeRegion((IManager) manager))
      {
        using (manager1)
        {
          if (isBackend)
          {
            List<Guid> allowedAccessSiteIDs;
            result = SecurityManager.VerifyLoginRequest(user, userHostAddress, issueDate, out allowedAccessSiteIDs, tokenId, isToDisableUsersLoginsLimitation);
            SecurityManager.CreateOrUpdateUserActivity(user, issueDate, tokenId, result, userHostAddress, manager1, allowedAccessSiteIDs);
            switch (result)
            {
              case UserLoggingReason.Success:
                SecurityManager.RegisterUserLogin(user, manager, persistent, issueDate, tokenId, allowedAccessSiteIDs);
                user.LastLoginDate = DateTime.UtcNow;
                user.IsLoggedIn = true;
                break;
              case UserLoggingReason.UserLimitReached:
              case UserLoggingReason.UserLoggedFromDifferentIp:
              case UserLoggingReason.UserLoggedFromDifferentComputer:
              case UserLoggingReason.UserAlreadyLoggedIn:
              case UserLoggingReason.SiteAccessNotAllowed:
                user.IsLoggedIn = false;
                break;
              case UserLoggingReason.SessionExpired:
                manager1.GetUserActivity(user.Id, user.ProviderName).IsLoggedIn = true;
                manager1.SaveChanges();
                user.LastLoginDate = DateTime.UtcNow;
                user.IsLoggedIn = true;
                break;
              default:
                throw new ArgumentException("Unhandled User logging reason.");
            }
          }
          else
          {
            if (manager1.GetUserActivity(user.Id, user.ProviderName) == null)
              SecurityManager.CreateUserActivityWithTransaction(user.Id, userHostAddress, user.ProviderName, false, DateTime.UtcNow, DateTime.UtcNow, tokenId);
            result = UserLoggingReason.Success;
            user.LastLoginDate = DateTime.UtcNow;
            SecurityManager.RegisterUserLogin(user, manager, persistent, issueDate, tokenId);
          }
        }
      }
      SecurityManager.RaiseLoginEvent(user, userHostAddress, result);
      return result;
    }

    private static void CreateOrUpdateUserActivity(
      User user,
      DateTime issueDate,
      string tokenId,
      UserLoggingReason result,
      string ip,
      UserActivityManager uaManager,
      List<Guid> allowedAccessSiteIDs)
    {
      UserActivity userActivity = uaManager.GetUserActivity(user.Id, user.ProviderName);
      if (result != UserLoggingReason.Success)
        return;
      if (userActivity == null)
      {
        uaManager.AddToCache(user.Id, ip, user.ProviderName, true, issueDate, issueDate, allowedAccessSiteIDs, tokenId);
        uaManager.CreateUserActivity(user.Id, ip, user.ProviderName, user.IsBackendUser, issueDate, issueDate, tokenId);
      }
      else
      {
        uaManager.AddToCache(user.Id, ip, user.ProviderName, user.IsBackendUser, issueDate, issueDate, allowedAccessSiteIDs, tokenId);
        userActivity.LastLoginDate = issueDate;
        userActivity.LastActivityDate = issueDate;
        userActivity.LoginIP = ip;
        userActivity.IsLoggedIn = true;
        userActivity.IsBackendUser = user.IsBackendUser;
        userActivity.TokenId = tokenId;
      }
      uaManager.SaveChanges();
    }

    internal static string GetUserToken(User user) => "WRAP access_token=" + ClaimsManager.BuildSimpleWebToken(new ClaimsPrincipal((IEnumerable<ClaimsIdentity>) new SitefinityIdentity[1]
    {
      new SitefinityIdentity(user, true)
    }), (string) null).RawToken.Base64Encode();

    private static void RaiseLoginEvent(User user, string ip, UserLoggingReason result) => EventHub.Raise((IEvent) new LoginCompletedEvent(user, ip, result, "Sitefinity Login - RP"));

    internal static void RaiseLoginEvent(
      string userId = null,
      string username = null,
      string email = null,
      string providerName = null,
      UserLoggingReason loginResult = UserLoggingReason.Success,
      string eventSource = "Sitefinity Login - RP")
    {
      HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
      string ip = (string) null;
      if (currentHttpContext != null)
        ip = currentHttpContext.Request.UserHostAddress;
      EventHub.Raise((IEvent) new LoginCompletedEvent(userId, username, email, providerName, ip, loginResult, eventSource));
    }

    internal static void RaiseInternalStsLoginEvent(
      string userId = null,
      string username = null,
      string email = null,
      string providerName = null,
      InternalStsUserLoginResult loginResult = InternalStsUserLoginResult.Success,
      string externalProviderName = null)
    {
      HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
      string ip = (string) null;
      if (currentHttpContext != null)
        ip = currentHttpContext.Request.UserHostAddress;
      EventHub.Raise((IEvent) new InternalStsLoginCompletedEvent(userId, username, email, providerName, ip, loginResult, externalProviderName));
    }

    private static void RaiseLogoutEvent(User user, string ip) => EventHub.Raise((IEvent) new LogoutCompletedEvent(user, ip, "Sitefinity Logout - RP"), false);

    internal static void RaiseLogoutEvent(
      string userId = null,
      string username = null,
      string providerName = null,
      string eventSource = "Sitefinity Logout - RP",
      bool isBackend = false)
    {
      HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
      string ip = (string) null;
      if (currentHttpContext != null)
        ip = currentHttpContext.Request.UserHostAddress;
      EventHub.Raise((IEvent) new LogoutCompletedEvent(userId, username, providerName, ip, eventSource, isBackend), false);
    }

    /// <summary>
    /// Gets an authentication key for the current user that will be valid for authentication using
    /// <c>AuthenticateUser(string encryptedValidationKey)</c>.
    /// The authentication should be done explicitly by calling <c>AuthenticateUser(string encryptedValidationKey)</c>.
    /// </summary>
    /// <param name="validity">If no validity is specified it uses 5 minutes as a default value.</param>
    /// <returns>The authentication key that should be passed to the request.</returns>
    internal static string GetUserAuthenticationKey(TimeSpan? validity = null, string forUrlPath = null)
    {
      if (!validity.HasValue)
        validity = new TimeSpan?(TimeSpan.FromMinutes(5.0));
      SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
      string data = string.Format("{0}{1}{2}{3}{4}", (object) currentIdentity.MembershipProvider, (object) ',', (object) currentIdentity.UserId, (object) ',', (object) DateTime.UtcNow.Add(validity.Value).ToString("u"));
      if (!string.IsNullOrEmpty(forUrlPath))
        data = string.Format("{0}{1}{2}", (object) data, (object) ',', (object) HttpUtility.UrlEncode(forUrlPath));
      return SecurityManager.EncryptData(data);
    }

    private static ClaimsPrincipal GetPrincipal(
      string validationKey,
      out User user,
      bool validateUrlPath = false)
    {
      string str1 = (string) null;
      try
      {
        str1 = SecurityManager.DecryptData(validationKey);
      }
      catch
      {
      }
      if (!string.IsNullOrWhiteSpace(str1))
      {
        int num = validateUrlPath ? 4 : 3;
        string[] strArray = str1.Split(',');
        if (strArray.Length == num)
        {
          string providerName = strArray[0];
          string input = strArray[1];
          string str2 = strArray[2];
          DateTime datetime;
          Guid result;
          if (!string.IsNullOrWhiteSpace(str2) && SecurityManager.TryGetUtcDate(str2, out datetime) && DateTime.UtcNow <= datetime && Guid.TryParse(input, out result))
          {
            bool flag = true;
            if (validateUrlPath)
            {
              string str3 = strArray[3];
              HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
              flag = currentHttpContext != null && currentHttpContext.Request.Path.Equals(HttpUtility.UrlDecode(str3));
            }
            if (flag)
            {
              UserManager manager = UserManager.GetManager(providerName);
              user = manager.GetUser(result);
              if (user != null)
                return (ClaimsPrincipal) new SitefinityPrincipal((ClaimsIdentity) new SitefinityIdentity(user, true));
            }
          }
        }
      }
      user = (User) null;
      return (ClaimsPrincipal) null;
    }

    /// <summary>
    /// Authenticates the user using the decrypting the validation key
    /// without other (license) validation and without updating the user activity.
    /// It is suitable for authentication of a single request using an encrypted key.
    /// </summary>
    /// <param name="encryptedValidationKey">The validation key.</param>
    /// <param name="validateUrlPath">Specify whether the url path should be validated for the current request.</param>
    internal static void AuthenticateUser(string encryptedValidationKey, bool validateUrlPath = false)
    {
      User user;
      ClaimsPrincipal principal = SecurityManager.GetPrincipal(encryptedValidationKey, out user, validateUrlPath);
      if (principal == null)
        return;
      HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
      currentHttpContext.User = (IPrincipal) principal;
      Thread.CurrentPrincipal = currentHttpContext.User;
      if (user == null)
        return;
      currentHttpContext.Items[(object) "sfMultisiteContextUserId"] = (object) new Tuple<Guid, string>(user.Id, user.ProviderName);
    }

    internal static void AuthenticateSystemRequest(HttpContextBase context)
    {
      ApplicationRole applicationRole = Config.Get<SecurityConfig>().ApplicationRoles["Administrators"];
      RoleInfo roleInfo = new RoleInfo()
      {
        Id = applicationRole.Id,
        Name = applicationRole.Name,
        Provider = applicationRole.Provider.Name
      };
      ClaimsIdentity identity = new ClaimsIdentity("System");
      ClaimsManager.SetName(identity, "system");
      ClaimsManager.SetUserId(identity, SecurityManager.SystemAccountIDs[0]);
      ClaimsManager.SetSitefinityRoles(identity, SecurityManager.AdminRole);
      ClaimsManager.SetMembershipProvider(identity, string.Empty);
      ClaimsManager.SetLastLoginDate(identity, DateTime.UtcNow);
      if (SecurityManager.AuthenticationMode == Telerik.Sitefinity.Security.Configuration.AuthenticationMode.Forms)
      {
        SitefinityPrincipal sitefinityPrincipal = new SitefinityPrincipal((ClaimsIdentity) new FormsSitefinityIdentity(identity));
        context.User = (IPrincipal) sitefinityPrincipal;
        SecurityManager.SetAuthenticationCookieInt(context.Response, "System", "system", SecurityManager.SystemAccountIDs[0], DateTime.UtcNow, false);
        SecurityManager.UpdateCookies(context);
      }
      else
      {
        SitefinityPrincipal sitefinityPrincipal = new SitefinityPrincipal(identity);
        context.User = (IPrincipal) sitefinityPrincipal;
      }
    }

    internal static void AuthenticateUserRequest(
      HttpContextBase context,
      string userName,
      string userId,
      string membershipProvider = null)
    {
      DateTime lastLoggedInDate = DateTime.UtcNow;
      UserActivity userActivity = UserActivityManager.GetManager().GetUserActivity(new Guid(userId), membershipProvider);
      if (userActivity != null)
        lastLoggedInDate = userActivity.LastLoginDate;
      SecurityManager.SetAuthenticationCookieInt(context.Response, membershipProvider, userName, userId, lastLoggedInDate, false);
    }

    /// <summary>Authenticates the request.</summary>
    /// <param name="context">The context.</param>
    public static void AuthenticateRequest(HttpContextBase context)
    {
      if (context == null)
        throw new ArgumentNullException(nameof (context));
      if (context.User != null && context.User.Identity is FormsSitefinityIdentity || SecurityManager.IsBasicAuthenticaton(context))
        return;
      SitefinityPrincipal principal = SecurityManager.BuildPrincipal(context);
      if (principal.Identity.IsAuthenticated)
      {
        FormsSitefinityIdentity identity = (FormsSitefinityIdentity) principal.Identity;
        if (identity.UserId.ToString().Equals(SecurityManager.SystemAccountIDs[0], StringComparison.OrdinalIgnoreCase))
        {
          SecurityManager.AuthenticateSystemRequest(context);
          return;
        }
        string userHostAddress = context.Request.UserHostAddress;
        DeletedUserRecord deletedUser = UserActivityManager.FindDeletedUser(identity.UserId, identity.MembershipProvider);
        if (deletedUser != null && identity.LastLoginDate < deletedUser.LogDate)
        {
          SecurityManager.RevokeCurrentUser(context);
          return;
        }
        if (Bootstrapper.IsSystemInitialized)
        {
          if (identity.IsBackendUser)
          {
            UserLoggingReason reason = SecurityManager.VerifyAuthenticateRequest(principal, userHostAddress, identity.LastLoginTimeStampString);
            if (reason != UserLoggingReason.Success)
            {
              context.User = (IPrincipal) new ClaimsPrincipal((IIdentity) SitefinityIdentity.GetAnonymous());
              SecurityManager.Logout(reason, context);
              return;
            }
          }
          else
            UserActivityManager.GetManager().UpdateRecord(identity.UserId, DateTime.UtcNow);
        }
      }
      context.User = (IPrincipal) principal;
    }

    public static DateTime ParseLoginTimeStamp(string lastLoginStamp)
    {
      DateTime result;
      if (!DateTime.TryParseExact(lastLoginStamp, "u", (IFormatProvider) null, DateTimeStyles.AdjustToUniversal, out result))
        result = DateTime.UtcNow;
      return result;
    }

    private static bool IsBasicAuthenticaton(HttpContextBase context)
    {
      string header = context.Request.Headers["Authorization"];
      if (!header.IsNullOrEmpty())
      {
        string str = header.Trim();
        if (str.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
        {
          string[] strArray = Encoding.UTF8.GetString(Convert.FromBase64String(str.Substring(5))).Split(':');
          string userName = strArray[0];
          string password = strArray[1];
          int length = userName.IndexOf('\\');
          string membershipProviderName;
          if (length != -1)
          {
            membershipProviderName = userName.Substring(0, length);
            userName = userName.Substring(length + 1);
          }
          else
            membershipProviderName = string.Empty;
          UserLoggingReason userLoggingReason = SecurityManager.AuthenticateUser(membershipProviderName, userName, password, false);
          if (userLoggingReason != UserLoggingReason.Success)
            throw new HttpException(403, "Failed to authenticate. Reason: " + (object) userLoggingReason);
          ((SitefinityIdentity) context.User.Identity).SetAuthenticationType("Basic");
          return true;
        }
      }
      return false;
    }

    private static void EndResponse(HttpContextBase context, UserLoggingReason authResult)
    {
      context.Response.Write(SecurityManager.GetLoginResultText(authResult));
      context.Response.End();
    }

    internal static void RevokeCurrentUser(HttpContextBase context)
    {
      if (context == null)
        throw new ArgumentNullException(nameof (context));
      SecurityManager.BuildLogoutCookie(UserLoggingReason.UserRevoked, context);
      if (context.User != null && context.User.Identity is FormsSitefinityIdentity identity && identity.IsAuthenticated)
      {
        if (SecurityManager.FindUserById(identity.UserId, identity.MembershipProvider) != null)
          SecurityManager.Logout(identity.MembershipProvider, identity.UserId);
        context.User = (IPrincipal) new ClaimsPrincipal((IIdentity) SitefinityIdentity.GetAnonymous());
      }
      SecurityManager.DeleteAuthCookies();
    }

    /// <summary>Updates the roles cookie.</summary>
    /// <param name="context">The context.</param>
    public static void UpdateCookies(HttpContextBase context)
    {
      if (context.User == null)
        return;
      if (context.User.Identity.IsAuthenticated && context.User.Identity.AuthenticationType == "Basic")
      {
        SecurityManager.Logout();
      }
      else
      {
        if (!(context.User.Identity is FormsSitefinityIdentity identity) || !identity.RolesChanged || !identity.IsAuthenticated || !context.Request.Browser.Cookies)
          return;
        SecurityConfig securityConfig = Config.Get<SecurityConfig>();
        string str = SecurityManager.EncryptData(identity.SerializeRoles());
        if (string.IsNullOrEmpty(str) || str.Length > 4096)
        {
          SecurityManager.DeleteCookie(securityConfig.RolesCookieName, securityConfig.RolesCookiePath, securityConfig.RolesCookieDomain, securityConfig.RolesCookieRequireSsl);
        }
        else
        {
          HttpCookie cookie = new HttpCookie(securityConfig.RolesCookieName, str)
          {
            HttpOnly = true,
            Path = securityConfig.RolesCookiePath,
            Domain = securityConfig.RolesCookieDomain
          };
          cookie.Expires = identity.RolesCookieExpireDate;
          cookie.Secure = securityConfig.RolesCookieRequireSsl;
          try
          {
            if (context.Response.Cookies.Get(cookie.Name) != null)
              context.Response.Cookies.Remove(cookie.Name);
            context.Response.Cookies.Add(cookie);
          }
          catch (HttpException ex)
          {
          }
        }
      }
    }

    /// <summary>Deletes a cookie from the current response.</summary>
    /// <param name="cookieName">The name of the cookie.</param>
    /// <param name="path">The cookie path.</param>
    /// <param name="domain">The cookie domain.</param>
    /// <param name="requireSsl">if set to <c>true</c> [requires SSL].</param>
    public static void DeleteCookie(
      string cookieName,
      string path,
      string domain,
      bool requireSsl)
    {
      HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
      if (currentHttpContext == null || !currentHttpContext.Request.Browser.Cookies)
        return;
      string str = string.Empty;
      if (SecurityManager.SupportsEmptyStringInCookieValue(currentHttpContext.Request) == "false")
        str = "NoCookie";
      HttpCookie cookie = new HttpCookie(cookieName, str)
      {
        HttpOnly = true,
        Path = path,
        Domain = domain,
        Expires = new DateTime(1999, 10, 12),
        Secure = requireSsl
      };
      try
      {
        currentHttpContext.Request.Cookies.Remove(cookieName);
        currentHttpContext.Response.Cookies.Remove(cookieName);
        currentHttpContext.Response.Cookies.Add(cookie);
      }
      catch
      {
      }
    }

    /// <summary>
    /// Gets a string representation cryptographically strong sequence of hexadecimal random values.
    /// </summary>
    /// <param name="byteLength">
    /// Length of the key in bytes.
    /// The returned string will be double the specified length as each two chars
    /// represent one hexadecimal value.
    /// </param>
    /// <returns>The key.</returns>
    public static string GetRandomKey(int byteLength)
    {
      byte[] numArray = new byte[byteLength];
      SecurityManager.rng.GetBytes(numArray);
      return Utility.BytesToHex(numArray);
    }

    /// <summary>
    /// Gets a string representation cryptographically strong sequence of hexadecimal random values.
    /// </summary>
    /// <param name="byteLength">
    /// Length of the key in bytes.
    /// The returned string will be double the specified length as each two chars
    /// represent one hexadecimal value.
    /// </param>
    /// <returns>The key.</returns>
    public static byte[] GetRandomByteKey(int byteLength)
    {
      byte[] data = new byte[byteLength];
      SecurityManager.rng.GetBytes(data);
      return data;
    }

    /// <summary>Hexes to byte.</summary>
    /// <param name="hexString">The hex string.</param>
    /// <returns></returns>
    public static byte[] HexToByte(string hexString) => Utility.HexToBytes(hexString);

    /// <summary>Computes the hash.</summary>
    /// <param name="data">The data.</param>
    /// <returns></returns>
    internal static string ComputeHash(string data) => Convert.ToBase64String(SecurityManager.ComputeHash(Encoding.Unicode.GetBytes(data)));

    /// <summary>Computes the hash.</summary>
    /// <param name="data">The data.</param>
    /// <param name="key">The hash key that will be used to compute the result.</param>
    /// <returns></returns>
    internal static string ComputeHash(string data, string key) => Convert.ToBase64String(SecurityManager.ComputeHash(Encoding.Unicode.GetBytes(data), key));

    /// <summary>Computes the hash.</summary>
    /// <param name="data">The data.</param>
    /// <returns></returns>
    internal static byte[] ComputeHash(byte[] data)
    {
      string key = (string) null;
      return SecurityManager.ComputeHash(data, key);
    }

    /// <summary>Computes the hash.</summary>
    /// <param name="data">The data.</param>
    /// <param name="key">The hash key that will be used to compute the result.
    /// If no hash key has been provided (null) the default one from SecurityConfig validation key will be used.</param>
    /// <returns></returns>
    internal static byte[] ComputeHash(byte[] data, string key)
    {
      if (string.IsNullOrWhiteSpace(key))
        key = EnvironmentVariables.Current.GetValidationKey();
      if (key == "Auto")
        return data;
      byte[] hashKey = SecurityManager.HexToByte(key);
      return SecurityManager.ComputeHash(data, hashKey);
    }

    /// <summary>Computes the hash.</summary>
    /// <param name="data">The data.</param>
    /// <param name="hashKey">The key that will be used to compute the hash.</param>
    /// <returns></returns>
    internal static byte[] ComputeHash(byte[] data, byte[] hashKey)
    {
      using (HMACSHA1 hmacshA1 = new HMACSHA1())
      {
        hmacshA1.Key = hashKey;
        return hmacshA1.ComputeHash(data);
      }
    }

    /// <summary>
    /// Encrypts the data using TripleDes algorith and the Sitefinity internal security key.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <returns></returns>
    public static string EncryptData(string data)
    {
      string decryptionKey = EnvironmentVariables.Current.GetDecryptionKey();
      return CryptographyManager.EncryptData(data, decryptionKey);
    }

    /// <summary>
    /// Decrypts the data using TripleDes algorith and the Sitefinity internal security key.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <returns></returns>
    public static string DecryptData(string data)
    {
      string decryptionKey = EnvironmentVariables.Current.GetDecryptionKey();
      return CryptographyManager.DecryptData(data, decryptionKey);
    }

    /// <summary>
    /// Determines whether the number of simultaneous logged in users licensing limit is reached.
    /// </summary>
    /// <returns>
    /// 	<c>true</c> if the licensed user limit is reached; otherwise, <c>false</c>.
    /// </returns>
    internal static bool IsUserLimitReached()
    {
      if (LicenseState.Current.LicenseInfo.Users == 0)
        return false;
      DateTime expTime = SecurityManager.ExpiredSessionsLastLoginDate;
      UserActivityManager manager = UserActivityManager.GetManager();
      int num;
      using (new ReadUncommitedRegion((IManager) manager))
        num = manager.UserActivities().Where<UserActivity>((Expression<Func<UserActivity, bool>>) (ua => ua.IsBackendUser == true && ua.IsLoggedIn == true && ua.LastActivityDate > expTime)).Count<UserActivity>();
      int users = LicenseState.Current.LicenseInfo.Users;
      return num >= users;
    }

    /// <summary>Gets the auth cookie timeout.</summary>
    /// <value>The auth cookie timeout.</value>
    internal static TimeSpan AuthCookieTimeout => Config.Get<SecurityConfig>().AuthCookieTimeout;

    /// <summary>Updates the user activity.</summary>
    /// <param name="userId">The user id.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="lastActivityDate">last activity date</param>
    internal static void UpdateUserActivity(
      Guid userId,
      string providerName,
      DateTime lastActivityDate)
    {
      if (!Bootstrapper.IsReady)
        return;
      lock (SecurityManager.userActivitySyncObject)
      {
        string transactionName = "UserActivity";
        using (UserActivityManager userActivityManager = new UserActivityManager("OpenAccessUserActivityProvider", transactionName))
        {
          if (userActivityManager.Suspended)
            return;
          try
          {
            userActivityManager.GetUserActivity(userId, providerName).LastActivityDate = lastActivityDate;
            TransactionManager.CommitTransaction(transactionName);
          }
          finally
          {
            TransactionManager.DisposeTransaction(transactionName);
          }
        }
      }
    }

    /// <summary>Updates the user activity.</summary>
    /// <param name="record">The record.</param>
    internal static void UpdateUserActivity(UserActivityRecord record) => SecurityManager.UpdateUserActivity(record.UserId, record.ProviderName, record.LastActivityDate);

    /// <summary>Gets the logged in backend users count.</summary>
    /// <returns></returns>
    internal static int GetLoggedInBackendUsersCount()
    {
      DateTime expTime = SecurityManager.ExpiredSessionsLastLoginDate;
      UserActivityManager manager = UserActivityManager.GetManager();
      using (new ReadUncommitedRegion((IManager) manager))
        return manager.UserActivities().Where<UserActivity>((Expression<Func<UserActivity, bool>>) (ua => ua.IsBackendUser == true && ua.IsLoggedIn == true && ua.LastActivityDate > expTime)).Count<UserActivity>();
    }

    /// <summary>
    ///  If the user is logged in before this date, his/her session is expired
    /// </summary>
    /// <value>The session expiration time.</value>
    internal static DateTime ExpiredSessionsLastLoginDate => DateTime.UtcNow - SecurityManager.BackendUsersSessionTimeout;

    /// <summary>Gets the server session timeout.</summary>
    /// <value>The server session timeout</value>
    internal static TimeSpan BackendUsersSessionTimeout => Config.Get<SecurityConfig>().BackendUsersSessionTimeout;

    /// <summary>Gets the current user id.</summary>
    /// <value>The current user id.</value>
    public static Guid CurrentUserId
    {
      get
      {
        SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
        return currentIdentity != null ? currentIdentity.UserId : Guid.Empty;
      }
    }

    internal static RoleInfo AdminRole => SecurityManager.GetAppRoleOrDefault("Administrators");

    /// <summary>Gets the backend users role.</summary>
    /// <value>The back end users role.</value>
    internal static RoleInfo BackEndUsersRole => SecurityManager.GetAppRoleOrDefault("BackendUsers");

    /// <summary>Gets the editors role.</summary>
    /// <value>The editors role.</value>
    internal static RoleInfo EditorsRole => SecurityManager.GetAppRoleOrDefault("Editors");

    /// <summary>Gets the authors role.</summary>
    /// <value>The authors role.</value>
    internal static RoleInfo AuthorsRole => SecurityManager.GetAppRoleOrDefault("Authors");

    /// <summary>Gets the designers role.</summary>
    /// <value>The designers role.</value>
    internal static RoleInfo DesignersRole => SecurityManager.GetAppRoleOrDefault("Designers");

    /// <summary>Gets the everyone role.</summary>
    /// <value>The everyone role.</value>
    internal static RoleInfo EveryoneRole => SecurityManager.GetAppRoleOrDefault("Everyone");

    /// <summary>Gets the logged in backend users.</summary>
    /// <returns></returns>
    public static IList<User> GetLoggedInBackendUsers()
    {
      List<User> loggedInBackendUsers = new List<User>();
      UserActivityManager manager = UserActivityManager.GetManager();
      using (new ReadUncommitedRegion((IManager) manager))
      {
        IQueryable<UserActivity> source = manager.UserActivities();
        Expression<Func<UserActivity, bool>> predicate = (Expression<Func<UserActivity, bool>>) (ua => ua.IsLoggedIn == true && ua.IsBackendUser == true && ua.LastActivityDate > SecurityManager.ExpiredSessionsLastLoginDate);
        foreach (UserActivity userActivity in source.Where<UserActivity>(predicate).ToList<UserActivity>())
        {
          User user = UserManager.GetManager(userActivity.ProviderName).GetUser(userActivity.UserId);
          if (user != null)
            loggedInBackendUsers.Add(user);
        }
      }
      return (IList<User>) loggedInBackendUsers;
    }

    /// <summary>
    /// Gets the number of logged in backend users for a specific provider or for all providers if <paramref name="provider" /> is empty.
    /// </summary>
    /// <param name="provider">The name of the provider.</param>
    /// <returns>The number of logged in backend users.</returns>
    public static int GetLoggedInBackendUsersCount(string providerName)
    {
      DateTime expTime = SecurityManager.ExpiredSessionsLastLoginDate;
      UserActivityManager manager = UserActivityManager.GetManager();
      using (new ReadUncommitedRegion((IManager) manager))
      {
        if (string.IsNullOrEmpty(providerName))
          return manager.UserActivities().Where<UserActivity>((Expression<Func<UserActivity, bool>>) (ua => ua.IsLoggedIn == true && ua.IsBackendUser == true && ua.LastActivityDate > expTime)).Count<UserActivity>();
        return manager.UserActivities().Where<UserActivity>((Expression<Func<UserActivity, bool>>) (ua => ua.IsLoggedIn == true && ua.IsBackendUser == true && ua.ProviderName == providerName && ua.LastActivityDate > expTime)).Count<UserActivity>();
      }
    }

    /// <summary>This method is created to ease making of unit tests</summary>
    /// <param name="userId"></param>
    /// <param name="providerName"></param>
    /// <returns></returns>
    private static User FindUserById(Guid userId, string providerName) => UserManager.GetManager(providerName).GetUser(userId);

    /// <summary>Verifies the authentication in the database.</summary>
    /// <param name="principal">The principal.</param>
    /// <param name="loginIp">The login ip.</param>
    /// <param name="lastLoginStamp">The last login stamp.</param>
    /// <returns></returns>
    private static UserLoggingReason VerifyAuthenticateInTheDatabase(
      ClaimsPrincipal principal,
      string loginIp,
      string lastLoginStamp)
    {
      SitefinityIdentity identity = (SitefinityIdentity) principal.Identity;
      string membershipProvider = identity.MembershipProvider;
      string tokenId = identity.TokenId;
      Guid userId = identity.UserId;
      User user = (User) null;
      if (userId != Guid.Empty)
        user = SecurityManager.FindUserById(userId, membershipProvider);
      if (user == null)
        return UserLoggingReason.UserNotFound;
      UserActivityManager manager = UserActivityManager.GetManager();
      UserActivity userActivity = manager.GetUserActivity(user.Id, membershipProvider);
      if (userActivity == null)
        return UserLoggingReason.UserLoggedOff;
      if (!userActivity.IsBackendUser)
      {
        List<Guid> list = SystemManager.MultisiteContext.GetAllowedSites(user.Id, user.ProviderName).ToList<Guid>();
        SecurityManager.UpdateLastActivityDate(manager, userActivity, user, loginIp, list, membershipProvider, tokenId);
        return UserLoggingReason.Success;
      }
      if (!userActivity.IsLoggedIn)
        return UserLoggingReason.UserLoggedOff;
      if (userActivity.LastActivityDate < SecurityManager.ExpiredSessionsLastLoginDate || SecurityManager.TokenIdChanged(identity.TokenId, userActivity.TokenId))
        return UserLoggingReason.SessionExpired;
      if (!SecurityManager.IsToDisableActiveUserLoginsLimitation())
      {
        if (SecurityManager.AuthenticationMode == Telerik.Sitefinity.Security.Configuration.AuthenticationMode.Claims)
        {
          if (!SecurityManager.IsToDisableActiveUserLoginsLimitation(principal) && userActivity.TokenId != identity.TokenId)
            return UserLoggingReason.UserLoggedFromDifferentComputer;
        }
        else if (!SecurityManager.EqualDates(userActivity.LastLoginDate, lastLoginStamp))
          return UserLoggingReason.UserLoggedFromDifferentComputer;
      }
      List<Guid> list1 = SystemManager.MultisiteContext.GetAllowedSites(user.Id, user.ProviderName).ToList<Guid>();
      SecurityManager.UpdateLastActivityDate(manager, userActivity, user, loginIp, list1, membershipProvider, tokenId);
      return UserLoggingReason.Success;
    }

    private static void UpdateLastActivityDate(
      UserActivityManager manager,
      UserActivity userActivity,
      User user,
      string loginIp,
      List<Guid> allowedSites,
      string providerName,
      string tokenId)
    {
      UserActivityRecord fromCache = manager.GetFromCache(user.Id);
      if (fromCache == null)
      {
        lock (SecurityManager.recordLock)
        {
          if (manager.GetFromCache(user.Id) != null)
            return;
          SecurityManager.UpdateLastActivityDateInDb(manager, userActivity, user, loginIp, allowedSites, providerName, tokenId);
        }
      }
      else
      {
        UserActivityRecord userActivityRecord = fromCache;
        lock (SecurityManager.recordLock)
        {
          if (manager.GetFromCache(user.Id) != userActivityRecord)
            return;
          SecurityManager.UpdateLastActivityDateInDb(manager, userActivity, user, loginIp, allowedSites, providerName, tokenId);
        }
      }
    }

    private static void UpdateLastActivityDateInDb(
      UserActivityManager manager,
      UserActivity userActivity,
      User user,
      string loginIp,
      List<Guid> allowedSites,
      string providerName,
      string tokenId)
    {
      DateTime utcNow = DateTime.UtcNow;
      manager.AddToCache(user.Id, loginIp, providerName, true, userActivity.LastLoginDate, utcNow, allowedSites, tokenId.ToString());
      UserActivityBuffer.Instance.AddUserActivity(providerName, user.Id, utcNow);
    }

    /// <summary>
    /// Verifies the login request.
    /// Method first is trying to use cache. If cache record is not existing it goes to the database.
    /// and makes the verification.
    /// </summary>
    /// <param name="principal"></param>
    /// <param name="loginIp">The login ip.</param>
    /// <param name="lastLoginStamp">Last login date serialized as string.</param>
    /// <returns>UserLoginResult describes the result of the verification</returns>
    internal static UserLoggingReason VerifyAuthenticateRequest(
      SitefinityPrincipal principal,
      string loginIp,
      string lastLoginStamp)
    {
      SitefinityIdentity identity = (SitefinityIdentity) principal.Identity;
      Guid userId = identity.UserId;
      if (string.Compare(userId.ToString(), SecurityManager.SystemAccountIDs[0], true) == 0)
        return UserLoggingReason.Success;
      UserActivityRecord fromCache = UserActivityManager.GetManager().GetFromCache(userId);
      if (fromCache == null)
        return SecurityManager.VerifyAuthenticateInTheDatabase((ClaimsPrincipal) principal, loginIp, lastLoginStamp);
      return !fromCache.IsBackendUser || !(fromCache.LastActivityDate < SecurityManager.ExpiredSessionsLastLoginDate) && !(fromCache.LoginIP != loginIp) && SecurityManager.EqualDates(fromCache.LastLoginDate, lastLoginStamp) && !SecurityManager.TokenIdChanged(identity.TokenId, fromCache.TokenId) ? UserLoggingReason.Success : SecurityManager.VerifyAuthenticateInTheDatabase((ClaimsPrincipal) principal, loginIp, lastLoginStamp);
    }

    private static bool TokenIdChanged(string tokenId, string newTokenId)
    {
      if (!(tokenId != newTokenId))
        return false;
      return !string.IsNullOrWhiteSpace(tokenId) || !string.IsNullOrWhiteSpace(newTokenId);
    }

    /// <summary>
    /// Compare dates with seconds precision converting them as strings
    /// </summary>
    /// <param name="universalDate"></param>
    /// <param name="universalDateString"></param>
    /// <returns></returns>
    private static bool EqualDates(DateTime universalDate, string universalDateString)
    {
      string str1 = universalDate.ToString("yyyyMMddHHmmss");
      string empty = string.Empty;
      DateTime result;
      if (!DateTime.TryParseExact(universalDateString, "u", (IFormatProvider) null, DateTimeStyles.AdjustToUniversal, out result))
        return str1 == universalDateString;
      string str2 = result.ToString("yyyyMMddHHmmss");
      return str1 == str2;
    }

    internal static UserLoggingReason VerifyLoginRequest(
      User user,
      string loginIp,
      DateTime issueDate,
      out List<Guid> allowedAccessSiteIDs,
      string tokenId = null,
      bool isToDisableUsersLoginsLimitation = false)
    {
      allowedAccessSiteIDs = (List<Guid>) null;
      if (user == null)
        return UserLoggingReason.UserNotFound;
      IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
      if (multisiteContext != null)
      {
        allowedAccessSiteIDs = multisiteContext.GetAllowedSites(user.Id, user.ProviderName).ToList<Guid>();
        if (!SecurityManager.GuardSiteBackendAccessible(user, allowedAccessSiteIDs))
          return UserLoggingReason.SiteAccessNotAllowed;
      }
      using (UserActivityManager manager = UserActivityManager.GetManager())
      {
        UserActivity userActivity = manager.GetUserActivity(user.Id, user.ProviderName);
        if (userActivity == null)
          return SecurityManager.IsUserLimitReached() ? UserLoggingReason.UserLimitReached : UserLoggingReason.Success;
        string tokenIdFromCookie = SecurityManager.GetTokenIdFromCookie();
        if (SecurityManager.AuthenticationMode == Telerik.Sitefinity.Security.Configuration.AuthenticationMode.Forms || userActivity.TokenId == null || userActivity.TokenId != tokenId && userActivity.TokenId != tokenIdFromCookie)
        {
          if (userActivity.IsLoggedIn && userActivity.LastActivityDate > SecurityManager.ExpiredSessionsLastLoginDate && !SecurityManager.IsToDisableActiveUserLoginsLimitation() && !isToDisableUsersLoginsLimitation && (!SecurityManager.IsToLogOutUsersFromDifferentClientsOnLogin() || SecurityManager.IsCurrentRequestUrlEndsWithSignOutPage()))
            return UserLoggingReason.UserAlreadyLoggedIn;
          if (SecurityManager.IsUserLimitReached())
            return UserLoggingReason.UserLimitReached;
          if (SecurityManager.IsCurrentRequestUrlEndsWithLoginExceptionPage())
            return UserLoggingReason.LoginFailedWithError;
        }
        return UserLoggingReason.Success;
      }
    }

    private static bool GuardSiteBackendAccessible(User user, List<Guid> allowedAccessSiteIDs)
    {
      ISiteContext siteContextWithUser = SecurityManager.GetCurrentSiteContextWithUser(user);
      if (!SystemManager.IsBackendRequest() || allowedAccessSiteIDs.Contains(siteContextWithUser.Site.Id))
        return true;
      SecurityManager.TryRedirectToFirstAccessibleSite(allowedAccessSiteIDs, siteContextWithUser);
      return false;
    }

    private static ISiteContext GetCurrentSiteContextWithUser(User user)
    {
      HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
      if (currentHttpContext != null)
        currentHttpContext.Items[(object) "sfMultisiteContextUserId"] = (object) new Tuple<Guid, string>(user.Id, user.ProviderName);
      IMultisiteContext multisiteContext1 = SystemManager.CurrentContext.MultisiteContext;
      if (multisiteContext1 is MultisiteContext multisiteContext2)
        multisiteContext2.InvalidateCache();
      ISiteContext currentSiteContext = multisiteContext1.CurrentSiteContext;
      if (currentHttpContext == null)
        return currentSiteContext;
      currentHttpContext.Items.Remove((object) "sfMultisiteContextUserId");
      return currentSiteContext;
    }

    private static bool TryRedirectToFirstAccessibleSite(
      List<Guid> allowedAccessSiteIDs,
      ISiteContext currentSiteContext)
    {
      HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
      if (currentHttpContext == null || allowedAccessSiteIDs.Count == 0 || currentSiteContext.ResolutionType != SiteContextResolutionTypes.ByDomain)
        return false;
      IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
      ISite site1 = multisiteContext.GetSiteById(allowedAccessSiteIDs[0]);
      if (allowedAccessSiteIDs.Count > 1 && currentSiteContext.Site is MultisiteContext.SiteProxy site2)
      {
        foreach (Guid allowedAccessSiteId in allowedAccessSiteIDs)
        {
          if (multisiteContext.GetSiteById(allowedAccessSiteId) is MultisiteContext.SiteProxy siteById && site2.Contains(siteById))
          {
            site1 = (ISite) siteById;
            break;
          }
        }
      }
      string rawUrl = currentHttpContext.Request.RawUrl;
      int num = rawUrl.IndexOf("?");
      string url;
      if (num != -1)
      {
        QueryStringBuilder queryStringBuilder = new QueryStringBuilder(rawUrl.Substring(num));
        queryStringBuilder.Remove("sf_site");
        queryStringBuilder.Add("sf_site", site1.Id.ToString());
        url = rawUrl.Substring(0, num) + queryStringBuilder.ToString();
      }
      else
        url = rawUrl + "?sf_site=" + site1.Id.ToString();
      currentHttpContext.Response.Redirect(url, true);
      return true;
    }

    /// <summary>Verifies the user IP.</summary>
    /// <param name="userId">The user id.</param>
    /// <param name="ip">The IP.</param>
    /// <returns></returns>
    public static bool VerifyUserIp(Guid userId, string ip)
    {
      UserActivityRecord fromCache = UserActivityManager.GetManager().GetFromCache(userId);
      return fromCache != null ? fromCache.LoginIP == ip : SecurityManager.GetUser(userId).LastLoginIp == ip;
    }

    /// <summary>Verifies the user IP.</summary>
    /// <param name="u">The user object.</param>
    /// <param name="ip">The last login IP.</param>
    /// <returns></returns>
    public static bool VerifyUserIp(User u, string ip) => !u.IsLoggedIn || string.IsNullOrEmpty(u.LastLoginIp) || !(u.LastLoginIp != ip);

    /// <summary>
    /// Registers the user login in the database and in the server cache.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <param name="membershipProvider">The membership provider.</param>
    /// <param name="rememberMe">if set to <c>true</c> [remember me].</param>
    /// <param name="issueDate">the issue date of the cookie/STS token</param>
    /// <param name="tokenId">The STS token id (if present).</param>
    /// <param name="manager"></param>
    /// <param name="setAuthenticationCookie"></param>
    internal static void RegisterUserLogin(
      User user,
      UserManager manager,
      bool rememberMe,
      DateTime issueDate,
      string tokenId = null,
      List<Guid> allowedAccessSiteIDs = null)
    {
      string userHostAddress = SystemManager.CurrentHttpContext.Request.UserHostAddress;
      IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
      if (allowedAccessSiteIDs == null && multisiteContext != null)
        allowedAccessSiteIDs = multisiteContext.GetAllowedSites(user.Id, user.ProviderName).ToList<Guid>();
      using (UserActivityManager manager1 = UserActivityManager.GetManager())
      {
        UserActivity userActivity = manager1.GetUserActivity(user.Id, user.ProviderName);
        userActivity.IsLoggedIn = true;
        userActivity.LoginIP = userHostAddress;
        userActivity.LastActivityDate = issueDate;
        userActivity.TokenId = tokenId;
        manager1.SaveChanges();
        user.LastLoginIp = userHostAddress;
        if (SecurityManager.AuthenticationMode == Telerik.Sitefinity.Security.Configuration.AuthenticationMode.Forms)
          SecurityManager.SetAuthenticationCookie(SystemManager.CurrentHttpContext.Response, manager.Provider.Name, user, rememberMe, issueDate);
        if (!string.IsNullOrWhiteSpace(tokenId) && !SystemManager.CurrentContext.IsServiceRequest)
          SecurityManager.SetTokenIdCookie(tokenId);
        manager1.AddToCache(user.Id, userHostAddress, manager.Provider.Name, user.IsBackendUser, issueDate, issueDate, allowedAccessSiteIDs, tokenId);
      }
    }

    private static void SetTokenIdCookie(string tokenId)
    {
      if (SystemManager.CurrentContext.IsServiceRequest)
        return;
      HttpRequestBase request = SystemManager.CurrentHttpContext.Request;
      HttpResponseBase response = SystemManager.CurrentHttpContext.Response;
      HttpCookie cookie = new HttpCookie(SecurityManager.TokenIdCookieName, tokenId)
      {
        Path = "/",
        Expires = DateTime.UtcNow.Add(SecurityManager.BackendUsersSessionTimeout),
        SameSite = SameSiteMode.Lax
      };
      request.Cookies.Remove(SecurityManager.TokenIdCookieName);
      response.Cookies.Remove(SecurityManager.TokenIdCookieName);
      response.Cookies.Add(cookie);
    }

    private static string GetTokenIdFromCookie()
    {
      HttpRequestBase request = SystemManager.CurrentHttpContext.Request;
      string tokenIdFromCookie = (string) null;
      if (request.Cookies.Keys.Contains(SecurityManager.TokenIdCookieName))
      {
        HttpCookie cookie = request.Cookies[SecurityManager.TokenIdCookieName];
        if (cookie.Expires < DateTime.UtcNow)
          tokenIdFromCookie = cookie.Value;
      }
      return tokenIdFromCookie;
    }

    /// <summary>
    /// Gets the IDs of application roles to which users should not be assigned.
    /// </summary>
    public static List<Guid> UnassignableRoles => SecurityManager.CurrentSettings.UnassignableRoles.Select<RoleInfo, Guid>((Func<RoleInfo, Guid>) (r => r.Id)).ToList<Guid>();

    /// <summary>
    /// Sets the currently logged in user IsLoggedIn to either true or false upon request.
    /// </summary>
    /// <param name="newStatus">The new status.</param>
    internal static void SetUserLoggedInStatus(bool newStatus)
    {
      SitefinityIdentity identity = SystemManager.CurrentHttpContext.User.Identity as SitefinityIdentity;
      UserActivityManager manager = UserActivityManager.GetManager();
      UserActivity userActivity;
      if (identity != null)
      {
        userActivity = manager.GetUserActivity(identity.UserId, identity.MembershipProvider);
      }
      else
      {
        User user = UserManager.FindUser(ClaimsManager.GetUserId((SystemManager.CurrentHttpContext.User.Identity as ClaimsIdentity).Claims, false));
        userActivity = manager.GetUserActivity(user.Id, user.ProviderName);
      }
      if (userActivity == null)
        return;
      userActivity.IsLoggedIn = newStatus;
      manager.SaveChanges();
    }

    /// <summary>
    ///     <para>Gets the localized error text for the given login result.</para>
    ///     <para>Only error messages are localized. If result is Success it returns an empty string.</para>
    /// </summary>
    /// <param name="loginResult"></param>
    /// <returns>Localized message containing the login result.</returns>
    public static string GetLoginResultText(UserLoggingReason loginResult)
    {
      string loginResultText = string.Empty;
      switch (loginResult)
      {
        case UserLoggingReason.UserLimitReached:
          loginResultText = Res.Get<Labels>().UserLimitReached;
          break;
        case UserLoggingReason.UserNotFound:
          loginResultText = Res.Get<Labels>().UserNotFound;
          break;
        case UserLoggingReason.UserLoggedFromDifferentIp:
          loginResultText = Res.Get<Labels>().UserLoggedFromDifferentIp;
          break;
        case UserLoggingReason.SessionExpired:
          loginResultText = Res.Get<Labels>().SessionExpired;
          break;
        case UserLoggingReason.UserLoggedOff:
          loginResultText = Res.Get<Labels>().UserLoggedOff;
          break;
        case UserLoggingReason.UserLoggedFromDifferentComputer:
          loginResultText = Res.Get<Labels>().UserLoggedFromDifferentComputer;
          break;
        case UserLoggingReason.Unknown:
          loginResultText = Res.Get<Labels>().UnknownReason;
          break;
        case UserLoggingReason.NeedAdminRights:
          loginResultText = Res.Get<Labels>().NeedAdminRights;
          break;
        case UserLoggingReason.UserAlreadyLoggedIn:
          loginResultText = Res.Get<Labels>().UserAlreadyLoggedIn;
          break;
        case UserLoggingReason.UserRevoked:
          loginResultText = Res.Get<Labels>().UserRevoked;
          break;
      }
      return loginResultText;
    }

    /// <summary>Assigns specified role to the user.</summary>
    /// <param name="user">The user.</param>
    /// <param name="roleManager">The role manager.</param>
    /// <param name="roleToAssign">The role to assign.</param>
    public static void AssignRoleToUser(User user, RoleManager roleManager, Role roleToAssign)
    {
      if (user == null)
        throw new ArgumentNullException(nameof (user));
      if (roleManager == null)
        throw new ArgumentNullException(nameof (roleManager));
      if (roleToAssign == null)
        throw new ArgumentNullException(nameof (roleToAssign));
      if (roleManager.Provider.Abilities.Keys.Contains<string>("AssingUserToRole") && !roleManager.Provider.Abilities["AssingUserToRole"].Supported || roleToAssign.GetProviderName() == "AppRoles" && roleToAssign.Name != "Users")
        return;
      bool suppressSecurityChecks = roleManager.Provider.SuppressSecurityChecks;
      try
      {
        roleManager.Provider.SuppressSecurityChecks = true;
        roleManager.AddUserToRole(user, roleToAssign);
      }
      catch (Exception ex)
      {
        throw;
      }
      finally
      {
        roleManager.Provider.SuppressSecurityChecks = suppressSecurityChecks;
      }
    }

    internal static ISubscriberRequest GetSubscriberObject(Guid userId) => SecurityManager.GetSubscriberObject(SecurityManager.GetUser(userId));

    internal static ISubscriberRequest GetSubscriberObject(User user)
    {
      SecurityManager.UserSubscriber subscriberObject = new SecurityManager.UserSubscriber()
      {
        Email = user.Email,
        ResolveKey = SecurityManager.GetUserKey(user)
      };
      string fullName = typeof (SitefinityProfile).FullName;
      if (UserProfileManager.GetManager(UserProfilesHelper.GetProfileTypeSettings(fullName).ProfileProvider).GetUserProfile(user.Id, fullName) is SitefinityProfile userProfile)
      {
        subscriberObject.FirstName = userProfile.FirstName;
        subscriberObject.LastName = userProfile.LastName;
      }
      else
      {
        subscriberObject.FirstName = user.FirstName;
        subscriberObject.LastName = user.LastName;
      }
      return (ISubscriberRequest) subscriberObject;
    }

    internal static string GetUserKey(User user) => user.Id.ToString() + ":" + user.ProviderName;

    /// <summary>
    /// Specifies if the active users sessions limitation should apply.
    /// </summary>
    /// <returns></returns>
    internal static bool IsToDisableActiveUserLoginsLimitation() => Config.Get<SecurityConfig>().DisableActiveUserLoginsLimitation && LicenseState.IsUnlimitedUsersLicense;

    /// <summary>
    /// Specifies if the active users sessions limitation should apply when claims authentication is used.
    /// </summary>
    /// <param name="principal">The principal.</param>
    /// <returns></returns>
    internal static bool IsToDisableActiveUserLoginsLimitation(ClaimsPrincipal principal) => Config.Get<SecurityConfig>().DisableActiveUserLoginsLimitation && principal.Identity is SitefinityIdentity identity && identity.StsType != null && identity.StsType.Equals("wa", StringComparison.InvariantCultureIgnoreCase);

    /// <summary>Logs out users from different clients on login.</summary>
    /// <returns></returns>
    internal static bool IsToLogOutUsersFromDifferentClientsOnLogin() => Config.Get<SecurityConfig>().LogOutUsersFromDifferentClientsOnLogin;

    /// <summary>
    /// Determines whether [is current request URL ends with sign out page].
    /// </summary>
    /// <returns></returns>
    internal static bool IsCurrentRequestUrlEndsWithSignOutPage()
    {
      string absolutePath = SystemManager.CurrentHttpContext.Request.Url.AbsolutePath;
      return absolutePath.EndsWith("Sitefinity/SignOut" + "/" + "forcelogout") && absolutePath.EndsWith("Sitefinity/SignOut" + "/" + "selflogout") && absolutePath.EndsWith("Sitefinity/SignOut" + "/" + "needadminrights") && absolutePath.EndsWith("Sitefinity/SignOut" + "/" + "sitenotaccessible");
    }

    internal static bool IsCurrentRequestUrlEndsWithLoginExceptionPage() => SystemManager.CurrentHttpContext.Request.Url.AbsolutePath.EndsWith("Sitefinity/SignOut" + "/" + "loginFailed");

    internal static bool TryGetUtcDate(string value, out DateTime datetime)
    {
      try
      {
        datetime = SecurityManager.GetUtcDate(value);
        return true;
      }
      catch
      {
        datetime = new DateTime();
        return false;
      }
    }

    internal static DateTime GetUtcDate(string value) => DateTime.SpecifyKind(DateTime.ParseExact(value, "u", (IFormatProvider) null, DateTimeStyles.AdjustToUniversal), DateTimeKind.Utc);

    internal static bool IsGlobalUserProvider(string providerName) => ((IEnumerable<string>) SecurityManager.globalUserProviders.Value).Contains<string>(providerName);

    /// <summary>
    /// Create a new user action with random id that is boud to this provider's application
    /// </summary>
    /// <returns>New user action</returns>
    public UserAction CreateUserAction() => this.Provider.CreateUserAction();

    /// <summary>
    /// Create a new user action with <paramref name="id" />. The action is bound to the
    /// current provider's application
    /// </summary>
    /// <param name="id">Id to set to the user action</param>
    /// <returns>New user action</returns>
    public UserAction CreateUserAction(Guid id) => this.Provider.CreateUserAction(id);

    /// <summary>
    /// Mark an <paramref name="action" /> for deletion
    /// </summary>
    /// <param name="action">User action to be marked for deletion</param>
    public void DeleteUserAction(UserAction action) => this.Provider.DeleteUserAction(action);

    /// <summary>Get action by id</summary>
    /// <param name="id">Id of the action to retrieve</param>
    /// <returns>User action or null</returns>
    public UserAction GetUserAction(Guid id) => this.Provider.GetUserAction(id);

    /// <summary>Get query for all user actions</summary>
    /// <returns>Queryable objects containing all user actions</returns>
    public IQueryable<UserAction> GetUserActions() => this.Provider.GetUserActions();

    private static string SupportsEmptyStringInCookieValue(HttpRequestBase request) => request.Browser["supportsEmptyStringInCookieValue"];

    private static void FormsAuthSignOut() => FormsAuthentication.SignOut();

    private static void CreateUserActivityWithTransaction(
      Guid userId,
      string loginIp,
      string providerName,
      bool isBackendUser,
      DateTime lastLoginDate,
      DateTime lastActivityDate,
      string tokenId = null)
    {
      using (UserActivityManager userActivityManager = new UserActivityManager("OpenAccessUserActivityProvider", "UserActivity"))
      {
        if (userActivityManager.Suspended)
          return;
        try
        {
          userActivityManager.CreateUserActivity(userId, loginIp, providerName, isBackendUser, lastLoginDate, lastActivityDate, tokenId);
          TransactionManager.CommitTransaction("UserActivity");
        }
        catch
        {
          throw;
        }
        finally
        {
          TransactionManager.DisposeTransaction("UserActivity");
        }
      }
    }

    private static RoleInfo GetAppRoleOrDefault(string roleName)
    {
      RoleInfo roleInfo;
      return !SecurityManager.CurrentSettings.AppRoles.TryGetValue(roleName, out roleInfo) ? (RoleInfo) null : roleInfo;
    }

    private static string[] GetGlobalUserProviders()
    {
      string[] array = ((IEnumerable<string>) Config.Get<SecurityConfig>().UsersPerSiteSettings.GlobalUsersProviderNames.Split(new char[1]
      {
        ','
      }, StringSplitOptions.RemoveEmptyEntries)).Select<string, string>((Func<string, string>) (s => s.Trim())).ToArray<string>();
      if (array.Length == 0)
        array = (ManagerBase<MembershipDataProvider>.StaticProvidersCollection ?? UserManager.GetManager().Providers).Select<MembershipDataProvider, string>((Func<MembershipDataProvider, string>) (p => p.Name)).ToArray<string>();
      return array;
    }

    public static string AuthenticationReturnUrl
    {
      get
      {
        string authenticationReturnUrl = Config.Get<SecurityConfig>().AuthenticationReturnUrl;
        return string.IsNullOrEmpty(authenticationReturnUrl) ? "ReturnUrl" : authenticationReturnUrl;
      }
    }

    internal class UserSubscriber : ISubscriberRequest
    {
      public string Email { get; set; }

      public string FirstName { get; set; }

      public string LastName { get; set; }

      public string ResolveKey { get; set; }

      public bool Disabled { get; set; }

      public IDictionary<string, string> CustomProperties { get; set; }
    }
  }
}
