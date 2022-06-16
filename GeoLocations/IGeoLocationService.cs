// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.GeoLocations.IGeoLocationService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.GeoLocations.Model;

namespace Telerik.Sitefinity.GeoLocations
{
  /// <summary>Defines functions for resolving geo locations items.</summary>
  public interface IGeoLocationService
  {
    /// <summary>Gets the locations in circle.</summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="contentType">Type of the content.</param>
    /// <param name="latitude">The latitude.</param>
    /// <param name="longitude">The longitude.</param>
    /// <param name="radius">The radius.</param>
    /// <param name="sortExpression">The sort expression.</param>
    IEnumerable<IGeoLocation> GetLocationsInCircle(
      double latitude,
      double longitude,
      double radius,
      DistanceSorting sorting = DistanceSorting.Asc,
      ItemFilter itemFilter = null);

    /// <summary>Gets the location.</summary>
    /// <param name="itemId">The item id.</param>
    /// <param name="contentType">Type of the content.</param>
    /// <param name="customKey">The custom key.</param>
    /// <param name="providerName">Name of the provider.</param>
    IGeoLocation GetLocation(
      Guid itemId,
      string contentType,
      string customKey,
      string providerName);

    /// <summary>Creates or updates an existing GeoLocation.</summary>
    /// <param name="id">The id of the GeoLocation. If the passed id is an empty GUID, than a new GeoLocation is created</param>
    /// <param name="contentType">The full name of the content type.</param>
    /// <param name="providerName">The name of the provider.</param>
    /// <param name="customKey">The custom key which is associated with the GeoLocation.</param>
    /// <param name="contentItemId">The id of the content item.</param>
    /// <param name="latitude">The latitude of the point.</param>
    /// <param name="longitude">The longitude of the point.</param>
    void UpdateLocation(
      Guid id,
      string contentType,
      string providerName,
      string customKey,
      Guid contentItemId,
      double latitude,
      double longitude);

    /// <summary>Deletes the location.</summary>
    /// <param name="id">The id.</param>
    /// <param name="providerName">Name of the provider.</param>
    void DeleteLocation(Guid id);
  }
}
