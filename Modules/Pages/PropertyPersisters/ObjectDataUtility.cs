// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.PropertyPersisters.ObjectDataUtility
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Progress.Sitefinity.Renderer.Entities.Content;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Modules.Pages.PropertyPersisters
{
  /// <summary>
  /// This class provides utility methods for working with Sitefinity <see cref="T:Telerik.Sitefinity.Pages.Model.ObjectData" />
  /// class.
  /// </summary>
  public class ObjectDataUtility
  {
    /// <summary>
    /// Gets the type of persistence Sitefinity will use for the given property.
    /// </summary>
    /// <param name="propertyDescriptor">An instance of <see cref="T:System.ComponentModel.PropertyDescriptor" /> that describes
    /// the property that ought to be persisted.
    /// </param>
    /// <returns>One of the possible property persistence types.</returns>
    public virtual PropertyPersistenceType GetPersistenceType(
      PropertyDescriptor propertyDescriptor)
    {
      if (propertyDescriptor == null)
        throw new ArgumentNullException(nameof (propertyDescriptor));
      if (!propertyDescriptor.IsBrowsable && propertyDescriptor.Name != "ControllerName" && propertyDescriptor.Name != "ID")
        return PropertyPersistenceType.NotPersistable;
      TypeConverter converter = propertyDescriptor.GetConverter();
      if (converter.CanConvertFrom(typeof (string)) && !propertyDescriptor.IsReadOnly)
        return PropertyPersistenceType.StringConvertable;
      if (typeof (IList).IsAssignableFrom(propertyDescriptor.PropertyType) && propertyDescriptor.IsReadOnly)
        return PropertyPersistenceType.IList;
      if (typeof (IDictionary).IsAssignableFrom(propertyDescriptor.PropertyType))
        return PropertyPersistenceType.IDictionary;
      if (((IEnumerable<Type>) propertyDescriptor.PropertyType.GetInterfaces()).Any<Type>((Func<Type, bool>) (x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof (ICollection<>))) && propertyDescriptor.IsReadOnly)
        return PropertyPersistenceType.IListGeneric;
      if (typeof (IEnumerable).IsAssignableFrom(propertyDescriptor.PropertyType) && !propertyDescriptor.IsReadOnly)
        return PropertyPersistenceType.IEnumerable;
      return converter.GetPropertiesSupported() ? PropertyPersistenceType.ComplexProperty : PropertyPersistenceType.NotPersistable;
    }

    /// <summary>
    /// Resolves the concrete instance of property persister depenging on the supplied
    /// instance of <see cref="T:System.ComponentModel.PropertyDescriptor" />.
    /// </summary>
    /// <param name="propertyDescriptor">An instance of property descriptor for which the property persister ought to be returned.</param>
    /// <param name="objectData">An instance of the persistent object data type that acts as a parent (control or parent property).</param>
    /// <param name="liveInstance">An instance of the object on which the property described by the property descriptor is defined.</param>
    /// <param name="culture">The culture to be used for pupulating the values of property</param>
    /// <returns>
    /// A concrete implementation of abstract <see cref="T:Telerik.Sitefinity.Modules.Pages.PropertyPersisters.PropertyPersister" /> type that is capable of reading
    /// and populating a property of a given type. Returns null if no persister can work with the given type
    /// of a property.
    /// </returns>
    public virtual PropertyPersister ResolvePersister(
      PropertyDescriptor propertyDescriptor,
      ObjectData objectData,
      object liveInstance,
      CultureInfo culture = null,
      object defaultInstance = null)
    {
      culture = ControlHelper.NormalizeLanguage(culture);
      PropertyPersister persisterInstance = this.GetPersisterInstance(this.GetPersistenceType(propertyDescriptor), propertyDescriptor, objectData, liveInstance, defaultInstance);
      if (persisterInstance != null)
      {
        persisterInstance.ObjectDataUtility = this;
        if (culture != null)
          persisterInstance.PropertyPopulationCulture = culture;
      }
      return persisterInstance;
    }

    protected virtual PropertyPersister GetPersisterInstance(
      PropertyPersistenceType persistenceType,
      PropertyDescriptor propertyDescriptor,
      ObjectData objectData,
      object liveInstance,
      object defaultInstance = null)
    {
      PropertyPersister persisterInstance = (PropertyPersister) null;
      switch (persistenceType)
      {
        case PropertyPersistenceType.StringConvertable:
          persisterInstance = (PropertyPersister) new StringablePropertyPersister(propertyDescriptor, objectData, liveInstance, defaultInstance);
          break;
        case PropertyPersistenceType.ComplexProperty:
          persisterInstance = (PropertyPersister) new ComplexPropertyPersister(propertyDescriptor, objectData, liveInstance, defaultInstance);
          break;
        case PropertyPersistenceType.IEnumerable:
          persisterInstance = (PropertyPersister) new EnumerablePropertyPersister(propertyDescriptor, objectData, liveInstance);
          break;
        case PropertyPersistenceType.IList:
        case PropertyPersistenceType.IListGeneric:
          persisterInstance = (PropertyPersister) new ListPropertyPersister(propertyDescriptor, objectData, liveInstance, defaultInstance);
          break;
        case PropertyPersistenceType.IDictionary:
          persisterInstance = (PropertyPersister) new DictionaryPropertyPersister(propertyDescriptor, objectData, liveInstance, defaultInstance);
          break;
      }
      if (propertyDescriptor.PropertyType == typeof (MixedContentContext))
        persisterInstance = (PropertyPersister) new MixedContentContextPropertyPersister(propertyDescriptor, objectData, liveInstance);
      return persisterInstance;
    }

    /// <summary>
    /// Gets the value of the property described by the given property descriptor on the
    /// supplied component.
    /// </summary>
    /// <param name="descriptor">An instance of property descriptor that describes the property.</param>
    /// <param name="component">An instance on which the property has been declared.</param>
    /// <returns>The value of the property.</returns>
    public virtual object GetPropertyValue(PropertyDescriptor descriptor, object component)
    {
      if (descriptor == null)
        throw new ArgumentNullException(nameof (descriptor));
      if (component == null)
        throw new ArgumentNullException(nameof (component));
      try
      {
        return descriptor.GetValue(component);
      }
      catch (Exception ex)
      {
        if (this.HandleException(ex))
          throw;
        else
          return ((DefaultValueAttribute) descriptor.Attributes[typeof (DefaultValueAttribute)])?.Value;
      }
    }

    public virtual object CreateDefaultInstance(Type type)
    {
      if (type == (Type) null)
        throw new ArgumentNullException(nameof (type));
      object defaultInstance = (object) null;
      ConstructorInfo constructor = type.GetConstructor(new Type[0]);
      if (constructor != (ConstructorInfo) null)
        defaultInstance = constructor.Invoke((object[]) null);
      return defaultInstance;
    }

    /// <summary>
    /// Handles the exception and determines whether the exception should be thrown or not.
    /// </summary>
    /// <param name="ex">An instance of the original exception that was thrown.</param>
    /// <returns>
    /// True if the exception should be thrown; otherwise false.
    /// </returns>
    protected internal virtual bool HandleException(Exception ex) => Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions);
  }
}
