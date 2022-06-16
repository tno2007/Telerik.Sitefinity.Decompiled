﻿// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Locations.Utilities.CountryRetriever
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
  internal class CountryRetriever
  {
    public static List<CountryData> GetCountryData() => new List<CountryData>()
    {
      CountryInfo.Afghanistan,
      CountryInfo.Albania,
      CountryInfo.Algeria,
      CountryInfo.AmericanSamoa,
      CountryInfo.Andorra,
      CountryInfo.Angola,
      CountryInfo.Anguilla,
      CountryInfo.AntiguaBarbuda,
      CountryInfo.Argentina,
      CountryInfo.Armenia,
      CountryInfo.Aruba,
      CountryInfo.Australia,
      CountryInfo.Austria,
      CountryInfo.Azerbaijan,
      CountryInfo.Azores,
      CountryInfo.Bahamas,
      CountryInfo.Bahrain,
      CountryInfo.Bangladesh,
      CountryInfo.Barbados,
      CountryInfo.Belgium,
      CountryInfo.Belize,
      CountryInfo.Belarus,
      CountryInfo.Benin,
      CountryInfo.Bermuda,
      CountryInfo.Bhutan,
      CountryInfo.Bolivia,
      CountryInfo.Bonaire,
      CountryInfo.BosniaHerzegovina,
      CountryInfo.Botswana,
      CountryInfo.Brazil,
      CountryInfo.BritishVirginIslands,
      CountryInfo.Brunei,
      CountryInfo.Bulgaria,
      CountryInfo.BurkinaFaso,
      CountryInfo.Burundi,
      CountryInfo.Cambodia,
      CountryInfo.Cameroon,
      CountryInfo.Canada,
      CountryInfo.CanaryIslands,
      CountryInfo.CapeVerdeIslands,
      CountryInfo.CaymanIslands,
      CountryInfo.CentralAfricanRepublic,
      CountryInfo.Chad,
      CountryInfo.ChannelIslands,
      CountryInfo.Chile,
      CountryInfo.China,
      CountryInfo.ChristmasIsland,
      CountryInfo.CocosIslands,
      CountryInfo.Colombia,
      CountryInfo.Comoros,
      CountryInfo.Congo,
      CountryInfo.CookIslands,
      CountryInfo.CostaRica,
      CountryInfo.Croatia,
      CountryInfo.Cuba,
      CountryInfo.Curacao,
      CountryInfo.Cyprus,
      CountryInfo.CzechRepublic,
      CountryInfo.DemocraticRepublicOfCongo,
      CountryInfo.Denmark,
      CountryInfo.Djibouti,
      CountryInfo.Dominica,
      CountryInfo.DominicanRepublic,
      CountryInfo.Ecuador,
      CountryInfo.Egypt,
      CountryInfo.ElSalvador,
      CountryInfo.England,
      CountryInfo.EquitorialGuinea,
      CountryInfo.Eritrea,
      CountryInfo.Estonia,
      CountryInfo.Ethiopia,
      CountryInfo.FaeroeIslands,
      CountryInfo.FalklandIslands,
      CountryInfo.FederatedStatesofMicronesia,
      CountryInfo.Fiji,
      CountryInfo.Finland,
      CountryInfo.France,
      CountryInfo.FrenchGuiana,
      CountryInfo.FrenchPolynesia,
      CountryInfo.Gabon,
      CountryInfo.Gambia,
      CountryInfo.Georgia,
      CountryInfo.Germany,
      CountryInfo.Ghana,
      CountryInfo.Gibraltar,
      CountryInfo.Greece,
      CountryInfo.Greenland,
      CountryInfo.Grenada,
      CountryInfo.Guadeloupe,
      CountryInfo.Guam,
      CountryInfo.Guatemala,
      CountryInfo.Guinea,
      CountryInfo.GuineaBissau,
      CountryInfo.Guyana,
      CountryInfo.Haiti,
      CountryInfo.Honduras,
      CountryInfo.HongKong,
      CountryInfo.Hungary,
      CountryInfo.Iceland,
      CountryInfo.India,
      CountryInfo.Indonesia,
      CountryInfo.Iran,
      CountryInfo.Iraq,
      CountryInfo.Ireland,
      CountryInfo.Israel,
      CountryInfo.Italy,
      CountryInfo.IvoryCoast,
      CountryInfo.Jamaica,
      CountryInfo.Japan,
      CountryInfo.Jordan,
      CountryInfo.Kazakhstan,
      CountryInfo.Kenya,
      CountryInfo.Kiribati,
      CountryInfo.Kosovo,
      CountryInfo.Kosrae,
      CountryInfo.Kuwait,
      CountryInfo.Kyrgyzstan,
      CountryInfo.Laos,
      CountryInfo.Latvia,
      CountryInfo.Lebanon,
      CountryInfo.Lesotho,
      CountryInfo.Liberia,
      CountryInfo.Libya,
      CountryInfo.Liechtenstein,
      CountryInfo.Lithuania,
      CountryInfo.Luxembourg,
      CountryInfo.Macau,
      CountryInfo.Macedonia,
      CountryInfo.Madagascar,
      CountryInfo.Madeira,
      CountryInfo.Malawi,
      CountryInfo.Malaysia,
      CountryInfo.Maldives,
      CountryInfo.Mali,
      CountryInfo.Malta,
      CountryInfo.MarshallIslands,
      CountryInfo.Martinique,
      CountryInfo.Mauritania,
      CountryInfo.Mauritius,
      CountryInfo.Mayotte,
      CountryInfo.Mexico,
      CountryInfo.Moldova,
      CountryInfo.Monaco,
      CountryInfo.Mongolia,
      CountryInfo.Montserrat,
      CountryInfo.Morocco,
      CountryInfo.Mozambique,
      CountryInfo.Myanmar,
      CountryInfo.Namibia,
      CountryInfo.Nauru,
      CountryInfo.Nepal,
      CountryInfo.Netherlands,
      CountryInfo.NetherlandsAntilles,
      CountryInfo.NewCaledonia,
      CountryInfo.NewZealand,
      CountryInfo.Nicaragua,
      CountryInfo.Niger,
      CountryInfo.Nigeria,
      CountryInfo.Niue,
      CountryInfo.NorfolkIsland,
      CountryInfo.NorthernMarianaIslands,
      CountryInfo.NorthKorea,
      CountryInfo.Norway,
      CountryInfo.Oman,
      CountryInfo.Pakistan,
      CountryInfo.Palau,
      CountryInfo.Palestine,
      CountryInfo.Panama,
      CountryInfo.PapuaNewGuinea,
      CountryInfo.Paraguay,
      CountryInfo.Peru,
      CountryInfo.Philippines,
      CountryInfo.Poland,
      CountryInfo.Ponape,
      CountryInfo.Portugal,
      CountryInfo.PuertoRico,
      CountryInfo.Qatar,
      CountryInfo.RepublicofYemen,
      CountryInfo.Reunion,
      CountryInfo.Romania,
      CountryInfo.Rota,
      CountryInfo.Russia,
      CountryInfo.Rwanda,
      CountryInfo.Saba,
      CountryInfo.Saipan,
      CountryInfo.SanMarino,
      CountryInfo.SaoTomeAndPrincipe,
      CountryInfo.SaudiArabia,
      CountryInfo.Scotland,
      CountryInfo.Senegal,
      CountryInfo.Serbia,
      CountryInfo.Seychelles,
      CountryInfo.SierraLeone,
      CountryInfo.Singapore,
      CountryInfo.Slovakia,
      CountryInfo.Slovenia,
      CountryInfo.SolomonIslands,
      CountryInfo.Somalia,
      CountryInfo.SouthAfrica,
      CountryInfo.SouthKorea,
      CountryInfo.Spain,
      CountryInfo.SriLanka,
      CountryInfo.StBarthelemy,
      CountryInfo.StChristopher,
      CountryInfo.StCroix,
      CountryInfo.StEustatius,
      CountryInfo.StJohn,
      CountryInfo.StKittsNevis,
      CountryInfo.StLucia,
      CountryInfo.StMaarten,
      CountryInfo.StMartin,
      CountryInfo.StThomas,
      CountryInfo.StVincenttheGrenadines,
      CountryInfo.Sudan,
      CountryInfo.Suriname,
      CountryInfo.Swaziland,
      CountryInfo.Sweden,
      CountryInfo.Switzerland,
      CountryInfo.Syria,
      CountryInfo.Tahiti,
      CountryInfo.Taiwan,
      CountryInfo.Tajikistan,
      CountryInfo.Tanzania,
      CountryInfo.Thailand,
      CountryInfo.Tinian,
      CountryInfo.Togo,
      CountryInfo.Tonga,
      CountryInfo.Tokelau,
      CountryInfo.Tortola,
      CountryInfo.TrinidadTobago,
      CountryInfo.Truk,
      CountryInfo.Tunisia,
      CountryInfo.Turkey,
      CountryInfo.Turkmenistan,
      CountryInfo.TurksCaicosIslands,
      CountryInfo.Tuvalu,
      CountryInfo.Uganda,
      CountryInfo.Ukraine,
      CountryInfo.UnionIsland,
      CountryInfo.UnitedArabEmirates,
      CountryInfo.UnitedKingdom,
      CountryInfo.UnitedStates,
      CountryInfo.Uruguay,
      CountryInfo.USVirginIslands,
      CountryInfo.Uzbekistan,
      CountryInfo.Vanuatu,
      CountryInfo.Venezuela,
      CountryInfo.Vietnam,
      CountryInfo.VirginGorda,
      CountryInfo.WakeIsland,
      CountryInfo.Wales,
      CountryInfo.WallisFutunaIslands,
      CountryInfo.WesternSamoa,
      CountryInfo.Zambia,
      CountryInfo.Zimbabwe,
      CountryInfo.VaticanCity
    };

    /// <summary>
    /// Gets all the active countries from the configuration, ordered by name
    /// </summary>
    /// <returns>Returns a list of <see cref="T:Telerik.Sitefinity.Locations.CountryElement" /> objects for all the active countries, ordered by country name</returns>
    public static List<CountryElement> GetActiveCountries() => Config.Get<LocationsConfig>().Countries.Values.Where<CountryElement>((Func<CountryElement, bool>) (x => x.CountryIsActive)).OrderBy<CountryElement, string>((Func<CountryElement, string>) (x => x.Name)).ToList<CountryElement>();

    internal static string GetCultureOfCountryFromCurrencyIsoCode(string currencyIsoCode)
    {
      CountryData countryData = CountryRetriever.GetCountryData().Where<CountryData>((Func<CountryData, bool>) (cd => cd.CurrencyIsoCode == currencyIsoCode)).FirstOrDefault<CountryData>();
      return countryData != null ? countryData.Culture : string.Empty;
    }

    internal static string GetCultureOfCountryFromCountryIsoCode(string countryIsoCode)
    {
      CountryData countryData = CountryRetriever.GetCountryData().Where<CountryData>((Func<CountryData, bool>) (cd => cd.IsoCode == countryIsoCode)).FirstOrDefault<CountryData>();
      return countryData != null ? countryData.Culture : string.Empty;
    }

    internal static string GetCountryNameFromIsoCode(string countryIsoCode)
    {
      CountryData countryData = CountryRetriever.GetCountryData().Where<CountryData>((Func<CountryData, bool>) (cd => cd.IsoCode == countryIsoCode)).FirstOrDefault<CountryData>();
      return countryData != null ? countryData.Name : string.Empty;
    }
  }
}
