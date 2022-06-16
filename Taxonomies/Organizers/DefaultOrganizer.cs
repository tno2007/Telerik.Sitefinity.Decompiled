// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Organizers.DefaultOrganizer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Telerik.OpenAccess;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.RecycleBin;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Taxonomies.Web;

namespace Telerik.Sitefinity.Taxonomies.Organizers
{
  /// <summary>
  /// This is the default implementation of the organizer type for data items that use taxonomies.
  /// </summary>
  public class DefaultOrganizer : OrganizerBase
  {
    private IMetadataManager metadataManager;
    private const string collectionNullMessage = "The property '{0}' is a collection and should always return an instance of a collection (even an empty one).";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Taxonomies.Organizers.DefaultOrganizer" /> class.
    /// </summary>
    /// <param name="dataItem">The data item.</param>
    public DefaultOrganizer(IDataItem dataItem)
      : this(dataItem, (IMetadataManager) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Taxonomies.Organizers.DefaultOrganizer" /> class.
    /// </summary>
    /// <param name="dataItem">The data item.</param>
    /// <param name="metadataManager">The metadata manager used to work with meta fields.</param>
    public DefaultOrganizer(IDataItem dataItem, IMetadataManager metadataManager)
      : base(dataItem)
    {
      this.metadataManager = metadataManager;
    }

    /// <summary>Sets the taxon.</summary>
    /// <param name="propertyName">Name of the property.</param>
    /// <param name="taxonId">Id of the taxon which should be added.</param>
    /// <remarks>
    /// This property can only be used on properties that hold single taxon; it is invalid to use it on
    /// taxa properties (collection of taxons)
    /// </remarks>
    public override void SetTaxon(string propertyName, Guid taxonId)
    {
      if (taxonId == Guid.Empty)
        throw new ArgumentNullException("taxon");
      if (!(this.GetPropertyDescriptor(propertyName, this.TaxonType) is TaxonomyPropertyDescriptor propertyDescriptor))
        throw new ArgumentException("'{0}' is not a taxonomy field".Arrange((object) propertyName));
      Guid taxonId1 = (Guid) propertyDescriptor.GetValue((object) this.DataItem);
      if (taxonId1 == Guid.Empty)
      {
        this.AdjustMarkedItems(propertyDescriptor, taxonId, 1);
      }
      else
      {
        this.AdjustMarkedItems(propertyDescriptor, taxonId1, -1);
        this.AdjustMarkedItems(propertyDescriptor, taxonId, 1);
      }
      propertyDescriptor.SetValue((object) this.DataItem, (object) taxonId);
    }

    /// <summary>Adds taxa (multiple taxons) to the data item.</summary>
    /// <param name="propertyName">Name of the property which is used for persisting the taxa (multiple taxons).</param>
    /// <param name="taxonIds">Array of taxon ids that should be added.</param>
    /// <remarks>
    /// This method can be only used for properties that hold taxa (collection of taxons); it is invalid to use it
    /// on a single taxon property
    /// </remarks>
    /// <exception cref="T:System.InvalidOperationException">
    /// Thrown if the property is not a collection.
    /// </exception>
    public override void AddTaxa(string propertyName, params Guid[] taxonIds)
    {
      if (taxonIds == null)
        throw new ArgumentNullException(nameof (taxonIds));
      if (!(this.GetPropertyDescriptor(propertyName, this.TaxaType) is TaxonomyPropertyDescriptor propertyDescriptor))
        throw new ArgumentException("'{0}' is not a taxonomy field".Arrange((object) propertyName));
      if (!(propertyDescriptor.GetValue((object) this.DataItem) is TrackedList<Guid> trackedList))
        throw new NullReferenceException(string.Format("The property '{0}' is a collection and should always return an instance of a collection (even an empty one).", (object) propertyName));
      foreach (Guid guid in trackedList)
      {
        if (((IEnumerable<Guid>) taxonIds).Contains<Guid>(guid))
          throw new InvalidOperationException("A data item is already marked with this taxon.");
      }
      foreach (Guid taxonId in taxonIds)
      {
        trackedList.Add(taxonId);
        this.AdjustMarkedItems(propertyDescriptor, taxonId, 1);
      }
    }

    /// <summary>Removes the taxon from the data item.</summary>
    /// <param name="propertyName">Name of the property which is used for persisting the taxon.</param>
    /// <param name="taxonId">Id of the taxon which should be removed.</param>
    /// <exception cref="T:System.ArgumentException">
    /// Thrown if taxon does not exist in the specified property.
    /// </exception>
    /// <remarks>
    /// Use the TaxonExists method to check if taxon exists prior to calling this method. This property can only be
    /// used on properties that hold single taxon; it is invalid to use it on taxa properties (collection of taxons)
    /// </remarks>
    public override void RemoveTaxon(string propertyName, Guid taxonId)
    {
      if (taxonId == Guid.Empty)
        throw new ArgumentNullException(nameof (taxonId));
      if (!(this.GetPropertyDescriptor(propertyName, this.TaxonType) is TaxonomyPropertyDescriptor propertyDescriptor))
        throw new ArgumentException("'{0}' is not a taxonomy field".Arrange((object) propertyName));
      Guid guid = (Guid) propertyDescriptor.GetValue((object) this.DataItem);
      if (guid == Guid.Empty)
        throw new InvalidOperationException(string.Format("The property '{0}' is not set to an instance of an object and hence taxon cannot be removed from it.", (object) propertyName));
      if (guid != taxonId)
        throw new ArgumentException(string.Format("The specified taxonId '{0}' does not exist in property '{1}'.", (object) taxonId, (object) propertyName));
      propertyDescriptor.SetValue((object) this.DataItem, (object) Guid.Empty);
      this.AdjustMarkedItems(propertyDescriptor, taxonId, -1);
    }

    /// <summary>
    /// Removes the multiple taxon objects from the data item.
    /// </summary>
    /// <param name="propertyName">Name of the property which is used for persisting the taxa (multiple taxons).</param>
    /// <param name="taxonIds">Array of taxon ids that should be removed.</param>
    /// <remarks>
    /// This method can be only used for properties that hold taxa (collection of taxons); it is invalid to use it
    /// on a single taxon property
    /// </remarks>
    /// <exception cref="T:System.ArgumentException">
    /// Thrown if one or more of the taxon objects does not exist on the property of the data item.
    /// </exception>
    public override void RemoveTaxa(string propertyName, params Guid[] taxonIds)
    {
      if (taxonIds == null)
        throw new ArgumentNullException(nameof (taxonIds));
      if (!(this.GetPropertyDescriptor(propertyName, this.TaxaType) is TaxonomyPropertyDescriptor propertyDescriptor))
        throw new ArgumentException("'{0}' is not a taxonomy field".Arrange((object) propertyName));
      if (!(propertyDescriptor.GetValue((object) this.DataItem) is TrackedList<Guid> trackedList))
        throw new NullReferenceException(string.Format("The property '{0}' is a collection and should always return an instance of a collection (even an empty one).", (object) propertyName));
      foreach (Guid taxonId in taxonIds)
      {
        if (!trackedList.Contains(taxonId))
          throw new ArgumentException(string.Format("The property '{0}' that holds the taxa does not contain taxon id '{1}'; no data has been updated.", (object) propertyName, (object) taxonId));
      }
      foreach (Guid taxonId in taxonIds)
      {
        trackedList.Remove(taxonId);
        this.AdjustMarkedItems(propertyDescriptor, taxonId, -1);
      }
    }

    /// <summary>
    /// Clears the taxon or taxa from the property. This method can be used both with single taxon or
    /// collection of taxons (taxa).
    /// </summary>
    /// <param name="propertyName">Name of the property which holds the taxa that ought to be cleared.</param>
    public override void Clear(string propertyName)
    {
      if (!(this.GetPropertyDescriptor(propertyName, this.TaxonType, this.TaxaType) is TaxonomyPropertyDescriptor propertyDescriptor))
        throw new ArgumentException("'{0}' is not a taxonomy field".Arrange((object) propertyName));
      if (propertyDescriptor.PropertyType == this.TaxonType)
      {
        Guid taxonId = (Guid) propertyDescriptor.GetValue((object) this.DataItem);
        if (taxonId != Guid.Empty)
          this.AdjustMarkedItems(propertyDescriptor, taxonId, -1);
        propertyDescriptor.SetValue((object) this.DataItem, (object) Guid.Empty);
      }
      else
      {
        if (!(propertyDescriptor.PropertyType == this.TaxaType))
          return;
        if (!(propertyDescriptor.GetValue((object) this.DataItem) is TrackedList<Guid> taxons))
          throw new NullReferenceException(string.Format("The property '{0}' is a collection and should always return an instance of a collection (even an empty one).", (object) propertyName));
        this.AdjustMarkedItems(propertyDescriptor, (IEnumerable<Guid>) taxons, -1);
        taxons.Clear();
      }
    }

    /// <summary>
    /// Determines whether a given taxon exists on the specified property.
    /// </summary>
    /// <param name="propertyName">Name of the property which is used for persisting the taxa or taxon.</param>
    /// <param name="taxonId">Id of the taxon for which the existence is being checked.</param>
    /// <returns>
    /// True if the taxon exists on the data item in the given property; otherwise false
    /// </returns>
    public override bool TaxonExists(string propertyName, Guid taxonId)
    {
      if (taxonId == Guid.Empty)
        throw new ArgumentNullException(nameof (taxonId));
      PropertyDescriptor propertyDescriptor = this.GetPropertyDescriptor(propertyName, this.TaxonType, this.TaxaType);
      if (propertyDescriptor.PropertyType == this.TaxonType)
        return (Guid) propertyDescriptor.GetValue((object) this.DataItem) == taxonId;
      if (!(propertyDescriptor.PropertyType == this.TaxaType))
        throw new InvalidOperationException();
      if (!(propertyDescriptor.GetValue((object) this.DataItem) is TrackedList<Guid> trackedList))
        throw new NullReferenceException(string.Format("The property '{0}' is a collection and should always return an instance of a collection (even an empty one).", (object) propertyName));
      return trackedList.Contains(taxonId);
    }

    /// <summary>
    /// Performs the actual operation of adjusting the number of marked items on the data provider.
    /// </summary>
    /// <param name="property">The property descriptor used to determine the taxonomy provider.</param>
    /// <param name="taxonId">Id of the taxon that marks the items for which the adjustment is being made.</param>
    /// <param name="adjustment">The amount of adjustment to be made. Positive to increase; negative to decrease.</param>
    /// <returns>Returns true if items were adjusted; otherwise false</returns>
    public virtual bool AdjustMarkedItems(
      TaxonomyPropertyDescriptor property,
      Guid taxonId,
      int adjustment)
    {
      return this.AdjustMarkedItems(property, (IEnumerable<Guid>) new Guid[1]
      {
        taxonId
      }, adjustment);
    }

    private bool AdjustMarkedItems(
      TaxonomyPropertyDescriptor property,
      IEnumerable<Guid> taxons,
      int adjustment)
    {
      if (property == null)
        throw new ArgumentNullException(nameof (property));
      if (adjustment == 0)
        throw new ArgumentException("Adjustment cannot be zero (0).");
      if (this.StatisticType != ContentLifecycleStatus.Temp && (!(this.DataItem is IRecyclableDataItem dataItem) || !dataItem.IsDeleted))
      {
        DataProviderBase provider;
        this.GetTracker(out provider)?.Update(property, this.DataItem.GetType(), provider.Name, this.StatisticType, taxons, adjustment);
      }
      return true;
    }

    private TaxonomyStatisticsTracker GetTracker(
      out DataProviderBase provider)
    {
      provider = this.DataItem.Provider as DataProviderBase;
      if (provider == null)
        return (TaxonomyStatisticsTracker) null;
      if (!(provider.GetExecutionStateData("taxonomy_statistics_changes") is TaxonomyStatisticsTracker data))
      {
        data = new TaxonomyStatisticsTracker();
        data.SkipAutoTracking = true;
        provider.SetExecutionStateData("taxonomy_statistics_changes", (object) data);
      }
      return data;
    }

    /// <summary>Gets the instance</summary>
    /// <param name="property">The property descriptor through which the taxonomy provider name will be discovered.</param>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Taxonomies.Web.ITaxonomyManager" />.</returns>
    protected internal virtual ITaxonomyManager GetTaxonomyManager(
      TaxonomyPropertyDescriptor property)
    {
      string transactionName = (string) null;
      if (this.DataItem.Provider is DataProviderBase provider)
        transactionName = provider.TransactionName;
      return transactionName.IsNullOrEmpty() ? (ITaxonomyManager) TaxonomyManager.GetManager(property.TaxonomyProvider) : (ITaxonomyManager) TaxonomyManager.GetManager(property.TaxonomyProvider, transactionName);
    }
  }
}
