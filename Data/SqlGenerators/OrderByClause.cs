// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.SqlGenerators.OrderByClause
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Telerik.Sitefinity.Data.SqlGenerators
{
  internal class OrderByClause : ISqlItem
  {
    public OrderByClause(Column column, bool desc = false)
    {
      this.OrderByColumnDescending = new Dictionary<Column, bool>();
      this.OrderByColumnDescending.Add(column, desc);
    }

    public OrderByClause(Dictionary<Column, bool> orderByColumnDesc) => this.OrderByColumnDescending = orderByColumnDesc;

    public Dictionary<Column, bool> OrderByColumnDescending { get; private set; }

    public string GetSql(SqlGenerator generator)
    {
      StringBuilder stringBuilder = new StringBuilder();
      int num = this.OrderByColumnDescending.Count<KeyValuePair<Column, bool>>();
      foreach (Column key in this.OrderByColumnDescending.Keys)
      {
        stringBuilder.Append(key.GetSql(generator));
        if (this.OrderByColumnDescending[key])
          stringBuilder.AppendFormat(" {0}", (object) "DESC");
        if (--num > 0)
          stringBuilder.Append(", ");
      }
      return string.Format("ORDER BY {0}", (object) stringBuilder.ToString());
    }
  }
}
