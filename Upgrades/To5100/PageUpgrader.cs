// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Upgrades.To5100.PageUpgrader
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Telerik.OpenAccess;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Data.SqlGenerators;
using Telerik.Sitefinity.Descriptors;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Upgrades.To5100
{
  internal class PageUpgrader
  {
    public void Upgrade(
      UpgradingContext context,
      SitefinityMetadataSourceBase metadataSource,
      int upgradedFromSchemaVersionNumber)
    {
      string str = "OpenAccessPageProvider : Upgrade to {0}".Arrange((object) SitefinityVersion.Sitefinity7_0.Build.ToString());
      try
      {
        this.UpgradePageNodePageData(context, metadataSource);
        context.SaveChanges();
        Log.Write((object) string.Format("PASSED : {0}", (object) str), ConfigurationPolicy.UpgradeTrace);
      }
      catch (Exception ex)
      {
        Log.Write((object) string.Format("FAILED: {0} - {1}", (object) str, (object) ex.Message), ConfigurationPolicy.UpgradeTrace);
        if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
          throw;
      }
      if (upgradedFromSchemaVersionNumber >= 3900)
      {
        try
        {
          str = "OpenAccessPageProvider : Upgrade to {0} . Updating sf_content_relation".Arrange((object) SitefinityVersion.Sitefinity7_0.Build.ToString());
          this.UpgradeContentItemRelations(context);
          context.SaveChanges();
          Log.Write((object) string.Format("PASSED : {0}", (object) str), ConfigurationPolicy.UpgradeTrace);
        }
        catch (Exception ex)
        {
          Log.Write((object) string.Format("FAILED: {0} - {1}", (object) str, (object) ex.Message), ConfigurationPolicy.UpgradeTrace);
          if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
            throw;
        }
      }
      try
      {
        str = "OpenAccessPageProvider : Upgrade to {0} . Updating sf_rdsgn_media_query_link".Arrange((object) SitefinityVersion.Sitefinity7_0.Build.ToString());
        this.UpgradeResponsiveDesignMediaQueries(context);
        context.SaveChanges();
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

    private void UpgradeContentItemRelations(UpgradingContext context)
    {
      SqlGenerator.Get(context.Connection.DbType);
      string str1 = "sf_content_relation";
      string str2 = "object_id";
      string str3 = "sf_page_node";
      string str4 = "content_id";
      string str5 = "object_type";
      string str6 = "id";
      string empty = string.Empty;
      string commandText;
      switch (context.DatabaseContext.DatabaseType)
      {
        case DatabaseType.Oracle:
          commandText = "UPDATE \"sf_content_relation\"\r\n                            SET \"sf_content_relation\".\"object_id\" = \r\n                            (\r\n\t                            SELECT \"sf_page_node\".\"content_id\"\r\n                                FROM \"sf_page_node\"\r\n                                WHERE \"sf_page_node\".\"id\" = \"sf_content_relation\".\"object_id\"\r\n                            ),\r\n                            \"sf_content_relation\".\"object_type\" = 'Telerik.Sitefinity.Pages.Model.PageData'\r\n                            WHERE EXISTS\r\n                            (\r\n\t                            SELECT 1\r\n                                FROM \"sf_page_node\"\r\n                                WHERE \"sf_page_node\".\"id\" = \"sf_content_relation\".\"object_id\"\r\n                            )";
          break;
        case DatabaseType.MySQL:
          commandText = "update {0}\r\n                        join {2}                                                 \r\n                        on {0}.{1} = {2}.{5}\r\n                        set {1} = {2}.{3},\r\n                        {4} = 'Telerik.Sitefinity.Pages.Model.PageData'".Arrange((object) str1, (object) str2, (object) str3, (object) str4, (object) str5, (object) str6);
          break;
        default:
          commandText = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'{0}') AND type in (N'U'))\r\n                        update {0} set\r\n                        {1} = {2}.{3},\r\n                        {4} = 'Telerik.Sitefinity.Pages.Model.PageData'\r\n                        from {0} \r\n                        join {2} \r\n                        on {0}.{1} = {2}.{5}".Arrange((object) str1, (object) str2, (object) str3, (object) str4, (object) str5, (object) str6);
          break;
      }
      context.ExecuteNonQuery(commandText);
    }

    private void UpgradeResponsiveDesignMediaQueries(UpgradingContext context)
    {
      SqlGenerator.Get(context.Connection.DbType);
      string str1 = "sf_rdsgn_media_query_link";
      string str2 = "item_id";
      string str3 = "sf_page_node";
      string str4 = "content_id";
      string str5 = "id";
      string empty = string.Empty;
      string commandText;
      switch (context.DatabaseContext.DatabaseType)
      {
        case DatabaseType.Oracle:
          commandText = "UPDATE \"sf_rdsgn_media_query_link\"\r\n                            SET \"sf_rdsgn_media_query_link\".\"item_id\" = \r\n                            (\r\n\t                            SELECT \"sf_page_node\".\"content_id\"\r\n                                FROM \"sf_page_node\"\r\n                                WHERE \"sf_page_node\".\"id\" = \"sf_rdsgn_media_query_link\".\"item_id\"\r\n                            )\r\n                            WHERE EXISTS\r\n                            (\r\n\t                            SELECT 1\r\n                                FROM \"sf_page_node\"\r\n                                WHERE \"sf_page_node\".\"id\" = \"sf_rdsgn_media_query_link\".\"item_id\"\r\n                            )";
          break;
        case DatabaseType.MySQL:
          commandText = " update {0} \r\n                             join {2} \r\n                             on {0}.{1} = {2}.{4}\r\n                             set {1} = {2}.{3}".Arrange((object) str1, (object) str2, (object) str3, (object) str4, (object) str5);
          break;
        default:
          commandText = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'{0}') AND type in (N'U')) \r\n                        update {0} set\r\n                        {1} = {2}.{3}\r\n                        from {0}\r\n                        join {2} \r\n                        on {0}.{1} = {2}.{4}".Arrange((object) str1, (object) str2, (object) str3, (object) str4, (object) str5);
          break;
      }
      context.ExecuteNonQuery(commandText);
    }

    /// <summary>
    /// Updates reference from PageData to PageNode and moves page metadata information from PageData to PageNode.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="sfVersion"></param>
    private void UpgradePageNodePageData(
      UpgradingContext context,
      SitefinityMetadataSourceBase metadataSource)
    {
      SqlGenerator sqlGen = SqlGenerator.Get(context.Connection.DbType);
      PageUpgrader.UpdatePageDataPageNodeReference(context, sqlGen);
      PageUpgrader.UpdatePageNodePropertiesFromPageData(context, sqlGen);
      string commandText1 = "UPDATE {0}\r\n                    SET {1} = {2}".Arrange((object) sqlGen.GetTableName("sf_page_data"), (object) sqlGen.GetColumnName("culture"), (object) sqlGen.GetColumnName("ui_culture"));
      context.ExecuteNonQuery(commandText1);
      string commandText2 = "UPDATE {0}\r\n                    SET {1} = {2}".Arrange((object) sqlGen.GetTableName("sf_page_node"), (object) sqlGen.GetColumnName("inc_in_srch_idx"), (object) sqlGen.GetColumnName("crawlable"));
      context.ExecuteNonQuery(commandText2);
      this.MigrateMLProperties(context, sqlGen, metadataSource);
      ApprovalRecordsUpgrader.DropConstraints((OpenAccessContext) context);
    }

    private static void UpdatePageNodePropertiesFromPageData(
      UpgradingContext context,
      SqlGenerator sqlGen)
    {
      string empty = string.Empty;
      string commandText;
      switch (context.DatabaseContext.DatabaseType)
      {
        case DatabaseType.Oracle:
          commandText = "UPDATE \"sf_page_node\"\r\n                            SET \"sf_page_node\".\"crawlable\" = \r\n                            (\r\n\t                            SELECT \"sf_page_data\".\"crawlable\"\r\n                                FROM \"sf_page_data\"\r\n                                WHERE \"sf_page_data\".\"content_id\" = \"sf_page_node\".\"content_id\"\r\n                            ),\r\n                            \"sf_page_node\".\"require_ssl\" = \r\n                            (\r\n\t                            SELECT \"sf_page_data\".\"require_ssl\"\r\n                                FROM \"sf_page_data\"\r\n                                WHERE \"sf_page_data\".\"content_id\" = \"sf_page_node\".\"content_id\"\r\n                            ),\r\n                            \"sf_page_node\".\"loc_strtgy\" = \r\n                            (\r\n\t                            SELECT \"sf_page_data\".\"localization_strategy\"\r\n                                FROM \"sf_page_data\"\r\n                                WHERE \"sf_page_data\".\"content_id\" = \"sf_page_node\".\"content_id\"\r\n                            )\r\n                            WHERE EXISTS\r\n                            (\r\n\t                            SELECT 1\r\n                                FROM \"sf_page_data\"\r\n                                WHERE \"sf_page_data\".\"content_id\" = \"sf_page_node\".\"content_id\"\r\n                            )";
          break;
        case DatabaseType.MySQL:
          commandText = "\r\n                    UPDATE sf_page_node\r\n                    INNER JOIN sf_page_data\r\n                    ON sf_page_node.content_id = sf_page_data.content_id\r\n                    SET    sf_page_node.crawlable = sf_page_data.crawlable,\r\n                           sf_page_node.require_ssl = sf_page_data.require_ssl,\r\n                           sf_page_node.loc_strtgy = sf_page_data.localization_strategy";
          break;
        default:
          commandText = "\r\n                    UPDATE sf_page_node\r\n                    SET    sf_page_node.crawlable = sf_page_data.crawlable,\r\n                           sf_page_node.require_ssl = sf_page_data.require_ssl,\r\n                           sf_page_node.loc_strtgy = ISNULL(sf_page_data.localization_strategy, 0)\r\n                    FROM   sf_page_node\r\n                    INNER JOIN sf_page_data\r\n                    ON sf_page_node.content_id = sf_page_data.content_id";
          break;
      }
      context.ExecuteNonQuery(commandText);
    }

    private static void UpdatePageDataPageNodeReference(
      UpgradingContext context,
      SqlGenerator sqlGen)
    {
      string empty = string.Empty;
      string commandText;
      switch (context.DatabaseContext.DatabaseType)
      {
        case DatabaseType.Oracle:
          commandText = "UPDATE \"sf_page_data\"\r\n                            SET \"sf_page_data\".\"page_node_id\" = \r\n                            (\r\n\t                            SELECT \"sf_page_node\".\"id\"\r\n                                FROM \"sf_page_node\"\r\n                                WHERE \"sf_page_data\".\"content_id\" = \"sf_page_node\".\"content_id\"\r\n                            )\r\n                            WHERE EXISTS\r\n                            (\r\n\t                            SELECT 1\r\n                                FROM \"sf_page_node\"\r\n                                WHERE \"sf_page_data\".\"content_id\" = \"sf_page_node\".\"content_id\"\r\n                            )";
          break;
        case DatabaseType.MySQL:
          commandText = "\r\n                        UPDATE sf_page_data\r\n                        INNER JOIN sf_page_node\r\n                        ON sf_page_node.content_id = sf_page_data.content_id\r\n                        SET sf_page_data.page_node_id = sf_page_node.id";
          break;
        default:
          commandText = "\r\n                        UPDATE sf_page_data\r\n                        SET sf_page_data.page_node_id = sf_page_node.id\r\n                        FROM sf_page_node\r\n                        INNER JOIN sf_page_data on sf_page_node.content_id = sf_page_data.content_id";
          break;
      }
      context.ExecuteNonQuery(commandText);
    }

    private void RemoveRedundantLanguageForProperty(
      UpgradingContext context,
      string columnName,
      string splitColumnName,
      CultureInfo[] cultures,
      CultureInfo[] splitCultures,
      SqlGenerator sqlGen)
    {
      string tableName1 = sqlGen.GetTableName("sf_page_data");
      string tableName2 = sqlGen.GetTableName("sf_page_node");
      string columnName1 = sqlGen.GetColumnName("page_node_id");
      string columnName2 = sqlGen.GetColumnName("id");
      string columnName3 = sqlGen.GetColumnName("content_id");
      string columnName4 = sqlGen.GetColumnName("root_id");
      string empty = string.Empty;
      foreach (CultureInfo splitCulture in splitCultures)
      {
        string tableName3 = sqlGen.GetTableName(LstringPropertyDescriptor.GetFieldNameForCulture("sf_page_node", splitCulture));
        string tableName4 = sqlGen.GetTableName(LstringPropertyDescriptor.GetFieldNameForCulture("sf_page_data", splitCulture));
        string columnName5 = sqlGen.GetColumnName(LstringPropertyDescriptor.GetFieldNameForCulture(splitColumnName, splitCulture));
        string commandText;
        switch (context.DatabaseContext.DatabaseType)
        {
          case DatabaseType.Oracle:
            commandText = "UPDATE {0}\r\n                                SET {0}.{2} = NULL\r\n                                WHERE EXISTS\r\n                                (\r\n\t                                SELECT 1\r\n                                    FROM \"sf_page_node\"\r\n                                    INNER JOIN \"sf_page_data\" ON \"sf_page_data\".\"page_node_id\" = {0}.\"id\"\r\n                                    INNER JOIN {1} ON \"sf_page_data\".\"content_id\" = {1}.\"content_id\"\r\n                                    WHERE \"sf_page_node\".\"id\" = {0}.\"id\"\r\n                                    AND {0}.{2} IS NULL\r\n                                    AND \"sf_page_node\".\"root_id\" != 'f669d9a7-009d-4d83-ddaa-000000000003'\r\n                                )".Arrange((object) tableName3, (object) tableName4, (object) columnName5);
            break;
          case DatabaseType.MySQL:
            commandText = "    \r\n                            UPDATE {0}\r\n                            INNER JOIN {9} ON {9}.{5} = {0}.{5}\r\n                            INNER JOIN {3} ON {3}.{4} = {0}.{5}\r\n                            INNER JOIN {1} ON {3}.{6} = {1}.{6}\r\n                            SET {0}.{2} = NULL\r\n                            WHERE {1}.{2} IS NULL AND {9}.{7} != '{8}'".Arrange((object) tableName3, (object) tableName4, (object) columnName5, (object) tableName1, (object) columnName1, (object) columnName2, (object) columnName3, (object) columnName4, (object) SiteInitializer.BackendRootNodeId, (object) tableName2);
            break;
          default:
            commandText = "    \r\n                            UPDATE {0}\r\n                            SET {2} = NULL\r\n                            FROM {0}\r\n                            INNER JOIN {9} ON {9}.{5} = {0}.{5}\r\n                            INNER JOIN {3} ON {3}.{4} = {0}.{5}\r\n                            INNER JOIN {1} ON {3}.{6} = {1}.{6}\r\n                            WHERE {1}.{2} IS NULL AND {9}.{7} != '{8}'".Arrange((object) tableName3, (object) tableName4, (object) columnName5, (object) tableName1, (object) columnName1, (object) columnName2, (object) columnName3, (object) columnName4, (object) SiteInitializer.BackendRootNodeId, (object) tableName2);
            break;
        }
        context.ExecuteNonQuery(commandText);
      }
      foreach (CultureInfo culture in cultures)
      {
        string commandText;
        switch (context.DatabaseContext.DatabaseType)
        {
          case DatabaseType.Oracle:
            commandText = " UPDATE \"sf_page_node\"\r\n                                SET \"sf_page_node\".{0} = NULL\r\n                                WHERE EXISTS\r\n                                (\r\n\t                                SELECT 1\r\n                                    FROM \"sf_page_data\"\r\n                                    WHERE \"sf_page_data\".\"page_node_id\" = \"sf_page_node\".\"id\"\r\n                                    AND \"sf_page_data\".{0} IS NULL\r\n                                )".Arrange((object) sqlGen.GetColumnName(LstringPropertyDescriptor.GetFieldNameForCulture(columnName, culture)));
            break;
          case DatabaseType.MySQL:
            commandText = "    \r\n                            UPDATE {2}\r\n                            INNER JOIN {1} ON {2}.{3} = {1}.{4}                            \r\n                            SET {2}.{0} = NULL\r\n                            WHERE {1}.{0} IS NULL AND {2}.{5} != '{6}'".Arrange((object) sqlGen.GetColumnName(LstringPropertyDescriptor.GetFieldNameForCulture(columnName, culture)), (object) tableName1, (object) tableName2, (object) columnName2, (object) columnName1, (object) columnName4, (object) SiteInitializer.BackendRootNodeId);
            break;
          default:
            commandText = "    \r\n                            UPDATE {2}\r\n                            SET {0} = NULL\r\n                            FROM {2}\r\n                            INNER JOIN {1} ON {2}.{3} = {1}.{4}\r\n                            WHERE {1}.{0} IS NULL AND {2}.{5} != '{6}'".Arrange((object) sqlGen.GetColumnName(LstringPropertyDescriptor.GetFieldNameForCulture(columnName, culture)), (object) tableName1, (object) tableName2, (object) columnName2, (object) columnName1, (object) columnName4, (object) SiteInitializer.BackendRootNodeId);
            break;
        }
        context.ExecuteNonQuery(commandText);
      }
    }

    private void MigrateMLProperties(
      UpgradingContext context,
      SqlGenerator sqlGen,
      SitefinityMetadataSourceBase metadataSource)
    {
      CultureInfo[] cultureInfoArray1 = new CultureInfo[0];
      CultureInfo[] splitCultures = new CultureInfo[0];
      CultureInfo[] cultureInfoArray2 = MetadataSourceAggregator.GetConfiguredCultures();
      IDatabaseMappingOptions mappingOptions = metadataSource.MappingOptions;
      if (mappingOptions.UseMultilingualSplitTables)
      {
        IEnumerable<CultureInfo> ignoredCultures = mappingOptions.SplitTablesIgnoredCultures.Select<string, CultureInfo>((Func<string, CultureInfo>) (x => CultureInfo.GetCultureInfo(x)));
        splitCultures = ((IEnumerable<CultureInfo>) cultureInfoArray2).Where<CultureInfo>((Func<CultureInfo, bool>) (x => !ignoredCultures.Contains<CultureInfo>(x))).ToArray<CultureInfo>();
        cultureInfoArray2 = ((IEnumerable<CultureInfo>) cultureInfoArray2).Where<CultureInfo>((Func<CultureInfo, bool>) (x => ignoredCultures.Contains<CultureInfo>(x))).ToArray<CultureInfo>();
      }
      this.RemoveRedundantLanguageForProperty(context, "title", "Title", cultureInfoArray2, splitCultures, sqlGen);
      context.ExecuteNonQuery(sqlGen.GetDropColumn("sf_page_data", "crawlable"));
      context.ExecuteNonQuery(sqlGen.GetDropColumn("sf_page_data", "require_ssl"));
      context.ExecuteNonQuery(sqlGen.GetDropColumn("sf_page_data", "ui_culture"));
      context.ExecuteNonQuery(sqlGen.GetDropColumn("sf_page_data", "localization_strategy"));
      context.ExecuteNonQuery(sqlGen.GetDropColumn("sf_page_data", "translation_initialized"));
      context.ExecuteNonQuery(sqlGen.GetDropColumn("sf_page_node", "id2"));
      context.ExecuteNonQuery(sqlGen.GetDropColumn("sf_draft_pages", "id2"));
      context.ExecuteNonQuery(sqlGen.GetDropColumn("sf_control_properties", "multilingual"));
      this.DropMLProperty(context, "title", "Title", cultureInfoArray2, splitCultures, sqlGen);
      this.DropMLProperty(context, "url_name", "UrlName", cultureInfoArray2, splitCultures, sqlGen);
      context.ExecuteNonQuery(sqlGen.GetDropColumn("sf_page_data", "allow_track_backs"));
      context.ExecuteNonQuery(sqlGen.GetDropColumn("sf_page_data", "allow_comments"));
      context.ExecuteNonQuery(sqlGen.GetDropColumn("sf_page_data", "approve_comments"));
      context.ExecuteNonQuery(sqlGen.GetDropColumn("sf_page_data", "email_author"));
      context.ExecuteNonQuery(sqlGen.GetDropColumn("sf_page_data", "draft_culture"));
      context.ExecuteNonQuery(sqlGen.GetDropColumn("sf_page_data", "post_rights"));
    }

    private void DropMLProperty(
      UpgradingContext context,
      string columnName,
      string splitColumnName,
      CultureInfo[] cultures,
      CultureInfo[] splitCultures,
      SqlGenerator sqlGen)
    {
      foreach (CultureInfo splitCulture in splitCultures)
      {
        string commandText = "ALTER TABLE {0} DROP COLUMN {1}".Arrange((object) sqlGen.GetTableName(LstringPropertyDescriptor.GetFieldNameForCulture("sf_page_data", splitCulture)), (object) sqlGen.GetColumnName(LstringPropertyDescriptor.GetFieldNameForCulture(splitColumnName, splitCulture)));
        context.ExecuteNonQuery(commandText);
      }
      string str;
      if (PageUpgrader.GetDbType((OpenAccessContext) context) == DatabaseType.Oracle)
        str = "ALTER TABLE {1} DROP ({0}".Arrange((object) sqlGen.GetColumnName(LstringPropertyDescriptor.GetFieldNameForCulture(columnName, CultureInfo.InvariantCulture)), (object) sqlGen.GetTableName("sf_page_data"));
      else
        str = "ALTER TABLE {1} DROP COLUMN {0}".Arrange((object) sqlGen.GetColumnName(LstringPropertyDescriptor.GetFieldNameForCulture(columnName, CultureInfo.InvariantCulture)), (object) sqlGen.GetTableName("sf_page_data"));
      StringBuilder stringBuilder = new StringBuilder();
      foreach (CultureInfo culture in cultures)
      {
        switch (PageUpgrader.GetDbType((OpenAccessContext) context))
        {
          case DatabaseType.MySQL:
            stringBuilder.AppendFormat(", DROP COLUMN {0}", (object) sqlGen.GetColumnName(LstringPropertyDescriptor.GetFieldNameForCulture(columnName, culture)));
            break;
          default:
            stringBuilder.AppendFormat(", {0}", (object) sqlGen.GetColumnName(LstringPropertyDescriptor.GetFieldNameForCulture(columnName, culture)));
            break;
        }
      }
      if (PageUpgrader.GetDbType((OpenAccessContext) context) == DatabaseType.Oracle)
        stringBuilder.Append(")");
      context.ExecuteNonQuery(str + stringBuilder.ToString());
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
