// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.GeoLocations.Data.OpenAccessGeoLocationsProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using Telerik.OpenAccess.Metadata;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.GeoLocations.Model;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.GeoLocations.Data
{
  public class OpenAccessGeoLocationsProvider : 
    GeoLocationsDataProvider,
    IOpenAccessDataProvider,
    IDataProviderBase,
    IDisposable,
    ICloneable,
    IDataProviderEventsCall,
    IOpenAccessMetadataProvider
  {
    /// <inheritdoc />
    internal override GeoLocation CreateGeoLocation() => this.CreateGeoLocation(this.GetNewGuid());

    /// <inheritdoc />
    internal override GeoLocation CreateGeoLocation(Guid geoLocationId)
    {
      GeoLocation entity = new GeoLocation(this.ApplicationName, geoLocationId);
      entity.Provider = (object) this;
      if (geoLocationId != Guid.Empty)
        this.GetContext().Add((object) entity);
      return entity;
    }

    /// <inheritdoc />
    internal override void Delete(GeoLocation geoLocation) => this.GetContext().Remove((object) geoLocation);

    /// <inheritdoc />
    internal override GeoLocation GetGeoLocation(Guid geoLocationId)
    {
      GeoLocation itemById = this.GetContext().GetItemById<GeoLocation>(geoLocationId.ToString());
      itemById.Provider = (object) this;
      return itemById;
    }

    /// <inheritdoc />
    internal override IQueryable<GeoLocation> GetGeoLocations()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<GeoLocation>((DataProviderBase) this).Where<GeoLocation>((Expression<Func<GeoLocation, bool>>) (c => c.ApplicationName == appName));
    }

    /// <summary>
    /// Gets or sets the OpenAccess context. Alternative to Database.
    /// </summary>
    /// <value>The context.</value>
    public OpenAccessProviderContext Context { get; set; }

    public MetadataSource GetMetaDataSource(IDatabaseMappingContext context) => (MetadataSource) new GeoLocationsMetadataSource(context);
  }
}
