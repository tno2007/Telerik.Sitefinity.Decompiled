﻿// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.DataProcessing.Helpers.PropertyValue
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ComponentModel;

namespace Telerik.Sitefinity.Data.DataProcessing.Helpers
{
  internal class PropertyValue : IPropertyValue
  {
    private PropertyDescriptor property;
    private object item;

    public PropertyValue(PropertyDescriptor property, object item)
    {
      this.property = property;
      this.item = item;
    }

    public object GetValue() => this.property.GetValue(this.item);

    public void SetValue(object value) => this.property.SetValue(this.item, value);

    public PropertyDescriptor GetProperty() => this.property;
  }
}
