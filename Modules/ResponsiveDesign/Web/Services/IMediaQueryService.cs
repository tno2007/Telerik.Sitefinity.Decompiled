// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ResponsiveDesign.Web.Services.IMediaQueryService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ServiceModel;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Modules.ResponsiveDesign.Web.Services
{
  /// <summary>
  /// The service interface that defines the API of the service for managing media queries
  /// of the Responsive Design module.
  /// </summary>
  [ServiceContract]
  public interface IMediaQueryService
  {
    /// <summary>
    /// Gets the single media query and returns it in JSON format.
    /// </summary>
    /// <param name="mediaQueryId">Id of the media query data item that ought to be retrieved.</param>
    /// <param name="providerName">Name of the provider that is to be used to retrieve the media query.</param>
    /// <returns>An instance of ItemContext that contains the media query to be retrieved.</returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{mediaQueryId}/?provider={providerName}")]
    ItemContext<MediaQueryViewModel> GetMediaQuery(
      string mediaQueryId,
      string providerName);

    /// <summary>
    /// Gets the single media query and returns it in XML format.
    /// </summary>
    /// <param name="mediaQueryId">Id of the media query data item that ought to be retrieved.</param>
    /// <param name="providerName">Name of the provider that is to be used to retrieve the media query.</param>
    /// <returns>An instance of ItemContext that contains the media query to be retrieved.</returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/{mediaQueryId}/?provider={providerName}")]
    ItemContext<MediaQueryViewModel> GetMediaQueryInXml(
      string mediaQueryId,
      string providerName);

    /// <summary>
    /// Gets all the media queries for the given provider. The results are returned in JSON format.
    /// </summary>
    /// <param name="provider">Name of the provider from which the media queries ought to be retrieved.</param>
    /// <param name="sortExpression">Sort expression used to order the media queries.</param>
    /// <param name="skip">Number of media queries to skip in result set. (used for paging)</param>
    /// <param name="take">Number of media queries to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <returns>Collection context object of <see cref="!:MediaQuery" /> objects.</returns>
    [WebHelp(Comment = "Gets all the media queries for the given provider. The results are returned in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    [OperationContract]
    CollectionContext<MediaQueryViewModel> GetMediaQueries(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter);

    /// <summary>
    /// Gets all the media queries for the given provider. The results are returned in XML format.
    /// </summary>
    /// <param name="provider">Name of the provider from which the media queries ought to be retrieved.</param>
    /// <param name="sortExpression">Sort expression used to order the media queries.</param>
    /// <param name="skip">Number of media queries to skip in result set. (used for paging)</param>
    /// <param name="take">Number of media queries to take in the result set. (used for paging)</param>
    /// <param name="filter">Dynamic LINQ expression used to filter the wanted result set.</param>
    /// <returns>Collection context object of <see cref="!:MediaQuery" /> objects.</returns>
    [WebHelp(Comment = "Gets all the media queries for the given provider. The results are returned in XML format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    [OperationContract]
    CollectionContext<MediaQueryViewModel> GetMediaQueriesInXml(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter);

    /// <summary>
    /// Saves a media query. If the media query with specified id exists that media query will be updated; otherwise new media query will be created.
    /// The saved media query is returned in JSON format.
    /// </summary>
    /// <param name="productId">Id of the media query to be saved.</param>
    /// <param name="product">The media query to be saved.</param>
    /// <param name="provider">The provider through which the media query ought to be saved.</param>
    /// <param name="itemType">The name of the actual type being saved.</param>
    /// <returns>The saved media query.</returns>
    [WebHelp(Comment = "Saves a media query. If the media query with specified id exists that media query will be updated; otherwise new media query will be created. The saved media query is returned in JSON format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{mediaQueryId}/?provider={provider}&itemType={itemType}")]
    [OperationContract]
    ItemContext<MediaQueryViewModel> SaveMediaQuery(
      string mediaQueryId,
      ItemContext<MediaQueryViewModel> mediaQuery,
      string provider,
      string itemType);

    /// <summary>
    /// Saves a media query. If the media query with specified id exists that media query will be updated; otherwise new media query will be created.
    /// The saved media query is returned in XML format.
    /// </summary>
    /// <param name="productId">Id of the media query to be saved.</param>
    /// <param name="product">The media query to be saved.</param>
    /// <param name="provider">The provider through which the media query ought to be saved.</param>
    /// <param name="itemType">The name of the actual type being saved.</param>
    /// <returns>The saved media query.</returns>
    [WebHelp(Comment = "Saves a media query. If the media query with specified id exists that media query will be updated; otherwise new media query will be created. The saved media query is returned in JSON format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/{mediaQueryId}/?provider={provider}&itemType={itemType}")]
    [OperationContract]
    ItemContext<MediaQueryViewModel> SaveMediaQueryInXml(
      string mediaQueryId,
      ItemContext<MediaQueryViewModel> mediaQuery,
      string provider,
      string itemType);

    /// <summary>
    /// Deletes the media query by id and returns true if the media query has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="mediaQueryId">Id of the media query to be deleted.</param>
    /// <param name="provider">The name of provider.</param>
    [WebHelp(Comment = "Deletes the media query for the given provider and supplied id. The results are returned in JSON format.")]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{mediaQueryId}/?provider={provider}")]
    [OperationContract]
    bool DeleteMediaQuery(string mediaQueryId, string provider);

    /// <summary>
    /// Deletes the media query by id and returns true if the media query has been deleted; otherwise false.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="mediaQueryId">Id of the media query to be deleted.</param>
    /// <param name="provider">The name of provider.</param>
    [WebHelp(Comment = "Deletes the media query for the given provider and supplied id. The results are returned in XML format.")]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/{mediaQueryId}/?provider={provider}")]
    [OperationContract]
    bool DeleteMediaQueryInXml(string mediaQueryId, string provider);

    /// <summary>
    /// Deletes a collection of media queries. Result is returned in JSON format.
    /// </summary>
    /// <param name="mediaQueryIds">An array of the media query ids of the media queries to delete.</param>
    /// <param name="provider">The name of the responsive design provider.</param>
    /// <returns>True if all media queries have been deleted; otherwise false.</returns>
    [WebHelp(Comment = "Deletes multiple media queries.")]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/batch/?provider={provider}")]
    [OperationContract]
    bool BatchDeleteMediaQueries(string[] mediaQueryIds, string provider);

    /// <summary>
    /// Deletes a collection of media queries. Result is returned in XML format.
    /// </summary>
    /// <param name="mediaQueryIds">An array of the media query ids of the media queries to delete.</param>
    /// <param name="provider">The name of the responsive design provider.</param>
    /// <returns>True if all media queries have been deleted; otherwise false.</returns>
    [WebHelp(Comment = "Deletes multiple media queries.")]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/xml/batch/?provider={provider}")]
    [OperationContract]
    bool BatchDeleteMediaQueriesInXml(string[] mediaQueryIds, string provider);

    /// <summary>
    /// Saves or updates the media query links for the provided page/template.
    /// </summary>
    /// <param name="provider">The provider through which the media query link ought to be saved.</param>
    /// <returns>The saved media query link view model</returns>
    [WebHelp(Comment = "Saves or updates the media query links for the provided page/template")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/mql/?provider={provider}")]
    [OperationContract]
    ItemContext<MediaQueryLinkViewModel> SaveMediaQueryLink(
      ItemContext<MediaQueryLinkViewModel> mediaQueryLink,
      string provider);
  }
}
