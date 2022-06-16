// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.InlineEditing.Setters.IPropertySetter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using Telerik.Sitefinity.Services.InlineEditing;

namespace Telerik.Sitefinity.InlineEditing.Setters
{
  /// <summary>
  /// Every setter used from Inline editing needs to implement this interface and to be registered.
  /// The setters are used in the strategies to set the field values and they are resolved by the interface.
  /// </summary>
  public interface IPropertySetter
  {
    /// <summary>Returns the type of the property being set</summary>
    Type GetPropertyType();

    /// <summary>Sets the property value</summary>
    /// <param name="item">The item</param>
    /// <param name="field">The item`s field</param>
    /// <param name="property">The property</param>
    void Set(object item, FieldValueModel field, PropertyDescriptor property);
  }
}
