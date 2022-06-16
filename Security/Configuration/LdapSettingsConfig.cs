// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Configuration.LdapSettingsConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using System.DirectoryServices.Protocols;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Security.Configuration
{
  /// <summary>Summary description for LdapConnectionSettings</summary>
  public class LdapSettingsConfig : ConfigElement
  {
    internal LdapSettingsConfig()
      : base(false)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.Configuration.LdapSettingsConfig" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public LdapSettingsConfig(ConfigElement parent)
      : base(parent)
    {
    }

    internal LdapSettingsConfig(bool check)
      : base(check)
    {
    }

    /// <summary>Gets or sets the name of the Ldap config element</summary>
    [ObjectInfo(typeof (ConfigDescriptions), Title = "ItemName")]
    [ConfigurationProperty("name", DefaultValue = "", IsKey = true, IsRequired = true)]
    public virtual string Name
    {
      get => (string) this["name"];
      set => this["name"] = (object) value;
    }

    /// <summary>Ldap server name</summary>
    [ObjectInfo(typeof (ConfigDescriptions), Title = "LdapServerName")]
    [ConfigurationProperty("serverName", IsRequired = true)]
    public virtual string ServerName
    {
      get => (string) this["serverName"];
      set => this["serverName"] = (object) value;
    }

    /// <summary>Ldap server port</summary>
    [ObjectInfo(typeof (ConfigDescriptions), Title = "LdapServerPort")]
    [ConfigurationProperty("serverPort", DefaultValue = 389, IsRequired = true)]
    public virtual int Port
    {
      get => (int) this["serverPort"];
      set => this["serverPort"] = (object) value;
    }

    /// <summary>Ldap domain</summary>
    [ObjectInfo(typeof (ConfigDescriptions), Title = "LdapConnectionDomain")]
    [ConfigurationProperty("connectionDomain")]
    public virtual string ConnectionDomain
    {
      get => (string) this["connectionDomain"];
      set => this["connectionDomain"] = (object) value;
    }

    /// <summary>Ldap username for connecting to ldap server</summary>
    [ObjectInfo(typeof (ConfigDescriptions), Title = "LdapConnectionUsername")]
    [ConfigurationProperty("connectionUsername", IsRequired = false)]
    public virtual string ConnectionUsername
    {
      get => (string) this["connectionUsername"];
      set => this["connectionUsername"] = (object) value;
    }

    /// <summary>Ldap user password for connecting to ldap server</summary>
    [ObjectInfo(typeof (ConfigDescriptions), Title = "LdapConnectionPassword")]
    [ConfigurationProperty("connectionPassword", IsRequired = false)]
    public virtual string ConnectionPassword
    {
      get => (string) this["connectionPassword"];
      set => this["connectionPassword"] = (object) value;
    }

    /// <summary>indicates if the connection used SSL</summary>
    [ObjectInfo(typeof (ConfigDescriptions), Title = "LdapUseSSL")]
    [ConfigurationProperty("useSSL", DefaultValue = false, IsRequired = false)]
    public virtual bool UseSsl
    {
      get => (bool) this["useSSL"];
      set => this["useSSL"] = (object) value;
    }

    /// <summary>Ldap server name</summary>
    [ConfigurationProperty("resultCacheExpiration", DefaultValue = 0, IsRequired = false)]
    public virtual int ResultCacheExpirationTime
    {
      get => (int) this["resultCacheExpiration"];
      set => this["resultCacheExpiration"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the max returned users for the Ldap connection.
    /// </summary>
    /// <value>The max returned users.</value>
    [ObjectInfo(typeof (ConfigDescriptions), Title = "LdapMaxReturnedUsers")]
    [ConfigurationProperty("maxReturnedUsers", DefaultValue = 10, IsRequired = true)]
    public virtual int MaxReturnedUsers
    {
      get => (int) this["maxReturnedUsers"];
      set => this["maxReturnedUsers"] = (object) value;
    }

    /// <summary>
    /// Ldap distinguished name path(organizational unit) for users traversing and search. Users are searched from this path down the organziational tree
    /// </summary>
    [ObjectInfo(typeof (ConfigDescriptions), Title = "LdapUserDNs")]
    [ConfigurationProperty("usersDN", IsRequired = true)]
    public virtual string UserDns
    {
      get => (string) this["usersDN"];
      set => this["usersDN"] = (object) value;
    }

    /// <summary>Custom user filter in ldap format</summary>
    [ObjectInfo(typeof (ConfigDescriptions), Title = "LdapUserFilter")]
    [ConfigurationProperty("userFilter", DefaultValue = "(&(!(objectClass=computer))(objectClass=person))", IsRequired = false)]
    public virtual string UserFilter
    {
      get => (string) this["userFilter"];
      set => this["userFilter"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the max returned roles for the Ldap connection.
    /// </summary>
    /// <value>The max returned roles.</value>
    [ObjectInfo(typeof (ConfigDescriptions), Title = "LdapMaxReturnedRoles")]
    [ConfigurationProperty("maxReturnedRoles", DefaultValue = 10, IsRequired = true)]
    public virtual int MaxReturnedRoles
    {
      get => (int) this["maxReturnedRoles"];
      set => this["maxReturnedRoles"] = (object) value;
    }

    /// <summary>
    /// LDAP distinguished name path (organizational unit) for roles traversing and search. Roles are searched from this path down the organizational tree
    /// </summary>
    [ObjectInfo(typeof (ConfigDescriptions), Title = "LdapRolesDNs")]
    [ConfigurationProperty("rolesDns", IsRequired = true)]
    public virtual string RolesDNs
    {
      get => (string) this["rolesDns"];
      set => this["rolesDns"] = (object) value;
    }

    /// <summary>Custom role filter in LDAP format</summary>
    [ObjectInfo(typeof (ConfigDescriptions), Title = "LdapRolesFilter")]
    [ConfigurationProperty("roleFilter", IsRequired = false)]
    public virtual string RolesFilter
    {
      get => (string) this["roleFilter"];
      set => this["roleFilter"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the LDAP connection will use the currently logged in user credentials or the configuration credentials
    /// </summary>
    /// <value>
    /// 	<c>true</c> if [connect with logon credentials]; otherwise, <c>false</c>.
    /// </value>
    [ConfigurationProperty("connectWithLogonCredentials", DefaultValue = false)]
    public virtual bool ConnectWithLogOnCredentials
    {
      get => (bool) this["connectWithLogonCredentials"];
      set => this["connectWithLogonCredentials"] = (object) value;
    }

    /// <summary>Gets or sets the LDAP protocol version</summary>
    /// <value>Gets or sets the LDAP protocol version</value>
    [ObjectInfo(typeof (ConfigDescriptions), Description = "LdapVersionDescription", Title = "LdapVersion")]
    [ConfigurationProperty("ldapVersion", DefaultValue = 2)]
    public virtual int LdapVersion
    {
      get => (int) this["ldapVersion"];
      set => this["ldapVersion"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the type of the authentication used to connect to the LDAP: Anonymous, Basic, Digest, Dpa, External, Kerberos, Msn, Negotiate, Ntlm or Sicily
    /// </summary>
    /// <value>Gets or sets the type of the authentication used to connect to the LDAP: Anonymous, Basic, Digest, Dpa, External, Kerberos, Msn, Negotiate, Ntlm or Sicily</value>
    [ObjectInfo(typeof (ConfigDescriptions), Title = "LdapAuthenticationType")]
    [ConfigurationProperty("authenticationType", DefaultValue = AuthType.Negotiate)]
    public virtual AuthType AuthenticationType
    {
      get => (AuthType) this["authenticationType"];
      set => this["authenticationType"] = (object) value;
    }
  }
}
