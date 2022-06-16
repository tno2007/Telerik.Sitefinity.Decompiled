// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Installer.StartupConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ComponentModel;
using System.Configuration;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Installer
{
  /// <summary>
  /// This configuration is written by the Project Manager during express installation, providing the information needed to
  /// bypass the startup wizard and go directly to the Dashboard.
  /// </summary>
  /// <remarks>
  /// Any changes to the format of the configuration should also be reflected in Project Manager's code.
  /// </remarks>
  [Browsable(false)]
  internal class StartupConfig : ConfigSection
  {
    private const string EnabledPropName = "enabled";
    private const string InitializedPropName = "initialized";
    private const string SqlInstancePropName = "sqlInstance";
    private const string DBTypePropName = "dbType";
    private const string DBNamePropName = "dbName";
    private const string SqlAuthUserNamePropName = "sqlAuthUserName";
    private const string SqlAuthUserPasswordPropName = "sqlAuthUserPassword";
    private const string OracleDatabasePropName = "oracleDatabase";
    private const string OracleUsernamePropName = "oracleUsername";
    private const string OraclePasswordPropName = "oraclePassword";
    private const string MysqlServerPropName = "mysqlServerName";
    private const string MysqlPortPropName = "mysqlPort";
    private const string MysqlUsernamePropName = "mysqlUsername";
    private const string MysqlPasswordPropName = "mysqlPassword";
    private const string MysqlDatabasePropName = "mysqlDatabase";
    private const string PostgrSqlServerPropName = "postgresqlServerName";
    private const string PostgrSqlPortPropName = "postgresqlPort";
    private const string PostgrSqlUserIdPropName = "postgresqlUserId";
    private const string PostgrSqlPasswordPropName = "postgresqlPassword";
    private const string PostgrSqlDatabasePropName = "postgresqlDatabase";
    private const string UserNamePropName = "username";
    private const string PasswordPropName = "password";
    private const string FirstNamePropName = "firstName";
    private const string LastNamePropName = "lastName";
    private const string EmailPropName = "email";
    private const string SiteUrlPropName = "siteUrl";
    private const string SkipAdminUserPropName = "skipAdminUser";

    public override bool VisibleInUI => false;

    [ConfigurationProperty("enabled", DefaultValue = false)]
    public bool Enabled
    {
      get => (bool) this["enabled"];
      set => this["enabled"] = (object) value;
    }

    [ConfigurationProperty("initialized", DefaultValue = false)]
    public new bool Initialized
    {
      get => (bool) this["initialized"];
      set => this["initialized"] = (object) value;
    }

    [ConfigurationProperty("skipAdminUser", DefaultValue = false)]
    public bool SkipAdminUser
    {
      get => (bool) this["skipAdminUser"];
      set => this["skipAdminUser"] = (object) value;
    }

    [ConfigurationProperty("dbType")]
    public string DBType
    {
      get => (string) this["dbType"];
      set => this["dbType"] = (object) value;
    }

    [ConfigurationProperty("oracleDatabase", DefaultValue = "")]
    public string OracleDatabase
    {
      get => (string) this["oracleDatabase"];
      set => this["oracleDatabase"] = (object) value;
    }

    [ConfigurationProperty("oracleUsername", DefaultValue = "")]
    public string OracleUsername
    {
      get => (string) this["oracleUsername"];
      set => this["oracleUsername"] = (object) value;
    }

    [ConfigurationProperty("oraclePassword", DefaultValue = "")]
    public string OraclePassword
    {
      get => (string) this["oraclePassword"];
      set => this["oraclePassword"] = (object) value;
    }

    [ConfigurationProperty("mysqlServerName", DefaultValue = "")]
    public string MysqlServer
    {
      get => (string) this["mysqlServerName"];
      set => this["mysqlServerName"] = (object) value;
    }

    [ConfigurationProperty("mysqlPort", DefaultValue = "")]
    public string MysqlPort
    {
      get => (string) this["mysqlPort"];
      set => this["mysqlPort"] = (object) value;
    }

    [ConfigurationProperty("mysqlUsername", DefaultValue = "")]
    public string MysqlUsername
    {
      get => (string) this["mysqlUsername"];
      set => this["mysqlUsername"] = (object) value;
    }

    [ConfigurationProperty("mysqlPassword", DefaultValue = "")]
    public string MysqlPassword
    {
      get => (string) this["mysqlPassword"];
      set => this["mysqlPassword"] = (object) value;
    }

    [ConfigurationProperty("mysqlDatabase", DefaultValue = "")]
    public string MysqlDatabase
    {
      get => (string) this["mysqlDatabase"];
      set => this["mysqlDatabase"] = (object) value;
    }

    [ConfigurationProperty("postgresqlServerName", DefaultValue = "")]
    public string PostgreSqlServer
    {
      get => (string) this["postgresqlServerName"];
      set => this["postgresqlServerName"] = (object) value;
    }

    [ConfigurationProperty("postgresqlPort", DefaultValue = "")]
    public string PostgreSqlPort
    {
      get => (string) this["postgresqlPort"];
      set => this["postgresqlPort"] = (object) value;
    }

    [ConfigurationProperty("postgresqlUserId", DefaultValue = "")]
    public string PostgreSqlUserId
    {
      get => (string) this["postgresqlUserId"];
      set => this["postgresqlUserId"] = (object) value;
    }

    [ConfigurationProperty("postgresqlPassword", DefaultValue = "")]
    public string PostgreSqlPassword
    {
      get => (string) this["postgresqlPassword"];
      set => this["postgresqlPassword"] = (object) value;
    }

    [ConfigurationProperty("postgresqlDatabase", DefaultValue = "")]
    public string PostgreSqlDatabase
    {
      get => (string) this["postgresqlDatabase"];
      set => this["postgresqlDatabase"] = (object) value;
    }

    [ConfigurationProperty("sqlInstance", DefaultValue = "")]
    public string SqlInstance
    {
      get => (string) this["sqlInstance"];
      set => this["sqlInstance"] = (object) value;
    }

    [ConfigurationProperty("dbName", DefaultValue = "")]
    public string DBName
    {
      get => (string) this["dbName"];
      set => this["dbName"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the user name that will be used to establish SQL connection.
    /// </summary>
    /// <remarks>
    /// If present SQL authentication will be used instead of Windows authentication.
    /// </remarks>
    /// <value>The name of the DB user.</value>
    [ConfigurationProperty("sqlAuthUserName", DefaultValue = "")]
    public string SqlAuthUserName
    {
      get => (string) this["sqlAuthUserName"];
      set => this["sqlAuthUserName"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the password that will be used to establish SQL connection.
    /// </summary>
    /// <value>The database user password.</value>
    [ConfigurationProperty("sqlAuthUserPassword", DefaultValue = "")]
    public string SqlAuthUserPassword
    {
      get => (string) this["sqlAuthUserPassword"];
      set => this["sqlAuthUserPassword"] = (object) value;
    }

    [ConfigurationProperty("password", DefaultValue = "")]
    public string Password
    {
      get => (string) this["password"];
      set => this["password"] = (object) value;
    }

    [ConfigurationProperty("firstName", DefaultValue = "")]
    public string FirstName
    {
      get => (string) this["firstName"];
      set => this["firstName"] = (object) value;
    }

    [ConfigurationProperty("lastName", DefaultValue = "")]
    public string LastName
    {
      get => (string) this["lastName"];
      set => this["lastName"] = (object) value;
    }

    [ConfigurationProperty("email", DefaultValue = "")]
    public string Email
    {
      get => (string) this["email"];
      set => this["email"] = (object) value;
    }

    [ConfigurationProperty("siteUrl", DefaultValue = "")]
    public string SiteUrl
    {
      get => (string) this["siteUrl"];
      set => this["siteUrl"] = (object) value;
    }

    [ConfigurationProperty("username", DefaultValue = "")]
    public string UserName
    {
      get => (string) this["username"];
      set => this["username"] = (object) value;
    }

    internal void Clear()
    {
      this.Email = string.Empty;
      this.FirstName = string.Empty;
      this.LastName = string.Empty;
    }
  }
}
