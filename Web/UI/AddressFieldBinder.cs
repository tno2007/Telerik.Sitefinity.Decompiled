// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.AddressFieldBinder
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Sitefinity.GeoLocations.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// This class provides binding functionality for the address field
  /// on the public side in the read mode.
  /// </summary>
  public class AddressFieldBinder
  {
    /// <summary>
    /// Finds all the address fields in the container and binds them to the model.
    /// </summary>
    /// <param name="container">
    /// The container in which the instances of <see cref="T:Telerik.Sitefinity.Web.UI.Fields.AddressField" /> should
    /// be found.
    /// </param>
    /// <param name="model">
    /// The instance of the data item to which the address fields should be bound.
    /// </param>
    public static void BindAllAddressFields(Control container, IDataItem model)
    {
      if (container == null)
        throw new ArgumentNullException(nameof (container));
      if (model == null)
        throw new ArgumentNullException(nameof (model));
      IList<AddressField> addressFields = (IList<AddressField>) new List<AddressField>();
      AddressFieldBinder.FindAllAddressControls(container, addressFields);
      foreach (AddressField addressField in (IEnumerable<AddressField>) addressFields)
        AddressFieldBinder.BindAddressFieldControl(addressField, model);
    }

    private static void FindAllAddressControls(Control container, IList<AddressField> addressFields)
    {
      foreach (Control control in container.Controls)
      {
        if (control is AddressField addressField)
          addressFields.Add(addressField);
        else
          AddressFieldBinder.FindAllAddressControls(control, addressFields);
      }
    }

    private static void BindAddressFieldControl(AddressField addressField, IDataItem model)
    {
      if (addressField == null)
        throw new ArgumentNullException(nameof (addressField));
      if (model == null)
        throw new ArgumentNullException(nameof (model));
      string dataFieldName = addressField.DataFieldName;
      PropertyDescriptor propertyDescriptor = !string.IsNullOrEmpty(dataFieldName) ? TypeDescriptor.GetProperties((object) model)[dataFieldName] : throw new ArgumentException("The AddressField must have defined DataFieldName.");
      if (propertyDescriptor == null)
        return;
      Address address = (Address) propertyDescriptor.GetValue((object) model);
      if (address == null)
        return;
      addressField.AddressData = address;
      addressField.WorkMode = AddressFieldBinder.GetAddressFieldWorkMode(address);
    }

    private static AddressWorkMode GetAddressFieldWorkMode(Address address)
    {
      bool flag1 = !address.Longitude.HasValue && !address.Latitude.HasValue && !address.MapZoomLevel.HasValue;
      bool flag2 = address.Street == null && address.CountryCode == null && address.StateCode == null && address.City == null && address.Zip == null;
      if (!flag1 && !flag2)
        return AddressWorkMode.Hybrid;
      return flag1 && !flag2 || !(!flag1 & flag2) ? AddressWorkMode.FormOnly : AddressWorkMode.MapOnly;
    }
  }
}
