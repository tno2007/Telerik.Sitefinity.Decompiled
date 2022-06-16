// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Restriction.ColumnRestrictionStrategy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts;

namespace Telerik.Sitefinity.Restriction
{
  /// <summary>Defines a data column restriction strategy.</summary>
  internal class ColumnRestrictionStrategy : IColumnRestrictionStrategy, IRestrictionStrategy
  {
    private static readonly Dictionary<string, RestrictionLevel> RestrictedColumnsByName = new Dictionary<string, RestrictionLevel>();

    /// <summary>
    /// Determines whether the specified data column is restricted.
    /// </summary>
    /// <param name="item">The data column.</param>
    /// <returns>Whether item is restricted.</returns>
    /// <exception cref="T:System.ArgumentException">item is not of types DataColumnElement.</exception>
    public bool IsRestricted(object item)
    {
      string empty = string.Empty;
      string key = item is IColumnDefinition ? ((IColumnDefinition) item).Name : throw new ArgumentException("item is not IColumnDefinition.");
      return ColumnRestrictionStrategy.RestrictedColumnsByName.ContainsKey(key) && !SystemManager.IsOperationEnabled(ColumnRestrictionStrategy.RestrictedColumnsByName[key]);
    }

    /// <summary>Adds the specified page node identifier.</summary>
    /// <param name="columnName">Name of the column.</param>
    /// <param name="restrictionLevel">The restriction level.</param>
    /// <exception cref="T:System.ArgumentException">data column name</exception>
    internal static void Add(string columnName, RestrictionLevel restrictionLevel)
    {
      if (string.IsNullOrEmpty(columnName))
        throw new ArgumentException("data column name");
      if (ColumnRestrictionStrategy.RestrictedColumnsByName.ContainsKey(columnName))
        return;
      ColumnRestrictionStrategy.RestrictedColumnsByName.Add(columnName, restrictionLevel);
    }

    protected static Dictionary<string, RestrictionLevel> GetRestrictedCommandWidgets() => ColumnRestrictionStrategy.RestrictedColumnsByName;
  }
}
