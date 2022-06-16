// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Organizers.PageDataOrganizer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Taxonomies.Organizers
{
  /// <summary>
  /// Organizer implementation for the <see cref="!:PageData" /> data item type.
  /// </summary>
  public class PageDataOrganizer : OrganizerBase
  {
    private IMetadataManager metadataManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Taxonomies.Organizers.PageDataOrganizer" /> class.
    /// </summary>
    /// <param name="dataItem">The data item.</param>
    public PageDataOrganizer(IDataItem dataItem, IMetadataManager metadataManager)
      : base(dataItem)
    {
      this.metadataManager = metadataManager;
    }

    /// <summary>Adds a specified taxon to the data item.</summary>
    /// <param name="propertyName">Name of the property which is used for persisting the taxon.</param>
    /// <param name="taxonId">Id of the taxon which should be added.</param>
    public override void SetTaxon(string propertyName, Guid taxonId) => throw new NotImplementedException();

    /// <summary>Adds taxa (multiple taxons) to the data item.</summary>
    /// <param name="propertyName">Name of the property which is used for persisting the taxa (multiple taxons).</param>
    /// <param name="taxonIds">Array of taxon ids that should be added.</param>
    /// <exception cref="T:System.InvalidOperationException">
    /// Thrown if the property is not a collection.
    /// </exception>
    public override void AddTaxa(string propertyName, params Guid[] taxonIds) => throw new NotImplementedException();

    /// <summary>Removes the taxon from the data item.</summary>
    /// <param name="propertyName">Name of the property which is used for persisting the taxon.</param>
    /// <param name="taxonId">Id of the taxon which should be removed.</param>
    /// <exception cref="!:InvalidArgumentException">
    /// Thrown if taxon does not exist in the specified property.
    /// </exception>
    /// <remarks>
    /// Use the TaxonExists method to check if taxon exists prior to calling this method.
    /// </remarks>
    public override void RemoveTaxon(string propertyName, Guid taxonId) => throw new NotImplementedException();

    /// <summary>
    /// Removes the multiple taxon objects from the data item.
    /// </summary>
    /// <param name="propertyName">Name of the property which is used for persisting the taxa (multiple taxons).</param>
    /// <param name="taxonIds">Array of taxon ids that should be removed.</param>
    /// <exception cref="!:InvalidArgumentException">
    /// Thrown if one or more of the taxon objects does not exist on the property of the data item.
    /// </exception>
    public override void RemoveTaxa(string propertyName, params Guid[] taxonIds) => throw new NotImplementedException();

    /// <summary>Clears all the taxa from the property.</summary>
    /// <param name="propertyName">Name of the property which holds the taxa that ought to be cleared.</param>
    public override void Clear(string propertyName) => throw new NotImplementedException();

    /// <summary>
    /// Determines whether a given taxon exists on the specified property.
    /// </summary>
    /// <param name="propertyName">Name of the property which is used for persisting the taxa or taxon.</param>
    /// <param name="taxonId">Id of the taxon for which the existence is being checked.</param>
    /// <returns>
    /// True if the taxon exists on the data item in the given property; otherwise false
    /// </returns>
    public override bool TaxonExists(string propertyName, Guid taxonId) => throw new NotImplementedException();
  }
}
