// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.DesignTimeSite
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;

namespace Telerik.Sitefinity.Abstractions
{
  /// <summary>
  /// Represents dummy site. Use this class to set controls in design mode.
  /// </summary>
  internal class DesignTimeSite : ISite, IServiceProvider
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Abstractions.DesignTimeSite" /> class.
    /// </summary>
    /// <param name="component">The component.</param>
    internal DesignTimeSite(IComponent component) => this.Component = component;

    /// <summary>
    /// Gets the component associated with the <see cref="T:System.ComponentModel.ISite" /> when implemented by a class.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// The <see cref="T:System.ComponentModel.IComponent" /> instance associated with the <see cref="T:System.ComponentModel.ISite" />.
    /// </returns>
    public IComponent Component { get; private set; }

    /// <summary>
    /// Gets the <see cref="T:System.ComponentModel.IContainer" /> associated with the <see cref="T:System.ComponentModel.ISite" /> when implemented by a class.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// The <see cref="T:System.ComponentModel.IContainer" /> instance associated with the <see cref="T:System.ComponentModel.ISite" />.
    /// </returns>
    public IContainer Container { get; private set; }

    /// <summary>
    /// Determines whether the component is in design mode when implemented by a class.
    /// </summary>
    /// <value></value>
    /// <returns>true if the component is in design mode; otherwise, false.</returns>
    public bool DesignMode => true;

    /// <summary>
    /// Gets or sets the name of the component associated with the <see cref="T:System.ComponentModel.ISite" /> when implemented by a class.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// The name of the component associated with the <see cref="T:System.ComponentModel.ISite" />; or null, if no name is assigned to the component.
    /// </returns>
    public string Name { get; set; }

    /// <summary>Gets the service object of the specified type.</summary>
    /// <param name="serviceType">An object that specifies the type of service object to get.</param>
    /// <returns>
    /// A service object of type <paramref name="serviceType" />.
    /// -or-
    /// null if there is no service object of type <paramref name="serviceType" />.
    /// </returns>
    public object GetService(Type serviceType) => (object) null;
  }
}
