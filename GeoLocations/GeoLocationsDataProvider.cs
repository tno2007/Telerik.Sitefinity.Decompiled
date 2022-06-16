// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.GeoLocations.GeoLocationsDataProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GeoLocations.Model;

namespace Telerik.Sitefinity.GeoLocations
{
  public abstract class GeoLocationsDataProvider : DataProviderBase
  {
    /// <summary>
    /// Creates a new <see cref="T:Telerik.Sitefinity.GeoLocations.Model.GeoLocation" /> instance.
    /// </summary>
    internal abstract GeoLocation CreateGeoLocation();

    /// <summary>
    /// Creates a new <see cref="T:Telerik.Sitefinity.GeoLocations.Model.GeoLocation" /> with the given id.
    /// </summary>
    /// <param name="settingId">The location id.</param>
    /// <returns>The <see cref="T:Telerik.Sitefinity.GeoLocations.Model.GeoLocation" />.</returns>
    internal abstract GeoLocation CreateGeoLocation(Guid geoLocationId);

    /// <summary>
    /// Marks a <see cref="T:Telerik.Sitefinity.GeoLocations.Model.GeoLocation" /> for removal.
    /// </summary>
    /// <param name="setting">The location to delete.</param>
    internal abstract void Delete(GeoLocation geoLocation);

    /// <summary>
    /// Gets a <see cref="T:Telerik.Sitefinity.GeoLocations.Model.GeoLocation" /> object with the given id.
    /// </summary>
    /// <param name="settingId">The location id.</param>
    internal abstract GeoLocation GetGeoLocation(Guid geoLocationId);

    /// <summary>
    /// Gets an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.GeoLocations.Model.GeoLocation" /> objects.
    /// </summary>
    /// <returns>
    /// Returns an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.GeoLocations.Model.GeoLocation" /> objects.
    /// </returns>
    internal abstract IQueryable<GeoLocation> GetGeoLocations();

    /// <summary>Get a list of types served by this manager</summary>
    /// <returns></returns>
    public override Type[] GetKnownTypes() => new Type[1]
    {
      typeof (GeoLocation)
    };

    /// <summary>Gets a unique key for each data provider base.</summary>
    /// <value></value>
    public override string RootKey => "GeoLocationsDataProvicer";
  }
}
