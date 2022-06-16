// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.PropertyPersisters.StringablePropertyPersister
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Modules.Pages.PropertyPersisters
{
  /// <summary>
  /// Specialized property instructor class that is used for instructing page manager how and weather
  /// to persist properties that can be instantiated from string values.
  /// </summary>
  public class StringablePropertyPersister : PropertyPersister
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.PropertyPersisters.StringablePropertyPersister" /> class.
    /// </summary>
    /// <param name="propertyDescriptor">The property descriptor.</param>
    /// <param name="persistentData">The persistent data.</param>
    /// <param name="liveInstance">The live instance.</param>
    public StringablePropertyPersister(
      PropertyDescriptor propertyDescriptor,
      ObjectData persistentData,
      object liveInstance)
      : this(propertyDescriptor, persistentData, liveInstance, (object) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.PropertyPersisters.StringablePropertyPersister" /> class.
    /// </summary>
    /// <param name="propertyDescriptor">The property descriptor.</param>
    /// <param name="persistentData">The persistent data.</param>
    /// <param name="liveInstance">The live instance.</param>
    /// <param name="defaultInstance">The default instance.</param>
    public StringablePropertyPersister(
      PropertyDescriptor propertyDescriptor,
      ObjectData persistentData,
      object liveInstance,
      object defaultInstance)
      : base(propertyDescriptor, persistentData, liveInstance, defaultInstance)
    {
    }

    /// <summary>
    /// This method reads the properties from the instance and persists them to data layer.
    /// </summary>
    /// <param name="manager">An instance of <see cref="!:ControlManager" />.</param>
    /// <param name="properties">
    /// Collection of the properties to which all read properties should be added.
    /// </param>
    public override void Read<TProvider>(
      ControlManager<TProvider> manager,
      IList<ControlProperty> properties)
    {
      ControlProperty controlProperty = manager != null ? this.GetOrCreateProperty<TProvider>(manager, properties) : throw new ArgumentNullException(nameof (manager));
      controlProperty.Value = this.LiveInstanceValueString;
      properties.Add(controlProperty);
    }

    /// <summary>Populates the specified manager.</summary>
    /// <param name="manager">The manager.</param>
    /// <param name="propertyData">The property data.</param>
    public override void Populate<TProvider>(
      ControlManager<TProvider> manager,
      ControlProperty propertyData)
    {
      if (string.IsNullOrEmpty(propertyData.Value))
        return;
      this.Descriptor.SetValue(this.LiveInstance, this.Descriptor.Converter.ConvertFromInvariantString(propertyData.Value));
    }

    /// <summary>
    /// Determines whether the value of the property on the live instance ought to be persisted.
    /// </summary>
    /// <returns>
    /// True if the value on the live instance is different than the value on the default
    /// instance; otherwise false.
    /// </returns>
    public override bool DoPersist => (!string.IsNullOrEmpty(this.LiveInstanceValueString) || !string.IsNullOrEmpty(this.DefaultInstanceValueString)) && !object.Equals(this.LiveInstanceValue, this.DefaultInstanceValue);

    /// <summary>
    /// Gets the string representation of the property value on the live instance object.
    /// </summary>
    public virtual string LiveInstanceValueString => this.LiveInstanceValue == null ? (string) null : this.Descriptor.Converter.ConvertToInvariantString(this.LiveInstanceValue);

    /// <summary>
    /// Gets the string representation of the property value on the default instance object.
    /// </summary>
    private string DefaultInstanceValueString => this.DefaultInstanceValue == null ? (string) null : this.Descriptor.Converter.ConvertToInvariantString(this.DefaultInstanceValue);
  }
}
