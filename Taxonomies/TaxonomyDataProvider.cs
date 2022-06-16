// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.TaxonomyDataProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Taxonomies
{
  /// <summary>Represents base class for taxonomy data providers.</summary>
  public abstract class TaxonomyDataProvider : DataProviderBase, IMultisiteEnabledProvider
  {
    private string[] supportedPermissionSets = new string[1]
    {
      "Taxonomies"
    };
    private Type[] acceptedTypes = new Type[8]
    {
      typeof (FlatTaxonomy),
      typeof (FacetTaxonomy),
      typeof (HierarchicalTaxonomy),
      typeof (NetworkTaxonomy),
      typeof (FlatTaxon),
      typeof (FacetTaxon),
      typeof (HierarchicalTaxon),
      typeof (NetworkTaxon)
    };

    /// <summary>Creates new taxonomy.</summary>
    /// <typeparam name="TTaxonomy">The type of the taxonomy.</typeparam>
    /// <returns>The new taxonomy.</returns>
    [ValuePermission("Taxonomies", new string[] {"CreateTaxonomy"})]
    public abstract TTaxonomy CreateTaxonomy<TTaxonomy>() where TTaxonomy : class, ITaxonomy;

    /// <summary>Creates new taxonomy with the provided ID.</summary>
    /// <typeparam name="TTaxonomy">The type of the taxonomy.</typeparam>
    /// <param name="pageId">The pageId.</param>
    /// <returns>The new taxonomy.</returns>
    [ValuePermission("Taxonomies", new string[] {"CreateTaxonomy"})]
    public abstract TTaxonomy CreateTaxonomy<TTaxonomy>(Guid id) where TTaxonomy : class, ITaxonomy;

    /// <summary>Gets the taxonomy with the specified ID.</summary>
    /// <param name="pageId">The ID.</param>
    /// <returns></returns>
    [ValuePermission("Taxonomies", new string[] {"ViewTaxonomy"})]
    public abstract ITaxonomy GetTaxonomy(Guid id);

    /// <summary>Gets the taxonomy with the specified ID.</summary>
    /// <typeparam name="TTaxonomy">The type of the taxonomy.</typeparam>
    /// <param name="pageId">The ID.</param>
    /// <returns></returns>
    [ValuePermission("Taxonomies", new string[] {"ViewTaxonomy"})]
    public abstract TTaxonomy GetTaxonomy<TTaxonomy>(Guid id) where TTaxonomy : class, ITaxonomy;

    /// <summary>Gets the taxonomies.</summary>
    /// <typeparam name="TTaxonomy">The type of the taxonomies to get.</typeparam>
    /// <returns></returns>
    [EnumeratorPermission("Taxonomies", new string[] {"ViewTaxonomy"})]
    public abstract IQueryable<TTaxonomy> GetTaxonomies<TTaxonomy>() where TTaxonomy : class, ITaxonomy;

    /// <summary>Deletes the taxonomy.</summary>
    /// <param name="taxonomy">The taxonomy.</param>
    [ParameterPermission("taxonomy", "Taxonomies", new string[] {"DeleteTaxonomy"})]
    public abstract void Delete(ITaxonomy taxonomy);

    /// <summary>Creates the taxon.</summary>
    /// <typeparam name="TTaxon">The type of the taxon.</typeparam>
    /// <returns></returns>
    [ValuePermission("Taxonomies", new string[] {"ModifyTaxonomyAndSubTaxons"})]
    public abstract TTaxon CreateTaxon<TTaxon>() where TTaxon : class, ITaxon;

    /// <summary>Creates the taxon.</summary>
    /// <typeparam name="TTaxon">The type of the taxon.</typeparam>
    /// <param name="pageId">The pageId.</param>
    /// <returns></returns>
    [ValuePermission("Taxonomies", new string[] {"ModifyTaxonomyAndSubTaxons"})]
    public abstract TTaxon CreateTaxon<TTaxon>(Guid id) where TTaxon : class, ITaxon;

    /// <summary>Gets the taxon with the specified ID.</summary>
    /// <param name="pageId">The ID of the taxon.</param>
    /// <returns>The taxon.</returns>
    [ValuePermission("Taxonomies", new string[] {"ViewTaxonomy"})]
    public abstract ITaxon GetTaxon(Guid id);

    /// <summary>Gets the taxon with the specified ID.</summary>
    /// <typeparam name="TTaxon">The type of the taxon.</typeparam>
    /// <param name="pageId">The ID of the taxon.</param>
    /// <returns>The taxon.</returns>
    [ValuePermission("Taxonomies", new string[] {"ViewTaxonomy"})]
    public abstract TTaxon GetTaxon<TTaxon>(Guid id) where TTaxon : class, ITaxon;

    /// <summary>
    /// Gets a query for all taxon objects from all taxonomies of the given type.
    /// </summary>
    /// <typeparam name="TTaxon">The type of the taxon.</typeparam>
    /// <returns>The query.</returns>
    [EnumeratorPermission("Taxonomies", new string[] {"ViewTaxonomy"})]
    public abstract IQueryable<TTaxon> GetTaxa<TTaxon>() where TTaxon : class, ITaxon;

    /// <summary>Deletes the taxon.</summary>
    /// <param name="taxon">The taxon.</param>
    [ParameterPermission("taxon", "Taxonomies", new string[] {"ModifyTaxonomyAndSubTaxons"})]
    public abstract void Delete(ITaxon taxon);

    /// <summary>Creates new synonym.</summary>
    /// <returns></returns>
    public abstract Synonym CreateSynonym();

    /// <summary>Creates new synonym with the provided identity.</summary>
    /// <param name="pageId">The pageId.</param>
    /// <returns></returns>
    public abstract Synonym CreateSynonym(Guid id);

    /// <summary>Gets the synonym for the provided identity.</summary>
    /// <param name="pageId">The identity number.</param>
    /// <returns></returns>
    public abstract Synonym GetSynonym(Guid id);

    /// <summary>Gets a query object for the synonyms.</summary>
    /// <returns></returns>
    public abstract IQueryable<Synonym> GetSynonyms();

    /// <summary>Deletes the specified synonym.</summary>
    /// <param name="synonym">The synonym.</param>
    public abstract void Delete(Synonym synonym);

    /// <summary>
    /// Create a new instance of the <see cref="T:Telerik.Sitefinity.Taxonomies.Model.TaxonomyStatistic" /> type.
    /// </summary>
    /// <param name="dataItemType">Type of the data item.</param>
    /// <param name="taxonomyId">The pageId of the taxonomy to which the taxon belongs to.</param>
    /// <param name="taxonId">The pageId of the taxon for which the statistics are being maintained.</param>
    /// <returns>
    /// Newly created <see cref="T:Telerik.Sitefinity.Taxonomies.Model.TaxonomyStatistic" /> object.
    /// </returns>
    public abstract TaxonomyStatistic CreateStatistic(
      Type dataItemType,
      Guid taxonomyId,
      Guid taxonId);

    /// <summary>Gets a query object for the taxonomy statistics.</summary>
    /// <returns>Taxonomy statistics query.</returns>
    public abstract IQueryable<TaxonomyStatistic> GetStatistics();

    /// <summary>Deletes the taxonomy statistics object.</summary>
    /// <param name="statistic">The taxonomy statistics object to be deleted.</param>
    public abstract void DeleteStatistic(TaxonomyStatistic statistic);

    /// <summary>
    /// Gets the permission sets relevant to this specific secured object.
    /// To be overridden by relevant providers (which involve security roots)
    /// </summary>
    /// <value>The supported permission sets.</value>
    public override string[] SupportedPermissionSets
    {
      get => this.supportedPermissionSets;
      set => this.supportedPermissionSets = value;
    }

    /// <summary>Creates new data item.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="pageId">The pageId.</param>
    /// <returns></returns>
    public override object CreateItem(Type itemType, Guid id)
    {
      if (itemType == (Type) null)
        throw new ArgumentNullException(nameof (itemType));
      if (itemType == typeof (FlatTaxonomy))
        return (object) this.CreateTaxonomy<FlatTaxonomy>(id);
      if (itemType == typeof (FacetTaxonomy))
        return (object) this.CreateTaxonomy<FacetTaxonomy>(id);
      if (itemType == typeof (HierarchicalTaxonomy))
        return (object) this.CreateTaxonomy<HierarchicalTaxonomy>(id);
      if (itemType == typeof (NetworkTaxonomy))
        return (object) this.CreateTaxonomy<NetworkTaxonomy>(id);
      if (itemType == typeof (FlatTaxon))
        return (object) this.CreateTaxon<FlatTaxon>(id);
      if (itemType == typeof (FacetTaxon))
        return (object) this.CreateTaxon<FacetTaxon>(id);
      if (itemType == typeof (HierarchicalTaxon))
        return (object) this.CreateTaxon<HierarchicalTaxon>(id);
      if (itemType == typeof (NetworkTaxon))
        return (object) this.CreateTaxon<NetworkTaxon>(id);
      throw DataProviderBase.GetInvalidItemTypeException(itemType, this.acceptedTypes);
    }

    /// <summary>
    /// Gets the data item with the specified ID.
    /// An exception should be thrown if an item with the specified ID does not exist.
    /// </summary>
    /// <param name="itemType"></param>
    /// <param name="pageId">The ID of the item to return.</param>
    /// <returns></returns>
    public override object GetItem(Type itemType, Guid id)
    {
      if (itemType == (Type) null)
        throw new ArgumentNullException(nameof (itemType));
      if (itemType == typeof (Taxonomy))
        return (object) this.GetTaxonomy<Taxonomy>(id);
      if (itemType == typeof (FlatTaxonomy))
        return (object) this.GetTaxonomy<FlatTaxonomy>(id);
      if (itemType == typeof (FacetTaxonomy))
        return (object) this.GetTaxonomy<FacetTaxonomy>(id);
      if (itemType == typeof (HierarchicalTaxonomy))
        return (object) this.GetTaxonomy<HierarchicalTaxonomy>(id);
      if (itemType == typeof (NetworkTaxonomy))
        return (object) this.GetTaxonomy<NetworkTaxonomy>(id);
      if (itemType == typeof (Taxon))
        return (object) this.GetTaxon(id);
      if (itemType == typeof (FlatTaxon))
        return (object) this.GetTaxon<FlatTaxon>(id);
      if (itemType == typeof (FacetTaxon))
        return (object) this.GetTaxon<FacetTaxon>(id);
      if (itemType == typeof (HierarchicalTaxon))
        return (object) this.GetTaxon<HierarchicalTaxon>(id);
      return itemType == typeof (NetworkTaxon) ? (object) this.GetTaxon<NetworkTaxon>(id) : base.GetItem(itemType, id);
    }

    /// <summary>
    /// Get item by primary key without throwing exceptions if it doesn't exist
    /// </summary>
    /// <param name="itemType">Type of the item to get</param>
    /// <param name="id">Primary key</param>
    /// <returns>Item or default value</returns>
    public override object GetItemOrDefault(Type itemType, Guid id)
    {
      if (itemType == (Type) null)
        throw new ArgumentNullException(nameof (itemType));
      if (itemType == typeof (Taxonomy))
        return (object) this.GetTaxonomies<Taxonomy>().Where<Taxonomy>((Expression<Func<Taxonomy, bool>>) (t => t.Id == id)).FirstOrDefault<Taxonomy>();
      if (itemType == typeof (FlatTaxonomy))
        return (object) this.GetTaxonomies<FlatTaxonomy>().Where<FlatTaxonomy>((Expression<Func<FlatTaxonomy, bool>>) (t => t.Id == id)).FirstOrDefault<FlatTaxonomy>();
      if (itemType == typeof (FacetTaxonomy))
        return (object) this.GetTaxonomies<FacetTaxonomy>().Where<FacetTaxonomy>((Expression<Func<FacetTaxonomy, bool>>) (t => t.Id == id)).FirstOrDefault<FacetTaxonomy>();
      if (itemType == typeof (HierarchicalTaxonomy))
        return (object) this.GetTaxonomies<HierarchicalTaxonomy>().Where<HierarchicalTaxonomy>((Expression<Func<HierarchicalTaxonomy, bool>>) (t => t.Id == id)).FirstOrDefault<HierarchicalTaxonomy>();
      if (itemType == typeof (NetworkTaxonomy))
        return (object) this.GetTaxonomies<NetworkTaxonomy>().Where<NetworkTaxonomy>((Expression<Func<NetworkTaxonomy, bool>>) (t => t.Id == id)).FirstOrDefault<NetworkTaxonomy>();
      if (itemType == typeof (Taxon))
        return (object) this.GetTaxa<Taxon>().Where<Taxon>((Expression<Func<Taxon, bool>>) (t => t.Id == id)).FirstOrDefault<Taxon>();
      if (itemType == typeof (FlatTaxon))
        return (object) this.GetTaxa<FlatTaxon>().Where<FlatTaxon>((Expression<Func<FlatTaxon, bool>>) (t => t.Id == id)).FirstOrDefault<FlatTaxon>();
      if (itemType == typeof (FacetTaxon))
        return (object) this.GetTaxa<FacetTaxon>().Where<FacetTaxon>((Expression<Func<FacetTaxon, bool>>) (t => t.Id == id)).FirstOrDefault<FacetTaxon>();
      if (itemType == typeof (HierarchicalTaxon))
        return (object) this.GetTaxa<HierarchicalTaxon>().Where<HierarchicalTaxon>((Expression<Func<HierarchicalTaxon, bool>>) (t => t.Id == id)).FirstOrDefault<HierarchicalTaxon>();
      if (!(itemType == typeof (NetworkTaxon)))
        return base.GetItem(itemType, id);
      return (object) this.GetTaxa<NetworkTaxon>().Where<NetworkTaxon>((Expression<Func<NetworkTaxon, bool>>) (t => t.Id == id)).FirstOrDefault<NetworkTaxon>();
    }

    /// <summary>Gets the items.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="orderExpression">The order expression.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <param name="totalCount">Total count of the items that are filtered by <paramref name="filterExpression" /></param>
    /// <returns></returns>
    public override IEnumerable GetItems(
      Type itemType,
      string filterExpression,
      string orderExpression,
      int skip,
      int take,
      ref int? totalCount)
    {
      if (itemType == (Type) null)
        throw new ArgumentNullException(nameof (itemType));
      if (itemType == typeof (Taxonomy))
        return (IEnumerable) DataProviderBase.SetExpressions<Taxonomy>(this.GetTaxonomies<Taxonomy>(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      if (itemType == typeof (FlatTaxonomy))
        return (IEnumerable) DataProviderBase.SetExpressions<FlatTaxonomy>(this.GetTaxonomies<FlatTaxonomy>(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      if (itemType == typeof (FacetTaxonomy))
        return (IEnumerable) DataProviderBase.SetExpressions<FacetTaxonomy>(this.GetTaxonomies<FacetTaxonomy>(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      if (itemType == typeof (HierarchicalTaxonomy))
        return (IEnumerable) DataProviderBase.SetExpressions<HierarchicalTaxonomy>(this.GetTaxonomies<HierarchicalTaxonomy>(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      if (itemType == typeof (NetworkTaxonomy))
        return (IEnumerable) DataProviderBase.SetExpressions<NetworkTaxonomy>(this.GetTaxonomies<NetworkTaxonomy>(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      if (itemType == typeof (Taxon))
        return (IEnumerable) DataProviderBase.SetExpressions<Taxon>(this.GetTaxa<Taxon>(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      if (itemType == typeof (FlatTaxon))
        return (IEnumerable) DataProviderBase.SetExpressions<FlatTaxon>(this.GetTaxa<FlatTaxon>(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      if (itemType == typeof (FacetTaxon))
        return (IEnumerable) DataProviderBase.SetExpressions<FacetTaxon>(this.GetTaxa<FacetTaxon>(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      if (itemType == typeof (HierarchicalTaxon))
        return (IEnumerable) DataProviderBase.SetExpressions<HierarchicalTaxon>(this.GetTaxa<HierarchicalTaxon>(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      if (itemType == typeof (NetworkTaxon))
        return (IEnumerable) DataProviderBase.SetExpressions<NetworkTaxon>(this.GetTaxa<NetworkTaxon>(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      throw DataProviderBase.GetInvalidItemTypeException(itemType, this.GetKnownTypes());
    }

    /// <summary>
    /// Marks the provided persistent item for deletion.
    /// The item is deleted form the storage when the transaction is committed.
    /// </summary>
    /// <param name="item">The item to be deleted.</param>
    public override void DeleteItem(object item)
    {
      switch (item)
      {
        case null:
          throw new ArgumentNullException(nameof (item));
        case ITaxon _:
          this.Delete((ITaxon) item);
          break;
        case ITaxonomy _:
          this.Delete((ITaxonomy) item);
          break;
        default:
          throw DataProviderBase.GetInvalidItemTypeException(item.GetType(), this.GetKnownTypes());
      }
    }

    /// <summary>Get a list of types served by this manager</summary>
    /// <returns></returns>
    public override Type[] GetKnownTypes() => this.acceptedTypes;

    /// <inheritdoc />
    public abstract SiteItemLink CreateSiteItemLink();

    /// <inheritdoc />
    public abstract void Delete(SiteItemLink link);

    /// <inheritdoc />
    public abstract void DeleteLinksForItem(IDataItem item);

    /// <inheritdoc />
    public abstract IQueryable<SiteItemLink> GetSiteItemLinks();

    /// <inheritdoc />
    public abstract SiteItemLink AddItemLink(Guid siteId, IDataItem item);

    /// <inheritdoc />
    public abstract IQueryable<T> GetSiteItems<T>(Guid siteId) where T : IDataItem;

    /// <summary>Gets a unique key for each data provider base.</summary>
    /// <value></value>
    public override string RootKey => nameof (TaxonomyDataProvider);

    /// <summary>Flushes the provided transaction.</summary>
    [TransactionPermission(typeof (Taxonomy), "Taxonomies", SecurityConstants.TransactionActionType.Updated, new string[] {"ModifyTaxonomyAndSubTaxons"})]
    [TransactionPermission(typeof (HierarchicalTaxonomy), "Taxonomies", SecurityConstants.TransactionActionType.Updated, new string[] {"ModifyTaxonomyAndSubTaxons"})]
    [TransactionPermission(typeof (FlatTaxonomy), "Taxonomies", SecurityConstants.TransactionActionType.Updated, new string[] {"ModifyTaxonomyAndSubTaxons"})]
    [TransactionPermission(typeof (HierarchicalTaxon), "Taxonomies", "Taxonomy", SecurityConstants.TransactionActionType.Updated, new string[] {"ModifyTaxonomyAndSubTaxons"})]
    [TransactionPermission(typeof (FlatTaxon), "Taxonomies", "Taxonomy", SecurityConstants.TransactionActionType.Updated, new string[] {"ModifyTaxonomyAndSubTaxons"})]
    [TransactionPermission(typeof (Taxon), "Taxonomies", "Taxonomy", SecurityConstants.TransactionActionType.Updated, new string[] {"ModifyTaxonomyAndSubTaxons"})]
    [TransactionPermission(typeof (Taxon), "Taxonomies", "Taxonomy", SecurityConstants.TransactionActionType.Deleted, new string[] {"ModifyTaxonomyAndSubTaxons"})]
    public override void FlushTransaction() => base.FlushTransaction();

    /// <summary>Commits the provided transaction.</summary>
    [TransactionPermission(typeof (Taxonomy), "Taxonomies", SecurityConstants.TransactionActionType.Updated, new string[] {"ModifyTaxonomyAndSubTaxons"})]
    [TransactionPermission(typeof (HierarchicalTaxonomy), "Taxonomies", SecurityConstants.TransactionActionType.Updated, new string[] {"ModifyTaxonomyAndSubTaxons"})]
    [TransactionPermission(typeof (FlatTaxonomy), "Taxonomies", SecurityConstants.TransactionActionType.Updated, new string[] {"ModifyTaxonomyAndSubTaxons"})]
    [TransactionPermission(typeof (HierarchicalTaxon), "Taxonomies", "Taxonomy", SecurityConstants.TransactionActionType.Updated, new string[] {"ModifyTaxonomyAndSubTaxons"})]
    [TransactionPermission(typeof (FlatTaxon), "Taxonomies", "Taxonomy", SecurityConstants.TransactionActionType.Updated, new string[] {"ModifyTaxonomyAndSubTaxons"})]
    [TransactionPermission(typeof (Taxon), "Taxonomies", "Taxonomy", SecurityConstants.TransactionActionType.Updated, new string[] {"ModifyTaxonomyAndSubTaxons"})]
    [TransactionPermission(typeof (Taxon), "Taxonomies", "Taxonomy", SecurityConstants.TransactionActionType.Deleted, new string[] {"ModifyTaxonomyAndSubTaxons"})]
    public override void CommitTransaction() => base.CommitTransaction();

    /// <summary>Sets the root permissions.</summary>
    /// <param name="root">The root.</param>
    public override void SetRootPermissions(SecurityRoot root)
    {
      Permission permission1 = this.CreatePermission("Taxonomies", root.Id, SecurityManager.EveryoneRole.Id);
      permission1.GrantActions(false, "ViewTaxonomy");
      root.Permissions.Add(permission1);
      Permission permission2 = this.CreatePermission("Taxonomies", root.Id, SecurityManager.OwnerRole.Id);
      permission2.GrantActions(false, "ModifyTaxonomyAndSubTaxons", "DeleteTaxonomy");
      root.Permissions.Add(permission2);
      Permission permission3 = this.CreatePermission("Taxonomies", root.Id, SecurityManager.EditorsRole.Id);
      permission3.GrantActions(false, "CreateTaxonomy", "ModifyTaxonomyAndSubTaxons", "DeleteTaxonomy", "ChangeTaxonomyOwner");
      root.Permissions.Add(permission3);
      Permission permission4 = this.CreatePermission("Taxonomies", root.Id, SecurityManager.AuthorsRole.Id);
      permission4.GrantActions(false, "CreateTaxonomy", "ModifyTaxonomyAndSubTaxons");
      root.Permissions.Add(permission4);
    }
  }
}
