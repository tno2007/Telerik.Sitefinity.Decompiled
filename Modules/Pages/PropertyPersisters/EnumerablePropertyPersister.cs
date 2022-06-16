// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.PropertyPersisters.EnumerablePropertyPersister
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.ComponentModel;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Modules.Pages.PropertyPersisters
{
  /// <summary>
  /// Specialized property instructor class that is used for instructing page manager how and weather
  /// to persist properties types that implement <see cref="!:IEnumerable" /> interface.
  /// </summary>
  public class EnumerablePropertyPersister : PropertyPersister
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.PropertyPersisters.EnumerablePropertyPersister" /> class.
    /// </summary>
    /// <param name="propertyDescriptor">The property descriptor.</param>
    /// <param name="persistentData">The persistent data.</param>
    /// <param name="liveInstance">The live instance.</param>
    public EnumerablePropertyPersister(
      PropertyDescriptor propertyDescriptor,
      ObjectData persistentData,
      object liveInstance)
      : this(propertyDescriptor, persistentData, liveInstance, (object) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.PropertyPersisters.EnumerablePropertyPersister" /> class.
    /// </summary>
    /// <param name="propertyDescriptor">The property descriptor.</param>
    /// <param name="persistentData">The persistent data.</param>
    /// <param name="liveInstance">The live instance.</param>
    /// <param name="defaultInstance">The default instance.</param>
    public EnumerablePropertyPersister(
      PropertyDescriptor propertyDescriptor,
      ObjectData persistentData,
      object liveInstance,
      object defaultInstance)
      : base(propertyDescriptor, persistentData, liveInstance, defaultInstance)
    {
    }

    public override bool DoPersist => false;

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
    }

    /// <summary>
    /// This method reas the properties from the data layer and populates them on the
    /// instance of the component.
    /// </summary>
    /// <param name="manager">An instance of <see cref="!:ControlManager" />.</param>
    /// <param name="propertyData">An instance of <see cref="T:Telerik.Sitefinity.Pages.Model.ControlProperty" /> that should be read
    /// and the value of which should be populated on the instance of the object.</param>
    public override void Populate<TProvider>(
      ControlManager<TProvider> manager,
      ControlProperty propertyData)
    {
    }
  }
}
