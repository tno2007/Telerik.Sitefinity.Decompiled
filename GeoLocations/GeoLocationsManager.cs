// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.GeoLocations.GeoLocationsManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GeoLocations.Configuration;
using Telerik.Sitefinity.GeoLocations.Model;

namespace Telerik.Sitefinity.GeoLocations
{
  internal class GeoLocationsManager : ManagerBase<GeoLocationsDataProvider>
  {
    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.GeoLocations.GeoLocationsManager" />
    /// </summary>
    public GeoLocationsManager()
      : this((string) null)
    {
    }

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.GeoLocations.GeoLocationsManager" />
    /// </summary>
    /// <param name="providerName">The name of the provider.</param>
    public GeoLocationsManager(string providerName)
      : base(providerName)
    {
    }

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.GeoLocations.GeoLocationsManager" />
    /// </summary>
    /// <param name="providerName">The name of the provider.</param>
    /// <param name="transactionName">The name of the transaction.</param>
    public GeoLocationsManager(string providerName, string transactionName)
      : base(providerName, transactionName)
    {
    }

    /// <summary>
    /// Creates a new <see cref="T:Telerik.Sitefinity.GeoLocations.Model.GeoLocation" />
    /// </summary>
    /// <returns></returns>
    internal GeoLocation CreateGeoLocation() => this.Provider.CreateGeoLocation();

    /// <summary>
    /// Creates a new <see cref="T:Telerik.Sitefinity.GeoLocations.Model.GeoLocation" />
    /// </summary>
    /// <param name="geoLocationId">The id of the <see cref="T:Telerik.Sitefinity.GeoLocations.Model.GeoLocation" />.</param>
    /// <returns></returns>
    internal GeoLocation CreateGeoLocation(Guid geoLocationId) => this.provider.CreateGeoLocation(geoLocationId);

    /// <summary>
    /// Deletes a <see cref="T:Telerik.Sitefinity.GeoLocations.Model.GeoLocation" />.
    /// </summary>
    /// <param name="geoLocation">The <see cref="T:Telerik.Sitefinity.GeoLocations.Model.GeoLocation" /> to be deleted.</param>
    internal void Delete(GeoLocation geoLocation) => this.provider.Delete(geoLocation);

    /// <summary>
    /// Gets a <see cref="T:Telerik.Sitefinity.GeoLocations.Model.GeoLocation" />.
    /// </summary>
    /// <param name="geoLocationId">The id of the <see cref="T:Telerik.Sitefinity.GeoLocations.Model.GeoLocation" /> to get.</param>
    /// <returns></returns>
    internal GeoLocation GetGeoLocation(Guid geoLocationId) => this.provider.GetGeoLocation(geoLocationId);

    /// <summary>
    /// Gets all of the <see cref="T:Telerik.Sitefinity.GeoLocations.Model.GeoLocation" /> items.
    /// </summary>
    /// <returns>The <see cref="T:Telerik.Sitefinity.GeoLocations.Model.GeoLocation" /> items.</returns>
    internal IQueryable<GeoLocation> GetGeoLocations() => this.provider.GetGeoLocations();

    protected internal override GetDefaultProvider DefaultProviderDelegate => (GetDefaultProvider) (() => Config.Get<GeoLocationsConfig>().DefaultProvider);

    /// <summary>Gets the name of the module.</summary>
    public override string ModuleName => string.Empty;

    protected internal override ConfigElementDictionary<string, DataProviderSettings> ProvidersSettings => Config.Get<GeoLocationsConfig>().Providers;

    /// <summary>
    /// Get an instance of the GeoLocationsManager using the default provider
    /// </summary>
    /// <returns>Instance of GeoLocationsManager</returns>
    public static GeoLocationsManager GetManager() => ManagerBase<GeoLocationsDataProvider>.GetManager<GeoLocationsManager>();

    /// <summary>
    /// Get an instance of the GeoLocationsManager by explicitly specifying the required provider to use
    /// </summary>
    /// <param name="providerName">Name of the provider to use, or null/empty string to use the default provider.</param>
    /// <returns>Instance of the GeoLocationsManager</returns>
    public static GeoLocationsManager GetManager(string providerName) => ManagerBase<GeoLocationsDataProvider>.GetManager<GeoLocationsManager>(providerName);

    /// <summary>
    /// Get an instance of the GeoLocationsManager by explicitly specifying the required provider to use
    /// </summary>
    /// <param name="providerName">Name of the provider to use, or null/empty string to use the default provider.</param>
    /// <param name="transactionName">Name of the transaction.</param>
    /// <returns>Instance of the GeoLocationsManager</returns>
    public static GeoLocationsManager GetManager(
      string providerName,
      string transactionName)
    {
      return ManagerBase<GeoLocationsDataProvider>.GetManager<GeoLocationsManager>(providerName, transactionName);
    }
  }
}
