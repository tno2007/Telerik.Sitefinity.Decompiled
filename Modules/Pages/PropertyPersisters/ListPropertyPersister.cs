// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.PropertyPersisters.ListPropertyPersister
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Modules.Pages.PropertyPersisters
{
  /// <summary>
  /// Specialized property instructor class that is used for instructing page manager how and weather
  /// to persist properties types that implement <see cref="T:System.Collections.IList" /> interface.
  /// </summary>
  public class ListPropertyPersister : PropertyPersister
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.PropertyPersisters.ListPropertyPersister" /> class.
    /// </summary>
    /// <param name="propertyDescriptor">The property descriptor.</param>
    /// <param name="persistentData">The persistent data.</param>
    /// <param name="liveInstance">The live instance.</param>
    public ListPropertyPersister(
      PropertyDescriptor propertyDescriptor,
      ObjectData persistentData,
      object liveInstance)
      : this(propertyDescriptor, persistentData, liveInstance, (object) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.PropertyPersisters.ListPropertyPersister" /> class.
    /// </summary>
    /// <param name="propertyDescriptor">The property descriptor.</param>
    /// <param name="persistentData">The persistent data.</param>
    /// <param name="liveInstance">The live instance.</param>
    /// <param name="defaultInstance">The default instance.</param>
    public ListPropertyPersister(
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
    public virtual IList LiveInstanceList => this.LiveInstanceValue as IList;

    /// <summary>
    /// Gets the value of the property on the default instance, already cast to the
    /// <see cref="T:System.Collections.IList" /> type.
    /// </summary>
    public virtual IList DefaultInstanceList => this.DefaultInstanceValue as IList;

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
    public override bool DoPersist
    {
      get
      {
        PersistenceModeAttribute attribute = (PersistenceModeAttribute) this.Descriptor.Attributes[typeof (PersistenceModeAttribute)];
        return attribute != null && attribute.Mode != PersistenceMode.Attribute && this.LiveInstanceList != null && this.LiveInstanceList.Count > 0;
      }
    }

    /// <summary>
    /// This method reads the properties from the instance and persists them to data layer.
    /// </summary>
    /// <param name="manager">
    /// An instance of <see cref="!:ControlManager" />.
    /// </param>
    /// <param name="properties">
    /// Collection of properties to which the read properties should be added.
    /// </param>
    public override void Read<TProvider>(
      ControlManager<TProvider> manager,
      IList<ControlProperty> properties)
    {
      ControlProperty controlProperty = manager != null ? this.GetOrCreateProperty<TProvider>(manager, properties) : throw new ArgumentNullException(nameof (manager));
      controlProperty.Value = (string) null;
      IList liveInstanceList = this.LiveInstanceList;
      IList defaultInstanceList = this.DefaultInstanceList;
      for (int index = 0; index < liveInstanceList.Count; ++index)
      {
        object component = liveInstanceList[index];
        object defaultComponentInstance = (object) null;
        if (defaultInstanceList != null && defaultInstanceList.Count > index)
          defaultComponentInstance = defaultInstanceList[index];
        if (component.GetType().GetConstructor(new Type[0]) != (ConstructorInfo) null)
        {
          ObjectData objectData = manager.CreateObjectData(false);
          manager.ReadProperties(component, objectData, this.PropertyPopulationCulture, defaultComponentInstance);
          if (objectData.Properties.Count == 0)
          {
            manager.DeleteItem((object) objectData);
          }
          else
          {
            objectData.CollectionIndex = index;
            objectData.ParentProperty = controlProperty;
            if (!controlProperty.ListItems.Contains(objectData))
              controlProperty.ListItems.Add(objectData);
          }
        }
      }
      if (!controlProperty.HasListItems())
        manager.Delete(controlProperty);
      else
        properties.Add(controlProperty);
    }

    /// <summary>
    /// This method reas the properties from the data layer and populates them on the instance of the component.
    /// </summary>
    /// <param name="manager">An instance of <see cref="!:ControlManager" />.</param>
    /// <param name="propertyData">
    /// An instance of <see cref="T:Telerik.Sitefinity.Pages.Model.ControlProperty" /> that should be read
    /// and the value of which should be populated on the instance of the object.
    /// </param>
    [SuppressMessage("Telerik.Sitefinity", "SF1001:AvoidToListOnIQueryable")]
    public override void Populate<TProvider>(
      ControlManager<TProvider> manager,
      ControlProperty propertyData)
    {
      List<ObjectData> list = propertyData.ListItems.OrderBy<ObjectData, int>((Func<ObjectData, int>) (i => i.CollectionIndex)).ToList<ObjectData>();
      for (int index = 0; index < list.Count<ObjectData>(); ++index)
      {
        ObjectData objectData = list[index];
        if (this.LiveInstanceList.Count > objectData.CollectionIndex)
          manager.PopulateProperties(this.LiveInstanceList[objectData.CollectionIndex], (IList<ControlProperty>) objectData.GetProperties(true).ToList<ControlProperty>(), objectData: objectData);
        else
          this.LiveInstanceList.Add(manager.LoadObject(objectData));
      }
    }
  }
}
