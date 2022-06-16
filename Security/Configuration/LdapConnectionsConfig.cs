// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Configuration.LdapConnectionsConfig
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
  /// <summary>
  /// </summary>
  public class LdapConnectionsConfig : ConfigElement
  {
    protected const string defaultLdapConnection = "DefaultLdapConnection";

    /// <summary>
    /// </summary>
    internal LdapConnectionsConfig()
      : base(false)
    {
    }

    /// <summary>
    /// </summary>
    /// <param name="parent"></param>
    public LdapConnectionsConfig(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets or sets the name of the default ldap provider</summary>
    [ObjectInfo(typeof (ConfigDescriptions), Title = "DefaultLdapConnection")]
    [ConfigurationProperty("defaultLdapConnection", DefaultValue = "DefaultLdapConnection")]
    public virtual string DefaultLdapConnection
    {
      get => (string) this["defaultLdapConnection"];
      set => this["defaultLdapConnection"] = (object) value;
    }

    /// <summary>Gets a collection of data provider settings.</summary>
    [ObjectInfo(typeof (ConfigDescriptions), Title = "LdapConnectionsTitle")]
    [ConfigurationProperty("connections", IsDefaultCollection = true)]
    [ConfigurationCollection(typeof (LdapSettingsConfig), AddItemName = "LdapConnection")]
    public virtual ConfigElementDictionary<string, LdapSettingsConfig> Connections
    {
      get => (ConfigElementDictionary<string, LdapSettingsConfig>) this["connections"];
      set => this["connections"] = (object) value;
    }

    /// <summary>
    /// Called after the properties of this instance have been initialized.
    /// Load default values here.
    /// </summary>
    protected override void OnPropertiesInitialized()
    {
      base.OnPropertiesInitialized();
      this.AddDefaultConnection();
    }

    public virtual void AddDefaultConnection()
    {
      if (this.Connections.Count != 0)
        return;
      lock (this.Connections)
      {
        if (this.Connections.Count != 0)
          return;
        this.Connections.Add("DefaultLdapConnection", new LdapSettingsConfig((ConfigElement) this.Connections)
        {
          Name = "DefaultLdapConnection",
          ServerName = "",
          Port = 389,
          AuthenticationType = AuthType.Negotiate,
          ConnectionDomain = "",
          ConnectionUsername = "",
          ConnectionPassword = "",
          UserDns = "",
          UserFilter = "(&(objectClass=user)(!(objectClass=computer)))",
          RolesDNs = "",
          RolesFilter = "(objectClass=group)",
          MaxReturnedRoles = 200,
          MaxReturnedUsers = 200,
          ResultCacheExpirationTime = 0
        });
      }
    }

    /// <summary>
    /// 
    /// </summary>
    [ObjectInfo(typeof (ConfigDescriptions), Title = "LDAPMapping")]
    [ConfigurationProperty("ldapMapping", IsRequired = false)]
    public virtual LdapMappingConfig LdapMapping
    {
      get => (LdapMappingConfig) this["ldapMapping"];
      set => this["ldapMapping"] = (object) value;
    }
  }
}
