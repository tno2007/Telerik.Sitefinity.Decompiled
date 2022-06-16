// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.PropertyPersisters.PropertyPersister
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Modules.Pages.PropertyPersisters
{
  /// <summary>
  /// Abstract class which contains all the information needed by PageManager to read the property
  /// of an object. This class is used to pass the property read information to specialized
  /// methods for reading the properties. The concrete implementations provide functionality specific
  /// to particular property type.
  /// </summary>
  public abstract class PropertyPersister
  {
    private object defaultInstance;
    private ObjectDataUtility objectDataUtility;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.PropertyPersisters.PropertyPersister" /> class.
    /// </summary>
    /// <param name="propertyDescriptor">The property descriptor.</param>
    /// <param name="persistentData">The persistent data.</param>
    /// <param name="liveInstance">The live instance.</param>
    public PropertyPersister(
      PropertyDescriptor propertyDescriptor,
      ObjectData persistentData,
      object liveInstance)
      : this(propertyDescriptor, persistentData, liveInstance, (object) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.PropertyPersisters.PropertyPersister" /> class.
    /// </summary>
    /// <param name="propertyDescriptor">The property descriptor.</param>
    /// <param name="persistentData">The persistent data.</param>
    /// <param name="liveInstance">The live instance.</param>
    public PropertyPersister(
      PropertyDescriptor propertyDescriptor,
      ObjectData persistentData,
      object liveInstance,
      object defaultInstance)
    {
      if (propertyDescriptor == null)
        throw new ArgumentNullException(nameof (propertyDescriptor));
      if (liveInstance == null)
        throw new ArgumentNullException(nameof (liveInstance));
      this.Descriptor = propertyDescriptor;
      this.PersistentData = persistentData;
      this.LiveInstance = liveInstance;
      this.defaultInstance = defaultInstance;
    }

    /// <summary>
    /// Gets an instance of <see cref="T:System.ComponentModel.PropertyDescriptor" /> that describes the property
    /// being read.
    /// </summary>
    public virtual PropertyDescriptor Descriptor { get; private set; }

    /// <summary>
    /// Gets an instance of <see cref="T:Telerik.Sitefinity.Pages.Model.ObjectData" /> used to persist the data (properties)
    /// of an object.
    /// </summary>
    public virtual ObjectData PersistentData { get; private set; }

    /// <summary>
    /// Gets the instance of <see cref="T:Telerik.Sitefinity.Pages.Model.ControlProperty" /> object which represents the
    /// parent property of the property being currently ready. If the property is a top
    /// level property the value of parent property is null.
    /// </summary>
    public virtual ControlProperty ParentProperty { get; set; }

    /// <summary>
    /// Gets a default instance of an object for which properties are being read.
    /// </summary>
    /// <remarks>
    /// This object is used to decide whether property should be persisted or not. The logic
    /// is that we compare the value of the property on default instance and live instance and
    /// we persist property value only if they are different.
    /// </remarks>
    public virtual object DefaultInstance
    {
      get
      {
        if (this.defaultInstance == null)
          this.defaultInstance = this.CreateDefaultObject(this.LiveInstance.GetType());
        return this.defaultInstance;
      }
    }

    /// <summary>
    /// Gets the value of the property from the default instance of the component.
    /// </summary>
    /// <returns>The value of the property from the default instance.</returns>
    public virtual object DefaultInstanceValue => this.DefaultInstance == null ? (object) null : this.ObjectDataUtility.GetPropertyValue(this.Descriptor, this.DefaultInstance);

    /// <summary>
    /// Gets a live instance of an object for which properties are being read.
    /// </summary>
    /// <remarks>
    /// If the value of the property on the live instance is different from the value of the
    /// same property on the default instance, the value of the live instance property will be
    /// persisted by Sitefinity.
    /// </remarks>
    public virtual object LiveInstance { get; private set; }

    /// <summary>
    /// Gets the value of the property from the live instance of the component.
    /// </summary>
    /// <returns>The value of the property from the live instance.</returns>
    public virtual object LiveInstanceValue => this.ObjectDataUtility.GetPropertyValue(this.Descriptor, this.LiveInstance);

    /// <summary>
    /// Determines whether the value of the property on the live instance ought to be persisted.
    /// </summary>
    /// <returns>
    /// True if the value on the live instance is different than the value on the default
    /// instance; otherwise false.
    /// </returns>
    public virtual bool DoPersist => !object.Equals(this.LiveInstanceValue, this.DefaultInstanceValue);

    /// <summary>
    /// Gets the instance of the <see cref="P:Telerik.Sitefinity.Modules.Pages.PropertyPersisters.PropertyPersister.ObjectDataUtility" /> object.
    /// </summary>
    protected internal virtual ObjectDataUtility ObjectDataUtility
    {
      get
      {
        if (this.objectDataUtility == null)
          this.objectDataUtility = new ObjectDataUtility();
        return this.objectDataUtility;
      }
      set => this.objectDataUtility = value;
    }

    /// <summary>
    /// Specifies the culture that will be used for pupulation of the properties
    /// </summary>
    public CultureInfo PropertyPopulationCulture { get; set; }

    public string PropertyCultureName => this.PropertyPopulationCulture != null ? this.PropertyPopulationCulture.Name : (string) null;

    /// <summary>
    /// This method reads the properties from the instance of the component and persists them to data layer.
    /// </summary>
    /// <param name="manager">An instance of <see cref="!:ControlManager" />.</param>
    /// <param name="properties">
    /// Collection of properties to which the read properties should be added.
    /// </param>
    public abstract void Read<TProvider>(
      ControlManager<TProvider> manager,
      IList<ControlProperty> properties)
      where TProvider : ContentDataProviderBase;

    /// <summary>
    /// This method reas the properties from the data layer and populates them on the instance of the component.
    /// </summary>
    /// <param name="manager">An instance of <see cref="!:ControlManager" />.</param>
    /// <param name="propertyData">An instance of <see cref="T:Telerik.Sitefinity.Pages.Model.ControlProperty" /> that should be read
    /// and the value of which should be populated on the instance of the object.</param>
    public abstract void Populate<TProvider>(
      ControlManager<TProvider> manager,
      ControlProperty propertyData)
      where TProvider : ContentDataProviderBase;

    /// <summary>
    /// Creates an instance of the object from the provided type.
    /// </summary>
    /// <param name="type">
    /// Type of object for which the default instance ought to be provided.
    /// </param>
    /// <returns>A default instance of the object.</returns>
    protected internal virtual object CreateDefaultObject(Type type) => this.ObjectDataUtility.CreateDefaultInstance(type);

    public virtual ControlProperty GetOrCreateProperty<TProvider>(
      ControlManager<TProvider> manager,
      IList<ControlProperty> properties)
      where TProvider : ContentDataProviderBase
    {
      IEnumerable<ControlProperty> source = properties.Where<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == this.Descriptor.Name && p.Language == this.PropertyCultureName));
      ControlProperty property;
      if (source.Count<ControlProperty>() > 0)
      {
        property = source.First<ControlProperty>();
        if (source.Count<ControlProperty>() > 1)
        {
          foreach (ControlProperty controlProperty in source.Skip<ControlProperty>(1).ToList<ControlProperty>())
            manager.Delete(controlProperty);
        }
      }
      else
      {
        property = manager.CreateProperty();
        property.Name = this.Descriptor.Name;
        property.Language = this.PropertyCultureName;
      }
      return property;
    }
  }
}
