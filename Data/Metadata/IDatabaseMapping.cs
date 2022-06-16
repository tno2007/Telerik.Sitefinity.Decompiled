// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Metadata.IDatabaseMapping
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Data.Metadata
{
  /// <summary>
  /// Describes the mapping between a meta-field and a database column.
  /// </summary>
  public interface IDatabaseMapping
  {
    /// <summary>The CLR type of the meta field.</summary>
    string ClrType { get; }

    /// <summary>The generic OpenAccess database type.</summary>
    string DbType { get; }

    /// <summary>The concrete Microsoft SQL Server data type.</summary>
    string DbSqlType { get; }

    /// <summary>The length of the DB type, when relevant.</summary>
    string DbLength { get; }

    /// <summary>The precision of the numeric DB type, when relevant.</summary>
    string DbPrecision { get; }

    /// <summary>The scale of the numeric DB type, when relevant.</summary>
    string DbScale { get; }

    /// <summary>Indicates whether the database type is nullable.</summary>
    bool Nullable { get; }

    /// <summary>Indicates whether the DB colum should be indexed.</summary>
    bool Indexed { get; }

    /// <summary>
    /// The name of the DB column. May be <c>null</c>.
    /// </summary>
    string ColumnName { get; }
  }
}
