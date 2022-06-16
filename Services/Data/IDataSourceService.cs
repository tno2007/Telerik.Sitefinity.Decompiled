// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Data.IDataSourceService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ServiceModel;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Services.Data
{
  /// <summary>
  /// Interface that defines the members of the service working with data sources.
  /// </summary>
  [ServiceContract]
  public interface IDataSourceService
  {
    /// <summary>
    /// Gets the providers for specified site and data source. The results are returned in JSON format.
    /// </summary>
    /// <param name="siteId">The id of the site.</param>
    /// <param name="dataSourceName">The name of the data source.</param>
    /// <param name="sortExpression">Sort expression used to order the providers.</param>
    /// <param name="skip">Number of providers to skip in result set. (used for paging)</param>
    /// <param name="take">Number of providers to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <param name="addDefaultSiteProvider">if set to <c>true</c> [add default site provider].</param>
    /// <returns>
    /// Collection context object of <see cref="T:Telerik.Sitefinity.Services.Data.DataProviderViewModel" /> objects.
    /// </returns>
    [WebHelp(Comment = "Gets the providers for specified site and data source. The results are returned in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/providers/?siteId={siteId}&dataSourceName={dataSourceName}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&addDefaultSiteProvider={addDefaultSiteProvider}")]
    [OperationContract]
    CollectionContext<DataProviderViewModel> GetDataSourceProviders(
      string siteId,
      string dataSourceName,
      string sortExpression,
      int skip,
      int take,
      string filter,
      bool addDefaultSiteProvider);

    /// <summary>
    /// Gets the providers for specified site and data source. The results are returned in XML format.
    /// </summary>
    /// <param name="siteId">The id of the site.</param>
    /// <param name="dataSourceName">The name of the data source.</param>
    /// <param name="sortExpression">Sort expression used to order the providers.</param>
    /// <param name="skip">Number of providers to skip in result set. (used for paging)</param>
    /// <param name="take">Number of providers to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <param name="addDefaultSiteProvider">if set to <c>true</c> [add default site provider].</param>
    /// <returns>
    /// Collection context object of <see cref="T:Telerik.Sitefinity.Services.Data.DataProviderViewModel" /> objects.
    /// </returns>
    [WebHelp(Comment = "Gets the providers for specified site and data source. The results are returned in XML format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/providers/?siteId={siteId}&dataSourceName={dataSourceName}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&addDefaultSiteProvider={addDefaultSiteProvider}")]
    [OperationContract]
    CollectionContext<DataProviderViewModel> GetDataSourceProvidersInXml(
      string siteId,
      string dataSourceName,
      string sortExpression,
      int skip,
      int take,
      string filter,
      bool addDefaultSiteProvider);

    /// <summary>
    /// Gets the providers for specified site and content type. The results are returned in JSON format.
    /// </summary>
    /// <param name="siteId">The id of the site.</param>
    /// <param name="typeName">The name of the content type.</param>
    /// <returns>
    /// Collection context object of <see cref="T:Telerik.Sitefinity.Services.Data.DataProviderViewModel" /> objects.
    /// </returns>
    [WebHelp(Comment = "Gets the providers for specified site and content type. The results are returned in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/providers/{typeName}/?siteId={siteId}")]
    [OperationContract]
    CollectionContext<DataProviderViewModel> GetTypeProviders(
      string siteId,
      string typeName);
  }
}
