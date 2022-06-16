// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.DataSource.DataSourceRegistry
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Telerik.Sitefinity.Data.DataSource
{
  /// <summary>
  /// Provides functionality for managing <see cref="T:Telerik.Sitefinity.Data.DataSource.IDataSource" /> objects
  /// </summary>
  internal class DataSourceRegistry : IDataSourceRegistry
  {
    private ConcurrentDictionary<string, IDataSource> dataSources;

    /// <inheritdoc />
    public DataSourceRegistry() => this.dataSources = new ConcurrentDictionary<string, IDataSource>();

    /// <inheritdoc />
    public IEnumerable<IDataSource> GetDataSources() => (IEnumerable<IDataSource>) this.dataSources.Values;

    /// <inheritdoc />
    public void RegisterDataSource(IDataSource dataSource) => this.dataSources.TryAdd(dataSource.Name, dataSource);

    /// <inheritdoc />
    public void UnregisterDataSource(string dataSourceName) => this.dataSources.TryRemove(dataSourceName, out IDataSource _);

    /// <inheritdoc />
    public bool IsDataSourceRegistered(string dataSourceName) => this.dataSources.ContainsKey(dataSourceName);

    /// <inheritdoc />
    public void Clear() => this.dataSources.Clear();

    /// <inheritdoc />
    public IDataSource GetDataSource(string dataSourceName) => this.dataSources[dataSourceName];
  }
}
