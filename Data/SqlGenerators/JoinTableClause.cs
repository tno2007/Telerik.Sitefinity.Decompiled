// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.SqlGenerators.JoinTableClause
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telerik.Sitefinity.Data.SqlGenerators
{
  internal class JoinTableClause : ISqlItem
  {
    public JoinTableClause(
      Table joinTable,
      Column leftTableColumn,
      Column rightTableColumn,
      JoinTableClause.JoinType type = JoinTableClause.JoinType.Inner)
    {
      this.JoinTable = joinTable;
      this.LeftTableRightTableColumns = new Dictionary<Column, Column>();
      this.LeftTableRightTableColumns.Add(leftTableColumn, rightTableColumn);
      this.Type = type;
    }

    public JoinTableClause(
      Table joinTable,
      Dictionary<Column, Column> leftTableRightTableColumns,
      JoinTableClause.JoinType type = JoinTableClause.JoinType.Inner)
    {
      this.JoinTable = joinTable;
      this.LeftTableRightTableColumns = leftTableRightTableColumns;
      this.Type = type;
    }

    public Table JoinTable { get; private set; }

    public Dictionary<Column, Column> LeftTableRightTableColumns { get; private set; }

    public JoinTableClause.JoinType Type { get; private set; }

    public string GetSql(SqlGenerator generator)
    {
      string upper = Enum.GetName(typeof (JoinTableClause.JoinType), (object) this.Type).ToUpper();
      StringBuilder stringBuilder = new StringBuilder();
      int num = this.LeftTableRightTableColumns.Count<KeyValuePair<Column, Column>>();
      foreach (Column key in this.LeftTableRightTableColumns.Keys)
      {
        stringBuilder.AppendFormat("{0} = {1}", (object) key.GetSql(generator), (object) this.LeftTableRightTableColumns[key].GetSql(generator));
        if (--num > 0)
          stringBuilder.Append(" AND ");
      }
      return string.Format("{0} JOIN {1} ON {2}", (object) upper, (object) this.JoinTable.GetSql(generator), (object) stringBuilder.ToString());
    }

    public enum JoinType
    {
      Inner,
      Left,
      Right,
      Full,
    }
  }
}
