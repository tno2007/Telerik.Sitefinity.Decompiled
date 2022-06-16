// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.PropertyResolvingEventArgs`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Provides data for events fired before resolving the property.
  /// </summary>
  public class PropertyResolvingEventArgs<T> : EventArgs
  {
    private bool cancel;
    private string propertyName;
    private T instanceValue;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.PropertyResolvingEventArgs`1" /> class.
    /// </summary>
    public PropertyResolvingEventArgs()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.PropertyResolvingEventArgs`1" /> class.
    /// </summary>
    /// <param name="propertyName">Name of the property.</param>
    /// <param name="instanceValue">The instance value.</param>
    public PropertyResolvingEventArgs(string propertyName, T instanceValue)
    {
      this.propertyName = propertyName;
      this.instanceValue = instanceValue;
    }

    /// <summary>Gets the name of the property.</summary>
    /// <value>The name of the property.</value>
    public string PropertyName => this.propertyName;

    /// <summary>Gets the instance value.</summary>
    /// <value>The instance value.</value>
    public T InstanceValue => this.instanceValue;

    /// <summary>
    /// Gets or sets a value indicating whether the current resolution should be canceled.
    /// </summary>
    /// <value><c>true</c> if cancel; otherwise, <c>false</c>.</value>
    public bool Cancel
    {
      get => this.cancel;
      set => this.cancel = value;
    }
  }
}
