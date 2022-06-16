// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Web.Services.IMarkedItemsService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ServiceModel;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Modules.GenericContent.Web.Services;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Taxonomies.Web.Services
{
  /// <summary>The WCF web service interface for taxonomies.</summary>
  [ServiceContract]
  public interface IMarkedItemsService
  {
    /// <summary>
    /// Gets the items that are marked with a specified taxon and returns the collection context of <see cref="!:ContentSummary" />
    /// objects in JSON format.
    /// </summary>
    /// <param name="taxonId">Id of the taxon that marks the items that are to be returned.</param>
    /// <param name="itemType">Type of the item that ought to be returned.</param>
    /// <param name="provider">Name of the provider that ought to be used to return the collection of content summary objects.</param>
    /// <param name="sortExpression">Sort expression used to order the marked items.</param>
    /// <param name="skip">Number of items to skip before starting to take the items in the result set.</param>
    /// <param name="take">Number of items to take in the result set.</param>
    /// <param name="filter">Filter expression used to filter the items that will be taken in the result set.</param>
    /// <returns>Collection of ContentSummary objects that are marked with the specified taxon.</returns>
    [OperationContract]
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/items/{taxonId}/?itemType={itemType}&provider={provider}&itemProvider={itemProvider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    CollectionContext<WcfMarketContentItem> GetItems(
      string taxonId,
      string itemType,
      string provider,
      string itemProvider,
      string sortExpression,
      int skip,
      int take,
      string filter);

    /// <summary>
    /// Gets the items that are marked with a specified taxon and returns the collection context of <see cref="!:ContentSummary" />
    /// objects in XML format.
    /// </summary>
    /// <param name="taxonId">Id of the taxon that marks the items that are to be returned.</param>
    /// <param name="itemType">Type of the item that ought to be returned.</param>
    /// <param name="provider">Name of the provider that ought to be used to return the collection of content summary objects.</param>
    /// <param name="sortExpression">Sort expression used to order the marked items.</param>
    /// <param name="skip">Number of items to skip before starting to take the items in the result set.</param>
    /// <param name="take">Number of items to take in the result set.</param>
    /// <param name="filter">Filter expression used to filter the items that will be taken in the result set.</param>
    /// <returns>Collection of ContentSummary objects that are marked with the specified taxon.</returns>
    [OperationContract]
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/items/{taxonId}/?itemType={itemType}&provider={provider}&itemProvider={itemProvider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}")]
    CollectionContext<WcfMarketContentItem> GetItemsInXml(
      string taxonId,
      string itemType,
      string provider,
      string itemProvider,
      string sortExpression,
      int skip,
      int take,
      string filter);

    /// <summary>
    /// Gets the statistical information for a specified taxon and returns the collection of taxon statistic objects in JSON format.
    /// </summary>
    /// <param name="taxonId">Id of the taxon for which the statistical information ought to be retrieved.</param>
    /// <param name="provider">Name of the taxonomy provider from which the statistical information ought to be retrieved.</param>
    /// <returns>CollectionContext object with the collection of wcf taxon statistic objects.</returns>
    [OperationContract]
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{taxonId}/?provider={provider}")]
    CollectionContext<WcfTaxonStatistic> GetTaxonStatistics(
      string taxonId,
      string provider);

    /// <summary>
    /// Gets the statistical information for a specified taxon and returns the collection of taxon statistic objects in XML format.
    /// </summary>
    /// <param name="taxonId">Id of the taxon for which the statistical information ought to be retrieved.</param>
    /// <param name="provider">Name of the taxonomy provider from which the statistical information ought to be retrieved.</param>
    /// <returns>CollectionContext object with the collection of wcf taxon statistic objects.</returns>
    [OperationContract]
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/{taxonId}/?provider={provider}")]
    CollectionContext<WcfTaxonStatistic> GetTaxonStatisticsInXml(
      string taxonId,
      string provider);

    /// <summary>
    /// Removes (unmarks) the item from the taxon and returns true if the item has been removed; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="taxonId">Id of the taxon that should not mark the item anymore.</param>
    /// <param name="itemType">Type of the item that should be unmarked.</param>
    /// <param name="itemIDs">Id of the item that should be unmarked.</param>
    /// <param name="provider">Name of the taxonomy provider that should be used when unmarking the item.</param>
    [OperationContract]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/items/{taxonId}/?itemType={itemType}&provider={provider}&itemProvider={itemProvider}")]
    bool RemoveItemFromTaxon(
      string taxonId,
      string itemType,
      string provider,
      string itemProvider,
      string itemIDs);

    /// <summary>
    /// Removes (unmarks) the item from the taxon and returns true if the item has been removed; otherwise false.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="taxonId">Id of the taxon that should not mark the item anymore.</param>
    /// <param name="itemType">Type of the item that should be unmarked.</param>
    /// <param name="itemIDs">Id of the item that should be unmarked.</param>
    /// <param name="provider">Name of the taxonomy provider that should be used when unmarking the item.</param>
    [OperationContract]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/items/{taxonId}/?itemType={itemType}&provider={provider}&itemProvider={itemProvider}")]
    bool RemoveItemFromTaxonInXml(
      string taxonId,
      string itemType,
      string provider,
      string itemProvider,
      string itemIDs);
  }
}
