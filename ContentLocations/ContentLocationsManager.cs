// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContentLocations.ContentLocationsManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Globalization;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.ContentLocations.Configuration;
using Telerik.Sitefinity.ContentLocations.Model;
using Telerik.Sitefinity.Data;

namespace Telerik.Sitefinity.ContentLocations
{
  /// <summary>Managers class for the Site Location.</summary>
  internal class ContentLocationsManager : ManagerBase<ContentLocationsDataProvider>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.ContentLocations.ContentLocationsManager" /> class.
    /// </summary>
    public ContentLocationsManager()
      : this((string) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.ContentLocations.ContentLocationsManager" /> class.
    /// </summary>
    /// <param name="providerName">Name of the provider.</param>
    public ContentLocationsManager(string providerName)
      : base(providerName)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.ContentLocations.ContentLocationsManager" /> class.
    /// </summary>
    /// <param name="providerName">
    /// The name of the provider. If empty string or null the default provider is set
    /// </param>
    /// <param name="transactionName">
    /// The name of a distributed transaction. If empty string or null this manager will use separate transaction.
    /// </param>
    public ContentLocationsManager(string providerName, string transactionName)
      : base(providerName, transactionName)
    {
    }

    internal ContentLocationDataItem CreateLocation() => this.Provider.CreateLocation();

    internal ContentLocationDataItem CreateLocation(Guid locationId) => this.Provider.CreateLocation(locationId);

    internal void Delete(ContentLocationDataItem location) => this.Provider.Delete(location);

    internal ContentLocationDataItem GetLocation(Guid locationId) => this.Provider.GetLocation(locationId);

    internal IQueryable<ContentLocationDataItem> GetLocations() => this.Provider.GetLocations();

    /// <summary>
    /// Gets the content filters of a specific content location.
    /// </summary>
    /// <param name="contentLocationId">The id of the content location the filters belong to.</param>
    /// <returns></returns>
    public virtual IQueryable<ContentLocationFilterDataItem> GetContentFilters(
      Guid contentLocationId)
    {
      return this.Provider.GetContentFilters(contentLocationId);
    }

    /// <summary>
    /// Creates a new <see cref="T:Telerik.Sitefinity.ContentLocations.Model.ContentLocationFilterDataItem" /> instance.
    /// </summary>
    internal ContentLocationFilterDataItem CreateContentFilter() => this.Provider.CreateContentFilter();

    /// <summary>
    /// Creates a new <see cref="T:Telerik.Sitefinity.ContentLocations.Model.ContentLocationFilterDataItem" /> with the given id.
    /// </summary>
    /// <param name="contentFilterId">The content id.</param>
    /// <returns></returns>
    internal ContentLocationFilterDataItem CreateContentFilter(
      Guid contentFilterId)
    {
      return this.Provider.CreateContentFilter(contentFilterId);
    }

    /// <summary>
    /// Marks a <see cref="T:Telerik.Sitefinity.ContentLocations.Model.ContentLocationFilterDataItem" /> for removal.
    /// </summary>
    /// <param name="contentFilter">The ContentFilter to delete.</param>
    internal void Delete(ContentLocationFilterDataItem contentFilter) => this.Provider.Delete(contentFilter);

    /// <summary>
    /// Gets a <see cref="T:Telerik.Sitefinity.ContentLocations.Model.ContentLocationFilterDataItem" /> object with the given id.
    /// </summary>
    /// <param name="contentFilterId">The content filter id.</param>
    internal ContentLocationFilterDataItem GetContentFilter(
      Guid contentFilterId)
    {
      return this.Provider.GetContentFilter(contentFilterId);
    }

    /// <summary>
    /// Gets an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.ContentLocations.Model.ContentLocationFilterDataItem" /> objects.
    /// </summary>
    /// <returns>
    /// Returns an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.ContentLocations.Model.ContentLocationFilterDataItem" /> objects.
    /// </returns>
    internal IQueryable<ContentLocationFilterDataItem> GetContentFilters() => this.Provider.GetContentFilters();

    protected internal override GetDefaultProvider DefaultProviderDelegate => (GetDefaultProvider) (() => Config.Get<ContentLocationsConfig>().DefaultProvider);

    public override string ModuleName => string.Empty;

    protected internal override ConfigElementDictionary<string, DataProviderSettings> ProvidersSettings => Config.Get<ContentLocationsConfig>().Providers;

    public override IEnumerable GetItems(
      Type itemType,
      string filterExpression,
      string orderExpression,
      int skip,
      int take)
    {
      return this.Provider.GetItems(itemType, filterExpression, orderExpression, skip, take);
    }

    public override IEnumerable GetItems(
      Type itemType,
      string filterExpression,
      string orderExpression,
      int skip,
      int take,
      ref int? totalCount)
    {
      return this.Provider.GetItems(itemType, filterExpression, orderExpression, skip, take, ref totalCount);
    }

    public override void DeleteItem(object item, CultureInfo language)
    {
      switch (item)
      {
        case ContentLocationDataItem _:
        case ContentLocationFilterDataItem _:
          this.DeleteItem(item);
          break;
        default:
          base.DeleteItem(item, language);
          break;
      }
    }

    /// <summary>
    /// Get an instance of the SiteSettingsManager using the default provider
    /// </summary>
    /// <returns>Instance of SiteSettingsManager</returns>
    public static ContentLocationsManager GetManager() => ManagerBase<ContentLocationsDataProvider>.GetManager<ContentLocationsManager>();

    /// <summary>
    /// Get an instance of the SiteSettingsManager by explicitly specifying the required provider to use
    /// </summary>
    /// <param name="providerName">Name of the provider to use, or null/empty string to use the default provider.</param>
    /// <returns>Instance of the SiteSettingsManager</returns>
    public static ContentLocationsManager GetManager(string providerName) => ManagerBase<ContentLocationsDataProvider>.GetManager<ContentLocationsManager>(providerName);

    /// <summary>
    /// Get an instance of the SiteSettingsManager by explicitly specifying the required provider to use
    /// </summary>
    /// <param name="providerName">Name of the provider to use, or null/empty string to use the default provider.</param>
    /// <param name="transactionName">Name of the transaction.</param>
    /// <returns>Instance of the SiteSettingsManager</returns>
    public static ContentLocationsManager GetManager(
      string providerName,
      string transactionName)
    {
      return ManagerBase<ContentLocationsDataProvider>.GetManager<ContentLocationsManager>(providerName, transactionName);
    }
  }
}
