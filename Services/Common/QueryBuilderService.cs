// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Common.QueryBuilderService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.ServiceModel.Activation;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.QueryBuilder;
using Telerik.Sitefinity.Web.Model;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Services.Common
{
  /// <summary>
  /// A service used by the QueryBuilder to get or save queries
  /// </summary>
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  public class QueryBuilderService : IQueryBuilderService
  {
    /// <summary>Gets the query.</summary>
    /// <param name="pageId">The pageId.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns></returns>
    public QueryData GetQuery(string id, string providerName) => (ManagerBase<QueryDataProvider>.GetManager<QueryManager>(providerName).GetQuery(new Guid(id)) ?? throw new KeyNotFoundException()).QueryData;

    /// <summary>Gets the queries for the QueryBuilder control.</summary>
    /// <param name="sortExpression">The sort expression.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <param name="filter">The filter.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns></returns>
    public QueryCollectionContext GetQueries(
      string sortExpression,
      int skip,
      int take,
      string filter,
      string providerName)
    {
      QueryManager manager = ManagerBase<QueryDataProvider>.GetManager<QueryManager>(providerName);
      int? totalCount = new int?();
      return new QueryCollectionContext((IEnumerable<QueryData>) DataProviderBase.SetExpressions<Query>(manager.GetQueries(), filter, sortExpression, new int?(skip), new int?(take), ref totalCount).AsQueryable<Query>().Select<Query, QueryData>((Expression<Func<Query, QueryData>>) (q => q.QueryData)));
    }

    /// <summary>Saves the query in the QB.</summary>
    /// <param name="query">The query.</param>
    public void CreateOrUpdateQuery(QueryData query, string queryId, string providerName)
    {
      QueryManager manager = QueryManager.GetManager(providerName);
      Query query1;
      if (query.Id == Guid.Empty)
      {
        manager.CancelChanges();
        query1 = manager.CreateQuery();
      }
      else
        query1 = manager.GetQuery(query.Id);
      query1.QueryData = query;
      manager.SaveChanges();
    }

    /// <summary>Gets the query expression.</summary>
    /// <param name="query">The query.</param>
    /// <returns></returns>
    public string GetQueryExpression(QueryData query) => new ExpressionGenerator(query).Generate().ToString();
  }
}
