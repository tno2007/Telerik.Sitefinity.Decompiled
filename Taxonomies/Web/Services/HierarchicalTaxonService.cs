// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Web.Services.HierarchicalTaxonService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Threading;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Taxonomies.Web.Services.Common;
using Telerik.Sitefinity.Utilities;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Taxonomies.Web.Services
{
  /// <summary>
  /// WCF service for working with hierarchical taxon objects.
  /// </summary>
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  public class HierarchicalTaxonService : IHierarchicalTaxonService
  {
    private static object writeLock = new object();

    /// <summary>
    /// Gets the collection of hierarchical taxons and returns it in JSON format.
    /// </summary>
    /// <param name="taxonomyId">Id of the taxonomy from which the hiearchical taxons ought the be retrieved.</param>
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
    public CollectionContext<WcfHierarchicalTaxon> GetTaxa(
      string taxonomyId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string itemType,
      bool hierarchyMode,
      string mode,
      string siteContextMode = "currentSiteContext")
    {
      return this.GetTaxaInternal(taxonomyId, provider, sortExpression, skip, take, filter, itemType, hierarchyMode, mode, siteContextMode);
    }

    /// <summary>
    /// Gets the collection of hierarchical taxons and returns it in XML format.
    /// </summary>
    /// <param name="taxonomyId">Id of the taxonomy from which the hiearchical taxons ought the be retrieved.</param>
    /// <param name="provider">Name of the provider used to retrieve the hierarchical taxons.</param>
    /// <param name="sortExpression">Sort expression used to order the taxons in the result set.</param>
    /// <param name="skip">Number of items to skip before starting to take items into the result set.</param>
    /// <param name="take">Number of items to take into the result set.</param>
    /// <param name="filter">Filter expression to be used to filter the items that will be considerd for the result set.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="hierarchyMode">if set to <c>true</c> [hierarchy mode].</param>
    /// <param name="mode">The mode.</param>
    /// <param name="siteContextMode">Defines the mode which determines which taxonomy to use based on the site context.</param>
    /// <returns>
    /// An instance of CollectionContext that contains the result set of hierarchical taxons.
    /// </returns>
    public CollectionContext<WcfHierarchicalTaxon> GetTaxaInXml(
      string taxonomyId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string itemType,
      bool hierarchyMode,
      string mode,
      string siteContextMode = "currentSiteContext")
    {
      return this.GetTaxaInternal(taxonomyId, provider, sortExpression, skip, take, filter, itemType, hierarchyMode, mode, siteContextMode);
    }

    /// <summary>Gets path for the collection of taxons.</summary>
    /// <param name="taxonIds">The taxon ids.</param>
    /// <param name="provider">Name of the provider used to retrieve the hierarchical taxons.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <returns></returns>
    public CollectionContext<WcfHierarchicalTaxon[]> BatchGetPath(
      string[] taxonIds,
      string provider,
      string itemType)
    {
      return this.BatchGetPathInternal(taxonIds, provider, itemType);
    }

    /// <summary>
    /// Gets the sub taxa or children of a hierarchical taxon and returns the collection of these taxons in JSON format.
    /// </summary>
    /// <param name="taxonId">Id of the taxon for which the subtaxa ought to be retrieved.</param>
    /// <param name="provider">Name of the provider used to retrieve the subtaxa.</param>
    /// <param name="sortExpression">Sort expression used to order the taxons in the result set.</param>
    /// <param name="skip">Number of items to skip before starting to take items into the result set.</param>
    /// <param name="take">Number of items to take into the result set.</param>
    /// <param name="filter">Filter expression to be used to filter the items that will be considered for the result set.</param>
    /// <returns>
    /// An instance of CollectionContext that contains the result set of subtaxa.
    /// </returns>
    public CollectionContext<WcfHierarchicalTaxon> GetSubTaxa(
      string taxonId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string itemType,
      string mode)
    {
      return this.GetSubTaxaInternal(taxonId, provider, sortExpression, skip, take, filter, itemType, mode);
    }

    /// <summary>
    /// Gets the sub taxa or children of a hierarchical taxon and returns the collection of these taxons in XML format.
    /// </summary>
    /// <param name="taxonId">Id of the taxon for which the subtaxa ought to be retrieved.</param>
    /// <param name="provider">Name of the provider used to retrieve the subtaxa.</param>
    /// <param name="sortExpression">Sort expression used to order the taxons in the result set.</param>
    /// <param name="skip">Number of items to skip before starting to take items into the result set.</param>
    /// <param name="take">Number of items to take into the result set.</param>
    /// <param name="filter">Filter expression to be used to filter the items that will be considered for the result set.</param>
    /// <returns>
    /// An instance of CollectionContext that contains the result set of subtaxa.
    /// </returns>
    public CollectionContext<WcfHierarchicalTaxon> GetSubTaxaInXml(
      string taxonId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string itemType,
      string mode)
    {
      return this.GetSubTaxaInternal(taxonId, provider, sortExpression, skip, take, filter, itemType, mode);
    }

    /// <summary>
    /// Gets the path of the hierarchical taxons up to the root taxon, starting with the designated taxon and returns a collection
    /// of hierarchical taxons in JSON format.
    /// </summary>
    /// <param name="taxonId">Id of the taxon for which the upstream hierarchy should be retrieved.</param>
    /// <param name="provider">Name of the taxonomy provider to be used to retrieved the upstream hierarchy.</param>
    /// <param name="sortExpression">Sort expression used to order the items in the result set.</param>
    /// <param name="skip">Number of items to skip before starting to take items into the result set.</param>
    /// <param name="take">Number ot items to take</param>
    /// <param name="filter"></param>
    /// <returns>
    /// The upstream hierarchy of taxons starting from the designated taxon and all the way to the root taxon.
    /// </returns>
    /// <remarks>
    /// This method is a performance optimization approach to avoid multiple bindings when several levels of the tree need to be
    /// bound in order to display the current node which is buried deep in the hierarchy.
    /// </remarks>
    public CollectionContext<WcfHierarchicalTaxon> GetPredecessorTaxa(
      string taxonId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      bool onlyPath,
      string itemType,
      string mode)
    {
      return this.GetPredecessorTaxaInternal(taxonId, provider, sortExpression, skip, take, filter, onlyPath, itemType, mode);
    }

    /// <summary>
    /// Gets the path of the hierarchical taxons up to the root taxon, starting with the designated taxon and returns a collection
    /// of hierarchical taxons in XML format.
    /// </summary>
    /// <param name="taxonId">Id of the taxon for which the upstream hierarchy should be retrieved.</param>
    /// <param name="provider">Name of the taxonomy provider to be used to retrieve the upstream hierarchy.</param>
    /// <param name="sortExpression">Sort expression used to order the items in the result set.</param>
    /// <param name="skip">Number of items to skip before starting to take items into the result set.</param>
    /// <param name="take">Number ot items to take</param>
    /// <param name="filter"></param>
    /// <returns>
    /// The upstream hierarchy of taxons starting from the designated taxon and all the way to the root taxon.
    /// </returns>
    /// <remarks>
    /// This method is a performance optimization approach to avoid multiple bindings when several levels of the tree need to be
    /// bound in order to display the current node which is buried deep in the hierarchy.
    /// </remarks>
    public CollectionContext<WcfHierarchicalTaxon> GetPredecessorTaxaInXml(
      string taxonId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      bool onlyPath,
      string itemType,
      string mode)
    {
      return this.GetPredecessorTaxaInternal(taxonId, provider, sortExpression, skip, take, filter, onlyPath, itemType, mode);
    }

    /// <summary>
    /// Gets the single hierarchical taxon and returns it in JSON format.
    /// </summary>
    /// <param name="taxonomyId">Id of the taxonomy to which the hierarchical taxon belongs to.</param>
    /// <param name="taxonId">Id of the taxon that ought to be retrieved.</param>
    /// <param name="provider">Name of the taxonomy provider to be used to retrieved the hierarchical taxon.</param>
    /// <returns>
    /// An instance of WcfHierarchicalTaxon representing the hierarchical taxon.
    /// </returns>
    public WcfHierarchicalTaxon GetTaxon(
      string taxonomyId,
      string taxonId,
      string provider,
      string ordinal,
      string insertionPosition,
      string itemType)
    {
      return this.GetTaxonInternal(taxonId, provider, itemType);
    }

    /// <summary>
    /// Gets the single hierarchical taxon and returns it in XML format.
    /// </summary>
    /// <param name="taxonomyId">Id of the taxonomy to which the hierarchical taxon belongs to.</param>
    /// <param name="taxonId">Id of the taxon that ought to be retrieved.</param>
    /// <param name="provider">Name of the taxonomy provider to be used to retrieved the hierarchical taxon.</param>
    /// <returns>
    /// An instance of WcfHierarchicalTaxon representing the hierarchical taxon.
    /// </returns>
    public WcfHierarchicalTaxon GetTaxonInXml(
      string taxonomyId,
      string taxonId,
      string provider,
      string ordinal,
      string insertionPosition,
      string itemType)
    {
      return this.GetTaxonInternal(taxonId, provider, itemType);
    }

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
    /// <param name="skipSiteContext">The [skip site context] determines whether the taxonomy should be resolved in the current context.</param>
    /// <returns>An instance of WcfHierarchicalTaxon that was saved.</returns>
    public WcfHierarchicalTaxon SaveTaxon(
      WcfHierarchicalTaxon wcfTaxon,
      string taxonomyId,
      string taxonId,
      string provider,
      string ordinal,
      string insertionPosition,
      string itemType,
      bool skipSiteContext)
    {
      return this.SaveTaxonInternal(wcfTaxon, taxonomyId, taxonId, provider, ordinal, insertionPosition, skipSiteContext);
    }

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
    public WcfHierarchicalTaxon SaveTaxonInXml(
      WcfHierarchicalTaxon wcfTaxon,
      string taxonomyId,
      string taxonId,
      string provider,
      string ordinal,
      string insertionPosition,
      string itemType,
      bool skipSiteContext)
    {
      return this.SaveTaxonInternal(wcfTaxon, taxonomyId, taxonId, provider, ordinal, insertionPosition, skipSiteContext);
    }

    /// <summary>
    /// Moves a single taxon one place up in the level to which the taxon belongs to.
    /// </summary>
    /// <param name="taxonomyId">Id of the taxonomy to which the taxon that ought to be moved up belongs to.</param>
    /// <param name="taxonId">Id of the taxon that ought to be moved up.</param>
    /// <param name="provider">Name of the taxonomy provider to be used to move the taxon up.</param>
    public void MoveTaxonUp(string taxonomyId, string taxonId, string provider) => this.MoveTaxonInternal(taxonId, provider, MovingDirection.Up);

    /// <summary>
    /// Moves a single taxon one place down in the level to which the taxon belongs to.
    /// </summary>
    /// <param name="taxonomyId">Id of the taxonomy to which the taxon that ought to be moved belongs to.</param>
    /// <param name="taxonId">Id of the taxon that ought to be moved down.</param>
    /// <param name="provider">Name of the taxonomy provider to be used to move the taxon down.</param>
    public void MoveTaxonDown(string taxonomyId, string taxonId, string provider) => this.MoveTaxonInternal(taxonId, provider, MovingDirection.Down);

    /// <summary>Changes a parent taxon of a single taxon.</summary>
    /// <param name="taxonomyId">Id of the taxonomy to which the taxon that ought to be moved belongs to.</param>
    /// <param name="taxonId">Id of the taxon for which the parent should be changed.</param>
    /// <param name="newParentId">Id of the taxon that is the new parent of the taxon.</param>
    /// <param name="provider">Name of the taxonomy provider to be used to change the parent of the taxon.</param>
    public void ChangeParent(
      string taxonomyId,
      string taxonId,
      string newParentId,
      string provider)
    {
      this.ChangeParentInternal(taxonId, newParentId, provider, true);
    }

    /// <summary>
    /// Moves the collection of taxons up - each taxon for one place up in their current level.
    /// </summary>
    /// <param name="taxonomyId">Id of the taxonomy to which the taxons that ought to be moved belong to.</param>
    /// <param name="provider">Name of the taxonomy provider to be used to move the taxons down.</param>
    /// <param name="taxonIds">Ids of the taxons that should be moved up by one place.</param>
    public void BatchMoveTaxonsUp(string[] taxonIds, string taxonomyId, string provider) => this.BatchMoveTaxonsInternal(provider, taxonIds, MovingDirection.Up);

    /// <summary>
    /// Moves the collection of taxons down - each taxon for one place down in their current level.
    /// </summary>
    /// <param name="taxonomyId">Id of the taxonomy to which the taxons that ought to be moved belong to.</param>
    /// <param name="provider">Name of the taxonomy provider to be used to moved the taxons down.</param>
    /// <param name="taxonIds">Ids of the taxons that should be moved down by one place.</param>
    public void BatchMoveTaxonsDown(string[] taxonIds, string taxonomyId, string provider) => this.BatchMoveTaxonsInternal(provider, taxonIds, MovingDirection.Down);

    /// <summary>Changes the parent for the collection of taxons.</summary>
    /// <param name="taxonomyId">Id of the taxonomy to which the taxons that are changing parent belong to.</param>
    /// <param name="parentChangeObject">Object which contains the information about the change that should take place.</param>
    /// <param name="provider">Name of the taxonomy provider to be used to change the taxons' parent.</param>
    public void BatchChangeParent(
      string taxonomyId,
      WcfBatchChangeParent parentChangeObject,
      string provider)
    {
      this.BatchChangeParentInternal(provider, parentChangeObject);
    }

    /// <summary>Deletes a hierarchical taxon.</summary>
    /// <param name="taxonomyId">Id of the taxonomy to which the taxon that ought to be deleted belongs to.</param>
    /// <param name="taxonId">Id of the taxon to be deleted.</param>
    /// <param name="provider">Name of the taxonomy provider to be used to delete the taxon.</param>
    /// <param name="lang">The language to be deleted. If null, all versions will be deleted</param>
    public bool DeleteTaxon(
      string taxonomyId,
      string taxonId,
      string provider,
      string ordinal,
      string insertionPosition,
      string itemType,
      string lang)
    {
      return this.DeleteTaxonInternal(taxonId, provider, lang);
    }

    /// <summary>
    /// Deletes a hierarchical taxon and returns true if the taxon has been deleted; otherwise false.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="taxonomyId">Id of the taxonomy to which the taxon that ought to be deleted belongs to.</param>
    /// <param name="taxonId">Id of the taxon to be deleted.</param>
    /// <param name="provider">Name of the taxonomy provider to be used to delete the taxon.</param>
    /// <param name="lang">The language to be deleted. If null, all versions will be deleted</param>
    public bool DeleteTaxonInXml(
      string taxonomyId,
      string taxonId,
      string provider,
      string ordinal,
      string insertionPosition,
      string itemType,
      string lang)
    {
      return this.DeleteTaxonInternal(taxonId, provider, lang);
    }

    /// <summary>
    /// Deletes a list of flat taxons.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="taxonIds">An array of the ids of the taxons to delete.</param>
    /// <param name="taxonomyId">The taxonomy pageId.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="lang">The language to be deleted. If null, all versions will be deleted</param>
    /// <returns>
    /// true if all flat taxons have been deleted; otherwise false.
    /// </returns>
    public bool BatchDeleteTaxons(
      string[] taxonIds,
      string taxonomyId,
      string provider,
      string lang)
    {
      return this.BatchDeleteTaxonsInternal(taxonIds, provider, lang);
    }

    /// <summary>
    /// Deletes a list of hierarchical taxons and returns failed ids if exists.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="taxonIds">An array of the ids of the taxons to delete.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="lang">The language to be deleted. If null, all versions will be deleted</param>
    /// <returns>Item ids which failed to delete.</returns>
    internal Guid[] BatchDeleteTaxonsWithResult(
      string[] taxonIds,
      string provider,
      string lang)
    {
      return this.BatchDeleteTaxonsWithResultInternal(taxonIds, provider, lang);
    }

    /// <summary>
    /// Deletes a list of flat taxons.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="taxonIds">An array of the ids of the taxons to delete.</param>
    /// <param name="taxonomyId">The taxonomy pageId.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="lang">The language to be deleted. If null, all versions will be deleted</param>
    /// <returns>
    /// true if all flat taxons have been deleted; otherwise false.
    /// </returns>
    public bool BatchDeleteTaxonsInXml(
      string[] taxonIds,
      string taxonomyId,
      string provider,
      string lang)
    {
      return this.BatchDeleteTaxonsInternal(taxonIds, provider, lang);
    }

    private CollectionContext<WcfHierarchicalTaxon> GetTaxaInternal(
      string taxonomyId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string itemType,
      bool hierarchyMode,
      string mode,
      string siteContextMode)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      Guid taxonomyId1 = new Guid(taxonomyId);
      TaxonomyManager manager = TaxonomyManager.GetManager(provider);
      SiteContextMode siteContextMode1 = TaxonServiceHelper.GetSiteContextMode(siteContextMode);
      IQueryable<HierarchicalTaxon> queryable1 = TaxonServiceHelper.GetSiteContextTaxa<HierarchicalTaxon>(manager, siteContextMode1, taxonomyId1);
      WcTaxonDataFlags flags = TaxonServiceHelper.GetFlags(mode);
      if ((flags & WcTaxonDataFlags.SetSynonyms) == WcTaxonDataFlags.SetSynonyms)
        queryable1 = queryable1.Include<HierarchicalTaxon>((Expression<Func<HierarchicalTaxon, object>>) (x => x.Synonyms));
      if (hierarchyMode)
        queryable1 = queryable1.Where<HierarchicalTaxon>((Expression<Func<HierarchicalTaxon, bool>>) (t => t.Parent == default (object)));
      int? totalCount = new int?(0);
      if (skip == 0 && take == 0)
        totalCount = new int?();
      string orderExpression = sortExpression;
      if (string.IsNullOrEmpty(orderExpression))
        orderExpression = "LastModified ASC";
      IQueryable<HierarchicalTaxon> queryable2 = DataProviderBase.SetExpressions<HierarchicalTaxon>(queryable1, filter, orderExpression, new int?(skip), new int?(take), ref totalCount).Where<HierarchicalTaxon>((Expression<Func<HierarchicalTaxon, bool>>) (x => (int) x.Status == 0));
      IDictionary<string, HierarchyLevelState> dictionary = (IDictionary<string, HierarchyLevelState>) null;
      if (take != 0)
      {
        dictionary = (IDictionary<string, HierarchyLevelState>) new Dictionary<string, HierarchyLevelState>();
        dictionary[Guid.Empty.ToString()] = new HierarchyLevelState()
        {
          Skip = skip,
          Take = take,
          Total = totalCount.Value
        };
      }
      List<WcfHierarchicalTaxon> hierarchicalTaxonList = new List<WcfHierarchicalTaxon>();
      Type dataItemType = (Type) null;
      if (!string.IsNullOrEmpty(itemType))
        dataItemType = TypeResolutionService.ResolveType(itemType);
      foreach (HierarchicalTaxon hierarchicalTaxon in (IEnumerable<HierarchicalTaxon>) queryable2)
      {
        WcfHierarchicalTaxon wcfTaxon = this.CreateWcfTaxon(manager, hierarchicalTaxon, flags, dataItemType);
        hierarchicalTaxonList.Add(wcfTaxon);
      }
      if (!string.IsNullOrEmpty(sortExpression))
        hierarchicalTaxonList = hierarchicalTaxonList.AsQueryable<WcfHierarchicalTaxon>().OrderBy<WcfHierarchicalTaxon>(sortExpression, (object) StringComparison.OrdinalIgnoreCase).ToList<WcfHierarchicalTaxon>();
      if (hierarchyMode)
        HierarchicalTaxonService.SetTaxaHasChildren(manager, (IEnumerable<WcfHierarchicalTaxon>) hierarchicalTaxonList);
      if (!totalCount.HasValue)
        totalCount = new int?(hierarchicalTaxonList.Count);
      ServiceUtility.DisableCache();
      TaxonomyCollectionContext taxaInternal = new TaxonomyCollectionContext((IEnumerable<WcfHierarchicalTaxon>) hierarchicalTaxonList);
      taxaInternal.TotalCount = totalCount.Value;
      taxaInternal.TaxonomyContext = dictionary;
      return (CollectionContext<WcfHierarchicalTaxon>) taxaInternal;
    }

    public static void SetTaxaHasChildren(
      TaxonomyManager manager,
      IEnumerable<WcfHierarchicalTaxon> taxa)
    {
      IEnumerable<Guid> ids = taxa.Select<WcfHierarchicalTaxon, Guid>((Func<WcfHierarchicalTaxon, Guid>) (t => t.Id));
      IDictionary<Guid, int> taxonsChildrenCounts = HierarchicalTaxonService.GetTaxonsChildrenCounts(manager, ids);
      foreach (WcfHierarchicalTaxon taxon in taxa)
      {
        int num;
        if (taxonsChildrenCounts.TryGetValue(taxon.Id, out num))
          taxon.HasChildren = num > 0;
      }
    }

    public static IDictionary<Guid, int> GetTaxonsChildrenCounts(
      TaxonomyManager manager,
      IEnumerable<Guid> ids,
      int batchSize = 50)
    {
      if (!ids.Any<Guid>())
        return (IDictionary<Guid, int>) null;
      Dictionary<Guid, int> taxonsChildrenCounts = new Dictionary<Guid, int>();
      foreach (Guid[] guidArray in ids.OnBatchesOf<Guid>(batchSize))
      {
        Guid[] batchIds = guidArray;
        IQueryable<HierarchicalTaxon> source1 = manager.GetTaxa<HierarchicalTaxon>().Where<HierarchicalTaxon>((Expression<Func<HierarchicalTaxon, bool>>) (t => batchIds.Contains<Guid>(t.Parent.Id)));
        Expression<Func<HierarchicalTaxon, \u003C\u003Ef__AnonymousType68<Guid, Guid>>> selector = t => new
        {
          TaxonID = t.Id,
          ParentTaxonID = t.Parent.Id
        };
        foreach (IGrouping<Guid, \u003C\u003Ef__AnonymousType68<Guid, Guid>> source2 in source1.Select(selector).ToList().GroupBy(t => t.ParentTaxonID))
          taxonsChildrenCounts.Add(source2.Key, source2.Count());
      }
      return (IDictionary<Guid, int>) taxonsChildrenCounts;
    }

    private CollectionContext<WcfHierarchicalTaxon[]> BatchGetPathInternal(
      string[] taxonIds,
      string provider,
      string itemType)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      TaxonomyManager manager = TaxonomyManager.GetManager(provider);
      int? nullable1 = new int?(0);
      List<WcfHierarchicalTaxon[]> items = new List<WcfHierarchicalTaxon[]>();
      Type dataItemType = (Type) null;
      if (!string.IsNullOrEmpty(itemType))
        dataItemType = Type.GetType(itemType);
      List<Guid> guidIds = ((IEnumerable<string>) taxonIds).Select<string, Guid>((Func<string, Guid>) (x => Guid.Parse(x))).ToList<Guid>();
      if (guidIds.Count > 0)
      {
        IQueryable<HierarchicalTaxon> queryable = manager.GetTaxa<HierarchicalTaxon>().Where<HierarchicalTaxon>((Expression<Func<HierarchicalTaxon, bool>>) (x => guidIds.Contains(x.Id))).Include<HierarchicalTaxon>((Expression<Func<HierarchicalTaxon, object>>) (x => x.Parent)).Include<HierarchicalTaxon>((Expression<Func<HierarchicalTaxon, object>>) (x => x.Attributes)).Include<HierarchicalTaxon>((Expression<Func<HierarchicalTaxon, object>>) (x => x.Taxonomy));
        Dictionary<Guid, int> dictionary = manager.GetTaxa<HierarchicalTaxon>().Where<HierarchicalTaxon>((Expression<Func<HierarchicalTaxon, bool>>) (x => guidIds.Contains(x.Id))).Select(x => new
        {
          TaxaId = x.Id,
          ChildrenCount = x.Subtaxa.Count
        }).ToDictionary(x => x.TaxaId, y => y.ChildrenCount);
        foreach (HierarchicalTaxon taxon in (IEnumerable<HierarchicalTaxon>) queryable)
        {
          List<HierarchicalTaxon> taxonPath = new List<HierarchicalTaxon>();
          this.ConstructPath(taxon, taxonPath);
          List<WcfHierarchicalTaxon> hierarchicalTaxonList = new List<WcfHierarchicalTaxon>(taxonPath.Count);
          foreach (HierarchicalTaxon hierarchicalTaxon in taxonPath)
          {
            WcfHierarchicalTaxon wcfTaxon = this.CreateWcfTaxon(manager, hierarchicalTaxon, WcTaxonDataFlags.None, dataItemType);
            int num = 0;
            if (dictionary.TryGetValue(taxon.Id, out num))
              wcfTaxon.HasChildren = num > 0;
            hierarchicalTaxonList.Add(wcfTaxon);
          }
          int? nullable2 = nullable1;
          nullable1 = nullable2.HasValue ? new int?(nullable2.GetValueOrDefault() + 1) : new int?();
          items.Add(hierarchicalTaxonList.ToArray());
        }
      }
      ServiceUtility.DisableCache();
      return new CollectionContext<WcfHierarchicalTaxon[]>((IEnumerable<WcfHierarchicalTaxon[]>) items)
      {
        TotalCount = nullable1.Value
      };
    }

    private CollectionContext<WcfHierarchicalTaxon> GetSubTaxaInternal(
      string taxonId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string itemType,
      string mode)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      Guid tId = new Guid(taxonId);
      TaxonomyManager manager = TaxonomyManager.GetManager(provider);
      int? nullable = new int?(0);
      IQueryable<HierarchicalTaxon> query = manager.GetTaxa<HierarchicalTaxon>().Where<HierarchicalTaxon>((Expression<Func<HierarchicalTaxon, bool>>) (t => t.Parent.Id == tId));
      string str = sortExpression;
      if (string.IsNullOrEmpty(str))
        str = "LastModified ASC";
      string filterExpression = filter;
      string orderExpression = str;
      int? skip1 = new int?(skip);
      int? take1 = new int?(take);
      ref int? local = ref nullable;
      IQueryable<HierarchicalTaxon> queryable = DataProviderBase.SetExpressions<HierarchicalTaxon>(query, filterExpression, orderExpression, skip1, take1, ref local);
      IDictionary<string, HierarchyLevelState> dictionary = (IDictionary<string, HierarchyLevelState>) null;
      List<WcfHierarchicalTaxon> hierarchicalTaxonList = new List<WcfHierarchicalTaxon>();
      Type dataItemType = (Type) null;
      if (!string.IsNullOrEmpty(itemType))
        dataItemType = TypeResolutionService.ResolveType(itemType);
      WcTaxonDataFlags flags = TaxonServiceHelper.GetFlags(mode);
      foreach (HierarchicalTaxon hierarchicalTaxon in (IEnumerable<HierarchicalTaxon>) queryable)
      {
        WcfHierarchicalTaxon wcfTaxon = this.CreateWcfTaxon(manager, hierarchicalTaxon, flags, dataItemType);
        hierarchicalTaxonList.Add(wcfTaxon);
      }
      if (!string.IsNullOrEmpty(sortExpression))
        hierarchicalTaxonList = hierarchicalTaxonList.AsQueryable<WcfHierarchicalTaxon>().OrderBy<WcfHierarchicalTaxon>(sortExpression, (object) StringComparison.OrdinalIgnoreCase).ToList<WcfHierarchicalTaxon>();
      HierarchicalTaxonService.SetTaxaHasChildren(manager, (IEnumerable<WcfHierarchicalTaxon>) hierarchicalTaxonList);
      if (take != 0)
      {
        if (dictionary == null)
          dictionary = (IDictionary<string, HierarchyLevelState>) new Dictionary<string, HierarchyLevelState>();
        dictionary[taxonId] = new HierarchyLevelState()
        {
          Skip = skip,
          Take = take,
          Total = nullable.Value
        };
      }
      ServiceUtility.DisableCache();
      if (dictionary != null)
      {
        TaxonomyCollectionContext subTaxaInternal = new TaxonomyCollectionContext((IEnumerable<WcfHierarchicalTaxon>) hierarchicalTaxonList);
        subTaxaInternal.TotalCount = nullable.HasValue ? nullable.Value : 0;
        subTaxaInternal.TaxonomyContext = dictionary;
        return (CollectionContext<WcfHierarchicalTaxon>) subTaxaInternal;
      }
      return new CollectionContext<WcfHierarchicalTaxon>((IEnumerable<WcfHierarchicalTaxon>) hierarchicalTaxonList)
      {
        TotalCount = nullable.Value
      };
    }

    private WcfHierarchicalTaxon GetTaxonInternal(
      string taxonId,
      string provider,
      string itemType)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      Guid id = new Guid(taxonId);
      TaxonomyManager manager = TaxonomyManager.GetManager(provider);
      HierarchicalTaxon taxon = manager.GetTaxon<HierarchicalTaxon>(id);
      ServiceUtility.DisableCache();
      Type dataItemType = (Type) null;
      if (!string.IsNullOrEmpty(itemType))
        dataItemType = Type.GetType(itemType);
      return this.CreateWcfTaxon(manager, taxon, WcTaxonDataFlags.All, dataItemType);
    }

    private CollectionContext<WcfHierarchicalTaxon> GetPredecessorTaxaInternal(
      string taxonId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      bool onlyPath,
      string itemType,
      string mode)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      Guid id = new Guid(taxonId);
      TaxonomyManager manager = TaxonomyManager.GetManager(provider);
      HierarchicalTaxon taxon = manager.GetTaxon<HierarchicalTaxon>(id);
      if (taxon == null)
        throw new WebProtocolException(HttpStatusCode.NotFound, Res.Get<TaxonomyResources>().TaxonNotFound, (Exception) null);
      int? totalCount = new int?(0);
      List<HierarchicalTaxon> taxonPath = new List<HierarchicalTaxon>();
      this.ConstructPath(taxon, taxonPath);
      ServiceUtility.DisableCache();
      Type type = (Type) null;
      if (!string.IsNullOrEmpty(itemType))
        type = TypeResolutionService.ResolveType(itemType, false);
      WcTaxonDataFlags flags = TaxonServiceHelper.GetFlags(mode);
      IDictionary<string, HierarchyLevelState> taxonomyContext = (IDictionary<string, HierarchyLevelState>) null;
      if (take != 0)
        taxonomyContext = (IDictionary<string, HierarchyLevelState>) new Dictionary<string, HierarchyLevelState>();
      if (!onlyPath)
      {
        List<WcfHierarchicalTaxon> hierarchicalTaxonList = new List<WcfHierarchicalTaxon>(taxonPath.Count);
        if (taxonPath.Count > 0)
          hierarchicalTaxonList = this.ConstructTaxonSubTree(taxonPath, hierarchicalTaxonList, manager, ref totalCount, sortExpression, skip, take, filter, type, flags, taxonomyContext);
        HierarchicalTaxonService.SetTaxaHasChildren(manager, (IEnumerable<WcfHierarchicalTaxon>) hierarchicalTaxonList);
        TaxonomyCollectionContext predecessorTaxaInternal = new TaxonomyCollectionContext((IEnumerable<WcfHierarchicalTaxon>) hierarchicalTaxonList);
        predecessorTaxaInternal.TotalCount = totalCount.Value;
        predecessorTaxaInternal.TaxonomyContext = taxonomyContext;
        return (CollectionContext<WcfHierarchicalTaxon>) predecessorTaxaInternal;
      }
      List<WcfHierarchicalTaxon> hierarchicalTaxonList1 = new List<WcfHierarchicalTaxon>();
      foreach (HierarchicalTaxon hierarchicalTaxon in taxonPath)
        hierarchicalTaxonList1.Add(this.CreateWcfTaxon(manager, hierarchicalTaxon, flags, type));
      HierarchicalTaxonService.SetTaxaHasChildren(manager, (IEnumerable<WcfHierarchicalTaxon>) hierarchicalTaxonList1);
      return new CollectionContext<WcfHierarchicalTaxon>((IEnumerable<WcfHierarchicalTaxon>) hierarchicalTaxonList1)
      {
        TotalCount = totalCount.Value
      };
    }

    private WcfHierarchicalTaxon SaveTaxonInternal(
      WcfHierarchicalTaxon wcfTaxon,
      string taxonomyId,
      string taxonId,
      string provider,
      string ordinal,
      string insertionPosition,
      bool skipSiteContext)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      lock (HierarchicalTaxonService.writeLock)
      {
        TaxonomyManager manager = TaxonomyManager.GetManager(provider);
        Guid guid = new Guid(taxonomyId);
        if (!skipSiteContext)
          guid = MultisiteTaxonomiesResolver.GetMultisiteTaxonomiesResolver(manager).ResolveSiteTaxonomyId(guid);
        Guid id = new Guid(taxonId);
        HierarchicalTaxon taxon = !(id != Guid.Empty) ? manager.CreateTaxon<HierarchicalTaxon>() : manager.GetTaxon<HierarchicalTaxon>(id);
        wcfTaxon.Id = taxon.Id;
        this.SetHierarchicalTaxonProperties(taxon, wcfTaxon, manager, guid);
        wcfTaxon.Attributes.CopyTo(taxon.Attributes);
        this.ValidateConstraints(manager, taxon);
        if (!string.IsNullOrEmpty(ordinal))
        {
          manager.MoveTaxon(taxon, MovingDirection.Up);
          this.CalculateOrdinal(taxon, ordinal, insertionPosition, manager);
        }
        manager.SaveChanges();
        wcfTaxon.AvailableLanguages = taxon.AvailableLanguages;
        return wcfTaxon;
      }
    }

    private void ValidateConstraints(TaxonomyManager manager, HierarchicalTaxon taxon)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      try
      {
        manager.ValidateConstraints(taxon);
      }
      catch (SystemException ex)
      {
        manager.CancelChanges();
        throw ex;
      }
    }

    private void MoveTaxonInternal(string taxonId, string provider, MovingDirection direction)
    {
      lock (HierarchicalTaxonService.writeLock)
      {
        ServiceUtility.RequestBackendUserAuthentication();
        Guid id = new Guid(taxonId);
        TaxonomyManager manager = TaxonomyManager.GetManager(provider);
        manager.MoveTaxon(manager.GetTaxon<HierarchicalTaxon>(id), direction);
        manager.SaveChanges();
      }
    }

    private void ChangeParentInternal(
      string taxonId,
      string newParentId,
      string provider,
      bool commit)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      this.ChangeParentInternal(new Guid(taxonId), new Guid(newParentId), provider, commit);
    }

    private void ChangeParentInternal(
      Guid taxonId,
      Guid newParentId,
      string provider,
      bool commit)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      try
      {
        if (commit)
          Monitor.Enter(HierarchicalTaxonService.writeLock);
        TaxonomyManager manager = TaxonomyManager.GetManager(provider);
        HierarchicalTaxon taxon1 = manager.GetTaxon<HierarchicalTaxon>(taxonId);
        if (newParentId == Guid.Empty)
        {
          taxon1.Parent = (HierarchicalTaxon) null;
        }
        else
        {
          List<HierarchicalTaxon> taxonPath = new List<HierarchicalTaxon>();
          HierarchicalTaxon taxon2 = manager.GetTaxon<HierarchicalTaxon>(newParentId);
          this.ConstructPath(taxon2, taxonPath);
          if (taxonPath.Contains(taxon1))
            this.InvalidTreeStructureReorganization();
          taxon1.Parent = taxon2;
        }
        this.ValidateConstraints(manager, taxon1);
        if (!commit)
          return;
        manager.SaveChanges();
      }
      finally
      {
        if (commit)
          Monitor.Exit(HierarchicalTaxonService.writeLock);
      }
    }

    private void BatchMoveTaxonsInternal(
      string provider,
      string[] taxonIds,
      MovingDirection direction)
    {
      lock (HierarchicalTaxonService.writeLock)
      {
        ServiceUtility.RequestBackendUserAuthentication();
        TaxonomyManager manager = TaxonomyManager.GetManager(provider);
        manager.MoveTaxons(this.GetTaxons(manager, taxonIds), direction);
        manager.SaveChanges();
      }
    }

    private List<HierarchicalTaxon> GetTaxons(
      TaxonomyManager manager,
      string[] taxonIds)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      List<HierarchicalTaxon> taxons = new List<HierarchicalTaxon>();
      foreach (string taxonId in taxonIds)
        taxons.Add(manager.GetTaxon<HierarchicalTaxon>(new Guid(taxonId)));
      return taxons;
    }

    private void BatchChangeParentInternal(
      string provider,
      WcfBatchChangeParent batchChangeParentObject)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      lock (HierarchicalTaxonService.writeLock)
      {
        foreach (Guid taxonId in batchChangeParentObject.TaxonIds)
          this.ChangeParentInternal(taxonId, batchChangeParentObject.NewParentId, provider, false);
        TaxonomyManager.GetManager(provider).SaveChanges();
      }
    }

    private bool DeleteTaxonInternal(string taxonId, string provider, string lang)
    {
      lock (HierarchicalTaxonService.writeLock)
      {
        ServiceUtility.RequestBackendUserAuthentication();
        CultureInfo language = (CultureInfo) null;
        if (!string.IsNullOrEmpty(lang))
          language = new CultureInfo(lang);
        Guid id = new Guid(taxonId);
        TaxonomyManager manager = TaxonomyManager.GetManager(provider);
        manager.Delete((ITaxon) manager.GetTaxon<HierarchicalTaxon>(id), language);
        manager.SaveChanges();
        return true;
      }
    }

    private Guid[] BatchDeleteTaxonsWithResultInternal(
      string[] taxaIds,
      string provider,
      string lang)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      HierarchicalTaxonService.\u003C\u003Ec__DisplayClass38_0 cDisplayClass380 = new HierarchicalTaxonService.\u003C\u003Ec__DisplayClass38_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass380.taxaIds = taxaIds;
      lock (HierarchicalTaxonService.writeLock)
      {
        CultureInfo language = (CultureInfo) null;
        if (!string.IsNullOrEmpty(lang))
          language = new CultureInfo(lang);
        string transactionName = string.Format("DeleteTaxon_{0}", (object) Guid.NewGuid());
        TaxonomyManager manager1 = TaxonomyManager.GetManager(provider, transactionName);
        List<HierarchicalTaxon> source = new List<HierarchicalTaxon>();
        IQueryable<HierarchicalTaxon> taxa = manager1.GetTaxa<HierarchicalTaxon>();
        ParameterExpression parameterExpression;
        // ISSUE: method reference
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method reference
        Expression<Func<HierarchicalTaxon, bool>> predicate = Expression.Lambda<Func<HierarchicalTaxon, bool>>((Expression) Expression.Call((Expression) null, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Enumerable.Contains)), new Expression[2]
        {
          cDisplayClass380.taxaIds,
          (Expression) Expression.Call(t.Id, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (object.ToString)), Array.Empty<Expression>())
        }), parameterExpression);
        foreach (HierarchicalTaxon taxon in (IEnumerable<HierarchicalTaxon>) taxa.Where<HierarchicalTaxon>(predicate))
        {
          // ISSUE: reference to a compiler-generated field
          if (!this.CheckIfParentIsMarkedForDeletetion(taxon, cDisplayClass380.taxaIds, language))
            source.Add(taxon);
        }
        IEnumerable<Guid> guids = source.Select<HierarchicalTaxon, Guid>((Func<HierarchicalTaxon, Guid>) (t => t.Id));
        List<Guid> guidList = new List<Guid>();
        try
        {
          // ISSUE: reference to a compiler-generated field
          if (cDisplayClass380.taxaIds.Length != 0)
          {
            foreach (HierarchicalTaxon hierarchicalTaxon in source)
              manager1.Delete((ITaxon) hierarchicalTaxon, language);
            TransactionManager.CommitTransaction(transactionName);
          }
        }
        catch (Exception ex1)
        {
          TransactionManager.RollbackTransaction(transactionName);
          TaxonomyManager manager2 = TaxonomyManager.GetManager(provider);
          foreach (Guid id in guids)
          {
            try
            {
              ITaxon taxon = manager2.GetTaxon(id);
              manager2.Delete(taxon, language);
              manager2.SaveChanges();
            }
            catch (Exception ex2)
            {
              guidList.Add(id);
            }
          }
        }
        return guidList.ToArray();
      }
    }

    private bool CheckIfParentIsMarkedForDeletetion(
      HierarchicalTaxon taxon,
      string[] taxonIds,
      CultureInfo language)
    {
      bool flag = false;
      HierarchicalTaxon hierarchicalTaxon = taxon;
      while (hierarchicalTaxon.Parent != null)
      {
        hierarchicalTaxon = hierarchicalTaxon.Parent;
        if (((IEnumerable<string>) taxonIds).Contains<string>(hierarchicalTaxon.Id.ToString()) && (language == null || ((IEnumerable<CultureInfo>) hierarchicalTaxon.AvailableCultures).Contains<CultureInfo>(language) && ((IEnumerable<CultureInfo>) hierarchicalTaxon.AvailableCultures).Where<CultureInfo>((Func<CultureInfo, bool>) (c => !string.IsNullOrEmpty(c.Name))).Count<CultureInfo>() == 1))
        {
          flag = true;
          break;
        }
      }
      return flag;
    }

    private bool BatchDeleteTaxonsInternal(string[] taxonIds, string provider, string lang)
    {
      this.BatchDeleteTaxonsWithResultInternal(taxonIds, provider, lang);
      return true;
    }

    internal WcfHierarchicalTaxon CreateWcfTaxon(
      TaxonomyManager taxonomyManager,
      HierarchicalTaxon hierarchicalTaxon,
      WcTaxonDataFlags flags,
      Type dataItemType)
    {
      WcfHierarchicalTaxon wcfTaxonObject = TaxonServiceHelper.CreateWcfTaxonObject((Taxon) hierarchicalTaxon, taxonomyManager, flags, dataItemType) as WcfHierarchicalTaxon;
      if (hierarchicalTaxon.Parent != null)
      {
        wcfTaxonObject.ParentTaxonId = new Guid?(hierarchicalTaxon.Parent.Id);
        wcfTaxonObject.ParentTaxonTitle = (string) hierarchicalTaxon.Parent.Title;
      }
      if (hierarchicalTaxon.Taxonomy != null)
      {
        wcfTaxonObject.TaxonomyId = new Guid?(hierarchicalTaxon.Taxonomy.Id);
        wcfTaxonObject.TaxonomyName = TaxonServiceHelper.GetTaxonomyName((Taxon) hierarchicalTaxon);
      }
      return wcfTaxonObject;
    }

    internal void SetHierarchicalTaxonProperties(
      HierarchicalTaxon taxon,
      WcfHierarchicalTaxon wcfTaxon,
      TaxonomyManager taxonomyManager,
      Guid taxonomyGuidId)
    {
      this.SetHierarchicalTaxonMainProperties(taxon, wcfTaxon, taxonomyManager, taxonomyGuidId);
      TaxonServiceHelper.SetTaxonSynonyms((Taxon) taxon, (IWcfTaxon) wcfTaxon, taxonomyManager);
    }

    internal void SetHierarchicalTaxonMainProperties(
      HierarchicalTaxon taxon,
      WcfHierarchicalTaxon wcfTaxon,
      TaxonomyManager taxonomyManager,
      Guid taxonomyGuidId)
    {
      taxon.Title = (Lstring) wcfTaxon.Title;
      taxon.Name = wcfTaxon.Name;
      taxon.Description = (Lstring) wcfTaxon.Description;
      taxon.UrlName = (Lstring) wcfTaxon.UrlName;
      Guid? parentTaxonId = wcfTaxon.ParentTaxonId;
      Guid id1 = taxon.Id;
      if ((parentTaxonId.HasValue ? (parentTaxonId.HasValue ? (parentTaxonId.GetValueOrDefault() == id1 ? 1 : 0) : 1) : 0) != 0)
        throw new InvalidOperationException("Taxon can't be a parent of itself");
      parentTaxonId = wcfTaxon.ParentTaxonId;
      if (parentTaxonId.HasValue)
      {
        parentTaxonId = wcfTaxon.ParentTaxonId;
        Guid empty = Guid.Empty;
        if ((parentTaxonId.HasValue ? (parentTaxonId.HasValue ? (parentTaxonId.GetValueOrDefault() != empty ? 1 : 0) : 0) : 1) != 0)
        {
          if (taxon.Parent != null)
          {
            Guid id2 = taxon.Parent.Id;
            parentTaxonId = wcfTaxon.ParentTaxonId;
            Guid guid = parentTaxonId.Value;
            if (!(id2 != guid))
              goto label_8;
          }
          TaxonomyManager taxonomyManager1 = taxonomyManager;
          parentTaxonId = wcfTaxon.ParentTaxonId;
          Guid id3 = parentTaxonId.Value;
          HierarchicalTaxon taxon1 = taxonomyManager1.GetTaxon<HierarchicalTaxon>(id3);
          taxon.Parent = taxon1;
          goto label_8;
        }
      }
      taxon.Parent = (HierarchicalTaxon) null;
label_8:
      HierarchicalTaxonomy taxonomy = taxonomyManager.GetTaxonomy<HierarchicalTaxonomy>(taxonomyGuidId);
      taxon.Taxonomy = (Taxonomy) taxonomy;
    }

    private void CalculateOrdinal(
      HierarchicalTaxon taxon,
      string ordinal,
      string insertionPosition,
      TaxonomyManager taxonomyManager)
    {
      bool flag = !string.IsNullOrEmpty(insertionPosition) && insertionPosition.ToLower() == "before";
      IQueryable<HierarchicalTaxon> taxa = taxonomyManager.GetTaxa<HierarchicalTaxon>();
      IQueryable<HierarchicalTaxon> source1;
      if (taxon.Parent != null)
      {
        Guid parentId = taxon.Parent.Id;
        source1 = taxa.Where<HierarchicalTaxon>((Expression<Func<HierarchicalTaxon, bool>>) (t => t.Parent.Id == parentId));
      }
      else
        source1 = taxa.Where<HierarchicalTaxon>((Expression<Func<HierarchicalTaxon, bool>>) (t => t.Parent == default (object)));
      float ordinalValue = float.Parse(ordinal);
      IQueryable<HierarchicalTaxon> source2;
      if (flag)
        source2 = (IQueryable<HierarchicalTaxon>) source1.Where<HierarchicalTaxon>((Expression<Func<HierarchicalTaxon, bool>>) (t => t.Ordinal <= ordinalValue)).OrderByDescending<HierarchicalTaxon, float>((Expression<Func<HierarchicalTaxon, float>>) (t => t.Ordinal));
      else
        source2 = (IQueryable<HierarchicalTaxon>) source1.Where<HierarchicalTaxon>((Expression<Func<HierarchicalTaxon, bool>>) (t => t.Ordinal >= ordinalValue)).OrderBy<HierarchicalTaxon, float>((Expression<Func<HierarchicalTaxon, float>>) (t => t.Ordinal));
      HierarchicalTaxon[] array = source2.Take<HierarchicalTaxon>(2).ToArray<HierarchicalTaxon>();
      if (array.Length < 1)
        return;
      IOrderedItem previousAdjacentItem = (IOrderedItem) array[0];
      IOrderedItem nextAdjacentItem = array.Length >= 2 ? (IOrderedItem) array[1] : (IOrderedItem) null;
      if (flag)
      {
        IOrderedItem orderedItem = previousAdjacentItem;
        previousAdjacentItem = nextAdjacentItem;
        nextAdjacentItem = orderedItem;
      }
      taxon.SetOrdinalBetweenItems(previousAdjacentItem, nextAdjacentItem);
    }

    /// <summary>
    /// Method that will construct array representing the sub tree of hierarchical taxons from a given path
    /// </summary>
    /// <param name="taxonPath">The taxon path.</param>
    /// <param name="taxonPredecessorSubTree">The taxon predecessor sub tree.</param>
    /// <param name="taxonomyManager">The taxonomy manager.</param>
    /// <param name="totalCount">The total count.</param>
    /// <param name="sortExpression">The sort expression.</param>
    /// <param name="skip">The number of taxons to skip.</param>
    /// <param name="take">The number of taxons to take.</param>
    /// <param name="filter">The filter.</param>
    private List<WcfHierarchicalTaxon> ConstructTaxonSubTree(
      List<HierarchicalTaxon> taxonPath,
      List<WcfHierarchicalTaxon> taxonPredecessorSubTree,
      TaxonomyManager taxonomyManager,
      ref int? totalCount,
      string sortExpression,
      int skip,
      int take,
      string filter,
      Type itemType,
      WcTaxonDataFlags flags,
      IDictionary<string, HierarchyLevelState> taxonomyContext)
    {
      this.EnsureTaxonPathStartsFromRoot(taxonPath);
      if (taxonPath.Count > 0)
      {
        Guid taxonomyId = taxonPath.First<HierarchicalTaxon>().Taxonomy.Id;
        int? totalCount1 = new int?(0);
        IQueryable<HierarchicalTaxon> queryable1 = DataProviderBase.SetExpressions<HierarchicalTaxon>(taxonomyManager.GetTaxa<HierarchicalTaxon>().Where<HierarchicalTaxon>((Expression<Func<HierarchicalTaxon, bool>>) (t => t.Parent == default (object) && t.Taxonomy.Id == taxonomyId)), filter, sortExpression, new int?(skip), new int?(take), ref totalCount1);
        foreach (HierarchicalTaxon hierarchicalTaxon in (IEnumerable<HierarchicalTaxon>) queryable1)
        {
          WcfHierarchicalTaxon wcfTaxon = this.CreateWcfTaxon(taxonomyManager, hierarchicalTaxon, flags, itemType);
          taxonPredecessorSubTree.Add(wcfTaxon);
        }
        if (!string.IsNullOrEmpty(sortExpression))
          taxonPredecessorSubTree = taxonPredecessorSubTree.AsQueryable<WcfHierarchicalTaxon>().OrderBy<WcfHierarchicalTaxon>(sortExpression, (object) StringComparison.OrdinalIgnoreCase).ToList<WcfHierarchicalTaxon>();
        if (totalCount1.HasValue)
        {
          ref int? local = ref totalCount;
          int? nullable1 = totalCount;
          int num = totalCount1.Value;
          int? nullable2 = nullable1.HasValue ? new int?(nullable1.GetValueOrDefault() + num) : new int?();
          local = nullable2;
        }
        if (taxonomyContext != null)
          taxonomyContext[Guid.Empty.ToString()] = new HierarchyLevelState()
          {
            Skip = skip,
            Take = take,
            Total = totalCount1.Value
          };
        IQueryable<HierarchicalTaxon> source1 = queryable1;
        foreach (HierarchicalTaxon hierarchicalTaxon1 in taxonPath)
        {
          int? totalCount2 = new int?(0);
          Guid taxonId = hierarchicalTaxon1.Id;
          if (source1.Any<HierarchicalTaxon>((Expression<Func<HierarchicalTaxon, bool>>) (t => t.Id == taxonId)))
          {
            IQueryable<HierarchicalTaxon> taxa = taxonomyManager.GetTaxa<HierarchicalTaxon>();
            Expression<Func<HierarchicalTaxon, bool>> predicate = (Expression<Func<HierarchicalTaxon, bool>>) (t => t.Parent.Id == taxonId && t.Taxonomy.Id == taxonomyId);
            IQueryable<HierarchicalTaxon> queryable2;
            source1 = queryable2 = DataProviderBase.SetExpressions<HierarchicalTaxon>(taxa.Where<HierarchicalTaxon>(predicate), filter, sortExpression, new int?(skip), new int?(take), ref totalCount2);
            if (taxonomyContext != null)
              taxonomyContext[taxonId.ToString()] = new HierarchyLevelState()
              {
                Skip = skip,
                Take = take,
                Total = totalCount2.Value
              };
            List<WcfHierarchicalTaxon> source2 = new List<WcfHierarchicalTaxon>();
            foreach (HierarchicalTaxon hierarchicalTaxon2 in (IEnumerable<HierarchicalTaxon>) queryable2)
              source2.Add(this.CreateWcfTaxon(taxonomyManager, hierarchicalTaxon2, flags, itemType));
            if (!string.IsNullOrEmpty(sortExpression))
              taxonPredecessorSubTree.AddRange((IEnumerable<WcfHierarchicalTaxon>) source2.AsQueryable<WcfHierarchicalTaxon>().OrderBy<WcfHierarchicalTaxon>(sortExpression, (object) StringComparison.OrdinalIgnoreCase));
            if (totalCount2.HasValue)
            {
              ref int? local = ref totalCount;
              int? nullable3 = totalCount;
              int? nullable4 = totalCount2;
              int? nullable5 = nullable3.HasValue & nullable4.HasValue ? new int?(nullable3.GetValueOrDefault() + nullable4.GetValueOrDefault()) : new int?();
              local = nullable5;
            }
          }
          else
            break;
        }
      }
      return taxonPredecessorSubTree;
    }

    private void EnsureTaxonPathStartsFromRoot(List<HierarchicalTaxon> taxonPath)
    {
      if (taxonPath.Count <= 0 || taxonPath.First<HierarchicalTaxon>().Parent == null)
        return;
      this.TreeMalformed();
    }

    private void ConstructPath(HierarchicalTaxon taxon, List<HierarchicalTaxon> taxonPath)
    {
      taxonPath.Add(taxon);
      if (taxon.Parent == null)
        return;
      this.VisitTaxon(taxon, taxonPath, new HashSet<Guid>()
      {
        taxon.Id
      });
    }

    private void VisitTaxon(
      HierarchicalTaxon taxon,
      List<HierarchicalTaxon> taxonPath,
      HashSet<Guid> visitedTaxonsIds)
    {
      if (taxon.Parent == null)
        return;
      if (visitedTaxonsIds.Contains(taxon.Parent.Id))
        this.TreeMalformed();
      visitedTaxonsIds.Add(taxon.Parent.Id);
      taxonPath.Insert(0, taxon.Parent);
      this.VisitTaxon(taxon.Parent, taxonPath, visitedTaxonsIds);
    }

    private void TreeMalformed() => throw new WebProtocolException(HttpStatusCode.InternalServerError, Res.Get<TaxonomyResources>().TaxonomyTreeMalformed, (Exception) null);

    private void InvalidTreeStructureReorganization() => throw new WebProtocolException(HttpStatusCode.InternalServerError, Res.Get<TaxonomyResources>().CannotBeParentToItself, (Exception) null);
  }
}
