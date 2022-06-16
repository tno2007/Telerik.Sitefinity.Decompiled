// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.OA.MySqlDatabaseMappingContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Data.OA
{
  public class MySqlDatabaseMappingContext : DefaultDatabaseMappingContext
  {
    private const int maxVarcharLength = 20000;

    /// <inheritdoc />
    public override DatabaseType DatabaseType => DatabaseType.MySQL;

    /// <inheritdoc />
    public override DatabaseColumnMapping GetTextMapping(
      bool isLong,
      bool isUnicode,
      int? length)
    {
      if (length.HasValue && length.Value > 20000)
        length = new int?(20000);
      string str = !isLong ? (isUnicode ? "NVARCHAR" : "VARCHAR") : (isUnicode ? "LONGTEXT CHARACTER SET utf8 COLLATE utf8_general_ci" : "LONGTEXT");
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
      string str = isLong ? "LONGBLOB" : "BLOB";
      DatabaseColumnMapping binaryMapping = new DatabaseColumnMapping()
      {
        Type = str
      };
      if (!isLong && length.HasValue)
        binaryMapping.Length = length;
      return binaryMapping;
    }

    /// <inheritdoc />
    public override DatabaseColumnMapping GetGuidMapping() => new DatabaseColumnMapping()
    {
      Type = "VARCHAR",
      Length = new int?(40)
    };
  }
}
