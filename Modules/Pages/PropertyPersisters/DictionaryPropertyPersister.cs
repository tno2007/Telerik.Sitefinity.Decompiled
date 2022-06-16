// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.PropertyPersisters.DictionaryPropertyPersister
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Modules.Pages.PropertyPersisters
{
  /// <summary>
  /// Specialized property instructor class that is used for instructing page manager how and weather
  /// to persist properties types that implement <see cref="T:System.Collections.IDictionary" /> interface.
  /// </summary>
  public class DictionaryPropertyPersister : PropertyPersister
  {
    public DictionaryPropertyPersister(
      PropertyDescriptor propertyDescriptor,
      ObjectData persistentData,
      object liveInstance)
      : this(propertyDescriptor, persistentData, liveInstance, (object) null)
    {
    }

    public DictionaryPropertyPersister(
      PropertyDescriptor propertyDescriptor,
      ObjectData persistentData,
      object liveInstance,
      object defaultInstance)
      : base(propertyDescriptor, persistentData, liveInstance, defaultInstance)
    {
    }

    /// <summary>
    /// Gets the value of the property on the live instance, already cast to the
    /// <see cref="T:System.Collections.IList" /> type.
    /// </summary>
    public virtual IDictionary LiveInstanceDictionary => this.LiveInstanceValue as IDictionary;

    /// <summary>
    /// Determines whether the value of the property on the live instance ought to be persisted.
    /// </summary>
    /// <returns>
    /// True if the value on the live instance is different than the value on the default
    /// instance; otherwise false.
    /// </returns>
    /// <remarks>
    /// Lists are always persisted if they are not null and have at least one item. For the sake of
    /// simplicity Sitefinity will always clear the previous list items and replace them with new items.
    /// </remarks>
    public override bool DoPersist => this.LiveInstanceDictionary != null && this.LiveInstanceDictionary.Count > 0;

    /// <summary>
    /// This method reads the properties from the instance of the component and persists them to data layer.
    /// </summary>
    /// <param name="manager">An instance of <see cref="!:ControlManager" />.</param>
    /// <param name="properties">
    /// Collection of properties to which the read properties should be added.
    /// </param>
    public override void Read<TProvider>(
      ControlManager<TProvider> manager,
      IList<ControlProperty> properties)
    {
      ControlProperty controlProperty = manager != null ? this.GetOrCreateProperty<TProvider>(manager, properties) : throw new ArgumentNullException(nameof (manager));
      controlProperty.Value = (string) null;
      IDictionary instanceDictionary = this.LiveInstanceDictionary;
      foreach (object key in (IEnumerable) instanceDictionary.Keys)
      {
        object component = instanceDictionary[key];
        if (component.GetType().GetConstructor(new Type[0]) != (ConstructorInfo) null)
        {
          ObjectData objectData = manager.CreateObjectData(false);
          manager.ReadProperties(component, objectData);
          objectData.DictionaryKey = key.ToString();
          controlProperty.ListItems.Add(objectData);
        }
      }
      properties.Add(controlProperty);
    }

    /// <summary>
    /// This method reas the properties from the data layer and populates them on the instance
    /// of the component.
    /// </summary>
    /// <param name="manager">An instance of <see cref="!:ControlManager" />.</param>
    /// <param name="propertyData">An instance of <see cref="T:Telerik.Sitefinity.Pages.Model.ControlProperty" /> that should be read
    /// and the value of which should be populated on the instance of the object.</param>
    public override void Populate<TProvider>(
      ControlManager<TProvider> manager,
      ControlProperty propertyData)
    {
      if (this.LiveInstanceDictionary == null)
        return;
      foreach (ObjectData listItem in (IEnumerable<ObjectData>) propertyData.ListItems)
        this.LiveInstanceDictionary[(object) listItem.DictionaryKey] = manager.LoadObject(listItem);
    }
  }
}
