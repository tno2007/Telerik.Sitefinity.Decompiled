// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.GeoLocations.GeoLocationsHelper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Microsoft.SqlServer.Types;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.GeoLocations.Model;

namespace Telerik.Sitefinity.GeoLocations
{
  internal class GeoLocationsHelper
  {
    /// <summary>Filters the geo location items.</summary>
    /// <param name="geoLocations">The geo locations.</param>
    /// <param name="pointCoordinates">The point coordinates.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="locationType">Type of the location.</param>
    /// <param name="radius">The radius.</param>
    /// <param name="customKey">The custom key.</param>
    internal static IQueryable<GeoLocation> FilterGeoLocationItems(
      IQueryable<GeoLocation> geoLocations,
      SqlGeography pointCoordinates,
      double radius,
      ItemFilter itemFilter = null)
    {
      if (itemFilter == null)
        itemFilter = new ItemFilter();
      if (!string.IsNullOrEmpty(itemFilter.ProviderName))
        geoLocations = geoLocations.Where<GeoLocation>((Expression<Func<GeoLocation, bool>>) (g => g.ProviderName == itemFilter.ProviderName));
      if (!string.IsNullOrEmpty(itemFilter.ContentType))
        geoLocations = geoLocations.Where<GeoLocation>((Expression<Func<GeoLocation, bool>>) (g => g.ContentType == itemFilter.ContentType));
      if (!string.IsNullOrEmpty(itemFilter.CustomKey))
        geoLocations = geoLocations.Where<GeoLocation>((Expression<Func<GeoLocation, bool>>) (g => g.CustomKey == itemFilter.CustomKey));
      geoLocations = geoLocations.Where<GeoLocation>((Expression<Func<GeoLocation, bool>>) (g => g.Coordinates.STDistance(pointCoordinates).Value <= radius));
      return geoLocations;
    }

    /// <summary>Populates the distance of the dynamic items.</summary>
    /// <param name="items">The items.</param>
    /// <param name="geoLocations">The geo locations.</param>
    /// <param name="latitude">The latitude.</param>
    /// <param name="longitude">The longitude.</param>
    internal static void PopulateDistance<T>(
      IEnumerable<T> items,
      IEnumerable<IGeoLocation> geoLocations,
      double latitude,
      double longitude)
      where T : IGeoLocationDistance
    {
      SqlGeography other = SqlGeography.Point(latitude, longitude, 4326);
      foreach (T obj in items)
      {
        T item = obj;
        IGeoLocation geoLocation = geoLocations.FirstOrDefault<IGeoLocation>((Func<IGeoLocation, bool>) (gl => gl.ContentItemId == item.Id));
        if (geoLocations != null)
        {
          SqlGeography sqlGeography = SqlGeography.Point(geoLocation.Latitude, geoLocation.Longitude, 4326);
          item.Distance = sqlGeography.STDistance(other).Value / 1000.0;
        }
      }
    }

    /// <summary>Sorts the geo location items.</summary>
    /// <param name="geoLocations">The geo locations.</param>
    /// <param name="sorting">The sorting.</param>
    /// <param name="latitude">The latitude.</param>
    /// <param name="longitude">The longitude.</param>
    internal static IQueryable<GeoLocation> SortGeoLocationItems(
      IQueryable<GeoLocation> geoLocations,
      DistanceSorting sorting,
      double latitude,
      double longitude)
    {
      SqlGeography coordinates = SqlGeography.Point(latitude, longitude, 4326);
      return GeoLocationsHelper.SortGeoLocationItems(geoLocations, sorting, coordinates);
    }

    /// <summary>Sorts the geo location items.</summary>
    /// <param name="geoLocations">The geo locations.</param>
    /// <param name="sorting">The sorting.</param>
    /// <param name="coordinates">The coordinates.</param>
    internal static IQueryable<GeoLocation> SortGeoLocationItems(
      IQueryable<GeoLocation> geoLocations,
      DistanceSorting sorting,
      SqlGeography coordinates)
    {
      if (sorting == DistanceSorting.Desc)
        geoLocations = (IQueryable<GeoLocation>) geoLocations.OrderByDescending<GeoLocation, SqlDouble>((Expression<Func<GeoLocation, SqlDouble>>) (g => g.Coordinates.STDistance(coordinates)));
      else
        geoLocations = (IQueryable<GeoLocation>) geoLocations.OrderBy<GeoLocation, SqlDouble>((Expression<Func<GeoLocation, SqlDouble>>) (g => g.Coordinates.STDistance(coordinates)));
      return geoLocations;
    }
  }
}
