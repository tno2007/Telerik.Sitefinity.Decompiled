// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.SqlGenerators.Table
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Data.SqlGenerators
{
  internal class Table : ISqlItem
  {
    public Table(string tableName) => this.TableName = tableName != null ? tableName : throw new ArgumentException("tableName must not be null");

    public string TableName { get; private set; }

    public string GetSql(SqlGenerator generator) => !string.IsNullOrWhiteSpace(this.TableName) ? generator.GetTableName(this.TableName) : string.Empty;

    public override bool Equals(object obj)
    {
      if (obj == this)
        return true;
      return obj is Table table && table != null && table.TableName != null && table.TableName.Equals(this.TableName);
    }

    public override int GetHashCode() => this.TableName.GetHashCode();
  }
}
