// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.WrapperObjectPropertyInfo
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace Telerik.Sitefinity.Publishing
{
  public class WrapperObjectPropertyInfo : PropertyInfo
  {
    private string name;
    private Type propertyType;
    private List<Attribute> attributes;

    public WrapperObjectPropertyInfo(string name, Type propertyType, List<Attribute> attributes = null)
    {
      this.name = name;
      this.propertyType = propertyType;
      this.attributes = attributes;
    }

    public override string Name => this.name;

    public override Type PropertyType => this.propertyType;

    public override bool CanRead => true;

    public override bool CanWrite => true;

    public override object GetValue(
      object obj,
      BindingFlags invokeAttr,
      Binder binder,
      object[] index,
      CultureInfo culture)
    {
      return ((WrapperObject) obj).GetProperty(this.name);
    }

    public override void SetValue(
      object obj,
      object value,
      BindingFlags invokeAttr,
      Binder binder,
      object[] index,
      CultureInfo culture)
    {
      ((WrapperObject) obj).SetProperty(this.name, value);
    }

    public override object[] GetCustomAttributes(bool inherit) => this.attributes != null ? (object[]) this.attributes.ToArray() : new object[0];

    public override object[] GetCustomAttributes(Type attributeType, bool inherit) => this.attributes != null ? (object[]) this.attributes.Where<Attribute>((Func<Attribute, bool>) (a => a.GetType() == attributeType)).ToArray<Attribute>() : new object[0];

    public override PropertyAttributes Attributes => throw new NotImplementedException();

    public override Type DeclaringType => throw new NotImplementedException();

    public override MethodInfo[] GetAccessors(bool nonPublic) => throw new NotImplementedException();

    public override MethodInfo GetGetMethod(bool nonPublic) => throw new NotImplementedException();

    public override ParameterInfo[] GetIndexParameters() => throw new NotImplementedException();

    public override MethodInfo GetSetMethod(bool nonPublic) => throw new NotImplementedException();

    public override bool IsDefined(Type attributeType, bool inherit) => throw new NotImplementedException();

    public override Type ReflectedType => throw new NotImplementedException();
  }
}
