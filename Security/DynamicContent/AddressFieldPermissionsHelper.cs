// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.AddressFieldPermissionsHelper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Globalization;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GeoLocations.Model;

namespace Telerik.Sitefinity.Security
{
  /// <summary>
  /// Helper class for determining state of address fields when field permissions are applied
  /// </summary>
  internal class AddressFieldPermissionsHelper
  {
    /// <summary>
    /// originalValue for Address field is of type AddressProperties. The current address object is updated with the original values of its properties.
    /// </summary>
    internal static void SetOriginalValueBack(
      DynamicModuleDataProvider provider,
      PropertyDescriptor property,
      DynamicContent dataItem,
      object originalValue)
    {
      Address address = (Address) property.GetValue((object) dataItem);
      AddressFieldPermissionsHelper.AddressProperties addressProperties = (AddressFieldPermissionsHelper.AddressProperties) originalValue;
      address.Street = addressProperties.Street;
      address.Zip = addressProperties.Zip;
      address.StateCode = addressProperties.StateCode;
      address.CountryCode = addressProperties.CountryCode;
      address.City = addressProperties.City;
      address.Latitude = addressProperties.Latitude;
      address.Longitude = addressProperties.Longitude;
      address.MapZoomLevel = addressProperties.MapZoomLevel;
      property.SetValue((object) dataItem, (object) address);
    }

    /// <summary>
    /// Checks if current property values of Address field are different from the original ones
    /// </summary>
    internal static bool HasPropertyValueChanged(
      PropertyDescriptor property,
      object originalValue,
      object currentValue)
    {
      Address address = (Address) currentValue;
      AddressFieldPermissionsHelper.AddressProperties addressProperties = (AddressFieldPermissionsHelper.AddressProperties) originalValue;
      if (!(address.Street != addressProperties.Street) && !(address.Zip != addressProperties.Zip) && !(address.StateCode != addressProperties.StateCode) && !(address.CountryCode != addressProperties.CountryCode) && !(address.City != addressProperties.City))
      {
        double? nullable = address.Latitude;
        double? latitude = addressProperties.Latitude;
        if (nullable.GetValueOrDefault() == latitude.GetValueOrDefault() & nullable.HasValue == latitude.HasValue)
        {
          double? longitude = address.Longitude;
          nullable = addressProperties.Longitude;
          if (longitude.GetValueOrDefault() == nullable.GetValueOrDefault() & longitude.HasValue == nullable.HasValue)
          {
            int? mapZoomLevel1 = address.MapZoomLevel;
            int? mapZoomLevel2 = addressProperties.MapZoomLevel;
            return !(mapZoomLevel1.GetValueOrDefault() == mapZoomLevel2.GetValueOrDefault() & mapZoomLevel1.HasValue == mapZoomLevel2.HasValue);
          }
        }
      }
      return true;
    }

    internal static object GetOriginalValue(
      DynamicModuleDataProvider provider,
      DynamicContent dataItem,
      PropertyDescriptor property)
    {
      Address entity = (Address) property.GetValue((object) dataItem);
      AddressFieldPermissionsHelper.AddressProperties originalValue = new AddressFieldPermissionsHelper.AddressProperties();
      try
      {
        originalValue.Street = provider.GetOriginalValue<string>((object) entity, "Street");
        originalValue.City = provider.GetOriginalValue<string>((object) entity, "City");
        originalValue.Zip = provider.GetOriginalValue<string>((object) entity, "Zip");
        originalValue.CountryCode = provider.GetOriginalValue<string>((object) entity, "CountryCode");
        originalValue.StateCode = provider.GetOriginalValue<string>((object) entity, "StateCode");
        originalValue.MapZoomLevel = provider.GetOriginalValue<int?>((object) entity, "MapZoomLevel");
        originalValue.Latitude = provider.GetOriginalValue<double?>((object) entity, "Latitude");
        originalValue.Longitude = provider.GetOriginalValue<double?>((object) entity, "Longitude");
      }
      catch (InvalidOperationException ex)
      {
      }
      return (object) originalValue;
    }

    internal static bool IsDefaultPropertyValue(
      PropertyDescriptor property,
      object currentValue,
      CultureInfo culture)
    {
      Address address = (Address) currentValue;
      if (string.IsNullOrEmpty(address.Street) && string.IsNullOrEmpty(address.Zip) && string.IsNullOrEmpty(address.StateCode) && string.IsNullOrEmpty(address.CountryCode) && string.IsNullOrEmpty(address.City))
      {
        double? nullable = address.Latitude;
        if (!nullable.HasValue)
        {
          nullable = address.Longitude;
          if (!nullable.HasValue)
            return !address.MapZoomLevel.HasValue;
        }
      }
      return false;
    }

    private struct AddressProperties
    {
      public string Zip { get; set; }

      public string StateCode { get; set; }

      public string CountryCode { get; set; }

      public string City { get; set; }

      public string Street { get; set; }

      public int? MapZoomLevel { get; set; }

      public double? Latitude { get; set; }

      public double? Longitude { get; set; }
    }
  }
}
