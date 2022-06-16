// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Web.Services.IFlatTaxonService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ServiceModel;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Taxonomies.Web.Services
{
  /// <summary>
  /// The WCF web service interface for flat taxonomy management.
  /// </summary>
  [ServiceContract]
  public interface IFlatTaxonService
  {
    /// <summary>
    /// Gets the collection of flat taxons and returns the result in JSON format.
    /// </summary>
    /// <param name="taxonomyId">Id of the taxonomy from which the taxons should be returned.</param>
    /// <param name="provider">Name of the provider to be used when retrieving the taxons.</param>
    /// <param name="sortExpression">Sort expression used to order the taxons in the collection.</param>
    /// <param name="skip">Number of items to skip before starting to take results in the result set.</param>
    /// <param name="take">Number of items to take in the result set.</param>
    /// <param name="filter">Filter expression used to filter the items that ought to be part of the result set.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="mode">The mode.</param>
    /// <param name="siteContextMode">Defines the mode which determines which taxonomy to use based on the site context.</param>
    /// <returns>
    /// A CollectionContext instance that contains the collection of WcfFlatTaxon objects.
    /// </returns>
    [WebHelp(Comment = "Gets the collection of flat taxons and returns the result in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{taxonomyId}/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&itemType={itemType}&mode={mode}&siteContextMode={siteContextMode}")]
    [OperationContract]
    CollectionContext<WcfFlatTaxon> GetTaxa(
      string taxonomyId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string itemType,
      string mode,
      string siteContextMode = "currentSiteContext");

    /// <summary>
    /// Gets the collection of flat taxons and returns the result in XML format.
    /// </summary>
    /// <param name="taxonomyId">Id of the taxonomy from which the taxons should be returned.</param>
    /// <param name="provider">Name of the provider to be used when retrieving the taxons.</param>
    /// <param name="sortExpression">Sort expression used to order the taxons in the collection.</param>
    /// <param name="skip">Number of items to skip before starting to take results in the result set.</param>
    /// <param name="take">Number of items to take in the result set.</param>
    /// <param name="filter">Filter expression used to filter the items that ought to be part of the result set.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="mode">The mode.</param>
    /// <param name="siteContextMode">Defines the mode which determines which taxonomy to use based on the site context.</param>
    /// <returns>
    /// A CollectionContext instance that contains the collection of WcfFlatTaxon objects.
    /// </returns>
    [WebHelp(Comment = "Gets the collection of flat taxons and returns the result in XML format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/{taxonomyId}/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&itemType={itemType}&mode={mode}&siteContextMode={siteContextMode}")]
    [OperationContract]
    CollectionContext<WcfFlatTaxon> GetTaxaInXml(
      string taxonomyId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string itemType,
      string mode,
      string siteContextMode = "currentSiteContext");

    /// <summary>
    /// Gets the collection of flat taxons and returns the result in JSON format. Request is expected with a POST HTTP method.
    /// </summary>
    /// <param name="taxonomyId">Id of the taxonomy from which the taxons should be returned.</param>
    /// <param name="filter">Represents request data that is required to retrieve taxons.</param>
    /// <returns>
    /// A CollectionContext instance that contains the collection of WcfFlatTaxon objects.
    /// </returns>
    [WebHelp(Comment = "Gets the collection of flat taxons and returns the result in JSON format.")]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "{taxonomyId}/")]
    [OperationContract]
    CollectionContext<WcfFlatTaxon> GetTaxaExtended(
      string taxonomyId,
      TaxaFilter filter);

    /// <summary>
    /// Gets the collection of flat taxons and returns the result in XML format.Request is expected with a POST HTTP method.
    /// </summary>
    /// <param name="taxonomyId">Id of the taxonomy from which the taxons should be returned.</param>
    /// <param name="filter">Represents request data that is required to retrieve taxons.</param>
    /// <returns>
    /// A CollectionContext instance that contains the collection of WcfFlatTaxon objects.
    /// </returns>
    [WebHelp(Comment = "Gets the collection of flat taxons and returns the result in XML format.")]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/{taxonomyId}/")]
    [OperationContract]
    CollectionContext<WcfFlatTaxon> GetTaxaExtendedInXml(
      string taxonomyId,
      TaxaFilter filter);

    /// <summary>
    /// Gets a single flat taxon and returns it in JSON format.
    /// </summary>
    /// <param name="taxonomyId">Id of the taxonomy to which the taxon belongs to.</param>
    /// <param name="taxonId">Id of the taxon that ought to be retrieved.</param>
    /// <param name="provider">Name of the provider to be used when retrieving the taxon.</param>
    /// <returns>An instance of WcfFlatTaxon.</returns>
    [WebHelp(Comment = "Gets a single flat taxon and returns it in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{taxonomyId}/{taxonId}/?provider={provider}&itemType={itemType}")]
    [OperationContract]
    WcfFlatTaxon GetTaxon(
      string taxonomyId,
      string taxonId,
      string provider,
      string itemType);

    /// <summary>
    /// Gets a single flat taxon and returns it in XML format.
    /// </summary>
    /// <param name="taxonomyId">Id of the taxonomy to which the taxon belongs to.</param>
    /// <param name="taxonId">Id of the taxon that ought to be retrieved.</param>
    /// <param name="provider">Name of the provider to be sued when retrieving the taxon.</param>
    /// <returns>An instance of WcfFlatTaxon.</returns>
    [WebHelp(Comment = "Gets a single flat taxon and returns it in XML format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/{taxonomyId}/{taxonId}/?provider={provider}&itemType={itemType}")]
    [OperationContract]
    WcfFlatTaxon GetTaxonInXml(
      string taxonomyId,
      string taxonId,
      string provider,
      string itemType);

    /// <summary>
    /// Saves a flat taxon. If the taxon with specified pageId exists that taxon will be updated; otherwise new taxon will
    /// be created. The saved taxon is returned in JSON format.
    /// </summary>
    /// <param name="wcfTaxon">An instance of taxon to be saved.</param>
    /// <param name="taxonomyId">Id of the taxonomy to which the taxon belongs.</param>
    /// <param name="taxonId">Id of the taxon that ought to be saved.</param>
    /// <param name="provider">Name of the provider to be used when saving the taxon.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="skipSiteContext">The [skip site context] determines whether the taxonomy should be resolved in the current site context.</param>
    /// <returns>An instance of WcfFlatTaxon that was saved.</returns>
    [WebHelp(Comment = "Saves a flat taxon. If the taxon with specified id exists that taxon will be updated; otherwise new taxon will be created. The saved taxon is returned in JSON format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{taxonomyId}/{taxonId}/?provider={provider}&itemType={itemType}&skipSiteContext={skipSiteContext}")]
    [OperationContract]
    WcfFlatTaxon SaveTaxon(
      WcfFlatTaxon wcfTaxon,
      string taxonomyId,
      string taxonId,
      string provider,
      string itemType,
      bool skipSiteContext);

    /// <summary>
    /// Saves a flat taxon. If the taxon with specified pageId exists that taxon will be updated; otherwise new taxon will
    /// be created. The saved taxon is returned in XML format.
    /// </summary>
    /// <param name="wcfTaxon">An instance of taxon to be saved.</param>
    /// <param name="taxonomyId">Id of the taxonomy to which the taxon belongs.</param>
    /// <param name="taxonId">Id of the taxon that ought to be saved.</param>
    /// <param name="provider">Name of the provider to be used when saving the taxon.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="skipSiteContext">The [skip site context] determines whether the taxonomy should be resolved in the current site context.</param>
    /// <returns>An instance of WcfFlatTaxon that was saved.</returns>
    [WebHelp(Comment = "Saves a flat taxon. If the taxon with specified id exists that taxon will be updated; otherwise new taxon will be created. The saved taxon is returned in XML format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/{taxonomyId}/{taxonId}/?provider={provider}&itemType={itemType}&skipSiteContext={skipSiteContext}")]
    [OperationContract]
    WcfFlatTaxon SaveTaxonInXml(
      WcfFlatTaxon wcfTaxon,
      string taxonomyId,
      string taxonId,
      string provider,
      string itemType,
      bool skipSiteContext);

    /// <summary>
    /// Deletes a list of flat taxons.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="taxonIds">An array of the ids of the taxons to delete.</param>
    /// <param name="taxonomyId">The taxonomy pageId.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="language">The language to be deleted. If null, all versions will be deleted</param>
    /// <returns>true if all flat taxons have been deleted; otherwise false.</returns>
    [WebHelp(Comment = "Deletes multiple flat taxons.")]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "{taxonomyId}/batch/?provider={provider}&language={language}")]
    [OperationContract]
    bool BulkDeleteTaxons(string[] taxonIds, string taxonomyId, string provider, string language);

    /// <summary>
    /// Deletes a list of flat taxons.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="taxonIds">An array of the ids of the taxons to delete.</param>
    /// <param name="taxonomyId">The taxonomy pageId.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="language">The language to be deleted. If null, all versions will be deleted</param>
    /// <returns>true if all flat taxons have been deleted; otherwise false.</returns>
    [WebHelp(Comment = "Deletes multiple flat taxons.")]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/{taxonomyId}/batch/?provider={provider}&language={language}")]
    [OperationContract]
    bool BulkDeleteTaxonsInXml(
      string[] taxonIds,
      string taxonomyId,
      string provider,
      string language);

    /// <summary>
    /// Deletes a flat taxon and returns true if the flat taxon has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="taxonomyId">Id of the taxonomy to which the taxon that ought to be deleted belongs to.</param>
    /// <param name="taxonId">Id of the taxon that ought to be deleted.</param>
    /// <param name="provider">Name of the provider to be used when deleting a taxon.</param>
    /// <param name="lang">The language to be deleted. If null, all versions will be deleted</param>
    [WebHelp(Comment = "Deletes a flat taxon.")]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, UriTemplate = "{taxonomyId}/{taxonId}/?provider={provider}&itemType={itemType}&lang={lang}")]
    [OperationContract]
    bool DeleteTaxon(
      string taxonomyId,
      string taxonId,
      string provider,
      string itemType,
      string lang);

    /// <summary>
    /// Deletes a flat taxon and returns true if the flat taxon has been deleted; otherwise false.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="taxonomyId">Id of the taxonomy to which the taxon that ought to be deleted belongs to.</param>
    /// <param name="taxonId">Id of the taxon that ought to be deleted.</param>
    /// <param name="provider">Name of the provider to be used when deleting a taxon.</param>
    /// <param name="lang">The language to be deleted. If null, all versions will be deleted</param>
    [WebHelp(Comment = "Deletes a flat taxon.")]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/{taxonomyId}/{taxonId}/?provider={provider}&itemType={itemType}&lang={lang}")]
    [OperationContract]
    bool DeleteTaxonInXml(
      string taxonomyId,
      string taxonId,
      string provider,
      string itemType,
      string lang);

    /// <summary>
    /// This method accepts an array of taxon titles. For each title it will try to retrive a tag with the same title from the specified
    /// taxonomy. For the titles for which taxon cannot be found, new taxon objects will be created. Method will return an array of WcfFlatTaxon
    /// objects (retrieved by title or newly created) in JSON format.
    /// </summary>
    /// <param name="taxonomyId">Id of the flat taxonomy in which the taxa ought to be ensured.</param>
    /// <param name="taxonTitle">Title of the taxon that ought to be ensured.</param>
    /// <param name="provider">Name of the provider to be used when ensuring the taxon.</param>
    /// <returns>
    /// An array of WcfFlatTaxon objects representing the taxon objects based on the passed taxon title array.
    /// </returns>
    [WebHelp(Comment = "This method accepts an array of taxon titles. For each title it will try to retrive a tag with the same title from the specified taxonomy. For the titles for which taxon cannot be found, new taxon objects will be created. Method will return a list of WcfFlatTaxon objects (retrieved by title or newly created) in JSON format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{taxonomyId}/ensure/?provider={provider}&itemType={itemType}")]
    [OperationContract]
    WcfFlatTaxon[] EnsureTaxa(
      string taxonomyId,
      string[] taxonTitles,
      string provider,
      string itemType);

    /// <summary>
    /// This method accepts an array of taxon titles. For each title it will try to retrive a tag with the same title from the specified
    /// taxonomy. For the titles for which taxon cannot be found, new taxon objects will be created. Method will return an array of WcfFlatTaxon
    /// objects (retrieved by title or newly created) in JSON format.
    /// </summary>
    /// <param name="taxonomyId">Id of the flat taxonomy in which the taxa ought to be ensured.</param>
    /// <param name="taxonTitle">Title of the taxon that ought to be ensured.</param>
    /// <param name="provider">Name of the provider to be used when ensuring the taxon.</param>
    /// <returns>
    /// An array of WcfFlatTaxon objects representing the taxon objects based on the passed taxon title array.
    /// </returns>
    [WebHelp(Comment = "This method accepts an array of taxon titles. For each title it will try to retrive a tag with the same title from the specified taxonomy. For the titles for which taxon cannot be found, new taxon objects will be created. Method will return a list of WcfFlatTaxon objects (retrieved by title or newly created) in XML format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/{taxonomyId}/ensure/?provider={provider}&itemType={itemType}")]
    [OperationContract]
    WcfFlatTaxon[] EnsureTaxaInXml(
      string taxonomyId,
      string[] taxonTitles,
      string provider,
      string itemType);
  }
}
