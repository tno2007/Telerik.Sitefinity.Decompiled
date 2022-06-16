// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.PropertyPersisters.GenericListPropertyPersister
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
  /// Specialized property instructor class that is used for instructing control manager how and weather
  /// to persist properties types that implement <see cref="!: IList" /> interface.
  /// </summary>
  public class GenericListPropertyPersister : PropertyPersister
  {
    public GenericListPropertyPersister(
      PropertyDescriptor propertyDescriptor,
      ObjectData persistentData,
      object liveInstance)
      : base(propertyDescriptor, persistentData, liveInstance)
    {
    }

    /// <summary>
    /// This method reads the properties from the instance of the component and persists them to data layer.
    /// </summary>
    /// <typeparam name="TProvider"></typeparam>
    /// <param name="manager">An instance of <see cref="!:ControlManager" />.</param>
    /// <param name="properties">Collection of properties to which the read properties should be added.</param>
    public override void Read<TProvider>(
      ControlManager<TProvider> manager,
      IList<ControlProperty> properties)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// This method reas the properties from the data layer and populates them on the instance of the component.
    /// </summary>
    /// <typeparam name="TProvider"></typeparam>
    /// <param name="manager">An instance of <see cref="!:ControlManager" />.</param>
    /// <param name="propertyData">An instance of <see cref="T:Telerik.Sitefinity.Pages.Model.ControlProperty" /> that should be read
    /// and the value of which should be populated on the instance of the object.</param>
    public override void Populate<TProvider>(
      ControlManager<TProvider> manager,
      ControlProperty propertyData)
    {
      throw new NotImplementedException();
    }
  }
}
