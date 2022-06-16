// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Locations.Configuration.LocationsConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Locations.Utilities;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Locations.Configuration
{
  public class LocationsConfig : ConfigSection
  {
    /// <summary>Gets a collection of supported countries.</summary>
    [DescriptionResource(typeof (ConfigDescriptions), "SupportedCountriesConfig")]
    [ConfigurationProperty("countries")]
    public virtual ConfigElementDictionary<string, CountryElement> Countries => (ConfigElementDictionary<string, CountryElement>) this["countries"];

    /// <summary>
    /// Called after the properties of this instance have been initialized.
    /// Load default values here.
    /// </summary>
    protected override void OnPropertiesInitialized()
    {
      base.OnPropertiesInitialized();
      if (this.Countries.Count == 0)
        this.InstallCountries();
      if (this.Countries["US"] != null)
        this.InstallUSStates();
      if (this.Countries["CA"] == null)
        return;
      this.InstallCanadianStates();
    }

    private void InstallCanadianStates()
    {
      this.Countries["CA"].StatesProvinces.Add("AB", new StateProvinceElement()
      {
        Abbreviation = "AB",
        Name = "Alberta",
        StateIsActive = true,
        Latitude = 55.1667,
        Longitude = -114.4
      });
      this.Countries["CA"].StatesProvinces.Add("BC", new StateProvinceElement()
      {
        Abbreviation = "BC",
        Name = "British Columbia",
        StateIsActive = true,
        Latitude = 54.9,
        Longitude = -124.5
      });
      this.Countries["CA"].StatesProvinces.Add("MB", new StateProvinceElement()
      {
        Abbreviation = "MB",
        Name = "Manitoba",
        StateIsActive = true,
        Latitude = 55.0667,
        Longitude = -97.5167
      });
      this.Countries["CA"].StatesProvinces.Add("NB", new StateProvinceElement()
      {
        Abbreviation = "NB",
        Name = "New Brunswick",
        StateIsActive = true,
        Latitude = 46.5653163,
        Longitude = -66.4619164
      });
      this.Countries["CA"].StatesProvinces.Add("NL", new StateProvinceElement()
      {
        Abbreviation = "NL",
        Name = "Newfoundland and Labrador",
        StateIsActive = true,
        Latitude = 52.6244,
        Longitude = -59.685
      });
      this.Countries["CA"].StatesProvinces.Add("NT", new StateProvinceElement()
      {
        Abbreviation = "NT",
        Name = "Northwest Territories",
        StateIsActive = true,
        Latitude = 64.2667,
        Longitude = -119.1833
      });
      this.Countries["CA"].StatesProvinces.Add("NS", new StateProvinceElement()
      {
        Abbreviation = "NS",
        Name = "Nova Scotia",
        StateIsActive = true,
        Latitude = 44.6819866,
        Longitude = -63.744311
      });
      this.Countries["CA"].StatesProvinces.Add("NU", new StateProvinceElement()
      {
        Abbreviation = "NU",
        Name = "Nunavut",
        StateIsActive = true,
        Latitude = 65.658275,
        Longitude = -96.240234
      });
      this.Countries["CA"].StatesProvinces.Add("ON", new StateProvinceElement()
      {
        Abbreviation = "ON",
        Name = "Ontario",
        StateIsActive = true,
        Latitude = 50.7,
        Longitude = -86.05
      });
      this.Countries["CA"].StatesProvinces.Add("PE", new StateProvinceElement()
      {
        Abbreviation = "PE",
        Name = "Prince Edward Island",
        StateIsActive = true,
        Latitude = 46.3333,
        Longitude = -63.5
      });
      this.Countries["CA"].StatesProvinces.Add("QC", new StateProvinceElement()
      {
        Abbreviation = "QC",
        Name = "Quebec",
        StateIsActive = true,
        Latitude = 46.8902,
        Longitude = -71.2189
      });
      this.Countries["CA"].StatesProvinces.Add("SK", new StateProvinceElement()
      {
        Abbreviation = "SK",
        Name = "Saskatchewan",
        StateIsActive = true,
        Latitude = 55.1167,
        Longitude = -106.05
      });
      this.Countries["CA"].StatesProvinces.Add("YT", new StateProvinceElement()
      {
        Abbreviation = "YT",
        Name = "Yukon",
        StateIsActive = true,
        Latitude = 63.6333,
        Longitude = -135.7667
      });
    }

    private void InstallUSStates()
    {
      this.Countries["US"].StatesProvinces.Add("AL", new StateProvinceElement()
      {
        Abbreviation = "AL",
        Name = "Alabama",
        StateIsActive = true,
        Latitude = 32.799,
        Longitude = -86.8073
      });
      this.Countries["US"].StatesProvinces.Add("AK", new StateProvinceElement()
      {
        Abbreviation = "AK",
        Name = "Alaska",
        StateIsActive = true,
        Latitude = 61.385,
        Longitude = -152.2683
      });
      this.Countries["US"].StatesProvinces.Add("AZ", new StateProvinceElement()
      {
        Abbreviation = "AZ",
        Name = "Arizona",
        StateIsActive = true,
        Latitude = 33.7712,
        Longitude = -111.3877
      });
      this.Countries["US"].StatesProvinces.Add("AR", new StateProvinceElement()
      {
        Abbreviation = "AR",
        Name = "Arkansas",
        StateIsActive = true,
        Latitude = 34.9513,
        Longitude = -92.3809
      });
      this.Countries["US"].StatesProvinces.Add("CA", new StateProvinceElement()
      {
        Abbreviation = "CA",
        Name = "California",
        StateIsActive = true,
        Latitude = 36.17,
        Longitude = -119.7462
      });
      this.Countries["US"].StatesProvinces.Add("CO", new StateProvinceElement()
      {
        Abbreviation = "CO",
        Name = "Colorado",
        StateIsActive = true,
        Latitude = 39.0646,
        Longitude = -105.3272
      });
      this.Countries["US"].StatesProvinces.Add("CT", new StateProvinceElement()
      {
        Abbreviation = "CT",
        Name = "Connecticut",
        StateIsActive = true,
        Latitude = 41.5834,
        Longitude = -72.7622
      });
      this.Countries["US"].StatesProvinces.Add("DE", new StateProvinceElement()
      {
        Abbreviation = "DE",
        Name = "Delaware",
        StateIsActive = true,
        Latitude = 39.3498,
        Longitude = -75.5148
      });
      this.Countries["US"].StatesProvinces.Add("FL", new StateProvinceElement()
      {
        Abbreviation = "FL",
        Name = "Florida",
        StateIsActive = true,
        Latitude = 27.8333,
        Longitude = -81.717
      });
      this.Countries["US"].StatesProvinces.Add("GA", new StateProvinceElement()
      {
        Abbreviation = "GA",
        Name = "Georgia",
        StateIsActive = true,
        Latitude = 32.9866,
        Longitude = -83.6487
      });
      this.Countries["US"].StatesProvinces.Add("HI", new StateProvinceElement()
      {
        Abbreviation = "HI",
        Name = "Hawaii",
        StateIsActive = true,
        Latitude = 21.1098,
        Longitude = -157.5311
      });
      this.Countries["US"].StatesProvinces.Add("ID", new StateProvinceElement()
      {
        Abbreviation = "ID",
        Name = "Idaho",
        StateIsActive = true,
        Latitude = 44.2394,
        Longitude = -114.5103
      });
      this.Countries["US"].StatesProvinces.Add("IL", new StateProvinceElement()
      {
        Abbreviation = "IL",
        Name = "Illinois",
        StateIsActive = true,
        Latitude = 40.3363,
        Longitude = -89.0022
      });
      this.Countries["US"].StatesProvinces.Add("IN", new StateProvinceElement()
      {
        Abbreviation = "IN",
        Name = "Indiana",
        StateIsActive = true,
        Latitude = 39.8647,
        Longitude = -86.2604
      });
      this.Countries["US"].StatesProvinces.Add("IA", new StateProvinceElement()
      {
        Abbreviation = "IA",
        Name = "Iowa",
        StateIsActive = true,
        Latitude = 42.0046,
        Longitude = -93.214
      });
      this.Countries["US"].StatesProvinces.Add("KS", new StateProvinceElement()
      {
        Abbreviation = "KS",
        Name = "Kansas",
        StateIsActive = true,
        Latitude = 38.5111,
        Longitude = -96.8005
      });
      this.Countries["US"].StatesProvinces.Add("KY", new StateProvinceElement()
      {
        Abbreviation = "KY",
        Name = "Kentucky",
        StateIsActive = true,
        Latitude = 37.669,
        Longitude = -84.6514
      });
      this.Countries["US"].StatesProvinces.Add("LA", new StateProvinceElement()
      {
        Abbreviation = "LA",
        Name = "Louisiana",
        StateIsActive = true,
        Latitude = 31.1801,
        Longitude = -91.8749
      });
      this.Countries["US"].StatesProvinces.Add("ME", new StateProvinceElement()
      {
        Abbreviation = "ME",
        Name = "Maine",
        StateIsActive = true,
        Latitude = 44.6074,
        Longitude = -69.3977
      });
      this.Countries["US"].StatesProvinces.Add("MD", new StateProvinceElement()
      {
        Abbreviation = "MD",
        Name = "Maryland",
        StateIsActive = true,
        Latitude = 39.0724,
        Longitude = -76.7902
      });
      this.Countries["US"].StatesProvinces.Add("MA", new StateProvinceElement()
      {
        Abbreviation = "MA",
        Name = "Massachusetts",
        StateIsActive = true,
        Latitude = 42.2373,
        Longitude = -71.5314
      });
      this.Countries["US"].StatesProvinces.Add("MI", new StateProvinceElement()
      {
        Abbreviation = "MI",
        Name = "Michigan",
        StateIsActive = true,
        Latitude = 43.3504,
        Longitude = -84.5603
      });
      this.Countries["US"].StatesProvinces.Add("MN", new StateProvinceElement()
      {
        Abbreviation = "MN",
        Name = "Minnesota",
        StateIsActive = true,
        Latitude = 45.7326,
        Longitude = -93.9196
      });
      this.Countries["US"].StatesProvinces.Add("MS", new StateProvinceElement()
      {
        Abbreviation = "MS",
        Name = "Mississippi",
        StateIsActive = true,
        Latitude = 32.7673,
        Longitude = -89.6812
      });
      this.Countries["US"].StatesProvinces.Add("MO", new StateProvinceElement()
      {
        Abbreviation = "MO",
        Name = "Missouri",
        StateIsActive = true,
        Latitude = 38.4623,
        Longitude = -92.302
      });
      this.Countries["US"].StatesProvinces.Add("MT", new StateProvinceElement()
      {
        Abbreviation = "MT",
        Name = "Montana",
        StateIsActive = true,
        Latitude = 46.9048,
        Longitude = -110.3261
      });
      this.Countries["US"].StatesProvinces.Add("NE", new StateProvinceElement()
      {
        Abbreviation = "NE",
        Name = "Nebraska",
        StateIsActive = true,
        Latitude = 41.1289,
        Longitude = -98.2883
      });
      this.Countries["US"].StatesProvinces.Add("NV", new StateProvinceElement()
      {
        Abbreviation = "NV",
        Name = "Nevada",
        StateIsActive = true,
        Latitude = 38.4199,
        Longitude = -117.1219
      });
      this.Countries["US"].StatesProvinces.Add("NH", new StateProvinceElement()
      {
        Abbreviation = "NH",
        Name = "New Hampshire",
        StateIsActive = true,
        Latitude = 43.4108,
        Longitude = -71.5653
      });
      this.Countries["US"].StatesProvinces.Add("NJ", new StateProvinceElement()
      {
        Abbreviation = "NJ",
        Name = "New Jersey",
        StateIsActive = true,
        Latitude = 40.314,
        Longitude = -74.5089
      });
      this.Countries["US"].StatesProvinces.Add("NM", new StateProvinceElement()
      {
        Abbreviation = "NM",
        Name = "New Mexico",
        StateIsActive = true,
        Latitude = 2787.0 / 80.0,
        Longitude = -106.2371
      });
      this.Countries["US"].StatesProvinces.Add("NY", new StateProvinceElement()
      {
        Abbreviation = "NY",
        Name = "New York",
        StateIsActive = true,
        Latitude = 42.1497,
        Longitude = -74.9384
      });
      this.Countries["US"].StatesProvinces.Add("NC", new StateProvinceElement()
      {
        Abbreviation = "NC",
        Name = "North Carolina",
        StateIsActive = true,
        Latitude = 35.6411,
        Longitude = -79.8431
      });
      this.Countries["US"].StatesProvinces.Add("ND", new StateProvinceElement()
      {
        Abbreviation = "ND",
        Name = "North Dakota",
        StateIsActive = true,
        Latitude = 47.5362,
        Longitude = -99.793
      });
      this.Countries["US"].StatesProvinces.Add("OH", new StateProvinceElement()
      {
        Abbreviation = "OH",
        Name = "Ohio",
        StateIsActive = true,
        Latitude = 40.3736,
        Longitude = -82.7755
      });
      this.Countries["US"].StatesProvinces.Add("OK", new StateProvinceElement()
      {
        Abbreviation = "OK",
        Name = "Oklahoma",
        StateIsActive = true,
        Latitude = 35.5376,
        Longitude = -96.9247
      });
      this.Countries["US"].StatesProvinces.Add("OR", new StateProvinceElement()
      {
        Abbreviation = "OR",
        Name = "Oregon",
        StateIsActive = true,
        Latitude = 44.5672,
        Longitude = -122.1269
      });
      this.Countries["US"].StatesProvinces.Add("PA", new StateProvinceElement()
      {
        Abbreviation = "PA",
        Name = "Pennsylvania",
        StateIsActive = true,
        Latitude = 40.5773,
        Longitude = -77.264
      });
      this.Countries["US"].StatesProvinces.Add("RI", new StateProvinceElement()
      {
        Abbreviation = "RI",
        Name = "Rhode Island",
        StateIsActive = true,
        Latitude = 41.6772,
        Longitude = -71.5101
      });
      this.Countries["US"].StatesProvinces.Add("SC", new StateProvinceElement()
      {
        Abbreviation = "SC",
        Name = "South Carolina",
        StateIsActive = true,
        Latitude = 33.8191,
        Longitude = -80.9066
      });
      this.Countries["US"].StatesProvinces.Add("SD", new StateProvinceElement()
      {
        Abbreviation = "SD",
        Name = "South Dakota",
        StateIsActive = true,
        Latitude = 44.2853,
        Longitude = -99.4632
      });
      this.Countries["US"].StatesProvinces.Add("TN", new StateProvinceElement()
      {
        Abbreviation = "TN",
        Name = "Tennessee",
        StateIsActive = true,
        Latitude = 35.7449,
        Longitude = -86.7489
      });
      this.Countries["US"].StatesProvinces.Add("TX", new StateProvinceElement()
      {
        Abbreviation = "TX",
        Name = "Texas",
        StateIsActive = true,
        Latitude = 31.106,
        Longitude = -97.6475
      });
      this.Countries["US"].StatesProvinces.Add("UT", new StateProvinceElement()
      {
        Abbreviation = "UT",
        Name = "Utah",
        StateIsActive = true,
        Latitude = 40.1135,
        Longitude = -111.8535
      });
      this.Countries["US"].StatesProvinces.Add("VT", new StateProvinceElement()
      {
        Abbreviation = "VT",
        Name = "Vermont",
        StateIsActive = true,
        Latitude = 44.0407,
        Longitude = -72.7093
      });
      this.Countries["US"].StatesProvinces.Add("VA", new StateProvinceElement()
      {
        Abbreviation = "VA",
        Name = "Virginia",
        StateIsActive = true,
        Latitude = 37.768,
        Longitude = -78.2057
      });
      this.Countries["US"].StatesProvinces.Add("WA", new StateProvinceElement()
      {
        Abbreviation = "WA",
        Name = "Washington",
        StateIsActive = true,
        Latitude = 47.3917,
        Longitude = -121.5708
      });
      this.Countries["US"].StatesProvinces.Add("WV", new StateProvinceElement()
      {
        Abbreviation = "WV",
        Name = "West Virginia",
        StateIsActive = true,
        Latitude = 38.468,
        Longitude = -80.9696
      });
      this.Countries["US"].StatesProvinces.Add("WI", new StateProvinceElement()
      {
        Abbreviation = "WI",
        Name = "Wisconsin",
        StateIsActive = true,
        Latitude = 44.256,
        Longitude = -89.6385
      });
      this.Countries["US"].StatesProvinces.Add("WY", new StateProvinceElement()
      {
        Abbreviation = "WY",
        Name = "Wyoming",
        StateIsActive = true,
        Latitude = 42.7475,
        Longitude = -107.2085
      });
    }

    private void InstallCountries()
    {
      foreach (CountryData countryData in CountryRetriever.GetCountryData())
        this.Countries.Add(new CountryElement()
        {
          Name = countryData.Name,
          IsoCode = countryData.IsoCode,
          CurrencyName = countryData.CurrencyName,
          CurrencyIsoCode = countryData.CurrencyIsoCode,
          CurrencyIsoNumericCode = countryData.CurrencyIsoNumericCode,
          Culture = countryData.Culture,
          CurrencyFriendlyName = countryData.DisplayCurrencyName,
          CountryIsActive = true,
          Latitude = countryData.Latitude,
          Longitude = countryData.Longitude
        });
    }
  }
}
