// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Locations.Utilities.LocationRetriever
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Locations.Configuration;

namespace Telerik.Sitefinity.Locations.Utilities
{
  /// <summary>
  /// Class responsible for retrieving country and state (or province) information from the Locations configuration.
  /// </summary>
  public class LocationRetriever
  {
    /// <summary>
    /// Gets the all the active countries in the configuration.
    /// </summary>
    /// <returns>a list of active countries from the Locations configuration.</returns>
    public IList<CountryElement> GetCountries() => (IList<CountryElement>) Config.Get<LocationsConfig>().Countries.Values.Where<CountryElement>((Func<CountryElement, bool>) (x => x.CountryIsActive)).OrderBy<CountryElement, string>((Func<CountryElement, string>) (x => x.Name)).ToList<CountryElement>();

    /// <summary>Gets the country for the specified country ISO code.</summary>
    /// <param name="countryIsoCode">The country iso code.</param>
    /// <returns>the country for the specified country ISO code; otherwise, if no country with the ISO code exists, then it returns null</returns>
    public CountryElement GetCountry(string countryIsoCode) => Config.Get<LocationsConfig>().Countries.Values.Where<CountryElement>((Func<CountryElement, bool>) (cd => cd.IsoCode == countryIsoCode && cd.CountryIsActive)).FirstOrDefault<CountryElement>();

    /// <summary>
    /// Gets the states and provinces for the specified country ISO code.
    /// </summary>
    /// <param name="countryIsoCode">The country iso code.</param>
    /// <returns>a list of states (or provinces) that have been configured for the specified country.</returns>
    public IList<StateProvinceElement> GetStatesAndProvinces(
      string countryIsoCode)
    {
      CountryElement country = this.GetCountry(countryIsoCode);
      return country == null ? (IList<StateProvinceElement>) new List<StateProvinceElement>() : (IList<StateProvinceElement>) country.StatesProvinces.Values.ToList<StateProvinceElement>();
    }
  }
}
