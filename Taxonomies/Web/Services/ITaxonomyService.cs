// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Web.Services.ITaxonomyService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Taxonomies.Web.Services
{
  /// <summary>The WCF web service interface for taxonomies.</summary>
  [ServiceContract]
  public interface ITaxonomyService
  {
    /// <summary>
    /// Gets the collection of taxonomies and returns the result in JSON format.
    /// </summary>
    /// <param name="provider">Name of the taxonomy provider to be used when retriving taxonomies.</param>
    /// <param name="sortExpression">Sort expression used to order the collection of taxonomies.</param>
    /// <param name="skip">Number of items to skip before starting to take the result set.</param>
    /// <param name="take">Number of items to take in the result set.</param>
    /// <param name="filter">Filter expression used to filter which taxonomies ought to be retrieved.</param>
    /// <param name="taxonomyType">Type of the taxonomy that ought to be retrieved in the result set.</param>
    /// <returns>An instance of CollectionContext object that contains the collection of taxonomies.</returns>
    [WebHelp(Comment = "Gets the collection of taxonomies and returns the result in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&taxonomyType={taxonomyType}&skipSiteContext={skipSiteContext}")]
    [OperationContract]
    CollectionContext<WcfTaxonomy> GetTaxonomies(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string taxonomyType,
      bool skipSiteContext = false);

    /// <summary>
    /// Gets the collection of taxonomies and returns the result in XML format.
    /// </summary>
    /// <param name="provider">Name of the taxonomy provider to be used when retriving taxonomies.</param>
    /// <param name="sortExpression">Sort expression used to order the collection of taxonomies.</param>
    /// <param name="skip">Number of items to skip before starting to take the result set.</param>
    /// <param name="take">Number of items to take in the result set.</param>
    /// <param name="filter">Filter expression used to filter which taxonomies ought to be retrieved.</param>
    /// <param name="taxonomyType">Type of the taxonomy that ought to be retrieved in the result set.</param>
    /// <returns>An instance of CollectionContext object that contains the collection of taxonomies.</returns>
    [WebHelp(Comment = "Gets the collection of taxonomies and returns the result in XML format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&taxonomyType={taxonomyType}&skipSiteContext={skipSiteContext}")]
    [OperationContract]
    CollectionContext<WcfTaxonomy> GetTaxonomiesInXml(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string taxonomyType,
      bool skipSiteContext = false);

    /// <summary>
    /// Gets a single taxonomy and returns it in the JSON format.
    /// </summary>
    /// <param name="taxonomyId">Id of the taxonomy to be retrieved.</param>
    /// <param name="provider">Name of the taxonomy provider to be used when retrieving the taxonomy.</param>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Taxonomies.Web.Services.WcfTaxonomy" /> object representing the taxonomy.</returns>
    [WebHelp(Comment = "Gets a single taxonomy and returns it in the JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "{taxonomyId}/?provider={provider}")]
    [OperationContract]
    WcfTaxonomy GetTaxonomy(string taxonomyId, string provider);

    /// <summary>
    /// Gets a single taxonomy and returns it in the XML format.
    /// </summary>
    /// <param name="taxonomyId">Id of the taxonomy to be retrieved.</param>
    /// <param name="provider">Name of the taxonomy provider to be used when retrieving the taxonomy.</param>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Taxonomies.Web.Services.WcfTaxonomy" /> object representing the taxonomy.</returns>
    [WebHelp(Comment = "Gets a single taxonomy and returns it in the XML format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/{taxonomyId}/?provider={provider}")]
    [OperationContract]
    WcfTaxonomy GetTaxonomyInXml(string taxonomyId, string provider);

    /// <summary>
    /// Saves a taxonomy. If the taxonomy exists it updates it; otherwise creates a new taxonomy.
    /// Returns the saved taxonomy object in JSON format.
    /// </summary>
    /// <param name="taxonomy">Taxonomy object to be saved.</param>
    /// <param name="taxonomyId">Id of the taxonomy under which taxonomy ought to be saved.</param>
    /// <param name="provider">Name of the taxonomy provider to be used when saving the taxonomy.</param>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Taxonomies.Web.Services.WcfTaxonomy" /> that represents the saved taxonomy object.</returns>
    [WebHelp(Comment = "Saves a taxonomy. If the taxonomy exists it updates it; otherwise creates a new taxonomy. Returns the saved taxonomy object in JSON format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{taxonomyId}/?provider={provider}")]
    [OperationContract]
    WcfTaxonomy SaveTaxonomy(WcfTaxonomy taxonomy, string taxonomyId, string provider);

    /// <summary>
    /// Saves a taxonomy. If the taxonomy exists it updates it; otherwise creates a new taxonomy.
    /// Returns the saved taxonomy object in XML format.
    /// </summary>
    /// <param name="taxonomy">Taxonomy object to be saved.</param>
    /// <param name="taxonomyId">Id of the taxonomy under which taxonomy ought to be saved.</param>
    /// <param name="provider">Name of the taxonomy provider to be used when saving the taxonomy.</param>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Taxonomies.Web.Services.WcfTaxonomy" /> that represents the saved taxonomy object.</returns>
    [WebHelp(Comment = "Saves a taxonomy. If the taxonomy exists it updates it; otherwise creates a new taxonomy. Returns the saved taxonomy object in XML format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/{taxonomyId}/?provider={provider}")]
    [OperationContract]
    WcfTaxonomy SaveTaxonomyInXml(
      WcfTaxonomy taxonomy,
      string taxonomyId,
      string provider);

    /// <summary>
    /// Deletes a taxonomy and returns true if the taxonomy has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="taxonomyId">Id of the taxonomy that ought to be deleted.</param>
    /// <param name="provider">Name of the taxonomy provider to be used when deleting the taxonomy.</param>
    /// <param name="lang">The language to be deleted. If null, all versions will be deleted</param>
    /// <param name="deleteTaxaOnly">Indicates whether to delete all the taxonomy taxa only or delete the entire taxonomy itself.</param>
    [WebHelp(Comment = "Deletes a taxonomy.")]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, UriTemplate = "{taxonomyId}/?provider={provider}&lang={lang}&deleteTaxaOnly={deleteTaxaOnly}")]
    [OperationContract]
    bool DeleteTaxonomy(string taxonomyId, string provider, string lang, bool deleteTaxaOnly);

    /// <summary>
    /// Deletes a taxonomy and returns true if the taxonomy has been deleted; otherwise false.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="taxonomyId">Id of the taxonomy that ought to be deleted.</param>
    /// <param name="provider">Name of the taxonomy provider to be used when deleting the taxonomy.</param>
    /// <param name="lang">The language to be deleted. If null, all versions will be deleted</param>
    /// <param name="deleteTaxaOnly">Indicates whether to delete all the taxonomy taxa only or delete the entire taxonomy itself.</param>
    [WebHelp(Comment = "Deletes a taxonomy.")]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/{taxonomyId}/?provider={provider}&lang={lang}&deleteTaxaOnly={deleteTaxaOnly}")]
    [OperationContract]
    bool DeleteTaxonomyInXml(string taxonomyId, string provider, string lang, bool deleteTaxaOnly);

    /// <summary>
    /// Shares the taxonomy with sites.
    /// Returns the saved taxonomy object in JSON format.
    /// </summary>
    /// <param name="siteIds">The site ids.</param>
    /// <param name="taxonomyId">Id of the taxonomy under which taxonomy ought to be saved.</param>
    /// <param name="provider">Name of the taxonomy provider to be used when saving the taxonomy.</param>
    /// <returns>
    /// An instance of <see cref="T:Telerik.Sitefinity.Taxonomies.Web.Services.WcfTaxonomy" /> that represents the saved taxonomy object.
    /// </returns>
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "share/{taxonomyId}/?provider={provider}")]
    [OperationContract]
    void ShareTaxonomyWithSites(string[] siteIds, string taxonomyId, string provider);

    /// <summary>
    /// Shares the taxonomy with sites.
    /// Returns the saved taxonomy object in XML format.
    /// </summary>
    /// <param name="siteIds">The site ids.</param>
    /// <param name="taxonomyId">Id of the taxonomy under which taxonomy ought to be saved.</param>
    /// <param name="provider">Name of the taxonomy provider to be used when saving the taxonomy.</param>
    /// <returns>
    /// An instance of <see cref="T:Telerik.Sitefinity.Taxonomies.Web.Services.WcfTaxonomy" /> that represents the saved taxonomy object.
    /// </returns>
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/share/{taxonomyId}/?provider={provider}")]
    [OperationContract]
    void ShareTaxonomyWithSitesInXml(string[] siteIds, string taxonomyId, string provider);

    /// <summary>Shares the taxonomy with the current site.</summary>
    /// <param name="taxonomyId">Id of the taxonomy under which taxonomy ought to be saved.</param>
    /// <param name="provider">Name of the taxonomy provider to be used when saving the taxonomy.</param>
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "{taxonomyId}/share/?provider={provider}")]
    [OperationContract]
    void ShareTaxonomyWithCurrentSite(string taxonomyId, string provider);

    /// <summary>Shares the taxonomy with the current site.</summary>
    /// <param name="taxonomyId">Id of the taxonomy under which taxonomy ought to be saved.</param>
    /// <param name="provider">Name of the taxonomy provider to be used when saving the taxonomy.</param>
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/{taxonomyId}/share/?provider={provider}")]
    [OperationContract]
    void ShareTaxonomyWithCurrentInXml(string taxonomyId, string provider);

    /// <summary>
    /// Splits the taxonomy with site.
    /// Returns the saved taxonomy object in JSON format.
    /// </summary>
    /// <param name="taxonomyId">Id of the taxonomy under which taxonomy ought to be saved.</param>
    /// <param name="provider">Name of the taxonomy provider to be used when saving the taxonomy.</param>
    /// <param name="duplicate">Duplicate current taxonomy for this site.</param>
    /// <returns></returns>
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "{taxonomyId}/split/?provider={provider}&duplicate={duplicate}")]
    [OperationContract]
    WcfTaxonomy SplitTaxonomyWithSite(
      string taxonomyId,
      string provider,
      bool duplicate);

    /// <summary>
    /// Splits the taxonomy with site.
    /// Returns the saved taxonomy object in XML format.
    /// </summary>
    /// <param name="taxonomyId">Id of the taxonomy under which taxonomy ought to be saved.</param>
    /// <param name="provider">Name of the taxonomy provider to be used when saving the taxonomy.</param>
    /// <param name="duplicate">Duplicate current taxonomy for this site.</param>
    /// <returns></returns>
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/{taxonomyId}/split/?provider={provider}&duplicate={duplicate}")]
    [OperationContract]
    WcfTaxonomy SplitTaxonomyWithSiteInXml(
      string taxonomyId,
      string provider,
      bool duplicate);

    /// <summary>Gets the shared sites for taxonomy.</summary>
    /// <param name="rootTaxonomyId">The root taxonomy identifier.</param>
    /// <param name="provider">The provider.</param>
    /// <returns></returns>
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "{rootTaxonomyId}/sites/?provider={provider}")]
    [OperationContract]
    Dictionary<Guid, string> GetSitesForTaxonomy(
      string rootTaxonomyId,
      string provider);

    /// <summary>Gets the shared sites for taxonomy in XML.</summary>
    /// <param name="rootTaxonomyId">The root taxonomy identifier.</param>
    /// <param name="provider">The provider.</param>
    /// <returns></returns>
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/{rootTaxonomyId}/sites/?provider={provider}")]
    [OperationContract]
    Dictionary<Guid, string> GetSitesForTaxonomyInXml(
      string rootTaxonomyId,
      string provider);

    /// <summary>
    /// Deletes an array of content items.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="Ids">An array containing the Ids of the content items to be deleted.</param>
    /// <param name="itemsType">The actual type of the content items to be deleted.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the item.</param>
    /// <param name="deletedLanguage">The language version to delete. If null, all versions are deleted(e.g. the item itself is deleted).</param>
    /// <param name="deleteTaxaOnly">Indicates whether to delete all the taxonomy taxa only or delete the entire taxonomy itself.</param>
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "batch/?providerName={providerName}&itemType={itemType}&language={deletedLanguage}&deleteTaxaOnly={deleteTaxaOnly}")]
    bool BatchDeleteTaxonomy(
      string[] ids,
      string itemType,
      string providerName,
      string deletedLanguage,
      bool deleteTaxaOnly);

    /// <summary>
    /// Deletes an array of content items.
    /// Result is returned in Xml format.
    /// </summary>
    /// <param name="Ids">An array containing the Ids of the content items to be deleted.</param>
    /// <param name="itemsType">The actual type of the content items to be deleted.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the item.</param>
    /// <param name="deletedLanguage">The language version to delete. If null, all versions are deleted(e.g. the item itself is deleted).</param>
    /// <param name="deleteTaxaOnly">Indicates whether to delete all the taxonomy taxa only or delete the entire taxonomy itself.</param>
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/batch/?providerName={providerName}&itemType={itemType}&language={deletedLanguage}&deleteTaxaOnly={deleteTaxaOnly}")]
    bool BatchDeleteTaxonomyInXml(
      string[] ids,
      string itemType,
      string providerName,
      string deletedLanguage,
      bool deleteTaxaOnly);
  }
}
