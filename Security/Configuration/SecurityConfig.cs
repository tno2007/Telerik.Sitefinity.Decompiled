// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Configuration.SecurityConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Web.Security;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Configuration.Environment;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Libraries.UserFiles;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security.Data;
using Telerik.Sitefinity.Security.Ldap;
using Telerik.Sitefinity.Services.Configuration;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Security.Configuration
{
  /// <summary>Defines security configuration settings.</summary>
  [DescriptionResource(typeof (ConfigDescriptions), "SecurityConfig")]
  public class SecurityConfig : ConfigSection, IPermissionsConfig
  {
    /// <summary>The default issuer/realm placeholder key.</summary>
    internal const string DefaultRealmConfig = "http://localhost";

    /// <summary>Gets the notifications settings.</summary>
    /// <value>The notifications.</value>
    [ConfigurationProperty("notifications")]
    public NotificationsSettings Notifications => (NotificationsSettings) this["notifications"];

    [ConfigurationProperty("authenticationMode", DefaultValue = AuthenticationMode.Claims)]
    public virtual AuthenticationMode AuthenticationMode
    {
      get => (AuthenticationMode) this["authenticationMode"];
      set => this["authenticationMode"] = (object) value;
    }

    [ConfigurationProperty("accessControlAllowOrigin", DefaultValue = "")]
    [DescriptionResource(typeof (ConfigDescriptions), "CorsDescription")]
    public virtual string AccessControlAllowOrigin
    {
      get => (string) this["accessControlAllowOrigin"];
      set => this["accessControlAllowOrigin"] = (object) value;
    }

    [ConfigurationProperty("enableSessionMode", DefaultValue = false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public virtual bool EnableSessionMode
    {
      get => (bool) this["enableSessionMode"];
      set => this["enableSessionMode"] = (object) value;
    }

    [ConfigurationProperty("securityTokenIssuers")]
    [ConfigurationCollection(typeof (SecurityTokenKeyElement))]
    public virtual ConfigElementDictionary<string, SecurityTokenKeyElement> SecurityTokenIssuers => (ConfigElementDictionary<string, SecurityTokenKeyElement>) this["securityTokenIssuers"];

    [ConfigurationProperty("relyingParties")]
    [ConfigurationCollection(typeof (SecurityTokenKeyElement))]
    public virtual ConfigElementDictionary<string, SecurityTokenKeyElement> RelyingParties => (ConfigElementDictionary<string, SecurityTokenKeyElement>) this["relyingParties"];

    /// <summary>
    /// Gets or sets the value indicating whether to sign out from the STS.
    /// </summary>
    /// <value>The STS sign out.</value>
    [ConfigurationProperty("stsSignout", DefaultValue = false)]
    [DescriptionResource(typeof (ConfigDescriptions), "StsSignoutDescription")]
    public virtual bool StsSignout
    {
      get => (bool) this["stsSignout"];
      set => this["stsSignout"] = (object) value;
    }

    /// <summary>
    /// Defines the name of the cookie used for authentication tickets caching.
    /// </summary>
    [ConfigurationProperty("authCookieName", DefaultValue = ".SFAUTH")]
    [DescriptionResource(typeof (ConfigDescriptions), "AuthCookieNameDescription")]
    public virtual string AuthCookieName
    {
      get => (string) this["authCookieName"];
      set => this["authCookieName"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the virtual path of the cookie that is used to cache authentication tickets.
    /// </summary>
    [ConfigurationProperty("authCookiePath", DefaultValue = "/")]
    [DescriptionResource(typeof (ConfigDescriptions), "AuthCookiePathDescription")]
    public virtual string AuthCookiePath
    {
      get => (string) this["authCookiePath"];
      set => this["authCookiePath"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the name of the domain that is associated with the cookie used for caching authentication tickets.
    /// </summary>
    [ConfigurationProperty("authCookieDomain", DefaultValue = "")]
    [DescriptionResource(typeof (ConfigDescriptions), "AuthCookieDomainDescription")]
    public virtual string AuthCookieDomain
    {
      get => (string) this["authCookieDomain"];
      set => this["authCookieDomain"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the cookie that is used to cache authentication tickets will be reset periodically.
    /// </summary>
    [ConfigurationProperty("authCookieSlidingExpiration", DefaultValue = true)]
    [DescriptionResource(typeof (ConfigDescriptions), "AuthCookieSlidingExpirationDescription")]
    public virtual bool AuthCookieSlidingExpiration
    {
      get => (bool) this["authCookieSlidingExpiration"];
      set => this["authCookieSlidingExpiration"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the number of minutes before the cookie that is used to cache authentication tickets expires.
    /// (In claims authentication mode this value is the Relying Party's cookie timeout.
    /// You should enable AuthCookieIsPersistent in order to be able to control this value.
    /// You could also configure STS cookie timeout in the web.config of the STS application by configuring ASP.NET's Forms cookie timeout).
    /// </summary>
    [ConfigurationProperty("authCookieTimeout", DefaultValue = "600")]
    [TimeSpanValidator(MaxValueString = "10675199.02:48:05.4775807", MinValueString = "00:00:00")]
    [TypeConverter(typeof (TimeSpanMinutesConverter))]
    [DescriptionResource(typeof (ConfigDescriptions), "AuthCookieTimeoutDescription")]
    public virtual TimeSpan AuthCookieTimeout
    {
      get => (TimeSpan) this["authCookieTimeout"];
      set => this["authCookieTimeout"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the cookie that is used to cache authentication tickets
    /// requires a Secure Sockets Layer (SSL) connection in order to be returned to the server.
    /// </summary>
    [ConfigurationProperty("authCookieRequireSsl", DefaultValue = false)]
    [DescriptionResource(typeof (ConfigDescriptions), "AuthCookieRequireSslDescription")]
    public virtual bool AuthCookieRequireSsl
    {
      get => (bool) this["authCookieRequireSsl"];
      set => this["authCookieRequireSsl"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to require https for all requests.
    /// </summary>
    [ConfigurationProperty("requireHttpsForAllRequests", DefaultValue = false)]
    [DescriptionResource(typeof (ConfigDescriptions), "RequireHttpsForAllRequests")]
    public virtual bool RequireHttpsForAllRequests
    {
      get => (bool) this["requireHttpsForAllRequests"];
      set => this["requireHttpsForAllRequests"] = (object) value;
    }

    /// <summary>
    /// Defines the name of the cookie used for user roles caching.
    /// </summary>
    [DescriptionResource(typeof (ConfigDescriptions), "RolesCookieName")]
    [ConfigurationProperty("rolesCookieName", DefaultValue = ".SFROLES")]
    public virtual string RolesCookieName
    {
      get => (string) this["rolesCookieName"];
      set => this["rolesCookieName"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the virtual path of the cookie that is used to cache role names.
    /// </summary>
    [ConfigurationProperty("rolesCookiePath", DefaultValue = "/")]
    [DescriptionResource(typeof (ConfigDescriptions), "RolesCookiePathDescription")]
    public virtual string RolesCookiePath
    {
      get => (string) this["rolesCookiePath"];
      set => this["rolesCookiePath"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the name of the domain that is associated with the cookie that is used to cache role names.
    /// </summary>
    [ConfigurationProperty("rolesCookiePathDomain", DefaultValue = "")]
    [DescriptionResource(typeof (ConfigDescriptions), "RolesCookieDomainDescription")]
    public virtual string RolesCookieDomain
    {
      get => (string) this["rolesCookiePathDomain"];
      set => this["rolesCookiePathDomain"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the cookie that is used to cache role names will be reset periodically.
    /// </summary>
    [ConfigurationProperty("rolesCookieSlidingExpiration", DefaultValue = true)]
    [DescriptionResource(typeof (ConfigDescriptions), "RolesCookieSlidingExpirationDescription")]
    public virtual bool RolesCookieSlidingExpiration
    {
      get => (bool) this["rolesCookieSlidingExpiration"];
      set => this["rolesCookieSlidingExpiration"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the number of minutes before the cookie that is used to cache role names expires.
    /// </summary>
    [ConfigurationProperty("rolesCookieTimeout", DefaultValue = "30")]
    [TimeSpanValidator(MaxValueString = "10675199.02:48:05.4775807", MinValueString = "00:00:00")]
    [TypeConverter(typeof (TimeSpanMinutesConverter))]
    [DescriptionResource(typeof (ConfigDescriptions), "RolesCookieTimeoutDescription")]
    public virtual TimeSpan RolesCookieTimeout
    {
      get => (TimeSpan) this["rolesCookieTimeout"];
      set => this["rolesCookieTimeout"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the cookie that is used to cache role names
    /// requires a Secure Sockets Layer (SSL) connection in order to be returned to the server.
    /// </summary>
    [ConfigurationProperty("rolesCookieRequireSsl", DefaultValue = false)]
    [DescriptionResource(typeof (ConfigDescriptions), "RolesCookieRequireSslDescription")]
    public virtual bool RolesCookieRequireSsl
    {
      get => (bool) this["rolesCookieRequireSsl"];
      set => this["rolesCookieRequireSsl"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the length of time, in minutes, before a user is no longer considered to be online.
    /// </summary>
    [DescriptionResource(typeof (ConfigDescriptions), "UserIsOnlineTimeWindow")]
    [TimeSpanValidator(MaxValueString = "10675199.02:48:05.4775807", MinValueString = "00:01:00")]
    [ConfigurationProperty("userIsOnlineTimeWindow", DefaultValue = "15")]
    [TypeConverter(typeof (TimeSpanMinutesConverter))]
    public virtual TimeSpan UserIsOnlineTimeWindow
    {
      get => (TimeSpan) this["userIsOnlineTimeWindow"];
      set => this["userIsOnlineTimeWindow"] = (object) value;
    }

    /// <summary>A collection of defined permission sets.</summary>
    [DescriptionResource(typeof (ConfigDescriptions), "Permissions")]
    [ConfigurationProperty("permissions")]
    [ConfigurationCollection(typeof (Permission), AddItemName = "permission")]
    public virtual ConfigElementDictionary<string, Permission> Permissions => (ConfigElementDictionary<string, Permission>) this["permissions"];

    /// <summary>
    /// A collection of customized actions defined per secured object type and specific permission sets.
    /// </summary>
    [DescriptionResource(typeof (ConfigDescriptions), "CustomPermissionsDisplaySettings")]
    [ConfigurationProperty("customPermissionsDisplaySettings")]
    [ConfigurationCollection(typeof (SecuredObjectCustomPermissionSet), AddItemName = "customSet")]
    public virtual ConfigElementDictionary<string, CustomPermissionsDisplaySettingsConfig> CustomPermissionsDisplaySettings => (ConfigElementDictionary<string, CustomPermissionsDisplaySettingsConfig>) this["customPermissionsDisplaySettings"];

    /// <summary>A collection of defined application roles.</summary>
    [ConfigurationProperty("applicationRoles")]
    [ConfigurationCollection(typeof (ApplicationRole), AddItemName = "role")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ApplicationRolesDescription", Title = "ApplicationRolesTitle")]
    [Browsable(false)]
    public virtual ConfigElementDictionary<string, ApplicationRole> ApplicationRoles => (ConfigElementDictionary<string, ApplicationRole>) this["applicationRoles"];

    /// <summary>
    /// Gets or sets the name of the default data provider that is used to manage security.
    /// </summary>
    [DescriptionResource(typeof (ConfigDescriptions), "DefaultProvider")]
    [ConfigurationProperty("defaultSecurityProvider", DefaultValue = "OpenAccessDataProvider")]
    public virtual string DefaultSecurityProvider
    {
      get => (string) this["defaultSecurityProvider"];
      set => this["defaultSecurityProvider"] = (object) value;
    }

    /// <summary>Gets a collection of data provider settings.</summary>
    [ConfigurationProperty("securityProviders")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "SecurityProvidersDescription", Title = "SecurityProvidersTitle")]
    public virtual ConfigElementDictionary<string, DataProviderSettings> SecurityProviders => (ConfigElementDictionary<string, DataProviderSettings>) this["securityProviders"];

    /// <summary>
    /// Gets or sets the global resource class ID for retrieving localized strings.
    /// </summary>
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ResourceClassIdDescription", Title = "ResourceClassIdCaption")]
    [ConfigurationProperty("resourceClassId", DefaultValue = "SecurityResources")]
    public virtual string ResourceClassId
    {
      get => (string) this["resourceClassId"];
      set => this["resourceClassId"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the key that is used to validate encrypted data, or the process by which the key is generated.
    /// </summary>
    [ConfigurationProperty("validationKey", DefaultValue = "Auto")]
    [DescriptionResource(typeof (ConfigDescriptions), "ValidationKeyDescription")]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("Use Telerik.Sitefinity.Configuration.Environment.EnvironmentVariables.Current.GetValidationKey()")]
    public virtual string ValidationKey
    {
      get => (string) this["validationKey"];
      set => this["validationKey"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the key that is used to encrypt and decrypt data, or the process by which the key is generated.
    /// </summary>
    [ConfigurationProperty("decryptionKey", DefaultValue = "Auto")]
    [DescriptionResource(typeof (ConfigDescriptions), "DecryptionKeyDescription")]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("Use Telerik.Sitefinity.Configuration.Environment.EnvironmentVariables.Current.GetDecryptionKey()")]
    public virtual string DecryptionKey
    {
      get => (string) this["decryptionKey"];
      set => this["decryptionKey"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the key that is used to encrypt and decrypt data, or the process by which the key is generated.
    /// </summary>
    [ConfigurationProperty("desKey", DefaultValue = "q2veZH0E0I7eyHJu5P50ERO6UztpKIXx")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DesKeyDescription", Title = "DesKeyCaption")]
    public virtual string DesKey
    {
      get => (string) this["desKey"];
      set => this["desKey"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the name of the default role provider for backend users (site administrators).
    /// </summary>
    [DescriptionResource(typeof (ConfigDescriptions), "DefaultBackendRoleProvider")]
    [ConfigurationProperty("defaultBackendRoleProvider", DefaultValue = "Default")]
    public virtual string DefaultBackendRoleProvider
    {
      get => (string) this["defaultBackendRoleProvider"];
      set => this["defaultBackendRoleProvider"] = (object) value;
    }

    /// <summary>Gets a collection of data provider settings.</summary>
    [ConfigurationProperty("roleProviders")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "RoleProvidersDescription", Title = "RoleProvidersTitle")]
    public virtual ConfigElementDictionary<string, DataProviderSettings> RoleProviders => (ConfigElementDictionary<string, DataProviderSettings>) this["roleProviders"];

    /// <summary>
    /// Gets or sets the name of the default membership provider for backend users (site administrators).
    /// </summary>
    [DescriptionResource(typeof (ConfigDescriptions), "DefaultBackendMembershipProvider")]
    [ConfigurationProperty("defaultBackendMembershipProvider", DefaultValue = "Default")]
    public virtual string DefaultBackendMembershipProvider
    {
      get => (string) this["defaultBackendMembershipProvider"];
      set => this["defaultBackendMembershipProvider"] = (object) value;
    }

    /// <summary>Gets a collection of data provider settings.</summary>
    [ConfigurationProperty("membershipProviders")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "MembershipProvidersDescription", Title = "MembershipProvidersTitle")]
    public virtual ConfigElementDictionary<string, DataProviderSettings> MembershipProviders => (ConfigElementDictionary<string, DataProviderSettings>) this["membershipProviders"];

    /// <summary>Gets the administrative roles.</summary>
    /// <value>The administrative roles.</value>
    [DescriptionResource(typeof (ConfigDescriptions), "AdministrativeRoles")]
    [ConfigurationProperty("administrativeRoles")]
    [ConfigurationCollection(typeof (AdministrativeRole), AddItemName = "role")]
    public virtual ConfigElementList<AdministrativeRole> AdministrativeRoles => (ConfigElementList<AdministrativeRole>) this["administrativeRoles"];

    /// <summary>Get/set ldap the connections settings</summary>
    [ObjectInfo(typeof (ConfigDescriptions), Description = "LdapConnectionsDescription", Title = "LdapConnectionsConfigTitle")]
    [ConfigurationProperty("LdapConnections")]
    public virtual LdapConnectionsConfig LdapConnections
    {
      get => (LdapConnectionsConfig) this[nameof (LdapConnections)];
      set => this[nameof (LdapConnections)] = (object) value;
    }

    /// <summary>
    /// Defines the name of the cookie used for authentication tickets caching.
    /// </summary>
    [ConfigurationProperty("loggingCookieName", DefaultValue = ".SFLOG")]
    [DescriptionResource(typeof (ConfigDescriptions), "LoggingCookieNameDescription")]
    public virtual string LoggingCookieName
    {
      get => (string) this["loggingCookieName"];
      set => this["loggingCookieName"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the number of minutes before the backend users server tracked session exprires
    /// </summary>
    [ConfigurationProperty("backendUsersSessionTimeout", DefaultValue = "118")]
    [TimeSpanValidator(MaxValueString = "10675199.02:48:05.4775807", MinValueString = "00:00:00")]
    [TypeConverter(typeof (TimeSpanMinutesConverter))]
    [DescriptionResource(typeof (ConfigDescriptions), "BackendUsersSessionTimeoutDescription")]
    public virtual TimeSpan BackendUsersSessionTimeout
    {
      get => (TimeSpan) this["backendUsersSessionTimeout"];
      set => this["backendUsersSessionTimeout"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to filter queries by view permissions.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if filter queries by view permissions; otherwise, <c>false</c>.
    /// </value>
    [ObjectInfo(typeof (ConfigDescriptions), Description = "FilterQueriesByViewPermissionsDescription", Title = "FilterQueriesByViewPermissions")]
    [ConfigurationProperty("filterQueriesByViewPermissions", DefaultValue = false)]
    public virtual bool FilterQueriesByViewPermissions
    {
      get => (bool) this["filterQueriesByViewPermissions"];
      set => this["filterQueriesByViewPermissions"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether using of the external role providers defined in the web.config is allowed in Sitefinity.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if allow external role providers; otherwise, <c>false</c>.
    /// </value>
    [ConfigurationProperty("allowExternalRoleProviders", DefaultValue = true)]
    public virtual bool AllowExternalRoleProviders
    {
      get => (bool) this["allowExternalRoleProviders"];
      set => this["allowExternalRoleProviders"] = (object) value;
    }

    /// <summary>Gets or sets the configuration for the users cache.</summary>
    [ObjectInfo(typeof (ConfigDescriptions), Description = "UsersCacheConfigDescription", Title = "UsersCacheConfigTitle")]
    [ConfigurationProperty("usersCache")]
    public virtual CacheProfileElement UsersCache
    {
      get => (CacheProfileElement) this["usersCache"];
      set => this["usersCache"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the configuration for the permission filter cache.
    /// </summary>
    [ObjectInfo(typeof (ConfigDescriptions), Description = "PermissionFilterConfigurationsDescription", Title = "PermissionFilterConfigurationsTitle")]
    [ConfigurationProperty("permissionFilterCache")]
    public virtual PermissionsFilterCacheElement PermissionFilterCache
    {
      get => (PermissionsFilterCacheElement) this["permissionFilterCache"];
      set => this["permissionFilterCache"] = (object) value;
    }

    /// <summary>
    /// This setting disables the limitation that a given user should access the system from a single http client.
    /// If the client is a browser this will prevent the self logout (you are logged in from another machine) dialog from appearing
    /// when making authenticated requests from multiple HTTP clients. If the authenticated request is made from a service client
    /// the server will not respond with 403 "you are logged in from another machine".
    /// </summary>
    [ConfigurationProperty("disableActiveUserLoginsLimitation", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DisableActiveUserLoginsLimitationDescription", Title = "DisableActiveUserLoginsLimitationTitle")]
    public virtual bool DisableActiveUserLoginsLimitation
    {
      get => (bool) this["disableActiveUserLoginsLimitation"];
      set => this["disableActiveUserLoginsLimitation"] = (object) value;
    }

    /// <summary>
    /// If this is set to <c>true</c> every login request will automatically logout the user from other HTTP clients.
    /// If the client is a browser this will prevent the self logout (you are logged in from another machine) dialog from appearing
    /// after logging in a user. If the login request is made from a service client the server will not respond with 403
    /// "you are logged in from another machine".
    /// </summary>
    [ConfigurationProperty("logOutUsersFromDifferentClientsOnLogin", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "LogOutUsersFromDifferentClientsOnLoginDescription", Title = "LogOutUsersFromDifferentClientsOnLoginTitle")]
    public virtual bool LogOutUsersFromDifferentClientsOnLogin
    {
      get => (bool) this["logOutUsersFromDifferentClientsOnLogin"];
      set => this["logOutUsersFromDifferentClientsOnLogin"] = (object) value;
    }

    /// <summary>
    /// Disables the validation of http headers preventing performing requests from non trusted domains. This is a CSRF protection.
    /// </summary>
    [ConfigurationProperty("disableRequestSourceValidation")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DisableRequestSourceValidationDescription", Title = "DisableRequestSourceValidationTitle")]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public virtual bool DisableRequestSourceValidation
    {
      get => (bool) this["disableRequestSourceValidation"];
      set => this["disableRequestSourceValidation"] = (object) value;
    }

    /// <summary>
    /// Comma separated list of white-listed domains that are allowed to login to the site.
    /// </summary>
    [ConfigurationProperty("trustedLoginDomains")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "TrustedLoginDomainsDescription", Title = "TrustedLoginDomainsTitle")]
    public virtual string TrustedLoginDomains
    {
      get => (string) this["trustedLoginDomains"];
      set => this["trustedLoginDomains"] = (object) value;
    }

    /// <summary>
    /// Server session lifetime in minutes (valid when authentication mode is set to claims).
    /// </summary>
    [ConfigurationProperty("serverSessionTokenLifetime", DefaultValue = 1440)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ServerSessionTokenLifetimeDescription", Title = "ServerSessionTokenLifetimeTitle")]
    public virtual int ServerSessionTokenLifetime
    {
      get => (int) this["serverSessionTokenLifetime"];
      set => this["serverSessionTokenLifetime"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to redirect not authenticated requests to the frontend login page instead of Sitefinity backend page.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if redirect to frotend login page; otherwise, <c>false</c>.
    /// </value>
    [ConfigurationProperty("authenticateOnFrontendLoginPage", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "AuthenticateOnFrontendLoginPageDescription", Title = "AuthenticateOnFrontendLoginPageTitle")]
    [Browsable(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public virtual bool AuthenticateOnFrontendLoginPage
    {
      get => (bool) this["authenticateOnFrontendLoginPage"];
      set => this["authenticateOnFrontendLoginPage"] = (object) value;
    }

    /// <summary>Disables the HTML sanitization of the HTML fields.</summary>
    [ConfigurationProperty("disableHtmlSanitization", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DisableHtmlSanitizationDescription", Title = "DisableHtmlSanitizationTitle")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public virtual bool DisableHtmlSanitization
    {
      get => (bool) this["disableHtmlSanitization"];
      set => this["disableHtmlSanitization"] = (object) value;
    }

    /// <summary>Sets ReturnUrl parameter name for authentication.</summary>
    [ConfigurationProperty("authenticationReturnUrl", DefaultValue = null)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public virtual string AuthenticationReturnUrl
    {
      get => (string) this["authenticationReturnUrl"];
      set => this["authenticationReturnUrl"] = (object) value;
    }

    /// <summary>Gets users per site settings</summary>
    [ConfigurationProperty("usersPerSite")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "UsersPerSiteSettingsDescription", Title = "UsersPerSiteSettingsTitle")]
    public virtual UsersPerSiteSettings UsersPerSiteSettings => (UsersPerSiteSettings) this["usersPerSite"];

    internal bool RequireSaving { get; private set; }

    /// <summary>
    /// Called after the properties of this instance have been initialized.
    /// </summary>
    protected override void OnPropertiesInitialized()
    {
      this.LoadDefaults();
      this.LdapConnections.AddDefaultConnection();
      this.LdapConnections.LdapMapping.AddDefaultMappings();
      this.getDefaultValueHandler = new GetDefaultValue(this.GetDefaultValueHandler);
      this.LoadEnvironmentVariables();
      this.LoadDefaultSiteAdminPermissions();
    }

    private object GetDefaultValueHandler(string propertyName) => propertyName == "userIsOnlineTimeWindow" && Bootstrapper.IsReady ? (object) new TimeSpan(0, this.GetUserIsOnlineTimeWindow(), 0) : (object) null;

    public override void Upgrade(Version oldVersion, Version newVersion)
    {
      base.Upgrade(oldVersion, newVersion);
      DataProviderSettings providerSettings;
      if (oldVersion.Build < 1600 && this.SecurityProviders.TryGetValue("XmlDataProvider", out providerSettings) && providerSettings.ProviderType == (Type) null)
        this.SecurityProviders.Remove(providerSettings);
      if (oldVersion.Build < 2260)
      {
        this.AuthenticationMode = AuthenticationMode.Forms;
        this.FilterQueriesByViewPermissions = true;
      }
      if (oldVersion < SitefinityVersion.Sitefinity8_1 && this.Permissions.ContainsKey("Comments"))
        this.Permissions.Remove("Comments");
      if (!(oldVersion < SitefinityVersion.Sitefinity10_2) || this.DisableHtmlSanitization)
        return;
      this.DisableHtmlSanitization = true;
    }

    private void LoadEnvironmentVariables()
    {
      string setting = EnvironmentVariables.Current.GetSetting("DefaultRealmKey");
      if (setting == null)
        return;
      SecurityTokenKeyElement element;
      if (!this.SecurityTokenIssuers.TryGetValue("http://localhost", out element))
      {
        element = new SecurityTokenKeyElement((ConfigElement) this.SecurityTokenIssuers)
        {
          Realm = "http://localhost",
          Encoding = BinaryEncoding.Hexadecimal,
          MembershipProvider = this.DefaultBackendMembershipProvider
        };
        this.SecurityTokenIssuers.Add(element);
      }
      element.Key = setting;
      if (!this.RelyingParties.TryGetValue("http://localhost", out element))
      {
        element = new SecurityTokenKeyElement((ConfigElement) this.RelyingParties)
        {
          Realm = "http://localhost",
          Encoding = BinaryEncoding.Hexadecimal
        };
        this.RelyingParties.Add(element);
      }
      element.Key = setting;
    }

    private void LoadDefaults()
    {
      string name = typeof (SecurityResources).Name;
      this.SecurityProviders.Add(new DataProviderSettings((ConfigElement) this.SecurityProviders)
      {
        Name = "OpenAccessDataProvider",
        Description = "A data provider that stores security information using OpenAccess.",
        ProviderType = typeof (OpenAccessSecurityProvider)
      });
      this.RoleProviders.Add(new DataProviderSettings((ConfigElement) this.RoleProviders)
      {
        Name = this.DefaultBackendRoleProvider,
        Description = this.DefaultBackendRoleProvider + "Description",
        ResourceClassId = this.ResourceClassId,
        ProviderType = typeof (OpenAccessRoleProvider),
        Parameters = new NameValueCollection()
        {
          {
            "applicationName",
            "Backend/"
          }
        }
      });
      this.RoleProviders.Add(new DataProviderSettings((ConfigElement) this.RoleProviders)
      {
        Name = "AppRoles",
        Description = "AppRolesDescription",
        ResourceClassId = this.ResourceClassId,
        ProviderType = typeof (OpenAccessRoleProvider),
        Parameters = new NameValueCollection()
        {
          {
            "applicationName",
            "App/"
          }
        }
      });
      this.RoleProviders.Add(new DataProviderSettings((ConfigElement) this.RoleProviders)
      {
        Name = "LdapRoles",
        Description = "AppRolesDescription",
        Enabled = false,
        ProviderType = typeof (LdapRoleProvider),
        Parameters = new NameValueCollection()
        {
          {
            "applicationName",
            "LdapBackend/"
          }
        }
      });
      this.MembershipProviders.Add(new DataProviderSettings((ConfigElement) this.MembershipProviders)
      {
        Name = this.DefaultBackendMembershipProvider,
        Description = this.DefaultBackendMembershipProvider + "Description",
        ResourceClassId = this.ResourceClassId,
        ProviderType = typeof (OpenAccessMembershipProvider),
        Parameters = new NameValueCollection()
        {
          {
            "applicationName",
            "Backend/"
          },
          {
            "maxInvalidPasswordAttempts",
            "5"
          },
          {
            "minRequiredNonalphanumericCharacters",
            "0"
          },
          {
            "minRequiredPasswordLength",
            "7"
          },
          {
            "newPasswordLength",
            "8"
          },
          {
            "passwordAttemptWindow",
            "10"
          },
          {
            "passwordFormat",
            MembershipPasswordFormat.Hashed.ToString()
          },
          {
            "passwordStrengthRegularExpression",
            string.Empty
          },
          {
            "requiresQuestionAndAnswer",
            "false"
          },
          {
            "enablePasswordRetrieval",
            "false"
          },
          {
            "enablePasswordReset",
            "false"
          },
          {
            "recoveryMailAddress",
            string.Empty
          },
          {
            "recoveryMailBody",
            "Your password has been successfully changed.<br /><br />User Name: <%\\s*UserName\\s*%><br />Password: <%\\s*Password\\s*%>"
          },
          {
            "recoveryMailSubject",
            "Password recovery"
          }
        }
      });
      this.MembershipProviders.Add(new DataProviderSettings((ConfigElement) this.MembershipProviders)
      {
        Name = "LdapUsers",
        Description = "Special provider for LDAP users.",
        ProviderType = typeof (LdapMembershipProvider),
        Enabled = false,
        Parameters = new NameValueCollection()
        {
          {
            "applicationName",
            "LdapBackend/"
          },
          {
            "connection",
            string.Empty
          }
        }
      });
      ConfigElementDictionary<string, Permission> permissions = this.Permissions;
      Permission element1 = new Permission((ConfigElement) permissions)
      {
        Name = "General",
        Title = "GeneralPermissions",
        Description = "GeneralPermissionsDescription",
        ResourceClassId = name
      };
      permissions.Add(element1);
      ConfigElementDictionary<string, SecurityAction> actions1 = element1.Actions;
      actions1.Add(new SecurityAction((ConfigElement) actions1)
      {
        Name = "View",
        Type = SecurityActionTypes.View,
        Title = "View",
        Description = "ViewDescription",
        ResourceClassId = name
      });
      actions1.Add(new SecurityAction((ConfigElement) actions1)
      {
        Name = "Create",
        Type = SecurityActionTypes.Create,
        Title = "Create",
        Description = "CreateDescription",
        ResourceClassId = name
      });
      actions1.Add(new SecurityAction((ConfigElement) actions1)
      {
        Name = "Modify",
        Type = SecurityActionTypes.Modify,
        Title = "Modify",
        Description = "ModifyDescription",
        ResourceClassId = name
      });
      actions1.Add(new SecurityAction((ConfigElement) actions1)
      {
        Name = "Delete",
        Type = SecurityActionTypes.Delete,
        Title = "Delete",
        Description = "DeleteDescription",
        ResourceClassId = name
      });
      actions1.Add(new SecurityAction((ConfigElement) actions1)
      {
        Name = "ChangeOwner",
        Type = SecurityActionTypes.ChangeOwner,
        Title = "ChangeOwner",
        Description = "ChangeOwnerDescription",
        ResourceClassId = name
      });
      actions1.Add(new SecurityAction((ConfigElement) actions1)
      {
        Name = "ChangePermissions",
        Type = SecurityActionTypes.ChangePermissions,
        Title = "ChangePermissions",
        Description = "ChangePermissionsDescription",
        ResourceClassId = name
      });
      actions1.Add(new SecurityAction((ConfigElement) actions1)
      {
        Name = "Unlock",
        Type = SecurityActionTypes.Unlock,
        Title = "Unlock",
        Description = "UnlockDescription",
        ResourceClassId = name
      });
      Permission element2 = new Permission((ConfigElement) permissions)
      {
        Name = "Pages",
        Title = "PagePermissions",
        Description = "PagePermissionsDescription",
        ResourceClassId = name
      };
      permissions.Add(element2);
      ConfigElementDictionary<string, SecurityAction> actions2 = element2.Actions;
      actions2.Add(new SecurityAction((ConfigElement) actions2)
      {
        Name = "View",
        Type = SecurityActionTypes.View,
        Title = "ViewPageActionName",
        Description = "ViewPageActionDescription",
        ResourceClassId = name
      });
      actions2.Add(new SecurityAction((ConfigElement) actions2)
      {
        Name = "CreateChildControls",
        Title = "CreateChildControlsActionName",
        Description = "CreateChildControlsActionDescription",
        ResourceClassId = name
      });
      actions2.Add(new SecurityAction((ConfigElement) actions2)
      {
        Name = "EditContent",
        Title = "EditContentActionName",
        Description = "EditContentActionDescription",
        ResourceClassId = name
      });
      actions2.Add(new SecurityAction((ConfigElement) actions2)
      {
        Name = "Create",
        Type = SecurityActionTypes.Create,
        Title = "CreatePageActionName",
        Description = "CreatePageActionDescription",
        ResourceClassId = name
      });
      actions2.Add(new SecurityAction((ConfigElement) actions2)
      {
        Name = "Modify",
        Type = SecurityActionTypes.Modify,
        Title = "ModifyPageActionName",
        Description = "ModifyPageActionDescription",
        ResourceClassId = name
      });
      actions2.Add(new SecurityAction((ConfigElement) actions2)
      {
        Name = "Delete",
        Type = SecurityActionTypes.Delete,
        Title = "DeletePageActionName",
        Description = "DeletePageActionDescription",
        ResourceClassId = name
      });
      actions2.Add(new SecurityAction((ConfigElement) actions2)
      {
        Name = "ChangeOwner",
        Type = SecurityActionTypes.ChangeOwner,
        Title = "ChangePageOwnerActionName",
        Description = "ChangePageOwnerActionDescription",
        ResourceClassId = name
      });
      actions2.Add(new SecurityAction((ConfigElement) actions2)
      {
        Name = "ChangePermissions",
        Type = SecurityActionTypes.ChangePermissions,
        Title = "ChangePagePermissionsActionName",
        Description = "ChangePagePermissionsActionDescription",
        ResourceClassId = name
      });
      actions2.Add(new SecurityAction((ConfigElement) actions2)
      {
        Name = "Unlock",
        Type = SecurityActionTypes.Unlock,
        Title = "UnlockPageActionName",
        Description = "UnlockPagesActionDescription",
        ResourceClassId = name
      });
      Permission element3 = new Permission((ConfigElement) permissions)
      {
        Name = "Image",
        Title = "ImagePermissions",
        Description = "ImagePermissionsDescription",
        ResourceClassId = name
      };
      permissions.Add(element3);
      ConfigElementDictionary<string, SecurityAction> actions3 = element3.Actions;
      actions3.Add(new SecurityAction((ConfigElement) actions3)
      {
        Name = "ViewImage",
        Type = SecurityActionTypes.View,
        Title = "ViewImage",
        Description = "ViewImageDescription",
        ResourceClassId = name
      });
      actions3.Add(new SecurityAction((ConfigElement) actions3)
      {
        Name = "ManageImage",
        Type = SecurityActionTypes.Manage,
        Title = "ManageImage",
        Description = "ManageImageDescription",
        ResourceClassId = name
      });
      actions3.Add(new SecurityAction((ConfigElement) actions3)
      {
        Name = "ChangeImageOwner",
        Type = SecurityActionTypes.ChangeOwner,
        Title = "ChangeImageOwner",
        Description = "ChangeImageOwnerDescription",
        ResourceClassId = name
      });
      actions3.Add(new SecurityAction((ConfigElement) actions3)
      {
        Name = "ChangeImagePermissions",
        Type = SecurityActionTypes.ChangePermissions,
        Title = "ChangeImagePermissions",
        Description = "ChangeImagePermissionsDescription",
        ResourceClassId = name
      });
      actions3.Add(new SecurityAction((ConfigElement) actions3)
      {
        Name = "UnlockImage",
        Type = SecurityActionTypes.Unlock,
        Title = "UnlockImage",
        Description = "UnlockImageDescription",
        ResourceClassId = name
      });
      Permission element4 = new Permission((ConfigElement) permissions)
      {
        Name = "Album",
        Title = "AlbumPermissions",
        Description = "AlbumPermissionsDescription",
        ResourceClassId = name
      };
      permissions.Add(element4);
      ConfigElementDictionary<string, SecurityAction> actions4 = element4.Actions;
      actions4.Add(new SecurityAction((ConfigElement) actions4)
      {
        Name = "ViewAlbum",
        Type = SecurityActionTypes.View,
        Title = "ViewAlbum",
        Description = "ViewAlbumDescription",
        ResourceClassId = name
      });
      actions4.Add(new SecurityAction((ConfigElement) actions4)
      {
        Name = "CreateAlbum",
        Type = SecurityActionTypes.Create,
        Title = "CreateAlbum",
        Description = "CreateAlbumDescription",
        ResourceClassId = name
      });
      actions4.Add(new SecurityAction((ConfigElement) actions4)
      {
        Name = "DeleteAlbum",
        Type = SecurityActionTypes.Delete,
        Title = "DeleteAlbum",
        Description = "DeleteAlbumDescription",
        ResourceClassId = name
      });
      actions4.Add(new SecurityAction((ConfigElement) actions4)
      {
        Name = "ChangeAlbumOwner",
        Type = SecurityActionTypes.ChangeOwner,
        Title = "ChangeAlbumOwner",
        Description = "ChangeAlbumOwnerDescription",
        ResourceClassId = name
      });
      actions4.Add(new SecurityAction((ConfigElement) actions4)
      {
        Name = "ChangeAlbumPermissions",
        Type = SecurityActionTypes.ChangePermissions,
        Title = "ChangeAlbumPermissions",
        Description = "ChangeAlbumPermissionsDescription",
        ResourceClassId = name
      });
      Permission element5 = new Permission((ConfigElement) permissions)
      {
        Name = "Document",
        Title = "DocumentPermissions",
        Description = "DocumentPermissionsDescription",
        ResourceClassId = name
      };
      permissions.Add(element5);
      ConfigElementDictionary<string, SecurityAction> actions5 = element5.Actions;
      actions5.Add(new SecurityAction((ConfigElement) actions5)
      {
        Name = "ViewDocument",
        Type = SecurityActionTypes.View,
        Title = "ViewDocument",
        Description = "ViewDocumentDescription",
        ResourceClassId = name
      });
      actions5.Add(new SecurityAction((ConfigElement) actions5)
      {
        Name = "ManageDocument",
        Type = SecurityActionTypes.Manage,
        Title = "ManageDocument",
        Description = "ManageDocumentDescription",
        ResourceClassId = name
      });
      actions5.Add(new SecurityAction((ConfigElement) actions5)
      {
        Name = "ChangeDocumentOwner",
        Type = SecurityActionTypes.ChangeOwner,
        Title = "ChangeDocumentOwner",
        Description = "ChangeDocumentOwnerDescription",
        ResourceClassId = name
      });
      actions5.Add(new SecurityAction((ConfigElement) actions5)
      {
        Name = "ChangeDocumentPermissions",
        Type = SecurityActionTypes.ChangePermissions,
        Title = "ChangeDocumentPermissions",
        Description = "ChangeDocumentPermissionsDescription",
        ResourceClassId = name
      });
      actions5.Add(new SecurityAction((ConfigElement) actions5)
      {
        Name = "UnlockDocument",
        Type = SecurityActionTypes.Unlock,
        Title = "UnlockDocument",
        Description = "UnlockDocumentDescription",
        ResourceClassId = name
      });
      Permission element6 = new Permission((ConfigElement) permissions)
      {
        Name = "DocumentLibrary",
        Title = "DocumentLibraryPermissions",
        Description = "DocumentLibraryPermissionsDescription",
        ResourceClassId = name
      };
      permissions.Add(element6);
      ConfigElementDictionary<string, SecurityAction> actions6 = element6.Actions;
      actions6.Add(new SecurityAction((ConfigElement) actions6)
      {
        Name = "ViewDocumentLibrary",
        Type = SecurityActionTypes.View,
        Title = "ViewDocumentLibrary",
        Description = "ViewDocumentLibraryDescription",
        ResourceClassId = name
      });
      actions6.Add(new SecurityAction((ConfigElement) actions6)
      {
        Name = "CreateDocumentLibrary",
        Type = SecurityActionTypes.Create,
        Title = "CreateDocumentLibrary",
        Description = "CreateDocumentLibraryDescription",
        ResourceClassId = name
      });
      actions6.Add(new SecurityAction((ConfigElement) actions6)
      {
        Name = "DeleteDocumentLibrary",
        Type = SecurityActionTypes.Delete,
        Title = "DeleteDocumentLibrary",
        Description = "DeleteDocumentLibraryDescription",
        ResourceClassId = name
      });
      actions6.Add(new SecurityAction((ConfigElement) actions6)
      {
        Name = "ChangeDocumentLibraryOwner",
        Type = SecurityActionTypes.ChangeOwner,
        Title = "ChangeDocumentLibraryOwner",
        Description = "ChangeDocumentLibraryOwnerDescription",
        ResourceClassId = name
      });
      actions6.Add(new SecurityAction((ConfigElement) actions6)
      {
        Name = "ChangeDocumentLibraryPermissions",
        Type = SecurityActionTypes.ChangePermissions,
        Title = "ChangeDocumentLibraryPermissions",
        Description = "ChangeDocumentLibraryPermissionsDescription",
        ResourceClassId = name
      });
      Permission element7 = new Permission((ConfigElement) permissions)
      {
        Name = "Video",
        Title = "VideoPermissions",
        Description = "VideoPermissionsDescription",
        ResourceClassId = name
      };
      permissions.Add(element7);
      ConfigElementDictionary<string, SecurityAction> actions7 = element7.Actions;
      actions7.Add(new SecurityAction((ConfigElement) actions7)
      {
        Name = "ViewVideo",
        Type = SecurityActionTypes.View,
        Title = "ViewVideo",
        Description = "ViewVideoDescription",
        ResourceClassId = name
      });
      actions7.Add(new SecurityAction((ConfigElement) actions7)
      {
        Name = "ManageVideo",
        Type = SecurityActionTypes.Manage,
        Title = "ManageVideo",
        Description = "ManageVideoDescription",
        ResourceClassId = name
      });
      actions7.Add(new SecurityAction((ConfigElement) actions7)
      {
        Name = "ChangeVideoOwner",
        Type = SecurityActionTypes.ChangeOwner,
        Title = "ChangeVideoOwner",
        Description = "ChangeVideoOwnerDescription",
        ResourceClassId = name
      });
      actions7.Add(new SecurityAction((ConfigElement) actions7)
      {
        Name = "ChangeVideoPermissions",
        Type = SecurityActionTypes.ChangePermissions,
        Title = "ChangeVideoPermissions",
        Description = "ChangeVideoPermissionsDescription",
        ResourceClassId = name
      });
      actions7.Add(new SecurityAction((ConfigElement) actions7)
      {
        Name = "UnlockVideo",
        Type = SecurityActionTypes.Unlock,
        Title = "UnlockVideo",
        Description = "UnlockVideoDescription",
        ResourceClassId = name
      });
      Permission element8 = new Permission((ConfigElement) permissions)
      {
        Name = "VideoLibrary",
        Title = "VideoLibraryPermissions",
        Description = "VideoLibraryPermissionsDescription",
        ResourceClassId = name
      };
      permissions.Add(element8);
      ConfigElementDictionary<string, SecurityAction> actions8 = element8.Actions;
      actions8.Add(new SecurityAction((ConfigElement) actions8)
      {
        Name = "ViewVideoLibrary",
        Type = SecurityActionTypes.View,
        Title = "ViewVideoLibrary",
        Description = "ViewVideoLibraryDescription",
        ResourceClassId = name
      });
      actions8.Add(new SecurityAction((ConfigElement) actions8)
      {
        Name = "CreateVideoLibrary",
        Type = SecurityActionTypes.Create,
        Title = "CreateVideoLibrary",
        Description = "CreateVideoLibraryDescription",
        ResourceClassId = name
      });
      actions8.Add(new SecurityAction((ConfigElement) actions8)
      {
        Name = "DeleteVideoLibrary",
        Type = SecurityActionTypes.Delete,
        Title = "DeleteVideoLibrary",
        Description = "DeleteVideoLibraryDescription",
        ResourceClassId = name
      });
      actions8.Add(new SecurityAction((ConfigElement) actions8)
      {
        Name = "ChangeVideoLibraryOwner",
        Type = SecurityActionTypes.ChangeOwner,
        Title = "ChangeVideoLibraryOwner",
        Description = "ChangeVideoLibraryOwnerDescription",
        ResourceClassId = name
      });
      actions8.Add(new SecurityAction((ConfigElement) actions8)
      {
        Name = "ChangeVideoLibraryPermissions",
        Type = SecurityActionTypes.ChangePermissions,
        Title = "ChangeVideoLibraryPermissions",
        Description = "ChangeVideoLibraryPermissionsDescription",
        ResourceClassId = name
      });
      Permission element9 = new Permission((ConfigElement) permissions)
      {
        Name = "Controls",
        Title = "ControlsPermissions",
        Description = "ControlsPermissionsDescription",
        ResourceClassId = name
      };
      permissions.Add(element9);
      ConfigElementDictionary<string, SecurityAction> actions9 = element9.Actions;
      actions9.Add(new SecurityAction((ConfigElement) actions9)
      {
        Name = "ViewControl",
        Type = SecurityActionTypes.View,
        Title = "ViewControlActionName",
        Description = "ViewControlActionDescription",
        ResourceClassId = name
      });
      actions9.Add(new SecurityAction((ConfigElement) actions9)
      {
        Name = "MoveControl",
        Title = "MoveControlActionName",
        Description = "MoveControlActionDescription",
        ResourceClassId = name
      });
      actions9.Add(new SecurityAction((ConfigElement) actions9)
      {
        Name = "EditControlProperties",
        Type = SecurityActionTypes.Modify,
        Title = "EditControlPropertiesActionName",
        Description = "EditControlPropertiesActionDescription",
        ResourceClassId = name
      });
      actions9.Add(new SecurityAction((ConfigElement) actions9)
      {
        Name = "DeleteControl",
        Type = SecurityActionTypes.Delete,
        Title = "DeleteControlActionName",
        Description = "DeleteControlActionDescription",
        ResourceClassId = name
      });
      actions9.Add(new SecurityAction((ConfigElement) actions9)
      {
        Name = "ChangeControlOwner",
        Type = SecurityActionTypes.ChangeOwner,
        Title = "ChangeControlOwnerActionName",
        Description = "ChangeControlOwnerActionDescription",
        ResourceClassId = name
      });
      actions9.Add(new SecurityAction((ConfigElement) actions9)
      {
        Name = "ChangeControlPermissions",
        Type = SecurityActionTypes.ChangePermissions,
        Title = "ChangeControlPermissionsActionName",
        Description = "ChangeControlPermissionsActionDescription",
        ResourceClassId = name
      });
      Permission element10 = new Permission((ConfigElement) permissions)
      {
        Name = "LayoutElement",
        Title = "LayoutElementPermissionSetName",
        Description = "LayoutElementPermissionSetDescription",
        ResourceClassId = name
      };
      permissions.Add(element10);
      ConfigElementDictionary<string, SecurityAction> actions10 = element10.Actions;
      actions10.Add(new SecurityAction((ConfigElement) actions10)
      {
        Name = "ViewLayout",
        Type = SecurityActionTypes.View,
        Title = "ViewLayoutActionName",
        Description = "ViewLayoutActionDescription",
        ResourceClassId = name
      });
      actions10.Add(new SecurityAction((ConfigElement) actions10)
      {
        Name = "MoveLayout",
        Title = "MoveLayoutActionName",
        Description = "MoveLayoutActionDescription",
        ResourceClassId = name
      });
      actions10.Add(new SecurityAction((ConfigElement) actions10)
      {
        Name = "EditLayoutProperties",
        Type = SecurityActionTypes.Modify,
        Title = "EditLayoutPropertiesActionName",
        Description = "EditControlPropertiesActionDescription",
        ResourceClassId = name
      });
      actions10.Add(new SecurityAction((ConfigElement) actions10)
      {
        Name = "DeleteLayout",
        Type = SecurityActionTypes.Delete,
        Title = "DeleteLayoutActionName",
        Description = "DeleteLayoutActionDescription",
        ResourceClassId = name
      });
      actions10.Add(new SecurityAction((ConfigElement) actions10)
      {
        Name = "ChangeLayoutOwner",
        Type = SecurityActionTypes.ChangeOwner,
        Title = "ChangeLayoutOwnerActionName",
        Description = "ChangeLayoutOwnerActionDescription",
        ResourceClassId = name
      });
      actions10.Add(new SecurityAction((ConfigElement) actions10)
      {
        Name = "ChangeLayoutPermissions",
        Type = SecurityActionTypes.ChangePermissions,
        Title = "ChangeLayoutPermissionsActionName",
        Description = "ChangeLayoutPermissionsActionDescription",
        ResourceClassId = name
      });
      actions10.Add(new SecurityAction((ConfigElement) actions10)
      {
        Name = "DropOnLayout",
        Title = "DropOnLayoutActionName",
        Description = "DropOnLayoutActionDescription",
        ResourceClassId = name
      });
      Permission element11 = new Permission((ConfigElement) permissions)
      {
        Name = "PageTemplates",
        Title = "PageTemplatesPermissions",
        Description = "PageTemplatesPermissionsDescription",
        ResourceClassId = name
      };
      permissions.Add(element11);
      ConfigElementDictionary<string, SecurityAction> actions11 = element11.Actions;
      actions11.Add(new SecurityAction((ConfigElement) actions11)
      {
        Name = "View",
        Type = SecurityActionTypes.View,
        Title = "View",
        Description = "ViewDescription",
        ResourceClassId = name
      });
      actions11.Add(new SecurityAction((ConfigElement) actions11)
      {
        Name = "Create",
        Type = SecurityActionTypes.Create,
        Title = "Create",
        Description = "CreateDescription",
        ResourceClassId = name
      });
      actions11.Add(new SecurityAction((ConfigElement) actions11)
      {
        Name = "Modify",
        Type = SecurityActionTypes.Modify,
        Title = "Modify",
        Description = "ModifyDescription",
        ResourceClassId = name
      });
      actions11.Add(new SecurityAction((ConfigElement) actions11)
      {
        Name = "Delete",
        Type = SecurityActionTypes.Delete,
        Title = "Delete",
        Description = "DeleteDescription",
        ResourceClassId = name
      });
      actions11.Add(new SecurityAction((ConfigElement) actions11)
      {
        Name = "ChangeOwner",
        Type = SecurityActionTypes.ChangeOwner,
        Title = "ChangeOwner",
        Description = "ChangeOwnerDescription",
        ResourceClassId = name
      });
      actions11.Add(new SecurityAction((ConfigElement) actions11)
      {
        Name = "ChangePermissions",
        Type = SecurityActionTypes.ChangePermissions,
        Title = "ChangePermissions",
        Description = "ChangePermissionsDescription",
        ResourceClassId = name
      });
      actions11.Add(new SecurityAction((ConfigElement) actions11)
      {
        Name = "Unlock",
        Type = SecurityActionTypes.Unlock,
        Title = "Unlock",
        Description = "UnlockDescription",
        ResourceClassId = name
      });
      Permission element12 = new Permission((ConfigElement) permissions)
      {
        Name = "Backend",
        Title = "BackendPermissions",
        Description = "BackendPermissionsDescription",
        ResourceClassId = name
      };
      permissions.Add(element12);
      ConfigElementDictionary<string, SecurityAction> actions12 = element12.Actions;
      actions12.Add(new SecurityAction((ConfigElement) actions12)
      {
        Name = "ManageUsers",
        Title = "ManageUsers",
        Description = "ManageUsersDescription",
        ResourceClassId = name
      });
      actions12.Add(new SecurityAction((ConfigElement) actions12)
      {
        Name = "ManageRoles",
        Title = "ManageRoles",
        Description = "ManageRolesDescription",
        ResourceClassId = name
      });
      actions12.Add(new SecurityAction((ConfigElement) actions12)
      {
        Name = "ViewPermissions",
        Title = "ViewPermissions",
        Description = "ViewPermissionsDescription",
        ResourceClassId = name
      });
      actions12.Add(new SecurityAction((ConfigElement) actions12)
      {
        Name = "ChangePermissions",
        Type = SecurityActionTypes.ChangePermissions,
        Title = "ChangePermissions",
        Description = "ChangePermissionsDescription",
        ResourceClassId = name
      });
      actions12.Add(new SecurityAction((ConfigElement) actions12)
      {
        Name = "ViewConfigurations",
        Title = "ViewConfigurations",
        Description = "ViewConfigurationsDescription",
        ResourceClassId = name
      });
      actions12.Add(new SecurityAction((ConfigElement) actions12)
      {
        Name = "ChangeConfigurations",
        Title = "ChangeConfigurations",
        Description = "ChangeConfigurationsDescription",
        ResourceClassId = name
      });
      actions12.Add(new SecurityAction((ConfigElement) actions12)
      {
        Name = "ManageLabels",
        Title = "ManageLabels",
        Description = "ManageLabelsDescription",
        ResourceClassId = name
      });
      actions12.Add(new SecurityAction((ConfigElement) actions12)
      {
        Name = "ManageFiles",
        Title = "ManageFiles",
        Description = "ManageFilesDescription",
        ResourceClassId = name
      });
      actions12.Add(new SecurityAction((ConfigElement) actions12)
      {
        Name = "ManageLicenses",
        Title = "ManageLicenses",
        Description = "ManageLicensesDescription",
        ResourceClassId = name
      });
      actions12.Add(new SecurityAction((ConfigElement) actions12)
      {
        Name = "UseBrowseAndEdit",
        Title = "UseBrowseAndEdit",
        Description = "UseBrowseAndEditDescription",
        ResourceClassId = name
      });
      actions12.Add(new SecurityAction((ConfigElement) actions12)
      {
        Name = "ManageUserProfiles",
        Title = "ManageUserProfiles",
        Description = "ManageUserProfilesDescription",
        ResourceClassId = name
      });
      actions12.Add(new SecurityAction((ConfigElement) actions12)
      {
        Name = "ManageBackendPages",
        Title = "ManageBackendPages",
        Description = "ManageBackendPagesDescription",
        ResourceClassId = name
      });
      actions12.Add(new SecurityAction((ConfigElement) actions12)
      {
        Name = "ManagePublishingSystem",
        Title = "ManagePublishingSystem",
        Description = "ManagePublishingSystemDescription",
        ResourceClassId = name
      });
      actions12.Add(new SecurityAction((ConfigElement) actions12)
      {
        Name = "ManageSearchIndices",
        Title = "ManageSearchIndices",
        Description = "ManageSearchIndicesDescription",
        ResourceClassId = name
      });
      actions12.Add(new SecurityAction((ConfigElement) actions12)
      {
        Name = "ManageWidgets",
        Title = "ManageWidgets",
        Description = "ManageWidgetsDescription",
        ResourceClassId = name
      });
      ModuleSecurityAction element13 = new ModuleSecurityAction((ConfigElement) actions12);
      element13.Name = "ManageNewsletters";
      element13.Title = "ManageNewsletters";
      element13.Description = "ManageNewslettersDescription";
      element13.ResourceClassId = name;
      element13.ModuleName = "Newsletters";
      actions12.Add((SecurityAction) element13);
      ModuleSecurityAction element14 = new ModuleSecurityAction((ConfigElement) actions12);
      element14.Name = "ManageEcommerce";
      element14.Title = "ManageEcommerce";
      element14.Description = "ManageEcommerceDescription";
      element14.ResourceClassId = name;
      element14.ModuleName = "Ecommerce";
      actions12.Add((SecurityAction) element14);
      ModuleSecurityAction element15 = new ModuleSecurityAction((ConfigElement) actions12);
      element15.Name = "SiteSynchronization";
      element15.Title = "SiteSynchronization";
      element15.Description = "SiteSynchronizationDescription";
      element15.ResourceClassId = name;
      element15.ModuleName = "Synchronization";
      actions12.Add((SecurityAction) element15);
      actions12.Add(new SecurityAction((ConfigElement) actions12)
      {
        Name = "ManageUserFiles",
        Title = "ManageUserFiles",
        Description = "ManageUserFilesDescription",
        ResourceClassId = name
      });
      ModuleSecurityAction element16 = new ModuleSecurityAction((ConfigElement) actions12);
      element16.Name = "ManageMultisiteManagement";
      element16.Title = "ManageMultisiteManagement";
      element16.Description = "ManageMultisiteManagementDescription";
      element16.ResourceClassId = name;
      element16.ModuleName = "MultisiteInternal";
      actions12.Add((SecurityAction) element16);
      ModuleSecurityAction element17 = new ModuleSecurityAction((ConfigElement) actions12);
      element17.Name = "AccessForums";
      element17.Title = "AccessForums";
      element17.Description = "AccessForumsDescription";
      element17.ResourceClassId = "ForumsResources";
      element17.ModuleName = "Forums";
      actions12.Add((SecurityAction) element17);
      ModuleSecurityAction element18 = new ModuleSecurityAction((ConfigElement) actions12);
      element18.Name = "AccessResponsiveDesign";
      element18.Title = "AccessResponsiveDesign";
      element18.Description = "AccessResponsiveDesignDescription";
      element18.ResourceClassId = "ResponsiveDesignResources";
      element18.ModuleName = "ResponsiveDesign";
      actions12.Add((SecurityAction) element18);
      ModuleSecurityAction element19 = new ModuleSecurityAction((ConfigElement) actions12);
      element19.Name = "AccessPersonalization";
      element19.Title = "AccessPersonalizationTitle";
      element19.Description = "AccessPersonalizationDescription";
      element19.ResourceClassId = "PersonalizationResources";
      element19.ModuleName = "Personalization";
      actions12.Add((SecurityAction) element19);
      ModuleSecurityAction element20 = new ModuleSecurityAction((ConfigElement) actions12);
      element20.Name = "AccessSharepointConnector";
      element20.Title = "AccessSharepointConnectorTitle";
      element20.Description = "AccessSharepointConnectorDescription";
      element20.ResourceClassId = "ConnectorResources";
      element20.ModuleName = "SharepointConnector";
      actions12.Add((SecurityAction) element20);
      ModuleSecurityAction element21 = new ModuleSecurityAction((ConfigElement) actions12);
      element21.Name = "AccessSalesForceConnector";
      element21.Title = "AccessSalesForceConnectorTitle";
      element21.Description = "AccessSalesForceConnectorDescription";
      element21.ResourceClassId = "SalesForceConnectorResources";
      element21.ModuleName = "SalesForceConnector";
      actions12.Add((SecurityAction) element21);
      ModuleSecurityAction element22 = new ModuleSecurityAction((ConfigElement) actions12);
      element22.Name = "AccessEverliveConnector";
      element22.ModuleName = "EverliveConnector";
      actions12.Add((SecurityAction) element22);
      ModuleSecurityAction element23 = new ModuleSecurityAction((ConfigElement) actions12);
      element23.Name = "AccessMarketoConnector";
      element23.Title = "AccessMarketoConnectorTitle";
      element23.Description = "AccessMarketoConnectorDescription";
      element23.ResourceClassId = "MarketoConnectorResources";
      element23.ModuleName = "MarketoConnector";
      actions12.Add((SecurityAction) element23);
      ModuleSecurityAction element24 = new ModuleSecurityAction((ConfigElement) actions12);
      element24.Name = "AccessComments";
      element24.Title = "AccessCommentsTitle";
      element24.Description = "AccessCommentsDescription";
      element24.ResourceClassId = "CommentsResources";
      element24.ModuleName = "Comments";
      actions12.Add((SecurityAction) element24);
      ModuleSecurityAction element25 = new ModuleSecurityAction((ConfigElement) actions12);
      element25.Name = "AccessDataIntelligenceConnector";
      element25.Title = "AccessDataIntelligenceConnectorTitle";
      element25.Description = "AccessDataIntelligenceConnectorDescription";
      element25.ResourceClassId = name;
      element25.ModuleName = "DataIntelligenceConnector";
      actions12.Add((SecurityAction) element25);
      ModuleSecurityAction element26 = new ModuleSecurityAction((ConfigElement) actions12);
      element26.Name = "AccessTranslations";
      element26.Title = "AccessTranslationsTitle";
      element26.Description = "AccessTranslationsDescription";
      element26.ResourceClassId = name;
      element26.ModuleName = "Translations";
      actions12.Add((SecurityAction) element26);
      Permission element27 = new Permission((ConfigElement) permissions)
      {
        Name = "Taxonomies",
        Title = "TaxonomyPermissions",
        Description = "TaxonomyPermissionsDescription",
        ResourceClassId = name
      };
      permissions.Add(element27);
      ConfigElementDictionary<string, SecurityAction> actions13 = element27.Actions;
      actions13.Add(new SecurityAction((ConfigElement) actions13)
      {
        Name = "ViewTaxonomy",
        Type = SecurityActionTypes.View,
        Title = "ViewTaxonomyActionName",
        Description = "ViewTaxonomyActionDescription",
        ResourceClassId = name
      });
      actions13.Add(new SecurityAction((ConfigElement) actions13)
      {
        Name = "CreateTaxonomy",
        Type = SecurityActionTypes.Create,
        Title = "CreateTaxonomyActionName",
        Description = "CreateTaxonomyActionDescription",
        ResourceClassId = name
      });
      actions13.Add(new SecurityAction((ConfigElement) actions13)
      {
        Name = "ModifyTaxonomyAndSubTaxons",
        Type = SecurityActionTypes.Modify,
        Title = "ModifyTaxonomyActionName",
        Description = "ModifyTaxonomyActionDescription",
        ResourceClassId = name
      });
      actions13.Add(new SecurityAction((ConfigElement) actions13)
      {
        Name = "DeleteTaxonomy",
        Type = SecurityActionTypes.Delete,
        Title = "DeleteTaxonomyActionName",
        Description = "DeleteTaxonomyActionDescription",
        ResourceClassId = name
      });
      actions13.Add(new SecurityAction((ConfigElement) actions13)
      {
        Name = "ChangeTaxonomyOwner",
        Type = SecurityActionTypes.ChangeOwner,
        Title = "ChangeTaxonomyOwnerActionName",
        Description = "ChangeTaxonomyOwnerActionDescription",
        ResourceClassId = name
      });
      actions13.Add(new SecurityAction((ConfigElement) actions13)
      {
        Name = "ChangeTaxonomyPermissions",
        Type = SecurityActionTypes.ChangePermissions,
        Title = "ChangeTaxonomyPermissionsActionName",
        Description = "ChangeTaxonomyPermissionsActionDescription",
        ResourceClassId = name
      });
      Permission element28 = new Permission((ConfigElement) permissions)
      {
        Name = "Forms",
        Title = "FormsPermissions",
        Description = "FormsPermissionsDescription",
        ResourceClassId = name
      };
      permissions.Add(element28);
      ConfigElementDictionary<string, SecurityAction> actions14 = element28.Actions;
      actions14.Add(new SecurityAction((ConfigElement) actions14)
      {
        Name = "View",
        Type = SecurityActionTypes.View,
        Title = "ViewForm",
        Description = "ViewDescription",
        ResourceClassId = name
      });
      actions14.Add(new SecurityAction((ConfigElement) actions14)
      {
        Name = "Create",
        Type = SecurityActionTypes.Create,
        Title = "CreateForm",
        Description = "CreateDescription",
        ResourceClassId = name
      });
      actions14.Add(new SecurityAction((ConfigElement) actions14)
      {
        Name = "Modify",
        Type = SecurityActionTypes.Modify,
        Title = "ModifyForm",
        Description = "ModifyDescription",
        ResourceClassId = name
      });
      actions14.Add(new SecurityAction((ConfigElement) actions14)
      {
        Name = "Delete",
        Type = SecurityActionTypes.Delete,
        Title = "DeleteForm",
        Description = "DeleteDescription",
        ResourceClassId = name
      });
      actions14.Add(new SecurityAction((ConfigElement) actions14)
      {
        Name = "ChangeOwner",
        Type = SecurityActionTypes.ChangeOwner,
        Title = "ChangeFormOwner",
        Description = "ChangeOwnerDescription",
        ResourceClassId = name
      });
      actions14.Add(new SecurityAction((ConfigElement) actions14)
      {
        Name = "ChangePermissions",
        Type = SecurityActionTypes.ChangePermissions,
        Title = "ChangePermissions",
        Description = "ChangePermissionsDescription",
        ResourceClassId = name
      });
      actions14.Add(new SecurityAction((ConfigElement) actions14)
      {
        Name = "ViewResponses",
        Type = SecurityActionTypes.View,
        Title = "ViewResponses",
        Description = "ViewDescription",
        ResourceClassId = name
      });
      actions14.Add(new SecurityAction((ConfigElement) actions14)
      {
        Name = "ManageResponses",
        Type = SecurityActionTypes.Manage,
        Title = "ManageResponses",
        Description = "ManageResponsesDescription",
        ResourceClassId = name
      });
      actions14.Add(new SecurityAction((ConfigElement) actions14)
      {
        Name = "Unlock",
        Type = SecurityActionTypes.Unlock,
        Title = "UnlockForm",
        Description = "UnlockDescription",
        ResourceClassId = name
      });
      Permission element29 = new Permission((ConfigElement) permissions)
      {
        Name = "WorkflowDefinition",
        Title = "WorkflowDefinitionPermissions",
        Description = "WorkflowDefinitionPermissionsDescription",
        ResourceClassId = name
      };
      permissions.Add(element29);
      ConfigElementDictionary<string, SecurityAction> actions15 = element29.Actions;
      actions15.Add(new SecurityAction((ConfigElement) actions15)
      {
        Name = "View",
        Type = SecurityActionTypes.View,
        Title = "View",
        Description = "ViewDescription",
        ResourceClassId = name
      });
      actions15.Add(new SecurityAction((ConfigElement) actions15)
      {
        Name = "Create",
        Type = SecurityActionTypes.Create,
        Title = "Create",
        Description = "CreateDescription",
        ResourceClassId = name
      });
      actions15.Add(new SecurityAction((ConfigElement) actions15)
      {
        Name = "Modify",
        Type = SecurityActionTypes.Modify,
        Title = "Modify",
        Description = "ModifyDescription",
        ResourceClassId = name
      });
      actions15.Add(new SecurityAction((ConfigElement) actions15)
      {
        Name = "Delete",
        Type = SecurityActionTypes.Delete,
        Title = "Delete",
        Description = "DeleteDescription",
        ResourceClassId = name
      });
      actions15.Add(new SecurityAction((ConfigElement) actions15)
      {
        Name = "ChangeOwner",
        Type = SecurityActionTypes.ChangeOwner,
        Title = "ChangeOwner",
        Description = "ChangeOwnerDescription",
        ResourceClassId = name
      });
      actions15.Add(new SecurityAction((ConfigElement) actions15)
      {
        Name = "ChangePermissions",
        Type = SecurityActionTypes.ChangePermissions,
        Title = "ChangePermissions",
        Description = "ChangePermissionsDescription",
        ResourceClassId = name
      });
      Permission element30 = new Permission((ConfigElement) permissions)
      {
        Name = "Site",
        Title = "MultisiteManagementPermissions",
        Description = "MultisiteManagementPermissionsDescription",
        ResourceClassId = name
      };
      permissions.Add(element30);
      ConfigElementDictionary<string, SecurityAction> actions16 = element30.Actions;
      actions16.Add(new SecurityAction((ConfigElement) actions16, 2)
      {
        Name = "AccessSite",
        Type = SecurityActionTypes.View,
        Title = "AccessSite",
        Description = "AccessSiteDescription",
        ResourceClassId = name
      });
      actions16.Add(new SecurityAction((ConfigElement) actions16, 4)
      {
        Name = "CreateEditSite",
        Type = SecurityActionTypes.Create | SecurityActionTypes.Modify,
        Title = "CreateEditSite",
        Description = "CreateEditSiteDescription",
        ResourceClassId = name
      });
      actions16.Add(new SecurityAction((ConfigElement) actions16, 8)
      {
        Name = "DeleteSite",
        Type = SecurityActionTypes.Delete,
        Title = "DeleteSite",
        Description = "DeleteSiteDescription",
        ResourceClassId = name
      });
      actions16.Add(new SecurityAction((ConfigElement) actions16, 16)
      {
        Name = "ConfigureModules",
        Title = "ConfigureModules",
        Description = "ConfigureModulesDescription",
        ResourceClassId = name
      });
      actions16.Add(new SecurityAction((ConfigElement) actions16, 32)
      {
        Name = "StartStopSite",
        Title = "StartStopSite",
        Description = "StartStopSiteDescription",
        ResourceClassId = name
      });
      actions16.Add(new SecurityAction((ConfigElement) actions16, 64)
      {
        Name = "ChangeOwner",
        Type = SecurityActionTypes.ChangeOwner,
        Title = "ChangeOwner",
        Description = "ChangeOwnerDescription",
        ResourceClassId = name
      });
      actions16.Add(new SecurityAction((ConfigElement) actions16, 128)
      {
        Name = "ChangePermissions",
        Type = SecurityActionTypes.ChangePermissions,
        Title = "ChangeSitePermissions",
        Description = "ChangeSitePermissionsDescription",
        ResourceClassId = name
      });
      Permission element31 = new Permission((ConfigElement) permissions)
      {
        Name = "SitemapGeneration",
        Title = "SitemapGenerationPermissions",
        Description = "SitemapGenerationPermissionsDescription",
        ResourceClassId = name
      };
      permissions.Add(element31);
      ConfigElementDictionary<string, SecurityAction> actions17 = element31.Actions;
      actions17.Add(new SecurityAction((ConfigElement) actions17, 2)
      {
        Name = "ChangeBackendLinkPermissions",
        Type = SecurityActionTypes.ChangePermissions,
        Title = "ChangeBackendLinkPermissions",
        Description = "ChangeBackendLinkPermissionsDescription",
        ResourceClassId = name
      });
      actions17.Add(new SecurityAction((ConfigElement) actions17, 4)
      {
        Name = "ViewBackendLink",
        Title = "ViewBackendLinkTitle",
        Description = "ViewBackendLinkDescription",
        ResourceClassId = name
      });
      Permission element32 = new Permission((ConfigElement) permissions)
      {
        Name = "ImagesSitemapGeneration",
        Title = "SitemapGenerationPermissions",
        Description = "SitemapGenerationPermissionsDescription",
        ResourceClassId = name
      };
      permissions.Add(element32);
      ConfigElementDictionary<string, SecurityAction> actions18 = element32.Actions;
      actions18.Add(new SecurityAction((ConfigElement) actions18, 2)
      {
        Name = "ChangeBackendLinkPermissions",
        Type = SecurityActionTypes.ChangePermissions,
        Title = "ChangeBackendLinkPermissions",
        Description = "ChangeBackendLinkPermissionsDescription",
        ResourceClassId = name
      });
      actions18.Add(new SecurityAction((ConfigElement) actions18, 4)
      {
        Name = "ViewBackendLink",
        Title = "ViewBackendLinkTitle",
        Description = "ViewBackendLinkDescription",
        ResourceClassId = name
      });
      Permission element33 = new Permission((ConfigElement) permissions)
      {
        Name = "VideosSitemapGeneration",
        Title = "SitemapGenerationPermissions",
        Description = "SitemapGenerationPermissionsDescription",
        ResourceClassId = name
      };
      permissions.Add(element33);
      ConfigElementDictionary<string, SecurityAction> actions19 = element33.Actions;
      actions19.Add(new SecurityAction((ConfigElement) actions19, 2)
      {
        Name = "ChangeBackendLinkPermissions",
        Type = SecurityActionTypes.ChangePermissions,
        Title = "ChangeBackendLinkPermissions",
        Description = "ChangeBackendLinkPermissionsDescription",
        ResourceClassId = name
      });
      actions19.Add(new SecurityAction((ConfigElement) actions19, 4)
      {
        Name = "ViewBackendLink",
        Title = "ViewBackendLinkTitle",
        Description = "ViewBackendLinkDescription",
        ResourceClassId = name
      });
      Permission element34 = new Permission((ConfigElement) permissions)
      {
        Name = "DocumentsSitemapGeneration",
        Title = "SitemapGenerationPermissions",
        Description = "SitemapGenerationPermissionsDescription",
        ResourceClassId = name
      };
      permissions.Add(element34);
      ConfigElementDictionary<string, SecurityAction> actions20 = element34.Actions;
      actions20.Add(new SecurityAction((ConfigElement) actions20, 2)
      {
        Name = "ChangeBackendLinkPermissions",
        Type = SecurityActionTypes.ChangePermissions,
        Title = "ChangeBackendLinkPermissions",
        Description = "ChangeBackendLinkPermissionsDescription",
        ResourceClassId = name
      });
      actions20.Add(new SecurityAction((ConfigElement) actions20, 4)
      {
        Name = "ViewBackendLink",
        Title = "ViewBackendLinkTitle",
        Description = "ViewBackendLinkDescription",
        ResourceClassId = name
      });
      CustomPermissionsDisplaySettingsConfig element35 = new CustomPermissionsDisplaySettingsConfig((ConfigElement) this.CustomPermissionsDisplaySettings)
      {
        SetName = "General"
      };
      this.CustomPermissionsDisplaySettings.Add(element35);
      SecuredObjectCustomPermissionSet element36 = new SecuredObjectCustomPermissionSet((ConfigElement) element35.SecuredObjectCustomPermissionSets)
      {
        TypeName = typeof (ContentItem).FullName
      };
      element35.SecuredObjectCustomPermissionSets.Add(element36);
      CustomSecurityAction element37 = new CustomSecurityAction((ConfigElement) element36.CustomSecurityActions)
      {
        Name = "Create",
        ShowActionInList = false,
        Title = string.Empty,
        ResourceClassId = string.Empty
      };
      element36.CustomSecurityActions.Add(element37);
      CustomSecurityAction element38 = new CustomSecurityAction((ConfigElement) element36.CustomSecurityActions)
      {
        Name = "Modify",
        ShowActionInList = true,
        Title = "ModifyThisItem",
        ResourceClassId = typeof (SecurityResources).Name
      };
      element36.CustomSecurityActions.Add(element38);
      CustomSecurityAction element39 = new CustomSecurityAction((ConfigElement) element36.CustomSecurityActions)
      {
        Name = "View",
        ShowActionInList = true,
        Title = "ViewThisItem",
        ResourceClassId = typeof (SecurityResources).Name
      };
      element36.CustomSecurityActions.Add(element39);
      CustomSecurityAction element40 = new CustomSecurityAction((ConfigElement) element36.CustomSecurityActions)
      {
        Name = "Delete",
        ShowActionInList = true,
        Title = "DeleteThisItem",
        ResourceClassId = typeof (SecurityResources).Name
      };
      element36.CustomSecurityActions.Add(element40);
      CustomSecurityAction element41 = new CustomSecurityAction((ConfigElement) element36.CustomSecurityActions)
      {
        Name = "ChangeOwner",
        ShowActionInList = true,
        Title = "ChangeOwnerOfThisItem",
        ResourceClassId = typeof (SecurityResources).Name
      };
      element36.CustomSecurityActions.Add(element41);
      CustomSecurityAction element42 = new CustomSecurityAction((ConfigElement) element36.CustomSecurityActions)
      {
        Name = "ChangePermissions",
        ShowActionInList = true,
        Title = "ChangePermissionsOfThisItem",
        ResourceClassId = typeof (SecurityResources).Name
      };
      element36.CustomSecurityActions.Add(element42);
      CustomSecurityAction element43 = new CustomSecurityAction((ConfigElement) element36.CustomSecurityActions)
      {
        Name = "Unlock",
        ShowActionInList = true,
        Title = "UnlockThisItem",
        ResourceClassId = typeof (SecurityResources).Name
      };
      element36.CustomSecurityActions.Add(element43);
      CustomPermissionsDisplaySettingsConfig element44 = new CustomPermissionsDisplaySettingsConfig((ConfigElement) this.CustomPermissionsDisplaySettings)
      {
        SetName = "Image"
      };
      this.CustomPermissionsDisplaySettings.Add(element44);
      SecuredObjectCustomPermissionSet element45 = new SecuredObjectCustomPermissionSet((ConfigElement) element44.SecuredObjectCustomPermissionSets)
      {
        TypeName = typeof (Telerik.Sitefinity.Libraries.Model.Album).FullName
      };
      element44.SecuredObjectCustomPermissionSets.Add(element45);
      CustomSecurityAction element46 = new CustomSecurityAction((ConfigElement) element45.CustomSecurityActions)
      {
        Name = "ManageImage",
        ShowActionInList = true,
        Title = "ManageThisAlbum",
        ResourceClassId = typeof (SecurityResources).Name
      };
      element45.CustomSecurityActions.Add(element46);
      SecuredObjectCustomPermissionSet element47 = new SecuredObjectCustomPermissionSet((ConfigElement) element44.SecuredObjectCustomPermissionSets)
      {
        TypeName = typeof (Telerik.Sitefinity.Libraries.Model.Image).FullName
      };
      element44.SecuredObjectCustomPermissionSets.Add(element47);
      CustomSecurityAction element48 = new CustomSecurityAction((ConfigElement) element47.CustomSecurityActions)
      {
        Name = "ViewImage",
        ShowActionInList = true,
        Title = "ViewThisImage",
        ResourceClassId = typeof (SecurityResources).Name
      };
      element47.CustomSecurityActions.Add(element48);
      CustomSecurityAction element49 = new CustomSecurityAction((ConfigElement) element47.CustomSecurityActions)
      {
        Name = "ManageImage",
        ShowActionInList = true,
        Title = "ManageImage",
        ResourceClassId = typeof (SecurityResources).Name
      };
      element47.CustomSecurityActions.Add(element49);
      CustomSecurityAction element50 = new CustomSecurityAction((ConfigElement) element47.CustomSecurityActions)
      {
        Name = "ChangeImageOwner",
        ShowActionInList = true,
        Title = "ChangeThisImageOwner",
        ResourceClassId = typeof (SecurityResources).Name
      };
      element47.CustomSecurityActions.Add(element50);
      CustomSecurityAction element51 = new CustomSecurityAction((ConfigElement) element47.CustomSecurityActions)
      {
        Name = "ChangeImagePermissions",
        ShowActionInList = true,
        Title = "ChangeThisImagePermissions",
        ResourceClassId = typeof (SecurityResources).Name
      };
      element47.CustomSecurityActions.Add(element51);
      CustomSecurityAction element52 = new CustomSecurityAction((ConfigElement) element47.CustomSecurityActions)
      {
        Name = "UnlockImage",
        ShowActionInList = true,
        Title = "UnlockThisImage",
        ResourceClassId = typeof (SecurityResources).Name
      };
      element47.CustomSecurityActions.Add(element52);
      CustomPermissionsDisplaySettingsConfig element53 = new CustomPermissionsDisplaySettingsConfig((ConfigElement) this.CustomPermissionsDisplaySettings)
      {
        SetName = "Album"
      };
      this.CustomPermissionsDisplaySettings.Add(element53);
      SecuredObjectCustomPermissionSet element54 = new SecuredObjectCustomPermissionSet((ConfigElement) element53.SecuredObjectCustomPermissionSets)
      {
        TypeName = typeof (Telerik.Sitefinity.Libraries.Model.Album).FullName
      };
      element53.SecuredObjectCustomPermissionSets.Add(element54);
      CustomSecurityAction element55 = new CustomSecurityAction((ConfigElement) element54.CustomSecurityActions)
      {
        Name = "CreateAlbum",
        ShowActionInList = true,
        Title = string.Empty,
        ResourceClassId = string.Empty
      };
      element54.CustomSecurityActions.Add(element55);
      CustomSecurityAction element56 = new CustomSecurityAction((ConfigElement) element54.CustomSecurityActions)
      {
        Name = "ViewAlbum",
        ShowActionInList = true,
        Title = "ViewThisAlbum",
        ResourceClassId = typeof (SecurityResources).Name
      };
      element54.CustomSecurityActions.Add(element56);
      CustomSecurityAction element57 = new CustomSecurityAction((ConfigElement) element54.CustomSecurityActions)
      {
        Name = "DeleteAlbum",
        ShowActionInList = true,
        Title = "DeleteThisAlbum",
        ResourceClassId = typeof (SecurityResources).Name
      };
      element54.CustomSecurityActions.Add(element57);
      CustomSecurityAction element58 = new CustomSecurityAction((ConfigElement) element54.CustomSecurityActions)
      {
        Name = "ChangeAlbumOwner",
        ShowActionInList = true,
        Title = "ChangeThisAlbumOwner",
        ResourceClassId = typeof (SecurityResources).Name
      };
      element54.CustomSecurityActions.Add(element58);
      CustomSecurityAction element59 = new CustomSecurityAction((ConfigElement) element54.CustomSecurityActions)
      {
        Name = "ChangeAlbumPermissions",
        ShowActionInList = true,
        Title = "ChangeThisAlbumPermissions",
        ResourceClassId = typeof (SecurityResources).Name
      };
      element54.CustomSecurityActions.Add(element59);
      CustomPermissionsDisplaySettingsConfig element60 = new CustomPermissionsDisplaySettingsConfig((ConfigElement) this.CustomPermissionsDisplaySettings)
      {
        SetName = "Video"
      };
      this.CustomPermissionsDisplaySettings.Add(element60);
      SecuredObjectCustomPermissionSet element61 = new SecuredObjectCustomPermissionSet((ConfigElement) element60.SecuredObjectCustomPermissionSets)
      {
        TypeName = typeof (Telerik.Sitefinity.Libraries.Model.VideoLibrary).FullName
      };
      element60.SecuredObjectCustomPermissionSets.Add(element61);
      CustomSecurityAction element62 = new CustomSecurityAction((ConfigElement) element61.CustomSecurityActions)
      {
        Name = "ManageVideo",
        ShowActionInList = true,
        Title = "ManageThisVideoLibrary",
        ResourceClassId = typeof (SecurityResources).Name
      };
      element61.CustomSecurityActions.Add(element62);
      SecuredObjectCustomPermissionSet element63 = new SecuredObjectCustomPermissionSet((ConfigElement) element60.SecuredObjectCustomPermissionSets)
      {
        TypeName = typeof (Telerik.Sitefinity.Libraries.Model.Video).FullName
      };
      element60.SecuredObjectCustomPermissionSets.Add(element63);
      CustomSecurityAction element64 = new CustomSecurityAction((ConfigElement) element63.CustomSecurityActions)
      {
        Name = "ViewVideo",
        ShowActionInList = true,
        Title = "ViewThisVideo",
        ResourceClassId = typeof (SecurityResources).Name
      };
      element63.CustomSecurityActions.Add(element64);
      CustomSecurityAction element65 = new CustomSecurityAction((ConfigElement) element63.CustomSecurityActions)
      {
        Name = "ManageVideo",
        ShowActionInList = true,
        Title = "ManageVideo",
        ResourceClassId = typeof (SecurityResources).Name
      };
      element63.CustomSecurityActions.Add(element65);
      CustomSecurityAction element66 = new CustomSecurityAction((ConfigElement) element63.CustomSecurityActions)
      {
        Name = "ChangeVideoOwner",
        ShowActionInList = true,
        Title = "ChangeThisVideoOwner",
        ResourceClassId = typeof (SecurityResources).Name
      };
      element63.CustomSecurityActions.Add(element66);
      CustomSecurityAction element67 = new CustomSecurityAction((ConfigElement) element63.CustomSecurityActions)
      {
        Name = "ChangeVideoPermissions",
        ShowActionInList = true,
        Title = "ChangeThisVideoPermissions",
        ResourceClassId = typeof (SecurityResources).Name
      };
      element63.CustomSecurityActions.Add(element67);
      CustomSecurityAction element68 = new CustomSecurityAction((ConfigElement) element63.CustomSecurityActions)
      {
        Name = "UnlockVideo",
        ShowActionInList = true,
        Title = "UnlockThisVideo",
        ResourceClassId = typeof (SecurityResources).Name
      };
      element63.CustomSecurityActions.Add(element68);
      CustomPermissionsDisplaySettingsConfig element69 = new CustomPermissionsDisplaySettingsConfig((ConfigElement) this.CustomPermissionsDisplaySettings)
      {
        SetName = "VideoLibrary"
      };
      this.CustomPermissionsDisplaySettings.Add(element69);
      SecuredObjectCustomPermissionSet element70 = new SecuredObjectCustomPermissionSet((ConfigElement) element69.SecuredObjectCustomPermissionSets)
      {
        TypeName = typeof (Telerik.Sitefinity.Libraries.Model.VideoLibrary).FullName
      };
      element69.SecuredObjectCustomPermissionSets.Add(element70);
      CustomSecurityAction element71 = new CustomSecurityAction((ConfigElement) element70.CustomSecurityActions)
      {
        Name = "CreateVideoLibrary",
        ShowActionInList = true,
        Title = string.Empty,
        ResourceClassId = string.Empty
      };
      element70.CustomSecurityActions.Add(element71);
      CustomSecurityAction element72 = new CustomSecurityAction((ConfigElement) element70.CustomSecurityActions)
      {
        Name = "ViewVideoLibrary",
        ShowActionInList = true,
        Title = "ViewThisVideoLibrary",
        ResourceClassId = typeof (SecurityResources).Name
      };
      element70.CustomSecurityActions.Add(element72);
      CustomSecurityAction element73 = new CustomSecurityAction((ConfigElement) element70.CustomSecurityActions)
      {
        Name = "DeleteVideoLibrary",
        ShowActionInList = true,
        Title = "DeleteThisVideoLibrary",
        ResourceClassId = typeof (SecurityResources).Name
      };
      element70.CustomSecurityActions.Add(element73);
      CustomSecurityAction element74 = new CustomSecurityAction((ConfigElement) element70.CustomSecurityActions)
      {
        Name = "ChangeVideoLibraryOwner",
        ShowActionInList = true,
        Title = "ChangeThisVideoLibraryOwner",
        ResourceClassId = typeof (SecurityResources).Name
      };
      element70.CustomSecurityActions.Add(element74);
      CustomSecurityAction element75 = new CustomSecurityAction((ConfigElement) element70.CustomSecurityActions)
      {
        Name = "ChangeVideoLibraryPermissions",
        ShowActionInList = true,
        Title = "ChangeThisVideoLibraryPermissions",
        ResourceClassId = typeof (SecurityResources).Name
      };
      element70.CustomSecurityActions.Add(element75);
      CustomPermissionsDisplaySettingsConfig element76 = new CustomPermissionsDisplaySettingsConfig((ConfigElement) this.CustomPermissionsDisplaySettings)
      {
        SetName = "Document"
      };
      this.CustomPermissionsDisplaySettings.Add(element76);
      SecuredObjectCustomPermissionSet element77 = new SecuredObjectCustomPermissionSet((ConfigElement) element76.SecuredObjectCustomPermissionSets)
      {
        TypeName = typeof (Telerik.Sitefinity.Libraries.Model.DocumentLibrary).FullName
      };
      element76.SecuredObjectCustomPermissionSets.Add(element77);
      CustomSecurityAction element78 = new CustomSecurityAction((ConfigElement) element77.CustomSecurityActions)
      {
        Name = "ManageVideo",
        ShowActionInList = true,
        Title = "ManageThisDocumentLibrary",
        ResourceClassId = typeof (SecurityResources).Name
      };
      element77.CustomSecurityActions.Add(element78);
      SecuredObjectCustomPermissionSet element79 = new SecuredObjectCustomPermissionSet((ConfigElement) element76.SecuredObjectCustomPermissionSets)
      {
        TypeName = typeof (Telerik.Sitefinity.Libraries.Model.Document).FullName
      };
      element76.SecuredObjectCustomPermissionSets.Add(element79);
      CustomSecurityAction element80 = new CustomSecurityAction((ConfigElement) element79.CustomSecurityActions)
      {
        Name = "ViewDocument",
        ShowActionInList = true,
        Title = "ViewThisDocument",
        ResourceClassId = typeof (SecurityResources).Name
      };
      element79.CustomSecurityActions.Add(element80);
      CustomSecurityAction element81 = new CustomSecurityAction((ConfigElement) element79.CustomSecurityActions)
      {
        Name = "ManageDocument",
        ShowActionInList = true,
        Title = "ManageDocument",
        ResourceClassId = typeof (SecurityResources).Name
      };
      element79.CustomSecurityActions.Add(element81);
      CustomSecurityAction element82 = new CustomSecurityAction((ConfigElement) element79.CustomSecurityActions)
      {
        Name = "ChangeDocumentOwner",
        ShowActionInList = true,
        Title = "ChangeThisDocumentOwner",
        ResourceClassId = typeof (SecurityResources).Name
      };
      element79.CustomSecurityActions.Add(element82);
      CustomSecurityAction element83 = new CustomSecurityAction((ConfigElement) element79.CustomSecurityActions)
      {
        Name = "ChangeDocumentPermissions",
        ShowActionInList = true,
        Title = "ChangeThisDocumentPermissions",
        ResourceClassId = typeof (SecurityResources).Name
      };
      element79.CustomSecurityActions.Add(element83);
      CustomSecurityAction element84 = new CustomSecurityAction((ConfigElement) element79.CustomSecurityActions)
      {
        Name = "UnlockDocument",
        ShowActionInList = true,
        Title = "UnlockThisDocument",
        ResourceClassId = typeof (SecurityResources).Name
      };
      element79.CustomSecurityActions.Add(element84);
      CustomPermissionsDisplaySettingsConfig element85 = new CustomPermissionsDisplaySettingsConfig((ConfigElement) this.CustomPermissionsDisplaySettings)
      {
        SetName = "DocumentLibrary"
      };
      this.CustomPermissionsDisplaySettings.Add(element85);
      SecuredObjectCustomPermissionSet element86 = new SecuredObjectCustomPermissionSet((ConfigElement) element85.SecuredObjectCustomPermissionSets)
      {
        TypeName = typeof (Telerik.Sitefinity.Libraries.Model.DocumentLibrary).FullName
      };
      element85.SecuredObjectCustomPermissionSets.Add(element86);
      CustomSecurityAction element87 = new CustomSecurityAction((ConfigElement) element86.CustomSecurityActions)
      {
        Name = "CreateDocumentLibrary",
        ShowActionInList = true,
        Title = string.Empty,
        ResourceClassId = string.Empty
      };
      element86.CustomSecurityActions.Add(element87);
      CustomSecurityAction element88 = new CustomSecurityAction((ConfigElement) element86.CustomSecurityActions)
      {
        Name = "ViewDocumentLibrary",
        ShowActionInList = true,
        Title = "ViewThisDocumentLibrary",
        ResourceClassId = typeof (SecurityResources).Name
      };
      element86.CustomSecurityActions.Add(element88);
      CustomSecurityAction element89 = new CustomSecurityAction((ConfigElement) element86.CustomSecurityActions)
      {
        Name = "DeleteDocumentLibrary",
        ShowActionInList = true,
        Title = "DeleteThisDocumentLibrary",
        ResourceClassId = typeof (SecurityResources).Name
      };
      element86.CustomSecurityActions.Add(element89);
      CustomSecurityAction element90 = new CustomSecurityAction((ConfigElement) element86.CustomSecurityActions)
      {
        Name = "ChangeDocumentLibraryOwner",
        ShowActionInList = true,
        Title = "ChangeThisDocumentLibraryOwner",
        ResourceClassId = typeof (SecurityResources).Name
      };
      element86.CustomSecurityActions.Add(element90);
      CustomSecurityAction element91 = new CustomSecurityAction((ConfigElement) element86.CustomSecurityActions)
      {
        Name = "ChangeDocumentLibraryPermissions",
        ShowActionInList = true,
        Title = "ChangeThisDocumentLibraryPermissions",
        ResourceClassId = typeof (SecurityResources).Name
      };
      element86.CustomSecurityActions.Add(element91);
      SecuredObjectCustomPermissionSet element92 = new SecuredObjectCustomPermissionSet((ConfigElement) element85.SecuredObjectCustomPermissionSets)
      {
        TypeName = typeof (Telerik.Sitefinity.Libraries.Model.DocumentLibrary).FullName,
        SecuredObjectIds = UserFilesConstants.DefaultDownloadableGoodsLibraryId.ToString()
      };
      element85.SecuredObjectCustomPermissionSets.Add("UserFileLibraryCustomActions", element92);
      CustomSecurityAction element93 = new CustomSecurityAction((ConfigElement) element92.CustomSecurityActions)
      {
        Name = "CreateDocumentLibrary",
        ShowActionInList = false,
        Title = string.Empty,
        ResourceClassId = string.Empty
      };
      element92.CustomSecurityActions.Add(element93);
      CustomSecurityAction element94 = new CustomSecurityAction((ConfigElement) element92.CustomSecurityActions)
      {
        Name = "ViewDocumentLibrary",
        ShowActionInList = true,
        Title = "ViewThisUserFileLibrary",
        ResourceClassId = typeof (SecurityResources).Name
      };
      element92.CustomSecurityActions.Add(element94);
      CustomSecurityAction element95 = new CustomSecurityAction((ConfigElement) element92.CustomSecurityActions)
      {
        Name = "DeleteDocumentLibrary",
        ShowActionInList = true,
        Title = "DeleteThisUserFileLibrary",
        ResourceClassId = typeof (SecurityResources).Name
      };
      element92.CustomSecurityActions.Add(element95);
      CustomSecurityAction element96 = new CustomSecurityAction((ConfigElement) element92.CustomSecurityActions)
      {
        Name = "ChangeDocumentLibraryOwner",
        ShowActionInList = true,
        Title = "ChangeThisUserFileLibraryOwner",
        ResourceClassId = typeof (SecurityResources).Name
      };
      element92.CustomSecurityActions.Add(element96);
      CustomSecurityAction element97 = new CustomSecurityAction((ConfigElement) element92.CustomSecurityActions)
      {
        Name = "ChangeDocumentLibraryPermissions",
        ShowActionInList = true,
        Title = "ChangeThisUserFileLibraryPermissions",
        ResourceClassId = typeof (SecurityResources).Name
      };
      element92.CustomSecurityActions.Add(element97);
      CustomPermissionsDisplaySettingsConfig element98 = new CustomPermissionsDisplaySettingsConfig((ConfigElement) this.CustomPermissionsDisplaySettings)
      {
        SetName = "Taxonomies"
      };
      this.CustomPermissionsDisplaySettings.Add(element98);
      SecuredObjectCustomPermissionSet element99 = new SecuredObjectCustomPermissionSet((ConfigElement) element98.SecuredObjectCustomPermissionSets)
      {
        TypeName = typeof (Taxonomy).FullName
      };
      element98.SecuredObjectCustomPermissionSets.Add(element99);
      CustomSecurityAction element100 = new CustomSecurityAction((ConfigElement) element99.CustomSecurityActions)
      {
        Name = "CreateTaxonomy",
        ShowActionInList = false,
        Title = string.Empty,
        ResourceClassId = string.Empty
      };
      element99.CustomSecurityActions.Add(element100);
      CustomSecurityAction element101 = new CustomSecurityAction((ConfigElement) element99.CustomSecurityActions)
      {
        Name = "ModifyTaxonomyAndSubTaxons",
        ShowActionInList = true,
        Title = "ModifyATaxonomy",
        ResourceClassId = typeof (SecurityResources).Name
      };
      element99.CustomSecurityActions.Add(element101);
      CustomPermissionsDisplaySettingsConfig element102 = new CustomPermissionsDisplaySettingsConfig((ConfigElement) this.CustomPermissionsDisplaySettings)
      {
        SetName = "Pages"
      };
      this.CustomPermissionsDisplaySettings.Add(element102);
      SecuredObjectCustomPermissionSet customPermissionSet = new SecuredObjectCustomPermissionSet((ConfigElement) element102.SecuredObjectCustomPermissionSets);
      customPermissionSet.TypeName = typeof (PageNode).FullName;
      Guid guid = SiteInitializer.FrontendRootNodeId;
      string str1 = guid.ToString();
      guid = SiteInitializer.BackendRootNodeId;
      string str2 = guid.ToString();
      customPermissionSet.SecuredObjectIds = str1 + "," + str2;
      SecuredObjectCustomPermissionSet element103 = customPermissionSet;
      element102.SecuredObjectCustomPermissionSets.Add(element103);
      element103.CustomSecurityActions.Add(new CustomSecurityAction((ConfigElement) element103.CustomSecurityActions)
      {
        Name = "View",
        ShowActionInList = true,
        Title = "ViewPages",
        ResourceClassId = typeof (SecurityResources).Name
      });
      element103.CustomSecurityActions.Add(new CustomSecurityAction((ConfigElement) element103.CustomSecurityActions)
      {
        Name = "Create",
        ShowActionInList = true,
        Title = "CreatePages",
        ResourceClassId = typeof (SecurityResources).Name
      });
      element103.CustomSecurityActions.Add(new CustomSecurityAction((ConfigElement) element103.CustomSecurityActions)
      {
        Name = "Modify",
        ShowActionInList = true,
        Title = "ModifyPages",
        ResourceClassId = typeof (SecurityResources).Name
      });
      element103.CustomSecurityActions.Add(new CustomSecurityAction((ConfigElement) element103.CustomSecurityActions)
      {
        Name = "Delete",
        ShowActionInList = true,
        Title = "DeletePages",
        ResourceClassId = typeof (SecurityResources).Name
      });
      element103.CustomSecurityActions.Add(new CustomSecurityAction((ConfigElement) element103.CustomSecurityActions)
      {
        Name = "EditContent",
        ShowActionInList = true,
        Title = "EditPagesContent",
        ResourceClassId = typeof (SecurityResources).Name
      });
      element103.CustomSecurityActions.Add(new CustomSecurityAction((ConfigElement) element103.CustomSecurityActions)
      {
        Name = "CreateChildControls",
        ShowActionInList = true,
        Title = "CreatePagesChildControls",
        ResourceClassId = typeof (SecurityResources).Name
      });
      element103.CustomSecurityActions.Add(new CustomSecurityAction((ConfigElement) element103.CustomSecurityActions)
      {
        Name = "ChangePermissions",
        ShowActionInList = true,
        Title = "ChangePagesPermissions",
        ResourceClassId = typeof (SecurityResources).Name
      });
      element103.CustomSecurityActions.Add(new CustomSecurityAction((ConfigElement) element103.CustomSecurityActions)
      {
        Name = "ChangeOwner",
        ShowActionInList = true,
        Title = "ChangePagesOwner",
        ResourceClassId = typeof (SecurityResources).Name
      });
      element103.CustomSecurityActions.Add(new CustomSecurityAction((ConfigElement) element103.CustomSecurityActions)
      {
        Name = "Unlock",
        ShowActionInList = true,
        Title = "UnlockPages",
        ResourceClassId = typeof (SecurityResources).Name
      });
      CustomPermissionsDisplaySettingsConfig element104 = new CustomPermissionsDisplaySettingsConfig((ConfigElement) this.CustomPermissionsDisplaySettings)
      {
        SetName = "Site"
      };
      this.CustomPermissionsDisplaySettings.Add(element104);
      SecuredObjectCustomPermissionSet element105 = new SecuredObjectCustomPermissionSet((ConfigElement) element104.SecuredObjectCustomPermissionSets)
      {
        TypeName = "Telerik.Sitefinity.Multisite.Model.Site"
      };
      element104.SecuredObjectCustomPermissionSets.Add(element105);
      element105.CustomSecurityActions.Add(new CustomSecurityAction((ConfigElement) element105.CustomSecurityActions)
      {
        Name = "CreateEditSite",
        ShowActionInList = true,
        Title = "CreateEditSite",
        ResourceClassId = name
      });
      element105.CustomSecurityActions.Add(new CustomSecurityAction((ConfigElement) element105.CustomSecurityActions)
      {
        Name = "DeleteSite",
        ShowActionInList = true,
        Title = "DeleteThisSite",
        ResourceClassId = name
      });
      element105.CustomSecurityActions.Add(new CustomSecurityAction((ConfigElement) element105.CustomSecurityActions)
      {
        Name = "ConfigureModules",
        ShowActionInList = true,
        Title = "ConfigureThisSiteModules",
        ResourceClassId = name
      });
      element105.CustomSecurityActions.Add(new CustomSecurityAction((ConfigElement) element105.CustomSecurityActions)
      {
        Name = "StartStopSite",
        ShowActionInList = true,
        Title = "StartStopThisSite",
        ResourceClassId = name
      });
      element105.CustomSecurityActions.Add(new CustomSecurityAction((ConfigElement) element105.CustomSecurityActions)
      {
        Name = "ChangePermissions",
        ShowActionInList = true,
        Title = "ChangePermissionsForThisSite",
        ResourceClassId = name
      });
      element105.CustomSecurityActions.Add(new CustomSecurityAction((ConfigElement) element105.CustomSecurityActions)
      {
        Name = "ChangeOwner",
        ShowActionInList = true,
        Title = "ChangeOwnerForThisSite",
        ResourceClassId = name
      });
      CustomPermissionsDisplaySettingsConfig element106 = new CustomPermissionsDisplaySettingsConfig((ConfigElement) this.CustomPermissionsDisplaySettings)
      {
        SetName = "SitemapGeneration"
      };
      this.CustomPermissionsDisplaySettings.Add(element106);
      SecuredObjectCustomPermissionSet element107 = new SecuredObjectCustomPermissionSet((ConfigElement) element106.SecuredObjectCustomPermissionSets)
      {
        TypeName = "Telerik.Sitefinity.Security.Model.SecurityRoot, Telerik.Sitefinity.Model"
      };
      element106.SecuredObjectCustomPermissionSets.Add(element107);
      CustomSecurityAction element108 = new CustomSecurityAction((ConfigElement) element107.CustomSecurityActions)
      {
        Name = "ViewBackendLink",
        ShowActionInList = true,
        Title = "ViewBackendLinkTitle",
        ResourceClassId = typeof (SecurityResources).Name
      };
      element107.CustomSecurityActions.Add(element108);
      CustomSecurityAction element109 = new CustomSecurityAction((ConfigElement) element107.CustomSecurityActions)
      {
        Name = "ChangeBackendLinkPermissions",
        ShowActionInList = false,
        Title = "ChangeBackendLinkPermissions",
        ResourceClassId = typeof (SecurityResources).Name
      };
      element107.CustomSecurityActions.Add(element109);
      CustomPermissionsDisplaySettingsConfig element110 = new CustomPermissionsDisplaySettingsConfig((ConfigElement) this.CustomPermissionsDisplaySettings)
      {
        SetName = "ImagesSitemapGeneration"
      };
      this.CustomPermissionsDisplaySettings.Add(element110);
      SecuredObjectCustomPermissionSet element111 = new SecuredObjectCustomPermissionSet((ConfigElement) element110.SecuredObjectCustomPermissionSets)
      {
        TypeName = "Telerik.Sitefinity.Security.Model.SecurityRoot, Telerik.Sitefinity.Model"
      };
      element110.SecuredObjectCustomPermissionSets.Add(element111);
      CustomSecurityAction element112 = new CustomSecurityAction((ConfigElement) element111.CustomSecurityActions)
      {
        Name = "ViewBackendLink",
        ShowActionInList = true,
        Title = "ViewBackendLinkTitle",
        ResourceClassId = typeof (SecurityResources).Name
      };
      element111.CustomSecurityActions.Add(element112);
      CustomSecurityAction element113 = new CustomSecurityAction((ConfigElement) element111.CustomSecurityActions)
      {
        Name = "ChangeBackendLinkPermissions",
        ShowActionInList = false,
        Title = "ChangeBackendLinkPermissions",
        ResourceClassId = typeof (SecurityResources).Name
      };
      element111.CustomSecurityActions.Add(element113);
      CustomPermissionsDisplaySettingsConfig element114 = new CustomPermissionsDisplaySettingsConfig((ConfigElement) this.CustomPermissionsDisplaySettings)
      {
        SetName = "VideosSitemapGeneration"
      };
      this.CustomPermissionsDisplaySettings.Add(element114);
      SecuredObjectCustomPermissionSet element115 = new SecuredObjectCustomPermissionSet((ConfigElement) element114.SecuredObjectCustomPermissionSets)
      {
        TypeName = "Telerik.Sitefinity.Security.Model.SecurityRoot, Telerik.Sitefinity.Model"
      };
      element114.SecuredObjectCustomPermissionSets.Add(element115);
      CustomSecurityAction element116 = new CustomSecurityAction((ConfigElement) element115.CustomSecurityActions)
      {
        Name = "ViewBackendLink",
        ShowActionInList = true,
        Title = "ViewBackendLinkTitle",
        ResourceClassId = typeof (SecurityResources).Name
      };
      element115.CustomSecurityActions.Add(element116);
      CustomSecurityAction element117 = new CustomSecurityAction((ConfigElement) element115.CustomSecurityActions)
      {
        Name = "ChangeBackendLinkPermissions",
        ShowActionInList = false,
        Title = "ChangeBackendLinkPermissions",
        ResourceClassId = typeof (SecurityResources).Name
      };
      element115.CustomSecurityActions.Add(element117);
      CustomPermissionsDisplaySettingsConfig element118 = new CustomPermissionsDisplaySettingsConfig((ConfigElement) this.CustomPermissionsDisplaySettings)
      {
        SetName = "DocumentsSitemapGeneration"
      };
      this.CustomPermissionsDisplaySettings.Add(element118);
      SecuredObjectCustomPermissionSet element119 = new SecuredObjectCustomPermissionSet((ConfigElement) element118.SecuredObjectCustomPermissionSets)
      {
        TypeName = "Telerik.Sitefinity.Security.Model.SecurityRoot, Telerik.Sitefinity.Model"
      };
      element118.SecuredObjectCustomPermissionSets.Add(element119);
      CustomSecurityAction element120 = new CustomSecurityAction((ConfigElement) element119.CustomSecurityActions)
      {
        Name = "ViewBackendLink",
        ShowActionInList = true,
        Title = "ViewBackendLinkTitle",
        ResourceClassId = typeof (SecurityResources).Name
      };
      element119.CustomSecurityActions.Add(element120);
      CustomSecurityAction element121 = new CustomSecurityAction((ConfigElement) element119.CustomSecurityActions)
      {
        Name = "ChangeBackendLinkPermissions",
        ShowActionInList = false,
        Title = "ChangeBackendLinkPermissions",
        ResourceClassId = typeof (SecurityResources).Name
      };
      element119.CustomSecurityActions.Add(element121);
      ConfigElementDictionary<string, ApplicationRole> applicationRoles = this.ApplicationRoles;
      applicationRoles.Add(new ApplicationRole((ConfigElement) applicationRoles)
      {
        Name = "Everyone",
        Description = "EveryoneDescription",
        ResourceClassId = name,
        AllowManualUserAssignment = false
      });
      applicationRoles.Add(new ApplicationRole((ConfigElement) applicationRoles)
      {
        Name = "Anonymous",
        Description = "AnonymousDescription",
        ResourceClassId = name,
        AllowManualUserAssignment = false
      });
      applicationRoles.Add(new ApplicationRole((ConfigElement) applicationRoles)
      {
        Name = "Authenticated",
        Description = "AuthenticatedDescription",
        ResourceClassId = name,
        AllowManualUserAssignment = false
      });
      applicationRoles.Add(new ApplicationRole((ConfigElement) applicationRoles)
      {
        Name = "Owner",
        Description = "OwnerDescription",
        ResourceClassId = name,
        AllowManualUserAssignment = false
      });
      applicationRoles.Add(new ApplicationRole((ConfigElement) applicationRoles)
      {
        Name = "Administrators",
        Description = "AdministratorsDescription",
        ResourceClassId = name,
        AllowManualUserAssignment = true
      });
      applicationRoles.Add(new ApplicationRole((ConfigElement) applicationRoles)
      {
        Name = "BackendUsers",
        Description = "BackendUsersRoleDescription",
        ResourceClassId = name,
        AllowManualUserAssignment = true
      });
      applicationRoles.Add(new ApplicationRole((ConfigElement) applicationRoles)
      {
        Name = "Authors",
        Description = "AuthorsRoleDescription",
        ResourceClassId = name,
        AllowManualUserAssignment = true
      });
      applicationRoles.Add(new ApplicationRole((ConfigElement) applicationRoles)
      {
        Name = "Editors",
        Description = "EditorsRoleDescription",
        ResourceClassId = name,
        AllowManualUserAssignment = true
      });
      applicationRoles.Add(new ApplicationRole((ConfigElement) applicationRoles)
      {
        Name = "Designers",
        Description = "DesignersRoleDescription",
        ResourceClassId = name,
        AllowManualUserAssignment = true
      });
      applicationRoles.Add(new ApplicationRole((ConfigElement) applicationRoles)
      {
        Name = "Users",
        Description = "FrontendUsersRoleDescription",
        ResourceClassId = name,
        AllowManualUserAssignment = true
      });
    }

    private void LoadDefaultSiteAdminPermissions()
    {
      ConfigElementDictionary<string, EffectivePermission> adminPermissions = this.UsersPerSiteSettings.SiteAdminPermissions;
      Permission permission;
      if (!this.Permissions.TryGetValue("Backend", out permission))
        return;
      int num = 0;
      SecurityAction securityAction;
      if (permission.Actions.TryGetValue(AppAction.ManageUsers.ToString(), out securityAction))
        num |= securityAction.Value;
      if (permission.Actions.TryGetValue(AppAction.ViewConfigurations.ToString(), out securityAction))
        num |= securityAction.Value;
      if (permission.Actions.TryGetValue(AppAction.ChangeConfigurations.ToString(), out securityAction))
        num |= securityAction.Value;
      if (permission.Actions.TryGetValue(AppAction.ManagePublishingSystem.ToString(), out securityAction))
        num |= securityAction.Value;
      if (permission.Actions.TryGetValue("AccessComments", out securityAction))
        num |= securityAction.Value;
      if (permission.Actions.TryGetValue("AccessForums", out securityAction))
        num |= securityAction.Value;
      if (permission.Actions.TryGetValue("AccessPersonalization", out securityAction))
        num |= securityAction.Value;
      if (permission.Actions.TryGetValue("AccessTranslations", out securityAction))
        num |= securityAction.Value;
      adminPermissions.Add(new EffectivePermission((ConfigElement) adminPermissions)
      {
        Name = "Backend",
        Grant = num
      });
    }

    /// <summary>Gets the Membership.UserIsOnlineTimeWindow.</summary>
    /// <returns></returns>
    internal virtual int GetUserIsOnlineTimeWindow() => Membership.UserIsOnlineTimeWindow;

    /// <summary>
    /// Workaround for bug #36108 - Forums: Forums module is not activated
    /// </summary>
    internal void InvalidatePermissionsCache()
    {
      foreach (Permission permission in (IEnumerable<Permission>) this.Permissions.Values)
        permission.InvalidateCache();
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct ConfigProps
    {
      public const string UserIsOnlineTimeWindow = "userIsOnlineTimeWindow";
    }
  }
}
