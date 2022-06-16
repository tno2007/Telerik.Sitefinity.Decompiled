// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.PropertyPersisters.ComplexPropertyPersister
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Modules.Pages.PropertyPersisters
{
  /// <summary>
  /// Specialized property instructor class that is used for instructing page manager how and weather
  /// to persist properties that have properties of their own.
  /// </summary>
  public class ComplexPropertyPersister : PropertyPersister
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.PropertyPersisters.ComplexPropertyPersister" /> class.
    /// </summary>
    /// <param name="propertyDescriptor">The property descriptor.</param>
    /// <param name="persistentData">The persistent data.</param>
    /// <param name="liveInstance">The live instance.</param>
    public ComplexPropertyPersister(
      PropertyDescriptor propertyDescriptor,
      ObjectData persistentData,
      object liveInstance)
      : this(propertyDescriptor, persistentData, liveInstance, (object) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.PropertyPersisters.ComplexPropertyPersister" /> class.
    /// </summary>
    /// <param name="propertyDescriptor">The property descriptor.</param>
    /// <param name="persistentData">The persistent data.</param>
    /// <param name="liveInstance">The live instance.</param>
    /// <param name="defaultInstance">The default instance.</param>
    public ComplexPropertyPersister(
      PropertyDescriptor propertyDescriptor,
      ObjectData persistentData,
      object liveInstance,
      object defaultInstance)
      : base(propertyDescriptor, persistentData, liveInstance, defaultInstance)
    {
    }

    /// <summary>
    /// Determines whether the value of the property on the live instance ought to be persisted.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// True if the value on the live instance is different than the value on the default
    /// instance; otherwise false.
    /// </returns>
    public override bool DoPersist => !this.ArePropertiesEqual(this.DefaultInstanceValue, this.LiveInstanceValue, this.Descriptor.PropertyType);

    /// <summary>
    /// This method reads the properties from the instance of the component and persists
    /// them to data layer.
    /// </summary>
    /// <param name="manager">An instance of <see cref="!:ControlManager" />.</param>
    /// <param name="properties">
    /// Collection of properties to which the read properties should be added.
    /// </param>
    public override void Read<TProvider>(
      ControlManager<TProvider> manager,
      IList<ControlProperty> properties)
    {
      ControlProperty property = this.GetOrCreateProperty<TProvider>(manager, properties);
      properties.Add(property);
      manager.ReadProperties(this.LiveInstanceValue, property, this.PropertyPopulationCulture, this.DefaultInstanceValue);
      if (property.HasChildProps())
        return;
      manager.Delete(property);
    }

    /// <summary>
    /// This method reas the properties from the data layer and populates them on the instance
    /// of the component.
    /// </summary>
    /// <param name="manager">An instance of <see cref="!:ControlManager" />.</param>
    /// <param name="propertyData">An instance of <see cref="T:Telerik.Sitefinity.Pages.Model.ControlProperty" /> that should be read
    /// and the value of which should be populated on the instance of the object.</param>
    public override void Populate<ContentDataProviderBase>(
      ControlManager<ContentDataProviderBase> manager,
      ControlProperty propertyData)
    {
      object liveInstanceValue = this.LiveInstanceValue;
      if (liveInstanceValue == null)
        return;
      manager.PopulateProperties(liveInstanceValue, propertyData.ChildProperties);
    }

    /// <summary>
    /// Compares the values of public browsable properties of the provided objects and returns true if
    /// they are identical, otherwise returns false.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="target">The target.</param>
    /// <param name="propertyType">Type of the property.</param>
    /// <returns></returns>
    protected internal virtual bool ArePropertiesEqual(
      object source,
      object target,
      Type propertyType)
    {
      if (source == null && target == null)
        return true;
      if (source == null || target == null)
        return false;
      if (object.Equals(source, target))
        return true;
      Type type1 = source.GetType();
      Type type2 = target.GetType();
      if (propertyType == (Type) null)
      {
        if (type1 != type2)
          return false;
      }
      else if (!propertyType.IsAssignableFrom(type1) && propertyType.IsAssignableFrom(type2))
        throw new ArgumentException("Source '{0}' and target '{1}' objects must both be assignable to '{2}'.".Arrange((object) source.GetType().FullName, (object) target.GetType().FullName, (object) propertyType.FullName));
      if (type1.IsValueType)
        return false;
      foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(source))
      {
        if (property.IsBrowsable)
        {
          TypeConverter converter = property.Converter;
          if (converter.CanConvertFrom(typeof (string)))
          {
            object propertyValue1 = this.ObjectDataUtility.GetPropertyValue(property, source);
            object propertyValue2 = this.ObjectDataUtility.GetPropertyValue(property, target);
            if ((propertyValue1 != null || propertyValue2 != null) && (propertyValue1 == null || propertyValue2 == null || !object.Equals(propertyValue1, propertyValue2) && (!(propertyValue1 is ICollection) || !this.AreCollectionsEqual((ICollection) propertyValue1, (ICollection) propertyValue2))))
              return false;
          }
          else if (converter.GetPropertiesSupported())
          {
            object propertyValue3 = this.ObjectDataUtility.GetPropertyValue(property, source);
            object propertyValue4 = this.ObjectDataUtility.GetPropertyValue(property, target);
            if ((propertyValue3 != null || propertyValue4 != null) && (propertyValue3 != null && propertyValue4 != null && !this.ArePropertiesEqual(propertyValue3, propertyValue4, property.PropertyType) || propertyValue3 is ICollection && !this.AreCollectionsEqual((ICollection) propertyValue3, (ICollection) propertyValue4)))
              return false;
          }
        }
      }
      return true;
    }

    /// <summary>
    /// Determines whether two collections are equal, meaning that they have same number of elements,
    /// in the same order where all elements have the same properties.
    /// </summary>
    /// <param name="source">The source collection.</param>
    /// <param name="target">The target collection.</param>
    /// <returns>True if the collections are considered equal; otherwise false.</returns>
    protected internal virtual bool AreCollectionsEqual(ICollection source, ICollection target)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      if (target == null)
        throw new ArgumentNullException(nameof (target));
      if (object.Equals((object) source, (object) target))
        return true;
      if (source.Count != target.Count)
        return false;
      List<object> objectList1 = new List<object>();
      List<object> objectList2 = new List<object>();
      foreach (object obj in (IEnumerable) source)
        objectList1.Add(obj);
      foreach (object obj in (IEnumerable) target)
        objectList2.Add(obj);
      for (int index = 0; index < objectList1.Count; ++index)
      {
        object obj1 = objectList1[index];
        object obj2 = objectList2[index];
        if (!object.Equals(obj1, obj2) && !this.ArePropertiesEqual(obj1, obj2, this.GetCommonBaseType(obj1, obj2)))
          return false;
      }
      return true;
    }

    private Type GetCommonBaseType(object src, object trg)
    {
      Queue<Type> typeQueue1 = new Queue<Type>();
      Queue<Type> typeQueue2 = new Queue<Type>();
      typeQueue1.Enqueue(src.GetType());
      typeQueue2.Enqueue(trg.GetType());
      for (Type type = src.GetType(); type.BaseType != (Type) null; type = type.BaseType)
        typeQueue1.Enqueue(type.BaseType);
      for (Type type = trg.GetType(); type.BaseType != (Type) null; type = type.BaseType)
        typeQueue2.Enqueue(type.BaseType);
      Queue<Type> typeQueue3;
      Queue<Type> typeQueue4;
      if (typeQueue1.Count == typeQueue2.Count)
      {
        typeQueue3 = typeQueue1;
        typeQueue4 = typeQueue2;
      }
      else if (typeQueue1.Count > typeQueue2.Count)
      {
        typeQueue3 = typeQueue1;
        typeQueue4 = typeQueue2;
      }
      else
      {
        typeQueue3 = typeQueue2;
        typeQueue4 = typeQueue1;
      }
      Type commonBaseType = typeof (object);
      while (typeQueue3.Count > 0)
      {
        commonBaseType = typeQueue3.Dequeue();
        if (typeQueue4.Contains(commonBaseType))
          return commonBaseType;
      }
      return !(commonBaseType == typeof (object)) ? commonBaseType : throw new InvalidOperationException("The types of the collection being compared are not of the same type or do not have a common base class (excluding the type object)");
    }
  }
}
