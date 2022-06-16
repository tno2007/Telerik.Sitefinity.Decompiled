// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.WrapperObjectPropertyDescriptor
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Telerik.Sitefinity.Descriptors;

namespace Telerik.Sitefinity.Publishing
{
  /// <summary>
  /// Provides an abstraction of a property on a instances of type <see cref="!:Telerik.Sitefinity.WrapperObject" />
  /// </summary>
  [DebuggerDisplay("[WrapperObjectPropertyDescriptor] {Name} ")]
  public class WrapperObjectPropertyDescriptor : PropertyDescriptor, ILocalizablePropertyDescriptor
  {
    private readonly PropertyInfo propInfo;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Publishing.WrapperObjectPropertyDescriptor" /> class.
    /// </summary>
    /// <param name="name">The name of the property.</param>
    public WrapperObjectPropertyDescriptor(string name)
      : base(name, (Attribute[]) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Publishing.WrapperObjectPropertyDescriptor" /> class.
    /// </summary>
    /// <param name="propInfo">The prop info.</param>
    public WrapperObjectPropertyDescriptor(PropertyInfo propInfo)
      : base(propInfo.Name, propInfo.GetCustomAttributes(true).Cast<Attribute>().ToArray<Attribute>())
    {
      this.propInfo = propInfo;
    }

    /// <inheritdoc />
    public override Type PropertyType
    {
      get
      {
        Type propertyType = typeof (object);
        if (this.propInfo != (PropertyInfo) null)
          propertyType = this.propInfo.PropertyType;
        return propertyType;
      }
    }

    /// <inheritdoc />
    public override bool IsReadOnly => this.propInfo != (PropertyInfo) null && !this.propInfo.CanWrite;

    /// <inheritdoc />
    public override Type ComponentType => this.propInfo.DeclaringType;

    /// <inheritdoc />
    public override object GetValue(object component) => ((WrapperObject) component).GetProperty(this.Name);

    /// <inheritdoc />
    object ILocalizablePropertyDescriptor.GetValue(
      object component,
      CultureInfo culture)
    {
      return ((WrapperObject) component).GetProperty(this.Name, culture);
    }

    /// <inheritdoc />
    public override void SetValue(object component, object value)
    {
      if (component is WrapperObject wrapperObject)
      {
        Dictionary<string, object> additionalProperties = wrapperObject.AdditionalProperties;
        if (additionalProperties.ContainsKey(this.Name))
        {
          additionalProperties[this.Name] = value;
          return;
        }
      }
      this.propInfo.SetValue(component, value, (object[]) null);
    }

    /// <inheritdoc />
    public override bool CanResetValue(object component) => false;

    /// <inheritdoc />
    public override void ResetValue(object component) => throw new NotSupportedException();

    /// <inheritdoc />
    public override bool ShouldSerializeValue(object component) => false;
  }
}
