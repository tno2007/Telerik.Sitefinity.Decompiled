// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.SqlGenerators.SqlGenerator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Data.SqlGenerators
{
  /// <summary>
  /// Contains methods for generating sql statements for a specific DbType.
  /// </summary>
  internal class SqlGenerator
  {
    /// <summary>
    /// Gets the new id function specific for each database type.
    /// </summary>
    /// <returns></returns>
    internal virtual string GetNewId() => " NEWID() ";

    internal virtual string GetTableName(string tableName) => tableName;

    internal virtual string GetColumnName(string columnName) => columnName;

    internal virtual string GetIndexName(string indexName) => indexName;

    internal virtual string GetConstraint(string constrName) => constrName;

    public static SqlGenerator Get(DatabaseType dbType)
    {
      if (dbType == DatabaseType.Oracle)
        return (SqlGenerator) new OracleSqlGenerator();
      return dbType == DatabaseType.MySQL ? (SqlGenerator) new MySqlGenerator() : new SqlGenerator();
    }

    /// <summary>Gets the alter column sql script.</summary>
    /// <param name="column">The column that will be altered.</param>
    /// <param name="table">The table that will be altered.</param>
    /// <param name="newType">The new db type.</param>
    /// <param name="allowDbNull">If the column will allow null values.</param>
    /// <param name="length">The length of the column if applicable.</param>
    /// <returns></returns>
    public virtual string GetAlterColumn(
      Table table,
      Column column,
      SqlDbType newType,
      bool allowDbNull,
      int? length = null)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendFormat("ALTER TABLE {0} ALTER COLUMN {1} ", (object) table.GetSql(this), (object) this.GetColumnName(column.ColumnName));
      stringBuilder.Append(Enum.GetName(typeof (SqlDbType), (object) newType).ToLowerInvariant());
      if (length.HasValue)
        stringBuilder.AppendFormat("({0}) ", (object) length.Value.ToString());
      if (!allowDbNull)
        stringBuilder.AppendFormat(" NOT ");
      stringBuilder.Append(" NULL ");
      return stringBuilder.ToString();
    }

    /// <summary>Gets the drop column.</summary>
    /// <param name="column">The column.</param>
    /// <param name="table">The table that will be altered.</param>
    /// <returns></returns>
    public virtual string GetDropColumn(Table table, Column column) => string.Format("ALTER TABLE {0} DROP COLUMN {1}", (object) table.GetSql(this), (object) this.GetColumnName(column.ColumnName));

    public virtual string GetDropColumn(string tableName, string columnName) => string.Format("ALTER TABLE {0} DROP COLUMN {1}", (object) this.GetTableName(tableName), (object) this.GetColumnName(columnName));

    public virtual string GetDropConstraint(string tableName, string constrName) => "ALTER TABLE {0} DROP CONSTRAINT {1}".Arrange((object) this.GetTableName(tableName), (object) this.GetIndexName(constrName));

    public virtual string GetDropIndex(string tableName, string indexName) => string.Format("DROP INDEX {0} ON {1}", (object) this.GetIndexName(indexName), (object) this.GetTableName(tableName));

    public virtual string GetDropTable(string tableName) => "DROP TABLE {0}".Arrange((object) this.GetTableName(tableName));

    public virtual string GetSelect(
      Table fromTable,
      List<Column> columnNames,
      List<JoinTableClause> joinClauses = null,
      OrderByClause orderBy = null,
      List<Column> groupByColumns = null,
      Dictionary<string, string> havingColumnOperationsChecks = null)
    {
      StringBuilder stringBuilder1 = new StringBuilder();
      StringBuilder stringBuilder2 = new StringBuilder();
      int num = columnNames.Count<Column>();
      foreach (Column columnName in columnNames)
      {
        stringBuilder2.AppendFormat("{0}", (object) columnName.GetSql(this));
        if (--num > 0)
          stringBuilder2.Append(", ");
      }
      stringBuilder1.AppendFormat("SELECT {0} FROM {1} ", (object) stringBuilder2.ToString(), (object) fromTable.GetSql(this));
      stringBuilder1.Append(this.GetJoinClausesSql(joinClauses));
      stringBuilder1.Append(this.GetGroupBy(groupByColumns));
      stringBuilder1.Append(this.GetHaving(havingColumnOperationsChecks));
      if (orderBy != null)
      {
        stringBuilder1.AppendLine();
        stringBuilder1.Append(orderBy.GetSql(this));
      }
      return stringBuilder1.ToString();
    }

    public virtual string GetUpdate(
      Table updateTable,
      Dictionary<Column, Column> setColumnColumn,
      Table fromTable,
      JoinTableClause joinClause = null,
      Dictionary<Column, string> whereColumnConditions = null)
    {
      Dictionary<Column, string> setColumnValues = new Dictionary<Column, string>();
      foreach (Column key in setColumnColumn.Keys)
        setColumnValues.Add(key, setColumnColumn[key].GetSql(this));
      List<JoinTableClause> joinClauses = new List<JoinTableClause>();
      if (joinClause != null)
        joinClauses.Add(joinClause);
      return this.GetUpdate(updateTable, setColumnValues, fromTable, joinClauses, whereColumnConditions);
    }

    public virtual string GetUpdate(
      Table updateTable,
      Dictionary<Column, string> setColumnValues,
      Table fromTable,
      List<JoinTableClause> joinClauses = null,
      Dictionary<Column, string> whereColumnConditions = null)
    {
      StringBuilder stringBuilder = new StringBuilder();
      string setColumnList = this.GetSetColumnList(setColumnValues);
      stringBuilder.AppendFormat("UPDATE {0} SET {1} ", (object) updateTable.GetSql(this), (object) setColumnList);
      if (fromTable != null)
      {
        stringBuilder.AppendFormat(" FROM {0} ", (object) fromTable.GetSql(this));
        stringBuilder.Append(this.GetJoinClausesSql(joinClauses));
      }
      stringBuilder.Append(this.GetWhereConditionSql(whereColumnConditions));
      return stringBuilder.ToString();
    }

    public virtual string GetInsert(
      Table insertTable,
      List<Column> insertedColumns,
      Table fromTable,
      List<string> columnValues,
      List<JoinTableClause> joinClauses = null,
      Dictionary<Column, string> whereColumnConditions = null)
    {
      int num1 = insertedColumns.Count<Column>();
      int num2 = columnValues.Count<string>();
      if (num1 != num2)
        throw new ArgumentException("Inserted columns should have corresponding values but their count is different");
      StringBuilder stringBuilder1 = new StringBuilder();
      StringBuilder stringBuilder2 = new StringBuilder();
      foreach (Column insertedColumn in insertedColumns)
      {
        stringBuilder2.AppendFormat(" {0} ", (object) insertedColumn.GetSql(this));
        if (--num1 > 0)
          stringBuilder2.Append(", ");
      }
      StringBuilder stringBuilder3 = new StringBuilder();
      foreach (string columnValue in columnValues)
      {
        stringBuilder3.Append(columnValue);
        if (--num2 > 0)
          stringBuilder3.Append(", ");
      }
      stringBuilder1.AppendFormat("INSERT INTO {0} ({1}) SELECT {2} FROM {3}", (object) insertTable.GetSql(this), (object) stringBuilder2, (object) stringBuilder3, (object) fromTable.GetSql(this));
      stringBuilder1.Append(this.GetJoinClausesSql(joinClauses));
      stringBuilder1.Append(this.GetWhereConditionSql(whereColumnConditions));
      return stringBuilder1.ToString();
    }

    /// <summary>Gets the delete sql statement.</summary>
    /// <param name="fromTable">From table.</param>
    /// <param name="joinClauses">The join clauses.</param>
    /// <param name="whereColumnValues">The where column values.</param>
    /// <returns></returns>
    public virtual string GetDelete(
      Table fromTable,
      List<JoinTableClause> joinClauses = null,
      Dictionary<Column, string> whereColumnValues = null)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendFormat("DELETE {0} FROM {0}", (object) fromTable.GetSql(this));
      stringBuilder.Append(this.GetJoinClausesSql(joinClauses));
      stringBuilder.Append(this.GetWhereConditionSql(whereColumnValues));
      return stringBuilder.ToString();
    }

    /// <summary>
    /// Gets the where condition SQL. If there are conditions returns " WHERE {conditions}. Otherwise returns an empty string."
    /// </summary>
    /// <param name="whereColumnConditions">The where column conditions.</param>
    /// <param name="sqlOperator">The SQL operator.</param>
    /// <returns></returns>
    protected virtual string GetWhereConditionSql(
      Dictionary<Column, string> whereColumnConditions,
      string sqlOperator = null)
    {
      StringBuilder stringBuilder1 = new StringBuilder();
      if (whereColumnConditions != null)
      {
        if (string.IsNullOrWhiteSpace(sqlOperator))
          sqlOperator = " AND ";
        int num = whereColumnConditions.Count<KeyValuePair<Column, string>>();
        StringBuilder stringBuilder2 = new StringBuilder();
        foreach (Column key in whereColumnConditions.Keys)
        {
          stringBuilder2.AppendFormat(" {0} {1} ", (object) key.GetSql(this), (object) whereColumnConditions[key]);
          if (--num > 0)
            stringBuilder2.AppendFormat(" {0} ", (object) sqlOperator);
        }
        stringBuilder1.AppendFormat(" WHERE {0}", (object) stringBuilder2);
      }
      return stringBuilder1.ToString();
    }

    /// <summary>Gets the set column list string.</summary>
    /// <param name="setColumnValues">The set column values.</param>
    /// <returns></returns>
    protected string GetSetColumnList(Dictionary<Column, string> setColumnValues)
    {
      StringBuilder stringBuilder = new StringBuilder();
      int num = setColumnValues.Count<KeyValuePair<Column, string>>();
      foreach (Column key in setColumnValues.Keys)
      {
        stringBuilder.AppendFormat("{0} = {1}", (object) key.GetSql(this), (object) setColumnValues[key]);
        if (--num > 0)
          stringBuilder.Append(", ");
      }
      return stringBuilder.ToString();
    }

    private string GetJoinClausesSql(List<JoinTableClause> joinClauses)
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (joinClauses != null && joinClauses.Count<JoinTableClause>() > 0)
      {
        stringBuilder.AppendLine();
        int num = joinClauses.Count<JoinTableClause>();
        foreach (JoinTableClause joinClause in joinClauses)
        {
          stringBuilder.Append(joinClause.GetSql(this));
          if (--num > 0)
            stringBuilder.AppendLine();
        }
      }
      return stringBuilder.ToString();
    }

    private string GetGroupBy(List<Column> groupByColumns)
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (groupByColumns != null && groupByColumns.Count<Column>() > 0)
      {
        stringBuilder.AppendLine();
        stringBuilder.Append(" GROUP BY ");
        int num = groupByColumns.Count<Column>();
        foreach (Column groupByColumn in groupByColumns)
        {
          stringBuilder.Append(groupByColumn.GetSql(this));
          if (--num > 0)
            stringBuilder.Append(", ");
        }
      }
      return stringBuilder.ToString();
    }

    private string GetHaving(
      Dictionary<string, string> havingColumnOperationsChecks)
    {
      StringBuilder stringBuilder = new StringBuilder();
      if (havingColumnOperationsChecks != null && havingColumnOperationsChecks.Count<KeyValuePair<string, string>>() > 0)
      {
        stringBuilder.AppendLine();
        stringBuilder.Append(" HAVING ");
        int num = havingColumnOperationsChecks.Count<KeyValuePair<string, string>>();
        foreach (KeyValuePair<string, string> columnOperationsCheck in havingColumnOperationsChecks)
        {
          stringBuilder.Append(string.Format("{0} {1}", (object) columnOperationsCheck.Key, (object) columnOperationsCheck.Value));
          if (--num > 0)
            stringBuilder.Append(" AND ");
        }
      }
      return stringBuilder.ToString();
    }
  }
}
