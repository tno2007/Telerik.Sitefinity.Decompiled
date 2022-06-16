// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.PropertyPersisters.MixedContentContextPropertyPersister
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Newtonsoft.Json;
using Progress.Sitefinity.Renderer.Entities.Content;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Modules.Pages.PropertyPersisters
{
  internal class MixedContentContextPropertyPersister : PropertyPersister
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.PropertyPersisters.MixedContentContextPropertyPersister" /> class.
    /// </summary>
    /// <param name="propertyDescriptor">The property descriptor.</param>
    /// <param name="persistentData">The persistent data.</param>
    /// <param name="liveInstance">The live instance.</param>
    public MixedContentContextPropertyPersister(
      PropertyDescriptor propertyDescriptor,
      ObjectData persistentData,
      object liveInstance)
      : this(propertyDescriptor, persistentData, liveInstance, (object) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.PropertyPersisters.MixedContentContextPropertyPersister" /> class.
    /// </summary>
    /// <param name="propertyDescriptor">The property descriptor.</param>
    /// <param name="persistentData">The persistent data.</param>
    /// <param name="liveInstance">The live instance.</param>
    /// <param name="defaultInstance">The default instance.</param>
    public MixedContentContextPropertyPersister(
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
    /// <typeparam name="TProvider">The element type of the array</typeparam>
    public override void Read<TProvider>(
      ControlManager<TProvider> manager,
      IList<ControlProperty> properties)
    {
      ControlProperty controlProperty = manager != null ? this.GetOrCreateProperty<TProvider>(manager, properties) : throw new ArgumentNullException(nameof (manager));
      if (this.LiveInstanceValue != null)
      {
        try
        {
          controlProperty.Value = JsonConvert.SerializeObject(this.LiveInstanceValue);
        }
        catch (Exception ex)
        {
        }
      }
      properties.Add(controlProperty);
    }

    /// <summary>
    /// This method reads the properties from the data layer and populates them on the instance
    /// of the component.
    /// </summary>
    /// <param name="manager">An instance of <see cref="!:ControlManager" />.</param>
    /// <param name="propertyData">An instance of <see cref="T:Telerik.Sitefinity.Pages.Model.ControlProperty" /> that should be read
    /// and the value of which should be populated on the instance of the object.</param>
    /// <typeparam name="ContentDataProviderBase">The element type of the array</typeparam>
    public override void Populate<ContentDataProviderBase>(
      ControlManager<ContentDataProviderBase> manager,
      ControlProperty propertyData)
    {
      if (string.IsNullOrEmpty(propertyData.Value))
        return;
      try
      {
        this.Descriptor.SetValue(this.LiveInstance, (object) JsonConvert.DeserializeObject<MixedContentContext>(propertyData.Value));
      }
      catch (Exception ex)
      {
      }
    }

    /// <summary>
    /// Compares the values of public browsable properties of the provided objects and returns true if
    /// they are identical, otherwise returns false.
    /// </summary>
    /// <param name="source">The source.</param>
    /// <param name="target">The target.</param>
    /// <param name="propertyType">Type of the property.</param>
    /// <returns>If properties are equal.</returns>
    protected internal virtual bool ArePropertiesEqual(
      object source,
      object target,
      Type propertyType)
    {
      return source == null && target == null;
    }
  }
}
