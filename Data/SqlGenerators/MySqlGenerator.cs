// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.SqlGenerators.MySqlGenerator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telerik.Sitefinity.Data.SqlGenerators
{
  internal class MySqlGenerator : SqlGenerator
  {
    internal override string GetNewId() => " uuid() ";

    public override string GetDropConstraint(string tableName, string constrName) => "ALTER TABLE {0} DROP FOREIGN KEY {1};".Arrange((object) this.GetTableName(tableName), (object) this.GetIndexName(constrName));

    /// <summary>Gets the update statements for the given conditions.</summary>
    /// <param name="updateTable">The update table.</param>
    /// <param name="setColumnValues">The set column values.</param>
    /// <param name="fromTable">From table.</param>
    /// <param name="joinClauses">The join clauses.</param>
    /// <param name="whereColumnConditions">The where column conditions.</param>
    /// <returns></returns>
    public override string GetUpdate(
      Table updateTable,
      Dictionary<Column, string> setColumnValues,
      Table fromTable,
      List<JoinTableClause> joinClauses = null,
      Dictionary<Column, string> whereColumnConditions = null)
    {
      StringBuilder updateTableReferences = this.GetUpdateTableReferences(updateTable, fromTable, joinClauses);
      string setColumnList = this.GetSetColumnList(setColumnValues);
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendFormat("UPDATE {0} SET {1}", (object) updateTableReferences.ToString(), (object) setColumnList);
      stringBuilder.Append(this.GetWhereClause(joinClauses, whereColumnConditions));
      return stringBuilder.ToString();
    }

    /// <summary>
    /// Gets the update table references from the given table and all join tables.
    /// </summary>
    /// <param name="updateTable">The update table.</param>
    /// <param name="fromTable">From table.</param>
    /// <param name="joinClauses">The join clauses.</param>
    /// <returns></returns>
    private StringBuilder GetUpdateTableReferences(
      Table updateTable,
      Table fromTable,
      List<JoinTableClause> joinClauses)
    {
      List<Table> source = new List<Table>();
      source.Add(updateTable);
      if (!source.Contains(fromTable))
        source.Add(fromTable);
      foreach (Table table in joinClauses.Select<JoinTableClause, Table>((Func<JoinTableClause, Table>) (j => j.JoinTable)))
      {
        if (!source.Contains(table))
          source.Add(table);
      }
      StringBuilder updateTableReferences = new StringBuilder();
      int num = source.Count<Table>();
      foreach (Table table in source)
      {
        updateTableReferences.Append(table.GetSql((SqlGenerator) this));
        if (--num > 0)
          updateTableReferences.Append(", ");
      }
      return updateTableReferences;
    }

    /// <summary>
    /// Gets the where clause from all join clauses conditions and where column conditions.
    /// <para>If there are conditions returns " WHERE conditions". Otherwise returns an empty string.</para>
    /// </summary>
    /// <param name="joinClauses">The join clauses.</param>
    /// <param name="whereColumnConditions">The where column conditions.</param>
    /// <returns></returns>
    private string GetWhereClause(
      List<JoinTableClause> joinClauses,
      Dictionary<Column, string> whereColumnConditions)
    {
      Dictionary<Column, string> dictionary = new Dictionary<Column, string>();
      if (joinClauses != null && joinClauses.Count<JoinTableClause>() > 0)
      {
        foreach (JoinTableClause joinClause in joinClauses)
        {
          foreach (KeyValuePair<Column, Column> rightTableColumn in joinClause.LeftTableRightTableColumns)
            dictionary.Add(rightTableColumn.Key, rightTableColumn.Value.GetSql((SqlGenerator) this));
        }
      }
      if (whereColumnConditions != null)
      {
        foreach (KeyValuePair<Column, string> whereColumnCondition in whereColumnConditions)
          dictionary.Add(whereColumnCondition.Key, whereColumnCondition.Value);
      }
      return this.GetWhereConditionSql(whereColumnConditions);
    }
  }
}
