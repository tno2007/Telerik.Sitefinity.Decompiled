// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Data.OpenAccessTaxonomyProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Telerik.OpenAccess.Metadata;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Decorators;
using Telerik.Sitefinity.Data.Events;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Events;
using Telerik.Sitefinity.Taxonomies.Events;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Taxonomies.Data
{
  /// <summary>
  /// Represents taxonomy data provider that uses OpenAccess to store and retrieve taxonomy data.
  /// </summary>
  [UrlProviderDecorator(typeof (OpenAccessUrlProviderDecorator))]
  public class OpenAccessTaxonomyProvider : 
    TaxonomyDataProvider,
    IOpenAccessDataProvider,
    IDataProviderBase,
    IDisposable,
    ICloneable,
    IDataProviderEventsCall,
    IOpenAccessMetadataProvider,
    IDataEventProvider,
    IMultisiteEnabledOAProvider
  {
    /// <summary>Creates new taxonomy.</summary>
    /// <typeparam name="TTaxonomy">The type of the taxonomy.</typeparam>
    /// <returns>The new taxonomy.</returns>
    public override TTaxonomy CreateTaxonomy<TTaxonomy>() => this.CreateTaxonomy<TTaxonomy>(this.GetNewGuid());

    /// <summary>Creates new taxonomy with the provided ID.</summary>
    /// <typeparam name="TTaxonomy">The type of the taxonomy.</typeparam>
    /// <param name="pageId">The taxonomy id.</param>
    /// <returns>The new taxonomy.</returns>
    public override TTaxonomy CreateTaxonomy<TTaxonomy>(Guid id) => (TTaxonomy) this.CreateItem(typeof (TTaxonomy), id);

    /// <summary>Gets the taxonomy with the specified ID.</summary>
    /// <param name="pageId">The ID.</param>
    /// <returns></returns>
    public override ITaxonomy GetTaxonomy(Guid id)
    {
      Taxonomy taxonomy = !(id == Guid.Empty) ? this.GetContext().GetItemById<Taxonomy>(id.ToString()) : throw new ArgumentException("Id cannot be an Empty Guid");
      ((IDataItem) taxonomy).Provider = (object) this;
      return (ITaxonomy) taxonomy;
    }

    /// <summary>Gets the taxonomy with the specified ID.</summary>
    /// <typeparam name="TTaxonomy">The type of the taxonomy.</typeparam>
    /// <param name="pageId">The ID.</param>
    /// <returns></returns>
    public override TTaxonomy GetTaxonomy<TTaxonomy>(Guid id)
    {
      TTaxonomy taxonomy = !(id == Guid.Empty) ? this.GetContext().GetItemById<TTaxonomy>(id.ToString()) : throw new ArgumentException("Id cannot be an Empty Guid");
      taxonomy.Provider = (object) this;
      return taxonomy;
    }

    /// <summary>Gets the taxonomies.</summary>
    /// <typeparam name="TTaxonomy">The type of the taxonomies to get.</typeparam>
    /// <returns></returns>
    public override IQueryable<TTaxonomy> GetTaxonomies<TTaxonomy>()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<TTaxonomy>((DataProviderBase) this).Where<TTaxonomy>((Expression<Func<TTaxonomy, bool>>) (t => t.ApplicationName == appName));
    }

    /// <summary>Deletes the taxonomy.</summary>
    /// <param name="taxonomy">The taxonomy.</param>
    public override void Delete(ITaxonomy taxonomy)
    {
      this.GuardTaxonomyHasNoSplits(taxonomy);
      foreach (ITaxon taxon in taxonomy.Taxa)
        this.Delete(taxon);
      this.DeleteLinksForItem((IDataItem) taxonomy, typeof (Taxonomy).FullName);
      this.DeleteItem_private((object) taxonomy);
    }

    /// <summary>Creates the taxon.</summary>
    /// <typeparam name="TTaxon">The type of the taxon.</typeparam>
    /// <returns></returns>
    public override TTaxon CreateTaxon<TTaxon>() => this.CreateTaxon<TTaxon>(this.GetNewGuid());

    /// <summary>Creates the taxon.</summary>
    /// <typeparam name="TTaxon">The type of the taxon.</typeparam>
    /// <param name="pageId">The pageId.</param>
    /// <returns></returns>
    public override TTaxon CreateTaxon<TTaxon>(Guid id) => (TTaxon) this.CreateItem(typeof (TTaxon), id);

    /// <summary>Gets the taxon with the specified ID.</summary>
    /// <param name="pageId">The ID of the taxon.</param>
    /// <returns>The taxon.</returns>
    public override ITaxon GetTaxon(Guid id)
    {
      if (id == Guid.Empty)
        throw new ArgumentException("Id cannot be an Empty Guid");
      Taxon result = (Taxon) null;
      if (this.GetContext().TryGetItemById<Taxon>(id.ToString(), out result))
        ((IDataItem) result).Provider = (object) this;
      return (ITaxon) result;
    }

    /// <summary>Gets the taxon with the specified ID.</summary>
    /// <typeparam name="TTaxon">The type of the taxon.</typeparam>
    /// <param name="pageId">The ID of the taxon.</param>
    /// <returns>The taxon.</returns>
    public override TTaxon GetTaxon<TTaxon>(Guid id)
    {
      TTaxon taxon = !(id == Guid.Empty) ? this.GetContext().GetItemById<TTaxon>(id.ToString()) : throw new ArgumentException("Id cannot be an Empty Guid");
      taxon.Provider = (object) this;
      return taxon;
    }

    /// <summary>
    /// Gets a query for all taxon objects from all taxonomies of the given type.
    /// </summary>
    /// <typeparam name="TTaxon">The type of the taxon.</typeparam>
    /// <returns>The query.</returns>
    public override IQueryable<TTaxon> GetTaxa<TTaxon>()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<TTaxon>((DataProviderBase) this, MethodBase.GetCurrentMethod()).Where<TTaxon>((Expression<Func<TTaxon, bool>>) (t => t.ApplicationName == appName));
    }

    /// <summary>Deletes the taxon.</summary>
    /// <param name="taxon">The taxon.</param>
    public override void Delete(ITaxon taxon)
    {
      if (taxon is HierarchicalTaxon hierarchicalTaxon)
      {
        while (hierarchicalTaxon.Subtaxa.Count<HierarchicalTaxon>() > 0)
          this.Delete((ITaxon) hierarchicalTaxon.Subtaxa.First<HierarchicalTaxon>());
      }
      this.DeleteItem_private((object) taxon);
    }

    /// <summary>
    /// Marks the provided persistent item for deletion.
    /// The item is deleted form the storage when the transaction is committed.
    /// </summary>
    /// <param name="item">The item to be deleted.</param>
    internal virtual void DeleteItem_private(object item)
    {
      SitefinityOAContext context = this.GetContext();
      this.providerDecorator.DeletePermissions(item);
      object entity = item;
      context.Remove(entity);
    }

    /// <summary>Creates the item.</summary>
    /// <param name="type">The type.</param>
    /// <param name="pageId">The pageId.</param>
    /// <returns></returns>
    private new object CreateItem(Type type, Guid id)
    {
      object instance = Activator.CreateInstance(type, (object) this.ApplicationName, (object) id);
      ((IDataItem) instance).Provider = (object) this;
      if (instance is ISecuredObject)
      {
        ((ISecuredObject) instance).InheritsPermissions = true;
        ((ISecuredObject) instance).CanInheritPermissions = true;
        this.CreatePermissionInheritanceAssociation(this.GetSecurityRoot(), (ISecuredObject) instance);
      }
      if (instance is IOwnership)
        ((IOwnership) instance).Owner = SecurityManager.GetCurrentUserId();
      SitefinityOAContext context = this.GetContext();
      if (id != Guid.Empty)
        context.Add(instance);
      return instance;
    }

    /// <summary>Creates new synonym.</summary>
    /// <returns></returns>
    public override Synonym CreateSynonym() => this.CreateSynonym(this.GetNewGuid());

    /// <summary>Creates new synonym with the provided identity.</summary>
    /// <param name="pageId">The pageId.</param>
    /// <returns></returns>
    public override Synonym CreateSynonym(Guid id)
    {
      Synonym entity = !(id == Guid.Empty) ? new Synonym()
      {
        ApplicationName = this.ApplicationName,
        Id = id
      } : throw new ArgumentException("id cannot be empty Guid");
      ((IDataItem) entity).Provider = (object) this;
      this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>Gets the synonym for the provided identity.</summary>
    /// <param name="pageId">The identity number.</param>
    /// <returns></returns>
    public override Synonym GetSynonym(Guid id)
    {
      Synonym synonym = !(id == Guid.Empty) ? this.GetContext().GetItemById<Synonym>(id.ToString()) : throw new ArgumentException("id cannot be empty Guid");
      ((IDataItem) synonym).Provider = (object) this;
      return synonym;
    }

    /// <summary>Gets a query object for the synonyms.</summary>
    /// <returns></returns>
    public override IQueryable<Synonym> GetSynonyms()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<Synonym>((DataProviderBase) this).Where<Synonym>((Expression<Func<Synonym, bool>>) (s => s.ApplicationName == appName));
    }

    /// <summary>Deletes the specified synonym.</summary>
    /// <param name="synonym">The synonym.</param>
    public override void Delete(Synonym synonym) => this.GetContext().Remove((object) synonym);

    /// <summary>
    /// Create a new instance of the <see cref="T:Telerik.Sitefinity.Taxonomies.Model.TaxonomyStatistic" /> type.
    /// </summary>
    /// <param name="dataItemType">Type of the data item.</param>
    /// <param name="taxonomyId">The pageId of the taxonomy to which the taxon belongs to.</param>
    /// <param name="taxonId">The pageId of the taxon for which the statistics are being maintained.</param>
    /// <returns>
    /// Newly created <see cref="T:Telerik.Sitefinity.Taxonomies.Model.TaxonomyStatistic" /> object.
    /// </returns>
    public override TaxonomyStatistic CreateStatistic(
      Type dataItemType,
      Guid taxonomyId,
      Guid taxonId)
    {
      if (dataItemType == (Type) null)
        throw new ArgumentNullException(nameof (dataItemType));
      if (taxonomyId == Guid.Empty)
        throw new ArgumentNullException(nameof (taxonomyId));
      if (taxonId == Guid.Empty)
        throw new ArgumentNullException(nameof (taxonId));
      TaxonomyStatistic entity = new TaxonomyStatistic()
      {
        Id = this.GetNewGuid(),
        ApplicationName = this.ApplicationName,
        DataItemType = dataItemType.FullName,
        TaxonId = taxonId,
        TaxonomyId = taxonomyId
      };
      ((IDataItem) entity).Provider = (object) this;
      this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>Gets a query object for the taxonomy statistics.</summary>
    /// <returns>Taxonomy statistics query.</returns>
    public override IQueryable<TaxonomyStatistic> GetStatistics()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<TaxonomyStatistic>((DataProviderBase) this).Where<TaxonomyStatistic>((Expression<Func<TaxonomyStatistic, bool>>) (s => s.ApplicationName == appName));
    }

    /// <summary>Deletes the taxonomy statistics object.</summary>
    /// <param name="statistic">The taxonomy statistic object to be deleted.</param>
    public override void DeleteStatistic(TaxonomyStatistic statistic)
    {
      if (statistic == null)
        throw new ArgumentNullException(nameof (statistic));
      this.GetContext().Remove((object) statistic);
    }

    /// <summary>Gets the meta data source.</summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public MetadataSource GetMetaDataSource(IDatabaseMappingContext context) => (MetadataSource) new TaxonomyMetadataSource(context);

    /// <summary>
    /// Gets or sets the OpenAccess context. Alternative to Database.
    /// </summary>
    /// <value>The context.</value>
    public OpenAccessProviderContext Context { get; set; }

    /// <inheritdoc />
    public override SiteItemLink CreateSiteItemLink() => MultisiteExtensions.CreateSiteItemLink(this);

    /// <inheritdoc />
    public override void Delete(SiteItemLink link) => MultisiteExtensions.Delete(this, link);

    /// <inheritdoc />
    public override void DeleteLinksForItem(IDataItem item) => MultisiteExtensions.DeleteLinksForItem(this, item);

    /// <inheritdoc />
    public override IQueryable<SiteItemLink> GetSiteItemLinks() => MultisiteExtensions.GetSiteItemLinks(this);

    /// <inheritdoc />
    public override SiteItemLink AddItemLink(Guid siteId, IDataItem item) => this.AddItemLink(siteId, item, typeof (Taxonomy).FullName);

    /// <inheritdoc />
    public override IQueryable<T> GetSiteItems<T>(Guid siteId) => MultisiteExtensions.GetSiteItems<T>(this, siteId);

    /// <inheritdoc />
    bool IDataEventProvider.DataEventsEnabled => true;

    /// <inheritdoc />
    bool IDataEventProvider.ApplyDataEventItemFilter(IDataItem item) => item is Taxon || item is Taxonomy;

    /// <inheritdoc />
    protected override ICollection<IEvent> GetDataEventItems(
      Func<IDataItem, bool> filterPredicate)
    {
      IList dirtyItems = this.GetDirtyItems();
      List<IEvent> result = new List<IEvent>(dirtyItems.Count);
      foreach (object itemInTransaction in (IEnumerable) dirtyItems)
      {
        if (itemInTransaction is IDataItem dataItem && filterPredicate(dataItem))
        {
          SecurityConstants.TransactionActionType dirtyItemStatus = this.GetDirtyItemStatus(itemInTransaction);
          this.GetEventsForTrackingContextItem(dataItem, dirtyItemStatus, result);
        }
      }
      return (ICollection<IEvent>) result;
    }

    private void GetEventsForTrackingContextItem(
      IDataItem dataItem,
      SecurityConstants.TransactionActionType action,
      List<IEvent> result)
    {
      if (!(dataItem is IHasTrackingContext hasTrackingContext))
        return;
      if (hasTrackingContext.HasDeletedOperation())
        action = SecurityConstants.TransactionActionType.Deleted;
      foreach (string eventLanguage in this.GetEventLanguages(hasTrackingContext))
      {
        if (this.IsValidEvent(dataItem, action, eventLanguage))
          result.Add(this.CreateEventItem(dataItem, action.ToString(), eventLanguage));
      }
      if (hasTrackingContext.HasOperation(OperationStatus.UnlinkWithSite) || hasTrackingContext.HasOperation(OperationStatus.LinkWithSite))
      {
        ITrackingContext trackingContext = hasTrackingContext.TrackingContext;
        string eventOrigin = this.GetEventOrigin();
        foreach (Guid siteId in trackingContext.SiteIds)
          result.Add((IEvent) LinkEventFactory.CreateEvent(dataItem, trackingContext.Operation, siteId, action.ToString(), eventOrigin));
      }
      hasTrackingContext.Clear();
    }

    private bool IsValidEvent(
      IDataItem dataItem,
      SecurityConstants.TransactionActionType action,
      string language)
    {
      if (action != SecurityConstants.TransactionActionType.Updated)
        return true;
      CultureInfo culture = string.IsNullOrEmpty(language) ? (CultureInfo) null : CultureInfo.GetCultureInfo(language);
      return DataEventFactory.GetChangedProperties(dataItem, culture).Count<KeyValuePair<string, PropertyChange>>() != 0;
    }

    private List<string> GetEventLanguages(IHasTrackingContext hasTrackingContext)
    {
      if (hasTrackingContext.GetLanguages().Any<string>())
        return hasTrackingContext.GetLanguages().Where<string>((Func<string, bool>) (l => !l.IsNullOrEmpty())).ToList<string>();
      return new List<string>()
      {
        SystemManager.CurrentContext.Culture.Name
      };
    }

    /// <summary>
    /// Guards that the specified rootTaxonomy has no split taxonomies linked to it.
    /// </summary>
    /// <param name="rootTaxonomy">The rootTaxonomy to be checked.</param>
    private void GuardTaxonomyHasNoSplits(ITaxonomy rootTaxonomy)
    {
      if (rootTaxonomy.IsSplitTaxonomy())
        return;
      string appName = this.ApplicationName;
      Guid[] array1 = this.GetContext().GetAll<Taxonomy>().Where<Taxonomy>((Expression<Func<Taxonomy, bool>>) (t => t.ApplicationName == appName && t.RootTaxonomyId == (Guid?) rootTaxonomy.Id)).Select<Taxonomy, Guid>((Expression<Func<Taxonomy, Guid>>) (t => t.Id)).ToArray<Guid>();
      if (array1.Length == 0)
        return;
      Guid[] array2 = this.GetDirtyItems().OfType<Taxonomy>().Where<Taxonomy>((Func<Taxonomy, bool>) (t => this.GetDirtyItemStatus((object) t) == SecurityConstants.TransactionActionType.Deleted)).Select<Taxonomy, Guid>((Func<Taxonomy, Guid>) (t => t.Id)).ToArray<Guid>();
      if (((IEnumerable<Guid>) array1).Except<Guid>((IEnumerable<Guid>) array2).Count<Guid>() > 0)
        throw new InvalidOperationException(string.Format("Taxonomy with Id: {0} and Type: {1} cannot be delete because there are split taxonomies linked to it.", (object) rootTaxonomy.Id, (object) rootTaxonomy.GetType().FullName));
    }
  }
}
