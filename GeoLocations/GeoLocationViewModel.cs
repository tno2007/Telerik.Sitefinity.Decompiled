// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.GeoLocations.GeoLocationViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;
using Telerik.Sitefinity.GeoLocations.Model;

namespace Telerik.Sitefinity.GeoLocations
{
  /// <summary>The view model for the GeoLocation type.</summary>
  [DataContract]
  internal class GeoLocationViewModel
  {
    private DateTime lastModified;
    private Guid contentTypeId;
    private Guid id;
    private string locationType;
    private double latitude;
    private double longitude;
    private string customKey;

    /// <summary>Creates a new GeoLocationViewModel.</summary>
    public GeoLocationViewModel()
    {
    }

    /// <summary>
    /// Creates a new GeoLocaitonViewModel from a <see cref="T:Telerik.Sitefinity.GeoLocations.Model.GeoLocation" />.
    /// </summary>
    /// <param name="geoLocation">The <see cref="T:Telerik.Sitefinity.GeoLocations.Model.GeoLocation" />.</param>
    internal GeoLocationViewModel(GeoLocation geoLocation)
    {
      this.customKey = geoLocation.CustomKey;
      this.contentTypeId = geoLocation.ContentItemId;
      this.locationType = geoLocation.ContentType;
      this.latitude = geoLocation.Coordinates.Lat.Value;
      this.longitude = geoLocation.Coordinates.Long.Value;
      this.lastModified = geoLocation.LastModified;
      this.id = geoLocation.Id;
    }

    /// <summary>
    /// Gets or sets the id of the <see cref="T:Telerik.Sitefinity.GeoLocations.Model.GeoLocation" />.
    /// </summary>
    [DataMember]
    public Guid Id
    {
      get => this.id;
      set => this.id = value;
    }

    /// <summary>
    /// Gets or sets the last time the <see cref="T:Telerik.Sitefinity.GeoLocations.Model.GeoLocation" /> was modified.
    /// </summary>
    [DataMember]
    public DateTime LastModified
    {
      get => this.lastModified;
      set => this.lastModified = value;
    }

    /// <summary>
    /// Gets or sets the custom key for the <see cref="T:Telerik.Sitefinity.GeoLocations.Model.GeoLocation" />.
    /// </summary>
    [DataMember]
    public string CustomKey
    {
      get => this.customKey;
      set => this.customKey = value;
    }

    /// <summary>
    /// Gets or sets the location type of the <see cref="T:Telerik.Sitefinity.GeoLocations.Model.GeoLocation" />.
    /// </summary>
    [DataMember]
    public string LocationType
    {
      get => this.locationType;
      set => this.locationType = value;
    }

    /// <summary>
    /// Gets or sets the longitude of the <see cref="T:Telerik.Sitefinity.GeoLocations.Model.GeoLocation" />.
    /// </summary>
    [DataMember]
    public double Longitude
    {
      get => this.longitude;
      set => this.longitude = value;
    }

    /// <summary>
    /// Gets or sets the latitude of the <see cref="T:Telerik.Sitefinity.GeoLocations.Model.GeoLocation" />.
    /// </summary>
    [DataMember]
    public double Latitude
    {
      get => this.latitude;
      set => this.latitude = value;
    }

    /// <summary>
    /// Gets or sets the id of the content type for the <see cref="T:Telerik.Sitefinity.GeoLocations.Model.GeoLocation" />.
    /// </summary>
    [DataMember]
    public Guid ContentTypeId
    {
      get => this.contentTypeId;
      set => this.contentTypeId = value;
    }
  }
}
