// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Upgrades.To5100.ApprovalRecordsUpgrader
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Data.Common;
using System.Linq;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Data.SqlGenerators;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Upgrades.To5100
{
  internal class ApprovalRecordsUpgrader
  {
    public static void UpgradeDb(
      OpenAccessContext context,
      string tableName,
      string providerName,
      string primaryKeyColumn = "content_id",
      string columnToDrop = "id")
    {
      string str = "{0} : Upgrade to {1}. Update approval records.".Arrange((object) providerName, (object) SitefinityVersion.Sitefinity7_0.Build.ToString());
      try
      {
        SqlGenerator gen = SqlGenerator.Get(ApprovalRecordsUpgrader.GetDbType(context));
        ApprovalRecordsUpgrader.UpdateRecords(context, tableName, primaryKeyColumn, gen);
        ApprovalRecordsUpgrader.DropColumn(context, tableName, columnToDrop, gen);
        Log.Write((object) string.Format("PASSED : {0}", (object) str), ConfigurationPolicy.UpgradeTrace);
      }
      catch (Exception ex)
      {
        Log.Write((object) string.Format("FAILED: {0} - {1}", (object) str, (object) ex.Message), ConfigurationPolicy.UpgradeTrace);
        if (!Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
          return;
        throw;
      }
    }

    private static void UpdateRecords(
      OpenAccessContext context,
      string tableName,
      string primaryKeyColumn,
      SqlGenerator gen)
    {
      string columnName1 = gen.GetColumnName("sf_approval_tracking_record");
      string columnName2 = gen.GetColumnName("workflow_item_id");
      string columnName3 = gen.GetColumnName("id");
      string columnName4 = gen.GetColumnName("id2");
      string columnName5 = gen.GetColumnName("status");
      string columnName6 = gen.GetColumnName("sf_pprvl_trckng_rcrd_mp_sf_ppr");
      string tableName1 = gen.GetTableName(tableName);
      string columnName7 = gen.GetColumnName(primaryKeyColumn);
      string commandText = string.Empty;
      switch (ApprovalRecordsUpgrader.GetDbType(context))
      {
        case DatabaseType.MsSql:
          commandText = "  update {2}\r\n                          set {2}.{3} = {0}.{1}\r\n                          from {0}\r\n                          join {4}\r\n                          on {0}.{5} = {4}.{5}\r\n                          join {2}\r\n                          on {4}.{6} = {2}.{5}\r\n                          where {0}.{7} = 0".Arrange((object) tableName1, (object) columnName7, (object) columnName1, (object) columnName2, (object) columnName6, (object) columnName3, (object) columnName4, (object) columnName5);
          break;
        case DatabaseType.Oracle:
          commandText = "UPDATE \"sf_approval_tracking_record\"\r\n                            SET \"sf_approval_tracking_record\".\"workflow_item_id\" = \r\n                            (\r\n\t                            SELECT {0}.{1}\r\n                                FROM {0}\r\n                                JOIN \"sf_pprvl_trckng_rcrd_mp_sf_ppr\"\r\n\t                            ON \"sf_pprvl_trckng_rcrd_mp_sf_ppr\".\"id\" = {0}.\"id\"\r\n                                WHERE \"sf_pprvl_trckng_rcrd_mp_sf_ppr\".\"id2\" = \"sf_approval_tracking_record\".\"id\"\r\n                                AND {0}.\"status\" = 0\r\n                            )\r\n                            WHERE EXISTS\r\n                            (\r\n\t                            SELECT 1\r\n                                FROM {0}\r\n                                JOIN \"sf_pprvl_trckng_rcrd_mp_sf_ppr\"\r\n\t                            ON \"sf_pprvl_trckng_rcrd_mp_sf_ppr\".\"id\" = {0}.\"id\"\r\n                                WHERE \"sf_pprvl_trckng_rcrd_mp_sf_ppr\".\"id2\" = \"sf_approval_tracking_record\".\"id\"\r\n                                AND {0}.\"status\" = 0\r\n                            )".Arrange((object) tableName1, (object) columnName7);
          break;
        case DatabaseType.MySQL:
          commandText = "\r\n                        update sf_approval_tracking_record\r\n                        join sf_approval_tracking_record_map_sf_approval_tracking_record\r\n                        on sf_approval_tracking_record_map_sf_approval_tracking_record.id2 = sf_approval_tracking_record.id\r\n                        join {0}\r\n                        on {0}.id = sf_approval_tracking_record_map_sf_approval_tracking_record.id\r\n                        set sf_approval_tracking_record.workflow_item_id = {0}.{1}\r\n                        where {0}.status = 0".Arrange((object) tableName1, (object) columnName7);
          break;
      }
      context.ExecuteNonQuery(commandText);
    }

    private static void DropColumn(
      OpenAccessContext context,
      string tableName,
      string columnName,
      SqlGenerator gen)
    {
      string dropColumn = gen.GetDropColumn(tableName, columnName);
      context.ExecuteNonQuery(dropColumn);
    }

    internal static void DropConstraints(OpenAccessContext context)
    {
      SqlGenerator sqlGenerator = SqlGenerator.Get(ApprovalRecordsUpgrader.GetDbType(context));
      switch (ApprovalRecordsUpgrader.GetDbType(context))
      {
        case DatabaseType.MySQL:
          context.ExecuteNonQuery(sqlGenerator.GetDropConstraint("sf_approval_tracking_record_map_sf_approval_tracking_record", "ref_sf_pprvl_trckng_rcrd_mp_sf_pprvl_trckng_rcrd_sf_pprvl_trckn2"));
          context.ExecuteNonQuery(sqlGenerator.GetDropConstraint("sf_approval_tracking_record_map_sf_approval_tracking_record", "ref_sf_pprvl_trckng_rcrd_mp_sf_pprvl_trckng_rcrd_sf_pprvl_trckng"));
          context.ExecuteNonQuery(sqlGenerator.GetDropConstraint("sf_approval_tracking_record_sf_approval_tracking_record_map", "ref_sf_pprvl_trckng_rcrd_sf_pprvl_trckng_rcrd_mp_sf_pprvl_trckn2"));
          context.ExecuteNonQuery(sqlGenerator.GetDropConstraint("sf_approval_tracking_record_sf_approval_tracking_record_map", "ref_sf_pprvl_trckng_rcrd_sf_pprvl_trckng_rcrd_mp_sf_pprvl_trckng"));
          break;
        default:
          context.ExecuteNonQuery(sqlGenerator.GetDropConstraint("sf_pprvl_trckng_rcrd_sf_pprvl_", "ref_sf_pprvl_trckng_rcrd_sf_p2"));
          context.ExecuteNonQuery(sqlGenerator.GetDropConstraint("sf_pprvl_trckng_rcrd_sf_pprvl_", "ref_sf_pprvl_trckng_rcrd_sf_pp"));
          context.ExecuteNonQuery(sqlGenerator.GetDropConstraint("sf_pprvl_trckng_rcrd_mp_sf_ppr", "ref_sf_pprvl_trckng_rcrd_mp_s2"));
          context.ExecuteNonQuery(sqlGenerator.GetDropConstraint("sf_pprvl_trckng_rcrd_mp_sf_ppr", "ref_sf_pprvl_trckng_rcrd_mp_sf"));
          break;
      }
    }

    internal static void DropApprovalRecordTables(OpenAccessContext context)
    {
      SqlGenerator sqlGenerator = SqlGenerator.Get(ApprovalRecordsUpgrader.GetDbType(context));
      switch (ApprovalRecordsUpgrader.GetDbType(context))
      {
        case DatabaseType.MsSql:
          context.ExecuteNonQuery(sqlGenerator.GetDropTable("sf_approval_tracking_record_map"));
          context.ExecuteNonQuery(sqlGenerator.GetDropTable("sf_pprvl_trckng_rcrd_sf_pprvl_"));
          context.ExecuteNonQuery(sqlGenerator.GetDropTable("sf_pprvl_trckng_rcrd_mp_sf_ppr"));
          break;
        case DatabaseType.Oracle:
          context.ExecuteNonQuery(sqlGenerator.GetDropTable("sf_apprvl_trckng_rcrd_mp"));
          context.ExecuteNonQuery(sqlGenerator.GetDropTable("sf_pprvl_trckng_rcrd_sf_pprvl_"));
          context.ExecuteNonQuery(sqlGenerator.GetDropTable("sf_pprvl_trckng_rcrd_mp_sf_ppr"));
          break;
        case DatabaseType.MySQL:
          context.ExecuteNonQuery(sqlGenerator.GetDropTable("sf_approval_tracking_record_map"));
          context.ExecuteNonQuery(sqlGenerator.GetDropTable("sf_approval_tracking_record_map_sf_approval_tracking_record"));
          context.ExecuteNonQuery(sqlGenerator.GetDropTable("sf_approval_tracking_record_sf_approval_tracking_record_map"));
          break;
      }
    }

    internal static void UpgradeAllAvailableDbs()
    {
      string str = "Clean approval records : Upgrade to {0}".Arrange((object) SitefinityVersion.Sitefinity7_0.Build.ToString());
      foreach (OpenAccessConnection connection in OpenAccessConnection.GetConnections())
      {
        System.Action<OpenAccessContext>[] actionArray = new System.Action<OpenAccessContext>[2]
        {
          new System.Action<OpenAccessContext>(ApprovalRecordsUpgrader.DropConstraints),
          new System.Action<OpenAccessContext>(ApprovalRecordsUpgrader.DropApprovalRecordTables)
        };
        foreach (System.Action<OpenAccessContext> action in actionArray)
        {
          try
          {
            using (SitefinityOAContext currentContext = connection.GetCurrentContext((IOpenAccessMetadataProvider) null))
            {
              if (currentContext.Metadata.PersistentTypes.Any<MetaPersistentType>((Func<MetaPersistentType, bool>) (x => x.FullName == "Telerik.Sitefinity.Workflow.Model.Tracking.ApprovalTrackingRecord")))
              {
                action((OpenAccessContext) currentContext);
                currentContext.SaveChanges();
              }
            }
          }
          catch (DbException ex)
          {
          }
          catch (Exception ex)
          {
            Log.Write((object) string.Format("FAILED: {0} - {1}", (object) str, (object) ex.Message), ConfigurationPolicy.UpgradeTrace);
            if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
              throw;
          }
        }
        Log.Write((object) string.Format("PASSED : {0}", (object) str), ConfigurationPolicy.UpgradeTrace);
      }
    }

    private static DatabaseType GetDbType(OpenAccessContext context)
    {
      switch (context)
      {
        case SitefinityOAContext _:
          return (context as SitefinityOAContext).OpenAccessConnection.DbType;
        case UpgradingContext _:
          return (context as UpgradingContext).Connection.DbType;
        default:
          return DatabaseType.Unspecified;
      }
    }
  }
}
