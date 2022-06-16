// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.WrapperObjectTypeDescriptor
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Telerik.Sitefinity.Publishing.Model;

namespace Telerik.Sitefinity.Publishing
{
  /// <summary>Class that supplies dynamic custom type information
  /// for an instance of type <see cref="!:Telerik.Sitefinity.WrapperObject" />.
  /// </summary>
  internal class WrapperObjectTypeDescriptor : ICustomTypeDescriptor
  {
    private object instance;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Publishing.WrapperObjectTypeDescriptor" /> class.
    /// </summary>
    /// <param name="instance">The instance.</param>
    public WrapperObjectTypeDescriptor(object instance) => this.instance = instance != null ? instance : throw new ArgumentNullException(nameof (instance));

    /// <summary>
    /// Returns a collection of custom attributes for this instance of a component.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.ComponentModel.AttributeCollection" /> containing the attributes for this object.
    /// </returns>
    public AttributeCollection GetAttributes() => TypeDescriptor.GetAttributes(this.instance);

    /// <summary>
    /// Returns the class name of this instance of a component.
    /// </summary>
    /// <returns>
    /// The class name of the object, or null if the class does not have a name.
    /// </returns>
    public string GetClassName() => TypeDescriptor.GetClassName(this.instance);

    /// <summary>Returns the name of this instance of a component.</summary>
    /// <returns>
    /// The name of the object, or null if the object does not have a name.
    /// </returns>
    public string GetComponentName() => TypeDescriptor.GetComponentName(this.instance);

    /// <summary>
    /// Returns a type converter for this instance of a component.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.ComponentModel.TypeConverter" /> that is the converter for this object, or null if there is no <see cref="T:System.ComponentModel.TypeConverter" /> for this object.
    /// </returns>
    public TypeConverter GetConverter() => TypeDescriptor.GetConverter(this.instance);

    /// <summary>
    /// Returns the default event for this instance of a component.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.ComponentModel.EventDescriptor" /> that represents the default event for this object, or null if this object does not have events.
    /// </returns>
    public EventDescriptor GetDefaultEvent() => TypeDescriptor.GetDefaultEvent(this.instance);

    /// <summary>
    /// Returns the default property for this instance of a component.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that represents the default property for this object, or null if this object does not have properties.
    /// </returns>
    public PropertyDescriptor GetDefaultProperty() => TypeDescriptor.GetDefaultProperty(this.instance);

    /// <summary>
    /// Returns an editor of the specified type for this instance of a component.
    /// </summary>
    /// <param name="editorBaseType">A <see cref="T:System.Type" /> that represents the editor for this object.</param>
    /// <returns>
    /// An <see cref="T:System.Object" /> of the specified type that is the editor for this object, or null if the editor cannot be found.
    /// </returns>
    public object GetEditor(Type editorBaseType) => TypeDescriptor.GetEditor(this.instance, editorBaseType);

    /// <summary>
    /// Returns the events for this instance of a component using the specified attribute array as a filter.
    /// </summary>
    /// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that is used as a filter.</param>
    /// <returns>
    /// An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> that represents the filtered events for this component instance.
    /// </returns>
    public EventDescriptorCollection GetEvents(Attribute[] attributes) => TypeDescriptor.GetEvents(this.instance, attributes);

    /// <summary>Returns the events for this instance of a component.</summary>
    /// <returns>
    /// An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> that represents the events for this component instance.
    /// </returns>
    public EventDescriptorCollection GetEvents() => TypeDescriptor.GetEvents(this.instance);

    /// <summary>
    /// Returns the properties for this instance of a component using the attribute array as a filter.
    /// </summary>
    /// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that is used as a filter.</param>
    /// <returns>
    /// A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that represents the filtered properties for this component instance.
    /// </returns>
    public PropertyDescriptorCollection GetProperties(
      Attribute[] attributes)
    {
      List<PropertyDescriptor> propertyDescriptorList = this.GetNewPropertyDescriptorList();
      WrapperObject instanseAsWrapperObject = this.GetInstanseAsWrapperObject();
      PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(instanseAsWrapperObject.WrappedObject);
      if (instanseAsWrapperObject.MappingSettings == null)
      {
        if (instanseAsWrapperObject.WrappedObject != null)
        {
          for (int index = 0; index < properties.Count; ++index)
          {
            PropertyDescriptor propertyDescriptor = properties[index];
            WrapperObjectPropertyInfo propInfo = new WrapperObjectPropertyInfo(propertyDescriptor.Name, propertyDescriptor.PropertyType);
            propertyDescriptorList.Add((PropertyDescriptor) new WrapperObjectPropertyDescriptor((PropertyInfo) propInfo));
          }
        }
      }
      else if (this.instance != null)
      {
        foreach (Mapping mapping1 in instanseAsWrapperObject.MappingSettings.Mappings.Where<Mapping>((Func<Mapping, bool>) (m => ((IEnumerable<string>) m.SourcePropertyNames).Any<string>())))
        {
          Mapping mapping = mapping1;
          if (!propertyDescriptorList.Any<PropertyDescriptor>((Func<PropertyDescriptor, bool>) (pd => pd.Name == mapping.DestinationPropertyName)))
          {
            PropertyDescriptor propertyDescriptor = (PropertyDescriptor) null;
            foreach (string sourcePropertyName in mapping.SourcePropertyNames)
            {
              propertyDescriptor = properties.Find(sourcePropertyName, false);
              if (propertyDescriptor == null && !instanseAsWrapperObject.AdditionalProperties.ContainsKey(sourcePropertyName))
                instanseAsWrapperObject.AdditionalProperties.Add(sourcePropertyName, (object) null);
            }
            Type propertyType = typeof (object);
            if (propertyDescriptor != null)
              propertyType = propertyDescriptor.PropertyType;
            WrapperObjectPropertyInfo info = new WrapperObjectPropertyInfo(mapping.DestinationPropertyName, propertyType);
            propertyDescriptorList.Add((PropertyDescriptor) this.GetWrapperObjectPropertyDescriptor(info));
          }
        }
      }
      foreach (KeyValuePair<string, object> additionalProperty in instanseAsWrapperObject.AdditionalProperties)
      {
        KeyValuePair<string, object> pair = additionalProperty;
        if (!propertyDescriptorList.Any<PropertyDescriptor>((Func<PropertyDescriptor, bool>) (pd => pd.Name == pair.Key)))
          propertyDescriptorList.Add((PropertyDescriptor) this.GetWrapperObjectPropertyDescriptor(new WrapperObjectPropertyInfo(pair.Key, typeof (object))));
      }
      return new PropertyDescriptorCollection(propertyDescriptorList.ToArray());
    }

    private WrapperObject GetInstanseAsWrapperObject() => this.instance as WrapperObject;

    private List<PropertyDescriptor> GetNewPropertyDescriptorList() => new List<PropertyDescriptor>();

    protected virtual WrapperObjectPropertyDescriptor GetWrapperObjectPropertyDescriptor(
      WrapperObjectPropertyInfo info)
    {
      return new WrapperObjectPropertyDescriptor((PropertyInfo) info);
    }

    /// <summary>
    /// Returns the properties for this instance of a component.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that represents the properties for this component instance.
    /// </returns>
    public PropertyDescriptorCollection GetProperties() => this.GetProperties((Attribute[]) null);

    /// <summary>Gets the property owner.</summary>
    /// <param name="pd">The property descriptor.</param>
    /// <returns></returns>
    public object GetPropertyOwner(PropertyDescriptor pd) => this.instance;
  }
}
