// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Ldap.LdapSecurityConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using System.DirectoryServices.Protocols;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Security.Ldap
{
  /// <summary>Ldap connection configuration</summary>
  public class LdapSecurityConfig : ConfigSection
  {
    /// <summary>
    /// Gets or sets the name of the server for the Ldap connection.
    /// </summary>
    /// <value>The name of the server.</value>
    [ConfigurationProperty("serverName", IsRequired = true)]
    public string ServerName
    {
      get => (string) this["serverName"];
      set => this["serverName"] = (object) value;
    }

    /// <summary>Gets or sets the server port for the Ldap connection.</summary>
    /// <value>The server port.</value>
    [ConfigurationProperty("serverPort", DefaultValue = 389, IsRequired = true)]
    public int ServerPort
    {
      get => (int) this["serverPort"];
      set => this["serverPort"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the name of the user for the Ldap connection.
    /// </summary>
    /// <value>The name of the user.</value>
    [ConfigurationProperty("userName")]
    public string UserName
    {
      get => (string) this["userName"];
      set => this["userName"] = (object) value;
    }

    /// <summary>Gets or sets the password for the Ldap connection.</summary>
    /// <value>The password.</value>
    [ConfigurationProperty("password")]
    public string Password
    {
      get => (string) this["password"];
      set => this["password"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the type of the authentication for the Ldap connection.
    /// </summary>
    /// <value>The type of the authentication.</value>
    [ConfigurationProperty("authenticationType", DefaultValue = AuthType.Basic, IsRequired = true)]
    public AuthType AuthenticationType
    {
      get => (AuthType) this["authenticationType"];
      set => this["authenticationType"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the max returned users for the Ldap connection.
    /// </summary>
    /// <value>The max returned users.</value>
    [ConfigurationProperty("maxReturnedUsers", DefaultValue = 0, IsRequired = false)]
    public int MaxReturnedUsers
    {
      get => (int) this["maxReturnedUsers"];
      set => this["maxReturnedUsers"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the max returned roles for the Ldap connection.
    /// </summary>
    /// <value>The max returned roles.</value>
    [ConfigurationProperty("maxReturnedRoles", DefaultValue = 0, IsRequired = false)]
    public int MaxReturnedRoles
    {
      get => (int) this["maxReturnedRoles"];
      set => this["maxReturnedRoles"] = (object) value;
    }
  }
}
