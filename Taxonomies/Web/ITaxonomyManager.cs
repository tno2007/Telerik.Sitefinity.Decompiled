// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Web.ITaxonomyManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Taxonomies.Web
{
  /// <summary>
  /// Defines the functionality of the business logic component of the taxonomies module
  /// </summary>
  public interface ITaxonomyManager : IManager, IDisposable, IProviderResolver
  {
    /// <summary>Creates new taxonomy.</summary>
    /// <typeparam name="TTaxonomy">The type of the taxonomy.</typeparam>
    /// <returns>The new taxonomy.</returns>
    TTaxonomy CreateTaxonomy<TTaxonomy>() where TTaxonomy : class, ITaxonomy;

    /// <summary>Creates new taxonomy with the provided ID.</summary>
    /// <typeparam name="TTaxonomy">The type of the taxonomy.</typeparam>
    /// <param name="pageId">The pageId.</param>
    /// <returns>The new taxonomy.</returns>
    TTaxonomy CreateTaxonomy<TTaxonomy>(Guid id) where TTaxonomy : class, ITaxonomy;

    /// <summary>Deletes the taxonomy.</summary>
    /// <param name="taxonomy">The taxonomy.</param>
    void Delete(ITaxonomy taxonomy);

    /// <summary>Gets the taxonomy with the specified ID.</summary>
    /// <param name="pageId">The ID.</param>
    /// <returns></returns>
    ITaxonomy GetTaxonomy(Guid id);

    /// <summary>Gets the taxonomy with the specified ID.</summary>
    /// <typeparam name="TTaxonomy">The type of the taxonomy.</typeparam>
    /// <param name="pageId">The ID.</param>
    /// <returns></returns>
    TTaxonomy GetTaxonomy<TTaxonomy>(Guid id) where TTaxonomy : class, ITaxonomy;

    /// <summary>Gets the taxonomies.</summary>
    /// <typeparam name="TTaxonomy">The type of the taxonomies to get.</typeparam>
    /// <returns></returns>
    IQueryable<TTaxonomy> GetTaxonomies<TTaxonomy>() where TTaxonomy : class, ITaxonomy;

    /// <summary>Creates the taxon.</summary>
    /// <typeparam name="TTaxon">The type of the taxon.</typeparam>
    /// <returns></returns>
    TTaxon CreateTaxon<TTaxon>() where TTaxon : class, ITaxon;

    /// <summary>Creates the taxon.</summary>
    /// <typeparam name="TTaxon">The type of the taxon.</typeparam>
    /// <param name="pageId">The pageId.</param>
    /// <returns></returns>
    TTaxon CreateTaxon<TTaxon>(Guid id) where TTaxon : class, ITaxon;

    /// <summary>Gets the taxon with the specified ID.</summary>
    /// <param name="pageId">The ID of the taxon.</param>
    /// <returns>The taxon.</returns>
    ITaxon GetTaxon(Guid id);

    /// <summary>Gets the taxon with the specified ID.</summary>
    /// <typeparam name="TTaxon">The type of the taxon.</typeparam>
    /// <param name="pageId">The ID of the taxon.</param>
    /// <returns>The taxon.</returns>
    TTaxon GetTaxon<TTaxon>(Guid id) where TTaxon : class, ITaxon;

    /// <summary>
    /// Gets a query for all taxon objects from all taxonomies of the given type.
    /// </summary>
    /// <typeparam name="TTaxon">The type of the taxon.</typeparam>
    /// <returns>The query.</returns>
    IQueryable<TTaxon> GetTaxa<TTaxon>() where TTaxon : class, ITaxon;

    /// <summary>Deletes the taxon.</summary>
    /// <param name="taxon">The taxon.</param>
    void Delete(ITaxon taxon);

    /// <summary>
    /// Adjusts the marked items count for the given taxon (taxon pageId) by the amount passed as count.
    /// </summary>
    /// <typeparam name="TDataItem">Type of the data item for which the count is being adjusted.</typeparam>
    /// <param name="taxonId">Id of the taxon for which the count is being adjusted.</param>
    /// <param name="count">
    /// Amount of the adjustment. Positive amount will increase count, while negative will decrease it.
    /// </param>
    void AdjustMarkedItemsCount<TDataItem>(
      Guid taxonId,
      int count,
      ContentLifecycleStatus statisticType);

    /// <summary>
    /// Adjusts the marked items count for the given taxon (taxon pageId) by the amount passed as count.
    /// </summary>
    /// <param name="dataItemType">Type of the data item for which the count is being adjusted.</param>
    /// <param name="taxonId">Id of the taxon for which the count is being adjusted.</param>
    /// <param name="count">
    /// Amount of the adjustment. Positive amount will increase count, while negative will decrease it.
    /// </param>
    void AdjustMarkedItemsCount(
      Type dataItemType,
      Guid taxonId,
      int count,
      ContentLifecycleStatus statisticType);

    /// <summary>Adjusts the marked items count.</summary>
    /// <param name="dataItem">The data item.</param>
    /// <param name="taxonId">The taxon id.</param>
    /// <param name="count">The count.</param>
    void AdjustMarkedItemsCount(
      IDataItem dataItem,
      Guid taxonId,
      int count,
      ContentLifecycleStatus statisticType);

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
    uint GetTaxonItemsCount<TDataItem>(Guid taxonId, ContentLifecycleStatus statisticType);

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
    uint GetTaxonItemsCount(Type dataItemType, Guid taxonId, ContentLifecycleStatus statisticType);

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
    uint GetTaxonomyItemsCount<TDataItem>(Guid taxonomyId, ContentLifecycleStatus statisticType);

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
    uint GetTaxonomyItemsCount(
      Type dataItemType,
      Guid taxonomyId,
      ContentLifecycleStatus statisticType);

    /// <summary>
    /// Gets all the types that have been marked with a specified taxon.
    /// </summary>
    /// <param name="taxonId">
    /// Id of the taxon for which the marked types ought to be retrieved.
    /// </param>
    /// <returns>An array of marked types; empty array if no types have been marked by that taxon.</returns>
    Type[] GetTypesByTaxon(Guid taxonId, ContentLifecycleStatus statisticType);

    /// <summary>
    /// Gets all the types that have been marked with a taxon belonging to
    /// a specifed taxonomy.
    /// </summary>
    /// <param name="taxonomyId">
    /// If of the taxonomy for which the types ought to be retrieved.
    /// </param>
    /// <returns>An array of marked types; empty array if no types have been marked by that taxonomy.</returns>
    Type[] GetTypesByTaxonomy(Guid taxonomyId, ContentLifecycleStatus statisticType);
  }
}
