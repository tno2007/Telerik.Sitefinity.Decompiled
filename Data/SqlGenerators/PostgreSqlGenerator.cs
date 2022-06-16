// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.SqlGenerators.PostgreSqlGenerator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Data.SqlGenerators
{
  internal class PostgreSqlGenerator : SqlGenerator
  {
    internal override string GetColumnName(string columnName) => string.Format("\"{0}\"", (object) columnName);

    internal override string GetTableName(string tableName) => string.Format("\"{0}\"", (object) tableName);

    internal override string GetIndexName(string indexName) => string.Format("\"{0}\"", (object) indexName);

    internal override string GetConstraint(string constrName) => string.Format("\"{0}\"", (object) constrName);
  }
}
