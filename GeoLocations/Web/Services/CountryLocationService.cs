// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.GeoLocations.Web.Services.CountryLocationService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Locations;
using Telerik.Sitefinity.Locations.Configuration;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.GeoLocations.Web.Services
{
  /// <summary>
  /// The WCF Service that is used for retrieving country locations
  /// </summary>
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  internal class CountryLocationService : ICountryLocationService
  {
    /// <summary>
    ///     <para>The web service relative URL of the CountryLocation Service.</para>
    /// </summary>
    internal const string WebServiceUrl = "Sitefinity/Services/CountryLocationService";

    /// <inheritdoc />
    public SimpleLocationViewModel GetCountryLocation(string isoCode)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      return this.GetCountryLocationInternal(isoCode);
    }

    /// <inheritdoc />
    public SimpleLocationViewModel GetCountryLocationXml(string isoCode)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      return this.GetCountryLocationInternal(isoCode);
    }

    /// <inheritdoc />
    public SimpleLocationViewModel GetStateLocation(
      string countryIsoCode,
      string stateIsoCode)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      return this.GetStateLocationInternal(countryIsoCode, stateIsoCode);
    }

    /// <inheritdoc />
    public SimpleLocationViewModel GetStateLocationXml(
      string countryIsoCode,
      string stateIsoCode)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      return this.GetStateLocationInternal(countryIsoCode, stateIsoCode);
    }

    private SimpleLocationViewModel GetCountryLocationInternal(string isoCode)
    {
      if (isoCode == null)
        throw new ArgumentNullException("isoCode cannot be null");
      CountryElement countryElement = Config.Get<LocationsConfig>().Countries.Values.Where<CountryElement>((Func<CountryElement, bool>) (x => x.IsoCode == isoCode)).FirstOrDefault<CountryElement>();
      SimpleLocationViewModel locationInternal = new SimpleLocationViewModel();
      if (countryElement != null)
      {
        locationInternal.Latitude = countryElement.Latitude;
        locationInternal.Longitude = countryElement.Longitude;
      }
      return locationInternal;
    }

    private SimpleLocationViewModel GetStateLocationInternal(
      string countryIsoCode,
      string stateIsoCode)
    {
      if (countryIsoCode == null)
        throw new ArgumentNullException("countryIsoCode cannot be null");
      if (stateIsoCode == null)
        throw new ArgumentNullException("stateIsoCode cannot be null");
      CountryElement countryElement = Config.Get<LocationsConfig>().Countries.Values.Where<CountryElement>((Func<CountryElement, bool>) (x => x.IsoCode == countryIsoCode)).FirstOrDefault<CountryElement>();
      SimpleLocationViewModel locationInternal = new SimpleLocationViewModel();
      if (countryElement != null)
      {
        StateProvinceElement statesProvince = countryElement.StatesProvinces[stateIsoCode];
        if (statesProvince != null)
        {
          locationInternal.Latitude = statesProvince.Latitude;
          locationInternal.Longitude = statesProvince.Longitude;
        }
      }
      return locationInternal;
    }
  }
}
