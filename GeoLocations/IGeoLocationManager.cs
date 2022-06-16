// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.GeoLocations.IGeoLocationManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.GeoLocations.Model;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.GeoLocations
{
  public interface IGeoLocationManager
  {
    /// <summary>Filters the items by geo location.</summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="itemsList">The items list.</param>
    /// <param name="latitude">The latitude.</param>
    /// <param name="longitude">The longitude.</param>
    /// <param name="radius">The radius.</param>
    /// <param name="geoLocationsList">The geo locations list.</param>
    /// <param name="itemFilter">The item filter.</param>
    IQueryable<T> FilterByGeoLocation<T>(
      IQueryable<T> itemsList,
      double latitude,
      double longitude,
      double radius,
      out IEnumerable<IGeoLocation> geoLocationsList,
      ItemFilter itemFilter = null)
      where T : IDataItem;

    /// <summary>
    /// Sorts the items by distance to a specified point, using the specified geo location points
    /// </summary>
    /// <param name="itemsList">The items list.</param>
    /// <param name="geoLocationsList">The geo locations list.</param>
    /// <param name="latitude">The latitude.</param>
    /// <param name="longitude">The longitude.</param>
    /// <param name="distanceSorting">The distance sorting.</param>
    IEnumerable<T> SortByDistance<T>(
      IEnumerable<T> itemsList,
      IEnumerable<IGeoLocation> geoLocationsList,
      double latitude,
      double longitude,
      DistanceSorting distanceSorting)
      where T : IGeoLocationDistance;
  }
}
