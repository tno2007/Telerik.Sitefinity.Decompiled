// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.ContentFluentApi.OrganizationFacade`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Fluent.ContentFluentApi
{
  /// <summary>
  /// Facade for managing tags, categories and other taxonomies
  /// </summary>
  /// <typeparam name="TParentFacade">Type of the facade that hosts the organization facade</typeparam>
  public class OrganizationFacade<TParentFacade> : BaseFacadeWithParent<TParentFacade>
    where TParentFacade : BaseFacade
  {
    private IOrganizable organizable;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.ContentFluentApi.OrganizationFacade`1" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <param name="parentFacade">The parent facade. Can be null</param>
    /// <param name="organizable">Item whose taxonomies this facade will manage</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> or <paramref name="organizable" />is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public OrganizationFacade(
      AppSettings settings,
      TParentFacade parentFacade,
      IOrganizable organizable)
      : base(settings, parentFacade)
    {
      FacadeHelper.AssertArgumentNotNull<IOrganizable>(organizable, nameof (organizable));
      this.organizable = organizable;
    }

    /// <summary>IOrganizable whose taxonomies this facade manages</summary>
    /// <exception cref="T:System.InvalidOperationException">If, upon getting, returned value is null</exception>
    /// <exception cref="T:System.ArgumentNullException">If, upon setting, proposed value is null</exception>
    protected IOrganizable Organizable
    {
      get
      {
        FacadeHelper.AssertNotNull<IOrganizable>(this.organizable, "Organizable item can not be null");
        return this.organizable;
      }
      set
      {
        FacadeHelper.AssertArgumentNotNull<IOrganizable>(value, nameof (Organizable));
        this.organizable = value;
      }
    }

    /// <summary>
    /// Create a new instance of the manager in a named transaction using <see cref="!:settings" />
    /// </summary>
    /// <returns>Instance of this facade's manager</returns>
    /// <remarks>This is called internally by <see cref="!:GetManager" />. Do not call this manually unless you override GetManager as well.</remarks>
    protected override IManager InitializeManager() => throw new NotSupportedException();

    /// <summary>Adds taxa (multiple taxons) to the data item.</summary>
    /// <param name="taxaPropertyName">Name of the property which is used for persisting the taxon.</param>
    /// <param name="taxonIds">Collection of taxon ids that should be added.</param>
    /// <returns>Organization facade</returns>
    public virtual OrganizationFacade<TParentFacade> AddTaxa(
      string taxaPropertyName,
      IEnumerable<Guid> taxonIds)
    {
      this.Organizable.Organizer.AddTaxa(taxaPropertyName, taxonIds.ToArray<Guid>());
      return this;
    }

    /// <summary>Adds taxa (multiple taxons) to the data item.</summary>
    /// <param name="taxaPropertyName">Name of the property which is used for persisting the taxon.</param>
    /// <param name="taxa">Collection of taxons that should be added.</param>
    /// <returns>Organization facade</returns>
    public virtual OrganizationFacade<TParentFacade> AddTaxa(
      string taxaPropertyName,
      IEnumerable<ITaxon> taxa)
    {
      this.Organizable.Organizer.AddTaxa(taxaPropertyName, this.GetTaxaIds(taxa));
      return this;
    }

    /// <summary>
    /// Removes the multiple taxon objects from the data item.
    /// </summary>
    /// <param name="taxaPropertyName">Name of the property which is used for persisting the taxon.</param>
    /// <param name="taxonIds">Collection of the taxon ids which should be removed.</param>
    /// <returns>Organization facade</returns>
    public virtual OrganizationFacade<TParentFacade> RemoveTaxa(
      string taxaPropertyName,
      IEnumerable<Guid> taxonIds)
    {
      Guid[] array = taxonIds.ToArray<Guid>();
      this.Organizable.Organizer.RemoveTaxa(taxaPropertyName, array);
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
      this.Organizable.Organizer.RemoveTaxa(taxaPropertyName, this.GetTaxaIds(taxa));
      return this;
    }

    /// <summary>Sets a specified taxon to the data item.</summary>
    /// <param name="taxonPropertyName">Name of the property which is used for persisting the taxon.</param>
    /// <param name="taxonId">Id of the taxon which should be set.</param>
    /// <returns>Organization facade</returns>
    public virtual OrganizationFacade<TParentFacade> SetTaxon(
      string taxonPropertyName,
      Guid taxonId)
    {
      this.Organizable.Organizer.SetTaxon(taxonPropertyName, taxonId);
      return this;
    }

    /// <summary>Sets a specified taxon to the data item.</summary>
    /// <param name="taxonPropertyName">Name of the property which is used for persisting the taxon.</param>
    /// <param name="taxonId">The taxon which should be set.</param>
    /// <returns>Organization facade</returns>
    public virtual OrganizationFacade<TParentFacade> SetTaxon(
      string taxonPropertyName,
      ITaxon taxon)
    {
      this.Organizable.Organizer.SetTaxon(taxonPropertyName, taxon.Id);
      return this;
    }

    /// <summary>Clears all the taxa from the property.</summary>
    /// <param name="propertyName">Name of the property which holds the taxa that ought to be cleared.</param>
    public virtual OrganizationFacade<TParentFacade> Clear(string taxaPropertyName)
    {
      this.Organizable.Organizer.Clear(taxaPropertyName);
      return this;
    }

    /// <summary>
    /// Determines whether a given taxon exists on the specified property.
    /// </summary>
    /// <param name="taxaPropertyName">Name of the property which is used for persisting the taxa or taxon.</param>
    /// <param name="taxonId">Id of the taxon for which the existence is being checked.</param>
    /// <param name="exists">True if the taxon exists on the data item in the given property; otherwise false</param>
    /// <returns>Organization facade</returns>
    public virtual OrganizationFacade<TParentFacade> Exists(
      string taxaPropertyName,
      Guid taxonId,
      out bool exists)
    {
      exists = this.Organizable.Organizer.TaxonExists(taxaPropertyName, taxonId);
      return this;
    }

    /// <summary>
    /// Determines whether a given taxon exists on the specified property.
    /// </summary>
    /// <param name="taxaPropertyName">Name of the property which is used for persisting the taxa or taxon.</param>
    /// <param name="taxon">The taxon for which the existence is being checked.</param>
    /// <param name="exists">True if the taxon exists on the data item in the given property; otherwise false</param>
    /// <returns>Organization facade</returns>
    public virtual OrganizationFacade<TParentFacade> Exists(
      string taxaPropertyName,
      ITaxon taxon,
      out bool exists)
    {
      exists = this.Organizable.Organizer.TaxonExists(taxaPropertyName, taxon.Id);
      return this;
    }

    private Guid[] GetTaxaIds(IEnumerable<ITaxon> taxa) => taxa.Select<ITaxon, Guid>((Func<ITaxon, Guid>) (t => t.Id)).ToArray<Guid>();
  }
}
