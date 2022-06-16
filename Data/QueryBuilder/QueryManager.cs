// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.QueryBuilder.QueryManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.Model;

namespace Telerik.Sitefinity.Data.QueryBuilder
{
  /// <summary>
  /// Represents an intermediary between Query objects and persisted data
  /// </summary>
  public class QueryManager : ManagerBase<QueryDataProvider>
  {
    private ConfigElementDictionary<string, DataProviderSettings> providerSettings;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.QueryBuilder.QueryManager" /> class with the default provider.
    /// </summary>
    public QueryManager()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.QueryBuilder.QueryManager" /> class and sets the specified provider.
    /// </summary>
    /// <param name="providerName">Name of the provider. If empty string or null, the default provider is set</param>
    public QueryManager(string providerName)
      : base(providerName)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.QueryBuilder.QueryManager" /> class.
    /// </summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="transactionName">Name of the transaction.</param>
    public QueryManager(string providerName, string transactionName)
      : base(providerName, transactionName)
    {
    }

    /// <summary>Creates a new query</summary>
    /// <param name="persistentClassName">Name of the persistent class.</param>
    /// <param name="dataProviderName">Name of the data provider.</param>
    /// <returns></returns>
    public virtual Query CreateQuery() => this.Provider.CreateQuery();

    /// <summary>Creates a new query with the specified ID.</summary>
    /// <param name="pageId">The pageId of the query.</param>
    /// <param name="persistentClassName">Name of the persistent class.</param>
    /// <param name="dataProviderName">Name of the data provider.</param>
    /// <returns></returns>
    public virtual Query CreateQuery(Guid id) => this.Provider.CreateQuery(id);

    /// <summary>Gets a query by ID.</summary>
    /// <param name="pageId">The pageId of the query.</param>
    /// <returns></returns>
    public virtual Query GetQuery(Guid id) => this.Provider.GetQuery(id);

    /// <summary>Gets all queries.</summary>
    /// <returns></returns>
    public virtual IQueryable<Query> GetQueries() => this.Provider.GetQueries();

    /// <summary>Gets queries for a persistent type</summary>
    /// <param name="persistentTypeName">The name of the persistent type</param>
    /// <returns></returns>
    public virtual IQueryable<Query> GetQueries(string persistentTypeName) => this.Provider.GetQueries(persistentTypeName);

    /// <summary>Deletes a query.</summary>
    /// <param name="query">The query to delete.</param>
    public virtual void DeleteQuery(Query query) => this.Provider.DeleteQuery(query);

    /// <summary>The name of the module that this manager belongs to.</summary>
    /// <value></value>
    public override string ModuleName => "QueryBuilder";

    /// <summary>Gets the default provider for this manager.</summary>
    /// <value></value>
    protected internal override GetDefaultProvider DefaultProviderDelegate => (GetDefaultProvider) (() => Config.Get<QueryBuilderConfig>().DefaultProvider);

    /// <summary>Gets all provider settings.</summary>
    protected internal override ConfigElementDictionary<string, DataProviderSettings> ProvidersSettings
    {
      get
      {
        if (this.providerSettings == null)
          this.providerSettings = Config.Get<QueryBuilderConfig>().Providers;
        return this.providerSettings;
      }
    }

    /// <summary>Gets a manager instance for the default data provider</summary>
    /// <returns></returns>
    public static QueryManager GetManager() => ManagerBase<QueryDataProvider>.GetManager<QueryManager>();

    /// <summary>
    /// Gets a manger instance for the specified data provider.
    /// </summary>
    /// <param name="providerName">The name of the data provider.</param>
    /// <returns>The manager instance.</returns>
    public static QueryManager GetManager(string providerName) => ManagerBase<QueryDataProvider>.GetManager<QueryManager>(providerName);

    /// <summary>Gets the manager.</summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="transactionName">Name of the transaction.</param>
    /// <returns></returns>
    public static QueryManager GetManager(string providerName, string transactionName) => ManagerBase<QueryDataProvider>.GetManager<QueryManager>(providerName, transactionName);
  }
}
