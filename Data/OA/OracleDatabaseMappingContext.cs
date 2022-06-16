// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.OA.OracleDatabaseMappingContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Text;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Data.OA
{
  public class OracleDatabaseMappingContext : DefaultDatabaseMappingContext
  {
    /// <inheritdoc />
    public override DatabaseType DatabaseType => DatabaseType.Oracle;

    /// <inheritdoc />
    public override DatabaseColumnMapping GetTextMapping(
      bool isLong,
      bool isUnicode,
      int? length)
    {
      string str = isLong ? "CLOB" : "VARCHAR2";
      if (isUnicode)
        str = "N" + str;
      int? nullable = length;
      int num = 0;
      if (nullable.GetValueOrDefault() > num & nullable.HasValue && length.Value > 2000)
        length = new int?(2000);
      DatabaseColumnMapping textMapping = new DatabaseColumnMapping()
      {
        Type = str
      };
      if (!isLong && length.HasValue)
        textMapping.Length = length;
      return textMapping;
    }

    /// <inheritdoc />
    public override DatabaseColumnMapping GetBinaryMapping(
      bool isLong,
      int? length)
    {
      string str = isLong ? "BLOB" : "RAW";
      DatabaseColumnMapping binaryMapping = new DatabaseColumnMapping()
      {
        Type = str
      };
      if (!isLong && length.HasValue)
        binaryMapping.Length = length;
      return binaryMapping;
    }

    /// <inheritdoc />
    public override DatabaseColumnMapping GetBooleanMapping(bool useTinyInt) => new DatabaseColumnMapping()
    {
      Type = "NUMBER",
      Precision = new int?(1)
    };

    /// <inheritdoc />
    public override DatabaseColumnMapping GetIntegerMapping(int? precision = null) => new DatabaseColumnMapping()
    {
      Type = "NUMBER",
      Precision = precision
    };

    /// <inheritdoc />
    public override DatabaseColumnMapping GetFloatMapping(
      int? precision = null,
      int? scale = null)
    {
      return new DatabaseColumnMapping()
      {
        Type = "NUMBER",
        Precision = precision,
        Scale = scale
      };
    }

    /// <inheritdoc />
    public override DatabaseColumnMapping GetDecimalMapping(
      int? precision = null,
      int? scale = null)
    {
      return new DatabaseColumnMapping()
      {
        Type = "NUMBER",
        Precision = precision,
        Scale = scale
      };
    }

    /// <inheritdoc />
    public override DatabaseColumnMapping GetMoneyMapping(
      int? precision = null,
      int? scale = null)
    {
      return new DatabaseColumnMapping()
      {
        Type = "NUMBER",
        Precision = precision,
        Scale = scale
      };
    }

    /// <inheritdoc />
    public override DatabaseColumnMapping GetDateTimeMapping() => new DatabaseColumnMapping()
    {
      Type = "TIMESTAMP"
    };

    /// <inheritdoc />
    public override DatabaseColumnMapping GetGuidMapping() => new DatabaseColumnMapping()
    {
      Type = "VARCHAR2",
      Length = new int?(40)
    };

    /// <inheritdoc />
    public override string GetSupportedTableName(string tableName) => tableName == "sf_approval_tracking_record_map" ? "sf_apprvl_trckng_rcrd_mp" : (tableName == "sf_publishing_throttle_settings" ? "sf_pblshng_thrttl_sttngs" : (tableName == "sf_permissions_inheritance_map" ? "sf_prmssns_inhrtnc_mp" : (tableName == "sf_stfnty_wrkflow_instance_record" ? "sf_stfnty_wrkflow_instnc_rcrd" : (tableName == "sf_sitefinity_activity_state_record" ? "sf_stfnt_actvt_stat_rcrd" : (tableName == "sf_page_node_translation_siblings" ? "sf_pg_nd_trsln_sblngs" : tableName)))));

    /// <inheritdoc />
    public override string GetColumnTypeMigrationScript(
      string tableName,
      string columnName,
      string newType,
      string oldType = null)
    {
      if (string.IsNullOrWhiteSpace(tableName) || string.IsNullOrWhiteSpace(columnName) || string.IsNullOrWhiteSpace(newType))
        throw new ArgumentException("tableName, columnName, and newType are required parameters");
      string str1 = this.GetMapping(newType, (Type) null, (DatabaseColumnMapping) null).ToString();
      string supportedTableName = this.GetSupportedTableName(tableName);
      StringBuilder stringBuilder = new StringBuilder();
      string str2 = "TEMP1";
      stringBuilder.Append("declare need_change number;");
      stringBuilder.Append("begin");
      stringBuilder.AppendFormat(" select count(*) into need_change from user_tab_cols where table_name='{0}' and column_name='{1}' and data_type <> '{2}'; ", (object) supportedTableName, (object) columnName, (object) str1);
      stringBuilder.Append(" if need_change > 0 then ");
      stringBuilder.AppendFormat(" execute immediate 'ALTER TABLE \"{0}\" ADD (\"{1}\" {2})'; ", (object) supportedTableName, (object) str2, (object) str1);
      stringBuilder.AppendFormat(" execute immediate 'UPDATE \"{0}\" SET \"{1}\" = \"{2}\"'; ", (object) supportedTableName, (object) str2, (object) columnName);
      stringBuilder.AppendFormat(" execute immediate 'ALTER TABLE \"{0}\" DROP COLUMN \"{1}\"'; ", (object) supportedTableName, (object) columnName);
      stringBuilder.AppendFormat(" execute immediate 'ALTER TABLE \"{0}\" ADD (\"{1}\" {2})'; ", (object) supportedTableName, (object) columnName, (object) str1);
      stringBuilder.AppendFormat(" execute immediate 'UPDATE \"{0}\" SET \"{1}\" = \"{2}\"'; ", (object) supportedTableName, (object) columnName, (object) str2);
      stringBuilder.AppendFormat(" execute immediate 'ALTER TABLE \"{0}\" DROP COLUMN \"{1}\"'; ", (object) supportedTableName, (object) str2);
      stringBuilder.Append("end if;");
      stringBuilder.Append("end;");
      return stringBuilder.ToString();
    }
  }
}
