// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Web.Services.IHierarchicalTaxonService
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
  /// The WCF web service interface for hierarchical taxonomy management.
  /// </summary>
  [ServiceContract]
  public interface IHierarchicalTaxonService
  {
    /// <summary>
    /// Gets the collection of hierarchical taxons and returns it in JSON format.
    /// </summary>
    /// <param name="taxonomyId">Id of the taxonomy from which the hierarchical taxons ought the be retrieved.</param>
    /// <param name="provider">Name of the provider used to retrieve the hierarchical taxons.</param>
    /// <param name="sortExpression">Sort expression used to order the taxons in the result set.</param>
    /// <param name="skip">Number of items to skip before starting to take items into the result set.</param>
    /// <param name="take">Number of items to take into the result set.</param>
    /// <param name="filter">Filter expression to be used to filter the items that will be considered for the result set.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="hierarchyMode">if set to <c>true</c> [hierarchy mode].</param>
    /// <param name="mode">The mode.</param>
    /// <param name="siteContextMode">Defines the mode which determines which taxonomy to use based on the site context.</param>
    /// <returns>
    /// An instance of CollectionContext that contains the result set of hierarchical taxons.
    /// </returns>
    [WebHelp(Comment = "Gets the collection of hierarchical taxons and returns it in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{taxonomyId}/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&itemType={itemType}&hierarchyMode={hierarchyMode}&mode={mode}&siteContextMode={siteContextMode}")]
    [OperationContract]
    CollectionContext<WcfHierarchicalTaxon> GetTaxa(
      string taxonomyId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string itemType,
      bool hierarchyMode,
      string mode,
      string siteContextMode = "currentSiteContext");

    /// <summary>
    /// Gets the collection of hierarchical taxons and returns it in XML format.
    /// </summary>
    /// <param name="taxonomyId">Id of the taxonomy from which the hiearchical taxons ought the be retrieved.</param>
    /// <param name="provider">Name of the provider used to retrieve the hierarchical taxons.</param>
    /// <param name="sortExpression">Sort expression used to order the taxons in the result set.</param>
    /// <param name="skip">Number of ite ms to skip before starting to take items into the result set.</param>
    /// <param name="take">Number of items to take into the result set.</param>
    /// <param name="filter">Filter expression to be used to filter the items that will be considerd for the result set.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="hierarchyMode">if set to <c>true</c> [hierarchy mode].</param>
    /// <param name="mode">The mode.</param>
    /// <param name="siteContextMode">Defines the mode which determines which taxonomy to use based on the site context.</param>
    /// <returns>
    /// An instance of CollectionContext that contains the result set of hierarchical taxons.
    /// </returns>
    [WebHelp(Comment = "Gets the collection of hierarchical taxons and returns it in XML format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/{taxonomyId}/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&itemType={itemType}&hierarchyMode={hierarchyMode}&mode={mode}&siteContextMode={siteContextMode}")]
    [OperationContract]
    CollectionContext<WcfHierarchicalTaxon> GetTaxaInXml(
      string taxonomyId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string itemType,
      bool hierarchyMode,
      string mode,
      string siteContextMode = "currentSiteContext");

    /// <summary>Gets path for the collection of taxons.</summary>
    /// <param name="taxonIds">The taxon ids.</param>
    /// <param name="provider">Name of the provider used to retrieve the hierarchical taxons.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <returns></returns>
    [WebHelp(Comment = "Gets the collection of hierarchical taxons and returns it in JSON format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "batchpath/?provider={provider}&itemType={itemType}")]
    [OperationContract]
    CollectionContext<WcfHierarchicalTaxon[]> BatchGetPath(
      string[] taxonIds,
      string provider,
      string itemType);

    /// <summary>
    /// Gets the sub taxa or children of a hierarchical taxon and returns the collection of these taxons in JSON format.
    /// </summary>
    /// <param name="taxonId">Id of the taxon for which the subtaxa ought to be retrieved.</param>
    /// <param name="provider">Name of the provider used to retrieve the subtaxa.</param>
    /// <param name="sortExpression">Sort expression used to order the taxons in the result set.</param>
    /// <param name="skip">Number of items to skip before starting to take items into the result set.</param>
    /// <param name="take">Number of items to take into the result set.</param>
    /// <param name="filter">Filter expression to be used to filter the items that will be considered for the result set.</param>
    /// <returns>An instance of CollectionContext that contains the result set of subtaxa.</returns>
    [WebHelp(Comment = "Gets the sub taxa or children of a hierarchical taxon and returns the collection of these taxons in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "subtaxa/{taxonId}/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&itemType={itemType}&mode={mode}")]
    [OperationContract]
    CollectionContext<WcfHierarchicalTaxon> GetSubTaxa(
      string taxonId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string itemType,
      string mode);

    /// <summary>
    /// Gets the sub taxa or children of a hierarchical taxon and returns the collection of these taxons in XML format.
    /// </summary>
    /// <param name="taxonId">Id of the taxon for which the subtaxa ought to be retrieved.</param>
    /// <param name="provider">Name of the provider used to retrieve the subtaxa.</param>
    /// <param name="sortExpression">Sort expression used to order the taxons in the result set.</param>
    /// <param name="skip">Number of items to skip before starting to take items into the result set.</param>
    /// <param name="take">Number of items to take into the result set.</param>
    /// <param name="filter">Filter expression to be used to filter the items that will be considered for the result set.</param>
    /// <returns>An instance of CollectionContext that contains the result set of subtaxa.</returns>
    [WebHelp(Comment = "Gets the sub taxa or children of a hierarchical taxon and returns the collection of these taxons in XML format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/subtaxa/{taxonId}/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&itemType={itemType}&mode={mode}")]
    [OperationContract]
    CollectionContext<WcfHierarchicalTaxon> GetSubTaxaInXml(
      string taxonId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string itemType,
      string mode);

    /// <summary>
    /// Gets the path of the hierarchical taxons up to the root taxon, starting with the designated taxon and returns a collection
    /// of hierarchical taxons in JSON format.
    /// </summary>
    /// <param name="taxonId">Id of the taxon for which the upstream hierarchy should be retrieved.</param>
    /// <param name="provider">Name of the taxonomy provider to be used to retrieved the upstream hierarchy.</param>
    /// <param name="sortExpression">Sort expression used to order the items in the result set.</param>
    /// <param name="skip">Number of items to skip before starting to take items into the result set.</param>
    /// <param name="take">Number ot items to take </param>
    /// <param name="filter"></param>
    /// <remarks>
    /// This method is a performance optimization approach to avoid multiple bindings when several levels of the tree need to be
    /// bound in order to display the current node which is buried deep in the hierarchy.
    /// </remarks>
    /// <returns>The upstream hierarchy of taxons starting from the designated taxon and all the way to the root taxon.</returns>
    [WebHelp(Comment = "Gets the path of the hierarchical taxons up to the root taxon, starting with the designated taxon and returns a collection of hierarchical taxons in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/predecessor/{taxonId}/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&onlyPath={onlyPath}&itemType={itemType}&mode={mode}")]
    [OperationContract]
    CollectionContext<WcfHierarchicalTaxon> GetPredecessorTaxa(
      string taxonId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      bool onlyPath,
      string itemType,
      string mode);

    /// <summary>
    /// Gets the path of the hierarchical taxons up to the root taxon, starting with the designated taxon and returns a collection
    /// of hierarchical taxons in XML format.
    /// </summary>
    /// <param name="taxonId">Id of the taxon for which the upstream hierarchy should be retrieved.</param>
    /// <param name="provider">Name of the taxonomy provider to be used to retrieve the upstream hierarchy.</param>
    /// <param name="sortExpression">Sort expression used to order the items in the result set.</param>
    /// <param name="skip">Number of items to skip before starting to take items into the result set.</param>
    /// <param name="take">Number ot items to take </param>
    /// <param name="filter"></param>
    /// <remarks>
    /// This method is a performance optimization approach to avoid multiple bindings when several levels of the tree need to be
    /// bound in order to display the current node which is buried deep in the hierarchy.
    /// </remarks>
    /// <returns>The upstream hierarchy of taxons starting from the designated taxon and all the way to the root taxon.</returns>
    [WebHelp(Comment = "Gets the path of the hierarchical taxons up to the root taxon, starting with the designated taxon and returns a collection of hierarchical taxons in XML format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/predecessor/{taxonId}/?provider={provider}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&onlyPath={onlyPath}&itemType={itemType}&mode={mode}")]
    [OperationContract]
    CollectionContext<WcfHierarchicalTaxon> GetPredecessorTaxaInXml(
      string taxonId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      bool onlyPath,
      string itemType,
      string mode);

    /// <summary>
    /// Gets the single hierarchical taxon and returns it in JSON format.
    /// </summary>
    /// <param name="taxonomyId">Id of the taxonomy to which the hierarchical taxon belongs to.</param>
    /// <param name="taxonId">Id of the taxon that ought to be retrieved.</param>
    /// <param name="provider">Name of the taxonomy provider to be used to retrieved the hierarchical taxon.</param>
    /// <returns>An instance of WcfHierarchicalTaxon representing the hierarchical taxon.</returns>
    [WebHelp(Comment = "Gets the single hierarchical taxon and returns it in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "{taxonomyId}/{taxonId}/?provider={provider}&ordinal={ordinal}&insertionPosition={insertionPosition}&itemType={itemType}")]
    [OperationContract]
    WcfHierarchicalTaxon GetTaxon(
      string taxonomyId,
      string taxonId,
      string provider,
      string ordinal,
      string insertionPosition,
      string itemType);

    /// <summary>
    /// Gets the single hierarchical taxon and returns it in XML format.
    /// </summary>
    /// <param name="taxonomyId">Id of the taxonomy to which the hierarchical taxon belongs to.</param>
    /// <param name="taxonId">Id of the taxon that ought to be retrieved.</param>
    /// <param name="provider">Name of the taxonomy provider to be used to retrieved the hierarchical taxon.</param>
    /// <returns>An instance of WcfHierarchicalTaxon representing the hierarchical taxon.</returns>
    [WebHelp(Comment = "Gets the single hierarchical taxon and returns it in XML format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/{taxonomyId}/{taxonId}/?provider={provider}&ordinal={ordinal}&insertionPosition={insertionPosition}&itemType={itemType}")]
    [OperationContract]
    WcfHierarchicalTaxon GetTaxonInXml(
      string taxonomyId,
      string taxonId,
      string provider,
      string ordinal,
      string insertionPosition,
      string itemType);

    /// <summary>
    /// Saves the hierarchical taxon and returns the saved taxon in JSON format. If the taxon with specified pageId
    /// exists the taxon will be updated; otherwise new taxon will be created.
    /// </summary>
    /// <param name="wcfTaxon">The taxon object to be saved.</param>
    /// <param name="taxonomyId">The pageId of the taxonomy taht the taxon to be saved belongs to.</param>
    /// <param name="taxonId">The pageId of the taxon to be saved.</param>
    /// <param name="provider">Name of the taxonomy provider to be used to save the taxon.</param>
    /// <param name="ordinal">The ordinal.</param>
    /// <param name="insertionPosition">The insertion position.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="skipSiteContext">The [skip site context] determines whether the taxonomy should be resolved in the current site context.</param>
    /// <returns>An instance of WcfHierarchicalTaxon that was saved.</returns>
    [WebHelp(Comment = "Saves the hierarchical taxon and returns the saved taxon in JSON format. If the taxon with specified id exists the taxon will be updated; otherwise new taxon will be created.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "{taxonomyId}/{taxonId}/?provider={provider}&ordinal={ordinal}&insertionPosition={insertionPosition}&itemType={itemType}&skipSiteContext={skipSiteContext}")]
    [OperationContract]
    WcfHierarchicalTaxon SaveTaxon(
      WcfHierarchicalTaxon wcfTaxon,
      string taxonomyId,
      string taxonId,
      string provider,
      string ordinal,
      string insertionPosition,
      string itemType,
      bool skipSiteContext);

    /// <summary>
    /// Saves the hierarchical taxon and returns the saved taxon in XML format. If the taxon with specified pageId
    /// exists the taxon will be updated; otherwise new taxon will be created.
    /// </summary>
    /// <param name="wcfTaxon">The taxon object to be saved.</param>
    /// <param name="taxonomyId">The pageId of the taxonomy taht the taxon to be saved belongs to.</param>
    /// <param name="taxonId">The pageId of the taxon to be saved.</param>
    /// <param name="provider">Name of the taxonomy provider to be used to save the taxon.</param>
    /// <param name="ordinal">The ordinal.</param>
    /// <param name="insertionPosition">The insertion position.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="skipSiteContext">The [skip site context] determines whether the taxonomy should be resolved in the current site context.</param>
    /// <returns>An instance of WcfHierarchicalTaxon that was saved.</returns>
    [WebHelp(Comment = "Saves the hierarchical taxon and returns the saved taxon in XML format. If the taxon with specified id exists the taxon will be updated; otherwise new taxon will be created.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/{taxonomyId}/{taxonId}/?provider={provider}&ordinal={ordinal}&insertionPosition={insertionPosition}&itemType={itemType}&skipSiteContext={skipSiteContext}")]
    [OperationContract]
    WcfHierarchicalTaxon SaveTaxonInXml(
      WcfHierarchicalTaxon wcfTaxon,
      string taxonomyId,
      string taxonId,
      string provider,
      string ordinal,
      string insertionPosition,
      string itemType,
      bool skipSiteContext);

    /// <summary>
    /// Moves a single taxon one place up in the level to which the taxon belongs to.
    /// </summary>
    /// <param name="taxonomyId">Id of the taxonomy to which the taxon that ought to be moved up belongs to.</param>
    /// <param name="taxonId">Id of the taxon that ought to be moved up.</param>
    /// <param name="provider">Name of the taxonomy provider to be used to move the taxon up.</param>
    [WebHelp(Comment = "Moves a single taxon one place up in the level to which the taxon belongs to.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "moveup/{taxonomyId}/{taxonId}/?provider={provider}")]
    [OperationContract]
    void MoveTaxonUp(string taxonomyId, string taxonId, string provider);

    /// <summary>
    /// Moves a single taxon one place down in the level to which the taxon belongs to.
    /// </summary>
    /// <param name="taxonomyId">Id of the taxonomy to which the taxon that ought to be moved belongs to.</param>
    /// <param name="taxonId">Id of the taxon that ought to be moved down.</param>
    /// <param name="provider">Name of the taxonomy provider to be used to move the taxon down.</param>
    [WebHelp(Comment = "Moves a single taxon one place down in the level to which the taxon belongs to.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "movedown/{taxonomyId}/{taxonId}/?provider={provider}")]
    [OperationContract]
    void MoveTaxonDown(string taxonomyId, string taxonId, string provider);

    /// <summary>Changes a parent taxon of a single taxon.</summary>
    /// <param name="taxonomyId">Id of the taxonomy to which the taxon that ought to be moved belongs to.</param>
    /// <param name="taxonId">Id of the taxon for which the parent should be changed.</param>
    /// <param name="newParentId">Id of the taxon that is the new parent of the taxon.</param>
    /// <param name="provider">Name of the taxonomy provider to be used to change the parent of the taxon.</param>
    [WebHelp(Comment = "Changes a parent taxon of a single taxon.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "changeparent/{taxonomyId}/{taxonId}/?provider={provider}")]
    [OperationContract]
    void ChangeParent(string taxonomyId, string taxonId, string newParentId, string provider);

    /// <summary>
    /// Moves the collection of taxons up - each taxon for one place up in their current level.
    /// </summary>
    /// <param name="taxonomyId">Id of the taxonomy to which the taxons that ought to be moved belong to.</param>
    /// <param name="provider">Name of the taxonomy provider to be used to move the taxons down.</param>
    /// <param name="taxonIds">Ids of the taxons that should be moved up by one place.</param>
    [WebHelp(Comment = "Moves the collection of taxons up - each taxon for one place up in their current level.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "batchmoveup/{taxonomyId}/?provider={provider}")]
    [OperationContract]
    void BatchMoveTaxonsUp(string[] taxonIds, string taxonomyId, string provider);

    /// <summary>
    /// Moves the collection of taxons down - each taxon for one place down in their current level.
    /// </summary>
    /// <param name="taxonomyId">Id of the taxonomy to which the taxons that ought to be moved belong to.</param>
    /// <param name="provider">Name of the taxonomy provider to be used to moved the taxons down.</param>
    /// <param name="taxonIds">Ids of the taxons that should be moved down by one place.</param>
    [WebHelp(Comment = "Moves the collection of taxons down - each taxon for one place down in their current level.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "batchmovedown/{taxonomyId}/?provider={provider}")]
    [OperationContract]
    void BatchMoveTaxonsDown(string[] taxonIds, string taxonomyId, string provider);

    /// <summary>Changes the parent for the collection of taxons.</summary>
    /// <param name="taxonomyId">Id of the taxonomy to which the taxons that are changing parent belong to.</param>
    /// <param name="parentChangeObject">Object which contains the information about the change that should take place.</param>
    /// <param name="provider">Name of the taxonomy provider to be used to change the taxons' parent.</param>
    [WebHelp(Comment = "Changes the parent for the collection of taxons.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "batchchangeparent/{taxonomyId}/?provider={provider}")]
    [OperationContract]
    void BatchChangeParent(
      string taxonomyId,
      WcfBatchChangeParent parentChangeObject,
      string provider);

    /// <summary>
    /// Deletes a hierarchical taxon and returns true if the taxon has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="taxonomyId">Id of the taxonomy to which the taxon that ought to be deleted belongs to.</param>
    /// <param name="taxonId">Id of the taxon to be deleted.</param>
    /// <param name="provider">Name of the taxonomy provider to be used to delete the taxon.</param>
    /// <param name="lang">The language to be deleted. If null, all versions will be deleted</param>
    [WebHelp(Comment = "Deletes a hierarchical taxon.")]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, UriTemplate = "{taxonomyId}/{taxonId}/?provider={provider}&ordinal={ordinal}&insertionPosition={insertionPosition}&itemType={itemType}&lang={lang}")]
    [OperationContract]
    bool DeleteTaxon(
      string taxonomyId,
      string taxonId,
      string provider,
      string ordinal,
      string insertionPosition,
      string itemType,
      string lang);

    /// <summary>
    /// Deletes a hierarchical taxon and returns true if the hierarchical taxon has been deleted; otherwise false.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="taxonomyId">Id of the taxonomy to which the taxon that ought to be deleted belongs to.</param>
    /// <param name="taxonId">Id of the taxon to be deleted.</param>
    /// <param name="provider">Name of the taxonomy provider to be used to delete the taxon.</param>
    /// <param name="lang">The language to be deleted. If null, all versions will be deleted</param>
    [WebHelp(Comment = "Deletes a hierarchical taxon.")]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/{taxonomyId}/{taxonId}/?provider={provider}&ordinal={ordinal}&insertionPosition={insertionPosition}&itemType={itemType}&lang={lang}")]
    [OperationContract]
    bool DeleteTaxonInXml(
      string taxonomyId,
      string taxonId,
      string provider,
      string ordinal,
      string insertionPosition,
      string itemType,
      string lang);

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
    bool BatchDeleteTaxons(string[] taxonIds, string taxonomyId, string provider, string language);

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
    bool BatchDeleteTaxonsInXml(
      string[] taxonIds,
      string taxonomyId,
      string provider,
      string language);
  }
}
