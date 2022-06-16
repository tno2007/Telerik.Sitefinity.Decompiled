// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Upgrades.CommonUpgrader
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Telerik.OpenAccess;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Data.SqlGenerators;
using Telerik.Sitefinity.Descriptors;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Upgrades
{
  /// <summary>Provides common upgrade functions</summary>
  internal class CommonUpgrader
  {
    private readonly SitefinityMetadataSourceBase metadataSource;

    public CommonUpgrader()
      : this((SitefinityMetadataSourceBase) null)
    {
    }

    public CommonUpgrader(SitefinityMetadataSourceBase metadataSource) => this.metadataSource = metadataSource;

    public void DeleteMultilingualField(
      UpgradingContext context,
      string tableName,
      string columnName,
      bool skipInvariantCulture,
      bool dropSplitTable)
    {
      DatabaseType dbType = this.GetDbType((OpenAccessContext) context);
      if (dbType != DatabaseType.MsSql)
        return;
      CommonUpgrader.CultureSettings cultureSettings = this.GetCultureSettings(skipInvariantCulture);
      SqlGenerator sqlGen = SqlGenerator.Get(dbType);
      foreach (CultureInfo splitTableCulture in (IEnumerable<CultureInfo>) cultureSettings.SplitTableCultures)
      {
        string tableName1 = sqlGen.GetTableName(LstringPropertyDescriptor.GetFieldNameForCulture(tableName, splitTableCulture));
        if (dropSplitTable)
        {
          context.ExecuteNonQuery("DROP TABLE {0}".Arrange((object) tableName1));
        }
        else
        {
          string columnName1 = sqlGen.GetColumnName(LstringPropertyDescriptor.GetFieldNameForCulture(this.ToUpperFirstLetter(columnName), splitTableCulture));
          context.ExecuteNonQuery("ALTER TABLE {0} DROP COLUMN {1}".Arrange((object) tableName1, (object) columnName1));
        }
      }
      if (cultureSettings.InTableCultures.Count <= 0)
        return;
      IEnumerable<string> values = cultureSettings.InTableCultures.Select<CultureInfo, string>((Func<CultureInfo, string>) (x => sqlGen.GetColumnName(LstringPropertyDescriptor.GetFieldNameForCulture(columnName, x))));
      string commandText = "ALTER TABLE {0} DROP COLUMN {1}".Arrange((object) tableName, (object) string.Join(",", values));
      context.ExecuteNonQuery(commandText);
    }

    private CommonUpgrader.CultureSettings GetCultureSettings(
      bool skipInvariantCulture)
    {
      List<CultureInfo> cultureInfoList = new List<CultureInfo>();
      List<CultureInfo> splitTableCultures = new List<CultureInfo>();
      List<CultureInfo> list = ((IEnumerable<CultureInfo>) MetadataSourceAggregator.GetConfiguredCultures()).ToList<CultureInfo>();
      IDatabaseMappingOptions databaseMappingOptions = this.metadataSource != null ? this.metadataSource.MappingOptions : this.GetDefaultMappingOptions();
      if (databaseMappingOptions.UseMultilingualSplitTables)
      {
        IEnumerable<CultureInfo> ignoredCultures = databaseMappingOptions.SplitTablesIgnoredCultures.Select<string, CultureInfo>((Func<string, CultureInfo>) (x => CultureInfo.GetCultureInfo(x)));
        splitTableCultures = list.Where<CultureInfo>((Func<CultureInfo, bool>) (x => !ignoredCultures.Contains<CultureInfo>(x))).ToList<CultureInfo>();
        list = list.Where<CultureInfo>((Func<CultureInfo, bool>) (x => ignoredCultures.Contains<CultureInfo>(x))).ToList<CultureInfo>();
      }
      if (!skipInvariantCulture)
        list.Add(CultureInfo.InvariantCulture);
      return new CommonUpgrader.CultureSettings((IEnumerable<CultureInfo>) list, (IEnumerable<CultureInfo>) splitTableCultures);
    }

    private string ToUpperFirstLetter(string source)
    {
      if (string.IsNullOrEmpty(source))
        return string.Empty;
      char[] charArray = source.ToCharArray();
      charArray[0] = char.ToUpper(charArray[0]);
      return new string(charArray);
    }

    private DatabaseType GetDbType(OpenAccessContext context)
    {
      switch (context)
      {
        case SitefinityOAContext _:
          return (context as SitefinityOAContext).OpenAccessConnection.DbType;
        case UpgradingContext _:
          return (context as UpgradingContext).Connection.DbType;
        default:
          return DatabaseType.Unspecified;
      }
    }

    private IDatabaseMappingOptions GetDefaultMappingOptions()
    {
      DatabaseMappingOptions defaultMappingOptions = new DatabaseMappingOptions();
      defaultMappingOptions.LoadDefaults();
      return (IDatabaseMappingOptions) defaultMappingOptions;
    }

    private class CultureSettings
    {
      public CultureSettings(
        IEnumerable<CultureInfo> inTableCultures,
        IEnumerable<CultureInfo> splitTableCultures)
      {
        this.InTableCultures = (IList<CultureInfo>) inTableCultures.ToList<CultureInfo>();
        this.SplitTableCultures = (IList<CultureInfo>) splitTableCultures.ToList<CultureInfo>();
      }

      internal IList<CultureInfo> InTableCultures { get; set; }

      internal IList<CultureInfo> SplitTableCultures { get; set; }
    }
  }
}
