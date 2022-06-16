// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.TaxonomyManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Microsoft.Practices.Unity.Utility;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.Localization;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Packaging;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies.Configuration;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Taxonomies.Proxy;
using Telerik.Sitefinity.Taxonomies.Web;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Services;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Taxonomies
{
  /// <summary>
  /// Represents an intermediary between taxonomy objects and taxonomy data.
  /// </summary>
  [BlankItemDelegate(typeof (TaxonomyWebServiceBlankItemGenerator))]
  public class TaxonomyManager : 
    ManagerBase<TaxonomyDataProvider>,
    ITaxonomyManager,
    IManager,
    IDisposable,
    IProviderResolver,
    IMultisiteEnabledManager
  {
    internal const string MODULE_NAME = "Taxonomy";
    /// <summary>Identifier for the built-in Tags flat taxonomy</summary>
    public static readonly Guid TagsTaxonomyId = new Guid("CB0F3A19-A211-48a7-88EC-77495C0F5374");
    /// <summary>
    /// Identified for the built-in Categories hierarchical taxonomy
    /// </summary>
    public static readonly Guid CategoriesTaxonomyId = new Guid("E5CD6D69-1543-427b-AD62-688A99F5E7D4");
    /// <summary>
    /// Identified for the built-in Department hierarchical taxonomy
    /// </summary>
    public static readonly Guid DepartmentsTaxonomyId = new Guid("D7831091-E7B1-41B8-9E75-DFF32D6A7837");
    /// <summary>
    /// The name of the metafield created in each content item to associate it with a tags (flat taxons)
    /// </summary>
    public static readonly string TagsMetafieldName = "Tags";
    /// <summary>
    /// The name of the metafield created in each content item to associate it with categories (hierarchical taxons)
    /// </summary>
    public static readonly string CategoriesMetafieldName = "Category";
    /// <summary>
    /// The name of the metafield created in each content item to associate it with departments (hierarchical taxons)
    /// </summary>
    public static readonly string DepartmentsMetafieldName = "Department";
    private MultisiteTaxonomyGuard multisiteTaxonomyGuard;
    private const string DeletedTaxonsExecutionStateKey = "deleted_taxons_execution_state";
    private const string IgnoreSiteContextParam = "sf_ignore_site_context";
    internal const string HierarchicalTaxonFieldControlTagResource = "Telerik.Sitefinity.Resources.Templates.Fields.HierarchicalTaxonFieldReadMode.ascx";
    internal const string FlatTaxonFieldControlTagResource = "Telerik.Sitefinity.Resources.Templates.Fields.FlatTaxonFieldReadMode.ascx";
    private static readonly object TaxonomyCacheSync = new object();
    private const string TaxonomiesCacheKey = "sf_taxonomies_cache";
    private static readonly ReadOnlyCollection<Guid> SystemTaxonomies = new ReadOnlyCollection<Guid>((IList<Guid>) new Guid[1]
    {
      SiteInitializer.PageTemplatesTaxonomyId
    });

    /// <summary>
    /// Initializes the <see cref="T:Telerik.Sitefinity.Taxonomies.TaxonomyManager" /> class.
    /// </summary>
    static TaxonomyManager()
    {
      ManagerBase<TaxonomyDataProvider>.Executing += new EventHandler<ExecutingEventArgs>(TaxonomyManager.Provider_Executing);
      ManagerBase<TaxonomyDataProvider>.Executed += new EventHandler<ExecutedEventArgs>(TaxonomyManager.Provider_Executed);
    }

    /// <summary>
    /// Initializes a new instance of <see cref="T:Telerik.Sitefinity.Taxonomies.TaxonomyManager" /> class with the default provider.
    /// </summary>
    public TaxonomyManager()
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="T:Telerik.Sitefinity.Taxonomies.TaxonomyManager" /> class and sets the specified provider.
    /// </summary>
    /// <param name="providerName">
    /// The name of the provider. If empty string or null the default provider is set.
    /// </param>
    public TaxonomyManager(string providerName)
      : base(providerName)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Taxonomies.TaxonomyManager" /> class.
    /// </summary>
    /// <param name="providerName">
    /// The name of the provider. If empty string or null the default provider is set
    /// </param>
    /// <param name="transactionName">
    /// The name of a distributed transaction. If empty string or null this manager will use separate transaction.
    /// </param>
    public TaxonomyManager(string providerName, string transactionName)
      : base(providerName, transactionName)
    {
    }

    /// <summary>Creates new taxonomy.</summary>
    /// <typeparam name="TTaxonomy">The type of the taxonomy.</typeparam>
    /// <returns>The new taxonomy.</returns>
    public virtual TTaxonomy CreateTaxonomy<TTaxonomy>() where TTaxonomy : class, ITaxonomy => this.Provider.CreateTaxonomy<TTaxonomy>();

    /// <summary>Creates new taxonomy with the provided ID.</summary>
    /// <typeparam name="TTaxonomy">The type of the taxonomy.</typeparam>
    /// <param name="pageId">The pageId.</param>
    /// <returns>The new taxonomy.</returns>
    public virtual TTaxonomy CreateTaxonomy<TTaxonomy>(Guid id) where TTaxonomy : class, ITaxonomy => this.Provider.CreateTaxonomy<TTaxonomy>(id);

    /// <summary>Deletes the taxonomy.</summary>
    /// <param name="taxonomy">The taxonomy.</param>
    public virtual void Delete(ITaxonomy taxonomy)
    {
      if (MetadataManager.GetManager().GetMetafields().Any<MetaField>((Expression<Func<MetaField, bool>>) (mf => mf.TaxonomyId == taxonomy.Id)))
        throw new InvalidOperationException("{0} could not be deleted because it is used by different content types.\nTo be able to delete it you must remove it from the content types first.".Arrange((object) taxonomy.Title));
      this.Provider.Delete(taxonomy);
    }

    /// <summary>
    /// Deletes a taxonomy and all split taxonomies linked to it.
    /// </summary>
    /// <param name="taxonomy">The taxonomy.</param>
    internal void DeleteInAllSites(ITaxonomy taxonomy)
    {
      if (taxonomy.IsRootTaxonomy())
      {
        IQueryable<Taxonomy> taxonomies = this.GetTaxonomies<Taxonomy>();
        Expression<Func<Taxonomy, bool>> predicate = (Expression<Func<Taxonomy, bool>>) (t => t.RootTaxonomyId == (Guid?) taxonomy.Id);
        foreach (ITaxonomy taxonomy1 in taxonomies.Where<Taxonomy>(predicate).ToArray<Taxonomy>())
          this.Delete(taxonomy1);
      }
      this.Delete(taxonomy);
    }

    /// <summary>Gets the taxonomy with the specified ID.</summary>
    /// <param name="pageId">The ID.</param>
    /// <returns></returns>
    public virtual ITaxonomy GetTaxonomy(Guid id) => this.Provider.GetTaxonomy(id);

    /// <summary>Gets the taxonomy with the specified ID.</summary>
    /// <typeparam name="TTaxonomy">The type of the taxonomy.</typeparam>
    /// <param name="pageId">The ID.</param>
    /// <returns></returns>
    public virtual TTaxonomy GetTaxonomy<TTaxonomy>(Guid id) where TTaxonomy : class, ITaxonomy => this.Provider.GetTaxonomy<TTaxonomy>(id);

    /// <summary>Gets the taxonomies.</summary>
    /// <typeparam name="TTaxonomy">The type of the taxonomies to get.</typeparam>
    /// <returns></returns>
    public virtual IQueryable<TTaxonomy> GetTaxonomies<TTaxonomy>() where TTaxonomy : class, ITaxonomy => this.Provider.GetTaxonomies<TTaxonomy>();

    /// <summary>Creates the taxon.</summary>
    /// <typeparam name="TTaxon">The type of the taxon.</typeparam>
    /// <returns></returns>
    public virtual TTaxon CreateTaxon<TTaxon>() where TTaxon : class, ITaxon => this.Provider.CreateTaxon<TTaxon>();

    /// <summary>Creates the taxon.</summary>
    /// <typeparam name="TTaxon">The type of the taxon.</typeparam>
    /// <param name="pageId">The pageId.</param>
    /// <returns></returns>
    public virtual TTaxon CreateTaxon<TTaxon>(Guid id) where TTaxon : class, ITaxon => this.Provider.CreateTaxon<TTaxon>(id);

    /// <summary>Deletes the statistic item by its Id.</summary>
    /// <param name="id"></param>
    internal void DeleteStatistic(TaxonomyStatistic statistic)
    {
      if (statistic == null)
        throw new ArgumentNullException(nameof (statistic));
      this.Provider.DeleteStatistic(statistic);
    }

    /// <summary>Gets the taxon with the specified ID.</summary>
    /// <param name="pageId">The ID of the taxon.</param>
    /// <returns>The taxon.</returns>
    public virtual ITaxon GetTaxon(Guid id) => this.Provider.GetTaxon(id);

    /// <summary>Gets the taxon with the specified ID.</summary>
    /// <typeparam name="TTaxon">The type of the taxon.</typeparam>
    /// <param name="pageId">The ID of the taxon.</param>
    /// <returns>The taxon.</returns>
    public virtual TTaxon GetTaxon<TTaxon>(Guid id) where TTaxon : class, ITaxon => this.Provider.GetTaxon<TTaxon>(id);

    /// <summary>
    /// Gets a query for all taxon objects from all taxonomies of the given type.
    /// </summary>
    /// <typeparam name="TTaxon">The type of the taxon.</typeparam>
    /// <returns>The query.</returns>
    public virtual IQueryable<TTaxon> GetTaxa<TTaxon>() where TTaxon : class, ITaxon => (IQueryable<TTaxon>) this.Provider.GetTaxa<TTaxon>().OrderBy<TTaxon, Lstring>((Expression<Func<TTaxon, Lstring>>) (t => t.Title));

    /// <summary>Deletes the taxon.</summary>
    /// <param name="taxon">The taxon.</param>
    public virtual void Delete(ITaxon taxon)
    {
      this.Provider.Delete(taxon);
      if (!(taxon is IHasTrackingContext context))
        return;
      IEnumerable<IHasTrackingContext> children = (IEnumerable<IHasTrackingContext>) null;
      if (taxon is HierarchicalTaxon hierarchicalTaxon)
        children = (IEnumerable<IHasTrackingContext>) hierarchicalTaxon.Subtaxa;
      context.RegisterDeletedOperation(children: children);
    }

    /// <summary>
    /// Deletes the specified language version of the given item. If no language is
    /// specified or the specified language is the only available for the item,
    /// then the item itself is deleted.
    /// </summary>
    /// <param name="taxon">The item.</param>
    /// <param name="language">The language version to delete.</param>
    public virtual void Delete(ITaxon taxon, CultureInfo language)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      string[] availableLanguages = this.GetAvailableLanguages((object) taxon, false);
      if (language == null || availableLanguages.Length == 0 || this.IsTheLastAvailableLanguage(language, availableLanguages))
      {
        this.Delete(taxon);
        ((IHasTrackingContext) taxon).RegisterDeletedOperation(availableLanguages);
      }
      else
        this.DeleteItemTranslation((IHasTrackingContext) taxon, language);
    }

    /// <summary>
    /// Deletes the specified language version of the given item. If no language is
    /// specified or the specified language is the only available for the item,
    /// then the item itself is deleted.
    /// </summary>
    /// <param name="taxon">The item.</param>
    /// <param name="language">The language version to delete.</param>
    public virtual void Delete(ITaxonomy taxonomy, CultureInfo language)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      string[] availableLanguages = this.GetAvailableLanguages((object) taxonomy, false);
      if (this.IsTheLastAvailableLanguage(language, availableLanguages))
      {
        this.Delete(taxonomy);
        ((IHasTrackingContext) taxonomy).RegisterDeletedOperation(availableLanguages);
      }
      else
        this.DeleteItemTranslation((IHasTrackingContext) taxonomy, language);
    }

    /// <summary>
    /// Deletes the specified language version of the given taxonomy. If no language is
    /// specified or the specified language is the only available for the taxonomy,
    /// then the taxonomy itself is deleted and all split taxonomies linked to it.
    /// </summary>
    /// <param name="taxon">The item.</param>
    /// <param name="language">The language version to delete.</param>
    internal void DeleteInAllSites(ITaxonomy taxonomy, CultureInfo language)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      string[] availableLanguages = this.GetAvailableLanguages((object) taxonomy, false);
      bool flag = language != null && language.Name.Equals(SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage.Name) && availableLanguages.Length == 0;
      if (this.IsTheLastAvailableLanguage(language, availableLanguages) | flag)
      {
        this.DeleteInAllSites(taxonomy);
        ((IHasTrackingContext) taxonomy).RegisterDeletedOperation(availableLanguages);
      }
      else
        this.DeleteItemTranslation((IHasTrackingContext) taxonomy, language);
    }

    /// <summary>Deletes all taxa for the specified taxonomy.</summary>
    /// <param name="taxonomy">The taxonomy for which items are deleted.</param>
    /// <param name="language">The language version to delete.</param>
    internal void DeleteAllTaxa(ITaxonomy taxonomy, CultureInfo language)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      string[] availableLanguages = taxonomy != null ? this.GetAvailableLanguages((object) taxonomy, false) : throw new ArgumentNullException(nameof (taxonomy));
      if (this.IsTheLastAvailableLanguage(language, availableLanguages))
      {
        foreach (ITaxon taxon in taxonomy.Taxa)
          this.Provider.Delete(taxon);
      }
      else
        this.DeleteItemTranslation((IHasTrackingContext) taxonomy, language);
    }

    private void DeleteItemTranslation(IHasTrackingContext item, CultureInfo language)
    {
      LocalizationHelper.ClearLstringPropertiesForLanguage((object) item, language);
      item.RegisterDeletedOperation(new string[1]
      {
        language.GetLanguageKey()
      });
    }

    private bool IsTheLastAvailableLanguage(CultureInfo culture, string[] availableLanguages) => culture == null || this.IsTheLastAvailableLanguage(culture.Name, availableLanguages);

    /// <summary>
    /// Moves a hierarchical taxon by one place inside of its level.
    /// </summary>
    /// <param name="taxon">An instance of <see cref="T:Telerik.Sitefinity.Taxonomies.Model.HierarchicalTaxon" /> to be moved</param>
    /// <param name="direction">
    /// The <see cref="T:Telerik.Sitefinity.Taxonomies.MovingDirection" /> in wich to move the taxon.
    /// Up means to less ordinal value, and down to bigger ordinal value.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">If the taxon argument is not set to an instance.</exception>
    /// <exception cref="T:System.InvalidOperationException">If the taxon is the first taxon in its level.</exception>
    public virtual void MoveTaxon(HierarchicalTaxon taxon, MovingDirection direction)
    {
      Pair<IOrderedItem, IOrderedItem> pair = taxon != null ? this.GetTaxonClosestAdjecentItems(taxon, direction) : throw new ArgumentNullException(nameof (taxon));
      bool insertBefore = direction != MovingDirection.Down;
      taxon.SetOrdinalBetweenItems(pair.First, pair.Second, insertBefore);
    }

    /// <summary>
    /// Moves a collection of taxons in the specified <see cref="T:Telerik.Sitefinity.Taxonomies.MovingDirection" />.
    /// </summary>
    /// <param name="taxons">Collection of taxons to be moved up.</param>
    /// <param name="direction">
    /// The <see cref="T:Telerik.Sitefinity.Taxonomies.MovingDirection" /> in which the move should be performed.
    /// Up means to less ordinal value, and down to bigger ordinal value.
    /// </param>
    /// <exception cref="T:System.ArgumentNullException">
    /// If the taxons argument is not set to an instance of an object.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    /// If the collection of taxons is empty.
    /// </exception>
    public virtual void MoveTaxons(List<HierarchicalTaxon> taxons, MovingDirection direction)
    {
      if (taxons == null)
        throw new ArgumentNullException(nameof (taxons));
      if (taxons.Count == 0)
        throw new ArgumentException(Res.Get<TaxonomyResources>().EmptyCollectionOfTaxonsIsNotAllowed);
      List<HierarchicalTaxon> list = taxons.OrderBy<HierarchicalTaxon, float>((Func<HierarchicalTaxon, float>) (t => t.Ordinal)).ToList<HierarchicalTaxon>();
      Pair<IOrderedItem, IOrderedItem> pair = direction != MovingDirection.Up ? this.GetTaxonClosestAdjecentItems(list.Last<HierarchicalTaxon>(), direction) : this.GetTaxonClosestAdjecentItems(list.First<HierarchicalTaxon>(), direction);
      if (direction == MovingDirection.Down && (double) pair.Second.Ordinal == 0.0)
        pair.Second.Ordinal = (float) ((int) pair.First.Ordinal + list.Count);
      List<IOrderedItem> taxons1 = new List<IOrderedItem>(list.Cast<IOrderedItem>());
      taxons1.Insert(0, pair.First);
      taxons1.Add(pair.Second);
      this.OrderWidthFirst((IList<IOrderedItem>) taxons1, 0, taxons1.Count - 1);
    }

    /// <summary>
    /// Moves a collection of taxons down, each by one place inside of its level.
    /// </summary>
    /// <exception cref="T:System.ArgumentNullException">
    /// If the taxons argument is not set to an instance.
    /// </exception>
    /// <exception cref="T:System.ArgumentException">
    /// If the collection of taxons is empty.
    /// </exception>
    /// <exception cref="T:System.InvalidOperationException">
    /// If even one of the taxons in the collection is the last taxon in it's level.
    /// </exception>
    /// <param name="taxons">Collection of taxons to be moved down.</param>
    public virtual void MoveTaxonsDown(List<HierarchicalTaxon> taxons)
    {
      if (taxons == null)
        throw new ArgumentNullException(nameof (taxons));
      if (taxons.Count == 0)
        throw new ArgumentException(Res.Get<TaxonomyResources>().EmptyCollectionOfTaxonsIsNotAllowed);
      foreach (HierarchicalTaxon taxon in (IEnumerable<HierarchicalTaxon>) taxons.OrderByDescending<HierarchicalTaxon, float>((Func<HierarchicalTaxon, float>) (t => t.Ordinal)))
      {
        this.MoveTaxon(taxon, MovingDirection.Down);
        this.SaveChanges();
      }
    }

    /// <summary>Gets the custom taxonomies from the cache.</summary>
    /// <returns></returns>
    public static IEnumerable<ITaxonomyProxy> GetTaxonomiesCache()
    {
      ICacheManager cacheManager = SystemManager.GetCacheManager(CacheManagerInstance.Internal);
      if (!(cacheManager["sf_taxonomies_cache"] is IEnumerable<ITaxonomyProxy> list1))
      {
        lock (TaxonomyManager.TaxonomyCacheSync)
        {
          if (!(cacheManager["sf_taxonomies_cache"] is IEnumerable<ITaxonomyProxy> list1))
          {
            list1 = (IEnumerable<ITaxonomyProxy>) TaxonomyManager.GetManager().GetTaxonomies<Taxonomy>().Where<Taxonomy>((Expression<Func<Taxonomy, bool>>) (t => !TaxonomyManager.SystemTaxonomies.Contains(t.Id))).Select<Taxonomy, TaxonomyProxy>((Expression<Func<Taxonomy, TaxonomyProxy>>) (t => new TaxonomyProxy(t))).ToList<TaxonomyProxy>();
            cacheManager.Add("sf_taxonomies_cache", (object) list1, CacheItemPriority.Normal, (ICacheItemRefreshAction) null, (ICacheItemExpiration) new DataItemCacheDependency(typeof (Taxonomy), (string) null), (ICacheItemExpiration) new SlidingTime(TimeSpan.FromMinutes(60.0)));
          }
        }
      }
      return list1;
    }

    /// <summary>Gets the type of the taxonomy.</summary>
    /// <param name="type">The type.</param>
    /// <returns></returns>
    public static TaxonomyType GetTaxonomyEnum(Type type)
    {
      if (typeof (FlatTaxonomy).IsAssignableFrom(type) || typeof (FlatTaxon).IsAssignableFrom(type))
        return TaxonomyType.Flat;
      if (typeof (HierarchicalTaxonomy).IsAssignableFrom(type) || typeof (HierarchicalTaxon).IsAssignableFrom(type))
        return TaxonomyType.Hierarchical;
      throw new ArgumentException("Invalid type.");
    }

    /// <summary>
    /// Determines whether the given taxonomy is built in. Built-in taxonomies requires special behavior
    /// like disabling delete for them
    /// </summary>
    /// <param name="taxonomy">The taxonomy.</param>
    /// <returns>
    /// 	<c>true</c> if the taxonomy is built-in; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsTaxonomyBuiltIn(ITaxonomy taxonomy)
    {
      Guid id = taxonomy.Id;
      return id == TaxonomyManager.TagsTaxonomyId || id == TaxonomyManager.CategoriesTaxonomyId || id == TaxonomyManager.DepartmentsTaxonomyId;
    }

    /// <summary>Checks if taxonomy is system or not.</summary>
    /// <param name="taxonomy">The taxonomy to check.</param>
    internal static bool IsSystemTaxonomy(ITaxonomy taxonomy) => TaxonomyManager.IsSystemTaxonomy(taxonomy.Id);

    /// <summary>Checks if taxonomy is system or not.</summary>
    /// <param name="taxonomy">The taxonomy to check.</param>
    internal static bool IsSystemTaxonomy(Guid id) => TaxonomyManager.SystemTaxonomies.Contains(id);

    /// <summary>
    /// Copies the specified source taxon to the specified target taxonomy.
    /// </summary>
    /// <typeparam name="TTaxon">The type of the taxon.</typeparam>
    /// <param name="sourceTaxon">The source taxon.</param>
    /// <param name="targetTaxonomy">The target taxonomy.</param>
    /// <returns>The newly created taxon</returns>
    public virtual TTaxon CopyTaxon<TTaxon>(TTaxon sourceTaxon, ITaxonomy targetTaxonomy) where TTaxon : class, ITaxon
    {
      TTaxon taxon = this.provider.CreateTaxon<TTaxon>();
      this.CopyTaxon<TTaxon>(sourceTaxon, taxon);
      taxon.Taxonomy = targetTaxonomy;
      taxon.Provider = (object) this.provider;
      return taxon;
    }

    /// <summary>Copies the taxon properties.</summary>
    /// <typeparam name="TTaxon">The type of the taxon.</typeparam>
    /// <param name="sourceTaxon">The source taxon.</param>
    /// <param name="destinationTaxon">The destination taxon.</param>
    internal void CopyTaxon<TTaxon>(TTaxon sourceTaxon, TTaxon destinationTaxon) where TTaxon : class, ITaxon
    {
      this.CopyProperties<TTaxon>(sourceTaxon, destinationTaxon, this.GetIgnoreCopyFunction());
      if (!((object) sourceTaxon is Taxon))
        return;
      Taxon source = (object) sourceTaxon as Taxon;
      Taxon target = (object) destinationTaxon as Taxon;
      this.CopyAttributes(source, target);
      this.CopySynonyms(source, target);
    }

    /// <summary>Copies the taxonomy properties.</summary>
    /// <typeparam name="TTaxonomy">The type of the taxonomy.</typeparam>
    /// <param name="sourceTaxonomy">The source taxonomy.</param>
    /// <param name="destinationTaxonomy">The destination taxonomy.</param>
    internal void CopyTaxonomy<TTaxonomy>(TTaxonomy sourceTaxonomy, TTaxonomy destinationTaxonomy) where TTaxonomy : class, ITaxonomy => this.CopyProperties<TTaxonomy>(sourceTaxonomy, destinationTaxonomy, this.GetIgnoreCopyFunction());

    private T CopyProperties<T>(
      T source,
      T target,
      params Func<PropertyDescriptor, bool>[] ignores)
      where T : class
    {
      IEnumerable<PropertyDescriptor> source1 = TypeDescriptor.GetProperties((object) target).OfType<PropertyDescriptor>();
      if (ignores != null)
        source1 = source1.Where<PropertyDescriptor>((Func<PropertyDescriptor, bool>) (p => !((IEnumerable<Func<PropertyDescriptor, bool>>) ignores).Any<Func<PropertyDescriptor, bool>>((Func<Func<PropertyDescriptor, bool>, bool>) (i => i(p)))));
      foreach (PropertyDescriptor propertyDescriptor in source1)
      {
        try
        {
          object source2 = propertyDescriptor.GetValue((object) source);
          object target1 = propertyDescriptor.GetValue((object) target);
          if (propertyDescriptor.PropertyType == typeof (Lstring))
            LocalizationHelper.CopyLstring((Lstring) source2, (Lstring) target1);
          else if (!propertyDescriptor.IsReadOnly)
            propertyDescriptor.SetValue((object) target, source2);
        }
        catch (Exception ex)
        {
        }
      }
      return target;
    }

    private void CopyAttributes(Taxon source, Taxon target)
    {
      foreach (KeyValuePair<string, string> attribute in (IEnumerable<KeyValuePair<string, string>>) source.Attributes)
        target.Attributes.Add(attribute.Key, attribute.Value);
    }

    private void CopySynonyms(Taxon source, Taxon target)
    {
      foreach (Synonym synonym1 in (IEnumerable<Synonym>) source.Synonyms)
      {
        Synonym synonym2 = this.CreateSynonym();
        this.CopyProperties<Synonym>(synonym1, synonym2, this.GetIgnoreCopyFunction());
        synonym2.Parent = target;
        target.Synonyms.Add(synonym2);
      }
    }

    /// <summary>
    /// Copies the taxa from the source taxonomy to the target one.
    /// </summary>
    /// <typeparam name="TTaxonomy">The type of the taxonomy.</typeparam>
    /// <param name="sourceTaxonomyId">The source taxonomy id.</param>
    /// <param name="targetTaxonomyId">The target taxonomy id.</param>
    public virtual void CopyTaxa<TTaxonomy>(Guid sourceTaxonomyId, Guid targetTaxonomyId) where TTaxonomy : class, ITaxonomy => this.CopyTaxaInternal<TTaxonomy>(this.Provider.GetTaxonomy<TTaxonomy>(sourceTaxonomyId), this.Provider.GetTaxonomy<TTaxonomy>(targetTaxonomyId));

    /// <summary>
    /// Copies the taxa from the source taxonomy to the target one.
    /// </summary>
    /// <typeparam name="TTaxonomy">The type of the taxonomy.</typeparam>
    /// <param name="sourceTaxonomy">The source taxonomy.</param>
    /// <param name="targetTaxonomy">The target taxonomy.</param>
    public virtual void CopyTaxa<TTaxonomy>(TTaxonomy sourceTaxonomy, TTaxonomy targetTaxonomy) where TTaxonomy : class, ITaxonomy => this.CopyTaxaInternal<TTaxonomy>(sourceTaxonomy, targetTaxonomy);

    /// <summary>
    /// Copies the taxa from the source taxonomy to the target one.
    /// </summary>
    /// <typeparam name="TTaxonomy">The type of the taxonomy.</typeparam>
    /// <param name="sourceTaxonomy">The source taxonomy.</param>
    /// <param name="targetTaxonomy">The target taxonomy.</param>
    internal void CopyTaxaInternal<TTaxonomy>(TTaxonomy sourceTaxonomy, TTaxonomy targetTaxonomy) where TTaxonomy : class, ITaxonomy
    {
      if ((object) sourceTaxonomy == null)
        throw new ArgumentNullException(nameof (sourceTaxonomy));
      if ((object) targetTaxonomy == null)
        throw new ArgumentNullException(nameof (targetTaxonomy));
      Dictionary<Guid, Guid> dictionary = new Dictionary<Guid, Guid>();
      Guid id1;
      foreach (ITaxon sourceTaxon in (IEnumerable<ITaxon>) sourceTaxonomy.Taxa.OrderByDescending<ITaxon, float>((Func<ITaxon, float>) (x => x.Ordinal)))
      {
        if ((object) sourceTaxonomy is FlatTaxonomy)
          id1 = this.CopyTaxon<FlatTaxon>((FlatTaxon) sourceTaxon, (ITaxonomy) targetTaxonomy).Id;
        else if ((object) sourceTaxonomy is HierarchicalTaxonomy)
        {
          Guid id2 = this.CopyTaxon<HierarchicalTaxon>((HierarchicalTaxon) sourceTaxon, (ITaxonomy) targetTaxonomy).Id;
          dictionary.Add(sourceTaxon.Id, id2);
        }
        else if ((object) sourceTaxonomy is NetworkTaxonomy)
        {
          id1 = this.CopyTaxon<NetworkTaxon>((NetworkTaxon) sourceTaxon, (ITaxonomy) targetTaxonomy).Id;
        }
        else
        {
          if (!((object) sourceTaxonomy is FacetTaxonomy))
            throw new ArgumentException("Invalid taxonomy type.");
          id1 = this.CopyTaxon<FacetTaxon>((FacetTaxon) sourceTaxon, (ITaxonomy) targetTaxonomy).Id;
        }
      }
      if (dictionary.Count <= 0)
        return;
      foreach (HierarchicalTaxon taxon in sourceTaxonomy.Taxa)
      {
        if (taxon.Parent != null)
        {
          Guid targetTaxonId = dictionary[taxon.Id];
          HierarchicalTaxon hierarchicalTaxon1 = targetTaxonomy.Taxa.Single<ITaxon>((Func<ITaxon, bool>) (t => t.Id == targetTaxonId)) as HierarchicalTaxon;
          Guid targetTaxonParentId = dictionary[taxon.Parent.Id];
          HierarchicalTaxon hierarchicalTaxon2 = targetTaxonomy.Taxa.Single<ITaxon>((Func<ITaxon, bool>) (t => t.Id == targetTaxonParentId)) as HierarchicalTaxon;
          hierarchicalTaxon1.Parent = hierarchicalTaxon2;
        }
      }
    }

    internal void ValidateTaxonomyConstraints(string name, Guid id)
    {
      if (this.GetTaxonomies<Taxonomy>().Where<Taxonomy>((Expression<Func<Taxonomy, bool>>) (t => t.Name == name && t.Id != id)).SingleOrDefault<Taxonomy>() != null)
        throw new ArgumentException("A taxonomy with the same name has already exists.");
    }

    internal void ValidateConstraints(Taxonomy taxonomy) => this.ValidateTaxonomyConstraints(taxonomy.Name, taxonomy.Id);

    internal void ValidateConstraints(FlatTaxon taxon)
    {
      Guid taxonId = taxon.Id;
      string taxonUrl = taxon.UrlName.Value;
      Guid taxonomyId = taxon.Taxonomy.Id;
      if (this.GetTaxa<FlatTaxon>().Where<FlatTaxon>((Expression<Func<FlatTaxon, bool>>) (t => t.Taxonomy.Id == taxonomyId && t.UrlName == (Lstring) taxonUrl && t.Id != taxonId)).FirstOrDefault<FlatTaxon>() == null)
        return;
      string name = taxon.Taxonomy.Name;
      this.ThrowDuplicateUrlException((string) taxon.UrlName, (string) taxon.Taxonomy.TaxonName, name);
    }

    internal void ValidateConstraints(HierarchicalTaxon taxon)
    {
      Guid taxonId = taxon.Id;
      string taxonUrl = taxon.UrlName.Value;
      Guid taxonomyId = taxon.Taxonomy.Id;
      HierarchicalTaxon hierarchicalTaxon1;
      if (taxon.Parent != null)
      {
        Guid parentId = taxon.Parent.Id;
        hierarchicalTaxon1 = this.GetTaxa<HierarchicalTaxon>().Where<HierarchicalTaxon>((Expression<Func<HierarchicalTaxon, bool>>) (t => t.Taxonomy.Id == taxonomyId && t.Parent.Id == parentId && t.UrlName == (Lstring) taxonUrl && t.Id != taxonId)).FirstOrDefault<HierarchicalTaxon>();
      }
      else
        hierarchicalTaxon1 = this.GetTaxa<HierarchicalTaxon>().Where<HierarchicalTaxon>((Expression<Func<HierarchicalTaxon, bool>>) (t => t.Taxonomy.Id == taxonomyId && t.Parent == default (object) && t.UrlName == (Lstring) taxonUrl && t.Id != taxonId)).FirstOrDefault<HierarchicalTaxon>();
      if (hierarchicalTaxon1 == null)
        return;
      HierarchicalTaxon hierarchicalTaxon2 = taxon;
      HierarchicalTaxon parent = taxon.Parent;
      List<string> stringList = new List<string>();
      for (; parent != null; parent = parent.Parent)
      {
        Lstring urlName = parent.UrlName;
        if (!string.IsNullOrEmpty((string) urlName))
          stringList.Add((string) urlName);
        hierarchicalTaxon2 = parent;
      }
      string parentName = hierarchicalTaxon2.Taxonomy.Name;
      string urlName1 = (string) taxon.UrlName;
      string taxonName = (string) hierarchicalTaxon2.Taxonomy.TaxonName;
      if (stringList.Count > 0)
      {
        stringList.Reverse();
        parentName = parentName + "/" + string.Join("/", stringList.ToArray());
      }
      this.ThrowDuplicateUrlException(urlName1, taxonName, parentName);
    }

    private void ThrowDuplicateUrlException(string urlName, string taxonName, string parentName) => throw new WebProtocolException(HttpStatusCode.InternalServerError, string.Format(Res.Get<TaxonomyResources>().DuplicateUrlException, (object) taxonName, (object) urlName, (object) parentName), (Exception) null);

    internal bool IsTaxonUrlDuplicate(object item)
    {
      bool flag = false;
      try
      {
        switch (item)
        {
          case FlatTaxon _:
            this.ValidateConstraints(item as FlatTaxon);
            break;
          case HierarchicalTaxon _:
            this.ValidateConstraints(item as HierarchicalTaxon);
            break;
        }
      }
      catch (Exception ex)
      {
        flag = true;
      }
      return flag;
    }

    /// <summary>
    /// Adjusts the marked items count for the given taxon (taxon pageId) by the amount passed as count.
    /// </summary>
    /// <typeparam name="TDataItem">Type of the data item for which the count is being adjusted.</typeparam>
    /// <param name="taxonId">Id of the taxon for which the count is being adjusted.</param>
    /// <param name="count">
    /// Amount of the adjustment. Positive amount will increase count, while negative will decrease it.
    /// </param>
    public virtual void AdjustMarkedItemsCount<TDataItem>(
      Guid taxonId,
      int count,
      ContentLifecycleStatus statisticType)
    {
      this.AdjustMarkedItemsCount(typeof (TDataItem), taxonId, count, statisticType);
    }

    /// <summary>Adjusts the marked items count.</summary>
    /// <param name="dataItem">The data item.</param>
    /// <param name="taxonId">The taxon id.</param>
    /// <param name="count">The count.</param>
    public virtual void AdjustMarkedItemsCount(
      IDataItem dataItem,
      Guid taxonId,
      int count,
      ContentLifecycleStatus statisticType)
    {
      if (dataItem == null)
        throw new ArgumentNullException(nameof (dataItem));
      string empty = string.Empty;
      if (dataItem.Provider != null)
        empty = dataItem.Provider.ToString();
      this.AdjustMarkedItemsCount(dataItem.GetType(), taxonId, count, empty, statisticType);
    }

    /// <summary>
    /// Adjusts the marked items count for the given taxon (taxon id) by the amount passed as count.
    /// </summary>
    /// <param name="dataItemType">Type of the data item for which the count is being adjusted.</param>
    /// <param name="taxonId">Id of the taxon for which the count is being adjusted.</param>
    /// <param name="count">Amount of the adjustment. Positive amount will increase count, while negative will decrease it.</param>
    public virtual void AdjustMarkedItemsCount(
      Type dataItemType,
      Guid taxonId,
      int count,
      ContentLifecycleStatus statisticType)
    {
      this.AdjustMarkedItemsCount(dataItemType, taxonId, count, string.Empty, statisticType);
    }

    /// <summary>
    /// Adjusts the marked items count for the given taxon (taxon id) by the amount passed as count.
    /// </summary>
    /// <param name="dataItemType">Type of the data item for which the count is being adjusted.</param>
    /// <param name="taxonId">Id of the taxon for which the count is being adjusted.</param>
    /// <param name="count">
    /// Amount of the adjustment. Positive amount will increase count, while negative will decrease it.
    /// </param>
    public virtual void AdjustMarkedItemsCount(
      Type dataItemType,
      Guid taxonId,
      int count,
      string itemProviderName,
      ContentLifecycleStatus statisticType)
    {
      if (dataItemType == (Type) null)
        throw new ArgumentNullException(nameof (dataItemType));
      if (taxonId == Guid.Empty)
        throw new ArgumentNullException(nameof (taxonId));
      if (count == 0)
        throw new ArgumentOutOfRangeException(nameof (count), Res.Get<Labels>().ArgumentNameCannotBeZero.Arrange((object) nameof (count)));
      ITaxon taxon = this.Provider.GetTaxon(taxonId);
      if (taxon == null)
        return;
      Guid taxonomyId;
      if (taxon.GetType() == typeof (FlatTaxon))
        taxonomyId = taxon.Taxonomy.Id;
      else if (taxon.GetType() == typeof (HierarchicalTaxon))
      {
        HierarchicalTaxon parent = ((HierarchicalTaxon) taxon).Parent;
        while (parent != null && parent.Parent != null)
          parent = parent.Parent;
        taxonomyId = parent == null ? taxon.Taxonomy.Id : parent.Taxonomy.Id;
      }
      else
        taxonomyId = Guid.Empty;
      this.AdjustMarkedItemsCount(dataItemType, taxonomyId, taxonId, count, itemProviderName, statisticType);
    }

    internal void AdjustMarkedItemsCount(
      Type dataItemType,
      Guid taxonomyId,
      Guid taxonId,
      int count,
      string itemProviderName,
      ContentLifecycleStatus statisticType)
    {
      if (dataItemType == (Type) null)
        throw new ArgumentNullException(nameof (dataItemType));
      if (taxonId == Guid.Empty)
        throw new ArgumentNullException(nameof (taxonId));
      if (count == 0)
        throw new ArgumentOutOfRangeException(nameof (count), Res.Get<Labels>().ArgumentNameCannotBeZero.Arrange((object) nameof (count)));
      string dataItemTypeName = dataItemType.FullName;
      TaxonomyStatistic taxonomyStatistic = this.Provider.GetStatistics().Where<TaxonomyStatistic>((Expression<Func<TaxonomyStatistic, bool>>) (st => st.DataItemType == dataItemTypeName && st.TaxonId == taxonId && (int) st.StatisticType == (int) statisticType && st.ItemProviderName == itemProviderName)).FirstOrDefault<TaxonomyStatistic>();
      if (taxonomyStatistic == null)
      {
        if (count < 0)
          return;
        TaxonomyStatistic statistic = this.Provider.CreateStatistic(dataItemType, taxonomyId, taxonId);
        statistic.StatisticType = statisticType;
        statistic.MarkedItemsCount = (uint) count;
        statistic.ItemProviderName = itemProviderName;
      }
      else
      {
        uint num = (uint) Math.Abs(count);
        if (count > 0)
        {
          taxonomyStatistic.MarkedItemsCount += num;
        }
        else
        {
          if ((long) taxonomyStatistic.MarkedItemsCount - (long) num < 0L)
            return;
          taxonomyStatistic.MarkedItemsCount -= num;
        }
      }
    }

    /// <summary>
    /// Gets the count of items marked with the specified taxa.
    /// </summary>
    /// <param name="itemsIds">
    /// The taxa IDs for which the count is being retrieved.
    /// </param>
    /// <param name="statisticType">
    /// The statistic type for which the count is being retrieved.
    /// </param>
    /// <returns>Dictonary with taxon id and marked items count.</returns>
    public virtual IDictionary<Guid, uint> GetTaxaItemsCount(
      IEnumerable<Guid> itemsIds,
      ContentLifecycleStatus statisticType)
    {
      return (IDictionary<Guid, uint>) this.Provider.GetStatistics().Where<TaxonomyStatistic>((Expression<Func<TaxonomyStatistic, bool>>) (st => (int) st.StatisticType == (int) statisticType && itemsIds.Contains<Guid>(st.TaxonId))).Select(st => new
      {
        TaxonId = st.TaxonId,
        MarkedItemsCount = st.MarkedItemsCount
      }).ToList().GroupBy(st => st.TaxonId).ToDictionary<IGrouping<Guid, \u003C\u003Ef__AnonymousType58<Guid, uint>>, Guid, uint>(g => g.Key, g => (uint) g.Sum(st => (long) st.MarkedItemsCount));
    }

    /// <summary>
    /// Gets the number of items marked with a specified taxon pageId.
    /// </summary>
    /// <param name="taxonId">
    /// Id of the taxon for which the count is being retrieved.
    /// </param>
    /// <returns>Number of marked items.</returns>
    public virtual uint GetTaxonItemsCount(Guid taxonId, ContentLifecycleStatus statisticType)
    {
      if (taxonId == Guid.Empty)
        throw new ArgumentNullException(nameof (taxonId));
      return (uint) this.Provider.GetStatistics().Where<TaxonomyStatistic>((Expression<Func<TaxonomyStatistic, bool>>) (st => st.TaxonId == taxonId && (int) st.StatisticType == (int) statisticType)).ToList<TaxonomyStatistic>().Sum<TaxonomyStatistic>((Func<TaxonomyStatistic, long>) (st => (long) st.MarkedItemsCount));
    }

    /// <summary>
    /// Gets the number of items of a given type marked with a specified taxon pageId.
    /// </summary>
    /// <typeparam name="TDataItem">
    /// Type of the data item for which the count is being retrieved.
    /// </typeparam>
    /// <param name="taxonId">
    /// Id of the taxon for which the count is being retrieved.
    /// </param>
    /// <returns>Number of marked items.</returns>
    public virtual uint GetTaxonItemsCount<TDataItem>(
      Guid taxonId,
      ContentLifecycleStatus statisticType)
    {
      return this.GetTaxonItemsCount(typeof (TDataItem), taxonId, statisticType);
    }

    /// <summary>
    /// Gets the number of items of a given type marked with a specified taxon pageId.
    /// </summary>
    /// <param name="dataItemType">
    /// Type of the data item for which the count is being retrieved.
    /// </param>
    /// <param name="taxonId">
    /// Id of the taxon for which the count is being retrieved.
    /// </param>
    /// <returns>Number of marked items.</returns>
    public virtual uint GetTaxonItemsCount(
      Type dataItemType,
      Guid taxonId,
      ContentLifecycleStatus statisticType)
    {
      if (dataItemType == (Type) null)
        return this.GetTaxonItemsCount(taxonId, statisticType);
      if (taxonId == Guid.Empty)
        throw new ArgumentNullException(nameof (taxonId));
      string dataItemTypeName = dataItemType.FullName;
      return (uint) this.Provider.GetStatistics().Where<TaxonomyStatistic>((Expression<Func<TaxonomyStatistic, bool>>) (st => st.DataItemType == dataItemTypeName && st.TaxonId == taxonId && (int) st.StatisticType == (int) statisticType)).ToList<TaxonomyStatistic>().Sum<TaxonomyStatistic>((Func<TaxonomyStatistic, long>) (st => (long) st.MarkedItemsCount));
    }

    /// <summary>
    /// Gets the number of items of a given type marked with taxons belonging
    /// to a specified taxonomy.
    /// </summary>
    /// <typeparam name="TDataItem">
    /// Type of the data item for which the count is being retrieved.
    /// </typeparam>
    /// <param name="taxonomyId">
    /// Id of the taxonomy for which the count is being retrieved.
    /// </param>
    /// <returns>Number of marked items.</returns>
    public virtual uint GetTaxonomyItemsCount<TDataItem>(
      Guid taxonomyId,
      ContentLifecycleStatus statisticType)
    {
      return this.GetTaxonomyItemsCount(typeof (TDataItem), taxonomyId, statisticType);
    }

    /// <summary>
    /// Gets the number of items of a given type marked with taxons belonging
    /// to a specified taxonomy.
    /// </summary>
    /// <param name="dataItemType">
    /// Type of the data item for which the count is being retrieved.
    /// </param>
    /// <param name="taxonomyId">
    /// Id of the taxonomy for which the count is being retrieved.
    /// </param>
    /// <returns>Number of marked items.</returns>
    public virtual uint GetTaxonomyItemsCount(
      Type dataItemType,
      Guid taxonomyId,
      ContentLifecycleStatus statisticType)
    {
      if (dataItemType == (Type) null)
        throw new ArgumentNullException(nameof (dataItemType));
      if (taxonomyId == Guid.Empty)
        throw new ArgumentNullException(nameof (taxonomyId));
      string dataItemTypeName = dataItemType.AssemblyQualifiedName;
      return (uint) this.Provider.GetStatistics().Where<TaxonomyStatistic>((Expression<Func<TaxonomyStatistic, bool>>) (st => st.DataItemType == dataItemTypeName && st.TaxonomyId == taxonomyId && (int) st.StatisticType == (int) statisticType)).ToList<TaxonomyStatistic>().Sum<TaxonomyStatistic>((Func<TaxonomyStatistic, long>) (st => (long) st.MarkedItemsCount));
    }

    /// <summary>
    /// Gets all the types that have been marked with a specified taxon.
    /// </summary>
    /// <param name="taxonId">
    /// Id of the taxon for which the marked types ought to be retrieved.
    /// </param>
    /// <returns>An array of marked types; empty array if no types have been marked by that taxon.</returns>
    public virtual Type[] GetTypesByTaxon(Guid taxonId, ContentLifecycleStatus statisticType)
    {
      if (taxonId == Guid.Empty)
        throw new ArgumentNullException(nameof (taxonId));
      List<Type> typeList = new List<Type>();
      IQueryable<TaxonomyStatistic> statistics = this.Provider.GetStatistics();
      Expression<Func<TaxonomyStatistic, bool>> predicate = (Expression<Func<TaxonomyStatistic, bool>>) (st => st.TaxonId == taxonId && (int) st.StatisticType == (int) statisticType);
      foreach (TaxonomyStatistic taxonomyStatistic in (IEnumerable<TaxonomyStatistic>) statistics.Where<TaxonomyStatistic>(predicate))
      {
        Type type = Type.GetType(taxonomyStatistic.DataItemType);
        if (!typeList.Contains(type))
          typeList.Add(type);
      }
      return typeList.ToArray();
    }

    /// <summary>
    /// Gets all the types that have been marked with a taxon belonging to
    /// a specifed taxonomy.
    /// </summary>
    /// <param name="taxonomyId">
    /// If of the taxonomy for which the types ought to be retrieved.
    /// </param>
    /// <returns>An array of marked types; empty array if no types have been marked by that taxonomy.</returns>
    public virtual Type[] GetTypesByTaxonomy(
      Guid taxonomyId,
      ContentLifecycleStatus statisticType)
    {
      if (taxonomyId == Guid.Empty)
        throw new ArgumentNullException(nameof (taxonomyId));
      List<Type> typeList = new List<Type>();
      IQueryable<TaxonomyStatistic> statistics = this.Provider.GetStatistics();
      Expression<Func<TaxonomyStatistic, bool>> predicate = (Expression<Func<TaxonomyStatistic, bool>>) (st => st.TaxonomyId == taxonomyId && (int) st.StatisticType == (int) statisticType);
      foreach (TaxonomyStatistic taxonomyStatistic in (IEnumerable<TaxonomyStatistic>) statistics.Where<TaxonomyStatistic>(predicate))
      {
        Type type = Type.GetType(taxonomyStatistic.DataItemType);
        if (!typeList.Contains(type))
          typeList.Add(type);
      }
      return typeList.ToArray();
    }

    /// <summary>Gets the query for taxonomy statistic.</summary>
    /// <returns>Query for <see cref="T:Telerik.Sitefinity.Taxonomies.Model.TaxonomyStatistic" /></returns>
    public virtual IQueryable<TaxonomyStatistic> GetStatistics() => this.Provider.GetStatistics();

    /// <summary>Unmarks all content items.</summary>
    /// <param name="taxon">The taxon.</param>
    public virtual void UnmarkAllItems(ITaxon taxon)
    {
      TaxonomyManager.TryUnmarkAllItems(this.Provider, TaxonomyManager.GetAllTaxaIDs(taxon));
      if (taxon is HierarchicalTaxon hierarchicalTaxon)
      {
        foreach (ITaxon taxon1 in (IEnumerable<HierarchicalTaxon>) hierarchicalTaxon.Subtaxa)
          this.UnmarkAllItems(taxon1);
      }
      Guid taxonId = taxon.Id;
      IQueryable<TaxonomyStatistic> statistics = this.GetStatistics();
      Expression<Func<TaxonomyStatistic, bool>> predicate = (Expression<Func<TaxonomyStatistic, bool>>) (st => st.TaxonId == taxonId);
      foreach (TaxonomyStatistic taxonomyStatistic in (IEnumerable<TaxonomyStatistic>) statistics.Where<TaxonomyStatistic>(predicate))
      {
        Type itemType = TypeResolutionService.ResolveType(taxonomyStatistic.DataItemType, false);
        if (itemType != (Type) null)
        {
          TaxonomyPropertyDescriptor propertyDescriptor = TaxonomyManager.GetPropertyDescriptor(itemType, taxon);
          IManager mappedManager = ManagerBase.GetMappedManager(itemType, taxonomyStatistic.ItemProviderName);
          if (mappedManager.Provider is IOrganizableProvider provider)
          {
            int? totalCount = new int?(0);
            foreach (object obj in provider.GetItemsByTaxon(taxonId, propertyDescriptor.MetaField.IsSingleTaxon, propertyDescriptor.Name, itemType, (string) null, (string) null, 0, 0, ref totalCount))
            {
              if (!propertyDescriptor.MetaField.IsSingleTaxon)
                ((Content) obj).Organizer.RemoveTaxa(propertyDescriptor.Name, taxon.Id);
              else
                ((Content) obj).Organizer.RemoveTaxon(propertyDescriptor.Name, taxon.Id);
            }
            mappedManager.SaveChanges();
          }
        }
      }
    }

    /// <summary>Creates new synonym.</summary>
    /// <returns></returns>
    public virtual Synonym CreateSynonym() => this.Provider.CreateSynonym();

    /// <summary>Creates new synonym with the provided identity.</summary>
    /// <param name="pageId">The pageId.</param>
    /// <returns></returns>
    public virtual Synonym CreateSynonym(Guid id) => this.Provider.CreateSynonym(id);

    /// <summary>Gets the synonym for the provided identity.</summary>
    /// <param name="pageId">The identity number.</param>
    /// <returns></returns>
    public virtual Synonym GetSynonym(Guid id) => this.Provider.GetSynonym(id);

    /// <summary>Gets a query object for the synonyms.</summary>
    /// <returns></returns>
    public virtual IQueryable<Synonym> GetSynonyms() => this.Provider.GetSynonyms();

    /// <summary>Deletes the specified synonym.</summary>
    /// <param name="synonym">The synonym.</param>
    public virtual void Delete(Synonym synonym) => this.Provider.Delete(synonym);

    /// <summary>
    /// Gets the name of the default provider for this manager.
    /// </summary>
    protected internal override GetDefaultProvider DefaultProviderDelegate => (GetDefaultProvider) (() => Config.Get<TaxonomyConfig>().DefaultProvider);

    /// <summary>
    /// Gets the name of the module to which this manager belongs.
    /// </summary>
    public override string ModuleName => "Taxonomy";

    /// <summary>Gets all provider settings.</summary>
    protected internal override ConfigElementDictionary<string, DataProviderSettings> ProvidersSettings => Config.Get<TaxonomyConfig>().Providers;

    /// <summary>Gets a manger instance for the default data provider.</summary>
    /// <returns>The manager instance.</returns>
    public static TaxonomyManager GetManager() => ManagerBase<TaxonomyDataProvider>.GetManager<TaxonomyManager>();

    /// <summary>
    /// Gets a manger instance for the specified data provider.
    /// </summary>
    /// <param name="providerName">The name of the data provider.</param>
    /// <returns>The manager instance.</returns>
    public static TaxonomyManager GetManager(string providerName) => ManagerBase<TaxonomyDataProvider>.GetManager<TaxonomyManager>(providerName);

    /// <summary>
    /// Gets a manger instance for the specified data provider.
    /// </summary>
    /// <typeparam name="T">The type of the manager.</typeparam>
    /// <param name="providerName">The name of the data provider.</param>
    /// <param name="transactionName">Name of a named global transaction.</param>
    /// <returns>The manager instance.</returns>
    public static TaxonomyManager GetManager(
      string providerName,
      string transactionName)
    {
      return ManagerBase<TaxonomyDataProvider>.GetManager<TaxonomyManager>(providerName, transactionName);
    }

    /// <inheritdoc />
    Type[] IMultisiteEnabledManager.GetShareableTypes() => new Type[1]
    {
      typeof (Taxonomy)
    };

    /// <inheritdoc />
    Type[] IMultisiteEnabledManager.GetSiteSpecificTypes() => new Type[0];

    /// <summary>Gets the taxonomy by its id and a specified site.</summary>
    /// <remarks> In case that no site is specified then the site from the current context is used.</remarks>
    /// <typeparam name="TTaxonomy">The type of the taxonomy (ex. flat or hierarchical).</typeparam>
    /// <param name="rootTaxonomyId">The root taxonomy id.</param>
    /// <param name="siteId">The site id.</param>
    /// <returns>
    /// If the specified site has split taxonomy, then it returns it, otherwise it returns the root taxonomy.
    /// </returns>
    public virtual TTaxonomy GetSiteTaxonomy<TTaxonomy>(Guid rootTaxonomyId, Guid? siteId = null) where TTaxonomy : class, ITaxonomy
    {
      this.MultisiteTaxonomyGuard.IsRootTaxonomy(rootTaxonomyId);
      Guid siteId1 = siteId ?? SystemManager.CurrentContext.MultisiteContext.CurrentSite.Id;
      return this.GetSiteTaxonomyInternal<TTaxonomy>(rootTaxonomyId, siteId1);
    }

    /// <summary>
    /// Gets the split taxonomy related to the specified root taxonomy.
    /// </summary>
    /// <remarks> In case that no site is specified then the site from the current context is used.</remarks>
    /// <typeparam name="TTaxonomy">The type of the taxonomy.</typeparam>
    /// <param name="rootTaxonomyId">The root taxonomy id.</param>
    public virtual TTaxonomy GetSplitTaxonomy<TTaxonomy>(Guid rootTaxonomyId, Guid? siteId = null) where TTaxonomy : class, ITaxonomy
    {
      this.MultisiteTaxonomyGuard.IsRootTaxonomy(rootTaxonomyId);
      Guid siteId1 = siteId ?? SystemManager.CurrentContext.MultisiteContext.CurrentSite.Id;
      return this.GetSplitTaxonomyInternal<TTaxonomy>(rootTaxonomyId, siteId1);
    }

    /// <summary>Gets the sites using the specified taxonomy.</summary>
    /// <typeparam name="TTaxonomy">The type of the taxonomy.</typeparam>
    /// <param name="taxonomy">The taxonomy.</param>
    /// <returns>List of sites.</returns>
    public virtual IEnumerable<Telerik.Sitefinity.Multisite.ISite> GetRelatedSites(
      ITaxonomy taxonomy)
    {
      Guard.ArgumentNotNull((object) taxonomy, nameof (taxonomy));
      IMultisiteEnabledOAProvider provider = (IMultisiteEnabledOAProvider) this.Provider;
      string typeofTTaxonomy = typeof (Taxonomy).FullName;
      if (taxonomy.IsSplitTaxonomy())
      {
        IQueryable<Guid> sites = provider.GetSiteItemLinks().Where<SiteItemLink>((Expression<Func<SiteItemLink, bool>>) (l => l.ItemId == taxonomy.Id && l.ItemType == typeofTTaxonomy)).Select<SiteItemLink, Guid>((Expression<Func<SiteItemLink, Guid>>) (l => l.SiteId));
        return SystemManager.CurrentContext.MultisiteContext.GetSites().Where<Telerik.Sitefinity.Multisite.ISite>((Func<Telerik.Sitefinity.Multisite.ISite, bool>) (s => sites.Contains<Guid>(s.Id)));
      }
      Guid[] splits = this.Provider.GetTaxonomies<Taxonomy>().Where<Taxonomy>((Expression<Func<Taxonomy, bool>>) (t => t.RootTaxonomyId == (Guid?) taxonomy.Id)).Select<Taxonomy, Guid>((Expression<Func<Taxonomy, Guid>>) (t => t.Id)).ToArray<Guid>();
      Guid[] splitSites = provider.GetSiteItemLinks().Where<SiteItemLink>((Expression<Func<SiteItemLink, bool>>) (l => l.ItemType == typeofTTaxonomy && splits.Contains<Guid>(l.ItemId))).Select<SiteItemLink, Guid>((Expression<Func<SiteItemLink, Guid>>) (l => l.SiteId)).ToArray<Guid>();
      return SystemManager.CurrentContext.MultisiteContext.GetSites().Where<Telerik.Sitefinity.Multisite.ISite>((Func<Telerik.Sitefinity.Multisite.ISite, bool>) (s => !((IEnumerable<Guid>) splitSites).Contains<Guid>(s.Id)));
    }

    /// <summary>
    /// Creates a new split taxonomy for the specified site. The new split taxonomy is a copy of the currently used taxonomy by the site.
    /// The new split taxonomy is linked to the site.
    /// </summary>
    /// <typeparam name="TTaxonomy">The type of the taxonomy.</typeparam>
    /// <param name="rootTaxonomyId">The root taxonomy id.</param>
    /// <param name="siteId">The site id.</param>
    /// <remarks> In case that no site is specified then the site from the current context is used.</remarks>
    /// <returns>Returns the newly created split taxonomy.</returns>
    public virtual TTaxonomy SplitSiteTaxonomy<TTaxonomy>(TTaxonomy rootTaxonomy, Guid? siteId = null) where TTaxonomy : class, ITaxonomy
    {
      Guard.ArgumentNotNull((object) rootTaxonomy, nameof (rootTaxonomy));
      this.MultisiteTaxonomyGuard.IsRootTaxonomy((ITaxonomy) rootTaxonomy);
      if (!siteId.HasValue)
        siteId = new Guid?(SystemManager.CurrentContext.CurrentSite.Id);
      this.UnlinkSiteTaxonomy<Taxonomy>(this.GetSiteTaxonomyInternal<TTaxonomy>(rootTaxonomy.Id, siteId.GetValueOrDefault()).Id, siteId.GetValueOrDefault());
      TTaxonomy taxonomy = this.Provider.CreateTaxonomy<TTaxonomy>();
      this.CopyTaxonomy<TTaxonomy>(rootTaxonomy, taxonomy);
      taxonomy.Name = rootTaxonomy.Name + (object) taxonomy.Id;
      taxonomy.Provider = (object) this.Provider;
      taxonomy.RootTaxonomyId = new Guid?(rootTaxonomy.Id);
      this.LinkSiteTaxonomy<Taxonomy>(taxonomy.Id, siteId.Value);
      return taxonomy;
    }

    /// <summary>Arranges the target site to use the given taxonomy.</summary>
    /// <param name="taxonomy">The taxonomy to be used.</param>
    /// <param name="targetSiteId">The target site id.</param>
    public virtual void UseTaxonomyInSite(ITaxonomy taxonomy, Guid targetSiteId)
    {
      Guard.ArgumentNotNull((object) taxonomy, nameof (taxonomy));
      Taxonomy siteTaxonomy = this.GetSiteTaxonomy<Taxonomy>(taxonomy.GetRootTaxonomyId(), new Guid?(targetSiteId));
      if (siteTaxonomy.Id == taxonomy.Id)
        return;
      this.UnlinkSiteTaxonomy<Taxonomy>(siteTaxonomy.Id, targetSiteId);
      this.LinkSiteTaxonomy<Taxonomy>(taxonomy.Id, targetSiteId);
    }

    /// <summary>
    /// Gets the taxa belonging to a taxonomy in a specific site.
    /// </summary>
    /// <remarks> In case that no site is specified then the site from the current context is used.</remarks>
    /// <typeparam name="TTaxon">The type of the taxon (ex. flat or hierarchical).</typeparam>
    /// <param name="rootTaxonomyId">The root taxonomy id.</param>
    /// <param name="siteId">The site id.</param>
    /// <returns>If the specified site has split taxonomy, then it returns its taxa, otherwise it returns the taxa of the root taxonomy.</returns>
    internal IQueryable<TTaxon> GetSiteTaxaByTaxonomy<TTaxonomy, TTaxon>(
      Guid rootTaxonomyId,
      Guid? siteId = null)
      where TTaxonomy : class, ITaxonomy
      where TTaxon : class, ITaxon
    {
      this.MultisiteTaxonomyGuard.IsRootTaxonomy(rootTaxonomyId);
      Guid guid = siteId ?? SystemManager.CurrentContext.MultisiteContext.CurrentSite.Id;
      TTaxonomy taxonomy = this.GetSiteTaxonomy<TTaxonomy>(rootTaxonomyId, new Guid?(guid));
      return this.GetTaxa<TTaxon>().Where<TTaxon>((Expression<Func<TTaxon, bool>>) (t => t.Taxonomy.Id == taxonomy.Id));
    }

    /// <summary>
    /// Gets the taxonomy statistics for the specified taxonomy in the specified site.
    /// </summary>
    /// <remarks> In case that no site is specified then the site from the current context is used.</remarks>
    /// <param name="rootTaxonomyId">The root taxonomy id.</param>
    /// <param name="siteId">The site id.</param>
    /// <returns>If the specified site has split taxonomy, then it returns its statistics, otherwise it returns the statistics for the root taxonomy.</returns>
    internal IQueryable<TaxonomyStatistic> GetSiteTaxonomyStatistics(
      Guid rootTaxonomyId,
      Guid? siteId = null)
    {
      this.MultisiteTaxonomyGuard.IsRootTaxonomy(rootTaxonomyId);
      Guid guid = siteId ?? SystemManager.CurrentContext.MultisiteContext.CurrentSite.Id;
      Taxonomy taxonomy = this.GetSiteTaxonomy<Taxonomy>(rootTaxonomyId, new Guid?(guid));
      return this.GetStatistics().Where<TaxonomyStatistic>((Expression<Func<TaxonomyStatistic, bool>>) (t => t.TaxonomyId == taxonomy.Id && t.MarkedItemsCount > 0U && (int) t.StatisticType == 2));
    }

    /// <summary>Gets the taxonomy by its name and a specified site.</summary>
    /// <remarks> In case that no site is specified then the site from the current context is used.</remarks>
    /// <typeparam name="TTaxonomy">The type of the taxonomy (ex. flat or hierarchical).</typeparam>
    /// <param name="rootTaxonomyName">Name of the root taxonomy.</param>
    /// <param name="siteId">The site id.</param>
    /// <returns>If the specified site has split taxonomy, then it returns it, otherwise it returns the root taxonomy.</returns>
    internal TTaxonomy GetSiteTaxonomy<TTaxonomy>(string rootTaxonomyName, Guid? siteId = null) where TTaxonomy : class, ITaxonomy
    {
      Guid siteId1 = siteId ?? SystemManager.CurrentContext.MultisiteContext.CurrentSite.Id;
      TTaxonomy taxonomy = Queryable.FirstOrDefault<TTaxonomy>(this.GetTaxonomies<TTaxonomy>().Where<TTaxonomy>((Expression<Func<TTaxonomy, bool>>) (t => t.Name == rootTaxonomyName && !t.RootTaxonomyId.HasValue)));
      return (object) taxonomy != null ? this.GetSiteTaxonomyInternal<TTaxonomy>(taxonomy.Id, siteId1) : default (TTaxonomy);
    }

    /// <summary>Gets the site taxonomy internal.</summary>
    /// <typeparam name="TTaxonomy">The type of the T taxonomy.</typeparam>
    /// <param name="rootTaxonomyId">The root taxonomy id.</param>
    /// <param name="siteId">The site id.</param>
    /// <returns></returns>
    internal TTaxonomy GetSiteTaxonomyInternal<TTaxonomy>(Guid rootTaxonomyId, Guid siteId) where TTaxonomy : class, ITaxonomy => this.GetSplitTaxonomyInternal<TTaxonomy>(rootTaxonomyId, siteId) ?? this.GetTaxonomy<TTaxonomy>(rootTaxonomyId);

    /// <summary>Gets the split taxonomy for a given root and site.</summary>
    /// <typeparam name="TTaxonomy">The type of the T taxonomy.</typeparam>
    /// <param name="rootTaxonomyId">The Id of the root taxonomy.</param>
    /// <param name="siteId">The Id of the site.</param>
    /// <returns>The split taxonomy for the given site and root or <c>Null</c> if none exists.</returns>
    internal TTaxonomy GetSplitTaxonomyInternal<TTaxonomy>(Guid rootTaxonomyId, Guid siteId) where TTaxonomy : class, ITaxonomy => Queryable.FirstOrDefault<TTaxonomy>(((IMultisiteEnabledOAProvider) this.Provider).FilterBySite<TTaxonomy>(this.GetTaxonomies<TTaxonomy>().Where<TTaxonomy>((Expression<Func<TTaxonomy, bool>>) (t => t.RootTaxonomyId == (Guid?) rootTaxonomyId)), siteId, typeof (Taxonomy).FullName));

    /// <summary>
    /// Creates a link between a specified split taxonomy and a specified site.
    /// If the method is called for a root taxonomy, nothing is done because no explicit links are created between a root taxonomy an site.
    /// </summary>
    /// <typeparam name="TTaxonomy">The type of the taxonomy.</typeparam>
    /// <param name="taxonomyId">The taxonomy id.</param>
    /// <param name="siteId">The site id.</param>
    internal void LinkSiteTaxonomy<TTaxonomy>(Guid taxonomyId, Guid siteId) where TTaxonomy : class, ITaxonomy
    {
      TTaxonomy taxonomy = this.GetTaxonomy<TTaxonomy>(taxonomyId);
      if (!taxonomy.IsRootTaxonomy())
      {
        if (this.Provider.GetItemSiteLinks((IDataItem) taxonomy, typeof (Taxonomy).FullName).SingleOrDefault<SiteItemLink>((Expression<Func<SiteItemLink, bool>>) (l => l.SiteId == siteId)) != null)
          throw new InvalidOperationException(string.Format("There is already a link between the provided split taxonomy with Id: {0} and the provided site with Id:{1}.", (object) taxonomy.Id, (object) siteId));
        this.Provider.AddItemLink(siteId, (IDataItem) taxonomy);
      }
      this.MarkTaxonomyAsDirty((ITaxonomy) taxonomy);
      this.RegisterTrackingContextSiteOperation((object) taxonomy as IHasTrackingContext, OperationStatus.LinkWithSite, siteId);
    }

    /// <summary>
    /// Deletes the link between a specified taxonomy and a specified site.
    /// </summary>
    /// <typeparam name="TTaxonomy">The type of the taxonomy.</typeparam>
    /// <param name="taxonomyId">The taxonomy id.</param>
    /// <param name="siteId">The site id.</param>
    internal void UnlinkSiteTaxonomy<TTaxonomy>(Guid taxonomyId, Guid siteId) where TTaxonomy : class, ITaxonomy
    {
      TTaxonomy taxonomy = this.GetTaxonomy<TTaxonomy>(taxonomyId);
      if (taxonomy.IsSplitTaxonomy())
      {
        ((IMultisiteEnabledOAProvider) this.Provider).Delete(this.Provider.GetItemSiteLinks((IDataItem) taxonomy, typeof (Taxonomy).FullName).SingleOrDefault<SiteItemLink>((Expression<Func<SiteItemLink, bool>>) (l => l.SiteId == siteId)) ?? throw new InvalidOperationException(string.Format("There is no link between the provided split taxonomy with Id: {0} and the provided site with Id:{1}.", (object) taxonomy.Id, (object) siteId)));
      }
      else
      {
        TTaxonomy taxonomyInternal = this.GetSplitTaxonomyInternal<TTaxonomy>(taxonomy.Id, siteId);
        if ((object) taxonomyInternal != null)
          throw new InvalidOperationException(string.Format("There is no link between the provided root taxonomy with Id: {0} and the provided site with Id:{1}. The site is linked to split taxonomy with Id:{2}.", (object) taxonomy.Id, (object) siteId, (object) taxonomyInternal.Id));
      }
      this.MarkTaxonomyAsDirty((ITaxonomy) taxonomy);
      this.RegisterTrackingContextSiteOperation((object) taxonomy as IHasTrackingContext, OperationStatus.UnlinkWithSite, siteId);
    }

    /// <summary>Gets all taxonomies that are not used in any site.</summary>
    /// <param name="items">The source items that will be filtered.</param>
    /// <returns>IQueryable with unused taxonomies in any site.</returns>
    internal virtual IQueryable<Taxonomy> FilterByNotUsedTaxonomies()
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      TaxonomyManager.\u003C\u003Ec__DisplayClass91_0 cDisplayClass910 = new TaxonomyManager.\u003C\u003Ec__DisplayClass91_0();
      MultisiteManager manager = MultisiteManager.GetManager();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass910.allSites = manager.GetSites();
      IMultisiteEnabledOAProvider provider = (IMultisiteEnabledOAProvider) this.Provider;
      IQueryable<Taxonomy> queryable = this.GetTaxonomies<Taxonomy>().Where<Taxonomy>((Expression<Func<Taxonomy, bool>>) (t => t.RootTaxonomyId.HasValue));
      // ISSUE: reference to a compiler-generated field
      IQueryable<Taxonomy> source1 = provider.GetItemsSiteLinks<Taxonomy>(queryable).Join((IEnumerable<Taxonomy>) queryable, (Expression<Func<SiteItemLink, Guid>>) (link => link.ItemId), (Expression<Func<Taxonomy, Guid>>) (taxonomy => taxonomy.Id), (link, taxonomy) => new
      {
        link = link,
        taxonomy = taxonomy
      }).Where(data => data.link.ItemType == typeof (Taxonomy).FullName).Select(data => new
      {
        Taxonomy = data.taxonomy,
        SiteId = data.link.SiteId
      }).GroupBy(t => t.Taxonomy.RootTaxonomy).Where<IGrouping<Taxonomy, \u003C\u003Ef__AnonymousType60<Taxonomy, Guid>>>(g => g.Key.Taxa.Any<Taxon>() && !cDisplayClass910.allSites.Select<Site, Guid>((Expression<Func<Site, Guid>>) (x => x.Id)).Except<Guid>(g.Select(y => y.SiteId)).Any<Guid>()).Select<IGrouping<Taxonomy, \u003C\u003Ef__AnonymousType60<Taxonomy, Guid>>, Taxonomy>(g => g.Key);
      Guid[] array = provider.GetItemsSiteLinks<Taxonomy>(queryable).Select<SiteItemLink, Guid>((Expression<Func<SiteItemLink, Guid>>) (link => link.ItemId)).ToArray<Guid>();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass910.splitBySiteTaxonomiesIds = array;
      // ISSUE: reference to a compiler-generated field
      IQueryable<Taxonomy> source2 = queryable.Where<Taxonomy>((Expression<Func<Taxonomy, bool>>) (t => !cDisplayClass910.splitBySiteTaxonomiesIds.Contains<Guid>(t.Id)));
      return source1.Union<Taxonomy>((IEnumerable<Taxonomy>) source2);
    }

    /// <summary>Determines whether the taxonomy is used.</summary>
    /// <param name="taxonomy">The taxonomy.</param>
    /// <param name="manager">The manager.</param>
    /// <exception cref="T:System.ArgumentNullException">taxonomy or manager</exception>
    internal bool IsTaxonomyUsed(ITaxonomy taxonomy) => this.GetRelatedSites(taxonomy).Count<Telerik.Sitefinity.Multisite.ISite>() > 0;

    /// <summary>
    /// Determines whether the specified taxonomy is split for the specified site.
    /// </summary>
    /// <remarks> In case that no site is specified then the site from the current context is used.</remarks>
    /// <typeparam name="TTaxonomy">The type of the taxonomy.</typeparam>
    /// <param name="taxonomy">The taxonomy.</param>
    /// <param name="siteId">The site id.</param>
    /// <returns>True if the specified taxonomy is split for the specified site, otherwise returns false</returns>
    internal bool IsSplitTaxonomy(ITaxonomy taxonomy, Guid? siteId = null)
    {
      this.MultisiteTaxonomyGuard.IsSplitTaxonomy(taxonomy);
      return this.IsSplitTaxonomyInternal(taxonomy, siteId);
    }

    /// <summary>
    /// Determines whether the specified taxonomy is split for the specified site.
    /// </summary>
    /// <remarks> In case that no site is specified then the site from the current context is used.</remarks>
    /// <typeparam name="TTaxonomy">The type of the taxonomy.</typeparam>
    /// <param name="taxonomy">The taxonomy.</param>
    /// <param name="siteId">The site id.</param>
    /// <returns>True if the specified taxonomy is split for the specified site, otherwise returns false</returns>
    internal bool IsSplitTaxonomyInternal(ITaxonomy taxonomy, Guid? siteId = null)
    {
      if (taxonomy == null)
        throw new ArgumentNullException(nameof (taxonomy));
      Guid siteId1 = siteId ?? SystemManager.CurrentContext.MultisiteContext.CurrentSite.Id;
      return ((IMultisiteEnabledOAProvider) this.Provider).FilterBySite<Taxonomy>(this.GetTaxonomies<Taxonomy>().Where<Taxonomy>((Expression<Func<Taxonomy, bool>>) (t => t.RootTaxonomyId != new Guid?())), siteId1).Select<Taxonomy, Guid>((Expression<Func<Taxonomy, Guid>>) (t => t.Id)).Contains<Guid>(taxonomy.Id);
    }

    /// <summary>
    /// Gets the taxonomy nodes array consisted of the root taxonomy with the specified id and all related split taxonomies.
    /// </summary>
    /// <typeparam name="TTaxonomy">The type of the taxonomy.</typeparam>
    /// <param name="rootTaxonomyId">The root taxonomy id.</param>
    /// <returns>The taxonomy with the specified id and all related split taxonomies</returns>
    internal Guid[] GetTaxonomyNodes(Guid rootTaxonomyId)
    {
      this.MultisiteTaxonomyGuard.IsRootTaxonomy(rootTaxonomyId);
      return this.GetTaxonomies<Taxonomy>().Where<Taxonomy>((Expression<Func<Taxonomy, bool>>) (t => t.Id == rootTaxonomyId || t.RootTaxonomyId == (Guid?) rootTaxonomyId)).Select<Taxonomy, Guid>((Expression<Func<Taxonomy, Guid>>) (t => t.Id)).ToArray<Guid>();
    }

    /// <summary>Gets the sites using the specified taxonomy.</summary>
    /// <remarks>If the specified taxonomy is a root taxonomy it is resolved in the current context.</remarks>
    /// <typeparam name="TTaxonomy">The type of the taxonomy.</typeparam>
    /// <param name="taxonomy">The taxonomy.</param>
    /// <returns>List of sites.</returns>
    internal IEnumerable<Telerik.Sitefinity.Multisite.ISite> GetRelatedSitesInContext(
      ITaxonomy taxonomy)
    {
      if (taxonomy == null)
        throw new ArgumentNullException(nameof (taxonomy));
      if (taxonomy.IsRootTaxonomy())
      {
        Taxonomy splitTaxonomy = this.GetSplitTaxonomy<Taxonomy>(taxonomy.Id);
        if (splitTaxonomy != null)
          return this.GetRelatedSites((ITaxonomy) splitTaxonomy);
      }
      return this.GetRelatedSites(taxonomy);
    }

    private void MarkTaxonomyAsDirty(ITaxonomy taxonomy)
    {
      string name = taxonomy.Name;
      taxonomy.Name = string.Empty;
      taxonomy.Name = name;
    }

    private void RegisterTrackingContextSiteOperation(
      IHasTrackingContext trackingContextItem,
      OperationStatus operationStatus,
      Guid siteIdGuid)
    {
      ITrackingContext trackingContext = trackingContextItem.TrackingContext;
      if (operationStatus == OperationStatus.UnlinkWithSite && trackingContext.Operation == OperationStatus.LinkWithSite)
        throw new InvalidOperationException("You cannot register UnlinkWithSite operation for item which already has LinkWithSite operation.");
      if (operationStatus == OperationStatus.LinkWithSite && trackingContext.Operation == OperationStatus.UnlinkWithSite)
        throw new InvalidOperationException("You cannot register LinkWithSite operation for item which already has UnlinkWithSite operation.");
      trackingContext.RegisterOperation(operationStatus, siteIds: new Guid[1]
      {
        siteIdGuid
      });
    }

    /// <summary>
    /// Recalculates the taxonomy statistic for the specified type and providers, making sure that the statistics returns the actual marked items count.
    /// </summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="providers">The providers to synchronize. If null - all providers will be synchronized</param>
    public static void RecalculateStatistics(Type itemType, string[] providers = null) => new TaxonomyStatisticsSynchronizer().Syncronize(itemType, providers);

    /// <summary>Gets the taxonomy property descriptor.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="taxon">The taxon.</param>
    /// <returns></returns>
    public static TaxonomyPropertyDescriptor GetPropertyDescriptor(
      Type itemType,
      ITaxon taxon)
    {
      ITaxonomy taxonomy = TaxonomyManager.GetTaxonomy(taxon);
      return taxonomy.IsSplitTaxonomy() ? TaxonomyManager.GetPropertyDescriptor(itemType, taxonomy.RootTaxonomy) : TaxonomyManager.GetPropertyDescriptor(itemType, taxonomy);
    }

    /// <summary>Gets the taxonomy property descriptor.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="taxon">The taxonomy.</param>
    /// <returns></returns>
    public static TaxonomyPropertyDescriptor GetPropertyDescriptor(
      Type itemType,
      ITaxonomy taxonomy)
    {
      return TaxonomyManager.GetPropertyDescriptor(itemType, taxonomy.Id);
    }

    /// <summary>Gets the property descriptor.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="taxonomyId">The taxonomy id.</param>
    /// <returns></returns>
    public static TaxonomyPropertyDescriptor GetPropertyDescriptor(
      Type itemType,
      Guid taxonomyId)
    {
      return ((IEnumerable<TaxonomyPropertyDescriptor>) OrganizerBase.GetPropertiesForType(itemType)).Where<TaxonomyPropertyDescriptor>((Func<TaxonomyPropertyDescriptor, bool>) (p => p.TaxonomyId == taxonomyId)).SingleOrDefault<TaxonomyPropertyDescriptor>();
    }

    /// <summary>
    /// A common bullet-proof method for getting the taxonomy of the specified taxon.
    /// </summary>
    /// <param name="taxon">The taxon.</param>
    /// <returns></returns>
    public static ITaxonomy GetTaxonomy(ITaxon taxon)
    {
      ITaxonomy taxonomy = (ITaxonomy) null;
      if (taxon is HierarchicalTaxon)
      {
        HierarchicalTaxon hierarchicalTaxon = (HierarchicalTaxon) taxon;
        while (taxonomy == null)
        {
          taxonomy = (ITaxonomy) hierarchicalTaxon.Taxonomy;
          hierarchicalTaxon = hierarchicalTaxon.Parent;
        }
      }
      else
        taxonomy = taxon.Taxonomy;
      return taxonomy;
    }

    public static string GetTaxonomyEditUrl(ITaxonomy taxonomy)
    {
      string empty = string.Empty;
      string url;
      if (taxonomy.GetType() == typeof (FlatTaxonomy))
        url = BackendSiteMap.FindSiteMapNode(SiteInitializer.FlatTaxonomyPageId, false).Url;
      else if (taxonomy.GetType() == typeof (HierarchicalTaxonomy))
        url = BackendSiteMap.FindSiteMapNode(SiteInitializer.HierarchicalTaxonomyPageId, false).Url;
      else if (taxonomy.GetType() == typeof (NetworkTaxonomy))
      {
        url = BackendSiteMap.FindSiteMapNode(SiteInitializer.NetworkTaxonomyPageId, false).Url;
      }
      else
      {
        if (!(taxonomy.GetType() == typeof (FacetTaxonomy)))
          throw new ArgumentException("Invalid taxonomy type.");
        url = BackendSiteMap.FindSiteMapNode(SiteInitializer.FacetTaxonomyPageId, false).Url;
      }
      return VirtualPathUtility.ToAbsolute(VirtualPathUtility.AppendTrailingSlash(url) + taxonomy.Name);
    }

    public static string GetTaxonomyUserFriendlyName(ITaxonomy taxonomy)
    {
      string empty = string.Empty;
      if (taxonomy.GetType() == typeof (FlatTaxonomy))
        return Res.Get<TaxonomyResources>().FlatTaxonomyUserFriendly;
      if (taxonomy.GetType() == typeof (HierarchicalTaxonomy))
        return Res.Get<TaxonomyResources>().HierarchicalTaxonomyUserFriendly;
      if (taxonomy.GetType() == typeof (NetworkTaxonomy))
        return Res.Get<TaxonomyResources>().NetworkTaxonomyUserFriendly;
      if (taxonomy.GetType() == typeof (FacetTaxonomy))
        return Res.Get<TaxonomyResources>().FacetTaxonomyUserFriendly;
      throw new ArgumentException("Invalid taxonomy type.");
    }

    public static string GetTaxonomyCssClass(ITaxonomy taxonomy)
    {
      string empty = string.Empty;
      if (taxonomy.GetType() == typeof (FlatTaxonomy))
        return "sfFlat";
      if (taxonomy.GetType() == typeof (HierarchicalTaxonomy))
        return "sfHierarchical";
      if (taxonomy.GetType() == typeof (NetworkTaxonomy))
        return "sfFacet";
      if (taxonomy.GetType() == typeof (FacetTaxonomy))
        return "sfNetwork";
      throw new ArgumentException("Invalid taxonomy type.");
    }

    internal static string GetTaxonomyFieldControlTemplate(string fieldName, ITaxonomy taxonomy)
    {
      string str1 = (string) null;
      string str2 = string.Empty;
      string str3 = string.Empty;
      if (taxonomy.GetType() == typeof (FlatTaxonomy))
      {
        str1 = "FlatTaxonField";
        str2 = "~/Sitefinity/Services/Taxonomies/FlatTaxon.svc";
        str3 = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Fields.FlatTaxonFieldReadMode.ascx");
      }
      else if (taxonomy.GetType() == typeof (HierarchicalTaxonomy))
      {
        str1 = "HierarchicalTaxonField";
        str2 = "~/Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc";
        str3 = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Fields.HierarchicalTaxonFieldReadMode.ascx");
      }
      if (str1 == null)
        return (string) null;
      return string.Format(ControlUtilities.GetSitefinityTextResource("Telerik.Sitefinity.Resources.Templates.Fields.FrontendTaxonFieldTag.htm"), (object) str1, (object) fieldName, (object) taxonomy.Id, (object) str2, (object) str3);
    }

    /// <summary>Gets taxon's closest adjecent items.</summary>
    /// <param name="taxon">The taxon.</param>
    /// <param name="direction">The direction.</param>
    private Pair<IOrderedItem, IOrderedItem> GetTaxonClosestAdjecentItems(
      HierarchicalTaxon taxon,
      MovingDirection direction)
    {
      IQueryable<HierarchicalTaxon> taxa = this.Provider.GetTaxa<HierarchicalTaxon>();
      IQueryable<HierarchicalTaxon> source;
      if (taxon.Parent == null)
      {
        Guid taxonomyId = taxon.Taxonomy.Id;
        source = taxa.Where<HierarchicalTaxon>((Expression<Func<HierarchicalTaxon, bool>>) (t => t.Taxonomy.Id == taxonomyId && t.Parent == default (object)));
      }
      else
      {
        Guid parentId = taxon.Parent.Id;
        source = taxa.Where<HierarchicalTaxon>((Expression<Func<HierarchicalTaxon, bool>>) (t => t.Parent.Id == parentId));
      }
      float taxonOrdinalValue = taxon.Ordinal;
      switch (direction)
      {
        case MovingDirection.Up:
          source = source.Where<HierarchicalTaxon>((Expression<Func<HierarchicalTaxon, bool>>) (t => t.Ordinal < taxonOrdinalValue)).OrderByDescending<HierarchicalTaxon, float>((Expression<Func<HierarchicalTaxon, float>>) (t => t.Ordinal)).Take<HierarchicalTaxon>(2);
          break;
        case MovingDirection.Down:
          source = source.Where<HierarchicalTaxon>((Expression<Func<HierarchicalTaxon, bool>>) (t => t.Ordinal > taxonOrdinalValue)).OrderBy<HierarchicalTaxon, float>((Expression<Func<HierarchicalTaxon, float>>) (t => t.Ordinal)).Take<HierarchicalTaxon>(2);
          break;
      }
      HierarchicalTaxon[] array = source.ToArray<HierarchicalTaxon>();
      if (array.Length < 1)
      {
        if (direction == MovingDirection.Up)
          throw new InvalidOperationException(Res.Get<TaxonomyResources>().CannotMoveFirstTaxonUp);
        if (direction == MovingDirection.Down)
          throw new InvalidOperationException(Res.Get<TaxonomyResources>().CannotMoveLastTaxonDown);
        throw new InvalidOperationException(Res.Get<TaxonomyResources>().CannotMoveTaxon);
      }
      IOrderedItem first = (IOrderedItem) ((IEnumerable<HierarchicalTaxon>) array).First<HierarchicalTaxon>();
      IOrderedItem second = array.Length < 2 ? (IOrderedItem) null : (IOrderedItem) array[1];
      if (direction == MovingDirection.Up)
      {
        IOrderedItem orderedItem = first;
        first = second;
        second = orderedItem;
      }
      return new Pair<IOrderedItem, IOrderedItem>(first, second);
    }

    /// <summary>
    /// Orders the taxons between the first and the last item in the taxons collection by traversing them in width first.
    /// </summary>
    /// <param name="taxons">The taxons.</param>
    /// <param name="startIndex">The start index.</param>
    /// <param name="endIndex">The end index.</param>
    /// <remarks>
    /// This way of traversing of the taxons ensures the uniformly distribution of them between the first and the last taxons in the collection.
    /// </remarks>
    private void OrderWidthFirst(IList<IOrderedItem> taxons, int startIndex, int endIndex)
    {
      if (startIndex == endIndex || startIndex + 1 == endIndex)
        return;
      int num = (startIndex + endIndex) / 2;
      taxons[num].SetOrdinalBetweenItems(taxons[startIndex], taxons[endIndex]);
      this.OrderWidthFirst(taxons, startIndex, num);
      this.OrderWidthFirst(taxons, num, endIndex);
    }

    internal MultisiteTaxonomyGuard MultisiteTaxonomyGuard
    {
      get
      {
        if (this.multisiteTaxonomyGuard == null)
          this.multisiteTaxonomyGuard = ObjectFactory.Container.Resolve<MultisiteTaxonomyGuard>((ResolverOverride[]) new ParameterOverride[1]
          {
            new ParameterOverride("manager", (object) this)
          });
        return this.multisiteTaxonomyGuard;
      }
      set => this.multisiteTaxonomyGuard = value;
    }

    internal bool ShouldIgnoreSiteContext()
    {
      NameValueCollection queryString = SystemManager.CurrentHttpContext.Request.QueryString;
      return queryString.Keys.Contains("sf_ignore_site_context", StringComparison.OrdinalIgnoreCase) && queryString["sf_ignore_site_context"].Equals("true", StringComparison.OrdinalIgnoreCase);
    }

    internal static void Provider_Executing(object sender, ExecutingEventArgs args)
    {
      if (!(args.CommandName == "CommitTransaction") && !(args.CommandName == "FlushTransaction"))
        return;
      TaxonomyDataProvider taxonomyDataProvider = sender as TaxonomyDataProvider;
      IList dirtyItems = taxonomyDataProvider.GetDirtyItems();
      List<Guid> data = (List<Guid>) null;
      foreach (object itemInTransaction in (IEnumerable) dirtyItems)
      {
        if (itemInTransaction is Taxon taxon)
        {
          if (taxonomyDataProvider.GetDirtyItemStatus(itemInTransaction) == SecurityConstants.TransactionActionType.Deleted)
          {
            if (data == null)
              data = new List<Guid>();
            data.Add(taxon.Id);
            taxon.Taxonomy = (Taxonomy) null;
            PackagingOperations.DeleteAddonLinks(taxon.Id, taxon.GetType().FullName);
          }
          else if (taxon.Name.IsNullOrEmpty())
          {
            string stringAnyLanguage = taxon.Title.GetStringAnyLanguage(out CultureInfo _);
            if (!stringAnyLanguage.IsNullOrEmpty())
              taxon.Name = stringAnyLanguage.Replace(" ", string.Empty);
          }
        }
        if (itemInTransaction is Taxonomy taxonomy)
        {
          if (taxonomyDataProvider.GetDirtyItemStatus(itemInTransaction) == SecurityConstants.TransactionActionType.Deleted)
            PackagingOperations.DeleteAddonLinks(taxonomy.Id, taxonomy.GetType().FullName);
          else if (taxonomy.Name.IsNullOrEmpty())
          {
            string stringAnyLanguage = taxonomy.Title.GetStringAnyLanguage(out CultureInfo _);
            if (!stringAnyLanguage.IsNullOrEmpty())
              taxonomy.Name = stringAnyLanguage.Replace(" ", string.Empty);
          }
        }
      }
      if (data == null || data.Count <= 0)
        return;
      taxonomyDataProvider.SetExecutionStateData("deleted_taxons_execution_state", (object) data);
    }

    internal static void Provider_Executed(object sender, ExecutedEventArgs args)
    {
      if (!(args.CommandName == "CommitTransaction"))
        return;
      TaxonomyDataProvider provider = sender as TaxonomyDataProvider;
      if (!(provider.GetExecutionStateData("deleted_taxons_execution_state") is List<Guid> executionStateData))
        return;
      provider.SetExecutionStateData("deleted_taxons_execution_state", (object) null);
      TaxonomyManager.TryUnmarkAllItems(provider, (IEnumerable<Guid>) executionStateData);
    }

    private static void TryUnmarkAllItems(TaxonomyDataProvider provider, IEnumerable<Guid> taxons)
    {
      foreach (Guid taxon in taxons)
      {
        Guid taxonId = taxon;
        IQueryable<TaxonomyStatistic> statistics = provider.GetStatistics();
        Expression<Func<TaxonomyStatistic, bool>> predicate = (Expression<Func<TaxonomyStatistic, bool>>) (st => st.TaxonId == taxonId);
        foreach (TaxonomyStatistic statistic in (IEnumerable<TaxonomyStatistic>) statistics.Where<TaxonomyStatistic>(predicate))
        {
          Type type = TypeResolutionService.ResolveType(statistic.DataItemType, false);
          if (type != (Type) null)
          {
            TaxonomyPropertyDescriptor propertyDescriptor = TaxonomyManager.GetPropertyDescriptor(type, statistic.TaxonomyId);
            if (propertyDescriptor != null)
            {
              try
              {
                IManager mappedManager = ManagerBase.GetMappedManager(type, statistic.ItemProviderName);
                if (mappedManager.Provider is IOrganizableProvider provider1)
                {
                  string filterExpression = (string) null;
                  if (typeof (Content).IsAssignableFrom(type) || typeof (DynamicContent).IsAssignableFrom(type))
                    filterExpression = statistic.StatisticType == ContentLifecycleStatus.Master ? "Status = Master" : "Status = Live";
                  using (new ElevatedModeRegion(mappedManager))
                  {
                    int? totalCount = new int?(0);
                    foreach (object obj in provider1.GetItemsByTaxon(taxonId, propertyDescriptor.MetaField.IsSingleTaxon, propertyDescriptor.Name, type, filterExpression, (string) null, 0, 0, ref totalCount))
                    {
                      if (!propertyDescriptor.MetaField.IsSingleTaxon)
                        ((IOrganizable) obj).Organizer.RemoveTaxa(propertyDescriptor.Name, taxonId);
                      else
                        ((IOrganizable) obj).Organizer.RemoveTaxon(propertyDescriptor.Name, taxonId);
                    }
                  }
                  mappedManager.SaveChanges();
                }
                provider.DeleteStatistic(statistic);
              }
              catch (Exception ex)
              {
                if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
                  throw;
              }
            }
          }
        }
      }
      if (provider.GetDirtyItems().Count <= 0)
        return;
      provider.CommitTransaction();
    }

    private static IEnumerable<Guid> GetAllTaxaIDs(ITaxon taxon)
    {
      List<Guid> list = new List<Guid>();
      TaxonomyManager.FillTaxaRecursive(taxon, (IList<Guid>) list);
      return (IEnumerable<Guid>) list;
    }

    private static void FillTaxaRecursive(ITaxon taxon, IList<Guid> list)
    {
      list.Add(taxon.Id);
      if (!(taxon is HierarchicalTaxon hierarchicalTaxon))
        return;
      foreach (ITaxon taxon1 in (IEnumerable<HierarchicalTaxon>) hierarchicalTaxon.Subtaxa)
        TaxonomyManager.FillTaxaRecursive(taxon1, list);
    }

    private Func<PropertyDescriptor, bool> GetIgnoreCopyFunction() => (Func<PropertyDescriptor, bool>) (p => p.Attributes[typeof (SkipCopyAttribute)] is SkipCopyAttribute attribute && attribute.Skip);
  }
}
