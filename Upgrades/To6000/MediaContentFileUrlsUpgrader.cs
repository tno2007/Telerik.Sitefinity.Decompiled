// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Upgrades.To6000.MediaContentFileUrlsUpgrader
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Telerik.OpenAccess;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Data.SqlGenerators;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Services.Web.UI;

namespace Telerik.Sitefinity.Upgrades.To6000
{
  /// <summary>
  /// Contains the sql scripts for upgrading media content file urls migration in Sitefinity 9.0
  /// </summary>
  [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Reviewed.")]
  internal class MediaContentFileUrlsUpgrader
  {
    private UpgradingContext context;
    private int upgradedFromSchemaVersionNumber;
    private static string invariantCulture = CultureInfo.InvariantCulture.LCID.ToString();
    private static Table mediaFileLinksTable = new Table("sf_media_file_links");
    private static Column mflId = new Column("id", MediaContentFileUrlsUpgrader.mediaFileLinksTable);
    private static Column mflWidth = new Column("width", MediaContentFileUrlsUpgrader.mediaFileLinksTable);
    private static Column mflTotalSize = new Column("total_size", MediaContentFileUrlsUpgrader.mediaFileLinksTable);
    private static Column mflNumberOfChunks = new Column("number_of_chunks", MediaContentFileUrlsUpgrader.mediaFileLinksTable);
    private static Column mflContentId = new Column("content_id", MediaContentFileUrlsUpgrader.mediaFileLinksTable);
    private static Column mflHeight = new Column("height", MediaContentFileUrlsUpgrader.mediaFileLinksTable);
    private static Column mflFilePath = new Column("file_path", MediaContentFileUrlsUpgrader.mediaFileLinksTable);
    private static Column mflFileId = new Column("file_id", MediaContentFileUrlsUpgrader.mediaFileLinksTable);
    private static Column mflExtension = new Column("extension", MediaContentFileUrlsUpgrader.mediaFileLinksTable);
    private static Column mflDefaultUrl = new Column("default_url", MediaContentFileUrlsUpgrader.mediaFileLinksTable);
    private static Column mflCulture = new Column("culture", MediaContentFileUrlsUpgrader.mediaFileLinksTable);
    private static Column mflChunkSize = new Column("chunk_size", MediaContentFileUrlsUpgrader.mediaFileLinksTable);
    private static Column mflMimeType = new Column("mime_type", MediaContentFileUrlsUpgrader.mediaFileLinksTable);
    private static Column mflAppName = new Column("app_name", MediaContentFileUrlsUpgrader.mediaFileLinksTable);
    private static Table mediaContentTable = new Table("sf_media_content");
    private static Column mcWidth = new Column("width", MediaContentFileUrlsUpgrader.mediaContentTable);
    private static Column mcTotalSize = new Column("total_size", MediaContentFileUrlsUpgrader.mediaContentTable);
    private static Column mcNumberOfChunks = new Column("number_of_chunks", MediaContentFileUrlsUpgrader.mediaContentTable);
    private static Column mcMimeType = new Column("mime_type", MediaContentFileUrlsUpgrader.mediaContentTable);
    private static Column mcContentId = new Column("content_id", MediaContentFileUrlsUpgrader.mediaContentTable);
    private static Column mcHeight = new Column("height", MediaContentFileUrlsUpgrader.mediaContentTable);
    private static Column mcFilePath = new Column("file_path", MediaContentFileUrlsUpgrader.mediaContentTable);
    private static Column mcFileId = new Column("file_id", MediaContentFileUrlsUpgrader.mediaContentTable);
    private static Column mcExtension = new Column("extension", MediaContentFileUrlsUpgrader.mediaContentTable);
    private static Column mcItemDefaultUrl = new Column("item_default_url_", MediaContentFileUrlsUpgrader.mediaContentTable);
    private static Column mcChunkSize = new Column("chunk_size", MediaContentFileUrlsUpgrader.mediaContentTable);
    private static Column mcWidth2 = new Column("width2", MediaContentFileUrlsUpgrader.mediaContentTable);
    private static Column mcHeight2 = new Column("height2", MediaContentFileUrlsUpgrader.mediaContentTable);
    private static Column mcAppName = new Column("app_name", MediaContentFileUrlsUpgrader.mediaContentTable);
    private static Table mediaFileUrlsTable = new Table("sf_media_file_urls");
    private static Column mfuId = new Column("id", MediaContentFileUrlsUpgrader.mediaFileUrlsTable);
    private static Column mfuUrl = new Column("url", MediaContentFileUrlsUpgrader.mediaFileUrlsTable);
    private static Column mfuMflId = new Column("media_file_link_id", MediaContentFileUrlsUpgrader.mediaFileUrlsTable);
    private static Column mfuIsDefault = new Column("is_default", MediaContentFileUrlsUpgrader.mediaFileUrlsTable);
    private static Column mfuRedirectToDefault = new Column("redirect_to_default", MediaContentFileUrlsUpgrader.mediaFileUrlsTable);
    private static Column mfuAppName = new Column("app_name", MediaContentFileUrlsUpgrader.mediaFileUrlsTable);
    private static Table urlDataTable = new Table("sf_url_data");
    private static Column udUrl = new Column("url", MediaContentFileUrlsUpgrader.urlDataTable);
    private static Column udIsDefault = new Column("is_default", MediaContentFileUrlsUpgrader.urlDataTable);
    private static Column udRedirect = new Column("redirect", MediaContentFileUrlsUpgrader.urlDataTable);
    private static Column udAppName = new Column("app_name", MediaContentFileUrlsUpgrader.urlDataTable);
    private static Column udContentId = new Column("content_id", MediaContentFileUrlsUpgrader.urlDataTable);
    private static Column udCulture = new Column("culture", MediaContentFileUrlsUpgrader.urlDataTable);
    /// <summary>The upgrade script that is executed dynamically.</summary>
    private static string upgradeScriptBackup = "\r\nINSERT INTO sf_media_file_links\r\n            (sf_media_file_links.id,\r\n             sf_media_file_links.width,\r\n             sf_media_file_links.total_size,\r\n             sf_media_file_links.number_of_chunks,\r\n             sf_media_file_links.content_id,\r\n             sf_media_file_links.height,\r\n             sf_media_file_links.file_path,\r\n             sf_media_file_links.file_id,\r\n             sf_media_file_links.extension,\r\n             sf_media_file_links.default_url,\r\n             sf_media_file_links.culture,\r\n             sf_media_file_links.chunk_size,\r\n             sf_media_file_links.mime_type,\r\n             sf_media_file_links.app_name)\r\nSELECT Newid(),\r\n       sf_media_content.width,\r\n       sf_media_content.total_size,\r\n       sf_media_content.number_of_chunks,\r\n       sf_media_content.content_id,\r\n       sf_media_content.height,\r\n       sf_media_content.file_path,\r\n       sf_media_content.file_id,\r\n       sf_media_content.extension,\r\n       sf_url_data.url,\r\n       sf_url_data.culture,\r\n       sf_media_content.chunk_size,\r\n       sf_media_content.mime_type,\r\n       sf_media_content.app_name\r\nFROM   sf_media_content\r\n       INNER JOIN sf_url_data\r\n               ON sf_media_content.content_id = sf_url_data.content_id\r\nWHERE  sf_url_data.is_default = 1\r\n\r\nUPDATE sf_media_file_links\r\nSET    sf_media_file_links.width = sf_media_content.width2,\r\n       sf_media_file_links.height = sf_media_content.height2\r\nFROM   sf_media_content\r\n       INNER JOIN sf_media_file_links\r\n               ON sf_media_content.content_id = sf_media_file_links.content_id\r\nWHERE  sf_media_file_links.width IS NULL\r\n       AND sf_media_file_links.height IS NULL\r\n       AND sf_media_content.width2 IS NOT NULL\r\n       AND sf_media_content.height2 IS NOT NULL\r\n\r\nDELETE sf_media_file_links\r\nFROM   sf_media_file_links\r\nWHERE  sf_media_file_links.culture = 127\r\n       AND sf_media_file_links.content_id IN (SELECT sf_media_file_links.content_id\r\n                                              FROM   sf_media_file_links\r\n                                              GROUP  BY sf_media_file_links.content_id\r\n                                              HAVING Count(sf_media_file_links.id) > 1)\r\n\r\nDELETE U1\r\nFROM sf_url_data U1\r\nINNER JOIN sf_url_data U2 ON U1.content_id = U2.content_id AND U1.url = U2.url\r\nINNER JOIN sf_media_file_links ON sf_media_file_links.content_id = U1.content_id\r\nWHERE U1.id <> U2.id\r\nAND U1.is_default = 0 AND U2.is_default = 1\r\n\r\nINSERT INTO sf_media_file_urls\r\n            (sf_media_file_urls.id,\r\n             sf_media_file_urls.url,\r\n             sf_media_file_urls.media_file_link_id,\r\n             sf_media_file_urls.is_default,\r\n             sf_media_file_urls.redirect_to_default,\r\n             sf_media_file_urls.app_name)\r\nSELECT Newid(),\r\n       sf_url_data.url,\r\n       sf_media_file_links.id,\r\n       sf_url_data.is_default,\r\n       sf_url_data.redirect,\r\n       sf_url_data.app_name\r\nFROM   sf_url_data\r\n       INNER JOIN sf_media_file_links\r\n               ON sf_url_data.content_id = sf_media_file_links.content_id\r\n                  AND sf_url_data.culture = sf_media_file_links.culture\r\n\r\nALTER TABLE sf_media_content\r\n  ALTER COLUMN number_of_chunks INT NULL\r\n\r\nALTER TABLE sf_media_content\r\n  ALTER COLUMN total_size BIGINT NULL\r\n\r\nALTER TABLE sf_media_content\r\n  ALTER COLUMN chunk_size INT NULL \r\n\r\n";

    public MediaContentFileUrlsUpgrader(
      UpgradingContext context,
      int upgradedFromSchemaVersionNumber)
    {
      this.context = context;
      this.upgradedFromSchemaVersionNumber = upgradedFromSchemaVersionNumber;
    }

    /// <summary>Upgrades the specified context.</summary>
    public void Upgrade()
    {
      this.FillMediaFileLinks();
      this.ClearUrlDuplicates();
      this.FillFileUrls();
      this.AlterUnusedColumns();
    }

    private void FillMediaFileLinks()
    {
      SqlGenerator sqlGen = SqlGenerator.Get(this.context.Connection.DbType);
      this.LoadMediaFileLinks(sqlGen);
      this.context.SaveChanges();
      try
      {
        this.UpdateVideosWidthHeight(sqlGen);
        this.context.SaveChanges();
      }
      catch (Exception ex)
      {
        Log.Write((object) "FAILED: Update videos width and height", ConfigurationPolicy.UpgradeTrace);
      }
      try
      {
        this.RemoveInvariantDuplicates(sqlGen);
        this.context.SaveChanges();
      }
      catch (Exception ex)
      {
        Log.Write((object) "FAILED: Remove duplicate entries for invariant culture", ConfigurationPolicy.UpgradeTrace);
      }
    }

    private void ClearUrlDuplicates()
    {
      switch (this.context.Connection.DbType)
      {
        case DatabaseType.Oracle:
          break;
        default:
          this.ExecuteSql("\r\nDELETE U1\r\nFROM sf_url_data U1\r\nINNER JOIN sf_url_data U2 ON U1.content_id = U2.content_id AND U1.url = U2.url\r\nINNER JOIN sf_media_file_links ON sf_media_file_links.content_id = U1.content_id\r\nWHERE U1.id <> U2.id\r\nAND U1.is_default = 0 AND U2.is_default = 1\r\n");
          this.context.SaveChanges();
          break;
      }
    }

    private void FillFileUrls()
    {
      SqlGenerator generator = SqlGenerator.Get(this.context.Connection.DbType);
      List<Column> insertedColumns = new List<Column>()
      {
        MediaContentFileUrlsUpgrader.mfuId,
        MediaContentFileUrlsUpgrader.mfuUrl,
        MediaContentFileUrlsUpgrader.mfuMflId,
        MediaContentFileUrlsUpgrader.mfuIsDefault,
        MediaContentFileUrlsUpgrader.mfuRedirectToDefault,
        MediaContentFileUrlsUpgrader.mfuAppName
      };
      List<string> columnValues = new List<string>()
      {
        generator.GetNewId(),
        MediaContentFileUrlsUpgrader.udUrl.GetSql(generator),
        MediaContentFileUrlsUpgrader.mflId.GetSql(generator),
        MediaContentFileUrlsUpgrader.udIsDefault.GetSql(generator),
        MediaContentFileUrlsUpgrader.udRedirect.GetSql(generator),
        MediaContentFileUrlsUpgrader.udAppName.GetSql(generator)
      };
      this.ExecuteSql(this.context.Connection.DbType != DatabaseType.Oracle ? generator.GetInsert(MediaContentFileUrlsUpgrader.mediaFileUrlsTable, insertedColumns, MediaContentFileUrlsUpgrader.urlDataTable, columnValues, new List<JoinTableClause>()
      {
        new JoinTableClause(MediaContentFileUrlsUpgrader.mediaFileLinksTable, new Dictionary<Column, Column>()
        {
          {
            MediaContentFileUrlsUpgrader.udContentId,
            MediaContentFileUrlsUpgrader.mflContentId
          },
          {
            MediaContentFileUrlsUpgrader.udCulture,
            MediaContentFileUrlsUpgrader.mflCulture
          }
        })
      }) : "INSERT INTO \"sf_media_file_urls\"\r\n                (\"sf_media_file_urls\".\"id\",\r\n                 \"sf_media_file_urls\".\"url\",\r\n                 \"sf_media_file_urls\".\"media_file_link_id\",\r\n                 \"sf_media_file_urls\".\"is_default\",\r\n                 \"sf_media_file_urls\".\"redirect_to_default\",\r\n                 \"sf_media_file_urls\".\"app_name\")\r\n                SELECT \"sf_url_data\".\"id\",\r\n                       \"sf_url_data\".\"url\",\r\n                       \"sf_media_file_links\".\"id\",\r\n                       \"sf_url_data\".\"is_default\",\r\n                       \"sf_url_data\".\"redirect\",\r\n                       \"sf_url_data\".\"app_name\"\r\n                FROM   \"sf_url_data\"\r\n                       INNER JOIN \"sf_media_file_links\"\r\n                   ON \"sf_url_data\".\"content_id\" = \"sf_media_file_links\".\"content_id\"\r\n                      AND \"sf_url_data\".\"culture\" = \"sf_media_file_links\".\"culture\"");
      this.context.SaveChanges();
    }

    private void AlterUnusedColumns()
    {
      SqlGenerator sqlGenerator = SqlGenerator.Get(this.context.Connection.DbType);
      if (this.context.Connection.DbType == DatabaseType.MsSql)
      {
        this.ExecuteSql(sqlGenerator.GetAlterColumn(MediaContentFileUrlsUpgrader.mediaContentTable, MediaContentFileUrlsUpgrader.mcNumberOfChunks, SqlDbType.Int, true));
        this.ExecuteSql(sqlGenerator.GetAlterColumn(MediaContentFileUrlsUpgrader.mediaContentTable, MediaContentFileUrlsUpgrader.mcTotalSize, SqlDbType.BigInt, true));
        this.ExecuteSql(sqlGenerator.GetAlterColumn(MediaContentFileUrlsUpgrader.mediaContentTable, MediaContentFileUrlsUpgrader.mcChunkSize, SqlDbType.Int, true));
      }
      else
      {
        this.ExecuteSql(sqlGenerator.GetDropColumn(MediaContentFileUrlsUpgrader.mediaContentTable, MediaContentFileUrlsUpgrader.mcFileId));
        if (this.upgradedFromSchemaVersionNumber > 1650)
          this.ExecuteSql(sqlGenerator.GetDropColumn(MediaContentFileUrlsUpgrader.mediaContentTable, MediaContentFileUrlsUpgrader.mcFilePath));
        this.ExecuteSql(sqlGenerator.GetDropColumn(MediaContentFileUrlsUpgrader.mediaContentTable, MediaContentFileUrlsUpgrader.mcMimeType));
        this.ExecuteSql(sqlGenerator.GetDropColumn(MediaContentFileUrlsUpgrader.mediaContentTable, MediaContentFileUrlsUpgrader.mcExtension));
        this.ExecuteSql(sqlGenerator.GetDropColumn(MediaContentFileUrlsUpgrader.mediaContentTable, MediaContentFileUrlsUpgrader.mcNumberOfChunks));
        this.ExecuteSql(sqlGenerator.GetDropColumn(MediaContentFileUrlsUpgrader.mediaContentTable, MediaContentFileUrlsUpgrader.mcTotalSize));
        this.ExecuteSql(sqlGenerator.GetDropColumn(MediaContentFileUrlsUpgrader.mediaContentTable, MediaContentFileUrlsUpgrader.mcChunkSize));
        this.ExecuteSql(sqlGenerator.GetDropColumn(MediaContentFileUrlsUpgrader.mediaContentTable, MediaContentFileUrlsUpgrader.mcWidth));
        this.ExecuteSql(sqlGenerator.GetDropColumn(MediaContentFileUrlsUpgrader.mediaContentTable, MediaContentFileUrlsUpgrader.mcHeight));
        this.ExecuteSql(sqlGenerator.GetDropColumn(MediaContentFileUrlsUpgrader.mediaContentTable, MediaContentFileUrlsUpgrader.mcWidth2));
        this.ExecuteSql(sqlGenerator.GetDropColumn(MediaContentFileUrlsUpgrader.mediaContentTable, MediaContentFileUrlsUpgrader.mcHeight2));
      }
      this.context.SaveChanges();
    }

    private void LoadMediaFileLinks(SqlGenerator sqlGen)
    {
      List<Column> insertedColumns = new List<Column>()
      {
        MediaContentFileUrlsUpgrader.mflId,
        MediaContentFileUrlsUpgrader.mflWidth,
        MediaContentFileUrlsUpgrader.mflTotalSize,
        MediaContentFileUrlsUpgrader.mflNumberOfChunks,
        MediaContentFileUrlsUpgrader.mflContentId,
        MediaContentFileUrlsUpgrader.mflHeight,
        MediaContentFileUrlsUpgrader.mflFileId,
        MediaContentFileUrlsUpgrader.mflExtension,
        MediaContentFileUrlsUpgrader.mflDefaultUrl,
        MediaContentFileUrlsUpgrader.mflCulture,
        MediaContentFileUrlsUpgrader.mflChunkSize,
        MediaContentFileUrlsUpgrader.mflMimeType,
        MediaContentFileUrlsUpgrader.mflAppName
      };
      List<string> columnValues = new List<string>()
      {
        sqlGen.GetNewId(),
        MediaContentFileUrlsUpgrader.mcWidth.GetSql(sqlGen),
        MediaContentFileUrlsUpgrader.mcTotalSize.GetSql(sqlGen),
        MediaContentFileUrlsUpgrader.mcNumberOfChunks.GetSql(sqlGen),
        MediaContentFileUrlsUpgrader.mcContentId.GetSql(sqlGen),
        MediaContentFileUrlsUpgrader.mcHeight.GetSql(sqlGen),
        MediaContentFileUrlsUpgrader.mcFileId.GetSql(sqlGen),
        MediaContentFileUrlsUpgrader.mcExtension.GetSql(sqlGen),
        MediaContentFileUrlsUpgrader.udUrl.GetSql(sqlGen),
        MediaContentFileUrlsUpgrader.udCulture.GetSql(sqlGen),
        MediaContentFileUrlsUpgrader.mcChunkSize.GetSql(sqlGen),
        MediaContentFileUrlsUpgrader.mcMimeType.GetSql(sqlGen),
        MediaContentFileUrlsUpgrader.mcAppName.GetSql(sqlGen)
      };
      if (this.upgradedFromSchemaVersionNumber >= 1650)
      {
        insertedColumns.Add(MediaContentFileUrlsUpgrader.mflFilePath);
        columnValues.Add(MediaContentFileUrlsUpgrader.mcFilePath.GetSql(sqlGen));
      }
      this.ExecuteSql(this.context.Connection.DbType != DatabaseType.Oracle ? sqlGen.GetInsert(MediaContentFileUrlsUpgrader.mediaFileLinksTable, insertedColumns, MediaContentFileUrlsUpgrader.mediaContentTable, columnValues, new List<JoinTableClause>()
      {
        new JoinTableClause(MediaContentFileUrlsUpgrader.urlDataTable, MediaContentFileUrlsUpgrader.mcContentId, MediaContentFileUrlsUpgrader.udContentId)
      }, new Dictionary<Column, string>()
      {
        {
          MediaContentFileUrlsUpgrader.udIsDefault,
          " = 1 "
        }
      }) : this.GetLoadMediaFileLinksOracleScript());
    }

    [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Method's logic requires more than 65 lines.")]
    private string GetLoadMediaFileLinksOracleScript() => this.upgradedFromSchemaVersionNumber < 1650 ? "INSERT INTO \"sf_media_file_links\"\r\n                (\"sf_media_file_links\".\"id\",\r\n                 \"sf_media_file_links\".\"width\",\r\n                 \"sf_media_file_links\".\"total_size\",\r\n                 \"sf_media_file_links\".\"number_of_chunks\",\r\n                 \"sf_media_file_links\".\"content_id\",\r\n                 \"sf_media_file_links\".\"height\",\r\n                 \"sf_media_file_links\".\"file_id\",\r\n                 \"sf_media_file_links\".\"extension\",\r\n                 \"sf_media_file_links\".\"default_url\",\r\n                 \"sf_media_file_links\".\"culture\",\r\n                 \"sf_media_file_links\".\"chunk_size\",\r\n                 \"sf_media_file_links\".\"mime_type\",\r\n                 \"sf_media_file_links\".\"app_name\")\r\n                SELECT SYS_GUID(),\r\n                       \"sf_media_content\".\"width\",\r\n                       \"sf_media_content\".\"total_size\",\r\n                       \"sf_media_content\".\"number_of_chunks\",\r\n                       \"sf_media_content\".\"content_id\",\r\n                       \"sf_media_content\".\"height\",\r\n                       \"sf_media_content\".\"file_id\",\r\n                       \"sf_media_content\".\"extension\",\r\n                       \"sf_url_data\".\"url\",\r\n                       \"sf_url_data\".\"culture\",\r\n                       \"sf_media_content\".\"chunk_size\",\r\n                       \"sf_media_content\".\"mime_type\",\r\n                       \"sf_media_content\".\"app_name\" \r\n                FROM   \"sf_media_content\"\r\n                       INNER JOIN \"sf_url_data\"\r\n                               ON \"sf_media_content\".\"content_id\" = \"sf_url_data\".\"content_id\"\r\n                WHERE  \"sf_url_data\".\"is_default\" = 1" : "INSERT INTO \"sf_media_file_links\"\r\n                (\"sf_media_file_links\".\"id\",\r\n                 \"sf_media_file_links\".\"width\",\r\n                 \"sf_media_file_links\".\"total_size\",\r\n                 \"sf_media_file_links\".\"number_of_chunks\",\r\n                 \"sf_media_file_links\".\"content_id\",\r\n                 \"sf_media_file_links\".\"height\",\r\n                 \"sf_media_file_links\".\"file_path\",\r\n                 \"sf_media_file_links\".\"file_id\",\r\n                 \"sf_media_file_links\".\"extension\",\r\n                 \"sf_media_file_links\".\"default_url\",\r\n                 \"sf_media_file_links\".\"culture\",\r\n                 \"sf_media_file_links\".\"chunk_size\",\r\n                 \"sf_media_file_links\".\"mime_type\",\r\n                 \"sf_media_file_links\".\"app_name\")\r\n                SELECT SYS_GUID(),\r\n                       \"sf_media_content\".\"width\",\r\n                       \"sf_media_content\".\"total_size\",\r\n                       \"sf_media_content\".\"number_of_chunks\",\r\n                       \"sf_media_content\".\"content_id\",\r\n                       \"sf_media_content\".\"height\",\r\n                       \"sf_media_content\".\"file_path\",\r\n                       \"sf_media_content\".\"file_id\",\r\n                       \"sf_media_content\".\"extension\",\r\n                       \"sf_url_data\".\"url\",\r\n                       \"sf_url_data\".\"culture\",\r\n                       \"sf_media_content\".\"chunk_size\",\r\n                       \"sf_media_content\".\"mime_type\",\r\n                       \"sf_media_content\".\"app_name\" \r\n                FROM   \"sf_media_content\"\r\n                       INNER JOIN \"sf_url_data\"\r\n                               ON \"sf_media_content\".\"content_id\" = \"sf_url_data\".\"content_id\"\r\n                WHERE  \"sf_url_data\".\"is_default\" = 1";

    private void UpdateVideosWidthHeight(SqlGenerator sqlGen)
    {
      Dictionary<Column, Column> setColumnColumn = new Dictionary<Column, Column>();
      setColumnColumn.Add(MediaContentFileUrlsUpgrader.mflWidth, MediaContentFileUrlsUpgrader.mcWidth2);
      setColumnColumn.Add(MediaContentFileUrlsUpgrader.mflHeight, MediaContentFileUrlsUpgrader.mcHeight2);
      JoinTableClause joinClause = new JoinTableClause(MediaContentFileUrlsUpgrader.mediaFileLinksTable, MediaContentFileUrlsUpgrader.mcContentId, MediaContentFileUrlsUpgrader.mflContentId);
      this.ExecuteSql(this.context.Connection.DbType != DatabaseType.Oracle ? sqlGen.GetUpdate(MediaContentFileUrlsUpgrader.mediaFileLinksTable, setColumnColumn, MediaContentFileUrlsUpgrader.mediaContentTable, joinClause, new Dictionary<Column, string>()
      {
        {
          MediaContentFileUrlsUpgrader.mflWidth,
          " IS NULL "
        },
        {
          MediaContentFileUrlsUpgrader.mflHeight,
          " IS NULL "
        },
        {
          MediaContentFileUrlsUpgrader.mcWidth2,
          " IS NOT NULL "
        },
        {
          MediaContentFileUrlsUpgrader.mcHeight2,
          " IS NOT NULL "
        }
      }) : "UPDATE \"sf_media_file_links\" \"mfl\" SET \"mfl\".\"width\" = \r\n                (select \"mc\".\"width2\" from \"sf_media_content\" \"mc\" where \"mc\".\"content_id\" = \"mfl\".\"content_id\"\r\n                AND \"mfl\".\"width\" IS NULL AND \"mc\".\"width2\" IS NOT NULL), \"mfl\".\"height\" = \r\n                (select \"mc\".\"height2\" from \"sf_media_content\" \"mc\" where \"mc\".\"content_id\" = \"mfl\".\"content_id\"\r\n                AND \"mfl\".\"height\" IS NULL AND \"mc\".\"height2\" IS NOT NULL);");
    }

    private void RemoveInvariantDuplicates(SqlGenerator sqlGen)
    {
      if (this.context.Connection.DbType == DatabaseType.MySQL)
      {
        this.context.Delete((IEnumerable) this.context.GetAll<MediaFileLink>().Where<MediaFileLink>((Expression<Func<MediaFileLink, bool>>) (mfl => mfl.Culture == CultureInfo.InvariantCulture.LCID && mfl.MediaContent.MediaFileLinks.Count<MediaFileLink>() > 1)));
      }
      else
      {
        List<Column> columnList = new List<Column>();
        columnList.Add(MediaContentFileUrlsUpgrader.mflContentId);
        Dictionary<string, string> dictionary = new Dictionary<string, string>();
        dictionary.Add(string.Format(" COUNT({0}) ", (object) MediaContentFileUrlsUpgrader.mflId.GetSql(sqlGen)), " > 1");
        SqlGenerator sqlGenerator = sqlGen;
        Table mediaFileLinksTable = MediaContentFileUrlsUpgrader.mediaFileLinksTable;
        List<Column> columnNames = new List<Column>();
        columnNames.Add(MediaContentFileUrlsUpgrader.mflContentId);
        List<Column> groupByColumns = columnList;
        Dictionary<string, string> havingColumnOperationsChecks = dictionary;
        string select = sqlGenerator.GetSelect(mediaFileLinksTable, columnNames, groupByColumns: groupByColumns, havingColumnOperationsChecks: havingColumnOperationsChecks);
        Dictionary<Column, string> whereColumnValues = new Dictionary<Column, string>();
        whereColumnValues.Add(MediaContentFileUrlsUpgrader.mflCulture, string.Format(" = {0} ", (object) MediaContentFileUrlsUpgrader.invariantCulture));
        whereColumnValues.Add(MediaContentFileUrlsUpgrader.mflContentId, string.Format(" IN ({0})", (object) select));
        string commandText;
        if (this.context.Connection.DbType == DatabaseType.Oracle)
        {
          commandText = "DELETE FROM   \"sf_media_file_links\"\r\n                    WHERE  \"culture\" = 127 AND \"content_id\" IN \r\n                    (SELECT \"content_id\" FROM   \"sf_media_file_links\"\r\n                    GROUP  BY \"content_id\" HAVING Count(\"id\") > 1)";
          this.ExecuteSql("Update \"sf_media_file_links\" \"mfl\" SET \"id\" = (select LOWER(regexp_replace(\"id\"\r\n                    , '([A-F0-9]{8})([A-F0-9]{4})([A-F0-9]{4})([A-F0-9]{4})([A-F0-9]{12})'\r\n                    , '\\1-\\2-\\3-\\4-\\5'))\r\n                    from \"sf_media_file_links\" \"mfl2\" WHERE \"mfl\".\"id\" = \"mfl2\".\"id\")");
        }
        else
          commandText = sqlGen.GetDelete(MediaContentFileUrlsUpgrader.mediaFileLinksTable, whereColumnValues: whereColumnValues);
        this.ExecuteSql(commandText);
      }
    }

    private void ExecuteSql(string commandText)
    {
      int num1 = 1800;
      try
      {
        int num2 = PerformanceOptimization.ExecuteNonQueryCommand((OpenAccessContext) this.context, commandText, new int?(num1));
        Log.Write((object) string.Format("PASSED: {0}, Total Rows Affected: {1}", (object) commandText, (object) num2), ConfigurationPolicy.UpgradeTrace);
      }
      catch (Exception ex)
      {
        Log.Write((object) string.Format("FAILED: {0} - {1}. Timeout specified {2} seconds", (object) commandText, (object) ex.Message, (object) num1), ConfigurationPolicy.UpgradeTrace);
        throw;
      }
    }
  }
}
