// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Contracts.Operations.Countries.CountriesOperationProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Locations;
using Telerik.Sitefinity.Locations.Utilities;
using Telerik.Sitefinity.Web.Services.Contracts.Operations.Address;

namespace Telerik.Sitefinity.Web.Services.Contracts.Operations.Countries
{
  internal class CountriesOperationProvider : IOperationProvider
  {
    public IEnumerable<OperationData> GetOperations(Type clrType)
    {
      OperationData operationData = OperationData.Create<IEnumerable<CountryModel>>(new Func<OperationContext, IEnumerable<CountryModel>>(this.SfCountries));
      operationData.OperationType = OperationType.Unbound;
      return (IEnumerable<OperationData>) new OperationData[1]
      {
        operationData
      };
    }

    private IEnumerable<CountryModel> SfCountries(OperationContext context)
    {
      IList<CountryElement> countries = new LocationRetriever().GetCountries();
      List<CountryModel> countryModelList = new List<CountryModel>();
      foreach (CountryElement countryElement in (IEnumerable<CountryElement>) countries)
      {
        CountryElement country = countryElement;
        List<StateModel> list = country.StatesProvinces.Values.Select<StateProvinceElement, StateModel>((Func<StateProvinceElement, StateModel>) (s => new StateModel()
        {
          Abbreviation = s.Abbreviation,
          CountryKey = country.CurrencyIsoCode,
          StateProvinceName = s.Name
        })).ToList<StateModel>();
        countryModelList.Add(new CountryModel()
        {
          IsoCode = country.IsoCode,
          Name = country.Name,
          States = (IEnumerable<StateModel>) list
        });
      }
      return (IEnumerable<CountryModel>) countryModelList;
    }
  }
}
