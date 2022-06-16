// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.ImageProcessing.ImageProcessingProperty
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Modules.Libraries.ImageProcessing
{
  /// <summary>
  /// Defines the metadata for the particular property used as a parameter for the image processing method.
  /// </summary>
  public class ImageProcessingProperty
  {
    private readonly ImageProcessingMethod method;
    private readonly PropertyInfo propertyInfo;
    private readonly TypeConverter converter;
    private readonly string title;
    private readonly string units;
    private readonly string resourceClassId;
    private readonly string regularExpressionViolationMessage;
    private readonly string regularExpression;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.ImageProcessing.ImageProcessingProperty" /> class.
    /// </summary>
    /// <param name="method">The method.</param>
    /// <param name="propertyInfo">The property info.</param>
    /// <param name="attribute">The attribute.</param>
    public ImageProcessingProperty(
      ImageProcessingMethod method,
      PropertyInfo propertyInfo,
      ImageProcessingPropertyAttribute attribute)
    {
      this.method = method;
      this.propertyInfo = propertyInfo;
      this.title = attribute.Title;
      this.units = attribute.Units;
      this.resourceClassId = attribute.ResourceClassId;
      this.IsRequired = attribute.IsRequired;
      TypeConverterAttribute converterAttribute = ((IEnumerable<object>) propertyInfo.GetCustomAttributes(typeof (TypeConverterAttribute), true)).FirstOrDefault<object>() as TypeConverterAttribute;
      this.regularExpression = attribute.RegularExpression;
      this.regularExpressionViolationMessage = attribute.RegularExpressionViolationMessage;
      if (converterAttribute == null)
        return;
      this.converter = (TypeConverter) Activator.CreateInstance(Type.GetType(converterAttribute.ConverterTypeName), true);
    }

    /// <summary>Gets the name.</summary>
    /// <value>The name.</value>
    public string Name => this.PropertyInfo.Name;

    /// <summary>Gets the method.</summary>
    /// <value>The method.</value>
    public ImageProcessingMethod Method => this.method;

    /// <summary>Gets the property info.</summary>
    /// <value>The property info.</value>
    public PropertyInfo PropertyInfo => this.propertyInfo;

    /// <summary>Gets the title.</summary>
    /// <returns></returns>
    public virtual string GetTitle()
    {
      string key = this.title.IsNullOrEmpty() ? this.propertyInfo.Name : this.title;
      return !this.resourceClassId.IsNullOrEmpty() ? Res.Get(this.resourceClassId, key) : key;
    }

    /// <summary>Gets the units.</summary>
    /// <returns></returns>
    public virtual string GetUnits()
    {
      if (this.resourceClassId.IsNullOrEmpty() && !this.units.IsNullOrEmpty())
        return this.units;
      return !this.resourceClassId.IsNullOrEmpty() && !this.units.IsNullOrEmpty() ? Res.Get(this.resourceClassId, this.units) : "px";
    }

    /// <summary>Gets the regular expression violation message.</summary>
    public virtual string GetRegularExpressionViolationMessage()
    {
      if (this.resourceClassId.IsNullOrEmpty() && !this.regularExpressionViolationMessage.IsNullOrEmpty())
        return this.regularExpressionViolationMessage;
      return !this.resourceClassId.IsNullOrEmpty() && !this.regularExpressionViolationMessage.IsNullOrEmpty() ? Res.Get(this.resourceClassId, this.regularExpressionViolationMessage) : "Field value is invalid";
    }

    /// <summary>Gets the converter.</summary>
    public TypeConverter Converter => this.converter;

    internal string GetStringValue(object instance) => this.Method.ConvertPropertyValueToString(this, this.PropertyInfo.GetValue(instance, (object[]) null));

    internal bool IsRequired { get; set; }

    /// <summary>Gets the regular expression.</summary>
    public string RegularExpression => this.regularExpression;
  }
}
