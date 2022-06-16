// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.OA.SitefinityDBSchemaGenerator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Data.OA
{
  /// <summary>
  /// This class is used for testing open access compatibility and integration issues
  /// </summary>
  internal class SitefinityDBSchemaGenerator
  {
    internal static string mySqlFalseConnectionString = "Server=192.168.57.95;Uid=root;Pwd=password;Database=SF_OpenAcess_garo;CharacterSet=utf8";
    internal static string msSqlFalseConnectionString = "Server=.\\SQLEXPRESSNOTINITIALIZED; Database=Release_OA; Integrated Security = true";
    internal static string oracleFalseConnectionString = "Data Source=SF;User Id=Garo;Password=admin;";
    internal static string postgreSqlFalseConnectionString = "Server=127.0.0.1;Port=5432;Database=PGDb;User Id=fcb;Password=1234;";

    internal static OpenAccessContext CreateEmptyMsSQLContext()
    {
      SitefinityOpenAccessMetadataSource accessMetadataSource = new SitefinityOpenAccessMetadataSource((IDatabaseMappingContext) new MsSqlDatabaseMappingContext());
      BackendConfiguration backendConfiguration = new BackendConfiguration()
      {
        Backend = "mssql"
      };
      return new OpenAccessContext(SitefinityDBSchemaGenerator.msSqlFalseConnectionString, backendConfiguration, (MetadataSource) accessMetadataSource);
    }

    internal static OpenAccessContext CreateEmptyMySQLContext()
    {
      SitefinityOpenAccessMetadataSource accessMetadataSource = new SitefinityOpenAccessMetadataSource((IDatabaseMappingContext) new MySqlDatabaseMappingContext());
      BackendConfiguration backendConfiguration = new BackendConfiguration()
      {
        Backend = "mysql"
      };
      return new OpenAccessContext(SitefinityDBSchemaGenerator.mySqlFalseConnectionString, backendConfiguration, (MetadataSource) accessMetadataSource);
    }

    internal static OpenAccessContext CreateEmptyOracleContext()
    {
      SitefinityOpenAccessMetadataSource accessMetadataSource = new SitefinityOpenAccessMetadataSource((IDatabaseMappingContext) new OracleDatabaseMappingContext());
      BackendConfiguration backendConfiguration = new BackendConfiguration()
      {
        Backend = "oracle"
      };
      return new OpenAccessContext(SitefinityDBSchemaGenerator.oracleFalseConnectionString, backendConfiguration, (MetadataSource) accessMetadataSource);
    }

    /// <summary>
    /// Creates PostgreSql context with a false connection string but able to construct DDL create script
    /// Here is how the DDL script should look like for PostgreSql:
    /// -- Telerik.Sitefinity.Newsletters.Model.ABCampaign
    /// </summary>
    /// <returns>OpenAccessContext constructed with PostgreSqlDatabaseMappingContext</returns>
    internal static OpenAccessContext CreateEmptyPostgreSqlContext()
    {
      SitefinityOpenAccessMetadataSource accessMetadataSource = new SitefinityOpenAccessMetadataSource((IDatabaseMappingContext) new PostgreSqlDatabaseMappingContext());
      BackendConfiguration backendConfiguration = new BackendConfiguration()
      {
        Backend = "postgresql"
      };
      return new OpenAccessContext(SitefinityDBSchemaGenerator.postgreSqlFalseConnectionString, backendConfiguration, (MetadataSource) accessMetadataSource);
    }

    internal static string CreateDDLScript(OpenAccessContext context) => context.GetSchemaHandler().CreateDDLScript();

    internal static string GenerateCreateDDLScript(string backend)
    {
      if (backend.Equals("mssql", StringComparison.CurrentCultureIgnoreCase))
      {
        using (OpenAccessContext emptyMsSqlContext = SitefinityDBSchemaGenerator.CreateEmptyMsSQLContext())
          return SitefinityDBSchemaGenerator.CreateDDLScript(emptyMsSqlContext);
      }
      else if (backend.Equals("mysql", StringComparison.CurrentCultureIgnoreCase))
      {
        using (OpenAccessContext emptyMySqlContext = SitefinityDBSchemaGenerator.CreateEmptyMySQLContext())
          return SitefinityDBSchemaGenerator.CreateDDLScript(emptyMySqlContext);
      }
      else if (backend.Equals("oracle", StringComparison.CurrentCultureIgnoreCase))
      {
        using (OpenAccessContext emptyOracleContext = SitefinityDBSchemaGenerator.CreateEmptyOracleContext())
          return SitefinityDBSchemaGenerator.CreateDDLScript(emptyOracleContext);
      }
      else
      {
        if (!backend.Equals("postgresql", StringComparison.CurrentCultureIgnoreCase))
          throw new ArgumentOutOfRangeException(backend, "No such backend DB supported");
        using (OpenAccessContext postgreSqlContext = SitefinityDBSchemaGenerator.CreateEmptyPostgreSqlContext())
          return SitefinityDBSchemaGenerator.CreateDDLScript(postgreSqlContext);
      }
    }
  }
}
