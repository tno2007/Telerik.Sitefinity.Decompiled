// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.OA.SqlCeDatabaseMappingContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Data.OA
{
  public class SqlCeDatabaseMappingContext : MsSqlDatabaseMappingContext
  {
    private const int maxLength = 510;

    /// <inheritdoc />
    public override DatabaseType DatabaseType => DatabaseType.SqlCE;

    /// <inheritdoc />
    public override DatabaseColumnMapping GetTextMapping(
      bool isLong,
      bool isUnicode,
      int? length)
    {
      if (length.HasValue && length.Value > 510)
        isLong = true;
      string str = !isLong ? (isUnicode ? "NVARCHAR" : "VARCHAR") : "NTEXT";
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
      throw new NotImplementedException();
    }
  }
}
