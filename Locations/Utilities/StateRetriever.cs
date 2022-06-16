// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Locations.Utilities.StateRetriever
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Locations.Configuration;

namespace Telerik.Sitefinity.Locations.Utilities
{
  internal class StateRetriever
  {
    public static List<StateData> GetStateData() => new List<StateData>()
    {
      StateInfo.Alabama,
      StateInfo.Alaska,
      StateInfo.Arizona,
      StateInfo.Arkansas,
      StateInfo.California,
      StateInfo.Colorado,
      StateInfo.Connecticut,
      StateInfo.Delaware,
      StateInfo.Florida,
      StateInfo.Georgia,
      StateInfo.Hawaii,
      StateInfo.Idaho,
      StateInfo.Illinois,
      StateInfo.Indiana,
      StateInfo.Iowa,
      StateInfo.Kansas,
      StateInfo.Kentucky,
      StateInfo.Louisiana,
      StateInfo.Maine,
      StateInfo.Maryland,
      StateInfo.Massachusetts,
      StateInfo.Michigan,
      StateInfo.Minnesota,
      StateInfo.Mississippi,
      StateInfo.Missouri,
      StateInfo.Montana,
      StateInfo.Nebraska,
      StateInfo.Nevada,
      StateInfo.NewHampshire,
      StateInfo.NewJersey,
      StateInfo.NewMexico,
      StateInfo.NewYork,
      StateInfo.NorthCarolina,
      StateInfo.NorthDakota,
      StateInfo.Ohio,
      StateInfo.Oklahoma,
      StateInfo.Oregon,
      StateInfo.Pennsylvania,
      StateInfo.RhodeIsland,
      StateInfo.SouthCarolina,
      StateInfo.SouthDakota,
      StateInfo.Tennessee,
      StateInfo.Texas,
      StateInfo.Utah,
      StateInfo.Vermont,
      StateInfo.Virginia,
      StateInfo.Washington,
      StateInfo.WestVirginia,
      StateInfo.Wisconsin,
      StateInfo.Wyoming,
      StateInfo.Alberta,
      StateInfo.BritishColumbia,
      StateInfo.Manitoba,
      StateInfo.NewBrunswick,
      StateInfo.NewfoundlandandLabrador,
      StateInfo.NorthwestTerritories,
      StateInfo.NovaScotia,
      StateInfo.Nunavut,
      StateInfo.Ontario,
      StateInfo.PrinceEdwardIsland,
      StateInfo.Quebec,
      StateInfo.Saskatchewan,
      StateInfo.Yukon
    };

    /// <summary>
    /// Gets all active states from the configuration, ordered by state name
    /// </summary>
    /// <returns>Returns a list of <see cref="T:Telerik.Sitefinity.Locations.CountryStateInfo" /> objects containing information about the active states. The list is ordered by state name</returns>
    public static List<CountryStateInfo> GetActiveStates()
    {
      List<CountryElement> list1 = Config.Get<LocationsConfig>().Countries.Values.Where<CountryElement>((Func<CountryElement, bool>) (x => x.CountryIsActive)).OrderBy<CountryElement, string>((Func<CountryElement, string>) (x => x.Name)).ToList<CountryElement>();
      List<CountryStateInfo> activeStates = new List<CountryStateInfo>();
      foreach (CountryElement country in list1)
      {
        List<StateProvinceElement> list2 = country.StatesProvinces.Values.OrderBy<StateProvinceElement, string>((Func<StateProvinceElement, string>) (x => x.Name)).ToList<StateProvinceElement>();
        string desiredCountryCssClass = StateRetriever.GetDesiredCountryCssClass(country);
        foreach (StateProvinceElement stateProvinceElement in list2)
          activeStates.Add(new CountryStateInfo()
          {
            CssClass = desiredCountryCssClass,
            Abbreviation = stateProvinceElement.Abbreviation,
            StateProvinceName = stateProvinceElement.Name
          });
      }
      return activeStates;
    }

    internal static string GetStateNameFromCode(string code)
    {
      CountryStateInfo countryStateInfo = StateRetriever.GetActiveStates().Where<CountryStateInfo>((Func<CountryStateInfo, bool>) (s => s.Abbreviation == code)).FirstOrDefault<CountryStateInfo>();
      return countryStateInfo != null ? countryStateInfo.StateProvinceName : string.Empty;
    }

    internal static string GetStateNameFromCodeAndCountry(string code, string countryIsoCode)
    {
      StateProvinceElement stateProvinceElement = StateRetriever.GetStatesAndProvincesForCountry(countryIsoCode).Where<StateProvinceElement>((Func<StateProvinceElement, bool>) (s => s.Abbreviation == code)).FirstOrDefault<StateProvinceElement>();
      return stateProvinceElement != null ? stateProvinceElement.Name : string.Empty;
    }

    internal static string GetStateProvinceDataAsJson() => new JavaScriptSerializer().Serialize((object) StateRetriever.GetActiveStates());

    internal static List<StateProvinceElement> GetStatesAndProvincesForCountry(
      string selectedCountry)
    {
      CountryElement countryElement = Config.Get<LocationsConfig>().Countries.Values.Where<CountryElement>((Func<CountryElement, bool>) (c => c.CountryIsActive && c.IsoCode == selectedCountry)).FirstOrDefault<CountryElement>();
      return countryElement == null ? (List<StateProvinceElement>) null : countryElement.StatesProvinces.Values.OrderBy<StateProvinceElement, string>((Func<StateProvinceElement, string>) (x => x.Name)).ToList<StateProvinceElement>();
    }

    private static string GetDesiredCountryCssClass(CountryElement country) => string.Compare(country.IsoCode, CountryInfo.UnitedStates.IsoCode, true) != 0 ? (string.Compare(country.IsoCode, CountryInfo.Canada.IsoCode, true) != 0 ? "sf" + country.IsoCode + "Country" : "sfCanada") : "sfUnitedStates";
  }
}
