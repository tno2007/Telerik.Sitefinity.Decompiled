// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.OA.PostgreSqlDatabaseMappingContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Diagnostics.CodeAnalysis;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Data.OA
{
  /// <summary>Database mapping context for PostgreSql</summary>
  [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "PostgreSql is the correct spelling.")]
  public class PostgreSqlDatabaseMappingContext : DefaultDatabaseMappingContext
  {
    private const int MaxVarcharLength = 2000;

    /// <inheritdoc />
    public override DatabaseType DatabaseType => DatabaseType.PostgreSql;

    /// <inheritdoc />
    public override DatabaseColumnMapping GetTextMapping(
      bool isLong,
      bool isUnicode,
      int? length)
    {
      string str = isLong ? "text" : "varchar";
      if (length.HasValue && length.Value > 2000)
        length = new int?(2000);
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
      DatabaseColumnMapping binaryMapping = new DatabaseColumnMapping()
      {
        Type = "bytea"
      };
      if (!isLong && length.HasValue)
        binaryMapping.Length = length;
      return binaryMapping;
    }

    /// <inheritdoc />
    public new virtual DatabaseColumnMapping GetMoneyMapping(
      int? precision = null,
      int? scale = null)
    {
      return new DatabaseColumnMapping() { Type = "MONEY" };
    }

    /// <inheritdoc />
    public override DatabaseColumnMapping GetDateTimeMapping() => new DatabaseColumnMapping()
    {
      Type = "TIMESTAMP"
    };

    /// <inheritdoc />
    public override DatabaseColumnMapping GetGuidMapping() => new DatabaseColumnMapping()
    {
      Type = "uuid"
    };
  }
}
