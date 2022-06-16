// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.GeoLocations.GeoLocationService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Microsoft.SqlServer.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.GeoLocations.Model;

namespace Telerik.Sitefinity.GeoLocations
{
  /// <inheritdoc />
  public class GeoLocationService : IGeoLocationService
  {
    /// <inheritdoc />
    IEnumerable<IGeoLocation> IGeoLocationService.GetLocationsInCircle(
      double latitude,
      double longitude,
      double radius,
      DistanceSorting sorting = DistanceSorting.Asc,
      ItemFilter itemFilter = null)
    {
      GeoLocationsManager manager = GeoLocationsManager.GetManager();
      SqlGeography sqlGeography = SqlGeography.Point(latitude, longitude, 4326);
      double radius1 = 1000.0 * radius;
      return (IEnumerable<IGeoLocation>) Queryable.Cast<IGeoLocation>(GeoLocationsHelper.SortGeoLocationItems(GeoLocationsHelper.FilterGeoLocationItems(manager.GetGeoLocations(), sqlGeography, radius1, itemFilter), sorting, sqlGeography));
    }

    /// <inheritdoc />
    public void UpdateLocation(
      Guid id,
      string contentType,
      string providerName,
      string customKey,
      Guid contentItemId,
      double latitude,
      double longitude)
    {
      this.CheckParameters(contentType, customKey, latitude, longitude);
      GeoLocationsManager manager = GeoLocationsManager.GetManager();
      GeoLocation geoLocation = !(id == Guid.Empty) ? manager.GetGeoLocation(id) : manager.CreateGeoLocation();
      geoLocation.ContentType = contentType;
      geoLocation.ProviderName = providerName;
      geoLocation.ContentItemId = contentItemId;
      geoLocation.CustomKey = customKey;
      geoLocation.Coordinates = SqlGeography.Point(latitude, longitude, 4326);
      manager.SaveChanges();
    }

    /// <inheritdoc />
    public void DeleteLocation(Guid id)
    {
      GeoLocationsManager manager = GeoLocationsManager.GetManager();
      manager.Delete(manager.GetGeoLocation(id));
      manager.SaveChanges();
    }

    /// <inheritdoc />
    public IGeoLocation GetLocation(
      Guid itemId,
      string contentType,
      string customKey,
      string providerName)
    {
      return (IGeoLocation) GeoLocationsManager.GetManager().GetGeoLocations().Where<GeoLocation>((Expression<Func<GeoLocation, bool>>) (x => x.ContentType == contentType && x.ContentItemId == itemId && x.CustomKey == customKey && x.ProviderName == providerName)).FirstOrDefault<GeoLocation>();
    }

    private void CheckParameters(
      string locationType,
      string customKey,
      double latitude,
      double longitude)
    {
      if (string.IsNullOrEmpty(locationType))
        throw new ArgumentNullException(nameof (locationType), "Value cannot be null");
      if (string.IsNullOrEmpty(customKey))
        throw new ArgumentNullException(nameof (customKey), "Value cannot be null");
      SqlGeography.Point(latitude, longitude, 4326);
    }
  }
}
