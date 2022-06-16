// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ControlDesign.GenericCollectionConverter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.Sitefinity.Web.UI.ControlDesign
{
  public class GenericCollectionConverter : TypeConverter, IComplexConverter
  {
    public override PropertyDescriptorCollection GetProperties(
      ITypeDescriptorContext context,
      object value,
      Attribute[] attributes)
    {
      Type type = value.GetType();
      IEnumerable enumerable = (IEnumerable) value;
      List<PropertyDescriptor> propertyDescriptorList = new List<PropertyDescriptor>();
      List<Attribute> attributeList = new List<Attribute>();
      attributeList.Add((Attribute) new TypeConverterAttribute(typeof (ExpandableObjectConverter)));
      foreach (object obj in enumerable)
      {
        string empty = string.Empty;
        PropertyDescriptor defaultProperty = TypeDescriptor.GetDefaultProperty(obj);
        string propName = defaultProperty == null ? TypeDescriptor.GetProperties(obj)[0].GetValue(obj).ToString() : defaultProperty.GetValue(obj).ToString();
        propertyDescriptorList.Add((PropertyDescriptor) new GenericCollectionPropertyDescriptor(obj, propName, type, attributeList.ToArray()));
      }
      return new PropertyDescriptorCollection(propertyDescriptorList.ToArray());
    }

    public override bool GetPropertiesSupported(ITypeDescriptorContext context) => true;
  }
}
