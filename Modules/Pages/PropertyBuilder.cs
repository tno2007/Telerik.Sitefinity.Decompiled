// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.PropertyBuilder
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Telerik.Sitefinity.Modules.Pages.Web.Services;
using Telerik.Sitefinity.Pages;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Modules.Pages
{
  /// <summary>Property Builder</summary>
  [DebuggerDisplay("PropertyBuilder, Name={descriptor.Name}, Type={descriptor.PropertyType}")]
  public class PropertyBuilder : IOrderedProperties
  {
    private object value;
    private PropertyDescriptor descriptor;
    private List<PropertyBuilder> childBuilders;
    private List<ControlBuilder> listItems;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.PropertyBuilder" /> class.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="descriptor">The descriptor.</param>
    public PropertyBuilder(ControlProperty data, PropertyDescriptor descriptor)
      : this(data, descriptor, (object) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.PropertyBuilder" /> class.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <param name="descriptor">The descriptor.</param>
    /// <param name="instance">The instance.</param>
    public PropertyBuilder(ControlProperty data, PropertyDescriptor descriptor, object instance)
    {
      this.descriptor = descriptor;
      object obj = instance != null ? descriptor.GetValue(instance) : (object) null;
      TypeConverter converter = this.descriptor.Converter;
      if (converter.CanConvertFrom(typeof (string)) && converter.IsValid((object) (data.Value ?? string.Empty)))
        this.value = converter.ConvertFromInvariantString(data.Value ?? string.Empty);
      else if (data.HasListItems())
      {
        List<ObjectData> list = data.ListItems.OrderBy<ObjectData, int>((Func<ObjectData, int>) (li => li.CollectionIndex)).ToList<ObjectData>();
        this.listItems = new List<ControlBuilder>(list.Count);
        foreach (ObjectData data1 in list)
          this.listItems.Add(new ControlBuilder(data1));
      }
      else if (descriptor is IControlPropertyDescriptor)
      {
        PropertyDescriptorCollection childProperties = ((IControlPropertyDescriptor) descriptor).GetChildProperties(data);
        this.AddPropertiesToChildBuilders(data, childProperties, obj);
      }
      else
      {
        PropertyDescriptorCollection properties = obj == null || Attribute.GetCustomAttribute((MemberInfo) this.descriptor.PropertyType, typeof (ReflectInheritedPropertiesAttribute)) == null && !this.descriptor.Attributes.OfType<ReflectInheritedPropertiesAttribute>().Any<ReflectInheritedPropertiesAttribute>() ? TypeDescriptor.GetProperties(descriptor.PropertyType) : TypeDescriptor.GetProperties(obj);
        this.AddPropertiesToChildBuilders(data, properties, obj);
      }
    }

    /// <summary>Sets the property.</summary>
    /// <param name="component">The component.</param>
    public void SetProperty(object component)
    {
      if (this.childBuilders != null)
      {
        object component1 = this.descriptor.GetValue(component);
        if (component1 == null)
        {
          ConstructorInfo constructor = this.descriptor.PropertyType.GetConstructor(new Type[0]);
          if (constructor != (ConstructorInfo) null)
            component1 = constructor.Invoke((object[]) null);
        }
        foreach (PropertyBuilder childBuilder in this.childBuilders)
          childBuilder.SetProperty(component1);
      }
      else if (this.listItems != null)
      {
        if (!(this.descriptor.GetValue(component) is IList list) && !this.descriptor.IsReadOnly)
        {
          ConstructorInfo constructor = this.descriptor.PropertyType.GetConstructor(new Type[0]);
          if (constructor != (ConstructorInfo) null)
          {
            list = (IList) constructor.Invoke((object[]) null);
            this.descriptor.SetValue(component, this.value);
          }
        }
        if (list == null)
          return;
        for (int index = 0; index < this.listItems.Count; ++index)
        {
          ControlBuilder listItem = this.listItems[index];
          if (list.Count > listItem.CollectionIndex)
            listItem.PupulateProperties(list[listItem.CollectionIndex]);
          else
            list.Add(listItem.CreateObject());
        }
      }
      else
        this.descriptor.SetValue(component, this.value);
    }

    /// <summary>
    /// Gets a value indicating whether this property depends on other properties and
    /// should be populated after the dependent ones
    /// </summary>
    [Obsolete("It should not be used at all. Instead use IOrderedProperties interface extensions (they internally use this logic).")]
    public bool IsDependantProperty => this.listItems != null || this.descriptor.PropertyType == typeof (object);

    /// <inheritdoc />
    public bool HasChildProperties() => this.childBuilders != null && this.childBuilders.Count<PropertyBuilder>() > 0;

    /// <inheritdoc />
    public bool HasListItems() => this.listItems != null && this.listItems.Count<ControlBuilder>() > 0;

    /// <summary>Adds the properties to child builders collection.</summary>
    /// <param name="data">The data.</param>
    /// <param name="properties">The properties.</param>
    /// <param name="instance">The instance.</param>
    private void AddPropertiesToChildBuilders(
      ControlProperty data,
      PropertyDescriptorCollection properties,
      object instance)
    {
      this.childBuilders = new List<PropertyBuilder>();
      if (properties == null)
        return;
      foreach (ControlProperty childProperty in (IEnumerable<ControlProperty>) data.ChildProperties)
      {
        PropertyDescriptor descriptor = properties.Find(childProperty.Name, true);
        if (descriptor != null)
        {
          TypeConverter converter = descriptor.Converter;
          if (converter.CanConvertFrom(typeof (string)) || converter.GetPropertiesSupported())
            this.childBuilders.Add(new PropertyBuilder(childProperty, descriptor, instance));
        }
      }
    }
  }
}
