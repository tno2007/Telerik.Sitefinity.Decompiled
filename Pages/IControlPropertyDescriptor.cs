// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Pages.IControlPropertyDescriptor
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ComponentModel;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Pages
{
  /// <summary>
  /// Defines a base interface for components capable of retrieving a collection of
  /// property descriptors based on a specified control property data.
  /// </summary>
  public interface IControlPropertyDescriptor
  {
    /// <summary>
    /// Gets the child properties based on the specified <paramref name="data" />.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <returns>A collection of property descriptors.</returns>
    PropertyDescriptorCollection GetChildProperties(ControlProperty data);
  }
}
