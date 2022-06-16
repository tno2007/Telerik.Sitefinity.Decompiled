// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Locations.CountryElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Locations
{
  /// <summary>
  /// Configuration element which holds information about a country.
  /// </summary>
  public class CountryElement : ConfigElement
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public CountryElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Locations.CountryElement" /> class.
    /// </summary>
    internal CountryElement()
      : base(false)
    {
    }

    /// <summary>Gets or sets the ISO-4217 name of the country.</summary>
    [ConfigurationProperty("name")]
    public string Name
    {
      get => (string) this["name"];
      set => this["name"] = (object) value;
    }

    /// <summary>Gets or sets the ISO-4217 code of the country.</summary>
    [DescriptionResource(typeof (ConfigDescriptions), "CountryIsoCodeConfig")]
    [ConfigurationProperty("isoCode", DefaultValue = "", IsKey = true, IsRequired = true)]
    public string IsoCode
    {
      get => (string) this["isoCode"];
      set => this["isoCode"] = (object) value;
    }

    [ConfigurationProperty("culture")]
    public string Culture
    {
      get => (string) this["culture"];
      set => this["culture"] = (object) value;
    }

    [ConfigurationProperty("currencyName")]
    public string CurrencyName
    {
      get => (string) this["currencyName"];
      set => this["currencyName"] = (object) value;
    }

    [ConfigurationProperty("currencyIsoCode")]
    public string CurrencyIsoCode
    {
      get => (string) this["currencyIsoCode"];
      set => this["currencyIsoCode"] = (object) value;
    }

    [ConfigurationProperty("currencyFriendlyName")]
    public string CurrencyFriendlyName
    {
      get => (string) this["currencyFriendlyName"];
      set => this["currencyFriendlyName"] = (object) value;
    }

    [ConfigurationProperty("currencyIsoNumericCode", DefaultValue = 0)]
    public int CurrencyIsoNumericCode
    {
      get => (int) this["currencyIsoNumericCode"];
      set => this["currencyIsoNumericCode"] = (object) value;
    }

    /// <summary>Gets a collection of supported states and provinces.</summary>
    [DescriptionResource(typeof (ConfigDescriptions), "SupportedStatesConfig")]
    [ConfigurationProperty("states")]
    public virtual ConfigElementDictionary<string, StateProvinceElement> StatesProvinces => (ConfigElementDictionary<string, StateProvinceElement>) this["states"];

    [ConfigurationProperty("countryIsActive", DefaultValue = true)]
    public bool CountryIsActive
    {
      get => (bool) this["countryIsActive"];
      set => this["countryIsActive"] = (object) value;
    }

    /// <summary>Gets or sets the country latitude</summary>
    [ConfigurationProperty("latitude", DefaultValue = 0.0)]
    public double Latitude
    {
      get => double.Parse(this["latitude"].ToString());
      set => this["latitude"] = (object) value;
    }

    /// <summary>Gets or sets the country longitude</summary>
    [ConfigurationProperty("longitude", DefaultValue = 0.0)]
    public double Longitude
    {
      get => double.Parse(this["longitude"].ToString());
      set => this["longitude"] = (object) value;
    }
  }
}
