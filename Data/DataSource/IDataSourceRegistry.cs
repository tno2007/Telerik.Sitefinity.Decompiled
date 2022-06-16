// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.DataSource.IDataSourceRegistry
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;

namespace Telerik.Sitefinity.Data.DataSource
{
  /// <summary>
  /// Provides functionality for managing <see cref="T:Telerik.Sitefinity.Data.DataSource.IDataSource" /> objects
  /// </summary>
  internal interface IDataSourceRegistry
  {
    /// <summary>Gets all registered data sources inside the system</summary>
    /// <returns>Returns all registered data sources inside the system</returns>
    IEnumerable<IDataSource> GetDataSources();

    /// <summary>Registers a data source</summary>
    /// <param name="dataSource">The data source</param>
    void RegisterDataSource(IDataSource dataSource);

    /// <summary>Unregisters a data source</summary>
    /// <param name="dataSourceName">The name of the data source</param>
    void UnregisterDataSource(string dataSourceName);

    /// <summary>
    /// Determines whether a data source with the specified name is registered.
    /// </summary>
    /// <param name="dataSourceName">The name of the data source.</param>
    /// <returns>
    /// 	<c>true</c> if a data source with the specified name is registered; otherwise, <c>false</c>.
    /// </returns>
    bool IsDataSourceRegistered(string dataSourceName);

    /// <summary>Unregisters all registered data sources</summary>
    void Clear();

    /// <summary>Gets a registered data source by key</summary>
    /// <param name="key"></param>
    /// <returns>The item that corresponds to given key</returns>
    IDataSource GetDataSource(string dataSourceName);
  }
}
