// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.SqlGenerators.OracleSqlGenerator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Telerik.Sitefinity.Data.SqlGenerators
{
  internal class OracleSqlGenerator : SqlGenerator
  {
    internal override string GetColumnName(string columnName) => string.Format("\"{0}\"", (object) columnName);

    internal override string GetTableName(string tableName) => string.Format("\"{0}\"", (object) tableName);

    internal override string GetIndexName(string indexName) => string.Format("\"{0}\"", (object) indexName);

    internal override string GetConstraint(string constrName) => string.Format("\"{0}\"", (object) constrName);

    internal override string GetNewId() => " SYS_GUID() ";

    public override string GetUpdate(
      Table updateTable,
      Dictionary<Column, string> setColumnValues,
      Table fromTable,
      List<JoinTableClause> joinClauses = null,
      Dictionary<Column, string> whereColumnConditions = null)
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (fromTable != null)
        throw new NotImplementedException("Update statement with join operation is not suppoerted");
      stringBuilder.Append(base.GetUpdate(updateTable, setColumnValues, fromTable, joinClauses, whereColumnConditions));
      return stringBuilder.ToString();
    }

    public override string GetAlterColumn(
      Table table,
      Column column,
      SqlDbType newType,
      bool allowDbNull,
      int? length = null)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendFormat("ALTER TABLE {0} MODIFY COLUMN {1} ", (object) table.GetSql((SqlGenerator) this), (object) this.GetColumnName(column.ColumnName));
      if (!allowDbNull)
        stringBuilder.AppendFormat(" NOT ");
      stringBuilder.Append(" NULL ");
      return stringBuilder.ToString();
    }

    public override string GetDelete(
      Table fromTable,
      List<JoinTableClause> joinClauses = null,
      Dictionary<Column, string> whereColumnValues = null)
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (joinClauses != null)
        throw new NotImplementedException("Delete statement with join operation is not supported");
      stringBuilder.Append(base.GetDelete(fromTable, joinClauses, whereColumnValues));
      return stringBuilder.ToString();
    }
  }
}
