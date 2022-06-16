// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Summary.SummarySettingsConverter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;

namespace Telerik.Sitefinity.Data.Summary
{
  /// <summary>
  /// Provides a way of converting SummarySettings structure to its string representation and vice versa.
  /// </summary>
  public class SummarySettingsConverter : TypeConverter
  {
    /// <summary>
    /// Returns whether this converter can convert an object of the given type to
    ///     the type of this converter, using the specified context.
    /// </summary>
    /// <param name="context">A System.ComponentModel.ITypeDescriptorContext that provides a format context.</param>
    /// <param name="sourceType">A System.Type that represents the type you want to convert from.</param>
    /// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) => sourceType == typeof (string) || base.CanConvertFrom(context, sourceType);

    /// <summary>
    /// Returns whether this converter can convert the object to the specified type,
    ///     using the specified context.
    /// </summary>
    /// <param name="context">A System.ComponentModel.ITypeDescriptorContext that provides a format context.</param>
    /// <param name="destinationType">A System.Type that represents the type you want to convert to.</param>
    /// <returns>true if this converter can perform the conversion; otherwise, false.</returns>
    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) => destinationType == typeof (InstanceDescriptor) || base.CanConvertTo(context, destinationType);

    /// <summary>
    /// Converts the given object to the type of this converter, using the specified context and culture information.
    /// </summary>
    /// <param name="context">An ITypeDescriptorContext that provides a format context. </param>
    /// <param name="culture">The CultureInfo to use as the current culture.</param>
    /// <param name="value">The Object to convert.</param>
    /// <returns></returns>
    public override object ConvertFrom(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value)
    {
      return value is string strVal ? SummarySettingsConverter.ConstructData(strVal) : base.ConvertFrom(context, culture, value);
    }

    /// <summary>
    /// Converts the given value object to the specified type, using the specified context and culture information.
    /// </summary>
    /// <param name="context">An ITypeDescriptorContext that provides a format context.</param>
    /// <param name="culture">A CultureInfo. If null is passed, the current culture is assumed.</param>
    /// <param name="value">The Object to convert.</param>
    /// <param name="destinationType">The Type to convert the value parameter to.</param>
    /// <returns></returns>
    public override object ConvertTo(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value,
      Type destinationType)
    {
      if (destinationType == (Type) null)
        throw new ArgumentNullException(nameof (destinationType));
      if (value is SummarySettings summarySettings)
      {
        if (destinationType == typeof (string))
          return (object) this.Serialize(summarySettings);
        if (destinationType == typeof (InstanceDescriptor))
          return (object) new InstanceDescriptor((MemberInfo) this.GetType().GetMethod("ConstructData"), (ICollection) new object[1]
          {
            (object) this.Serialize(summarySettings)
          });
      }
      return base.ConvertTo(context, culture, value, destinationType);
    }

    /// <summary>Returns a deserialized object</summary>
    /// <param name="strVal">The serialized object</param>
    /// <returns>Deserialized object</returns>
    public static object ConstructData(string strVal) => (object) SummarySettings.Parse(strVal);

    private string Serialize(SummarySettings value) => value.ToString();
  }
}
