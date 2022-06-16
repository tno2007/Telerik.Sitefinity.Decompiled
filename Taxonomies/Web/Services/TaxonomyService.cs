// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Web.Services.TaxonomyService
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
using System.Text;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Taxonomies.Web.Services.Common;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Taxonomies.Web.Services
{
  /// <summary>Implementation of WCF service for roles management.</summary>
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  public class TaxonomyService : ITaxonomyService
  {
    /// <summary>
    /// Gets the collection of taxonomies and returns the result in JSON format.
    /// </summary>
    /// <param name="provider">Name of the taxonomy provider to be used when retrieving taxonomies.</param>
    /// <param name="sortExpression">Sort expression used to order the collection of taxonomies.</param>
    /// <param name="skip">Number of items to skip before starting to take the result set.</param>
    /// <param name="take">Number of items to take in the result set.</param>
    /// <param name="filter">Filter expression used to filter which taxonomies ought to be retrieved.</param>
    /// <param name="taxonomyType">Type of the taxonomy that ought to be retrieved in the result set.</param>
    /// <param name="skipSiteContext">Indicates whether to load taxonomy items by skipping site context.</param>
    /// <returns>
    /// An instance of CollectionContext object that contains the collection of taxonomies.
    /// </returns>
    public virtual CollectionContext<WcfTaxonomy> GetTaxonomies(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string taxonomyType,
      bool skipSiteContext = false)
    {
      return this.GetTaxonomiesInternal(provider, sortExpression, skip, take, filter, taxonomyType, skipSiteContext);
    }

    /// <summary>
    /// Gets the collection of taxonomies and returns the result in XML format.
    /// </summary>
    /// <param name="provider">Name of the taxonomy provider to be used when retrieving taxonomies.</param>
    /// <param name="sortExpression">Sort expression used to order the collection of taxonomies.</param>
    /// <param name="skip">Number of items to skip before starting to take the result set.</param>
    /// <param name="take">Number of items to take in the result set.</param>
    /// <param name="filter">Filter expression used to filter which taxonomies ought to be retrieved.</param>
    /// <param name="taxonomyType">Type of the taxonomy that ought to be retrieved in the result set.</param>
    /// <returns>
    /// An instance of CollectionContext object that contains the collection of taxonomies.
    /// </returns>
    public virtual CollectionContext<WcfTaxonomy> GetTaxonomiesInXml(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string taxonomyType,
      bool skipSiteContext = false)
    {
      return this.GetTaxonomiesInternal(provider, sortExpression, skip, take, filter, taxonomyType, skipSiteContext);
    }

    /// <summary>
    /// Gets a single taxonomy and returns it in the JSON format.
    /// </summary>
    /// <param name="taxonomyId">Id of the taxonomy to be retrieved.</param>
    /// <param name="provider">Name of the taxonomy provider to be used when retrieving the taxonomy.</param>
    /// <returns>
    /// An instance of <see cref="T:Telerik.Sitefinity.Taxonomies.Web.Services.WcfTaxonomy" /> object representing the taxonomy.
    /// </returns>
    public virtual WcfTaxonomy GetTaxonomy(string taxonomyId, string provider) => this.GetTaxonomyInternal(taxonomyId, provider);

    /// <summary>Gets the taxonomy in XML.</summary>
    /// <param name="taxonomyId">Id of the taxonomy to be retrieved.</param>
    /// <param name="provider">The provider.</param>
    /// <returns>
    /// An instance of <see cref="T:Telerik.Sitefinity.Taxonomies.Web.Services.WcfTaxonomy" /> object representing the taxonomy.
    /// </returns>
    public virtual WcfTaxonomy GetTaxonomyInXml(string taxonomyId, string provider) => this.GetTaxonomyInternal(taxonomyId, provider);

    /// <summary>
    /// Saves a taxonomy. If the taxonomy exists it updates it; otherwise creates a new taxonomy.
    /// Returns the saved taxonomy object in JSON format.
    /// </summary>
    /// <param name="taxonomy">Taxonomy object to be saved.</param>
    /// <param name="taxonomyId">Id of the taxonomy under which taxonomy ought to be saved.</param>
    /// <param name="provider">Name of the taxonomy provider to be used when saving the taxonomy.</param>
    /// <returns>
    /// An instance of <see cref="T:Telerik.Sitefinity.Taxonomies.Web.Services.WcfTaxonomy" /> that represents the saved taxonomy object.
    /// </returns>
    public virtual WcfTaxonomy SaveTaxonomy(
      WcfTaxonomy taxonomy,
      string taxonomyId,
      string provider)
    {
      return this.CreateTaxonomyInternal(taxonomy, taxonomyId, provider);
    }

    /// <summary>
    /// Saves a taxonomy. If the taxonomy exists it updates it; otherwise creates a new taxonomy.
    /// Returns the saved taxonomy object in XML format.
    /// </summary>
    /// <param name="taxonomy">Taxonomy object to be saved.</param>
    /// <param name="taxonomyId">Id of the taxonomy under which taxonomy ought to be saved.</param>
    /// <param name="provider">Name of the taxonomy provider to be used when saving the taxonomy.</param>
    /// <returns>
    /// An instance of <see cref="T:Telerik.Sitefinity.Taxonomies.Web.Services.WcfTaxonomy" /> that represents the saved taxonomy object.
    /// </returns>
    public virtual WcfTaxonomy SaveTaxonomyInXml(
      WcfTaxonomy taxonomy,
      string taxonomyId,
      string provider)
    {
      return this.CreateTaxonomyInternal(taxonomy, taxonomyId, provider);
    }

    /// <summary>
    /// Deletes a taxonomy and returns true if the taxonomy has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="taxonomyId">Id of the taxonomy that ought to be deleted.</param>
    /// <param name="provider">Name of the taxonomy provider to be used when deleting the taxonomy.</param>
    /// <param name="lang">The language to be deleted. If null, all versions will be deleted</param>
    /// <param name="deleteTaxaOnly">Indicates whether to delete all the taxonomy taxa only or delete the entire taxonomy itself.</param>
    /// <returns></returns>
    public virtual bool DeleteTaxonomy(
      string taxonomyId,
      string provider,
      string lang,
      bool deleteTaxaOnly)
    {
      return this.DeleteTaxonomyInternal(taxonomyId, provider, lang, deleteTaxaOnly);
    }

    /// <summary>
    /// Deletes a taxonomy and returns true if the taxonomy has been deleted; otherwise false.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="taxonomyId">Id of the taxonomy that ought to be deleted.</param>
    /// <param name="provider">Name of the taxonomy provider to be used when deleting the taxonomy.</param>
    /// <param name="lang">The language to be deleted. If null, all versions will be deleted</param>
    /// <param name="deleteTaxaOnly">Indicates whether to delete all the taxonomy taxa only or delete the entire taxonomy itself.</param>
    /// <returns></returns>
    public virtual bool DeleteTaxonomyInXml(
      string taxonomyId,
      string provider,
      string lang,
      bool deleteTaxaOnly)
    {
      return this.DeleteTaxonomyInternal(taxonomyId, provider, lang, deleteTaxaOnly);
    }

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
    public virtual void ShareTaxonomyWithSites(
      string[] siteIds,
      string taxonomyId,
      string provider)
    {
      this.ShareTaxonomyWithSitesInternal(siteIds, taxonomyId, provider);
    }

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
    public virtual void ShareTaxonomyWithSitesInXml(
      string[] siteIds,
      string taxonomyId,
      string provider)
    {
      this.ShareTaxonomyWithSitesInternal(siteIds, taxonomyId, provider);
    }

    /// <summary>Shares the taxonomy with the current site.</summary>
    /// <param name="taxonomyId">Id of the taxonomy under which taxonomy ought to be saved.</param>
    /// <param name="provider">Name of the taxonomy provider to be used when saving the taxonomy.</param>
    public virtual void ShareTaxonomyWithCurrentSite(string taxonomyId, string provider) => this.ShareTaxonomyWithCurrentSiteInternal(taxonomyId, provider);

    /// <summary>Shares the taxonomy with the current site.</summary>
    /// <param name="taxonomyId">Id of the taxonomy under which taxonomy ought to be saved.</param>
    /// <param name="provider">Name of the taxonomy provider to be used when saving the taxonomy.</param>
    public virtual void ShareTaxonomyWithCurrentInXml(string taxonomyId, string provider) => this.ShareTaxonomyWithCurrentSiteInternal(taxonomyId, provider);

    /// <summary>
    /// Splits the taxonomy with site.
    /// Returns the saved taxonomy object in JSON format.
    /// </summary>
    /// <param name="taxonomyId">Id of the taxonomy under which taxonomy ought to be saved.</param>
    /// <param name="provider">Name of the taxonomy provider to be used when saving the taxonomy.</param>
    /// <param name="duplicate">Duplicate current taxonomy for this site.</param>
    /// <returns></returns>
    public virtual WcfTaxonomy SplitTaxonomyWithSite(
      string taxonomyId,
      string provider,
      bool duplicate)
    {
      return this.SplitTaxonomyWithSiteInternal(taxonomyId, provider, duplicate);
    }

    /// <summary>
    /// Splits the taxonomy with site.
    /// Returns the saved taxonomy object in XML format.
    /// </summary>
    /// <param name="taxonomyId">Id of the taxonomy under which taxonomy ought to be saved.</param>
    /// <param name="provider">Name of the taxonomy provider to be used when saving the taxonomy.</param>
    /// <param name="duplicate">Duplicate current taxonomy for this site.</param>
    /// <returns></returns>
    public virtual WcfTaxonomy SplitTaxonomyWithSiteInXml(
      string taxonomyId,
      string provider,
      bool duplicate)
    {
      return this.SplitTaxonomyWithSiteInternal(taxonomyId, provider, duplicate);
    }

    /// <summary>Gets the shared sites for taxonomy.</summary>
    /// <param name="rootTaxonomyId">The root taxonomy identifier.</param>
    /// <param name="provider">The provider.</param>
    /// <returns></returns>
    public virtual Dictionary<Guid, string> GetSitesForTaxonomy(
      string rootTaxonomyId,
      string provider)
    {
      return this.GetSitesForTaxonomyInternal(rootTaxonomyId, provider);
    }

    /// <summary>Gets the shared sites for taxonomy in XML.</summary>
    /// <param name="rootTaxonomyId">The root taxonomy identifier.</param>
    /// <param name="provider">The provider.</param>
    /// <returns></returns>
    public virtual Dictionary<Guid, string> GetSitesForTaxonomyInXml(
      string rootTaxonomyId,
      string provider)
    {
      return this.GetSitesForTaxonomyInternal(rootTaxonomyId, provider);
    }

    /// <summary>
    /// Deletes an array of content items.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="Ids">An array containing the Ids of the content items to be deleted.</param>
    /// <param name="itemsType">The actual type of the content items to be deleted.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the item.</param>
    /// <param name="deletedLanguage">The language version to delete. If null, all versions are deleted(e.g. the item itself is deleted).</param>
    /// <param name="deleteTaxaOnly">Indicates whether to delete all the taxonomy taxa only or delete the entire taxonomy itself.</param>
    public virtual bool BatchDeleteTaxonomy(
      string[] ids,
      string itemType,
      string providerName,
      string deletedLanguage,
      bool deleteTaxaOnly)
    {
      return this.BatchDeleteTaxonomiesInternal(ids, providerName, deletedLanguage, deleteTaxaOnly);
    }

    /// <summary>
    /// Deletes an array of content items.
    /// Result is returned in Xml format.
    /// </summary>
    /// <param name="Ids">An array containing the Ids of the content items to be deleted.</param>
    /// <param name="itemsType">The actual type of the content items to be deleted.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the item.</param>
    /// <param name="deletedLanguage">The language version to delete. If null, all versions are deleted(e.g. the item itself is deleted).</param>
    /// <param name="deleteTaxaOnly">Indicates whether to delete all the taxonomy taxa only or delete the entire taxonomy itself.</param>
    public virtual bool BatchDeleteTaxonomyInXml(
      string[] ids,
      string itemType,
      string providerName,
      string deletedLanguage,
      bool deleteTaxaOnly)
    {
      return this.BatchDeleteTaxonomiesInternal(ids, providerName, deletedLanguage, deleteTaxaOnly);
    }

    private bool BatchDeleteTaxonomiesInternal(
      string[] ids,
      string providerName,
      string deletedLanguage,
      bool deleteTaxaOnly)
    {
      CultureInfo language = (CultureInfo) null;
      if (!string.IsNullOrEmpty(deletedLanguage))
        language = new CultureInfo(deletedLanguage);
      string str = string.Empty;
      Stack<string> stringStack1 = new Stack<string>();
      Stack<string> stringStack2 = new Stack<string>();
      foreach (string id in ids)
      {
        try
        {
          TaxonomyManager manager = TaxonomyManager.GetManager(providerName);
          ITaxonomy taxonomy = manager.GetTaxonomy(new Guid(id));
          str = (string) taxonomy.Title;
          this.DeleteTaxonomy(manager, taxonomy, language, deleteTaxaOnly);
          manager.SaveChanges();
        }
        catch (Exception ex)
        {
          stringStack2.Push(str);
          stringStack1.Push(ex.Message);
        }
      }
      if (stringStack1.Count == 1)
        throw new Exception(stringStack1.Pop());
      if (stringStack1.Count > 1)
      {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("Some or all taxonomies could not be deleted");
        while (stringStack1.Count > 0)
          stringBuilder.Append("\n{0}: {1}".Arrange((object) stringStack2.Pop(), (object) stringStack1.Pop()));
        throw new Exception(stringBuilder.ToString());
      }
      return true;
    }

    private CollectionContext<WcfTaxonomy> GetTaxonomiesInternal(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string taxonomyType,
      bool skipSiteContext)
    {
      TaxonomyManager manager = TaxonomyManager.GetManager(provider);
      if (string.IsNullOrEmpty(sortExpression))
        sortExpression = "LastModified DESC";
      int? totalCount = new int?(0);
      List<WcfTaxonomy> wcfTaxonomyList = new List<WcfTaxonomy>();
      if (taxonomyType == typeof (FlatTaxonomy).Name)
        this.GetRootTaxonomies<FlatTaxonomy, FlatTaxon>(manager, filter, sortExpression, skip, take, wcfTaxonomyList, ref totalCount, skipSiteContext);
      else if (taxonomyType == typeof (HierarchicalTaxonomy).Name)
        this.GetRootTaxonomies<HierarchicalTaxonomy, HierarchicalTaxon>(manager, filter, sortExpression, skip, take, wcfTaxonomyList, ref totalCount, skipSiteContext);
      else if (taxonomyType == typeof (NetworkTaxonomy).Name)
        this.GetRootTaxonomies<NetworkTaxonomy, NetworkTaxon>(manager, filter, sortExpression, skip, take, wcfTaxonomyList, ref totalCount, skipSiteContext);
      else if (taxonomyType == typeof (FacetTaxonomy).Name)
      {
        this.GetRootTaxonomies<FacetTaxonomy, FacetTaxon>(manager, filter, sortExpression, skip, take, wcfTaxonomyList, ref totalCount, skipSiteContext);
      }
      else
      {
        string filterName;
        IQueryable<Taxonomy> query;
        if (NamedFiltersHandler.TryParseFilterName(filter, out filterName))
          query = ObjectFactory.Container.Resolve<ITaxonomiesNamedFilterHandler>((ResolverOverride[]) new ParameterOverride[1]
          {
            new ParameterOverride("manager", (object) manager)
          }).GetFilteredTaxonomies(filterName);
        else
          query = manager.GetTaxonomies<Taxonomy>().Where<Taxonomy>((Expression<Func<Taxonomy, bool>>) (t => (Guid?) t.RootTaxonomyId.Value == new Guid?()));
        foreach (Taxonomy setExpression in (IEnumerable<Taxonomy>) DataProviderBase.SetExpressions<Taxonomy>(query, filter, sortExpression, new int?(skip), new int?(take), ref totalCount))
        {
          if (!TaxonomyManager.IsSystemTaxonomy((ITaxonomy) setExpression))
          {
            WcfTaxonomy taxonomy;
            if (setExpression.GetType() == typeof (FlatTaxonomy))
              taxonomy = this.TranslateTaxonomy<FlatTaxonomy, FlatTaxon>(manager, (ITaxonomy) setExpression, skipSiteContext);
            else if (setExpression.GetType() == typeof (HierarchicalTaxonomy))
              taxonomy = this.TranslateTaxonomy<HierarchicalTaxonomy, HierarchicalTaxon>(manager, (ITaxonomy) setExpression, skipSiteContext);
            else if (setExpression.GetType() == typeof (NetworkTaxonomy))
            {
              taxonomy = this.TranslateTaxonomy<NetworkTaxonomy, NetworkTaxon>(manager, (ITaxonomy) setExpression, skipSiteContext);
            }
            else
            {
              if (!(setExpression.GetType() == typeof (FacetTaxonomy)))
                throw new NotSupportedException();
              taxonomy = this.TranslateTaxonomy<FacetTaxonomy, FacetTaxon>(manager, (ITaxonomy) setExpression, skipSiteContext);
            }
            this.ProcessTaxonomyEditUrl(taxonomy, skipSiteContext);
            wcfTaxonomyList.Add(taxonomy);
          }
        }
      }
      ServiceUtility.DisableCache();
      return new CollectionContext<WcfTaxonomy>((IEnumerable<WcfTaxonomy>) wcfTaxonomyList)
      {
        TotalCount = totalCount.Value
      };
    }

    private void GetRootTaxonomies<TTaxonomy, TTaxon>(
      TaxonomyManager taxonomyManager,
      string filter,
      string sortExpression,
      int skip,
      int take,
      List<WcfTaxonomy> taxonomyList,
      ref int? totalCount,
      bool skipSiteContext)
      where TTaxonomy : class, ITaxonomy
      where TTaxon : class, ITaxon
    {
      IQueryable<TTaxonomy> taxonomies = taxonomyManager.GetTaxonomies<TTaxonomy>();
      Expression<Func<TTaxonomy, bool>> predicate = (Expression<Func<TTaxonomy, bool>>) (t => (Guid?) t.RootTaxonomyId.Value == new Guid?());
      foreach (TTaxonomy setExpression in (IEnumerable<TTaxonomy>) DataProviderBase.SetExpressions<TTaxonomy>(taxonomies.Where<TTaxonomy>(predicate), filter, sortExpression, new int?(skip), new int?(take), ref totalCount))
      {
        if (!TaxonomyManager.IsSystemTaxonomy((ITaxonomy) setExpression))
          taxonomyList.Add(this.TranslateTaxonomy<TTaxonomy, TTaxon>(taxonomyManager, (ITaxonomy) setExpression, skipSiteContext));
      }
    }

    private WcfTaxonomy GetTaxonomyInternal(string taxonomyId, string provider) => TaxonomyServiceHelper.GetWcfTaxonmyObject(TaxonomyManager.GetManager(provider).GetTaxonomy(new Guid(taxonomyId)));

    private WcfTaxonomy TranslateTaxonomy<TTaxonomy, TaxonType>(
      TaxonomyManager taxonomyManager,
      ITaxonomy taxonomy,
      bool skipSiteContext)
      where TTaxonomy : class, ITaxonomy
      where TaxonType : class, ITaxon
    {
      WcfTaxonomy wcfTaxonmyObject = TaxonomyServiceHelper.GetWcfTaxonmyObject(taxonomy);
      wcfTaxonmyObject.SharedSitesCount = TaxonomyServiceHelper.SetSharedSitesCount(taxonomyManager, taxonomy, skipSiteContext);
      Guid taxonomyId = this.GetSiteContextTaxonomyId<TTaxonomy>(taxonomyManager, taxonomy.Id, skipSiteContext);
      wcfTaxonmyObject.CurrentSiteTaxonomyId = taxonomyId;
      wcfTaxonmyObject.FirstTwoTaxons = taxonomyManager.GetTaxa<TaxonType>().Where<TaxonType>((Expression<Func<TaxonType, bool>>) (t => t.Taxonomy.Id == taxonomyId)).OrderBy<TaxonType, float>((Expression<Func<TaxonType, float>>) (t => t.Ordinal)).Skip<TaxonType>(0).Take<TaxonType>(2).ToList<TaxonType>().Select<TaxonType, string>((Func<TaxonType, string>) (t => t.Title.ToString())).ToArray<string>();
      wcfTaxonmyObject.TotalTaxaCount = taxonomyManager.GetTaxa<TaxonType>().Where<TaxonType>((Expression<Func<TaxonType, bool>>) (t => t.Taxonomy.Id == taxonomyId)).Count<TaxonType>();
      return wcfTaxonmyObject;
    }

    private void ProcessTaxonomyEditUrl(WcfTaxonomy taxonomy, bool skipSiteContext)
    {
      string str = "?";
      if (taxonomy.EditUrl.Contains("?"))
        str = "&";
      if (!skipSiteContext)
        return;
      taxonomy.EditUrl += string.Format("{0}skipSiteContext=true", (object) str);
    }

    /// <summary>
    /// Gets the site context taxonomy id based on the site context mode.
    /// </summary>
    /// <typeparam name="TTaxonomy">The type of the T taxonomy.</typeparam>
    /// <param name="taxonomyManager">The taxonomy manager.</param>
    /// <param name="rootTaxonomyId">The root taxonomy id.</param>
    /// <param name="skipSiteContext">The skip site context.</param>
    /// <returns></returns>
    private Guid GetSiteContextTaxonomyId<TTaxonomy>(
      TaxonomyManager taxonomyManager,
      Guid rootTaxonomyId,
      bool skipSiteContext)
      where TTaxonomy : class, ITaxonomy
    {
      return !skipSiteContext ? ((object) taxonomyManager.GetSiteTaxonomy<TTaxonomy>(rootTaxonomyId) as Taxonomy).Id : rootTaxonomyId;
    }

    private WcfTaxonomy CreateTaxonomyInternal(
      WcfTaxonomy wcfTaxonomy,
      string taxonomyId,
      string provider)
    {
      TaxonomyManager manager = TaxonomyManager.GetManager(provider);
      Guid id = new Guid(taxonomyId);
      ITaxonomy taxonomy1;
      if (id != Guid.Empty)
      {
        taxonomy1 = manager.GetTaxonomy(id);
      }
      else
      {
        string urlName = string.IsNullOrEmpty(wcfTaxonomy.Name) ? wcfTaxonomy.Title : wcfTaxonomy.Name;
        this.ValidateConstrains(manager, urlName);
        if (wcfTaxonomy.Type == typeof (FlatTaxonomy).Name)
        {
          taxonomy1 = (ITaxonomy) manager.CreateTaxonomy<FlatTaxonomy>();
        }
        else
        {
          if (!(wcfTaxonomy.Type == typeof (HierarchicalTaxonomy).Name))
            throw new NotSupportedException();
          taxonomy1 = (ITaxonomy) manager.CreateTaxonomy<HierarchicalTaxonomy>();
        }
      }
      taxonomy1.Description = (Lstring) wcfTaxonomy.Description;
      taxonomy1.Title = (Lstring) wcfTaxonomy.Title;
      taxonomy1.Name = wcfTaxonomy.Name;
      taxonomy1.TaxonName = (Lstring) wcfTaxonomy.SingleItemName;
      manager.SaveChanges();
      WcfTaxonomy taxonomy2;
      if (wcfTaxonomy.Type == typeof (FlatTaxonomy).Name)
        taxonomy2 = this.TranslateTaxonomy<FlatTaxonomy, FlatTaxon>(manager, taxonomy1, false);
      else if (wcfTaxonomy.Type == typeof (HierarchicalTaxonomy).Name)
        taxonomy2 = this.TranslateTaxonomy<HierarchicalTaxonomy, HierarchicalTaxon>(manager, taxonomy1, false);
      else if (wcfTaxonomy.Type == typeof (NetworkTaxonomy).Name)
      {
        taxonomy2 = this.TranslateTaxonomy<NetworkTaxonomy, NetworkTaxon>(manager, taxonomy1, false);
      }
      else
      {
        if (!(wcfTaxonomy.Type == typeof (FacetTaxonomy).Name))
          throw new NotSupportedException();
        taxonomy2 = this.TranslateTaxonomy<FacetTaxonomy, FacetTaxon>(manager, taxonomy1, false);
      }
      this.ProcessTaxonomyEditUrl(taxonomy2, false);
      return taxonomy2;
    }

    private void ValidateConstrains(TaxonomyManager taxonomyManager, string urlName) => taxonomyManager.ValidateTaxonomyConstraints(urlName, Guid.Empty);

    private bool DeleteTaxonomyInternal(
      string taxonomyId,
      string provider,
      string lang,
      bool deleteTaxaOnly)
    {
      CultureInfo language = (CultureInfo) null;
      if (!string.IsNullOrEmpty(lang))
        language = new CultureInfo(lang);
      Guid id = new Guid(taxonomyId);
      TaxonomyManager manager = TaxonomyManager.GetManager(provider);
      ITaxonomy taxonomy = manager.GetTaxonomy(id);
      this.DeleteTaxonomy(manager, taxonomy, language, deleteTaxaOnly);
      manager.SaveChanges();
      return true;
    }

    private void ShareTaxonomyWithSitesInternal(
      string[] siteIds,
      string taxonomyId,
      string provider)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      Guid id = new Guid(taxonomyId);
      TaxonomyManager manager = TaxonomyManager.GetManager(provider);
      ITaxonomy taxonomy = manager.GetTaxonomy(id);
      ISiteTaxonomyLinker siteTaxonomyLinker = ObjectFactory.Container.Resolve<ISiteTaxonomyLinker>((ResolverOverride[]) new ParameterOverride[1]
      {
        new ParameterOverride("manager", (object) manager)
      });
      foreach (string siteId in siteIds)
      {
        Guid targetSiteId = new Guid(siteId);
        siteTaxonomyLinker.UseTaxonomyInSite(taxonomy, targetSiteId, false);
      }
      manager.SaveChanges();
    }

    private void ShareTaxonomyWithCurrentSiteInternal(string strTaxonomyId, string provider)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      Guid id1 = Guid.Parse(strTaxonomyId);
      TaxonomyManager manager = TaxonomyManager.GetManager(provider);
      ITaxonomy taxonomy1 = manager.GetTaxonomy(id1);
      ISiteTaxonomyLinker siteTaxonomyLinker = ObjectFactory.Container.Resolve<ISiteTaxonomyLinker>((ResolverOverride[]) new ParameterOverride[1]
      {
        new ParameterOverride("manager", (object) manager)
      });
      Guid id2 = SystemManager.CurrentContext.MultisiteContext.CurrentSite.Id;
      ITaxonomy taxonomy2 = taxonomy1;
      Guid targetSiteId = id2;
      siteTaxonomyLinker.UseTaxonomyInSite(taxonomy2, targetSiteId);
    }

    private WcfTaxonomy SplitTaxonomyWithSiteInternal(
      string strTaxonomyId,
      string provider,
      bool isDuplicate)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      Guid guid = Guid.Parse(strTaxonomyId);
      TaxonomyManager manager = TaxonomyManager.GetManager(provider);
      Taxonomy taxonomy;
      switch (manager.GetTaxonomy(guid))
      {
        case FlatTaxonomy _:
          taxonomy = (Taxonomy) this.SplitOrDuplicateTaxonomy<FlatTaxonomy>(guid, manager, isDuplicate);
          break;
        case HierarchicalTaxonomy _:
          taxonomy = (Taxonomy) this.SplitOrDuplicateTaxonomy<HierarchicalTaxonomy>(guid, manager, isDuplicate);
          break;
        case NetworkTaxonomy _:
          taxonomy = (Taxonomy) this.SplitOrDuplicateTaxonomy<NetworkTaxonomy>(guid, manager, isDuplicate);
          break;
        case FacetTaxonomy _:
          taxonomy = (Taxonomy) this.SplitOrDuplicateTaxonomy<FacetTaxonomy>(guid, manager, isDuplicate);
          break;
        default:
          throw new NotSupportedException();
      }
      return TaxonomyServiceHelper.GetWcfTaxonmyObject((ITaxonomy) taxonomy);
    }

    private T SplitOrDuplicateTaxonomy<T>(
      Guid taxonomyId,
      TaxonomyManager taxonomyManager,
      bool isDuplicate)
      where T : class, ITaxonomy
    {
      return ObjectFactory.Container.Resolve<ISiteTaxonomyLinker>((ResolverOverride[]) new ParameterOverride[1]
      {
        new ParameterOverride("manager", (object) taxonomyManager)
      }).SplitSiteTaxonomy<T>(taxonomyId, duplicateTaxa: isDuplicate);
    }

    private Dictionary<Guid, string> GetSitesForTaxonomyInternal(
      string rootTaxonomyId,
      string provider)
    {
      Guid id = Guid.Parse(rootTaxonomyId);
      TaxonomyManager manager = TaxonomyManager.GetManager(provider);
      ITaxonomy rootTaxonomy = manager.GetTaxonomy(id);
      Dictionary<Guid, string> taxonomyInternal = new Dictionary<Guid, string>();
      IQueryable<Taxonomy> taxonomies = manager.GetTaxonomies<Taxonomy>();
      Expression<Func<Taxonomy, bool>> predicate = (Expression<Func<Taxonomy, bool>>) (t => t.RootTaxonomyId == (Guid?) rootTaxonomy.Id);
      foreach (Taxonomy taxonomy in (IEnumerable<Taxonomy>) taxonomies.Where<Taxonomy>(predicate))
      {
        List<string> list = manager.GetRelatedSites((ITaxonomy) taxonomy).Select<ISite, string>((Func<ISite, string>) (s => s.Name)).OrderBy<string, string>((Func<string, string>) (n => n)).ToList<string>();
        if (list.Count<string>() > 0)
          taxonomyInternal.Add(taxonomy.Id, this.FormatStringSitesForTaxonomy(list));
      }
      List<string> list1 = manager.GetRelatedSites(rootTaxonomy).Select<ISite, string>((Func<ISite, string>) (s => s.Name)).OrderBy<string, string>((Func<string, string>) (n => n)).ToList<string>();
      if (list1.Count<string>() > 0)
        taxonomyInternal.Add(rootTaxonomy.Id, this.FormatStringSitesForTaxonomy(list1));
      return taxonomyInternal;
    }

    private string FormatStringSitesForTaxonomy(List<string> sites) => sites.Count<string>() > 3 ? string.Join(", ", sites.Take<string>(3)) + string.Format(Res.Get(typeof (TaxonomyResources), "AndNumberMore"), (object) (sites.Count<string>() - 3)) : string.Join(", ", (IEnumerable<string>) sites);

    private void DeleteTaxonomy(
      TaxonomyManager manager,
      ITaxonomy taxonomy,
      CultureInfo language,
      bool deleteTaxaOnly)
    {
      if (deleteTaxaOnly && taxonomy.IsRootTaxonomy())
        manager.DeleteAllTaxa(taxonomy, language);
      else
        manager.DeleteInAllSites(taxonomy, language);
    }

    private MultisiteTaxonomyGuard GetMultisiteTaxonomyGuard(
      TaxonomyManager taxonomyManager = null)
    {
      if (taxonomyManager == null)
        return ObjectFactory.Container.Resolve<MultisiteTaxonomyGuard>();
      return ObjectFactory.Container.Resolve<MultisiteTaxonomyGuard>((ResolverOverride[]) new ParameterOverride[1]
      {
        new ParameterOverride("manager", (object) taxonomyManager)
      });
    }
  }
}
