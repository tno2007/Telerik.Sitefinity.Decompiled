// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Web.Services.FlatTaxonService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Taxonomies.Web.Services.Common;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Taxonomies.Web.Services
{
  /// <summary>
  /// Wcf service implementation for working with FlatTaxon objects.
  /// </summary>
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  public class FlatTaxonService : IFlatTaxonService
  {
    private static readonly object writeLock = new object();

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
    /// <param name="siteContextMode">The mode in which the taxa should be obtained (ex. current site context, no site context, all sites context).</param>
    /// <returns>
    /// A CollectionContext instance that contains the collection of WcfFlatTaxon objects.
    /// </returns>
    public CollectionContext<WcfFlatTaxon> GetTaxa(
      string taxonomyId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string itemType,
      string mode,
      string siteContextMode = "currentSiteContext")
    {
      return this.GetTaxaInternal(taxonomyId, provider, sortExpression, skip, take, filter, itemType, mode, siteContextMode);
    }

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
    /// <param name="siteContextMode">The mode in which the taxa should be obtained (ex. current site context, no site context, all sites context).</param>
    /// <returns>
    /// A CollectionContext instance that contains the collection of WcfFlatTaxon objects.
    /// </returns>
    public CollectionContext<WcfFlatTaxon> GetTaxaInXml(
      string taxonomyId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string itemType,
      string mode,
      string siteContextMode = "currentSiteContext")
    {
      return this.GetTaxaInternal(taxonomyId, provider, sortExpression, skip, take, filter, itemType, mode, siteContextMode);
    }

    /// <summary>
    /// Gets the collection of flat taxons and returns the result in JSON format. Request is expected with a POST HTTP method.
    /// </summary>
    /// <param name="taxonomyId">Id of the taxonomy from which the taxons should be returned.</param>
    /// <param name="filter">Represents request data that is required to retrieve taxons.</param>
    /// <returns>
    /// A CollectionContext instance that contains the collection of WcfFlatTaxon objects.
    /// </returns>
    public CollectionContext<WcfFlatTaxon> GetTaxaExtended(
      string taxonomyId,
      TaxaFilter filter)
    {
      return this.GetTaxaInternal(taxonomyId, filter.Provider, filter.SortExpression, filter.Skip, filter.Take, filter.FilterExpression, filter.ItemType, filter.Mode, filter.SiteContextMode);
    }

    /// <summary>
    /// Gets the collection of flat taxons and returns the result in XML format.Request is expected with a POST HTTP method.
    /// </summary>
    /// <param name="taxonomyId">Id of the taxonomy from which the taxons should be returned.</param>
    /// <param name="filter">Represents request data that is required to retrieve taxons.</param>
    /// <returns>
    /// A CollectionContext instance that contains the collection of WcfFlatTaxon objects.
    /// </returns>
    public CollectionContext<WcfFlatTaxon> GetTaxaExtendedInXml(
      string taxonomyId,
      TaxaFilter filter)
    {
      return this.GetTaxaInternal(taxonomyId, filter.Provider, filter.SortExpression, filter.Skip, filter.Take, filter.FilterExpression, filter.ItemType, filter.Mode, filter.SiteContextMode);
    }

    /// <summary>
    /// Gets a single flat taxon and returns it in JSON format.
    /// </summary>
    /// <param name="taxonomyId">Id of the taxonomy to which the taxon belongs to.</param>
    /// <param name="taxonId">Id of the taxon that ought to be retrieved.</param>
    /// <param name="provider">Name of the provider to be used when retrieving the taxon.</param>
    /// <returns>An instance of WcfFlatTaxon.</returns>
    public WcfFlatTaxon GetTaxon(
      string taxonomyId,
      string taxonId,
      string provider,
      string itemType)
    {
      return this.GetTaxonInternal(taxonomyId, taxonId, provider, itemType);
    }

    /// <summary>
    /// Gets a single flat taxon and returns it in XML format.
    /// </summary>
    /// <param name="taxonomyId">Id of the taxonomy to which the taxon belongs to.</param>
    /// <param name="taxonId">Id of the taxon that ought to be retrieved.</param>
    /// <param name="provider">Name of the provider to be sued when retrieving the taxon.</param>
    /// <returns>An instance of WcfFlatTaxon.</returns>
    public WcfFlatTaxon GetTaxonInXml(
      string taxonomyId,
      string taxonId,
      string provider,
      string itemType)
    {
      throw new NotImplementedException();
    }

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
    public WcfFlatTaxon SaveTaxon(
      WcfFlatTaxon wcfTaxon,
      string taxonomyId,
      string taxonId,
      string provider,
      string itemType,
      bool skipSiteContext)
    {
      ServiceUtility.ProtectBackendServices();
      return this.SaveTaxonInternal(wcfTaxon, taxonomyId, taxonId, provider, skipSiteContext);
    }

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
    public WcfFlatTaxon SaveTaxonInXml(
      WcfFlatTaxon wcfTaxon,
      string taxonomyId,
      string taxonId,
      string provider,
      string itemType,
      bool skipSiteContext)
    {
      ServiceUtility.ProtectBackendServices();
      return this.SaveTaxonInternal(wcfTaxon, taxonomyId, taxonId, provider, skipSiteContext);
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
    public bool BulkDeleteTaxons(
      string[] taxonIds,
      string taxonomyId,
      string provider,
      string lang)
    {
      ServiceUtility.ProtectBackendServices();
      return this.BulkDeleteTaxonsInternal(taxonIds, provider, lang);
    }

    /// <summary>
    /// Deletes a list of flat taxons and returns failed ids if exists.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="taxonIds">An array of the ids of the taxons to delete.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="lang">The language to be deleted. If null, all versions will be deleted</param>
    /// <returns>Item ids which failed to delete.</returns>
    internal Guid[] BulkDeleteTaxonsWithResult(string[] taxonIds, string provider, string lang) => this.BulkDeleteTaxonsWithResultInternal(taxonIds, provider, lang);

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
    public bool BulkDeleteTaxonsInXml(
      string[] taxonIds,
      string taxonomyId,
      string provider,
      string lang)
    {
      ServiceUtility.ProtectBackendServices();
      return this.BulkDeleteTaxonsInternal(taxonIds, provider, lang);
    }

    /// <summary>
    /// Deletes a flat taxon and returns true if the flat taxon has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="taxonomyId">Id of the taxonomy to which the taxon that ought to be deleted belongs to.</param>
    /// <param name="taxonId">Id of the taxon that ought to be deleted.</param>
    /// <param name="provider">Name of the provider to be used when deleting a taxon.</param>
    /// <param name="lang">The language to be deleted. If null, all versions will be deleted</param>
    /// <returns></returns>
    public bool DeleteTaxon(
      string taxonomyId,
      string taxonId,
      string provider,
      string itemType,
      string lang)
    {
      ServiceUtility.ProtectBackendServices();
      return this.DeleteTaxonInternal(taxonId, provider, lang);
    }

    /// <summary>
    /// Deletes a flat taxon and returns true if the flat taxon has been deleted; otherwise false.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="taxonomyId">Id of the taxonomy to which the taxon that ought to be deleted belongs to.</param>
    /// <param name="taxonId">Id of the taxon that ought to be deleted.</param>
    /// <param name="provider">Name of the provider to be used when deleting a taxon.</param>
    /// <param name="lang">The language to be deleted. If null, all versions will be deleted</param>
    /// <returns></returns>
    public bool DeleteTaxonInXml(
      string taxonomyId,
      string taxonId,
      string provider,
      string itemType,
      string lang)
    {
      ServiceUtility.ProtectBackendServices();
      return this.DeleteTaxonInternal(taxonId, provider, lang);
    }

    /// <summary>
    /// This method accepts an array of taxon titles. For each title it will try to retrieve a tag with the same title from the specified
    /// taxonomy. For the titles for which taxon cannot be found, new taxon objects will be created. Method will return an array of WcfFlatTaxon
    /// objects (retrieved by title or newly created) in JSON format.
    /// </summary>
    /// <param name="taxonomyId">Id of the flat taxonomy in which the taxa ought to be ensured.</param>
    /// <param name="taxonTitles"></param>
    /// <param name="provider">Name of the provider to be used when ensuring the taxon.</param>
    /// <returns>
    /// An array of WcfFlatTaxon objects representing the taxon objects based on the passed taxon title array.
    /// </returns>
    public WcfFlatTaxon[] EnsureTaxa(
      string taxonomyId,
      string[] taxonTitles,
      string provider,
      string itemType)
    {
      ServiceUtility.ProtectBackendServices();
      return this.EnsureTaxaInternal(taxonomyId, taxonTitles, provider, itemType);
    }

    /// <summary>
    /// This method accepts an array of taxon titles. For each title it will try to retrieve a tag with the same title from the specified
    /// taxonomy. For the titles for which taxon cannot be found, new taxon objects will be created. Method will return an array of WcfFlatTaxon
    /// objects (retrieved by title or newly created) in JSON format.
    /// </summary>
    /// <param name="taxonomyId">Id of the flat taxonomy in which the taxa ought to be ensured.</param>
    /// <param name="taxonTitles"></param>
    /// <param name="provider">Name of the provider to be used when ensuring the taxon.</param>
    /// <returns>
    /// An array of WcfFlatTaxon objects representing the taxon objects based on the passed taxon title array.
    /// </returns>
    public WcfFlatTaxon[] EnsureTaxaInXml(
      string taxonomyId,
      string[] taxonTitles,
      string provider,
      string itemType)
    {
      ServiceUtility.ProtectBackendServices();
      return this.EnsureTaxaInternal(taxonomyId, taxonTitles, provider, itemType);
    }

    private CollectionContext<WcfFlatTaxon> GetTaxaInternal(
      string taxonomyId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string itemType,
      string mode,
      string siteContextMode)
    {
      ServiceUtility.RequestAuthentication();
      Guid taxonomyId1 = new Guid(taxonomyId);
      TaxonomyManager manager = TaxonomyManager.GetManager(provider);
      SiteContextMode siteContextMode1 = TaxonServiceHelper.GetSiteContextMode(siteContextMode);
      IQueryable<FlatTaxon> siteContextTaxa = TaxonServiceHelper.GetSiteContextTaxa<FlatTaxon>(manager, siteContextMode1, taxonomyId1);
      int? totalCount = new int?(0);
      if (skip == 0 && take == 0)
        totalCount = new int?();
      WcTaxonDataFlags flags = TaxonServiceHelper.GetFlags(mode);
      IQueryable<FlatTaxon> queryable;
      if (TaxonServiceHelper.UseTaxonDataFlag(flags, WcTaxonDataFlags.AutoComplete))
      {
        IEnumerable<FlatTaxon> source2 = TaxonServiceHelper.FilterSynonymsForCurrentContext((IEnumerable<Synonym>) manager.GetSynonyms().Where<Synonym>((Expression<Func<Synonym, bool>>) (x => (int) x.Parent.Status == 0 && (x.Value.StartsWith(filter) || x.Value.Contains(filter))))).Select<Synonym, FlatTaxon>((Func<Synonym, FlatTaxon>) (x => x.Parent as FlatTaxon));
        queryable = siteContextTaxa.Where<FlatTaxon>((Expression<Func<FlatTaxon, bool>>) (x => x.Title.StartsWith(filter) && (int) x.Status == 0)).Union<FlatTaxon>(source2);
      }
      else
        queryable = DataProviderBase.SetExpressions<FlatTaxon>(siteContextTaxa, filter, sortExpression, new int?(skip), new int?(take), ref totalCount).Where<FlatTaxon>((Expression<Func<FlatTaxon, bool>>) (x => (int) x.Status == 0));
      List<WcfFlatTaxon> wcfFlatTaxonList = new List<WcfFlatTaxon>();
      Type dataItemType = (Type) null;
      if (!string.IsNullOrEmpty(itemType))
        dataItemType = TypeResolutionService.ResolveType(itemType);
      foreach (FlatTaxon flatTaxon in (IEnumerable<FlatTaxon>) queryable)
      {
        if (flatTaxon != null)
        {
          WcfFlatTaxon wcfTaxonObject = TaxonServiceHelper.CreateWcfTaxonObject((Taxon) flatTaxon, manager, flags, dataItemType) as WcfFlatTaxon;
          wcfFlatTaxonList.Add(wcfTaxonObject);
        }
      }
      if (!string.IsNullOrEmpty(sortExpression))
        wcfFlatTaxonList = wcfFlatTaxonList.AsQueryable<WcfFlatTaxon>().OrderBy<WcfFlatTaxon>(sortExpression, (object) StringComparison.OrdinalIgnoreCase).ToList<WcfFlatTaxon>();
      if (!totalCount.HasValue)
        totalCount = new int?(wcfFlatTaxonList.Count);
      ServiceUtility.DisableCache();
      return new CollectionContext<WcfFlatTaxon>((IEnumerable<WcfFlatTaxon>) wcfFlatTaxonList)
      {
        TotalCount = totalCount.Value
      };
    }

    private WcfFlatTaxon GetTaxonInternal(
      string taxonomyId,
      string taxonId,
      string provider,
      string itemType)
    {
      ServiceUtility.RequestAuthentication();
      Guid id = new Guid(taxonId);
      TaxonomyManager manager1 = TaxonomyManager.GetManager(provider);
      FlatTaxon taxon = manager1.GetTaxon<FlatTaxon>(id);
      Type type = (Type) null;
      if (!string.IsNullOrEmpty(itemType))
        type = Type.GetType(itemType);
      TaxonomyManager manager2 = manager1;
      Type dataItemType = type;
      WcfFlatTaxon wcfTaxonObject = TaxonServiceHelper.CreateWcfTaxonObject((Taxon) taxon, manager2, WcTaxonDataFlags.SetSynonyms, dataItemType) as WcfFlatTaxon;
      ServiceUtility.DisableCache();
      return wcfTaxonObject;
    }

    private WcfFlatTaxon SaveTaxonInternal(
      WcfFlatTaxon wcfTaxon,
      string taxonomyId,
      string taxonId,
      string provider,
      bool skipSiteContext)
    {
      ServiceUtility.ProtectBackendServices();
      ServiceUtility.RequestBackendUserAuthentication();
      lock (FlatTaxonService.writeLock)
      {
        TaxonomyManager manager = TaxonomyManager.GetManager(provider);
        Guid id1 = new Guid(taxonomyId);
        Guid id2 = new Guid(taxonId);
        FlatTaxonomy taxonomy = manager.GetTaxonomy<FlatTaxonomy>(id1);
        if (!skipSiteContext)
          taxonomy = MultisiteTaxonomiesResolver.GetMultisiteTaxonomiesResolver(manager).ResolveSiteTaxonomy<FlatTaxonomy>(taxonomy);
        FlatTaxon taxon;
        if (id2 != Guid.Empty)
        {
          taxon = manager.GetTaxon<FlatTaxon>(id2);
        }
        else
        {
          taxon = manager.CreateTaxon<FlatTaxon>();
          taxon.Taxonomy = (Taxonomy) taxonomy;
          wcfTaxon.Id = taxon.Id;
        }
        taxon.Title = (Lstring) wcfTaxon.Title;
        taxon.Name = wcfTaxon.Name;
        taxon.Description = (Lstring) wcfTaxon.Description;
        taxon.UrlName = (Lstring) wcfTaxon.UrlName;
        taxon.LastModified = DateTime.UtcNow;
        wcfTaxon.Attributes.CopyTo(taxon.Attributes);
        TaxonServiceHelper.SetTaxonSynonyms((Taxon) taxon, (IWcfTaxon) wcfTaxon, manager);
        this.ValidateConstraints(manager, taxon);
        manager.SaveChanges();
        return wcfTaxon;
      }
    }

    private void ValidateConstraints(TaxonomyManager manager, FlatTaxon taxon)
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

    private bool DeleteTaxonInternal(string taxonId, string provider, string lang)
    {
      lock (FlatTaxonService.writeLock)
      {
        ServiceUtility.RequestBackendUserAuthentication();
        CultureInfo language = (CultureInfo) null;
        if (!string.IsNullOrEmpty(lang))
          language = new CultureInfo(lang);
        Guid id = new Guid(taxonId);
        TaxonomyManager manager = TaxonomyManager.GetManager(provider);
        manager.Delete((ITaxon) manager.GetTaxon<FlatTaxon>(id), language);
        manager.SaveChanges();
        return true;
      }
    }

    private Guid[] BulkDeleteTaxonsWithResultInternal(
      string[] taxonIds,
      string provider,
      string lang)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      lock (FlatTaxonService.writeLock)
      {
        CultureInfo language = (CultureInfo) null;
        if (!string.IsNullOrEmpty(lang))
          language = CultureInfo.GetCultureInfo(lang);
        List<Guid> guidList = new List<Guid>();
        string transactionName = string.Format("DeleteTaxon_{0}", (object) Guid.NewGuid());
        try
        {
          TaxonomyManager manager = TaxonomyManager.GetManager(provider, transactionName);
          if (taxonIds.Length != 0)
          {
            foreach (string taxonId in taxonIds)
              FlatTaxonService.DeleteTaxon(taxonId, language, manager, false);
            TransactionManager.CommitTransaction(transactionName);
          }
        }
        catch (Exception ex1)
        {
          TransactionManager.RollbackTransaction(transactionName);
          TaxonomyManager manager = TaxonomyManager.GetManager(provider);
          foreach (string taxonId in taxonIds)
          {
            try
            {
              FlatTaxonService.DeleteTaxon(taxonId, language, manager);
            }
            catch (Exception ex2)
            {
              guidList.Add(Guid.Parse(taxonId));
            }
          }
        }
        return guidList.ToArray();
      }
    }

    private static void DeleteTaxon(
      string taxonId,
      CultureInfo language,
      TaxonomyManager taxonomyManager,
      bool saveChanges = true)
    {
      Guid id = new Guid(taxonId);
      FlatTaxon taxon = taxonomyManager.GetTaxon<FlatTaxon>(id);
      taxonomyManager.Delete((ITaxon) taxon, language);
      if (!saveChanges)
        return;
      taxonomyManager.SaveChanges();
    }

    private bool BulkDeleteTaxonsInternal(string[] taxonIds, string provider, string lang)
    {
      this.BulkDeleteTaxonsWithResultInternal(taxonIds, provider, lang);
      return true;
    }

    private WcfFlatTaxon[] EnsureTaxaInternal(
      string taxonomyId,
      string[] taxonTitles,
      string provider,
      string itemType)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      lock (FlatTaxonService.writeLock)
      {
        List<WcfFlatTaxon> wcfFlatTaxonList = new List<WcfFlatTaxon>();
        TaxonomyManager manager = TaxonomyManager.GetManager(provider);
        Guid id = new Guid(taxonomyId);
        FlatTaxonomy taxonomy = manager.GetTaxonomy<FlatTaxonomy>(id);
        taxonomy = MultisiteTaxonomiesResolver.GetMultisiteTaxonomiesResolver(manager).ResolveSiteTaxonomy<FlatTaxonomy>(taxonomy);
        foreach (string str1 in ((IEnumerable<string>) taxonTitles).Distinct<string>())
        {
          string title = str1;
          FlatTaxon flatTaxon = manager.GetTaxa<FlatTaxon>().Where<FlatTaxon>((Expression<Func<FlatTaxon, bool>>) (t => t.Title == (Lstring) title && t.Taxonomy.Id == taxonomy.Id)).SingleOrDefault<FlatTaxon>();
          Type dataItemType = (Type) null;
          if (!string.IsNullOrEmpty(itemType))
            dataItemType = Type.GetType(itemType);
          if (flatTaxon != null)
          {
            wcfFlatTaxonList.Add(TaxonServiceHelper.CreateWcfTaxonObject((Taxon) flatTaxon, manager, WcTaxonDataFlags.All, dataItemType) as WcfFlatTaxon);
          }
          else
          {
            FlatTaxon taxon = manager.CreateTaxon<FlatTaxon>();
            taxon.Title = (Lstring) title;
            string str2 = new Regex(DefinitionsHelper.UrlRegularExpressionDotNetFilter).Replace(title.ToLower(), "-");
            taxon.UrlName = (Lstring) str2;
            taxon.Name = str2;
            taxon.Taxonomy = (Taxonomy) taxonomy;
            this.ValidateConstraints(manager, taxon);
            taxonomy.Taxa.Add((Taxon) taxon);
            wcfFlatTaxonList.Add(TaxonServiceHelper.CreateWcfTaxonObject((Taxon) taxon, manager, WcTaxonDataFlags.All, dataItemType) as WcfFlatTaxon);
          }
        }
        manager.SaveChanges();
        return wcfFlatTaxonList.ToArray();
      }
    }
  }
}
