// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.SqlGenerators.Column
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Data.SqlGenerators
{
  internal class Column : ISqlItem
  {
    public Column(string columnName)
      : this(columnName, (Table) null)
    {
    }

    public Column(string columnName, Table table)
    {
      this.ColumnName = columnName;
      this.Table = table;
    }

    public string ColumnName { get; private set; }

    public Table Table { get; private set; }

    public string GetSql(SqlGenerator generator)
    {
      if (this.Table != null)
      {
        string sql = this.Table.GetSql(generator);
        if (!string.IsNullOrWhiteSpace(sql))
          return string.Format("{0}.{1}", (object) sql, (object) generator.GetColumnName(this.ColumnName));
      }
      return generator.GetColumnName(this.ColumnName);
    }
  }
}
