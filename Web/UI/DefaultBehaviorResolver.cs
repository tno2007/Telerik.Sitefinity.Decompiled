// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.DefaultBehaviorResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// The behavior resolver component for Sitefinity web form controls.
  /// </summary>
  internal class DefaultBehaviorResolver : IControlBehaviorResolver
  {
    /// <summary>
    /// Gets the object that defines the behavior of a <see cref="T:System.Web.UI.Control" /> control.
    /// </summary>
    /// <param name="control">The control data.</param>
    /// <returns>The object that defines the behavior of a <see cref="T:System.Web.UI.Control" />.</returns>
    public object GetBehaviorObject(Control control) => (object) control;

    /// <summary>
    /// Gets the full type name of the behavior object from a persisted control data.
    /// </summary>
    /// <param name="ctrlData">The control data.</param>
    /// <returns>The full type name of the behavior object from a persisted control data.</returns>
    public string GetBehaviorObjectType(ControlData ctrlData) => ctrlData.ObjectType;

    /// <summary>Gets the persisted properties of the control.</summary>
    /// <param name="ctrlData">The control data.</param>
    /// <returns>The persisted properties of the control.</returns>
    public IEnumerable<ControlProperty> GetPersistedProperties(
      ControlData ctrlData)
    {
      return (IEnumerable<ControlProperty>) ctrlData.Properties;
    }
  }
}
