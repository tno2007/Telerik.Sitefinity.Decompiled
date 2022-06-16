// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Locations.CountryData
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Locations
{
  internal class CountryData
  {
    public string Name { get; set; }

    public string IsoCode { get; set; }

    public string CurrencyName { get; set; }

    public string CurrencyIsoCode { get; set; }

    public int CurrencyIsoNumericCode { get; set; }

    public string Culture { get; set; }

    public string DisplayCurrencyName { get; set; }

    /// <summary>Holds the latitude of the country</summary>
    public double Latitude { get; set; }

    /// <summary>Holds the longitude of the country</summary>
    public double Longitude { get; set; }

    public CountryData(
      string name,
      string isoCode,
      string currencyName,
      string currencyIsoCode,
      int currencyIsoNumericCode,
      string culture,
      string displayCurrencyName,
      double latitude,
      double longitude)
    {
      this.Name = name;
      this.IsoCode = isoCode;
      this.CurrencyName = currencyName;
      this.CurrencyIsoCode = currencyIsoCode;
      this.CurrencyIsoNumericCode = currencyIsoNumericCode;
      this.Culture = culture;
      this.DisplayCurrencyName = displayCurrencyName;
      this.Latitude = latitude;
      this.Longitude = longitude;
    }
  }
}
