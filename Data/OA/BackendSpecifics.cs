// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.OA.BackendSpecifics
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.Configuration;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Data.OA
{
  internal class BackendSpecifics
  {
    /// <summary>Gets the naming settings for specified database type.</summary>
    /// <param name="dbType">Type of the db.</param>
    public static BackendNamingSettings GetNamingSettingsForBackend(
      DatabaseType dbType)
    {
      switch (dbType)
      {
        case DatabaseType.MsSql:
          return BackendSpecifics.GetSqlNamingSettings();
        case DatabaseType.SqlAzure:
          if (Config.Get<DataConfig>().DatabaseMappingOptions.AzureOptions.UseMsSqlIdentifierLimitations)
            return BackendSpecifics.GetSqlNamingSettings();
          break;
        case DatabaseType.Oracle:
          return BackendSpecifics.GetOracleNamingSettings();
        case DatabaseType.MySQL:
          return BackendSpecifics.GetMySqlNamingSettings();
        case DatabaseType.PostgreSql:
          return BackendSpecifics.GetPostgreSqlNamingSettings();
      }
      return new BackendNamingSettings();
    }

    /// <summary>Gets the naming settings for MsSql database.</summary>
    /// <returns></returns>
    public static BackendNamingSettings GetSqlNamingSettings() => new BackendNamingSettings()
    {
      MaxColumnNameLength = 128,
      MaxTableNameLength = 128,
      MaxConstraintNameLength = 128,
      MaxIndexNameLength = 128,
      MaxProcedureNameLength = 128,
      ReservedWords = {
        ["system_user"] = "sys_user",
        ["identity"] = "ident"
      }
    };

    /// <summary>Gets the naming settings for MySql database.</summary>
    /// <returns></returns>
    public static BackendNamingSettings GetMySqlNamingSettings() => new BackendNamingSettings()
    {
      MaxColumnNameLength = 64,
      MaxTableNameLength = 64,
      MaxConstraintNameLength = 64,
      MaxIndexNameLength = 64,
      MaxProcedureNameLength = 64,
      ReservedWords = {
        ["interval"] = "intervl",
        ["change"] = "chnge",
        ["keys"] = "kys",
        ["xor"] = "xr",
        ["connection"] = "connectn"
      }
    };

    /// <summary>Gets the naming settings for oracle database.</summary>
    /// <returns></returns>
    public static BackendNamingSettings GetOracleNamingSettings() => new BackendNamingSettings()
    {
      MaxColumnNameLength = 30,
      MaxTableNameLength = 30,
      MaxConstraintNameLength = 30,
      MaxIndexNameLength = 30,
      MaxProcedureNameLength = 30,
      NameCanStartWithUnderscore = false
    };

    /// <summary>Gets the naming settings for oracle database.</summary>
    /// <returns></returns>
    public static BackendNamingSettings GetPostgreSqlNamingSettings() => new BackendNamingSettings()
    {
      MaxColumnNameLength = 63,
      MaxTableNameLength = 63,
      MaxConstraintNameLength = 63,
      MaxIndexNameLength = 63,
      MaxProcedureNameLength = 63,
      NameCanStartWithUnderscore = true
    };
  }
}
