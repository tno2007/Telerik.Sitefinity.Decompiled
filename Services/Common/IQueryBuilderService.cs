// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Common.IQueryBuilderService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ServiceModel;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Web.Model;

namespace Telerik.Sitefinity.Services.Common
{
  /// <summary>
  /// WCF interface for the service used by the QueryBuilder control
  /// </summary>
  [ServiceContract]
  public interface IQueryBuilderService
  {
    /// <summary>Gets the queries for the QueryBuilder control.</summary>
    /// <param name="sortExpression">The sort expression.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <param name="filter">The filter.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns></returns>
    [OperationContract]
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/?sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&provider={providerName}")]
    QueryCollectionContext GetQueries(
      string sortExpression,
      int skip,
      int take,
      string filter,
      string providerName);

    /// <summary>Gets the query.</summary>
    /// <param name="pageId">The pageId.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns></returns>
    [OperationContract]
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{id}/?provider={providerName}")]
    QueryData GetQuery(string id, string providerName);

    /// <summary>Creates or updates a query</summary>
    /// <param name="query">The query.</param>
    /// <param name="queryId">The query pageId.</param>
    /// <param name="providerName">Name of the provider.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", UriTemplate = "/{queryId}/?provider={providerName}")]
    void CreateOrUpdateQuery(QueryData query, string queryId, string providerName);

    /// <summary>Gets the query expression.</summary>
    /// <param name="query">The query.</param>
    /// <returns></returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "")]
    string GetQueryExpression(QueryData query);
  }
}
