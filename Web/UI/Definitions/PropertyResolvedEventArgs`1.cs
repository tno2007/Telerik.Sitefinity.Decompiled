// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.PropertyResolvedEventArgs`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Provides data for events fired after resolving the property.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class PropertyResolvedEventArgs<T> : EventArgs
  {
    private string propertyName;
    private T instanceValue;
    private T resolvedValue;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.PropertyResolvedEventArgs`1" /> class.
    /// </summary>
    public PropertyResolvedEventArgs()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.PropertyResolvedEventArgs`1" /> class.
    /// </summary>
    /// <param name="propertyName">Name of the property.</param>
    /// <param name="instanceValue">The instance value.</param>
    /// <param name="resolvedValue">The resolved value.</param>
    public PropertyResolvedEventArgs(string propertyName, T instanceValue, T resolvedValue)
    {
      this.propertyName = propertyName;
      this.instanceValue = instanceValue;
      this.resolvedValue = resolvedValue;
    }

    /// <summary>Gets the name of the property.</summary>
    /// <value>The name of the property.</value>
    public string PropertyName => this.propertyName;

    /// <summary>Gets the instance value.</summary>
    /// <value>The instance value.</value>
    public T InstanceValue => this.instanceValue;
  }
}
