// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.SiteTaxonomyLinker
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Taxonomies
{
  /// <summary>
  /// A component dedicated to multisite operations for linking and unlinking of taxonomies and sites
  /// </summary>
  internal class SiteTaxonomyLinker : ISiteTaxonomyLinker
  {
    private MultisiteTaxonomyGuard multisiteTaxonomyGuard;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Taxonomies.SiteTaxonomyLinker" /> class.
    /// </summary>
    public SiteTaxonomyLinker()
      : this((TaxonomyManager) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Taxonomies.SiteTaxonomyLinker" /> class.
    /// </summary>
    /// <param name="taxonomyManager">The taxonomy manager.</param>
    public SiteTaxonomyLinker(TaxonomyManager taxonomyManager)
    {
      if (taxonomyManager == null)
        taxonomyManager = TaxonomyManager.GetManager();
      this.TaxonomyManager = taxonomyManager;
    }

    internal MultisiteTaxonomyGuard MultisiteTaxonomyGuard
    {
      get
      {
        if (this.multisiteTaxonomyGuard == null)
          this.multisiteTaxonomyGuard = ObjectFactory.Container.Resolve<MultisiteTaxonomyGuard>((ResolverOverride[]) new ParameterOverride[1]
          {
            new ParameterOverride("manager", (object) this.TaxonomyManager)
          });
        return this.multisiteTaxonomyGuard;
      }
      set => this.multisiteTaxonomyGuard = value;
    }

    private TaxonomyManager TaxonomyManager { get; set; }

    /// <summary>
    /// Creates a new split taxonomy for the specified site. The new split taxonomy is a copy of the currently used taxonomy by the site.
    /// The new split taxonomy is linked to the site.
    /// </summary>
    /// <typeparam name="TTaxonomy">The type of the taxonomy.</typeparam>
    /// <param name="rootTaxonomyId">The root taxonomy id.</param>
    /// <param name="siteId">The site id.</param>
    /// <remarks> In case that no site is specified then the site from the current context is used.</remarks>
    /// <param name="duplicateTaxa">A flag indicating if the newly created split taxonomy should have duplicates of the original taxonomy taxa.</param>
    /// <param name="performSaveChanges">A flag indicating if the method should perform save changes.</param>
    /// <returns>Returns the newly created split taxonomy.</returns>
    public virtual TTaxonomy SplitSiteTaxonomy<TTaxonomy>(
      Guid rootTaxonomyId,
      Guid? siteId = null,
      bool duplicateTaxa = false,
      bool performSaveChanges = true)
      where TTaxonomy : class, ITaxonomy
    {
      TTaxonomy taxonomy = this.TaxonomyManager.Provider.GetTaxonomy<TTaxonomy>(rootTaxonomyId);
      TTaxonomy sourceTaxonomy = default (TTaxonomy);
      if (duplicateTaxa)
        sourceTaxonomy = this.TaxonomyManager.GetSiteTaxonomy<TTaxonomy>(taxonomy.Id, siteId);
      TTaxonomy targetTaxonomy = this.TaxonomyManager.SplitSiteTaxonomy<TTaxonomy>(taxonomy, siteId);
      if (duplicateTaxa)
        this.TaxonomyManager.CopyTaxa<TTaxonomy>(sourceTaxonomy, targetTaxonomy);
      if (performSaveChanges)
        this.TaxonomyManager.SaveChanges();
      return targetTaxonomy;
    }

    /// <summary>Arranges the target site to use the given taxonomy.</summary>
    /// <param name="taxonomyId">The taxonomy id to be used.</param>
    /// <param name="targetSiteId">The target site id.</param>
    /// <param name="performSaveChanges">A flag indicating if the method should perform save changes.</param>
    public virtual void UseTaxonomyInSite(
      Guid taxonomyId,
      Guid targetSiteId,
      bool performSaveChanges = true)
    {
      this.TaxonomyManager.UseTaxonomyInSite(this.TaxonomyManager.GetTaxonomy(taxonomyId), targetSiteId);
      if (!performSaveChanges)
        return;
      this.TaxonomyManager.SaveChanges();
    }

    /// <summary>Arranges the target site to use the given taxonomy.</summary>
    /// <param name="taxonomy">The taxonomy to be used.</param>
    /// <param name="targetSiteId">The target site id.</param>
    /// <param name="performSaveChanges">A flag indicating if the method should perform save changes.</param>
    public virtual void UseTaxonomyInSite(
      ITaxonomy taxonomy,
      Guid targetSiteId,
      bool performSaveChanges = true)
    {
      this.TaxonomyManager.UseTaxonomyInSite(taxonomy, targetSiteId);
      if (!performSaveChanges)
        return;
      this.TaxonomyManager.SaveChanges();
    }

    /// <summary>
    /// Breaks the link between a taxonomy and a site.
    /// Removes all taxa values of the given taxonomy from all content items in the site that no longer have intersection with that taxonomy in any site.
    /// </summary>
    /// <param name="taxonomyId">The taxonomy id (root or split).</param>
    /// <param name="siteId">The site id.</param>
    public virtual void CleanOrphanedSiteTaxonomyUsage(Guid taxonomyId, Guid siteId)
    {
      MultisiteManager manager = MultisiteManager.GetManager();
      ITaxonomy taxonomy = this.TaxonomyManager.GetTaxonomy(taxonomyId);
      List<Guid> list = taxonomy.Taxa.Select<ITaxon, Guid>((Func<ITaxon, Guid>) (t => t.Id)).ToList<Guid>();
      Guid[] array1 = this.TaxonomyManager.GetRelatedSites(taxonomy).Select<ISite, Guid>((Func<ISite, Guid>) (s => s.Id)).ToArray<Guid>();
      Site[] array2 = manager.GetSites().ToArray<Site>();
      Site site = ((IEnumerable<Site>) array2).SingleOrDefault<Site>((Func<Site, bool>) (s => s.Id == siteId));
      foreach (KeyValuePair<Type, List<string>> keyValuePair in this.GetTypesAndFieldsUsingTaxonomy(taxonomy))
      {
        Type key = keyValuePair.Key;
        List<string> itemTypeTaxonomyFields = keyValuePair.Value;
        IManager mappedManager = ManagerBase.GetMappedManager(key);
        string dataSourceName = this.GetDataSourceName(mappedManager, key);
        foreach (string providerName in mappedManager.GetProviderNames(ProviderBindingOptions.NoFilter))
        {
          string itemProviderName = providerName;
          if (this.DoesSiteUseProviderOrDefault(site, dataSourceName, itemProviderName) && ((IEnumerable<Guid>) ((IEnumerable<Site>) array2).Where<Site>((Func<Site, bool>) (s => this.DoesSiteUseProvider(s, dataSourceName, itemProviderName))).Select<Site, Guid>((Func<Site, Guid>) (s => s.Id)).ToArray<Guid>()).Intersect<Guid>((IEnumerable<Guid>) array1).Count<Guid>() == 0)
            this.BreakLinkBetweenTaxoniomyAndTypeItems(key, itemProviderName, itemTypeTaxonomyFields, list);
        }
      }
    }

    /// <summary>
    /// Checks if the specified site uses the specified provider. If the site is null assumes that the provider must be processed (returns <c>True</c>
    /// </summary>
    /// <param name="site">The site.</param>
    /// <param name="dataSourceName">Name of the data source.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns>
    /// Returns <c>True</c> if the specified site uses the specified provider and <c>False</c> otherwise. Also returns <c>True</c> if the site is null.
    /// </returns>
    private bool DoesSiteUseProviderOrDefault(
      Site site,
      string dataSourceName,
      string providerName)
    {
      return site == null || this.DoesSiteUseProvider(site, dataSourceName, providerName);
    }

    /// <summary>
    /// Checks if the specified site uses the specified provider.
    /// </summary>
    /// <param name="site">The site.</param>
    /// <param name="dataSourceName">Name of the data source.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns>
    /// Returns <c>True</c> if the specified site uses the specified provider and <c>False</c> otherwise.
    /// </returns>
    private bool DoesSiteUseProvider(Site site, string dataSourceName, string providerName) => site.SiteDataSourceLinks.Any<SiteDataSourceLink>((Func<SiteDataSourceLink, bool>) (sl => sl.DataSource.Name == dataSourceName && sl.DataSource.Provider == providerName));

    /// <summary>
    /// Gets the name of the data source for the given item type in the given manager.
    /// </summary>
    /// <param name="itemManager">The item manager.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <returns>The name of the data source holding the given item type.</returns>
    private string GetDataSourceName(IManager itemManager, Type itemType) => itemManager is DynamicModuleManager ? ModuleBuilderManager.GetManager().GetDynamicModuleType(itemType).ModuleName : itemManager.GetType().FullName;

    /// <summary>
    /// Gets a dictionary of all the types using the specified taxonomy and for every type all the field names using the taxonomy.
    /// </summary>
    /// <param name="taxonomy">The taxonomy.</param>
    /// <returns>A dictionary of all the types using a specified taxonomy. For every type there is a list of field names using the taxonomy.</returns>
    private Dictionary<Type, List<string>> GetTypesAndFieldsUsingTaxonomy(
      ITaxonomy taxonomy)
    {
      Guid rootTaxonomyId = taxonomy.RootTaxonomyId ?? taxonomy.Id;
      Dictionary<Type, List<string>> fieldsUsingTaxonomy = new Dictionary<Type, List<string>>();
      IQueryable<MetaField> metafields = MetadataManager.GetManager().GetMetafields();
      Expression<Func<MetaField, bool>> predicate = (Expression<Func<MetaField, bool>>) (mf => mf.TaxonomyId == rootTaxonomyId);
      foreach (MetaField metaField in metafields.Where<MetaField>(predicate).ToArray<MetaField>())
      {
        Type clrType = metaField.Parent.ClrType;
        if (!fieldsUsingTaxonomy.ContainsKey(clrType))
          fieldsUsingTaxonomy[clrType] = new List<string>();
        fieldsUsingTaxonomy[clrType].Add(metaField.FieldName);
      }
      return fieldsUsingTaxonomy;
    }

    /// <summary>
    /// Breaks the link between a taxonomy taxa and all the items of a specified type in a specified provider.
    /// </summary>
    /// <param name="itemType">Type of the items.</param>
    /// <param name="providerName">Name of the provider holding the items.</param>
    /// <param name="itemTypeTaxonomyFields">The list of field names using the specified taxonomy for the specified item type.</param>
    /// <param name="taxaGuidsToUnlink">The list of Guids of all the taxonomy taxa that the method needs to unlink.</param>
    private void BreakLinkBetweenTaxoniomyAndTypeItems(
      Type itemType,
      string providerName,
      List<string> itemTypeTaxonomyFields,
      List<Guid> taxaGuidsToUnlink)
    {
      IManager mappedManager = ManagerBase.GetMappedManager(itemType, providerName);
      int skip = 0;
      int take = 500;
      IQueryable<object> items = (IQueryable<object>) mappedManager.GetItems(itemType, string.Empty, string.Empty, skip, take);
      for (bool flag = items.Any<object>(); flag; flag = items.Any<object>())
      {
        foreach (object obj in (IEnumerable<object>) items)
          this.RemoveItemLinkToTaxons(obj, itemTypeTaxonomyFields, taxaGuidsToUnlink);
        mappedManager.Provider.CommitTransaction();
        skip += take;
        items = (IQueryable<object>) mappedManager.GetItems(itemType, string.Empty, string.Empty, skip, take);
      }
    }

    /// <summary>
    /// Removes the link between the specified item and the specified list of taxonomy taxa.
    /// The method expects the list of field names that are using the taxonomy for the item type.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="itemTaxonomyFields">The list of field names using the taxonomy for the item type.</param>
    /// <param name="taxaGuidsToUnlink">The list of Guids of taxonomy taxa to unlink.</param>
    private void RemoveItemLinkToTaxons(
      object item,
      List<string> itemTaxonomyFields,
      List<Guid> taxaGuidsToUnlink)
    {
      foreach (string itemTaxonomyField in itemTaxonomyFields)
      {
        Guid[] array = ((IEnumerable<Guid>) ((IDynamicFieldsContainer) item).GetValue<IList<Guid>>(itemTaxonomyField).ToArray<Guid>()).Intersect<Guid>((IEnumerable<Guid>) taxaGuidsToUnlink).ToArray<Guid>();
        if (array.Length != 0)
          ((IOrganizable) item).Organizer.RemoveTaxa(itemTaxonomyField, array);
      }
    }
  }
}
