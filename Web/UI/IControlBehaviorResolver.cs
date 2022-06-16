// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.IControlBehaviorResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Provides an interface to retrieve the behavior descriptor object of a <see cref="T:System.Web.UI.Control" /> control
  /// </summary>
  public interface IControlBehaviorResolver
  {
    /// <summary>
    /// Gets the object that defines the behavior of a <see cref="T:System.Web.UI.Control" /> control
    /// </summary>
    object GetBehaviorObject(Control control);

    /// <summary>
    /// Gets the full type name of the behavior object from a persisted control data.
    /// </summary>
    string GetBehaviorObjectType(ControlData ctrlData);

    /// <summary>Gets the persisted properties of the control.</summary>
    IEnumerable<ControlProperty> GetPersistedProperties(
      ControlData ctrlData);
  }
}
