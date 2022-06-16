// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.PropertyValueChangedEventArgs
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>Represents event Arguments for property editor.</summary>
  public class PropertyValueChangedEventArgs : EventArgs
  {
    private object propertyValue;
    private string propertyName;
    private IDictionary<string, object> dependentProps;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.PropertyValueChangedEventArgs" /> class.
    /// </summary>
    /// <param name="propertyName">Name of the property.</param>
    /// <param name="propertyValue">The property value.</param>
    public PropertyValueChangedEventArgs(string propertyName, object propertyValue)
    {
      this.propertyName = propertyName;
      this.propertyValue = propertyValue;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.PropertyValueChangedEventArgs" /> class.
    /// </summary>
    /// <param name="propertyName">Name of the property.</param>
    /// <param name="propertyValue">The property value.</param>
    /// <param name="dependentProperties">The dependent properties.</param>
    public PropertyValueChangedEventArgs(
      string propertyName,
      object propertyValue,
      IDictionary<string, object> dependentProperties)
    {
      this.propertyName = propertyName;
      this.propertyValue = propertyValue;
      this.dependentProps = dependentProperties;
    }

    /// <summary>The name of the edited property</summary>
    public string PropertyName => this.propertyName;

    /// <summary>Gets the new value</summary>
    public object PropertyValue => this.propertyValue;

    /// <summary>Gets the dependent properties.</summary>
    /// <value>The dependent properties.</value>
    public IDictionary<string, object> DependentProperties => this.dependentProps;
  }
}
