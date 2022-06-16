// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSync.AddressConverter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Web;
using System.Xml.Linq;
using Telerik.Sitefinity.GeoLocations.Model;
using Telerik.Sitefinity.SiteSync.Serialization;

namespace Telerik.Sitefinity.SiteSync
{
  internal class AddressConverter : ISiteSyncConverter
  {
    public string Serialize(object obj, Type type, object settings)
    {
      XDocument xdocument = new XDocument(new object[1]
      {
        (object) new XElement((XName) type.FullName)
      });
      this.AddElements(xdocument.Root, obj);
      return xdocument.ToString();
    }

    protected virtual void AddElements(XElement root, object obj)
    {
      Address address = (Address) obj;
      string content1 = address.Latitude.HasValue ? address.Latitude.Value.ToString((IFormatProvider) CultureInfo.InvariantCulture) : (string) null;
      string content2 = address.Longitude.HasValue ? address.Longitude.Value.ToString((IFormatProvider) CultureInfo.InvariantCulture) : (string) null;
      root.Add((object) new XElement((XName) "City", (object) address.City));
      root.Add((object) new XElement((XName) "CountryCode", (object) address.CountryCode));
      root.Add((object) new XElement((XName) "Id", (object) address.Id));
      root.Add((object) new XElement((XName) "Latitude", (object) content1));
      root.Add((object) new XElement((XName) "Longitude", (object) content2));
      root.Add((object) new XElement((XName) "MapZoomLevel", (object) address.MapZoomLevel));
      root.Add((object) new XElement((XName) "StateCode", (object) address.StateCode));
      root.Add((object) new XElement((XName) "Street", (object) address.Street));
      root.Add((object) new XElement((XName) "Zip", (object) address.Zip));
    }

    public void ImportProperty(
      object instance,
      PropertyDescriptor prop,
      object value,
      Type type,
      object args)
    {
      string str1 = value as string;
      IEnumerable<string> strings = value as IEnumerable<string>;
      if (str1 != null)
      {
        // ISSUE: reference to a compiler-generated field
        if (AddressConverter.\u003C\u003Eo__2.\u003C\u003Ep__1 == null)
        {
          // ISSUE: reference to a compiler-generated field
          AddressConverter.\u003C\u003Eo__2.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, Address>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (Address), typeof (AddressConverter)));
        }
        // ISSUE: reference to a compiler-generated field
        Func<CallSite, object, Address> target = AddressConverter.\u003C\u003Eo__2.\u003C\u003Ep__1.Target;
        // ISSUE: reference to a compiler-generated field
        CallSite<Func<CallSite, object, Address>> p1 = AddressConverter.\u003C\u003Eo__2.\u003C\u003Ep__1;
        // ISSUE: reference to a compiler-generated field
        if (AddressConverter.\u003C\u003Eo__2.\u003C\u003Ep__0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          AddressConverter.\u003C\u003Eo__2.\u003C\u003Ep__0 = CallSite<Func<CallSite, AddressConverter, string, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Deserialize", (IEnumerable<Type>) null, typeof (AddressConverter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[4]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj = AddressConverter.\u003C\u003Eo__2.\u003C\u003Ep__0.Target((CallSite) AddressConverter.\u003C\u003Eo__2.\u003C\u003Ep__0, this, str1, type, args);
        Address address = target((CallSite) p1, obj);
        prop.SetValue(instance, (object) address);
      }
      else
      {
        if (strings == null)
          return;
        List<Address> addressList1 = new List<Address>();
        foreach (string str2 in strings)
        {
          List<Address> addressList2 = addressList1;
          // ISSUE: reference to a compiler-generated field
          if (AddressConverter.\u003C\u003Eo__2.\u003C\u003Ep__3 == null)
          {
            // ISSUE: reference to a compiler-generated field
            AddressConverter.\u003C\u003Eo__2.\u003C\u003Ep__3 = CallSite<Func<CallSite, object, Address>>.Create(Binder.Convert(CSharpBinderFlags.ConvertExplicit, typeof (Address), typeof (AddressConverter)));
          }
          // ISSUE: reference to a compiler-generated field
          Func<CallSite, object, Address> target = AddressConverter.\u003C\u003Eo__2.\u003C\u003Ep__3.Target;
          // ISSUE: reference to a compiler-generated field
          CallSite<Func<CallSite, object, Address>> p3 = AddressConverter.\u003C\u003Eo__2.\u003C\u003Ep__3;
          // ISSUE: reference to a compiler-generated field
          if (AddressConverter.\u003C\u003Eo__2.\u003C\u003Ep__2 == null)
          {
            // ISSUE: reference to a compiler-generated field
            AddressConverter.\u003C\u003Eo__2.\u003C\u003Ep__2 = CallSite<Func<CallSite, AddressConverter, string, Type, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "Deserialize", (IEnumerable<Type>) null, typeof (AddressConverter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[4]
            {
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
              CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
            }));
          }
          // ISSUE: reference to a compiler-generated field
          // ISSUE: reference to a compiler-generated field
          object obj = AddressConverter.\u003C\u003Eo__2.\u003C\u003Ep__2.Target((CallSite) AddressConverter.\u003C\u003Eo__2.\u003C\u003Ep__2, this, str2, typeof (Address), args);
          Address address = target((CallSite) p3, obj);
          addressList2.Add(address);
        }
        prop.SetValue(instance, (object) addressList1.ToArray());
      }
    }

    public object Deserialize(string str, Type type, object settings)
    {
      XElement root = XDocument.Parse(HttpUtility.HtmlDecode(str)).Root;
      Address instance = (Address) Activator.CreateInstance(type);
      string s1 = root.Element((XName) "Latitude").Value;
      string s2 = root.Element((XName) "Longitude").Value;
      instance.City = root.Element((XName) "City").Value;
      instance.CountryCode = root.Element((XName) "CountryCode").Value;
      instance.Id = new Guid(root.Element((XName) "Id").Value);
      if (!string.IsNullOrEmpty(s1))
        instance.Latitude = new double?(double.Parse(s1, (IFormatProvider) CultureInfo.InvariantCulture));
      if (!string.IsNullOrEmpty(s2))
        instance.Longitude = new double?(double.Parse(s2, (IFormatProvider) CultureInfo.InvariantCulture));
      if (!string.IsNullOrEmpty(root.Element((XName) "MapZoomLevel").Value))
        instance.MapZoomLevel = new int?(int.Parse(root.Element((XName) "MapZoomLevel").Value));
      instance.StateCode = root.Element((XName) "StateCode").Value;
      instance.Street = root.Element((XName) "Street").Value;
      instance.Zip = root.Element((XName) "Zip").Value;
      return (object) instance;
    }
  }
}
