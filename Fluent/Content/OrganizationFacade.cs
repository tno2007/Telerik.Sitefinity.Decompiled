// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Content.OrganizationFacade`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Fluent.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Fluent.Content
{
  [Obsolete]
  public class OrganizationFacade<TParentFacade> where TParentFacade : class
  {
    private AppSettings settings;
    private IOrganizable currentItem;
    private TParentFacade parentFacade;
    private MetadataManager manager;

    /// <summary>Gets or sets the parent facade.</summary>
    /// <value>The parent facade.</value>
    public virtual TParentFacade ParentFacade
    {
      get => this.parentFacade;
      set => this.parentFacade = value;
    }

    /// <summary>Gets or sets the current item.</summary>
    /// <value>The current item.</value>
    public virtual IOrganizable CurrentItem
    {
      get => this.currentItem;
      set => this.currentItem = value;
    }

    private Guid[] GetTaxaIds(IEnumerable<ITaxon> taxa)
    {
      Guid[] taxaIds = new Guid[taxa.Count<ITaxon>()];
      int index = 0;
      foreach (ITaxon taxon in taxa)
        taxaIds[index] = taxon.Id;
      return taxaIds;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Content.OrganizationFacade`1" /> class.
    /// </summary>
    /// <param name="settings">The settings.</param>
    /// <param name="item">The item.</param>
    /// <param name="parentFacade">The parent facade.</param>
    public OrganizationFacade(AppSettings settings, IOrganizable item, TParentFacade parentFacade)
    {
      if (item == null)
        throw new ArgumentNullException(nameof (item));
      if ((object) parentFacade == null)
        throw new ArgumentNullException(nameof (parentFacade));
      this.settings = settings != null ? settings : throw new ArgumentNullException(nameof (settings));
      this.CurrentItem = item;
      this.parentFacade = parentFacade;
    }

    /// <summary>Used only for Unit Tests</summary>
    internal OrganizationFacade()
    {
    }

    /// <summary>Adds taxa (multiple taxons) to the data item.</summary>
    /// <param name="taxaPropertyName">Name of the property which is used for persisting the taxon.</param>
    /// <param name="taxonIds">Collection of taxon ids that should be added.</param>
    /// <returns></returns>
    public virtual OrganizationFacade<TParentFacade> AddTaxa(
      string taxaPropertyName,
      IEnumerable<Guid> taxonIds)
    {
      this.CurrentItem.Organizer.AddTaxa(taxaPropertyName, taxonIds.ToArray<Guid>());
      return this;
    }

    /// <summary>Adds taxa (multiple taxons) to the data item.</summary>
    /// <param name="taxaPropertyName">Name of the property which is used for persisting the taxon.</param>
    /// <param name="taxa">Collection of taxons that should be added.</param>
    /// <returns></returns>
    public virtual OrganizationFacade<TParentFacade> AddTaxa(
      string taxaPropertyName,
      IEnumerable<ITaxon> taxa)
    {
      this.CurrentItem.Organizer.AddTaxa(taxaPropertyName, this.GetTaxaIds(taxa));
      return this;
    }

    /// <summary>
    /// Removes the multiple taxon objects from the data item.
    /// </summary>
    /// <param name="taxaPropertyName">Name of the property which is used for persisting the taxon.</param>
    /// <param name="taxonIds">Collection of the taxon ids which should be removed.</param>
    /// <returns></returns>
    public virtual OrganizationFacade<TParentFacade> RemoveTaxa(
      string taxaPropertyName,
      IEnumerable<Guid> taxonIds)
    {
      Guid[] array = taxonIds.ToArray<Guid>();
      this.CurrentItem.Organizer.RemoveTaxa(taxaPropertyName, array);
      return this;
    }

    /// <summary>
    /// Removes the multiple taxon objects from the data item.
    /// </summary>
    /// <param name="taxaPropertyName">Name of the property which is used for persisting the taxon.</param>
    /// <param name="taxa">Collection of the taxons which should be removed.</param>
    /// <returns></returns>
    public virtual OrganizationFacade<TParentFacade> RemoveTaxa(
      string taxaPropertyName,
      IEnumerable<ITaxon> taxa)
    {
      this.CurrentItem.Organizer.RemoveTaxa(taxaPropertyName, this.GetTaxaIds(taxa));
      return this;
    }

    /// <summary>Sets a specified taxon to the data item.</summary>
    /// <param name="taxonPropertyName">Name of the property which is used for persisting the taxon.</param>
    /// <param name="taxonId">Id of the taxon which should be set.</param>
    /// <returns></returns>
    public virtual OrganizationFacade<TParentFacade> SetTaxon(
      string taxonPropertyName,
      Guid taxonId)
    {
      this.CurrentItem.Organizer.SetTaxon(taxonPropertyName, taxonId);
      return this;
    }

    /// <summary>Sets a specified taxon to the data item.</summary>
    /// <param name="taxonPropertyName">Name of the property which is used for persisting the taxon.</param>
    /// <param name="taxonId">The taxon which should be set.</param>
    /// <returns></returns>
    public virtual OrganizationFacade<TParentFacade> SetTaxon(
      string taxonPropertyName,
      ITaxon taxon)
    {
      this.CurrentItem.Organizer.SetTaxon(taxonPropertyName, taxon.Id);
      return this;
    }

    /// <summary>Clears all the taxa from the property.</summary>
    /// <param name="propertyName">Name of the property which holds the taxa that ought to be cleared.</param>
    public virtual OrganizationFacade<TParentFacade> Clear(string taxaPropertyName)
    {
      this.CurrentItem.Organizer.Clear(taxaPropertyName);
      return this;
    }

    /// <summary>
    /// Determines whether a given taxon exists on the specified property.
    /// </summary>
    /// <param name="taxaPropertyName">Name of the property which is used for persisting the taxa or taxon.</param>
    /// <param name="taxonId">Id of the taxon for which the existence is being checked.</param>
    /// <param name="exists">True if the taxon exists on the data item in the given property; otherwise false</param>
    /// <returns></returns>
    public virtual OrganizationFacade<TParentFacade> Exists(
      string taxaPropertyName,
      Guid taxonId,
      out bool exists)
    {
      exists = this.CurrentItem.Organizer.TaxonExists(taxaPropertyName, taxonId);
      return this;
    }

    /// <summary>
    /// Determines whether a given taxon exists on the specified property.
    /// </summary>
    /// <param name="taxaPropertyName">Name of the property which is used for persisting the taxa or taxon.</param>
    /// <param name="taxon">The taxon for which the existence is being checked.</param>
    /// <param name="exists">True if the taxon exists on the data item in the given property; otherwise false</param>
    /// <returns></returns>
    public virtual OrganizationFacade<TParentFacade> Exists(
      string taxaPropertyName,
      ITaxon taxon,
      out bool exists)
    {
      exists = this.CurrentItem.Organizer.TaxonExists(taxaPropertyName, taxon.Id);
      return this;
    }

    /// <summary>
    /// Returns the parent facade that initialized this child facade.
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">
    /// thrown if method is called and parentFacade is null; meaning that facade is not a child facade in this context.
    /// </exception>
    /// <returns>An instance of the parent facade type that initialized this facade as a child facade.</returns>
    public virtual TParentFacade Done() => this.ParentFacade;

    /// <summary>Not Implemented</summary>
    /// <typeparam name="TTaxon"></typeparam>
    /// <returns></returns>
    public virtual TaxonFacade<TTaxon> Taxon<TTaxon>() => throw new NotImplementedException();

    /// <summary>Not Implemented</summary>
    /// <typeparam name="TTaxonomy"></typeparam>
    /// <returns></returns>
    public virtual TaxonomyFacade<TTaxonomy> Taxonomy<TTaxonomy>() => throw new NotImplementedException();

    /// <summary>Not Implemented</summary>
    /// <typeparam name="TTaxonomy"></typeparam>
    /// <returns></returns>
    public virtual TaxonomiesFacade<TTaxonomy> Taxonomies<TTaxonomy>() => throw new NotImplementedException();

    /// <summary>Not Implemented</summary>
    /// <typeparam name="TTaxon"></typeparam>
    /// <returns></returns>
    public virtual TaxaFacade<TTaxon> Taxa<TTaxon>() => throw new NotImplementedException();
  }
}
