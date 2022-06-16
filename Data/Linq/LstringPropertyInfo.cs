// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.LstringPropertyInfo
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Globalization;
using System.Reflection;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Data
{
  internal class LstringPropertyInfo : PropertyInfo
  {
    private PropertyInfo lstringPropertyInfo;
    private string cultureSpecificName;

    public LstringPropertyInfo(PropertyInfo lstringPropertyInfo, string cultSpecificName)
    {
      if (lstringPropertyInfo == (PropertyInfo) null)
        throw new ArgumentNullException(nameof (lstringPropertyInfo));
      if (string.IsNullOrEmpty(cultSpecificName))
        throw new ArgumentException("cultSpecificName is null or empty");
      this.lstringPropertyInfo = !(lstringPropertyInfo.PropertyType != typeof (Lstring)) ? lstringPropertyInfo : throw new NotSupportedException("This class can only wrap Lstring PropertyInfo");
      this.cultureSpecificName = cultSpecificName;
    }

    public override PropertyAttributes Attributes => this.lstringPropertyInfo.Attributes;

    public override bool CanRead => this.lstringPropertyInfo.CanRead;

    public override bool CanWrite => this.lstringPropertyInfo.CanWrite;

    public override MethodInfo[] GetAccessors(bool nonPublic) => this.lstringPropertyInfo.GetAccessors(nonPublic);

    public override MethodInfo GetGetMethod(bool nonPublic) => this.lstringPropertyInfo.GetGetMethod(nonPublic);

    public override ParameterInfo[] GetIndexParameters() => this.lstringPropertyInfo.GetIndexParameters();

    public override MethodInfo GetSetMethod(bool nonPublic) => this.lstringPropertyInfo.GetSetMethod(nonPublic);

    public override object GetValue(
      object obj,
      BindingFlags invokeAttr,
      Binder binder,
      object[] index,
      CultureInfo culture)
    {
      Lstring lstring = this.lstringPropertyInfo.GetValue(obj, invokeAttr, binder, index, culture) as Lstring;
      return lstring != (Lstring) null ? (object) lstring[culture] : (object) null;
    }

    public override Type PropertyType => typeof (string);

    public override void SetValue(
      object obj,
      object value,
      BindingFlags invokeAttr,
      Binder binder,
      object[] index,
      CultureInfo culture)
    {
      this.lstringPropertyInfo.SetValue((object) new Lstring(obj as string), value, invokeAttr, binder, index, culture);
    }

    public override Type DeclaringType => this.lstringPropertyInfo.DeclaringType;

    public override object[] GetCustomAttributes(Type attributeType, bool inherit) => this.lstringPropertyInfo.GetCustomAttributes(attributeType, inherit);

    public override object[] GetCustomAttributes(bool inherit) => this.lstringPropertyInfo.GetCustomAttributes(inherit);

    public override bool IsDefined(Type attributeType, bool inherit) => this.lstringPropertyInfo.IsDefined(attributeType, inherit);

    public override string Name => this.cultureSpecificName;

    public override Type ReflectedType => this.lstringPropertyInfo.ReflectedType;
  }
}
