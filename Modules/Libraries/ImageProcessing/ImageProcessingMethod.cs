// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.ImageProcessing.ImageProcessingMethod
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Modules.Libraries.ImageProcessing
{
  /// <summary>Defines the image processing method's information.</summary>
  public class ImageProcessingMethod
  {
    private readonly IImageProcessor imageProcessor;
    private List<ImageProcessingProperty> argProperties;
    private string methodKey;
    private readonly string sizeFormat;
    private readonly MethodInfo methodInfo;
    private readonly Type argumentType;
    private readonly string title;
    private readonly string resourceClassId;
    private readonly bool isBrowsable;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.ImageProcessing.ImageProcessingMethod" /> class.
    /// </summary>
    /// <param name="methodName">Name of the method.</param>
    /// <param name="attribute">The attribute.</param>
    /// <param name="argumentType">Type of the argument.</param>
    public ImageProcessingMethod(
      IImageProcessor imageProcessor,
      MethodInfo method,
      ImageProcessingMethodAttribute attribute,
      Type argumentType)
    {
      this.imageProcessor = imageProcessor;
      this.methodInfo = method;
      this.argumentType = argumentType;
      this.resourceClassId = attribute.ResourceClassId;
      this.title = attribute.Title;
      if (string.IsNullOrEmpty(this.title))
        this.title = string.Format("{0} {1}", (object) method.Name, (object) argumentType.Name);
      this.sizeFormat = attribute.LabelFormat;
      this.DescriptionText = attribute.DescriptionText;
      this.DescriptionImageResourceName = attribute.DescriptionImageResourceName;
      this.ValidateArgumentsMethodName = attribute.ValidateArgumentsMethodName;
      this.AssemblyInfo = method.DeclaringType;
      if (!(((IEnumerable<object>) method.GetCustomAttributes(typeof (BrowsableAttribute), true)).FirstOrDefault<object>() is BrowsableAttribute browsableAttribute) || browsableAttribute.Browsable)
        this.isBrowsable = true;
      this.argProperties = new List<ImageProcessingProperty>();
      if (!(this.argumentType != (Type) null))
        return;
      foreach (PropertyInfo property in this.argumentType.GetProperties(BindingFlags.Instance | BindingFlags.Public))
      {
        if (((IEnumerable<object>) property.GetCustomAttributes(typeof (ImageProcessingPropertyAttribute), true)).FirstOrDefault<object>() is ImageProcessingPropertyAttribute propertyAttribute)
          this.argProperties.Add(this.CreateImageProcessingProperty(property, propertyAttribute));
      }
    }

    /// <summary>Gets the image processor.</summary>
    /// <value>The image processor.</value>
    public IImageProcessor ImageProcessor => this.imageProcessor;

    /// <summary>Gets the method info.</summary>
    /// <value>The method info.</value>
    public MethodInfo MethodInfo => this.methodInfo;

    /// <summary>Gets the name of the image processing method.</summary>
    /// <value>The name of the method.</value>
    public string MethodName => this.methodInfo.Name;

    /// <summary>
    /// Gets the type of the argument passed when generating image.
    /// </summary>
    /// <value>The type of the argument.</value>
    public Type ArgumentType => this.argumentType;

    /// <summary>Gets the method key.</summary>
    public virtual string MethodKey
    {
      get
      {
        if (this.methodKey == null)
        {
          this.methodKey = this.MethodName;
          if (this.ArgumentType != (Type) null)
            this.methodKey += this.ArgumentType.Name;
        }
        return this.methodKey;
      }
    }

    internal string ValidateArgumentsMethodName { get; set; }

    internal string DescriptionText { get; set; }

    internal string DescriptionImageResourceName { get; set; }

    internal Type AssemblyInfo { get; set; }

    /// <summary>
    /// Creates the argument instance and sets it's properties to the given values.
    /// </summary>
    /// <param name="properties">The properties.</param>
    /// <returns></returns>
    public virtual object CreateArgumentInstance(NameValueCollection properties = null)
    {
      object instance = Activator.CreateInstance(this.ArgumentType);
      if (properties != null)
      {
        for (int index = properties.Count - 1; index >= 0; --index)
        {
          string propName = properties.GetKey(index);
          ImageProcessingProperty property = this.argProperties.Where<ImageProcessingProperty>((Func<ImageProcessingProperty, bool>) (pr => pr.PropertyInfo.Name.ToLowerInvariant() == propName.ToLowerInvariant())).FirstOrDefault<ImageProcessingProperty>();
          if (property != null)
          {
            string stringValue = properties.Get(index);
            if (stringValue.IsNullOrEmpty())
            {
              if (property.PropertyInfo.PropertyType.IsValueType)
                stringValue = Activator.CreateInstance(property.PropertyInfo.PropertyType).ToString();
              else
                continue;
            }
            object obj = this.ConvertPropertyValueFromString(property, stringValue);
            property.PropertyInfo.SetValue(instance, obj, (object[]) null);
          }
        }
      }
      return instance;
    }

    public static object GetDefault(Type type) => type.IsValueType ? Activator.CreateInstance(type) : (object) null;

    protected virtual object ConvertPropertyValueFromString(
      ImageProcessingProperty property,
      string stringValue)
    {
      Type propertyType = property.PropertyInfo.PropertyType;
      object obj;
      if (propertyType == typeof (string))
        obj = (object) stringValue;
      else if (propertyType == typeof (bool))
        obj = (object) bool.Parse(stringValue);
      else if (propertyType == typeof (DateTime))
        obj = (object) DateTime.Parse(stringValue, (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
      else if (propertyType == typeof (float) || propertyType == typeof (double) || propertyType == typeof (Decimal))
        obj = (object) float.Parse(stringValue, (IFormatProvider) CultureInfo.InvariantCulture);
      else if (propertyType == typeof (Type))
      {
        obj = (object) TypeResolutionService.ResolveType(stringValue, false);
      }
      else
      {
        TypeConverter typeConverter = property.Converter ?? TypeDescriptor.GetConverter(propertyType);
        obj = typeConverter != null && typeConverter.CanConvertFrom(typeof (string)) ? typeConverter.ConvertFromString(stringValue) : throw new NotSupportedException(string.Format("No appropriate conversion for {0} argument property {1} with type {2}.", (object) propertyType, (object) property.PropertyInfo.Name, (object) propertyType));
      }
      return obj;
    }

    protected internal virtual string ConvertPropertyValueToString(
      ImageProcessingProperty property,
      object value)
    {
      if (value == null)
        return string.Empty;
      Type propertyType = property.PropertyInfo.PropertyType;
      if (propertyType == typeof (DateTime))
        return ((DateTime) value).ToString("u");
      return propertyType.IsValueType ? (Activator.CreateInstance(propertyType).Equals(value) ? string.Empty : Convert.ToString(value, (IFormatProvider) CultureInfo.InvariantCulture)) : (property.Converter != null && property.Converter.CanConvertTo(typeof (string)) ? property.Converter.ConvertToString(value) : value.ToString());
    }

    protected virtual ImageProcessingProperty CreateImageProcessingProperty(
      PropertyInfo propertyInfo,
      ImageProcessingPropertyAttribute propertyAttribute)
    {
      return new ImageProcessingProperty(this, propertyInfo, propertyAttribute);
    }

    /// <summary>Validates the arguments.</summary>
    public void ValidateArguments(object args)
    {
      if (this.ValidateArgumentsMethodName.IsNullOrEmpty())
        return;
      MethodInfo methodInfo = ((IEnumerable<MethodInfo>) this.ImageProcessor.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)).FirstOrDefault<MethodInfo>((Func<MethodInfo, bool>) (x => x.Name == this.ValidateArgumentsMethodName));
      if (!(methodInfo != (MethodInfo) null))
        return;
      if (args is NameValueCollection)
        args = this.CreateArgumentInstance(args as NameValueCollection);
      methodInfo.Invoke((object) this.ImageProcessor, new object[1]
      {
        args
      });
    }

    /// <summary>
    /// Gets the argument properties that will be used when generating the image.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<ImageProcessingProperty> GetArgumentProperties() => (IEnumerable<ImageProcessingProperty>) this.argProperties;

    /// <summary>Gets the title.</summary>
    public virtual string GetTitle() => !this.resourceClassId.IsNullOrEmpty() ? Res.Get(this.resourceClassId, this.title) : this.title;

    internal bool IsBrowsable => this.isBrowsable;

    internal string GetSizeMessage(NameValueCollection configProperties)
    {
      string input = string.Empty;
      if (!this.resourceClassId.IsNullOrEmpty())
        input = Res.Get(this.resourceClassId, this.sizeFormat);
      string pattern = "{(.*?)}";
      for (Match match = Regex.Match(input, pattern); match.Success; match = match.NextMatch())
      {
        string propertyName = match.Value.Replace("{", string.Empty).Replace("}", string.Empty);
        if (this.argProperties.FirstOrDefault<ImageProcessingProperty>((Func<ImageProcessingProperty, bool>) (x => x.PropertyInfo.Name == propertyName)) != null)
          input = input.Replace(match.Value, configProperties[propertyName]);
      }
      return input;
    }
  }
}
