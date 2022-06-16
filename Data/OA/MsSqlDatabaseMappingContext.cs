// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.OA.MsSqlDatabaseMappingContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Data.OA
{
  public class MsSqlDatabaseMappingContext : DefaultDatabaseMappingContext
  {
    /// <inheritdoc />
    public override DatabaseType DatabaseType => DatabaseType.MsSql;

    /// <inheritdoc />
    public override DatabaseColumnMapping GetTextMapping(
      bool isLong,
      bool isUnicode,
      int? length)
    {
      if (length.HasValue && (isUnicode && length.Value > 4000 || !isUnicode && length.Value > 8000))
        isLong = true;
      return base.GetTextMapping(isLong, isUnicode, length);
    }

    /// <inheritdoc />
    public new virtual DatabaseColumnMapping GetMoneyMapping(
      int? precision = null,
      int? scale = null)
    {
      return new DatabaseColumnMapping()
      {
        Type = "MONEY",
        Precision = precision,
        Scale = scale
      };
    }
  }
}
