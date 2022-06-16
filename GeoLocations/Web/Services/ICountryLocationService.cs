// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.GeoLocations.Web.Services.ICountryLocationService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ServiceModel;
using System.ServiceModel.Web;

namespace Telerik.Sitefinity.GeoLocations.Web.Services
{
  /// <summary>Defines functions for retrieving country locations.</summary>
  [ServiceContract]
  internal interface ICountryLocationService
  {
    /// <summary>
    /// Returns coordinates of country by Iso Code in json format.
    /// </summary>
    /// <param name="isoCode">The ISO code of the country.</param>
    /// as
    [OperationContract]
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "country/{isoCode}/")]
    SimpleLocationViewModel GetCountryLocation(string isoCode);

    /// <summary>
    /// Returns coordinates of country by Iso Code in xml format.
    /// </summary>
    /// <param name="isoCode">The ISO code of the country.</param>
    [OperationContract]
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/country/{isoCode}/")]
    SimpleLocationViewModel GetCountryLocationXml(string isoCode);

    /// <summary>
    /// Returns coordinates of state having provided country iso code, and state code in json format.
    /// </summary>
    /// <param name="countryIsoCode">The ISO code of the country.</param>
    /// <param name="stateCode">The ISO code of the state.</param>
    [OperationContract]
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "state/{countryIsoCode}/{stateIsoCode}/")]
    SimpleLocationViewModel GetStateLocation(
      string countryIsoCode,
      string stateIsoCode);

    /// <summary>
    /// Returns coordinates of state having provided country iso code, and state code in xml format.
    /// </summary>
    /// <param name="countryIsoCode">The ISO code of the country.</param>
    /// <param name="stateCode">The ISO code of the state.</param>
    [OperationContract]
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/state/{countryIsoCode}/{stateIsoCode}/")]
    SimpleLocationViewModel GetStateLocationXml(
      string countryIsoCode,
      string stateIsoCode);
  }
}
